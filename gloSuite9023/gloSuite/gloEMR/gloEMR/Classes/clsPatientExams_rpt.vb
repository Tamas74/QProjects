Imports System.Data.SqlClient
Imports ADODB.StreamClass
Imports System.IO

Public Class clsPatientExams_rpt
    Implements IDisposable
    'Private da As SqlDataAdapter
    'Private ds As New DataSet
    'Private dt As DataTable
    'Private dv As DataView
    Private Con As SqlConnection = Nothing
    ' Private conString As String



    Public Function Fill_AllExams(ByVal PatientID As Long, ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, Optional ByVal IsFinished As Int16 = 2, Optional ByVal TypeWise As Int16 = 0) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        'Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillAllExams_PrintFAX"
        objCmd.Connection = objCon

        Dim objParaPatientID As New SqlParameter
        With objParaPatientID
            .ParameterName = "@PatientID"
            .Value = PatientID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaPatientID)

        Dim objParaFrom As New SqlParameter
        With objParaFrom
            .ParameterName = "@FromDate"
            .Value = dtFromDate.Date
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objParaFrom)

        Dim objParaTo As New SqlParameter
        With objParaTo
            .ParameterName = "@ToDate"
            .Value = dtToDate.Date
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objParaTo)

        Dim objParaFinished As New SqlParameter
        With objParaFinished
            .ParameterName = "@Finished"
            .Value = IsFinished
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaFinished)

        Dim objParaFlag As New SqlParameter
        With objParaFlag
            .ParameterName = "@Flag"
            '' 0=Datewise ||||  1=Typewise (Finish/UnFinish)  |||| 2 = All Exams of Patient
            .Value = TypeWise
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaFlag)

        objCmd.Connection = objCon
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)

        Dim dt As New DataTable
        '' Add One Column to Select 
        Dim clmnSelect As New DataColumn
        With clmnSelect
            .ColumnName = "Select"
            .DataType = System.Type.GetType("System.Boolean")
            .DefaultValue = CBool("False")
        End With
        dt.Columns.Add(clmnSelect)
        ' "Select", System.Type.GetType("System.Boolean"))
        objDA.Fill(dt)
        '' Select, ExamID, DOS, ExamName, VisitID, DOB, ProviderID, ProviderName, bIsFinished
        objCon.Close()
        objCon.Dispose()
        objCon = Nothing
        objDA.Dispose()
        objDA = Nothing
        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If

        objParaFlag = Nothing
        objParaFinished = Nothing
        objParaFrom = Nothing
        objParaPatientID = Nothing
        objParaTo = Nothing
        clmnSelect = Nothing

        Return dt
    End Function


    Public Function GetPastExams(ByVal ExamId As Long) As DataSet
        Dim Cmd As New SqlCommand
        Dim sParam As New SqlParameter
        Con = New SqlConnection(GetConnectionString)

        'Dim objSQLDataReader As SqlDataReader
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.CommandText = "gsp_GetPastExamContents"
        Cmd.Connection = Con

        Cmd.Parameters.Clear()
        sParam.ParameterName = "@ExamID"
        sParam.Direction = ParameterDirection.Input
        sParam.SqlDbType = SqlDbType.BigInt
        sParam.Value = ExamId
        Cmd.Parameters.Add(sParam)


        Dim da As New SqlDataAdapter(Cmd)
        Dim dsData As New DataSet
        da.Fill(dsData)
        If Cmd IsNot Nothing Then
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
        End If
        Con.Dispose()
        Con = Nothing
        da.Dispose()
        da = Nothing
        sParam = Nothing
        Return dsData
    End Function

    Public Function Fill_AllExams_AllPatients(ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, Optional ByVal IsFinished As Int16 = 2, Optional ByVal TypeWise As Int16 = 0, Optional ByVal Gender As String = "") As DataTable
        '' For PrintAll- FaxAll Report of All Patients
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        'Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillAllPatients_Exams_PrintFAX"
        objCmd.Connection = objCon

        Dim objParaFrom As New SqlParameter
        With objParaFrom
            .ParameterName = "@FromDate"
            .Value = dtFromDate.Date
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objParaFrom)

        Dim objParaTo As New SqlParameter
        With objParaTo
            .ParameterName = "@ToDate"
            .Value = dtToDate.Date
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.DateTime
        End With
        objCmd.Parameters.Add(objParaTo)

        Dim objParaFinished As New SqlParameter
        With objParaFinished
            .ParameterName = "@Finished"
            .Value = IsFinished
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaFinished)

        Dim objParaFlag As New SqlParameter
        With objParaFlag
            .ParameterName = "@Flag"
            '' 0=Datewise ||||  1=Typewise (Finish/UnFinish)  |||| 2 = All Exams of Patient
            .Value = TypeWise
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaFlag)

        '''''''' modified by Bipin on 20070503 for CCHIT 2007
        'Dim objParaGender As New SqlParameter
        'With objParaGender
        '    .ParameterName = "@Gender"
        '    .Value = Gender
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.VarChar
        'End With
        'objCmd.Parameters.Add(objParaGender)
        '''''''''

        objCmd.Connection = objCon
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)

        Dim dt As New DataTable
        Dim clmnSelect As New DataColumn
        With clmnSelect
            .ColumnName = "Select"
            .DataType = System.Type.GetType("System.Boolean")
            .DefaultValue = CBool("False")
        End With
        dt.Columns.Add(clmnSelect)
        '' Add One Column to Select 
        'dt.Columns.Add("Select", System.Type.GetType("System.Boolean"))

        objDA.Fill(dt)
        '' Select, ExamID, PatientName , ExamName, DOS, VisitID, PatientID, PatientCode, DOB, ProviderID, ProviderName, bIsFinished 
        objCon.Close()
        objCon.Dispose()
        objCon = Nothing
        objDA.Dispose()
        objDA = Nothing
        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If

        objParaFlag = Nothing
        objParaFinished = Nothing
        objParaTo = Nothing
        objParaFrom = Nothing
        clmnSelect = Nothing
        Return dt
    End Function

    '05-Nov-14 Aniekt: Bug #75629: gloEMR: Reports-ExamPrint fax - Application is showing an exception
    Public Function Fill_AllExams_AllPatients1(ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, ByVal PatientID As Long, Optional ByVal IsFinished As Int16 = 2, Optional ByVal TypeWise As Int16 = 0, Optional ByVal Gender As String = "", Optional ByVal AgeType As Integer = 0, Optional ByVal AgeFrom As Integer = 0, Optional ByVal AgeTo As Integer = 0, Optional ByVal strMedication As String = "", Optional ByVal strFinishStatus As String = "", Optional ByVal ProviderID As Int64 = 0, Optional ByVal strDiagnosisICD9 As String = "", Optional ByVal bAllPatient As Boolean = False) As DataTable
        'Parameter "ByVal PatientID As Long" added by dipak for 20100826 for case UC5070.003 
        'Dim sAgeFrom = AgeType.ToString
        'Dim sAgeTo = AgeTo.ToString
        'dtFromDate, dtToDate, IsFinished, TypeWise, Gender, AgeType, AgeFrom, AgeTo, strMedication

        If strMedication = "" AndAlso IsDBNull(dtFromDate) Then ' this is the temparory condition is added to avoid IF loop which is previous logic '' Bipin 20070517

            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            'Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_FillAllPatients_Exams_PrintFAX"
            objCmd.Connection = objCon

            Dim objParaFrom As New SqlParameter
            With objParaFrom
                .ParameterName = "@FromDate"
                .Value = dtFromDate.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFrom)

            Dim objParaTo As New SqlParameter
            With objParaTo
                .ParameterName = "@ToDate"
                .Value = dtToDate.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaTo)

            Dim objParaFinished As New SqlParameter
            With objParaFinished
                .ParameterName = "@Finished"
                .Value = IsFinished
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaFinished)

            Dim objParaFlag As New SqlParameter
            With objParaFlag
                .ParameterName = "@Flag"
                '' 0=Datewise ||||  1=Typewise (Finish/UnFinish)  |||| 2 = All Exams of Patient
                .Value = TypeWise
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaFlag)

            '''''''' modified by Bipin on 20070503 for CCHIT 2007
            Dim objParaGender As New SqlParameter
            With objParaGender
                .ParameterName = "@Gender"
                .Value = Gender
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaGender)

            '' parameter for the AgeType
            Dim objParaAgeType As New SqlParameter
            With objParaAgeType
                .ParameterName = "@AgeType"
                .Value = AgeType
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaAgeType)

            '' parameter for the From Age
            Dim objParaAgeFrom As New SqlParameter
            With objParaAgeFrom
                .ParameterName = "@AgeFrom"
                .Value = AgeFrom
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaAgeFrom)

            '' parameter for the To Age
            Dim objParaAgeTo As New SqlParameter
            With objParaAgeTo
                .ParameterName = "@AgeTo"
                .Value = AgeTo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaAgeTo)

            '' for medication 
            Dim objParaMedication As New SqlParameter
            With objParaMedication
                .ParameterName = "@sMedication"
                .Value = strMedication
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaMedication)
            '''''''''
            Dim objDA As New SqlDataAdapter(objCmd)

            Dim dt As New DataTable
            Dim clmnSelect As New DataColumn
            With clmnSelect
                .ColumnName = "Select"
                .DataType = System.Type.GetType("System.Boolean")
                .DefaultValue = CBool("False")
            End With
            dt.Columns.Add(clmnSelect)

            objDA.Fill(dt)
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing
            objDA.Dispose()
            objDA = Nothing
            objParaGender = Nothing
            objParaMedication = Nothing
            objParaAgeType = Nothing
            objParaAgeTo = Nothing
            objParaFlag = Nothing
            objParaAgeFrom = Nothing
            objParaFinished = Nothing
            objParaTo = Nothing
            objParaFrom = Nothing

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            clmnSelect = Nothing
            Return dt
        Else
            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim dt As DataTable = Nothing
            Dim dateConditions As String = ""

            Dim _strSQL As String = ""

            If AgeType = 0 Then
                dateConditions = " between 0 and 124 "
            ElseIf AgeType = 1 Then
                dateConditions = " = '" & AgeFrom & "' "
            ElseIf AgeType = 2 Then
                dateConditions = " < '" & AgeFrom & "' "
            ElseIf AgeType = 3 Then
                dateConditions = " > '" & AgeFrom & "' "
            ElseIf AgeType = 4 Then
                dateConditions = " between '" & AgeFrom & "' and '" & AgeTo & "' "
            End If

            If Gender = "All" Or Gender = "" Then
                Gender = " 'Male','Female','Other' "
            Else
                Gender = "'" & Gender & "'"
            End If
            ' Bug #43173: EMR-Exam-Application is saving the Exam with New exam template name
            ' sPatientNotes IS NOT NULL " condition added to where clause to avoid the issue
            _strSQL = " SELECT DISTINCT  0, " _
                    & " PatientExams.nExamID, " _
                    & " isnull(Patient.sFirstName,'')+SPACE(2)+isnull(Patient.sLastName,'') as PatientName, " _
                    & " PatientExams.sExamName, " _
                    & " PatientExams.dtDOS, " _
                    & " Visits.nVisitID, " _
                    & " Patient.nPatientID, " _
                    & " Patient.sPatientCode, " _
                    & " Patient.dtDOB, " _
                    & " Provider_MST.nProviderID, " _
                    & " isnull(Provider_MST.sFirstName,'')+SPACE(1)+isnull(Provider_MST.sMiddleName,'')+SPACE(1)+isnull(Provider_MST.sLastName,'') AS ProviderName, " _
                    & " PatientExams.bIsFinished, " _
                    & " DATEDIFF(yy, Patient.dtDOB, dbo.gloGetDate()) AS Age, " _
                    & " Patient.sGender " _
                    & " FROM PatientExams " _
                    & " INNER JOIN Patient ON PatientExams.nPatientID = Patient.nPatientID " _
                    & " INNER JOIN Provider_MST ON Patient.nProviderID = Provider_MST.nProviderID " _
                    & " INNER JOIN Visits ON PatientExams.nVisitID = Visits.nVisitID " _
                    & " LEFT OUTER JOIN Medication ON PatientExams.nPatientID = Medication.nPatientID " _
                    & " LEFT OUTER JOIN ExamICD9CPT ON PatientExams.nExamID = ExamICD9CPT.nExamID " _
                    & " WHERE (DateDiff(yy, dtdob, dbo.gloGetDate())) " & dateConditions '& " AND PatientExams.sPatientNotes IS NOT NULL "

            ' check for selected Medication
            If strMedication <> "" Then
                _strSQL = _strSQL & " AND medication.sMedication + ' ' + isnull(sDosage,'') in ( " & strMedication & " ) "
            Else
                _strSQL = _strSQL
            End If

            If strDiagnosisICD9 <> "" Then
                _strSQL = _strSQL & " and isnull(ExamICD9CPT.sICD9Code,'')+Space(1)+isnull(ExamICD9CPT.sICD9Description,'') in ( '" & strDiagnosisICD9.Replace("'", "''") & "' ) "             '''''''& strDiagnosisICD9 & " "
            Else
                _strSQL = _strSQL
            End If
            _strSQL = _strSQL & " AND dtDOS between '" & dtFromDate & "' and '" & dtToDate & "' "

            If bAllPatient = True Then
                _strSQL = _strSQL
                If Gender = "All" Then
                    _strSQL = _strSQL
                Else
                    _strSQL = _strSQL & " AND Patient.sGender in ( " & Gender & " )  "
                End If

                If ProviderID = 0 Then
                    _strSQL = _strSQL
                Else
                    '_strSQL = _strSQL & " AND (ISNULL(Provider_MST.sFirstName,'')+ SPACE(1) + CASE ISNULL(Provider_MST.sMiddleName,'') WHEN  '' THEN '' When Provider_MST.sMiddleName then  Provider_MST.sMiddleName + SPACE(1) END +ISNULL(Provider_MST.sLastName,'')) = '" & strProvidersName.Trim().Replace("'", "''") & "' "
                    _strSQL = _strSQL & " AND (Provider_MST.nProviderID = " & ProviderID & ")"
                End If

                If strFinishStatus = "All" Then
                    _strSQL = _strSQL
                ElseIf strFinishStatus = "Finished" Then
                    _strSQL = _strSQL & " and bIsFinished = " & 1 & "  "
                ElseIf strFinishStatus = "Unfinished" Then
                    _strSQL = _strSQL & " and bIsFinished = " & 0 & "  "
                End If
            Else
                If strFinishStatus = "All" Then
                    _strSQL = _strSQL
                ElseIf strFinishStatus = "Finished" Then
                    _strSQL = _strSQL & " and bIsFinished = " & 1 & "  "
                ElseIf strFinishStatus = "Unfinished" Then
                    _strSQL = _strSQL & " and bIsFinished = " & 0 & "  "
                End If
                _strSQL = _strSQL & " and patient.npatientid = '" & PatientID & "' "
            End If
            _strSQL = _strSQL & "  ORDER BY dtDOS  Desc,PatientName " ''Sandip Darade 20100118 Bug ID 5697

            odb.Connect(GetConnectionString)
            dt = odb.ReadQueryDataTable(_strSQL)

            Dim clmnSelect As New DataColumn
            With clmnSelect
                .ColumnName = "Select"
                .DataType = System.Type.GetType("System.Boolean")
                .DefaultValue = CBool("False")
            End With
            dt.Columns.Add(clmnSelect)
            clmnSelect = Nothing
            odb.Disconnect()
            odb.Dispose()
            odb = Nothing
            Return dt
        End If
    End Function


    Public Function GetExamContents(ByVal ExamId As Long) As Object
        Dim Cmd As New SqlCommand
        Dim sParam As New SqlParameter
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.CommandText = "gsp_GetPastExamContents"
        Cmd.Parameters.Clear()
        sParam.ParameterName = "@ExamID"
        sParam.Value = ExamId
        Cmd.Parameters.Add(sParam)
        Cmd.Connection = Con
        Dim objExamContents As Object
        Con.Open()
        objExamContents = Cmd.ExecuteScalar()
        Con.Close()
        'Con.Dispose()
        'Con = Nothing
        If Cmd IsNot Nothing Then
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
        End If
        sParam = Nothing
        Return objExamContents
    End Function

    Public Sub New()
        Try
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
        Catch ex As SqlException ' Catch the error.
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        End Try
    End Sub
#Region "IDisposable Support"
    Private disposedValue As Boolean = False ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                If IsNothing(Con) = False Then
                    If Con.State = ConnectionState.Open Then ''Slr
                        Con.Close()
                    End If
                    Con.Dispose() : Con = Nothing
                End If
            End If


            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
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
