Imports gloEMR.gloOBVitals
Public Class frmOBVital_Comments
#Region "Variable Declarations"
    Public CommentType As String
    Public _OBVitalCommentsID As Long = 0
    Public _Notes As String
    Public _blnModify As Boolean
    Public _IsModified As Boolean = False
    Public CurrentCommentsID As Long = 0
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub frmOBVital_Comments_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If CommentType = "OBComments" Then
            Me.Text = "OB Vital Comments"
            lblNotes.Text = "OB Vital Comment : "
        End If
        If _blnModify = True Then
            RemoveHandler txtNotes.TextChanged, AddressOf txtNotes_TextChanged
            txtNotes.Text = _Notes
            AddHandler txtNotes.TextChanged, AddressOf txtNotes_TextChanged
        End If
    End Sub

    Private Sub tlsp_SpecimenMaster_ItemClicked(sender As System.Object, e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_NotesMaster.ItemClicked
        Try
            Select Case e.ClickedItem.Tag
                Case "Save"
                    InUpNotes()
                Case "Close"
                    If _IsModified = True Or txtNotes.Text.Trim = "" Then
                        Dim dresult As DialogResult
                        dresult = MessageBox.Show("Do you want to save the changes?", gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)
                        If dresult = Windows.Forms.DialogResult.No Then
                            _IsModified = False
                            Me.Close()
                        End If
                        If dresult = Windows.Forms.DialogResult.Yes Then
                            InUpNotes()
                            _IsModified = False

                        End If
                        If dresult = Windows.Forms.DialogResult.Cancel Then
                            Return
                        End If

                    Else
                        
                        Me.Close()
                    End If
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function InUpNotes()

        Dim strMsg As String = "Please enter OB Vital Comments"
        If txtNotes.Text.Trim() = "" Then
            MessageBox.Show(strMsg, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtNotes.Select()
            InUpNotes = Nothing
            Exit Function
        End If

        Dim oBVitalsComments As New ClsOBVitalsComment
        Try
            If oBVitalsComments.CheckDuplicate(_OBVitalCommentsID, txtNotes.Text.Trim) = True Then
                MessageBox.Show("Comment already Exists", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtNotes.Focus()
            Else
                CurrentCommentsID = oBVitalsComments.AddModify(_OBVitalCommentsID, txtNotes.Text.Trim)
                Me.Close()
            End If
        Catch ex As Exception
        Finally
            oBVitalsComments.Dispose()
            oBVitalsComments = Nothing
        End Try
        InUpNotes = Nothing
    End Function

    Private Sub txtNotes_TextChanged(sender As Object, e As System.EventArgs) Handles txtNotes.TextChanged
        _IsModified = True
    End Sub
End Class