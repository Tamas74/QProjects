Imports gloEMRGeneralLibrary.gloEMRDatabase

Public Class clsViewScreenings

    Public Shared Function GetScreenings(ByVal PatientID As Int64, ByVal ProviderID As Int64, ByVal ScreeningType As String) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParameter As DBParameter

        Dim oResultTable As DataTable = Nothing
        Try

            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@nPatientID"
            oParameter.Value = PatientID
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@nProviderID"
            oParameter.Value = ProviderID
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@sScreeningType"
            oParameter.Value = ScreeningType
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oResultTable = oDB.GetDataTable("GetScreening")

            If Not oResultTable Is Nothing Then
                Return oResultTable
            End If
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PTProtocol, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    Public Sub New()

    End Sub
   
End Class
