Imports System.Data.SqlClient

Namespace gloStream

    Namespace gloDataBase

        Public Class gloDataBase

            Implements IDisposable

            Private _ErrorMessage As String
            Private gloDBConnection As System.Data.SqlClient.SqlConnection = Nothing
            Private _DBParameters As gloStream.gloDataBase.gloDBSupport.DBParameters = Nothing
            Private bgloDBConnection As Boolean = False
            Private bDBParameters As Boolean = False
            'Properties
            Public Property DBParameters() As gloStream.gloDataBase.gloDBSupport.DBParameters
                Get
                    Return _DBParameters
                End Get
                Set(ByVal Value As gloStream.gloDataBase.gloDBSupport.DBParameters)
                    If (bDBParameters) Then
                        If (IsNothing(_DBParameters) = False) Then
                            _DBParameters.Dispose()
                            _DBParameters = Nothing
                        End If
                        bDBParameters = False
                    End If
                    _DBParameters = Value
                End Set
            End Property

            'Methods
            Public Function ReadQueryRecordAsDataSet(ByVal SQLQuery As String) As DataSet
                ' Data Reader

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        ReadQueryRecordAsDataSet = Nothing

                        Exit Function
                    End If
                    'Work with database
                    Dim oDataAdapter As New SqlDataAdapter(SQLQuery, gloDBConnection)
                    Dim oDS As New DataSet
                    oDataAdapter.Fill(oDS)
                    oDataAdapter.Dispose()
                    Return oDS
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsgloDataBase -- ReadQueryRecordAsDataSet -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsgloDataBase -- ReadQueryRecordAsDataSet -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally

                End Try
            End Function

            Public Function ReadData(ByVal StoredProcedure As String) As DataTable
                Dim oSQLCommand As SqlCommand = Nothing 'SQL Command
                Dim dsData As DataSet = Nothing
                Dim OsqlParmeter As SqlParameter
                Dim i As Integer


                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        ReadData = Nothing
                        Exit Function
                    End If
                    oSQLCommand = New SqlCommand  'SQL Command
                   
                    'Work with database
                    oSQLCommand.CommandType = CommandType.StoredProcedure 'SQL Command Type, is Store Procedure
                    oSQLCommand.CommandText = StoredProcedure 'Set Store Procedure Name
                    ''Bug #51463: 00000460 : Error Messsage- EMR Recover Exams tool in gloEMR does not work.
                    ''Description: Exception throws for connection to server(timeout). So set CommandTimeout=0 to solve problem.
                    oSQLCommand.CommandTimeout = 0
                    oSQLCommand.Connection = gloDBConnection


                    For i = 1 To _DBParameters.Count
                        OsqlParmeter = New SqlParameter
                        With OsqlParmeter
                            .Direction = _DBParameters.Item(i).Direction
                            .ParameterName = _DBParameters.Item(i).Name
                            .SqlDbType = _DBParameters.Item(i).DataType
                            .Value = _DBParameters.Item(i).Value
                        End With
                        oSQLCommand.Parameters.Add(OsqlParmeter)
                    Next

                    Dim objDA As New SqlDataAdapter(oSQLCommand)

                    dsData = New DataSet
                    objDA.Fill(dsData)

                    objDA.Dispose() : objDA = Nothing

                    Return dsData.Tables(0).Copy()
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsgloDataBase -- ReadData -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsgloDataBase -- ReadData -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally

                    If oSQLCommand IsNot Nothing Then
                        oSQLCommand.Parameters.Clear()
                        oSQLCommand.Dispose()
                        oSQLCommand = Nothing
                    End If
                    If dsData IsNot Nothing Then
                        dsData.Dispose()
                        dsData = Nothing
                    End If


                    OsqlParmeter = Nothing

                End Try
            End Function

            Public Function ReadQueryDataTable(ByVal SQLQuery As String) As DataTable
                ' Data Table
                Dim oDT As DataTable = Nothing
                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        ReadQueryDataTable = Nothing
                        Exit Function
                    End If

                    'Work with database
                    Dim oDataAdapter As New SqlDataAdapter(SQLQuery, gloDBConnection)
                    oDT = New DataTable
                    oDataAdapter.Fill(oDT)

                    oDataAdapter.Dispose() : oDataAdapter = Nothing

                    Return oDT

                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsgloDataBase -- ReadQueryDataTable -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsgloDataBase -- ReadQueryDataTable -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    oDT = Nothing
                End Try

            End Function

            Public Function ReadRecords(ByVal StoredProcedure As String) As System.Data.SqlClient.SqlDataReader
                Dim oSQLCommand As SqlCommand = Nothing 'SQL Command
                Dim oDataReader As SqlDataReader 'Data Reader
                Dim OsqlParmeter As SqlParameter
                Dim i As Integer

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        ReadRecords = Nothing
                        Exit Function
                    End If
                    oSQLCommand = New SqlCommand
                    'Work with database
                    oSQLCommand.CommandType = CommandType.StoredProcedure 'SQL Command Type, is Store Procedure
                    oSQLCommand.CommandText = StoredProcedure 'Set Store Procedure Name
                    oSQLCommand.Connection = gloDBConnection

                    For i = 1 To _DBParameters.Count
                        OsqlParmeter = New SqlParameter
                        With OsqlParmeter
                            .Direction = _DBParameters.Item(i).Direction
                            .ParameterName = _DBParameters.Item(i).Name
                            .SqlDbType = _DBParameters.Item(i).DataType
                            .Value = _DBParameters.Item(i).Value
                        End With
                        oSQLCommand.Parameters.Add(OsqlParmeter)
                    Next

                    oDataReader = oSQLCommand.ExecuteReader

                    Return oDataReader

                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsgloDataBase -- ReadRecords -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsgloDataBase -- ReadRecords -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally

                    If IsNothing(oSQLCommand) = False Then
                        oSQLCommand.Parameters.Clear()
                        oSQLCommand.Dispose()
                        oSQLCommand = Nothing
                    End If

                    oDataReader = Nothing
                    OsqlParmeter = Nothing

                End Try
            End Function

            Public Function ReadQueryRecords(ByVal SQLQuery As String) As System.Data.SqlClient.SqlDataReader
                Dim oSQLCommand As SqlCommand = Nothing 'SQL Command
                Dim oDataReader As SqlDataReader 'Data Reader

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        ReadQueryRecords = Nothing
                        Exit Function
                    End If
                    If Trim(SQLQuery) = "" Then
                        _ErrorMessage = "Please select query description"
                        ReadQueryRecords = Nothing
                        Exit Function
                    End If
                    oSQLCommand = New SqlCommand
                    'Work with database
                    oSQLCommand.CommandType = CommandType.Text   'SQL Command Type , is Store Procedure
                    oSQLCommand.CommandText = SQLQuery           'Set Store Procedure Name
                    oSQLCommand.Connection = gloDBConnection
                    oDataReader = oSQLCommand.ExecuteReader
                    Return oDataReader
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsgloDataBase -- ReadQueryRecords -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsgloDataBase -- ReadQueryRecords -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If oSQLCommand IsNot Nothing Then
                        oSQLCommand.Parameters.Clear()
                        oSQLCommand.Dispose()
                        oSQLCommand = Nothing
                    End If
                End Try
            End Function

            Public Function ReadQueryRecord(ByVal SQLQuery As String) As Boolean
                Dim oSQLCommand As SqlCommand = Nothing 'SQL Command
                '                Dim oDataReader As SqlDataReader 'Data Reader

                Dim _blnResult As Boolean = False
                Dim _RecordAffected As Integer = 0
                'Dim i As Integer

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        ReadQueryRecord = Nothing
                        Exit Function
                    End If
                    oSQLCommand = New SqlCommand
                    'Work with database
                    oSQLCommand.CommandType = CommandType.Text   'SQL Command Type , is Store Procedure
                    oSQLCommand.CommandText = SQLQuery           'Set Store Procedure Name
                    oSQLCommand.Connection = gloDBConnection

                    _RecordAffected = oSQLCommand.ExecuteScalar

                    If _RecordAffected > 0 Then
                        _blnResult = True
                    End If

                    Return _blnResult
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsgloDataBase -- ReadQueryRecord -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsgloDataBase -- ReadQueryRecord -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If oSQLCommand IsNot Nothing Then
                        oSQLCommand.Parameters.Clear()
                        oSQLCommand.Dispose()
                        oSQLCommand = Nothing
                    End If
                End Try
            End Function

            Public Function ReadCatRecords(ByVal SQLQuery As String) As System.Data.DataSet
                Dim oSQLCommand As SqlCommand = Nothing 'SQL Command
                Dim oDataSet As DataSet = Nothing


                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        ReadCatRecords = Nothing
                        Exit Function
                    End If
                    If Trim(SQLQuery) = "" Then
                        _ErrorMessage = "Please select query description"
                        ReadCatRecords = Nothing
                        Exit Function
                    End If
                    oSQLCommand = New SqlCommand
                    'Work with database
                    oSQLCommand.CommandType = CommandType.Text   'SQL Command Type , is Store Procedure
                    oSQLCommand.CommandText = SQLQuery           'Set Store Procedure Name
                    oSQLCommand.Connection = gloDBConnection
                    Dim OsqlDataAdp As New SqlDataAdapter
                    OsqlDataAdp.SelectCommand = oSQLCommand
                    oDataSet = New DataSet
                    OsqlDataAdp.Fill(oDataSet)
                    OsqlDataAdp.Dispose()
                    OsqlDataAdp = Nothing
                    Return oDataSet

                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsgloDataBase -- ReadCatRecords -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsgloDataBase -- ReadCatRecords -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If oSQLCommand IsNot Nothing Then
                        oSQLCommand.Parameters.Clear()
                        oSQLCommand.Dispose()
                        oSQLCommand = Nothing
                    End If
                End Try
            End Function

            Public Function ExecuteNonQuery(ByVal StoredProcedure As String) As Boolean
                Dim oSQLCommand As SqlCommand = Nothing 'SQL Command
                ' Dim oDataReader As SqlDataReader  'Data Reader
                Dim OsqlParmeter As SqlParameter
                Dim _blnResult As Boolean = False
                Dim _RecordAffected As Integer = 0
                Dim i As Integer

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        ExecuteNonQuery = Nothing
                        Exit Function
                    End If
                    oSQLCommand = New SqlCommand
                    'Work with database
                    oSQLCommand.CommandType = CommandType.StoredProcedure 'SQL Command Type , is Store Procedure
                    oSQLCommand.CommandText = StoredProcedure 'Set Store Procedure Name
                    oSQLCommand.Connection = gloDBConnection


                    For i = 1 To _DBParameters.Count
                        OsqlParmeter = New SqlParameter
                        With OsqlParmeter
                            .Direction = _DBParameters.Item(i).Direction
                            .ParameterName = _DBParameters.Item(i).Name
                            .SqlDbType = _DBParameters.Item(i).DataType
                            .Value = _DBParameters.Item(i).Value

                        End With
                        oSQLCommand.Parameters.Add(OsqlParmeter)
                    Next

                    _RecordAffected = oSQLCommand.ExecuteNonQuery()

                    If _RecordAffected > 0 Then
                        _blnResult = True
                    End If

                    Return _blnResult
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsgloDataBase -- ExecuteNonQuery -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsgloDataBase -- ExecuteNonQuery -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If oSQLCommand IsNot Nothing Then
                        oSQLCommand.Parameters.Clear()
                        oSQLCommand.Dispose()
                        oSQLCommand = Nothing
                    End If
                    OsqlParmeter = Nothing
                End Try
            End Function

            Public Function ExecuteQueryNonQuery(ByVal SQLQuery As String) As Boolean
                Dim oSQLCommand As SqlCommand = Nothing                    ' SQL Command
                Dim _blnResult As Boolean = False
                Dim _RecordAffected As Integer = 0
                'Dim i As Integer

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        ExecuteQueryNonQuery = Nothing
                        Exit Function
                    End If
                    oSQLCommand = New SqlCommand
                    'Work with database
                    oSQLCommand.CommandType = CommandType.Text
                    oSQLCommand.CommandText = SQLQuery
                    oSQLCommand.Connection = gloDBConnection

                    _RecordAffected = oSQLCommand.ExecuteNonQuery()

                    If _RecordAffected > 0 Then
                        _blnResult = True
                    End If

                    Return _blnResult
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsgloDataBase -- ExecuteQueryNonQuery -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsgloDataBase -- ExecuteQueryNonQuery -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If oSQLCommand IsNot Nothing Then
                        oSQLCommand.Parameters.Clear()
                        oSQLCommand.Dispose()
                        oSQLCommand = Nothing
                    End If
                End Try
            End Function

            'Function to return the value from stored proce ...now it is used only in datacatcher added on 24/08/2006
            Public Function ExecuteNonQueryVal(ByVal StoredProcedure As String, ByRef intPatientId As Long) As Boolean
                Dim oSQLCommand As SqlCommand = Nothing 'SQL Command
                Dim OsqlParmeter As SqlParameter = Nothing
                Dim _Result As String = ""
                Dim i As Integer

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        ExecuteNonQueryVal = Nothing
                        Exit Function
                    End If
                    oSQLCommand = New SqlCommand
                    'Work with database
                    oSQLCommand.CommandType = CommandType.StoredProcedure 'SQL Command Type , is Store Procedure
                    oSQLCommand.CommandText = StoredProcedure 'Set Store Procedure Name
                    oSQLCommand.Connection = gloDBConnection


                    For i = 1 To _DBParameters.Count

                        OsqlParmeter = New SqlParameter
                        With OsqlParmeter
                            .Direction = _DBParameters.Item(i).Direction
                            .ParameterName = _DBParameters.Item(i).Name
                            .SqlDbType = _DBParameters.Item(i).DataType
                            If .Direction = ParameterDirection.Input Then
                                .Value = _DBParameters.Item(i).Value
                            End If
                        End With
                        oSQLCommand.Parameters.Add(OsqlParmeter)

                    Next

                    oSQLCommand.ExecuteNonQuery()
                    If OsqlParmeter.Value <> 0 Then
                        intPatientId = OsqlParmeter.Value
                    End If
                    Return True
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsgloDataBase -- ExecuteNonQueryVal -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsgloDataBase -- ExecuteNonQueryVal -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If oSQLCommand IsNot Nothing Then
                        oSQLCommand.Parameters.Clear()
                        oSQLCommand.Dispose()
                        oSQLCommand = Nothing
                    End If
                    OsqlParmeter = Nothing
                End Try
            End Function
            '----------------------

            Public Function ExecuteNonSQLQuery(ByVal SQLQuery As String) As Boolean
                Dim oSQLCommand As SqlCommand = Nothing                      ' SQL Command
                '  Dim oDataReader As SqlDataReader                        ' Data Reader
                Dim OsqlParmeter As SqlParameter
                Dim _blnResult As Boolean = False
                Dim _RecordAffected As Integer = 0
                Dim i As Integer

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        ExecuteNonSQLQuery = Nothing
                        Exit Function
                    End If

                    oSQLCommand = New SqlCommand
                    'Work with database
                    oSQLCommand.CommandType = CommandType.Text 'SQL Command Type , is Store Procedure
                    oSQLCommand.CommandText = SQLQuery 'Set Store Procedure Name
                    oSQLCommand.Connection = gloDBConnection

                    For i = 1 To _DBParameters.Count
                        OsqlParmeter = New SqlParameter
                        With OsqlParmeter
                            .Direction = _DBParameters.Item(i).Direction
                            .ParameterName = _DBParameters.Item(i).Name
                            .SqlDbType = _DBParameters.Item(i).DataType
                            .Value = _DBParameters.Item(i).Value
                        End With
                        oSQLCommand.Parameters.Add(OsqlParmeter)
                    Next

                    _RecordAffected = oSQLCommand.ExecuteNonQuery()

                    If _RecordAffected > 0 Then
                        _blnResult = True
                    End If

                    Return _blnResult
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsgloDataBase -- ExecuteNonSQLQuery -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsgloDataBase -- ExecuteNonSQLQuery -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If oSQLCommand IsNot Nothing Then
                        oSQLCommand.Parameters.Clear()
                        oSQLCommand.Dispose()
                        oSQLCommand = Nothing
                    End If
                    OsqlParmeter = Nothing
                End Try
            End Function

            Public Function ExecuteNonQueryForOutput(ByVal StoredProcedure As String) As Long
                Dim ID As Long = 0
                Dim oSQLCommand As SqlCommand = Nothing 'SQL Command
                ' Dim oDataReader As SqlDataReader 'Data Reader
                Dim OsqlParmeter As SqlParameter = Nothing
                Dim _blnResult As Boolean = False
                Dim _RecordAffected As Integer = 0
                Dim i As Integer

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        ExecuteNonQueryForOutput = Nothing
                        Exit Function
                    End If
                    oSQLCommand = New SqlCommand
                    'Work with database
                    oSQLCommand.CommandType = CommandType.StoredProcedure 'SQL Command Type , is Store Procedure
                    oSQLCommand.CommandText = StoredProcedure 'Set Store Procedure Name
                    oSQLCommand.Connection = gloDBConnection

                    For i = 1 To _DBParameters.Count
                        OsqlParmeter = New SqlParameter
                        With OsqlParmeter
                            .Direction = _DBParameters.Item(i).Direction
                            .ParameterName = _DBParameters.Item(i).Name
                            .SqlDbType = _DBParameters.Item(i).DataType
                            .Value = _DBParameters.Item(i).Value
                        End With
                        oSQLCommand.Parameters.Add(OsqlParmeter)
                    Next

                    _RecordAffected = oSQLCommand.ExecuteNonQuery()
                    ID = OsqlParmeter.Value

                    If _RecordAffected > 0 Then
                        _blnResult = True
                    End If

                    Return ID
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsgloDataBase -- ExecuteNonQueryForOutput -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsgloDataBase -- ExecuteNonQueryForOutput -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If oSQLCommand IsNot Nothing Then
                        oSQLCommand.Parameters.Clear()
                        oSQLCommand.Dispose()
                        oSQLCommand = Nothing
                    End If
                    OsqlParmeter = Nothing
                End Try
            End Function

            Public Function ExecuteScaler(ByVal StoredProcedure As String) As String
                Dim oSQLCommand As SqlCommand = Nothing                    ' SQL Command
                Dim OsqlParmeter As SqlParameter
                Dim _Result As String = ""
                Dim i As Integer

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        ExecuteScaler = Nothing
                        Exit Function
                    End If
                    oSQLCommand = New SqlCommand
                    'Work with database
                    oSQLCommand.CommandType = CommandType.StoredProcedure 'SQL Command Type, is Store Procedure
                    oSQLCommand.CommandText = StoredProcedure 'Set Store Procedure Name
                    oSQLCommand.Connection = gloDBConnection

                    For i = 1 To _DBParameters.Count
                        OsqlParmeter = New SqlParameter
                        With OsqlParmeter
                            .Direction = _DBParameters.Item(i).Direction
                            .ParameterName = _DBParameters.Item(i).Name
                            .SqlDbType = _DBParameters.Item(i).DataType
                            .Value = _DBParameters.Item(i).Value
                        End With
                        oSQLCommand.Parameters.Add(OsqlParmeter)
                    Next

                    _Result = oSQLCommand.ExecuteScalar

                    If _Result Is Nothing Then
                        _Result = ""
                    End If

                    Return _Result
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsgloDataBase -- ExecuteScaler -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsgloDataBase -- ExecuteScaler -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If oSQLCommand IsNot Nothing Then
                        oSQLCommand.Parameters.Clear()
                        oSQLCommand.Dispose()
                        oSQLCommand = Nothing
                    End If
                    OsqlParmeter = Nothing
                End Try
            End Function

            Public Function ExecuteQueryScaler(ByVal SQLQuery As String) As String
                Dim oSQLCommand As SqlCommand = Nothing
                Dim _Result As String = ""
                Dim oResult As Object

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        ExecuteQueryScaler = Nothing
                        Exit Function
                    End If

                    oSQLCommand = New SqlCommand
                    'Work with database
                    oSQLCommand.CommandType = CommandType.Text 'SQL Command Type , is Store Procedure
                    oSQLCommand.CommandText = SQLQuery 'Set Store Procedure Name
                    oSQLCommand.Connection = gloDBConnection

                    oResult = oSQLCommand.ExecuteScalar

                    If oResult Is Nothing Then
                        _Result = ""
                    Else
                        If IsDBNull(oResult) Then
                            _Result = ""
                        Else
                            _Result = CType(oResult, String)
                        End If
                    End If

                    Return _Result
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsgloDataBase -- ExecuteQueryScaler -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsgloDataBase -- ExecuteQueryScaler -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                Finally
                    If oSQLCommand IsNot Nothing Then
                        oSQLCommand.Parameters.Clear()
                        oSQLCommand.Dispose()
                        oSQLCommand = Nothing
                    End If
                    oResult = Nothing
                    _Result = Nothing
                End Try
            End Function

            'Connections
            Public Function Connect(ByVal ConnectionStrings As String) As Boolean
                Try
                    If Trim(ConnectionStrings) <> "" Then
                        With gloDBConnection
                            If .State <> ConnectionState.Closed Then .Close()
                            .ConnectionString = ConnectionStrings
                            If .State = ConnectionState.Closed Then
                                .Open()
                            End If
                        End With
                        Return True
                    Else
                        _ErrorMessage = "Connection information is not valid"
                        Return False
                        Exit Function
                    End If
                Catch ex As SqlException
                    MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    UpdateLog("clsgloDataBase -- Connect -- " & ex.ToString)
                    Return Nothing
                Catch ex As Exception
                    UpdateLog("clsgloDataBase -- Connect -- " & ex.ToString)
                    MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return Nothing
                End Try
            End Function

            Public Function Disconnect() As Boolean
                Try
                    If gloDBConnection.State <> ConnectionState.Closed Then gloDBConnection.Close()
                Catch objError As Exception
                    _ErrorMessage = objError.Message
                    Disconnect = Nothing
                    Exit Function
                End Try
                Return Nothing
            End Function

            Public Property ErrorMessage() As String
                Get
                    Return _ErrorMessage
                End Get
                Set(ByVal Value As String)
                    _ErrorMessage = Value
                End Set
            End Property

            Public Sub New()
                MyBase.new()
                _DBParameters = New gloStream.gloDataBase.gloDBSupport.DBParameters
                bDBParameters = True
                gloDBConnection = New System.Data.SqlClient.SqlConnection
                bgloDBConnection = True
            End Sub

            Private disposed As Boolean = False

            Public Sub Dispose() Implements System.IDisposable.Dispose
                Dispose(True)
                GC.SuppressFinalize(Me)
            End Sub

            Protected Overridable Sub Dispose(disposing As Boolean)
                If Not Me.disposed Then
                    If disposing Then
                        If (bgloDBConnection) Then
                            If gloDBConnection IsNot Nothing Then
                                gloDBConnection.Dispose()
                                gloDBConnection = Nothing
                            End If
                            bgloDBConnection = False
                        End If
                       
                        _ErrorMessage = Nothing
                        If (bDBParameters) Then
                            If IsNothing(_DBParameters) = False Then
                                _DBParameters.Clear()
                                _DBParameters.Dispose()
                                _DBParameters = Nothing

                            End If
                            bDBParameters = False
                        End If

                    End If
                End If
                disposed = True
            End Sub

            Protected Overrides Sub Finalize()
                If (bDBParameters) Then
                    _DBParameters = Nothing


                    bDBParameters = False
                End If
                If (bgloDBConnection) Then
                    gloDBConnection = Nothing
                    bgloDBConnection = False
                End If


                MyBase.Finalize()
            End Sub

        End Class

        Namespace gloDBSupport

            Public Class DBParameter

                Private _Name As String
                Private _value As Object
                Private _Direction As System.Data.ParameterDirection
                Private _DataType As System.Data.SqlDbType

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

            'Parameters Collection
            Public Class DBParameters

                Implements System.Collections.IEnumerable
                Private mCol As Collection
                Public Sub Dispose()
                    If (IsNothing(mCol) = False) Then
                        mCol.Clear()
                        mCol = Nothing
                    End If
                End Sub
                Public Function Add(ByVal Name As String, ByVal Value As Object, ByVal Direction As System.Data.ParameterDirection, ByVal DataType As System.Data.SqlDbType, Optional ByVal Size As Integer = 0) As DBParameter
                    'create a new object
                    Try
                        If (IsNothing(mCol)) Then
                            mCol = New Collection
                        End If
                        Dim objNewMember As DBParameter
                        objNewMember = New DBParameter

                        'set the properties passed into the method
                        objNewMember.Name = Name
                        objNewMember.Value = Value
                        objNewMember.Direction = Direction
                        objNewMember.DataType = DataType

                        mCol.Add(objNewMember)

                        'return the object created
                        Add = objNewMember
                        'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1029"'
                        objNewMember = Nothing
                    Catch ex As Exception
                        UpdateLog("clsgloDataBase -- Add -- " & ex.ToString)
                        MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Add = Nothing
                    End Try
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As DBParameter
                    Get
                        If (IsNothing(mCol)) Then
                            Item = Nothing
                        Else
                            Item = mCol.Item(vntIndexKey)
                        End If

                    End Get
                End Property

                Public ReadOnly Property Count() As Integer
                    Get
                        If (IsNothing(mCol)) Then
                            Count = 0
                        Else
                            Count = mCol.Count()
                        End If
                    End Get
                End Property

                Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
                    'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
                    'GetEnumerator = mCol.GetEnumerator
                    Return Nothing
                End Function

                Public Sub Remove(ByRef vntIndexKey As Object)
                    If (IsNothing(mCol) = False) Then
                        'Dim thisDbParameter As DBParameter = DirectCast(mCol.Item(vntIndexKey), DBParameter)
                        mCol.Remove(vntIndexKey)
                        'Try
                        '    thisDbParameter = Nothing
                        'Catch ex As Exception

                        'End Try
                    End If
                End Sub

                Public Sub New()
                    MyBase.New()
                    mCol = New Collection
                End Sub

                Protected Overrides Sub Finalize()
                    Clear()
                    mCol = Nothing
                    MyBase.Finalize()
                End Sub

                Public Sub Clear()
                    If mCol Is Nothing Then Exit Sub ' Shouldn't happen, but just in case.

                    Dim i As Short
                    For i = mCol.Count() To 1 Step -1
                        mCol.Remove(i)
                    Next i
                End Sub
            End Class

        End Namespace

    End Namespace

End Namespace