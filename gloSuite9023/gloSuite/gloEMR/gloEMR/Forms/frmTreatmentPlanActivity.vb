Public Class frmTreatmentPlanActivity
    Dim Activity As String = ""
#Region " Constructor "

    Public Sub New(ByVal _activity As String)
        MyBase.New()
        InitializeComponent()
        Activity = Activity
        Me.Text = Me.Text & " - " & Activity
    End Sub

#End Region ' Constructor '

    Private Sub frmTreatmentPlanActivity_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

   
    Private Sub ComboBox1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub
End Class