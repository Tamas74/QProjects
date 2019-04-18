Imports System.Data.SqlClient

Namespace gloStream
    Namespace gloDataBase
        Public Class gloDataBase
            Private _ErrorMessage As String
            Private gloDBConnection As New System.Data.SqlClient.SqlConnection
            Private _DBParameters As gloStream.gloDataBase.gloDBSupport.DBParameters

            '-----------------
            'Properties
            Public Property DBParameters() As gloStream.gloDataBase.gloDBSupport.DBParameters
                Get
                    Return _DBParameters
                End Get
                Set(ByVal Value As gloStream.gloDataBase.gloDBSupport.DBParameters)
                    _DBParameters = Value
                End Set
            End Property

            'Methods

            Public Function ReadQueryRecordAsDataSet(ByVal SQLQuery As String) As DataSet
                ' Data Reader
                Dim oDS As New DataSet
                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        'sarika 25th june 07
                        Return Nothing
                        '-------------
                        Exit Function
                    End If

                    'Work with database
                    Dim oDataAdapter As New SqlDataAdapter(SQLQuery, gloDBConnection)
                    oDataAdapter.Fill(oDS)

                    Return oDS
                Catch objError As Exception
                    _ErrorMessage = objError.Message
                    'sarika 25th june 07
                    Return Nothing
                    '-------------
                    Exit Function
                End Try
            End Function


            Public Function ReadData(ByVal StoredProcedure As String) As DataTable
                Dim oSQLCommand As New SqlCommand                       ' SQL Command
                Dim OsqlParmeter As SqlParameter
                Dim i As Integer

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        'sarika 25th june 07
                        Return Nothing
                        '-------------------------
                        Exit Function
                    End If

                    'Work with database
                    oSQLCommand.CommandType = CommandType.StoredProcedure   ' SQL Command Type , is Store Procedure
                    oSQLCommand.CommandText = StoredProcedure          ' Set Store Procedure Name
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
                    Dim dsData As New DataSet
                    objDA.Fill(dsData)

                    Return dsData.Tables(0)
                Catch objError As Exception
                    _ErrorMessage = objError.Message
                    'sarika 25th june 07
                    Return Nothing
                    '-------------------------
                    Exit Function
                Finally
                    oSQLCommand = Nothing
                    OsqlParmeter = Nothing
                End Try
            End Function

            Public Function ReadQueryDataTable(ByVal SQLQuery As String) As DataTable
                ' Data Table
                Dim oDT As New DataTable
                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        'sarika 25th june 07
                        Return Nothing
                        '-------------------------
                        Exit Function
                    End If

                    'Work with database
                    Dim oDataAdapter As New SqlDataAdapter(SQLQuery, gloDBConnection)
                    oDataAdapter.Fill(oDT)

                    Return oDT
                Catch objError As Exception
                    _ErrorMessage = objError.Message
                    'sarika 25th june 07
                    Return Nothing
                    '-------------------------
                    Exit Function
                End Try
            End Function

            Public Function ReadRecords(ByVal StoredProcedure As String) As System.Data.SqlClient.SqlDataReader
                Dim oSQLCommand As New SqlCommand                       ' SQL Command
                Dim oDataReader As SqlDataReader                        ' Data Reader
                Dim OsqlParmeter As SqlParameter
                Dim i As Integer

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        'sarika 25th june 07
                        Return Nothing
                        '-------------------------
                        Exit Function
                    End If

                    'Work with database
                    oSQLCommand.CommandType = CommandType.StoredProcedure   ' SQL Command Type , is Store Procedure
                    oSQLCommand.CommandText = StoredProcedure          ' Set Store Procedure Name
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
                Catch objError As Exception
                    _ErrorMessage = objError.Message
                    'sarika 25th june 07
                    Return Nothing
                    '-------------------------
                    Exit Function
                Finally
                    oSQLCommand = Nothing
                    OsqlParmeter = Nothing
                End Try
            End Function

            Public Function ReadQueryRecords(ByVal SQLQuery As String) As System.Data.SqlClient.SqlDataReader
                Dim oSQLCommand As New SqlCommand                       ' SQL Command
                Dim oDataReader As SqlDataReader                        ' Data Reader

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        'sarika 25th june 07
                        Return Nothing
                        '-------------------------
                        Exit Function
                    End If
                    If Trim(SQLQuery) = "" Then
                        _ErrorMessage = "Please select query description"
                        'sarika 25th june 07
                        Return Nothing
                        '-------------------------
                        Exit Function
                    End If

                    'Work with database
                    oSQLCommand.CommandType = CommandType.Text   ' SQL Command Type , is Store Procedure
                    oSQLCommand.CommandText = SQLQuery           ' Set Store Procedure Name
                    oSQLCommand.Connection = gloDBConnection
                    oDataReader = oSQLCommand.ExecuteReader
                    Return oDataReader
                Catch objError As Exception
                    _ErrorMessage = objError.Message
                    'sarika 25th june 07
                    Return Nothing
                    '-------------------------
                    Exit Function
                Finally
                    oSQLCommand = Nothing
                End Try
            End Function
            Public Function ReadQueryData(ByVal SQLQuery As String) As DataTable
                Dim oSQLCommand As New SqlCommand                       ' SQL Command

                Dim i As Integer

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        Exit Function
                    End If

                    'Work with database
                    oSQLCommand.CommandType = CommandType.Text ' SQL Command Type , is Store Procedure
                    oSQLCommand.CommandText = SQLQuery          ' Set Store Procedure Name
                    oSQLCommand.Connection = gloDBConnection
                    'oSQLCommand.CommandTimeout = 0

                    Dim objDA As New SqlDataAdapter(oSQLCommand)
                    Dim dsData As New DataSet
                    objDA.Fill(dsData)

                    Return dsData.Tables(0)
                Catch objError As Exception
                    _ErrorMessage = objError.Message
                    Exit Function
                Finally
                    oSQLCommand = Nothing

                End Try
            End Function

            Public Function FindDifferenceTable(ByVal findTable As DataTable, ByVal containertable As DataTable, ByVal FindFieldName As String, ByVal FieldDataTypeIsNumeric As Boolean, Optional ByVal FindFieldName1 As String = "", Optional ByVal Field1DataTypeIsNumeric As Boolean = False) As DataTable
                Try
                    Dim nCount As Integer
                    Dim dtDifferenece As New DataTable
                    dtDifferenece = findTable.Clone
                    Dim dr As DataRow
                    Dim nCount1 As Int16
                    If Trim(FindFieldName1) = "" Then
                        If FieldDataTypeIsNumeric = True Then
                            For nCount = 0 To findTable.Rows.Count - 1
                                If containertable.Select(FindFieldName & "=" & findTable.Rows(nCount).Item(FindFieldName)).GetUpperBound(0) = -1 Then
                                    dr = dtDifferenece.NewRow
                                    For nCount1 = 0 To dtDifferenece.Columns.Count - 1
                                        dr(nCount1) = findTable.Rows(nCount).Item(nCount1)
                                    Next
                                    dtDifferenece.Rows.Add(dr)
                                End If
                            Next
                        Else
                            For nCount = 0 To findTable.Rows.Count - 1
                                If containertable.Select(FindFieldName & "= '" & findTable.Rows(nCount).Item(FindFieldName) & "'").GetUpperBound(0) = -1 Then
                                    dr = dtDifferenece.NewRow
                                    For nCount1 = 0 To dtDifferenece.Columns.Count - 1
                                        dr(nCount1) = findTable.Rows(nCount).Item(nCount1)
                                    Next
                                    dtDifferenece.Rows.Add(dr)
                                End If
                            Next
                        End If
                    Else
                        Select Case FieldDataTypeIsNumeric
                            Case True
                                Select Case Field1DataTypeIsNumeric
                                    Case True

                                    Case False
                                        Dim strCondition As String
                                        For nCount = 0 To findTable.Rows.Count - 1
                                            '//And condition in single string//- If containertable.Select(FindFieldName & "=" & findTable.Rows(nCount).Item(FindFieldName) & " And " & FindFieldName1 & "= '" & findTable.Rows(nCount).Item(FindFieldName1) & "'").GetUpperBound(0) = -1 Then
                                            '//Actual Condition with And Sign - If containertable.Select(FindFieldName & "=" & findTable.Rows(nCount).Item(FindFieldName)).GetUpperBound(0) = -1 And containertable.Select(FindFieldName1 & "= '" & findTable.Rows(nCount).Item(FindFieldName1) & "'").GetUpperBound(0) = -1 Then
                                            '// Actual Single Condition//- If containertable.Select(FindFieldName & "=" & findTable.Rows(nCount).Item(FindFieldName)).GetUpperBound(0) = -1 Then
                                            'If containertable.Select(FindFieldName & "=" & findTable.Rows(nCount).Item(FindFieldName)).GetUpperBound(0) = -1 And containertable.Select(FindFieldName1 & "= '" & findTable.Rows(nCount).Item(FindFieldName1) & "'").GetUpperBound(0) = -1 Then
                                            strCondition = FindFieldName & "=" & findTable.Rows(nCount).Item(FindFieldName) & " AND " & FindFieldName1 & " = '" & findTable.Rows(nCount).Item(FindFieldName1) & "'"
                                            If containertable.Select(strCondition).GetUpperBound(0) = -1 Then
                                                dr = dtDifferenece.NewRow
                                                For nCount1 = 0 To dtDifferenece.Columns.Count - 1
                                                    dr(nCount1) = findTable.Rows(nCount).Item(nCount1)
                                                Next
                                                dtDifferenece.Rows.Add(dr)
                                            End If
                                        Next
                                End Select
                            Case False
                                Select Case Field1DataTypeIsNumeric
                                    Case True

                                    Case False

                                End Select
                        End Select
                    End If

                    Return dtDifferenece
                Catch oError As Exception
                    _ErrorMessage = oError.Message
                End Try

            End Function

            Public Function ReadQueryRecord(ByVal SQLQuery As String) As Boolean
                Dim oSQLCommand As New SqlCommand                       ' SQL Command
                '   Dim oDataReader As SqlDataReader                ' Data Reader

                Dim _blnResult As Boolean = False
                Dim _RecordAffected As Integer = 0
                '   Dim i As Integer

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        Exit Function
                    End If

                    'Work with database
                    oSQLCommand.CommandType = CommandType.Text   ' SQL Command Type , is Store Procedure
                    oSQLCommand.CommandText = SQLQuery           ' Set Store Procedure Name
                    oSQLCommand.Connection = gloDBConnection


                    _RecordAffected = oSQLCommand.ExecuteScalar

                    If _RecordAffected > 0 Then
                        _blnResult = True
                    End If

                    Return _blnResult
                Catch objError As Exception
                    _ErrorMessage = objError.Message
                    Exit Function
                Finally
                    oSQLCommand = Nothing
                End Try
            End Function

            Public Function CommonRecordsInDMS(ByVal findTable As DataTable, ByVal containertable As DataTable, Optional ByVal strFieldName As String = "", Optional ByVal FieldDataTypeIsNumeric As Boolean = False) As DataTable
                Dim nCount As Integer
                Dim dtCommonRecords As New DataTable
                dtCommonRecords = findTable.Clone
                Dim dr As DataRow
                Dim nCount1 As Int16
                Dim strCondition As String
                For nCount = 0 To findTable.Rows.Count - 1
                    strCondition = "DocumentFileName=" & findTable.Rows(nCount).Item("DocumentFileName")
                    If strFieldName <> "" Then
                        If FieldDataTypeIsNumeric Then
                            strCondition = strCondition & " AND " & strFieldName & "=" & findTable.Rows(nCount).Item(strFieldName)
                        Else
                            strCondition = strCondition & " AND " & strFieldName & "='" & findTable.Rows(nCount).Item(strFieldName) & "'"
                        End If

                    End If
                    If containertable.Select(strCondition).GetUpperBound(0) >= 0 Then
                        dr = dtCommonRecords.NewRow
                        For nCount1 = 0 To dtCommonRecords.Columns.Count - 1
                            dr(nCount1) = findTable.Rows(nCount).Item(nCount1)
                        Next
                        dtCommonRecords.Rows.Add(dr)
                    End If
                Next
                Return dtCommonRecords
            End Function



            Public Function ReadCatRecords(ByVal SQLQuery As String) As System.Data.DataSet
                Dim oSQLCommand As New SqlCommand                       ' SQL Command
                Dim oDataSet As New DataSet
                Dim OsqlDataAdp As New SqlDataAdapter

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        'sarika 25th june 07
                        Return Nothing
                        '--------------
                        Exit Function

                    End If
                    If Trim(SQLQuery) = "" Then
                        _ErrorMessage = "Please select query description"
                        'sarika 25th june 07
                        Return Nothing
                        '--------------
                        Exit Function
                    End If

                    'Work with database
                    oSQLCommand.CommandType = CommandType.Text   ' SQL Command Type , is Store Procedure
                    oSQLCommand.CommandText = SQLQuery           ' Set Store Procedure Name
                    oSQLCommand.Connection = gloDBConnection
                    OsqlDataAdp.SelectCommand = oSQLCommand
                    OsqlDataAdp.Fill(oDataSet)


                    'oDataReader = oSQLCommand.ExecuteReader

                    Return oDataSet
                Catch objError As Exception
                    _ErrorMessage = objError.Message
                    'sarika 25th june 07
                    Return Nothing
                    '--------------
                    Exit Function
                Finally
                    oSQLCommand = Nothing
                End Try
            End Function
            '------------------------

            '------------------------
            Public Function ExecuteNonQuery(ByVal StoredProcedure As String) As Boolean
                Dim oSQLCommand As New SqlCommand                       ' SQL Command
                'Dim oDataReader As SqlDataReader                        ' Data Reader
                Dim OsqlParmeter As SqlParameter
                Dim _blnResult As Boolean = False
                Dim _RecordAffected As Integer = 0
                Dim i As Integer

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        Exit Function
                    End If

                    'Work with database
                    oSQLCommand.CommandType = CommandType.StoredProcedure   ' SQL Command Type , is Store Procedure
                    oSQLCommand.CommandText = StoredProcedure          ' Set Store Procedure Name
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
                Catch objError As Exception
                    _ErrorMessage = objError.Message
                    Exit Function
                Finally
                    oSQLCommand = Nothing
                    OsqlParmeter = Nothing
                End Try
            End Function

            Public Function ExecuteQueryNonQuery(ByVal SQLQuery As String) As Boolean
                Dim oSQLCommand As New SqlCommand                       ' SQL Command
                Dim _blnResult As Boolean = False
                Dim _RecordAffected As Integer = 0
                '  Dim i As Integer

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        Exit Function
                    End If

                    'Work with database
                    oSQLCommand.CommandType = CommandType.Text
                    oSQLCommand.CommandText = SQLQuery
                    oSQLCommand.Connection = gloDBConnection

                    _RecordAffected = oSQLCommand.ExecuteNonQuery()

                    If _RecordAffected > 0 Then
                        _blnResult = True
                    End If

                    Return _blnResult
                Catch objError As Exception
                    _ErrorMessage = objError.Message
                    Exit Function
                Finally
                    oSQLCommand = Nothing
                End Try
            End Function

            'Function to return the value from stored proce ...now it is used only in datacatcher added on 24/08/2006
            Public Function ExecuteNonQueryVal(ByVal StoredProcedure As String, ByRef intPatientId As Long) As Boolean
                Dim oSQLCommand As New SqlCommand
                'Sql(Command)
                'sarika 25th june 07
                'Dim OsqlParmeter As SqlParameter
                Dim OsqlParmeter As SqlParameter = Nothing
                '-----------------
                Dim _Result As String = ""
                Dim i As Integer

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        Exit Function
                    End If

                    'Work with database
                    oSQLCommand.CommandType = CommandType.StoredProcedure   ' SQL Command Type , is Store Procedure
                    oSQLCommand.CommandText = StoredProcedure          ' Set Store Procedure Name
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
                        'OsqlParmeter = Nothing
                    Next

                    oSQLCommand.ExecuteNonQuery()
                    If OsqlParmeter.Value <> 0 Then
                        intPatientId = OsqlParmeter.Value
                    End If
                    Return True
                Catch ex As SqlException
                    MsgBox(ex.Message)
                Catch objError As Exception
                    _ErrorMessage = objError.Message
                    Exit Function
                Finally
                    oSQLCommand = Nothing
                    OsqlParmeter = Nothing
                End Try
            End Function
            '----------------------

            Public Function ExecuteNonSQLQuery(ByVal SQLQuery As String) As Boolean
                Dim oSQLCommand As New SqlCommand                       ' SQL Command
                ''sarika 25th june 07
                ''Dim oDataReader As SqlDataReader                        ' Data Reader
                'Dim oDataReader As SqlDataReader = Nothing
                ''---------
                Dim OsqlParmeter As SqlParameter
                Dim _blnResult As Boolean = False
                Dim _RecordAffected As Integer = 0
                Dim i As Integer

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connecion"
                        Exit Function
                    End If

                    'Work with database
                    oSQLCommand.CommandType = CommandType.Text   ' SQL Command Type , is Store Procedure
                    oSQLCommand.CommandText = SQLQuery          ' Set Store Procedure Name
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
                Catch objError As Exception
                    _ErrorMessage = objError.Message
                    Exit Function
                Finally
                    oSQLCommand = Nothing
                    OsqlParmeter = Nothing
                End Try
            End Function

            '----------------------
            Public Function ExecuteNonQueryForOutput(ByVal StoredProcedure As String) As Long
                Dim ID As Long = 0
                Dim oSQLCommand As New SqlCommand                       ' SQL Command
                'sarika 25th june 07
                'Dim oDataReader As SqlDataReader                        ' Data Reader
                Dim OsqlParmeter As SqlParameter = Nothing
                '-------
                Dim _blnResult As Boolean = False
                Dim _RecordAffected As Integer = 0
                Dim i As Integer

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connection"
                        Exit Function
                    End If

                    'Work with database
                    oSQLCommand.CommandType = CommandType.StoredProcedure   ' SQL Command Type , is Store Procedure
                    oSQLCommand.CommandText = StoredProcedure          ' Set Store Procedure Name
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
                Catch objError As Exception
                    _ErrorMessage = objError.Message
                    Exit Function
                Finally
                    oSQLCommand = Nothing
                    OsqlParmeter = Nothing
                End Try
            End Function
            Public Function ExecuteScaler(ByVal StoredProcedure As String) As String
                Dim oSQLCommand As New SqlCommand                       ' SQL Command
                Dim OsqlParmeter As SqlParameter
                Dim _Result As String = ""
                Dim i As Integer

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connection"
                        'sarika 25th  june 07
                        Return _Result
                        '-----------
                        Exit Function
                    End If

                    'Work with database
                    oSQLCommand.CommandType = CommandType.StoredProcedure   ' SQL Command Type , is Store Procedure
                    oSQLCommand.CommandText = StoredProcedure          ' Set Store Procedure Name
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

                    Return _Result
                Catch objError As Exception
                    _ErrorMessage = objError.Message
                    'sarika 25th  june 07
                    Return ""
                    '-----------
                    Exit Function
                Finally
                    oSQLCommand = Nothing
                    OsqlParmeter = Nothing
                End Try
            End Function

            Public Function ExecuteQueryScaler(ByVal SQLQuery As String) As String
                Dim oSQLCommand As New SqlCommand                       ' SQL Command
                Dim _Result As String = ""
                '  Dim i As Integer

                Try
                    'Check Connection
                    If gloDBConnection.State = ConnectionState.Closed Then
                        _ErrorMessage = "Please check database connection"
                        'sarika 25th  june 07
                        Return ""
                        '-----------
                        Exit Function
                    End If

                    'Work with database
                    oSQLCommand.CommandType = CommandType.Text   ' SQL Command Type , is Store Procedure
                    oSQLCommand.CommandText = SQLQuery          ' Set Store Procedure Name
                    oSQLCommand.Connection = gloDBConnection

                    _Result = oSQLCommand.ExecuteScalar

                    If _Result Is Nothing Then
                        _Result = ""
                    End If

                    Return _Result
                Catch objError As Exception
                    _ErrorMessage = objError.Message
                    'sarika 25th  june 07
                    Return ""
                    '-----------
                    Exit Function
                Finally
                    oSQLCommand = Nothing
                End Try
            End Function

            '------------------------



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
                Catch objEror As Exception
                    _ErrorMessage = objEror.Message
                    Exit Function
                End Try
            End Function

            Public Function Disconnect() As Boolean
                Try
                    If gloDBConnection.State <> ConnectionState.Closed Then gloDBConnection.Close()
                Catch objError As Exception
                    _ErrorMessage = objError.Message
                    Exit Function
                End Try
            End Function
            '------------------------
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
            End Sub

            Protected Overrides Sub Finalize()
                _DBParameters = Nothing
                MyBase.Finalize()
            End Sub

            '---Sachin
            'Dim paraCollection As New gloParameters

            'Public Sub setPrameter(ByVal ParameterName As String, ByVal Value As Object, ByVal Direction As ParameterDirection, ByVal SqlDbType As SqlDbType)
            '    para.ParameterName = ParameterName
            '    para.Value = Value
            '    para.Direction = Direction
            '    para.SqlDbType = SqlDbType

            '    paraCollection.Add(para)
            'End Sub

            'Public Function ExecureProcedure(ByVal SpName As String) As Boolean
            '    Dim cmd As New SqlCommand
            '    'This will store out put of cmd.ExecuteNonQuery
            '    Dim intResult As Int16

            '    'Dim SpParameter Asparameter
            '    Dim i As Integer
            '    Try
            '        If getConnect(_ConnectionString) = False Then
            '            MsgBox("Problem in Database connection")
            '            Exit Function
            '        End If

            '        cmd.Connection = cn
            '        cmd.CommandType = CommandType.StoredProcedure
            '        cmd.CommandText = Trim(SpName)

            '        With paraCollection
            '            For i = 1 To .count
            '                Dim para As New SqlParameter

            '                para.ParameterName = .Item(i).ParameterName
            '                para.Direction = .Item(i).Direction
            '                para.SqlDbType = .Item(i).SqlDbType
            '                para.Value = .Item(i).Value

            '                cmd.Parameters.Add(para)
            '                'MsgBox(.Item(i).Direction.ToString & " " & .Item(i).ParameterName.ToString & " " & .Item(i).SqlDbType.ToString & " " & .Item(i).Value.ToString)
            '            Next
            '            cmd.ExecuteNonQuery()
            '        End With
            '    Catch ex As Exception
            '        MsgBox(ex.Message, MsgBoxStyle.Critical)
            '    End Try
            'End Function
        End Class


        Namespace gloDBSupport
            'Parameter
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

                Public Function Add(ByVal Name As String, ByVal Value As Object, ByVal Direction As System.Data.ParameterDirection, ByVal DataType As System.Data.SqlDbType, Optional ByVal Size As Integer = 0) As DBParameter
                    'create a new object
                    Dim objNewMember As DBParameter
                    objNewMember = New DBParameter

                    'set the properties passed into the method
                    objNewMember.Name = Name
                    objNewMember.Value = Value
                    objNewMember.Direction = Direction
                    objNewMember.DataType = DataType
                   
                    'If Len(sKey) = 0 Then
                    mCol.Add(objNewMember)
                    'Else
                    '    mCol.Add objNewMember, sKey
                    'End If


                    'return the object created
                    Add = objNewMember
                    'UPGRADE_NOTE: Object objNewMember may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1029"'
                    objNewMember = Nothing
                End Function

                Default Public ReadOnly Property Item(ByVal vntIndexKey As Object) As DBParameter
                    Get
                        Item = mCol.Item(vntIndexKey)
                    End Get
                End Property

                Public ReadOnly Property Count() As Integer
                    Get
                        Count = mCol.Count()
                    End Get
                End Property

                Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
                    'UPGRADE_TODO: Uncomment and change the following line to return the collection enumerator. Click for more: 'ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1055"'
                    'GetEnumerator = mCol.GetEnumerator

                    'sarika 3rd july 07
                    Return Nothing

                End Function

                Public Sub Remove(ByRef vntIndexKey As Object)
                    mCol.Remove(vntIndexKey)
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