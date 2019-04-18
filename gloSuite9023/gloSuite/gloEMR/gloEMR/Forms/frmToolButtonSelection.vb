Public Class frmToolButtonSelection

    'Public _ToolButtons As System.Windows.Forms.ToolStripItemCollection
    Private _toolStrip As ToolStrip
    Private _moduleName As enumModuleName
    Private _selectedButtons As New ArrayList()
    Private _ToolSeperator As ToolStripSeparator
    Private _DefaultToolStrip As ArrayList
    Private _ButtonsToHide As New ArrayList

    Public Shared ToolButtons As Collection

    Private oToolTip As ToolTip
    Private blnIsOpenForTaskList As Boolean = False
    Private _DefaultTaskColumns As New ArrayList()
    Private _AllTaskColumns As New ArrayList()
    Private _AllVisibleTaskColumns As New ArrayList()
    Private _AllHideTaskColumns As New ArrayList()
    Private _TaskCurrentColumns As New ArrayList()
    Public _SelectedTaskColumn As New ArrayList()



    Public strFlexGroupCustomColumns As String = ""




    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _moduleName = enumModuleName.PatientExam
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal Tls As ToolStrip, ByVal ModuleName As enumModuleName, Optional ByVal arrDefaultToolStrip As ArrayList = Nothing)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _toolStrip = Tls
        _moduleName = ModuleName
        _DefaultToolStrip = arrDefaultToolStrip
        ' Add any initialization after the InitializeComponent() call.

        If ModuleName = enumModuleName.Dashboard Then
            If gblnAdvErxEnabled = False Then
                _ButtonsToHide.Add("RxElig")
            End If

            If gblnIsSecureMsgEnable = False Or gblnSecureUserrights = False Then
                _ButtonsToHide.Add("Inbox")
                _ButtonsToHide.Add("SecureMsg")
            End If
        ElseIf ModuleName = enumModuleName.PatientDetails Then
            If gblnAdvErxEnabled = False Then
                _ButtonsToHide.Add("Eligibility Information")
            End If
            If gblnIsSecureMsgEnable = False Or gblnSecureUserrights = False Then
                _ButtonsToHide.Add("Inbox")
                _ButtonsToHide.Add("SecureMsg")
            End If
        End If
        'Lines Addded by dipak 20091014 to make Button visible false Temporarily  
        _ButtonsToHide.Add("Billing")
        _ButtonsToHide.Add("Balance")
    End Sub
    ''Constructor added by Sandip Darade 20091105
    Public Sub New(ByVal Tls As ToolStrip, ByVal ModuleName As enumModuleName, ByVal arrDefaultToolStrip As ArrayList, ByVal arrButtonsToHide As ArrayList)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _toolStrip = Tls
        _moduleName = ModuleName
        _DefaultToolStrip = arrDefaultToolStrip
        ' Add any initialization after the InitializeComponent() call.
        _ButtonsToHide = arrButtonsToHide

        If ModuleName = enumModuleName.Dashboard Then

            '06-May-13 Aniket: Resolving Bug #50030:
            If gblnAdvErxEnabled = False Or gblnEligibilityUserRights = False Then
                _ButtonsToHide.Add("RxElig")
                'else condition added by pradeep 20100729
            Else
                _ButtonsToHide.Remove("RxElig")
            End If

            If gblnIsSecureMsgEnable = False Or gblnSecureUserrights = False Then
                _ButtonsToHide.Add("Inbox")
                _ButtonsToHide.Add("SecureMsg")
            Else
                _ButtonsToHide.Remove("Inbox")
                _ButtonsToHide.Add("SecureMsg")
            End If
        ElseIf ModuleName = enumModuleName.PatientDetails Then
            If gblnAdvErxEnabled = False Then
                _ButtonsToHide.Add("Eligibility Information")
            End If
            If gblnIsSecureMsgEnable = False Or gblnSecureUserrights = False Then
                _ButtonsToHide.Add("Inbox")
                _ButtonsToHide.Add("SecureMsg")
            End If
        End If
        'Lines Addded by dipak 20091014 to make Button visible false Temporarily  
        _ButtonsToHide.Add("Billing")
        _ButtonsToHide.Add("Balance")

    End Sub

    Public Sub New(ByVal arrCurrentColumn As ArrayList)
        InitializeComponent()
        blnIsOpenForTaskList = True
        _TaskCurrentColumns = arrCurrentColumn
        _DefaultTaskColumns.Add("Due Date")
        _DefaultTaskColumns.Add("Patient Name")
        _DefaultTaskColumns.Add("Subject")

        _AllTaskColumns.Add("Due Date")
        _AllTaskColumns.Add("Subject")
        _AllTaskColumns.Add("Patient Name")
        _AllTaskColumns.Add("Task Type")
        _AllTaskColumns.Add("Status")
        _AllTaskColumns.Add("Priority")
        _AllTaskColumns.Add("SSN")
        _AllTaskColumns.Add("Date Of Birth")
        _AllTaskColumns.Add("Resp")  ''added for task responsibility 
        trvButtons.Nodes.Clear()
        trvSelectedButtons.Nodes.Clear()

        trvButtons.ItemHeight = 20
        For Each taskCol1 As String In _AllTaskColumns
            If _TaskCurrentColumns.Contains(taskCol1) = False Then
                If taskCol1 = "Patient Name" Or taskCol1 = "Due Date" Or taskCol1 = "Subject" Then
                    trvSelectedButtons.Nodes.Add(taskCol1)
                    _AllVisibleTaskColumns.Add(taskCol1)
                Else
                    trvButtons.Nodes.Add(taskCol1)
                    _AllHideTaskColumns.Add(taskCol1)
                End If
            End If
        Next
        trvSelectedButtons.ItemHeight = 20

        For Each taskCol As String In _TaskCurrentColumns
            trvSelectedButtons.Nodes.Add(taskCol)
            _AllVisibleTaskColumns.Add(taskCol)
        Next

      

    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Public Enum enumModuleName
        Dashboard = 1
        PatientExam = 2
        PatientDetails = 3
    End Enum


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

    Private Sub frmToolButtonSelection_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        oToolTip.RemoveAll()
        oToolTip.Dispose()
        oToolTip = Nothing
    End Sub

    'Public Property ToolButtons() As Collection
    '    Get
    '        Return _ToolButtons
    '    End Get
    '    Set(ByVal value As Collection)
    '        _ToolButtons = value
    '    End Set
    'End Property

    Private Sub frmToolButtonSelection_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            oToolTip = New ToolTip
            If blnIsOpenForTaskList = False Then
                oToolTip.SetToolTip(btnAdd, "Add Button")
                oToolTip.SetToolTip(btnRemove, "Remove Button")
            Else
                oToolTip.SetToolTip(btnAdd, "Add Column")
                oToolTip.SetToolTip(btnRemove, "Remove Column")
            End If
           
            oToolTip.SetToolTip(btnUp, "Move Up")
            oToolTip.SetToolTip(btnDown, "Move Down")

            Call Fill_ToolButtons()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '' SUDHIR 20090421 '' OLD METHOD ''
    'Private Sub Fill_ToolButtons()
    '    Try


    '        Dim i, j As Integer
    '        'For i = 0 To _ToolButtons.Count - 1
    '        '    If _ToolButtons(i).Text.Trim <> "" Then
    '        '        Dim chkIndex As Integer
    '        '        chkIndex = chkToolButtons.Items.Add(_ToolButtons(i))
    '        '        chkToolButtons.Text = _ToolButtons(i).Text
    '        '        If _ToolButtons(i).Visible = True Or _ToolButtons.Item(i).IsOnOverflow = True Then
    '        '            chkToolButtons.SetItemChecked(chkIndex, True)

    '        '        Else
    '        '            chkToolButtons.SetItemChecked(chkIndex , False)
    '        '        End Ifs
    '        '    End If
    '        'Next


    '        '' Fill ToolBar Buttons [from enumToolStripButtons] to Checklist
    '        'For i = 0 To [Enum].GetValues(GetType(enumToolStripButtons)).Length - 1
    '        '    chkToolButtons.Items.Add([Enum].GetValues(GetType(enumToolStripButtons)).GetValue(i).ToString)
    '        'Next
    '        'With chkToolButtons.Items
    '        '    chkToolButtons.Items.Add("DxCPT") ''= 0
    '        '    .Add("Smart Diagnosis") '' = 1
    '        '    .Add("Smart Treatment") '' = 2
    '        '    .Add("Prescription") ''= 3
    '        '    .Add("Order") ''= 4
    '        '    .Add("Labs") '' = 5
    '        '    .Add("History") '' = 6
    '        '    .Add("ROS") '' = 7
    '        '    .Add("Medication") '' = 8
    '        '    .Add("Vitals") '' = 9
    '        '    .Add("Problem List") '' = 10
    '        '    .Add("Flow Sheet") '' = 11
    '        '    .Add("Chief Complaint") '' = 12
    '        '    .Add("Tasks") '' = 13
    '        '    .Add("Patient Education") '' = 14
    '        '    .Add("Guideline") '' = 15
    '        '    .Add("Disease Management") '' = 16
    '        '    .Add("Undo") '' = 17
    '        '    .Add("Redo") '' = 18
    '        '    .Add("Print") '' = 19
    '        '    .Add("FAX") '' = 20
    '        '    .Add("Insert Signature") '' = 21
    '        '    .Add("Capture Signature") '' = 22
    '        '    .Add("Insert File") '' = 23
    '        '    .Add("Scan Image") ''  = 24
    '        '    .Add("View Documents") ''  = 25
    '        '    .Add("RefferalLetter") ''  = 26
    '        '    .Add("Patient Demographics") ''  = 27
    '        '    If gblnCoSignFlag = True Then
    '        '        .Add("Co Signature") ''  = 28
    '        '    End If

    '        'End With

    '        ' '' Check The Items Tool Bar Buttons
    '        'If IsNothing(ToolButtons) = False Then
    '        '    With chkToolButtons
    '        '        For i = 1 To ToolButtons.Count
    '        '            For j = 0 To .Items.Count - 1
    '        '                If ToolButtons(i) = j Then
    '        '                    ''if User has have check the Button then Mark it as Checked & 
    '        '                    .SetItemChecked(j, True)
    '        '                    Exit For
    '        '                End If
    '        '            Next
    '        '        Next
    '        '    End With
    '        'End If

    '        '--------------Saket 20080528
    '        ' If _moduleName <> enumModuleName.PatientExam Then

    '        With chkToolButtons.Items
    '            .Clear()
    '            For i = 0 To _toolStrip.Items.Count - 1

    '                If _moduleName <> enumModuleName.PatientExam Then
    '                    If IsNothing(_toolStrip.Items(i).Tag) = False Then
    '                        If CType(_toolStrip.Items(i).Tag, String).Trim() <> "" And _toolStrip.Items(i).Tag.ToString <> "Microphone" Then
    '                            .Add(_toolStrip.Items(i).Tag.ToString())
    '                        End If
    '                    End If
    '                Else
    '                    If IsNothing(_toolStrip.Items(i).ToolTipText) = False Then
    '                        If CType(_toolStrip.Items(i).ToolTipText, String).Trim() <> "" And _toolStrip.Items(i).ToolTipText.Trim <> "Close" And _toolStrip.Items(i).ToolTipText.Trim <> "Save and Close" And _toolStrip.Items(i).ToolTipText.Trim <> "Save" And _toolStrip.Items(i).ToolTipText.Trim <> "Finish" Then
    '                            If _toolStrip.Items(i).ToolTipText.Trim = "Mic" Then
    '                                If gblnVoiceEnabled = True And gblnSpeakerExists = True Then
    '                                    .Add(_toolStrip.Items(i).ToolTipText.Trim)
    '                                End If
    '                            ElseIf _toolStrip.Items(i).ToolTipText.Trim <> "Addendum" Then
    '                                .Add(_toolStrip.Items(i).ToolTipText.Trim)
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            Next
    '        End With


    '        Dim obtnSelection As New ClsToolButtonSelection()
    '        Dim dt As DataTable
    '        dt = obtnSelection.GetGetButtonSelection(gnLoginID, _moduleName)
    '        Dim arrButtons As String()
    '        Dim strButtons As String
    '        If Not IsNothing(dt) Then
    '            If dt.Rows.Count > 0 Then
    '                strButtons = dt.Rows(0)("sButtons").ToString()
    '                arrButtons = strButtons.Split(",")
    '                _selectedButtons.Clear()
    '                For j = 0 To arrButtons.Length - 1
    '                    _selectedButtons.Add(arrButtons(j))
    '                Next
    '            Else
    '                For i = 0 To _toolStrip.Items.Count - 1
    '                    If (_toolStrip.Items(i).Visible = True Or _toolStrip.Items(i).IsOnOverflow = True) And IsNothing(_toolStrip.Items(i).Tag) = False Then
    '                        If _moduleName = enumModuleName.PatientExam Then
    '                            _selectedButtons.Add(_toolStrip.Items(i).ToolTipText.Trim)
    '                        Else
    '                            _selectedButtons.Add(_toolStrip.Items(i).Tag.ToString())
    '                        End If
    '                    End If
    '                Next
    '            End If
    '        Else
    '            For i = 0 To _toolStrip.Items.Count - 1
    '                If _toolStrip.Items(i).Visible = True And IsNothing(_toolStrip.Items(i).Tag) = False Then
    '                    _selectedButtons.Add(_toolStrip.Items(i).Tag.ToString.Trim)
    '                End If
    '            Next

    '        End If

    '        FillSelection()
    '        '----------------------------
    '        '    End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Sub

    Private Sub Fill_ToolButtons()


        Try

            If blnIsOpenForTaskList = False Then


                Dim i, j As Integer

                trvButtons.Nodes.Clear()
                trvSelectedButtons.Nodes.Clear()

                '' INSERT SEPERATOR IN TREE '' 
                If _moduleName <> enumModuleName.PatientDetails Then
                    trvButtons.Nodes.Add("Separator")
                End If
                '' ''

                '' INITIALIZE IMAGELIST TO TREEVIEW ''
                Select Case _moduleName
                    Case enumModuleName.Dashboard
                        trvButtons.ImageList = imgDashBoard
                        trvSelectedButtons.ImageList = imgDashBoard
                        trvButtons.ItemHeight = 35
                        trvSelectedButtons.ItemHeight = 35
                    Case enumModuleName.PatientDetails
                        trvButtons.ImageList = imgPatientDetails
                        trvSelectedButtons.ImageList = imgPatientDetails
                        trvButtons.ItemHeight = 20
                        trvSelectedButtons.ItemHeight = 20
                    Case enumModuleName.PatientExam
                        trvButtons.ImageList = imgExam
                        trvSelectedButtons.ImageList = imgExam
                        trvButtons.ItemHeight = 35
                        trvSelectedButtons.ItemHeight = 35
                End Select
                '' ''

                For i = 0 To _toolStrip.Items.Count - 1

                    If _moduleName <> enumModuleName.PatientExam Then
                        If IsNothing(_toolStrip.Items(i).Tag) = False Then
                            If CType(_toolStrip.Items(i).Tag, String).Trim() <> "" And _toolStrip.Items(i).Tag.ToString <> "Microphone" And _toolStrip.Items(i).Tag.ToString <> "Close" And _ButtonsToHide.Contains(_toolStrip.Items(i).Tag.ToString) = False Then
                                InsertNode(i)
                            End If
                        Else
                            If _moduleName <> enumModuleName.PatientDetails And (_toolStrip.Items(i).Visible = True Or _toolStrip.Items(i).IsOnOverflow = True) Then
                                trvSelectedButtons.Nodes.Add("Separator")
                            End If
                        End If
                    Else
                        If IsNothing(_toolStrip.Items(i).ToolTipText) = False Then
                            If CType(_toolStrip.Items(i).ToolTipText, String).Trim() <> "" And _toolStrip.Items(i).ToolTipText.Trim <> "Close" And _toolStrip.Items(i).ToolTipText.Trim <> "Save and Close" And _toolStrip.Items(i).ToolTipText.Trim <> "Save" And _toolStrip.Items(i).ToolTipText.Trim <> "Finish" And _ButtonsToHide.Contains(_toolStrip.Items(i).Tag.ToString) = False Then
                                If _toolStrip.Items(i).ToolTipText.Trim <> "Add Addendum" And _toolStrip.Items(i).ToolTipText.Trim <> "Mic" Then
                                    InsertNode(i)
                                End If
                            End If
                        Else
                            If _moduleName <> enumModuleName.PatientDetails And (_toolStrip.Items(i).Visible = True Or _toolStrip.Items(i).IsOnOverflow = True) Then
                                trvSelectedButtons.Nodes.Add("Separator")
                            End If
                        End If
                    End If
                Next


                Dim obtnSelection As New ClsToolButtonSelection()
                Dim dt As DataTable
                dt = obtnSelection.GetGetButtonSelection(gnLoginID, _moduleName)
                Dim arrButtons As String()
                Dim strButtons As String
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        strButtons = dt.Rows(0)("sButtons").ToString()
                        arrButtons = strButtons.Split(",")
                        _selectedButtons.Clear()
                        For j = 0 To arrButtons.Length - 1
                            _selectedButtons.Add(arrButtons(j))
                        Next
                    Else
                        For i = 0 To _toolStrip.Items.Count - 1
                            If (_toolStrip.Items(i).Visible = True OrElse _toolStrip.Items(i).IsOnOverflow = True) AndAlso IsNothing(_toolStrip.Items(i).Tag) = False Then
                                If _moduleName = enumModuleName.PatientExam Then
                                    _selectedButtons.Add(_toolStrip.Items(i).ToolTipText.Trim)
                                Else
                                    _selectedButtons.Add(_toolStrip.Items(i).Tag.ToString())
                                End If
                            End If
                        Next
                    End If
                Else
                    For i = 0 To _toolStrip.Items.Count - 1
                        If _toolStrip.Items(i).Visible = True AndAlso IsNothing(_toolStrip.Items(i).Tag) = False Then
                            _selectedButtons.Add(_toolStrip.Items(i).Tag.ToString.Trim)
                        End If
                    Next
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub InsertNode(ByVal btnIndex As Integer)
        Try
            Dim oNode As New TreeNode

            '' SET NODE TEXT AS BUTTON TAG/TOOLTIP ''
            If _moduleName = enumModuleName.PatientExam Then
                oNode.Text = _toolStrip.Items(btnIndex).ToolTipText.Trim
            Else
                oNode.Text = _toolStrip.Items(btnIndex).Tag.ToString.Trim
            End If

            '' COPY IMAGE TO PERTUCULAR IMAGELIST ''
            Select Case _moduleName
                Case enumModuleName.Dashboard
                    imgDashBoard.Images.Add(_toolStrip.Items(btnIndex).Image)
                    oNode.ImageIndex = imgDashBoard.Images.Count - 1
                    oNode.SelectedImageIndex = imgDashBoard.Images.Count - 1
                Case enumModuleName.PatientDetails
                    imgPatientDetails.Images.Add(_toolStrip.Items(btnIndex).Image)
                    oNode.ImageIndex = imgPatientDetails.Images.Count - 1
                    oNode.SelectedImageIndex = imgPatientDetails.Images.Count - 1
                Case enumModuleName.PatientExam
                    imgExam.Images.Add(_toolStrip.Items(btnIndex).Image)
                    oNode.ImageIndex = imgExam.Images.Count - 1
                    oNode.SelectedImageIndex = imgExam.Images.Count - 1
            End Select


            If _toolStrip.Items(btnIndex).Visible = True Or _toolStrip.Items(btnIndex).IsOnOverflow = True Then
                trvSelectedButtons.Nodes.Add(oNode)
            Else
                trvButtons.Nodes.Add(oNode)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private Sub InsertNodeAfterReset(ByVal buttonName As String, ByVal buttonFlag As String)
        Try
            Dim foundIndex As Integer
            '' SEARCH FOR PERTICULAR TOOL BUTTON
            For i As Integer = 0 To _toolStrip.Items.Count - 1
                If IsNothing(_toolStrip.Items(i).Tag) = False Then
                    If _moduleName = enumModuleName.PatientExam Then
                        If buttonName = _toolStrip.Items(i).ToolTipText.Trim() Then
                            foundIndex = i
                            Exit For
                        End If
                    Else
                        If buttonName = _toolStrip.Items(i).Tag.ToString.Trim Then
                            foundIndex = i
                            Exit For
                        End If
                    End If
                End If
            Next
            '' SEARCH END ''

            Dim oNode As New TreeNode

            '' SET NODE TEXT AS BUTTON NAME ''            
            oNode.Text = buttonName

            '' COPY IMAGE TO PERTUCULAR IMAGELIST ''
            Select Case _moduleName
                Case enumModuleName.Dashboard
                    imgDashBoard.Images.Add(_toolStrip.Items(foundIndex).Image)
                    oNode.ImageIndex = imgDashBoard.Images.Count - 1
                    oNode.SelectedImageIndex = imgDashBoard.Images.Count - 1
                Case enumModuleName.PatientDetails
                    imgPatientDetails.Images.Add(_toolStrip.Items(foundIndex).Image)
                    oNode.ImageIndex = imgPatientDetails.Images.Count - 1
                    oNode.SelectedImageIndex = imgPatientDetails.Images.Count - 1
                Case enumModuleName.PatientExam
                    imgExam.Images.Add(_toolStrip.Items(foundIndex).Image)
                    oNode.ImageIndex = imgExam.Images.Count - 1
                    oNode.SelectedImageIndex = imgExam.Images.Count - 1
            End Select
            '' INSERT NODE ''
            If buttonFlag = "Visible" Then
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

    '' COMMENT BY SUDHIR 20090421 '' OLD FUNCTION ''
    'Private Sub OKToolButtonSelection()

    '    Try
    '        Dim i As Integer

    '        '' COMMENT BY SUDHIR 20090417 '' AS WE ARE NOT USING ToolBarButtons_Rights TABLE ''
    '        'ToolButtons = New Collection
    '        ''Dim ToolBarButton As System.Windows.Forms.ToolStripItem
    '        'With chkToolButtons
    '        '    For i = 0 To .Items.Count - 1
    '        '        'Dim nToolButtonIndex As Integer
    '        '        'nToolButtonIndex = _ToolButtons.IndexOfKey(CType(.Items(i), ToolStripButton).Name)
    '        '        If chkToolButtons.GetItemChecked(i) = True Then
    '        '            '_ToolButtons.Item(nToolButtonIndex).Visible = True
    '        '            ToolButtons.Add(i)
    '        '        End If
    '        '    Next
    '        'End With
    '        ' '' To Save into DB
    '        'Call SaveToolBarButtons()
    '        '' END SUDHIR ''
    '        Me.Close()
    '        '-----------------------Saket 20080528
    '        'If _moduleName <> enumModuleName.PatientExam Then
    '        _selectedButtons.Clear()
    '        With chkToolButtons
    '            For i = 0 To .Items.Count - 1
    '                If chkToolButtons.GetItemChecked(i) = True Then
    '                    _selectedButtons.Add(chkToolButtons.Items(i).ToString())
    '                End If
    '            Next
    '        End With

    '        Dim obtnSelection As New ClsToolButtonSelection()
    '        If obtnSelection.SaveButtonSelection(gnLoginID, _moduleName, _selectedButtons) = True Then
    '            ShowButtonSelection()
    '        Else
    '            MessageBox.Show("Error : Can Not Save Button Selection ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End If
    '        'End If
    '        '------------------------------------
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub
    Private Sub OKToolButtonSelection()
        Try
            If blnIsOpenForTaskList = False Then
                Dim i As Integer
                Me.Close()
                _selectedButtons.Clear()
                For i = 0 To trvSelectedButtons.Nodes.Count - 1
                    If trvSelectedButtons.Nodes(i).Text = "Separator" Then
                        _selectedButtons.Add("|")
                    Else
                        _selectedButtons.Add(trvSelectedButtons.Nodes(i).Text)
                    End If
                Next

                Dim obtnSelection As New ClsToolButtonSelection()
                If obtnSelection.SaveButtonSelection(gnLoginID, _moduleName, _selectedButtons) = True Then
                    ShowButtonSelection()
                Else
                    MessageBox.Show("Error : Can Not Save Button Selection ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                _SelectedTaskColumn.Clear()
                strFlexGroupCustomColumns = ""
                Dim nColCount As Int16 = 0
                For Each tNd As TreeNode In trvSelectedButtons.Nodes
                    nColCount = nColCount + 1
                    If strFlexGroupCustomColumns <> "" Then
                        strFlexGroupCustomColumns = strFlexGroupCustomColumns + ","
                    End If
                    strFlexGroupCustomColumns = strFlexGroupCustomColumns + tNd.Text + ":" + nColCount.ToString() + ":1"
                Next

                If trvButtons.Nodes.Count > 0 Then
                    For Each tnd As TreeNode In trvButtons.Nodes
                        nColCount = nColCount + 1
                        If strFlexGroupCustomColumns <> "" Then
                            strFlexGroupCustomColumns = strFlexGroupCustomColumns + ","
                        End If
                        strFlexGroupCustomColumns = strFlexGroupCustomColumns + tnd.Text + ":" + nColCount.ToString() + ":0"
                    Next
                End If
                Dim ogloSettings As New gloSettings.GeneralSettings(GetConnectionString)
                ogloSettings.AddSetting("TaskFlexGroupCustomColumn", strFlexGroupCustomColumns, gnClinicID, gnLoginID, gloSettings.SettingFlag.Clinic)
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SaveToolBarButtons()
        Try

            Dim strSQL As String = ""
            Dim oDB As New gloStream.gloDataBase.gloDataBase


            strSQL = "Delete ToolBarButtons_Rights where nUserID = " & gnLoginID
            oDB.Connect(GetConnectionString)
            oDB.ExecuteNonSQLQuery(strSQL)
            oDB.Disconnect()

            'Fill new data for selected Node
            For i As Integer = 1 To ToolButtons.Count
                strSQL = " INSERT INTO ToolBarButtons_Rights (nUserID, nToolBarButton, nType) " _
                        & " VALUES (" & gnLoginID & ", " & ToolButtons(i) & ", 0)"
                oDB.Connect(GetConnectionString)
                oDB.ExecuteNonSQLQuery(strSQL)
                oDB.Disconnect()
            Next

        Catch ex As IndexOutOfRangeException
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '' COMMENT BY SUDHIR 20090421 '' OLD FUNCTION ''
    'Public Sub ShowButtonSelection()
    '    Try

    '        Dim obtnSelection As New ClsToolButtonSelection()
    '        Dim dt As DataTable
    '        dt = obtnSelection.GetGetButtonSelection(gnLoginID, _moduleName)
    '        Dim arrButtons As String()
    '        Dim strButtons As String
    '        If Not IsNothing(dt) Then
    '            If dt.Rows.Count > 0 Then
    '                strButtons = dt.Rows(0)("sButtons").ToString()
    '            Else
    '                Exit Sub
    '            End If
    '        End If
    '        arrButtons = strButtons.Split(",")
    '        _selectedButtons.Clear()

    '        For j As Integer = 0 To arrButtons.Length - 1
    '            _selectedButtons.Add(arrButtons(j))
    '        Next

    '        For i As Integer = 0 To _toolStrip.Items.Count - 1
    '            If _toolStrip.Items(i).Tag <> Nothing Then
    '                _toolStrip.Items(i).Visible = False
    '            Else
    '                _toolStrip.Items(i).Visible = True
    '            End If
    '        Next

    '        'Make selected buttons Visiable 
    '        For i As Integer = 0 To _toolStrip.Items.Count - 1
    '            'For j As Integer = 0 To _selectedButtons.Count - 1
    '            If _moduleName = enumModuleName.PatientExam Then
    '                If _toolStrip.Items(i).ToolTipText <> Nothing Then
    '                    'If _toolStrip.Items(i).ToolTipText.Trim = _selectedButtons(j).ToString() Then
    '                    If _selectedButtons.Contains(_toolStrip.Items(i).ToolTipText.Trim) Then
    '                        _toolStrip.Items(i).Visible = True
    '                        If gblnCoSignFlag = False And _toolStrip.Items(i).ToolTipText.Trim = "Co-Sign" Then ''THIS CONDITION IS ONLY FOR CO-SIGN FLAG
    '                            _toolStrip.Items(i).Visible = False
    '                        End If
    '                        If (gblnVoiceEnabled = False Or gblnSpeakerExists = False) And _toolStrip.Items(i).ToolTipText.Trim = "Mic" Then
    '                            _toolStrip.Items(i).Visible = False
    '                        End If
    '                        'Exit For
    '                    End If
    '                End If
    '            Else
    '                If _toolStrip.Items(i).Tag <> Nothing Then
    '                    'If _toolStrip.Items(i).Tag.ToString() = _selectedButtons(j).ToString() Then
    '                    If _selectedButtons.Contains(_toolStrip.Items(i).Tag.ToString()) Then
    '                        _toolStrip.Items(i).Visible = True
    '                        'Exit For
    '                    End If
    '                End If
    '            End If
    '            'Next

    '            If IsNothing(_toolStrip.Items(i).ToolTipText) = False Then
    '                If _moduleName = enumModuleName.PatientExam And (_toolStrip.Items(i).ToolTipText.Trim = "Close" Or _toolStrip.Items(i).ToolTipText.Trim = "Save and Close" Or _toolStrip.Items(i).ToolTipText.Trim = "Save" Or _toolStrip.Items(i).ToolTipText.Trim = "Finish") Then
    '                    _toolStrip.Items(i).Visible = True
    '                End If
    '            End If
    '        Next

    '        '' For Hiding the Consigative Splitters
    '        For i As Integer = 0 To _toolStrip.Items.Count - 1

    '            'Find first visible Splitter
    '            Dim j As Integer
    '            For j = i To _toolStrip.Items.Count - 1
    '                If _toolStrip.Items(j).Tag = Nothing And _toolStrip.Items(j).Visible = True Then
    '                    Exit For
    '                End If
    '            Next

    '            'Hide second Splitter if no visible items between first and second splitter   
    '            Dim k As Integer
    '            For k = j + 1 To _toolStrip.Items.Count - 1
    '                If _toolStrip.Items(k).Tag = Nothing And _toolStrip.Items(k).Visible = True Then
    '                    _toolStrip.Items(k).Visible = False
    '                ElseIf IsNothing(_toolStrip.Items(k).Tag) = False And _toolStrip.Items(k).Visible = True Then
    '                    Exit For
    '                End If
    '            Next

    '        Next

    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    '' COMMENT BY SUDHIR 20090421 ''
    'Private Sub FillSelection()
    '    Try

    '        For i As Integer = 0 To chkToolButtons.Items.Count - 1
    '            'For j As Integer = 0 To _selectedButtons.Count - 1
    '            'If chkToolButtons.Items(i).ToString().Trim() = _selectedButtons(j).ToString().Trim() Then
    '            If _selectedButtons.Contains(chkToolButtons.Items(i).ToString().Trim()) Then
    '                chkToolButtons.SetItemChecked(i, True)
    '                'Exit For
    '            End If
    '            'Next
    '        Next

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    ''Ojeswini_19June2008''

    Public Sub ShowButtonSelection()
        Try
            '' FETCHING USER SETTING FROM DATABASE ''
            Dim obtnSelection As New ClsToolButtonSelection()
            Dim dt As DataTable
            dt = obtnSelection.GetGetButtonSelection(gnLoginID, _moduleName)

            obtnSelection = Nothing

            Dim arrButtons As String()
            Dim strButtons As String = ""

            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    strButtons = dt.Rows(0)("sButtons").ToString()
                Else
                    '' SUDHIR 20100323 '' TO HIDE BUTTONS FROM BUTTONS_TO_HIDE COLLECTION WHEN NO SETTINGS FOUND IN DATABASE '' 
                    For iButton As Integer = 0 To _toolStrip.Items.Count - 1
                        If IsNothing(_toolStrip.Items(iButton).Tag) = False Then
                            If _ButtonsToHide.Contains(_toolStrip.Items(iButton).Tag.ToString) Then
                                _toolStrip.Items(iButton).Visible = False
                            End If
                        End If
                    Next
                    Exit Sub
                End If
            End If

            dt.Dispose()
            dt = Nothing

            arrButtons = strButtons.Split(",")

            strButtons = Nothing

            _selectedButtons.Clear()

            '' STORE SELECTED BUTTON SETTING IN ARRAYLIST ''
            For j As Integer = 0 To arrButtons.Length - 1
                _selectedButtons.Add(arrButtons(j))
            Next

            arrButtons = Nothing

            '' HIDE ALL BUTTONS OF CURRENT TOOLSTRIP ''
            For i As Integer = _toolStrip.Items.Count - 1 To 0 Step -1
                If _toolStrip.Items(i).Tag <> Nothing Then
                    If _moduleName = enumModuleName.PatientExam And (_toolStrip.Items(i).ToolTipText.Trim = "Close" Or _toolStrip.Items(i).ToolTipText.Trim = "Save and Close" Or _toolStrip.Items(i).ToolTipText.Trim = "Save" Or _toolStrip.Items(i).ToolTipText.Trim = "Finish") Then
                        _toolStrip.Items(i).Visible = True
                    ElseIf _moduleName = enumModuleName.PatientExam And _toolStrip.Items(i).ToolTipText.Trim = "Mic" And gblnVoiceEnabled = True And gblnSpeakerExists = True Then
                        _toolStrip.Items(i).Visible = True
                    Else
                        _toolStrip.Items(i).Visible = False
                    End If
                Else
                    _toolStrip.Items.RemoveAt(i)
                End If
            Next

            '' SHOW SELECTED BUTTONS ONE BY ONE ''
            '' BY SHIFTING BUTTON LOCATIONS ''
            For i As Integer = 0 To _selectedButtons.Count - 1
                If _selectedButtons(i) = "|" Then  '' PIPE CHARACTER INDICATES SEPARATOR ''
                    _ToolSeperator = New ToolStripSeparator
                    _toolStrip.Items.Add(_ToolSeperator)
                ElseIf _selectedButtons(i) <> "" Then
                    FindButton(_selectedButtons(i))
                End If
            Next

            '' ONLY FOR CLOSE BUTTON OF DASHBOARD''
            If _moduleName = enumModuleName.Dashboard Then
                FindButton("Close") '' AS IT IS CUMPOSARY BUTTON AT END ''
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function GetDefaultToolStripSetting() As ArrayList
        Try
            Dim arrToolItems As New ArrayList
            For i As Integer = 0 To _toolStrip.Items.Count - 1
                If IsNothing(_toolStrip.Items(i).Tag) = False Then

                    If _toolStrip.Items(i).Tag = "" Then
                        Continue For
                    End If

                    If _moduleName = enumModuleName.PatientExam Then
                        If _toolStrip.Items(i).Visible = True Or _toolStrip.Items(i).IsOnOverflow = True Then
                            arrToolItems.Add(_toolStrip.Items(i).ToolTipText.Trim & ".Visible")
                        Else
                            arrToolItems.Add(_toolStrip.Items(i).ToolTipText.Trim & ".Invisible")
                        End If
                    Else
                        If _toolStrip.Items(i).Visible = True Or _toolStrip.Items(i).IsOnOverflow = True Then
                            arrToolItems.Add(_toolStrip.Items(i).Tag.ToString.Trim & ".Visible")
                        Else
                            arrToolItems.Add(_toolStrip.Items(i).Tag.ToString.Trim & ".Invisible")
                        End If
                    End If
                Else
                    If _toolStrip.Items(i).Visible = True And _moduleName <> enumModuleName.PatientDetails Then
                        arrToolItems.Add("|")
                    End If
                End If
            Next
            Return arrToolItems
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function


    Private Sub ResetSettings()
        Try
            trvButtons.Nodes.Clear()
            trvSelectedButtons.Nodes.Clear()
            If blnIsOpenForTaskList = False Then


                trvButtons.BeginUpdate()
                trvSelectedButtons.BeginUpdate()

                If IsNothing(_DefaultToolStrip) Then
                    Exit Sub
                ElseIf _DefaultToolStrip.Count = 0 Then
                    Exit Sub
                End If

                '' INSERT SEPERATOR IN TREE '' 
                If _moduleName <> enumModuleName.PatientDetails Then
                    trvButtons.Nodes.Add("Separator")
                End If
                '' ''

                '' CLEAN PREVIOUS IMAGES FROM IMAGELIST ''
                Select Case _moduleName
                    Case enumModuleName.Dashboard
                        For i As Integer = imgDashBoard.Images.Count - 1 To 1 Step -1
                            imgDashBoard.Images.RemoveAt(i)
                        Next
                    Case enumModuleName.PatientDetails
                        For i As Integer = imgPatientDetails.Images.Count - 1 To 1 Step -1
                            imgPatientDetails.Images.RemoveAt(i)
                        Next
                    Case enumModuleName.PatientExam
                        For i As Integer = imgExam.Images.Count - 1 To 1 Step -1
                            imgExam.Images.RemoveAt(i)
                        Next
                End Select
                '' ''

                '' FILLING TREE WITH DEFAULT SETTINGS ''
                Dim strButton() As String
                Dim buttonName As String
                Dim buttonFlag As String = ""

                For i As Integer = 0 To _DefaultToolStrip.Count - 1

                    '' SPLIT STRING ''
                    strButton = Split(_DefaultToolStrip(i), ".")
                    buttonName = strButton(0)
                    If strButton.Length > 1 Then
                        buttonFlag = strButton(1)
                    End If

                    If _moduleName <> enumModuleName.PatientExam Then
                        If buttonName = "|" Then
                            If _moduleName <> enumModuleName.PatientDetails Then
                                trvSelectedButtons.Nodes.Add("Separator")
                            End If
                        Else
                            If buttonName <> "Microphone" And buttonName <> "Close" And _ButtonsToHide.Contains(buttonName) = False Then
                                InsertNodeAfterReset(buttonName, buttonFlag)
                            End If
                        End If
                    Else
                        If buttonName = "|" Then
                            If _moduleName <> enumModuleName.PatientDetails Then
                                trvSelectedButtons.Nodes.Add("Separator")
                            End If
                        Else
                            If buttonName <> "Close" And buttonName <> "Save and Close" And buttonName <> "Save" And buttonName <> "Finish" Then
                                If buttonName <> "Add Addendum" And buttonName <> "Mic" Then
                                    If (buttonName = "Clinical Decision Support") Then
                                        If (gblnCDSRights) Then
                                            InsertNodeAfterReset(buttonName, buttonFlag)
                                        End If
                                    ElseIf (buttonName = "Dx Snomed") Then
                                        If (gblnICD9Driven) Then
                                            InsertNodeAfterReset(buttonName, buttonFlag)
                                        End If
                                    Else

                                        InsertNodeAfterReset(buttonName, buttonFlag)
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
            Else

                trvButtons.ItemHeight = 20
                For Each taskCol1 As String In _AllHideTaskColumns
                    trvButtons.Nodes.Add(taskCol1)
                Next
                trvSelectedButtons.ItemHeight = 20

                For Each taskCol As String In _AllVisibleTaskColumns
                    trvSelectedButtons.Nodes.Add(taskCol)
                Next

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            trvButtons.EndUpdate()
            trvSelectedButtons.EndUpdate()
        End Try
    End Sub

    Private Sub FindButton(ByVal buttonName As String)
        '  Dim _toolButton As ToolStripButton
        Try
            For i As Integer = 0 To _toolStrip.Items.Count - 1
                If _moduleName = enumModuleName.PatientExam Then
                    If _toolStrip.Items(i).ToolTipText <> Nothing Then
                        If _toolStrip.Items(i).ToolTipText.Trim = buttonName Then
                            If gblnCoSignFlag = False And _toolStrip.Items(i).ToolTipText.Trim = "Co-Sign" Then ''THIS CONDITION IS ONLY FOR CO-SIGN FLAG
                                _toolStrip.Items(i).Visible = False
                                Exit Sub
                            Else
                                '' MOVING BUTTON ''
                                InsertButton(i)
                                Exit Sub
                                '' MOVING BUTTON ''
                            End If

                            If (gblnVoiceEnabled = False Or gblnSpeakerExists = False) And _toolStrip.Items(i).ToolTipText.Trim = "Mic" Then
                                _toolStrip.Items(i).Visible = False
                                Exit Sub
                            Else
                                '' MOVING BUTTON ''
                                InsertButton(i)
                                Exit Sub
                                '' MOVING BUTTON ''
                            End If
                        End If
                    End If
                Else
                    If _toolStrip.Items(i).Tag <> Nothing Then
                        If _toolStrip.Items(i).Tag.ToString() = buttonName And _ButtonsToHide.Contains(_toolStrip.Items(i).Tag.ToString()) = False Then
                            '' MOVING BUTTON ''
                            InsertButton(i)
                            Exit Sub
                            '' MOVING BUTTON ''
                        End If
                    End If
                End If

            Next
        Catch ex As Exception
            MessageBox.Show("Error on Button : " & buttonName & vbNewLine & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub InsertButton(ByVal buttonIndex As Integer)
        Dim _ToolButton As ToolStripItem
        _ToolButton = _toolStrip.Items(buttonIndex)
        _toolStrip.Items.RemoveAt(buttonIndex)
        _ToolButton.Visible = True
        _toolStrip.Items.Add(_ToolButton)

        If _moduleName = enumModuleName.PatientDetails Then
            _ToolSeperator = New ToolStripSeparator
            _toolStrip.Items.Add(_ToolSeperator)
        End If
    End Sub

    Private Sub tls_ToolButton_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_ToolButton.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "OK"
                    OKToolButtonSelection()

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
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK)
        End Try
    End Sub

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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        trvButtons.EndUpdate()
        trvSelectedButtons.EndUpdate()
    End Sub

    Private Sub btnRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        trvButtons.BeginUpdate()
        trvSelectedButtons.BeginUpdate()
        If blnIsOpenForTaskList Then
            Try
                Dim oNode As TreeNode
                If IsNothing(trvSelectedButtons.SelectedNode) = False Then
                    oNode = trvSelectedButtons.SelectedNode.Clone
                    If oNode.Text <> "Due Date" And oNode.Text <> "Patient Name" And oNode.Text <> "Subject" Then
                        trvSelectedButtons.SelectedNode.Remove()
                        If oNode.Text <> "Separator" Then
                            trvButtons.Nodes.Add(oNode)
                        End If
                    Else
                        MessageBox.Show("‘Due Date’, ‘Patient Name’ and ‘Subject’ are default columns and cannot be removed.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
                RemoveRepeatSeperator()
            Catch ex As Exception
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            Try
                Dim oNode As TreeNode
                If IsNothing(trvSelectedButtons.SelectedNode) = False Then
                    oNode = trvSelectedButtons.SelectedNode.Clone
                    trvSelectedButtons.SelectedNode.Remove()
                    If oNode.Text <> "Separator" Then
                        trvButtons.Nodes.Add(oNode)
                    End If
                End If
                RemoveRepeatSeperator()
            Catch ex As Exception
                MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
       
        trvButtons.EndUpdate()
        trvSelectedButtons.EndUpdate()
    End Sub

    Private Sub trvButtons_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvButtons.NodeMouseDoubleClick
        Try
            If IsNothing(e.Node) = False Then
                btnAdd_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub trvSelectedButtons_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvSelectedButtons.NodeMouseDoubleClick
        Try
            If IsNothing(e.Node) = False Then
                btnRemove_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        trvSelectedButtons.EndUpdate()
        If trvSelectedButtons.SelectedNode IsNot Nothing Then
            trvSelectedButtons.SelectedNode.EnsureVisible()
        End If
    End Sub

#Region " Mouse Hoover & Leave Events "  ''<<<<<<<<<<Ojeswini>>>>>>>For Give Mouse Hover and Leave image.

    Private Sub btnAdd_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.MouseHover
        btnAdd.BackgroundImage = Global.gloEMR.My.Resources.YellowRight24
    End Sub

    Private Sub btnAdd_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.MouseLeave
        btnAdd.BackgroundImage = Global.gloEMR.My.Resources.Right24
    End Sub

    Private Sub btnDown_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.MouseHover
        btnDown.BackgroundImage = Global.gloEMR.My.Resources.YellowDown24
    End Sub

    Private Sub btnDown_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDown.MouseLeave
        btnDown.BackgroundImage = Global.gloEMR.My.Resources.Down24
    End Sub

    Private Sub btnRemove_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.MouseHover
        btnRemove.BackgroundImage = Global.gloEMR.My.Resources.YellowLeft24
    End Sub

    Private Sub btnRemove_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRemove.MouseLeave
        btnRemove.BackgroundImage = Global.gloEMR.My.Resources.Left24
    End Sub

    Private Sub btnUp_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.MouseHover
        btnUp.BackgroundImage = Global.gloEMR.My.Resources.YellowUp24
    End Sub

    Private Sub btnUp_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUp.MouseLeave
        btnUp.BackgroundImage = Global.gloEMR.My.Resources.Up24
    End Sub
#End Region
End Class