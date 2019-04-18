Imports System.Data.SqlClient
Imports System.Data


Public Class ClsPatientSynopsis
    Implements IDisposable

    'Dim cmd As SqlCommand
    Dim strsql As String = ""
    'Dim sqladpt As SqlDataAdapter
    'Dim dt As New DataTable
    Dim conn As SqlConnection = New SqlConnection(GetConnectionString())

    ' Dim dtCategory As New DataTable
    ' Dim dtGroup As New DataTable
    'Dim dtTest As New DataTable

    Private disposed As Boolean = False

    'This Is For Radiology
    Public Function PopulateRadiology(ByVal nPatientID) As DataTable

        'Handle exception using try-catch-finally block
        Try

            'Declare a variable for command
            Dim cmd As SqlCommand = New SqlCommand

            'Open connection
            conn.Open()

            'declare a variable for sql string
            Dim strSQL = ""

            'Create a sql command string
            strSQL = " SELECT  DISTINCT  LM_Category.lm_category_Description " _
                    & " FROM  LM_Test LEFT OUTER JOIN LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN " _
                    & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID " _
                    & " WHERE  (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID = lm_Patient_ID) " _
                    & " ORDER BY lm_category_Description "

            'Pass sql string to command object
            cmd.CommandText = strSQL
            cmd.CommandType = CommandType.Text

            cmd.Connection = conn
            Dim sqladpt As SqlDataAdapter = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            Dim dt As New DataTable

            sqladpt.Fill(dt)
            sqladpt.Dispose()
            sqladpt = Nothing
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            PopulateRadiology = Nothing
            Throw ex

            'Finally checks and close the connections
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End If
        End Try

        'Return Data Table

    End Function

    'This is for Procedure
    Public Function PopulateProcedures(ByVal nPatientID As Int64) As DataTable

        'Handle exception using try-catch-finally block
        Try

            'Declare a variable for command
            Dim cmd As SqlCommand = New SqlCommand

            'Connection open
            conn.Open()

            'Declare a variable for sql string
            Dim strsql = ""

            'Create a sql command string
            strsql = "select Distinct(isnull(sCPTcode,'') + space(1) + isnull(sCPTDescription,'')) ,'(' +  convert(varchar(50),dtdos,101) + ')' ,ExamICD9CPT.nExamID as ExamID ,ExamICD9CPT.nVisitId as visitid ,dtDOS as DOS,patientexams.sExamName as ExamName, isnull(PatientExams.bIsFinished,0)  as ExamStatus from ExamICD9CPT inner join patientexams on examicd9cpt.nexamid=patientexams.nexamid where examicd9cpt.nPatientID=" & nPatientID & " and sCPTCode is not null"

            'Pass the sql string to command
            cmd.CommandText = strsql
            cmd.CommandType = CommandType.Text
            cmd.Connection = conn
            Dim sqladpt As SqlDataAdapter = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            Dim dt As New DataTable
            sqladpt.Fill(dt)
            sqladpt.Dispose()
            sqladpt = Nothing
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
            'Finally checks and close the connections
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End If
        End Try

        'Return Data Table

    End Function

    'This is for Problem List
    Public Function GetVisitdate(ByVal nVisitID As Int64) As DateTime

        'Handle exception using try-catch-finally block
        Try

            'Declare a variable for command
            Dim cmd As SqlCommand = New SqlCommand

            'Open connection
            conn.Open()

            Dim strsql = ""

            'Create a sql command string
            strsql = "select dtvisitdate from visits where nVisitID=" & nVisitID

            'Pass the sql string to command
            cmd.CommandText = strsql
            cmd.CommandType = CommandType.Text
            cmd.Connection = conn
            Dim Visitdate As DateTime
            Visitdate = cmd.ExecuteScalar
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            Return Visitdate
        Catch ex As Exception
            Throw ex
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'Finally checks and close the connections
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End If
        End Try
    End Function
    'This is for Problem List
    Public Function GetVisitIDforLabs(ByVal nOrderID As Int64) As Int64

        'Handle exception using try-catch-finally block
        Try

            'Declare a variable for command
            Dim cmd As SqlCommand = New SqlCommand

            'Open connection
            conn.Open()

            Dim strsql = ""

            'Create a sql command string
            strsql = "select labom_VisitID from Lab_Order_MST where labom_OrderID=" & nOrderID

            'Pass the sql string to command
            cmd.CommandText = strsql
            cmd.CommandType = CommandType.Text
            cmd.Connection = conn
            Dim VisitID As Int64
            VisitID = cmd.ExecuteScalar
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            Return VisitID
        Catch ex As Exception
            Throw ex
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'Finally checks and close the connections
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End If
        End Try
    End Function
    'This is for Problem List
    Public Function PopulateProbemList(ByVal nPatientID As Int64) As DataTable

        'Handle exception using try-catch-finally block
        Try

            'Declare a variable for command
            Dim cmd As SqlCommand = New SqlCommand

            'Open connection
            conn.Open()

            Dim strsql = ""

            'Create a sql command string
            strsql = "select isnull(sCheifComplaint,''),nProblemID as ProblemId,nVisitID as VisitID,dtDos as DOS from ProblemList where nPatientID=" & nPatientID

            'Pass the sql string to command
            cmd.CommandText = strsql
            cmd.CommandType = CommandType.Text
            cmd.Connection = conn
            Dim sqladpt As SqlDataAdapter = New SqlDataAdapter
            sqladpt.SelectCommand = cmd

            Dim dt As New DataTable

            sqladpt.Fill(dt)
            sqladpt.Dispose()
            sqladpt = Nothing
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            Return dt
        Catch ex As Exception
            PopulateProbemList = Nothing
            Throw ex
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'Finally checks and close the connections
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End If
        End Try

        'Return Data Table

    End Function
    Public Function PopulateSynopsisData(ByVal nPatientID As Int64, Optional ByVal ModuleName As String = "All") As DataSet

        'Declare a variables
        Dim cmd As SqlCommand = Nothing
        Dim osqlpara As SqlParameter

        Dim da As SqlDataAdapter = Nothing
        Dim ds As New DataSet

        'Handle exception using try-catch-finally block
        Try

            'Declare a variable for command
            cmd = New SqlCommand("gsp_LoadSynopsisData", conn)

            'Use stored procedure for commands
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "gsp_LoadSynopsisData"

            'Open connection
            conn.Open()

            'Declare a variable for parameter
            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@nPatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = nPatientID
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing
            'Declare a variable for parameter
            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@dtVisitDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.DateTime
            osqlpara.Value = Now.Date

            'Pass the parameter to command
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@StrModule"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.String
            osqlpara.Value = ModuleName

            'Pass the parameter to command
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            'Pass the parameter to command


            da = New SqlDataAdapter(cmd)
            da.Fill(ds)
            If ModuleName = "All" Then
                ds.Tables(0).TableName = "PatientInfo"
                ds.Tables(1).TableName = "Problems"
                ds.Tables(2).TableName = "Medications"
                ds.Tables(3).TableName = "Procedures"
                ds.Tables(4).TableName = "OrderTemplates"
                ds.Tables(5).TableName = "History"
                ds.Tables(6).TableName = "Labs"
                ds.Tables(7).TableName = "Imaging"
                ds.Tables(8).TableName = "Implant"
            Else
                ds.Tables(0).TableName = ModuleName
            End If
            


        Catch ex As Exception
            Throw ex
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'Finally checks and close the connections
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End If
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            osqlpara = Nothing
        End Try

        'Return Data Set
        Return ds
    End Function
    'For Latest Medication
    Public Function PopulateLatestMedications(ByVal nPatientID As Int64) As DataTable

        'Declare a variables
        Dim cmd As SqlCommand = Nothing
        Dim osqlpara As SqlParameter
        Dim _table As New DataTable

        'Handle exception using try-catch-finally block
        Try

            'Declare a variable for command
            cmd = New SqlCommand("GetLatestMedication", conn)

            'Use stored procedure for commands
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "GetLatestMedication"

            'Open connection
            conn.Open()

            'Declare a variable for parameter
            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@VisitDate"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.DateTime
            osqlpara.Value = Now.Date

            'Pass the parameter to command
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            'Declare a variable for parameter
            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@PatientID"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = nPatientID

            'Pass the parameter to command
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            _table.Load(cmd.ExecuteReader())

        Catch ex As Exception
            Throw ex
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'Finally checks and close the connections
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End If
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            osqlpara = Nothing
        End Try

        'Return Data Table
        Return _table
    End Function


    'This is for Latest Lab
    Public Function PopulateLabs(ByVal nPatientID As Int64) As DataTable

        'Declaration of Variables
        Dim _table As New DataTable
        Dim osqlpara As SqlParameter = Nothing
        Dim cmd As SqlCommand = Nothing
        'Handle exception using try-catch-finally block
        Try

            'Use stored procedure for command
            cmd = New SqlCommand("GetLatestMedication", conn)
            cmd.CommandType = CommandType.StoredProcedure

            'Connection open
            conn.Open()
            cmd = New SqlCommand
            cmd.Connection = conn

            'Command type
            cmd.CommandType = CommandType.StoredProcedure

            'Stored procedure name
            cmd.CommandText = "Labs_GetLabsforPatient"

            'Declare a variable for parameter
            osqlpara = New SqlParameter
            osqlpara.ParameterName = "@npatientid"
            osqlpara.Direction = ParameterDirection.Input
            osqlpara.DbType = DbType.Int64
            osqlpara.Value = nPatientID

            'Pass that parameter to command
            cmd.Parameters.Add(osqlpara)
            osqlpara = Nothing

            _table.Load(cmd.ExecuteReader)

            Return _table
        Catch ex As Exception
            Throw ex
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'Finally checks and close the connections
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End If
            If Not IsNothing(osqlpara) Then   'Disposed by mitesh
                osqlpara = Nothing
            End If
            'If Not IsNothing(_table) Then
            '    _table.Dispose()
            '    _table = Nothing
            'End If
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            osqlpara = Nothing
        End Try
        'Return Data Table
        Return _table

    End Function

    'For Name Of Category For Order
    Public Function CategoryName_Summary(ByVal nPatientID As Int64) As DataTable

        'Handle exception using try-catch-finally block
        'Declaration of Variables
        Dim cmd As SqlCommand = New SqlCommand
        Try



            'Create a sql command string
            strsql = " SELECT  DISTINCT  LM_Category.lm_category_Description " _
                    & " FROM  LM_Test LEFT OUTER JOIN LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN " _
                    & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID " _
                    & " WHERE  (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID =    " & nPatientID & ") " _
                    & " ORDER BY lm_category_Description "

            'Pass the sql string to command
            cmd.CommandText = strsql
            cmd.CommandType = CommandType.Text

            cmd.Connection = conn
            Dim sqladpt As SqlDataAdapter = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            Dim dt As New DataTable

            sqladpt.Fill(dt)
            sqladpt.Dispose()
            sqladpt = Nothing
            Return dt
            'Handle Catch block for Try
        Catch ex As Exception
            CategoryName_Summary = Nothing
            Throw ex
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'Finally checks and close the connections
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End If
            If Not IsNothing(cmd) Then   'Disposed by mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try

        'Return Data Table

    End Function

    'For Test Details Under Category
    Public Function TestDetails_Summary(ByVal nPatientID As Int64, ByVal CategoryDescription As String) As DataTable

        Dim cmd As SqlCommand = New SqlCommand
        Try

            'Create a sql command string
            strsql = " SELECT DISTINCT LM_Test.lm_test_GroupNo, LM_Test_1.lm_test_Name AS GroupName " _
                         & " FROM LM_Test LEFT OUTER JOIN " _
                         & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID LEFT OUTER JOIN " _
                         & " LM_Test LM_Test_1 ON LM_Test.lm_test_GroupNo = LM_Test_1.lm_test_ID LEFT OUTER JOIN " _
                         & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID " _
                         & " WHERE (LM_Orders.lm_Patient_ID =" & nPatientID & ")  AND  " _
                         & " (LM_Category.lm_category_Description = '" & Replace(CategoryDescription, "'", "''") & "') AND (LM_Test.lm_test_Name IS NOT NULL) " _
                         & " ORDER BY LM_Test_1.lm_test_Name, LM_Test.lm_test_GroupNo "

            'Pass the sql string to command
            cmd.CommandText = strsql
            cmd.CommandType = CommandType.Text

            cmd.Connection = conn
            Dim sqladpt As SqlDataAdapter = New SqlDataAdapter
            sqladpt.SelectCommand = cmd
            Dim dt As New DataTable

            sqladpt.Fill(dt)
            sqladpt.Dispose()
            sqladpt = Nothing
            Return dt
            'Handle Catch block for Try
        Catch ex As Exception
            TestDetails_Summary = Nothing
            Throw ex
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'Finally checks and close the connections
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End If
            If Not IsNothing(cmd) Then   'Disposed by mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try

        'Return Data Table

    End Function


    ''For Detail description Of Each Test
    'Public Function DetailDescription_Summary(ByVal nPatientID As Int64, ByVal CategoryDescription As String, ByVal GroupNo As Int64) As DataTable


    '    'Handle exception using try-catch-finally block
    '    Try

    '        'Create a sql command string
    '        strsql = " SELECT LM_Test.lm_test_Name, LM_Orders.lm_Order_ID, LM_Orders.lm_Visit_ID, LM_Orders.lm_Patient_ID, LM_Orders.lm_Provider_ID, " _
    '        & " LM_Orders.lm_test_ID, LM_Orders.lm_OrderDate, LM_Orders.lm_NumericResult, LM_Orders.lm_Result, LM_Orders.lm_IsFinished, LM_Orders.lm_Status, " _
    '        & " LM_Test.lm_test_Template_ID , LM_Test.lm_test_TestGroupFlag, LM_Test.lm_test_Dimension, LM_Orders.lm_sICD9Code, LM_Orders.lm_sICD9Description " _
    '        & " FROM  LM_Test INNER JOIN " _
    '        & " LM_Orders ON LM_Test.lm_test_ID = LM_Orders.lm_test_ID INNER JOIN " _
    '        & " LM_Category ON LM_Test.lm_test_CategoryID = LM_Category.lm_category_ID " _
    '        & " WHERE     (LM_Test.lm_test_Name IS NOT NULL) AND (LM_Orders.lm_Patient_ID =" & nPatientID & ")  AND  " _
    '        & " (LM_Category.lm_category_Description = '" & Replace(CategoryDescription, "'", "''") & "') AND (LM_Test.lm_test_GroupNo =" & GroupNo & ") " _
    '        & " ORDER BY LM_Test.lm_test_GroupNo, LM_Test.lm_test_LevelNo, LM_Test.lm_test_Name, LM_Test.lm_test_TestGroupFlag "

    '        'Handle Catch block for Try
    '    Catch ex As Exception
    '        Throw ex
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        'Finally checks and close the connections
    '    Finally
    '        If Not IsNothing(conn) Then
    '            If conn.State = ConnectionState.Open Then
    '                conn.Close()
    '            End If
    '        End If
    '    End Try

    '    'Return Data Table
    '    Return dt
    'End Function

    'Public Function PopulateLatestHistory(ByVal nPatientID As Int64) As DataTable

    '    'Declaration of variables
    '    Dim osqlpara As SqlParameter = Nothing
    '    Dim oDataReader As SqlDataReader = Nothing

    '    'Handle exception using try-catch-finally block
    '    Try

    '        'Connection Open
    '        conn.Open()

    '        'Declare a variable for command
    '        Dim cmd As SqlCommand = New SqlCommand("GetLatestMedication", conn)

    '        'Command Type
    '        cmd.CommandType = CommandType.StoredProcedure

    '        'Stored procedure name
    '        cmd.CommandText = "gsp_CCDGetLatestAllergies"

    '        'Declare a variable for parameter
    '        osqlpara = New SqlParameter
    '        osqlpara.ParameterName = "@PatientID"
    '        osqlpara.Direction = ParameterDirection.Input
    '        osqlpara.DbType = DbType.Int64
    '        osqlpara.Value = nPatientID

    '        'Pass that parameter to command
    '        cmd.Parameters.Add(osqlpara)
    '        osqlpara = Nothing

    '        'Declare a variable for parameter
    '        osqlpara = New SqlParameter
    '        osqlpara.ParameterName = "@Category"
    '        osqlpara.Direction = ParameterDirection.Input
    '        osqlpara.DbType = DbType.String
    '        osqlpara.Value = ""

    '        'Pass that parameter to command
    '        cmd.Parameters.Add(osqlpara)
    '        osqlpara = Nothing

    '        'Declare a variable for parameter
    '        osqlpara = New SqlParameter
    '        osqlpara.ParameterName = "@VisitDate"
    '        osqlpara.Direction = ParameterDirection.Input
    '        osqlpara.DbType = DbType.DateTime
    '        osqlpara.Value = Now.Date

    '        'Pass that parameter to command
    '        cmd.Parameters.Add(osqlpara)

    '        oDataReader = cmd.ExecuteReader()
    '        oDataReader.Close()
    '        cmd.Parameters.Clear()
    '        cmd.Dispose()
    '        cmd = Nothing

    '        'Handle Catch block for Try
    '    Catch ex As Exception
    '        Throw ex
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        'Finally checks and close the connections
    '    Finally
    '        If Not IsNothing(conn) Then
    '            If conn.State = ConnectionState.Open Then
    '                conn.Close()
    '            End If
    '        End If
    '        If Not IsNothing(oDataReader) Then   'Disposed by mitesh
    '            oDataReader.Dispose()
    '            oDataReader = Nothing
    '        End If
    '        If Not IsNothing(osqlpara) Then
    '            osqlpara = Nothing
    '        End If
    '        osqlpara = Nothing
    '    End Try

    '    'Return Data Table
    '    Return dt
    'End Function

    Public Function Fill_PastExams(ByVal nPatientID As Int64, ByVal nProviderId As Int64, ByVal dtFrom As DateTime, ByVal dtTo As DateTime, ByVal IsFinished As Short) As DataTable

        'Declare a variable for connection
        Dim objCon As SqlConnection = New SqlConnection("server=gloint;database=gloEMR50_CCHIT2008_1;Integrated security=True")
        'Declare a variable for command
        Dim objcmd As New SqlCommand("gsp_GetPastExams", objCon)
        'Handle exception using try-catch-finally block
        'Declare a variable for parameter
        Dim objParaFrom As New SqlParameter
        Dim objParaTo As New SqlParameter
        Dim objParaStatus As New SqlParameter
        Dim objParaDoctorID As New SqlParameter
        Try



            'Stored procedure name
            objcmd.CommandType = CommandType.StoredProcedure


            'Stored procedure name
            objcmd.CommandText = "gsp_GetPastExams"
            'objcmd.Parameters.Add("@PatientID", nPatientID)


    


            objParaFrom.ParameterName = "@FromDate"
            objParaFrom.Value = dtFrom.Date
            objParaFrom.Direction = ParameterDirection.Input
            objParaFrom.SqlDbType = SqlDbType.DateTime

            'Pass that parameter to command
            objcmd.Parameters.Add(objParaFrom)



            objParaTo.ParameterName = "@ToDate"
            objParaTo.Value = dtTo.Date
            objParaTo.Direction = ParameterDirection.Input
            objParaTo.SqlDbType = SqlDbType.DateTime


            'Pass that parameter to command
            objcmd.Parameters.Add(objParaTo)

            'osqlpara = New SqlParameter
            'osqlpara.ParameterName = "@PatientID"
            'osqlpara.Direction = ParameterDirection.Input
            'osqlpara.DbType = DbType.Int64
            'osqlpara.Value = nPatientID


            'Declare a variable for parameter


            objParaStatus.ParameterName = "@Status"
            objParaStatus.Value = IsFinished
            objParaStatus.Direction = ParameterDirection.Input
            objParaStatus.SqlDbType = SqlDbType.Int


            'Pass that parameter to command
            objcmd.Parameters.Add(objParaStatus)

            'Declare a variable for parameter


            objParaDoctorID.ParameterName = "@nProviderID"
            objParaDoctorID.Value = nProviderId
            objParaDoctorID.Direction = ParameterDirection.Input
            objParaDoctorID.SqlDbType = SqlDbType.BigInt


            'Pass that parameter to command
            objcmd.Parameters.Add(objParaDoctorID)

            Dim da As SqlDataAdapter
            Dim dt As DataTable

            da = New SqlDataAdapter
            da.SelectCommand = objcmd
            dt = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing
            If Not dt Is Nothing Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            'Finally checks and close the connections
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End If
            If Not IsNothing(objCon) Then   'Disposed by mitesh
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(objcmd) Then
                objcmd.Parameters.Clear()
                objcmd.Dispose()
                objcmd = Nothing
            End If
            objParaFrom = Nothing
            objParaTo = Nothing
            objParaStatus = Nothing
            objParaDoctorID = Nothing
        End Try
    End Function

    Public Function Fill_Medication(ByVal nPatientID As Long) As DataTable


        'Dim objCon As New SqlConnection
        'Handle exception using try-catch-finally block
        Dim cmd As New SqlCommand("gsp_GetMedication", conn)
        Try


            cmd.CommandType = CommandType.StoredProcedure

            'Stored procedure name
            cmd.CommandText = "gsp_GetMedication"


            'Declare a variable for command
            Dim objparam1 As New SqlParameter
            objparam1.ParameterName = "@PatientID"
            objparam1.Direction = ParameterDirection.Input
            objparam1.DbType = DbType.Int64
            objparam1.Value = nPatientID

            cmd.CommandType = CommandType.StoredProcedure

            'Pass that parameter to command
            cmd.Parameters.Add(objparam1)
            'objcmd.Parameters.Add(objParaDoctorID)

            Dim da As SqlDataAdapter
            Dim dt As DataTable

            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing

            objparam1 = Nothing
            'Handle Catch block for Try
            Return dt
        Catch ex As Exception
            Fill_Medication = Nothing
            Throw ex

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'Finally checks and close the connections
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End If
            If Not IsNothing(cmd) Then   'Disposed by mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try

        'Return Data Table



    End Function

    Public Function GetVisitID(ByVal VisitDate As Date, Optional ByVal PatientID As Long = 0) As Long

        'Handle exception using try-catch-finally block

        Dim Cmd As New SqlCommand("gsp_GetVisitID", conn)
        Try
            'Call InitialzeCon()

            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@VisitDate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitDate

            objParam = Cmd.Parameters.Add("@PatientID", SqlDbType.BigInt, 18)
            objParam.Direction = ParameterDirection.Input

            objParam.Value = PatientID

            Cmd.CommandType = CommandType.StoredProcedure

            If conn.State <> ConnectionState.Open Then
                conn.Open()
            End If
            Dim VisitID As Long
            VisitID = Cmd.ExecuteScalar
            conn.Close()

            If IsDBNull(VisitID) = True Then
                '' If VisitId is Not Found then Return 0
                VisitID = 0
            End If

            objParam = Nothing
            Return VisitID

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            Return 0
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
            Return 0
        Finally
            If Not IsNothing(Cmd) Then   'Disposed by mitesh
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function


    Public Function Fill_History(ByVal PatientID As Long, ByVal VisitID As Long, ByVal Flag As Integer) As DataTable

        'Handle exception using try-catch-finally block
        Dim cmd As New SqlCommand("gsp_GetHistory", conn)

        Try
            'Dim Con As New SqlConnection(GetConnectionString)


            'Name of Store procedure
            cmd.CommandType = CommandType.StoredProcedure

            'Declaration of variable
            Dim objParam As SqlParameter
            objParam = cmd.Parameters.Add("@dtVisitdate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Now.Date

            'Pass that parameter to command
            objParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)

            objParam.Direction = ParameterDirection.Input
            objParam.Value = PatientID

            'Pass that parameter to command
            objParam = cmd.Parameters.Add("@flag", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Flag

            objParam = cmd.Parameters.Add("@VisitId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitID

            Dim da As New SqlDataAdapter
            Dim dt As New DataTable
            da.SelectCommand = cmd
            da.Fill(dt)
            objParam = Nothing
            da.Dispose()
            da = Nothing
            'Handle Catch block for Try
            Return dt
        Catch ex As Exception
            Fill_History = Nothing
            Throw ex
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'Finally checks and close the connections
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End If
            If Not IsNothing(cmd) Then   'Disposed by mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try

        'Return Data Table

    End Function



    Public Function Fill_ProblemLists(ByVal nPatientId As Long) As DataTable
        'Dim objCon As New SqlConnection
        Dim cmd As New SqlCommand("gsp_FillProblemList", conn)
        Try

            cmd.CommandType = CommandType.StoredProcedure

            Dim objparame As New SqlParameter

            objparame.ParameterName = "@PatientID"
            objparame.Direction = ParameterDirection.Input
            objparame.DbType = DbType.Int64
            objparame.Value = nPatientId

            'cmd.CommandType = CommandType.StoredProcedure

            'Pass that parameter to command
            cmd.Parameters.Add(objparame)
            'objParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt, 18)



            'cmd.Parameters.Add(objparame)
            Dim da As SqlDataAdapter
            Dim dt As DataTable
            conn.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            conn.Close()
            da.Dispose()
            da = Nothing
            objparame = Nothing
            Return dt
        Catch ex As Exception
            Fill_ProblemLists = Nothing
            Throw ex
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.SynopsisScreen, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'Finally checks and close the connections
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
            End If
            If Not IsNothing(cmd) Then   'Disposed by mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try

    End Function

    Public Sub New()

    End Sub
    ' Implement IDisposable.
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If disposed = False Then
            If disposing Then
                ' Free other state (managed objects).
                If Not IsNothing(conn) Then
                    conn.Dispose()
                    conn = Nothing
                End If
                disposed = True
            End If
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            'If Not IsNothing(dtTest) Then
            '    dtTest.Dispose()
            '    dtTest = Nothing
            'End If
            'If Not IsNothing(dtGroup) Then
            '    dtGroup.Dispose()
            '    dtGroup = Nothing
            'End If
            'If Not IsNothing(dtCategory) Then
            '    dtCategory.Dispose()
            '    dtCategory = Nothing
            'End If
           
            'If Not IsNothing(sqladpt) Then
            '    sqladpt.Dispose()
            '    sqladpt = Nothing
            'End If
            'If Not IsNothing(cmd) Then
            '    cmd.Dispose()
            '    cmd = Nothing
            'End If

        End If
    End Sub

    Protected Overrides Sub Finalize()

        Dispose(False)
    End Sub
End Class






