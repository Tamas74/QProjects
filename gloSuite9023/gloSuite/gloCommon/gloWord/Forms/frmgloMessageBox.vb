Imports System.Text
Public Class frmgloMessageBox

    Private Sub frmgloMessageBox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Public Sub Setmessage(ByVal strmessage As StringBuilder)
        txtMessage.WordWrap = True
        txtMessage.Text = strmessage.ToString()
        txtMessage.SelectionStart = 0
        txtMessage.SelectionLength = 0
    End Sub


    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
End Class