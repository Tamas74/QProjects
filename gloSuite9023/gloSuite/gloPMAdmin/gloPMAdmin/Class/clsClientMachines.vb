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
    Dim _sClientProductCode As String = "1"

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
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
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
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objCon.Close()
        objCon = Nothing
        Return dsData.Tables(0)
    End Function
    Public Function ScanClientMachine(ByVal nClientID As Integer) As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_ViewClientMachinesByID"
        objCmd.Connection = objCon
        Dim objParaClientID As New SqlParameter
        With objParaClientID
            .ParameterName = "@MachineID"
            .Value = nClientID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaClientID)

        ''Sandip Darade 20091113
        Dim objParaProductCode As New SqlParameter
        With objParaProductCode
            .ParameterName = "@sProductCode"
            .Value = _sClientProductCode
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaProductCode)
        objCmd.Connection = objCon
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objCon.Close()
        objCon = Nothing
        Return dsData.Tables(0)
    End Function
    Public Function Fill_ClientMachines() As Collection
        Dim clClientMachines As New Collection
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillClientMachines"
        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader
        If objSQLDataReader.HasRows = True Then
            While objSQLDataReader.Read
                clClientMachines.Add(objSQLDataReader.Item(0))
            End While
        End If
        objCon.Close()
        objCon = Nothing
        objCmd = Nothing
        objSQLDataReader = Nothing
        Return clClientMachines
    End Function
    Public Function Fill_Clients() As DataTable
        Dim dtClients As New DataTable
        Dim clClientMachines As New Collection
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillClientMachines"
        objCmd.Connection = objCon
        ''Sandip Darade 20091121
        Dim objParaProductCode As New SqlParameter
        With objParaProductCode
            .ParameterName = "@sProductCode"
            .Value = _sClientProductCode
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaProductCode)

        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        objDA.Fill(dtClients)
        objCon.Close()
        objCon = Nothing
        objCmd = Nothing
        objSQLDataReader = Nothing
        Return dtClients
    End Function
    Public Function UpdateClient(ByVal nClientID As Integer) As Boolean
        Return UpdateClient(nClientID, _sClientMachineName, _blnVoiceEnabled, _blnScanEnabled)
    End Function
    Public Function UpdateClient(ByVal nClientID As Integer, ByVal strMachineName As String, ByVal blnVoiceEnabled As Boolean, ByVal blnScanEnabled As Boolean) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        'Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_InUpClientSettings"

        Dim objParaMachineID As New SqlParameter
        With objParaMachineID
            .ParameterName = "@ClientID"
            .Value = nClientID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaMachineID)


        Dim objParaMachineName As New SqlParameter
        With objParaMachineName
            .ParameterName = "@MachineName"
            .Value = strMachineName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaMachineName)


        Dim objParaVoiceEnabled As New SqlParameter
        With objParaVoiceEnabled
            .ParameterName = "@VoiceEnabled"
            .Value = blnVoiceEnabled
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objParaVoiceEnabled)

        Dim objParaScanEnabled As New SqlParameter
        With objParaScanEnabled
            .ParameterName = "@ScanEnabled"
            .Value = blnScanEnabled
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objParaScanEnabled)

        ''Sandip Darade 20091113
        Dim objParaProductCode As New SqlParameter
        With objParaProductCode
            .ParameterName = "@sProductCode"
            .Value = _sClientProductCode
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaProductCode)

        objCmd.Connection = objCon
        objCon.Open()
        objCmd.ExecuteNonQuery()
        objCon.Close()
        objCmd = Nothing
        objCon = Nothing
        Return True
    End Function
    Public Function InsertClient() As Boolean
        Return InsertClient(_sClientMachineName, _blnVoiceEnabled, _blnScanEnabled)
    End Function
    Public Function InsertClient(ByVal strMachineName As String, ByVal blnVoiceEnabled As Boolean, ByVal blnScanEnabled As Boolean) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand

        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_InUpClientSettings"


        Dim objParaMachineName As New SqlParameter
        With objParaMachineName
            .ParameterName = "@MachineName"
            .Value = strMachineName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaMachineName)


        Dim objParaVoiceEnabled As New SqlParameter
        With objParaVoiceEnabled
            .ParameterName = "@VoiceEnabled"
            .Value = blnVoiceEnabled
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objParaVoiceEnabled)

        Dim objParaScanEnabled As New SqlParameter
        With objParaScanEnabled
            .ParameterName = "@ScanEnabled"
            .Value = blnScanEnabled
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objParaScanEnabled)

        ''Sandip Darade 20091113
        Dim objParaProductCode As New SqlParameter
        With objParaProductCode
            .ParameterName = "@sProductCode"
            .Value = _sClientProductCode
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaProductCode)

        objCmd.Connection = objCon
        objCon.Open()
        objCmd.ExecuteNonQuery()
        objCon.Close()
        objCmd = Nothing
        objCon = Nothing
        Return True
    End Function
    Public Sub SearchClient(ByVal strClientName As String)
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
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
        objSQLDataReader = objCmd.ExecuteReader()
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
        objCon.Close()
        objCon = Nothing
    End Sub
    Public Sub SearchClient(ByVal nClientID As Integer)
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
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

        ''Sandip Darade 20091113
        Dim objParaProductCode As New SqlParameter
        With objParaProductCode
            .ParameterName = "@sProductCode"
            .Value = _sClientProductCode
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaProductCode)

        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader()
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
        objCon.Close()
        objCon = Nothing
    End Sub
    Public Function DeleteClientSettings(ByVal strClientMachineName As String) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
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
        objCmd = Nothing
        objCon = Nothing
        Return True
    End Function
    Public Function DeleteClientSettingsByID(ByVal nClientID As Integer) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand

        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_DeleteClientMachineByID"


        Dim objParaMachineID As New SqlParameter
        With objParaMachineID
            .ParameterName = "@MachineID"
            .Value = nClientID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaMachineID)

        ''Sandip Darade 20091113
        Dim objParaProductCode As New SqlParameter
        With objParaProductCode
            .ParameterName = "@sProductCode"
            .Value = _sClientProductCode
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaProductCode)

        objCmd.Connection = objCon
        objCon.Open()
        objCmd.ExecuteNonQuery()
        objCon.Close()
        objCmd = Nothing
        objCon = Nothing
        Return True
    End Function
    Public Function CheckMachineExists(ByVal strMachineName As String, Optional ByVal nMachineID As Integer = 0) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
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

        ''Sandip Darade 20091113
        Dim objParaProductCode As New SqlParameter
        With objParaProductCode
            .ParameterName = "@sProductCode"
            .Value = _sClientProductCode
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaProductCode)

        If nMachineID <> 0 Then
            Dim objParaMachineID As New SqlParameter
            With objParaMachineID
                .ParameterName = "@MachineID"
                .Value = nMachineID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Int
            End With
            objCmd.Parameters.Add(objParaMachineID)
        End If
        objCmd.Connection = objCon
        Dim nCount As Integer
        objCon.Open()
        nCount = objCmd.ExecuteScalar
        objCon.Close()
        objCon = Nothing
        If nCount = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    Public Function ScanClientMachineUpdates() As DataTable
        Dim objCon As New SqlConnection
        objCon.ConnectionString = mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_ViewClientUpdateDetailByID"
        objCmd.Connection = objCon
       
        objCmd.Connection = objCon
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objCon.Close()
        objCon = Nothing
        Return dsData.Tables(0)
    End Function

#End Region
End Class

Public Class clsClientInterface
#Region "Public Properties for Client Interface"

    Public Property IsInsertClientInterface As Boolean
    Public Property ClientSettings_InterfaceID As Int64
    Public Property ProductName As String
    Public Property MachineName As String
    Public Property Hl7_SendPatientDetails As Boolean
    Public Property HL7_SendAppointmentDetails As Boolean
    Public Property HL7_UseDefault As Boolean

#End Region
    Private _sClientProductCode As String = "1"

#Region "Public function for Client Interface"

    Public Function ScanClientInterface(ByVal strProductName As String, ByVal strMachineName As String) As Boolean

        Dim objCon As SqlConnection = Nothing
        Dim objCmd As SqlCommand = Nothing

        Dim objParam As SqlParameter = Nothing

        Dim objDA As SqlDataAdapter = Nothing

        Dim dsData As DataSet = Nothing
        Dim nCount As Integer


        Dim oDBLayer As New gloDatabaseLayer.DBLayer(gloPMAdmin.mdlGeneral.GetConnectionString())
        Try
            objCon = New SqlConnection
            objCmd = New SqlCommand

            objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()

            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_ViewClientInterface"

            objCmd.Connection = objCon

            objParam = New SqlParameter
            With objParam
                .ParameterName = "@sMachineName"
                .Value = strMachineName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.NVarChar
            End With
            objCmd.Parameters.Add(objParam)
            objParam = Nothing

            objParam = New SqlParameter
            With objParam
                .ParameterName = "@sProductName"
                .Value = strProductName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.NVarChar
            End With
            objCmd.Parameters.Add(objParam)
            objParam = Nothing
            objCmd.Connection = objCon

            If Not IsNothing(objCon) AndAlso objCon.State <> ConnectionState.Open Then
                objCon.Open()
            End If

            objDA = New SqlDataAdapter(objCmd)
            dsData = New DataSet

            objDA.Fill(dsData)

            For nCount = 0 To dsData.Tables(0).Rows.Count - 1
                ClientSettings_InterfaceID = dsData.Tables(0).Rows.Item(nCount).Item("nClientSettings_InterfaceID")
                Hl7_SendPatientDetails = dsData.Tables(0).Rows.Item(nCount).Item("bHl7_SendPatientDetails")
                HL7_SendAppointmentDetails = dsData.Tables(0).Rows.Item(nCount).Item("bHL7_SendAppointmentDetails")
                HL7_UseDefault = dsData.Tables(0).Rows.Item(nCount).Item("bHL7_UseDefault")
            Next
            ProductName = strProductName
            MachineName = strMachineName


            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            Return False
        Finally

            If Not IsNothing(objCon) Then
                objCon.Close()
                objCon = Nothing
            End If

            If Not IsNothing(dsData) Then
                dsData = Nothing
            End If

            If Not IsNothing(objDA) Then
                objDA = Nothing
            End If

            If Not IsNothing(objCmd) Then
                objCmd = Nothing
            End If

        End Try
        Return True
    End Function

    Public Function InsertClientInterface() As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand

        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_INUPClientSettings_Interface"

        Dim objClientSettings_InterfaceID As New SqlParameter
        With objClientSettings_InterfaceID
            .ParameterName = "@nClientSettings_InterfaceID"
            .Value = ClientSettings_InterfaceID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objClientSettings_InterfaceID)

        Dim objProductName As New SqlParameter
        With objProductName
            .ParameterName = "@sProductName"
            .Value = ProductName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.NVarChar

        End With
        objCmd.Parameters.Add(objProductName)

        Dim objMachineName As New SqlParameter
        With objMachineName
            .ParameterName = "@sMachineName"
            .Value = MachineName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.NVarChar
        End With
        objCmd.Parameters.Add(objMachineName)


        Dim objHl7_SendPatientDetails As New SqlParameter
        With objHl7_SendPatientDetails
            .ParameterName = "@bHl7_SendPatientDetails"
            .Value = Hl7_SendPatientDetails
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objHl7_SendPatientDetails)



        Dim objHL7_SendAppointmentDetails As New SqlParameter
        With objHL7_SendAppointmentDetails
            .ParameterName = "@bHL7_SendAppointmentDetails"
            .Value = HL7_SendAppointmentDetails
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objHL7_SendAppointmentDetails)



        Dim objHL7_UseDefault As New SqlParameter
        With objHL7_UseDefault
            .ParameterName = "@bHl7_usedefault"
            .Value = HL7_UseDefault
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objHL7_UseDefault)




        objCmd.Connection = objCon
        objCon.Open()
        objCmd.ExecuteNonQuery()
        objCon.Close()
        objCmd = Nothing
        objCon = Nothing
        Return True

    End Function

    'Public Function InsertClientAllMachineInterface() As Boolean
    '    Dim objCon As New SqlConnection
    '    objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
    '    Dim objCmd As New SqlCommand

    '    objCmd.CommandType = CommandType.StoredProcedure
    '    objCmd.CommandText = "gsp_INUPAllMachineClientSettings_Interface"



    '    Dim objProductName As New SqlParameter
    '    With objProductName
    '        .ParameterName = "@sProductName"
    '        .Value = ProductName
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.NVarChar

    '    End With
    '    objCmd.Parameters.Add(objProductName)



    '    Dim objProductCode As New SqlParameter
    '    With objProductCode
    '        .ParameterName = "@sProductCode"
    '        .Value = _sClientProductCode                  ''for PM product Code is also 1 as decided on 12/03/2012
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar

    '    End With
    '    objCmd.Parameters.Add(objProductCode)





    '    Dim objHl7_SendPatientDetails As New SqlParameter
    '    With objHl7_SendPatientDetails
    '        .ParameterName = "@bHl7_SendPatientDetails"
    '        .Value = Hl7_SendPatientDetails
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Bit
    '    End With
    '    objCmd.Parameters.Add(objHl7_SendPatientDetails)



    '    Dim objHL7_SendAppointmentDetails As New SqlParameter
    '    With objHL7_SendAppointmentDetails
    '        .ParameterName = "@bHL7_SendAppointmentDetails"
    '        .Value = HL7_SendAppointmentDetails
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Bit
    '    End With
    '    objCmd.Parameters.Add(objHL7_SendAppointmentDetails)


    '    objCmd.Connection = objCon
    '    objCon.Open()
    '    objCmd.ExecuteNonQuery()
    '    objCon.Close()
    '    objCmd = Nothing
    '    objCon = Nothing
    '    Return True

    'End Function


#End Region

End Class
