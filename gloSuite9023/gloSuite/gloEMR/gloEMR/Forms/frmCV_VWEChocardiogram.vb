Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports System.IO
Imports Microsoft.Win32
Imports C1.Win.C1FlexGrid
Imports System.Data.SqlClient
Imports System.Data.Common
Public Class frmCV_VWEChocardiogram
    Implements IPatientContext


    '  Private COL_PROCEDUREDATE As Integer = 5
    Private COL_EChocardiogramID As Integer = 0
    Private COL_PATIENTID As Integer = 1
    Private COL_EXAMID As Integer = 2
    Private COL_VISITID As Integer = 3


    Private COL_PROCEDUREDATE As Integer = 4
    Private Col_Name As Integer = 5
    Private COL_COUNT As Integer = 6
    ' Private COL_INTERVENTIONTYPE As Integer = 7
    ' Private COL_PHYSICIANNAME As Integer = 8
    'Private COL_CPTCODETESTTYPE As Integer = 6


    Public mPatientID As Long
    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Add"
                ''Start :: EChocardiogram
                Dim _isCurrentDate As Boolean = False
                If C1CV_Echocardio.Rows.Count > 0 Then
                    If C1CV_Echocardio.RowSel > 0 Then
                        Dim _myDate As Date = C1CV_Echocardio.GetData(C1CV_Echocardio.RowSel, COL_PROCEDUREDATE)
                        If _myDate = Date.Now.Date Then '' If there is data against the todays date then open for the modify
                            modifyrecord()
                            fillgriddata()
                        Else
                            _isCurrentDate = True

                        End If
                    Else
                        _isCurrentDate = True
                    End If
                End If
                If _isCurrentDate = True Then
                    _isCurrentDate = False
                    Dim ofrm As New frmCV_Echocardiogram(mPatientID, 0, Date.Now)
                    ofrm.nTransaction = 1
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    fillgriddata()
                    ofrm.Dispose()
                    ofrm = Nothing
                End If
                ''End :: EChocardiogram
            Case "Modify"

                modifyrecord()

            Case "Delete"
                deleterecord()

            Case "Close"
                Me.Close()
        End Select
    End Sub

    Private Sub modifyrecord()
        Dim rono As Integer = C1CV_Echocardio.RowSel()
        'Dim str As Date = C1CV_Echocardio.GetData(sel, Col_Name).ToString()
        Try

            Dim _patdate As Date = C1CV_Echocardio.GetData(rono, COL_PROCEDUREDATE)
            Dim nPatID As Int64 = Convert.ToInt64(C1CV_Echocardio.GetData(rono, COL_PATIENTID))
            Dim nvsitID As Int64 = Convert.ToInt64(C1CV_Echocardio.GetData(rono, COL_VISITID))

            If (nPatID <> 0 AndAlso nvsitID <> 0) Then
                Dim objechocardiogram As New frmCV_Echocardiogram(nPatID, nvsitID, _patdate)
                objechocardiogram.nTransaction = 2
                objechocardiogram.ShowDialog(IIf(IsNothing(objechocardiogram.Parent), Me, objechocardiogram.Parent))
                objechocardiogram.Dispose()
                objechocardiogram = Nothing
                fillgriddata()
            End If
        Catch ex As Exception

        End Try
    End Sub


    Private Sub deleterecord()
        Dim rono As Integer = C1CV_Echocardio.RowSel()
        'Dim str As Date = C1CV_Echocardio.GetData(sel, Col_Name).ToString()
        Try

            Dim _patdate As Date = C1CV_Echocardio.GetData(rono, COL_PROCEDUREDATE)
            Dim nPatID As Int64 = Convert.ToInt64(C1CV_Echocardio.GetData(rono, COL_PATIENTID))
            Dim nvsitID As Int64 = Convert.ToInt64(C1CV_Echocardio.GetData(rono, COL_VISITID))

            If (nPatID <> 0 AndAlso nvsitID <> 0) Then

                If MessageBox.Show("Are you sure you want to delete the Record?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    Dim oDB As New DataBaseLayer
                    Dim _strSQL As String = ""

                    Try
                        _strSQL = "delete FROM CV_Echocardiogram WHERE  nPatientID = " & nPatID & " and nVisitID= " & nvsitID & " and  convert(varchar,dtproceduredate,101)= '" & Format(_patdate, "MM/dd/yyyy") & "'"
                        Dim b As Boolean = oDB.Delete_Query(_strSQL)
                        C1CV_Echocardio.Clear()
                        txtsearch.Text = ""
                        SetGridSytle()
                        FillElectroPhysioTest()
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Delete, "Echocardiogram deleted", nPatID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    Catch ex As Exception

                    End Try

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmCV_VWEChocardiogram_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmCV_VWEChocardiogram_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Close, "Echocardiogram closed", GetCurrentPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
    End Sub
    Private Sub frmCV_VWEChocardiogram_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            C1CV_Echocardio.Clear()
            SetGridSytle()
            FillElectroPhysioTest()
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.View, "Echocardiogram view", GetCurrentPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Try
                gloPatient.gloPatient.GetWindowTitle(Me, mPatientID, GetConnectionString(), gstrMessageBoxCaption)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.ElectroCardioGram, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub fillgriddata()
        C1CV_Echocardio.Clear()
        SetGridSytle()
        FillElectroPhysioTest()
    End Sub

    Private Sub FillElectroPhysioTest()
        Try

            Dim _Row As Integer
            'Dim i As Integer
            'set properties of treeview in flexgrid
            With C1CV_Echocardio
                .Tree.Column = Col_Name
                .Tree.Style = C1.Win.C1FlexGrid.TreeStyleFlags.Simple
                .Tree.LineStyle = Drawing2D.DashStyle.Solid

                .Tree.Indent = 6
            End With

            Dim dtProcDate As DataTable
            Dim dtCPT As DataTable
            ' Dim dtProc As DataTable
            Dim dtUsers As DataTable

            Dim ICD9Node As myTreeNode
            Dim CPTNode As myTreeNode
            Dim MODNode As myTreeNode

            Dim nDOS As Int16
            Dim nCPT As Int16
            'Dim nProc As Int16
            'Dim nUser As Int16


            'Dim strselecrICD9Qry As String
            'Dim strselectCPTQry As String
            'Dim strselectProcQry As String
            'Dim strselectMODQry As String
            Dim strconcatCPT1 As String = ""
            ' Dim nextICD As Integer

            '''''''''''''''''''
            ''Dim objrtf As New RichTextBox
            ''Dim i As Integer
            ''Dim temp As Integer
            ''i = objrtf.SelectionFont
            ''objrtf.SelectionFont= = i / 3 * 2
            ''objrtf.SelectionCharOffset = temp * 5
            ''Dim str As String
            ''Dim str1 As String
            ''str.Format(str1, objrtf)

            '''''''''''''''''

            Dim _tempdtprocdata As DataTable = Nothing

            Dim oDB As New DataBaseLayer
            Dim _strSQL As String = ""
            Dim dvdata As DataView = Nothing

            Dim _tempdata As DataTable = Nothing


            Try
                '  _strSQL = "SELECT nEchocardiogramID,sAortic,sLAArea,sLVDiastvol,sLvedd,sLVesd,sLVMass,sLVsystvol,sLVpostwallthick,sSeptalthick,sMitral,smvarea,sAVArea,sIDofintrepertingphys,sCPTCode,sNarrativeSummary,sProcedures,nPatientId,nvisitId,nExamId,convert(varchar,dtproceduredate,101) as dtproceduredate   FROM CV_Echocardiogram WHERE  nPatientID = " & gnPatientID & " and nVisitID= " & gnVisitID & " order by dtproceduredate " 'and  convert(varchar,dtproceduredate,101)=convert(varchar,dbo.gloGetDate(),101)"

                _strSQL = "select isnull(nEchocardiogramID,0)as nEchocardiogramID,isnull(sAortic,'')as sAortic,isnull(sLAArea,'')as 'sLAArea',isnull(sLVDiastvol,'')as sLVDiastvol,isnull(sLvedd,'')as sLvedd,isnull(sLVesd,'')as sLVesd,isnull(sLVMass,'')as sLVMass,isnull(sLVsystvol,'')as sLVsystvol,isnull(sLVpostwallthick,'')as sLVpostwallthick, isnull(sSeptalthick,'')as sSeptalthick,isnull(sMitral,'')as sMitral,isnull(smvarea,'')as smvarea,isnull(sAVArea,'')as sAVArea,isnull(sIDofintrepertingphys,'')as sIDofintrepertingphys,isnull(sCPTCode,'')as sCPTCode,isnull(sNarrativeSummary,'')as sNarrativeSummary,isnull(sProcedures,'')as sProcedures,isnull(nPatientId,0)as nPatientId,isnull(nvisitId,0)as nvisitId,isnull(nExamId,0)as nExamId,isnull(convert(varchar,dtproceduredate,101),'') as dtproceduredate   FROM CV_Echocardiogram WHERE  nPatientID = " & mPatientID & " order by dtproceduredate "
                dtProcDate = oDB.GetDataTable_Query(_strSQL)

                _tempdtprocdata = dtProcDate
                dvdata = dtProcDate.DefaultView

                _tempdata = dvdata.ToTable(True, "dtproceduredate")


            Catch ex As Exception

            Finally
                oDB = Nothing
            End Try

            For Each drc As DataRow In _tempdata.Rows

                Dim drrecord As DataRow() = _tempdtprocdata.Select("dtproceduredate='" & drc(0).ToString() & "'")

                C1CV_Echocardio.Rows.Add()
                _Row = C1CV_Echocardio.Rows.Count - 1
                ''set the properties for newly added row
                With C1CV_Echocardio.Rows(_Row)
                    .AllowEditing = False
                    .ImageAndText = True
                    .Height = 24
                    .IsNode = True
                    .Node.Level = 0
                    .Node.Data = Convert.ToDateTime(drrecord(nDOS)("dtproceduredate")).ToShortDateString  '' 02/20/2009

                    .Node.Image = ImageList1.Images(1) 'Procedure Date Icon from imagelist 'Global.gloEMR.My.Resources.Resources.ICD_09
                End With


                With C1CV_Echocardio

                    .SetData(_Row, COL_EChocardiogramID, drrecord(nDOS)("nEchocardiogramID").ToString())
                    .SetData(_Row, COL_PATIENTID, drrecord(nDOS)("nPatientID").ToString())
                    .SetData(_Row, COL_EXAMID, drrecord(nDOS)("nExamID").ToString())
                    .SetData(_Row, COL_VISITID, drrecord(nDOS)("nVisitID").ToString())
                    .SetData(_Row, COL_PROCEDUREDATE, drrecord(nDOS)("dtproceduredate"))
                    .SetData(_Row, Col_Name, Convert.ToDateTime(drrecord(nDOS)("dtproceduredate")).ToShortDateString())

                End With


                C1CV_Echocardio.Rows.Add()
                _Row = C1CV_Echocardio.Rows.Count - 1
                With C1CV_Echocardio.Rows(_Row)
                    .AllowEditing = False
                    .ImageAndText = True
                    .Height = 24
                    .IsNode = True
                    .Node.Level = 1
                    ' .Node.Data = "CPT"  '' 02/20/2009
                    .Node.Data = "CPT Code"
                    .Node.Image = ImageList1.Images(17) 'Procedure Date Icon from imagelist 'Global.gloEMR.My.Resources.Resources.ICD_09
                End With


                With C1CV_Echocardio

                    .SetData(_Row, COL_EChocardiogramID, drrecord(0)("nEchocardiogramID").ToString())
                    .SetData(_Row, COL_PATIENTID, drrecord(0)("nPatientID").ToString())
                    .SetData(_Row, COL_EXAMID, drrecord(0)("nExamID").ToString())
                    .SetData(_Row, COL_VISITID, drrecord(0)("nVisitID").ToString())
                    .SetData(_Row, COL_PROCEDUREDATE, drrecord(0)("dtproceduredate"))
                    ' .SetData(_Row, Col_Name, "CPT")
                    .SetData(_Row, Col_Name, "CPT Code")
                End With


                C1CV_Echocardio.Rows.Add()
                _Row = C1CV_Echocardio.Rows.Count - 1

                Dim nextCPT As Integer = _Row

                For nCPT = 0 To drrecord.Length - 1
                    If drrecord(nCPT)("scptcode").ToString() <> "" Then
                        With C1CV_Echocardio.Rows(nextCPT)
                            .AllowEditing = False
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 2

                            .Node.Data = drrecord(nCPT)("scptcode").ToString()
                            .Node.Image = ImageList1.Images(3)
                        End With

                        With C1CV_Echocardio

                            .SetData(nextCPT, COL_EChocardiogramID, drrecord(nCPT)("nEchocardiogramID").ToString())
                            .SetData(nextCPT, COL_PATIENTID, drrecord(nCPT)("nPatientID").ToString())
                            .SetData(nextCPT, COL_EXAMID, drrecord(nCPT)("nExamID").ToString())
                            .SetData(nextCPT, COL_VISITID, drrecord(nCPT)("nVisitID").ToString())
                            .SetData(nextCPT, COL_PROCEDUREDATE, drrecord(nCPT)("dtproceduredate"))
                            .SetData(nextCPT, Col_Name, drrecord(nCPT)("scptcode").ToString())

                        End With

                        C1CV_Echocardio.Rows.Add()
                        nextCPT = C1CV_Echocardio.Rows.Count - 1
                    End If
                Next



                Dim nextPRoc As Integer = nextCPT

                With C1CV_Echocardio.Rows(nextPRoc)
                    .AllowEditing = False
                    .ImageAndText = True
                    .Height = 24
                    .IsNode = True
                    .Node.Level = 1
                    ' .Node.Data = "Procedure"  '' 02/20/2009
                    .Node.Data = "Procedure Name"
                    .Node.Image = ImageList1.Images(0)

                End With

                With C1CV_Echocardio

                    .SetData(nextPRoc, COL_EChocardiogramID, drrecord(0)("nEchocardiogramID").ToString())
                    .SetData(nextPRoc, COL_PATIENTID, drrecord(0)("nPatientID").ToString())
                    .SetData(nextPRoc, COL_EXAMID, drrecord(0)("nExamID").ToString())
                    .SetData(nextPRoc, COL_VISITID, drrecord(0)("nVisitID").ToString())
                    .SetData(nextPRoc, COL_PROCEDUREDATE, drrecord(0)("dtproceduredate"))
                    '.SetData(nextPRoc, Col_Name, "Procedure")
                    .SetData(nextPRoc, Col_Name, "Procedure Name")
                End With


                C1CV_Echocardio.Rows.Add()
                nextPRoc = C1CV_Echocardio.Rows.Count - 1



                For nCPT = 0 To drrecord.Length - 1
                    If drrecord(nCPT)("sprocedures").ToString() <> "" Then
                        With C1CV_Echocardio.Rows(nextPRoc)
                            .AllowEditing = False
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 2

                            .Node.Data = drrecord(nCPT)("sprocedures").ToString()
                            .Node.Image = ImageList1.Images(3)
                            With C1CV_Echocardio
                                .SetData(nextPRoc, COL_EChocardiogramID, drrecord(nCPT)("nEchocardiogramID").ToString())
                                .SetData(nextPRoc, COL_PATIENTID, drrecord(nCPT)("nPatientID").ToString())
                                .SetData(nextPRoc, COL_EXAMID, drrecord(nCPT)("nExamID").ToString())
                                .SetData(nextPRoc, COL_VISITID, drrecord(nCPT)("nVisitID").ToString())
                                .SetData(nextPRoc, COL_PROCEDUREDATE, drrecord(nCPT)("dtproceduredate"))
                                .SetData(nextPRoc, Col_Name, drrecord(nCPT)("sprocedures").ToString())
                            End With

                            C1CV_Echocardio.Rows.Add()
                            nextPRoc = C1CV_Echocardio.Rows.Count - 1

                        End With
                    End If
                Next


                Dim nextUsr As Integer = nextPRoc

                With C1CV_Echocardio.Rows(nextUsr)
                    .AllowEditing = False
                    .ImageAndText = True
                    .Height = 24
                    .IsNode = True
                    .Node.Level = 1
                    '  .Node.Data = "User"  '' 02/20/2009
                    .Node.Data = "Physicians"
                    .Node.Image = ImageList1.Images(2)

                End With


                With C1CV_Echocardio
                    .SetData(nextUsr, COL_EChocardiogramID, drrecord(0)("nEchocardiogramID").ToString())
                    .SetData(nextUsr, COL_PATIENTID, drrecord(0)("nPatientID").ToString())
                    .SetData(nextUsr, COL_EXAMID, drrecord(0)("nExamID").ToString())
                    .SetData(nextUsr, COL_VISITID, drrecord(0)("nVisitID").ToString())
                    .SetData(nextUsr, COL_PROCEDUREDATE, drrecord(0)("dtproceduredate"))
                    ' .SetData(nextUsr, Col_Name, "User")
                    .SetData(nextUsr, Col_Name, "Physicians")
                End With

                C1CV_Echocardio.Rows.Add()
                nextUsr = C1CV_Echocardio.Rows.Count - 1


                For nCPT = 0 To drrecord.Length - 1
                    If drrecord(nCPT)("sIDofintrepertingphys").ToString() <> "" Then
                        With C1CV_Echocardio.Rows(nextUsr)
                            .AllowEditing = False
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 2
                            .Node.Data = drrecord(nCPT)("sIDofintrepertingphys").ToString()
                            .Node.Image = ImageList1.Images(3)
                        End With
                        With C1CV_Echocardio
                            .SetData(nextUsr, COL_EChocardiogramID, drrecord(nCPT)("nEchocardiogramID").ToString())
                            .SetData(nextUsr, COL_PATIENTID, drrecord(nCPT)("nPatientID").ToString())
                            .SetData(nextUsr, COL_EXAMID, drrecord(nCPT)("nExamID").ToString())
                            .SetData(nextUsr, COL_VISITID, drrecord(nCPT)("nVisitID").ToString())
                            .SetData(nextUsr, COL_PROCEDUREDATE, drrecord(nCPT)("dtproceduredate"))
                            .SetData(nextUsr, Col_Name, drrecord(nCPT)("sIDofintrepertingphys").ToString())
                        End With
                        C1CV_Echocardio.Rows.Add()
                        nextUsr = C1CV_Echocardio.Rows.Count - 1
                    End If
                Next



                ''Dim restnodes As Integer = C1CV_Echocardio.Rows.Count - 1
                Dim restnodes As Integer = nextUsr

                ' ''With C1CV_Echocardio.Rows(restnodes)
                ' ''    .AllowEditing = False
                ' ''    .ImageAndText = True
                ' ''    .Height = 24
                ' ''    .IsNode = True
                ' ''    .Node.Level = 1
                ' ''    .Node.Data = "M-Mode"  '' 02/20/2009
                ' ''    .Node.Image = ImageList1.Images(14)
                ' ''End With
                ' ''With C1CV_Echocardio
                ' ''    .SetData(restnodes, COL_EChocardiogramID, drrecord(0)("nEchocardiogramID").ToString())
                ' ''    .SetData(restnodes, COL_PATIENTID, drrecord(0)("nPatientID").ToString())
                ' ''    .SetData(restnodes, COL_EXAMID, drrecord(0)("nExamID").ToString())
                ' ''    .SetData(restnodes, COL_VISITID, drrecord(0)("nVisitID").ToString())
                ' ''    .SetData(restnodes, COL_PROCEDUREDATE, drrecord(0)("dtproceduredate"))
                ' ''    .SetData(restnodes, Col_Name, "M-Mode")
                ' ''End With

                ' ''C1CV_Echocardio.Rows.Add()
                ' ''restnodes = C1CV_Echocardio.Rows.Count - 1


                Dim _strlvd As String = ""
                For nCPT = 0 To 0
                    '  If dtProcDate.Rows(nCPT)("sIDofintrepertingphys").ToString() <> "" Then



                    If drrecord(nCPT)("sLvedd").ToString().Trim() <> "" Then
                        _strlvd = " LVEDD : " & drrecord(nCPT)("sLvedd").ToString() + " " + "mm" + "," '& ","
                    End If

                    If drrecord(nCPT)("sLvesd").ToString().Trim() <> "" Then
                        _strlvd = _strlvd & " LVESD : " & drrecord(nCPT)("sLvesd").ToString() + " " + "mm" + "," '& ","
                    End If

                    If drrecord(nCPT)("sLVpostwallthick").ToString().Trim() <> "" Then
                        _strlvd = _strlvd & " LV Posterior Wall Thickness : " & drrecord(nCPT)("sLVpostwallthick").ToString() + " " + "mm" + "," '& ","
                    End If

                    If drrecord(nCPT)("sSeptalthick").ToString().Trim() <> "" Then
                        _strlvd = _strlvd & "  Septal Thickness : " & drrecord(nCPT)("sSeptalthick").ToString() + " " + "mm" + "," '& ","
                    End If
                    If _strlvd.Length > 0 Then
                        _strlvd = _strlvd.Substring(0, _strlvd.Length - 1)
                    End If
                    If _strlvd <> "" Then

                        With C1CV_Echocardio.Rows(restnodes)
                            .AllowEditing = False
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 1
                            .Node.Data = "M-Mode"  '' 02/20/2009
                            .Node.Image = ImageList1.Images(14)
                        End With
                        With C1CV_Echocardio
                            .SetData(restnodes, COL_EChocardiogramID, drrecord(0)("nEchocardiogramID").ToString())
                            .SetData(restnodes, COL_PATIENTID, drrecord(0)("nPatientID").ToString())
                            .SetData(restnodes, COL_EXAMID, drrecord(0)("nExamID").ToString())
                            .SetData(restnodes, COL_VISITID, drrecord(0)("nVisitID").ToString())
                            .SetData(restnodes, COL_PROCEDUREDATE, drrecord(0)("dtproceduredate"))
                            .SetData(restnodes, Col_Name, "M-Mode")
                        End With

                        C1CV_Echocardio.Rows.Add()
                        restnodes = C1CV_Echocardio.Rows.Count - 1

                        With C1CV_Echocardio.Rows(restnodes)
                            .AllowEditing = False
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 2

                            .Node.Data = _strlvd
                            .Node.Image = ImageList1.Images(3)
                        End With
                    End If




                    With C1CV_Echocardio
                        .SetData(restnodes, COL_EChocardiogramID, drrecord(nCPT)("nEchocardiogramID").ToString())
                        .SetData(restnodes, COL_PATIENTID, drrecord(nCPT)("nPatientID").ToString())
                        .SetData(restnodes, COL_EXAMID, drrecord(nCPT)("nExamID").ToString())
                        .SetData(restnodes, COL_VISITID, drrecord(nCPT)("nVisitID").ToString())
                        .SetData(restnodes, COL_PROCEDUREDATE, drrecord(nCPT)("dtproceduredate"))
                        ' "Lvedd : " & drrecord(nCPT)("sLvedd").ToString() & " Lvesd : " & drrecord(nCPT)("sLvesd").ToString() & ", Lvposterior wall thickness: " & drrecord(nCPT)("sLVpostwallthick").ToString() & ", Septal Thickness: " & drrecord(nCPT)("sSeptalthick").ToString()
                        .SetData(restnodes, Col_Name, _strlvd)
                    End With
                    C1CV_Echocardio.Rows.Add()
                    restnodes = C1CV_Echocardio.Rows.Count - 1
                Next
                nCPT = 0


                If drrecord(nCPT)("sAortic").ToString().Trim() = "" And drrecord(nCPT)("sMitral").ToString().Trim() = "" Then
                    Dim dpp As Integer = 0
                Else
                    With C1CV_Echocardio.Rows(restnodes)
                        .AllowEditing = False
                        .ImageAndText = True
                        .Height = 24
                        .IsNode = True
                        .Node.Level = 1
                        .Node.Data = "Doppler Gradients"
                        .Node.Image = ImageList1.Images(13)
                    End With
                    With C1CV_Echocardio
                        .SetData(restnodes, COL_EChocardiogramID, drrecord(0)("nEchocardiogramID").ToString())
                        .SetData(restnodes, COL_PATIENTID, drrecord(0)("nPatientID").ToString())
                        .SetData(restnodes, COL_EXAMID, drrecord(0)("nExamID").ToString())
                        .SetData(restnodes, COL_VISITID, drrecord(0)("nVisitID").ToString())
                        .SetData(restnodes, COL_PROCEDUREDATE, drrecord(0)("dtproceduredate"))
                        .SetData(restnodes, Col_Name, "Doppler Gradients")
                    End With
                    C1CV_Echocardio.Rows.Add()
                    restnodes = C1CV_Echocardio.Rows.Count - 1





                    '  For nCPT = 0 To 0
                    '  If dtProcDate.Rows(nCPT)("sIDofintrepertingphys").ToString() <> "" Then
                    Dim _straero As String = ""
                    With C1CV_Echocardio.Rows(restnodes)


                        If drrecord(nCPT)("sAortic").ToString().Trim() <> "" Then
                            _straero = _straero & "  Aortic : " & drrecord(nCPT)("sAortic").ToString() + " " + "mmHg" + "," '& ","
                        End If

                        If drrecord(nCPT)("sMitral").ToString().Trim() <> "" Then
                            _straero = _straero & "  Mitral : " & drrecord(nCPT)("sMitral").ToString() & ","
                        End If

                        If _straero.Length > 0 Then
                            _straero = _straero.Substring(0, _straero.Length - 1)
                        End If

                        If _straero <> "" Then
                            .AllowEditing = False
                            .ImageAndText = True
                            .Height = 24
                            .IsNode = True
                            .Node.Level = 2

                            .Node.Data = _straero '"Aortic" & drrecord(nCPT)("sAortic").ToString() & " Mitral: " & drrecord(nCPT)("sMitral").ToString()
                            .Node.Image = ImageList1.Images(3)
                        End If



                    End With
                    With C1CV_Echocardio
                        .SetData(restnodes, COL_EChocardiogramID, drrecord(nCPT)("nEchocardiogramID").ToString())
                        .SetData(restnodes, COL_PATIENTID, drrecord(nCPT)("nPatientID").ToString())
                        .SetData(restnodes, COL_EXAMID, drrecord(nCPT)("nExamID").ToString())
                        .SetData(restnodes, COL_VISITID, drrecord(nCPT)("nVisitID").ToString())
                        .SetData(restnodes, COL_PROCEDUREDATE, drrecord(nCPT)("dtproceduredate"))
                        .SetData(restnodes, Col_Name, _straero)
                    End With
                    C1CV_Echocardio.Rows.Add()
                    restnodes = C1CV_Echocardio.Rows.Count - 1
                End If



                If drrecord(nCPT)("sLAArea").ToString().Trim() <> "" Then

                    With C1CV_Echocardio.Rows(restnodes)
                        .AllowEditing = False
                        .ImageAndText = True
                        .Height = 24
                        .IsNode = True
                        .Node.Level = 1
                        .Node.Data = "LA Area"
                        .Node.Image = ImageList1.Images(6)
                    End With
                    With C1CV_Echocardio
                        .SetData(restnodes, COL_EChocardiogramID, drrecord(0)("nEchocardiogramID").ToString())
                        .SetData(restnodes, COL_PATIENTID, drrecord(0)("nPatientID").ToString())
                        .SetData(restnodes, COL_EXAMID, drrecord(0)("nExamID").ToString())
                        .SetData(restnodes, COL_VISITID, drrecord(0)("nVisitID").ToString())
                        .SetData(restnodes, COL_PROCEDUREDATE, drrecord(0)("dtproceduredate"))
                        .SetData(restnodes, Col_Name, "LA Area")
                    End With
                    C1CV_Echocardio.Rows.Add()
                    restnodes = C1CV_Echocardio.Rows.Count - 1
                    restnodes = setdata(restnodes, drrecord(nCPT)("sLAArea").ToString() + " " + "cm2", drrecord)
                End If


                If drrecord(nCPT)("sAVArea").ToString().Trim() <> "" Then

                    With C1CV_Echocardio.Rows(restnodes)
                        .AllowEditing = False
                        .ImageAndText = True
                        .Height = 24
                        .IsNode = True
                        .Node.Level = 1
                        .Node.Data = "AV Area"
                        .Node.Image = ImageList1.Images(5)
                    End With
                    With C1CV_Echocardio
                        .SetData(restnodes, COL_EChocardiogramID, drrecord(0)("nEchocardiogramID").ToString())
                        .SetData(restnodes, COL_PATIENTID, drrecord(0)("nPatientID").ToString())
                        .SetData(restnodes, COL_EXAMID, drrecord(0)("nExamID").ToString())
                        .SetData(restnodes, COL_VISITID, drrecord(0)("nVisitID").ToString())
                        .SetData(restnodes, COL_PROCEDUREDATE, drrecord(0)("dtproceduredate"))
                        .SetData(restnodes, Col_Name, "AV Area")
                    End With
                    C1CV_Echocardio.Rows.Add()
                    restnodes = C1CV_Echocardio.Rows.Count - 1
                    restnodes = setdata(restnodes, drrecord(nCPT)("sAVArea").ToString() + " " + "cm2", drrecord)
                End If


                If drrecord(nCPT)("sMVArea").ToString().Trim() <> "" Then
                    With C1CV_Echocardio.Rows(restnodes)
                        .AllowEditing = False
                        .ImageAndText = True
                        .Height = 24
                        .IsNode = True
                        .Node.Level = 1
                        .Node.Data = "MV Area"
                        .Node.Image = ImageList1.Images(4)
                    End With
                    With C1CV_Echocardio
                        .SetData(restnodes, COL_EChocardiogramID, drrecord(0)("nEchocardiogramID").ToString())
                        .SetData(restnodes, COL_PATIENTID, drrecord(0)("nPatientID").ToString())
                        .SetData(restnodes, COL_EXAMID, drrecord(0)("nExamID").ToString())
                        .SetData(restnodes, COL_VISITID, drrecord(0)("nVisitID").ToString())
                        .SetData(restnodes, COL_PROCEDUREDATE, drrecord(0)("dtproceduredate"))
                        .SetData(restnodes, Col_Name, "MV Area")
                    End With
                    C1CV_Echocardio.Rows.Add()
                    restnodes = C1CV_Echocardio.Rows.Count - 1
                    restnodes = setdata(restnodes, drrecord(nCPT)("sMVArea").ToString() + " " + "cm2", drrecord)
                End If


                If drrecord(nCPT)("sLVDiastVol").ToString().Trim() <> "" Then

                    With C1CV_Echocardio.Rows(restnodes)
                        .AllowEditing = False
                        .ImageAndText = True
                        .Height = 24
                        .IsNode = True
                        .Node.Level = 1
                        .Node.Data = "LV Diastolic volume"
                        .Node.Image = ImageList1.Images(9)
                    End With
                    With C1CV_Echocardio
                        .SetData(restnodes, COL_EChocardiogramID, drrecord(0)("nEchocardiogramID").ToString())
                        .SetData(restnodes, COL_PATIENTID, drrecord(0)("nPatientID").ToString())
                        .SetData(restnodes, COL_EXAMID, drrecord(0)("nExamID").ToString())
                        .SetData(restnodes, COL_VISITID, drrecord(0)("nVisitID").ToString())
                        .SetData(restnodes, COL_PROCEDUREDATE, drrecord(0)("dtproceduredate"))
                        .SetData(restnodes, Col_Name, "LV Diastolic volume")
                    End With
                    C1CV_Echocardio.Rows.Add()
                    restnodes = C1CV_Echocardio.Rows.Count - 1
                    restnodes = setdata(restnodes, drrecord(nCPT)("sLVDiastVol").ToString(), drrecord)
                End If



                If drrecord(nCPT)("sLVsystVol").ToString().Trim() <> "" Then

                    With C1CV_Echocardio.Rows(restnodes)
                        .AllowEditing = False
                        .ImageAndText = True
                        .Height = 24
                        .IsNode = True
                        .Node.Level = 1
                        .Node.Data = "LV Systolic volume"
                        .Node.Image = ImageList1.Images(10)
                    End With
                    With C1CV_Echocardio
                        .SetData(restnodes, COL_EChocardiogramID, drrecord(0)("nEchocardiogramID").ToString())
                        .SetData(restnodes, COL_PATIENTID, drrecord(0)("nPatientID").ToString())
                        .SetData(restnodes, COL_EXAMID, drrecord(0)("nExamID").ToString())
                        .SetData(restnodes, COL_VISITID, drrecord(0)("nVisitID").ToString())
                        .SetData(restnodes, COL_PROCEDUREDATE, drrecord(0)("dtproceduredate"))
                        .SetData(restnodes, Col_Name, "LV Systolic volume")
                    End With
                    C1CV_Echocardio.Rows.Add()
                    restnodes = C1CV_Echocardio.Rows.Count - 1
                    restnodes = setdata(restnodes, drrecord(nCPT)("sLVsystVol").ToString(), drrecord)
                End If


                If drrecord(nCPT)("sLVMass").ToString().Trim() <> "" Then

                    With C1CV_Echocardio.Rows(restnodes)
                        .AllowEditing = False
                        .ImageAndText = True
                        .Height = 24
                        .IsNode = True
                        .Node.Level = 1
                        .Node.Data = "LV Mass"
                        .Node.Image = ImageList1.Images(11)
                    End With
                    With C1CV_Echocardio
                        .SetData(restnodes, COL_EChocardiogramID, drrecord(0)("nEchocardiogramID").ToString())
                        .SetData(restnodes, COL_PATIENTID, drrecord(0)("nPatientID").ToString())
                        .SetData(restnodes, COL_EXAMID, drrecord(0)("nExamID").ToString())
                        .SetData(restnodes, COL_VISITID, drrecord(0)("nVisitID").ToString())
                        .SetData(restnodes, COL_PROCEDUREDATE, drrecord(0)("dtproceduredate"))
                        .SetData(restnodes, Col_Name, "LV Mass")
                    End With
                    C1CV_Echocardio.Rows.Add()
                    restnodes = C1CV_Echocardio.Rows.Count - 1
                    restnodes = setdata(restnodes, drrecord(nCPT)("sLVMass").ToString() + " " + "g", drrecord)
                End If



                If drrecord(nCPT)("sNarrativeSummary").ToString().Trim() <> "" Then

                    With C1CV_Echocardio.Rows(restnodes)
                        .AllowEditing = False
                        .ImageAndText = True
                        .Height = 24
                        .IsNode = True
                        .Node.Level = 1
                        .Node.Data = "Narrative Summary"
                        .Node.Image = ImageList1.Images(8)
                    End With

                    With C1CV_Echocardio
                        .SetData(restnodes, COL_EChocardiogramID, drrecord(0)("nEchocardiogramID").ToString())
                        .SetData(restnodes, COL_PATIENTID, drrecord(0)("nPatientID").ToString())
                        .SetData(restnodes, COL_EXAMID, drrecord(0)("nExamID").ToString())
                        .SetData(restnodes, COL_VISITID, drrecord(0)("nVisitID").ToString())
                        .SetData(restnodes, COL_PROCEDUREDATE, drrecord(0)("dtproceduredate"))
                        .SetData(restnodes, Col_Name, "Narrative Summary")
                    End With
                    C1CV_Echocardio.Rows.Add()
                    restnodes = C1CV_Echocardio.Rows.Count - 1
                    restnodes = setdata(restnodes, drrecord(nCPT)("sNarrativeSummary").ToString(), drrecord)
                End If
            Next



            ' Next
            ' drrecord = Nothing
            dtProcDate = Nothing
            dtCPT = Nothing
            dtUsers = Nothing

            ICD9Node = Nothing
            CPTNode = Nothing
            MODNode = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Electrophysiology, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            '   MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
    End Sub


    Public Function setdata(ByVal tree_ro_no As Integer, ByVal data As String, ByVal drrecords As DataRow()) As Integer



        With C1CV_Echocardio.Rows(tree_ro_no)
            .AllowEditing = False
            .ImageAndText = True
            .Height = 24
            .IsNode = True
            .Node.Level = 2

            .Node.Data = data
            .Node.Image = ImageList1.Images(3)

        End With



        With C1CV_Echocardio

            .SetData(tree_ro_no, COL_EChocardiogramID, drrecords(0)("nEchocardiogramID").ToString())
            .SetData(tree_ro_no, COL_PATIENTID, drrecords(0)("nPatientID").ToString())
            .SetData(tree_ro_no, COL_EXAMID, drrecords(0)("nExamID").ToString())
            .SetData(tree_ro_no, COL_VISITID, drrecords(0)("nVisitID").ToString())
            .SetData(tree_ro_no, COL_PROCEDUREDATE, drrecords(0)("dtproceduredate"))

            .SetData(tree_ro_no, Col_Name, data)
        End With

        C1CV_Echocardio.Rows.Add()
        tree_ro_no = C1CV_Echocardio.Rows.Count - 1

        drrecords = Nothing
        Return tree_ro_no

    End Function


    Private Sub SetGridSytle()
        ' Dim struser As String
        With C1CV_Echocardio
            '  Dim i As Int16
            .Dock = DockStyle.Fill
            .Cols.Count = COL_COUNT  'column count
            .Rows.Fixed = 1
            .Rows.Count = 1
            .AllowEditing = False
            .AllowAddNew = False

            .Styles.ClearUnused()




            .Cols(COL_EChocardiogramID).Width = .Width * 0
            .Cols(COL_EChocardiogramID).AllowEditing = False
            .SetData(0, COL_EChocardiogramID, "EchocardiogramID")
            .Cols(COL_EChocardiogramID).TextAlignFixed = TextAlignEnum.LeftCenter


            .Cols(COL_PATIENTID).Width = .Width * 0
            .Cols(COL_PATIENTID).AllowEditing = False
            .SetData(0, COL_PATIENTID, "PatientID")
            .Cols(COL_PATIENTID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_EXAMID).Width = .Width * 0
            .Cols(COL_EXAMID).AllowEditing = False
            .SetData(0, COL_EXAMID, "ExamID")
            .Cols(COL_EXAMID).TextAlignFixed = TextAlignEnum.LeftCenter

            .Cols(COL_VISITID).Width = .Width * 0
            .Cols(COL_VISITID).AllowEditing = False
            .SetData(0, COL_VISITID, "VisitID")
            .Cols(COL_VISITID).TextAlignFixed = TextAlignEnum.LeftCenter


            .Cols(COL_PROCEDUREDATE).Width = .Width * 0
            .Cols(COL_PROCEDUREDATE).AllowEditing = False
            .SetData(0, COL_PROCEDUREDATE, "Date")
            .Cols(COL_PROCEDUREDATE).DataType = GetType(String)

            .Cols(Col_Name).Width = .Width * 0.95
            .Cols(Col_Name).AllowEditing = False
            .SetData(0, Col_Name, "Date")
            .Cols(Col_Name).DataType = GetType(String)

            '.Cols(COL_PROCEDUREDATE).TextAlignFixed = TextAlignEnum.CenterCenter



        End With
    End Sub

    Private Sub C1CV_Echocardio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1CV_Echocardio.Click

    End Sub

    Private Sub C1CV_Echocardio_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C1CV_Echocardio.DoubleClick

        Dim rono As Integer = C1CV_Echocardio.RowSel

        Try

            Dim smdt As Date = C1CV_Echocardio.GetData(rono, COL_PROCEDUREDATE)
            Dim nPatID As Int64 = Convert.ToInt64(C1CV_Echocardio.GetData(rono, COL_PATIENTID))
            Dim nvsitID As Int64 = Convert.ToInt64(C1CV_Echocardio.GetData(rono, COL_VISITID))

            '19-Jan-16 Aniket: Resolving Bug #92657 ( Modified): gloEMR: Synopsis: applicatiion gives exception on save and close of Electrochardiogram
            If (nPatID <> 0 AndAlso nvsitID <> 0) Then
                Dim objechocardiogram As New frmCV_Echocardiogram(nPatID, nvsitID, smdt)

                objechocardiogram.nTransaction = 2
                objechocardiogram.ShowDialog(IIf(IsNothing(objechocardiogram.Parent), Me, objechocardiogram.Parent))
                objechocardiogram.Dispose()
                objechocardiogram = Nothing
                fillgriddata()
            End If

        Catch ex As Exception

        Finally

        End Try


    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtsearch.Text = ""
        txtsearch.Focus()
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

            With C1CV_Echocardio

                If strSearch.Trim <> "" And strSearch.Trim.Length = 1 Then
                    ''''''''''''
                    Dim objComm As New Cls_CardioVasculars
                    objComm.ExpandAll(C1CV_Echocardio)
                    objComm = Nothing
                    ''''''''''''
                End If


                .Row = .FindRow(strSearch, 1, Col_Name, False, False, True)
                If .Row > 0 Then
                    Exit Sub
                End If

                ''InString Search 
                Dim strNode As String = ""
                For i As Int16 = 1 To .Rows.Count - 1
                    strNode = ""
                    strNode = UCase(.GetData(i, Col_Name).ToString.Trim)
                    If InStr(strNode, UCase(strSearch.Trim), CompareMethod.Text) > 0 Then
                        .Row = i
                        Exit Sub
                    End If
                Next
                '' ---
            End With


        Catch ex As Exception
            '' MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub New(ByVal PatientID As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        mPatientID = PatientID
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return mPatientID  'Curent patient variable(Local variable) for this module 
        End Get
    End Property
End Class


