Imports System.Data.SqlClient
Public Class gloDataBase
    Private _ErrorMessage As String
    Private gloDBConnection As New System.Data.SqlClient.SqlConnection
    Private _DBParameters As DBParameters

    '-----------------
    'Properties
    Public Property DBParameters() As DBParameters
        Get
            Return _DBParameters
        End Get
        Set(ByVal Value As DBParameters)
            _DBParameters = Value
        End Set
    End Property

    'Methods
    'Public Function ReadRecords(ByVal StoredProcedure As String) As System.Data.SqlClient.SqlDataReader
    '    Dim oSQLCommand As New SqlCommand                       ' SQL Command
    '    Dim oDataReader As SqlDataReader                        ' Data Reader
    '    Dim OsqlParmeter As SqlParameter
    '    Dim i As Integer

    '    Try
    '        'Check Connection
    '        If gloDBConnection.State = ConnectionState.Closed Then
    '            _ErrorMessage = "Please check database connecion"
    '            Exit Function
    '        End If

    '        'Work with database
    '        oSQLCommand.CommandType = CommandType.StoredProcedure   ' SQL Command Type , is Store Procedure
    '        oSQLCommand.CommandText = StoredProcedure          ' Set Store Procedure Name
    '        oSQLCommand.Connection = gloDBConnection


    '        For i = 1 To _DBParameters.Count
    '            OsqlParmeter = New SqlParameter
    '            With OsqlParmeter
    '                .Direction = _DBParameters.Item(i).Direction
    '                .ParameterName = _DBParameters.Item(i).Name
    '                .SqlDbType = _DBParameters.Item(i).DataType
    '                .Value = _DBParameters.Item(i).Value
    '            End With
    '            oSQLCommand.Parameters.Add(OsqlParmeter)
    '        Next

    '        oDataReader = oSQLCommand.ExecuteReader

    '        Return oDataReader
    '    Catch objError As Exception
    '        _ErrorMessage = objError.Message
    '        Exit Function
    '    Finally
    '        oSQLCommand = Nothing
    '        OsqlParmeter = Nothing
    '    End Try
    'End Function
    'Public Function ReadRecordsInDT(ByVal StoredProcedure As String) As System.Data.DataTable
    '    Dim oSQLCommand As New SqlCommand                       ' SQL Command
    '    ' Dim oDataAdapter As SqlDataAdapter                         ' Data Reader
    '    Dim OsqlParmeter As SqlParameter
    '    Dim oDT As New DataTable
    '    Dim i As Integer

    '    Try
    '        'Check Connection
    '        If gloDBConnection.State = ConnectionState.Closed Then
    '            _ErrorMessage = "Please check database connecion"
    '            Exit Function
    '        End If

    '        'Work with database
    '        oSQLCommand.CommandType = CommandType.StoredProcedure   ' SQL Command Type , is Store Procedure
    '        oSQLCommand.CommandText = StoredProcedure          ' Set Store Procedure Name
    '        oSQLCommand.Connection = gloDBConnection

    '        For i = 1 To _DBParameters.Count
    '            OsqlParmeter = New SqlParameter
    '            With OsqlParmeter
    '                .Direction = _DBParameters.Item(i).Direction
    '                .ParameterName = _DBParameters.Item(i).Name
    '                .SqlDbType = _DBParameters.Item(i).DataType
    '                .Value = _DBParameters.Item(i).Value
    '            End With
    '            oSQLCommand.Parameters.Add(OsqlParmeter)
    '        Next

    '        'Work with database

    '        Dim oDataAdapter As New SqlDataAdapter(oSQLCommand)
    '        oDataAdapter.Fill(oDT)

    '        Return oDT
    '    Catch objError As Exception
    '        _ErrorMessage = objError.Message
    '        Exit Function
    '    Finally
    '        oSQLCommand = Nothing
    '        OsqlParmeter = Nothing
    '    End Try
    'End Function

    Public Function ReadQueryRecords(ByVal SQLQuery As String) As System.Data.SqlClient.SqlDataReader
        Dim oSQLCommand As SqlCommand = Nothing                       ' SQL Command
        Dim oDataReader As SqlDataReader                        ' Data Reader

        Try
            'Check Connection
            If gloDBConnection.State = ConnectionState.Closed Then
                _ErrorMessage = "Please check database connecion"
                Return Nothing
                Exit Function
            End If
            If Trim(SQLQuery) = "" Then
                _ErrorMessage = "Please select query description"
                Return Nothing
                Exit Function
            End If
            oSQLCommand = New SqlCommand
            'Work with database
            oSQLCommand.CommandType = CommandType.Text   ' SQL Command Type , is Store Procedure
            oSQLCommand.CommandText = SQLQuery           ' Set Store Procedure Name
            oSQLCommand.Connection = gloDBConnection
            oDataReader = oSQLCommand.ExecuteReader
            Return oDataReader
        Catch objError As Exception
            _ErrorMessage = objError.Message
            Return Nothing
            Exit Function
        Finally
            If oSQLCommand IsNot Nothing Then
                oSQLCommand.Parameters.Clear()
                oSQLCommand.Dispose()
                oSQLCommand = Nothing
            End If
        End Try
    End Function

    '------------------------
    Public Function ExecuteNonQuery(ByVal StoredProcedure As String) As Boolean
        Dim oSQLCommand As SqlCommand = Nothing                 ' SQL Command
        Dim OsqlParmeter As SqlParameter = Nothing
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

            _RecordAffected = oSQLCommand.ExecuteNonQuery

            If _RecordAffected > 0 Then
                _blnResult = True
            End If

            Return _blnResult
        Catch objError As Exception
            _ErrorMessage = objError.Message
            ExecuteNonQuery = Nothing
            Exit Function
        Finally
            If oSQLCommand IsNot Nothing Then
                oSQLCommand.Parameters.Clear()
                oSQLCommand.Dispose()
                oSQLCommand = Nothing
            End If
            If OsqlParmeter IsNot Nothing Then
                OsqlParmeter = Nothing
            End If

        End Try
    End Function


    Public Function ExecuteNonSQLQuery(ByVal SQLQuery As String) As Boolean

        Dim _blnResult As Boolean = False
        Dim _RecordAffected As Integer = 0
        Try
            'Check Connection
            If gloDBConnection.State = ConnectionState.Closed Then
                _ErrorMessage = "Please check database connecion"
                ExecuteNonSQLQuery = Nothing
                Exit Function
            End If
            Dim oSQLCommand As New SqlCommand                       ' SQL Command
            'Work with database
            oSQLCommand.CommandType = CommandType.Text   ' SQL Command Type , is Store Procedure
            oSQLCommand.CommandText = SQLQuery           ' Set Store Procedure Name
            oSQLCommand.Connection = gloDBConnection



            _RecordAffected = oSQLCommand.ExecuteNonQuery

            If _RecordAffected > 0 Then
                _blnResult = True
            End If
            If oSQLCommand IsNot Nothing Then
                oSQLCommand.Parameters.Clear()
                oSQLCommand.Dispose()
                oSQLCommand = Nothing
            End If

            Return _blnResult
        Catch objError As Exception
            _ErrorMessage = objError.Message
            ExecuteNonSQLQuery = Nothing
            Exit Function
        Finally
           
        End Try
    End Function
    '----------------------

    Public Function ExecuteScaler(ByVal StoredProcedure As String) As String
        Dim oSQLCommand As SqlCommand = Nothing                       ' SQL Command
        Dim OsqlParmeter As SqlParameter = Nothing
        Dim _Result As String = ""
        Dim i As Integer

        Try
            'Check Connection
            If gloDBConnection.State = ConnectionState.Closed Then
                _ErrorMessage = "Please check database connecion"
                Return Nothing
                Exit Function
            End If
            oSQLCommand = New SqlCommand
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
            Return Nothing
            Exit Function
        Finally
            If oSQLCommand IsNot Nothing Then
                oSQLCommand.Parameters.Clear()
                oSQLCommand.Dispose()
                oSQLCommand = Nothing
            End If
            If OsqlParmeter IsNot Nothing Then
                OsqlParmeter = Nothing
            End If
        End Try
    End Function
    Public Function ExecuteNonQuery(ByVal StoredProcedure As String, ByRef intPatientId As Int64) As Boolean
        Dim oSQLCommand As SqlCommand = Nothing                     ' SQL Command
        Dim OsqlParmeter As SqlParameter = Nothing
        Dim oPatientIDSqlParameter As SqlParameter = Nothing
        Dim _Result As String = ""
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
            oSQLCommand.CommandType = CommandType.StoredProcedure   ' SQL Command Type , is Store Procedure
            oSQLCommand.CommandText = StoredProcedure          ' Set Store Procedure Name
            oSQLCommand.Connection = gloDBConnection


            For i = 1 To _DBParameters.Count
                If i = 1 Then
                    oPatientIDSqlParameter = New SqlParameter
                    With oPatientIDSqlParameter
                        .Direction = _DBParameters.Item(i).Direction
                        .ParameterName = _DBParameters.Item(i).Name
                        .SqlDbType = _DBParameters.Item(i).DataType
                        .Value = _DBParameters.Item(i).Value
                    End With
                    oSQLCommand.Parameters.Add(oPatientIDSqlParameter)
                    'oPatientIDSqlParameter = Nothing
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

            oSQLCommand.ExecuteNonQuery()
            If oPatientIDSqlParameter.Value <> 0 Then
                intPatientId = oPatientIDSqlParameter.Value
            End If
            If intPatientId > 0 Then
                Return True
            Else
                Return False
            End If
        Catch objError As Exception
            _ErrorMessage = objError.Message
            ExecuteNonQuery = Nothing
            Exit Function
        Finally
            If oSQLCommand IsNot Nothing Then
                oSQLCommand.Parameters.Clear()
                oSQLCommand.Dispose()
                oSQLCommand = Nothing
            End If
            If OsqlParmeter IsNot Nothing Then
                OsqlParmeter = Nothing
            End If
            If oPatientIDSqlParameter IsNot Nothing Then
                oPatientIDSqlParameter = Nothing
            End If
        End Try
    End Function
    Public Function ExecuteQueryScaler(ByVal SQLQuery As String) As String

        Dim _Result As String = ""

        Try
            'Check Connection
            If gloDBConnection.State = ConnectionState.Closed Then
                _ErrorMessage = "Please check database connecion"
                Return _Result
                Exit Function
            End If
            Dim oSQLCommand As New SqlCommand                       ' SQL Command
            'Work with database
            oSQLCommand.CommandType = CommandType.Text   ' SQL Command Type , is Store Procedure
            oSQLCommand.CommandText = SQLQuery          ' Set Store Procedure Name
            oSQLCommand.Connection = gloDBConnection

            _Result = oSQLCommand.ExecuteScalar & ""
            If oSQLCommand IsNot Nothing Then
                oSQLCommand.Parameters.Clear()
                oSQLCommand.Dispose()
                oSQLCommand = Nothing
            End If
            Return _Result
        Catch objError As Exception
            _ErrorMessage = objError.Message
            Return _Result
            Exit Function
        Finally
           
        End Try
    End Function
    Public Function ReadQueryRecord(ByVal SQLQuery As String) As Boolean

        Dim _blnResult As Boolean = False
        Dim _RecordAffected As Integer = 0
        Try
            'Check Connection
            If gloDBConnection.State = ConnectionState.Closed Then
                _ErrorMessage = "Please check database connecion"
                ReadQueryRecord = Nothing
                Exit Function
            End If
            Dim oSQLCommand As New SqlCommand                       ' SQL Command
            'Work with database
            oSQLCommand.CommandType = CommandType.Text   ' SQL Command Type , is Store Procedure
            oSQLCommand.CommandText = SQLQuery           ' Set Store Procedure Name
            oSQLCommand.Connection = gloDBConnection
            _RecordAffected = oSQLCommand.ExecuteScalar

            If _RecordAffected > 0 Then
                _blnResult = True
            End If
            If oSQLCommand IsNot Nothing Then
                oSQLCommand.Parameters.Clear()
                oSQLCommand.Dispose()
                oSQLCommand = Nothing
            End If
            Return _blnResult
        Catch objError As Exception
            _ErrorMessage = objError.Message
            ReadQueryRecord = Nothing
            Exit Function
        Finally
            
        End Try
    End Function

    'Methods
    Public Function ReadPatRecord(ByVal SQLquery As String) As DataSet
        ' Data Reader

        Try
            'Check Connection
            If gloDBConnection.State = ConnectionState.Closed Then
                _ErrorMessage = "Please check database connecion"
                Return Nothing
                Exit Function
            End If
            Dim oDS As New DataSet
            'Work with database
            Dim oDataAdapter As New SqlDataAdapter(SQLquery, gloDBConnection)
            oDataAdapter.Fill(oDS)
            oDataAdapter.Dispose()
            oDataAdapter = Nothing
            Return oDS
        Catch objError As Exception
            _ErrorMessage = objError.Message
            Return Nothing
            Exit Function
        Finally

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
            Connect = False
            Exit Function
        End Try
    End Function

    Public Function Disconnect() As Boolean
        Try
            If gloDBConnection.State <> ConnectionState.Closed Then gloDBConnection.Close()
            Return True
        Catch objError As Exception
            _ErrorMessage = objError.Message
            Disconnect = False
            Exit Function
        End Try
    End Function
    '------------------------


    Private Shared Function GetConnectionString(ByVal strSQLServerName As String, ByVal strDatabase As String, ByVal WindowsAuthentication As Boolean, Optional ByVal strUserName As String = "", Optional ByVal strPassword As String = "") As String
        ' Variable to store SQL Connection String
        Dim strConnectionString As String

        'Check the SQL Server Authentication
        If WindowsAuthentication = True Then
            'Build Connection String by Windows Authentication
            strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";Integrated Security=SSPI"
        Else
            'Build Connection String by SQL Server Authentication
            strConnectionString = "SERVER=" & strSQLServerName & ";UID=" & strUserName & ";PWD=" & strPassword & ";DATABASE=" & strDatabase
        End If

        'Return Builded connection string
        Return strConnectionString
    End Function

    Public Shared Function IsConnect(ByVal strSQLServerName As String, ByVal strDatabase As String, Optional ByVal WindowsAuthentication As Boolean = False, Optional ByVal strUserName As String = "", Optional ByVal strPassword As String = "") As Boolean
        'Create the object of SQL Connection class
        Dim objCon As New SqlConnection
        Try
            'Assign the connection string
            Dim strcon As String = ""
            strcon = GetConnectionString(strSQLServerName, strDatabase, WindowsAuthentication, strUserName, strPassword)
            objCon.ConnectionString = strcon
            'Open the connection
            objCon.Open()
            'Connection to SQL Server database successfully established
            Return True
        Catch ex As Exception
            'gloCCDGeneral.UpdateLog(ex.ToString)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex, False)
            Return False
        Finally
            'Close the  connection
            objCon.Close()
            'Connection to SQL Server database is not established
            objCon.Dispose()
            objCon = Nothing
        End Try

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
        _DBParameters = New DBParameters
    End Sub

    Protected Overrides Sub Finalize()
        _DBParameters = Nothing
        If (IsNothing(gloDBConnection) = False) Then
            If gloDBConnection.State <> ConnectionState.Closed Then gloDBConnection.Close()
            gloDBConnection.Dispose()
        End If
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

    Public Function Add(ByVal Name As String, ByVal Value As Object, ByVal Direction As System.Data.ParameterDirection, ByVal DataType As System.Data.SqlDbType) As DBParameter
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

