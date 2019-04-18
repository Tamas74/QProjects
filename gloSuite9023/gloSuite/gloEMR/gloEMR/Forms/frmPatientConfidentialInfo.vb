Imports System.Data
Imports System.Data.SqlClient
Public Class frmPatientConfidentialInfo

    Private ObjPatientConfidentialDBLayer As clsPatientConfidentialInfoDBLayer
    'Private WithEvents dgCustomGrid As CustomDataGrid
    Private WithEvents dgcustomGrid As CustomTask

    Dim ReferralCount As Integer
    Private btnStatus As Integer

    Private Col_Check As Integer = 5
    Private Col_UserID As Integer = 0
    Private Col_LoginName As Integer = 1
    Private Col_Column1 As Integer = 2
    Private Col_Column2 As Integer = 3
    Private Col_ProviderID As Integer = 4

    Private Col_Count As Integer = 6

    Dim _confidentialid As Int64 = 0
    Dim _VisitID As Int64 = 0
    Dim _status As Int32 = 0
    Dim _IsOpenFromExam As Boolean = False
    Dim _ExamDate As Date

    Dim _nPatientId As Int64 '= gnPatientID 'commented by dipak to remove referance
    Dim _nExamId As Int64 = 0
    'Dim _dtvisitdate As DateTime = Nothing



    ''Sudhir - 20090212 - For ListControl for User
    Private oListControl As gloListControl.gloListControl
    Private oListUsers As gloListControl.gloListControl
    Private ToList As gloGeneralItem.gloItems
    ''

    Dim _VisitDate As Date = Now
    Public Property VisitDate() As Date
        Get
            Return _VisitDate
        End Get
        Set(ByVal value As Date)
            _VisitDate = value
        End Set
    End Property


    Public Sub New(ByVal ConfidentialID As Int64, ByVal VisitID As Int64, ByVal PatientID As Long)
        ' _confidentialid = ConfidentialID
        _VisitID = VisitID

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        _nPatientId = PatientID
    End Sub
    Public Sub New(ByVal ConfidentialID As Int64, ByVal VisitID As Int64, ByVal PatientID As Long, ByVal IsOpenFromExam As Boolean, ByVal ExamDate As Date)
        ' _confidentialid = ConfidentialID
        _VisitID = VisitID
        _IsOpenFromExam = IsOpenFromExam
        _ExamDate = ExamDate
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        _nPatientId = PatientID
    End Sub


    Private Sub frmPatientConfidentialInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim dt As DataTable
            ObjPatientConfidentialDBLayer = New clsPatientConfidentialInfoDBLayer(_nPatientId)

            '' Get The Confidential information for selected Visit
            dt = ObjPatientConfidentialDBLayer.GetConfidentialInfo(_VisitID)

            If IsNothing(dt) = False Then

                cmbUser.DataSource = Nothing
                cmbUser.Items.Clear()
                If (IsNothing(ToList) = False) Then
                    ToList.Dispose()
                    ToList = Nothing
                End If
                ToList = New gloGeneralItem.gloItems
                Dim ToItem As gloGeneralItem.gloItem
                For i As Int32 = 0 To dt.Rows.Count - 1
                    ''Comment By Sudhir - To Fill ToList of Users While Load.
                    '    cmbUser.Items.Add(New myList(CType(dt.Rows(i)("nUserId"), Long), CType(dt.Rows(i)("sUserName"), System.String)))
                    '    cmbUser.Text = CType(dt.Rows(i)("sUserName"), System.String)

                    ''New Code To Fill UserCombo.. 
                    ToItem = New gloGeneralItem.gloItem()

                    ToItem.ID = CType(dt.Rows(i)("nUserId"), Long)
                    ToItem.Description = dt.Rows(i)("sUserName").ToString()
                    ToItem.Code = ""

                    ToList.Add(ToItem)
                    ToItem.Dispose()
                    ToItem = Nothing
                Next

                cmbUser.DataSource = dt
                cmbUser.DisplayMember = dt.Columns("sUserName").ColumnName
                cmbUser.ValueMember = dt.Columns("nUserID").ColumnName

                If (dt.Rows.Count > 0) Then
                    txtInformation.Text = dt.Rows(0)("sDescription")
                    If Convert.ToBoolean(dt.Rows(0)("bIsActive")) = True Then
                        chk_Active.CheckState = CheckState.Checked
                    Else
                        chk_Active.CheckState = CheckState.Unchecked
                    End If

                End If

            End If

            '' ObjPatientConfidentialDBLayer.getconfidentialinfo(gnPatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ConfidentialInformation, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        
        Finally

        End Try
    End Sub

    Private Sub AddControl()


        dgcustomGrid = New CustomTask
        PnlMain.Controls.Add(dgcustomGrid)


        dgcustomGrid.SetVisible = False
    End Sub

    Private Sub RemoveControl()
        If Not IsNothing(dgcustomGrid) Then
            PnlMain.Controls.Remove(dgcustomGrid)
            dgcustomGrid.Visible = False
            dgcustomGrid.Dispose()
            dgcustomGrid = Nothing
        End If
    End Sub

    Private Sub LoadGrid()
        Try
            AddControl()
            If Not IsNothing(dgcustomGrid) Then
                'dgcustomGrid.Top = pnlInformationCtl.Top
                'dgcustomGrid.Left = pnlInformationCtl.Left
                'dgcustomGrid.Height = PnlMain.Height ' + pnlUserCombo.Height  '+ pnlOK.Height
                'dgcustomGrid.Visible = True
                'dgcustomGrid.Width = pnlInformationCtl.Width


                ''fill the control
                FillCustomControl()

                



                'PnlMain.Dock = DockStyle.None
                dgcustomGrid.Top = PnlToolStrip.Top
                dgcustomGrid.Left = PnlMain.Left
                dgcustomGrid.Height = pnlUserCombo.Height + PnlToolStrip.Height + pnlInformationCtl.Height
                dgcustomGrid.Visible = True
                dgcustomGrid.Width = PnlMain.Width
                dgcustomGrid.Dock = DockStyle.Fill

                dgcustomGrid.BringToFront()
                BindGrid()
                dgcustomGrid.Selectsearch(CustomDataGrid.enmcontrol.Search)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Confidential Information", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub FillCustomControl()
        Try
            PnlToolStrip.Dock = DockStyle.None
            pnlUserCombo.Dock = DockStyle.None
            PnlToolStrip.SendToBack()
            pnlUserCombo.SendToBack()
            PnlMain.Dock = DockStyle.Fill
            PnlMain.BringToFront()
            pnlInformationCtl.BringToFront()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RestoreOriginalsettings()
        Try
            PnlToolStrip.Dock = DockStyle.Top
            pnlUserCombo.Dock = DockStyle.Top
            PnlToolStrip.BringToFront()
            pnlUserCombo.BringToFront()
            PnlMain.Dock = DockStyle.Fill
            PnlMain.BringToFront()
            pnlInformationCtl.BringToFront()
        Catch ex As Exception

        End Try
    End Sub



    Private Sub BindGrid()
        Try
            Dim dt As DataTable = Nothing
            ObjPatientConfidentialDBLayer = New clsPatientConfidentialInfoDBLayer(_nPatientId)
            ReferralCount = 0
            Dim col As New DataColumn

            If btnStatus = 1 Then
                dt = ObjPatientConfidentialDBLayer.FillControls(btnStatus)
                '''' Add One Column to the Datatable For CheckBOX
                col.ColumnName = "Select"
                col.DataType = System.Type.GetType("System.Boolean")
                col.DefaultValue = CBool("False")
                dt.Columns.Add(col)

                'ElseIf btnStatus = 2 Then
                '    'Fill datatable with Patient Record
                '    dt = ObjTasksDBLayer.FillControls(btnStatus)
            End If


            If Not IsNothing(dt) Then
                If btnStatus = 1 Then
                    '' For DataBinding Users
                    dgcustomGrid.datasource(ObjPatientConfidentialDBLayer.DsDataview)
                    ' Sort data view on Login Name
                    ' ObjTasksDBLayer.SortDataview(ObjTasksDBLayer.DsDataview.Table.Columns(1).ColumnName)

                    'ElseIf btnStatus = 2 Then
                    '    '''' For Databinding Patient
                    '    dgcustomGrid.datasource(ObjTasksDBLayer.DsDataview)
                    '    ' Sort data view on First Name
                    '    'sarika 1st oct 07
                    '    'changed the sortdataview from column 2 to column3 as column3 is Last name
                    '    ObjTasksDBLayer.SortDataview(ObjTasksDBLayer.DsDataview.Table.Columns(3).ColumnName)
                    '    dgcustomGrid.Label1.Text = "Last Name"
                    '    '-----------------------
                End If

            End If
            ReferralCount = dt.Rows.Count
            HideColumns()
            'End With


        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Patient Confidential Information", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Confidential Information", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub HideColumns()
        Dim _TotalWidth As Single = dgcustomGrid.C1Task.Width - 5
        If btnStatus = 1 Then
            ' '' Show User Info
            With dgcustomGrid.C1Task
                .Cols.Fixed = 0
                .Rows.Fixed = 1
                .Cols.Count = 6
                .AllowEditing = True

                .SetData(0, Col_Check, "Select")
                '.Cols(Col_Check).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(Col_Check).Width = _TotalWidth * 0.09
                .Cols(Col_Check).AllowEditing = True


                .SetData(0, Col_UserID, "UserID")
                '.Cols(Col_UserID).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(Col_UserID).Width = _TotalWidth * 0
                .Cols(Col_UserID).AllowEditing = False

                .SetData(0, Col_LoginName, "Login Name")
                '.Cols(Col_LoginName).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(Col_LoginName).Width = _TotalWidth * 0.44
                .Cols(Col_LoginName).AllowEditing = False

                .SetData(0, Col_Column1, "Name")
                '.Cols(Col_Column1).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter
                .Cols(Col_Column1).Width = _TotalWidth * 0.47
                .Cols(Col_Column1).AllowEditing = False

                .Cols(Col_ProviderID).Width = 0
                .Cols(Col_Column2).Width = 0

                'Move the last column select to first column
                .Cols.Move(.Cols.Count - 1, 0)
            End With

            'ElseIf btnStatus = 2 Then
            '    '''' Show Patient Info
            '    With dgcustomGrid.C1Task
            '        .Cols.Fixed = 0
            '        .Cols.Count = 4

            '        .SetData(0, Col_nPatientID, "nPatientID")
            '        .Cols(Col_nPatientID).Width = 0

            '        .SetData(0, Col_PatientCode, "Patient Code")
            '        .Cols(Col_PatientCode).Width = _TotalWidth * 0.19
            '        '.Cols(Col_PatientCode).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            '        .SetData(0, Col_Column12, "First Name")
            '        .Cols(Col_Column12).Width = _TotalWidth * 0.39
            '        '.Cols(Col_Column12).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter

            '        .SetData(0, Col_Column22, "Last Name")
            '        .Cols(Col_Column22).Width = _TotalWidth * 0.39
            '        '.Cols(Col_Column22).TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.GeneralCenter
            '    End With
        End If
    End Sub
    Private Sub SetGridValues(Optional ByVal dblstatus As System.Int16 = 0)
        Try
            If btnStatus = 1 Then

                If dblstatus = 0 Then
                    Dim i As Integer
                    ' Dim count As Integer

                    'Pramod 05122007
                    ''Bind the checked user in the combo box

                    For i = 1 To dgcustomGrid.C1Task.Rows.Count - 1
                        If dgcustomGrid.C1Task.GetCellCheck(i, 0) = C1.Win.C1FlexGrid.CheckEnum.Checked Then
                            If FindDuplicateTo(CType(dgcustomGrid.C1Task.GetData(i, 1), Long)) Then
                                cmbUser.Items.Add(New myList(CType(dgcustomGrid.C1Task.GetData(i, 1), Long), CType(dgcustomGrid.C1Task.GetData(i, 2), System.String)))
                                cmbUser.Text = CType(dgcustomGrid.GetItem(i, 2), System.String)
                            End If
                        End If
                    Next

                End If
                ''ElseIf btnStatus = 2 Then

                ''    ''''<><><><><> Check Patient Status <><><><><><>''''
                ''    '''' 20060918 -Mahesh 
                ''    Dim PatID As Long = 0
                ''    '''''''********** The following IF statement is added by Anil on 06/10/2007 
                ''    '''''''********** This is added because the application was giving error : "Index was out of range" while adding blank Patient name in Add Task form.
                ''    If Not dgcustomGrid.GetCurrentrowIndex >= 0 Then
                ''        MessageBox.Show("Patient's Name Required", "Add Tasks", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ''        Exit Sub
                ''    Else
                ''        PatID = CType(dgcustomGrid.GetItem(dgcustomGrid.GetCurrentrowIndex, 0), Long)
                ''        '''''<><><><><> Check Patient Status <><><><><><>''''
                ''        ''''' 20070125 -Mahesh 
                ''        If CheckPatientStatus(PatID) = False Then
                ''            Exit Sub
                ''        End If
                ''        '''''<><><><><> Check Patient Status <><><><><><>''''

                ''        txtPatient.Text = CType(dgcustomGrid.GetItem(dgcustomGrid.GetCurrentrowIndex, 2), System.String) & " " & CType(dgcustomGrid.GetItem(dgcustomGrid.GetCurrentrowIndex, 3), System.String)
                ''        txtPatient.Tag = CType(dgcustomGrid.GetItem(dgcustomGrid.GetCurrentrowIndex, 0), Long)
                ''        btnPatient.Focus()
                ''    End If
            End If
            RemoveControl()
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Patient Confidential Information", MessageBoxButtons.OK, MessageBoxIcon.Error)
            RemoveControl()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Confidential Information", MessageBoxButtons.OK, MessageBoxIcon.Error)
            RemoveControl()
        Finally
            'RemoveControl()
        End Try
    End Sub
    Private Function FindDuplicateTo(ByVal Id As Long) As Boolean
        Dim i As Integer
        For i = 0 To cmbUser.Items.Count - 1
            Dim objmylist As myList
            objmylist = (CType(cmbUser.Items.Item(i), myList))
            If Id = objmylist.Index Then
                Return False
                Exit Function
            End If
        Next
        Return True
    End Function

    Private Sub btnSelectInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectInfo.Click
        '' Comment by -- Sudhir - 20090212 - Old UserList. --''
        'RemoveControl()
        'btnStatus = 1
        'LoadGrid()

        ''When users custom control is visible them hide the search textbox and label
        'dgcustomGrid.Label1.Visible = False
        'dgcustomGrid.txtsearch.Visible = False
        '' -- -- ''

        Try
            ''Remove ListControl if Present.
            If Not IsNothing(oListUsers) Then
                For i As Integer = Me.Controls.Count - 1 To 0 Step -1
                    If Me.Controls(i).Name = oListUsers.Name Then
                        Me.Controls.Remove(Me.Controls(i))
                        Exit For
                    End If
                Next
                Try
                    RemoveHandler oListUsers.ItemSelectedClick, AddressOf oListUsers_ItemSelectedClick
                    RemoveHandler oListUsers.ItemClosedClick, AddressOf oListUsers_ItemClosedClick
                Catch ex As Exception

                End Try

                oListUsers.Dispose()
                oListUsers = Nothing
            End If

            oListUsers = New gloListControl.gloListControl(GetConnectionString, gloListControl.gloListControlType.Users, True, Me.Width)
            oListUsers.ControlHeader = "Users"

            AddHandler oListUsers.ItemSelectedClick, AddressOf oListUsers_ItemSelectedClick
            AddHandler oListUsers.ItemClosedClick, AddressOf oListUsers_ItemClosedClick
            oListUsers.Dock = DockStyle.Fill

            ''To Select already Added Users.
            If IsNothing(ToList) = False Then
                For i As Integer = 0 To ToList.Count - 1
                    oListUsers.SelectedItems.Add(ToList(i))
                Next
            End If
            ''
            Me.Controls.Add(oListUsers)
            oListUsers.OpenControl()
            oListUsers.ShowHeaderPanel(False)
            PnlToolStrip.Visible = False
            oListUsers.Dock = DockStyle.Fill
            oListUsers.BringToFront()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ConfidentialInformation, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Error on UserListControl" & ex.ToString(), "Patient Orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub dgCustomGrid_OKClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgcustomGrid.OKClick
        SetGridValues(0)

        RestoreOriginalsettings()

    End Sub
    Private Sub dgCustomGrid_CloseClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgcustomGrid.CloseClick
        RemoveControl()
        RestoreOriginalsettings()
    End Sub

    Private Sub oListUsers_ItemSelectedClick(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim dtUsers As New DataTable()
            Dim dcId As New DataColumn("ID")
            Dim dcDescription As New DataColumn("Description")
            dtUsers.Columns.Add(dcId)
            dtUsers.Columns.Add(dcDescription)
            If (IsNothing(ToList) = False) Then
                ToList.Dispose()
                ToList = Nothing
            End If
            ToList = New gloGeneralItem.gloItems()
            Dim ToItem As gloGeneralItem.gloItem

            If oListUsers.SelectedItems.Count > 0 Then
                For i As Int16 = 0 To oListUsers.SelectedItems.Count - 1
                    Dim drTemp As DataRow = dtUsers.NewRow()
                    drTemp("ID") = oListUsers.SelectedItems(i).ID
                    drTemp("Description") = oListUsers.SelectedItems(i).Description
                    dtUsers.Rows.Add(drTemp)

                    ToItem = New gloGeneralItem.gloItem()

                    ToItem.ID = oListUsers.SelectedItems(i).ID
                    ToItem.Description = oListUsers.SelectedItems(i).Description

                    ToList.Add(ToItem)
                    ToItem.Dispose()
                    ToItem = Nothing
                Next
            End If
            cmbUser.DataSource = dtUsers
            cmbUser.ValueMember = dtUsers.Columns("ID").ColumnName
            cmbUser.DisplayMember = dtUsers.Columns("Description").ColumnName
            PnlToolStrip.Visible = True

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ConfidentialInformation, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show("Error on UserListControl" & ex.ToString(), "Patient Orders", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub oListUsers_ItemClosedClick(ByVal sender As Object, ByVal e As EventArgs)
        PnlToolStrip.Visible = True
    End Sub

    'save
    Private Sub tlsSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsSave.Click

        Dim _sdescription As String = ""
        Try
            If cmbUser.Items.Count < 1 Then
                MessageBox.Show("Select User Information", "Patient Confidential Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                btnSelectInfo.Focus()
                Exit Sub
            ElseIf txtInformation.Text.Trim = "" Then
                MessageBox.Show("Fill Patient Confidential Information ", "Patient Confidential Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtInformation.Focus()
                Exit Sub
            End If

            ObjPatientConfidentialDBLayer = New clsPatientConfidentialInfoDBLayer(_nPatientId)

            '_confidentialid = ObjPatientConfidentialDBLayer.getconfidentialID()
            _sdescription = txtInformation.Text

            'add into PatientConfidentialInfo => patientid, new confidentialid, description
            'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            '_confidentialid = ObjPatientConfidentialDBLayer.AddNewPatientConfidentialID(gnPatientID, _confidentialid, _sdescription, _VisitID, 0, _VisitDate, chk_Active.Checked)
            If _IsOpenFromExam = True Then
                _confidentialid = ObjPatientConfidentialDBLayer.AddNewPatientConfidentialID(_nPatientId, _confidentialid, _sdescription, _VisitID, 0, _ExamDate, chk_Active.Checked)
            Else
                _confidentialid = ObjPatientConfidentialDBLayer.AddNewPatientConfidentialID(_nPatientId, _confidentialid, _sdescription, _VisitID, 0, _VisitDate, chk_Active.Checked)
            End If

            'end modification

            'add into PatientConfidentialInfoDetails => userid, confidentialid,username
            Dim arrUsers As New ArrayList
            ' Dim i As Integer
            ''Comment By sudhir 20090212 
            'For i = 0 To cmbUser.Items.Count - 1
            '    Dim myList As New myList
            '    myList = CType(cmbUser.Items(i), myList)
            '    arrUsers.Add(myList)
            'Next

            ObjPatientConfidentialDBLayer.AddPatientConfidentialDetails(_confidentialid, ToList)
            '\\ Added on 20090411
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ConfidentialInformation, gloAuditTrail.ActivityType.Add, "Fill Patient Confidential Information", gloAuditTrail.ActivityOutCome.Success)

            txtInformation.Text = ""

            Me.Close()


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ConfidentialInformation, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "Patient Confidential Information", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub ToolStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

    End Sub

    Private Sub tlsClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tlsClose.Click
        Me.Close()

    End Sub

    ' Delete User from Combo
    Private Sub btnClearUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearUser.Click
        Try
            Dim _userId As Int64
            Dim i As Int16
            If cmbUser.SelectedIndex >= 0 Then
                If Not IsNothing(cmbUser.SelectedItem) Then
                    If MessageBox.Show("Do you want to clear selected user?", "Patient Confidential Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                        'cmbUser.Items.Remove(CType(cmbUser.SelectedItem, myList))
                        'cmbUser.DataSource = Nothing
                        'ToList.Clear()
                        ''Added by Mayuri:20101030-to delete selected user one by one
                        _userId = Convert.ToInt64(cmbUser.SelectedValue)

                        For i = 0 To ToList.Count - 1

                            If (ToList(i).ID = _userId) Then

                                ToList.RemoveAt(i)
                                Exit For

                            End If
                        Next
                        If cmbUser.Items.Count = 0 Then
                            cmbUser.Text = ""

                        End If



                        Dim dtUsers As DataTable = DirectCast(cmbUser.DataSource, DataTable)
                        dtUsers.Rows.RemoveAt(cmbUser.SelectedIndex)

                        If cmbUser.SelectedIndex > 0 Then
                            cmbUser.SelectedIndex = 0
                        End If
                        ''End


                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Patient Confidential Information", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectInfo.MouseHover, btnClearUser.MouseHover
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btn_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectInfo.MouseLeave, btnClearUser.MouseLeave
        CType(sender, Button).BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongButton
        CType(sender, Button).BackgroundImageLayout = ImageLayout.Stretch
    End Sub
End Class