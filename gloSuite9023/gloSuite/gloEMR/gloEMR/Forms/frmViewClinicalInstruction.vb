Public Class frmViewClinicalInstruction
    Dim _nPatientId As Int64
    Private WithEvents gloUC_PatientStrip As gloUserControlLibrary.gloUC_PatientStrip
    Dim dtData As DataTable

    Public WriteOnly Property OpenAddWindow() As Boolean

        Set(ByVal value As Boolean)

            If value = True Then

                'Call Add only if no existing Clinical Instruction present
                If IsNothing(C1_ClinicalInstruction) = True OrElse C1_ClinicalInstruction.Rows.Count <= 1 Then
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


    Private Sub frmViewClinicalInstruction_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


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
                .Padding = New Padding(3, 0, 3, 0)

                .ShowDetail(_nPatientId, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.ChiefComplaint)

                .BringToFront()
                .DTPValue = Format(Now, "MM/dd/yyyy")
                .DTPEnabled = False
            End With
            Me.Controls.Add(gloUC_PatientStrip)
            Pnl_main.BringToFront()
            C1_ClinicalInstruction.BringToFront()

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmViewClinicalInstruction_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
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
            Using objPatientClinicalInstruction As New ClsPatientClinicalInstruction()
                dtData = objPatientClinicalInstruction.GetPatientClinicalInstruction(_nPatientId, 0, Nothing, True)
            End Using

            If dtData IsNot Nothing Then
                'C1_ClinicalInstruction.Clear()
                C1_ClinicalInstruction.DataSource = Nothing
                C1_ClinicalInstruction.DataSource = dtData.DefaultView
                DesignGrid()
                Rule_Status()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub DesignGrid()
        Try
            gloC1FlexStyle.Style(C1_ClinicalInstruction)
            C1_ClinicalInstruction.Rows.Fixed = 1


            C1_ClinicalInstruction.Cols("nId").Visible = False
            C1_ClinicalInstruction.Cols("nPatientId").Visible = False
            C1_ClinicalInstruction.Cols("dtCreatedDate").Visible = False
            C1_ClinicalInstruction.Cols("dtModifiedDate").Visible = False

            C1_ClinicalInstruction.Cols("dtDate").Caption = "Date"
            C1_ClinicalInstruction.Cols("sInstruction").Caption = "Instruction"
            C1_ClinicalInstruction.Cols("sInstructionDtl").Caption = "Description"
            C1_ClinicalInstruction.Cols("bIsActive").Caption = "Active"

            C1_ClinicalInstruction.Cols("dtDate").Width = 75
            C1_ClinicalInstruction.Cols("sInstruction").Width = 300
            C1_ClinicalInstruction.Cols("sInstructionDtl").Width = 813
            C1_ClinicalInstruction.Cols("bIsActive").Width = 50

            'C1_ClinicalInstruction.ShowCellLabels = True
            C1_ClinicalInstruction.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn
            C1_ClinicalInstruction.Redraw = True

            C1_ClinicalInstruction.Cols("dtDate").AllowEditing = False
            C1_ClinicalInstruction.Cols("sInstruction").AllowEditing = False
            C1_ClinicalInstruction.Cols("sInstructionDtl").AllowEditing = False
            C1_ClinicalInstruction.Cols("bIsActive").AllowEditing = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub ts_btnAdd_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnAdd.Click
        Try
            Dim frm As New frmPatientClinicalInstruction(_nPatientId, Date.Now)
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
          

            If (C1_ClinicalInstruction.Rows.Count > 1) Then
                If (C1_ClinicalInstruction.RowSel > 0) Then
                    _nID = Convert.ToInt64(C1_ClinicalInstruction.GetData(C1_ClinicalInstruction.RowSel, 0))
                    Dim ofrm As New frmPatientClinicalInstruction(_nPatientId, _nID, True)
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

    Private Sub ts_btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnDelete.Click
        Try
            If (C1_ClinicalInstruction.Rows.Count > 1) Then
                If C1_ClinicalInstruction.RowSel > 0 Then
                    If MessageBox.Show("Are you sure you want to Delete the current record ?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

                        Dim _nID As Int64 = Convert.ToInt64(C1_ClinicalInstruction.GetData(C1_ClinicalInstruction.RowSel, 0))

                        Using ObjInstruction As New ClsPatientClinicalInstruction()
                            ObjInstruction.DeletePatientClinicalInstruction(_nID)
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
           SetClinicalInstructions()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ts_btnClose_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnClose.Click
        Me.Close()
    End Sub

    Private Sub ts_btnDeactivate_Click(sender As System.Object, e As System.EventArgs) Handles ts_btnDeactivate.Click
        Try
            If (C1_ClinicalInstruction.Rows.Count > 1) Then
                If C1_ClinicalInstruction.RowSel > 0 Then
                    If ts_btnDeactivate.Tag = "DEACTIVATE" Then
                        If MessageBox.Show("Are you sure you want to de-activate this record?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Dim _nID As Int64 = Convert.ToInt64(C1_ClinicalInstruction.GetData(C1_ClinicalInstruction.RowSel, 0))
                            Using ObjInstruction As New ClsPatientClinicalInstruction()
                                ObjInstruction.DeActivatePatientClinicalInstruction(_nID)
                            End Using
                            SetClinicalInstructions()
                        End If
                    Else

                        If MessageBox.Show("Are you sure you want to activate this record?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            Dim _nID As Int64 = Convert.ToInt64(C1_ClinicalInstruction.GetData(C1_ClinicalInstruction.RowSel, 0))
                            Dim _dtDate As Date = C1_ClinicalInstruction.GetData(C1_ClinicalInstruction.RowSel, 2)
                            Using ObjInstruction As New ClsPatientClinicalInstruction()
                                If (ObjInstruction.CheckActivePatientClinicalInstruction(_dtDate, _nPatientId)) Then
                                    MessageBox.Show("Clinical Instruction is already present for this visit. Enter a different date to save the record.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Else
                                    ObjInstruction.ActivatePatientClinicalInstruction(_nID)
                                End If


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

    Private Sub txtSearch_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtSearch.TextChanged
        Dim _dv As New DataView()
        _dv = DirectCast(C1_ClinicalInstruction.DataSource, DataView)
        If (IsNothing(_dv) = False) Then


            C1_ClinicalInstruction.DataSource = _dv
            Try
                Dim strSearch As String = txtSearch.Text.Trim()
                strSearch = strSearch.Replace("'", "''").Replace("[", "").Replace("]", "").Replace("%", "").Replace("*", "")
                If strSearch.StartsWith("%") = True Or strSearch.StartsWith("*") = True Then
                    _dv.RowFilter = (_dv.Table.Columns("sInstruction").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sInstructionDtl").ColumnName & " Like '%") + strSearch & "%'"
                Else
                    _dv.RowFilter = (_dv.Table.Columns("sInstruction").ColumnName & " Like '%") + strSearch & "%' OR " & (_dv.Table.Columns("sInstructionDtl").ColumnName & " Like '%") + strSearch & "%'"
                End If
            Catch Ex As Exception
                MessageBox.Show(Ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub C1_ClinicalInstruction_MouseDoubleClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles C1_ClinicalInstruction.MouseDoubleClick
        Try

            Dim hti As C1.Win.C1FlexGrid.HitTestInfo = C1_ClinicalInstruction.HitTest(e.X, e.Y)
            If (hti.Column = 1 And hti.Row = -1 Or hti.Row = 0) Then
                C1_ClinicalInstruction.Row = -1
            End If

            If (C1_ClinicalInstruction.Row = -1) Then
                Return
            End If

            If (C1_ClinicalInstruction.Rows.Count > 1) Then
                If (C1_ClinicalInstruction.RowSel > 0) Then
                    Dim _nID As Int64 = Convert.ToInt64(C1_ClinicalInstruction.GetData(C1_ClinicalInstruction.RowSel, 0))
                    Dim ofrm As New frmPatientClinicalInstruction(_nPatientId, _nID, True)
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

    Private Sub btnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
        txtSearch.Text = ""
    End Sub


    Private Sub C1_ClinicalInstruction_RowColChange(sender As System.Object, e As System.EventArgs) Handles C1_ClinicalInstruction.RowColChange
        Dim _sStatus As Boolean
        If C1_ClinicalInstruction.RowSel > 0 Then
            _sStatus = C1_ClinicalInstruction.GetData(C1_ClinicalInstruction.RowSel, 5)
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
        If C1_ClinicalInstruction.RowSel > 0 Then
            _sStatus = C1_ClinicalInstruction.GetData(C1_ClinicalInstruction.RowSel, 5).ToString()
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

    Private Sub C1_ClinicalInstruction_MouseMove(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles C1_ClinicalInstruction.MouseMove
        gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, sender, e.Location)
    End Sub
End Class