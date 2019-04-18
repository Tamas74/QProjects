Public Class frmToolCustomize

#Region " Private Variables "
    Private _UserID As Int64
    Private _ModuleName As String
    Private _ToolStrip As ToolStrip
    Private _SelectedButtons As New ArrayList()
    Private _ToolSeperator As ToolStripSeparator
    Private _DefaultToolStrip As ArrayList
    Private _ButtonsToHide As New ArrayList
    Private oToolTip As ToolTip
    Private _ShowSeparators As Boolean
    Private _ToolStripHeight As Integer
    Private _ButtonNameType As gloToolStrip.enumButtonNameType
    Private _FinishTemplate As Boolean
#End Region

#Region " Public Properties "
    Public Property ModuleName() As String
        Get
            Return _ModuleName
        End Get
        Set(ByVal value As String)
            _ModuleName = value
        End Set
    End Property
    Public Property FinishTemplate() As Boolean
        Get
            Return _FinishTemplate
        End Get
        Set(ByVal value As Boolean)
            _FinishTemplate = value
        End Set
    End Property

    Public Property UserID() As Int64
        Get
            Return _UserID
        End Get
        Set(ByVal value As Int64)
            _UserID = value
        End Set
    End Property

    Public Property ShowSeparator() As Boolean
        Get
            Return _ShowSeparators
        End Get
        Set(ByVal value As Boolean)
            _ShowSeparators = value
        End Set
    End Property

    Public Property ToolStripHeight() As Integer
        Get
            Return _ToolStripHeight
        End Get
        Set(ByVal value As Integer)
            _ToolStripHeight = value
        End Set
    End Property

    Public Property ButtonNameType() As gloToolStrip.enumButtonNameType
        Get
            Return _ButtonNameType
        End Get
        Set(ByVal value As gloToolStrip.enumButtonNameType)
            _ButtonNameType = value
        End Set
    End Property

    Public Property ButtonsToHide() As ArrayList
        Get
            Return _ButtonsToHide
        End Get
        Set(ByVal value As ArrayList)
            _ButtonsToHide = value
        End Set
    End Property

    Public ReadOnly Property SelectedButtons() As ArrayList
        Get
            Return _SelectedButtons
        End Get
    End Property
#End Region

#Region " Constructor Distructor "
    Public Sub New(ByVal oToolStrip As ToolStrip, ByVal arrDefaultToolStrip As ArrayList)
        InitializeComponent()
        _ToolStrip = oToolStrip
        _DefaultToolStrip = arrDefaultToolStrip
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
#End Region

#Region " Enumerations "
    Public Enum enumToolStripButtons
        'Close = 0
        'SaveClose = 1
        'Save = 2
        'Finish = 3
        DxCPT = 0
        SmartDiagnosis = 1
        SmartTreatment = 2
        Prescription = 3
        Order = 4
        Labs = 5
        History = 6
        ROS = 7
        Medication = 8
        Vitals = 9
        ProblemList = 10
        FlowSheet = 11
        ChiefComplaint = 12
        Tasks = 13
        PatientEducation = 14
        Guideline = 15
        DeceaseManagement = 16
        UnDo = 17
        ReDo = 18
        Print = 19
        FAX = 20
        InsertSignature = 21
        CaptureSignature = 22
        InsertFile = 23
        ScanImage = 24
        ViewDocuments = 25
        RefferalLetter = 26
        PatientDemographics = 27
        CoSignature = 28
    End Enum
#End Region

#Region " Form Load Close Events "
    Private Sub frmToolButtonSelection_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        oToolTip.RemoveAll()
        oToolTip.Dispose()
        oToolTip = Nothing
    End Sub

    Private Sub frmToolButtonSelection_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            '' SET TOOL TIPS ''
            oToolTip = New ToolTip
            oToolTip.SetToolTip(btnAdd, "Add Button")
            oToolTip.SetToolTip(btnRemove, "Remove Button")
            oToolTip.SetToolTip(btnUp, "Move Up")
            oToolTip.SetToolTip(btnDown, "Move Down")

            FillToolButtons()
            Me.Activate()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, sMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region " Private Methods "
    Private Sub FillToolButtons()
        Try
            trvButtons.Nodes.Clear()
            trvSelectedButtons.Nodes.Clear()

            If _ButtonsToHide Is Nothing Then
                _ButtonsToHide = New ArrayList
            End If

            '' INSERT SEPERATOR IN TREE '' 
            If _ShowSeparators Then
                trvButtons.Nodes.Add("Separator")
            End If

            '' INITIALIZE IMAGELIST TO TREEVIEW ''
            If _ToolStripHeight > 30 Then
                trvButtons.ImageList = img32
                trvSelectedButtons.ImageList = img32
                trvButtons.ItemHeight = 35
                trvSelectedButtons.ItemHeight = 35
            Else
                trvButtons.ImageList = img16
                trvSelectedButtons.ImageList = img16
                trvButtons.ItemHeight = 20
                trvSelectedButtons.ItemHeight = 20
            End If

            For i As Integer = 0 To _ToolStrip.Items.Count - 1

                If IsNothing(_ToolStrip.Items(i).Tag) = False Then
                    If CType(_ToolStrip.Items(i).Tag, String).Trim() <> "" And _ToolStrip.Items(i).Text.Trim <> "Mic" And _ButtonsToHide.Contains(_ToolStrip.Items(i).Name) = False Then
                        InsertNode(i)
                    End If

                Else
                    If _ShowSeparators And (_ToolStrip.Items(i).Visible = True Or _ToolStrip.Items(i).IsOnOverflow = True) Then
                        trvSelectedButtons.Nodes.Add("Separator")
                    End If
                End If

            Next

        Catch ex As Exception
            MessageBox.Show(ex.ToString, sMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub InsertNode(ByVal btnIndex As Integer)
        Try
            Dim oNode As New TreeNode

            '' SET NODE TEXT AS BUTTON TAG/TOOLTIP ''
            If _ButtonNameType = gloToolStrip.enumButtonNameType.ShowToolTipText Then
                oNode.Text = _ToolStrip.Items(btnIndex).ToolTipText.Trim
            ElseIf _ButtonNameType = gloToolStrip.enumButtonNameType.ShowTagText Then
                oNode.Text = _ToolStrip.Items(btnIndex).Tag.ToString.Trim
            End If            
            '' COPY IMAGE TO PERTICULAR IMAGELIST ''
            If _ToolStrip.Items(btnIndex).Image IsNot Nothing Then
                If _ToolStripHeight > 30 Then
                    img32.Images.Add(_ToolStrip.Items(btnIndex).Image)
                    oNode.ImageIndex = img32.Images.Count - 1
                    oNode.SelectedImageIndex = img32.Images.Count - 1
                Else
                    img16.Images.Add(_ToolStrip.Items(btnIndex).Image)
                    oNode.ImageIndex = img16.Images.Count - 1
                    oNode.SelectedImageIndex = img16.Images.Count - 1
                End If
            Else
                oNode.ImageIndex = 1
                oNode.SelectedImageIndex = 1
            End If

            If _ToolStrip.Items(btnIndex).Visible = True Or _ToolStrip.Items(btnIndex).IsOnOverflow = True Then
                trvSelectedButtons.Nodes.Add(oNode)                
            Else
                trvButtons.Nodes.Add(oNode)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub InsertNodeAfterReset(ByVal sButtonName As String, ByVal sButtonFlag As String)
        Try
            Dim _FoundIndex As Integer

            '' SEARCH FOR PERTICULAR TOOL BUTTON
            For i As Integer = 0 To _ToolStrip.Items.Count - 1

                If IsNothing(_ToolStrip.Items(i).Tag) = False Then
                    If sButtonName = _ToolStrip.Items(i).Name.Trim() Then
                        _FoundIndex = i
                        Exit For
                    End If
                End If

            Next
            '' SEARCH END ''

            Dim oNode As New TreeNode

            '' SET NODE TEXT AS BUTTON NAME ''  
            If _ButtonNameType = gloToolStrip.enumButtonNameType.ShowTagText Then
                oNode.Text = _ToolStrip.Items(_FoundIndex).Tag.Trim()
            ElseIf _ButtonNameType = gloToolStrip.enumButtonNameType.ShowToolTipText Then
                oNode.Text = _ToolStrip.Items(_FoundIndex).ToolTipText.Trim()
            End If

            '' COPY IMAGE TO PERTICULAR IMAGELIST ''
            If _ToolStrip.Items(_FoundIndex).Image IsNot Nothing Then
                If _ToolStripHeight > 30 Then
                    img32.Images.Add(_ToolStrip.Items(_FoundIndex).Image)
                    oNode.ImageIndex = img32.Images.Count - 1
                    oNode.SelectedImageIndex = img32.Images.Count - 1
                Else
                    img16.Images.Add(_ToolStrip.Items(_FoundIndex).Image)
                    oNode.ImageIndex = img16.Images.Count - 1
                    oNode.SelectedImageIndex = img16.Images.Count - 1
                End If
            Else
                oNode.ImageIndex = 1
                oNode.SelectedImageIndex = 1
            End If

            '' INSERT NODE ''
            If sButtonFlag = "Visible" Then
                trvSelectedButtons.Nodes.Add(oNode)
            Else
                trvButtons.Nodes.Add(oNode)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub RemoveRepeatSeperator()
        For iNode As Integer = trvSelectedButtons.Nodes.Count - 1 To 1 Step -1
            If trvSelectedButtons.Nodes(iNode).Text = "Separator" And trvSelectedButtons.Nodes(iNode).PrevNode.Text = "Separator" Then
                trvSelectedButtons.Nodes.RemoveAt(iNode)
            End If
        Next
    End Sub

    Private Sub SaveToolButtonSelection()
        Try
            _SelectedButtons.Clear()
            '' GLO2011-0013707 : 0595 - message errors
            '' If Buttons not found in the selected buttons list then add to selected buttons array.
            '' ---- Strart ----
            If _ModuleName = "gloWordToolStrip_Messages" Then
                If trvSelectedButtons.Nodes.ContainsKey(_ToolStrip.Items("Close").Name) = False Then
                    _SelectedButtons.Add(_ToolStrip.Items("Close").Tag.ToString.Trim)
                End If
                If trvSelectedButtons.Nodes.ContainsKey(_ToolStrip.Items("Save & Close").Name) = False Then
                    _SelectedButtons.Add(_ToolStrip.Items("Save & Close").Tag.ToString.Trim)
                End If
                If trvSelectedButtons.Nodes.ContainsKey(_ToolStrip.Items("Save").Name) = False Then
                    _SelectedButtons.Add(_ToolStrip.Items("Save").Tag.ToString.Trim)
                End If                
                If trvSelectedButtons.Nodes.ContainsKey(_ToolStrip.Items("Save & Finish").Name) = False Then
                    _SelectedButtons.Add(_ToolStrip.Items("Save & Finish").Tag.ToString.Trim)
                End If                                
                If trvSelectedButtons.Nodes(0).Text <> "Separator" Then
                    _SelectedButtons.Add("|")
                End If
            End If



            '' ---- End ----
            For iNode As Integer = 0 To trvSelectedButtons.Nodes.Count - 1
                If trvSelectedButtons.Nodes(iNode).Text = "Separator" Then
                    _SelectedButtons.Add("|")
                Else
                    _SelectedButtons.Add(trvSelectedButtons.Nodes(iNode).Text)
                End If
            Next


            If _ModuleName = "gloWordToolStrip_LabOrder" And _FinishTemplate = False Then

                'If _FinishTemplate = False Then


                'If trvSelectedButtons.Nodes.ContainsKey(_ToolStrip.Items("Finish").Name) = False Then
                '    _SelectedButtons.Add(_ToolStrip.Items("Finish").Tag.ToString.Trim)
                'End If
                If trvSelectedButtons.Nodes.ContainsKey(_ToolStrip.Items("Save").Name) = False Then
                    _SelectedButtons.Add(_ToolStrip.Items("Save").Tag.ToString.Trim)
                End If
                If trvSelectedButtons.Nodes.ContainsKey(_ToolStrip.Items("Save & Close").Name) = False Then
                    _SelectedButtons.Add(_ToolStrip.Items("Save & Close").Tag.ToString.Trim)
                End If

                'If trvSelectedButtons.Nodes.ContainsKey(_ToolStrip.Items("Print").Name) = False Then
                '    _SelectedButtons.Add(_ToolStrip.Items("Print").Tag.ToString.Trim)
                'End If

                'If trvSelectedButtons.Nodes.ContainsKey(_ToolStrip.Items("Fax").Name) = False Then
                '    _SelectedButtons.Add(_ToolStrip.Items("Fax").Tag.ToString.Trim)
                'End If

                'If trvSelectedButtons.Nodes.ContainsKey(_ToolStrip.Items("Insert Sign").Name) = False Then
                '    _SelectedButtons.Add(_ToolStrip.Items("Insert Sign").Tag.ToString.Trim)
                'End If
                'If trvSelectedButtons.Nodes.ContainsKey(_ToolStrip.Items("Undo").Name) = False Then
                '    _SelectedButtons.Add(_ToolStrip.Items("Undo").Tag.ToString.Trim)
                'End If
                'If trvSelectedButtons.Nodes.ContainsKey(_ToolStrip.Items("Redo").Name) = False Then
                '    _SelectedButtons.Add(_ToolStrip.Items("Redo").Tag.ToString.Trim)
                'End If

                'If trvSelectedButtons.Nodes.ContainsKey(_ToolStrip.Items("Insert File").Name) = False Then
                '    _SelectedButtons.Add(_ToolStrip.Items("Insert File").Tag.ToString.Trim)
                'End If

                'If trvSelectedButtons.Nodes.ContainsKey(_ToolStrip.Items("Scan Documents").Name) = False Then
                '    _SelectedButtons.Add(_ToolStrip.Items("Scan Documents").Tag.ToString.Trim)
                'End If
                'If trvSelectedButtons.Nodes.ContainsKey(_ToolStrip.Items("tblbtn_StrikeThrough").Name) = False Then
                '    _SelectedButtons.Add(_ToolStrip.Items("tblbtn_StrikeThrough").Tag.ToString.Trim)
                'End If
                If trvSelectedButtons.Nodes.ContainsKey(_ToolStrip.Items("Close").Name) = False Then
                    _SelectedButtons.Add(_ToolStrip.Items("Close").Tag.ToString.Trim)
                End If
                'If trvSelectedButtons.Nodes.ContainsKey(_ToolStrip.Items("SecureMsg").Name) = False Then
                '    _SelectedButtons.Add(_ToolStrip.Items("SecureMsg").Tag.ToString.Trim)
                'End If
                'If trvSelectedButtons.Nodes.ContainsKey(_ToolStrip.Items("Insert Associated Provider Signature").Name) = False Then
                '    _SelectedButtons.Add(_ToolStrip.Items("Insert Associated Provider Signature").Tag.ToString.Trim)
                'End If
            End If




            'If trvSelectedButtons.Nodes.ContainsKey(_ToolStrip.Items("finish").Name) = False Then
            '    _SelectedButtons.Add(_ToolStrip.Items("finish").Tag.ToString.Trim)
            'End If
            'End If

            'If trvSelectedButtons.Nodes(0).Text <> "Separator" Then
            '    _SelectedButtons.Add("|")
            'End If
            'End If


            'For iNode As Integer = 0 To trvSelectedButtons.Nodes.Count - 1
            '    If trvSelectedButtons.Nodes(iNode).Text = "Separator" Then
            '        _SelectedButtons.Add("|")
            '    Else
            '        _SelectedButtons.Add(trvSelectedButtons.Nodes(iNode).Text)
            '    End If
            'Next

            '' ---- End ----


            Dim oToolStrip As New clsToolStrip
            oToolStrip.SaveButtons(_UserID, _ModuleName, _SelectedButtons)
            oToolStrip = Nothing

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, sMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ResetSettings()
        Try
            trvButtons.Nodes.Clear()
            trvSelectedButtons.Nodes.Clear()

            trvButtons.BeginUpdate()
            trvSelectedButtons.BeginUpdate()

            If IsNothing(_DefaultToolStrip) Then
                Exit Sub
            ElseIf _DefaultToolStrip.Count = 0 Then
                Exit Sub
            End If

            '' INSERT SEPERATOR IN TREE '' 
            If _ShowSeparators Then
                trvButtons.Nodes.Add("Separator")
            End If
            '' ''

            '' CLEAN PREVIOUS IMAGES FROM IMAGELIST ''
            If _ToolStripHeight > 30 Then
                For i As Integer = img32.Images.Count - 1 To 2 Step -1
                    img32.Images.RemoveAt(i)
                Next
            Else
                For i As Integer = img16.Images.Count - 1 To 2 Step -1
                    img16.Images.RemoveAt(i)
                Next
            End If

            '' FILLING TREE WITH DEFAULT SETTINGS ''
            Dim strButton() As String
            Dim _ButtonName As String = ""
            Dim _ButtonFlag As String = ""

            For i As Integer = 0 To _DefaultToolStrip.Count - 1

                '' SPLIT STRING ''
                strButton = Split(_DefaultToolStrip(i), ".")
                _ButtonName = strButton(0)
                If strButton.Length > 1 Then
                    _ButtonFlag = strButton(1)
                End If

                If _ButtonName = "|" Then
                    If _ShowSeparators Then
                        trvSelectedButtons.Nodes.Add("Separator")
                    End If
                Else
                    If _ButtonsToHide.Contains(_ButtonName) = False Then
                        InsertNodeAfterReset(_ButtonName, _ButtonFlag)
                    End If
                End If

            Next
        Catch ex As Exception
            MessageBox.Show(ex.ToString, sMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            trvButtons.EndUpdate()
            trvSelectedButtons.EndUpdate()
        End Try
    End Sub

    Private Sub tls_ToolButton_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_ToolButton.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "OK"
                    SaveToolButtonSelection()

                Case "Cancel"
                    Me.Close()

                Case "SelectAll"
                    'For i As Integer = 0 To chkToolButtons.Items.Count - 1
                    '    chkToolButtons.SetItemChecked(i, True)
                    'Next
                    ts_btnSelectAll.Visible = False
                    ts_btnClearAll.Visible = True
                Case "ClearAll"
                    'For i As Integer = 0 To chkToolButtons.Items.Count - 1
                    '    chkToolButtons.SetItemChecked(i, False)
                    'Next
                    ts_btnClearAll.Visible = False
                    ts_btnSelectAll.Visible = True
                Case "Reset"
                    ResetSettings()
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.Message, sMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub trvButtons_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvButtons.NodeMouseDoubleClick
        Try
            If IsNothing(e.Node) = False Then
                btnAdd_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, sMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvSelectedButtons_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvSelectedButtons.NodeMouseDoubleClick
        Try
            If IsNothing(e.Node) = False Then
                btnRemove_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, sMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region " Navigation Buttons and Events "

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        trvButtons.BeginUpdate()
        trvSelectedButtons.BeginUpdate()
        Try
            Dim oNode As TreeNode
            If IsNothing(trvButtons.SelectedNode) = False Then
                oNode = trvButtons.SelectedNode.Clone
                If oNode.Text <> "Separator" Then
                    trvButtons.SelectedNode.Remove()
                End If

                '' ADD / INSERT NODE ''
                If IsNothing(trvSelectedButtons.SelectedNode) = False Then
                    trvSelectedButtons.Nodes.Insert(trvSelectedButtons.SelectedNode.Index, oNode)
                Else
                    trvSelectedButtons.Nodes.Add(oNode)
                End If

                RemoveRepeatSeperator()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, sMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        trvButtons.EndUpdate()
        trvSelectedButtons.EndUpdate()
    End Sub

    Private Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        trvButtons.BeginUpdate()
        trvSelectedButtons.BeginUpdate()
        Try
            Dim oNode As TreeNode
            If IsNothing(trvSelectedButtons.SelectedNode) = False Then
                oNode = trvSelectedButtons.SelectedNode.Clone
                ''Close ,Save, Save and close condition added for bugid 83574 
                If oNode.Text <> "Separator" And oNode.Text <> "Close" And oNode.Text <> "Save" And oNode.Text <> "Save and Close" Then
                    trvButtons.Nodes.Add(oNode)
                    trvSelectedButtons.SelectedNode.Remove()
                End If

            End If
            RemoveRepeatSeperator()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, sMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        trvButtons.EndUpdate()
        trvSelectedButtons.EndUpdate()
    End Sub

    Private Sub btnUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.Click
        trvSelectedButtons.BeginUpdate()
        Try
            Dim oNode As TreeNode
            Dim prevIndex As Integer
            If IsNothing(trvSelectedButtons.SelectedNode) = False Then
                oNode = trvSelectedButtons.SelectedNode.Clone
                If IsNothing(oNode) = False Then
                    If trvSelectedButtons.SelectedNode.Index <> 0 Then
                        prevIndex = trvSelectedButtons.SelectedNode.PrevNode.Index
                        trvSelectedButtons.Nodes.Remove(trvSelectedButtons.SelectedNode)
                        trvSelectedButtons.Nodes.Insert(prevIndex, oNode)
                        trvSelectedButtons.SelectedNode = oNode
                    End If
                End If
            End If

            RemoveRepeatSeperator()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, sMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        trvSelectedButtons.EndUpdate()
        If trvSelectedButtons.SelectedNode IsNot Nothing Then
            trvSelectedButtons.SelectedNode.EnsureVisible()
        End If
    End Sub

    Private Sub btnDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.Click
        trvSelectedButtons.BeginUpdate()
        Try
            Dim oNode As TreeNode
            Dim nextIndex As Integer
            If IsNothing(trvSelectedButtons.SelectedNode) = False Then
                oNode = trvSelectedButtons.SelectedNode.Clone
                If IsNothing(oNode) = False Then
                    If trvSelectedButtons.SelectedNode.Index <> trvSelectedButtons.Nodes.Count - 1 Then
                        nextIndex = trvSelectedButtons.SelectedNode.NextNode.Index
                        trvSelectedButtons.Nodes.Remove(trvSelectedButtons.SelectedNode)
                        trvSelectedButtons.Nodes.Insert(nextIndex, oNode)
                        trvSelectedButtons.SelectedNode = oNode
                    End If
                End If
            End If

            RemoveRepeatSeperator()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, sMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        trvSelectedButtons.EndUpdate()
        If trvSelectedButtons.SelectedNode IsNot Nothing Then
            trvSelectedButtons.SelectedNode.EnsureVisible()
        End If
    End Sub

    Private Sub btnAdd_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.MouseHover
        btnAdd.BackgroundImage = Global.gloToolStrip.My.Resources.YellowRight24
    End Sub

    Private Sub btnAdd_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.MouseLeave
        btnAdd.BackgroundImage = Global.gloToolStrip.My.Resources.Right24
    End Sub

    Private Sub btnDown_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.MouseHover
        btnDown.BackgroundImage = Global.gloToolStrip.My.Resources.YellowDown24
    End Sub

    Private Sub btnDown_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.MouseLeave
        btnDown.BackgroundImage = Global.gloToolStrip.My.Resources.Down24
    End Sub

    Private Sub btnRemove_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.MouseHover
        btnRemove.BackgroundImage = Global.gloToolStrip.My.Resources.YellowLeft24
    End Sub

    Private Sub btnRemove_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.MouseLeave
        btnRemove.BackgroundImage = Global.gloToolStrip.My.Resources.Left24
    End Sub

    Private Sub btnUp_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.MouseHover
        btnUp.BackgroundImage = Global.gloToolStrip.My.Resources.YellowUp24
    End Sub

    Private Sub btnUp_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.MouseLeave
        btnUp.BackgroundImage = Global.gloToolStrip.My.Resources.Up24
    End Sub
#End Region

End Class