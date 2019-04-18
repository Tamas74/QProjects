Imports System.IO
Imports Microsoft.Win32
Imports C1.Win.C1FlexGrid
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.Data.Common



Public Class frmCV_VWInterventions
    Implements IPatientContext
    Dim mPatientID As Long = 0
    Dim mElectroPhysioID As Long = 0
    Dim mVisitID As Long = 0
    Dim mExamID As Long = 0
    Dim mClinicID As Long = 0
    Dim nVisitDate As Date


    Private COL_InterventionID As Integer = 0
    Private COL_PATIENTID As Integer = 1
    Private COL_EXAMID As Integer = 2
    Private COL_VISITID As Integer = 3
    Private COL_CLINICID As Integer = 4
    Private COL_VISITDATE As Integer = 5
    Dim COL_VISITDATEINVISIBLE As Integer = 6
    Private COL_COUNT As Integer = 7

    Public Sub New(ByVal nPatientId As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        mPatientID = nPatientId
        mClinicID = 1
    End Sub

    Private Sub SetGridStyle()
        'Declare a variable
        'Dim cStyle As C1.Win.C1FlexGrid.CellStyle

        'Dim struser As String
        With C1CV_Intervention
            'Dim i As Int16
            .Dock = DockStyle.Fill
            Dim _TotalWidth As Single = 0
            _TotalWidth = (.Width - 20) / 2

            .Cols.Count = COL_COUNT '' COLUMN_COUNT
            .Rows.Fixed = 1
            .Rows.Count = 1
            .AllowEditing = False
            .AllowAddNew = False

            .Styles.ClearUnused()

            .Cols(COL_InterventionID).Width = .Width * 0
            .Cols(COL_InterventionID).AllowEditing = False
            .SetData(0, COL_InterventionID, "InterventionID")
            .Cols(COL_InterventionID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_PATIENTID).Width = .Width * 0
            .Cols(COL_PATIENTID).AllowEditing = False
            .SetData(0, COL_PATIENTID, "PatientID")
            .Cols(COL_PATIENTID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_EXAMID).Width = .Width * 0
            .Cols(COL_EXAMID).AllowEditing = False
            .SetData(0, COL_EXAMID, "ExamID")
            .Cols(COL_EXAMID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_VISITID).Width = .Width * 0
            .Cols(COL_VISITID).AllowEditing = False
            .SetData(0, COL_VISITID, "VisitID")
            .Cols(COL_VISITID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_CLINICID).Width = .Width * 0
            .Cols(COL_CLINICID).AllowEditing = False
            .SetData(0, COL_VISITID, "ClinicID")
            .Cols(COL_CLINICID).TextAlignFixed = TextAlignEnum.CenterCenter

            .Cols(COL_VISITDATE).Width = _TotalWidth * 2
            .SetData(0, COL_VISITDATE, "Visit Date")
            .Cols(COL_VISITDATE).DataType = GetType(String)
            .Cols(COL_VISITDATE).AllowEditing = False


            .Cols(COL_VISITDATEINVISIBLE).Width = _TotalWidth * 0
            .SetData(0, COL_VISITDATEINVISIBLE, "VisitDateInv")
            .Cols(COL_VISITDATEINVISIBLE).AllowEditing = False

            'Dim dt1 As DataTable
            'dt1 = fillusercombo()
            'Dim strUserName As New System.Text.StringBuilder
            'For j As Int32 = 0 To dt1.Rows.Count - 1
            '    If j > 0 Then
            '        strUserName.Append("|")
            '    End If
            '    strUserName.Append(dt1.Rows(j)("sLoginName"))
            'Next





        End With
    End Sub

    Private Sub FillInterventions()
        Try

            Dim _Row As Integer
            'Dim i As Integer
            'set properties of treeview in flexgrid
            With C1CV_Intervention
                .Tree.Column = COL_VISITDATE
                .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                .Tree.LineStyle = Drawing2D.DashStyle.Solid

                .Tree.Indent = 15
            End With

            Dim dtVisitDate As DataTable = Nothing
            Dim dtExam As DataTable = Nothing
            Dim dtIntervention As DataTable = Nothing
            Dim dtUsers As DataTable = Nothing

            Dim ICD9Node As myTreeNode
            Dim CPTNode As myTreeNode
            Dim MODNode As myTreeNode

            Dim nDOS As Int16
            Dim nExam As Int16
            Dim nProc As Int16
            'Dim nUser As Int16


            Dim strselecrICD9Qry As String
            Dim strselectCPTQry As String
            Dim strselectProcQry As String
            'Dim strselectMODQry As String
            Dim strconcatCPT1 As String = ""
            Dim nextICD As Integer


            strselecrICD9Qry = "SELECT Distinct ExamIntervention.nPatientID as nPatientID,ExamIntervention.nVisitID as nVisitID,ExamIntervention.nClinicID as nClinicID ,dtVisitDate as VisitDate FROM ExamIntervention  inner join Visits on ExamIntervention.nVisitID=Visits.nVisitID WHERE  ExamIntervention.nPatientID = " & mPatientID
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)
            dtVisitDate = oDB.ReadQueryDataTable(strselecrICD9Qry)
            oDB.Disconnect()

            With dtVisitDate
                If IsNothing(dtVisitDate) = False Then
                    For nDOS = 0 To .Rows.Count - 1
                        Dim ElectroPhysiologyID As Int64 = 0
                        Dim PatientID As Int64 = 0
                        Dim VisitID As Int64 = 0

                        Dim ClinicID As Int64 = 0
                        Dim VisitDate As Date

                        Dim count As Integer = nDOS + 1
                        If CStr(dtVisitDate.Rows(nDOS)("VisitDate")).Trim <> "" Then
                            C1CV_Intervention.Rows.Add()
                            _Row = C1CV_Intervention.Rows.Count - 1
                            'set the properties for newly added row
                            With C1CV_Intervention.Rows(_Row)
                                .AllowEditing = False
                                .ImageAndText = True
                                .Height = 24
                                .IsNode = True
                                .Node.Level = 0
                                'sarika bug 1613
                                '.Node.Data = dtVisitDate.Rows(nDOS)("VisitDate") '' 02/20/2009
                                .Node.Data = Convert.ToDateTime(dtVisitDate.Rows(nDOS)("VisitDate")).ToShortDateString  '' 02/20/2009
                                '---
                                .Node.Image = ImageList1.Images(0) 'Procedure Date Icon from imagelist 'Global.gloEMR.My.Resources.Resources.ICD_09
                            End With
                            nextICD = _Row
                            With C1CV_Intervention
                                '.SetData(_Row, COL_DateofStudy, _Row)
                                '.SetData(_Row, COL_ELECPHYSIOLOGYID, dtVisitDate.Rows(nDOS)("nElectroPhysiologyID"))
                                .SetData(_Row, COL_PATIENTID, dtVisitDate.Rows(nDOS)("nPatientID"))
                                .SetData(_Row, COL_EXAMID, 0)
                                .SetData(_Row, COL_VISITID, dtVisitDate.Rows(nDOS)("nVisitID"))
                                .SetData(_Row, COL_CLINICID, dtVisitDate.Rows(nDOS)("nClinicID"))
                                .SetData(_Row, COL_VISITDATE, Convert.ToDateTime(dtVisitDate.Rows(nDOS)("VisitDate")).ToShortDateString)

                                ' ElectroPhysiologyID = dtVisitDate.Rows(nDOS)("nElectroPhysiologyID")
                                PatientID = dtVisitDate.Rows(nDOS)("nPatientID")
                                VisitID = dtVisitDate.Rows(nDOS)("nVisitID")
                                ClinicID = dtVisitDate.Rows(nDOS)("nClinicID")
                                VisitDate = Convert.ToDateTime(dtVisitDate.Rows(nDOS)("VisitDate")).ToShortDateString
                            End With


                            VisitDate = Convert.ToDateTime(dtVisitDate.Rows(nDOS)("VisitDate")).ToShortDateString


                            'Query for selecting CPT for current exam
                            strselectCPTQry = "SELECT DISTINCT isnull(PatientExams.sExamName,'') as sExamName,PatientExams.nExamId as ExamID FROM ExamIntervention inner join PatientExams on ExamIntervention.nExamID=PatientExams.nExamID WHERE  ExamIntervention.nPatientID = " & PatientID & " and ExamIntervention.nVisitID = " & VisitID & ""

                            oDB.Connect(GetConnectionString)
                            dtExam = oDB.ReadQueryDataTable(strselectCPTQry)
                            oDB.Disconnect()

                            'dtExam = oclsDiagnosis.FetchExamICD9CPT(ExamID, .Rows(nDOS)("sExamName"))

                            With dtExam
                                If IsNothing(dtExam) = False Then
                                    If dtExam.Rows.Count > 0 Then
                                        C1CV_Intervention.Rows.Add()
                                        _Row = C1CV_Intervention.Rows.Count - 1
                                        With C1CV_Intervention.Rows(_Row)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 1
                                            .Node.Data = "Exam" '' "CPT"
                                            .Node.Image = Global.gloEMR.My.Resources.Resources.cpt
                                        End With
                                    End If
                                    For nExam = 0 To .Rows.Count - 1
                                        Dim strCurrentCPT As String = dtExam.Rows(nExam)("sExamName")
                                        Dim ExamID As Int64
                                        If strCurrentCPT.Trim <> "" Then
                                            C1CV_Intervention.Rows.Add()
                                            _Row = C1CV_Intervention.Rows.Count - 1
                                            'set the properties for newly added row
                                            With C1CV_Intervention.Rows(_Row)
                                                .AllowEditing = False
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 2
                                                .Node.Data = dtExam.Rows(nExam)("sExamName")
                                                .Node.Image = ImageList1.Images(3) 'arrow Icon from imagelist 'Global.gloEMR.My.Resources.Resources.CPT1
                                            End With

                                            With C1CV_Intervention
                                                ' .SetData(_Row, COL_ELECPHYSIOLOGYID, ElectroPhysiologyID)

                                                ExamID = dtExam.Rows(nExam)("ExamID")
                                                .SetData(_Row, COL_PATIENTID, PatientID)
                                                .SetData(_Row, COL_EXAMID, ExamID)
                                                .SetData(_Row, COL_VISITID, VisitID)
                                                .SetData(_Row, COL_CLINICID, ClinicID)
                                                .SetData(_Row, COL_VISITDATEINVISIBLE, VisitDate.ToShortDateString)

                                            End With


                                        End If
                                        '*************************************************************
                                        'Query for selecting CPT for current exam
                                        strselectProcQry = "SELECT DISTINCT isnull(sInterventionDescription,'') as InterventionDesc FROM ExamIntervention WHERE  nPatientID = " & PatientID & " and nVisitID = " & VisitID & " and nExamID=" & ExamID & ""
                                        oDB.Connect(GetConnectionString)
                                        dtIntervention = oDB.ReadQueryDataTable(strselectProcQry)
                                        oDB.Disconnect()

                                        'dtExam = oclsDiagnosis.FetchExamICD9CPT(ExamID, .Rows(nDOS)("sExamName"))

                                        With dtIntervention
                                            If IsNothing(dtIntervention) = False Then
                                                If dtIntervention.Rows.Count > 0 Then
                                                    C1CV_Intervention.Rows.Add()
                                                    _Row = C1CV_Intervention.Rows.Count - 1
                                                    With C1CV_Intervention.Rows(_Row)
                                                        .AllowEditing = False
                                                        .ImageAndText = True
                                                        .Height = 24
                                                        .IsNode = True
                                                        .Node.Level = 3
                                                        .Node.Data = "Intervention Description" '' "CPT"
                                                        .Node.Image = ImageList1.Images(1) 'Procedures Icon from imagelist' Global.gloEMR.My.Resources.Resources.cpt
                                                    End With
                                                End If
                                                For nProc = 0 To .Rows.Count - 1
                                                    Dim strIntervention As String = dtIntervention.Rows(nProc)("InterventionDesc")
                                                    If strIntervention.Trim <> "" Then
                                                        C1CV_Intervention.Rows.Add()
                                                        _Row = C1CV_Intervention.Rows.Count - 1
                                                        'set the properties for newly added row
                                                        With C1CV_Intervention.Rows(_Row)
                                                            .AllowEditing = True
                                                            .ImageAndText = True
                                                            .Height = 24
                                                            .IsNode = True
                                                            .Node.Level = 4
                                                            .Node.Data = dtIntervention.Rows(nProc)("InterventionDesc")
                                                            .Node.Image = ImageList1.Images(3) 'arrow Icon from imagelist'Global.gloEMR.My.Resources.Resources.CPT1
                                                        End With



                                                        With C1CV_Intervention
                                                            ' .SetData(_Row, COL_ELECPHYSIOLOGYID, ElectroPhysiologyID)
                                                            .SetData(_Row, COL_PATIENTID, PatientID)
                                                            .SetData(_Row, COL_EXAMID, ExamID)
                                                            .SetData(_Row, COL_VISITID, VisitID)
                                                            .SetData(_Row, COL_CLINICID, ClinicID)
                                                            .SetData(_Row, COL_VISITDATEINVISIBLE, VisitDate.ToShortDateString)
                                                        End With


                                                    End If


                                                Next '' For nExam = 0 To .Rows.Count - 1
                                            End If
                                        End With '' With dtExam

                                    Next '' For nExam = 0 To .Rows.Count - 1
                                End If
                            End With '' With dtExam




                            '*************************************************************


                        End If  '' If CStr(dtStudyDate.Rows(nDOS)("sICD9Code")).Trim <> "" Then
                    Next ''For nDOS = 0 To .Rows.Count - 1
                End If  '' If IsNothing(dtStudyDate) = False Then
            End With '' With dtStudyDate
            If (IsNothing(dtVisitDate) = False) Then
                dtVisitDate.Dispose()
                dtVisitDate = Nothing
            End If
            If (IsNothing(dtExam) = False) Then
                dtExam.Dispose()
                dtExam = Nothing
            End If
            If (IsNothing(dtUsers) = False) Then
                dtUsers.Dispose()
                dtUsers = Nothing
            End If
            If (IsNothing(dtIntervention) = False) Then
                dtIntervention.Dispose()
                dtIntervention = Nothing
            End If

            ICD9Node = Nothing
            CPTNode = Nothing
            MODNode = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmCV_VWInterventions_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmCV_VWElectroPhysiology_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(C1CV_Intervention)

        Try
            SetGridStyle()
            FillInterventions()
        Catch ex As Exception

        End Try
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, mPatientID, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub C1CV_ElectroPhysio_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1CV_Intervention.DoubleClick
        Try
            ModifyIntervention()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Try
            Select Case e.ClickedItem.Tag

                Case "Add"
                    Dim VisitID As Int64 = GenerateVisitID(mPatientID)
                    Dim ofrmIntervention As New frmExamIntervention(mExamID, VisitID, mPatientID)
                    ofrmIntervention.ShowDialog(IIf(IsNothing(ofrmIntervention.Parent), Me, ofrmIntervention.Parent))
                    SetGridStyle()
                    FillInterventions()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Add, "Record viewed for Exam Intervention.", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101011
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Add, "Record viewed for Exam Intervention.", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.RecordViewed, "Record viewed for Exam Intervention.  ", gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Success, gstrMessageBoxCaption)
                    ofrmIntervention.Dispose()
                    ofrmIntervention = Nothing
                Case "Modify"
                    ModifyIntervention()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, "Record modified for Intervention.  ", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101011
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, "Record modified for Intervention.  ", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Record modified for Intervention.  ", gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Success, gstrMessageBoxCaption)
                Case "Delete"
                    If C1CV_Intervention.Row > 0 Then
                        mPatientID = C1CV_Intervention.GetData(C1CV_Intervention.Row, COL_PATIENTID)
                        mElectroPhysioID = C1CV_Intervention.GetData(C1CV_Intervention.Row, COL_InterventionID)
                        mExamID = C1CV_Intervention.GetData(C1CV_Intervention.Row, COL_EXAMID)
                        mVisitID = C1CV_Intervention.GetData(C1CV_Intervention.Row, COL_VISITID)
                        mClinicID = C1CV_Intervention.GetData(C1CV_Intervention.Row, COL_CLINICID)

                        nVisitDate = C1CV_Intervention.GetData(C1CV_Intervention.Row, COL_VISITDATEINVISIBLE)

                        'sarika 20090515
                        If MessageBox.Show("Are you sure you want to delete the Intervention record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            '---
                            DeleteIntervention()
                            txtsearch.Text = ""
                            'Dim objElectroPhysioDBLayer As New clsElectroPhysioDBLayer
                            'objElectroPhysioDBLayer.DeleteElectroPhysioTest(mPatientID, mVisitID, nVisitDate)
                            SetGridStyle()
                            FillInterventions()
                        End If
                        mExamID = 0
                        mVisitID = 0
                    Else  '' solving TFS id issue- 1863
                        MessageBox.Show(" No Intervention records available for deletion. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '' end
                    End If
                Case "Refresh"
                        SetGridStyle()
                        FillInterventions()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Refresh, "Refreshed Record for Intervention.  ", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101011
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Refresh, "Refreshed Record for Intervention.  ", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Refreshed Record for Intervention.  ", gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Success, gstrMessageBoxCaption)
                Case "Close"
                        Me.Close()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Close, "Closed Intervention.  ", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101011
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Close, "Closed Intervention.  ", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Other, "Closed Intervention.  ", gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Success, gstrMessageBoxCaption)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub

    Private Sub DeleteIntervention()
        Dim conn As New SqlConnection(GetConnectionString)
        Try
            conn.Open()
            Dim strQuery As String = "Delete from ExamIntervention where nPatientID= " & mPatientID & " and nVisitID=" & mVisitID & " and nExamID=" & mExamID & ""
            Dim cmd As New SqlCommand(strQuery, conn)
            If (cmd.ExecuteNonQuery() > 0) Then
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Record deleted.  ", gstrLoginName, gstrClientMachineName, mPatientID, False, gloAuditTrail.enmOutCome.Success, gstrMessageBoxCaption)
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Delete, "CV Intervention deleted.  ", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Delete, "CV Intervention deleted.  ", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            End If
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Record not deleted.  ", gstrLoginName, gstrClientMachineName, mPatientID, False, gloAuditTrail.enmOutCome.Failure, gstrMessageBoxCaption)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Delete, "CV Intervention not deleted.  ", gloAuditTrail.ActivityOutCome.Failure)
            Throw ex
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            conn.Dispose()
            conn = Nothing
        End Try
    End Sub

    Private Sub ModifyIntervention()
        If C1CV_Intervention.Row > 0 Then

            mPatientID = C1CV_Intervention.GetData(C1CV_Intervention.Row, COL_PATIENTID)
            mElectroPhysioID = C1CV_Intervention.GetData(C1CV_Intervention.Row, COL_InterventionID)
            mExamID = C1CV_Intervention.GetData(C1CV_Intervention.Row, COL_EXAMID)
            mVisitID = C1CV_Intervention.GetData(C1CV_Intervention.Row, COL_VISITID)
            mClinicID = C1CV_Intervention.GetData(C1CV_Intervention.Row, COL_CLINICID)

            nVisitDate = C1CV_Intervention.GetData(C1CV_Intervention.Row, COL_VISITDATEINVISIBLE)

            Dim ofrmIntervention As New frmExamIntervention(mExamID, mVisitID, mPatientID)
            ofrmIntervention.ShowDialog(IIf(IsNothing(ofrmIntervention.Parent), Me, ofrmIntervention.Parent))
            SetGridStyle()
            FillInterventions()
            ofrmIntervention.Dispose()
            ofrmIntervention = Nothing
        Else '' solving TFS id issue-1863
            MessageBox.Show("No Intervention records available for modification. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ''end
        End If
    End Sub

    Private Sub txtsearch_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsearch.TextChanged
        Try

            Dim strSearch As String
            With txtsearch
                If Trim(.Text) <> "" Then
                    strSearch = Replace(.Text, "'", "''")
                Else
                    strSearch = ""
                End If
            End With

            With C1CV_Intervention
                ' by Ujwala Atre to Expand All Nodes for Search - as on 20101029
                If strSearch.Trim <> "" And strSearch.Trim.Length = 1 Then
                    ''''''''''''
                    Dim objComm As New Cls_CardioVasculars
                    objComm.ExpandAll(C1CV_Intervention)
                    objComm.Dispose()
                    objComm = Nothing
                    ''''''''''''
                End If
                ' by Ujwala Atre to Expand All Nodes for Search - as on 20101029
                .Row = .FindRow(strSearch, 1, COL_VISITDATE, False, False, True)
                If .Row > 0 Then
                    Exit Sub
                End If

                '' 20070921 - Mahesh - InString Search 
                Dim strNode As String = ""
                For i As Int16 = 1 To .Rows.Count - 1
                    strNode = ""
                    strNode = UCase(.GetData(i, COL_VISITDATE).ToString.Trim)
                    If InStr(strNode, UCase(strSearch.Trim), CompareMethod.Text) > 0 Then
                        .Row = i
                        Exit Sub
                    End If
                Next
                '' ---
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Searched Interventions having substring " & txtsearch.Text.Trim, gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, "Searched Interventions having substring " & txtsearch.Text.Trim, gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, "Searched Interventions having substring " & txtsearch.Text.Trim, mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            End With


        Catch ex As Exception
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Query, "Could not search Interventions having substring " & txtsearch.Text.Trim, gstrLoginName, gstrClientMachineName, mPatientID, True, gloAuditTrail.enmOutCome.Success, "gloEMR")
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, "Could not search Interventions having substring " & txtsearch.Text.Trim, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub C1CV_Intervention_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1CV_Intervention.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'SHUBHANGI 20091006
        'Use to clear Search text box
        txtsearch.ResetText()
        txtsearch.Focus()
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
End Class
