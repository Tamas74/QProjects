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
Public Class clsBackupSchedule

#Region " Enumerators"
    Enum enmScheduleCriteria
        Today
        Yesterday
        LastWeek
        LastMonth
        LastQuarter
        LastYear
        All
    End Enum
    Enum enmBackupType
        Complete
        Differencial
    End Enum
    Enum enmBackupOverwrite
        AppendToMedia
        OverwriteExistingMedia
    End Enum
    Enum enmBackupTo
        Disk
        Tape
    End Enum

#End Region
    
#Region " Private Variables"
    Dim _sBackupType As enmBackupType
    Dim _sOverwrite As enmBackupOverwrite
    Dim _sBackupTo As enmBackupTo
    Dim _sBackupLocation As String
    Dim _nUserID As Integer
#End Region

#Region " Public Properties"
    Public Property BackupType() As enmBackupType
        Get
            Return _sBackupType
        End Get
        Set(ByVal Value As enmBackupType)
            _sBackupType = Value
        End Set
    End Property
    Public Property Overwrite() As enmBackupOverwrite
        Get
            Return _sOverwrite
        End Get
        Set(ByVal Value As enmBackupOverwrite)
            _sOverwrite = Value
        End Set
    End Property
    Public Property BackupTo() As enmBackupTo
        Get
            Return _sBackupTo
        End Get
        Set(ByVal Value As enmBackupTo)
            _sBackupTo = Value
        End Set
    End Property
    Public Property BackupLocation() As String
        Get
            Return _sBackupLocation
        End Get
        Set(ByVal Value As String)
            _sBackupLocation = Value
        End Set
    End Property
    Public Property UserID() As Integer
        Get
            Return _nUserID
        End Get
        Set(ByVal Value As Integer)
            _nUserID = Value
        End Set
    End Property
#End Region

#Region " Public Functions"
    'sarika 25th june 07
    'Public Function RetrieveBackupSchedule(ByVal strJobID As String)
    Public Function RetrieveBackupSchedule(ByVal strJobID As String) As Boolean
        '---
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_ScanBackupSchedule"

        Dim objParaJobID As New SqlParameter
        With objParaJobID
            .ParameterName = "@JobID"
            .Value = strJobID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaJobID)
        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader
        If objSQLDataReader.HasRows = True Then
            objSQLDataReader.Read()
            If Trim(objSQLDataReader.Item("BackupType")) = "Complete" Then
                _sBackupType = enmBackupType.Complete
            Else
                _sBackupType = enmBackupType.Differencial
            End If
            If UCase(Trim(objSQLDataReader.Item("Overwrite"))) = UCase("Append to media") Then
                _sOverwrite = enmBackupOverwrite.AppendToMedia
            Else
                _sOverwrite = enmBackupOverwrite.OverwriteExistingMedia
            End If
            If Trim(objSQLDataReader.Item("BackupTo")) = "Disk" Then
                _sBackupTo = enmBackupTo.Disk
            Else
                _sBackupTo = enmBackupTo.Tape
            End If
            _sBackupLocation = objSQLDataReader.Item("BackupLocation")
        End If
        objSQLDataReader.Close()
        objCon.Close()
        objSQLDataReader = Nothing
        'objCmd = Nothing
        'objCon = Nothing
        ''Sandip Darade 20091117
        If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
            objCmd.Dispose()
            objCon.Dispose()
        End If
    End Function
    Public Function RetrieveBackupSchedule(ByVal enmCriteria As enmScheduleCriteria) As DataTable
        Dim objSQLServer As New SQLServer2
        'or Windows Authentication
        objSQLServer.LoginSecure = True
        objSQLServer.Connect(gstrSQLServerName)
        Dim dtBackup As New DataTable("BackupSchedule")
        Dim clmnScheduleID As New DataColumn("ScheduleID")
        Dim clmnJobID As New DataColumn("JobID")
        Dim clmnJobName As New DataColumn("JobName")
        Dim clmnJobCategory As New DataColumn("JobCategory")
        Dim clmnJobEnabled As New DataColumn("JobEnabled")
        Dim clmnJobDateCreated As New DataColumn("JobDateCreated")
        Dim clmnJobLastRun As New DataColumn("JobLastRunDateTime")
        Dim clmnJobNextRun As New DataColumn("JobNextRunDateTime")
        dtBackup.Columns.Add(clmnScheduleID)
        dtBackup.Columns.Add(clmnJobID)
        dtBackup.Columns.Add(clmnJobName)
        dtBackup.Columns.Add(clmnJobCategory)
        dtBackup.Columns.Add(clmnJobEnabled)
        dtBackup.Columns.Add(clmnJobDateCreated)
        dtBackup.Columns.Add(clmnJobLastRun)
        dtBackup.Columns.Add(clmnJobNextRun)
        Dim nCount As Int16
        Dim drRow As DataRow
        Dim objJob As Job
        Dim strLastRun As String
        Dim strNextRun As String
        Dim blnAdd As Boolean
        For nCount = 1 To objSQLServer.JobServer.Jobs.Count
            blnAdd = False
            objJob = objSQLServer.JobServer.Jobs.Item(nCount)
            Select Case enmCriteria
                Case enmScheduleCriteria.Today
                    If CType(objJob.DateCreated, Date).Date = Date.Now.Date Then
                        blnAdd = True
                    End If
                Case enmScheduleCriteria.Yesterday
                    If CType(objJob.DateCreated, Date).Date = Date.Now.Date.AddDays(-1) Then
                        blnAdd = True
                    End If
                Case enmScheduleCriteria.LastWeek
                    If CType(objJob.DateCreated, Date).Date >= Date.Now.Date.AddDays(-7) Then
                        blnAdd = True
                    End If
                Case enmScheduleCriteria.LastMonth
                    If CType(objJob.DateCreated, Date).Date >= Date.Now.Date.AddMonths(-1) Then
                        blnAdd = True
                    End If
                Case enmScheduleCriteria.LastQuarter
                    If CType(objJob.DateCreated, Date).Date >= Date.Now.Date.AddMonths(-3) Then
                        blnAdd = True
                    End If
                Case enmScheduleCriteria.LastYear
                    If CType(objJob.DateCreated, Date).Date >= Date.Now.Date.AddYears(-1) Then
                        blnAdd = True
                    End If
                Case enmScheduleCriteria.All
                    blnAdd = True
            End Select
            If blnAdd = True Then
                'Check the Job Exists or not
                Dim objCon As New SqlConnection
                objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
                Dim objCmd As New SqlCommand
                Dim objSQLDataReader As SqlDataReader
                objCmd.CommandType = CommandType.StoredProcedure
                objCmd.CommandText = "gsp_ScanBackupSchedule"

                Dim objParaJobID As New SqlParameter
                With objParaJobID
                    .ParameterName = "@JobID"
                    .Value = objJob.JobID
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.VarChar
                End With
                objCmd.Parameters.Add(objParaJobID)
                objCmd.Connection = objCon
                objCon.Open()
                objSQLDataReader = objCmd.ExecuteReader
                If objSQLDataReader.HasRows = True Then
                    objSQLDataReader.Read()
                    drRow = dtBackup.NewRow
                    drRow(0) = objSQLDataReader.Item(0)
                    objSQLDataReader.Close()
                    objCon.Close()
                    objSQLDataReader = Nothing
                    objCmd = Nothing
                    objCon = Nothing
                    drRow(1) = objJob.JobID
                    drRow(2) = objJob.Name
                    drRow(3) = objJob.Category()
                    drRow(4) = objJob.Enabled
                    drRow(5) = objJob.DateCreated
                    If objJob.LastRunDate <> 0 And CStr(objJob.LastRunDate).Length = 8 Then
                        strLastRun = Mid(CStr(objJob.LastRunDate), 5, 2) & "/" & Mid(CStr(objJob.LastRunDate), 7, 2) & "/" & Left(CStr(objJob.LastRunDate), 4)
                        If objJob.LastRunTime <> 0 And CStr(objJob.LastRunTime).Length = 6 Then
                            strLastRun = strLastRun & " "
                            strLastRun = strLastRun & Left(CStr(objJob.LastRunTime), 2) & ":" & Mid(CStr(objJob.LastRunTime), 3, 2) & ":" & Mid(CStr(objJob.LastRunTime), 5, 2)
                        End If
                    Else
                        strLastRun = ""
                    End If
                    drRow(6) = strLastRun 'objJob.LastRunDate & "-" & objJob.LastRunTime

                    If objJob.NextRunDate <> 0 And CStr(objJob.NextRunDate).Length = 8 Then
                        strNextRun = Mid(CStr(objJob.NextRunDate), 5, 2) & "/" & Mid(CStr(objJob.NextRunDate), 7, 2) & "/" & Left(CStr(objJob.NextRunDate), 4)
                        If objJob.NextRunTime <> 0 And CStr(objJob.NextRunTime).Length = 6 Then
                            strNextRun = strNextRun & " "
                            strNextRun = strNextRun & Left(CStr(objJob.NextRunTime), 2) & ":" & Mid(CStr(objJob.NextRunTime), 3, 2) & ":" & Mid(CStr(objJob.NextRunTime), 5, 2)
                        End If
                    Else
                        strNextRun = ""
                    End If
                    drRow(7) = strNextRun 'objJob.NextRunDate & "-" & objJob.NextRunTime
                    dtBackup.Rows.Add(drRow)
                Else
                    objSQLDataReader.Close()
                    objCon.Close()
                    objSQLDataReader = Nothing
                    objCmd = Nothing
                    objCon = Nothing
                End If


            End If
        Next
        Return dtBackup
    End Function
    'Public Function RetrieveBackupSchedule(ByVal enmCriteria As enmScheduleCriteria) As DataTable
    '    Dim dtFromDate As DateTime
    '    Dim dtEndDate As DateTime
    '    dtEndDate = Date.Now.Date
    '    Select Case enmCriteria
    '        Case enmScheduleCriteria.Today
    '            dtFromDate = Date.Now.Date
    '        Case enmScheduleCriteria.Yesterday
    '            dtFromDate = Date.Now.Date.AddDays(-1)
    '        Case enmScheduleCriteria.LastWeek
    '            dtFromDate = Date.Now.Date.AddDays(-7)
    '        Case enmScheduleCriteria.LastMonth
    '            dtFromDate = Date.Now.Date.AddMonths(-1)
    '        Case enmScheduleCriteria.LastQuarter
    '            dtFromDate = Date.Now.Date.AddMonths(-3)
    '        Case enmScheduleCriteria.LastYear
    '            dtFromDate = Date.Now.Date.AddYears(-1)
    '    End Select
    '    Dim objCon As New SqlConnection
    '    objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
    '    Dim objCmd As New SqlCommand
    '    Dim objSQLDataReader As SqlDataReader
    '    objCmd.CommandType = CommandType.StoredProcedure
    '    objCmd.CommandText = "sp_ScanBackupSchedule"
    '    objCmd.Connection = objCon
    '    If enmCriteria <> enmScheduleCriteria.All Then
    '        Dim objParaFromDate As New SqlParameter
    '        With objParaFromDate
    '            .ParameterName = "@FromDate"
    '            .Value = dtFromDate
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.DateTime
    '        End With
    '        objCmd.Parameters.Add(objParaFromDate)

    '        Dim objParaToDate As New SqlParameter
    '        With objParaToDate
    '            .ParameterName = "@ToDate"
    '            .Value = dtEndDate
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.DateTime
    '        End With
    '        objCmd.Parameters.Add(objParaToDate)
    '    End If
    '    objCon.Open()
    '    Dim objDA As New SqlDataAdapter(objCmd)
    '    Dim dsData As New DataSet
    '    objDA.Fill(dsData)
    '    objCon.Close()
    '    objCon = Nothing
    '    Return dsData.Tables(0)
    'End Function
    Public Function DefaultBackupPath() As String
        Dim strPath As String
        Dim objRegistry As SQLDMO.Registry2
        Dim objSQLServer As New SQLServer2
        'or Windows Authentication
        objSQLServer.LoginSecure = True
        objSQLServer.Connect(gstrSQLServerName)
        objRegistry = objSQLServer.Registry
        strPath = objRegistry.BackupDirectory
        objSQLServer.Close()
        Return strPath
    End Function
    Public Function DeleteBackup(ByVal nScheduleID As Integer, ByVal strJobID As String) As Boolean
        Dim nCount As Int16
        Dim objSQLServer As New SQLServer2
        'or Windows Authentication
        objSQLServer.LoginSecure = True
        objSQLServer.Connect(gstrSQLServerName)
        For nCount = 1 To objSQLServer.JobServer.Jobs.Count
            If Trim(objSQLServer.JobServer.Jobs.Item(nCount).JobID) = Trim(strJobID) Then
                objSQLServer.JobServer.Jobs.Remove(nCount)
                Exit For
            End If
        Next
        objSQLServer.Close()
        objSQLServer = Nothing
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        'sarika 25th june 07
        'Dim objSQLDataReader As SqlDataReader
        '---
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_DeleteBackupSchedule"

        Dim objParaScheduleID As New SqlParameter
        With objParaScheduleID
            .ParameterName = "@ScheduleID"
            .Value = nScheduleID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaScheduleID)
        objCmd.Connection = objCon
        objCon.Open()
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
    End Function
    Public Function AddUpdateBackupSchedule(ByVal strJobID As String, Optional ByVal nScheduleID As Integer = 0) As Boolean
        Return AddUpdateBackupSchedule(strJobID, _sBackupType, _sOverwrite, _sBackupTo, _sBackupLocation, _nUserID, nScheduleID)
    End Function
    Public Function AddUpdateBackupSchedule(ByVal strJobID As String, ByVal sBackupType As enmBackupType, ByVal sOverwrite As enmBackupOverwrite, ByVal sBackupTo As enmBackupTo, ByVal sBackupLocation As String, ByVal nUserID As Integer, Optional ByVal nScheduleID As Integer = 0) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        'sarika 25th june 07
        'Dim objSQLDataReader As SqlDataReader
        '----
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_InUpBackupSchedule"

        Dim objParaScheduleID As New SqlParameter
        With objParaScheduleID
            .ParameterName = "@ScheduleID"
            .Value = nScheduleID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaScheduleID)

        Dim objParaJobID As New SqlParameter
        With objParaJobID
            .ParameterName = "@JobID"
            .Value = strJobID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaJobID)

        Dim objParaBackupType As New SqlParameter
        With objParaBackupType
            .ParameterName = "@BackupType"
            If sBackupType = enmBackupType.Complete Then
                .Value = "Complete"
            Else
                .Value = "Differenical"
            End If
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaBackupType)

        Dim objParaOverwrite As New SqlParameter
        With objParaOverwrite
            .ParameterName = "@Overwrite"
            If sOverwrite = enmBackupOverwrite.AppendToMedia Then
                .Value = "Append To Media"
            Else
                .Value = "Overwrite Existing Media"
            End If
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaOverwrite)


        Dim objParaBackupTo As New SqlParameter
        With objParaBackupTo
            .ParameterName = "@BackupTo"
            If sBackupTo = enmBackupTo.Disk Then
                .Value = "Disk"
            Else
                .Value = "Tape"
            End If
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaBackupTo)


        Dim objParaBackupLocation As New SqlParameter
        With objParaBackupLocation
            .ParameterName = "@BackupLocation"
            .Value = sBackupLocation
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaBackupLocation)

        Dim objParaUserID As New SqlParameter
        With objParaUserID
            .ParameterName = "@UserID"
            .Value = nUserID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaUserID)

        objCmd.Connection = objCon
        objCon.Open()
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
    End Function
#End Region

End Class
