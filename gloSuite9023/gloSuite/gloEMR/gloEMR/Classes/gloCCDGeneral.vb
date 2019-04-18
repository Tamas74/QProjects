Public Class gloCCDGeneral
    Public Shared gstrMessageBoxCaption As String = "gloEMR"
    Public Shared gServerName As String = ""
    Public Shared gDataBase As String = ""
    Public Shared Sub UpdateLog(ByVal strLogMessage As String)
        Try

           
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, strLogMessage, gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.ViewCCD, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub
End Class
