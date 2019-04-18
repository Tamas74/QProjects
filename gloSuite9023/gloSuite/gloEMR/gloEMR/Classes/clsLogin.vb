Imports System.Data.SqlClient
Public Class clsLogin
    Implements IDisposable ''slr implement idisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free managed resources when explicitly called
                _sLoginName = Nothing
                _sPassword = Nothing
                _sNickName = Nothing

            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

#Region "   Private Variables"
    Dim _sLoginName As String = ""
    Dim _sPassword As String = ""
    Dim _sNickName As String = ""
#End Region
#Region "   Public Properties"
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
    Public Property NickName() As String
        Get
            Return _sNickName
        End Get
        Set(ByVal Value As String)
            _sNickName = Value
        End Set
    End Property

#End Region
#Region "   Public Functions"
    Public Function IsValidLogin() As Boolean
        Return IsValidLogin(_sLoginName, _sPassword)
    End Function
    'Public Function IsClientAccess(ByVal strClientMachineName As String) As Boolean
    '    'gnPrefixTransactionID = 0
    '    'Dim nLoginUsers As Byte
    '    Dim objCon As New SqlConnection
    '    objCon.ConnectionString = GetConnectionString()
    '    Dim objCmd As New SqlCommand
    '    objCmd.CommandType = CommandType.StoredProcedure
    '    objCmd.CommandText = "gsp_CheckClientMachinePermission"
    '    objCmd.Connection = objCon

    '    Dim objParaClientMachineName As New SqlParameter
    '    With objParaClientMachineName
    '        .ParameterName = "@MachineName"
    '        .Value = strClientMachineName
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaClientMachineName)

    '    gnClientMachineID = 0
    '    objCon.Open()
    '    gnClientMachineID = objCmd.ExecuteScalar
    '    objCon.Close()
    '    If IsNothing(gnClientMachineID) Then
    '        gnClientMachineID = 0
    '    End If
    '    objCmd = Nothing
    '    objCon = Nothing
    '    If gnClientMachineID = 0 Then
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function

    Public Function IsClientAccess(ByVal strClientMachineName As String) As Boolean
        'gnPrefixTransactionID = 0
        'Dim nLoginUsers As Byte
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure

        'Aniket Renamed gsp_CheckClientMachinePermission to sp_CheckClientMachinePermission as it is necessary for backward compatibility in multiple databases
        objCmd.CommandText = "sp_CheckClientMachinePermission"
        objCmd.Connection = objCon

        Dim objParaClientMachineName As New SqlParameter
        With objParaClientMachineName
            .ParameterName = "@MachineName"
            .Value = strClientMachineName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaClientMachineName)
        ''Sandip Darade 20091113
        Dim objParaProductCode As New SqlParameter
        With objParaProductCode
            .ParameterName = "@sProductCode"
            .Value = "1"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaProductCode)

        gnClientMachineID = 0
        objCon.Open()
        gnClientMachineID = objCmd.ExecuteScalar
        objCon.Close()
        If IsNothing(gnClientMachineID) Then
            gnClientMachineID = 0
        End If
        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        objParaClientMachineName = Nothing
        objParaProductCode = Nothing
        objCon.Dispose()
        objCon = Nothing
        If gnClientMachineID = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function IsValidLogin(ByVal sLoginName As String, ByVal sPassword As String) As Boolean
        Dim nLoginUsers As Byte
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
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
        nLoginUsers = objCmd.ExecuteScalar
        objCon.Close()
        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        objParaPassword = Nothing
        objParaLoginName = Nothing
        objCon.Dispose()
        objCon = Nothing
        If nLoginUsers = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function IsValidLoginByNickName(ByVal sNickName As String) As Boolean
        Dim nLoginUsers As Byte
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_CheckLoginByNickName"
        objCmd.Connection = objCon

        Dim objParaNickName As New SqlParameter
        With objParaNickName
            .ParameterName = "@NickName"
            .Value = sNickName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaNickName)


        objCon.Open()
        nLoginUsers = objCmd.ExecuteScalar
        objCon.Close()
        objCmd.Parameters.Clear()
        objCmd.Dispose()
        objCmd = Nothing
        objCon.Dispose()
        objParaNickName = Nothing
        objCon = Nothing
        If nLoginUsers = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function GetLoginUserNameByNickName(ByVal sNickName As String) As String
        Dim strLoginUser As String = ""
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        ' Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_RetrieveUserNameByNickName"
        objCmd.Connection = objCon

        Dim objParaNickName As New SqlParameter
        With objParaNickName
            .ParameterName = "@NickName"
            .Value = sNickName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaNickName)


        objCon.Open()
        strLoginUser = objCmd.ExecuteScalar
        objCon.Close()
        If IsNothing(strLoginUser) Then
            strLoginUser = ""
        End If
        objCmd.Parameters.Clear()
        objCmd.Dispose()
        objCmd = Nothing        
        objCon.Dispose()
        objCon = Nothing
        objParaNickName = Nothing
        Return strLoginUser
    End Function
    Public Function IsAccessPermission(ByVal sLoginName As String) As Boolean
        Dim nLoginUsers As Byte
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
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
        objCon.Open()
        nLoginUsers = objCmd.ExecuteScalar
        objCon.Close()
        objCmd.Parameters.Clear()
        objCmd.Dispose()
        objCmd = Nothing
        objCon.Dispose()
        objCon = Nothing
        objParaLoginName = Nothing
        If nLoginUsers = 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    'Public Function ScanClientMachine(ByVal strClientMachineName As String) As DataTable
    '    Dim objCon As New SqlConnection
    '    objCon.ConnectionString = GetConnectionString()
    '    Dim objCmd As New SqlCommand
    '    objCmd.CommandType = CommandType.StoredProcedure
    '    objCmd.CommandText = "gsp_ViewClientMachines"
    '    objCmd.Connection = objCon
    '    Dim objParaProviderName As New SqlParameter
    '    With objParaProviderName
    '        .ParameterName = "@MachineName"
    '        .Value = strClientMachineName
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaProviderName)
    '    objCmd.Connection = objCon
    '    objCon.Open()
    '    Dim objDA As New SqlDataAdapter(objCmd)
    '    Dim dtTable As New DataTable
    '    objDA.Fill(dtTable)
    '    objCon.Close()
    '    objCon = Nothing
    '    Return dtTable
    'End Function
    Public Function ScanClientMachine(ByVal strClientMachineName As String) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_ViewClientMachines"
        objCmd.Connection = objCon
        Dim objParaProviderName As New SqlParameter
        With objParaProviderName
            .ParameterName = "@MachineName"
            .Value = strClientMachineName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaProviderName)

        ''Sandip Darade 20091113
        Dim objParaProductCode As New SqlParameter
        With objParaProductCode
            .ParameterName = "@sProductCode"
            .Value = "1"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaProductCode)

        objCmd.Connection = objCon
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dtTable As New DataTable
        objDA.Fill(dtTable)
        objDA.Dispose()
        objDA = Nothing
        objCon.Close()
        objCon.Dispose()
        objCon = Nothing

        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If

        objParaProviderName = Nothing
        objParaProductCode = Nothing

        Return dtTable
    End Function

    Public Function UpdateLoginStatus(ByVal strUserName As String, ByVal blnLoginInStatus As Boolean, ByVal strMachineName As String, LoginSessionID As Long)

        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim objParameter As New SqlParameter

        objCon.ConnectionString = GetConnectionString()


        objCmd.CommandType = CommandType.StoredProcedure

        If blnLoginInStatus = True Then
            objCmd.CommandText = "gsp_InsertLoginUsers"
        Else
            objCmd.CommandText = "gsp_DeleteLoginUsers"
        End If

        With objParameter
            .ParameterName = "@UserName"
            .Value = strUserName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParameter)

        If blnLoginInStatus = True Then
            'Dim objParaMachine As New SqlParameter

            objParameter = New SqlParameter
            With objParameter
                .ParameterName = "@MachineName"
                .Value = strMachineName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParameter)
            objParameter = Nothing

        End If

        objParameter = New SqlParameter
        With objParameter
            .ParameterName = "@LoginSessionID"
            .Value = LoginSessionID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With

        objCmd.Parameters.Add(objParameter)
        objParameter = Nothing

        objCmd.Connection = objCon
        objCon.Open()
        objCmd.ExecuteNonQuery()
        objCon.Close()

        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If

        objParameter = Nothing

        objCon.Dispose()
        objCon = Nothing

        Return True

    End Function

    Public Function DeleteLoginStatus(ByVal strUserName As String)
        Dim Con As SqlConnection
        'Dim conString As String
        Dim cmd As SqlCommand
        Try
            Con = New SqlConnection(GetConnectionString())
            cmd = New SqlCommand("gsp_DeleteLoginUsers", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter
            Dim objParaMachine As New SqlParameter
            sqlParam = cmd.Parameters.Add("@UserName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = strUserName
            Con.Open()
            cmd.ExecuteNonQuery()
            Con.Close()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            sqlParam = Nothing
            Con.Dispose()
            Con = Nothing
            sqlParam = Nothing
            objParaMachine = Nothing
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

        End Try
    End Function
    Public Function GetSingleSignONUser(ByVal WindowsUser As String) As DataTable

        Dim objCon As New SqlConnection
        Dim dtTable As New DataTable
        Dim objParaProviderName As New SqlParameter
        Dim objCmd As New SqlCommand
        Dim objDA As SqlDataAdapter

        Try

            objCon.ConnectionString = GetConnectionString()


            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetSingleSignONUser"
            objCmd.Connection = objCon


            With objParaProviderName
                .ParameterName = "@WindowsUser"
                .Value = WindowsUser
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With

            objCmd.Parameters.Add(objParaProviderName)

            objCmd.Connection = objCon
            objDA = New SqlDataAdapter(objCmd)

            objCon.Open()
            objDA.Fill(dtTable)
            objCon.Close()

            If IsNothing(objDA) = False Then
                objDA.Dispose()
                objDA = Nothing
            End If

            If IsNothing(objParaProviderName) = False Then
                objParaProviderName = Nothing
            End If

            If IsNothing(objCon) = False Then
                objCon.Dispose()
                objCon = Nothing
            End If


            If IsNothing(objCmd) = False Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dtTable

    End Function
    Public Function DefaultLoginProvider(ByVal sLoginName As String, ByRef nProviderID As Int64) As String
        Dim strProviderName As String = ""
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_RetrieveLoginProvider"
        Dim objParaUserName As New SqlParameter
        With objParaUserName
            .ParameterName = "@LoginName"
            .Value = sLoginName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaUserName)
        objCmd.Connection = objCon
        objCon.Open()

        objSQLDataReader = objCmd.ExecuteReader()
        While objSQLDataReader.Read
            strProviderName = objSQLDataReader.Item("ProviderName").ToString.Trim
            nProviderID = Convert.ToInt64(objSQLDataReader.Item("ProviderID"))
        End While
        objSQLDataReader.Close()
        objCon.Close()
        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        objParaUserName = Nothing
        objCon.Dispose()
        objCon = Nothing
        Return strProviderName
    End Function


    Public Function GetLoginProviderID(ByVal sLoginName As String) As Int64
        Dim _result As Int64 = 0
        Dim strProviderID As String = ""
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        ' Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_RetrieveLoginProviderID"
        Dim objParaUserName As New SqlParameter
        With objParaUserName
            .ParameterName = "@LoginName"
            .Value = sLoginName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaUserName)
        objCmd.Connection = objCon
        objCon.Open()
        strProviderID = objCmd.ExecuteScalar
        If IsNothing(strProviderID) Then
            _result = 0
        Else
            _result = CType(strProviderID, Int64)
        End If
        objCon.Close()
        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        objParaUserName = Nothing
        objCon.Dispose()
        objCon = Nothing
        Return _result
    End Function

    Public Function IsLoginUserAdmin(ByVal sLoginName As String, Optional ByVal blnAdminCheck As Boolean = False) As Boolean
        Dim nLoginUsers As Byte
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
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
            objParaCheckAdmin = Nothing
        End If
        objCon.Open()
        nLoginUsers = objCmd.ExecuteScalar
        objCon.Close()
        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        objParaLoginName = Nothing
        objCon.Dispose()
        objCon = Nothing
        If nLoginUsers = 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Function IsUnLockUserAdmin(ByVal sNickName As String, Optional ByVal blnAdminCheck As Boolean = False) As Boolean
        Dim nLoginUsers As Byte
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_CheckPermissionByNickName"
        objCmd.Connection = objCon

        Dim objParaNickName As New SqlParameter
        With objParaNickName
            .ParameterName = "@NickName"
            .Value = sNickName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaNickName)

        If blnAdminCheck = True Then
            Dim objParaCheckAdmin As New SqlParameter
            With objParaCheckAdmin
                .ParameterName = "@Administrator"
                .Value = 1
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objParaCheckAdmin)
            objParaCheckAdmin = Nothing
        End If
        objCon.Open()
        nLoginUsers = objCmd.ExecuteScalar
        objCon.Close()
        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        objParaNickName = Nothing
        objCon.Dispose()
        objCon = Nothing
        If nLoginUsers = 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    ''' <summary>
    ''' dhruv 20091128
    ''' Passing the parameter to update the database of client setting
    ''' </summary>
    ''' <param name="strClientMachineName"></param>
    ''' <param name="strCurrentProductVersion"></param>
    ''' <param name="strdtUpdateDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateClientVersion(ByVal strClientMachineName As String, ByVal strCurrentProductVersion As String, ByVal strdtUpdateDate As Date)
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand()

        Try


            con.ConnectionString = GetConnectionString()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_UPCheckClientMachinePermission"
            cmd.CommandTimeout = 0
            cmd.Connection = con

            ''to pass the parameter
            Dim oParamMachineName As New SqlParameter
            With oParamMachineName
                .ParameterName = "@sMachineName"
                .Value = strClientMachineName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            cmd.Parameters.Add(oParamMachineName)

            Dim oParamCurrentProductVersion As New SqlParameter
            With oParamCurrentProductVersion
                .ParameterName = "@sCurrentProductVersion"
                .Value = strCurrentProductVersion
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            cmd.Parameters.Add(oParamCurrentProductVersion)

            Dim oParamUpdatedate As New SqlParameter
            With oParamUpdatedate
                .ParameterName = "@dtUpdateDate"
                .Value = strdtUpdateDate
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            cmd.Parameters.Add(oParamUpdatedate)


            cmd.Connection = con
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()

            oParamMachineName = Nothing
            oParamCurrentProductVersion = Nothing
            oParamUpdatedate = Nothing
        Catch ex As Exception

        Finally
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            con.Dispose()
            con = Nothing

        End Try
        Return Nothing
    End Function

    Public Function GetLoginFullName(ByVal sLoginName As String) As String
        Dim strLoginUserName As String = ""
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        'Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.Text
        objCmd.CommandText = "Select ISNULL(sFirstName,'') + ' ' + ISNULL(sMiddleName,'') + ' ' + ISNULL(sLastName,'') AS LoginName From User_MST Where sLoginName = '" & sLoginName & "'"
        objCmd.Connection = objCon

        'Dim objParaNickName As New SqlParameter
        'With objParaNickName
        '    .ParameterName = "@NickName"
        '    .Value = sLoginName
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.VarChar
        'End With
        'objCmd.Parameters.Add(objParaNickName)


        objCon.Open()
        strLoginUserName = objCmd.ExecuteScalar
        objCon.Close()
        If IsNothing(strLoginUserName) Then
            strLoginUserName = ""
        End If
        objCmd.Parameters.Clear()
        objCmd.Dispose()
        objCmd = Nothing
        objCon.Dispose()
        objCon = Nothing
        Return strLoginUserName
    End Function
#End Region






End Class
