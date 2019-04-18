Imports System.Data.SqlClient
Imports ADODB
Imports System.IO
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloEmdeonCommon.gloEMRWord
Imports System.Windows.Forms

Public Class clsPatientExams
    Implements IDisposable
    'Private da As SqlDataAdapter
    'Private ds As New DataSet
    'Private dt As DataTable
    'Private dv As DataView
    Private Con As SqlConnection
    'Private conString As String
    'Private _controls As New C1.Win.C1FlexGrid.C1FlexGrid

    Public Sub New()
        Try
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
            gstrMessageBoxCaption = GetMessageBoxCaption()
        Catch ex As SqlException ' Catch the error.
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        End Try
    End Sub

    Public Sub UpdateExamProvider(ByRef _ExamID As Long, ByVal _ProviderId As Long)

        Dim oDB As New DataBaseLayer
        Dim oParameter As DBParameter
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB = Nothing
        End Try

    End Sub
    Public Function GetProviderIdforExam(ByVal Examid As Long) As Long

        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Dim oResult As Object
        Dim ProviderId As Long
        strSQL = "select nProviderId from PatientExams where nExamid = " & Examid
        oResult = oDB.GetRecord_Query(strSQL)

        '' SUDHIR 20090708 ''
        If oResult IsNot Nothing Then
            ProviderId = CType(oResult, Int64)
        End If

        If ProviderId > 0 Then
            Return ProviderId
        Else
            Return 0
        End If

        'If Not IsDBNull(providerId) Then

        '    Return ProviderId
        'Else
        '    Return 0
        'End If

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

    End Function

    Public Function GetReviewerDetails(ByVal Examid As Int64) As DataTable

        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Dim oResult As New DataTable
        Try

            ' strSQL = "select * from PatientExams where nExamid = " & Examid  //Remove select *
            strSQL = "select nExamid from PatientExams where nExamid = " & Examid

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
            oDB = Nothing
        End Try
    End Function

    Public Function GetSeniorProviderIdforExam(ByVal Examid As Int64) As DataTable
        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Dim oResult As New DataTable
        Try

            strSQL = "SELECT distinct ProviderSettings.nOthersID FROM ProviderSettings  INNER JOIN PatientExams ON PatientExams.nProviderID = ProviderSettings.nProviderID and ProviderSettings.sSettingstype='ProviderSeniorAssignment' AND PatientExams.nExamid = " & Examid
            ' strSQL = "SELECT distinct ProviderSettings.nOthersID FROM ProviderSettings where ProviderSettings.nProviderID = " &  & " and sSettingstype='ProviderSeniorAssignment'"
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
            oDB = Nothing

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
            oDB = Nothing
        End Try

    End Function


    Public Sub SetProviderExam(ByVal nPastExamID As Long)
        'Dim clsExam As New clsPatientExams
        'clsExam.Dispose()
        'clsExam = Nothing
        Dim ExamProviderId As Long
        ExamProviderId = GetProviderIdforExam(nPastExamID)
        Dim strProviderName As String
        If ExamProviderId <> 0 Then
            strProviderName = GetProvidernameforExam(ExamProviderId)
        Else
            strProviderName = ""

        End If



        If gnLoginProviderID <> 0 Then
            If gnLoginProviderID = ExamProviderId Then
                Exit Sub
            Else
                ''Prompt User whether to associate the exam to him
                Dim dialogResult As DialogResult
                dialogResult = MessageBox.Show("This notes is documented by '" & strProviderName & "'. Do you want to change the Provider", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If dialogResult = Windows.Forms.DialogResult.Yes Then
                    UpdateExamProvider(nPastExamID, gnLoginProviderID)
                Else
                    Exit Sub

                End If
            End If
        Else
            Exit Sub
        End If
    End Sub


    Public Sub SaveExam(ByRef nExamID As Long, ByVal nVisitID As Long, ByVal nPatinetID As Long, ByVal strExamName As String, ByVal strTempFilePath As String, ByVal isFinished As Byte, ByVal dtDOS As DateTime, ByVal nReviewerId As Int64, ByVal ReviewDate As DateTime, ByVal bIsReviewed As Boolean)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim ExamParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gsp_InUpExam", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Con.Open()

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
                'Dim mstream As ADODB.Stream
                'mstream = New ADODB.Stream
                'mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
                'mstream.Open()
                'mstream.LoadFromFile(strTempFilePath)
                'sqlParam.Value = mstream.Read()
                'mstream.Close()
                Dim objword As New clsWordDocument
                sqlParam.Value = objword.ConvertFiletoBinary(strTempFilePath)
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
            sqlParam.Value = GetPrefixTransactionID(nPatinetID)

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


            cmd.ExecuteNonQuery()
            nExamID = ExamParam.Value


        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If Not IsNothing(ExamParam) Then
                ExamParam = Nothing
            End If
            Con.Close()
        End Try
    End Sub




    Public Sub UpdateExam(ByRef nExamID As Long, ByVal strTempFilePath As String)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim ExamParam As SqlParameter = Nothing

        Try
            cmd = New SqlCommand("gsp_UpdateExam", Con)
            cmd.CommandType = CommandType.StoredProcedure
          
            Con.Open()
            'Exam ID
            'sqlParam = cmd.Parameters.Add("@ExamID", SqlDbType.bigInt)
            'sqlParam.Direction = ParameterDirection.InputOutput
            'sqlParam.Value = nExamID

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
            Con.Close()

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If

            If Not IsNothing(ExamParam) Then
                ExamParam = Nothing
            End If
            Con.Close()
        End Try
    End Sub

    Public Function Fill_Exams(ByVal nPatientID As Long) As DataTable
        Dim cmd As SqlCommand = Nothing
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_GetPastExams", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PatientID", nPatientID)
            Dim da As SqlDataAdapter
            Dim dt As DataTable
            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            objCon.Close()
            objCon.Dispose()
            da.Dispose()
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
        ' Return dt
    End Function

    Public Function Fill_PatientExamHx(ByVal nExamid As Long) As DataTable
        Dim cmd As SqlCommand = Nothing
        Try
            '''''''Added by Ujwala Atre - To maintain Patient Exam History Record - as on 10/02/2010
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            Dim sQry As String
            '' sQry = "select nExamID [Exam ID],sExamName [Exam Name],dtModifieddateTime [Modified DateTime],nUserID [User ID],sUserName [User Name],sComments Comments,sMachineName [Machine Name],sEventName [Event Name]from PatientExamHistory where nUserID= " & gnLoginID & ""
            ''sQry = "select nExamID [Exam ID],sExamName [Exam Name],dtModifieddateTime [Modified DateTime],nUserID [User ID],sUserName [User Name],sComments Comments,sMachineName [Machine Name],sEventName [Event Name]from PatientExamHistory where nUserID= " & gnLoginID & " order by dtModifieddateTime desc"
            sQry = "select nExamID [Exam ID],sExamName [Exam Name],dtModifieddateTime [Modified DateTime],nUserID [User ID],sUserName [User Name],sComments Comments,sMachineName [Machine Name],sEventName [Event Name]from PatientExamHistory where nExamID= " & nExamid & " order by dtModifieddateTime desc"
            cmd = New SqlCommand(sQry, objCon)
            cmd.CommandType = CommandType.Text

            Dim da As SqlDataAdapter
            Dim dt As DataTable
            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            objCon.Close()
            objCon.Dispose()
            da.Dispose()
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return Nothing
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
        '   Return dt
        '''''''Added by Ujwala Atre - To maintain Patient Exam History Record - as on 10/02/2010
    End Function


    Public Function Fill_TemplatesCategory() As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            Dim dt As DataTable
            Dim da As SqlDataAdapter
            'fill data table
            cmd = New SqlCommand("gsp_FillCategory_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = "Template"
            Con.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            Con.Close()
            da.Dispose()
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return Nothing
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If sqlParam IsNot Nothing Then
                sqlParam = Nothing
            End If
        End Try

        'Return dt
    End Function

    Public Function Chk_Reff_PriCarePhycnt(ByVal PatientId As Long) As Integer
        Dim cnt As Object = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam1 As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gsp_Chk_Refer_PriCareCnt", Con)
            'Dim cmd As New SqlCommand("gsp_temp", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Con.Open()
            cmd.Parameters.Clear()
            sqlParam1 = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam1.Direction = ParameterDirection.Input
            sqlParam1.Value = PatientId
            cnt = cmd.ExecuteScalar()

            Con.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If sqlParam1 IsNot Nothing Then
                sqlParam1 = Nothing
            End If
        End Try
        Return Convert.ToInt32(cnt)

    End Function


    Public Function Fill_ExamTemplateNames(ByVal nCategoryID As Long, ByVal nProviderID As Long) As DataTable
        Dim sqlParam1 As SqlParameter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim dt As DataTable = Nothing
        Try

            Dim da As SqlDataAdapter
            cmd = New SqlCommand("gsp_ViewTemplateGallery_MST", Con)
            'Dim cmd As New SqlCommand("gsp_temp", Con)
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
            Con.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If sqlParam1 IsNot Nothing Then
                sqlParam1 = Nothing
            End If
        End Try
        Return dt
    End Function

    Public Function GetTemplateContents(ByVal TemplateId As Long) As DataTable
        Dim Cmd As New SqlCommand
        Dim sParam As New SqlParameter
        Dim dtData As New DataTable
        Try
            'Dim objSQLDataReader As SqlDataReader
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandText = "gsp_GetExamContents"
            Con.Open()
            Cmd.Parameters.Clear()
            sParam.ParameterName = "@nTemplateId"
            sParam.Value = TemplateId
            sParam.SqlDbType = SqlDbType.BigInt
            Cmd.Parameters.Add(sParam)
            'Dim dr As New SqlDataAdapter
            Cmd.Connection = Con
            Dim da As New SqlDataAdapter(Cmd)
            da.Fill(dtData)
            Con.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If sParam IsNot Nothing Then
                sParam = Nothing
            End If
        End Try
        Return dtData
    End Function

    Public Function RetrieveTemplateContents(ByVal TemplateId As Long) As DataTable
        Dim Cmd As New SqlCommand
        Dim sParam As New SqlParameter
        Dim dtData As New DataTable
        Try

            'Dim objSQLDataReader As SqlDataReader
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandText = "gsp_GetExamContents"
            Con.Open()
            Cmd.Parameters.Clear()
            sParam.ParameterName = "@nTemplateId"
            sParam.Value = TemplateId
            sParam.SqlDbType = SqlDbType.BigInt
            Cmd.Parameters.Add(sParam)
            'Dim dr As New SqlDataAdapter
            Cmd.Connection = Con
            Dim da As New SqlDataAdapter(Cmd)
            da.Fill(dtData)
            Con.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If sParam IsNot Nothing Then
                sParam = Nothing
            End If
        End Try
        Return dtData
    End Function

    'Public Function GetTemplateContentsbydt(ByVal TemplateId As Long) As DataTable
    '    Dim Cmd As New SqlCommand
    '    Dim sParam As New SqlParameter
    '    'Dim objSQLDataReader As SqlDataReader
    '    Cmd.CommandType = CommandType.StoredProcedure
    '    Cmd.CommandText = "gsp_GetExamContents"
    '    Con.Open()
    '    Cmd.Parameters.Clear()
    '    sParam.ParameterName = "@nTemplateId"
    '    sParam.Value = TemplateId
    '    Cmd.Parameters.Add(sParam)
    '    'Dim dr As New SqlDataAdapter
    '    Cmd.Connection = Con
    '    Dim da As New SqlDataAdapter(Cmd)
    '    Dim dt As New DataTable
    '    da.Fill(dt)
    '    Con.Close()
    '    Return dt
    'End Function
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
        Dim nNoOfCoverPages As Byte
        Try
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandText = "gsp_CheckFAXCoverPageTemplate"
            Cmd.Parameters.Clear()
            sParam.ParameterName = "@TemplateName"
            sParam.Value = "Cover Page"
            sParam.SqlDbType = SqlDbType.VarChar
            Cmd.Parameters.Add(sParam)
            'Dim dr As New SqlDataAdapter
            Cmd.Connection = Con
            Con.Open()
            nNoOfCoverPages = Cmd.ExecuteScalar
            Con.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If sParam IsNot Nothing Then
                sParam = Nothing
            End If
        End Try
        If nNoOfCoverPages >= 1 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function CheckFAXCoverPageTemplateExists(ByVal enmFAXDocumentType As enmFAXType) As Boolean
        Dim Cmd As New SqlCommand
        Dim sParam As New SqlParameter
        Dim blnTemplateExists As Boolean
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

            'Dim objSQLDataReader As SqlDataReader
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandText = "gsp_CheckFAXCoverPageTemplate"
            Con.Open()
            Cmd.Parameters.Clear()
            sParam.ParameterName = "@TemplateName"
            sParam.Value = strTemplateName
            sParam.SqlDbType = SqlDbType.VarChar
            Cmd.Parameters.Add(sParam)
            'Dim dr As New SqlDataAdapter
            Cmd.Connection = Con
            Dim objSQLReader As SqlDataReader
            objSQLReader = Cmd.ExecuteReader()
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
            Con.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If sParam IsNot Nothing Then
                sParam = Nothing
            End If
        End Try
        Return blnTemplateExists
    End Function
    Public Function GetTemplateContentsForFAX() As Object
        Dim Cmd As New SqlCommand
        Dim sParam As New SqlParameter
        Dim objTemplateContents As Object = Nothing
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
            Con.Open()

            objTemplateContents = Cmd.ExecuteScalar
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If sParam IsNot Nothing Then
                sParam = Nothing
            End If
        End Try
        Return objTemplateContents
    End Function
    Public Function GetTemplateContentsForFAX(ByVal enmFAXDocumentType As enmFAXType) As DataSet
        Dim Cmd As New SqlCommand
        Dim sParam As New SqlParameter
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
            'Dim objSQLDataReader As SqlDataReader
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandText = "gsp_GetExamContentsByTemplateName"
            Con.Open()
            Cmd.Parameters.Clear()
            sParam.ParameterName = "@TemplateName"
            sParam.Value = strTemplateName
            Cmd.Parameters.Add(sParam)
            'Dim dr As New SqlDataAdapter
            Cmd.Connection = Con
            Dim da As New SqlDataAdapter(Cmd)
            da.Fill(dsData)
            Con.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If sParam IsNot Nothing Then
                sParam = Nothing
            End If
        End Try
        Return dsData
    End Function

    'Public Function GenerateVisitID() As Long
    '    Dim cmdVisits As SqlCommand
    '    Dim objParamFlag As SqlParameter
    '    Dim nVisitID As Long
    '    Dim objParam As SqlParameter
    '    cmdVisits = New SqlCommand("gsp_InsertVisits", Con)
    '    cmdVisits.CommandType = CommandType.StoredProcedure
    '    Con.ConnectionString = GetConnectionString()
    '    Con.Open()
    '    objParam = cmdVisits.Parameters.Add("@nPatientID", SqlDbType.Int)
    '    objParam.Direction = ParameterDirection.Input
    '    'objParam.Value = objPrescription.PatientID
    '    objParam.Value = gnPatientID

    '    objParam = cmdVisits.Parameters.Add("@dtVisitdate", SqlDbType.DateTime)
    '    objParam.Direction = ParameterDirection.Input
    '    objParam.Value = Now

    '    'Retrieve Appointment ID
    '    Dim nAppointmentID As Integer
    '    Dim objAppointmentID As New clsAppointments
    '    nAppointmentID = objAppointmentID.GetPatientAppointment(System.DateTime.Now, gnPatientID)
    '    objAppointmentID = Nothing


    '    objParam = cmdVisits.Parameters.Add("@AppointmentID", SqlDbType.Int)
    '    objParam.Direction = ParameterDirection.Input
    '    objParam.Value = nAppointmentID

    '    objParam = cmdVisits.Parameters.Add("@MachineID", SqlDbType.BigInt)
    '    objParam.Direction = ParameterDirection.Input
    '    objParam.Value = GetPrefixTransactionID

    '    objParam = cmdVisits.Parameters.Add("@VisitID", 0)
    '    objParam.Direction = ParameterDirection.Output
    '    'objParam.Value = 0

    '    objParamFlag = cmdVisits.Parameters.Add("@Flag", SqlDbType.Int)
    '    objParamFlag.Direction = ParameterDirection.Output



    '    cmdVisits.ExecuteNonQuery()
    '    nVisitID = objParam.Value
    '    If objParamFlag.Value = 0 Then
    '        Dim objAudit As New clsAudit
    '        objAudit.CreateLog(clsAudit.enmActivityType.Add, "Visit Added on " & CType(Now, String), gstrLoginName, gstrClientMachineName)
    '        Con.Close()
    '    Else
    '        'USe existing visit
    '    End If
    '    Return nVisitID
    'End Function
    'Temporary Commented
    ''Public Function getData(ByVal PatientID As Long, ByVal strFields As Collection, ByVal ExamID As Long) As Collection

    ''    Dim strData As String
    ''    Dim dtDOB As Date
    ''    Dim flagOthers As Integer
    ''    Dim strDataCol As New Collection
    ''    Dim objCmd As New SqlCommand
    ''    Dim objSQLDataReader As SqlDataReader
    ''    Dim sqlParam As SqlParameter
    ''    objCmd.CommandType = CommandType.StoredProcedure
    ''    objCmd.CommandText = "gsp_GetFieldsdata"
    ''    objCmd.Parameters.Clear()
    ''    Con.Open()
    ''    Dim nCount As Int16
    ''    ''nTestTrialID = 0
    ''    For nCount = 1 To strFields.Count
    ''        '' nTestTrialID = nTestTrialID + 1
    ''        strData = ""
    ''        If strFields.Item(nCount) <> "" Then
    ''            If InStr(strFields(nCount), "Narration") Or InStr(strFields(nCount), "imgSignature") Then
    ''                objCmd.Parameters.Clear()
    ''                sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar, 5000)
    ''                sqlParam.Direction = ParameterDirection.Input
    ''                sqlParam.Value = strFields(nCount)

    ''                sqlParam = objCmd.Parameters.Add("@nPatientID", SqlDbType.Int)
    ''                sqlParam.Direction = ParameterDirection.Input
    ''                sqlParam.Value = PatientID

    ''                sqlParam = objCmd.Parameters.Add("@nExamID", SqlDbType.Int)
    ''                sqlParam.Direction = ParameterDirection.Input
    ''                sqlParam.Value = ExamID

    ''                objCmd.Connection = Con

    ''                objSQLDataReader = objCmd.ExecuteReader
    ''                If objSQLDataReader.HasRows = True Then
    ''                    'objSQLDataReader.Read()
    ''                    While objSQLDataReader.Read
    ''                        If IsDBNull(objSQLDataReader.Item(0)) = False Then
    ''                            Dim strFileName As String
    ''                            If objSQLDataReader.Item(1) = "2" Then
    ''                                strFileName = Application.StartupPath & "\Temp\Narration.Txt"
    ''                            Else
    ''                                strFileName = Application.StartupPath & "\Temp\image.bmp"
    ''                            End If
    ''                            strData = strFileName
    ''                            '''''
    ''                            'Save contents in file
    ''                            Dim mstream As ADODB.Stream
    ''                            mstream = New ADODB.Stream
    ''                            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
    ''                            mstream.Open()
    ''                            '       Dim sContents As String
    ''                            'Check if there are records for selected Node
    ''                            mstream.Write(objSQLDataReader.Item(0))
    ''                            If System.IO.File.Exists(strFileName) Then
    ''                                System.IO.File.Delete(strFileName)
    ''                            End If
    ''                            mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
    ''                            mstream.Close()
    ''                            '''''
    ''                            flagOthers = objSQLDataReader.Item(1)
    ''                        End If
    ''                    End While
    ''                End If
    ''                objSQLDataReader.Close()

    ''            ElseIf Left(strFields(nCount), 6) <> "Others" Then
    ''                objCmd.Parameters.Clear()
    ''                sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar, 5000)
    ''                sqlParam.Direction = ParameterDirection.Input
    ''                sqlParam.Value = strFields(nCount)

    ''                sqlParam = objCmd.Parameters.Add("@nPatientID", SqlDbType.Int)
    ''                sqlParam.Direction = ParameterDirection.Input
    ''                sqlParam.Value = PatientID

    ''                sqlParam = objCmd.Parameters.Add("@nExamID", SqlDbType.Int)
    ''                sqlParam.Direction = ParameterDirection.Input
    ''                sqlParam.Value = ExamID

    ''                objCmd.Connection = Con

    ''                objSQLDataReader = objCmd.ExecuteReader
    ''                If objSQLDataReader.HasRows = True Then
    ''                    'objSQLDataReader.Read()
    ''                    While objSQLDataReader.Read
    ''                        If IsDBNull(objSQLDataReader.Item(0)) = False Then
    ''                            If strData = Nothing Then
    ''                                strData = objSQLDataReader.Item(0)
    ''                                flagOthers = objSQLDataReader.Item(1)
    ''                            Else

    ''                                'strData = strData & "" & vbLf & "" & objSQLDataReader.Item(0)
    ''                                strData = strData & Chr(11) & objSQLDataReader.Item(0)
    ''                            End If
    ''                        End If
    ''                    End While
    ''                End If
    ''                objSQLDataReader.Close()

    ''            Else
    ''                If InStr(strFields(nCount), "Age") Then
    ''                    Dim objCmd1 As New SqlCommand
    ''                    Dim objSQLDataReader1 As SqlDataReader
    ''                    Dim sqlParam1 As SqlParameter
    ''                    objCmd1.CommandType = CommandType.StoredProcedure
    ''                    objCmd1.CommandText = "gsp_GetFieldsdata"
    ''                    objCmd1.Parameters.Clear()

    ''                    objCmd1.CommandText = "gsp_GetDOB"
    ''                    objCmd1.Parameters.Clear()
    ''                    sqlParam1 = objCmd1.Parameters.Add("@nPatientID", SqlDbType.Int)
    ''                    sqlParam1.Direction = ParameterDirection.Input
    ''                    sqlParam1.Value = PatientID

    ''                    objCmd1.Connection = Con

    ''                    objSQLDataReader1 = objCmd1.ExecuteReader

    ''                    If objSQLDataReader1.HasRows = True Then
    ''                        objSQLDataReader1.Read()
    ''                        If IsDBNull(objSQLDataReader1.Item(0)) = False Then
    ''                            dtDOB = objSQLDataReader1.Item(0)
    ''                        End If
    ''                    End If
    ''                    Dim nMonths As Int16
    ''                    nMonths = DateDiff(DateInterval.Month, CType(dtDOB, Date), Date.Now.Date)
    ''                    strData = nMonths \ 12 & " Yrs " & nMonths Mod 12 & " Months" ' DateDiff(DateInterval.Month, CType(gstrPatientDOB, Date), Date.Now.Date) & " Yrs"
    ''                    '                        strData = "20"
    ''                    objSQLDataReader1.Close()
    ''                ElseIf InStr(strFields(nCount), "TodayDate") Then
    ''                    strData = Now.Date
    ''                End If
    ''            End If
    ''        End If
    ''        strDataCol.Add(strData & "|" & flagOthers.ToString)
    ''    Next
    ''    Con.Close()
    ''    Return strDataCol
    ''End Function

    Public Function getData(ByVal PatientID As Long, ByVal strFields As Collection, ByVal ExamID As Long, ByVal nVisitID As Long) As Collection

        Dim strData As String
        Dim dtDOB As Date
        Dim flagOthers As Integer
        Dim filecnt As Int16
        Dim strDataCol As New Collection
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        Dim sqlParam As SqlParameter = Nothing
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_GetFieldsdata"
        objCmd.Parameters.Clear()
        Con.Open()
        Dim nCount As Int16
        ''nTestTrialID = 0
        For nCount = 1 To strFields.Count
            '' nTestTrialID = nTestTrialID + 1
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
                        'objSQLDataReader.Read()
                        While objSQLDataReader.Read
                            If IsDBNull(objSQLDataReader.Item(0)) = False Then
                                Dim strFileName As String
                                If objSQLDataReader.Item(1) = "2" Then
                                    filecnt = filecnt + 1
                                    If InStr(strFields(nCount), "Narration") Then
                                        strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "Narration.Txt"
                                    Else
                                        strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "Flowsheet" & filecnt & ".Txt"
                                    End If
                                Else
                                    strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "image.bmp"
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
                                    Dim oWrite As System.IO.StreamWriter
                                    oWrite = File.CreateText(strFileName)
                                    oWrite.WriteLine(strNewString.Item(0))

                                    ''If strNewString.Count > 1 Then
                                    ''    oWrite.WriteLine(strNewString.Item(1))
                                    ''End If
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
                                        'Else
                                        '    oWrite.WriteLine(strNewString.Item(strNewString.Count - 1))
                                    End If
                                    oWrite.Close()
                                    oWrite.Dispose()
                                    oWrite = Nothing
                                    ''''' 20070121 -- By Mahesh
                                    'Call FlowSheet_Of_Visit(strFileName, vbTab, nVisitID)
                                    '''' 
                                End If
                                '''''
                                flagOthers = objSQLDataReader.Item(1)
                            End If
                        End While
                    End If
                    objSQLDataReader.Close()

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
                        'objSQLDataReader.Read()
                        While objSQLDataReader.Read
                            If IsDBNull(objSQLDataReader.Item(0)) = False Then
                                If strData = Nothing Then
                                    strData = objSQLDataReader.Item(0)
                                    flagOthers = objSQLDataReader.Item(1)
                                Else

                                    'strData = strData & "" & vbLf & "" & objSQLDataReader.Item(0)
                                    strData = strData & Chr(11) & objSQLDataReader.Item(0)
                                End If
                            End If
                        End While
                    End If
                    objSQLDataReader.Close()

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
                        If objCmd1 IsNot Nothing Then
                            objCmd1.Parameters.Clear()
                            objCmd1.Dispose()
                            objCmd1 = Nothing
                        End If
                        If sqlParam1 IsNot Nothing Then
                            sqlParam1 = Nothing
                        End If
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
            '''''
            strDataCol.Add(strData & "|" & flagOthers.ToString)

        Next

        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        If sqlParam IsNot Nothing Then
            sqlParam = Nothing
        End If
        Con.Close()
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
        Dim strDataCol As String = ""

        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        Dim sqlParam As SqlParameter = Nothing
        Try

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
                                        strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "Narration.Txt"
                                    Else
                                        strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "Flowsheet" & filecnt & ".Txt"
                                    End If
                                Else
                                    strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "image.bmp"
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
                                If InStr(strFields, "SingleRow") Then
                                    '  Dim oFile As System.IO.File
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
                                    oWrite.Close()
                                    oWrite.Dispose()
                                    oWrite = Nothing
                                End If
                                '''''
                                flagOthers = objSQLDataReader.Item(1)
                            End If
                        End While
                    End If
                    objSQLDataReader.Close()

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
                        'objSQLDataReader.Read()
                        While objSQLDataReader.Read
                            If IsDBNull(objSQLDataReader.Item(0)) = False Then
                                If strData = Nothing Then
                                    strData = objSQLDataReader.Item(0)
                                    flagOthers = objSQLDataReader.Item(1)
                                Else

                                    'strData = strData & "" & vbLf & "" & objSQLDataReader.Item(0)
                                    strData = strData & Chr(11) & objSQLDataReader.Item(0)
                                End If
                            End If
                        End While
                    End If
                    objSQLDataReader.Close()

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
                        '                        strData = "20"
                        objSQLDataReader1.Close()

                        If objCmd1 IsNot Nothing Then
                            objCmd1.Parameters.Clear()
                            objCmd1.Dispose()
                            objCmd1 = Nothing
                        End If
                        If sqlParam1 IsNot Nothing Then
                            sqlParam1 = Nothing
                        End If
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
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If sqlParam IsNot Nothing Then
                sqlParam = Nothing
            End If
        End Try
        Return strDataCol
    End Function

    Public Function getDataOthers(ByVal PatientID As Long, ByVal strFields As Collection, ByVal nVisitID As Long, Optional ByVal nTestID As Long = 0) As Collection
        '''' 20070106 Optional ByVal nTestID As Long = 0
        '' TestID to get the data regarding the paricular test
        ''
        Dim strData As String
        Dim dtDOB As Date
        Dim flagOthers As Integer

        Dim filecnt As Int16
        Dim strDataCol As New Collection
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        Dim sqlParam As SqlParameter = Nothing
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_GetFieldsdataForOthers"
        objCmd.Parameters.Clear()
        Con.Open()
        Dim nCount As Int16
        ''nTestTrialID = 0
        For nCount = 1 To strFields.Count
            '' nTestTrialID = nTestTrialID + 1
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

                    'sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar)
                    'sqlParam.Direction = ParameterDirection.Input
                    'sqlParam.Value = strFields(nCount)

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
                        'objSQLDataReader.Read()
                        While objSQLDataReader.Read
                            If IsDBNull(objSQLDataReader.Item(0)) = False Then
                                Dim strFileName As String
                                If objSQLDataReader.Item(1) = "2" Then
                                    filecnt = filecnt + 1
                                    If InStr(strFields(nCount), "Narration") Then
                                        strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "Narration.Txt"
                                    Else
                                        strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "Flowsheet" & filecnt & ".Txt"
                                    End If
                                Else
                                    strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "image.bmp"
                                End If
                                strData = strFileName
                                '''''
                                'Save contents in file
                                Dim mstream As ADODB.Stream
                                mstream = New ADODB.Stream
                                mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
                                mstream.Open()
                                'Dim sContents As String
                                '''' Check if there are records for selected Node
                                mstream.Write(objSQLDataReader.Item(0))
                                If System.IO.File.Exists(strFileName) Then
                                    System.IO.File.Delete(strFileName)
                                End If
                                mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
                                mstream.Close()
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
                                    Dim oWrite As System.IO.StreamWriter
                                    oWrite = File.CreateText(strFileName)
                                    oWrite.WriteLine(strNewString.Item(0))

                                    ''If strNewString.Count > 1 Then
                                    ''    oWrite.WriteLine(strNewString.Item(1))
                                    ''End If
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
                                        'Else
                                        '    oWrite.WriteLine(strNewString.Item(strNewString.Count - 1))
                                    End If


                                    oWrite.Close() 'SLR to be closed.?
                                    oWrite.Dispose()
                                    oWrite = Nothing
                                    
                                    '                                    oWrite.Close()
                                    ''''' 20070121 -- By Mahesh
                                    ' Call FlowSheet_Of_Visit(strFileName, vbTab, nVisitID)
                                    '''' 
                                End If
                                '''''
                                flagOthers = objSQLDataReader.Item(1)
                            End If
                        End While
                    End If
                    objSQLDataReader.Close()

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
                        'objSQLDataReader.Read()
                        While objSQLDataReader.Read
                            If IsDBNull(objSQLDataReader.Item(0)) = False Then
                                If strData = Nothing Then
                                    strData = objSQLDataReader.Item(0)
                                    flagOthers = objSQLDataReader.Item(1)
                                Else

                                    'strData = strData & "" & vbLf & "" & objSQLDataReader.Item(0)
                                    strData = strData & Chr(11) & objSQLDataReader.Item(0)
                                End If
                            End If
                        End While
                    End If
                    objSQLDataReader.Close()

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
                        '                        strData = "20"
                        objSQLDataReader1.Close()

                        If objCmd1 IsNot Nothing Then
                            objCmd1.Parameters.Clear()
                            objCmd1.Dispose()
                            objCmd1 = Nothing
                        End If
                        If sqlParam1 IsNot Nothing Then
                            sqlParam1 = Nothing
                        End If

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
        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        If sqlParam IsNot Nothing Then
            sqlParam = Nothing
        End If
        Return strDataCol
    End Function

    'Private Function FlowSheet_Of_Visit(ByVal FileName As String, ByVal Delimiter As String, ByVal VisitID As Long)
    '    'Dim oFile As FileStream = New FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.Read)
    '    'Dim oReader As StreamReader = New StreamReader(oFile)

    '    Dim oFile As System.IO.File

    '    Dim oRead As System.IO.StreamReader

    '    Dim LineIn As String
    '    'Dim strNewString As New ArrayList
    '    Dim j As Integer = 0
    '    Dim strROW1() As String
    '    Dim strROW2() As String
    '    Dim oCollection As New Collection
    '    Try
    '        oRead = oFile.OpenText(FileName)

    '        While oRead.Peek <> -1
    '            LineIn = oRead.ReadLine()
    '            If j = 0 Then
    '                '' Get All Column Names
    '                strROW1 = Split(LineIn, Delimiter)
    '            Else
    '                '' Get Data In Collection (Row wise)
    '                strROW2 = Split(LineIn, Delimiter)
    '                oCollection.Add(strROW2)
    '            End If
    '            j = j + 1
    '        End While
    '        ' oRead.Read("", 0, 1)
    '        oRead.Close()

    '        Dim dt As New DataTable
    '        Dim i As Integer = 0
    '        For i = 0 To strROW1.Length - 1
    '            '' Add the Columns to the Table
    '            dt.Columns.Add(strROW1(i))
    '        Next

    '        For i = 1 To oCollection.Count
    '            '' Add Rows to DataTable
    '            dt.Rows.Add(oCollection(i))
    '        Next

    '        '''' Get VisitDate 
    '        Dim VisitDate As Date
    '        VisitDate = GetVisitdate(VisitID)

    '        '' 
    '        Dim dv As New DataView
    '        dv = dt.DefaultView()

    '        '' Get 
    '        dv.RowFilter = dt.Columns(1).ColumnName & " ='" & VisitDate.Date & "'"
    '        'DataGrid1.DataSource = dv

    '        '' Add 
    '        Dim _Flex As New C1.Win.C1FlexGrid.C1FlexGrid
    '        'Me.Controls.Add(_Flex)
    '        With _Flex
    '            _Flex.Rows.Count = 1
    '            _Flex.Cols.Fixed = 0
    '            _Flex.Cols.Count = dv.Table.Columns.Count

    '            For i = 1 To dt.Columns.Count - 1
    '                .SetData(0, i, dt.Columns(i).ColumnName)
    '            Next
    '            '_Flex.DataSource = dv

    '            For i = 1 To dv.Count
    '                .Rows.Add()
    '                For j = 0 To .Cols.Count - 1
    '                    .SetData(i, j, dv.Item(i - 1)(j))
    '                Next
    '            Next

    '            _Flex.SaveGrid(FileName, C1.Win.C1FlexGrid.FileFormatEnum.TextTab, C1.Win.C1FlexGrid.FileFlags.IncludeFixedCells)
    '        End With
    '        _Flex = Nothing
    '        ' Me.Controls.Remove(_Flex)

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try

    'End Function

    Public Function getDataOthers_Old(ByVal PatientID As Long, ByVal strFields As Collection, ByVal nVisitID As Long) As Collection
        '' on 20060328
        Dim strData As String
        Dim dtDOB As Date
        Dim flagOthers As Integer
        Dim strDataCol As New Collection
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        Dim sqlParam As SqlParameter = Nothing
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_GetFieldsdataForOthers"
        objCmd.Parameters.Clear()
        Con.Open()
        Dim nCount As Int16
        ''nTestTrialID = 0
        For nCount = 1 To strFields.Count
            '' nTestTrialID = nTestTrialID + 1
            strData = ""
            If strFields.Item(nCount) <> "" Then
                If InStr(strFields(nCount), "Narration") Or InStr(strFields(nCount), "FlowSheet") Or InStr(strFields(nCount), "imgSignature") Then
                    objCmd.Parameters.Clear()
                    sqlParam = objCmd.Parameters.Add("@sFields", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = strFields(nCount)

                    sqlParam = objCmd.Parameters.AddWithValue("@nPatientID", PatientID)
                    sqlParam.Direction = ParameterDirection.Input

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
                                    strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "Narration.Txt"
                                Else
                                    strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "image.bmp"
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
                                '''''
                                flagOthers = objSQLDataReader.Item(1)
                            End If
                        End While
                    End If
                    objSQLDataReader.Close()

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

                    objCmd.Connection = Con

                    objSQLDataReader = objCmd.ExecuteReader
                    If objSQLDataReader.HasRows = True Then
                        'objSQLDataReader.Read()
                        While objSQLDataReader.Read
                            If IsDBNull(objSQLDataReader.Item(0)) = False Then
                                If strData = Nothing Then
                                    strData = objSQLDataReader.Item(0)
                                    flagOthers = objSQLDataReader.Item(1)
                                Else

                                    'strData = strData & "" & vbLf & "" & objSQLDataReader.Item(0)
                                    strData = strData & Chr(11) & objSQLDataReader.Item(0)
                                End If
                            End If
                        End While
                    End If
                    objSQLDataReader.Close()

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
                        '                        strData = "20"
                        objSQLDataReader1.Close()

                        If objCmd1 IsNot Nothing Then
                            objCmd1.Parameters.Clear()
                            objCmd1.Dispose()
                            objCmd1 = Nothing
                        End If
                        If sqlParam1 IsNot Nothing Then
                            sqlParam1 = Nothing
                        End If

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
        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        If Not IsNothing(sqlParam) Then
            sqlParam = Nothing
        End If
        Return strDataCol
    End Function

    Public Function getDataReferrals(ByVal PatientID As Long, ByVal strFields As String, ByVal ReferralID As Long, ByVal nVisitID As Long) As String
        Dim strData As String
        Dim dtDOB As Date
        Dim flagOthers As Integer
        Dim strDataCol As String
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        Dim sqlParam As SqlParameter = Nothing
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_GetFieldsdataForReferrals"
        objCmd.Parameters.Clear()
        'Con.Open()
        'Dim nCount As Int16
        ''nTestTrialID = 0
        '' nTestTrialID = nTestTrialID + 1
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
                                strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "Narration.Txt"
                            Else
                                strFileName = gloSettings.FolderSettings.AppTempFolderPath & gloGlobal.clsFileExtensions.GetUniqueDateString("yyyyMMddhhmmssmmm") & "image.bmp"
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
                            '''''
                            flagOthers = objSQLDataReader.Item(1)
                        End If
                    End While
                End If
                objSQLDataReader.Close()

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
                    'objSQLDataReader.Read()
                    While objSQLDataReader.Read
                        If IsDBNull(objSQLDataReader.Item(0)) = False Then
                            If strData = Nothing Then
                                strData = objSQLDataReader.Item(0)
                                flagOthers = objSQLDataReader.Item(1)
                            Else

                                'strData = strData & "" & vbLf & "" & objSQLDataReader.Item(0)
                                strData = strData & Chr(11) & objSQLDataReader.Item(0)
                            End If
                        End If
                    End While
                End If
                objSQLDataReader.Close()

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

                    If objCmd1 IsNot Nothing Then
                        objCmd1.Parameters.Clear()
                        objCmd1.Dispose()
                        objCmd1 = Nothing
                    End If
                    If sqlParam1 IsNot Nothing Then
                        sqlParam1 = Nothing
                    End If

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
        'Con.Close()
        If objCmd IsNot Nothing Then
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
        Dim dsData As New DataSet
        Try
            'Dim objSQLDataReader As SqlDataReader
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandText = "gsp_GetPastExamContents"

            Cmd.Parameters.Clear()
            sParam.ParameterName = "@ExamID"
            'sParam.Value = lstPatExams.Items(0).Text
            sParam.Value = ExamId
            Cmd.Parameters.Add(sParam)
            Cmd.Connection = Con
            Dim da As New SqlDataAdapter(Cmd)

            da.Fill(dsData)
            Con.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Not IsNothing(sParam) Then
                sParam = Nothing
            End If
        End Try
        Return dsData
    End Function

    Public Function GetExamContents(ByVal ExamId As Long) As Object
        Dim Cmd As New SqlCommand
        Dim sParam As New SqlParameter
        Dim objExamContents As Object = Nothing
        Try
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.CommandText = "gsp_GetPastExamContents"
            Cmd.Parameters.Clear()
            sParam.ParameterName = "@ExamID"
            sParam.Value = ExamId
            Cmd.Parameters.Add(sParam)
            Cmd.Connection = Con
            Con.Open()
            objExamContents = Cmd.ExecuteScalar()
            Con.Close()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Not IsNothing(sParam) Then
                sParam = Nothing
            End If
        End Try
        Return objExamContents
    End Function

    Public Sub DeleteExam(ByVal ExamID As Long, ByVal ExamName As String, Optional ByVal blnNewExam As Boolean = False)
        Dim cmd As SqlCommand = Nothing
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
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
            Con.Close()
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    'Public Function Fill_Exams(ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, Optional ByVal blnFinished As Boolean = False) As DataTable
    '    Dim objCon As New SqlConnection
    '    objCon.ConnectionString = GetConnectionString()
    '    Dim objCmd As New SqlCommand
    '    'Dim objSQLDataReader As SqlDataReader
    '    objCmd.CommandType = CommandType.StoredProcedure
    '    objCmd.CommandText = "gsp_FillExams"
    '    objCmd.Connection = objCon
    '    Dim dsData As New DataSet
    '    Try


    '        Dim objParaFrom As New SqlParameter
    '        With objParaFrom
    '            .ParameterName = "@FromDate"
    '            .Value = dtFromDate.Date
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.DateTime
    '        End With
    '        objCmd.Parameters.Add(objParaFrom)

    '        Dim objParaTo As New SqlParameter
    '        With objParaTo
    '            .ParameterName = "@ToDate"
    '            .Value = dtToDate.Date
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.DateTime
    '        End With
    '        objCmd.Parameters.Add(objParaTo)

    '        Dim objParaFinished As New SqlParameter
    '        With objParaFinished
    '            .ParameterName = "@Finished"
    '            If blnFinished = True Then
    '                .Value = 1
    '            Else
    '                .Value = 0
    '            End If
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.Bit
    '        End With
    '        objCmd.Parameters.Add(objParaFinished)

    '        Dim objParaDoctorID As New SqlParameter
    '        With objParaDoctorID
    '            .ParameterName = "@nProviderID"
    '            .Value = gnLoginProviderID
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.BigInt
    '        End With
    '        objCmd.Parameters.Add(objParaDoctorID)

    '        objCmd.Connection = objCon
    '        objCon.Open()
    '        Dim objDA As New SqlDataAdapter(objCmd)

    '        objDA.Fill(dsData)
    '        objCon.Close()
    '        objCon = Nothing
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        ' MessageBox.Show(ex.ToString())
    '    End Try

    '    Return dsData.Tables(0)
    'End Function

    '' ---By Mahesh
    '' For PrintAll- FaxAll Report of Selected Patient

    Public Function Fill_AllExams(ByVal PatientID As Long, ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, Optional ByVal IsFinished As Int16 = 2, Optional ByVal TypeWise As Int16 = 0) As DataTable
        Dim objCmd As SqlCommand = Nothing
        Dim objCon As New SqlConnection
        Try
            objCmd = New SqlCommand
            objCon.ConnectionString = GetConnectionString()
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
            objParaPatientID = Nothing


            Dim objParaFrom As New SqlParameter
            With objParaFrom
                .ParameterName = "@FromDate"
                .Value = dtFromDate.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFrom)
            objParaFrom = Nothing


            Dim objParaTo As New SqlParameter
            With objParaTo
                .ParameterName = "@ToDate"
                .Value = dtToDate.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaTo)
            objParaTo = Nothing


            Dim objParaFinished As New SqlParameter
            With objParaFinished
                .ParameterName = "@Finished"
                .Value = IsFinished
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaFinished)
            objParaFinished = Nothing


            Dim objParaFlag As New SqlParameter
            With objParaFlag
                .ParameterName = "@Flag"
                '' 0=Datewise ||||  1=Typewise (Finish/UnFinish)  |||| 2 = All Exams of Patient
                .Value = TypeWise
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaFlag)
            objParaFlag = Nothing


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
            objDA.Dispose()
            clmnSelect = Nothing
          
            objParaFinished = Nothing
            objParaFlag = Nothing
            objParaFrom = Nothing
          
            objParaTo = Nothing

            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            objCon.Close()
            objCon = Nothing
        End Try
        ' Return dt
    End Function

    ' '' ---By Mahesh
    ' '' For PrintAll- FaxAll Report of All Patients
    'Public Function Fill_AllExams_AllPatients(ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, Optional ByVal IsFinished As Int16 = 2, Optional ByVal TypeWise As Int16 = 0) As DataTable
    '    Dim objCon As New SqlConnection
    '    objCon.ConnectionString = GetConnectionString()
    '    Dim objCmd As New SqlCommand
    '    Dim objSQLDataReader As SqlDataReader
    '    objCmd.CommandType = CommandType.StoredProcedure
    '    objCmd.CommandText = "gsp_FillAllPatients_Exams_PrintFAX"
    '    objCmd.Connection = objCon

    '    Dim objParaFrom As New SqlParameter
    '    With objParaFrom
    '        .ParameterName = "@FromDate"
    '        .Value = dtFromDate.Date
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.DateTime
    '    End With
    '    objCmd.Parameters.Add(objParaFrom)

    '    Dim objParaTo As New SqlParameter
    '    With objParaTo
    '        .ParameterName = "@ToDate"
    '        .Value = dtToDate.Date
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.DateTime
    '    End With
    '    objCmd.Parameters.Add(objParaTo)

    '    Dim objParaFinished As New SqlParameter
    '    With objParaFinished
    '        .ParameterName = "@Finished"
    '        .Value = IsFinished
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Int
    '    End With
    '    objCmd.Parameters.Add(objParaFinished)

    '    Dim objParaFlag As New SqlParameter
    '    With objParaFlag
    '        .ParameterName = "@Flag"
    '        '' 0=Datewise ||||  1=Typewise (Finish/UnFinish)  |||| 2 = All Exams of Patient
    '        .Value = TypeWise
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Int
    '    End With
    '    objCmd.Parameters.Add(objParaFlag)

    '    objCmd.Connection = objCon
    '    objCon.Open()
    '    Dim objDA As New SqlDataAdapter(objCmd)

    '    Dim dt As New DataTable
    '    Dim clmnSelect As New DataColumn
    '    With clmnSelect
    '        .ColumnName = "Select"
    '        .DataType = System.Type.GetType("System.Boolean")
    '        .DefaultValue = CBool("False")
    '    End With
    '    dt.Columns.Add(clmnSelect)
    '    '' Add One Column to Select 
    '    'dt.Columns.Add("Select", System.Type.GetType("System.Boolean"))

    '    objDA.Fill(dt)
    '    '' Select, ExamID, PatientName , ExamName, DOS, VisitID, PatientID, PatientCode, DOB, ProviderID, ProviderName, bIsFinished 
    '    objCon.Close()
    '    objCon = Nothing

    '    Return dt
    'End Function

    ' '' ---By Bipin 20070507
    ' '' For PrintAll- FaxAll Report of All Patients
    'Public Function Fill_AllExams_AllPatients(ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, Optional ByVal IsFinished As Int16 = 2, Optional ByVal TypeWise As Int16 = 0, Optional ByVal Gender As String = "") As DataTable
    '    Dim objCon As New SqlConnection
    '    objCon.ConnectionString = GetConnectionString()
    '    Dim objCmd As New SqlCommand
    '    Dim objSQLDataReader As SqlDataReader
    '    objCmd.CommandType = CommandType.StoredProcedure
    '    objCmd.CommandText = "gsp_FillAllPatients_Exams_PrintFAX"
    '    objCmd.Connection = objCon

    '    Dim objParaFrom As New SqlParameter
    '    With objParaFrom
    '        .ParameterName = "@FromDate"
    '        .Value = dtFromDate.Date
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.DateTime
    '    End With
    '    objCmd.Parameters.Add(objParaFrom)

    '    Dim objParaTo As New SqlParameter
    '    With objParaTo
    '        .ParameterName = "@ToDate"
    '        .Value = dtToDate.Date
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.DateTime
    '    End With
    '    objCmd.Parameters.Add(objParaTo)

    '    Dim objParaFinished As New SqlParameter
    '    With objParaFinished
    '        .ParameterName = "@Finished"
    '        .Value = IsFinished
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Int
    '    End With
    '    objCmd.Parameters.Add(objParaFinished)

    '    Dim objParaFlag As New SqlParameter
    '    With objParaFlag
    '        .ParameterName = "@Flag"
    '        '' 0=Datewise ||||  1=Typewise (Finish/UnFinish)  |||| 2 = All Exams of Patient
    '        .Value = TypeWise
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Int
    '    End With
    '    objCmd.Parameters.Add(objParaFlag)

    '    '''''''' modified by Bipin on 20070503 for CCHIT 2007
    '    'Dim objParaGender As New SqlParameter
    '    'With objParaGender
    '    '    .ParameterName = "@Gender"
    '    '    .Value = Gender
    '    '    .Direction = ParameterDirection.Input
    '    '    .SqlDbType = SqlDbType.VarChar
    '    'End With
    '    'objCmd.Parameters.Add(objParaGender)
    '    '''''''''

    '    objCmd.Connection = objCon
    '    objCon.Open()
    '    Dim objDA As New SqlDataAdapter(objCmd)

    '    Dim dt As New DataTable
    '    Dim clmnSelect As New DataColumn
    '    With clmnSelect
    '        .ColumnName = "Select"
    '        .DataType = System.Type.GetType("System.Boolean")
    '        .DefaultValue = CBool("False")
    '    End With
    '    dt.Columns.Add(clmnSelect)
    '    '' Add One Column to Select 
    '    'dt.Columns.Add("Select", System.Type.GetType("System.Boolean"))

    '    objDA.Fill(dt)
    '    '' Select, ExamID, PatientName , ExamName, DOS, VisitID, PatientID, PatientCode, DOB, ProviderID, ProviderName, bIsFinished 
    '    objCon.Close()
    '    objCon = Nothing

    '    Return dt
    'End Function


    'Public Function Fill_AllExams_AllPatients1(ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, Optional ByVal IsFinished As Int16 = 2, Optional ByVal TypeWise As Int16 = 0, Optional ByVal Gender As String = "", Optional ByVal AgeType As Integer = 0, Optional ByVal AgeFrom As Integer = 0, Optional ByVal AgeTo As Integer = 0) As DataTable
    '    'Dim sAgeFrom = AgeType.ToString
    '    'Dim sAgeTo = AgeTo.ToString

    '    Dim objCon As New SqlConnection
    '    objCon.ConnectionString = GetConnectionString()
    '    Dim objCmd As New SqlCommand
    '    Dim objSQLDataReader As SqlDataReader
    '    objCmd.CommandType = CommandType.StoredProcedure
    '    objCmd.CommandText = "gsp_FillAllPatients_Exams_PrintFAX"
    '    objCmd.Connection = objCon

    '    Dim objParaFrom As New SqlParameter
    '    With objParaFrom
    '        .ParameterName = "@FromDate"
    '        .Value = dtFromDate.Date
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.DateTime
    '    End With
    '    objCmd.Parameters.Add(objParaFrom)

    '    Dim objParaTo As New SqlParameter
    '    With objParaTo
    '        .ParameterName = "@ToDate"
    '        .Value = dtToDate.Date
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.DateTime
    '    End With
    '    objCmd.Parameters.Add(objParaTo)

    '    Dim objParaFinished As New SqlParameter
    '    With objParaFinished
    '        .ParameterName = "@Finished"
    '        .Value = IsFinished
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Int
    '    End With
    '    objCmd.Parameters.Add(objParaFinished)

    '    Dim objParaFlag As New SqlParameter
    '    With objParaFlag
    '        .ParameterName = "@Flag"
    '        '' 0=Datewise ||||  1=Typewise (Finish/UnFinish)  |||| 2 = All Exams of Patient
    '        .Value = TypeWise
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Int
    '    End With
    '    objCmd.Parameters.Add(objParaFlag)

    '    '''''''' modified by Bipin on 20070503 for CCHIT 2007
    '    Dim objParaGender As New SqlParameter
    '    With objParaGender
    '        .ParameterName = "@Gender"
    '        .Value = Gender
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaGender)

    '    '' parameter for the AgeType
    '    Dim objParaAgeType As New SqlParameter
    '    With objParaAgeType
    '        .ParameterName = "@AgeType"
    '        .Value = AgeType
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Int
    '    End With
    '    objCmd.Parameters.Add(objParaAgeType)

    '    '' parameter for the From Age
    '    Dim objParaAgeFrom As New SqlParameter
    '    With objParaAgeFrom
    '        .ParameterName = "@AgeFrom"
    '        .Value = AgeFrom
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Int
    '    End With
    '    objCmd.Parameters.Add(objParaAgeFrom)

    '    '' parameter for the To Age
    '    Dim objParaAgeTo As New SqlParameter
    '    With objParaAgeTo
    '        .ParameterName = "@AgeTo"
    '        .Value = AgeTo
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Int
    '    End With
    '    objCmd.Parameters.Add(objParaAgeTo)
    '    '''''''''

    '    'objCmd.Connection = objCon
    '    'objCon.Open()
    '    Dim objDA As New SqlDataAdapter(objCmd)

    '    Dim dt As New DataTable
    '    Dim clmnSelect As New DataColumn
    '    With clmnSelect
    '        .ColumnName = "Select"
    '        .DataType = System.Type.GetType("System.Boolean")
    '        .DefaultValue = CBool("False")
    '    End With
    '    dt.Columns.Add(clmnSelect)
    '    '' Add One Column to Select 
    '    'dt.Columns.Add("Select", System.Type.GetType("System.Boolean"))

    '    objDA.Fill(dt)
    '    '' Select, ExamID, PatientName , ExamName, DOS, VisitID, PatientID, PatientCode, DOB, ProviderID, ProviderName, bIsFinished 
    '    objCon.Close()
    '    objCon = Nothing

    '    Return dt
    'End Function


    '' ---By Bipin 20070509
    '' For PrintAll- FaxAll Report of All Patients

    Public Function Fill_AllExams_AllPatients(ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, Optional ByVal IsFinished As Int16 = 2, Optional ByVal TypeWise As Int16 = 0, Optional ByVal Gender As String = "") As DataTable
        Dim objCmd As SqlCommand = Nothing
        Dim objCon As SqlConnection = Nothing
        Try
            objCmd = New SqlCommand
            objCon = New SqlConnection
            objCon.ConnectionString = GetConnectionString()
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
            objParaFrom = Nothing


            Dim objParaTo As New SqlParameter
            With objParaTo
                .ParameterName = "@ToDate"
                .Value = dtToDate.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaTo)
            objParaTo = Nothing


            Dim objParaFinished As New SqlParameter
            With objParaFinished
                .ParameterName = "@Finished"
                .Value = IsFinished
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaFinished)
            objParaFinished = Nothing



            Dim objParaFlag As New SqlParameter
            With objParaFlag
                .ParameterName = "@Flag"
                '' 0=Datewise ||||  1=Typewise (Finish/UnFinish)  |||| 2 = All Exams of Patient
                .Value = TypeWise
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaFlag)
            objParaFlag = Nothing


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
            clmnSelect = Nothing
       
            objParaFinished = Nothing
            objParaFlag = Nothing
            objParaFrom = Nothing
            objParaTo = Nothing

            Return dt
            '' Select, ExamID, PatientName , ExamName, DOS, VisitID, PatientID, PatientCode, DOB, ProviderID, ProviderName, bIsFinished 
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If (IsNothing(objCon) = False) Then
                objCon.Close()
                objCon.Dispose()
            End If
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
        '  Return dt
    End Function

    Public Function Fill_AllExams_AllPatients1(ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, Optional ByVal IsFinished As Int16 = 2, Optional ByVal TypeWise As Int16 = 0, Optional ByVal Gender As String = "", Optional ByVal AgeType As Integer = 0, Optional ByVal AgeFrom As Integer = 0, Optional ByVal AgeTo As Integer = 0, Optional ByVal strMedication As String = "") As DataTable
        Dim objCmd As SqlCommand = Nothing
        Dim objCon As SqlConnection = Nothing
        Try

            objCmd = New SqlCommand
            objCon = New SqlConnection

            'Dim sAgeFrom = AgeType.ToString
            'Dim sAgeTo = AgeTo.ToString
            objCon.ConnectionString = GetConnectionString()

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
            objParaFrom = Nothing


            Dim objParaTo As New SqlParameter
            With objParaTo
                .ParameterName = "@ToDate"
                .Value = dtToDate.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaTo)
            objParaTo = Nothing


            Dim objParaFinished As New SqlParameter
            With objParaFinished
                .ParameterName = "@Finished"
                .Value = IsFinished
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaFinished)
            objParaFinished = Nothing


            Dim objParaFlag As New SqlParameter
            With objParaFlag
                .ParameterName = "@Flag"
                '' 0=Datewise ||||  1=Typewise (Finish/UnFinish)  |||| 2 = All Exams of Patient
                .Value = TypeWise
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaFlag)
            objParaFlag = Nothing


            '''''''' modified by Bipin on 20070503 for CCHIT 2007
            Dim objParaGender As New SqlParameter
            With objParaGender
                .ParameterName = "@Gender"
                .Value = Gender
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaGender)
            objParaGender = Nothing


            '' parameter for the AgeType
            Dim objParaAgeType As New SqlParameter
            With objParaAgeType
                .ParameterName = "@AgeType"
                .Value = AgeType
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaAgeType)
            objParaAgeType = Nothing



            '' parameter for the From Age
            Dim objParaAgeFrom As New SqlParameter
            With objParaAgeFrom
                .ParameterName = "@AgeFrom"
                .Value = AgeFrom
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaAgeFrom)
            objParaAgeFrom = Nothing


            '' parameter for the To Age
            Dim objParaAgeTo As New SqlParameter
            With objParaAgeTo
                .ParameterName = "@AgeTo"
                .Value = AgeTo
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaAgeTo)
            objParaAgeTo = Nothing


            '' for medication 
            Dim objParaMedication As New SqlParameter
            With objParaMedication
                .ParameterName = "@sMedication"
                .Value = strMedication
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaMedication)
            objParaMedication = Nothing
            '''''''''
            'objCmd.Connection = objCon
            'objCon.Open()
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
            objDA.Dispose()
            clmnSelect = Nothing
            objParaAgeFrom = Nothing
            objParaAgeTo = Nothing
            objParaAgeType = Nothing
            objParaFinished = Nothing
            objParaFlag = Nothing
            objParaFrom = Nothing
            objParaGender = Nothing
            objParaMedication = Nothing
            objParaTo = Nothing

            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If (IsNothing(objCon) = False) Then
                objCon.Close()
                objCon.Dispose()
            End If

            objCon = Nothing
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
        ' Return dt
    End Function

    '' 20070919 -  From gloCMS
    Public Function getMedicationVisitsAfterDate(ByVal dtDOS As Date, ByVal PatientID As Long) As DataTable
        Dim Cmd As SqlCommand = Nothing
        Dim Param As SqlParameter = Nothing
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

            Cmd.Connection = Con
            Dim da As New SqlDataAdapter(Cmd)
            Dim dt As New DataTable
            da.Fill(dt)
            da.Dispose()
            Con.Close()
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Not IsNothing(Param) Then
                Param = Nothing
            End If
        End Try
        ' Return dt
    End Function

    '' 20070919 -  From gloCMS
    ''-- TO RETRIVE MEDICATION OF VISIT AND SAVE IT TO ANOTHER VISIT
    'Public Sub RetriveAndSaveMedication(ByVal VisitID_OLD As Long, ByVal VisitID_NEW As Long, ByVal dtDOS As DateTime)
    '    '   @VisitIdOld		numeric(18,0),
    '    '	@VisitIdNew		numeric(18,0),
    '    '	@dtDOS			datetime,
    '    '	@dtMedicationdate	datetime,
    '    Dim cmd As New SqlCommand("gsp_InsertMedicationforDOS", Con)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    Dim param As SqlParameter

    '    param = New SqlParameter
    '    With param
    '        .ParameterName = "@VisitIdOld"
    '        .Value = VisitID_OLD
    '        .SqlDbType = SqlDbType.BigInt
    '    End With
    '    cmd.Parameters.Add(param)
    '    param = Nothing

    '    param = New SqlParameter
    '    With param
    '        .ParameterName = "@VisitIdNew"
    '        .Value = VisitID_NEW
    '        .SqlDbType = SqlDbType.BigInt
    '    End With
    '    cmd.Parameters.Add(param)
    '    param = Nothing

    '    param = New SqlParameter
    '    With param
    '        .ParameterName = "@dtDOS"
    '        .Value = dtDOS
    '        .SqlDbType = SqlDbType.DateTime
    '    End With
    '    cmd.Parameters.Add(param)
    '    param = Nothing

    '    param = New SqlParameter
    '    With param
    '        .ParameterName = "@dtMedicationdate"
    '        .Value = Now
    '        .SqlDbType = SqlDbType.DateTime
    '    End With
    '    cmd.Parameters.Add(param)
    '    param = Nothing

    '    param = New SqlParameter
    '    With param
    '        .ParameterName = "@MachineID"
    '        param.Value = GetPrefixTransactionID()
    '        .SqlDbType = SqlDbType.BigInt
    '    End With
    '    cmd.Parameters.Add(param)
    '    param = Nothing

    '    If Con.State = ConnectionState.Closed Then
    '        Con.Open()
    '    End If
    '    cmd.ExecuteNonQuery()
    '    cmd = Nothing
    '    Con.Close()
    'End Sub

    '' 20070919 -  From gloCMS
    'Public Function getMedicationOfDate(ByVal VisitID As Long, ByVal dtMedicationDate As Date) As DataTable
    '    Dim Cmd As New SqlCommand("gsp_GetMedication_AfterDate", Con)
    '    Dim Param As SqlParameter
    '    Cmd.CommandType = CommandType.StoredProcedure

    '    Param = New SqlParameter
    '    Param.ParameterName = "@nVisitID"
    '    Param.Value = VisitID
    '    Cmd.Parameters.Add(Param)

    '    Param = New SqlParameter
    '    Param.ParameterName = "@dtMedicationdate"
    '    Param.Value = dtMedicationDate
    '    Cmd.Parameters.Add(Param)

    '    Param = New SqlParameter
    '    Param.ParameterName = "@nPatientID"
    '    Param.Value = gnPatientID
    '    Cmd.Parameters.Add(Param)

    '    Cmd.Connection = Con
    '    Dim da As New SqlDataAdapter(Cmd)
    '    Dim dt As New DataTable
    '    da.Fill(dt)
    '    Con.Close()
    '    Return dt
    'End Function

    Public Function GetExamAssociatedTag(ByVal _nPatientID As Int64, ByVal _nExamID As Int64) As String
        Dim _Result As Object = Nothing
        Dim Cmd As SqlCommand = Nothing
        Try
            Dim strQRY As String = "SELECT DISTINCT sAssociatedTags FROM LiquidData_DTL WHERE nPatientID = " & _nPatientID & " AND nPrimaryID = " & _nExamID & ""
            Cmd = New SqlCommand(strQRY, Con)
            Con.Open()
            _Result = Cmd.ExecuteScalar()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            Con.Close()
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
        Try
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
            Dim dt As New DataTable
            da.Fill(dt)
            da.Dispose()
            Con.Close()
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Not IsNothing(Param) Then
                Param = Nothing
            End If
        End Try
        'Return dt
    End Function


#End Region

#Region " Exam Status Only For CMS"
    '' ---By Mahesh
    Public Enum enmExamStatus
        WorkersComp = 6
    End Enum
    Public Function Fill_PatientExamStatus(ByVal dtDate As Date, Optional ByVal Status As enmExamStatus = enmExamStatus.WorkersComp) As DataTable
        Dim objCmd As SqlCommand = Nothing
        Dim objParaFrom As SqlParameter = Nothing
        Dim dt As New DataTable
        Try
            objCmd = New SqlCommand
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            'Dim objSQLDataReader As SqlDataReader
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
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            objDA.Fill(dt)
            ''PatientID, PatientCode, PatientName, ExamName, Status, dtDate
            objCon.Close()
            objCon = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(objParaFrom) Then
                objParaFrom = Nothing
            End If
        End Try
        Return dt
    End Function

    Public Function Fill_Exams(ByVal dtDOS As Date) As DataTable
        Dim objCmd As SqlCommand = Nothing
        Dim dsData As New DataSet
        Try
            objCmd = New SqlCommand
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            'Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ViewExamsOfDate"
            objCmd.Connection = objCon

            Dim objParaFrom As New SqlParameter
            With objParaFrom
                .ParameterName = "@dtDOS"
                .Value = dtDOS.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFrom)
            objParaFrom = Nothing

            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            objDA.Fill(dsData)
            objCon.Close()
            objCon = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
        Return dsData.Tables(0)
    End Function

    Public Function Fill_AppointmentedPatients(ByVal dtAppointmentDate As DateTime, Optional ByVal strProviderName As String = "All", Optional ByVal strLocation As String = "All") As DataTable
        Dim objCmd As SqlCommand = Nothing
        Dim dsData As New DataSet
        Try
            objCmd = New SqlCommand
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()

            'Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_FillPulledPatients"
            objCmd.Connection = objCon

            Dim objParaAppointmentDate As New SqlParameter
            With objParaAppointmentDate
                .ParameterName = "@AppointmentDate"
                .Value = dtAppointmentDate.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaAppointmentDate)
            objParaAppointmentDate = Nothing

            Dim objParaProvider As New SqlParameter
            With objParaProvider
                .ParameterName = "@ProviderName"
                .Value = Trim(strProviderName)
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaProvider)
            objParaProvider = Nothing


            Dim objParaLocation As New SqlParameter
            With objParaLocation
                .ParameterName = "@Location"
                .Value = Trim(strLocation)
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaLocation)
            objParaLocation = Nothing

            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            objDA.Fill(dsData)
            objCon.Close()
            objCon = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
        Return dsData.Tables(0)

    End Function

#End Region





#Region " Custom Tool Bar Buttons "
    'Public Function Get_ToolBarButtons() As DataTable
    '    Try
    '        'Fill dataTable with selected value for cheklist item
    '        Dim strSQL As String = ""
    '        Dim oDB As New gloStream.gloDataBase.gloDataBase
    '        Dim dt As DataTable
    '        strSQL = " Select  nToolBarButton, nType , IsNull(nUserID,0) as nUserID FROM ToolBarButtons_Rights Where nUserID =" & gnLoginID
    '        oDB.Connect(GetConnectionString)
    '        dt = oDB.ReadQueryDataTable(strSQL)
    '        oDB.Disconnect()
    '        oDB = Nothing
    '        Return dt
    '    Catch ex As SqlException
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Function
#End Region

    Public Function GetUnfinshedExams(ByVal PatientId As Int64) As DataTable

        If Not IsDBNull(PatientId) AndAlso PatientId <> 0 Then
            Dim oDB As New DataBaseLayer
            Dim strSQL As String
            Dim oResultTable As New DataTable

            Try

                'If ProviderID <> 0 Then
                '    strSQL = "SELECT TOP (1) nExamID, nVisitID, sExamName,  dtDOS, nProviderID  FROM PatientExams WHERE  bIsFinished = 0 AND nPatientID = " & PatientId & " AND nProviderID = " & ProviderID & " ORDER BY dtDOS DESC"
                'Else
                strSQL = "SELECT TOP (1) nExamID, nVisitID, sExamName,  dtDOS, nProviderID  FROM PatientExams WHERE  bIsFinished = 0 AND nPatientID = " & PatientId & " ORDER BY dtDOS DESC"
                ' End If
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
                oDB = Nothing
            End Try
        Else
            Return Nothing
        End If
    End Function

    'sarika Date Mgt 20081111
#Region "Date Mgt Methods"
    'get all the unfinished exams
    Public Function GetAllUnfinishedExams(ByVal ProviderID As Long) As DataTable

        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Dim oResultTable As New DataTable

        Try

            'If ProviderID <> 0 Then
            '    strSQL = "SELECT TOP (1) nExamID, nVisitID, sExamName,  dtDOS, nProviderID  FROM PatientExams WHERE  bIsFinished = 0 AND nPatientID = " & PatientId & " AND nProviderID = " & ProviderID & " ORDER BY dtDOS DESC"
            'Else
            If ProviderID <> 0 Then
                strSQL = "SELECT  PatientExams.nExamID AS ExamID, PatientExams.nPatientID as PatientID, Patient.sPatientCode as PatientCode , PatientExams.sExamName AS ExamName, dtDOS as DOS,ISNULL(Patient.sFirstName,'') + space(1) + ISNULL(Patient.sMiddleName,'')+ space(1) + ISNULL(Patient.sLastName,'') as PatientName,'T' AS Flag ,PatientExams.nVisitID AS VisitID,Patient.dtDOB as PatientDOB, '' as  Category   FROM  PatientExams INNER JOIN Patient ON PatientExams.nPatientID = Patient.nPatientID INNER  JOIN Visits ON PatientExams.nVisitID = Visits.nVisitID WHERE isnull( bIsFinished,0) = 0 and PatientExams.nProviderID =" & ProviderID & " ORDER BY dtDOS DESC"
            Else
                strSQL = "SELECT  PatientExams.nExamID AS ExamID, PatientExams.nPatientID as PatientID, Patient.sPatientCode as PatientCode , PatientExams.sExamName AS ExamName, dtDOS as DOS,ISNULL(Patient.sFirstName,'') + space(1) + ISNULL(Patient.sMiddleName,'')+ space(1) + ISNULL(Patient.sLastName,'') as PatientName,'T' AS Flag ,PatientExams.nVisitID AS VisitID,Patient.dtDOB as PatientDOB, '' as  Category   FROM  PatientExams INNER JOIN Patient ON PatientExams.nPatientID = Patient.nPatientID INNER  JOIN Visits ON PatientExams.nVisitID = Visits.nVisitID WHERE isnull( bIsFinished,0) = 0" & " ORDER BY dtDOS DESC"
            End If


            '  End If
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return Nothing
        Finally
            oDB = Nothing
        End Try

    End Function


    ''Get Login Provider's Unfinished exams
    Public Function GetProviderUnfinishedExams(ByVal ProviderID As Long) As DataTable

        Dim oDB As New DataBaseLayer
        Dim strSQL As String
        Dim oResultTable As New DataTable

        Try

            'If ProviderID <> 0 Then
            '    strSQL = "SELECT TOP (1) nExamID, nVisitID, sExamName,  dtDOS, nProviderID  FROM PatientExams WHERE  bIsFinished = 0 AND nPatientID = " & PatientId & " AND nProviderID = " & ProviderID & " ORDER BY dtDOS DESC"
            'Else
            'If ProviderID <> 0 Then
            strSQL = "SELECT  PatientExams.nExamID AS ExamID, PatientExams.nPatientID as PatientID, Patient.sPatientCode as PatientCode , PatientExams.sExamName AS ExamName, dtDOS as DOS,ISNULL(Patient.sFirstName,'') + space(1) + ISNULL(Patient.sMiddleName,'')+ space(1) + ISNULL(Patient.sLastName,'') as PatientName,'T' AS Flag ,PatientExams.nVisitID AS VisitID,Patient.dtDOB as PatientDOB, '' as  Category   FROM  PatientExams INNER JOIN Patient ON PatientExams.nPatientID = Patient.nPatientID INNER  JOIN Visits ON PatientExams.nVisitID = Visits.nVisitID WHERE isnull( bIsFinished,0) = 0 and PatientExams.nProviderID =" & ProviderID & " ORDER BY dtDOS DESC"

            '  End If
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return Nothing
        Finally
            oDB = Nothing
        End Try

    End Function


#End Region
    '----
    ''Sandip Darade 27 Feb 09 
#Region "Add-Retrieve Methods for 'Specify Provider's Role/Add roles' "

    ''Get existing roles for providers
    Public Function GetProvidersRole(ByVal ExamID As Long) As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As SqlCommand = Nothing
        Dim strQry As String = ""

        Try
            objCmd = New SqlCommand
            objCon.ConnectionString = GetConnectionString()
            '      objCmd.CommandType = CommandType.Text
            strQry = "SELECT ISNULL(nProviderID,0) AS nProviderID , ISNULL(sProviderName,'') AS sName, ISNULL(sLoginName,'') AS sUserName ,ISNULL(sCategory,'') AS sCategory,ISNULL(nExamDetailID,0) AS nExamDetailID  FROM  PatientExam_DTL WHERE  nExamID = " & ExamID & ""
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = strQry

            objCmd.Connection = objCon
            Dim da As SqlDataAdapter
            Dim dt As DataTable
            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = objCmd
            dt = New DataTable
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            If objCmd IsNot Nothing Then
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
        Dim objCon As SqlConnection = Nothing
        Dim objCmd As SqlCommand = Nothing
        Try
            objCon = New SqlConnection
            objCmd = New SqlCommand
            objCon.ConnectionString = GetConnectionString()
            objCon.Open()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "DELETE  FROM  PatientExam_DTL WHERE  nExamID = " & ExamnId & ""
            objCmd.Connection = objCon
            If (objCmd.ExecuteNonQuery() > 0) Then
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Provider's role deleted.  ", gstrLoginName, gstrClientMachineName, gnPatientID, False, gloAuditTrail.enmOutCome.Success, gstrMessageBoxCaption)
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Provider's role deleted.", gloAuditTrail.ActivityOutCome.Success)
            End If

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Success)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Provider's role not deleted.  ", gstrLoginName, gstrClientMachineName, gnPatientID, False, gloAuditTrail.enmOutCome.Failure, gstrMessageBoxCaption)
            Throw
        Finally
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Sub

    ''Get users 
    Public Function GetUsers() As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter
        Dim dt As DataTable
        Dim sQLQuery As String = ""
        Try
            objCmd = New SqlCommand
            'sQLQuery = "SELECT ISNULL(nUserID,0) AS nUserID,  ISNULL(sLoginName,'') AS sLoginName, " _
            '       & " (ISNULL(sFirstName,'')  + SPACE(1) + ISNULL(sMiddleName,'')  + SPACE(1) + ISNULL(sLastName,'') )AS sName   FROM  USER_MST"

            sQLQuery = " SELECT	DISTINCT  ISNULL(User_MST.nUserID, 0) AS nUserID, ISNULL(User_MST.sLoginName, '') AS sLoginName,ISNULL(Provider_MST.nProviderID, 0) AS nProviderID, ISNULL(Provider_MST.sFirstName, '') + SPACE(1)  " _
            & "+ ISNULL(Provider_MST.sMiddleName, '') + SPACE(1) + ISNULL(Provider_MST.sLastName, '') AS sName FROM	User_MST LEFT OUTER JOIN " _
             & " Provider_MST ON User_MST.nProviderID = Provider_MST.nProviderID ORDER BY sLoginName "
            objCon.ConnectionString = GetConnectionString()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = sQLQuery
            objCmd.Connection = objCon

            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = objCmd
            dt = New DataTable
            da.Fill(dt)
            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw
            Return Nothing
        Finally
            If objCmd IsNot Nothing Then
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
        Dim cmd As SqlCommand = Nothing
        Dim conString As String
        Dim sqlParam As SqlParameter = Nothing
        Try
            '@nExamID,@ID,@nProviderID,@sProviderName,@sCategory)
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
            Con.Open()
            If (cmd.ExecuteNonQuery() > 0) Then
            End If
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, "Provider's role added", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Intervention, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString())
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.PatientDetail, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'gloAuditTrail.gloAuditTrail.CreateAuditLog( (gloAuditTrail.enmActivityType.Add, "Provider's role not added.  ", gstrLoginName, gstrClientMachineName, gnPatientID, False, gloAuditTrail.enmOutCome.Failure, gstrMessageBoxCaption)
            Throw
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
            'Con.Close()
            If Not IsNothing(Con) Then
                Con.Dispose()
                Con = Nothing
            End If
        End Try
    End Sub

    ''Get all roles
    Public Function GetRoles() As DataTable
        Dim objCon As SqlConnection = Nothing
        Dim objCmd As SqlCommand = Nothing
        Dim _roles As String = "Role"
        Try
            objCon = New SqlConnection
            objCmd = New SqlCommand
            objCon.ConnectionString = GetConnectionString()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = " SELECT ISNULL(sDescription,'') AS Category FROM  Category_MST WHERE sCategoryType='" + _roles + "' "
            objCmd.Connection = objCon
            Dim da As SqlDataAdapter
            Dim dt As DataTable
            objCon.Open()
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
            If objCmd IsNot Nothing Then
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
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            If IsNothing(Con) = False Then
                Con.Dispose() : Con = Nothing
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
