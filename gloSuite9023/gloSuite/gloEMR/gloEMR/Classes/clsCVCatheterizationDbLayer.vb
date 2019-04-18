
Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase

Public Class ClsCVCatheterizationDbLayer

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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
    Public Function PopulateCathDt() As DataTable
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
            Dim cmd As SqlCommand = New SqlCommand("Select sCPTCode,sDescription , isnull(sCPTCode,0)+'-'+isnull(sDescription,'') as CPTDesc from CPT_MST ", conn)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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

    Public Function PopulateCatheterizationDt(ByVal sDate As Date, ByVal patientID As Long, ByVal visitID As Long) As DataTable

        'delclare a variable for connection
        Dim conn As SqlConnection = New SqlConnection(GetConnectionString)
        Dim dt As New DataTable
        'Handle Try-Catch-Finally block 
        Try
            conn.Open()
            '''''''''" and nVisitID=" & visitID &
            Dim cmd As SqlCommand = New SqlCommand("Select distinct sCPTCode,sTestType from CV_Catheterization where nPatientID=" & patientID & " and dtProcedureDate= '" & sDate & "'", conn)

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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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

    'Function to PopulateCathUserDt
    Public Function PopulateCathUserDt(ByVal sDate As Date, ByVal patientID As Long, ByVal visitID As Long) As DataTable

        'delclare a variable for connection
        Dim conn As SqlConnection = New SqlConnection(GetConnectionString)
        Dim dt As New DataTable
        'Handle Try-Catch-Finally block 
        Try
            conn.Open()
            '''''''''nVisitID=" & visitID & " and 
            Dim cmd As SqlCommand = New SqlCommand("select distinct sUserName from CV_Catheterization where nPatientID=" & patientID & " and @dtProcedureDate='" & sDate & "'", conn)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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



            

    Public Function SaveCatheterization(ByVal CatheterizationID As Long, ByVal ClinicID As Int64, ByVal PatientID As Int64, ByVal examid As Int64, ByVal VisitID As Int64, ByVal Proceduredt As Date, ByVal GroupID As Long, Optional ByVal CPTCode As String = "", Optional ByVal TestTp As String = "", Optional ByVal PhysicianNM As String = "", Optional ByVal InterventionTP As String = "", Optional ByVal RaPressure As String = "", Optional ByVal LaPressure As String = "", Optional ByVal RPulmonary As String = "", Optional ByVal LPulmonary As String = "", Optional ByVal RV As String = "", Optional ByVal LV As String = "", Optional ByVal Peak As String = "", Optional ByVal Diastolic As String = "", Optional ByVal Mean As String = "", Optional ByVal PaPressure As String = "", Optional ByVal IVc As String = "", Optional ByVal Svc As String = "", Optional ByVal RASaturations As String = "", Optional ByVal RVSAturations As String = "", Optional ByVal PASaturations As String = "", Optional ByVal LVEjectionFraction As String = "", Optional ByVal LVDiastolicVol As String = "", Optional ByVal LVSystolicVol As String = "", Optional ByVal NarrativeSmry As String = "", Optional ByVal ModEntry As Boolean = False) As Long

        Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim CathId As Long = 0

        'Dim trCatheterization As SqlTransaction
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        'trCatheterization = Con.BeginTransaction()
        ''Dim objCatheterization As Cls_CardioVascular
        Try

            '' '' ''For i As Int16 = 0 To ArrList.Count - 1

            ''objCatheterization = New Cls_CardioVascular
            ''objCatheterization = CType(ArrList(0), Cls_CardioVascular)
            Dim CathParam As SqlParameter

            If ModEntry = True Then



                '''' if exist then delete that Catheterization
                cmd = New SqlCommand("CV_DeleteCatheterization", Con)
                cmd.CommandType = CommandType.StoredProcedure
                'cmd.Transaction = trCatheterization

                CathParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                CathParam.Direction = ParameterDirection.Input
                CathParam.Value = PatientID


                CathParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
                CathParam.Direction = ParameterDirection.Input
                CathParam.Value = VisitID

                CathParam = cmd.Parameters.Add("@dtProcedureDate", SqlDbType.DateTime)
                CathParam.Direction = ParameterDirection.Input
                CathParam.Value = Convert.ToDateTime(Proceduredt.Date)


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
            cmd = New SqlCommand("CV_InUpCatheterization", Con)
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.Transaction = trCatheterization


            CathParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = PatientID
            'oDB.DBParametersCol.Add(CathParam)

            CathParam = cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = examid
            'oDB.DBParametersCol.Add(CathParam)

            CathParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = VisitID
            'oDB.DBParametersCol.Add(CathParam)

            CathParam = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = ClinicID

            CathParam = cmd.Parameters.Add("@dtProcedureDate", SqlDbType.DateTime)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = Convert.ToDateTime(Proceduredt)
            ''Now.Date
            ''Convert.ToDateTime(Proceduredt)
            CathParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = GetPrefixTransactionID()

            CathParam = cmd.Parameters.AddWithValue("@nCatheterizationID", CatheterizationID)
            CathParam.Direction = ParameterDirection.InputOutput
            CathParam.Value = CatheterizationID


            CathParam = cmd.Parameters.AddWithValue("@nGroupID", CatheterizationID)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = GroupID

            ''''''''''''''''''''''''''''''
            CathParam = cmd.Parameters.Add("@sCPTCode", SqlDbType.VarChar, 500)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(CPTCode) Then
                CathParam.Value = CPTCode
            End If

            CathParam = cmd.Parameters.Add("@sTestType", SqlDbType.VarChar, 500)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(TestTp) Then
                CathParam.Value = TestTp
            End If

            CathParam = cmd.Parameters.Add("@sPhysicianName", SqlDbType.VarChar, 2000)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(PhysicianNM) Then
                CathParam.Value = PhysicianNM
            End If

            CathParam = cmd.Parameters.Add("@sInterventionType", SqlDbType.VarChar, 2000)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(InterventionTP) Then
                CathParam.Value = InterventionTP
            End If

            CathParam = cmd.Parameters.Add("@sRAPressure", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(RaPressure) Then
                CathParam.Value = RaPressure
            End If

            CathParam = cmd.Parameters.Add("@sLAPressure", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(LaPressure) Then
                CathParam.Value = LaPressure
            End If

            CathParam = cmd.Parameters.Add("@sRPulmonary", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(RPulmonary) Then
                CathParam.Value = RPulmonary
            End If

            CathParam = cmd.Parameters.Add("@sLPulmonary", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(LPulmonary) Then
                CathParam.Value = LPulmonary
            End If

            CathParam = cmd.Parameters.Add("@sRV", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(RV) Then
                CathParam.Value = RV
            End If

            CathParam = cmd.Parameters.Add("@sLV", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(LV) Then
                CathParam.Value = LV
            End If

            CathParam = cmd.Parameters.Add("@sPeak", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(Peak) Then
                CathParam.Value = Peak
            End If

            CathParam = cmd.Parameters.Add("@sDiastolic", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(Diastolic) Then
                CathParam.Value = Diastolic
            End If

            CathParam = cmd.Parameters.Add("@sMean", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(Mean) Then
                CathParam.Value = Mean
            End If

            CathParam = cmd.Parameters.Add("@sPAPressure", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(PaPressure) Then
                CathParam.Value = PaPressure
            End If

            CathParam = cmd.Parameters.Add("@sIVC", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(IVc) Then
                CathParam.Value = IVc
            End If

            CathParam = cmd.Parameters.Add("@sSVC", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(Svc) Then
                CathParam.Value = Svc
            End If

            CathParam = cmd.Parameters.Add("@sRASaturations", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(RASaturations) Then
                CathParam.Value = RASaturations
            End If

            CathParam = cmd.Parameters.Add("@sRVSaturations", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(RVSAturations) Then
                CathParam.Value = RVSAturations
            End If

            CathParam = cmd.Parameters.Add("@sPASaturations", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(PASaturations) Then
                CathParam.Value = PASaturations
            End If

            CathParam = cmd.Parameters.Add("@sLVEjectionFraction", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(LVEjectionFraction) Then
                CathParam.Value = LVEjectionFraction
            End If

            CathParam = cmd.Parameters.Add("@sLVDiastolicVol", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(LVDiastolicVol) Then
                CathParam.Value = LVDiastolicVol
            End If

            CathParam = cmd.Parameters.Add("@sLVSystolicVol", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(LVSystolicVol) Then
                CathParam.Value = LVSystolicVol
            End If

            CathParam = cmd.Parameters.Add("@sNarrativeSummary", SqlDbType.VarChar, 5000)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(NarrativeSmry) Then
                CathParam.Value = NarrativeSmry
            End If
            ''''''''''''''''''''''''''''''

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            If cmd.ExecuteNonQuery() > 0 Then
                CathId = cmd.Parameters("@nCatheterizationID").Value
                ''nCatheterizationID = CathId
            End If

            ''If nCatheterizationID = 0 Then
            ''    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Add, "Catheterization Added", gloAuditTrail.ActivityOutCome.Success)
            ''Else
            ''    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Modify, "Catheterization could not be Modified", gloAuditTrail.ActivityOutCome.Success)
            ''End If

            ' '' ''Next
            ''trCatheterization.Commit()
            CathParam = Nothing
            Return CathId
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ''If CatheterizationID = 0 Then
            ''    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Add, "Catheterization could not be Added", gloAuditTrail.ActivityOutCome.Failure)
            ''Else
            ''    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Catheterization could not be Modified", gloAuditTrail.ActivityOutCome.Failure)
            ''End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ''trCatheterization.Rollback()
            Return False
        Catch ex As Exception
            ''If nCatheterizationID = 0 Then
            ''    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Catheterization could not be Added", gloAuditTrail.ActivityOutCome.Failure)
            ''Else
            ''    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Catheterization could not be Modified", gloAuditTrail.ActivityOutCome.Failure)
            ''End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ''trCatheterization.Rollback()
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
    End Function

    Public Function SaveCatheterization11(ByVal ArrList As ArrayList, Optional ByVal FirstEntry As Boolean = False) As Long

        Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim CathId As Long = 0

        'Dim trCatheterization As SqlTransaction
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        'trCatheterization = Con.BeginTransaction()
        Dim objCatheterization As Cls_CardioVascular = Nothing
        Try

            '' '' ''For i As Int16 = 0 To ArrList.Count - 1

            'objCatheterization = New Cls_CardioVascular
            objCatheterization = CType(ArrList(0), Cls_CardioVascular)
            Dim CathParam As SqlParameter

            If FirstEntry = True Then



                '''' if exist then delete that Catheterization
                cmd = New SqlCommand("CV_DeleteCatheterization", Con)
                cmd.CommandType = CommandType.StoredProcedure
                'cmd.Transaction = trCatheterization

                CathParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                CathParam.Direction = ParameterDirection.Input
                CathParam.Value = objCatheterization.nPatientID


                CathParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
                CathParam.Direction = ParameterDirection.Input
                CathParam.Value = objCatheterization.nVisitID

                CathParam = cmd.Parameters.Add("@dtProcedureDate", SqlDbType.DateTime)
                CathParam.Direction = ParameterDirection.Input
                CathParam.Value = objCatheterization.dtproceduredate


                If Con.State = ConnectionState.Closed Then
                    Con.Open()
                End If

                If cmd.ExecuteNonQuery() > 0 Then
                    'Dim objAudit As New clsAudit
                    ''objAudit.CreateLog(clsAudit.enmActivityType.Delete, "'Problem List Deleted for date " & objCatheterization.VisitDate & "'", gstrLoginName, gstrClientMachineName, gnPatientID)
                    'objAudit = Nothing
                End If
                If cmd IsNot Nothing Then
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                End If

            End If
            '' Insert Or Update problem List
            cmd = New SqlCommand("CV_InUpCatheterization", Con)
            cmd.CommandType = CommandType.StoredProcedure
            'cmd.Transaction = trCatheterization


            CathParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = objCatheterization.nPatientID
            'oDB.DBParametersCol.Add(CathParam)
            
            CathParam = cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = objCatheterization.nexamid
            'oDB.DBParametersCol.Add(CathParam)

            CathParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = objCatheterization.nVisitID
            'oDB.DBParametersCol.Add(CathParam)

            CathParam = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = objCatheterization.nClinicID

            CathParam = cmd.Parameters.Add("@dtProcedureDate", SqlDbType.DateTime)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = objCatheterization.dtproceduredate

            CathParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = GetPrefixTransactionID()

            CathParam = cmd.Parameters.AddWithValue("@nCatheterizationID", objCatheterization.nCatheterizationID)
            CathParam.Direction = ParameterDirection.InputOutput
            CathParam.Value = objCatheterization.nCatheterizationID


            CathParam = cmd.Parameters.AddWithValue("@nGroupID", objCatheterization.nCatheterizationID)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = objCatheterization.nGroupID

            ''''''''''''''''''''''''''''''
            CathParam = cmd.Parameters.Add("@sCPTCode", SqlDbType.VarChar, 100)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sCPTCode) Then
                CathParam.Value = objCatheterization.sCPTCode
            End If

            CathParam = cmd.Parameters.Add("@sTestType", SqlDbType.VarChar, 100)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sTestType) Then
                CathParam.Value = objCatheterization.sTestType
            End If

            CathParam = cmd.Parameters.Add("@sPhysicianName", SqlDbType.VarChar, 2000)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sPhysicianName) Then
                CathParam.Value = objCatheterization.sPhysicianName
            End If

            CathParam = cmd.Parameters.Add("@sInterventionType", SqlDbType.VarChar, 100)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sInterventionType) Then
                CathParam.Value = objCatheterization.sInterventionType
            End If

            CathParam = cmd.Parameters.Add("@sRAPressure", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sRaPressure) Then
                CathParam.Value = objCatheterization.sRaPressure
            End If

            CathParam = cmd.Parameters.Add("@sLAPressure", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sLaPressure) Then
                CathParam.Value = objCatheterization.sLaPressure
            End If

            CathParam = cmd.Parameters.Add("@sRPulmonary", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sRPulmonary) Then
                CathParam.Value = objCatheterization.sRPulmonary
            End If

            CathParam = cmd.Parameters.Add("@sLPulmonary", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sLPulmonary) Then
                CathParam.Value = objCatheterization.sLPulmonary
            End If

            CathParam = cmd.Parameters.Add("@sRV", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sRV) Then
                CathParam.Value = objCatheterization.sRV
            End If

            CathParam = cmd.Parameters.Add("@sLV", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sLV) Then
                CathParam.Value = objCatheterization.sLV
            End If

            CathParam = cmd.Parameters.Add("@sPeak", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sPeak) Then
                CathParam.Value = objCatheterization.sPeak
            End If

            CathParam = cmd.Parameters.Add("@sDiastolic", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sDiastolic) Then
                CathParam.Value = objCatheterization.sDiastolic
            End If

            CathParam = cmd.Parameters.Add("@sMean", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sMean) Then
                CathParam.Value = objCatheterization.sMean
            End If

            CathParam = cmd.Parameters.Add("@sPAPressure", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sPaPressure) Then
                CathParam.Value = objCatheterization.sPaPressure
            End If

            CathParam = cmd.Parameters.Add("@sIVC", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sIVc) Then
                CathParam.Value = objCatheterization.sIVc
            End If

            CathParam = cmd.Parameters.Add("@sSVC", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sSvc) Then
                CathParam.Value = objCatheterization.sSvc
            End If

            CathParam = cmd.Parameters.Add("@sRASaturations", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sRASaturations) Then
                CathParam.Value = objCatheterization.sRASaturations
            End If

            CathParam = cmd.Parameters.Add("@sRVSaturations", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sRVSAturations) Then
                CathParam.Value = objCatheterization.sRVSAturations
            End If

            CathParam = cmd.Parameters.Add("@sPASaturations", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sPASaturations) Then
                CathParam.Value = objCatheterization.sPASaturations
            End If

            CathParam = cmd.Parameters.Add("@sLVEjectionFraction", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sLVEjectionFraction) Then
                CathParam.Value = objCatheterization.sLVEjectionFraction
            End If

            CathParam = cmd.Parameters.Add("@sLVDiastolicVol", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sLVDiastolicVol) Then
                CathParam.Value = objCatheterization.sLVDiastolicVol
            End If

            CathParam = cmd.Parameters.Add("@sLVSystolicVol", SqlDbType.VarChar, 50)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sLVSystolicVol) Then
                CathParam.Value = objCatheterization.sLVSystolicVol
            End If

            CathParam = cmd.Parameters.Add("@sNarrativeSummary", SqlDbType.VarChar, 5000)
            CathParam.Direction = ParameterDirection.Input
            If Not IsNothing(objCatheterization.sNarrativeSummary) Then
                CathParam.Value = objCatheterization.sNarrativeSummary
            End If
            ''''''''''''''''''''''''''''''

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            If cmd.ExecuteNonQuery() > 0 Then
                CathId = cmd.Parameters("@nCatheterizationID").Value
                ''objCatheterization.nCatheterizationID = CathId
            End If

            'If objCatheterization.nCatheterizationID = 0 Then
            '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Add, "Catheterization Added", gloAuditTrail.ActivityOutCome.Success)
            'Else
            '    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Modify, "Catheterization could not be Modified", gloAuditTrail.ActivityOutCome.Success)
            'End If

            '' ''Next
            'trCatheterization.Commit()
            CathParam = Nothing
            Return CathId
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            If objCatheterization.nCatheterizationID = 0 Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Add, "Catheterization could not be Added", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 201000915
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Add, "Catheterization could not be Added", objCatheterization.nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Failure)
                ''
            Else
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Catheterization could not be Modified", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 201000915
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Catheterization could not be Modified", objCatheterization.nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Failure)
                ''
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'trCatheterization.Rollback()
            Return False
        Catch ex As Exception
            If objCatheterization.nCatheterizationID = 0 Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Catheterization could not be Added", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 201000915
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Add, "Catheterization could not be Added", objCatheterization.nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Failure)
                ''
            Else
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Catheterization could not be Modified", gloAuditTrail.ActivityOutCome.Failure)
                ''Added Rahul P on 201000915
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.Ejectionfraction, gloAuditTrail.ActivityType.Modify, "Catheterization could not be Modified", objCatheterization.nPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Failure)
                ''
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'trCatheterization.Rollback()
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
    End Function

    Public Function DeleteCatheterization(ByVal mPatientID As Int64, ByVal mVisitId As Int64, ByVal mdtproceduredate As Date)
        Dim Con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing

        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If

        Dim CathParam As SqlParameter
        Try

            '''' if exist then delete that Catheterization
            cmd = New SqlCommand("CV_DeleteCatheterization", Con)
            cmd.CommandType = CommandType.StoredProcedure


            CathParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = mPatientID


            CathParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = mVisitId

            CathParam = cmd.Parameters.Add("@dtProcedureDate", SqlDbType.DateTime)
            CathParam.Direction = ParameterDirection.Input
            CathParam.Value = Convert.ToDateTime(mdtproceduredate)


            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            If cmd.ExecuteNonQuery() > 0 Then

            End If


            CathParam = Nothing

            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Delete, "CV CVCatheterization deleted.", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 201000915
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Delete, "CV CVCatheterization deleted.", mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Success)
            ''
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Delete, "CV CVCatheterization could not be deleted.", gloAuditTrail.ActivityOutCome.Failure).
            ''Added Rahul P on 201000915
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Synopsis, gloAuditTrail.ActivityCategory.CVCatheterization, gloAuditTrail.ActivityType.Delete, "CV CVCatheterization could not be deleted.", mPatientID, 0, gnPatientProviderID, gloAuditTrail.ActivityOutCome.Failure)
            ''
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

        Return True
    End Function



End Class
