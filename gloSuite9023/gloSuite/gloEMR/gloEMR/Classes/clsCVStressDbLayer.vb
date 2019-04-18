
Imports System.Data.SqlClient

Public Class ClsCVStressDbLayer

    'function to populate data table of Users
    Public Function PopulateTypeDt() As DataTable
        Dim dt As New DataTable
        Dim conn As SqlConnection = New SqlConnection(GetConnectionString)
        Try

            'conn = New SqlConnection("server=gloint;database=gloEMR50_CCHIT2008_1;Integrated security=True")
            conn.Open()
            Dim cmd As SqlCommand = New SqlCommand("Select sDescription from Category_MST where sCategoryType='Cardio Test Type'", conn)
            Dim ad As SqlDataAdapter = New SqlDataAdapter(cmd)
            ad.Fill(dt)
            ad.Dispose()
            ad = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            'Handle exception if connection is not established
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Connection Fail", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)

            'Finally checks and close the connections
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
        Return dt
    End Function
    'function to populate data table of Users
    Public Function PopulateUserDt() As DataTable
        Dim dt As New DataTable
        Dim conn As SqlConnection = New SqlConnection(GetConnectionString)
        Try

            'conn = New SqlConnection("server=gloint;database=gloEMR50_CCHIT2008_1;Integrated security=True")
            conn.Open()
            Dim cmd As SqlCommand = New SqlCommand("Select sLoginName ,isnull(sfirstname,'') + Space(1) + isnull(slastname,'') as LoginName from User_MST ", conn)
            Dim ad As SqlDataAdapter = New SqlDataAdapter(cmd)
            ad.Fill(dt)
            ad.Dispose()
            ad = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            'Handle exception if connection is not established
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Connection Fail", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)

            'Finally checks and close the connections
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
        Return dt
    End Function
    Public Function PopulateStressDt() As DataTable
        Dim dt As New DataTable
        Dim conn As SqlConnection = New SqlConnection(GetConnectionString)
        Try

            'conn = New SqlConnection("server=gloint;database=gloEMR50_CCHIT2008_1;Integrated security=True")
            conn.Open()
            Dim cmd As SqlCommand = New SqlCommand("Select sLoginName from User_MST ", conn)
            Dim ad As SqlDataAdapter = New SqlDataAdapter(cmd)
            ad.Fill(dt)
            ad.Dispose()
            ad = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            'Handle exception if connection is not established
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Connection Fail", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)

            'Finally checks and close the connections
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
        Return dt
    End Function
    'function to populate data table of Procedure
    Public Function PopulateCptDt() As DataTable
        Dim dt As New DataTable
        Dim conn As SqlConnection = New SqlConnection(GetConnectionString)

        'Handle Try-Catch-Finally block 
        Try

            'conn = New SqlConnection("server=gloint;database=gloEMR50_CCHIT2008_1;Integrated security=True")
            conn.Open()
            Dim cmd As SqlCommand = New SqlCommand("Select sCPT,sDescription , isnull(sCPT,0)+'-'+isnull(sDescription,'') as CPTDesc from CPT_MST ", conn)
            Dim ad As SqlDataAdapter = New SqlDataAdapter(cmd)
            ad.Fill(dt)
            ad.Dispose()
            ad = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            'Handle exception if connection is not established
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Connection Fail", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)

            'Finally checks and close the connections
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
        Return dt
    End Function

    Public Function PopulateStressTestDt(ByVal sDate As Date, ByVal patientID As Long, ByVal visitID As Long) As DataTable

        'delclare a variable for connection
        Dim conn As SqlConnection = New SqlConnection(GetConnectionString)
        Dim dt As New DataTable
        'Handle Try-Catch-Finally block 
        Try
            conn.Open()
            '' Dim cmd As SqlCommand = New SqlCommand("select isnull(sCPT,0)as Procedure,isnull(sResult,'')as Result,isnull(sTestType,'')as TestType from CV_StressTest where nPatientID=" & patientID & " and nVisitID=" & visitID & " and dtDateOfStudy= '" & sDate & "'", conn)
            Dim cmd As SqlCommand = New SqlCommand("Select distinct sCPT,sResult,sTestType from CV_StressTest where nPatientID=" & patientID & " and nVisitID=" & visitID & " and dtDateOfStudy= '" & sDate & "'", conn)

            Dim ad As SqlDataAdapter = New SqlDataAdapter(cmd)
            ad.Fill(dt)
            ad.Dispose()
            ad = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            'Handle exception if connection is not established
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Connection Fail", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)

            'Finally checks and close the connections
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
        Return dt

    End Function

    'Function to populateStressUserDt
    Public Function PopulateStressUserDt(ByVal sDate As Date, ByVal patientID As Long, ByVal visitID As Long) As DataTable

        'delclare a variable for connection
        Dim conn As SqlConnection = New SqlConnection(GetConnectionString)
        Dim dt As New DataTable
        'Handle Try-Catch-Finally block 
        Try
            conn.Open()
            Dim cmd As SqlCommand = New SqlCommand("select distinct sUserName from CV_StressTest where nPatientID=" & patientID & " and nVisitID=" & visitID & " and dtDateOfStudy='" & sDate & "'", conn)
            Dim ad As SqlDataAdapter = New SqlDataAdapter(cmd)
            ad.Fill(dt)
            ad.Dispose()
            ad = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            'Handle exception if connection is not established
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVStressTest, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Connection Fail", MessageBoxButtons.OKCancel, MessageBoxIcon.Error)

            'Finally checks and close the connections
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                End If
                conn.Dispose()
                conn = Nothing
            End If
        End Try
        Return dt
    End Function

    Public Function SaveStressTest(ByVal StressTestID As Long, ByVal ClinicID As Int64, ByVal PatientID As Int64, ByVal examid As Int64, ByVal VisitID As Int64, ByVal dateofStudy As Date, ByVal GroupID As Long, Optional ByVal CPTCode As String = "", Optional ByVal TestTp As String = "", Optional ByVal PhysicianNM As String = "", Optional ByVal Result As String = "", Optional ByVal TotExerciseTime As String = "", Optional ByVal RestingHeartRate As Long = 0, Optional ByVal RestingBPMin As Long = 0, Optional ByVal RestingBPMax As Long = 0, Optional ByVal PeakHeartRate As Long = 0, Optional ByVal PeakBPMin As Long = 0, Optional ByVal PeakBPMax As Long = 0, Optional ByVal EjectionFraction As String = "", Optional ByVal NarrativeSmry As String = "", Optional ByVal ModEntry As Boolean = False) As Long
        ''''''''''' Added by Ujwala Atre for new Stress Test moudle - as on 20101018
        Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim StressID As Long

        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If

        Try
            Dim StressParam As SqlParameter

            If ModEntry = True Then


                '''' if exist then delete that Catheterization
                cmd = New SqlCommand("CV_DeleteStressTest", Con)
                cmd.CommandType = CommandType.StoredProcedure
                'cmd.Transaction = trCatheterization

                StressParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
                StressParam.Direction = ParameterDirection.Input
                StressParam.Value = PatientID


                StressParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
                StressParam.Direction = ParameterDirection.Input
                StressParam.Value = VisitID

                StressParam = cmd.Parameters.Add("@DateofStudy", SqlDbType.DateTime)
                StressParam.Direction = ParameterDirection.Input
                StressParam.Value = Convert.ToDateTime(dateofStudy.Date)


                If Con.State = ConnectionState.Closed Then
                    Con.Open()
                End If

                If cmd.ExecuteNonQuery() > 0 Then
                    'Dim objAudit As New clsAudit
                    ''objAudit.CreateLog(clsAudit.enmActivityType.Delete, "'Problem List Deleted for date " & VisitDate & "'", gstrLoginName, gstrClientMachineName, gnPatientID)
                    'objAudit = Nothing
                End If
                If cmd IsNot Nothing Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If
            End If
            '' Insert Or Update problem List
            cmd = New SqlCommand("CV_InUpStressTests", Con)
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.Transaction = trCatheterization


            StressParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            StressParam.Direction = ParameterDirection.Input
            StressParam.Value = PatientID
            'oDB.DBParametersCol.Add(StressParam)

            StressParam = cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
            StressParam.Direction = ParameterDirection.Input
            StressParam.Value = examid
            'oDB.DBParametersCol.Add(StressParam)

            StressParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            StressParam.Direction = ParameterDirection.Input
            StressParam.Value = VisitID
            'oDB.DBParametersCol.Add(StressParam)

            StressParam = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
            StressParam.Direction = ParameterDirection.Input
            StressParam.Value = ClinicID

            StressParam = cmd.Parameters.Add("@dtDateOfStudy", SqlDbType.DateTime)
            StressParam.Direction = ParameterDirection.Input
            StressParam.Value = Convert.ToDateTime(dateofStudy)
            ''Now.Date
            ''Convert.ToDateTime(Proceduredt)
            StressParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            StressParam.Direction = ParameterDirection.Input
            StressParam.Value = GetPrefixTransactionID()

            StressParam = cmd.Parameters.Add("@nStressID", SqlDbType.BigInt)
            StressParam.Direction = ParameterDirection.InputOutput
            StressParam.Value = StressTestID


            StressParam = cmd.Parameters.Add("@nGroupID", SqlDbType.BigInt)
            StressParam.Direction = ParameterDirection.Input
            StressParam.Value = GroupID

            ''''''''''''''''''''''''''''''
            StressParam = cmd.Parameters.Add("@sCPT", SqlDbType.VarChar, 500)
            StressParam.Direction = ParameterDirection.Input
            If Not IsNothing(CPTCode) Then
                StressParam.Value = CPTCode
            End If

            StressParam = cmd.Parameters.Add("@sTestType", SqlDbType.VarChar, 500)
            StressParam.Direction = ParameterDirection.Input
            If Not IsNothing(TestTp) Then
                StressParam.Value = TestTp
            End If

            StressParam = cmd.Parameters.Add("@sUserName", SqlDbType.VarChar, 2000)
            StressParam.Direction = ParameterDirection.Input
            If Not IsNothing(PhysicianNM) Then
                StressParam.Value = PhysicianNM
            End If

            StressParam = cmd.Parameters.Add("@sResult", SqlDbType.VarChar, 100)
            StressParam.Direction = ParameterDirection.Input
            If Not IsNothing(Result) Then
                StressParam.Value = Result
            End If

            StressParam = cmd.Parameters.Add("@sTotExerciseTime", SqlDbType.VarChar, 50)
            StressParam.Direction = ParameterDirection.Input
            If Not IsNothing(TotExerciseTime) Then
                StressParam.Value = TotExerciseTime
            End If

            StressParam = cmd.Parameters.Add("@nRestingHeartRate", SqlDbType.BigInt)
            StressParam.Direction = ParameterDirection.Input
            If Not IsNothing(RestingHeartRate) Then
                StressParam.Value = RestingHeartRate
            End If

            StressParam = cmd.Parameters.Add("@nRestingBPMin", SqlDbType.BigInt)
            StressParam.Direction = ParameterDirection.Input
            If Not IsNothing(RestingBPMin) Then
                StressParam.Value = RestingBPMin
            End If

            StressParam = cmd.Parameters.Add("@nRestingBPMax", SqlDbType.BigInt)
            StressParam.Direction = ParameterDirection.Input
            If Not IsNothing(RestingBPMax) Then
                StressParam.Value = RestingBPMax
            End If

            StressParam = cmd.Parameters.Add("@nPeakHeartRate", SqlDbType.BigInt)
            StressParam.Direction = ParameterDirection.Input
            If Not IsNothing(PeakHeartRate) Then
                StressParam.Value = PeakHeartRate
            End If

            StressParam = cmd.Parameters.Add("@nPeakBPMin", SqlDbType.BigInt)
            StressParam.Direction = ParameterDirection.Input
            If Not IsNothing(PeakBPMin) Then
                StressParam.Value = PeakBPMin
            End If

            StressParam = cmd.Parameters.Add("@nPeakBPMax", SqlDbType.BigInt)
            StressParam.Direction = ParameterDirection.Input
            If Not IsNothing(PeakBPMax) Then
                StressParam.Value = PeakBPMax
            End If

            StressParam = cmd.Parameters.Add("@sEjectionFraction", SqlDbType.VarChar, 50)
            StressParam.Direction = ParameterDirection.Input
            If Not IsNothing(EjectionFraction) Then
                StressParam.Value = EjectionFraction
            End If

            StressParam = cmd.Parameters.Add("@sNarrativeSummary", SqlDbType.VarChar, 5000)
            StressParam.Direction = ParameterDirection.Input
            If Not IsNothing(NarrativeSmry) Then
                StressParam.Value = NarrativeSmry
            End If
            ''''''''''''''''''''''''''''''

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            If cmd.ExecuteNonQuery() > 0 Then
                StressID = cmd.Parameters("@nStressID").Value
            End If
            StressParam = Nothing
            Return StressID
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ''If objStressTest.StressID = 0 Then
            ''    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Stress Test could not be Added", gstrLoginName, gstrClientMachineName, objStressTest.PatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
            ''    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Stress Test could not be Added", gloAuditTrail.ActivityOutCome.Failure)
            ''Else
            ''    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Stress Test  could not be Modified", gstrLoginName, gstrClientMachineName, objStressTest.PatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
            ''    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Stress Test could not be Modified", gloAuditTrail.ActivityOutCome.Failure)
            ''End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)            
            Return False
        Catch ex As Exception
            ''If objStressTest.StressID = 0 Then
            ''    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Stress Test could not be Added", gstrLoginName, gstrClientMachineName, objStressTest.PatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
            ''    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Stress Test could not be Added", gloAuditTrail.ActivityOutCome.Failure)
            ''Else
            ''    'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Stress Test could not be Modified", gstrLoginName, gstrClientMachineName, objStressTest.PatientID, True, gloAuditTrail.enmOutCome.Failure, "gloEMR")
            ''    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Stress Test could not be Modified", gloAuditTrail.ActivityOutCome.Failure)
            ''End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ''trStressTest.Rollback()
            Return False
        Finally

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(Con) Then
                If Con.State = ConnectionState.Open Then
                    Con.Close()
                End If
                Con.Dispose()
                Con = Nothing
            End If

        End Try
        ''''''''''' Added by Ujwala Atre for new Stress Test moudle - as on 20101018
    End Function

    
End Class
