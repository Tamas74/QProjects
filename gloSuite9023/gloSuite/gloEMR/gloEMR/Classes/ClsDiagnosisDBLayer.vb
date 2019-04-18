Imports System.Data.SqlClient
Imports gloDatabaseLayer

Public Class ClsDiagnosisDBLayer
    Implements IDisposable

    Private Conn As SqlConnection
    Private Dv As DataView


    Public Sub New()
        Dim sqlconn As String
        Try
            sqlconn = GetConnectionString()
            Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- New -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- New -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
        End Try
    End Sub

    Public ReadOnly Property DsDataview() As DataView
        Get

            Return Dv

        End Get

    End Property
    Public Sub SortDataview(ByVal strsort As String)
        Dv.Sort = strsort
    End Sub

    Public Sub ClearCol()

    End Sub
    'Added Code for SavingMyWidth for DxCPT and SmartDx 
    Public Function AddMyWidthSetting(ByVal Name As String, ByVal Value As String, ByVal ClinicID As Int64, ByVal UserID As Int64, ByVal UserClinicFlag As Boolean) As Boolean

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oDBParameters As gloDatabaseLayer.DBParameters = New gloDatabaseLayer.DBParameters()
        Try
            oDB.Connect(False)

            oDBParameters.Add("@sSettingsName", Name, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@sSettingsValue", Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@nClinicID", ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@nUserID", UserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@nUserClinicFlag", UserClinicFlag.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)

            oDB.Execute("gsp_InUpSettings", oDBParameters)
            Return True

        Catch
            Return False

        Finally
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDB.Dispose()
        End Try
    End Function

    Public Function GetPQRSCodes(ByVal nPatientID As Int64) As DataTable

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oDBParameters As gloDatabaseLayer.DBParameters = New gloDatabaseLayer.DBParameters()
        Dim dtPqrs As DataTable = Nothing
        Try
            oDB.Connect(False)

            oDBParameters.Add("@nPatientID", nPatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)

            oDB.Retrive("gsp_GetPQRSHistoryCodes", oDBParameters, dtPqrs)
            Return dtPqrs

        Catch
            Return Nothing

        Finally

            oDB.Disconnect()
            oDBParameters.Dispose()
            oDB.Dispose()
        End Try
    End Function

    'Added Code for GettingMyWidth for DxCPT and SmartDx 
    Public Function GetMyWidthSetting(ByVal SettingName As String) As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim dtTable As New DataTable
        Try
            objCon.ConnectionString = GetConnectionString()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WHERE sSettingsName = '" & SettingName & "'  AND nClinicID = " & gnClinicID & " AND nUserID = " & gnLoginID & ""
            objCmd.Connection = objCon
            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            objDA.Fill(dtTable)
            objCon.Close()

            objDA.Dispose() : objDA = Nothing

            Return dtTable

        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose() : objCon = Nothing
            End If

            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose() : objCmd = Nothing
            End If

            If IsNothing(dtTable) Then
                dtTable = Nothing
            End If
        End Try

    End Function

    Public Function GetAllICD9() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Try
            Dim dtICD9 As DataTable = Nothing

            Dim _Query As String = "SELECT nICD9ID,sICD9Code,sDescription FROM ICD9 WHERE sICD9Code <> ''"
            oDB.Connect(False)
            oDB.Retrive_Query(_Query, dtICD9)
            If dtICD9 IsNot Nothing Then
                Return dtICD9
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function

    

    Public Function GetICD9To10Mapping(ByVal sICD9Code As String) As DataTable

        Dim adpt As New SqlDataAdapter
        Dim dt As New DataTable
        Dim cmd As SqlCommand = Nothing

        Try

            '20-Oct-14 Aniket: Resolving Bug #75090: Problem No : 00000788
            cmd = New SqlCommand("ICD10_ICD9To10Codes", Conn)

            cmd.CommandType = CommandType.StoredProcedure
            adpt.SelectCommand = cmd

            Dim objParam As SqlParameter

            objParam = cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = sICD9Code

            adpt.Fill(dt)
            Conn.Close()

            objParam = Nothing

            Return dt

        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Load, "ClsDiagnosisDBLayer -- FillICD -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Load, "ClsDiagnosisDBLayer -- FillICD -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Not IsNothing(adpt) Then  ''free adpt against problem 00000591
                adpt.Dispose()
                adpt = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
        End Try
    End Function

    Public Function FillPatientProblemDiagnosis(ByVal ICDRevision As Int16, ByVal SearchString As String, Optional ByVal nPatientID As Int64 = 0) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim adpt As New SqlDataAdapter
        Dim dt As New DataTable
        Try
            cmd = New SqlCommand("ICD10_GetPatientProblemList", Conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim objparam As SqlParameter
            'objparam = cmd.Parameters.Add("@Flag", SqlDbType.Int)
            'objparam.Direction = ParameterDirection.Input
            'objparam.Value = Flag
            'objparam = Nothing

            objparam = cmd.Parameters.Add("@nICDRevision", SqlDbType.SmallInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = ICDRevision
            objparam = Nothing

            objparam = cmd.Parameters.Add("@SearchString", SqlDbType.VarChar)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = SearchString
            objparam = Nothing

            objparam = cmd.Parameters.Add("@nPatientId", SqlDbType.VarChar)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = nPatientID
            objparam = Nothing
            '''''' if Flag=0 then Orderby ICD9COde
            ''''''''Else Orderby ICD9Description

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            adpt.SelectCommand = cmd

            adpt.Fill(dt)
            Conn.Close()
            Return dt

        Catch ex As Exception

            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(adpt) Then
                adpt.Dispose()
                adpt = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    Public Function GetAllICD9ICD10(ByVal nICDRevision As Int16, ByVal SearchString As String) As DataTable

        Dim adpt As New SqlDataAdapter
        Dim dt As New DataTable
        Dim cmd As SqlCommand = Nothing

        Try

            '20-Oct-14 Aniket: Resolving Bug #75090: Problem No : 00000788
            cmd = New SqlCommand("gsp_Diagnosis_Search_gloEMR", Conn)

            cmd.CommandType = CommandType.StoredProcedure
            adpt.SelectCommand = cmd

            Dim objParam As SqlParameter

            objParam = cmd.Parameters.Add("@SearchString", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = SearchString


            objParam = cmd.Parameters.Add("@nICDRevision", SqlDbType.SmallInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = nICDRevision

            adpt.Fill(dt)
            Conn.Close()

            objParam = Nothing

            Return dt

        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Load, "ClsDiagnosisDBLayer -- FillICD -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Load, "ClsDiagnosisDBLayer -- FillICD -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Not IsNothing(adpt) Then  ''free adpt against problem 00000591
                adpt.Dispose()
                adpt = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
        End Try
    End Function
    Public Function FillICD9(Optional ByVal flag1 As Int16 = 0) As DataTable
        Dim adpt As New SqlDataAdapter
        Dim dt As New DataTable
        Dim objParam As SqlParameter
        Dim Cmd As SqlCommand = Nothing
        Try
            Cmd = New SqlCommand("gsp_FillICD9", Conn)

            Cmd.CommandType = CommandType.StoredProcedure
            adpt.SelectCommand = Cmd



            objParam = Cmd.Parameters.Add("@flag", SqlDbType.Char)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = "D"

            objParam = Cmd.Parameters.Add("@flag1", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = flag1

            adpt.Fill(dt)
            Conn.Close()
            Return dt

            ''''' -- dt(0)= ICD9Code
            ''''' -- dt(1)= ICD9Code+Description
            ''''' -- dt(2)= Description
            ''''' -- dt(3)= Speciality
            ''''' -- dt(4)= ICD9ID
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- FillICD9 -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- FillICD9 -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Not IsNothing(adpt) Then  ''free adpt against problem 00000591
                adpt.Dispose()
                adpt = Nothing
            End If
            'If Not IsNothing(dt) Then  ''free adpt against problem 00000591
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If

            objParam = Nothing
        End Try
    End Function

    Public Function FetchICD9forUpdate(ByVal ExamID As Long, ByVal VisitID As Long) As DataTable

        Dim dt As New DataTable
        Dim sqladpt As New SqlDataAdapter
        Dim objParam As SqlParameter
        Dim Cmd As SqlCommand = Nothing
        Try
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_scanDiagnosis", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = Cmd



            objParam = Cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ExamID

            objParam = Cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitID

            sqladpt.Fill(dt)
            Conn.Close()
            Return dt
            ''''' -- dt(0)= ICD9Code
            ''''' -- dt(1)= Description
            ''''' -- dt(2)= ICD9ID
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Select, "ClsDiagnosisDBLayer -- FetchICD9forUpdate -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Select, "ClsDiagnosisDBLayer -- FetchICD9forUpdate -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If

            objParam = Nothing
        End Try
    End Function

    Public Function GetCPTDrivenDiagnosis(ByVal nVisitID As Int64, ByVal nPatientID As Int64) As ArrayList
        Dim arrTreatment As ArrayList = Nothing

        Try
            Dim con As New SqlClient.SqlConnection(GetConnectionString)
            Dim query As String
            query = " SELECT ISNULL(sCPTcode,'') AS sCPTcode, ISNULL(sCPTDescription,'') AS sCPTDescription, " _
            & " ISNULL(sICD9Code,'') AS sICD9Code, ISNULL(sICD9Description,'') AS sICD9Description, " _
            & " ISNULL(sModCode,'') AS sModCode, ISNULL(sModDescription,'') AS sModDescription, " _
            & " ISNULL(nUnit,0) AS nUnit, ISNULL(nLineNo,0) AS nLineNo, ISNULL(sSnomedCode,'') AS sSnomedCode, ISNULL(sSnomedDesc,'') AS sSnomedDesc,ISNULL(nICDRevision,9) AS nICDRevision,nExamId " _
            & " FROM ExamICD9CPT WHERE nPatientID = " & nPatientID & " AND nVisitID = " & nVisitID & " ORDER BY nLineNo "
            Dim cmd As New SqlCommand(query, con)
            Dim adp As New SqlDataAdapter(cmd)
            Dim dtTreatment As New DataTable
            adp.Fill(dtTreatment)
            If dtTreatment IsNot Nothing Then
                If dtTreatment.Rows.Count > 0 Then
                    Dim oList As gloEMRGeneralLibrary.gloGeneral.myList
                    arrTreatment = New ArrayList

                    For iRow As Integer = 0 To dtTreatment.Rows.Count - 1
                        oList = New gloEMRGeneralLibrary.gloGeneral.myList

                        oList.Code = dtTreatment.Rows(iRow)("sICD9Code").ToString
                        oList.Description = dtTreatment.Rows(iRow)("sICD9Description").ToString
                        oList.HistoryCategory = dtTreatment.Rows(iRow)("sCPTcode").ToString
                        oList.HistoryItem = dtTreatment.Rows(iRow)("sCPTDescription").ToString
                        oList.Value = dtTreatment.Rows(iRow)("sModCode").ToString
                        oList.ParameterName = dtTreatment.Rows(iRow)("sModDescription").ToString
                        oList.TemplateResult = dtTreatment.Rows(iRow)("nUnit").ToString
                        oList.ICD9No = CType(dtTreatment.Rows(iRow)("nLineNo"), Integer)
                        oList.SnomedID = Convert.ToString(dtTreatment.Rows(iRow)("sSnomedCode"))
                        oList.SnomedDesc = Convert.ToString(dtTreatment.Rows(iRow)("sSnomedDesc"))
                        oList.nICDRevision = CType(dtTreatment.Rows(iRow)("nICDRevision"), Integer)
                        oList.ID = CType(dtTreatment.Rows(iRow)("nExamId"), Long)
                        arrTreatment.Add(oList)
                    Next

                    query = Nothing


                End If
                dtTreatment.Dispose()
                dtTreatment = Nothing
            End If
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If (IsNothing(adp) = False) Then
                adp.Dispose()
                adp = Nothing
            End If
            If Not IsNothing(con) Then
                con.Close()
                con.Dispose()
                con = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
        Return arrTreatment
    End Function


    Public Function GetCPTDrivenDiagnosis(ByVal nExamID As Int64, ByVal nVisitID As Int64, ByVal nPatientID As Int64) As ArrayList
        Dim arrTreatment As ArrayList = Nothing

        Try
            Dim con As New SqlClient.SqlConnection(GetConnectionString)
            Dim query As String
            query = " SELECT ISNULL(sCPTcode,'') AS sCPTcode, ISNULL(sCPTDescription,'') AS sCPTDescription, " _
            & " ISNULL(sICD9Code,'') AS sICD9Code, ISNULL(sICD9Description,'') AS sICD9Description, " _
            & " ISNULL(sModCode,'') AS sModCode, ISNULL(sModDescription,'') AS sModDescription, " _
            & " ISNULL(nUnit,0) AS nUnit, ISNULL(nLineNo,0) AS nLineNo, ISNULL(sSnomedCode,'') AS sSnomedCode, ISNULL(sSnomedDesc,'') AS sSnomedDesc,ISNULL(nICDRevision,9) AS nICDRevision, " _
            & " isnull(nTimedTherapy,'') AS nTimedTherapy, isnull(nUnTimedTherapy,'') AS nUnTimedTherapy, " _
            & " isnull(sReasonConceptID,'') as sReasonConceptID,isnull(sReasonConceptDesc,'') as sReasonConceptDesc " _
            & " FROM ExamICD9CPT WHERE nPatientID = " & nPatientID & " AND nExamID = " & nExamID & "  AND nVisitID = " & nVisitID & " ORDER BY nLineNo "
            Dim cmd As New SqlCommand(query, con)
            Dim adp As New SqlDataAdapter(cmd)
            Dim dtTreatment As New DataTable
            adp.Fill(dtTreatment)
            If dtTreatment IsNot Nothing Then
                If dtTreatment.Rows.Count > 0 Then
                    arrTreatment = New ArrayList
                    Dim oList As gloEMRGeneralLibrary.gloGeneral.myList
                    For iRow As Integer = 0 To dtTreatment.Rows.Count - 1
                        oList = New gloEMRGeneralLibrary.gloGeneral.myList

                        oList.Code = dtTreatment.Rows(iRow)("sICD9Code").ToString
                        oList.Description = dtTreatment.Rows(iRow)("sICD9Description").ToString
                        oList.HistoryCategory = dtTreatment.Rows(iRow)("sCPTcode").ToString
                        oList.HistoryItem = dtTreatment.Rows(iRow)("sCPTDescription").ToString
                        oList.Value = dtTreatment.Rows(iRow)("sModCode").ToString
                        oList.ParameterName = dtTreatment.Rows(iRow)("sModDescription").ToString
                        oList.TemplateResult = dtTreatment.Rows(iRow)("nUnit").ToString
                        oList.TimedTherapy = dtTreatment.Rows(iRow)("nTimedTherapy").ToString
                        oList.UnTimedTherapy = dtTreatment.Rows(iRow)("nUnTimedTherapy").ToString
                        oList.ICD9No = CType(dtTreatment.Rows(iRow)("nLineNo"), Integer)
                        oList.SnomedID = Convert.ToString(dtTreatment.Rows(iRow)("sSnomedCode"))
                        oList.SnomedDesc = Convert.ToString(dtTreatment.Rows(iRow)("sSnomedDesc"))
                        oList.nICDRevision = CType(dtTreatment.Rows(iRow)("nICDRevision"), Integer)
                        oList.ID = nExamID
                        oList.ReasonConceptID = Convert.ToString(dtTreatment.Rows(iRow)("sReasonConceptID"))
                        oList.ReasonConceptDesc = Convert.ToString(dtTreatment.Rows(iRow)("sReasonConceptDesc"))
                        arrTreatment.Add(oList)
                    Next


                    query = Nothing




                End If

                dtTreatment.Dispose()
                dtTreatment = Nothing
            End If
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If (IsNothing(adp) = False) Then
                adp.Dispose()
                adp = Nothing
            End If
            If Not IsNothing(con) Then
                con.Close()
                con.Dispose()
                con = Nothing
            End If


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try
        Return arrTreatment
    End Function


#Region " FOR EXAM ICD9 CPT"

    Public Function FetchExamICD9(ByVal ExamID As Long) As DataTable

        Dim Cmd As SqlCommand = Nothing
        Try
            Dim dt As New DataTable
            Dim sqladpt As New SqlDataAdapter

            Cmd = New System.Data.SqlClient.SqlCommand("gsp_ScanExamICD9", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = Cmd

            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ExamID

            sqladpt.Fill(dt)
            Conn.Close()
            sqladpt.Dispose()
            sqladpt = Nothing

            objParam = Nothing
            Return dt

        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Select, "ClsDiagnosisDBLayer -- FetchExamICD9 -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Select, "ClsDiagnosisDBLayer -- FetchExamICD9 -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function

    Public Function FetchExamICD9CPT(ByVal ExamID As Long, ByVal ICD9Code As String) As DataTable

        Dim Cmd As SqlCommand = Nothing

        Try
            Dim dt As New DataTable
            Dim sqladpt As New SqlDataAdapter

            Cmd = New System.Data.SqlClient.SqlCommand("gsp_ScanExamICD9CPT", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = Cmd

            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ExamID

            objParam = Cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ICD9Code

            sqladpt.Fill(dt)
            Conn.Close()
            sqladpt.Dispose()
            sqladpt = Nothing
            objParam = Nothing
            Return dt

        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Select, "ClsDiagnosisDBLayer -- FetchExamICD9CPT -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Select, "ClsDiagnosisDBLayer -- FetchExamICD9CPT -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function


    Public Function FetchExamICD9CPTModifier(ByVal ExamID As Long, ByVal ICD9Code As String, ByVal CPTCode As String) As DataTable

        Dim Cmd As SqlCommand = Nothing

        Dim sqladpt As New SqlDataAdapter
        Try
            Dim dt As New DataTable


            Cmd = New System.Data.SqlClient.SqlCommand("gsp_ScanExamICD9CPTModifier", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = Cmd

            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ExamID

            objParam = Cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ICD9Code

            objParam = Cmd.Parameters.Add("@CPTcode", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CPTCode

            sqladpt.Fill(dt)
            Conn.Close()

            objParam = Nothing
            Return dt
            '' dt.COL(0) = ModCode, 
            '' dt.COL(1) = ModDesc, 
            '' dt.COL(2) = Unit
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("ClsDiagnosisDBLayer -- FetchExamICD9CPTModifier -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Select, "ClsDiagnosisDBLayer -- FetchExamICD9CPTModifier -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception
            'UpdateLog("ClsDiagnosisDBLayer -- FetchExamICD9CPTModifier -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Select, "ClsDiagnosisDBLayer -- FetchExamICD9CPTModifier -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
            End If
            sqladpt = Nothing

        End Try
    End Function
    'Shubhangi 20091110
    Public Function IsICD9CPT_Present(ByVal nExamID As Int64, ByVal nPatientID As Int64) As Boolean
        Dim _Query As String = "SELECT COUNT(*) FROM ExamICD9CPT WHERE nPatientID = " & nPatientID & " AND nExamID = " & nExamID & ""

        Dim cmd As New SqlCommand(_Query, Conn)
        Try

            Dim oResult As Object
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            oResult = cmd.ExecuteScalar
            If Convert.ToInt32(oResult) > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        Finally
            Conn.Close()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function
    'End Shubhangi

    Public Function UpdateDiagTreatmentAssociation(ByVal ExamID As Long, ByVal PatientID As Long, ByVal VisitID As Long, ByVal IcdCode As String, ByVal SnomedCode As String, ByVal SnomedDesc As String) As Boolean
        Conn.Open()
        Dim trDiagnosis As SqlTransaction = Nothing
        trDiagnosis = Conn.BeginTransaction
        Dim objparam As SqlParameter
        Dim cmddelete As SqlCommand = Nothing
        Try
            cmddelete = New System.Data.SqlClient.SqlCommand("gsp_UpdateExamICD9CPTModifier", Conn)
            cmddelete.CommandType = CommandType.StoredProcedure
            cmddelete.Transaction = trDiagnosis

            objparam = cmddelete.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = PatientID

            objparam = cmddelete.Parameters.Add("@nExamId", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = ExamID

            objparam = cmddelete.Parameters.Add("@nVisitId", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = VisitID

            objparam = cmddelete.Parameters.Add("@sIcdCOde", SqlDbType.VarChar)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = IcdCode

            objparam = cmddelete.Parameters.Add("@sSnomedCode", SqlDbType.VarChar)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = SnomedCode

            objparam = cmddelete.Parameters.Add("@sSnomedDesc", SqlDbType.VarChar)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = SnomedDesc

            cmddelete.ExecuteNonQuery()


            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "Updated Diagnosis", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            trDiagnosis.Commit()
            Conn.Close()
            Return True
        Catch ex As Exception
            trDiagnosis.Rollback()
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- UpdateDiagTreatmentAssociation -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            trDiagnosis = Nothing
            'Cmd = Nothing
            cmddelete = Nothing
            Conn.Close()
            Return False
        Finally
            If cmddelete IsNot Nothing Then
                cmddelete.Parameters.Clear()
                cmddelete.Dispose()
                cmddelete = Nothing
            End If
            If (IsNothing(trDiagnosis) = False) Then
                trDiagnosis.Dispose()
                trDiagnosis = Nothing
            End If

            objparam = Nothing
        End Try
    End Function

    Public Function SaveExamIcd9Cpt(ByVal dtExmIcdCPT As DataTable) As Boolean

        Dim Con As SqlConnection = Nothing

        ' Dim da As SqlDataAdapter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim _param As SqlParameter = Nothing
        Try

            Con = New SqlConnection(GetConnectionString())
            cmd = New SqlCommand("SaveSmartDx_TVP", Con)
            cmd.CommandType = CommandType.StoredProcedure
            _param = cmd.Parameters.AddWithValue("@tvpSmartDx", dtExmIcdCPT)
            _param.SqlDbType = SqlDbType.Structured
            Con.Open()
            cmd.ExecuteNonQuery()
            Con.Close()
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- FillProblemList -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return False
        Finally
            If IsNothing(Con) = False Then
                Con.Dispose()
                Con = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If IsNothing(_param) = False Then
                _param = Nothing
            End If
        End Try
    End Function

    Public Function SaveDiagTreatmentAssociation(ByVal ExamID As Long, ByVal PatientID As Long, ByVal VisitID As Long, ByVal arrlist As ArrayList, ByVal frm As Object, Optional ByVal isTreatment As Boolean = False, Optional ByVal _isFromDxCPT As Boolean = False) As Boolean

        Conn.Open()
        Dim trDiagnosis As SqlTransaction = Nothing
        trDiagnosis = Conn.BeginTransaction
        Dim cmddelete As SqlCommand = Nothing
        Dim Cmd As SqlCommand = Nothing
        ' Dim strQRY As String
        ' Dim _result As Object
        ' Dim ID As Int64
        Dim cmdProblem As SqlCommand = Nothing
        Dim dtICD9 As New DataTable
        Dim dvICD9 As DataView = Nothing
        Dim hashIcd As New Hashtable
        Try



            cmdProblem = New SqlCommand("SELECT ISNULL(sSnomedCode,'') AS sSnomedCode ,ISNULL(sSnomedDesc,'') AS sSnomedDesc,isnull(sICD9Code,'') as sICD9Code from Examicd9cpt where nExamid=" & ExamID & " and nVisitID=" & VisitID & "", Conn)
            Dim adp As New SqlDataAdapter(cmdProblem)
            cmdProblem.Transaction = trDiagnosis
            adp.Fill(dtICD9)
            adp.Dispose()
            adp = Nothing

            cmdProblem.Parameters.Clear()
            cmdProblem.Dispose()
            cmdProblem = Nothing


            Dim i As Integer
            Dim objparam As SqlParameter

            cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteExamICD9CPTModifier", Conn)
            cmddelete.CommandType = CommandType.StoredProcedure
            cmddelete.Transaction = trDiagnosis

            objparam = cmddelete.Parameters.Add("@nExamId", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = ExamID

            objparam = cmddelete.Parameters.Add("@nVisitId", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = VisitID

            cmddelete.ExecuteNonQuery()
            cmddelete.Parameters.Clear()
            cmddelete.Dispose()
            cmddelete = Nothing
            Dim blnOneSnoMed As Boolean
            Dim strSnomedCode As String = ""
            Dim strSnomedDesc As String = ""


            Dim PreviosICD As String = ""
            ''
            If Not IsNothing(dtICD9) Then
                dvICD9 = dtICD9.Copy().DefaultView
            End If

            For i = 0 To arrlist.Count - 1


                Dim lst As Object
                If isTreatment = False Then
                    lst = CType(arrlist.Item(i), myList)
                Else
                    lst = CType(arrlist.Item(i), gloEMRGeneralLibrary.gloGeneral.myList)
                End If

                'Insert data in ExamICD9CPT

                Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertExamICD9CPTModifier", Conn)

                Cmd.CommandType = CommandType.StoredProcedure

                Cmd.Transaction = trDiagnosis

                objparam = Cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = PatientID

                objparam = Cmd.Parameters.Add("@nExamId", SqlDbType.BigInt)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = ExamID

                objparam = Cmd.Parameters.Add("@nVisitId", SqlDbType.BigInt)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = VisitID

                objparam = Cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = lst.Code & ""

                objparam = Cmd.Parameters.Add("@ICD9Desc", SqlDbType.VarChar)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = lst.Description & ""

                objparam = Cmd.Parameters.Add("@CPTcode", SqlDbType.VarChar)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = lst.HistoryCategory & ""

                objparam = Cmd.Parameters.Add("@CPTDesc", SqlDbType.VarChar)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = lst.HistoryItem & ""

                objparam = Cmd.Parameters.Add("@ModCode", SqlDbType.VarChar)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = lst.Value & ""

                objparam = Cmd.Parameters.Add("@ModDesc", SqlDbType.VarChar)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = lst.ParameterName & ""

                objparam = Cmd.Parameters.Add("@Unit", SqlDbType.Decimal)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = CType(lst.TemplateResult, Decimal)

                objparam = Cmd.Parameters.Add("@LineNo", SqlDbType.Int)
                objparam.Direction = ParameterDirection.Input
                If isTreatment = False Then
                    objparam.Value = CType(lst.ICD9Count, Int16)
                Else
                    objparam.Value = CType(lst.ICD9No, Int16)
                End If


                If _isFromDxCPT Then
                    If isTreatment Then
                        strSnomedCode = DirectCast(lst, gloEMRGeneralLibrary.gloGeneral.myList).SnomedID & ""
                        strSnomedDesc = DirectCast(lst, gloEMRGeneralLibrary.gloGeneral.myList).SnomedDesc & ""
                    Else
                        strSnomedCode = DirectCast(lst, gloEMR.myList).SnowMadeID & ""
                        strSnomedDesc = DirectCast(lst, gloEMR.myList).SnoDescription & ""
                    End If
                Else


                    Dim blnpresent As Boolean = False
                    If PreviosICD <> lst.Code Then
                        strSnomedCode = ""
                        strSnomedDesc = ""
                    End If

                    If Not IsNothing(dvICD9) Then
                        dvICD9.RowFilter = "sICD9Code = '" & lst.Code & "'"
                        If dvICD9.Count > 0 Then
                            strSnomedCode = dvICD9.ToTable.Rows(0)(0)
                            strSnomedDesc = dvICD9.ToTable.Rows(0)(1)
                            blnpresent = True
                        End If
                    End If

                    Dim htSno As New Hashtable()

                    If (hashIcd.Contains(lst.code)) Then
                        Dim value As String = hashIcd(lst.code)
                        Dim splstrvalue As String() = value.Split("~")

                        If (splstrvalue.Length > 1) Then
                            htSno.Add(splstrvalue(0), splstrvalue(1))
                        Else
                            htSno.Add(splstrvalue(0), "")
                        End If
                        '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
                    Else

                        If isTreatment Then
                            htSno = GetDefaultSnomed(lst.code, lst.Description, CType(DirectCast(lst, gloEMRGeneralLibrary.gloGeneral.myList).nICDRevision, Int16), blnOneSnoMed, frm, PreviosICD, blnpresent)
                        Else
                            htSno = GetDefaultSnomed(lst.code, lst.Description, CType(DirectCast(lst, gloEMR.myList).nICDRevision, Int16), blnOneSnoMed, frm, PreviosICD, blnpresent)
                        End If
                    End If

                    If Not IsNothing(htSno) Then

                        Dim key As ICollection = htSno.Keys
                        Dim k As String

                        For Each k In key

                            strSnomedCode = k
                            strSnomedDesc = htSno(k)
                        Next k

                        If (hashIcd.Contains(lst.code) = False) Then
                            hashIcd.Add(lst.code, strSnomedCode & "~" & strSnomedDesc)
                        End If


                    End If
                    htSno = Nothing

                    PreviosICD = lst.Code
                End If
                If isTreatment = False Then




                    objparam = Cmd.Parameters.Add("@SnomedCT", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = strSnomedCode & ""

                    objparam = Cmd.Parameters.Add("@SnomedDesc", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = strSnomedDesc & ""

                    objparam = Cmd.Parameters.Add("@nICDRevision", SqlDbType.SmallInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = CType(DirectCast(lst, gloEMR.myList).nICDRevision, Int16)

                    ''Added by Mayuri:20140721-To save icdrevision and icd snomed mapping type
                    objparam = Cmd.Parameters.Add("@bIsSnoMedOneToOneMapping", SqlDbType.Bit)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = blnOneSnoMed

                Else
                    objparam = Cmd.Parameters.Add("@SnomedCT", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = strSnomedCode & ""

                    objparam = Cmd.Parameters.Add("@SnomedDesc", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = strSnomedDesc & ""

                    objparam = Cmd.Parameters.Add("@nICDRevision", SqlDbType.SmallInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = CType(DirectCast(lst, gloEMRGeneralLibrary.gloGeneral.myList).nICDRevision, Int16)

                    '14-Jul-14 Aniket: Problem List SnoMed Project CPT Driven
                    objparam = Cmd.Parameters.Add("@bIsSnoMedOneToOneMapping", SqlDbType.Bit)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = blnOneSnoMed
                End If

                If lst.TimedTherapy IsNot Nothing Then
                    objparam = Cmd.Parameters.Add("@TimedTherapy", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = lst.TimedTherapy
                End If

                If lst.UnTimedTherapy IsNot Nothing Then
                    objparam = Cmd.Parameters.Add("@UnTimedTherapy", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = lst.UnTimedTherapy
                End If

                If lst.ReasonConceptID IsNot Nothing Then
                    objparam = Cmd.Parameters.Add("@ReasonConceptID", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = lst.ReasonConceptID
                End If

                If lst.ReasonConceptDesc IsNot Nothing Then
                    objparam = Cmd.Parameters.Add("@ReasonConceptDesc", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = lst.ReasonConceptDesc
                End If
                'Insert data in ExamICD9CPT

                Cmd.ExecuteNonQuery()

                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
                objparam = Nothing
            Next

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "Treatment are associated with Diagnosis", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

            trDiagnosis.Commit()
            Conn.Close()

            Return True
        Catch ex As SqlException
            trDiagnosis.Rollback()
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- SaveDiagTreatmentAssociation -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            trDiagnosis = Nothing
            Cmd = Nothing

            If Not IsNothing(cmddelete) Then
                cmddelete.Dispose()
                cmddelete = Nothing
            End If

            Conn.Close()
            Return False

        Catch ex As Exception
            trDiagnosis.Rollback()

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- SaveDiagTreatmentAssociation -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            trDiagnosis = Nothing
            Cmd = Nothing

            If Not IsNothing(cmddelete) Then
                cmddelete.Dispose()
                cmddelete = Nothing
            End If

            Conn.Close()
            Return False

        Finally
            If Not IsNothing(Conn) Then
                If Conn.State = ConnectionState.Open Then
                    Conn.Close()
                End If
            End If
            If Not IsNothing(trDiagnosis) Then
                trDiagnosis.Dispose()
                trDiagnosis = Nothing
            End If
            ''free trDiagnosis,Cmd,cmddelete gaainst problem 00000591
            Cmd = Nothing

            If Not IsNothing(cmddelete) Then
                cmddelete.Parameters.Clear()
                cmddelete.Dispose()
                cmddelete = Nothing
            End If
            If Not IsNothing(dtICD9) Then
                dtICD9.Dispose()
                dtICD9 = Nothing
            End If
            If Not IsNothing(dvICD9) Then
                dvICD9.Dispose()
                dvICD9 = Nothing
            End If
            If Not IsNothing(hashIcd) Then
                hashIcd.Clear()
                hashIcd = Nothing
            End If
        End Try

    End Function

    Public Function GetDefaultSnomed(ByVal strICDCode As String, ByVal strICDDesc As String, ByVal IcdRevision As Integer, ByRef GetIsSnoMedOneToOne As Boolean, ByRef objfrm As Object, Optional ByVal PreviousICD As String = "", Optional ByVal blnpresent As Boolean = False) As Hashtable

        Dim dtProblemSnomed As DataTable
        Dim objclsProblist As New clsPatientProblemList
        Dim objclsSnomed As New gloSnoMed.clsSnomedIcdMap
        Dim dtTempSetting As DataTable = Nothing
        Dim icdRev As Int16
        Dim frm As gloSnoMed.FrmSelectProblem
        Dim hshSnomed As New Hashtable
        Dim IsSnomedCTMandatory As Boolean = False

        Try

            icdRev = IcdRevision
            IsSnomedCTMandatory = objclsProblist.IsSnomedMandatory()

            'If dtTempSetting IsNot Nothing Then
            '    If dtTempSetting.Rows.Count > 0 Then
            '        IsSnomedCTMandatory = Convert.ToBoolean(dtTempSetting.Rows(0)("IsSnomedCTMandatory"))
            '    End If
            '    dtTempSetting.Dispose()
            '    dtTempSetting = Nothing

            'End If




            dtProblemSnomed = objclsSnomed.Get_DefaultSnomedForICD(strICDCode, strICDDesc, IcdRevision, GetConnectionString())

            If dtProblemSnomed IsNot Nothing Then

                'If multiple SnoMed Codes are found for the selected ICD code
                If dtProblemSnomed.Rows.Count > 1 Then
                    ''Open Snomed Selecter form 
                    If IsSnomedCTMandatory Then
                        If PreviousICD <> strICDCode Then
                            ' If ID = 0 Then
                            If blnpresent Then  ''If snomed already present against icd then do not open selector
                                GetIsSnoMedOneToOne = False
                            Else


                                gstrSMDBConnstr = GetHybridConnectionString(gstrSMDBServerName, gstrSMDBDatabaseName, gblnSMDBAuthen, gstrSMDBUserID, gstrSMDBPassWord)
                                frm = New gloSnoMed.FrmSelectProblem("Select Problem", gstrSMDBConnstr, GetConnectionString())

                                frm.blnIsProblem = True

                                If icdRev = 9 Then
                                    frm.strCodeSystem = "ICD9"
                                ElseIf icdRev = 10 Then
                                    frm.strCodeSystem = "ICD10"
                                End If

                                frm.txtSMSearch.Text = Convert.ToString(strICDCode)
                                frm.ShowDialog(IIf(IsNothing(frm.Parent), objfrm, frm.Parent))

                                If frm._DialogResult = True Then
                                    hshSnomed.Add(Convert.ToString(frm.strSelectedConceptID), Convert.ToString(frm.strSelectedDescription))
                                    '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
                                    GetIsSnoMedOneToOne = False
                                End If

                                frm.Dispose()
                                frm = Nothing
                            End If


                        End If
                    End If
                Else
                    If blnpresent Then
                        GetIsSnoMedOneToOne = True
                    Else
                        If dtProblemSnomed.Rows.Count <> 0 Then
                            hshSnomed.Add(Convert.ToString(dtProblemSnomed.Rows(0)("CONCEPTID")), Convert.ToString(dtProblemSnomed.Rows(0)("TermDescription")))
                            '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
                            GetIsSnoMedOneToOne = True
                        End If
                    End If
                    'If no SnoMed Codes are found for the selected ICD code


                End If

            End If
           

            If IsNothing(objclsSnomed) = False Then
                objclsSnomed = Nothing
            End If

            If IsNothing(objclsProblist) = False Then
                objclsProblist.Dispose()
                objclsProblist = Nothing
            End If

            If IsNothing(dtProblemSnomed) = False Then
                dtProblemSnomed.Dispose()
                dtProblemSnomed = Nothing
            End If

            If IsNothing(dtTempSetting) = False Then
                dtTempSetting.Dispose()
                dtTempSetting = Nothing
            End If



            Return hshSnomed

        Catch ex As Exception

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return hshSnomed

        Finally


        End Try


    End Function
    Public Function SaveSmartDiagTreatmentAssociation(ByVal ExamID As Long, ByVal PatientID As Long, ByVal VisitID As Long, ByVal arrlist As ArrayList) As Boolean
        Conn.Open()
        Dim trDiagnosis As SqlTransaction = Nothing
        trDiagnosis = Conn.BeginTransaction

        Dim cmdselect As SqlCommand = Nothing
        Dim objparam As SqlParameter = Nothing
        Dim Cmd As SqlCommand = Nothing

        Try
            Dim i As Integer



            Dim dt As DataTable
            Dim da As SqlDataAdapter

            Dim isPresent As Boolean = False
            '          Dim oDB As New gloStream.gloDataBase.gloDataBase
            For i = 0 To arrlist.Count - 1
                Dim lst As myList
                lst = CType(arrlist.Item(i), myList)
                'SP for selecting ICD9CPT from ExamICD9CPT tabel on current ExamID and VisitID
                cmdselect = New System.Data.SqlClient.SqlCommand("gsp_selectExamICD9CPT", Conn)
                cmdselect.CommandType = CommandType.StoredProcedure
                cmdselect.Transaction = trDiagnosis

                objparam = cmdselect.Parameters.Add("@nExamID", SqlDbType.BigInt)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = ExamID

                objparam = cmdselect.Parameters.Add("@nVisitID", SqlDbType.BigInt)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = VisitID
                da = New SqlDataAdapter
                da.SelectCommand = cmdselect
                dt = New DataTable
                da.Fill(dt)
                'oDB.Connect(GetConnectionString)
                'dt = oDB.ReadQueryDataTable(strSelectQry)
                'oDB.Disconnect()
                cmdselect.Parameters.Clear()
                cmdselect.Dispose()
                cmdselect = Nothing
                da.Dispose()
                da = Nothing
                If IsNothing(dt) = False Then
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If dt.Rows(j)("nExamId") = ExamID AndAlso dt.Rows(j)("nVisitId") = VisitID AndAlso dt.Rows(j)("sICD9Code") = lst.Code AndAlso dt.Rows(j)("sICD9Description") = lst.Description AndAlso dt.Rows(j)("sCPTcode") = lst.HistoryCategory AndAlso dt.Rows(j)("sCPTDescription") = lst.HistoryItem Then
                            isPresent = True
                            Exit For
                        Else
                            isPresent = False
                        End If
                    Next
                    dt.Dispose()
                    dt = Nothing
                End If


                'Insert data in ExamICD9CPT
                If isPresent = False Then

                    Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertExamICD9CPTModifier", Conn)

                    Cmd.CommandType = CommandType.StoredProcedure

                    Cmd.Transaction = trDiagnosis

                    objparam = Cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = PatientID

                    objparam = Cmd.Parameters.Add("@nExamId", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = ExamID

                    objparam = Cmd.Parameters.Add("@nVisitId", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = VisitID

                    objparam = Cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = lst.Code & ""

                    objparam = Cmd.Parameters.Add("@ICD9Desc", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = lst.Description & ""

                    objparam = Cmd.Parameters.Add("@CPTcode", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = lst.HistoryCategory & ""

                    objparam = Cmd.Parameters.Add("@CPTDesc", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = lst.HistoryItem & ""

                    objparam = Cmd.Parameters.Add("@ModCode", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = lst.Value & ""

                    objparam = Cmd.Parameters.Add("@ModDesc", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = lst.ParameterName & ""

                    objparam = Cmd.Parameters.Add("@Unit", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = CType(lst.TemplateResult, Int64)

                    'Insert data in ExamICD9CPT

                    Cmd.ExecuteNonQuery()

                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing
                End If

            Next


            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "Treatment are associated with Diagnosis", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)


            trDiagnosis.Commit()
            Conn.Close()


            Return True
        Catch ex As SqlException
            trDiagnosis.Rollback()
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- SaveSmartDiagTreatmentAssociation -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            trDiagnosis = Nothing
            Cmd = Nothing

            Conn.Close()
            Return False
        Catch ex As Exception
            trDiagnosis.Rollback()

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- SaveSmartDiagTreatmentAssociation -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            trDiagnosis = Nothing
            Cmd = Nothing

            Conn.Close()
            Return False
        Finally
            ''free variables for problem 00000591
            If Not IsNothing(trDiagnosis) Then
                trDiagnosis.Dispose()
                trDiagnosis = Nothing
            End If

            Cmd = Nothing
            'cmddelete = Nothing
            If Not IsNothing(Conn) Then
                Conn.Close()
            End If

            If cmdselect IsNot Nothing Then
                cmdselect.Parameters.Clear()
                cmdselect.Dispose()
                cmdselect = Nothing
            End If

            objparam = Nothing
        End Try
    End Function

    Public Function FillProblemList(ByVal PatientID As Int64, ByVal VisitID As Int64, ByVal DOS As DateTime, ByVal ICD9Code As String, ByVal ICD9Description As String, ByVal SnomedCode As String, ByVal SnomedDesc As String, ByVal nICDRevision As Int16, ByVal blnIsOneToOne As Boolean, Optional ByVal examid As Long = 0)

        Dim trDiagnosis As SqlTransaction = Nothing
        Dim Cmd As SqlCommand = Nothing
        Dim strSnoMedDescription As String = ""
        Dim strSnoMedCode As String = ""

        'If Trim(SnomedDesc) <> "" Then
        '    If SnomedDesc.IndexOf(SnomedCode) <> -1 Then
        '        strSnoMedDescription = Trim(SnomedDesc.Substring(SnomedDesc.IndexOf(SnomedCode) + 1))
        '    Else
        '        strSnoMedDescription = Trim(SnomedDesc)
        '    End If
        'End If

        '23-Jul-14 Aniket: Resolving Bug #71262: 
        If Trim(SnomedDesc) <> "" Then
            strSnoMedDescription = SnomedDesc
            strSnoMedCode = SnomedCode

            If strSnoMedDescription.IndexOf(strSnoMedCode) <> -1 Then
                strSnoMedDescription = Trim(strSnoMedDescription.Replace(strSnoMedCode & " - ", ""))
            Else
                strSnoMedDescription = Trim(strSnoMedDescription)
            End If
        Else

            strSnoMedDescription = ""
        End If


        Try

            ''nProblemID, nVisitID, nPatientID, dtDOS, sICD9Code, sICD9Desc, sCheifComplaint, nProblemStatus, sPrescription, nUserID, sUserName, sMachineName
            Dim strQRY As String
            Dim _result As Object
            Dim sqlParam As SqlParameter
            Dim ID As Int64 = 0
            ' ICD9Description = ReplaceSpecialCharacters(ICD9Description) 'chetan commented on nov 22 2010



            Conn.Open()
            trDiagnosis = Conn.BeginTransaction

            strQRY = "SELECT nProblemID FROM ProblemList WHERE nPatientID = " & PatientID & " AND rtrim(ltrim(sICD9Desc)) = '" + UCase(ICD9Description.Replace("'", "''")).ToString.Trim + "'" & " AND RTRIM(LTRIM(sICD9Code))='" & ICD9Code & "' AND nVisitId=" & VisitID
            Cmd = New System.Data.SqlClient.SqlCommand(strQRY, Conn)
            Cmd.Transaction = trDiagnosis
            _result = Cmd.ExecuteScalar()
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            ID = Convert.ToInt64(_result)
            If ID = 0 Then
                strQRY = "SELECT nProblemID FROM ProblemList WHERE nPatientID = " & PatientID & " AND rtrim(ltrim(sICD9Desc)) = '" + UCase(ICD9Description.Replace("'", "''")).ToString.Trim + "'" & " AND RTRIM(LTRIM(sICD9Code))='" & ICD9Code & "'"
                Cmd = New System.Data.SqlClient.SqlCommand(strQRY, Conn)
                Cmd.Transaction = trDiagnosis
                _result = Cmd.ExecuteScalar()
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
                ID = Convert.ToInt64(_result)

                If ID <> 0 Then
                    strQRY = "SELECT nProblemID FROM ProblemList WHERE nPatientID = " & PatientID & " AND rtrim(ltrim(sICD9Desc)) = '" + UCase(ICD9Description.Replace("'", "''")).ToString.Trim + "'" & " AND RTRIM(LTRIM(sICD9Code))='" & ICD9Code & "' AND nProblemStatus=2 "
                    Cmd = New System.Data.SqlClient.SqlCommand(strQRY, Conn)
                    Cmd.Transaction = trDiagnosis
                    _result = Cmd.ExecuteScalar()
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing
                    ID = Convert.ToInt64(_result)
                End If
            End If




            Cmd = New SqlCommand("gsp_InUpProblemListFromExam", Conn)
            Cmd.CommandType = CommandType.StoredProcedure

            Cmd.Transaction = trDiagnosis


            sqlParam = Cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = Cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = VisitID


            sqlParam = Cmd.Parameters.Add("@DOS", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = DOS

            sqlParam = Cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar, 50)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ICD9Code

            sqlParam = Cmd.Parameters.Add("@ICD9Desc", SqlDbType.VarChar, 255)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ICD9Description

            sqlParam = Cmd.Parameters.Add("@CheifComplaint", SqlDbType.VarChar, 255)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = If(Trim(strSnoMedDescription) = "", ICD9Description, strSnoMedDescription) 'ICD9Description

            sqlParam = Cmd.Parameters.Add("@Prescription", SqlDbType.VarChar, 255)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ""

            sqlParam = Cmd.Parameters.Add("@ProblemStatus", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Convert.ToInt16(frmProblemList.Status.Active.GetHashCode())

            ''By Mahesh ' 20070316

            sqlParam = Cmd.Parameters.Add("@UserID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = gnLoginID




            sqlParam = Cmd.Parameters.AddWithValue("@MachineID", GetPrefixTransactionID)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = Cmd.Parameters.Add("@ExamID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = examid


            sqlParam = Cmd.Parameters.AddWithValue("@ProblemID", ID)
            sqlParam.Direction = ParameterDirection.InputOutput

            sqlParam = Cmd.Parameters.Add("@bIsEncounterDiagnosis", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = True


            sqlParam = Cmd.Parameters.Add("@sConceptID", SqlDbType.VarChar, 50)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = SnomedCode

            sqlParam = Cmd.Parameters.Add("@sTransactionID1", SqlDbType.VarChar, 250)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = SnomedDesc
            ''Added by Mayuri:20140721-To save icdrevision and icd snomed mapping type
            sqlParam = Cmd.Parameters.Add("@nICDRevision", SqlDbType.SmallInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nICDRevision

            sqlParam = Cmd.Parameters.Add("@IsOneToOne", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            If blnIsOneToOne = True Then
                sqlParam.Value = 1
            Else
                sqlParam.Value = 0
            End If


            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            Cmd.ExecuteNonQuery()
            ID = Cmd.Parameters("@ProblemID").Value
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing

            sqlParam = Nothing
            trDiagnosis.Commit()




        Catch ex As SqlException
            trDiagnosis.Rollback()
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("ClsDiagnosisDBLayer -- FillProblemList -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- FillProblemList -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
        Catch ex As Exception
            trDiagnosis.Rollback()
            'UpdateLog("ClsDiagnosisDBLayer -- FillProblemList -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- FillProblemList -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Not IsNothing(trDiagnosis) Then
                trDiagnosis.Dispose()
                trDiagnosis = Nothing
            End If

        End Try
        Return Nothing

    End Function




    ''Added on 20111014
    Public Function SaveDxCPTProblem(ByVal PatientID As Int64, ByVal VisitID As Int64, ByVal DOS As DateTime, ByVal _Arrlist As ArrayList, Optional ByVal examid As Long = 0)

        Dim ExamDelteParam As SqlParameter = Nothing
        Dim trDiagnosis As SqlTransaction = Nothing
        Dim cmddelete As SqlCommand = Nothing
        Dim _colProblemItem As Integer = 0
        Dim dtSource As DataTable = Nothing
        Dim dtProblem As DataTable = Nothing

        Dim _strCode As String = ""
        Dim _strDescription As String = ""

        Dim strQRY As String
        Dim _result As Object

        Dim ID As Int64 = 0
        Dim Cmd As SqlCommand = Nothing
        Dim strSnoMedDescription As String = ""
        Dim strSnoMedCode As String = ""

        Try
            Conn.Open()

            trDiagnosis = Conn.BeginTransaction

            cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteExamProblemList", Conn)
            cmddelete.CommandType = CommandType.StoredProcedure
            cmddelete.Transaction = trDiagnosis

            ExamDelteParam = cmddelete.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            ExamDelteParam.Direction = ParameterDirection.Input
            ExamDelteParam.Value = PatientID

            ExamDelteParam = cmddelete.Parameters.Add("@nExamId", SqlDbType.BigInt)
            ExamDelteParam.Direction = ParameterDirection.Input
            ExamDelteParam.Value = examid

            ExamDelteParam = cmddelete.Parameters.Add("@nVisitId", SqlDbType.BigInt)
            ExamDelteParam.Direction = ParameterDirection.Input
            ExamDelteParam.Value = VisitID

            cmddelete.ExecuteNonQuery()
            cmddelete.Parameters.Clear()
            cmddelete.Dispose()
            cmddelete = Nothing
            dtSource = GetProblemListTableStructure()
            If IsNothing(_Arrlist) = False Then
                If _Arrlist.Count > 0 Then
                    For _colProblemItem = 0 To _Arrlist.Count - 1

                        _strCode = CType(_Arrlist.Item(_colProblemItem), mytable).Code.ToString
                        _strDescription = CType(_Arrlist.Item(_colProblemItem), mytable).Description.ToString

                        strQRY = "SELECT nProblemID FROM ProblemList WHERE nPatientID = " & PatientID & " AND rtrim(ltrim(sICD9Desc)) = '" + UCase(_strDescription.Replace("'", "''")).ToString.Trim + "'" & " AND RTRIM(LTRIM(sICD9Code))='" & _strCode.Replace("'", "''") & "' AND nVisitId=" & VisitID
                        Cmd = New System.Data.SqlClient.SqlCommand(strQRY, Conn)
                        Cmd.Transaction = trDiagnosis
                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        _result = Cmd.ExecuteScalar()
                        Cmd.Parameters.Clear()
                        Cmd.Dispose()
                        Cmd = Nothing
                        ID = Convert.ToInt64(_result)
                        If ID = 0 Then
                            strQRY = "SELECT nProblemID FROM ProblemList WHERE nPatientID = " & PatientID & " AND rtrim(ltrim(sICD9Desc)) = '" + UCase(_strDescription.Replace("'", "''")).ToString.Trim + "'" & " AND RTRIM(LTRIM(sICD9Code))='" & _strCode.Replace("'", "''") & "'"
                            Cmd = New System.Data.SqlClient.SqlCommand(strQRY, Conn)
                            Cmd.Transaction = trDiagnosis
                            If Conn.State = ConnectionState.Closed Then
                                Conn.Open()
                            End If
                            _result = Cmd.ExecuteScalar()
                            Cmd.Parameters.Clear()
                            Cmd.Dispose()
                            Cmd = Nothing
                            ID = Convert.ToInt64(_result)


                            If ID <> 0 Then
                                strQRY = "SELECT nProblemID FROM ProblemList WHERE nPatientID = " & PatientID & " AND rtrim(ltrim(sICD9Desc)) = '" + UCase(_strDescription.Replace("'", "''")).ToString.Trim + "'" & " AND RTRIM(LTRIM(sICD9Code))='" & _strCode.Replace("'", "''") & "' AND nProblemStatus=2 "
                                Cmd = New System.Data.SqlClient.SqlCommand(strQRY, Conn)
                                Cmd.Transaction = trDiagnosis
                                If Conn.State = ConnectionState.Closed Then
                                    Conn.Open()
                                End If
                                _result = Cmd.ExecuteScalar()
                                Cmd.Parameters.Clear()
                                Cmd.Dispose()
                                Cmd = Nothing
                                ID = Convert.ToInt64(_result)

                            End If
                        End If

                        '23-Jul-14 Aniket: Resolving Bug #71262:
                        If Trim(Convert.ToString(CType(_Arrlist.Item(_colProblemItem), mytable).snomeddescription)) <> "" Then
                            strSnoMedDescription = Trim(Convert.ToString(CType(_Arrlist.Item(_colProblemItem), mytable).snomeddescription))
                            strSnoMedCode = Trim(Convert.ToString(CType(_Arrlist.Item(_colProblemItem), mytable).SnoCode))

                            If strSnoMedDescription.IndexOf(strSnoMedCode) <> -1 Then
                                strSnoMedDescription = Trim(strSnoMedDescription.Replace(strSnoMedCode & " - ", ""))
                            Else
                                strSnoMedDescription = Trim(strSnoMedDescription)
                            End If
                        Else

                            strSnoMedDescription = ""
                        End If


                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        'Cmd.ExecuteNonQuery()
                        Dim drow As DataRow

                        drow = dtSource.NewRow
                        drow("nPatientID") = PatientID
                        drow("nVisitID") = VisitID
                        drow("dtDOS") = DOS


                        drow("nUserID") = gnLoginID
                        drow("sExamID") = examid
                        drow("nProblemID") = ID
                        drow("sICD9Code") = CType(_Arrlist.Item(_colProblemItem), mytable).Code.ToString
                        drow("sICD9Desc") = CType(_Arrlist.Item(_colProblemItem), mytable).Description.ToString
                        drow("nProblemStatus") = Convert.ToInt16(frmProblemList.Status.Active.GetHashCode())
                        drow("sCheifComplaint") = If(strSnoMedDescription = "", CType(_Arrlist.Item(_colProblemItem), mytable).Description.ToString, strSnoMedDescription)
                        drow("sPrescription") = ""
                        drow("sConceptID") = Convert.ToString(CType(_Arrlist.Item(_colProblemItem), mytable).SnoCode)
                        drow("sTransactionID1") = Convert.ToString(CType(_Arrlist.Item(_colProblemItem), mytable).snomeddescription)
                        drow("nICDRevision") = Convert.ToInt16(CType(_Arrlist.Item(_colProblemItem), mytable).nICDRevision)

                        '14-Jul-14 Aniket: Problem List SnoMed Project ICD Driven
                        drow("bIsSnoMedOneToOneMapping") = CType(_Arrlist.Item(_colProblemItem), mytable).blnIsSnoMedOneToOneMapping
                        dtSource.Rows.Add(drow)

                        drow = Nothing
                    Next
                    dtProblem = dtSource
                End If
            End If
            trDiagnosis.Commit()
            Dim _Result1 As Boolean = False
            '06232012
            'bug no 29059 if conditions added for 'dtProblem'
            If Not IsNothing(dtProblem) Then
                If dtProblem.Rows.Count > 0 Then
                    _Result1 = SaveProblemListTable(dtProblem, PatientID)
                End If
            End If
            If IsNothing(dtProblem) = False Then
                dtProblem.Dispose()
                dtProblem = Nothing
            End If
            If IsNothing(dtSource) = False Then
                dtSource.Dispose()
                dtSource = Nothing
            End If
        Catch ex As SqlException
            trDiagnosis.Rollback()
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("ClsDiagnosisDBLayer -- FillProblemList -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- FillProblemList -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
        Catch ex As Exception
            trDiagnosis.Rollback()
            'UpdateLog("ClsDiagnosisDBLayer -- FillProblemList -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- FillProblemList -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If IsNothing(ExamDelteParam) = False Then
                ExamDelteParam = Nothing
            End If
            If IsNothing(cmddelete) = False Then
                cmddelete.Parameters.Clear()
                cmddelete.Dispose()
                cmddelete = Nothing
            End If
            If Not IsNothing(trDiagnosis) Then
                trDiagnosis.Dispose()
                trDiagnosis = Nothing
            End If
        End Try

        Return Nothing
    End Function
    ''End Code Added on 20111014
    Public Function SaveProblemListTable(ByVal dtProblem As DataTable, ByVal PatientID As Int64) As Boolean
        Dim Con As SqlConnection = Nothing

        ' Dim da As SqlDataAdapter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim _param As SqlParameter = Nothing
        Try

            Con = New SqlConnection(GetConnectionString())
            cmd = New SqlCommand("gsp_InUpProblem_TVP", Con)
            cmd.CommandType = CommandType.StoredProcedure
            _param = cmd.Parameters.AddWithValue("@SaveProblem", dtProblem)
            _param.SqlDbType = SqlDbType.Structured
            Con.Open()
            cmd.ExecuteNonQuery()
            Con.Close()
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- FillProblemList -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If IsNothing(Con) = False Then
                Con.Dispose()
                Con = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            'If IsNothing(da) = False Then
            '    da.Dispose()
            '    da = Nothing
            'End If
            If IsNothing(_param) = False Then
                _param = Nothing
            End If
        End Try





    End Function
    Public Function GetICD9CPT(ByVal nExamID As Int64, ByVal nVisitID As Int64) As Boolean
        Conn.Open()
        Dim cmd As SqlCommand = Nothing
        Dim blnResult As Boolean = False
        Try
            Dim objparam As SqlParameter

            cmd = New System.Data.SqlClient.SqlCommand("getICD9CPT", Conn)
            cmd.CommandType = CommandType.StoredProcedure

            objparam = cmd.Parameters.Add("@nExamId", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = nExamID

            objparam = cmd.Parameters.Add("@nVisitId", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = nVisitID

            blnResult = cmd.ExecuteScalar
            cmd.Parameters.Clear()

            Conn.Close()
            objparam = Nothing

            Return blnResult
        Catch ex As Exception
            UpdateLog("Error in Getting ICD9/CPT" & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            cmd = Nothing
            Conn.Close()
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try

    End Function
    Public Function FetchICD9CPTMod(ByVal nExamID As Int64, ByVal nVisitID As Int64, ByVal ICD9Code As String, ByVal CPTCode As String, ByVal MODCode As String, ByVal Flag As Integer, Optional ByVal ICDDescr As String = "") As DataTable
        Dim sda As New SqlDataAdapter
        Dim Cmd As SqlCommand = Nothing
        Try
            Dim dt As New DataTable

            Cmd = New System.Data.SqlClient.SqlCommand("gsp_GetICD9CPTMod", Conn)
            Cmd.CommandType = CommandType.StoredProcedure


            sda.SelectCommand = Cmd


            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = nExamID

            objParam = Cmd.Parameters.Add("@nVisitID", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = nVisitID

            objParam = Cmd.Parameters.Add("@sICD9Code", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ICD9Code

            objParam = Cmd.Parameters.Add("@sCPTcode", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CPTCode

            objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Flag

            ''Sanjog -Added on 20101030 for fetching the CPT from ICD Description
            objParam = Cmd.Parameters.Add("@sICD9Descr", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ICDDescr
            ''Sanjog -Added on 20101030 for fetching the CPT from ICD Description

            sda.Fill(dt)



            Conn.Close()

            objParam = Nothing
            Return dt
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(sda) Then  ''free sda problem 00000591
                sda.Dispose()
                sda = Nothing
            End If
            If IsNothing(Cmd) = False Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function


    '' SUDHIR 20090916 '' TO CLEAN RECORDS IF NO CPT/DIAGNOSIS TO SAVE ''
    Public Function CleanDiagTreatmentAssociation(ByVal ExamID As Long, ByVal VisitID As Long) As Boolean

        Conn.Open()
        Dim cmddelete As SqlCommand = Nothing
        Try
            ' Dim i As Integer
            Dim objparam As SqlParameter

            cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteExamICD9CPTModifier", Conn)
            cmddelete.CommandType = CommandType.StoredProcedure

            objparam = cmddelete.Parameters.Add("@nExamId", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = ExamID

            objparam = cmddelete.Parameters.Add("@nVisitId", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = VisitID

            cmddelete.ExecuteNonQuery()
            cmddelete.Parameters.Clear()

            Conn.Close()
            objparam = Nothing

            Return True
        Catch ex As Exception
            UpdateLog("ClsDiagnosisDBLayer -- CleanDiagTreatmentAssociation -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Cmd = Nothing
            cmddelete = Nothing
            Conn.Close()
            Return False
        Finally
            If Not IsNothing(cmddelete) Then  ''free cmddelete problem 00000591
                cmddelete.Parameters.Clear()
                cmddelete.Dispose()
                cmddelete = Nothing
            End If
        End Try
    End Function
    '' END SUDHIR ''
    Public Function GetProblemListTableStructure() As DataTable
        'Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim Conn As New SqlConnection(GetConnectionString())
        Dim daProblem As New SqlDataAdapter
        Dim dtProblem As New DataTable
        Dim Cmd As SqlCommand = Nothing
        Try

            Conn.Open()
            Cmd = New SqlCommand("gsp_GetProblemListTableStructure", Conn)

            Cmd.CommandType = CommandType.StoredProcedure
            daProblem.SelectCommand = Cmd


            daProblem.Fill(dtProblem)
            Conn.Close()
            Return dtProblem

        Catch ex As Exception
            Conn.Close()
            Return Nothing
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'oDB.Disconnect()
            'oDB.Dispose()
            'oDB = Nothing
            If IsNothing(Conn) = False Then
                Conn.Dispose()
                Conn = Nothing
            End If
            If IsNothing(daProblem) = False Then
                daProblem.Dispose()
                daProblem = Nothing
            End If
            If Not IsNothing(Cmd) Then ''free previous memory problem 00000591
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If

        End Try
    End Function

    Public Function FetchCommonCPT() As DataTable
        Dim sda As New SqlDataAdapter
        Dim Cmd As SqlCommand = Nothing
        Try
            Dim dt As New DataTable
            Dim objparam As SqlParameter
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_GetCommonCPT", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            objparam = Cmd.Parameters.Add("@CommonType", SqlDbType.SmallInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = 1


         
            sda.SelectCommand = Cmd
            sda.Fill(dt)
            Conn.Close()
            Return dt
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(sda) Then  ''free sda problem 00000591
                sda.Dispose()
                sda = Nothing
            End If
            If IsNothing(Cmd) = False Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.

            If Not IsNothing(Conn) Then
                Conn.Dispose()
                Conn = Nothing
            End If

            If Not IsNothing(Dv) Then
                Dv.Dispose()
                Dv = Nothing
            End If


            'If Not IsNothing(Cmd) Then
            '    Cmd.Dispose()
            '    Cmd = Nothing
            'End If

        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
