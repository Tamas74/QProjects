Imports System.Data.SqlClient
Imports System.Windows.Forms


Public Class clsModifiers
    Private da As SqlDataAdapter
    Private ds As New DataSet
    Private dt As DataTable
    Private dv As DataView
    Private Con As SqlConnection
    Private conString As String

    Public Sub New()
        Try
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
            gstrMessageBoxCaption = GetMessageBoxCaption()
        Catch ex As SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        Catch ex As Exception   ' Catch the error.
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Con.Close()
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Sub

    Public ReadOnly Property GetDataSet() As DataSet
        Get
            'Dv = Ds.Tables("Category_Mst").DefaultView
            Return ds
            'Return Ds
        End Get
    End Property

    Public ReadOnly Property GetDataview() As DataView
        Get
            'Dv = Ds.Tables("Category_Mst").DefaultView
            Return dv
            'Return Ds
        End Get
    End Property

    Public Sub Search(ByVal dv As DataView, ByVal colIndex As Integer, ByVal txtSearch As String)
        'Dim dv As DataView
        'dv = grdCPT.DataSource
        If (IsNothing(dv) = False) Then


            dv.RowFilter = "" & dv.Table.Columns(colIndex).ColumnName & " Like '" & txtSearch & "%'"
        End If

        'grdCPT.DataSource = dv
    End Sub

    Public Sub SortDataview(ByVal strsort As String, Optional ByVal strSortOrder As String = "")
        If (IsNothing(dv) = False) Then
            'DCatview.Sort = strsort
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
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
        End Try
    End Function
    Public Function GetAllModifiers(Optional ByVal Flag As Int16 = 0) As DataView
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gsp_ViewModifier_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@Flag", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Flag

            Con.Open()
            'cmd.ExecuteNonQuery()
            ds.Clear()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            dv = New DataView(dt)
            Return dv

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return Nothing
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
            Con.Close()
        End Try
    End Function

    '' SUDHIR 20090711 ''
    Public Function GetAllModifier() As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Try
            Dim dtModifier As DataTable = Nothing
            Dim _Query As String = "SELECT nModifierID, sModifierCode, sDescription FROM Modifier_MST"
            oDB.Connect(False)
            oDB.Retrive_Query(_Query, dtModifier)
            If dtModifier IsNot Nothing Then
                Return dtModifier
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

    Public Function CheckDuplicate(ByVal ID As Int64, ByVal Code As String) As Boolean
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            'objBusLayer.Open_Con()
            cmd = New SqlCommand("gsp_CheckModifiers_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure


            sqlParam = cmd.Parameters.Add("@ID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            sqlParam = cmd.Parameters.Add("@Code", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Code

            Con.Open()

            Dim rowAffected As Int64
            rowAffected = CType(cmd.ExecuteScalar, Int64)
            If rowAffected > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
            'objBusLayer = Nothing
            Con.Close()
        End Try
    End Function

    Public Sub SelectModifiers(ByVal ID As Long)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gsp_ScanModifier_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            sqlParam = cmd.Parameters.Add("@ModifierID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            Con.Open()
            'cmd.ExecuteNonQuery()
            ds.Clear()
            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            dv = New DataView(dt)

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
            Con.Close()
        End Try
    End Sub

    Public Sub AddNewModifier(ByVal ID As Int64, ByVal Code As String, ByVal Description As String)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gsp_InUpModifier_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure

            sqlParam = cmd.Parameters.Add("@ModifierID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID

            sqlParam = cmd.Parameters.Add("@ModifierCode", SqlDbType.VarChar, 50)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Code

            sqlParam = cmd.Parameters.Add("@Description", SqlDbType.VarChar, 50)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = Description

            sqlParam = cmd.Parameters.Add("@ClinicID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = gnClinicID

            Con.Open()
            cmd.ExecuteNonQuery()
            'Return objBusLayer.PassCmdGetDV(cmd)


            If ID <> 0 Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Modifier, gloAuditTrail.ActivityType.Modify, "Modifier Modified", gloAuditTrail.ActivityOutCome.Success)
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Modifier Modified", gstrLoginName, gstrClientMachineName)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Modifier, gloAuditTrail.ActivityType.Add, "Modifier Added", gloAuditTrail.ActivityOutCome.Success)
                'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Modifier Added", gstrLoginName, gstrClientMachineName)
            End If

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
            Con.Close()
        End Try
    End Sub

    Public Sub DeleteModifier(ByVal ID As Long, ByVal Code As String)
        Dim cmd As SqlCommand = Nothing
        Dim sqlParam As SqlParameter = Nothing
        Try
            cmd = New SqlCommand("gsp_DeleteModifier_MST", Con)
            cmd.CommandType = CommandType.StoredProcedure
            sqlParam = cmd.Parameters.Add("@ModifierID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ID
            Con.Open()
            cmd.ExecuteNonQuery()

            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Delete, Code & " Modifier Deleted", gstrLoginName, gstrClientMachineName)
            'objAudit = Nothing
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Modifier, gloAuditTrail.ActivityType.Delete, "Modifier Deleted", gloAuditTrail.ActivityOutCome.Success)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Modifier Deleted", gstrLoginName, gstrClientMachineName)
        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(sqlParam) Then
                sqlParam = Nothing
            End If
            Con.Close()
        End Try
    End Sub

End Class
