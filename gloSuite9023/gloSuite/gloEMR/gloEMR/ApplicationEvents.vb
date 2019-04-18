Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException            
            If e.Exception.ToString.Contains("System.AccessViolationException: Attempted to read or write protected memory. This is often an indication that other memory is corrupt") = True Then
                e.ExitApplication = False

            ElseIf e.Exception.ToString.Contains("CrystalDecisions.Windows.Forms.PageControl.OnMouseMove") = True Then
                e.ExitApplication = False

                '30-Oct-13 Aniket: Fixing Bug #58956
            ElseIf e.Exception.ToString.Contains("System.ObjectDisposedException: Cannot access a disposed object") = True Then
                e.ExitApplication = False

            ElseIf e.Exception.ToString.Contains("A StartDocPrinter call was not issued") = True Then
                MsgBox("Exception occurred while printing. Please try again." & vbCrLf & vbCrLf & e.Exception.ToString, MsgBoxStyle.Critical, "gloEMR")
                e.ExitApplication = False
                ''or condition added for bugid 98615
            ElseIf (e.Exception.ToString.Contains("cannot have a width or height equal to 0")) = True Then
                MsgBox("Unable to load the Calender screen. This might be caused due to the arrangement of the dashboard panels. Please rearrange the panels manually or click on 'Tools->Load Default Display Settings' to resolve this.", MsgBoxStyle.Information, "gloEMR")
                e.ExitApplication = False
                ''or condition added for JNSAW ,JNSAC for bugid 98615
            ElseIf (e.Exception.ToString.Contains("at Janus.Windows.UI.Dock.JNSCM") Or e.Exception.ToString.Contains("at Janus.Windows.Schedule.JNSAW") Or e.Exception.ToString.Contains("at Janus.Windows.Schedule.JNSAC")) = True Then
                '31-Mar-17 Aniket: Hiding the error message Bug #104344 ( Modified): gloEMR: Patient Referrals[Concurrency]: Applications shows Exception
                'MsgBox("Exception occurred while loading Janus control. Please logoff the application and again login." & vbCrLf & vbCrLf & e.Exception.ToString, MsgBoxStyle.Critical, "gloEMR")
                e.ExitApplication = False

                '22-Jun-16 Aniket: Bug #97055 ( Modified): EMR > Application throws an Exception when Finish Exam ( On Cloud Env. )
            ElseIf e.Exception.ToString.Contains("at Janus.Windows.UI.Dock.JNSK") = True Then
                e.ExitApplication = False

            ElseIf e.Exception.ToString.Contains("at Janus.Windows.UI.Dock.UIPanelBase.Dispose") = True Then
                MsgBox("Exception occurred while loading Janus control." & vbCrLf & vbCrLf & e.Exception.ToString, MsgBoxStyle.Critical, "gloEMR")
                e.ExitApplication = False
            Else
                MsgBox("gloEMR: UnhandledException" & vbCrLf & vbCrLf & e.Exception.ToString, MsgBoxStyle.Critical, "gloEMR")
                e.ExitApplication = False
            End If

        End Sub
    End Class

End Namespace

