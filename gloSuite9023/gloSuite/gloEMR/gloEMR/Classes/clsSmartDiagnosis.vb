Imports System.Data.SqlClient
Imports gloEMRGeneralLibrary.gloEMRDatabase
Imports gloTaskMail

Public Class clsSmartDiagnosis
    Implements IDisposable

    Public Sub New()
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
    End Sub

    Private Conn As SqlConnection
    ' Private Dv As DataView
    'Private Cmd As System.Data.SqlClient.SqlCommand
    'Private ArrMedicationCol As New ArrayList
    ' Dim arrDruglist As New ArrayList  '' For Priscription
    '    Private ObjTasksDBLayer As ClsTasksDBLayer

    'Public dtDiagnosis As New DataTable
    'Public dtTreatment As New DataTable
    'Public dtDrugs As New DataTable
    'Public odB As DataBaseLayer


    Public Function ScanDiagnosis(ByVal ExamID As Long, ByVal VisitID As Long) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim adpt As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try

            adpt = New SqlDataAdapter
            dt = New DataTable

            cmd = New SqlCommand("gsp_scanDiagnosis", Conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim objparam As SqlParameter

            objparam = cmd.Parameters.Add("@nExamId", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = ExamID

            objparam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = VisitID


            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            adpt.SelectCommand = cmd
            adpt.Fill(dt)
            objparam = Nothing
            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            Conn.Close()
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(adpt) Then
                adpt.Dispose()
                adpt = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    Public Function ScanTreatment(ByVal ExamID As Long) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim adpt As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            adpt = New SqlDataAdapter
            dt = New DataTable

            cmd = New SqlCommand("gsp_scanTreatment", Conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim objparam As SqlParameter

            objparam = cmd.Parameters.Add("@nExamId", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = ExamID


            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            adpt.SelectCommand = cmd
            adpt.Fill(dt)
            objparam = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            Conn.Close()
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(adpt) Then
                adpt.Dispose()
                adpt = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    Public Function FetchSmartDiagnosis() As DataTable
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013
        Dim _query As String
        Dim dt As DataTable = Nothing
        Dim objSDA As SqlDataAdapter = Nothing
        'Dim _Result As Object
        Try

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            '' _query = "select nUserID, sFieldName, bFieldStatus, nFieldSequence, sFieldType, bSendTask from SmartConfig where nUserId='" & gnLoginID & "' AND sFieldType = 'SmartDX' Order by nFieldSequence"
            _query = "select ISNULL(nUserID,0) AS nUserID, ISNULL(sFieldName,'') AS sFieldName, ISNULL(bFieldStatus,'true') AS bFieldStatus, ISNULL(nFieldSequence,0) AS nFieldSequence, ISNULL(sFieldType,'') AS sFieldType, ISNULL(bSendTask,'false') AS bSendTask,ISNULL(sTaskusers,'') as sTaskusers,ISNULL(bAllowviewtsk,0) as bAllowviewtsk,ISNULL(sUserID,'') as sUserID from SmartConfig where nUserId='" & gnLoginID & "' AND sFieldType = 'SmartDX' Order by nFieldSequence"
            objSDA = New SqlDataAdapter(_query, Conn)
            dt = New DataTable()
            objSDA.Fill(dt)

            Return dt
        Catch ex As Exception
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(objSDA) Then
                objSDA.Dispose()
                objSDA = Nothing
            End If

            If Not IsNothing(Conn) Then
                If (Conn.State = ConnectionState.Open) Then
                    Conn.Close()
                End If
            End If
        End Try
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013
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

    Public Function FillICD9(ByVal ICDRevision As Int16, ByVal SearchString As String, Optional ByVal Flag As Int16 = 0, Optional ByVal nPatientID As Int64 = 0, Optional bIsPatientSelected As Boolean = False) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim adpt As New SqlDataAdapter
        Dim dt As New DataTable
        Try
            cmd = New SqlCommand("gsp_FillAssociatedICD9", Conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim objparam As SqlParameter
            objparam = cmd.Parameters.Add("@Flag", SqlDbType.Int)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = Flag
            objparam = Nothing

            objparam = cmd.Parameters.Add("@ICDRevision", SqlDbType.SmallInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = ICDRevision
            objparam = Nothing

            objparam = cmd.Parameters.Add("@SearchString", SqlDbType.VarChar)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = SearchString
            objparam = Nothing

            objparam = cmd.Parameters.Add("@nPatientId", SqlDbType.BigInt)
            objparam.Direction = ParameterDirection.Input
            objparam.Value = nPatientID
            objparam = Nothing

            objparam = cmd.Parameters.Add("@IsPatientSelected", SqlDbType.Bit)
            objparam.Direction = ParameterDirection.Input
            If bIsPatientSelected Then
                objparam.Value = 1
            Else
                objparam.Value = 0
            End If

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
    Public Function FetchICD9forUpdate(ByVal ICD9ID As Long) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqladpt As New SqlDataAdapter
        Dim dt As New DataTable
        Try
            cmd = New System.Data.SqlClient.SqlCommand("gsp_scanICD9Association", Conn)
            cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = cmd

            Dim objParam As SqlParameter

            objParam = cmd.Parameters.Add("@nICD9ID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ICD9ID

            sqladpt.Fill(dt)
            Conn.Close()
            objParam = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MsgBox(ex.Message)
            'Conn.Close()
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

        End Try
    End Function

    Public Function FetchDrugsFromPrescription(ByVal VisitID As Long, ByVal PriscriptionDate As DateTime) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim dt As New DataTable
        Dim sqladpt As New SqlDataAdapter
        Try
            cmd = New System.Data.SqlClient.SqlCommand("gsp_GetPrescriptionforSmartDiag", Conn)
            cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = cmd

            Dim objParam As SqlParameter

            objParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitID

            objParam = cmd.Parameters.Add("@dtPrescriptiondate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = PriscriptionDate

            sqladpt.Fill(dt)
            Conn.Close()
            objParam = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MsgBox(ex.Message)
            'Conn.Close()
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
        End Try
    End Function

    Public Function FetchFlowsheetFromFlowsheet(ByVal VisitID As Long) As DataTable
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013
        Dim cmd As SqlCommand = Nothing
        Dim dt As New DataTable
        Dim sqladpt As New SqlDataAdapter
        Try
            cmd = New System.Data.SqlClient.SqlCommand("gsp_Flowsheetnamebyvisit_id", Conn)
            cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = cmd

            Dim objParam As SqlParameter = Nothing

            objParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitID

            'objParam = Cmd.Parameters.Add("@dtPrescriptiondate", SqlDbType.DateTime)
            'objParam.Direction = ParameterDirection.Input
            'objParam.Value = PriscriptionDate

            sqladpt.Fill(dt)
            Conn.Close()
            objParam = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MsgBox(ex.Message)
            'Conn.Close()
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
        End Try
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013
    End Function

    Public Function FetchLaborderName(ByVal VisitID As Long) As DataTable
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013
        Dim cmd As SqlCommand = Nothing
        Dim dt As New DataTable
        Dim sqladpt As New SqlDataAdapter
        Try
            cmd = New System.Data.SqlClient.SqlCommand("gsp_selectlabordernamebyvisitid", Conn)
            cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = cmd

            Dim objParam As SqlParameter = Nothing

            objParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitID

            ' objParam = Cmd.Parameters.Add("@transactiondate", SqlDbType.DateTime)
            ' objParam.Direction = ParameterDirection.Input
            ' objParam.Value = transactionDate
            sqladpt.Fill(dt)
            Conn.Close()
            objParam = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MsgBox(ex.Message)
            'Conn.Close()
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
        End Try
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013
    End Function


    Public Function FetchReferralNameFromReferral(ByVal VisitID As Long) As DataTable
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013
        Dim cmd As SqlCommand = Nothing
        Dim dt As New DataTable
        Dim sqladpt As New SqlDataAdapter
        Try
            cmd = New System.Data.SqlClient.SqlCommand("gsp_Referralnamebyvisit_id", Conn)
            cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = cmd

            Dim objParam As SqlParameter = Nothing

            objParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitID

            'objParam = Cmd.Parameters.Add("@dtPrescriptiondate", SqlDbType.DateTime)
            'objParam.Direction = ParameterDirection.Input
            'objParam.Value = PriscriptionDate

            sqladpt.Fill(dt)
            Conn.Close()
            objParam = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MsgBox(ex.Message)
            'Conn.Close()
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
        End Try
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013
    End Function


    Public Function FetchOrder(ByVal VisitID As Long) As DataTable
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013
        Dim cmd As SqlCommand = Nothing
        Dim dt As New DataTable
        Dim sqladpt As New SqlDataAdapter
        Try
            cmd = New System.Data.SqlClient.SqlCommand("gsp_OrderName_by_visit_id", Conn)
            cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = cmd

            Dim objParam As SqlParameter = Nothing

            objParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitID

            'objParam = Cmd.Parameters.Add("@dtPrescriptiondate", SqlDbType.DateTime)
            'objParam.Direction = ParameterDirection.Input
            'objParam.Value = PriscriptionDate

            sqladpt.Fill(dt)
            Conn.Close()
            objParam = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MsgBox(ex.Message)
            'Conn.Close()
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

        End Try
        ''''''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013
    End Function



    Public Function FetchPatientEducation(ByVal VisitID As Long) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim dt As New DataTable
        Dim sqladpt As New SqlDataAdapter
        Try
            cmd = New System.Data.SqlClient.SqlCommand("gsp_ScanPatientEducation", Conn)
            cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = cmd

            Dim objParam As SqlParameter = Nothing

            objParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitID

            sqladpt.Fill(dt)
            Conn.Close()
            objParam = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MsgBox(ex.Message)
            'Conn.Close()
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
        Dim dt As DataTable = Nothing
        Dim mstream As ADODB.Stream = Nothing
        Dim strFileName As String = ""

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
        Return strFileName
    End Function

    Public Function GetTemplate(ByVal TemplateID As Long, ByRef trSmartDiagnosis As SqlTransaction) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim dt As New DataTable
        Dim adpt As New SqlDataAdapter

        Try

            cmd = New SqlCommand("gsp_GetExamContents", Conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Transaction = trSmartDiagnosis

            Dim objParam As SqlParameter = Nothing
            objParam = cmd.Parameters.Add("@nTemplateID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = TemplateID

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            adpt.SelectCommand = cmd
            adpt.Fill(dt)
            objParam = Nothing

            'Conn.Close()
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
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
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            ''connection state checked
            If Not IsNothing(Conn) Then
                If (Conn.State = ConnectionState.Open) Then
                    Conn.Close()
                End If
            End If

        End Try
    End Function
    Public Function GetAllCategory(ByVal CategoryType As String) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim dt As New DataTable
        Dim dadpt As New SqlDataAdapter
        Try
            cmd = New SqlCommand("gsp_FillCategory_Mst", Conn)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = CategoryType

            sqlParam = cmd.Parameters.Add("@flag", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 1   '''' 
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            dadpt = New SqlDataAdapter
            dadpt.SelectCommand = cmd
            dt = New DataTable
            dadpt.Fill(dt)
            sqlParam = Nothing
            Return dt
            ''dt(0) = CategoryID
            ''dt(1) = CategoryName

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Smart Diagnosis", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'objBusLayer = Nothing
            Conn.Close()
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(dadpt) Then
                dadpt.Dispose()
                dadpt = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
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
        Dim cmd As SqlCommand = Nothing
        Dim dadpt As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Dim objparam As SqlParameter = Nothing
        Try
            Dim i As Integer


            For i = 0 To arrlist.Count - 1
                Dim lst As New myList
                lst.Description = Trim(CType(arrlist(i), myList).Description)

                cmd = New System.Data.SqlClient.SqlCommand("gsp_GetTemplateID", Conn)

                cmd.CommandType = CommandType.StoredProcedure
                ' Cmd.Transaction = trSmartDiagnosis

                objparam = cmd.Parameters.Add("@sTemplateName", SqlDbType.VarChar)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = lst.Description

                objparam = cmd.Parameters.Add("@Flag", SqlDbType.Int)
                objparam.Direction = ParameterDirection.Input
                objparam.Value = 99

                If Conn.State = ConnectionState.Closed Then
                    Conn.Open()
                End If

                dadpt = New SqlDataAdapter
                dadpt.SelectCommand = cmd
                dt = New DataTable
                dadpt.Fill(dt)
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
                ' Cmd = Nothing
                dadpt.Dispose()
                dadpt = Nothing

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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)
            'trMedication.Rollback()
            ' trSmartDiagnosis.Rollback()
            ' trSmartDiagnosis = Nothing
            Return Nothing
        Finally
            Conn.Close()
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(dadpt) Then
                dadpt.Dispose()
                dadpt = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
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

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient Education", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient Education", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            Conn.Close()
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            ExamParam = Nothing
        End Try
        Return Nothing
    End Function


    ''Check the ICD9 and associated CPT is present or not 
    ''Pramod 20072906
    Public Function CheckExistance(ByVal ExamID As Long, ByVal VisitID As Long, ByVal strICD9Code As String, ByVal strICD9Desc As String, ByVal strCPTCode As String, ByVal strCPTDesc As String) As Boolean
        Dim odB As DataBaseLayer = New DataBaseLayer
        Dim oParameter As DBParameter = Nothing
        Dim _IsPresent As Boolean = False
        Try
            oParameter = New DBParameter
            oParameter.Name = "ExamID"
            oParameter.Direction = ParameterDirection.Input
            oParameter.Value = ExamID
            oParameter.DataType = SqlDbType.BigInt
            odB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oParameter = New DBParameter
            oParameter.Name = "VisitID"
            oParameter.Direction = ParameterDirection.Input
            oParameter.Value = VisitID
            oParameter.DataType = SqlDbType.BigInt
            odB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oParameter = New DBParameter
            oParameter.Name = "ICD9Code"
            oParameter.Direction = ParameterDirection.Input
            oParameter.Value = strICD9Code
            oParameter.DataType = SqlDbType.VarChar
            odB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oParameter = New DBParameter
            oParameter.Name = "@ICD9Desc"
            oParameter.Direction = ParameterDirection.Input
            oParameter.Value = strICD9Desc
            oParameter.DataType = SqlDbType.VarChar
            odB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oParameter = New DBParameter
            oParameter.Name = "@CPTCode"
            oParameter.Direction = ParameterDirection.Input
            oParameter.Value = strCPTCode
            oParameter.DataType = SqlDbType.VarChar
            odB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oParameter = New DBParameter
            oParameter.Name = "@CPTDesc"
            oParameter.Direction = ParameterDirection.Input
            oParameter.Value = strCPTDesc
            oParameter.DataType = SqlDbType.VarChar
            odB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            Return odB.GetDataValue("gsp_ScanSmartExamICD9CPT")
        Catch ex As Exception
            Return Nothing
        Finally
            odB.Dispose()
            odB = Nothing
        End Try
    End Function
    ''Added Rahul on 20101027
    ''Fuctions Call when User have no right to view Task form as per new Implementation In Task.
    ''Save Task.
    Public Sub AddTasks(ByVal Subject As String, ByVal Notes As String, ByVal TaskDate As DateTime, ByVal DueDate As DateTime, ByVal TaskType As gloTaskMail.TaskType, ByVal NoteExt As String, ByVal UserID As String, ByVal Taskusers As String, ByVal _PatientID As Long, Optional ByVal FaxTiffFileName As String = "")
        Dim arrlist As New ArrayList

        Call GetData(arrlist, Subject, _PatientID)

        Dim i As Integer

        Dim ogloTask As New gloTaskMail.gloTask(GetConnectionString)
        Dim oTask As New Task()
        Dim oTaskProgress As New gloTaskMail.TaskProgress

        Dim _UsersList() As String = UserID.Split("|")
        Dim _Taskusers() As String = Taskusers.Split("|")
        For i = 0 To _UsersList.Length - 1

            Dim oTaskAssign As New TaskAssign

            oTaskAssign.AssignFromID = gnLoginID
            oTaskAssign.AssignFromName = gstrLoginName

            If _UsersList(i) <> "" Then
                oTaskAssign.AssignToID = _UsersList(i) ''Task UserId
            Else
                oTaskAssign.AssignToID = gnLoginID ''Assign Default User(Login User)
            End If

            If oTaskAssign.AssignFromID = oTaskAssign.AssignToID Then
                oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Self
                oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Accept
            Else
                oTaskAssign.SelfAssigned = gloTasksMails.Common.SelfAssigned.Assigned
                oTaskAssign.AcceptRejectHold = gloTasksMails.Common.AcceptRejectHold.Hold
            End If

            If _Taskusers(i) <> "" Then
                oTaskAssign.AssignToName = _Taskusers(i) ''Task UserName
            Else
                oTaskAssign.AssignToName = gstrLoginName ''Assign Default User(Login User)
            End If

            oTaskAssign.ClinicID = gnClinicID
            oTask.Assignment.Add(oTaskAssign)
            oTaskAssign.Dispose()
            oTaskAssign = Nothing
        Next

        oTaskProgress.ClinicID = gnClinicID
        oTaskProgress.Complete = 0
        oTaskProgress.DateTime = TaskDate
        oTaskProgress.Description = Notes
        oTaskProgress.StatusID = 1 '' Not Started
        oTaskProgress.TaskID = 0

        '' 
        oTask.UserID = gnLoginID
        oTask.TaskGroupID = ogloTask.GetUniqueueId()
        oTask.TaskType = TaskType
        oTask.PatientID = _PatientID
        oTask.Subject = Subject
        oTask.ClinicID = gnClinicID
        oTask.DateCreated = gloDateMaster.gloDate.DateAsNumber(TaskDate)
        oTask.StartDate = gloDateMaster.gloDate.DateAsNumber(TaskDate)
        oTask.DueDate = gloDateMaster.gloDate.DateAsNumber(DueDate)
        oTask.FaxTiffFileName = TaskDate  ''OrderDate 
        oTask.IsPrivate = False
        oTask.MachineName = gstrClientMachineName
        oTask.Progress = oTaskProgress
        oTask.PriorityID = 1
        oTask.ProviderID = gnPatientProviderID
        NoteExt = NoteExt.Replace("'", "''")
        oTask.Notes = NoteExt

        Dim _TaskId As Int64 = ogloTask.Add(oTask)
        oTaskProgress.Dispose()
        oTaskProgress = Nothing
        oTask.Dispose()
        oTask = Nothing
        ogloTask.Dispose()
        ogloTask = Nothing

        If _TaskId > 0 Then
            Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
            Dim strQry As String = "UPDATE tm_taskmst SET sNoteEXT='" & NoteExt & "' where ntaskid=" & _TaskId
            oDB.Connect(False)
            oDB.Execute_Query(strQry)
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
        End If
    End Sub
    Private Sub GetData(ByRef Arrlist As ArrayList, ByVal Subject As String, ByRef _PatientID As Long)
        Dim LoginId As Int64
        Dim ObjTasksDBLayer As ClsTasksDBLayer = New ClsTasksDBLayer
        LoginId = ObjTasksDBLayer.GetLoginId

        Arrlist.Add(0)
        Arrlist.Add(LoginId)
        Arrlist.Add(Now.ToString())   ''dtTaskDate

        Arrlist.Add(Subject)   '' sSubject
        Arrlist.Add(Now.ToString())    '' dtDuedate
        Arrlist.Add("High")   '' sPriority
        Arrlist.Add("Not Started")   '' sStatus
        Arrlist.Add("")   '' sNotes
        Arrlist.Add(_PatientID)
        ObjTasksDBLayer.Dispose()
        ObjTasksDBLayer = Nothing
    End Sub
    ''End
    Private disposed As Boolean = False

    Public Sub Dispose() Implements System.IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                If Not IsNothing(Conn) Then
                    Conn.Dispose()
                    Conn = Nothing
                End If
            End If
        End If
        disposed = True
    End Sub
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
