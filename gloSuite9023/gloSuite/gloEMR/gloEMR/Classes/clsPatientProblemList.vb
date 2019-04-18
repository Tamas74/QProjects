Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase

Public Class clsPatientProblemList
    Implements IDisposable

    Public Function Fill_ProblemLists(ByVal nPatientId As Long) As DataTable
        Dim objCon As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_FillProblemList", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PatientID", nPatientId)
            '' chetan added on 15-nov-2010
            cmd.Parameters.AddWithValue("@Specialty", gstrSpeciality)
            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            objCon.Close()
            Return dt
        Catch ex As Exception
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function
    Public Function Fill_ActiveProblemLists(ByVal nPatientId As Long) As DataTable
        Dim objCon As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_FillActiveProblemList", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PatientID", nPatientId)
            '' chetan added on 15-nov-2010
            cmd.Parameters.AddWithValue("@Specialty", gstrSpeciality)
            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            objCon.Close()
            Return dt
        Catch ex As Exception
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Public Function Get_ProblemList(ByVal ProblemID As Long, ByVal VisitID As Long) As DataTable
        Dim objCon As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Dim ExamParam As SqlParameter
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_GetProblemList", objCon)
            cmd.CommandType = CommandType.StoredProcedure


            ExamParam = cmd.Parameters.Add("@ProblemID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = ProblemID

            ExamParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = VisitID

            'cmd.Parameters.Add("@ProblemID", SqlDbType.BigInt, ProblemID)
            'cmd.Parameters.Add("@VisitID", SqlDbType.BigInt, VisitID)
            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            objCon.Close()
            Return dt

        Catch ex As Exception
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            ExamParam = Nothing
        End Try
    End Function

    Public Function Get_ProblemListRx(ByVal _PatientId As Int64) As DataTable

        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Dim oResult As DataTable = Nothing
        Try
            ''Query changed to show all drugs (duplicate also)on prescription form in problem list also
            ' strSQL = "SELECT DISTINCT IsNull(sMedication,'') as DrugName, IsNull(sDosage,'') as Dosage  FROM Prescription WHERe nPatientID = " & _PatientId & " order by DrugName"
            strSQL = "SELECT  IsNull(sMedication,'') as DrugName, IsNull(sDosage,'') as Dosage  FROM Prescription WHERe nPatientID = " & _PatientId & " order by DrugName"

            oResult = oDB.GetDataTable_Query(strSQL)
            If Not oResult Is Nothing Then
                Return oResult
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
            'If Not IsNothing(oResult) Then
            '    oResult.Dispose()
            '    oResult = Nothing
            'End If
        End Try
    End Function


    Public Function Get_ProblemListDiagnosis(ByVal VisitID As Long, ByVal PatientID As Long) As DataTable
        Dim objCon As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Dim ExamParam As SqlParameter
        Try
            '' to get the All Diagnosis of the selected Visit of the Patient
            objCon = New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_GetProblemListDiagnosis", objCon)
            cmd.CommandType = CommandType.StoredProcedure


            ExamParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = VisitID
            ''Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            'ExamParam = cmd.Parameters.Add("@PatientID", gnPatientID)
            ExamParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            'end modification
            ExamParam.Direction = ParameterDirection.Input

            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            objCon.Close()

            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            ExamParam = Nothing
        End Try
    End Function

    Public Function Get_ProblemListICD9(ByVal VisitID As Long, ByVal PatientID As Long) As DataTable
        Dim objCon As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Dim ExamParam As SqlParameter
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_GetProblemListICD9", objCon)
            cmd.CommandType = CommandType.StoredProcedure


            ExamParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = VisitID
            ExamParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            ExamParam.Direction = ParameterDirection.Input

            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            objCon.Close()
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            ExamParam = Nothing
        End Try
    End Function

    Public Function Get_DefaultSnomedforICD(ByVal ICD9Code As String, ByVal ICD9Description As String) As DataTable
        Dim dtSnomed As DataTable = Nothing
        Return dtSnomed
    End Function


    Public Function Get_Icd9Snomed(ByVal nPatientId As Long, ByVal nVisitId As Long) As DataTable
        Dim oDB As New DataBaseLayer
        Dim dt As DataTable = Nothing

        Try
            Dim strQuery As String = "select nProblemID ,nVisitID,dtDOS,dtDischargeDate,sICD9Code,sICD9Desc ,sConceptID,sTransactionID1 from ProblemList where nPatientID =" & nPatientId & ""
            dt = oDB.GetDataTable_Query(strQuery)
            Return dt
        Catch ex As Exception

            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try
    End Function



    Public Function Get_ICD9ImmediancyDefault(ByVal ICD9Code As String, ByVal ICD9Description As String) As DataTable
        Dim oDB As New DataBaseLayer
        Dim dt As DataTable = Nothing

        Try
            Dim strQuery As String = " SELECT ISNULL(nImmediacyDefault,3) as nImmediacyDefault FROM dbo.ICD9 where  sICD9Code = '" & ICD9Code & "'"
            dt = oDB.GetDataTable_Query(strQuery)
            Return dt
        Catch ex As Exception

            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try
    End Function
    

    Public Function IsSnomedMandatory() As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim Con As New SqlConnection(GetConnectionString())
        Dim _IsSnomedMandatory As Boolean = False
        Try
            Dim strQry As String = "SELECT ISNULL(sSettingsValue,0) as IsSnomedCTMandatory FROM dbo.Settings WHERE sSettingsName ='REQUIRESNOMEDCT'"

            cmd = New SqlCommand(strQry, Con)
            Con.Open()
            _IsSnomedMandatory = cmd.ExecuteScalar()
            Con.Close()
            Return _IsSnomedMandatory
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(Con) Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If

        End Try
    End Function
    Public Function getSnomedCodemissingProblem(ByVal nPatientID As Int64) As Int64
        Dim cmd As SqlCommand = Nothing
        Dim Con As New SqlConnection(GetConnectionString())
        Dim _cntProb As Int64 = 0
        Try
            Dim strQry As String = "Select count(nProblemID) FROM dbo.ProblemList WHERE  nProblemStatus=2 AND nPatientID =" & nPatientID & " AND ISNULL(sConceptID,'')=''"

            cmd = New SqlCommand(strQry, Con)
            Con.Open()
            _cntProb = CType(cmd.ExecuteScalar(), Int64)
            Con.Close()
            Return _cntProb
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(Con) Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If

        End Try
    End Function
    

    Public Function IsAutogeneratedProblem() As Object
        Dim oDB As New DataBaseLayer
        Dim value As String = Nothing

        Try
            Dim strQuery As String = "SELECT Top 1 ISNULL(sSettingsValue,0) as IsAutogeneratedProblem FROM dbo.Settings WHERE sSettingsName ='AUTOGENERATEDPROBLEMFROMEXAM'"
            value = oDB.GetRecord_Query(strQuery)
            Return value
        Catch ex As Exception

            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try
    End Function

    Public Function IsProblemExists(ByVal VisitID As Long) As String
        Dim Conn As New SqlConnection
        Dim Cmd As SqlCommand = Nothing
        Dim objParam As SqlParameter
        Try
            Conn.ConnectionString = GetConnectionString()
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_CheckProblemList", Conn)
            Cmd.CommandType = CommandType.StoredProcedure


            objParam = Cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitID

            Conn.Open()
            Dim rd As SqlDataReader
            Dim strComplaints As String = ""
            rd = Cmd.ExecuteReader
            Do While rd.Read
                strComplaints = rd(0) & "|" & rd(1)
            Loop
            rd.Close()
            rd = Nothing
            Conn.Close()
            '' Problem is Exists for the given Visit
            Return strComplaints & ""

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ""
        Finally
            If Conn.State <> ConnectionState.Closed Then
                Conn.Close()
            End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Not IsNothing(Conn) Then
                Conn.Dispose()
                Conn = Nothing
            End If
            objParam = Nothing
        End Try

    End Function

    '' SaveProblemList(gnPatientID, VisitID, Format(dtpDOS.Value, "MM/dd/yyyy"), Arrlist)


    Public Function DeleteProblemList(ByVal PatientID As Long, ByVal ArrList As ArrayList)
        Dim Con As New SqlConnection(GetConnectionString)
        ' Dim cmd As SqlCommand
        ' Dim cmdDelete As SqlCommand
        Dim trProbList As SqlTransaction = Nothing
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        trProbList = Con.BeginTransaction()
        Try
            Dim lst As myList
            If ArrList IsNot Nothing Then
                For i As Int16 = 0 To ArrList.Count - 1
                    'lst = New myList
                    lst = CType(ArrList(i), myList)
                    '  Dim sqlParam As SqlParameter
                    Dim ExamParam As SqlParameter

                    If lst.ID <> 0 Then

                        Dim cmd As SqlCommand = New SqlCommand("gsp_DeleteProblemList", Con)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Transaction = trProbList

                        ExamParam = cmd.Parameters.AddWithValue("@ProblemID", lst.ID)
                        ExamParam.Direction = ParameterDirection.Input

                        If Con.State = ConnectionState.Closed Then
                            Con.Open()
                        End If

                        If cmd.ExecuteNonQuery() > 0 Then

                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Delete, "Problem Deleted for date " & lst.VisitDate, PatientID, lst.ID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            ''

                        End If
                        cmd.Parameters.Clear()
                        cmd.Dispose()
                        cmd = Nothing
                    End If
                    ExamParam = Nothing

                Next
            End If
           
            trProbList.Commit()
            Return True
        Catch ex As SqlException

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            trProbList.Rollback()
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            trProbList.Rollback()
            Return False
        Finally
            If (IsNothing(trProbList) = False) Then
                trProbList.Dispose()
                trProbList = Nothing
            End If
            Con.Close()
            Con.Dispose()
            Con = Nothing
        End Try
    End Function
    Public Function SaveProblemList(ByVal PatientID As Long, ByVal VisitID As Long, ByRef ArrList As ArrayList, Optional ByVal dtRecProblist As DataTable = Nothing) As Boolean  ''dtRecProblist added for reconcilation type problem 
        Dim Con As New SqlConnection(GetConnectionString)
        'Dim cmd As SqlCommand
        ' Dim cmdDelete As SqlCommand
        Dim trProbList As SqlTransaction = Nothing
        Dim nProblemId As Int64 = 0

        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If

        Dim ExamParam As SqlParameter
        Try

            If ArrList.Count > 0 Then
                Dim lst As myList
                trProbList = Con.BeginTransaction()
                For i As Int16 = 0 To ArrList.Count - 1
                    'lst = New myList
                    lst = CType(ArrList(i), myList)
                    'Dim sqlParam As SqlParameter


                    ' If lst.ID = 0 Then


                    If lst.ID <> 0 AndAlso lst.ParameterName = "" Then
                        '''' if chief Complaints are not inserted then delete that problem list
                        Dim cmd As SqlCommand = New SqlCommand("gsp_DeleteProblemList", Con)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Transaction = trProbList

                        ExamParam = cmd.Parameters.AddWithValue("@ProblemID", lst.ID)
                        ExamParam.Direction = ParameterDirection.Input

                        If Con.State = ConnectionState.Closed Then
                            Con.Open()
                        End If

                        If cmd.ExecuteNonQuery() > 0 Then

                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Delete, "'Problem List Deleted for date " & lst.VisitDate & "'", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            ''

                        End If
                        cmd.Parameters.Clear()
                        cmd.Dispose()
                        cmd = Nothing
                    Else

                        ''
                        '''' if chief Complaints are not inserted then delete that problem list

                        '' Insert Or Update problem List
                        Dim cmd As SqlCommand = New SqlCommand("gsp_InUpProblemList", Con)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Transaction = trProbList


                        ExamParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
                        ExamParam.Direction = ParameterDirection.Input

                        ExamParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
                        ExamParam.Direction = ParameterDirection.Input
                        If Val(lst.HistoryItem) > 0 Then
                            ExamParam.Value = CLng(lst.HistoryItem)
                        Else
                            ExamParam.Value = 0
                        End If

                        ExamParam = cmd.Parameters.Add("@DOS", SqlDbType.DateTime)
                        ExamParam.Direction = ParameterDirection.Input
                        ExamParam.Value = lst.VisitDate

                        If (gblnEnableCQMCypressTesting) Then
                            ExamParam = cmd.Parameters.Add("@dtDischargeDate", SqlDbType.DateTime)
                            ExamParam.Direction = ParameterDirection.Input
                            ExamParam.Value = lst.DischargeDate
                        End If

                        ExamParam = cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar, 50)
                        ExamParam.Direction = ParameterDirection.Input
                        ExamParam.Value = lst.Code

                        ExamParam = cmd.Parameters.Add("@ICD9Desc", SqlDbType.VarChar, 255)
                        ExamParam.Direction = ParameterDirection.Input
                        ExamParam.Value = lst.Description

                        ExamParam = cmd.Parameters.Add("@CheifComplaint", SqlDbType.VarChar, 255)
                        ExamParam.Direction = ParameterDirection.Input
                        ExamParam.Value = lst.ParameterName

                        ExamParam = cmd.Parameters.Add("@Prescription", SqlDbType.VarChar, 255)
                        ExamParam.Direction = ParameterDirection.Input
                        ExamParam.Value = lst.HistoryCategory

                        ExamParam = cmd.Parameters.Add("@ProblemStatus", SqlDbType.Int)
                        ExamParam.Direction = ParameterDirection.Input
                        ExamParam.Value = Convert.ToInt16(lst.Value)

                        ''By Mahesh ' 20070316
                        Dim objMessages As New clsMessage
                        ExamParam = cmd.Parameters.Add("@UserID", SqlDbType.BigInt)
                        ExamParam.Direction = ParameterDirection.Input
                        If lst.Index = 0 Then
                            ExamParam.Value = objMessages.GetUserID(gstrLoginName)
                        Else
                            ExamParam.Value = lst.Index
                        End If
                        objMessages.Dispose()
                        objMessages = Nothing
                        ''

                        ExamParam = cmd.Parameters.AddWithValue("@MachineID", GetPrefixTransactionID)
                        ExamParam.Direction = ParameterDirection.Input

                        ExamParam = cmd.Parameters.AddWithValue("@ProblemID", lst.ID)
                        ExamParam.Direction = ParameterDirection.InputOutput

                        '' chetan added 07 oct 2010
                        ''''''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 140110
                        ExamParam = cmd.Parameters.Add("@RsDt", SqlDbType.DateTime)
                        ExamParam.Direction = ParameterDirection.Input
                        If lst.ResolvedDate <> Nothing Then
                            ExamParam.Value = lst.ResolvedDate
                        Else
                            ExamParam.Value = "01/01/1900"
                        End If


                        ExamParam = cmd.Parameters.Add("@RsComment", SqlDbType.VarChar, 255)
                        ExamParam.Direction = ParameterDirection.Input
                        If lst.RComment <> Nothing AndAlso lst.RComment.Trim() <> "" Then
                            ExamParam.Value = lst.RComment
                        Else
                            ExamParam.Value = ""
                        End If

                        ''''''''''''' Added by Ujwala Atre - to add Resolved Date & Comment - as on 140110

                        ExamParam = cmd.Parameters.Add("@nImmediacy", SqlDbType.Int)
                        ExamParam.Direction = ParameterDirection.Input

                        If lst.Immediacy <> Nothing AndAlso lst.Immediacy.ToString().Trim() <> "" Then
                            ExamParam.Value = lst.Immediacy
                        Else
                            ExamParam.Value = 3
                        End If


                        ExamParam = cmd.Parameters.Add("@sComments", SqlDbType.VarChar)
                        ExamParam.Direction = ParameterDirection.Input
                        If lst.Comments <> Nothing Then
                            If lst.Comments.ToString().Trim() <> "" Then
                                ExamParam.Value = lst.Comments
                            End If
                        Else
                            ExamParam.Value = ""
                        End If



                        'ExamParam = cmd.Parameters.Add("@nStatus", SqlDbType.Int)
                        'ExamParam.Direction = ParameterDirection.Input
                        'ExamParam.Value = lst.Status


                        ExamParam = cmd.Parameters.Add("@sProvider", SqlDbType.VarChar)
                        ExamParam.Direction = ParameterDirection.Input

                        If lst.Provider <> Nothing AndAlso Convert.ToString(lst.Provider).Trim() <> "" Then
                            ExamParam.Value = lst.Provider
                        Else
                            ExamParam.Value = ""
                        End If


                        ExamParam = cmd.Parameters.Add("@sLocation", SqlDbType.VarChar)
                        ExamParam.Direction = ParameterDirection.Input

                        If lst.Location <> Nothing AndAlso lst.Location.ToString().Trim() <> "" Then
                            ExamParam.Value = lst.Location
                        Else
                            ExamParam.Value = ""
                        End If


                        ExamParam = cmd.Parameters.Add("@dtModifiedDate", SqlDbType.DateTime)
                        ExamParam.Direction = ParameterDirection.Input
                        '' ExamParam.Value = lst.LastModified

                        If lst.LastModified <> Nothing AndAlso lst.LastModified.ToString().Trim() <> "" Then
                            If Not (lst.LastModified.ToString().Contains("1/1/0001") OrElse lst.LastModified.ToString().Contains("01/01/0001")) Then
                                ExamParam.Value = lst.LastModified.ToString()
                            End If


                        End If

                        ExamParam = cmd.Parameters.Add("@ExamID", SqlDbType.VarChar)
                        ExamParam.Direction = ParameterDirection.Input
                        '' ExamParam.Value = lst.ExamID
                        If lst.ExamID <> Nothing AndAlso lst.ExamID.ToString().Trim() <> "" Then
                            ExamParam.Value = lst.ExamID
                        Else
                            ExamParam.Value = ""
                        End If



                        ExamParam = cmd.Parameters.Add("@sTranUser", SqlDbType.VarChar)
                        ExamParam.Direction = ParameterDirection.Input
                        ExamParam.Value = gstrLoginName



                        ExamParam = cmd.Parameters.Add("@sConceptID", SqlDbType.VarChar)
                        ExamParam.Direction = ParameterDirection.Input
                        '' ExamParam.Value = lst.ConceptId
                        If lst.ConceptId <> Nothing AndAlso lst.ConceptId.ToString().Trim() <> "" Then
                            ExamParam.Value = lst.ConceptId
                        Else
                            ExamParam.Value = ""
                        End If

                        ExamParam = cmd.Parameters.Add("@sSnoMedID", SqlDbType.VarChar)
                        ExamParam.Direction = ParameterDirection.Input
                        '' ExamParam.Value = lst.SnowMadeID

                        If lst.SnowMadeID <> Nothing AndAlso lst.SnowMadeID.ToString().Trim() <> "" Then
                            ExamParam.Value = lst.SnowMadeID
                        Else
                            ExamParam.Value = ""
                        End If

                        ExamParam = cmd.Parameters.Add("@sDescriptionID", SqlDbType.VarChar)
                        ExamParam.Direction = ParameterDirection.Input
                        ' ExamParam.Value = lst.DescId

                        If lst.DescId <> Nothing AndAlso lst.DescId.ToString().Trim() <> "" Then
                            ExamParam.Value = lst.DescId
                        Else
                            ExamParam.Value = ""
                        End If

                        ExamParam = cmd.Parameters.Add("@sDescription", SqlDbType.VarChar)
                        ExamParam.Direction = ParameterDirection.Input


                        If lst.SnoDescription <> Nothing Then

                            If lst.SnoDescription.ToString().Trim() <> "" Then
                                ExamParam.Value = lst.SnoDescription
                            End If
                        Else
                            ExamParam.Value = ""
                        End If

                        ExamParam = cmd.Parameters.Add("@sTransactionID1", SqlDbType.VarChar)
                        ExamParam.Direction = ParameterDirection.Input
                        ExamParam.Value = lst.AssociatedProperty

                        'ExamParam = cmd.Parameters.Add("@sTranUser", SqlDbType.VarChar)
                        'ExamParam.Direction = ParameterDirection.Input
                        'ExamParam.Value = gstrLoginName


                        If lst.AssociatedProperty <> Nothing AndAlso lst.AssociatedProperty.ToString().Trim() <> "" Then
                            ExamParam.Value = lst.AssociatedProperty
                        Else
                            ExamParam.Value = ""
                        End If
                        '' chetan integrated 

                        ExamParam = cmd.Parameters.Add("@bIsEncounterDiagnosis", SqlDbType.Bit)
                        ExamParam.Direction = ParameterDirection.Input
                        ExamParam.Value = lst.IsEncounterDiagnosis

                        ExamParam = cmd.Parameters.Add("@nICDRevision", SqlDbType.SmallInt)
                        ExamParam.Direction = ParameterDirection.Input
                        ExamParam.Value = lst.nICDRevision

                        ExamParam = cmd.Parameters.Add("@sReasonConceptID", SqlDbType.VarChar)
                        ExamParam.Direction = ParameterDirection.Input
                        ExamParam.Value = lst.ReasonConceptID

                        ExamParam = cmd.Parameters.Add("@sReasonConceptDesc", SqlDbType.VarChar)
                        ExamParam.Direction = ParameterDirection.Input
                        ExamParam.Value = lst.ReasonConceptDesc

                        ExamParam = cmd.Parameters.AddWithValue("@Rx_ProblemMedicationReconcillation", dtRecProblist) ''Medication Reconcillation for problem list( 2015 certification)
                        ExamParam.SqlDbType = SqlDbType.Structured


                        ExamParam = cmd.Parameters.Add("@sConcernStatus", SqlDbType.VarChar)
                        ExamParam.Direction = ParameterDirection.Input
                        ExamParam.Value = lst.sConcernStatus

                        If lst.sConcernStatus = "Completed" Then
                            ExamParam = cmd.Parameters.Add("@dtConcernEndDate", SqlDbType.DateTime)
                            ExamParam.Direction = ParameterDirection.Input
                            ExamParam.Value = DateTime.Now

                        End If

                        ExamParam = cmd.Parameters.Add("@sProblemType", SqlDbType.VarChar)
                        ExamParam.Direction = ParameterDirection.Input
                        ExamParam.Value = lst.sCDAProblemType

                        ExamParam = cmd.Parameters.Add("@nProviderID", SqlDbType.BigInt)
                        ExamParam.Direction = ParameterDirection.Input
                        ExamParam.Value = gloGlobal.gloPMGlobal.LoginProviderID

                        If Con.State = ConnectionState.Closed Then
                            Con.Open()
                        End If
                        cmd.ExecuteNonQuery()

                        nProblemId = 0
                        nProblemId = Convert.ToInt64(cmd.Parameters("@ProblemID").Value)
                        cmd.Parameters.Clear()
                        cmd.Dispose()
                        cmd = Nothing
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, "Problem List Added", gloAuditTrail.ActivityOutCome.Success)


                        If lst.ID = 0 Then
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Add, "Problem added", PatientID, nProblemId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                        Else
                            If lst.IsModified = True Then
                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Modify, "Problem modified", PatientID, lst.ID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                            End If
                            ''
                        End If
                        lst.ID = nProblemId
                    End If
                    '   End If
                Next
                trProbList.Commit()
                Return True
            Else
                If (dtRecProblist.Rows.Count > 0) Then
                    Dim cmd As SqlCommand = New SqlCommand("gsp_UpdateMedicationReconcillation", Con)
                    cmd.CommandType = CommandType.StoredProcedure


                    ExamParam = cmd.Parameters.Add("@nPatID", SqlDbType.BigInt)
                    ExamParam.Direction = ParameterDirection.Input
                    ExamParam.Value = PatientID
                    ExamParam = cmd.Parameters.Add("@nVisID", SqlDbType.BigInt)
                    ExamParam.Direction = ParameterDirection.Input
                    ExamParam.Value = VisitID
                    ExamParam = cmd.Parameters.Add("@nReconcillationType", SqlDbType.SmallInt)
                    ExamParam.Direction = ParameterDirection.Input
                    ExamParam.Value = 1
                    ExamParam = cmd.Parameters.Add("@Rx_MedicationReconcillation", SqlDbType.Structured) ''Medication Reconcillation for problem list( 2015 certification)
                    ExamParam.Direction = ParameterDirection.Input
                    ExamParam.Value = dtRecProblist
                    ExamParam = cmd.Parameters.Add("@nProviderID", SqlDbType.BigInt)
                    ExamParam.Direction = ParameterDirection.Input
                    ExamParam.Value = gloGlobal.gloPMGlobal.LoginProviderID
                    If Con.State = ConnectionState.Closed Then
                        Con.Open()
                    End If
                    cmd.ExecuteNonQuery()

                End If
                Return True
            End If
        Catch ex As SqlException

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If Not IsNothing(trProbList) Then
                trProbList.Rollback()
            End If
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If Not IsNothing(trProbList) Then
                trProbList.Rollback()
            End If
            Return False
        Finally
            If Not IsNothing(Con) Then
                If Con.State = ConnectionState.Open Then
                    Con.Close()

                    Con.Dispose()
                End If
            End If
            Con = Nothing
            If (IsNothing(trProbList) = False) Then
                trProbList.Dispose()
                trProbList = Nothing
            End If
            ExamParam = Nothing
        End Try

    End Function

    Public Function SaveExamICD9Snomed(ByVal PatientID As Int64, ByVal VisitID As Int64, ByVal ICD9Code As String, ByVal ICD9Description As String, ByVal SnomedCode As String, ByVal SnomedDesc As String, ByVal ExamID As Long, Optional ByVal nIcdRevision As Int16 = 9)

        Dim trDiagnosis As SqlTransaction = Nothing
        Dim Conn As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter

        Try


            Dim strQRY As String
            Dim _result As Object
            Dim ID As Int64 = 0

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            trDiagnosis = Conn.BeginTransaction

            If ICD9Code <> "" Or SnomedCode <> "" Then


                'strQRY = "SELECT nExamID FROM ExamICD9CPT WHERE nPatientID = " & PatientID & " AND (rtrim(ltrim(sICD9Description)) = '" + UCase(ICD9Description.Replace("'", "''")).ToString.Trim + "'" & " AND RTRIM(LTRIM(sICD9Code))='" & ICD9Code & "' OR rtrim(ltrim(sSnomedDesc)) = '" + UCase(SnomedDesc.Replace("'", "''")).ToString.Trim + "'" & " AND RTRIM(LTRIM(sSnomedCode))='" & SnomedCode & "') AND nExamID= '" & examid & "'  AND nVisitId=" & VisitID
                'cmd = New System.Data.SqlClient.SqlCommand(strQRY, Conn)

                '18-Jul-14 Aniket: Resolving Bug #71180: Problem No : 00000732
                cmd = New SqlCommand("gsp_ValidateSendToExam", Conn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Transaction = trDiagnosis

                sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = PatientID

                sqlParam = cmd.Parameters.Add("@ExamID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ExamID

                sqlParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = VisitID

                sqlParam = cmd.Parameters.Add("@ICDCode", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ICD9Code

                sqlParam = cmd.Parameters.Add("@ICDDescription", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = UCase(ICD9Description.Replace("'", "''")).ToString.Trim

                sqlParam = cmd.Parameters.Add("@SnoMedCode", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = SnomedCode

                sqlParam = cmd.Parameters.Add("@SnoMedDescription", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = UCase(SnomedDesc.Replace("'", "''")).ToString.Trim

                _result = cmd.ExecuteScalar()

                ID = Convert.ToInt64(_result)
            End If


            If ID = 0 Then
                If ICD9Code <> "" Then
                    ''''Update Exam name from template name to icd9 name if icd9 added.
                    strQRY = "SELECT Count(nExamID) FROM ExamICD9CPT WHERE nPatientID = " & PatientID & " AND nExamID= '" & examid & "'  AND nVisitId=" & VisitID
                    cmd = New System.Data.SqlClient.SqlCommand(strQRY, Conn)
                    cmd.Transaction = trDiagnosis
                    _result = cmd.ExecuteScalar()
                    cmd.Parameters.Clear()
                    cmd.Dispose()
                    cmd = Nothing
                    If _result = 0 Then
                        strQRY = "UPDATE patientexams SET sExamName= '" & ICD9Code + " " + ICD9Description & "' WHERE  nPatientID = " & PatientID & " AND nExamID= '" & examid & "'  AND nVisitId=" & VisitID
                        cmd = New System.Data.SqlClient.SqlCommand(strQRY, Conn)
                        cmd.Transaction = trDiagnosis
                        cmd.ExecuteNonQuery()
                        cmd.Parameters.Clear()
                        cmd.Dispose()
                        cmd = Nothing
                    End If
                End If



                cmd = New SqlCommand("gsp_InsertExamICD9CPTModifier", Conn)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Transaction = trDiagnosis


                sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = PatientID

                sqlParam = cmd.Parameters.Add("@nExamId", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = examid

                sqlParam = cmd.Parameters.Add("@nVisitId", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = VisitID

                sqlParam = cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ICD9Code

                sqlParam = cmd.Parameters.Add("@ICD9Desc", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ICD9Description

                sqlParam = cmd.Parameters.Add("@CPTcode", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@CPTDesc", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@ModCode", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@ModDesc", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ""

                sqlParam = cmd.Parameters.Add("@Unit", SqlDbType.Decimal)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = CType(1, Decimal)

                sqlParam = cmd.Parameters.Add("@LineNo", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam.Value = 1

                sqlParam = cmd.Parameters.Add("@SnomedCT", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = SnomedCode

                sqlParam = cmd.Parameters.Add("@SnomedDesc", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = SnomedDesc

                sqlParam = cmd.Parameters.Add("@nICDRevision", SqlDbType.SmallInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = nIcdRevision

                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
                MessageBox.Show("Dx Snomed sent to selected exam successfully.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else

                MessageBox.Show("Selected Dx Snomed already exists in the selected exam.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
                Exit Function
            End If
            trDiagnosis.Commit()


            Return True

        Catch ex As SqlException
            trDiagnosis.Rollback()
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'UpdateLog("ClsDiagnosisDBLayer -- FillProblemList -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- FillProblemList -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return False
        Catch ex As Exception
            trDiagnosis.Rollback()
            'UpdateLog("ClsDiagnosisDBLayer -- FillProblemList -- " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Diagnosis, gloAuditTrail.ActivityType.Add, "ClsDiagnosisDBLayer -- FillProblemList -- " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If (IsNothing(trDiagnosis) = False) Then
                trDiagnosis.Dispose()
                trDiagnosis = Nothing
            End If
            sqlParam = Nothing
        End Try


    End Function
    'Public Function SaveProblemList(ByVal ProblemID As Long, ByVal PatientID As Long, ByVal VisitID As Long, ByVal dtDOS As Date, ByVal ICD9Code As String, ByVal ICD9Desc As String, ByVal CheifComplaints As String, ByVal ProblemStatus As Integer) As Long
    '    Dim Con As New SqlConnection
    '    Try
    '        Con.ConnectionString = GetConnectionString()
    '        Dim cmd As New SqlCommand("gsp_InUpProblemList", Con)
    '        cmd.CommandType = CommandType.StoredProcedure
    '        Dim sqlParam As SqlParameter
    '        Dim ExamParam As SqlParameter
    '        '@ProblemID 	numeric(18,0) OutPut,
    '        '@PatientID 	numeric(18,0),
    '        '@VisitID 	    numeric(18,0),
    '        '@DOS 		    datetime,
    '        '@ICD9Code	    Varchar(50),	
    '        '@ICD9Desc	    Varchar(255),	
    '        '@CheifComplaint	Varchar(255),
    '        '@ProblemStatus	Int,
    '        '@MachineID     numeric(18,0),   

    '        ExamParam = cmd.Parameters.Add("@PatientID", PatientID)
    '        ExamParam.Direction = ParameterDirection.Input

    '        ExamParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
    '        ExamParam.Direction = ParameterDirection.Input
    '        ExamParam.Value = VisitID

    '        ExamParam = cmd.Parameters.Add("@DOS", SqlDbType.DateTime)
    '        ExamParam.Direction = ParameterDirection.Input
    '        ExamParam.Value = dtDOS

    '        ExamParam = cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar, 50)
    '        ExamParam.Direction = ParameterDirection.Input
    '        ExamParam.Value = ICD9Code

    '        ExamParam = cmd.Parameters.Add("@ICD9Desc", SqlDbType.VarChar, 255)
    '        ExamParam.Direction = ParameterDirection.Input
    '        ExamParam.Value = ICD9Desc

    '        ExamParam = cmd.Parameters.Add("@CheifComplaint", SqlDbType.VarChar, 255)
    '        ExamParam.Direction = ParameterDirection.Input
    '        ExamParam.Value = CheifComplaints

    '        ExamParam = cmd.Parameters.Add("@ProblemStatus", SqlDbType.Int)
    '        ExamParam.Direction = ParameterDirection.Input
    '        ExamParam.Value = ProblemStatus

    '        ExamParam = cmd.Parameters.Add("@MachineID", GetPrefixTransactionID)
    '        ExamParam.Direction = ParameterDirection.Input

    '        ExamParam = cmd.Parameters.Add("@ProblemID", ProblemID)
    '        ExamParam.Direction = ParameterDirection.InputOutput


    '        If Con.State = ConnectionState.Closed Then
    '            Con.Open()
    '        End If
    '        cmd.ExecuteNonQuery()

    '        Dim objAudit As New clsAudit
    '        If ProblemID = 0 Then
    '            objAudit.CreateLog(clsAudit.enmActivityType.Add, "'Problem List Added for date " & dtDOS & "'", gstrLoginName, gstrClientMachineName, gnPatientID)
    '        Else
    '            objAudit.CreateLog(clsAudit.enmActivityType.Modify, "'Problem List Modified for date " & dtDOS & "'", gstrLoginName, gstrClientMachineName, gnPatientID)
    '        End If
    '        objAudit = Nothing

    '        ProblemID = ExamParam.Value
    '        Return ProblemID

    '    Catch ex As SqlException
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return 0
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return 0
    '    Finally
    '        Con.Close()
    '    End Try
    'End Function

    Public Function UpdateProblemListDiagnosis(ByVal PatientID As Long, ByVal VisitID As Long, ByVal dtDOS As Date, ByVal ICD9Code As String, ByVal ICD9Desc As String, Optional ByVal ProblemID As Long = 0) As Boolean
        Dim Con As New SqlConnection
        Dim ExamParam As SqlParameter
        Try
            Con.ConnectionString = GetConnectionString()
            Dim cmd As New SqlCommand("gsp_UpdateProblemListDiagnosis", Con)
            cmd.CommandType = CommandType.StoredProcedure


            ExamParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            ExamParam.Direction = ParameterDirection.Input

            ExamParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = VisitID

            ExamParam = cmd.Parameters.Add("@DOS", SqlDbType.DateTime)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = dtDOS

            ExamParam = cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar, 50)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = ICD9Code

            ExamParam = cmd.Parameters.Add("@ICD9Desc", SqlDbType.VarChar, 255)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = ICD9Desc

            ExamParam = cmd.Parameters.Add("@ProblemID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = ProblemID

            '''' by Mahesh, 20070317
            ExamParam = cmd.Parameters.Add("@UserID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            Dim objMessages As New clsMessage
            ExamParam.Value = objMessages.GetUserID(gstrLoginName)
            objMessages.Dispose()
            objMessages = Nothing

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Modify, "'Problem List Diagnosis Modified for date " & dtDOS & "'", gstrLoginName, gstrClientMachineName, gnPatientID)
            'objAudit = Nothing

            Return True

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
            Con.Dispose()
            Con = Nothing
            ExamParam = Nothing
        End Try
    End Function

    '' By Mahesh 20070130
    Public Function UpdateProblemListPrescription(ByVal PatientID As Long, ByVal VisitID As Long, ByVal dtDOS As Date, ByVal Prescription As String, Optional ByVal ProblemID As Long = 0) As Boolean
        Dim Con As New SqlConnection
        Dim ExamParam As SqlParameter
        Try
            Con.ConnectionString = GetConnectionString()
            Dim cmd As New SqlCommand("gsp_UpdateProblemListPrescription", Con)
            cmd.CommandType = CommandType.StoredProcedure



            ExamParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            ExamParam.Direction = ParameterDirection.Input

            ExamParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = VisitID

            ExamParam = cmd.Parameters.Add("@DOS", SqlDbType.DateTime)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = dtDOS

            ExamParam = cmd.Parameters.Add("@Prescription", SqlDbType.VarChar, 255)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = Prescription

            ExamParam = cmd.Parameters.Add("@ProblemID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = ProblemID

            '''' by Mahesh, 20070317
            ExamParam = cmd.Parameters.Add("@UserID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            Dim objMessages As New clsMessage
            ExamParam.Value = objMessages.GetUserID(gstrLoginName)
            objMessages.Dispose()
            objMessages = Nothing


            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Modify, "'Problem List Prescription Modified for date " & dtDOS & "'", gstrLoginName, gstrClientMachineName, gnPatientID)
            'objAudit = Nothing
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, "'Problem List Prescription Modified for date " & dtDOS & "'", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101009
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.ProblemList, gloAuditTrail.ActivityType.Modify, "'Problem List Prescription Modified for date " & dtDOS & "'", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

            Return True

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
            Con.Dispose()
            Con = Nothing
            ExamParam = Nothing
        End Try
    End Function
    Public Function Fill_LockProblemList(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable
        Dim Conn As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim sqladpt As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Dim objParam As SqlParameter
        Try
            Conn = New SqlConnection
            Conn.ConnectionString = GetConnectionString()
            dt = New DataTable
            cmd = New System.Data.SqlClient.SqlCommand("gsp_Select_UnLock_Record", Conn)
            cmd.CommandType = CommandType.StoredProcedure


            objParam = cmd.Parameters.Add("@sMachinName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = MachinName

            objParam = cmd.Parameters.Add("@nTrnType", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = TransactionType

            objParam = cmd.Parameters.Add("@nMachinID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 0

            sqladpt = New SqlDataAdapter
            sqladpt.SelectCommand = cmd

            sqladpt.Fill(dt)

            Conn.Close()
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(Conn) Then
                Conn.Dispose()
                Conn = Nothing
            End If
            objParam = Nothing
        End Try
    End Function
    Public Function GetExamDetails(ByVal visitID As Int64, ByVal Code As String, ByVal Description As String, ByVal dtDOS As DateTime, ByVal PatientID As Long) As DataTable
        Dim ODB As New DataBaseLayer
        Dim dt As New DataTable
        Try
            'Line commented and modified by dipak 20100907 for case UC5070.003: replace gnPatientID by local variable .
            '        Dim strQry As String = " SELECT  PatientExams.nExamID, PatientExams.nVisitID, PatientExams.sExamName, PatientExams.dtDOS, PatientExams.bIsFinished, PatientExams.nPatientID " _
            '& " FROM         ExamICD9CPT INNER JOIN " _
            '& " PatientExams ON ExamICD9CPT.nExamID = PatientExams.nExamID  " _
            '& " WHERE ExamICD9CPT.nPatientID = " & gnPatientID & " AND RTRIM(LTRIM(ExamICD9CPT.sICD9Code)) = '" & Code & "' AND RTRIM(LTRIM(ExamICD9CPT.sICD9Description)) = '" & Description & "' AND PatientExams.dtDOS='" & dtDOS & "' order by PatientExams.nexamid desc"
            'Resolving Bug No . 71302::Problem List > Open Exam> It is showing message "No exam is associated for this diagnosis"
            Dim strQry As String = " SELECT Top 1 PatientExams.nExamID, PatientExams.nVisitID, ISNULL(PatientExams.sExamName,'') as sExamName, PatientExams.dtDOS,PatientExams.dtDischargeDate, PatientExams.bIsFinished, PatientExams.nPatientID,ISNULL(PatientExams.sTemplateName,'') as sTemplateName " _
            & " FROM         ExamICD9CPT INNER JOIN " _
            & " PatientExams ON ExamICD9CPT.nExamID = PatientExams.nExamID  " _
            & " WHERE ExamICD9CPT.nPatientID = " & PatientID & " AND RTRIM(LTRIM(ExamICD9CPT.sICD9Code)) = '" & Code & "' AND RTRIM(LTRIM(ExamICD9CPT.sICD9Description)) = '" & Description & "'  order by PatientExams.dtDOS desc"
            'end modification 
            '
            dt = ODB.GetDataTable_Query(strQry)
            Return dt
        Catch ex As Exception
            Return Nothing
        Finally
            ODB.Dispose()
            ODB = Nothing
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
        End Try
    End Function

    Public Function fillProvider() As DataTable
        Dim myDT As DataTable = Nothing
        Try
            Dim Str As String = String.Empty
            Str = " SELECT 0 as nProviderID,'' as ProviderName union SELECT nProviderID, sFirstName + ' ' + sMiddleName + ' ' + sLastName AS ProviderName FROM Provider_MST"
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)
            myDT = oDB.ReadQueryDataTable(Str)
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
            If Not IsNothing(myDT) Then
                Return myDT
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        Finally
            'If Not IsNothing(myDT) Then
            '    myDT.Dispose()
            '    myDT = Nothing
            'End If
        End Try

    End Function
    Public Function FillLocation() As DataTable
        Dim myDT As DataTable = Nothing
        Try
            Dim Str As String = String.Empty
            Str = "SELECT nCategoryID,ISNULL(sDescription,'') as sDescription FROM Category_MST WHERE sCategoryType = 'Location'"
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)
            myDT = oDB.ReadQueryDataTable(Str)
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
            If Not IsNothing(myDT) Then
                Return myDT
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        Finally
            'If Not IsNothing(myDT) Then
            '    myDT.Dispose()
            '    myDT = Nothing
            'End If
        End Try
    End Function
    Public Function InsertICD9(ByVal PatientID As Long, ByVal VisitID As Long, ByVal ArrList As ArrayList) As Boolean
        Dim Con As New SqlConnection(GetConnectionString)
        'Dim cmd As SqlCommand = Nothing
        Dim trProbList As SqlTransaction
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        trProbList = Con.BeginTransaction()
        Dim ExamParam As SqlParameter
        Try
            Dim lst As myList
            For i As Int16 = 0 To ArrList.Count - 1
                ' lst = New myList
                lst = CType(ArrList(i), myList)
                '  Dim sqlParam As SqlParameter

                If lst.Code <> "" Then


                    If lst.Value <> "5" Then
                        '' Insert Or Update problem List
                        Dim cmd As SqlCommand = New SqlCommand("gsp_InsertICD9", Con)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Transaction = trProbList

                        ExamParam = cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar, 50)
                        ExamParam.Direction = ParameterDirection.Input
                        ExamParam.Value = lst.Code

                        ExamParam = cmd.Parameters.Add("@Description", SqlDbType.VarChar, 255)
                        ExamParam.Direction = ParameterDirection.Input
                        ExamParam.Value = lst.Description

                        ExamParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
                        ExamParam.Direction = ParameterDirection.Input

                        ExamParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
                        ExamParam.Direction = ParameterDirection.Input
                        If Val(lst.HistoryItem) > 0 Then
                            ExamParam.Value = CLng(lst.HistoryItem)
                        Else
                            ExamParam.Value = 0
                        End If

                        ExamParam = cmd.Parameters.Add("@SnomedCode", SqlDbType.VarChar, 50)
                        ExamParam.Direction = ParameterDirection.Input
                        If lst.ConceptId <> Nothing AndAlso lst.ConceptId.ToString().Trim() <> "" Then
                            ExamParam.Value = lst.ConceptId
                        Else
                            ExamParam.Value = ""
                        End If

                        ExamParam = cmd.Parameters.Add("@SnomedDesc", SqlDbType.VarChar, 250)
                        ExamParam.Direction = ParameterDirection.Input
                        If lst.AssociatedProperty <> Nothing AndAlso lst.AssociatedProperty.ToString().Trim() <> "" Then
                            ExamParam.Value = lst.AssociatedProperty
                        Else
                            ExamParam.Value = ""
                        End If
                        If Con.State = ConnectionState.Closed Then
                            Con.Open()
                        End If
                        cmd.ExecuteNonQuery()
                        cmd.Parameters.Clear()
                        cmd.Dispose()
                        cmd = Nothing
                        lst = Nothing
                        gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "ICD9  Added", gloAuditTrail.ActivityOutCome.Success)

                    End If
                End If
            Next
            trProbList.Commit()
            Return True
        Catch ex As SqlException

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            trProbList.Rollback()
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ProblemList, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            trProbList.Rollback()
            Return False
        Finally
            Con.Close()
            Con.Dispose()
            Con = Nothing
            If Not IsNothing(trProbList) Then
                trProbList.Dispose()
                trProbList = Nothing
            End If
            ExamParam = Nothing
        End Try
    End Function

    Public Function GetExamName(ByVal nExamID As Long) As DataTable
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Dim oResult As DataTable = Nothing
        Try

            strSQL = "SELECT  ISNULL(PatientExams.nExamID,0) as nExamID, ISNULL(PatientExams.nVisitID,0) as nVisitID ,ISNULL(PatientExams.sExamName,'') as sExamName,ISNULL(PatientExams.sTemplateName,'') as sTemplateName ,ISNULL(PatientExams.dtDOS,'') as dtDOS ,ISNULL(PatientExams.dtDischargeDate,'') as dtDischargeDate,ISNULL(PatientExams.bIsFinished,0) as bIsFinished FROM PatientExams WHERE nExamID = " & nExamID & ""
            oResult = oDB.GetDataTable_Query(strSQL)
            If Not oResult Is Nothing Then
                Return oResult
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
            'If Not IsNothing(oResult) Then
            '    oResult.Dispose()
            '    oResult = Nothing
            'End If
        End Try
    End Function

    Public Function GetExamCount(ByVal nExamID As Long) As String
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Dim oResult As String
        Try

            strSQL = "SELECT Count(*) From PatientExams where nExamID = " & nExamID & ""
            oResult = oDB.GetRecord_Query(strSQL)
            If Not oResult Is Nothing Then
                Return oResult
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        Finally
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function
   
    Public Function Get_PatientProblemList(ByVal PatientID As Long, Optional ByVal VisitDate As Object = Nothing) As DataTable
        Dim objCon As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Dim ExamParam As SqlParameter
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_GetPatientProblemList", objCon)
            cmd.CommandType = CommandType.StoredProcedure

            ExamParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = PatientID

            If IsDate(VisitDate) Then
                ExamParam = cmd.Parameters.Add("@VisitDate", SqlDbType.Date)
                ExamParam.Direction = ParameterDirection.Input
                ExamParam.Value = VisitDate
            End If

            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            objCon.Close()
            Return dt

        Catch ex As Exception
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            ExamParam = Nothing
        End Try
    End Function
    Public Function Get_CDACodeSystemtype(ByVal codesystemtype As String) As DataTable
        Dim objCon As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Dim ExamParam As SqlParameter
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_GetCDACodeSystemtype", objCon)
            cmd.CommandType = CommandType.StoredProcedure


            ExamParam = cmd.Parameters.Add("@codesystemtype", SqlDbType.VarChar)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = codesystemtype


            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            objCon.Close()
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            ExamParam = Nothing
        End Try
    End Function
    Public Sub New()

    End Sub
    Private disposed As Boolean = False

    Public Sub Dispose() Implements System.IDisposable.Dispose
        'Disconnect();
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
            End If
        End If
        disposed = True
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
