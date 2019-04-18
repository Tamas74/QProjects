Imports System.Windows.Forms

Public Class frmReviewDialog

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click

    End Sub

    Private Sub btnContinue_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.MouseHover
        btnContinue.BackgroundImage = My.Resources.Resources.Img_LongYellow
        btnContinue.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnContinue_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContinue.MouseLeave
        btnContinue.BackgroundImage = My.Resources.Resources.Img_LongButton
        btnContinue.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnStop_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.MouseHover
        btnStop.BackgroundImage = My.Resources.Resources.Img_LongYellow
        btnStop.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnStop_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStop.MouseLeave
        btnStop.BackgroundImage = My.Resources.Resources.Img_LongButton
        btnStop.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btn_Review_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Review.MouseHover
        btn_Review.BackgroundImage = My.Resources.Resources.Img_LongYellow
        btn_Review.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btn_Review_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Review.MouseLeave
        btn_Review.BackgroundImage = My.Resources.Resources.Img_LongButton
        btn_Review.BackgroundImageLayout = ImageLayout.Stretch
    End Sub
    Public Function Changecaption()
        btnContinue.Text = "Continue Preview"
        btnStop.Visible = False
        Return Nothing
    End Function

    Private Sub frmReviewDialog_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.TopMost = False
        If Me.DialogResult <> Windows.Forms.DialogResult.Yes And Me.DialogResult <> Windows.Forms.DialogResult.Cancel Then
            Me.DialogResult = Windows.Forms.DialogResult.No
        End If
    End Sub

    Private Sub frmReviewDialog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.TopMost = True
    End Sub
    Public Sub SetReviewBtnVisibility()
        btn_Review.Visible = False
        btnContinue.Location = btn_Review.Location
    End Sub
    
End Class