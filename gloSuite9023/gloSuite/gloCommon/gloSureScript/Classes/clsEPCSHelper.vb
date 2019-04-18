Imports System.IO
Imports System.Xml.Serialization
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Windows.Forms
Imports System.Xml
Imports System.Linq
Imports gloGlobal



Public Class EpcsRequest
    Public Property FlagEpcsSeviceCall() As EpcsSeviceCall
        Get
            Return m_FlagEpcsSeviceCall
        End Get
        Set(value As EpcsSeviceCall)
            m_FlagEpcsSeviceCall = value
        End Set
    End Property
    Private m_FlagEpcsSeviceCall As EpcsSeviceCall

    Public Property RequestHeder() As clsEpcsRequestHeder
        Get
            Return m_RequestHeder
        End Get
        Set(value As clsEpcsRequestHeder)
            m_RequestHeder = value
        End Set
    End Property
    Private m_RequestHeder As clsEpcsRequestHeder
    Public Property RequestBody() As Object
        Get
            Return m_RequestBody
        End Get
        Set(value As Object)
            m_RequestBody = value
        End Set
    End Property
    Private m_RequestBody As Object
End Class

Public Class clsEpcsRequestHeder

    Public Property OrganizationName() As String
        Get
            Return m_OrganizationName
        End Get
        Set(value As String)
            m_OrganizationName = value
        End Set
    End Property
    Private m_OrganizationName As String
    Public Property OrganizationLabel() As String
        Get
            Return m_OrganizationLabel
        End Get
        Set(value As String)
            m_OrganizationLabel = value
        End Set
    End Property
    Private m_OrganizationLabel As String

    Public Property VendorName() As String
        Get
            Return m_VendorName
        End Get
        Set(value As String)
            m_VendorName = value
        End Set
    End Property
    Private m_VendorName As String

    Public Property VendorLabel() As String
        Get
            Return m_VendorLabel
        End Get
        Set(value As String)
            m_VendorLabel = value
        End Set
    End Property
    Private m_VendorLabel As String
    Public Property VendorNodeLabel() As String
        Get
            Return m_VendorNodeLabel
        End Get
        Set(value As String)
            m_VendorNodeLabel = value
        End Set
    End Property
    Private m_VendorNodeLabel As String
    Public Property VendorNodeName() As String
        Get
            Return m_VendorNodeName
        End Get
        Set(value As String)
            m_VendorNodeName = value
        End Set
    End Property
    Private m_VendorNodeName As String
    Public Property AppName() As String
        Get
            Return m_AppName
        End Get
        Set(value As String)
            m_AppName = value
        End Set
    End Property
    Private m_AppName As String
    Public Property ApplicationVersion() As String
        Get
            Return m_ApplicationVersion
        End Get
        Set(value As String)
            m_ApplicationVersion = value
        End Set
    End Property
    Private m_ApplicationVersion As String
    Public Property SourceOrganizationId() As String
        Get
            Return m_SourceOrganizationId
        End Get
        Set(value As String)
            m_SourceOrganizationId = value
        End Set
    End Property
    Private m_SourceOrganizationId As String
    Public Property HeaderDate() As String
        Get
            Return m_HeaderDate
        End Get
        Set(value As String)
            m_HeaderDate = value
        End Set
    End Property
    Private m_HeaderDate As String
    Public Property LogoUrl() As String
        Get
            Return m_LogoUrl
        End Get
        Set(value As String)
            m_LogoUrl = value
        End Set
    End Property
    Private m_LogoUrl As String
End Class


Public Enum EpcsSeviceCall
    WSSetUpOrganization = 0
    WSInvitePrescriber = 1
    WSGetPrescriberStatus = 2
    WSGetPrescriberToken = 3
    WSDisablePrescriberToken = 4
    WSGetPrescriptionStatus = 5
    WSEPCSDrugCheckService = 6
    UILaunchSigning = 7
    WSUpdatePrescriptionStatus = 9
    WSEPCSDrugCheckServiceMultiple = 10
End Enum

Public Class clsEPCSHelper

    Public Sub New()
    End Sub

    Public Function GetFileName(ByVal strAppPath As [String]) As [String]
        Try
            Return gloGlobal.clsFileExtensions.NewDocumentName(strAppPath, ".xml", "MMddyyyyHHmmssffff")
        Catch ex As Exception
            Return ""
        Finally
        End Try
    End Function

    Public Function generateEpcsRequestHeader(valCase As EpcsSeviceCall, requestBody As Object) As Object
        Dim request As clsEpcsRequestHeder = requestBody
        Select Case valCase
            Case EpcsSeviceCall.WSEPCSDrugCheckService, EpcsSeviceCall.WSEPCSDrugCheckServiceMultiple
                Dim requestHeader As WSEPCSDrugCheckServiceMultiple.EpcsRequestEpcsRequestHeader = New WSEPCSDrugCheckServiceMultiple.EpcsRequestEpcsRequestHeader
                requestHeader.VendorName = request.VendorName
                requestHeader.VendorLabel = request.VendorLabel
                requestHeader.VendorNodeLabel = request.VendorNodeLabel
                requestHeader.VendorNodeName = request.VendorNodeName
                requestHeader.SourceOrganizationId = request.SourceOrganizationId
                requestHeader.Date = request.HeaderDate
                requestHeader.EpcsApiVersion = "2.4"
                requestHeader.AppVersion = New WSEPCSDrugCheckServiceMultiple.EpcsRequestEpcsRequestHeaderAppVersion
                requestHeader.AppVersion.AppName = request.AppName
                requestHeader.AppVersion.ApplicationVersion = request.ApplicationVersion
                Return requestHeader

            Case EpcsSeviceCall.UILaunchSigning
                Dim requestHeader As UILaunchSigning.EpcsRequest = New UILaunchSigning.EpcsRequest()
                requestHeader.EpcsRequestHeader = New UILaunchSigning.EpcsRequestEpcsRequestHeader()
                requestHeader.EpcsRequestHeader.VendorName = request.VendorName
                requestHeader.EpcsRequestHeader.VendorLabel = request.VendorLabel
                requestHeader.EpcsRequestHeader.VendorNodeName = request.VendorNodeName
                requestHeader.EpcsRequestHeader.VendorNodeLabel = request.VendorNodeLabel
                requestHeader.EpcsRequestHeader.AppVersion = New UILaunchSigning.EpcsRequestEpcsRequestHeaderAppVersion()
                requestHeader.EpcsRequestHeader.AppVersion.AppName = request.AppName
                requestHeader.EpcsRequestHeader.AppVersion.ApplicationVersion = request.ApplicationVersion
                requestHeader.EpcsRequestHeader.SourceOrganizationId = request.SourceOrganizationId
                requestHeader.EpcsRequestHeader.Date = request.HeaderDate
                requestHeader.EpcsRequestHeader.PrivateLabel = New UILaunchSigning.EpcsRequestEpcsRequestHeaderPrivateLabel()
                requestHeader.EpcsRequestHeader.PrivateLabel.ApplicationName = "QEMR"
                requestHeader.EpcsRequestHeader.PrivateLabel.LogoUrl = request.LogoUrl
                Return (requestHeader.EpcsRequestHeader)

            Case EpcsSeviceCall.WSGetPrescriptionStatus
                Dim requestHeader As WSGetPrescriptionStatus.EpcsRequest = New WSGetPrescriptionStatus.EpcsRequest()
                requestHeader.EpcsRequestHeader = New WSGetPrescriptionStatus.EpcsRequestEpcsRequestHeader()
                requestHeader.EpcsRequestHeader.VendorName = request.VendorName
                requestHeader.EpcsRequestHeader.VendorLabel = request.VendorLabel
                requestHeader.EpcsRequestHeader.VendorNodeName = request.VendorNodeName
                requestHeader.EpcsRequestHeader.VendorNodeLabel = request.VendorNodeLabel
                requestHeader.EpcsRequestHeader.AppVersion = New WSGetPrescriptionStatus.EpcsRequestEpcsRequestHeaderAppVersion()
                requestHeader.EpcsRequestHeader.AppVersion.AppName = request.AppName
                requestHeader.EpcsRequestHeader.AppVersion.ApplicationVersion = request.ApplicationVersion
                requestHeader.EpcsRequestHeader.SourceOrganizationId = request.SourceOrganizationId
                requestHeader.EpcsRequestHeader.Date = request.HeaderDate
                Return (requestHeader.EpcsRequestHeader)

            Case EpcsSeviceCall.WSUpdatePrescriptionStatus
                Dim requestHeader As WSUpdatePrescriptionStatus.EpcsRequest = New WSUpdatePrescriptionStatus.EpcsRequest()
                requestHeader.EpcsRequestHeader = New WSUpdatePrescriptionStatus.EpcsRequestEpcsRequestHeader()
                requestHeader.EpcsRequestHeader.VendorName = request.VendorName
                requestHeader.EpcsRequestHeader.VendorLabel = request.VendorLabel
                requestHeader.EpcsRequestHeader.VendorNodeName = request.VendorNodeName
                requestHeader.EpcsRequestHeader.VendorNodeLabel = request.VendorNodeLabel
                requestHeader.EpcsRequestHeader.AppVersion = New WSUpdatePrescriptionStatus.EpcsRequestEpcsRequestHeaderAppVersion()
                requestHeader.EpcsRequestHeader.AppVersion.AppName = request.AppName
                requestHeader.EpcsRequestHeader.AppVersion.ApplicationVersion = request.ApplicationVersion
                requestHeader.EpcsRequestHeader.SourceOrganizationId = request.SourceOrganizationId
                requestHeader.EpcsRequestHeader.Date = request.HeaderDate
                Return (requestHeader.EpcsRequestHeader)

            Case Else
                Return Nothing
        End Select

    End Function

    Public Function generateEpcsRequestBody(valCase As EpcsSeviceCall, requestObj As Object, ByVal RefillItem As Int16, ByRef erxFiles As List(Of Dictionary(Of String, String))()) As Object
        Try

            Select Case valCase
                Case EpcsSeviceCall.WSEPCSDrugCheckService
                    Dim request As EPrescription = requestObj
                    Dim oDrugInfo As EDrug = request.DrugsCol.Item(RefillItem)
                    Dim requestBody As WSEPCSDrugCheckServiceMultiple.EpcsRequestEpcsRequestBody = New WSEPCSDrugCheckServiceMultiple.EpcsRequestEpcsRequestBody()
                    requestBody.WsGetEPCSDrugCheckRequest = New WSEPCSDrugCheckServiceMultiple.EpcsRequestEpcsRequestBodyWsGetEPCSDrugCheckRequest()
                    Dim _list As New List(Of WSEPCSDrugCheckServiceMultiple.EpcsRequestEpcsRequestBodyWsGetEPCSDrugCheckRequestEPCSDrugPermissionStatusType)
                    Dim _Item As New WSEPCSDrugCheckServiceMultiple.EpcsRequestEpcsRequestBodyWsGetEPCSDrugCheckRequestEPCSDrugPermissionStatusType
                    _Item.NcpdpId = String.Format(oDrugInfo.PhNCPDPID, "0000000")
                    _Item.DrugName = oDrugInfo.DrugName
                    _Item.NdcId = String.Format(oDrugInfo.ProdCode, "00000000000")
                    _Item.PrescriberStateCode = request.RxPrescriber.PrescriberAddress.State '"CA"
                    _list.Add(_Item)
                    requestBody.WsGetEPCSDrugCheckRequest.EPCSDrugCheckRequestListType = _list.ToArray()
                    Return requestBody
                Case EpcsSeviceCall.WSEPCSDrugCheckServiceMultiple
                    Dim request As EPrescription = requestObj
                    Dim oDrugInfo As EDrug = request.DrugsCol.Item(RefillItem)
                    Dim requestBody As WSEPCSDrugCheckServiceMultiple.EpcsRequestEpcsRequestBody = New WSEPCSDrugCheckServiceMultiple.EpcsRequestEpcsRequestBody()
                    requestBody.WsGetEPCSDrugCheckRequest = New WSEPCSDrugCheckServiceMultiple.EpcsRequestEpcsRequestBodyWsGetEPCSDrugCheckRequest()
                    Dim _list As New List(Of WSEPCSDrugCheckServiceMultiple.EpcsRequestEpcsRequestBodyWsGetEPCSDrugCheckRequestEPCSDrugPermissionStatusType)
                    Dim lstProductIDs As List(Of String) = Nothing
                    Using oDIBGSHelper As New gloGlobal.DIB.gloGSHelper(gloSurescriptGeneral.sDIBServiceURL)
                        lstProductIDs = oDIBGSHelper.GetAlternativeNDCS(request.DrugsCol.Item(RefillItem).mpid, request.DrugsCol.Item(RefillItem).ProdCode)
                    End Using
                    If lstProductIDs IsNot Nothing Then
                        For Each ndc As String In lstProductIDs
                            Dim _InnerItem As New WSEPCSDrugCheckServiceMultiple.EpcsRequestEpcsRequestBodyWsGetEPCSDrugCheckRequestEPCSDrugPermissionStatusType
                            _InnerItem.NcpdpId = String.Format(oDrugInfo.PhNCPDPID, "0000000")
                            _InnerItem.DrugName = oDrugInfo.DrugName
                            _InnerItem.NdcId = String.Format(ndc, "00000000000")
                            _InnerItem.PrescriberStateCode = request.RxPrescriber.PrescriberAddress.State
                            _list.Add(_InnerItem)
                        Next
                    End If
                    requestBody.WsGetEPCSDrugCheckRequest.EPCSDrugCheckRequestListType = _list.ToArray()
                    Return requestBody
                Case EpcsSeviceCall.UILaunchSigning
                    Dim request As EPrescription = requestObj
                    Dim reqBody As UILaunchSigning.EpcsRequest = New UILaunchSigning.EpcsRequest()
                    reqBody.EpcsRequestBody = New UILaunchSigning.EpcsRequestEpcsRequestBody()

                    reqBody.EpcsRequestBody.UiLaunchSigningRequest = New UILaunchSigning.EpcsRequestEpcsRequestBodyUiLaunchSigningRequest()
                    Dim STRNumber As String = String.Empty
                    STRNumber = System.Guid.NewGuid.ToString()
                    reqBody.EpcsRequestBody.UiLaunchSigningRequest.SourceTransactionReferenceNumber = "gloEPCS" & gloSurescriptGeneral.GetUniqueID()  '"cdd372282e64464bb729a0a68a2397dc"
                    reqBody.EpcsRequestBody.UiLaunchSigningRequest.PrescriberNpi = request.RxPrescriber.PrescriberNPI  '"5627182012"
                    reqBody.EpcsRequestBody.UiLaunchSigningRequest.PatientPrescriptionDataList = New UILaunchSigning.EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataList()
                    reqBody.EpcsRequestBody.UiLaunchSigningRequest.PatientPrescriptionDataList.PatientPrescriptionData = New UILaunchSigning.EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataListPatientPrescriptionData()
                    reqBody.EpcsRequestBody.UiLaunchSigningRequest.PatientPrescriptionDataList.PatientPrescriptionData.Patient = New UILaunchSigning.EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataListPatientPrescriptionDataPatient()
                    reqBody.EpcsRequestBody.UiLaunchSigningRequest.PatientPrescriptionDataList.PatientPrescriptionData.Patient.ExternalPatientId = request.PatientID '"3161315"
                    reqBody.EpcsRequestBody.UiLaunchSigningRequest.PatientPrescriptionDataList.PatientPrescriptionData.Patient.Firstname = request.RxPatient.PatientName.FirstName   '"Elizabeth"
                    reqBody.EpcsRequestBody.UiLaunchSigningRequest.PatientPrescriptionDataList.PatientPrescriptionData.Patient.Lastname = request.RxPatient.PatientName.LastName '"Tilman"
                    reqBody.EpcsRequestBody.UiLaunchSigningRequest.PatientPrescriptionDataList.PatientPrescriptionData.Patient.Middlename = request.RxPatient.PatientName.MiddleName '"Elizabeth"


                    If request.RxPatient.PatientName.Suffix IsNot Nothing AndAlso Not String.IsNullOrEmpty(request.RxPatient.PatientName.Suffix) AndAlso Not String.IsNullOrWhiteSpace(request.RxPatient.PatientName.Suffix) Then
                        reqBody.EpcsRequestBody.UiLaunchSigningRequest.PatientPrescriptionDataList.PatientPrescriptionData.Patient.Suffix = request.RxPatient.PatientName.Suffix
                    End If

                    If request.RxPatient.PatientName.Prefix IsNot Nothing AndAlso Not String.IsNullOrEmpty(request.RxPatient.PatientName.Prefix) AndAlso Not String.IsNullOrWhiteSpace(request.RxPatient.PatientName.Prefix) Then
                        reqBody.EpcsRequestBody.UiLaunchSigningRequest.PatientPrescriptionDataList.PatientPrescriptionData.Patient.Prefix = request.RxPatient.PatientName.Prefix
                    End If

                    reqBody.EpcsRequestBody.UiLaunchSigningRequest.PatientPrescriptionDataList.PatientPrescriptionData.Patient.Gender = gloSureScript.gloSureScriptInterface.GetGender(request.RxPatient.Gender)  '"F"
                    Dim DOB As Date = CDate(request.RxPatient.DateofBirth)
                    reqBody.EpcsRequestBody.UiLaunchSigningRequest.PatientPrescriptionDataList.PatientPrescriptionData.Patient.Dateofbirth = DOB.Year & DOB.Month.ToString("D2") & DOB.Day.ToString("D2") '"19841211"               

                    reqBody.EpcsRequestBody.UiLaunchSigningRequest.PatientPrescriptionDataList.PatientPrescriptionData.Patient.AddressLine1 = request.RxPatient.PatientAddress.Address1

                    If request.RxPatient.PatientAddress.Address2 IsNot Nothing AndAlso Not String.IsNullOrEmpty(request.RxPatient.PatientAddress.Address2) AndAlso Not String.IsNullOrWhiteSpace(request.RxPatient.PatientAddress.Address2) Then
                        reqBody.EpcsRequestBody.UiLaunchSigningRequest.PatientPrescriptionDataList.PatientPrescriptionData.Patient.Addressline2 = request.RxPatient.PatientAddress.Address2
                    End If

                    reqBody.EpcsRequestBody.UiLaunchSigningRequest.PatientPrescriptionDataList.PatientPrescriptionData.Patient.City = request.RxPatient.PatientAddress.City
                    reqBody.EpcsRequestBody.UiLaunchSigningRequest.PatientPrescriptionDataList.PatientPrescriptionData.Patient.State = request.RxPatient.PatientAddress.State
                    reqBody.EpcsRequestBody.UiLaunchSigningRequest.PatientPrescriptionDataList.PatientPrescriptionData.Patient.Zipcode = request.RxPatient.PatientAddress.Zip '"90248"

                    Dim PrescrList As New List(Of UILaunchSigning.EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataListPatientPrescriptionDataPrescription)
                    Dim Pres As UILaunchSigning.EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataListPatientPrescriptionDataPrescription

                    For Each strValue As KeyValuePair(Of String, String) In request.eRxMultipleFilePath
                        Pres = New UILaunchSigning.EpcsRequestEpcsRequestBodyUiLaunchSigningRequestPatientPrescriptionDataListPatientPrescriptionDataPrescription
                        Pres.SourcePrescriptionId = strValue.Key
                        Pres.SSMessage = GetSSMessage_xml(strValue.Value)  'GetSSMessage(strValue.Value)
                        PrescrList.Add(Pres)
                        Pres = Nothing
                    Next
                    reqBody.EpcsRequestBody.UiLaunchSigningRequest.PatientPrescriptionDataList.PatientPrescriptionData.PrescriptionList = PrescrList.ToArray()
                    reqBody.EpcsRequestBody.UiLaunchSigningRequest.RouterLabel = request.RouterName '"glostream_router_label"
                    reqBody.EpcsRequestBody.UiLaunchSigningRequest.RoutePrescriptionViaEpcs = "y"
                    reqBody.EpcsRequestBody.UiLaunchSigningRequest.CompressResponseWithGzip = "n"

                    Return reqBody.EpcsRequestBody

                Case EpcsSeviceCall.WSGetPrescriptionStatus
                    Dim request As EPrescription = requestObj
                    Dim reqPreBody As WSGetPrescriptionStatus.EpcsRequest = New WSGetPrescriptionStatus.EpcsRequest()
                    reqPreBody.EpcsRequestBody = New WSGetPrescriptionStatus.EpcsRequestEpcsRequestBody()
                    reqPreBody.EpcsRequestBody.WsGetPrescriptionStatusRequest = New WSGetPrescriptionStatus.EpcsRequestEpcsRequestBodyWsGetPrescriptionStatusRequest()

                    Dim PrescrstatusList As New List(Of WSGetPrescriptionStatus.EpcsRequestEpcsRequestBodyWsGetPrescriptionStatusRequestPrescriptionRequestList)
                    Dim PresStatus As WSGetPrescriptionStatus.EpcsRequestEpcsRequestBodyWsGetPrescriptionStatusRequestPrescriptionRequestList
                    For Each strValue As KeyValuePair(Of String, String) In request.eRxMultipleFilePath
                        PresStatus = New WSGetPrescriptionStatus.EpcsRequestEpcsRequestBodyWsGetPrescriptionStatusRequestPrescriptionRequestList
                        PresStatus.SourcePrescriptionId = strValue.Key
                        PrescrstatusList.Add(PresStatus)
                        PresStatus = Nothing
                    Next
                    reqPreBody.EpcsRequestBody.WsGetPrescriptionStatusRequest.PrescriptionRequestList = PrescrstatusList.ToArray()

                    Return reqPreBody.EpcsRequestBody
                Case EpcsSeviceCall.WSUpdatePrescriptionStatus
                    Dim reqPreBody As WSUpdatePrescriptionStatus.EpcsRequest = New WSUpdatePrescriptionStatus.EpcsRequest()
                    reqPreBody.EpcsRequestBody = New WSUpdatePrescriptionStatus.EpcsRequestEpcsRequestBody()
                    reqPreBody.EpcsRequestBody.WsUpdatePrescriptionStatusRequest = New WSUpdatePrescriptionStatus.EpcsRequestEpcsRequestBodyWsUpdatePrescriptionStatusRequest()
                    reqPreBody.EpcsRequestBody.WsUpdatePrescriptionStatusRequest.SSMessage = GetSSMessage_xml(requestObj.ToString()) 'GetSSMessage_xml(requestObj.ToString()) '"[b64]" & requestObj.ToString()
                    Return (reqPreBody.EpcsRequestBody)

                Case Else
                    Return Nothing
            End Select
        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog("Error Occurred processing EPCS generateEpcsRequestBody : " & ex.Message)
            Return Nothing
        End Try
    End Function
    Private Function GetSSMessage(ByVal seRxFilePath As String) As String
        Dim _resultMessage As String = String.Empty
        Try
            Dim xdoc As Xml.XmlDocument = New Xml.XmlDocument()
            xdoc.Load(seRxFilePath)
            'Dim query = From c In xdoc.GetElementsByTagName("NewRx") Select c
            _resultMessage = "[b64]" & Convert.ToBase64String(Encoding.UTF8.GetBytes(xdoc.InnerXml))
            ' _resultMessage = xdoc.InnerXml
            xdoc = Nothing
        Catch ex As Exception
            Throw ex
        End Try
        Return _resultMessage
    End Function
    Private Function GetSSMessage_xml(ByVal seRxFilePath As String) As XmlDocument
        Dim _resultMessage As XmlDocument = Nothing
        Try
            Dim xdoc As Xml.XmlDocument = New Xml.XmlDocument()
            xdoc.Load(seRxFilePath)
            'Dim query = From c In xdoc.GetElementsByTagName("NewRx") Select c
            '_resultMessage = "[b64]" & Convert.ToBase64String(Encoding.UTF8.GetBytes(FormatXML(xdoc.InnerXml)))
            _resultMessage = xdoc
            xdoc = Nothing
        Catch ex As Exception
            Throw ex
        End Try
        Return _resultMessage
    End Function
  
    Public Function RemoveNamespace(xml As String) As String
        Dim xmlnsPattern As String = "\s+xmlns:xsd\s*(:\w)?\s*=\s*\""(?<url>[^\""]*)\"""
        Dim matchCol As MatchCollection = Regex.Matches(xml, xmlnsPattern)
        For Each m As Match In matchCol
            xml = xml.Replace(m.ToString(), "")
        Next
        Return xml
    End Function

    Public Function GenerateWSEPCSDrugCheckService(epcsRequest As EpcsRequest, ByVal RefillItem As Int16) As String
        Dim requestFile As String = ""
        Try

            Dim request As WSEPCSDrugCheckServiceMultiple.EpcsRequest = New WSEPCSDrugCheckServiceMultiple.EpcsRequest()
            If Not Directory.Exists(gloSettings.FolderSettings.AppTempFolderPath + "Outbox") Then
                Directory.CreateDirectory(gloSettings.FolderSettings.AppTempFolderPath + "Outbox")
            End If
            requestFile = GetFileName(gloSettings.FolderSettings.AppTempFolderPath + "Outbox") 'System.AppDomain.CurrentDomain.BaseDirectory + "Outbox\\\\" + System.DateTime.Now.ToString("yyyyMMddhhmmss") + ".xml"
            request.EpcsRequestBody = generateEpcsRequestBody(epcsRequest.FlagEpcsSeviceCall, epcsRequest.RequestBody, RefillItem, Nothing)
            request.EpcsRequestHeader = generateEpcsRequestHeader(epcsRequest.FlagEpcsSeviceCall, epcsRequest.RequestHeder)

            If request IsNot Nothing Then
                Dim serializer As New XmlSerializer(GetType(WSEPCSDrugCheckServiceMultiple.EpcsRequest))
                Using file As New System.IO.StreamWriter(requestFile)
                    serializer.Serialize(file, request)
                    file.Close()
                End Using
                serializer = Nothing
            End If
            Dim doc As Xml.XmlDocument = New Xml.XmlDocument()
            doc.Load(requestFile)
            doc.InnerXml = RemoveNamespace(doc.InnerXml)
            doc.Save(requestFile)
            doc = Nothing
            request = Nothing
        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog("Error Occurred processing EPCS GenerateWSEPCSDrugCheckService : " & ex.Message)
        End Try
        Return requestFile
    End Function


    Public Function GenerateWSGetPrescriptionStatusService(epcsRequest As EpcsRequest) As String
        Dim requestFile As String = ""
        Try

            Dim request As WSGetPrescriptionStatus.EpcsRequest = New WSGetPrescriptionStatus.EpcsRequest()
            If Not Directory.Exists(gloSettings.FolderSettings.AppTempFolderPath + "Outbox") Then
                Directory.CreateDirectory(gloSettings.FolderSettings.AppTempFolderPath + "Outbox")
            End If
            requestFile = GetFileName(gloSettings.FolderSettings.AppTempFolderPath + "Outbox") 'System.AppDomain.CurrentDomain.BaseDirectory + "Outbox\\\\" + System.DateTime.Now.ToString("yyyyMMddhhmmss") + ".xml"
            request.EpcsRequestBody = generateEpcsRequestBody(5, epcsRequest.RequestBody, 0, Nothing)
            request.EpcsRequestHeader = New WSGetPrescriptionStatus.EpcsRequestEpcsRequestHeader()
            request.EpcsRequestHeader = generateEpcsRequestHeader(5, epcsRequest.RequestHeder)

            If request IsNot Nothing Then
                Dim serializer As New XmlSerializer(GetType(WSGetPrescriptionStatus.EpcsRequest))
                Using file As New System.IO.StreamWriter(requestFile)
                    serializer.Serialize(file, request)
                    file.Close()
                End Using
                serializer = Nothing
            End If
            Dim doc As Xml.XmlDocument = New Xml.XmlDocument()
            doc.Load(requestFile)
            doc.InnerXml = RemoveNamespace(doc.InnerXml)
            doc.Save(requestFile)

        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog("Error Occurred processing EPCS GenerateWSEPCSDrugCheckService : " & ex.Message)
        End Try
        Return requestFile
    End Function


#Region "Ui laun"
    Public Function GenerateUILaunchSigning(epcsRequest As EpcsRequest, ByVal RefillItem As Int16, ByRef erxFiles As List(Of Dictionary(Of String, String))()) As String
        Dim requestFile As String = ""
        Try


            Dim request As UILaunchSigning.EpcsRequest = New UILaunchSigning.EpcsRequest()
            request.EpcsRequestBody = New UILaunchSigning.EpcsRequestEpcsRequestBody()
            If Not Directory.Exists(gloSettings.FolderSettings.AppTempFolderPath + "Outbox") Then
                Directory.CreateDirectory(gloSettings.FolderSettings.AppTempFolderPath + "Outbox")
            End If
            requestFile = GetFileName(gloSettings.FolderSettings.AppTempFolderPath + "Outbox")
            request.EpcsRequestBody = generateEpcsRequestBody(7, epcsRequest.RequestBody, RefillItem, erxFiles)
            request.EpcsRequestHeader = New UILaunchSigning.EpcsRequestEpcsRequestHeader()
            request.EpcsRequestHeader = generateEpcsRequestHeader(7, epcsRequest.RequestHeder)

            If request IsNot Nothing Then
                Dim serializer As New XmlSerializer(GetType(UILaunchSigning.EpcsRequest))
                Using file As New System.IO.StreamWriter(requestFile)
                    serializer.Serialize(file, request)
                    file.Close()
                End Using
                serializer = Nothing
            End If

            Dim doc As Xml.XmlDocument = New Xml.XmlDocument()
            doc.Load(requestFile)
            doc.InnerXml = RemoveNamespace(doc.InnerXml)
            doc.Save(requestFile)

        Catch ex As Exception
            gloSurescriptGeneral.UpdateLog("Error Occurred processing EPCS GenerateUILaunchSigning: " & ex.Message)
        End Try
        Return requestFile
    End Function
#End Region

    Function GenerateWSUpdatePrescriptionStatus(epcsRequest As EpcsRequest) As String
        Dim request As WSUpdatePrescriptionStatus.EpcsRequest = New WSUpdatePrescriptionStatus.EpcsRequest()
        Dim requestFile As String = String.Empty
        Dim serializer As XmlSerializer = Nothing
        Dim doc As Xml.XmlDocument = Nothing
        Try

            If Not Directory.Exists(gloSettings.FolderSettings.AppTempFolderPath + "Outbox") Then
                Directory.CreateDirectory(gloSettings.FolderSettings.AppTempFolderPath + "Outbox")
            End If
            requestFile = GetFileName(gloSettings.FolderSettings.AppTempFolderPath + "Outbox") 'System.AppDomain.CurrentDomain.BaseDirectory + "Outbox\\\\" + System.DateTime.Now.ToString("yyyyMMddhhmmss") + ".xml"
            request.EpcsRequestHeader = generateEpcsRequestHeader(EpcsSeviceCall.WSUpdatePrescriptionStatus, epcsRequest.RequestHeder)
            request.EpcsRequestBody = generateEpcsRequestBody(EpcsSeviceCall.WSUpdatePrescriptionStatus, epcsRequest.RequestBody, Nothing, Nothing)
            If request IsNot Nothing Then
                serializer = New XmlSerializer(GetType(WSUpdatePrescriptionStatus.EpcsRequest))
                Using file As New System.IO.StreamWriter(requestFile)
                    serializer.Serialize(file, request)
                    file.Close()
                End Using

            End If
            doc = New Xml.XmlDocument()
            doc.Load(requestFile)
            doc.InnerXml = RemoveNamespace(doc.InnerXml)
            doc.Save(requestFile)
            Return requestFile
        Catch ex As Exception
            Return ""
        Finally
            If Not IsNothing(request) Then
                request = Nothing
            End If
            If Not IsNothing(serializer) Then
                serializer = Nothing
            End If
            If Not IsNothing(doc) Then
                doc = Nothing
            End If
        End Try
    End Function

End Class
