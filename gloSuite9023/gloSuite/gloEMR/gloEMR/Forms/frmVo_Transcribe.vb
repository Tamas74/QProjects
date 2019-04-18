Imports System
Imports System.IO

Public Class frmVo_Transcribe
    Private axDgnEngineControl1 As AxDNSTools.AxDgnEngineControl

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    RichTextBox1.Clear()
    '    If TextBox1.Text.Equals("") Then
    '        MessageBox.Show("Wave File Input is required.", "Dragon", MessageBoxButtons.OK)
    '        Return
    '    End If

    '    If RadioButton1.Checked Then
    '        axDgnDictEdit1.TranscribeFile(TextBox1.Text)
    '        Button1.Enabled = False
    '    End If
    '    If RadioButton2.Checked Then
    '        If TextBox2.Text.Equals("") Then
    '            MessageBox.Show("File Output is required.", "Dragon", MessageBoxButtons.OK)
    '            Return
    '        End If
    '        axDgnDictEdit1.TranscribeFileToFile(TextBox1.Text, TextBox2.Text)
    '        Button1.Enabled = False
    '    End If
    '    If RadioButton3.Checked Then
    '        ' axDgnEngineControl1.DlgShow(DNSTools.DgnDialogConstants.dgndlgTranscribe, this.Handle.ToInt32(), TextBox1.Text, null); 
    '        Dim FormHandle As IntPtr = Me.Handle

    '        Dim intFormHandle As Int32 = FormHandle.ToInt32()

    '        Dim undefined As Object = Nothing
    '        Dim data As Object = TextBox1.Text
    '        If axDgnEngineControl1.DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgTranscribe) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
    '            axDgnEngineControl1.DlgShow(DNSTools.DgnDialogConstants.dgndlgTranscribe, intFormHandle, TextBox1.Text, Nothing)
    '        End If
    '    End If


    'End Sub

    Public Sub New()

        InitializeComponent()



    End Sub
    Public Sub Initialize(ByVal Engine As AxDNSTools.AxDgnEngineControl)
        axDgnEngineControl1 = Engine
        'axDgnEngineControl1.Register(DNSTools.DgnRegisterConstants.dgnregNoTrayMic)
        Dim RcHandle As IntPtr = RichTextBox1.Handle

        Dim intRcHandle As Int32 = RcHandle.ToInt32()

        'axDgnDictEdit1.Register(intRcHandle, DNSTools.DgnRegisterConstants.dgnregNoTrayMic); 
        axDgnDictEdit1.Register(intRcHandle)
        ' axDgnEngineControl1.set_Option (DNSTools.DgnDictationOptionConstants.dgndictoptionTranscriptionCommandSet, DNSTools.DgnTranscriptionCommandSetConstants.dgntranscriptionCmdDictationOnly); 


        AddHandler axDgnDictEdit1.TranscriptionStopped, AddressOf axDgnDictEdit1_TranscriptionStopped
        axDgnEngineControl1.set_CompatibilityModule(DNSTools.DgnCompatibilityModuleConstants.dgncompmoduleNatText, -1, False)
        axDgnEngineControl1.set_CompatibilityModule(DNSTools.DgnCompatibilityModuleConstants.dgncompmoduleEditControlSupport, -1, True)

    End Sub
    Private Sub axDgnDictEdit1_TranscriptionStopped(ByVal sender As Object, ByVal e As EventArgs) Handles axDgnDictEdit1.TranscriptionStopped
        Button1.Enabled = True
        LockUI(True)
    End Sub

    Private Sub LockUI(ByVal Lock As Boolean)
        tls_btnTranscribe.Enabled = Lock
        tlsp_VoceCenter.Enabled = Lock
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        OpenFileDialog1.Filter = "Wave Files (*.wav)|*.wav"
        OpenFileDialog1.CheckFileExists = True
        OpenFileDialog1.ShowDialog(System.Windows.Forms.Form.ActiveForm)
        TextBox1.Text = OpenFileDialog1.FileName
    End Sub

    Private Sub tlsp_VoceCenter_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles tlsp_VoceCenter.ItemClicked


        Select Case e.ClickedItem.Tag
            Case "Transcribe"
                RichTextBox1.Clear()

                If TextBox1.Text.Equals("") Then
                    MessageBox.Show("Wave File Input is required.", "Dragon", MessageBoxButtons.OK)
                    Return
                Else

                    If File.Exists(TextBox1.Text) = False Then
                        MessageBox.Show("Invalid file path given.", "Dragon", MessageBoxButtons.OK)
                        Return
                    End If

                End If

                If rbTranscribeToText.Checked Then
                    LockUI(False)
                    axDgnDictEdit1.TranscribeFile(TextBox1.Text)
                    Button1.Enabled = False
                End If
                If rbTranscribeFileToFile.Checked Then
                    If TextBox2.Text.Equals("") Then
                        MessageBox.Show("File Output is required.", "Dragon", MessageBoxButtons.OK)
                        Return
                    End If
                    LockUI(False)
                    axDgnDictEdit1.TranscribeFileToFile(TextBox1.Text, TextBox2.Text)
                    Button1.Enabled = False
                End If
                If rbTranscribeDialog.Checked Then
                    ' axDgnEngineControl1.DlgShow(DNSTools.DgnDialogConstants.dgndlgTranscribe, this.Handle.ToInt32(), TextBox1.Text, null); 
                    Dim FormHandle As IntPtr = Me.Handle

                    Dim intFormHandle As Int32 = FormHandle.ToInt32()

                    Dim undefined As Object = Nothing
                    Dim data As Object = TextBox1.Text
                    If axDgnEngineControl1.DlgStatusGet(DNSTools.DgnDialogConstants.dgndlgTranscribe) = DNSTools.DgnDlgStatusConstants.dgndlgstatusOK Then
                        axDgnEngineControl1.DlgShow(DNSTools.DgnDialogConstants.dgndlgTranscribe, intFormHandle, TextBox1.Text, Nothing)
                    End If
                End If

            Case "Close"
                Me.Close()

        End Select

    End Sub


    Private Sub rbTranscribeToText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbTranscribeToText.CheckedChanged
        If rbTranscribeToText.Checked = True Then
            rbTranscribeToText.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbTranscribeToText.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)

        End If
    End Sub

    Private Sub rbTranscribeFileToFile_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbTranscribeFileToFile.CheckedChanged

        If rbTranscribeFileToFile.Checked = True Then
            rbTranscribeFileToFile.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbTranscribeFileToFile.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)

        End If
    End Sub

    Private Sub rbTranscribeDialog_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbTranscribeDialog.CheckedChanged

        If rbTranscribeDialog.Checked = True Then
            rbTranscribeDialog.Font = gloGlobal.clsgloFont.gFont_BOLD 'New Font("Tahoma", 9, FontStyle.Bold)
        Else
            rbTranscribeDialog.Font = gloGlobal.clsgloFont.gFont 'New Font("Tahoma", 9, FontStyle.Regular)

        End If
    End Sub

    Private Sub tls_btnTranscribe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tls_btnTranscribe.Click

    End Sub
End Class
