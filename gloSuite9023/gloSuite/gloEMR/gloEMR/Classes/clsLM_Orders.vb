
Imports System.Data.SqlClient
Imports System.IO
Imports gloEMRGeneralLibrary.gloEMRDatabase

Public Class clsLM_Orders
    Implements IDisposable
    '********************************
    'Private da As SqlDataAdapter

    '08-May-13 Aniket: Resolving Memory Leaks
    'Private ds As New DataSet
    ' Private dt As DataTable
    Private dv As DataView = Nothing
    Private Con As SqlConnection = Nothing
    'Private conString As String
    Public Sub New()
        Try
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
        Catch ex As Exception   ' Catch the error.
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        End Try
    End Sub

    '08-May-13 Aniket: Resolving Memory Leaks
    'Public ReadOnly Property GetDataSet() As DataSet
    '    Get
    '        'Dv = Ds.Tables("Category_Mst").DefaultView
    '        Return ds
    '        'Return Ds
    '    End Get
    'End Property
    Private _OrderTemplate As String = "OrderTemplate"
    Public ReadOnly Property OrderTemplate() As String
        Get
            Return _OrderTemplate
        End Get
    End Property
    Public ReadOnly Property GetDataview() As DataView
        Get
            'Dv = Ds.Tables("Category_Mst").DefaultView
            Return dv
            'Return Ds
        End Get
    End Property

    Public Function Search(ByVal dv As DataView, ByVal colIndex As Integer, ByVal txtSearch As String) As DataView
        'Dim dv As DataView
        'dv = grdCPT.DataSource
        If (IsNothing(dv) = False) Then
            dv.RowFilter = "" & dv.Table.Columns(colIndex).ColumnName & " Like '" & txtSearch & "%'"
        End If

        Return dv
        'grdCPT.DataSource = dv
    End Function

    Public Sub SetRowFilter(ByVal txtSearch As String)
        Dim strexpr As String
        Dim str As String
        If (IsNothing(dv) = False) Then
            str = dv.Sort
            str = Mid(str, 2)
            str = Mid(str, 1, Len(str) - 1)
            strexpr = "" & dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
            dv.RowFilter = strexpr
        End If

    End Sub

    Public Sub SortDataview(ByVal strsort As String)
        If (IsNothing(dv) = False) Then
            dv.Sort = "[" & strsort & "]"
        End If

    End Sub

    Public Function GetAllTests() As DataTable

        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim c As New DataColumn("Select", System.Type.GetType("System.Boolean"))

        Try
            cmd = New SqlCommand("gsp_LM_SelectTest", Con)
            cmd.CommandType = CommandType.StoredProcedure

            da = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable


            dt.Columns.Add(c)


            da.Fill(dt)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'If IsNothing(dt) = False Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            c.Dispose()
            c = Nothing
            If IsNothing(da) = False Then
                da.Dispose()
                da = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    '' to fill OrderTypes from Orders for Modify 
    Public Function GetOrderType(ByVal PatientID As Long, ByVal VisitID As Long, ByVal OrderType As String) As DataView
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing

        Try
            cmd = New SqlCommand("gsp_FillOrdersType", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = cmd.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = "History"

            sqlParam = cmd.Parameters.Add("@OrderType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = "History"
            sqlParam.Value = OrderType

            Con.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            If IsNothing(dv) = False Then
                dv.Dispose()
                dv = Nothing
            End If

            dv = New DataView(dt.Copy())
            If IsNothing(dt) = False Then
                dt.Dispose()
                dt = Nothing
            End If
            sqlParam = Nothing
            Return dv

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If IsNothing(da) = False Then
                da.Dispose()
                da = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            'objBusLayer = Nothing
            Con.Close()
        End Try
    End Function

    '' to Fill OrderTypes from Orders for Modify 
    Public Function GetOrders(ByVal PatientID As Long, ByVal VisitID As Long, ByVal OrderDate As Date) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing

        Try
            cmd = New SqlCommand("gsp_LM_GetOrders", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = cmd.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = "History"
            sqlParam.Value = VisitID

            sqlParam = cmd.Parameters.Add("@dtOrderDate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = "History"
            sqlParam.Value = OrderDate

            Con.Open()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            sqlParam = Nothing
            Return (dt)
            '' , , , , 
            ''dt(0)= lm_Visit_ID,
            ''dt(1) = lm_test_ID
            ''dt(2)= lm_NumericResult, 
            ''dt(3) = lm_Result
            ''dt(4) = lm_IsFinished 
            ''dt(5) = lm_sICD9Code,
            ''dt(6) = lm_sICD9Description 

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'objBusLayer = Nothing
            If IsNothing(da) = False Then
                da.Dispose()
                da = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Con.Close()
        End Try
    End Function

    Public Function GetAllVisits(ByVal PatientID As Long) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing

        Try
            cmd = New SqlCommand("gsp_GetNoOfVisits", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter
            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            Con.Open()

            da = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            sqlParam = Nothing
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'objBusLayer = Nothing

            If IsNothing(da) = False Then
                da.Dispose()
                da = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Con.Close()
        End Try
    End Function

    'Fill Previous Orders in trvOrderDetails, According to Interval Selected
    Public Function GetPrevOrders(ByVal Interval As String, ByVal PatientID As Long, ByVal Sysdate As Date) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing

        Try
            cmd = New SqlCommand("gsp_LM_ViewOrders", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@Interval", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Interval

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = cmd.Parameters.Add("@dtSysdate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Sysdate

            Con.Open()

            da = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            sqlParam = Nothing
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'objBusLayer = Nothinge

            If IsNothing(da) = False Then
                da.Dispose()
                da = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Con.Close()
        End Try
    End Function

    Public Function DeleteOrders(ByVal VisitID As Long, ByVal PatientID As Long, ByVal dtOrderDate As Date, ByVal TaskID As Long, ByVal Testname As String) As Boolean

        Dim cmd As SqlCommand = Nothing
        Dim cmdDELTask As SqlCommand = Nothing
        Dim trOrder As SqlTransaction = Nothing
        Dim sqlParam As SqlParameter = Nothing

        Try

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            trOrder = Con.BeginTransaction

            cmd = New SqlCommand("gsp_LM_DeleteOrder", Con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Transaction = trOrder


            sqlParam = cmd.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = cmd.Parameters.Add("@dtOrderDate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = dtOrderDate


            sqlParam = cmd.Parameters.Add("@test", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Testname

            cmd.ExecuteNonQuery()

            cmd.Parameters.Clear()

            '' Deletes Both Task Master & Detail 
            cmdDELTask = New SqlCommand("gsp_DeleteTasks_Mst", Con)
            cmdDELTask.CommandType = CommandType.StoredProcedure
            cmdDELTask.Transaction = trOrder

            sqlParam = cmdDELTask.Parameters.Add("@nTaskId", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = TaskID

            cmdDELTask.ExecuteNonQuery()

            trOrder.Commit()
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Add, "Order added", gloAuditTrail.ActivityOutCome.Success)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Delete, "Order deleted", gloAuditTrail.ActivityOutCome.Success)

            sqlParam = Nothing
            Return True

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Success)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False

        Finally

            If IsNothing(cmdDELTask) = False Then
                cmdDELTask.Parameters.Clear()
                cmdDELTask.Dispose()
                cmdDELTask = Nothing
            End If

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            '18-Mar-14 Aniket: Resolving Memory Leaks
            If IsNothing(trOrder) = False Then
                trOrder.Dispose()
                trOrder = Nothing
            End If

            '18-Mar-14 Aniket: Resolving Memory Leaks
            If IsNothing(sqlParam) = False Then
                'sqlParam.Dispose()
                sqlParam = Nothing
            End If


            Con.Close()
        End Try

    End Function


    '' Update Orders (Delete - Insert) 
    '' By Mahesh

    Public Function UpdateOrders(ByVal VisitID As Long, ByVal PatientID As Long, ByVal dtOrderDate As Date, ByVal Arrlst As ArrayList)
        Dim trOrder As SqlTransaction = Nothing

        Try
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            trOrder = Con.BeginTransaction

            'Dim cmddelete As SqlCommand

            Dim cmddelete As New SqlCommand("gsp_DeleteOrders_Patient", Con)  ''Rename sp_DeleteOrders to gsp_DeleteOrders_Patient
            cmddelete.CommandType = CommandType.StoredProcedure
            cmddelete.Transaction = trOrder

            Dim sqlParam1 As SqlParameter

            sqlParam1 = cmddelete.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam1.Direction = ParameterDirection.Input
            sqlParam1.Value = VisitID

            sqlParam1 = cmddelete.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam1.Direction = ParameterDirection.Input

            sqlParam1 = cmddelete.Parameters.Add("@dtOrderDate", SqlDbType.DateTime)
            sqlParam1.Direction = ParameterDirection.Input
            sqlParam1.Value = dtOrderDate

            cmddelete.ExecuteNonQuery()
            If cmddelete IsNot Nothing Then
                cmddelete.Parameters.Clear()
                cmddelete.Dispose()
                cmddelete = Nothing
            End If
            cmddelete = Nothing

            sqlParam1 = Nothing
            Dim i As Integer
            For i = 0 To Arrlst.Count - 1

                Dim cmd As New SqlCommand("gsp_InUpOrders", Con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Transaction = trOrder

                Dim sqlParam As SqlParameter

                sqlParam = cmd.Parameters.Add("@OrderID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0  '' OrderID

                sqlParam = cmd.Parameters.AddWithValue("@VisitID", VisitID)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = VisitID

                sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
                sqlParam.Direction = ParameterDirection.Input

                '20080922 sqlParam = cmd.Parameters.Add("@ProviderID", gnDoctorID)
                sqlParam = cmd.Parameters.Add("@ProviderID", SqlDbType.BigInt)
                sqlParam.Value = gnLoginProviderID
                sqlParam.Direction = ParameterDirection.Input

                sqlParam = cmd.Parameters.Add("@dtOrderDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = dtOrderDate '' Order Time

                sqlParam = cmd.Parameters.Add("@LabsorRadID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = CType(Arrlst(i), myList).Index  '' LabsOrRadID

                sqlParam = cmd.Parameters.Add("@OrderType", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = CType(Arrlst(i), myList).Description  '' "Labs" Or "Radiology"

                sqlParam = cmd.Parameters.Add("@Result", SqlDbType.Image)
                sqlParam.Direction = ParameterDirection.Input
                'SLR: Assigned directly lst.TemplateResult on 12/2/2014
                If (IsNothing(CType(Arrlst(i), myList).TemplateResult) = False) Then
                    sqlParam.Value = CType((CType(Arrlst(i), myList).TemplateResult), Byte()).Clone()
                Else
                    sqlParam.Value = DBNull.Value
                End If
                'If IsNothing(CType(Arrlst(i), myList).TemplateResult) = False Then
                '    Dim mstream As ADODB.Stream
                '    mstream = New ADODB.Stream
                '    mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
                '    mstream.Open()
                '    mstream.Write(CType(Arrlst(i), myList).TemplateResult)   '' Template (Object)

                '    Dim strFileName As String
                '    strFileName = ExamNewDocumentName

                '    'mstream.SaveToFile(Application.StartupPath & "\Temp\Temp9.doc", ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
                '    mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
                '    sqlParam.Value = mstream.Read
                '    mstream.Close()
                '    mstream = Nothing

                'End If

                sqlParam = cmd.Parameters.Add("@NumericResult", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                If CType(Arrlst(i), myList).IsFinished = True Then '' Order StaTus
                    sqlParam.Value = 1 '' Finished
                Else
                    sqlParam.Value = 0 '' Not Finished 
                End If

                If Con.State = ConnectionState.Closed Then
                    Con.Open()
                End If

                sqlParam = cmd.Parameters.AddWithValue("@MachineID", GetPrefixTransactionID(PatientID))
                sqlParam.Direction = ParameterDirection.Input

                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                sqlParam = Nothing
            Next

            trOrder.Commit()

            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Add, "Order added", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101009
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Add, "Order added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If IsNothing(trOrder) = False Then
                trOrder.Rollback()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If IsNothing(trOrder) = False Then
                trOrder.Rollback()

            End If
        Finally
            Con.Close()
            If IsNothing(trOrder) = False Then
                trOrder.Dispose()
                trOrder = Nothing
            End If

        End Try
        Return Nothing
    End Function

    Public Function Add_LM_Orders(ByVal VisitID As Long, ByVal PatientID As Long, ByVal dtOrderDate As Date, ByVal ProviderID As Long, ByVal Arrlst As ArrayList)
        Dim sqlParam1 As SqlParameter
        Dim trOrder As SqlTransaction = Nothing
        Try
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If


            trOrder = Con.BeginTransaction


            Dim cmddelete As New SqlCommand("gsp_LM_DeleteOrders", Con)
            cmddelete.CommandType = CommandType.StoredProcedure
            cmddelete.Transaction = trOrder

            sqlParam1 = cmddelete.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam1.Direction = ParameterDirection.Input
            sqlParam1.Value = VisitID

            sqlParam1 = cmddelete.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam1.Direction = ParameterDirection.Input

            sqlParam1 = cmddelete.Parameters.Add("@dtOrderDate", SqlDbType.DateTime)
            sqlParam1.Direction = ParameterDirection.Input
            sqlParam1.Value = dtOrderDate

            cmddelete.ExecuteNonQuery()
            cmddelete.Parameters.Clear()
            cmddelete.Dispose()
            cmddelete = Nothing

            Dim i As Integer
            For i = 0 To Arrlst.Count - 1

                Dim cmd As New SqlCommand("gsp_LM_InUpOrders", Con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Transaction = trOrder

                Dim sqlParam As SqlParameter

                sqlParam = cmd.Parameters.Add("@OrderID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = 0  '' OrderID

                sqlParam = cmd.Parameters.AddWithValue("@VisitID", VisitID)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = VisitID

                sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam = cmd.Parameters.AddWithValue("@ProviderID", ProviderID)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam = cmd.Parameters.Add("@dtOrderDate", SqlDbType.DateTime)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = dtOrderDate '' Order Time

                sqlParam = cmd.Parameters.Add("@TestID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = CType(Arrlst(i), myList).Index  '' TestID

                sqlParam = cmd.Parameters.Add("@Result", SqlDbType.Image)
                sqlParam.Direction = ParameterDirection.Input
                'SLR: Assigned directly lst.TemplateResult on 12/2/2014
                If (IsNothing(CType(Arrlst(i), myList).TemplateResult) = False) Then
                    sqlParam.Value = CType((CType(Arrlst(i), myList).TemplateResult), Byte()).Clone()
                Else
                    sqlParam.Value = DBNull.Value
                End If
                'If IsNothing(CType(Arrlst(i), myList).TemplateResult) = False Then
                '    Dim mstream As ADODB.Stream
                '    mstream = New ADODB.Stream
                '    mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
                '    mstream.Open()
                '    mstream.Write(CType(Arrlst(i), myList).TemplateResult)   '' Template (Object)

                '    Dim strFileName As String
                '    strFileName = ExamNewDocumentName

                '    'mstream.SaveToFile(Application.StartupPath & "\Temp\Temp9.doc", ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
                '    mstream.SaveToFile(strFileName, ADODB.SaveOptionsEnum.adSaveCreateOverWrite)
                '    sqlParam.Value = mstream.Read
                '    mstream.Close()
                '    mstream = Nothing

                'End If

                sqlParam = cmd.Parameters.Add("@NumericResult", SqlDbType.Decimal)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = Val(CType(Arrlst(i), myList).Value) '' Numeric Result Of Test

                sqlParam = cmd.Parameters.Add("@IsFinish", SqlDbType.Bit)
                sqlParam.Direction = ParameterDirection.Input
                If CType(Arrlst(i), myList).IsFinished = True Then '' Order StaTus
                    sqlParam.Value = 1 '' Finished
                Else
                    sqlParam.Value = 0 '' Not Finished 
                End If

                sqlParam = cmd.Parameters.Add("@Status", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = CType(Arrlst(i), myList).OrderComment.GetHashCode  '' STATUS OF ORDER COMMENT '' WHETHER IT ASSIGNED OR NOT ''

                If Con.State = ConnectionState.Closed Then
                    Con.Open()
                End If


                ''By Mahesh 20070129 

                Dim str() As String
                str = Split(CType(Arrlst(i), myList).Description, "-", 2)

                sqlParam = cmd.Parameters.Add("@sICD9Code", SqlDbType.VarChar, 255)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = str(0).Trim  '' ICD9Code

                sqlParam = cmd.Parameters.Add("@sICD9Description", SqlDbType.VarChar, 255)
                sqlParam.Direction = ParameterDirection.Input
                If str.Length > 1 Then
                    sqlParam.Value = str(1).Trim '' ICD9decription
                Else
                    sqlParam.Value = ""
                End If

                ''''Add by Pramod For CCHIT 2007 Store DMS ID
                sqlParam = cmd.Parameters.Add("@nDMSID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                ' CType(Arrlst(i), myList).DMSID = Nothing
                If IsNothing(CType(Arrlst(i), myList).DMSID) OrElse CType(Arrlst(i), myList).DMSID = "" Then 'If Statement by sudhir 20090131
                    sqlParam.Value = 0
                Else
                    sqlParam.Value = CType(Arrlst(i), myList).DMSID
                End If

                ''''END
                sqlParam = cmd.Parameters.Add("@lm_DICOMID", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If IsNothing(CType(Arrlst(i), myList).DICOMID) Then 'If Statement by sudhir 20090131
                    sqlParam.Value = 0
                Else
                    If (CType(Arrlst(i), myList).DICOMID) = "" Then
                        sqlParam.Value = 0
                    Else
                        sqlParam.Value = Convert.ToString(CType(Arrlst(i), myList).DICOMID)
                    End If

                End If
                ''****
                '' SUDHIR 20090420 '' DONE FOR ORDER DENORMALISATION ''

                sqlParam = cmd.Parameters.Add("@sCategoryName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = CType(Arrlst(i), myList).HistoryCategory '' CATEGORY NAME

                sqlParam = cmd.Parameters.Add("@sGroupName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = CType(Arrlst(i), myList).Group '' GROUP NAME

                sqlParam = cmd.Parameters.Add("@sTestName", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = CType(Arrlst(i), myList).HistoryItem '' TEST NAME

                ''END SUDHIR

                '' chetan added for Patient Tracking maintaining user name on 18-oct-2010
                sqlParam = cmd.Parameters.Add("@sTranUser", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = gstrLoginName  '' Login User Name
                '' chetan added for Patient Tracking maintaining user name on 18-oct-2010

                sqlParam = cmd.Parameters.AddWithValue("@MachineID", GetPrefixTransactionID(PatientID))
                sqlParam.Direction = ParameterDirection.Input

                ''Added Rahul for Status,TextComment & LoincId on 20101021.
                sqlParam = cmd.Parameters.Add("@LM_sStatus", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = CType(Arrlst(i), myList).Status '' Status

                sqlParam = cmd.Parameters.Add("@sTextComment", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = CType(Arrlst(i), myList).TextComment '' Text Comment

                sqlParam = cmd.Parameters.Add("@sLonicID", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = CType(Arrlst(i), myList).LoincCode '' LOINC Code
                ''End

                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing

                sqlParam = Nothing
            Next

            trOrder.Commit()
            Con.Close()

            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Add, "Order added", gloAuditTrail.ActivityOutCome.Success)

            If Arrlst.Count > 0 Then

                'If IsAssigned = False Then
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Add, "Order added", gloAuditTrail.ActivityOutCome.Success)
            Else
                '    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Add, "Order added", gloAuditTrail.ActivityOutCome.Success)
                'End If
            End If

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If IsNothing(trOrder) = False Then
                trOrder.Rollback()

            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.Assciation, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If IsNothing(trOrder) = False Then
                trOrder.Rollback()

            End If
        Finally
            Con.Close()
            sqlParam1 = Nothing
            If IsNothing(trOrder) = False Then
                trOrder.Dispose()
                trOrder = Nothing
            End If
        End Try
        Return Nothing
    End Function
    Public Function GetOrderTaskID(ByVal PatientID As Long, ByVal visitDate As Date) As Long
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim Query As String
        Dim TaskID As Int64

        Try
            oDB.Connect(False)
            Query = "SELECT nTaskID FROM TM_TaskMST WHERE nPatientID = " & PatientID & " AND sFaxTiffFileName = '" & visitDate.ToString() & "' AND nTaskType = " & ClsTasksDBLayer.TaskType.OrderRadiology
            TaskID = oDB.ExecuteScalar_Query(Query)
            oDB.Disconnect()
            Return TaskID
        Catch ex As Exception
            Return 0
        Finally
            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function


    Public Function GetTaskID_OfLab(ByVal LabOrderID As Int64) As Int64
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim Query As String
        Dim LabID As Int64

        Try
            oDB.Connect(False)
            Query = "SELECT nTaskID FROM TM_TaskMST WHERE nReferenceID1 = " & LabOrderID
            LabID = oDB.ExecuteScalar_Query(Query)
            oDB.Disconnect()
            Return LabID
        Catch ex As Exception
            Return 0
        Finally
            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function


    Public Function GetTemplate(ByVal TemplateID As Long) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Try
            cmd = New SqlCommand("gsp_GetExamContents", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@nTemplateId", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = TemplateID

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            da = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            sqlParam = Nothing
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If IsNothing(da) = False Then
                da.Dispose()
                da = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            Con.Close()
        End Try

    End Function

    Public Function CheckRecordCount(ByVal strflag As String, ByVal PatientID As Long) As Boolean
        Try
            Dim Cmd As SqlCommand
            Dim count As Int64
            Cmd = New SqlCommand("gsp_LM_CheckRecordcount", Con)
            Cmd.CommandType = CommandType.StoredProcedure

            Dim objParam As SqlParameter
            objParam = Cmd.Parameters.Add("@Interval", SqlDbType.Char)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = strflag

            'objParam = Cmd.Parameters.Add("@PatientID", gnPatientID)
            objParam = Cmd.Parameters.AddWithValue("@PatientID", PatientID)
            objParam.Direction = ParameterDirection.Input

            objParam = Cmd.Parameters.Add("@dtSysDate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Format(Now, "MM/dd/yyyy hh:mm:ss")

            objParam = Cmd.Parameters.Add("@formstatus", SqlDbType.Char)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = "O"

            Con.Open()
            count = Cmd.ExecuteScalar
            Con.Close()
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing

            objParam = Nothing
            If count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
            Return Nothing
        End Try
    End Function

    '26-May-14 Aniket: Resolving Bug #67061. Get Patient Provider instead of Visit Provider
    Public Function Get_ProviderInfo(ByVal PatientID) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Try
            cmd = New SqlCommand("gsp_LM_GetProvider", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatientID

            da = New SqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            sqlParam = Nothing
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If IsNothing(da) = False Then
                da.Dispose()
                da = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    ''Problem No: 00000096 : EMR Settings
    ''Reason: New Dropdownlist added for Provider as suggested in problem.
    ''Description: Function added to get table for all provider.
    Public Function GetAllProvider() As DataTable

        '18-Mar-14 Aniket: Resolving Memory Leaks
        Dim oDB As New DataBaseLayer
        Dim oResultTable As DataTable

        Try

            oResultTable = oDB.GetDataTable("gsp_FillProvider_MST")

            If Not oResultTable Is Nothing Then
                Return oResultTable
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally

            '18-Mar-14 Aniket: Resolving Memory Leaks
            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

    End Function

    Public Function GetOrderStatus() As DataTable

        '18-Mar-14 Aniket: Resolving Memory Leaks
        Dim oDB As New DataBaseLayer
        Dim oResultTable As DataTable

        Try

            oResultTable = oDB.GetDataTable("gsp_GetOrderStatusForFilter")

            If Not oResultTable Is Nothing Then
                Return oResultTable
            Else
                Return Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally

            '18-Mar-14 Aniket: Resolving Memory Leaks
            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If

        End Try

    End Function


    Public Function Fill_Tests(ByVal OrderDate As Date, Optional ByVal TestID As Long = 0, Optional ByVal PatientID As Long = 0) As DataTable

        Dim cmd As SqlCommand = Nothing
        Dim daMain As SqlDataAdapter = Nothing

        Dim c As DataColumn = New DataColumn("Select", System.Type.GetType("System.Boolean"))

        Try

            cmd = New SqlCommand("gsp_LM_SelectTest", Con)
            cmd.CommandType = CommandType.StoredProcedure

            Dim objParam As SqlParameter

            objParam = cmd.Parameters.Add("@TestID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = TestID

            objParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = PatientID

            objParam = cmd.Parameters.Add("@OrderDate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = OrderDate

            daMain = New SqlDataAdapter(cmd)

            Dim dt As New DataTable

            dt.Columns.Add(c)

            daMain.Fill(dt)
            objParam = Nothing
            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString, False)
            Return Nothing
        Finally

            If (IsNothing(c) = False) Then
                c.Dispose()
                c = Nothing

            End If
            If IsNothing(daMain) = False Then
                daMain.Dispose()
                daMain = Nothing
            End If

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try

    End Function

    Public Function GetOrderFromVisitID(ByVal VisitID As Int64) As DateTime
        Dim OrderDate As String = ""
        Dim Result As DateTime
        Dim strSQL As String = ""
        Dim oDB As New DataBaseLayer
        Dim objDBParameter As DBParameter

        Try
            ' _strSQL = ""

            oDB.DBParametersCol.Clear()
            objDBParameter = New DBParameter
            objDBParameter.Direction = ParameterDirection.Input
            objDBParameter.DataType = SqlDbType.BigInt
            objDBParameter.Value = VisitID
            objDBParameter.Name = "@nVisitID"
            oDB.DBParametersCol.Add(objDBParameter)

            '_OrderID = _gloEMRDBID.GetDataValue("Lm_Lab_GetOrderID", True)
            OrderDate = oDB.GetDataValue("Lm_Lab_GetOrderID", True)
            If OrderDate <> "" Then
                If IsDate(OrderDate) Then
                    Result = Convert.ToDateTime(OrderDate)
                End If
            End If
            oDB.Dispose()
            oDB = Nothing
            Return Result
        Catch ex As Exception
            Return Now
        End Try
    End Function


    Public Function GetTodayOrder(ByVal OrderDate As DateTime, ByVal PatientID As Long) As DataTable
        Dim _gloEMROrders As New DataBaseLayer
        Dim dt As DataTable = Nothing
        Try
            Dim strSelectQry As String = "select lm_Order_ID, lm_Visit_ID, lm_Patient_ID, lm_test_ID, lm_Provider_ID, lm_OrderDate, " _
             & " lm_NumericResult, lm_Result, lm_IsFinished, lm_Status, lm_sICD9Code, lm_sICD9Description,lm_DMSID, " _
             & " isnull(lm_DICOMID,'') as lm_DICOMID, ISNULL(lm_sCategoryName,'') AS lm_sCategoryName, " _
             & " ISNULL(lm_sGroupName,'') AS lm_sGroupName, ISNULL(lm_sTestName,'') AS lm_sTestName,ISNULL(lm_sStatus,'') as lm_sStatus, ISNULL(lm_sLonicID,'') AS lm_sLonicID,  ISNULL(lm_sTextComment,'') AS lm_sTextComment from LM_Orders " _
             & " where lm_OrderDate = '" & OrderDate & "' AND lm_Patient_ID = " & PatientID

            'dt = New DataTable
            dt = _gloEMROrders.GetDataTable_Query(strSelectQry)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            _gloEMROrders.Dispose()
            _gloEMROrders = Nothing
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
        End Try
    End Function

    Public Function GetAcknowledment(ByVal OrderID As Int64, ByVal TestId As Int64) As Boolean

        Dim _gloEMROrders As New DataBaseLayer

        Dim strSelectQry As String = "SELECT nOrderId, nTestId, sTestName, nUserID, ReviewDatetime, Comments FROM LM_Acknowledment Where nOrderId = " & OrderID & " and  nTestId = '" & TestId & "' "
        Dim sID As String = ""
        Dim IsRecordPresent As Boolean = False

        sID = _gloEMROrders.GetRecord_Query(strSelectQry)
        If sID <> "" Then
            IsRecordPresent = True
        End If
        _gloEMROrders.Dispose()
        _gloEMROrders = Nothing
        Return IsRecordPresent
    End Function

    ''Problem No: 00000096 : EMR Settings
    ''Reason: New optional parameter for nproviderID is added to get list of order corresponding to login provider or selected provider form provider .
    ''Description: New optional parameter for nproviderID is added and pass to SP.
    Public Function FillOrders(ByVal dtFromDate As DateTime, ByVal dtToDate As DateTime, Optional ByVal _nProviderID As Int64 = 0) As DataTable

        Dim strSelectQry As String
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objDA As SqlDataAdapter = Nothing
        Dim dtData As DataTable

        Try
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_LM_GetAllOrders"
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

            Dim objParaDoctorID As New SqlParameter
            With objParaDoctorID
                .ParameterName = "@nProviderID"
                '20080922 .Value = gnDoctorID
                '20120412 .Value = gnLoginProviderID
                .Value = _nProviderID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaDoctorID)

            objCmd.Connection = objCon
            objCon.Open()
            objDA = New SqlDataAdapter(objCmd)
            dtData = New DataTable
            objDA.Fill(dtData)
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
            For i As Integer = dtData.Rows.Count - 1 To 0 Step -1
                Dim DMSID As Int64 = 0
                'AND CONDDITION IS ADDED TO RESOLVED ISSUE 10215
                If IsDBNull(dtData.Rows(i)("lm_DMSID")) = False AndAlso dtData.Rows(i)("lm_DMSID").ToString() <> "" Then
                    DMSID = dtData.Rows(i)("lm_DMSID")
                End If

                If DMSID <> 0 Then
                    strSelectQry = "Select DocumentID From DMS_MST where DocumentFileName = '" & DMSID & "' and IsReviewed = '0'"

                    objCmd = New SqlCommand(strSelectQry, objCon)
                    Dim IsPresent As Boolean = objCmd.ExecuteScalar()
                    objCmd.Parameters.Clear()
                    objCmd.Dispose()
                    objCmd = Nothing
                    If IsPresent = False Then
                        Dim r As DataRow = dtData.Rows(i)
                        dtData.Rows.Remove(r)
                    End If
                End If
            Next

            objCon.Close()
            objCon.Dispose()
            objCon = Nothing

            objParaFrom = Nothing
            objParaTo = Nothing
            objParaDoctorID = Nothing

            Return dtData

        Catch ex As Exception
            Return Nothing
        Finally
            If IsNothing(objCon) = False Then
                objCon.Close()
                objCon.Dispose()
                objCon = Nothing
            End If

            If IsNothing(objDA) = False Then
                objDA.Dispose()
                objDA = Nothing
            End If
            If IsNothing(objCmd) = False Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
    End Function


    Public Sub Add_Acknowledgment(ByVal OrderID As Int64, ByVal TestID As Int64, ByVal TestName As String, ByVal UserID As Int64, ByVal Reviewdate As DateTime, ByVal comments As String)
        Dim _gloEMRDatabase As New DataBaseLayer
        Try
            '_gloEMRDatabase = Nothing
            '_gloEMRDatabase = New DataBaseLayer
            If Not IsNothing(_gloEMRDatabase.DBParametersCol) Then

                _gloEMRDatabase.DBParametersCol.Clear()

                Dim objDBParameter As DBParameter


                objDBParameter = New DBParameter
                objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = OrderID
                objDBParameter.Name = "@nOrderId"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                ' objDBParameter = Nothing

                'objDBParameter = New DBParameter
                'objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = TestID
                objDBParameter.Name = "@nTestId"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                'objDBParameter = Nothing

                'objDBParameter = New DBParameter
                'objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = TestName
                objDBParameter.Name = "@sTestName"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                'objDBParameter = Nothing

                'objDBParameter = New DBParameter
                'objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.BigInt
                objDBParameter.Value = UserID
                objDBParameter.Name = "@nUserID"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                'objDBParameter = Nothing

                'objDBParameter = New DBParameter
                'objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.DateTime
                objDBParameter.Value = Reviewdate
                objDBParameter.Name = "@ReviewDatetime"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                'objDBParameter = Nothing

                'objDBParameter = New DBParameter
                'objDBParameter.Direction = ParameterDirection.Input
                objDBParameter.DataType = SqlDbType.VarChar
                objDBParameter.Value = comments
                objDBParameter.Name = "@Comments"
                _gloEMRDatabase.DBParametersCol.Add(objDBParameter)
                objDBParameter = Nothing

                _gloEMRDatabase.Add("LM_InsertAcknowledgment")
            End If
        Catch ex As Exception
        Finally
            If IsNothing(_gloEMRDatabase) = False Then
                _gloEMRDatabase.Dispose()
                _gloEMRDatabase = Nothing
            End If
        End Try

    End Sub
    Public Function Update_LM_Orders(ByVal PatientID As Long, ByVal Arrlst As ArrayList)

        Dim trOrder As SqlTransaction = Nothing

        Try

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If


            trOrder = Con.BeginTransaction

            Dim i As Integer
            For i = 0 To Arrlst.Count - 1

                Dim cmd As New SqlCommand("gsp_LM_UpDateDICOMIDs", Con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Transaction = trOrder

                Dim sqlParam As SqlParameter

                sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
                sqlParam.Direction = ParameterDirection.Input


                sqlParam = cmd.Parameters.Add("@TestID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = CType(Arrlst(i), myList).Index  '' TestID

                sqlParam = cmd.Parameters.Add("@lm_DICOMID", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                If IsNothing(CType(Arrlst(i), myList).DICOMID) Then
                    sqlParam.Value = 0
                Else
                    If ((CType(Arrlst(i), myList).DICOMID) = "" Or (CType(Arrlst(i), myList).DICOMID) = "0") Then  '''' Or condition added for solving bug id-10247 (6020)
                        sqlParam.Value = 0
                    Else
                        sqlParam.Value = Convert.ToString(CType(Arrlst(i), myList).DICOMID)
                    End If
                End If

                ''solving bug id-10247 (6020)
                sqlParam = cmd.Parameters.Add("@nOrderID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                If IsNothing(CType(Arrlst(i), myList).OrderID) Then
                    sqlParam.Value = 0
                Else
                    sqlParam.Value = CType(Arrlst(i), myList).OrderID
                End If
                '' End
                cmd.ExecuteNonQuery()
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
                sqlParam = Nothing
            Next

            trOrder.Commit()
            Con.Close()

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If IsNothing(trOrder) = False Then
                trOrder.Rollback()


            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            If IsNothing(trOrder) = False Then
                trOrder.Rollback()

            End If
        Finally
            Con.Close()

            '08-May-13 Aniket: Resolving Memory Leaks
            If IsNothing(trOrder) = False Then
                trOrder.Dispose()
                trOrder = Nothing
            End If


        End Try
        Return Nothing
    End Function









    Public Sub fill_widthofExam(ByRef pnlGloUC_TemplateTreeControl As Panel)
        Dim oDB As DataBaseLayer = Nothing
        Dim oParameter As DBParameter = Nothing
        Dim sDrugForm As String = ""
        Try


            oDB = New DataBaseLayer
            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@nUserID"
            oParameter.Value = gnLoginID
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.VarChar
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@SettingsName"
            oParameter.Value = OrderTemplate
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            oParameter = New DBParameter
            oParameter.DataType = SqlDbType.Int
            oParameter.Direction = ParameterDirection.Input
            oParameter.Name = "@Flag"
            oParameter.Value = 1
            oDB.DBParametersCol.Add(oParameter)
            oParameter = Nothing

            sDrugForm = oDB.GetDataValue("gsp_TemplatePanelWidth", True)

            If IsNumeric(sDrugForm) Then
                pnlGloUC_TemplateTreeControl.Width = sDrugForm
            End If
        Catch ex As Exception
            
        Finally
            If Not IsNothing(oParameter) Then
                oParameter = Nothing
            End If
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Sub

    Public Sub SaveWidthInDatabase(ByVal nUserId As String, ByVal value As Integer)


        Dim oDB As New DataBaseLayer
        Dim oParamater As DBParameter
        Try
            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.BigInt
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@Flag"
            oParamater.Value = 0
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@nUserID"
            oParamater.Value = nUserId
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@SettingsName"
            oParamater.Value = OrderTemplate
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@SettingsValue"
            oParamater.Value = value
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing

            oParamater = New DBParameter
            oParamater.DataType = SqlDbType.VarChar
            oParamater.Direction = ParameterDirection.Input
            oParamater.Name = "@MachinName"
            oParamater.Value = ""
            oDB.DBParametersCol.Add(oParamater)
            oParamater = Nothing
            oDB.Add("gsp_TemplatePanelWidth")

        Catch ex As Exception

        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
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
                If Con IsNot Nothing Then
                    Con.Dispose()
                    Con = Nothing

                End If
                If IsNothing(dv) = False Then
                    dv.Dispose()
                    dv = Nothing
                End If
            End If
        End If
        disposed = True
    End Sub


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
