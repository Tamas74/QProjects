Imports gloEMRGeneralLibrary.gloEMRLab

Public Class frmAddNotes
#Region "Variable Declarations"

    Public _Notes As String = ""
    Public _LabelCaption As String = "Reason for delete"
#End Region

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub frmAddNotes_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If _LabelCaption <> "" Then
            lblNotes.Text = _LabelCaption
        End If

        txtNotes.Text = _Notes

    End Sub

    Private Sub tlsp_SpecimenMaster_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_NotesMaster.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    InUpNotes()
                Case "Close"
                    Me.Close()
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub InUpNotes()

        Dim strMsg As String = "Please enter reason for delete record"

        If _LabelCaption = "Reason for delete" Then
            strMsg = "Please enter reason for delete record"
        ElseIf _LabelCaption = "Reason for Inactive" Then
            strMsg = "Please enter reason for inactive record"
        End If

        If txtNotes.Text.Trim() = "" Then
            MessageBox.Show(strMsg, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtNotes.Select()

            Exit Sub
        End If

        _Notes = txtNotes.Text.Trim
        Me.Close()

    End Sub
End Class