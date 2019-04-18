Imports System.Data.SqlClient
Imports System.Net
Imports System.Configuration
Imports gloSettings
Imports gloCommunity.Classes
Imports System.Management


Public Class clsSettings

    Implements IDisposable

#Region "Private Variables"

    Dim _tmAppointmentStartTime As DateTime
    Dim _tmAppointmentEndTime As DateTime
    Dim _nAppointmentInterval As Int16
    Dim _nPULLCHARTSInterval As Int16
    Dim _nNoOfAttempts As Int16

    Private m_RxDeclarationText As String
    Private m_RxFooterNote As String
    Dim m_FAXCompression As String = ""
    Dim m_MCIRReportPath As String = ""
    Dim m_RxHubDisclaimer As String = "" 'RxHub Disclaimer
    Dim m_RxHubParticipantId As String = "" 'Rxhub Staging and production credentials.
    Dim m_RxHubPassword As String = ""
    'Rxhub Staging and production credentials this is realted to EDIWebService settings
    Dim m_RXELIGIBILITYEMR As String = "" 'this will tell wether the processing for RxEligibility is to be done using Code or by Webservice
    Dim m_EDISERVICEPATH As String = "" 'this will tell to which url the webservice is pointing and therefore the Rxeligibility request will be sent to this url
    Dim m_OMRCategory_History As String
    Dim m_OMRCategory_ROS As String
    Dim m_OMRCategory_PatientRegistration As String
    Dim m_DMSCategory_PatientDirective As String
    Dim m_DMSCategory_Labs As String
    Dim m_CCDFilePath As String = "" 'CCD file path
    Dim m_StyleSheetPath As String = "" 'Style Sheet Path
    Dim m_DMSCategory_Radiology As String
    Dim m_DMSCategory_VIS As String 'Added by Mayuri:20120118-VIS Immunization category
    Dim m_DMSCategory_Amedment As String
    Dim m_DMSCategory_RxMed As String 'for cchit11 medication reconcilation
    Dim m_ClinicDISettings As String
    Dim m_ClinicFormularySettings As String 'Clinic formulary alert setting
    Private _seFaxUserID As String = "" 'eFax Login Key
    Private _seFaxUserPassword As String = ""
    Private _SignatureText As String
    Private _CoSignatureText As String
    Dim _VitalSettingsValue As String = ""
    Dim _OBVitalSettingsValue As String = ""
    Dim _showDMAlert As String = ""
    Dim _SMDBsettings As String() 'Added by Ujwala - for Snomed Immunization  - as on 20101007

    Dim m_NoOfAttempts As Integer = 0
    Dim _isEnableRecoverExam As Integer = 0 'Code for recover exam Module

    Dim m_HPIEnabled As Boolean = False
    Dim m_getMic As Boolean = False
    Dim m_AdvancedGrowthChart As Boolean = False ''sudhir 20081126
    Dim m_LocationAddress As Boolean = False
    Private _IsAdvErxEnabled As Boolean
    Private _IsRxHubStagingServer As Boolean
    Dim _blnRecordLevelLocking As Boolean 'Mahesh 20070723
    Dim _blnAutoPatientCode As Boolean 'Declared by Anil on 20071119
    Dim _isVitalsHeightCopyForward As Boolean
    Dim _EnableSamrtDxReviewScreen As Boolean
    Private _blnInternetFax As Boolean = False 'sarika internet fax 
    Private _IsSurescriptEnabled As Boolean
    Private _IsStagingServer As Boolean
    Private _EMExamType As AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType 'Shubhangi20100105 Set the global variable for the EMExam type which we set from Admin

    ' Dim emrAdminsettings As New gloSettings.gloEMRAdminSettings()

#End Region

#Region "Public Properties"

    Private _LoadProbOnMedication As Boolean
    Public Property LoadProbOnMedication() As Boolean
        Get
            Return _LoadProbOnMedication
        End Get
        Set(ByVal value As Boolean)
            _LoadProbOnMedication = value
        End Set
    End Property

    'Code for recover exam Module
    Public Property IsEnableRecoverExam() As Boolean
        Get
            Return _isEnableRecoverExam
        End Get
        Set(ByVal Value As Boolean)
            _isEnableRecoverExam = Value
        End Set
    End Property

    Public Property IsImmunization() As Boolean
        Get
            Return gblnIsImmunization
        End Get
        Set(ByVal Value As Boolean)
            gblnIsImmunization = Value
        End Set
    End Property

    Public Property CCDDefaultUserID() As Long
        Get
            Return gnCCDDefaultUserID
        End Get
        Set(ByVal Value As Long)
            gnCCDDefaultUserID = Value
        End Set
    End Property

    'this is realted to EDIWebService settings
    Public Property RXELIGIBILITYEMR() As String
        Get
            Return m_RXELIGIBILITYEMR
        End Get
        Set(ByVal Value As String)
            m_RXELIGIBILITYEMR = Value
        End Set
    End Property

    'this is realted to EDIWebService settings
    Public Property EDISERVICEPATH() As String
        Get
            Return m_EDISERVICEPATH
        End Get
        Set(ByVal Value As String)
            m_EDISERVICEPATH = Value
        End Set
    End Property
    'AutoEligibilityONorOFF
    Public Property AutoEligibility() As Boolean
    'CCD file path
    Public Property CCDFilePath() As String
        Get
            Return m_CCDFilePath
        End Get
        Set(ByVal Value As String)
            m_CCDFilePath = Value
        End Set

    End Property
    Public Property AutoPDMPEligiblity() As Boolean

    Public Property PDMPSendToDMS() As Boolean

    'StyleSheet  path
    Public Property StyleSheetPath() As String
        Get
            Return m_StyleSheetPath
        End Get
        Set(ByVal Value As String)
            m_StyleSheetPath = Value
        End Set
    End Property

    Public Property AppointmentStartTime() As DateTime
        Get
            Return _tmAppointmentStartTime
        End Get
        Set(ByVal Value As DateTime)
            _tmAppointmentStartTime = Value
        End Set
    End Property

    Public Property AppointmentEndTime() As DateTime
        Get
            Return _tmAppointmentEndTime
        End Get
        Set(ByVal Value As DateTime)
            _tmAppointmentEndTime = Value
        End Set
    End Property

    Public Property AppointmentInterval() As Int16
        Get
            Return _nAppointmentInterval
        End Get
        Set(ByVal Value As Int16)
            _nAppointmentInterval = Value
        End Set
    End Property

    Public Property PULLCHARTSInterval() As Int16
        Get
            Return _nPULLCHARTSInterval
        End Get
        Set(ByVal Value As Int16)
            _nPULLCHARTSInterval = Value
        End Set
    End Property

    Public Property FAXNoOfAttempts() As Int16
        Get
            Return _nNoOfAttempts
        End Get
        Set(ByVal Value As Int16)
            _nNoOfAttempts = Value
        End Set
    End Property

    Public Property FAXCompression() As String
        Get
            If Trim(m_FAXCompression) <> "" Then
                Return m_FAXCompression
            Else
                Return "CCITT G3"
            End If

        End Get
        Set(ByVal Value As String)
            m_FAXCompression = Value
        End Set
    End Property

    Public Property HPIEnabled() As Boolean
        Get
            Return m_HPIEnabled
        End Get
        Set(ByVal Value As Boolean)
            m_HPIEnabled = Value
        End Set
    End Property

    Public Property getMic() As Boolean
        Get
            Return m_getMic
        End Get
        Set(ByVal Value As Boolean)
            m_getMic = Value
        End Set
    End Property

    Public Property MCIRReportPath() As String
        Get
            Return m_MCIRReportPath
        End Get
        Set(ByVal Value As String)
            m_MCIRReportPath = Value
        End Set
    End Property

    'RxHub _Disclaimer
    Public Property RxHubDisclaimer() As String
        Get
            Return m_RxHubDisclaimer
        End Get
        Set(ByVal Value As String)
            m_RxHubDisclaimer = Value
        End Set
    End Property

    'RxHub Staging/production server credentials
    Public Property RxHubParticipantId() As String
        Get
            Return m_RxHubParticipantId
        End Get
        Set(ByVal Value As String)
            m_RxHubParticipantId = Value
        End Set
    End Property

    Public Property RxHubPassword() As String
        Get
            Return m_RxHubPassword
        End Get
        Set(ByVal Value As String)
            m_RxHubPassword = Value
        End Set
    End Property
    'RxHub Staging/production server credentials

    Public Property ShowAdvancedGrowthChart() As Boolean
        Get
            Return m_AdvancedGrowthChart
        End Get
        Set(ByVal Value As Boolean)
            m_AdvancedGrowthChart = Value
        End Set
    End Property

    Public Property LocationAddress() As Boolean
        Get
            Return m_LocationAddress
        End Get
        Set(ByVal Value As Boolean)
            m_LocationAddress = Value
        End Set
    End Property

    Public Property OMRCategory_History() As String
        Get
            Return m_OMRCategory_History
        End Get
        Set(ByVal Value As String)
            m_OMRCategory_History = Value
        End Set
    End Property

    Public Property OMRCategory_ROS() As String
        Get
            Return m_OMRCategory_ROS
        End Get
        Set(ByVal Value As String)
            m_OMRCategory_ROS = Value
        End Set
    End Property

    Public Property OMRCategory_PatientRegistration() As String
        Get
            Return m_OMRCategory_PatientRegistration
        End Get
        Set(ByVal Value As String)
            m_OMRCategory_PatientRegistration = Value
        End Set
    End Property

    Public Property DMSCategory_PatientDirective() As String
        Get
            Return m_DMSCategory_PatientDirective
        End Get
        Set(ByVal Value As String)
            m_DMSCategory_PatientDirective = Value
        End Set
    End Property

    Public Property DMSCategory_Labs() As String
        Get
            Return m_DMSCategory_Labs
        End Get
        Set(ByVal value As String)
            m_DMSCategory_Labs = value
        End Set
    End Property

    Public Property DMSCategory_Radiology() As String
        Get
            Return m_DMSCategory_Radiology
        End Get
        Set(ByVal value As String)
            m_DMSCategory_Radiology = value
        End Set
    End Property

    ''for CCHIT11 medication reconcilation 
    Public Property DMSCategory_RxMed() As String
        Get
            Return m_DMSCategory_RxMed
        End Get
        Set(ByVal value As String)
            m_DMSCategory_RxMed = value
        End Set
    End Property

    ''Mayuri:20120118-For VIS -Immunization Category
    Public Property DMSCategory_VIS() As String
        Get
            Return m_DMSCategory_VIS
        End Get
        Set(ByVal value As String)
            m_DMSCategory_VIS = value
        End Set
    End Property
    Public Property DMSCategory_Amedment() As String
        Get
            Return m_DMSCategory_Amedment
        End Get
        Set(ByVal value As String)
            m_DMSCategory_Amedment = value
        End Set
    End Property

    Public Property NoOfAttempts() As Integer
        Get
            Return m_NoOfAttempts
        End Get
        Set(ByVal Value As Integer)
            m_NoOfAttempts = Value
        End Set
    End Property

    'Enable/Disable Drug Interaction for spcific machine
    Public Property ClinicDISettings() As String
        Get
            Return m_ClinicDISettings
        End Get
        Set(ByVal Value As String)
            m_ClinicDISettings = Value
        End Set
    End Property

    'Enable/Disable Formulary Alert for spcific machine
    Public Property ClinicFormularySettings() As String
        Get
            Return m_ClinicFormularySettings
        End Get
        Set(ByVal Value As String)
            m_ClinicFormularySettings = Value
        End Set
    End Property

    'Mahesh 20070723 - Record Level Locking 
    Public Property RecordLevelLocking() As Boolean
        Get
            Return _blnRecordLevelLocking
        End Get
        Set(ByVal value As Boolean)
            _blnRecordLevelLocking = value
        End Set
    End Property

    ''Property Added by Anil 0n 20071119
    Public Property AutoGeneratePatientCode() As Boolean
        Get
            Return _blnAutoPatientCode
        End Get
        Set(ByVal value As Boolean)
            _blnAutoPatientCode = value
        End Set
    End Property

    Public Property EnableSamrtDxReviewScreen() As Boolean
        Get
            Return _EnableSamrtDxReviewScreen
        End Get
        Set(ByVal value As Boolean)
            _EnableSamrtDxReviewScreen = value
        End Set
    End Property

    Public Property VitalsHeightCopyForward() As Boolean
        Get
            Return _isVitalsHeightCopyForward
        End Get
        Set(ByVal value As Boolean)
            _isVitalsHeightCopyForward = value
        End Set
    End Property

    'sarika internet fax
    Public Property InternetFax() As Boolean
        Get
            Return _blnInternetFax
        End Get
        Set(ByVal value As Boolean)
            _blnInternetFax = value
        End Set
    End Property

    'eFax Login Key
    Public Property eFaxUserID() As String
        Get
            Return _seFaxUserID
        End Get
        Set(ByVal value As String)
            _seFaxUserID = value
        End Set
    End Property

    Public Property eFaxUserPassword() As String
        Get
            Return _seFaxUserPassword
        End Get
        Set(ByVal value As String)
            _seFaxUserPassword = value
        End Set
    End Property

    Public Property IsSurescriptEnabled() As Boolean
        Get
            Return _IsSurescriptEnabled
        End Get
        Set(ByVal value As Boolean)
            _IsSurescriptEnabled = value
        End Set
    End Property

    Public Property IsStagingServer() As Boolean
        Get
            Return _IsStagingServer
        End Get
        Set(ByVal value As Boolean)
            _IsStagingServer = value
        End Set
    End Property

    Public Property IsAdvErxEnabled() As Boolean
        Get
            Return _IsAdvErxEnabled
        End Get
        Set(ByVal value As Boolean)
            _IsAdvErxEnabled = value
        End Set
    End Property

    Public Property IsRxHubStagingServer() As Boolean
        Get
            Return _IsRxHubStagingServer
        End Get
        Set(ByVal value As Boolean)
            _IsRxHubStagingServer = value
        End Set
    End Property

    Public Property RxDeclarationText() As String
        Get
            Return m_RxDeclarationText
        End Get
        Set(ByVal Value As String)
            m_RxDeclarationText = Value
        End Set
    End Property

    Public Property RxFooterNote() As String
        Get
            Return m_RxFooterNote
        End Get
        Set(ByVal Value As String)
            m_RxFooterNote = Value
        End Set
    End Property

    Public Property SignatureText() As String
        Get
            Return _SignatureText
        End Get
        Set(ByVal value As String)
            _SignatureText = value
        End Set
    End Property

    Public Property CoSignatureText() As String
        Get
            Return _CoSignatureText
        End Get
        Set(ByVal value As String)
            _CoSignatureText = value
        End Set
    End Property

    'Procedure to retrive EMExamType
    Public Property EMExamType() As AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType
        Get
            Return _EMExamType
        End Get
        Set(ByVal value As AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType)
            _EMExamType = value
        End Set
    End Property

    Public Property VitalSettingsValue() As String
        Get
            Return _VitalSettingsValue
        End Get
        Set(ByVal value As String)
            _VitalSettingsValue = value
        End Set
    End Property

    Public Property ShowDMAlert() As String
        Get
            Return _showDMAlert
        End Get
        Set(ByVal value As String)
            _showDMAlert = value
        End Set
    End Property

    Public Property OBVitalSettingsValue() As String
        Get
            Return _OBVitalSettingsValue
        End Get
        Set(ByVal value As String)
            _OBVitalSettingsValue = value
        End Set
    End Property

#End Region

    Public Sub GetSurescriptSettings()
        Dim objCon As New SqlConnection

        Try
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "GetSurescriptServiceURL"
            objCmd.Connection = objCon

            objCon.Open()

            gstrSurescriptServiceURL = objCmd.ExecuteScalar()

            objCon.Close()

        Catch ex As Exception
            MessageBox.Show("Unable to get surescript service URL. Please do the settings through gloEMR-Admin ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Public Sub GetSettings()
        Dim dtTable As New DataTable

        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()
            Dim objCmd As New SqlCommand
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_FillSettings"
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
                    Case "ExternalCollectionfeature".ToUpper()
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        End If

                    Case "CQM CYPRESS TESTING"
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRAdminSettings.globlnEnableCypressTesting = CType(dtTable.Rows(nCount).Item(2), Boolean)

                        End If
                        'MU2 Features Setting
                    Case "ENABLE MU2 FEATURES".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            globlnEnableMultipleRaceFeatures = CType(dtTable.Rows(nCount).Item(2), Boolean)
                            gloEMRAdminSettings.globlnEnableMultipleRaceFeatures = globlnEnableMultipleRaceFeatures
                        End If

                        'Added by Mayuri:20120914-Hisotry PRD-Added settings optional,mandatory and warning
                    Case "CODEFIELDSINHISTORY".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrCodeFieldsinHistory = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                        End If

                        'Case Added For Checking Intuit Communication
                    Case "INTUIT FEATURE ENABLE SETTING".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnIntuitCommunication = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        End If

                    Case "INCLUDE ORIGINAL MESSAGE IN REPLY".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnIncludeMessageInReply = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        End If

                        'Start :: YES NO LAB SETTING Not if there is no setting then by default it is false
                    Case "YESNOLAB".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnYesNoLab = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                        End If
                        'END :: YES NO LAB SETTING

                        'Start :: PATIENT DEMOGRAPHIC MERGE
                    Case "PATIENTDAEMOMERG".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnPatDeamoMerg = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                        End If
                        'END :: PATIENT DEMOGRAPHIC MERGE


                        'Dhruv Added for the Demo application and used in the gloEDocumentV3
                    Case "COUNTRY".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrCountry = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                        End If

                    Case "Demo application".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEDocumentV3.gloEDocV3Admin.ISDMSDEMO = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                            'Added by madan on 20100701
                            If CType(dtTable.Rows(nCount).Item(2), Int16) = 1 Then
                                gloEmdeonInterface.Classes.clsEmdeonGeneral.IsDemoLab = True
                            Else
                                gloEmdeonInterface.Classes.clsEmdeonGeneral.IsDemoLab = False
                            End If
                            'End madan on 20100701
                        End If

                    Case "Clinic Start Time".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            _tmAppointmentStartTime = CType(dtTable.Rows(nCount).Item(2), DateTime)
                        End If

                    Case "Clinic Closing Time".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            _tmAppointmentEndTime = CType(dtTable.Rows(nCount).Item(2), DateTime)
                        End If

                    Case "Appointment Interval".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            _nAppointmentInterval = CType(dtTable.Rows(nCount).Item(2), Int16)
                        End If

                    Case "Pull Chart Interval".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            _nPULLCHARTSInterval = CType(dtTable.Rows(nCount).Item(2), Int16)
                        End If

                    Case "Max FAX Retries".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            _nNoOfAttempts = CType(dtTable.Rows(nCount).Item(2), Int16)
                        End If

                    Case "FAX Compression".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_FAXCompression = dtTable.Rows(nCount).Item(2)
                        Else
                            m_FAXCompression = "CCITT G3"
                        End If

                    Case "HPI".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            If CType(dtTable.Rows(nCount).Item(2), Int16) = 0 Then
                                m_HPIEnabled = False
                            Else
                                m_HPIEnabled = True
                            End If
                        Else
                            m_HPIEnabled = False
                        End If

                    Case "Pull Address".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            If CType(dtTable.Rows(nCount).Item(2), Int16) = 0 Then
                                m_LocationAddress = False
                            Else
                                m_LocationAddress = True
                            End If
                        Else
                            m_LocationAddress = False
                        End If

                    Case "OMR Category - History".ToUpper '- History
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_OMRCategory_History = CType(dtTable.Rows(nCount).Item(2), String)
                        End If

                    Case "OMR Category - ROS".ToUpper '- ROS
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_OMRCategory_ROS = CType(dtTable.Rows(nCount).Item(2), String)
                        End If

                    Case "OMR Category - Patient Registration".ToUpper '- Patient Registration
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_OMRCategory_PatientRegistration = CType(dtTable.Rows(nCount).Item(2), String)
                        End If

                    Case "OMR Category - Directive".ToUpper '- Patient Directive
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_DMSCategory_PatientDirective = CType(dtTable.Rows(nCount).Item(2), String)
                        End If

                    Case "Lab Category".ToUpper '- Labs
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_DMSCategory_Labs = CType(dtTable.Rows(nCount).Item(2), String)
                        End If

                    Case "Radiology Category".ToUpper '-Radiology
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_DMSCategory_Radiology = CType(dtTable.Rows(nCount).Item(2), String)
                        End If

                    Case "RxMeds Category".ToUpper '-Medication Reconcilation
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_DMSCategory_RxMed = CType(dtTable.Rows(nCount).Item(2), String)
                        End If
                        ''Added by Mayuri:20120118-VIS immunization category
                    Case "VISCATEGORY".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_DMSCategory_VIS = CType(dtTable.Rows(nCount).Item(2), String)
                        End If
                    Case "AmedmentsCategory".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_DMSCategory_Amedment = CType(dtTable.Rows(nCount).Item(2), String)
                        End If
                    Case "RXELIGIBILITYEMR".ToUpper '-EDI functionality thru web service
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_RXELIGIBILITYEMR = CType(dtTable.Rows(nCount).Item(2), String)
                        End If

                    Case "EDISERVICEPATH".ToUpper '-EDI functionality thru web service
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_EDISERVICEPATH = CType(dtTable.Rows(nCount).Item(2), String)
                        End If

                    Case "AutoEligibilityONorOFF".ToUpper ''Individual auto eRx Eligibility ON/Off
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            AutoEligibility = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        End If
                    Case "RxReportPath".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrRxReportpath = CType(dtTable.Rows(nCount).Item(2), String)
                        End If

                    Case "No. Of. Attempts".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_NoOfAttempts = CType(dtTable.Rows(nCount).Item(2), Integer)
                        Else
                            m_NoOfAttempts = 0
                        End If

                    Case "Clinic DI Settings".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_ClinicDISettings = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            m_ClinicDISettings = 0
                        End If

                        'Case "Clinic Formulary Settings".ToUpper
                        '    If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                        '        m_ClinicFormularySettings = CType(dtTable.Rows(nCount).Item(2), String)
                        '    Else
                        '        m_ClinicFormularySettings = 0
                        '    End If

                    Case "Record Level Locking".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            _blnRecordLevelLocking = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            _blnRecordLevelLocking = False
                        End If

                    Case "IsDrugDBMigrated".ToUpper
                        Try
                            If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                                gblIsDrugDBMigrated = CType(dtTable.Rows(nCount).Item(2), Boolean)
                            Else
                                gblIsDrugDBMigrated = False
                            End If
                        Catch ex As Exception
                            gblIsDrugDBMigrated = False
                        End Try

                    Case "Isnew".ToUpper
                        Try
                            If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                                gblIsNewInstallation = CType(dtTable.Rows(nCount).Item(2), Boolean)
                            Else
                                gblIsNewInstallation = False
                            End If
                        Catch ex As Exception
                            gblIsNewInstallation = False
                        End Try

                    Case "Threshold Value".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gnThresholdSetting = CType(dtTable.Rows(nCount).Item(2), Int32)
                        Else
                            gnThresholdSetting = 420
                        End If

                        'Code added by Anil on 20071119
                    Case "Auto-Generate Patient Code".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            If CType(dtTable.Rows(nCount).Item(2), Int16) = 0 Then
                                _blnAutoPatientCode = False
                            Else
                                _blnAutoPatientCode = True
                            End If
                        Else
                            _blnAutoPatientCode = False
                        End If

                    Case "VITALS HEIGHT COPY FORWARD".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            If CType(dtTable.Rows(nCount).Item(2), Int16) = 0 Then
                                _isVitalsHeightCopyForward = False
                            Else
                                _isVitalsHeightCopyForward = True
                            End If
                        Else
                            _isVitalsHeightCopyForward = False
                        End If
                    Case "EnableSmartDxReviewScreen".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            If CType(dtTable.Rows(nCount).Item(2), Int16) = 0 Then
                                _EnableSamrtDxReviewScreen = False
                            Else
                                _EnableSamrtDxReviewScreen = True
                            End If
                        Else
                            _EnableSamrtDxReviewScreen = False
                        End If
                        'sarika internet fax
                    Case "InternetFax".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            _blnInternetFax = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            _blnInternetFax = False
                        End If

                        'eFax Login Key
                    Case "eFax User ID".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            _seFaxUserID = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            _seFaxUserID = ""
                        End If

                    Case "eFax User Password".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            _seFaxUserPassword = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            _seFaxUserPassword = ""
                        End If

                        'sarika internet fax
                    Case "Surescript Enabled".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            _IsSurescriptEnabled = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            _IsSurescriptEnabled = False
                        End If

                    Case "StagingServer".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            _IsStagingServer = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            _IsStagingServer = False
                        End If

                        'code added by supriya 25/7/2008
                    Case "ADVANCE RX ENABLED".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            _IsAdvErxEnabled = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            _IsAdvErxEnabled = False
                        End If

                    Case "ADVANCE RX STAGING SERVER".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            _IsRxHubStagingServer = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            _IsRxHubStagingServer = False
                        End If

                    Case UCase("RXDECLARATION")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_RxDeclarationText = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            m_RxDeclarationText = ""
                        End If

                    Case "GENERATEMIC".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_getMic = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            m_getMic = False
                        End If

                    Case "MCIR REPORT PATH".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_MCIRReportPath = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            m_MCIRReportPath = False
                        End If
                        'Advanced Growth Chart

                    Case UCase("Advanced Growth Chart")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_AdvancedGrowthChart = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            m_AdvancedGrowthChart = False
                        End If
                        'CCD file path

                    Case UCase("CCD File PATH")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_CCDFilePath = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            m_CCDFilePath = ""
                        End If
                    Case UCase("Style Sheet Path")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_StyleSheetPath = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            m_StyleSheetPath = ""
                        End If
                    Case UCase("SIGNATURETEXT")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            _SignatureText = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            _SignatureText = ""
                        End If

                    Case UCase("COSIGNATURETEXT")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            _CoSignatureText = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            _CoSignatureText = False
                        End If

                    Case UCase("EM CHIEFCOMPLAINT TYPE")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrChiefComplaintType = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrChiefComplaintType = "ChiefComplaint"
                        End If

                    Case UCase("OtherPatientType")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gbOtherPatientType = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gbOtherPatientType = False
                        End If

                        'RxHub MedicationHx Disclaimer
                    Case UCase("RXHUBDISCLAIMER")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_RxHubDisclaimer = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            m_RxHubDisclaimer = ""
                        End If

                        'RxHub Staging\Production participant id
                    Case UCase("RXHUB PARTICIPANTID")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_RxHubParticipantId = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            m_RxHubParticipantId = ""
                        End If
                        'RxHub Staging\Production participant id

                        'RxHub Staging\Production Password
                    Case UCase("RXHUB PASSWORD")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            m_RxHubPassword = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            m_RxHubPassword = ""
                        End If

                    Case "GeniusCode".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrGeniusCode = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrGeniusCode = ""
                        End If

                    Case "GeniusPath".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrGeniusPath = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrGeniusPath = ""
                        End If

                    Case "HL7 System Path".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrHL7MessagePath = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrHL7MessagePath = ""
                        End If

                        'for Genius username and password....
                    Case "GeniusUsername".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrGeniusUsername = CType(dtTable.Rows(nCount).Item(2), String)
                            'gstrGeniusUsername = "GENIUSUSERNAME"
                        Else
                            gstrGeniusUsername = ""
                        End If

                    Case "GeniusPassword".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            Dim objclsencryption As clsencryption = New clsencryption()
                            'gstrGeniusPassword = mdlGeneral.constEncryptDecryptKey_Services
                            gstrGeniusPassword = objclsencryption.DecryptFromBase64String(CType(dtTable.Rows(nCount).Item(2), String), mdlGeneral.constEncryptDecryptKey.ToString())
                            'gstrGeniusPassword = "GENIUSPASSWORD"
                        Else
                            gstrGeniusPassword = ""
                        End If

                    Case "Exam Diagnosis".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            If dtTable.Rows(nCount).Item(2).ToString.Trim <> "" Then
                                gnGeniusICD9Driven = CType(dtTable.Rows(nCount).Item(2), Int64)
                                gblnICD9Driven = CType(dtTable.Rows(nCount).Item(2), Boolean)
                            Else
                                gnGeniusICD9Driven = 1
                                gblnICD9Driven = True
                            End If
                        Else
                            gnGeniusICD9Driven = 1
                            gblnICD9Driven = True
                        End If
                        gloEmdeonInterface.Classes.clsEmdeonGeneral.gblnICD9Driven = gblnICD9Driven

                    Case "SET CPT TO ALL ICD9".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnSetCPTtoAllICD9 = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnSetCPTtoAllICD9 = False
                        End If
                        gloEmdeonCommon.mdlGeneral.gblnSetCPTtoAllICD9 = gblnSetCPTtoAllICD9
                    Case "SHOWDXCPTSCREENONSMARTDX".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnShowDXCPTScreenOnSmartDx = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnShowDXCPTScreenOnSmartDx = False
                        End If
                        '  gloEmdeonCommon.mdlGeneral.gblnSetCPTtoAllICD9 = gblnSetCPTtoAllICD9
                    Case "SHOW 8 ICD9".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnShow8ICD9 = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnShow8ICD9 = True
                        End If
                        gloEmdeonCommon.mdlGeneral.gblnSetCPTtoAllICD9 = gblnSetCPTtoAllICD9
                    Case "SAVELIQUIDDATA".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnSAVELIQUIDDATA = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnSAVELIQUIDDATA = False
                        End If
                    Case "LOCKDATEFIELD".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnLOCKDATEFIELD = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnLOCKDATEFIELD = False
                        End If
                    Case "SHOW 4 MODIFIER".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnShow4Modifier = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnShow4Modifier = True
                        End If
                        gloEmdeonCommon.mdlGeneral.gblnShow4Modifier = gblnShow4Modifier

                        '' PT Billing : Setting to control visibility of Timed & Untimed Therapy Columns on DxCPT Screen
                    Case "IsExamPTBillingEnabled".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnIsExamPTBillingEnabled = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnIsExamPTBillingEnabled = True
                        End If
                        gloEmdeonCommon.mdlGeneral.gblnIsExamPTBillingEnabled = gblnIsExamPTBillingEnabled

                    Case "MULTIPLE SUPERVISORS FOR PAPER RX".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnMultipleSupervisorsforPaperRx = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnMultipleSupervisorsforPaperRx = True
                        End If

                    Case "PATIENT QUESTIONNAIRE".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnPatientQuestionnaire = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnPatientQuestionnaire = False
                        End If

                    Case "gloLab User Name".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEmdeonInterface.Classes.clsEmdeonGeneral.emdeonUserName = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                        End If

                    Case "gloLab Password".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            Dim oEncryption As New clsencryption
                            gloEmdeonInterface.Classes.clsEmdeonGeneral.emdeonUserPassword = (oEncryption.DecryptFromBase64String(CType(dtTable.Rows(nCount).Item(2), String), "20gloStreamInc08")).Trim()
                            oEncryption = Nothing
                        End If

                    Case "gloLab URL".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEmdeonInterface.Classes.clsEmdeonGeneral.emdeonURL = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                        End If

                    Case "gloLab Facility Code".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEmdeonInterface.Classes.clsEmdeonGeneral.emdeonFacilityCode = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                        End If

                    Case "gloLab billing type".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_BillingStatus = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                        End If

                    Case "gloLab hsi label".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_hsilabel = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                        End If
                        'end
                        'Added by madan on 20100614
                    Case "GLOLAB PROVIDER USAGE".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_Provider_Usage = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                        End If
                        'End madan Changes on 20100614
                        ''''''''Seperate Formulary Database 
                    Case "FORMULARYAUTHENTICATION".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sFORMULARYAUTHENTICATION = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                        End If
                    Case "FORMULARYSERVERNAME".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sFORMULARYSERVERNAME = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                        End If
                    Case "FORMULARYDATABASENAME".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sFORMULARYDATABASENAME = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                        End If
                    Case "FORMULARYUSERID".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sFORMULARYUSERID = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                        End If
                    Case "FORMULARYPASSWORD".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sFORMULARYPASSWORD = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                        End If

                        'Added Rahul for Snomed Setting on 20101006
                    Case "GLOSMDBSETTING".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnSMDBSetting = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.blnSMDBSetting = gblnSMDBSetting
                        End If

                    Case "GLOSMSERVERNAME".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrSMDBServerName = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrSMDBServerName = gstrSMDBServerName
                        End If

                    Case "GLOSMDBNAME".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrSMDBDatabaseName = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrSMDBDatabaseName = gstrSMDBDatabaseName
                        End If

                    Case "GLOSMUSERID".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrSMDBUserID = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrSMDBUserID = gstrSMDBUserID
                        End If

                        'Start :: SnowMade Password 
                    Case "GLOSMPASSWORD".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrSMDBPassWord = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                            Dim oEncryption As New clsencryption
                            If Not IsNothing(oEncryption) Then
                                gstrSMDBPassWord = oEncryption.DecryptFromBase64String(gstrSMDBPassWord, constEncryptDecryptKey)
                                gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrSMDBPassWord = gstrSMDBPassWord
                                oEncryption = Nothing
                            End If
                        End If
                        'End :: SnowMade Password 

                    Case "GLOSMAUTHEN".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnSMDBAuthen = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                        End If

                        'Code Start-Added  by kanchan on 20100909 for rxnorm db settings
                    Case "GLORxNSERVERNAME".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gRxNServerName = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                            ' added by Rahul patel for RxNorm Database setting on 23-10-2010
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNServerName = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                            gloCCDLibrary.gloLibCCDGeneral.gRxNServerName = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                        End If

                    Case "GLORxNDBNAME".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gRxNDatabaseName = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                            ' added by Rahul patel for RxNorm Database setting on 23-10-2010
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNDatabaseName = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                            gloCCDLibrary.gloLibCCDGeneral.gRxNDatabaseName = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                        End If

                    Case "GLORxNUSERID".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gRxNUserID = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                            ' added by Rahul patel for RxNorm Database setting on 23-10-2010
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNUserID = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                        End If

                        'Start :: RxNorm Password
                    Case "GLORxNPASSWORD".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gRxNPassWord = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                            If gRxNPassWord <> "" Then
                                Dim oEncryptions As New clsencryption
                                If Not IsNothing(oEncryptions) Then
                                    'gRxNPassWord = oEncryptions.DecryptFromBase64String(gRxNPassWord, constEncryptDecryptKey)
                                    ' added by Rahul patel for RxNorm Database setting on 23-10-2010
                                    gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNPassWord = oEncryptions.DecryptFromBase64String(gRxNPassWord, constEncryptDecryptKey)

                                    '09-Jan-15 Aniket: Resolving Bug #78290: gloEMR >Not able to login gloEMR application 
                                    gRxNPassWord = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNPassWord
                                    oEncryptions = Nothing
                                End If
                            End If
                        End If
                        'End :: RxNorm Password

                    Case "GLORxNAUTHEN".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gRxNIsSQLAUTHEN = CType(dtTable.Rows(nCount).Item(2), Boolean)
                            ' added by Rahul patel for RxNorm Database setting on 23-10-2010
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNIsSQLAUTHEN = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gRxNIsSQLAUTHEN = False
                            ' added by Rahul patel for RxNorm Database setting on 23-10-2010
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gRxNIsSQLAUTHEN = False
                        End If

                    Case "GLORxNDBSETTING".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnRxNDBSetting = CType(dtTable.Rows(nCount).Item(2), Boolean)
                            'added by Rahul patel for RxNorm Database setting on 23-10-2010
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnRxNDBSetting = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnRxNDBSetting = False
                            'added by Rahul patel for RxNorm Database setting on 23-10-2010
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnRxNDBSetting = False
                        End If
                        'Code End-Added  by kanchan on 20100909 for rxnorm db settings

                    Case "VITALSCUSTOMIZATION".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            _VitalSettingsValue = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            _VitalSettingsValue = ""
                        End If

                        ''End code Added by Mayuri:20100608

                    Case "SHOWDMALERT".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            _showDMAlert = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            _showDMAlert = ""
                        End If
                        'Added by Ujwala - for Snomed Immunization  - as on 20101007

                    Case "GLOSMTRV"
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            _SMDBsettings = CType(dtTable.Rows(nCount).Item(2), String).Trim().Split(",")
                            gstrSMHistory = ""
                            gstrSMImmunization = ""
                            gstrSMProblem = ""
                            For i As Integer = 0 To _SMDBsettings.Count - 1
                                If _SMDBsettings(i).ToUpper.Trim() = "HISTORY" Then
                                    gstrSMHistory = "History"
                                ElseIf _SMDBsettings(i).ToUpper.Trim() = "IMMUNIZATION" Then
                                    gstrSMImmunization = "Immunization"
                                ElseIf _SMDBsettings(i).ToUpper.Trim() = "PROBLEM" Then
                                    gstrSMProblem = ""
                                End If
                            Next
                        End If
                        'Added by Ujwala - for Snomed Immunization  - as on 20101007
                        'Added By Rahul Patel for retiriving  MMW Database Setting  on 21-10-2010-----'

                    Case "GLOMMWSERVERNAME".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwServerName = CType(dtTable.Rows(nCount).Item(2), String)
                            gloCCDLibrary.gloLibCCDGeneral.sMmwServerName = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwServerName = ""
                            gloCCDLibrary.gloLibCCDGeneral.sMmwServerName = ""
                        End If

                    Case "GLOMMWDBNAME".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwDatabaseName = CType(dtTable.Rows(nCount).Item(2), String)
                            gloCCDLibrary.gloLibCCDGeneral.sMmwDatabaseName = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwDatabaseName = ""
                            gloCCDLibrary.gloLibCCDGeneral.sMmwDatabaseName = ""
                        End If

                    Case "GLOMMWUSERID".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwUserId = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwUserId = ""
                        End If

                    Case "GLOMMWPASSWORD".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwPassword = CType(dtTable.Rows(nCount).Item(2), String)
                            Dim oMMWEncryptions As New clsencryption
                            If Not IsNothing(oMMWEncryptions) Then
                                gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwPassword = oMMWEncryptions.DecryptFromBase64String(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwPassword, constEncryptDecryptKey)
                                oMMWEncryptions = Nothing
                            End If
                        Else
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwPassword = ""
                        End If

                    Case "GLOMMWAUTHEN".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sIsSqlAuthentication = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sIsSqlAuthentication = False
                        End If

                        '--- Added by Rahul Patel on 26-10-2010
                        '--- For DMS Database setting retriving ---'

                    Case "GLODMSSERVERNAME".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsServerName = CType(dtTable.Rows(nCount).Item(2), String)
                            gloEDocumentV3.gloEDocV3Admin.gstrDMSSqlServerName = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsServerName = ""
                            gloEDocumentV3.gloEDocV3Admin.gstrDMSSqlServerName = ""
                        End If

                    Case "GLODMSDBNAME".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsDatabaseName = CType(dtTable.Rows(nCount).Item(2), String)
                            gloEDocumentV3.gloEDocV3Admin.gstrDMSDatabaseName = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsDatabaseName = ""
                            gloEDocumentV3.gloEDocV3Admin.gstrDMSDatabaseName = ""
                        End If

                    Case "GLODMSUSERID".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsUserId = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsUserId = ""
                        End If

                    Case "GLODMSPASSWORD".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsPassword = CType(dtTable.Rows(nCount).Item(2), String)
                            Dim oMMWEncryptions As New clsencryption
                            If Not IsNothing(oMMWEncryptions) Then
                                If gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsPassword.Trim <> "" Then
                                    gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsPassword = oMMWEncryptions.DecryptFromBase64String(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsPassword, constEncryptDecryptKey)
                                Else
                                    gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsPassword = ""
                                End If
                                oMMWEncryptions = Nothing
                            End If
                        Else
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sMmwPassword = ""
                        End If

                    Case "GLODMSAUTHEN".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsIsSqlAuthentication = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsIsSqlAuthentication = False
                        End If

                        '--- End of Code Added by Rahul Patel for DMS database Setting ---'

                        '-------End of code added by rahul patel for MMW Database Setting ----------'
                        'Code Added by kanchan on 20100531
                    Case "GloImmunizationSetting".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnIsImmunization = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnIsImmunization = False
                        End If

                        'Code Added by kanchan on 20100605 for CCD
                    Case "CCD Default UserID".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gnCCDDefaultUserID = CType(dtTable.Rows(nCount).Item(2), Long)
                        Else
                            gnCCDDefaultUserID = 0
                        End If

                        ''Added by Abhijeet on 20101109 for HL7 Database connection string
                    Case "GLOHL7SERVERNAME".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrHL7ServerName = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrHL7ServerName = ""
                        End If

                        gloSettings.HL7Settings.gloHL7ServerName = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrHL7ServerName

                    Case "GLOHL7DBNAME".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrHL7DatabaseName = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrHL7DatabaseName = ""
                        End If

                        gloSettings.HL7Settings.gloHL7DatabaseName = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrHL7DatabaseName

                    Case "GLOHL7USERID".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrHL7UserId = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrHL7UserId = ""
                        End If

                        gloSettings.HL7Settings.gloHL7UserID = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrHL7UserId

                    Case "GLOHL7PASSWORD".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrHL7Password = CType(dtTable.Rows(nCount).Item(2), String)
                            Dim oMMWEncryptions As New clsencryption
                            If Not IsNothing(oMMWEncryptions) Then
                                If gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrHL7Password.Trim <> "" Then
                                    gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrHL7Password = oMMWEncryptions.DecryptFromBase64String(gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrHL7Password, constEncryptDecryptKey)
                                Else
                                    gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrHL7Password = ""
                                End If
                                oMMWEncryptions = Nothing
                            End If
                        Else
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrHL7Password = ""
                        End If

                        gloSettings.HL7Settings.gloHL7Password = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrHL7Password

                    Case "GLOHL7AUTHEN".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gboolHL7IsSQLAuthentication = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gboolHL7IsSQLAuthentication = False
                        End If

                        gloSettings.HL7Settings.IsSQLAuthenticationOnForGloHL7 = gloEMRGeneralLibrary.gloGeneral.clsgeneral.gboolHL7IsSQLAuthentication
                        ''End of changes by Abhijeet on 20101109 for HL7 database connection string

                        'Code Start-Added by kanchan on 20101112 for patient confidential info
                    Case "IsShowPatientConfiInfo".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblIsConfidentialInfo = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblIsConfidentialInfo = False
                        End If

                        ''Added by Abhijeet on 20101129
                    Case "PatientSpecificResultRange".ToUpper()
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrSpecificResultRange = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrSpecificResultRange = "0"
                        End If

                        ''Added by Abhijeet on 20110422
                    Case "IMRegistryHL7Format".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gboolIMRegistryHL7Format = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gboolIMRegistryHL7Format = False
                        End If

                        'For Recover Exam Module.
                    Case "GLORECOVEREXAM".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            _isEnableRecoverExam = Convert.ToInt32(dtTable.Rows(nCount).Item(2))
                        Else
                            _isEnableRecoverExam = 0
                        End If

                        ''gloCommunity Region -: Added for gloCommunity server setting on 20110823"
                    Case "gloCommunityServerName".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrSharepointSrvNm = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrSharepointSrvNm = ""
                        End If

                    Case "gloCommunitySiteName".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrSharepointSiteNm = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrSharepointSiteNm = ""
                        End If

                    Case "COMMUNITYCONNECT".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrCommsrv = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrCommsrv = ""
                        End If

                    Case "ADFSServerName".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrDomainName = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrDomainName = ""
                        End If

                        'chetan added on  20120102 for gloCommunity setting
                    Case "gloCommunity Feature".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblngloCommunity = CType(dtTable.Rows(nCount).Item(2), Boolean)

                        Else
                            gblngloCommunity = False

                        End If

                        ''Added by Pranit on 20120105
                    Case "OBVITALSCUSTOMIZATION".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            _OBVitalSettingsValue = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            _OBVitalSettingsValue = ""
                        End If

                    Case "CommunityEnvironment".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnIscommunityStaging = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnIscommunityStaging = True
                        End If

                    Case "CCDAViewerUrl".ToUpper()
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrCCDAViewerURL = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrCCDAViewerURL = ""
                        End If

                    Case "CCDAImportCategories".ToUpper()
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrCCDAImportCategory = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrCCDAImportCategory = ""
                        End If


                        ''Added for check which authentication is use for access gloCommunity on 20120730
                    Case "gloCommunityAuthentication".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrgloCommunityAuthentication = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrgloCommunityAuthentication = ""
                        End If

                    Case "Include Frequency Abbrevation In RxMeds".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnIncludeFrequencyAbbrevationInRxMeds = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnIncludeFrequencyAbbrevationInRxMeds = False
                        End If

                    Case "SECURE MESSAGE ENABLED".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnIsSecureMsgEnable = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnIsSecureMsgEnable = False
                        End If
                    Case "SECUREMSGSTAGINGSERVER".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnIsSecureStagingsever = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnIsSecureStagingsever = False
                        End If

                    Case "SECUREMSGSTAGINGWEBSERVICEURL".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrSecureStagingUrl = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrSecureStagingUrl = ""
                        End If
                    Case "SECUREMSGPRODUCTIONWEBSERVICEURL".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrSecureProductionUrl = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrSecureProductionUrl = ""
                        End If
                        ''Added by Mayuri:20100513-Seeting whether allow user to remove current patient data during reconciliaiton.
                    Case "REMOVEPATIENTCURRENTDATA".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnRemovePatientCurrentData = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                            ' gloEMRGeneralLibrary.glogeneral.clsgeneral.blnRemovePatientDataSetting = gblnRemovePatientCurrentData
                            gloCCDLibrary.gloLibCCDGeneral.blnRemovePatientDataSetting = gblnRemovePatientCurrentData
                        End If

                        ''Added for MU2 Patient portal implementation on 20130702
                    Case "USEINTUITINTERFACE".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnUSEINTUITINTERFACE = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnUSEINTUITINTERFACE = False
                        End If
                    Case "PatientPortalEnabled".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnPatientPortalEnabled = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnPatientPortalEnabled = False
                        End If
                    Case "PatientPortalSiteName".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrPatientPortalSiteNm = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrPatientPortalSiteNm = String.Empty
                        End If
                    Case "PatientPortalEmailService".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrPatientPortalEmailService = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrPatientPortalEmailService = ""
                        End If
                    Case "ALLOWREFILLCANCEL".ToUpper ''this setting is added to show/hide the cancel button in Pending refill request screen.
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnAllowRefillCancel = CType(dtTable.Rows(nCount).Item(2), String).Trim()
                        End If
                        ''End
                    Case "PATIENTSAVINGMESSAGEENABLED".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnIsPatientSavingEnabled = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnIsPatientSavingEnabled = False
                        End If
                    Case "SHOWPATIENTSAVINGDIRECTMESSAGE".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnEnablePatientSavingsInbox = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnEnablePatientSavingsInbox = False
                        End If
                    Case "PatientPortalAutoCompleteTask".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnPatientPortalAutoCompleteTaskEnabled = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnPatientPortalAutoCompleteTaskEnabled = False
                        End If

                    Case "PATIENTPORTALPFAUTOCOMPLETETASK".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnPatientPortalPFAutoCompleteTaskEnabled = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnPatientPortalPFAutoCompleteTaskEnabled = False
                        End If


                    Case ("GLODIURL")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrDrugInteractionServiceURL = CType(dtTable.Rows(nCount).Item(2), String)
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDruginteractionServiceURL = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrDrugInteractionServiceURL = ""
                        End If
                    Case ("ISFORMULARYSERVICEENABLED")
                        'Added by Ashish on 2nd March 2015 for Centralized Formulary 3.0 changes
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = True Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnIsFormularyServiceEnabled = False
                        Else
                            If TypeOf (dtTable.Rows(nCount).Item(2)) Is String Then
                                gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnIsFormularyServiceEnabled = CType(dtTable.Rows(nCount).Item(2), Boolean)
                            Else
                                gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnIsFormularyServiceEnabled = False
                            End If
                        End If
                    Case ("GLOFORMULARYURL")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = True Then
                            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrFormularyServiceURL = String.Empty
                        Else
                            If TypeOf (dtTable.Rows(nCount).Item(2)) Is String Then
                                gstrFormularyServiceURL = CType(dtTable.Rows(nCount).Item(2), String)
                            Else
                                gstrFormularyServiceURL = String.Empty
                            End If
                        End If

                    Case "GLODIBURL"
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = True Then
                            gstrDIBServiceURL = String.Empty
                        Else
                            If TypeOf (dtTable.Rows(nCount).Item(2)) Is String Then
                                gstrDIBServiceURL = CType(dtTable.Rows(nCount).Item(2), String)
                                gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDIBServiceURL = CType(dtTable.Rows(nCount).Item(2), String)
                                gloCCDLibrary.gloLibCCDGeneral.sDIBServiceURL = CType(dtTable.Rows(nCount).Item(2), String)
                            Else
                                gstrDIBServiceURL = String.Empty
                            End If
                        End If
                    Case "GLOEPAURL"
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = True Then
                            gstrEPAServiceURL = String.Empty
                        Else
                            If TypeOf (dtTable.Rows(nCount).Item(2)) Is String Then
                                gstrEPAServiceURL = CType(dtTable.Rows(nCount).Item(2), String)
                            Else
                                gstrEPAServiceURL = String.Empty
                            End If
                        End If
                    Case "GLOEPAAPIURL"
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = True Then
                            gstrEPAAPIURL = String.Empty
                        Else
                            If TypeOf (dtTable.Rows(nCount).Item(2)) Is String Then
                                gstrEPAAPIURL = CType(dtTable.Rows(nCount).Item(2), String)
                            Else
                                gstrEPAAPIURL = String.Empty
                            End If
                        End If
                    Case "MEDHXTENDOTSIXSTAGINGURL".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrMedHXStagingUrl = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrMedHXStagingUrl = ""
                        End If
                    Case "MEDHXTENDOTSIXPRODUCTIONGURL".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrMedHXProductionUrl = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrMedHXProductionUrl = ""
                        End If
                    Case UCase("RXDECLARATION")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            RxDeclaration = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            RxDeclaration = ""
                        End If
                    Case UCase("RXHUBDISCLAIMER")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrRxHubDisclaimer = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrRxHubDisclaimer = ""
                        End If
                        'code added by supriya 11/7/2008

                    Case UCase("PrintMultipleRx_PerScriptPage_setting")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrPrintMultipleRx_PerScriptPage = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrPrintMultipleRx_PerScriptPage = False
                        End If
                    Case UCase("CustomizeRxReportSetting")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnIsCustomizeReport = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnIsCustomizeReport = False

                        End If
                    Case UCase("PrintPharmacyOnRxReportSetting")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnIsPharymacyInclude = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnIsPharymacyInclude = True
                        End If
                    Case UCase("MULTIPLERXSTATECUSTIOMIZEREPORT")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            Dim strArr() As String = Nothing
                            strArr = CType(dtTable.Rows(nCount).Item(2), String).Split(".")
                            gstrMultipleRxCustomizeReport = strArr(0)
                        Else
                            gstrMultipleRxCustomizeReport = ""

                        End If
                    Case UCase("QRDAIIISUBMISSIONPATH")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = True Then
                            gstrQPPServiceURL = String.Empty
                        Else
                            If TypeOf (dtTable.Rows(nCount).Item(2)) Is String Then
                                gstrQPPServiceURL = CType(dtTable.Rows(nCount).Item(2), String)
                            Else
                                gstrQPPServiceURL = String.Empty
                            End If
                        End If
                    Case UCase("SINGLERXSTATECUSTIOMIZEREPORT")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            Dim strArr() As String = Nothing
                            strArr = CType(dtTable.Rows(nCount).Item(2), String).Split(".")
                            gstrSingleRxCustomizeReport = strArr(0)
                        Else
                            gstrSingleRxCustomizeReport = ""
                        End If
                    Case UCase("OTCISSUEWARNING")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnIsOTCIssueWarningEnabled = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnIsOTCIssueWarningEnabled = True
                        End If
                    Case UCase("OB SPECIALITY")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnIsOBSpecialityEnabled = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnIsOBSpecialityEnabled = False
                        End If
                    Case UCase("EpcsEnableSetting")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnEpcsEnabled = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnEpcsEnabled = False
                        End If
                    Case UCase("AllowPrintForCSSetting")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnAllowPrintForCSEnabled = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            gblnAllowPrintForCSEnabled = True
                        End If
                    Case UCase("AdvChartLicensekey")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrAdvChartLicensekey = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrAdvChartLicensekey = ""
                        End If
                    Case UCase("AdvChartExtLicensekey")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrAdvChartExtLicensekey = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrAdvChartExtLicensekey = True
                        End If
                    Case "USESIGNATUREPAD".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            If CType(dtTable.Rows(nCount).Item(2), String).ToUpper = "NEW" Then
                                gblbUseNewSignaturePad = True
                            Else
                                gblbUseNewSignaturePad = False
                            End If
                        Else
                            gblbUseNewSignaturePad = False
                        End If
                    Case "PDR_PC_PORTALID".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = True Then
                            gstrPDR_PC_PortalID = String.Empty
                        Else
                            If TypeOf (dtTable.Rows(nCount).Item(2)) Is String Then
                                gstrPDR_PC_PortalID = CType(dtTable.Rows(nCount).Item(2), String)
                            Else
                                gstrPDR_PC_PortalID = String.Empty
                            End If
                        End If

                    Case "PDR_PC_PFORMAT".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = True Then
                            gstrPDR_PC_PFormat = String.Empty
                        Else
                            If TypeOf (dtTable.Rows(nCount).Item(2)) Is String Then
                                gstrPDR_PC_PFormat = CType(dtTable.Rows(nCount).Item(2), String).ToLower()
                            Else
                                gstrPDR_PC_PFormat = String.Empty
                            End If
                        End If
                    Case "PDR_PC_URL".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = True Then
                            gstrPDR_URL = String.Empty
                        Else
                            If TypeOf (dtTable.Rows(nCount).Item(2)) Is String Then
                                gstrPDR_URL = CType(dtTable.Rows(nCount).Item(2), String).ToLower()
                            Else
                                gstrPDR_URL = String.Empty
                            End If
                        End If
                    Case "PDR_EnableSerialization".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = True Then
                            gbnlPDR_EnableSerialization = False
                        Else
                            If TypeOf (dtTable.Rows(nCount).Item(2)) Is String Then
                                gbnlPDR_EnableSerialization = CType(dtTable.Rows(nCount).Item(2), Boolean)
                            Else
                                gbnlPDR_EnableSerialization = False
                            End If
                        End If
                    Case "CCDAValidatorAPIUrl".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloCCDLibrary.gloLibCCDGeneral.sCDAValidatorUrl = Convert.ToString(dtTable.Rows(nCount).Item(2))
                        End If
                    Case "SendCDAExamFinish".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnsendcdafinishExam = Convert.ToBoolean(dtTable.Rows(nCount).Item(2))
                        End If
                    Case "PromptProviderForCDASend".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gblnPromptProviderForCDASend = Convert.ToBoolean(dtTable.Rows(nCount).Item(2))
                        End If
                    Case "HoosKoosSurveyService".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrHoosKoosSurveyUrl = Convert.ToString(dtTable.Rows(nCount).Item(2))
                        End If
                    Case "PDMPService".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            PDMPServiceURL = Convert.ToString(dtTable.Rows(nCount).Item(2))
                        End If
                    Case "PDMPAutoSettingEnable".ToUpper()
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            AutoPDMPEligiblity = Convert.ToString(dtTable.Rows(nCount).Item(2))
                        End If

                    Case "PDMPSendToDMS".ToUpper()
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            PDMPSendToDMS = Convert.ToString(dtTable.Rows(nCount).Item(2))
                        End If

                    Case "PDMP_Username".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            PDMPUsername = Convert.ToString(dtTable.Rows(nCount).Item(2))
                        End If
                    Case "PDMP_Password".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            PDMPPassword = Convert.ToString(dtTable.Rows(nCount).Item(2))
                        End If

                    Case "SendUnstructuredDocument".ToUpper()
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gloSettings.gloEMRAdminSettings.XDmMessageEnabled = Convert.ToString(dtTable.Rows(nCount).Item(2))
                        End If
                End Select
            Next

            GetSurescriptSettings()

            Fill_Clinic()
            GetgloCommunitySetting()

        Catch ex As Exception
            MessageBox.Show("Unable to get all settings. Please do the settings through gloEMR-Admin ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            If IsNothing(dtTable) = False Then
                dtTable.Dispose() : dtTable = Nothing
            End If
        End Try
    End Sub

    '' added for gloCommunity Cinic Name Setting
    Private Sub Fill_Clinic()
        Dim oCon As New SqlConnection(GetConnectionString())
        Dim oCmd As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim strQuery As String = String.Empty
        Dim dtClinics As New DataTable()
        Try
            'Get the Clinic Information
            strQuery = "select ISNULL(nClinicID,0) AS nClinicID ,ISNULL(sClinicName,'') AS sClinicName,ISNULL(sState,'') AS sClinicState,ISNULL(sSiteID,'') as SiteID,ISNULL(sSiteID,'') as Location,ISNULL(sExternalcode,'') as AUSID, ISNULL(sAddress1, '') AS sAddress, ISNULL(sCity, '') AS sCity, ISNULL(sZip, '') AS sZip, ISNULL(sPhoneNo, '') AS sPhoneNo, ISNULL(sFax, '') AS sFax from Clinic_MST"
            oCmd.Connection = oCon
            oCmd.CommandText = strQuery
            da.SelectCommand = oCmd
            da.Fill(dtClinics)
            If dtClinics IsNot Nothing AndAlso dtClinics.Rows.Count > 0 Then
                gnClinicID = dtClinics.Rows(0)("nClinicID")
                gstrClinicName = dtClinics.Rows(0)("sClinicName")
                gstrClinicState = dtClinics.Rows(0)("sClinicState")
                gstrSiteID = dtClinics.Rows(0)("SiteID")
                gstrLocation = dtClinics.Rows(0)("Location")                
                gstrAUSID = dtClinics.Rows(0)("AUSID")
                'gstrAUSID = "999999999"
                gstrClinicAddress = dtClinics.Rows(0)("sAddress")
                gstrClinicCity = dtClinics.Rows(0)("sCity")
                gstrClinicZip = dtClinics.Rows(0)("sZip")
                gstrClinicPhone = dtClinics.Rows(0)("sPhoneNo")
                gstrClinicFax = dtClinics.Rows(0)("sFax")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.[Error])
        Finally

            strQuery = Nothing

            If Not IsNothing(oCon) Then
                oCon.Dispose() : oCon = Nothing
            End If

            If Not IsNothing(oCmd) Then
                oCmd.Parameters.Clear()
                oCmd.Dispose() : oCmd = Nothing
            End If

            If Not IsNothing(da) Then
                da.Dispose() : da = Nothing
            End If

            If Not IsNothing(dtClinics) Then
                dtClinics.Dispose() : dtClinics = Nothing
            End If

        End Try
    End Sub

    Public Sub GetVendorAndUrlInformationForEpcs(isStaging As Boolean)
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
        Dim RouterName As String = ""
        Try
            Dim objCon As New SqlConnection
            objCon.ConnectionString = GetConnectionString()
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
                    Case "RouterName".ToUpper

                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            RouterName = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            RouterName = ""
                        End If

                End Select
            Next
        Catch ex As Exception
            MessageBox.Show("Unable to get vendor settings. Please do the settings through gloEMR ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            If IsNothing(dtTable) = False Then
                dtTable.Dispose() : dtTable = Nothing
            End If
        End Try
        If isStaging Then
            gstrVendorName = StagingVendorName
            gstrVendorLabel = StagingVendorLabel
            gstrVendorNodeName = StagingVendorNodeName
            gstrVendorNodeLabel = StagingVendorNodeLabel
            gstrEpcsUrl = StagingEpcsUrl
            gstrSharedSecret = StagingsharedSecret
        Else
            gstrVendorName = ProductionVendorName
            gstrVendorLabel = ProductionVendorLabel
            gstrVendorNodeName = ProductionVendorNodeName
            gstrVendorNodeLabel = ProductionVendorNodeLabel
            gstrEpcsUrl = ProductionEpcsUrl
            gstrSharedSecret = ProductionSharedSecret
        End If
        gstrRouterName = RouterName
    End Sub


    Private Function GetgloCommunitySetting()
        ''gloCommunity.Classes.clsGeneral.WebFolder = WebFolder
        gloCommunity.Classes.clsGeneral.ClinicWebFolder = ClinicWebFolder
        gloCommunity.Classes.clsGeneral.WebSite = WebSite
        gloCommunity.Classes.clsGeneral.ClinicRepository = ClinicRepository
        gloCommunity.Classes.clsGeneral.GlobalRepository = GlobalRepository
        gloCommunity.Classes.clsGeneral.ClinicXmlFolder = ClinicXmlFolder
        gloCommunity.Classes.clsGeneral.WebGlobalXmlFolder = WebGlobalXmlFolder
        gloCommunity.Classes.clsGeneral.WebUserXmlFolder = WebUserXmlFolder
        gloCommunity.Classes.clsGeneral.gstrCommsrv = gstrCommsrv
        gloCommunity.Classes.clsGeneral.gstrSharepointSrvNm = gstrSharepointSrvNm
        gloCommunity.Classes.clsGeneral.gstrSharepointSiteNm = gstrSharepointSiteNm
        gloCommunity.Classes.clsGeneral.gstrVti_Bin = gstrVti_Bin
        gloCommunity.Classes.clsGeneral.gstrListSvc = gstrListSvc
        gloCommunity.Classes.clsGeneral.gstrSiteDataSvc = gstrSiteDataSvc
        gloCommunity.Classes.clsGeneral.gstr_Layouts = gstr_Layouts
        gloCommunity.Classes.clsGeneral.IsCommunityLiquidData = IsCommunityLiquidData
        gloCommunity.Classes.clsGeneral.IsCommunitySmartDx = IsCommunitySmartDx
        gloCommunity.Classes.clsGeneral.Webpath = Webpath
        gloCommunity.Classes.clsGeneral.gstrgloEMRStartupPath = gstrgloEMRStartupPath
        gloCommunity.Classes.clsGeneral.gstrgloTempFolder = gstrgloTempFolder
        gloCommunity.Classes.clsGeneral.gstrClinicName = gstrClinicName
        gloCommunity.Classes.clsGeneral.gstrMessageBoxCaption = gstrMessageBoxCaption
        gloCommunity.Classes.clsGeneral.gstrsmdxflnm = gstrsmdxflnm
        gloCommunity.Classes.clsGeneral.gstrLiquidDataFNm = gstrLiquidDataFNm
        gloCommunity.Classes.clsGeneral.EMRConnectionString = GetConnectionString()
        gloCommunity.Classes.clsGeneral.gstrTskMlflnm = gstrTskMlflnm
        gloCommunity.Classes.clsGeneral.gstrIMSetupflnm = gstrIMSetupflnm
        gloCommunity.Classes.clsGeneral.gstrCVSetupflnm = gstrCVSetupflnm
        gloCommunity.Classes.clsGeneral.gstrblconfflnm = gstrblconfflnm
        gloCommunity.Classes.clsGeneral.gstrappconfflnm = gstrappconfflnm
        gloCommunity.Classes.clsGeneral.gstrflowshflnm = gstrflowshflnm
        gloCommunity.Classes.clsGeneral.gstrformglry = gstrformglry
        gloCommunity.Classes.clsGeneral.gstrDmSetupflnm = gstrDmSetupflnm
        gloCommunity.Classes.clsGeneral.gblnIscommunityStaging = gblnIscommunityStaging


        'code commented by kanchan on 20111109 as per ujwala m'am to hide biztalk
        ''Code Start-Added by kanchan on 20110914 For BizTalk Database setting
        'gloCommunity.Classes.clsGeneral.BizTalkServerConnection = GetHybridConnectionString(gBizServerName, gBizDatabaseName, gBizIsSQLAUTHEN, gBizUserID, gBizPassWord)
        ''Code Start-Added by kanchan on 20110914 For BizTalk Database setting
        gloCommunity.Classes.clsGeneral.gstrSmartCPTflnm = gstrSmartCPTflnm
        gloCommunity.Classes.clsGeneral.gstrSmartOrderflnm = gstrSmartOrderflnm
        gloCommunity.Classes.clsGeneral.gnClinicID = gnClinicID
        gloCommunity.Classes.clsGeneral.gstrHistoryflnm = gstrHistoryflnm

        gstrCommunityWebUrl = gstrSharepointSrvNm + "/" + gstrSharepointSiteNm
        gloCommunity.Classes.clsGeneral.gstrCommunityWebUrl = gstrCommunityWebUrl
        If gblnIscommunityStaging = True Then
            gstrServiceNamespace = ConfigurationManager.AppSettings("ServiceNameSpace")
        Else
            gstrServiceNamespace = ConfigurationManager.AppSettings("ProductionServiceNameSpace")
        End If


        gloCommunity.Classes.clsGeneral.gstrServiceNamespace = gstrServiceNamespace

        ''gstrDomainName = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).HostName
        gloCommunity.Classes.clsGeneral.gstrDomainName = gstrDomainName

        gstrMgmtServiceReply = "https://" + gstrServiceNamespace + ".accesscontrol.windows.net:443/v2/wsfederation"
        gloCommunity.Classes.clsGeneral.gstrMgmtServiceReply = gstrMgmtServiceReply

        gstrADFSServer = "https://" + gstrDomainName + "/adfs/services/trust/13/windowstransport"
        gloCommunity.Classes.clsGeneral.gstrADFSServer = gstrADFSServer

        gstrSharePointAutthPage = gstrCommunityWebUrl + "/_layouts/Authenticate.aspx?Source=%2F" + gstrSharepointSiteNm
        gloCommunity.Classes.clsGeneral.gstrSharePointAutthPage = gstrSharePointAutthPage

        gstrSharePointSiteReply = gstrCommunityWebUrl + "_trust"
        gloCommunity.Classes.clsGeneral.gstrSharePointSiteReply = gstrSharePointSiteReply

        gstrACSRelyingPartyurl = "https://" + gstrServiceNamespace + ".accesscontrol.windows.net/FederationMetadata/2007-06/FederationMetadata.xml"
        gloCommunity.Classes.clsGeneral.gstrACSRelyingPartyurl = gstrACSRelyingPartyurl


        ''''Added for Form authentication as on 20120725
        clsGeneral.gnLoginID = gnLoginID
        gstrAuthenticationWSAddress = gstrSharepointSrvNm + "/" + gstrVti_Bin + "/Authentication.asmx"
        clsGeneral.gstrAuthenticationWSAddress = gstrAuthenticationWSAddress
        ''End


        ''Added for check which authentication is use for access gloCommunity on 20120730
        clsGeneral.gstrgloCommunityAuthentication = gstrgloCommunityAuthentication
        ''End

        If Trim(gstrSpeciality) <> "" Then
            WebFolder = gstrSpeciality
        Else
            WebFolder = "Other"
        End If
        gloCommunity.Classes.clsGeneral.WebFolder = WebFolder

        '' ''gloCommunity Security Token
        ''If ConfigurationManager.AppSettings("Environment") = "Production" Then
        ''    GetSucurityToken()
        ''End If
        ' ''end
        Return Nothing
    End Function

    'function added to get specific or individual setting value() 
    Public Function GetSetting(ByVal SettingName As String) As DataTable
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim dtTable As New DataTable
        Try
            objCon.ConnectionString = GetConnectionString()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = "SELECT ISNULL(sSettingsValue,'') AS sSettingsValue, ISNULL(nUserID,0) AS nUserID FROM Settings WHERE sSettingsName = '" & SettingName & "'  AND nClinicID = " & gnClinicID & ""
            objCmd.Connection = objCon
            objCmd.Connection = objCon
            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)
            objDA.Fill(dtTable)
            objCon.Close()

            objDA.Dispose() : objDA = Nothing

            Return dtTable

        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose() : objCon = Nothing
            End If

            If Not IsNothing(objCmd) Then
                objCmd.Parameters.Clear()
                objCmd.Dispose() : objCmd = Nothing
            End If

            If IsNothing(dtTable) Then
                dtTable = Nothing
            End If
        End Try

    End Function

    Public Function IsSettingEnabled(ByVal SettingName As String) As Boolean
        Dim objSettings As New clsSettings
        Dim dt As DataTable = Nothing
        Dim Is_SettingOn As Boolean = False
        Try

            dt = objSettings.GetSetting(SettingName)

            If IsNothing(dt) = False Then
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0)("sSettingsValue") = "TRUE" Then
                        Is_SettingOn = True
                    End If
                End If
            End If

            Return Is_SettingOn

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return Nothing
        Finally

            If (IsNothing(objSettings) = False) Then
                objSettings.Dispose() : objSettings = Nothing
            End If

            If (IsNothing(dt) = False) Then
                dt.Dispose() : dt = Nothing
            End If

        End Try

    End Function

    Public Function GetSettingValue(ByVal SettingName As String) As String
        Dim objSettings As New clsSettings
        Dim dt As DataTable = Nothing
        Dim SettingValue As String = ""

        Try
            dt = objSettings.GetSetting(SettingName)
            If IsNothing(dt) = False Then
                If dt.Rows.Count > 0 Then
                    SettingValue = dt.Rows(0)("sSettingsValue")

                End If
            End If
            Return SettingValue

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return ""
        Finally

            If (IsNothing(objSettings) = False) Then
                objSettings.Dispose() : objSettings = Nothing
            End If
            If (IsNothing(dt) = False) Then
                dt.Dispose() : dt = Nothing
            End If
        End Try
    End Function

    Public Function SetLinkSetting(ByVal LinkName As String)
        Dim conn As New SqlConnection
        Dim cmd As SqlCommand = Nothing

        Try
            Dim _strSQL As String = ""
            _strSQL = "Update Settings SET sSettingsValue  = '" & LinkName & "' WHERE UPPER(sSettingsName) = 'MEDICATIONINFO'"
            conn.ConnectionString = GetConnectionString()

            conn.Open()
            cmd = New SqlCommand(_strSQL, conn)
            _strSQL = cmd.ExecuteNonQuery
            conn.Close()

        Catch ex As Exception
        Finally
            If (IsNothing(conn) = False) Then
                conn.Dispose() : conn = Nothing
            End If

            If (IsNothing(cmd) = False) Then
                cmd.Parameters.Clear()
                cmd.Dispose() : cmd = Nothing
            End If
        End Try
        Return Nothing
    End Function

    Public Sub GetSettings_Rx()
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim dtTable As New DataTable

        Try
            objCon.ConnectionString = GetConnectionString()
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetSettings_Rx"
            objCmd.Connection = objCon

            objCon.Open()
            Dim objDA As New SqlDataAdapter(objCmd)

            objDA.Fill(dtTable)
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing

            objDA.Dispose() : objDA = Nothing

            Dim nCount As Integer
            For nCount = 0 To dtTable.Rows.Count - 1

                Select Case dtTable.Rows(nCount).Item(1).ToString.ToUpper
                    Case UCase("LOADPROBONMEDICATION")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            LoadProbOnMedication = CType(dtTable.Rows(nCount).Item(2), Boolean)
                        Else
                            LoadProbOnMedication = False
                        End If
                    Case UCase("RXDECLARATION")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            RxDeclaration = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            RxDeclaration = ""
                        End If
                    Case UCase("RXHUBDISCLAIMER")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrRxHubDisclaimer = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrRxHubDisclaimer = ""
                        End If
                        'code added by supriya 11/7/2008

                    Case UCase("PrintMultipleRx_PerScriptPage_setting")
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            gstrPrintMultipleRx_PerScriptPage = CType(dtTable.Rows(nCount).Item(2), String)
                        Else
                            gstrPrintMultipleRx_PerScriptPage = False
                        End If
                    Case "DisableAllowSubstitution".ToUpper
                        If IsDBNull(dtTable.Rows(nCount).Item(2)) = False Then
                            Try
                                gblnDisableAllowSubstitution = Nothing
                                Dim _DisableAllowSubstitution As String = CType(dtTable.Rows(nCount).Item(2), String)
                                If mdlGeneral.gstrClinicState.Trim() <> "" Then
                                    Dim sState As String = Convert.ToString(mdlGeneral.gstrClinicState).Trim
                                    Dim stateArray As String() = _DisableAllowSubstitution.Split("|")
                                    For Each _itemString As String In stateArray
                                        Dim stateDefaults As String() = _itemString.Split("~")
                                        If stateDefaults.Length > 0 Then
                                            If sState = stateDefaults(0) Then
                                                gblnDisableAllowSubstitution = Convert.ToInt16(stateDefaults(1).Trim)
                                            End If
                                        End If
                                    Next
                                End If
                            Catch ex As Exception

                            End Try
                        End If

                End Select
            Next

        Catch ex As Exception
            MessageBox.Show("Unable to get all settings. Please do the settings through gloEMR-Admin ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally
            If IsNothing(objCon) = False Then
                objCon.Dispose() : objCon = Nothing
            End If

            If IsNothing(objCmd) = False Then
                objCmd.Parameters.Clear()
                objCmd.Dispose() : objCmd = Nothing
            End If

            If IsNothing(dtTable) = False Then
                dtTable.Dispose() : dtTable = Nothing
            End If
        End Try
    End Sub

    ''Start gloCommunity
    Public Function GetSucurityToken() As Boolean
        Try
            Dim oAuth As New gloCommunity.Classes.Authentication()
            Dim SamlToken As String = oAuth.GetSamlToken()

            If Not String.IsNullOrEmpty(SamlToken) Then

                gloCommunity.Classes.clsGeneral.oCookie = New CookieContainer()
                Dim samlAuth As New Cookie("FedAuth", SamlToken)
                samlAuth.Expires = DateTime.Now.AddYears(1) ''AddHours(1)
                samlAuth.Path = "/"
                samlAuth.Secure = True
                samlAuth.HttpOnly = True
                Dim samlUri As New Uri(gstrCommunityWebUrl)
                samlAuth.Domain = samlUri.Host
                gloCommunity.Classes.clsGeneral.oCookie.Add(samlAuth)
                samlUri = Nothing
                samlAuth = Nothing
                Return True
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            Return False
        End Try
    End Function
    ''end gloCommunity

    Private disposed As Boolean = False

    Public Sub Dispose() Implements System.IDisposable.Dispose
        'Disconnect();
        ' emrAdminsettings.Dispose()
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                DisposeDeclaredObject()
            End If
        End If
        disposed = True
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Function AddSetting(ByVal Name As String, ByVal Value As String, ByVal ClinicID As Int64, ByVal UserID As Int64, ByVal UserClinicFlag As SettingFlag) As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDB.Connect(False)
            oDBParameters.Add("@sSettingsName", Name, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@sSettingsValue", Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@nClinicID", ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@nUserID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@nUserClinicFlag", UserClinicFlag.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDB.Execute("gsp_InUpSettings", oDBParameters)
            oDB.Disconnect()

            Return True
        Catch DBErr As gloDatabaseLayer.DBException
            Return False
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Setting, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Return False
        Finally

            If IsNothing(oDB) = False Then
                oDB.Dispose() : oDB = Nothing
            End If

            If IsNothing(oDBParameters) = False Then
                oDBParameters.Dispose() : oDBParameters = Nothing
            End If

        End Try
    End Function

    Function IsCDSEnabledForSelectedPatient(ByVal PatientID As Long) As Boolean

        Dim objCon As New SqlConnection
        Dim dtCDSSetting As New DataTable
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objDA As SqlDataAdapter = Nothing
        Dim objParaProvider As New SqlParameter

        Try
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetCDS_ProviderSettings"
            objCmd.Connection = objCon
            objCon.Open()
            Dim ProviderID As Int64
            Dim oclsProvider As New clsProvider
            ProviderID = oclsProvider.GetPatientProvider(PatientID)

            oclsProvider.Dispose() : oclsProvider = Nothing

            With objParaProvider
                .ParameterName = "@ProviderID"
                .Value = ProviderID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaProvider)


            objDA = New SqlDataAdapter(objCmd)
            objDA.Fill(dtCDSSetting)
            objCon.Close()

            Dim IsCDSEnabledForPatient As Boolean = False
            If (dtCDSSetting.Rows.Count > 0) Then
                IsCDSEnabledForPatient = Convert.ToBoolean(dtCDSSetting.Rows(0)("CDS_Enabled").ToString)
            End If

            Return IsCDSEnabledForPatient

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return False
        Finally
            If IsNothing(objCon) = False Then
                objCon.Dispose() : objCon = Nothing
            End If
            If IsNothing(dtCDSSetting) = False Then
                dtCDSSetting.Dispose() : dtCDSSetting = Nothing
            End If
            If IsNothing(objParaProvider) = False Then
                objParaProvider = Nothing
            End If

            If IsNothing(objCmd) = False Then
                objCmd.Parameters.Clear()
                objCmd.Dispose() : objCmd = Nothing
            End If
            If IsNothing(objDA) = False Then
                objDA.Dispose() : objDA = Nothing
            End If
        End Try
    End Function

    Function GetCDSSettingsForPatientProvider(ByVal PatientID As Long) As DataTable
        Dim objCon As New SqlConnection
        Dim dtCDSSetting As New DataTable
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objDA As SqlDataAdapter = Nothing
        Dim objParaProvider As New SqlParameter

        Try
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "gsp_GetCDS_ProviderSettings"
            objCmd.Connection = objCon
            objCon.Open()
            Dim ProviderID As Int64

            Dim oclsProvider As New clsProvider
            ProviderID = oclsProvider.GetPatientProvider(PatientID)

            oclsProvider.Dispose() : oclsProvider = Nothing

            With objParaProvider
                .ParameterName = "@ProviderID"
                .Value = ProviderID
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaProvider)

            objDA = New SqlDataAdapter(objCmd)
            objDA.Fill(dtCDSSetting)
            objCon.Close()

            Return dtCDSSetting

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            Return Nothing
        Finally
            If IsNothing(objCon) = False Then
                objCon.Dispose() : objCon = Nothing
            End If
            If IsNothing(dtCDSSetting) = False Then
                dtCDSSetting = Nothing
            End If
            If IsNothing(objParaProvider) = False Then
                objParaProvider = Nothing
            End If
            If IsNothing(objCmd) = False Then
                objCmd.Parameters.Clear()
                objCmd.Dispose() : objCmd = Nothing
            End If
            If IsNothing(objDA) = False Then
                objDA.Dispose() : objDA = Nothing
            End If
        End Try
    End Function

    Public Function GetOSName() As String
        Dim result As String = String.Empty
        Try
            Dim searcher As New ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem")
            For Each os As ManagementObject In searcher.Get()
                result = os("Caption").ToString()
                Exit For
            Next
            searcher.Dispose() : searcher = Nothing

        Catch ex As Exception
        End Try
        Return result
    End Function

    Private Sub DisposeDeclaredObject()

        m_RxDeclarationText = Nothing
        m_RxFooterNote = Nothing
        m_FAXCompression = Nothing
        m_MCIRReportPath = Nothing
        m_RxHubDisclaimer = Nothing
        m_RxHubParticipantId = Nothing
        m_RxHubPassword = Nothing
        m_RXELIGIBILITYEMR = Nothing
        m_EDISERVICEPATH = Nothing
        m_OMRCategory_History = Nothing
        m_OMRCategory_ROS = Nothing
        m_OMRCategory_PatientRegistration = Nothing
        m_DMSCategory_PatientDirective = Nothing
        m_DMSCategory_Labs = Nothing
        m_CCDFilePath = Nothing
        m_DMSCategory_Radiology = Nothing
        m_DMSCategory_VIS = Nothing
        m_DMSCategory_RxMed = Nothing
        m_ClinicDISettings = Nothing
        m_ClinicFormularySettings = Nothing
        _SignatureText = Nothing
        _CoSignatureText = Nothing
        _VitalSettingsValue = Nothing
        _OBVitalSettingsValue = Nothing
        _showDMAlert = Nothing
        _seFaxUserID = Nothing
        _seFaxUserPassword = Nothing
        _SMDBsettings = Nothing

        _tmAppointmentStartTime = Nothing
        _tmAppointmentEndTime = Nothing
        _nAppointmentInterval = Nothing
        _nPULLCHARTSInterval = Nothing
        _nNoOfAttempts = Nothing

        m_NoOfAttempts = Nothing
        _isEnableRecoverExam = Nothing

        m_HPIEnabled = Nothing
        m_getMic = Nothing
        m_AdvancedGrowthChart = Nothing
        m_LocationAddress = Nothing
        _IsAdvErxEnabled = Nothing
        _IsRxHubStagingServer = Nothing
        _blnRecordLevelLocking = Nothing
        _blnAutoPatientCode = Nothing
        _isVitalsHeightCopyForward = Nothing
        _blnInternetFax = Nothing
        _IsSurescriptEnabled = Nothing
        _IsStagingServer = Nothing

        _EMExamType = Nothing

        'If IsNothing(emrAdminsettings) = False Then
        '    emrAdminsettings.Dispose() : emrAdminsettings = Nothing
        'End If


    End Sub

    Public Sub GetICD10TransitionSettings()

        Dim objCon As New SqlConnection
        Dim dtCDSSetting As New DataTable
        Dim objCmd As New SqlCommand
        Dim objDA As SqlDataAdapter = Nothing
        Dim objParaProvider As New SqlParameter

        Try

            objCon.ConnectionString = GetConnectionString()
            objCmd.CommandType = CommandType.StoredProcedure
            objCmd.CommandText = "BL_SELECT_ICD10Transaction_DOS"
            objCmd.Connection = objCon
            objCon.Open()

            With objParaProvider
                .ParameterName = "@nContactID"
                .Value = 0
                .Direction = ParameterDirection.Input
                .SqlDbType = SqlDbType.BigInt
            End With
            objCmd.Parameters.Add(objParaProvider)

            objDA = New SqlDataAdapter(objCmd)
            objDA.Fill(dtCDSSetting)
            objCon.Close()

            If (dtCDSSetting.Rows.Count > 0) Then

                If dtCDSSetting.Rows(0)(0).ToString() <> "" Then
                    gdtIcd10Transition = Convert.ToDateTime(dtCDSSetting.Rows(0)(0).ToString())
                End If

                'Dim dtCompare As Integer = DateTime.Compare(System.DateTime.Now.Date, gdtIcd10Transition.Date)

                If gdtIcd10Transition.Date <= System.DateTime.Now.Date Then
                    'gblnIcd10Transition = True
                    gblnIcd10MasterTransition = True
                    gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnIcd10Transition = True
                Else
                    'gblnIcd10Transition = False
                    gblnIcd10MasterTransition = False
                    gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnIcd10Transition = False
                End If

                'If dtCompare < 0 Then
                '    gblnIcd10Transition = False
                '    gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnIcd10Transition = False
                'ElseIf dtCompare = 0 Or dtCompare > 0 Then
                '    gblnIcd10Transition = True
                '    gloEMRGeneralLibrary.gloGeneral.clsgeneral.gblnIcd10Transition = True
                'End If

            End If

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)

        Finally
            If IsNothing(objCon) = False Then
                objCon.Dispose() : objCon = Nothing
            End If
            If IsNothing(dtCDSSetting) = False Then
                dtCDSSetting.Dispose()
                dtCDSSetting = Nothing
            End If
            If IsNothing(objParaProvider) = False Then
                objParaProvider = Nothing
            End If
            If IsNothing(objCmd) = False Then
                objCmd.Parameters.Clear()
                objCmd.Dispose() : objCmd = Nothing
            End If
            If IsNothing(objDA) = False Then
                objDA.Dispose() : objDA = Nothing
            End If
        End Try

    End Sub

    Public Function InitializeDISettings()
        Try
            If gloRegistrySetting.IsRegistryKeyExists(gloRegistrySetting.gstrSoft & DMSReg_MainName) = False Then
                InitializeDISettings = Nothing
                Exit Function
            End If

            gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoft & DMSReg_MainName, True)

            If IsNothing(gloRegistrySetting.GetRegistryValue(DrugToDiseaseInteractionAlert)) = False Then
                gblnDrugToDiseaseInteractionAlert = gloRegistrySetting.GetRegistryValue(DrugToDiseaseInteractionAlert)
            Else
                gblnDrugToDiseaseInteractionAlert = False
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(DrugToFoodInteractionAlert)) = False Then
                gblnDrugToFoodInteractionAlert = gloRegistrySetting.GetRegistryValue(DrugToFoodInteractionAlert)
            Else
                gblnDrugToFoodInteractionAlert = False
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(DuplicateTherapyInteractionAlert)) = False Then
                gblnDuplicateTherapyInteractionAlert = gloRegistrySetting.GetRegistryValue(DuplicateTherapyInteractionAlert)
            Else
                gblnDuplicateTherapyInteractionAlert = False
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(AdverseDrugEffectAlert)) = False Then
                gblnAdverseDrugEffectAlert = gloRegistrySetting.GetRegistryValue(AdverseDrugEffectAlert)
            Else
                gblnAdverseDrugEffectAlert = False
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(ShowDrugAlertMsg)) = False Then
                gblnDrugAlertMsg = gloRegistrySetting.GetRegistryValue(ShowDrugAlertMsg)
            Else
                gblnDrugAlertMsg = False
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(ADESeverityLevel)) = False Then
                gstrADESeverityLevel = gloRegistrySetting.GetRegistryValue(ADESeverityLevel)
            Else
                gstrADESeverityLevel = "Severe"
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(ADEOnsetLevel)) = False Then
                gstrADEOnsetLevel = gloRegistrySetting.GetRegistryValue(ADEOnsetLevel)
            Else
                gstrADEOnsetLevel = "Early"
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(DISeverityLevel)) = False Then
                gstrDISeverityLevel = gloRegistrySetting.GetRegistryValue(DISeverityLevel)
            Else
                gstrDISeverityLevel = "Severe"
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(DFASeverityLevel)) = False Then
                gstrDFASeverityLevel = gloRegistrySetting.GetRegistryValue(DFASeverityLevel)
            Else
                gstrDFASeverityLevel = "Severe"
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(DIDocLevel)) = False Then
                gstrDIDocLevel = gloRegistrySetting.GetRegistryValue(DIDocLevel)
            Else
                gstrDIDocLevel = "Likely Established"
            End If

            If IsNothing(gloRegistrySetting.GetRegistryValue(DFADocLevel)) = False Then
                gstrDFADocLevel = gloRegistrySetting.GetRegistryValue(DFADocLevel)
            Else
                gstrDFADocLevel = "Likely Established"
            End If

            Return Nothing
        Catch err As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Drugs, gloAuditTrail.ActivityCategory.Drugs, gloAuditTrail.ActivityType.General, err.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            Throw New Exception("Error Initializing Drug Interaction Settings")
        Finally
            gloRegistrySetting.CloseRegistryKey()
        End Try
    End Function
End Class
