Imports System.Data
Imports System.Data.SqlClient
Public Class ClsTasksDBLayer
    Implements IDisposable
    '''' 20070102
    Enum TaskType
        Task = 1
        OrderRadiology = 2
        FAX = 3
        LabOrder = 4
        DMS = 5
        Exam = 6
    End Enum

    Public Sub New()
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
    End Sub

    Private Conn As SqlConnection = Nothing
    ' Private Adapter As System.Data.SqlClient.SqlDataAdapter
    'Private sqlreader As System.Data.SqlClient.SqlDataReader
    'Private sqlcmmd As System.Data.SqlClient.SqlCommand
    ' Private Ds As System.Data.DataSet = Nothing
    Private Dv As DataView = Nothing
    ' Private Tb As DataTable
    ' Private Cmd As System.Data.SqlClient.SqlCommand


    Public Function GetLoginId() As Int64
        Dim Cmd As SqlCommand = Nothing
        Try
            'Dim dt As New DataTable
            'Adapter = New System.Data.SqlClient.SqlDataAdapter
            Dim intId As Int64 = 0
            Cmd = New SqlCommand("gsp_GetUser_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@sLogin", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = gstrLoginName

            objParam = Cmd.Parameters.Add("@nUserID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Output


            'Adapter.SelectCommand = Cmd

            'Adapter.Fill(dt)


            'Dv = New DataView(dt)
            Conn.Open()
            Cmd.ExecuteNonQuery()
            If Not IsDBNull(objParam.Value) Then
                intId = objParam.Value
            End If
            Conn.Close()
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
            Return intId
        Catch ex As Exception
            MsgBox(ex.Message)
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If

            Return 0
        End Try
    End Function
    Public Function FetchData(ByVal type) As Boolean
        Dim Cmd As SqlCommand = Nothing
        Try
            Dim dt As New DataTable
            Dim Adapter As SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
            Cmd = New SqlCommand("gsp_ViewTasks_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@sLogin", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = gstrLoginName

            objParam = Cmd.Parameters.Add("@type", SqlDbType.Char)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = type

            Adapter.SelectCommand = Cmd

            Adapter.Fill(dt)
            If IsNothing(Dv) = False Then
                Dv.Dispose()
                Dv = Nothing
            End If
            Dv = New DataView(dt.Copy())
            Conn.Close()
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
            dt.Dispose()
            dt = Nothing
            Adapter.Dispose()
            Adapter = Nothing
            Return True
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            Return False
        End Try
    End Function
    'function commented by dipak as not in use
    'Public Function AddData(ByVal ArrList As ArrayList, ByVal Arr1 As Array, ByVal Type As TaskType, ByVal ReAssignFlag As Boolean) As Boolean
    '    Conn.Open()
    '    Dim trTasks As SqlTransaction
    '    trTasks = Conn.BeginTransaction

    '    Try
    '        Dim objparam As SqlParameter
    '        Dim objParamTaskId As SqlParameter

    '        'Insert data in 

    '        Cmd = New System.Data.SqlClient.SqlCommand("gsp_InUpTasks_Mst", Conn)

    '        Cmd.CommandType = CommandType.StoredProcedure
    '        Cmd.Transaction = trTasks

    '        objParamTaskId = Cmd.Parameters.Add("@nTaskID", SqlDbType.BigInt)
    '        objParamTaskId.Direction = ParameterDirection.InputOutput
    '        If ReAssignFlag = True Then
    '            objParamTaskId.Value = 0
    '        Else
    '            objParamTaskId.Value = CType(ArrList.Item(0), Long)
    '        End If


    '        objparam = Cmd.Parameters.Add("@nFromID", SqlDbType.BigInt)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = CType(ArrList.Item(1), Long)

    '        objparam = Cmd.Parameters.Add("@dtTaskDate", SqlDbType.DateTime)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = CType(ArrList.Item(2), System.DateTime)

    '        objparam = Cmd.Parameters.Add("@sSubject", SqlDbType.VarChar)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = CType(ArrList.Item(3), System.String)

    '        objparam = Cmd.Parameters.Add("@dtDueDate", SqlDbType.DateTime)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = CType(ArrList.Item(4), System.DateTime)

    '        objparam = Cmd.Parameters.Add("@sPriority", SqlDbType.VarChar)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = CType(ArrList.Item(5), System.String)

    '        objparam = Cmd.Parameters.Add("@sStatus", SqlDbType.VarChar)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = CType(ArrList.Item(6), System.String)

    '        objparam = Cmd.Parameters.Add("@sNotes", SqlDbType.VarChar, 255)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = CType(ArrList.Item(7), System.String)

    '        objparam = Cmd.Parameters.Add("@nPatientId", SqlDbType.BigInt)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = CType(ArrList.Item(8), Long)

    '        ''''20070102
    '        objparam = Cmd.Parameters.Add("@TaskType", SqlDbType.Int)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = Type '' To Add TaskType 

    '        If Type = TaskType.Exam Then
    '            objparam = Cmd.Parameters.Add("@FAXTIFFFileName", SqlDbType.VarChar)
    '            objparam.Direction = ParameterDirection.Input
    '            objparam.Value = CType(ArrList.Item(10), String)
    '        End If

    '        ''''

    '        objparam = Cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = GetPrefixTransactionID()

    '        Cmd.ExecuteNonQuery()
    '        Cmd.Parameters.Clear()

    '        Dim i As Integer
    '        For i = 0 To Arr1.Length - 1
    '            Dim cmddetails1 As SqlCommand
    '            cmddetails1 = New SqlCommand("gsp_InsertTasks_DTL", Conn)
    '            cmddetails1.CommandType = CommandType.StoredProcedure

    '            cmddetails1.Transaction = trTasks
    '            objparam = cmddetails1.Parameters.Add("@nTaskID", SqlDbType.BigInt)
    '            objparam.Direction = ParameterDirection.Input
    '            objparam.Value = objParamTaskId.Value

    '            objparam = cmddetails1.Parameters.Add("@nToID", SqlDbType.BigInt)
    '            objparam.Direction = ParameterDirection.Input
    '            objparam.Value = Arr1(i)

    '            cmddetails1.ExecuteNonQuery()
    '            cmddetails1.Parameters.Clear()
    '        Next

    '        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Tasks Added", gstrLoginName, gstrClientMachineName, gnPatientID)
    '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Delete, "Task Deleted", gnPatientID, 0, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success)
    '        trTasks.Commit()
    '        Conn.Close()
    '        Return True
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        trTasks.Rollback()
    '        trTasks = Nothing
    '        Cmd = Nothing
    '        'cmddelete = Nothing
    '        If Conn.State = ConnectionState.Open Then
    '            Conn.Close()
    '        End If
    '        Return False
    '    End Try
    'End Function
    'function commenrted as not in use
    'Public Function UpdateData(ByVal ArrList As ArrayList, ByVal Arr1 As Array, ByVal Type As TaskType) As Boolean
    '    Conn.Open()
    '    Dim trTasks As SqlTransaction
    '    trTasks = Conn.BeginTransaction

    '    Try

    '        Dim objparam As SqlParameter
    '        'Insert data in ICD9CPT

    '        Cmd = New System.Data.SqlClient.SqlCommand("gsp_InUpTasks_Mst", Conn)

    '        Cmd.CommandType = CommandType.StoredProcedure
    '        Cmd.Transaction = trTasks

    '        ''objparam = Cmd.Parameters.Add("@nTaskID", SqlDbType.Int)
    '        ''objparam.Direction = ParameterDirection.InputOutput
    '        ''objparam.Value = CType(ArrList.Item(0), System.Int64)

    '        objparam = Cmd.Parameters.Add("@nFromID", SqlDbType.BigInt)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = CType(ArrList.Item(1), System.Int64)

    '        objparam = Cmd.Parameters.Add("@dtTaskDate", SqlDbType.DateTime)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = CType(ArrList.Item(2), System.DateTime)

    '        objparam = Cmd.Parameters.Add("@sSubject", SqlDbType.VarChar)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = CType(ArrList.Item(3), System.String)

    '        objparam = Cmd.Parameters.Add("@dtDueDate", SqlDbType.DateTime)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = CType(ArrList.Item(4), System.DateTime)

    '        objparam = Cmd.Parameters.Add("@sPriority", SqlDbType.VarChar)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = CType(ArrList.Item(5), System.String)

    '        objparam = Cmd.Parameters.Add("@sStatus", SqlDbType.VarChar)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = CType(ArrList.Item(6), System.String)


    '        objparam = Cmd.Parameters.Add("@sNotes", SqlDbType.VarChar, 255)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = CType(ArrList.Item(7), System.String)

    '        objparam = Cmd.Parameters.Add("@nPatientId", SqlDbType.BigInt)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = CType(ArrList.Item(8), Long)

    '        If Type = TaskType.DMS Then
    '            objparam = Cmd.Parameters.Add("@FAXTIFFFileName", SqlDbType.VarChar)
    '            objparam.Direction = ParameterDirection.Input
    '            objparam.Value = CType(ArrList.Item(9), String)
    '        End If


    '        ''''20070102
    '        objparam = Cmd.Parameters.Add("@TaskType", SqlDbType.Int)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = Type '' To Add TaskType 
    '        ''''

    '        objparam = Cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = GetPrefixTransactionID()

    '        objparam = Cmd.Parameters.Add("@nTaskID", SqlDbType.BigInt)
    '        objparam.Direction = ParameterDirection.InputOutput
    '        objparam.Value = CType(ArrList.Item(0), Long)

    '        'Insert data in ICD9Drugs

    '        Cmd.ExecuteNonQuery()
    '        Dim TaskID As Long
    '        TaskID = objparam.Value

    '        Cmd.Parameters.Clear()

    '        Dim cmddetails As SqlCommand
    '        cmddetails = New SqlCommand("gsp_DeleteTasks_DTL", Conn)
    '        cmddetails.CommandType = CommandType.StoredProcedure
    '        cmddetails.Transaction = trTasks

    '        objparam = cmddetails.Parameters.Add("@nTaskID", SqlDbType.BigInt)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = TaskID  '' CType(ArrList.Item(0), System.Int64)

    '        cmddetails.ExecuteNonQuery()

    '        Dim i As Integer
    '        For i = 0 To Arr1.Length - 1
    '            Dim cmddetails1 As SqlCommand
    '            cmddetails1 = New SqlCommand("gsp_InsertTasks_DTL", Conn)
    '            cmddetails1.CommandType = CommandType.StoredProcedure

    '            cmddetails1.Transaction = trTasks
    '            objparam = cmddetails1.Parameters.Add("@nTaskID", SqlDbType.BigInt)
    '            objparam.Direction = ParameterDirection.Input
    '            objparam.Value = TaskID '' CType(ArrList.Item(0), System.Int64)


    '            objparam = cmddetails1.Parameters.Add("@nToID", SqlDbType.BigInt)
    '            objparam.Direction = ParameterDirection.Input
    '            objparam.Value = Arr1(i)

    '            cmddetails1.ExecuteNonQuery()
    '            cmddetails1.Parameters.Clear()
    '        Next


    '        'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Tasks Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
    '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "Task Modified", gnPatientID, 0, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success)
    '        trTasks.Commit()
    '        Conn.Close()
    '        Return True


    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        trTasks.Rollback()
    '        trTasks = Nothing
    '        Cmd = Nothing
    '        'cmddelete = Nothing
    '        If Conn.State = ConnectionState.Open Then
    '            Conn.Close()
    '        End If
    '        Return False
    '    End Try
    'End Function


    Public Sub DeleteData(ByVal TaskID As Long)
        Dim objParam As SqlParameter
        Dim Cmd As SqlCommand = Nothing
        Try
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_DeleteTasks_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure

            objParam = Cmd.Parameters.Add("@nTaskId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = TaskID

            Conn.Open()
            Cmd.ExecuteNonQuery()

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Delete, "Task Deleted", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Conn.Close()
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
        End Try

    End Sub
    'Public ReadOnly Property DsDataSet() As DataSet
    '    Get
    '        'Dv = Ds.Tables("Category_Mst").DefaultView
    '        Return Ds
    '        'Return Ds
    '    End Get
    'End Property
    Public ReadOnly Property DsDataview() As DataView
        Get
            'Dv = Ds.Tables("Category_Mst").DefaultView
            Return Dv
            'Return Ds
        End Get

    End Property
    Public Sub SortDataview(ByVal strsort As String, Optional ByVal strSortOrder As String = "")
        'Dv.Sort = strsort
        Dv.Sort = "[" & strsort & "]" & strSortOrder
    End Sub
    Public Function FillControls(ByVal btnstatus As Int16) As DataTable
        Try
            Dim adpt As New SqlDataAdapter
            Dim dt As New DataTable
            Dim objParam As SqlParameter
            Dim Cmd As SqlCommand = Nothing
            If btnstatus = 1 Then
                Cmd = New SqlCommand("gsp_FillUsers", Conn)
                Cmd.CommandType = CommandType.StoredProcedure



                objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
                objParam.Direction = ParameterDirection.Input
                objParam.Value = 1
                'User_MST.nUserId, User_MST.sLoginName ,ISNULL( User_MST.sFirstName,'')+' '+ISNULL(User_MST.sLastName,'') , Provider_MST.nProviderID , ISNULL(Provider_MST.sFirstName,'') +' '+ ISNULL(Provider_MST.sLastName,'')  
            ElseIf btnstatus = 2 Then
                'nPatientID, sPatientCode ,ISNULL(sFirstName,'') , ISNULL( sLastName,'')
                Cmd = New SqlCommand("gsp_ViewPatient", Conn)
                Cmd.CommandType = CommandType.StoredProcedure
            End If
            adpt.SelectCommand = Cmd
            adpt.Fill(dt)
            If (IsNothing(Dv) = False) Then
                Dv.Dispose()
                Dv = Nothing
            End If
            Dv = New DataView(dt.Copy())
            Conn.Close()
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
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
        Catch ex As Exception
            MsgBox(ex.Message)
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            Return Nothing
        End Try
    End Function
    Public Function FetchTasksForUpdate(ByVal TaskId As Int64) As ArrayList
        Dim Cmd As SqlCommand = Nothing
        Try
            Dim arrlist As New ArrayList
            Cmd = New SqlCommand("gsp_scanTasks_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure

            Dim objParam As SqlParameter
            objParam = Cmd.Parameters.Add("@nTaskID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = TaskId

            Dim dreader As SqlDataReader
            Conn.Open()
            dreader = Cmd.ExecuteReader()

            Dim i As Integer
            i = 0
            Do While dreader.Read
                For i = 0 To 11
                    arrlist.Add(dreader.Item(i))
                Next
            Loop
            dreader.Close()
            Conn.Close()
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
            Return arrlist
        Catch ex As Exception
            MsgBox(ex.Message)
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            Return Nothing
        End Try
    End Function
    Public Function FetchTasksDetailsForUpdate(ByVal TaskId As Long) As ArrayList
        Dim Cmd As SqlCommand = Nothing
        Try
            Dim arrlist As New ArrayList
            Cmd = New SqlCommand("gsp_scanTasks_DTL", Conn)
            Cmd.CommandType = CommandType.StoredProcedure

            Dim objParam As SqlParameter
            objParam = Cmd.Parameters.Add("@nTaskID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = TaskId

            Dim dreader As SqlDataReader
            Conn.Open()
            dreader = Cmd.ExecuteReader()

            Do While dreader.Read
                Dim lst As New myList
                lst.Index = CType(dreader.Item(0), System.Int64)
                lst.Description = CType(dreader.Item(1), System.String)
                lst.VisitDate = CType(dreader.Item(4), System.DateTime)
                arrlist.Add(lst)
                'frmVWOrders._strTasksNotes = CType(dreader.Item(3), System.String)
                frm_LM_Orders._strTasksNotes = CType(dreader.Item(3), System.String)
            Loop
            dreader.Close()
            Conn.Close()
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
            Return arrlist
        Catch ex As Exception
            MsgBox(ex.Message)
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            Return Nothing
        End Try
    End Function
    Public Sub SetRowFilter(ByVal ColIndex As Integer, ByVal txtSearch As String)
        Dim strexpr As String
        strexpr = "" & Dv.Table.Columns(ColIndex).ColumnName() & "  Like '" & txtSearch & "%'"
        Dv.RowFilter = strexpr
    End Sub
    Public Sub SetRowFilter(ByVal txtSearch As String)
        Dim strexpr As String
        Dim str As String
        str = Dv.Sort
        str = Splittext(str)
        str = Mid(str, 2)
        str = Mid(str, 1, Len(str) - 1)
        strexpr = "" & Dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
        Dv.RowFilter = strexpr
    End Sub
    Private Function Splittext(ByVal strsplittext As String) As String

        Dim arrstring() As String
        Try
            If Trim(strsplittext) <> "" Then

                arrstring = Split(strsplittext, " ")
                Return arrstring(0)
            Else
                Return strsplittext
            End If
        Catch ex As Exception

            Return strsplittext
        End Try
    End Function

    'function commented by dipak as not in use
    'Public Function UpdateTaskStatus(ByVal TaskID As Long, ByVal Status As String)
    '    Try
    '        Dim objparam As SqlParameter
    '        Cmd = New System.Data.SqlClient.SqlCommand("gsp_UpdateTasksStatus", Conn)
    '        Cmd.CommandType = CommandType.StoredProcedure

    '        objparam = Cmd.Parameters.Add("@TaskID", SqlDbType.BigInt)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = TaskID

    '        objparam = Cmd.Parameters.Add("@TaskStatus", SqlDbType.VarChar)
    '        objparam.Direction = ParameterDirection.Input
    '        objparam.Value = Status

    '        Conn.Open()
    '        Cmd.ExecuteNonQuery()
    '        Conn.Close()


    '        'objAudit.CreateLog(clsAudit.enmActivityType.Modify, "Task Modified", gstrLoginName, gstrClientMachineName)
    '        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Delete, "Task Deleted", gnPatientID, 0, gnLoginProviderID, gloAuditTrail.ActivityOutCome.Success)

    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        Cmd = Nothing
    '        Conn.Close()
    '    End Try
    'End Function
    Public Function Fill_LockTask(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable
        Try
            Dim dt As New DataTable
            Dim sqladpt As New SqlDataAdapter
            Dim Cmd As SqlCommand = Nothing
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_Select_UnLock_Record", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@sMachinName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = MachinName

            objParam = Cmd.Parameters.Add("@nTrnType", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = TransactionType

            objParam = Cmd.Parameters.Add("@nMachinID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 0

            sqladpt.SelectCommand = Cmd

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
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Public Function FetchDocumentName(ByVal DocumentID As Long) As String
        Dim Cmd As SqlCommand = Nothing
        Dim strQry As String = "select DocumentFileName from DMS_MST Where DocumentID = '" & DocumentID & "'"
        Conn.Open()
        Cmd = New SqlCommand(strQry, Conn)
        Dim DocumentName As String = Cmd.ExecuteScalar()
        Conn.Close()
        Cmd.Parameters.Clear()
        Cmd.Dispose()
        Cmd = Nothing
        Return DocumentName
    End Function
    Private disposed As Boolean = False

    Public Sub Dispose() Implements System.IDisposable.Dispose
        'Disconnect();
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                If Conn IsNot Nothing Then
                    Conn.Dispose()
                    Conn = Nothing

                End If
                If IsNothing(Dv) = False Then
                    Dv.Dispose()
                    Dv = Nothing
                End If
                'If IsNothing(Ds) = False Then
                '    Ds.Dispose()
                '    Ds = Nothing
                'End If
            End If
        End If
        disposed = True
    End Sub


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
