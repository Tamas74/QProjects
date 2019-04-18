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
Public Class clsLogin

#Region " Private Variables"
    Dim _sLoginName As String
    Dim _sPassword As String
#End Region

#Region " Public Properties"
    Public Property LoginName() As String
        Get
            Return _sLoginName
        End Get
        Set(ByVal Value As String)
            _sLoginName = Value
        End Set
    End Property
    Public Property Password() As String
        Get
            Return _sPassword
        End Get
        Set(ByVal Value As String)
            _sPassword = Value
        End Set
    End Property
#End Region

#Region " Public Functions"
    Public Function IsValidLogin() As Boolean
        Return IsValidLogin(_sLoginName, _sPassword)
    End Function
    Public Function IsClientAccess(ByVal strClientMachineName As String) As Boolean
        Dim nLoginUsers As Byte
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_CheckClientMachinePermission"
        objCmd.Connection = objCon

        Dim objParaClientMachineName As New SqlParameter
        With objParaClientMachineName
            .ParameterName = "@MachineName"
            .Value = strClientMachineName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaClientMachineName)


        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader()
        If objSQLDataReader.HasRows = True Then
            objSQLDataReader.Read()
            nLoginUsers = objSQLDataReader.Item(0)
        Else
            nLoginUsers = 0
        End If
        objCon.Close()
        'objCmd = Nothing
        'objCon = Nothing
        ''Sandip Darade 20091117
        If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
            objCmd.Dispose()
            objCon.Dispose()
        End If
        If nLoginUsers = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function IsValidLogin(ByVal sLoginName As String, ByVal sPassword As String) As Boolean
        Dim nLoginUsers As Byte
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_CheckLogin"
        objCmd.Connection = objCon

        Dim objParaLoginName As New SqlParameter
        With objParaLoginName
            .ParameterName = "@LoginName"
            .Value = sLoginName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaLoginName)

        Dim objParaPassword As New SqlParameter
        With objParaPassword
            .ParameterName = "@Password"
            .Value = sPassword
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaPassword)


        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader()
        If objSQLDataReader.HasRows = True Then
            objSQLDataReader.Read()
            nLoginUsers = objSQLDataReader.Item(0)
        Else
            nLoginUsers = 0
        End If
        objCon.Close()
        ''Sandip Darade 20091117
        If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
            objCmd.Dispose()
            objCon.Dispose()
        End If
        If nLoginUsers = 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Function IsAccessPermission(ByVal sLoginName As String, Optional ByVal blnAdminCheck As Boolean = False) As Boolean
        Dim nLoginUsers As Byte
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_CheckPermission"
        objCmd.Connection = objCon

        Dim objParaLoginName As New SqlParameter
        With objParaLoginName
            .ParameterName = "@LoginName"
            .Value = sLoginName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaLoginName)

        If blnAdminCheck = True Then
            Dim objParaCheckAdmin As New SqlParameter
            With objParaCheckAdmin
                .ParameterName = "@Administrator"
                .Value = 1
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objParaCheckAdmin)
        End If
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader()
        If objSQLDataReader.HasRows = True Then
            objSQLDataReader.Read()
            nLoginUsers = objSQLDataReader.Item(0)
        Else
            nLoginUsers = 0
        End If
        objCon.Close()
        'objCmd = Nothing
        'objCon = Nothing
        ''Sandip Darade 20091117
        If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
            objCmd.Dispose()
            objCon.Dispose()
        End If
        If nLoginUsers = 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Function RetrieveCurrentLoginUsers() As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_LoginUsers"
        objCmd.Connection = objCon
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objCon.Close()
        'objCon = Nothing
        ''Sandip Darade 20091117
        If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
            objCmd.Dispose()
            objCon.Dispose()
        End If
        Return dsData.Tables(0)
    End Function
#End Region
End Class
