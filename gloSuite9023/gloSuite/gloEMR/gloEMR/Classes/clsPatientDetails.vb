Imports System.Data.SqlClient
Public Class clsPatientDetails
    Implements IDisposable
    Enum enmCriteria
        Today
        Yesterday
        LastWeek
        LastMonth
        Customize
    End Enum

    Public Function Fill_DemoGraphic(ByVal nPatientId As Long) As DataTable
        Dim objCon As New SqlConnection
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_GetDemographic", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PatientID", nPatientId)
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
    Public Function Fill_Medication(ByVal nPatientId As Long) As DataTable
        Dim objCon As New SqlConnection
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_GetMedication", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PatientID", nPatientId)
            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            objCon.Close()
            Return dt
            ''dt Contains Following Columns
            ' ''dtMedicationDate, sMedication ,sDosage ,sRoute, sFrequency, sDuration, sAmount, sStatus, dtStartDate, dtEndDate, UserID , UserName 
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

    ' Dhruv -  to fill the Exam Template against soap Called from the dashboard UpdateTemplate
    'Bug #84149: CR00000379: New exam enhancement
    'Public Function Fill_NewExams(ByVal ProviderID As Long, CategoryID As Long) As DataTable

    '    Dim objConn As SqlConnection = Nothing
    '    Dim objCmd As SqlCommand = Nothing
    '    Dim da As SqlDataAdapter = Nothing
    '    Dim dt As DataTable = Nothing
    '    'Bug #84149: CR00000379: New exam enhancement
    '    Dim _QueryString As String = "select nTemplateID,sTemplateName from TemplateGallery_MST tm inner join category_mst cm on tm.ncategoryid=cm.ncategoryid"
    '    If ProviderID <> 0 Then
    '        _QueryString += " where tm.nProviderID = " & ProviderID & " "
    '        'Changes for new Template Category parameter
    '        If CategoryID <> 0 Then
    '            _QueryString += " AND tm.ncategoryid = " & CategoryID & " "
    '        End If
    '    Else
    '        If CategoryID <> 0 Then
    '            _QueryString += " where tm.ncategoryid = " & CategoryID & " "
    '        End If
    '    End If
    '    _QueryString += " order by sTemplateName"

    '    Try
    '        objConn = New SqlConnection()
    '        If Not IsNothing(objConn) Then
    '            objConn.ConnectionString = GetConnectionString()
    '            If Not IsNothing(objConn) Then
    '                objCmd = New SqlCommand(_QueryString, objConn)
    '                If Not IsNothing(objCmd) Then
    '                    objConn.Open()
    '                    da = New SqlDataAdapter(objCmd)
    '                    If Not IsNothing(da) Then
    '                        dt = New DataTable()
    '                        If Not IsNothing(dt) Then
    '                            da.Fill(dt)
    '                            objConn.Close()
    '                        End If
    '                    End If

    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Dim _ErrorMessage As String = ex.ToString()
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Add, _ErrorMessage, gloAuditTrail.ActivityOutCome.Failure)

    '    Finally
    '        'If Not IsNothing(dt) Then
    '        '    dt.Dispose()
    '        '    dt = Nothing
    '        'End If
    '        If Not IsNothing(da) Then
    '            da.Dispose()
    '            da = Nothing
    '        End If
    '        If Not IsNothing(objCmd) Then
    '            objConn.Dispose()
    '            objConn = Nothing
    '        End If
    '        If Not IsNothing(objCmd) Then
    '            objCmd.Parameters.Clear()
    '            objCmd.Dispose()
    '            objCmd = Nothing
    '        End If
    '    End Try
    '    Return dt
    'End Function

    Public Function Fill_NewExams(ByVal ProviderID As Long, CategoryID As Long) As DataTable
        'Changes for new Template Category parameter
        Dim objCon As New SqlConnection
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing

        Try
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_GetTemplatesNewExam", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@nProviderID", ProviderID)
            cmd.Parameters.AddWithValue("@nCategoryID", CategoryID)

            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            objCon.Close()

            Return dt

        Catch ex As Exception
            Dim _ErrorMessage As String = ex.ToString()
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Add, _ErrorMessage, gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally

            If Not IsNothing(da) Then
                da.Dispose() : da = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear() : cmd.Dispose() : cmd = Nothing
            End If

            If Not IsNothing(objCon) Then
                objCon.Dispose() : objCon = Nothing
            End If

        End Try

    End Function


    Public Function Fill_PastExams(ByVal nPatientID As Long) As DataTable
        Dim objCon As New SqlConnection
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_GetPastExams", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PatientID", nPatientID)
           
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
    ''added for optimizing dashboard patient details Exam tab
    Public Function Fill_PastExams_Dashboard(ByVal nPatientID As Long) As DataSet
        Dim objCon As New SqlConnection
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim ds As DataSet = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_GetPastExams_DashBoard", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PatientID", nPatientID)

            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            ds = New DataSet
            da.Fill(ds)
            objCon.Close()
            Return ds
        Catch ex As Exception
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

        End Try
    End Function


    '' New Fucntion Added While Date filter is Not applied
    Public Function Fill_PastExams(ByVal nPatientID As Long, ByVal nProviderId As Int64, ByVal IsFinished As Short) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objcmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try

            objcmd = New SqlCommand("gsp_GetPastExams", objCon)
            objcmd.CommandType = CommandType.StoredProcedure
            objcmd.Parameters.AddWithValue("@PatientID", nPatientID)

            Dim objParaFinished As New SqlParameter
            With objParaFinished
                .ParameterName = "@Finished"
                .Value = IsFinished
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objcmd.Parameters.Add(objParaFinished)

            Dim objParaStatus As New SqlParameter
            With objParaStatus
                .ParameterName = "@Status"
                .Value = IsFinished
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objcmd.Parameters.Add(objParaStatus)

            Dim objParaDoctorID As New SqlParameter
            With objParaDoctorID
                .ParameterName = "@nProviderID"
                .Value = nProviderId
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objcmd.Parameters.Add(objParaDoctorID)

            da = New SqlDataAdapter
            da.SelectCommand = objcmd
            dt = New DataTable
            da.Fill(dt)

            objParaFinished = Nothing
            objParaStatus = Nothing
            objParaDoctorID = Nothing

            If Not dt Is Nothing Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
            
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If objcmd IsNot Nothing Then
                objcmd.Parameters.Clear()
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Public Function Fill_PastExams(ByVal nPatientID As Long, ByVal nProviderId As Int64, ByVal dtFrom As DateTime, ByVal dtTo As DateTime, ByVal IsFinished As Short) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objcmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try

            objcmd = New SqlCommand("gsp_GetPastExams", objCon)
            objcmd.CommandType = CommandType.StoredProcedure
            objcmd.Parameters.AddWithValue("@PatientID", nPatientID)

            Dim objParaFrom As New SqlParameter
            With objParaFrom
                .ParameterName = "@FromDate"
                .Value = dtFrom.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objcmd.Parameters.Add(objParaFrom)

            Dim objParaTo As New SqlParameter
            With objParaTo
                .ParameterName = "@ToDate"
                .Value = dtTo.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objcmd.Parameters.Add(objParaTo)

            'Dim objParaFinished As New SqlParameter
            'With objParaFinished
            '    .ParameterName = "@Finished"
            '    .Value = Isfinished
            '    .Direction = ParameterDirection.Input
            '    .SqlDbType = SqlDbType.Bit
            'End With
            'objcmd.Parameters.Add(objParaFinished)

            Dim objParaStatus As New SqlParameter
            With objParaStatus
                .ParameterName = "@Status"
                .Value = IsFinished
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objcmd.Parameters.Add(objParaStatus)

            Dim objParaDoctorID As New SqlParameter
            With objParaDoctorID
                .ParameterName = "@nProviderID"
                .Value = nProviderId
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objcmd.Parameters.Add(objParaDoctorID)

            da = New SqlDataAdapter
            da.SelectCommand = objcmd
            dt = New DataTable
            da.Fill(dt)

            objParaFrom = Nothing
            objParaDoctorID = Nothing
            objParaStatus = Nothing
            objParaTo = Nothing

            If Not dt Is Nothing Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally
          
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If objcmd IsNot Nothing Then
                objcmd.Parameters.Clear()
                objcmd.Dispose()
                objcmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function
    Public Function Fill_PatientMessges(ByVal nPatientID As Long) As DataTable
        Dim objCon As New SqlConnection
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_GetMesseges", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PatientID", nPatientID)
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
    Public Function Fill_PatientCommunication(ByVal nPatientID As Long) As DataTable
        Dim objCon As New SqlConnection
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_ViewPatientCommunication", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PatientID", nPatientID)
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
    Public Function Fill_PatientOrders(ByVal nPatientID As Long) As DataTable
        Dim objCon As New SqlConnection
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            'Dim cmd As New SqlCommand("gsp_GetPatientOrders", objCon)
            cmd = New SqlCommand("gsp_LM_GetPatientOrders", objCon)
            'LM_Orders.lm_Visit_ID, LM_Orders.lm_OrderDate, LM_Category.lm_category_Description, LM_Test.lm_test_Name, 
            'LM_Orders.lm_NumericResult()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PatientID", nPatientID)

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


    Public Function Fill_PatientOrders_SplitControl(ByVal nPatientID As Long) As DataTable
        Dim objCon As New SqlConnection
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            'Dim cmd As New SqlCommand("gsp_GetPatientOrders", objCon)
            cmd = New SqlCommand("gsp_LM_GetPatientOrders_SplitControl", objCon)
            'LM_Orders.lm_Visit_ID, LM_Orders.lm_OrderDate, LM_Category.lm_category_Description, LM_Test.lm_test_Name, 
            'LM_Orders.lm_NumericResult()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PatientID", nPatientID)

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



    'sarika 10th Feb 09
    'sarika Show Labs info in Patient Details grid on Dashboard
    Public Function Fill_PatientLabOrders(ByVal nPatientID As Long) As DataTable
        Dim objCon As New SqlConnection
        Dim cmd As New SqlCommand() ''"gsp_LM_GetPatientOrders", objCon)
        Dim dt As New DataTable
        Dim da As SqlDataAdapter = Nothing
        Dim strSQL As String = ""

        Try
            objCon.ConnectionString = GetConnectionString()
            strSQL = "SELECT isnull(Lab_Order_MST.labom_OrderID,0) as OrderID,isnull(Lab_Order_TestDtl.labotd_TestID,0) as TestID,'Order Date' =   Lab_Order_MST.labom_TransactionDate  ,'Order No' =   isnull(Lab_Order_MST.labom_OrderNoPrefix,'') + '-' +  convert(varchar, isnull(Lab_Order_MST.labom_OrderNoID,0))   , 'TestName'= isnull(Lab_Test_Mst.labtm_Name,'')  FROM Lab_Order_MST INNER JOIN Lab_Order_TestDtl ON Lab_Order_MST.labom_OrderID = Lab_Order_TestDtl.labotd_OrderID INNER JOIN Lab_Test_Mst ON Lab_Order_TestDtl.labotd_TestID = Lab_Test_Mst.labtm_ID WHERE labom_PatientID = " & nPatientID
            cmd.Connection = objCon
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strSQL
            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            da.Fill(dt)
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
          
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

    Public Function Fill_TestDiagnosis(ByVal OrderID As Long, ByVal TestID As Long) As DataTable
        Dim objCon As New SqlConnection
        Dim cmd As New SqlCommand() ''"gsp_LM_GetPatientOrders", objCon)
        Dim da As SqlDataAdapter = Nothing
        Dim dt As New DataTable
        Dim strSQL As String = ""

        Try
            objCon.ConnectionString = GetConnectionString()

            strSQL = "select isnull(labodtl_Description,'') as DiagnosisCPT from Lab_Order_TestDtl_DiagCPT where labodtl_OrderID = " & OrderID & " and  labodtl_TestID = " & TestID & " and labodtl_Type = 1"

            cmd.Connection = objCon
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strSQL

            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd

            da.Fill(dt)
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
         
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

    Public Function Fill_TestTreatments(ByVal OrderID As Long, ByVal TestID As Long) As DataTable
        Dim objCon As New SqlConnection
        Dim cmd As New SqlCommand() ''"gsp_LM_GetPatientOrders", objCon)
        Dim da As SqlDataAdapter = Nothing
        Dim dt As New DataTable
        Dim strSQL As String = ""

        Try
            objCon.ConnectionString = GetConnectionString()

            strSQL = "select isnull(labodtl_Description,'') as DiagnosisCPT from Lab_Order_TestDtl_DiagCPT where labodtl_OrderID = " & OrderID & " and  labodtl_TestID = " & TestID & " and labodtl_Type = 2"

            cmd.Connection = objCon
            cmd.CommandType = CommandType.Text
            cmd.CommandText = strSQL

            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd

            da.Fill(dt)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
         
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

        Return dt
    End Function
    '------
    'Procedure to fill templates
    Public Function Fill_Templates() As DataTable
        Dim objCon As New SqlConnection
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_FillTemplateGallery_MST", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@flag", 11)
        
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
    Public Function Fill_Templates_ForFillMenu() As DataTable
        Dim objCon As New SqlConnection
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_FillTemplateGallery_MST", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@flag", 21)
            
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

    Public Function Fill_AuditTrails(ByVal nPatientId As Long) As DataTable
        Dim objCon As New SqlConnection
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_GetAuditTrails", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PatientID", nPatientId)
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

    Public Function Fill_FAXStatus(ByVal nPatientId As Long) As DataTable
        Dim objCon As New SqlConnection
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try

            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_GetFAXSetails", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@PatientID", nPatientId)
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

    Public Function Fill_FAXStatus(ByVal enmFAXCriteria As enmCriteria, ByVal dtFAXFromDate As DateTime, ByVal dtFAXTodate As DateTime, Optional ByVal nPatientId As Long = 0) As DataTable

        Dim objCon As New SqlConnection
        Dim objCmd As SqlCommand = Nothing
        Dim objDA As SqlDataAdapter = Nothing
        Dim dsData As DataSet = Nothing
        Dim objSQLDataReader As SqlDataReader = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            objCmd = New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetAllFAXSetails"
            objCmd.Connection = objCon

            'Dim dtFromDate As DateTime
            'Dim dtToDate As DateTime

            'Select Case enmFAXCriteria
            '    Case enmCriteria.Today
            '        dtFromDate = System.DateTime.Now
            '    Case enmCriteria.Yesterday
            '        dtFromDate = System.DateTime.Now.AddDays(-1)
            '    Case enmCriteria.LastWeek
            '        dtFromDate = System.DateTime.Now.AddDays(-7)
            '        dtToDate = System.DateTime.Now
            '    Case enmCriteria.LastMonth
            '        dtFromDate = System.DateTime.Now.AddMonths(-1)
            '        dtToDate = System.DateTime.Now
            '    Case enmCriteria.Customize
            '        dtFromDate = dtFAXFromDate
            '        dtToDate = dtFAXTodate.AddDays(1)
            'End Select
            '''''''''''''''''''Code modifications are done by Anil on 20071113
            Dim _DateRange() As DateTime

            Select Case enmFAXCriteria
                Case enmCriteria.Today
                    _DateRange = GetDateRange(DateCategory.Today)
                    If _DateRange.Length > 0 Then
                        dtFAXFromDate = _DateRange(0)
                        dtFAXTodate = _DateRange(1)
                    End If
                Case enmCriteria.Yesterday
                    _DateRange = GetDateRange(DateCategory.Yesterday)
                    If _DateRange.Length > 0 Then
                        dtFAXFromDate = _DateRange(0)
                        dtFAXTodate = _DateRange(1)
                    End If
                Case enmCriteria.LastWeek
                    _DateRange = GetDateRange(DateCategory.LastWeek)
                    If _DateRange.Length > 0 Then
                        dtFAXFromDate = _DateRange(0)
                        dtFAXTodate = _DateRange(1)
                    End If
                Case enmCriteria.LastMonth
                    _DateRange = GetDateRange(DateCategory.LastMonth)
                    If _DateRange.Length > 0 Then
                        dtFAXFromDate = _DateRange(0)
                        dtFAXTodate = _DateRange(1)
                    End If
                Case enmCriteria.Customize
                    dtFAXFromDate = dtFAXFromDate.Date
                    dtFAXTodate = dtFAXTodate.Date
            End Select
            ''''''''''''''''''''''''''''''''''''''''''''''

            Dim objParaFromDate As New SqlParameter
            With objParaFromDate
                .ParameterName = "@FromDate"
                .Value = dtFAXFromDate.Date
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.DateTime
            End With
            objCmd.Parameters.Add(objParaFromDate)
            If Not (enmFAXCriteria = enmCriteria.Today OrElse enmFAXCriteria = enmCriteria.Yesterday) Then
                Dim objParaToDate As New SqlParameter
                With objParaToDate
                    .ParameterName = "@ToDate"
                    .Value = dtFAXTodate.Date
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.DateTime
                End With
                objCmd.Parameters.Add(objParaToDate)
                objParaToDate = Nothing
            End If

            Dim objParaPatientID As New SqlParameter
            With objParaPatientID
                .ParameterName = "@PatientID"
                .Value = nPatientId
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaPatientID)

            objCmd.Connection = objCon
            objCon.Open()
            objDA = New SqlDataAdapter(objCmd)
            dsData = New DataSet
            objDA.Fill(dsData)
            objCon.Close()
            objCon = Nothing

            objParaFromDate = Nothing
            objParaPatientID = Nothing            
            Dim myTable As DataTable = dsData.Tables(0).Copy()
            Return myTable
        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(dsData) Then
                dsData.Dispose()
                dsData = Nothing
            End If
            If Not IsNothing(objDA) Then
                objDA.Dispose()
                objDA = Nothing
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

    'Procedure to get user to whom message has been sent by passing message ID
    Public Function GetMessageToUser(ByVal nMessageId As Long) As DataTable

        Dim objCon As New SqlConnection
        Dim Cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_GetMessageToUser", objCon)
            cmd.CommandType = CommandType.StoredProcedure
            Cmd.Parameters.AddWithValue("@MessageId", nMessageId)
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
           
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If Not IsNothing(Cmd) Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If

        End Try
    End Function

    Public Function GetPatientFullName(ByVal nPatientID As Long) As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try

            objCon.ConnectionString = GetConnectionString()
            objCmd = New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetPatientFULLName"
            objCmd.Connection = objCon

            'Dim dtFromDate As DateTime
            'Dim dtToDate As DateTime


            Dim objParaPatientID As New SqlParameter
            With objParaPatientID
                .ParameterName = "@PatientID"
                .Value = nPatientID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaPatientID)
            objCmd.Connection = objCon
            'objCon.Open()
            'Dim strPatientName As String
            'If IsNothing(objCmd.ExecuteNonQuery) = False Then
            '    strPatientName = objCmd.ExecuteScalar
            'End If
            da = New SqlDataAdapter(objCmd)
            dt = New DataTable
            da.Fill(dt)
            objParaPatientID = Nothing

            Return dt
        Catch ex As Exception
            Return Nothing
        Finally
           
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
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

    Public Function GetTemplateID(ByVal strTemplateName As String) As Long
        Dim nTemplateID As Long = 0
        Dim objCon As New SqlConnection
        Dim objCmd As SqlCommand = Nothing
        Try

            objCon.ConnectionString = GetConnectionString()
            objCmd = New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_RetrieveTemplateID"
            objCmd.Connection = objCon


            Dim objParaName As New SqlParameter
            With objParaName
                .ParameterName = "@TemplateName"
                .Value = strTemplateName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaName)
            objCmd.Connection = objCon
            objCon.Open()
            nTemplateID = objCmd.ExecuteScalar
            objCon.Close()
            objParaName = Nothing
            Return nTemplateID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return nTemplateID
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
    End Function
    Public Function GetTemplateID(ByVal strTemplateName As String, ByVal strCategoryName As String) As Long
        Dim nTemplateID As Long = 0
        Dim objCon As New SqlConnection
        Dim objCmd As SqlCommand = Nothing
        Try

            objCon.ConnectionString = GetConnectionString()
            objCmd = New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_RetrieveTemplateID"
            objCmd.Connection = objCon


            Dim objParaName As New SqlParameter
            With objParaName
                .ParameterName = "@TemplateName"
                .Value = strTemplateName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaName)
            objParaName = Nothing

            Dim objParaCategory As New SqlParameter
            With objParaCategory
                .ParameterName = "@TemplateCategory"
                .Value = strCategoryName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaCategory)
            objParaCategory = Nothing

            objCmd.Connection = objCon
            objCon.Open()
            nTemplateID = objCmd.ExecuteScalar
            objCon.Close()
            objCon = Nothing
            Return nTemplateID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return nTemplateID
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
    End Function

    '' By Mahesh  
#Region "Exam Status"
    'function commented by dipak as not in use
    '' To Insert the Exam Status 
    'Public Function SetExamStatus(ByVal dtDate As Date, ByVal ExamSatus As MainMenu.enmExamStatus, Optional ByVal ExamName As String = "") As Boolean
    '    Dim oCon As SqlConnection
    '    Dim oCmd As SqlCommand
    '    Try
    '        oCon = New SqlConnection
    '        oCon.ConnectionString = GetConnectionString()
    '        oCmd = New SqlCommand("gsp_InsertPatientExamStatus", oCon)
    '        oCmd.CommandType = CommandType.StoredProcedure
    '        Dim SQLParam As SqlParameter

    '        SQLParam = New SqlParameter
    '        With SQLParam
    '            .ParameterName = "@PatientID"
    '            .Value = gnPatientID
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.BigInt
    '        End With
    '        oCmd.Parameters.Add(SQLParam)

    '        SQLParam = New SqlParameter
    '        With SQLParam
    '            .ParameterName = "@dtDate"
    '            .Value = dtDate.Date
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.DateTime
    '        End With
    '        oCmd.Parameters.Add(SQLParam)

    '        SQLParam = New SqlParameter
    '        With SQLParam
    '            .ParameterName = "@ExamName"
    '            .Value = ExamName
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        oCmd.Parameters.Add(SQLParam)

    '        SQLParam = New SqlParameter
    '        With SQLParam
    '            .ParameterName = "@Status"
    '            .Value = ExamSatus
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.Int
    '        End With
    '        oCmd.Parameters.Add(SQLParam)

    '        oCon.Open()
    '        oCmd.ExecuteNonQuery()
    '        oCon.Close()
    '        Return True
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '        Return False
    '    Finally
    '        oCon = Nothing
    '        oCmd = Nothing
    '    End Try
    'End Function

    '''' Check for Patient Exam Status 
    'function commented as nnot in use
    'Public Function CheckPatientExamStatus(ByVal ExamName As String, ByVal dtDate As Date) As DataTable
    '    Dim oCon As New SqlConnection
    '    Dim oCmd As SqlCommand
    '    Try
    '        oCon.ConnectionString = GetConnectionString()
    '        oCmd = New SqlCommand("gsp_GetPatientExamStatus", oCon)
    '        oCmd.CommandType = CommandType.StoredProcedure
    '        Dim SQLParam As SqlParameter

    '        SQLParam = New SqlParameter
    '        With SQLParam
    '            .ParameterName = "@PatientID"
    '            .Value = gnPatientID
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.BigInt
    '        End With
    '        oCmd.Parameters.Add(SQLParam)

    '        SQLParam = New SqlParameter
    '        With SQLParam
    '            .ParameterName = "@dtDate"
    '            .Value = dtDate.Date
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.DateTime
    '        End With
    '        oCmd.Parameters.Add(SQLParam)

    '        'SQLParam = New SqlParameter
    '        'With SQLParam
    '        '    .ParameterName = "@ExamName"
    '        '    .Value = ExamName
    '        '    .Direction = ParameterDirection.Input
    '        '    .SqlDbType = SqlDbType.VarChar
    '        'End With
    '        'oCmd.Parameters.Add(SQLParam)

    '        Dim da As SqlDataAdapter
    '        Dim dt As DataTable
    '        oCon.Open()
    '        da = New SqlDataAdapter
    '        da.SelectCommand = oCmd
    '        dt = New DataTable
    '        da.Fill(dt)

    '        Return dt
    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    Finally
    '        oCmd = Nothing
    '        oCon = Nothing
    '    End Try
    'End Function
    'Function commented as not in use
    'Public Function DeletePatientExamStatus(ByVal ExamName As String, ByVal dtDate As Date, ByVal Status As MainMenu.enmExamStatus) As Boolean            '' Check for Patient Exam Status 
    '    Dim oCon As New SqlConnection
    '    Dim oCmd As SqlCommand
    '    Try
    '        oCon.ConnectionString = GetConnectionString()
    '        oCmd = New SqlCommand("gsp_DeletePatientExamStatus", oCon)
    '        oCmd.CommandType = CommandType.StoredProcedure
    '        Dim SQLParam As SqlParameter

    '        SQLParam = New SqlParameter
    '        With SQLParam
    '            .ParameterName = "@PatientID"
    '            .Value = gnPatientID
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.BigInt
    '        End With
    '        oCmd.Parameters.Add(SQLParam)

    '        SQLParam = New SqlParameter
    '        With SQLParam
    '            .ParameterName = "@dtDate"
    '            .Value = dtDate.Date
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.DateTime
    '        End With
    '        oCmd.Parameters.Add(SQLParam)

    '        SQLParam = New SqlParameter
    '        With SQLParam
    '            .ParameterName = "@ExamName"
    '            .Value = ExamName
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.VarChar
    '        End With
    '        oCmd.Parameters.Add(SQLParam)

    '        SQLParam = New SqlParameter
    '        With SQLParam
    '            .ParameterName = "@Status"
    '            SQLParam.Value = Status
    '            .Direction = ParameterDirection.Input
    '            .SqlDbType = SqlDbType.Int
    '        End With
    '        oCmd.Parameters.Add(SQLParam)

    '        oCon.Open()
    '        oCmd.ExecuteNonQuery()
    '        oCon.Close()

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
    '    Finally
    '        oCmd = Nothing
    '        oCon = Nothing
    '    End Try
    'End Function
    'function commented as not in use
    'Public Function IsExamExists(ByVal dtDate As Date) As Boolean
    '    '       gsp_IsExamExists()

    '    ''      @dtDate DateTime,
    '    ''      @PatientID Numeric(18,0)
    '    Dim oCon As New SqlConnection
    '    Dim oCmd As SqlCommand
    '    Dim oPara As SqlParameter

    '    oCon.ConnectionString = GetConnectionString()
    '    oCmd = New SqlCommand("gsp_IsExamExists", oCon)
    '    oCmd.CommandType = CommandType.StoredProcedure

    '    oPara = New SqlParameter
    '    With oPara
    '        .ParameterName = "@dtDate"
    '        .Value = dtDate.Date
    '        .SqlDbType = SqlDbType.DateTime
    '    End With
    '    oCmd.Parameters.Add(oPara)

    '    oPara = New SqlParameter
    '    With oPara
    '        .ParameterName = "@PatientID"
    '        .Value = gnPatientID
    '        .SqlDbType = SqlDbType.BigInt
    '    End With
    '    oCmd.Parameters.Add(oPara)

    '    Dim oCount As Int16
    '    oCon.Open()
    '    oCount = CInt(oCmd.ExecuteScalar())
    '    oCon.Close()
    '    oCmd = Nothing

    '    If oCount > 0 Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

#End Region

    'sarika Add Providers to ChartPull 20081118

    Public Function Fill_Providers() As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        '    Dim cmd As New SqlCommand("select 0 as nProviderID,'All' as ProviderName  union select nProviderID, isnull(sfirstname,'') + ' ' + isnull(smiddlename,'') + ' ' + isnull(slastname,'') as ProviderName from Provider_Mst order by ProviderName", objCon)
        'SHUBHANGI 20100616 REMOVE MIDDLE NAME SPACE FOR CASE 4942
        Dim cmd As New SqlCommand(" select nProviderID, isnull(sfirstname,'') + ' ' + CASE ISNULL(Provider_MST.sMiddleName,'') WHEN  '' THEN '' When Provider_MST.sMiddleName then  " _
                                 & " Provider_MST.sMiddleName +' ' END + isnull(slastname,'') as ProviderName from Provider_Mst  WHERE isnull(bIsBlocked,0) = 0 order by ProviderName", objCon)
        cmd.CommandType = CommandType.Text

        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing

        Try
            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            Return (dt)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.PatientSummary, gloAuditTrail.ActivityCategory.ProblemType, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
        Finally
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
         
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

        Return dt
    End Function
    '---

    'sarika Add Providers to ChartPull 20081201
    'fill Provider specific templates
    Public Function Fill_ProviderTemplates(ByVal ProviderID As Long) As DataTable
        Dim objCon As New SqlConnection
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("select nTemplateID,sTemplateName from TemplateGallery_MST tm inner join category_mst cm on tm.ncategoryid=cm.ncategoryid where cm.sDescription='Soap'   and tm.nProviderID = " & ProviderID & " order by sTemplateName", objCon)
            cmd.CommandType = CommandType.Text
            ' cmd.Parameters.Add("@flag", 11)
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


    Public Function GetTemplateCategory() As DataTable
        'Changes for new Template Category parameter
        Dim objCon As New SqlConnection
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Try
            objCon.ConnectionString = GetConnectionString()
            cmd = New SqlCommand("gsp_GetTemplateCategory", objCon)
            cmd.CommandType = CommandType.StoredProcedure

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

            If Not IsNothing(da) Then
                da.Dispose() : da = Nothing
            End If

            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear() : cmd.Dispose() : cmd = Nothing
            End If

            If Not IsNothing(objCon) Then
                objCon.Dispose() : objCon = Nothing
            End If

            If Not IsNothing(dt) Then
                dt.Dispose() : dt = Nothing
            End If

        End Try
    End Function

    Private disposed As Boolean = False

    Public Sub Dispose() Implements System.IDisposable.Dispose
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
