Imports System.Data.SqlClient

Public Class clsICD9
    ' Private da As SqlDataAdapter
    'Private ds As New DataSet
    '  Private dt As DataTable
    Private dv As DataView
    Private Con As SqlConnection
    ' Private conString As String

    Public Sub New()
        Try

            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
        Catch ex As Exception   ' Catch the error.
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        End Try
    End Sub

    'Shubhangi
    Public Sub New(ByVal sConnectionString As String)
        If sConnectionString <> "" Then
            Con = New SqlConnection(sConnectionString)
            Con.Open()
        End If
    End Sub

    Public Sub Dispose()
        If Con IsNot Nothing Then
            If Con.State = ConnectionState.Open Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If
        End If
        If dv IsNot Nothing Then
            dv.Dispose()
            dv = Nothing
        End If
    End Sub
    'End

    'Public Function Search(ByVal dv As DataView, ByVal colIndex As Integer, ByVal txtSearch As String) As DataView
    '    'Dim dv As DataView
    '    'dv = grdCPT.DataSource

    '    dv.RowFilter = "" & dv.Table.Columns(colIndex).ColumnName & " Like '" & txtSearch & "%'"
    '    'grdCPT.DataSource = dv
    'End Function

    Public Sub Search(ByVal dv As DataView, ByVal colIndex As Integer, ByVal txtSearch As String)
        'Dim dv As DataView
        'dv = grdCPT.DataSource
        If (IsNothing(dv) = False) Then
            dv.RowFilter = "" & dv.Table.Columns(colIndex).ColumnName & " Like '" & txtSearch & "%'"
        End If

        'grdCPT.DataSource = dv
    End Sub

    Public Function GetAllICD(ByVal SpecialityID As Int64) As DataView
        Try
            Dim cmd As New SqlCommand("gsp_ViewICD9", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@id", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = SpecialityID

            Con.Open()
            'cmd.ExecuteNonQuery()
            ' ds.Clear()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            dv = New DataView(dt.Copy())
            da.Dispose()
            da = Nothing
            dt.Dispose()
            dt = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
           
            Return dv
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsICD9 - GetAllICD : " & ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
            'objBusLayer = Nothing

        End Try
    End Function

    Public Function GetAllICD9(Optional ByVal Speciality As String = "") As DataTable
        Dim cmd As SqlCommand = Nothing
        Try
            ''code commented for optimization 6020
            'Dim dtICD9 As New DataTable
            'Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
            'oDB.Connect(False)
            'Dim query As String = ""
            ''Code Modified by Mayuri:20091005
            ''Used Speciality varible in order to fill treeview ICD9Current according to selected Speciality
            'If Speciality = "" Or Speciality = "All" Then
            '    query = "SELECT nICD9ID, ISNULL(sICD9Code,'') AS sICD9Code, ISNULL(sDescription,'') AS sDescription FROM ICD9 WHERE ISNULL(bIsBlocked,0) = 0"
            'Else
            '    'query = "SELECT nICD9ID, ISNULL(sICD9Code,'') AS sICD9Code, ISNULL(sDescription,'') AS sDescription FROM ICD9 WHERE ISNULL(bIsBlocked,0) = 0 AND ="
            '    query = "SELECT nICD9ID, ISNULL(sICD9Code,'') AS sICD9Code, ISNULL(ICD9.sDescription,'') AS sDescription FROM ICD9 LEFT OUTER JOIN " _
            '         & " Specialty_MST ON ICD9.nSpecialtyID = Specialty_MST.nSpecialtyID WHERE ISNULL(ICD9.bIsBlocked,0) = 0 AND Specialty_MST.sDescription = '" & Speciality & "'"
            'End If
            ''End Code Modified by Mayuri:20091005
            'oDB.Retrive_Query(query, dtICD9)
            'oDB.Disconnect()
            'oDB.Dispose()
            'oDB = Nothing
            'If IsNothing(dtICD9) = False Then
            '    Return dtICD9
            'End If
            ''converted query into stored procdure in 6020
            cmd = New SqlCommand("gsp_GetAllICD9", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter
            sqlParam = cmd.Parameters.Add("@Speciality", SqlDbType.VarChar, 255)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Speciality

            Con.Open()
            'cmd.ExecuteNonQuery()

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing
            sqlParam = Nothing
            Return dt


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
            'If IsNothing(dt) = False Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
           
        End Try
    End Function

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

    Public Function CheckDuplicate(ByVal ICD9Code As String, ByVal description As String) As Boolean
        Dim _isConOpened As Boolean = False
        Try
            'description = description.Replace("'", "''")
            'Dim cmd As New SqlCommand("SELECT COUNT(*) FROM ICD9 WHERE sICD9Code = '" & ICD9Code & "' AND sDescription = '" & description & "'", Con)

            Dim oResult As Object
            ''If Con.State = ConnectionState.Closed Then
            'If Con Is Nothing Then
            '    Con.Open()
            'End If
            'oResult = cmd.ExecuteScalar
            'Con.Close()
            'If CType(oResult, Int32) > 0 Then
            '    Return True
            'Else
            '    Return False
            'End If
            Dim oCmd As SqlCommand
            Dim _strSQL As String = ""

            _strSQL = "SELECT COUNT(*) FROM ICD9 WHERE sICD9Code = '" & ICD9Code.Replace("'", "''") & "' AND sDescription = '" & description.Replace("'", "''") & "'"

            'Shubhangi
            If Con Is Nothing Then
                Con = New SqlConnection
            End If
            If Con.State = ConnectionState.Closed Then
                Con.Dispose()
                Con = New SqlConnection(GetConnectionString)
                Con.Open()
                _isConOpened = True
            End If
            oCmd = New SqlCommand(_strSQL, Con)
            'End
            oResult = oCmd.ExecuteScalar()

            If oCmd IsNot Nothing Then
                oCmd.Parameters.Clear()
                oCmd.Dispose()
                oCmd = Nothing
            End If

            If IsNothing(oResult) = False Then
                If oResult.ToString <> "" Then
                    If CInt(oResult.ToString) > 0 Then
                        Return True
                    End If
                End If
            End If

            Return False


        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If _isConOpened Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If
        End Try
    End Function

    Public Function CheckDuplicate(ByVal ICD9ID As Long, ByVal DrugName As String, ByVal SpecialtyID As Int64) As Boolean

        Try
            'objBusLayer.Open_Con()
            Dim cmd As New SqlCommand("gsp_CheckICD9_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@ID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ICD9ID

            sqlParam = cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = DrugName

            sqlParam = cmd.Parameters.Add("@SpecialtyID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = SpecialtyID

            Con.Open()

            Dim rowAffected As Int64
            rowAffected = CType(cmd.ExecuteScalar, Int64)

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
            If rowAffected > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As SqlException
            'UpdateLog("clsICD9 - CheckDuplicate : " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            'objBusLayer = Nothing
            Con.Close()
        End Try
    End Function

    Public Sub SelectICD9(ByVal ID As Long)
        'Dim objBusLayer As New clsBuslayer
        Try

            Dim cmd As New SqlCommand("gsp_ScanICD9", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@ICD9ID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            Con.Open()
            'cmd.ExecuteNonQuery()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            If (IsNothing(dv) = False) Then
                dv.Dispose()
                dv = Nothing
            End If
            dv = dt.Copy().DefaultView()
            da.Dispose()
            da = Nothing
            dt.Dispose()
            dt = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            sqlParam = Nothing
            'Return objBusLayer.PassCmdGetDV(cmd)
            'objBusLayer.Close_Con()
        Catch ex As SqlException
            'UpdateLog("clsICD9 - SelectICD9 : " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Select, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'objBusLayer = Nothing
            Con.Close()
        End Try
    End Sub

    Public Function GetAllSpeciality() As DataTable

        Try

            Dim cmd As New SqlCommand("gsp_FillSpecialty_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure

            Con.Open()
            'cmd.ExecuteNonQuery()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            da.Dispose()
            da = Nothing

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            Return dt
            'Return objBusLayer.PassCmdGetDV(cmd)
            'objBusLayer.Close_Con()
        Catch ex As SqlException
            'UpdateLog("clsICD9 - GetAllSpeciality : " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try

    End Function

    Public Sub AddNewICD9(ByVal ID As Long, ByVal ICD9Code As String, ByVal Description As String, ByVal SpecialtyID As Long)
        'Dim objBusLayer As New clsBuslayer
        Try
            If Con Is Nothing Then
                Con = New SqlConnection(GetConnectionString)
            End If
            'objBusLayer.Open_Con()
            Dim cmd As New SqlCommand("gsp_InUpICD9", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@ICD9ID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            sqlParam = cmd.Parameters.Add("@ICD9Code", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ICD9Code

            sqlParam = cmd.Parameters.Add("@Description", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Description

            sqlParam = cmd.Parameters.Add("@SpecialtyID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = SpecialtyID

            sqlParam = cmd.Parameters.Add("@ClinicID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = gnClinicID

            'sqlParam = cmd.Parameters.Add("@MachineID", GetPrefixTransactionID)
            sqlParam = cmd.Parameters.AddWithValue("@MachineID", GetPrefixTransactionID)
            sqlParam.Direction = ParameterDirection.Input

            sqlParam = cmd.Parameters.AddWithValue("@Inactive", False)
            sqlParam.Direction = ParameterDirection.Input

            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
            cmd.ExecuteNonQuery()
            'Return objBusLayer.PassCmdGetDV(cmd)


            If ID <> 0 Then
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, " ICD9 Modified", gstrLoginName, gstrClientMachineName)
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Modify, "ICD9 Modified", gloAuditTrail.ActivityOutCome.Success)
                'gloAuditTrail.gloAuditTrail.UpdateLog(
            Else
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, " ICD9 Added", gstrLoginName, gstrClientMachineName)
                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "ICD9 Added", gloAuditTrail.ActivityOutCome.Success)
            End If

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            sqlParam = Nothing
        Catch ex As SqlException
            'UpdateLog("clsICD9 - AddNewICD9 : " & ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Con.State = ConnectionState.Open Then ''connection state closed
                Con.Close()
            End If
            'Con.Close()
        End Try
    End Sub
    'Code Added by Mayuri:20091003
    'To delete ICD9 from either ICD9Gallery  or CurrentICD9
    Public Sub DeletICD9(ByVal ID As Long, ByVal _isSelectedICD9Gallery As Boolean)
        Try
            ' Dim dtICD9Gallery As New DataTable

            ' Dim conn As New SqlConnection(GetConnectionString())
            Dim oCmd As SqlCommand
            Dim _strSQL As String = ""
            Dim _isConOpened As Boolean = False
            ' Dim _result As Object

            'Dim ad As SqlDataAdapter

            If _isSelectedICD9Gallery = True Then
                _strSQL = "delete ICD9Gallery where nICD9ID=" & ID & ""
            Else
                _strSQL = "delete ICD9 where nICD9ID=" & ID & ""
            End If
            'Shubhangi
            If Con Is Nothing Then
                Con = New SqlConnection
            End If
            If Con.State = ConnectionState.Closed Then
                Con.Dispose()
                Con = New SqlConnection(GetConnectionString)
                Con.Open()
                _isConOpened = True
            End If
            oCmd = New SqlCommand(_strSQL, Con)
            'End

            'oCmd = New SqlCommand()

            'oCmd.Connection = conn
            'oCmd.CommandType = CommandType.Text
            'oCmd.CommandText = _strSQL
            'conn.Open()

            oCmd.ExecuteNonQuery()
            oCmd.Parameters.Clear()
            oCmd.Dispose()
            oCmd = Nothing

            If _isConOpened Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            '  Con.Close()
        End Try
    End Sub
    
    Public Sub DeleteCurrentCPT(ByVal ID As Long, ByVal _isSelectedTreeview As Boolean)
        'Try
        '    Dim dtCPTGallery As New DataTable

        '    Dim conn As New SqlConnection(GetConnectionString())
        '    Dim oCmd As SqlCommand
        '    Dim _strSQL As String = ""
        '    Dim _result As Object

        '    Dim ad As SqlDataAdapter


        '    _strSQL = "delete CPT_MST where nCPTID=" & ID & ""

        '    oCmd = New SqlCommand()

        '    oCmd.Connection = conn
        '    oCmd.CommandType = CommandType.Text
        '    oCmd.CommandText = _strSQL
        '    conn.Open()

        '    oCmd.ExecuteNonQuery()
        'Catch ex As Exception
        '    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
        '    MessageBox.Show(ex.ToString, "CPT", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'Finally
        '    Con.Close()
        'End Try

    End Sub




    Public Sub DeleteICD9(ByVal ID As Long, ByVal ICD9Code As String)
        Try
            Dim cmd As New SqlCommand("gsp_DeleteICD9", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@id", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID
            Con.Open()
            cmd.ExecuteNonQuery()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, ICD9Code & " ICD9 Code Deleted", gstrLoginName, gstrClientMachineName)
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Delete, "ICD9 Deleted", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Delete, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            'UpdateLog("clsICD9 - DeleteICD9 : " & ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Delete, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try
    End Sub

    Public Sub SetRowFilter(ByVal txtSearch As String)
        Dim strexpr As String
        Dim str As String
        str = dv.Sort
        str = Splittext(str)
        str = Mid(str, 2)
        str = Mid(str, 1, Len(str) - 1)

        strexpr = "" & dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
        dv.RowFilter = strexpr

    End Sub
    ''Public Sub SetRowFilter(ByVal txtSearch As String, ByVal Dview As DataView)
    ''    Dim strexpr As String
    ''    Dim str As String
    ''    str = Dview.Sort
    ''    str = Splittext(str)
    ''    str = Mid(str, 2)
    ''    str = Mid(str, 1, Len(str) - 1)
    ''    strexpr = "" & Dview.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
    ''    Dview.RowFilter = strexpr
    ''End Sub
    Public Sub SortDataview(ByVal strsort As String, Optional ByVal strSortOrder As String = "")
        'DCatview.Sort = strsort
        dv.Sort = "[" & strsort & "]" & strSortOrder
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            Return strsplittext
        End Try
    End Function

End Class
