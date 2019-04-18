Imports gloEMRGeneralLibrary.gloEMRLab

Public Class frmLabMakeNormalLotes

    Private Sub frmLabMakeNormalLotes_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        FillNotes()
    End Sub

    Private Sub tlsp_NormalNotes_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_NormalNotes.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "Save"
                AddNormalNote()
            Case "Close"
                Me.Close()
        End Select
    End Sub

    Private Sub FillNotes()
        Dim oLabAckNotes As New LabAckNotes
        Try
            Dim dtNotes As DataTable = oLabAckNotes.Get_AckNotes(0, 0) ''Get Internal notes
            If Not dtNotes Is Nothing AndAlso dtNotes.Rows.Count > 0 Then
                ''Add blank row
                Dim newCustomersRow As DataRow = dtNotes.NewRow()
                newCustomersRow("labAckNotes_ID") = -1
                newCustomersRow("labAckNotes") = ""
                dtNotes.Rows.InsertAt(newCustomersRow, 0)
                ''End blank row

                cmbInternalNotes.DataSource = dtNotes
                cmbInternalNotes.DisplayMember = Convert.ToString(dtNotes.Columns("labAckNotes"))
                cmbInternalNotes.ValueMember = Convert.ToString(dtNotes.Columns("labAckNotes_ID"))
            End If

            dtNotes = oLabAckNotes.Get_AckNotes(0, 1) ''Get Patient notes
            If Not dtNotes Is Nothing AndAlso dtNotes.Rows.Count > 0 Then
                ''Add blank row
                Dim newCustomersRow As DataRow = dtNotes.NewRow()
                newCustomersRow("labAckNotes_ID") = -1
                newCustomersRow("labAckNotes") = ""
                dtNotes.Rows.InsertAt(newCustomersRow, 0)
                ''End blank row

                cmbPatientNotes.DataSource = dtNotes
                cmbPatientNotes.DisplayMember = Convert.ToString(dtNotes.Columns("labAckNotes"))
                cmbPatientNotes.ValueMember = Convert.ToString(dtNotes.Columns("labAckNotes_ID"))
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oLabAckNotes = Nothing
        End Try
    End Sub

    Private Sub AddNormalNote()
        Dim oLabAckNotes As New LabAckNotes
        Try
            oLabAckNotes.AddNormalNote(Convert.ToInt64(cmbInternalNotes.SelectedValue), Convert.ToInt64(cmbPatientNotes.SelectedValue))
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oLabAckNotes = Nothing
        End Try
    End Sub

End Class