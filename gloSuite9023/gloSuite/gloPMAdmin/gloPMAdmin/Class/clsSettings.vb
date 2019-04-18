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
Public Class clsSettings

    '' To Identify whether the Setting is User or Clinic Setting 
    Public Enum enumSettingFlag
        None = 0
        Clinic = 1
        User = 2
    End Enum
    Public Enum MidLevelSettingsType
        AllProvidersAllPlans = 1
        SpecificProviderAllPlans = 2
        SpecificPlan = 3
        SpecificPlanSpecificProvider = 4
    End Enum


#Region " Private Variables"
    Dim _tmAppointmentStartTime As String
    Dim _tmAppointmentEndTime As String
    Dim _nAppointmentInterval As Int16
    Dim _nPULLCHARTSInterval As Int16
    Dim _nMaxNoOfFAXRetries As Int16
    Dim _nFAXRetryInterval As Int16
    Dim m_FAXCompression As String = ""
    Dim m_FAXSpeakerVolume As String = ""
    Dim m_FAXReceiveEnabled As Boolean
    Dim m_HPIEnabled As Boolean
    Dim m_LocationAddressed As Boolean

    '********
    Dim m_blnPwdComplexity As Boolean

    Dim m_OMRCategoryHistory As String = ""
    Dim m_OMRCategoryROS As String = ""
    Dim m_OMRCategoryPatientRegistration As String = ""
    Dim m_OMRDirectiveCategory As String = ""

    Private m_pwdstrsql As String = ""

    Dim m_NoOfAttempts As Integer = 0
    ''//Code added by Ravikiran for RxReport Path on 13/02/2007 
    Dim m_RxReportPath As String = ""

    'sarika 14th june 07 //for Drug interaction Settings
    Dim m_blnClinicDISettings As Boolean

    'clinic level formulary setting
    Dim m_blnClinicFormularySettings As Boolean

    'Pramod 16 july 07 // for Lab scan documents
    Dim m_Labs As String = ""
    '''' Pramod 16 Jan 08 // For Radiology Scan Document
    Dim m_Radiology As String = ""
    '' Mahesh 20070723 -- For Record Level Locking
    Dim _blnRecordLevelLocking As Boolean = False

    'Sagar 31 july 2007 for adding threshold value in Mx & Rx
    Dim _nThresholdValue As Double = 1

    'suraj 20080725 for rxdeclaration value
    Dim _RxDeclaration As String = ""

    'RxHub Disclaimer
    Dim _RxHUBDisclaimer As String = ""

    Dim _Generatemic As Boolean

    'var added by sarika on 11th aug 07
    Dim _sHL7SystemPath As String = ""

    'to save the CCD file path.
    Dim _sCCDfilePath As String = ""

    '*****Sandip Deshmukh 22 Nov 2007
    Dim _sHL7SendingFacility As String = ""
    Dim _sHL7ReceivingApplication As String = ""
    Dim _sHL7ReceivingFacility As String = ""
    '***** 22 Nov 2007

    'sarika 31st aug 07

    Dim m_OMRCategoryFax As String = ""
    Dim _sDBVersion As String = ""
    Dim _sAppVersion As String = ""
    '----------------------

    'sarika 5th sept 07
    Dim _nPendingFaxUserID As Int64 = 0
    Dim _nRecieveFaxUserID As Int64 = 0
    '---------------------------

    '''''''''''Anil 20071119
    Dim _blnAutoPatientCode As Boolean = False
    Dim _blnAllowEditablePatientCode As Boolean = False
    Dim _UseSitePrefix As Boolean = False
    Dim _UnclosedDayStatement As Boolean = False
    Dim _FutureCloseDateDays As Integer = 0
    'sarika 18th jan 08 for gloReporting authentication settings
    Dim _sRptUserName As String = ""
    Dim _sRptPassword As String = ""
    ''sarika 18th jan 08 for gloReporting authentication settings

    'sarika UseFaxNoPrefix  12th april 08
    Private _blnUseFaxNoPrefix As Boolean = False
    Private _sFaxNoPrefix As String = ""
    '=======0--------------sarika UseFaxNoPrefix  12th april 08

    'sarika Internet fax
    Private _blnInternetFax As Boolean = False
    'eFax login key
    Private _seFaxUserID As String = ""
    Private _seFaxUserPassword As String = ""
    'sarika internet fax

    'sarika internet fax'
    Private _seFaxDirDownload As String = ""
    'sarika internet fax'

    '' sagarK 20080802 
    Private _MCIRReportPath As String = ""

    'code by supriya 11/7/2008
    'Surescript clinic settings
    Private _IsSurescriptEnabled As Boolean
    Private _IsStagingServer As Boolean
    'code by supriya 11/7/2008

    '' Mahesh 20080809, For Advanced Growth Chart
    Private _ShowAdvancedGrowthChart As Boolean

    'by sudhir 20081111
    Private _ShowAgeInDays As Boolean
    Private _AgeLimit As Int64
    'Sudhir 20081124
    Private _AgeLimitForWeeks As Int64

    '' SUDHIR 20090721 '' GROWTH CHART PERCENTILE SETTING ''
    Private _GrowthChartPercentile As frmSettings_New.enumGrowthChartPercentile
    '' END SUDHIR ''


    'sarika 
    'DMS 20080908 -- for Loading no of recieved faxes in DMS
    Private _nLoadNoOfFaxes As Integer = 1
    '-------

    Private _SendUnassociatedDiagnosis As Boolean

    'sarika PM DB Credentials 20081128

    Private m_blnAddPatient As Boolean = False
    Private m_PMServerName As String = ""
    Private m_PMDatabaseName As String = ""
    Private m_PMSQLUserId As String = ""
    Private m_PMSQLPwd As String = ""
    Private m_blnSQLAuthentication As Boolean = False
    Private m_PMConnectionString As String = ""

    '---
    Private m_SignatureText As String = ""
    Private m_CoSignatureText As String = ""

    Private _nDMSImageDIP As Int32 = 0
    Private _bUseFileCompression As Boolean = False
    '\\ Suraj 20090123 Document Split
    Private _SplitDoc As Boolean = False
    '\\ Suraj 20090128 DMSV2 Document recover
    Private m_RecoverDMSV2 As Boolean = False
    Private m_RecoverDMSV2Path As String = ""
    '\\ Suraj 20090128 Delete DMS Document After Migration
    Private m_DeleteDMSDoc As Boolean = False

    '\\ Sandip Darade 20090210  Use Coded History
    Private _bUsecodedhistory As Boolean = False
    Private _EMChiefComplaintType As String = ""
    Private _OtherPatientType As Boolean
    '\\ Sandip Darade 20090328  Show Coded History code or desc or both 
    Private _sShowCodedHistory As String = ""
    Dim _nIM_ReminderDays As Integer = 1



    'sarika SendEMail 20090508
    Private _bIsSendEMail As Boolean = False
    Private _sSendEmailAddress As String = ""
    '---

    'sarika RxHUB Server Settings 20090602
    Private _bIsAdvanceRxEnabled As Boolean
    Private _bIsAdvanceRxStagingServer As Boolean
    Private _sEARDirectory As String = ""
    '--

    '20090819 RxHUB partipantID & password setting
    Private _participantid As String = ""
    Private _rxPassword As String = ""

    'sarika SiteID setting 20090607
    Private _sSiteID As String = ""
    '--
    '\\ Sandip Darade 20090622
    Dim _sDefaultPatientGender As String = " "

    '\\ Sandip Darade 20090709
    ' DefaultFeeSpeciality  DefaultCarrierNumber  DefaultLocality  
    ' CLINICFEESCHEDULE NoOfClaimPerBatch NoOfModifiers ShowLabCol DEFAULTFEECHARGES
    Private _DefaultFeeSpeciality As String = ""
    Private _DefaultSelfPayAllowed As String = ""
    Private _DefaultSelfPayFeeSchedule As String = ""
    Private _ClinicFeeSchedule As String = ""
    Private _NoOfClaimPerBatch As String = ""
    Private _NoOfModifiers As String = "4"
    Private _ShowLabCol As String = ""
    Private _Defaultfeecharges As String = ""

    Private _SetCPTtoAllICD9 As Boolean
    'Mayuri
    Private _ICD9Driven As Boolean = True
    Private _Show8ICD9 As Boolean = True
    Private _Show4Modifier As Boolean = True

    Private _PrescriptionProviderAssociation As Boolean = False
    Private _PatientQuestionnaire As Boolean = False
    ''Sandip Darade 20090830
    Private m_GeniusPath As String = ""
    Private m_GeniusCode As String = ""

    ''MaheshB
    Private _PatientAccountCode As String = ""

    Private _sWriteOff As String
    Private _sCopay As String
    Private _sDeductible As String
    Private _sCoInsurance As String
    Private _sWithhold As String

    ''MaheshB
    Private _IncludeFacilitieswithPOS11onClaim As String = ""
    ''Vijay P 20100813 
    Private _UB04_EnableBilling As String = ""
    Private _UB04_RevenueCode As String = ""
    Private _UB04_TypeOfBilling As String = ""
    Private _UB04_AdmisionType As String = ""
    Private _UB04_admisionSource As String = ""
    Private _UB04_DischargeStatus As String = ""

    Private _UB04_Font As String = ""
    Private _UB04_FontSize As String = ""
    Private _UB04_Capitalizedata As Boolean = False

    Private _CMS1500_Font As String = ""
    Private _CMS1500_FontSize As String = ""
    Private _CMS1500_Capitalize0212data As Boolean = False



    'Mahesh Nawal   20100407
    Private _CopyClaimtoServer As String = ""

    Private _IsMultipleClearingHouse As Boolean = False
    Private _blnUseNPOIForExcelIntegration As Boolean = False

    'Added by Subashish on 05-01-2011 for add 2 new property variable to hold the radio buttons value for  Patient Account Feature and Allow Multiple Guranter 
    Private _IsPatientAccountFeatureEnabled As Boolean
    Private _IsAllowMultipleGuranterEnabled As Boolean
    Private _IsAllowCopyAccountEnabled As Boolean
    'Added By Mahesh S(Apollo) On 1-Feb-2011 for MergeAccount
    Private _IsAllowMergeAccountEnabled As Boolean

    '//End 

    Private _StatementMinPay As String = ""

    Private _ExplicitlyAcceptTask As Integer = 0
    ''7022Items: Home Billing- Added to save setting for USEAREACODEFORPATIENT in database
    ''Save false if No is selected and true if Yes is selected
    Private _UseAreaCodeForPatient As Boolean = False

    Private odsAdminSettingsTVP As New dsAdminSettingsTVP

    ''Added Services settings by Ujwala on 23022015 to get ServicesDB Name from settings table
    Private m_ServicesServerName As String = vbNullString
    Private m_ServicesDatabaseName As String = vbNullString
    Private m_ServicesUserID As String = vbNullString
    Private m_ServicesPassword As String = vbNullString
    Private m_ServicesAuthentication As Boolean = False
    ''Added Services settings by Ujwala on 23022015 to get ServicesDB Name from settings table


#End Region

#Region " Public Properties"

    Public Property AppointmentStartTime() As String
        Get
            Return _tmAppointmentStartTime
        End Get
        Set(ByVal Value As String)
            _tmAppointmentStartTime = Value
        End Set
    End Property
    Public Property AppointmentEndTime() As String
        Get
            Return _tmAppointmentEndTime
        End Get
        Set(ByVal Value As String)
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
    Public Property MaxNoOfFAXRetries() As Int16
        Get
            Return _nMaxNoOfFAXRetries
        End Get
        Set(ByVal Value As Int16)
            _nMaxNoOfFAXRetries = Value
        End Set
    End Property
    Public Property FAXRetryInterval() As Int16
        Get
            Return _nFAXRetryInterval
        End Get
        Set(ByVal Value As Int16)
            _nFAXRetryInterval = Value
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
    Public Property LocationAddressed() As Boolean
        Get
            Return m_LocationAddressed
        End Get
        Set(ByVal Value As Boolean)
            m_LocationAddressed = Value
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
    Public Property FAXSpeakerVoulme() As String
        Get
            If Trim(m_FAXSpeakerVolume) <> "" Then
                Return m_FAXSpeakerVolume
            Else
                Return "No Volume"
            End If

        End Get
        Set(ByVal Value As String)
            m_FAXSpeakerVolume = Value
        End Set
    End Property
    Public Property FAXReceiveEnabled() As Boolean
        Get
            Return m_FAXReceiveEnabled
        End Get
        Set(ByVal Value As Boolean)
            m_FAXReceiveEnabled = Value
        End Set
    End Property
    Public Property OMRCategoryHistory() As String
        Get
            Return m_OMRCategoryHistory
        End Get
        Set(ByVal Value As String)
            m_OMRCategoryHistory = Value
        End Set
    End Property
    Public Property OMRCategoryROS() As String
        Get
            Return m_OMRCategoryROS
        End Get
        Set(ByVal Value As String)
            m_OMRCategoryROS = Value
        End Set
    End Property
    Public Property OMRCategoryPatientRegistration() As String
        Get
            Return m_OMRCategoryPatientRegistration
        End Get
        Set(ByVal Value As String)
            m_OMRCategoryPatientRegistration = Value
        End Set
    End Property

    Public Property OMRCategoryDirective() As String
        Get
            Return m_OMRDirectiveCategory
        End Get
        Set(ByVal Value As String)
            m_OMRDirectiveCategory = Value
        End Set
    End Property

    Public Property blnPwdComplexity() As Boolean
        Get
            Return m_blnPwdComplexity
        End Get
        Set(ByVal Value As Boolean)
            m_blnPwdComplexity = Value
        End Set
    End Property

    Public Property PwdStrSQL() As String
        Get
            Return m_pwdstrsql
        End Get
        Set(ByVal Value As String)
            m_pwdstrsql = Value
        End Set
    End Property

    'sarika 12th feb

    Public Property NoOfAttempts() As Integer
        Get
            Return m_NoOfAttempts
        End Get
        Set(ByVal Value As Integer)
            m_NoOfAttempts = Value
        End Set
    End Property

    'sarika 14th june 07
    Public Property ClinicDISettings() As Boolean
        Get
            Return m_blnClinicDISettings
        End Get
        Set(ByVal Value As Boolean)
            m_blnClinicDISettings = Value
        End Set
    End Property

    'clinic level formulary setting
    Public Property ClinicFormularySettings() As Boolean
        Get
            Return m_blnClinicFormularySettings
        End Get
        Set(ByVal Value As Boolean)
            m_blnClinicFormularySettings = Value
        End Set
    End Property

    'Pramod 16 July 07
    Public Property Labs() As String
        Get
            Return m_Labs
        End Get
        Set(ByVal value As String)
            m_Labs = value
        End Set
    End Property

    'Pramod 16 Jan 08
    Public Property Radiology() As String
        Get
            Return m_Radiology
        End Get
        Set(ByVal value As String)
            m_Radiology = value
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

    'Sagar 31 july 2007 for adding threshold value in Mx & Rx
    Public Property ThresholdValue() As Double
        Get
            Return _nThresholdValue
        End Get
        Set(ByVal Value As Double)
            _nThresholdValue = Value
        End Set
    End Property
    'suraj 20080725
    Public Property RxDeclaration() As String
        Get
            Return _RxDeclaration
        End Get
        Set(ByVal value As String)
            _RxDeclaration = value
        End Set
    End Property
    'suraj 200800801

    'RxHub Disclaimer
    Public Property RxHUBDisclaimer() As String
        Get
            Return _RxHUBDisclaimer
        End Get
        Set(ByVal value As String)
            _RxHUBDisclaimer = value
        End Set
    End Property
    'RxHub Disclaimer

    Public Property SendUnassociatedDiagnosis() As Boolean
        Get
            Return _SendUnassociatedDiagnosis
        End Get
        Set(ByVal value As Boolean)
            _SendUnassociatedDiagnosis = value
        End Set
    End Property

    Public Property GenerateMic() As String
        Get
            Return _Generatemic
        End Get
        Set(ByVal value As String)
            _Generatemic = value
        End Set
    End Property
    'sarika 11th aug 07
    Public Property HL7SystemPath() As String
        Get
            Return _sHL7SystemPath
        End Get
        Set(ByVal value As String)
            _sHL7SystemPath = value
        End Set
    End Property

    'to save the CCD files
    Public Property CCDfilePath() As String
        Get
            Return _sCCDfilePath
        End Get
        Set(ByVal value As String)
            _sCCDfilePath = value
        End Set
    End Property

    '******Sandip Deshmukh 22 Nov 2007
    Public Property HL7SendingFacility() As String
        Get
            Return _sHL7SendingFacility
        End Get
        Set(ByVal value As String)
            _sHL7SendingFacility = value
        End Set
    End Property
    Public Property HL7ReceivingApplication() As String
        Get
            Return _sHL7ReceivingApplication
        End Get
        Set(ByVal value As String)
            _sHL7ReceivingApplication = value
        End Set
    End Property
    Public Property HL7ReceivingFacility() As String
        Get
            Return _sHL7ReceivingFacility
        End Get
        Set(ByVal value As String)
            _sHL7ReceivingFacility = value
        End Set
    End Property

    '****** 22 Nov 2007
    Public Property OMRCategoryFax() As String
        Get
            Return m_OMRCategoryFax
        End Get
        Set(ByVal value As String)
            m_OMRCategoryFax = value
        End Set
    End Property

    Public Property DBVersion() As String
        Get
            Return _sDBVersion
        End Get
        Set(ByVal value As String)
            _sDBVersion = value
        End Set
    End Property
    Public Property AppVersion() As String
        Get
            Return _sAppVersion
        End Get
        Set(ByVal value As String)
            _sAppVersion = value
        End Set
    End Property

    '----------------------------

    'sarika 5th sept 07
    Public Property PendingFaxUserID() As Int64
        Get
            Return _nPendingFaxUserID
        End Get
        Set(ByVal value As Int64)
            _nPendingFaxUserID = value
        End Set
    End Property
    Public Property RecieveFaxUserID() As Int64
        Get
            Return _nRecieveFaxUserID
        End Get
        Set(ByVal value As Int64)
            _nRecieveFaxUserID = value
        End Set
    End Property
    '----------------------------------
    ''''''''''''''''''''''Property Added by Anil 0n 20071119
    Public Property AutoGeneratePatientCode() As Boolean
        Get
            Return _blnAutoPatientCode
        End Get
        Set(ByVal value As Boolean)
            _blnAutoPatientCode = value
        End Set
    End Property
    ''Added by Mayuri:20101004-To add functonality of save as copy patient
    Public Property AllowEditablePatientCode() As Boolean
        Get
            Return _blnAllowEditablePatientCode
        End Get
        Set(ByVal value As Boolean)
            _blnAllowEditablePatientCode = value
        End Set
    End Property
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    'sarika 18th jan 08 for gloReporting authentication settings
    Public Property ReportingUserName() As String
        Get
            Return _sRptUserName
        End Get
        Set(ByVal value As String)
            _sRptUserName = value
        End Set
    End Property

    Public Property ReportingPassword() As String
        Get
            Return _sRptPassword
        End Get
        Set(ByVal value As String)
            _sRptPassword = value
        End Set
    End Property

    ''sarika 18th jan 08 for gloReporting authentication settings

    'sarika UseFaxNoPrefix  12th april 08
    Public Property UseFaxNoPrefix() As Boolean
        Get
            Return _blnUseFaxNoPrefix
        End Get
        Set(ByVal value As Boolean)
            _blnUseFaxNoPrefix = value
        End Set
    End Property

    Public Property FaxNoPrefix() As String
        Get
            Return _sFaxNoPrefix
        End Get
        Set(ByVal value As String)
            _sFaxNoPrefix = value
        End Set
    End Property

    '=======0--------------sarika UseFaxNoPrefix  12th april 08
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
    'sarika internet fax


    'sarika internet fax'
    Public Property eFaxDirDownload() As String
        Get
            Return _seFaxDirDownload
        End Get
        Set(ByVal value As String)
            _seFaxDirDownload = value
        End Set
    End Property
    'sarika internet fax'

    '' SagarK 20080802 
    Public Property MCIRReportPath() As String
        Get
            Return _MCIRReportPath
        End Get
        Set(ByVal value As String)
            _MCIRReportPath = value
        End Set
    End Property
    ''

    'code added by supriya 11/7/2008
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
    'code added by supriya 11/7/2008

    Public Property ShowAdvancedGrowthChart() As Boolean
        Get
            Return _ShowAdvancedGrowthChart
        End Get
        Set(ByVal value As Boolean)
            _ShowAdvancedGrowthChart = value
        End Set
    End Property

    'by sudhir 20081111
    Public Property ShowAgeInDays() As Boolean
        Get
            Return _ShowAgeInDays
        End Get
        Set(ByVal value As Boolean)
            _ShowAgeInDays = value
        End Set
    End Property

    Public Property AgeLimit() As Int64
        Get
            Return _AgeLimit
        End Get
        Set(ByVal value As Int64)
            _AgeLimit = value
        End Set
    End Property
    'sudhir 20081124
    Public Property AgeLimitForWeeks() As Int64
        Get
            Return _AgeLimitForWeeks
        End Get
        Set(ByVal value As Int64)
            _AgeLimitForWeeks = value
        End Set
    End Property

    Public Property GrowthChartPercentile() As frmSettings_New.enumGrowthChartPercentile
        Get
            Return _GrowthChartPercentile
        End Get
        Set(ByVal value As frmSettings_New.enumGrowthChartPercentile)
            _GrowthChartPercentile = value
        End Set
    End Property

    'sarika 
    'DMS 20080908 -- for Loading no of recieved faxes in DMS
    Public Property LoadNoOfFaxes() As Integer
        Get
            Return _nLoadNoOfFaxes
        End Get
        Set(ByVal value As Integer)
            _nLoadNoOfFaxes = value
        End Set
    End Property
    '----------


    'sarika PM DB Credentials 20081128
    Public Property PMServerName() As String
        Get
            Return m_PMServerName
        End Get
        Set(ByVal value As String)
            m_PMServerName = value
        End Set
    End Property

    Public Property PMDatabaseName() As String
        Get
            Return m_PMDatabaseName
        End Get
        Set(ByVal value As String)
            m_PMDatabaseName = value
        End Set
    End Property

    Public Property PMUserID() As String
        Get
            Return m_PMSQLUserId
        End Get
        Set(ByVal value As String)
            m_PMSQLUserId = value
        End Set
    End Property
    Public Property PMSQLPwd() As String
        Get
            Return m_PMSQLPwd
        End Get
        Set(ByVal value As String)
            m_PMSQLPwd = value
        End Set
    End Property

    Public Property PMSQLAuthentication() As Boolean
        Get
            Return m_blnSQLAuthentication
        End Get
        Set(ByVal value As Boolean)
            m_blnSQLAuthentication = value
        End Set
    End Property

    Public Property PMConnectionString() As String
        Get
            Return m_PMConnectionString
        End Get
        Set(ByVal value As String)
            m_PMConnectionString = value
        End Set
    End Property

    Public Property PMAddPatient() As Boolean
        Get
            Return m_blnAddPatient
        End Get
        Set(ByVal value As Boolean)
            m_blnAddPatient = value
        End Set
    End Property
    Public Property SignatureText() As String
        Get
            Return m_SignatureText
        End Get
        Set(ByVal value As String)
            m_SignatureText = value
        End Set
    End Property
    Public Property CoSignatureText() As String
        Get
            Return m_CoSignatureText
        End Get
        Set(ByVal value As String)
            m_CoSignatureText = value
        End Set
    End Property
    '----

    Public Property DMSImageDIP() As Int32
        Get
            Return _nDMSImageDIP
        End Get
        Set(ByVal value As Int32)
            _nDMSImageDIP = value
        End Set
    End Property

    Public Property UseFileCompression() As Boolean
        Get
            Return _bUseFileCompression
        End Get
        Set(ByVal value As Boolean)
            _bUseFileCompression = value
        End Set
    End Property
    '\\Suraj 20090123
    Public Property SplitDocument() As Boolean
        Get
            Return _SplitDoc
        End Get
        Set(ByVal value As Boolean)
            _SplitDoc = value
        End Set
    End Property

    '\\Suraj 20090128 RecoveryDMS Check
    Public Property RecoveryDMSV2Doc() As Boolean
        Get
            Return m_RecoverDMSV2
        End Get
        Set(ByVal value As Boolean)
            m_RecoverDMSV2 = value
        End Set
    End Property
    Public Property RecoveryDMSV2Path() As String
        Get
            Return m_RecoverDMSV2Path
        End Get
        Set(ByVal value As String)
            m_RecoverDMSV2Path = value
        End Set
    End Property
    '\\suraj 20090128
    Public Property DeleteDMSDocAfterMigration() As Boolean
        Get
            Return m_DeleteDMSDoc
        End Get
        Set(ByVal value As Boolean)
            m_DeleteDMSDoc = value
        End Set
    End Property
    'Sandip Darade 200090210
    Public Property Usecodedhistory() As Boolean
        Get
            Return _bUsecodedhistory
        End Get
        Set(ByVal value As Boolean)
            _bUsecodedhistory = value
        End Set
    End Property

    Public Property EMChiefComplaintType() As String
        Get
            Return _EMChiefComplaintType
        End Get
        Set(ByVal value As String)
            _EMChiefComplaintType = value
        End Set
    End Property

    'Shubhangi 20090306'
    Public Property OtherPatientType() As Boolean
        Get
            Return _OtherPatientType
        End Get
        Set(ByVal value As Boolean)
            _OtherPatientType = value
        End Set
    End Property
    ' Sandip Darade 20090328 
    '' Show Coded History code or desc or both 
    Public Property ShowCodedHistory() As String
        Get
            Return _sShowCodedHistory
        End Get
        Set(ByVal value As String)
            _sShowCodedHistory = value
        End Set
    End Property

    Public Property IM_ReminderDays() As Integer
        Get
            Return _nIM_ReminderDays
        End Get
        Set(ByVal value As Integer)
            _nIM_ReminderDays = value
        End Set
    End Property

    'sarika SendEMail 20090502

    Public Property IsSendEMail() As Boolean
        Get
            Return _bIsSendEMail
        End Get
        Set(ByVal value As Boolean)
            _bIsSendEMail = value
        End Set
    End Property

    Public Property SendEmailAddress() As String
        Get
            Return _sSendEmailAddress
        End Get
        Set(ByVal value As String)
            _sSendEmailAddress = value
        End Set
    End Property


    'sarika RxHUB Server Settings 20090602
    Public Property IsAdvanceRxEnabled() As Boolean
        Get
            Return _bIsAdvanceRxEnabled
        End Get
        Set(ByVal value As Boolean)
            _bIsAdvanceRxEnabled = value
        End Set
    End Property
    Public Property IsAdvanceRxStagingServer() As Boolean
        Get
            Return _bIsAdvanceRxStagingServer
        End Get
        Set(ByVal value As Boolean)
            _bIsAdvanceRxStagingServer = value
        End Set
    End Property
    Public Property EARDirectory() As String
        Get
            Return _sEARDirectory
        End Get
        Set(ByVal value As String)
            _sEARDirectory = value
        End Set
    End Property
    '--------------
    '20090819 RxHUB participantID 
    Public Property ParticipantID() As String
        Get
            Return _participantid
        End Get
        Set(ByVal value As String)
            _participantid = value
        End Set
    End Property

    '20090819 RxHUB rxPassword 
    Public Property RxPassword() As String
        Get
            Return _rxPassword
        End Get
        Set(ByVal value As String)
            _rxPassword = value
        End Set
    End Property

    'sarika SiteID setting 20090607
    Public Property SiteID() As String
        Get
            Return _sSiteID
        End Get
        Set(ByVal value As String)
            _sSiteID = value
        End Set
    End Property
    '---
    ' Sandip Darade 20090622
    '' 
    Public Property DefaultPatientGender() As String
        Get
            Return _sDefaultPatientGender
        End Get
        Set(ByVal value As String)
            _sDefaultPatientGender = value
        End Set
    End Property
    'Sandip Darade 20090709
    Public Property DefaultFeeSpeciality() As String
        Get
            Return _DefaultFeeSpeciality
        End Get
        Set(ByVal Value As String)
            _DefaultFeeSpeciality = Value
        End Set
    End Property
    Public Property DefaultSelfPayAllowed() As String
        Get
            Return _DefaultSelfPayAllowed
        End Get
        Set(ByVal Value As String)
            _DefaultSelfPayAllowed = Value
        End Set
    End Property
    Public Property DefaultSelfPayFeeSchedule() As String
        Get
            Return _DefaultSelfPayFeeSchedule
        End Get
        Set(ByVal Value As String)
            _DefaultSelfPayFeeSchedule = Value
        End Set
    End Property
    Public Property ClinicFeeSchedule() As String
        Get
            Return _ClinicFeeSchedule
        End Get
        Set(ByVal Value As String)
            _ClinicFeeSchedule = Value
        End Set
    End Property
    Public Property NoOfClaimPerBatch() As String
        Get
            Return _NoOfClaimPerBatch
        End Get
        Set(ByVal Value As String)
            _NoOfClaimPerBatch = Value
        End Set
    End Property
    Public Property NoOfModifiers() As String
        Get
            Return _NoOfModifiers
        End Get
        Set(ByVal Value As String)
            _NoOfModifiers = Value
        End Set
    End Property
    Public Property ShowLabCol() As String
        Get
            Return _ShowLabCol
        End Get
        Set(ByVal Value As String)
            _ShowLabCol = Value
        End Set
    End Property
    Public Property Defaultfeecharges() As String
        Get
            Return _Defaultfeecharges
        End Get
        Set(ByVal Value As String)
            _Defaultfeecharges = Value
        End Set
    End Property

    Public Property SetCPTtoAllICD9() As Boolean
        Get
            Return _SetCPTtoAllICD9
        End Get
        Set(ByVal value As Boolean)
            _SetCPTtoAllICD9 = value

        End Set
    End Property

    Public Property ICD9Driven() As Boolean
        Get
            Return _ICD9Driven
        End Get
        Set(ByVal value As Boolean)
            _ICD9Driven = value
        End Set
    End Property

    Public Property Show8ICD9() As Boolean
        Get
            Return _Show8ICD9

        End Get
        Set(ByVal value As Boolean)
            _Show8ICD9 = value

        End Set
    End Property
    Public Property Show4Modifier() As Boolean
        Get
            Return _Show4Modifier

        End Get
        Set(ByVal value As Boolean)
            _Show4Modifier = value

        End Set
    End Property
    Public Property PrescriptionProviderAssociation() As Boolean
        Get
            Return _PrescriptionProviderAssociation

        End Get
        Set(ByVal value As Boolean)
            _PrescriptionProviderAssociation = value

        End Set
    End Property
    Public Property PatientQuestionnaire() As Boolean
        Get
            Return _PatientQuestionnaire

        End Get
        Set(ByVal value As Boolean)
            _PatientQuestionnaire = value

        End Set
    End Property

    ''Sandip Darade 20090830

    Public Property GeniusPath() As String
        Get
            Return m_GeniusPath
        End Get
        Set(ByVal value As String)
            m_GeniusPath = value
        End Set
    End Property

    '\\Suraj 20090207   Code
    Public Property GeniusCode() As String
        Get
            Return m_GeniusCode
        End Get
        Set(ByVal value As String)
            m_GeniusCode = value
        End Set
    End Property

    ''MaheshB
    Public Property PatientAccountCode() As String
        Get
            Return _PatientAccountCode
        End Get
        Set(ByVal value As String)
            _PatientAccountCode = value
        End Set
    End Property

    ''MaheshB 20091127
    ''Added by vijay patil Property for UB04 Billing
    Public Property UB04_EnableBilling() As String
        Get
            Return _UB04_EnableBilling
        End Get
        Set(ByVal value As String)
            _UB04_EnableBilling = value
        End Set
    End Property
    Public Property UB04_RevenueCode() As String
        Get
            Return _UB04_RevenueCode
        End Get
        Set(ByVal value As String)
            _UB04_RevenueCode = value
        End Set
    End Property
    Public Property UB04_TypeOfBilling() As String
        Get
            Return _UB04_TypeOfBilling
        End Get
        Set(ByVal value As String)
            _UB04_TypeOfBilling = value
        End Set
    End Property

    Public Property UB04_AdmisionType() As String
        Get
            Return _UB04_AdmisionType
        End Get
        Set(ByVal value As String)
            _UB04_AdmisionType = value
        End Set
    End Property
    Public Property UB04_AdmisionSource() As String
        Get
            Return _UB04_admisionSource
        End Get
        Set(ByVal value As String)
            _UB04_admisionSource = value
        End Set
    End Property
    Public Property UB04_DischargeStatus() As String
        Get
            Return _UB04_DischargeStatus
        End Get
        Set(ByVal value As String)
            _UB04_DischargeStatus = value
        End Set
    End Property

    'End Code vijay


    Public Property UB04_Font() As String
        Get
            Return _UB04_Font
        End Get
        Set(ByVal value As String)
            _UB04_Font = value
        End Set
    End Property

    Public Property UB04_FontSize() As String
        Get
            Return _UB04_FontSize
        End Get
        Set(ByVal value As String)
            _UB04_FontSize = value
        End Set
    End Property

    Public Property UB04_Capitalizedata() As Boolean
        Get
            Return _UB04_Capitalizedata
        End Get
        Set(ByVal value As Boolean)
            _UB04_Capitalizedata = value
        End Set
    End Property


    Public Property CMS1500_Font() As String
        Get
            Return _CMS1500_Font
        End Get
        Set(ByVal value As String)
            _CMS1500_Font = value
        End Set
    End Property

    Public Property CMS1500_FontSize() As String
        Get
            Return _CMS1500_FontSize
        End Get
        Set(ByVal value As String)
            _CMS1500_FontSize = value
        End Set
    End Property

    Public Property CMS1500_Capitalize0212data() As Boolean
        Get
            Return _CMS1500_Capitalize0212data
        End Get
        Set(ByVal value As Boolean)
            _CMS1500_Capitalize0212data = value
        End Set
    End Property




    Public Property IncludeFacilitieswithPOS11onClaim() As String
        Get
            Return _IncludeFacilitieswithPOS11onClaim
        End Get
        Set(ByVal value As String)
            _IncludeFacilitieswithPOS11onClaim = value
        End Set
    End Property

    'Mahesh Nawal   20100407
    Public Property CopyClaimtoServer() As String
        Get
            Return _CopyClaimtoServer
        End Get
        Set(ByVal value As String)
            _CopyClaimtoServer = value
        End Set
    End Property

    Public Property WriteOff() As String
        Get
            If Not _sWriteOff Is Nothing Then
                Return _sWriteOff
            Else
                Return "NULL"
            End If

        End Get
        Set(ByVal value As String)
            _sWriteOff = value
        End Set
    End Property
    Public Property Copay() As String
        Get
            If Not _sCopay Is Nothing Then
                Return _sCopay
            Else
                Return "NULL"
            End If
        End Get
        Set(ByVal value As String)
            _sCopay = value
        End Set
    End Property
    Public Property Deductible() As String
        Get
            If Not _sDeductible Is Nothing Then
                Return _sDeductible
            Else
                Return "NULL"
            End If
        End Get
        Set(ByVal value As String)
            _sDeductible = value
        End Set
    End Property
    Public Property CoInsurance() As String
        Get
            If Not _sCoInsurance Is Nothing Then
                Return _sCoInsurance
            Else
                Return "NULL"
            End If

        End Get
        Set(ByVal value As String)
            _sCoInsurance = value
        End Set
    End Property
    Public Property Withhold() As String
        Get
            If Not _sWithhold Is Nothing Then
                Return _sWithhold
            Else
                Return "NULL"
            End If
        End Get
        Set(ByVal value As String)
            _sWithhold = value
        End Set
    End Property

    ''code added by pradeep for User Site Prefix
    Public Property UseSitePrefix() As Boolean
        Get
            Return _UseSitePrefix
        End Get
        Set(ByVal value As Boolean)
            _UseSitePrefix = value
        End Set
    End Property
    Public Property GenerateUnclosedDayStatement() As Boolean
        Get
            Return _UnclosedDayStatement
        End Get
        Set(ByVal value As Boolean)
            _UnclosedDayStatement = value
        End Set
    End Property

    Public Property MultipleClearingHouse() As Boolean
        Get
            Return _IsMultipleClearingHouse
        End Get
        Set(ByVal value As Boolean)
            _IsMultipleClearingHouse = value
        End Set
    End Property



    Public Property UseNPOIForExcelIntegration() As Boolean
        Get
            Return _blnUseNPOIForExcelIntegration
        End Get
        Set(ByVal value As Boolean)
            _blnUseNPOIForExcelIntegration = value
        End Set
    End Property

    'Added by Subashish date 05-01-2011 for add 2 new property  to set the radio buttons value for  Patient Account Feature and Allow Multiple Guranter 

    Public Property IsPatientAccountFeatureEnabled() As Boolean
        Get
            Return _IsPatientAccountFeatureEnabled
        End Get
        Set(ByVal value As Boolean)
            _IsPatientAccountFeatureEnabled = value

        End Set
    End Property

    Public Property IsAllowMultipleGuranterEnabled() As Boolean
        Get
            Return _IsAllowMultipleGuranterEnabled
        End Get
        Set(ByVal value As Boolean)
            _IsAllowMultipleGuranterEnabled = value

        End Set
    End Property

    Public Property IsAllowCopyAccountEnabled() As Boolean
        Get
            Return _IsAllowCopyAccountEnabled
        End Get
        Set(ByVal value As Boolean)
            _IsAllowCopyAccountEnabled = value

        End Set
    End Property
    'End 

    'Added By Mahesh S(Apollo) on 1-Feb-2011 for MergeAccount 
    Public Property IsAllowMergeAccountEnabled() As Boolean
        Get
            Return _IsAllowMergeAccountEnabled
        End Get
        Set(ByVal value As Boolean)
            _IsAllowMergeAccountEnabled = value

        End Set
    End Property
    'End

    Public Property StatementMinPay() As String
        Get
            Return _StatementMinPay
        End Get
        Set(ByVal value As String)
            _StatementMinPay = value
        End Set
    End Property

    Public Property FutureCloseDateDays() As Integer
        Get
            Return _FutureCloseDateDays
        End Get
        Set(ByVal value As Integer)
            _FutureCloseDateDays = value
        End Set
    End Property

    Public Property ExplicitlyAcceptTask() As Integer
        Get
            Return _ExplicitlyAcceptTask
        End Get
        Set(ByVal value As Integer)
            _ExplicitlyAcceptTask = value
        End Set
    End Property
    ''7022Items: Home Billing- Added to save setting for USEAREACODEFORPATIENT in database
    ''Save false if No is selected and true if Yes is selected
    Public Property UseAreaCodeForPatient() As Boolean
        Get
            Return _UseAreaCodeForPatient
        End Get
        Set(ByVal value As Boolean)
            _UseAreaCodeForPatient = value
        End Set
    End Property

    Public Property ServicesDatabaseName() As String
        ''Added ServicesDatabaseName by Ujwala on 23022015 to get ServicesDB Name from settings table
        Get
            Return m_ServicesDatabaseName
        End Get
        Set(ByVal value As String)
            m_ServicesDatabaseName = value
        End Set
        ''Added ServicesDatabaseName by Ujwala on 23022015 to get ServicesDB Name from settings table
    End Property

    Public Property ServicesServerName() As String
        ''Added SERVICESSERVERNAME by Ujwala on 20022015 to get ServicesDB Name from settings table
        Get
            Return m_ServicesServerName
        End Get
        Set(ByVal value As String)
            m_ServicesServerName = value
        End Set
        ''Added SERVICESSERVERNAME by Ujwala on 20022015 to get ServicesDB Name from settings table
    End Property
    Public Property ServicesUserID() As String
        ''Added SERVICESUSERID by Ujwala on 20022015 to get ServicesDB Name from settings table
        Get
            Return m_ServicesUserID
        End Get
        Set(ByVal value As String)
            m_ServicesUserID = value
        End Set
        ''Added SERVICESUSERID by Ujwala on 20022015 to get ServicesDB Name from settings table
    End Property
    Public Property ServicesPassword() As String
        ''Added SERVICESPASSWORD by Ujwala on 20022015 to get ServicesDB Name from settings table
        Get
            Return m_ServicesPassword
        End Get
        Set(ByVal value As String)
            m_ServicesPassword = value
        End Set
        ''Added SERVICESPASSWORD by Ujwala on 20022015 to get ServicesDB Name from settings table
    End Property
    Public Property ServicesAuthentication() As Boolean
        ''Added SERVICESAUTHEN by Ujwala on 20022015 to get ServicesDB Name from settings table
        Get
            Return m_ServicesAuthentication
        End Get
        Set(ByVal value As Boolean)
            m_ServicesAuthentication = value
        End Set
        ''Added SERVICESAUTHEN by Ujwala on 20022015 to get ServicesDB Name from settings table
    End Property

    Public Property Country() As String
    Public Property SameDayApptType() As String
    Public Property FutureApptType() As String
    'added on 07-dec-2012 for setting global hl7 outbound setting

    Public Property globlnhl7OutBound() As Boolean
    Public Property globlnhl7Sendpatientdet() As Boolean
    Public Property globlnhl7Sendapptdet() As Boolean
    Public Property EnablePatientAppointmentsLinkingToCharges As Boolean
    'added on 07-dec-2012 for setting global hl7 outbound setting
    Public Property gloAusPortalUrl() As String
    Public Property DemoNPIs() As String

    Private _EnableCMSFontSizeSelection As Boolean
    Public Property EnableCMSFontSizeSelection() As Boolean
        Get
            Return _EnableCMSFontSizeSelection
        End Get
        Set(ByVal value As Boolean)
            _EnableCMSFontSizeSelection = value
        End Set
    End Property

    Private _EnableUBFontSizeSelection As Boolean
    Public Property EnableUBFontSizeSelection() As Boolean
        Get
            Return _EnableUBFontSizeSelection
        End Get
        Set(ByVal value As Boolean)
            _EnableUBFontSizeSelection = value
        End Set
    End Property

    Private _OCPRCMDefaultCategory As String
    Public Property OCPRCMDefaultCategory() As String
        Get
            Return _OCPRCMDefaultCategory
        End Get
        Set(ByVal value As String)
            _OCPRCMDefaultCategory = value
        End Set
    End Property

    Private _OCPPortalEnable As Boolean
    Public Property OCPPortalEnable() As Boolean
        Get
            Return _OCPPortalEnable
        End Get
        Set(ByVal value As Boolean)
            _OCPPortalEnable = value
        End Set
    End Property
    Private _IsCentralizedRuleEngineEnable As Boolean = False

    Public Property IsCentralizedRuleEngineEnable() As Boolean
        Get
            Return _IsCentralizedRuleEngineEnable
        End Get
        Set(ByVal value As Boolean)
            _IsCentralizedRuleEngineEnable = value
        End Set
    End Property

    Private _sCentralizedQCommunicationServiceURL As String = ""
    Public Property sCentralizedQCommunicationServiceURL() As String
        Get
            Return _sCentralizedQCommunicationServiceURL
        End Get
        Set(ByVal value As String)
            _sCentralizedQCommunicationServiceURL = value
        End Set
    End Property

    Private _CorrectRemittance As Boolean
    Public Property CorrectRemittance() As Boolean
        Get
            Return _CorrectRemittance
        End Get
        Set(ByVal value As Boolean)
            _CorrectRemittance = value
        End Set
    End Property
#End Region

#Region " Public Functions"
    Public Function UpdateSettings() As Boolean
        ' _sHL7SendingFacility, _sHL7ReceivingApplication , _sHL7ReceivingFacility 


        'Return UpdateSettings(_tmAppointmentStartTime, _tmAppointmentEndTime, _nAppointmentInterval, _nPULLCHARTSInterval, _nMaxNoOfFAXRetries, _nFAXRetryInterval, m_FAXCompression, m_FAXSpeakerVolume, m_FAXReceiveEnabled, m_HPIEnabled, m_LocationAddressed, m_OMRCategoryHistory, m_OMRCategoryROS, m_OMRCategoryPatientRegistration, m_OMRDirectiveCategory, m_Labs, m_OMRCategoryFax, m_blnPwdComplexity, m_NoOfAttempts, m_blnClinicDISettings, _blnRecordLevelLocking, _nThresholdValue, _sHL7SystemPath, _sHL7SendingFacility, _sHL7ReceivingApplication, _sHL7ReceivingFacility, _sDBVersion, _sAppVersion, _nPendingFaxUserID, _nRecieveFaxUserID, _blnAutoPatientCode, m_Radiology, _sRptUserName, _sRptPassword, _blnUseFaxNoPrefix, _sFaxNoPrefix)
        'sarika internet fax parameters added
        Return UpdateSettings(_tmAppointmentStartTime, _tmAppointmentEndTime, _nAppointmentInterval, _nPULLCHARTSInterval, _nMaxNoOfFAXRetries, _nFAXRetryInterval, m_FAXCompression, m_FAXSpeakerVolume, m_FAXReceiveEnabled, m_HPIEnabled, m_LocationAddressed, m_OMRCategoryHistory, m_OMRCategoryROS, m_OMRCategoryPatientRegistration, m_OMRDirectiveCategory, m_Labs, m_OMRCategoryFax, m_blnPwdComplexity, m_NoOfAttempts, m_blnClinicDISettings, _blnRecordLevelLocking, _nThresholdValue, _sHL7SystemPath, _sHL7SendingFacility, _sHL7ReceivingApplication, _sHL7ReceivingFacility, _sDBVersion, _sAppVersion, _nPendingFaxUserID, _nRecieveFaxUserID, _blnAutoPatientCode, m_Radiology, _sRptUserName, _sRptPassword, _blnUseFaxNoPrefix, _sFaxNoPrefix, _blnInternetFax, _seFaxUserID, _seFaxUserPassword, _seFaxDirDownload, _MCIRReportPath, _sCCDfilePath, _ShowAdvancedGrowthChart, _GrowthChartPercentile, _nLoadNoOfFaxes, _ShowAgeInDays, _AgeLimit, _AgeLimitForWeeks, m_blnAddPatient, m_PMServerName, m_PMDatabaseName, m_blnSQLAuthentication, m_PMSQLUserId, m_PMSQLPwd, m_PMConnectionString, m_SignatureText, _nDMSImageDIP, _bUseFileCompression, _SplitDoc, m_RecoverDMSV2, m_RecoverDMSV2Path, m_DeleteDMSDoc, _bUsecodedhistory, _bIsSendEMail, _sSendEmailAddress, _bIsAdvanceRxEnabled, _bIsAdvanceRxStagingServer, _sEARDirectory, _sSiteID, m_blnClinicFormularySettings, _SetCPTtoAllICD9, _SendUnassociatedDiagnosis, _UseSitePrefix, _UnclosedDayStatement, _blnAllowEditablePatientCode, _FutureCloseDateDays, _ExplicitlyAcceptTask, globlnhl7OutBound, globlnhl7Sendpatientdet, globlnhl7Sendapptdet, m_ServicesServerName, m_ServicesDatabaseName, m_ServicesUserID, m_ServicesPassword, m_ServicesAuthentication, _OCPRCMDefaultCategory, _OCPPortalEnable, _IsCentralizedRuleEngineEnable, _sCentralizedQCommunicationServiceURL) '', m_coSignature)

    End Function


    'Public Function UpdateSettings(ByVal tmAppointmentStartTime As DateTime, ByVal tmAppointmentEndTime As DateTime, ByVal nAppointmentInterval As Int16, ByVal nPULLCHARTSInterval As Int16, ByVal nMaxNoOfFAXRetries As Int16, ByVal nFAXRetryInterval As Int16, ByVal sFAXCompression As String, ByVal sFAXSpeakerVolume As String, ByVal blnFAXReceiveEnabled As Boolean, ByVal blnHPIEnabled As Boolean, ByVal blnLocationAddressed As Boolean, ByVal sOMRCategoryHistory As String, ByVal sOMRCategoryROS As String, ByVal sOMRCategoryPatientRegistration As String, ByVal sOMRCategoryDirective As String, ByVal sLabsCategory As String, ByVal sOMRCategoryFax As String, ByVal blnPwdComplexityflag As Boolean, ByVal NoOfAttempts As Integer, ByVal blnClinicDISettings As Boolean, ByVal blnRecordLevelLocking As Boolean, ByVal nThresholdValue As Double, ByVal sHL7SystemPath As String, ByVal sHL7SendingFacility As String, ByVal sHL7ReceivingApplication As String, ByVal sHL7ReceivingFacility As String, ByVal sDBVersion As String, ByVal sAppVersion As String, ByVal nPendingFaxUserID As Int64, ByVal nRecieveFaxUserID As Int64, ByVal blnAutoPatientCode As Boolean, ByVal RadiologyCategory As String, ByVal sRptUserName As String, ByVal sRptPassword As String, ByVal blnUseFaxNoPrefix As Boolean, ByVal sFaxNoPrefix As String) As Boolean
    '    Dim objCon As New SqlConnection
    '    objCon.ConnectionString = gloEMRAdmin.mdlGeneral.GetConnectionString()
    '    Dim objCmd As New SqlCommand
    '    'Dim objSQLDataReader As SqlDataReader
    '    objCmd.CommandType = CommandType.StoredProcedure
    '    objCmd.CommandText = "sp_UpdateSettings"
    '    Dim objParaSettingsName As New SqlParameter
    '    Dim objParaSettingsValue As New SqlParameter
    '    objCmd.Connection = objCon

    '    objCon.Open()
    '    'Clinic Working Time
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Clinic Start Time"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)

    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = tmAppointmentStartTime
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.DateTime
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()


    '    'Clinic Closing Time
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Clinic Closing Time"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = tmAppointmentEndTime
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.DateTime
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()


    '    'Appointment Interval
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Appointment Interval"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = nAppointmentInterval
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Int
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    'PULL CHARTS Interval
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Pull Chart Interval"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = nPULLCHARTSInterval
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Int
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    'Max No Of FAX Retries
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Max FAX Retries"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = nMaxNoOfFAXRetries
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Int
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    'FAX Retry Interval
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "FAX Retry Interval"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = nFAXRetryInterval
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Int
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    'FAX Compression
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "FAX Compression"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = sFAXCompression
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()


    '    'FAX Speaker Volume
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "FAX Speaker Volume"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = sFAXSpeakerVolume
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    'FAX Receive Enabled
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "FAX Receive"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        If blnFAXReceiveEnabled = True Then
    '            .Value = 1
    '        Else
    '            .Value = 0
    '        End If
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Int
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()


    '    'HPI Enabled
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "HPI"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        If blnHPIEnabled = True Then
    '            .Value = 1
    '        Else
    '            .Value = 0
    '        End If
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Int
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    'Pull Address
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Pull Address"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        If blnLocationAddressed = True Then
    '            .Value = 1
    '        Else
    '            .Value = 0
    '        End If
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Int
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    'OMR Category History
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "OMR Category - History"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = sOMRCategoryHistory
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    'OMR Category ROS
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "OMR Category - ROS"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = sOMRCategoryROS
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    'OMR Category Patient Registration
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "OMR Category - Patient Registration"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = sOMRCategoryPatientRegistration
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    '****************************************
    '    'OMR Category Directive
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "OMR Category - Directive"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = sOMRCategoryDirective
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    'Lab Category
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Lab Category"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)

    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = sLabsCategory
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    'Radiology Category
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Radiology Category"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)

    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = RadiologyCategory
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    'sarika 31st aug 07
    '    'Fax Category
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "OMR Category - Fax"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = sOMRCategoryFax
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()
    '    '--------------

    '    'Password Complexity
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Password Complexity"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        If blnPwdComplexityflag = True Then
    '            .Value = 1
    '        Else
    '            .Value = 0
    '        End If
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Bit
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()


    '    'Setting the No. of Attempts
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "No. Of. Attempts"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        'If blnPwdComplexityflag = True Then
    '        '    .Value = 1
    '        'Else
    '        '    .Value = 0
    '        'End If
    '        .Value = NoOfAttempts
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.BigInt
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    'sarika 14th June 07
    '    'Setting the Clinic DI
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Clinic DI Settings"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        If blnClinicDISettings = True Then
    '            .Value = 1
    '        Else
    '            .Value = 0
    '        End If
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Bit
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    ' '' Mahesh 20070723 
    '    ' '' Setting for Record Level Locking
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Record Level Locking"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = blnRecordLevelLocking
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Bit
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()



    '    'Threshold value ' 31 july 2007
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Threshold Value"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = nThresholdValue
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Float
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()
    '    '****************************************

    '    '*****************************to save the password complexity settings************************************************


    '    '*****************************to save the password complexity settings************************************************


    '    'sarika 11th aug 07 --to save HL7 System Path
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "HL7 System Path"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = sHL7SystemPath
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    If System.IO.Directory.Exists(sHL7SystemPath) = True Then
    '        Dim _RootPath As String = sHL7SystemPath
    '        Dim _MessageFolder As String = sHL7SystemPath & "\HL7 Message Box"
    '        Dim _InBox As String = _MessageFolder & "\Inbox"
    '        Dim _OutBox As String = _MessageFolder & "\Outbox"
    '        Dim _Error As String = _MessageFolder & "\Errors"

    '        System.IO.Directory.CreateDirectory(_RootPath)
    '        System.IO.Directory.CreateDirectory(_MessageFolder)
    '        System.IO.Directory.CreateDirectory(_InBox)
    '        System.IO.Directory.CreateDirectory(_OutBox)
    '        System.IO.Directory.CreateDirectory(_Error)
    '    End If


    '    '--------------------------------------------------
    '    '******Sandip Deshmukh 22 Nov 2007
    '    '******Sending Facility
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Sending Facility"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = sHL7SendingFacility
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    '******Receiving Application
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Receiving Application"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = sHL7ReceivingApplication
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    '******Receiving Facility
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Receiving Facility"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = sHL7ReceivingFacility
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()
    '    '****** 22 Nov 2007

    '    'sarika 31st aug 07
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "DB Version"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = sDBVersion
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Application Version"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = sAppVersion
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()
    '    '------------------------------------------------------------

    '    'sarika 5th sept 07
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Pending Fax User"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = _nPendingFaxUserID
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.BigInt
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Recieve Fax User"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = _nRecieveFaxUserID
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.BigInt
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()
    '    '-----------------------------------------------------------
    '    ''''''''''''''''''''''''''''''''''''''''''''Code added by Anil on 20071119
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Auto-Generate Patient Code"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        If blnAutoPatientCode = True Then
    '            .Value = 1
    '        Else
    '            .Value = 0
    '        End If
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Bit
    '    End With

    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()
    '    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    '    ''''code added by sarika on 18th jan 08 for gloReporting authentication parameters'''''''''''''''''
    '    'Report UserName
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "UserName"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        If sRptUserName <> "" Then
    '            .Value = sRptUserName
    '        Else
    '            .Value = ""
    '        End If
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With

    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()


    '    'Report Password
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Password"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        If sRptPassword <> "" Then
    '            .Value = sRptPassword
    '        Else
    '            .Value = ""
    '        End If
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With

    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    ''''end of code added by sarika on 18th jan 08 for gloReporting authentication parameters'''''''''''''''''



    '    'sarika UseFaxNoPrefix 12th April 08

    '    '//Use Fax No. Prefix
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Use Fax No. Prefix"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        If blnUseFaxNoPrefix = True Then
    '            .Value = 1
    '        Else
    '            .Value = 0
    '        End If

    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.Bit
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    'Fax No. Prefix
    '    objCmd.Parameters.Clear()
    '    With objParaSettingsName
    '        .ParameterName = "@SettingsName"
    '        .Value = "Fax No. Prefix"
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsName)
    '    With objParaSettingsValue
    '        .ParameterName = "@SettingsValue"
    '        .Value = sFaxNoPrefix
    '        .Direction = ParameterDirection.Input
    '        .SqlDbType = SqlDbType.VarChar
    '    End With
    '    objCmd.Parameters.Add(objParaSettingsValue)
    '    objCmd.ExecuteNonQuery()

    '    '---------sarika UseFaxNoPrefix 12th April 08---------------------------------------------------------------



    '    objCon.Close()
    '    objCon = Nothing
    '    objCmd = Nothing
    '    Return True
    'End Function


    Public Function UpdateSettings(ByVal tmAppointmentStartTime As DateTime, ByVal tmAppointmentEndTime As DateTime, ByVal nAppointmentInterval As Int16, ByVal nPULLCHARTSInterval As Int16, ByVal nMaxNoOfFAXRetries As Int16, ByVal nFAXRetryInterval As Int16, ByVal sFAXCompression As String, ByVal sFAXSpeakerVolume As String, ByVal blnFAXReceiveEnabled As Boolean, ByVal blnHPIEnabled As Boolean, ByVal blnLocationAddressed As Boolean, ByVal sOMRCategoryHistory As String, ByVal sOMRCategoryROS As String, ByVal sOMRCategoryPatientRegistration As String, ByVal sOMRCategoryDirective As String, ByVal sLabsCategory As String, ByVal sOMRCategoryFax As String, ByVal blnPwdComplexityflag As Boolean, ByVal NoOfAttempts As Integer, ByVal blnClinicDISettings As Boolean, ByVal blnRecordLevelLocking As Boolean, ByVal nThresholdValue As Double, ByVal sHL7SystemPath As String, ByVal sHL7SendingFacility As String, ByVal sHL7ReceivingApplication As String, ByVal sHL7ReceivingFacility As String, ByVal sDBVersion As String, ByVal sAppVersion As String, ByVal nPendingFaxUserID As Int64, ByVal nRecieveFaxUserID As Int64, ByVal blnAutoPatientCode As Boolean, ByVal RadiologyCategory As String, ByVal sRptUserName As String, ByVal sRptPassword As String, ByVal blnUseFaxNoPrefix As Boolean, ByVal sFaxNoPrefix As String, ByVal blnInternetFax As Boolean, ByVal seFaxUserID As String, ByVal seFaxUserPassword As String, ByVal seFaxDirDownload As String, ByVal sMCIRReportPath As String, ByVal sCCDfilePath As String, ByVal blnShowAdvancedGrowthChart As Boolean, ByVal enumGrowthChartPercentile As frmSettings_New.enumGrowthChartPercentile, ByVal nLoadNoOfFaxes As Integer, ByVal ShowAgeInDays As Boolean, ByVal AgeLimit As Int64, ByVal AgeLimitForWeeks As Int64, ByVal AddPatientToPM As Boolean, ByVal PMServerName As String, ByVal PMDatabaseName As String, ByVal SQLAuthentication As Boolean, ByVal PMSQLUserId As String, ByVal PMSQLPwd As String, ByVal PMConnectionString As String, ByVal SignatureText As String, ByVal DMSImageDPI As Int32, ByVal UseFileCompression As Boolean, ByVal blnSlitDocument As Boolean, ByVal blnRecoverDMSV2Doc As Boolean, ByVal strRecoveryDMSV2Path As String, ByVal blnDeleteDMSDocumentAfterMigration As Boolean, ByVal blnUsecodedHistory As Boolean, ByVal bIsSendEMail As Boolean, ByVal sSendEmailAddress As String, ByVal IsAdvanceRxEnabled As Boolean, ByVal IsAdvanceRxStagingServer As Boolean, ByVal EARDirectory As String, ByVal SiteID As String, ByVal blnClinicFormularySettings As Boolean, ByVal blnSetCPTtoAllICD9 As Boolean, ByVal SendUnassociatedDiagnosis As Boolean, ByVal _UseSitePrefix As Boolean, ByVal _UnclosedDayStatement As Boolean, ByVal blnAllowEditablePatientCode As Boolean, ByVal _FutureCloseDateDays As Integer, ByVal _AutoAcceptPatientTask As Integer, ByVal tgloblnhl7OutBound As Boolean, ByVal tgloblnhl7Sendpatientdet As Boolean, ByVal tgloblnhl7Sendapptdet As Boolean, ByVal m_ServicesServerName As String, ByVal m_ServicesDatabaseName As String, ByVal m_ServicesUserID As String, ByVal m_ServicesPassword As String, ByVal m_ServicesAuthentication As Boolean, ByVal sOCPRCMDefaultCategory As String, ByVal bIsOCPPortalEnable As Boolean, ByVal bIsEnableCentralizedRE As Boolean, ByVal sCentralizedQComminicationServiceURL As String) As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        'Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_UpdateSettings"
        Dim objParaSettingsName As New SqlParameter
        Dim objParaSettingsValue As New SqlParameter

        Dim objParaSettingsClinicID As New SqlParameter
        Dim objParaSettingsUserID As New SqlParameter
        Dim objParaSettingsUserClinicFlag As New SqlParameter

        objCmd.Connection = objCon

        objCon.Open()
        'Clinic Working Time
        ''Sandip Darade 20090417
        '1.	Clinic Start Time
        '2.	Clinic Closing Time
        'Replace this setting internal name (database value) with
        '1. ClinicStartTime
        '2. ClinicEndTime
        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            '.Value = "Clinic Start Time"
            .Value = "ClinicStartTime"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)

        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = _tmAppointmentStartTime
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()


        'Clinic Closing Time
        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            '.Value = "Clinic Closing Time"
            .Value = "ClinicEndTime"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = _tmAppointmentEndTime
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()

        '' START   commnnted by  Sandip Darade 200091214  as setting not used in gloPMAdmin

        'Password Complexity

        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "Password Complexity"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            If blnPwdComplexityflag = True Then
                .Value = 1
            Else
                .Value = 0
            End If
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()


        'Setting the No. of Attempts
        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "No. Of. Attempts"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            'If blnPwdComplexityflag = True Then
            '    .Value = 1
            'Else
            '    .Value = 0
            'End If
            .Value = NoOfAttempts
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()



        'sarika 11th aug 07 --to save HL7 System Path
        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "HL7 System Path"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = sHL7SystemPath
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()

        If System.IO.Directory.Exists(sHL7SystemPath) = True Then
            Dim _RootPath As String = sHL7SystemPath
            Dim _MessageFolder As String = sHL7SystemPath & "\HL7 Message Box"
            Dim _InBox As String = _MessageFolder & "\Inbox"
            Dim _OutBox As String = _MessageFolder & "\Outbox"
            Dim _Error As String = _MessageFolder & "\Errors"

            System.IO.Directory.CreateDirectory(_RootPath)
            System.IO.Directory.CreateDirectory(_MessageFolder)
            System.IO.Directory.CreateDirectory(_InBox)
            System.IO.Directory.CreateDirectory(_OutBox)
            System.IO.Directory.CreateDirectory(_Error)
        End If


        '--------------------------------------------------
        '******Sandip Deshmukh 22 Nov 2007
        '******Sending Facility
        'objCmd.Parameters.Clear()
        'With objParaSettingsName
        '    .ParameterName = "@SettingsName"
        '    .Value = "Sending Facility"
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.VarChar
        'End With
        'objCmd.Parameters.Add(objParaSettingsName)
        'With objParaSettingsValue
        '    .ParameterName = "@SettingsValue"
        '    .Value = sHL7SendingFacility
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.VarChar
        'End With
        'objCmd.Parameters.Add(objParaSettingsValue)
        ''Sandip Darade 20090420
        ' ''Add ClinicID, UserID,UserClinicFlag
        'With objParaSettingsClinicID
        '    .ParameterName = "@nClinicID"
        '    .Value = 1
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.BigInt
        'End With
        'objCmd.Parameters.Add(objParaSettingsClinicID)

        'With objParaSettingsUserID
        '    .ParameterName = "@nUserID"
        '    .Value = 0
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.BigInt
        'End With
        'objCmd.Parameters.Add(objParaSettingsUserID)

        'With objParaSettingsUserClinicFlag
        '    .ParameterName = "@nUserClinicFlag"
        '    .Value = enumSettingFlag.User
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.Int
        'End With
        'objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        ' '' End Add ClinicID, UserID,UserClinicFlag
        'objCmd.ExecuteNonQuery()

        '******Receiving Application
        'objCmd.Parameters.Clear()
        'With objParaSettingsName
        '    .ParameterName = "@SettingsName"
        '    .Value = "Receiving Application"
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.VarChar
        'End With
        'objCmd.Parameters.Add(objParaSettingsName)
        'With objParaSettingsValue
        '    .ParameterName = "@SettingsValue"
        '    .Value = sHL7ReceivingApplication
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.VarChar
        'End With
        'objCmd.Parameters.Add(objParaSettingsValue)
        ''Sandip Darade 20090420
        ' ''Add ClinicID, UserID,UserClinicFlag
        'With objParaSettingsClinicID
        '    .ParameterName = "@nClinicID"
        '    .Value = 1
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.BigInt
        'End With
        'objCmd.Parameters.Add(objParaSettingsClinicID)

        'With objParaSettingsUserID
        '    .ParameterName = "@nUserID"
        '    .Value = 0
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.BigInt
        'End With
        'objCmd.Parameters.Add(objParaSettingsUserID)

        'With objParaSettingsUserClinicFlag
        '    .ParameterName = "@nUserClinicFlag"
        '    .Value = enumSettingFlag.User
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.Int
        'End With
        'objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        ' '' End Add ClinicID, UserID,UserClinicFlag
        'objCmd.ExecuteNonQuery()

        ''******Receiving Facility
        'objCmd.Parameters.Clear()
        'With objParaSettingsName
        '    .ParameterName = "@SettingsName"
        '    .Value = "Receiving Facility"
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.VarChar
        'End With
        'objCmd.Parameters.Add(objParaSettingsName)
        'With objParaSettingsValue
        '    .ParameterName = "@SettingsValue"
        '    .Value = sHL7ReceivingFacility
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.VarChar
        'End With
        'objCmd.Parameters.Add(objParaSettingsValue)
        ''Sandip Darade 20090420
        ' ''Add ClinicID, UserID,UserClinicFlag
        'With objParaSettingsClinicID
        '    .ParameterName = "@nClinicID"
        '    .Value = 1
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.BigInt
        'End With
        'objCmd.Parameters.Add(objParaSettingsClinicID)

        'With objParaSettingsUserID
        '    .ParameterName = "@nUserID"
        '    .Value = 0
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.BigInt
        'End With
        'objCmd.Parameters.Add(objParaSettingsUserID)

        'With objParaSettingsUserClinicFlag
        '    .ParameterName = "@nUserClinicFlag"
        '    .Value = enumSettingFlag.User
        '    .Direction = ParameterDirection.Input
        '    .SqlDbType = SqlDbType.Int
        'End With
        'objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        ' '' End Add ClinicID, UserID,UserClinicFlag
        'objCmd.ExecuteNonQuery()
        ''****** 22 Nov 2007

        'sarika 31st aug 07
        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "DB Version"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = sDBVersion
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()

        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "PMApplication Version"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = sAppVersion
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        objCmd.ExecuteNonQuery()

        ''added by pradeep on 29/06/2010 for site prefix
        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "UseSitePrefix"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            If _UseSitePrefix = True Then
                .Value = 1
            Else
                .Value = 0
            End If
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        objCmd.ExecuteNonQuery()


        objCmd.Parameters.Clear()


        '-----------------------------------------------------------
        ''''''''''''''''''''''''''''''''''''''''''''Code added by Anil on 20071119
        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "Auto-Generate Patient Code"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            If blnAutoPatientCode = True Then
                .Value = 1
            Else
                .Value = 0
            End If
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With

        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()
        ''Added by Mayuri:20101004-To add functonality of save as copy patient
        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "Allow-Editable Patient Code"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            If blnAllowEditablePatientCode = True Then
                .Value = 1
            Else
                .Value = 0
            End If
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With

        objCmd.Parameters.Add(objParaSettingsValue)

        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




        'sarika PM DB Credentials 20081128

        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "AddPatientToPM"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = AddPatientToPM
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()

        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "PMServerName"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = PMServerName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()

        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "PMDatabaseName"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = PMDatabaseName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()

        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "PMSQLAuthentication"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = PMSQLAuthentication
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()

        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "PMSQLUserID"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = PMSQLUserId
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()

        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "PMSQLPwd"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = PMSQLPwd
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()


        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "PMConnectionString"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = PMConnectionString
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()


        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "SignatureText"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = m_SignatureText
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()


        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "CoSignatureText"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = m_CoSignatureText
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()





        'Sandip Darade 20090622
        ''Add Default Patient Gender
        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "DefaultPatientGender"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = _sDefaultPatientGender
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()




        ''Sandip Darade 20090709
        ''Add other billing settings
        ' DefaultFeeSpeciality 
        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "DefaultSelfPayAllowed"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = DefaultSelfPayAllowed
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()

        'DefaultSeflPayFeeSchedule
        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "DEFAULTSELFPAYFEESCHEDULE"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = DefaultSelfPayFeeSchedule
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()


        ' CLINICFEESCHEDULE 
        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "CLINICFEESCHEDULE"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = ClinicFeeSchedule
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()

        'Start Auto Accept Patient Task
        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "ExplicitlyAcceptTask"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = _AutoAcceptPatientTask
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With

        objCmd.Parameters.Add(objParaSettingsValue)

        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        objCmd.ExecuteNonQuery()

        'END Auto Accept Patient Task

        'NoOfClaimPerBatch 
        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "NoOfClaimPerBatch"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = NoOfClaimPerBatch
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()

        'NoOfModifiers 
        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "NoOfModifiers"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = NoOfModifiers
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()
        'ShowLabCol DEFAULTFEECHARGES
        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "ShowLabCol"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = ShowLabCol
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()

        ' Defaultfeecharges
        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "DEFAULTFEECHARGES"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = Defaultfeecharges
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()
        '' END Add other billing settings

        '' Start --> Generate Unclosed Day Statement Setting
        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "UnclosedDayStatement"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = _UnclosedDayStatement
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)

        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        objCmd.ExecuteNonQuery()

        '' End --> Generate Unclosed Day Statement Setting



        '' Start --> Future Close date Days Setting
        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "FutureCloseDateDays"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = _FutureCloseDateDays
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        objCmd.ExecuteNonQuery()

        '' Start --> Future Close date Days Setting

        ''Setting added for global HL7 outbound on 7-dec-2012


        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "HL7SENDOUTBOUNDGLOPM"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = tgloblnhl7OutBound
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()



        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "HL7SENDPATIENTDETAILSDEFAULTGLOPM"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = tgloblnhl7Sendpatientdet
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()

        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "HL7SENDAPPOINTMENTDETAILSDEFAULTGLOPM"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = tgloblnhl7Sendapptdet
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        'Sandip Darade 20090420
        ''Add ClinicID, UserID,UserClinicFlag
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)

        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)

        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        '' End Add ClinicID, UserID,UserClinicFlag
        objCmd.ExecuteNonQuery()

        'added  by Ujwala as on 02032015 to store gloServices DB settings 
        'saving gloServices database settings
        objCmd.Parameters.Clear()
        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "SERVICESSERVERNAME"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = m_ServicesServerName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)
        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)
        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        objCmd.ExecuteNonQuery()
        objCmd.Parameters.Clear()

        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "SERVICESDATABASENAME"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = m_ServicesDatabaseName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)
        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)
        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        objCmd.ExecuteNonQuery()
        objCmd.Parameters.Clear()

        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "SERVICESUSERID"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = m_ServicesUserID
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)
        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)
        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        objCmd.ExecuteNonQuery()
        objCmd.Parameters.Clear()

        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "SERVICESPASSWORD"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = m_ServicesPassword
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)
        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)
        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        objCmd.ExecuteNonQuery()
        objCmd.Parameters.Clear()

        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "SERVICESAUTHEN"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = m_ServicesAuthentication
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)
        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)
        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.User
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        objCmd.ExecuteNonQuery()
        'end  by Ujwala as on 02032015 to store gloServices DB settings

        objCmd.Parameters.Clear()

        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "OCPDEFAULTRCMCATEGORY"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = sOCPRCMDefaultCategory
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)
        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)
        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.Clinic
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        objCmd.ExecuteNonQuery()

        objCmd.Parameters.Clear()

        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "OCPPortalEnable"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = bIsOCPPortalEnable
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)
        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)
        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.Clinic
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        objCmd.ExecuteNonQuery()


        objCmd.Parameters.Clear()

        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "IsCentralizedRuleEngineEnable"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = bIsEnableCentralizedRE
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Bit
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)
        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)
        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.Clinic
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        objCmd.ExecuteNonQuery()


        objCmd.Parameters.Clear()

        With objParaSettingsName
            .ParameterName = "@SettingsName"
            .Value = "sCentralizedQCommunicationServiceURL"
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsName)
        With objParaSettingsValue
            .ParameterName = "@SettingsValue"
            .Value = sCentralizedQComminicationServiceURL
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaSettingsValue)
        With objParaSettingsClinicID
            .ParameterName = "@nClinicID"
            .Value = 1
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsClinicID)
        With objParaSettingsUserID
            .ParameterName = "@nUserID"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.BigInt
        End With
        objCmd.Parameters.Add(objParaSettingsUserID)
        With objParaSettingsUserClinicFlag
            .ParameterName = "@nUserClinicFlag"
            .Value = enumSettingFlag.Clinic
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaSettingsUserClinicFlag)
        objCmd.ExecuteNonQuery()

        objCon.Close()
        'objCon = Nothing
        'objCmd = Nothing
        ''Sandip Darade 20091117
        If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
            objCmd.Dispose()
            objCon.Dispose()
        End If
        Return True
    End Function

    Public Function GetSettings() As Boolean
        Dim objCon As New SqlConnection
        objCon.ConnectionString = gloPMAdmin.mdlGeneral.GetConnectionString()
        Dim objCmd As New SqlCommand
        'sarika 25th june 07
        '    Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_FillSettings"
        objCmd.Connection = objCon
        objCon.Open()
        Dim objDA As New SqlDataAdapter(objCmd)
        Dim dsData As New DataSet
        objDA.Fill(dsData)
        objCon.Close()
        'objCon = Nothing
        ''Sandip Darade 20091117
        If Not IsNothing(objCmd) And Not IsNothing(objCon) Then
            objCmd.Dispose()
            objCon.Dispose()
        End If
        If dsData.Tables(0).Rows.Count = 0 Then
            Return False
        End If
        Dim nCount As Integer
        For nCount = 0 To dsData.Tables(0).Rows.Count - 1

            Select Case dsData.Tables(0).Rows(nCount).Item(1).ToString.ToUpper.Trim()
                Case "EnableUB04FontSizeSelection".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _EnableUBFontSizeSelection = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    Else
                        _EnableUBFontSizeSelection = False
                    End If
                Case "EnableCMSFontSizeSelection".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _EnableCMSFontSizeSelection = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    Else
                        _EnableCMSFontSizeSelection = False
                    End If
                Case "UB04 Font".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _UB04_Font = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _UB04_Font = ""
                    End If
                Case "UB04 Font Size".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _UB04_FontSize = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _UB04_FontSize = ""
                    End If
                Case "Capatalize UB04 data".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _UB04_Capitalizedata = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    Else
                        _UB04_Capitalizedata = False
                    End If

                Case "CMS1500 Font".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _CMS1500_Font = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _CMS1500_Font = ""
                    End If
                Case "CMS1500 Font Size".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _CMS1500_FontSize = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _CMS1500_FontSize = ""
                    End If
                Case "Capatalize CMS1500 02/12 data".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _CMS1500_Capitalize0212data = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    Else
                        _CMS1500_Capitalize0212data = False
                    End If

                    'Case "Clinic Start Time".ToUpper
                Case "Use NPOI Library for Excel Integration".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _blnUseNPOIForExcelIntegration = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    Else
                        _blnUseNPOIForExcelIntegration = False
                    End If
                Case "ClinicStartTime".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _tmAppointmentStartTime = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        Return False
                    End If
                    'Case "Clinic Closing Time".ToUpper
                Case "ClinicEndTime".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _tmAppointmentEndTime = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        Return False
                    End If

                Case "Password Complexity".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        m_blnPwdComplexity = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        Return False
                    End If

                    '' Generate Statement on Unclosed Day
                Case "UNCLOSEDDAYSTATEMENT".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        GenerateUnclosedDayStatement = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        Return False
                    End If

                    '' Future Close Date Days
                Case "FUTURECLOSEDATEDAYS".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        FutureCloseDateDays = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        Return False
                    End If

                Case "No. Of. Attempts".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        m_NoOfAttempts = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                        'Else
                        '    m_NoOfAttempts = 3
                    End If


                Case "HL7 System Path".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _sHL7SystemPath = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _sHL7SystemPath = ""
                    End If
                    '******Sandip Deshmukh 22 Nov 2007
                Case "Sending Facility".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _sHL7SendingFacility = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _sHL7SendingFacility = ""
                    End If

                Case "Receiving Application".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _sHL7ReceivingApplication = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _sHL7ReceivingApplication = ""
                    End If

                Case "Receiving Facility".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _sHL7ReceivingFacility = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _sHL7ReceivingFacility = ""
                    End If

                Case "DB Version".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _sDBVersion = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _sDBVersion = ""
                    End If
                Case "gloPMApplicationVersion".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _sAppVersion = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _sAppVersion = ""
                    End If

                Case "Auto-Generate Patient Code".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _blnAutoPatientCode = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    Else
                        _blnAutoPatientCode = False
                    End If
                    ''Added by Mayuri:20101006-To add functonality of save as copy patient
                Case "Allow-Editable Patient Code".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _blnAllowEditablePatientCode = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    Else
                        _blnAllowEditablePatientCode = False
                    End If


                    ''''code added by pradeep on 29/06/2010
                Case "UseSitePrefix".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _UseSitePrefix = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    Else
                        _UseSitePrefix = False
                    End If
                    '''''''''''''''''''''''''''''''''''''''''''''''''



                Case "AddPatientToPM".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        m_blnAddPatient = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    Else
                        m_blnAddPatient = False
                    End If


                Case "PMServerName".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        m_PMServerName = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        m_PMServerName = ""
                    End If

                Case "PMDatabaseName".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        m_PMDatabaseName = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        m_PMDatabaseName = ""
                    End If

                Case "PMSQLAuthentication".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        m_blnSQLAuthentication = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    Else
                        m_blnSQLAuthentication = False
                    End If


                Case "PMSQLPwd".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        m_PMSQLPwd = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        m_PMSQLPwd = ""
                    End If


                Case "PMSQLUserID".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        m_PMSQLUserId = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        m_PMSQLUserId = ""
                    End If

                Case "PMConnectionString".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        m_PMConnectionString = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        m_PMConnectionString = ""
                    End If
                Case "SignatureText".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        m_SignatureText = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        m_SignatureText = ""
                    End If
                Case "CoSignatureText".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        m_CoSignatureText = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        m_CoSignatureText = ""
                    End If

                Case "eARDirectory".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _sEARDirectory = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _sEARDirectory = False
                    End If
                Case "SiteID".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _sSiteID = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _sSiteID = False
                    End If



                    ' Sandip  Darade 20090622
                    ' Read DefaultPatientGender setting
                Case "DefaultPatientGender".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _sDefaultPatientGender = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _sDefaultPatientGender = ""
                    End If
                    '\\ Sandip Darade 20090709
                    '       
                Case "DefaultSelfPayAllowed".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _DefaultSelfPayAllowed = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _DefaultSelfPayAllowed = ""
                    End If

                Case "DEFAULTSELFPAYFEESCHEDULE".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _DefaultSelfPayFeeSchedule = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _DefaultSelfPayFeeSchedule = ""
                    End If

                Case "CLINICFEESCHEDULE".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _ClinicFeeSchedule = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _ClinicFeeSchedule = ""
                    End If
                    '     
                Case "NoOfClaimPerBatch".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _NoOfClaimPerBatch = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _NoOfClaimPerBatch = ""
                    End If
                Case "NoOfModifiers".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _NoOfModifiers = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _NoOfModifiers = ""
                    End If
                Case "ShowLabCol".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _ShowLabCol = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _ShowLabCol = "0"
                    End If
                Case "DEFAULTFEECHARGES".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _Defaultfeecharges = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _Defaultfeecharges = ""
                    End If

                    ''Added By MaheshB
                Case "PatientAccountCode".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _PatientAccountCode = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _PatientAccountCode = ""
                    End If

                    ''Added By MaheshB 20091127
                Case "IncludeFacilitieswithPOS11onClaim".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _IncludeFacilitieswithPOS11onClaim = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _IncludeFacilitieswithPOS11onClaim = ""
                    End If
                    'Mahesh Nawal   20100407

                    'Added by Vijay 20100813 for getting Enable Billing and Revenue code settings

                Case "UB04_EnableBilling".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _UB04_EnableBilling = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _UB04_EnableBilling = ""
                    End If

                Case "UB04_RevenueCode".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _UB04_RevenueCode = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _UB04_RevenueCode = ""
                    End If
                Case "UB04_TypeOfBill".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _UB04_TypeOfBilling = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _UB04_TypeOfBilling = ""
                    End If
                Case "UB04_AdmisionType".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _UB04_AdmisionType = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _UB04_AdmisionType = ""
                    End If
                Case "UB04_AdmisionSource".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _UB04_admisionSource = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _UB04_admisionSource = ""
                    End If


                Case "UB04_DischargeStatus".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _UB04_DischargeStatus = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _UB04_DischargeStatus = ""
                    End If
                    'End code vijay
                Case "COPY_EDI_FILES".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _CopyClaimtoServer = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _CopyClaimtoServer = ""
                    End If
                Case "IsMultipleClearingHouse".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _IsMultipleClearingHouse = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    Else
                        _IsMultipleClearingHouse = False
                    End If

                Case "Statement Minimum Payment".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _StatementMinPay = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        _StatementMinPay = False
                    End If
                    'Added by Subashish date 05-01-2011 for add 3 new property variable to hold the radio buttons value for  Patient Account Feature and Allow Multiple Guranter 
                Case "Patient Account Feature".ToUpper

                    _IsPatientAccountFeatureEnabled = False
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        If dsData.Tables(0).Rows(nCount).Item(2).ToString().Length > 0 Then
                            If CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean) = True Then
                                _IsPatientAccountFeatureEnabled = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                            End If
                        End If
                        '_IsPatientAccountFeatureEnabled = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                        '_IsPatientAccountFeatureEnabled = Not CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    End If

                Case "Allow Multiple Guarantor".ToUpper
                    _IsAllowMultipleGuranterEnabled = False
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        If dsData.Tables(0).Rows(nCount).Item(2).ToString().Length > 0 Then
                            If CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean) = True Then
                                _IsAllowMultipleGuranterEnabled = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                            End If
                        End If
                    End If

                Case "ExplicitlyAcceptTask".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _ExplicitlyAcceptTask = CType(dsData.Tables(0).Rows(nCount).Item(2), Integer)
                    Else
                        _ExplicitlyAcceptTask = 0
                    End If

                Case "Country".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        Country = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        Country = ""
                    End If

                    ''code added for global hl7outbound settings

                Case "Default AppointmentType for Same Day".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        SameDayApptType = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        SameDayApptType = ""
                    End If

                Case "Default AppointmentType for future".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        FutureApptType = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        FutureApptType = ""
                    End If

                Case "HL7SENDOUTBOUNDGLOPM".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        globlnhl7OutBound = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    Else
                        globlnhl7OutBound = False
                    End If

                Case "HL7SENDPATIENTDETAILSDEFAULTGLOPM".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        globlnhl7Sendpatientdet = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    Else
                        globlnhl7Sendpatientdet = False
                    End If

                Case "HL7SENDAPPOINTMENTDETAILSDEFAULTGLOPM".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        globlnhl7Sendapptdet = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    Else
                        globlnhl7Sendapptdet = False
                    End If

                    'Case "Copy Account".ToUpper
                    '    _IsAllowCopyAccountEnabled = False
                    '    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                    '        If dsData.Tables(0).Rows(nCount).Item(2).ToString().Length > 0 Then
                    '            If CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean) = True Then
                    '                _IsAllowCopyAccountEnabled = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    '            End If
                    '        End If
                    '    End If

                    'Case "Merge Account".ToUpper 'Added By Mahesh S(Apollo) on 1-Feb-2011 for MergeAccount
                    '    _IsAllowMergeAccountEnabled = False
                    '    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                    '        If dsData.Tables(0).Rows(nCount).Item(2).ToString().Length > 0 Then
                    '            If CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean) = True Then
                    '                _IsAllowMergeAccountEnabled = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    '            End If
                    '        End If
                    '    End If

                    '    'End
                    ''7022Items: Home Billing- Added to save setting for USEAREACODEFORPATIENT in database
                    ''Save false if No is selected and true if Yes is selected
                Case "USEAREACODEFORPATIENT"
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        _UseAreaCodeForPatient = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    Else
                        _UseAreaCodeForPatient = False
                    End If
                    ''Case "SERVICESDATABASENAME".ToUpper()
                    ''    ''Added ServicesDatabaseName by Ujwala on 23022015 to get ServicesDB Name from settings table     
                    ''    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False And Convert.ToString(dsData.Tables(0).Rows(nCount).Item(2)).Trim() <> "" Then
                    ''        m_ServicesDatabaseName = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    ''    Else
                    ''        m_ServicesDatabaseName = "gloServices"
                    ''    End If
                    ''    ''Added ServicesDatabaseName by Ujwala on 23022015 to get ServicesDB Name from settings table 
                Case "SERVICESSERVERNAME".ToUpper()
                    ''Added SERVICESSERVERNAME by Ujwala on 02032015 to get ServicesDB Name from settings table     
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False And Convert.ToString(dsData.Tables(0).Rows(nCount).Item(2)).Trim() <> "" Then
                        m_ServicesServerName = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        m_ServicesServerName = ""
                    End If
                    ''Added SERVICESSERVERNAME by Ujwala on 02032015 to get ServicesDB Name from settings table   
                Case "SERVICESDATABASENAME".ToUpper()
                    ''Added ServicesDatabaseName by Ujwala on 20022015 to get ServicesDB Name from settings table     
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False And Convert.ToString(dsData.Tables(0).Rows(nCount).Item(2)).Trim() <> "" Then
                        m_ServicesDatabaseName = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        m_ServicesDatabaseName = "gloServices"
                    End If
                    ''Added ServicesDatabaseName by Ujwala on 02032015 to get ServicesDB Name from settings table   
                Case "SERVICESUSERID".ToUpper()
                    ''Added SERVICESUSERID by Ujwala on 02032015 to get ServicesDB Name from settings table     
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False And Convert.ToString(dsData.Tables(0).Rows(nCount).Item(2)).Trim() <> "" Then
                        m_ServicesUserID = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        m_ServicesUserID = ""
                    End If
                    ''Added SERVICESUSERID by Ujwala on 02032015 to get ServicesDB Name from settings table   
                Case "SERVICESPASSWORD".ToUpper()
                    ''Added ServicesDatabaseName by Ujwala on 02032015 to get ServicesDB Name from settings table     
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False And Convert.ToString(dsData.Tables(0).Rows(nCount).Item(2)).Trim() <> "" Then
                        m_ServicesPassword = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        m_ServicesPassword = ""
                    End If
                    ''Added SERVICESPASSWORD by Ujwala on 02032015 to get ServicesDB Name from settings table  
                Case "SERVICESAUTHEN".ToUpper()
                    ''Added SERVICESAUTHEN by Ujwala on 02032015 to get ServicesDB Name from settings table     
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False And Convert.ToString(dsData.Tables(0).Rows(nCount).Item(2)).Trim() <> "" Then
                        m_ServicesAuthentication = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    Else
                        m_ServicesAuthentication = False
                    End If
                    ''Added SERVICESAUTHEN by Ujwala on 02032015 to get ServicesDB Name from settings table 
                Case "EnableAppointmentLinkingToCharges".ToUpper()
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False And Convert.ToString(dsData.Tables(0).Rows(nCount).Item(2)).Trim() <> "" Then
                        EnablePatientAppointmentsLinkingToCharges = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    Else
                        EnablePatientAppointmentsLinkingToCharges = False
                    End If
                Case "GLOAUSPORTALURL".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        gloAusPortalUrl = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        gloAusPortalUrl = ""
                    End If
                Case "DEMONPIS".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        DemoNPIs = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        DemoNPIs = ""
                    End If
                Case "OCPDEFAULTRCMCATEGORY".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        OCPRCMDefaultCategory = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        OCPRCMDefaultCategory = ""
                    End If
                Case "OCPPORTALENABLE".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        OCPPortalEnable = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    Else
                        OCPPortalEnable = False
                    End If
                Case "Restrict user to correct the remittance".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        CorrectRemittance = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    Else
                        CorrectRemittance = False
                    End If
                Case "IsCentralizedRuleEngineEnable".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False Then
                        IsCentralizedRuleEngineEnable = CType(dsData.Tables(0).Rows(nCount).Item(2), Boolean)
                    Else
                        IsCentralizedRuleEngineEnable = False
                    End If
                Case "sCentralizedQCommunicationServiceURL".ToUpper
                    If IsDBNull(dsData.Tables(0).Rows(nCount).Item(2)) = False And Convert.ToString(dsData.Tables(0).Rows(nCount).Item(2)).Trim() <> "" Then
                        sCentralizedQCommunicationServiceURL = CType(dsData.Tables(0).Rows(nCount).Item(2), String)
                    Else
                        sCentralizedQCommunicationServiceURL = ""
                    End If
            End Select
        Next
        Return True
    End Function

    'Public Function SetPwdComplexitySettings(ByVal NoofCapitalLetters As Integer, ByVal NoofLetters As Integer, ByVal NoofDigits As Integer, ByVal NoOfSpecialChars As Integer, ByVal NoofDays As Integer, ByVal strSQL As String) As Boolean
    Public Function SetPwdComplexitySettings(ByVal strSQL As String) As Boolean
        Dim conn As New SqlConnection(gloPMAdmin.mdlGeneral.GetConnectionString)
        Dim cmd As SqlCommand
        Dim cnt As Integer = 0
        'Dim _strSQL As String = ""

        Try
            conn.Open()
            cmd = New SqlCommand(strSQL, conn)

            cmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        Finally
            conn.Close()
        End Try
    End Function


    '' Sandip Darade 20090709

    Public Sub Get_gloEMRSetting(ByVal SettingName As String, ByRef Value As Object)
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDB.Connect(False)
            oDBParameters.Add("@sSettingsName", SettingName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)

            Value = oDB.ExecuteScalar_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WHERE sSettingsName = '" & SettingName & "'")
        Catch DBErr As gloDatabaseLayer.DBException

            Value = Nothing
            DBErr.ERROR_Log(DBErr.Message)
        Catch ex As Exception

            Value = Nothing
        Finally
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDB.Dispose()
        End Try
    End Sub
    Public Sub Add_gloEMRSetting(ByVal SettingName As String, ByVal Value As String)
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDB.Connect(False)
            oDBParameters.Add("@sSettingsName", SettingName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@sSettingsValue", Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@nClinicID", gnClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
            oDBParameters.Add("@nUserID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)
            oDBParameters.Add("@nUserClinicFlag", SettingFlag.Clinic, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt)

            oDB.Execute("gsp_InUpSettings", oDBParameters)
        Catch DBErr As gloDatabaseLayer.DBException
            DBErr.ERROR_Log(DBErr.Message)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDB.Dispose()
        End Try
    End Sub

    Public Function Add(ByVal Name As String, ByVal Value As String, ByVal ClinicID As Int64, ByVal UserID As Int64, ByVal UserClinicFlag As SettingFlag) As Boolean
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Try
            oDB.Connect(False)

            oDBParameters.Add("@sSettingsName", Name, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@sSettingsValue", Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@nClinicID", ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@nUserID", 0, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)
            oDBParameters.Add("@nUserClinicFlag", UserClinicFlag.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar)

            oDB.Execute("gsp_InUpSettings", oDBParameters)

            Return True
        Catch DBErr As gloDatabaseLayer.DBException

            Return False
        Catch ex As Exception

            Return False
        Finally
            oDB.Disconnect()
            oDBParameters.Dispose()
            oDB.Dispose()
        End Try
    End Function


    Public Function AddValueInTVP(ByVal Name As String, ByVal Value As String, ByVal ClinicID As Int64, ByVal UserID As Int64, ByVal UserClinicFlag As SettingFlag) As Boolean

        Dim rowIndex As Integer = 0

        Try

            rowIndex = odsAdminSettingsTVP.Tables("Settings").Rows.Count
            odsAdminSettingsTVP.Tables("Settings").Rows.Add()
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nSettingsID") = 0
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("sSettingsName") = Name
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("sSettingsValue") = Value
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nClinicID") = gnClinicID
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nUserID") = UserID
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nUserClinicFlag") = UserClinicFlag
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("sType") = "Admin"

            Return True
        Catch ex As Exception
            Return False
        Finally

        End Try
    End Function

    Public Function ClearValuesFromTVP() As Boolean
        Try
            If odsAdminSettingsTVP IsNot Nothing Then
                odsAdminSettingsTVP.Clear()
            End If

            Return True
        Catch ex As Exception
            Return False
        Finally

        End Try
    End Function


    Public Sub AddProviderSettingsInTVP(ByVal ProviderID As Int64, ByVal SettingName As String, ByVal SettingValue As String, ByVal UserID As Long, ByVal ClinicID As Long, ByVal OthersID As Int64, ByVal SettingsType As String)
        Dim rowIndex As Integer = 0
        Try

            rowIndex = odsAdminSettingsTVP.Tables("Settings").Rows.Count

            odsAdminSettingsTVP.Tables("Settings").Rows.Add()
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nSettingsID") = 0
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("sSettingsName") = SettingName
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("sSettingsValue") = SettingValue
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nClinicID") = gnClinicID
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nUserID") = UserID
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nUserClinicFlag") = 0
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nProviderID") = ProviderID
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nOthersID") = OthersID
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("sSettingsType") = SettingsType
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("bSettingFlag") = False
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("sType") = "Provider"

        Catch ex As Exception

        Finally

        End Try
    End Sub

    Public Function AddPatientAccInTVP(ByVal Name As String, ByVal Value As String, ByVal ClinicID As Int64, ByVal UserID As Int64, ByVal UserClinicFlag As SettingFlag) As Boolean

        Dim rowIndex As Integer = 0

        Try

            rowIndex = odsAdminSettingsTVP.Tables("Settings").Rows.Count
            odsAdminSettingsTVP.Tables("Settings").Rows.Add()
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nSettingsID") = 0
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("sSettingsName") = Name
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("sSettingsValue") = Value
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nClinicID") = gnClinicID
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nUserID") = UserID
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nUserClinicFlag") = UserClinicFlag
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("sType") = "PatientAcc"

            Return True
        Catch ex As Exception
            Return False
        Finally

        End Try
    End Function




    Public Sub AddSettings(ByVal ProviderID As Int64, ByVal SettingName As String, ByVal SettingValue As String, ByVal UserID As Long, ByVal ClinicID As Long, ByVal OthersID As Int64, ByVal SettingsType As String)
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()

        Try
            oDB.Connect(False)

            oDBParameters.Add("@SettingName", SettingName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@SettingValue", SettingValue, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@SettingFlag", False, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameters.Add("@ProviderID", ProviderID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@UserID", UserID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@ClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nOthersID", OthersID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@sSettingsType", SettingsType, ParameterDirection.Input, SqlDbType.VarChar)

            oDB.Execute("gsp_InUpProviderSettings", oDBParameters)
        Catch dbex As gloDatabaseLayer.DBException
            dbex.ERROR_Log(dbex.ToString())
        Catch ex As Exception
        Finally
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
            End If
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Sub

    Public Sub AddMidLevelBillingSettingsInTVP(ByVal nProviderID As Int64, ByVal nSettingID As Int64, ByVal UserID As Long, ByVal ClinicID As Long, ByVal SettingsType As MidLevelSettingsType)
        Dim rowIndex As Integer = 0
        Try

            rowIndex = odsAdminSettingsTVP.Tables("Settings").Rows.Count

            odsAdminSettingsTVP.Tables("Settings").Rows.Add()
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nSettingsID") = nSettingID
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("sSettingsName") = ""
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("sSettingsValue") = ""
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nClinicID") = gnClinicID
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nUserID") = UserID
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nUserClinicFlag") = 0
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nProviderID") = nProviderID
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nOthersID") = 0
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("sSettingsType") = ""
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("bSettingFlag") = False
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("nBillingSettingsType") = SettingsType.GetHashCode()
            odsAdminSettingsTVP.Tables("Settings").Rows(rowIndex)("sType") = "MidLevel"

        Catch ex As Exception

        Finally

        End Try
    End Sub

    Public Sub AddMidLevelBillingSettings(ByVal nProviderID As Int64, ByVal nSettingID As Int64, ByVal UserID As Long, ByVal ClinicID As Long, ByVal SettingsType As MidLevelSettingsType)
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()

        Try
            oDB.Connect(False)

            oDBParameters.Add("@nID", 0, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nProviderID", nProviderID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nSettingsID", nSettingID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nBillingSettingType", SettingsType, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@dtCreatedDateTime", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime)
            oDBParameters.Add("@dtModifiedDateTime", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime)
            oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nUserID", UserID, ParameterDirection.Input, SqlDbType.BigInt)

            oDB.Execute("BL_INUP_BL_MidLevelBilling_Settings", oDBParameters)

        Catch dbex As gloDatabaseLayer.DBException
            dbex.ERROR_Log(dbex.ToString())
        Catch ex As Exception
        Finally
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
            End If
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Sub

    Public Sub AddAppointmentDefaultSettings(ByVal nAppointmentTypeID As Int64, ByVal DefaultDOS As Boolean, ByVal DefaultFacility As Boolean, ByVal DefaultRenderingProvider As Boolean)
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()

        Try
            oDB.Connect(False)

            oDBParameters.Add("@nAppointmentTypeID", nAppointmentTypeID, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@bIsDefaultDOS", DefaultDOS, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameters.Add("@bIsDefaultFacility", DefaultFacility, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameters.Add("@bIsDefaultRenderingProvider", DefaultRenderingProvider, ParameterDirection.Input, SqlDbType.Bit)
            oDBParameters.Add("@dtCreatedDate", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime)
            oDBParameters.Add("@dtModifiedDate", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime)

            oDB.Execute("BL_INUP_ChargeDefault_ApptType", oDBParameters)

        Catch dbex As gloDatabaseLayer.DBException
            dbex.ERROR_Log(dbex.ToString())
        Catch ex As Exception
        Finally
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
            End If
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Sub


    Public Function SaveAdminSettings() As Boolean

        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        Dim sTransResult As String = ""
        Dim _oResult As Object = New Object
        Try

            oDBParameters.Add("@tvpSettings", odsAdminSettingsTVP.Tables("Settings"), ParameterDirection.Input, SqlDbType.Structured)
            oDBParameters.Add("@sTranResult", sTransResult, ParameterDirection.Output, SqlDbType.VarChar, 1000)
            oDB.Connect(False)
            oDB.Execute("SaveAdminSettings_TVP", oDBParameters, _oResult)

            If _oResult IsNot Nothing Then
                sTransResult = Convert.ToString(_oResult)
            Else
                sTransResult = ""
            End If

            If (sTransResult <> "Succuess") Then
                MessageBox.Show(sTransResult, "Error While Saving", MessageBoxButtons.OK)
            End If


        Catch dbex As gloDatabaseLayer.DBException
            dbex.ERROR_Log(dbex.ToString())
        Catch ex As Exception
        Finally
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
            End If
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try


    End Function
    Public Sub UpdateNDCUnitPrice(ByVal sNDCSettings As String)
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()

        Try
            oDB.Connect(False)

            oDBParameters.Add("@sNDCSettings", sNDCSettings, ParameterDirection.Input, SqlDbType.VarChar)
            oDB.Execute("UP_NDCUnitPricing", oDBParameters)

        Catch dbex As gloDatabaseLayer.DBException
            dbex.ERROR_Log(dbex.ToString())
        Catch ex As Exception
        Finally
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
            End If
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Sub

    Public Sub GetSetting(ByVal SettingName As String, ByVal UserID As Int64, ByVal ClinicID As Int64, ByRef Value As Object)
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Try
            oDB.Connect(False)
            Dim _sqlQuery As String = "SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WHERE sSettingsName = '" & SettingName & "' AND nClinicID = " & ClinicID & ""
            Value = oDB.ExecuteScalar_Query(_sqlQuery)
        Catch DBErr As gloDatabaseLayer.DBException
            Value = Nothing
            DBErr.ERROR_Log(DBErr.Message)
        Catch ex As Exception
            Value = Nothing
        Finally
            oDB.Disconnect()
            oDB.Dispose()
        End Try
    End Sub

    Public Function GetBusinessCenterSettings(SettingsName As String) As [Boolean]
        Dim _IsRequireBusinessCenterOnPAccounts As Boolean = False
        Dim oDB As New gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString)
        Dim strQuery As String = ""
        Dim _Result As [Object] = Nothing
        Try
            If SettingsName.ToLower() = "businesscenter_feature" Then
                strQuery = "SELECT isnull(sSettingsValue,'False') FROM Settings WHERE lower(sSettingsName)='businesscenter_feature'  and (SELECT isnull(sSettingValue,'False') FROM Settings_Replication WHERE  lower(sSettingName)='patient account feature') = 'True'  "
            ElseIf SettingsName.ToLower() = "businesscenter_patientaccount" Then
                strQuery = "SELECT isnull(sSettingsValue,'False') FROM Settings WHERE  lower(sSettingsName)='businesscenter_patientaccount' and (SELECT isnull(sSettingsValue,'False') FROM Settings WHERE  lower(sSettingsName)='businesscenter_feature') = 'True' and (SELECT isnull(sSettingValue,'False') FROM Settings_Replication WHERE  lower(sSettingName)='patient account feature') = 'True' "
            Else
                strQuery = "SELECT isnull(sSettingsValue,'False') FROM Settings WHERE  lower(sSettingsName)='" + SettingsName.ToLower() + "' and (SELECT isnull(sSettingsValue,'False') FROM Settings WHERE  lower(sSettingsName)='businesscenter_feature') = 'True' and (SELECT isnull(sSettingsValue,'False') FROM Settings WHERE  lower(sSettingsName)='businesscenter_patientaccount') = 'True' and (SELECT isnull(sSettingValue,'False') FROM Settings_Replication WHERE  lower(sSettingName)='patient account feature') = 'True' "
            End If

            oDB.Connect(False)
            _Result = oDB.ExecuteScalar_Query(strQuery)
            If _Result IsNot Nothing AndAlso _Result <> "" Then
                _IsRequireBusinessCenterOnPAccounts = Convert.ToBoolean(_Result)
            End If
            oDB.Disconnect()
        Catch dbex As gloDatabaseLayer.DBException
            dbex.ERROR_Log(dbex.ToString())
            Return _IsRequireBusinessCenterOnPAccounts
        Catch generatedExceptionName As Exception
            Return _IsRequireBusinessCenterOnPAccounts
        Finally
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
        Return _IsRequireBusinessCenterOnPAccounts
    End Function




    Public Sub IsSettingExsits(ByVal SettingName As String, ByVal UserID As Int64, ByVal ClinicID As Int64, ByRef Value As Object)
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Try
            oDB.Connect(False)
            Dim _sqlQuery As String = "SELECT Count(*) As Count FROM Settings WHERE sSettingsName = '" & SettingName & "' AND nClinicID = " & ClinicID & ""
            Value = oDB.ExecuteScalar_Query(_sqlQuery)
        Catch DBErr As gloDatabaseLayer.DBException
            Value = Nothing
            DBErr.ERROR_Log(DBErr.Message)
        Catch ex As Exception
            Value = Nothing
        Finally
            oDB.Disconnect()
            oDB.Dispose()
        End Try
    End Sub


    Public Sub AddPaymentSettings(ByVal InsuranceTypeCode As String, ByVal InsuranceTypeDesc As String, ByVal Payrow As String, ByVal PayrowIndex As String)
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim oDBParameters As New gloDatabaseLayer.DBParameters()
        'nSettingID, sInsTypeCode, sInsTypeDesc, sRowName, sRowIndex, sSettingsName, sSettingsValue, 
        'nProviderID, nUserID, nClinicID, nUserClinicProviderFlag
        Try
            oDB.Connect(False)
            '  oDBParameters.Add("@nSettingID", SettingName, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sInsTypeCode", Convert.ToString(InsuranceTypeCode), ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sInsTypeDesc", InsuranceTypeDesc, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sRowName", Payrow, ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sRowIndex", Convert.ToString(PayrowIndex), ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sSettingsName", "", ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@sSettingsValue", "", ParameterDirection.Input, SqlDbType.VarChar)
            oDBParameters.Add("@nUserID", 0, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nClinicID", 1, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nProviderID", 0, ParameterDirection.Input, SqlDbType.BigInt)
            oDBParameters.Add("@nUserClinicProviderFlag", 0, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Execute("gsp_UpdatePaymentSettings", oDBParameters)
        Catch dbex As gloDatabaseLayer.DBException
            dbex.ERROR_Log(dbex.ToString())
        Catch ex As Exception
        Finally
            If oDBParameters IsNot Nothing Then
                oDBParameters.Dispose()
            End If
            If oDB IsNot Nothing Then
                oDB.Dispose()
            End If
        End Try
    End Sub

    Public Sub DeletePaymentsetting(ByVal InsuranceType As String)
        Try

            Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
            oDB.Connect(False)
            Dim _result As Integer
            _result = oDB.Execute_Query("DELETE Settings_Payment WHERE sInsTypeCode='" & InsuranceType & "'")

        Catch ex As Exception

        End Try
    End Sub

    Public Function GetBillingSettings() As DataTable
        Try
            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim _sqlQuery As String = " SELECT ISNULL(sName,'') as sName, ISNULL(sValue,'') as sValue , ISNULL(bSettingFlag,'0') as bSettingFlag," _
                                     & " ISNULL( nProviderID,0) as nProviderID  FROM Providersettings  WHERE  nClinicID = " & gnClinicID & " ORDER BY nProviderID"
            Dim dt As New DataTable
            odb.Connect(gstrConnectionString)
            dt = odb.ReadQueryData(_sqlQuery)
            Return dt

        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Public Function GetMidLevelBillingSettings() As DataTable
        Try
            Dim odb As New gloStream.gloDataBase.gloDataBase
            Dim _sqlQuery As String = " SELECT Provider.nProviderID," _
            & " Settings.sMidLevelBillingType,nBillingSettingsType,Settings.nSettingsID FROM " _
            & " BL_MidLevelBilling_Settings Provider " _
            & " INNER JOIN BL_MidLevelSettings_MST Settings " _
            & " ON Provider.nSettingsID = Settings.nSettingsID "
            Dim dt As New DataTable
            odb.Connect(gstrConnectionString)
            dt = odb.ReadQueryData(_sqlQuery)
            Return dt
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    Public Function IsanyBadDebtPatient() As Boolean
        Dim oDb As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Dim _Query As String = ""
        Dim _dt As New DataTable()
        Dim Result As Object = 0
        Try
            oDb.Connect(False)
            _Query = "SELECT TOP 1 nPatientId FROM dbo.Patient_BadDebt WITH (NOLOCK)"
            Result = oDb.ExecuteScalar_Query(_Query)

            If Result <> Nothing And Convert.ToString(Result) <> "" Then
                If Convert.ToInt64(Result) > 0 Then
                    Return True
                Else
                    Return False
                End If

            Else
                Return False
            End If

        Catch Ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), True)
            Ex = Nothing
        Finally
            If Not IsDBNull(oDb) Then
                oDb.Disconnect()
                oDb.Dispose()
                oDb = Nothing


            End If
        End Try
        Return False
    End Function
#End Region

End Class


Friend Class clsSettingsGeneral
    ''' <summary>
    ''' This method is called for reading Settings stored per user per Clinic information.
    ''' </summary>
    ''' <param name="SettingName">Name of Setting </param>
    ''' <param name="UserID">User Identifier</param>
    ''' <param name="ClinicID">Clinic ID</param>
    ''' <param name="Value">Setting Value</param>
    ''' <remarks></remarks>
    Public Shared Sub GetSetting(ByVal SettingName As String, ByVal UserID As Int64, ByVal ClinicID As Int64, ByRef Value As Object)
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Try
            oDB.Connect(False)
            Dim _sqlQuery As String = "SELECT Top 1 ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WHERE sSettingsName = '" & SettingName & "' AND nClinicID = " & ClinicID & ""
            Value = oDB.ExecuteScalar_Query(_sqlQuery)
        Catch DBErr As gloDatabaseLayer.DBException
            Value = Nothing
            DBErr.ERROR_Log(DBErr.Message)
        Catch ex As Exception
            Value = Nothing
        Finally
            oDB.Disconnect()
            oDB.Dispose()
        End Try
    End Sub
    ''' <summary>
    ''' This method is need to be called for only in case USERs are not required/Stored and Settings are global in Clinic.
    ''' </summary>
    ''' <param name="SettingName">Name of Setting </param>
    ''' <param name="ClinicID">Clinic ID</param>
    ''' <param name="Value">Setting Value</param>
    ''' <remarks></remarks>
    Public Shared Sub GetSetting(ByVal SettingName As String, ByVal ClinicID As Int64, ByRef Value As Object)
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Try
            oDB.Connect(False)
            Dim _sqlQuery As String = "SELECT Top 1 ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WHERE sSettingsName = '" & SettingName & "' AND nClinicID = " & ClinicID & ""
            Value = oDB.ExecuteScalar_Query(_sqlQuery)
        Catch DBErr As gloDatabaseLayer.DBException
            Value = Nothing
            DBErr.ERROR_Log(DBErr.Message)
        Catch ex As Exception
            Value = Nothing
        Finally
            oDB.Disconnect()
            oDB.Dispose()
        End Try
    End Sub
    ''' <summary>
    ''' This method is need to be called for only in case Clinic and User levels are not required and Settings are global in Clinic.
    ''' </summary>
    ''' <param name="SettingName">Name of Setting </param>
    ''' <param name="Value">Setting Value</param>
    ''' <remarks></remarks>
    Public Shared Sub GetSetting(ByVal SettingName As String, ByRef Value As Object)
        Dim oDB As New gloDatabaseLayer.DBLayer(gstrConnectionString)
        Try
            oDB.Connect(False)
            Dim _sqlQuery As String = "SELECT Top 1 ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WHERE sSettingsName = '" & SettingName & "'"
            Value = oDB.ExecuteScalar_Query(_sqlQuery)
        Catch DBErr As gloDatabaseLayer.DBException
            Value = Nothing
            DBErr.ERROR_Log(DBErr.Message)
        Catch ex As Exception
            Value = Nothing
        Finally
            oDB.Disconnect()
            oDB.Dispose()
        End Try
    End Sub
End Class