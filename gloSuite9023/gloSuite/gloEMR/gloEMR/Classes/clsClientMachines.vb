'***************************************************************************
' Module Name :- gloEMR Admin Login
' Company Name :- gloStream Inc.
' Written By :- Pankaj Naval
' Description :-
'This form is to validate the User Name and Password
'Processes
'   1) 
'***************************************************************************

Imports System.Data.SqlClient
Public Class clsClientMachines

#Region " Private Variables"
    Dim _nClientMachineID As Integer
    Dim _sClientMachineName As String
    Dim _blnVoiceEnabled As Boolean
    Dim _blnScanEnabled As Boolean
#End Region
#Region " Public Properties"
    Public Property ClientMachineID() As Integer
        Get
            Return _nClientMachineID
        End Get
        Set(ByVal Value As Integer)
            _nClientMachineID = Value
        End Set
    End Property
    Public Property ClientMachineName() As String
        Get
            Return _sClientMachineName
        End Get
        Set(ByVal Value As String)
            _sClientMachineName = Value
        End Set
    End Property
    Public Property VoiceEnabled() As Boolean
        Get
            Return _blnVoiceEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnVoiceEnabled = Value
        End Set
    End Property
    Public Property ScanEnabled() As Boolean
        Get
            Return _blnScanEnabled
        End Get
        Set(ByVal Value As Boolean)
            _blnScanEnabled = Value
        End Set
    End Property
#End Region
#Region " Public Functions"
    Public Function ScanClientMachine(ByVal strClientMachineName As String) As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Try
            objCon.ConnectionString = GetConnectionString()

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ViewClientMachines"
            objCmd.Connection = objCon
            Dim objParaProviderName As New SqlParameter
            With objParaProviderName
                .ParameterName = "@MachineName"
                .Value = strClientMachineName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaProviderName)
            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            Dim dtTable As New DataTable
            objDA.Fill(dtTable)
            objCon.Close()
            'objCon = Nothing
            objParaProviderName = Nothing
            objDA.Dispose()
            objDA = Nothing
            Return dtTable
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsClientMachines -- ScanClientMachine -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsClientMachines -- ScanClientMachine -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(objCon) = False Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
       

    End Function
    Public Function Fill_ClientMachines() As Collection
        Dim clClientMachines As New Collection
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Try
            objCon.ConnectionString = GetConnectionString()

            Dim objSQLDataReader As SqlDataReader
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_FillClientMachines"
            objCmd.Connection = objCon
            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader
            While objSQLDataReader.Read
                clClientMachines.Add(objSQLDataReader.Item(0))
            End While
            objSQLDataReader.Close()
            objCon.Close()
            ' objCon = Nothing
            'objCmd = Nothing
            objSQLDataReader = Nothing
            Return clClientMachines
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsClientMachines -- Fill_ClientMachines -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsClientMachines -- Fill_ClientMachines -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(objCon) = False Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        End Try
    End Function
    Public Sub SearchClient(ByVal strClientName As String)
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ViewClientMachines"
            objCmd.Connection = objCon
            Dim objSQLDataReader As SqlDataReader
            Dim objParaProviderName As New SqlParameter
            With objParaProviderName
                .ParameterName = "@MachineName"
                .Value = strClientName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaProviderName)
            objCmd.Connection = objCon
            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader(CommandBehavior.SingleRow)
            If objSQLDataReader.HasRows = True Then
                objSQLDataReader.Read()
                _sClientMachineName = strClientName
                If IsDBNull(objSQLDataReader.Item(0)) = False Then
                    _nClientMachineID = objSQLDataReader.Item(0)
                End If
                If IsDBNull(objSQLDataReader.Item(1)) = False Then
                    _blnVoiceEnabled = CType(objSQLDataReader.Item(1), Boolean)
                End If
                If IsDBNull(objSQLDataReader.Item(2)) = False Then
                    _blnScanEnabled = CType(objSQLDataReader.Item(2), Boolean)
                End If
            End If
            objSQLDataReader.Close()
            objCon.Close()
            '            objCon = Nothing

            If objParaProviderName IsNot Nothing Then
                objParaProviderName = Nothing
            End If

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsClientMachines -- SearchClient(1) -- " & ex.ToString)
        Catch ex As Exception
            UpdateLog("clsClientMachines -- SearchClient(1) -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(objCon) = False Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Sub
    Public Sub SearchClient(ByVal nClientID As Integer)
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ViewClientMachinesByID"
            objCmd.Connection = objCon
            Dim objSQLDataReader As SqlDataReader
            Dim objParaMachineID As New SqlParameter
            With objParaMachineID
                .ParameterName = "@MachineID"
                .Value = nClientID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaMachineID)
            objCmd.Connection = objCon
            objCon.Open()
            objSQLDataReader = objCmd.ExecuteReader(CommandBehavior.SingleRow)
            If objSQLDataReader.HasRows = True Then
                objSQLDataReader.Read()
                _nClientMachineID = nClientID
                If IsDBNull(objSQLDataReader.Item("MachineName")) = False Then
                    _sClientMachineName = objSQLDataReader.Item("MachineName")
                End If
                If IsDBNull(objSQLDataReader.Item("VoiceEnabled")) = False Then
                    _blnVoiceEnabled = CType(objSQLDataReader.Item("VoiceEnabled"), Boolean)
                End If
                If IsDBNull(objSQLDataReader.Item("ScanEnabled")) = False Then
                    _blnScanEnabled = CType(objSQLDataReader.Item("ScanEnabled"), Boolean)
                End If
            End If
            objSQLDataReader.Close()
            objCon.Close()
            '    objCon = Nothing

            If objParaMachineID IsNot Nothing Then
                objParaMachineID = Nothing
            End If

            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsClientMachines -- SearchClient(2) -- " & ex.ToString)
        Catch ex As Exception
            UpdateLog("clsClientMachines -- SearchClient(2) -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If IsNothing(objCon) = False Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If

        End Try
    End Sub
    Public Function DeleteClientSettings(ByVal strClientMachineName As String) As Boolean
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_DeleteClientMachine"


            Dim objParaMachineName As New SqlParameter
            With objParaMachineName
                .ParameterName = "@MachineName"
                .Value = strClientMachineName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaMachineName)

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            '            objCmd = Nothing
            '           objCon = Nothing


            If objParaMachineName IsNot Nothing Then
                objParaMachineName = Nothing
            End If
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            Return True
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsClientMachines -- DeleteClientSettings -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsClientMachines -- DeleteClientSettings -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(objCon) = False Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If
        End Try
    End Function
    Public Function CheckMachineExists(ByVal strMachineName As String, Optional ByVal nMachineID As Integer = 0) As Boolean
        Dim objCon As New SqlConnection
        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_CheckMachineNameExists"
            Dim objParaMachineName As New SqlParameter
            With objParaMachineName
                .ParameterName = "@MachineName"
                .Value = strMachineName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.VarChar
            End With
            objCmd.Parameters.Add(objParaMachineName)

            If nMachineID <> 0 Then
                Dim objParaMachineID As New SqlParameter
                With objParaMachineID
                    .ParameterName = "@MachineID"
                    .Value = nMachineID
                    .Direction = ParameterDirection.Input
                    .SqlDbType = SqlDbType.Int
                End With
                objCmd.Parameters.Add(objParaMachineID)

                objParaMachineID = Nothing
            End If

            objCmd.Connection = objCon
            Dim nCount As Integer
            objCon.Open()
            nCount = objCmd.ExecuteScalar
            objCon.Close()
            'objCon = Nothing

            If objParaMachineName IsNot Nothing Then
                objParaMachineName = Nothing
            End If

           
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If nCount = 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As SqlException
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            UpdateLog("clsClientMachines -- CheckMachineExists -- " & ex.ToString)
            Return Nothing
        Catch ex As Exception
            UpdateLog("clsClientMachines -- CheckMachineExists -- " & ex.ToString)
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If IsNothing(objCon) = False Then
                If objCon.State = ConnectionState.Open Then
                    objCon.Close()
                End If
                objCon.Dispose()
                objCon = Nothing
            End If

        End Try
    End Function
#End Region
End Class
