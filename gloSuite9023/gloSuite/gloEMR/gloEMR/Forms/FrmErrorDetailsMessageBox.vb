Imports System.Windows.Forms

Public Class FrmErrorDetailsMessageBox

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub FrmErrorDetailsMessageBox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Public Sub New(ByVal ErrorHeading As String, ByVal ErrorDetails As String)


        ' This call is required by the designer.
        InitializeComponent()
        txtErrorDetails.Text = ErrorDetails
        lblErrorHeading.Text = ErrorHeading
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetails.Click
        If (btnDetails.Text = "Show Details") Then
            Me.Height = 365
            btnDetails.Text = "Hide Details"
        Else
            Me.Height = 133
            btnDetails.Text = "Show Details"
        End If

    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Me.Close()
    End Sub
End Class
