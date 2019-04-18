Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRPrescription

Public Class frmRxProviderAssociation

#Region " Private Variables "
    Private _PatientID As Int64
    Private _VisitID As Int64
    Private _dtPrescriptionDate As DateTime = Now
    Private _ProviderID As Int64
    Private _dtProviderAssociation As DataTable

    Private oToolTip As New ToolTip
    Private oProvider As New clsProvider

    Private _FormLoading As Boolean = False
    Private _TemporarySave As Boolean = False    
    Private WithEvents _RxBusinessLayer As RxBusinesslayer
#End Region

#Region " Constructor "
    Public Sub New(ByVal PatientID As Long)
        InitializeComponent()
        _PatientID = PatientID
    End Sub
    Public Sub New(ByVal nPatientID As Int64, ByVal nVisitID As Int64, ByVal dtPrescriptionDate As DateTime, ByVal nProviderID As Int64)
        InitializeComponent()
        _PatientID = nPatientID
        _VisitID = nVisitID
        _dtPrescriptionDate = dtPrescriptionDate
        _ProviderID = nProviderID
    End Sub
    Public Sub New(ByVal nProviderID As Int64, ByVal dtProviderAssociation As DataTable)
        InitializeComponent()
        _dtProviderAssociation = dtProviderAssociation
        _TemporarySave = True
        _ProviderID = nProviderID
    End Sub
#End Region

#Region " Public Properties "
    Public ReadOnly Property ProviderAssociation() As DataTable
        Get
            Return _dtProviderAssociation
        End Get
    End Property
#End Region

#Region " Form Events "
    Private Sub frmRxProviderAssociation_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        oToolTip.RemoveAll()
        oToolTip.Dispose()
        oToolTip = Nothing

        If _RxBusinessLayer IsNot Nothing Then
            _RxBusinessLayer.Dispose()
            _RxBusinessLayer = Nothing
        End If
    End Sub
    Private Sub frmRxProviderAssociation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            _FormLoading = True

            oToolTip.SetToolTip(btnAdd, "Add Provider")
            oToolTip.SetToolTip(btnRemove, "Remove Provider")

            _RxBusinessLayer = New RxBusinesslayer(_PatientID)

            FillSeniorDoctors()
            FillJuniorProviders()            
            FillProviderAssociation()
            FillRxAssociation()

            If _ProviderID = 0 Then
                pnlSetAsDefaultMain.Visible = False
            Else
                pnlSetAsDefaultMain.Visible = True
            End If

            _FormLoading = False


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region " Private Methods "

    Private Sub FillSeniorDoctors()
        Try
            Dim dtDoctors As DataTable
            dtDoctors = oProvider.GetAllSeniorProviders
            If dtDoctors IsNot Nothing Then
                trvSeniorProvider.Nodes.Clear()
                Dim oNode As myTreeNode
                For iRow As Integer = 0 To dtDoctors.Rows.Count - 1
                    oNode = New myTreeNode
                    oNode.Key = dtDoctors.Rows(iRow)("nProviderID")
                    oNode.Text = dtDoctors.Rows(iRow)("sProviderName") & " : " & dtDoctors.Rows(iRow)("sDEA")
                    oNode.TemplateResult = dtDoctors.Rows(iRow)("sProviderName")
                    oNode.Tag = dtDoctors.Rows(iRow)("sDEA")
                    trvSeniorProvider.Nodes.Add(oNode)
                Next
                trvSeniorProvider.Sort()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillJuniorProviders()
        Try
            Dim dtDoctors As DataTable
            dtDoctors = oProvider.GetAllJuniorProviders
            If dtDoctors IsNot Nothing Then
                trvAssociation.Nodes.Clear()
                Dim oNode As myTreeNode
                For iRow As Integer = 0 To dtDoctors.Rows.Count - 1

                    If _ProviderID <> 0 And _ProviderID <> dtDoctors.Rows(iRow)("nProviderID") Then Continue For

                    oNode = New myTreeNode
                    oNode.Key = dtDoctors.Rows(iRow)("nProviderID")
                    oNode.Text = dtDoctors.Rows(iRow)("sProviderName") & " : " & dtDoctors.Rows(iRow)("sDEA")
                    oNode.TemplateResult = dtDoctors.Rows(iRow)("sProviderName")
                    oNode.Tag = dtDoctors.Rows(iRow)("sDEA")
                    trvAssociation.Nodes.Add(oNode)
                Next
                trvAssociation.Sort()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillProviderAssociation()
        Try
            Dim dtProvider As DataTable
            If gblnMultipleSupervisorsforPaperRx = False Then
                trvAssociation.CheckBoxes = False
            End If
            For Each oJrProvider As myTreeNode In trvAssociation.Nodes
                dtProvider = oProvider.GetSeniorProviders(oJrProvider.Key)
                If dtProvider IsNot Nothing Then
                    Dim oNode As myTreeNode
                    For iRow As Integer = 0 To dtProvider.Rows.Count - 1
                        oNode = New myTreeNode
                        oNode.Key = dtProvider.Rows(iRow)("nProviderID")
                        oNode.Text = dtProvider.Rows(iRow)("sProviderName") & " : " & dtProvider.Rows(iRow)("sDEA")
                        oNode.TemplateResult = dtProvider.Rows(iRow)("sProviderName")
                        oNode.Tag = dtProvider.Rows(iRow)("sDEA")
                        oNode.ImageIndex = 0
                        oNode.SelectedImageIndex = 0
                        oJrProvider.Nodes.Add(oNode)
                    Next
                End If
            Next
            trvAssociation.ExpandAll()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillRxAssociation()
        Try
            If _TemporarySave Then

                If _dtProviderAssociation IsNot Nothing Then

                    For Each oJrProvider As myTreeNode In trvAssociation.Nodes
                        For i As Integer = 0 To Me.ProviderAssociation.Rows.Count - 1
                            For iNode As Integer = 0 To oJrProvider.Nodes.Count - 1
                                If Convert.ToInt64(ProviderAssociation.Rows(i)("nProviderID")) = CType(oJrProvider.Nodes(iNode), myTreeNode).Key Then
                                    oJrProvider.Nodes(iNode).Checked = True
                                    Exit For
                                End If
                            Next
                        Next
                    Next
                End If
            Else
                Dim dtAssociation As DataTable
                For Each oJrProvider As myTreeNode In trvAssociation.Nodes
                    If _ProviderID = 0 Then
                        '' IF FORM OPENED FROM DASHBOARD THEN GET COMMON JUNIOR PROVIDER ASSOCIATION ''
                        dtAssociation = RxBusinesslayer.GetRxProviderAssociation(oJrProvider.Key)
                    Else
                        '' IF FORM OPENED FROM PRESCRIPTION THEN GET ASSOCIATION FOR THIS PRESCRITION ''
                        '' IF NO RECORD PRESENT AGAINST THIS PRESCRIPTION THEN PULL COMMON ASSOCIATION ''
                        dtAssociation = RxBusinesslayer.GetRxProviderAssociation(_PatientID, _VisitID, _dtPrescriptionDate)
                        If dtAssociation.Rows.Count = 0 Then
                            dtAssociation = RxBusinesslayer.GetRxProviderAssociation(oJrProvider.Key)
                        End If
                    End If

                    If dtAssociation IsNot Nothing Then
                        Dim _NodeFound As Boolean
                        Dim oNode As myTreeNode
                        For iRow As Integer = 0 To dtAssociation.Rows.Count - 1
                            _NodeFound = False
                            For iNode As Integer = 0 To oJrProvider.Nodes.Count - 1
                                If dtAssociation.Rows(iRow)("nProviderID") = CType(oJrProvider.Nodes(iNode), myTreeNode).Key Then
                                    oJrProvider.Nodes(iNode).Checked = True
                                    _NodeFound = True
                                    Exit For
                                End If
                            Next

                            '' IF ANY PROVIDER HAS REMOVED FROM DEFAULT SETTING, THEN ADD PROVIDER NODE ''
                            If _NodeFound = False Then
                                oNode = New myTreeNode
                                oNode.Key = dtAssociation.Rows(iRow)("nProviderID")
                                oNode.Text = dtAssociation.Rows(iRow)("sProviderName") & " : " & dtAssociation.Rows(iRow)("sDEA")
                                oNode.TemplateResult = dtAssociation.Rows(iRow)("sProviderName")
                                oNode.Tag = dtAssociation.Rows(iRow)("sDEA")
                                oNode.ImageIndex = 0
                                oNode.SelectedImageIndex = 0
                                oNode.Checked = True
                                oJrProvider.Nodes.Add(oNode)
                            End If
                        Next
                    End If
                Next
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub On_RemoveProvider_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If trvAssociation.SelectedNode IsNot Nothing Then
                If trvAssociation.SelectedNode.Level = 1 Then
                    trvAssociation.Nodes.Remove(trvAssociation.SelectedNode)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub tlsProviderAssociation_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsProviderAssociation.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "OK"
                    If Validation() Then
                        SaveProviderAssociation()
                        SaveRxAssociation()
                        Me.DialogResult = Windows.Forms.DialogResult.OK
                        ' Me.Close()
                        gloWord.WordDialogBoxBackgroundCloser.Close(Me)
                    End If
                Case "Cancel"
                    Me.DialogResult = Windows.Forms.DialogResult.Cancel
                    '  Me.Close()
                    gloWord.WordDialogBoxBackgroundCloser.Close(Me)
            End Select
        Catch
        End Try
    End Sub

    Private Sub SaveRxAssociation()
        Try
            Dim dtProviderAssociation As DataTable
            For Each oJrProvider As myTreeNode In trvAssociation.Nodes

                dtProviderAssociation = New DataTable("ProviderAssociation")

                dtProviderAssociation.Columns.Add(New DataColumn("nProviderID", GetType(Int64)))
                dtProviderAssociation.Columns.Add(New DataColumn("sDescription", GetType(String)))
                dtProviderAssociation.Columns.Add(New DataColumn("sDEA", GetType(String)))

                If oJrProvider.Nodes.Count > 0 Then
                    For iNode As Integer = 0 To oJrProvider.Nodes.Count - 1
                        If oJrProvider.Nodes(iNode).Checked Then

                            Dim thisNode As myTreeNode = CType(oJrProvider.Nodes(iNode), myTreeNode)

                            Dim dRow As DataRow = dtProviderAssociation.NewRow()
                            dRow("nProviderID") = thisNode.Key 'PROVIDER ID
                            dRow("sDescription") = thisNode.TemplateResult.ToString  'PROVIDER NAME
                            dRow("sDEA") = thisNode.Tag 'PROVIDER DEA
                            dtProviderAssociation.Rows.Add(dRow)
                            dRow = Nothing
                            thisNode = Nothing
                            'oList = New myList

                            'oList.ID = thisNode.Key '' PROVIDER ID ''
                            'oList.Description = thisNode.TemplateResult.ToString   '' PROVIDER NAME ''
                            'oList.Code = thisNode.Tag '' PROVIDER DEA ''
                            'arrProvider.Add(oList)
                        End If
                    Next
                End If

                If _ProviderID = 0 Then
                    _RxBusinessLayer.SaveRxProviderAssociation(oJrProvider.Key, dtProviderAssociation)
                Else
                    If chkSetAsDefault.Checked Then
                        _RxBusinessLayer.SaveRxProviderAssociation(oJrProvider.Key, dtProviderAssociation)
                    End If

                    '' SAVE FOR PERTICULAR PRESCRIPTION ''
                    If _TemporarySave Then
                        '' RETURN ARRAYLIST ONLY ''
                        _dtProviderAssociation = dtProviderAssociation
                    Else
                        _RxBusinessLayer.SaveRxProviderAssociation(_PatientID, _VisitID, _dtPrescriptionDate, _ProviderID, dtProviderAssociation)
                    End If
                End If

            Next

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveProviderAssociation()
        Try
            Dim arrProvider As ArrayList
            For Each oJrProvider As myTreeNode In trvAssociation.Nodes
                '' SAVING PROVIDER ASSOCIATION ONE BY ONE FOR EACH JUNIOR PROVIDER ''
                arrProvider = New ArrayList
                If oJrProvider.Nodes.Count > 0 Then
                    For iNode As Integer = 0 To oJrProvider.Nodes.Count - 1
                        arrProvider.Add(CType(oJrProvider.Nodes(iNode), myTreeNode).Key) '' PROVIDER ID ''
                    Next
                    oProvider.SaveProviderAssociation(oJrProvider.Key, arrProvider)
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function Validation() As Boolean
        Try
            '' VALIDATION FOR ATLEAST ONE PROVIDER ''
            For Each oJrProvider As TreeNode In trvAssociation.Nodes
                ''Yatin 25/04/2012
                Dim jNode As myTreeNode = CType(oJrProvider, myTreeNode)
                Dim isBlockedProvider() As Boolean = oProvider.isBlockedProvider(0, jNode.Key)
                If Not isBlockedProvider(1) Then

                    If oJrProvider.Nodes.Count = 0 Then
                        If _ProviderID = 0 Then
                            MessageBox.Show("Please associate at least one provider for each junior provider.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("Please associate at least one provider.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        Return False
                    End If
                End If
            Next

            '' VALIDATION FOR CHECKED PROVIDER ''
            Dim _CheckeFound As Boolean
            For Each oJrProvider As TreeNode In trvAssociation.Nodes
                _CheckeFound = False
                ''Yatin 25/04/2012
                Dim jNode As myTreeNode = CType(oJrProvider, myTreeNode)
                Dim isBlockedProvider() As Boolean = oProvider.isBlockedProvider(0, jNode.Key)
                If Not isBlockedProvider(1) Then

                    For Each oNode As TreeNode In oJrProvider.Nodes

                        If oNode.Checked Then
                            _CheckeFound = True
                        End If

                    Next
                    If _CheckeFound = False Then
                        If _ProviderID = 0 Then
                            MessageBox.Show("Please select at least one provider for each junior provider.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show("Please select at least one provider.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                        Return False
                    End If
                End If
            Next

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

    End Function
#End Region

#Region " Tree View Events "
    Private Sub trvSeniorProvider_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvSeniorProvider.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Dim oNode As TreeNode = trvSeniorProvider.HitTest(e.X, e.Y).Node
                If oNode IsNot Nothing Then trvSeniorProvider.SelectedNode = oNode
            End If
        Catch
        End Try
    End Sub
    Private Sub trvAssociation_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvAssociation.AfterCheck
        Try
            If _FormLoading = False Then
                If e.Node.Level = 0 Then
                    Dim _CheckCount As Integer = 0

                    '' TAKE COUNT OF ALREADY CHECKED NODES ''
                    If e.Node.Checked Then
                        For Each oChild As TreeNode In e.Node.Nodes
                            If oChild.Checked Then
                                _CheckCount += 1
                            End If
                        Next
                    End If

                    _FormLoading = True
                    For Each oChild As TreeNode In e.Node.Nodes

                        If _CheckCount >= 3 Then
                            _FormLoading = False
                            Exit Sub
                        End If

                        If e.Node.Checked = True Then
                            '' IF NODE IS ALREADY CHECKED THEN SKIP IT ''
                            If oChild.Checked = False Then
                                oChild.Checked = e.Node.Checked
                                If e.Node.Checked Then
                                    _CheckCount += 1
                                End If
                            End If

                        Else
                            oChild.Checked = False
                        End If
                    Next
                    _FormLoading = False
                End If
            End If
        Catch
        End Try
    End Sub
    Private Sub trvAssociation_BeforeCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles trvAssociation.BeforeCheck
        Try
            If _FormLoading = False And e.Node.Checked = False Then

                If e.Node.Level = 1 Then
                    Dim _CheckCount As Integer = 0
                    For Each oNode As TreeNode In e.Node.Parent.Nodes
                        If oNode.Checked Then
                            _CheckCount += 1
                        End If
                    Next

                    If _CheckCount >= 3 Then
                        MessageBox.Show("There cannot be more than three providers selected for Prescription Provider Association.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        e.Cancel = True
                    End If
                End If

            End If
        Catch
        End Try
    End Sub
    Private Sub trvAssociation_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvAssociation.MouseDown
        Try
            If e.Button = Windows.Forms.MouseButtons.Right Then
                Try
                    If IsNothing(trvAssociation.ContextMenu) = False Then
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(trvAssociation.ContextMenu)
                        If (IsNothing(trvAssociation.ContextMenu.MenuItems) = False) Then
                            trvAssociation.ContextMenu.MenuItems.Clear()
                        End If
                        trvAssociation.ContextMenu.Dispose()
                        trvAssociation.ContextMenu = Nothing
                    End If
                Catch ex As Exception

                End Try
                trvAssociation.ContextMenu = Nothing
                Dim oNode As TreeNode = trvAssociation.HitTest(e.X, e.Y).Node
                If oNode IsNot Nothing Then trvAssociation.SelectedNode = oNode
                If oNode.Level = 1 Then
                    Dim oContext As New ContextMenu()
                    Dim oMenuItem As New MenuItem("Remove")
                    AddHandler oMenuItem.Click, AddressOf On_RemoveProvider_Click
                    oContext.MenuItems.Add(oMenuItem)
                    Try
                        If IsNothing(trvAssociation.ContextMenu) = False Then
                            gloGlobal.cEventHelper.RemoveAllEventHandlers(trvAssociation.ContextMenu)
                            If (IsNothing(trvAssociation.ContextMenu.MenuItems) = False) Then
                                trvAssociation.ContextMenu.MenuItems.Clear()
                            End If
                            trvAssociation.ContextMenu.Dispose()
                            trvAssociation.ContextMenu = Nothing
                        End If
                    Catch ex As Exception

                    End Try
                    trvAssociation.ContextMenu = oContext
                End If
            End If
        Catch
        End Try
    End Sub
    Private Sub trvSeniorProvider_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvSeniorProvider.NodeMouseDoubleClick
        Try
            If trvAssociation.SelectedNode Is Nothing Then
                Exit Sub
            End If

            If trvAssociation.SelectedNode.Level = 1 Then
                trvAssociation.SelectedNode = trvAssociation.SelectedNode.Parent
            End If

            Dim oNode As myTreeNode = CType(trvSeniorProvider.SelectedNode, myTreeNode)
            Dim jNode As myTreeNode = CType(trvAssociation.SelectedNode, myTreeNode)
            If oNode IsNot Nothing Then
                'Yatin 17/04/2012
                'Blocked Provider Association Case

                Dim isBlockedProvider() As Boolean = oProvider.isBlockedProvider(oNode.Key, jNode.Key)
                If isBlockedProvider(0) Then
                    MessageBox.Show("This senior provider is blocked and cannot be associated.", "Blocked Senior Provider", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ElseIf isBlockedProvider(1) Then
                    MessageBox.Show("This junior provider is blocked and cannot be associated.", "Blocked Junior Provider", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    For iNode As Integer = 0 To trvAssociation.SelectedNode.Nodes.Count - 1
                        '' FIND IS NODE ALREADY PRESENT ''
                        If CType(trvAssociation.SelectedNode.Nodes(iNode), myTreeNode).Key = CType(oNode, myTreeNode).Key Then
                            Exit Sub
                        End If
                    Next

                    Dim oNodeToAdd As New myTreeNode
                    oNodeToAdd.Key = oNode.Key '' ID ''
                    oNodeToAdd.TemplateResult = oNode.TemplateResult.ToString
                    oNodeToAdd.Text = oNode.Text '' NAME : DEA ''
                    oNodeToAdd.Tag = oNode.Tag '' DEA ''
                    oNodeToAdd.ImageIndex = 0
                    oNodeToAdd.SelectedImageIndex = 0
                    trvAssociation.SelectedNode.Nodes.Add(oNodeToAdd)
                End If
                'If Not isBlockedProvider(0) Then
                '    For iNode As Integer = 0 To trvAssociation.SelectedNode.Nodes.Count - 1
                '        '' FIND IS NODE ALREADY PRESENT ''
                '        If CType(trvAssociation.SelectedNode.Nodes(iNode), myTreeNode).Key = CType(oNode, myTreeNode).Key Then
                '            Exit Sub
                '        End If
                '    Next

                '    Dim oNodeToAdd As New myTreeNode
                '    oNodeToAdd.Key = oNode.Key '' ID ''
                '    oNodeToAdd.TemplateResult = oNode.TemplateResult.ToString
                '    oNodeToAdd.Text = oNode.Text '' NAME : DEA ''
                '    oNodeToAdd.Tag = oNode.Tag '' DEA ''
                '    oNodeToAdd.ImageIndex = 0
                '    oNodeToAdd.SelectedImageIndex = 0
                '    trvAssociation.SelectedNode.Nodes.Add(oNodeToAdd)
                'Else
                '    MessageBox.Show("This provider is blocked and cannot be added.", "Blocked Provider", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub trvAssociation_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvAssociation.NodeMouseDoubleClick
        On_RemoveProvider_Click(Nothing, Nothing)
    End Sub
#End Region

#Region " Navigation Buttons "

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        trvSeniorProvider_NodeMouseDoubleClick(Nothing, Nothing)
    End Sub
    Private Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        On_RemoveProvider_Click(Nothing, Nothing)
    End Sub

    Private Sub btnAdd_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.MouseHover
        btnAdd.BackgroundImage = Global.gloEMR.My.Resources.YellowRight24
    End Sub
    Private Sub btnAdd_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.MouseLeave
        btnAdd.BackgroundImage = Global.gloEMR.My.Resources.Right24
    End Sub
    Private Sub btnRemove_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.MouseHover
        btnRemove.BackgroundImage = Global.gloEMR.My.Resources.YellowLeft24
    End Sub
    Private Sub btnRemove_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.MouseLeave
        btnRemove.BackgroundImage = Global.gloEMR.My.Resources.Left24
    End Sub
#End Region

End Class