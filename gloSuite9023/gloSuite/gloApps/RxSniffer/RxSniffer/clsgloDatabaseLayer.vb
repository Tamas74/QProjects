Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections
Imports System.Windows.Forms

Namespace gloDatabaseLayer
    '************ EXCEPTION CLASS ********************
    Public Class DBException
        Inherits System.ApplicationException

        Dim _no As Integer

        Dim _description As String

        Dim _sourceException As Exception

        'public DBException(int no, string description, Exception source)
        '{
        '    this.no = no; this.description = description; this.sourceException = source;
        '}
        Public Sub New(ByVal sErrorMessage As String, ByVal ActualError As String)
            MyBase.New(sErrorMessage)
            ERROR_Log(ActualError)
        End Sub

        Public Sub New(ByVal sErrorMessage As String, ByVal ActualError As String, ByVal inner As System.Exception)
            MyBase.New(sErrorMessage, inner)
            ERROR_Log(ActualError)
        End Sub

        Public Property SourceException() As Exception
            Get
                Return _sourceException
            End Get
            Set(ByVal value As Exception)
                _sourceException = value
            End Set
        End Property

        Public Property No() As Integer
            Get
                Return _no
            End Get
            Set(ByVal value As Integer)
                _no = value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property

        Public Sub ERROR_Log(ByVal strLogMessage As String)
            'Changes Done for Resolving Case no :GLO2010-0010138 i.e RxSniffer Dying -Start
            Dim objFile As System.IO.StreamWriter = Nothing
            Try
                objFile = New System.IO.StreamWriter((Application.StartupPath & "\\gloDb_ERRORLog.log"), True)
                objFile.WriteLine(System.DateTime.Now & ":" & System.DateTime.Now.Millisecond & "   " & strLogMessage & vbLf)
                objFile.Close()
                objFile = Nothing
            Catch ex As Exception
                'MessageBox.Show(ex.Message.ToString())
            Finally
                If Not IsNothing(objFile) Then
                    objFile.Close()
                    objFile = Nothing
                End If
            End Try
            'Changes Done for Resolving Case no :GLO2010-0010138 i.e RxSniffer Dying -End
            'MessageBox.Show("Error while accessing Database. Please click on Help to view log.", "gloFaxService", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, 0, (Application.StartupPath + "\\gloPMS_ERRORLog.log"))
        End Sub
    End Class

    '************ CONNECTION CLASS *******************
    Class DBConnection
       
        Private _conectionstring As String = ""

        Protected _connection As SqlConnection = Nothing

        Protected _transaction As SqlTransaction = Nothing

        Private disposed As Boolean = False

        Public Sub New(ByVal ConnectionString As String)
            MyBase.New()
            _connection = New SqlConnection
            _connection.ConnectionString = ConnectionString
        End Sub

        Private Sub New()
            MyBase.New()
            Dispose(False)
        End Sub

        Public Overloads Sub Dispose()
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposed Then
                If disposing Then

                End If
            End If
            disposed = True
        End Sub

        Public Sub OpenConnection()
            Try
                If (Not (_connection) Is Nothing) Then
                    If (_connection.State <> ConnectionState.Open) Then
                        _connection.Open()
                    End If
                Else
                    _connection = New SqlConnection
                    _connection.ConnectionString = _conectionstring
                    _connection.Open()
                End If
            Catch ex As SqlException
                Throw New DBException("Error in opening database connection", ex.ToString)
            Catch ex As Exception
                Throw New DBException("Error in opening database connection", ex.ToString)
            End Try
        End Sub

        Public Function GetConnection() As SqlConnection
            Return _connection
        End Function

        Public Function CloseConnection() As Boolean
            Try
                If (Not (_connection) Is Nothing) Then
                    If (_connection.State = ConnectionState.Open) Then
                        _connection.Close()
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Catch ex As SqlException
                Throw New DBException("Error in closing database connection", ex.ToString)
            Catch ex As Exception
                Throw New DBException("Error in closing database connection", ex.ToString)
            End Try
        End Function

        Public Function BeginTransaction() As Boolean
            Try
                If (Not (_connection) Is Nothing) Then
                    If (_connection.State = ConnectionState.Open) Then
                        _transaction = _connection.BeginTransaction
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Catch ex As SqlException
                Throw New DBException("Error in begin transaction", ex.ToString)
            Catch ex As Exception
                Throw New DBException("Error in begin transaction", ex.ToString)
            End Try
        End Function

        Public Function GetTransaction() As SqlTransaction
            Return _transaction
        End Function

        Public Function CommitTransaction() As Boolean
            Try
                If (Not (_transaction) Is Nothing) Then
                    _transaction.Commit()
                    Return True
                Else
                    Return False
                End If
            Catch ex As SqlException
                Throw New DBException("Error in commit transaction", ex.ToString)
            Catch ex As Exception
                Throw New DBException("Error in commit transaction", ex.ToString)
            End Try
        End Function

        Public Function RollbackTransaction() As Boolean
            Try
                If (Not (_transaction) Is Nothing) Then
                    _transaction.Rollback()
                    Return True
                Else
                    Return False
                End If
            Catch ex As SqlException
                Throw New DBException("Error in roll back transaction", ex.ToString)
            Catch ex As Exception
                Throw New DBException("Error in roll back transaction", ex.ToString)
            End Try
        End Function
    End Class

    Public Class DBLayer


        Private _conectionstring As String = ""

        Protected _withtransaction As Boolean = False

        Private oConnection As DBConnection

        Private disposed As Boolean = False

        Public Sub New(ByVal ConnectionString As String)
            MyBase.New()
            _conectionstring = ConnectionString
        End Sub

        Private Sub New()
            MyBase.New()
            Dispose(False)
        End Sub

        Public Overloads Sub Dispose()
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposed Then
                If disposing Then

                End If
            End If
            disposed = True
        End Sub

        Public Function Connect(ByVal WithTransaction As Boolean) As Boolean
            oConnection = New DBConnection(_conectionstring)
            Try
                If (WithTransaction = True) Then
                    oConnection.OpenConnection()
                    oConnection.BeginTransaction()
                    _withtransaction = WithTransaction
                    Return True
                Else
                    oConnection.OpenConnection()
                    Return True
                End If
            Catch ex As DBException
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As Exception
                Throw New DBException("Error in database execution", ex.ToString)
            End Try
        End Function

        Public Function CheckConnection() As Boolean
            Dim _IsValidConnection As Boolean = True
            oConnection = New DBConnection(_conectionstring)
            Dim _connection As SqlConnection = Nothing
            Try
                If (Not (_connection) Is Nothing) Then
                    If (_connection.State <> ConnectionState.Open) Then
                        _connection.Open()
                    End If
                Else
                    _connection = New SqlConnection
                    _connection.ConnectionString = _conectionstring
                    _connection.Open()
                End If
                Disconnect()
            Catch ex As DBException
                _IsValidConnection = False
            Catch ex As Exception
                _IsValidConnection = False
            End Try
            Return _IsValidConnection
        End Function

        Public Function Disconnect() As Boolean
            Try
                If (_withtransaction = True) Then
                    oConnection.CommitTransaction()
                    oConnection.CloseConnection()
                    Return True
                Else
                    oConnection.CloseConnection()
                    Return True
                End If
                If (Not (oConnection) Is Nothing) Then
                    oConnection.Dispose()
                End If
            Catch ex As DBException
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As Exception
                Throw New DBException("Error in database execution", ex.ToString)
            End Try
        End Function

        Public Overloads Function Execute(ByVal StoredProcedureName As String, ByVal Parameters As DBParameters) As Integer
            Dim _result As Integer = 0
            Dim _counter As Integer = 0
            Dim _sqlcommand As SqlCommand = New SqlCommand
            Dim osqlParameter As SqlParameter
            Try
                _sqlcommand.CommandType = CommandType.StoredProcedure
                _sqlcommand.CommandText = StoredProcedureName
                _sqlcommand.Connection = oConnection.GetConnection
                ''Incident - 00004116 Connection Timeout exception found in the log file.
                _sqlcommand.CommandTimeout = 0
                _counter = 0
                Do While (_counter _
                            <= (Parameters.Count - 1))
                    osqlParameter = New SqlParameter
                    osqlParameter.ParameterName = Parameters(_counter).ParameterName
                    osqlParameter.SqlDbType = Parameters(_counter).DataType
                    osqlParameter.Direction = Parameters(_counter).ParameterDirection
                    osqlParameter.Value = Parameters(_counter).Value

                    If (Parameters(_counter).Size <> 0) Then
                        osqlParameter.Size = Parameters(_counter).Size
                    End If

                    _sqlcommand.Parameters.Add(osqlParameter)
                    osqlParameter = Nothing
                    _counter = (_counter + 1)
                Loop
                _result = _sqlcommand.ExecuteNonQuery
            Catch ex As SqlException
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As DBException
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As Exception
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw New DBException("Error in database execution", ex.ToString)
            Finally
                If (Not (_sqlcommand) Is Nothing) Then
                    _sqlcommand.Dispose()
                End If
            End Try
            Return _result
        End Function

        Public Overloads Function Execute(ByVal StoredProcedureName As String) As Integer
            Dim _result As Integer = 0
            Dim _sqlcommand As SqlCommand = New SqlCommand
            Try
                _sqlcommand.CommandType = CommandType.StoredProcedure
                _sqlcommand.CommandText = StoredProcedureName
                _sqlcommand.Connection = oConnection.GetConnection
                ''Incident - 00004116 Connection Timeout exception found in the log file.
                _sqlcommand.CommandTimeout = 0
                _result = _sqlcommand.ExecuteNonQuery
            Catch ex As SqlException
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As DBException
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw ex
            Catch ex As Exception
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw New DBException("Error in database execution", ex.ToString)
            Finally
                If (Not (_sqlcommand) Is Nothing) Then
                    _sqlcommand.Dispose()
                End If
            End Try
            Return _result
        End Function

        Public Overloads Function Execute(ByVal StoredProcedureName As String, ByVal Parameters As DBParameters, ByRef ParameterValue As Object) As Integer
            Dim _result As Integer = 0
            Dim _counter As Integer = 0
            Dim _sqlcommand As SqlCommand = New SqlCommand
            Dim _outputCounter As Integer = 0
            Dim osqlParameter As SqlParameter
            Try
                _sqlcommand.CommandType = CommandType.StoredProcedure
                _sqlcommand.CommandText = StoredProcedureName
                _sqlcommand.Connection = oConnection.GetConnection
                ''Incident - 00004116 Connection Timeout exception found in the log file.
                _sqlcommand.CommandTimeout = 0
                _counter = 0
                Do While (_counter _
                            <= (Parameters.Count - 1))
                    If ((Parameters(_counter).ParameterDirection = ParameterDirection.Output) _
                                OrElse (Parameters(_counter).ParameterDirection = ParameterDirection.InputOutput)) Then
                        _outputCounter = _counter
                    End If
                    osqlParameter = New SqlParameter
                    osqlParameter.ParameterName = Parameters(_counter).ParameterName
                    osqlParameter.SqlDbType = Parameters(_counter).DataType
                    osqlParameter.Direction = Parameters(_counter).ParameterDirection
                    osqlParameter.Value = Parameters(_counter).Value

                    If (Parameters(_counter).Size <> 0) Then
                        osqlParameter.Size = Parameters(_counter).Size
                    End If

                    _sqlcommand.Parameters.Add(osqlParameter)
                    osqlParameter = Nothing
                    _counter = (_counter + 1)
                Loop
                _result = _sqlcommand.ExecuteNonQuery
                If (Not (_sqlcommand.Parameters(_outputCounter).Value) Is Nothing) Then
                    ParameterValue = _sqlcommand.Parameters(_outputCounter).Value
                Else
                    ParameterValue = 0
                End If
            Catch ex As SqlException
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As DBException
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As Exception
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw New DBException("Error in database execution", ex.ToString)
            Finally
                If (Not (_sqlcommand) Is Nothing) Then
                    _sqlcommand.Dispose()
                End If
            End Try
            Return _result
        End Function

        Public Overloads Function ExecuteScalar(ByVal StoredProcedureName As String, ByVal Parameters As DBParameters) As Object
            Dim _result As Object = Nothing
            Dim _counter As Integer = 0
            Dim _sqlcommand As SqlCommand = New SqlCommand
            Dim osqlParameter As SqlParameter
            Try
                _sqlcommand.CommandType = CommandType.StoredProcedure
                _sqlcommand.CommandText = StoredProcedureName
                _sqlcommand.Connection = oConnection.GetConnection
                _counter = 0
                Do While (_counter _
                            <= (Parameters.Count - 1))
                    osqlParameter = New SqlParameter
                    osqlParameter.ParameterName = Parameters(_counter).ParameterName
                    osqlParameter.SqlDbType = Parameters(_counter).DataType
                    osqlParameter.Direction = Parameters(_counter).ParameterDirection
                    osqlParameter.Value = Parameters(_counter).Value

                    If (Parameters(_counter).Size <> 0) Then
                        osqlParameter.Size = Parameters(_counter).Size
                    End If

                    _sqlcommand.Parameters.Add(osqlParameter)
                    osqlParameter = Nothing
                    _counter = (_counter + 1)
                Loop
                _result = _sqlcommand.ExecuteScalar
                If (_result Is Nothing) Then
                    _result = ""
                End If
            Catch ex As SqlException
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As DBException
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As Exception
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw New DBException("Error in database execution", ex.ToString)
            Finally
                If (Not (_sqlcommand) Is Nothing) Then
                    _sqlcommand.Dispose()
                End If
            End Try
            Return _result
        End Function

        Public Overloads Function ExecuteScalar(ByVal StoredProcedureName As String) As Object
            Dim _result As Object = Nothing
            Dim _sqlcommand As SqlCommand = New SqlCommand
            Try
                _sqlcommand.CommandType = CommandType.StoredProcedure
                _sqlcommand.CommandText = StoredProcedureName
                _sqlcommand.Connection = oConnection.GetConnection
                _result = _sqlcommand.ExecuteScalar
                If (_result Is Nothing) Then
                    _result = ""
                End If
            Catch ex As SqlException
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As DBException
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As Exception
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw New DBException("Error in database execution", ex.ToString)
            Finally
                If (Not (_sqlcommand) Is Nothing) Then
                    _sqlcommand.Dispose()
                End If
            End Try
            Return _result
        End Function

        Public Function Execute_Query(ByVal SQLQuery As String) As Integer
            Dim _result As Integer = 0
            Dim _sqlcommand As SqlCommand = New SqlCommand
            Try
                _sqlcommand.CommandType = CommandType.Text
                _sqlcommand.CommandText = SQLQuery
                _sqlcommand.Connection = oConnection.GetConnection
                _result = _sqlcommand.ExecuteNonQuery
            Catch ex As SqlException
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As DBException
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As Exception
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw New DBException("Error in database execution", ex.ToString)
            Finally
                If (Not (_sqlcommand) Is Nothing) Then
                    _sqlcommand.Dispose()
                End If
            End Try
            Return _result
        End Function

        Public Function ExecuteScalar_Query(ByVal SQLQuery As String) As Object
            Dim _result As Object = Nothing
            Dim _sqlcommand As SqlCommand = New SqlCommand
            Try
                _sqlcommand.CommandType = CommandType.Text
                _sqlcommand.CommandText = SQLQuery
                _sqlcommand.Connection = oConnection.GetConnection
                _result = _sqlcommand.ExecuteScalar
                If (_result Is Nothing) Then
                    _result = ""
                End If
            Catch ex As SqlException
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As DBException
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As Exception
                If (_withtransaction = True) Then
                    oConnection.RollbackTransaction()
                End If
                Throw New DBException("Error in database execution", ex.ToString)
            Finally
                If (Not (_sqlcommand) Is Nothing) Then
                    _sqlcommand.Dispose()
                End If
            End Try
            Return _result
        End Function

        Public Overloads Sub Retrive(ByVal StoredProcedureName As String, ByVal Parameters As DBParameters, ByRef _result As SqlDataReader)
            'SqlDataReader _result;
            Dim _counter As Integer = 0
            Dim _sqlcommand As SqlCommand = New SqlCommand
            Dim osqlParameter As SqlParameter
            Try
                _sqlcommand.CommandType = CommandType.StoredProcedure
                _sqlcommand.CommandText = StoredProcedureName
                _sqlcommand.Connection = oConnection.GetConnection
                _counter = 0
                Do While (_counter _
                            <= (Parameters.Count - 1))
                    osqlParameter = New SqlParameter
                    osqlParameter.ParameterName = Parameters(_counter).ParameterName
                    osqlParameter.SqlDbType = Parameters(_counter).DataType
                    osqlParameter.Direction = Parameters(_counter).ParameterDirection
                    osqlParameter.Value = Parameters(_counter).Value

                    If (Parameters(_counter).Size <> 0) Then
                        osqlParameter.Size = Parameters(_counter).Size
                    End If

                    _sqlcommand.Parameters.Add(osqlParameter)
                    osqlParameter = Nothing
                    _counter = (_counter + 1)
                Loop
                _result = _sqlcommand.ExecuteReader
            Catch ex As SqlException
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As DBException
                Throw ex
            Catch ex As Exception
                Throw New DBException("Error in database execution", ex.ToString)
            Finally
                If (Not (_sqlcommand) Is Nothing) Then
                    _sqlcommand.Dispose()
                End If
            End Try
            'return _result;
        End Sub

        Public Overloads Sub Retrive(ByVal StoredProcedureName As String, ByVal Parameters As DBParameters, ByRef _result As DataSet)
            'DataSet _result = new DataSet();
            Dim _counter As Integer = 0
            Dim _sqlcommand As SqlCommand = New SqlCommand
            Dim osqlParameter As SqlParameter
            Try
                _sqlcommand.CommandType = CommandType.StoredProcedure
                _sqlcommand.CommandText = StoredProcedureName
                _sqlcommand.Connection = oConnection.GetConnection
                _counter = 0
                Do While (_counter _
                            <= (Parameters.Count - 1))
                    osqlParameter = New SqlParameter
                    osqlParameter.ParameterName = Parameters(_counter).ParameterName
                    osqlParameter.SqlDbType = Parameters(_counter).DataType
                    osqlParameter.Direction = Parameters(_counter).ParameterDirection
                    osqlParameter.Value = Parameters(_counter).Value

                    If (Parameters(_counter).Size <> 0) Then
                        osqlParameter.Size = Parameters(_counter).Size
                    End If

                    _sqlcommand.Parameters.Add(osqlParameter)
                    osqlParameter = Nothing
                    _counter = (_counter + 1)
                Loop
                Dim _dataAdapter As SqlDataAdapter = New SqlDataAdapter(_sqlcommand)
                Dim _resultinternal As DataSet = New DataSet
                '_dataAdapter.Fill(_result);
                _dataAdapter.Fill(_resultinternal)
                _dataAdapter.Dispose()
                _result = _resultinternal
            Catch ex As SqlException
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As DBException
                Throw ex
            Catch ex As Exception
                Throw New DBException("Error in database execution", ex.ToString)
            Finally
                If (Not (_sqlcommand) Is Nothing) Then
                    _sqlcommand.Dispose()
                End If
            End Try
            'return _result;
        End Sub

        Public Overloads Sub Retrive(ByVal StoredProcedureName As String, ByVal Parameters As DBParameters, ByRef _result As DataTable)
            'DataTable _result = new DataTable();
            Dim _counter As Integer = 0
            Dim _sqlcommand As SqlCommand = New SqlCommand
            Dim osqlParameter As SqlParameter
            Try
                _sqlcommand.CommandType = CommandType.StoredProcedure
                _sqlcommand.CommandText = StoredProcedureName
                _sqlcommand.Connection = oConnection.GetConnection
                _counter = 0
                Do While (_counter _
                            <= (Parameters.Count - 1))
                    osqlParameter = New SqlParameter
                    osqlParameter.ParameterName = Parameters(_counter).ParameterName
                    osqlParameter.SqlDbType = Parameters(_counter).DataType
                    osqlParameter.Direction = Parameters(_counter).ParameterDirection
                    osqlParameter.Value = Parameters(_counter).Value

                    If (Parameters(_counter).Size <> 0) Then
                        osqlParameter.Size = Parameters(_counter).Size
                    End If

                    _sqlcommand.Parameters.Add(osqlParameter)
                    osqlParameter = Nothing
                    _counter = (_counter + 1)
                Loop
                Dim _dataAdapter As SqlDataAdapter = New SqlDataAdapter(_sqlcommand)
                Dim _dataset As DataSet = New DataSet
                Dim _resultinternal As DataTable = New DataTable
                _dataAdapter.Fill(_dataset)
                If (Not (_dataset.Tables(0)) Is Nothing) Then
                    _resultinternal = _dataset.Tables(0)
                End If
                _result = _resultinternal
                _resultinternal.Dispose()
                _dataset.Dispose()
                _dataAdapter.Dispose()
            Catch ex As SqlException
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As DBException
                Throw ex
            Catch ex As Exception
                Throw New DBException("Error in database execution", ex.ToString)
            Finally
                If (Not (_sqlcommand) Is Nothing) Then
                    _sqlcommand.Dispose()
                End If
            End Try
            'return _result;
        End Sub

        ' Without Parameters //
        Public Overloads Sub Retrive(ByVal StoredProcedureName As String, ByRef _result As SqlDataReader)
            'SqlDataReader _result;
            Dim _sqlcommand As SqlCommand = New SqlCommand
            Try
                _sqlcommand.CommandType = CommandType.StoredProcedure
                _sqlcommand.CommandText = StoredProcedureName
                _sqlcommand.Connection = oConnection.GetConnection
                _result = _sqlcommand.ExecuteReader
            Catch ex As SqlException
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As DBException
                Throw ex
            Catch ex As Exception
                Throw New DBException("Error in database execution", ex.ToString)
            Finally
                If (Not (_sqlcommand) Is Nothing) Then
                    _sqlcommand.Dispose()
                End If
            End Try
            'return _result;
        End Sub

        Public Overloads Sub Retrive(ByVal StoredProcedureName As String, ByRef _result As DataSet)
            'DataSet _result = new DataSet();
            Dim _sqlcommand As SqlCommand = New SqlCommand
            Try
                _sqlcommand.CommandType = CommandType.StoredProcedure
                _sqlcommand.CommandText = StoredProcedureName
                _sqlcommand.Connection = oConnection.GetConnection
                Dim _dataAdapter As SqlDataAdapter = New SqlDataAdapter(_sqlcommand)
                Dim _resultinternal As DataSet = New DataSet
                '_dataAdapter.Fill(_result);
                _dataAdapter.Fill(_resultinternal)
                _dataAdapter.Dispose()
                _result = _resultinternal
            Catch ex As SqlException
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As DBException
                Throw ex
            Catch ex As Exception
                Throw New DBException("Error in database execution", ex.ToString)
            Finally
                If (Not (_sqlcommand) Is Nothing) Then
                    _sqlcommand.Dispose()
                End If
            End Try
            'return _result;
        End Sub

        Public Overloads Sub Retrive(ByVal StoredProcedureName As String, ByRef _result As DataTable)
            'DataTable _result = new DataTable();
            Dim _sqlcommand As SqlCommand = New SqlCommand
            Try
                _sqlcommand.CommandType = CommandType.StoredProcedure
                _sqlcommand.CommandText = StoredProcedureName
                _sqlcommand.Connection = oConnection.GetConnection
                Dim _dataAdapter As SqlDataAdapter = New SqlDataAdapter(_sqlcommand)
                Dim _dataset As DataSet = New DataSet
                Dim _resultinternal As DataTable = New DataTable
                _dataAdapter.Fill(_dataset)
                If (Not (_dataset.Tables(0)) Is Nothing) Then
                    _resultinternal = _dataset.Tables(0)
                End If
                _result = _resultinternal
                _resultinternal.Dispose()
                _dataset.Dispose()
                _dataAdapter.Dispose()
            Catch ex As SqlException
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As DBException
                Throw ex
            Catch ex As Exception
                Throw New DBException("Error in database execution", ex.ToString)
            Finally
                If (Not (_sqlcommand) Is Nothing) Then
                    _sqlcommand.Dispose()
                End If
            End Try
            'return _result;
        End Sub

        Public Overloads Sub Retrive_Query(ByVal SQLQuery As String, ByRef _result As SqlDataReader)
            'SqlDataReader _result;
            Dim _sqlcommand As SqlCommand = New SqlCommand
            Try
                _sqlcommand.CommandType = CommandType.Text
                _sqlcommand.CommandText = SQLQuery
                _sqlcommand.Connection = oConnection.GetConnection
                'Incident - 00004116 Connection Timeout exception found in the log file.
                _sqlcommand.CommandTimeout = 0
                _result = _sqlcommand.ExecuteReader
            Catch ex As SqlException
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As DBException
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As Exception
                Throw New DBException("Error in database execution", ex.ToString)
            Finally
                If (Not (_sqlcommand) Is Nothing) Then
                    _sqlcommand.Dispose()
                End If
            End Try
            'return _result;
        End Sub

        Public Overloads Sub Retrive_Query(ByVal SQLQuery As String, ByRef _result As DataSet)
            'DataSet _result = new DataSet();
            Dim _sqlcommand As SqlCommand = New SqlCommand
            Try
                _sqlcommand.CommandType = CommandType.Text
                _sqlcommand.CommandText = SQLQuery
                _sqlcommand.Connection = oConnection.GetConnection
                ''Incident - 00004116 Connection Timeout exception found in the log file.
                _sqlcommand.CommandTimeout = 0
                Dim _dataAdapter As SqlDataAdapter = New SqlDataAdapter(_sqlcommand)
                Dim _resultinternal As DataSet = New DataSet
                '_dataAdapter.Fill(_result);
                _dataAdapter.Fill(_resultinternal)
                _dataAdapter.Dispose()
                _result = _resultinternal
            Catch ex As SqlException
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As DBException
                Throw ex
            Catch ex As Exception
                Throw New DBException("Error in database execution", ex.ToString)
            Finally
                If (Not (_sqlcommand) Is Nothing) Then
                    _sqlcommand.Dispose()
                End If
            End Try
            'return _result;
        End Sub

        Public Overloads Sub Retrive_Query(ByVal SQLQuery As String, ByRef _result As DataTable)
            'DataTable _result = new DataTable();
            Dim _sqlcommand As SqlCommand = New SqlCommand
            Try
                _sqlcommand.CommandType = CommandType.Text
                _sqlcommand.CommandText = SQLQuery
                _sqlcommand.Connection = oConnection.GetConnection
                ''Incident - 00004116 Connection Timeout exception found in the log file.
                _sqlcommand.CommandTimeout = 0
                Dim _dataAdapter As SqlDataAdapter = New SqlDataAdapter(_sqlcommand)
                Dim _dataset As DataSet = New DataSet
                Dim _resultinternal As DataTable = New DataTable
                _dataAdapter.Fill(_dataset)
                If (Not (_dataset.Tables(0)) Is Nothing) Then
                    _resultinternal = _dataset.Tables(0)
                End If
                _result = _resultinternal
                _resultinternal.Dispose()
                _dataset.Dispose()
                _dataAdapter.Dispose()
            Catch ex As SqlException
                'try
                '{
                '    MessageBox.Show("Error in DB", "gloPMS", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1,  0,  "E:\\Developer Working Folder\\gloPMS\\gloPMS\\bin\\Debug\\gloPMS_ERRORLog.log");
                '}
                'catch (Exception ex1)
                '{
                '    MessageBox.Show(ex1.ToString());
                '}
                Throw New DBException("Error in database execution", ex.ToString)
            Catch ex As DBException
                Throw ex
            Catch ex As Exception
                Throw New DBException("Error in database execution", ex.ToString)
            Finally
                If (Not (_sqlcommand) Is Nothing) Then
                    _sqlcommand.Dispose()
                End If
            End Try
            'return _result;
        End Sub
    End Class

    Public Class DBParameter

        Private _parametername As String

        Private _parameterdirection As ParameterDirection

        Private _datatype As SqlDbType

        Private _value As Object

        Private _size As Integer = 0

        Private disposed As Boolean = False

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal parametername As String, ByVal value As Object, ByVal parameterdirection As ParameterDirection, ByVal datatype As SqlDbType, ByVal fieldsize As Integer)
            MyBase.New()
            _parametername = parametername
            _parameterdirection = parameterdirection
            _datatype = datatype
            _value = value
            _size = fieldsize
        End Sub

        Public Sub New(ByVal parametername As String, ByVal value As Object, ByVal parameterdirection As ParameterDirection, ByVal datatype As SqlDbType)
            MyBase.New()
            _parametername = parametername
            _parameterdirection = parameterdirection
            _datatype = datatype
            _value = value
        End Sub

        Public Property ParameterName() As String
            Get
                Return _parametername
            End Get
            Set(ByVal value As String)
                _parametername = value
            End Set
        End Property

        Public Property ParameterDirection() As ParameterDirection
            Get
                Return _parameterdirection
            End Get
            Set(ByVal value As ParameterDirection)
                _parameterdirection = value
            End Set
        End Property

        Public Property DataType() As SqlDbType
            Get
                Return _datatype
            End Get
            Set(ByVal value As SqlDbType)
                _datatype = value
            End Set
        End Property

        Public Property Value() As Object
            Get
                Return _value
            End Get
            Set(ByVal value As Object)
                _value = value
            End Set
        End Property

        Public Property Size() As Integer
            Get
                Return _size
            End Get
            Set(ByVal value As Integer)
                _size = value
            End Set
        End Property

        Public Overloads Sub Dispose()
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposed Then
                If disposing Then

                End If
            End If
            disposed = True
        End Sub
    End Class

    Public Class DBParameters


        Protected _innerlist As ArrayList

        Private disposed As Boolean = False

        Public Sub New()
            MyBase.New()
            _innerlist = New ArrayList
        End Sub

        Public ReadOnly Property Count() As Integer
            Get
                Return _innerlist.Count
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal index As Integer) As DBParameter
            Get
                Return CType(_innerlist(index), DBParameter)
            End Get
        End Property

        Public Overloads Sub Dispose()
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposed Then
                If disposing Then

                End If
            End If
            disposed = True
        End Sub

        Public Overloads Sub Add(ByVal item As DBParameter)
            _innerlist.Add(item)
        End Sub

        Public Overloads Function Add(ByVal parametername As String, ByVal value As Object, ByVal parameterdirection As ParameterDirection, ByVal datatype As SqlDbType, ByVal fieldsize As Integer) As Integer
            Dim item As DBParameter = New DBParameter(parametername, value, parameterdirection, datatype, fieldsize)
            Return _innerlist.Add(item)
        End Function

        Public Overloads Function Add(ByVal parametername As String, ByVal value As Object, ByVal parameterdirection As ParameterDirection, ByVal datatype As SqlDbType) As Integer
            Dim item As DBParameter = New DBParameter(parametername, value, parameterdirection, datatype)
            Return _innerlist.Add(item)
        End Function

        Public Function Remove(ByVal item As DBParameter) As Boolean
            Dim result As Boolean = False
            Dim obj As DBParameter
            Dim i As Integer = 0
            Do While (i < _innerlist.Count)
                'store current index being checked
                obj = New DBParameter
                obj = CType(_innerlist(i), DBParameter)
                If ((obj.ParameterName = item.ParameterName) AndAlso (obj.DataType = item.DataType)) Then
                    _innerlist.RemoveAt(i)
                    result = True
                    Exit Do
                End If
                obj = Nothing
                i = (i + 1)
            Loop
            Return result
        End Function

        Public Function RemoveAt(ByVal index As Integer) As Boolean
            Dim result As Boolean = False
            _innerlist.RemoveAt(index)
            result = True
            Return result
        End Function

        Public Sub Clear()
            _innerlist.Clear()
        End Sub

        Public Function Contains(ByVal item As DBParameter) As Boolean
            Return _innerlist.Contains(item)
        End Function

        Public Function IndexOf(ByVal item As DBParameter) As Integer
            Return _innerlist.IndexOf(item)
        End Function

        Public Sub CopyTo(ByVal array() As DBParameter, ByVal index As Integer)
            _innerlist.CopyTo(array, index)
        End Sub
    End Class
End Namespace



