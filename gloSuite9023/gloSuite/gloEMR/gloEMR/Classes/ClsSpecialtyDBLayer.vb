Imports System.Data
Imports System.Data.SqlClient
Public Class ClsSpecialtyDBLayer
    Public Sub New()
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)

    End Sub
    Public Sub Dispose()

        If Conn IsNot Nothing Then
            Conn.Dispose()
            Conn = Nothing

        End If
        If IsNothing(Dv) = False Then
            Dv.Dispose()
            Dv = Nothing
        End If
        If IsNothing(Ds) = False Then
            Ds.Dispose()
            Ds = Nothing
        End If
    End Sub
    Private Conn As SqlConnection = Nothing
    ' Private Adapter As System.Data.SqlClient.SqlDataAdapter
    'Private sqlreader As System.Data.SqlClient.SqlDataReader
    'Private sqlcmmd As System.Data.SqlClient.SqlCommand
    Private Ds As System.Data.DataSet
    Private Dv As DataView = Nothing
    ' Private Tb As DataTable
    ' Private Cmd As System.Data.SqlClient.SqlCommand
    Public Sub SetRowFilter(ByVal ColIndex As Integer, ByVal txtSearch As String)
        Dim strexpr As String
        strexpr = "" & Dv.Table.Columns(ColIndex).ColumnName() & "  Like '" & txtSearch & "%'"
        Dv.RowFilter = strexpr
    End Sub
    Public Function FetchData()
        Dim Cmd As SqlCommand = Nothing
        Try
            Dim Adapter As SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
            Cmd = New SqlCommand("gsp_ViewSpecialty_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure

            Adapter.SelectCommand = Cmd
            If IsNothing(Ds) = False Then
                Ds.Dispose()
                Ds = Nothing
            End If
            Ds = New DataSet
            Adapter.Fill(Ds)
            Dim Tb As DataTable = Ds.Tables(0)
            If IsNothing(Dv) = False Then
                Dv.Dispose()
                Dv = Nothing
            End If
            Dv = New DataView(Tb.Copy())
            Adapter.Dispose()
            Adapter = Nothing
            Tb.Dispose()
            Tb = Nothing
            Conn.Close()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
        Return Nothing
        'Return Ds
        'Return Ds
    End Function
    Public Sub AddData(ByVal str1 As String)
        Dim Cmd As SqlCommand = Nothing
        Try
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_InUpSpecialty_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@nSpecialtyId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = 0

            objParam = Cmd.Parameters.Add("@sDescription", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str1

            objParam = Cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = GetPrefixTransactionID()

            Conn.Open()
            Cmd.ExecuteNonQuery()
            objParam = Nothing
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Speciality, gloAuditTrail.ActivityType.Add, "Specialty Added", gloAuditTrail.ActivityOutCome.Success)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Specialty Added", gstrLoginName, gstrClientMachineName)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Sub
    Public Sub UpdateData(ByVal str1 As String, ByVal SpecialtyId As Long)
        Dim Cmd As SqlCommand = Nothing
        Try
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_InUpSpecialty_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter
            objParam = Cmd.Parameters.Add("@nSpecialtyId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = SpecialtyId

            objParam = Cmd.Parameters.Add("@sDescription", SqlDbType.VarChar, 50)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str1

            objParam = Cmd.Parameters.Add("@MachineID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = GetPrefixTransactionID()

            Conn.Open()
            Cmd.ExecuteNonQuery()
            objParam = Nothing
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Speciality, gloAuditTrail.ActivityType.Modify, "Specialty Modified", gloAuditTrail.ActivityOutCome.Success)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Modify, "Specialty Modified", gstrLoginName, gstrClientMachineName)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Sub
    Public Sub DeleteData(ByVal Specialtyid As Long)
        Dim Cmd As SqlCommand = Nothing
        Try
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_DeleteSpecialty_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter
            objParam = Cmd.Parameters.Add("@nSpecialtyId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = Specialtyid
            Conn.Open()
            Cmd.ExecuteNonQuery()
            objParam = Nothing
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Patient, gloAuditTrail.ActivityCategory.Speciality, gloAuditTrail.ActivityType.Delete, "Specialty Deleted", gloAuditTrail.ActivityOutCome.Success)
            'gloAuditTrail.gloAuditTrail.CreateLog(gloAuditTrail.enmActivityType.Add, "Specialty Deleted", gstrLoginName, gstrClientMachineName)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
    End Sub
    Public Function FetchDataForUpdate(ByVal id As Long) As String
        Dim Cmd As SqlCommand = Nothing
        Try
            ' Dim arrlist As New ArrayList
            Dim str1 As String = Nothing
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_scanSpecialty_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@nSpecialtyId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = id

            Dim dreader As SqlDataReader
            Conn.Open()
            dreader = Cmd.ExecuteReader
            Do While dreader.Read()
                str1 = dreader(0)
            Loop
            dreader.Close()
            dreader = Nothing
            Conn.Close()
            objParam = Nothing
            Return str1

        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
        End Try
        'Return Ds
        'Return Ds
    End Function
    Public Function ValidateDescription(ByVal str1 As String, ByVal id As Int64) As Boolean
        Dim Cmd As SqlCommand = Nothing
        Dim objParam As SqlParameter
        Try
            Cmd = New System.Data.SqlClient.SqlCommand("gsp_checkSpecialty_Mst", Conn)
            Cmd.CommandType = CommandType.StoredProcedure


            objParam = Cmd.Parameters.Add("@sDescription", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = str1

            objParam = Cmd.Parameters.Add("@nspecialtyid", SqlDbType.BigInt)
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
                    dreader.Close()
                    Conn.Close()
                    Return False
                Else
                    dreader.Close()
                    Conn.Close()
                    Return True
                End If
            Loop
            Return False
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then
                Conn.Close()
            End If
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
        End Try

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
End Class

