Imports System.Data.SqlClient


Public Class clsPatientHistory
    ' Private da As SqlDataAdapter
    'Private ds As New DataSet
    'Private dt As DataTable
    'Private dsHistory As DataSet = Nothing
    Private dv As DataView = Nothing
    Private Con As SqlConnection = Nothing
    '  Private conString As String

    'sarika flag for 2nd insert
    Private _TempHistoryID As Long = 0
    '----
    Public Sub Dispose()

        ''slr free dv
        If Not IsNothing(dv) Then
            dv.Dispose()
            dv = Nothing
        End If
        'slr free Con
        If Not IsNothing(Con) Then
            Con.Dispose()
            Con = Nothing
        End If

    End Sub
    Public Sub New()
        Try
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
        Catch ex As Exception   ' Catch the error.
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        End Try
    End Sub

    'Public ReadOnly Property GetDataSet() As DataSet
    '    Get
    '        'Dv = Ds.Tables("Category_Mst").DefaultView
    '        Return ds
    '        'Return Ds
    '    End Get
    'End Property

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

        'grdCPT.DataSource = dv
        Return Nothing
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


    Public Function GetPatientHistory(ByVal ID As Long) As DataView
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            'objBusLayer.Open_Con()
            cmd = New SqlCommand("gsp_ViewPatientHistory", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.AddWithValue("@ID", ID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = ID

            Con.Open()
            'cmd.ExecuteNonQuery()
            ' ds.Clear()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Con.Close()
            da.Dispose()
            da = Nothing
            If (IsNothing(dv) = False) Then
                dv.Dispose()
                dv = Nothing
            End If
            If (IsNothing(dt) = False) Then
                dv = New DataView(dt.Copy())
                dt.Dispose()
                dt = Nothing
            End If

            Return dv
            'objBusLayer.Close_Con()
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
           
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
        End Try
    End Function



    Public Function CheckDuplicate_New(ByVal VisitID As Long, ByVal HistoryCategory As String, ByVal HistoryItem As String) As Boolean
        Dim cmd1 As SqlCommand = Nothing
        Dim sqlParam1 As SqlParameter

        Try


            'objBusLayer.Open_Con()
            cmd1 = New SqlCommand("gsp_CheckPatientHistory", Con)
            cmd1.CommandType = CommandType.StoredProcedure


            sqlParam1 = cmd1.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam1.Direction = ParameterDirection.Input
            'sqlParam1.Value = VisitID

            sqlParam1 = cmd1.Parameters.Add("@HistoryCategory", SqlDbType.VarChar)
            sqlParam1.Direction = ParameterDirection.Input
            sqlParam1.Value = HistoryCategory

            sqlParam1 = cmd1.Parameters.Add("@HistoryItem", SqlDbType.VarChar)
            sqlParam1.Direction = ParameterDirection.Input
            sqlParam1.Value = HistoryItem

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            ''''''''DONT CLOSE CONNECTION HERE
            '''''' IT IS HANDLED IN AddNewHistory Function

            Dim rowAffected As Long
            ' Dim dataread As SqlDataReader
            rowAffected = CType(cmd1.ExecuteScalar, Long)

            If rowAffected > 0 Then
                Return True     ' Duplicate Exists
            Else
                Return False    ' Duplicate Not Exists
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If cmd1 IsNot Nothing Then
                cmd1.Parameters.Clear()
                cmd1.Dispose()
                cmd1 = Nothing
            End If
            sqlParam1 = Nothing
        End Try
    End Function

    ' For Edit 
    Public Function SelectPatientHistory(ByVal VisitID As Long, ByVal PatientID As Long) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            'objBusLayer.Open_Con()
            cmd = New SqlCommand("gsp_ScanHistory", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = VisitID

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Con.Close()
            da.Dispose()
            da = Nothing

            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
        End Try
    End Function


    Public Function SelectCategory(ByVal ID As Long)
        'Dim objBusLayer As New clsBuslayer
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            'objBusLayer.Open_Con()
            cmd = New SqlCommand("gsp_ScanCategory_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@CategoryID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            Con.Open()
            cmd.ExecuteNonQuery()
            Con.Close()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
        End Try
        Return Nothing
    End Function


    Public Function GetSmokingStatus(ByVal ID As Int64) As String

        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim str As String = ""
        Try
            cmd = New SqlCommand("GetSmokingStatus", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = New SqlParameter("@CategoryID", SqlDbType.BigInt)
            sqlParam.Value = ID
            cmd.Parameters.Add(sqlParam)


            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Con.Close()
            da.Dispose()
            da = Nothing

            If dt.Rows.Count > 0 Then
                str = Convert.ToString(dt.Rows(0)("SmokingStatus"))
            End If
            dt.Dispose()
            dt = Nothing
            Return str

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If

        End Try
    End Function

    Public Function getFamilyMember() As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim str As String = ""
        Try
            cmd = New SqlCommand("gsp_viewFamilyMember_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = New SqlParameter("@nMemberID", SqlDbType.BigInt)
            sqlParam.Value = 0
            cmd.Parameters.Add(sqlParam)


            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Con.Close()
            da.Dispose()
            da = Nothing


            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If Con.State = ConnectionState.Open Then
                Con.Close()
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If

        End Try
    End Function

    Public Function GetAllCategory(ByVal CategoryType As String) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gsp_FillCategory_Mst", Con)
            cmd.CommandType = CommandType.StoredProcedure

            Con.Open()
            sqlParam = cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = "History"
            sqlParam.Value = CategoryType

            sqlParam = cmd.Parameters.Add("@flag", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 1


            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Con.Close()
            da.Dispose()
            da = Nothing

            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If

        End Try
    End Function
    'Public Function GetICD9ORCPTCode(ByVal FlagTYpe As Integer) As DataTable
    '    Dim cmd As SqlCommand
    '    Dim sqlParam As SqlParameter
    '    Try
    '        cmd = New SqlCommand("gsp_FillICD9ORCPT_History", Con)
    '        cmd.CommandType = CommandType.StoredProcedure

    '        Con.Open()
    '        sqlParam = cmd.Parameters.Add("@ClinicID", SqlDbType.BigInt)
    '        sqlParam.Direction = ParameterDirection.Input

    '        sqlParam.Value = gnClinicID

    '        sqlParam = cmd.Parameters.Add("@Flag", SqlDbType.Int)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = FlagTYpe


    '        da = New SqlDataAdapter
    '        da.SelectCommand = cmd
    '        dt = New DataTable
    '        da.Fill(dt)
    '        Con.Close()
    '        Return dt

    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally

    '        If IsNothing(cmd) = False Then
    '            cmd.Dispose()
    '            cmd = Nothing
    '        End If
    '        If IsNothing(sqlParam) = False Then
    '            sqlParam = Nothing
    '        End If

    '    End Try
    'End Function

    Public Function GetHistory(ByVal PatientID As Long, ByVal intflag As Int16, ByVal VisitDate As Date, Optional ByVal intvisitId As Long = 0) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing

        Try
            'intflag = 0 --check if current history exists  
            'intflag = 1 --check if history record before current date in history table        
            'intflag = 2 --if history record before current date in history table fetch details of that transaction        

            cmd = New SqlCommand("gsp_GetHistory", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@dtVisitdate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = VisitDate

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = cmd.Parameters.Add("@flag", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = intflag

            If intflag = 2 Then
                sqlParam = cmd.Parameters.AddWithValue("@VisitId", intvisitId)
                sqlParam.Direction = ParameterDirection.Input
                'sqlParam.Value = intvisitId
            End If
            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Con.Close()
            da.Dispose()
            da = Nothing
            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If

        End Try
    End Function


    Public Function GetPatName(ByVal PatientID As Long) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("GetPatNameWithoutExtraSpace", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@nPatientID", SqlDbType.BigInt)
            sqlParam.Value = PatientID
            sqlParam.Direction = ParameterDirection.Input

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Con.Close()
            da.Dispose()
            da = Nothing
            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If

        End Try
    End Function

    Public Function UpdateHistoryAnswers() As DataTable

        Dim da As SqlDataAdapter
        Dim dtHistoryAnswers As New DataTable '= Nothing

        Try

            da = New SqlDataAdapter("gsp_GetHistoryAnswers", Con)

            da.Fill(dtHistoryAnswers)
            dtHistoryAnswers.TableName = "InitialExam"

            If IsNothing(da) = False Then
                da.Dispose()
                da = Nothing
            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure)
        End Try

        Return dtHistoryAnswers

    End Function


    Public Function GetHistory_optimize(ByVal PatientID As Long, ByVal intflag As Int16, ByVal VisitDate As Date, ByVal CategoryType As String, ByVal Flag As Int16, Optional ByVal intvisitId As Long = 0, Optional ByVal _isOBHistory As Boolean = False) As DataSet
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim _IsActive As Boolean = False
        Try
            'intflag = 0 --check if current history exists  
            'intflag = 1 --check if history record before current date in history table        
            'intflag = 2 --if history record before current date in history table fetch details of that transaction        

            cmd = New SqlCommand("gsp_GetHistoryData", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@dtVisitdate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = VisitDate

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = cmd.Parameters.Add("@flag1", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = intflag

            sqlParam = cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = CategoryType

            sqlParam = cmd.Parameters.Add("@flag", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 1




            If intflag = 2 Or Flag = 1 Then
                sqlParam = cmd.Parameters.AddWithValue("@VisitId", intvisitId)
                sqlParam.Direction = ParameterDirection.Input
                'sqlParam.Value = intvisitId
            End If
            sqlParam = cmd.Parameters.Add("@IsActiveCase", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            _IsActive = CreateOBCase(PatientID)
            If _IsActive = True Then
                sqlParam.Value = 1
            Else
                sqlParam.Value = 0
            End If
            sqlParam = cmd.Parameters.Add("@IsOBHistory", SqlDbType.Bit)
            sqlParam.Direction = ParameterDirection.Input
            If _isOBHistory = True Then
                sqlParam.Value = 1
            Else
                sqlParam.Value = 0
            End If


            'sqlParam = cmd.Parameters.Add("@DefaultSortOrder", SqlDbType.VarChar)
            'sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = gblnDefaultSortOrder
            ''Fixed issue #75937-gloEMR: History- Application gives exception
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd

            Dim dsHistory As DataSet = New DataSet
            da.Fill(dsHistory, "Category")
            Con.Close()
            dsHistory.Tables(1).TableName = "Review"
            dsHistory.Tables(2).TableName = "Patient"
            dsHistory.Tables(3).TableName = "History"
            dsHistory.Tables(4).TableName = "ReviewHistory"
            dsHistory.Tables(5).TableName = "Narration"
            dsHistory.Tables(6).TableName = "HistoryTypes"
            dsHistory.Tables(7).TableName = "CategoryType"
            dsHistory.Tables(8).TableName = "GetCategory"
            dsHistory.Tables(9).TableName = "InitialExam"
            dsHistory.Tables(10).TableName = "UDI"

            da.Dispose()
            da = Nothing

            Return dsHistory

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
        End Try
    End Function
    Private Function CreateOBCase(ByVal _PatientID As Int64) As Long
        Dim IsCreate As Long = 0
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oParam As gloDatabaseLayer.DBParameters = Nothing
        Dim dtdata As DataTable = Nothing
        Try
            oDB.Connect(False)
            oParam = New gloDatabaseLayer.DBParameters
            oParam.Add("@PatientId", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("gsp_GetActiveOBCases", oParam, dtdata)
            oDB.Disconnect()
            If (IsNothing(dtdata) = False) Then
                If dtdata.Rows.Count <= 0 Then
                    IsCreate = 0
                Else
                    IsCreate = Convert.ToUInt64(dtdata.Rows(0)("nCaseId"))
                End If
            End If
            Return IsCreate
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return IsCreate
        Finally
            If Not IsNothing(oDB) Then
                oDB.Dispose()
                oDB = Nothing
            End If
            If Not IsNothing(oParam) Then
                oParam.Dispose()
                oParam = Nothing
            End If
            If Not IsNothing(dtdata) Then
                dtdata.Dispose()
                dtdata = Nothing
            End If
        End Try
    End Function

    Public Function GetAllAllergies() As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gsp_FillAllergies", Con)

            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Con.Close()
            da.Dispose()
            da = Nothing

            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
        End Try
    End Function

    ''End 20110623
    Public Function GetAllHistory(ByVal strGroup As String) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim da As SqlDataAdapter = New SqlDataAdapter
        Dim dt As DataTable = New DataTable

        Try
            cmd = New SqlCommand("gsp_FillHistory", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@strGroup", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = strGroup

            da.SelectCommand = cmd
            da.Fill(dt)
            da.Dispose()
            da = Nothing

            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
        End Try
    End Function

    Public Function GetAllICD9Gallery() As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            'Dim cmd As New SqlCommand("select distinct isnull(nICD9ID,0) as ICD9ID, isnull(sICD9Code,'') +" & "' - '" & "+ isnull(sDescription,'') , isnull(sICD9Code,'') as ICD9Code, isnull(sDescription,'') as Description from ICD9 ORDER BY ICD9Code", Con)
            cmd = New SqlCommand("select distinct isnull(nICD9ID,0) as ICD9ID,RTrim(Ltrim(isnull(sICD9Code,''))) +" & "' - '" & "+ RTrim(Ltrim(isnull(sDescriptionMedium,''))) ,RTrim(Ltrim(isnull(sICD9Code,''))) as ID, RTrim(Ltrim(isnull(sDescriptionMedium,''))) as Description from ICD9Gallery ORDER BY ID", Con)
            cmd.CommandType = CommandType.Text


            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Con.Close()
            da.Dispose()
            da = Nothing
            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
        End Try
    End Function



    Public Function GetAllergyServerity() As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gsp_GetAllergySeverity", Con)
            cmd.CommandType = CommandType.StoredProcedure


            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Con.Close()
            da.Dispose()
            da = Nothing
            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
        End Try
    End Function

    Public Function GetAllVisits(ByVal PatientID As Long) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gsp_FillVisit", Con)
            cmd.CommandType = CommandType.StoredProcedure
            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = PatientID

            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Con.Close()
            da.Dispose()
            da = Nothing
            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
        End Try
    End Function

    'Fill Previous Histories in  trvPrevHistory
    Public Function GetPrevHistory(ByVal Interval As String, ByVal PatientID As Long, ByVal Sysdate As Date) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gsp_ViewHistory", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@Interval", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Interval

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = cmd.Parameters.Add("@dtSysdate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Sysdate

            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Con.Close()
            da.Dispose()
            da = Nothing
            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
        End Try
    End Function



    '
    Public Function AddNewHistory(ByVal PreviousVisitID As Int64, ByVal CurrentVisitDate As DateTime, ByVal PatientID As Long) As Long
        Dim trHistory As SqlTransaction = Nothing
        Dim cmdVisits As SqlCommand = Nothing
        Try
            'Dim visitid As Long
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If

            trHistory = Con.BeginTransaction


            cmdVisits = New SqlCommand("gsp_InUpHistory1", Con)
            cmdVisits.CommandType = CommandType.StoredProcedure

            cmdVisits.Transaction = trHistory

            Dim sqlParam As SqlParameter

            sqlParam = cmdVisits.Parameters.Add("@PreviousVisitID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PreviousVisitID

            sqlParam = cmdVisits.Parameters.Add("@CurrentVisitDate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = CurrentVisitDate

            sqlParam = cmdVisits.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatientID



            sqlParam = cmdVisits.Parameters.Add("@MachineID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = GetPrefixTransactionID()

            sqlParam = cmdVisits.Parameters.Add("@CurrentVisitID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Output
            sqlParam.Value = 0

            'UpdateLog(" Start - sp_InUpHistory1 ")
            cmdVisits.ExecuteNonQuery()
            'UpdateLog(" End - sp_InUpHistory1 ")
            trHistory.Commit()

            Dim nVisitID As Long
            nVisitID = sqlParam.Value

            sqlParam = Nothing
            Return nVisitID
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            If (IsNothing(trHistory) = False) Then


                trHistory.Rollback()
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If cmdVisits IsNot Nothing Then
                cmdVisits.Parameters.Clear()
                cmdVisits.Dispose()
                cmdVisits = Nothing
            End If
            If (IsNothing(trHistory) = False) Then
                trHistory.Dispose()
                trHistory = Nothing
            End If

        End Try
    End Function




    Public Function AddNewHistory_New(ByVal HistoryID As Long, ByVal VisitID As Long, ByVal PatientID As Long, ByVal ArrLst As ArrayList, ByVal IsModify As Boolean, Optional ByVal Col_RemovedAllergies As Collection = Nothing) As Boolean
        '''' 20070123
        '''' Col_RemovedAllergies Collection Passed to Maintain Audit Log of Removed Allergies
        If Con.State = ConnectionState.Closed Then
            Con.Open()
        End If

        ''''' To Delete History 
        '''' If History is in Modify Mode then Use Delete-Insert Methode
        If IsModify = True Then
            Dim cmdDel As New SqlCommand("gsp_DeleteHistory", Con)
            cmdDel.CommandType = CommandType.StoredProcedure

            Dim sqlParam1 As SqlParameter
            sqlParam1 = cmdDel.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam1.Direction = ParameterDirection.Input
            'sqlParam1.Value = VisitID

            sqlParam1 = cmdDel.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam1.Direction = ParameterDirection.Input
            'sqlParam1.Value = PatientID

            cmdDel.ExecuteNonQuery()
            cmdDel.Parameters.Clear()
            cmdDel.Dispose()
            sqlParam1 = Nothing
            cmdDel = Nothing
        End If
        _TempHistoryID = 0

        Dim i As Integer



        For i = 0 To ArrLst.Count - 1
            Dim lst As myList
            lst = CType(ArrLst(i), myList)

            '' Reset
            HistoryID = 0

            '''' If Modify then All Recordes are Deleted 
            '''' there No Need to Check Duplicate Data 
            If IsModify = False Then
                If CheckDuplicate_New(VisitID, lst.HistoryCategory, lst.HistoryItem) = True Then
                    '' Record found  
                    If CType(lst.HistoryCategory, String) <> "Allergies" Then
                        '' if Not Allergies then Modify
                        HistoryID = 1  ''(Update)
                    Else
                        ''  if Allergies then Skip Add-Update
                        'GoTo LINE1
                        HistoryID = 2 'Skip the Record
                    End If
                Else
                    ''If not found any record then Add (Not Update)
                    HistoryID = 0
                End If
            End If



            If HistoryID <= 1 Then
                Dim cmd As New SqlCommand("gsp_AddHistory", Con)
                cmd.CommandType = CommandType.StoredProcedure
                Dim sqlParam As SqlParameter
                sqlParam = cmd.Parameters.AddWithValue("@HistoryID", HistoryID)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam = cmd.Parameters.AddWithValue("@VisitID", VisitID)
                sqlParam.Direction = ParameterDirection.Input
                'sqlParam.Value = VisitID

                sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
                sqlParam.Direction = ParameterDirection.Input

                sqlParam = cmd.Parameters.Add("@HistoryCategory", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = lst.HistoryCategory '' Rows(i)(0) ''HistoryTable.HistoryCategory

                sqlParam = cmd.Parameters.Add("@HistoryItem", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = lst.HistoryItem  '' Rows(i)(1) ''HistoryItem

                sqlParam = cmd.Parameters.Add("@Comments", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = lst.Description  '' Rows(i)(2) ''Comments

                'Pramod 2007-05-02 Add new field in talbe Reaction 
                sqlParam = cmd.Parameters.Add("@Reaction", SqlDbType.VarChar)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = lst.Reaction '' Rows(i)(3) ''Reaction + Status

                sqlParam = cmd.Parameters.Add("@DrugID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = lst.ID '' ''Node.Key '' DrugID

                sqlParam = cmd.Parameters.Add("@TempHistoryID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.InputOutput
                sqlParam.Value = _TempHistoryID '' ''Node.Key '' c

                sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = GetPrefixTransactionID()

                sqlParam = cmd.Parameters.Add("@nmedicalconditionid", SqlDbType.BigInt)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = lst.MedicalConditionID '' ''Node.Key '' DrugID

                'For Deormalization of History table
                'DrugName
                If Not IsNothing(lst.HxDrugName) Then
                    sqlParam = cmd.Parameters.Add("@sDrugName", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = lst.HxDrugName
                Else
                    sqlParam = cmd.Parameters.Add("@sDrugName", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""
                End If

                'DrugDosage
                If Not IsNothing(lst.HxDrugDosage) Then
                    sqlParam = cmd.Parameters.Add("@sDosage", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = lst.HxDrugDosage
                Else
                    sqlParam = cmd.Parameters.Add("@sDosage", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""
                End If


                'NDCCode
                If Not IsNothing(lst.HxNDCCode) Then
                    sqlParam = cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = lst.HxNDCCode
                Else
                    sqlParam = cmd.Parameters.Add("@sNDCCode", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""
                End If


                sqlParam = cmd.Parameters.Add("@mpid", SqlDbType.Int)
                sqlParam.Direction = ParameterDirection.Input
                sqlParam.Value = lst.mpid
                'For Deormalization of History table

                ''Added Rahul on 20101005
                If Not IsNothing(lst.DOEOAllergy) Then
                    If lst.DOEOAllergy.Trim() <> "" Then
                        sqlParam = cmd.Parameters.Add("@DOEOAllergy", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = lst.DOEOAllergy
                    End If
                End If

                If Not IsNothing(lst.ConceptId) Then
                    sqlParam = cmd.Parameters.Add("@ConceptID", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = lst.ConceptId
                End If

                If Not IsNothing(lst.DescId) Then
                    sqlParam = cmd.Parameters.Add("@DescID", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = lst.DescId
                End If

                If Not IsNothing(lst.SnowMadeID) Then
                    sqlParam = cmd.Parameters.Add("@SnoMedID", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = lst.SnowMadeID
                End If

                If Not IsNothing(lst.SnoDescription) Then
                    sqlParam = cmd.Parameters.Add("@sDescription", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = lst.SnoDescription
                End If

                If Not IsNothing(lst.ICD9) Then
                    sqlParam = cmd.Parameters.Add("@sICD9", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = lst.ICD9
                Else
                    sqlParam = cmd.Parameters.Add("@sICD9", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""
                End If
                If Not IsNothing(lst.RxNormID) Then
                    sqlParam = cmd.Parameters.Add("@sRxNormID", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = lst.RxNormID
                Else
                    sqlParam = cmd.Parameters.Add("@sRxNormID", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = ""
                End If
                ''
                If Not IsNothing(lst.ReasonConceptID) Then
                    sqlParam = cmd.Parameters.Add("@ReasonConceptID", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = lst.ReasonConceptID
                End If
                If Not IsNothing(lst.ReasonConceptDesc) Then
                    sqlParam = cmd.Parameters.Add("@ReasonDesc", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = lst.ReasonConceptDesc
                End If
                If Not IsNothing(lst.nDeviceListID) Then
                    sqlParam = cmd.Parameters.Add("@nDeviceList_ID", SqlDbType.BigInt)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = lst.nDeviceListID
                End If
                If Not IsNothing(lst.sProcStatus) Then
                    sqlParam = cmd.Parameters.Add("@sProcStatus", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = lst.sProcStatus
                End If
                Try

                    '' chetan added for saving username for patient tracking oct 18 2010
                    sqlParam = cmd.Parameters.Add("@sTranUser", SqlDbType.VarChar)
                    sqlParam.Direction = ParameterDirection.Input
                    sqlParam.Value = gstrLoginName

                    If Not IsNothing(lst.CPT) Then
                        sqlParam = cmd.Parameters.Add("@sCPT", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = lst.CPT
                    Else
                        sqlParam = cmd.Parameters.Add("@sCPT", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = ""
                    End If
                    If Not IsNothing(lst.OnsetDate) Then
                        If lst.OnsetDate <> "" Then
                            sqlParam = cmd.Parameters.Add("@OnsetDate", SqlDbType.DateTime)
                            sqlParam.Direction = ParameterDirection.Input
                            sqlParam.Value = lst.OnsetDate
                        Else
                            sqlParam = cmd.Parameters.Add("@OnsetDate", SqlDbType.DateTime)
                            sqlParam.Direction = ParameterDirection.Input
                            sqlParam.Value = System.DBNull.Value
                        End If

                    Else
                        sqlParam = cmd.Parameters.Add("@OnsetDate", SqlDbType.DateTime)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = System.DBNull.Value
                    End If

                    ''loinc code and desc added
                    If Not IsNothing(lst.LoincCode) Then
                        sqlParam = cmd.Parameters.Add("@sLoincCode", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = lst.LoincCode
                    End If
                    If Not IsNothing(lst.LoincDesc) Then
                        sqlParam = cmd.Parameters.Add("@sLoincDescr", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = lst.LoincDesc
                    End If


                    ''cqm


                    If Not IsNothing(lst.CqmCode) Then
                        sqlParam = cmd.Parameters.Add("@sValueSetOID", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = lst.CqmCode
                    End If
                    If Not IsNothing(lst.CqmDesc) Then
                        sqlParam = cmd.Parameters.Add("@sValueSetName", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = lst.CqmDesc
                    End If

                    If Not IsNothing(lst.sAllergySeverity) Then
                        sqlParam = cmd.Parameters.Add("@sSeverity", SqlDbType.VarChar) ''Bug #110255:while pulling history to back date, Issue Resolved
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = lst.sAllergySeverity
                    End If

                    If Not IsNothing(lst.sAllergyIntelorenceCode) Then
                        sqlParam = cmd.Parameters.Add("@sAllergyIntelorenceCode", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = lst.sAllergyIntelorenceCode
                    End If

                    If Not IsNothing(lst.nRowOrder) Then
                        sqlParam = cmd.Parameters.Add("@nRowOrder", SqlDbType.BigInt)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = lst.nRowOrder
                    Else
                        sqlParam = cmd.Parameters.Add("@nRowOrder", SqlDbType.BigInt)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = ""
                    End If
                    If Not IsNothing(lst.sHistoryType) Then
                        sqlParam = cmd.Parameters.Add("@sHistoryType", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = lst.sHistoryType
                    Else
                        sqlParam = cmd.Parameters.Add("@sHistoryType", SqlDbType.VarChar)
                        sqlParam.Direction = ParameterDirection.Input
                        sqlParam.Value = ""
                    End If

                    cmd.ExecuteNonQuery()
                    '  UpdateLog("End--sp_InUpHistory")

                    _TempHistoryID = cmd.Parameters("@TempHistoryID").Value
                    sqlParam = Nothing
                Catch ex As Exception
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
                    Throw ex
                End Try
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        Next

        'UpdateLog("End of sp_InUpHistory")




        If IsModify = True Then
            'gloAuditTrail.gloAuditTrail.CreateLog(clsAudit.enmActivityType.Modify, "Patient History Modified", gstrLoginName, gstrClientMachineName, gnPatientID)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.Modify, "Patient History Modified", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.Modify, "Patient History Modified", gloAuditTrail.ActivityOutCome.Success)
        Else
            'gloAuditTrail.gloAuditTrail.CreateLog(clsAudit.enmActivityType.Add, "Patient History Added", gstrLoginName, gstrClientMachineName, gnPatientID)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.Add, "Patient History Added", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.Add, "Patient History Added", gloAuditTrail.ActivityOutCome.Success)
        End If

        '''' By Mahesh 20070123
        '' To MAintain Audit Log of Deleted Allergies
        If IsNothing(Col_RemovedAllergies) = False Then
            For i = 1 To Col_RemovedAllergies.Count
                'objAudit.CreateLog(clsAudit.enmActivityType.Delete, "'" & Col_RemovedAllergies(i) & "' - Allergy Removed", gstrLoginName, gstrClientMachineName, gnPatientID)
                'gloAuditTrail.gloAuditTrail.CreateLog(clsAudit.enmActivityType.Delete, "Allergy Removed", gstrLoginName, gstrClientMachineName, gnPatientID)
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Allergy Removed", gloAuditTrail.ActivityOutCome.Success)
            Next
        End If


        Con.Close()
        Return True
    End Function

    Public Function AddNewHistoryDataset(ByVal bIsPastVisit As Boolean, ByVal bSubsequentResult As Boolean, ByVal PatientID As Int64, ByVal IsModify As Boolean, ByVal dshistory As DataSet, ByVal ParameterName As String, Optional ByVal dtpHistory As DataTable = Nothing, Optional ByVal Rx_HistoryMedicationReconcillation As String = "") As Boolean
        Dim Con As SqlConnection = Nothing
        Dim sqlDataAdapter As System.Data.SqlClient.SqlDataAdapter = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim _param As SqlParameter = Nothing

        Dim returnedDatatable As New DataTable
        Try

            Con = New SqlConnection(GetConnectionString())
            cmd = New SqlCommand("gsp_InUpHistory", Con)
            cmd.CommandType = CommandType.StoredProcedure ''column name change for refusal changes 
            If (dshistory.Tables("History").Columns.Contains("sRefusalCode")) Then
                dshistory.Tables("History").Columns("sRefusalCode").ColumnName = "sReasonConceptID"
            End If
            If (dshistory.Tables("History").Columns.Contains("sRefusalDesc")) Then
                dshistory.Tables("History").Columns("sRefusalDesc").ColumnName = "sReasonConceptDesc"
            End If
            _param = cmd.Parameters.AddWithValue(ParameterName, dshistory.Tables("History"))
            _param.SqlDbType = SqlDbType.Structured
            If (Rx_HistoryMedicationReconcillation.Trim() <> "") Then
                _param = cmd.Parameters.AddWithValue(Rx_HistoryMedicationReconcillation, dtpHistory)
                _param.SqlDbType = SqlDbType.Structured
            End If

            _param = cmd.Parameters.AddWithValue("@nPatientID", PatientID)
            _param.SqlDbType = SqlDbType.BigInt

            _param = cmd.Parameters.AddWithValue("@nProviderId", gloGlobal.gloPMGlobal.LoginProviderID)
            _param.SqlDbType = SqlDbType.BigInt

            _param = cmd.Parameters.AddWithValue("@IsPastVisit", bIsPastVisit)
            _param.SqlDbType = SqlDbType.Bit

            _param = cmd.Parameters.AddWithValue("@IsAddUpdateinSubsequentVisits", bSubsequentResult)
            _param.SqlDbType = SqlDbType.Bit


            If Not System.Configuration.ConfigurationManager.AppSettings("BreakTheGlass") Is Nothing Then
                If Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings("BreakTheGlass")) Then
                    _param = cmd.Parameters.AddWithValue("@bAllowEmergencyAccess", 1)
                    _param.SqlDbType = SqlDbType.Bit
                End If
            End If

            sqlDataAdapter = New SqlDataAdapter(cmd)
            sqlDataAdapter.SelectCommand.Connection = Con

            sqlDataAdapter.Fill(returnedDatatable)

            'Con.Open()
            'returnedDatatable = cmd.ExecuteScalar()
            'Con.Close()

            'Changes by Ashish 8th Oct 2013 for Audit Trail Logging
            'Previously the audit entries were not being executed due to
            'an incorrectly placed Return statement.
            'Audit Log for modified rows
            'Dim dataRowModified As IEnumerable(Of DataRow) = From QueriedRow As DataRow In dshistory.Tables("History").Rows
            '                                                            Where QueriedRow("RowState") = "Modified" And QueriedRow("nHistoryID") <> 0
            '                                                                Select QueriedRow

            '05-May-15 Aniket: Resolving Bug #82999: EMR: PT history- Application gives exception
            'Dim dataRowModified As IEnumerable(Of DataRow) = From QueriedRow As DataRow In dshistory.Tables("History").Rows
            '                                                            Where QueriedRow("RowState").Equals("Modified") And QueriedRow("nHistoryID") <> 0
            '                                                                Select QueriedRow

            'If dataRowModified IsNot Nothing Then
            '    If dataRowModified.Count > 0 Then
            '        For Each ElementRow As DataRow In dataRowModified
            '            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.Modify, "Patient History Modified", PatientID, ElementRow("nHistoryID"), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            '        Next
            '        dataRowModified = Nothing
            '        'gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.Modify, "Patient History Modified", gloAuditTrail.ActivityOutCome.Success)
            '    End If
            'End If

            ''Audit log for added entries
            'If returnedDatatable IsNot Nothing Then
            '    If returnedDatatable.Rows.Count > 0 Then

            '        Dim dataRowAdded As IEnumerable(Of DataRow) = From AddedRow As DataRow In returnedDatatable.Rows
            '                                                               Select AddedRow

            '        For Each AddedRow As DataRow In dataRowAdded
            '            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.Add, "Patient History Added", PatientID, AddedRow("nHistoryID"), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            '        Next
            '        dataRowAdded = Nothing
            '        'gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.Add, "Patient History Added", gloAuditTrail.ActivityOutCome.Success)
            '    End If
            'End If

            ''Audit Log for deleted entries
            ''Dim dataRowDeleted As IEnumerable(Of DataRow) = From QueriedRow As DataRow In dshistory.Tables("History").Rows
            ''                                                            Where QueriedRow("RowState") = "Deleted" And QueriedRow("nHistoryID") <> 0
            ''                                                                Select QueriedRow

            ''05-May-15 Aniket: Resolving Bug #82999: EMR: PT history- Application gives exception
            'Dim dataRowDeleted As IEnumerable(Of DataRow) = From QueriedRow As DataRow In dshistory.Tables("History").Rows
            '                                                         Where QueriedRow("RowState").Equals("Deleted") And QueriedRow("nHistoryID") <> 0
            '                                                             Select QueriedRow

            'If dataRowDeleted IsNot Nothing Then
            '    If dataRowDeleted.Count > 0 Then
            '        For Each ElementRow As DataRow In dataRowDeleted
            '            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.Delete, "Patient History Deleted", PatientID, ElementRow("nHistoryID"), 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            '        Next
            '        dataRowDeleted = Nothing
            '        'gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.History, gloAuditTrail.ActivityType.Modify, "Patient History Modified", gloAuditTrail.ActivityOutCome.Success)
            '    End If
            'End If
            'If IsModify = True Then
            '    'gloAuditTrail.gloAuditTrail.CreateLog(clsAudit.enmActivityType.Modify, "Patient History Modified", gstrLoginName, gstrClientMachineName, gnPatientID)                
            'Else
            '    'gloAuditTrail.gloAuditTrail.CreateLog(clsAudit.enmActivityType.Add, "Patient History Added", gstrLoginName, gstrClientMachineName, gnPatientID)                                
            'End If
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(Con) = False Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlDataAdapter) = False Then
                sqlDataAdapter.Dispose()
                sqlDataAdapter = Nothing
            End If
            If IsNothing(_param) = False Then
                _param = Nothing
            End If
            If IsNothing(returnedDatatable) = False Then
                returnedDatatable.Dispose()
                returnedDatatable = Nothing
            End If
        End Try






    End Function



    Public Sub DeleteHistory(ByVal VisitID As Long, ByVal PatientID As Long)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gsp_DeleteHistory", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = VisitID

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            Con.Open()
            cmd.ExecuteNonQuery()
            Con.Close()
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
        End Try

    End Sub

    Public Sub SaveNarration(ByVal VisitID As Long, ByVal PatientID As Long, ByVal strTempFileName1 As String)

        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Dim mstream As ADODB.Stream

        Try
            cmd = New SqlCommand("gsp_InUpNarration", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            'Narration as Word Document
            sqlParam = cmd.Parameters.Add("@Narration", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input

            mstream = New ADODB.Stream
            mstream.Type = ADODB.StreamTypeEnum.adTypeBinary
            mstream.Open()
            mstream.LoadFromFile(strTempFileName1)
            sqlParam.Value = mstream.Read()
            mstream.Close()
            mstream = Nothing

            'Resolving Bug #83522: EMR: Patien History- Application gives exception on save and close button of OB history
            If Con.State <> ConnectionState.Open Then
                Con.Open()
            End If

            cmd.ExecuteNonQuery()


        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
        End Try
    End Sub

    Public Sub SaveNarrationBytes(ByVal VisitID As Long, ByVal PatientID As Long, ByVal bBytes() As Byte)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gsp_InUpNarration", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = VisitID

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input

            'Narration as Word Document
            sqlParam = cmd.Parameters.Add("@Narration", SqlDbType.Image)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam.Value = bBytes.Clone()

            Con.Open()
            cmd.ExecuteNonQuery()

            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Add, strExamName & " Narration Added", gstrLoginName, gstrClientMachineName, nPatinetID)
            ' objAudit = Nothing
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
        End Try
    End Sub ''For Edit Narration
    Public Function SelectNarration(ByVal VisitID As Long, ByVal PatientID As Long) As DataTable
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gsp_ScanNarration", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.AddWithValue("@VisitID", VisitID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = VisitID

            sqlParam = cmd.Parameters.AddWithValue("@PatientID", PatientID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = PatientID

            Con.Open()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Con.Close()
            da.Dispose()
            da = Nothing
            Return dt

            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Add, strExamName & " Narration Added", gstrLoginName, gstrClientMachineName, nPatinetID)
            'objAudit = Nothing
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
        End Try
    End Function

    Public Sub DeleteNarration(ByVal VisitID As Long, ByVal PatientID As Long)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing

        Try
            cmd = New SqlCommand("gsp_DeleteNarration", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.AddWithValue("@VisitId", VisitID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = VisitID

            sqlParam = cmd.Parameters.AddWithValue("@PatientId", PatientID)
            sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = PatientID

            Con.Open()
            cmd.ExecuteNonQuery()
            Con.Close()

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Patient History", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If IsNothing(sqlParam) = False Then
                sqlParam = Nothing
            End If
        End Try
    End Sub



    Public Function Fill_LockPatientHistory(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable

        Dim Cmd As New SqlCommand
        Try


            Cmd = New System.Data.SqlClient.SqlCommand("gsp_Select_UnLock_Record", Con)
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

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = Cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Con.Close()
            da.Dispose()
            da = Nothing
            objParam = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Con.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Function


    'Public Function FillDetails(ByVal ConceptID As String) As DataTable
    '    Dim cmd As SqlCommand

    '    Dim dtDetails As New DataTable
    '    Dim da As New SqlDataAdapter

    '    Try
    '        Con.Open()
    '        cmd = New SqlCommand()

    '        cmd.CommandType = CommandType.Text
    '        cmd.CommandText = "SELECT ISNULL(sDESCRIPTIONID,'') AS sDESCRIPTIONID,ISNULL(sSnoMedID,'') AS sSnoMedID,ISNULL(sTranID1,'') AS sTranID1,ISNULL(sTranID2,'') AS sTranID2 FROM History_MST WHERE sCONCEPTID = " & ConceptID & ""
    '        cmd.Connection = Con

    '        da.SelectCommand = cmd
    '        da.Fill(dtDetails)



    '        Con.Close()


    '    Catch ex As Exception
    '        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
    '        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

    '    Finally
    '        If IsNothing(cmd) = False Then
    '            cmd.Dispose()
    '            cmd = Nothing
    '        End If
    '        If IsNothing(da) = False Then
    '            da.Dispose()
    '            da = Nothing
    '        End If
    '    End Try
    '    Return dtDetails
    'End Function
    Public Function FillDetailsFromMaster(ByVal ConceptID As String, ByVal Description As String) As DataTable

        Dim Cmd As New SqlCommand
        Dim objParam As SqlParameter
        Try


            Cmd = New System.Data.SqlClient.SqlCommand("History_FillDetailsFromMst", Con)
            Cmd.CommandType = CommandType.StoredProcedure


            objParam = Cmd.Parameters.Add("@ConceptID", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ConceptID

            objParam = Cmd.Parameters.Add("@Description", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Description



            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = Cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Con.Close()
            da.Dispose()
            da = Nothing

            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Con.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
        End Try
    End Function
    Public Function FillSubTypeTreeView(ByVal ConceptID As String, ByVal NodeText As String) As DataTable
        Dim cmd As SqlCommand = Nothing
        Try
            Con.Open()
            cmd = New SqlCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = " SELECT ISNULL(sSnomedDefination,'') AS sSnomedDefination,ISNULL(sConceptID,'') AS sConceptID,ISNULL(sDescriptionID,'') AS sDescriptionID,ISNULL(sSnoMedID,'') AS sSnoMedID From History_Mst WHERE sConceptID = " + ConceptID + " AND sDescription = '" + NodeText.Replace("'", "''") + "' "
            cmd.Connection = Con
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Con.Close()
            da.Dispose()
            da = Nothing
            Return dt

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function
    Public Function Fill_ICD9(ByVal ConceptId As String)
        Dim cmd As SqlCommand = Nothing
        Try
            Con.Open()
            cmd = New SqlCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = " SELECT ISNULL(sICD9,'') AS sICD9, ISNULL(sConceptID,'') AS sConceptID FROM History_MST WHERE sConceptID = '" + ConceptId + "' "
            cmd.Connection = Con
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Con.Close()
            da.Dispose()
            da = Nothing
            Return dt
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try

    End Function
    Public Function GetReviewHistory(ByVal m_PatientID As Int64, ByVal visitid As Int64) As DataTable
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Try

            Dim strSelect = "Select sComments,dtReviewDate from ReviewHistory where nPatientID = " & m_PatientID & " AND nVisitID = " & visitid & " Order By dtReviewDate DESC"
            '   Dim strdtReviewDate As String
            Dim dtReview As DataTable
            oDB.Connect(GetConnectionString)
            dtReview = oDB.ReadQueryDataTable(strSelect)
            oDB.Disconnect()
            Return dtReview
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Sub UpdateMedicalReconcillation(ByVal m_PatientID As Long, ByVal visitid As Long, ByVal nReconcillationType As Int16, Optional ByVal dtUpdateMedRec As DataTable = Nothing)
        Dim oDB As New gloStream.gloDataBase.gloDataBase
        Try
            Dim cmd As SqlCommand = Nothing

            oDB.Connect(GetConnectionString)
            oDB.DBParameters.Clear()
            oDB.DBParameters.Add("@nPatID", m_PatientID, ParameterDirection.Input, SqlDbType.BigInt)

            oDB.DBParameters.Add("@nVisID", visitid, ParameterDirection.Input, SqlDbType.BigInt)

            oDB.DBParameters.Add("@nReconcillationType", nReconcillationType, ParameterDirection.Input, SqlDbType.SmallInt)

            oDB.DBParameters.Add("@Rx_MedicationReconcillation", dtUpdateMedRec, ParameterDirection.Input, SqlDbType.Structured)


            oDB.DBParameters.Add("@nProviderID", gloGlobal.gloPMGlobal.LoginProviderID, ParameterDirection.Input, SqlDbType.BigInt)




            oDB.ExecuteNonQuery("gsp_UpdateMedicationReconcillation")
            oDB.Disconnect()

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            If IsNothing(oDB) = False Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Sub

    Public Function GetHistoyOFHistory(ByVal npatientid As Int64) As DataTable

        Dim _sqlcmd As SqlCommand = Nothing

        Dim objParam As SqlParameter
        Try
            Con.Open()
            _sqlcmd = New SqlCommand("gsp_GetHistoryOfHistory", Con)
            _sqlcmd.CommandType = CommandType.StoredProcedure


            objParam = _sqlcmd.Parameters.Add("@PatientID", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = npatientid

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = _sqlcmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            Con.Close()
            da.Dispose()
            da = Nothing
            Return dt


        Catch ex As Exception
            Return Nothing
        Finally

            If IsNothing(_sqlcmd) = False Then
                _sqlcmd.Parameters.Clear()
                _sqlcmd.Dispose()
                _sqlcmd = Nothing
            End If

            objParam = Nothing
        End Try
    End Function

    Public Function Fill_ICD9Code(ByVal ICD9 As String, ByVal CPT As String)
        Dim _sqlcmd As SqlCommand = Nothing
        Dim ds As New DataSet
        Dim _sqlda As New SqlDataAdapter
        Dim objParam As SqlParameter
        Try
            Con.Open()
            _sqlcmd = New SqlCommand("History_FillICD9Code", Con)
            _sqlcmd.CommandType = CommandType.StoredProcedure


            objParam = _sqlcmd.Parameters.Add("@ICD9", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ICD9

            objParam = _sqlcmd.Parameters.Add("@CPT", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CPT

            _sqlda.SelectCommand = _sqlcmd

            _sqlda.Fill(ds)
            ds.Tables(0).TableName = "ICD9"
            ds.Tables(1).TableName = "CPT"
            Con.Close()
            Return ds
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(_sqlcmd) = False Then
                _sqlcmd.Parameters.Clear()
                _sqlcmd.Dispose()
                _sqlcmd = Nothing
            End If
            If IsNothing(_sqlda) = False Then
                _sqlda.Dispose()
                _sqlda = Nothing
            End If
            objParam = Nothing
        End Try
        Return ds
    End Function
    Public Function CheckHistoryforCurrentVisit(ByVal PatientID As Long, ByVal VisitID As Long) As Boolean
        Dim _sqlcmd As SqlCommand = Nothing
        Dim _Sqlparam As SqlParameter = Nothing

        Try


            'objBusLayer.Open_Con()
            _sqlcmd = New SqlCommand("History_CheckHistoryForVisit", Con)
            _sqlcmd.CommandType = CommandType.StoredProcedure


            _Sqlparam = _sqlcmd.Parameters.AddWithValue("@PatientID", PatientID)
            _Sqlparam.Direction = ParameterDirection.Input
            '  sqlParam1.Value = PatientID

            _Sqlparam = _sqlcmd.Parameters.AddWithValue("@VisitID", VisitID)
            _Sqlparam.Direction = ParameterDirection.Input
            ' sqlParam1.Value = VisitID

           

           
            ''''''''DONT CLOSE CONNECTION HERE
            '''''' IT IS HANDLED IN AddNewHistory Function

            Dim rowAffected As Long
            '    Dim dataread As SqlDataReader
            Con.Open()
            rowAffected = CType(_sqlcmd.ExecuteScalar, Long)
            Con.Close()
            If rowAffected > 0 Then
                Return True     ' Duplicate Exists
            Else
                Return False    ' Duplicate Not Exists
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If IsNothing(_sqlcmd) = False Then
                _sqlcmd.Parameters.Clear()
                _sqlcmd.Dispose()
                _sqlcmd = Nothing
            End If
            If IsNothing(_Sqlparam) = False Then

                _Sqlparam = Nothing
            End If
        End Try
    End Function
    Public Function getHistoryTypefromcategorymaster_Other(ByVal _categoryType As String, ByVal ds As DataSet)
        Dim _CategoryTypeFromMaster As String = ""
        If (IsNothing(ds) = False) Then


            For i As Integer = 0 To ds.Tables(1).Rows.Count - 1
                If ds.Tables(1).Rows(i)("sDescription") = _categoryType Then
                    _CategoryTypeFromMaster = ds.Tables(1).Rows(i)("sHistoryType")
                    Return _CategoryTypeFromMaster
                End If
            Next
        End If
        Return ""
    End Function
    Public Function CheckHistoryTypeinStandardTable_other(ByVal CategoryType As String, ByVal ds As DataSet) As String
        Dim IsOnsetDate As Boolean = False
        Dim IsActive As Boolean = False
        Dim strOnsetActiveStatus As String = ""
        If (IsNothing(ds) = False) Then


            If IsNothing(ds.Tables(0)) = False Then
                If ds.Tables(0).Rows.Count > 0 Then
                    For h As Integer = 0 To ds.Tables(0).Rows.Count - 1

                        If ds.Tables(0).Rows(h)("sShortDescription").ToString().Trim = CategoryType Then
                            If Convert.ToBoolean(ds.Tables(0).Rows(h)("bIsOnsetDate")) = True Then
                                IsOnsetDate = True

                            End If
                            If Convert.ToBoolean(ds.Tables(0).Rows(h)("bIsActive")) = True Then
                                IsActive = True
                            End If
                            Exit For
                        End If




                    Next
                End If
            End If
        End If
        strOnsetActiveStatus = IsOnsetDate & "," & IsActive
        Return strOnsetActiveStatus
    End Function
    Public Function Fill_History(ByVal PatientID As Long, ByVal VisitID As Long, ByVal Flag As Integer) As DataTable
        Dim con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim objParam As SqlParameter
        Try
            cmd = New SqlCommand("gsp_GetHistory", con)
            cmd.CommandType = CommandType.StoredProcedure


            objParam = cmd.Parameters.Add("@dtVisitdate", SqlDbType.DateTime)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Now.Date

            objParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = PatientID

            objParam = cmd.Parameters.Add("@flag", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Flag

            objParam = cmd.Parameters.Add("@VisitId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = VisitID

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            con.Close()
            da.Dispose()
            da = Nothing
            Return dt


        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorsDashBoard -- Fill_History -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDoctorsDashBoard -- Fill_History -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If
            objParam = Nothing
        End Try
    End Function

    Public Function Fill_StandardHistoryTypes() As DataSet
        Dim con As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand("History_GetStandardHistoryTypes", con)
            cmd.CommandType = CommandType.StoredProcedure



            Dim da As New SqlDataAdapter
            Dim ds As New DataSet
            da.SelectCommand = cmd
            da.Fill(ds)
            ds.Tables(1).TableName = "CategoryType"
            ds.Tables(0).TableName = "HistoryTypes"
            da.Dispose()
            da = Nothing
            Return ds

        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDoctorsDashBoard -- Fill_History -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDoctorsDashBoard -- Fill_History -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(con) Then
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Function
End Class


