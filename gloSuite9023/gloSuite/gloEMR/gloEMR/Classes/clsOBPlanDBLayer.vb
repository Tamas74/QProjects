Imports System.Data
Imports System.Data.SqlClient

Public Class clsOBPlanDBLayer
    Implements IDisposable

    Public Sub New()
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
    End Sub
    Private Conn As SqlConnection
    ' Private Adapter As System.Data.SqlClient.SqlDataAdapter
    'Private sqlreader As System.Data.SqlClient.SqlDataReader
    'Private sqlcmmd As System.Data.SqlClient.SqlCommand
    Private Ds As System.Data.DataSet
    Private Dv As DataView
    ' Private Tb As DataTable
    ' Private Cmd As System.Data.SqlClient.SqlCommand

    Public Sub SetRowFilter(ByVal ColIndex As Integer, ByVal txtSearch As String)
        Dim strexpr As String
        strexpr = "" & Dv.Table.Columns(ColIndex).ColumnName() & "  Like '" & txtSearch & "%'"
        Dv.RowFilter = strexpr
    End Sub
    Public Sub FetchData(ByVal CategoryId As Long)
        Try
            Dim Adapter As SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
            Dim Cmd As SqlCommand = New SqlCommand("gsp_ViewOBPlan_MST", Conn)
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
            'Tb = Ds.Tables(0)
            If (IsNothing(Dv) = False) Then
                Dv.Dispose()
                Dv = Nothing
            End If
            Dv = New DataView(Ds.Tables(0))
            Adapter.Dispose()
            Adapter = Nothing
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing

            objParam = Nothing
        Catch ex As SqlException
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Conn.Close()
        End Try
        'Return Ds
        'Return Ds
    End Sub


    Public Function AddData(ByVal strDescription As String, ByVal strComments As String, ByVal CategoryID As Long) As Long

        Dim objParam As SqlParameter

        Try


            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_InUpOBPlan_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure



            objParam = Cmd.Parameters.Add("@sDescription", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = strDescription

            objParam = Cmd.Parameters.Add("@sComments", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = strComments

            objParam = Cmd.Parameters.Add("@nCategoryId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CategoryID


            objParam = Cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = GetPrefixTransactionID()

            objParam = Cmd.Parameters.Add("@nOBPlanID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.InputOutput
            objParam.Value = 0

            Conn.Open()
            Cmd.ExecuteNonQuery()
            Dim myObject = Cmd.Parameters("@nOBPlanID").Value
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            Return myObject

            'gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.enmActivityType.Add, " History Added", gstrLoginName, gstrClientMachineName)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, " History Added", gloAuditTrail.ActivityOutCome.Success)

            Conn.Close()
        Catch ex As SqlException
            Conn.Close()
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            objParam = Nothing
        End Try
        'Next
    End Function
    Public Sub UpdateData(ByVal strDescription As String, ByVal strComments As String, ByVal HistoryId As Long, ByVal CategoryID As Int64)
        Dim objParam As SqlParameter
        Try
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_InUpOBPlan_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure

            ''objParam = Cmd.Parameters.Add("@nHistoryId", SqlDbType.bigInt)
            ''objParam.Direction = ParameterDirection.Input
            ''objParam.Value = HistoryId

            objParam = Cmd.Parameters.Add("@sDescription", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = strDescription

            objParam = Cmd.Parameters.Add("@sComments", SqlDbType.VarChar, 255)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = strComments

            objParam = Cmd.Parameters.Add("@nOBPlanID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = HistoryId


            objParam = Cmd.Parameters.Add("@ncategoryId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = CategoryID

            Conn.Open()
            Cmd.ExecuteNonQuery()
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing

            Conn.Close()
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "History Modified", gstrLoginName, gstrClientMachineName)
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, "History Modified", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As SqlException
            Conn.Close()
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            objParam = Nothing
        End Try
    End Sub
    Public Sub DeleteData(ByVal Historyid As Long)
        Try
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_DeleteOBPlan_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter
            objParam = Cmd.Parameters.Add("@nOBPlanID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Historyid
            Conn.Open()
            Cmd.ExecuteNonQuery()
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing

            Conn.Close()

            objParam = Nothing
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Delete, "History Deleted", gloAuditTrail.ActivityOutCome.Success)
        Catch ex As SqlException
            Conn.Close()
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Delete, "History Delete " & ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function FetchDataForUpdate(ByVal id As Long, ByVal _CategoryID As Long) As ArrayList
        Try
            Dim arrlist As New ArrayList
            Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_ScanOBPlan_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@nOBPlanID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = id

            objParam = Cmd.Parameters.Add("@nCategoryID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = _CategoryID

            Dim dreader As SqlDataReader
            Conn.Open()
            dreader = Cmd.ExecuteReader
            Do While dreader.Read()

                arrlist.Add(dreader.Item(0))
                arrlist.Add(dreader.Item(1))
                arrlist.Add(dreader.Item(2))
                arrlist.Add(dreader.Item(3))
                arrlist.Add(dreader.Item(4))
                arrlist.Add(dreader.Item(5))
                arrlist.Add(dreader.Item(6))
                arrlist.Add(dreader.Item(7))
                arrlist.Add(dreader.Item(8))
                arrlist.Add(dreader.Item(9))
                arrlist.Add(dreader.Item(10))
                arrlist.Add(dreader.Item(11))
                arrlist.Add(dreader.Item(12))
            Loop
            dreader.Close()
            dreader = Nothing
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            Conn.Close()
            objParam = Nothing
            Return arrlist

        Catch ex As SqlException
            Conn.Close()
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            Conn.Close()
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
        Dim ds As DataSet = Nothing
        Try
            Dim adpt As New SqlDataAdapter


            Dim Cmd As SqlCommand = New SqlCommand("gsp_FillCategory_Mst", Conn)
            Dim objParam As SqlParameter

            Cmd.CommandType = CommandType.StoredProcedure
            adpt.SelectCommand = Cmd

            objParam = Cmd.Parameters.Add("@CategoryType", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = "OB Plan"

            objParam = Cmd.Parameters.Add("@flag", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 1
            ds = New DataSet
            adpt.Fill(ds)
            adpt.Dispose()
            adpt = Nothing

            Conn.Close()
            objParam = Nothing
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing
            Return ds.Tables(0).Copy()
        Catch ex As SqlException
            Conn.Close()
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If (IsNothing(ds) = False) Then
                ds.Dispose()
                ds = Nothing
            End If

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

        Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_CheckOBPlan_Mst", Conn)
        Try

            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@ncategoryId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = categoryid

            objParam = Cmd.Parameters.Add("@sDescription", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str1

            objParam = Cmd.Parameters.Add("@nOBPlanID", SqlDbType.BigInt)
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
                    Return False
                Else
                    Conn.Close()
                    dreader.Close()
                    Return True
                End If
            Loop

            objParam = Nothing
            Return Nothing
        Catch ex As SqlException
            Conn.Close()
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Cmd.Parameters.Clear()
            Cmd.Dispose()
            Cmd = Nothing

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
    Public Function GetStandardTypes() As DataTable
        Dim Cmd As SqlCommand = Nothing
        Dim adpt As New SqlDataAdapter
        Dim ds As New DataSet

        Try

            Cmd = New SqlCommand("History_GetStandardTypes", Conn)
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@FlgHistoryMst", SqlDbType.Bit)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 0

            Cmd.CommandType = CommandType.StoredProcedure
            adpt.SelectCommand = Cmd


            adpt.Fill(ds)
            Conn.Close()
            objParam = Nothing
            Return ds.Tables(0).Copy()
        Catch ex As SqlException
            Conn.Close()
            UpdateLog(ex.ToString)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            Conn.Close()
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(Cmd) = False Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            If IsNothing(adpt) = False Then
                adpt.Dispose()
                adpt = Nothing
            End If
            If IsNothing(ds) = False Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then

                If (IsNothing(Ds) = False) Then
                    Ds.Dispose()
                    Ds = Nothing
                End If

                If (IsNothing(Dv) = False) Then
                    Dv.Dispose()
                    Dv = Nothing
                End If
                If (IsNothing(Conn) = False) Then
                    Conn.Dispose()
                    Conn = Nothing
                End If

            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class



