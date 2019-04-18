Imports gloEMRGeneralLibrary
Public Class gloUC_LabTest

    '//
    Private _TestID As Int64
    Private _ht As Int64 = 84

    Public Event gUC_PrecuationChanged(ByVal TestID As Int64, ByVal sData As String)
    Public Event gUC_InstructionChanged(ByVal TestID As Int64, ByVal sData As String)

    '//

    Private Sub gloUC_LabTest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnDown.Visible = True
        btnDown.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Down
        btnDown.BackgroundImageLayout = ImageLayout.Center
        btnUp.Visible = False
        pnlPatientDetail.Visible = True
        'Size
        On Error Resume Next
        'pnlPatientDetail.Height = 45
        Me.Height = _ht
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        pnlPatientDetail.Visible = True
        btnUp.Visible = False
        btnDown.Visible = True
        btnDown.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Down
        btnDown.BackgroundImageLayout = ImageLayout.Center
        'Size
        On Error Resume Next
        ' pnlPatientDetail.Height = 0
        Me.Height = _ht
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        pnlPatientDetail.Visible = False
        btnUp.Visible = True
        btnUp.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.UP
        btnUp.BackgroundImageLayout = ImageLayout.Center
        btnDown.Visible = False
        'Size
        On Error Resume Next
        'pnlPatientDetail.Height = 45
        Me.Height = 33
    End Sub

    '//
    Public Function SetData(ByVal TestID As Int64, ByVal Specimen As String, ByVal CollectionContainer As String, ByVal StorageTemperature As String, ByVal LOINCCode As String, ByVal Instructionas As String, ByVal Precuation As String, ByVal Comments As String) As Boolean
        _TestID = TestID
        lblSpecimen.Text = Specimen
        lblCollectionContainer.Text = CollectionContainer
        lblStorageTemperature.Text = StorageTemperature
        lblLOINCCode.Text = LOINCCode
        txtInstruction.Text = Instructionas
        txtPrecaution.Text = Precuation
        Return True
    End Function

    Private Sub txtInstruction_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtInstruction.Leave
        RaiseEvent gUC_InstructionChanged(_TestID, txtInstruction.Text)
    End Sub

    Private Sub txtPrecaution_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrecaution.Leave
        RaiseEvent gUC_PrecuationChanged(_TestID, txtPrecaution.Text)
    End Sub

    Private Sub btnDown_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.MouseHover
        btnDown.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.DownHover
        btnDown.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub btnDown_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.MouseLeave
        btnDown.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.Down
        btnDown.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub btnUp_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.MouseHover
        btnUp.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.UPHover
        btnUp.BackgroundImageLayout = ImageLayout.Center
    End Sub

    Private Sub btnUp_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.MouseLeave
        btnUp.BackgroundImage = Global.gloUserControlLibrary.My.Resources.Resources.UP
        btnUp.BackgroundImageLayout = ImageLayout.Center
    End Sub
End Class
