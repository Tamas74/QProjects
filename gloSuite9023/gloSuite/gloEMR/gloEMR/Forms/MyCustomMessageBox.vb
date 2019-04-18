Public Class MyCustomMessageBox

    'Private _strMessageMethod As String
    'Private _p2 As Object
    'Private _p3 As Object

    Public Sub New(ByVal message As String, ByVal button1Name As String, ByVal button2name As String)
        ' This call is required by the designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call.

        Label1.Text = message
        btnYes.Text = button1Name
        btnNo.Text = button2name
        DialogResult = Windows.Forms.DialogResult.Cancel

    End Sub

    'Sub New(StrMessageMethod As String, Optional p2 As Object = Nothing, Optional p3 As Object = Nothing)
    '    ' TODO: Complete member initialization 
    '    _strMessageMethod = StrMessageMethod
    '    _p2 = p2
    '    _p3 = p3
    'End Sub

    Private Sub btnYes_Click(sender As System.Object, e As System.EventArgs) Handles btnYes.Click
        DialogResult = Windows.Forms.DialogResult.Yes
        Me.Close()
    End Sub

    Private Sub btnNo_Click(sender As System.Object, e As System.EventArgs) Handles btnNo.Click
        DialogResult = Windows.Forms.DialogResult.No
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class