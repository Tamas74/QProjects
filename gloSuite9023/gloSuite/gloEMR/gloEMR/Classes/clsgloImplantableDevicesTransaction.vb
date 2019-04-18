Imports System.Net
Imports System.Runtime.Serialization.Json
Imports System.Web.Script.Serialization
Imports System.IO
Public Class clsgloImplantableDevicesTransaction

    Private _transaction_id As Decimal
    Private _transaction_date As DateTime = Nothing
    Private _DeviceActive As Boolean
    Private _ProviderID As Decimal
    Private _manufacturer As String
    Private _expiration_date As String
    Private _Manufacturing_date As String
    Private _HCTP_Code As String
    Private _UDI As String
    Private _IssuingAgency As String
    Private _DeviceID As String
    Private _Version_Model As String
    Private _Brand_Name As String
    Private _LabeledContainingNRL As Boolean
    Private _MRIStaus As String
    Private _LotBatch As String
    Private _SerialNumber As String
    Private _user_id As Decimal
    Private _PatientID As Decimal
    Private _SMDescription As String
    Private _ConceptID As String
    Private _Visitid As Decimal
    Private _dtProc As DataTable
    Private _DeviceDescription As String
    Private _GmdnPTName As String
    Public _strStatusNotes As String = ""
    Public TranctionID As Int64 = -1

    Public Property ProviderID() As Decimal
        Get
            Return _ProviderID
        End Get
        Set(ByVal Value As Decimal)
            _ProviderID = Value
        End Set
    End Property
    Public Property DtProcedure() As DataTable
        Get
            Return _dtProc
        End Get
        Set(ByVal Value As DataTable)
            _dtProc = Value
        End Set
    End Property
    Public Property DeviceDescription() As String
        Get
            Return _DeviceDescription
        End Get
        Set(ByVal Value As String)
            _DeviceDescription = Value
        End Set
    End Property
    Public Property GmdnPTName() As String
        Get
            Return _GmdnPTName
        End Get
        Set(ByVal Value As String)
            _GmdnPTName = Value
        End Set
    End Property
    Public Property manufacturer() As String
        Get
            Return _manufacturer
        End Get
        Set(ByVal Value As String)
            _manufacturer = Value
        End Set
    End Property

    Public Property PatientID() As Decimal
        Get
            Return _PatientID
        End Get
        Set(ByVal Value As Decimal)
            _PatientID = Value
        End Set
    End Property
    Public Property VisitID() As Decimal
        Get
            Return _Visitid
        End Get
        Set(ByVal Value As Decimal)
            _Visitid = Value
        End Set
    End Property
    Public Property user_id() As Decimal
        Get
            Return _user_id
        End Get
        Set(ByVal Value As Decimal)
            _user_id = Value
        End Set
    End Property

    Public Property DeviceActive() As Boolean
        Get
            Return _DeviceActive
        End Get
        Set(ByVal Value As Boolean)
            _DeviceActive = Value
        End Set
    End Property
    Public Property SMDescription() As String
        Get
            Return _SMDescription
        End Get
        Set(ByVal Value As String)
            _SMDescription = Value
        End Set
    End Property
    Public Property ConceptID() As String
        Get
            Return _ConceptID
        End Get
        Set(ByVal Value As String)
            _ConceptID = Value
        End Set
    End Property
    Public Property transaction_date() As DateTime
        Get
            Return _transaction_date
        End Get
        Set(ByVal Value As DateTime)
            _transaction_date = Value
        End Set
    End Property

    Public Property transaction_id() As Decimal
        Get
            Return _transaction_id
        End Get
        Set(ByVal Value As Decimal)
            _transaction_id = Value
        End Set
    End Property
    Public Property expiration_date() As String
        Get
            Return _expiration_date
        End Get
        Set(ByVal Value As String)
            _expiration_date = Value
        End Set
    End Property

    Public Property Manufacturing_date() As String
        Get
            Return _Manufacturing_date
        End Get
        Set(ByVal Value As String)
            _Manufacturing_date = Value
        End Set
    End Property
    Public Property HCTP_Code() As String
        Get
            Return _HCTP_Code
        End Get
        Set(ByVal Value As String)
            _HCTP_Code = Value
        End Set
    End Property
    Public Property SerialNumber() As String
        Get
            Return _SerialNumber
        End Get
        Set(ByVal Value As String)
            _SerialNumber = Value
        End Set
    End Property
    Public Property LotBatchNumber() As String
        Get
            Return _LotBatch
        End Get
        Set(ByVal Value As String)
            _LotBatch = Value
        End Set
    End Property
    Public Property MRIStatus() As String
        Get
            Return _MRIStaus
        End Get
        Set(ByVal Value As String)
            _MRIStaus = Value
        End Set
    End Property
    Public Property LabeledContainingNRL() As String
        Get
            If _LabeledContainingNRL Then
                Return "Yes"
            Else
                Return "No"
            End If

        End Get
        Set(ByVal Value As String)
            If Value = "No" Then
                _LabeledContainingNRL = 0
            Else
                _LabeledContainingNRL = 1
            End If

        End Set
    End Property
    Public Property BrandName() As String
        Get
            Return _Brand_Name
        End Get
        Set(ByVal Value As String)
            _Brand_Name = Value
        End Set
    End Property
    Public Property VersionOrModel() As String
        Get
            Return _Version_Model
        End Get
        Set(ByVal Value As String)
            _Version_Model = Value
        End Set
    End Property
    Public Property IssuingAgency() As String
        Get
            Return _IssuingAgency
        End Get
        Set(ByVal Value As String)
            _IssuingAgency = Value
        End Set
    End Property
    Public Property DeviceID() As String
        Get
            Return _DeviceID
        End Get
        Set(ByVal Value As String)
            _DeviceID = Value
        End Set
    End Property
    Public Property UDI() As String
        Get
            Return _UDI
        End Get
        Set(ByVal Value As String)
            _UDI = Value
        End Set
    End Property
    Public Function AddIMTransaction() As Int64
        'Dim oDB As New gloStream.gloDataBase.gloDataBase

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()

        Try
            oDB.Connect(False)
            oDBParameters.Clear()
            oDBParameters.Add("@Patientid", _PatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@transaction_id", _transaction_id, ParameterDirection.InputOutput, SqlDbType.BigInt)
            If _transaction_date <> "#12:00:00 AM#" Then
                oDBParameters.Add("@transaction_date", _transaction_date, ParameterDirection.Input, SqlDbType.DateTime)
            End If
            oDBParameters.Add("@nProviderID", _ProviderID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@DeviceID", _DeviceID, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@IssuingAgency", _IssuingAgency, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@BrandName", _Brand_Name, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@manufacturer", _manufacturer, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@VersionModel", _Version_Model, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@MRISafetyStatus", _MRIStaus, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@bLabelContainingNRL", _LabeledContainingNRL, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameters.Add("@serialnumber", _SerialNumber, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@lotbatchnumber", _LotBatch, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@manufacturingdate", _Manufacturing_date, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@expirydate", _expiration_date, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@DeviceHCTPCode", _HCTP_Code, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@DeviceStatus", _DeviceActive, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameters.Add("@UDI", _UDI, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@user_id", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@ConceptID", _ConceptID, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@sDescription", _SMDescription, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@nVisitID", _Visitid, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@TVP_procedures", _dtProc, ParameterDirection.Input, SqlDbType.Structured)
            oDBParameters.Add("@sDeviceDescription", _DeviceDescription, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@sGmdnPTName", _GmdnPTName, ParameterDirection.Input, SqlDbType.Text)
            oDBParameters.Add("@sStatusReason", _strStatusNotes, ParameterDirection.Input, SqlDbType.Text)
            TranctionID = -1
            oDB.Execute("gsp_InUpPatientImplantableDevice", oDBParameters, TranctionID)

            Return TranctionID
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return Nothing
        Finally
            If (IsNothing(oDBParameters) = False) Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If
            If (IsNothing(oDB) = False) Then
                oDB.Disconnect()
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try

    End Function

    Public Function ShowDeviceInformation(ByVal _DeviceID As String) As DataTable


        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oDBParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim dtIM As DataTable = Nothing

        Try

            oDB.Connect(False)
            oDBParameters = New gloDatabaseLayer.DBParameters
            oDBParameters.Add("@sDeviceID", _DeviceID, ParameterDirection.Input, SqlDbType.Text)
            oDB.Retrive("gsp_ScanImplantableDeviceSetup", oDBParameters, dtIM)

        Catch ex As Exception


            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB = Nothing
            End If
        End Try
        Return dtIM

    End Function
    Public Function get_Procedures(ByVal _DeviceList_ID As Int64, ByVal _nPatientID As Int64) As DataTable

        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oDBParameters As gloDatabaseLayer.DBParameters = Nothing
        Dim dtIM As DataTable = Nothing

        Try

            oDB.Connect(False)
            oDBParameters = New gloDatabaseLayer.DBParameters
            oDBParameters.Add("@nDeviceListID", _DeviceList_ID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nPatientID", _nPatientID, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("gsp_GetSelectedProcedures", oDBParameters, dtIM)

        Catch ex As Exception


            MessageBox.Show("Error on Immunization." & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
                oDBParameters = Nothing
            End If

            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB = Nothing
            End If
        End Try
        Return dtIM

    End Function
End Class
Public Class Parse_UDI
    Public UDI As String = ""
    Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings

    Private Function GetParserURL() As String
        Dim objSettings As New clsSettings
        Dim sURL As String = objSettings.GetSettingValue("UDI API URL")
        If sURL = "" Then
            sURL = "https://accessgudid.nlm.nih.gov/api/v1/parse_udi.json"
        End If
        If sURL.Substring((sURL.Length) - 1, 1) = "/" Or sURL.Substring((sURL.Length) - 1, 1) = "\" Then
            sURL = sURL.Substring(0, (sURL.Length) - 1)
        End If
        Return sURL
    End Function
    Dim sParseParameter As String = "/parse_udi.json?udi="
    Dim sLookupParameter As String = "/devices/lookup.json?di="
    
    Public Function UDI_Parser() As APIResponse
        Try
            Dim request As HttpWebRequest = DirectCast(WebRequest.Create(GetParserURL() + sParseParameter + UDI), HttpWebRequest)
            Dim response As HttpWebResponse = Nothing
            Try
                response = DirectCast(request.GetResponse(), HttpWebResponse)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show("Enter valid UDI and try again.                        ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            If Not response Is Nothing Then
                Dim jsonSerializer As New DataContractJsonSerializer(GetType(APIResponse))
                Dim objResponse As Object = jsonSerializer.ReadObject(response.GetResponseStream())
                Return TryCast(objResponse, APIResponse)
            End If

            If Not request Is Nothing Then
                request = Nothing
            End If
            If Not response Is Nothing Then
                response = Nothing
            End If
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally

        End Try
    End Function
    Public Function UDI_Lookup() As String
        Dim request As HttpWebRequest = Nothing
        Dim response As HttpWebResponse = Nothing
        Dim objResponse As Object = Nothing
        Dim jdeserialize As JavaScriptSerializer = Nothing
        Try
            Dim sDeviceDescription As String = String.Empty
            Dim sGMDNPTName As String = String.Empty
            request = DirectCast(WebRequest.Create(GetParserURL() + sLookupParameter + UDI), HttpWebRequest)            
            Try
                response = DirectCast(request.GetResponse(), HttpWebResponse)
                Dim json As String = New StreamReader(response.GetResponseStream()).ReadToEnd()

                jdeserialize = New JavaScriptSerializer()
                objResponse = jdeserialize.Deserialize(Of Object)(json)
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
                MessageBox.Show("Enter valid UDI and try again.                        ", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            If Not objResponse Is Nothing Then
                For Each skey As KeyValuePair(Of String, Object) In objResponse
                    For Each skey1 As KeyValuePair(Of String, Object) In skey.Value
                        For Each sDevice As KeyValuePair(Of String, Object) In skey1.Value
                            If sDevice.Key = "deviceDescription" Then
                                sDeviceDescription = sDevice.Value
                            ElseIf sDevice.Key = "gmdnTerms" Then
                                For Each sgmdnterms As KeyValuePair(Of String, Object) In sDevice.Value
                                    If sgmdnterms.Key = "gmdn" Then
                                        For Each sgmdn As KeyValuePair(Of String, Object) In sgmdnterms.Value
                                            If sgmdn.Key = "gmdnPTName" Then
                                                sGMDNPTName = sgmdn.Value
                                                Exit For
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                            If sDeviceDescription <> "" And sGMDNPTName <> "" Then
                                Return sDeviceDescription + "|" + sGMDNPTName
                            End If
                        Next
                    Next
                Next
            End If
            Return sDeviceDescription + "|" + sGMDNPTName


        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.ImplantableDevice, gloAuditTrail.ActivityCategory.ViewTransaction, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return ""
        Finally
            If Not request Is Nothing Then
                request = Nothing
            End If
            If Not response Is Nothing Then
                response = Nothing
            End If
            If Not jdeserialize Is Nothing Then
                jdeserialize = Nothing
            End If
        End Try
    End Function
End Class

Public Class APIResponse
    Public Property udi() As String
        Get
            Return m_udi
        End Get
        Set(value As String)
            m_udi = Value
        End Set
    End Property
    Private m_udi As String
    Public Property issuing_agency() As String
        Get
            Return m_issuing_agency
        End Get
        Set(value As String)
            m_issuing_agency = Value
        End Set
    End Property
    Private m_issuing_agency As String
    Public Property di() As String
        Get
            Return m_di
        End Get
        Set(value As String)
            m_di = Value
        End Set
    End Property
    Private m_di As String
    Public Property manufacturing_date_original() As String
        Get
            Return m_manufacturing_date_original
        End Get
        Set(value As String)
            m_manufacturing_date_original = Value
        End Set
    End Property
    Private m_manufacturing_date_original As String
    Public Property manufacturing_date_original_format() As String
        Get
            Return m_manufacturing_date_original_format
        End Get
        Set(value As String)
            m_manufacturing_date_original_format = Value
        End Set
    End Property
    Private m_manufacturing_date_original_format As String
    'YYMMDD
    Public Property manufacturing_date() As String
        Get
            Return m_manufacturing_date
        End Get
        Set(value As String)
            m_manufacturing_date = Value
        End Set
    End Property
    Private m_manufacturing_date As String
    Public Property expiration_date_original() As String
        Get
            Return m_expiration_date_original
        End Get
        Set(value As String)
            m_expiration_date_original = Value
        End Set
    End Property
    Private m_expiration_date_original As String
    Public Property expiration_date_original_format() As String
        Get
            Return m_expiration_date_original_format
        End Get
        Set(value As String)
            m_expiration_date_original_format = Value
        End Set
    End Property
    Private m_expiration_date_original_format As String
    'YYMMDD
    Public Property expiration_date() As String
        Get
            Return m_expiration_date
        End Get
        Set(value As String)
            m_expiration_date = Value
        End Set
    End Property
    Private m_expiration_date As String
    Public Property lot_number() As String
        Get
            Return m_lot_number
        End Get
        Set(value As String)
            m_lot_number = Value
        End Set
    End Property
    Private m_lot_number As String
    Public Property serial_number() As String
        Get
            Return m_serial_number
        End Get
        Set(value As String)
            m_serial_number = Value
        End Set
    End Property
    Private m_serial_number As String
    Public Property donation_id() As String
        Get
            Return m_donation_id
        End Get
        Set(value As String)
            m_donation_id = Value
        End Set
    End Property
    Private m_donation_id As String
End Class
