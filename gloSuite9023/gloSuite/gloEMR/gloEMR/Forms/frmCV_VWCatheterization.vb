Imports System.IO
Imports Microsoft.Win32
Imports C1.Win.C1FlexGrid
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.Data.Common



Public Class frmCV_VWCatheterization
    Implements IPatientContext

    Dim mPatientID As Long = 0
    Dim mCatheterizationID As Long = 0
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

    Private COL_COUNT As Integer = 18



    Public Sub New(ByVal nPatientId As Long)
        '
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mPatientID = nPatientId
        mClinicID = 1



    End Sub

    Private Sub frmCV_VWCatheterization_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    Private Sub frmCV_VWCatheterization_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        SetGridSytle()
        FillCatheterization()

        ''swaraj - 30-04-2010 - To disable new button if patient exists
        'mVisitID = GetVisitID(Now.Date, mPatientID)
        'If mVisitID > 0 Then
        '    ts_btnAdd.Enabled = False
        'End If
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, mPatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
    ''swaraj - 04-28-2010 - To set C1flexgrid style''
    Private Sub SetGridSytle()
        '  Dim struser As String
        With C1CV_Catheterization
            ' Dim i As Int16
            .Dock = DockStyle.Fill
            .Cols.Count = COL_COUNT  'column count
            .Rows.Fixed = 1
            .Rows.Count = 1
            .AllowEditing = False
            .AllowAddNew = False

            .Styles.ClearUnused()

            .Cols(COL_CATHETERIZATIONID).Width = .Width * 0
            .Cols(COL_CATHETERIZATIONID).AllowEditing = False
            .SetData(0, COL_CATHETERIZATIONID, "Catheterization ID")
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
            .SetData(0, COL_INTERVENTIONTYPE, "Intervention Type")
            .Cols(COL_INTERVENTIONTYPE).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_PHYSICIANNAME).Width = .Width * 0
            .Cols(COL_PHYSICIANNAME).AllowEditing = False
            .SetData(0, COL_PHYSICIANNAME, "Physician Name")
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
            .SetData(0, COL_NARRATIVESUMMARY, "Narrative Summary")
            .Cols(COL_NARRATIVESUMMARY).TextAlignFixed = TextAlignEnum.LeftCenter


            .Cols(COL_PROCEDUREDATE).Width = .Width * 1.3
            ''.Cols(COL_PROCEDUREDATE).AllowEditing = False
            .SetData(0, COL_PROCEDUREDATE, "Procedure Date")
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
        End With
        ''swaraj - 04-28-2010 - To set C1flexgrid style''
    End Sub
    'swaraj - 28-04-2010 - To fill catheterization information ''
    Private Sub FillCatheterization()

        Try
            Dim _Row As Integer

            ''set properties of treeview in flexgrid
            With C1CV_Catheterization
                .Tree.Column = COL_PROCEDUREDATE
                .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                .Tree.LineStyle = Drawing2D.DashStyle.Solid
                .Tree.Indent = 15
            End With

            Dim dtProcDate As DataTable
            Dim dtCPT As DataTable
            Dim dtTypeIntervention As DataTable
            Dim dtAddPhysician As DataTable
            Dim dtPressure As DataTable
            Dim dtSaturation As DataTable
            Dim dtLV As DataTable
            ''Dim dtRV As DataTable
            Dim dtNarrativeSummary As DataTable
            Dim dtTestType As DataTable


            Dim mDOS As Int16
            Dim mCPT As Int16
            Dim mTestType As Int16
            Dim mTypeintervention As Int16
            Dim mPhysician As Int16
            Dim mPressure As Int16
            Dim mSaturation As Int16
            Dim mLV As Int16
            ''Dim mRV As Int16
            Dim mNarrativesummary As Int16

            Dim strdtQry As String
            Dim strCPTcodeQry As String
            Dim strTestTypeQry As String
            Dim strTypeInterventionQry As String
            Dim strPhysicianQry As String
            Dim strPressureQry As String
            Dim strSaturationQry As String
            Dim strLVQry As String
            ''Dim strRVQry As String
            Dim strNarrativesummaryQry As String
            Dim strconcatCPT1 As String = ""
            Dim nextRow As Integer
            Dim strcombine As String


            strdtQry = "SELECT Distinct isnull(nCatheterizationID,0) as nCatheterizationID,isnull(nPatientID,0) as nPatientID,isnull(nExamID,0) as nExamID,isnull(nVisitID,0) as nVisitID,isnull(nClinicID,0) as nClinicID,dtProcedureDate as ProcedureDate from CV_Catheterization   where nGroupID=0 AND nPatientID='" & mPatientID & "' order by ProcedureDate"
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)
            dtProcDate = oDB.ReadQueryDataTable(strdtQry)
            oDB.Disconnect()

            With dtProcDate
                If IsNothing(dtProcDate) = False Then
                    For mDOS = 0 To .Rows.Count - 1
                        Dim CatheterizationID As Int64 = 0
                        Dim PatientID As Int64 = 0
                        Dim ExamID As Int64 = 0
                        Dim VisitID As Int64 = 0
                        Dim ClinicID As Int64 = 0
                        Dim DateofProc As Date

                        Dim count As Integer = mDOS + 1
                        If CStr(dtProcDate.Rows(mDOS)("ProcedureDate")).Trim <> "" Then
                            C1CV_Catheterization.Rows.Add()
                            _Row = C1CV_Catheterization.Rows.Count - 1

                            ''set the properties for newly added row
                            With C1CV_Catheterization.Rows(_Row)
                                .AllowEditing = False
                                .ImageAndText = True
                                .Height = 24
                                .IsNode = True
                                .Node.Level = 0
                                .Node.Data = Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString
                                .Node.Image = ImageList1.Images(1)
                            End With
                            nextRow = _Row
                            With C1CV_Catheterization
                                .SetData(_Row, COL_CATHETERIZATIONID, dtProcDate.Rows(mDOS)("nCatheterizationID"))
                                .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                .SetData(_Row, COL_EXAMID, dtProcDate.Rows(mDOS)("nExamID"))
                                .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                .SetData(_Row, COL_CLINICID, dtProcDate.Rows(mDOS)("nClinicID"))
                                ''.SetData(_Row, COL_PROCEDUREDATE, Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString())
                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString)

                                CatheterizationID = dtProcDate.Rows(mDOS)("nCatheterizationID")
                                PatientID = dtProcDate.Rows(mDOS)("nPatientID")
                                ExamID = dtProcDate.Rows(mDOS)("nExamID")
                                VisitID = dtProcDate.Rows(mDOS)("nVisitID")
                                ClinicID = dtProcDate.Rows(mDOS)("nClinicID")
                                DateofProc = Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString()
                            End With

                            Dim dtProcedureDate As Date = dtProcDate.Rows(mDOS)("ProcedureDate")


                            '' Query for selecting CPT Code ''
                            strCPTcodeQry = "SELECT DISTINCT isnull(sCPTCode,'') as sCPTCode from CV_Catheterization where nPatientID=" & mPatientID & " AND dtProcedureDate='" & dtProcedureDate & "' AND nGroupID='" & CatheterizationID & "' AND sCPTCode<>''"
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
                                            .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                            .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString)
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
                                                .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString)
                                            End With



                                            ''Query for selecting Test Type
                                            strTestTypeQry = "SELECT DISTINCT isnull(sTestType,'') as sTestType from CV_Catheterization where nPatientID=" & mPatientID & " AND dtProcedureDate='" & dtProcedureDate & "' AND nGroupID=" & CatheterizationID & " AND sCPTCode='" & strCurrentCPT & "'  and sTestType<>''"
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
                                                    .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                    .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString)
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
                                                                .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                                .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString)
                                                            End With
                                                        Next
                                                    End If
                                                End With
                                            End If  ''If dtTestType.Rows.Count > 0
                                        End If
                                    Next   ''For mcpt=0 to .Rows.Count-1
                                End If   ''with dtCPT
                            End With



                            '' Query for selecting  Type Of Intervention ''
                            strTypeInterventionQry = "SELECT DISTINCT ISNULL(sInterventionType,'') as InterventionType from CV_Catheterization where nPatientID=" & mPatientID & " AND dtProcedureDate='" & dtProcedureDate & "' AND nCatheterizationID=" & CatheterizationID
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
                                            .Node.Data = "Type Of Intervention"
                                            .Node.Image = ImageList1.Images(15)
                                        End With
                                        With C1CV_Catheterization
                                            .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                            .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString)
                                        End With
                                    End If
                                    For mTypeintervention = 0 To dtTypeIntervention.Rows.Count - 1
                                        Dim strInterventionType As String = dtTypeIntervention.Rows(mTypeintervention)("InterventionType")
                                        If strInterventionType.Trim <> "" Then
                                            C1CV_Catheterization.Rows.Add()
                                            _Row = C1CV_Catheterization.Rows.Count - 1

                                            With C1CV_Catheterization.Rows(_Row)
                                                .AllowEditing = True
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 2
                                                .Node.Data = dtTypeIntervention.Rows(mTypeintervention)("InterventionType")
                                                .Node.Image = ImageList1.Images(3)
                                            End With
                                            With C1CV_Catheterization
                                                .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString)
                                            End With
                                        End If
                                    Next  'For mTypeintervention = 0 To .Rows.Count - 1
                                End If  'dtTypeIntervention
                            End With



                            '' Query for selecting Physician ''
                            strPhysicianQry = "SELECT DISTINCT ISNULL(sPhysicianName,'') as PhysicianName from CV_Catheterization where nPatientID=" & mPatientID & " AND dtProcedureDate='" & dtProcedureDate & "'  AND nCatheterizationID=" & CatheterizationID
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
                                            .Node.Data = "Physician's"
                                            .Node.Image = ImageList1.Images(18)
                                        End With
                                        With C1CV_Catheterization
                                            .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                            .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString)
                                        End With
                                    End If
                                    For mPhysician = 0 To .Rows.Count - 1
                                        Dim strPhysician As String = dtAddPhysician.Rows(mPhysician)("PhysicianName")
                                        If strPhysician.Trim <> "" Then
                                            C1CV_Catheterization.Rows.Add()
                                            _Row = C1CV_Catheterization.Rows.Count - 1

                                            With C1CV_Catheterization.Rows(_Row)
                                                .AllowEditing = True
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 2
                                                .Node.Data = dtAddPhysician.Rows(mPhysician)("PhysicianName")
                                                .Node.Image = ImageList1.Images(3)
                                            End With
                                            With C1CV_Catheterization
                                                .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString)
                                            End With
                                        End If
                                    Next  'For mPhysician = 0 To .Rows.Count - 1
                                End If  'dtAddPhysician
                            End With



                            '' Query for selecting Pressure ''
                            strPressureQry = "SELECT DISTINCT isnull(sRAPressure,'') as sRAPressure,isnull(sLAPressure,'') as sLAPressure,isnull(sRPulmonary,'') as sRPulmonary,isnull(sLPulmonary,'') as sLPulmonary,isnull(sRV,'') as sRV,isnull(sLV,'') as sLV,isnull(sPAPressure,'') as sPAPressure,isnull(sPeak,'') as sPeak,isnull(sDiastolic,'') as sDiastolic,isnull(sMean,'') as sMean from CV_Catheterization where nPatientID=" & mPatientID & " AND dtProcedureDate='" & dtProcedureDate & "' AND nCatheterizationID=" & CatheterizationID
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
                                            .Node.Data = "Pressure"
                                            .Node.Image = ImageList1.Images(19)
                                        End With
                                        With C1CV_Catheterization
                                            .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                            .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString)
                                        End With
                                    End If
                                    For mPressure = 0 To dtPressure.Rows.Count - 1
                                        strcombine = ""
                                        If Not IsNothing(dtPressure.Rows(mPressure)("sRAPressure")) Then
                                            If (dtPressure.Rows(mPressure)("sRAPressure").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "RA" + " " + ":" + " " + dtPressure.Rows(mPressure)("sRAPressure")
                                                End If
                                            End If
                                        End If
                                        If Not IsNothing(dtPressure.Rows(mPressure)("sLAPressure")) Then
                                            If (dtPressure.Rows(mPressure)("sLAPressure").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "LA" + " " + ":" + " " + dtPressure.Rows(mPressure)("sLAPressure")
                                                Else
                                                    strcombine = strcombine + "    " + "LA" + " " + ":" + " " + dtPressure.Rows(mPressure)("sLAPressure")
                                                End If

                                            End If
                                        End If
                                        If Not IsNothing(dtPressure.Rows(mPressure)("sRPulmonary")) Then
                                            If (dtPressure.Rows(mPressure)("sRPulmonary").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "Right Pulmonary" + " " + ":" + " " + dtPressure.Rows(mPressure)("sRPulmonary")
                                                Else
                                                    strcombine = strcombine + "    " + "Right Pulmonary Wedge" + " " + ":" + " " + dtPressure.Rows(mPressure)("sRPulmonary")
                                                End If

                                            End If
                                        End If
                                        If Not IsNothing(dtPressure.Rows(mPressure)("sLPulmonary")) Then
                                            If (dtPressure.Rows(mPressure)("sLPulmonary").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "Left Pulmonary" + " " + ":" + " " + dtPressure.Rows(mPressure)("sLPulmonary")
                                                Else
                                                    strcombine = strcombine + "    " + "Left Pulmonary Wedge" + " " + ":" + " " + dtPressure.Rows(mPressure)("sLPulmonary")
                                                End If

                                            End If
                                        End If
                                        If Not IsNothing(dtPressure.Rows(mPressure)("sRV")) Then
                                            If (dtPressure.Rows(mPressure)("sRV").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "RV" + " " + ":" + " " + dtPressure.Rows(mPressure)("sRV")
                                                Else
                                                    strcombine = strcombine + "    " + "RV" + " " + ":" + " " + dtPressure.Rows(mPressure)("sRV")
                                                End If
                                            End If
                                        End If
                                        If Not IsNothing(dtPressure.Rows(mPressure)("sLV")) Then
                                            If (dtPressure.Rows(mPressure)("sLV").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "LV" + " " + ":" + " " + dtPressure.Rows(mPressure)("sLV")
                                                Else
                                                    strcombine = strcombine + "    " + "LV" + " " + ":" + " " + dtPressure.Rows(mPressure)("sLV")
                                                End If
                                            End If
                                        End If
                                        If Not IsNothing(dtPressure.Rows(mPressure)("sPAPressure")) Then
                                            If (dtPressure.Rows(mPressure)("sPAPressure").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "PA" + " " + ":" + " " + dtPressure.Rows(mPressure)("sPAPressure")
                                                Else
                                                    strcombine = strcombine + "    " + "PA" + " " + ":" + " " + dtPressure.Rows(mPressure)("sPAPressure")
                                                End If
                                            End If
                                        End If
                                        If Not IsNothing(dtPressure.Rows(mPressure)("sPeak")) Then
                                            If (dtPressure.Rows(mPressure)("sPeak").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "Peak" + " " + ":" + " " + dtPressure.Rows(mPressure)("sPeak")
                                                Else
                                                    strcombine = strcombine + "    " + "Peak" + " " + ":" + " " + dtPressure.Rows(mPressure)("sPeak")
                                                End If
                                            End If
                                        End If
                                        If Not IsNothing(dtPressure.Rows(mPressure)("sDiastolic")) Then
                                            If (dtPressure.Rows(mPressure)("sDiastolic").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "Diastolic" + " " + ":" + " " + dtPressure.Rows(mPressure)("sDiastolic")
                                                Else
                                                    strcombine = strcombine + "    " + "Diastolic" + " " + ":" + " " + dtPressure.Rows(mPressure)("sDiastolic")
                                                End If
                                            End If
                                        End If
                                        If Not IsNothing(dtPressure.Rows(mPressure)("sMean")) Then
                                            If (dtPressure.Rows(mPressure)("sMean").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "Mean" + " " + ":" + " " + dtPressure.Rows(mPressure)("sMean")
                                                Else
                                                    strcombine = strcombine + "    " + "Mean" + " " + ":" + " " + dtPressure.Rows(mPressure)("sMean")
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
                                                .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString)
                                            End With
                                        End If
                                    Next     'For mPressure = 0 To .Rows.Count - 1
                                End If  'dtPressure
                            End With




                            '' Query for selecting Saturation ''
                            strSaturationQry = "SELECT DISTINCT isnull(sIVC,'') as sIVC,isnull(sSVC,'') as sSVC,isnull(sRASaturations,'') as sRASaturations,isnull(sRVSaturations,'') as sRVSaturations,isnull(sPASaturations,'') as sPASaturations from CV_Catheterization where nPatientID=" & mPatientID & " AND dtProcedureDate='" & dtProcedureDate & "'  AND nCatheterizationID=" & CatheterizationID
                            oDB.Connect(GetConnectionString)
                            dtSaturation = oDB.ReadQueryDataTable(strSaturationQry)
                            oDB.Disconnect()

                            With dtSaturation
                                If IsNothing(dtSaturation) = False Then
                                    If dtSaturation.Rows.Count > 0 Then
                                        C1CV_Catheterization.Rows.Add()
                                        _Row = C1CV_Catheterization.Rows.Count - 1

                                        With C1CV_Catheterization.Rows(_Row)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 1
                                            .Node.Data = "Saturation"
                                            .Node.Image = ImageList1.Images(7)
                                        End With
                                        With C1CV_Catheterization
                                            .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                            .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString)
                                        End With
                                    End If
                                    For mSaturation = 0 To dtSaturation.Rows.Count - 1
                                        strcombine = ""
                                        If Not IsNothing(dtSaturation.Rows(mSaturation)("sIVC")) Then
                                            If (dtSaturation.Rows(mSaturation)("sIVC").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "IVC" + " " + ":" + " " + dtSaturation.Rows(mSaturation)("sIVC") + " " + "%"
                                                End If
                                            End If
                                        End If
                                        If Not IsNothing(dtSaturation.Rows(mSaturation)("sSVC")) Then
                                            If (dtSaturation.Rows(mSaturation)("sSVC").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "SVC" + " " + ":" + " " + dtSaturation.Rows(mSaturation)("sSVC") + " " + "%"
                                                Else
                                                    strcombine = strcombine + "    " + "SVC" + " " + ":" + " " + dtSaturation.Rows(mSaturation)("sSVC") + " " + "%"
                                                End If
                                            End If
                                        End If
                                        If Not IsNothing(dtSaturation.Rows(mSaturation)("sRASaturations")) Then
                                            If (dtSaturation.Rows(mSaturation)("sRASaturations").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "RA" + " " + ":" + " " + dtSaturation.Rows(mSaturation)("sRASaturations") + " " + "%"
                                                Else
                                                    strcombine = strcombine + "    " + "RA" + " " + ":" + " " + dtSaturation.Rows(mSaturation)("sRASaturations") + " " + "%"
                                                End If

                                            End If
                                        End If
                                        If Not IsNothing(dtSaturation.Rows(mSaturation)("sRVSaturations")) Then
                                            If (dtSaturation.Rows(mSaturation).Item("sRVSaturations").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "RV" + " " + ":" + " " + dtSaturation.Rows(mSaturation)("sRVSaturations") + " " + "%"
                                                Else
                                                    strcombine = strcombine + "    " + "RV" + " " + ":" + " " + dtSaturation.Rows(mSaturation)("sRVSaturations") + " " + "%"
                                                End If

                                            End If
                                        End If
                                        If Not IsNothing(dtSaturation.Rows(mSaturation)("sPASaturations")) Then
                                            If (dtSaturation.Rows(mSaturation)("sPASaturations").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "PA" + " " + ":" + " " + dtSaturation.Rows(mSaturation)("sPASaturations") + " " + "%"
                                                Else
                                                    strcombine = strcombine + "    " + "PA" + " " + ":" + " " + dtSaturation.Rows(mSaturation)("sPASaturations") + " " + "%"
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
                                                .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString)
                                            End With
                                        End If
                                    Next  'For mSaturation  = 0 To .Rows.Count - 1
                                End If  'dtPressure
                            End With


                            '' Query for selecting LV ''
                            strLVQry = "SELECT DISTINCT isnull(sLVEjectionFraction,'') as sLVEjectionFraction,isnull(sLVDiastolicVol,'') as sLVDiastolicVol,isnull(sLVSystolicVol,'') as sLVSystolicVol,isnull(nPatientID,0) as nPatientID,isnull(nVisitID,0) as nVisitID,dtProcedureDate as ProcedureDate from CV_Catheterization where nPatientID=" & mPatientID & " AND dtProcedureDate='" & dtProcedureDate & "' AND nCatheterizationID=" & CatheterizationID
                            oDB.Connect(GetConnectionString)
                            dtLV = oDB.ReadQueryDataTable(strLVQry)
                            oDB.Disconnect()

                            With dtLV
                                If IsNothing(dtLV) = False Then
                                    If dtLV.Rows.Count > 0 Then
                                        C1CV_Catheterization.Rows.Add()
                                        _Row = C1CV_Catheterization.Rows.Count - 1

                                        With C1CV_Catheterization.Rows(_Row)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 1
                                            .Node.Data = "Left Ventricular"
                                            .Node.Image = ImageList1.Images(10)
                                        End With
                                        With C1CV_Catheterization
                                            .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                            .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString)
                                        End With
                                    End If
                                    For mLV = 0 To dtLV.Rows.Count - 1
                                        strcombine = ""
                                        If Not IsNothing(dtLV.Rows(mLV)("sLVEjectionFraction")) Then
                                            If (dtLV.Rows(mLV)("sLVEjectionFraction").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "Ejection Fraction" + " " + ":" + " " + dtLV.Rows(mLV)("sLVEjectionFraction") + " " + "%"
                                                End If
                                            End If
                                        End If
                                        If Not IsNothing(dtLV.Rows(mLV)("sLVDiastolicVol")) Then
                                            If (dtLV.Rows(mLV)("sLVDiastolicVol").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "Diastolic Volume" + " " + ":" + " " + dtLV.Rows(mLV)("sLVDiastolicVol") + " " + "ml"
                                                Else
                                                    strcombine = strcombine + "    " + "Diastolic Volume" + " " + ":" + " " + dtLV.Rows(mLV)("sLVDiastolicVol") + " " + "ml"
                                                End If

                                            End If
                                        End If
                                        If Not IsNothing(dtLV.Rows(mLV)("sLVSystolicVol")) Then
                                            If (dtLV.Rows(mLV)("sLVSystolicVol").ToString() <> "") Then
                                                If (strcombine = "") Then
                                                    strcombine = "Systolic Volume" + " " + ":" + " " + dtLV.Rows(mLV)("sLVSystolicVol") + " " + "ml"
                                                Else
                                                    strcombine = strcombine + "    " + "Systolic Volume" + " " + ":" + " " + dtLV.Rows(mLV)("sLVSystolicVol") + " " + "ml"
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
                                                .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString)
                                            End With
                                        End If
                                    Next  'For mLV  = 0 To .Rows.Count - 1
                                End If  'dtLV
                            End With


                            '' '' Query for selecting RV ''
                            ''strRVQry = "SELECT DISTINCT sRVEjectionFraction,sRVDiastolicVol,sRVSystolicVol from CV_Catheterization where nPatientID=" & mPatientID & " AND dtProcedureDate='" & dtProcedureDate & "' AND nCatheterizationID=" & CatheterizationID
                            ''oDB.Connect(GetConnectionString)
                            ''dtRV = oDB.ReadQueryDataTable(strRVQry)
                            ''oDB.Disconnect()


                            ''With dtRV
                            ''    If IsNothing(dtRV) = False Then
                            ''        If dtLV.Rows.Count > 0 Then
                            ''            C1CV_Catheterization.Rows.Add()
                            ''            _Row = C1CV_Catheterization.Rows.Count - 1
                            ''            With C1CV_Catheterization.Rows(_Row)
                            ''                .AllowEditing = False
                            ''                .ImageAndText = True
                            ''                .Height = 24
                            ''                .IsNode = True
                            ''                .Node.Level = 1
                            ''                .Node.Data = "Right Ventricular"
                            ''            End With
                            ''With C1CV_Catheterization
                            ''    .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                            ''    .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                            ''    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString)
                            ''End With
                            ''        End If
                            ''        For mRV = 0 To dtRV.Rows.Count - 1
                            ''            strcombine = ""
                            ''            If Not IsNothing(dtRV.Rows(mRV)("sRVEjectionFraction")) Then
                            ''                If (dtRV.Rows(mRV)("sRVEjectionFraction").ToString() <> "") Then
                            ''                    If (strcombine = "") Then
                            ''                        strcombine = "Ejection Fraction" + " " + ":" + " " + dtRV.Rows(mRV)("sRVEjectionFraction")
                            ''                    End If
                            ''                End If
                            ''            End If
                            ''            If Not IsNothing(dtRV.Rows(mRV)("sRVDiastolicVol")) Then
                            ''                If (dtRV.Rows(mRV)("sRVDiastolicVol").ToString() <> "") Then
                            ''                    If (strcombine = "") Then
                            ''                        strcombine = "Diastolic Volume" + " " + ":" + " " + dtRV.Rows(mRV)("sRVDiastolicVol")
                            ''                    Else
                            ''                        strcombine = strcombine + "    " + "Diastolic Volume" + " " + ":" + " " + dtRV.Rows(mRV)("sRVDiastolicVol")
                            ''                    End If

                            ''                End If
                            ''            End If
                            ''            If Not IsNothing(dtRV.Rows(mRV)("sRVSystolicVol")) Then
                            ''                If (dtRV.Rows(mRV)("sRVSystolicVol").ToString() <> "") Then
                            ''                    If (strcombine = "") Then
                            ''                        strcombine = "Systolic Volume" + " " + ":" + " " + dtRV.Rows(mRV)("sRVSystolicVol")
                            ''                    Else
                            ''                        strcombine = strcombine + "    " + "Systolic Volume" + " " + ":" + " " + dtRV.Rows(mRV)("sRVSystolicVol")
                            ''                    End If

                            ''                End If
                            ''            End If

                            ''            If strcombine.Trim <> "" Then
                            ''                C1CV_Catheterization.Rows.Add()
                            ''                _Row = C1CV_Catheterization.Rows.Count - 1
                            ''                With C1CV_Catheterization.Rows(_Row)
                            ''                    .AllowEditing = True
                            ''                    .ImageAndText = True
                            ''                    .Height = 24
                            ''                    .IsNode = True
                            ''                    .Node.Level = 2
                            ''                    .Node.Data = strcombine
                            ''                    '.Node.Image=ImageList1.Images(3)
                            ''                End With
                            ''With C1CV_Catheterization
                            ''    .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                            ''    .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                            ''    .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString)
                            ''End With
                            ''            End If
                            ''        Next  'For mRV  = 0 To .Rows.Count - 1
                            ''    End If  'dtRV
                            ''End With



                            '' Query for selecting Narrative Summary ''
                            strNarrativesummaryQry = "SELECT DISTINCT ISNULL(sNarrativeSummary,'') as NarrativeSummary,isnull(nPatientID,0) as nPatientID,isnull(nVisitID,0) as nVisitID,dtProcedureDate as ProcedureDate from CV_Catheterization where nPatientID=" & mPatientID & " AND dtProcedureDate='" & dtProcedureDate & "' AND nCatheterizationID=" & CatheterizationID
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
                                            .Node.Data = "Narrative Summary"
                                            .Node.Image = ImageList1.Images(8)
                                        End With
                                        With C1CV_Catheterization
                                            .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                            .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                            .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString)
                                        End With
                                    End If
                                    For mNarrativesummary = 0 To dtNarrativeSummary.Rows.Count - 1
                                        Dim strNarrativesummary As String = dtNarrativeSummary.Rows(mNarrativesummary)("NarrativeSummary")
                                        If strNarrativesummary.Trim <> "" Then
                                            C1CV_Catheterization.Rows.Add()
                                            _Row = C1CV_Catheterization.Rows.Count - 1


                                            With C1CV_Catheterization.Rows(_Row)
                                                .AllowEditing = True
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 2
                                                .Node.Data = dtNarrativeSummary.Rows(mNarrativesummary)("NarrativeSummary")
                                                .Node.Image = ImageList1.Images(3)
                                            End With
                                            With C1CV_Catheterization
                                                .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(mDOS)("nPatientID"))
                                                .SetData(_Row, COL_VISITID, dtProcDate.Rows(mDOS)("nVisitID"))
                                                .SetData(_Row, COL_DateofStudyInvisible, Convert.ToDateTime(dtProcDate.Rows(mDOS)("ProcedureDate")).ToShortDateString)
                                            End With
                                        End If
                                    Next  'For mNarrativesummary  = 0 To .Rows.Count - 1
                                End If  'dtNarrativeSummary
                            End With

                        End If
                    Next 'For mDOS = 0 To .Rows.Count - 1
                End If
            End With 'dtProcDate


        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ''swaraj - 28-04-2010 - To fill catheterization information''
    End Sub



    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Try
            Select Case e.ClickedItem.Tag

                Case "Add"

                    ''start :: Catheterization
                    Dim _isCurrentDate As Boolean = False
                    Dim selectedrow As Integer = 0
                    If C1CV_Catheterization.Row > 0 Then
                        selectedrow = C1CV_Catheterization.Row
                        Dim myDate As Date = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_DateofStudyInvisible)
                        If myDate = Date.Now.Date Then  '' If there is data against the todays date then open for the modify
                            If C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_PATIENTID) > 0 And C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_VISITID) > 0 Then
                                mPatientID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_PATIENTID)
                                mCatheterizationID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_CATHETERIZATIONID)
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
                    ''End :: Catheterization
                    Dim ofrm As New frmCV_Catheterization(mPatientID, mDateofProc, mVisitID)
                    If Not IsNothing(ofrm) Then
                        If _isCurrentDate = True Then
                            ofrm.blnIsNew = True
                            _isCurrentDate = False
                        End If
                        ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                        SetGridSytle()
                        FillCatheterization()
                        ofrm.Dispose()
                        ofrm = Nothing
                    End If
                    If C1CV_Catheterization.Rows.Count > 1 Then ''if condition added for  bugid 90798
                        If (selectedrow > 0) And (selectedrow < C1CV_Catheterization.Rows.Count) Then
                            C1CV_Catheterization.Select(selectedrow, 0)
                        Else
                            C1CV_Catheterization.Select(1, 0)
                        End If
                    Else
                        If (C1CV_Catheterization.Rows.Count = 1) Then ''added for bugid 92181
                            C1CV_Catheterization.Select(0, 0)
                        End If
                    End If
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.View, "Record viewed for Cardio Vascular Catheterization", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20100916
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.View, "Record viewed for Cardio Vascular Catheterization", mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    ''

                Case "Modify"
                    Dim selectedrow As Integer = 0
                    If C1CV_Catheterization.Row > 0 Then
                        selectedrow = C1CV_Catheterization.Row
                        If C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_PATIENTID) > 0 And C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_VISITID) > 0 Then
                            mPatientID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_PATIENTID)
                            mCatheterizationID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_CATHETERIZATIONID)
                            mExamID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_EXAMID)
                            mVisitID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_VISITID)
                            mClinicID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_CLINICID)

                            mDateofProc = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_DateofStudyInvisible)
                            SetGridSytle()
                            FillCatheterization()
                            Dim ofrm As New frmCV_Catheterization(mPatientID, mDateofProc, mVisitID)
                            ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                            SetGridSytle()
                            FillCatheterization()
                            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Modify, "Modified Records in Cardio Vascular Catheterization", gloAuditTrail.ActivityOutCome.Success)
                            ''Added Rahul P on 20100916
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Modify, "Modified Records in Cardio Vascular Catheterization", mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                            ''
                            ofrm.Dispose()
                            ofrm = Nothing
                        End If
                        If C1CV_Catheterization.Rows.Count > 1 Then  ''if condition added for  bugid 90798
                            If (selectedrow > 0) And (selectedrow < C1CV_Catheterization.Rows.Count) Then
                                C1CV_Catheterization.Select(selectedrow, 0)
                            Else
                                C1CV_Catheterization.Select(1, 0)
                            End If
                        Else
                            If (C1CV_Catheterization.Rows.Count = 1) Then ''added for bugid 92181
                                C1CV_Catheterization.Select(0, 0)
                            End If
                            End If
                    End If

                Case "Delete"
                        ''swaraj - 04-28-2010 - To delete the catheterization record''
                        If C1CV_Catheterization.Rows.Count > 1 And C1CV_Catheterization.Row > 0 Then
                            mPatientID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_PATIENTID)
                            mCatheterizationID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_CATHETERIZATIONID)
                            mExamID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_EXAMID)
                            mVisitID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_VISITID)
                            mClinicID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_CLINICID)
                            mDateofProc = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_DateofStudyInvisible)

                            If MessageBox.Show(" Are you sure you want to delete the Catheterization Record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                                Dim objCVCatheterizationDbLayer As New ClsCVCatheterizationDbLayer
                                objCVCatheterizationDbLayer.DeleteCatheterization(mPatientID, mVisitID, mDateofProc)
                                txtsearch.Text = ""
                                SetGridSytle()
                                FillCatheterization()
                            End If
                        End If
                        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Delete, "CV CVCatheterization deleted.", gloAuditTrail.ActivityOutCome.Success)
                        ''Added Rahul P on 20100916
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Delete, "CV CVCatheterization deleted.", mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        ''


                Case "Refresh"
                        ''swaraj - 04-28-2010 - To refresh the catheterization record''
                        SetGridSytle()
                        FillCatheterization()
                        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Add, "CVCatheterization data refreshed. ", gloAuditTrail.ActivityOutCome.Success)
                        ''Added Rahul P on 20100916
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Refresh, "CVCatheterization data refreshed. ", mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        ''


                Case "Close"
                        ''swaraj - 04-28-2010 - To close the catheterization form''
                        Me.Close()
                        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Add, "CVCatheterization form is closed ", gloAuditTrail.ActivityOutCome.Success)
                        ''Added Rahul P on 20100916
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Close, "CVCatheterization form is closed ", mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                        ''


            End Select
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

                If strSearch.Trim <> "" And strSearch.Trim.Length = 1 Then
                    ''''''''''''
                    Dim objComm As New Cls_CardioVasculars
                    objComm.ExpandAll(C1CV_Catheterization)
                    objComm = Nothing
                    ''''''''''''
                End If


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

    Private Sub C1CV_Catheterization_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1CV_Catheterization.MouseDoubleClick
        Try
            If C1CV_Catheterization.Row > 0 Then
                If C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_PATIENTID) > 0 And C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_VISITID) > 0 Then
                    mPatientID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_PATIENTID)
                    mVisitID = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_VISITID)
                    ''mDateofProc = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_PROCEDUREDATE)
                    mDateofProc = C1CV_Catheterization.GetData(C1CV_Catheterization.Row, COL_DateofStudyInvisible)

                    Dim ofrm As New frmCV_Catheterization(mPatientID, mDateofProc, mVisitID)
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    SetGridSytle()
                    FillCatheterization()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Modify, "Modified Records in Cardio Vascular Catheterization", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20100916
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Modify, "Modified Records in Cardio Vascular Catheterization", mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
                    ''
                    ofrm.Dispose()
                    ofrm = Nothing
                End If
            End If

        Catch ex As Exception
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ''Added Rahul P on 20100916
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.General, ex.ToString(), mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Failure)
            ''
        End Try

    End Sub

    Private Sub C1CV_Catheterization_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1CV_Catheterization.MouseMove
        If C1CV_Catheterization.Row > 0 Then
            gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
        End If
    End Sub

    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return mPatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property
End Class