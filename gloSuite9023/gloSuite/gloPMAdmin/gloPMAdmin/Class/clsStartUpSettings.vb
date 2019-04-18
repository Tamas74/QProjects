'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************


Imports System.Data.SqlClient
Imports SQLDMO
Public Class clsStartUpSettings

    Public Function Fill_RegisteredSQLServers() As Collection
        Dim clSQLServers As New Collection
        Dim objSQLApplication As New Application
        If objSQLApplication.ServerGroups.Count >= 1 Then
            Dim objSQLGroup As ServerGroup
            objSQLGroup = objSQLApplication.ServerGroups.Item(1)
            Dim nCount As Int16
            For nCount = 1 To objSQLGroup.RegisteredServers.Count
                clSQLServers.Add(objSQLGroup.RegisteredServers.Item(nCount).Name())
            Next
        End If
        objSQLApplication = Nothing
        Return clSQLServers
    End Function
    Public Function Fill_SQLDatabases(ByVal strSQLServer As String) As Collection
        Dim clDatabases As New Collection
        Try
            Dim objSQLServer As New SQLServer
            objSQLServer.LoginSecure = True
            objSQLServer.LoginTimeout = 5
            objSQLServer.Connect(strSQLServer)
            Dim nCount As Int16
            For nCount = 1 To objSQLServer.Databases.Count
                clDatabases.Add(objSQLServer.Databases.Item(nCount).Name)
            Next
            objSQLServer.Close()
            objSQLServer = Nothing
            Return clDatabases
        Catch ex As Exception
            Return clDatabases
        End Try
    End Function

    Public Function IsConnect(ByVal strSQLServerName As String, ByVal strDatabaseName As String, ByVal isSQLAuthentication As Boolean, ByVal sUserName As String, ByVal sPassword As String, Optional ByRef sErrorMessage As String = "") As Boolean
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(strSQLServerName, strDatabaseName, isSQLAuthentication, sUserName, sPassword)
            objCon.Open()
            objCon.Close()
            ' objCon = Nothing
            Return True
        Catch ex As Exception
            ' objCon = Nothing
            sErrorMessage = ex.Message
            Return False
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
            End If
        End Try

    End Function


    Public Function IsSQLConnect(ByVal strSQLServerName As String, ByVal strDatabaseName As String, ByVal strUserID As String, ByVal strPwd As String) As Boolean
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(strSQLServerName, strDatabaseName, strUserID, strPwd)
            objCon.Open()
            objCon.Close()
            objCon = Nothing
            Return True
        Catch ex As Exception
            objCon = Nothing
            Return False
        End Try
    End Function

End Class
