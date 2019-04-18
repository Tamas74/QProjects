Imports System.Data.SqlClient
Public Class clsCSZ

#Region " Public Functions"
    Public Function FetchAddressInfo(ByVal zip As Int64) As DataTable 'against a Zipcode
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            Dim adpt As New SqlDataAdapter
            Dim arrlist As New ArrayList
            Dim dt As New DataTable
            Dim Cmd As SqlCommand
            Cmd = New SqlCommand("gsp_scanCSZ_MST")
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@nZip", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = zip
            adpt.SelectCommand = Cmd

            Cmd.Connection = objCon
            objCon.Open()
            adpt.Fill(dt)
            objCon.Close()
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
#End Region
End Class
