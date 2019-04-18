Imports System.IO
Imports System.Xml.Serialization
Imports System.Text.RegularExpressions
Imports gloSureScript
Imports System.Configuration
Imports System.Data.SqlClient


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
Public Class clsUiLaunchLogicalAccessRequest

    Public Property SourceTransactionReferenceNumber() As String
        Get
            Return m_SourceTransactionReferenceNumber
        End Get
        Set(value As String)
            m_SourceTransactionReferenceNumber = value
        End Set
    End Property
    Private m_SourceTransactionReferenceNumber As String

    Public Property ApprovedByStaffName() As String
        Get
            Return m_ApprovedByStaffName
        End Get
        Set(value As String)
            m_ApprovedByStaffName = value
        End Set
    End Property
    Private m_ApprovedByStaffName As String

    Public Property ApprovedByStaffId() As String
        Get
            Return m_ApprovedByStaffId
        End Get
        Set(value As String)
            m_ApprovedByStaffId = value
        End Set
    End Property
    Private m_ApprovedByStaffId As String

    Public Property ValidatingPrescriberNpi() As String
        Get
            Return m_ValidatingPrescriberNpi
        End Get
        Set(value As String)
            m_ValidatingPrescriberNpi = value
        End Set
    End Property
    Private m_ValidatingPrescriberNpi As String

    Public Property PostBackUrl() As String
        Get
            Return m_PostBackUrl
        End Get
        Set(value As String)
            m_PostBackUrl = value
        End Set
    End Property
    Private m_PostBackUrl As String

    Public Property CloseWindowOnExit() As String
        Get
            Return m_CloseWindowOnExit
        End Get
        Set(value As String)
            m_CloseWindowOnExit = value
        End Set
    End Property
    Private m_CloseWindowOnExit As String

    Public Property Npi() As Int64
        Get
            Return m_Npi
        End Get
        Set(value As Int64)
            m_Npi = value
        End Set
    End Property
    Private m_Npi As Int64
End Class
Public Class clsWsGetPrescriberStatus
    Public Property Npi() As String
        Get
            Return m_Int64
        End Get
        Set(value As String)
            m_Int64 = value
        End Set
    End Property
    Private m_Int64 As String
End Class
Public Enum EpcsSeviceCall
    WSSetUpOrganization = 0
    WSInvitePrescriber = 1
    WSGetPrescriberStatus = 2
    WSGetPrescriberToken = 3
    WSDisablePrescriberToken = 4
    WSGetPrescriptionStatus = 5
    UILaunchLogicalAccess = 6
End Enum
Public Class clsEPCSHelper
    Implements IDisposable
    Public Shared sVendorName As String
    Public Shared sVendorLabel As String
    Public Shared sVendorNodeName As String
    Public Shared sVendorNodeLabel As String
    Public Shared sEpcsUrl As String
    Public Shared sSharedSecret As String

    Public Sub New()
    End Sub
    Public Function GetFileName(ByVal strAppPath As [String]) As [String]
        Try
            Dim _NewDocumentName As String = ""
            Dim _Extension As String = ".xml"
            Dim _dtCurrentDateTime As DateTime = System.DateTime.Now
            Dim i As Integer = 0
            _NewDocumentName = _dtCurrentDateTime.ToString("MMddyyyyhhmmssffftt") & _Extension
            While File.Exists(Convert.ToString(strAppPath) & "\" & _NewDocumentName) = True
                i = i + 1
                _NewDocumentName = _dtCurrentDateTime.ToString("MMddyyyyhhmmssffftt") & "-" & i & _Extension
            End While
            Return Convert.ToString(strAppPath) & "\" & _NewDocumentName
        Catch ex As Exception
            Return ""
        Finally
        End Try
    End Function
    Public Function SetUpEPCSOrganizationEnable() As Boolean
        Dim flag As Boolean
        Dim strFileName As String = ""
        Dim epcsRequest As EpcsRequest
        Try
            gloSurescriptGeneral.ServerName = gstrSQLServerName
            gloSurescriptGeneral.DatabaseName = gstrDatabaseName
            gloSurescriptGeneral.RootPath = System.Windows.Forms.Application.StartupPath
            gloSurescriptGeneral.blnStagingServer = gblnIsStagingServer
            gloSurescriptGeneral.blnSQLAuthentication = gblnSQLAuthentication
            gloSurescriptGeneral.UserName = gstrSQLUserEMR
            gloSurescriptGeneral.Password = gstrSQLPasswordEMR
            gloSurescriptGeneral.blnNCPDP10dot6 = gbln10dot6Version
            gloSurescriptGeneral.checkDownloadVersion()
            gloSurescriptGeneral.blnSecureMessage4dot5 = gblnSecureMessage4dot5
            gloSurescriptGeneral.sSSPRODUCTIONACCOUNTID = gstrSSPRODUCTIONACCOUNTID ''261 common for both 10.6 and 8.1 portals
            gloSurescriptGeneral.sSSPRODUCTIONPORTALID = gstrSSPRODUCTIONPORTALID ''264 for 8.1 portal
            gloSurescriptGeneral.sSSPRODUCTION10dot6PORTALID = gstrSSPRODUCTION10dot6PORTALID ''1018 for 10.6 portal
            gloSurescriptGeneral.sSTAGING10DOT6ACCOUNTID = gstrSTAGING10DOT6ACCOUNTID ''338 common for both 10.6 and 8.1 portals
            gloSurescriptGeneral.sSTAGING10DOT6PORTALID = gstrSTAGING10DOT6PORTALID ''2273 for 10.6 portal
            gloSurescriptGeneral.sSTAGING8DOT1PORTALID = gstrSTAGING8DOT1PORTALID ''422 for 8.1 portal
            gloSurescriptGeneral.MessageBoxCaption = gstrMessageBoxCaption
            gloSurescriptGeneral.sAusID = frmSettings_New.GetClinicInformation("sExternalcode")

            epcsRequest = getEpcsRequest()
            If Not IsNothing(epcsRequest.RequestHeder) Then
                strFileName = GenerateWSSetUpOrganization(epcsRequest)
            End If
            If strFileName <> "" Then
                If System.IO.File.Exists(strFileName) Then
                     flag = gloSureScript.gloSurescriptGeneral.GeneratePostOrganizationRequest(strFileName, "WSSetUpOrganizationStatus")
                End If
            End If
            If flag Then
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Add, "Orgnization setup successfull", gloAuditTrail.ActivityOutCome.Success)
            Else
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Add, "Orgnization setup unsuccessfull", gloAuditTrail.ActivityOutCome.Failure)
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Add, "SetUpOrganizationStatus Call." & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            flag = False
        Finally

        End Try
        Return flag
    End Function
    Private Function getEpcsRequest() As EpcsRequest
        Dim request As EpcsRequest = New EpcsRequest
        request.RequestHeder = getRequestHeder()
        Return request
    End Function
    Public Function getRequestHeder() As clsEpcsRequestHeder
        Dim requestHeder As clsEpcsRequestHeder = New clsEpcsRequestHeder
        Dim dtclinic As DataTable = Nothing
        Try
            dtclinic = GetAllClinicInformation(gnClinicID)

            If Not IsNothing(dtclinic) AndAlso dtclinic.Rows.Count > 0 Then

                If Trim(dtclinic.Rows(0)("sClinicName").ToString()) = "" Then
                    MessageBox.Show("Please enter the Clinic Name from Clinic settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return Nothing
                    Exit Function
                End If
                If Trim(dtclinic.Rows(0)("sClinicLabel").ToString()) = "" Then
                    MessageBox.Show("Please enter the Clinic Label from Clinic settings", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return Nothing
                    Exit Function
                End If
                If Trim(dtclinic.Rows(0)("sExternalcode").ToString()) = "" Then
                    MessageBox.Show("Please enter the Clinic AUS user name ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return Nothing
                    Exit Function
                End If
                Dim stagingProductionUrl As String = ""
                Dim ArrayUrl As String() = Nothing
                If gblnIsStagingServer Then
                    requestHeder.VendorName = sVendorName 'ConfigurationManager.AppSettings("VendorNameStaging")
                    requestHeder.VendorLabel = sVendorLabel  'ConfigurationManager.AppSettings("VendorLabelStaging")
                    requestHeder.VendorNodeName = sVendorNodeName 'ConfigurationManager.AppSettings("VendorNodeNameStaging")
                    requestHeder.VendorNodeLabel = sVendorNodeLabel 'ConfigurationManager.AppSettings("VendorNodeLabelStaging")

                    stagingProductionUrl = gloSureScript.gloSurescriptGeneral.eRx10dot6StagingWebserviceURL
                    If Not IsNothing(stagingProductionUrl) Then
                        ArrayUrl = stagingProductionUrl.Split("/")
                        If ArrayUrl.Length > 4 Then
                            requestHeder.LogoUrl = ArrayUrl(0) & "//" & ArrayUrl(2) & "/" & ArrayUrl(3) & "/images/logo.png"
                        End If
                    End If
                Else
                    requestHeder.VendorName = sVendorName 'ConfigurationManager.AppSettings("VendorNameProduction")
                    requestHeder.VendorLabel = sVendorLabel 'ConfigurationManager.AppSettings("VendorLabelProduction")
                    requestHeder.VendorNodeName = sVendorNodeName 'ConfigurationManager.AppSettings("VendorNodeNameProduction")
                    requestHeder.VendorNodeLabel = sVendorNodeLabel 'ConfigurationManager.AppSettings("VendorNodeLabelProduction")

                    stagingProductionUrl = gloSureScript.gloSurescriptGeneral.eRx10dot6ProductionWebserviceURL
                    If Not IsNothing(stagingProductionUrl) Then
                        ArrayUrl = stagingProductionUrl.Split("/")
                        If ArrayUrl.Length > 4 Then
                            requestHeder.LogoUrl = ArrayUrl(0) & "//" & ArrayUrl(2) & "/" & ArrayUrl(3) & "/images/logo.png"
                        End If
                    End If
                End If
                requestHeder.ApplicationVersion = System.Windows.Forms.Application.ProductVersion
                requestHeder.AppName = System.Windows.Forms.Application.ProductName
                requestHeder.SourceOrganizationId = dtclinic.Rows(0)("sExternalcode").ToString()

                Dim dtdate As DateTime = System.DateTime.UtcNow
                Dim strdate As String = dtdate.ToString("yyyy-MM-dd")
                Dim strtime As String = dtdate.ToString("hh:mm:ss")
                Dim strUTCFormat As String = (Convert.ToString(strdate & Convert.ToString("T")) & strtime) & "Z"
                requestHeder.HeaderDate = strUTCFormat
                requestHeder.OrganizationName = dtclinic.Rows(0)("sClinicName").ToString()
                requestHeder.OrganizationLabel = dtclinic.Rows(0)("sClinicLabel").ToString()
            End If
            Return requestHeder
        Catch
            Return Nothing
        Finally
            If Not IsNothing(dtclinic) Then
                dtclinic.Dispose()
                dtclinic = Nothing
            End If
            If Not IsNothing(requestHeder) Then
                requestHeder = Nothing
            End If
        End Try
    End Function
    Public Shared Sub GetVendorAndUrlInformationForEpcs(clinicId As Long, isStaging As Boolean)
        Dim dtTable As New DataTable
        Dim StagingVendorName As String = ""
        Dim StagingVendorLabel As String = ""
        Dim StagingVendorNodeName As String = ""
        Dim StagingVendorNodeLabel As String = ""
        Dim StagingEpcsUrl As String = ""
        Dim StagingsharedSecret As String = ""

        Dim ProductionVendorName As String = ""
        Dim ProductionVendorLabel As String = ""
        Dim ProductionVendorNodeName As String = ""
        Dim ProductionVendorNodeLabel As String = ""
        Dim ProductionEpcsUrl As String = ""
        Dim ProductionSharedSecret As String = ""
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = gstrConnectionString
            Dim objCmd As New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetVendorInformationForEpcs"
            objCmd.Connection = objCon

            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            objDA.Fill(dtTable)
            objCon.Close()

            objCon.Dispose() : objCon = Nothing

            objCmd.Parameters.Clear()
            objCmd.Dispose() : objCmd = Nothing

            objDA.Dispose() : objDA = Nothing

            Dim nCount As Integer
            For nCount = 0 To dtTable.Rows.Count - 1
                Select Case dtTable.Rows(nCount).Item(1).ToString.ToUpper
                    Case "StagingVendorName".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            StagingVendorName = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            StagingVendorName = ""
                        End If
                    Case "StagingVendorLabel".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            StagingVendorLabel = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            StagingVendorLabel = ""
                        End If

                    Case "StagingVendorNodeName".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            StagingVendorNodeName = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            StagingVendorNodeName = ""
                        End If
                    Case "StagingVendorNodeLabel".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            StagingVendorNodeLabel = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            StagingVendorNodeLabel = ""
                        End If

                    Case "StagingEpcsUrl".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            StagingEpcsUrl = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            StagingEpcsUrl = ""
                        End If

                    Case "StagingSharedSecret".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            StagingsharedSecret = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            StagingsharedSecret = ""
                        End If

                    Case "ProductionVendorName".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            ProductionVendorName = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            ProductionVendorName = ""
                        End If

                    Case "ProductionVendorLabel".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            ProductionVendorLabel = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            ProductionVendorLabel = ""
                        End If

                    Case "ProductionVendorNodeName".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            ProductionVendorNodeName = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            ProductionVendorNodeName = ""
                        End If

                    Case "ProductionVendorNodeLabel".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            ProductionVendorNodeLabel = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            ProductionVendorNodeLabel = ""
                        End If

                    Case "ProductionEpcsUrl".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            ProductionEpcsUrl = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            ProductionEpcsUrl = ""
                        End If

                    Case "ProductionSharedSecret".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            ProductionSharedSecret = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            ProductionSharedSecret = ""
                        End If

                End Select
            Next
        Catch ex As Exception
            MessageBox.Show("Unable to get vendor settings. Please do the settings through gloEMR-Admin ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            If IsNothing(dtTable) = False Then
                dtTable.Dispose() : dtTable = Nothing
            End If
        End Try
        If isStaging Then
            sVendorName = StagingVendorName
            sVendorLabel = StagingVendorLabel
            sVendorNodeName = StagingVendorNodeName
            sVendorNodeLabel = StagingVendorNodeLabel
            sEpcsUrl = StagingEpcsUrl
            sSharedSecret = StagingsharedSecret
        Else
            sVendorName = ProductionVendorName
            sVendorLabel = ProductionVendorLabel
            sVendorNodeName = ProductionVendorNodeName
            sVendorNodeLabel = ProductionVendorNodeLabel
            sEpcsUrl = ProductionEpcsUrl
            sSharedSecret = ProductionSharedSecret
        End If
    End Sub
    Public Shared Function GetAllClinicInformation(clinicId As Long) As DataTable

        Dim conn As New SqlConnection(gstrConnectionString)
        Dim sqladpt As New SqlDataAdapter
        Dim dtclinic As New DataTable

        conn.Open()
        Dim sql As SqlCommand = Nothing
        Dim _strsql As String
        Try
            sql = New SqlCommand
            sql.CommandType = CommandType.Text
            _strsql = "select * from Clinic_MST where nclinicid = " & clinicId & ""
            sql.CommandText = _strsql
            sql.Connection = conn
            sqladpt.SelectCommand = sql
            sqladpt.Fill(dtclinic)
            Return dtclinic
        Catch
            Return Nothing
        Finally
            If Not IsNothing(sqladpt) Then
                sqladpt.Dispose()
                sqladpt = Nothing
            End If
            If Not IsNothing(conn) Then
                conn.Dispose()
                conn = Nothing
            End If
            If Not IsNothing(sql) Then
                sql.Dispose()
                sql = Nothing
            End If
            If Not IsNothing(dtclinic) Then
                dtclinic.Dispose()
                dtclinic = Nothing
            End If
        End Try
    End Function

    Public Function generateEpcsRequestHeader(valCase As EpcsSeviceCall, requestBody As Object) As Object
        Dim request As clsEpcsRequestHeder = requestBody
        Select Case valCase
            Case EpcsSeviceCall.WSSetUpOrganization
                Dim requestHeader As WSSetUpOrganization.EpcsRequestHeaderType = New WSSetUpOrganization.EpcsRequestHeaderType()
                requestHeader.VendorName = request.VendorName
                requestHeader.VendorLabel = request.VendorLabel
                requestHeader.VendorNodeLabel = request.VendorNodeLabel
                requestHeader.VendorNodeName = request.VendorNodeName
                requestHeader.SourceOrganizationId = request.SourceOrganizationId
                requestHeader.Date = request.HeaderDate
                requestHeader.AppVersion = New WSSetUpOrganization.AppVersionType()
                requestHeader.AppVersion.AppName = request.AppName
                requestHeader.AppVersion.ApplicationVersion = request.ApplicationVersion
                Return requestHeader
            Case EpcsSeviceCall.WSInvitePrescriber
                Dim requestHeader As WSInvitePrescriber.EpcsRequestHeaderType = New WSInvitePrescriber.EpcsRequestHeaderType()
                requestHeader.VendorName = request.VendorName
                requestHeader.VendorLabel = request.VendorLabel
                requestHeader.VendorNodeLabel = request.VendorNodeLabel
                requestHeader.VendorNodeName = request.VendorNodeName
                requestHeader.SourceOrganizationId = request.SourceOrganizationId
                requestHeader.AppVersion = New WSInvitePrescriber.AppVersionType()
                requestHeader.AppVersion.AppName = request.AppName
                requestHeader.AppVersion.ApplicationVersion = request.ApplicationVersion
                requestHeader.Date = request.HeaderDate
                Return requestHeader
            Case EpcsSeviceCall.WSGetPrescriberStatus
                Dim requestHeader As WSGetPrescriberStatus.EpcsRequestHeaderType = New WSGetPrescriberStatus.EpcsRequestHeaderType()
                requestHeader.VendorName = request.VendorName
                requestHeader.VendorLabel = request.VendorLabel
                requestHeader.VendorNodeLabel = request.VendorNodeLabel
                requestHeader.VendorNodeName = request.VendorNodeName
                requestHeader.SourceOrganizationId = request.SourceOrganizationId
                requestHeader.AppVersion = New WSGetPrescriberStatus.AppVersionType()
                requestHeader.AppVersion.AppName = request.AppName
                requestHeader.AppVersion.ApplicationVersion = request.ApplicationVersion
                requestHeader.Date = request.HeaderDate
                Return requestHeader
            Case EpcsSeviceCall.UILaunchLogicalAccess
                Dim requestHeader As UiLaunchLogicalAccess.EpcsRequestEpcsRequestHeader = New UiLaunchLogicalAccess.EpcsRequestEpcsRequestHeader()
                requestHeader.VendorName = request.VendorName
                requestHeader.VendorLabel = request.VendorLabel
                requestHeader.VendorNodeLabel = request.VendorNodeLabel
                requestHeader.VendorNodeName = request.VendorNodeName
                requestHeader.SourceOrganizationId = request.SourceOrganizationId
                requestHeader.Date = request.HeaderDate
                requestHeader.AppVersion = New UiLaunchLogicalAccess.EpcsRequestEpcsRequestHeaderAppVersion()
                requestHeader.AppVersion.AppName = request.AppName
                requestHeader.AppVersion.ApplicationVersion = request.ApplicationVersion
                requestHeader.PrivateLabel = New UiLaunchLogicalAccess.EpcsRequestEpcsRequestHeaderPrivateLabel()
                requestHeader.PrivateLabel.LogoUrl = request.LogoUrl
                Return requestHeader
            Case Else
                Return Nothing
        End Select

    End Function
    Public Function generateEpcsRequestBody(valCase As EpcsSeviceCall, requestObj As Object) As Object
        Dim requestMain As Object = requestObj
        Select Case valCase
            Case EpcsSeviceCall.WSSetUpOrganization
                Dim request As clsEpcsRequestHeder = requestMain
                Dim requestBody As WSSetUpOrganization.EpcsRequestBodyType = New WSSetUpOrganization.EpcsRequestBodyType()
                requestBody.WsSetUpOrganizationStatusRequest = New WSSetUpOrganization.WsSetUpOrganizationStatusRequestType()
                requestBody.WsSetUpOrganizationStatusRequest.VendorLabel = request.VendorLabel
                requestBody.WsSetUpOrganizationStatusRequest.VendorName = request.VendorName
                requestBody.WsSetUpOrganizationStatusRequest.VendorNodeLabel = request.VendorNodeLabel
                requestBody.WsSetUpOrganizationStatusRequest.VendorNodeName = request.VendorNodeName
                requestBody.WsSetUpOrganizationStatusRequest.OrganizationLabel = request.OrganizationLabel
                requestBody.WsSetUpOrganizationStatusRequest.OrganizationName = request.OrganizationName
                requestBody.WsSetUpOrganizationStatusRequest.OrganizationId = request.SourceOrganizationId
                Return requestBody
            Case EpcsSeviceCall.WSInvitePrescriber
                Dim request As gloSureScript.Prescriber = requestMain
                Dim requestBody As WSInvitePrescriber.EpcsRequestBodyType = New WSInvitePrescriber.EpcsRequestBodyType()
                requestBody.WsInvitePrescriberRequest = New WSInvitePrescriber.WsInvitePrescriberRequestType()
                requestBody.WsInvitePrescriberRequest.PrescriberInviteRequestList = New WSInvitePrescriber.PrescriberInviteRequestListType()
                requestBody.WsInvitePrescriberRequest.PrescriberInviteRequestList.PrescriberInviteRequest = New WSInvitePrescriber.PrescriberInviteRequestType()
                requestBody.WsInvitePrescriberRequest.PrescriberInviteRequestList.PrescriberInviteRequest.Npi = request.NPI
                requestBody.WsInvitePrescriberRequest.PrescriberInviteRequestList.PrescriberInviteRequest.Firstname = request.PrescriberName.FirstName
                requestBody.WsInvitePrescriberRequest.PrescriberInviteRequestList.PrescriberInviteRequest.Lastname = request.PrescriberName.LastName
                requestBody.WsInvitePrescriberRequest.PrescriberInviteRequestList.PrescriberInviteRequest.Gender = request.Gender
                requestBody.WsInvitePrescriberRequest.PrescriberInviteRequestList.PrescriberInviteRequest.Dateofbirth = request.DateTimeStamp 'DateTimeStamp field use as DOB
                requestBody.WsInvitePrescriberRequest.PrescriberInviteRequestList.PrescriberInviteRequest.PrimaryAddressline1 = request.PrescriberAddress.Address1
                requestBody.WsInvitePrescriberRequest.PrescriberInviteRequestList.PrescriberInviteRequest.PrimaryCity = request.PrescriberAddress.City
                requestBody.WsInvitePrescriberRequest.PrescriberInviteRequestList.PrescriberInviteRequest.PrimaryState = request.PrescriberAddress.State
                requestBody.WsInvitePrescriberRequest.PrescriberInviteRequestList.PrescriberInviteRequest.PrimaryZipcode = request.PrescriberAddress.Zip
                requestBody.WsInvitePrescriberRequest.PrescriberInviteRequestList.PrescriberInviteRequest.SocialSecurityNumber = "0" 'request.ProviderSSN
                requestBody.WsInvitePrescriberRequest.PrescriberInviteRequestList.PrescriberInviteRequest.Email = request.PrescriberPhone.Email
                'requestBody.WsInvitePrescriberRequest.PrescriberInviteRequestList.PrescriberInviteRequest.TokenMailingAddressline1 = request.PrescriberAddress.Address1
                'requestBody.WsInvitePrescriberRequest.PrescriberInviteRequestList.PrescriberInviteRequest.TokenMailingAddressline2 = request.PrescriberAddress.Address2
                'requestBody.WsInvitePrescriberRequest.PrescriberInviteRequestList.PrescriberInviteRequest.TokenMailingCity = request.PrescriberAddress.City
                'requestBody.WsInvitePrescriberRequest.PrescriberInviteRequestList.PrescriberInviteRequest.TokenMailingState = request.PrescriberAddress.State
                'requestBody.WsInvitePrescriberRequest.PrescriberInviteRequestList.PrescriberInviteRequest.TokenMailingZipcode = request.PrescriberAddress.Zip
                Return (requestBody)
            Case EpcsSeviceCall.WSGetPrescriberStatus
                Dim request As clsWsGetPrescriberStatus = requestMain
                Dim requestBody As WSGetPrescriberStatus.EpcsRequestBodyType = New WSGetPrescriberStatus.EpcsRequestBodyType()
                requestBody.WsGetPrescriberStatusRequest = New WSGetPrescriberStatus.WsGetPrescriberStatusRequestType()
                requestBody.WsGetPrescriberStatusRequest.NpiList = New WSGetPrescriberStatus.NpiListType()
                requestBody.WsGetPrescriberStatusRequest.NpiList.Npi = request.Npi
                Return requestBody
            Case EpcsSeviceCall.UILaunchLogicalAccess
                Dim request As clsUiLaunchLogicalAccessRequest = requestMain
                Dim requestBody As UiLaunchLogicalAccess.EpcsRequestEpcsRequestBody = New UiLaunchLogicalAccess.EpcsRequestEpcsRequestBody()
                requestBody.UiLaunchLogicalAccessRequest = New UiLaunchLogicalAccess.EpcsRequestEpcsRequestBodyUiLaunchLogicalAccessRequest()
                requestBody.UiLaunchLogicalAccessRequest.SourceTransactionReferenceNumber = request.SourceTransactionReferenceNumber
                requestBody.UiLaunchLogicalAccessRequest.ApprovedByStaffName = request.ApprovedByStaffName
                'requestBody.UiLaunchLogicalAccessRequest.ApprovedByStaffId = request.ApprovedByStaffId
                requestBody.UiLaunchLogicalAccessRequest.ValidatingPrescriberNpi = request.ValidatingPrescriberNpi
                'requestBody.UiLaunchLogicalAccessRequest.PostBackUrl = request.PostBackUrl
                'requestBody.UiLaunchLogicalAccessRequest.CloseWindowOnExit = request.CloseWindowOnExit
                'requestBody.UiLaunchLogicalAccessRequest.NpiList = New UiLaunchLogicalAccess.NpiListType()
                'requestBody.UiLaunchLogicalAccessRequest.NpiList.Npi = request.Npi
                Return requestBody
            Case Else
                Return Nothing
        End Select
    End Function

    Public Function RemoveNamespace(xml As String) As String
        Dim xmlnsPattern As String = "\s+xmlns:xsd\s*(:\w)?\s*=\s*\""(?<url>[^\""]*)\"""
        Dim matchCol As MatchCollection = Regex.Matches(xml, xmlnsPattern)
        For Each m As Match In matchCol
            xml = xml.Replace(m.ToString(), "")
        Next
        Return xml
    End Function
    Public Function GenerateWSSetUpOrganization(epcsRequest As EpcsRequest) As String
        Dim request As WSSetUpOrganization.EpcsRequestType = New WSSetUpOrganization.EpcsRequestType()
        Dim requestFile As String = String.Empty
        Dim serializer As XmlSerializer
        Dim doc As Xml.XmlDocument
        Try

            If Not Directory.Exists(gloSettings.FolderSettings.AppTempFolderPath + "Outbox") Then
                Directory.CreateDirectory(gloSettings.FolderSettings.AppTempFolderPath + "Outbox")
            End If
            requestFile = GetFileName(gloSettings.FolderSettings.AppTempFolderPath + "Outbox")
            request.EpcsRequestBody = generateEpcsRequestBody(EpcsSeviceCall.WSSetUpOrganization, epcsRequest.RequestHeder)
            request.EpcsRequestHeader = generateEpcsRequestHeader(EpcsSeviceCall.WSSetUpOrganization, epcsRequest.RequestHeder)
            If request IsNot Nothing Then
                serializer = New XmlSerializer(GetType(WSSetUpOrganization.EpcsRequestType))
                Using file As New System.IO.StreamWriter(requestFile)
                    serializer.Serialize(file, request)
                    file.Close()
                End Using

            End If
            doc = New Xml.XmlDocument()
            doc.Load(requestFile)
            doc.InnerXml = RemoveNamespace(doc.InnerXml)
            doc.Save(requestFile)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Add, "Epcs SetUpOrganization Call." & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return ""
        Finally
            request = Nothing
            serializer = Nothing
            doc = Nothing
        End Try
        Return requestFile
    End Function
    Public Function GenerateWSInvitePrescriber(epcsRequest As EpcsRequest) As String
        Dim request As WSInvitePrescriber.EpcsRequestType = New WSInvitePrescriber.EpcsRequestType()
        Dim requestFile As String = String.Empty
        Dim serializer As XmlSerializer
        Dim doc As Xml.XmlDocument
        Try

            If Not Directory.Exists(gloSettings.FolderSettings.AppTempFolderPath + "Outbox") Then
                Directory.CreateDirectory(gloSettings.FolderSettings.AppTempFolderPath + "Outbox")
            End If
            requestFile = GetFileName(gloSettings.FolderSettings.AppTempFolderPath + "Outbox")
            request.EpcsRequestBody = generateEpcsRequestBody(EpcsSeviceCall.WSInvitePrescriber, epcsRequest.RequestBody)
            request.EpcsRequestHeader = generateEpcsRequestHeader(EpcsSeviceCall.WSInvitePrescriber, epcsRequest.RequestHeder)
            If request IsNot Nothing Then
                serializer = New XmlSerializer(GetType(WSInvitePrescriber.EpcsRequestType))
                Using file As New System.IO.StreamWriter(requestFile)
                    serializer.Serialize(file, request)
                    file.Close()
                End Using
                serializer = Nothing

            End If
            doc = New Xml.XmlDocument()
            doc.Load(requestFile)
            doc.InnerXml = RemoveNamespace(doc.InnerXml)
            doc.Save(requestFile)

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Invite, "Invite Prescriber Call. " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return ""
        Finally
            request = Nothing
            serializer = Nothing
            doc = Nothing
        End Try
        Return requestFile
    End Function

    Function GenerateWSGetPrescriberStatus(epcsRequest As EpcsRequest) As String
        Dim request As WSGetPrescriberStatus.EpcsRequestType = New WSGetPrescriberStatus.EpcsRequestType()
        Dim requestFile As String = String.Empty
        Dim serializer As XmlSerializer
        Dim doc As Xml.XmlDocument
        Try

            If Not Directory.Exists(gloSettings.FolderSettings.AppTempFolderPath + "Outbox") Then
                Directory.CreateDirectory(gloSettings.FolderSettings.AppTempFolderPath + "Outbox")
            End If
            requestFile = GetFileName(gloSettings.FolderSettings.AppTempFolderPath + "Outbox") 'System.AppDomain.CurrentDomain.BaseDirectory + "Outbox\\\\" + System.DateTime.Now.ToString("yyyyMMddhhmmss") + ".xml"
            request.EpcsRequestBody = generateEpcsRequestBody(EpcsSeviceCall.WSGetPrescriberStatus, epcsRequest.RequestBody)
            request.EpcsRequestHeader = generateEpcsRequestHeader(EpcsSeviceCall.WSGetPrescriberStatus, epcsRequest.RequestHeder)
            If request IsNot Nothing Then
                serializer = New XmlSerializer(GetType(WSGetPrescriberStatus.EpcsRequestType))
                Using file As New System.IO.StreamWriter(requestFile)
                    serializer.Serialize(file, request)
                    file.Close()
                End Using

            End If
            doc = New Xml.XmlDocument()
            doc.Load(requestFile)
            doc.InnerXml = RemoveNamespace(doc.InnerXml)
            doc.Save(requestFile)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Status, "Get Prescriber Status Call. " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return ""
        Finally
            request = Nothing
            serializer = Nothing
            doc = Nothing
        End Try
        Return requestFile
    End Function
    Function GenerateUiLaunchLogicalAccess(epcsRequest As EpcsRequest) As String
        Dim request As UiLaunchLogicalAccess.EpcsRequest = New UiLaunchLogicalAccess.EpcsRequest()
        Dim requestFile As String = String.Empty
        Dim serializer As XmlSerializer
        Dim doc As Xml.XmlDocument
        Try
            If Not Directory.Exists(gloSettings.FolderSettings.AppTempFolderPath + "Outbox") Then
                Directory.CreateDirectory(gloSettings.FolderSettings.AppTempFolderPath + "Outbox")
            End If
            requestFile = GetFileName(gloSettings.FolderSettings.AppTempFolderPath + "Outbox")
            request.EpcsRequestHeader = generateEpcsRequestHeader(EpcsSeviceCall.UILaunchLogicalAccess, epcsRequest.RequestHeder)
            request.EpcsRequestBody = generateEpcsRequestBody(EpcsSeviceCall.UILaunchLogicalAccess, epcsRequest.RequestBody)
            If request IsNot Nothing Then
                serializer = New XmlSerializer(GetType(UiLaunchLogicalAccess.EpcsRequest))
                Using file As New System.IO.StreamWriter(requestFile)
                    serializer.Serialize(file, request)
                    file.Close()
                End Using
                serializer = Nothing
            End If
            doc = New Xml.XmlDocument()
            doc.Load(requestFile)
            doc.InnerXml = RemoveNamespace(doc.InnerXml)
            doc.Save(requestFile)
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.EpcsPrescriberRegistration, gloAuditTrail.ActivityType.Activate, "UiLaunch Logical Access Call. " & ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return ""
        Finally
            request = Nothing
            serializer = Nothing
            doc = Nothing
        End Try
        Return requestFile
    End Function
#Region " IDisposable Support "
    Private disposedValue As Boolean = False
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
            End If
        End If
        Me.disposedValue = True
    End Sub
#End Region
End Class
