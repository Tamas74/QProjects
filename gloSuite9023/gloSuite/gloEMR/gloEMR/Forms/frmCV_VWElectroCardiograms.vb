Imports System.IO
Imports Microsoft.Win32
Imports C1.Win.C1FlexGrid
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.Data.Common
Imports gloEmdeonCommon
Imports System.Runtime.InteropServices
Imports gloEmdeonInterface.Classes
Imports gloEmdeonInterface.Forms


Public Class frmCV_VWElectroCardiograms
    Implements IPatientContext

    Dim mPatientID As Long = 0
    Dim mECGID As Long = 0
    Dim mVisitID As Long = 0
    Dim mExamID As Long = 0
    Dim mClinicID As Long = 0
    Dim mDateofProc As Date
    'Dim mProcedures As String = ""


    Private COL_CATHETERIZATIONID As Integer = 0
    Private COL_PATIENTID As Integer = 1
    Private COL_EXAMID As Integer = 2
    Private COL_VISITID As Integer = 3
    Private COL_CLINICID As Integer = 4
    Private COL_CPTCODE As Integer = 5
    Private COL_TESTTYPE As Integer = 6

    Private COL_INTERVENTIONTYPE As Integer = 7
    Private COL_PHYSICIANNAME As Integer = 8
    'Private COL_CPTCODETESTTYPE As Integer = 6
    Private COL_PRESSURE As Integer = 9
    Private COL_SATURATION As Integer = 10
    Private COL_LV As Integer = 11
    Private COL_RV As Integer = 12
    Private COL_NARRATIVESUMMARY As Integer = 13
    Private COL_PROCEDUREDATE As Integer = 14
    Dim COL_PARENT As Integer = 15
    Dim COL_IDENTITY As Integer = 16
    Dim COL_DateofStudyInvisible As Integer = 17

    Dim COL_OrderID As Integer = 18
    Dim COL_TestID As Integer = 19
    Dim COL_deviceType As Integer = 20

    Dim COL_DMSDocumentID As Integer = 21

    Private COL_COUNT As Integer = 22
    Dim gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip = Nothing

    Private IsHeartCentrixECGDeviceConfigured As Boolean = False
    Private IsWelchAllynECGDeviceConfigured As Boolean = False
    Private IsMidmarkIQECGDeviceConfigured As Boolean = False


    Public Sub New(ByVal nPatientId As Long)
        '
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mPatientID = nPatientId
        mClinicID = 1

        Device_Availabel()

    End Sub

    Private Sub frmCV_VWElectroCardiograms_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

   
    Private Sub frmCV_VWElectroCardiograms_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'start of code commented by manoj jadhav on 20111102 for make button visible true/false as per settings 
        'LoadPatientStrip()
        ''If ECg settings is activated then show button either do not show.
        'gloEmdeonCommon.mdlEcgProcessLayer.sConnectionString = GetConnectionString()
        'If gloEmdeonCommon.mdlEcgProcessLayer.ValidateUserSettings(gnLoginID) Then
        '    tlbHealthCentrix.Visible = True
        '    tlbGetPendingStatus.Visible = True
        'End If

        'SetGridSytle()
        'FillECG()

        ' ''swaraj - 30-04-2010 - To disable new button if patient exists
        ''mVisitID = GetVisitID(Now.Date, mPatientID)
        ''If mVisitID > 0 Then
        ''    ts_btnAdd.Enabled = False
        ''End If
        'Try
        '    gloPatient.gloPatient.GetWindowTitle(Me, mPatientID, GetConnectionString(), gstrMessageBoxCaption)
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        'End Try
        'end of code commented by manoj jadhav on 20111102 for make button visible true/false as per settings 
        'start of code added by manoj jadhav on 20111102 for make button visible true/false as per settings 
        Try
            LoadPatientStrip()
            gloEmdeonCommon.mdlEcgProcessLayer.sConnectionString = GetConnectionString()

            Dim oSettings As gloSettings.GeneralSettings = New gloSettings.GeneralSettings(gloEmdeonCommon.mdlEcgProcessLayer.sConnectionString)
            Dim oResult As Object = Nothing
            oSettings.GetSetting("EnableLocalWelchAllynECGDevice", mdlGeneral.gnLoginID, mdlGeneral.gClinicID, oResult)

            If Not String.IsNullOrEmpty(Convert.ToString(oResult)) Then
                If String.Compare(Convert.ToString(oResult), "True", True) = 0 Then
                    bEnableLocalWelchAllynECGDevice = True
                End If
            End If

            oResult = Nothing
            oSettings.Dispose()
            oSettings = Nothing

            'Device_Availabel()
            SetGridSytle()
            FillECG()
            gloPatient.gloPatient.GetWindowTitle(Me, mPatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        End Try
        'end of code commented by manoj jadhav on 20111102 for make button visible true/false as per settings 
    End Sub
    ''swaraj - 04-28-2010 - To set C1flexgrid style''
    Private Sub SetGridSytle()
        'Dim struser As String
        With C1CV_Catheterization
            'Dim i As Int16
            .Dock = DockStyle.Fill
            .Cols.Count = COL_COUNT  'column count
            .Rows.Fixed = 1
            .Rows.Count = 1
            .AllowEditing = False
            .AllowAddNew = False

            .Styles.ClearUnused()

            .Cols(COL_CATHETERIZATIONID).Width = .Width * 0
            .Cols(COL_CATHETERIZATIONID).AllowEditing = False
            .SetData(0, COL_CATHETERIZATIONID, "ECG ID")
            .Cols(COL_CATHETERIZATIONID).TextAlignFixed = TextAlignEnum.LeftCenter


            .Cols(COL_PATIENTID).Width = .Width * 0
            .Cols(COL_PATIENTID).AllowEditing = False
            .SetData(0, COL_PATIENTID, "Patient ID")
            .Cols(COL_PATIENTID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_EXAMID).Width = .Width * 0
            .Cols(COL_EXAMID).AllowEditing = False
            .SetData(0, COL_EXAMID, "Exam ID")
            .Cols(COL_EXAMID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_VISITID).Width = .Width * 0
            .Cols(COL_VISITID).AllowEditing = False
            .SetData(0, COL_VISITID, "Visit ID")
            .Cols(COL_VISITID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_CLINICID).Width = .Width * 0
            .Cols(COL_CLINICID).AllowEditing = False
            .SetData(0, COL_CLINICID, "Clinic ID")
            .Cols(COL_CLINICID).TextAlignFixed = TextAlignEnum.LeftCenter


            .Cols(COL_CPTCODE).Width = .Width * 0
            .Cols(COL_CPTCODE).AllowEditing = False
            .SetData(0, COL_CPTCODE, "CPT Code")
            .Cols(COL_CPTCODE).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_TESTTYPE).Width = .Width * 0
            .Cols(COL_TESTTYPE).AllowEditing = False
            .SetData(0, COL_TESTTYPE, "CPT Test Type")
            .Cols(COL_TESTTYPE).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_INTERVENTIONTYPE).Width = .Width * 0
            .Cols(COL_INTERVENTIONTYPE).AllowEditing = False
            .SetData(0, COL_INTERVENTIONTYPE, "Review In Physician")
            .Cols(COL_INTERVENTIONTYPE).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_PHYSICIANNAME).Width = .Width * 0
            .Cols(COL_PHYSICIANNAME).AllowEditing = False
            .SetData(0, COL_PHYSICIANNAME, "Order In Physician")
            .Cols(COL_PHYSICIANNAME).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_PRESSURE).Width = .Width * 0
            .Cols(COL_PRESSURE).AllowEditing = False
            .SetData(0, COL_PRESSURE, "Pressure")
            .Cols(COL_PRESSURE).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_SATURATION).Width = .Width * 0
            .Cols(COL_SATURATION).AllowEditing = False
            .SetData(0, COL_SATURATION, "Saturation")
            .Cols(COL_SATURATION).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_LV).Width = .Width * 0
            .Cols(COL_LV).AllowEditing = False
            .SetData(0, COL_LV, "LV")
            .Cols(COL_LV).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_RV).Width = .Width * 0
            .Cols(COL_RV).AllowEditing = False
            .SetData(0, COL_RV, "RV")
            .Cols(COL_RV).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_NARRATIVESUMMARY).Width = .Width * 0
            .Cols(COL_NARRATIVESUMMARY).AllowEditing = False
            .SetData(0, COL_NARRATIVESUMMARY, "ECG InterPretation")
            .Cols(COL_NARRATIVESUMMARY).TextAlignFixed = TextAlignEnum.LeftCenter


            .Cols(COL_PROCEDUREDATE).Width = .Width * 1.3
            ''.Cols(COL_PROCEDUREDATE).AllowEditing = False
            .SetData(0, COL_PROCEDUREDATE, "Given Date")
            .Cols(COL_PROCEDUREDATE).DataType = GetType(String)
            .Cols(COL_PROCEDUREDATE).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_PARENT).Width = .Width * 0
            .Cols(COL_PARENT).AllowEditing = False
            .SetData(0, COL_PARENT, "Parent")
            .Cols(COL_PARENT).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_IDENTITY).Width = .Width * 0
            .Cols(COL_IDENTITY).AllowEditing = False
            .SetData(0, COL_IDENTITY, "Identity")
            .Cols(COL_IDENTITY).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_DateofStudyInvisible).Width = 0
            .SetData(0, COL_DateofStudyInvisible, "Date of Study Invisible")
            .Cols(COL_DateofStudyInvisible).DataType = GetType(String)
            .Cols(COL_DateofStudyInvisible).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_TestID).Visible = False
            .Cols(COL_OrderID).Visible = False

            'start of code added by manoj jadhav on 20111102 for add device Type Colum to grid 
            .Cols(COL_deviceType).Width = 0
            .SetData(0, COL_deviceType, "DeviceType")
            .Cols(COL_deviceType).DataType = GetType(String)
            .Cols(COL_deviceType).TextAlignFixed = TextAlignEnum.LeftCenter
            .Cols(COL_deviceType).Visible = False
            'end of code added by manoj jadhav on 20111102 for add device Type Colum to grid 

            .Cols(COL_DMSDocumentID).Visible = False
        End With
        ''swaraj - 04-28-2010 - To set C1flexgrid style''
    End Sub

    Private Sub FillECG()

        'start of code added by manoj jadhav on 20111102 for clear grid 
        C1CV_Catheterization.Rows.RemoveRange(1, C1CV_Catheterization.Rows.Count - 1)
        'end of code added by manoj jadhav on 20111102 for clear grid 

        Dim _Row As Integer

        ''set properties of treeview in flexgrid
        With C1CV_Catheterization
            .Tree.Column = COL_PROCEDUREDATE
            .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
            .Tree.LineStyle = Drawing2D.DashStyle.Solid
            .Tree.Indent = 15
        End With

        Dim dtProcDate As DataTable = Nothing
        Dim dtCPT As DataTable = Nothing
        Dim dtTypeIntervention As DataTable = Nothing
        Dim dtAddPhysician As DataTable = Nothing
        Dim dtPressure As DataTable = Nothing
        'Dim dtSaturation As DataTable
        'Dim dtLV As DataTable
        ''Dim dtRV As DataTable
        Dim dtNarrativeSummary As DataTable = Nothing
        Dim dtTestType As DataTable = Nothing
        Dim dtReviewDate As DataTable = Nothing
        Dim dtECGType As DataTable = Nothing

        Dim mDOS As Int16
        Dim mCPT As Int16
        Dim mTestType As Int16
        Dim mTypeintervention As Int16
        Dim mPhysician As Int16
        Dim mPressure As Int16
        'Dim mSaturation As Int16
        'Dim mLV As Int16
        ''Dim mRV As Int16
        Dim mNarrativesummary As Int16
        Dim mReviewDate As Int16
        Dim mECGType As Int16

        Dim strdtQry As String
        Dim strCPTcodeQry As String
        Dim strTestTypeQry As String
        Dim strTypeInterventionQry As String
        Dim strPhysicianQry As String
        Dim strPressureQry As String
        'Dim strSaturationQry As String
        'Dim strLVQry As String
        ''Dim strRVQry As String
        Dim strNarrativesummaryQry As String
        Dim strReviewDateQry As String
        Dim strECGType As String

        Dim strconcatCPT1 As String = ""
        Dim nextRow As Integer
        Dim strcombine As String

        Dim oDB As gloStream.gloDataBase.gloDataBase = Nothing

        Dim ECGID As Int64 = 0
        Dim PatientID As Int64 = 0
        Dim ExamID As Int64 = 0
        Dim VisitID As Int64 = 0
        Dim ClinicID As Int64 = 0
        Dim DateofProc As Date
        Dim TestID As String = String.Empty
        Dim sOrderID As String = String.Empty
        Dim sDeviceType As String = String.Empty
        Dim sInterpretation As String = String.Empty
        Dim count As Integer = 0
        Try
            ''Select Query for Given date
            ' strdtQry = "SELECT Distinct isnull(nECGID,0) as nECGID,isnull(nPatientID,0) as nPatientID,isnull(nExamID,0) as nExamID,isnull(nVisitID,0) as nVisitID,isnull(nClinicID,0) as nClinicID,dtGivenDate as dtGivenDate,ISNULL(sOrderId,'') as OrderID,ISNULL(sTestId,'') as TestId from CV_ElectroCardioGrams   where nGroupID=0 AND nPatientID='" & mPatientID & "' order by dtGivenDate"
            strdtQry = "SELECT Distinct ISNULL(nECGID,0) as nECGID,ISNULL(nPatientID,0) as nPatientID,ISNULL(nExamID,0) as nExamID,ISNULL(nVisitID,0) as nVisitID,ISNULL(nClinicID,0) as nClinicID,dtGivenDate as dtGivenDate,ISNULL(sOrderId,'') as OrderID,ISNULL(sTestId,'') as TestId, sDeviceType,sECGInterpretation,ISNULL(nDMSDocumentID,0) AS nDMSDocumentID from CV_ElectroCardioGrams   where nGroupID=0 AND nPatientID='" & mPatientID & "' order by dtGivenDate DESC"
            oDB = New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)
            dtProcDate = oDB.ReadQueryDataTable(strdtQry)
            oDB.Disconnect()

            With dtProcDate
                If IsNothing(dtProcDate) = False Then
                    For mDOS = 0 To .Rows.Count - 1
                        'Dim ECGID As Int64 = 0
                        'Dim PatientID As Int64 = 0
                        'Dim ExamID As Int64 = 0
                        'Dim VisitID As Int64 = 0
                        'Dim ClinicID As Int64 = 0
                        'Dim DateofProc As Date
                        'Dim TestID As Int32 = 0
                        'Dim sOrderID As String = String.Empty
                        ECGID = 0
                        PatientID = 0
                        ExamID = 0
                        VisitID = 0
                        ClinicID = 0
                        TestID = String.Empty
                        sOrderID = String.Empty
                        sDeviceType = String.Empty
                        sInterpretation = String.Empty


                        count = mDOS + 1
                        If CStr(dtProcDate.Rows(mDOS)("dtGivenDate")).Trim <> "" Then
                            C1CV_Catheterization.Rows.Add()
                            _Row = C1CV_Catheterization.Rows.Count - 1

                            ''ADded by madan on 20110901
                            If Not IsNothing(dtProcDate.Rows(mDOS)("TestId")) AndAlso dtProcDate.Rows(mDOS)("TestId").ToString() <> "" Then
                                TestID = Convert.ToString(dtProcDate.Rows(mDOS)("TestId"))
                            End If

                            If Not IsNothing(dtProcDate.Rows(mDOS)("OrderID")) AndAlso dtProcDate.Rows(mDOS)("OrderID").ToString() <> "" Then
                                sOrderID = dtProcDate.Rows(mDOS)("OrderID")
                            End If
                            ''end madan

                            'start of code added by manoj jadhav on 20111102 for retrieve Device Type
                            If Not IsNothing(dtProcDate.Rows(mDOS)("sDeviceType")) Then
                                sDeviceType = Convert.ToString(dtProcDate.Rows(mDOS)("sDeviceType"))
                            End If

                            If (sDeviceType.Trim().Length <= 0) Then

                                If TestID.Trim().Length <= 0 And sOrderID.Trim().Length <= 0 Then
                                    sDeviceType = "Local"
                                ElseIf TestID.Trim().ToUpper() = sOrderID.Trim().ToUpper() Then
                                    sDeviceType = "WelchAllyn"
                                Else
                                    sDeviceType = "HeartCentrix"  'modified of code added by manoj jadhav on 20111111 for change Cardio to HertCentrix
                                End If

                            End If

                            'start of code added by manoj jadhav on 20111111 for change Cardio to HertCentrix
                            If sDeviceType.Trim().ToUpper() = "Cardio".Trim().ToUpper() Then
                                sDeviceType = "HeartCentrix"
                            End If
                            'end of code added by manoj jadhav on 20111111 for change Cardio to HertCentrix

                            Long.TryParse(Convert.ToString(dtProcDate.Rows(mDOS)("nClinicID")), ClinicID)
                            'start of code added by manoj jadhav on 20111102 for retrieve Device Type

                            ''set the properties for newly added row
                            With C1CV_Catheterization.Rows(_Row)
                                .AllowEditing = False
                                .ImageAndText = True
                                .Height = 24
                                .IsNode = True
                                .Node.Level = 0
                                'start of commented by manoj jadhav
                                'If sOrderID.Length <= 0 Then
                                '    .Node.Data = dtProcDate.Rows(mDOS)("dtGivenDate") & "  : " & "ECG Records"
                                'Else
                                '    .Node.Data = dtProcDate.Rows(mDOS)("dtGivenDate") & "  : " & "Device Records"
                                'End If
                                'end of commented by manoj jadhav
                                .Node.Data = dtProcDate.Rows(mDOS)("dtGivenDate") & "  : " & sDeviceType ' line of code added by manoj jadhav
                                .Node.Image = ImageList1.Images(1)
                            End With

                            nextRow = _Row
                            With C1CV_Catheterization
                                .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nECGID"))
                                .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                .SetData(_Row, COL_EXAMID, dtProcDate.Rows(mDOS)("nExamID"))
                                .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                .SetData(_Row, COL_CLINICID, dtProcDate.Rows(mDOS)("nClinicID"))
                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString)
                                .SetData(_Row, COL_OrderID, dtProcDate.Rows(mDOS)("OrderID"))
                                .SetData(_Row, COL_TestID, dtProcDate.Rows(mDOS)("TestId"))
                                .SetData(_Row, COL_deviceType, sDeviceType)
                                .SetData(_Row, COL_CLINICID, ClinicID)
                                .SetData(_Row, COL_DMSDocumentID, dtProcDate.Rows(mDOS)("nDMSDocumentID"))

                                ECGID = dtProcDate.Rows(mDOS)("nECGID")
                                PatientID = dtProcDate.Rows(mDOS)("nPatientID")
                                ExamID = dtProcDate.Rows(mDOS)("nExamID")
                                VisitID = dtProcDate.Rows(mDOS)("nVisitID")
                                ClinicID = dtProcDate.Rows(mDOS)("nClinicID")
                                DateofProc = Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString()

                            End With


                            Dim dtProcedureDate As Date = dtProcDate.Rows(mDOS)("dtGivenDate")
                            If sOrderID.Length <= 0 Then
                                '' Query for selecting CPT Code ''
                                strCPTcodeQry = "SELECT DISTINCT isnull(sCPTCode,'') as sCPTCode from CV_ElectroCardioGrams where nPatientID=" & mPatientID & " AND dtGivenDate='" & dtProcedureDate & "' AND nGroupID='" & ECGID & "' AND sCPTCode<>''"
                                oDB.Connect(GetConnectionString)
                                dtCPT = oDB.ReadQueryDataTable(strCPTcodeQry)
                                oDB.Disconnect()

                                With dtCPT
                                    If IsNothing(dtCPT) = False Then
                                        If dtCPT.Rows.Count >= 0 Then
                                            C1CV_Catheterization.Rows.Add()
                                            _Row = C1CV_Catheterization.Rows.Count - 1

                                            With C1CV_Catheterization.Rows(_Row)
                                                .AllowEditing = False
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 1
                                                .Node.Data = "CPT"
                                                .Node.Image = ImageList1.Images(17)
                                            End With
                                            With C1CV_Catheterization
                                                .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nECGID"))
                                                .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_OrderID, dtProcDate.Rows(mDOS)("OrderID"))
                                                .SetData(_Row, COL_TestID, dtProcDate.Rows(mDOS)("TestId"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString)
                                                .SetData(_Row, COL_deviceType, sDeviceType)
                                                .SetData(_Row, COL_CLINICID, ClinicID)
                                                .SetData(_Row, COL_DMSDocumentID, dtProcDate.Rows(mDOS)("nDMSDocumentID"))
                                            End With
                                        End If
                                        For mCPT = 0 To dtCPT.Rows.Count - 1
                                            Dim strCurrentCPT As String = dtCPT.Rows(mCPT)("sCPTCode")
                                            If strCurrentCPT.Trim <> "" Then
                                                C1CV_Catheterization.Rows.Add()
                                                _Row = C1CV_Catheterization.Rows.Count - 1


                                                With C1CV_Catheterization.Rows(_Row)
                                                    .AllowEditing = False
                                                    .ImageAndText = True
                                                    .Height = 24
                                                    .IsNode = True
                                                    .Node.Level = 2
                                                    .Node.Data = strCurrentCPT
                                                    .Node.Image = ImageList1.Images(3)
                                                End With

                                                With C1CV_Catheterization
                                                    .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nECGID"))
                                                    .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                    .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                    .SetData(_Row, COL_OrderID, dtProcDate.Rows(mDOS)("OrderID"))
                                                    .SetData(_Row, COL_TestID, dtProcDate.Rows(mDOS)("TestId"))
                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString)
                                                    .SetData(_Row, COL_deviceType, sDeviceType)
                                                    .SetData(_Row, COL_CLINICID, ClinicID)
                                                    .SetData(_Row, COL_DMSDocumentID, dtProcDate.Rows(mDOS)("nDMSDocumentID"))
                                                End With



                                                ''Query for selecting Test Type
                                                strTestTypeQry = "SELECT DISTINCT isnull(sTestType,'') as sTestType from CV_ElectroCardioGrams where nPatientID=" & mPatientID & " AND dtGivenDate='" & dtProcedureDate & "' AND nGroupID=" & ECGID & " AND sCPTCode='" & strCurrentCPT & "'  and sTestType<>''"
                                                oDB.Connect(GetConnectionString)
                                                dtTestType = oDB.ReadQueryDataTable(strTestTypeQry)
                                                oDB.Disconnect()


                                                If dtTestType.Rows.Count > 0 Then
                                                    C1CV_Catheterization.Rows.Add()
                                                    _Row = C1CV_Catheterization.Rows.Count - 1

                                                    With C1CV_Catheterization.Rows(_Row)
                                                        .AllowEditing = False
                                                        .ImageAndText = True
                                                        .Height = 24
                                                        .IsNode = True
                                                        .Node.Level = 3
                                                        .Node.Data = "CPT Coded Test Type"
                                                        .Node.Image = ImageList1.Images(20)
                                                    End With
                                                    With C1CV_Catheterization
                                                        .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nECGID"))
                                                        .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                        .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                        .SetData(_Row, COL_OrderID, dtProcDate.Rows(mDOS)("OrderID"))
                                                        .SetData(_Row, COL_TestID, dtProcDate.Rows(mDOS)("TestId"))
                                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString)
                                                        .SetData(_Row, COL_deviceType, sDeviceType)
                                                        .SetData(_Row, COL_CLINICID, ClinicID)
                                                        .SetData(_Row, COL_DMSDocumentID, dtProcDate.Rows(mDOS)("nDMSDocumentID"))
                                                    End With

                                                    With dtTestType
                                                        If IsNothing(dtTestType) = False Then
                                                            For mTestType = 0 To dtTestType.Rows.Count - 1
                                                                Dim strTestType As String = dtTestType.Rows(mTestType)("sTestType")
                                                                C1CV_Catheterization.Rows.Add()
                                                                _Row = C1CV_Catheterization.Rows.Count - 1

                                                                With C1CV_Catheterization.Rows(_Row)
                                                                    .AllowEditing = True
                                                                    .ImageAndText = True
                                                                    .Height = 24
                                                                    .IsNode = True
                                                                    .Node.Level = 4
                                                                    .Node.Data = strTestType
                                                                    .Node.Image = ImageList1.Images(3)
                                                                End With
                                                                With C1CV_Catheterization
                                                                    .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nECGID"))
                                                                    .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                                    .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                                    .SetData(_Row, COL_OrderID, dtProcDate.Rows(mDOS)("OrderID"))
                                                                    .SetData(_Row, COL_TestID, dtProcDate.Rows(mDOS)("TestId"))
                                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString)
                                                                    .SetData(_Row, COL_deviceType, sDeviceType)
                                                                    .SetData(_Row, COL_CLINICID, ClinicID)
                                                                    .SetData(_Row, COL_DMSDocumentID, dtProcDate.Rows(mDOS)("nDMSDocumentID"))
                                                                End With
                                                            Next
                                                        End If
                                                    End With
                                                End If  ''If dtTestType.Rows.Count > 0
                                            End If
                                        Next   ''For mcpt=0 to .Rows.Count-1
                                    End If   ''with dtCPT
                                End With


                                '' Query for Order In Physician ''
                                strPhysicianQry = "SELECT DISTINCT ISNULL(sOrderInPhysician,'') as sOrderInPhysician from CV_ElectroCardioGrams where nPatientID=" & mPatientID & " AND dtGivenDate='" & dtProcedureDate & "'  AND nECGID=" & ECGID
                                oDB.Connect(GetConnectionString)
                                dtAddPhysician = oDB.ReadQueryDataTable(strPhysicianQry)
                                oDB.Disconnect()

                                With dtAddPhysician
                                    If IsNothing(dtAddPhysician) = False Then
                                        If dtAddPhysician.Rows.Count > 0 Then
                                            C1CV_Catheterization.Rows.Add()
                                            _Row = C1CV_Catheterization.Rows.Count - 1

                                            With C1CV_Catheterization.Rows(_Row)
                                                .AllowEditing = False
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 1
                                                .Node.Data = "Order in Physician"
                                                .Node.Image = ImageList1.Images(18)
                                            End With
                                            With C1CV_Catheterization
                                                .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nECGID"))
                                                .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_OrderID, dtProcDate.Rows(mDOS)("OrderID"))
                                                .SetData(_Row, COL_TestID, dtProcDate.Rows(mDOS)("TestId"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString)
                                                .SetData(_Row, COL_deviceType, sDeviceType)
                                                .SetData(_Row, COL_CLINICID, ClinicID)
                                                .SetData(_Row, COL_DMSDocumentID, dtProcDate.Rows(mDOS)("nDMSDocumentID"))
                                            End With
                                        End If
                                        For mPhysician = 0 To .Rows.Count - 1
                                            Dim strPhysician As String = dtAddPhysician.Rows(mPhysician)("sOrderInPhysician")
                                            If strPhysician.Trim <> "" Then
                                                C1CV_Catheterization.Rows.Add()
                                                _Row = C1CV_Catheterization.Rows.Count - 1

                                                With C1CV_Catheterization.Rows(_Row)
                                                    .AllowEditing = True
                                                    .ImageAndText = True
                                                    .Height = 24
                                                    .IsNode = True
                                                    .Node.Level = 2
                                                    .Node.Data = dtAddPhysician.Rows(mPhysician)("sOrderInPhysician")
                                                    .Node.Image = ImageList1.Images(3)
                                                End With
                                                With C1CV_Catheterization
                                                    .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nECGID"))
                                                    .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                    .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                    .SetData(_Row, COL_OrderID, dtProcDate.Rows(mDOS)("OrderID"))
                                                    .SetData(_Row, COL_TestID, dtProcDate.Rows(mDOS)("TestId"))
                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString)
                                                    .SetData(_Row, COL_deviceType, sDeviceType)
                                                    .SetData(_Row, COL_CLINICID, ClinicID)
                                                    .SetData(_Row, COL_DMSDocumentID, dtProcDate.Rows(mDOS)("nDMSDocumentID"))
                                                End With
                                            End If
                                        Next  'For mPhysician = 0 To .Rows.Count - 1
                                    End If  'dtAddPhysician
                                End With

                                '' Query for selecting  Review in Physician ''
                                strTypeInterventionQry = "SELECT DISTINCT ISNULL(sReviewInPhysician,'') as sReviewInPhysician from CV_ElectroCardioGrams where nPatientID=" & mPatientID & " AND dtGivenDate='" & dtProcedureDate & "' AND nECGID=" & ECGID
                                oDB.Connect(GetConnectionString)
                                dtTypeIntervention = oDB.ReadQueryDataTable(strTypeInterventionQry)
                                oDB.Disconnect()

                                With dtTypeIntervention
                                    If IsNothing(dtTypeIntervention) = False Then
                                        If dtTypeIntervention.Rows.Count > 0 Then
                                            C1CV_Catheterization.Rows.Add()
                                            _Row = C1CV_Catheterization.Rows.Count - 1

                                            With C1CV_Catheterization.Rows(_Row)
                                                .AllowEditing = False
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 1
                                                .Node.Data = "Review in Physician"
                                                .Node.Image = ImageList1.Images(21)
                                            End With
                                            With C1CV_Catheterization
                                                .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nECGID"))
                                                .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_OrderID, dtProcDate.Rows(mDOS)("OrderID"))
                                                .SetData(_Row, COL_TestID, dtProcDate.Rows(mDOS)("TestId"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString)
                                                .SetData(_Row, COL_deviceType, sDeviceType)
                                                .SetData(_Row, COL_CLINICID, ClinicID)
                                                .SetData(_Row, COL_DMSDocumentID, dtProcDate.Rows(mDOS)("nDMSDocumentID"))
                                            End With
                                        End If
                                        For mTypeintervention = 0 To dtTypeIntervention.Rows.Count - 1
                                            Dim strInterventionType As String = dtTypeIntervention.Rows(mTypeintervention)("sReviewInPhysician")
                                            If strInterventionType.Trim <> "" Then
                                                C1CV_Catheterization.Rows.Add()
                                                _Row = C1CV_Catheterization.Rows.Count - 1

                                                With C1CV_Catheterization.Rows(_Row)
                                                    .AllowEditing = True
                                                    .ImageAndText = True
                                                    .Height = 24
                                                    .IsNode = True
                                                    .Node.Level = 2
                                                    .Node.Data = dtTypeIntervention.Rows(mTypeintervention)("sReviewInPhysician")
                                                    .Node.Image = ImageList1.Images(3)
                                                End With
                                                With C1CV_Catheterization
                                                    .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nECGID"))
                                                    .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                    .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                    .SetData(_Row, COL_OrderID, dtProcDate.Rows(mDOS)("OrderID"))
                                                    .SetData(_Row, COL_TestID, dtProcDate.Rows(mDOS)("TestId"))
                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString)
                                                    .SetData(_Row, COL_deviceType, sDeviceType)
                                                    .SetData(_Row, COL_CLINICID, ClinicID)
                                                    .SetData(_Row, COL_DMSDocumentID, dtProcDate.Rows(mDOS)("nDMSDocumentID"))
                                                End With
                                            End If
                                        Next  'For mTypeintervention = 0 To .Rows.Count - 1
                                    End If  'dtTypeIntervention
                                End With

                                '' Query for selecting ECG Type

                                strECGType = "SELECT DISTINCT ISNULL(sECGPerform,'') as sECGPerform,isnull(nPatientID,0) as nPatientID,isnull(nVisitID,0) as nVisitID,dtGivenDate as dtGivenDate from CV_ElectroCardioGrams where nPatientID=" & mPatientID & " AND dtGivenDate='" & dtProcedureDate & "' AND nECGID=" & ECGID
                                oDB.Connect(GetConnectionString)
                                dtECGType = oDB.ReadQueryDataTable(strECGType)
                                oDB.Disconnect()


                                With dtECGType
                                    If IsNothing(dtECGType) = False Then
                                        If dtECGType.Rows.Count > 0 Then
                                            C1CV_Catheterization.Rows.Add()
                                            _Row = C1CV_Catheterization.Rows.Count - 1


                                            With C1CV_Catheterization.Rows(_Row)
                                                .AllowEditing = False
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 1
                                                .Node.Data = "ECG Type"
                                                .Node.Image = ImageList1.Images(22)
                                            End With
                                            With C1CV_Catheterization
                                                .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nECGID"))
                                                .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_OrderID, dtProcDate.Rows(mDOS)("OrderID"))
                                                .SetData(_Row, COL_TestID, dtProcDate.Rows(mDOS)("TestId"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString)
                                                .SetData(_Row, COL_deviceType, sDeviceType)
                                                .SetData(_Row, COL_CLINICID, ClinicID)
                                                .SetData(_Row, COL_DMSDocumentID, dtProcDate.Rows(mDOS)("nDMSDocumentID"))
                                            End With
                                        End If
                                        For mECGType = 0 To dtECGType.Rows.Count - 1
                                            Dim strECGType1 As String = dtECGType.Rows(mECGType)("sECGPerform")
                                            If strECGType1.Trim <> "" Then
                                                C1CV_Catheterization.Rows.Add()
                                                _Row = C1CV_Catheterization.Rows.Count - 1


                                                With C1CV_Catheterization.Rows(_Row)
                                                    .AllowEditing = True
                                                    .ImageAndText = True
                                                    .Height = 24
                                                    .IsNode = True
                                                    .Node.Level = 2
                                                    .Node.Data = dtECGType.Rows(mECGType)("sECGPerform")
                                                    .Node.Image = ImageList1.Images(3)
                                                End With
                                                With C1CV_Catheterization
                                                    .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nECGID"))
                                                    .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                    .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                    .SetData(_Row, COL_OrderID, dtProcDate.Rows(mDOS)("OrderID"))
                                                    .SetData(_Row, COL_TestID, dtProcDate.Rows(mDOS)("TestId"))
                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString)
                                                    .SetData(_Row, COL_deviceType, sDeviceType)
                                                    .SetData(_Row, COL_CLINICID, ClinicID)
                                                    .SetData(_Row, COL_DMSDocumentID, dtProcDate.Rows(mDOS)("nDMSDocumentID"))
                                                End With
                                            End If
                                        Next
                                    End If
                                End With
                                ''

                                '' Query for selecting Review Date ''
                                strReviewDateQry = "SELECT DISTINCT ISNULL(sECGInterpretation,'') as ECGInterpretation,isnull(nPatientID,0) as nPatientID,isnull(nVisitID,0) as nVisitID,dtReviewDate as dtReviewDate from CV_ElectroCardioGrams where nPatientID=" & mPatientID & " AND dtGivenDate='" & dtProcedureDate & "' AND nECGID=" & ECGID
                                oDB.Connect(GetConnectionString)
                                dtReviewDate = oDB.ReadQueryDataTable(strReviewDateQry)
                                oDB.Disconnect()


                                With dtReviewDate
                                    If IsNothing(dtReviewDate) = False Then
                                        If dtReviewDate.Rows.Count > 0 Then
                                            C1CV_Catheterization.Rows.Add()
                                            _Row = C1CV_Catheterization.Rows.Count - 1


                                            With C1CV_Catheterization.Rows(_Row)
                                                .AllowEditing = False
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 1
                                                .Node.Data = "Review Date"
                                                .Node.Image = ImageList1.Images(23)
                                            End With
                                            With C1CV_Catheterization
                                                .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nECGID"))
                                                .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_OrderID, dtProcDate.Rows(mDOS)("OrderID"))
                                                .SetData(_Row, COL_TestID, dtProcDate.Rows(mDOS)("TestId"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString)
                                                .SetData(_Row, COL_deviceType, sDeviceType)
                                                .SetData(_Row, COL_CLINICID, ClinicID)
                                                .SetData(_Row, COL_DMSDocumentID, dtProcDate.Rows(mDOS)("nDMSDocumentID"))
                                            End With
                                        End If
                                        For mReviewDate = 0 To dtReviewDate.Rows.Count - 1
                                            Dim strReviewDate As String = dtReviewDate.Rows(mReviewDate)("dtReviewDate")
                                            If strReviewDate.Trim <> "" Then
                                                C1CV_Catheterization.Rows.Add()
                                                _Row = C1CV_Catheterization.Rows.Count - 1


                                                With C1CV_Catheterization.Rows(_Row)
                                                    .AllowEditing = True
                                                    .ImageAndText = True
                                                    .Height = 24
                                                    .IsNode = True
                                                    .Node.Level = 2
                                                    .Node.Data = Convert.ToDateTime(dtReviewDate.Rows(mReviewDate)("dtReviewDate")).ToShortDateString
                                                    .Node.Image = ImageList1.Images(3)
                                                End With
                                                With C1CV_Catheterization
                                                    .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nECGID"))
                                                    .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                    .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                    .SetData(_Row, COL_OrderID, dtProcDate.Rows(mDOS)("OrderID"))
                                                    .SetData(_Row, COL_TestID, dtProcDate.Rows(mDOS)("TestId"))
                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString)
                                                    .SetData(_Row, COL_deviceType, sDeviceType)
                                                    .SetData(_Row, COL_CLINICID, ClinicID)
                                                    .SetData(_Row, COL_DMSDocumentID, dtProcDate.Rows(mDOS)("nDMSDocumentID"))
                                                End With
                                            End If
                                        Next
                                    End If
                                End With

                            End If
                            '' Query for selecting Interval ''
                            strPressureQry = "SELECT DISTINCT isnull(sPR,'') as sPR,isnull(sQT,'') as sQT,isnull(sQTc,'') as sQTc,isnull(sORSDuration,'') as sORSDuration,isnull(sPAxis,'') as sPAxis,isnull(sQRSAxis,'') as sQRSAxis,isnull(sTAxis,'') as sTAxis,isnull(sECGInterpretation,'') as sECGInterpretation,isnull(sECGPerform,'') as sECGPerform from CV_ElectroCardioGrams where nPatientID=" & mPatientID & " AND dtGivenDate='" & dtProcedureDate.ToString("yyyy-MM-dd HH:mm:ss.FFF") & "' AND nECGID=" & ECGID
                            oDB.Connect(GetConnectionString)
                            dtPressure = oDB.ReadQueryDataTable(strPressureQry)
                            oDB.Disconnect()

                            With dtPressure
                                If IsNothing(dtPressure) = False Then
                                    If dtPressure.Rows.Count > 0 Then
                                        C1CV_Catheterization.Rows.Add()
                                        _Row = C1CV_Catheterization.Rows.Count - 1

                                        With C1CV_Catheterization.Rows(_Row)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 1
                                            .Node.Data = "Interval"
                                            .Node.Image = ImageList1.Images(25)
                                        End With
                                        With C1CV_Catheterization
                                            .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nECGID"))
                                            .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                            .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                            .SetData(_Row, COL_OrderID, dtProcDate.Rows(mDOS)("OrderID"))
                                            .SetData(_Row, COL_TestID, dtProcDate.Rows(mDOS)("TestId"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString)
                                            .SetData(_Row, COL_deviceType, sDeviceType)
                                            .SetData(_Row, COL_CLINICID, ClinicID)
                                            .SetData(_Row, COL_DMSDocumentID, dtProcDate.Rows(mDOS)("nDMSDocumentID"))
                                        End With
                                    End If
                                    For mPressure = 0 To dtPressure.Rows.Count - 1
                                        strcombine = ""
                                        If Not IsNothing(dtPressure.Rows(mPressure)("sPR")) Then
                                            If (dtPressure.Rows(mPressure)("sPR").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "PR" + " " + ":" + " " + dtPressure.Rows(mPressure)("sPR")
                                                End If
                                            End If
                                        End If
                                        If Not IsNothing(dtPressure.Rows(mPressure)("sQT")) Then
                                            If (dtPressure.Rows(mPressure)("sQT").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "QT" + " " + ":" + " " + dtPressure.Rows(mPressure)("sQT")
                                                Else
                                                    strcombine = strcombine + "    " + "QT" + " " + ":" + " " + dtPressure.Rows(mPressure)("sQT")
                                                End If

                                            End If
                                        End If
                                        If Not IsNothing(dtPressure.Rows(mPressure)("sQTc")) Then
                                            If (dtPressure.Rows(mPressure)("sQTc").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "QTc" + " " + ":" + " " + dtPressure.Rows(mPressure)("sQTc")
                                                Else
                                                    strcombine = strcombine + "    " + "QTc" + " " + ":" + " " + dtPressure.Rows(mPressure)("sQTc")
                                                End If

                                            End If
                                        End If
                                        If Not IsNothing(dtPressure.Rows(mPressure)("sORSDuration")) Then
                                            If (dtPressure.Rows(mPressure)("sORSDuration").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "QRS Duration" + " " + ":" + " " + dtPressure.Rows(mPressure)("sORSDuration")
                                                Else
                                                    strcombine = strcombine + "    " + "QRS Duration" + " " + ":" + " " + dtPressure.Rows(mPressure)("sORSDuration")
                                                End If

                                            End If
                                        End If
                                        If Not IsNothing(dtPressure.Rows(mPressure)("sPAxis")) Then
                                            If (dtPressure.Rows(mPressure)("sPAxis").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "P Axis" + " " + ":" + " " + dtPressure.Rows(mPressure)("sPAxis")
                                                Else
                                                    strcombine = strcombine + "    " + "P Axis" + " " + ":" + " " + dtPressure.Rows(mPressure)("sPAxis")
                                                End If
                                            End If
                                        End If
                                        If Not IsNothing(dtPressure.Rows(mPressure)("sQRSAxis")) Then
                                            If (dtPressure.Rows(mPressure)("sQRSAxis").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "QRS Axis" + " " + ":" + " " + dtPressure.Rows(mPressure)("sQRSAxis")
                                                Else
                                                    strcombine = strcombine + "    " + "QRS Axis" + " " + ":" + " " + dtPressure.Rows(mPressure)("sQRSAxis")
                                                End If
                                            End If
                                        End If
                                        If Not IsNothing(dtPressure.Rows(mPressure)("sTAxis")) Then
                                            If (dtPressure.Rows(mPressure)("sTAxis").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "T Axis" + " " + ":" + " " + dtPressure.Rows(mPressure)("sTAxis")
                                                Else
                                                    strcombine = strcombine + "    " + "T Axis" + " " + ":" + " " + dtPressure.Rows(mPressure)("sTAxis")
                                                End If
                                            End If
                                        End If


                                        If strcombine.Trim <> "" Then
                                            C1CV_Catheterization.Rows.Add()
                                            _Row = C1CV_Catheterization.Rows.Count - 1


                                            With C1CV_Catheterization.Rows(_Row)
                                                .AllowEditing = True
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 2
                                                .Node.Data = strcombine
                                                .Node.Image = ImageList1.Images(3)
                                            End With
                                            With C1CV_Catheterization
                                                .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nECGID"))
                                                .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_OrderID, dtProcDate.Rows(mDOS)("OrderID"))
                                                .SetData(_Row, COL_TestID, dtProcDate.Rows(mDOS)("TestId"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString)
                                                .SetData(_Row, COL_deviceType, sDeviceType)
                                                .SetData(_Row, COL_CLINICID, ClinicID)
                                                .SetData(_Row, COL_DMSDocumentID, dtProcDate.Rows(mDOS)("nDMSDocumentID"))
                                            End With
                                        End If
                                    Next     'For mPressure = 0 To .Rows.Count - 1
                                End If  'dtPressure
                            End With

                            '' Query for selecting ECG InterPretaion ''
                            strNarrativesummaryQry = "SELECT DISTINCT ISNULL(sECGInterpretation,'') as ECGInterpretation,isnull(nPatientID,0) as nPatientID,isnull(nVisitID,0) as nVisitID,dtGivenDate as dtGivenDate from CV_ElectroCardioGrams where nPatientID=" & mPatientID & " AND dtGivenDate='" & dtProcedureDate.ToString("yyyy-MM-dd HH:mm:ss.FFF") & "' AND nECGID=" & ECGID
                            oDB.Connect(GetConnectionString)
                            dtNarrativeSummary = oDB.ReadQueryDataTable(strNarrativesummaryQry)
                            oDB.Disconnect()


                            With dtNarrativeSummary
                                If IsNothing(dtNarrativeSummary) = False Then
                                    If dtNarrativeSummary.Rows.Count > 0 Then
                                        C1CV_Catheterization.Rows.Add()
                                        _Row = C1CV_Catheterization.Rows.Count - 1


                                        With C1CV_Catheterization.Rows(_Row)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 1
                                            .Node.Data = "ECG Interpretation"
                                            .Node.Image = ImageList1.Images(24)
                                        End With
                                        With C1CV_Catheterization
                                            .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nECGID"))
                                            .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                            .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                            .SetData(_Row, COL_OrderID, dtProcDate.Rows(mDOS)("OrderID"))
                                            .SetData(_Row, COL_TestID, dtProcDate.Rows(mDOS)("TestId"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString)
                                            .SetData(_Row, COL_deviceType, sDeviceType)
                                            .SetData(_Row, COL_CLINICID, ClinicID)
                                            .SetData(_Row, COL_DMSDocumentID, dtProcDate.Rows(mDOS)("nDMSDocumentID"))
                                        End With
                                    End If
                                    For mNarrativesummary = 0 To dtNarrativeSummary.Rows.Count - 1
                                        Dim strNarrativesummary As String = dtNarrativeSummary.Rows(mNarrativesummary)("ECGInterpretation")
                                        If strNarrativesummary.Trim <> "" Then
                                            C1CV_Catheterization.Rows.Add()
                                            _Row = C1CV_Catheterization.Rows.Count - 1


                                            With C1CV_Catheterization.Rows(_Row)
                                                .AllowEditing = True
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 2
                                                .Node.Data = dtNarrativeSummary.Rows(mNarrativesummary)("ECGInterpretation")
                                                .Node.Image = ImageList1.Images(3)
                                            End With
                                            With C1CV_Catheterization
                                                .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nECGID"))
                                                .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_OrderID, dtProcDate.Rows(mDOS)("OrderID"))
                                                .SetData(_Row, COL_TestID, dtProcDate.Rows(mDOS)("TestId"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString)
                                                .SetData(_Row, COL_deviceType, sDeviceType)
                                                .SetData(_Row, COL_CLINICID, ClinicID)
                                                .SetData(_Row, COL_DMSDocumentID, dtProcDate.Rows(mDOS)("nDMSDocumentID"))
                                            End With
                                        End If
                                    Next
                                End If
                            End With

                            ''Device order status... Added by madan/
                            If sOrderID.Trim().Length > 0 Then
                                C1CV_Catheterization.Rows.Add()
                                _Row = C1CV_Catheterization.Rows.Count - 1
                                With C1CV_Catheterization.Rows(_Row)
                                    .AllowEditing = False
                                    .ImageAndText = True
                                    .Height = 24
                                    .IsNode = True
                                    .Node.Level = 1
                                    .Node.Data = "Device Order Status"
                                    .Node.Image = ImageList1.Images(26)
                                End With
                                With C1CV_Catheterization
                                    .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nECGID"))
                                    .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                    .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                    .SetData(_Row, COL_OrderID, dtProcDate.Rows(mDOS)("OrderID"))
                                    .SetData(_Row, COL_TestID, dtProcDate.Rows(mDOS)("TestId"))
                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString)
                                    .SetData(_Row, COL_deviceType, sDeviceType)
                                    .SetData(_Row, COL_CLINICID, ClinicID)
                                    .SetData(_Row, COL_DMSDocumentID, dtProcDate.Rows(mDOS)("nDMSDocumentID"))
                                End With

                                If TestID.Trim().Length <= 0 Then
                                    C1CV_Catheterization.Rows.Add()
                                    _Row = C1CV_Catheterization.Rows.Count - 1
                                    With C1CV_Catheterization.Rows(_Row)
                                        .AllowEditing = True
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 2
                                        .Node.Data = "Test Pending"
                                        .Node.Image = ImageList1.Images(3)
                                    End With
                                    With C1CV_Catheterization
                                        .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nECGID"))
                                        .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                        .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                        .SetData(_Row, COL_OrderID, dtProcDate.Rows(mDOS)("OrderID"))
                                        .SetData(_Row, COL_TestID, dtProcDate.Rows(mDOS)("TestId"))
                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString)
                                        .SetData(_Row, COL_deviceType, sDeviceType)
                                        .SetData(_Row, COL_CLINICID, ClinicID)
                                        .SetData(_Row, COL_DMSDocumentID, dtProcDate.Rows(mDOS)("nDMSDocumentID"))
                                    End With
                                Else
                                    C1CV_Catheterization.Rows.Add()
                                    _Row = C1CV_Catheterization.Rows.Count - 1
                                    With C1CV_Catheterization.Rows(_Row)
                                        .AllowEditing = True
                                        .ImageAndText = True
                                        .Height = 24
                                        .IsNode = True
                                        .Node.Level = 2
                                        .Node.Data = "Test Completed"
                                        .Node.Image = ImageList1.Images(3)
                                    End With
                                    With C1CV_Catheterization
                                        .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nECGID"))
                                        .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                        .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                        .SetData(_Row, COL_OrderID, dtProcDate.Rows(mDOS)("OrderID"))
                                        .SetData(_Row, COL_TestID, dtProcDate.Rows(mDOS)("TestId"))
                                        .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("dtGivenDate")).ToShortDateString)
                                        .SetData(_Row, COL_deviceType, sDeviceType)
                                        .SetData(_Row, COL_CLINICID, ClinicID)
                                        .SetData(_Row, COL_DMSDocumentID, dtProcDate.Rows(mDOS)("nDMSDocumentID"))
                                    End With
                                End If
                            End If
                        End If
                    Next 'For mDOS = 0 To .Rows.Count - 1
                End If
            End With 'dtProcDate


            C1CV_Catheterization.Tree.Show(0) ' added by manoj jadhav to show all tree in collapsed mode

            expand_Node() 'Add call by manoj jadhav on 21120420 to funcation to show Lattest Test in expamded mode

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, " Error in frmCV_VWElectroCardiograms.FillECG() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not dtProcDate Is Nothing Then
                dtProcDate.Dispose()
                dtProcDate = Nothing
            End If
            If Not dtCPT Is Nothing Then
                dtCPT.Dispose()
                dtCPT = Nothing
            End If
            If Not dtTypeIntervention Is Nothing Then
                dtTypeIntervention.Dispose()
                dtTypeIntervention = Nothing
            End If
            If Not dtAddPhysician Is Nothing Then
                dtAddPhysician.Dispose()
                dtAddPhysician = Nothing
            End If
            If Not dtPressure Is Nothing Then
                dtPressure.Dispose()
                dtPressure = Nothing
            End If
            If Not dtNarrativeSummary Is Nothing Then
                dtNarrativeSummary.Dispose()
                dtNarrativeSummary = Nothing
            End If
            If Not dtTestType Is Nothing Then
                dtTestType.Dispose()
                dtTestType = Nothing
            End If
            If Not dtReviewDate Is Nothing Then
                dtReviewDate.Dispose()
                dtReviewDate = Nothing
            End If
            If Not dtECGType Is Nothing Then
                dtECGType.Dispose()
                dtECGType = Nothing
            End If
            If Not oDB Is Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If



        End Try
        ''swaraj - 28-04-2010 - To fill catheterization information''
    End Sub

    'Added funcation by manoj jadhav on 21120420 to show Lattest Test in expamded mode
    Private Sub expand_Node()
        Dim TempECGID As Long = 0
        Try
            If C1CV_Catheterization.Rows.Count >= 1 Then
                If mECGID = 0 Then

                    If C1CV_Catheterization.Rows(1).IsNode Then
                        C1CV_Catheterization.Rows(1).Node.Expanded = True
                        C1CV_Catheterization.Select(1, COL_CATHETERIZATIONID)
                    End If

                Else

                    For iRow As Integer = 0 To C1CV_Catheterization.Rows.Count - 1

                        TempECGID = 0

                        If Long.TryParse(Convert.ToString(C1CV_Catheterization.GetData(iRow, COL_CATHETERIZATIONID)), TempECGID) Then

                            If TempECGID = mECGID Then

                                If C1CV_Catheterization.Rows(iRow).IsNode Then
                                    C1CV_Catheterization.Rows(iRow).Node.Expanded = True
                                    C1CV_Catheterization.Select(iRow, COL_CATHETERIZATIONID)
                                End If

                                Exit For
                            End If

                        End If

                    Next

                End If
            End If

        Catch ex As Exception
            ex = Nothing
        Finally
            TempECGID = 0
        End Try
    End Sub

    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Try
            Select Case e.ClickedItem.Tag

                'added by madan on 20110501
                Case "AddCardiacEcg"
                    ' NewDeviceOrder() '  'line of code commented by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration  
                    'start of code added by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration 
                    If NewDeviceTest() Then
                        FillECG()
                    End If
                    'end of code added by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration 

                Case "EcgPendingStatus"
                    'start of code commneted by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration 
                    'Dim objEcg As New clsCVElectroCardioGrams()
                    'If objEcg.CheckPendingDeviceOrders(mPatientID) = False Then
                    '    Return
                    'End If
                    'objEcg = Nothing

                    'gloEmdeonCommon.mdlEcgProcessLayer.sConnectionString = GetConnectionString()
                    'If gloEmdeonCommon.mdlEcgProcessLayer.ValidateUserSettings(gnLoginID) = False Then
                    '    MessageBox.Show("Please configure ECG Device interface settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '    Return
                    'End If

                    'Dim objECGForm As New gloEmdeonCommon.frmHealthCentrixLoad(mPatientID, frmHealthCentrixLoad.ProcessTypes.GetOrderStatus, "", mVisitID, GetPrefixTransactionID())
                    'objECGForm.BringToFront()
                    'objECGForm.ShowInTaskbar = False
                    'objECGForm.ShowDialog(Me)
                    'If objECGForm.ErrorString.Length > 0 Then
                    '    MessageBox.Show(objECGForm.ErrorString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    'End If
                    'objECGForm.Dispose()
                    'SetGridSytle()
                    'FillECG()
                    'end of code commented by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration 
                    'start of code added by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration 
                    If (GetCardioScinceECGPendingOrders()) Then
                        FillECG()
                    End If
                    'end of code added by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration 

                    'start of code added by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration 
                Case "Import Test"

                    If Validate_Settings(ClsWelchAllynECGLayer.DeviceType.WelchAllynECGDevice) Then

                        If WelchAllynDeviceTest(FrmWelChallynECG.TestType.GetAlltest, String.Empty, 0) Then

                            FillECG()

                        End If

                    End If
                    'end of code added by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration 

                Case "Add"
                    ''Start :: ElectroCardiogrm
                    Dim _isCurrentDate As Boolean = False
                    If C1CV_Catheterization.Row > 0 Then
                        Dim myDate As Date = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_DateofStudyInvisible)

                        If myDate = Date.Now.Date Then  '' If there is data against the todays date then open for the modify
                            If C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_PATIENTID) > 0 And C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_VISITID) > 0 Then
                                mPatientID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_PATIENTID)
                                mECGID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_CATHETERIZATIONID)
                                mExamID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_EXAMID)
                                mVisitID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_VISITID)
                                mClinicID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_CLINICID)

                                mDateofProc = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_DateofStudyInvisible)
                            End If
                        Else
                            mPatientID = mPatientID
                            mExamID = 0
                            'Check for visit aganist current date,if visit not available pass 0
                            mVisitID = GetVisitID(Now.Date, mPatientID)
                            mClinicID = gnClinicID
                            mDateofProc = Now.Date
                            _isCurrentDate = True
                        End If
                    Else
                        mPatientID = mPatientID
                        mExamID = 0
                        'Check for visit aganist current date,if visit not available pass 0
                        mVisitID = GetVisitID(Now.Date, mPatientID)
                        mClinicID = gnClinicID
                        mDateofProc = Now.Date
                        _isCurrentDate = True
                    End If

                    If C1CV_Catheterization.Row > 0 Then
                        If C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_TestID) <> "" Or C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_OrderID) <> "" Then
                            _isCurrentDate = True
                        End If
                    End If

                    ''Added by madan on 20110131-- to Generate visit id if visit id is not found.
                    If mVisitID = 0 Then
                        mVisitID = GenerateVisitID(mPatientID)
                    End If
                    'End madan-- changes .. Generating visit id,.

                    ''Start :: ElectroCardiogrm
                    Dim ofrm As New frmCV_ElectroCardiograms(mPatientID, mDateofProc, mVisitID)
                    If _isCurrentDate = True Then
                        ofrm.blnIsNew = True
                        _isCurrentDate = False
                    End If
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    '  SetGridSytle()
                    If ofrm.MainCathID Then
                        mECGID = ofrm.MainCathID
                    End If
                    FillECG()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.View, "Record viewed for Cardio Vascular Catheterization", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20100916
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.View, "Record viewed for Cardio Vascular ElectroCardioGram", mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    ''
                    ofrm.Dispose()
                    ofrm = Nothing

                Case "Modify"

                    'start of code commented by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration 
                    'If C1CV_Catheterization.Row > 0 Then
                    '    'modified by madan on 20100108
                    '    Dim _sTestId As String = String.Empty
                    '    Dim _sOrderID As String = String.Empty

                    '    If C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_PATIENTID) > 0 And C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_VISITID) > 0 Then

                    '        If C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_TestID) <> "" Then
                    '            _sTestId = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_TestID)
                    '        End If

                    '        If C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_OrderID) <> "" Then
                    '            _sOrderID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_OrderID)
                    '        End If

                    '        mPatientID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_PATIENTID)
                    '        mECGID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_CATHETERIZATIONID)
                    '        mExamID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_EXAMID)
                    '        mVisitID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_VISITID)
                    '        mClinicID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_CLINICID)

                    '        mDateofProc = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_DateofStudyInvisible)

                    '        If _sTestId.Length > 0 And _sOrderID.Length > 0 Then

                    '            gloEmdeonCommon.mdlEcgProcessLayer.sConnectionString = GetConnectionString()
                    '            If gloEmdeonCommon.mdlEcgProcessLayer.ValidateUserSettings(gnLoginID) = False Then
                    '                MessageBox.Show("Please configure ECG Device interface settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '                Return
                    '            End If

                    '            Dim ofrmDeviceECG As New gloEmdeonCommon.frmECGHealthCentrix(mPatientID, GetConnectionString(), _sOrderID, _sTestId, mVisitID)
                    '            ofrmDeviceECG.ShowInTaskbar = False
                    '            ofrmDeviceECG.BringToFront()
                    '            ofrmDeviceECG.ShowDialog(Me)
                    '            ofrmDeviceECG.Dispose()
                    '            SetGridSytle()
                    '            FillECG()
                    '        ElseIf _sTestId.Length <= 0 And _sOrderID.Length > 0 Then
                    '            MessageBox.Show("The selected ECG order test status is pending. You can click ""Order Status"" to check for updated status.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        Else
                    '            SetGridSytle()
                    '            FillECG()
                    '            Dim ofrm As New frmCV_ElectroCardiograms(mPatientID, mDateofProc, mVisitID)
                    '            ofrm.ShowDialog(Me)
                    '            SetGridSytle()
                    '            FillECG()
                    '            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Modify, "Modified Records in Cardio Vascular Catheterization", gloAuditTrail.ActivityOutCome.Success)
                    '            ''Added Rahul P on 20100916
                    '            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Modify, "Modified Records in Cardio Vascular ElectroCardioGram", mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    '        End If
                    '    End If
                    '    _sTestId = String.Empty
                    '    _sOrderID = String.Empty
                    'End If
                    'end of code commented by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration 
                    'start of code added by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration 
                    If C1CV_Catheterization.RowSel > 0 Then

                        ModifyTest()

                    End If
                    'end of code added by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration 

                Case "Delete"
                    'start of code commented by manoj jadhav on 20111108 to fix bug NO.13905
                    ' ''swaraj - 04-28-2010 - To delete the catheterization record''
                    'If C1CV_Catheterization.Rows.Count > 1 Then


                    '    Dim _sOrderID As String = String.Empty
                    '    If C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_OrderID) <> "" Then
                    '        _sOrderID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_OrderID)
                    '    End If


                    '    If _sOrderID.Length > 0 Then
                    '        MessageBox.Show("Device records cannot be deleted.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        Return
                    '    End If

                    '    mPatientID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_PATIENTID)
                    '    mECGID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_CATHETERIZATIONID)
                    '    mExamID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_EXAMID)
                    '    mVisitID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_VISITID)
                    '    mClinicID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_CLINICID)
                    '    mDateofProc = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_DateofStudyInvisible)

                    '    If MessageBox.Show(" Are you sure you want to delete the ECG Record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    '        Dim objCV_ECG As New clsCVElectroCardioGrams
                    '        objCV_ECG.DeleteElectroCardioGrams(mPatientID, mVisitID, mDateofProc)
                    '        txtsearch.Text = ""
                    '        'SetGridSytle()
                    '        FillECG()
                    '    End If
                    '    _sOrderID = String.Empty
                    'End If
                    ' ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Delete, "CV CVCatheterization deleted.", gloAuditTrail.ActivityOutCome.Success)
                    ' ''Added Rahul P on 20100916
                    'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Delete, "CV CVElectroCardioGram deleted.", mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    ' ''
                    'end of code commented by manoj jadhav on 20111108 to fix bug NO.13905
                    'start of code added by manoj jadhav on 20111108 to fix bug NO.13905
                    If DeleteECGTest() Then
                        txtsearch.Text = ""
                        FillECG()
                    End If
                    'end of code added by manoj jadhav on 20111108 to fix bug NO.13905


                Case "Refresh"
                    'start of code commented by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration 
                    ' ''swaraj - 04-28-2010 - To refresh the catheterization record''
                    'SetGridSytle()
                    'FillECG()
                    ' ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Add, "CVCatheterization data refreshed. ", gloAuditTrail.ActivityOutCome.Success)
                    ' ''Added Rahul P on 20100916
                    'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Add, "CVElectroCardioGram data refreshed. ", mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    ' ''
                    'end of code commented by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration 
                    'start of code added by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration 
                    Device_Availabel()
                    If C1CV_Catheterization.RowSel < 0 Then
                        Exit Try
                    End If

                    If C1CV_Catheterization.GetData(C1CV_Catheterization.RowSel, COL_PATIENTID) > 0 And C1CV_Catheterization.GetData(C1CV_Catheterization.RowSel, COL_VISITID) > 0 Then
                        mECGID = C1CV_Catheterization.GetData(C1CV_Catheterization.RowSel, COL_CATHETERIZATIONID)
                        FillECG()
                    End If
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Add, "CVElectroCardioGram data refreshed. ", mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    'end of code commented by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration 

                Case "Close"
                    ''swaraj - 04-28-2010 - To close the catheterization form''
                    Me.Close()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Add, "CVCatheterization form is closed ", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20100916
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Add, "CVElectroCardioGram data refreshed. ", mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    ''
                Case "ShowDocs"
                    If C1CV_Catheterization.RowSel > 0 Then
                        If (C1CV_Catheterization.GetData(C1CV_Catheterization.RowSel, COL_DMSDocumentID)) > 0 Then
                            Dim objgloEDocV3Management As gloEDocumentV3.gloEDocV3Management = New gloEDocumentV3.gloEDocV3Management()
                            objgloEDocV3Management.ShowEDocument(mPatientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocumentForExternalModule, Me, gloEDocumentV3.Enumeration.enum_OpenExternalSource.DashBoard, C1CV_Catheterization.GetData(C1CV_Catheterization.RowSel, COL_DMSDocumentID))
                            objgloEDocV3Management.Dispose()
                            objgloEDocV3Management = Nothing
                        End If
                    End If

            End Select

        Catch ex As COMException

            If ex.ErrorCode = -2147221164 Then
                MessageBox.Show("Please install corresponding ECG device prerequisites.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception


            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub


    '' swaraj - 04-30-2010 - To implement search functionality ''
    Private Sub txtsearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged
        Try

            Dim strSearch As String
            With txtsearch
                If Trim(.Text) <> "" Then
                    strSearch = Replace(.Text, "'", "''")
                Else
                    strSearch = ""
                End If
            End With

            With C1CV_Catheterization
                ' by Ujwala Atre to Expand All Nodes for Search - as on 20101029
                If strSearch.Trim <> "" And strSearch.Trim.Length = 1 Then
                    ''''''''''''
                    Dim objComm As New Cls_CardioVasculars
                    objComm.ExpandAll(C1CV_Catheterization)
                    objComm = Nothing
                    ''''''''''''
                End If
                ' by Ujwala Atre to Expand All Nodes for Search - as on 20101029
                .Row = .FindRow(strSearch, 1, COL_PROCEDUREDATE, False, False, True)
                If .Row > 0 Then
                    Exit Sub
                End If

                ''InString Search 
                Dim strNode As String = ""
                For i As Int16 = 1 To .Rows.Count - 1
                    strNode = ""
                    strNode = UCase(.GetData(i, COL_PROCEDUREDATE).ToString.Trim)
                    If InStr(strNode, UCase(strSearch.Trim), CompareMethod.Text) > 0 Then
                        .Row = i
                        Exit Sub
                    End If
                Next
                '' ---
            End With


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ''Use to clear search text box
        txtsearch.ResetText()
        txtsearch.Focus()
    End Sub

    Private Sub C1CV_Catheterization_AfterSelChange(sender As Object, e As C1.Win.C1FlexGrid.RangeEventArgs) Handles C1CV_Catheterization.AfterSelChange
        Try
            tsb_ShowDoc.Enabled = False

            If C1CV_Catheterization.RowSel > 0 Then
                If (C1CV_Catheterization.GetData(C1CV_Catheterization.RowSel, COL_DMSDocumentID)) > 0 Then
                    tsb_ShowDoc.Enabled = True
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1CV_Catheterization_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1CV_Catheterization.MouseDoubleClick
        'start of code commented by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration 
        'Try
        '    If C1CV_Catheterization.Row > 0 Then

        '        If C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_PATIENTID) > 0 And C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_VISITID) > 0 Then

        '            'modified by madan on 20100108
        '            Dim _sTestId As String = String.Empty
        '            Dim _sOrderID As String = String.Empty

        '            If C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_TestID) <> "" Then
        '                _sTestId = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_TestID)
        '            End If

        '            If C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_OrderID) <> "" Then
        '                _sOrderID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_OrderID)
        '            End If

        '            mPatientID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_PATIENTID)
        '            mVisitID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_VISITID)
        '            ''mDateofProc = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_PROCEDUREDATE)
        '            mDateofProc = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_DateofStudyInvisible)

        '            If _sTestId.Length > 0 And _sOrderID.Length > 0 Then

        '                gloEmdeonCommon.mdlEcgProcessLayer.sConnectionString = GetConnectionString()
        '                If gloEmdeonCommon.mdlEcgProcessLayer.ValidateUserSettings(gnLoginID) = False Then
        '                    MessageBox.Show("Please configure ECG Device interface settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '                    Return
        '                End If


        '                Dim ofrmDeviceECG As New gloEmdeonCommon.frmECGHealthCentrix(mPatientID, GetConnectionString(), _sOrderID, _sTestId, mVisitID)
        '                ofrmDeviceECG.ShowInTaskbar = False
        '                ofrmDeviceECG.BringToFront()
        '                ofrmDeviceECG.ShowDialog(Me)
        '                ofrmDeviceECG.Dispose()
        '                SetGridSytle()
        '                FillECG()
        '            ElseIf _sTestId.Length <= 0 And _sOrderID.Length > 0 Then
        '                MessageBox.Show("The selected ECG order test status is pending. You can click ""Order Status"" to check for updated status.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '            Else
        '                Dim ofrm As New frmCV_ElectroCardiograms(mPatientID, mDateofProc, mVisitID)
        '                ofrm.ShowDialog(Me)
        '                SetGridSytle()
        '                FillECG()
        '                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Modify, "Modified Records in Cardio Vascular Catheterization", gloAuditTrail.ActivityOutCome.Success)
        '                ''Added Rahul P on 20100916
        '                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Modify, "Modified Records in Cardio Vascular ElectroCardioGram", mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
        '                ''
        '            End If
        '            _sTestId = String.Empty
        '            _sOrderID = String.Empty
        '        End If
        '    End If
        'Catch ex As COMException
        '    If ex.ErrorCode = -2147221164 Then
        '        MessageBox.Show("Please install appropriate ECG device prerequisites.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    End If
        'Catch ex As Exception
        '    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        '    ''Added Rahul P on 20100916
        '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
        '    ''
        'End Try
        'end of code commented by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration 
        'start of code added by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration 
        If C1CV_Catheterization.RowSel > 0 Then

            ModifyTest()

        End If
        'end of code added by manoj jadhav on 20111102 for WelchAllyn DeviceIntegration 
    End Sub

    Private Sub C1CV_Catheterization_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1CV_Catheterization.MouseMove
        ' gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private Function NewDeviceOrder() As Boolean

        'validate settings 
        If Not Validate_Settings(ClsWelchAllynECGLayer.DeviceType.HeartCentrixECGDevice) Then
            NewDeviceOrder = False
            Exit Function
        End If

        'Process flow.
        '1. Check for pending orders
        '2. if the pending order are available then get the satus of the pending order.
        '3. again get the pending orders if the orders are till pending ask the user for cancel the order and place new order
        '4. if user selects cancel order then cancel existing order and place new order.

        Dim objEcg As New clsCVElectroCardioGrams()

        Try
            gloEmdeonCommon.mdlEcgProcessLayer.sConnectionString = GetConnectionString()
            If gloEmdeonCommon.mdlEcgProcessLayer.ValidateUserSettings(gnLoginID) = False Then
                MessageBox.Show("Please configure ECG Device interface settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                NewDeviceOrder = False
                Exit Function
                Return NewDeviceOrder
            End If

            mVisitID = GetVisitID(Now.Date, mPatientID)
            If mVisitID <= 0 Then
                mVisitID = GenerateVisitID(Now.Date, mPatientID)
            End If

            '1. Check for pending orders
            If objEcg.CheckPendingDeviceOrders(mPatientID) Then
                'if found then get the staus of order.

                mVisitID = GetVisitID(Now.Date, mPatientID)

                Dim objECGForm As New gloEmdeonCommon.frmHealthCentrixLoad(mPatientID, frmHealthCentrixLoad.ProcessTypes.GetOrderStatus, "", mVisitID, GetPrefixTransactionID())
                objECGForm.BringToFront()
                objECGForm.ShowInTaskbar = False
                objECGForm.ShowDialog(IIf(IsNothing(objECGForm.Parent), Me, objECGForm.Parent))
                If objECGForm.ErrorString.Length > 0 And objECGForm.ErrorString <> "Test result not available, Please try again later." Then
                    MessageBox.Show(objECGForm.ErrorString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    objECGForm.Dispose()
                    NewDeviceOrder = False
                    Exit Function
                    Return NewDeviceOrder
                End If
                objECGForm.Dispose()
                objECGForm = Nothing
                mVisitID = 0

                'again get the status of order
                If objEcg.CheckPendingDeviceOrders(mPatientID) Then
                    ''Ask user wether the existing order should be canceled and place new order.
                    Dim dgResult As DialogResult
                    dgResult = MessageBox.Show("Do you want to cancel the pending device order and place new device order?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information)

                    If dgResult = Windows.Forms.DialogResult.Yes Then

                        'if yes then cancel and create new device order.
                        mVisitID = GetVisitID(Now.Date, mPatientID)
                        objECGForm = New gloEmdeonCommon.frmHealthCentrixLoad(mPatientID, frmHealthCentrixLoad.ProcessTypes.CancelAndPlaceNewOrder, "", mVisitID, GetPrefixTransactionID())
                        objECGForm.BringToFront()
                        objECGForm.ShowInTaskbar = False
                        objECGForm.ShowDialog(IIf(IsNothing(objECGForm.Parent), Me, objECGForm.Parent))
                        If objECGForm.ErrorString.Length > 0 Then
                            MessageBox.Show(objECGForm.ErrorString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            NewDeviceOrder = False
                            objECGForm.Dispose()
                            Exit Function
                            Return NewDeviceOrder
                        Else
                            '"New ECG order placed successfully"
                            MessageBox.Show("New ECG order placed successfully", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            mECGID = objECGForm.ECGID
                            NewDeviceOrder = True
                            objECGForm.Dispose()
                            Return NewDeviceOrder
                        End If
                        objECGForm.Dispose()
                        mVisitID = 0
                    Else
                        'If no then return
                        NewDeviceOrder = Nothing
                        Exit Function
                    End If

                Else
                    'if no create new device order.
                    mVisitID = GetVisitID(Now.Date, mPatientID)

                    objECGForm = New gloEmdeonCommon.frmHealthCentrixLoad(mPatientID, frmHealthCentrixLoad.ProcessTypes.PlaceOrder, "", mVisitID, GetPrefixTransactionID())
                    objECGForm.BringToFront()
                    objECGForm.ShowInTaskbar = False
                    objECGForm.ShowDialog(IIf(IsNothing(objECGForm.Parent), Me, objECGForm.Parent))
                    If objECGForm.ErrorString.Length > 0 Then
                        MessageBox.Show(objECGForm.ErrorString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        NewDeviceOrder = False
                        objECGForm.Dispose()
                        Exit Function
                        Return NewDeviceOrder
                    Else
                        '"New ECG order placed successfully"
                        MessageBox.Show("New ECG order placed successfully", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        mECGID = objECGForm.ECGID
                        NewDeviceOrder = True
                        Return NewDeviceOrder
                    End If
                    objECGForm.Dispose()
                    mVisitID = 0
                End If
            Else
                'if no create new device order.
                mVisitID = GetVisitID(Now.Date, mPatientID)

                Dim objECGForm As New gloEmdeonCommon.frmHealthCentrixLoad(mPatientID, frmHealthCentrixLoad.ProcessTypes.PlaceOrder, "", mVisitID, GetPrefixTransactionID())
                objECGForm.BringToFront()
                objECGForm.ShowInTaskbar = False
                objECGForm.ShowDialog(IIf(IsNothing(objECGForm.Parent), Me, objECGForm.Parent))
                If objECGForm.ErrorString.Length > 0 Then
                    MessageBox.Show(objECGForm.ErrorString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    NewDeviceOrder = False
                    objECGForm.Dispose()
                    Exit Function
                    Return NewDeviceOrder
                Else
                    '"New ECG order placed successfully"
                    MessageBox.Show("New ECG order placed successfully", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    mECGID = objECGForm.ECGID
                    NewDeviceOrder = True
                    objECGForm.Dispose()
                    Exit Function
                    Return NewDeviceOrder
                End If
                objECGForm.Dispose()
                mVisitID = 0
            End If
            'SetGridSytle()
            'FillECG()

        Catch ex As COMException
            If ex.ErrorCode = -2147221164 Then
                MessageBox.Show("Please install appropriate ECG device prerequisites.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return Nothing
        Finally
            objEcg = Nothing
        End Try
    End Function

    Private Sub LoadPatientStrip()
        Try
            '  Add Patient Details Control 
            gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip

            gloUC_PatientStrip1.Padding = New Padding(3, 0, 3, 0)
            gloUC_PatientStrip1.BringToFront()
            pnlToolStrip.SendToBack()
            Panel1.BringToFront()
            pnlC1CV_ElectroPhysio.BringToFront()
            gloUC_PatientStrip1.DTPEnabled = False
            If True Then
                gloUC_PatientStrip1.Dock = DockStyle.Top
                gloUC_PatientStrip1.DTPValue = Now
                gloUC_PatientStrip1.ShowDetail(mPatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.None, 0, 0, 0, False, _
                 False, False, "", False)
            End If
            Me.Controls.Add(gloUC_PatientStrip1)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub
    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return mPatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property

    Public Function Device_Availabel() As Boolean
        Dim ObjClsWelchAllynECGLayer As ClsWelchAllynECGLayer = Nothing
        Try
            ObjClsWelchAllynECGLayer = New ClsWelchAllynECGLayer(mdlGeneral.GetConnectionString())
            ObjClsWelchAllynECGLayer.Device_Available(IsHeartCentrixECGDeviceConfigured, IsWelchAllynECGDeviceConfigured, IsMidmarkIQECGDeviceConfigured)
            tlbHealthCentrix.Visible = False
            tlbGetPendingStatus.Visible = False
            tlbImportTest.Visible = False
            If (IsHeartCentrixECGDeviceConfigured) Then
                tlbHealthCentrix.Visible = True
                tlbGetPendingStatus.Visible = True
            End If
            If (IsWelchAllynECGDeviceConfigured) Then
                tlbHealthCentrix.Visible = True
                tlbImportTest.Visible = True
            End If
            If (IsMidmarkIQECGDeviceConfigured) Then
                tlbHealthCentrix.Visible = True
            End If
            If IsHeartCentrixECGDeviceConfigured Or IsWelchAllynECGDeviceConfigured Or IsMidmarkIQECGDeviceConfigured Then
                Device_Availabel = True
            Else
                Device_Availabel = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Load, "Exception occurs while checking device availability = " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally
            If Not ObjClsWelchAllynECGLayer Is Nothing Then
                ObjClsWelchAllynECGLayer.Dispose()
                ObjClsWelchAllynECGLayer = Nothing
            End If
        End Try
    End Function

    Public Function NewDeviceTest() As Boolean

        Try
            NewDeviceTest = False
            'if only Cardio Science ECG Device is Enabled
            If IsHeartCentrixECGDeviceConfigured And Not IsWelchAllynECGDeviceConfigured And Not IsMidmarkIQECGDeviceConfigured Then

                NewDeviceTest = NewDeviceOrder()

                'if only WelchAllyn ECG Device is Enabled
            ElseIf Not IsHeartCentrixECGDeviceConfigured And IsWelchAllynECGDeviceConfigured And Not IsMidmarkIQECGDeviceConfigured Then

                NewDeviceTest = WelchAllynDeviceTest(FrmWelChallynECG.TestType.NewTest, String.Empty, 0)

                'if only Midmark IQ ECG Device is Enabled
            ElseIf Not IsHeartCentrixECGDeviceConfigured And Not IsWelchAllynECGDeviceConfigured And IsMidmarkIQECGDeviceConfigured Then


                NewDeviceTest = MidmarkIQECGTest(0, String.Empty)

                'multiple device are enabled
            Else

                Select Case GetSelectedDevice()

                    Case ClsWelchAllynECGLayer.DeviceType.HeartCentrixECGDevice

                        NewDeviceTest = NewDeviceOrder()

                    Case ClsWelchAllynECGLayer.DeviceType.WelchAllynECGDevice

                        NewDeviceTest = WelchAllynDeviceTest(FrmWelChallynECG.TestType.NewTest, String.Empty, 0)

                    Case ClsWelchAllynECGLayer.DeviceType.MidMarkECGDevice

                        NewDeviceTest = MidmarkIQECGTest(0, String.Empty)
                    Case Else
                        NewDeviceTest = False
                End Select
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, "Error In frmCV_VWElectroCardiograms.NewDeviceTest()" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            NewDeviceTest = False
        End Try
    End Function

    Private Function GetSelectedDevice() As ClsWelchAllynECGLayer.DeviceType
        Dim ObjFrmSelectECGDeviceNew As FrmSelectECGDevice = Nothing
        Try

            ObjFrmSelectECGDeviceNew = New gloEmdeonInterface.Forms.FrmSelectECGDevice(IsHeartCentrixECGDeviceConfigured, IsWelchAllynECGDeviceConfigured, IsMidmarkIQECGDeviceConfigured)

            ObjFrmSelectECGDeviceNew.ShowDialog(IIf(IsNothing(ObjFrmSelectECGDeviceNew.Parent), Me, ObjFrmSelectECGDeviceNew.Parent))

            GetSelectedDevice = ObjFrmSelectECGDeviceNew.GetSelectedDeviceType

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, "Error In frmCV_VWElectroCardiograms.NewDeviceTest()" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            GetSelectedDevice = ClsWelchAllynECGLayer.DeviceType.NoDeviceSelected
        Finally
            If Not ObjFrmSelectECGDeviceNew Is Nothing Then
                ObjFrmSelectECGDeviceNew.Dispose()
                ObjFrmSelectECGDeviceNew = Nothing
            End If
        End Try
    End Function

    Private Function MidmarkIQECGTest(ByVal nECGTestId As Long, ByVal TestID As String) As Boolean
        Dim ObjFrmMidmarkECGTest As FrmMidmarkECGTest = Nothing
        Dim strgloDeviceConnection As String = String.Empty
        Try
            MidmarkIQECGTest = False
            If Validate_Settings(ClsWelchAllynECGLayer.DeviceType.MidMarkECGDevice) Then

                strgloDeviceConnection = gloEmdeonInterface.Classes.clsSpiroGeneralModule.RetriveDeviceCon(gnClinicID, mdlGeneral.GetConnectionString())

                ObjFrmMidmarkECGTest = New FrmMidmarkECGTest(nECGTestId, mPatientID, strgloDeviceConnection, mdlGeneral.GetConnectionString(), TestID)
                ObjFrmMidmarkECGTest.ShowDialog(IIf(IsNothing(ObjFrmMidmarkECGTest.Parent), Me, ObjFrmMidmarkECGTest.Parent))

                If (ObjFrmMidmarkECGTest.IsDataChanged()) Then
                    mECGID = ObjFrmMidmarkECGTest.ECGID
                    MidmarkIQECGTest = True
                Else
                    MidmarkIQECGTest = False
                End If
                ObjFrmMidmarkECGTest.Dispose()
                ObjFrmMidmarkECGTest = Nothing
            End If

        Catch ex As Exception
            MidmarkIQECGTest = False
            ex = Nothing
        End Try
    End Function

    Private Function WelchAllynDeviceTest(ByVal TesType As FrmWelChallynECG.TestType, ByVal TestID As String, ByVal ECGID As Int64) As Boolean
        Dim ObjFrmWelChallynECG As FrmWelChallynECG = Nothing
        Dim ofrmWelchAllynECG_Local As frmWelchAllynECG_Local = Nothing

        Try
            WelchAllynDeviceTest = False
            If Validate_Settings(ClsWelchAllynECGLayer.DeviceType.WelchAllynECGDevice) Then
                If bEnableLocalWelchAllynECGDevice Then
                    If IsLocalWelchAllynECGDeviceEnabled() Then
                        ofrmWelchAllynECG_Local = New frmWelchAllynECG_Local(TesType, mPatientID, TestID, mdlGeneral.gnLoginID, mdlGeneral.gClinicID, mdlGeneral.GetConnectionString(), ECGID)
                        ofrmWelchAllynECG_Local.ShowDialog(IIf(IsNothing(ofrmWelchAllynECG_Local.Parent), Me, ofrmWelchAllynECG_Local.Parent))
                        If ofrmWelchAllynECG_Local.IsECGTestDataChanged Then
                            WelchAllynDeviceTest = True
                            mECGID = ofrmWelchAllynECG_Local.nECGID()
                        End If
                    Else
                        MessageBox.Show("Please enable local ECG Device interface setting in gloLDSSniffer service.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                Else
                    ObjFrmWelChallynECG = New FrmWelChallynECG(TesType, mPatientID, TestID, mdlGeneral.gnLoginID, mdlGeneral.gClinicID, mdlGeneral.GetConnectionString())
                    ObjFrmWelChallynECG.ShowDialog(IIf(IsNothing(ObjFrmWelChallynECG.Parent), Me, ObjFrmWelChallynECG.Parent))
                    If ObjFrmWelChallynECG.IsECGTestDataChanged Then
                        WelchAllynDeviceTest = True
                        mECGID = ObjFrmWelChallynECG.nECGID()
                    End If
                End If
                
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            WelchAllynDeviceTest = False
            ex = Nothing
        Finally
            If Not ObjFrmWelChallynECG Is Nothing Then
                ObjFrmWelChallynECG.Dispose()
                ObjFrmWelChallynECG = Nothing
            End If

            If Not ofrmWelchAllynECG_Local Is Nothing Then
                ofrmWelchAllynECG_Local.Dispose()
                ofrmWelchAllynECG_Local = Nothing
            End If
        End Try
    End Function

    Private Function IsLocalWelchAllynECGDeviceEnabled() As Boolean
        Try
            IsLocalWelchAllynECGDeviceEnabled = False

            Dim sConfigPath As String = "\\tsclient\Y\Config"          '"Y:\Config"         '"\\tsclient\Y\Config"

            If Directory.Exists(sConfigPath) Then
                Dim sConfigFilePath = sConfigPath & "\Service.config"

                If File.Exists(sConfigFilePath) Then
                    Dim oFile As Configuration.ExeConfigurationFileMap = New Configuration.ExeConfigurationFileMap()
                    oFile.ExeConfigFilename = sConfigFilePath

                    Dim oConfig As Configuration.Configuration = Configuration.ConfigurationManager.OpenMappedExeConfiguration(oFile, Configuration.ConfigurationUserLevel.None)

                    If Not IsNothing(oConfig) Then
                        If Not IsNothing(oConfig.AppSettings.Settings("EnableLocalWelchAllynECGDevice")) Then
                            If String.Compare(oConfig.AppSettings.Settings("EnableLocalWelchAllynECGDevice").Value, "True", True) = 0 Then
                                IsLocalWelchAllynECGDeviceEnabled = True
                            End If
                        End If
                    End If

                    oConfig = Nothing
                    oFile = Nothing
                End If

                sConfigFilePath = Nothing
            End If

            sConfigPath = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            IsLocalWelchAllynECGDeviceEnabled = False
            ex = Nothing
        End Try

        Return IsLocalWelchAllynECGDeviceEnabled
    End Function

    Private Function GetCardioScinceECGPendingOrders() As Boolean
        Dim objEcg As clsCVElectroCardioGrams = Nothing
        Dim objECGForm As frmHealthCentrixLoad = Nothing
        Try
            GetCardioScinceECGPendingOrders = False
            If Validate_Settings(ClsWelchAllynECGLayer.DeviceType.HeartCentrixECGDevice) Then
                objEcg = New clsCVElectroCardioGrams()
                If objEcg.CheckPendingDeviceOrders(mPatientID) Then
                    gloEmdeonCommon.mdlEcgProcessLayer.sConnectionString = GetConnectionString()
                    If gloEmdeonCommon.mdlEcgProcessLayer.ValidateUserSettings(gnLoginID) = False Then
                        MessageBox.Show("Please configure ECG Device interface settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        GetCardioScinceECGPendingOrders = False
                        Exit Try
                    End If
                    objECGForm = New frmHealthCentrixLoad(mPatientID, frmHealthCentrixLoad.ProcessTypes.GetOrderStatus, "", mVisitID, GetPrefixTransactionID())
                    objECGForm.BringToFront()
                    objECGForm.ShowInTaskbar = False
                    objECGForm.ShowDialog(IIf(IsNothing(objECGForm.Parent), Me, objECGForm.Parent))
                    If objECGForm.ErrorString.Length > 0 Then
                        MessageBox.Show(objECGForm.ErrorString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        mECGID = objECGForm.ECGID
                        GetCardioScinceECGPendingOrders = True
                    End If
                End If
            Else
                GetCardioScinceECGPendingOrders = False
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, "Error while checking pending ECG Test from HertCentrix ECG device " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            GetCardioScinceECGPendingOrders = False
        Finally
            objEcg = Nothing
            If Not objECGForm Is Nothing Then
                objECGForm.Dispose()
                objECGForm = Nothing
            End If
        End Try

    End Function

    Public Function UpdateTest(ByVal nECGID As Long, ByVal nPatientId As Long, ByVal mVisitID As Long, ByVal VisitDate As DateTime, ByVal nClinicID As Long, ByVal TestId As String, ByVal OrderId As String, ByVal DeviceType As String) As Boolean
        Try
            UpdateTest = False
            'start of code added by manoj jadhav on 20111111 for change Cardio to HertCentrix
            If DeviceType.Trim().ToUpper() = "Cardio".Trim().ToUpper() Then
                DeviceType = "HeartCentrix"
            End If
            'start of code added by manoj jadhav on 20111111 for change Cardio to HertCentrix

            Select Case DeviceType.Trim().ToUpper

                Case "HeartCentrix".ToUpper()   'modified of code added by manoj jadhav on 20111111 for change Cardio to HertCentrix

                    If Not Validate_Settings(ClsWelchAllynECGLayer.DeviceType.HeartCentrixECGDevice) Then
                        UpdateTest = False
                        Exit Select
                    Else
                        gloEmdeonCommon.mdlEcgProcessLayer.sConnectionString = GetConnectionString()
                        If gloEmdeonCommon.mdlEcgProcessLayer.ValidateUserSettings(gnLoginID) = False Then
                            MessageBox.Show("Please configure ECG Device interface settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            UpdateTest = False
                            Exit Select
                        End If

                        If TestId.Length > 0 And OrderId.Length > 0 Then
                            Dim ofrmDeviceECG As frmECGHealthCentrix = Nothing
                            Try
                                ofrmDeviceECG = New frmECGHealthCentrix(mPatientID, GetConnectionString(), OrderId, TestId, mVisitID)
                                ofrmDeviceECG.ShowInTaskbar = False
                                ofrmDeviceECG.BringToFront()
                                ofrmDeviceECG.ShowDialog(IIf(IsNothing(ofrmDeviceECG.Parent), Me, ofrmDeviceECG.Parent))
                                UpdateTest = True
                            Catch ex As Exception
                                ex = Nothing
                            Finally
                                If Not ofrmDeviceECG Is Nothing Then
                                    ofrmDeviceECG.Dispose()
                                    ofrmDeviceECG = Nothing
                                End If
                            End Try
                        Else
                            UpdateTest = False
                            MessageBox.Show("The selected ECG order test status is pending. You can click ""Order Status"" to check for updated status.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End If
                Case "WelChAllyn".ToUpper()
                    If Not Validate_Settings(ClsWelchAllynECGLayer.DeviceType.WelchAllynECGDevice) Then
                        UpdateTest = False
                        Exit Select
                    Else
                        If TestId.Trim.Length >= 0 Then
                            UpdateTest = WelchAllynDeviceTest(FrmWelChallynECG.TestType.UpdateTest, TestId, nECGID)
                            'C1CV_Catheterization.Tree.Show(0)
                        End If
                    End If

                Case "Local".ToUpper()
                    Dim ofrm As New frmCV_ElectroCardiograms(mPatientID, VisitDate, mVisitID)
                    Try
                        ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                        If ofrm.MainCathID > 0 Then
                            mECGID = ofrm.MainCathID
                            UpdateTest = True
                        Else
                            UpdateTest = False
                        End If
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, "Error while updating Local ECG order " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                        ex = Nothing
                        UpdateTest = False
                    Finally
                        If Not ofrm Is Nothing Then
                            ofrm.Dispose()
                            ofrm = Nothing
                        End If
                    End Try
                Case "Midmark IQ ECG".ToUpper()
                    Try
                        If Not Validate_Settings(ClsWelchAllynECGLayer.DeviceType.MidMarkECGDevice) Then
                            UpdateTest = False
                            Exit Select
                        Else
                            UpdateTest = MidmarkIQECGTest(nECGID, TestId)
                        End If
                    Catch ex As Exception
                        UpdateTest = False
                    End Try
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, "Error while updating ECG Order" + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Return Nothing
        Finally

        End Try
    End Function

    Private Sub ModifyTest()
        Dim _sTestId As String = String.Empty
        Dim _sOrderID As String = String.Empty
        Dim _sDeviceType As String = String.Empty
        Try
            If C1CV_Catheterization.GetData(C1CV_Catheterization.RowSel, COL_PATIENTID) > 0 And C1CV_Catheterization.GetData(C1CV_Catheterization.RowSel, COL_VISITID) > 0 Then
                mECGID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_CATHETERIZATIONID)
                _sTestId = Convert.ToString(C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_TestID))
                _sOrderID = Convert.ToString(C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_OrderID))
                mPatientID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_PATIENTID)
                mECGID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_CATHETERIZATIONID)
                mExamID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_EXAMID)
                mVisitID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_VISITID)
                mClinicID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_CLINICID)
                mDateofProc = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_DateofStudyInvisible)
                _sDeviceType = Convert.ToString(C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_deviceType))
                If UpdateTest(mECGID, mPatientID, mVisitID, mDateofProc, mClinicID, _sTestId, _sOrderID, _sDeviceType) Then
                    FillECG()
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, "Error in frmCV_VWElectroCardiograms.ModifyTest() " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
        Finally
            _sTestId = String.Empty
            _sOrderID = String.Empty
            _sDeviceType = String.Empty
        End Try
    End Sub

    Private Function Validate_Settings(ByVal DeviceType As ClsWelchAllynECGLayer.DeviceType) As Boolean
        Try
            Validate_Settings = ClsWelchAllynECGLayerGenral.Validate_Device_Settings(DeviceType, mClinicID, mdlGeneral.gnLoginID, mdlGeneral.GetConnectionString(), mdlGeneral.gstrMessageBoxCaption, IsHeartCentrixECGDeviceConfigured, IsWelchAllynECGDeviceConfigured, IsMidmarkIQECGDeviceConfigured)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, "Error while validating settings " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            Validate_Settings = False
        End Try
    End Function

    'new function added by manoj jadhav on 20111108 to fix bug NO.13905
    Private Function DeleteECGTest()
        Dim bDeleteECGTest As Boolean = False
        Dim _sOrderID As String = String.Empty
        Dim objCV_ECG As clsCVElectroCardioGrams = Nothing
        Try
            If C1CV_Catheterization.RowSel > 0 Then
                _sOrderID = Convert.ToString(C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_OrderID))
                If _sOrderID.Length > 0 Then
                    MessageBox.Show("Device records cannot be deleted.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    bDeleteECGTest = False
                Else
                    Long.TryParse(Convert.ToString(C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_PATIENTID)), mPatientID)
                    Long.TryParse(Convert.ToString(C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_CATHETERIZATIONID)), mECGID)
                    Long.TryParse(Convert.ToString(C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_EXAMID)), mExamID)
                    Long.TryParse(Convert.ToString(C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_VISITID)), mVisitID)
                    Long.TryParse(Convert.ToString(C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_CLINICID)), mClinicID)
                    DateTime.TryParse(Convert.ToString(C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_DateofStudyInvisible)), mDateofProc)
                    If MessageBox.Show(" Are you sure you want to delete the ECG Record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                        objCV_ECG = New clsCVElectroCardioGrams
                        objCV_ECG.DeleteElectroCardioGrams(mPatientID, mVisitID, mDateofProc)
                        bDeleteECGTest = True
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ex = Nothing
            bDeleteECGTest = False
        Finally
            _sOrderID = String.Empty
            objCV_ECG = Nothing
        End Try
        DeleteECGTest = bDeleteECGTest
    End Function

End Class