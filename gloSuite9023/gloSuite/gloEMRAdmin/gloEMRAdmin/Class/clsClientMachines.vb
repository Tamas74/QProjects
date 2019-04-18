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
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
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
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
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
            .Value = "1"
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
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillClientMachines"
        ''Sandip Darade 20091127
        Dim objParaProductCode As New SqlParameter
        With objParaProductCode
            .ParameterName = "@sProductCode"
            .Value = "1"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaProductCode)

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
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillClientMachines"


        ''Sandip Darade 20091127
        Dim objParaProductCode As New SqlParameter
        With objParaProductCode
            .ParameterName = "@sProductCode"
            .Value = "1"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaProductCode)


        objCmd.Connection = objCon
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
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
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
            .Value = "1"
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
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
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
            .Value = "1"
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
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
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
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
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
            .Value = "1"
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
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
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
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
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
            .Value = "1"
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
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
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
            .Value = "1"
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
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_ViewClientUpdateDetailByID"
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
    Public Property HL7_SendChargesSaveClose As Boolean
    Public Property HL7_SendChargesSaveFinish As Boolean
    Public Property Hl7_SendImmunizationDetails As Boolean
    Public Property Genius_SendChargesSaveClose As Boolean
    Public Property Genius_SendChargesSaveFinish As Boolean
    Public Property HL7_UseDefault As Boolean
    Public Property Genius_UseDefault As Boolean
    Public Property HL7_SendVisitSumSaveClose As Boolean
    Public Property HL7_SendVisitSumSaveFinish As Boolean

#End Region

#Region "Public function for Client Interface"

    Public Function ScanClientInterface(ByVal strProductName As String, ByVal strMachineName As String) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_ViewClientInterface"
        objCmd.Connection = objCon
        Dim objMachineName As New SqlParameter
        With objMachineName
            .ParameterName = "@sMachineName"
            .Value = strMachineName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.NVarChar
        End With
        objCmd.Parameters.Add(objMachineName)

        Dim objParaProduct As New SqlParameter
        With objParaProduct
            .ParameterName = "@sProductName"
            .Value = strProductName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.NVarChar
        End With
        objCmd.Parameters.Add(objParaProduct)
        objCmd.Connection = objCon
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objCon.Close()

        Dim nCount As Integer
        For nCount = 0 To dsData.Tables(0).Rows.Count - 1
            ClientSettings_InterfaceID = dsData.Tables(0).Rows.Item(nCount).Item("nClientSettings_InterfaceID")
            Hl7_SendPatientDetails = dsData.Tables(0).Rows.Item(nCount).Item("bHl7_SendPatientDetails")
            HL7_SendAppointmentDetails = dsData.Tables(0).Rows.Item(nCount).Item("bHL7_SendAppointmentDetails")
            HL7_SendChargesSaveClose = dsData.Tables(0).Rows.Item(nCount).Item("bHL7_SendChargesSaveClose")
            HL7_SendChargesSaveFinish = dsData.Tables(0).Rows.Item(nCount).Item("bHL7_SendChargesSaveFinish")
            Hl7_SendImmunizationDetails = dsData.Tables(0).Rows.Item(nCount).Item("bHl7_SendImmunizationDetails")
            Genius_SendChargesSaveClose = dsData.Tables(0).Rows.Item(nCount).Item("bGenius_SendChargesSaveClose")
            Genius_SendChargesSaveFinish = dsData.Tables(0).Rows.Item(nCount).Item("bGenius_SendChargesSaveFinish")
            HL7_UseDefault = dsData.Tables(0).Rows.Item(nCount).Item("bHL7_UseDefault")
            Genius_UseDefault = dsData.Tables(0).Rows.Item(nCount).Item("bGenius_UseDefault")
            HL7_SendVisitSumSaveClose = dsData.Tables(0).Rows.Item(nCount).Item("bHL7_SendVisitSumSaveClose")
            HL7_SendVisitSumSaveFinish = dsData.Tables(0).Rows.Item(nCount).Item("bHL7_SendVisitSumSaveFinish")
        Next
        ProductName = strProductName
        MachineName = strMachineName

        dsData = Nothing
        objCon = Nothing
        objDA = Nothing
        objCmd = Nothing
        Return True
    End Function

    Public Function InsertClientInterface(ByVal Inshl7 As Boolean, ByVal InsGen As Boolean) As Boolean
        Dim objHL7_SendVisitSumSaveClose As SqlParameter = Nothing
        Dim objHL7_SendVisitSumSaveFinish As SqlParameter = Nothing
        Try


            Dim objCon As New SqlConnection
            objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
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
            objClientSettings_InterfaceID = Nothing

            Dim objProductName As New SqlParameter
            With objProductName
                .ParameterName = "@sProductName"
                .Value = ProductName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.NVarChar

            End With
            objCmd.Parameters.Add(objProductName)
            objProductName = Nothing

            Dim objMachineName As New SqlParameter
            With objMachineName
                .ParameterName = "@sMachineName"
                .Value = MachineName
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.NVarChar
            End With
            objCmd.Parameters.Add(objMachineName)
            objMachineName = Nothing

            Dim objHl7_SendPatientDetails As New SqlParameter
            With objHl7_SendPatientDetails
                .ParameterName = "@bHl7_SendPatientDetails"
                .Value = Hl7_SendPatientDetails
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objHl7_SendPatientDetails)

            objHl7_SendPatientDetails = Nothing


            Dim objHL7_SendAppointmentDetails As New SqlParameter
            With objHL7_SendAppointmentDetails
                .ParameterName = "@bHL7_SendAppointmentDetails"
                .Value = HL7_SendAppointmentDetails
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objHL7_SendAppointmentDetails)
            objHL7_SendAppointmentDetails = Nothing


            Dim objHL7_SendChargesSaveClose As New SqlParameter
            With objHL7_SendChargesSaveClose
                .ParameterName = "@bHL7_SendChargesSaveClose"
                .Value = HL7_SendChargesSaveClose
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objHL7_SendChargesSaveClose)
            objHL7_SendChargesSaveClose = Nothing



            Dim objHL7_SendChargesSaveFinish As New SqlParameter
            With objHL7_SendChargesSaveFinish
                .ParameterName = "@bHL7_SendChargesSaveFinish"
                .Value = HL7_SendChargesSaveFinish
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objHL7_SendChargesSaveFinish)
            objHL7_SendChargesSaveFinish = Nothing


            Dim objHl7_SendImmunizationDetails As New SqlParameter
            With objHl7_SendImmunizationDetails
                .ParameterName = "@bHl7_SendImmunizationDetails"
                .Value = Hl7_SendImmunizationDetails
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objHl7_SendImmunizationDetails)
            objHl7_SendImmunizationDetails = Nothing

            Dim objhl7_useDefault As New SqlParameter
            With objhl7_useDefault
                .ParameterName = "@bHl7_usedefault"
                .Value = Inshl7
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objhl7_useDefault)
            objhl7_useDefault = Nothing



            Dim objGenius_SendChargesSaveClose As New SqlParameter
            With objGenius_SendChargesSaveClose
                .ParameterName = "@bGenius_SendChargesSaveClose"
                .Value = Genius_SendChargesSaveClose
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objGenius_SendChargesSaveClose)
            objGenius_SendChargesSaveClose = Nothing



            Dim objGenius_SendChargesSaveFinish As New SqlParameter
            With objGenius_SendChargesSaveFinish
                .ParameterName = "@bGenius_SendChargesSaveFinish"
                .Value = Genius_SendChargesSaveFinish
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objGenius_SendChargesSaveFinish)
            objGenius_SendChargesSaveFinish = Nothing

            Dim objGenius_useDefault As New SqlParameter
            With objGenius_useDefault
                .ParameterName = "@bGenius_usedefault"
                .Value = InsGen
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.Bit
            End With
            objCmd.Parameters.Add(objGenius_useDefault)
            objGenius_useDefault = Nothing

            'objHL7_SendVisitSumSaveClose
            objHL7_SendVisitSumSaveClose = New SqlParameter()
            objHL7_SendVisitSumSaveClose.ParameterName = "@bHL7_SendVisitSumSaveClose"
            objHL7_SendVisitSumSaveClose.Value = HL7_SendVisitSumSaveClose
            objHL7_SendVisitSumSaveClose.Direction = ParameterDirection.Input
            objHL7_SendVisitSumSaveClose.SqlDbType = SqlDbType.Bit
            objCmd.Parameters.Add(objHL7_SendVisitSumSaveClose)
            objHL7_SendVisitSumSaveClose = Nothing

            'objHL7_SendVisitSumSaveFinish
            objHL7_SendVisitSumSaveFinish= New SqlParameter()
            objHL7_SendVisitSumSaveFinish.ParameterName = "@bHL7_SendVisitSumSaveFinish"
            objHL7_SendVisitSumSaveFinish.Value = HL7_SendVisitSumSaveFinish
            objHL7_SendVisitSumSaveFinish.Direction = ParameterDirection.Input
            objHL7_SendVisitSumSaveFinish.SqlDbType = SqlDbType.Bit
            objCmd.Parameters.Add(objHL7_SendVisitSumSaveFinish)
            objHL7_SendVisitSumSaveFinish = Nothing

            objCmd.Connection = objCon
            objCon.Open()
            objCmd.ExecuteNonQuery()
            objCon.Close()
            objCmd = Nothing
            objCon = Nothing
            Return True

        Catch ex As Exception
            UpdateLog("InsertClientInterface() : " & ex.ToString())
            ex = Nothing
            Return False
        Finally
            objHL7_SendVisitSumSaveClose = Nothing
            objHL7_SendVisitSumSaveFinish = Nothing
        End Try
    End Function



    'Public Function InsertClientAllMachineInterface() As Boolean
    '    Dim objCon As New SqlConnection
    '    objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
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
    '        .Value = "1"                  ''for gloEMR product Code is 1
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


    '    Dim objHL7_SendChargesSaveClose As New SqlParameter
    '    With objHL7_SendChargesSaveClose
    '        .ParameterName = "@bHL7_SendChargesSaveClose"
    '        .Value = HL7_SendChargesSaveClose
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Bit
    '    End With
    '    objCmd.Parameters.Add(objHL7_SendChargesSaveClose)



    '    Dim objHL7_SendChargesSaveFinish As New SqlParameter
    '    With objHL7_SendChargesSaveFinish
    '        .ParameterName = "@bHL7_SendChargesSaveFinish"
    '        .Value = HL7_SendChargesSaveFinish
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Bit
    '    End With
    '    objCmd.Parameters.Add(objHL7_SendChargesSaveFinish)


    '    Dim objHl7_SendImmunizationDetails As New SqlParameter
    '    With objHl7_SendImmunizationDetails
    '        .ParameterName = "@bHl7_SendImmunizationDetails"
    '        .Value = Hl7_SendImmunizationDetails
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Bit
    '    End With
    '    objCmd.Parameters.Add(objHl7_SendImmunizationDetails)


    '    Dim objGenius_SendChargesSaveClose As New SqlParameter
    '    With objGenius_SendChargesSaveClose
    '        .ParameterName = "@bGenius_SendChargesSaveClose"
    '        .Value = Genius_SendChargesSaveClose
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Bit
    '    End With
    '    objCmd.Parameters.Add(objGenius_SendChargesSaveClose)



    '    Dim objGenius_SendChargesSaveFinish As New SqlParameter
    '    With objGenius_SendChargesSaveFinish
    '        .ParameterName = "@bGenius_SendChargesSaveFinish"
    '        .Value = Genius_SendChargesSaveFinish
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Bit
    '    End With
    '    objCmd.Parameters.Add(objGenius_SendChargesSaveFinish)


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
