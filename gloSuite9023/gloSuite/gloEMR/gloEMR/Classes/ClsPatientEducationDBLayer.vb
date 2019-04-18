Imports System.Data
Imports System.Data.SqlClient
Public Class ClsPatientEducationDBLayer
    Implements IDisposable

    Public Sub New()
        Dim sqlconn As String
        sqlconn = GetConnectionString()
        Conn = New System.Data.SqlClient.SqlConnection(sqlconn)
    End Sub
    Private Conn As SqlConnection
    '  Private Adapter As System.Data.SqlClient.SqlDataAdapter
    'Private sqlreader As System.Data.SqlClient.SqlDataReader
    'Private sqlcmmd As System.Data.SqlClient.SqlCommand
    Private Ds As System.Data.DataSet
    Private Dv As DataView
    ' Private Tb As DataTable
    ' Private Cmd As System.Data.SqlClient.SqlCommand
    Public Sub FetchData()
        Dim Adapter As SqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
        Dim Cmd As SqlCommand = New SqlCommand("gsp_ViewPatientEducation_Mst", Conn)
        Cmd.CommandType = CommandType.StoredProcedure

        Adapter.SelectCommand = Cmd
        If (IsNothing(Ds) = False) Then
            Ds.Dispose()
            Ds = Nothing
        End If
        Ds = New DataSet
        Adapter.Fill(Ds)
        Dim Tb As DataTable = Ds.Tables(0).Copy()
        If (IsNothing(Dv) = False) Then
            Dv.Dispose()
            Dv = Nothing
        End If
        Dv = New DataView(Tb.Copy())
        Conn.Close()
        Tb.Dispose()
        Tb = Nothing
        Adapter.Dispose()
        Adapter = Nothing
        Cmd.Parameters.Clear()
        Cmd.Dispose()
        Cmd = Nothing
        'Return Ds
        'Return Ds
    End Sub
    Public Sub AddData(ByVal Arrlist As ArrayList)
        Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_InUpPatientEducation_Mst", Conn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim objParam As SqlParameter
        objParam = Cmd.Parameters.Add("@nPatientEducationId", SqlDbType.BigInt)
        objParam.Direction = ParameterDirection.Input
        objParam.Value = 0

        objParam = Cmd.Parameters.Add("@sTopicName", SqlDbType.VarChar, 50)
        objParam.Direction = ParameterDirection.Input
        objParam.Value = CType(Arrlist.Item(1), System.String)

        objParam = Cmd.Parameters.Add("@sMaterialFormat", SqlDbType.VarChar, 50)
        objParam.Direction = ParameterDirection.Input
        objParam.Value = CType(Arrlist.Item(2), System.String)

        objParam = Cmd.Parameters.Add("@sDocPath", SqlDbType.VarChar, 25)
        objParam.Direction = ParameterDirection.Input
        objParam.Value = CType(Arrlist.Item(3), System.String)

        objParam = Cmd.Parameters.Add("@sDescription", SqlDbType.NText)
        objParam.Direction = ParameterDirection.Input
        objParam.Value = CType(Arrlist.Item(4), System.String)

        Conn.Open()
        Cmd.ExecuteNonQuery()
        Conn.Close()
        objParam = Nothing
        Cmd.Parameters.Clear()
        Cmd.Dispose()
        Cmd = Nothing

    End Sub
    Public Sub UpdateData(ByVal Arrlist As ArrayList)
        Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_InUpPatientEducation_Mst", Conn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim objParam As SqlParameter
        objParam = Cmd.Parameters.Add("@nPatientEducationId", SqlDbType.BigInt)
        objParam.Direction = ParameterDirection.Input
        objParam.Value = Arrlist.Item(0)

        objParam = Cmd.Parameters.Add("@sTopicName", SqlDbType.VarChar, 50)
        objParam.Direction = ParameterDirection.Input
        objParam.Value = Arrlist.Item(1)

        objParam = Cmd.Parameters.Add("@sMaterialFormat", SqlDbType.VarChar, 50)
        objParam.Direction = ParameterDirection.Input
        objParam.Value = Arrlist.Item(2)

        objParam = Cmd.Parameters.Add("@sDocPath", SqlDbType.VarChar, 25)
        objParam.Direction = ParameterDirection.Input
        objParam.Value = Arrlist.Item(3)

        objParam = Cmd.Parameters.Add("@sDescription", SqlDbType.NText)
        objParam.Direction = ParameterDirection.Input
        objParam.Value = Arrlist.Item(4)

        Conn.Open()
        Cmd.ExecuteNonQuery()
        Conn.Close()
        objParam = Nothing
        Cmd.Parameters.Clear()
        Cmd.Dispose()
        Cmd = Nothing

    End Sub
    Public Sub DeleteData(ByVal PatientEducationid As Long)
        Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_DeletePatientEducation_Mst", Conn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim objParam As SqlParameter
        objParam = Cmd.Parameters.Add("@nPatientEducationId", SqlDbType.BigInt)
        objParam.Direction = ParameterDirection.Input
        objParam.Value = PatientEducationid
        Conn.Open()
        Cmd.ExecuteNonQuery()
        Conn.Close()
        objParam = Nothing
        Cmd.Parameters.Clear()
        Cmd.Dispose()
        Cmd = Nothing

    End Sub
    Public Function FetchDataForUpdate(ByVal PatientEducationid As Long) As ArrayList
        Dim arrlist As New ArrayList
        Dim Cmd As SqlCommand = New System.Data.SqlClient.SqlCommand("gsp_scanPatientEducation_Mst", Conn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim objParam As SqlParameter

        objParam = Cmd.Parameters.Add("@nPatientEducationid", SqlDbType.BigInt)
        objParam.Direction = ParameterDirection.Input
        objParam.Value = PatientEducationid

        Dim dreader As SqlDataReader
        Conn.Open()
        dreader = Cmd.ExecuteReader
        Do While dreader.Read()

            arrlist.Add(dreader.Item(0))
            arrlist.Add(dreader.Item(1))
            arrlist.Add(dreader.Item(2))
            arrlist.Add(dreader.Item(3))
        Loop
        dreader.Close()
        Conn.Close()
        objParam = Nothing
        Cmd.Parameters.Clear()
        Cmd.Dispose()
        Cmd = Nothing

        Return arrlist

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
    Public Sub SortDataview(ByVal strsort As String)
        Dv.Sort = strsort
    End Sub

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
