Public Class frmUnMachedPatientDialog
    Public Sub New(ByVal CCDPatient As String, ByVal DashBoardPatient As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        lblCCDPatient.Text = ""
        lblDashboardPatient.Text = ""

        ' Add any initialization after the InitializeComponent() call.
        lblCCDPatient.Text = CCDPatient
        lblDashboardPatient.Text = DashBoardPatient

    End Sub


    Private Sub frmUnMachedPatientDialog_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub


    Private Sub btnStop_Click(sender As System.Object, e As System.EventArgs) Handles btnStop.Click
        Me.DialogResult = Windows.Forms.DialogResult.No
    End Sub

    Private Sub btnContinue_Click(sender As System.Object, e As System.EventArgs) Handles btnContinue.Click
        Me.DialogResult = Windows.Forms.DialogResult.Yes
    End Sub


    Private Sub BtnSelectDiff_Click(sender As System.Object, e As System.EventArgs) Handles BtnSelectDiff.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub


    Private Sub frmUnMachedPatientDialog_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'If form is directly closed without selecting any action 
        If Me.DialogResult <> Windows.Forms.DialogResult.Yes And Me.DialogResult <> Windows.Forms.DialogResult.Cancel Then
            Me.DialogResult = Windows.Forms.DialogResult.No
        End If
    End Sub

  
    Private Sub btnStop_MouseHover(sender As System.Object, e As System.EventArgs) Handles btnStop.MouseHover
        btnStop.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnStop.BackgroundImageLayout = ImageLayout.Stretch
    End Sub


    Private Sub btnContinue_MouseHover(sender As System.Object, e As System.EventArgs) Handles btnContinue.MouseHover
        btnContinue.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        btnContinue.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub BtnSelectDiff_MouseHover(sender As System.Object, e As System.EventArgs) Handles BtnSelectDiff.MouseHover
        BtnSelectDiff.BackgroundImage = Global.gloEMR.My.Resources.Resources.Img_LongYellow
        BtnSelectDiff.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnStop_MouseLeave(sender As System.Object, e As System.EventArgs) Handles btnStop.MouseLeave
        btnStop.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btnStop.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub btnContinue_MouseLeave(sender As System.Object, e As System.EventArgs) Handles btnContinue.MouseLeave
        btnContinue.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        btnContinue.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    Private Sub BtnSelectDiff_MouseLeave(sender As System.Object, e As System.EventArgs) Handles BtnSelectDiff.MouseLeave
        BtnSelectDiff.BackgroundImage = Global.gloEMR.My.Resources.Img_LongButton
        BtnSelectDiff.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

   
End Class