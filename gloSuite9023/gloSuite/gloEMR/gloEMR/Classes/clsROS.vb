Imports System.Data
Imports System.Data.SqlClient

Public Class clsROS

    Public Sub New()
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
    End Sub
    Private Conn As SqlConnection
    'Private Adapter As System.Data.SqlClient.SqlDataAdapter
    'Private sqlreader As System.Data.SqlClient.SqlDataReader
    'Private sqlcmmd As System.Data.SqlClient.SqlCommand
    Private Ds As System.Data.DataSet = Nothing
    Private Dv As DataView = Nothing
    '  Private Tb As DataTable
    '  Private Cmd As System.Data.SqlClient.SqlCommand
    Public Sub Dispose()

        If Conn IsNot Nothing Then
            Conn.Dispose()
            Conn = Nothing

        End If
        If IsNothing(dv) = False Then
            dv.Dispose()
            dv = Nothing
        End If
        If IsNothing(Ds) = False Then
            Ds.Dispose()
            Ds = Nothing
        End If
    End Sub
    Public Sub SetRowFilter(ByVal ColIndex As Integer, ByVal txtSearch As String)
        Dim strexpr As String
        strexpr = "" & Dv.Table.Columns(ColIndex).ColumnName() & "  Like '" & txtSearch & "%'"
        Dv.RowFilter = strexpr
    End Sub
    Public Function FetchData(ByVal CategoryId As Long)
        Try
            Dim Adapter As SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
            Dim Cmd As SqlCommand = New SqlCommand("gsp_ViewROS_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter
            objParam = Cmd.Parameters.Add("@nCategoryId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CategoryId

            Adapter.SelectCommand = Cmd
            If (IsNothing(Ds) = False) Then
                Ds.Dispose()
                Ds = Nothing
            End If
            Ds = New DataSet
            Adapter.Fill(Ds)
            Dim Tb As DataTable = Ds.Tables(0)
            If (IsNothing(Dv) = False) Then
                Dv.Dispose()
                Dv = Nothing
            End If
            Dv = New DataView(Tb.Copy())
            Adapter.Dispose()
            Adapter = Nothing
            Tb.Dispose()
            Tb = Nothing
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
            Conn.Close()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        'Return Ds
        'Return Ds
        Return Nothing
    End Function

    Public Function AddData(ByVal str1 As String, ByVal str2 As String, ByVal CategoryID As Long) As Long
        Try
            'Dim i As Int16
            'For i = 1 To 100
            'str1 = "Social ROS " & i
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_InUpROS_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter
            Dim returningValue As Long
            ''objParam = Cmd.Parameters.Add("@nROSId", SqlDbType.bigint)
            '''''objParam.Direction = ParameterDirection.Input
            ''objParam.Direction = ParameterDirection.InputOutput
            ''objParam.Value = 0

            objParam = Cmd.Parameters.Add("@sDescription", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str1

            objParam = Cmd.Parameters.Add("@sComments", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str2

            objParam = Cmd.Parameters.Add("@nCategoryId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CategoryID

            objParam = Cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = GetPrefixTransactionID()

            objParam = Cmd.Parameters.Add("@nROSId", SqlDbType.BigInt)
            ''objParam.Direction = ParameterDirection.Input
            objParam.Direction = ParameterDirection.InputOutput
            objParam.Value = 0

            Conn.Open()
            Cmd.ExecuteNonQuery()

            returningValue = Convert.ToInt64(objParam.Value)

            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing

            Return returningValue

            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Add, str1 & " ROS Added", gstrLoginName, gstrClientMachineName)
            'objAudit = Nothing


        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Add, "Ros added", gloAuditTrail.ActivityOutCome.Success)


            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Add, "Ros added", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Add, "Ros added", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''

            Conn.Close()
        End Try
        'Next
    End Function

    Public Sub UpdateData(ByVal str1 As String, ByVal str2 As String, ByVal ROSId As Long)
        Try
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_InUpROS_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter
            ''objParam = Cmd.Parameters.Add("@nROSId", SqlDbType.bigInt)
            ''objParam.Direction = ParameterDirection.Input
            ''objParam.Value = ROSId

            objParam = Cmd.Parameters.Add("@sDescription", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str1

            objParam = Cmd.Parameters.Add("@sComments", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str2

            objParam = Cmd.Parameters.Add("@nROSId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ROSId

            objParam = Cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = GetPrefixTransactionID()

            Conn.Open()
            Cmd.ExecuteNonQuery()

            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
            Conn.Close()

            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Modify, "ROS Modified", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Modify, "Ros Modified", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "ROS Modified", gstrLoginName, gstrClientMachineName)

        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub DeleteData(ByVal ROSid As Long)
        Try
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_DeleteROS_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter
            objParam = Cmd.Parameters.Add("@nROSId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ROSid
            Conn.Open()
            Cmd.ExecuteNonQuery()
            objParam = Nothing
            'Dim objAudit As New clsAudit
            'objAudit.CreateLog(clsAudit.enmActivityType.Delete, " ROS Deleted", gstrLoginName, gstrClientMachineName)
            'objAudit = Nothing
            Conn.Close()

            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            ''gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Delete, "Ros Deleted", gloAuditTrail.ActivityOutCome.Success)
            ''Added Rahul P on 20101011
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.ROS, gloAuditTrail.ActivityCategory.Review, gloAuditTrail.ActivityType.Delete, "Ros Deleted", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR)
            ''
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Delete, "Ros Deleted", gstrLoginName, gstrClientMachineName)
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Function FetchDataForUpdate(ByVal id As Long) As ArrayList
        Try
            Dim arrlist As New ArrayList
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_scanROS_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@nROSId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = id

            Dim dreader As SqlDataReader
            Conn.Open()
            dreader = Cmd.ExecuteReader
            Do While dreader.Read()

                arrlist.Add(dreader.Item(0))
                arrlist.Add(dreader.Item(1))

            Loop
            Conn.Close()
            dreader.Close()
            dreader = Nothing
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
            Return arrlist
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

        'Return Ds
        'Return Ds
    End Function
    Public ReadOnly Property DsDataSet() As DataSet
        Get
            'Dv = Ds.Tables("Category_Mst").DefaultView
            Return Ds
            'Return Ds
        End Get
    End Property
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

    Public Function FillControls() As DataTable
        Try
            Dim adpt As New SqlDataAdapter
            Dim ds As New DataSet

            Dim Cmd As SqlCommand = New SqlCommand("gsp_FillCategory_Mst", Conn)
            Dim objParam As SqlParameter

            Cmd.CommandType = CommandType.StoredProcedure
            adpt.SelectCommand = Cmd

            objParam = Cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = "ROS"

            objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 1

            adpt.Fill(ds)
            Conn.Close()

            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
            Dim myTable As DataTable = ds.Tables(0).Copy()
            adpt.Dispose()
            adpt = Nothing
            ds.Dispose()
            ds = Nothing
            Return myTable
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try

        'Dim dreader As SqlDataReader
        'Conn.Open()
        'dreader = Cmd.ExecuteReader()

        'Do While dreader.Read
        '    Dim i As Integer
        '    i = dreader("nSpecialtyID")

        'Loop
    End Function
    Public Function ValidateDescription(ByVal categoryid As System.Int64, ByVal str1 As String, ByVal id As Int64) As Boolean
        Try
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_checkROS_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@ncategoryId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = categoryid

            objParam = Cmd.Parameters.Add("@sDescription", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str1

            objParam = Cmd.Parameters.Add("@nROSId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = id

            Dim dreader As SqlDataReader
            If Conn.State <> ConnectionState.Open Then
                Conn.Open()
            End If

            dreader = Cmd.ExecuteReader
            Dim i As Int64
            Do While dreader.Read
                i = CType(dreader.Item(0), System.Int64)
                If i > 0 Then
                    Conn.Close()
                    dreader.Close()
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing
                    dreader = Nothing
                    Return False
                Else
                    Conn.Close()
                    dreader.Close()
                    Cmd.Parameters.Clear()
                    Cmd.Dispose()
                    Cmd = Nothing
                    dreader = Nothing
                    Return True
                End If
            Loop
            dreader.Close()
            dreader = Nothing
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
            Return False
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

    End Function
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

    Public Function GetCategoryID(ByVal CategoryName As String, ByVal Flag As String) As Long
        Try

            Dim Cmd As SqlCommand = New SqlCommand("gsp_GetCategoryID", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CategoryName

            objParam = Cmd.Parameters.Add("@Flag", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Flag


            objParam = Cmd.Parameters.Add("@nCategoryID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Output

            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            Cmd.ExecuteNonQuery()
            Dim nCategoryID As Long
            nCategoryID = objParam.Value
            objParam = Nothing
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If

           
            Return nCategoryID


        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Conn.Close()
        End Try
        'Return Ds
        'Return Ds
    End Function
End Class



