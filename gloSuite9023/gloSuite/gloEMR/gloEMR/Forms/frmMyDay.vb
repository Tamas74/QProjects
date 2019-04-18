
Imports C1.Win.C1FlexGrid
Imports gloEmdeonInterface.Forms  '' Add reference by Abhijeet
Imports System.Data.SqlClient
Imports gloEMR.gloEMRWord 'Added by manoj jadhav on 20130305

Public Class frmMyDay

#Region " C1 Constants "

    Private Const COL_MSTAppointmentID = 0
    Private Const COL_DayName = 1
    Private Const COL_Description = 2
    Private Const Col_AppointmentDate = 3
    Private Const COL_ProviderName = 4
    Private Const COL_PatientName = 5
    Private Const COL_DTLAppointmentID = 6
    Private Const COL_ProviderID = 7
    Private Const COL_LocationName = 8
    Private Const COL_DepartmentName = 9
    Private Const COL_AppointmentTypeDesc = 10
    Private Const COL_UsedStatus = 11
    Private Const COL_DTLAppMethod = 12
    Private Const COL_MSTAppMethod = 13
    Private Const COL_LineNumber = 14
    Private Const COL_StartTime = 15
    Private Const COL_EndTime = 16
    Private Const COL_Cal_COUNT = 17

    Private Const Col_Task_No = 3
    Private Const Col_Task_TaskID = 2
    Private Const Col_Task_Subject = 1
    Private Const Col_Task_DueDate = 0
    Private Const Col_Task_Status = 4
    Private Const Col_Task_Completed = 5
    Private Const Col_Task_Priority = 6
    Private Const Col_Task_TaskDate = 7
    Private Const Col_Task_PatientID = 8
    Private Const Col_Task_Assigned = 9
    Private Const Col_Task_ColCount = 10

    Private Const COL_MSG_ID = 0
    Private Const COL_MSG_DATE = 1
    Private Const COL_MSG_PatientName = 2
    Private Const COL_MSG_Subject = 3  ''added for message PRD changes 8022
    Private Const COL_MSG_DESC = 4
    Private Const COL_MSG_PatientCode = 5
    Private Const COL_MSG_PatientID = 6
    Private Const COL_MSG_Priority = 7
    Private Const COL_MSG_COUNT = 8
    Private Const COL_Triage_ID = 0
    Private Const COL_Triage_DATE = 1
    Private Const COL_Triage_PatientID = 2
    Private Const COL_Triage_PatientName = 3
    Private Const COL_Triage_DESC = 4
    Private Const COL_Triage_COUNT = 5

#End Region


    ''Sandip Darade  20100427
    Const col_color0 = 0
    Const col_color1 = 1
    Const col_color2 = 2
    Const col_color3 = 3
    Const col_color4 = 4
    Const col_color5 = 5
    Const col_No = 6
    Const col_AppId = 7
    Const col_AppPatientName = 11
    Const col_AppDate = 9
    Const col_AppPatientID = 10
    Const col_AppdtlId = 14
    Const col_AppProviderID = 12
    Const col_AppTime = 8
    Const col_AppLocation = 13
    Const col_AppdtlMethod = 15
    Const col_AppMstMethod = 16
    Const col_AppDepartmentname = 17
    Const col_AppUsedStatus = 18

    Const col_Count = 19

    Dim csCalendar As C1.Win.C1FlexGrid.CellStyle
    Dim oClsTriage As New gloStream.gloEMR.Triage.clsTriage
    Dim oTriages As gloStream.gloEMR.Triage.Supportings.Triages
    Dim oDashBoard As New clsDoctorsDashBoard
    Dim ofrmTask As gloTaskMail.frmTask
    Dim currentDate As Date = Now.Date
    Dim isShowAllDays As Boolean = False
    Dim oToolTip As ToolTip
    Dim nTaskId As Int64  '' By Abhijeet on date 20100327

    'Bug #82464: 00000909: Task screen not opening respective screen
    Public Delegate Sub OnViewTaskChange(sender As Object, e As EventArgs, e2 As gloTaskMail.TaskChangeEventArg, objfrmtask As Object) ''new parameter added to delegate for task incident
    Public Event OnViewTask_Change As OnViewTaskChange


    Public Enum GridType
        None = 0
        Calender = 1
        Task = 2
        Messages = 3
        Triage = 4
    End Enum
    Dim dtPatientOnDate As Date = Now.Date
    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            Return cp
        End Get
    End Property


#Region " Form Load Event "


    Private Sub frmMyDay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            btnNext.BackgroundImage = Global.gloEMR.My.Resources.Forward
            btnNext.BackgroundImageLayout = ImageLayout.Center
            btnPrevious.BackgroundImage = Global.gloEMR.My.Resources.Rewind
            btnPrevious.BackgroundImageLayout = ImageLayout.Center

            oToolTip = New ToolTip
            oToolTip.SetToolTip(btnNext, "Next Day")
            oToolTip.SetToolTip(btnPrevious, "Previous Day")

            lblTodayDate.Text = Now.Date.ToLongDateString
            lblHeader.Text = " My Day - " & oDashBoard.GetUserName(gnLoginID)

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmMyDay_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        RefreshAll()
    End Sub

    Private Sub frmMyDay_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        '' This is Important to remove ToolTips. otherwise It gives random NULL reference error at EXE lifetime.. 
        If IsNothing(oToolTip) = False Then
            oToolTip.RemoveAll()
            oToolTip.Dispose()
            oToolTip = Nothing
        End If
        'SLR: Free oClsTriage, oTriages, oDashBoard 
        oClsTriage = Nothing

        If Not oTriages Is Nothing Then
            oTriages.Clear()
            oTriages = Nothing
        End If
        
        oDashBoard = Nothing
    End Sub
#End Region

    Private Sub DefineStyles()
        '= oflex.Styles.Add("csCalendar")
        csCalendar.ForeColor = Color.FromArgb(31, 73, 125)
        csCalendar.Border.Style = C1.Win.C1FlexGrid.BorderStyleEnum.None
        csCalendar.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte((0)))
        csCalendar.WordWrap = True
        'csCalendar.BuildString(C1.Win.C1FlexGrid.StyleElementFlags.WordWrap)
    End Sub

    Public Sub DesignGrid(ByVal oflex As C1.Win.C1FlexGrid.C1FlexGrid, ByVal oType As GridType)
        Try
            oflex.Rows.Count = 0
            oflex.AllowEditing = False

            If oType = GridType.Calender Then
                oflex.Cols.Count = COL_Cal_COUNT

            ElseIf oType = GridType.Task Then
                C1Task.Styles.Normal.WordWrap = True
                oflex.Cols.Count = Col_Task_ColCount
            ElseIf oType = GridType.Messages Then
                oflex.Cols.Count = COL_MSG_COUNT
                
            ElseIf oType = GridType.Triage Then
                oflex.Cols.Count = COL_Triage_COUNT
                
            End If

            Dim csDays As C1.Win.C1FlexGrid.CellStyle '= oflex.Styles.Add("csCalendar")
            Try
                If (oflex.Styles.Contains("csCalendar")) Then
                    csDays = oflex.Styles("csCalendar")
                Else
                    csDays = oflex.Styles.Add("csCalendar")
                    csDays.ForeColor = Color.FromArgb(31, 73, 125)
                    csDays.Border.Style = C1.Win.C1FlexGrid.BorderStyleEnum.None
                    csDays.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte((0)))
                    csDays.BackColor = Color.FromArgb(222, 231, 250)
                    csDays.ForeColor = Color.FromArgb(21, 66, 139)
                    csDays.WordWrap = True
                End If
            Catch ex As Exception
                csDays = oflex.Styles.Add("csCalendar")
                csDays.ForeColor = Color.FromArgb(31, 73, 125)
                csDays.Border.Style = C1.Win.C1FlexGrid.BorderStyleEnum.None
                csDays.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CByte((0)))
                csDays.BackColor = Color.FromArgb(222, 231, 250)
                csDays.ForeColor = Color.FromArgb(21, 66, 139)
                csDays.WordWrap = True
            End Try
           

            Dim csNormal As C1.Win.C1FlexGrid.CellStyle '= oflex.Styles.Add("csAppointment")
            Try
                If (oflex.Styles.Contains("csAppointment")) Then
                    csNormal = oflex.Styles("csAppointment")
                Else
                    csNormal = oflex.Styles.Add("csAppointment")
                    csNormal.ForeColor = Color.FromArgb(31, 73, 125)
                    csNormal.Border.Style = C1.Win.C1FlexGrid.BorderStyleEnum.None
                    csNormal.Font = gloGlobal.clsgloFont.gFont_SMALL 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte((0)))
                    csNormal.BackColor = Color.FromArgb(240, 247, 255)
                    csNormal.ForeColor = Color.Black
                    csNormal.WordWrap = True
                End If
            Catch ex As Exception
                csNormal = oflex.Styles.Add("csAppointment")
                csNormal.ForeColor = Color.FromArgb(31, 73, 125)
                csNormal.Border.Style = C1.Win.C1FlexGrid.BorderStyleEnum.None
                csNormal.Font = gloGlobal.clsgloFont.gFont_SMALL 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte((0)))
                csNormal.BackColor = Color.FromArgb(240, 247, 255)
                csNormal.ForeColor = Color.Black
                csNormal.WordWrap = True
            End Try
           

            If oType = GridType.Calender Then

                ShowAppointments()

            ElseIf oType = GridType.Task Then
                UpdateLog("FillTasks:START")
                FillTasks()
                UpdateLog("FillTasks:END")
            ElseIf oType = GridType.Messages Then
                oflex.Cols(COL_MSG_ID).Visible = False
                oflex.Cols(COL_MSG_PatientCode).Visible = False
                oflex.Cols(COL_MSG_PatientID).Visible = False
                UpdateLog("FillMessage:START")
                FillMessage()
                UpdateLog("FillMessage:END")
            ElseIf oType = GridType.Triage Then

                oflex.Cols(COL_Triage_ID).Visible = False
                oflex.Cols(COL_Triage_PatientID).Visible = False

                oflex.Cols(COL_Triage_DATE).Width = oflex.Width * 0.3
                oflex.Cols(COL_Triage_PatientName).Width = oflex.Width * 0.3
                oflex.Cols(COL_Triage_DESC).Width = oflex.Width * 0.4

                FillReceivedTriage_new()

            End If
            Application.DoEvents()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub RefreshAll()
        Me.Cursor = Cursors.WaitCursor

        ShowAppointments()

        DesignGrid(C1Task, GridType.Task)

        DesignGrid(C1Message, GridType.Messages)

        DesignGrid(C1Triage, GridType.Triage)

        Me.Cursor = Cursors.Default
    End Sub

#Region " APPOINTMENTS "
    Private Function GetAppointments() As DataTable
        Dim oDB As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
        Dim oResultTable As DataTable  ''slr new is not needed 

        Try
            Dim oParamater As gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            oParamater.DataType = SqlDbType.DateTime
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ApptDate"
            oParamater.Value = currentDate
            oDB.DBParametersCol.Add(oParamater)

            oParamater = New gloEMRGeneralLibrary.gloEMRDatabase.DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@userid"
            oParamater.Value = gnLoginProviderID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            oResultTable = oDB.GetDataTable("Triage_GetAppointments")

            Return oResultTable
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function
    Private Function GetAppointmentsForUser_old() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim dtAppointment As New DataTable()
        Try

            oDB.Connect(False)
            Dim strSQL As String = ""


           
            strSQL = "  SELECT	ISNULL(AS_Appointment_DTL.nMSTAppointmentID, 0) AS nMSTAppointmentID, ISNULL(AS_Appointment_DTL.nDTLAppointmentID, 0) AS nDTLAppointmentID, " _
           & " ISNULL(AS_Appointment_MST.sAppointmentTypeDesc,'')  AS sAppointmentType,ISNULL(AS_Appointment_MST.sLocationName,'')  AS sLocationName, AS_Appointment_DTL.dtStartDate,  AS_Appointment_DTL.dtEndDate,AS_Appointment_MST.nPatientID AS  nPatientID,AS_Appointment_DTL.dtStartTime as dtStartTime , AS_Appointment_DTL.dtEndTime AS dtEndTime , " _
           & "  (ISNULL(Provider_MST.sFirstName,'')+ SPACE(1) + ISNULL(Provider_MST.sMiddleName,'') + SPACE(1) +ISNULL(Provider_MST.sLastName,'')) AS ProviderName, Provider_MST.nProviderID" _
           & "  ,  ISNULL(AS_Appointment_DTL.bIsSingleRecurrence,0) AS DTLAppMethod,ISNULL(AS_Appointment_MST.bIsSingleRecurrence,0) AS MSTAppMethod,ISNULL(AS_Appointment_DTL.sDepartmentName,'') AS sDepartmentName,ISNULL(AS_Appointment_DTL.nUsedStatus,0) AS nUsedStatus" _
           & " FROM AS_Appointment_DTL INNER JOIN AS_Appointment_MST ON AS_Appointment_DTL.nMSTAppointmentID = AS_Appointment_MST.nMSTAppointmentID INNER JOIN " _
           & " Provider_MST ON AS_Appointment_MST.nASBaseID = Provider_MST.nProviderID " _
           & " WHERE AS_Appointment_DTL.nClinicID = 1 And AS_Appointment_DTL.nASBaseID = " & gnLoginProviderID & " " _
           & " AND AS_Appointment_DTL.nASBaseFlag = 1 AND AS_Appointment_DTL.nUsedStatus <> 6 AND AS_Appointment_DTL.nUsedStatus <> 7 AND AS_Appointment_DTL.nUsedStatus <> 5 AND  " _
           & " AS_Appointment_DTL.dtStartdate = " & gloDateMaster.gloDate.DateAsNumber(currentDate.ToString) & "  ORDER BY AS_Appointment_DTL.dtStartDate , dtStartTime "



            oDB.Retrive_Query(strSQL, dtAppointment)
            oDB.Disconnect()
            If Not IsNothing(oDB) Then ''slr free odb
                oDB.Dispose()
            End If
            oDB = Nothing
            Return dtAppointment
        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Return Nothing

        End Try
    End Function
    ''Added by Mayuri:20110316-For optimization
    Private Function GetAppointmentsForUser() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dtAppointment As New DataTable()
        Try

            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@nProviderId", gnLoginProviderID, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@nAppointmentDate", gloDateMaster.gloDate.DateAsNumber(currentDate.ToString), ParameterDirection.Input, SqlDbType.Int)
            oParam.Add("@nClinicID", gnClinicID, ParameterDirection.Input, SqlDbType.Int)
            oDB.Retrive("GetAppointmentsForUser", oParam, dtAppointment)
            oDB.Disconnect()
            Return dtAppointment
        Catch ex As Exception
            MessageBox.Show(Me, ex.ToString(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.[Error])
            Return Nothing
        Finally
            If (IsNothing(oParam) = False) Then
                oParam.Dispose()
                oParam = Nothing
            End If
            If Not IsNothing(oDB) Then ''slr free odb
                oDB.Dispose()
            End If
            oDB = Nothing

        End Try
    End Function

    Private Sub ShowAppointments_old()
        Dim dt As DataTable = Nothing  ''slr new not needed 
        Try
            C1Calendar.ScrollBars = ScrollBars.None
            'C1Calendar.Clear()
            C1Calendar.DataSource = Nothing

            C1Calendar.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns
            C1Calendar.Clear(C1.Win.C1FlexGrid.ClearFlags.All)

            C1Calendar.Cols.Count = col_Count ''Sandip Darade  20100308
            C1Calendar.Rows.Count = 1
            C1Calendar.AllowEditing = False
            C1Calendar.Styles.Focus.BackColor = Color.FromArgb(255, 224, 160)

            Dim NormalCellBackColor As Color = Color.FromArgb(240, 247, 255)



            C1Calendar.SetData(0, col_No, "No.")
            C1Calendar.SetData(0, col_AppId, "Appointment ID")
            C1Calendar.SetData(0, col_AppPatientName, "Patient")
            C1Calendar.SetData(0, col_AppDate, "Appointment Date")
            C1Calendar.SetData(0, col_AppPatientID, "Patient ID")
            C1Calendar.SetData(0, col_AppdtlId, "Appointment DtlID")
            C1Calendar.SetData(0, col_AppProviderID, "Provider ID")
            C1Calendar.SetData(0, col_AppTime, "Time")
            C1Calendar.SetData(0, col_AppLocation, "Location")
            C1Calendar.SetData(0, col_AppDepartmentname, "Departmentname")
            C1Calendar.SetData(0, col_AppdtlMethod, "Method DTL")
            C1Calendar.SetData(0, col_AppMstMethod, "Method MST")
            C1Calendar.SetData(0, col_AppUsedStatus, "Status")


            Dim rrDueDate As New ArrayList ''Used to Store DueDates and sort it afterwards
            Dim i As Integer
            dt = Nothing
            dt = GetAppointmentsForUser()

            Dim oPatient As New gloPatient.gloPatient(GetConnectionString)
            ' Dim oClsDashBoard As New clsDoctorsDashBoard  slr not used

            Dim strPatientname As String = "" '' This string contains: Patient Name
            Dim strLocation As String = "" '' This string contains: Patient Name
            Dim strtime As String = "" '' This string contains: Patient Name
            ' Dim _PatientID As Int64
            ' Dim _PatientName As String
            C1Calendar.Cols(col_AppTime).Format = "t"
            Dim ParentRowIndex As Int32 = 0

            If IsNothing(dt) = False Then
                For i = 0 To dt.Rows.Count - 1
                    'If oTriages(i).IsFinished = False Then
                    Dim _ApptmntDate As DateTime = Convert.ToDateTime(gloDateMaster.gloDate.DateAsDate(dt.Rows(i)("dtEndDate")))
                    Dim _ApptmntDateString As String = " Date : " + Format(_ApptmntDate, "MM/dd/yyyy")

                    C1Calendar.Rows.Add()

                    Dim rowIndex As Integer = C1Calendar.Rows.Count - 1
                    Dim ChildRowIndex As Int32 = rowIndex

                    ''''''
                    C1Calendar.Rows(ChildRowIndex).ImageAndText = True

                    strPatientname = oPatient.GetPatientName(CType(dt.Rows(i)("nPatientID"), Int64)) + " : " + Convert.ToString(dt.Rows(i)("sLocationName") + " : " + Convert.ToString(gloDateMaster.gloTime.TimeAsDateTime(Date.Today, Convert.ToInt64(dt.Rows(i)("dtStarttime"))).ToShortTimeString()) + " To " + Convert.ToString(gloDateMaster.gloTime.TimeAsDateTime(Date.Today, Convert.ToInt64(dt.Rows(i)("dtEndtime"))).ToShortTimeString()))
                   strLocation = Convert.ToString(dt.Rows(i)("sLocationName"))
                    If (Convert.ToString(dt.Rows(i)("sAppointmentType")) <> "") Then
                        strPatientname = strPatientname + " - " + Convert.ToString(dt.Rows(i)("sAppointmentType"))
                    End If
                    strtime = Convert.ToString(gloDateMaster.gloTime.TimeAsDateTime(Date.Today, Convert.ToInt64(dt.Rows(i)("dtStarttime"))).ToShortTimeString())
                    Dim dt1 As DateTime
                    dt1 = gloDateMaster.gloTime.TimeAsDateTime(Date.Today, Convert.ToInt64(dt.Rows(i)("dtStarttime"))).ToShortTimeString()

                    C1Calendar.SetData(ChildRowIndex, col_No, i)
                    C1Calendar.SetData(ChildRowIndex, col_AppId, CType(dt.Rows(i)("nMSTAppointmentID"), Int64)) '' Triage ID
                    C1Calendar.SetData(ChildRowIndex, col_AppPatientName, strPatientname) '' Patient Name
                    C1Calendar.SetData(ChildRowIndex, col_AppTime, dt1) '' Patient Name
                    C1Calendar.SetData(ChildRowIndex, col_AppLocation, strLocation) '' Patient Name
                    C1Calendar.SetData(ChildRowIndex, col_AppDate, _ApptmntDate)
                    C1Calendar.SetData(ChildRowIndex, col_AppPatientID, CType(dt.Rows(i)("nPatientID"), Int64))
                    C1Calendar.SetData(ChildRowIndex, col_AppdtlId, CType(dt.Rows(i)("nDTLAppointmentID"), Int64))
                    C1Calendar.SetData(ChildRowIndex, col_AppProviderID, CType(dt.Rows(i)("nProviderID"), Int64))
                    C1Calendar.SetData(ChildRowIndex, col_AppdtlMethod, CType(dt.Rows(i)("DTLAppMethod"), Int64))
                    C1Calendar.SetData(ChildRowIndex, col_AppMstMethod, CType(dt.Rows(i)("MSTAppMethod"), Int64))
                    C1Calendar.SetData(ChildRowIndex, col_AppDepartmentname, CType(dt.Rows(i)("sDepartmentName"), String))
                    C1Calendar.SetData(ChildRowIndex, col_AppUsedStatus, CType(dt.Rows(i)("nUsedStatus"), Int64))



                    Dim dtENV As DataTable ''slr new not needed
                    Dim cls As New clsDoctorsDashBoard
                    dtENV = cls.Get_Patient_ClinicEnvironment(CType(dt.Rows(i)("nPatientID"), Int64))
                    cls = Nothing

                    If IsNothing(dtENV) = False Then
                        'SLR: To be changed on 8/5/2014
                        '' Set Patient's Clinical Environment Color Codes
                        For j As Integer = 0 To dtENV.Rows.Count - 1
                            Dim rg As C1.Win.C1FlexGrid.CellRange
                            Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing '= C1Calendar.Styles.Add("ENV" & i & "" & j)
                            Dim myString As String = "ENV" & rowIndex.ToString("D5") & "" & j.ToString("D5")
                            If dtENV.Rows(j)("nColor") <> -1 Then


                                Try
                                    If (C1Calendar.Styles.Contains(myString)) Then
                                        cStyle = C1Calendar.Styles(myString)
                                    Else
                                        cStyle = C1Calendar.Styles.Add(myString)
                                        cStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New Font("Tahoma", 8.25F, FontStyle.Bold)

                                    End If
                                Catch ex As Exception
                                    cStyle = C1Calendar.Styles.Add(myString)
                                    cStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New Font("Tahoma", 8.25F, FontStyle.Bold)

                                End Try

                                cStyle.ForeColor = Color.FromArgb(dtENV.Rows(j)("nColor"))
                                cStyle.BackColor = Color.FromArgb(dtENV.Rows(j)("nColor")) ''Color.White
                                cStyle.TextEffect = TextEffectEnum.Flat
                                cStyle.Border.Style = BorderStyleEnum.Flat
                                cStyle.TextAlign = TextAlignEnum.CenterCenter
                            Else
                                cStyle = Nothing
                            End If

                            Select Case dtENV.Rows(j)("nEnvironment")
                                Case 1
                                    If IsNothing(cStyle) = False Then
                                        rg = C1Calendar.GetCellRange(ChildRowIndex, col_color0, ChildRowIndex, col_color0)
                                        rg.Style = cStyle
                                        'C1Calendar.SetData(ChildRowIndex, col_color0, "*")
                                    Else
                                        Dim Style As C1.Win.C1FlexGrid.CellStyle '= C1Calendar.Styles.Add("cs_StatusColor" & col_color0 & ChildRowIndex)
                                        Dim myColorString As String = "cs_StatusColor" & col_color0.ToString("D5") & ChildRowIndex.ToString("D5")

                                        Try
                                            If (C1Calendar.Styles.Contains(myColorString)) Then
                                                Style = C1Calendar.Styles(myColorString)
                                            Else
                                                Style = C1Calendar.Styles.Add(myColorString)
                                               
                                            End If
                                        Catch ex As Exception
                                            Style = C1Calendar.Styles.Add(myColorString)
                                      
                                        End Try
                                        Style.BackColor = NormalCellBackColor
                                        Style.ForeColor = NormalCellBackColor
                                        C1Calendar.SetCellStyle(ChildRowIndex, col_color0, Style)
                                    End If
                                Case 2
                                    If IsNothing(cStyle) = False Then

                                        rg = C1Calendar.GetCellRange(ChildRowIndex, col_color1, ChildRowIndex, col_color1)
                                        rg.Style = cStyle
                                        'C1Calendar.SetData(ChildRowIndex, col_color1, "*")
                                    Else
                                        Dim Style As C1.Win.C1FlexGrid.CellStyle '= C1Calendar.Styles.Add("cs_StatusColor" & col_color1 & ChildRowIndex)
                                        Dim myColorString As String = "cs_StatusColor" & col_color1.ToString("D5") & ChildRowIndex.ToString("D5")

                                        Try
                                            If (C1Calendar.Styles.Contains(myColorString)) Then
                                                Style = C1Calendar.Styles(myColorString)
                                            Else
                                                Style = C1Calendar.Styles.Add(myColorString)
                                              
                                            End If
                                        Catch ex As Exception
                                            Style = C1Calendar.Styles.Add(myColorString)
                                            
                                        End Try
                                        Style.BackColor = NormalCellBackColor
                                        Style.ForeColor = NormalCellBackColor
                                        C1Calendar.SetCellStyle(ChildRowIndex, col_color1, Style)
                                    End If
                                Case 3
                                    If IsNothing(cStyle) = False Then

                                        rg = C1Calendar.GetCellRange(ChildRowIndex, col_color2, ChildRowIndex, col_color2)
                                        rg.Style = cStyle
                                        ' C1Calendar.SetData(ChildRowIndex, col_color2, "*")
                                    Else
                                        Dim Style As C1.Win.C1FlexGrid.CellStyle '= C1Calendar.Styles.Add("cs_StatusColor" & col_color2 & ChildRowIndex)
                                        Dim myColorString As String = "cs_StatusColor" & col_color2.ToString("D5") & ChildRowIndex.ToString("D5")

                                        Try
                                            If (C1Calendar.Styles.Contains(myColorString)) Then
                                                Style = C1Calendar.Styles(myColorString)
                                            Else
                                                Style = C1Calendar.Styles.Add(myColorString)
                                            
                                            End If
                                        Catch ex As Exception
                                            Style = C1Calendar.Styles.Add(myColorString)
                                           
                                        End Try
                                        Style.BackColor = NormalCellBackColor
                                        Style.ForeColor = NormalCellBackColor
                                        C1Calendar.SetCellStyle(ChildRowIndex, col_color2, Style)
                                    End If
                                Case 4
                                    If IsNothing(cStyle) = False Then

                                        rg = C1Calendar.GetCellRange(ChildRowIndex, col_color3, ChildRowIndex, col_color3)
                                        rg.Style = cStyle
                                        ' C1Calendar.SetData(ChildRowIndex, col_color3, "*")
                                    Else
                                        Dim Style As C1.Win.C1FlexGrid.CellStyle '= C1Calendar.Styles.Add("cs_StatusColor" & col_color3 & ChildRowIndex)
                                        Dim myColorString As String = "cs_StatusColor" & col_color3.ToString("D5") & ChildRowIndex.ToString("D5")

                                        Try
                                            If (C1Calendar.Styles.Contains(myColorString)) Then
                                                Style = C1Calendar.Styles(myColorString)
                                            Else
                                                Style = C1Calendar.Styles.Add(myColorString)
                                              
                                            End If
                                        Catch ex As Exception
                                            Style = C1Calendar.Styles.Add(myColorString)
                                            
                                        End Try
                                        Style.BackColor = Color.FromArgb(240, 247, 255)
                                        Style.ForeColor = NormalCellBackColor
                                        C1Calendar.SetCellStyle(ChildRowIndex, col_color3, Style)
                                    End If
                                Case 5
                                    If IsNothing(cStyle) = False Then

                                        rg = C1Calendar.GetCellRange(ChildRowIndex, col_color4, ChildRowIndex, col_color4)
                                        rg.Style = cStyle
                                        ' C1Calendar.SetData(ChildRowIndex, col_color4, "*")
                                    Else
                                        Dim Style As C1.Win.C1FlexGrid.CellStyle '= C1Calendar.Styles.Add("cs_StatusColor" & col_color4 & ChildRowIndex)
                                        Dim myColorString As String = "cs_StatusColor" & col_color4.ToString("D5") & ChildRowIndex.ToString("D5")

                                        Try
                                            If (C1Calendar.Styles.Contains(myColorString)) Then
                                                Style = C1Calendar.Styles(myColorString)
                                            Else
                                                Style = C1Calendar.Styles.Add(myColorString)
                                             
                                            End If
                                        Catch ex As Exception
                                            Style = C1Calendar.Styles.Add(myColorString)
                                             
                                        End Try
                                        Style.BackColor = NormalCellBackColor
                                        Style.ForeColor = NormalCellBackColor
                                        C1Calendar.SetCellStyle(ChildRowIndex, col_color4, Style)
                                    End If
                                Case 6
                                    If IsNothing(cStyle) = False Then

                                        rg = C1Calendar.GetCellRange(ChildRowIndex, col_color5, ChildRowIndex, col_color5)
                                        rg.Style = cStyle
                                        ' C1Calendar.SetData(ChildRowIndex, col_color5, "*")
                                    Else
                                        Dim Style As C1.Win.C1FlexGrid.CellStyle '= C1Calendar.Styles.Add("cs_StatusColor" & col_color5 & ChildRowIndex)
                                        Dim myColorString As String = "cs_StatusColor" & col_color5.ToString("D5") & ChildRowIndex.ToString("D5")

                                        Try
                                            If (C1Calendar.Styles.Contains(myColorString)) Then
                                                Style = C1Calendar.Styles(myColorString)
                                            Else
                                                Style = C1Calendar.Styles.Add(myColorString)
                                             
                                            End If
                                        Catch ex As Exception
                                            Style = C1Calendar.Styles.Add(myColorString)
                                          
                                        End Try
                                        Style.BackColor = NormalCellBackColor
                                        Style.ForeColor = NormalCellBackColor
                                        C1Calendar.SetCellStyle(ChildRowIndex, col_color5, Style)
                                    End If


                            End Select

                            cStyle = Nothing
                            rg = Nothing
                        Next
                    End If
                    ''slr free dtenv
                    If Not IsNothing(dtENV) Then
                        dtENV.Dispose()
                        dtENV = Nothing
                    End If
                    Dim rStyle As C1.Win.C1FlexGrid.CellRange
                    Dim csAppointment As C1.Win.C1FlexGrid.CellStyle '= C1Calendar.Styles.Add("cs_Child")
                    Try
                        If (C1Calendar.Styles.Contains("cs_Child")) Then
                            csAppointment = C1Calendar.Styles("cs_Child")
                        Else
                            csAppointment = C1Calendar.Styles.Add("cs_Child")
                            csAppointment.Font = gloGlobal.clsgloFont.gFont_SMALL 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                            csAppointment.BackColor = NormalCellBackColor
                            csAppointment.ForeColor = Color.FromArgb(31, 73, 125)
                            csAppointment.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
                            csAppointment.ImageSpacing = 2
                            csAppointment.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
                        End If
                    Catch ex As Exception
                        csAppointment = C1Calendar.Styles.Add("cs_Child")
                        csAppointment.Font = gloGlobal.clsgloFont.gFont_SMALL 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                        csAppointment.BackColor = NormalCellBackColor
                        csAppointment.ForeColor = Color.FromArgb(31, 73, 125)
                        csAppointment.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
                        csAppointment.ImageSpacing = 2
                        csAppointment.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
                    End Try
                

                    rStyle = C1Calendar.GetCellRange(ChildRowIndex, col_No, ChildRowIndex, col_AppPatientID + 1)
                    rStyle.Style = csAppointment
                Next
            Else
                C1Calendar.Rows.RemoveRange(0, C1Calendar.Rows.Count)
            End If

            '' C1Calendar.Cols(5).Style = csProviderName

           


            C1Calendar.Cols(col_No).Visible = False
            C1Calendar.Cols(col_AppId).Visible = False
            C1Calendar.Cols(col_AppDate).Visible = False
            C1Calendar.Cols(col_AppPatientName).Visible = True
            C1Calendar.Cols(col_AppTime).Visible = True
            C1Calendar.Cols(col_AppLocation).Visible = True
            C1Calendar.Cols(col_AppPatientID).Visible = False
            C1Calendar.Cols(col_AppProviderID).Visible = False  '' ProviderID

            C1Calendar.Cols(col_AppdtlMethod).Visible = False
            C1Calendar.Cols(col_AppMstMethod).Visible = False
            C1Calendar.Cols(col_AppDepartmentname).Visible = False
            C1Calendar.Cols(col_AppUsedStatus).Visible = False



            C1Calendar.Cols(col_color1).Visible = True  '' color
            C1Calendar.Cols(col_color2).Visible = True
            C1Calendar.Cols(col_color3).Visible = True
            C1Calendar.Cols(col_color4).Visible = True
            C1Calendar.Cols(col_color5).Visible = True

            Dim _width As Integer = C1Calendar.Width - 68
            C1Calendar.Cols(col_AppDate).Width = 0 '' Date
            C1Calendar.Cols(col_No).Width = 0 '' NO
            C1Calendar.Cols(col_AppId).Width = 0 '' Id
            C1Calendar.Cols(col_AppPatientID).Width = 0 ''Patient Id

            ''C1Calendar.Cols(col_AppTime).Width = Convert.ToInt32(_width * 0.15)
            C1Calendar.Cols(col_AppTime).Width = 60
            C1Calendar.Cols(col_AppLocation).Width = Convert.ToInt32(_width * 0.29)

            C1Calendar.Cols(col_AppdtlId).Visible = False '' Provider Name 
            C1Calendar.Cols(col_AppPatientName).Width = Convert.ToInt32(_width * 0.29) ''Patient Name
            C1Calendar.Cols(col_AppdtlId).Width = 0            'End If
            C1Calendar.Cols(col_AppProviderID).Width = 0 ''ProviderID

            C1Calendar.Cols(col_AppdtlMethod).Width = 0
            C1Calendar.Cols(col_AppMstMethod).Width = 0
            C1Calendar.Cols(col_AppDepartmentname).Width = 0
            C1Calendar.Cols(col_AppUsedStatus).Width = 0


            C1Calendar.Cols(col_color0).Width = 9 ''Colorcode
            C1Calendar.Cols(col_color1).Width = 9 ''Colorcode
            C1Calendar.Cols(col_color2).Width = 9 ''Colorcode
            C1Calendar.Cols(col_color3).Width = 9 ''Colorcode
            C1Calendar.Cols(col_color4).Width = 9 ''Colorcode
            C1Calendar.Cols(col_color5).Width = 9 ''Colorcode

            C1Calendar.Cols(col_AppTime).TextAlign = TextAlignEnum.RightCenter



            C1Calendar.Cols(col_color0).AllowResizing = False  '' color
            C1Calendar.Cols(col_color1).AllowResizing = False
            C1Calendar.Cols(col_color2).AllowResizing = False
            C1Calendar.Cols(col_color3).AllowResizing = False
            C1Calendar.Cols(col_color4).AllowResizing = False
            C1Calendar.Cols(col_color5).AllowResizing = False
            C1Calendar.Cols(col_AppTime).AllowResizing = False


            '' start Merge header for all color columns 
            '  Dim colstyle As C1.Win.C1FlexGrid.CellRange
            Dim cscolstyle As C1.Win.C1FlexGrid.CellStyle '= C1Calendar.Styles.Add("cs")
            Try
                If (C1Calendar.Styles.Contains("cs")) Then
                    cscolstyle = C1Calendar.Styles("cs")
                Else
                    cscolstyle = C1Calendar.Styles.Add("cs")
                    cscolstyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                    cscolstyle.BackColor = Color.FromArgb(86, 126, 211)
                    cscolstyle.ForeColor = Color.White
                    cscolstyle.Border.Color = Color.FromArgb(159, 181, 221)
                    cscolstyle.Border.Width = 1
                    cscolstyle.TextAlign = TextAlignEnum.CenterCenter
                End If
            Catch ex As Exception
                cscolstyle = C1Calendar.Styles.Add("cs")
                cscolstyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                cscolstyle.BackColor = Color.FromArgb(86, 126, 211)
                cscolstyle.ForeColor = Color.White
                cscolstyle.Border.Color = Color.FromArgb(159, 181, 221)
                cscolstyle.Border.Width = 1
                cscolstyle.TextAlign = TextAlignEnum.CenterCenter
            End Try

            C1Calendar.AllowMerging = AllowMergingEnum.Custom
            Dim cs As CellRange = C1Calendar.GetCellRange(0, col_color0, 0, col_color5)
            cs.Data = " Status "
            cs.Style = cscolstyle

            C1Calendar.MergedRanges.Add(cs, False)
            Dim cs1 As CellRange = C1Calendar.GetCellRange(0, col_No, 0, col_AppUsedStatus)
            cs1.Style = cscolstyle


            '' End Merge header for all color columns 



            C1Calendar.SelectionMode = SelectionModeEnum.Cell

            C1Calendar.AllowDragging = False
            C1Calendar.AllowSorting = AllowSortingEnum.SingleColumn
            C1Calendar.Row = -1
            C1Calendar.Col = -1
            'SLR: Free oPatient, oclsdashboard
            If Not IsNothing(oPatient) Then
                oPatient.Dispose()
                oPatient = Nothing
            End If
            
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            C1Calendar.ScrollBars = ScrollBars.Both
            If IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If


        End Try
    End Sub
    ''Modified by Mayuri:20110316-for optimization-used stored procedure to retrieve data

    Private Sub ShowAppointments()
        Dim dt As DataTable = Nothing ''slr new not needed 
        Try
            C1Calendar.ScrollBars = ScrollBars.None
            'C1Calendar.Clear()
            C1Calendar.DataSource = Nothing

            C1Calendar.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns
            C1Calendar.Clear(C1.Win.C1FlexGrid.ClearFlags.All)

            C1Calendar.Cols.Count = col_Count ''Sandip Darade  20100308
            C1Calendar.Rows.Count = 1
            C1Calendar.AllowEditing = False
            C1Calendar.Styles.Focus.BackColor = Color.FromArgb(255, 224, 160)

            Dim NormalCellBackColor As Color = Color.FromArgb(240, 247, 255)



            C1Calendar.SetData(0, col_No, "No.")
            C1Calendar.SetData(0, col_AppId, "Appointment ID")
            C1Calendar.SetData(0, col_AppPatientName, "Patient")
            C1Calendar.SetData(0, col_AppDate, "Appointment Date")
            C1Calendar.SetData(0, col_AppPatientID, "Patient ID")
            C1Calendar.SetData(0, col_AppdtlId, "Appointment DtlID")
            C1Calendar.SetData(0, col_AppProviderID, "Provider ID")
            C1Calendar.SetData(0, col_AppTime, "Time")
            C1Calendar.SetData(0, col_AppLocation, "Location")
            C1Calendar.SetData(0, col_AppDepartmentname, "Departmentname")
            C1Calendar.SetData(0, col_AppdtlMethod, "Method DTL")
            C1Calendar.SetData(0, col_AppMstMethod, "Method MST")
            C1Calendar.SetData(0, col_AppUsedStatus, "Status")


            Dim rrDueDate As New ArrayList ''Used to Store DueDates and sort it afterwards
            Dim i As Integer
            dt = Nothing
            dt = GetAppointmentsForUser()

            '  Dim oPatient As New gloPatient.gloPatient(GetConnectionString)  slr not used
            '  Dim oClsDashBoard As New clsDoctorsDashBoard slr not used 

            Dim strPatientname As String = "" '' This string contains: Patient Name
            Dim strLocation As String = "" '' This string contains: Patient Name
            Dim strtime As String = "" '' This string contains: Patient Name
            ' Dim _PatientID As Int64
            'Dim _PatientName As String
            C1Calendar.Cols(col_AppTime).Format = "t"
            Dim ParentRowIndex As Int32 = 0

            If IsNothing(dt) = False Then
                For i = 0 To dt.Rows.Count - 1
                    'If oTriages(i).IsFinished = False Then
                    Dim _ApptmntDate As DateTime = Convert.ToDateTime(gloDateMaster.gloDate.DateAsDate(dt.Rows(i)("dtEndDate")))
                    Dim _ApptmntDateString As String = " Date : " + Format(_ApptmntDate, "MM/dd/yyyy")

                    C1Calendar.Rows.Add()

                    Dim rowIndex As Integer = C1Calendar.Rows.Count - 1
                    Dim ChildRowIndex As Int32 = rowIndex

                    ''''''
                    C1Calendar.Rows(ChildRowIndex).ImageAndText = True

                  
                    strPatientname = Convert.ToString(dt.Rows(i)("sPatientName"))
                    If (Convert.ToString(dt.Rows(i)("sAppointmentType")) <> "") Then
                        strPatientname = strPatientname + " - " + Convert.ToString(dt.Rows(i)("sAppointmentType"))
                    End If
                    strtime = Convert.ToString(gloDateMaster.gloTime.TimeAsDateTime(Date.Today, Convert.ToInt64(dt.Rows(i)("dtStarttime"))).ToShortTimeString())
                    Dim dt1 As DateTime
                    dt1 = gloDateMaster.gloTime.TimeAsDateTime(Date.Today, Convert.ToInt64(dt.Rows(i)("dtStarttime"))).ToShortTimeString()

                    C1Calendar.SetData(ChildRowIndex, col_No, i)
                    C1Calendar.SetData(ChildRowIndex, col_AppId, CType(dt.Rows(i)("nMSTAppointmentID"), Int64)) '' Triage ID
                    C1Calendar.SetData(ChildRowIndex, col_AppPatientName, strPatientname) '' Patient Name
                    C1Calendar.SetData(ChildRowIndex, col_AppTime, dt1) '' Patient Name
                    C1Calendar.SetData(ChildRowIndex, col_AppLocation, Convert.ToString(dt.Rows(i)("sLocationName"))) '' Patient Name
                    C1Calendar.SetData(ChildRowIndex, col_AppDate, _ApptmntDate)
                    C1Calendar.SetData(ChildRowIndex, col_AppPatientID, CType(dt.Rows(i)("nPatientID"), Int64))
                    C1Calendar.SetData(ChildRowIndex, col_AppdtlId, CType(dt.Rows(i)("nDTLAppointmentID"), Int64))
                    C1Calendar.SetData(ChildRowIndex, col_AppProviderID, CType(dt.Rows(i)("nProviderID"), Int64))
                    C1Calendar.SetData(ChildRowIndex, col_AppdtlMethod, CType(dt.Rows(i)("DTLAppMethod"), Int64))
                    C1Calendar.SetData(ChildRowIndex, col_AppMstMethod, CType(dt.Rows(i)("MSTAppMethod"), Int64))
                    C1Calendar.SetData(ChildRowIndex, col_AppDepartmentname, CType(dt.Rows(i)("sDepartmentName"), String))
                    C1Calendar.SetData(ChildRowIndex, col_AppUsedStatus, CType(dt.Rows(i)("nUsedStatus"), Int64))

                    Dim dtENV As New DataTable
                    Dim cls As New clsDoctorsDashBoard
                    
                    'combined both in one stored procedure(pradeep 20101221)
                    dtENV = cls.Get_Patient_ClinicEnvironment(CType(dt.Rows(i)("nPatientID"), Int64))
                    cls = Nothing

                    If IsNothing(dtENV) = False Then

                        '' Set Patient's Clinical Environment Color Codes
                        For j As Integer = 0 To dtENV.Rows.Count - 1
                            Dim rg As C1.Win.C1FlexGrid.CellRange
                            ' Dim cStyle As C1.Win.C1FlexGrid.CellStyle '= C1Calendar.Styles.Add("ENV" & i & "" & j)
                            Dim cStyle As C1.Win.C1FlexGrid.CellStyle = Nothing '= C1Calendar.Styles.Add("ENV" & i & "" & j)
                            Dim myString As String = "ENV" & rowIndex.ToString("D5") & "" & j.ToString("D5")
                            If dtENV.Rows(j)("nColor") <> -1 Then

                                Try
                                    If (C1Calendar.Styles.Contains(myString)) Then
                                        cStyle = C1Calendar.Styles(myString)
                                    Else
                                        cStyle = C1Calendar.Styles.Add(myString)
                                      
                                        cStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New Font("Tahoma", 8.25F, FontStyle.Bold)

                                    End If
                                Catch ex As Exception
                                    cStyle = C1Calendar.Styles.Add(myString)
                                  
                                    cStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New Font("Tahoma", 8.25F, FontStyle.Bold)

                                End Try

                                cStyle.ForeColor = Color.FromArgb(dtENV.Rows(j)("nColor"))
                                cStyle.BackColor = Color.FromArgb(dtENV.Rows(j)("nColor")) ''Color.White
                                cStyle.TextEffect = TextEffectEnum.Flat
                                cStyle.Border.Style = BorderStyleEnum.Flat
                                cStyle.TextAlign = TextAlignEnum.CenterCenter
                                

                            Else
                                cStyle = Nothing
                            End If

                            Select Case dtENV.Rows(j)("nEnvironment")
                                Case 1
                                    If IsNothing(cStyle) = False Then
                                        rg = C1Calendar.GetCellRange(ChildRowIndex, col_color0, ChildRowIndex, col_color0)
                                        rg.Style = cStyle
                                        'C1Calendar.SetData(ChildRowIndex, col_color0, "*")
                                    Else
                                        '  Dim Style As C1.Win.C1FlexGrid.CellStyle '= C1Calendar.Styles.Add("cs_StatusColor" & col_color0 & ChildRowIndex)
                                        Dim Style As C1.Win.C1FlexGrid.CellStyle '= C1Calendar.Styles.Add("cs_StatusColor" & col_color0 & ChildRowIndex)
                                        Dim myColorString As String = "cs_StatusColor" & col_color0.ToString("D5") & ChildRowIndex.ToString("D5")


                                        Try
                                            If (C1Calendar.Styles.Contains(myColorString)) Then
                                                Style = C1Calendar.Styles(myColorString)
                                            Else
                                                Style = C1Calendar.Styles.Add(myColorString)
                                              
                                            End If
                                        Catch ex As Exception
                                            Style = C1Calendar.Styles.Add(myColorString)
                                      
                                        End Try
                                        Style.BackColor = NormalCellBackColor
                                        Style.ForeColor = NormalCellBackColor
                                        C1Calendar.SetCellStyle(ChildRowIndex, col_color0, Style)
                                    End If
                                Case 2
                                    If IsNothing(cStyle) = False Then

                                        rg = C1Calendar.GetCellRange(ChildRowIndex, col_color1, ChildRowIndex, col_color1)
                                        rg.Style = cStyle
                                        'C1Calendar.SetData(ChildRowIndex, col_color1, "*")
                                    Else
                                        Dim Style As C1.Win.C1FlexGrid.CellStyle '= C1Calendar.Styles.Add("cs_StatusColor" & col_color1 & ChildRowIndex)
                                        Dim myColorString As String = "cs_StatusColor" & col_color1.ToString("D5") & ChildRowIndex.ToString("D5")

                                        Try
                                            If (C1Calendar.Styles.Contains(myColorString)) Then
                                                Style = C1Calendar.Styles(myColorString)
                                            Else
                                                Style = C1Calendar.Styles.Add(myColorString)
                                              
                                            End If
                                        Catch ex As Exception
                                            Style = C1Calendar.Styles.Add(myColorString)
                                        
                                        End Try
                                        Style.BackColor = NormalCellBackColor
                                        Style.ForeColor = NormalCellBackColor
                                        C1Calendar.SetCellStyle(ChildRowIndex, col_color1, Style)
                                    End If
                                Case 3
                                    If IsNothing(cStyle) = False Then

                                        rg = C1Calendar.GetCellRange(ChildRowIndex, col_color2, ChildRowIndex, col_color2)
                                        rg.Style = cStyle
                                        ' C1Calendar.SetData(ChildRowIndex, col_color2, "*")
                                    Else
                                        Dim Style As C1.Win.C1FlexGrid.CellStyle '= C1Calendar.Styles.Add("cs_StatusColor" & col_color2 & ChildRowIndex)
                                        Dim myColorString As String = "cs_StatusColor" & col_color2.ToString("D5") & ChildRowIndex.ToString("D5")

                                        Try
                                            If (C1Calendar.Styles.Contains(myColorString)) Then
                                                Style = C1Calendar.Styles(myColorString)
                                            Else
                                                Style = C1Calendar.Styles.Add(myColorString)
                                              
                                            End If
                                        Catch ex As Exception
                                            Style = C1Calendar.Styles.Add(myColorString)
                                         
                                        End Try
                                        Style.BackColor = NormalCellBackColor
                                        Style.ForeColor = NormalCellBackColor
                                        C1Calendar.SetCellStyle(ChildRowIndex, col_color2, Style)
                                    End If
                                Case 4
                                    If IsNothing(cStyle) = False Then

                                        rg = C1Calendar.GetCellRange(ChildRowIndex, col_color3, ChildRowIndex, col_color3)
                                        rg.Style = cStyle
                                        ' C1Calendar.SetData(ChildRowIndex, col_color3, "*")
                                    Else
                                        Dim Style As C1.Win.C1FlexGrid.CellStyle '= C1Calendar.Styles.Add("cs_StatusColor" & col_color3 & ChildRowIndex)
                                        Dim myColorString As String = "cs_StatusColor" & col_color3.ToString("D5") & ChildRowIndex.ToString("D5")

                                        Try
                                            If (C1Calendar.Styles.Contains(myColorString)) Then
                                                Style = C1Calendar.Styles(myColorString)
                                            Else
                                                Style = C1Calendar.Styles.Add(myColorString)
                                              
                                            End If
                                        Catch ex As Exception
                                            Style = C1Calendar.Styles.Add(myColorString)
                                        
                                        End Try
                                        Style.BackColor = Color.FromArgb(240, 247, 255)
                                        Style.ForeColor = NormalCellBackColor
                                        C1Calendar.SetCellStyle(ChildRowIndex, col_color3, Style)
                                    End If
                                Case 5
                                    If IsNothing(cStyle) = False Then

                                        rg = C1Calendar.GetCellRange(ChildRowIndex, col_color4, ChildRowIndex, col_color4)
                                        rg.Style = cStyle
                                        ' C1Calendar.SetData(ChildRowIndex, col_color4, "*")
                                    Else
                                        Dim Style As C1.Win.C1FlexGrid.CellStyle '= C1Calendar.Styles.Add("cs_StatusColor" & col_color4 & ChildRowIndex)
                                        Dim myColorString As String = "cs_StatusColor" & col_color4.ToString("D5") & ChildRowIndex.ToString("D5")

                                        Try
                                            If (C1Calendar.Styles.Contains(myColorString)) Then
                                                Style = C1Calendar.Styles(myColorString)
                                            Else
                                                Style = C1Calendar.Styles.Add(myColorString)
                                                
                                            End If
                                        Catch ex As Exception
                                            Style = C1Calendar.Styles.Add("cs_StatusColor" & col_color4 & ChildRowIndex)
                                       
                                        End Try
                                        Style.BackColor = NormalCellBackColor
                                        Style.ForeColor = NormalCellBackColor
                                        C1Calendar.SetCellStyle(ChildRowIndex, col_color4, Style)
                                    End If
                                Case 6
                                    If IsNothing(cStyle) = False Then

                                        rg = C1Calendar.GetCellRange(ChildRowIndex, col_color5, ChildRowIndex, col_color5)
                                        rg.Style = cStyle
                                        ' C1Calendar.SetData(ChildRowIndex, col_color5, "*")
                                    Else
                                        Dim Style As C1.Win.C1FlexGrid.CellStyle '= C1Calendar.Styles.Add("cs_StatusColor" & col_color5 & ChildRowIndex)
                                        Dim myColorString As String = "cs_StatusColor" & col_color5.ToString("D5") & ChildRowIndex.ToString("D5")

                                        Try
                                            If (C1Calendar.Styles.Contains(myColorString)) Then
                                                Style = C1Calendar.Styles(myColorString)
                                            Else
                                                Style = C1Calendar.Styles.Add(myColorString)
                                           
                                            End If
                                        Catch ex As Exception
                                            Style = C1Calendar.Styles.Add(myColorString)
                                            
                                        End Try
                                        Style.BackColor = NormalCellBackColor
                                        Style.ForeColor = NormalCellBackColor
                                        C1Calendar.SetCellStyle(ChildRowIndex, col_color5, Style)
                                    End If


                            End Select

                            cStyle = Nothing
                            rg = Nothing
                        Next
                    End If

                    If Not IsNothing(dtENV) Then  ''slr free dtENV
                        dtENV.Dispose()
                        dtENV = Nothing
                    End If

                    Dim rStyle As C1.Win.C1FlexGrid.CellRange
                    Dim csAppointment As C1.Win.C1FlexGrid.CellStyle '= C1Calendar.Styles.Add("cs_Child")
                    Try
                        If (C1Calendar.Styles.Contains("cs_Child")) Then
                            csAppointment = C1Calendar.Styles("cs_Child")
                        Else
                            csAppointment = C1Calendar.Styles.Add("cs_Child")
                            csAppointment.Font = gloGlobal.clsgloFont.gFont_SMALL 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                            csAppointment.BackColor = NormalCellBackColor
                            csAppointment.ForeColor = Color.FromArgb(31, 73, 125)
                            csAppointment.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
                            csAppointment.ImageSpacing = 2
                            csAppointment.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
                        End If
                    Catch ex As Exception
                        csAppointment = C1Calendar.Styles.Add("cs_Child")
                        csAppointment.Font = gloGlobal.clsgloFont.gFont_SMALL 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                        csAppointment.BackColor = NormalCellBackColor
                        csAppointment.ForeColor = Color.FromArgb(31, 73, 125)
                        csAppointment.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
                        csAppointment.ImageSpacing = 2
                        csAppointment.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
                    End Try
                    

                    rStyle = C1Calendar.GetCellRange(ChildRowIndex, col_No, ChildRowIndex, col_AppPatientID + 1)
                    rStyle.Style = csAppointment
                Next
            Else
                C1Calendar.Rows.RemoveRange(0, C1Calendar.Rows.Count)
            End If

            '' C1Calendar.Cols(5).Style = csProviderName



            C1Calendar.Cols(col_No).Visible = False
            C1Calendar.Cols(col_AppId).Visible = False
            C1Calendar.Cols(col_AppDate).Visible = False
            C1Calendar.Cols(col_AppPatientName).Visible = True
            C1Calendar.Cols(col_AppTime).Visible = True
            C1Calendar.Cols(col_AppLocation).Visible = True
            C1Calendar.Cols(col_AppPatientID).Visible = False
            C1Calendar.Cols(col_AppProviderID).Visible = False  '' ProviderID

            C1Calendar.Cols(col_AppdtlMethod).Visible = False
            C1Calendar.Cols(col_AppMstMethod).Visible = False
            C1Calendar.Cols(col_AppDepartmentname).Visible = False
            C1Calendar.Cols(col_AppUsedStatus).Visible = False



            C1Calendar.Cols(col_color1).Visible = True  '' color
            C1Calendar.Cols(col_color2).Visible = True
            C1Calendar.Cols(col_color3).Visible = True
            C1Calendar.Cols(col_color4).Visible = True
            C1Calendar.Cols(col_color5).Visible = True

            Dim _width As Integer = C1Calendar.Width - 68
            C1Calendar.Cols(col_AppDate).Width = 0 '' Date
            C1Calendar.Cols(col_No).Width = 0 '' NO
            C1Calendar.Cols(col_AppId).Width = 0 '' Id
            C1Calendar.Cols(col_AppPatientID).Width = 0 ''Patient Id

            ''C1Calendar.Cols(col_AppTime).Width = Convert.ToInt32(_width * 0.15)
            C1Calendar.Cols(col_AppTime).Width = 60
            C1Calendar.Cols(col_AppLocation).Width = Convert.ToInt32(_width * 0.29)

            C1Calendar.Cols(col_AppdtlId).Visible = False '' Provider Name 
            C1Calendar.Cols(col_AppPatientName).Width = Convert.ToInt32(_width * 0.29) ''Patient Name
            C1Calendar.Cols(col_AppdtlId).Width = 0            'End If
            C1Calendar.Cols(col_AppProviderID).Width = 0 ''ProviderID

            C1Calendar.Cols(col_AppdtlMethod).Width = 0
            C1Calendar.Cols(col_AppMstMethod).Width = 0
            C1Calendar.Cols(col_AppDepartmentname).Width = 0
            C1Calendar.Cols(col_AppUsedStatus).Width = 0


            C1Calendar.Cols(col_color0).Width = 9 ''Colorcode
            C1Calendar.Cols(col_color1).Width = 9 ''Colorcode
            C1Calendar.Cols(col_color2).Width = 9 ''Colorcode
            C1Calendar.Cols(col_color3).Width = 9 ''Colorcode
            C1Calendar.Cols(col_color4).Width = 9 ''Colorcode
            C1Calendar.Cols(col_color5).Width = 9 ''Colorcode

            C1Calendar.Cols(col_AppTime).TextAlign = TextAlignEnum.RightCenter



            C1Calendar.Cols(col_color0).AllowResizing = False  '' color
            C1Calendar.Cols(col_color1).AllowResizing = False
            C1Calendar.Cols(col_color2).AllowResizing = False
            C1Calendar.Cols(col_color3).AllowResizing = False
            C1Calendar.Cols(col_color4).AllowResizing = False
            C1Calendar.Cols(col_color5).AllowResizing = False
            C1Calendar.Cols(col_AppTime).AllowResizing = False


            '' start Merge header for all color columns 
            ' Dim colstyle As C1.Win.C1FlexGrid.CellRange
            Dim cscolstyle As C1.Win.C1FlexGrid.CellStyle '= C1Calendar.Styles.Add("cs")
            Try
                If (C1Calendar.Styles.Contains("cs")) Then
                    cscolstyle = C1Calendar.Styles("cs")
                Else
                    cscolstyle = C1Calendar.Styles.Add("cs")
                    cscolstyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                    cscolstyle.BackColor = Color.FromArgb(86, 126, 211)
                    cscolstyle.ForeColor = Color.White
                    cscolstyle.Border.Color = Color.FromArgb(159, 181, 221)
                    cscolstyle.Border.Width = 1
                    cscolstyle.TextAlign = TextAlignEnum.CenterCenter
                End If
            Catch ex As Exception
                cscolstyle = C1Calendar.Styles.Add("cs")
                cscolstyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                cscolstyle.BackColor = Color.FromArgb(86, 126, 211)
                cscolstyle.ForeColor = Color.White
                cscolstyle.Border.Color = Color.FromArgb(159, 181, 221)
                cscolstyle.Border.Width = 1
                cscolstyle.TextAlign = TextAlignEnum.CenterCenter
            End Try
           
            C1Calendar.AllowMerging = AllowMergingEnum.Custom
            Dim cs As CellRange = C1Calendar.GetCellRange(0, col_color0, 0, col_color5)
            cs.Data = " Status "
            cs.Style = cscolstyle

            C1Calendar.MergedRanges.Add(cs, False)
            Dim cs1 As CellRange = C1Calendar.GetCellRange(0, col_No, 0, col_AppUsedStatus)
            cs1.Style = cscolstyle


            '' End Merge header for all color columns 



            C1Calendar.SelectionMode = SelectionModeEnum.Cell

            C1Calendar.AllowDragging = False
            C1Calendar.AllowSorting = AllowSortingEnum.SingleColumn
            C1Calendar.Row = -1
            C1Calendar.Col = -1
          
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            C1Calendar.ScrollBars = ScrollBars.Both
            If IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Sub
    Private Sub FillCalendar()
        Dim dt As DataTable = GetAppointments()
        C1Calendar.ScrollBars = ScrollBars.None
        Try

            With C1Calendar

                Dim i As Int16
                .Dock = DockStyle.Fill

                .Cols.Count = COL_Cal_COUNT
                .AllowEditing = False

                .Styles.ClearUnused()

                .Cols(Col_AppointmentDate).Width = .Width * 0
                .Cols(COL_MSTAppointmentID).Width = .Width * 0
                .Cols(COL_DayName).Width = .Width * 0.3
                .Cols(COL_Description).Width = .Width * 0.68
                .Cols(COL_ProviderName).Width = .Width * 0
                .Cols(COL_PatientName).Width = 0
                .Cols(COL_DTLAppointmentID).Width = 0
                .Cols(COL_ProviderID).Width = 0
                .Cols(COL_LocationName).Width = 0
                .Cols(COL_DepartmentName).Width = 0
                .Cols(COL_AppointmentTypeDesc).Width = 0
                .Cols(COL_UsedStatus).Width = 0
                .Cols(COL_DTLAppMethod).Width = 0
                .Cols(COL_MSTAppMethod).Width = 0
                .Cols(COL_LineNumber).Width = 0
                .Cols(COL_StartTime).Width = 0
                .Cols(COL_EndTime).Width = 0

                .Cols(Col_AppointmentDate).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_MSTAppointmentID).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_DayName).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_Description).TextAlignFixed = TextAlignEnum.CenterCenter
                .Cols(COL_ProviderName).TextAlignFixed = TextAlignEnum.CenterCenter

                For i = 0 To dt.Rows.Count - 1
                    '' Current Date Logic Start ''
                    '' CHECK WHETHER TODAY HAS APPOINTMENT OR NOT.. IF NOT.. THEN EXIT FOR ''
                    If i = 0 And isShowAllDays = False And dt.Rows(i)("DayName") <> "Today" Then
                        Exit For
                    End If
                    '' CHECK WHETHER TODAY'S APPOINTMENTS HAS FILLED IN GRID..IF ALL FILLED .. DON'T LET FILL NEXT DAY'S APPOINTMENTS.
                    If i > 0 And isShowAllDays = False Then
                        If dt.Rows(i)("MSTAppointmentID") = 0 Then
                            Exit For
                        End If
                    End If
                    '' Current Date Logic End ''
                    .Rows.Add()
                    Dim lastRow As Integer = C1Calendar.Rows.Count - 1

                    .SetData(lastRow, COL_MSTAppointmentID, dt.Rows(i)("MSTAppointmentID"))
                    .SetData(lastRow, Col_AppointmentDate, dt.Rows(i)("AppointmentDate"))
                    .SetData(lastRow, COL_ProviderName, dt.Rows(i)("ProviderName"))
                    .SetData(lastRow, COL_PatientName, dt.Rows(i)("PatientName"))

                    If dt.Rows(i)("MSTAppointmentID") = 0 Then
                        .SetData(lastRow, COL_DayName, dt.Rows(i)("DayName"))
                        .SetCellStyle(lastRow, COL_DayName, "csCalendar")
                        .SetCellStyle(lastRow, COL_Description, "csCalendar")
                    Else
                        Dim AppointmentTime As String = dt.Rows(i)("StartTime") & " - " & dt.Rows(i)("EndTime")
                        Dim description As String = dt.Rows(i)("PatientName") + " : " + dt.Rows(i)("AppointmentTypeDesc") + " : " + dt.Rows(i)("Description") + " : " + dt.Rows(i)("LocationName")
                        .SetData(lastRow, COL_DayName, AppointmentTime)
                        .SetData(lastRow, COL_Description, description)

                        .SetCellStyle(lastRow, COL_DayName, "csAppointment")
                        .SetCellStyle(lastRow, COL_Description, "csAppointment")
                    End If

                    .SetData(lastRow, COL_DTLAppointmentID, dt.Rows(i)("DTLAppointmentID"))
                    .SetData(lastRow, COL_ProviderID, dt.Rows(i)("ProviderID"))
                    .SetData(lastRow, COL_LocationName, dt.Rows(i)("LocationName"))
                    .SetData(lastRow, COL_DepartmentName, dt.Rows(i)("DepartmentName"))
                    .SetData(lastRow, COL_AppointmentTypeDesc, dt.Rows(i)("AppointmentTypeDesc"))
                    .SetData(lastRow, COL_UsedStatus, dt.Rows(i)("UsedStatus"))
                    .SetData(lastRow, COL_DTLAppMethod, dt.Rows(i)("DTLAppMethod"))
                    .SetData(lastRow, COL_MSTAppMethod, dt.Rows(i)("MSTAppMethod"))
                    .SetData(lastRow, COL_LineNumber, dt.Rows(i)("LineNumber"))
                    .SetData(lastRow, COL_StartTime, dt.Rows(i)("StartTime"))
                    .SetData(lastRow, COL_EndTime, dt.Rows(i)("EndTime"))
                Next
            End With

            C1Calendar.Row = -1 '' TO DISSELECT ROW
            If Not IsNothing(dt) Then ''slr free dt
                dt.Dispose()
                dt = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        C1Calendar.ScrollBars = ScrollBars.Both
    End Sub

    Private Sub C1Calendar_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Calendar.MouseDoubleClick
       
        Try
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo  ''slr new not needed 
            htInfo = C1Calendar.HitTest(e.X, e.Y)
            If htInfo.Row > 0 Then

                If Convert.ToInt64(C1Calendar.GetData(htInfo.Row, col_AppdtlId)) <= 0 Then
                    Exit Sub
                End If

                If MainMenu.IsAccess(False, Convert.ToInt64(C1Calendar.GetData(htInfo.Row, col_AppPatientID))) = False Then
                    Exit Sub
                Else
                    Dim oAppParameters As New gloAppointmentScheduling.SetAppointmentParameter()
                    Dim oSetupAppointment As New gloAppointmentScheduling.frmSetupAppointment(GetConnectionString())

                    Dim _AppStatus As gloAppointmentScheduling.ASUsedStatus = gloAppointmentScheduling.ASUsedStatus.Unknown3
                    _AppStatus = DirectCast(Convert.ToInt32(C1Calendar.GetData(htInfo.Row, col_AppUsedStatus)), gloAppointmentScheduling.ASUsedStatus)
                    If _AppStatus = gloAppointmentScheduling.ASUsedStatus.CheckOut Then

                        If Convert.ToDateTime(C1Calendar.GetData(htInfo.Row, col_AppDate)).[Date] < DateTime.Now.[Date] Then
                            MessageBox.Show("Cannot modify past appointment.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return
                        Else
                            If MessageBox.Show("Patient '" & C1Calendar.GetData(htInfo.Row, col_AppPatientName) & "' is already checked out. Are you sure you wish to modify this appointment?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Else
                                Return
                            End If
                        End If

                    End If

                    If Convert.ToDateTime(C1Calendar.GetData(htInfo.Row, col_AppDate)).[Date] < DateTime.Now.[Date] Then
                        MessageBox.Show("Cannot modify past appointment.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return
                    End If

                    '' Find for Single/Recurrence Option ''
                    Dim _SinRecCriteria As gloAppointmentScheduling.SingleRecurrence = gloAppointmentScheduling.SingleRecurrence.[Single]
                    ''                _SinRecCriteria = DirectCast(C1Calendar.GetData(htInfo.Row, col_AppdtlMethod), gloAppointmentScheduling.SingleRecurrence)
                    _SinRecCriteria = CType(C1Calendar.GetData(htInfo.Row, col_AppdtlMethod), gloAppointmentScheduling.SingleRecurrence)
                    'SelectedForModDel

                    Dim dt As DataTable ''slr new not needed 
                    Dim dtappdatetime As DataTable ''slr new not needed 
                    Dim _resourceID As Int64
                    Dim Starttime As DateTime
                    Dim Endtime As DateTime
                    Dim StartDate As DateTime
                    Dim ogloAppointment As gloAppointmentScheduling.gloAppointment = New gloAppointmentScheduling.gloAppointment(GetConnectionString())

                    _resourceID = Convert.ToInt64(C1Calendar.GetData(htInfo.Row, col_AppProviderID))
                    dtappdatetime = ogloAppointment.Appointmentdatetime(Convert.ToInt64(C1Calendar.GetData(htInfo.Row, col_AppId)))
                    StartDate = gloDateMaster.gloDate.DateAsDate(Convert.ToDouble(dtappdatetime.Rows(0)("dtStartDate")))
                    Starttime = Convert.ToDateTime(dtappdatetime.Rows(0)("dtStartTime"))
                    Endtime = Convert.ToDateTime(dtappdatetime.Rows(0)("dtEndTime"))
                    dt = ogloAppointment.ResourseName(_resourceID, Starttime, Endtime, StartDate)
                    If Not IsNothing(dt) Then
                        If (dt.Rows.Count > 0) Then

                            If (DialogResult.No = MessageBox.Show("Schedule for " + dt.Rows(0)("sASBaseDesc") + " is blocked from " & dt.Rows(0)("dtStarttime") + " to " + dt.Rows(0)("dtEndtime") + "." + vbCrLf + "Modify this " + Starttime.ToShortTimeString() + " - " + Endtime.ToShortTimeString() + " appointment? ", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)) Then
                                Exit Sub
                            End If
                        End If
                    End If

                    oAppParameters.AddTrue_ModifyFalse_Flag = False
                    If _SinRecCriteria = gloAppointmentScheduling.SingleRecurrence.[Single] Then
                        oAppParameters.ModifyAppointmentMethod = gloAppointmentScheduling.SingleRecurrence.[Single]
                        oAppParameters.ModifyMasterAppointmentMethod = gloAppointmentScheduling.SingleRecurrence.[Single]
                        oAppParameters.ModifySingleAppointmentFromReccurence = False
                    Else

                        oAppParameters.ModifyAppointmentMethod = gloAppointmentScheduling.SingleRecurrence.[Single]
                        oAppParameters.ModifyMasterAppointmentMethod = gloAppointmentScheduling.SingleRecurrence.Recurrence

                        oAppParameters.ModifySingleAppointmentFromReccurence = True
                    End If

                    '' Set Appointment Parameter ''
                    oAppParameters.MasterAppointmentID = Convert.ToInt64(C1Calendar.GetData(htInfo.Row, col_AppId))
                    oAppParameters.AppointmentID = Convert.ToInt64(C1Calendar.GetData(htInfo.Row, col_AppdtlId))
                    oAppParameters.AppointmentFlag = gloAppointmentScheduling.AppointmentScheduleFlag.Appointment
                    oAppParameters.AppointmentTypeID = 0
                    oAppParameters.AppointmentTypeCode = ""
                    oAppParameters.AppointmentTypeDesc = ""
                    oAppParameters.ProviderID = Convert.ToInt64(C1Calendar.GetData(htInfo.Row, col_AppProviderID))
                    ''oAppParameters.ProviderName = Convert.ToString(C1Calendar.GetData(htInfo.Row, col_AppProviderID))
                    oAppParameters.ProblemTypes = Nothing
                    oAppParameters.Resources = Nothing
                    oAppParameters.PatientID = 0

                    oAppParameters.Location = Convert.ToString(C1Calendar.GetData(htInfo.Row, col_AppLocation))
                    oAppParameters.Department = Convert.ToString(C1Calendar.GetData(htInfo.Row, col_AppDepartmentname))
                    oAppParameters.StartDate = Convert.ToDateTime(C1Calendar.GetData(htInfo.Row, col_AppDate))
                    oAppParameters.StartTime = Convert.ToDateTime(C1Calendar.GetData(htInfo.Row, col_AppTime))
                    oAppParameters.Duration = 0
                    oAppParameters.ClinicID = gnClinicID
                    ''                oAppParameters.LineNumber = Convert.ToInt64(C1Calendar.GetData(htInfo.Row, COL_LineNumber))

                    oAppParameters.LoadParameters = False

                    oSetupAppointment.SetAppointmentParameters = oAppParameters
                    oSetupAppointment.MasterAppointmentId = oAppParameters.MasterAppointmentID
                    oSetupAppointment.DetailAppointmentId = oAppParameters.AppointmentID

                    oSetupAppointment.ShowDialog(IIf(IsNothing(oSetupAppointment.Parent), Me, oSetupAppointment.Parent))



                    ShowAppointments()
                    ''SLR: Free dt, oSetupAppointment, oAppParameters, ogloAppointment, dtappdatetime
                    If Not IsNothing(dt) Then  ''slr free dt
                        dt.Dispose()
                        dt = Nothing
                    End If
                    If Not IsNothing(oSetupAppointment) Then  ''slr free oSetupAppointment
                        oSetupAppointment.Dispose()
                        oSetupAppointment = Nothing
                    End If
                    If Not IsNothing(oAppParameters) Then  ''slr free oAppParameters
                        oAppParameters.Dispose()
                        oAppParameters = Nothing
                    End If
                    If Not IsNothing(ogloAppointment) Then  ''slr free ogloAppointment
                        ogloAppointment.Dispose()
                        ogloAppointment = Nothing
                    End If
                    If Not IsNothing(dtappdatetime) Then  ''slr free dtappdatetime
                        dtappdatetime.Dispose()
                        dtappdatetime = Nothing
                    End If
                End If

            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub C1Calendar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Calendar.MouseDown
        pnlMntCalendar.Visible = False
    End Sub
#End Region

#Region " TRIAGE "
    Private Sub FillReceivedTriage_old()
        Dim oPatient As New gloPatient.gloPatient(GetConnectionString)
        Dim lastRow As Integer
        Try
            'DesignReceivedGrid()

            If Not IsNothing(oTriages) Then ''slr free previous memory
                oTriages = Nothing
            End If
            oTriages = oClsTriage.GetUserTriage(gnLoginID)
            If Not IsNothing(oTriages) Then
                For i As Integer = 1 To oTriages.Count

                    '' Current Day logic start ''
                    If isShowAllDays = False Then
                        If oTriages(i).TriageDate.Date <> currentDate Then
                            Continue For
                        End If
                    End If
                    '' Current Day logic start ''

                    If oTriages(i).IsFinished = False Then
                        With C1Triage
                            .Rows.Add()
                            lastRow = .Rows.Count - 1

                            .SetData(lastRow, COL_Triage_ID, oTriages(i).TriageID)
                            .SetData(lastRow, COL_Triage_DATE, oTriages(i).TriageDate)
                            '.SetData(lastRow, col_Received_Sender, oDashBoard.GetUserName(oTriages(i).FromID))
                            .SetData(lastRow, COL_Triage_PatientID, oTriages(i).PatientID)
                            .SetData(lastRow, COL_Triage_PatientName, oPatient.GetPatientName(oTriages(i).PatientID))
                            .SetData(lastRow, COL_Triage_DESC, oTriages(i).TemplateName)

                        End With
                    End If
                Next
            End If
            C1Triage.Row = -1
            If Not IsNothing(oPatient) Then
                oPatient.Dispose()
                oPatient = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FillReceivedTriage_new()
        'Dim oPatient As New gloPatient.gloPatient(GetConnectionString)
        Dim lastRow As Integer
        Try
            'DesignReceivedGrid()
            Dim strPatientname As String = "" '' This string contains Subject : Patient Name - triage name
            '   Dim _PatientID As Int64
            ' Dim _PatientName As String

            Dim ParentRowIndex As Int32 = 0
            Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
            Dim oParam As gloDatabaseLayer.DBParameters
            Dim dtTriage As New DataTable()

            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@UserID", gnLoginID, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@nTriageDate", currentDate, ParameterDirection.Input, SqlDbType.DateTime)
            oDB.Retrive("TRI_ShowLoginUserTriage", oParam, dtTriage)
           
            Dim i As Int16
            For i = 0 To dtTriage.Rows.Count - 1
                With C1Triage
                    .Rows.Add()
                    lastRow = .Rows.Count - 1
                    strPatientname = Convert.ToString(dtTriage.Rows(i)("sPatientName"))
                    Dim _TriageDate As DateTime = Convert.ToDateTime(dtTriage.Rows(i)("dtDate"))

                    .SetData(lastRow, COL_Triage_ID, CType(dtTriage.Rows(i)("nTriageID"), Int64))
                    .SetData(lastRow, COL_Triage_DATE, _TriageDate)
                    .SetData(lastRow, COL_Triage_PatientID, CType(dtTriage.Rows(i)("nPatientID"), Int64))
                    .SetData(lastRow, COL_Triage_PatientName, strPatientname)
                    C1Triage.SetData(lastRow, COL_Triage_DESC, CType(dtTriage.Rows(i)("sTemplateName"), String))

                End With

            Next

            'SLR: FRee oDB, dtTriage, oParam
            If Not IsNothing(oDB) Then
                oDB.Disconnect()
                oDB.Dispose()
            End If
            If Not IsNothing(dtTriage) Then
                dtTriage.Dispose()
                dtTriage = Nothing
            End If

            If Not IsNothing(oParam) Then
                oParam.Dispose()
                oParam = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub C1Triage_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Triage.MouseDoubleClick
        Try
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo  ''SLR: new is not needed
            htInfo = C1Triage.HitTest(e.X, e.Y)
            If htInfo.Row >= 0 Then
                Dim _TriageID As Int64 = CType(C1Triage.GetData(htInfo.Row, COL_Triage_ID), Int64)
                Dim _PatientID As Int64 = CType(C1Triage.GetData(htInfo.Row, COL_Triage_PatientID), Int64)
               
                If MainMenu.IsAccess(False, _PatientID) = False Then
                    Exit Sub
                Else
                    OpenTriage(_TriageID, _PatientID)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1Triage_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Triage.MouseDown
        pnlMntCalendar.Visible = False
    End Sub

    Private Sub OpenTriage(ByVal triageID As Int64, ByVal patientID As Int64)
        Try
            If triageID = 0 Then
                MessageBox.Show("Please select triage to modify.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            Dim frm As frmTriage

            frm = frmTriage.GetInstance(triageID, patientID, 2) '' Flag 2 Indicates, It is Opened from MyDay
            'AddHandler frm.EvntGenerateCDAFromTriage, AddressOf Raise_EvntGenerateCDAFromMyDay

            If IsNothing(frm) = True Then
                Exit Sub
            End If

            CType(Me.MdiParent, MainMenu).ShowHideMainMenu(False, False)
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

            frm.MdiParent = CType(Me.ParentForm, MainMenu)
            frm.myCallerMain = CType(Me.ParentForm, MainMenu)
            frm.MyDayCaller = Me
            frm.IsModify = True
            frm.WindowState = FormWindowState.Maximized
            frm.BringToFront()
            frm.Show()

        Catch ex As Exception
            CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = True
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region " MESSAGES "

    ''' <summary>
    ''' Fill Message from Database when Time Click
    ''' </summary>
    ''' <remarks>Add By Pramod for Surescript Message Alert</remarks>
    Public Sub FillMessage()
        Try
           
            Dim objclsMessage As New clsMessage
            Dim dt As DataTable
            Dim lastRow As Integer
            Dim _messageDate As Date

            dt = objclsMessage.GetAllMessages("T", gnLoginID)

            If Not IsNothing(dt) Then
                C1Message.Cols(COL_MSG_Priority).WidthDisplay = C1Message.Cols(COL_MSG_Priority).WidthDisplay - 30 ''added for bugid 69505
                C1Message.Cols(COL_MSG_DATE).WidthDisplay = C1Message.Cols(COL_MSG_DATE).WidthDisplay - 30
                C1Message.Cols(COL_MSG_Subject).WidthDisplay = C1Message.Cols(COL_MSG_Subject).WidthDisplay + 60
                For i As Integer = 0 To dt.Rows.Count - 1

                    _messageDate = Format(dt.Rows(i)("dtMessage"), "MM/dd/yyyy")

                    '' Current Day logic start ''
                    If isShowAllDays = False Then
                        If _messageDate <> currentDate Then
                            Continue For
                        End If
                    End If
                    '' Current Day logic start ''

                    If _messageDate = currentDate Then
                        C1Message.Rows.Add()
                        lastRow = C1Message.Rows.Count - 1

                        C1Message.SetData(lastRow, COL_MSG_ID, dt.Rows(i)("nMessageID"))
                        C1Message.SetData(lastRow, COL_MSG_DATE, _messageDate)
                        C1Message.SetData(lastRow, COL_MSG_PatientCode, dt.Rows(i)("sPatientCode"))
                        C1Message.SetData(lastRow, COL_MSG_PatientName, dt.Rows(i)("PatientName"))
                        C1Message.SetData(lastRow, COL_MSG_Subject, dt.Rows(i)("Subject"))  ''added for message PRD changes 8022
                        C1Message.SetData(lastRow, COL_MSG_DESC, dt.Rows(i)("sTemplateName"))
                        C1Message.SetData(lastRow, COL_MSG_PatientID, dt.Rows(i)("PatientID"))

                        '' SUDHIR 20090627 ''
                        Select Case dt.Rows(i)("Priority")
                            Case "High"
                                C1Message.SetData(lastRow, COL_MSG_Priority, "High Priority")
                            Case "Normal"
                                C1Message.SetData(lastRow, COL_MSG_Priority, "Normal Priority")
                            Case "Low"
                                C1Message.SetData(lastRow, COL_MSG_Priority, "Low Priority")
                            Case Else '' Default Low
                                C1Message.SetData(lastRow, COL_MSG_Priority, "Low Priority")
                        End Select

                    End If
                Next
            End If

            C1Message.Row = -1 '' TO DESSELECT ROW
            'SLR: Free objclsmessage
            If Not IsNothing(objclsMessage) Then
                objclsMessage.Dispose()
                objclsMessage = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
      
    End Sub

    Private Sub OpenMessages(ByVal messageID As Int64, ByVal patientCode As String, ByVal messageDate As DateTime, ByVal PatientID As Int64)
        Dim MsgID As Long
        Dim MsgDate As Date

        Dim objfrmMsg As frmMessages

        Try

            ''''''''''''' Added by Ujwala Atre - to implement 'lock status' functionality as on 11152010
            If MainMenu.IsAccess(False, PatientID) = False Then
                Exit Sub
            End If
            ''''''''''''' Added by Ujwala Atre - to implement 'lock status' functionality as on 11152010

            Dim blnisFinished As Boolean = False

            Dim blnRecordLock As Boolean = False

            Dim mydt As mytable ''slr new not needed
            If gblnRecordLocking = True Then
                mydt = Scan_n_Lock_Transaction(TrnType.Messages, MsgID, 0, MsgDate)
                If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                    If MessageBox.Show("This message is being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = Windows.Forms.DialogResult.Yes Then
                        ''Record open only for view.
                        blnRecordLock = True
                        '' Word document is only view can't add data to documents.
                        blnisFinished = True
                    Else
                        If Not IsNothing(mydt) Then ''slr free mydt
                            mydt.Dispose()
                            mydt = Nothing
                        End If

                        'Return False
                        Exit Sub
                    End If
                End If

                If Not IsNothing(mydt) Then ''slr free mydt
                    mydt.Dispose()
                    mydt = Nothing
                End If

            End If
            '''' <><><> Record Level Locking <><><><> 

            objfrmMsg = frmMessages.GetInstance(messageID, gnLoginID, messageDate, PatientID, 2, False) ''Flag 2 is to indicate frmMessages is Opened from frmMyDay ''

            If IsNothing(objfrmMsg) = True Then
                Exit Sub
            End If


            objfrmMsg.Text = "Modify Messages"
            objfrmMsg.myCallerMain = CType(Me.MdiParent, MainMenu)

            objfrmMsg.MyDayCaller = Me
            objfrmMsg.IsModify = True
            objfrmMsg.MdiParent = CType(Me.MdiParent, MainMenu)

            objfrmMsg.WindowState = FormWindowState.Maximized
            objfrmMsg.BringToFront()
            objfrmMsg.Show()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Messages", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1Message_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Message.MouseDoubleClick
        Try
            Dim htInfo As C1.Win.C1FlexGrid.HitTestInfo  ''slr new not needed 
            htInfo = C1Message.HitTest(e.X, e.Y)
            If htInfo.Row >= 0 Then
                Dim messageID As Int64 = CType(C1Message.GetData(htInfo.Row, COL_MSG_ID), Int64)
                Dim patientCode As String = C1Message.GetData(htInfo.Row, COL_MSG_PatientCode)
                Dim messageDate As DateTime = CType(C1Message.GetData(htInfo.Row, COL_MSG_DATE), DateTime)
                Dim patientID As Int64 = CType(C1Message.GetData(htInfo.Row, COL_MSG_PatientID), Int64)
               
                If MainMenu.IsAccess(False, patientID) = False Then
                    Exit Sub
                Else
                    OpenMessages(messageID, patientCode, messageDate, patientID)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1Message_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Message.MouseDown
        pnlMntCalendar.Visible = False
    End Sub

#End Region

#Region " TASKS "

    Private Sub FillTasks_old()

        Try
            C1Task.ScrollBars = ScrollBars.None
            'Get the Patient Reffrals.
            'C1Task.Clear()
            C1Task.DataSource = Nothing

            C1Task.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns
            C1Task.Clear(C1.Win.C1FlexGrid.ClearFlags.All)
            'Set the column Names.                              
            C1Task.Cols.Count = Col_Task_ColCount
            C1Task.Rows.Count = 1

            Dim csProvider As C1.Win.C1FlexGrid.CellStyle '= C1Task.Styles.Add("cs_Parent")
            Try
                If (C1Task.Styles.Contains("cs_Parent")) Then
                    csProvider = C1Task.Styles("cs_Parent")
                Else
                    csProvider = C1Task.Styles.Add("cs_Parent")
                    csProvider.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                    csProvider.BackColor = Color.FromArgb(222, 231, 250)
                    csProvider.ForeColor = Color.FromArgb(21, 66, 139)
                    csProvider.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
                    csProvider.ImageSpacing = 2
                    csProvider.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
                    csProvider.Border.Color = Color.FromArgb(159, 181, 221)
                End If
            Catch ex As Exception
                csProvider = C1Task.Styles.Add("cs_Parent")
                csProvider.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                csProvider.BackColor = Color.FromArgb(222, 231, 250)
                csProvider.ForeColor = Color.FromArgb(21, 66, 139)
                csProvider.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
                csProvider.ImageSpacing = 2
                csProvider.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
                csProvider.Border.Color = Color.FromArgb(159, 181, 221)
            End Try
 

            Dim csAppointment As C1.Win.C1FlexGrid.CellStyle '= C1Task.Styles.Add("cs_Child")
            Try
                If (C1Task.Styles.Contains("cs_Child")) Then
                    csAppointment = C1Task.Styles("cs_Child")
                Else
                    csAppointment = C1Task.Styles.Add("cs_Child")
                    csAppointment.Font = gloGlobal.clsgloFont.gFont_SMALL 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                    csAppointment.BackColor = Color.FromArgb(240, 247, 255)
                    csAppointment.ForeColor = Color.Black
                    csAppointment.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
                    csAppointment.ImageSpacing = 2
                    csAppointment.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
                End If
            Catch ex As Exception
                csAppointment = C1Task.Styles.Add("cs_Child")
                csAppointment.Font = gloGlobal.clsgloFont.gFont_SMALL 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                csAppointment.BackColor = Color.FromArgb(240, 247, 255)
                csAppointment.ForeColor = Color.Black
                csAppointment.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
                csAppointment.ImageSpacing = 2
                csAppointment.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
            End Try


            Dim csAppointment1 As C1.Win.C1FlexGrid.CellStyle '= C1Task.Styles.Add("cs_Child_Bold")
            Try
                If (C1Task.Styles.Contains("cs_Child_Bold")) Then
                    csAppointment1 = C1Task.Styles("cs_Child_Bold")
                Else
                    csAppointment1 = C1Task.Styles.Add("cs_Child_Bold")
                    csAppointment1.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD ' System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                    csAppointment1.BackColor = Color.FromArgb(240, 247, 255)
                    csAppointment1.ForeColor = Color.Black
                    csAppointment1.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
                    csAppointment1.ImageSpacing = 2
                    csAppointment1.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
                End If
            Catch ex As Exception
                csAppointment1 = C1Task.Styles.Add("cs_Child_Bold")
                csAppointment1.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                csAppointment1.BackColor = Color.FromArgb(240, 247, 255)
                csAppointment1.ForeColor = Color.Black
                csAppointment1.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
                csAppointment1.ImageSpacing = 2
                csAppointment1.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
            End Try

          

            C1Task.Tree.Column = 2
            C1Task.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
            C1Task.Tree.Indent = 7
            C1Task.Tree.LineColor = Color.Transparent

            C1Task.SetData(0, Col_Task_No, "No.")
            C1Task.SetData(0, Col_Task_TaskID, "TaskID")
            C1Task.SetData(0, Col_Task_Subject, "Subject")
            C1Task.SetData(0, Col_Task_DueDate, "DueDate")
            C1Task.SetData(0, Col_Task_Status, "Status")
            C1Task.SetData(0, Col_Task_Completed, "% Completed")
            C1Task.SetData(0, Col_Task_Priority, "Priority")
            C1Task.SetData(0, Col_Task_TaskDate, "TaskDate")
            C1Task.SetData(0, Col_Task_PatientID, "PatientID")
            C1Task.SetData(0, Col_Task_Assigned, "AssignStatus")

            'Lines - Start   

            Dim ogloTasks As New gloTaskMail.gloTask(GetConnectionString)
            Dim oTasks As gloTaskMail.Tasks  ''slr new not needed 
            Dim oHoldTasks As New gloTaskMail.Tasks
            Dim oAssignedTask As gloTaskMail.TaskAssigns  ''slr new not needed 
            Dim oTempTask As gloTaskMail.Task ''Used for only Assigned Task Logic
            Dim arrDueDate As New ArrayList ''Used to Store DueDates and sort it afterwards



            oTasks = ogloTasks.GetUserTasksNew(gnLoginID.ToString()) ''Fetching All UserTasks Except Assigned Task.
            oAssignedTask = ogloTasks.GetAssignedTasks(gnLoginID)  ''Fetching Assigned Task for this user Only.

            Dim i As Integer

            '' Above Logic will store DueDates of all Task & Assigned Tasks.
            '' DueDate will not be repeated in ArrayList
            If Not IsNothing(oTasks) Then
                For i = 0 To oTasks.Count - 1
                    If Not arrDueDate.Contains(CType(oTasks(i).DueDate, Object)) AndAlso oTasks(i).Progress.StatusName <> "Completed" Then

                        If isShowAllDays = False Then
                            If gloDateMaster.gloDate.DateAsDate(oTasks(i).DueDate) = currentDate.Date Then
                                arrDueDate.Add(CType(oTasks(i).DueDate, Object))
                            End If
                        Else
                            arrDueDate.Add(CType(oTasks(i).DueDate, Object))
                        End If

                    End If
                Next
            End If

            If Not IsNothing(oAssignedTask) Then
                For i = 0 To oAssignedTask.Count - 1
                    ' oTempTask = New gloTaskMail.Task ''slr new not needed 
                    oTempTask = ogloTasks.GetTask(oAssignedTask.Item(i).TaskID)
                    If IsNothing(oTempTask) = False Then
                        oHoldTasks.Add(oTempTask)
                        If Not arrDueDate.Contains(CType(oTempTask.DueDate, Object)) Then
                            If isShowAllDays = False Then
                                If gloDateMaster.gloDate.DateAsDate(oTempTask.DueDate) = currentDate.Date Then
                                    arrDueDate.Add(CType(oTempTask.DueDate, Object))
                                End If
                            Else
                                arrDueDate.Add(CType(oTempTask.DueDate, Object))
                            End If
                        End If
                        oTempTask = Nothing
                    End If
                Next
            End If


            '' This will sort All DueDates 
            arrDueDate.Sort()

            '' Add All Parent Nodes First
            '' This is Reverse Loop .. Thought DueDate Sorted in Ascending Order.
            For i = arrDueDate.Count - 1 To 0 Step -1
                Dim _DueDate As DateTime = gloDateMaster.gloDate.DateAsDate(arrDueDate(i).ToString)
                Dim _DueDateString As String = "  Due Date : " + Format(_DueDate, "MM/dd/yyyy")

                C1Task.Rows.Add()
                C1Task.Rows(C1Task.Rows.Count - 1).IsNode = True
                C1Task.Rows(C1Task.Rows.Count - 1).ImageAndText = True
                C1Task.Rows(C1Task.Rows.Count - 1).Node.Image = imgList_Common.Images(13)
                C1Task.Rows(C1Task.Rows.Count - 1).Node.Level = 0
                C1Task.Rows(C1Task.Rows.Count - 1).Node.Data = _DueDateString '_DueDate.ToString() ' No
                C1Task.Rows(C1Task.Rows.Count - 1).Node.Key = _DueDate  '' TASK Due Date
                C1Task.Rows(C1Task.Rows.Count - 1).Node.Row.Style = C1Task.Styles("cs_Parent")
                C1Task.Rows(C1Task.Rows.Count - 1).Height = 21
            Next
            '' End DUEDATE LOGIC


            ''Sudhir - To Generate Subject String 
            Dim oClsDashBoard As New clsDoctorsDashBoard
            Dim _SubjectString As String = "" '' This string contains Subject : PatientCode : Patient Name : Task Status
            Dim _Subject As String
            Dim _PatientID As Int64
            Dim _PatientCode As String = ""
            Dim _PatientName As String = ""
            Dim _TaskStatus As String

            Dim ParentRowIndex As Int32 = 0

            ''To show Assigned task in TaskList
            If IsNothing(oHoldTasks) = False Then
                For i = 0 To oHoldTasks.Count - 1

                    Dim _DueDate As DateTime = gloDateMaster.gloDate.DateAsDate(oHoldTasks(i).DueDate)

                    '' Day Setting Logic start
                    If isShowAllDays = False Then
                        If _DueDate <> currentDate.Date Then
                            Continue For
                        End If
                    End If
                    '' Day Setting Logic end

                    Dim _DueDateString As String = "  Due Date : " + Format(_DueDate, "MM/dd/yyyy")
                    Dim NodeFound As Boolean = False

                    ''To Search ParentNode for Task DueDate.
                    ParentRowIndex = 0 ''To start Search from Initial row.
                    For j As Int64 = 1 To C1Task.Rows.Count - 1
                        If C1Task.Rows(j).Node.Level = 0 Then
                            If C1Task.Rows(j).Node.Data = _DueDateString Then
                                ParentRowIndex = j
                                NodeFound = True
                                Exit For
                            End If
                        End If
                    Next

                    ''THIS CODE WILL NEVER EXECUTE.. 
                    ''IN EXTREAM CASE, IF NODE DOEST NOT FOUND .. IT WILL ADD PARENT NODE OF DUEDATE.
                    ''Add Parent Node of Found Date. ''If Not Present.
                    If NodeFound = False Then
                        C1Task.Rows.Add()
                        C1Task.Rows(C1Task.Rows.Count - 1).IsNode = True
                        C1Task.Rows(C1Task.Rows.Count - 1).ImageAndText = True
                        C1Task.Rows(C1Task.Rows.Count - 1).Node.Image = imgList_Common.Images(13)
                        C1Task.Rows(C1Task.Rows.Count - 1).Node.Level = 0
                        C1Task.Rows(C1Task.Rows.Count - 1).Node.Data = _DueDateString '_DueDate.ToString() ' No
                        C1Task.Rows(C1Task.Rows.Count - 1).Node.Key = _DueDate  '' TASK Due Date
                        C1Task.Rows(C1Task.Rows.Count - 1).Node.Row.Style = C1Task.Styles("cs_Parent")
                        C1Task.Rows(C1Task.Rows.Count - 1).Height = 21
                        ParentRowIndex = C1Task.Rows.Count - 1 ''This Will be Parent Row Index for next Child in this loop.
                    End If

                    ''Add Child Node for Respective Parent .. Parent Index Defined By Variable [ ParentRowIndex ]
                    Dim oChild As C1.Win.C1FlexGrid.Node
                    oChild = C1Task.Rows(ParentRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "", _DueDate, Nothing)
                    Dim ChildRowIndex As Int32 = oChild.Row.Index

                    C1Task.Rows(ChildRowIndex).ImageAndText = True
                    C1Task.Rows(ChildRowIndex).Node.Row.Style = C1Task.Styles("cs_Child_Bold") ''Show Assinged Task in Bold.

                    _TaskStatus = oHoldTasks(i).Progress.StatusName

                    _Subject = CType(oHoldTasks(i).Subject, System.String)

                    _SubjectString = _Subject & " : " & _TaskStatus

                    C1Task.SetData(ChildRowIndex, Col_Task_No, i + 1)
                    C1Task.SetData(ChildRowIndex, Col_Task_TaskID, CType(oHoldTasks(i).TaskID, Int64)) '' Task ID
                    C1Task.SetData(ChildRowIndex, Col_Task_Subject, _SubjectString) '' Subject 
                    C1Task.SetData(ChildRowIndex, Col_Task_DueDate, _DueDate)
                    C1Task.SetData(ChildRowIndex, Col_Task_Status, oHoldTasks(i).Progress.StatusName.ToString) '' Status
                    C1Task.SetData(ChildRowIndex, Col_Task_Completed, oHoldTasks(i).TaskType.ToString) ''
                    '' Column 6 for priority Which is set below.. in CASE Statement.
                    C1Task.SetData(ChildRowIndex, Col_Task_TaskDate, gloDateMaster.gloDate.DateAsDate(oHoldTasks(i).DateCreated)) ''TaskDate
                    C1Task.SetData(ChildRowIndex, Col_Task_PatientID, oHoldTasks(i).PatientID) ''PatientID by Default 0 for Assigned Task.
                    C1Task.SetData(ChildRowIndex, Col_Task_Assigned, MainMenu.enumTaskAssignStatus.Hold.GetHashCode)

                    'Display Priority Flag Icon According to Priority Level
                    Select Case oHoldTasks(i).Priority.ToString
                        Case "High"
                            C1Task.SetCellImage(ChildRowIndex, Col_Task_Priority, imgList_Common.Images(14))
                        Case "Normal"
                            'C1Task.SetCellImage(ChildRowIndex, Col_Task_Priority, imgList_Common.Images(1))
                        Case "Low"
                            C1Task.SetCellImage(ChildRowIndex, Col_Task_Priority, imgList_Common.Images(15))
                        Case Else
                            '' Default Normal
                            C1Task.SetCellImage(ChildRowIndex, Col_Task_Priority, imgList_Common.Images(1))
                    End Select
                Next
            End If


            If Not IsNothing(oTasks) Then
                For i = 0 To oTasks.Count - 1
                    If oTasks.Item(i).Progress.StatusName <> "Completed" Then
                        'Dim ParentRowIndex As Int32 = 0
                        Dim _DueDate As DateTime = gloDateMaster.gloDate.DateAsDate(oTasks.Item(i).DueDate)

                        '' Day Setting Logic start
                        If isShowAllDays = False Then
                            If _DueDate <> currentDate.Date Then
                                Continue For
                            End If
                        End If
                        '' Day Setting Logic end

                        Dim _DueDateString As String = "  Due Date : " + Format(_DueDate, "MM/dd/yyyy")
                        Dim NodeFound As Boolean = False

                        ''To Search ParentNode for Task DueDate.
                        ParentRowIndex = 0 ''To start Search from Initial row.

                        For j As Int64 = 1 To C1Task.Rows.Count - 1
                            If C1Task.Rows(j).Node.Level = 0 Then
                                If C1Task.Rows(j).Node.Data.ToString = _DueDateString Then
                                    ParentRowIndex = j
                                    NodeFound = True
                                    Exit For
                                End If
                            End If
                        Next

                        ''THIS CODE WILL NEVER EXECUTE.. 
                        ''IN EXTREAM CASE, IF NODE DOEST NOT FOUND .. IT WILL ADD PARENT NODE OF DUEDATE.
                        ''Add Parent Node of Found Date. ''If Not Present.
                        If NodeFound = False Then
                            C1Task.Rows.Add()
                            C1Task.Rows(C1Task.Rows.Count - 1).IsNode = True
                            C1Task.Rows(C1Task.Rows.Count - 1).ImageAndText = True
                            C1Task.Rows(C1Task.Rows.Count - 1).Node.Image = imgList_Common.Images(13)
                            C1Task.Rows(C1Task.Rows.Count - 1).Node.Level = 0
                            C1Task.Rows(C1Task.Rows.Count - 1).Node.Data = _DueDateString ' No
                            C1Task.Rows(C1Task.Rows.Count - 1).Node.Key = _DueDate  '' TASK Due Date
                            C1Task.Rows(C1Task.Rows.Count - 1).Node.Row.Style = C1Task.Styles("cs_Parent")
                            C1Task.Rows(C1Task.Rows.Count - 1).Height = 21
                            ParentRowIndex = C1Task.Rows.Count - 1  ''This Will be Parent Row Index for next Child in this loop.
                        End If

                        ''Add Child Node for Respective Parent .. Parent Index Defined By Variable [ ParentRowIndex ]
                        Dim oChild As C1.Win.C1FlexGrid.Node
                        oChild = C1Task.Rows(ParentRowIndex).Node.AddNode(C1.Win.C1FlexGrid.NodeTypeEnum.LastChild, "", _DueDate, Nothing)
                        Dim ChildRowIndex As Int32 = oChild.Row.Index


                        C1Task.Rows(ChildRowIndex).ImageAndText = True
                        C1Task.Rows(ChildRowIndex).Node.Row.Style = C1Task.Styles("cs_Child")

                        _PatientID = oTasks.Item(i).PatientID
                        _TaskStatus = oTasks.Item(i).Progress.StatusName

                        Dim oTemp_Task As gloTaskMail.Task
                        ' oTemp_Task = New gloTaskMail.Task()  ''slr new not needed 
                        oTemp_Task = ogloTasks.GetTask(oTasks.Item(i).TaskID)
                        If oTemp_Task.TaskType = gloTaskMail.TaskType.UnmatchedPatient Then
                            Dim dt As DataTable ''SLR: new is not needed
                            dt = oClsDashBoard.GetUnmatchedPatientCodeAndName(oTasks.Item(i).TaskID)
                            _PatientID = 0
                            _PatientCode = ""
                            _PatientName = ""
                            If IsNothing(dt) = False Then
                                If dt.Rows.Count > 0 Then
                                    _PatientCode = dt.Rows(0)("PatientCode")
                                    _PatientName = dt.Rows(0)("PatientName")
                                End If
                            End If
                            If Not IsNothing(dt) Then ''slr free dt
                                dt.Dispose()
                                dt = Nothing
                            End If

                        Else
                            Dim dt As DataTable
                            dt = oClsDashBoard.GetPatientCodeAndNameFromID(_PatientID)
                            If IsNothing(dt) = False Then
                                If dt.Rows.Count > 0 Then
                                    _PatientCode = dt.Rows(0)("PatientCode")
                                    _PatientName = dt.Rows(0)("PatientName")
                                End If
                            End If

                            If Not IsNothing(dt) Then ''slr free dt
                                dt.Dispose()
                                dt = Nothing
                            End If
                        End If
                        If Not IsNothing(oTemp_Task) Then
                            oTemp_Task.Dispose()
                        End If
                        ''End of change by Abhijeet on 20100729

                        _Subject = CType(oTasks.Item(i).Subject, System.String)

                        _SubjectString = _Subject & " : " & _PatientCode & " : " & _PatientName & " : " & _TaskStatus

                        C1Task.SetData(ChildRowIndex, Col_Task_No, i + 1)
                        C1Task.SetData(ChildRowIndex, Col_Task_TaskID, CType(oTasks.Item(i).TaskID, Int64)) '' Task ID
                        C1Task.SetData(ChildRowIndex, Col_Task_Subject, _SubjectString) '' Subject 
                        C1Task.SetData(ChildRowIndex, Col_Task_DueDate, _DueDate)
                        C1Task.SetData(ChildRowIndex, Col_Task_Status, oTasks.Item(i).Progress.StatusName.ToString) '' Status
                        C1Task.SetData(ChildRowIndex, Col_Task_Completed, oTasks.Item(i).TaskType.ToString) ''
                        '' Column 6 for priority Which is set below.. in CASE Statement.
                        C1Task.SetData(ChildRowIndex, Col_Task_TaskDate, gloDateMaster.gloDate.DateAsDate(oTasks.Item(i).DateCreated)) ''TaskDate
                        C1Task.SetData(ChildRowIndex, Col_Task_PatientID, _PatientID) ''PatientID
                        C1Task.SetData(ChildRowIndex, Col_Task_Assigned, MainMenu.enumTaskAssignStatus.Accepted.GetHashCode)

                        'Display Priority Flag Icon According to Priority Level
                        Select Case oTasks.Item(i).Priority.ToString
                            Case "High"
                                'C1Task.SetCellImage(ChildRowIndex, Col_Task_Priority, imgList_Common.Images(0))
                            Case "Normal"
                                'C1Task.SetCellImage(ChildRowIndex, Col_Task_Priority, imgList_Common.Images(1))
                            Case "Low"
                                'C1Task.SetCellImage(ChildRowIndex, Col_Task_Priority, imgList_Common.Images(2))
                            Case Else
                                '' Default Normal
                                'C1Task.SetCellImage(ChildRowIndex, Col_Task_Priority, imgList_Common.Images(1))
                        End Select
                    End If
                Next
            End If

            'Lines - Finish

            C1Task.Rows(0).Visible = False

            C1Task.Cols(Col_Task_No).Visible = False
            C1Task.Cols(Col_Task_TaskID).Visible = False
            C1Task.Cols(Col_Task_DueDate).Visible = False
            C1Task.Cols(Col_Task_Status).Visible = False
            C1Task.Cols(Col_Task_Completed).Visible = False
            C1Task.Cols(Col_Task_TaskDate).Visible = False
            C1Task.Cols(Col_Task_PatientID).Visible = False
            C1Task.Cols(Col_Task_Assigned).Visible = False

            C1Task.Cols(Col_Task_No).AllowEditing = False
            C1Task.Cols(Col_Task_TaskID).AllowEditing = False
            C1Task.Cols(Col_Task_Subject).AllowEditing = False
            C1Task.Cols(Col_Task_DueDate).AllowEditing = False
            C1Task.Cols(Col_Task_Status).AllowEditing = False
            C1Task.Cols(Col_Task_Completed).AllowEditing = False
            C1Task.Cols(Col_Task_Priority).AllowEditing = False
            C1Task.Cols(Col_Task_TaskDate).AllowEditing = False
            C1Task.Cols(Col_Task_PatientID).AllowEditing = False
            C1Task.Cols(Col_Task_Assigned).AllowEditing = False

            Dim _width As Integer = C1Task.Width - 2

            C1Task.Cols(Col_Task_Subject).Width = Convert.ToInt32(_width * 0.82) ''Subject
            C1Task.Cols(Col_Task_Completed).Width = Convert.ToInt32(_width * 0) ''% Completed Hidden
            C1Task.Cols(Col_Task_Priority).Width = Convert.ToInt32(_width * 0.1) ''Priority Flag

            If IsNothing(ogloTasks) = False Then
                ogloTasks.Dispose()
                ogloTasks = Nothing
            End If
            If IsNothing(oTasks) = False Then
                oTasks.Dispose()
                oTasks = Nothing
            End If
            If IsNothing(oHoldTasks) = False Then
                oHoldTasks.Dispose()
                oHoldTasks = Nothing
            End If
            If IsNothing(oAssignedTask) = False Then
                oAssignedTask.Dispose()
                oAssignedTask = Nothing
            End If

            'SLR: Free oclsdashboard
            If Not IsNothing(oClsDashBoard) Then
                oClsDashBoard = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            C1Task.ScrollBars = ScrollBars.Both
        End Try
    End Sub
    
    ''Modified by Mayuri:20110316-for optimization-filltasks realted to user for selected date only
    Private Sub FillTasks()
        Try
            Me.Cursor = Cursors.WaitCursor
            'C1Task.Clear()
            C1Task.DataSource = Nothing
            C1Task.Cols.Count = Col_Task_ColCount
            C1Task.Cols.Fixed = 0
            C1Task.Rows.Count = 1

            Dim csProvider As C1.Win.C1FlexGrid.CellStyle = C1Task.Styles(C1.Win.C1FlexGrid.CellStyleEnum.Subtotal0)
            csProvider.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
            csProvider.BackColor = Color.FromArgb(222, 231, 250)
            csProvider.ForeColor = Color.FromArgb(21, 66, 139)
            csProvider.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
            csProvider.ImageSpacing = 2
            csProvider.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
            csProvider.Border.Color = Color.FromArgb(159, 181, 221)

            Dim csAppointment1 As C1.Win.C1FlexGrid.CellStyle '= C1Task.Styles.Add("cs_Child_Bold")
            Try
                If (C1Task.Styles.Contains("cs_Child_Bold")) Then
                    csAppointment1 = C1Task.Styles("cs_Child_Bold")
                Else
                    csAppointment1 = C1Task.Styles.Add("cs_Child_Bold")
                    csAppointment1.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                    csAppointment1.BackColor = Color.FromArgb(240, 247, 255)
                    csAppointment1.ForeColor = Color.Black
                    csAppointment1.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
                    csAppointment1.ImageSpacing = 2
                    csAppointment1.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
                End If
            Catch ex As Exception
                csAppointment1 = C1Task.Styles.Add("cs_Child_Bold")
                csAppointment1.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD 'New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
                csAppointment1.BackColor = Color.FromArgb(240, 247, 255)
                csAppointment1.ForeColor = Color.Black
                csAppointment1.ImageAlign = C1.Win.C1FlexGrid.ImageAlignEnum.LeftTop
                csAppointment1.ImageSpacing = 2
                csAppointment1.Border.Direction = C1.Win.C1FlexGrid.BorderDirEnum.Horizontal
            End Try
  


            C1Task.DrawMode = C1.Win.C1FlexGrid.DrawModeEnum.OwnerDraw
            C1Task.Rows(0).Visible = False
            C1Task.Cols(Col_Task_DueDate).Visible = True
            C1Task.Cols(Col_Task_TaskID).Visible = False
            C1Task.Cols(Col_Task_No).Visible = False
            C1Task.Cols(Col_Task_Status).Visible = False
            C1Task.Cols(Col_Task_Completed).Visible = False
            C1Task.Cols(Col_Task_TaskDate).Visible = False
            C1Task.Cols(Col_Task_PatientID).Visible = False
            C1Task.Cols(Col_Task_Assigned).Visible = False

            Dim _width As Integer = C1Task.Width - 2
            C1Task.Cols(Col_Task_Subject).Width = Convert.ToInt32(_width - 36) ''Subject
            C1Task.Cols(Col_Task_Completed).Width = Convert.ToInt32(_width * 0) ''% Completed Hidden
            C1Task.Cols(Col_Task_Priority).Width = 20

            Dim ogloTasks As New gloTaskMail.gloTask(GetConnectionString)
            Dim oTasks As gloTaskMail.Tasks  ''slr new  not needed 
            Dim oClsDashBoard As New clsDoctorsDashBoard
            Dim i As Integer
            Dim _SubjectString As String = "" '' This string contains Subject : PatientCode : Patient Name : Task Status
            Dim _Subject As String
            Dim _PatientID As Int64
            Dim _PatientCode As String
            Dim _PatientName As String
            Dim _TaskStatus As String
            Dim _DueDate As DateTime
            Dim _DueDateString As String
            oTasks = ogloTasks.GetCurrentUserTasks(gnLoginID, currentDate)

            C1Task.Redraw = False
            If Not IsNothing(oTasks) Then

                For i = 0 To oTasks.Count - 1
                    ''   Dim total1 = C1Task.Cols(1).Index
                    _DueDate = gloDateMaster.gloDate.DateAsDate(oTasks(i).DueDate.ToString)
                    _DueDateString = "Due Date : " & Format(_DueDate, "MM/dd/yyyy")

                    If oTasks.Item(i).TaskType = gloTaskMail.TaskType.UnmatchedPatient Then
                        '' Get Patient Name & ID from Unmatched Patient Table
                        Dim dt As DataTable 'SLR: new is not needed
                        dt = oClsDashBoard.GetUnmatchedPatientCodeAndName(oTasks.Item(i).TaskID)
                        _PatientID = 0
                        _PatientCode = ""
                        _PatientName = ""
                        If IsNothing(dt) = False Then
                            If dt.Rows.Count > 0 Then
                                _PatientCode = dt.Rows(0)("PatientCode")
                                _PatientName = dt.Rows(0)("PatientName")
                            End If
                        End If
                        If Not IsNothing(dt) Then
                            dt.Dispose()
                        End If
                        dt = Nothing
                    Else
                        _PatientID = oTasks.Item(i).PatientID
                        _PatientCode = oTasks.Item(i).PatientCode
                        _PatientName = oTasks.Item(i).PatientName
                    End If

                    _TaskStatus = oTasks.Item(i).Progress.StatusName
                    _Subject = CType(oTasks.Item(i).Subject, System.String)
                    _SubjectString = _Subject & " : " & _PatientCode & " : " & _PatientName & " : " & _TaskStatus


                    C1Task.Rows.Add()

                    C1Task.SetData(C1Task.Rows.Count - 1, Col_Task_No, i + 1)
                    C1Task.SetData(C1Task.Rows.Count - 1, Col_Task_TaskID, CType(oTasks(i).TaskID, Int64)) '' Task ID
                    C1Task.SetData(C1Task.Rows.Count - 1, Col_Task_Subject, _SubjectString) '' Subject 
                    C1Task.SetData(C1Task.Rows.Count - 1, Col_Task_DueDate, _DueDateString)
                    C1Task.SetData(C1Task.Rows.Count - 1, Col_Task_Status, oTasks(i).Progress.StatusName.ToString) '' Status
                    C1Task.SetData(C1Task.Rows.Count - 1, Col_Task_Completed, oTasks(i).TaskType.ToString) ''
                    '' Column 6 for priority Which is set below.. in CASE Statement.
                    C1Task.SetData(C1Task.Rows.Count - 1, Col_Task_TaskDate, gloDateMaster.gloDate.DateAsDate(oTasks(i).DateCreated)) ''TaskDate
                    C1Task.SetData(C1Task.Rows.Count - 1, Col_Task_PatientID, oTasks(i).PatientID) ''PatientID by Default 0 for Assigned Task.
                    C1Task.SetData(C1Task.Rows.Count - 1, Col_Task_Assigned, oTasks(i).UserID)
                    If (oTasks(i).UserID <> gnLoginID) Then
                        C1Task.Rows(C1Task.Rows.Count - 1).Style = csAppointment1
                    End If
                    'Display Priority Flag Icon According to Priority Level
                    Select Case oTasks(i).Priority.ToString
                        Case "High"
                            C1Task.SetCellImage(C1Task.Rows.Count - 1, Col_Task_Priority, imgList_Common.Images(14))
                            C1Task.SetData(C1Task.Rows.Count - 1, Col_Task_Priority, "High Priority")
                        Case "Normal"
                            'C1Task.SetCellImage(ChildRowIndex, Col_Task_Priority, imgList_Common.Images(1))
                            'C1Task.SetData(ChildRowIndex, Col_Task_Priority, "Normal Priority")
                        Case "Low"
                            C1Task.SetCellImage(C1Task.Rows.Count - 1, Col_Task_Priority, imgList_Common.Images(15))
                            C1Task.SetData(C1Task.Rows.Count - 1, Col_Task_Priority, "Low Priority")
                        Case Else
                            '' Default Normal
                            C1Task.SetCellImage(C1Task.Rows.Count - 1, Col_Task_Priority, imgList_Common.Images(1))
                            C1Task.SetData(C1Task.Rows.Count - 1, Col_Task_Priority, "Normal Priority")
                    End Select



                Next
                C1Task.Cols(0).Visible = False
                C1Task.Rows(0).Visible = False
                C1Task.RemoveItem(0)
               
                C1Task.Tree.Column = 1
                C1Task.Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.SimpleLeaf
                C1Task.Tree.Indent = 20
                C1Task.Tree.LineColor = Color.Transparent
               
                C1Task.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Count, 0, 0, 0, "{0}")
                C1Task.Redraw = True

            End If
            'SLR:  Free ogloTasks, oTasks, oclsDashboard
            If Not IsNothing(ogloTasks) Then
                ogloTasks.Dispose()
                ogloTasks = Nothing

            End If
            If Not IsNothing(oTasks) Then
                oTasks.Dispose()
                oTasks = Nothing

            End If
            If Not IsNothing(oClsDashBoard) Then

                oClsDashBoard = Nothing

            End If

        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub C1Task_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1Task.DoubleClick
        Try
            If C1Task.Row > 0 Then

                If C1Task.Rows(C1Task.Row).Node.Children = 0 Then
                    Dim _taskId As Int64 = 0
                    Dim _PatientID As Int64 = 0
                    _taskId = CType(C1Task.GetData(C1Task.Row, Col_Task_TaskID), Int64)
                    nTaskId = _taskId  '' By Abhijeet on date 20100327
                    _PatientID = CType(C1Task.GetData(C1Task.Row, Col_Task_PatientID), Int64)

                    'Developer: Sanjog Dhamke
                    'Date:21 Dec 2011
                    'Bug ID/PRD Name/Sales force Case: This issue is got when i fix another one so therefore i resolve this issue. Issue is that on MyDay->Task its show the status form Dashboard selected patient. 
                    'Reason: We pass 2 more parameter value to this function, so it will check the current patient status from system. B4 it shows status of Dashboard selected patient.
                    If MainMenu.IsAccess(False, _PatientID, False, True) = False Then
                        Exit Sub
                    Else
                        ofrmTask = New gloTaskMail.frmTask(GetConnectionString, _taskId)
                        ofrmTask.PatientID = _PatientID
                        'AddHandler ofrmTask.OnTask_Change, AddressOf HandlesTaskChange
                        AddHandler ofrmTask.OnTask_Change, AddressOf ofrmTask_OnTask_Change
                        ofrmTask.IsEMREnable = True
                        ofrmTask.ShowDialog(IIf(IsNothing(ofrmTask.Parent), Me, ofrmTask.Parent))

                        'Bug #82464: 00000909: Task screen not opening respective screen
                        ofrmTask_OnTask_Change(Nothing, Nothing, ofrmTask.e2Task, Nothing)

                        'SLR: remove handler and then
                        'RemoveHandler ofrmTask.OnTask_Change, AddressOf HandlesTaskChange
                        RemoveHandler ofrmTask.OnTask_Change, AddressOf ofrmTask_OnTask_Change


                        ofrmTask.Dispose()

                        ''Added by Abhijeet on 20111123 to auto complete of task after viewing it
                        Dim ogloTask As gloTaskMail.gloTask = New gloTaskMail.gloTask(GetConnectionString)
                        Dim oTask As gloTaskMail.Task 'SLR: new is not needed
                        oTask = ogloTask.GetTask(_taskId)
                        If _taskId > 0 AndAlso oTask.Progress.Complete <> 100 _
                                       AndAlso (oTask.TaskType = gloTaskMail.TaskType.HL7LabInboundFailureNotifyTask Or _
                                                oTask.TaskType = gloTaskMail.TaskType.PatientPortalTask) Then

                            ogloTask.CompleteAll(_taskId)
                            FillTasks() '' Refresh Task panel Data
                        End If
                        If Not IsNothing(ogloTask) Then
                            ogloTask.Dispose()
                            ogloTask = Nothing
                        End If
                        If Not IsNothing(oTask) Then
                            oTask.Dispose()
                            oTask = Nothing
                        End If
                        ''End of changes Added by Abhijeet on 20111123 to auto complete of task after viewing it
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1Task_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1Task.MouseDown
        pnlMntCalendar.Visible = False
    End Sub
    'Added by pradeep on 20101025 for SmartOrder
    Dim _OrderID As Int64
    Dim _IsLabSave As Boolean = False
    Dim _IsOrderSave As Boolean = False
    Dim _IsFlowSheetSave As Boolean = False
    'Added by pradeep on 20101025 for Lab Task/Smart Order
    Private Sub UpdateLabTask()
        _IsLabSave = True

    End Sub
    'Added by pradeep on 20101025 for Order Task/Smart Order
    Private Sub UpdateOrderTask()
        _IsOrderSave = True
    End Sub
    'Added by pradeep on 20101025 for Drug Task
    Private Sub UpdateFlowSheetTask()
        _IsFlowSheetSave = True
    End Sub
    'Added by pradeep on 20101025 for Drug Task
    Private Function FillDrugList(ByVal sDrugCode As String) As ArrayList
        Dim sNDCCode() As String
        Dim query As String = ""
        Dim arrDrugs = New ArrayList
        Dim dt As DataTable
        Dim sdap As SqlDataAdapter
        sNDCCode = Split(sDrugCode, "|")
        For i As Integer = 0 To sNDCCode.Length - 1
            dt = New DataTable()
            query = "SELECT  nDrugsID as nDrugsID, ISNULL(sDrugName,'') as sDrugName, ISNULL(sGenericName,'') as sGenericName, " _
            & " ISNULL(sDosage,'') as sDosage, ISNULL(sRoute,'') as sRoute,ISNULL(sFrequency,'') as sFrequency, " _
            & " ISNULL(sDuration,'') as sDuration, ISNULL(bIsClinicalDrug,'False') as bIsClinicalDrug,ISNULL(sAmount,'') as sAmount," _
            & " ISNULL(nIsNarcotics,0) as nIsNarcotics, ISNULL(mpid,0) as mpid,ISNULL(bIsAllergicDrug,'False') as bIsAllergicDrug," _
            & " ISNULL(nClinicID,0) as nClinicID, ISNULL(bIsBlocked,'False') as bIsBlocked,ISNULL(sNDCCode,'') as sNDCCode," _
            & " ISNULL(sDrugForm,'') as sDrugForm,ISNULL(sDrugQtyQualifier,'') as sDrugQtyQualifier,ISNULL(RxType,'') as RxType from Drugs_MST " _
            & " WHERE sNDCCode='" & sNDCCode(i).ToString() & "'"
            sdap = New SqlDataAdapter(query, GetConnectionString())
            sdap.Fill(dt)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    Dim oDrug As New gloEMRGeneralLibrary.gloEMRActors.Drug
                    oDrug.DrugsID = dt.Rows(0)("nDrugsID")
                    oDrug.DrugsName = dt.Rows(0)("sDrugName")
                    oDrug.Dosage = dt.Rows(0)("sDosage")
                    oDrug.DrugForm = dt.Rows(0)("sDrugForm")
                    oDrug.Route = dt.Rows(0)("sRoute")
                    oDrug.Frequency = dt.Rows(0)("sFrequency")
                    oDrug.NDCCode = dt.Rows(0)("sNDCCode")
                    oDrug.IsNarcotics = dt.Rows(0)("nIsNarcotics")
                    oDrug.Duration = dt.Rows(0)("sDuration")
                    oDrug.mpid = dt.Rows(0)("mpid")
                    oDrug.DrugQtyQualifier = dt.Rows(0)("sDrugQtyQualifier")
                    arrDrugs.Add(oDrug)
                End If
            End If
            'SLR:Free dt, sadap
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(sdap) Then
                sdap.Dispose()
                sdap = Nothing
            End If
        Next
        Return arrDrugs
    End Function


    'Bug #82464: 00000909: Task screen not opening respective screen
    Sub ofrmTask_OnTask_Change(ByVal sender As Object, ByVal e As System.EventArgs, ByVal e2 As gloTaskMail.TaskChangeEventArg, Optional ByVal objfrmtask As Object = Nothing)
        RaiseEvent OnViewTask_Change(sender, e, e2, objfrmtask)
    End Sub

    Private Sub HandlesTaskChange(ByVal sender As Object, ByVal e As System.EventArgs, ByVal e2 As gloTaskMail.TaskChangeEventArg)
        Dim arrlist As ArrayList
        Dim olist As myList
        If e2.IsTaskClose = False Then

            If e2.oTaskType = gloTaskMail.TaskType.OrderRadiology Then
                ''ORDER
                Dim oDB As New gloStream.gloDataBase.gloDataBase

                Dim nVisitID As Int64
                Dim nPatientID As Int64
                Dim VisitDate As Date
                ''Get OrderID for Current Task to open 
                Dim nTaskID As Int64 = e2.TaskID
                Dim Query As String
                Dim strOrder As String
                Dim strOrders() As String
                Dim strOrderDetails() As String
                ' Dim sSubj As String
                Try
                    oDB.Connect(GetConnectionString)
                    Query = "SELECT sNoteExt FROM TM_TaskMST WHERE nTaskID = " & nTaskID & ""
                    strOrder = oDB.ExecuteQueryScaler(Query)
                    oDB.Disconnect()
                    ' Dim dtOrder As DataTable  slr not used
                    Dim frm As frm_LM_Orders
                    If strOrder = "" Then
                        oDB.Connect(GetConnectionString)
                        Query = "SELECT DISTINCT lm_Visit_ID, lm_OrderDate,lm_Patient_ID FROM LM_Orders WHERE lm_OrderDate IN ( SELECT sFaxTiffFileName FROM TM_TaskMST WHERE nTaskID = " & nTaskID & ")"
                        Dim dt As DataTable = oDB.ReadQueryDataTable(Query)
                        oDB.Disconnect()
                        If IsNothing(dt) = False Then
                            If (dt.Rows.Count > 0) Then
                                nVisitID = dt.Rows(0)("lm_Visit_ID")
                                VisitDate = dt.Rows(0)("lm_OrderDate")
                                nPatientID = dt.Rows(0)("lm_Patient_ID")
                            Else
                                '' added by pradeep 20100906 - to avoid exception if we save no order with the task or if we delete order related with task
                                MessageBox.Show("This order does not exists.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                ''slr free dt,odb
                                If Not IsNothing(dt) Then
                                    dt.Dispose()
                                    dt = Nothing
                                End If
                                If Not IsNothing(oDB) Then
                                    oDB.Dispose()
                                    oDB = Nothing
                                End If
                                Exit Sub
                            End If
                        Else
                            '' added by pradeep 20100906 - to avoid exception if we save no order with the task or if we delete order related with task
                            MessageBox.Show("This order does not exists.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            ''slr free dt,odb
                            If Not IsNothing(dt) Then
                                dt.Dispose()
                                dt = Nothing
                            End If
                            If Not IsNothing(oDB) Then
                                oDB.Dispose()
                                oDB = Nothing
                            End If
                            Exit Sub
                        End If


                        '' '' <><><> Record Level Locking <><><><>                         
                        Dim blnRecordLock As Boolean = False
                   
                        frm = frm_LM_Orders.GetInstance(nVisitID, VisitDate, nPatientID, 1, blnRecordLock)

                        If IsNothing(frm) = True Then
                            Exit Sub
                        End If

                        With frm
                            frm_LM_Orders.IsopenfrmTask = True
                            .MinimizeBox = False
                            .MaximizeBox = False
                            .ShowInTaskbar = False
                            '.Visible = True
                            .MdiParent = Nothing
                            .BringToFront()
                            .WindowState = FormWindowState.Maximized
                            .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))

                        End With
                        ''slr free frm
                        If Not IsNothing(frm) Then
                            frm.Close()
                            frm.Dispose()
                            frm = Nothing
                        End If
                    Else
                        arrlist = New ArrayList
                        strOrders = Split(strOrder, "|")
                        For i As Integer = 0 To strOrders.Length - 1
                            strOrderDetails = Split(strOrders.GetValue(i), "~")
                            olist = New myList
                            olist.Index = strOrderDetails.GetValue(0)
                            If (strOrderDetails.Length > 1) Then
                                olist.Value = strOrderDetails.GetValue(1)
                            End If

                            arrlist.Add(olist)
                        Next
                        If Not IsNothing(arrlist) Then
                            If arrlist.Count > 0 Then

                                'Added by pradeep on 20101025 for SmartOrder
                                oDB.Connect(GetConnectionString)
                                Query = "SELECT nPatientID FROM TM_TaskMST WHERE nTaskID = " & nTaskID
                                nPatientID = oDB.ExecuteQueryScaler(Query)
                                oDB.Disconnect()
                                frm = frm_LM_Orders.GetInstance(0, Now, nPatientID)
                                'Sanjog - Added on 2011 June 08 to solve issue 14154
                                If IsNothing(frm) = True Then
                                    Exit Sub
                                End If
                                Try
                                    RemoveHandler frm.EvnSaveOrderHandler, AddressOf UpdateOrderTask
                                Catch ex As Exception

                                End Try

                                AddHandler frm.EvnSaveOrderHandler, AddressOf UpdateOrderTask
                                'Sanjog - Added on 2011 June 08 to solve issue 14154
                                With frm
                                    ._ExamID = 0
                                    ._ArrRadi = arrlist
                                    ._patientID = nPatientID
                                    ._VisitID = 0
                                    ._VisitDate = Now
                                    .WindowState = FormWindowState.Maximized
                                    .BringToFront()
                                    .ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
                                End With

                                'Code Start- Added by kanchan on 20100618 for SmartOrder-task to multiple user
                                If _IsOrderSave = True Then
                                    If Not IsNothing(frm._VisitDate) Then
                                        Dim OrderDate As Date = frm._VisitDate
                                        oDB.Connect(GetConnectionString)
                                        Query = "UPDATE TM_TaskMST SET sNoteEXT='',sFaxTiffFileName='" & OrderDate & "' where ntaskid= " & nTaskID
                                        oDB.ExecuteNonSQLQuery(Query)
                                        oDB.Disconnect()
                                        ofrmTask.FaxTiffFileName = OrderDate.ToString()
                                    End If
                                    _IsOrderSave = False
                                End If
                                '  'SLR: removehandler and then FRee frm
                                If Not IsNothing(frm) Then
                                    RemoveHandler frm.EvnSaveOrderHandler, AddressOf UpdateOrderTask
                                    frm.Close()
                                    frm.Dispose()
                                    frm = Nothing
                                End If
                                '                       Code End- Added by kanchan on 20100618 for SmartOrder-task to multiple user
                            End If
                        End If
                    End If

                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                End Try


            ElseIf e2.oTaskType = gloTaskMail.TaskType.OpenEmdeonlabOrder Then
                'Developer:Sanjog Dhamke
                'Date: 21 Dec 2011
                'Bug ID/PRD Name/Sales force Case: Lab usability PRD to open the Emdeon from lab task window.
                'Reason: This is new task type to handle the button event for Opening of Emdeon Interface.
                OpenEmdeon(e2.TaskID)
            ElseIf e2.oTaskType = gloTaskMail.TaskType.LabOrder Then

                Dim oDB As New gloStream.gloDataBase.gloDataBase
                ''Get OrderID for Current Task to open 
                Dim ngTaskID As Int64 = ofrmTask.TaskID
                Dim Query As String
                Dim nPatientID As Int64
                Dim nOrderID As Int64
                Dim nTaskDate As Int64
                Dim nVisitID As Int64
                'Sanjog - Added on 2011 June 08 to solve issue 14154
                Dim sOrderDetail As String = ""
                Dim sLabs() As String
                Dim sLabDetails() As String
                'Sanjog - Added on 2011 June 08 to solve issue 14154
                Try
                    oDB.Connect(GetConnectionString)
                    ''OrderID is Stored In TM_TaskMST.nReferenceID1  for referance.
                    Query = "SELECT nReferenceID1 FROM TM_TaskMST WHERE nTaskID = " & nTaskId
                    nOrderID = oDB.ExecuteQueryScaler(Query)
                    Query = "SELECT nDateCreated FROM TM_TaskMST WHERE nTaskID = " & nTaskId
                    nTaskDate = oDB.ExecuteQueryScaler(Query)
                    If nOrderID > 0 Then
                        Query = "SELECT labom_VisitID FROM Lab_Order_MST WHERE labom_OrderID = " & nOrderID
                        nVisitID = oDB.ExecuteQueryScaler(Query)
                    End If
                    Query = "SELECT nPatientID FROM TM_TaskMST WHERE nTaskID = " & nTaskId
                    nPatientID = oDB.ExecuteQueryScaler(Query)

                    'Sanjog - Added on 2011 June 08 to solve issue 14154
                    Query = "SELECT sNoteExt FROM TM_TaskMST WHERE nTaskID = " & nTaskId
                    sOrderDetail = oDB.ExecuteQueryScaler(Query)
                    'Sanjog - Added on 2011 June 08 to solve issue 14154

                    oDB.Disconnect()
                Catch ex As Exception

                End Try

                If nOrderID > 0 Then


                    'If gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_hsilabel <> "" Then
                    Dim frm_g As New gloEmdeonInterface.Forms.frmViewgloLab(nOrderID, nPatientID)
                    'Dim myEventHandler As New gloEmdeonInterface.Forms.frmViewgloLab.OpenClinicalChart(AddressOf CType(Me.ParentForm, MainMenu).OpenClinicalChart)

                    AddHandler frm_g.EventCDA, AddressOf mdlGeneral.OpenCDA
                    AddHandler frm_g.EvntOpenClinicalChart, AddressOf CType(Me.ParentForm, MainMenu).OpenClinicalChart 'myEventHandler
                    With frm_g.LabOrderParameter
                        .IsEditMode = False
                        .OrderID = nOrderID
                        .OrderNumberID = 0
                        .OrderNumberPrefix = "ORD"
                        .PatientID = nPatientID
                        .VisitID = nVisitID
                        .TransactionDate = gloDateMaster.gloDate.DateAsDate(nTaskDate)
                        .CloseAfterSave = True
                    End With
                    With frm_g

                        'Developer: Sanjog Dhamke
                        'Date:21 Dec 2011
                        'Bug ID/PRD Name/Sales force Case: Bug ID = 1703 - Task is getting closed after open of record result.
                        'Reason: Now we open this form as Dialog and not close the Task form.

                        '.MdiParent = CType(Me.MdiParent, MainMenu)
                        .WindowState = FormWindowState.Maximized
                        ''added for split control functionality order & result screen
                        Dim objclsSplit_Laborder As New gloEMRGeneralLibrary.clsSplitScreen()
                        objclsSplit_Laborder.clsPatientExams = New clsPatientExams()
                        objclsSplit_Laborder.clsPatientLetters = New clsPatientLetters()
                        objclsSplit_Laborder.clsPatientMessages = New clsMessage()
                        objclsSplit_Laborder.clsNurseNotes = New clsNurseNotes()
                        objclsSplit_Laborder.clsHistory = New clsPatientHistory()
                        objclsSplit_Laborder.clsLabs = New clsDoctorsDashBoard()
                        objclsSplit_Laborder.clsRxmed = New clsPatientDetails()
                        objclsSplit_Laborder.clsOrders = New clsPatientDetails()
                        objclsSplit_Laborder.clsProblemList = New clsPatientProblemList()
                        objclsSplit_Laborder.blnShowSmokingStatusCol = gblnShowSmokingColumn

                        frm_g.objCriteria = New DocCriteria
                        frm_g.objWord = New clsWordDocument
                        frm_g.VisitID = 0
                        frm_g.clsSplit_Laborder = objclsSplit_Laborder

                        .ShowDialog(IIf(IsNothing(frm_g.Parent), Me, frm_g.Parent))
                        'ofrmTask.Close()
                        If (Not objclsSplit_Laborder Is Nothing) Then
                            If (Not objclsSplit_Laborder.clsPatientExams Is Nothing) Then
                                CType(objclsSplit_Laborder.clsPatientExams, clsPatientExams).Dispose()
                                objclsSplit_Laborder.clsPatientExams = Nothing
                            End If
                            If (Not objclsSplit_Laborder.clsPatientLetters Is Nothing) Then
                                CType(objclsSplit_Laborder.clsPatientLetters, clsPatientLetters).Dispose()
                                objclsSplit_Laborder.clsPatientLetters = Nothing
                            End If
                            If (Not objclsSplit_Laborder.clsPatientMessages Is Nothing) Then
                                CType(objclsSplit_Laborder.clsPatientMessages, clsMessage).Dispose()
                                objclsSplit_Laborder.clsPatientMessages = Nothing
                            End If
                            If (Not objclsSplit_Laborder.clsNurseNotes Is Nothing) Then
                                CType(objclsSplit_Laborder.clsNurseNotes, clsNurseNotes).Dispose()
                                objclsSplit_Laborder.clsNurseNotes = Nothing
                            End If
                            If (Not objclsSplit_Laborder.clsHistory Is Nothing) Then
                                CType(objclsSplit_Laborder.clsHistory, clsPatientHistory).Dispose()
                                objclsSplit_Laborder.clsHistory = Nothing
                            End If
                            objclsSplit_Laborder.clsLabs = Nothing
                            If (Not objclsSplit_Laborder.clsRxmed Is Nothing) Then
                                CType(objclsSplit_Laborder.clsRxmed, clsPatientDetails).Dispose()
                                objclsSplit_Laborder.clsRxmed = Nothing
                            End If
                            If (Not objclsSplit_Laborder.clsOrders Is Nothing) Then
                                CType(objclsSplit_Laborder.clsOrders, clsPatientDetails).Dispose()
                                objclsSplit_Laborder.clsOrders = Nothing
                            End If
                            If (Not objclsSplit_Laborder.clsProblemList Is Nothing) Then
                                CType(objclsSplit_Laborder.clsProblemList, clsPatientProblemList).Dispose()
                                objclsSplit_Laborder.clsProblemList = Nothing
                            End If

                            objclsSplit_Laborder.Dispose()
                            objclsSplit_Laborder = Nothing
                        End If
                    End With
                    If (IsNothing(frm_g) = False) Then
                        RemoveHandler frm_g.EventCDA, AddressOf mdlGeneral.OpenCDA
                        RemoveHandler frm_g.EvntOpenClinicalChart, AddressOf CType(Me.ParentForm, MainMenu).OpenClinicalChart ' myEventHandler
                        frm_g.Close()
                        If (IsNothing(frm_g) = False) Then
                            frm_g.Dispose()
                            frm_g = Nothing
                        End If
                    End If

                Else
                    'Sanjog - Added on 2011 June 08 to solve issue 14154
                    arrlist = New ArrayList
                    If Not sOrderDetail = "" Then
                        sLabs = Split(sOrderDetail, "|")
                        For i As Integer = 0 To sLabs.Length - 1
                            sLabDetails = Split(sLabs.GetValue(i), "~")
                            For j As Integer = 0 To sLabDetails.Length - 1
                                Dim Emdeonlst As New gloEmdeonCommon.myList
                                Emdeonlst.ID = sLabDetails.GetValue(0)
                                If (sLabDetails.Length > 1) Then
                                    Emdeonlst.Value = sLabDetails.GetValue(1)
                                End If

                                arrlist.Add(Emdeonlst)
                            Next
                        Next
                    End If
                    'Sanjog - Added on 2011 June 08 to solve issue 14154

                    'Sanjog Added on 2011 July 29 to solve Bug No.5294 in 6040
                    If arrlist.Count > 0 Then
                        Dim frm_g As New gloEmdeonInterface.Forms.frmViewNormalLab(nPatientID)
                        AddHandler frm_g.saveLabOrder, AddressOf UpdateLabTask
                        AddHandler frm_g.Event_CallCDA, AddressOf mdlGeneral.OpenCDA
                        With frm_g.LabOrderParameter
                            .IsEditMode = False
                            .OrderID = 0
                            .OrderNumberID = 0
                            .OrderNumberPrefix = "ORD"
                            .PatientID = nPatientID
                            .VisitID = nVisitID
                            .TransactionDate = gloDateMaster.gloDate.DateAsDate(nTaskDate)
                            .CloseAfterSave = True
                        End With

                        With frm_g
                            .ArrLabs = arrlist
                            .WindowState = FormWindowState.Maximized
                            .ShowDialog(IIf(IsNothing(frm_g.Parent), Me, frm_g.Parent))
                        End With

                        If _IsLabSave = True Then
                            If frm_g.LabOrderParameter.OrderID <> 0 Then
                                Dim _orderid As Int64 = frm_g.LabOrderParameter.OrderID
                                oDB.Connect(GetConnectionString)
                                Query = "UPDATE TM_TaskMST SET sNoteEXT='',nReferenceID1=" & _orderid & " where ntaskid= " & nTaskId
                                oDB.ExecuteNonSQLQuery(Query)
                                oDB.Disconnect()
                                oDB.Dispose()
                                oDB = Nothing
                                ofrmTask.sNoteExt = ""
                                ofrmTask.ReferenceID = _orderid
                            End If
                            _IsLabSave = False
                        End If
                        If Not IsNothing(frm_g) Then
                            RemoveHandler frm_g.Event_CallCDA, AddressOf mdlGeneral.OpenCDA
                            RemoveHandler frm_g.saveLabOrder, AddressOf UpdateLabTask
                            frm_g.Close()
                            If (IsNothing(frm_g) = False) Then
                                frm_g.Dispose()
                                frm_g = Nothing
                            End If

                        End If
                    Else
                        Dim frm_g As New gloEmdeonInterface.Forms.frmViewgloLab(nPatientID)
                        ' Dim myEventHandler As New gloEmdeonInterface.Forms.frmViewgloLab.OpenClinicalChart(AddressOf CType(Me.ParentForm, MainMenu).OpenClinicalChart)

                        AddHandler frm_g.EventCDA, AddressOf mdlGeneral.OpenCDA
                        AddHandler frm_g.EvntOpenClinicalChart, AddressOf CType(Me.ParentForm, MainMenu).OpenClinicalChart 'myEventHandler
                        With frm_g.LabOrderParameter
                            .IsEditMode = False
                            .OrderID = 0
                            .OrderNumberID = 0
                            .OrderNumberPrefix = "ORD"
                            .PatientID = nPatientID
                            .VisitID = nVisitID
                            .TransactionDate = gloDateMaster.gloDate.DateAsDate(nTaskDate)
                            .CloseAfterSave = True
                        End With

                        Dim objclsSplit_Laborder As New gloEMRGeneralLibrary.clsSplitScreen()


                        objclsSplit_Laborder.clsPatientExams = New clsPatientExams()
                        objclsSplit_Laborder.clsPatientLetters = New clsPatientLetters()
                        objclsSplit_Laborder.clsPatientMessages = New clsMessage()
                        objclsSplit_Laborder.clsNurseNotes = New clsNurseNotes()
                        objclsSplit_Laborder.clsHistory = New clsPatientHistory()
                        objclsSplit_Laborder.clsLabs = New clsDoctorsDashBoard()
                        objclsSplit_Laborder.clsRxmed = New clsPatientDetails()
                        objclsSplit_Laborder.clsOrders = New clsPatientDetails()
                        objclsSplit_Laborder.clsProblemList = New clsPatientProblemList()
                        objclsSplit_Laborder.blnShowSmokingStatusCol = gblnShowSmokingColumn

                        frm_g.objCriteria = New DocCriteria
                        frm_g.objWord = New clsWordDocument
                        frm_g.VisitID = 0
                        frm_g.clsSplit_Laborder = objclsSplit_Laborder
                        With frm_g
                            .WindowState = FormWindowState.Maximized
                            .ShowDialog(IIf(IsNothing(frm_g.Parent), Me, frm_g.Parent))
                            If (Not objclsSplit_Laborder Is Nothing) Then
                                If (Not objclsSplit_Laborder.clsPatientExams Is Nothing) Then
                                    CType(objclsSplit_Laborder.clsPatientExams, clsPatientExams).Dispose()
                                    objclsSplit_Laborder.clsPatientExams = Nothing
                                End If
                                If (Not objclsSplit_Laborder.clsPatientLetters Is Nothing) Then
                                    CType(objclsSplit_Laborder.clsPatientLetters, clsPatientLetters).Dispose()
                                    objclsSplit_Laborder.clsPatientLetters = Nothing
                                End If
                                If (Not objclsSplit_Laborder.clsPatientMessages Is Nothing) Then
                                    CType(objclsSplit_Laborder.clsPatientMessages, clsMessage).Dispose()
                                    objclsSplit_Laborder.clsPatientMessages = Nothing
                                End If
                                If (Not objclsSplit_Laborder.clsNurseNotes Is Nothing) Then
                                    CType(objclsSplit_Laborder.clsNurseNotes, clsNurseNotes).Dispose()
                                    objclsSplit_Laborder.clsNurseNotes = Nothing
                                End If
                                If (Not objclsSplit_Laborder.clsHistory Is Nothing) Then
                                    CType(objclsSplit_Laborder.clsHistory, clsPatientHistory).Dispose()
                                    objclsSplit_Laborder.clsHistory = Nothing
                                End If
                                objclsSplit_Laborder.clsLabs = Nothing
                                If (Not objclsSplit_Laborder.clsRxmed Is Nothing) Then
                                    CType(objclsSplit_Laborder.clsRxmed, clsPatientDetails).Dispose()
                                    objclsSplit_Laborder.clsRxmed = Nothing
                                End If
                                If (Not objclsSplit_Laborder.clsOrders Is Nothing) Then
                                    CType(objclsSplit_Laborder.clsOrders, clsPatientDetails).Dispose()
                                    objclsSplit_Laborder.clsOrders = Nothing
                                End If
                                If (Not objclsSplit_Laborder.clsProblemList Is Nothing) Then
                                    CType(objclsSplit_Laborder.clsProblemList, clsPatientProblemList).Dispose()
                                    objclsSplit_Laborder.clsProblemList = Nothing
                                End If

                                objclsSplit_Laborder.Dispose()
                                objclsSplit_Laborder = Nothing
                            End If
                        End With
                        If Not IsNothing(frm_g) Then
                            RemoveHandler frm_g.EventCDA, AddressOf mdlGeneral.OpenCDA
                            RemoveHandler frm_g.EvntOpenClinicalChart, AddressOf CType(Me.ParentForm, MainMenu).OpenClinicalChart 'myEventHandler
                            frm_g.Close()
                            If (IsNothing(frm_g) = False) Then
                                frm_g.Dispose()
                                frm_g = Nothing
                            End If

                        End If
                    End If
                    'Sanjog Added on 2011 July 29 to solve Bug No.5294 in 6040
                    End If

                ElseIf e2.oTaskType = gloTaskMail.TaskType.DMS Then
                    '' Sudhir 20090410 ''
                    '' DMS Documents ''
                    Try
                        Dim oDB As New gloStream.gloDataBase.gloDataBase

                        Dim nTaskID As Int64 = ofrmTask.TaskID
                        Dim query As String
                        Dim _ContID As Long = 0
                        Dim _DocID As Long = 0
                        Dim _Year As String = ""
                        Dim nPatientID As Long

                        Try
                            oDB.Connect(GetConnectionString)
                            query = "SELECT nReferenceID1 FROM TM_TaskMST WHERE nTaskID = " & nTaskID
                            _ContID = oDB.ExecuteQueryScaler(query)
                            query = "SELECT nReferenceID2 FROM TM_TaskMST WHERE nTaskID = " & nTaskID
                            _DocID = oDB.ExecuteQueryScaler(query)
                            query = "SELECT nPatientID FROM TM_TaskMST WHERE nTaskID = " & nTaskID
                            nPatientID = oDB.ExecuteQueryScaler(query)
                            oDB.Disconnect()
                        Catch ex As Exception

                        End Try

                        If _DocID > 0 Then 'And _Year <> "" Then
                            If CType(Me.ParentForm, MainMenu).oEDocumentV3 Is Nothing Then
                                CType(Me.ParentForm, MainMenu).oEDocumentV3 = New gloEDocumentV3.gloEDocV3Management()
                            End If
                            CType(Me.ParentForm, MainMenu).oEDocumentV3.ShowEDocument(nPatientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocumentForExternalModule, CType(Me.ParentForm, MainMenu), gloEDocumentV3.Enumeration.enum_OpenExternalSource.DashBoard, _DocID)
                        End If

                        ''slr free oDB
                        If Not IsNothing(oDB) Then
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                    Catch ex As Exception

                    End Try

                    '' By Abhijeet on Date 20100327
                    '' code for calling unmatch patient matching action form
                ElseIf e2.oTaskType = gloTaskMail.TaskType.UnmatchedPatient Then
                    Try
                        If nTaskId <> e2.TaskID Then

                            Dim oDB As New gloStream.gloDataBase.gloDataBase
                            Dim query As String
                            oDB.Connect(GetConnectionString)
                            query = "UPDATE PatientsUnmatchedInLab set nTaskId=" & e2.TaskID & " where nTaskId= " & nTaskId
                            oDB.ExecuteNonSQLQuery(query)
                            oDB.Disconnect()
                            nTaskId = e2.TaskID

                            ''slr free oDB
                            If Not IsNothing(oDB) Then
                                oDB.Dispose()
                                oDB = Nothing
                            End If
                        End If

                        Dim objfrmViewUnMatchPatients As frmViewUnMatchPatients = New frmViewUnMatchPatients(e2.TaskID, gnLoginID)
                        objfrmViewUnMatchPatients.WindowState = FormWindowState.Normal
                        objfrmViewUnMatchPatients.ShowDialog(IIf(IsNothing(objfrmViewUnMatchPatients.Parent), Me, objfrmViewUnMatchPatients.Parent))
                        ' Task form is closed as overwriting existing updates of current task
                        If Not IsNothing(ofrmTask) Then
                            ofrmTask.Close()
                        End If
                        objfrmViewUnMatchPatients.Dispose()
                        objfrmViewUnMatchPatients = Nothing
                    Catch ex As Exception
                    End Try
                    '' End of Changes By Abhijeet 
                    ''Code Start - Added by kanchan on 20100605 for CCD
                ElseIf e2.oTaskType = gloTaskMail.TaskType.CCD Or e2.oTaskType = gloTaskMail.TaskType.CCDUnmatchedPatient Then
                    Try
                        Dim _taskType As Int32
                        If e2.oTaskType = gloTaskMail.TaskType.CCD Then
                            _taskType = 9
                        Else
                            _taskType = 10
                        End If
                        Dim oDB As New gloStream.gloDataBase.gloDataBase
                        Dim query As String
                        oDB.Connect(GetConnectionString)
                        If nTaskId <> e2.TaskID Then
                            query = "UPDATE CCD_Queue set nTaskId=" & e2.TaskID & " where nTaskId= " & nTaskId
                            oDB.ExecuteNonSQLQuery(query)
                            nTaskId = e2.TaskID
                        End If
                        Dim nPatientID As Int64
                        Dim _CCDID As Long = 0
                        ' CCDID is Stored in TM_TaskMST.nReferenceID1  for referance
                        oDB.Connect(GetConnectionString)
                        query = "SELECT nPatientID FROM TM_TaskMST WHERE nTaskID = " & nTaskId
                        nPatientID = oDB.ExecuteQueryScaler(query)
                        query = "SELECT nReferenceID1 FROM TM_TaskMST WHERE nTaskID = " & nTaskId
                        _CCDID = oDB.ExecuteQueryScaler(query)
                        oDB.Disconnect()
                        If _CCDID > 0 Then
                            Dim objfrmCCD_Display As frmCCD_Display = New frmCCD_Display(nPatientID)
                            objfrmCCD_Display.TaskType = e2.oTaskType
                            objfrmCCD_Display.CCDId = _CCDID
                            objfrmCCD_Display.WindowState = FormWindowState.Normal
                            objfrmCCD_Display.StartPosition = FormStartPosition.CenterScreen
                            objfrmCCD_Display.ShowDialog(IIf(IsNothing(objfrmCCD_Display.Parent), Me, objfrmCCD_Display.Parent))
                            If Not IsNothing(ofrmTask) Then
                                ofrmTask.Close()
                            End If
                            objfrmCCD_Display.Dispose()
                            objfrmCCD_Display = Nothing
                        End If

                        ''slr free oDB
                        If Not IsNothing(oDB) Then
                            oDB.Dispose()
                            oDB = Nothing
                        End If

                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                    End Try
                    ''Code End - Added by kanchan on 20100605 for CCD
                ElseIf e2.oTaskType = gloTaskMail.TaskType.Flowsheet Then
                    ''Code Start-Added by kanchan on 20100619 for Flowsheet
                    Try
                        Dim sFlowSheetName As String
                        Dim oDB As New gloStream.gloDataBase.gloDataBase
                        Dim nPatientID As Int64
                        Dim sFlowName As String
                        Dim query As String
                        Dim sFlow() As String
                        Dim sFlows() As String
                        oDB.Connect(GetConnectionString)
                        query = "SELECT nPatientID FROM TM_TaskMST WHERE nTaskID = " & nTaskId
                        nPatientID = oDB.ExecuteQueryScaler(query)
                        query = "SELECT sNoteExt FROM TM_TaskMST WHERE nTaskID = " & nTaskId
                        sFlowName = oDB.ExecuteQueryScaler(query)
                        If sFlowName = "" Then
                            query = "SELECT sFaxTiffFileName FROM TM_TaskMST WHERE nTaskID = " & nTaskId
                            sFlowSheetName = oDB.ExecuteQueryScaler(query)
                            oDB.Disconnect()
                            If sFlowSheetName <> "" Then
                                Dim objfrmpatientflowsheet As New frmPatientFlowSheet(nPatientID, sFlowSheetName)
                                ''Bug : 00000828: Record locking
                                If objfrmpatientflowsheet.FormLevelLock() Then
                                    objfrmpatientflowsheet.WindowState = FormWindowState.Maximized
                                    objfrmpatientflowsheet.ShowDialog(IIf(IsNothing(objfrmpatientflowsheet.Parent), Me, objfrmpatientflowsheet.Parent))
                                End If

                                objfrmpatientflowsheet.Dispose()
                                objfrmpatientflowsheet = Nothing
                            End If
                        Else
                            Dim _FlowSheetName As String = ""
                            arrlist = New ArrayList
                            sFlow = Split(sFlowName, "|")
                            For i As Integer = 0 To sFlow.Length - 1
                                sFlows = Split(sFlow.GetValue(i), "~")
                                olist = New myList
                            olist.ID = sFlows.GetValue(0)
                            If (sFlows.Length > 1) Then
                                olist.Value = sFlows.GetValue(1)
                            End If

                            _FlowSheetName = olist.Value
                            arrlist.Add(olist)
                        Next
                            If Not IsNothing(arrlist) Then
                                If arrlist.Count > 0 Then

                                    Dim objfrmpatientflowsheet As New frmPatientFlowSheet(nPatientID, _FlowSheetName)
                                    ''Bug : 00000828: Record locking
                                    If objfrmpatientflowsheet.FormLevelLock() Then
                                        AddHandler objfrmpatientflowsheet.EvnSaveFlowSheet, AddressOf UpdateFlowSheetTask

                                        With objfrmpatientflowsheet
                                            frmPatientFlowSheet.Array_Flow_List = arrlist
                                            .WindowState = FormWindowState.Maximized
                                            .BringToFront()
                                            .ShowDialog(IIf(IsNothing(objfrmpatientflowsheet.Parent), Me, objfrmpatientflowsheet.Parent))

                                        End With
                                        RemoveHandler objfrmpatientflowsheet.EvnSaveFlowSheet, AddressOf UpdateFlowSheetTask
                                    End If

                                    objfrmpatientflowsheet.Dispose()
                                    objfrmpatientflowsheet = Nothing

                                    If _IsFlowSheetSave = True Then
                                        If _FlowSheetName <> "" Then

                                            oDB.Connect(GetConnectionString)
                                            query = "UPDATE TM_TaskMST SET sNoteEXT='',sFaxTiffFileName='" & _FlowSheetName.Replace("'", "''").ToString & "' where ntaskid= " & nTaskId
                                            oDB.ExecuteNonSQLQuery(query)
                                            oDB.Disconnect()
                                            ofrmTask.FaxTiffFileName = _FlowSheetName
                                        End If
                                    End If
                                End If
                            End If
                        End If

                        ''slr free oDB
                        If Not IsNothing(oDB) Then
                            oDB.Dispose()
                            oDB = Nothing
                        End If
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                    End Try
                    ''Code End - Added by kanchan on 20100619 for flowsheet
                ElseIf e2.oTaskType = gloTaskMail.TaskType.Drug Then
                    'Code Start-Added by kanchan on 20100621 for Drugs
                    Try
                        Dim ArrDrugs As New ArrayList()
                        Dim sNotes As String
                        Dim oDB As New gloStream.gloDataBase.gloDataBase
                        Dim nPatientID As Int64
                        Dim nProviderID As Int64
                        Dim sDrugName As String
                        Dim query As String
                        Dim nDrugVisitID As Int64 = 0 'added by kanchan on 20100622

                        oDB.Connect(GetConnectionString)
                        query = "SELECT nPatientID FROM TM_TaskMST WHERE nTaskID = " & nTaskId
                        nPatientID = oDB.ExecuteQueryScaler(query)
                        query = "Select nProviderID FROM Patient where nPatientID= " & nPatientID
                        nProviderID = oDB.ExecuteQueryScaler(query)
                        query = "SELECT sNoteExt FROM TM_TaskMST WHERE nTaskID = " & nTaskId
                        sNotes = oDB.ExecuteQueryScaler(query)
                        If sNotes = "" Then
                            'Commented & added by kanchan on 20100622
                            query = "SELECT sFaxTiffFileName FROM TM_TaskMST WHERE nTaskID = " & nTaskId
                            sDrugName = oDB.ExecuteQueryScaler(query)
                            query = "SELECT nReferenceID1 FROM TM_TaskMST WHERE nTaskID = " & nTaskId
                            nDrugVisitID = oDB.ExecuteQueryScaler(query)
                            oDB.Disconnect()
                            If nDrugVisitID <> 0 Then

                                Dim ofrmPrescription As frmPrescription
                                ofrmPrescription = frmPrescription.GetInstance(nDrugVisitID, nPatientID)
                                If IsNothing(ofrmPrescription) = True Then
                                    Exit Sub
                                End If
                                With ofrmPrescription
                                    .WindowState = FormWindowState.Maximized
                                    .BringToFront()
                                    .ShowDialog(IIf(IsNothing(ofrmPrescription.Parent), Me, ofrmPrescription.Parent))

                                End With
                                ''slr free ofrmPrescription
                                If Not IsNothing(ofrmPrescription) Then
                                    ofrmPrescription.Close()
                                    ofrmPrescription.Dispose()
                                    ofrmPrescription = Nothing
                                End If
                            End If
                        Else
                            ArrDrugs = FillDrugList(sNotes)
                            If Not IsNothing(ArrDrugs) Then
                                If ArrDrugs.Count > 0 Then
                                    mdlGeneral.gblnIsDrugSave = False
                                    Dim nVisitID As Int64
                                    nVisitID = GetVisitID(Now.Date, nPatientID)

                                    Dim ofrmPrescription As frmPrescription
                                    ofrmPrescription = frmPrescription.GetInstance(ArrDrugs, nProviderID, nVisitID, nPatientID)
                                    If IsNothing(ofrmPrescription) = True Then
                                        Exit Sub
                                    End If
                                    ''''Dim ofrmPrescription As New frmPrescription(ArrDrugs, nProviderID, nVisitID, nPatientID)
                                    If frmPrescription.IsOpen = False Then

                                        If ofrmPrescription.LockForm(nPatientID) = False Then
                                            ofrmPrescription.Dispose()
                                            ofrmPrescription = Nothing
                                        Else
                                            With ofrmPrescription
                                                .WindowState = FormWindowState.Maximized
                                                .BringToFront()
                                                .ShowDialog(IIf(IsNothing(ofrmPrescription.Parent), Me, ofrmPrescription.Parent))
                                            End With
                                        End If
                                    Else
                                        MessageBox.Show("Rx/Meds screen cannot be opened as it is already open in the background.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    End If

                                    ''slr free ofrmPrescription
                                    If Not IsNothing(ofrmPrescription) Then
                                        ofrmPrescription.Close()
                                        ofrmPrescription.Dispose()
                                        ofrmPrescription = Nothing
                                    End If

                                    If mdlGeneral.gblnIsDrugSave = True Then
                                        If mdlGeneral.gnPrescriptionVisitID <> 0 Then
                                            oDB.Connect(GetConnectionString)
                                            'Commented & added by kanchan on 20100622
                                            'query = "UPDATE TM_TaskMST SET sNoteEXT='',sFaxTiffFileName='" & sNotes & "' where ntaskid= " & nTaskId
                                            query = "UPDATE TM_TaskMST SET sNoteEXT='',nReferenceID1=" & mdlGeneral.gnPrescriptionVisitID & " where ntaskid= " & nTaskId
                                            oDB.ExecuteNonSQLQuery(query)
                                            oDB.Disconnect()
                                            ofrmTask.ReferenceID = mdlGeneral.gnPrescriptionVisitID
                                        End If
                                    End If
                                End If
                            End If
                        End If

                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                    End Try
                    '    'Code End - Added by kanchan on 20100621 for Drugs

                    'Code Start - Added by kanchan on 20100319 for updating the task id 
                ElseIf e2.oTaskType = gloTaskMail.TaskType.Exam Then
                    Try
                        Dim _oDB As New gloStream.gloDataBase.gloDataBase
                        Dim _TaskID As Int64 = ofrmTask.TaskID
                        Dim _Query As String
                        Dim _ExamID As Long = 0
                        Dim dtExam As DataTable = Nothing ''slr new not needed 
                        Dim _DOS As String
                        Dim _ExamName As String
                        Dim _sTemplateName As String
                        Dim _blnFinished As Boolean
                        Dim _nVisitID As Int64
                        Dim _PatientID As Int64



                        _oDB.Connect(GetConnectionString)
                        _ExamID = e2.ReferenceID1
                        If _ExamID > 0 Then

                            _Query = ""
                            _Query = "SELECT ISNULL(nVisitID,0) as VisitID, ISNULL(nPatientID,0) as PatientID ,dtDOS,ISNULL(sExamName,'')  as ExamName ,ISNULL(sTemplateName,'') as TemplateName, bIsFinished FROM PatientExams WHERE nExamID = " & _ExamID
                            dtExam = _oDB.ReadQueryDataTable(_Query)
                            If dtExam IsNot Nothing Then
                                If dtExam.Rows.Count > 0 Then
                                    _nVisitID = dtExam.Rows(0)("VisitID")
                                    _PatientID = dtExam.Rows(0)("PatientID")
                                    _DOS = dtExam.Rows(0)("dtDOS")
                                    _ExamName = dtExam.Rows(0)("ExamName")
                                    _sTemplateName = dtExam.Rows(0)("TemplateName")
                                    _blnFinished = dtExam.Rows(0)("bIsFinished")

                                    '''''''''''''''''
                                    If IsAccess(False, _PatientID) = False Then
                                        Exit Sub
                                    End If
                                    '''''''''''''''''

                                    ShowPastExamFromTask(_ExamID, _PatientID, _nVisitID, _DOS, _ExamName, _sTemplateName, _blnFinished)
                                End If
                            End If
                        End If
                        ''slr free _oDB,dtexam
                        If Not IsNothing(dtExam) Then
                            dtExam.Dispose()
                            dtExam = Nothing
                        End If
                        If Not IsNothing(_oDB) Then
                            _oDB.Dispose()
                            _oDB = Nothing
                        End If

                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                    End Try

                ElseIf e2.oTaskType = gloTaskMail.TaskType.HL7LabInboundFailureNotifyTask Then

                    Dim ogloTask As gloTaskMail.gloTask = New gloTaskMail.gloTask(GetConnectionString)
                    Dim oTask As gloTaskMail.Task ''slr new not needed 
                    oTask = ogloTask.GetTask(e2.TaskID)
                    Dim nTaskDate As Long = oTask.DateCreated
                    Dim dtTaskDate As DateTime
                    If Not IsNothing(ogloTask) Then
                        ogloTask.Dispose()
                        ogloTask = Nothing
                    End If
                    If Not IsNothing(oTask) Then
                        oTask.Dispose()
                        oTask = Nothing
                    End If

                    If nTaskDate > 0 Then

                        dtTaskDate = gloDateMaster.gloDate.DateAsDate(nTaskDate) '' DateAsDateTime(nTaskDate)
                    Else
                        dtTaskDate = Now
                    End If

                    Dim ofrmViewReport As New gloReports.frmInterfacesMessageErrorReport()
                    ofrmViewReport.WindowState = FormWindowState.Normal
                    ofrmViewReport.dtSpecificDateReport = dtTaskDate
                    ofrmViewReport.ShowDialog(IIf(IsNothing(ofrmViewReport.Parent), Me, ofrmViewReport.Parent))

                    ofrmViewReport.Dispose()
                    ofrmViewReport = Nothing

                ElseIf e2.oTaskType = gloTaskMail.TaskType.PatientPortalTask Then

                    Dim ofrmPatientReview As New gloEmdeonInterface.Forms.frmIntuitPatientReview(mdlGeneral.globlnEnableMultipleRaceFeatures, mdlGeneral.gblnUSEINTUITINTERFACE)
                    ofrmPatientReview.WindowState = FormWindowState.Normal
                    ofrmPatientReview.ShowDialog(IIf(IsNothing(ofrmPatientReview.Parent), Me, ofrmPatientReview.Parent))
                    ofrmPatientReview.Dispose()
                    ofrmPatientReview = Nothing
                ElseIf e2.oTaskType = gloTaskMail.TaskType.HL7DocumentTask Then 'Start of code Added by manoj jadhav on 20130219 for diplaying DMS document from task module

                    Dim _query As String = String.Empty
                    Dim _RefranceID1 As Long = 0
                    Dim _RefranceID2 As Long = 0
                    Dim _PatientID As Long = 0
                    Dim _dtResult As DataTable = Nothing
                    Dim _ObjgloDataBase As gloStream.gloDataBase.gloDataBase = Nothing
                    Dim _nResultCount As Integer = 0
                    Try
                        _query = "SELECT nReferenceID1,nReferenceID2, nPatientID FROM dbo.TM_TaskMST WHERE nTaskID = " & e2.TaskID
                        _ObjgloDataBase = New gloStream.gloDataBase.gloDataBase()
                        _ObjgloDataBase.Connect(GetConnectionString())
                        _dtResult = _ObjgloDataBase.ReadQueryDataTable(_query)
                        _ObjgloDataBase.Disconnect()
                        If Not _dtResult Is Nothing AndAlso _dtResult.Rows.Count > 0 Then
                            _RefranceID1 = _dtResult.Rows(0)("nReferenceID1")
                            _RefranceID2 = _dtResult.Rows(0)("nReferenceID2")
                            _PatientID = _dtResult.Rows(0)("nPatientID")
                        End If
                        If _PatientID <= 0 Or _RefranceID1 <= 0 Or _RefranceID2 <= 0 Then
                            Exit Try
                        End If
                        'check for access
                        If Not IsAccess(False, _PatientID) Then
                            Exit Try
                        End If
                        If _RefranceID2 = 1 Then

                            _query = "Select COUNT(eDocumentID) from dbo.eDocument_Details_V3 WHERE eDocumentID =" & _RefranceID1 & " AND PatientID=" & _PatientID & ""
                            _ObjgloDataBase.Connect(GetDMSConnectionString())
                            _nResultCount = Convert.ToInt32(_ObjgloDataBase.ExecuteQueryScaler(_query))
                            _ObjgloDataBase.Disconnect()

                            If _nResultCount <= 0 Then
                                MessageBox.Show("This document does not exists.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Try
                            End If
                        Dim isItDialog As Boolean = True
                            Dim oEDocumentV3 As gloEDocumentV3.gloEDocV3Management = Nothing
                            Try
                                oEDocumentV3 = New gloEDocumentV3.gloEDocV3Management()
                                oEDocumentV3.oPatientExam = New clsPatientExams()
                                oEDocumentV3.oPatientMessages = New clsMessage()
                                oEDocumentV3.oPatientLetters = New clsPatientLetters()
                                oEDocumentV3.oNurseNotes = New clsNurseNotes
                                oEDocumentV3.oHistory = New clsPatientHistory
                                oEDocumentV3.oLabs = New clsLabs
                                oEDocumentV3.oDMS = New gloEDocumentV3.eDocManager.eDocGetList()
                                oEDocumentV3.oRxmed = New clsPatientDetails
                                oEDocumentV3.oOrders = New clsPatientDetails
                                oEDocumentV3.oProblemList = New clsPatientProblemList
                                oEDocumentV3.oCriteria = New DocCriteria
                                oEDocumentV3.oWord = New clsWordDocument
                            oEDocumentV3.ShowEDocument(_PatientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocumentForExternalModule, Me, gloEDocumentV3.Enumeration.enum_OpenExternalSource.ViewTask, _RefranceID1)
                            Catch ex As Exception
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                        Finally
                            If (isItDialog = True) Then
                                If Not oEDocumentV3 Is Nothing Then
                                    If (IsNothing(oEDocumentV3.oPatientExam) = False) Then
                                        DirectCast(oEDocumentV3.oPatientExam, clsPatientExams).Dispose()
                                        oEDocumentV3.oPatientExam = Nothing
                                    End If
                                    If (IsNothing(oEDocumentV3.oPatientMessages) = False) Then
                                        DirectCast(oEDocumentV3.oPatientMessages, clsMessage).Dispose()
                                        oEDocumentV3.oPatientMessages = Nothing
                                    End If
                                    If (IsNothing(oEDocumentV3.oPatientLetters) = False) Then
                                        DirectCast(oEDocumentV3.oPatientLetters, clsPatientLetters).Dispose()
                                        oEDocumentV3.oPatientLetters = Nothing
                                    End If
                                    If (IsNothing(oEDocumentV3.oNurseNotes) = False) Then
                                        DirectCast(oEDocumentV3.oNurseNotes, clsNurseNotes).Dispose()
                                        oEDocumentV3.oNurseNotes = Nothing
                                    End If
                                    If (IsNothing(oEDocumentV3.oHistory) = False) Then
                                        DirectCast(oEDocumentV3.oHistory, clsPatientHistory).Dispose()
                                        oEDocumentV3.oHistory = Nothing
                                    End If
                                    If (IsNothing(oEDocumentV3.oLabs) = False) Then
                                        DirectCast(oEDocumentV3.oLabs, clsLabs).Dispose()
                                        oEDocumentV3.oLabs = Nothing
                                    End If
                                    If (IsNothing(oEDocumentV3.oDMS) = False) Then
                                        DirectCast(oEDocumentV3.oDMS, gloEDocumentV3.eDocManager.eDocGetList).Dispose()
                                        oEDocumentV3.oDMS = Nothing
                                    End If
                                    If (IsNothing(oEDocumentV3.oRxmed) = False) Then
                                        DirectCast(oEDocumentV3.oRxmed, clsPatientDetails).Dispose()
                                        oEDocumentV3.oRxmed = Nothing
                                    End If
                                    If (IsNothing(oEDocumentV3.oOrders) = False) Then
                                        DirectCast(oEDocumentV3.oOrders, clsPatientDetails).Dispose()
                                        oEDocumentV3.oOrders = Nothing
                                    End If
                                    If (IsNothing(oEDocumentV3.oProblemList) = False) Then
                                        DirectCast(oEDocumentV3.oProblemList, clsPatientProblemList).Dispose()
                                        oEDocumentV3.oProblemList = Nothing
                                    End If

                                    If (IsNothing(oEDocumentV3.oCriteria) = False) Then
                                        DirectCast(oEDocumentV3.oCriteria, DocCriteria).Dispose()
                                        oEDocumentV3.oCriteria = Nothing
                                    End If
                                    oEDocumentV3.Dispose()
                                    oEDocumentV3 = Nothing
                                End If
                            End If
                          
                            End Try

                        ElseIf _RefranceID2 = 2 Then

                            _query = "Select COUNT(DicomID)  from dbo.DicomDetails WHERE DicomID=" & _RefranceID1 & " AND nPatientID=" & _PatientID & ""
                            _ObjgloDataBase.Connect(GetConnectionString())
                            _nResultCount = _ObjgloDataBase.ExecuteQueryScaler(_query)
                            _ObjgloDataBase.Disconnect()

                            If _nResultCount <= 0 Then
                                MessageBox.Show("This document does not exists.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Try
                            End If

                            'check if Dicom path is exists or not
                            If String.IsNullOrEmpty(DICOMPath) Then
                                MessageBox.Show("'Set the DICOM path from  Tool->Settings->Server Path.'", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Try
                            End If

                            Dim ofrmDICOM As frmgloDICOM = Nothing
                            Try
                                ofrmDICOM = New frmgloDICOM(_PatientID, "Task", _RefranceID1)
                                ofrmDICOM.WindowState = FormWindowState.Maximized
                                ofrmDICOM.ShowDialog(IIf(IsNothing(ofrmDICOM.Parent), Me, ofrmDICOM.Parent))
                            Catch comex As System.Runtime.InteropServices.COMException
                                MessageBox.Show("The required components for DICOM are missing.  Please install.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                gloAuditTrail.gloAuditTrail.ExceptionLog(comex.ToString, False)
                            Catch ex As Exception
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
                            Finally
                                If Not ofrmDICOM Is Nothing Then
                                    ofrmDICOM.Dispose()
                                    ofrmDICOM = Nothing
                                End If
                            End Try
                        End If
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                    Finally
                        _query = String.Empty
                        _RefranceID1 = 0
                        _RefranceID2 = 0
                        _PatientID = 0
                        _nResultCount = 0
                        If Not _dtResult Is Nothing Then
                            _dtResult.Dispose()
                            _dtResult = Nothing
                        End If
                        If Not _ObjgloDataBase Is Nothing Then
                            _ObjgloDataBase.Dispose()
                            _ObjgloDataBase = Nothing
                        End If
                    End Try 'END of code Added by manoj jadhav on 20130219 for diplaying DMS document from 

                Else
                    If nTaskId <> e2.TaskID Then
                        Try
                            Dim oDB As New gloStream.gloDataBase.gloDataBase
                            Dim query As String
                            oDB.Connect(GetConnectionString)

                            query = "UPDATE PatientsUnmatchedInLab set nTaskId=" & e2.TaskID & " where nTaskId= " & nTaskId
                            oDB.ExecuteNonSQLQuery(query)
                            'Code Start-Added by kanchan on 20100605 for CCD
                            query = "UPDATE CCD_Queue set nTaskId=" & e2.TaskID & " where nTaskId= " & nTaskId
                            oDB.ExecuteNonSQLQuery(query)
                            'Code End-Added by kanchan on 20100605 for CCD
                            oDB.Disconnect()
                            ''slr free oDB
                            If Not IsNothing(oDB) Then
                                oDB.Dispose()
                                oDB = Nothing
                            End If
                        Catch ex As Exception

                        End Try

                    End If

                End If
        End If

        FillTasks()
    End Sub
#End Region

#Region " Navigation & Button Events "

    Private Sub lblTodayDate_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lblTodayDate.MouseClick
        Try
            'isCalendarClicked = True
            Dim pt As New Point(PointToClient(Cursor.Position))
            pnlMntCalendar.Top = pt.Y
            pnlMntCalendar.Left = pt.X - (pnlMntCalendar.Width / 2)
            pnlMntCalendar.Visible = True
            mntCalendar.Select()
            mntCalendar.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mntCalendar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles mntCalendar.MouseDown
        Try
            Dim htInfo As MonthCalendar.HitTestInfo
            htInfo = mntCalendar.HitTest(e.X, e.Y)
            pnlMntCalendar.Visible = True

            If htInfo.HitArea = MonthCalendar.HitArea.Date Then
                currentDate = mntCalendar.SelectionStart
                lblTodayDate.Text = currentDate.ToLongDateString
                pnlMntCalendar.Visible = False
                RefreshAll()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub mntCalendar_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles mntCalendar.LostFocus
    '    pnlMntCalendar.Visible = False
    'End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            pnlMntCalendar.Visible = False
            currentDate = Convert.ToDateTime(lblTodayDate.Text).AddDays(1)
            lblTodayDate.Text = currentDate.ToLongDateString
            RefreshAll()
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPrevious_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrevious.Click
        Try
            pnlMntCalendar.Visible = False
            currentDate = Convert.ToDateTime(lblTodayDate.Text).AddDays(-1)
            lblTodayDate.Text = currentDate.ToLongDateString
            RefreshAll()
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btn_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrevious.MouseHover, btnNext.MouseHover
        If CType(sender, Button).Name = "btnNext" Then
            btnNext.BackgroundImage = Global.gloEMR.My.Resources.ForwardHover
            btnNext.BackgroundImageLayout = ImageLayout.Center
        ElseIf CType(sender, Button).Name = "btnPrevious" Then
            btnPrevious.BackgroundImage = Global.gloEMR.My.Resources.RewindHover
            btnPrevious.BackgroundImageLayout = ImageLayout.Center
        End If
    End Sub

    Private Sub btn_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrevious.MouseLeave, btnNext.MouseLeave
        If CType(sender, Button).Name = "btnNext" Then
            btnNext.BackgroundImage = Global.gloEMR.My.Resources.Forward
            btnNext.BackgroundImageLayout = ImageLayout.Center
        ElseIf CType(sender, Button).Name = "btnPrevious" Then
            btnPrevious.BackgroundImage = Global.gloEMR.My.Resources.Rewind
            btnPrevious.BackgroundImageLayout = ImageLayout.Center
        End If
    End Sub

    Private Sub ts_btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ts_btnClose.Click
        If Me.Cursor = Cursors.Default Then
            Me.Close()
        End If

    End Sub
#End Region

    Private Sub C1Calendar_RowColChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1Calendar.RowColChange
        If C1Calendar.Col >= col_color0 And C1Calendar.Col <= col_color5 Then
            C1Calendar.Col = col_AppPatientName
        End If
    End Sub
    Private Sub ShowPastExamFromTask(ByVal ExamID As Long, ByVal PatientId As Int64, ByVal VisitID As Long, ByVal DOS As String, ByVal ExamName As String, ByVal sTemplateName As String, ByVal blnFinished As Boolean)
        Try
            'If Trim(strPatientFirstName) <> "" Then

            If IsAccess(True, PatientId) = False Then
                Exit Sub
            End If

            If CheckWordForException() = False Then
                Exit Sub
            End If

            If Not blnFinished Then
                Dim objExam As New clsPatientExams
                objExam.SetProviderExam(ExamID)
                objExam.Dispose()
                objExam = Nothing
            End If

            Me.Cursor = Cursors.WaitCursor

            Dim frm As New frmPatientExam(PatientId, VisitID)
            AddHandler frm.FormClosed, AddressOf On_ExamClosed

            With frm
                .Hide()
                .blnModify = True
                .Text = "Past Exams"
                .WindowState = FormWindowState.Maximized
                .pnlPastExamView.Visible = False
                .pnlNewExam.Dock = DockStyle.Fill
                .Splitter1.Visible = False
                .Splitter3.Visible = False
                ._blnIsOpenFromTask = True
                'Dim sender As Object
                '  Dim e As System.EventArgs
                .PatientID = PatientId
                If (.OpenPastExam(ExamID, VisitID, Convert.ToDateTime(DOS), ExamName.Trim, blnFinished, sTemplateName) = True) Then
                    'Bug #48367: gloEMR - Exam - Application displays tamplte name as Exam name.
                    .IsPastExam = True
                    .Show(Me)
                    .BringToFront()
                    If .ExamViewMode Then
                        .ViewExam(ExamID)
                    Else
                        .OpenPastExamContents(ExamID, blnFinished)
                    End If
                    .Select()
                    ofrmTask.Enabled = False
                Else
                    ofrmTask.Enabled = True

                    RemoveHandler frm.FormClosed, AddressOf On_ExamClosed
                    

                    frm.Dispose()
                    frm = Nothing
                End If


            End With
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Me.Cursor = Cursors.Default
    End Sub
    'Against the case no :GLO2009 0002794 and bugzilla Id:3700
    Private Sub On_ExamClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs)
        Dim frm As frmPatientExam = Nothing

        Try
            frm = DirectCast(sender, frmPatientExam)
        Catch ex As Exception

        End Try
        Try
            If (IsNothing(frm) = False) Then
                RemoveHandler frm.FormClosed, AddressOf On_ExamClosed
            End If
            If (IsNothing(frm) = False) Then
                frm.Close()
            End If
            If (IsNothing(frm) = False) Then
                frm.Dispose()
                frm = Nothing
            End If

        Catch ex As Exception

        End Try
        If ofrmTask IsNot Nothing Then
            ofrmTask.Enabled = True
        End If
    End Sub
    'Voice break resolved by making function shared. 20110120
    Public Shared Function IsAccess(Optional ByVal bSkpMsg As Boolean = False, Optional ByVal nPatientID As Long = 0) As Boolean
        '''''''''''''  Integrated by Chetan - to show message when menu items clicked as on 05 Oct 2010
        '''''<><><><><> Check Patient Status <><><><><><>''''  
        ''''''''''''''''''''To Pass nPatientID from Synopsis form
        If nPatientID = 0 Then
            nPatientID = nPatientID
        End If
        IsAccess = True
        If gbAllowEmergencyAccess = False Then
            IsAccess = CheckPatientStatus(nPatientID, , , bSkpMsg)
            ''IsAccess = not CheckPatientStatus(gnPatientID, , , bSkpMsg)=false
        End If

        Return IsAccess
        '''''<><><><><> Check Patient Status <><><><><><>''''
        '''''''''''''  Integrated by Chetan - to implement 'lock status' as on 05 Oct 2010
    End Function

    'Developer: Sanjog Dhamke
    'Date:21 Dec 2011
    'Bug ID/PRD Name/Sales force Case: Lab Usability PRD to open the Emdeon Screen From Lab Task window
    'Reason: To open Emdeon interface for lab task
    Private Sub OpenEmdeon(ByVal TaskId As Int64)

        Try

            Dim oclsDB As New gloStream.gloDataBase.gloDataBase
            Dim nPatientID As Int64
            Dim nProviderID As Int64
            Dim query As String
            Dim nDrugVisitID As Int64 = 0 'added by kanchan on 20100622

            oclsDB.Connect(GetConnectionString)
            query = "SELECT nPatientID FROM TM_TaskMST WHERE nTaskID = " & TaskId
            nPatientID = oclsDB.ExecuteQueryScaler(query)
            query = "Select nProviderID FROM Patient where nPatientID= " & nPatientID
            nProviderID = oclsDB.ExecuteQueryScaler(query)

            Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
            Dim objclsgeneral As New gloEmdeonInterface.Classes.clsGeneral()
            Dim _LoginUserProviderID As Long = 0
            Dim _PatientProviderID As Long = 0

            Dim loopcnt As Int16 = 0
            Dim objClsgloLabPatientLayer As gloEmdeonInterface.Classes.clsgloLabPatientLayer = New gloEmdeonInterface.Classes.clsgloLabPatientLayer()
            Dim objpatient As gloPatient.Patient ''slr new not needed 
            Dim objgloPatient As New gloPatient.gloPatient(GetConnectionString)

            objpatient = objgloPatient.GetPatient(nPatientID)
            ' _LoginUserProviderID = GetProviderIDForUser(_LoginUserID)
            _LoginUserProviderID = GetProviderIDForUser(gnLoginID)
            _PatientProviderID = objpatient.DemographicsDetail.PatientProviderID

            If Not gloEmdeonInterface.Classes.clsEmdeonGeneral.CheckConnectionParameters(GetConnectionString) Then
                MessageBox.Show("Lab Settings have not been configured in gloEMR Admin." + vbCrLf + "Please complete Lab Settings before ordering.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim LabConnectionAvailable As gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity
            LabConnectionAvailable = objclsgeneral.IsInternetConnectionAvailable()
            If LabConnectionAvailable = gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity.Success Then

                If Not compareProvider(_PatientProviderID, _LoginUserProviderID, nPatientID) Then
                    Return
                End If

                Dim _billingStatus As Boolean = False

                Dim objGloPatientLayer = New gloEmdeonInterface.Classes.clsgloLabPatientLayer()
                _billingStatus = objClsgloLabPatientLayer.CheckBillingType(objpatient)

                If _billingStatus = True Then

                    If (gloEmdeonInterface.Classes.clsEmdeonGeneral.IsDemoLab) Then
                        Dim frmLabDemo As New gloEmdeonInterface.Forms.frmLabDemonstration(nPatientID)



                        frmLabDemo.nTaskId = TaskId
                        frmLabDemo.WindowState = FormWindowState.Maximized
                        frmLabDemo.BringToFront()
                        frmLabDemo.ShowDialog(IIf(IsNothing(frmLabDemo.Parent), Me, frmLabDemo.Parent))
                        frmLabDemo.Dispose()
                        frmLabDemo = Nothing
                    Else
                        Dim strQry As String = String.Empty
                        Dim boolPatientReg As [Boolean] = False
                        If ConfirmNull(objpatient.DemographicsDetail.PatientCode.ToString()) Then
                            strQry = "SELECT COUNT(*) FROM PatientExternalCodes INNER JOIN Patient ON PatientExternalCodes.nPatientId = Patient.nPatientID  where PatientExternalCodes.sExternalType = 'EMDEON' AND    Patient.sPatientCode='" & objpatient.DemographicsDetail.PatientCode.ToString().Trim() & "'"
                        End If
                        oDB.Connect(False)

                        For loopcnt = 1 To 3

                            Dim cnt As Int32 = 0
                            cnt = Convert.ToInt32(oDB.ExecuteScalar_Query(strQry))
                            If cnt < 1 Then
                                ' if cnt is greater than zero means patient registered

                                Application.DoEvents()

                                gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_patID = nPatientID

                                boolPatientReg = objClsgloLabPatientLayer.RegisterGloPatient(objpatient, GetConnectionString)

                                If boolPatientReg Then
                                    Exit For
                                End If
                            Else
                                boolPatientReg = True
                                Exit For
                            End If
                        Next

                        If boolPatientReg = True Then
                            Dim objfrmEmdonInterface As New gloEmdeonInterface.Forms.frmEmdeonInterface(nPatientID)
                            objfrmEmdonInterface.LoginProviderID = gnLoginProviderID
                            objfrmEmdonInterface.nTaskID = TaskId
                            objfrmEmdonInterface.WindowState = FormWindowState.Maximized
                            objfrmEmdonInterface.ShowDialog(IIf(IsNothing(objfrmEmdonInterface.Parent), Me, objfrmEmdonInterface.Parent))
                            ''slr free objfrmEmdonInterface
                            If Not IsNothing(objfrmEmdonInterface) Then
                                objfrmEmdonInterface.Close()
                                objfrmEmdonInterface.Dispose()
                                objfrmEmdonInterface = Nothing
                            End If
                        Else

                            If ConfirmNull(gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Identifier.ToString()) Then
                                MessageBox.Show(gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Identifier.ToString().Trim(), "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Else
                                MessageBox.Show("Patient is not registered With Emdeon,please try again.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End If
                        End If

                    End If
                End If
            Else

                If LabConnectionAvailable = gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity.NoInternet Then

                    Dim objFrmConnectionConfirm As New gloEmdeonInterface.Forms.frmConfirmInternetConnection(True)
                    objFrmConnectionConfirm.ShowInTaskbar = False
                    objFrmConnectionConfirm.ShowDialog(IIf(IsNothing(objFrmConnectionConfirm.Parent), Me, objFrmConnectionConfirm.Parent))
                    objFrmConnectionConfirm.Dispose()
                    objFrmConnectionConfirm = Nothing

                ElseIf LabConnectionAvailable = gloEmdeonInterface.Classes.clsGeneral.InternetConnectivity.ServerNotresponding Then
                    Dim objFrmConnectionConfirm As New gloEmdeonInterface.Forms.frmConfirmInternetConnection(False)
                    objFrmConnectionConfirm.ShowInTaskbar = False
                    objFrmConnectionConfirm.ShowDialog(IIf(IsNothing(objFrmConnectionConfirm.Parent), Me, objFrmConnectionConfirm.Parent))
                    objFrmConnectionConfirm.Dispose()
                    objFrmConnectionConfirm = Nothing

                End If
            End If

            'SLR: FRee oclsdb, oDB, objclsgeneral, objClsgloLabPatientLayer, objpatient, objgloPatient 
            If Not IsNothing(oclsDB) Then
                oclsDB.Dispose()
                oclsDB = Nothing
            End If
            If Not IsNothing(objclsgeneral) Then
                objclsgeneral.Dispose()
                objclsgeneral = Nothing
            End If
            If Not IsNothing(objClsgloLabPatientLayer) Then
                objClsgloLabPatientLayer.Dispose()
                objClsgloLabPatientLayer = Nothing
            End If
            If Not IsNothing(objpatient) Then
                objpatient.Dispose()
                objpatient = Nothing
            End If
            If Not IsNothing(objgloPatient) Then
                objgloPatient.Dispose()
                objgloPatient = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Public Function GetProviderIDForUser(ByVal UserID As Int64) As Int64
        Try

            Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
            Dim ProID As Int64 = 0
            Try
                oDB.Connect(False)
                ProID = Convert.ToInt64(oDB.ExecuteScalar_Query("SELECT nProviderID from user_mst where nUserID = " & UserID & ""))
                oDB.Disconnect()
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
                ProID = 0
            Finally
                oDB.Dispose()
            End Try
            Return ProID

        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        End Try
    End Function

    Protected Function ConfirmNull(ByVal strValue As String) As Boolean
        Dim blnCheck As Boolean = False
        Try
            If strValue IsNot Nothing AndAlso strValue.ToString().Trim().Length <> 0 AndAlso strValue.ToString() <> "" Then

                blnCheck = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return blnCheck
    End Function

    Private Function compareProvider(ByVal _PatientProviderID As Int64, ByVal _LoginUserProviderID As Int64, ByVal PatID As Int64) As Boolean
        Dim objClsGeneral As New gloEmdeonInterface.Classes.clsGeneral()
        Dim strProviderName As String = String.Empty
        Dim strLoginUserName As String = String.Empty
        Dim strLabID As String = String.Empty
        Try
            If _PatientProviderID <> 0 Then
                strProviderName = objClsGeneral.GetProviderName(_PatientProviderID, gnClinicID)
            End If
            If _LoginUserProviderID <> 0 Then
                strLoginUserName = objClsGeneral.GetProviderName(_LoginUserProviderID, gnClinicID)
            End If
            If _LoginUserProviderID = 0 Then

                Dim drMesgResult As DialogResult = MessageBox.Show(("The user you are using is not set up as a provider. If you proceed, the lab order " & vbCr & vbLf & "provider will be defaulted to the current patient’s provider '") + strProviderName & "'." & vbCr & vbLf & vbCr & vbLf & "Would you like to proceed with creating a new order?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If drMesgResult = DialogResult.Yes Then
                    strLabID = objClsGeneral.GetProvidergloLabId(_PatientProviderID)
                    If ConfirmNull(strLabID.ToString()) Then
                        Return True
                    Else
                        If MessageBox.Show("The current provider '" & strProviderName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                Else
                    Return False
                End If
            End If

            If _LoginUserProviderID <> _PatientProviderID Then
                Dim dgResult As DialogResult = MessageBox.Show((("This patient is currently assigned to the provider '" & strProviderName & "'.Would " & vbCr & vbLf & "you like to change the patient provider to '") + strLoginUserName & "' ? " & vbCr & vbLf & vbCr & vbLf & "If you select 'No', the lab order will be created for '") + strProviderName & "'.", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                If dgResult = DialogResult.Yes Then
                    If objClsGeneral.changePatientProvider(_LoginUserProviderID, PatID) Then
                        strLabID = objClsGeneral.GetProvidergloLabId(_LoginUserProviderID)
                        If ConfirmNull(strLabID.ToString()) Then
                            Return True
                        Else
                            If MessageBox.Show("The current provider '" & strLoginUserName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                Return True
                            Else
                                Return False
                            End If
                        End If
                    Else
                        Return False
                    End If
                ElseIf dgResult = DialogResult.No Then
                    strLabID = objClsGeneral.GetProvidergloLabId(_PatientProviderID)
                    If ConfirmNull(strLabID.ToString()) Then
                        Return True
                    Else
                        If MessageBox.Show("The current provider '" & strProviderName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Return True
                        Else
                            Return False
                        End If
                    End If
                ElseIf dgResult = DialogResult.Cancel Then
                    Return False

                End If
            End If

            If _LoginUserProviderID = _PatientProviderID Then
                strLabID = objClsGeneral.GetProvidergloLabId(_LoginUserProviderID)
                If ConfirmNull(strLabID.ToString()) Then
                    Return True
                Else
                    If MessageBox.Show("The current provider '" & strLoginUserName & " ' does not have a lab ID set up." & vbCr & vbLf & "If you place a lab order, you will have to select a provider in the labs interface." & vbCr & vbLf & "Would you like to proceed with the lab order? " & vbCr & vbLf & vbCr & vbLf & "To set up a lab ID, go to provider settings.", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Else
                Return False
            End If
            ''slr free objclsgeneral
            If Not IsNothing(objClsGeneral) Then
                objClsGeneral.Dispose()
                objClsGeneral = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return False
        End Try
    End Function
    'End Comment - Sanjog Dhamke on 21 Dec 2011

#Region "Call Generate CCDA from Dashboard"
    'Public Delegate Sub GenerateCDAFromMyDay(ByVal PatientID As Int64)
    'Public Event EvntGenerateCDAFromMyDay(ByVal PatientID As Int64)

    Protected Overridable Sub Raise_EvntGenerateCDAFromMyDay(ByVal PatientID As Int64)
        'RaiseEvent EvntGenerateCDAFromMyDay(PatientID)

        Try
            mdlGeneral.OpenCDA(PatientID)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, True)
        End Try
    End Sub
#End Region
End Class
