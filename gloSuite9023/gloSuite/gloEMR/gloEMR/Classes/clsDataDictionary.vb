Imports System.Data.SqlClient
Public Class clsDataDictionary
    Dim _nDataDictionaryID As Integer
    Dim _sDataDictionaryName As String
    Dim _sFieldNames As String
    Dim _sTableName As String

    Public Enum enumDictionaryType
        Vitals
    End Enum

    Public Property DataDictionaryID() As Integer
        Get
            Return _nDataDictionaryID
        End Get
        Set(ByVal Value As Integer)
            _nDataDictionaryID = Value
        End Set
    End Property

    Public Property DataDictionaryName() As String
        Get
            Return _sDataDictionaryName
        End Get
        Set(ByVal Value As String)
            _sDataDictionaryName = Value
        End Set
    End Property

    Public Property FieldNames() As String
        Get
            Return _sFieldNames
        End Get
        Set(ByVal Value As String)
            _sFieldNames = Value
        End Set
    End Property

    Public Property TableName() As String
        Get
            Return _sTableName
        End Get
        Set(ByVal Value As String)
            _sTableName = Value
        End Set
    End Property

    Public Function Fill_DataDictionary() As Collection
        Dim clDataDictionary As New Collection
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_FillDataDictionary"
            objCmd.Connection = objCon
            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader
            If objSQLDataReader.HasRows = True Then
                While objSQLDataReader.Read
                    clDataDictionary.Add(objSQLDataReader.Item(0))
                End While
            End If
            objSQLDataReader.Close()

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            objSQLDataReader = Nothing
            Return clDataDictionary
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDataDictionary -- Fill_DataDictionary -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDataDictionary -- Fill_DataDictionary -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    Public Sub ViewDataDictionary(ByVal strCaption As String, ByVal ID As Long)
        _sDataDictionaryName = strCaption
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            Dim objSQLDataReader As SqlDataReader
            Dim objParaCaption As New SqlParameter
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_RetrieveDataDictionary"

            objParaCaption = New SqlParameter
            With objParaCaption
                .ParameterName = "@Caption"
                .Value = strCaption
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaCaption)
            objParaCaption = Nothing

            objParaCaption = New SqlParameter
            With objParaCaption
                .ParameterName = "@ID"
                .Value = ID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaCaption)
            objCmd.Connection = objCon
            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader()
            If objSQLDataReader.HasRows Then
                objSQLDataReader.Read()
                _nDataDictionaryID = objSQLDataReader.Item("DictionaryID")
                _sFieldNames = objSQLDataReader.Item("FieldNames")
                _sTableName = objSQLDataReader.Item("TableName")
            End If
            objSQLDataReader.Close()
            objSQLDataReader = Nothing
            objParaCaption = Nothing

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDataDictionary -- ViewDataDictionary -- " & ex.ToString)
        Catch ex As Exception
            UpdateLog("clsDataDictionary -- ViewDataDictionary -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Sub

    Public Function UpdateDataDictionary(ByVal nDataDictionary As Integer) As Boolean
        Return UpdateDataDictionary(nDataDictionary, _sDataDictionaryName)
    End Function
    Public Function UpdateDataDictionary(ByVal nDataDictionary As Integer, ByVal sCaption As String) As Boolean
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            'Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_UpdateDataDictionary"

            Dim objParaDataDictionaryID As New SqlParameter
            With objParaDataDictionaryID
                .ParameterName = "@DictionaryID"
                .Value = nDataDictionary
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaDataDictionaryID)


            Dim objParaCaption As New SqlParameter
            With objParaCaption
                .ParameterName = "@Caption"
                .Value = sCaption
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaCaption)


            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()

            objParaDataDictionaryID = Nothing
            objParaCaption = Nothing

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            Return True
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDataDictionary -- UpdateDataDictionary -- " & ex.ToString)
            Return False
        Catch ex As Exception
            UpdateLog("clsDataDictionary -- UpdateDataDictionary -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function
    Public Function Fill_DataDictionaryTables() As Collection
        Dim clDataDictionary As New Collection
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_FillDataDictionarytables"
            objCmd.Connection = objCon
            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader
            If objSQLDataReader.HasRows = True Then
                While objSQLDataReader.Read
                    clDataDictionary.Add(objSQLDataReader.Item(0))
                End While
            End If
            objSQLDataReader.Close()

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            objSQLDataReader = Nothing
            Return clDataDictionary
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDataDictionary -- Fill_DataDictionaryTables -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDataDictionary -- Fill_DataDictionaryTables -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function
    Public Function Fill_DataDictionaryFields(ByVal sTableName As String) As Collection
        Dim clDataDictionary As New Collection
        Dim objCon As New SqlConnection
        Dim objParaTable As New SqlParameter
        Dim mylist As myList
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_FillDataDictionaryFields"

            With objParaTable
                .ParameterName = "@sTableName"
                .Value = sTableName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaTable)

            objCmd.Connection = objCon
            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader
            If objSQLDataReader.HasRows = True Then
                While objSQLDataReader.Read
                    mylist = New myList
                    mylist.Description = objSQLDataReader.Item(0)
                    mylist.ID = objSQLDataReader.Item(1)
                    clDataDictionary.Add(mylist)
                    mylist = Nothing
                End While
            End If
            objSQLDataReader.Close()

            objParaTable = Nothing
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            objSQLDataReader = Nothing
            Return clDataDictionary
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDataDictionary -- Fill_DataDictionaryFields -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDataDictionary -- Fill_DataDictionaryFields -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(objCon) Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function

    ' SUDHIR 20090629 ''
    Public Sub AddDataDictionary(ByVal fieldName As String, ByVal tableName As String, ByVal fieldCaption As String, ByVal tableCaption As String)
        Try
            Dim con As New SqlConnection(GetConnectionString)
            Dim cmd As New SqlCommand("gsp_InUpDataDictionary", con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.AddWithValue("@DictionaryID", 0)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.SqlDbType = SqlDbType.BigInt

            sqlParam = cmd.Parameters.AddWithValue("@FieldName", fieldName)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.SqlDbType = SqlDbType.VarChar

            sqlParam = cmd.Parameters.AddWithValue("@TableName", tableName)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.SqlDbType = SqlDbType.VarChar

            sqlParam = cmd.Parameters.AddWithValue("@Caption", fieldCaption)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.SqlDbType = SqlDbType.VarChar

            sqlParam = cmd.Parameters.AddWithValue("@sTableCaption", tableCaption)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.SqlDbType = SqlDbType.VarChar

            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            cmd.ExecuteNonQuery()

            If Not IsNothing(con) Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Dispose()
                con = Nothing
            End If
            sqlParam = Nothing
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Error while adding Data Dictionary " & vbLf & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub UpdateDataDictionary(ByVal nDictionaryID As Int64, ByVal fieldName As String, ByVal tableName As String, ByVal fieldCaption As String, ByVal tableCaption As String)
        Dim con As New SqlConnection(GetConnectionString)
        Try
            Dim sQuery As String = "UPDATE DataDictionary_MST SET sFieldName = '" & fieldName.Replace("'", "''") & "', sTableName = '" & tableName.Replace("'", "''") & "', sCaption = '" & fieldCaption.Replace("'", "''") & "', sTableCaption = '" & tableCaption.Replace("'", "''") & "' WHERE nDictionaryID = " & nDictionaryID & ""
            Dim cmd As New SqlCommand(sQuery, con)
            con.Open()
            cmd.ExecuteNonQuery()

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(con) Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Dispose()
                con = Nothing
            End If
        End Try
    End Sub

    Public Sub DeleteDataDictionary(ByVal sFieldName As String, Optional ByVal sFieldCaption As String = "")
        Try
            Dim con As New SqlConnection(GetConnectionString)
            Dim sQuery As String
            If sFieldCaption = "" Then
                sQuery = "DELETE FROM DataDictionary_MST WHERE sFieldName = '" & sFieldName.Replace("'", "''") & "'"
            Else
                sQuery = "DELETE FROM DataDictionary_MST WHERE sFieldName = '" & sFieldName.Replace("'", "''") & "' AND sCaption = '" & sFieldCaption.Replace("'", "''") & "'"
            End If
            Dim cmd As New SqlCommand(sQuery, con)
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            If Not IsNothing(con) Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Dispose()
                con = Nothing
            End If
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Error While Deleting DataDictionary" & vbLf & ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function IsFieldCaptionPresent(ByVal sFieldCaption As String, ByVal sTableCaption As String, Optional ByVal nDictionaryID As Int64 = 0) As Boolean
        Dim con As New SqlConnection(GetConnectionString)
        Dim sQuery As String = "SELECT COUNT(*) FROM DataDictionary_MST WHERE sCaption = '" & sFieldCaption.Replace("'", "''") & "' AND sTableCaption = '" & sTableCaption.Replace("'", "''") & "' AND nDictionaryID <> " & nDictionaryID & ""
        Dim cmd As New SqlCommand(sQuery, con)
        Dim oResult As Object
        Try
            con.Open()
            oResult = cmd.ExecuteScalar
            If CType(oResult, Integer) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally

            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

            If Not IsNothing(con) Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Dispose()
                con = Nothing
            End If

        End Try
    End Function

    Public Function ReplaceColumnNames(ByVal dtTable As DataTable, ByVal dictionaryType As enumDictionaryType) As DataTable
        Dim con As New SqlConnection(GetConnectionString)
        Dim sQuery As String = "SELECT sFieldName, sCaption FROM DataDictionary_MST WHERE sTableCaption = '" & dictionaryType.ToString & "'"
        Dim cmd As New SqlCommand(sQuery, con)
        Dim adp As New SqlDataAdapter(cmd)
        Dim dtTemp As New DataTable
        Dim dvTemp As DataView
        Try
            If dtTable IsNot Nothing Then
                adp.Fill(dtTemp)
                If dtTemp IsNot Nothing Then
                    dvTemp = dtTemp.DefaultView

                    '' SERCHING COLUMN NAME IN DATAVIEW AND REPLACING IT WITH ALICE NAME ''
                    If (IsNothing(dtTable) = False) Then


                        For iCol As Integer = 0 To dtTable.Columns.Count - 1

                            dvTemp.RowFilter = Nothing '' SEARCHING COLUMN NAME BY FILTERIGN DV ''
                            dvTemp.RowFilter = "sFieldName = '" & dictionaryType.ToString() & "." & dtTable.Columns(iCol).ColumnName & "'"

                            If dvTemp.ToTable.Rows.Count > 0 Then '' REPLACING COLUMN NAME HERE ''
                                dtTable.Columns(iCol).ColumnName = dvTemp.ToTable.Rows(0)("sCaption")
                            End If

                        Next
                    End If
                    dvTemp.Dispose()
                    dtTemp.Dispose()

                End If

            End If

            Return dtTable
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            adp.Dispose()
            adp = Nothing
            If Not IsNothing(con) Then
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Dispose()
                con = Nothing
            End If
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Function
    '' END SUDHIR ''

    Public Function GetAllTableTemplates() As DataTable
        Dim Con As New SqlConnection(GetConnectionString)
        Dim strSql As String = "SELECT nDictionaryID, sFieldName, sCaption, sTableCaption FROM DataDictionary_MST WHERE sTableName = 'TableTemplate'"
        Dim cmd As New SqlCommand(strSql, Con)
        Dim adp As New SqlDataAdapter(cmd)
        Dim dtTable As New DataTable
        Try
            adp.Fill(dtTable)
            If dtTable IsNot Nothing Then
                Return dtTable
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            adp.Dispose()
            adp = Nothing
            If Not IsNothing(Con) Then
                If Con.State = ConnectionState.Open Then
                    Con.Close()
                End If
                Con.Dispose()
                Con = Nothing
            End If
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try
    End Function

    Public Function GetDataDictionary(ByVal dictionaryType As enumDictionaryType) As DataTable
        Dim Con As New SqlConnection(GetConnectionString)
        Dim strSql As String = "SELECT nDictionaryID, sFieldName, sCaption, sTableCaption FROM DataDictionary_MST WHERE sFieldName not like 'Vitals.Last%' AND sTableName = '" & dictionaryType.ToString & "'"
        Dim cmd As New SqlCommand(strSql, Con)
        Dim adp As New SqlDataAdapter(cmd)
        Dim dtDictionary As New DataTable
        Try
            adp.Fill(dtDictionary)
            If dtDictionary IsNot Nothing Then
                Return dtDictionary
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            adp.Dispose()
            adp = Nothing
            If Not IsNothing(Con) Then
                If Con.State = ConnectionState.Open Then
                    Con.Close()
                End If
                Con.Dispose()
                Con = Nothing
            End If
            If cmd IsNot Nothing Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If

        End Try
    End Function

    '' Added by Mahesh to Update History ROS Fields
    ''''Commented by Pramod Not use this function in gloEMR 03102007

    'Public Sub GetDataDictionary(ByVal TableName As String, ByVal Caption As String)
    '    _sDataDictionaryName = Caption
    '    _sTableName = TableName
    '    Dim objCon As New SqlConnection
    '    objCon.ConnectionString = GetConnectionString()
    '    Dim objCmd As New SqlCommand
    '    Dim objSQLDataReader As SqlDataReader
    '    objCmd.CommandType = CommandType.StoredProcedure
    '    objCmd.CommandText = "gsp_RetrieveDataDictionary"

    '    Dim objParaCaption As New SqlParameter
    '    With objParaCaption
    '        .ParameterName = "@Caption"
    '        .Value = Caption
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaCaption)

    '    Dim objParaTable As New SqlParameter
    '    With objParaTable
    '        .ParameterName = "@TableName"
    '        .Value = TableName
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaTable)
    '    objCmd.Connection = objCon
    '    objCon.Open()
    '    objSQLDataReader = objCmd.ExecuteReader()
    '    If objSQLDataReader.HasRows Then
    '        objSQLDataReader.Read()
    '        _nDataDictionaryID = objSQLDataReader.Item("DictionaryID")
    '        _sFieldNames = objSQLDataReader.Item("FieldNames")
    '        _sTableName = objSQLDataReader.Item("TableName")
    '    End If
    '    objCon.Close()
    '    objCmd = Nothing
    '    objCon = Nothing
    'End Sub

End Class
