Imports System.Data.SqlClient
Imports ADODB.StreamClass
Imports System.IO
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEMR.gloEMRWord

Public Class clsPatientExams
    Implements IDisposable
    'Private da As SqlDataAdapter
    'Private dt As DataTable
    'Private dv As DataView
    Private Con As SqlConnection
    'Private conString As String

    Public Sub New()
        Try
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
        Catch ex As SqlException ' Catch the error.
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        End Try
    End Sub


    Public Function GetPatientExam(ByVal PatientID As Int64) As DataTable
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Dim oResult As DataTable
        Try

        'Dim ProviderId As Long
        strSQL = "SELECT ISNULL(PatientExams.nExamID,0) as ExamID,ISNULL(PatientExams.nVisitID, 0) AS VisitID, ISNULL(PatientExams.nPatientID, 0) AS PatientID, ISNULL(PatientExams.sExamName, '') AS ExamName, " _
                & "ISNULL(PatientExams.bIsFinished, 'false') AS IsFinished, ISNULL(PatientExams.dtDOS, dbo.gloGetDate()) AS DOS, ISNULL(PatientExams.sUserName, '') " _
                & "AS UserName, ISNULL(PatientExams.sMachineName, '') AS MachineName, ISNULL(PatientExams.nProviderID, 0) AS ProviderID, " _
                & "ISNULL(PatientExams.nReviewerId, 0) AS ReviewerID, ISNULL(PatientExams.dtReviewDate, dbo.gloGetDate()) AS ReviewDate, " _
                & "ISNULL(PatientExams.bIsReviewed, 'false') AS IsReviewed, (ISNULL(ExamICD9CPT.sICD9Code,'') + '-' + isnull(ExamICD9CPT.sICD9Description,'')) As ICD9Code,(isnull(Provider_MST.sFirstName,'')+ " _
                & " +' '+ isnull(Provider_MST.sMiddleName,'')+' '+isnull(Provider_MST.sLastName,'')) As ProviderName,ISNULL(sTemplateName,'') as sTemplateName " _
                & " FROM PatientExams INNER JOIN " _
                & " ExamICD9CPT ON PatientExams.nExamID = ExamICD9CPT.nExamID   INNER JOIN Provider_mst on Provider_mst.nProviderID=PatientExams.nProviderID " _
                & " WHERE PatientExams.nPatientID = " & PatientID & " order by dos"
        oResult = oDB.GetDataTable_Query(strSQL)
            Return oResult
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            'If Not IsNothing(oResult) Then  'Obj Disposed by Mitesh  'SLR: don't dispose since it is returned
            '    oResult.Dispose()
            '    oResult = Nothing
            'End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function


    Public Sub Record_PatientExamHx(ByVal nExamID As Long, ByVal strExamName As String, ByVal dtModiDt As DateTime, ByVal strComment As String, ByVal strEvent As String)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Try
            cmd = New SqlCommand("Insert_PatientExamHx", Con)
            cmd.CommandType = CommandType.StoredProcedure            
            If Con.State = ConnectionState.Closed Then Con.Open()


            sqlParam = cmd.Parameters.Add("@ExamID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nExamID


            sqlParam = cmd.Parameters.Add("@ExamName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = strExamName

            sqlParam = cmd.Parameters.Add("@dtModiDt", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = dtModiDt

            sqlParam = cmd.Parameters.Add("@UserID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = gnLoginID

            sqlParam = cmd.Parameters.Add("@UserName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = gstrLoginName


            sqlParam = cmd.Parameters.Add("@Comments", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = strComment

            sqlParam = cmd.Parameters.Add("@MachineNm", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = gstrClientMachineName

            sqlParam = cmd.Parameters.Add("@Event", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = strEvent

            cmd.ExecuteNonQuery()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Record Patient Exam History", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If

            If Not IsNothing(cmd) Then  'Obj Disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try
    End Sub

    Public Function Fill_PatientExamHx(ByVal nExamID As Long) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter

        Dim oResultTable As DataTable = Nothing  ''SLR: new is not needed
        Try

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nExamID"
            oParamater.Value = nExamID
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            oResultTable = oDB.GetDataTable("GetPatientExamHx")
            Return oResultTable
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Template Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            ' If IsNothing(oResultTable) = False Then 'SLR: Don't dispose since it is returned
            'oResultTable.Dispose()
            'oResultTable = Nothing
            'End If
            oDB.Dispose() 'SLR: and then make nothing
            oDB = Nothing
        End Try
    End Function

    'Public Function Fill_PatientExamHx(ByVal nExamid As Long) As DataTable
    '    'To maintain Patient Exam History Record - as on 10/02/2010
    '    Dim objCon As New SqlConnection
    '    objCon.ConnectionString = GetConnectionString()
    '    Dim sQry As String

    '    sQry = "select nExamID [Exam ID],sExamName [Exam Name],dtModifieddateTime [Modified DateTime],p.nUserID [User ID]," _
    '    & " sUserName [User Name],sComments [Comments],sMachineName [Machine Name],sEventName [Event Name]," _
    '    & " isnull((case u.nproviderid when 0 then u.sFirstName + ' '+ u.sMiddleName + ' ' + u.sLastName" _
    '    & " else (case pv.sSuffix when '' then pv.sFirstName + ' '+ pv.sMiddleName + ' ' + pv.sLastName " _
    '    & " else pv.sFirstName + ' '+ pv.sMiddleName +' ' + pv.sLastName + ','+ pv.sSuffix end ) end ),'') [Name]" _
    '    & " from PatientExamHistory p inner join User_MST u on p.nUserID=u.nUserID" _
    '    & " left outer join provider_mst pv on pv.nproviderid=u.nproviderid" _
    '    & " where nExamID= " & nExamid & " order by dtModifieddateTime desc"

    '    Dim cmd As New SqlCommand(sQry, objCon)
    '    cmd.CommandType = CommandType.Text

    '    Dim da As SqlDataAdapter
    '    Dim dt As DataTable
    '    objCon.Open()
    '    da = New SqlDataAdapter
    '    da.SelectCommand = cmd
    '    dt = New DataTable
    '    da.Fill(dt)
    '    objCon.Close()
    '    Return dt
    '    If Not IsNothing(dt) Then  'Obj Disposed by Mitesh
    '        dt.Dispose()
    '        dt = Nothing
    '    End If
    '    If Not IsNothing(da) Then
    '        da.Dispose()
    '        da = Nothing
    '    End If
    '    If Not IsNothing(cmd) Then
    '        cmd.Dispose()
    '        cmd = Nothing
    '    End If
    '    If Not IsNothing(objCon) Then
    '        objCon.Dispose()
    '        objCon = Nothing
    '    End If
    'End Function

    Public Sub UpdateExamProvider(ByRef _ExamID As Long, ByVal _ProviderId As Long)

        Dim oDB As New DataBaseLayer
        Dim oParameter As DBParameter = Nothing
        Try
            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@ExamID"
            oParameter.Value = _ExamID
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@ProviderID"
            oParameter.Value = _ProviderId
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oDB.Add("UpdateExamProvider")

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                oParameter = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Sub

    Public Function GetProviderIdforExam(ByVal Examid As Long) As Long

        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Dim oResult As Object
        Dim ProviderId As Long = 0
        strSQL = "select nProviderId from PatientExams where nExamid = " & Examid
        oResult = oDB.GetRecord_Query(strSQL)

        If oResult IsNot Nothing Then
            If oResult.ToString() <> "" Then
                ProviderId = CType(oResult, Int64)
            End If
        End If

        If ProviderId > 0 Then
            Return ProviderId
        Else
            Return 0
        End If
        If Not IsNothing(oResult) Then  'Obj Disposed by Mitesh
            oResult = Nothing
        End If
        If Not IsNothing(oDB) Then
            oDB.Dispose()
            oDB = Nothing
        End If
    End Function

    Public Function GetProviderIdforPatient(ByVal PatientID As Long) As Long
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Dim oResult As Object
        Dim ProviderId As Long = 0
        strSQL = "select nProviderId from Patient where nPatientid  = " & PatientID
        oResult = oDB.GetRecord_Query(strSQL)

        If oResult IsNot Nothing Then
            If oResult.ToString() <> "" Then
                ProviderId = CType(oResult, Int64)
            End If
        End If

        If ProviderId > 0 Then
            Return ProviderId
        Else
            Return 0
        End If
        If Not IsNothing(oResult) Then  'Obj Disposed by Mitesh
            oResult = Nothing
        End If
        If Not IsNothing(oDB) Then
            oDB.Dispose()
            oDB = Nothing
        End If
    End Function

    Public Function GetProviderfromLogin(ByVal LoginId As Long) As Int64
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Dim ProviderId As Long
        strSQL = "select nProviderId from User_MST where nUserId = " & LoginId
        ProviderId = oDB.GetRecord_Query(strSQL)
        If Not IsDBNull(ProviderId) Then
            Return ProviderId
        Else
            Return 0
        End If
       
        If Not IsNothing(oDB) Then  'Obj Disposed by Mitesh
            oDB.Dispose()
            oDB = Nothing
        End If
    End Function

    Public Function GetReviewerDetails(ByVal Examid As Int64) As DataTable
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Dim oResult As DataTable = Nothing  'SLR: no new is needed
        Try
            strSQL = "select bIsReviewed,nReviewerID,dtReviewDate from PatientExams where nExamid = " & Examid
            oResult = oDB.GetDataTable_Query(strSQL)
            If Not oResult Is Nothing Then
                Return oResult
            Else
                Return Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            'If Not IsNothing(oResult) Then  'Obj Disposed by Mitesh 'SLR: Don't dispose since it is returned
            '    oResult.Dispose()
            '    oResult = Nothing
            'End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function GetSeniorProviderIdforExam(ByVal Examid As Int64) As DataTable
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Dim oResult As DataTable = Nothing 'SLR: no new is needed
        Try
            strSQL = "SELECT distinct ProviderSettings.nOthersID FROM ProviderSettings  INNER JOIN PatientExams ON PatientExams.nProviderID = ProviderSettings.nProviderID and ProviderSettings.sSettingstype='ProviderSeniorAssignment' AND PatientExams.nExamid = " & Examid
            oResult = oDB.GetDataTable_Query(strSQL)
            If Not oResult Is Nothing Then
                If oResult.Rows.Count > 0 Then
                    Return oResult
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            'If Not IsNothing(oResult) Then  'Obj Disposed by Mitesh 'SLR: Don't dispose since it is returned
            '    oResult.Dispose()
            '    oResult = Nothing
            'End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function GetProvidernameforExam(ByVal ExamProviderId As Long) As String
        Dim oDB As New DataBaseLayer
        Dim oParameter As New DBParameter
        Dim strProviderName As String
        Try
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@ProviderID"
            oParameter.Value = ExamProviderId
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing
            strProviderName = oDB.GetDataValue("gsp_RetrieveProviderName")

            If Not IsDBNull(strProviderName) Then
                Return strProviderName
            Else
                Return ""
            End If
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return ""
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return ""
        Finally
            If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                oParameter = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function GetExamProvider(ByVal ExamID As Long) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParameter As New DBParameter
        ' Dim strProviderName As String
        Dim dt As DataTable = Nothing   'SLR: new is not neeeded
        Try
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@ExamID"
            oParameter.Value = ExamID
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing
            ' strProviderName = oDB.GetDataValue("gsp_RetrieveProviderName")
            dt = oDB.GetDataTable("gsp_GetExamProvider")
            Return dt
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                oParameter = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            ' If IsNothing(dt) = False Then   'SLR: don't dispose since dt is returned
            'dt.Dispose()
            ' dt = Nothing
            'End If
        End Try
    End Function
    Public Function SetProviderExam(ByVal nPastExamID As Long) As Boolean
        Dim clsExam As New clsPatientExams
        Dim ExamProviderId As Long

        'If ExamProviderId <> 0 Then
        '    strProviderName = GetProvidernameforExam(ExamProviderId)
        'Else
        '    strProviderName = ""
        'End If

        ' ''21-03-2012 Start '' 00000567 'Provider cannot able to see the Tags against it created Exams'
        'gnSelectedProviderID = gnLoginProviderID
        ' ''21-03-2012 End '' 00000567 'Provider cannot able to see the Tags against it created Exams'

        If gnLoginProviderID <> 0 Then
            Dim dt As DataTable
            'ExamProviderId = GetProviderIdforExam(nPastExamID)
            Dim strProviderName As String = ""
            dt = GetExamProvider(nPastExamID)
            If IsNothing(dt) = False Then
                If dt.Rows.Count > 0 Then
                    ExamProviderId = dt.Rows(0)("nProviderID")
                    strProviderName = dt.Rows(0)("ProviderName")
                End If
                dt.Dispose()  ''SLR: Free dt
                dt = Nothing


            End If
            If gnLoginProviderID = ExamProviderId Then
                If Not IsNothing(clsExam) Then  'Obj Disposed by Mitesh
                    clsExam.Dispose()
                    clsExam = Nothing
                End If

                Return False
            Else
                ''Prompt User whether to associate the exam to him
                Dim dialogResult As DialogResult
                dialogResult = MessageBox.Show("This Exam is documented by '" & Trim(strProviderName) & "'." & vbCr & vbCr & "Do you want to change the Exam Provider?" & vbCr & vbCr & "[This can also be done from the Patient Exam screen].", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If dialogResult = Windows.Forms.DialogResult.Yes Then
                    UpdateExamProvider(nPastExamID, gnLoginProviderID)

                Else
                    If Not IsNothing(clsExam) Then  'Obj Disposed by Mitesh
                        clsExam.Dispose()
                        clsExam = Nothing
                    End If

                    Return False
                End If
            End If
        Else
            If Not IsNothing(clsExam) Then  'Obj Disposed by Mitesh
                clsExam.Dispose()
                clsExam = Nothing
            End If

            Return False
        End If

        If Not IsNothing(clsExam) Then  'Obj Disposed by Mitesh
            clsExam.Dispose()
            clsExam = Nothing
        End If
        Return True
    End Function

    'Developer:Sanjog Dhamke
    'Date: 21 March 2012
    'Bug PRD Name:Copy Exam Functionality
    'Reason: To implement new functionality
    Public Sub SaveExam(ByRef nExamID As Long, ByVal nVisitID As Long, ByVal nPatinetID As Long, ByVal strExamName As String, ByVal strTempFilePath As String, ByVal isFinished As Byte, ByVal dtDOS As DateTime, ByVal nReviewerId As Int64, ByVal ReviewDate As DateTime, ByVal bIsReviewed As Boolean, ByVal strTemplateName As String, Optional ByVal CopyExamID As Int64 = 0, Optional ByVal UserID As Int64 = 0, Optional ByVal TemplateSpecility As String = "", Optional ByVal PatientSeen As Boolean = True, Optional ByVal PatientOfficeVisit As Boolean = True, Optional ByVal Encounter As Boolean = True, Optional ByVal PatientStatus As Byte = 2, Optional ByVal ProviderID As Int64 = 0, Optional ByVal CaseID As Int64 = 0, Optional ByVal DischargeDate As String = Nothing, Optional ByVal DischargeDisposition As String = "", Optional ByVal DiagnosisType As String = "", Optional ByVal dtadmitdate As String = Nothing)

        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim ExamParam As SqlParameter = Nothing

        Try

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            cmd = New SqlCommand("gsp_InUpExam", Con)
            cmd.CommandType = CommandType.StoredProcedure

            '17-Nov-14 Aniket: Resolved Bug #76018: gloEMR>New Exam>It is showing exception "System.Data.SqlClient.SqlException "
            'Saving exams may take longer depending on the size of the document saved.
            cmd.CommandTimeout = 0

            ExamParam = cmd.Parameters.Add("@ExamID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.InputOutput
            ExamParam.Value = nExamID

            'Visit ID
            sqlParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nVisitID
            'patient Id
            sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = nPatinetID

            'Exam
            sqlParam = cmd.Parameters.Add("@ExamName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = strExamName  'Exam name


            'Exam Contents
            If strTempFilePath <> "" Then
                sqlParam = cmd.Parameters.Add("@PatientNotes", SqlDbType.Image)
                sqlParam.Direction = ParameterDirection.Input
                'Dim objword As New clsWordDocument(nPatinetID)
                Dim objword As New clsWordDocument 'SLR: new nPatienID not needed
                sqlParam.Value = objword.ConvertFiletoBinary(strTempFilePath)
                'SLR: Free  
                objword = Nothing
            End If
            'Finished or not
            sqlParam = cmd.Parameters.Add("@Finished", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = isFinished


            sqlParam = cmd.Parameters.Add("@dtDOS", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = dtDOS

            sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = GetPrefixTransactionID()

            sqlParam = cmd.Parameters.Add("@ReviewerID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            If bIsReviewed Then
                sqlParam.Value = nReviewerId
            Else
                sqlParam.Value = DBNull.Value
            End If

            sqlParam = cmd.Parameters.Add("@IsReviewed", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            If bIsReviewed Then
                sqlParam.Value = bIsReviewed
            Else
                sqlParam.Value = DBNull.Value
            End If

            sqlParam = cmd.Parameters.Add("@ReviewDate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            If bIsReviewed Then
                sqlParam.Value = ReviewDate
            Else
                sqlParam.Value = DBNull.Value
            End If

            'For template name 
            sqlParam = cmd.Parameters.Add("@TemplateName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = strTemplateName  'selected template name

            'Developer:Sanjog Dhamke
            'Date: 21 March 2012
            'Bug PRD Name:Copy Exam Functionality
            'Reason: To implement new functionality 
            sqlParam = cmd.Parameters.Add("@CopyExamID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = CopyExamID  'selected template name

            sqlParam = cmd.Parameters.Add("@UserID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = UserID  'selected template name

            sqlParam = cmd.Parameters.Add("@TemplateSpecility", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = TemplateSpecility  'selected template name


            sqlParam = cmd.Parameters.Add("@PatientSeen", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatientSeen


            sqlParam = cmd.Parameters.Add("@OfficeVisit", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatientOfficeVisit


            sqlParam = cmd.Parameters.Add("@Encounter", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Encounter

            sqlParam = cmd.Parameters.Add("@PatientStatus", SqlDbType.SmallInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatientStatus

            sqlParam = cmd.Parameters.Add("@nProviderID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ProviderID

            sqlParam = cmd.Parameters.Add("@nCaseID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = CaseID

            If Not String.IsNullOrEmpty(DischargeDate) Then
                sqlParam = cmd.Parameters.Add("@dtDischargeDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = Convert.ToDateTime(DischargeDate)
            End If

            sqlParam = cmd.Parameters.Add("@DischargeDisposition", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = DischargeDisposition

            sqlParam = cmd.Parameters.Add("@DiagnosisType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = DiagnosisType

            If Not IsNothing(dtadmitdate) Then
                sqlParam = cmd.Parameters.Add("@dtadmitDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = Convert.ToDateTime(dtadmitdate)
            End If

            cmd.ExecuteNonQuery()
            nExamID = ExamParam.Value
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
            If Not IsNothing(sqlParam) Then 'Obj Disposed by Mitesh
                sqlParam = Nothing
            End If
            If Not IsNothing(ExamParam) Then
                ExamParam = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try
    End Sub

    Public Function SaveIntervention(ByVal ExamID As Int64, ByVal PatientID As Int64, ByVal VisitID As Int64, ByVal InterventionList As gloGeneralItem.gloItems) As Boolean

        Dim Trans As SqlTransaction = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Try
            ''@nPatientID, @nExamID, @nVisitID, @sInterventionDescription, @nClinicID

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            Trans = Con.BeginTransaction

            cmd = New SqlCommand("DELETE FROM ExamIntervention WHERE nExamID = " & ExamID & " AND nVisitID = " & VisitID & "", Con)
            cmd.Transaction = Trans
            cmd.ExecuteNonQuery()
            'SLR: Cmd=nothing and then
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            cmd = New SqlCommand("gsp_InExamIntervention", Con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Transaction = Trans            

            For i As Integer = 0 To InterventionList.Count - 1

                cmd.Parameters.Clear()

                'ExamID
                sqlParam = cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ExamID

                'Visit ID
                sqlParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = VisitID

                'Patient Id
                sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = PatientID

                'Intervention Description
                sqlParam = cmd.Parameters.Add("@sInterventionDescription", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = InterventionList(i).Description.ToString()

                'Clinic Id
                sqlParam = cmd.Parameters.Add("@nClinicID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = gnClinicID

                If (cmd.ExecuteNonQuery() > 0) Then
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Add, "Record Added.", gloAuditTrail.ActivityOutCome.Success)
                End If
            Next
            Trans.Commit()
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Trans.Rollback()
            Return Nothing
        Finally
            If Not IsNothing(Trans) Then 'Obj Disposed by Mitesh
                Trans.Dispose()
                Trans = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            'SLR: Con.close?
            If Con.State = ConnectionState.Open Then
                Con.Close()

            End If
            sqlParam = Nothing
        End Try
    End Function


    Public Sub UpdateExam(ByRef nExamID As Long, ByVal strTempFilePath As String)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim ExamParam As SqlParameter = Nothing
        Try
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            cmd = New SqlCommand("gsp_UpdateExam", Con)
            cmd.CommandType = CommandType.StoredProcedure
           
        
            'Exam ID
            ExamParam = cmd.Parameters.Add("@ExamID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = nExamID

            'Exam Contents
            sqlParam = cmd.Parameters.Add("@PatientNotes", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input
            Dim mstream As ADODB.Stream
            mstream = New ADODB.Stream
            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
            mstream.Open()
            mstream.LoadFromFile(strTempFilePath)
            sqlParam.Value = mstream.Read()

            mstream.Close()
            cmd.ExecuteNonQuery()
            mstream = Nothing    'SLR: Free mstream
            Con.Close()
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Modify, "Patient exam updated", 0, nExamID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
            If Not IsNothing(sqlParam) Then 'Obj Disposed by Mitesh
                sqlParam = Nothing
            End If
            If Not IsNothing(ExamParam) Then
                ExamParam = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Sub

    Public Function Fill_Exams(ByVal nPatientID As Long) As DataTable
        Dim objCon As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            objCon = New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            If objCon.State = ConnectionState.Closed Then
                objCon.Open()
            End If
            cmd = New SqlCommand("gsp_GetPastExams", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PatientID", nPatientID)
       
            
        da = New SqlDataAdapter
        da.SelectCommand = cmd
        dt = New DataTable
        da.Fill(dt)
        objCon.Close()
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return dt
        Finally
            'If Not IsNothing(dt) Then 'Obj Disposed by Mitesh 'SLR: Don't dispose, since it is returned
            'dt.Dispose()
            'dt = Nothing
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
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
        
    End Function

    Public Function Fill_TemplatesCategory() As DataTable
        Dim dt As DataTable = Nothing
        Dim da As SqlDataAdapter = Nothing
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        Dim cmd As New SqlCommand("gsp_FillCategory_MST", Con)
        Dim sqlParam As SqlParameter
        Try

        cmd.CommandType = CommandType.StoredProcedure        
        sqlParam = cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar)
        sqlParam.Direction = ParameterDirection.Input
        sqlParam.Value = "Template"
         
        da = New SqlDataAdapter
        da.SelectCommand = cmd
        dt = New DataTable
        da.Fill(dt)
        Con.Close()
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then  'Obj Disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            sqlParam = Nothing
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
        End Try
       
    End Function
    Public Function Fill_TemplatesCategory_Speed() As DataSet
        Dim ds As New DataSet
        Dim da As SqlDataAdapter = Nothing
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        Dim cmd As New SqlCommand("gsp_FillCategory_MST_Speed", Con)
        Dim sqlParam As SqlParameter
        Try

            cmd.CommandType = CommandType.StoredProcedure
            sqlParam = cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = "Template"

            da = New SqlDataAdapter
            da.SelectCommand = cmd

            da.Fill(ds)
            Con.Close()

            Return ds
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then  'Obj Disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            sqlParam = Nothing
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
        End Try

    End Function
    Public Function Fill_ExamTemplateNames(ByVal nCategoryID As Long, ByVal nProviderID As Long) As DataTable
        Dim dt As DataTable = Nothing
        Dim da As SqlDataAdapter = Nothing
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        Dim cmd As New SqlCommand("gsp_ViewTemplateGallery_MST", Con)
        Dim sqlParam1 As SqlParameter
        Try
           

        cmd.CommandType = CommandType.StoredProcedure        

        sqlParam1 = cmd.Parameters.Add("@ID", SqlDbType.BigInt)
        sqlParam1.Direction = ParameterDirection.Input
        sqlParam1.Value = nCategoryID 'dtCategory.Rows(nTableCount)(0)
        If nProviderID > 0 Then
            sqlParam1 = cmd.Parameters.Add("@ProviderID", SqlDbType.BigInt)
            sqlParam1.Direction = ParameterDirection.Input
            sqlParam1.Value = nProviderID 'dtCategory.Rows(nTableCount)(0)
        End If
        da = New SqlDataAdapter
        da.SelectCommand = cmd
        dt = New DataTable
        da.Fill(dt)

            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then  'Obj Disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
            sqlParam1 = Nothing
        End Try
       
    End Function

    Public Function Fill_ExamTemplateNames_Speed(ByVal sCategoryID As String, ByVal nProviderID As Long) As DataTable

        Dim dtMain As New DataTable
        Dim daMain As New SqlDataAdapter
        If Con.State = ConnectionState.Closed Then        'SLR: Con.Open is missing
            Con.Open()
        End If
        Dim cmdMain As New SqlCommand("gsp_ViewTemplateGallery_MST_Speed", Con)
        Dim sqlParam1 As SqlParameter
        Try
           
       
        cmdMain.CommandType = CommandType.StoredProcedure
        sqlParam1 = cmdMain.Parameters.Add("@ID", SqlDbType.VarChar)
        sqlParam1.Direction = ParameterDirection.Input
        sqlParam1.Value = sCategoryID

        If nProviderID > 0 Then
            sqlParam1 = cmdMain.Parameters.Add("@ProviderID", SqlDbType.BigInt)
            sqlParam1.Direction = ParameterDirection.Input
            sqlParam1.Value = nProviderID
        End If
        daMain.SelectCommand = cmdMain
        daMain.Fill(dtMain)

            Return dtMain
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(cmdMain) Then  'Obj Disposed by Mitesh
                cmdMain.Parameters.Clear()
                cmdMain.Dispose()
                cmdMain = Nothing
            End If
            If Not IsNothing(daMain) Then
                daMain.Dispose()
                daMain = Nothing
            End If
            '  If Not IsNothing(dtMain) Then 'SLR: Don't dispose since it s retuened
            'dtMain.Dispose()
            'dtMain = Nothing
            'End If
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
            sqlParam1 = Nothing
        End Try
        
    End Function

    Public Function GetTemplateContents(ByVal TemplateId As Long) As DataTable
        Dim Cmd As New SqlCommand
        Dim sParam As New SqlParameter
        Dim da As New SqlDataAdapter(Cmd)
        Dim dtData As New DataTable
        Try
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandText = "gsp_GetExamContents"
            If Con.State = ConnectionState.Closed Then        'SLR: Con.Open is missing
                Con.Open()
            End If
            Cmd.Parameters.Clear()
            sParam.ParameterName = "@nTemplateId"
            sParam.Value = TemplateId
            sParam.SqlDbType = SqlDbType.BigInt
            Cmd.Parameters.Add(sParam)
            Cmd.Connection = Con

            da.Fill(dtData)

            Return dtData
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(da) Then  'Obj Disposed by Mitesh
                da.Dispose()
                da = Nothing
            End If
            'If Not IsNothing(dtData) Then   'SLR: Don't dispsoe since this sis returned
            'dtData.Dispose()
            'dtData = Nothing
            ' End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If

            If Not IsNothing(sParam) Then    'SLR:Dispose sParam
                sParam = Nothing
            End If
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
        End Try
    End Function

    Public Function RetrieveTemplateContents(ByVal TemplateId As Long) As DataTable
        Dim Cmd As New SqlCommand
        Dim sParam As New SqlParameter
        Dim da As New SqlDataAdapter(Cmd)
        Dim dtData As New DataTable
        Try

        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.CommandText = "gsp_GetExamContents"
        Con.Open()
        Cmd.Parameters.Clear()
        sParam.ParameterName = "@nTemplateId"
        sParam.Value = TemplateId
        sParam.SqlDbType = SqlDbType.BigInt
        Cmd.Parameters.Add(sParam)
        Cmd.Connection = Con
       
        da.Fill(dtData)
        Con.Close()
        Return dtData
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(da) Then  'Obj Disposed by Mitesh
                da.Dispose()
                da = Nothing
            End If
            'If Not IsNothing(dtData) Then  'SLR: Don't dispsoe
            'dtData.Dispose()
            ' dtData = Nothing
            ' End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Not IsNothing(sParam) Then
                sParam = Nothing
            End If
        End Try
       
    End Function

    Public Enum enmFAXType
        FormGallery
        PatientExam
        PatientLetters
        PatientMessages
        Prescription
        Referrals
        Orders
        PTProtocols
    End Enum

    Public Function CheckFAXCoverPageTemplateExists() As Boolean
        Dim Cmd As New SqlCommand
        Dim sParam As New SqlParameter
        Try

        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.CommandText = "gsp_CheckFAXCoverPageTemplate"
        Cmd.Parameters.Clear()
        sParam.ParameterName = "@TemplateName"
        sParam.Value = "Cover Page"
        sParam.SqlDbType = SqlDbType.VarChar
        Cmd.Parameters.Add(sParam)
        Cmd.Connection = Con
        Dim nNoOfCoverPages As Byte
        Con.Open()
        nNoOfCoverPages = Cmd.ExecuteScalar
        Con.Close()
        If nNoOfCoverPages >= 1 Then
            Return True
        Else
            Return False
        End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(sParam) Then   'Obj Disposed by Mitesh
                sParam = Nothing
            End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
       
    End Function


    Public Function CheckFAXCoverPageTemplateExists(ByVal enmFAXDocumentType As enmFAXType) As Boolean
        Dim Cmd As New SqlCommand
        Dim sParam As New SqlParameter
        Try
            Dim strTemplateName As String = String.Empty
            Select Case enmFAXDocumentType
                Case enmFAXType.FormGallery
                    strTemplateName = "Form Gallery"
                Case enmFAXType.Orders
                    strTemplateName = "Orders"
                Case enmFAXType.PatientExam
                    strTemplateName = "Exam"
                Case enmFAXType.PatientLetters
                    strTemplateName = "Letters"
                Case enmFAXType.PatientMessages
                    strTemplateName = "Messages"
                Case enmFAXType.Prescription
                    strTemplateName = "Prescription"
                Case enmFAXType.Referrals
                    strTemplateName = "Referrals"
                Case enmFAXType.PTProtocols
                    strTemplateName = "PTProtocols"
            End Select

            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandText = "gsp_CheckFAXCoverPageTemplate"
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            Cmd.Parameters.Clear()
            sParam.ParameterName = "@TemplateName"
            sParam.Value = strTemplateName
            sParam.SqlDbType = SqlDbType.VarChar
            Cmd.Parameters.Add(sParam)
            'Dim dr As New SqlDataAdapter
            Cmd.Connection = Con
            Dim objSQLReader As SqlDataReader
            objSQLReader = Cmd.ExecuteReader()
            Dim blnTemplateExists As Boolean
            blnTemplateExists = False
            If objSQLReader.HasRows = True Then
                objSQLReader.Read()
                If IsNothing(objSQLReader.Item(0)) = False Then
                    If IsDBNull(objSQLReader.Item(0)) = False Then
                        If objSQLReader.Item(0) >= 1 Then
                            blnTemplateExists = True
                        End If
                    End If
                End If
            End If
            Try
                If Not IsNothing(objSQLReader) Then  ''slr free objSQLReader
                    objSQLReader.Close()
                    objSQLReader = Nothing
                End If
            Catch ex As Exception

            End Try
            
            Return blnTemplateExists
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally


            If Not IsNothing(sParam) Then   'Obj Disposed by Mitesh
                sParam = Nothing
            End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
        End Try
       
    End Function

    Public Function GetTemplateContentsForFAX() As Object
        Dim Cmd As New SqlCommand
        Dim sParam As New SqlParameter
        Try
            'Dim objSQLDataReader As SqlDataReader
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandText = "gsp_GetExamContentsByTemplateName"
            Cmd.Parameters.Clear()
            sParam.ParameterName = "@TemplateName"
            sParam.Value = "Cover Page"
            sParam.SqlDbType = SqlDbType.VarChar
            Cmd.Parameters.Add(sParam)
            'Dim dr As New SqlDataAdapter
            Cmd.Connection = Con
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            Dim objTemplateContents As Object
            objTemplateContents = Cmd.ExecuteScalar
            Return objTemplateContents
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(sParam) Then   'Obj Disposed by Mitesh
                sParam = Nothing
            End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
        End Try
    End Function

    Public Function GetTemplateContentsForFAX(ByVal enmFAXDocumentType As enmFAXType) As DataSet
        Dim Cmd As New SqlCommand
        Dim sParam As New SqlParameter
        Dim da As New SqlDataAdapter(Cmd)
        Dim dsData As New DataSet
        Try
            Dim strTemplateName As String = String.Empty
            Select Case enmFAXDocumentType
                Case enmFAXType.FormGallery
                    strTemplateName = "Form Gallery"
                Case enmFAXType.Orders
                    strTemplateName = "Orders"
                Case enmFAXType.PatientExam
                    strTemplateName = "Exam"
                Case enmFAXType.PatientLetters
                    strTemplateName = "Letters"
                Case enmFAXType.PatientMessages
                    strTemplateName = "Messages"
                Case enmFAXType.Prescription
                    strTemplateName = "Prescription"
                Case enmFAXType.Referrals
                    strTemplateName = "Referrals"
                Case enmFAXType.PTProtocols
                    strTemplateName = "PTProtocols"
            End Select


            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandText = "gsp_GetExamContentsByTemplateName"
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            Cmd.Parameters.Clear()
            sParam.ParameterName = "@TemplateName"
            sParam.Value = strTemplateName
            Cmd.Parameters.Add(sParam)
            Cmd.Connection = Con
            
            da.Fill(dsData)

            Return dsData
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dsData) Then   'Obj Disposed by Mitesh 'SLR: Don't dispsose
            'dsData.Dispose()
            ' dsData = Nothing
            'End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(sParam) Then
                sParam = Nothing
            End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If

        End Try
    End Function

    Public Function getData(ByVal PatientID As Long, ByVal strFields As Collection, ByVal ExamID As Long, ByVal nVisitID As Long) As Collection

        Dim strData As String
        Dim dtDOB As Date
        Dim flagOthers As Integer
        Dim filecnt As Int16
        Dim strDataCol As New Collection
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader = Nothing
        Dim sqlParam As SqlParameter = Nothing
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_GetFieldsdata"
        objCmd.Parameters.Clear()



        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If
        Dim nCount As Int16
        For nCount = 1 To strFields.Count
            strData = ""
            If strFields.Item(nCount) <> "" Then
                If InStr(strFields(nCount), "Narration") Or InStr(strFields(nCount), "FlowSheet") Or InStr(strFields(nCount), "LM_LabResult") Or InStr(strFields(nCount), "imgSignature") Or InStr(strFields(nCount), "imgClinicLogo") Then
                    objCmd.Parameters.Clear()
                    If InStr(strFields(nCount), "SingleRow") Then
                        sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = Mid(strFields(nCount), 1, InStrRev(strFields(nCount), "|") - 1)
                    Else
                        sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = strFields(nCount)
                    End If

                    sqlParam = objCmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = PatientID

                    sqlParam = objCmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ExamID

                    sqlParam = objCmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = nVisitID

                    objCmd.Connection = Con

                    objSQLDataReader = objCmd.ExecuteReader
                    If objSQLDataReader.HasRows = True Then
                        While objSQLDataReader.Read
                            If IsDBNull(objSQLDataReader.Item(0)) = False Then
                                Dim strFileName As String
                                If objSQLDataReader.Item(1) = "2" Then
                                    filecnt = filecnt + 1
                                    If InStr(strFields(nCount), "Narration") Then
                                        strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "Narration.Txt"
                                    Else
                                        strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "Flowsheet" & filecnt & ".Txt"
                                    End If
                                Else
                                    strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "image.bmp"
                                End If
                                strData = strFileName
                                '''''
                                'Save contents in file
                                Dim mstream As ADODB.Stream
                                mstream = New ADODB.Stream
                                mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
                                mstream.Open()
                                '       Dim sContents As String
                                'Check if there are records for selected Node
                                mstream.Write(objSQLDataReader.Item(0))
                                If System.IO.File.Exists(strFileName) Then
                                    System.IO.File.Delete(strFileName)
                                End If
                                mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
                                mstream.Close()
                                mstream = Nothing     'SLR: free mstream
                                If InStr(strFields(nCount), "SingleRow") Then
                                    Dim oFile As System.IO.File
                                    Dim oRead As System.IO.StreamReader
                                    Dim LineIn As String
                                    Dim strNewString As New ArrayList
                                    Dim j As Integer
                                    oRead = File.OpenText(strFileName)

                                    While oRead.Peek <> -1
                                        LineIn = oRead.ReadLine()
                                        strNewString.Add(LineIn)
                                    End While
                                    oRead.Close()
                                    oRead.Dispose() 'SLR: Free oRead
                                    oRead = Nothing
                                    Dim oWrite As System.IO.StreamWriter
                                    oWrite = File.CreateText(strFileName)
                                    oWrite.WriteLine(strNewString.Item(0))

                                    Dim strNewStringCount As Int16
                                    strNewStringCount = strNewString.Count
                                    If strNewStringCount > 1 Then
                                        On Error GoTo ERR1
PROC1:
                                        If Trim(strNewString.Item(strNewStringCount - 1)) = "" Then
                                            strNewStringCount = strNewStringCount - 1
                                            GoTo PROC1
                                        End If
                                        oWrite.WriteLine(strNewString.Item(strNewStringCount - 1))
ERR1:
                                    End If
                                    oWrite.Close()
                                    oWrite.Dispose()
                                    oWrite = Nothing
                                    strNewString = Nothing
                                    'SLR Free OWrite, strNewString
                                End If
                                flagOthers = objSQLDataReader.Item(1)
                            End If
                        End While
                    End If
                    objSQLDataReader.Close()        'SLR Free OWrite, strNewString
                    objSQLDataReader = Nothing
                ElseIf Left(strFields(nCount), 6) <> "Others" Then
                    objCmd.Parameters.Clear()
                    sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar, 5000)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = strFields(nCount)

                    sqlParam = objCmd.Parameters.AddWithValue("@nPatientID", PatientID)
                    sqlParam.Direction = ParameterDirection.Input

                    sqlParam = objCmd.Parameters.AddWithValue("@nExamID", ExamID)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ExamID

                    sqlParam = objCmd.Parameters.AddWithValue("@nVisitID", nVisitID)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = nVisitID

                    objCmd.Connection = Con

                    objSQLDataReader = objCmd.ExecuteReader
                    If objSQLDataReader.HasRows = True Then
                        While objSQLDataReader.Read
                            If IsDBNull(objSQLDataReader.Item(0)) = False Then
                                If strData = Nothing Then
                                    strData = objSQLDataReader.Item(0)
                                    flagOthers = objSQLDataReader.Item(1)
                                Else
                                    strData = strData & Chr(11) & objSQLDataReader.Item(0)
                                End If
                            End If
                        End While
                    End If
                    objSQLDataReader.Close()   'SLR Free objSQLDataReader
                    objSQLDataReader = Nothing
                Else
                    flagOthers = 0
                    If InStr(strFields(nCount), "Age") Then
                        Dim objCmd1 As New SqlCommand
                        Dim objSQLDataReader1 As SqlDataReader
                        Dim sqlParam1 As SqlParameter
                        objCmd1.CommandType = CommandType.StoredProcedure
                        objCmd1.CommandText = "gsp_GetFieldsdata"
                        objCmd1.Parameters.Clear()

                        objCmd1.CommandText = "gsp_GetDOB"
                        objCmd1.Parameters.Clear()
                        sqlParam1 = objCmd1.Parameters.AddWithValue("@nPatientID", PatientID)
                        sqlParam1.Direction = ParameterDirection.Input

                        objCmd1.Connection = Con

                        objSQLDataReader1 = objCmd1.ExecuteReader

                        If objSQLDataReader1.HasRows = True Then
                            objSQLDataReader1.Read()
                            If IsDBNull(objSQLDataReader1.Item(0)) = False Then
                                dtDOB = objSQLDataReader1.Item(0)
                            End If
                        End If
                        Dim nMonths As Int16
                        nMonths = DateDiff(DateInterval.Month, CType(dtDOB, Date), Date.Now.Date)
                        strData = nMonths \ 12 & " Yrs " & nMonths Mod 12 & " Months" ' DateDiff(DateInterval.Month, CType(gstrPatientDOB, Date), Date.Now.Date) & " Yrs"
                        '                        strData = "20"
                        objSQLDataReader1.Close()
                        objSQLDataReader1 = Nothing
                        'SLR: FRee objComd1, sqlparame1
                        objCmd1.Parameters.Clear()
                        objCmd1.Dispose()
                        objCmd1 = Nothing
                        sqlParam1 = Nothing
                    ElseIf InStr(strFields(nCount), "TodayDate") Then
                        strData = Now.Date
                    ElseIf InStr(strFields(nCount), "DOS") Then
                        strData = Now.Date
                    ElseIf InStr(strFields(nCount), "Time") Then
                        strData = Format(Now, "Medium Time")
                    End If
                End If
            End If
            '''' For Vitals if Field is of BloodPressure (Sitting or Standing(MIN/MAX))
            '''' then take only Integer part of its Value 
            If InStr(strFields(nCount), "dBloodPressure") Then
                strData = Split(strData, ".")(0)
            End If
            strDataCol.Add(strData & "|" & flagOthers.ToString)
        Next




        If Con.State = ConnectionState.Open Then
            Con.Close()
        End If
        If Not IsNothing(objSQLDataReader) Then   'Obj Disposed by Mitesh
            objSQLDataReader.Dispose()
            objSQLDataReader = Nothing
        End If
        If Not IsNothing(objCmd) Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        If Not IsNothing(sqlParam) Then
            sqlParam = Nothing
        End If



        Return strDataCol
    End Function

    Public Sub OpenConnection()
        Con.Open()
    End Sub

    Public Sub CloseConnection()
        Con.Close()
    End Sub

    Public Function getData_New(ByVal PatientID As Long, ByVal strFields As String, ByVal ExamID As Long, ByVal nVisitID As Long) As String
        Dim strData As String
        Dim dtDOB As Date
        Dim flagOthers As Integer
        Dim filecnt As Int16
        Dim strDataCol As String

        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader = Nothing
        Dim sqlParam As SqlParameter = Nothing
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_GetFieldsdata"
        objCmd.Parameters.Clear()
        'Con.Open()
        'Dim nCount As Int16
        strData = ""
        If strFields <> "" Then
            If InStr(strFields, "Narration") Or InStr(strFields, "FlowSheet") Or InStr(strFields, "LM_LabResult") Or InStr(strFields, "imgSignature") Or InStr(strFields, "imgClinicLogo") Then

                If InStr(strFields, "SingleRow") Then
                    sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = Mid(strFields, 1, InStrRev(strFields, "|") - 1)
                Else
                    sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = strFields
                End If

                sqlParam = objCmd.Parameters.AddWithValue("@nPatientID", PatientID)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam = objCmd.Parameters.AddWithValue("@nExamID", ExamID)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ExamID

                sqlParam = objCmd.Parameters.AddWithValue("@nVisitID", nVisitID)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = nVisitID

                objCmd.Connection = Con

                objSQLDataReader = objCmd.ExecuteReader
                If objSQLDataReader.HasRows = True Then
                    'objSQLDataReader.Read()
                    While objSQLDataReader.Read
                        If IsDBNull(objSQLDataReader.Item(0)) = False Then
                            Dim strFileName As String
                            If objSQLDataReader.Item(1) = "2" Then
                                filecnt = filecnt + 1
                                If InStr(strFields, "Narration") Then
                                    strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "Narration.Txt"
                                Else
                                    strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "Flowsheet" & filecnt & ".Txt"
                                End If
                            Else
                                strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "image.bmp"
                            End If
                            strData = strFileName
                            '''''
                            'Save contents in file
                            Dim mstream As ADODB.Stream
                            mstream = New ADODB.Stream
                            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
                            mstream.Open()
                            '       Dim sContents As String
                            'Check if there are records for selected Node
                            mstream.Write(objSQLDataReader.Item(0))
                            If System.IO.File.Exists(strFileName) Then
                                System.IO.File.Delete(strFileName)
                            End If
                            mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
                            mstream.Close()
                            mstream = Nothing   'SLR: Free mstream
                            If InStr(strFields, "SingleRow") Then
                                ' Dim oFile As System.IO.File
                                Dim oRead As System.IO.StreamReader
                                Dim LineIn As String
                                Dim strNewString As New ArrayList
                                'Dim j As Integer
                                oRead = File.OpenText(strFileName)

                                While oRead.Peek <> -1
                                    LineIn = oRead.ReadLine()
                                    strNewString.Add(LineIn)
                                End While
                                oRead.Close()
                                oRead.Dispose()  'SLR: Free oReead
                                oRead = Nothing
                                Dim oWrite As System.IO.StreamWriter
                                oWrite = File.CreateText(strFileName)
                                oWrite.WriteLine(strNewString.Item(0))

                                If strNewString.Count > 1 Then
                                    Dim nLoop As Int16
                                    For nLoop = strNewString.Count - 1 To 0 Step -1
                                        If Trim(strNewString.Item(nLoop)) <> "" Then
                                            Exit For
                                        End If
                                    Next
                                    oWrite.WriteLine(strNewString.Item(nLoop))
                                End If
                                oWrite.Close()   'SLR: Free  oWrite, strNewStreing
                                oWrite.Dispose()
                                oWrite = Nothing
                                strNewString = Nothing
                            End If
                            '''''
                            flagOthers = objSQLDataReader.Item(1)
                        End If
                    End While
                End If
                objSQLDataReader.Close()
                objSQLDataReader = Nothing     'SLR: FRee objSqldataReadrer
            ElseIf Left(strFields, 6) <> "Others" Then

                sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar, 500)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = strFields

                sqlParam = objCmd.Parameters.AddWithValue("@nPatientID", PatientID)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam = objCmd.Parameters.AddWithValue("@nExamID", ExamID)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ExamID

                sqlParam = objCmd.Parameters.AddWithValue("@nVisitID", nVisitID)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = nVisitID

                objCmd.Connection = Con

                objSQLDataReader = objCmd.ExecuteReader
                If objSQLDataReader.HasRows = True Then
                    While objSQLDataReader.Read
                        If IsDBNull(objSQLDataReader.Item(0)) = False Then
                            If strData = Nothing Then
                                strData = objSQLDataReader.Item(0)
                                flagOthers = objSQLDataReader.Item(1)
                            Else
                                strData = strData & Chr(11) & objSQLDataReader.Item(0)
                            End If
                        End If
                    End While
                End If
                objSQLDataReader.Close()
                objSQLDataReader = Nothing    'SLR: FRee objSqldataReadrer
            Else
                flagOthers = 0
                Select Case strFields
                End Select
                If InStr(strFields, "Age") Then
                    Dim objCmd1 As New SqlCommand
                    Dim objSQLDataReader1 As SqlDataReader
                    Dim sqlParam1 As SqlParameter
                    objCmd1.CommandType = CommandType.StoredProcedure
                    objCmd1.CommandText = "gsp_GetDOB"
                    objCmd1.Parameters.Clear()
                    sqlParam1 = objCmd1.Parameters.AddWithValue("@nPatientID", PatientID)
                    sqlParam1.Direction = ParameterDirection.Input

                    objCmd1.Connection = Con

                    objSQLDataReader1 = objCmd1.ExecuteReader

                    If objSQLDataReader1.HasRows = True Then
                        objSQLDataReader1.Read()
                        If IsDBNull(objSQLDataReader1.Item(0)) = False Then
                            dtDOB = objSQLDataReader1.Item(0)
                        End If
                    End If
                    Dim nMonths As Int16
                    nMonths = DateDiff(DateInterval.Month, CType(dtDOB, Date), Date.Now.Date)
                    strData = nMonths \ 12 & " Yrs " & nMonths Mod 12 & " Months" ' DateDiff(DateInterval.Month, CType(gstrPatientDOB, Date), Date.Now.Date) & " Yrs"
                    objSQLDataReader1.Close()
                    objSQLDataReader1 = Nothing
                    objCmd1.Parameters.Clear()
                    objCmd1.Dispose()
                    objCmd1 = Nothing
                    sqlParam1 = Nothing
                    'SLR: Free objSQLDatareade1, objcmd1, sqlparamre1
                ElseIf InStr(strFields, "TodayDate") Then
                    strData = Now.Date
                ElseIf InStr(strFields, "DOS") Then
                    strData = Now.Date
                ElseIf InStr(strFields, "Time") Then
                    strData = Format(Now, "Medium Time")
                End If
            End If
        End If
        '''' For Vitals if Field is of BloodPressure (Sitting or Standing(MIN/MAX))
        '''' then take only Integer part of its Value 
        If InStr(strFields, "dBloodPressure") Then
            strData = Split(strData, ".")(0)
        End If
        '''''
        strDataCol = strData & "|" & flagOthers.ToString

        If Not IsNothing(objSQLDataReader) Then   'Obj Disposed by Mitesh
            objSQLDataReader.Dispose()
            objSQLDataReader = Nothing
        End If
        If Not IsNothing(objCmd) Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        If Not IsNothing(sqlParam) Then
            sqlParam = Nothing
        End If

        Return strDataCol
    End Function

    Public Function getDataOthers(ByVal PatientID As Long, ByVal strFields As Collection, ByVal nVisitID As Long, Optional ByVal nTestID As Long = 0) As Collection
        '''' 20070106 Optional ByVal nTestID As Long = 0
        '' TestID to get the data regarding the paricular test
        Dim strData As String
        Dim dtDOB As Date
        Dim flagOthers As Integer

        Dim filecnt As Int16
        Dim strDataCol As New Collection
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader = Nothing
        Dim sqlParam As SqlParameter = Nothing
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_GetFieldsdataForOthers"
        objCmd.Parameters.Clear()
        Con.Open()
        Dim nCount As Int16
        For nCount = 1 To strFields.Count
            strData = ""
            If strFields.Item(nCount) <> "" Then
                If InStr(strFields(nCount), "Narration") Or InStr(strFields(nCount), "FlowSheet") Or InStr(strFields(nCount), "LM_LabResult") Or InStr(strFields(nCount), "imgSignature") Then
                    objCmd.Parameters.Clear()

                    If InStr(strFields(nCount), "SingleRow") Then
                        sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = Mid(strFields(nCount), 1, InStrRev(strFields(nCount), "|") - 1)
                    Else
                        sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = strFields(nCount)
                    End If

                    sqlParam = objCmd.Parameters.AddWithValue("@nPatientID", PatientID)
                    sqlParam.Direction = ParameterDirection.Input

                    sqlParam = objCmd.Parameters.AddWithValue("@nVisitID", nVisitID)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = nVisitID

                    sqlParam = objCmd.Parameters.Add("@nTestID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = nTestID

                    objCmd.Connection = Con

                    objSQLDataReader = objCmd.ExecuteReader
                    If objSQLDataReader.HasRows = True Then
                        While objSQLDataReader.Read
                            If IsDBNull(objSQLDataReader.Item(0)) = False Then
                                Dim strFileName As String
                                If objSQLDataReader.Item(1) = "2" Then
                                    filecnt = filecnt + 1
                                    If InStr(strFields(nCount), "Narration") Then
                                        strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "Narration.Txt"
                                    Else
                                        strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "Flowsheet" & filecnt & ".Txt"
                                    End If
                                Else
                                    strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "image.bmp"
                                End If
                                strData = strFileName
                                'Save contents in file
                                Dim mstream As ADODB.Stream
                                mstream = New ADODB.Stream
                                mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
                                mstream.Open()
                                '''' Check if there are records for selected Node
                                mstream.Write(objSQLDataReader.Item(0))
                                If System.IO.File.Exists(strFileName) Then
                                    System.IO.File.Delete(strFileName)
                                End If
                                mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
                                mstream.Close()       'SLR: Free mStream
                                mstream = Nothing

                                '''''
                                If InStr(strFields(nCount), "SingleRow") Then
                                    Dim oFile As System.IO.File
                                    Dim oRead As System.IO.StreamReader
                                    Dim LineIn As String
                                    Dim strNewString As New ArrayList
                                    Dim j As Integer
                                    oRead = File.OpenText(strFileName)

                                    While oRead.Peek <> -1
                                        LineIn = oRead.ReadLine()
                                        strNewString.Add(LineIn)
                                    End While
                                    oRead.Close()
                                    oRead.Dispose()
                                    oRead = Nothing  'SLR: Free oRead
                                    Dim oWrite As System.IO.StreamWriter
                                    oWrite = File.CreateText(strFileName)
                                    oWrite.WriteLine(strNewString.Item(0))

                                    Dim strNewStringCount As Int16
                                    strNewStringCount = strNewString.Count
                                    If strNewStringCount > 1 Then
                                        On Error GoTo ERR1
PROC1:
                                        If Trim(strNewString.Item(strNewStringCount - 1)) = "" Then
                                            strNewStringCount = strNewStringCount - 1
                                            GoTo PROC1
                                        End If
                                        oWrite.WriteLine(strNewString.Item(strNewStringCount - 1))
ERR1:                                  
                                    End If
                                    oWrite.Close()
                                    oWrite.Dispose()
                                    oWrite = Nothing
                                    strNewString = Nothing
                                    ''slr free oWrite ,strNewString
                                End If
                                flagOthers = objSQLDataReader.Item(1)
                            End If
                        End While
                    End If
                    objSQLDataReader.Close()
                    objSQLDataReader = Nothing
                    'SLR: FRee objsqldatatreader
                ElseIf Left(strFields(nCount), 6) <> "Others" Then
                    objCmd.Parameters.Clear()
                    sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar, 5000)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = strFields(nCount)

                    sqlParam = objCmd.Parameters.AddWithValue("@nPatientID", PatientID)
                    sqlParam.Direction = ParameterDirection.Input

                    sqlParam = objCmd.Parameters.AddWithValue("@nVisitID", nVisitID)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = nVisitID

                    sqlParam = objCmd.Parameters.Add("@nTestID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = nTestID

                    objCmd.Connection = Con

                    objSQLDataReader = objCmd.ExecuteReader
                    If objSQLDataReader.HasRows = True Then
                        While objSQLDataReader.Read
                            If IsDBNull(objSQLDataReader.Item(0)) = False Then
                                If strData = Nothing Then
                                    strData = objSQLDataReader.Item(0)
                                    flagOthers = objSQLDataReader.Item(1)
                                Else
                                    strData = strData & Chr(11) & objSQLDataReader.Item(0)
                                End If
                            End If
                        End While
                    End If
                    objSQLDataReader.Close()
                    objSQLDataReader = Nothing   'SLR: FRee objSqldatareadrer

                Else
                    If InStr(strFields(nCount), "Age") Then
                        Dim objCmd1 As New SqlCommand
                        Dim objSQLDataReader1 As SqlDataReader
                        Dim sqlParam1 As SqlParameter
                        objCmd1.CommandType = CommandType.StoredProcedure
                        objCmd1.CommandText = "gsp_GetFieldsdata"
                        objCmd1.Parameters.Clear()

                        objCmd1.CommandText = "gsp_GetDOB"
                        objCmd1.Parameters.Clear()
                        sqlParam1 = objCmd1.Parameters.AddWithValue("@nPatientID", PatientID)
                        sqlParam1.Direction = ParameterDirection.Input

                        objCmd1.Connection = Con

                        objSQLDataReader1 = objCmd1.ExecuteReader

                        If objSQLDataReader1.HasRows = True Then
                            objSQLDataReader1.Read()
                            If IsDBNull(objSQLDataReader1.Item(0)) = False Then
                                dtDOB = objSQLDataReader1.Item(0)
                            End If
                        End If
                        Dim nMonths As Int16
                        nMonths = DateDiff(DateInterval.Month, CType(dtDOB, Date), Date.Now.Date)
                        strData = nMonths \ 12 & " Yrs " & nMonths Mod 12 & " Months" ' DateDiff(DateInterval.Month, CType(gstrPatientDOB, Date), Date.Now.Date) & " Yrs"
                        objSQLDataReader1.Close()
                        'SLR: FRee objSqldatareade1, cmd1, sqlparame1
                        objSQLDataReader1 = Nothing
                        objCmd1.Dispose()
                        objCmd1 = Nothing
                        sqlParam1 = Nothing
                    ElseIf InStr(strFields(nCount), "TodayDate") Then
                        strData = Now.Date
                    ElseIf InStr(strFields(nCount), "DOS") Then
                        strData = Now.Date
                    ElseIf InStr(strFields(nCount), "Time") Then
                        strData = Format(Now, "Medium Time")
                    End If
                End If
            End If
            strDataCol.Add(strData & "|" & flagOthers.ToString)
        Next
        Con.Close()

        If Not IsNothing(objSQLDataReader) Then   'Obj Disposed by Mitesh
            objSQLDataReader.Dispose()
            objSQLDataReader = Nothing
        End If
        If Not IsNothing(objCmd) Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        If Not IsNothing(sqlParam) Then
            sqlParam = Nothing
        End If
        Return strDataCol
    End Function

    Private Sub FlowSheet_Of_Visit(ByVal FileName As String, ByVal Delimiter As String, ByVal VisitID As Long)
        'Dim oFile As System.IO.File = Nothing
        Dim oRead As System.IO.StreamReader
        Dim LineIn As String
        Dim j As Integer = 0
        Dim strROW1() As String = Nothing
        Dim strROW2() As String
        Dim oCollection As New Collection
        Try
            oRead = File.OpenText(FileName)

            While oRead.Peek <> -1
                LineIn = oRead.ReadLine()
                If j = 0 Then
                    '' Get All Column Names
                    strROW1 = Split(LineIn, Delimiter)
                Else
                    '' Get Data In Collection (Row wise)
                    strROW2 = Split(LineIn, Delimiter)
                    oCollection.Add(strROW2)
                End If
                j = j + 1
            End While
            oRead.Close()
            
            Dim dt As New DataTable
            Dim i As Integer = 0
            For i = 0 To strROW1.Length - 1
                '' Add the Columns to the Table
                dt.Columns.Add(strROW1(i))
            Next

            For i = 1 To oCollection.Count
                '' Add Rows to DataTable
                dt.Rows.Add(oCollection(i))
            Next

            '''' Get VisitDate 
            Dim VisitDate As Date
            VisitDate = GetVisitdate(VisitID)

            Dim dv As DataView  'SLR: new is not needed
            dv = dt.DefaultView()

            dv.RowFilter = dt.Columns(1).ColumnName & " ='" & VisitDate.Date & "'"

            Dim _Flex As New C1.Win.C1FlexGrid.C1FlexGrid
            With _Flex
                _Flex.Rows.Count = 1
                _Flex.Cols.Fixed = 0
                _Flex.Cols.Count = dv.Table.Columns.Count

                For i = 1 To dt.Columns.Count - 1
                    .SetData(0, i, dt.Columns(i).ColumnName)
                Next

                For i = 1 To dv.Count
                    .Rows.Add()
                    For j = 0 To .Cols.Count - 1
                        .SetData(i, j, dv.Item(i - 1)(j))
                    Next
                Next

                _Flex.SaveGrid(FileName, C1.Win.C1FlexGrid.FileFormatEnum.TextTab, C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells)
            End With
            _Flex.Dispose()
            _Flex = Nothing
            If Not IsNothing(dv) Then    'Obj Disposed by Mitesh
                dv.Dispose()
                dv = Nothing
            End If
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(oRead) Then
                oRead.Dispose()
                oRead = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    'Public Function getDataOthers_Old(ByVal PatientID As Long, ByVal strFields As Collection, ByVal nVisitID As Long) As Collection
    '    '' on 20060328
    '    Dim strData As String
    '    Dim dtDOB As Date
    '    Dim flagOthers As Integer
    '    Dim strDataCol As New Collection
    '    Dim objCmd As New SqlCommand
    '    Dim objSQLDataReader As SqlDataReader
    '    Dim sqlParam As SqlParameter
    '    objCmd.CommandType = CommandType.StoredProcedure
    '    objCmd.CommandText = "gsp_GetFieldsdataForOthers"
    '    objCmd.Parameters.Clear()
    '    Con.Open()
    '    Dim nCount As Int16
    '    For nCount = 1 To strFields.Count
    '        strData = ""
    '        If strFields.Item(nCount) <> "" Then
    '            If InStr(strFields(nCount), "Narration") Or InStr(strFields(nCount), "FlowSheet") Or InStr(strFields(nCount), "imgSignature") Then
    '                objCmd.Parameters.Clear()
    '                sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar)
    '                sqlParam.Direction = ParameterDirection.Input
    '                sqlParam.Value = strFields(nCount)

    '                sqlParam = objCmd.Parameters.AddWithValue("@nPatientID", PatientID)
    '                sqlParam.Direction = ParameterDirection.Input

    '                sqlParam = objCmd.Parameters.AddWithValue("@nVisitID", nVisitID)
    '                sqlParam.Direction = ParameterDirection.Input
    '                sqlParam.Value = nVisitID

    '                objCmd.Connection = Con

    '                objSQLDataReader = objCmd.ExecuteReader
    '                If objSQLDataReader.HasRows = True Then
    '                    While objSQLDataReader.Read
    '                        If IsDBNull(objSQLDataReader.Item(0)) = False Then
    '                            Dim strFileName As String
    '                            If objSQLDataReader.Item(1) = "2" Then
    '                                strFileName = gloSettings.FolderSettings.AppTempFolderPath & "Narration.Txt"
    '                            Else
    '                                strFileName = gloSettings.FolderSettings.AppTempFolderPath & "image.bmp"
    '                            End If
    '                            strData = strFileName

    '                            'Save contents in file
    '                            Dim mstream As ADODB.Stream
    '                            mstream = New ADODB.Stream
    '                            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
    '                            mstream.Open()

    '                            'Check if there are records for selected Node
    '                            mstream.Write(objSQLDataReader.Item(0))
    '                            If System.IO.File.Exists(strFileName) Then
    '                                System.IO.File.Delete(strFileName)
    '                            End If
    '                            mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
    '                            mstream.Close()
    '                            '''''
    '                            flagOthers = objSQLDataReader.Item(1)
    '                        End If
    '                    End While
    '                End If
    '                objSQLDataReader.Close()

    '            ElseIf Left(strFields(nCount), 6) <> "Others" Then
    '                objCmd.Parameters.Clear()
    '                sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar, 5000)
    '                sqlParam.Direction = ParameterDirection.Input
    '                sqlParam.Value = strFields(nCount)

    '                sqlParam = objCmd.Parameters.AddWithValue("@nPatientID", PatientID)
    '                sqlParam.Direction = ParameterDirection.Input

    '                sqlParam = objCmd.Parameters.AddWithValue("@nVisitID", nVisitID)
    '                sqlParam.Direction = ParameterDirection.Input
    '                sqlParam.Value = nVisitID

    '                objCmd.Connection = Con

    '                objSQLDataReader = objCmd.ExecuteReader
    '                If objSQLDataReader.HasRows = True Then
    '                    While objSQLDataReader.Read
    '                        If IsDBNull(objSQLDataReader.Item(0)) = False Then
    '                            If strData = Nothing Then
    '                                strData = objSQLDataReader.Item(0)
    '                                flagOthers = objSQLDataReader.Item(1)
    '                            Else
    '                                strData = strData & Chr(11) & objSQLDataReader.Item(0)
    '                            End If
    '                        End If
    '                    End While
    '                End If
    '                objSQLDataReader.Close()

    '            Else
    '                If InStr(strFields(nCount), "Age") Then
    '                    Dim objCmd1 As New SqlCommand
    '                    Dim objSQLDataReader1 As SqlDataReader
    '                    Dim sqlParam1 As SqlParameter
    '                    objCmd1.CommandType = CommandType.StoredProcedure
    '                    objCmd1.CommandText = "gsp_GetFieldsdata"
    '                    objCmd1.Parameters.Clear()

    '                    objCmd1.CommandText = "gsp_GetDOB"
    '                    objCmd1.Parameters.Clear()
    '                    sqlParam1 = objCmd1.Parameters.AddWithValue("@nPatientID", PatientID)
    '                    sqlParam1.Direction = ParameterDirection.Input

    '                    objCmd1.Connection = Con

    '                    objSQLDataReader1 = objCmd1.ExecuteReader

    '                    If objSQLDataReader1.HasRows = True Then
    '                        objSQLDataReader1.Read()
    '                        If IsDBNull(objSQLDataReader1.Item(0)) = False Then
    '                            dtDOB = objSQLDataReader1.Item(0)
    '                        End If
    '                    End If
    '                    Dim nMonths As Int16
    '                    nMonths = DateDiff(DateInterval.Month, CType(dtDOB, Date), Date.Now.Date)
    '                    strData = nMonths \ 12 & " Yrs " & nMonths Mod 12 & " Months" ' DateDiff(DateInterval.Month, CType(gstrPatientDOB, Date), Date.Now.Date) & " Yrs"
    '                    objSQLDataReader1.Close()
    '                ElseIf InStr(strFields(nCount), "TodayDate") Then
    '                    strData = Now.Date
    '                ElseIf InStr(strFields(nCount), "DOS") Then
    '                    strData = Now.Date
    '                ElseIf InStr(strFields(nCount), "Time") Then
    '                    strData = Format(Now, "Medium Time")
    '                End If
    '            End If
    '        End If
    '        strDataCol.Add(strData & "|" & flagOthers.ToString)
    '    Next
    '    Con.Close()
    '    Return strDataCol
    'End Function

    Public Function getDataReferrals(ByVal PatientID As Long, ByVal strFields As String, ByVal ReferralID As Long, ByVal nVisitID As Long) As String
        Dim strData As String
        Dim dtDOB As Date
        Dim flagOthers As Integer
        Dim strDataCol As String
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader = Nothing
        Dim sqlParam As SqlParameter = Nothing
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_GetFieldsdataForReferrals"
        objCmd.Parameters.Clear()
        'Dim nCount As Int16
        strData = ""
        If strFields <> "" Then
            If InStr(strFields, "Narration") Or InStr(strFields, "FlowSheet") Or InStr(strFields, "imgSignature") Then
                objCmd.Parameters.Clear()
                sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = strFields

                sqlParam = objCmd.Parameters.AddWithValue("@nPatientID", PatientID)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam = objCmd.Parameters.AddWithValue("@nContactID", ReferralID)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ReferralID

                sqlParam = objCmd.Parameters.AddWithValue("@nVisitID", nVisitID)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = nVisitID

                objCmd.Connection = Con

                objSQLDataReader = objCmd.ExecuteReader
                If objSQLDataReader.HasRows = True Then
                    'objSQLDataReader.Read()
                    While objSQLDataReader.Read
                        If IsDBNull(objSQLDataReader.Item(0)) = False Then
                            Dim strFileName As String
                            If objSQLDataReader.Item(1) = "2" Then
                                strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "Narration.Txt"
                            Else
                                strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "image.bmp"
                            End If
                            strData = strFileName

                            'Save contents in file
                            Dim mstream As ADODB.Stream
                            mstream = New ADODB.Stream
                            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
                            mstream.Open()

                            'Check if there are records for selected Node
                            mstream.Write(objSQLDataReader.Item(0))
                            If System.IO.File.Exists(strFileName) Then
                                System.IO.File.Delete(strFileName)
                            End If
                            mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
                            mstream.Close()
                            mstream = Nothing  ''slr free
                            '''''
                            flagOthers = objSQLDataReader.Item(1)
                        End If
                    End While
                End If
                objSQLDataReader.Close()
                objSQLDataReader = Nothing  ''slr free 
            ElseIf Left(strFields, 6) <> "Others" Then
                objCmd.Parameters.Clear()
                sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar, 5000)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = strFields
                sqlParam = objCmd.Parameters.AddWithValue("@nPatientID", PatientID)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam = objCmd.Parameters.AddWithValue("@nContactID", ReferralID)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = ReferralID

                sqlParam = objCmd.Parameters.AddWithValue("@nVisitID", nVisitID)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = nVisitID

                objCmd.Connection = Con

                objSQLDataReader = objCmd.ExecuteReader
                If objSQLDataReader.HasRows = True Then
                    While objSQLDataReader.Read
                        If IsDBNull(objSQLDataReader.Item(0)) = False Then
                            If strData = Nothing Then
                                strData = objSQLDataReader.Item(0)
                                flagOthers = objSQLDataReader.Item(1)
                            Else
                                strData = strData & Chr(11) & objSQLDataReader.Item(0)
                            End If
                        End If
                    End While
                End If
                objSQLDataReader.Close()
                objSQLDataReader = Nothing  ''slr free 
            Else
                If InStr(strFields, "Age") Then
                    Dim objCmd1 As New SqlCommand
                    Dim objSQLDataReader1 As SqlDataReader
                    Dim sqlParam1 As SqlParameter
                    objCmd1.CommandType = CommandType.StoredProcedure
                    objCmd1.CommandText = "gsp_GetFieldsdata"
                    objCmd1.Parameters.Clear()

                    objCmd1.CommandText = "gsp_GetDOB"
                    objCmd1.Parameters.Clear()
                    sqlParam1 = objCmd1.Parameters.AddWithValue("@nPatientID", PatientID)
                    sqlParam1.Direction = ParameterDirection.Input

                    objCmd1.Connection = Con

                    objSQLDataReader1 = objCmd1.ExecuteReader

                    If objSQLDataReader1.HasRows = True Then
                        objSQLDataReader1.Read()
                        If IsDBNull(objSQLDataReader1.Item(0)) = False Then
                            dtDOB = objSQLDataReader1.Item(0)
                        End If
                    End If
                    Dim nMonths As Int16
                    nMonths = DateDiff(DateInterval.Month, CType(dtDOB, Date), Date.Now.Date)
                    strData = nMonths \ 12 & " Yrs " & nMonths Mod 12 & " Months" ' DateDiff(DateInterval.Month, CType(gstrPatientDOB, Date), Date.Now.Date) & " Yrs"
                    '                        strData = "20"
                    objSQLDataReader1.Close()
                    If Not IsNothing(objSQLDataReader1) Then   'Obj Disposed by Mitesh
                        objSQLDataReader1.Dispose()
                        objSQLDataReader1 = Nothing  ''slr free 
                    End If
                    If Not IsNothing(objCmd1) Then
                        objCmd1.Parameters.Clear()
                        objCmd1.Dispose()
                        objCmd1 = Nothing
                    End If
                    sqlParam1 = Nothing
                ElseIf InStr(strFields, "TodayDate") Then
                    strData = Now.Date
                ElseIf InStr(strFields, "DOS") Then
                    strData = Now.Date
                ElseIf InStr(strFields, "Time") Then
                    strData = Format(Now, "Medium Time")
                End If

            End If
        End If
        strDataCol = strData & "|" & flagOthers.ToString
        If Not IsNothing(objSQLDataReader) Then   'Obj Disposed by Mitesh
            objSQLDataReader.Dispose()
            objSQLDataReader = Nothing  ''slr free 
        End If
        If Not IsNothing(objCmd) Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        If Not IsNothing(sqlParam) Then
            sqlParam = Nothing
        End If
        Return strDataCol
    End Function

    Public Function GetPastExams(ByVal ExamId As Long) As DataSet
        Dim Cmd As New SqlCommand
        Dim sParam As New SqlParameter
        Dim da As New SqlDataAdapter(Cmd)
        Dim dsData As New DataSet
        Try
            'Dim objSQLDataReader As SqlDataReader
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.CommandText = "gsp_GetPastExamContents"

        Cmd.Parameters.Clear()
        sParam.ParameterName = "@ExamID"
        sParam.Value = ExamId
        Cmd.Parameters.Add(sParam)
        Cmd.Connection = Con
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
        da.Fill(dsData)

        Return dsData
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dsData) Then    'Obj Disposed by Mitesh 'SLR: Don't dispsose
            '    dsData.Dispose()
            '    dsData = Nothing
            'End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(sParam) Then
                sParam = Nothing
            End If
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If

        End Try
    End Function

    Public Function GetExamContents(ByVal ExamId As Long) As Object
        Dim Cmd As New SqlCommand
        Dim sParam As New SqlParameter
        Try

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
            Return objExamContents
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(sParam) Then    'Obj Disposed by Mitesh
                sParam = Nothing
            End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function

    Public Sub DeleteExam(ByVal ExamID As Long, ByVal ExamName As String, Optional ByVal blnNewExam As Boolean = False)
        Dim cmd As New SqlCommand
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gsp_DeletePatientExam", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.AddWithValue("@Examid", ExamID)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ExamID

            Con.Open()
            cmd.ExecuteNonQuery()
            If blnNewExam = False Then
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Delete, "Patient Exam Deleted", gloAuditTrail.ActivityOutCome.Success)
            End If
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient Exam ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
            If Not IsNothing(sqlParam) Then    'Obj Disposed by Mitesh
                sqlParam = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Function Fill_Exams(ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, Optional ByVal blnFinished As Boolean = False, Optional ByVal ProviderID As Int64 = 0, Optional ByVal speciality As String = Nothing) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        'Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillExams"
        objCmd.CommandTimeout = 0
        objCmd.Connection = objCon
        Dim dsData As New DataSet
        Try


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
                If blnFinished = True Then
                    .Value = 1
                Else
                    .Value = 0
                End If
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objParaFinished)

            Dim objParaDoctorID As New SqlParameter
            With objParaDoctorID
                .ParameterName = "@nProviderID"
                .Value = ProviderID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaDoctorID)

            Dim objSpecialty As New SqlParameter
            With objSpecialty
                .ParameterName = "@Specialty"
                .Value = speciality
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objSpecialty)

            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)

            objDA.Fill(dsData)
            objCon.Close()
            Dim dtData As DataTable = Nothing
            dtData = dsData.Tables(0).Copy()  'SLR: Return a .copy so that whole can be disposed
            If Not IsNothing(objDA) Then
                objDA.Dispose()
                objDA = Nothing
            End If

            objParaFrom = Nothing
            objParaTo = Nothing
            objParaFinished = Nothing
            objParaDoctorID = Nothing

            Return dtData
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString())
            Return Nothing
        Finally
            If Not IsNothing(dsData) Then    'Obj Disposed by Mitesh
                dsData.Dispose()
                dsData = Nothing
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If


        End Try

        ' Return dsData.Tables(0)
    End Function

    '' ---By Mahesh
    '' For PrintAll- FaxAll Report of Selected Patient
    Public Function Fill_AllExams(ByVal PatientID As Long, ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, Optional ByVal IsFinished As Int16 = 2, Optional ByVal TypeWise As Int16 = 0) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim dt As New DataTable
        Try

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
            .Value = TypeWise
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaFlag)

        objCmd.Connection = objCon
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)


        '' Add One Column to Select 
        Dim clmnSelect As New DataColumn
        With clmnSelect
            .ColumnName = "Select"
            .DataType = System.Type.GetType("System.Boolean")
            .DefaultValue = CBool("False")
        End With
        dt.Columns.Add(clmnSelect)
        objDA.Fill(dt)

            If Not IsNothing(objDA) Then  ''slr free objda
                objDA.Dispose()
                objDA = Nothing
            End If

            objParaTo = Nothing
            objParaFinished = Nothing            
            objParaPatientID = Nothing
            objParaFrom = Nothing
            objParaFlag = Nothing

            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString())
            Return Nothing
        Finally
            '  If Not IsNothing(dt) Then    'Obj Disposed by Mitesh  'SLR: Don't dispsoe
            'dt.Dispose()
            'dt = Nothing
            ' End If
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    'For PrintAll- FaxAll Report of All Patients
    Public Function Fill_AllExams_AllPatients(ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, Optional ByVal IsFinished As Int16 = 2, Optional ByVal TypeWise As Int16 = 0, Optional ByVal Gender As String = "") As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim dt As New DataTable
        Try
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
                .Value = TypeWise
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaFlag)

            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)


            Dim clmnSelect As New DataColumn
            With clmnSelect
                .ColumnName = "Select"
                .DataType = System.Type.GetType("System.Boolean")
                .DefaultValue = CBool("False")
            End With
            dt.Columns.Add(clmnSelect)

            objDA.Fill(dt)

            If Not IsNothing(objDA) Then    'Obj Disposed by Mitesh
                objDA.Dispose()
                objDA = Nothing
            End If

            objParaTo = Nothing
            objParaFinished = Nothing                        
            objParaFlag = Nothing
            objParaFrom = Nothing

            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString())
            Return Nothing
        Finally
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            'If Not IsNothing(dt) Then    'Obj Disposed by Mitesh   'SLR: Don't dispsoe
            'dt.Dispose()
            ' dt = Nothing
            ' End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Public Function Fill_AllExams_AllPatients1(ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, Optional ByVal IsFinished As Int16 = 2, Optional ByVal TypeWise As Int16 = 0, Optional ByVal Gender As String = "", Optional ByVal AgeType As Integer = 0, Optional ByVal AgeFrom As Integer = 0, Optional ByVal AgeTo As Integer = 0, Optional ByVal strMedication As String = "") As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim dt As New DataTable
        Try
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

            Dim objDA As New SqlDataAdapter(objCmd)

            Dim clmnSelect As New DataColumn
            With clmnSelect
                .ColumnName = "Select"
                .DataType = System.Type.GetType("System.Boolean")
                .DefaultValue = CBool("False")
            End With
            dt.Columns.Add(clmnSelect)
            objDA.Fill(dt)
            ' objCon.Close()
            ' objCon = Nothing
            If Not IsNothing(objDA) Then
                objDA.Dispose()
                objDA = Nothing
            End If
            ''slr free parameters
            objParaFrom = Nothing
            objParaTo = Nothing
            objParaFinished = Nothing
            objParaFlag = Nothing
            objParaGender = Nothing
            objParaAgeType = Nothing
            objParaAgeFrom = Nothing
            objParaAgeTo = Nothing
            objParaMedication = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString())
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then    'Obj Disposed by Mitesh 'SLR: Don't dispsose 
            '    dt.Dispose()
            '    dt = Nothing
            'End If

            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    '' 20070919 -  From gloCMS
    Public Function getMedicationVisitsAfterDate(ByVal dtDOS As Date, ByVal PatientID As Long) As DataTable
        Dim Cmd As SqlCommand = Nothing
        Dim Param As SqlParameter = Nothing
        Dim dt As New DataTable
        Try

            Cmd = New SqlCommand("gsp_GetMedication_AfterDate", Con)

            Cmd.CommandType = CommandType.StoredProcedure

            Param = New SqlParameter
            Param.ParameterName = "@dtVisitDate"
            Param.Value = dtDOS
            Cmd.Parameters.Add(Param)

            Param = New SqlParameter
            Param.ParameterName = "@PatientID"
            Param.Value = PatientID
            Cmd.Parameters.Add(Param)
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            Cmd.Connection = Con
            Dim da As New SqlDataAdapter(Cmd)

            da.Fill(dt)
            If Not IsNothing(da) Then  ''slr free da
                da.Dispose()
                da = Nothing
            End If
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString())
            Return Nothing
        Finally
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If

            'If Not IsNothing(dt) Then    'Obj Disposed by Mitesh   slr
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(Param) Then
                Param = Nothing
            End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function

    '' 20070919 -  From gloCMS
    ''-- TO RETRIVE MEDICATION OF VISIT AND SAVE IT TO ANOTHER VISIT
    Public Sub RetriveAndSaveMedication(ByVal VisitID_OLD As Long, ByVal VisitID_NEW As Long, ByVal dtDOS As DateTime)
        Dim cmd As New SqlCommand("gsp_InsertMedicationforDOS", Con)
        cmd.CommandType = CommandType.StoredProcedure
        Dim param As SqlParameter = Nothing
        Try

            param = New SqlParameter
            With param
                .ParameterName = "@VisitIdOld"
                .Value = VisitID_OLD
                .SqlDbType = SqlDbType.BigInt
            End With
            cmd.Parameters.Add(param)
            param = Nothing

            param = New SqlParameter
            With param
                .ParameterName = "@VisitIdNew"
                .Value = VisitID_NEW
                .SqlDbType = SqlDbType.BigInt
            End With
            cmd.Parameters.Add(param)
            param = Nothing

            param = New SqlParameter
            With param
                .ParameterName = "@dtDOS"
                .Value = dtDOS
                .SqlDbType = SqlDbType.DateTime
            End With
            cmd.Parameters.Add(param)
            param = Nothing

            param = New SqlParameter
            With param
                .ParameterName = "@dtMedicationdate"
                .Value = Now
                .SqlDbType = SqlDbType.DateTime
            End With
            cmd.Parameters.Add(param)
            param = Nothing

            param = New SqlParameter
            With param
                .ParameterName = "@MachineID"
                param.Value = GetPrefixTransactionID()
                .SqlDbType = SqlDbType.BigInt
            End With
            cmd.Parameters.Add(param)
            param = Nothing

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            cmd.ExecuteNonQuery()
            'cmd = Nothing
            Con.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString())
        Finally
            
            If Not IsNothing(param) Then    'Obj Disposed by Mitesh
                param = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Sub

    Public Function GetEMField(ByVal nTemplateID As Int64) As String
        Dim _Result As Object = Nothing
        Dim Cmd As SqlCommand = Nothing
        Try
            Dim strQRY As String = "SELECT sAssociatedEMName FROM AssociatedEMField WHERE nFieldID = " & nTemplateID & " AND nFieldType = " & FieldType.Tags.GetHashCode() & ""
            Cmd = New SqlCommand(strQRY, Con)
            Con.Open()
            _Result = Cmd.ExecuteScalar()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            Con.Close()
            If Not IsNothing(Cmd) Then    'Obj Disposed by Mitesh
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
        If Not IsNothing(_Result) Then
            Return Convert.ToString(_Result)
        Else
            Return ""
        End If
    End Function

    Public Function GetEMTagsAssociatedFiled(ByVal nTemplatedID As Int64) As DataTable
        Dim _result As DataTable = Nothing  ''slr new not needed 
        Dim oDB As New DataBaseLayer
        Try
            Dim strQRY As String = "SELECT sAssociatedEMName,sAssociatedEMCategory,sStatus FROM AssociatedEMField WHERE nFieldID = " & nTemplatedID & " AND nFieldType = " & FieldType.Tags.GetHashCode() & ""
            _result = oDB.GetDataTable_Query(strQRY)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Template Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(oDB) Then    'Obj Disposed by Mitesh
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
        Return _result
    End Function

    Public Function GetExamAssociatedTag(ByVal _nPatientID As Int64, ByVal _nExamID As Int64) As String

        Dim _Result As Object = Nothing
        Dim Cmd As SqlCommand = Nothing

        Try
            Dim strQRY As String = "SELECT DISTINCT sAssociatedTags FROM LiquidData_DTL WHERE nPatientID = " & _nPatientID & " AND nPrimaryID = " & _nExamID & ""

            '15-Dec-15 Aniket: Resolving Bug #91814: gloEMR > Past Exam - Application takes time to open Past exam from dashboard
            If gblnSAVELIQUIDDATA = True Then
                Cmd = New SqlCommand(strQRY, Con)
                Con.Open()
                _Result = Cmd.ExecuteScalar()
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

        Finally
            Con.Close()
            If Not IsNothing(Cmd) Then    'Obj Disposed by Mitesh
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try

        If Not IsNothing(_Result) Then
            Return Convert.ToString(_Result)
        Else
            Return ""
        End If

    End Function

#Region " Alerts"
    '' 20060719 - CMS
    '' 20060727 - gloEMR Alert [History] 
    Public Function getHistoryVisitsAfterDate(ByVal dtDOS As Date, ByVal PatientID As Long) As DataTable
        Dim Cmd As SqlCommand = Nothing
        Dim Param As SqlParameter = Nothing
        Dim dt As New DataTable
        Try
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
        Cmd = New SqlCommand("gsp_GetHistory_AfterDate", Con)
        Cmd.CommandType = CommandType.StoredProcedure

        Param = New SqlParameter
        Param.ParameterName = "@dtVisitdate"
        Param.Value = dtDOS
        Cmd.Parameters.Add(Param)

        Param = New SqlParameter
        Param.ParameterName = "@PatientID"
        Param.Value = PatientID
        Cmd.Parameters.Add(Param)

           
            Cmd.Connection = Con
         

        Dim da As New SqlDataAdapter(Cmd)

            da.Fill(dt)
            If Not IsNothing(da) Then    'slr free da
                da.Dispose()
                da = Nothing
            End If

            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If Con.State = ConnectionState.Open Then
                Con.Close()

            End If

            'If Not IsNothing(dt) Then    'Obj Disposed by Mitesh
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(Cmd) Then    'Obj Disposed by Mitesh
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            Param = Nothing ''slr free param
        End Try
    End Function

    Public Function getPatientHistory(ByVal PatientID As Long) As DataTable
        Dim Cmd As SqlCommand = Nothing
        Dim Param As SqlParameter = Nothing
        Dim dt As New DataTable
        Try

            Cmd = New SqlCommand("gsp_GetPatientHistory", Con)
            Cmd.CommandType = CommandType.StoredProcedure

            Param = New SqlParameter
            Param.ParameterName = "@PatientID"
            Param.Value = PatientID
            Cmd.Parameters.Add(Param)
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            Cmd.Connection = Con
            Dim da As New SqlDataAdapter(Cmd)

            da.Fill(dt)

            If Not IsNothing(da) Then    'slr free da
                da.Dispose()
                da = Nothing
            End If
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If

            'If Not IsNothing(dt) Then    'Obj Disposed by Mitesh  slr
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(Cmd) Then    'Obj Disposed by Mitesh
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            Param = Nothing   ''slr free Param
        End Try
    End Function


#End Region

#Region " Exam Status Only For CMS"
    '' ---By Mahesh
    Public Function Fill_PatientExamStatus(ByVal dtDate As Date, Optional ByVal Status As MainMenu.enmExamStatus = MainMenu.enmExamStatus.WorkersComp) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        'Dim objSQLDataReader As SqlDataReader
        Dim objParaFrom As SqlParameter = Nothing
        Dim dt As New DataTable
        Try

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ViewPatientExamStatus"
            objCmd.Connection = objCon

            objParaFrom = New SqlParameter
            With objParaFrom
                .ParameterName = "@dtDate"
                .Value = dtDate.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFrom)

            objParaFrom = New SqlParameter
            With objParaFrom
                .ParameterName = "@Status"
                .Value = Status
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaFrom)

            objCmd.Connection = objCon
            If objCon.State = ConnectionState.Closed Then
                objCon.Open()
            End If

            Dim objDA As New SqlDataAdapter(objCmd)



            objDA.Fill(dt)
            ''PatientID, PatientCode, PatientName, ExamName, Status, dtDate

            '  objCon = Nothing
            If Not IsNothing(objDA) Then ''slr free objDA
                objDA.Dispose()
                objDA = Nothing
            End If
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString())
            Return Nothing
        Finally
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If

            'If Not IsNothing(dt) Then    'slr dont dispose it
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(objParaFrom) Then
                objParaFrom = Nothing
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Public Function Fill_Exams(ByVal dtDOS As Date) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objParaFrom As New SqlParameter
        Dim dt As DataTable  ''Slr new not needed 
        Try
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ViewExamsOfDate"
            objCmd.Connection = objCon


            With objParaFrom
                .ParameterName = "@dtDOS"
                .Value = dtDOS.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFrom)

            objCmd.Connection = objCon

            If objCon.State = ConnectionState.Closed Then
                objCon.Open()
            End If

            Dim objDA As New SqlDataAdapter(objCmd)
            Dim dsData As New DataSet
            objDA.Fill(dsData)


            dt = dsData.Tables(0).Copy()
            If Not IsNothing(dsData) Then
                dsData.Dispose()
                dsData = Nothing
            End If

            If Not IsNothing(objDA) Then  ''slr free objDA
                objDA.Dispose()
                objDA = Nothing
            End If
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString())
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then    'Obj Disposed by Mitesh
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objParaFrom) Then
                objParaFrom = Nothing
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Public Function Fill_AppointmentedPatients(ByVal dtAppointmentDate As DateTime, Optional ByVal strProviderName As String = "All", Optional ByVal strLocation As String = "All") As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objParaAppointmentDate As New SqlParameter
        Dim dt As DataTable = Nothing  ''slr new not needed
        Try
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_FillPulledPatients"
            objCmd.Connection = objCon


            With objParaAppointmentDate
                .ParameterName = "@AppointmentDate"
                .Value = dtAppointmentDate.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaAppointmentDate)


            Dim objParaProvider As New SqlParameter
            With objParaProvider
                .ParameterName = "@ProviderName"
                .Value = Trim(strProviderName)
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaProvider)


            Dim objParaLocation As New SqlParameter
            With objParaLocation
                .ParameterName = "@Location"
                .Value = Trim(strLocation)
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaLocation)

            objCmd.Connection = objCon
            If objCon.State = ConnectionState.Closed Then
                objCon.Open()
            End If
            Dim objDA As New SqlDataAdapter(objCmd)
            Dim dsData As New DataSet
            objDA.Fill(dsData)


            dt = dsData.Tables(0).Copy()
            If Not IsNothing(dsData) Then    'Obj Disposed by Mitesh
                dsData.Dispose()
                dsData = Nothing
            End If

            If Not IsNothing(objDA) Then    'slr free objDA
                objDA.Dispose()
                objDA = Nothing
            End If
            objParaAppointmentDate = Nothing  ''slr
            objParaProvider = Nothing
            objParaLocation = Nothing

            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString())
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then    'Obj Disposed by Mitesh  slr
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objParaAppointmentDate) Then
                objParaAppointmentDate = Nothing
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

#End Region

#Region " Functions & Procedures to Lock UnLock Exams"

    Public Function Scan_n_Lock_Exam(ByVal ExamID As Long) As mytable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParamExamID As New SqlParameter
        Dim mydt As New mytable
        Try
            '''' gsp_ScanLockedExam this Stored Procedure Checks if ExamID Exist in LockedExam Table then
            ''''' it Inserts this Exam into the Table 
            '''' Else Return the Corrosponding UserName

            cmd = New SqlCommand("gsp_ScanLockedExam", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParamExamID = cmd.Parameters.AddWithValue("@nExamid", ExamID)
            sqlParamExamID.Direction = ParameterDirection.Input
            sqlParamExamID.Value = ExamID

            Dim sqlParamUserName As New SqlParameter
            sqlParamUserName = cmd.Parameters.Add("@sUserName", SqlDbType.VarChar, 50)
            sqlParamUserName.Direction = ParameterDirection.InputOutput
            sqlParamUserName.Value = gstrLoginName

            Dim sqlParamMachineName As New SqlParameter
            sqlParamMachineName = cmd.Parameters.Add("@sMachineName", SqlDbType.VarChar, 50)
            sqlParamMachineName.Direction = ParameterDirection.InputOutput
            sqlParamMachineName.Value = gstrClientMachineName

            Dim sqlParamIsOpen As New SqlParameter
            sqlParamIsOpen = cmd.Parameters.Add("@bIsOpen", SqlDbType.Bit)
            sqlParamIsOpen.Direction = ParameterDirection.Output


            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            cmd.ExecuteNonQuery()


            If IsDBNull(sqlParamIsOpen.Value) Then
                mydt.Unit = 0
            Else
                If sqlParamIsOpen.Value = False Then
                    mydt.Unit = 0
                Else
                    mydt.Unit = 1
                End If
            End If
            If IsDBNull(sqlParamUserName.Value) Then
                mydt.Code = ""
            Else
                mydt.Code = sqlParamUserName.Value
            End If

            If IsDBNull(sqlParamMachineName.Value) Then
                mydt.Description = ""
            Else
                mydt.Description = sqlParamMachineName.Value
            End If
            ''slr free parameters
            sqlParamExamID = Nothing
            sqlParamUserName = Nothing
            sqlParamMachineName = Nothing
            sqlParamIsOpen = Nothing

            Return mydt

            '' if IsOpen <> 1 i.e Exam is Not Lock
            '' if IsOpen = 1 i.e Exam is Lock

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
            If Not IsNothing(cmd) Then    'Obj Disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            'If Not IsNothing(mydt) Then  ''slr dont free it
            '    mydt = Nothing
            'End If
            If Not IsNothing(sqlParamExamID) Then
                sqlParamExamID = Nothing
            End If
        End Try
    End Function

    Public Function UnLock_Exam(ByVal ExamID As Long, ByVal MachineName As String) As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            '''' IN gsp_UnLockExam Stored Procedure we delete record from "LockedExam" Table 
            '''' where ExamID & UserName Both Mathches

            cmd = New SqlCommand("gsp_UnLockExam", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.AddWithValue("@nExamid", ExamID)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ExamID

            sqlParam = cmd.Parameters.Add("@sMachineName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = MachineName.Trim

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            cmd.ExecuteNonQuery()

            Return True

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally

            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If

            If Not IsNothing(cmd) Then    'Obj Disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
        End Try
    End Function

    Public Function UnLock_AllExam(ByVal MachineName As String) As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            '''' IN gsp_UnLockAllExam Stored Procedure we Update record from PatientExam Table 
            '''' where MachineNAme Mathches

            cmd = New SqlCommand("gsp_UnLockAllExam", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@sMachineName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = MachineName


            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            cmd.ExecuteNonQuery()

            Return True

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally

            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
            If Not IsNothing(cmd) Then    'Obj Disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
        End Try
    End Function

    Public Function Update_UnLock_Exam(ByVal NewUserName As String, ByVal OldUserName As String) As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            '''' In gsp_UpdateLockedExams Stored Procedure we Update record from "LockedExam" Table 
            '''' While User Switching from Lock-Applcation

            cmd = New SqlCommand("gsp_UpdateLockedExams", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@sNewUser", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = NewUserName

            sqlParam = cmd.Parameters.Add("@sOldUser", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = OldUserName

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            cmd.ExecuteNonQuery()

            Return True
        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
            If Not IsNothing(cmd) Then    'Obj Disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
        End Try
    End Function

    '' Mahesh '' 20061113 Used in UnlockExams
    Public Function Fill_LockedExams() As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim dt As New DataTable
        Try
            '''' In gsp_FillLockedExams Stored Procedure we get all Locked Exams 

            cmd = New SqlCommand("gsp_FillLockedExams", Con)
            cmd.CommandType = CommandType.StoredProcedure

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            Dim da As New SqlDataAdapter(cmd)



            Dim clmnSelect As New DataColumn
            With clmnSelect
                .ColumnName = "Select"
                .DataType = System.Type.GetType("System.Boolean")
                .DefaultValue = CBool("False")
            End With
            dt.Columns.Add(clmnSelect)

            da.Fill(dt)

            If Not IsNothing(da) Then    ''SLR
                da.Dispose()
                da = Nothing
            End If
        

            Return dt
        Catch ex1 As SqlException
            MessageBox.Show(ex1.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
            If Not IsNothing(cmd) Then    'Obj Disposed by Mitesh
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            '  If Not IsNothing(dt) Then  'slr
            'dt.Dispose()
            ' dt = Nothing
            'End If
        End Try
    End Function

    Public Function IsExamLocked(ByVal ExamID As Long) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oDBPara As New gloDatabaseLayer.DBParameter
        Dim oDBParas As New gloDatabaseLayer.DBParameters
        Dim dt As DataTable = Nothing
        Try
            '''' gsp_IsExamLocked this Stored Procedure Checks if Exam is Locked
            '''' Return the Corrosponding Lock Status, UserName, Machine Name



            oDBPara.ParameterName = "@nExamid"
            oDBPara.ParameterDirection = ParameterDirection.Input
            oDBPara.Value = ExamID
            oDBParas.Add(oDBPara)
            oDBPara.Dispose()

            oDB.Connect(False)
            oDB.Retrive("gsp_IsExamLocked", oDBParas, dt)
            oDB.Disconnect()

            ' oDBParas.Dispose()
            oDB.Dispose()

            Return dt
            '' if IsOpen <> 1 i.e Exam is Not Lock
            '' if IsOpen = 1 i.e Exam is Lock
        Catch ex As SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then   'Obj Disposed by Mitesh  'SLR
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(oDBParas) Then
                oDBParas.Dispose()
                oDBParas = Nothing
            End If
            If Not IsNothing(oDBPara) Then
                oDBPara.Dispose()
                oDBPara = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function
#End Region

    Public Function GetUnfinshedExams(ByVal PatientId As Int64) As DataTable

        If Not IsDBNull(PatientId) AndAlso PatientId <> 0 Then
            Dim oDB As New DataBaseLayer
            Dim strSQL As String
            Dim oResultTable As DataTable = Nothing  ''SLR New not needed 

            Try
                strSQL = "SELECT TOP (1) nExamID, nVisitID, sExamName, sTemplateName, dtDOS, nProviderID  FROM PatientExams WHERE  bIsFinished = 0 and sPatientNotes is not NULL and nPatientID = " & PatientId & " ORDER BY dtDOS DESC"
                oResultTable = oDB.GetDataTable_Query(strSQL)
                If Not IsNothing(oResultTable) Then
                    If oResultTable.Rows.Count > 0 Then
                        Return oResultTable
                    Else
                        Return Nothing
                    End If
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                Return Nothing
            Finally
                'If Not IsNothing(oResultTable) Then   'slr dont free  it
                '    oResultTable.Dispose()
                '    oResultTable = Nothing
                'End If

                If Not IsNothing(oDB) Then
                    oDB.Dispose()
                    oDB = Nothing
                End If

                strSQL = Nothing

            End Try
        Else
            Return Nothing
        End If
    End Function
    Public Function LoadExamInfo(ByVal nExamId As Long, ByVal nVisitID As Long, ByVal nUserID As Long, ByVal PatientID As Long) As DataSet
        Dim oDB As New DataBaseLayer
        Dim oParameter As New DBParameter
        ' Dim strProviderName As String
        Dim ds As DataSet = Nothing   ''slr new not needed 
        Try
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@nExamId"
            oParameter.Value = nExamId
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing
            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@nVisitID"
            oParameter.Value = nVisitID
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing
            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@nUserID"
            oParameter.Value = nUserID
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@patientID"
            oParameter.Value = PatientID
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            ds = oDB.GetDataSet("gsp_GetAssignProviders")


            Return ds
            

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                oParameter = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            'If IsNothing(ds) = False Then  ''slr dont dispose as returned 
            '    ds.Dispose()
            '    ds = Nothing
            'End If
        End Try
    End Function




    'Fucntion To get All Template Speciality 
    Public Function GetAllTemplateSpecility() As DataTable

        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter = Nothing
        Dim oResultTable As DataTable = Nothing  ''slr new not needed 

        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@CategoryType"
            oParamater.Value = "Template Specialty"
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            oResultTable = oDB.GetDataTable("gsp_FillCategory_MST")

            'Dim r As DataRow
            'r = oResultTable.NewRow
            'r.Item("sDescription") = "<All>"
            'r.Item("nCategoryId") = 0
            'oResultTable.Rows.InsertAt(r, 0)

            If Not oResultTable Is Nothing Then
                Return oResultTable
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Finish, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If IsNothing(oParamater) = False Then
                oParamater = Nothing
            End If
            'If IsNothing(oResultTable) = False Then
            '    oResultTable.Dispose()
            '    oResultTable = Nothing
            'End If
        End Try
    End Function


#Region "Date Mgt Methods"
    'get all the unfinished exams
    Public Function GetAllUnfinishedExams(ByVal ProviderID As Long) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParameter As New DBParameter
        '  Dim strProviderName As String
        Dim oResultTable As DataTable = Nothing ''SLR new not needed
        Try
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@ProviderID"
            oParameter.Value = ProviderID
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing
            ' strProviderName = oDB.GetDataValue("gsp_RetrieveProviderName")
            oResultTable = oDB.GetDataTable("gsp_GetAllUnfinishedExams")
            If Not IsNothing(oResultTable) Then
                If oResultTable.Rows.Count > 0 Then
                    Return oResultTable
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
            Return oResultTable
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                oParameter = Nothing

            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            'If IsNothing(oResultTable) = False Then  --SLR dont dispose as returned
            '    oResultTable.Dispose()
            '    oResultTable = Nothing
            'End If
        End Try

    End Function

    Public Function GetLoginProvUnfinishedExams(ByVal ProviderID As Long, Optional ByVal dtStartDate As String = Nothing, Optional ByVal dtEndDate As String = Nothing, Optional ByVal speciality As String = Nothing) As DataTable
        Dim oDB As New DataBaseLayer
        Dim oParameter As New DBParameter
        '  Dim strProviderName As String
        Dim oResultTable As DataTable = Nothing ''SLR new not needed
        Try
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@ProviderID"
            oParameter.Value = ProviderID
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@Specialty"
            oParameter.Value = speciality
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing
            If Not IsNothing(dtStartDate) Then
                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.DateTime
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@dtStartDOS"
                oParameter.Value = Format(Convert.ToDateTime(dtStartDate), "MM/dd/yyyy")
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing

                oParameter = New DBParameter
                oParameter.DataType = SqlDbType.DateTime
                oParameter.Direction = ParameterDirection.Input
                oParameter.Name = "@dtENDDOS"
                oParameter.Value = Format(Convert.ToDateTime(dtEndDate), "MM/dd/yyyy")
                oDB.DBParametersCol.Add(oParameter)
                oParameter = Nothing
            End If

            ' strProviderName = oDB.GetDataValue("gsp_RetrieveProviderName")
            oResultTable = oDB.GetDataTable("gsp_GetAllUnfinishedExamsDOS")
            If Not IsNothing(oResultTable) Then
                If oResultTable.Rows.Count > 0 Then
                    Return oResultTable
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
            Return oResultTable
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                oParameter = Nothing

            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            'If IsNothing(oResultTable) = False Then  --SLR dont dispose as returned
            '    oResultTable.Dispose()
            '    oResultTable = Nothing
            'End If
        End Try

    End Function

    Public Function GetProviderUnfinishedExams(ByVal ProviderID As Long, Optional ByVal _IsShowAllUnfinishedExams As Boolean = False) As DataTable

        Dim oDB As New DataBaseLayer
        Dim oParameter As DBParameter = Nothing
        ' Dim strProviderName As String
        Dim oResultTable As DataTable = Nothing ''slr new not needed

        Try
            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.BigInt
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@ProviderID"
            oParameter.Value = ProviderID
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.Bit
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@ShowAllunfinishedExans"
            oParameter.Value = _IsShowAllUnfinishedExams
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oResultTable = oDB.GetDataTable("gsp_GetProviderUnfinishedExams_Dashboard")
            If Not IsNothing(oResultTable) Then
                If oResultTable.Rows.Count > 0 Then
                    Return oResultTable
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
            Return oResultTable
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If Not IsNothing(oParameter) Then  'Obj Disposed by Mitesh
                oParameter = Nothing

            End If

            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            'If IsNothing(oResultTable) = False Then
            '    oResultTable.Dispose()
            '    oResultTable = Nothing
            'End If
        End Try
    End Function
#End Region


#Region "Add-Retrieve Methods for 'Specify Provider's Role/Add roles' "

    ''Get existing roles for providers
    Public Function GetProvidersRole(ByVal ExamID As Long) As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim strQry As String = ""
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            strQry = "SELECT ISNULL(nProviderID,0) AS nProviderID , ISNULL(sProviderName,'') AS sName, ISNULL(sLoginName,'') AS sUserName ,ISNULL(sCategory,'') AS sCategory,ISNULL(nExamDetailID,0) AS nExamDetailID  FROM  PatientExam_DTL WHERE  nExamID = " & ExamID & ""
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = strQry

            objCmd.Connection = objCon

            If objCon.State = ConnectionState.Closed Then
                objCon.Open()
            End If

            da = New SqlDataAdapter
            da.SelectCommand = objCmd
            dt = New DataTable
            da.Fill(dt)
            
            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(da) Then  ''slr free da
                da.Dispose()
                da = Nothing
            End If
            'If Not IsNothing(dt) Then   'Obj Disposed by Mitesh  SLR
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    ''Delete roles 
    Public Sub Deleterole(ByVal ExamnId As Int64)

        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Try
            objCon.ConnectionString = GetConnectionString()
            If objCon.State = ConnectionState.Closed Then
                objCon.Open()
            End If
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "DELETE  FROM  PatientExam_DTL WHERE  nExamID = " & ExamnId & ""
            objCmd.Connection = objCon
            If (objCmd.ExecuteNonQuery() > 0) Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Delete, "Provider's role deleted.", 0, ExamnId, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Provider's role deleted.", gloAuditTrail.ActivityOutCome.Success)
            End If

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Success)
            Throw
        Finally
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
    End Sub

    ''Get users 
    Public Function GetUsers() As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim da As SqlDataAdapter
        Dim dt As DataTable = Nothing
        Dim sQLQuery As String = ""
        Try
            sQLQuery = " SELECT	DISTINCT  ISNULL(User_MST.nUserID, 0) AS nUserID, ISNULL(User_MST.sLoginName, '') AS sLoginName," _
                      & "ISNULL(Provider_MST.nProviderID, 0) AS nProviderID, ISNULL(Provider_MST.sFirstName, '') + SPACE(1)  " _
                      & "+ CASE ISNULL(Provider_MST.sMiddleName,'') WHEN  '' THEN '' When Provider_MST.sMiddleName then Provider_MST.sMiddleName + SPACE(1) END +" _
                      & "ISNULL(Provider_MST.sLastName, '') AS sName FROM	User_MST LEFT OUTER JOIN " _
             & " Provider_MST ON User_MST.nProviderID = Provider_MST.nProviderID ORDER BY sLoginName "
            objCon.ConnectionString = GetConnectionString()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = sQLQuery
            objCmd.Connection = objCon
            If objCon.State = ConnectionState.Closed Then
                objCon.Open()
            End If
            da = New SqlDataAdapter
            da.SelectCommand = objCmd
            dt = New DataTable
            da.Fill(dt)
            If Not IsNothing(da) Then 'slr
                da.Dispose()
                da = Nothing
            End If

            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then   'Obj Disposed by Mitesh slr
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    ''Add roles for providers for specified exam
    Public Sub Add(ByVal ExamID As Int64, ByVal ExamdetailID As Int64, ByVal ProviderID As Int64, ByVal ProviderName As String, ByVal UserID As Int64, ByVal UserName As String, ByVal Category As String)
        Dim Con As SqlConnection = Nothing
        Dim conString As String
        Dim sqlParam As SqlParameter = Nothing
        Dim cmd As SqlCommand = Nothing
        Try
            conString = GetConnectionString()
            Con = New SqlConnection(conString)

            cmd = New SqlCommand("gsp_PATIENT_INUP_EXAMDTL", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@nExamID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ExamID

            sqlParam = cmd.Parameters.Add("@nExamDetailID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.InputOutput
            sqlParam.Value = ExamdetailID

            sqlParam = cmd.Parameters.Add("@nProviderID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ProviderID

            sqlParam = cmd.Parameters.Add("@sProviderName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ProviderName

            sqlParam = cmd.Parameters.Add("@nUserId", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = UserID

            sqlParam = cmd.Parameters.Add("@sLoginName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = UserName


            sqlParam = cmd.Parameters.Add("@sCategory", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Category
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            If (cmd.ExecuteNonQuery() > 0) Then
                ExamdetailID = cmd.Parameters("@nExamDetailID").Value
            End If
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "Provider's role added", gloAuditTrail.ActivityOutCome.Success)


        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString())

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw

        Finally
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If

            If Not IsNothing(sqlParam) Then   'Obj Disposed by Mitesh
                sqlParam = Nothing
            End If
            If Not IsNothing(Con) Then
                Con.Dispose()
                Con = Nothing
            End If
            If Not IsNothing(cmd) Then  ''slr free cmd
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Sub

    ''Get all roles
    Public Function GetRoles() As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim _roles As String = "Role"
        Dim dt As DataTable
        Try
            objCon.ConnectionString = GetConnectionString()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = " SELECT ISNULL(sDescription,'') AS Category FROM  Category_MST WHERE sCategoryType='" + _roles + "' "
            objCmd.Connection = objCon
            Dim da As SqlDataAdapter
            If objCon.State = ConnectionState.Closed Then
                objCon.Open()
            End If

            da = New SqlDataAdapter
            da.SelectCommand = objCmd
            dt = New DataTable
            da.Fill(dt)
            Return dt

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing

        Finally
            If objCon.State = ConnectionState.Open Then  ''slr
                objCon.Close()
            End If

            'If Not IsNothing(dt) Then   'Obj Disposed by Mitesh slr
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try

    End Function

#End Region

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
