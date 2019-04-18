Imports gloEMRGeneralLibrary.gloEMRDatabase
Public Class ClsReconsileHistory
    '' To Select Patient Letter 
    Public Function getPatientRecHistory(ByVal nPatientID As Long, ByVal ReconsileType As Integer) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Dim dt As DataTable = Nothing
        '        Dim oResultTable As New DataTable
        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nPatientID"
            oParamater.Value = nPatientID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.Int
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@ReconsileType"
            oParamater.Value = ReconsileType
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

           

            dt = oDB.GetDataTable("gsp_GetReconsileHistory")
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function
End Class
