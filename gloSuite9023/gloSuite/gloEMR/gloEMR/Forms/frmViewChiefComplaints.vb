Imports System.Data.SqlClient
Public Class frmViewChiefComplaints
    Implements IPatientContext
    'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
    'Dim _nPatientId As Int64 = gnPatientID
    Dim _nPatientId As Int64
    'end modification by dipak
    Dim _nExamId As Int64 = 0
    Dim _nVisitId As Int64 = 0
    Dim _nComplaintid As Int64 = 0
    Dim _dtvisitdate As DateTime = Now
    Private Const Col_PatientName As Integer = 0
    Private Const Col_ChiefComplaintID As Integer = 1
    Private Const Col_PatientID As Integer = 2
    Private Const Col_VisitID As Integer = 3
    Private Const Col_ExamID As Integer = 4
    Private Const Col_Visitdate As Integer = 5
    Private Const Col_ChiefComplaint As Integer = 6
    Private Const Col_Injurydate As Integer = 7
    Private Const Col_Surgerydate As Integer = 8
    Private WithEvents gloUC_PatientStrip1 As gloUserControlLibrary.gloUC_PatientStrip = Nothing

    Private Sub frmViewChiefComplaints_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            CType(Me.ParentForm, MainMenu).SetGnPatientID = GetCurrentPatientID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Private Sub frmViewChiefComplaints_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Not IsNothing(gloUC_PatientStrip1) Then
            Me.Controls.Remove(gloUC_PatientStrip1)
            gloUC_PatientStrip1.Dispose()
            gloUC_PatientStrip1 = Nothing
        End If
    End Sub

    Private Sub frmViewChiefComplaints_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        gloC1FlexStyle.Style(C1_ChiefComplaints)
        Set_PatientDetailStrip()
        Designgrid()
        'Sanjog - Added on 2011 May 17 for Patient Safety
        Try
            gloPatient.gloPatient.GetWindowTitle(Me, _nPatientId, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
        'Sanjog - Added on 2011 May 17 for Patient Safety
    End Sub

    Private Sub Designgrid()
        ''nChiefComplaintID, nPatientID, nVisitID, nExamID, 
        ''dtVisitDate, sChiefComplaint, dtInjuryDate, dtSurgeryDate, nClinicID

        Try
            Dim dt As DataTable
            Dim _dv As DataView
            Dim objclsPatientInjuryDate As New ClsPatientInjuryDate(_nPatientId)
            dt = objclsPatientInjuryDate.GetAllComplaints()
            objclsPatientInjuryDate.Dispose()
            objclsPatientInjuryDate = Nothing
            _dv = dt.DefaultView
            'dt.Dispose()
            'dt = Nothing
            If dt IsNot Nothing Then
                C1_ChiefComplaints.Redraw = False
                C1_ChiefComplaints.Visible = True
                'C1_ChiefComplaints.Clear()
                C1_ChiefComplaints.DataSource = Nothing
                C1_ChiefComplaints.DataSource = _dv
                C1_ChiefComplaints.Rows.Fixed = 1

                C1_ChiefComplaints.Cols(Col_PatientName).Caption = "Patient"
                C1_ChiefComplaints.Cols(Col_ChiefComplaintID).Caption = "Complaint ID"
                C1_ChiefComplaints.Cols(Col_PatientID).Caption = "Patient ID"
                C1_ChiefComplaints.Cols(Col_VisitID).Caption = "Visit Id"
                C1_ChiefComplaints.Cols(Col_ExamID).Caption = "Exam ID"
                C1_ChiefComplaints.Cols(Col_Visitdate).Caption = "Visit Date"
                C1_ChiefComplaints.Cols(Col_ChiefComplaint).Caption = "Chief Complaint"
                C1_ChiefComplaints.Cols(Col_Injurydate).Caption = "Injury Date"
                C1_ChiefComplaints.Cols(Col_Surgerydate).Caption = "Surgery Date"


                C1_ChiefComplaints.Cols(Col_PatientName).Visible = False
                C1_ChiefComplaints.Cols(Col_ChiefComplaintID).Visible = False
                C1_ChiefComplaints.Cols(Col_PatientID).Visible = False
                C1_ChiefComplaints.Cols(Col_VisitID).Visible = False
                C1_ChiefComplaints.Cols(Col_ExamID).Visible = False
                C1_ChiefComplaints.Cols(Col_Visitdate).Visible = True
                C1_ChiefComplaints.Cols(Col_ChiefComplaint).Visible = True
                C1_ChiefComplaints.Cols(Col_Injurydate).Visible = True
                C1_ChiefComplaints.Cols(Col_Surgerydate).Visible = True

                C1_ChiefComplaints.Cols(Col_PatientName).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1_ChiefComplaints.Cols(Col_ChiefComplaint).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

                Dim nWidth As Integer = Pnl_grid.Width

                C1_ChiefComplaints.Cols(Col_ChiefComplaintID).Width = 0
                C1_ChiefComplaints.Cols(Col_PatientID).Width = 0
                C1_ChiefComplaints.Cols(Col_VisitID).Width = 0
                C1_ChiefComplaints.Cols(Col_ExamID).Width = 0
                C1_ChiefComplaints.Cols(Col_Visitdate).Width = CInt((0.22 * (nWidth)))
                C1_ChiefComplaints.Cols(Col_ChiefComplaint).Width = CInt((0.35 * (nWidth)))
                C1_ChiefComplaints.Cols(Col_Injurydate).Width = CInt((0.22 * (nWidth)))
                C1_ChiefComplaints.Cols(Col_Surgerydate).Width = CInt((0.2 * (nWidth)))

                'C1_ChiefComplaints.Cols.Move(Col_Visitdate, Col_ChiefComplaint)
                C1_ChiefComplaints.ShowCellLabels = True
                C1_ChiefComplaints.AllowEditing = False
                C1_ChiefComplaints.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn
                C1_ChiefComplaints.Redraw = True

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Try

            Select Case e.ClickedItem.Tag
                Case "Add"
                    Try
                        '' SUDHIR 20090709 '' CHECK PATIENT STATUS ''
                        'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                        'If CheckPatientStatus(gnPatientID) = False Then
                        'If CheckPatientStatus(_nPatientId) = False Then
                        '    Exit Sub
                        'End If
                        If MainMenu.IsAccess(False, _nPatientId) = False Then
                            Exit Sub
                        End If
                        'end modification by dipak

                        '' SUDHIR 20090521 '' CHECK PROVIDER ''

                        If gblnProviderDisable = True Then
                            'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                            'If ShowAssociateProvider(gnPatientID) = True Then
                            If ShowAssociateProvider(_nPatientId, Me) = True Then
                                CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
                            End If
                            'end modification by dipak 
                        End If
                        '' END SUDHIR

                        If (C1_ChiefComplaints.Rows.Count > 1) Then
                            '_nPatientId = Convert.ToInt64(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_PatientID))
                            _nExamId = Convert.ToInt64(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_ExamID))
                            Dim ofrm As New frmPatientInjuryDate(_nPatientId, 0, _nExamId, Now, True)
                            ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                            ofrm.Dispose()
                            ofrm = Nothing
                            Designgrid()
                        Else
                            Dim ofrm As New frmPatientInjuryDate(_nPatientId, 0, 0, Now, True)
                            ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                            ofrm.Dispose()
                            ofrm = Nothing
                            Designgrid()
                        End If

                    Catch ex As Exception
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Try
                Case "Modify"
                    Try
                        If (C1_ChiefComplaints.Rows.Count > 1) Then
                            '' SUDHIR 20090709 '' CHECK PATIENT STATUS ''
                            'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                            'If CheckPatientStatus(gnPatientID) = False Then
                            'If CheckPatientStatus(_nPatientId) = False Then
                            '    Exit Sub
                            'End If
                            If MainMenu.IsAccess(False, _nPatientId) = False Then
                                Exit Sub
                            End If
                            'end modification by dipak
                            '' SUDHIR 20090521 '' CHECK PROVIDER ''
                            If gblnProviderDisable = True Then
                                'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                                'If ShowAssociateProvider(gnPatientID) = True Then
                                If ShowAssociateProvider(_nPatientId, Me) = True Then
                                    CType(Me.ParentForm, MainMenu).oPatientListControl.FillPatients()
                                End If
                                'end modification by dipak 
                            End If
                            '' END SUDHIR

                            '_nPatientId = Convert.ToInt64(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_PatientID))
                            _nComplaintid = Convert.ToInt64(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_ChiefComplaintID))
                            _nExamId = Convert.ToInt64(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_ExamID))
                            _dtvisitdate = Convert.ToDateTime(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_Visitdate))
                            Dim ofrm As New frmPatientInjuryDate(_nPatientId, _nComplaintid, _nExamId, _dtvisitdate, False)
                            ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                            ofrm.Dispose()
                            ofrm = Nothing
                            Designgrid()
                        End If
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Try
                Case "Delete"
                    '' SUDHIR 20090709 '' CHECK PATIENT STATUS ''
                    'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
                    'If CheckPatientStatus(gnPatientID) = False Then
                    'If CheckPatientStatus(_nPatientId) = False Then
                    '    Exit Sub
                    'End If
                    If MainMenu.IsAccess(False, _nPatientId) = False Then
                        Exit Sub
                    End If
                    'end modification

                    If (C1_ChiefComplaints.Rows.Count > 1) Then
                        If MessageBox.Show("Are you sure to delete this record?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            _nComplaintid = Convert.ToInt64(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_ChiefComplaintID))
                            DeleteChiefComplaint(_nComplaintid)
                            Designgrid()
                        End If
                    End If
                Case "Refresh"
                    Designgrid()
                Case "Close"
                    Me.Close()
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub DeleteChiefComplaint(ByVal Id As Int64)

        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Try
            objCon.ConnectionString = GetConnectionString()
            objCon.Open()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "DELETE  FROM  PatientChiefComplaint WHERE  nChiefComplaintID = " & Id & ""
            objCmd.Connection = objCon

            If (objCmd.ExecuteNonQuery() > 0) Then
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ChiefComplaints, gloAuditTrail.ActivityType.Add, "chief complaint deleted ", gloAuditTrail.ActivityOutCome.Success)

                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ChiefComplaints, gloAuditTrail.ActivityType.Add, "chief complaint deleted", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101011
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ChiefComplaints, gloAuditTrail.ActivityType.Add, "chief complaint deleted", _nPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            End If

        Catch ex As SqlException

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.ChiefComplaints, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString())
        Finally
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(objCmd) Then

                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
    End Sub

    Private Sub Pnl_grid_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pnl_grid.Resize
        Dim nWidth As Integer = Pnl_grid.Width
        If C1_ChiefComplaints.Cols.Count > 8 Then
            C1_ChiefComplaints.Cols(Col_Visitdate).Width = CInt((0.22 * (nWidth)))
            C1_ChiefComplaints.Cols(Col_ChiefComplaint).Width = CInt((0.35 * (nWidth)))
            C1_ChiefComplaints.Cols(Col_Injurydate).Width = CInt((0.22 * (nWidth)))
            C1_ChiefComplaints.Cols(Col_Surgerydate).Width = CInt((0.2 * (nWidth)))
        End If
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Dim _dv As DataView
        _dv = DirectCast(C1_ChiefComplaints.DataSource, DataView)
        If (IsNothing(_dv) = False) Then


            C1_ChiefComplaints.DataSource = _dv

            Try
                Dim strSearch As String = txtSearch.Text.Trim()

                strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "")


                If strSearch.StartsWith("%") = True Or strSearch.StartsWith("*") = True Then
                    _dv.RowFilter = (_dv.Table.Columns("ChiefComplaint").ColumnName & " Like '%") + strSearch & "%'"
                Else
                    'commented by shubhangi 20091006
                    '  _dv.RowFilter = (_dv.Table.Columns("ChiefComplaint").ColumnName & " Like '") + strSearch & "%'"
                    'SHUBHANGI 20091006 
                    'WE WANR IN STRING SERACH   
                    _dv.RowFilter = (_dv.Table.Columns("ChiefComplaint").ColumnName & " Like '%") + strSearch & "%'"
                End If

                Pnl_grid_Resize(Nothing, Nothing)

            Catch Ex As Exception
            Finally

            End Try
        End If
    End Sub

    Private Sub C1_ChiefComplaints_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1_ChiefComplaints.MouseDoubleClick
        Try
            ''return if double click not on grid row
            Dim hti As C1.Win.C1FlexGrid.HitTestInfo = C1_ChiefComplaints.HitTest(e.X, e.Y)
            If (hti.Column = 1 And hti.Row = -1 Or hti.Row = 0) Then
                C1_ChiefComplaints.Row = -1
            End If

            If (C1_ChiefComplaints.Row = -1) Then
                Return
            End If

            If (C1_ChiefComplaints.Rows.Count > 1) Then
                '_nPatientId = Convert.ToInt64(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_PatientID))
                _nComplaintid = Convert.ToInt64(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_ChiefComplaintID))
                _nExamId = Convert.ToInt64(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_ExamID))
                _dtvisitdate = Convert.ToDateTime(C1_ChiefComplaints.GetData(C1_ChiefComplaints.RowSel, Col_Visitdate))
                Dim ofrm As New frmPatientInjuryDate(_nPatientId, _nComplaintid, _nExamId, _dtvisitdate, False)
                ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                ofrm.Dispose()
                ofrm = Nothing
                Designgrid()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub
    ''To display patient information
    Private Sub Set_PatientDetailStrip()
        ' '' Add Patient Details Control
        If (IsNothing(gloUC_PatientStrip1) = False) Then
            Me.Controls.Remove(gloUC_PatientStrip1)
            gloUC_PatientStrip1.Dispose()
            gloUC_PatientStrip1 = Nothing
        End If
        gloUC_PatientStrip1 = New gloUserControlLibrary.gloUC_PatientStrip

        With gloUC_PatientStrip1
            .Dock = DockStyle.Top
            .Padding = New Padding(3, 0, 3, 0)
            '' Pass Paarameters Type of Form
            ''Added On 20100628 by sanjog for Patinet Control Displayed
            'Line commented by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            '.ShowDetail(gnPatientID, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.ChiefComplaint)
            .ShowDetail(_nPatientId, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.ChiefComplaint)
            'end modification by dipak
            ''Added On 20100628 by sanjog for Patinet Control Displayed
            .BringToFront()
            .DTPValue = Format(Now, "MM/dd/yyyy")
            .DTPEnabled = False
        End With
        Me.Controls.Add(gloUC_PatientStrip1)
        ''''
        Pnl_main.BringToFront()
        C1_ChiefComplaints.BringToFront()
        '' Hide Previous Patient Details
        ' ''
    End Sub

    Private Sub C1_ChiefComplaints_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles C1_ChiefComplaints.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        'SHUBHANGI 20091006
        'use clear button to clear search text box.
        txtSearch.ResetText()
        txtSearch.Focus()
        ToolTip1.SetToolTip(btnClear, "Clear Search")
    End Sub

    Public Sub New(ByVal PatientID As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _nPatientId = PatientID
    End Sub
    ''' <summary>
    ''' Property writen for Patient Context
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property GetCurrentPatientID() As Int64 Implements mdlGeneral.IPatientContext.GetCurrentPatientID
        Get
            Return _nPatientId  'Curent patient variable(Local variable) for this module 
        End Get
    End Property
End Class