Imports System.Data
Imports System.Data.SqlClient
Imports gloUserControlLibrary
Imports System.Windows.Forms.Integration
Imports gloEMRGeneralLibrary.gloEMRLab
Imports gloEMRGeneralLibrary.gloEMRActors
Imports System.IO
Imports gloPatient
Public Class frm_Graph
    Implements IPatientContext
    'Dim GloUC_TransactionHistory As New gloUserControlLibrary.gloUC_TransactionHistory
    Dim el As New ElementHost
    'Dim dt As DataTable
    Dim _PatientId As Long = 0

    ''Public WithEvents objtimeline As timeGetTime   gloPatient.TimeActivityGraphHostPage()
    Public WithEvents objtimeline As TimeActivityGraphHostPage
    Public m_IsReadOnly As Boolean = False
    Private WithEvents _PatientStrip As gloUC_PatientStrip

    ' Dim _patdt As DataTable
    ' Dim oclsViewGraphs As New clsViewGraphs
    '  Dim UCtimeline As gloUCTimeline
    Dim GetDSFromDB As DataSet = Nothing ''Use for Filling Data From DataBase 
    Dim DsSendTimeline As DataSet = Nothing  ''Use for Sending Dataset to Timeline
    Private Sub PnlGraph_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PnlGraph.Paint


    End Sub

    Private Enum TimelineGraphName
        Exams = 1
        Problems = 2
        Medications = 3
        Vitals = 4
        Lab = 5
        Order = 6
    End Enum
    Private Sub loadPatientStrip()
        If IsNothing(_PatientStrip) = False Then
            Me.Controls.Remove(_PatientStrip)
            _PatientStrip.Dispose()
            _PatientStrip = Nothing
        End If
        _PatientStrip = New gloUC_PatientStrip
        _PatientStrip.ShowDetail(_PatientId, gloUC_PatientStrip.enumFormName.Timeline)
        _PatientStrip.Dock = DockStyle.Top
        _PatientStrip.Padding = New Padding(3, 0, 3, 0)


        Me.Controls.Add(_PatientStrip)
        pnlToolstrip.SendToBack()
        PnlGraph.BringToFront()
        If m_IsReadOnly Then
            _PatientStrip.DTPEnabled = False
        End If

    End Sub
    Dim XMLFilePath As String = ""
    Private Sub tblStrip_32_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tblStrip_32.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Close"
                'Try
                '    'File.Delete(objtimeline.strFileName)
                'Catch ex As Exception

                'End Try

                Me.Close()


        End Select
    End Sub

    Public Sub filltimeline()
        fillCUITimelinedata()
        'Dim objtimeline As New gloPatient.TimeActivityGraphHostPage()
        'el.Child = objtimeline
        'el.Dock = DockStyle.Fill
        'PnlGraph.Controls.Clear()
        'PnlGraph.Controls.Add(el)

    End Sub




    'Private Function GetResultsDetailsByResult() As DataTable    chetan commented on 13 oct 2010 stored procedure is written for that 
    '    Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
    '    Dim dt As DataTable
    '    Try

    '        dt = New DataTable
    '        With odb


    '            Dim _strSql As String = "SELECT convert(varchar,Lab_Order_Test_Result.labotr_TestResultDateTime,101) AS ResDate, " _
    '            & " Lab_Order_Test_ResultDtl.labotrd_ResultName, Lab_Order_Test_ResultDtl.labotrd_ResultValue, " _
    '            & " Lab_Order_Test_ResultDtl.labotrd_ResultUnit, Lab_Order_Test_ResultDtl.labotrd_ResultRange, " _
    '            & " Lab_Order_Test_ResultDtl.labotrd_OrderID, Lab_Order_Test_ResultDtl.labotrd_TestID, " _
    '            & " Lab_Order_Test_ResultDtl.labotrd_ResultNameID, Lab_Test_ResultDtl.labtrd_ResultName,Lab_Order_Test_ResultDtl.labotrd_ResultType,Lab_Order_Test_ResultDtl.labotrd_AbnormalFlag " _
    '            & " FROM Lab_Order_Test_ResultDtl LEFT OUTER JOIN Lab_Order_MST ON Lab_Order_Test_ResultDtl.labotrd_OrderID = Lab_Order_MST.labom_OrderID LEFT OUTER JOIN Lab_Order_Test_Result ON Lab_Order_Test_ResultDtl.labotrd_OrderID = Lab_Order_Test_Result.labotr_OrderID AND Lab_Order_Test_ResultDtl.labotrd_TestID = Lab_Order_Test_Result.labotr_TestID AND Lab_Order_Test_ResultDtl.labotrd_TestResultNumber = Lab_Order_Test_Result.labotr_TestResultNumber LEFT OUTER JOIN Lab_Test_ResultDtl ON Lab_Order_Test_ResultDtl.labotrd_TestID = Lab_Test_ResultDtl.labtrd_TestID AND Lab_Order_Test_ResultDtl.labotrd_ResultNameID = Lab_Test_ResultDtl.labtrd_ResultID " _
    '            & " WHERE(Lab_Order_MST.labom_PatientID = " & _PatientID & ") " _
    '            & " ORDER BY Lab_Order_Test_Result.labotr_TestResultDateTime , labotrd_ResultName"

    '            dt = .GetDataTable_Query(_strSql)

    '        End With

    '        Return dt
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '    End Try
    'End Function


    'Dim _strSQL As String = ""
    'Dim oDB As New gloStream.gloDataBase.gloDataBase
    'Dim oDataReader As SqlClient.SqlDataReader
    'Dim CriteriaID As Long = 0            '''''Previously it was declared as integer changed by Anil on 29/10/2007 toresolve bug no-436
    'Dim _Result As Boolean = True
    'Dim _Count As Long = 0

    'Dim Conn As SqlConnection
    'Dim cmd As SqlCommand

    '            Try

    ''connect to the database
    '                Conn = New SqlConnection(GetConnectionString())
    '                Conn.Open()
    '                oDB.Connect(GetConnectionString)

    ''extract the criteria id from the table for the given criteria name
    '                _strSQL = "SELECT dm_mst_Id FROM DM_Criteria_MST where dm_mst_CriteriaName = '" & oCriteriaName & "'"
    '                cmd = New SqlCommand(_strSQL, Conn)

    '                CriteriaID = cmd.ExecuteScalar()  'Val(oDB.ExecuteQueryScaler(_strSQL))
    '                Conn.Close()
    ''set the query string
    '                _strSQL = "SELECT COUNT(DM_TransId) FROM DM_Patient where DM_nCriteriaID =" & CriteriaID
    ''execute the query and return a datareader
    '                _Count = oDB.ExecuteQueryScaler(_strSQL)









    Public Sub New()
        ' _patdt = labdt
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Public Sub New(ByVal PatientId As Long)
        ' _patdt = labdt
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _PatientId = PatientId
    End Sub

    Public Sub fillListtimeline()
        'Me.Text = "Timeline Graph"
        'Dim objclsex As New clsPatientExams
        'Dim exdt As DataTable = objclsex.GetPatientExam(_PatientID)
        'UCtimeline = New gloUCTimeline(exdt, gnVisitID)

        'el.Child = UCtimeline

        'el.Dock = DockStyle.Fill
        'PnlGraph.Controls.Clear()
        'PnlGraph.Controls.Add(el)


    End Sub

    Private Sub frm_Graph_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frm_Graph_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave

    End Sub
    Private Sub frm_Graph_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Text = "Timeline"


        Getdata()

        Dim myEventHandler As gloPatient.TimeActivityGraphHostPage.onProblemClick = AddressOf OpenProblemlist

        fillCUITimelinedata() '' Use for Filling Timeline Graph Data
        If (IsNothing(objtimeline) = False) Then
            RemoveHandler objtimeline.onProblemEventclick, myEventHandler

            objtimeline = Nothing
        End If
        objtimeline = New gloPatient.TimeActivityGraphHostPage(DsSendTimeline)
        '10-May-13 Aniket: Disable Exam Open Screen from Timeline Bug 50458
        'AddHandler objtimeline.onclick, AddressOf Openexam
        AddHandler objtimeline.onProblemEventclick, myEventHandler
        myEventHandler = Nothing

        el.Child = objtimeline
        el.Dock = DockStyle.Fill
        PnlGraph.Controls.Add(el)

        Call loadPatientStrip()
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, _PatientId, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Public Sub OpenProblemlist()
        Dim blnRecordLock As Boolean = False
        If gblnRecordLocking = True Then
            Dim mydt As mytable = Scan_n_Lock_Transaction(TrnType.ProblemList, _PatientId, 0, Now)
            If mydt.Code <> gstrLoginName OrElse mydt.Description <> gstrClientMachineName Then
                If MessageBox.Show("This patients problem lists are being modified by " & mydt.Code & " on " & mydt.Description & ". You cannot Modify it now. Do you want to open it?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    blnRecordLock = True
                Else
                    mydt.Dispose()
                    mydt = Nothing
                    Exit Sub
                End If
            End If
            mydt.Dispose()
            mydt = Nothing
        End If


        Dim _problemID As Int64 = CType(objtimeline.ProblemID, Int64)    'ProblemID  
        Dim _visitID As Int64 = CType(objtimeline.VisitID, Int64)      'VisitID  
        Dim frm As New frmProblemList(_problemID, _visitID, blnRecordLock)
        frm.MdiParent = Me.MdiParent
        frm.WindowState = FormWindowState.Maximized
        frm.ShowMessageForPendingReconciliation()
        frm.Show()



    End Sub
    Public Sub Openexam()



        Try



            If CheckWordForException() = False Then
                Exit Sub
            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try


        'Dim objSender As Object
        'Dim objE As EventArgs
        Dim nPastExamID As Long
        Dim nVisitID As Long
        Dim dtDOS As DateTime
        Dim strExamName As String
        Dim blnFinished As Boolean
        Me.Cursor = Cursors.WaitCursor

        ''Dim em As System.Windows.Forms.MouseEventArgs
        nPastExamID = Convert.ToInt64(objtimeline.ExamID)
        nVisitID = Convert.ToInt64(objtimeline.VisitID)
        dtDOS = Convert.ToDateTime(objtimeline.ExamDos)
        strExamName = objtimeline.ExamName
        blnFinished = objtimeline.ExamStatus '' False

        '  Me.Hide()


        If Not blnFinished Then
            Dim objExam As New clsPatientExams
            objExam.SetProviderExam(nPastExamID)
            objExam.Dispose()
            objExam = Nothing
        End If

        Dim frm As New frmPatientExam
        With frm
            .Hide()
            .blnModify = True
            .Text = "Past Exams"
            ''    .lblPatientName.Text = dgData.Item(dgData.CurrentRowIndex, 1)
            .PatientID = _PatientId

            .pnlPastExam.Visible = False
            .pnlPastExamView.Visible = True
            ' .chkShowPreview.Visible = True
            'sarika 20081120 -- exam is opened only if user wants to view it
            If (.OpenPastExam(nPastExamID, nVisitID, dtDOS, strExamName, blnFinished, objtimeline.sTemplateName)) = True Then

                '---

                '   Me.Hide()

                '.IsPastExam = True
                .MdiParent = Me.ParentForm
                CType(Me.ParentForm, MainMenu).ShowHideMainMenu(False, False)
                CType(Me.ParentForm, MainMenu).pnlMainToolBar.Visible = False

                .Show()
                If .ExamViewMode Then
                    .ViewExam(nPastExamID)
                Else
                    .OpenPastExamContents(nPastExamID, blnFinished)
                End If
            Else
               

                frm.Dispose()
                frm = Nothing
            End If
        End With


        Me.Cursor = Cursors.Default

        'Dim objpe As New frmPatientExam
        'objpe.OpenPastExam(Convert.ToInt64(objtimeline.ExamID), gnVisitID, Convert.ToDateTime(objtimeline.ExamDos), objtimeline.ExamName, True) ' objtimeline.ExamStatus)
        'objpe.ShowDialog()
    End Sub
    '  Public Sub fillmeditationdata()
    '      Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
    '      Dim dt As DataTable
    '      Dim _strSql As String = ""
    '      Try

    '          dt = New DataTable
    '          With odb

    '              'Dim _strSql As String = "select  row_number() over( order by nPrescriptionid)as RowId,isnull(nPrescriptionid,'') as MedicationId,isnull(sMedication,'')+'-'+isnull(sDrugForm,'')+'-'+isnull(sroute,'')+'-'+isnull(sFrequency,'')+'-'+isnull(sduration,'') as DrugsData," _
    '              '& " '322277008' as VMPID,'' as VTMID,'' as AMPID,isnull(sFrequency,'') as Frequency, isnull(sAmount,'0') as FrequencyAmount,'385024007' as Form," _
    '              '& " 'suspension' as FormName,isnull(sDosage,'') as Strength,  isnull(sDosage,'') as DOSEQTY,isnull(sDuration,'') as DoseDuration,  isnull(sAmount,'0') as Rate," _
    '              '& " isnull(sDuration,'') as CourseDuration,'26643006' as Route,   'oral' as RouteName," _
    '              '& " '' as  Site,'' as Method,Provider_MSt.sFirstName as Prescriber, isnull(dtStartDate,dbo.gloGetDate()) as StartDate, " _
    '              '& " isnull(dtStartDate,dbo.gloGetDate()) as ReviewDate,  isnull(dtEndDate,dbo.gloGetDate()) as StopDate,'Started' as MedicationStatus, " _
    '              '& " isnull(dtStartDate,dbo.gloGetDate()) as StatusDate,'regular' as MedicationType,'Treatment of fever'as Reason, isnull(sPrescriberNotes,'') " _
    '              '& " as Instructions,nPrescriptionid as PrescriptionId,'0' as Patient_Id from Prescription inner join  Provider_MST on " _
    '              '& " Provider_MST.nProviderId = Prescription.nProviderId "



    '              '  Dim _strSql As String = "select  row_number() over( order by nPrescriptionid)as RowId,isnull(cast(nPrescriptionid as varchar),'') as MedicationId,isnull(sMedication,'')+'-'+isnull(sDrugForm,'')+'-'+isnull(sroute,'')+'-'+isnull(sFrequency,'')+'-'+isnull(sduration,'') as DrugsData," _
    '              '& " '322277008' as VMPID,'' as VTMID,'' as AMPID,isnull(sFrequency,'') as Frequency, isnull(sAmount,'0') as FrequencyAmount,'385024007' as Form," _
    '              '& " 'suspension' as FormName,isnull(sDosage,'') as Strength,  isnull(sDosage,'') as DOSEQTY,isnull(sDuration,'') as DoseDuration,  isnull(sAmount,'0') as Rate," _
    '              '& " isnull(sDuration,'') as CourseDuration,'26643006' as Route,   'oral' as RouteName," _
    '              '& " '' as  Site,'' as Method,Provider_MSt.sFirstName as Prescriber, isnull(dtStartDate,dbo.gloGetDate()) as StartDate, " _
    '              '& " isnull(dtStartDate,dbo.gloGetDate()) as ReviewDate,  isnull(dtEndDate,dbo.gloGetDate()) as StopDate,'Started' as MedicationStatus, " _
    '              '& " isnull(dtStartDate,dbo.gloGetDate()) as StatusDate,'regular' as MedicationType,'Treatment of fever' as Reason, isnull(Rx_sPrescriberNotes,'') " _
    '              '& " as Instructions,isnull(nPrescriptionid,'') as PrescriptionId,'0' as Patient_Id from Medication inner join  Provider_MST on " _
    '              '& " Provider_MST.nProviderId = Medication.Rx_nProviderId"
    '              ' isnull(sReason,'') as Reason


    '              _strSql = "select  row_number() over( order by nPrescriptionid)as RowId," _
    '& " isnull(cast(nPrescriptionid as varchar),'') as MedicationId," _
    '& "isnull(sMedication,'')+'-'+isnull(sDrugForm,'')+'-'+isnull(sroute,'')+'-'+isnull(sFrequency,'')+'-'+isnull(sduration,'') as DrugsData," _
    '                & " '322277008' as VMPID,'' as VTMID,'' as AMPID,isnull(sFrequency,'') as Frequency, isnull(sAmount,'0') as FrequencyAmount," _
    '                & " '385024007' as Form, 'suspension' as FormName,isnull(sDosage,'') as Strength,  isnull(sDosage,'') as DOSEQTY," _
    '& " isnull(sDuration,'') as DoseDuration,  isnull(sAmount,'0') as Rate, isnull(sDuration,'') as CourseDuration,'26643006' as Route, " _
    '                & " 'oral' as RouteName, '' as  Site,'' as Method,Provider_MSt.sFirstName as Prescriber, isnull(dtStartDate,dbo.gloGetDate()) as StartDate, " _
    '  & " isnull(dtStartDate,dbo.gloGetDate()) as ReviewDate,  isnull(dtEndDate,dbo.gloGetDate()) as StopDate,isnull(sstatus,'') as MedStatus,'Started' as MedicationStatus, " _
    '& " isnull(dtStartDate,dbo.gloGetDate()) as StatusDate,'regular' as MedicationType,isnull(sReason,'') as Reason, " _
    '& " ''  as Instructions,nPrescriptionid as PrescriptionId,'0' as Patient_Id from Medication inner join  Provider_MST on  Provider_MST.nProviderId = Medication.Rx_nProviderId "
    '              'and Medication.npatientiD=" + Convert.ToString(_PatientID) + ""



    '              dt = .GetDataTable_Query(_strSql)

    '          End With
    '          '  dt.ReadXml("f:\\BabyEvansData.XML")
    '          Dim ds As New DataSet
    '          ds.ReadXml(XMLFilePath & "\\BabyEvansData.XML")
    '          ds.Tables(1).Rows.Clear()
    '          For Each dr As DataRow In dt.Rows
    '              ds.Tables("Medication").ImportRow(dr)
    '          Next
    '          ds.WriteXml(XMLFilePath & "\\BabyEvansData.XML")
    '          Dim dt2 As DataTable = ds.Tables(0)
    '      Catch ex As Exception
    '          MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)

    '      End Try
    '  End Sub

    Public Sub fillCUITimelinedata()

        '  Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer

        Dim _strSql As String = ""

        Dim cntid As Integer = 1
        Dim itemcnt As Integer = 1
        Dim ds As New DataSet
        Try
            'Dim secExamId As Integer = 0
            '     ds.ReadXml(Application.StartupPath & "\\XMLFile" & "\\TimelineData1.XML")  '' reading schema of Timelinedata



            Dim dtScenario As DataTable = New DataTable
            dtScenario.TableName = "Scenario"
            dtScenario.Columns.Add("Scenario_Id", GetType(Int64))
            dtScenario.Columns.Add("PatientId", GetType(Integer))
            dtScenario.Columns.Add("Description", GetType(String))
            dtScenario.Columns.Add("BaseDate", GetType(DateTime))
            dtScenario.Columns.Add("TimeFrequencySelectedIndex", GetType(Integer))

            ds.Tables.Add(dtScenario)

            Dim dtSection As DataTable = New DataTable
            dtSection.TableName = "Section"
            dtSection.Columns.Add("Section_Id", GetType(Int64))
            dtSection.Columns.Add("Name", GetType(String))
            dtSection.Columns.Add("Scenario_Id", GetType(Int64))

            ds.Tables.Add(dtSection)
            'Dim drrel As New DataRelation("scesec", dtScenario.Columns("Scenario_Id"), dtSection.Columns("Scenario_Id"))


            Dim dtrow1 As DataTable = New DataTable
            dtrow1.TableName = "Row"
            dtrow1.Columns.Add("Row_Id", GetType(Int64))

            dtrow1.Columns.Add("Id", GetType(Int64))
            dtrow1.Columns.Add("Name", GetType(String))
            dtrow1.Columns.Add("Background", GetType(String))
            dtrow1.Columns.Add("MaxLabelStackLevels", GetType(String))
            dtrow1.Columns.Add("Description", GetType(String))
            dtrow1.Columns.Add("ShowLabelOvercrowdingNotifications", GetType(String))
            dtrow1.Columns.Add("Section_Id", GetType(Int64))

            ds.Tables.Add(dtrow1)
            'Dim drrel2 As New DataRelation("secrow", dtSection.Columns("Section_Id"), dtrow1.Columns("Section_Id"))

            Dim dtitem1 As DataTable = New DataTable
            dtitem1.TableName = "Item"
            dtitem1.Columns.Add("FluidStrength", GetType(String))

            dtitem1.Columns.Add("Brand", GetType(String))
            dtitem1.Columns.Add("Form", GetType(String))
            dtitem1.Columns.Add("DoseLabel", GetType(String))
            dtitem1.Columns.Add("Dose", GetType(String))
            dtitem1.Columns.Add("Route", GetType(String))
            dtitem1.Columns.Add("ExamID", GetType(String))
            dtitem1.Columns.Add("ExamStatus", GetType(String))
            dtitem1.Columns.Add("VisitID", GetType(String))
            dtitem1.Columns.Add("sTemplateName", GetType(String))
            dtitem1.Columns.Add("Frequency", GetType(String))
            dtitem1.Columns.Add("Item_Id", GetType(Int64))
            dtitem1.Columns.Add("DoseDuration", GetType(String))
            dtitem1.Columns.Add("SolidStrength", GetType(String))
            dtitem1.Columns.Add("Units", GetType(String))
            dtitem1.Columns.Add("UnitsDescription", GetType(String))
            dtitem1.Columns.Add("YAxisMaxValue", GetType(Single))
            dtitem1.Columns.Add("YAxisMinValue", GetType(Single))
            dtitem1.Columns.Add("YAxisPadding", GetType(Single))
            dtitem1.Columns.Add("YAxisMajorInterval", GetType(Single))
            dtitem1.Columns.Add("YAxisIntervalMinimumHeight", GetType(Single))
            dtitem1.Columns.Add("Height", GetType(Integer))
            dtitem1.Columns.Add("ShowNormalRange", GetType(Boolean))
            dtitem1.Columns.Add("NormalRangeDescription", GetType(String))
            dtitem1.Columns.Add("NormalRangeMinimumValue", GetType(Single))
            dtitem1.Columns.Add("NormalRangeMaximumValue", GetType(Single))
            dtitem1.Columns.Add("NormalRangeBrush", GetType(String))
            dtitem1.Columns.Add("HoverBackground", GetType(String))
            dtitem1.Columns.Add("InterpolationLineColor", GetType(String))
            dtitem1.Columns.Add("DataMarkerTemplate", GetType(String))
            dtitem1.Columns.Add("Style", GetType(String))
            dtitem1.Columns.Add("PointTemplate", GetType(String))
            dtitem1.Columns.Add("LabelTemplate", GetType(String))
            dtitem1.Columns.Add("Comments", GetType(String))
            dtitem1.Columns.Add("Name", GetType(String))
            dtitem1.Columns.Add("StartDate", GetType(Date))
            dtitem1.Columns.Add("EndDate", GetType(Date))
            dtitem1.Columns.Add("Type", GetType(String))
            dtitem1.Columns.Add("Row_Id", GetType(Int64))
            ds.Tables.Add(dtitem1)
            'Dim drrel3 As New DataRelation("rowitem", dtrow1.Columns("Row_Id"), dtitem1.Columns("Row_Id"))


            Dim dtevents As DataTable = New DataTable
            dtevents.TableName = "Events"
            dtevents.Columns.Add("Events_Id", GetType(Int64))
            dtevents.Columns.Add("Item_Id", GetType(Int64))
            ds.Tables.Add(dtevents)
            'Dim drrel4 As New DataRelation("rowevts", dtitem1.Columns("Item_Id"), dtevents.Columns("Item_Id"))

            Dim dtevent As DataTable = New DataTable
            dtevent.TableName = "Event"
            dtevent.Columns.Add("Comments", GetType(String))
            dtevent.Columns.Add("Clinician", GetType(String))
            dtevent.Columns.Add("Type", GetType(String))
            dtevent.Columns.Add("ActualStartDate", GetType(Date))
            dtevent.Columns.Add("ActualEndDate", GetType(Date))
            dtevent.Columns.Add("Status", GetType(String))
            dtevent.Columns.Add("PlannedStartDate", GetType(Date))
            dtevent.Columns.Add("Y1", GetType(Single))
            dtevent.Columns.Add("Y2", GetType(Single))
            dtevent.Columns.Add("Events_Id", GetType(Int64))
            ds.Tables.Add(dtevent)
            'Dim drrel5 As New DataRelation("evtsevt", dtevents.Columns("Events_Id"), dtevent.Columns("Events_Id"))


            Dim drscen As DataRow = ds.Tables("Scenario").NewRow()

            drscen("BaseDate") = DateTime.Now.ToString()
            drscen("Scenario_Id") = 0
            drscen("PatientId") = 1
            drscen("Description") = "Secondary Care Data"

            drscen("TimeFrequencySelectedIndex") = 19

            ds.Tables("Scenario").Rows.Add(drscen) '   = DateTime.Now.ToString()

            Dim dt1 As DataTable = ds.Tables("Section")

            'ds.Tables("Event").Rows.Clear()   ''Clearing All tables
            'ds.Tables("Events").Rows.Clear()
            'ds.Tables("Item").Rows.Clear()
            'ds.Tables("Row").Rows.Clear()
            'ds.Tables("Section").Rows.Clear()

            Dim drexam As DataRow = ds.Tables("Section").NewRow()
            drexam(0) = TimelineGraphName.Exams
            drexam("Name") = "Exams"
            drexam("Scenario_Id") = "0"
            ds.Tables("Section").Rows.Add(drexam)

            Dim drprob As DataRow = ds.Tables("Section").NewRow()
            drprob(0) = TimelineGraphName.Problems
            drprob("Name") = "Problems"
            drprob("Scenario_Id") = "0"
            ds.Tables("Section").Rows.Add(drprob)

            Dim drmed As DataRow = ds.Tables("Section").NewRow()
            drmed(0) = TimelineGraphName.Medications
            drmed("Name") = "Medications"
            drmed("Scenario_Id") = "0"
            ds.Tables("Section").Rows.Add(drmed)

            Dim drmeas As DataRow = ds.Tables("Section").NewRow()
            drmeas(0) = TimelineGraphName.Vitals
            drmeas("Name") = "Vitals"
            drmeas("Scenario_Id") = "0"

            ds.Tables("Section").Rows.Add(drmeas)

            Dim drorders As DataRow = ds.Tables("Section").NewRow()
            drorders(0) = TimelineGraphName.Order
            drorders("Name") = "Lab Orders"
            drorders("Scenario_Id") = "0"
            ds.Tables("Section").Rows.Add(drorders)


            Dim drlabs As DataRow = ds.Tables("Section").NewRow()
            drlabs(0) = TimelineGraphName.Lab
            drlabs("Name") = "Labs"
            drlabs("Scenario_Id") = "0"
            ds.Tables("Section").Rows.Add(drlabs)

            '   secExamId = Convert.ToInt32(drexam(0))
            'Dim dt2 As DataTable = ds.Tables(4)
            'Dim dte As DataTable = ds.Tables(5)
            'Dim dtrow As DataTable = ds.Tables(2)
            'Dim dtitem As DataTable = ds.Tables(3)
            Try


                ds.Tables("Scenario").Rows(0)("BaseDate") = DateTime.Now.ToString()
                ds.Tables("Scenario").Rows(0)("TimeFrequencySelectedIndex") = "17"
            Catch ex As Exception
            End Try


            Dim objclsex As New clsPatientExams
            Dim ptexdt As DataTable = Nothing
            ptexdt = GetDSFromDB.Tables(0)  'Getdata("gsp_getTimelineExamDetails")
            ''objclsex.GetPatientExam(gnPatientID)

            If Not IsNothing(ptexdt) Then
                For Each drc As DataRow In ptexdt.Rows
                    If Not IsNothing(drc("ExamName")) Then '' chetan added on 14 oct to not show empty exam name  
                        If drc("ExamName").ToString().Trim() <> "" Then '' chetan added on 14 oct to not show empty exam name  

                            Dim dr As DataRow = ds.Tables("Row").NewRow
                            dr("Row_Id") = cntid.ToString()
                            dr("Id") = cntid.ToString()
                            dr("Name") = drc("ExamName")   '' Setting Name Exam Graph 

                            dr("Background") = "#F2F2F2"  '' Setting Color for Background for Exam Graph 

                            dr("Section_Id") = TimelineGraphName.Exams   '' Setting SectionId  for Exam Graph 

                            ds.Tables("Row").Rows.Add(dr)

                            Dim dritem As DataRow = ds.Tables("Item").NewRow

                            dritem("Item_Id") = itemcnt.ToString()
                            dritem("Name") = drc("ExamName").ToString()
                            dritem("ExamID") = drc("ExamID").ToString()
                            dritem("ExamStatus") = drc("IsFinished").ToString()
                            dritem("VisitID") = drc("VisitID").ToString()
                            dritem("sTemplateName") = drc("sTemplateName").ToString()
                            dritem("Style") = "{StaticResource OverriddenDefaultStyle}"  '' added on 16-Oct-2010

                            If drc("dos").ToString().Trim() <> "" And drc("dos").ToString().Trim() <> "01 Jan 1900" Then
                                dritem("StartDate") = drc("dos").ToString()
                                dritem("EndDate") = drc("dos").ToString()
                            End If

                            dritem("Row_Id") = cntid.ToString()

                            ds.Tables("Item").Rows.Add(dritem)
                            cntid = cntid + 1
                            itemcnt = itemcnt + 1
                        End If
                    End If
                Next
            End If



            Dim dtProblem As DataTable = Nothing
            dtProblem = GetDSFromDB.Tables(1)  'Getdata("gsp_getTimelineProblemDetails")

            'Problems
            For Each drc As DataRow In dtProblem.Rows
                Dim dr As DataRow = ds.Tables("Row").NewRow
                dr("Row_Id") = cntid.ToString()
                dr("Id") = cntid.ToString()
                dr("Name") = drc("Name")
                dr("Background") = "#F2F2F2"
                dr("Section_Id") = TimelineGraphName.Problems

                ds.Tables("Row").Rows.Add(dr)

                Dim dritem As DataRow = ds.Tables("Item").NewRow

                dritem("Item_Id") = itemcnt.ToString()
                dritem("Name") = drc("Name").ToString()
                dritem("ExamID") = drc("nProblemId").ToString()

                dritem("VisitID") = drc("nVisitID").ToString()


                If drc("StartDate").ToString().Trim() <> "" And drc("StartDate").ToString().Trim() <> "01 Jan 1900" Then
                    dritem("StartDate") = drc("StartDate").ToString()
                End If
                If drc("EndDate").ToString().Trim() <> "" And drc("EndDate").ToString().Trim() <> "01 Jan 1900" Then
                    dritem("EndDate") = drc("EndDate").ToString()
                End If
                dritem("Type") = ""
                dritem("Row_Id") = cntid.ToString()

                ds.Tables("Item").Rows.Add(dritem)
                cntid = cntid + 1
                itemcnt = itemcnt + 1
            Next


            Dim meddt As DataTable = Nothing
            meddt = GetDSFromDB.Tables(2)  'Getdata("gsp_getTimelineMedicationdata")
            If Not IsNothing(meddt) Then
                For Each drc As DataRow In meddt.Rows
                    Dim dr As DataRow = ds.Tables("Row").NewRow
                    dr("Row_Id") = cntid.ToString()
                    dr("Id") = cntid.ToString()
                    dr("Name") = drc("MedName")
                    dr("Background") = "#F2F2F2"
                    dr("Section_Id") = TimelineGraphName.Medications
                    ds.Tables("Row").Rows.Add(dr)

                    Dim dritem As DataRow = ds.Tables("Item").NewRow
                    'dritem(0) =  itemcnt.ToString()
                    dritem("Item_Id") = itemcnt.ToString()
                    dritem("Name") = drc("MedName").ToString()
                    If drc("StartDate").ToString().Trim() <> "" And drc("StartDate").ToString().Trim() <> "01 Jan 1900" Then
                        dritem("StartDate") = drc("StartDate").ToString()
                    End If
                    If drc("StopDate").ToString().Trim() <> "" And drc("StopDate").ToString().Trim() <> "01 Jan 1900" Then
                        dritem("EndDate") = drc("StopDate").ToString()
                    End If
                    dritem("Type") = ""
                    dritem("Form") = drc("FormName").ToString()
                    dritem("DoseLabel") = "Dose"
                    dritem("Dose") = drc("DOSEQTY").ToString()
                    dritem("Route") = drc("RouteName").ToString()
                    dritem("Frequency") = drc("Frequency").ToString()
                    dritem("Row_Id") = cntid.ToString()
                    dritem("Style") = "{StaticResource OverriddenDefaultStyle}"  '' added on 16-Oct-2010

                    ds.Tables("Item").Rows.Add(dritem)
                    cntid = cntid + 1
                    itemcnt = itemcnt + 1
                Next
            End If
            objclsex.Dispose()

            'fill temp  graph

            ' ds.WriteXml(Application.StartupPath & "\\XMLFile" & "\\TimelineData.XML")

        Catch ex As Exception

        End Try
        fillgraph(cntid, itemcnt, ds)
    End Sub
    Public Sub Getdata()
        Try


            Dim objda As New SqlDataAdapter("gsp_GetAllTimelineGraph", GetConnectionString)
            Dim ds As New DataSet
            objda.SelectCommand.CommandType = CommandType.StoredProcedure

            objda.SelectCommand.Parameters.AddWithValue("@patientId", _PatientId)
            objda.Fill(ds)
            objda.Dispose()
            objda = Nothing
            If (IsNothing(GetDSFromDB) = False) Then
                GetDSFromDB.Dispose()
                GetDSFromDB = Nothing
            End If
            GetDSFromDB = ds

        Catch ex As Exception
            'Return Nothing
        End Try
    End Sub

    Public Sub fillgraph(ByVal cntid As Integer, ByVal itemcnt As Integer, ByVal ds As DataSet)
        'Dim dt_selectedResult As New DataTable
        ' Dim oclsviewGraphs As New clsViewGraphs
        'If (IsNothing(respdt) = False) Then
        '    respdt.Dispose()
        '    respdt = Nothing
        'End If
        Dim respdt As DataTable
        respdt = GetDSFromDB.Tables(3)  'oclsviewGraphs.GetRespiratoryData(_PatientId)


        Dim tempid As Integer = 0
        Dim respid As Integer = 0
        Dim evtid As Integer = 1
        Try


            If respdt.Rows.Count > 0 Then

                ds.Tables("Events").Rows.Clear()
                ds.Tables("Event").Rows.Clear()
                Dim flagtemp As Integer = 0
                Dim flagresp As Integer = 0
                For Each drc As DataRow In respdt.Rows

                    If drc("dtvitaldate").ToString().Trim() <> "" And drc("dtvitaldate").ToString().Trim() <> "01 Jan 1900" Then
                        If Convert.ToInt32(drc("Celcius")) <> -1 Then
                            If flagtemp = 0 Then

                                Dim dr As DataRow = ds.Tables("Row").NewRow
                                dr("Row_Id") = cntid.ToString()
                                dr("Id") = cntid.ToString()
                                dr("Name") = "Temperature"
                                dr("Background") = "#EBCCCC"
                                dr("Section_Id") = TimelineGraphName.Vitals
                                ds.Tables("Row").Rows.Add(dr)

                                Dim dritem As DataRow = ds.Tables("Item").NewRow
                                dritem("Item_Id") = itemcnt.ToString()
                                dritem("Units") = "°C"
                                dritem("UnitsDescription") = "°C:Degree Celcius"
                                dritem("YAxisMaxValue") = "45"
                                dritem("YAxisMinValue") = "30"
                                dritem("YAxisPadding") = "11"
                                dritem("YAxisMajorInterval") = "1"
                                dritem("YAxisIntervalMinimumHeight") = "20"
                                dritem("Height") = "100"
                                dritem("ShowNormalRange") = "True"
                                dritem("NormalRangeDescription") = "Adult normal range"
                                dritem("NormalRangeMinimumValue") = "35"
                                dritem("NormalRangeMaximumValue") = "37"
                                dritem("NormalRangeBrush") = "#FEFCE9"
                                dritem("HoverBackground") = "#B84E4E"
                                dritem("InterpolationLineColor") = "#990000"
                                dritem("DataMarkerTemplate") = "{StaticResource Square}"
                                dritem("Style") = "{StaticResource OverriddenDefaultStyle}"
                                dritem("Row_Id") = cntid.ToString()
                                ds.Tables("Item").Rows.Add(dritem)

                                Dim drevts As DataRow = ds.Tables("Events").NewRow()
                                drevts("Events_Id") = evtid.ToString()
                                drevts("Item_Id") = itemcnt.ToString()
                                ds.Tables("Events").Rows.Add(drevts)


                                tempid = evtid
                                evtid = evtid + 1
                                cntid = cntid + 1
                                itemcnt = itemcnt + 1
                                flagtemp = 1

                            End If


                            Dim drevt As DataRow = ds.Tables("Event").NewRow()
                            drevt("Type") = "GraphData"
                            drevt("ActualStartDate") = drc("dtvitaldate").ToString
                            drevt("Y1") = (Math.Round(drc("Celcius"), 2)).ToString()
                            drevt("Events_Id") = tempid.ToString()
                            ds.Tables("Event").Rows.Add(drevt)
                        End If
                        If Convert.ToInt32(drc("drespiratoryRate")) <> -99 Then
                            If flagresp = 0 Then

                                Dim dr As DataRow = ds.Tables("Row").NewRow
                                dr("Row_Id") = cntid.ToString()
                                dr("Id") = cntid.ToString()
                                dr("Name") = "Respiratory Rate"
                                dr("Background") = "#E7D3E7"
                                dr("Section_Id") = TimelineGraphName.Vitals

                                ds.Tables("Row").Rows.Add(dr)

                                Dim dritem As DataRow = ds.Tables("Item").NewRow
                                dritem("Item_Id") = itemcnt.ToString()
                                dritem("Units") = "Breaths Per Minute"

                                dritem("YAxisMaxValue") = "80"
                                dritem("YAxisMinValue") = "0"
                                dritem("YAxisPadding") = "12"
                                dritem("YAxisMajorInterval") = "10"
                                dritem("YAxisIntervalMinimumHeight") = "40"
                                dritem("Height") = "100"
                                dritem("ShowNormalRange") = "True"
                                dritem("NormalRangeDescription") = "Adult normal range"
                                dritem("NormalRangeMinimumValue") = "10"
                                dritem("NormalRangeMaximumValue") = "20"
                                dritem("NormalRangeBrush") = "#FEFCE9"
                                dritem("HoverBackground") = "#B966B9"
                                dritem("InterpolationLineColor") = "#B966B9"
                                dritem("DataMarkerTemplate") = "{StaticResource Triangle}"
                                dritem("Style") = "{StaticResource OverriddenDefaultStyle}"
                                dritem("Row_Id") = cntid.ToString()


                                ds.Tables("Item").Rows.Add(dritem)


                                Dim drevts As DataRow = ds.Tables("Events").NewRow()
                                drevts("Events_Id") = evtid.ToString()
                                drevts("Item_Id") = itemcnt.ToString()
                                ds.Tables("Events").Rows.Add(drevts)


                                respid = evtid
                                evtid = evtid + 1
                                cntid = cntid + 1
                                itemcnt = itemcnt + 1

                                flagresp = 1

                            End If


                            Dim drevt As DataRow = ds.Tables("Event").NewRow()
                            drevt("Type") = "GraphData"

                            drevt("ActualStartDate") = drc("dtvitaldate").ToString
                            drevt("Y1") = drc("drespiratoryRate").ToString()
                            drevt("Events_Id") = respid.ToString()


                            ds.Tables("Event").Rows.Add(drevt)
                        End If
                    End If
                Next
            End If
        Catch ex As Exception

        End Try
        fillbloodpressgrph(cntid, itemcnt, evtid, ds)
    End Sub



    Public Sub fillLabgraph(ByVal cntid As Integer, ByVal itemcnt As Integer, ByVal evtid As Integer, ByVal ds As DataSet)

        Try
            Dim dt As DataTable = Nothing
            dt = GetDSFromDB.Tables(5)  'Getdata("gsp_getTimelinelabdata") ''GetResultsDetailsByResult()
            If dt.Rows.Count > 0 Then
                Dim dv As DataView
                dv = dt.DefaultView
                dv = DirectCast(dv.ToTable(True, "labotrd_ResultName"), DataTable).DefaultView
                For Each dr As DataRow In dv.ToTable().Rows

                    Dim fl As Boolean = True
                    Dim arr(0) As Int32
                    Array.Resize(arr, 0)
                    Dim cnt As Integer = 0
                    Dim drc() As DataRow = dt.Select("labotrd_ResultName='" + dr("labotrd_ResultName").ToString().Trim() + "'")

                    If drc.Length >= 1 Then
                        Dim drrow As DataRow = ds.Tables("Row").NewRow
                        drrow("Row_Id") = cntid.ToString()
                        drrow("Id") = cntid.ToString()
                        drrow("Name") = dr("labotrd_ResultName").ToString()
                        drrow("Background") = "#E7D3E7"
                        drrow("Section_Id") = TimelineGraphName.Lab

                        ds.Tables("Row").Rows.Add(drrow)
                        Dim dritem As DataRow = ds.Tables("Item").NewRow
                        dritem("Item_Id") = itemcnt.ToString()

                        dritem("NormalRangeBrush") = "#FEFCE9"
                        dritem("HoverBackground") = "#B84E4E"
                        dritem("InterpolationLineColor") = "#990000"
                        dritem("DataMarkerTemplate") = "{StaticResource Square}"
                        dritem("Style") = "{StaticResource OverriddenDefaultStyle}"
                        dritem("Row_Id") = cntid.ToString()
                        dritem("YAxisMajorInterval") = "10"
                        dritem("YAxisIntervalMinimumHeight") = "20"
                        dritem("Height") = "150"
                        ds.Tables("Item").Rows.Add(dritem)

                        Dim drevts As DataRow = ds.Tables("Events").NewRow()
                        drevts("Events_Id") = evtid.ToString()
                        drevts("Item_Id") = itemcnt.ToString()
                        ds.Tables("Events").Rows.Add(drevts)

                        evtid = evtid + 1
                        cntid = cntid + 1
                        itemcnt = itemcnt + 1

                        For row_cnt As Int64 = 0 To drc.Length - 1
                            'If drc(row_cnt)("Resdate").ToString <> "" Or drc(row_cnt)("labotrd_ResultRange") <> "" Then
                            '    Dim splstr() As String = drc(row_cnt)("labotrd_ResultRange").ToString().Split("-")
                            '    If splstr.Length = 2 Then

                            '        Array.Resize(arr, arr.Length + 2)
                            '        arr(cnt) = Convert.ToInt32(splstr(0))
                            '        cnt = cnt + 1
                            '        arr(cnt) = Convert.ToInt32(splstr(1))
                            '        cnt = cnt + 1

                            '        Dim drevt As DataRow = ds.Tables("Event").NewRow()
                            '        drevt("Type") = "GraphData"

                            '        drevt("ActualStartDate") = drc(row_cnt)("Resdate").ToString
                            '        drevt("Y1") = drc(row_cnt)("labotrd_ResultValue").ToString()
                            '        drevt("Events_Id") = (evtid - 1).ToString()
                            '        ds.Tables("Event").Rows.Add(drevt)

                            '    End If

                            'End If


                            If drc(row_cnt)("Resdate").ToString <> "" Then


                                '   arr(cnt) = Convert.ToInt32(splstr(0))
                                ' cnt = cnt + 1
                                ' arr(cnt) = Convert.ToInt32(splstr(1))
                                ' cnt = cnt + 1

                                Dim drevt As DataRow = ds.Tables("Event").NewRow()
                                drevt("Type") = "GraphData"

                                drevt("ActualStartDate") = drc(row_cnt)("Resdate").ToString
                                Try
                                    Array.Resize(arr, arr.Length + 1)
                                    Dim dec As Single = Convert.ToDouble(drc(row_cnt)("labotrd_ResultValue").ToString())

                                    drevt("Y1") = drc(row_cnt)("labotrd_ResultValue").ToString()
                                    drevt("Events_Id") = (evtid - 1).ToString()
                                    ds.Tables("Event").Rows.Add(drevt)
                                    arr(cnt) = drc(row_cnt)("labotrd_ResultValue").ToString()
                                    cnt += 1
                                Catch ex As Exception

                                End Try


                            End If



                        Next

                        If arr.Length > 0 Then
                            Array.Sort(arr)

                            ds.Tables("Item").Rows(ds.Tables("Item").Rows.Count - 1)("YAxisMinValue") = arr(0)
                            ds.Tables("Item").Rows(ds.Tables("Item").Rows.Count - 1)("YAxisMaxValue") = (Convert.ToDouble(arr(arr.Length - 1).ToString()) + 10).ToString()
                            Dim doub As Integer = 10
                            Try
                                doub = ((ds.Tables("Item").Rows(ds.Tables("Item").Rows.Count - 1)("YAxisMaxValue")) - (ds.Tables("Item").Rows(ds.Tables("Item").Rows.Count - 1)("YAxisMinValue"))) / 10
                                If doub <= 10 Then
                                    doub = 10
                                Else
                                    doub += 50
                                End If
                            Catch ex As Exception
                                doub = 10
                            End Try
                            ds.Tables("Item").Rows(ds.Tables("Item").Rows.Count - 1)("YAxisMajorInterval") = doub.ToString()
                            Array.Clear(arr, 0, arr.Length - 1)
                        End If
                    End If
                Next
                dv.Dispose()
                dv = Nothing
            End If
            'dt.Dispose()
            'dt = Nothing
        Catch ex As Exception

        End Try
        FillOrdersGraph(cntid, itemcnt, evtid, ds)
        '   ds.WriteXml(Application.StartupPath & "\\XMLFile" & "\\TimelineData.XML")"")
    End Sub
    Dim endtime As DateTime = Nothing

    Public Sub FillOrdersGraph(ByVal cntid As Integer, ByVal itemcnt As Integer, ByVal evtid As Integer, ByVal ds As DataSet)

        Try
            Dim orderdt As DataTable = GetDSFromDB.Tables(6) 'Getdata("gsp_getTimelineOrderData") ''getordersdata()
            For Each drc As DataRow In orderdt.Rows
                If drc("StartDate").ToString().Trim() <> "" And drc("StartDate").ToString().Trim() <> "01 Jan 1900" Then

                    Dim dr As DataRow = ds.Tables("Row").NewRow
                    dr("Row_Id") = cntid.ToString()
                    dr("Id") = cntid.ToString()
                    dr("Name") = drc("TestName")
                    dr("Background") = "#F2F2F2"
                    dr("Section_Id") = TimelineGraphName.Order

                    ds.Tables("Row").Rows.Add(dr)

                    Dim dritem As DataRow = ds.Tables("Item").NewRow
                    dritem("Item_Id") = itemcnt.ToString()
                    dritem("Name") = drc("TestName").ToString()
                    'If drc("StartDate").ToString().Trim() <> "" And drc("StartDate").ToString().Trim() <> "01 Jan 1900" Then
                    dritem("StartDate") = drc("StartDate").ToString()
                    dritem("EndDate") = drc("StartDate").ToString()
                    'End If

                    dritem("Row_Id") = cntid.ToString()

                    ds.Tables("Item").Rows.Add(dritem)
                    cntid = cntid + 1
                    itemcnt = itemcnt + 1
                End If
            Next
        Catch ex As Exception

        End Try
        Try

            If (IsNothing(DsSendTimeline) = False) Then
                DsSendTimeline.Dispose()
                DsSendTimeline = Nothing
            End If
            DsSendTimeline = ds

            ' objtimeline.strFileName = TimelineNewFileName()
            TimeActivityGraphHostPage.strFileName = ""
            'If objtimeline.strFileName.Trim() <> "" Then
            '  objtimeline.strFileName = "10 25 2010 - 09 01 48 PM 46.XML"
            ''    ds.WriteXml(objtimeline.strFileName)


            ' End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try

    End Sub

    'Private Function TimelineNewFileName() As String

    '    Dim _Path As String = gstrgloEMRStartupPath & "\Temp"
    '    Dim _NewDocumentName As String = ""
    '    Dim _Extension As String = ".XML"
    '    Dim _dtCurrentDateTime As DateTime = System.DateTime.Now

    '    Dim i As Integer = 0
    '    _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & " " & _dtCurrentDateTime.Millisecond & _Extension
    '    While File.Exists(_Path & "\" & _NewDocumentName) = True
    '        i = i + 1
    '        _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & " " & _dtCurrentDateTime.Millisecond & "-" & i & _Extension
    '    End While
    '    Return _Path & "\" & _NewDocumentName

    'End Function

    Public Sub fillbloodpressgrph(ByVal cntid As Integer, ByVal itemcnt As Integer, ByVal evtid As Integer, ByVal ds As DataSet)

        '  Dim oclsViewGraphs As New clsViewGraphs
        Dim BPdt = GetDSFromDB.Tables(4)  'oclsViewGraphs.ScanAgeHtWt(_PatientId)
        Dim flagbp As Integer = 0
        Dim flagbpstan As Integer = 0
        Dim bpid As Integer = 1
        Dim bpidstan As Integer = 1
        Try
            For Each drc As DataRow In BPdt.Rows
                If drc("dtvitaldate").ToString().Trim() <> "" And drc("dtvitaldate").ToString().Trim() <> "01 Jan 1900" Then

                    If (drc("dBloodPressureSittingMin").ToString().Trim <> "" And drc("dBloodPressureSittingMax").ToString().Trim() <> "") Or (drc("dBloodPressureStandingMin").ToString().Trim <> "" And drc("dBloodPressureStandingMin").ToString().Trim() <> "") Then

                        If flagbp = 0 And (drc("dBloodPressureSittingMin").ToString().Trim() <> "" And drc("dBloodPressureSittingMax").ToString().Trim() <> "") Then
                            Dim dr As DataRow = ds.Tables("Row").NewRow
                            dr("Row_Id") = cntid.ToString()
                            dr("Id") = cntid.ToString()
                            dr("Name") = "Blood Pressure Sitting"
                            dr("Background") = "#D6EBD6"
                            dr("Section_Id") = TimelineGraphName.Vitals

                            ds.Tables("Row").Rows.Add(dr)

                            Dim dritem As DataRow = ds.Tables("Item").NewRow
                            dritem("Item_Id") = itemcnt.ToString()
                            dritem("Units") = "mmhg"
                            dritem("UnitsDescription") = "mmHg: Millimetres of mercury"
                            dritem("YAxisMaxValue") = "300"
                            dritem("YAxisMinValue") = "0"
                            dritem("YAxisPadding") = "6"
                            dritem("YAxisMajorInterval") = "10"
                            dritem("YAxisIntervalMinimumHeight") = "17"
                            dritem("Height") = "150"
                            dritem("ShowNormalRange") = "False"
                            dritem("HoverBackground") = "#5BA25B"
                            dritem("DataMarkerTemplate") = "{StaticResource IBarMarker}"
                            dritem("PointTemplate") = "{StaticResource IBarPoint}"
                            dritem("LabelTemplate") = "{StaticResource IBarLabel}"
                            dritem("Style") = "{StaticResource OverriddenDefaultStyle}"
                            dritem("Row_Id") = cntid.ToString()

                            ds.Tables("Item").Rows.Add(dritem)


                            Dim drevts As DataRow = ds.Tables("Events").NewRow()
                            drevts("Events_Id") = evtid.ToString()
                            drevts("Item_Id") = itemcnt.ToString()
                            ds.Tables("Events").Rows.Add(drevts)


                            bpid = evtid
                            evtid = evtid + 1
                            cntid = cntid + 1
                            itemcnt = itemcnt + 1
                            flagbp = 1

                        End If

                        If flagbpstan = 0 And (drc("dBloodPressureStandingMin").ToString().Trim <> "" And drc("dBloodPressureStandingMax").ToString().Trim() <> "") Then
                            Dim dr As DataRow = ds.Tables("Row").NewRow
                            dr("Row_Id") = cntid.ToString()
                            dr("Id") = cntid.ToString()
                            dr("Name") = "Blood Pressure Standing"
                            dr("Background") = "#D6EBD6"
                            dr("Section_Id") = TimelineGraphName.Vitals

                            ds.Tables("Row").Rows.Add(dr)

                            Dim dritem As DataRow = ds.Tables("Item").NewRow
                            dritem("Item_Id") = itemcnt.ToString()
                            dritem("Units") = "mmhg"
                            dritem("UnitsDescription") = "mmHg: Millimetres of mercury"
                            dritem("YAxisMaxValue") = "300"
                            dritem("YAxisMinValue") = "0"
                            dritem("YAxisPadding") = "6"
                            dritem("YAxisMajorInterval") = "10"
                            dritem("YAxisIntervalMinimumHeight") = "17"
                            dritem("Height") = "150"
                            dritem("ShowNormalRange") = "False"
                            dritem("HoverBackground") = "#5BA25B"
                            dritem("DataMarkerTemplate") = "{StaticResource IBarMarker}"
                            dritem("PointTemplate") = "{StaticResource IBarPoint}"
                            dritem("LabelTemplate") = "{StaticResource IBarLabel}"
                            dritem("Style") = "{StaticResource OverriddenDefaultStyle}"
                            dritem("Row_Id") = cntid.ToString()

                            ds.Tables("Item").Rows.Add(dritem)


                            Dim drevts As DataRow = ds.Tables("Events").NewRow()
                            drevts("Events_Id") = evtid.ToString()
                            drevts("Item_Id") = itemcnt.ToString()

                            ds.Tables("Events").Rows.Add(drevts)


                            bpidstan = evtid
                            evtid = evtid + 1
                            cntid = cntid + 1
                            itemcnt = itemcnt + 1
                            flagbpstan = 1

                        End If



                        If (drc("dBloodPressureSittingMin").ToString().Trim <> "" And drc("dBloodPressureSittingMax").ToString().Trim() <> "") Then

                            Dim drevt As DataRow = ds.Tables("Event").NewRow()
                            drevt("Type") = "GraphData"

                            drevt("ActualStartDate") = drc("dtvitaldate").ToString
                            'drevt("Y1") = drc("dBloodPressureSittingMin").ToString()
                            'drevt("Y2") = drc("dBloodPressureSittingMax").ToString()
                            drevt("Y2") = drc("dBloodPressureSittingMin").ToString()
                            drevt("Y1") = drc("dBloodPressureSittingMax").ToString()
                            drevt("Events_Id") = bpid.ToString()


                            ds.Tables("Event").Rows.Add(drevt)
                        End If
                        If (drc("dBloodPressureStandingMin").ToString().Trim <> "" And drc("dBloodPressureStandingMax").ToString().Trim() <> "") Then

                            Dim drevtst As DataRow = ds.Tables("Event").NewRow()
                            drevtst("Type") = "GraphData"

                            drevtst("ActualStartDate") = drc("dtvitaldate").ToString
                            'drevtst("Y1") = drc("dBloodPressureStandingMin").ToString()
                            'drevtst("Y2") = drc("dBloodPressureStandingMax").ToString()
                            drevtst("Y2") = drc("dBloodPressureStandingMin").ToString()
                            drevtst("Y1") = drc("dBloodPressureStandingMax").ToString()

                            drevtst("Events_Id") = bpidstan.ToString()
                            ds.Tables("Event").Rows.Add(drevtst)
                        End If
                    End If
                End If

            Next

        Catch ex As Exception

        End Try
        fillLabgraph(cntid, itemcnt, evtid, ds)

    End Sub



    'Public Function getordersdata() As DataTable  '' chetan commented on 13 oct 2010 stored procedure is written for that 
    '    Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
    '      Dim _strSql As String = ""
    '    With odb

    '        _strSql = " select isnull(labotd_DateTime,'') as StartDate ,isnull(labotd_TestName,'') as TestName from lab_order_Testdtl where " _
    '        & " labotd_orderID in (select labom_orderID  from lab_order_mst where labom_patientID= " + Convert.ToString(_PatientID) + ") order by  StartDate"

    '        dt = .GetDataTable_Query(_strSql)
    '    End With
    '    Return dt
    'End Function


    'Public Function getmedicationdata() As DataTable    chetan commented on 13 oct 2010 stored procedure is written for that 
    '    Dim odb As New gloEMRGeneralLibrary.gloEMRDatabase.DataBaseLayer
    '    Dim _strSql As String = ""
    '    With odb

    '        _strSql = "select  row_number() over( order by nPrescriptionid)as RowId," _
    '        & " isnull(cast(nPrescriptionid as varchar),'') as MedicationId," _
    '        & "isnull(sMedication,'')+'-'+isnull(sDrugForm,'')+'-'+isnull(sroute,'')+'-'+isnull(sFrequency,'')+'-'+isnull(sduration,'') as DrugsData," _
    '             & " '322277008' as VMPID,'' as VTMID,'' as AMPID,isnull(sFrequency,'') as Frequency, isnull(sAmount,'0') as FrequencyAmount," _
    '             & " '385024007' as Form, 'suspension' as FormName,isnull(sDosage,'') as Strength,  isnull(sDosage,'') as DOSEQTY," _
    '        & " isnull(sDuration,'') as DoseDuration,  isnull(sAmount,'0') as Rate, isnull(sDuration,'') as CourseDuration,'26643006' as Route, " _
    '             & " 'oral' as RouteName, '' as  Site,'' as Method,'' as Prescriber, isnull(Convert(varchar, dtStartDate, 106),'') as StartDate, " _
    '        & " isnull(dtStartDate,'') as ReviewDate,  isnull(Convert(varchar, DateAdd(Day, 1, dtEndDate), 106),'') as StopDate,isnull(sstatus,'') as MedStatus,'Started' as MedicationStatus, " _
    '        & " isnull(dtStartDate,'') as StatusDate,'regular' as MedicationType,isnull(sReason,'') as Reason,isnull(sMedication,'') as MedName , " _
    '        & " ''  as Instructions,nPrescriptionid as PrescriptionId,'0' as Patient_Id from Medication where Medication.npatientiD=" + Convert.ToString(_PatientID) + ""

    '        dt = .GetDataTable_Query(_strSql)
    '    End With
    '    Return dt

    'End Function


    Private Sub frm_Graph_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Try


        '    ' File.Delete(objtimeline.strFileName)
        'Catch ex As Exception

        'End Try
    End Sub
    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _PatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property
End Class