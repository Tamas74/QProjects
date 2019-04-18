Imports System.Data.SqlClient


Public Class clsSIG


    ' Private da As SqlDataAdapter
    'Private ds As New DataSet
    'Private dt As DataTable
    Private dv As DataView = Nothing
    Private Con As SqlConnection = Nothing
    'Private conString As String

    Public Sub New()
        Try
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
        
        Catch ex As SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        Catch ex As Exception   ' Catch the error.
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
        End Try
    End Sub
    Public Sub Dispose()

        If Con IsNot Nothing Then
            Con.Dispose()
            Con = Nothing

        End If
        If IsNothing(dv) = False Then
            dv.Dispose()
            dv = Nothing
        End If
        'If IsNothing(Ds) = False Then
        '    Ds.Dispose()
        '    Ds = Nothing
        'End If
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

        Return Nothing
        'grdCPT.DataSource = dv
    End Function

    Public Sub SetRowFilter(ByVal txtSearch As String)
        Dim strexpr As String
        Dim str As String
        If (IsNothing(dv) = False) Then
            str = dv.Sort
            str = Splittext(str)
            str = Mid(str, 2)
            str = Mid(str, 1, Len(str) - 1)
            If str <> dv.Table.Columns(5).ColumnName Then
                strexpr = "" & dv.Table.Columns(str).ColumnName() & "  Like '" & txtSearch & "%'"
                dv.RowFilter = strexpr
            End If
        End If

    End Sub
    Public Sub SortDataview(ByVal strsort As String, Optional ByVal strSortOrder As String = "")
        'DCatview.Sort = strsort
        If (IsNothing(dv) = False) Then
            dv.Sort = "[" & strsort & "]" & strSortOrder
        End If

    End Sub
    Public Function GetAllSIG() As DataView
        Dim cmd As SqlCommand = Nothing
        Try
            cmd = New SqlCommand("gsp_ViewSIG_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            ' Dim sqlParam As SqlParameter
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
            dv = New DataView(dt.Copy())
            dt.Dispose()
            dt = Nothing
            da.Dispose()
            da = Nothing
            Return dv

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function

    Public Function CheckDuplicate(ByVal ID As Long, ByVal Dosage As String, ByVal sRoute As String, ByVal sFrequency As String, ByVal sDuration As String, ByVal sRefills As String, ByVal DispenseAmt As String, ByVal bAsNeeded As Boolean) As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Try
            'objBusLayer.Open_Con()
            cmd = New SqlCommand("gsp_CheckSIG_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@ID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            'sqlParam = cmd.Parameters.Add("@Dosage", SqlDbType.VarChar)
            'sqlParam.Direction = ParameterDirection.Input
            'sqlParam.Value = Dosage

            ''Sandip Darade 20090806  
            ' nSIGID, sDosage, sRoute, sFrequency, sDuration, bAsNeeded, sRefills
            sqlParam = cmd.Parameters.Add("@sRoute", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sRoute

            sqlParam = cmd.Parameters.Add("@sFrequency", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sFrequency

            sqlParam = cmd.Parameters.Add("@sDuration", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sDuration

            sqlParam = cmd.Parameters.Add("@sRefills", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = sRefills

            sqlParam = cmd.Parameters.Add("@sAmount", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = DispenseAmt

            sqlParam = cmd.Parameters.Add("@bAsNeeded", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = bAsNeeded
            Con.Open()

            Dim rowAffected As Int64
            rowAffected = CType(cmd.ExecuteScalar, Int64)
            If rowAffected > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            'objBusLayer = Nothing
            Con.Close()
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try
    End Function

    Public Function SelectSIG(ByVal ID As Long)
        Try
            Dim cmd As New SqlCommand("gsp_ScanSIG_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@SIGID", SqlDbType.BigInt)
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
            dv = New DataView(dt.Copy())
            dt.Dispose()
            dt = Nothing
            da.Dispose()
            da = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
            'dv = dt.DefaultView()

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Con.Close()
        End Try
        Return Nothing
    End Function

    Public Function AddNewSIG(ByVal ID As Long, ByVal Dosage As String, ByVal Route As String, ByVal Freq As String, ByVal Duration As String, ByVal AsNeeded As CheckState, ByVal Refills As String, ByVal DispenseAmt As String) As Long
        Dim _ReturnValue As Long = 0

        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter        
        Try
            cmd = New SqlCommand("gsp_InUpSIG_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure




            sqlParam = cmd.Parameters.Add("@Dosage", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Dosage

            sqlParam = cmd.Parameters.Add("@Route", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Route

            sqlParam = cmd.Parameters.Add("@Frequency", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Freq

            sqlParam = cmd.Parameters.Add("@Duration", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Duration

            sqlParam = cmd.Parameters.Add("@AsNeeded", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input

            Dim chk As Integer
            If AsNeeded = CheckState.Checked Then
                chk = 1
            Else
                chk = 0
            End If
            sqlParam.Value = chk


            'PER NO: 2140 - 20 jun 2009 - Saagar K
            sqlParam = cmd.Parameters.Add("@sRefills", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Refills
            'PER NO: 2140 - 20 jun 2009 - Saagar K

            ''''Added Amount as per pravin sir discussion
            sqlParam = cmd.Parameters.Add("@sAmount", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = DispenseAmt

            sqlParam = cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = GetPrefixTransactionID()

            sqlParam = cmd.Parameters.Add("@SIGID", SqlDbType.BigInt)
            'sarika 15th oct 07
            'sqlParam.Direction = ParameterDirection.Input
            sqlParam.Direction = ParameterDirection.InputOutput
            '---------------------------------------
            sqlParam.Value = ID

            Con.Open()
            cmd.ExecuteNonQuery()

            'sarika 15th oct 07
            _ReturnValue = sqlParam.Value
            '--------------------
            'Return objBusLayer.PassCmdGetDV(cmd)


            If ID <> 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Modify, "SIG Modified", gloAuditTrail.ActivityOutCome.Success)
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "SIG Modified", gstrLoginName, gstrClientMachineName)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Add, "SIG Added", gloAuditTrail.ActivityOutCome.Success)
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "SIG Added", gstrLoginName, gstrClientMachineName)

            End If

            Return _ReturnValue
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            sqlParam = Nothing
        End Try
    End Function

    Public Function DeleteSIG(ByVal ID As Long, ByVal Dosage As String)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter
        Try
            cmd = New SqlCommand("gsp_DeleteSIG_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@id", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            Con.Open()
            cmd.ExecuteNonQuery()
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Delete, "SIG Deleted", gloAuditTrail.ActivityOutCome.Success)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "SIG Deleted", gstrLoginName, gstrClientMachineName)
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.Delete, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return strsplittext
        End Try
    End Function
End Class
