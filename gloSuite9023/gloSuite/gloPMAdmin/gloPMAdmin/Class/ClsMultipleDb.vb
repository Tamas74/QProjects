Imports System.Data.SqlClient
Imports System.IO


Public Class ClsMultipleDb

#Region "Variable Declaration"

    ''Added ServicesDatabaseName by Ujwala on 23022015 to get ServicesDB Name from settings table instead of Hardcoding
    ''dhruv 20091202 Hardcorded the Service DatabaseName
    '' Dim strgloServiceDatabaseName As String = "gloServices"
    ' Dim strgloServiceDatabaseName As String = gstrServicesDBName
    ''Added ServicesDatabaseName by Ujwala on 23022015 to get ServicesDB Name from settings table instead of Hardcoding

    Dim _nDBConnectionId As Int64
    Dim _sDatabaseName As String
    Dim _sServerName As String
    Dim _sSqlUserName As String
    Dim _sSqlPassword As String
    Dim _sServiceName As String
    Dim _bEnabled As Boolean
    Dim _bIsConnected As Boolean
    Dim _DatabaseID As Int64
    Dim _str As String
    Dim _isDefaultDatabase As Boolean
#End Region

#Region "Public Property"
    Public Property isDefaultDatabase() As Boolean
        Get
            Return _isDefaultDatabase
        End Get
        Set(ByVal Value As Boolean)
            _isDefaultDatabase = Value
        End Set
    End Property
    Public Property DBConnectionId() As Int64
        Get
            Return _nDBConnectionId
        End Get
        Set(ByVal Value As Int64)
            _nDBConnectionId = Value
        End Set
    End Property

    Public Property DatabaseNames() As String
        Get
            Return _sDatabaseName
        End Get
        Set(ByVal Value As String)
            _sDatabaseName = Value
        End Set
    End Property


    Public Property str() As String
        Get
            Return _str
        End Get
        Set(ByVal Value As String)
            _str = Value
        End Set
    End Property

    Public Property ServerNames() As String
        Get
            Return _sServerName
        End Get
        Set(ByVal Value As String)
            _sServerName = Value
        End Set
    End Property

    Public Property SqlUserName() As String
        Get
            Return _sSqlUserName
        End Get
        Set(ByVal Value As String)
            _sSqlUserName = Value
        End Set
    End Property

    Public Property SqlPasswords() As String
        Get
            Return _sSqlPassword
        End Get
        Set(ByVal Value As String)
            _sSqlPassword = Value
        End Set
    End Property


    Public Property ServiceName() As String
        Get
            Return _sServiceName
        End Get
        Set(ByVal Value As String)
            _sServiceName = Value
        End Set
    End Property

    Public Property Enabled() As Boolean
        Get
            Return _bEnabled
        End Get
        Set(ByVal Value As Boolean)
            _bEnabled = Value
        End Set
    End Property

    Public Property IsConnected() As Boolean
        Get
            Return _bIsConnected
        End Get
        Set(ByVal Value As Boolean)
            _bIsConnected = Value
        End Set
    End Property

    Public Property DatabaseID() As Int64
        Get
            Return _DatabaseID
        End Get
        Set(ByVal value As Int64)
            _DatabaseID = value
        End Set
    End Property

#End Region


#Region "To fill the Database"
    Public Function Fill_gloServiceServerNameSP() As DataTable

        Dim con As SqlConnection = Nothing
        Dim sqlCommand As SqlCommand = Nothing
        Dim ad As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing

        Try
            con = New SqlConnection()
            con.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(gstrServicesServerName, gstrServicesDBName, gbServicesIsSQLAUTHEN, gstrServicesUserID, gstrServicesPassWord)

            If Not IsNothing(con) Then


                Dim _sqlParameter As SqlParameter = Nothing

                sqlCommand = New SqlCommand()
                sqlCommand.Connection = con
                sqlCommand.CommandTimeout = 0
                sqlCommand.CommandType = CommandType.StoredProcedure
                sqlCommand.CommandText = "gsp_GetMultipleDbServerNames"

                _sqlParameter = New SqlParameter()
                _sqlParameter.ParameterName = "@ServiceName"
                _sqlParameter.Value = "gloEMR"
                _sqlParameter.Direction = ParameterDirection.Input
                _sqlParameter.SqlDbType = SqlDbType.VarChar
                _sqlParameter.Size = 150

                sqlCommand.Parameters.Add(_sqlParameter)
                _sqlParameter = Nothing

                _sqlParameter = New SqlParameter()
                _sqlParameter.ParameterName = "@MachineName"
                _sqlParameter.Value = gloAuditTrail.MachineDetails.LocalMachineDetails().MachineName
                _sqlParameter.Direction = ParameterDirection.Input
                _sqlParameter.SqlDbType = SqlDbType.VarChar
                _sqlParameter.Size = 150

                sqlCommand.Parameters.Add(_sqlParameter)
                _sqlParameter = Nothing

                ad = New SqlDataAdapter(sqlCommand)

                dt = New DataTable()
                ad.Fill(dt)

            End If


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
        Finally

            If Not IsNothing(ad) Then
                ad.Dispose()
                ad = Nothing
            End If

            If Not IsNothing(sqlCommand) Then
                If Not IsNothing(sqlCommand.Parameters) Then
                    sqlCommand.Parameters.Clear()
                End If
                sqlCommand.Dispose()
                sqlCommand = Nothing
            End If

            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If

        End Try

        Return dt
    End Function

    Public Function GetDistinctServiceDatabaseName(ByVal sServerName As String) As DataTable
        ''Variable Declaration
        Dim con As New SqlConnection
        ''getting the connection string
        con.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(gstrServicesServerName, gstrServicesDBName, gbServicesIsSQLAUTHEN, gstrServicesUserID, gstrServicesPassWord)
        Dim ad As SqlDataAdapter
        Dim dt As New DataTable
        Dim _query As String
        Dim dr As DataRow
        Try
            ''Storing the Query into the String
            '''''''Added on 20100701 by sanjog to show UserName and password 
            _query = "SELECT DISTINCT ISNULL(sDatabaseName,'') as [Database Name] " _
                & " FROM DBSettings" _
                & " WHERE sServerName ='" & sServerName & "'"
            '''''''Added on 20100701 by sanjog to show UserName and password 
            ''Passing the Value to the sqlDataAdapter   
            ad = New SqlDataAdapter(_query, con)
            ''filling the data into the datatable
            ad.Fill(dt)
            Return dt
        Catch ex As Exception

        End Try
    End Function
    Public Function GetServiceDatabaseName(ByVal sServerName As String) As DataTable

        Dim con As SqlConnection = Nothing
        Dim ad As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Dim sqlCommand As SqlCommand = Nothing

        Try

            con = New SqlConnection()
            con.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(gstrServicesServerName, gstrServicesDBName, gbServicesIsSQLAUTHEN, gstrServicesUserID, gstrServicesPassWord)

            dt = New DataTable()

            If Not IsNothing(con) Then

                Dim _sqlParameter As SqlParameter = Nothing

                SqlCommand = New SqlCommand()
                SqlCommand.Connection = con
                SqlCommand.CommandTimeout = 0
                SqlCommand.CommandType = CommandType.StoredProcedure
                SqlCommand.CommandText = "gsp_GetMultipleDatabases"

                _sqlParameter = New SqlParameter()
                _sqlParameter.ParameterName = "@ServiceName"
                _sqlParameter.Value = "gloEMR"
                _sqlParameter.Direction = ParameterDirection.Input
                _sqlParameter.SqlDbType = SqlDbType.VarChar
                _sqlParameter.Size = 150

                SqlCommand.Parameters.Add(_sqlParameter)
                _sqlParameter = Nothing

                _sqlParameter = New SqlParameter()
                _sqlParameter.ParameterName = "@MachineName"
                _sqlParameter.Value = gloAuditTrail.MachineDetails.LocalMachineDetails().MachineName
                _sqlParameter.Direction = ParameterDirection.Input
                _sqlParameter.SqlDbType = SqlDbType.VarChar
                _sqlParameter.Size = 150

                SqlCommand.Parameters.Add(_sqlParameter)
                _sqlParameter = Nothing

                _sqlParameter = New SqlParameter()
                _sqlParameter.ParameterName = "@IsForAdminList"
                _sqlParameter.Value = True
                _sqlParameter.Direction = ParameterDirection.Input
                _sqlParameter.SqlDbType = SqlDbType.Bit

                sqlCommand.Parameters.Add(_sqlParameter)
                _sqlParameter = Nothing

                _sqlParameter = New SqlParameter()
                _sqlParameter.ParameterName = "@ServerName"
                _sqlParameter.Value = Convert.ToString(sServerName)
                _sqlParameter.Direction = ParameterDirection.Input
                _sqlParameter.SqlDbType = SqlDbType.VarChar
                _sqlParameter.DbType = DbType.String
                _sqlParameter.Size = 250

                sqlCommand.Parameters.Add(_sqlParameter)
                _sqlParameter = Nothing

                ad = New SqlDataAdapter(sqlCommand)

                ad.Fill(dt)

            End If

        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)

        Finally

            If Not IsNothing(ad) Then
                ad.Dispose()
                ad = Nothing
            End If

            If Not IsNothing(sqlCommand) Then
                If Not IsNothing(sqlCommand.Parameters) Then
                    sqlCommand.Parameters.Clear()
                End If
                sqlCommand.Dispose()
                sqlCommand = Nothing
            End If

            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If

        End Try

        Return dt

    End Function
    Public Function DeleteServiceDatabaseName(ByVal nConnectionID As Int64) As Boolean
        ''Variable Declaration
        Dim con As New SqlConnection
        ''getting the connection string
        con.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(gstrServicesServerName, gstrServicesDBName, gbServicesIsSQLAUTHEN, gstrServicesUserID, gstrServicesPassWord)
        ''Creating the new command string
        Dim cmd As SqlCommand
        Dim _query As String
        Try
            ''Deleting the Query into the String
            _query = "DELETE DBSettings WHERE nDBConnectionId = " & nConnectionID & ""
            cmd = New SqlCommand(_query, con)
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            Return True

        Catch ex As Exception
            Return False

        End Try
    End Function
    Public Function IsDatabaseExists(ByVal nID As Int64, ByVal sServer As String, ByVal sDatabase As String) As Boolean
        Dim _Query As String
        Dim con As New SqlClient.SqlConnection
        con.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(gstrServicesServerName, gstrServicesDBName, gbServicesIsSQLAUTHEN, gstrServicesUserID, gstrServicesPassWord)
        Dim cmd As SqlClient.SqlCommand
        Dim _blnResult As Boolean = False
        Dim _strResult As Integer = 0

        Try
            _Query = "SELECT COUNT(*) FROM DBSettings WHERE UPPER(sServerName) = '" & sServer.ToUpper.ToString() & "' AND UPPER(sDatabaseName) = '" & sDatabase.ToUpper.ToString() & "' AND nDBConnectionId <> " & nID & " AND sServiceName = 'gloEMR'"
            cmd = New SqlClient.SqlCommand(_Query, con)

            con.Open()
            _strResult = cmd.ExecuteScalar
            con.Close()
            If _strResult > 0 Then
                _blnResult = True
            End If

            Return _blnResult
        Catch ex As Exception
        Finally
            con = Nothing
        End Try
    End Function
    Public Function IsDefaultSetted(ByVal _isChecked As Boolean) As Boolean
        Dim _Query As String
        Dim con As New SqlClient.SqlConnection
        con.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString(gstrServicesServerName, gstrServicesDBName, gbServicesIsSQLAUTHEN, gstrServicesUserID, gstrServicesPassWord)
        Dim cmd As SqlClient.SqlCommand
        Dim _blnResult As Boolean = False
        Dim _strResult As Integer = 0

        Try
            _Query = "SELECT COUNT(*) FROM DBSettings WHERE bEnabled ='" & _isChecked & "'"
            cmd = New SqlClient.SqlCommand(_Query, con)
            con.Open()
            _strResult = cmd.ExecuteScalar
            con.Close()
            If _strResult > 0 Then
                _blnResult = True
            End If

            Return _blnResult
        Catch ex As Exception
        Finally
            con = Nothing
        End Try
    End Function

    Public Function saveDataBaseCredentials(ByVal oDatabase As ClsMultipleDb) As Int64
        Dim oDbParameter As gloDatabaseLayer.DBParameters
        Dim oDbLayer As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString(gstrServicesServerName, gstrServicesDBName, gbServicesIsSQLAUTHEN, gstrServicesUserID, gstrServicesPassWord))

        If oDbLayer.CheckConnection = False Then
            MessageBox.Show("Please make sure that gloServices database is present on the server.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Function
        End If
        ''If the registry setting is not proper or not setting then it will not do any of the operation
        If gstrSQLServerName = Nothing Then
            MessageBox.Show("Registry setting is not proper", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Function
        Else
            oDbLayer.Connect(False)
        End If



        Dim _DBID As Object
        Dim _Query As String
        Try
            'string strInsertQuery = string.Empty;
            If oDatabase.isDefaultDatabase Then
                _Query = "UPDATE DBSettings SET bEnabled = 'FALSE' WHERE sServiceName = 'gloEMR'"
                oDbLayer.Execute_Query(_Query)
            End If

            If oDatabase.DBConnectionId <= 0 Then
                '' INSERT ''
                Dim oResult As Object
                _Query = "SELECT MAX(ISNULL(nDBConnectionId,0)) + 1 FROM DBSettings"
                oResult = oDbLayer.ExecuteScalar_Query(_Query)
                ''When there is no value into the gloService database then insert it into the database 
                '' and set the value as 1
                If IsDBNull(oResult) Then
                    oResult = 1
                    _Query = "INSERT INTO DBSettings(nDBConnectionId, sDatabaseName, sServerName, sSqlUserName, sSqlPassword, bEnabled, bIsConnected, sServiceName) VALUES( " _
                                         & " " & Convert.ToInt64(oResult) _
                                         & ", '" & oDatabase.DatabaseNames.Replace("'", "''") _
                                         & "', '" & oDatabase.ServerNames.Replace("'", "''") _
                                         & "', '" & oDatabase.SqlUserName.Replace("'", "''") _
                                         & "', '" & oDatabase.SqlPasswords.Replace("'", "''") _
                                         & "', '" & oDatabase.isDefaultDatabase _
                                         & "', '" & True _
                                         & "', 'gloEMR')"
                    oDbLayer.Execute_Query(_Query)
                    Exit Function
                End If

                If oResult IsNot Nothing Then
                    If oResult.ToString <> "" Then
                        _Query = "INSERT INTO DBSettings(nDBConnectionId, sDatabaseName, sServerName, sSqlUserName, sSqlPassword, bEnabled, bIsConnected, sServiceName) VALUES( " _
                        & " " & Convert.ToInt64(oResult) _
                        & ", '" & oDatabase.DatabaseNames.Replace("'", "''") _
                        & "', '" & oDatabase.ServerNames.Replace("'", "''") _
                        & "', '" & oDatabase.SqlUserName.Replace("'", "''") _
                        & "', '" & oDatabase.SqlPasswords.Replace("'", "''") _
                        & "', '" & oDatabase.isDefaultDatabase _
                        & "', '" & True _
                        & "', 'gloEMR')"
                        oDbLayer.Execute_Query(_Query)
                    End If
                End If

            Else
                '' UPDATE ''
                _Query = "UPDATE DBSettings SET " _
                & " sDatabaseName = '" & oDatabase.DatabaseNames.Replace("'", "''") _
                & "', sServerName = '" & oDatabase.ServerNames.Replace("'", "''") _
                & "', sSqlUserName = '" & oDatabase.SqlUserName.Replace("'", "''") _
                & "', sSqlPassword = '" & oDatabase.SqlPasswords.Replace("'", "''") _
                & "', bEnabled = '" & oDatabase.isDefaultDatabase _
                & "', bIsConnected = '" & True _
                & "', sServiceName = 'gloEMR' WHERE nDBConnectionId = " & oDatabase.DBConnectionId & ""
                oDbLayer.Execute_Query(_Query)

                '''Update Bathc Eligibility Service Entry
                _Query = "UPDATE DBSettings SET " _
               & " sSqlUserName = '" & oDatabase.SqlUserName.Replace("'", "''") _
                & "', sSqlPassword = '" & oDatabase.SqlPasswords.Replace("'", "''") _
                 & "' Where sServiceName = 'BatchEligibility' AND sDatabaseName='" & oDatabase.DatabaseNames.Replace("'", "''") & "'AND sServerName ='" & oDatabase.ServerNames.Replace("'", "''") & "'"
                oDbLayer.Execute_Query(_Query)
            End If

            'oDbParameter = New gloDatabaseLayer.DBParameters
            'oDbParameter.Add("@nDBConnectionId", oDatabase.DBConnectionId, ParameterDirection.InputOutput, SqlDbType.BigInt)
            'oDbParameter.Add("@sDatabaseName", oDatabase.DatabaseNames, ParameterDirection.Input, SqlDbType.VarChar)
            'oDbParameter.Add("@sServerName", oDatabase.ServerNames, ParameterDirection.Input, SqlDbType.VarChar)
            'oDbParameter.Add("@sSqlUserName", oDatabase.SqlUserName, ParameterDirection.Input, SqlDbType.VarChar)
            'oDbParameter.Add("@sSqlPassword", oDatabase.SqlPasswords, ParameterDirection.Input, SqlDbType.VarChar)
            'oDbParameter.Add("@bEnabled", oDatabase.isDefaultDatabase, ParameterDirection.Input, SqlDbType.Bit)
            'oDbParameter.Add("@bIsConnected", True, ParameterDirection.Input, SqlDbType.Bit)
            'oDbParameter.Add("@sServiceName", "gloEMR", ParameterDirection.Input, SqlDbType.VarChar)
            'oDbLayer.Execute("gsp_INUP_SERVICEDATABASE", oDbParameter, _DBID)

        Catch ex As Exception
            Throw ex
        Finally
            If (Not (oDbLayer) Is Nothing) Then
                oDbLayer.Disconnect()
                oDbLayer.Dispose()
            End If
        End Try
        If _DBID Is Nothing Then
            _DBID = 0
        End If
        If Not IsDBNull(_DBID) Then
            Return Convert.ToInt64(_DBID)
        End If
    End Function


#End Region


End Class
