
Public Class frmExportEncryption
    Public sEncryptKey As String = ""
    Public bEncryptedExe As Boolean = True

    Private Sub tsb_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsb_Close.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub tsb_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsb_Save.Click
        If txtEncryptKey.Text.Trim.Length <= 0 Then
            MessageBox.Show("Please enter the key.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        ElseIf gloSecurity.gloEncryption.ValidateKey(txtEncryptKey.Text.Trim()) = False Then
            Exit Sub
        End If

        sEncryptKey = txtEncryptKey.Text
        bEncryptedExe = chkIsExeEncryption.Checked
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class