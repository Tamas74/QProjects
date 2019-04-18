Imports System.Data.SqlClient

Public Class clsStartUpSettings
    Public Shared Function IsConnect(ByVal strSQLServerName As String, ByVal strDatabaseName As String, ByVal isSQLAuthentication As Boolean, ByVal sUserName As String, ByVal sPassword As String, Optional ByRef sErrorMessage As String = "") As Boolean
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString(strSQLServerName, strDatabaseName, isSQLAuthentication, sUserName, sPassword)
            objCon.Open()
            objCon.Close()

            'objCon = Nothing
            Return True
        Catch ex As Exception
            'objCon = Nothing
            sErrorMessage = ex.Message
            Return False
        Finally
            If (IsNothing(objCon) = False) Then
                objCon.Dispose()
                objCon = Nothing
            End If

        End Try
    End Function
End Class
