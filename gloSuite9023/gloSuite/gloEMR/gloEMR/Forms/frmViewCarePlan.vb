Public Class frmViewCarePlan
    Dim _nPatientId As Int64
    Private WithEvents gloUC_PatientStrip As gloUserControlLibrary.gloUC_PatientStrip
    Dim dtData As DataTable

    Public WriteOnly Property OpenAddWindow() As Boolean

        Set(ByVal value As Boolean)

            If value = True Then

                'Call Add only if no existing care plan present
                If IsNothing(C1_CarePlan) = True OrElse C1_CarePlan.Rows.Count <= 1 Then

                    Call ts_btnAdd_Click(Nothing, Nothing)

                End If

            End If

        End Set
    End Property

    Public Sub New(ByVal PatientID As Long)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _nPatientId = PatientID
    End Sub


    Private Sub frmViewCarePlan_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Set_PatientDetailStrip()
        SetClinicalInstructions()

        Try
            gloPatient.gloPatient.GetWindowTitle(Me, _nPatientId, GetConnectionString(), gstrMessageBoxCaption)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Set_PatientDetailStrip()
        ' '' Add Patient Details Control
        Try
            gloUC_PatientStrip = New gloUserControlLibrary.gloUC_PatientStrip

            With gloUC_PatientStrip
                .Dock = DockStyle.Top
                .Padding = New Padding(3, 3, 3, 0)

                .ShowDetail(_nPatientId, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.ChiefComplaint)

                .BringToFront()
                .DTPValue = Format(Now, "MM/dd/yyyy")
                .DTPEnabled = False
            End With
            Me.Controls.Add(gloUC_PatientStrip)
            pnlToolStrip.SendToBack()
            Pnl_main.BringToFront()
            C1_CarePlan.BringToFront()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmViewCarePlan_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            If Not IsNothing(gloUC_PatientStrip) Then
                gloUC_PatientStrip.Dispose()
                gloUC_PatientStrip = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub SetClinicalInstructions()
        Try
            dtData = Nothing
            Using objPatientCarePlan As New ClsPatientCarePlan()
                dtData = objPatientCarePlan.GetPatientCarePlan(_nPatientId, 0)
            End Using

            If dtData IsNot Nothing Then
                'C1_CarePlan.Clear()
                C1_CarePlan.DataSource = Nothing
                C1_CarePlan.DataSource = dtData.DefaultView
                DesignGrid()
                Rule_Status()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub DesignGrid()
        Try

            gloC1FlexStyle.Style(C1_CarePlan)
            C1_CarePlan.Rows.Fixed = 1


            C1_CarePlan.Cols("nId").Visible = False
            C1_CarePlan.Cols("nPatientId").Visible = False
            C1_CarePlan.Cols("dtCreatedDate").Caption = "Created Date"   ''added for 8022 prd changes
            C1_CarePlan.Cols("dtCreatedDate").Format = "MM/dd/yyyy h:mm tt"
            C1_CarePlan.Cols("sProblem").Caption = "Problem"
            C1_CarePlan.Cols("sGoal").Caption = "Goal"
            C1_CarePlan.Cols("sNote").Caption = "Note"
            C1_CarePlan.Cols("sInstruction").Caption = "Instruction"
            C1_CarePlan.Cols("bIsActive").Caption = "Active"
            Dim wdth As Integer = Screen.PrimaryScreen.WorkingArea.Width - 40
            C1_CarePlan.Cols("dtCreatedDate").Width = wdth * 0.14
            C1_CarePlan.Cols("sProblem").Width = wdth * 0.18
            C1_CarePlan.Cols("sGoal").Width = wdth * 0.18
            C1_CarePlan.Cols("sNote").Width = wdth * 0.15
            C1_CarePlan.Cols("sInstruction").Width = wdth * 0.25
            C1_CarePlan.Cols("bIsActive").Width = wdth * 0.1




            'C1_CarePlan.ShowCellLabels = True
            C1_CarePlan.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn
            C1_CarePlan.Redraw = True
            C1_CarePlan.Cols("dtCreatedDate").AllowEditing = False
            C1_CarePlan.Cols("sProblem").AllowEditing = False
            C1_CarePlan.Cols("sGoal").AllowEditing = False
            C1_CarePlan.Cols("sNote").AllowEditing = False
            C1_CarePlan.Cols("sInstruction").AllowEditing = False
            C1_CarePlan.Cols("bIsActive").AllowEditing = False

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub ts_btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnAdd.Click
        Try
            Dim frm As New frmPatientCarePlan(_nPatientId, 0)
            frm.ShowDialog(IIf(IsNothing(frm.Parent), Me, frm.Parent))
            If Not IsNothing(frm) Then
                frm.Dispose()
                frm = Nothing
            End If
            SetClinicalInstructions()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnModify_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnModify.Click
        Try
            Dim _nID As Int64


            If (C1_CarePlan.Rows.Count > 1) Then
                If (C1_CarePlan.RowSel > 0) Then
                    _nID = Convert.ToInt64(C1_CarePlan.GetData(C1_CarePlan.RowSel, 0))
                    Dim ofrm As New frmPatientCarePlan(_nPatientId, _nID)
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    ofrm.Dispose()
                    ofrm = Nothing

                    SetClinicalInstructions()

                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnDeactivate_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnDeactivate.Click
        Try
            If (C1_CarePlan.Rows.Count > 1) Then
                If C1_CarePlan.RowSel > 0 Then
                    If ts_btnDeactivate.Tag = "DEACTIVATE" Then
                        If MessageBox.Show("Are you sure you want to de-activate this record?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Dim _nID As Int64 = Convert.ToInt64(C1_CarePlan.GetData(C1_CarePlan.RowSel, 0))
                            Using ObjCarePlan As New ClsPatientCarePlan()
                                ObjCarePlan.DeActivatePatientCarePlan(_nID)
                            End Using
                            SetClinicalInstructions()
                        End If
                    Else

                        If MessageBox.Show("Are you sure you want to activate this record?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Dim _nID As Int64 = Convert.ToInt64(C1_CarePlan.GetData(C1_CarePlan.RowSel, 0))
                            Using ObjCarePlan As New ClsPatientCarePlan()
                                'If (ObjInstruction.CheckActivePatientClinicalInstruction(_dtDate, _nPatientId)) Then
                                '    MessageBox.Show("Active clinical instruction present for same date.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                'Else
                                ObjCarePlan.ActivatePatientCarePlan(_nID)
                                ' End If
                            End Using
                            SetClinicalInstructions()
                        End If

                    End If

                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnDelete.Click
        Try
            If (C1_CarePlan.Rows.Count > 1) Then
                If C1_CarePlan.RowSel > 0 Then
                    If MessageBox.Show("Are you sure you want to Delete the current record ?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        Dim _nID As Int64 = Convert.ToInt64(C1_CarePlan.GetData(C1_CarePlan.RowSel, 0))

                        Using ObjCarePlan As New ClsPatientCarePlan()
                            ObjCarePlan.DeletePatientCarePlan(_nID, _nPatientId)
                        End Using

                        SetClinicalInstructions()
                    End If
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnRefresh.Click
        Try
            txtSearch.Text = ""
            SetClinicalInstructions()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnClose_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Sub C1_CarePlan_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles C1_CarePlan.MouseDoubleClick
        Try

            Dim hti As C1.Win.C1FlexGrid.HitTestInfo = C1_CarePlan.HitTest(e.X, e.Y)
            If (hti.Column = 1 And hti.Row = -1 Or hti.Row = 0) Then
                C1_CarePlan.Row = -1
            End If

            If (C1_CarePlan.Row = -1) Then
                Return
            End If

            If (C1_CarePlan.Rows.Count > 1) Then
                If (C1_CarePlan.RowSel > 0) Then
                    Dim _nID As Int64 = Convert.ToInt64(C1_CarePlan.GetData(C1_CarePlan.RowSel, 0))
                    Dim ofrm As New frmPatientCarePlan(_nPatientId, _nID)
                    ofrm.ShowDialog(IIf(IsNothing(ofrm.Parent), Me, ofrm.Parent))
                    ofrm.Dispose()
                    ofrm = Nothing
                    SetClinicalInstructions()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub C1_CarePlan_RowColChange(sender As System.Object, e As System.EventArgs) Handles C1_CarePlan.RowColChange
        Dim _sStatus As Boolean
        If C1_CarePlan.RowSel > 0 Then
            _sStatus = C1_CarePlan.GetData(C1_CarePlan.RowSel, 7)
            If _sStatus Then
                ts_btnDeactivate.Text = "De-&activate"
                ts_btnDeactivate.Tag = "DEACTIVATE"
                ts_btnDeactivate.Image = Global.gloEMR.My.Resources.Deactivate
            Else
                ts_btnDeactivate.Text = "&Activate"
                ts_btnDeactivate.Tag = "ACTIVATE"
                ts_btnDeactivate.Image = Global.gloEMR.My.Resources.Activate
            End If
        End If
    End Sub


    Private Sub Rule_Status()
        Dim _sStatus As Boolean
        If C1_CarePlan.RowSel > 0 Then
            _sStatus = C1_CarePlan.GetData(C1_CarePlan.RowSel, 7).ToString()
            If _sStatus Then
                ts_btnDeactivate.Text = "De-&activate"
                ts_btnDeactivate.Tag = "DEACTIVATE"
                ts_btnDeactivate.Image = Global.gloEMR.My.Resources.Deactivate
            Else
                ts_btnDeactivate.Text = "&Activate"
                ts_btnDeactivate.Tag = "ACTIVATE"
                ts_btnDeactivate.Image = Global.gloEMR.My.Resources.Activate
            End If
        End If
    End Sub

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click, btnClear.TextChanged
        txtSearch.Text = ""
    End Sub
    Private Sub txtSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSearch.TextChanged
        Dim _dv As New DataView()
        _dv = DirectCast(C1_CarePlan.DataSource, DataView)
        If (IsNothing(_dv) = False) Then


            C1_CarePlan.DataSource = _dv
            Try
                Dim strSearch As String = txtSearch.Text.Trim()
                strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "")
                If strSearch.StartsWith("%") = True Or strSearch.StartsWith("*") = True Then
                    _dv.RowFilter = (_dv.Table.Columns("sProblem").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sGoal").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sNote").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sInstruction").ColumnName & " Like '%") + strSearch & "%'"
                Else
                    _dv.RowFilter = (_dv.Table.Columns("sProblem").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sGoal").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sNote").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sInstruction").ColumnName & " Like '%") + strSearch & "%'"
                End If
            Catch Ex As Exception
                MessageBox.Show(Ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If

    End Sub

    Private Sub C1_CarePlan_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles C1_CarePlan.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class