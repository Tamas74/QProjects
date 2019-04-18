Public Class gloSearchTextBox
    Inherits TextBox

    Public Event SearchFired()
    Dim _CurrentTime As Date


    Private Sub oTimer_Tick() Handles oTimer.Tick

        If Me.Text.Trim <> "" Then

            If DateTime.Now.Subtract(_CurrentTime).Milliseconds > 100 Then
                oTimer.Stop()
                ''Added by Mayuri: to set focus on sleected node after entering code
                If Me.Tag = "" Then
                    RaiseEvent SearchFired()
                ElseIf Me.Tag = "True" Then
                    Me.Tag = ""

                End If
            End If
                '


        Else
            oTimer.Stop()
            ''Added by Mayuri: to set focus on sleected node after entering code
            If Me.Tag = "" Then
                RaiseEvent SearchFired()
            ElseIf Me.Tag = "True" Then
                Me.Tag = ""

            End If



        End If


    End Sub

    Private Sub gloSearchTextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        ''Added on 20140221-To disable enter while searching in textbox
        If e.KeyValue = Keys.Enter Then
            Exit Sub
        End If
        _CurrentTime = DateTime.Now
        oTimer.Stop()
        oTimer.Interval = 700
        oTimer.Enabled = True
    End Sub

    Private Sub gloSearchTextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.TextChanged
       
        If oTimer.Enabled = False Then
            oTimer.Stop()
            oTimer.Enabled = True
        End If
    End Sub

End Class
