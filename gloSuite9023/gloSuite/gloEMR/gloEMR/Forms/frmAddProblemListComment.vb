Public Class frmAddProblemListComment

    Public strComment As String = ""
    Public _TitleText As String = ""
    Private Sub tls_comment_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tls_comment.ItemClicked
        Select Case e.ClickedItem.Tag
            Case "OK"
                If txtComment.Text.Trim = "" Then
                    MessageBox.Show("Please enter comments", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtComment.Focus()
                    Exit Sub
                Else
                    strComment = txtComment.Text
                End If
                Me.DialogResult = Windows.Forms.DialogResult.OK
            Case "Cancel"
                Me.DialogResult = Windows.Forms.DialogResult.Cancel
                Me.Close()

        End Select
    End Sub

    Private Sub frmAddProblemListComment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtComment.Text = _Comment
        If _TitleText.Trim() <> "" Then
            Me.Text = _TitleText
            lblComments.Text = _TitleText
        End If

    End Sub
    Public _Comment As String = String.Empty
    Public Sub New(Optional ByVal Comment As String = "")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _Comment = Comment
        ' Add any initialization after the InitializeComponent() call.

    End Sub

End Class