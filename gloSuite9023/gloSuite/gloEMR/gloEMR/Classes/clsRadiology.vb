Imports System.Data.SqlClient


Public Class clsRadiology

    ' Private da As SqlDataAdapter
    '   Private ds As New DataSet
    'Private dt As DataTable
    Private dv As DataView = Nothing
    Private Con As SqlConnection = Nothing
    'Private conString As String
    Public Sub Dispose()
       
                If Con IsNot Nothing Then
                    Con.Dispose()
                    Con = Nothing

                End If
                If IsNothing(dv) = False Then
                    dv.Dispose()
                    dv = Nothing
                End If
       
    End Sub

    Public Sub New()
        Try
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Labs", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()

        Catch ex As Exception   ' Catch the error.
            MessageBox.Show(ex.ToString, "Labs", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    Public Sub SortDataview(ByVal strsort As String)
        'DCatview.Sort = strsort
        If (IsNothing(dv) = False) Then
            dv.Sort = "[" & strsort & "]"
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
    Public Function GetAllRadiology() As DataView
        Try
            Dim cmd As New SqlCommand("gsp_ViewRadiology_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure

            Con.Open()
            'cmd.ExecuteNonQuery()
            '  ds.Clear()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            dv = New DataView(dt.Copy())
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            dt.Dispose()
            dt = Nothing
            da.Dispose()

            da = Nothing
            Return dv
        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Radiology", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Radiology", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing

        Finally
            Con.Close()
        End Try
    End Function
    Public Function GetAllTemplate() As DataTable
        Try
            Dim cmd As New SqlCommand("gsp_FillTemplateGallery_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@flag", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = 2  ''for Radiology flag =1

            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
            da.Dispose()

            da = Nothing
            Return dt
        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Radiology", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Radiology", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
        End Try
    End Function
    Public Function CheckDuplicate(ByVal ID As Long, ByVal Description As String, ByVal TemplateID As Long) As Boolean

        Try
            'objBusLayer.Open_Con()
            Dim cmd As New SqlCommand("gsp_CheckRadology_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@ID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            sqlParam = cmd.Parameters.Add("@Description", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Description

            sqlParam = cmd.Parameters.Add("@TemplateID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = TemplateID
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
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Labs", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'objBusLayer = Nothing
            Con.Close()
        End Try
    End Function
    Public Function SelectRadiology(ByVal ID As Long)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Try
            cmd = New SqlCommand("gsp_ScanRadiology_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure           

            sqlParam = cmd.Parameters.Add("@RadiologyID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            Con.Open()
            'cmd.ExecuteNonQuery()
            Dim da As SqlDataAdapter = New SqlDataAdapter
            da.SelectCommand = cmd
            Dim dt As DataTable = New DataTable
            da.Fill(dt)
            dv = New DataView(dt.Copy())
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            dt.Dispose()
            dt = Nothing
            da.Dispose()

            da = Nothing
            sqlParam = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Radiology", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
        Return Nothing
    End Function

    Public Function AddNewRadiology(ByVal ID As Long, ByVal Description As String, ByVal TemplateID As Long)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Try
            cmd = New SqlCommand("gsp_InUpRadiology_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure            

            sqlParam = cmd.Parameters.Add("@RadiologyID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            sqlParam = cmd.Parameters.Add("@Description", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Description

            sqlParam = cmd.Parameters.Add("@TemplateID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = TemplateID

            sqlParam = cmd.Parameters.AddWithValue("@MachineID", GetPrefixTransactionID)
            sqlParam.Direction = ParameterDirection.Input

            Con.Open()
            cmd.ExecuteNonQuery()
            'Return objBusLayer.PassCmdGetDV(cmd)

            'Dim objAudit As New clsAudit
            If ID <> 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Modify, Description & " Radiology Modified", gloAuditTrail.ActivityOutCome.Success)
                'objAudit.CreateLog(clsAudit.enmActivityType.Modify, Description & " Radiology Modified", gstrLoginName, gstrClientMachineName)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Add, Description & " Radiology Added", gloAuditTrail.ActivityOutCome.Success)
                'objAudit.CreateLog(clsAudit.enmActivityType.Add, Description & " Radiology Added", gstrLoginName, gstrClientMachineName)
            End If
        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Radiology", MessageBoxButtons.OK, MessageBoxIcon.Error)    'objAudit = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Radiology", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            Con.Close()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try
        Return Nothing
    End Function

    Public Function DeleteRadiology(ByVal ID As Long, ByVal Description As String)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Try
            cmd = New SqlCommand("gsp_DeleteRadiology_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@RadiologyID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            Con.Open()
            cmd.ExecuteNonQuery()
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Delete, "Radiology Deleted", gloAuditTrail.ActivityOutCome.Success)
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Delete, Description & " Radiology Deleted", gstrLoginName, gstrClientMachineName)
            'objAudit = Nothing
        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Radiology", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Radiology", MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            Con.Close()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try
        Return Nothing
    End Function
    Public Function Fill_LockRadiology(ByVal MachinName As String, ByVal TransactionType As Integer) As DataTable
        Try
            Dim dt As New DataTable
            Dim sqladpt As New SqlDataAdapter
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_Select_UnLock_Record", Con)
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
            sqladpt.Dispose()
            sqladpt = Nothing
            Con.Close()
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
            Return dt
        Catch ex As Exception
            Con.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
End Class
