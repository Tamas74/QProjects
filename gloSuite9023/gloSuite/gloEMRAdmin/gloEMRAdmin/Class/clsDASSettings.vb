Imports System.Data.SqlClient

Public Class clsDASSettings
    Implements IDisposable

    Public Function GetDASTest() As DataTable
        Dim dt As DataTable
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            Dim objCmd As New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "GetDASTest"
            objCmd.Connection = objCon
            objCon.Open()

            Dim objDA As New SqlDataAdapter(objCmd)
            dt = New DataTable
            objDA.Fill(dt)

            objCon.Close()
            objCon = Nothing

            If Not IsNothing(dt) Then
                Return dt
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    Public Function GetLabTest() As DataTable
        Dim dt As DataTable
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            Dim objCmd As New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "GetLabTestForDAS"
            objCmd.Connection = objCon
            objCon.Open()

            Dim objDA As New SqlDataAdapter(objCmd)
            dt = New DataTable
            objDA.Fill(dt)

            objCon.Close()
            objCon = Nothing

            If Not IsNothing(dt) Then
                Return dt
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
        End Try
    End Function

    Public Function GetDASTestResult(ByVal TestId As Int64) As DataSet
        Dim ds As DataSet
        Dim objCon As New SqlConnection
        Try

            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            Dim objCmd As New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "GetDASTestresult"

            Dim oparam As SqlParameter
            oparam = New SqlParameter("@TestID", SqlDbType.BigInt)
            oparam.Value = TestId
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()

            Dim objDA As New SqlDataAdapter(objCmd)
            ds = New DataSet
            objDA.Fill(ds, "DASTest")
            ds.Tables(1).TableName = "DASTestResult"

            ds.Tables("DASTest").Columns("TestID").AutoIncrement = True
            ds.Tables("DASTest").Columns("TestID").AutoIncrementSeed = 1


            ds.Tables("DASTestResult").Columns("TestResultsID").AutoIncrement = True
            ds.Tables("DASTestResult").Columns("TestResultsID").AutoIncrementSeed = 1

            ds.Relations.Add("DASTestDASTestResult", ds.Tables("DASTest").Columns("TestID"), ds.Tables("DASTestResult").Columns("TestID"))

            ds.Tables("DASTest").Columns("IsESR").DefaultValue = True
            ds.Tables("DASTest").Columns("IsCRP").DefaultValue = False

            objCon.Close()
            objCon = Nothing

            If Not IsNothing(ds) Then
                Return ds
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(ds) Then
                ds.Dispose()
                ds = Nothing
            End If
        End Try
    End Function


    Public Function SaveDataset(ByVal dsMain As DataSet, ByVal TableName As String, ByVal StoredProcedure As String, ByVal ParameterName As String) As Integer

        Dim connMain As New SqlConnection(gloEMRAdmin.mdlGeneral.GetConnectionString())
        Dim cmdMain As New SqlCommand(StoredProcedure, connMain)
        Dim tvpParam As SqlParameter
        Dim tvpParam1 As SqlParameter

        Try
            cmdMain.CommandType = CommandType.StoredProcedure
            'cmdMain.Parameters.AddWithValue(ParameterName, dsMain.Tables(TableName))
            tvpParam = cmdMain.Parameters.AddWithValue(ParameterName, dsMain.Tables(TableName))
            tvpParam.SqlDbType = SqlDbType.Structured
           

            tvpParam1 = cmdMain.Parameters.AddWithValue("@DASTest_Mst", dsMain.Tables("DASTest"))
            tvpParam1.SqlDbType = SqlDbType.Structured

            connMain.Open()
            Return cmdMain.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            cmdMain.Dispose()
            cmdMain = Nothing

            connMain.Close()
            connMain.Dispose()
            connMain = Nothing

        End Try

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
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


    Public Function DeleteDASTest(ByVal TestId As Int64) As Boolean
        Dim objCon As New SqlConnection
        Try

            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
            Dim objCmd As New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "DeleteDASTest"

            Dim oparam As SqlParameter
            oparam = New SqlParameter("@TestID", SqlDbType.BigInt)
            oparam.Value = TestId
            objCmd.Parameters.Add(oparam)
            oparam = Nothing

            objCmd.Connection = objCon
            objCon.Open()

            objCmd.ExecuteNonQuery()

            objCon.Close()
            objCon = Nothing

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
        Return True
    End Function




#End Region

End Class
