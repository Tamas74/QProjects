Imports gloEMRGeneralLibrary.gloEMRLab

Public Class frmLab_Notes
#Region "Variable Declarations"
    Public AckNoteType As String
    Public _labAckNotesID As Long = 0
    Public _Notes As String
    Public _blnModify As Boolean
#End Region

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub frmLab_Notes_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If AckNoteType = "InternalNotes" Then
            Me.Text = "Internal Comments"
            lblNotes.Text = "Internal Comment : "
        Else
            Me.Text = "Patient Notes"
            lblNotes.Text = "Notes to Patient : "
        End If
        If _blnModify = True Then
            txtNotes.Text = _Notes
        End If
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

    Private Function InUpNotes()

        Dim strMsg As String = "Please enter Internal Notes"
        Dim NoteType As Int16 = 0
        If AckNoteType = "PatientNotes" Then
            strMsg = "Please enter Patient Notes"
            NoteType = 1
        End If

        If txtNotes.Text.Trim() = "" Then
            MessageBox.Show(strMsg, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtNotes.Select()
            InUpNotes = Nothing
            Exit Function
        End If

        Dim oLabAckNotes As New LabAckNotes
        Try
            oLabAckNotes.AddModify(_labAckNotesID, txtNotes.Text, NoteType)
            Me.Close()
        Catch ex As Exception
        Finally
            oLabAckNotes = Nothing
        End Try
        InUpNotes = Nothing
    End Function
End Class