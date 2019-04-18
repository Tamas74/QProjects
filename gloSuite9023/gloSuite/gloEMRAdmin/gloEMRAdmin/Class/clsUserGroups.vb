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

Public Class clsUserGroups
    Dim _nGroupID As Integer
    Dim _sGroupName As String
    Dim _clGroupRights As New Collection
    Dim _clUsers As New Collection
    Dim _clUserPasswords As New Collection


    Public Property GroupID() As Integer
        Get
            Return _nGroupID
        End Get
        Set(ByVal Value As Integer)
            _nGroupID = Value
        End Set
    End Property


    Public Property GroupName() As String
        Get
            Return _sGroupName
        End Get
        Set(ByVal Value As String)
            _sGroupName = Value
        End Set
    End Property

    Public Property GroupRights() As Collection
        Get
            Return _clGroupRights
        End Get
        Set(ByVal Value As Collection)
            _clGroupRights = Value
        End Set
    End Property

    Public Property Users() As Collection
        Get
            Return _clUsers
        End Get
        Set(ByVal Value As Collection)
            _clUsers = Value
        End Set
    End Property

    Public Property UserPasswords() As Collection
        Get
            Return _clUserPasswords
        End Get
        Set(ByVal Value As Collection)
            _clUserPasswords = Value
        End Set
    End Property

    Public Function EditUserGroup(ByVal nGroupID As Integer) As Boolean
        Return EditUserGroup(nGroupID, _sGroupName, _clGroupRights)
    End Function


    '25-Oct-13 Aniket: User Group count check
    Public Function CheckUserGroups(ByVal GroupID As Integer) As Integer

        Dim connMain As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim cmdMain As New SqlCommand("gsp_CheckUserGroups", connMain)
        Dim tvpParam As SqlParameter

        Try


            cmdMain.CommandType = CommandType.StoredProcedure

            tvpParam = cmdMain.Parameters.AddWithValue("nGroupID", GroupID)
            tvpParam.SqlDbType = SqlDbType.Int

            connMain.Open()
            Return cmdMain.ExecuteScalar


        Catch ex As Exception
            Throw ex

        Finally
            cmdMain.Dispose()
            cmdMain = Nothing

            connMain.Close()
            connMain.Dispose()
            connMain = Nothing

        End Try

    End Function


    '17-Oct-13 Aniket: User Group permissions change as per UCD testing
    Public Function SaveUserGroups(ByVal dtMain As DataTable) As Integer

        Dim connMain As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim cmdMain As New SqlCommand("gsp_UpdateUserGroupRights", connMain)
        Dim tvpParam As SqlParameter
        Dim nModifiedByUserId As SqlParameter


        Try


            cmdMain.CommandType = CommandType.StoredProcedure

            tvpParam = cmdMain.Parameters.AddWithValue("TVP_UserGroupRights", dtMain)
            tvpParam.SqlDbType = SqlDbType.Structured

            nModifiedByUserId = cmdMain.Parameters.AddWithValue("@nModifiedByUserId", gnLoginID)
            nModifiedByUserId.SqlDbType = SqlDbType.BigInt

            connMain.Open()
            Return cmdMain.ExecuteNonQuery()


        Catch ex As Exception
            Throw ex

        Finally
            cmdMain.Dispose()
            cmdMain = Nothing

            connMain.Close()
            connMain.Dispose()
            connMain = Nothing

        End Try

    End Function

    Public Function EditUserGroup(ByVal nGroupID As Integer, ByVal strGroupName As String, ByVal clGroupRights As Collection) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_UpdateGroup"

        Dim objParaGroupID As New SqlParameter
        With objParaGroupID
            .ParameterName = "@GroupID"
            .Direction = ParameterDirection.Input
            .Value = nGroupID
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaGroupID)

        Dim objParaGroupName As New SqlParameter
        With objParaGroupName
            .ParameterName = "@GroupName"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
            .Value = strGroupName
        End With
        objCmd.Parameters.Add(objParaGroupName)

        objCmd.Connection = objCon
        objCon.Open()
        objCmd.ExecuteNonQuery()

        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_DeleteGroupRights"
        objCmd.Parameters.Clear()
        objCmd.Parameters.Add(objParaGroupID)
        objCmd.Connection = objCon
        objCmd.ExecuteNonQuery()


        Dim nCount As Integer
        For nCount = 1 To clGroupRights.Count
            Dim objcmdGroupRights As New SqlCommand
            objcmdGroupRights.CommandType = CommandType.StoredProcedure
            objcmdGroupRights.CommandText = "gsp_InsertGroupsRights"
            Dim objParaRightsGroupID As New SqlParameter
            With objParaRightsGroupID
                .ParameterName = "@GroupID"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
                .Value = nGroupID
            End With
            objcmdGroupRights.Parameters.Add(objParaRightsGroupID)

            Dim objParaRightsGroupName As New SqlParameter
            With objParaRightsGroupName
                .ParameterName = "@RightsID"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
                .Value = clGroupRights.Item(nCount)
            End With
            objcmdGroupRights.Parameters.Add(objParaRightsGroupName)
            objcmdGroupRights.Connection = objCon
            objcmdGroupRights.ExecuteNonQuery()

            objcmdGroupRights = Nothing
            objParaRightsGroupID = Nothing
            objParaRightsGroupName = Nothing
        Next
        For nCount = 1 To _clUsers.Count
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InsertUser"
            objCmd.Parameters.Clear()
            Dim objParaUserID As New SqlParameter
            With objParaUserID
                .ParameterName = "@UserID"
                .Direction = ParameterDirection.Output
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaUserID)

            Dim objParaUserName As New SqlParameter
            With objParaUserName
                .ParameterName = "@LoginName"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
                .Value = _clUsers.Item(nCount)
            End With
            objCmd.Parameters.Add(objParaUserName)

            Dim objParaPassword As New SqlParameter
            With objParaPassword
                .ParameterName = "@Password"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
                Dim objEncryption As New clsEncryption
                .Value = objEncryption.EncryptToBase64String(_clUserPasswords.Item(nCount), constEncryptDecryptKey)
                objEncryption = Nothing
            End With

            objCmd.Parameters.Add(objParaPassword)
            objCmd.Connection = objCon
            objCmd.ExecuteNonQuery()

            'Add User Group
            Dim objCmdGroup As New SqlCommand
            objCmdGroup.CommandType = CommandType.StoredProcedure
            objCmdGroup.CommandText = "gsp_InsertUserGroups"
            objCmdGroup.Parameters.Clear()
            Dim objParaUserGroupID As New SqlParameter
            With objParaUserGroupID
                .ParameterName = "@UserID"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
                .Value = objParaUserID.Value
            End With
            objCmdGroup.Parameters.Add(objParaUserGroupID)

            Dim objParaUserGroupName As New SqlParameter
            With objParaUserGroupName
                .ParameterName = "@GroupName"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
                .Value = strGroupName
            End With
            objCmdGroup.Parameters.Add(objParaUserGroupName)
            objCmdGroup.Connection = objCon
            objCmdGroup.ExecuteNonQuery()

            Dim nCount1 As Int16
            For nCount1 = 1 To _clGroupRights.Count
                Dim objCmd1 As New SqlCommand
                objCmd1.Connection = objCon
                objCmd1.CommandType = CommandType.StoredProcedure
                objCmd1.CommandText = "gsp_InsertUserRights"

                Dim objParaUserRightsID As New SqlParameter
                With objParaUserRightsID
                    .ParameterName = "@UserID"
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.Int
                    .Value = objParaUserID.Value
                End With
                objCmd1.Parameters.Add(objParaUserRightsID)

                Dim objParaRightsName As New SqlParameter
                With objParaRightsName
                    .ParameterName = "@RightsID"
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.BigInt
                    .Value = _clGroupRights.Item(nCount1)
                End With
                objCmd1.Parameters.Add(objParaRightsName)
                objCmd1.ExecuteNonQuery()
                objCmd1 = Nothing
                objParaUserRightsID = Nothing
                objParaRightsName = Nothing
            Next
            objParaUserID = Nothing
            objParaUserName = Nothing
            objParaPassword = Nothing
        Next
        objCon.Close()
        objCon = Nothing
        objCmd = Nothing
        Return True
    End Function

    Public Function AddUserGroups() As Boolean
        Return AddUserGroups(_sGroupName, _clGroupRights)
    End Function
    Public Function AddUserGroups(ByVal strGroupName As String, ByVal clGroupRights As Collection) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_InsertGroup"

        Dim objParaGroupID As New SqlParameter
        With objParaGroupID
            .ParameterName = "@GroupID"
            .Direction = ParameterDirection.Output
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaGroupID)

        Dim objParaGroupName As New SqlParameter
        With objParaGroupName
            .ParameterName = "@GroupName"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
            .Value = strGroupName
        End With
        objCmd.Parameters.Add(objParaGroupName)

        objCmd.Connection = objCon
        objCon.Open()
        objCmd.ExecuteNonQuery()

        Dim nCount As Integer
        For nCount = 1 To clGroupRights.Count
            Dim objcmdGroupRights As New SqlCommand
            objcmdGroupRights.CommandType = CommandType.StoredProcedure
            objcmdGroupRights.CommandText = "gsp_InsertGroupsRights"
            Dim objParaRightsGroupID As New SqlParameter
            With objParaRightsGroupID
                .ParameterName = "@GroupID"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
                .Value = objParaGroupID.Value
            End With
            objcmdGroupRights.Parameters.Add(objParaRightsGroupID)

            Dim objParaRightsGroupName As New SqlParameter
            With objParaRightsGroupName
                .ParameterName = "@RightsID"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
                .Value = clGroupRights.Item(nCount)
            End With
            objcmdGroupRights.Parameters.Add(objParaRightsGroupName)

            objcmdGroupRights.Connection = objCon
            objcmdGroupRights.ExecuteNonQuery()

            objcmdGroupRights = Nothing
            objParaRightsGroupID = Nothing
            objParaRightsGroupName = Nothing
        Next
        For nCount = 1 To _clUsers.Count
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InsertUser"
            objCmd.Parameters.Clear()
            Dim objParaUserID As New SqlParameter
            With objParaUserID
                .ParameterName = "@UserID"
                .Direction = ParameterDirection.Output
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaUserID)

            Dim objParaUserName As New SqlParameter
            With objParaUserName
                .ParameterName = "@LoginName"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
                .Value = _clUsers.Item(nCount)
            End With
            objCmd.Parameters.Add(objParaUserName)

            Dim objParaPassword As New SqlParameter
            With objParaPassword
                .ParameterName = "@Password"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
                Dim objEncryption As New clsEncryption
                .Value = objEncryption.EncryptToBase64String(_clUserPasswords.Item(nCount), constEncryptDecryptKey)
                objEncryption = Nothing
            End With

            objCmd.Parameters.Add(objParaPassword)
            objCmd.Connection = objCon
            objCmd.ExecuteNonQuery()

            'Add User Group
            Dim objCmdGroup As New SqlCommand
            objCmdGroup.CommandType = CommandType.StoredProcedure
            objCmdGroup.CommandText = "gsp_InsertUserGroups"
            objCmdGroup.Parameters.Clear()
            Dim objParaUserGroupID As New SqlParameter
            With objParaUserGroupID
                .ParameterName = "@UserID"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
                .Value = objParaUserID.Value
            End With
            objCmdGroup.Parameters.Add(objParaUserGroupID)

            Dim objParaUserGroupName As New SqlParameter
            With objParaUserGroupName
                .ParameterName = "@GroupName"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
                .Value = strGroupName
            End With
            objCmdGroup.Parameters.Add(objParaUserGroupName)
            objCmdGroup.Connection = objCon
            objCmdGroup.ExecuteNonQuery()

            Dim nCount1 As Int16
            For nCount1 = 1 To _clGroupRights.Count
                Dim objCmd1 As New SqlCommand
                objCmd1.Connection = objCon
                objCmd1.CommandType = CommandType.StoredProcedure
                objCmd1.CommandText = "gsp_InsertUserRights"

                Dim objParaUserRightsID As New SqlParameter
                With objParaUserRightsID
                    .ParameterName = "@UserID"
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.Int
                    .Value = objParaUserID.Value
                End With
                objCmd1.Parameters.Add(objParaUserRightsID)

                Dim objParaRightsName As New SqlParameter
                With objParaRightsName
                    .ParameterName = "@RightsName"
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                    .Value = _clGroupRights.Item(nCount1)
                End With
                objCmd1.Parameters.Add(objParaRightsName)
                objCmd1.ExecuteNonQuery()
                objCmd1 = Nothing
                objParaUserRightsID = Nothing
                objParaRightsName = Nothing
            Next
            objParaUserID = Nothing
            objParaUserName = Nothing
            objParaPassword = Nothing

        Next
        objCon.Close()
        objCon = Nothing
        objCmd = Nothing
        Return True
    End Function


    Public Function PopulateUserGroups() As Collection
        Dim clUserGroups As New Collection
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_RetrieveGroups"
        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader()
        If objSQLDataReader.HasRows = True Then
            While objSQLDataReader.Read
                clUserGroups.Add(objSQLDataReader.Item(0))
            End While
        End If
        objCon.Close()
        objCon = Nothing
        objCmd = Nothing
        objSQLDataReader = Nothing
        Return clUserGroups
    End Function
    Public Function PopulateUserGroupsRights(ByVal strGroupName As String) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        'sarika 25th june 07
        'Dim objSQLDataReader As SqlDataReader
        '---
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_RetrieveGroupsRights"
        objCmd.Connection = objCon
        Dim objParaGroupName As New SqlParameter
        With objParaGroupName
            .ParameterName = "@GroupName"
            .Value = strGroupName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaGroupName)
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objCon.Close()
        objCon = Nothing
        Return dsData.Tables(0)
    End Function
    Public Function DeleteUserGroup(ByVal strGroupName As String) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_DeleteGroup"
        objCmd.Connection = objCon
        Dim objParaGroupName As New SqlParameter
        With objParaGroupName
            .ParameterName = "@GroupName"
            .Value = strGroupName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaGroupName)
        objCmd.Connection = objCon
        objCon.Open()
        objCmd.ExecuteNonQuery()
        objCon.Close()
        objCon = Nothing
        Return True
    End Function

    Public Sub SearchGroup(ByVal strGroupName As String)
        _sGroupName = strGroupName
        'Clear the Rights
        Dim nCount As Int16
        For nCount = _clGroupRights.Count To 1 Step -1
            _clGroupRights.Remove(nCount)
        Next

        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        Dim objSQLDataReader As SqlDataReader
        Dim objParaProviderName As New SqlParameter
        With objParaProviderName
            .ParameterName = "@GroupName"
            .Value = strGroupName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaProviderName)
        objCmd.CommandText = "gsp_RetrieveGroupID"
        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader()
        If objSQLDataReader.HasRows = True Then
            objSQLDataReader.Read()
            _nGroupID = objSQLDataReader.Item(0)
        End If
        objSQLDataReader.Close()

        'objCmd.CommandText = "gsp_RetrieveGroupsRights"
        ''Sandip Darade  20090818
        ''Retrievr rightsIDs in place of ight names
        objCmd.CommandType = CommandType.Text
        Dim _sql As String = " SELECT Rights_MST.nRightsID AS RightsID FROM GroupsRights_DTL LEFT OUTER JOIN " _
                    & " Rights_MST ON GroupsRights_DTL.nRightsID = Rights_MST.nRightsID RIGHT OUTER JOIN  " _
                    & " Groups_MST ON GroupsRights_DTL.nGroupID = Groups_MST.nGroupID WHERE Groups_MST.sGroupName ='" & strGroupName.Replace("'", "''") & "'"
        objCmd.CommandText = _sql
        objSQLDataReader = objCmd.ExecuteReader()
        While objSQLDataReader.Read()
            _clGroupRights.Add(objSQLDataReader.Item(0))
        End While
        objCon.Close()
        objCon = Nothing
    End Sub

    Public Function CheckGroupExists(ByVal strGroupName As String, Optional ByVal nGroupID As Integer = 0) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_CheckGroupExists"
        Dim objParaProviderName As New SqlParameter
        With objParaProviderName
            .ParameterName = "@GroupName"
            .Value = strGroupName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaProviderName)

        If nGroupID <> 0 Then
            Dim objParaGroupID As New SqlParameter
            With objParaGroupID
                .ParameterName = "@GroupID"
                .Value = nGroupID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaGroupID)
        End If
        objCmd.Connection = objCon
        Dim nCount As Integer
        objCon.Open()
        nCount = objCmd.ExecuteScalar
        objCon.Close()
        objCon = Nothing
        If nCount = 0 Then
            Return False
        Else
            Return True
        End If

    End Function
    'Developer: Mitesh Patel
    'Date:26-Dec-2011'
    'PRD: Lab Usability Admin Setting
    Public Function Check_LabUserTask(ByVal _nGroupID As String, ByVal _nUserID As String) As DataTable
        Dim oDataTable As New DataTable
        Dim _SQLQuery As String
        Dim oDB As gloStream.gloDataBase.gloDataBase
        Try

            _SQLQuery = "SELECT   Distinct  LabUserTaskDetail.nProviderID, User_MST.sLoginName AS sUserName , ISNULL(Provider_MST.sFirstName, '') + SPACE(1) + ISNULL(dbo.Provider_MST.sMiddleName, '') + SPACE(1)  " & _
                        "  + ISNULL(Provider_MST.sLastName, '') AS sProviderName,LabUserTaskDetail.nUserID,LabUserTaskDetail.nGroupID " & _
                        " FROM   LabUserTaskDetail INNER JOIN    Provider_MST ON LabUserTaskDetail.nProviderID = Provider_MST.nProviderID INNER JOIN    User_MST ON LabUserTaskDetail.nUserID = User_MST.nUserID " & _
            "  WHERE LabUserTaskDetail.nUserID in  (" & _nUserID & ") And LabUserTaskDetail.nGroupID ='" & _nGroupID & "'"
            oDB = New gloStream.gloDataBase.gloDataBase
            oDB.Connect(gloEMRAdmin.mdlGeneral.GetConnectionString)
            oDataTable = oDB.ReadQueryData(_SQLQuery)


            Return oDataTable
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then
                oDB = Nothing
            End If
            If Not IsNothing(oDataTable) Then
                oDataTable.Dispose()
                oDataTable = Nothing
            End If
        End Try

    End Function
    'Developer: Mitesh Patel
    'Date:26-Dec-2011'
    'PRD: Lab Usability Admin Setting

    Public Function Delete_LabuserTasks(ByVal _nGroupID As String, ByVal _nUserID As String) As Boolean
        Dim odb As gloDatabaseLayer.DBLayer
        Try
            odb = New gloDatabaseLayer.DBLayer(gloEMRAdmin.mdlGeneral.GetConnectionString())
            odb.Connect(False)
            Dim sqry As String = "Delete from LabUserTaskDetail where nGroupId='" & _nGroupID & "'  And nUserID in (" & _nUserID & ")"
            odb.Execute_Query(sqry)
            odb.Disconnect()
            ' strUserID = String.Empty
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        Finally
            If Not IsNothing(odb) Then
                odb.Dispose()
                odb = Nothing
            End If
        End Try
    End Function
#Region " Methods Added for User groups"

    'sandip darade 7th Feb 09
    Public Function GetUserGroups() As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim strQry As String = ""
        Dim dt As New DataTable

        Try
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            strQry = "SELECT  ISNULL(nGroupID,0) AS nGroupID, ISNULL(sGroupName,'') AS sGroupName FROM SEC_UserGroups_MST WHERE nType= 0 " 'AND nClinicID = " + _ClinicID;
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = strQry

            objCmd.Connection = objCon
            Dim da As SqlDataAdapter
            objCon.Open()
            da = New SqlDataAdapter
            dt = New DataTable
            da.SelectCommand = objCmd
            da.Fill(dt)


        Catch ex As Exception
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try

        Return dt
    End Function
    Public Function GetGroupUsers(ByVal GroupID As Int64) As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim strQry As String = ""
        Dim dt As New DataTable

        Try
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            strQry = "SELECT  ISNULL(sUserName,'') AS Users ,nUserID  FROM SEC_UserGroups_DTL WHERE nGroupID=" & GroupID & "" 'AND nClinicID = " + _ClinicID;
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = strQry

            objCmd.Connection = objCon
            Dim da As SqlDataAdapter
            objCon.Open()
            da = New SqlDataAdapter
            dt = New DataTable
            da.SelectCommand = objCmd
            da.Fill(dt)


        Catch ex As Exception
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try

        Return dt
    End Function

    Public Sub DeleteGroup(ByVal GroupID As Int64)
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Try
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            objCon.Open()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "DELETE FROM SEC_UserGroups_MST WHERE nGroupID = " & GroupID & ""
            objCmd.Connection = objCon
            objCmd.ExecuteNonQuery()
            objCmd.CommandText = "DELETE FROM SEC_UserGroups_DTL WHERE nGroupID = " & GroupID & ""
            objCmd.Connection = objCon
            objCmd.ExecuteNonQuery()
        Catch ex As SqlException
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
        Catch ex As Exception
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
        Finally
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Sub


#End Region


End Class

