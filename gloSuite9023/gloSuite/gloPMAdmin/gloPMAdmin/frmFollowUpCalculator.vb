Public Class frmFollowupCalculator
    Private _message As Integer
    Public Property MessageID() As Integer
        Get
            Return _message
        End Get
        Set(ByVal value As Integer)
            _message = value
        End Set
    End Property

    Private Sub frmFollowupCalculator_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim _messageText As String = String.Empty
        Select Case _message
            Case 0
                ''Message changed according to phill sir
                _messageText = "All Patient Accounts and Insurance Claims will be reviewed by the system to calculate the initial Follow-up Dates and Actions."
                pnlRDButton.Visible = False
                Me.Height = 188
                Me.Width = 429
                Me.Text = "Calculate Follow-up Queues for Accounts or Claims"
                btnReset.Text = "Calculate"
            Case 1
                ''Message changed according to phill sir
                _messageText = "All Patient Accounts and Insurance Claims will be reviewed by the system again to recalculate the starting Follow-up Dates and Actions."
                pnlRDButton.Visible = True
                Me.Height = 270
                Me.Width = 429
                Me.Text = "Recalculate Follow-up Queues for Accounts or Claims"
                btnReset.Text = "Recalculate"
        End Select
        lblMessage.Text = _messageText
        Me.AcceptButton = btnCancel
    End Sub

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(sender As System.Object, e As System.EventArgs) Handles btnReset.Click
        Dim frm As frmAutoFollowupUtility
        Try
            If pnlRDButton.Visible = False Then
                frm = New frmAutoFollowupUtility("Both")
                frm.ShowDialog()
            Else
                If rdbtn_Account_FollowUp.Checked = False And rdbtn_Claim_FollowUp.Checked = False Then
                    MessageBox.Show("Please select Account or Claim to recalculate follow-up.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return
                End If

                Select Case True
                    Case rdbtn_Account_FollowUp.Checked
                        frm = New frmAutoFollowupUtility("Account")
                        frm.ShowDialog()
                    Case rdbtn_Claim_FollowUp.Checked
                        frm = New frmAutoFollowupUtility("Claim")
                        frm.ShowDialog()
                End Select

            End If
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Exception: " + ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If Not IsNothing(frm) Then
                frm.Dispose()
            End If
        Finally
            If Not IsNothing(frm) Then
                frm.Dispose()
            End If
        End Try

    End Sub

End Class