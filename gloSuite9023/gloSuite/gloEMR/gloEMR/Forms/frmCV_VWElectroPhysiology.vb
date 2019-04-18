Imports System.IO
Imports Microsoft.Win32
Imports C1.Win.C1FlexGrid
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SqlClient
Imports System.Data.Common



Public Class frmCV_VWElectroPhysiology
    Implements IPatientContext

    Dim mPatientID As Long = 0
    Dim mElectroPhysioID As Long = 0
    Dim mVisitID As Long = 0
    Dim mExamID As Long = 0
    Dim mClinicID As Long = 0
    Dim mDateofProc As Date
    Dim mProcedures As String = ""


    Private COL_ELECPHYSIOLOGYID As Integer = 0
    Private COL_PATIENTID As Integer = 1
    Private COL_EXAMID As Integer = 2
    Private COL_VISITID As Integer = 3
    Private COL_CLINICID As Integer = 4
    Private COL_PROCEDUREDATE As Integer = 5
    Private COL_CPTCODETESTTYPE As Integer = 6
    Private COL_PROCEDUREPERFORMED As Integer = 7
    Private COL_USERPROVIDERID As Integer = 8
    'Private COL_BtnUSERPROVIDERID As Integer = 9
    Dim COL_DATEOFPROCINVISIBLE As Integer = 9


    Private COL_COUNT As Integer = 10

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
        With C1CV_ElectroPhysio
            'Dim i As Int16
            .Dock = DockStyle.Fill
            'Dim _TotalWidth As Single = 0
            '_TotalWidth = (.Width - 20) / 4

            .Cols.Count = COL_COUNT '' COLUMN_COUNT
            .Rows.Fixed = 1
            .Rows.Count = 1
            .AllowEditing = False
            .AllowAddNew = False

            .Styles.ClearUnused()

            .Cols(COL_ELECPHYSIOLOGYID).Width = .Width * 0
            .Cols(COL_ELECPHYSIOLOGYID).AllowEditing = False
            .SetData(0, COL_ELECPHYSIOLOGYID, "ElectPhysioID")
            .Cols(COL_ELECPHYSIOLOGYID).TextAlignFixed = TextAlignEnum.CenterCenter

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

            .Cols(COL_PROCEDUREDATE).Width = .Width * 1.5 '_TotalWidth * 3
            .SetData(0, COL_PROCEDUREDATE, "ElectroPhysiology")
            .Cols(COL_PROCEDUREDATE).DataType = GetType(String)
            .Cols(COL_PROCEDUREDATE).AllowEditing = False

            .Cols(COL_CPTCODETESTTYPE).Width = .Width * 0 '_TotalWidth * 0
            .SetData(0, COL_CPTCODETESTTYPE, "CPT Code Type")
            .Cols(COL_CPTCODETESTTYPE).AllowEditing = False

            .Cols(COL_PROCEDUREPERFORMED).Width = .Width * 0 ' _TotalWidth * 0
            .SetData(0, COL_PROCEDUREPERFORMED, "Procedure Performed")
            .Cols(COL_PROCEDUREPERFORMED).AllowEditing = False

            .Cols(COL_USERPROVIDERID).Width = .Width * 0 ' _TotalWidth * 0
            .SetData(0, COL_USERPROVIDERID, "User")
            .Cols(COL_USERPROVIDERID).AllowEditing = False

            .Cols(COL_DATEOFPROCINVISIBLE).Width = 0
            .SetData(0, COL_DATEOFPROCINVISIBLE, "Date")
            .Cols(COL_DATEOFPROCINVISIBLE).DataType = GetType(String)
            .Cols(COL_DATEOFPROCINVISIBLE).AllowEditing = False


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

    Private Sub FillElectroPhysioTest()
        Try

            Dim _Row As Integer
            'Dim i As Integer
            'set properties of treeview in flexgrid
            With C1CV_ElectroPhysio
                .Tree.Column = COL_PROCEDUREDATE
                .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                .Tree.LineStyle = Drawing2D.DashStyle.Solid

                .Tree.Indent = 15
            End With

            Dim dtProcDate As DataTable
            Dim dtCPT As DataTable
            Dim dtProc As DataTable
            Dim dtUsers As DataTable

            Dim ICD9Node As myTreeNode
            Dim CPTNode As myTreeNode
            Dim MODNode As myTreeNode

            Dim nDOS As Int16
            Dim nCPT As Int16
            Dim nProc As Int16
            Dim nUser As Int16


            Dim strselecrICD9Qry As String
            Dim strselectCPTQry As String
            Dim strselectProcQry As String
            Dim strselectMODQry As String
            Dim strconcatCPT1 As String = ""
            Dim nextICD As Integer



            strselecrICD9Qry = "SELECT Distinct nPatientID,nExamID,nVisitID,nClinicID,dtProcedureDate as ProcedureDate FROM CV_ElectroPhysiology WHERE  nPatientID = " & mPatientID
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)
            dtProcDate = oDB.ReadQueryDataTable(strselecrICD9Qry)
            oDB.Disconnect()

            With dtProcDate
                If IsNothing(dtProcDate) = False Then
                    For nDOS = 0 To .Rows.Count - 1
                        Dim ElectroPhysiologyID As Int64 = 0
                        Dim PatientID As Int64 = 0
                        Dim VisitID As Int64 = 0
                        Dim ExamID As Int64 = 0
                        Dim ClinicID As Int64 = 0
                        Dim DateofProc As Date


                        Dim count As Integer = nDOS + 1
                        If CStr(dtProcDate.Rows(nDOS)("ProcedureDate")).Trim <> "" Then
                            C1CV_ElectroPhysio.Rows.Add()
                            _Row = C1CV_ElectroPhysio.Rows.Count - 1
                            'set the properties for newly added row
                            With C1CV_ElectroPhysio.Rows(_Row)
                                .AllowEditing = False
                                .ImageAndText = True
                                .Height = 24
                                .IsNode = True
                                .Node.Level = 0
                                'sarika bug 1613
                                '.Node.Data = dtProcDate.Rows(nDOS)("ProcedureDate") '' 02/20/2009
                                .Node.Data = Convert.ToDateTime(dtProcDate.Rows(nDOS)("ProcedureDate")).ToShortDateString  '' 02/20/2009
                                '--
                                .Node.Image = ImageList1.Images(0) 'Procedure Date Icon from imagelist 'Global.gloEMR.My.Resources.Resources.ICD_09
                            End With
                            nextICD = _Row
                            With C1CV_ElectroPhysio
                                '.SetData(_Row, COL_DateofStudy, _Row)
                                '.SetData(_Row, COL_ELECPHYSIOLOGYID, dtProcDate.Rows(nDOS)("nElectroPhysiologyID"))
                                .SetData(_Row, COL_PATIENTID, dtProcDate.Rows(nDOS)("nPatientID"))
                                .SetData(_Row, COL_EXAMID, dtProcDate.Rows(nDOS)("nExamID"))
                                .SetData(_Row, COL_VISITID, dtProcDate.Rows(nDOS)("nVisitID"))
                                .SetData(_Row, COL_CLINICID, dtProcDate.Rows(nDOS)("nClinicID"))
                                'sarika bug 1613

                                '.SetData(_Row, COL_DATEOFPROCINVISIBLE, dtProcDate.Rows(nDOS)("ProcedureDate"))
                                .SetData(_Row, COL_DATEOFPROCINVISIBLE, Convert.ToDateTime(dtProcDate.Rows(nDOS)("ProcedureDate")).ToShortDateString())
                                '---

                                ' ElectroPhysiologyID = dtProcDate.Rows(nDOS)("nElectroPhysiologyID")
                                PatientID = dtProcDate.Rows(nDOS)("nPatientID")
                                ExamID = dtProcDate.Rows(nDOS)("nExamID")
                                VisitID = dtProcDate.Rows(nDOS)("nVisitID")
                                ClinicID = dtProcDate.Rows(nDOS)("nClinicID")
                                'sarika bug 1613
                                'DateofProc = dtProcDate.Rows(nDOS)("ProcedureDate")
                                DateofProc = Convert.ToDateTime(dtProcDate.Rows(nDOS)("ProcedureDate")).ToShortDateString()
                                '---
                            End With


                            Dim dtProcedureDate As Date = dtProcDate.Rows(nDOS)("ProcedureDate")


                            'Query for selecting CPT for current exam
                            strselectCPTQry = "SELECT DISTINCT isnull(sCPTCode,'') as sCPTcode FROM CV_ElectroPhysiology WHERE  nPatientID = " & PatientID & " and dtProcedureDate = '" & dtProcedureDate & "'"
                            oDB.Connect(GetConnectionString)
                            dtCPT = oDB.ReadQueryDataTable(strselectCPTQry)
                            oDB.Disconnect()

                            'dtCPT = oclsDiagnosis.FetchExamICD9CPT(ExamID, .Rows(nDOS)("sCPTcode"))

                            With dtCPT
                                If IsNothing(dtCPT) = False Then
                                    If dtCPT.Rows.Count > 0 Then
                                        C1CV_ElectroPhysio.Rows.Add()
                                        _Row = C1CV_ElectroPhysio.Rows.Count - 1
                                        With C1CV_ElectroPhysio.Rows(_Row)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 1
                                            .Node.Data = "CPT" '' "CPT"
                                            .Node.Image = Global.gloEMR.My.Resources.Resources.CPT1
                                            C1CV_ElectroPhysio.SetData(_Row, COL_PATIENTID, PatientID)
                                            C1CV_ElectroPhysio.SetData(_Row, COL_EXAMID, ExamID)
                                            C1CV_ElectroPhysio.SetData(_Row, COL_VISITID, VisitID)
                                            C1CV_ElectroPhysio.SetData(_Row, COL_CLINICID, ClinicID)
                                            C1CV_ElectroPhysio.SetData(_Row, COL_DATEOFPROCINVISIBLE, DateofProc.ToShortDateString)
                                        End With
                                    End If
                                    For nCPT = 0 To .Rows.Count - 1
                                        Dim strCurrentCPT As String = dtCPT.Rows(nCPT)("sCPTcode")
                                        If strCurrentCPT.Trim <> "" Then
                                            C1CV_ElectroPhysio.Rows.Add()
                                            _Row = C1CV_ElectroPhysio.Rows.Count - 1
                                            'set the properties for newly added row
                                            With C1CV_ElectroPhysio.Rows(_Row)
                                                .AllowEditing = True
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 2
                                                .Node.Data = dtCPT.Rows(nCPT)("sCPTcode")
                                                .Node.Image = ImageList1.Images(3) 'arrow Icon from imagelist 'Global.gloEMR.My.Resources.Resources.CPT1
                                            End With
                                            If _Row = 2 Then
                                                strconcatCPT1 = Convert.ToString(count) + "|" + Convert.ToString(count) + "CPT"
                                            Else
                                                strconcatCPT1 = Convert.ToString(nextICD) + "|" + Convert.ToString(nextICD) + "CPT"
                                            End If



                                            With C1CV_ElectroPhysio
                                                ' .SetData(_Row, COL_ELECPHYSIOLOGYID, ElectroPhysiologyID)
                                                .SetData(_Row, COL_PATIENTID, PatientID)
                                                .SetData(_Row, COL_EXAMID, ExamID)
                                                .SetData(_Row, COL_VISITID, VisitID)
                                                .SetData(_Row, COL_CLINICID, ClinicID)
                                                .SetData(_Row, COL_DATEOFPROCINVISIBLE, DateofProc.ToShortDateString)
                                            End With


                                        End If


                                    Next '' For nCPT = 0 To .Rows.Count - 1
                                End If
                            End With '' With dtCPT


                            '*************************************************************
                            'Query for selecting CPT for current exam
                            strselectProcQry = "SELECT DISTINCT isnull(sProcedures,'') as sProcedures FROM CV_ElectroPhysiology WHERE  nPatientID = " & PatientID & " and dtProcedureDate = '" & dtProcedureDate & "'"
                            oDB.Connect(GetConnectionString)
                            dtProc = oDB.ReadQueryDataTable(strselectProcQry)
                            oDB.Disconnect()

                            'dtCPT = oclsDiagnosis.FetchExamICD9CPT(ExamID, .Rows(nDOS)("sCPTcode"))

                            With dtProc
                                If IsNothing(dtProc) = False Then
                                    If dtProc.Rows.Count > 0 Then
                                        C1CV_ElectroPhysio.Rows.Add()
                                        _Row = C1CV_ElectroPhysio.Rows.Count - 1
                                        With C1CV_ElectroPhysio.Rows(_Row)
                                            .AllowEditing = False
                                            .ImageAndText = True
                                            .Height = 24
                                            .IsNode = True
                                            .Node.Level = 1
                                            .Node.Data = "Procedures" '' "CPT"
                                            .Node.Image = ImageList1.Images(1) 'Procedures Icon from imagelist' Global.gloEMR.My.Resources.Resources.cpt
                                            C1CV_ElectroPhysio.SetData(_Row, COL_PATIENTID, PatientID)
                                            C1CV_ElectroPhysio.SetData(_Row, COL_EXAMID, ExamID)
                                            C1CV_ElectroPhysio.SetData(_Row, COL_VISITID, VisitID)
                                            C1CV_ElectroPhysio.SetData(_Row, COL_CLINICID, ClinicID)
                                            C1CV_ElectroPhysio.SetData(_Row, COL_DATEOFPROCINVISIBLE, DateofProc.ToShortDateString)
                                            'sarika bug 1613
                                            'Dim procedure As String = dtProcDate.Rows(nDOS)("ProcedureDate")
                                            Dim procedure As String = Convert.ToDateTime(dtProcDate.Rows(nDOS)("ProcedureDate")).ToShortDateString
                                            '---
                                        End With
                                    End If
                                    For nProc = 0 To .Rows.Count - 1
                                        Dim strCurrentCPT As String = dtProc.Rows(nProc)("sProcedures")
                                        If strCurrentCPT.Trim <> "" Then
                                            C1CV_ElectroPhysio.Rows.Add()
                                            _Row = C1CV_ElectroPhysio.Rows.Count - 1
                                            'set the properties for newly added row
                                            With C1CV_ElectroPhysio.Rows(_Row)
                                                .AllowEditing = True
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 2
                                                .Node.Data = dtProc.Rows(nProc)("sProcedures")
                                                .Node.Image = ImageList1.Images(3) 'arrow Icon from imagelist'Global.gloEMR.My.Resources.Resources.CPT1
                                            End With
                                            If _Row = 2 Then
                                                strconcatCPT1 = Convert.ToString(count) + "|" + Convert.ToString(count) + "Procedures"
                                            Else
                                                strconcatCPT1 = Convert.ToString(nextICD) + "|" + Convert.ToString(nextICD) + "Procedures"
                                            End If



                                            With C1CV_ElectroPhysio
                                                ' .SetData(_Row, COL_ELECPHYSIOLOGYID, ElectroPhysiologyID)
                                                .SetData(_Row, COL_PATIENTID, PatientID)
                                                .SetData(_Row, COL_EXAMID, ExamID)
                                                .SetData(_Row, COL_VISITID, VisitID)
                                                .SetData(_Row, COL_CLINICID, ClinicID)
                                                .SetData(_Row, COL_DATEOFPROCINVISIBLE, DateofProc.ToShortDateString)
                                                .SetData(_Row, COL_PROCEDUREPERFORMED, dtProc.Rows(nProc)("sProcedures"))
                                            End With


                                        End If


                                    Next '' For nCPT = 0 To .Rows.Count - 1
                                End If
                            End With '' With dtCPT

                            '*************************************************************


                            'Query for selecting Modifier for current exam 
                            strselectMODQry = "SELECT Distinct isnull(sUserProvider,'') as UserName from CV_ElectroPhysiology WHERE  nPatientID = " & PatientID & " and dtProcedureDate = '" & dtProcedureDate & "'"

                            oDB.Connect(GetConnectionString)
                            dtUsers = oDB.ReadQueryDataTable(strselectMODQry)
                            oDB.Disconnect()

                            With dtUsers

                                If IsNothing(dtUsers) = False Then
                                    If dtUsers.Rows.Count > 0 Then

                                        Dim strUsers As String = dtUsers.Rows(0)("UserName")

                                        Dim arrUsers() As String = Split(strUsers, "|")
                                        If arrUsers.Length > 0 Then


                                            C1CV_ElectroPhysio.Rows.Add()
                                            _Row = C1CV_ElectroPhysio.Rows.Count - 1
                                            With C1CV_ElectroPhysio.Rows(_Row)
                                                .AllowEditing = False
                                                .ImageAndText = True
                                                .Height = 24
                                                .IsNode = True
                                                .Node.Level = 1
                                                .Node.Data = "Users" '' "CPT"
                                                .Node.Image = ImageList1.Images(2) 'Users Icon from imagelist 'Global.gloEMR.My.Resources.Resources.p
                                                C1CV_ElectroPhysio.SetData(_Row, COL_PATIENTID, PatientID)
                                                C1CV_ElectroPhysio.SetData(_Row, COL_EXAMID, ExamID)
                                                C1CV_ElectroPhysio.SetData(_Row, COL_VISITID, VisitID)
                                                C1CV_ElectroPhysio.SetData(_Row, COL_CLINICID, ClinicID)
                                                C1CV_ElectroPhysio.SetData(_Row, COL_DATEOFPROCINVISIBLE, DateofProc.ToShortDateString)
                                            End With



                                            For nUser = 0 To arrUsers.Length - 1

                                                If arrUsers(nUser) <> "" Then


                                                    C1CV_ElectroPhysio.Rows.Add()
                                                    _Row = C1CV_ElectroPhysio.Rows.Count - 1
                                                    'set the properties for newly added row
                                                    With C1CV_ElectroPhysio.Rows(_Row)
                                                        .AllowEditing = False
                                                        .ImageAndText = True
                                                        .Height = 24
                                                        .IsNode = True
                                                        .Node.Level = 2
                                                        .Node.Data = arrUsers(nUser)
                                                        .Node.Image = ImageList1.Images(3) 'arrow Icon from imagelist'Global.gloEMR.My.Resources.Resources.Modify1
                                                    End With
                                                    'Dim concatMOD As String = strconcatCPT1 + "|" + "MOD"

                                                    With C1CV_ElectroPhysio
                                                        .SetData(_Row, COL_ELECPHYSIOLOGYID, ElectroPhysiologyID)
                                                        .SetData(_Row, COL_PATIENTID, PatientID)
                                                        .SetData(_Row, COL_EXAMID, ExamID)
                                                        .SetData(_Row, COL_VISITID, VisitID)
                                                        .SetData(_Row, COL_CLINICID, ClinicID)
                                                        .SetData(_Row, COL_DATEOFPROCINVISIBLE, DateofProc.ToShortDateString)
                                                    End With

                                                End If


                                            Next

                                        End If
                                    End If
                                End If
                            End With '' With dtUsers
                        End If  '' If CStr(dtStudyDate.Rows(nDOS)("sICD9Code")).Trim <> "" Then
                    Next ''For nDOS = 0 To .Rows.Count - 1
                End If  '' If IsNothing(dtStudyDate) = False Then
            End With '' With dtStudyDate


            dtProcDate = Nothing
            dtCPT = Nothing
            dtUsers = Nothing

            ICD9Node = Nothing
            CPTNode = Nothing
            MODNode = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmCV_VWElectroPhysiology_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmCV_VWElectroPhysiology_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(C1CV_ElectroPhysio)

        Try
            MenuItem_AddDevice.Visible = False
            MenuItem_ViewDevice.Visible = False

            SetGridStyle()
            FillElectroPhysioTest()
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, mPatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub C1CV_ElectroPhysio_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles C1CV_ElectroPhysio.DoubleClick
        Try
            'Dim rowid As Int32 = C1CV_ElectroPhysio.RowSel
            If C1CV_ElectroPhysio.RowSel >= 0 Then
                mPatientID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_PATIENTID)
                mElectroPhysioID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_ELECPHYSIOLOGYID)
                mExamID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_EXAMID)
                mVisitID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_VISITID)
                mClinicID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_CLINICID)
                'lOAD stress details

                mDateofProc = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_DATEOFPROCINVISIBLE)
                'lOAD stress details

                'Verify the form to show
                Dim ofrm As New frmCV_Electrophysiology(mPatientID, mDateofProc, mVisitID)
                ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                SetGridStyle()
                FillElectroPhysioTest() '' SUDHIR 20090715 ''
                'If C1CV_ElectroPhysio.Rows(rowid).Node.Level = 2 Then
                '    Dim ParentNode As Node = C1CV_ElectroPhysio.Rows(rowid).Node.GetNode(NodeTypeEnum.Parent)
                '    If ParentNode.Data = "Procedures" Then
                '        Dim oCardioDevice As New frmCardiologyDevice(mPatientID, mExamID, mVisitID)
                '        oCardioDevice.ShowDialog(Me)

                '    End If
                'End If
                ofrm.Dispose()
                ofrm = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Try
            Select Case e.ClickedItem.Tag

                Case "Add"
                    mPatientID = mPatientID
                    mExamID = 0
                    'mVisitID = GenerateVisitID(Now.Date, mPatientID)

                    'Check for visit aganist current date,if visit not available pass 0
                    mVisitID = GetVisitID(Now.Date, mPatientID)
                    Dim ofrmElectro As New frmCV_Electrophysiology(mPatientID, Now.Date, mVisitID)
                    ofrmElectro.ShowDialog(IIf(IsNothing(ofrmElectro.Parent), Me, ofrmElectro.Parent))
                    SetGridStyle()
                    FillElectroPhysioTest()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, "Electrophysiology added. ", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101011
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, "Electrophysiology added. ", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, "Electrophysiology added. ", gloAuditTrail.ActivityOutCome.Success)
                    ofrmElectro.Dispose()
                    ofrmElectro = Nothing
                Case "Modify"
                    If C1CV_ElectroPhysio.Row > 0 Then

                        mPatientID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_PATIENTID)
                        mElectroPhysioID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_ELECPHYSIOLOGYID)
                        mExamID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_EXAMID)
                        mVisitID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_VISITID)
                        mClinicID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_CLINICID)

                        mDateofProc = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_DATEOFPROCINVISIBLE)
                        Dim ofrmElectro As New frmCV_Electrophysiology(mPatientID, mDateofProc, mVisitID)
                        ofrmElectro.ShowDialog(IIf(IsNothing(ofrmElectro.Parent), Me, ofrmElectro.Parent))
                        SetGridStyle()
                        FillElectroPhysioTest()
                        ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, "Electrophysiology modified.  . ", gloAuditTrail.ActivityOutCome.Success)
                        ''Added Rahul P on 20101011
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, "Electrophysiology modified. ", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        ''
                        ofrmElectro.Dispose()
                        ofrmElectro = Nothing
                    End If
                Case "Delete"
                    If C1CV_ElectroPhysio.Row > 0 Then
                        mPatientID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_PATIENTID)
                        mElectroPhysioID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_ELECPHYSIOLOGYID)
                        mExamID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_EXAMID)
                        mVisitID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_VISITID)
                        mClinicID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_CLINICID)

                        mDateofProc = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_DATEOFPROCINVISIBLE)

                        'sarika 20090515
                        If MessageBox.Show("Are you sure you want to delete the ElectroPhysiology record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                            '---
                            Dim objElectroPhysioDBLayer As New clsElectroPhysioDBLayer
                            objElectroPhysioDBLayer.DeleteElectroPhysioTest(mPatientID, mVisitID, mDateofProc)
                            txtsearch.Text = ""
                            SetGridStyle()
                            FillElectroPhysioTest()
                        End If
                        '----
                    End If
                Case "Refresh"
                    SetGridStyle()
                    FillElectroPhysioTest()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, "Electrophysiology added. ", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101011
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, "Electrophysiology added. ", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
                Case "Close"
                    Me.Close()
                    ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, "Electrophysiology added. ", gloAuditTrail.ActivityOutCome.Success)
                    ''Added Rahul P on 20101011
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, "Electrophysiology added. ", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    ''
            End Select
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Success)

            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
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

            With C1CV_ElectroPhysio

                If strSearch.Trim <> "" And strSearch.Trim.Length = 1 Then
                    ''''''''''''
                    Dim objComm As New Cls_CardioVasculars
                    objComm.ExpandAll(C1CV_ElectroPhysio)
                    objComm = Nothing
                    ''''''''''''
                End If

                .Row = .FindRow(strSearch, 1, COL_PROCEDUREDATE, False, False, True)
                If .Row > 0 Then
                    Exit Sub
                End If

                '' 20070921 - Mahesh - InString Search 
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

    Private Sub C1CV_ElectroPhysio_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1CV_ElectroPhysio.MouseDown
        Try

            If e.Button = Windows.Forms.MouseButtons.Right Then
                'Try
                '    If (IsNothing(C1CV_ElectroPhysio.ContextMenuStrip) = False) Then
                '        C1CV_ElectroPhysio.ContextMenuStrip.Dispose()
                '        C1CV_ElectroPhysio.ContextMenuStrip = Nothing
                '    End If
                'Catch ex As Exception

                'End Try
                C1CV_ElectroPhysio.ContextMenuStrip = Nothing

                Dim r As Integer = C1CV_ElectroPhysio.HitTest(e.X, e.Y).Row
                If r > 0 Then
                    C1CV_ElectroPhysio.Select(r, True)
                    If C1CV_ElectroPhysio.Rows(r).Node.Level = 2 Then
                        'Try
                        '    If (IsNothing(C1CV_ElectroPhysio.ContextMenuStrip) = False) Then
                        '        C1CV_ElectroPhysio.ContextMenuStrip.Dispose()
                        '        C1CV_ElectroPhysio.ContextMenuStrip = Nothing
                        '    End If
                        'Catch ex As Exception

                        'End Try
                        C1CV_ElectroPhysio.ContextMenuStrip = ConMenuADDView
                        Dim ParentNode As Node = C1CV_ElectroPhysio.Rows(r).Node.GetNode(NodeTypeEnum.Parent)
                        If ParentNode.Data = "Procedures" Then
                            MenuItem_AddDevice.Visible = True
                            MenuItem_ViewDevice.Visible = True
                        Else
                            MenuItem_AddDevice.Visible = False
                            MenuItem_ViewDevice.Visible = False
                        End If
                    End If
                Else
                    'Try
                    '    If (IsNothing(C1CV_ElectroPhysio.ContextMenuStrip) = False) Then
                    '        C1CV_ElectroPhysio.ContextMenuStrip.Dispose()
                    '        C1CV_ElectroPhysio.ContextMenuStrip = Nothing
                    '    End If
                    'Catch ex As Exception

                    'End Try
                    C1CV_ElectroPhysio.ContextMenuStrip = Nothing
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Sub

    Private Sub MenuItem_AddDevice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItem_AddDevice.Click
        Try
            Dim rowid As Int32 = C1CV_ElectroPhysio.RowSel

            mPatientID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_PATIENTID)
            mElectroPhysioID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_ELECPHYSIOLOGYID)
            mExamID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_EXAMID)
            mVisitID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_VISITID)
            mClinicID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_CLINICID)
            mDateofProc = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_DATEOFPROCINVISIBLE)
            mProcedures = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_PROCEDUREPERFORMED)

            Dim Addflag As String = "ADD"
            Dim ofrm As New frmCV_ImplantDevice(mPatientID, mExamID, mVisitID, mDateofProc, mClinicID, mProcedures, Addflag)

            ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))

            ' '' ''Dim Addflag As String = "ADD"

            ' '' ''Dim ofrm As New frmCardiologyDevice(mPatientID, mExamID, mVisitID, mClinicID, mDateofProc, mProcedures, Addflag)
            ' '' ''ofrm.ShowDialog(Me)
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, "cv device added. ", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.Add, "cv device added. ", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            ofrm.Dispose()
            ofrm = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub MenuItem_ViewDevice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MenuItem_ViewDevice.Click
        Try
            Dim rowid As Int32 = C1CV_ElectroPhysio.RowSel

            mPatientID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_PATIENTID)
            mElectroPhysioID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_ELECPHYSIOLOGYID)
            mExamID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_EXAMID)
            mVisitID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_VISITID)
            mClinicID = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_CLINICID)
            mDateofProc = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_DATEOFPROCINVISIBLE)
            mProcedures = C1CV_ElectroPhysio.GetData(C1CV_ElectroPhysio.Row, COL_PROCEDUREPERFORMED)

            Dim Viewflag As String = "VIEW"
            Dim ofrm As New frmCV_ImplantDevice(mPatientID, mExamID, mVisitID, mDateofProc, mClinicID, mProcedures, Viewflag)

            ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))

            '' '' ''Dim Viewflag As String = "VIEW"

            '' '' ''Dim ofrm As New frmCardiologyDevice(mPatientID, mExamID, mVisitID, mClinicID, mDateofProc, mProcedures, Viewflag)
            '' '' ''ofrm.ShowDialog(Me)
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.View, "View . ", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.View, "View . ", mPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            ofrm.Dispose()
            ofrm = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub C1CV_ElectroPhysio_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1CV_ElectroPhysio.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'SHUBHANGI 20091006
        'Use to clear search text box
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
