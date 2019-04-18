Imports System.Data.SqlClient


Public Class clsLabs

    'Private da As SqlDataAdapter
    ' Private ds As New DataSet
    ' Private dt As DataTable
    Private dv As DataView
    Private Con As SqlConnection
    ' Private conString As String

    Public Sub New()
        Try
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
        Catch ex As SqlException   ' Catch the error.
            MessageBox.Show(ex.ToString, "Labs", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Labs", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        End Try
    End Sub
    Public Sub Dispose()
        If (IsNothing(Con) = False) Then
            Con.Dispose()
            Con = Nothing
        End If
        If (IsNothing(dv) = False) Then
            dv.Dispose()
            dv = Nothing
        End If
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

    Public Sub SortDataview(ByVal strsort As String, Optional ByVal strSortOrder As String = "")
        'DCatview.Sort = strsort
        If (IsNothing(dv) = False) Then
            dv.Sort = "[" & strsort & "]" & strSortOrder
        End If

    End Sub

    Public Sub SetRowFilter(ByVal txtSearch As String)
        Dim strexpr As String
        Dim str As String
        If (IsNothing(dv) = False) Then
            str = dv.Sort
            str = Splittext(str)
            str = Mid(str, 2)
            str = Mid(str, 1, Len(str) - 1)

            strexpr = "" & dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
            dv.RowFilter = strexpr
        End If


    End Sub

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

    Public Sub SetRowFilter(ByVal ColIndex As Integer, ByVal txtSearch As String)
        Dim strexpr As String
        If (IsNothing(dv) = False) Then
            strexpr = "" & dv.Table.Columns(ColIndex).ColumnName() & "  Like '" & txtSearch & "%'"
            dv.RowFilter = strexpr
        End If

    End Sub

    Public Sub SetRowFilter(ByVal txtSearch As String, ByVal Dview As DataView)
        Dim strexpr As String
        Dim str As String
        If (IsNothing(Dview) = False) Then
            str = Dview.Sort
            str = Splittext(str)
            str = Mid(str, 2)
            str = Mid(str, 1, Len(str) - 1)
            strexpr = "" & Dview.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
            Dview.RowFilter = strexpr
        End If

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

    Public Function GetAllLabs() As DataView
        Try
            Dim cmd As New SqlCommand("gsp_ViewLabs_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure

            Con.Open()
            'cmd.ExecuteNonQuery()                      
            '  ds.Clear()
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

            Return dv

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Labs", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Labs", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function

    Public Function CheckDuplicate(ByVal id As Long, ByVal Description As String) As Boolean

        Try
            'objBusLayer.Open_Con()
            Dim cmd As New SqlCommand("gsp_CheckLabs_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@ID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = id

            sqlParam = cmd.Parameters.Add("@Description", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Description

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
            UpdateLog("clsLabs - CheckDuplicate : " & ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Labs", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'objBusLayer = Nothing
            Con.Close()
        End Try
    End Function

    Public Sub SelectLabs(ByVal ID As Long)
        Try

            Dim cmd As New SqlCommand("gsp_ScanLabs_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@LabsID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

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
        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Labs", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Labs", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try
    End Sub

    Public Sub AddNewLabs(ByVal ID As Long, ByVal Description As String, ByVal Unit As String, ByVal LowValueMale As Int64, ByVal HighValueMale As Int64, ByVal LowValueFemale As Int64, ByVal HighValueFemale As Int64, ByVal TemplateID As Int64)
        Dim cmd As SqlCommand = Nothing

        Try
            cmd = New SqlCommand("gsp_InUpLabs_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@LabsID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            sqlParam = cmd.Parameters.Add("@Description", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Description

            sqlParam = cmd.Parameters.Add("@Unit", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Unit

            sqlParam = cmd.Parameters.Add("@LowValueMale", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = LowValueMale

            sqlParam = cmd.Parameters.Add("@HighValueMale", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = HighValueMale

            sqlParam = cmd.Parameters.Add("@LowValueFemale", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = LowValueFemale

            sqlParam = cmd.Parameters.Add("@HighValueFemale", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = HighValueFemale

            sqlParam = cmd.Parameters.Add("@TemplateID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = TemplateID

            sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = GetPrefixTransactionID()

            Con.Open()
            cmd.ExecuteNonQuery()

            'Dim objAudit As New clsAudit
            If ID <> 0 Then
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Labs modified.", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Labs modified.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            Else
                ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Labs added.", gloAuditTrail.ActivityOutCome.Success)
                ''Added Rahul P on 20101009
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, "Labs added.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
                ''
            End If

            sqlParam = Nothing
            'objAudit = Nothing
        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Labs", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Labs", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Sub

    Public Sub DeleteLabs(ByVal ID As Long, ByVal Description As String)

        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand("gsp_DeleteLabs_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@LabsID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            Con.Open()
            cmd.ExecuteNonQuery()

            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Labs deleted.", gloAuditTrail.ActivityOutCome.Success)
            sqlParam = Nothing
        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Labs", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Labs", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try

    End Sub

    Public Function GetAllTemplate() As DataTable
        Try
            Dim cmd As New SqlCommand("gsp_FillTemplateGallery_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@flag", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 1  ''for Labs flag =1

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
            sqlParam = Nothing

            Return dt

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Labs", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Labs", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function

    Public Sub SetRowFilter(ByVal ColIndex As Integer, ByVal txtSearch As String, ByVal dview As DataView)
        Dim strexpr As String
        strexpr = "" & dview.Table.Columns(ColIndex).ColumnName() & "  Like '" & txtSearch & "%'"
        dview.RowFilter = strexpr
    End Sub
    Public Function Fill_LockLab(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable
        Try
            Dim Cmd As New SqlCommand
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
            Dim sqladpt As New SqlDataAdapter

            sqladpt.SelectCommand = Cmd
            Dim dt As New DataTable

            sqladpt.Fill(dt)

            Con.Close()
            sqladpt.Dispose()
            sqladpt = Nothing

            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
            Return dt
        Catch ex As SqlException
            UpdateLog("clsLabs - Fill_LockLab : " & ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            Con.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
End Class
