Imports System.Data.SqlClient
Public Class frmMSTCarePlan

    Dim _nId As Int64 = 0
    Dim _sCriterianame As String = ""
    Private Const Col_ID As Integer = 0
    Private Const Col_Problem As Integer = 1
    Private Const Col_Goal As Integer = 2
    Private Const Col_InternalNote As Integer = 3
    Private Const Col_Instruction As Integer = 4

    Private Sub frmMSTCarePlan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        gloC1FlexStyle.Style(C1CarePlan)
        Designgrid()
    End Sub

    Private Sub Designgrid()

        Dim objCarePlan As New ClsCarePlanMST()
        Try
            Dim dt As New DataTable
            Dim _dv As New DataView()
            txtSearch.Focus()
            dt = objCarePlan.GetCarePlanMST(0)
            _dv = dt.DefaultView

            If dt IsNot Nothing Then

                C1CarePlan.Visible = True
                'C1CarePlan.Clear()
                C1CarePlan.DataSource = Nothing
                C1CarePlan.DataSource = _dv
                C1CarePlan.Rows.Fixed = 1

                C1CarePlan.Cols(Col_ID).Caption = "ID"
                C1CarePlan.Cols(Col_Problem).Caption = "Problem"
                C1CarePlan.Cols(Col_Goal).Caption = "Goal"
                C1CarePlan.Cols(Col_InternalNote).Caption = "Internal Note"
                C1CarePlan.Cols(Col_Instruction).Caption = "Patient Instruction"


                C1CarePlan.Cols(Col_ID).Visible = False
                C1CarePlan.Cols(Col_Problem).Visible = True
                C1CarePlan.Cols(Col_Goal).Visible = True
                C1CarePlan.Cols(Col_InternalNote).Visible = True
                C1CarePlan.Cols(Col_Instruction).Visible = True

                C1CarePlan.Cols(Col_ID).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1CarePlan.Cols(Col_Problem).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1CarePlan.Cols(Col_Goal).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1CarePlan.Cols(Col_InternalNote).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter
                C1CarePlan.Cols(Col_Instruction).TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter

                Dim nWidth As Integer = Pnl_grid.Width

                C1CarePlan.Cols(Col_ID).Width = 0
                C1CarePlan.Cols(Col_Problem).Width = CInt((0.24 * (nWidth)))
                C1CarePlan.Cols(Col_Goal).Width = CInt((0.25 * (nWidth)))
                C1CarePlan.Cols(Col_InternalNote).Width = CInt((0.25 * (nWidth)))
                C1CarePlan.Cols(Col_Instruction).Width = CInt((0.25 * (nWidth)))

                'C1CarePlan.ShowCellLabels = True
                C1CarePlan.AllowEditing = False

            End If
        Catch ex As Exception
        Finally
            If objCarePlan IsNot Nothing Then
                objCarePlan.Dispose()
                objCarePlan = Nothing
            End If
        End Try
    End Sub

    Private Sub ts_ViewButtons_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ts_ViewButtons.ItemClicked
        Try
            Me.Cursor = Cursors.WaitCursor
            Select Case e.ClickedItem.Tag
                Case "Add"
                    Dim ofrmPatientCarePlan As New frmPatientCarePlan(0, 0)
                    Try
                        ofrmPatientCarePlan.ShowDialog(IIf(IsNothing(ofrmPatientCarePlan.Parent), Me, ofrmPatientCarePlan.Parent))
                        Designgrid()
                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, "Care Plan Master Record not saved", gloAuditTrail.ActivityOutCome.Failure)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        If ofrmPatientCarePlan IsNot Nothing Then
                            ofrmPatientCarePlan.Dispose()
                            ofrmPatientCarePlan = Nothing
                        End If
                    End Try
                Case "Modify"
                    Dim oPatientCarePlan As frmPatientCarePlan = Nothing
                    Try
                        If C1CarePlan IsNot Nothing Then
                            If (C1CarePlan.Rows.Count > 1) Then

                                '11-Nov-14 Aniket: Bug #75866 ( Modified): gloEMR: Care Plan- Application gives exception
                                If C1CarePlan.RowSel <> -1 Then
                                    _nId = Convert.ToInt64(C1CarePlan.GetData(C1CarePlan.RowSel, Col_ID))
                                    If _nId > 0 Then
                                        oPatientCarePlan = New frmPatientCarePlan(0, _nId)
                                        oPatientCarePlan.ShowDialog(IIf(IsNothing(oPatientCarePlan.Parent), Me, oPatientCarePlan.Parent))
                                        Designgrid()
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "Care Plan Master Record Modified", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                                    End If
                                Else
                                    MessageBox.Show("Please select a record to Modify.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                End If
                            End If
                        End If

                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "Care Plan Master Record not Modified", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        If oPatientCarePlan IsNot Nothing Then
                            oPatientCarePlan.Dispose()
                            oPatientCarePlan = Nothing
                        End If
                    End Try
                Case "Delete"
                    Dim oCarePlan As New ClsCarePlanMST
                    Try
                        If C1CarePlan IsNot Nothing Then
                            If (C1CarePlan.Rows.Count > 1) Then
                                '11-Nov-14 Aniket: Bug #75866 ( Modified): gloEMR: Care Plan- Application gives exception
                                If C1CarePlan.RowSel <> -1 Then
                                    If MessageBox.Show("Are you sure you want to delete this record?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                        _nId = Convert.ToInt64(C1CarePlan.GetData(C1CarePlan.RowSel, Col_ID))
                                        If _nId > 0 Then
                                            oCarePlan.DeletePatientCarePlan(_nId)
                                            Designgrid()
                                        End If
                                    End If
                                Else
                                    MessageBox.Show("Please select a record to Delete.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)

                                End If

                            End If
                        End If

                    Catch ex As Exception
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "Care Plan Master Record not Modified", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        If oCarePlan IsNot Nothing Then
                            oCarePlan.Dispose()
                            oCarePlan = Nothing
                        End If
                    End Try

                Case "Refresh"
                    Designgrid()
                    txtSearch.Text = ""
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Refresh, "Care Plan Master List Refreshed", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                Case "Close"
                    Me.Close()
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.CloseTransaction, gloAuditTrail.ActivityType.Close, "View Care Plan Master Closed", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            End Select

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Dim _dv As New DataView()
        _dv = DirectCast(C1CarePlan.DataSource, DataView)
        If (IsNothing(_dv) = False) Then


            Try
                Dim strSearch As String = txtSearch.Text.Trim()

                strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "")


                If strSearch.StartsWith("%") = True Or strSearch.StartsWith("*") = True Then
                    _dv.RowFilter = (_dv.Table.Columns("sProblem").ColumnName & " Like '%") + strSearch & "%'"
                Else
                    _dv.RowFilter = (_dv.Table.Columns("sProblem").ColumnName & " Like '%") + strSearch & "%' OR " _
                            & (_dv.Table.Columns("sGoal").ColumnName & " Like '%") + strSearch & "%' Or " _
                            & (_dv.Table.Columns("sNote").ColumnName & " Like '%") + strSearch & "%' Or " _
                            & (_dv.Table.Columns("sInstruction").ColumnName & " Like '%") + strSearch & "%'"

                End If
                C1CarePlan.DataSource = _dv
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, "Searched care plan Master entry having substring  " & txtSearch.Text.Trim, 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            Catch Ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, "Could not search care plan Master entry having substring  " & txtSearch.Text.Trim, gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show(Ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        txtSearch.ResetText()
        txtSearch.Focus()
    End Sub

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub
    Private Sub C1CarePlan_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles C1CarePlan.MouseDoubleClick
        Dim hti As C1.Win.C1FlexGrid.HitTestInfo = C1CarePlan.HitTest(e.X, e.Y)
        If (hti.Column = 1 And hti.Row = -1 Or hti.Row = 0) Then
            C1CarePlan.Row = -1
        End If
        If (C1CarePlan.Row = -1) Then
            Return
        End If
        Dim oPatientCarePlan As frmPatientCarePlan = Nothing
        Try
            If C1CarePlan IsNot Nothing Then
                If (C1CarePlan.Rows.Count > 1) Then
                    _nId = Convert.ToInt64(C1CarePlan.GetData(C1CarePlan.RowSel, Col_ID))
                    If _nId > 0 Then
                        oPatientCarePlan = New frmPatientCarePlan(0, _nId)
                        oPatientCarePlan.ShowDialog(IIf(IsNothing(oPatientCarePlan.Parent), Me, oPatientCarePlan.Parent))
                        Designgrid()
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "Care Plan Master Record Modified", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CarePlan, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "Care Plan Master Record not Modified", 0, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If oPatientCarePlan IsNot Nothing Then
                oPatientCarePlan.Dispose()
                oPatientCarePlan = Nothing
            End If
        End Try

    End Sub

    Private Sub C1CarePlan_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles C1CarePlan.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class