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
Public Class clsAudit

    Enum enmActivityType
        Add
        Modify
        Delete
        Login
        Logout
        Other
        'RecordAdded
        'RecordModified
        'Delete
        RecordViewed
        UserBlocked
        UserUnBlocked
        SecurityAdmin
        ResetPassword
        NodeAuthenticationFailure
        Query
        ICD10DOSUpdate
        ICD10DOSInsert
    End Enum

    Enum enmOutcome
        Success
        Failure
    End Enum

#Region " Private Variables"
    Dim _nAuditTrailID As Int64
    Dim _dtActivityDate As Date
    Dim _sActivityCategory As String
    Dim _sDescription As String
    Dim _nPatientID As Int64
    Dim _nUserID As Int64
    Dim _sSoftwareComponent As String
#End Region

#Region " Public Properties"
    Public ReadOnly Property AuditTrailID() As Int64
        Get
            Return _nAuditTrailID
        End Get
    End Property
    Public ReadOnly Property ActivityDate() As Date
        Get
            Return _dtActivityDate
        End Get
    End Property
    Public Property ActivityCategory() As String
        Get
            Return _sActivityCategory
        End Get
        Set(ByVal Value As String)
            _sActivityCategory = Value
        End Set
    End Property
    Public Property Description() As String
        Get
            Return _sDescription
        End Get
        Set(ByVal Value As String)
            _sDescription = Value
        End Set
    End Property
    Public Property PatientID() As Long
        Get
            Return _nPatientID
        End Get
        Set(ByVal Value As Long)
            _nPatientID = Value
        End Set
    End Property
    Public Property UserID() As Int64
        Get
            Return _nUserID
        End Get
        Set(ByVal Value As Int64)
            _nUserID = Value
        End Set
    End Property

    Public Property SoftwareComponent() As String
        Get
            Return _sSoftwareComponent
        End Get
        Set(ByVal Value As String)
            _sSoftwareComponent = Value
        End Set
    End Property
#End Region

#Region " Public functions"
    Public Function Fill_AuditCategory() As Collection
        Dim clUsers As New Collection
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader

        Try
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_FillAuditCategory"
            objCmd.Connection = objCon
            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader()
            If objSQLDataReader.HasRows = True Then
                While objSQLDataReader.Read
                    clUsers.Add(objSQLDataReader.Item(0))
                End While
            End If
            objSQLDataReader.Close()
            objCon.Close()
            'objCon = Nothing
            'objCmd = Nothing
            ''Sandip Darade 20091117
            If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
                objCmd.Dispose()
                objCon.Dispose()
            End If
            objSQLDataReader = Nothing

            Return clUsers
        Catch ex As Exception
            'sarika 25th june 07
            Return Nothing
            '---
            MessageBox.Show(ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Function
    Public Function Fill_ArchivedAuditCategory() As Collection
        Dim clUsers As New Collection
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetArchiveConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader

        Try
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_FillAuditCategory"
            objCmd.Connection = objCon
            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader()
            If objSQLDataReader.HasRows = True Then
                While objSQLDataReader.Read
                    clUsers.Add(objSQLDataReader.Item(0))
                End While
            End If
            objSQLDataReader.Close()
            objCon.Close()
            'objCon = Nothing
            'objCmd = Nothing
            ''Sandip Darade 20091117
            If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
                objCmd.Dispose()
                objCon.Dispose()
            End If
            objSQLDataReader = Nothing

            Return clUsers
        Catch ex As Exception
            'sarika 25th june 07
            Return Nothing
            '---
            MessageBox.Show(ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function
    Public Function Fill_Users() As Collection
        Dim clUsers As New Collection
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader

        Try
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_FillUsers"
            objCmd.Connection = objCon
            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader()
            If objSQLDataReader.HasRows = True Then
                While objSQLDataReader.Read
                    clUsers.Add(objSQLDataReader.Item(0))
                End While
            End If
            objSQLDataReader.Close()
            objCon.Close()
            'objCon = Nothing
            'objCmd = Nothing
            ''Sandip Darade 20091117
            If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
                objCmd.Dispose()
                objCon.Dispose()
            End If
            objSQLDataReader = Nothing

            Return clUsers
        Catch ex As Exception
            'sarika 25th june 07
            Return Nothing
            '----
            MessageBox.Show(ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function
    Public Function Fill_ArchivedUsers() As DataTable
        Dim dtUsers As New DataTable("UserDetails")
        Dim clmnUserID As New DataColumn("UserID")
        Dim clmnUserName As New DataColumn("UserName")

        Try
            dtUsers.Columns.Add(clmnUserID)
            dtUsers.Columns.Add(clmnUserName)

            Dim clUsersID As New Collection
            Dim objArchivedCon As New SqlConnection
            objArchivedCon.ConnectionString = GetArchiveConnectionString()
            Dim objArchivedCmd As New SqlCommand
            Dim objArchivedSQLDataReader As SqlDataReader
            objArchivedCmd.CommandType = CommandType.StoredProcedure
            objArchivedCmd.CommandText = "gsp_FillArchivedUsers"

            objArchivedCmd.Connection = objArchivedCon
            Try
                objArchivedCon.Open()
            Catch ex As Exception
                MessageBox.Show("Please set the archive database from settings and try again.  ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return Nothing
            End Try

            objArchivedSQLDataReader = objArchivedCmd.ExecuteReader()
            If objArchivedSQLDataReader.HasRows = True Then
                While objArchivedSQLDataReader.Read
                    clUsersID.Add(objArchivedSQLDataReader.Item(0))
                End While
            End If
            objArchivedSQLDataReader.Close()
            objArchivedCon.Close()
            objArchivedCon = Nothing
            objArchivedCmd = Nothing
            objArchivedSQLDataReader = Nothing


            Dim clUsers As New Collection
            Dim objCon As New SqlConnection
            objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
            Dim objCmd As New SqlCommand
            Dim objSQLDataReader As SqlDataReader
            Dim nCount As Int16
            Dim dr As DataRow
            objCon.Open()

            For nCount = 1 To clUsersID.Count
                objCmd.Parameters.Clear()
                objCmd.CommandType = CommandType.StoredProcedure
                objCmd.CommandText = "gsp_RetrieveUser"
                objCmd.Connection = objCon
                Dim objParaUserID As New SqlParameter
                With objParaUserID
                    .ParameterName = "@UserID"
                    .Value = clUsersID.Item(nCount)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.BigInt
                End With
                objCmd.Parameters.Add(objParaUserID)
                objSQLDataReader = objCmd.ExecuteReader()
                If objSQLDataReader.HasRows = True Then
                    objSQLDataReader.Read()
                    dr = dtUsers.NewRow
                    dr(0) = clUsersID.Item(nCount)
                    dr(1) = objSQLDataReader.Item(1)
                    dtUsers.Rows.Add(dr)
                End If
                objSQLDataReader.Close()
            Next
            objCon.Close()
            'objCon = Nothing
            'objCmd = Nothing
            ''Sandip Darade 20091117
            If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
                objCmd.Dispose()
                objCon.Dispose()
            End If
            objSQLDataReader = Nothing

            Return dtUsers
        Catch ex As Exception
            'sarika 25th june 07
            Return Nothing
            '----
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Function

    Public Function ArchiveAuditReports() As Boolean
        Dim dsAuditReports As New DataSet
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        ' Dim objSQLDataReader As SqlDataReader

        Try
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ScanAllAuditTrails"
            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            objDA.Fill(dsAuditReports)

            Dim objConArchive As New SqlConnection
            objConArchive.ConnectionString = GetArchiveConnectionString()
            Dim objCmdArchive As New SqlCommand
            objCmdArchive.CommandType = CommandType.StoredProcedure
            objCmdArchive.CommandText = "gsp_InsertArchiveAuditTrail"
            objConArchive.Open()
            Dim nCount As Integer
            For nCount = 0 To dsAuditReports.Tables(0).Rows.Count - 1
                objCmdArchive.Parameters.Clear()
                Dim objParaArchiveDateTime As New SqlParameter
                With objParaArchiveDateTime
                    .ParameterName = "@ActivityDateTime"
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.DateTime
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(1)
                End With
                objCmdArchive.Parameters.Add(objParaArchiveDateTime)

                Dim objParaArchiveCategory As New SqlParameter
                With objParaArchiveCategory
                    .ParameterName = "@ActivityCategory"
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(2)
                End With
                objCmdArchive.Parameters.Add(objParaArchiveCategory)

                Dim objParaArchiveDescription As New SqlParameter
                With objParaArchiveDescription
                    .ParameterName = "@Description"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(3)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmdArchive.Parameters.Add(objParaArchiveDescription)

                Dim objParaArchivePatientID As New SqlParameter
                With objParaArchivePatientID
                    .ParameterName = "@PatientID"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(4)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.BigInt
                End With
                objCmdArchive.Parameters.Add(objParaArchivePatientID)

                Dim objParaArchiveUserID As New SqlParameter
                With objParaArchiveUserID
                    .ParameterName = "@UserID"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(5)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.BigInt
                End With
                objCmdArchive.Parameters.Add(objParaArchiveUserID)

                Dim objParaArchiveMachine As New SqlParameter
                With objParaArchiveMachine
                    .ParameterName = "@MachineName"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(6)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmdArchive.Parameters.Add(objParaArchiveMachine)

                'sarika 27th apr 2007
                Dim objParaArchiveSoftwareComponent As New SqlParameter
                With objParaArchiveSoftwareComponent
                    .ParameterName = "@SoftwareComponent"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(7)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmdArchive.Parameters.Add(objParaArchiveSoftwareComponent)


                Dim objParaArchiveOutcome As New SqlParameter
                With objParaArchiveOutcome
                    .ParameterName = "@Outcome"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(8)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmdArchive.Parameters.Add(objParaArchiveOutcome)


                objCmdArchive.Connection = objConArchive
                objCmdArchive.ExecuteNonQuery()


            Next
            objCmd.Parameters.Clear()
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_DeleteAuditTrail"
            objCmd.Connection = objCon
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objConArchive.Close()
            'objCon = Nothing
            'objConArchive = Nothing
            ''Sandip Darade 20091117
            If Not IsNothing(objCmd) And Not IsNothing(objCon) And Not IsNothing(objCmdArchive) And Not IsNothing(objConArchive) Then
                objCmd.Dispose()
                objCon.Dispose()
                objCmdArchive.Dispose()
                objConArchive.Dispose()
            End If

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Function

    Public Function RestoreArchiveAuditReports() As Boolean
        Dim dsAuditReports As New DataSet
        Dim objConArchive As New SqlConnection
        objConArchive.ConnectionString = GetArchiveConnectionString()
        Dim objCmdArchive As New SqlCommand
        'sarika 25th june 07
        'Dim objSQLDataReader As SqlDataReader
        '--

        Try
            objCmdArchive.CommandType = CommandType.StoredProcedure
            objCmdArchive.CommandText = "gsp_ScanAllArchiveAuditTrails"
            objCmdArchive.Connection = objConArchive
            objConArchive.Open()
            Dim objDA As New SqlDataAdapter(objCmdArchive)
            objDA.Fill(dsAuditReports)

            Dim objCon As New SqlConnection
            objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
            Dim objCmd As New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InsertArchiveAuditTrail"
            objCon.Open()
            Dim nCount As Integer
            For nCount = 0 To dsAuditReports.Tables(0).Rows.Count - 1
                objCmd.Parameters.Clear()
                Dim objParaArchiveDateTime As New SqlParameter
                With objParaArchiveDateTime
                    .ParameterName = "@ActivityDateTime"
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.DateTime
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(1)
                End With
                objCmd.Parameters.Add(objParaArchiveDateTime)


                Dim objParaArchiveCategory As New SqlParameter
                With objParaArchiveCategory
                    .ParameterName = "@ActivityCategory"
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(2)
                End With
                objCmd.Parameters.Add(objParaArchiveCategory)


                Dim objParaArchiveDescription As New SqlParameter
                With objParaArchiveDescription
                    .ParameterName = "@Description"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(3)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objParaArchiveDescription)

                Dim objParaArchivePatientID As New SqlParameter
                With objParaArchivePatientID
                    .ParameterName = "@PatientID"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(4)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.BigInt
                End With
                objCmd.Parameters.Add(objParaArchivePatientID)

                Dim objParaArchiveUserID As New SqlParameter
                With objParaArchiveUserID
                    .ParameterName = "@UserID"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(5)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.BigInt
                End With
                objCmd.Parameters.Add(objParaArchiveUserID)

                Dim objParaArchiveMachine As New SqlParameter
                With objParaArchiveMachine
                    .ParameterName = "@MachineName"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(6)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objParaArchiveMachine)

                'sarika 27th apr 2007
                Dim objParaArchiveSoftwareComponent As New SqlParameter
                With objParaArchiveSoftwareComponent
                    .ParameterName = "@SoftwareComponent"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(7)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objParaArchiveSoftwareComponent)

                Dim objParaArchiveOutcome As New SqlParameter
                With objParaArchiveOutcome
                    .ParameterName = "@OutCome"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(8)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objParaArchiveOutcome)

                objCmd.Connection = objCon
                objCmd.ExecuteNonQuery()

            Next
            objCmdArchive.Parameters.Clear()
            objCmdArchive.CommandType = CommandType.StoredProcedure
            objCmdArchive.CommandText = "gsp_DeleteAuditTrail"
            objCmdArchive.Connection = objConArchive
            objCmdArchive.ExecuteNonQuery()
            objConArchive.Close()
            objConArchive.Close()
            'objCon = Nothing
            'objConArchive = Nothing
            ''Sandip Darade 20091117
            If Not IsNothing(objCmd) And Not IsNothing(objCon) And Not IsNothing(objCmdArchive) And Not IsNothing(objConArchive) Then
                objCmd.Dispose()
                objCon.Dispose()
                objCmdArchive.Dispose()
                objConArchive.Dispose()
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function
    Public Function ArchiveAuditReports(ByVal dtFrom As Date, ByVal dtTo As Date, ByVal sCategory As String, ByVal strUser As String, Optional ByVal nPatientID As Int64 = 0, Optional ByVal strPatientFirstName As String = "", Optional ByVal strPatientLastName As String = "") As Boolean
        Dim dsAuditReports As New DataSet
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        'sarika 25th june 07
        'Dim objSQLDataReader As SqlDataReader
        '----

        Try
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ScanArchiveAuditTrails"
            objCmd.Parameters.Clear()
            Dim objParaFromDate As New SqlParameter
            With objParaFromDate
                .ParameterName = "@FromDate"
                .Value = dtFrom.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFromDate)

            Dim objParaToDate As New SqlParameter
            With objParaToDate
                .ParameterName = "@ToDate"
                .Value = dtTo.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaToDate)

            Dim objParaUser As New SqlParameter
            With objParaUser
                .ParameterName = "@UserName"
                .Value = strUser
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaUser)


            Dim objParaCategory As New SqlParameter
            With objParaCategory
                .ParameterName = "@Category"
                .Value = sCategory
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaCategory)

            If nPatientID <> 0 Then
                Dim objParaPatientID As New SqlParameter
                With objParaPatientID
                    .ParameterName = "@PatientID"
                    .Value = nPatientID
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.BigInt
                End With
                objCmd.Parameters.Add(objParaPatientID)
            End If

            If strPatientFirstName <> "" Then
                Dim objParaPatientFirstName As New SqlParameter
                With objParaPatientFirstName
                    .ParameterName = "@PatientFirstName"
                    .Value = strPatientFirstName
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objParaPatientFirstName)
            End If

            If strPatientLastName <> "" Then
                Dim objParaPatientLastName As New SqlParameter
                With objParaPatientLastName
                    .ParameterName = "@PatientLastName"
                    .Value = strPatientLastName
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objParaPatientLastName)
            End If

            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            objDA.Fill(dsAuditReports)
            Dim objConArchive As New SqlConnection
            objConArchive.ConnectionString = GetArchiveConnectionString()
            Dim objCmdArchive As New SqlCommand
            objCmdArchive.CommandType = CommandType.StoredProcedure
            objCmdArchive.CommandText = "gsp_InsertArchiveAuditTrail"
            objConArchive.Open()
            Dim nCount As Integer
            For nCount = 0 To dsAuditReports.Tables(0).Rows.Count - 1
                objCmdArchive.Parameters.Clear()
                Dim objParaArchiveDateTime As New SqlParameter
                With objParaArchiveDateTime
                    .ParameterName = "@ActivityDateTime"
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.DateTime
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(1)
                End With
                objCmdArchive.Parameters.Add(objParaArchiveDateTime)


                Dim objParaArchiveCategory As New SqlParameter
                With objParaArchiveCategory
                    .ParameterName = "@ActivityCategory"
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(2)
                End With
                objCmdArchive.Parameters.Add(objParaArchiveCategory)


                Dim objParaArchiveDescription As New SqlParameter
                With objParaArchiveDescription
                    .ParameterName = "@Description"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(3)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmdArchive.Parameters.Add(objParaArchiveDescription)

                Dim objParaArchivePatientID As New SqlParameter
                With objParaArchivePatientID
                    .ParameterName = "@PatientID"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(4)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.BigInt
                End With
                objCmdArchive.Parameters.Add(objParaArchivePatientID)

                Dim objParaArchiveUserID As New SqlParameter
                With objParaArchiveUserID
                    .ParameterName = "@UserID"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(5)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.BigInt
                End With
                objCmdArchive.Parameters.Add(objParaArchiveUserID)

                Dim objParaArchiveMachine As New SqlParameter
                With objParaArchiveMachine
                    .ParameterName = "@MachineName"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(6)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmdArchive.Parameters.Add(objParaArchiveMachine)

                'sarika 27th apr 2007
                Dim objParaArchiveSoftwareComponent As New SqlParameter
                With objParaArchiveSoftwareComponent
                    .ParameterName = "@SoftwareComponent"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(7)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmdArchive.Parameters.Add(objParaArchiveSoftwareComponent)

                Dim objParaArchiveOutcome As New SqlParameter
                With objParaArchiveOutcome
                    .ParameterName = "@Outcome"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(8)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmdArchive.Parameters.Add(objParaArchiveOutcome)

                objCmdArchive.Connection = objConArchive
                objCmdArchive.ExecuteNonQuery()


                objCmd.Parameters.Clear()
                objCmd.CommandType = CommandType.StoredProcedure
                objCmd.CommandText = "gsp_DeleteAuditTrail"
                Dim objParaAuditTrailID As New SqlParameter
                With objParaAuditTrailID
                    .ParameterName = "@AuditTrailID"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(0)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.BigInt
                End With
                objCmd.Parameters.Add(objParaAuditTrailID)
                objCmd.Connection = objCon
                objCmd.ExecuteNonQuery()
            Next
            objCon.Close()
            objConArchive.Close()
            'objCon = Nothing
            'objConArchive = Nothing
            ''Sandip Darade 20091117
            If Not IsNothing(objCmd) And Not IsNothing(objCon) And Not IsNothing(objCmdArchive) And Not IsNothing(objConArchive) Then
                objCmd.Dispose()
                objCon.Dispose()
                objCmdArchive.Dispose()
                objConArchive.Dispose()
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Function RestoreArchiveAuditReports(ByVal dtFrom As Date, ByVal dtTo As Date, ByVal sCategory As String, ByVal nUserID As Int64, Optional ByVal nPatientID As Int64 = 0) As Boolean
        Dim dsAuditReports As New DataSet
        Dim objConArchive As New SqlConnection
        objConArchive.ConnectionString = GetArchiveConnectionString()
        Dim objCmdArchive As New SqlCommand
        'sarika 25th june 07
        '     Dim objSQLDataReader As SqlDataReader
        '--
        Try
            objCmdArchive.CommandType = CommandType.StoredProcedure
            objCmdArchive.CommandText = "gsp_ScanArchivedAuditTrails"
            objCmdArchive.Parameters.Clear()
            Dim objParaFromDate As New SqlParameter
            With objParaFromDate
                .ParameterName = "@FromDate"
                .Value = dtFrom.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmdArchive.Parameters.Add(objParaFromDate)

            Dim objParaToDate As New SqlParameter
            With objParaToDate
                .ParameterName = "@ToDate"
                .Value = dtTo.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmdArchive.Parameters.Add(objParaToDate)

            Dim objParaUser As New SqlParameter
            With objParaUser
                .ParameterName = "@UserID"
                .Value = nUserID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmdArchive.Parameters.Add(objParaUser)


            Dim objParaCategory As New SqlParameter
            With objParaCategory
                .ParameterName = "@Category"
                .Value = sCategory
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmdArchive.Parameters.Add(objParaCategory)

            If nPatientID <> 0 Then
                Dim objParaPatientID As New SqlParameter
                With objParaPatientID
                    .ParameterName = "@PatientID"
                    .Value = nPatientID
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.BigInt
                End With
                objCmdArchive.Parameters.Add(objParaPatientID)
            End If

            objCmdArchive.Connection = objConArchive
            objConArchive.Open()
            Dim objDA As New SqlDataAdapter(objCmdArchive)
            objDA.Fill(dsAuditReports)

            Dim objCon As New SqlConnection
            objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
            Dim objCmd As New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InsertArchiveAuditTrail"
            objCon.Open()
            Dim nCount As Integer
            For nCount = 0 To dsAuditReports.Tables(0).Rows.Count - 1
                objCmd.Parameters.Clear()
                Dim objParaArchiveDateTime As New SqlParameter
                With objParaArchiveDateTime
                    .ParameterName = "@ActivityDateTime"
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.DateTime
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(1)
                End With
                objCmd.Parameters.Add(objParaArchiveDateTime)


                Dim objParaArchiveCategory As New SqlParameter
                With objParaArchiveCategory
                    .ParameterName = "@ActivityCategory"
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(2)
                End With
                objCmd.Parameters.Add(objParaArchiveCategory)


                Dim objParaArchiveDescription As New SqlParameter
                With objParaArchiveDescription
                    .ParameterName = "@Description"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(3)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objParaArchiveDescription)

                Dim objParaArchivePatientID As New SqlParameter
                With objParaArchivePatientID
                    .ParameterName = "@PatientID"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(4)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.BigInt
                End With
                objCmd.Parameters.Add(objParaArchivePatientID)

                Dim objParaArchiveUserID As New SqlParameter
                With objParaArchiveUserID
                    .ParameterName = "@UserID"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(5)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.BigInt
                End With
                objCmd.Parameters.Add(objParaArchiveUserID)

                Dim objParaArchiveMachine As New SqlParameter
                With objParaArchiveMachine
                    .ParameterName = "@MachineName"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(6)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objParaArchiveMachine)

                'sarika 27th apr 2007
                Dim objParaArchiveSoftwareComponent As New SqlParameter
                With objParaArchiveSoftwareComponent
                    .ParameterName = "@SoftwareComponent"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(7)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objParaArchiveSoftwareComponent)

                Dim objParaArchiveOutcome As New SqlParameter
                With objParaArchiveOutcome
                    .ParameterName = "@Outcome"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(8)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objParaArchiveOutcome)

                objCmd.Connection = objCon
                objCmd.ExecuteNonQuery()


                objCmdArchive.Parameters.Clear()
                objCmdArchive.CommandType = CommandType.StoredProcedure
                objCmdArchive.CommandText = "gsp_DeleteAuditTrail"
                Dim objParaAuditTrailID As New SqlParameter
                With objParaAuditTrailID
                    .ParameterName = "@AuditTrailID"
                    .Value = dsAuditReports.Tables(0).Rows(nCount).Item(0)
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.BigInt
                End With
                objCmdArchive.Parameters.Add(objParaAuditTrailID)
                objCmdArchive.Connection = objConArchive
                objCmdArchive.ExecuteNonQuery()
            Next
            objCon.Close()
            objConArchive.Close()
            'objCon = Nothing
            'objConArchive = Nothing
            ''Sandip Darade 20091117
            If Not IsNothing(objCmd) And Not IsNothing(objCon) And Not IsNothing(objCmdArchive) And Not IsNothing(objConArchive) Then
                objCmd.Dispose()
                objCon.Dispose()
                objCmdArchive.Dispose()
                objConArchive.Dispose()
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    'Public Function RetrieveAuditReports(ByVal dtFrom As Date, ByVal dtTo As Date, ByVal sCategory As String, ByVal strUser As String, Optional ByVal nPatientID As Integer = 0, Optional ByVal strPatientFirstName As String = "", Optional ByVal strPatientLastName As String = "") As DataTable
    '    Dim dsAuditReports As New DataSet
    '    Dim objCon As New SqlConnection
    '    objCon.ConnectionString = GetConnectionString()
    '    Dim objCmd As New SqlCommand
    '    Dim objSQLDataReader As SqlDataReader

    '    Try
    '        objCmd.CommandType = CommandType.StoredProcedure
    '        objCmd.CommandText = "sp_ScanAuditTrails"
    '        objCmd.Parameters.Clear()
    '        Dim objParaFromDate As New SqlParameter
    '        With objParaFromDate
    '            .ParameterName = "@FromDate"
    '            .Value = dtFrom.Date
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.DateTime
    '        End With
    '        objCmd.Parameters.Add(objParaFromDate)

    '        Dim objParaToDate As New SqlParameter
    '        With objParaToDate
    '            .ParameterName = "@ToDate"
    '            .Value = dtTo.Date
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.DateTime
    '        End With
    '        objCmd.Parameters.Add(objParaToDate)

    '        Dim objParaUser As New SqlParameter
    '        With objParaUser
    '            .ParameterName = "@UserName"
    '            .Value = strUser
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaUser)


    '        Dim objParaCategory As New SqlParameter
    '        With objParaCategory
    '            .ParameterName = "@Category"
    '            .Value = sCategory
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        objCmd.Parameters.Add(objParaCategory)

    '        If nPatientID <> 0 Then
    '            Dim objParaPatientID As New SqlParameter
    '            With objParaPatientID
    '                .ParameterName = "@PatientID"
    '                .Value = nPatientID
    '                .Direction = ParameterDirection.Input
    '                .SqlDbType = SqlDbType.BigInt
    '            End With
    '            objCmd.Parameters.Add(objParaPatientID)
    '        End If

    '        If strPatientFirstName <> "" Then
    '            Dim objParaPatientFirstName As New SqlParameter
    '            With objParaPatientFirstName
    '                .ParameterName = "@PatientFirstName"
    '                .Value = strPatientFirstName
    '                .Direction = ParameterDirection.Input
    '                .SqlDbType = SqlDbType.VarChar
    '            End With
    '            objCmd.Parameters.Add(objParaPatientFirstName)
    '        End If

    '        If strPatientLastName <> "" Then
    '            Dim objParaPatientLastName As New SqlParameter
    '            With objParaPatientLastName
    '                .ParameterName = "@PatientLastName"
    '                .Value = strPatientLastName
    '                .Direction = ParameterDirection.Input
    '                .SqlDbType = SqlDbType.VarChar
    '            End With
    '            objCmd.Parameters.Add(objParaPatientLastName)
    '        End If




    '        objCmd.Connection = objCon
    '        objCon.Open()
    '        Dim objDA As New SqlDataAdapter(objCmd)
    '        objCon.Close()
    '        objDA.Fill(dsAuditReports)
    '        objCon = Nothing

    '        Return dsAuditReports.Tables(0)

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    End Try

    'End Function

    'sarika 25th apr 2007
    Public Function RetrieveAuditReports(ByVal dtFrom As Date, ByVal dtTo As Date, ByVal sCategory As String, ByVal strUser As String, Optional ByVal nPatientID As String = "", Optional ByVal strPatientFirstName As String = "", Optional ByVal strPatientLastName As String = "") As DataTable
        Dim dsAuditReports As New DataSet
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        'sarika 25th june 07
        'Dim objSQLDataReader As SqlDataReader
        '----

        Try
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ScanAuditTrails"
            objCmd.Parameters.Clear()
            Dim objParaFromDate As New SqlParameter
            With objParaFromDate
                .ParameterName = "@FromDate"
                .Value = dtFrom.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFromDate)

            Dim objParaToDate As New SqlParameter
            With objParaToDate
                .ParameterName = "@ToDate"
                .Value = dtTo.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaToDate)

            Dim objParaUser As New SqlParameter
            With objParaUser
                .ParameterName = "@UserName"
                .Value = strUser
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaUser)

            Dim objParaCategory As New SqlParameter
            With objParaCategory
                .ParameterName = "@Category"
                .Value = sCategory
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaCategory)

            If nPatientID <> "" Then
                Dim objParaPatientID As New SqlParameter
                With objParaPatientID
                    .ParameterName = "@PatientCode"
                    .Value = nPatientID
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objParaPatientID)
            End If

            If strPatientFirstName <> "" Then
                Dim objParaPatientFirstName As New SqlParameter
                With objParaPatientFirstName
                    .ParameterName = "@PatientFirstName"
                    .Value = strPatientFirstName
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objParaPatientFirstName)
            End If

            If strPatientLastName <> "" Then
                Dim objParaPatientLastName As New SqlParameter
                With objParaPatientLastName
                    .ParameterName = "@PatientLastName"
                    .Value = strPatientLastName
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objParaPatientLastName)
            End If


            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            objCon.Close()
            objDA.Fill(dsAuditReports)
            'objCon = Nothing
            ''Sandip Darade 20091117
            If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
                objCmd.Dispose()
                objCon.Dispose()
            End If

            Return dsAuditReports.Tables(0)

        Catch ex As Exception
            'sarika 25th june 07
            Return Nothing
            '-------
            MessageBox.Show(ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function

    Public Function RetrieveAuditReportsLog(ByVal dtFrom As Date, ByVal dtTo As Date, ByVal sCategory As String, ByVal strUser As String, Optional ByVal nPatientID As String = "", Optional ByVal strPatientFirstName As String = "", Optional ByVal strPatientLastName As String = "") As DataTable
        Dim dsAuditReports As New DataSet
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        'sarika 25th june 07
        'Dim objSQLDataReader As SqlDataReader
        '--

        Try
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ScanAuditTrailsLog"
            objCmd.Parameters.Clear()
            Dim objParaFromDate As New SqlParameter
            With objParaFromDate
                .ParameterName = "@FromDate"
                .Value = dtFrom.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFromDate)

            Dim objParaToDate As New SqlParameter
            With objParaToDate
                .ParameterName = "@ToDate"
                .Value = dtTo.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaToDate)

            Dim objParaUser As New SqlParameter
            With objParaUser
                .ParameterName = "@UserName"
                .Value = strUser
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaUser)

            Dim objParaCategory As New SqlParameter
            With objParaCategory
                .ParameterName = "@Category"
                .Value = sCategory
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaCategory)

            If nPatientID <> "" Then
                Dim objParaPatientID As New SqlParameter
                With objParaPatientID
                    .ParameterName = "@PatientCode"
                    .Value = nPatientID
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objParaPatientID)
            End If

            If strPatientFirstName <> "" Then
                Dim objParaPatientFirstName As New SqlParameter
                With objParaPatientFirstName
                    .ParameterName = "@PatientFirstName"
                    .Value = strPatientFirstName
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objParaPatientFirstName)
            End If

            If strPatientLastName <> "" Then
                Dim objParaPatientLastName As New SqlParameter
                With objParaPatientLastName
                    .ParameterName = "@PatientLastName"
                    .Value = strPatientLastName
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objParaPatientLastName)
            End If


            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            objCon.Close()
            objDA.Fill(dsAuditReports)
            'objCon = Nothing
            ''Sandip Darade 20091117
            If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
                objCmd.Dispose()
                objCon.Dispose()
            End If
            Return dsAuditReports.Tables(0)

        Catch ex As Exception
            'sarika 25th june 07
            Return Nothing
            '---
            MessageBox.Show(ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try

    End Function
    '******By Sandip Deshmukh 24 Oct 07 12.14PM Bug# 242
    '******in following line the last param converted to Clng as Cint through overflow exception
    Public Function RetrieveArchivedAuditReports(ByVal dtFrom As Date, ByVal dtTo As Date, ByVal sCategory As String, ByVal nUserID As Int64, Optional ByVal nPatientID As Long = 0) As DataTable
        '******24 Oct 07 12.14PM Bug# 242
        Dim dsAuditReports As New DataSet
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetArchiveConnectionString()
        Dim objCmd As New SqlCommand
        'code  commented by sarika 25th june07
        'Dim objSQLDataReader As SqlDataReader

        Try
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ScanArchivedAuditTrails"
            objCmd.Parameters.Clear()
            Dim objParaFromDate As New SqlParameter
            With objParaFromDate
                .ParameterName = "@FromDate"
                .Value = dtFrom.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFromDate)

            Dim objParaToDate As New SqlParameter
            With objParaToDate
                .ParameterName = "@ToDate"
                .Value = dtTo.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaToDate)

            Dim objParaUser As New SqlParameter
            With objParaUser
                .ParameterName = "@UserID"
                .Value = nUserID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaUser)


            Dim objParaCategory As New SqlParameter
            With objParaCategory
                .ParameterName = "@Category"
                .Value = sCategory
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaCategory)

            If nPatientID <> 0 Then
                Dim objParaPatientID As New SqlParameter
                With objParaPatientID
                    .ParameterName = "@PatientID"
                    .Value = nPatientID
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.BigInt
                End With
                objCmd.Parameters.Add(objParaPatientID)
            End If

            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            objCon.Close()
            objDA.Fill(dsAuditReports)
            'objCon = Nothing
            ''Sandip Darade 20091117
            If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
                objCmd.Dispose()
                objCon.Dispose()
            End If
            Return dsAuditReports.Tables(0)
        Catch ex As Exception
            'sarika 25th june 07
            Return Nothing
            '---
            MessageBox.Show(ex.Message, "gloEMR Admin", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Function

   

    Public Function CreateLog(ByVal enmLogActivityType As enmActivityType, ByVal strDescription As String, ByVal sUserName As String, Optional ByVal sMachineName As String = "", Optional ByVal nPatientID As Long = 0, Optional ByVal loginAuditFlag As Boolean = False, Optional ByVal enmLogOutcome As enmOutcome = enmOutcome.Success) As Boolean
        Dim objCon As New SqlConnection

        '---------------------------------------
        'sarika 25th apr 2007
        'strDescription &= "(In gloEMR Admin)"
        Dim strSoftwareComponent As String
        strSoftwareComponent = "gloPM Admin"
        '--------------------------------------

        Try
            objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
            Dim objCmd As New SqlCommand
            Dim objSQLDataReader As SqlDataReader
            Dim _strSQL As String = ""
            Dim blnIsAuditTrailEnabledFlag As Boolean = False
            'By Mahesh - 20070130

            '' For Check If The Login User Has Facility to Make Entry in AditTrial
            '' If He HAS the Add the Entry in Audit Trial
            '' If NOT then Exit From the Function 
            '''''''''''''''''''''''
            _strSQL = "select IsAuditTrail from User_MST where sLoginName = '" & sUserName.Replace("'", "''") & "'"
            objCmd = New SqlCommand(_strSQL, objCon)
            If objCon.State = ConnectionState.Closed Then
                objCon.Open()
            End If

            objSQLDataReader = objCmd.ExecuteReader

            If Not objSQLDataReader Is Nothing Then
                If objSQLDataReader.HasRows = True Then
                    While objSQLDataReader.Read
                        'the value can be NULL besides 0 and 1 , so chk for null value
                        If Not IsDBNull(objSQLDataReader.Item("IsAuditTrail")) Then
                            'if not nulll then set the value of flag 
                            blnIsAuditTrailEnabledFlag = objSQLDataReader.Item("IsAuditTrail")
                        Else
                            'if the value is null/ 0 then set the flag to false
                            blnIsAuditTrailEnabledFlag = False
                        End If
                    End While
                End If
                objSQLDataReader.Close()
            End If

            '  objCon.Close()

            If blnIsAuditTrailEnabledFlag = False Then
                '' If The Login User does not have Facility to Make Entry in AuditTrial then Exit From the Function 
                If loginAuditFlag = False Then
                    objCon.Close()
                    Return True
                End If
            End If



            '''''''''''''''''''

            'Dim objCon As New SqlConnection
            'objCon.ConnectionString = GetConnectionString()


            objCmd = New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_InsertAuditTrail"

            Dim objParaCategory As New SqlParameter
            With objParaCategory
                .ParameterName = "@ActivityCategory"
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            Select Case enmLogActivityType
                Case enmActivityType.Add
                    objParaCategory.Value = "Record Added"
                Case enmActivityType.Delete
                    objParaCategory.Value = "Record Deleted"
                Case enmActivityType.Modify
                    objParaCategory.Value = "Record Modified"
                Case enmActivityType.Login
                    objParaCategory.Value = "Login"
                    'Sarika
                    '*************************************
                Case enmActivityType.Logout
                    objParaCategory.Value = "Logout"

                    'sarika 
                    '20th April 2007
                    '**************************************************************************
                Case enmActivityType.NodeAuthenticationFailure
                    objParaCategory.Value = "Node Authentication Failure"
                Case enmActivityType.RecordViewed
                    objParaCategory.Value = "Record Viewed"
                Case enmActivityType.ResetPassword
                    objParaCategory.Value = "Reset Password"
                Case enmActivityType.SecurityAdmin
                    objParaCategory.Value = "Security Administrative Event"
                Case enmActivityType.UserBlocked
                    objParaCategory.Value = "User Blocked"
                Case enmActivityType.UserUnBlocked
                    objParaCategory.Value = "User UnBlocked"
                Case enmActivityType.Query
                    objParaCategory.Value = "Query"

                    '**************************************************************************

                    '*************************************
                    'Case enmActivityType.RecordDeletedDocument
                    '    objParaCategory.Value = "Delete Document"
                    'Case enmActivityType.ImportDocument_FromFile
                    '    objParaCategory.Value = "Import Document From File"
                    'Case enmActivityType.ImportDocument_FromScanner
                    '    objParaCategory.Value = "Import Document From Scanner"
                    'Case enmActivityType.MergeDocument
                    '    objParaCategory.Value = "Merge Document"
                    'Case enmActivityType.NewDocument
                    '    objParaCategory.Value = "New Document"
                    'Case enmActivityType.RecordDeletedPage
                    '    objParaCategory.Value = "Delete Pages"
                Case enmActivityType.Other
                    objParaCategory.Value = "Other"
                Case enmActivityType.ICD10DOSUpdate
                    objParaCategory.Value = "ICD10 DOS Update"
                Case enmActivityType.ICD10DOSInsert
                    objParaCategory.Value = "ICD10 DOS Insert"
            End Select
            objCmd.Parameters.Add(objParaCategory)

            Dim objParaDescription As New SqlParameter
            With objParaDescription
                .ParameterName = "@Description"
                .Value = strDescription
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaDescription)

            Dim objParaPatientID As New SqlParameter
            With objParaPatientID
                .ParameterName = "@PatientID"
                .Value = nPatientID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaPatientID)

            'Dim objParaUserID As New SqlParameter
            'With objParaUserID
            '    .ParameterName = "@UserName"
            '    .Value = sUserName
            '    .Direction = ParameterDirection.Input
            '    .SqlDbType = SqlDbType.VarChar
            'End With
            'objCmd.Parameters.Add(objParaUserID)

            'Dim objParaMachine As New SqlParameter
            'With objParaMachine
            '    .ParameterName = "@MachineName"
            '    .Value = sMachineName
            '    .Direction = ParameterDirection.Input
            '    .SqlDbType = SqlDbType.VarChar
            'End With
            'objCmd.Parameters.Add(objParaMachine)

            '-------------------------------------------------
            'sarika 25th apr 2007
            'Dim objParaSoftwareComponent As New SqlParameter
            'With objParaSoftwareComponent
            '    .ParameterName = "@sSoftwareComponent"
            '    .Value = strSoftwareComponent
            '    .Direction = ParameterDirection.Input
            '    .SqlDbType = SqlDbType.VarChar
            'End With
            'objCmd.Parameters.Add(objParaSoftwareComponent)

            'sarika 26th apr 2007
            Dim objParaOutcome As New SqlParameter
            With objParaOutcome
                .ParameterName = "@sOutcome"

                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            Select Case enmLogOutcome
                Case enmOutcome.Success
                    objParaOutcome.Value = "Success"
                Case enmOutcome.Failure
                    objParaOutcome.Value = "Failure"
            End Select
            objCmd.Parameters.Add(objParaOutcome)

            '----------------------------------------------

            objCmd.Connection = objCon
            '   objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            'objCmd = Nothing
            'objCon = Nothing
            ''Sandip Darade 20091117
            If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
                objCmd.Dispose()
                objCon.Dispose()
            End If
            Return True

        Catch ex As Exception
            Return False
        Finally
            ' objCon.Close()
        End Try
    End Function

#End Region
End Class
