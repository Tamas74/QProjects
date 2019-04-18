Public Class clsSelectPatientGuarantor
    Implements IDisposable
    Private disposed As Boolean = False
    Public _GetConnectionString As String = String.Empty
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
    Dim nPatientID As Int64
    Dim nClinicID As Int64
    Public Function GetPatientAccounts(ByVal nPatientId As Int64, ByVal nClinicID As Int64) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters
        Dim dtPatientAccounts As DataTable = Nothing
        Try
            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@nPatientID", nPatientId, ParameterDirection.Input, SqlDbType.BigInt)
            oParam.Add("@nClinicID", nClinicID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("PA_GetAllPatientAccountsWithDescription", oParam, dtPatientAccounts)
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
            oParam.Dispose()
            oParam = Nothing
            Return dtPatientAccounts
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            'If IsNothing(dtPatientAccounts) = False Then
            '    dtPatientAccounts.Dispose()
            '    dtPatientAccounts = Nothing
            'End If
        End Try
    End Function

    Public Sub New(ByVal PatientID As Int64, ByVal ClinicID As Int64)
        nPatientID = PatientID
        nClinicID = ClinicID
    End Sub
    Public Sub Dispose() Implements System.IDisposable.Dispose
        'Disconnect();
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not disposed Then
            If disposing Then
                ''Use following snippet to dispose all private and public objects
                'If ObjectName IsNot Nothing Then
                '    ObjectName.Dispose()
                '    ObjectName = Nothing
                'End If
            End If
        End If
        disposed = True
    End Sub
    Public Function GetConnectionString()
        If _GetConnectionString.ToString() = "" Then
            If Not IsNothing(appSettings) Then
                _GetConnectionString = Convert.ToString(appSettings("DataBaseConnectionString"))
            End If
        End If
        Return _GetConnectionString
    End Function
End Class
