Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class ClsTreatmentDBLayer
    Public Sub New()
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
        gstrMessageBoxCaption = GetMessageBoxCaption()
    End Sub
    Private Conn As SqlConnection
    'Private Dv As DataView
    'Private Cmd As System.Data.SqlClient.SqlCommand
    '  Private ArrMedicationCol As New ArrayList
    'Public Function AddData(ByVal ExamID As Long, ByVal VisitID As Long, ByVal arrlist As ArrayList, ByVal arrmodifierlist As ArrayList) As Boolean
    '    Conn.Open()
    '    Dim trDiagnosis As SqlTransaction
    '    trDiagnosis = Conn.BeginTransaction
    '    Dim cmddelete As SqlCommand
    '    Dim cmddeleteModifier As SqlCommand
    '    Try
    '        Dim i As Integer
    '        Dim objparam As SqlParameter

    '        cmddeleteModifier = New System.Data.SqlClient.SqlCommand("gsp_DeleteCPTModifier", Conn)
    '        cmddeleteModifier.CommandType = CommandType.StoredProcedure
    '        cmddeleteModifier.Transaction = trDiagnosis

    '        'objparam = cmddeleteModifier.Parameters.Add("@nExamId", ExamID)
    '        objparam = cmddeleteModifier.Parameters.AddWithValue("@nExamId", ExamID)
    '        objparam.Direction = ParameterDirection.Input
    '        'objparam.Value = ExamID

    '        cmddeleteModifier.ExecuteNonQuery()
    '        cmddeleteModifier.Parameters.Clear()

    '        cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteTreatment", Conn)
    '        cmddelete.CommandType = CommandType.StoredProcedure
    '        cmddelete.Transaction = trDiagnosis

    '        'objparam = cmddelete.Parameters.Add("@nExamId", ExamID)
    '        objparam = cmddelete.Parameters.AddWithValue("@nExamId", ExamID)
    '        objparam.Direction = ParameterDirection.Input
    '        'objparam.Value = ExamID

    '        cmddelete.ExecuteNonQuery()
    '        cmddelete.Parameters.Clear()

    '        For i = 0 To arrlist.Count - 1
    '            Dim objmylist As mytable
    '            objmylist = CType(arrlist.Item(i), mytable)

    '            'Insert data in Diagnosis

    '            Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertTreatment", Conn)

    '            Cmd.CommandType = CommandType.StoredProcedure

    '            Cmd.Transaction = trDiagnosis
    '            'objparam = Cmd.Parameters.Add("@nExamId", ExamID)
    '            objparam = Cmd.Parameters.AddWithValue("@nExamId", ExamID)
    '            objparam.Direction = ParameterDirection.Input
    '            'objparam.Value = ExamID

    '            'objparam = Cmd.Parameters.Add("@nVisitID", VisitID)
    '            objparam = Cmd.Parameters.AddWithValue("@nVisitID", VisitID)
    '            objparam.Direction = ParameterDirection.Input
    '            'objparam.Value = VisitID

    '            'objparam = Cmd.Parameters.Add("@nPatientID", gnPatientID)
    '            objparam = Cmd.Parameters.AddWithValue("@nPatientID", gnPatientID)
    '            objparam.Direction = ParameterDirection.Input
    '            'objparam.Value = gnPatientID

    '            'objparam = Cmd.Parameters.Add("@sCPTcode", SqlDbType.VarChar)
    '            objparam = Cmd.Parameters.AddWithValue("@sCPTcode", SqlDbType.VarChar)
    '            objparam.Direction = ParameterDirection.Input
    '            objparam.Value = objmylist.Code

    '            'objparam = Cmd.Parameters.Add("@sCPTDescription", SqlDbType.VarChar)
    '            objparam = Cmd.Parameters.AddWithValue("@sCPTDescription", SqlDbType.VarChar)
    '            objparam.Direction = ParameterDirection.Input
    '            objparam.Value = objmylist.Description

    '            'Insert data in ICD9Drugs

    '            Cmd.ExecuteNonQuery()

    '            Cmd.Parameters.Clear()

    '        Next

    '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.Add, "CPTCode Added for Patient", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success)
    '        'Dim objAudit As New clsAudit
    '        'objAudit.CreateLog(clsAudit.enmActivityType.Add, "CPTCode Added for Patient", gstrLoginName, gstrClientMachineName, gnPatientID)
    '        'objAudit = Nothing

    '        Dim CmdModifier As SqlCommand
    '        For i = 0 To arrmodifierlist.Count - 1
    '            Dim objmylist As mytable
    '            objmylist = CType(arrmodifierlist.Item(i), mytable)

    '            CmdModifier = New System.Data.SqlClient.SqlCommand("gsp_InsertCPTModifier", Conn)
    '            CmdModifier.CommandType = CommandType.StoredProcedure
    '            CmdModifier.Transaction = trDiagnosis
    '            Dim sqlParam As SqlParameter

    '            '' Cmd.Transaction = trCPTModifier

    '            'sqlParam = CmdModifier.Parameters.Add("@ExamId", ExamID)
    '            sqlParam = CmdModifier.Parameters.AddWithValue("@ExamId", ExamID)
    '            sqlParam.Direction = ParameterDirection.Input
    '            'sqlParam.Value = ExamID

    '            'sqlParam = CmdModifier.Parameters.Add("@CPTCode", SqlDbType.VarChar)
    '            sqlParam = CmdModifier.Parameters.AddWithValue("@CPTCode", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objmylist.Code

    '            'sqlParam = CmdModifier.Parameters.Add("@ModifierCode", SqlDbType.VarChar)
    '            sqlParam = CmdModifier.Parameters.AddWithValue("@ModifierCode", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objmylist.ModCode

    '            sqlParam = CmdModifier.Parameters.Add("@ModifierDesc", SqlDbType.VarChar)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objmylist.Description

    '            sqlParam = CmdModifier.Parameters.Add("@Unit", SqlDbType.Int)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = objmylist.Unit

    '            sqlParam = CmdModifier.Parameters.Add("@MachineID", SqlDbType.BigInt)
    '            sqlParam.Direction = ParameterDirection.Input
    '            sqlParam.Value = GetPrefixTransactionID()

    '            If Conn.State = ConnectionState.Closed Then
    '                Conn.Open()
    '            End If
    '            CmdModifier.ExecuteNonQuery()

    '            CmdModifier.Parameters.Clear()
    '        Next
    '        trDiagnosis.Commit()
    '        Conn.Close()
    '        'If intMode = 1 Then 'Add
    '        '    objAudit.CreateLog(clsAudit.enmActivityType.Add, "Medication for Date " & Now & " Added", gstrLoginName, gstrClientMachineName, gnPatientID)
    '        'ElseIf intMode = 2 Then 'Modify
    '        '    objAudit.CreateLog(clsAudit.enmActivityType.Modify, "Medication for Date " & objMedication.PrescriptionDate & " Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
    '        'End If
    '        'objAudit = Nothing

    '        Return True
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        MsgBox(ex.Message)
    '        'trMedication.Rollback()
    '        trDiagnosis.Rollback()
    '        trDiagnosis = Nothing
    '        Cmd = Nothing
    '        cmddelete = Nothing
    '        Conn.Close()
    '        Return False
    '    End Try
    'End Function
    'Public Function DeleteCPTModifier(ByVal ExamID As Long, ByVal CPTCode As String)
    '    Dim objparam As SqlParameter

    '    cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteTreatment", Conn)
    '    cmddelete.CommandType = CommandType.StoredProcedure
    '    cmddelete.Transaction = trDiagnosis

    '    objparam = cmddelete.Parameters.Add("@nExamId", SqlDbType.Int)
    '    objparam.Direction = ParameterDirection.Input
    '    objparam.Value = ExamID

    '    cmddelete.ExecuteNonQuery()
    'End Function

    'Public Sub AddCPTModifier(ByVal ExamID As Long, ByVal CPTCode As String, ByVal ArrModifier As Array)

    '    'Dim trCPTModifier As SqlTransaction
    '    'trCPTModifier = Conn.BeginTransaction
    '    Try
    '        ''Dim i As Integer
    '        ''Dim objparam As SqlParameter

    '        ''cmddelete = New System.Data.SqlClient.SqlCommand("gsp_DeleteCPTModifier", Conn)
    '        ''cmddelete.CommandType = CommandType.StoredProcedure
    '        ''cmddelete.Transaction = trCPTModifier

    '        ''objparam = cmddelete.Parameters.Add("@CPTCode", SqlDbType.Int)
    '        ''objparam.Direction = ParameterDirection.Input
    '        ''objparam.Value = CPTCode

    '        ''cmddelete.ExecuteNonQuery()
    '        ''cmddelete.Parameters.Clear()
    '        Dim Cmd As SqlCommand
    '        Cmd = New System.Data.SqlClient.SqlCommand("gsp_InsertCPTModifier", Conn)
    '        Cmd.CommandType = CommandType.StoredProcedure
    '        Dim sqlParam As SqlParameter

    '        '' Cmd.Transaction = trCPTModifier

    '        'sqlParam = Cmd.Parameters.Add("@ExamId", ExamID)
    '        sqlParam = Cmd.Parameters.AddWithValue("@ExamId", ExamID)
    '        sqlParam.Direction = ParameterDirection.Input
    '        'sqlParam.Value = ExamID

    '        sqlParam = Cmd.Parameters.Add("@CPTCode", SqlDbType.VarChar)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = CPTCode

    '        sqlParam = Cmd.Parameters.Add("@ModifierCode", SqlDbType.VarChar)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = ArrModifier(0)

    '        sqlParam = Cmd.Parameters.Add("@ModifierDesc", SqlDbType.VarChar)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = ArrModifier(1)

    '        sqlParam = Cmd.Parameters.Add("@Unit", SqlDbType.Int)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = Val(ArrModifier(2))

    '        sqlParam = Cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = GetPrefixTransactionID()

    '        If Conn.State = ConnectionState.Closed Then
    '            Conn.Open()
    '        End If
    '        Cmd.ExecuteNonQuery()

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        ' MessageBox.Show(ex.ToString, "CPT Modifier")
    '    Finally
    '        Conn.Close()
    '    End Try
    'End Sub

    Public ReadOnly Property DsDataview() As DataView
        Get
            'Dv = Ds.Tables("Category_Mst").DefaultView
            Return Nothing ' Dv
            'Return Ds
        End Get

    End Property
    Public Sub SortDataview(ByVal strsort As String)
        'Dv.Sort = strsort
    End Sub
    Public Sub ClearCol()
        '     ArrMedicationCol.Clear()
    End Sub

    Public Function FillCPT(ByVal Flag As Int16) As DataTable
        Dim sqlParam As SqlParameter = Nothing
        Dim dt As New DataTable
        Dim Cmd As System.Data.SqlClient.SqlCommand = Nothing
        Dim adpt As New SqlDataAdapter
        Try


            '''''Flag=0 if Order by CPTCODE
            '''''Flag=1 if Order by CPTDESCRIPTION


            Cmd = New SqlCommand("gsp_FillCPT", Conn)

            Cmd.CommandType = CommandType.StoredProcedure
            adpt.SelectCommand = Cmd


            sqlParam = Cmd.Parameters.Add("@Flag", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Flag

            adpt.Fill(dt)
            Conn.Close()
         
            'Dim dreader As SqlDataReader
            'Conn.Open()
            'dreader = Cmd.ExecuteReader()

            'Do While dreader.Read
            '    Dim i As Integer
            '    i = dreader("nSpecialtyID")
            'Loop
        Catch ex As Exception
        Finally
            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
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
        Return dt

    End Function

    '' SUDHIR 20090711 ''





    Public Function GetAllCPT() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Try
            Dim dtCPT As DataTable = Nothing
            Dim _Query As String = "SELECT nCPTID, sCPTCode, sDescription FROM CPT_MST"
            oDB.Connect(False)
            oDB.Retrive_Query(_Query, dtCPT)
            If dtCPT IsNot Nothing Then
                Return dtCPT
            Else
                Return Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
        End Try
    End Function
    '' END SUDHIR ''






    Public Function FetchFlowsheetFromFlowsheet(ByVal VisitID As Long) As DataTable
        Dim objParam As SqlParameter = Nothing
        Dim Cmd As SqlCommand = Nothing
        Dim adpt As New SqlDataAdapter

        Try
            Dim dt As New DataTable
           
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_Flowsheetnamebyvisit_id", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            adpt.SelectCommand = Cmd

            objParam = Cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitID

            'objParam = Cmd.Parameters.Add("@dtPrescriptiondate", SqlDbType.DateTime)
            'objParam.Direction = ParameterDirection.Input
            'objParam.Value = PriscriptionDate

            adpt.Fill(dt)
            Conn.Close()
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
            'MsgBox(ex.Message)
            'Conn.Close()
            'Return Nothing
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
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

    Public Function FetchLaborderName(ByVal VisitID As Long) As DataTable
        Dim objParam As SqlParameter = Nothing
        Dim Cmd As SqlCommand = Nothing
        Dim adpt As New SqlDataAdapter
        Try
            ' ByVal transactionDate As DateTime
            Dim dt As New DataTable
            'Dim adpt As New SqlDataAdapter

            Cmd = New System.Data.SqlClient.SqlCommand("gsp_selectlabordernamebyvisitid", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            adpt.SelectCommand = Cmd



            objParam = Cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitID

            ' objParam = Cmd.Parameters.Add("@transactiondate", SqlDbType.DateTime)
            ' objParam.Direction = ParameterDirection.Input
            ' objParam.Value = transactionDate
            adpt.Fill(dt)
            Conn.Close()
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
            'MsgBox(ex.Message)
            'Conn.Close()
            'Return Nothing
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
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


    Public Function FetchReferralNameFromReferral(ByVal VisitID As Long) As DataTable
        Dim objParam As SqlParameter = Nothing
        Dim Cmd As SqlCommand = Nothing
        Dim adpt As New SqlDataAdapter
        Try
            Dim dt As New DataTable
            'Dim adpt As New SqlDataAdapter

            Cmd = New System.Data.SqlClient.SqlCommand("gsp_Referralnamebyvisit_id", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            adpt.SelectCommand = Cmd



            objParam = Cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitID

            'objParam = Cmd.Parameters.Add("@dtPrescriptiondate", SqlDbType.DateTime)
            'objParam.Direction = ParameterDirection.Input
            'objParam.Value = PriscriptionDate

            adpt.Fill(dt)
            Conn.Close()
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
            'MsgBox(ex.Message)
            'Conn.Close()
            'Return Nothing
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
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






    Public Function FetchOrder(ByVal VisitID As Long) As DataTable
        Dim objParam As SqlParameter = Nothing
        Dim Cmd As SqlCommand = Nothing
        Dim adpt As New SqlDataAdapter
        Try
            Dim dt As New DataTable
            'Dim adpt As New SqlDataAdapter

            Cmd = New System.Data.SqlClient.SqlCommand("gsp_OrderName_by_visit_id", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            adpt.SelectCommand = Cmd



            objParam = Cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitID

            'objParam = Cmd.Parameters.Add("@dtPrescriptiondate", SqlDbType.DateTime)
            'objParam.Direction = ParameterDirection.Input
            'objParam.Value = PriscriptionDate

            adpt.Fill(dt)
            Conn.Close()
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
            'MsgBox(ex.Message)
            'Conn.Close()
            'Return Nothing
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
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

    Public Function FetchCPTforUpdate(ByVal ExamID As Long, Optional ByVal ICD9Code As String = "", Optional ByVal ICD9Desc As String = "") As DataTable
        Dim objParam As SqlParameter = Nothing
        Dim Cmd As SqlCommand = Nothing
        Dim sqladpt As New SqlDataAdapter
        Try
            Dim dt As New DataTable
            'Dim sqladpt As New SqlDataAdapter

            Cmd = New System.Data.SqlClient.SqlCommand("gsp_scanTreatment", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            sqladpt.SelectCommand = Cmd



            'objParam = Cmd.Parameters.Add("@nExamID", ExamID)
            objParam = Cmd.Parameters.AddWithValue("@nExamID", ExamID)
            objParam.Direction = ParameterDirection.Input
            'objParam.Value = ExamID
            objParam = Cmd.Parameters.AddWithValue("@ICD9Code", ICD9Code)
            objParam.Direction = ParameterDirection.Input

            objParam = Cmd.Parameters.AddWithValue("@ICD9Desc", ICD9Desc)
            objParam.Direction = ParameterDirection.Input

            sqladpt.Fill(dt)
            Conn.Close()
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.Query, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'MsgBox(ex.Message)
            'Conn.Close()
            Return Nothing
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
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
    ''******************************
    '' By Mahesh 
    '' to Selelct Modifiers along with its Unit for selected Exam & CPT from CPTModifier Table
    '' I/P: ExamID, CPTCode
    '' O/P: DataTable 
    '' column(0): sModCode,  column(1):sModDesc, column(2):nUnit
    Public Function SelectCPTModifer(ByVal ExamID As Long, ByVal CPTCode As String) As DataTable
        Dim objParam As SqlParameter = Nothing
        Dim Cmd As SqlCommand = Nothing
        Dim sqladpt As New SqlDataAdapter
        Try
            Dim dt As New DataTable
            ' Dim sqladpt As New SqlDataAdapter

            Cmd = New System.Data.SqlClient.SqlCommand("gsp_ScanCPTModifier", Conn)
            Cmd.CommandType = CommandType.StoredProcedure


            'objParam = Cmd.Parameters.Add("@ExamID", ExamID)
            objParam = Cmd.Parameters.AddWithValue("@ExamID", ExamID)
            objParam.Direction = ParameterDirection.Input
            'objParam.Value = ExamID

            objParam = Cmd.Parameters.Add("@CPTCode", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CPTCode

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            sqladpt.SelectCommand = Cmd
            sqladpt.Fill(dt)
            Return dt

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartTreatment, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MsgBox(ex.Message)
            'Conn.Close()
            Return Nothing
        Finally
            If Not IsNothing(objParam) Then
                objParam = Nothing
            End If
            Conn.Close()
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
    ''******************************
End Class
