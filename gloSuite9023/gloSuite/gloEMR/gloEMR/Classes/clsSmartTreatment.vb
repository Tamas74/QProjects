Imports System.Data.SqlClient

Public Class clsSmartTreatment

    Public Sub New(ByVal PatientID As Long)
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
        _PatientID = PatientID
    End Sub
    Public Sub Dispose()

        If Conn IsNot Nothing Then
            Conn.Dispose()
            Conn = Nothing

        End If
        'If IsNothing(dv) = False Then
        '    dv.Dispose()
        '    dv = Nothing
        'End If
        'If IsNothing(Ds) = False Then
        '    Ds.Dispose()
        '    Ds = Nothing
        'End If
    End Sub
    Private Conn As SqlConnection
    'Private Dv As DataView
    'Private Cmd As System.Data.SqlClient.SqlCommand
    ' Private ArrMedicationCol As New ArrayList
    ' Dim arrDruglist As New ArrayList  '' For Priscription
    Dim _PatientID As Long
    Public dtDiagnosis As DataTable = Nothing
    Public dtTreatment As DataTable = Nothing
    Public dtDrugs As DataTable = Nothing

    Public Function ScanDiagnosis(ByVal ExamID As Long, ByVal VisitID As Long) As DataTable
        Dim Cmd As System.Data.SqlClient.SqlCommand = Nothing
        Try

            Dim adpt As New SqlDataAdapter
            Dim dt As New DataTable

            Cmd = New SqlCommand("gsp_scanDiagnosis", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objparam As SqlParameter

            objparam = Cmd.Parameters.Add("@nExamId", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = ExamID

            objparam = Cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = VisitID

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            adpt.SelectCommand = Cmd
            adpt.Fill(dt)
            adpt.Dispose()
            adpt = Nothing
            objparam = Nothing
            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            Conn.Close()

            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function

    Public Function ScanTreatment(ByVal ExamID As Long) As DataTable
        Dim Cmd As System.Data.SqlClient.SqlCommand = Nothing
        Try
            Dim adpt As New SqlDataAdapter
            Dim dt As New DataTable

            Cmd = New SqlCommand("gsp_scanTreatment", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objparam As SqlParameter

            objparam = Cmd.Parameters.Add("@nExamId", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = ExamID

            objparam = Cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = ""


            objparam = Cmd.Parameters.Add("@ICD9Desc", SqlDbType.VarChar)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = ""

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            objparam = Nothing
            adpt.SelectCommand = Cmd
            adpt.Fill(dt)
            adpt.Dispose()
            adpt = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.Message)
            Return Nothing
        Finally
            Conn.Close()
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function

    Public Function AddDiagnosis(ByVal ExamId As Long, ByVal VisitID As Long, ByVal arrlist As ArrayList)

        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If

        Dim trSmartDiagnosis As SqlTransaction
        trSmartDiagnosis = Conn.BeginTransaction
        'Dim cmddelete As SqlCommand = Nothing
        Dim objparam As SqlParameter
        Try
            Dim i As Integer


            ''''
            ''''''''''''''''''''  To Delete Diagnosis 
            'cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteDiagnosis", Conn)
            'cmddelete.CommandType = CommandType.StoredProcedure
            'cmddelete.Transaction = trSmartDiagnosis

            'objparam = cmddelete.Parameters.Add("@nExamId", SqlDbType.Int)
            'objparam.Direction = ParameterDirection.Input
            'objparam.Value = ExamID

            'If Conn.State = ConnectionState.Closed Then
            '    Conn.Open()
            'End If
            'cmddelete.ExecuteNonQuery()
            'cmddelete = Nothing
            'cmddelete.Parameters.Clear()
            ''''

            ''''''''''''''''''''  To Add Diagnosis 

            For i = 0 To arrlist.Count - 1
                Dim objmylist As mytable
                objmylist = CType(arrlist.Item(i), mytable)

                Dim Cmd As System.Data.SqlClient.SqlCommand = Nothing
                Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertDiagnosis", Conn)

                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Transaction = trSmartDiagnosis
                ' Dim ICD9Desc As Array
                objparam = Cmd.Parameters.Add("@nExamId", SqlDbType.BigInt)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = ExamId

                objparam = Cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = VisitID

                objparam = Cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = _PatientID

                objparam = Cmd.Parameters.Add("@sICD9Code", SqlDbType.VarChar)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = objmylist.Code

                objparam = Cmd.Parameters.Add("@sICD9Description", SqlDbType.VarChar)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = objmylist.Description

                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If
                Cmd.ExecuteNonQuery()
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            Next
            trSmartDiagnosis.Commit()
            Conn.Close()
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)
            'trMedication.Rollback()
            trSmartDiagnosis.Rollback()
            trSmartDiagnosis = Nothing
            '  Cmd = Nothing
            'cmddelete = Nothing
            Conn.Close()
            Return False
        Finally
            If trSmartDiagnosis IsNot Nothing Then

                trSmartDiagnosis.Dispose()
                trSmartDiagnosis = Nothing
            End If
            'If Cmd IsNot Nothing Then
            '    Cmd.Parameters.Clear()
            '    Cmd.Dispose()
            '    Cmd = Nothing
            'End If
            objparam = Nothing
        End Try
    End Function



    Public Function FetchSmartTreatment() As DataTable
        ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101013
        Dim _query As String
        Dim dt As DataTable
        Dim objSDA As New SqlDataAdapter
        '  Dim _Result As Object


        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If
        ''_query = "select nUserID, sFieldName, bFieldStatus, nFieldSequence, sFieldType, bSendTask from SmartConfig where nUserId='" & gnLoginID & "' AND sFieldType = 'SmartCPT' Order by nFieldSequence"
        _query = "select ISNULL(nUserID,0) AS nUserID, ISNULL(sFieldName,'') AS sFieldName, ISNULL(bFieldStatus,'true') AS bFieldStatus, ISNULL(nFieldSequence,0) AS nFieldSequence, ISNULL(sFieldType,'') AS sFieldType, ISNULL(bSendTask,'false') AS bSendTask,ISNULL(sTaskusers,'') as sTaskusers,ISNULL(bAllowviewtsk,0) as bAllowviewtsk,ISNULL(sUserID,'') as sUserID  from SmartConfig where nUserId='" & gnLoginID & "' AND sFieldType = 'SmartCPT' Order by nFieldSequence"
        objSDA = New SqlDataAdapter(_query, Conn)
        dt = New DataTable()
        objSDA.Fill(dt)
        objSDA.Dispose()
        objSDA = Nothing
        Return dt
        ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101013
    End Function

    Public Function Check_Existence(ByVal dt As DataTable, ByVal strCode As String, ByVal strDescription As String) As Integer
        ''dt(0)= Code
        ''dt(1)= Description

        '' Initialize Function to Default Value 
        Check_Existence = -1

        If IsNothing(dt) = False Then
            Dim i As Integer
            For i = 0 To dt.Rows.Count - 1
                '''' first Check the Code if found same code then check the Description
                If Trim(dt.Rows(i)(0).ToString) = Trim(strCode) Then
                    If Trim(dt.Rows(i)(1).ToString) = Trim(strDescription) Then
                        '' When Record found in the Table dt Return the row Index
                        Return i
                        '''' Else Return -1 (Default Value) i.e Code-Description DOES NOT Exist in dt
                    End If
                End If
            Next
        End If

    End Function
    '' To Fetch All CPTs 
    Public Function FillAssociatedCPT(Optional ByVal Flag As Int16 = 0) As DataTable
        Dim adpt As New SqlDataAdapter
        Dim dt As New DataTable

        Dim Cmd As SqlCommand = New SqlCommand("gsp_FillAssociatedCPT", Conn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim objparam As SqlParameter
        objparam = Cmd.Parameters.Add("@Flag", SqlDbType.Int)
        objparam.Direction = ParameterDirection.Input
        objparam.Value = Flag
        '''''' if Flag=0 then Orderby CPTCode
        ''''''''Else Orderby Description

        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If
        adpt.SelectCommand = Cmd

        adpt.Fill(dt)
        Conn.Close()
        If Cmd IsNot Nothing Then
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
        End If
        objparam = Nothing
        adpt.Dispose()
        adpt = Nothing
        Return dt

        'Dim dreader As SqlDataReader
        'Conn.Open()
        'dreader = Cmd.ExecuteReader()

        'Do While dreader.Read
        '    Dim i As Integer
        '    i = dreader("nSpecialtyID")

        'Loop
    End Function

    Public Function AddData(ByVal ExamId As Long, ByVal VisitID As Long, ByVal arrlist As ArrayList, ByVal ICD9 As String, ByVal arrDruglist As ArrayList, ByVal PatientID As Long, ByVal oParentForm As Form) As Boolean
        ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101013

        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If
        Dim trSmartDiagnosis As SqlTransaction
        trSmartDiagnosis = Conn.BeginTransaction
        Dim objparam As SqlParameter
        Try
            Dim i As Integer

            Dim PE_TemplateID As New ArrayList
            Dim ArrIndex As Int16 = 0

            For i = 0 To arrlist.Count - 1
                Dim objmylist As myList
                objmylist = CType(arrlist.Item(i), myList)

                ''''Insert data in Diagnosis =5
                ' If objmylist.ID = 5 Then

                Dim ICD9Desc As Array
                ICD9Desc = Splittext(objmylist.HistoryCategory)
                If (IsNothing(dtDiagnosis)) Then
                    dtDiagnosis = New DataTable
                End If
                If Check_Existence(dtDiagnosis, ICD9Desc(0), ICD9Desc(1)) = -1 Then
                    '' Diagnosis is Not Exists
                    '' Insert ICD9 data in Diagnosis
                    Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_InsertDiagnosis", Conn)
                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Transaction = trSmartDiagnosis

                    objparam = Cmd.Parameters.Add("@nExamId", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = ExamId

                    objparam = Cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = VisitID

                    objparam = Cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                    objparam.Direction = ParameterDirection.Input
                    objparam.Value = PatientID

                    objparam = Cmd.Parameters.Add("@sICD9Code", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    If UBound(ICD9Desc) >= 1 Then
                        objparam.Value = ICD9Desc(0)
                    End If

                    objparam = Cmd.Parameters.Add("@sICD9Description", SqlDbType.VarChar)
                    objparam.Direction = ParameterDirection.Input
                    If UBound(ICD9Desc) >= 1 Then
                        objparam.Value = ICD9Desc(1)
                    End If

                    If Conn.State = ConnectionState.Closed Then
                        Conn.Open()
                    End If
                    Cmd.ExecuteNonQuery()
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing
                    ''''''''''''''''''''

                    Dim r As DataRow
                    If (IsNothing(dtDiagnosis)) Then
                        dtDiagnosis = New DataTable
                    End If
                    r = dtDiagnosis.NewRow
                    r.Item(0) = ICD9Desc(0)
                    r.Item(1) = ICD9Desc(1)
                    dtDiagnosis.Rows.Add(r)
                End If

                ''''Insert data in CPT =1
                If objmylist.ID = 1 Then

                    Dim CPTDesc As Array
                    CPTDesc = Splittext(objmylist.Description)
                    If (IsNothing(dtTreatment)) Then
                        dtTreatment = New DataTable
                    End If
                    If Check_Existence(dtTreatment, CPTDesc(0), CPTDesc(1)) = -1 Then

                        Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_InsertTreatment", Conn)

                        Cmd.CommandType = CommandType.StoredProcedure
                        Cmd.Transaction = trSmartDiagnosis

                        objparam = Cmd.Parameters.Add("@nExamId", SqlDbType.BigInt)
                        objparam.Direction = ParameterDirection.Input
                        objparam.Value = ExamId

                        objparam = Cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
                        objparam.Direction = ParameterDirection.Input
                        objparam.Value = VisitID

                        objparam = Cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
                        objparam.Direction = ParameterDirection.Input
                        objparam.Value = PatientID

                        objparam = Cmd.Parameters.Add("@sCPTcode", SqlDbType.VarChar)
                        objparam.Direction = ParameterDirection.Input

                        If UBound(CPTDesc) >= 1 Then
                            objparam.Value = CPTDesc(0)
                        End If

                        objparam = Cmd.Parameters.Add("@sCPTDescription", SqlDbType.VarChar)
                        objparam.Direction = ParameterDirection.Input

                        If UBound(CPTDesc) >= 1 Then
                            objparam.Value = CPTDesc(1).ToString
                        End If

                        If Conn.State = ConnectionState.Closed Then
                            Conn.Open()
                        End If
                        Cmd.ExecuteNonQuery()
                        Cmd.Parameters.Clear()
                        Cmd.Dispose()
                        Cmd = Nothing
                        Dim r As DataRow
                        If (IsNothing(dtTreatment)) Then
                            dtTreatment = New DataTable
                        End If
                        r = dtTreatment.NewRow
                        r.Item(0) = CPTDesc(0)
                        r.Item(1) = CPTDesc(1)
                        dtTreatment.Rows.Add(r)

                    End If

                    ''''Insert data in Drugs = 2
                ElseIf objmylist.ID = 2 Then


                    '''''Insert data in Patient Education =3
                ElseIf objmylist.ID = 3 Then

                    'PE_TemplateID.SetValue(objmylist.Index, ArrIndex - 1)
                    PE_TemplateID.Add(objmylist.Index)
                    ArrIndex = ArrIndex + 1

                    ''''Insert data in Tags  =4
                ElseIf objmylist.ID = 4 Then

                End If

                '                Cmd.Parameters.Clear()
            Next

            '' if there exits Templates for patient education in assocated ICD9 
            If PE_TemplateID.Count > 0 Then
                '''' to Show Patient Education Form
                Dim objfrmpatienteducation As New frmPatientEducation(VisitID, PatientID, ExamId, PE_TemplateID)
                objfrmpatienteducation.ShowDialog(oParentForm)
                objfrmpatienteducation.Close()
                objfrmpatienteducation.Dispose()
                objfrmpatienteducation = Nothing
            End If

            '' To Show Priscription Form 
            'Dim arrDruglist As New ArrayList
            If arrDruglist.Count > 0 Then
                Dim objfrmprescription As frmPrescription = Nothing
                objfrmprescription = frmPrescription.GetInstance(arrDruglist, VisitID, PatientID)
                If IsNothing(objfrmprescription) = True Then
                    AddData = Nothing
                    Exit Function
                End If

                objfrmprescription.WindowState = FormWindowState.Maximized
                objfrmprescription.ShowDialog(oParentForm)
                objfrmprescription.Dispose()
                objfrmprescription = Nothing
            End If

            trSmartDiagnosis.Commit()

            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            'trMedication.Rollback()
            trSmartDiagnosis.Rollback()
            Return False
        Finally
            If trSmartDiagnosis IsNot Nothing Then

                trSmartDiagnosis.Dispose()
                trSmartDiagnosis = Nothing
            End If
            trSmartDiagnosis = Nothing
            'If Cmd IsNot Nothing Then
            '    Cmd.Parameters.Clear()
            '    Cmd.Dispose()
            '    Cmd = Nothing
            'End If
            'objparam = Nothing
            Conn.Close()
        End Try
        ''''''''''''''' Added by Ujwala - Smart Treatment Changes Integration - as on 20101013
    End Function


    Public Function FetchCPTforUpdate(ByVal CPTID As Long) As DataTable
        Try
            Dim dt As New DataTable
            Dim sqladpt As New SqlDataAdapter

            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_ScanCPTICD9Association", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = Cmd

            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@CPTID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CPTID

            sqladpt.Fill(dt)
            Conn.Close()
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
            sqladpt.Dispose()
            sqladpt = Nothing

            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'MsgBox(ex.Message)
            'Conn.Close()
            Return Nothing
        End Try
    End Function

    Public Function FetchDrugsFromPrescription(ByVal VisitID As Long, ByVal PriscriptionDate As DateTime) As DataTable
        Try
            Dim dt As New DataTable
            Dim sqladpt As New SqlDataAdapter

            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_GetPrescriptionforSmartDiag", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = Cmd

            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitID

            objParam = Cmd.Parameters.Add("@dtPrescriptiondate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = PriscriptionDate

            sqladpt.Fill(dt)
            Conn.Close()
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            sqladpt.Dispose()
            sqladpt = Nothing
            objParam = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'MsgBox(ex.Message)
            'Conn.Close()
            Return Nothing
        End Try
    End Function

    Public Function FetchPatientEducation(ByVal VisitID As Long) As DataTable
        Try
            Dim dt As New DataTable
            Dim sqladpt As New SqlDataAdapter

            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_ScanPatientEducation", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = Cmd

            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitID

            sqladpt.Fill(dt)
            Conn.Close()
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
            sqladpt.Dispose()
            sqladpt = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'MsgBox(ex.Message)
            'Conn.Close()
            Return Nothing
        End Try
    End Function

    Private Function Splittext(ByVal strsplittext As String) As Array
        If Trim(strsplittext) <> "" Then
            Dim arrstring() As String
            arrstring = Split(strsplittext, "-", 2)
            Return arrstring
        End If
        Return Nothing
    End Function

    Private Function Fill_TemplateGallery(ByVal TemplateID As Long, ByRef trSmartDiagnosis As SqlTransaction) As String
        Dim dt As DataTable
        Dim mstream As ADODB.Stream
        Dim strFileName As String
     
        dt = GetTemplate(TemplateID, trSmartDiagnosis)
        'Check if there are records for selected Node
        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then
                mstream = New ADODB.Stream
                mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
                'WdPatientEdu.Close()
                mstream.Open()
                mstream.Write(dt.Rows(0)(0))
                strFileName = gloSettings.FolderSettings.AppTempFolderPath & getUniqueID() & "Temp6.doc"
                mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
                mstream.Close()
                mstream = Nothing
                'WdPatientEdu.Open(strFileName)
                'oCurDoc = WdPatientEdu.ActiveDocument
                'SetWordObjectEntry()
                'getFields()
                dt.Dispose()
                dt = Nothing
                Return strFileName
            End If
            dt.Dispose()
            dt = Nothing
        End If
        Return ""
    End Function

    Public Function GetTemplate(ByVal TemplateID As Long, ByRef trSmartDiagnosis As SqlTransaction) As DataTable
        'Try
        Dim adpt As New SqlDataAdapter
        Dim dt As New DataTable

        Dim Cmd As SqlCommand = New SqlCommand("gsp_GetExamContents", Conn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Transaction = trSmartDiagnosis

        Dim objParam As SqlParameter
        objParam = Cmd.Parameters.Add("@nTemplateID", SqlDbType.BigInt)
        objParam.Direction = ParameterDirection.Input
        objParam.Value = TemplateID

        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If

        adpt.SelectCommand = Cmd
        adpt.Fill(dt)
        'Conn.Close()
        If Cmd IsNot Nothing Then
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
        End If
        objParam = Nothing
        adpt.Dispose()
        adpt = Nothing
        ''connection state is closed
        If Not IsNothing(Conn) Then
            If (Conn.State = ConnectionState.Open) Then
                Conn.Close()
            End If
        End If
        Return dt
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        '    trSmartDiagnosis.Rollback()
        '    If Conn.State = ConnectionState.Open Then
        '        Conn.Close()
        '    End If
        'End Try
    End Function
    Public Function GetAllCategory(ByVal CategoryType As String) As DataTable
        Try
            Dim cmd As New SqlCommand("gsp_FillCategory_Mst", Conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter
            Dim dt As DataTable = Nothing
            Dim dadpt As SqlDataAdapter
            sqlParam = cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = CategoryType

            sqlParam = cmd.Parameters.Add("@flag", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 1   '''' 
            Conn.Open()

            dadpt = New SqlDataAdapter
            dadpt.SelectCommand = cmd
            dt = New DataTable
            dadpt.Fill(dt)
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
            dadpt.Dispose()
            dadpt = Nothing
            Return dt
            ''dt(0) = CategoryID
            ''dt(1) = CategoryName

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Smart Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'objBusLayer = Nothing
            Conn.Close()
        End Try
    End Function

    Public Function GetEducationID(ByVal arrlist As ArrayList) As ArrayList
        ''arrlist contains The Name of Templates of type Patient Education

        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If

        Dim arrPE As New ArrayList

        ' Dim trSmartDiagnosis As SqlTransaction
        '  trSmartDiagnosis = Conn.BeginTransaction
        Dim dadpt As SqlDataAdapter
        Dim dt As DataTable
        Dim objparam As SqlParameter
        Try
            Dim i As Integer


            For i = 0 To arrlist.Count - 1
                Dim lst As New myList
                lst.Description = Trim(CType(arrlist(i), myList).Description)

                Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_GetTemplateID", Conn)

                Cmd.CommandType = CommandType.StoredProcedure
                ' Cmd.Transaction = trSmartDiagnosis

                objparam = Cmd.Parameters.Add("@sTemplateName", SqlDbType.VarChar)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = lst.Description

                objparam = Cmd.Parameters.Add("@Flag", SqlDbType.Int)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = 99

                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                dadpt = New SqlDataAdapter
                dadpt.SelectCommand = Cmd
                dt = New DataTable
                dadpt.Fill(dt)

                dadpt.Dispose()
                dadpt = Nothing
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing

                If dt.Rows.Count > 0 Then
                    lst.Index = dt.Rows(0)(0)
                    Dim j As Integer
                    Dim bIsExists As Boolean = False
                    For j = 0 To arrPE.Count - 1
                        If CType(arrPE.Item(j), myList).Index = dt.Rows(0)(0) Then
                            bIsExists = True
                        End If
                    Next
                    If bIsExists = False Then
                        arrPE.Add(lst)
                    End If
                End If
                dt.Dispose()
                dt = Nothing
              
            Next

            '  trSmartDiagnosis.Commit()
            Conn.Close()
            Return arrPE
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)
            'trMedication.Rollback()
            ' trSmartDiagnosis.Rollback()
            ' trSmartDiagnosis = Nothing
            '     Cmd = Nothing
            Conn.Close()
            Return Nothing
        Finally
            'If Cmd IsNot Nothing Then
            '    Cmd.Parameters.Clear()
            '    Cmd.Dispose()
            '    Cmd = Nothing
            'End If
            objparam = Nothing
        End Try
    End Function

    Public Function SaveExamEducation(ByVal VisitID As Long, ByVal PatientID As Long, ByVal nExamID As Int64, ByVal strTempFilePath As String, ByVal ProviderID As Long) As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim ExamParam As SqlParameter
        Try

            cmd = New SqlCommand("gsp_InUpExamEducation", Conn)
            cmd.CommandType = CommandType.StoredProcedure
            ' Dim sqlParam As SqlParameter


            ExamParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = VisitID

            ExamParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = PatientID

            ExamParam = cmd.Parameters.Add("@ExamID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = nExamID

            'Order Comment as Template
            ExamParam = cmd.Parameters.Add("@sPENotes", SqlDbType.Image)
            ExamParam.Direction = ParameterDirection.Input
            Dim mstream As ADODB.Stream
            mstream = New ADODB.Stream
            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
            mstream.Open()
            mstream.LoadFromFile(strTempFilePath)
            ExamParam.Value = mstream.Read()

            ExamParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = GetPrefixTransactionID()

            ExamParam = cmd.Parameters.Add("@ProviderID", SqlDbType.BigInt)
            ExamParam.Direction = ParameterDirection.Input
            ExamParam.Value = ProviderID


            mstream.Close()
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            cmd.ExecuteNonQuery()
            mstream = Nothing

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.Add, "Exam Added", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.Add, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient Education", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            Conn.Close()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            ExamParam = Nothing
        End Try
        Return Nothing
    End Function
End Class
