
Imports System
Imports System.Data
Imports System.Data.SqlClient
'Imports gloEMRActors
Namespace gloEMRDatabase
    Public Interface IgloEMRDatabase

        Function GetDataSet(ByVal strname As String, Optional ByVal blnCommandType As Boolean = True) As DataSet

        Function Add1(ByVal strname As String) As Int64
        Function Add(ByVal strname As String) As Int64
        Function Modify() As Boolean
        Function Delete(ByVal strname As String) As Boolean
        Function Delete_Query(ByVal SQLQuery As String) As Boolean

        Function GetDataTable(ByVal strname As String, Optional ByVal blnCommandType As Boolean = True) As DataTable
        ' Function GetDataSet(ByVal strname As String, Optional ByVal blnCommandType As Boolean = True) As DataTable
        Function GetDataValue(ByVal strname As String, Optional ByVal blnCommandType As Boolean = True) As Object
        'Function GetDataReader() As SqlDataReader
        Function GetDataTable_Query(ByVal SQLQuery As String) As DataTable
        Function GetRecord_Query(ByVal SQLQuery As String) As String

    End Interface

    Public Class DataBaseLayer
        Implements IgloEMRDatabase

        Private _DBParameters As DBParameters = Nothing
        Public Shared ConnectionString As String
        Private didICreatedNew As Boolean = False

        Public Sub New()
            MyBase.new()
            ' _DBParameters = New DBParameters
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Public Sub Dispose()
            If (IsNothing(_DBParameters) = False) Then
                If (didICreatedNew) Then
                    _DBParameters.Dispose()
                    didICreatedNew = False
                End If
                _DBParameters = Nothing
            End If
        End Sub

        Public Property DBParametersCol() As DBParameters
            Get
                If (IsNothing(_DBParameters)) Then
                    _DBParameters = New DBParameters
                    didICreatedNew = True
                End If
                Return _DBParameters
            End Get
            Set(ByVal value As DBParameters)
                If (IsNothing(_DBParameters) = False) Then
                    If (didICreatedNew) Then
                        _DBParameters.Dispose()
                        didICreatedNew = False
                    End If
                    _DBParameters = Nothing
                End If
                _DBParameters = value
            End Set
        End Property

        Public Function Add(ByVal strname As String) As Long Implements IgloEMRDatabase.Add

            Dim oSQLCommand As SqlCommand = Nothing
            Dim OsqlParmeter As SqlParameter = Nothing
            Dim _Result As Long
            Dim _RecordAffected As Long = 0
            Dim i As Integer
            Dim _sqlConnection As gloEMRDataConnection
            _sqlConnection = New gloEMRDataConnection(ConnectionString)

            Try
                oSQLCommand = New SqlCommand()
                oSQLCommand.CommandType = CommandType.StoredProcedure
                oSQLCommand.CommandText = strname
                oSQLCommand.Connection = _sqlConnection.gloSqlConnection
                If (IsNothing(_DBParameters) = False) Then


                    For i = 0 To _DBParameters.Count - 1
                        OsqlParmeter = New SqlParameter
                        With OsqlParmeter
                            .Direction = _DBParameters.Item(i).Direction
                            .ParameterName = _DBParameters.Item(i).Name
                            .SqlDbType = _DBParameters.Item(i).DataType
                            .Value = _DBParameters.Item(i).Value
                        End With
                        oSQLCommand.Parameters.Add(OsqlParmeter)
                    Next
                End If

                _RecordAffected = oSQLCommand.ExecuteNonQuery()

                If _RecordAffected > 0 Then
                    If OsqlParmeter.Direction = ParameterDirection.Output Or OsqlParmeter.Direction = ParameterDirection.InputOutput Then
                        _Result = CLng(OsqlParmeter.Value)
                    Else
                        _Result = 0
                    End If
                End If
                OsqlParmeter = Nothing
                Return _Result

            Catch ex As SqlException
                Return Nothing
            Catch ex As Exception
                Return Nothing
            Finally

                If Not IsNothing(oSQLCommand) Then
                    oSQLCommand.Parameters.Clear()
                    oSQLCommand.Dispose()
                    oSQLCommand = Nothing
                End If

                If Not IsNothing(_sqlConnection) Then
                    _sqlConnection.Dispose()
                    _sqlConnection = Nothing
                End If

                i = Nothing
                _Result = Nothing
                _RecordAffected = Nothing

            End Try
        End Function

        Public Function Delete(ByVal strname As String) As Boolean Implements IgloEMRDatabase.Delete
            Dim oSQLCommand As New SqlCommand
            Dim OsqlParmeter As SqlParameter
            Dim _sqlConnection As New gloEMRDataConnection(ConnectionString)
            Dim i As Integer

            Try
                oSQLCommand.CommandType = CommandType.StoredProcedure
                oSQLCommand.CommandText = strname
                oSQLCommand.Connection = _sqlConnection.gloSqlConnection
                If (IsNothing(_DBParameters) = False) Then
                    For i = 0 To _DBParameters.Count - 1
                        OsqlParmeter = New SqlParameter
                        With OsqlParmeter
                            .Direction = _DBParameters.Item(i).Direction
                            .ParameterName = _DBParameters.Item(i).Name
                            .SqlDbType = _DBParameters.Item(i).DataType
                            .Value = _DBParameters.Item(i).Value
                        End With
                        oSQLCommand.Parameters.Add(OsqlParmeter)
                        OsqlParmeter = Nothing
                    Next
                End If

                oSQLCommand.ExecuteNonQuery()

            Catch ex As Exception
                Dim objex As New gloDBException
                objex.ErrMessage = "Error Deleting Data"
                Throw objex
            Finally

                If Not IsNothing(oSQLCommand) Then
                    oSQLCommand.Parameters.Clear()
                    oSQLCommand.Dispose()
                    oSQLCommand = Nothing
                End If

                If Not IsNothing(_sqlConnection) Then
                    _sqlConnection.Dispose()
                    _sqlConnection = Nothing
                End If
                OsqlParmeter = Nothing
                i = Nothing

            End Try
            Return Nothing
        End Function

        Public Function GetDataTable(ByVal strname As String, Optional ByVal blnCommandType As Boolean = True) As System.Data.DataTable Implements IgloEMRDatabase.GetDataTable
            Dim oSQLCommand As New SqlCommand
            Dim OsqlParmeter As SqlParameter
            Dim _sqlConnection As New gloEMRDataConnection(ConnectionString)
            Dim i As Integer
            Dim dsData As New DataSet
            Try
                oSQLCommand.CommandType = CommandType.StoredProcedure
                oSQLCommand.CommandText = strname
                oSQLCommand.Connection = _sqlConnection.gloSqlConnection
                oSQLCommand.CommandTimeout = 0
                If (IsNothing(_DBParameters) = False) Then


                    For i = 0 To _DBParameters.Count - 1
                        OsqlParmeter = New SqlParameter
                        With OsqlParmeter
                            .Direction = _DBParameters.Item(i).Direction
                            .ParameterName = _DBParameters.Item(i).Name
                            .SqlDbType = _DBParameters.Item(i).DataType
                            .Value = _DBParameters.Item(i).Value
                        End With
                        oSQLCommand.Parameters.Add(OsqlParmeter)
                        OsqlParmeter = Nothing
                    Next
                End If

                Dim objDA As New SqlDataAdapter(oSQLCommand)

                objDA.Fill(dsData)
                objDA.Dispose()
                objDA = Nothing

                If dsData.Tables.Count > 0 Then
                    Return dsData.Tables(0).Copy()
                Else
                    Return Nothing
                End If

            Catch ex As Exception
                Dim objex As New gloDBException
                objex.ErrMessage = "Error Retrieving Data"
                Throw objex
                Return Nothing
            Finally

                If Not IsNothing(oSQLCommand) Then
                    oSQLCommand.Parameters.Clear()
                    oSQLCommand.Dispose()
                    oSQLCommand = Nothing
                End If

                If Not IsNothing(_sqlConnection) Then
                    _sqlConnection.Dispose()
                    _sqlConnection = Nothing
                End If
                If Not IsNothing(dsData) Then
                    dsData.Dispose()
                    dsData = Nothing
                End If
                OsqlParmeter = Nothing
                i = Nothing

            End Try
        End Function

        ''new method added for Prescription Provider issue Bug #46975 in 7022. 
        ''only to throw exception since the getDataTable() is used in word modules not to disturb the functionality we have added this special function
        Public Function GetSupervisingProviderData(ByVal strname As String, Optional ByVal blnCommandType As Boolean = True) As System.Data.DataTable
            Dim oSQLCommand As New SqlCommand
            Dim OsqlParmeter As SqlParameter
            Dim _sqlConnection As New gloEMRDataConnection(ConnectionString)
            Dim i As Integer
            Dim dsData As New DataSet
            Try
                oSQLCommand.CommandType = CommandType.StoredProcedure
                oSQLCommand.CommandText = strname
                oSQLCommand.Connection = _sqlConnection.gloSqlConnection
                oSQLCommand.CommandTimeout = 0
                If (IsNothing(_DBParameters) = False) Then


                    For i = 0 To _DBParameters.Count - 1
                        OsqlParmeter = New SqlParameter
                        With OsqlParmeter
                            .Direction = _DBParameters.Item(i).Direction
                            .ParameterName = _DBParameters.Item(i).Name
                            .SqlDbType = _DBParameters.Item(i).DataType
                            .Value = _DBParameters.Item(i).Value
                        End With
                        oSQLCommand.Parameters.Add(OsqlParmeter)
                        OsqlParmeter = Nothing
                    Next
                End If

                Dim objDA As New SqlDataAdapter(oSQLCommand)

                objDA.Fill(dsData)
                objDA.Dispose()
                objDA = Nothing

                If dsData.Tables.Count > 0 Then
                    Return dsData.Tables(0).Copy()
                Else
                    Return Nothing
                End If

            Catch ex As Exception

                Throw ex
                Return Nothing
            Finally

                If Not IsNothing(oSQLCommand) Then
                    oSQLCommand.Parameters.Clear()
                    oSQLCommand.Dispose()
                    oSQLCommand = Nothing
                End If

                If Not IsNothing(_sqlConnection) Then
                    _sqlConnection.Dispose()
                    _sqlConnection = Nothing
                End If
                If Not IsNothing(dsData) Then
                    dsData.Dispose()
                    dsData = Nothing
                End If
                'OsqlParmeter = Nothing

                i = Nothing

            End Try
        End Function

        Public Function GetDataSet(ByVal strname As String, Optional ByVal blnCommandType As Boolean = True) As System.Data.DataSet Implements IgloEMRDatabase.GetDataSet
            Dim oSQLCommand As New SqlCommand                       ' SQL Command
            Dim OsqlParmeter As SqlParameter = Nothing
            Dim _sqlConnection As New gloEMRDataConnection(ConnectionString)
            Dim i As Integer

            Try
                oSQLCommand.CommandType = CommandType.StoredProcedure   ' SQL Command Type , is Store Procedure
                oSQLCommand.CommandText = strname
                oSQLCommand.Connection = _sqlConnection.gloSqlConnection
                oSQLCommand.CommandTimeout = 0
                If (IsNothing(_DBParameters) = False) Then


                    For i = 0 To _DBParameters.Count - 1
                        OsqlParmeter = New SqlParameter
                        With OsqlParmeter
                            .Direction = _DBParameters.Item(i).Direction
                            .ParameterName = _DBParameters.Item(i).Name
                            .SqlDbType = _DBParameters.Item(i).DataType
                            .Value = _DBParameters.Item(i).Value
                        End With
                        oSQLCommand.Parameters.Add(OsqlParmeter)
                        OsqlParmeter = Nothing
                    Next
                End If

                Dim objDA As New SqlDataAdapter(oSQLCommand)
                Dim dsData As New DataSet
                objDA.Fill(dsData)
                objDA.Dispose()
                objDA = Nothing

                If dsData.Tables.Count > 0 Then
                    Return dsData
                Else
                    Return Nothing
                End If

            Catch ex As Exception
                Dim objex As New gloDBException
                objex.ErrMessage = "Error Retrieving Data"
                Throw objex
                Return Nothing
            Finally

                If Not IsNothing(oSQLCommand) Then
                    oSQLCommand.Parameters.Clear()
                    oSQLCommand.Dispose()
                    oSQLCommand = Nothing
                End If

                If Not IsNothing(_sqlConnection) Then
                    _sqlConnection.Dispose()
                    _sqlConnection = Nothing
                End If
                If Not IsNothing(OsqlParmeter) Then
                    OsqlParmeter = Nothing
                End If
                i = Nothing

            End Try
        End Function

        Public Function GetDataValue(ByVal strname As String, Optional ByVal blnCommandType As Boolean = True) As Object Implements IgloEMRDatabase.GetDataValue
            Dim oSQLCommand As New SqlCommand
            Dim OsqlParmeter As SqlParameter = Nothing
            Dim _sqlConnection As New gloEMRDataConnection(ConnectionString)
            Dim i As Integer

            Try
                oSQLCommand.CommandText = strname
                oSQLCommand.Connection = _sqlConnection.gloSqlConnection

                If blnCommandType Then
                    oSQLCommand.CommandType = CommandType.StoredProcedure
                    If (IsNothing(_DBParameters) = False) Then
                        For i = 0 To _DBParameters.Count - 1
                            OsqlParmeter = New SqlParameter
                            With OsqlParmeter
                                .Direction = _DBParameters.Item(i).Direction
                                .ParameterName = _DBParameters.Item(i).Name
                                .SqlDbType = _DBParameters.Item(i).DataType
                                .Value = _DBParameters.Item(i).Value
                            End With

                            oSQLCommand.Parameters.Add(OsqlParmeter)
                            OsqlParmeter = Nothing
                        Next
                    End If

                Else
                    oSQLCommand.CommandType = CommandType.Text
                End If

                Dim objreturnvalue As Object
                objreturnvalue = oSQLCommand.ExecuteScalar
                Return objreturnvalue

            Catch ex As Exception
                Dim objex As New gloDBException
                objex.ErrMessage = "Error Retrieving Data"
                Throw objex
            Finally

                If Not IsNothing(oSQLCommand) Then
                    oSQLCommand.Parameters.Clear()
                    oSQLCommand.Dispose()
                    oSQLCommand = Nothing
                End If

                If Not IsNothing(_sqlConnection) Then
                    _sqlConnection.Dispose()
                    _sqlConnection = Nothing
                End If
                If Not IsNothing(OsqlParmeter) Then
                    OsqlParmeter = Nothing
                End If
                i = Nothing

            End Try
        End Function

        Public Function Modify() As Boolean Implements IgloEMRDatabase.Modify
            Return Nothing
        End Function

        Public Function Add1(ByVal strname As String) As Long Implements IgloEMRDatabase.Add1
            Dim oSQLCommand As New SqlCommand
            Dim OsqlParmeter As SqlParameter = Nothing
            Dim osqlparametervisit As SqlParameter = Nothing
            Dim _blnResult As Boolean = False
            Dim _RecordAffected As Integer = 0
            Dim i As Integer
            Dim _sqlConnection As gloEMRDataConnection
            _sqlConnection = New gloEMRDataConnection(ConnectionString)

            Try
                oSQLCommand.CommandType = CommandType.StoredProcedure
                oSQLCommand.CommandText = strname
                oSQLCommand.Connection = _sqlConnection.gloSqlConnection
                If (IsNothing(_DBParameters) = False) Then

                    For i = 0 To _DBParameters.Count - 1
                        If _DBParameters.Item(i).Name = "@VisitID" Then
                            osqlparametervisit = New SqlParameter
                            With osqlparametervisit
                                .Direction = _DBParameters.Item(i).Direction
                                .ParameterName = _DBParameters.Item(i).Name
                                .SqlDbType = _DBParameters.Item(i).DataType
                                .Value = _DBParameters.Item(i).Value
                            End With
                            oSQLCommand.Parameters.Add(osqlparametervisit)

                            'plsease do not uncomment below line it causes problem
                            'osqlparametervisit = Nothing
                        Else
                            OsqlParmeter = New SqlParameter
                            With OsqlParmeter
                                .Direction = _DBParameters.Item(i).Direction
                                .ParameterName = _DBParameters.Item(i).Name
                                .SqlDbType = _DBParameters.Item(i).DataType
                                .Value = _DBParameters.Item(i).Value
                            End With
                            oSQLCommand.Parameters.Add(OsqlParmeter)
                            OsqlParmeter = Nothing
                        End If

                    Next

                End If

                _RecordAffected = oSQLCommand.ExecuteNonQuery()

                If _RecordAffected > 0 Then
                    _blnResult = True
                End If
                If Not IsNothing(osqlparametervisit.Value) Then
                    Return osqlparametervisit.Value
                Else
                    Return 0
                End If

            Catch ex As Exception
                Dim objex As New gloDBException
                objex.ErrMessage = "Error Inserting Data"
                Return 0
            Finally

                If Not IsNothing(oSQLCommand) Then
                    oSQLCommand.Parameters.Clear()
                    oSQLCommand.Dispose()
                    oSQLCommand = Nothing
                End If

                If Not IsNothing(_sqlConnection) Then
                    _sqlConnection.Dispose()
                    _sqlConnection = Nothing
                End If

                If Not IsNothing(OsqlParmeter) Then
                    OsqlParmeter = Nothing
                End If
                osqlparametervisit = Nothing

                i = Nothing
                _blnResult = Nothing
                _RecordAffected = Nothing

            End Try
        End Function


        Public Function GetAlternativeFromDrug_mst(ByVal dtTVP As System.Data.DataTable) As System.Data.DataTable
            Dim con As SqlConnection = Nothing
            Dim cmd As SqlCommand = Nothing
            Dim adp As SqlDataAdapter = Nothing
            Dim dtEligibilityInfo As DataSet = Nothing
            Try

                con = New SqlConnection(ConnectionString)

                cmd = New SqlCommand("gsp_GetAlternativeFromDrug", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New SqlParameter("@TVP_Ids", dtTVP))
                adp = New SqlDataAdapter(cmd)
                dtEligibilityInfo = New DataSet()
                adp.Fill(dtEligibilityInfo)
                If Not IsNothing(con) Then
                    con.Dispose()
                    con = Nothing
                End If

                Return dtEligibilityInfo.Tables(0).Copy()

            Catch ex As Exception
                Dim objex As New gloDBException
                objex.ErrMessage = "Error Retrieving Data"
                Throw objex
            Finally

                If Not IsNothing(cmd) Then
                    cmd.Dispose()
                    cmd = Nothing
                End If

                If Not IsNothing(con) Then
                    con.Dispose()
                    con = Nothing
                End If
                If Not IsNothing(adp) Then
                    adp.Dispose()
                    adp = Nothing
                End If
                If Not IsNothing(dtEligibilityInfo) Then
                    dtEligibilityInfo.Dispose()
                    dtEligibilityInfo = Nothing
                End If
            End Try
        End Function
        Public Function GetDataTable_Query(ByVal SQLQuery As String) As System.Data.DataTable Implements IgloEMRDatabase.GetDataTable_Query
            Dim oSQLCommand As New SqlCommand
            Dim _sqlConnection As New gloEMRDataConnection(ConnectionString)
            Dim dsData As New DataSet

            Try
                oSQLCommand.CommandType = CommandType.Text
                oSQLCommand.CommandText = SQLQuery
                oSQLCommand.Connection = _sqlConnection.gloSqlConnection

                Dim objDA As New SqlDataAdapter(oSQLCommand)

                objDA.Fill(dsData)

                If Not IsNothing(objDA) Then
                    objDA.Dispose()
                    objDA = Nothing
                End If

                Return dsData.Tables(0).Copy()

            Catch ex As Exception
                Dim objex As New gloDBException
                objex.ErrMessage = "Error Retrieving Data"
                Throw objex
            Finally

                If Not IsNothing(oSQLCommand) Then
                    oSQLCommand.Dispose()
                    oSQLCommand = Nothing
                End If

                If Not IsNothing(_sqlConnection) Then
                    _sqlConnection.Dispose()
                    _sqlConnection = Nothing
                End If

                If Not IsNothing(dsData) Then
                    dsData.Dispose()
                    dsData = Nothing
                End If

            End Try

        End Function

        Public Function GetRecord_Query(ByVal SQLQuery As String) As String Implements IgloEMRDatabase.GetRecord_Query

            Dim oSQLCommand As New SqlCommand
            Dim _sqlConnection As New gloEMRDataConnection(ConnectionString)
            Dim _Result As String = ""
            Dim objResult As Object

            Try

                oSQLCommand.CommandType = CommandType.Text
                oSQLCommand.CommandText = SQLQuery
                oSQLCommand.Connection = _sqlConnection.gloSqlConnection

                'If Not IsDBNull(oSQLCommand.ExecuteScalar) Then
                objResult = oSQLCommand.ExecuteScalar
                'Else
                '_Result = ""
                'End If

                If IsDBNull(objResult) = True Then
                    _Result = ""
                ElseIf objResult Is Nothing Then
                    _Result = ""
                Else
                    _Result = objResult
                End If

                Return _Result

            Catch ex As Exception
                GetRecord_Query = Nothing
                Dim objex As New gloDBException
                objex.ErrMessage = "Error Retrieving Data"
                Throw objex
            Finally

                If Not IsNothing(oSQLCommand) Then
                    oSQLCommand.Parameters.Clear()
                    oSQLCommand.Dispose()
                    oSQLCommand = Nothing
                End If

                If Not IsNothing(_sqlConnection) Then
                    _sqlConnection.Dispose()
                    _sqlConnection = Nothing
                End If

                _Result = Nothing

            End Try

        End Function

        Public Sub ExecuteNon_Query(ByVal SQLQuery As String)
            Dim oSQLCommand As New SqlCommand
            Dim _sqlConnection As New gloEMRDataConnection(ConnectionString)
            Dim osqlparameter As SqlParameter = Nothing

            Try
                oSQLCommand.CommandType = CommandType.StoredProcedure
                oSQLCommand.CommandText = SQLQuery
                oSQLCommand.Connection = _sqlConnection.gloSqlConnection
                If (IsNothing(_DBParameters) = False) Then
                    For i As Integer = 0 To _DBParameters.Count - 1
                        osqlparameter = New SqlParameter
                        With osqlparameter
                            .Direction = _DBParameters.Item(i).Direction
                            .ParameterName = _DBParameters.Item(i).Name
                            .SqlDbType = _DBParameters.Item(i).DataType
                            .Value = _DBParameters.Item(i).Value
                        End With
                        oSQLCommand.Parameters.Add(osqlparameter)
                        osqlparameter = Nothing
                    Next
                End If

                oSQLCommand.ExecuteNonQuery()

            Catch ex As Exception
                Dim objex As New gloDBException
                objex.ErrMessage = "Error Retrieving Data"
                Throw objex
            Finally

                If Not IsNothing(oSQLCommand) Then
                    oSQLCommand.Parameters.Clear()
                    oSQLCommand.Dispose()
                    oSQLCommand = Nothing
                End If

                If Not IsNothing(_sqlConnection) Then
                    _sqlConnection.Dispose()
                    _sqlConnection = Nothing
                End If

            End Try
        End Sub

        Public Function Delete_Query(ByVal SQLQuery As String) As Boolean Implements IgloEMRDatabase.Delete_Query
            Dim oSQLCommand As New SqlCommand
            Dim _Result As Boolean = False
            Dim _sqlConnection As gloEMRDataConnection
            _sqlConnection = New gloEMRDataConnection(ConnectionString)

            Try
                oSQLCommand.CommandType = CommandType.Text
                oSQLCommand.CommandText = SQLQuery
                oSQLCommand.Connection = _sqlConnection.gloSqlConnection
                oSQLCommand.ExecuteNonQuery()

                Return True

            Catch ex As SqlException
                Dim objex As New gloDBException
                objex.ErrMessage = "Error Retrieving Data"
                Throw objex
            Finally

                If Not IsNothing(oSQLCommand) Then
                    oSQLCommand.Parameters.Clear()
                    oSQLCommand.Dispose()
                    oSQLCommand = Nothing
                End If

                If Not IsNothing(_sqlConnection) Then
                    _sqlConnection.Dispose()
                    _sqlConnection = Nothing
                End If

                _Result = Nothing

            End Try

        End Function
    End Class

    Public Class gloEMRDataConnection

        Public Sub New(ByVal ConnectionString As String)
            MyBase.New()

            Dim _strconn As String = ConnectionString

            _sqlConnection = New SqlConnection
            Connect(_strconn)
        End Sub

        Public Sub Dispose()
            DisConnect()
            If (IsNothing(_sqlConnection) = False) Then
                _sqlConnection.Dispose()
                _sqlConnection = Nothing
            End If
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Private _sqlConnection As SqlConnection
        Private _sqlTransaction As SqlTransaction
        Friend Event SendMessage(ByVal strMessage As String)

        Public ReadOnly Property gloSqlConnection() As SqlConnection
            Get
                Return _sqlConnection
            End Get
        End Property

        Public ReadOnly Property DBTransaction() As SqlTransaction
            Get
                If Not IsNothing(_sqlTransaction) Then
                    Return _sqlTransaction
                Else
                    If _sqlConnection.State = ConnectionState.Open Then
                        _sqlTransaction = _sqlConnection.BeginTransaction
                        Return _sqlTransaction
                    Else
                        Return Nothing
                    End If
                End If
            End Get

        End Property

        Public Sub DisConnect()
            Try
                If _sqlConnection.State <> ConnectionState.Closed Then _sqlConnection.Close()
            Catch objError As Exception
                Dim objEx As New gloDBException
                objEx.ErrMessage = "Error closing connection."
                Throw objEx
            End Try
        End Sub

        Public Function Connect(ByVal ConnectionStrings As String) As Boolean
            Try
                If Trim(ConnectionStrings) <> "" Then
                    With _sqlConnection
                        If .State <> ConnectionState.Closed Then .Close()
                        .ConnectionString = ConnectionStrings
                        If .State = ConnectionState.Closed Then
                            .Open()
                        End If
                    End With
                    Return True
                Else
                    RaiseEvent SendMessage("Please check database connectivity.")
                    Return False
                End If
            Catch objEror As Exception

                Dim objEx As New gloDBException
                objEx.ErrMessage = "Error closing connection."
                Connect = False
                Throw objEx
            End Try
        End Function

    End Class

    Public Class DBParameter
        Private _Name As String
        Private _value As Object
        Private _Direction As System.Data.ParameterDirection
        Private _DataType As System.Data.SqlDbType
        Private _Size As Int32

        Public Property Size() As Int32
            Get
                Return _Size
            End Get
            Set(ByVal value As Int32)
                _Size = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal Value As String)
                _Name = Value
            End Set
        End Property

        Public Property Value() As Object
            Get
                Return _value
            End Get
            Set(ByVal Value As Object)
                _value = Value
            End Set
        End Property

        Public Property Direction() As System.Data.ParameterDirection
            Get
                Return _Direction
            End Get
            Set(ByVal Value As System.Data.ParameterDirection)
                _Direction = Value
            End Set
        End Property

        Public Property DataType() As System.Data.SqlDbType
            Get
                Return _DataType
            End Get
            Set(ByVal Value As System.Data.SqlDbType)
                _DataType = Value
            End Set
        End Property

        Public Sub New()
            MyBase.new()
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub
    End Class

    Public Class DBParameters

        Inherits CollectionBase

        'Remove Item at specified index
        Public Sub Remove(ByVal index As Integer)
            ' Check to see if there is a widget at the supplied index.
            If index > Count - 1 Or index < 0 Then
                ' If no object exists, a messagebox is shown and the operation is 
                ' cancelled.
                'System.Windows.Forms.MessageBox.Show("Index not valid!")
            Else
                ' Invokes the RemoveAt method of the List object.
                List.RemoveAt(index)
            End If
        End Sub

        ' This line declares the Item property as ReadOnly, and 
        ' declares that it will return a PatientInterface object.
        Public ReadOnly Property Item(ByVal index As Integer) As DBParameter
            Get
                ' The appropriate item is retrieved from the List object and 
                ' explicitly cast to the PatientInterface type, then returned to the 
                ' caller.
                Return CType(List.Item(index), DBParameter)
            End Get
        End Property

        ' Restricts to PatientInterface types, items that can be added to the collection.
        Public Sub Add(ByVal _DBParameter As DBParameter)
            ' Invokes Add method of the List object to add a PatientInterface.
            List.Add(_DBParameter)
        End Sub

        Public Overridable Sub Dispose()
            Me.Clear()
        End Sub

    End Class

End Namespace


