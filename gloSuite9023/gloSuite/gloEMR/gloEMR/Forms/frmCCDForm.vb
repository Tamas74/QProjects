Public Class frmCCDForm
    Public isCDS As Boolean = False
    Public isCDA As Boolean = False
    Private Sub frmCCDForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Dim myScreenWidth As Integer = System.Windows.SystemParameters.PrimaryScreenWidth - 10
            'Dim myScreenHeight As Integer = System.Windows.SystemParameters.PrimaryScreenHeight - 10
            'If (Me.Width > myScreenWidth) OrElse (Me.Height > myScreenHeight) Then
            '    Me.MaximumSize = New System.Drawing.Size(myScreenWidth, myScreenHeight)
            '    Me.AutoScroll = True
            'End If

            'Dim DesignScreenWidth As Integer = 1280
            'Dim DesignScreenHeight As Integer = 1024
            'Added for Screen Resolution case on 7/24/2014

            Dim DesignScreenWidth As Integer = Me.Width
            Dim DesignScreenHeight As Integer = Me.Height
            Dim CurrentScreenWidth As Integer = Screen.PrimaryScreen.Bounds.Width - 20
            Dim CurrentScreenHeight As Integer = Screen.PrimaryScreen.Bounds.Height - 50
            Dim RatioX As Double = CurrentScreenWidth / DesignScreenWidth
            Dim RatioY As Double = CurrentScreenHeight / DesignScreenHeight

            'Dim myWidth As Integer = Me.Width * RatioX
            'Dim myHeight As Integer = Me.Height * RatioY

            Me.Width = Me.Width * RatioX
            Me.Height = (Me.Height * RatioY)
            'Me.MaximumSize = New Size(myWidth, myHeight)
            Me.Top = Me.Height - Screen.PrimaryScreen.Bounds.Height + 50
            Me.Left = Me.Width - Screen.PrimaryScreen.Bounds.Width + 20
            Me.StartPosition = FormStartPosition.CenterScreen
            Me.AutoScroll = True
            ' Dim myHeight As Integer = Me.pnlMainSummary.Location.Y + Me.pnlMainSummary.Height
            'If Me.Height > myHeight Then
            '    Me.Height = myHeight
            '    Me.StartPosition = FormStartPosition.CenterScreen
            '    Me.AutoScroll = True
            'End If

            If (isCDS) Then
                Me.Icon = Global.gloEMR.My.Resources.View_CDS_files
                lblFormularyTransactionMessage.Text = "Viewing CDS Information…"
            ElseIf (isCDA) Then
                Me.Text = "Preview CDA"
                Me.Icon = Global.gloEMR.My.Resources.Preview_CDA
            Else
                Me.Icon = Global.gloEMR.My.Resources.Browse_CCD
            End If
            pnlPrintMessage.BringToFront()
            pnlPrintMessage.Visible = True
            Me.Cursor = Cursors.WaitCursor
            If Me.WebBrowser1.ReadyState = WebBrowserReadyState.Loaded Or Me.WebBrowser1.ReadyState = WebBrowserReadyState.Loading Then
                While Me.WebBrowser1.ReadyState <> WebBrowserReadyState.Complete
                    Application.DoEvents()
                End While
            End If

            'Yatin-05/23/2012 Bug No.27404
            If Not IsNothing(Me.Owner) Then
                Me.Owner.Activate()
            End If

            If Me.WebBrowser1.ReadyState = WebBrowserReadyState.Complete Then
                'Me.WebBrowser1.Print()
            End If

            pnlPrintMessage.Visible = False
            'Try
            '    gloPatient.gloPatient.GetWindowTitle(Me, _PatientID, GetConnectionString(), gstrMessageBoxCaption)
            'Catch ex As Exception
            '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'End Try
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
        Me.Cursor = Cursors.Default
        Me.BringToFront()
        Me.Activate()
    End Sub

    Private Sub tblClose_Click(sender As System.Object, e As System.EventArgs) Handles tblClose.Click
        Me.Close()
    End Sub
End Class