Public Class frmSelectProblem
    Public objclsgeneral As gloSnoMed.ClsGeneral
    Dim STR_ICD9DESC As String = String.Empty
    Private nodesubtypetext As String = ""
    Dim arrICD9 As New ArrayList
    Dim strDia, strRx As String
    Dim _ModuleName As String = ""
    Dim _blnnewProblem As Boolean = False
    Public _DialogResult As Boolean = False
    Dim _CurrentTime As Date
    Public gstrSMDBConnstr As String
    Public EMRConnString As String
    Dim _isFormLoading As Boolean = False

    Private Sub frmSelectProblem_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        For Each myForm As System.Windows.Forms.Form In System.Windows.Forms.Application.OpenForms
            If (myForm.TopMost) Then
                myForm.TopMost = False
            End If
        Next
        Me.TopMost = True
    End Sub

    Private Sub frmSelectProblem_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        Me.TopMost = False
    End Sub

    Private Sub frmSelectProblem_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim myScreenWidth As Integer
        Dim myScreenHeight As Integer

        _isFormLoading = True

        
        Try

            '17-Mar-14 Aniket: Resolving resolution issues
            myScreenWidth = System.Windows.SystemParameters.PrimaryScreenWidth
            myScreenHeight = System.Windows.SystemParameters.PrimaryScreenHeight


            If Me.Width > myScreenWidth Or Me.Height > myScreenHeight Then

                Me.MaximumSize = New System.Drawing.Size(myScreenWidth, myScreenHeight)
                Me.AutoScroll = True

                'Me.AutoSize = True
                'Me.AutoSizeMode = AutoSizeMode.GrowAndShrink

            End If


            txtSMSearch.Select()
            objclsgeneral = New gloSnoMed.ClsGeneral
            If gstrSMDBConnstr <> String.Empty Then
                objclsgeneral.IsConnect(gstrSMDBConnstr, EMRConnString)
            End If
            cmbSearchBy.Items.Add("Snomed Concept ID/Description")
            cmbSearchBy.Items.Add("ICD9 Code/Description")
            cmbSearchBy.Items.Add("ICD10 Code")
            cmbSearchBy.SelectedIndex = 0

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            _isFormLoading = False

        End Try

    End Sub

    Private Sub FillDetails()

        Dim dt As New DataTable
        Dim dsSnomed As New DataSet

        If (Not IsNothing(strConceptID)) And (Not IsNothing(txtSMSearch.Text.Trim)) Then
            If strConceptID <> "" And txtSMSearch.Text.Trim <> "" Then

                strConceptID = strConceptID
                dsSnomed = objclsgeneral.Fill_SnomedDetails(strConceptID, False, strConceptDesc)
                If IsNothing(dsSnomed) = False Then


                    objclsgeneral.Fill_snomedDescription(strConceptID, trvSnoMed, dsSnomed)
                    objclsgeneral.Fill_ICD9(strConceptID, txtSMSearch.Text.Trim, trICD9, dsSnomed)
                    objclsgeneral.Fill_ICD10(strConceptID, txtSMSearch.Text.Trim, trICD10, dsSnomed)

                    ''strDescriptionID = objclsgeneral.Fill_DescriptionID(oNode.Tag.ToString())
                    If IsNothing(dsSnomed) = False Then
                        If dsSnomed.Tables("SnomedCodes").Rows.Count > 0 Then
                            strDescriptionID = dsSnomed.Tables("SnomedCodes").Rows(0)("DESCRIPTIONID")
                            StrSnoMedID = dsSnomed.Tables("SnomedCodes").Rows(0)("SNOMEDID")
                        End If
                        If dsSnomed.Tables("IsDefinition").Rows.Count > 0 Then
                            strSelectedDescription = dsSnomed.Tables("IsDefinition").Rows(0)("FULLYSPECIFIEDNAME")
                        End If
                        If dsSnomed.Tables("RxNormNDC").Rows.Count > 0 Then
                            strNDCCode = dsSnomed.Tables("RxNormNDC").Rows(0)("NDCCode")
                            strRxNormCode = dsSnomed.Tables("RxNormNDC").Rows(0)("RxNorm")
                        Else
                            strNDCCode = ""
                            strRxNormCode = ""
                        End If
                    End If
                    strSelectedFindings = txtSMSearch.Text.Trim

                End If
                strSelectedConceptID = strConceptID
                strSelectedSnoMedID = StrSnoMedID
                strSelectedDescriptionID = strDescriptionID

                lblSnoMedID.Text = StrSnoMedID
                Dim ICD9Desc As String = ""
                If txtSMSearch.Text.Trim <> "" Then
                    ICD9Desc = objclsgeneral.Fill_ICD9Description(strConceptDesc.Trim)
                    lblConceptID.Text = strSelectedConceptID & " - " & ICD9Desc
                Else
                    lblConceptID.Text = strSelectedConceptID
                End If

                lblDescriptionID.Text = strSelectedDescriptionID

                STR_ICD9DESC = ""
                nodesubtypetext = ""

                nodesubtypetext = txtSMSearch.Text.Trim
                Dim trvnode As TreeNode
                Dim trvchildnode As TreeNode

                arrICD9.Clear()
                For Each trvnode In trICD9.Nodes
                    For Each trvchildnode In trvnode.Nodes
                        Dim lst As New myList
                        Dim codelen As Integer = trvchildnode.Text.IndexOf(":") - 1
                        If STR_ICD9DESC.Trim() = "" Then
                            STR_ICD9DESC = trvchildnode.Text
                        End If
                        Try

                            lst.Code = trvchildnode.Text.Substring(0, codelen)
                        Catch ex As Exception

                        End Try
                        lst.Description = trvchildnode.Text.Substring(codelen + 2, (trvchildnode.Text.Length - 1) - (codelen + 1))
                        lst.HistoryCategory = Nothing
                        lst.HistoryItem = Nothing
                        lst.Value = Nothing
                        lst.ParameterName = Nothing
                        lst.TemplateResult = Nothing
                        lst.ICD9Count = 0
                        lst.CPTCount = 0
                        lst.ModCount = 0

                        arrICD9.Add(lst)
                    Next
                Next
                strProblem = txtSMSearch.Text.Trim
            End If
        End If
        If Not IsNothing(dt) Then    'obj Disposed by mitesh
            dt.Dispose()
            dt = Nothing
        End If
    End Sub
    Public Property blnnewProblem() As Boolean
        Get
            Return _blnnewProblem
        End Get
        Set(ByVal value As Boolean)
            _blnnewProblem = value
        End Set
    End Property

    Public strProblem As String = ""
    Public strICD9 As String = ""
    Public strICD10 As String = ""
    Public strSelectedFindings As String = ""
    '  Public strSelectedSubtype As String = ""
    Public strSelectedConceptID As String = ""
    Public strSelectedSnoMedID As String = ""
    Public strSelectedDescriptionID As String = ""
    Public strSelectedDescription As String = ""
    Public strSelectedDefination As String = ""

    'Public strSubTypeConceptID As String = ""
    'Public strSubTypeSnoMedID As String = ""
    'Public strSubTypeDescriptionID As String = ""
    Public strConceptID As String = ""
    Public strDefination As String = ""
    Public strDescriptionID As String = ""
    Public StrSnoMedID As String = ""
    Public strNDCCode As String = ""
    Public strRxNormCode As String = ""
    Public strConceptDesc As String = ""

    Public strCodeSystem As String = "SNOMED"
    Public blnIsProblem As Boolean = False
    ''Event changed by Shweta 20100917 for MU
    ''text box changed to gloSearchTextBox
    Private Sub txtSMSearch_TextChanged() Handles txtSMSearch.SearchFired
        ''END-Event changed by Shweta 20100917
        Dim _Term As String
        Try

            If txtSMSearch.Text.Length > 1 Then
                Me.Cursor = Cursors.WaitCursor
                _Term = ""
                objclsgeneral.SearchSnomed(txtSMSearch.Text.Trim, False, trvFindings, cmbSearchBy.Text.Trim)
                'DescID = ""
                If strConceptID <> "" Then
                    _Term = objclsgeneral.Fill_Term(strDescriptionID, strConceptID)
                End If

                For Each onode As TreeNode In trvFindings.Nodes
                    If IsNothing(onode) = False Then
                        If onode.Tag = strConceptID Then
                            If onode.Tag = strConceptID And onode.Name.Trim = _Term.Trim Then
                                trvFindings.SelectedNode = onode
                                Exit For
                            End If
                        End If
                    End If

                Next
                Me.Cursor = Cursors.Default
            Else
                trvFindings.Nodes.Clear()
                trvSubtype.Nodes.Clear()
                trvSnoMed.Nodes.Clear()
                trICD9.Nodes.Clear()
                trICD10.Nodes.Clear()

                'Sanjog
                lblConceptID.Text = ""
                lblSnoMedID.Text = ""
                lblDescriptionID.Text = ""
                strSelectedSnoMedID = ""
                strSelectedDescriptionID = ""
                'Sanjog
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub trvFindings_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvFindings.AfterSelect
        If Timer1.Enabled = False Then
            Timer1.Stop()
            Timer1.Enabled = True
        End If
    End Sub

    Private Sub trvFindings_BeforeExpand(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles trvFindings.BeforeExpand
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim _strIcd9() As String = Nothing
            Dim _strIcd9Desc As String = ""
            Dim _strIcd9Code As String = ""


            If IsNothing(e.Node) = False Then
                '   trvFindings.Nodes.Remove(e.Node.Nodes(0))
                Dim eNode As New TreeNode
                eNode = e.Node
                Dim dsTreeview As New DataSet
                If Not IsNothing(eNode) Then
                    If IsNothing(e.Node.Parent) = False Then
                        If e.Node.Nodes(0).Tag = "TempNode999*" Then
                            trvFindings.Nodes.Remove(e.Node.Nodes(0))

                            dsTreeview = objclsgeneral.Fill_SubTypes(eNode.Parent.Tag.ToString(), False)
                            objclsgeneral.FillSubtypeHierarchy_New(eNode.Parent.Tag.ToString(), dsTreeview, eNode)


                        End If
                    Else
                        If e.Node.Nodes(0).Tag = "TempNode9999*" Then
                            If cmbSearchBy.Text = "ICD9 Code/Description" Then
                                _strIcd9Desc = objclsgeneral.Fill_ICD9(eNode.Text.Trim)

                                dsTreeview = objclsgeneral.Fill_SubTypes(eNode.Tag.ToString(), True, _strIcd9Desc.Trim, 1)
                            Else
                                dsTreeview = objclsgeneral.Fill_SubTypes(eNode.Tag.ToString(), True)
                            End If



                            objclsgeneral.FillParentNodes(trvFindings, eNode, dsTreeview)

                            strProblem = eNode.Name
                            strSelectedConceptID = eNode.Tag

                            If IsNothing(dsTreeview) = False Then
                                If dsTreeview.Tables("Parent").Rows.Count > 0 Then
                                    strDescriptionID = dsTreeview.Tables("Parent").Rows(0)("DESCRIPTIONID")
                                    StrSnoMedID = dsTreeview.Tables("Parent").Rows(0)("SNOMEDID")
                                End If
                                If dsTreeview.Tables("IsDefinition").Rows.Count > 0 Then
                                    strSelectedDescription = dsTreeview.Tables("IsDefinition").Rows(0)("FULLYSPECIFIEDNAME")
                                End If
                            End If
                            Dim ICD9Desc As String = ""
                            If eNode.Name.Trim <> "" Then
                                ICD9Desc = objclsgeneral.Fill_ICD9Description(eNode.Name.Trim)
                                lblConceptID.Text = eNode.Tag & " - " & ICD9Desc
                            Else
                                lblConceptID.Text = eNode.Tag
                            End If

                            lblSnoMedID.Text = strSelectedSnoMedID
                            lblDescriptionID.Text = strSelectedDescriptionID

                    End If

                    End If
                End If


                ' objclsgeneral.FillSubtypeHierarchy(eNode.Tag.ToString(), eNode.Text, trvSubtype)


                If Not IsNothing(eNode) Then   'obj Disposed by mitesh
                    eNode = Nothing
                End If
            End If
        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub trvFindings_BeforeSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles trvFindings.BeforeSelect
        _CurrentTime = DateTime.Now
        Timer1.Stop()
        Timer1.Interval = 700
        Timer1.Enabled = True
    End Sub
    Private Sub Timer1_Tick() Handles Timer1.Tick
        Me.Cursor = Cursors.WaitCursor
        Try

            If Me.Text.Trim <> "" Then

                If DateTime.Now.Subtract(_CurrentTime).Milliseconds > 100 Then
                    Timer1.Stop()
                    mnuFindings_Click(Nothing, Nothing)
                End If

            Else
                Timer1.Stop()
                mnuFindings_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub trvFindings_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvFindings.MouseClick
        'If e.Button = Windows.Forms.MouseButtons.Right Then
        '    With trvFindings
        '        Dim r As Integer = .HitTest(e.X, e.Y).Node.Index
        '        If r >= 0 Then
        '            trvFindings.SelectedNode = trvFindings.GetNodeAt(e.X, e.Y)
        '            trvFindings.ContextMenuStrip = cntFindings

        '        Else
        '            trvFindings.ContextMenu = Nothing
        '        End If
        '    End With
        'End If
    End Sub

    Private Sub trvFindings_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvFindings.MouseDoubleClick
        'Dim eNode As New TreeNode
        'eNode = trvFindings.SelectedNode
        'If Not IsNothing(eNode) Then
        '    objclsgeneral.FillSubtypeHierarchy(eNode.Tag.ToString(), eNode.Text, trvSubtype)

        '    strProblem = eNode.Text
        '    strSelectedConceptID = eNode.Tag
        '    strSelectedSnoMedID = objclsgeneral.Fill_SnoMedID(strSelectedConceptID)
        '    strSelectedDescriptionID = objclsgeneral.Fill_SnoMedID(strSelectedConceptID)
        '    strSelectedDescription = objclsgeneral.GetIsADefinition(eNode.Tag.ToString())
        '    lblConceptID.Text = eNode.Tag
        '    lblSnoMedID.Text = strSelectedSnoMedID
        '    lblDescriptionID.Text = strSelectedDescriptionID
        'End If
        'If Not IsNothing(eNode) Then   'obj Disposed by mitesh
        '    eNode = Nothing
        'End If
    End Sub

    'Private Sub trvFindings_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvFindings.NodeMouseClick
    '    If e.Button = Windows.Forms.MouseButtons.Right Then
    '        With trvFindings
    '            Dim r As Integer = .HitTest(e.X, e.Y).Node.Index
    '            If r >= 0 Then
    '                trvFindings.SelectedNode = trvFindings.GetNodeAt(e.X, e.Y)
    '                trvFindings.ContextMenuStrip = cntFindings

    '            Else
    '                trvFindings.ContextMenu = Nothing
    '            End If
    '        End With
    '    End If
    'End Sub

    'Private Sub trvFindings_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvFindings.NodeMouseDoubleClick
    '    objclsgeneral.FillSubtypeHierarchy(e.Node.Tag.ToString(), e.Node.Text, trvSubtype)
    '    If Not IsNothing(e.Node) Then
    '        strProblem = e.Node.Text
    '        strSelectedConceptID = e.Node.Tag
    '        strSelectedSnoMedID = objclsgeneral.Fill_SnoMedID(strSelectedConceptID)
    '        strSelectedDescriptionID = objclsgeneral.Fill_SnoMedID(strSelectedConceptID)
    '        strSelectedDescription = objclsgeneral.GetIsADefinition(e.Node.Tag.ToString())
    '        lblConceptID.Text = e.Node.Tag
    '        lblSnoMedID.Text = strSelectedSnoMedID
    '        lblDescriptionID.Text = strSelectedDescriptionID
    '    End If
    'End Sub

    Private Sub trvSubtype_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles trvSubtype.MouseDoubleClick
        'Dim eNode As New TreeNode
        'Dim dt As New DataTable
        'eNode = trvSubtype.SelectedNode
        'If IsNothing(eNode) = False Then


        '    objclsgeneral.Fill_SnomedDescription(eNode.Tag.ToString(), trvSnoMed)
        '    objclsgeneral.Fill_ICD9(eNode.Tag.ToString(), eNode.Text, trICD9)
        '    objclsgeneral.Fill_ICD10(eNode.Tag.ToString(), eNode.Text, trICD10)
        '    strConceptID = eNode.Tag
        '    strDescriptionID = objclsgeneral.Fill_DescriptionID(eNode.Tag)
        '    StrSnoMedID = objclsgeneral.Fill_SnoMedID(eNode.Tag)

        '    gRxNDBConnstr = String.Empty
        '    gRxNDBConnstr = GetHybridConnectionString(gRxNServerName, gRxNDatabaseName, gRxNIsSQLAUTHEN, gRxNUserID, gRxNPassWord)
        '    dt = objclsgeneral.Fill_RXNorm_NDC(eNode.Tag, gRxNDBConnstr, gRxNServerName, gRxNDatabaseName, gstrDatabaseName, gstrSQLServerName)
        '    If IsNothing(dt) = False Then


        '        If dt.Rows.Count > 0 Then
        '            strNDCCode = dt.Rows(0)(2).ToString()
        '            strRxNormCode = dt.Rows(0)(1).ToString()
        '        Else
        '            strNDCCode = ""
        '            strRxNormCode = ""
        '        End If
        '    End If
        '    STR_ICD9DESC = ""
        '    nodesubtypetext = ""

        '    nodesubtypetext = eNode.Text
        '    Dim trvnode As TreeNode
        '    Dim trvchildnode As TreeNode

        '    arrICD9.Clear()
        '    For Each trvnode In trICD9.Nodes
        '        For Each trvchildnode In trvnode.Nodes
        '            Dim lst As New myList
        '            Dim codelen As Integer = trvchildnode.Text.IndexOf(":") - 1
        '            If STR_ICD9DESC.Trim() = "" Then
        '                STR_ICD9DESC = trvchildnode.Text
        '            End If
        '            Try


        '                lst.Code = trvchildnode.Text.Substring(0, codelen)
        '            Catch ex As Exception

        '            End Try
        '            lst.Description = trvchildnode.Text.Substring(codelen + 2, (trvchildnode.Text.Length - 1) - (codelen + 1))
        '            lst.HistoryCategory = Nothing
        '            lst.HistoryItem = Nothing
        '            lst.Value = Nothing
        '            lst.ParameterName = Nothing
        '            lst.TemplateResult = Nothing
        '            lst.ICD9Count = 0
        '            lst.CPTCount = 0
        '            lst.ModCount = 0

        '            arrICD9.Add(lst)
        '        Next
        '    Next
        'End If
        'If Not IsNothing(eNode) Then
        '    strProblem = eNode.Text
        '    strSelectedConceptID = eNode.Tag
        '    strSelectedSnoMedID = objclsgeneral.Fill_SnoMedID(strSelectedConceptID)
        '    strSelectedDescriptionID = objclsgeneral.Fill_SnoMedID(strSelectedConceptID)
        '    strSelectedDescription = objclsgeneral.GetIsADefinition(eNode.Tag.ToString())
        '    lblConceptID.Text = eNode.Tag
        '    lblSnoMedID.Text = strSelectedSnoMedID
        '    lblDescriptionID.Text = strSelectedDescriptionID
        'End If
        'If Not IsNothing(dt) Then    'obj Disposed by mitesh
        '    dt.Dispose()
        '    dt = Nothing
        'End If
    End Sub


    Private Sub mnuFindings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuFindings.Click
        Dim oNode As TreeNode
        ' Dim dt As New DataTable
        Dim dsSnomed As DataSet = Nothing

        oNode = trvFindings.SelectedNode
        If Not IsNothing(oNode) Then
            strConceptID = oNode.Tag.ToString()
            Dim ICD9Description As String = ""
            'If cmbSearchBy.Text = "ICD9 Code/Description" Then
            '    ICD9Description = objclsgeneral.Fill_ICD9Description(oNode.Name.Trim)
            'Else
            '    ICD9Description = oNode.Text.Trim
            'End If
            If oNode.Name = "" Then
                oNode.Name = oNode.Text
            
            End If
            ICD9Description = oNode.Name.Trim
            dsSnomed = objclsgeneral.Fill_SnomedDetails(strConceptID, False, ICD9Description)
            If IsNothing(dsSnomed) = False Then


                objclsgeneral.Fill_snomedDescription(strConceptID, trvSnoMed, dsSnomed)
                objclsgeneral.Fill_ICD9(strConceptID, oNode.Name, trICD9, dsSnomed)
                objclsgeneral.Fill_ICD10(strConceptID, oNode.Name, trICD10, dsSnomed)

                ''strDescriptionID = objclsgeneral.Fill_DescriptionID(oNode.Tag.ToString())

                If dsSnomed.Tables("SnomedCodes").Rows.Count > 0 Then
                    strDescriptionID = dsSnomed.Tables("SnomedCodes").Rows(0)("DESCRIPTIONID")
                    StrSnoMedID = dsSnomed.Tables("SnomedCodes").Rows(0)("SNOMEDID")
                End If
                If dsSnomed.Tables("IsDefinition").Rows.Count > 0 Then
                    strSelectedDescription = dsSnomed.Tables("IsDefinition").Rows(0)("FULLYSPECIFIEDNAME")
                End If
                If dsSnomed.Tables("RxNormNDC").Rows.Count > 0 Then
                    strNDCCode = dsSnomed.Tables("RxNormNDC").Rows(0)("NDCCode")
                    strRxNormCode = dsSnomed.Tables("RxNormNDC").Rows(0)("RxNorm")
                Else
                    strNDCCode = ""
                    strRxNormCode = ""
                End If

            End If
            strSelectedConceptID = strConceptID
            strSelectedSnoMedID = StrSnoMedID
            strSelectedDescriptionID = strDescriptionID

            lblSnoMedID.Text = StrSnoMedID
            Dim ICD9Desc As String = ""

            If oNode.Name <> "" Then
                ICD9Desc = objclsgeneral.Fill_ICD9Description(oNode.Name)
                lblConceptID.Text = strSelectedConceptID & " - " & ICD9Desc
            Else
                lblConceptID.Text = strSelectedConceptID
            End If

            lblDescriptionID.Text = strSelectedDescriptionID

            STR_ICD9DESC = ""
            nodesubtypetext = ""

            nodesubtypetext = oNode.Name
            Dim trvnode As TreeNode
            Dim trvchildnode As TreeNode

            arrICD9.Clear()
            For Each trvnode In trICD9.Nodes
                For Each trvchildnode In trvnode.Nodes
                    Dim lst As New myList
                    Dim codelen As Integer = trvchildnode.Text.IndexOf(":") - 1
                    If STR_ICD9DESC.Trim() = "" Then
                        STR_ICD9DESC = trvchildnode.Text
                    End If
                    Try

                        lst.Code = trvchildnode.Text.Substring(0, codelen)
                    Catch ex As Exception

                    End Try
                    lst.Description = trvchildnode.Text.Substring(codelen + 2, (trvchildnode.Text.Length - 1) - (codelen + 1))
                    lst.HistoryCategory = Nothing
                    lst.HistoryItem = Nothing
                    lst.Value = Nothing
                    lst.ParameterName = Nothing
                    lst.TemplateResult = Nothing
                    lst.ICD9Count = 0
                    lst.CPTCount = 0
                    lst.ModCount = 0

                    arrICD9.Add(lst)
                Next
            Next
            strProblem = oNode.Name
        End If
        If IsNothing(dsSnomed) = False Then
            dsSnomed.Dispose()
        End If
        'If Not IsNothing(dt) Then    'obj Disposed by mitesh
        '    dt.Dispose()
        '    dt = Nothing
        'End If
    End Sub


    Private Sub tls_SM_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_SM.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Close"
                ' If Me.Text = "Edit Problem" Then
                'Me.DialogResult = Windows.Forms.DialogResult.No
                ' Dim strtxt As String = frmAddProblemList.Text
                ' _blnnewProblem = True
                ' Dim frmaddprb As frmAddProblemList
                'frmaddprb = frmAddProblemList.
                Me.DialogResult = Windows.Forms.DialogResult.Yes
                ' frmAddProblemList.blnclNewProblem = True
                _DialogResult = False

                ' frmAddProblemList.IsNewProblem = True
                Me.Close()
                ' ElseIf Me.Text = "Add Problem" Then
                'Me.DialogResult = Windows.Forms.DialogResult.Yes
                'Me.Close()
                ' Else
                ' Me.Close()
                ' End If

            Case "Save"
                'Dim oclsgenral As New gloSnoMed.ClsGeneral  'Not in use
                If Not IsNothing(trvFindings.SelectedNode) Then
                    Dim ICD9Desc As String = ""
                    ICD9Desc = objclsgeneral.Fill_ICD9Description(trvFindings.SelectedNode.Name)
                    strSelectedFindings = ICD9Desc
                    strProblem = ICD9Desc
                    'strFindingConceptID = trvFindings.SelectedNode.Tag
                    'strFindingSnoMedID = oclsgenral.Fill_SnoMedID(strFindingConceptID)
                    'strFindingDescriptionID = oclsgenral.Fill_DescriptionID(strFindingConceptID)
                End If

                'If Not IsNothing(trvSubtype.SelectedNode) Then
                '    strSelectedSubtype = trvSubtype.SelectedNode.Text
                '    'strSubTypeConceptID = trvSubtype.SelectedNode.Tag
                '    'strSubTypeSnoMedID = oclsgenral.Fill_SnoMedID(strFindingConceptID)
                '    'strSubTypeDescriptionID = oclsgenral.Fill_DescriptionID(strFindingConceptID)
                'End If
                strDefination = ""
                For k As Int16 = 0 To trvSnoMed.Nodes.Count - 1

                    If trvSnoMed.Nodes(k).Tag = "2" Then

                        If strDefination = "" Then
                            strDefination = trvSnoMed.Nodes(1).Text
                        Else
                            strDefination = strDefination & "|" & trvSnoMed.Nodes(1).Text
                        End If
                        For i As Int16 = 0 To trvSnoMed.Nodes(1).Nodes.Count - 1

                            strDefination = strDefination & "|" & trvSnoMed.Nodes(1).Nodes(i).Text
                            For j As Int16 = 0 To trvSnoMed.Nodes(1).Nodes(i).Nodes.Count - 1
                                strDefination = strDefination & ":" & trvSnoMed.Nodes(1).Nodes(i).Nodes(j).Text
                            Next
                        Next
                        'strDefination = strProblem & "|" & strDefination
                        strSelectedDefination = strDefination
                        strSelectedDescription = strProblem
                    End If
                Next

                If (strSelectedFindings <> "") And strDefination <> "" Then
                    saveProblem()
                    Me.Close()
                Else
                    'Sanjog -Added new message on 20110413 for this form in all module
                    'If _ModuleName = "" Then
                    '    MessageBox.Show("Please select Problem", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Else
                    'If _ModuleName = "Immunization" Then
                    '    MessageBox.Show("Please select Immunization", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'ElseIf _ModuleName = "Problem List" Then
                    '    MessageBox.Show("Please select Problem", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'ElseIf _ModuleName = "History" Then
                    'MessageBox.Show("Please select a SNOMED Concept before clicking ‘Save & Close’. When a SNOMED Concept" & vbCrLf & "       is selected, the description will show in the center panel of the selection window." & vbCrLf & "                             To exit the screen without saving, click ‘Close’", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    MessageBox.Show("Please select a SNOMED Concept before clicking ‘Save & Close’. When a SNOMED Concept is selected, the description will show in the center panel of the selection window." & vbCrLf & "To exit the screen without saving, click ‘Close’", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'End If
                    'End If
                    'Sanjog -Added ne w message on 20110413 for this form in all module
                    txtSMSearch.Focus()
                    Exit Sub

                End If
                _DialogResult = True
                'If Not IsNothing(oclsgenral) Then    'obj Disposed by mitesh
                '    oclsgenral.Dispose()
                '    oclsgenral = Nothing
                'End If
        End Select
    End Sub

    Private Sub saveProblem()

        For Each oParentNode As TreeNode In trICD10.Nodes
            '  For Each oChildNode As TreeNode In oParentNode.Nodes
            strICD10 = oParentNode.Text
            'Next
        Next

        For Each oParentNode As TreeNode In trICD9.Nodes
            '  For Each oChildNode As TreeNode In oParentNode.Nodes
            strICD9 = oParentNode.Text
            'Next
        Next
        

    End Sub

    Private Sub trvSnoMed_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvSnoMed.AfterSelect

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal ModuleName As String, ByVal SnomedConString As String, ByVal _EMRConnString As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        gstrSMDBConnstr = SnomedConString
        ' Add any initialization after the InitializeComponent() call.
        Me.Text = ModuleName
        _ModuleName = ModuleName
        EMRConnString = _EMRConnString


    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtSMSearch.Text = ""
        tlAddFields.SetToolTip(btnClear, "Clear")
    End Sub

    Private Sub cmbSearchBy_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSearchBy.SelectedIndexChanged
        trvFindings.Nodes.Clear()
        trvSubtype.Nodes.Clear()
        trvSnoMed.Nodes.Clear()
        trICD9.Nodes.Clear()
        trICD10.Nodes.Clear()

        'Sanjog
        lblConceptID.Text = ""
        lblSnoMedID.Text = ""
        lblDescriptionID.Text = ""
        strSelectedSnoMedID = ""
        strSelectedDescriptionID = ""
        If _isFormLoading = False Then
            If gloSettings.gloEMRSettings._gblnResetSearchTextBox = True Then
                txtSMSearch.Text = ""
            Else
                txtSMSearch_TextChanged()
            End If
        Else
            txtSMSearch_TextChanged()
        End If

    End Sub
End Class