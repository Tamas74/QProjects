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
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
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
        objCmd = Nothing
        objCon = Nothing
        If nLoginUsers = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function IsValidLogin(ByVal sLoginName As String, ByVal sPassword As String) As Boolean
        Dim nLoginUsers As Byte
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
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
        objCmd = Nothing
        objCon = Nothing
        If nLoginUsers = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function IsAccessPermission(ByVal sLoginName As String, Optional ByVal blnAdminCheck As Boolean = False) As Boolean
        Dim nLoginUsers As Byte
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
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
        objCmd = Nothing
        objCon = Nothing
        If nLoginUsers = 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Function RetrieveCurrentLoginUsers() As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_LoginUsers"
        objCmd.Connection = objCon
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objCon.Close()
        objCon = Nothing
        Return dsData.Tables(0)
    End Function
    'Added Code for Audit LOG Enhancement
    Public Function UpdateRemoteLoginDetails(ByVal strUserName As String, ByVal blnLoginInStatus As Boolean, Optional ByVal strMachineName As String = "", Optional ByVal SoftwareComponent As String = "", Optional ByVal ClinicID As Int64 = 0) As Boolean
        Dim remoteMachine As gloAuditTrail.MachineDetails.MachineInfo = gloAuditTrail.MachineDetails.RemoteMachineDetails()
        Dim localMachine As gloAuditTrail.MachineDetails.MachineInfo = gloAuditTrail.MachineDetails.LocalMachineDetails()

        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        If Not IsNothing(remoteMachine) Then

            If blnLoginInStatus = True Then
                objCmd.CommandText = "gsp_InsertRemoteUsersDetails"
            End If

            Dim objLoginName As New SqlParameter
            With objLoginName
                .ParameterName = "@sLoginName"
                .Value = strUserName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objLoginName)
            objLoginName = Nothing

            Dim objLocalMachineName As New SqlParameter
            With objLocalMachineName
                .ParameterName = "@LocalMachineName"
                .Value = localMachine.MachineName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objLocalMachineName)
            objLocalMachineName = Nothing

            Dim objLocalMachineIP As New SqlParameter
            With objLocalMachineIP
                .ParameterName = "@LocalMachineIP"
                .Value = localMachine.MachineIp
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objLocalMachineIP)
            objLocalMachineIP = Nothing

            Dim objLocalUserName As New SqlParameter
            With objLocalUserName
                .ParameterName = "@LocalUserName"
                .Value = localMachine.UserName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objLocalUserName)
            objLocalUserName = Nothing

            Dim objRemoteMachineName As New SqlParameter
            With objRemoteMachineName
                .ParameterName = "@RemoteMachineName"
                .Value = remoteMachine.MachineName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objRemoteMachineName)
            objRemoteMachineName = Nothing

            Dim objRemoteMachineIP As New SqlParameter
            With objRemoteMachineIP
                .ParameterName = "@RemoteMachineIP"
                .Value = remoteMachine.MachineIp
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objRemoteMachineIP)
            objRemoteMachineIP = Nothing

            Dim objRemoteUserName As New SqlParameter
            With objRemoteUserName
                .ParameterName = "@RemoteUserName"
                .Value = remoteMachine.UserName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objRemoteUserName)
            objRemoteUserName = Nothing

            Dim objDomain As New SqlParameter
            With objDomain
                .ParameterName = "@Domain"
                .Value = remoteMachine.DomainName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objDomain)
            objDomain = Nothing

            Dim objClientProcessID As New SqlParameter
            With objClientProcessID
                .ParameterName = "@ClientProcessID"
                .Value = Process.GetCurrentProcess().Id
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objClientProcessID)
            objClientProcessID = Nothing

            Dim objSoftwareComponent As New SqlParameter
            With objSoftwareComponent
                .ParameterName = "@SoftwareComponent"
                .Value = SoftwareComponent
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objSoftwareComponent)
            objSoftwareComponent = Nothing

            Dim objClinicID As New SqlParameter
            With objClinicID
                .ParameterName = "@ClinicID"
                .Value = ClinicID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objClinicID)
            objClinicID = Nothing

        End If
        objCmd.Connection = objCon
        objCon.Open()
        objCmd.ExecuteNonQuery()
        objCon.Close()
        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        objCon.Dispose()
        objCon = Nothing
        Return True
    End Function
#End Region
End Class
