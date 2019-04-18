Imports System.Data.SqlClient


Public Class clsAddendum

    'Private da As SqlDataAdapter
    'Private ds As New DataSet
    'Private dt As DataTable
    Private dv As DataView
    Private Con As SqlConnection
    Private conString As String

    Public Sub New()
        Try
            Dim conString As String
            conString = GetConnectionString()
            Con = New SqlConnection(conString)
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsAddendum -- New() -- " & ex.ToString)
            Con.Close()
        Catch ex As Exception   ' Catch the error.
            MessageBox.Show(ex.ToString, "Addendum", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    Public Sub Dispose()

        ''slr free dv
        If Not IsNothing(dv) Then
            dv.Dispose()
            dv = Nothing
        End If
        'If Not IsNothing(ds) Then
        '    ds.Dispose()
        '    ds = Nothing
        'End If

        'slr free Con
        If Not IsNothing(Con) Then
            Con.Dispose()
            Con = Nothing
        End If

    End Sub
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
            dv.RowFilter = "" & dv.Table.Columns(colIndex).ColumnName & " Like '%" & txtSearch & "%'"
        End If

        'grdCPT.DataSource = dv
        Return dv
    End Function
    'Function is commented by dipak as not in use
    'Public Function SelectAddendum(ByVal VisitID As Long, ByVal ExamID As Long) As DataTable
    '    Try

    '        Dim cmd As New SqlCommand("gsp_ScanAddendum", Con)
    '        cmd.CommandType = CommandType.StoredProcedure
    '        Dim sqlParam As SqlParameter

    '        sqlParam = cmd.Parameters.Add("@PatientID", gnPatientID)
    '        sqlParam.Direction = ParameterDirection.Input

    '        sqlParam = cmd.Parameters.Add("@VisitID", VisitID)
    '        sqlParam.Direction = ParameterDirection.Input

    '        sqlParam = cmd.Parameters.Add("@ExamID", ExamID)
    '        sqlParam.Direction = ParameterDirection.Input

    '        Con.Open()
    '        'cmd.ExecuteNonQuery()
    '        ds.Clear()
    '        da = New SqlDataAdapter
    '        da.SelectCommand = cmd
    '        dt = New DataTable
    '        da.Fill(dt)
    '        Return dt

    '    Catch ex As SqlException
    '        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        UpdateLog("clsAddendum -- SelectAddendum -- " & ex.ToString)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, "Addendum", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        Con.Close()
    '    End Try
    'End Function

    'Public Function AddNewAddendum(ByVal AddendumID As Long, ByVal VisitID As Long, ByVal ExamID As Long, ByVal AddendumNotes As String)
    '    Try
    '        Dim cmd As New SqlCommand("gsp_InUpAddendum", Con)
    '        cmd.CommandType = CommandType.StoredProcedure
    '        Dim sqlParam As SqlParameter

    '        sqlParam = cmd.Parameters.Add("@AddendumID", SqlDbType.Int)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = AddendumID

    '        sqlParam = cmd.Parameters.Add("@PatientID", gnPatientID)
    '        sqlParam.Direction = ParameterDirection.Input

    '        sqlParam = cmd.Parameters.Add("@VisitID", VisitID)
    '        sqlParam.Direction = ParameterDirection.Input

    '        sqlParam = cmd.Parameters.Add("@ExamID", ExamID)
    '        sqlParam.Direction = ParameterDirection.Input

    '        sqlParam = cmd.Parameters.Add("@AddendumNotes", SqlDbType.NText)
    '        sqlParam.Direction = ParameterDirection.Input
    '        sqlParam.Value = AddendumNotes

    '        Con.Open()
    '        cmd.ExecuteNonQuery()

    '        '' Dim objAudit As New clsAudit
    '        ''If AddendumID <> 0 Then
    '        ''    ' objAudit.CreateLog(clsAudit.enmActivityType.Modify, Description & " Lab Modified", 1, 0)
    '        ''Else
    '        ''    ' objAudit.CreateLog(clsAudit.enmActivityType.Add, Description & " Lab Added", 1, 0)
    '        ''End If
    '        ''objAudit = Nothing
    '    Catch ex As SqlException
    '        MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        UpdateLog("clsAddendum -- AddNewAddendum -- " & ex.ToString)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.ToString, "Addendum", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        Con.Close()
    '    End Try
    'End Function

End Class
