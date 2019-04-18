Imports System.Data.SqlClient

Public Class clsDMSCategory
    Dim _nCategoryId As Integer
    Dim _sCategoryName As String
    Dim _blnAddEditFlag As Boolean = False
    Dim _sModifyCategory As String

    Dim strSQL As String
    Dim gstrConstr As String = GetConnectionString()


    'Declare Property
    Public Property CategoryName() As String
        Get
            Return _sCategoryName
        End Get
        Set(ByVal Value As String)
            _sCategoryName = Value
        End Set
    End Property

    'Methods
    Public Sub NewCategory()
        _blnAddEditFlag = True
        _sModifyCategory = ""
    End Sub
    Public Sub OpenCategory(ByVal sCategoryName As String)
        _blnAddEditFlag = False
        _sModifyCategory = sCategoryName
    End Sub
    Public Function Save() As Boolean
        'Primary Declaration
        Dim oConnection As New SqlConnection                    ' New Connection
        oConnection.ConnectionString = gstrConstr               ' Connection String
        Dim oSQLCommand As New SqlCommand                       ' SQL Command
        ' Dim oDataReader As SqlDataReader                         ' Data Reader
        Try
            oSQLCommand.CommandType = CommandType.StoredProcedure   ' SQL Command Type , is Store Procedure
            oSQLCommand.CommandText = "gsp_InUpDMSCategory"          ' Set Store Procedure Name


            'Set Database Fields Value
            '1. Modify Category Name
            Dim oModifyCategory As New SqlParameter                 ' Declare Modify Category Object
            With oModifyCategory
                .ParameterName = "@ModifyName"                      ' SQL Delaration Variable Name
                .Value = _sModifyCategory
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            oSQLCommand.Parameters.Add(oModifyCategory)


            '1. Actual Category Name
            Dim oNewCategory As New SqlParameter                    ' Declare Actual Category Object
            With oNewCategory
                .ParameterName = "@CategoryName"                    ' SQL Delaration Variable Name
                .Value = _sCategoryName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            oSQLCommand.Parameters.Add(oNewCategory)


            oSQLCommand.Connection = oConnection
            oConnection.Open()
            oSQLCommand.ExecuteNonQuery()
          
            oModifyCategory = Nothing
            oNewCategory = Nothing

            Return True
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDMSCategory -- Save -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDMSCategory -- Save -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oConnection) Then
                If oConnection.State = ConnectionState.Open Then
                    oConnection.Close()
                End If
                oConnection.Dispose()
                oConnection = Nothing
            End If

            If oSQLCommand IsNot Nothing Then
                oSQLCommand.Parameters.Clear()
                oSQLCommand.Dispose()
                oSQLCommand = Nothing
            End If
        End Try
    End Function
    Public Function Delete() As Boolean
        'Primary Declaration
        Dim oConnection As New SqlConnection                    ' New Connection
        oConnection.ConnectionString = gstrConstr               ' Connection String
        Dim oSQLCommand As New SqlCommand                       ' SQL Command
        '    Dim oDataReader As SqlDataReader                        ' Data Reader
        Try
            oSQLCommand.CommandType = CommandType.StoredProcedure   ' SQL Command Type , is Store Procedure
            oSQLCommand.CommandText = "gsp_DeleteDMSCategoryMst"          ' Set Store Procedure Name

            Dim oCategoryName As New SqlParameter
            With oCategoryName
                .ParameterName = "@CategoryName"
                .Value = _sModifyCategory
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With

            oSQLCommand.Parameters.Add(oCategoryName)
            oSQLCommand.Connection = oConnection
            oConnection.Open()
            oSQLCommand.ExecuteNonQuery()
            oConnection.Close()
        
            oCategoryName = Nothing

            Return True
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDMSCategory -- Delete -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDMSCategory -- Delete -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oConnection) Then
                If oConnection.State = ConnectionState.Open Then
                    oConnection.Close()
                End If
                oConnection.Dispose()
                oConnection = Nothing
            End If

            If oSQLCommand IsNot Nothing Then
                oSQLCommand.Parameters.Clear()
                oSQLCommand.Dispose()
                oSQLCommand = Nothing
            End If
        End Try
    End Function
    Public Function IsExists(ByVal sCategoryName As String) As Boolean
        'Primary Declaration
        Dim oConnection As New SqlConnection                    ' New Connection
        oConnection.ConnectionString = gstrConstr               ' Connection String
        Dim oSQLCommand As New SqlCommand                       ' SQL Command
        ' Data Reader
        Try
            oSQLCommand.CommandType = CommandType.StoredProcedure   ' SQL Command Type , is Store Procedure
            oSQLCommand.CommandText = "gsp_ScanDMSCategory"          ' Set Store Procedure Name

            Dim oCategoryName As New SqlParameter
            With oCategoryName
                .ParameterName = "@CategoryName"
                .Value = sCategoryName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With

            oSQLCommand.Parameters.Add(oCategoryName)

            oSQLCommand.Connection = oConnection
            oConnection.Open()
            Dim oDataReader As SqlDataReader = oSQLCommand.ExecuteReader

            If oDataReader.HasRows = True Then
                oDataReader.Close()
                oDataReader = Nothing
                Return True
            End If
            oConnection.Close()

            oCategoryName = Nothing
            oDataReader.Close()
            oDataReader = Nothing
            Return Nothing
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDMSCategory -- IsExists -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDMSCategory -- IsExists -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oConnection) Then
                If oConnection.State = ConnectionState.Open Then
                    oConnection.Close()
                End If
                oConnection.Dispose()
                oConnection = Nothing
            End If

            If oSQLCommand IsNot Nothing Then
                oSQLCommand.Parameters.Clear()
                oSQLCommand.Dispose()
                oSQLCommand = Nothing
            End If
        End Try
    End Function
    Public Function IsDelete(ByVal sCategoryName As String) As Boolean
        Return Nothing
    End Function
    Public Function CategoryList() As Collection
        Dim clnCategoryList As New Collection
        Dim oConnection As New SqlConnection                    ' New Connection
        oConnection.ConnectionString = gstrConstr               ' Connection String
        Dim oSQLCommand As New SqlCommand
        Try
            'Primary Declaration
            ' SQL Command
            Dim oDataReader As SqlDataReader                        ' Data Reader
            oSQLCommand.CommandType = CommandType.StoredProcedure   ' SQL Command Type , is Store Procedure
            oSQLCommand.CommandText = "gsp_FillDMSCategory"          ' Set Store Procedure Name
            oSQLCommand.Connection = oConnection
            oConnection.Open()
            oDataReader = oSQLCommand.ExecuteReader

            If oDataReader.HasRows = True Then
                While oDataReader.Read
                    clnCategoryList.Add(oDataReader.Item(0))
                End While
            End If
            oDataReader.Close()
            oConnection.Close()



            oDataReader = Nothing
            Return clnCategoryList
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDMSCategory -- CategoryList -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDMSCategory -- CategoryList -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oConnection) Then
                If oConnection.State = ConnectionState.Open Then
                    oConnection.Close()
                End If
                oConnection.Dispose()
                oConnection = Nothing
            End If

            If oSQLCommand IsNot Nothing Then
                oSQLCommand.Parameters.Clear()
                oSQLCommand.Dispose()
                oSQLCommand = Nothing
            End If
        End Try
    End Function

    Public Function PatientList() As Collection
        Dim clnPatientList As New Collection
        Dim oConnection As New SqlConnection                    ' New Connection
        oConnection.ConnectionString = gstrConstr               ' Connection String
        Dim oSQLCommand As New SqlCommand                       ' SQL Command
        Try
            'Primary Declaration

            Dim oDataReader As SqlDataReader                        ' Data Reader
            oSQLCommand.CommandType = CommandType.StoredProcedure   ' SQL Command Type , is Store Procedure
            oSQLCommand.CommandText = "gsp_FillPatients"          ' Set Store Procedure Name
            oSQLCommand.Connection = oConnection
            oConnection.Open()
            oDataReader = oSQLCommand.ExecuteReader

            If oDataReader.HasRows = True Then
                While oDataReader.Read
                    clnPatientList.Add(oDataReader.Item(1) + " " + oDataReader.Item(2))
                End While
            End If
            oConnection.Close()
            oDataReader.Close()


            

            oDataReader = Nothing
            Return clnPatientList
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDMSCategory -- PatientList -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDMSCategory -- PatientList -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(oConnection) Then
                If oConnection.State = ConnectionState.Open Then
                    oConnection.Close()
                End If
                oConnection.Dispose()
                oConnection = Nothing
            End If

            If oSQLCommand IsNot Nothing Then
                oSQLCommand.Parameters.Clear()
                oSQLCommand.Dispose()
                oSQLCommand = Nothing
            End If
        End Try
    End Function
    Public Function Fill_LockDMS(ByVal MachinID As Long, ByVal TransactionType As Integer) As DataTable
        Try
            Dim dt As New DataTable
            Dim sqladpt As New SqlDataAdapter
            Dim Cmd As New SqlCommand
            Dim conn As New SqlConnection(GetConnectionString)

            Cmd = New System.Data.SqlClient.SqlCommand("gsp_Select_UnLock_Record", conn)
            Cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter

            objParam = Cmd.Parameters.Add("@sMachinName", SqlDbType.VarChar)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = ""

            objParam = Cmd.Parameters.Add("@nTrnType", SqlDbType.Int)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = TransactionType

            objParam = Cmd.Parameters.Add("@nMachinID", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = MachinID

            sqladpt.SelectCommand = Cmd

            sqladpt.Fill(dt)
            sqladpt.Dispose()
            conn.Close()
            conn.Dispose()
            If Cmd IsNot Nothing Then
                Cmd.Parameters.Clear()
                Cmd.Dispose()
                Cmd = Nothing
            End If
            objParam = Nothing
            Return dt
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsDMSCategory -- Fill_LockDMS -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsDMSCategory -- Fill_LockDMS -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
End Class
