Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class ClsDiagnosisDBLayer
    Public Sub New()
        Dim sqlconn As String
        Try
            sqlconn = GetConnectionString()
            Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
            gstrMessageBoxCaption = GetMessageBoxCaption() 'Added by madan on 20100514
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' UpdateLog("ClsDiagnosisDBLayer -- New -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- New -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
        Catch ex As Exception
            'UpdateLog("ClsDiagnosisDBLayer -- New -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- New -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
        End Try
    End Sub
    Private Conn As SqlConnection
    'Private Dv As DataView
    ' Private Cmd As System.Data.SqlClient.SqlCommand
    ' Private ArrMedicationCol As New ArrayList
    'Public Function AddData(ByVal ExamID As Long, ByVal VisitID As Long, ByVal arrlist As ArrayList) As Boolean

    '    Conn.Open()
    '    Dim trDiagnosis As SqlTransaction
    '    trDiagnosis = Conn.BeginTransaction
    '    Dim cmddelete As SqlCommand
    '    Try
    '        Dim i As Integer
    '        Dim objparam As SqlParameter

    '        cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteDiagnosis", Conn)
    '        cmddelete.CommandType = CommandType.StoredProcedure
    '        cmddelete.Transaction = trDiagnosis

    '        objparam = cmddelete.Parameters.Add("@nExamId", SqlDbType.BigInt)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = ExamID

    '        objparam = cmddelete.Parameters.Add("@nVisitID", SqlDbType.BigInt)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = VisitID

    '        cmddelete.ExecuteNonQuery()
    '        cmddelete.Parameters.Clear()

    '        For i = 0 To arrlist.Count - 1
    '            Dim objmylist As mytable
    '            objmylist = CType(arrlist.Item(i), mytable)

    '            'Insert data in Diagnosis

    '            Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertDiagnosis", Conn)

    '            Cmd.CommandType = CommandType.StoredProcedure

    '            Cmd.Transaction = trDiagnosis
    '            objparam = Cmd.Parameters.Add("@nExamId", SqlDbType.BigInt)
    '            objparam.Direction = ParameterDirection.Input
    '            objparam.Value = ExamID

    '            objparam = Cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
    '            objparam.Direction = ParameterDirection.Input
    '            objparam.Value = VisitID

    '            objparam = Cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
    '            objparam.Direction = ParameterDirection.Input
    '            objparam.Value = gnPatientID

    '            objparam = Cmd.Parameters.Add("@sICD9Code", SqlDbType.VarChar)
    '            objparam.Direction = ParameterDirection.Input
    '            objparam.Value = objmylist.Code

    '            objparam = Cmd.Parameters.Add("@sICD9Description", SqlDbType.VarChar)
    '            objparam.Direction = ParameterDirection.Input
    '            objparam.Value = objmylist.Description

    '            'Insert data in ICD9Drugs

    '            Cmd.ExecuteNonQuery()

    '            Cmd.Parameters.Clear()

    '        Next

    '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ICD9Code Added for Patient", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success)
    '        'gloAuditTrail.gloAuditTrail.CreateLog(clsAudit.enmActivityType.Add, "ICD9Code Added for Patient", gstrLoginName, gstrClientMachineName, gnPatientID)

    '        trDiagnosis.Commit()
    '        Conn.Close()
    '        'If intMode = 1 Then 'Add
    '        '    objAudit.CreateLog(clsAudit.enmActivityType.Add, "Medication for Date " & Now & " Added", gstrLoginName, gstrClientMachineName, gnPatientID)
    '        'ElseIf intMode = 2 Then 'Modify
    '        '    objAudit.CreateLog(clsAudit.enmActivityType.Modify, "Medication for Date " & objMedication.PrescriptionDate & " Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
    '        'End If
    '        'objAudit = Nothing

    '        Return True
    '    Catch ex As SqlException
    '        trDiagnosis.Rollback()
    '        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        'UpdateLog("ClsDiagnosisDBLayer -- AddData -- " & ex.ToString)
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- AddData -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '        trDiagnosis = Nothing
    '        Cmd = Nothing
    '        cmddelete = Nothing
    '        Conn.Close()
    '        Return False
    '    Catch ex As Exception
    '        trDiagnosis.Rollback()
    '        'UpdateLog("ClsDiagnosisDBLayer -- AddData -- " & ex.ToString)
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- AddData -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        'trMedication.Rollback()
    '        trDiagnosis = Nothing
    '        Cmd = Nothing
    '        cmddelete = Nothing
    '        Conn.Close()
    '        Return False
    '    End Try
    'End Function
    Public ReadOnly Property DsDataview() As DataView
        Get
            'Dv = Ds.Tables("Category_Mst").DefaultView
            ' Return Dv
            'Return Ds
            Return Nothing
        End Get

    End Property
    Public Sub SortDataview(ByVal strsort As String)
        'Dv.Sort = strsort
    End Sub

    Public Sub ClearCol()
        ' ArrMedicationCol.Clear()
    End Sub

    '' SUDHIR 20090711 ''
    'Public Function GetAllICD9() As DataTable
    '    Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
    '    Try
    '        Dim dtICD9 As DataTable = Nothing
    '        Dim _Query As String = "SELECT nICD9ID, sICD9Code, sDescription FROM ICD9 WHERE sICD9Code <> ''"
    '        oDB.Connect(False)
    '        oDB.Retrive_Query(_Query, dtICD9)
    '        If dtICD9 IsNot Nothing Then
    '            Return dtICD9
    '        Else
    '            Return Nothing
    '        End If
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return Nothing
    '    Finally
    '        oDB.Disconnect()
    '        oDB.Dispose()
    '        oDB = Nothing
    '    End Try
    'End Function
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
    'Added Code for GettingMyWidth for DxCPT and SmartDx 
    Public Function GetMyWidthSetting(ByVal SettingName As String, ByVal gnLoginID As Long) As DataTable
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
    Public Function GetAllICD9ICD10(ByVal nICDRevision As Int16, ByVal SearchString As String) As DataTable
        Dim adpt As New SqlDataAdapter
        Dim dt As New DataTable
        Dim objParam As SqlParameter = Nothing
        Dim Cmd As System.Data.SqlClient.SqlCommand = Nothing
        Try
            Cmd = New SqlCommand("gsp_Diagnosis_Search", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            adpt.SelectCommand = Cmd

            objParam = Cmd.Parameters.Add("@SearchString", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = SearchString


            objParam = Cmd.Parameters.Add("@nICDRevision", SqlDbType.SmallInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = nICDRevision

            adpt.Fill(dt)
            Conn.Close()
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
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Not IsNothing(adpt) Then  ''free adpt against problem 00000591
                adpt.Dispose()
                adpt = Nothing
            End If
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function
    '' END SUDHIR ''

    Public Function FillICD9(Optional ByVal flag1 As Int16 = 0) As DataTable
        Dim adpt As New SqlDataAdapter
        Dim dt As New DataTable
        Dim objParam As SqlParameter = Nothing
        Dim Cmd As System.Data.SqlClient.SqlCommand = Nothing
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
            'UpdateLog("ClsDiagnosisDBLayer -- FillICD9 -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- FillICD9 -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception
            'UpdateLog("ClsDiagnosisDBLayer -- FillICD9 -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- FillICD9 -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Not IsNothing(adpt) Then  ''free adpt against problem 00000591
                adpt.Dispose()
                adpt = Nothing
            End If
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function

    Public Function FetchICD9forUpdate(ByVal ExamID As Long, ByVal VisitID As Long) As DataTable
        Dim objParam As SqlParameter = Nothing

        Dim sqladpt As New SqlDataAdapter
        Dim Cmd As SqlCommand = Nothing
        Try
            Dim dt As New DataTable

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
            'UpdateLog("ClsDiagnosisDBLayer -- FetchICD9forUpdate -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Select, "ClsDiagnosisDBLayer -- FetchICD9forUpdate -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception
            'UpdateLog("ClsDiagnosisDBLayer -- FetchICD9forUpdate -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Select, "ClsDiagnosisDBLayer -- FetchICD9forUpdate -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Not IsNothing(sqladpt) Then  ''free adpt against problem 00000591
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
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

    '' SUDHIR 20090827 ''
    Public Function GetCPTDrivenDiagnosis(ByVal nExamID As Int64, ByVal nVisitID As Int64, ByVal nPatientID As Int64) As ArrayList
        Dim cmd As SqlCommand = Nothing
        Try
            Dim con As New SqlClient.SqlConnection(GetConnectionString)
            Dim query As String
            query = " SELECT ISNULL(sCPTcode,'') AS sCPTcode, ISNULL(sCPTDescription,'') AS sCPTDescription, " _
            & " ISNULL(sICD9Code,'') AS sICD9Code, ISNULL(sICD9Description,'') AS sICD9Description, " _
            & " ISNULL(sModCode,'') AS sModCode, ISNULL(sModDescription,'') AS sModDescription, " _
            & " ISNULL(nUnit,0) AS nUnit, ISNULL(nLineNo,0) AS nLineNo " _
            & " FROM ExamICD9CPT WHERE nPatientID = " & nPatientID & " AND nExamID = " & nExamID & " AND nVisitID = " & nVisitID & " ORDER BY nLineNo "


            cmd = New SqlCommand(query, con)
            Dim adp As New SqlDataAdapter(cmd)
            Dim dtTreatment As New DataTable
            adp.Fill(dtTreatment)
            adp.Dispose()
            adp = Nothing
            con.Close()
            con.Dispose()
            con = Nothing
            If dtTreatment IsNot Nothing Then
                If dtTreatment.Rows.Count > 0 Then
                    Dim arrTreatment As New ArrayList
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
                        oList.ICD9No = CType(dtTreatment.Rows(iRow)("nLineNo"), Integer)

                        arrTreatment.Add(oList)
                    Next
                    dtTreatment.Dispose()
                    dtTreatment = Nothing
                    Return arrTreatment
                Else
                    dtTreatment.Dispose()
                    dtTreatment = Nothing
                    Return Nothing
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function


#Region " FOR EXAM ICD9 CPT"

    Public Function FetchExamICD9(ByVal ExamID As Long) As DataTable
        '' O/P = '' dt.COL(0) = ICD9Code,'' dt.COL(1) = ICD9Desc
        Dim objParam As SqlParameter = Nothing
        Dim Cmd As SqlCommand = Nothing
        Dim sqladpt As New SqlDataAdapter
        Try
            Dim dt As New DataTable

            Cmd = New System.Data.SqlClient.SqlCommand("gsp_ScanExamICD9", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = Cmd
            objParam = Cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ExamID

            sqladpt.Fill(dt)
            Conn.Close()
            Return dt
            '' dt.COL(0) = ICD9Code,
            '' dt.COL(1) = ICD9Desc
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("ClsDiagnosisDBLayer -- FetchExamICD9 -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Select, "ClsDiagnosisDBLayer -- FetchExamICD9 -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception
            'UpdateLog("ClsDiagnosisDBLayer -- FetchExamICD9 -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Select, "ClsDiagnosisDBLayer -- FetchExamICD9 -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Not IsNothing(sqladpt) Then  ''free adpt against problem 00000591
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function

    Public Function FetchExamICD9CPT(ByVal ExamID As Long, ByVal ICD9Code As String) As DataTable
        '''' O/P = '' dt.COL(0) = CPTcode, '' dt.COL(1) = CPTDesc
        Dim objParam As SqlParameter = Nothing
        Dim Cmd As SqlCommand = Nothing
        Dim sqladpt As New SqlDataAdapter
        Try
            Dim dt As New DataTable
            'Dim sqladpt As New SqlDataAdapter

            Cmd = New System.Data.SqlClient.SqlCommand("gsp_ScanExamICD9CPT", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = Cmd



            objParam = Cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ExamID

            objParam = Cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ICD9Code

            sqladpt.Fill(dt)
            Conn.Close()
            Return dt
            '' dt.COL(0) = CPTcode, 
            '' dt.COL(1) = CPTDesc
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("ClsDiagnosisDBLayer -- FetchExamICD9CPT -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Select, "ClsDiagnosisDBLayer -- FetchExamICD9CPT -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception
            'UpdateLog("ClsDiagnosisDBLayer -- FetchExamICD9CPT -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Select, "ClsDiagnosisDBLayer -- FetchExamICD9CPT -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Not IsNothing(sqladpt) Then  ''free adpt against problem 00000591
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function


    Public Function FetchExamICD9CPTModifier(ByVal ExamID As Long, ByVal ICD9Code As String, ByVal CPTCode As String) As DataTable
        ''''  O/P = dt.COL(0) = ModCode, '' dt.COL(1) = ModDesc, '' dt.COL(2) = Unit
        Dim objParam As SqlParameter = Nothing
        Dim Cmd As SqlCommand = Nothing
        Dim sqladpt As New SqlDataAdapter
        Try
            Dim dt As New DataTable
            'Dim sqladpt As New SqlDataAdapter

            Cmd = New System.Data.SqlClient.SqlCommand("gsp_ScanExamICD9CPTModifier", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = Cmd



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
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Not IsNothing(sqladpt) Then  ''free adpt against problem 00000591
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            If (IsNothing(Cmd) = False) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function

    Public Function SaveDiagTreatmentAssociation(ByVal ExamID As Long, ByVal PatientID As Long, ByVal VisitID As Long, ByVal arrlist As ArrayList, Optional ByVal isTreatment As Boolean = False) As Boolean

        Conn.Open()
        Dim trDiagnosis As SqlTransaction
        trDiagnosis = Conn.BeginTransaction
        Dim cmddelete As SqlCommand = Nothing
        Dim objparam As SqlParameter = Nothing
        Dim Cmd As SqlCommand = Nothing
        'Dim sqladpt As New SqlDataAdapter
        Try
            Dim i As Integer
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
            For i = 0 To arrlist.Count - 1
                'Dim lst As myList
                '' SUDHIR 20090730 ''
                Dim lst As Object
                If isTreatment = False Then
                    lst = CType(arrlist.Item(i), myList)
                Else
                    lst = CType(arrlist.Item(i), gloEMRGeneralLibrary.glogeneral.myList)
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

                objparam = Cmd.Parameters.Add("@nICDRevision", SqlDbType.SmallInt)
                objparam.Direction = ParameterDirection.Input
                If isTreatment = False Then
                    objparam.Value = CType(DirectCast(lst, myList).nICDRevision, Int16)
                Else
                    objparam.Value = CType(DirectCast(lst, gloEMRGeneralLibrary.glogeneral.myList).nICDRevision, Int16)
                End If
                'objparam.Value = CType(DirectCast(lst, myList).nICDRevision, Int16)

                'Insert data in ExamICD9CPT

                Cmd.ExecuteNonQuery()

                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            Next

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "Treatment are associated with Diagnosis", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
            'gloAuditTrail.gloAuditTrail.CreateLog(clsAudit.enmActivityType.Add, "Treatment are associated with Diagnosis", gstrLoginName, gstrClientMachineName, gnPatientID)

            trDiagnosis.Commit()
            Conn.Close()
            'If intMode = 1 Then 'Add
            '    objAudit.CreateLog(clsAudit.enmActivityType.Add, "Medication for Date " & Now & " Added", gstrLoginName, gstrClientMachineName, gnPatientID)
            'ElseIf intMode = 2 Then 'Modify
            '    objAudit.CreateLog(clsAudit.enmActivityType.Modify, "Medication for Date " & objMedication.PrescriptionDate & " Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
            'End If
            'objAudit = Nothing

            Return True
        Catch ex As SqlException
            trDiagnosis.Rollback()
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("ClsDiagnosisDBLayer -- SaveDiagTreatmentAssociation -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- SaveDiagTreatmentAssociation -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            trDiagnosis = Nothing
            Cmd = Nothing
            cmddelete = Nothing
            Conn.Close()
            Return False
        Catch ex As Exception
            trDiagnosis.Rollback()
            'UpdateLog("ClsDiagnosisDBLayer -- SaveDiagTreatmentAssociation -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- SaveDiagTreatmentAssociation -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            trDiagnosis = Nothing
            Cmd = Nothing
            cmddelete = Nothing
            Conn.Close()
            Return False
        Finally
            If cmddelete IsNot Nothing Then
                cmddelete.Parameters.Clear()
                cmddelete.Dispose()
                cmddelete = Nothing
            End If
            If Not IsNothing(objparam) Then
                objparam = Nothing
            End If
            If (IsNothing(trDiagnosis) = False) Then
                trDiagnosis.Dispose()
                trDiagnosis = Nothing
            End If
        End Try
    End Function


    Public Function SaveSmartDiagTreatmentAssociation(ByVal ExamID As Long, ByVal PatientID As Long, ByVal VisitID As Long, ByVal arrlist As ArrayList) As Boolean
        Conn.Open()
        Dim trDiagnosis As SqlTransaction
        trDiagnosis = Conn.BeginTransaction
        Dim cmddelete As SqlCommand = Nothing
        Dim cmdselect As SqlCommand = Nothing
        Dim objparam As SqlParameter = Nothing
        Try
            Dim i As Integer


            'cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteExamICD9CPTModifier", Conn)
            'cmddelete.CommandType = CommandType.StoredProcedure
            'cmddelete.Transaction = trDiagnosis

            'objparam = cmddelete.Parameters.Add("@nExamId", SqlDbType.BigInt)
            'objparam.Direction = ParameterDirection.Input
            'objparam.Value = ExamID

            'objparam = cmddelete.Parameters.Add("@nVisitId", SqlDbType.BigInt)
            'objparam.Direction = ParameterDirection.Input
            'objparam.Value = VisitID

            'cmddelete.ExecuteNonQuery()
            'cmddelete.Parameters.Clear()
            'Dim strSelectQry = "Select nPatientID,nExamID,nVisitID,sICD9Code,sICD9Description,sCPTcode,sCPTDescription,sModCode,sModDescription,nUnit from ExamICD9CPT where nExamId = " & ExamID & " and nVisitID = " & VisitID & ""

            Dim dt As DataTable
            Dim da As SqlDataAdapter
         
            Dim isPresent As Boolean = False
            Dim oDB As New gloStream.gloDataBase.gloDataBase
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
                If IsNothing(dt) = False Then
                    For j As Integer = 0 To dt.Rows.Count - 1
                        If dt.Rows(j)("nExamId") = ExamID And dt.Rows(j)("nVisitId") = VisitID And dt.Rows(j)("sICD9Code") = lst.Code And dt.Rows(j)("sICD9Description") = lst.Description And dt.Rows(j)("sCPTcode") = lst.HistoryCategory And dt.Rows(j)("sCPTDescription") = lst.HistoryItem Then
                            isPresent = True
                            Exit For
                        Else
                            isPresent = False
                        End If
                    Next
              
                End If
                

                'Insert data in ExamICD9CPT
                If isPresent = False Then
                    Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_InsertExamICD9CPTModifier", Conn)
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

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "Treatment are associated with Diagnosis", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success)
            'gloAuditTrail.gloAuditTrail.CreateLog(clsAudit.enmActivityType.Add, "Treatment are associated with Diagnosis", gstrLoginName, gstrClientMachineName, gnPatientID)

            trDiagnosis.Commit()
            Conn.Close()
            'If intMode = 1 Then 'Add
            '    objAudit.CreateLog(clsAudit.enmActivityType.Add, "Medication for Date " & Now & " Added", gstrLoginName, gstrClientMachineName, gnPatientID)
            'ElseIf intMode = 2 Then 'Modify
            '    objAudit.CreateLog(clsAudit.enmActivityType.Modify, "Medication for Date " & objMedication.PrescriptionDate & " Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
            'End If
            'objAudit = Nothing

            Return True
        Catch ex As SqlException
            trDiagnosis.Rollback()
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("ClsDiagnosisDBLayer -- SaveSmartDiagTreatmentAssociation -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- SaveSmartDiagTreatmentAssociation -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            trDiagnosis = Nothing
            'Cmd = Nothing
            cmddelete = Nothing
            Conn.Close()
            Return False
        Catch ex As Exception
            trDiagnosis.Rollback()
            'UpdateLog("ClsDiagnosisDBLayer -- SaveSmartDiagTreatmentAssociation -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- SaveSmartDiagTreatmentAssociation -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            If cmdselect IsNot Nothing Then
                cmdselect.Parameters.Clear()
                cmdselect.Dispose()
                cmdselect = Nothing
            End If
            If Not IsNothing(objparam) Then
                objparam = Nothing
            End If
            If (IsNothing(trDiagnosis) = False) Then
                trDiagnosis.Dispose()
                trDiagnosis = Nothing
            End If
        End Try
    End Function

    'Public Function FillProblemList(ByVal PatientID As Int64, ByVal VisitID As Int64, ByVal DOS As DateTime, ByVal ICD9Code As String, ByVal ICD9Description As String)
    '    Try

    '        ''nProblemID, nVisitID, nPatientID, dtDOS, sICD9Code, sICD9Desc, sCheifComplaint, nProblemStatus, sPrescription, nUserID, sUserName, sMachineName
    '        Dim strQRY As String
    '        Dim _result As Object
    '        Dim sqlParam As SqlParameter
    '        Dim ID As Int64 = 0
    '        ICD9Description = ReplaceSpecialCharacters(ICD9Description)
    '        strQRY = "SELECT nProblemID FROM ProblemList WHERE nPatientID = " & PatientID & " AND sCheifComplaint = '" + UCase(ICD9Description.Replace("'", "''")).ToString.Trim + "'"
    '        Cmd = New System.Data.SqlClient.SqlCommand(strQRY, Conn)
    '        Conn.Open()
    '        _result = Cmd.ExecuteScalar()
    '        ID = Convert.ToInt64(_result)


    '        Cmd = New SqlCommand("gsp_InUpProblemList", Conn)
    '        Cmd.CommandType = CommandType.StoredProcedure


    '        '@ProblemID 	numeric(18,0) OutPut,
    '        '@PatientID 	numeric(18,0),
    '        '@VisitID       numeric(18,0),
    '        '@DOS 		    datetime,
    '        '@ICD9Code	    Varchar(50),	
    '        '@ICD9Desc	    Varchar(255),	
    '        '@CheifComplaint	Varchar(255),
    '        '@Prescription	    VarChar(255),
    '        '@ProblemStatus		Int,	
    '        '@UserID 	    numeric(18,0),
    '        '@MachineID	    Numeric(18,0)=0

    '        sqlParam = Cmd.Parameters.Add("@PatientID", PatientID)
    '        sqlParam.Direction = ParameterDirection.Input

    '        sqlParam = Cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = VisitID


    '        sqlParam = Cmd.Parameters.Add("@DOS", SqlDbType.DateTime)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = DOS

    '        sqlParam = Cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar, 50)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = ICD9Code

    '        sqlParam = Cmd.Parameters.Add("@ICD9Desc", SqlDbType.VarChar, 255)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = ICD9Description

    '        sqlParam = Cmd.Parameters.Add("@CheifComplaint", SqlDbType.VarChar, 255)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = ICD9Description

    '        sqlParam = Cmd.Parameters.Add("@Prescription", SqlDbType.VarChar, 255)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = ""

    '        sqlParam = Cmd.Parameters.Add("@ProblemStatus", SqlDbType.Int)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = Convert.ToInt16(frmProblemList.Status.Active.GetHashCode())

    '        ''By Mahesh ' 20070316

    '        sqlParam = Cmd.Parameters.Add("@UserID", SqlDbType.BigInt)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = gnLoginID




    '        sqlParam = Cmd.Parameters.Add("@MachineID", GetPrefixTransactionID)
    '        sqlParam.Direction = ParameterDirection.Input

    '        sqlParam = Cmd.Parameters.Add("@ProblemID", ID)
    '        sqlParam.Direction = ParameterDirection.InputOutput


    '        If Conn.State = ConnectionState.Closed Then
    '            Conn.Open()
    '        End If
    '        Cmd.ExecuteNonQuery()




    '        '' dt.COL(0) = ModCode, 
    '        '' dt.COL(1) = ModDesc, 
    '        '' dt.COL(2) = Unit
    '    Catch ex As SqlException
    '        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        'UpdateLog("ClsDiagnosisDBLayer -- FillProblemList -- " & ex.ToString)
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- FillProblemList -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '    Catch ex As Exception
    '        'UpdateLog("ClsDiagnosisDBLayer -- FillProblemList -- " & ex.ToString)
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- FillProblemList -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        If Conn.State = ConnectionState.Open Then
    '            Conn.Close()
    '        End If
    '    End Try


    'End Function

    Public Function FetchICD9CPTMod(ByVal nExamID As Int64, ByVal nVisitID As Int64, ByVal ICD9Code As String, ByVal CPTCode As String, ByVal MODCode As String, ByVal Flag As Integer) As DataTable
        Dim objParam As SqlParameter = Nothing
        Dim Cmd As SqlCommand = Nothing
        Dim sda As New SqlDataAdapter
        Try
            Dim dt As New DataTable
            'Dim sda As New SqlDataAdapter

            Cmd = New System.Data.SqlClient.SqlCommand("gsp_GetICD9CPTMod", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            sda.SelectCommand = Cmd



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

            sda.Fill(dt)
            Conn.Close()
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return Nothing
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
            If Not IsNothing(sda) Then  ''free adpt against problem 00000591
                sda.Dispose()
                sda = Nothing
            End If
            If (IsNothing(Cmd) = False) Then
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
        Dim objparam As SqlParameter = Nothing
        Try
            'Dim i As Integer


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

            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            UpdateLog("ClsDiagnosisDBLayer -- CleanDiagTreatmentAssociation -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally

            If Not IsNothing(objparam) Then
                objparam = Nothing
            End If
            'Cmd = Nothing
            If cmddelete IsNot Nothing Then
                cmddelete.Parameters.Clear()
                cmddelete.Dispose()
                cmddelete = Nothing
            End If
            Conn.Close()

        End Try
    End Function
    '' END SUDHIR ''
#End Region
End Class
