Imports System.IO
Imports gloEMRGeneralLibrary.gloGeneral.clsgeneral
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports System.Runtime.InteropServices
Imports gloSettings
Imports gloCCDSchema
Imports gloGlobal
Imports Microsoft.Win32
Imports Wd = Microsoft.Office.Interop.Word

Imports System.Text
'Imports gloEMRGeneralLibrary.gloEMRLab

Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Module mdlGeneral
    Private appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings

    Public gstrDirectWarningMessage As String = "Direct address not set to the login provider. Please set Direct address from gloEMR Admin."
    Public gintLoginSessionID As Long
    Public gblnAllowRefillCancel As Boolean = False ''this setting is added to show/hide the cancel button in Pending refill request screen.
    Public gblIsConfidentialInfo As Boolean = False 'Added by kanchan on 20101112
    Public gblnIsImmunization As Boolean    'Added by kanchan on 20100531
    ''Start :: Yes/No Labs from Admin
    Public gblnYesNoLab As Boolean = False
    ''End :: Yes/No Labs  from Admin

    'SigPlus--------
    ' Problem #28303: 00000156 : Signature Pad not working on Terminal Server
    Public gstrSigPlusTabletPortPath As String = ""
    Public gshortSigPlusTabletType As Short = 0
    Public gblnIsSigPlusSettingsAvailable As Boolean = False
    Public gblnSigPlusSupportTS As Boolean = False

    Private _gblnLocalSignaturePad As Boolean
    Public Property gblnLocalSignaturePad() As Boolean
        Get
            Return _gblnLocalSignaturePad
        End Get
        Set(ByVal value As Boolean)
            _gblnLocalSignaturePad = value
        End Set
    End Property
    '---------------

    Public gblnGreyScreenIssue As Boolean = False

    Public Patientage As New gloUserControlLibrary.AgeDetail
    'CCHIT 08 Setting for Coded History from Admin 
    Public gblnCodedHistory As Boolean
    ''Setting to Show Coded History code,description or both from Admin 
    Public gsrtrShowCodedHistory As String
    'Rx Report setting added by supriya 24/7/2008
    Public RxDeclaration As String = ""
    'CCHIT 08
    Public RxFooterNote As String = ""
    'CCHIT 08
    ''Added by kanchan on 20101008
    Public gnCCDDefaultUserID As Long
    Public gsCCDUSerName As String = ""

    Public gPBMAllselectFlag As Boolean
    Public glbIsPediatric As Boolean = False ''Pediatric Settings (Used in Dashboard and Splash)
    Public gblnShowAgeInDays As Boolean '' Splash Screen (GetSettings)
    Public gblnPatDeamoMerg As Boolean = False '' Called in/from Merge
    ''Added Rahul for Snomed Setting on 20101006
    Public gblnSMDBSetting As Boolean
    Public gstrSMDBConnstr As String
    Public gstrSMDBServerName As String
    Public gstrSMDBDatabaseName As String
    Public gstrSMDBUserID As String
    Public gstrSMDBPassWord As String
    Public gblnSMDBAuthen As Boolean
    Public gblnIsDrugSave As Boolean = False 'Added by pradeep on 20101025 for task of drug
    Public gnPrescriptionVisitID As Int64 = 0 'Added by pradeep on 20101025 for task of drug
    ''
    'Code Start-Added  by kanchan on 20100909 for rxnorm db settings
    Public gblnRxNDBSetting As Boolean
    Public gRxNDBConnstr As String = ""
    Public gRxNServerName As String = ""
    Public gRxNDatabaseName As String = ""
    Public gRxNUserID As String = ""
    Public gRxNPassWord As String = ""
    Public gRxNIsSQLAUTHEN As Boolean
    'Code End-Added  by kanchan on 20100909 for rxnorm db settings
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''

    ''Added by Rahul for Unauthenticated Login Banner on 20101020
    Public gstrUnauthLoginBanner As String
    ''End

    ''declared object by Abhijeet on 20110318 for WelchAllyn Device SDK connectivity
    Public gm_oConnectivity As WAConnSDKATLLib.WAConnectivityATLClass = Nothing
    ''End of changes by Abhijeet on 20110318 for WelchAllyn Device SDK connectivity

    Public gintAgeLimit As Int32 = 0 '' Splash Screen (GetSettings)
    Public gSelectedPBMName As String = ""
    Public gSelectedPBM_MemberID As String = ""
    Public RxHUBPatientConsent As String = ""
    'Public RxHUBstartDate As String = ""
    'Public RxHUBendDate As String = ""
    '' RxHub Disclaimer
    Public gstrRxHubDisclaimer As String
    'the RxHub Stagin and Production Login credentials are saved from admin in the Settings table
    Public gstrRxHubParticipantId As String = ""
    Public gstrRxHubPassword As String = ""
    Public gstrPrintMultipleRx_PerScriptPage As Boolean = False
    'Dhruv Used in the synopsis
    Public gblnNewPatientExam As Boolean = True
    '' chetan integrated to implement 'Allow Emergency Access of Patient Chart on Oct 05 2010'
    Public gbAllowEmergencyAccess As Boolean = False
    '' chetan  integrated to implement 'Allow Emergency Access of Patient Chart on Oct 05 2010'
    Public gblnIsRxEligUserRight As Boolean = False ''''''this var wil be initialized when the dashboard load with user rights func. used to show and hide the RxElig button in EMR


    Public gWinwordPath As String = "D:\Program Files\Microsoft Office\OFFICE11\winword.exe"

    Public gDMSV2TempPath As String = gloSettings.FolderSettings.AppTempFolderPath & "DMSV2Temp"
    Public gDMSV3TempPath As String = gloSettings.FolderSettings.AppTempFolderPath & "DMSV3Temp"
    Public gClinicID As Long = 1
    Public gDMSScanDocumentView As gloEDocumentV3.Enumeration.enum_DocumentView = gloEDocumentV3.Enumeration.enum_DocumentView.YearView
    Public gDMSViewDocumentView As gloEDocumentV3.Enumeration.enum_DocumentView = gloEDocumentV3.Enumeration.enum_DocumentView.MonthView
    Public Const gHistory_Category As String = "DBHistory"
    Public gblnGenerateMicReport As Boolean

    '' MCIR Report Path
    Public gstrMCIRReportPath As String

    Public gIsUnfinishedExamDelete As Boolean = False

    'CCD File Path
    Public gstrCCDFilePath As String
    'StyleSheetPath
    Public gstrStyleSheetPath As String
    '' Advanced Growth Chart
    Public gblnAdvancedGrowthChart As Boolean
    ''
    'DMS File Size Setting
    Public gnDMSFileSizeMax As Int64 = 10 * 1024 * 1024

    Public gnDMSFileSizeMin As Int64 = 5 * 1024 * 1024

    Public gstrgloEMRStartupPath As String

    '' Varable added to Solve the Terminal Server  
    '' For Keeping the Temporary Files
    Public gstrgloTempFolder As String

    Public gstrgloEMRAutoUpdatesPath As String
    Public blnMicrophoneOn As Boolean = False    'To check Diction/Voice commands are going on or not

    Public gstrMessageBoxCaption As String = "gloEMR"
    Public gstrClientMachineName As String
    Public gnClientMachineID As String
    'Bug #82275: CR0000361: Patient color status based on color
    'Added setting to Enable Static Color
    Public _isEnableStaticColor As Boolean = False

    'Added Setting for AutoDelete CCDA Files
    Public _isAutoDeleteCCDAFiles As Boolean = False

    Public gIsCCHITSecurityAdmin As Boolean = False

    Public gstrLoginName As String
    '' '  20070530
    Public gnLoginID As Long
    Public gstrNickName As String
    Public gstrLoginPassword As String
    Public gstrLoginTime As String
    Public gblnIsAdmin As Boolean
    Public gstrDatabaseName As String
    Public gstrSQLServerName As String
    Public gblnSQLAuthentication As Boolean
    Public gstrSQLUserEMR As String
    Public gstrSQLPasswordEMR As String
    Public gblnCheckNewVersion As Boolean
    Public gblnEMEnable As Boolean = False

    Public gstrCurrentSpeaker As String

    Public gblnFAXCoverPage As Boolean

    Public gblnVoiceEnabled As Boolean
    Public gblnScanEnabled As Boolean

    Public gblnSpeakerExists As Boolean = False
    Public gstrSpeakerTopic As String

    Public gClinicStartTime As DateTime
    Public gClinicEndTime As DateTime
    Public gnAppointmentInterval As Int16
    Public gnPULLCHARTSInterval As Int16
    Public gblnSecurityUser As Boolean = False

    '' This Encryption Key will be use for gloEMR User Password Encryption/Decryption
    Public Const constEncryptDecryptKey As String = "12345678"

    '' This Encryption Key will be use for Services & SQL Password
    Public Const constEncryptDecryptKey_Services As String = "20gloStreamInc08"
    ''
    Public gstrHelpProvider As String = ""
    Public gblnPatientAdded As Boolean = False

    '<Vinayak>
    Public gOMRCategory_History As String = ""
    Public gOMRCategory_ROS As String = ""
    Public gOMRCategory_PatientRegistration As String = ""
    Public gDMSCategory_PatientDirective As String = ""
    Public gDMSCategory_Labs As String = ""
    '<Vinayak>
    Public gDMSCategory_RxMed As String = "" ''''for cchit11 medication reconcilation
    ''Added by Mayuri:20110118-VIS Immunization category
    Public gDMSCategory_VIS As String = ""
    Public gDMSCategory_Amedment As String = ""

    Public gRXELIGIBILITYEMR As String = "" ''''this will tell wether the processing for RxEligibility is to be done using Code or by Web service
    Public gEDISERVICEPATH As String = "" '''''this will tell to which url the webservice is pointing and therefore the Rxeligibility request will be sent to this url
    '''' Pramod CCHIT 2007 for Radiology Scan Document
    Public gDMSCategory_Radiology As String = ""

    Public gblnAutoEligibility As Boolean = False
    Public gblnAutoPDMPEligiblity As Boolean = False
    Public gblPDMPSaveToDMS As Boolean = False
    Public nWorkingTimeColor As Integer
    Public nNonWorkingTimeColor As Integer
    Public nBusyTimeColor As Integer
    Public nMissingAppointmentsColor As Integer
    Public nPullChartsAppointmentsColor As Integer
    'Patient Portal
    Public gblnPatientPortalSendActivationEmail = False
    Public gblnPatientPortalActivationEmailAlreadySent = False
    'Patient Portal

    'FAX Settings
    Public gstrFAXPrinterName As String
    Public gstrFAXOutputDirectory As String

    Public gstrFAXReceivedDirectory As String = ""

    '' CR00000126 : FAX for Terminal Server
    '' New setting FAXDownloadDirectory, added for Terminal server case
    Public gstrFAXReceivedDirectoryTS As String


    Public gnNoOfAttempts As Int16
    Public gblnSameCoverPageForAllReferrals As Boolean
    Public gblnFAXPrinterSettingsSet As Boolean = False
    Public gstrFAXCompression As String = ""


    Public gstrServerPath As String

    Public frmOpenFinishExam As frmFinishExam

    Public gnAppointmentModuleLevel As Byte

    'Patient Status
    Public gtsrPatientStatus_Pending As String = "Legal Pending"
    Public gtsrPatientStatus_Deceased As String = "Deceased"
    Public gtsrPatientStatus_LockCharts As String = "Lock Charts"


    'Lock Screen
    Public gLockTime As Long = 10
    Public gblnAutoLockEnable As Boolean = False

    '<CMS>
    'flag to check if system is HPI enabled or not
    Public blnHPIEnabled As Boolean
    Public blnLocationAddress As Boolean
    '<CMS>

    '<DM-Start>
    Public x = Screen.PrimaryScreen.Bounds.Width
    Public y = Screen.PrimaryScreen.Bounds.Height
    Public IsFindCriteriaInProcess As Boolean = False
    '<DM-Finish>



    'RXHUB -  
    Public gblnAdvErxEnabled As Boolean
    Public gblnRxhubStagingServer As Boolean

    Public gblnMedHX10Dot6Enabled As Boolean
    Public MedHxRestriction As Int32 = 0
    'Public gblnMedHX10Dot6URL As String = String.Empty

    Public gnRefillReq_PrescriberID As Int64 'this is passed from the Refill Request popup grid selected PrescriberID to the RefillRequest Control on the Rx Form

    'if setting is true then load the formulary drugs with flag as 99 to sp_fillDrugs_Mst
    Public gblnLoadFormularyDrugs As Boolean = False

    'setting to store the formulary alert at clinic level
    Public gblnClinicFormularyAlert As Boolean = False '---clinic level
    'Removed Setting 
    ' Public gblnMachineFormularyAlert As Boolean = True '----- Machine level
    Public Const MachineFormularyAlert As String = "MachineFormularyAlert"

    '06-May-13 Aniket: Resolving Bug #50030:
    Public gblnEligibilityUserRights As Boolean

    Public gblnFormularyAlertnativesAllDrgs As Boolean = True '---All Drugs
    Public Const FormularyAlertnativesAllDrgs As String = "FormularyAlertnativesAllDrgs"
    Public gblnFormularyAlertnativesOffFormularyDrgs As Boolean = True '---Off Formulary
    Public Const FormularyAlertnativesOffFormularyDrgs As String = "FormularyAlertnativesOffFormularyDrgs"
    Public gblnFormularyAlertnativesNRDrgs As Boolean = True '---Not Reimbursable
    Public Const FormularyAlertnativesNRDrgs As String = "FormularyAlertnativesNRDrgs"
    Public gblnShowOffformularyalternatives As Boolean = False '--- Formulary Alternatives
    Public Const ShowOffformularyalternatives As String = "ShowOffFormularyAlternatives"

    Public gblnShowNDCInMedicationHistory As Boolean = False '--- Show NDC In Medication History
    Public Const ShowNDCInMedicationHistory As String = "ShowNDCInMedicationHistory"

    Public gblnShowNDCInAlternatives As Boolean = False '--- Show NDC In Alternatives
    Public Const ShowNDCInAlternatives As String = "ShowNDCInAlternatives"

    Public gblnClinicDISetting As Boolean
    Public gblnAllowUserDISetting As Boolean

    Public gblnviewCompleteOtherUsersTasks As Boolean  ''added to show other user dropdown in view task

    Public gblnDrugToDiseaseInteractionAlert As Boolean
    Public gblnDrugToFoodInteractionAlert As Boolean
    Public gblnDuplicateTherapyInteractionAlert As Boolean
    Public gblnAdverseDrugEffectAlert As Boolean

    Public Const DrugToDiseaseInteractionAlert As String = "DrugToDiseaseAlert"
    Public Const DrugToFoodInteractionAlert As String = "DrugToFoodAlert"
    Public Const DuplicateTherapyInteractionAlert As String = "DuplicateTherapyAlert"
    Public Const AdverseDrugEffectAlert As String = "AdverseDrugEffectAlert"

    Public gblnDrugAlertMsg As Boolean
    Public Const ShowDrugAlertMsg As String = "ShowDrugAlertMsg"

    '' Word setting 'Pramod 20070629
    ''By default Word is highlighted with yellow
    Public gblnWordColorHighlight As Boolean = True
    Public gblnWordBackColor As Int32 = 7
    Public gblnCoSignFlag As Boolean = False
    Public gblnExamSelection As Int32 = 1
    Public gblnPageNo As Boolean = True


    'Severity Level settings 
    Public gstrADESeverityLevel As String = ""
    Public gstrADEOnsetLevel As String = ""
    Public gstrDISeverityLevel As String = ""
    Public gstrDFASeverityLevel As String
    Public gstrDIDocLevel As String = ""
    Public gstrDFADocLevel As String
    Public gblnDFAAlert As Boolean
    Public Const ADESeverityLevel As String = "ADESeverityLevel"
    Public Const ADEOnsetLevel As String = "ADEOnsetLevel"

    Public Const DISeverityLevel As String = "DISeverityLevel"

    Public Const DFASeverityLevel As String = "DFASeverityLevel"
    Public Const DIDocLevel As String = "DIDocLevel"
    Public Const DFADocLevel As String = "DFADocLevel"

    Private _gstrRxReportpath As String

    Public gintNoOfAttempts As Integer = 0

    ''Added ServicesDatabaseName by Ujwala on 24022015 to get ServicesDB Name from settings table        
    Public gstrServicesDBName As String = ""
    Public gstrServicesServerName As String = ""
    Public gstrServicesUserID As String = ""
    Public gstrServicesPassWord As String = ""
    Public gbServicesIsSQLAUTHEN As Boolean
    ''Added ServicesDatabaseName by Ujwala on 24022015 to get ServicesDB Name from settings table        

    '' BY Mahesh , 20070423
    '''''<Patient Alert ForeColor>
    Public gnAlertForeColor As Long
    Public gnAlertBackColor As Long
    '''''<//Patient Alert ForeColor>

    Public gblnVisitSumSaveandClose As Boolean = False 'Added by manoj jadhav on 20140220 for MDM_T02 implementation
    Public gblnVisitSumSaveandFinish As Boolean = False 'Added by manoj jadhav on 20140220 for MDM_T02 implementation

    Public gblnSaveandClose As Boolean = False
    Public gblnSaveandFinish As Boolean = False
    Public gblnAddModPatient As Boolean = False
    'Public gblnGenerateOutboundMsg As Boolean = False
    Public gblnHL7SENDOUTBOUNDGLOEMR As Boolean = False

    Public gbInGENIUSSENDOUTBOUNDGLOEMR As Boolean = False
    Public gbInGeniusSaveClose As Boolean = False
    Public gbInGeniusSaveFinish As Boolean = False

    'Start- code added by kanchan on 20091202 for genius & Hl7 work simultaneously as case3176
    Public gblnSendChargesToGenius As Boolean = False
    'End- code added by kanchan on 20091202 for genius & Hl7 work simultaneously as case3176
    'Code Start-Added by kanchan on 20100521 for HL7 setting for immunization
    Public gblnSendImmunization As Boolean = False

    ''Added by Abhijeet on 20110919
    Public gblnSendHL7Appointment As Boolean = False
    ''End of changes by Abhijeet on 20110919


    Public gstrHL7MessagePath As String = ""

    Public gstrGeniusUsername As String = ""
    Public gstrGeniusPassword As String = ""
    Public gnGeniusICD9Driven As Int64 = 1

    Public gintPenWidth As Int32

    Public gblnBdayReminder As Boolean
    Public gnBDayReminderDays As Integer = 0

    '-------------------
    'used to show old dm alert 
    Public isOldDmAlertPresent As Boolean = False

    'Pramod 18022008 for surescriptAlert
    Public gblnSurescriptAlert As Boolean
    Public gStrSurescriptAlertmin As String  'gnSurescriptAlertmin
    Public Const SureScriptAlert As String = "SureScriptAlert"
    Public Const SureScriptAlertTime As String = "SureScriptAlertTime"
    Public gblnSurescriptEnabled As Boolean
    ''
    Public Const AddReferralsNote As String = "AddReferralsNote"
    Public gblnStagingServer As Boolean

    Public gblnAllowAddDrugs As Boolean
    Public gblnAllowDrugConfig As Boolean

    Public gblnDisableAllowSubstitution As Boolean?

    'Shubhangi 20090305'
    Public gstrChiefComplaintType As String = ""

    '20090306'
    Public gbOtherPatientType As Boolean

    Public gblIsDrugDBMigrated As Boolean = False
    Public gblIsNewInstallation As Boolean = False

    '-------------------
    'Supriya 10/06/2008 setting to enable disable fax button for Surescript enabled Pharmacy
    Public gblnIsFaxEnabled As Boolean
    Public Const SurescriptFaxSetting As String = "SurescriptFaxSetting"

    ''''Pramod 070711
    Public collCategoryDesc As Collection

    ' ''Mahesh - 20070723
    ' '' to Set the Record Level Lock
    Public gblnRecordLocking As Boolean = False
    ''
    'Rx/Med threshold setting
    Public gnThresholdSetting As Double
    Public Const Threshold As String = "Threshold"

    'Public gnSurcscriptMessageDate As String
    '' To Auto Update of Mesages 
    Public gMessageUpdateTime As Decimal = 5 '' Default Update Time is 5 Minutes
    '

    ''Decleared By Mahesh 20071019
    ''  TO SQL Exception message
    Public gstrSQLError As String = "Error while establishing connection with the server"
    ''

    Public gstrSignatureText As String = ""
    Public gstrCoSignatureText As String = ""

    Public gblnEnableCQMCypressTesting As Boolean = False

    '' in gloEMR 5.0
    Public gnClinicID As Int64 = 1
    Public gstrClinicName As String = ""
    Public gstrClinicState As String = ""
    Public gstrSiteID As String = ""
    Public gstrLocation As String = ""
    Public gstrAUSID As String = ""

    Public bClearDashboardSearch As Boolean = False

    Public bEnableLocalWelchAllynECGDevice As Boolean = False

    Private _gstrClinicPhone As String = ""
    Public Property gstrClinicPhone() As String
        Get
            Return _gstrClinicPhone
        End Get
        Set(ByVal value As String)
            _gstrClinicPhone = value
        End Set
    End Property

    Private _gstrClinicFax As String = ""
    Public Property gstrClinicFax() As String
        Get
            Return _gstrClinicFax
        End Get
        Set(ByVal value As String)
            _gstrClinicFax = value
        End Set
    End Property

    Private _gstrClinicZip As String = ""
    Public Property gstrClinicZip() As String
        Get
            Return _gstrClinicZip
        End Get
        Set(ByVal value As String)
            _gstrClinicZip = value
        End Set
    End Property

    Private _gstrClinicAddress As String = ""
    Public Property gstrClinicAddress() As String
        Get
            Return _gstrClinicAddress
        End Get
        Set(ByVal value As String)
            _gstrClinicAddress = value
        End Set
    End Property


    Private _gstrClinicCity As String = ""
    Public Property gstrClinicCity() As String
        Get
            Return _gstrClinicCity
        End Get
        Set(ByVal value As String)
            _gstrClinicCity = value
        End Set
    End Property

    ''By Saket 20080730
    Public gnPatientProviderID As Int64    

    Private _gnLoginProviderID As Int64
    Public Property gnLoginProviderID() As Int64
        Get
            Return _gnLoginProviderID
        End Get
        Set(ByVal value As Int64)
            _gnLoginProviderID = value
            Try
                Dim dtReturned As DataTable = Nothing
                Using prescriptionDBLayer As New gloEMRGeneralLibrary.gloEMRPrescription.RxBusinesslayer(0)
                    dtReturned = prescriptionDBLayer.GetPatientProviderDetails(_gnLoginProviderID)

                    If dtReturned IsNot Nothing AndAlso dtReturned.Rows.Count > 0 AndAlso dtReturned.Columns.Contains("sServiceLevel") Then
                        gstrProviderServiceLevel = dtReturned.Rows(0)("sServiceLevel")
                    End If

                End Using
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Prescription, gloAuditTrail.ActivityType.Load, "Error setting gstrProviderServiceLevel", 0, 0, _gnLoginProviderID, gloAuditTrail.ActivityOutCome.Failure)
            End Try
        End Set
    End Property

    Public ReadOnly Property IsCancelRxEnabledForProvider        
        Get
            Dim returned As Boolean = False
            Try
                If gstrProviderServiceLevel IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(gstrProviderServiceLevel) Then
                    If Mid(gstrProviderServiceLevel, 12, 1) = "1" Then
                        returned = True
                    End If
                End If
            Catch ex As Exception
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Prescription, gloAuditTrail.ActivityType.Load, "Error getting IsCancelRxEnabledForProvider", 0, 0, _gnLoginProviderID, gloAuditTrail.ActivityOutCome.Failure)
            End Try
            
            Return returned
        End Get
    End Property

    Public gstrProviderServiceLevel As String = ""

    Public gstrPatientProviderName As String = ""
    Public gstrLoginProviderName As String = ""
    ''Added by Mayuri:20100506
    Public gnSelectedProviderID As Int64
    ''sarika INTERNET FAX 
    Public gblnInternetFax As Boolean
    'login credentials for internet fax
    Public gstrEFaxUserID As String '= "817352"
    Public gstrEFaxUserPassword As String '= "glostream11"

    '----------sarika INTERNET FAX 
    Public gbIsProviderEPCSEnable As Boolean

    'sarika Show Printer Dialog Setting 20081004/ in gloEMR50 20081121
    Public gblnUseDefaultPrinter As Boolean = True

    'Local print setting added in gloEMR71 20160525
    Public gblnEnableLocalPrinter As Boolean = False
    Public gblnAddFooterInService As Boolean = False

    ''Sudhir 20081121 To show Patient age in Days according to given limit
    Public gblShowAgeInDays As Boolean = False
    Public gblAgeLimit As Int64
    'Public gblAgeLimitForWeeks As Int64     ''Not Required
    ''end sudhir

    '---
    Public gnDrugListButton As Int64


    Public Const strLabs As String = "Labs"
    Public Const strOrders As String = "Orders"
    Public Const strOtherDiagnosis As String = "OtherDiagnosis"
    Public Const strMangementOption As String = "ManagementOption"

    Public gnPatientSynopsisTabCount As Int64
    Public gstrVersion As String = "5.0.0.2"
    Public gstrBannerName As String = ""
    Public gstrMktngVersion As String


    Public gblnProviderDisable As Boolean = False

    'Genius Settings
    Public gstrGeniusCode As String = ""
    Public gstrGeniusPath As String = ""

    Public gblnSetCPTtoAllICD9 As Boolean
    Public gblnShowDXCPTScreenOnSmartDx As Boolean

    '' SUDHIR 20090827 '' EXAM DIAGNOSIS SETTINGS ''
    Public gblnICD9Driven As Boolean = True
    Public gblnIsVitalRequired As Boolean = False
    Public gblnIsICD9CPTRequired As Boolean = False

    Public gblnShowDragonWaitProcess As Boolean = False



    Public gblnShow8ICD9 As Boolean = True
    Public gblnShow4Modifier As Boolean = True
    Public gblnIsExamPTBillingEnabled As Boolean = False

    '' SUDHIR 2090828 '' 
    Public gblnMultipleSupervisorsforPaperRx As Boolean = True
    Public gblnPatientQuestionnaire As Boolean = False
    Public garrOpenDocument As New ArrayList
    'Shubhangi 20091003 For clear searct text box
    Public gblnResetSearchTextBox As Boolean
    ''Sandip Darade 20091007
    Public gstrCountry As String = ""
    'variable gblnAssociatedProviderSignature added by dipak 20100105 to store AssociatedProviderSignature right value
    Public gblnAssociatedProviderSignature As Boolean = False
    'COMMNTED BY SHUBHANGI 20110606
    'Public gEMExamType As AlphaII.CodeWizard.Objects.EvaluationManagement.EvalMgmtExamType

    ''Sandip Darade 20100118  BUG ID 5781
    Public gblnPastExam As Boolean

    ''Sandip Darade 2010213
    Public gblnIsSelectRefContact As Boolean = False '' will be used for batch referral fax

    Public gblnShowCheckedOutAppointment As Boolean = False
    Public gblnShowHealthPlan As Boolean = False

    'Public gFont As Font = New Font("Tahoma", 9, FontStyle.Regular)
    'Public gFont_SMALL As Font = New Font("Tahoma", 8.25!, FontStyle.Regular)
    'Public gFont_SMALL_BOLD As Font = New System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CType((0), Byte)))
    'Public gFont_BOLD As Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Public gstrExamTypes As String = ""
    Public gstrVisitTypes As String = ""

    'Public gFontArial_Bold As Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    'Public gFontArial_Regular As Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    'Public gFontArial_Big_Bold As Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    'Public gFontArial_Big_Regular As Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))

    'Public gFontVerdana_Bold As Font = New System.Drawing.Font("Verdana", 9, FontStyle.Bold)
    'Public gFontVerdana_Regular As Font = New System.Drawing.Font("Verdana", 9, FontStyle.Regular)

    ''''''''''Added by Ujwala - for Snomed Immunization  - as on 20101007
    Public gstrSMHistory As String = ""
    Public gstrSMImmunization As String = ""
    Public gstrSMProblem As String = ""
    ''''''''''Added by Ujwala - for Snomed Immunization  - as on 20101007
    ''Added Rahul for Speciality on 20101020
    Public gstrSpeciality As String = ""
    ''End
    Public gblnExtemptFromReport As Boolean
    ''Start'GLO2010-0007047[BJMC]: Webcam image too small
    ' Private _myPictureBoxControl() As Byte = Nothing ''gloPicutreBoxControl
    'Private _iPhoto As System.Drawing.Image = Nothing

    'Recover Exam Module change
    Public gBlnEnableRecoverExam As Boolean = False
    Public gblnShowSmokingColumn As Boolean = False
    Public gblnCDSRights As Boolean = False

    ' gloCommunity variables added on 02-jan-2012  by chetan for gloCommunity
    Public gblngloCommunity As Boolean = False
    Public gblnIscommunityStaging As Boolean = True
    Public gstrCCDAViewerURL As String = ""
    Public gstrCCDAImportCategory As String = ""
    '''''''''"http://tfs05:5020/HealthCare/"
    Public WebFolder As String = "Masters"
    Public ClinicWebFolder As String = "Templates"
    Public WebSite As String = ""
    Public ClinicRepository As String = "Repository" ''"Clinical Repository"
    Public GlobalRepository As String = "Global Repository"
    Public ClinicXmlFolder As String = "DataConnections"
    Public WebGlobalXmlFolder As String = "Global Data Connections" ''"Global Association Repository"
    Public WebUserXmlFolder As String = "Smart Association Connection"
    Public gstrCommsrv As String = "" ''"dev110:26078"
    Public gstrsmdxflnm As String = "SmartDxAssociation"
    Public gstrLiquidDataFNm As String = "LiquidData"
    Public gstrSmartCPTflnm As String = "SmartCPTAssociation"
    Public gstrSmartOrderflnm As String = "SmartOrderAssociation"
    Public gstrHistoryflnm As String = "History"
    ''Added for Sharepoint server setting
    Public gstrSharepointSrvNm As String = ""
    Public gstrSharepointSiteNm As String = ""
    Public gstrVti_Bin As String = "_vti_bin"
    Public gstrListSvc As String = "lists.asmx"
    Public gstrSiteDataSvc As String = "sitedata.asmx"
    Public gstr_Layouts As String = "_layouts"
    Public IsCommunityLiquidData As Boolean = False
    Public IsCommunitySmartDx As Boolean = False
    Public Webpath As String = "http://" + gstrSharepointSrvNm + "/" + gstrSharepointSiteNm + "/"
    Public gstrTskMlflnm As String = "TaskMail"
    Public gstrIMSetupflnm As String = "IMSetup"
    Public gstrCVSetupflnm As String = "CVSetup"
    Public gstrblconfflnm As String = "BillingConfiguration"
    Public gstrappconfflnm As String = "AppointmentConfiguration"
    Public gstrflowshflnm As String = "Flowsheet"
    Public gstrformglry As String = "Formgallery"
    Public gstrDmSetupflnm As String = "DmSetup"
    ''Added for gloCommunity Form authentication on 20120730
    Public gstrgloCommunityAuthentication As String = ""
    Public gstrAuthenticationWSAddress As String = ""
    Public gblnIsShareUserRights As Boolean = False
    ''Public gstrGCUserName As String = ""
    ''Public gstrGCPassword As String = ""
    ''End

    Public gstrHoosKoosSurveyUrl As String = ""
    Public PDMPServiceURL As String = ""
    Public PDMPUsername As String = ""
    Public PDMPPassword As String = ""
    Public gstrServiceNamespace As String = ""
    Public gstrDomainName As String = ""
    Public gstrCommunityWebUrl As String = ""
    Public gstrMgmtServiceReply As String = ""
    Public gstrADFSServer As String = ""
    Public gstrSharePointAutthPage As String = ""
    Public gstrSharePointSiteReply As String = ""
    Public gstrACSRelyingPartyurl As String = ""
    Public gstrRxEligThresholdvalue As String = ""

    Public gblnIntuitCommunication As Boolean
    Public gblnIncludeMessageInReply As Boolean
    Public gblnIncludeFrequencyAbbrevationInRxMeds As Boolean = False
    Public gstrCodeFieldsinHistory As String = ""
    Public gstrCodeFieldsinOBPlan As String = ""
    Public gblnsendcdafinishExam As Boolean = False
    Public gblnPromptProviderForCDASend As Boolean = False
    Public globlnEnableMultipleRaceFeatures As Boolean

    Public gblnIsSecureStagingsever As Boolean = True
    Public gblnIsSecureMsgEnable As Boolean = False
    Public gstrSecureStagingUrl As String = ""
    Public gstrSecureProductionUrl As String = ""

    Public gstrMedHXStagingUrl As String = ""
    Public gstrMedHXProductionUrl As String = ""

    Public gblnIsReferalNoteadd As String = False
    Public gblnSecureUserrights As String = False
    Public strProviderDirectAddress As String = ""
    Public gblnRemovePatientCurrentData As Boolean
    Public gbShowDMAlert As Boolean = True
    Public gbShowviewRecommendation As Boolean = True
    ''Added for MU2 Patient portal implementation on 20130702
    Public gblnUSEINTUITINTERFACE As Boolean
    Public gblnPatientPortalEnabled As Boolean
    Public gblnPatientPortalAutoCompleteTaskEnabled As Boolean = False
    Public gblnPatientPortalPFAutoCompleteTaskEnabled As Boolean = False
    Public gstrPatientPortalSiteNm As String
    Public gstrPatientPortalEmailService As String
    ''End
    Public gblnPatRegIntuitorPortal As Boolean = False  '' added to turn on if patient portal or intuit setting is on and patient is registered 
    Public gblnEducationMaterialMappingEnabled As Boolean
    Public gblnEducationMaterialEnabled As Boolean
    Public gblnAdvancedReferenceEnabled As Boolean

    Public gblnIcd10Transition As Boolean
    Public gdtIcd10Transition As Date

    Public gblnIcd10MasterTransition As Boolean
    Public gblnIsOTCIssueWarningEnabled As Boolean = True

    Public gblnEpcsEnabled As Boolean = False
    Public gblnAllowPrintForCSEnabled As Boolean = True

    Public gstrVendorName As String = ""
    Public gstrVendorLabel As String = ""
    Public gstrVendorNodeName As String = ""
    Public gstrVendorNodeLabel As String = ""
    Public gstrEpcsUrl As String = ""
    Public gstrSharedSecret As String = ""
    Public gstrRouterName As String = ""

    Public gblnIsPatientSavingEnabled As Boolean = True
    Public gblnIsPatientSavingRights As Boolean = True
    Public gblnEnablePatientSavingsInbox As Boolean = True
    Public gblnIsPharymacyInclude As Boolean = True  '' Enable by Default
    Public gblnIsCustomizeReport As Boolean = False
    Public gstrMultipleRxCustomizeReport As String = ""
    Public gstrSingleRxCustomizeReport As String = ""
    Public gblnIsOBSpecialityEnabled As Boolean = False
    Public gstrAdvChartLicensekey As String = ""
    Public gstrAdvChartExtLicensekey As String = ""
    Public gblnAutoCompDMSTask As Boolean = False ''settings added for autocomplete dms task and autoclose task window while opening dms screen from task
    Public gblnAutoCloseDMSWindow As Boolean = False
    ''Added on 20151124-Added SaveLiquidData setting to check whether to save liquid Data or not for optimization
    Public gblnSAVELIQUIDDATA As Boolean
    Public gblnLOCKDATEFIELD As Boolean
    Public gblbUseNewSignaturePad As Boolean = False

    Private _gstrEPAServiceURL As String
    Private _gstrEPAAPIURL As String

    Private _QPPServiceURL As String
    Public Property gstrQPPServiceURL() As String
        Get
            Return _QPPServiceURL
        End Get
        Set(ByVal value As String)
            _QPPServiceURL = value
        End Set
    End Property


#Region "Centralized DIB and Formulary Service Settings"

    Private _gstrFormularyServiceURL As String
    Private _gstrDIBServiceURL As String = String.Empty
    Public Property gstrDrugInteractionServiceURL() As String


#Region "PDR"
    Private _gstrPDR_URL As String
    Private _gstrPDR_PC_PortalID As String
    Private _gstrPDR_PC_PFormat As String
    Private _SurescriptServiceURL As String = ""
    Private _gbnlPDR_EnableSerialization As Boolean = False

    Public Property gbnlPDR_EnableSerialization() As Boolean
        Get
            Return _gbnlPDR_EnableSerialization
        End Get
        Set(ByVal value As Boolean)
            _gbnlPDR_EnableSerialization = value
        End Set
    End Property
    Public Property gstrSurescriptServiceURL() As String
        Get
            Return _SurescriptServiceURL
        End Get
        Set(ByVal value As String)
            _SurescriptServiceURL = value
        End Set
    End Property
    Public Property gstrPDR_URL() As String
        Get
            Return _gstrPDR_URL
        End Get
        Set(ByVal value As String)
            _gstrPDR_URL = value
        End Set
    End Property

    Public Property gstrPDR_PC_PFormat() As String
        Get
            Return _gstrPDR_PC_PFormat
        End Get
        Set(ByVal value As String)
            _gstrPDR_PC_PFormat = value
        End Set
    End Property

    Public Property gstrPDR_PC_PortalID() As String
        Get
            Return _gstrPDR_PC_PortalID
        End Get
        Set(ByVal value As String)
            _gstrPDR_PC_PortalID = value
        End Set
    End Property
#End Region

    Public Property gstrEPAServiceURL() As String
        Get
            Return _gstrEPAServiceURL
        End Get
        Set(ByVal value As String)
            _gstrEPAServiceURL = value
        End Set
    End Property

    Public Property gstrEPAAPIURL() As String
        Get
            Return _gstrEPAAPIURL
        End Get
        Set(ByVal value As String)
            _gstrEPAAPIURL = value
        End Set
    End Property


    Public Property gstrFormularyServiceURL() As String
        Get
            Return _gstrFormularyServiceURL
        End Get
        Set(ByVal value As String)
            _gstrFormularyServiceURL = value
            gloEMRGeneralLibrary.gloGeneral.clsgeneral.gstrFormularyServiceURL = _gstrFormularyServiceURL
        End Set
    End Property

    Public Property gstrDIBServiceURL() As String
        Get
            Return _gstrDIBServiceURL
        End Get
        Set(ByVal value As String)
            _gstrDIBServiceURL = value
            'TODO: Add gloEMRGeneralLibrary.gloGeneral.clsgeneral Service URL Setting here.
        End Set
    End Property

#End Region

    '17-Jul-14 Aniket: Resolving Bug #70972

    ' ''gloCommunity variables added on 02-jan-2012
    'Public Property MyPictureBoxControl() As Byte()
    '    Get
    '        Return _myPictureBoxControl
    '    End Get
    '    Set(ByVal value As Byte())
    '        _myPictureBoxControl = value
    '    End Set
    'End Property
    'Public Property PatientPhoto() As System.Drawing.Image
    '    Get
    '        Return _iPhoto
    '    End Get
    '    Set(ByVal value As System.Drawing.Image)
    '        If (IsNothing(_iPhoto) = False) Then
    '            _iPhoto.Dispose()
    '        End If
    '        If (IsNothing(value) = False) Then
    '            _iPhoto = CType(value.Clone(), Image)
    '        Else
    '            _iPhoto = Nothing
    '        End If


    '    End Set
    'End Property
    ''End'GLO2010-0007047[BJMC]: Webcam image too small
#Region "EM Variables"
    Public Const EMLymphatic = "Lymphatic" 'Lymphatic *requires palpation of two or more nodes per area 
    Public Const EMMusculoskeletalSpine = "MusculoskeletalSpine" 'Musculoskeletal-Spine, Ribs and Pelvis 
    Public Const EMMusculoskeletalupperR = "MusculoskeletalupperR" 'Musculoskeletal-Right upper extremity 
    Public Const EMMusculoskeletalupperL = "MusculoskeletalupperL" 'Musculoskeletal-Left upper extremity 
    Public Const EMMusculoskeletallowerR = "MusculoskeletallowerR" 'Musculoskeletal-Right lower extremity
    Public Const EMMusculoskeletallowerL = "MusculoskeletallowerL" 'Musculoskeletal-Left lower extremity

#End Region
    '''' Managment Option
    Public Const MagRespiratoryTreatments = "RespTreat" '"RespiratoryTreatments"
    Public Const MagMinorSurgeryWRiskFactors = "MinSurgWRisk" 'MinorSurgeryWRiskFactors 
    Public Const MagMajorSurgeryWRiskFactors = "MajSurWRisk" '"MajorSurgeryWRiskFactors"
    Public Const MagMajorEmergencySurgery = "MagEmerSur" 'MajorEmergencySurgery
    Public Const MagDecisionNotResuscitate = "DecNResuc" 'DecisionNotResuscitate
    Public Const MagDecisionObtainMedicalRecsOther = "DecisObtaMedResc" 'DecisionObtainMedicalRecsOther
    Public Const MagReviewMedicalRecsOther = "RevMedResc" 'ReviewMedicalRecsOther
    Public Const MagDiscussCaseWHealthProvider = "DiscCWHealPro" 'DiscussCaseWHealthProvider




    '''' Labs
    Public Const LabFluStrepMonoRoutine = "FluStrepMono" 'FluStrepMonoRoutine
    Public Const LabPregnancyTestRoutine = "PregnancyTest" 'PregnancyTestRoutine
    Public Const LabBunCreatinineRoutine = "BunCreatinine" 'BunCreatinineRoutine
    Public Const LabElectrolytesRoutine = "Electrolytes" 'ElectrolytesRoutine
    Public Const LabChemicalProfileRoutine = "ChemicalProfile" 'ChemicalProfileRoutine
    Public Const LabCardiacEnzymesRoutine = "CardiacEnzymes" 'CardiacEnzymesRoutine
    Public Const LabTypeCrossmatchRoutine = "TypeCrossmatch" 'TypeCrossmatchRoutine
    Public Const LabSuperficialBiopsyRoutine = "SuperficialBiopsy" 'SuperficialBiopsyRoutine
    Public Const LabIncisionalBiopsyRoutine = "IncisionalBiopsy" 'IncisionalBiopsyRoutine
    Public Const LabIndependentVisualTest = "IndependentVisual" 'IndependentVisualTest
    Public Const LabDiscussionWPerformingPhys = "DiscWPerfPhys" 'DiscussionWPerformingPhys

    '''' XRay/Radiology

    Public Const XDiagUltrasoundRoutine = "DiagUltrasound" 'DiagUltrasoundRoutine
    Public Const XGIGallbladderRoutine = "GIGallbladder" 'GIGallbladderRoutine
    Public Const XVascularStudiesRoutine = "VascularStudies" 'VascularStudiesRoutine
    Public Const XVascularStudiesWRiskRoutine = "VascularStuWRisk" 'VascularStudiesWRiskRoutine
    Public Const XIndependentVisualTest = "IndependentVisual" 'IndependentVisualTest
    Public Const XDiscussWPerformingPhys = "DiscWPerfPhys" 'DiscussWPerformingPhys

    '''' Other Diagnostics Test

    Public Const OHolterMonitorRoutine = "HolterMonitor" 'HolterMonitorRoutine
    Public Const OTreadmillStressTestRoutine = "TremillStress" 'TreadmillStressTestRoutine
    Public Const OVectorcardiogramRoutine = "Vectorcardiogram" 'VectorcardiogramRoutine
    Public Const ODopplerFlowStudiesRoutine = "DopplerFlowStud" 'DopplerFlowStudiesRoutine
    Public Const OPulmonaryStudiesRoutine = "PulmonaryStudies" 'PulmonaryStudiesRoutine
    Public Const OLumbarPunctureRoutine = "LumbarPuncture" 'LumbarPunctureRoutine
    Public Const OThoracentesisRoutine = "Thoracentesis" 'ThoracentesisRoutine
    Public Const OCuldocentesesRoutine = "Culdocenteses" 'CuldocentesesRoutine
    Public Const OEndoscopeWRiskRoutine = "EndoscopeWRisk" 'EndoscopeWRiskRoutine
    Public Const OIndependentVisualTest = "IndependentVisual" 'IndependentVisualTest
    Public Const ODiscussWPerformingPhys = "DiscWPerfPhys" 'DiscussWPerformingPhys

    Public Property IsExamAutoSaveEnable As Boolean = False
    Public Property ExamAutoSaveTime As Int32 = 1


    Enum DrugListDefault
        AllDrugs = 11
        PracticeFavorites = 12
        FrequentlyUsed = 13
        ProviderFrequentlyUsed = 20
        ProviderDrugs
        ClassfiedDrugs = 22
    End Enum


    'gloEMR Main Exception Class
    Public Enum gloEMRExceptionActorType
        General = 0
        WordDocument = 1
        Voice = 2
        Scan = 3
        DMS = 4
        CrystalReports = 5
        PrintFax = 6
        HL7 = 7
        WelchAllyn = 8
        Video = 9
        VMS = 10
        Unknown = 11
    End Enum

    '' By Mahesh 
    '' to Sort the Dates in following Categories
    Public Enum DateCategory
        None = 0
        Today = 1
        Yesterday = 2
        LastWeek = 3
        LastMonth = 4
        Older = 5
    End Enum

    Public Enum PatientAssociatType
        Exam = 1
        Radiology = 2
        Labs = 3
        ScanDocument = 4
    End Enum

    'By Pramod 
    'For Disclosure set
    Public Enum DisclosureSet
        None = 0
        PatientDemographics = 1
        History = 2
        Medication = 3
        PatientExam = 4
        Labs = 5
        Orders = 6
        DMS = 7
        Consent = 8
        Flowsheet = 9
        Immunization = 10
        Messages = 11
        Vitals = 12
    End Enum

    '''' Pramod E&M coding Start
    Public Enum ControlType
        None = 0
        CheckBox = 1
        Text = 2
    End Enum


    Public Enum CategoryType
        None = 0
        General = 1
        Hitory = 2
        Physical_Examination = 3
        Medical_Decision_Making = 4
        HPI = 5
        Management_option = 6
        Labs = 7
        X_Ray_Radiology = 8
        Other_Diagonsis_Tests = 9
        ROS = 10
        DB_History = 11
    End Enum

    Public Enum FieldType
        None = 0
        Labs = 1
        Radiology = 2
        FlowSheet = 3
        Tags = 4
    End Enum

    'Enum related to EMExamtype
    Public Enum enumExamControlType
        None = 0
        GeneralMultiSystem = 1
        Cardiovascular = 2
        EarsNoseThroat = 3
        Eye = 4
        Genitourinary = 5
        HemaLymphImmuno = 6
        Musculoskeletal = 7
        Neurological = 8
        Psychiatric = 9
        Respiratory = 10
        Skin = 11
        Pre97Guidelines = 12
    End Enum

    Public DICOMPath As String = ""
    Public gnBufferSize As Integer = 0


    Public Function DisclosureSetNames(ByVal enumSetType As DisclosureSet) As String

        Select Case enumSetType
            Case DisclosureSet.PatientDemographics
                Return "Patient Demographics"
            Case DisclosureSet.History
                Return "History"
            Case DisclosureSet.Medication
                Return "Medication"
            Case DisclosureSet.PatientExam
                Return "Patient Exam"
            Case DisclosureSet.Labs
                Return "Labs"
            Case DisclosureSet.Orders
                Return "Orders"
            Case DisclosureSet.DMS
                Return "DMS"
            Case DisclosureSet.Consent
                Return "Consent"
            Case DisclosureSet.Messages
                Return "Messages"
            Case DisclosureSet.Immunization
                Return "Immunization"
            Case DisclosureSet.Flowsheet
                Return "Flowsheet"
            Case DisclosureSet.Vitals
                Return "Vitals"
            Case DisclosureSet.None
                Return ""
            Case Else
                Return ""
        End Select
    End Function

    Public Function GetPatientInfo(ByVal patientid As Int64) As DataTable
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oParameters As New gloDatabaseLayer.DBParameters()
        Dim dt As DataTable = Nothing

        Try
            oDB.Connect(False)
            'Get the Patient Demographic Details for dashboard.
            oParameters.Add("@PatientID", patientid, ParameterDirection.Input, SqlDbType.BigInt)
            oDB.Retrive("gsp_PatientInfo", oParameters, dt)
            Return dt
        Catch dbEx As gloDatabaseLayer.DBException
            dbEx.ERROR_Log(dbEx.ToString())
            Return Nothing
        Catch ex As Exception
            MessageBox.Show("ERROR : " & ex.ToString(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return Nothing
        Finally
            'If Not IsNothing(dt) Then
            '    dt.Dispose()
            '    dt = Nothing
            'End If
            If Not IsNothing(oParameters) Then
                oParameters.Dispose()
                oParameters = Nothing
            End If
            If oDB IsNot Nothing Then
                oDB.Disconnect()
            End If
            If oDB IsNot Nothing Then
                oDB.Dispose()
                oDB = Nothing
            End If
        End Try
    End Function

    Public Function GetLabTaskProvider(ByVal ProviderId As Long) As DataTable
        'Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101013
        Dim strQRY As String
        Try
            Dim conn As New SqlConnection(GetConnectionString)
            Dim sda As SqlDataAdapter
            Dim dt As New DataTable
            strQRY = "SELECT ProviderSettings.nProviderID , isnull(Provider_MST.sFirstName,'')+space(1)+ CASE isnull(Provider_MST.sMiddleName,'') WHEN  '' THEN '' When isnull(Provider_MST.sMiddleName,'') then   Provider_MST.sMiddleName + SPACE(1) END +isnull(Provider_MST.sLastName,'') as ProviderName  " _
                      & " FROM ProviderSettings INNER JOIN " _
                      & "Provider_MST ON ProviderSettings.nProviderID = Provider_MST.nProviderID WHERE Provider_MST.nProviderID = " & ProviderId & "  AND ProviderSettings.sSettingsType = 'LabUser'"
            sda = New SqlDataAdapter(strQRY, conn)
            sda.Fill(dt)
            sda.Dispose()
            conn.Close()
            conn.Dispose()
            Return dt

        Catch ex As Exception
            Return Nothing
        Finally

        End Try
    End Function

    Public Function WriteExceptionLog(ByVal _Exception As System.Exception, ByVal _gloEMRExceptionActor As gloEMRExceptionActorType) As String
        '// WELCOME TO MODIFY MESSAGES.....BUT PLEASE DISCUSS BEFORE MODIFY...VINAYAK, SUPRIYA

        Dim _Result As String = ""
        Try
            '//Write Developer Message into Log File//
            Dim objFile As New System.IO.StreamWriter(Application.StartupPath & "\gloEMRException.log", True)

            Dim _StringToWrite As String = "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~" & vbCrLf
            _StringToWrite = _StringToWrite & System.DateTime.Now & ":" & System.DateTime.Now.Millisecond & vbCrLf
            _StringToWrite = _StringToWrite & "------------------------" & vbCrLf
            _StringToWrite = _StringToWrite & _gloEMRExceptionActor.ToString & "------------------------" & vbCrLf
            _StringToWrite = _StringToWrite & "------------------------" & vbCrLf
            _StringToWrite = _StringToWrite & "Error Message: " & _Exception.Message & vbCrLf
            _StringToWrite = _StringToWrite & "Error Path: " & _Exception.StackTrace & vbCrLf
            _StringToWrite = _StringToWrite & vbCrLf & "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~"
            objFile.WriteLine(_StringToWrite)
            objFile.Close()

            If Not IsNothing(objFile) Then
                objFile.Dispose()
                objFile = Nothing
            End If

            'Find Message
            If _Exception.GetType Is GetType(ArgumentNullException) Then
                '<<<IO>>>
                _Result = "Invalid operation performed by process."
            ElseIf _Exception.GetType Is GetType(ArgumentOutOfRangeException) Then
                '<<<IO>>>
                _Result = "Invalid operation performed by process."
            ElseIf _Exception.GetType Is GetType(DivideByZeroException) Then
                '<<<IO>>>
                _Result = "Invalid operation performed by process."
            ElseIf _Exception.GetType Is GetType(OverflowException) Then
                '<<<IO>>>
                _Result = "Invalid Operation during arithmetic calculation."
            ElseIf _Exception.GetType Is GetType(SqlException) Then
                '<<<IO>>>
                _Result = "There is problem with database, please contact database administrator or system administrator."
            ElseIf _Exception.GetType Is GetType(ObjectDisposedException) Then
                '<<<IO>>>
                _Result = "Invalid operation occurred during termination of process."
            ElseIf _Exception.GetType Is GetType(DirectoryNotFoundException) Then
                '<<<IO>>>
                _Result = "Directory does not exists."
            ElseIf _Exception.GetType Is GetType(DriveNotFoundException) Then
                '<<<IO>>>
                _Result = "Drive does not exists."
            ElseIf _Exception.GetType Is GetType(FileNotFoundException) Then
                '<<<IO>>>
                _Result = "File does not exists."
            ElseIf _Exception.GetType Is GetType(FileLoadException) Then
                '<<<IO>>>
                _Result = "Unable to load file."
            ElseIf _Exception.GetType Is GetType(PathTooLongException) Then
                '<<<IO>>>
                _Result = "File Path too long."
            ElseIf _Exception.GetType Is GetType(EndOfStreamException) Then
                '<<<IO>>>
                _Result = "Unable to read data from file, please check if file is corrupted."
            ElseIf _Exception.GetType Is GetType(Runtime.InteropServices.COMException) Then
                '<<<IO>>>
                _Result = "Illegal operation performed by componenent, please contact system administrator."
            ElseIf _Exception.GetType Is GetType(DllNotFoundException) Then
                '<<<IO>>>
                _Result = "Invalid operation performed by process."
            ElseIf _Exception.GetType Is GetType(EntryPointNotFoundException) Then
                '<<<IO>>>
                _Result = "Invalid operation performed by process."
            ElseIf _Exception.GetType Is GetType(ArgumentException) Then
                '<<<IO>>>
                _Result = "Invalid operation performed by process."
            ElseIf _Exception.GetType Is GetType(ArithmeticException) Then
                '<<<IO>>>
                _Result = "Invalid operation performed by process."
            ElseIf _Exception.GetType Is GetType(ArrayTypeMismatchException) Then
                '<<<IO>>>
                _Result = "Invalid operation performed by process."
            ElseIf _Exception.GetType Is GetType(IndexOutOfRangeException) Then
                '<<<IO>>>
                _Result = "Invalid operation performed by process."
            ElseIf _Exception.GetType Is GetType(OutOfMemoryException) Then
                '<<<IO>>>
                _Result = "System running out of memory."
            ElseIf _Exception.GetType Is GetType(InvalidCastException) Then
                '<<<IO>>>
                _Result = "Invalid operation performed by process."
            ElseIf _Exception.GetType Is GetType(MemberAccessException) Then
                '<<<IO>>>
                _Result = "Invalid operation performed by process."
            ElseIf _Exception.GetType Is GetType(NotSupportedException) Then
                '<<<IO>>>
                _Result = "Invalid operation performed by user."
            ElseIf _Exception.GetType Is GetType(NullReferenceException) Then
                '<<<IO>>>
                _Result = "Invalid operation performed by process."
            ElseIf _Exception.GetType Is GetType(RankException) Then
                '<<<IO>>>
                _Result = "Invalid operation performed by process."
            ElseIf _Exception.GetType Is GetType(TimeoutException) Then
                '<<<IO>>>
                _Result = "Time out performing requested operation."
            ElseIf _Exception.GetType Is GetType(InternalBufferOverflowException) Then
                '<<<IO>>>
                _Result = "Unable to process, file is too large."
            Else
                '<<<OTHER>>>
                _Result = "Illegal operation performed by process, please contact system administrator"
            End If

            Return _Result
        Catch ex As Exception
            MessageBox.Show("Unable to write to error log", gstrMessageBoxCaption, MessageBoxButtons.OK)
            Return Nothing
        End Try
    End Function

    Public Function GetImage() As Image
        Dim img As System.Drawing.Image = Nothing
        Try
            If Clipboard.ContainsImage Then
                img = Clipboard.GetImage()
            End If

        Catch ex As Exception
            MessageBox.Show("In General, unable to get image from Clipboard due to locked by " & gloWord.gloWord.GetOpenClipboardWindowText(), gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)

        End Try

        Return img
    End Function

    Public Function WriteCrystalExceptionLog(ByVal _Exception As System.Exception) As String

        '// WELCOME TO MODIFY MESSAGES........VINAYAK, SUPRIYA

        Dim _Result As String = ""
        Try
            '//Write Developer Message into Log File//
            Dim objFile As New System.IO.StreamWriter(Application.StartupPath & "\gloEMRException.log", True)

            objFile.WriteLine(System.DateTime.Now & ":" & System.DateTime.Now.Millisecond & vbTab & " Error Message: " & _Exception.Message & vbTab & " Error Path: " & _Exception.StackTrace)
            objFile.Close()

            If Not IsNothing(objFile) Then
                objFile.Dispose()
                objFile = Nothing
            End If

            'Find Message
            If _Exception.GetType Is GetType(CrystalDecisions.CrystalReports.Engine.LogOnException) Then
                '<<<IO>>>
                _Result = "Unable to load report."
            ElseIf _Exception.GetType Is GetType(CrystalDecisions.CrystalReports.Engine.ParameterFieldException) Then
                '<<<IO>>>
                _Result = "Unable to load report."
            ElseIf _Exception.GetType Is GetType(CrystalDecisions.CrystalReports.Engine.DataSourceException) Then
                '<<<IO>>>
                _Result = "Unable to load report."
            ElseIf _Exception.GetType Is GetType(CrystalDecisions.CrystalReports.Engine.InvalidArgumentException) Then
                '<<<IO>>>
                _Result = "Invalid operation performed by process."
            ElseIf _Exception.GetType Is GetType(CrystalDecisions.CrystalReports.Engine.PrintException) Then
                '<<<IO>>>
                _Result = "Unable to print report."
            ElseIf _Exception.GetType Is GetType(CrystalDecisions.CrystalReports.Engine.OutOfLicenseException) Then
                '<<<IO>>>
                _Result = "Please check license expiry."
            ElseIf _Exception.GetType Is GetType(CrystalDecisions.CrystalReports.Engine.EngineException) Then
                '<<<IO>>>
                _Result = "Invalid operation performed by process."
            Else
                '<<<OTHER>>>
                _Result = "Illegal operation performed by process, Contact system administrator"
            End If

            Return _Result
        Catch ex As Exception
            MessageBox.Show("Unable to write to error log", gstrMessageBoxCaption, MessageBoxButtons.OK)
            Return Nothing
        End Try
    End Function

    Public Function GetConnectionString(ByVal strSQLServerName As String, ByVal strDatabase As String, ByVal isSQLAuthentication As Boolean, ByVal sUserName As String, ByVal sPassword As String) As String
        Dim strConnectionString As String
        Try
            If isSQLAuthentication = False Then
                strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";Integrated Security=SSPI"
            Else
                strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";User ID=" & sUserName & ";Password=" & sPassword & ""
            End If
            ConnectionString = strConnectionString
            Return strConnectionString
        Catch ex As Exception
            Return Nothing
        Finally
            strConnectionString = Nothing

        End Try
    End Function

    Public Function GetConnectionString() As String
        Return GetConnectionString(gstrSQLServerName, gstrDatabaseName, gblnSQLAuthentication, gstrSQLUserEMR, gstrSQLPasswordEMR)
    End Function

    '-- Added By Rahul Patel on 26-10-2010
    '-- For DMS Connection String --'
    Public Function GetDMSConnectionString(ByVal strSQLServerName As String, ByVal strDatabase As String, ByVal isSQLAuthentication As Boolean, ByVal sUserName As String, ByVal sPassword As String) As String
        Dim strConnectionString As String
        If isSQLAuthentication = False Then
            strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";Integrated Security=SSPI"
        Else
            strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";User ID=" & sUserName & ";Password=" & sPassword & ""
        End If
        Return strConnectionString
    End Function

    Public Function GetDMSConnectionString() As String
        Return GetDMSConnectionString(gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsServerName, gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsDatabaseName, gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsIsSqlAuthentication, gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsUserId, gloEMRGeneralLibrary.gloGeneral.clsgeneral.sDmsPassword)
    End Function

    Public Function GetHybridConnectionString(ByVal strSQLServerName As String, ByVal strDatabase As String, ByVal isSQLAuthentication As Boolean, ByVal sUserName As String, ByVal sPassword As String) As String
        Dim strConnectionString As String
        If isSQLAuthentication = False Then
            strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";Integrated Security=SSPI"
        Else
            strConnectionString = "SERVER=" & strSQLServerName & ";DATABASE=" & strDatabase & ";User ID=" & sUserName & ";Password=" & sPassword & ""
        End If

        Return strConnectionString
    End Function

    Public Function RetrieveVersion() As String
        Dim objVer As Version = gloEMR.My.Application.Info.Version
        Return objVer.ToString
    End Function

    '<Vinayak - Health Plan Find Date - 2007 02 26>
    Public Enum HealthPlanDuration
        None = 0
        Day = 1
        Week = 2
        Month = 3
    End Enum

    Public Function FindHealthPlanDate(ByVal Duration As String, ByVal OnSetDate_mmddyyyy As Date) As Date
        Dim _HealthPlanDuration As HealthPlanDuration = HealthPlanDuration.None
        Dim _NoOfDaysAdd As Double = 0
        Dim _Result As Date

        If Duration.Trim <> "" Then
            'Find Duration Period
            Select Case Duration
                Case "1 Day", "2 Days", "3 Days", "4 Days", "5 Days", "6 Days"
                    _HealthPlanDuration = HealthPlanDuration.Day
                Case "1 Week", "2 Weeks", "3 Weeks", "4 Weeks"
                    _HealthPlanDuration = HealthPlanDuration.Week
                Case "1 Month", "2 Months", "3 Months"
                    _HealthPlanDuration = HealthPlanDuration.Month
            End Select

            _NoOfDaysAdd = Val(Duration)
            Select Case _HealthPlanDuration
                Case HealthPlanDuration.Day
                    _Result = DateAdd(DateInterval.Day, _NoOfDaysAdd, OnSetDate_mmddyyyy)
                Case HealthPlanDuration.Week
                    '_Result = DateAdd(DateInterval.Weekday, _NoOfDaysAdd, OnSetDate_mmddyyyy)
                    _Result = DateAdd(DateInterval.WeekOfYear, _NoOfDaysAdd, OnSetDate_mmddyyyy)
                Case HealthPlanDuration.Month
                    _Result = DateAdd(DateInterval.Month, _NoOfDaysAdd, OnSetDate_mmddyyyy)
            End Select

        End If

        Return _Result

    End Function


    '<Vinayak- Add Empty Temporary Folders function in gloEMR - 27 Dec 2005>
    Public Sub EmptyGloEMRTemp()
        'Empty Multiple Scan Document
        On Error Resume Next
        Dim _MultipleImageBaseFolder As String = gstrgloEMRStartupPath & "\" & gMultipleScanImagesFolder
        If System.IO.Directory.Exists(_MultipleImageBaseFolder) = True Then
            Dim oRootFolder As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(_MultipleImageBaseFolder)
            Dim oFolders As System.IO.DirectoryInfo() = oRootFolder.GetDirectories()
            Dim oFolder As System.IO.DirectoryInfo
            For Each oFolder In oFolders
                System.IO.Directory.Delete(oFolder.FullName, True)
            Next
            oFolders = Nothing
            oFolder = Nothing
        End If
        'Empty Acquire temporary scanning images
        Dim _ScanFolder As String = gstrgloEMRStartupPath & "\Acquire Image"
        If System.IO.Directory.Exists(_ScanFolder) = True Then
            Dim oRootFolder As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(_ScanFolder)
            Dim oFiles As System.IO.FileInfo() = oRootFolder.GetFiles()
            Dim oFile As System.IO.FileInfo
            For Each oFile In oFiles
                Kill(oFile.FullName)
            Next
            oFiles = Nothing
            oFile = Nothing
        End If
        'Empty Exam Temp Files
        Dim _ExamTempFolder As String = gloSettings.FolderSettings.AppTempFolderPath
        If System.IO.Directory.Exists(_ExamTempFolder) = True Then
            'Dim oRootFolder As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(_ExamTempFolder)
            'Dim oFiles As System.IO.FileInfo() = oRootFolder.GetFiles()
            'Dim oFile As System.IO.FileInfo
            'For Each oFile In oFiles
            '    If oFile.Name <> "Thumbs.db" Then
            '        Kill(oFile.FullName)
            '    End If
            'Next
            'For Each ofolder As System.IO.DirectoryInfo In oRootFolder.GetDirectories()
            '    For Each oFile In ofolder.GetFiles()
            '        If oFile.Name <> "Thumbs.db" Then
            '            Kill(oFile.FullName)
            '        End If
            '    Next
            '    ofolder.Delete(True)
            'Next
            'oFiles = Nothing
            'oFile = Nothing

            System.IO.Directory.Delete(_ExamTempFolder, True)
        End If
    End Sub


    Public Function GetPrefixTransactionID() As Long
        Dim strID As String
        Dim strPatientID As String = ""
        Dim randomDummy As Double = 0.0
        Dim strPatientTempID As String
        Dim nPatientID As Long
        nPatientID = MainMenu.gnPatientID
        Randomize(randomDummy)
        strPatientID = CStr(nPatientID)
        If strPatientID.Length >= 15 Then
            strPatientTempID = strPatientID.Substring(4, 1) & strPatientID.Substring(9, 1) & strPatientID.Substring(14, 1)
        Else
            Select Case strPatientID.Length
                Case 1
                    strPatientTempID = "00" & strPatientID
                Case 2
                    strPatientTempID = "0" & strPatientID
                Case Else
                    strPatientTempID = Right(strPatientID, 3)
            End Select
        End If
        Dim dtDate As DateTime
        dtDate = System.DateTime.Now

        strID = System.Math.Abs(DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date))
        strID = strID & strPatientTempID.Substring(0, 1)
        strID = strID & System.Math.Abs(DateDiff(DateInterval.Second, dtDate.Date, dtDate))
        strID = strID & strPatientTempID.Substring(1, 1)
        strID = strID & dtDate.Millisecond
        strID = strID & strPatientTempID.Substring(2, 1)
        Return CLng(strID)
    End Function

    Public Sub GetSecurityUser(ByVal username As String)
        Dim conn As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand
        Dim _strSQL As String = ""

        Try
            conn.Open()
            _strSQL = "select IsNull(bIsSecurityUser,0) from User_MST where sLoginName ='" & username.Replace("'", "''") & "'"

            cmd = New SqlCommand(_strSQL, conn)
            gblnSecurityUser = Convert.ToBoolean(cmd.ExecuteScalar)


            conn.Close()
            conn.Dispose()
            conn = Nothing
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
        End Try
    End Sub

    '//Document Name Changes Aug 2006//
    Public Function GetPrefixTransactionID(ByVal PatientID As Long) As Long
        Dim PatientDOB As DateTime
        Dim strID As String
        Dim dtDate As DateTime

        Try

            'Get Patient Date Of Birth
            Dim oDB As New gloStream.gloDataBase.gloDataBase
            oDB.Connect(GetConnectionString)
            PatientDOB = oDB.ExecuteQueryScaler("SELECT dtDOB FROM Patient WHERE nPatientID = " & PatientID & "")
            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing
            dtDate = System.DateTime.Now
            strID = System.Math.Abs(DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date)) & System.Math.Abs(DateDiff(DateInterval.Second, dtDate.Date, dtDate)) & System.Math.Abs(DateDiff(DateInterval.Day, CDate("1/1/1900"), PatientDOB.Date))

            Return CLng(strID)

            '14-Nov-14 Aniket: Bug #75993 ( Modified): gloEMR: Nurse Notes- Application gives exception
        Catch ex As Exception
            Return ""
        End Try

    End Function


    Public Function GetPrefixTransactionID(ByVal PatientDOB As DateTime) As Long
        Dim strID As String
        Dim dtDate As DateTime
        dtDate = System.DateTime.Now
        strID = System.Math.Abs(DateDiff(DateInterval.Day, CDate("1/1/1900"), dtDate.Date)) & System.Math.Abs(DateDiff(DateInterval.Second, dtDate.Date, dtDate)) & System.Math.Abs(DateDiff(DateInterval.Day, CDate("1/1/1900"), PatientDOB.Date))
        Return CLng(strID)
    End Function

    Public Sub UpdateLog_DI(ByVal strLogMessage As String)
        Try
            Dim objFile As New System.IO.StreamWriter(gstrgloEMRStartupPath & "\gloEMRDI.txt", True)
            objFile.WriteLine(System.DateTime.Now & ":" & System.DateTime.Now.Millisecond & vbTab & strLogMessage)
            objFile.Close()
            objFile.Dispose()
            objFile = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Public Sub UpdateLog(ByVal strLogMessage As String)
        Try
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, strLogMessage, gloAuditTrail.ActivityOutCome.Success)

        Catch ex As Exception
        End Try
    End Sub

    Public Sub UpdateSurescriptsLog(ByVal strLogMessage As String)
        Try
            Dim objFile As New System.IO.StreamWriter(Application.StartupPath & "\gloEMRSurescriptsLog.log", True)
            objFile.WriteLine(System.DateTime.Now & ":" & System.DateTime.Now.Millisecond & vbTab & strLogMessage)
            objFile.Close()
            objFile.Dispose()
            objFile = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Function GetRegistryValue(KeyName As String) As Object

        Dim regkey As RegistryKey = Registry.CurrentUser
        Dim rkey As RegistryKey
        Dim myObject As Object = Nothing

        rkey = regkey.OpenSubKey("Software\gloEMR")

        Try
            If rkey IsNot Nothing Then
                myObject = rkey.GetValue(KeyName)
                rkey.Close()
                rkey.Dispose()
            End If
            regkey.Close()
            regkey.Dispose()
        Catch
        End Try
        Return myObject

    End Function

    Public Sub UpdateExamLog(ByVal strLogMessage As String, PatientID As Long, ExamID As Long)

        Dim flgApplicationErr As Boolean

        Try

            If GetRegistryValue("EnableApplicationLogs") IsNot Nothing Then
                flgApplicationErr = Convert.ToBoolean(GetRegistryValue("EnableApplicationLogs"))
            End If


            If flgApplicationErr = True Then
                Dim objFile As New System.IO.StreamWriter(Application.StartupPath & "\Log\ApplicationLog\PatientExamLog-" & PatientID & "-" & ExamID & ".log", True)
                objFile.WriteLine(System.DateTime.Now & ":" & System.DateTime.Now.Millisecond & vbTab & strLogMessage)
                objFile.Close()
                objFile.Dispose()
                objFile = Nothing
            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Sub UpdateVoiceLog(ByVal strLogMessage As String)
        Try
            gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Voice, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.None, strLogMessage, gloAuditTrail.ActivityOutCome.Success)


        Catch ex As Exception
        End Try
    End Sub

    Public Sub UpdateLog_Mail(ByVal strLogMessage As String)
        Try
            Dim objFile As New System.IO.StreamWriter(gstrgloEMRStartupPath & "\gloEMR_Mail.txt", True)
            objFile.WriteLine(System.DateTime.Now & ":" & System.DateTime.Now.Millisecond & vbTab & strLogMessage)
            objFile.Close()
            objFile.Dispose()
            objFile = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Public ReadOnly Property SignatureNewImageName() As String
        Get
            'Dim _Path As String = gloSettings.FolderSettings.AppTempFolderPath
            'Dim _NewDocumentName As String = ""
            'Dim _Extension As String = ".Tif"
            'Dim _dtCurrentDateTime As DateTime = System.DateTime.Now

            'Dim i As Integer = 0
            '_NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & _Extension
            'While File.Exists(_Path & "\" & _NewDocumentName) = True And i < Integer.MaxValue
            '    i = i + 1
            '    _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & "-" & i & _Extension
            'End While
            'Return _Path & "\" & _NewDocumentName
            Return gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".Tif", "MMddyyyyHHmmssffff")
        End Get
    End Property

#Region " Patient Status "

    'Public Function CheckPatientStatus(ByVal PatientID As Long, Optional ByVal PatientCode As String = "", Optional ByVal IsFromPatientRegistration As Boolean = False, Optional ByVal IsFromDashBoard As Boolean = False) As Boolean
    '    'Check Patient Status <><><><><><>''''
    '    'Mahesh 
    '    'Check if Current User has Admin Rights 
    '    Dim oclsPatReg As New ClsPatientRegistrationDBLayer
    '    'Chetan integrated for Lock Status of Patient
    '    If IsFromDashBoard = True Then
    '        'from Dashboard restrict each user.....
    '        With oclsPatReg
    '            Dim PatientStatus As String = ""
    '            PatientStatus = .PatientStatus(PatientID, PatientCode)
    '            oclsPatReg = Nothing
    '            '' If Patient Status Is "LockCharts"  then 
    '            '' don't show any activity against this Patient
    '            If PatientStatus = gtsrPatientStatus_LockCharts Then
    '                Return False
    '            Else
    '                Return True
    '            End If
    '        End With
    '    End If

    '    If IsFromPatientRegistration = True Then
    '        If gblnIsAdmin = False Then
    '            ''if Not then warn user
    '            With oclsPatReg
    '                Dim PatientStatus As String = ""
    '                PatientStatus = .PatientStatus(PatientID, PatientCode)
    '                oclsPatReg = Nothing
    '                '' If Patient Status Is "Legal Pending" or "Decesed" then 
    '                '' don't Allow any activity against this Patient
    '                If PatientStatus = gtsrPatientStatus_Deceased Or PatientStatus = gtsrPatientStatus_Pending Or PatientStatus = gtsrPatientStatus_LockCharts Then
    '                    MessageBox.Show("The status of the patient is '" & PatientStatus & "'." & vbCrLf & "Only Adminstrator can modify this Patient's information.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                    Return False
    '                Else
    '                    Return True
    '                End If
    '            End With
    '        Else
    '            '' Allow User to save the Changes 
    '            Return True
    '        End If
    '    Else
    '        'Check Patient Status <><><><><><>''''
    '        '20070125 - Mahesh 
    '        With oclsPatReg
    '            Dim PatientStatus As String = ""
    '            PatientStatus = .PatientStatus(PatientID) ''gnPatientID)
    '            oclsPatReg = Nothing
    '            '' If Patient Status Is "Legal Pending" or "Deceased" then 
    '            '' don't Allow any activity against this Patient
    '            If PatientStatus = gtsrPatientStatus_Deceased Or PatientStatus = gtsrPatientStatus_Pending Or PatientStatus = gtsrPatientStatus_LockCharts Then
    '                MessageBox.Show("The status of the patient is '" & PatientStatus & "'." & vbCrLf & "You can not perform any activity on this Patient. ", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                Return False
    '            Else
    '                Return True
    '            End If
    '        End With
    '        ''''<><><><><><><><><><><>''''
    '    End If
    '    oclsPatReg.Dispose()
    '    oclsPatReg = Nothing

    'End Function

    Public Function CheckPatientStatus(ByVal PatientID As Long, Optional ByVal PatientCode As String = "", Optional ByVal IsFromPatientRegistration As Boolean = False, Optional ByVal IsFromDashBoard As Boolean = False, Optional ByVal AllowScreenAccess As Boolean = False) As Boolean
        Dim oclsPatReg As New ClsPatientRegistrationDBLayer
        Dim PatientStatus As String = String.Empty

        Try

            PatientStatus = oclsPatReg.PatientStatus(PatientID, PatientCode)

            If IsFromDashBoard = False And AllowScreenAccess = False Then 'And gblnIsAdmin = False
                If PatientStatus = gtsrPatientStatus_Deceased Or PatientStatus = gtsrPatientStatus_Pending Then
                    MessageBox.Show("The status of the patient is '" & PatientStatus & "'." & vbCrLf & "Only Administrator can modify this Patient's information.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ElseIf PatientStatus = gtsrPatientStatus_LockCharts Then
                    MessageBox.Show("The status of the patient is '" & PatientStatus & "'." & vbCrLf & "You can not perform any activity on this Patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End If

            Select Case PatientStatus
                Case gtsrPatientStatus_LockCharts
                    Return False

                Case gtsrPatientStatus_Deceased
                    'If gblnIsAdmin = True Then
                    '    Return True
                    'Else
                    ''Bug #81090: 00000879: deceased patient status
                    Return False
                    'If AllowScreenAccess = True Then 'Allow deceased / Legal Pending patient for screen access 
                    '    Return True
                    'Else
                    '    Return False
                    'End If

                    'End If

                Case gtsrPatientStatus_Pending
                    'If gblnIsAdmin = True Then
                    '    Return True
                    'Else
                    If AllowScreenAccess = True Then 'Allow deceased / Legal Pending patient for screen access 
                        Return True
                    Else
                        Return False
                    End If
                    'End If
                Case Else
                    Return True

            End Select
            'Else
            'Select Case PatientStatus
            '    Case gtsrPatientStatus_LockCharts
            '        MessageBox.Show("The status of the patient is '" & PatientStatus & "'." & vbCrLf & "Only Administrator can modify this Patient's information.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '        Return False
            '    Case gtsrPatientStatus_Deceased
            '        If gblnIsAdmin = False Then
            '            MessageBox.Show("The status of the patient is '" & PatientStatus & "'." & vbCrLf & "Only Administrator can modify this Patient's information.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '            Return False
            '        Else
            '            Return True
            '        End If

            '    Case gtsrPatientStatus_Pending
            '        If gblnIsAdmin = False Then
            '            MessageBox.Show("The status of the patient is '" & PatientStatus & "'." & vbCrLf & "Only Administrator can modify this Patient's information.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '            Return False
            '        Else
            '            Return True
            '        End If
            '    Case Else
            '        Return True

            'End Select
            'End If


        Catch ex As Exception
            Return Nothing
        Finally
            If Not IsNothing(oclsPatReg) Then
                oclsPatReg.Dispose()
                oclsPatReg = Nothing
            End If

        End Try
    End Function

    Public Function CheckPatientStatus_new(ByVal PatientStatus As String, Optional ByVal IsFromDashBoard As Boolean = False, Optional ByVal AllowScreenAccess As Boolean = False) As Boolean
        If IsFromDashBoard = False And AllowScreenAccess = False Then 'And gblnIsAdmin = False
            If PatientStatus = gtsrPatientStatus_Deceased Or PatientStatus = gtsrPatientStatus_Pending Then
                MessageBox.Show("The status of the patient is '" & PatientStatus & "'." & vbCrLf & "Only Administrator can modify this Patient's information.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ElseIf PatientStatus = gtsrPatientStatus_LockCharts Then
                MessageBox.Show("The status of the patient is '" & PatientStatus & "'." & vbCrLf & "You can not perform any activity on this Patient.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If

        Select Case PatientStatus
            Case gtsrPatientStatus_LockCharts
                Return False

            Case gtsrPatientStatus_Deceased
                'If gblnIsAdmin = True Then
                '    Return True
                'Else
                ''Bug #81090: 00000879: deceased patient status
                Return False
                'If AllowScreenAccess = True Then 'Allow deceased / Legal Pending patient for screen access 
                '    Return True
                'Else
                '    Return False
                'End If

                'End If

            Case gtsrPatientStatus_Pending
                'If gblnIsAdmin = True Then
                '    Return True
                'Else
                If AllowScreenAccess = True Then 'Allow deceased / Legal Pending patient for screen access 
                    Return True
                Else
                    Return False
                End If
                'End If
            Case Else
                Return True

        End Select
    End Function
#End Region

    Interface ISignature
        WriteOnly Property ImageFilePath() As String
        Sub AddSignature(ByVal sImagePath As String)
    End Interface
    ''' <summary>
    ''' Interface for patient context changes all patient specific mdichild forms should implement it to avoid patient context issue.
    ''' </summary>
    ''' <remarks></remarks>
    Interface IPatientContext
        ReadOnly Property GetCurrentPatientID() As Int64
    End Interface

    Interface IDataDictionary
        Function GetDictionary(ByVal m_flag As Boolean) As DataTable
        Function getReportData(ByVal strselect As String) As System.Data.DataTable
        Function GetClinicLogo() As DataTable
        Function GetProviderSign() As DataTable
        Function GetProviders() As ArrayList
    End Interface

    Interface ISectionDetails
        Property Suppress() As Boolean
        Property Height() As System.Single
    End Interface

    Public Property gstrRxReportpath() As String
        Get
            Return _gstrRxReportpath
        End Get
        Set(ByVal Value As String)
            _gstrRxReportpath = Value
        End Set
    End Property

    Public Sub FillCategoryDescription_Collection()
        collCategoryDesc = New Collection
        collCategoryDesc.Add("Labs")
        collCategoryDesc.Add("Radiology")
        collCategoryDesc.Add("Tags")
        collCategoryDesc.Add("Restriction")
        collCategoryDesc.Add("Messages")
        collCategoryDesc.Add("Patient Letters")
        collCategoryDesc.Add("Referral Letter")
        collCategoryDesc.Add("SOAP")
        collCategoryDesc.Add("PTProtocol")
        collCategoryDesc.Add("Wellness Guidelines")
        collCategoryDesc.Add("Disease Management")
        collCategoryDesc.Add("Patient Consent")
        collCategoryDesc.Add("Fax Cover Page")
        collCategoryDesc.Add("Patient Education")
        collCategoryDesc.Add("Orders")
        collCategoryDesc.Add("Preventive Services")
        collCategoryDesc.Add("Medical Condition")
        collCategoryDesc.Add("Disclosure Management")


        collCategoryDesc.Add("MIS Reports")

        ''Sandip Darade 20090306
        ''Coded History added to system categories collection  
        collCategoryDesc.Add("Coded History")

        '30-Apr-14 Aniket: Remove Smoking Status as System Defined Category
        'collCategoryDesc.Add("Smoking Status")
        collCategoryDesc.Add("Current every day smoker: Heavy (20-39 cigs/day)")
        collCategoryDesc.Add("Current every day smoker: Light (1-9 cigs/day)")
        collCategoryDesc.Add("Current every day smoker: Moderate (10-19 cigs/day)")
        collCategoryDesc.Add("Current every day smoker: Very heavy (40+ cigs/day)")

        collCategoryDesc.Add("Unknown if ever smoked")
        collCategoryDesc.Add("Current some day smoker")
        collCategoryDesc.Add("Smoker, Current status unknown")
        collCategoryDesc.Add("Never smoker")
        collCategoryDesc.Add("Former smoker")
        collCategoryDesc.Add("Consent Form")

        collCategoryDesc.Add("Born outside the United States")
        collCategoryDesc.Add("Lab evidence of previous disease")
        collCategoryDesc.Add("MD diagnosis of previous disease")
        collCategoryDesc.Add("Medical contraindication (finding)")
        collCategoryDesc.Add("Never offered vaccine")
        collCategoryDesc.Add("other")
        collCategoryDesc.Add("Parent/Patient forgot to vaccinate")
        collCategoryDesc.Add("Parent/Patient refusal")
        collCategoryDesc.Add("Parent/patient report of previous disease")
        collCategoryDesc.Add("Philosophical objection")
        collCategoryDesc.Add("Religious exemption")
        collCategoryDesc.Add("Under age for vaccination")
        collCategoryDesc.Add("unknown")
        collCategoryDesc.Add("Person Condition")
        collCategoryDesc.Add("Family Member Condition")
        collCategoryDesc.Add("Waiver")
        collCategoryDesc.Add("Documented Immunity/Titer")
        collCategoryDesc.Add("Secure Message")
        collCategoryDesc.Add("Fax")
        collCategoryDesc.Add("Mail")
        collCategoryDesc.Add("Phone")
        collCategoryDesc.Add("OB Medical History")
        collCategoryDesc.Add("OB Genetic History")
        collCategoryDesc.Add("OB Infection History")
        collCategoryDesc.Add("OB Initial Physical Examination")

        collCategoryDesc.Add("OB Initial Physical Examination Answers")


    End Sub

#Region " Record Locking "
    Public Enum TrnType
        None = 0
        PatientRegistration = 1
        PatientROS = 2
        PatientHistory = 3
        Medication = 4
        Prescription = 5
        PatientVitals = 6
        Radiology = 7
        Labs = 8
        Messages = 9
        Letters = 10
        PTProtocol = 11
        PatientConsent = 12
        Flowsheet = 13
        Task = 14
        Immunization = 15
        ReferralLetters = 16
        DMS = 17
        ProblemList = 18
        DisclosureManagement = 19
        NurseNotes = 20
        Triage = 21
        MUReport = 22
        WorkersCompForm = 23
        PatientOBPlan = 24
        ImplantDevices = 25
    End Enum


    Public Function Scan_n_Lock_Transaction(ByVal TransactionType As TrnType, ByVal RecordID As Long, ByVal VisitID As Long, ByVal VisitDate As DateTime) As mytable
        Dim Con As New SqlConnection(GetConnectionString)
        Try
            Dim cmd As New SqlCommand("gsp_Scan_n_Lock_Record", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As New SqlParameter

            sqlParam = cmd.Parameters.Add("@nRecordID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = RecordID

            sqlParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = VisitID

            sqlParam = cmd.Parameters.Add("@dtVisitDate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            If IsNothing(VisitDate) OrElse VisitDate = "12:00:00 AM" Then
                sqlParam.Value = Now
            Else
                sqlParam.Value = VisitDate
            End If

            sqlParam = cmd.Parameters.Add("@nTrnType", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = TransactionType

            Dim sqlParamUserName As New SqlParameter
            sqlParamUserName = cmd.Parameters.Add("@sUserName", SqlDbType.VarChar, 50)
            sqlParamUserName.Direction = ParameterDirection.InputOutput
            sqlParamUserName.Value = gstrLoginName

            Dim sqlParamMachineName As New SqlParameter
            sqlParamMachineName = cmd.Parameters.Add("@sMachineName", SqlDbType.VarChar, 50)
            sqlParamMachineName.Direction = ParameterDirection.InputOutput
            sqlParamMachineName.Value = gstrClientMachineName

            Con.Open()
            cmd.ExecuteNonQuery()

            Dim mydt As New mytable

            If IsDBNull(sqlParamUserName.Value) Then
                mydt.Code = ""
            Else
                mydt.Code = sqlParamUserName.Value
            End If

            If IsDBNull(sqlParamMachineName.Value) Then
                mydt.Description = ""
            Else
                mydt.Description = sqlParamMachineName.Value
            End If
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            Return mydt

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            Con.Close()
            Con.Dispose()
        End Try
    End Function

    Public Function UnLock_Transaction(ByVal TransactionType As TrnType, ByVal RecordID As Long, ByVal VisitID As Long, ByVal VisitDate As DateTime) As Boolean
        Dim Con As New SqlConnection(GetConnectionString)
        Try

            Dim cmd As New SqlCommand("gsp_UnLock_Record", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As SqlParameter

            sqlParam = cmd.Parameters.Add("@nRecordID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = RecordID

            sqlParam = cmd.Parameters.Add("@nVisitID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = VisitID

            sqlParam = cmd.Parameters.Add("@dtVisitDate", SqlDbType.DateTime)
            sqlParam.Direction = ParameterDirection.Input
            If IsNothing(VisitDate) = True OrElse VisitDate = "12:00:00 AM" Then
                sqlParam.Value = Now
            Else
                sqlParam.Value = VisitDate
            End If

            sqlParam = cmd.Parameters.Add("@nTrnType", SqlDbType.Int)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = TransactionType


            Con.Open()
            cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            Return True

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Patient Exam", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Finally
            Con.Close()
            Con.Dispose()
            Con = Nothing

        End Try
    End Function

    'Added by madan for... unlocking all records locked by this system.-- 20100624
    Public Function UnLock_ALLTransactions(ByVal Machinename As String, ByVal UserName As String) As Boolean

        Dim oDBLayer As gloDatabaseLayer.DBLayer = New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oDBParameters As gloDatabaseLayer.DBParameters = New gloDatabaseLayer.DBParameters()
        Dim _result As Boolean = False

        Try
            oDBLayer.Connect(False)
            oDBParameters.Clear()
            oDBParameters.Add("@MachineName", Machinename, ParameterDirection.Input, SqlDbType.NVarChar)
            'For resolving case no :GLO2011-0011178 i.e group Messaging
            oDBParameters.Add("@UserName", UserName, ParameterDirection.Input, SqlDbType.NVarChar)
            'oDBLayer.Execute("gsp_UnLockAll_Records_On_Machine", oDBParameters)
            oDBParameters.Add("@ProcessID", System.Diagnostics.Process.GetCurrentProcess().Id, ParameterDirection.Input, SqlDbType.BigInt)

            oDBLayer.Execute("gsp_UnLockAll_Records_On_Machine", oDBParameters)
            oDBLayer.Disconnect()
            _result = True


        Catch ex As SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            _result = False

        Catch ex As Exception

            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            _result = False

        Finally
            If Not IsNothing(oDBLayer) Then
                oDBLayer.Dispose()
            End If
            If Not IsNothing(oDBParameters) Then
                oDBParameters.Dispose()
            End If
        End Try

        Return _result

    End Function
    'End Madan

#End Region

    Public Function GetDateCategory(ByVal _Date As Date) As DateCategory

        Dim arrDate() As String = Split(Format(_Date, "MM/dd/yyyy"), "/")
        Dim arrDateNow() As String = Split(Format(Now, "MM/dd/yyyy"), "/")

        If Val(arrDate.GetValue(2)) < 2000 Then
            Return DateCategory.Older ' "Older"
        ElseIf Val(arrDate.GetValue(2)) = Val(arrDateNow.GetValue(2)) And Val(arrDate.GetValue(0)) = Val(arrDateNow.GetValue(0)) And Val(arrDate.GetValue(1)) = Val(arrDateNow.GetValue(1)) Then
            Return DateCategory.Today ' "Today"
        ElseIf Val(arrDate.GetValue(2)) = Val(arrDateNow.GetValue(2)) And Val(arrDate.GetValue(0)) = Val(arrDateNow.GetValue(0)) And Convert.ToInt16(arrDate.GetValue(1)) >= Convert.ToInt16(arrDateNow.GetValue(1)) + 1 Then
            Return DateCategory.Today '"Today"
        ElseIf Val(arrDate.GetValue(2)) > Val(arrDateNow.GetValue(2)) Then
            Return DateCategory.Today '"Today"
        ElseIf Val(arrDate.GetValue(0)) > Val(arrDateNow.GetValue(0)) And Val(arrDate.GetValue(0)) < 9 Then
            Return DateCategory.Today '"Today"
        ElseIf Val(arrDate.GetValue(0)) > Val(arrDateNow.GetValue(0)) And Val(arrDate.GetValue(2)) = Val(arrDateNow.GetValue(2)) Then
            Return DateCategory.Today '"Today"
        ElseIf Val(arrDate.GetValue(2)) = Val(arrDateNow.GetValue(2)) And Val(arrDate.GetValue(0)) = Val(arrDateNow.GetValue(0)) And Convert.ToInt16(arrDate.GetValue(1)) = Convert.ToInt16(arrDateNow.GetValue(1)) - 1 Then
            Return DateCategory.Yesterday '"Yestarday"
        ElseIf Val(arrDate.GetValue(2)) = Val(arrDateNow.GetValue(2)) And Val(arrDate.GetValue(0)) = Val(arrDateNow.GetValue(0)) And Convert.ToInt16(arrDate.GetValue(1)) >= 1 Then
            Return DateCategory.LastWeek '"Last Week"
        ElseIf Val(arrDate.GetValue(2)) = Val(arrDateNow.GetValue(2)) And Val(arrDate.GetValue(0)) = Val(arrDateNow.GetValue(0)) - 1 Then
            Return DateCategory.LastMonth '"Last Month"
        ElseIf Val(arrDate.GetValue(2)) = Val(arrDateNow.GetValue(2)) - 1 And Val(arrDate.GetValue(0)) = 12 Then
            Return DateCategory.LastMonth '"Last Month"
        Else
            Return DateCategory.Older '"Older"
        End If
    End Function

    Public Function GetDateRange(ByVal _DateCategory As DateCategory) As DateTime()
        Dim DateRange(1) As DateTime
        Try
            If _DateCategory = DateCategory.Today Then
                DateRange(0) = System.DateTime.Now.Date
                DateRange(1) = System.DateTime.Now.Date
                Return DateRange
            ElseIf _DateCategory = DateCategory.Yesterday Then
                DateRange(0) = System.DateTime.Now.Date.AddDays(-1)
                DateRange(1) = System.DateTime.Now.Date.AddDays(-1)
                Return DateRange
            ElseIf _DateCategory = DateCategory.LastWeek Then
                ''Anil 20071222
                ''To find First and Last date of the Last Week 
                Dim WeekLdate As DateTime
                ''Last date of week
                WeekLdate = System.DateTime.Now.Date.AddDays(-(Weekday(Now, FirstDayOfWeek.Sunday)))
                ''First date of the week
                DateRange(0) = WeekLdate.AddDays(-7)
                DateRange(1) = WeekLdate
                Return DateRange
                ''
            ElseIf _DateCategory = DateCategory.LastMonth Then
                Dim nLastMonth As Integer
                Dim nYear As Integer
                nLastMonth = (System.DateTime.Now.Month - 1)
                nYear = System.DateTime.Now.Year
                If nLastMonth = 0 Then
                    nLastMonth = 12
                    nYear = System.DateTime.Now.Year - 1
                End If
                DateRange(0) = DateAndTime.DateSerial(nYear, nLastMonth, 1) '' From Date
                DateRange(1) = DateAndTime.DateSerial(nYear, nLastMonth, Date.DaysInMonth(nYear, nLastMonth)) '' To Date
                Return DateRange
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function


    ''[DllImport("user32.dll")]
    Private Declare Function GetForegroundWindow Lib "User32.dll" () As Int32

    '[DllImport("user32")]
    Private Declare Function GetWindowThreadProcessId Lib "user32" (ByVal hWndas As Int32, ByVal lpdwProcessId As Int32) As UInt32

    Private Function GetWindowProcessID(ByVal hwnd As Int32) As Int32
        Dim pid As Int32 = 1
        GetWindowThreadProcessId(hwnd, pid)
        Return pid
    End Function

    Public Function GetFocusedProcess() As Process
        Dim hwnd As Int32
        hwnd = GetForegroundWindow()
        Return Process.GetProcessById(GetWindowProcessID(hwnd))
    End Function

    Public Function ReplaceSpecialCharacters(ByVal strSpecialChar As String) As String
        Try

            strSpecialChar = Replace(strSpecialChar, "#", "[#]") & ""
            strSpecialChar = Replace(strSpecialChar, "$", "[$]") & ""
            strSpecialChar = Replace(strSpecialChar, "%", "[%]") & ""
            strSpecialChar = Replace(strSpecialChar, "^", "[^]") & ""
            strSpecialChar = Replace(strSpecialChar, "&", "[&]") & ""

            '' Was Commented Before 2090602
            '' Uncommneted By Mahesh to Handle the Special Char in search By Replacing char with '[Char]'
            '' Ref: http://sqlserver2000.databases.aspfaq.com/how-do-i-search-for-special-characters-e-g-in-sql-server.html
            strSpecialChar = Replace(strSpecialChar, "~", "[~]") & ""
            strSpecialChar = Replace(strSpecialChar, "!", "[!]") & ""
            strSpecialChar = Replace(strSpecialChar, "*", "[*]") & ""
            strSpecialChar = Replace(strSpecialChar, ";", "[;]") & ""
            strSpecialChar = Replace(strSpecialChar, "/", "[/]") & ""
            strSpecialChar = Replace(strSpecialChar, "?", "[?]") & ""
            strSpecialChar = Replace(strSpecialChar, ">", "[>]") & ""
            strSpecialChar = Replace(strSpecialChar, "<", "[<]") & ""
            strSpecialChar = Replace(strSpecialChar, "\", "[\]") & ""
            strSpecialChar = Replace(strSpecialChar, "|", "[|]") & ""
            strSpecialChar = Replace(strSpecialChar, "{", "[{]") & ""
            strSpecialChar = Replace(strSpecialChar, "}", "[}]") & ""
            strSpecialChar = Replace(strSpecialChar, "-", "[-]") & ""
            strSpecialChar = Replace(strSpecialChar, "_", "[_]") & ""
            ''END Was Commented Before 2090602
            Return strSpecialChar
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function
    '' END SUDHIR ''


    Public Function ValidateText(ByVal Text As String, ByVal e As KeyPressEventArgs)
        Try
            If InStr(Trim(Text), ".") <> 0 And (e.KeyChar = ChrW(46)) Or ((e.KeyChar >= ChrW(35) And e.KeyChar <= ChrW(38)) Or (e.KeyChar = ChrW(64)) Or (e.KeyChar = ChrW(33)) Or (e.KeyChar = ChrW(42)) Or (e.KeyChar = ChrW(43)) Or (e.KeyChar = ChrW(45)) Or (e.KeyChar = ChrW(60)) Or (e.KeyChar = ChrW(59)) Or (e.KeyChar = ChrW(61)) Or (e.KeyChar = ChrW(94)) Or (e.KeyChar = ChrW(96)) Or (e.KeyChar = ChrW(124)) Or (e.KeyChar = ChrW(125)) Or (e.KeyChar = ChrW(126))) Then
                e.Handled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Return Nothing
    End Function

    '' SUDHIR 20090716 '' 
    Public Function ValidateNumeric(ByVal Text As String, ByVal e As KeyPressEventArgs)
        Try
            If Text.Contains(".") = True And e.KeyChar = "." Then
                e.Handled = True
                ValidateNumeric = False
                Exit Function
            End If

            If Char.IsNumber(e.KeyChar) = False And e.KeyChar <> "." And AscW(e.KeyChar) <> 8 Then
                e.Handled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ValidateNumeric = True
    End Function
    '' END SUDHIR ''

    'sarika internet fax fax log 
    Public Sub UpdateLogForFax(ByVal strLogMessage As String)
        Try

            Dim objFile As New System.IO.StreamWriter(Application.StartupPath & "\gloEMRFax.log", True)
            objFile.WriteLine(System.DateTime.Now & ":" & System.DateTime.Now.Millisecond & vbTab & strLogMessage)
            objFile.Close()
            objFile.Dispose()
            objFile = Nothing
        Catch ex As Exception

        End Try
    End Sub
    '----------sarika internet fax fax log 


    'sarika Automate Rx Eligibility Request

    Public Sub UpdateLogForRx(ByVal strLogMessage As String)
        Try

            Dim objFile As New System.IO.StreamWriter(Application.StartupPath & "\\gloRx.log", True)
            objFile.WriteLine(System.DateTime.Now & ":" & System.DateTime.Now.Millisecond & vbTab & strLogMessage)
            objFile.Close()
            objFile.Dispose()
            objFile = Nothing
        Catch ex As Exception

        End Try
    End Sub
    '-----

    'sarika Temp Directory issue on Terminal Server
    Public ReadOnly Property ExamNewFaxFileName(ByVal _path As String, ByVal _extension As String) As String
        Get
            'Dim _NewDocumentName As String = ""
            'Dim _dtCurrentDateTime As DateTime = System.DateTime.Now
            'Dim i As Integer = 0
            '_NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & _extension
            'While File.Exists(_path & _NewDocumentName) = True And i < Integer.MaxValue
            '    i = i + 1
            '    _NewDocumentName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss tt") & "-" & i & _extension
            'End While
            'Return _path & _NewDocumentName
            Return gloGlobal.clsFileExtensions.NewDocumentName(_path, _extension, "MMddyyyyHHmmssffff")
        End Get
    End Property

    ''' <summary>
    ''' Create a New Directory in Temp folder of gloEMR 
    ''' </summary>
    ''' <value></value>
    ''' <returns> Directory Path ONLY the Path After the Startup Path </returns>
    ''' <remarks> If Directory is Not Exists then it will Create the Temp Direcory </remarks>
    Public ReadOnly Property CreateNewTempDirectory() As String
        Get
            Dim _Path As String = gloSettings.FolderSettings.AppTempFolderPath
            If Directory.Exists(_Path) = False Then
                Directory.CreateDirectory(_Path)
            End If

            Dim NewDirectoryName As String = ""
            Dim _dtCurrentDateTime As DateTime = System.DateTime.Now

            Dim i As Integer = 0
            NewDirectoryName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss fff")
            While Directory.Exists(_Path & "\" & NewDirectoryName) = True And i < Integer.MaxValue
                i = i + 1
                NewDirectoryName = Format(_dtCurrentDateTime, "MM dd yyyy - hh mm ss fff") & "_" & i
            End While
            Directory.CreateDirectory(_Path & NewDirectoryName)

            Return _Path & NewDirectoryName
        End Get
    End Property



#Region "Function to Calculate Age for the date "

#Region "Get Age (Previous code)(New Code)"

    Public Function GetAge(ByVal BirthDate As DateTime) As String

        Dim _BDate As DateTime = BirthDate
        ' Compute the difference between BirthDate 'CODE FROM gloPM
        'year and end year. 
        Dim IsBirthDateLeap As Boolean = False
        Dim years As Integer = Now.Year - BirthDate.Year
        Dim months As Integer = 0
        Dim days As Integer = 0
        'Test if BirthDay for LeapYear.
        If BirthDate.Day = 29 And BirthDate.Month = 2 Then
            IsBirthDateLeap = True
        End If
        ' Check if the last year was a full year. 
        If Now < BirthDate.AddYears(years) AndAlso years <> 0 Then
            years -= 1
        End If
        BirthDate = BirthDate.AddYears(years)
        ' Now we know BirthDate <= end and the diff between them 
        ' is < 1 year. 
        If BirthDate.Year = Now.Year Then
            months = Now.Month - BirthDate.Month
        Else
            months = (12 - BirthDate.Month) + Now.Month
        End If
        ' Check if the last month was a full month. 
        If Now < BirthDate.AddMonths(months) AndAlso months <> 0 Then
            months -= 1
        End If
        BirthDate = BirthDate.AddMonths(months)
        ' Now we know that BirthDate < end and is within 1 month 
        ' of each other. 
        days = (Now - BirthDate).Days

        'To Adjust Age if BirthDate is 29th Feb in leap year
        If IsBirthDateLeap = True Then   ''Sequence of following IF code is too important.. DON'T MODIFY
            days -= 1
            If Now.Day = 29 And Now.Month = 2 Then
                days += 1
            ElseIf Now.Year Mod 4 = 0 Then
                days += 1
            End If
            If days < 0 And Now.Year Mod 4 <> 0 Then
                days = 30
                months = months - 1
                If months < 0 Then
                    months = 11
                    years = years - 1
                End If
            End If
            If months = 12 Then
                days = 30
                months = 11
            End If
        End If

        'Return years & " years " & months & " months " & days & " days"
        'Following code to display age in Numeric and Text

        'age.Age = years & " Years " & months & " Months " & days & " Days"
        '' Cases

        ''20081119   ''Following Code to Store ExactAge in String
        Dim _AgeStr As String = ""
        If gblShowAgeInDays = True And gblAgeLimit >= DateDiff(DateInterval.Day, CType(_BDate, Date), Date.Now.Date) Then
            If years = 0 Then
                If months = 0 Then
                    If days <= 1 Then
                        _AgeStr = days & " Day"
                    Else
                        _AgeStr = days & " Days"
                    End If
                ElseIf months = 1 Then
                    If days = 0 Then
                        _AgeStr = months & " Month"
                    ElseIf days = 1 Then
                        _AgeStr = months & " Month " & days & " Day"
                    Else
                        _AgeStr = months & " Month " & days & " Days"
                    End If
                ElseIf months > 1 Then
                    If days = 0 Then
                        _AgeStr = months & " Months"
                    ElseIf days = 1 Then
                        _AgeStr = months & " Months " & days & " Day"
                    Else
                        _AgeStr = months & " Months " & days & " Days"
                    End If
                End If
            ElseIf years = 1 Then
                If months = 0 Then
                    If days = 0 Then
                        _AgeStr = years & " Year "
                    ElseIf days = 1 Then
                        _AgeStr = years & " Year " & days & " Day"
                    Else
                        _AgeStr = years & " Year " & days & " Days"
                    End If
                ElseIf months = 1 Then
                    If days = 0 Then
                        _AgeStr = years & " Year " & months & " Month "
                    ElseIf days = 1 Then
                        _AgeStr = years & " Year " & months & " Month " & days & " Day"
                    Else
                        _AgeStr = years & " Year " & months & " Month " & days & " Days"
                    End If
                ElseIf months > 1 Then
                    If days = 0 Then
                        _AgeStr = years & " Year " & months & " Months "
                    ElseIf days = 1 Then
                        _AgeStr = years & " Year " & months & " Months " & days & " Day"
                    Else
                        _AgeStr = years & " Year " & months & " Months " & days & " Days"
                    End If
                End If
            ElseIf years > 1 Then
                If months = 0 Then
                    If days = 0 Then
                        _AgeStr = years & " Years "
                    ElseIf days = 1 Then
                        _AgeStr = years & " Years " & days & " Day"
                    Else
                        _AgeStr = years & " Years " & days & " Days"
                    End If
                ElseIf months = 1 Then
                    If days = 0 Then
                        _AgeStr = years & " Years " & months & " Month"
                    ElseIf days = 1 Then
                        _AgeStr = years & " Years " & months & " Month " & days & " Day"
                    Else
                        _AgeStr = years & " Years " & months & " Month " & days & " Days"
                    End If
                ElseIf months > 1 Then
                    If days = 0 Then
                        _AgeStr = years & " Years " & months & " Months"
                    ElseIf days = 1 Then
                        _AgeStr = years & " Years " & months & " Months " & days & " Day"
                    Else
                        _AgeStr = years & " Years " & months & " Months " & days & " Days"
                    End If
                End If
            End If
        Else 'ShowAgeInDay is False OR AgeLimit less than Settings.
            If years = 0 Then
                'Added by pravin on 11/25/2008
                '                If months = 0 And months = 1 Then
                If months = 1 Then
                    _AgeStr = months & " Month"
                ElseIf months > 1 Then
                    _AgeStr = months & " Months"
                End If
            ElseIf years = 1 Then
                If months = 0 Then
                    _AgeStr = years & " Year "
                ElseIf months = 1 Then
                    _AgeStr = years & " Year " & months & " Month "
                ElseIf months > 1 Then
                    _AgeStr = years & " Year " & months & " Months "
                End If
            ElseIf years > 1 Then
                If months = 0 Then
                    _AgeStr = years & " Years "
                ElseIf months = 1 Then
                    _AgeStr = years & " Years " & months & " Month "
                ElseIf months > 1 Then
                    _AgeStr = years & " Years " & months & " Months "
                End If
            End If
            'Added by pravin if age in days  11/25/2008
            If years = 0 And months = 0 Then
                If days <= 1 Then
                    _AgeStr = days & " Day"
                Else
                    _AgeStr = days & " Days"
                End If
            End If
        End If
        Patientage.Age = _AgeStr
        Patientage.Years = years
        Patientage.Months = months
        Patientage.Days = days
        Return _AgeStr
    End Function

#End Region

#End Region

    ''sudhir 20081112
    ''fuction to fetch admin settings for ShowAgeInDays on PatientStrip
    Public Sub GetAgeInDaysSettings()
        Try
            Dim adp As New SqlDataAdapter("select sSettingsName, ISNULL(sSettingsValue,'') AS sSettingsValue from settings where sSettingsName='SHOW AGE IN DAYS' or sSettingsName='AGE LIMIT'", GetConnectionString)
            Dim dt As New DataTable
            adp.Fill(dt)

            If dt.Rows.Count > 0 Then
                If Convert.ToString(dt.Rows(0)(0)).ToUpper = "SHOW AGE IN DAYS" And Convert.ToString(dt.Rows(0)(0)).ToUpper <> "" Then
                    gblShowAgeInDays = dt.Rows(0)(1)
                Else
                    gblShowAgeInDays = dt.Rows(1)(1)
                End If

                If Convert.ToString(dt.Rows(1)(0)).ToUpper = "AGE LIMIT" And Convert.ToString(dt.Rows(1)(0)).ToUpper <> "" Then
                    gblAgeLimit = CType((dt.Rows(1)(1) * 365), Int32)
                Else
                    gblAgeLimit = CType((dt.Rows(0)(1) * 365), Int32)
                End If
            End If

            adp.Dispose()
            adp = Nothing

            dt.Dispose()
            dt = Nothing

        Catch ex As Exception
        End Try
    End Sub

    'TO CHECK PATIENT CHECK IN STATUS
    Public Function IsPatientCheckIn(ByVal PatientID As Long) As Boolean
        Dim Query As String = ""
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
        Dim oResult As Object
        Try
            oDB.Connect(False)
            Query = " SELECT	COUNT(*)" _
                & " FROM AS_Appointment_DTL INNER JOIN AS_Appointment_MST ON AS_Appointment_DTL.nMSTAppointmentID = AS_Appointment_MST.nMSTAppointmentID " _
                & " WHERE AS_Appointment_MST.nPatientID = " & PatientID & " AND AS_Appointment_DTL.dtStartDate = '" & gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToShortDateString()) & "' AND AS_Appointment_DTL.nClinicID = " & gnClinicID & " AND AS_Appointment_DTL.nASBaseFlag = 1 " _
                & " AND nDTLAppointmentID IN " _
                & " (	SELECT nDTLAppointmentID	FROM PatientTracking" _
                & " 	WHERE nPatientID = " & PatientID & " AND CONVERT(varchar, dtDate, 101) = '" & DateTime.Now.Date.ToString("MM/dd/yyyy") & "' AND nClinicID = " & gnClinicID & " AND nTrackingStatus = 3 " _
                & " 	AND nDTLAppointmentID NOT IN " _
                & " 							(	SELECT nDTLAppointmentID	" _
                & " 								FROM PatientTracking" _
                & " 								WHERE nPatientID = " & PatientID & " AND CONVERT(varchar, dtDate, 101) = '" & DateTime.Now.Date.ToString("MM/dd/yyyy") & "' AND nClinicID = " & gnClinicID & "" _
                & " 								AND nTrackingStatus = 4 " _
                & " 							)" _
                & " )"
            oResult = oDB.ExecuteScalar_Query(Query)
            If CType(oResult, Integer) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return Nothing
        Finally
            oDB.Disconnect()
            oDB = Nothing
        End Try
    End Function

    'TO CHECK PROVIDER FOR ACTIVE/BLOCKED ''
    Public Function IsPatientProviderDisabled(ByVal patientID As Int64) As Boolean

        Dim oResult As Object
        Dim query As String

        Try
            Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
            query = " SELECT COUNT(nProviderID) FROM Provider_MST WHERE nProviderID = " _
                            & " (SELECT nProviderID FROM Patient WHERE nPatientID = " & patientID & ") AND bIsblocked = 'TRUE'"

            oDB.Connect(False)

            oResult = oDB.ExecuteScalar_Query(query)

            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing


            If CType(oResult, Int32) = 0 Then
                Return False '' PROVIDER IS NOT DISABLED ''
            Else
                Return True '' PROVIDER IS DISABLED ''
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oResult = Nothing
            query = Nothing
        End Try
    End Function
    Public Function IsExamProviderDisabled(ByVal providerid As Int64) As Boolean

        Dim oResult As Object
        Dim query As String

        Try
            Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString)
            query = " SELECT COUNT(nProviderID) FROM Provider_MST WHERE nProviderID = " & providerid & " AND bIsblocked = 'TRUE'"

            oDB.Connect(False)

            oResult = oDB.ExecuteScalar_Query(query)

            oDB.Disconnect()
            oDB.Dispose()
            oDB = Nothing


            If CType(oResult, Int32) = 0 Then
                Return False '' PROVIDER IS NOT DISABLED ''
            Else
                Return True '' PROVIDER IS DISABLED ''
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            oResult = Nothing
            query = Nothing
        End Try
    End Function
    Public Function ShowAssociateProvider(ByVal patientID As Int64, ByVal oParent As Form) As Boolean
        Try
            If MessageBox.Show("Provider associated with this patient has been disabled." & vbLf & "Do you want to associate this patient with other provider?", gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                Dim ofrm As New frmAssociateProvider(patientID)
                ofrm.ShowDialog(oParent)
                If ofrm.isAssociated = True Then
                    gblnProviderDisable = False
                    ofrm.Dispose()
                    ofrm = Nothing
                    Return True '' PROVIDER IS ASSOCIATED FOR CURRENT PATIENT ''
                Else
                    gblnProviderDisable = True
                    ofrm.Dispose()
                    ofrm = Nothing
                    Return False '' PROVIDER IS NOT SET DUE TO ERROR OR CLICKED CLOSE BUTTON ''
                End If
            Else
                Return False '' PROVIDER IS NOT SET. ''
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    'For Performance Related Changes
    Public Function GetAllCheckInPatient() As DataTable
        Dim dtPatient As New DataTable
        Dim clinicId As New Int64
        Dim con As SqlConnection = Nothing
        Dim cmd As SqlCommand = Nothing
        Dim da As New SqlDataAdapter
        Try
            clinicId = gnClinicID
            con = New SqlConnection(GetConnectionString)
            cmd = New SqlCommand("GetAllCheckInPatient", con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim objParam As SqlParameter
            objParam = cmd.Parameters.Add("@clinicId", SqlDbType.BigInt)
            objParam.Direction = ParameterDirection.Input
            objParam.Value = clinicId
            da.SelectCommand = cmd
            da.Fill(dtPatient)
            If IsNothing(dtPatient) = False Then
                If dtPatient.Rows.Count > 0 Then
                    Return dtPatient
                End If
            End If
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            con.Dispose()
            con = Nothing
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing

            da.Dispose()
            da = Nothing

            'dtPatient.Dispose()
            'dtPatient = Nothing
        End Try
    End Function

    ''To Get Smoking Column Setting
    Public Sub CheckSettingForSmokingColumn()

        Dim cmd As SqlCommand = Nothing
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Dim con As SqlConnection = Nothing
        Try
            con = New SqlConnection(GetConnectionString)
            cmd = New SqlCommand("GetSettingForSmokingColumn", con)
            cmd.CommandType = CommandType.StoredProcedure

            con.Open()

            da = New SqlDataAdapter
            da.SelectCommand = cmd
            dt = New DataTable
            da.Fill(dt)
            con.Close()
            If dt.Rows.Count > 0 Then
                If dt.Rows(0)(0).ToString() = "1" Then
                    gblnShowSmokingColumn = True
                Else
                    gblnShowSmokingColumn = False
                End If
            End If
            dt.Dispose()
            dt = Nothing

        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.History, gloAuditTrail.ActivityCategory.Category, gloAuditTrail.ActivityType.Select, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally

            If con.State = ConnectionState.Open Then
                con.Close()

            End If
            con.Dispose()
            con = Nothing
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
            If IsNothing(cmd) = False Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
        End Try
    End Sub
    ''To Get Smoking Column Setting


    'TO FETCH USER NAME
    Public Function GetDoctorName() As String
        Dim cls As New clsDoctorsDashBoard
        Dim dt As DataTable
        dt = cls.Get_LoginProviderDetails(gstrLoginName)

        cls = Nothing
        GetDoctorName = Nothing
        If IsNothing(dt) = False Then
            Try
                If dt.Rows.Count > 0 Then
                    gnLoginProviderID = dt.Rows(0)("nProviderID")
                    If Trim(dt.Rows(0)("ProviderName")) <> "" Then
                        Return Trim(dt.Rows(0)("ProviderName"))
                    ElseIf Trim(dt.Rows(0)("UserName")) <> "" Then
                        Return Trim(dt.Rows(0)("UserName"))
                    Else
                        Return gstrLoginName


                    End If
                Else
                    gnLoginProviderID = 0
                    Return Nothing
                End If

            Catch ex As Exception
            Finally
                dt.Dispose()
                dt = Nothing
            End Try

        Else
            gnLoginProviderID = 0
            Return Nothing
        End If

    End Function

    <DllImport("wininet.dll")> _
    Private Function InternetGetConnectedState(ByVal Description As Integer, ByVal ReservedValue As Integer) As Boolean
    End Function

    Public Function IsInternetConnectionAvailableAPI() As Boolean
        Dim _returnResult As Boolean = False
        Dim Desc As Integer
        If InternetGetConnectedState(Desc, 0) Then
            _returnResult = True
        Else
            _returnResult = False
        End If
        Return _returnResult
    End Function

    Private Declare Function InternetGetConnectedState Lib "wininet" (ByRef conn As Long, ByVal val As Long) As Boolean

    Public ReadOnly Property IsInternetConnectionAvailable() As Boolean
        Get
            Dim Out As Integer
            If InternetGetConnectedState(Out, 0) = True Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property


#Region "Auto Update Code"

    Public Function CheckforUpdate() As String
        Dim conn As New SqlConnection(GetConnectionString)
        Dim cmd As SqlCommand = Nothing
        Dim dt As New DataTable
        Dim sda As SqlDataAdapter = Nothing
        Dim _strSQL As String = ""
        Dim UpdateID As Int64
        Dim UpdateLocation As String = ""
        Try
            conn.Open()
            _strSQL = "select nClientID, sMachineName, nVoiceEnabled, nScanEnabled, sProductCode, sCurrentProductVersion, sLatestProductVersion, dtUpdatedate, blnIsUpdated, nUpdateID from ClientSettings_MST where blnIsUpdated = 'False' and  sMachineName = '" & gstrClientMachineName & "'"
            sda = New SqlDataAdapter(_strSQL, conn)
            sda.Fill(dt)

            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    MessageBox.Show("Updates are available for your machine.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    UpdateID = dt.Rows(0)("nUpdateID")
                    _strSQL = "select sUpdateLocation from ClinicUpdates_Settings where nUpdateID = '" & UpdateID & "'"
                    cmd = New SqlCommand(_strSQL, conn)
                    UpdateLocation = Convert.ToString(cmd.ExecuteScalar())
                    Return UpdateLocation
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return UpdateLocation
        Finally
            If Not IsNothing(dt) Then
                dt.Dispose()
                dt = Nothing
            End If
            If Not IsNothing(cmd) Then
                cmd.Parameters.Clear()
                cmd.Dispose()
                cmd = Nothing
            End If
            If Not IsNothing(conn) Then
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End If
            If Not IsNothing(sda) Then
                sda.Dispose()
                sda = Nothing
            End If
        End Try
    End Function

#End Region

#Region "View Diagnosis"

    ''check whether the respective exam has any diagnosis
    Private Function IsDiagnosisAvailable(ByVal visitID As Int64, ByVal ExamID As Int64) As Boolean
        Dim con As New SqlClient.SqlConnection(GetConnectionString)
        Dim cmd As New SqlClient.SqlCommand("SELECT COUNT(*) FROM ExamICD9CPT WHERE nExamID = " & ExamID & " AND nVisitID = " & visitID & "", con)

        Try
            Dim oResult As Object
            con.Open()
            oResult = cmd.ExecuteScalar()
            con.Close()
            If CType(oResult, Integer) > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return Nothing
        Finally
            con.Dispose()
            con = Nothing
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
        End Try
    End Function

    'Show the diagnosis for the respective exam,if any 
    Public Sub ShowDiagnosis(ByVal nPatientID As Int64, ByVal nExamID As Int64, ByVal nVisitID As Int64, Optional ByVal DOS As DateTime = Nothing, Optional ByVal sExamName As String = "")
        If nVisitID > 0 And nExamID > 0 Then
            If IsDiagnosisAvailable(nVisitID, nExamID) = True Then
                If gblnICD9Driven Then
                    Dim gen_Diag_Activewindow As IntPtr = gloWord.WordDialogBoxBackgroundCloser.GetForegroundWindow()
                    Dim frm As New frm_Diagnosis(nVisitID, nExamID, nPatientID, True, , gen_Diag_Activewindow)
                    frm.Width = 550
                    frm.pnlrht.Padding = New Padding(3, 3, 0, 0)
                    frm.StartPosition = FormStartPosition.CenterScreen
                    frm.BringToFront()
                    frm.ShowDialog(frm.Parent)
                    frm.Dispose()
                    frm = Nothing
                Else
                    If DOS = Nothing Then
                        DOS = Now.Date
                    End If
                    ''To fix issue:#5323-View Diagnosis" window details must be displayed.
                    Dim frm As New frm_Treatment(nExamID, nVisitID, DOS.Date, sExamName, nPatientID, True)
                    frm.ShowDialog(frm.Parent)
                    frm.Dispose()
                    frm = Nothing
                End If
            Else
                MessageBox.Show("Exam does not have any diagnosis.", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub
#End Region

#Region "Check Word For Exception"
    Public Function CheckWordForException() As Boolean
        Dim ofrmTest As New gloEMRWord.frmDSOTest
        Try
            If ofrmTest.ShowDialog(ofrmTest.Parent) = DialogResult.Yes Then
                Return True
            Else
                MessageBox.Show(ofrmTest.ErrorMessage, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
        Catch ex As Exception
            Return True
        Finally
            If (IsNothing(ofrmTest) = False) Then
                ofrmTest.Dispose()
                ofrmTest = Nothing
            End If
        End Try
    End Function
#End Region

    'To get formatted string
    Public Function GetFormattedString(ByVal str As String, ByVal _blnGetAlphaNumeric As Boolean, Optional ByVal _blnGetDecimal As Boolean = False) As String
        Dim _strRegex As String = ""
        If (_blnGetAlphaNumeric = True) Then
            'allow alphanumeric 
            _strRegex = "([0-9a-zA-Z])"
        Else
            ''Remove  characters other than numerics(0 to 9) and decimal sign (.)
            If (_blnGetDecimal = True) Then
                _strRegex = "([0-9.])"
            Else
                'allow numerics 
                _strRegex = "[0-9]"
            End If
        End If

        ''remove characters other than defined  format (i.e. _strRegex)
        For Each c As Char In str
            If Regex.IsMatch(c.ToString(), _strRegex) = False Then
                str = str.Replace(c.ToString(), "")
            End If
        Next
        Return str
    End Function

    Public Sub LockControls(ByVal Frm As Form, ByVal Pnl1 As Panel, Optional ByVal bVal As Boolean = False)
        'To enable/disable controls on the form - as on 20101018
        Try
            For Each Ctrl As Control In Frm.Controls
                If Ctrl.Parent.Name <> Pnl1.Name And Ctrl.Name <> Pnl1.Name Then
                    Ctrl.Enabled = bVal
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(" Error Occured", gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information)
            For Each Ctrl As Control In Frm.Controls
                If Ctrl.Parent.Name <> Pnl1.Name And Ctrl.Name <> Pnl1.Name Then
                    Ctrl.Enabled = True
                End If
            Next
        Finally

        End Try
    End Sub

    Public Function DeleteRxEligibilityFiles(ByVal strApplicationFilePath As String) As Boolean
        Try
            If strApplicationFilePath <> "" Then

                If Directory.Exists(strApplicationFilePath & "\Outbox") Then
                    Dim sourceDir As String = strApplicationFilePath & "\Outbox"
                    Dim X12List As String() = Directory.GetFiles(sourceDir, "*.X12")
                    For Each f As String In X12List
                    Next
                End If
                If Directory.Exists(strApplicationFilePath & "\Inbox") Then
                    Dim sourceDir As String = strApplicationFilePath & "\Inbox"
                    Dim X12List As String() = Directory.GetFiles(sourceDir, "*.X12")
                    For Each f As String In X12List
                        File.Delete(f)
                    Next
                End If
            End If
            Return True
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString())
            Return False
        End Try
    End Function

    Public Function SetConnectionStringForUnitTest() As Boolean
        Dim _IsValidDatabase As Boolean = False
        If gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR) = False Then
            Return False
        End If
        gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, True)
        If IsNothing(gloRegistrySetting.GetRegistryValue("SQLServer")) = True Then
            gloRegistrySetting.CloseRegistryKey()
            Return False
        End If
        If IsNothing(gloRegistrySetting.GetRegistryValue("Database")) = True Then
            _IsValidDatabase = True
            gloRegistrySetting.CloseRegistryKey()
            Return False
        Else
            _IsValidDatabase = False
        End If
        gstrSQLServerName = gloRegistrySetting.GetRegistryValue("SQLServer")
        gstrDatabaseName = gloRegistrySetting.GetRegistryValue("Database")
        '' SUDHIR 20090727 ''
        If gloRegistrySetting.GetRegistryValue("IsSQLAuthentication") IsNot Nothing Then
            gblnSQLAuthentication = gloRegistrySetting.GetRegistryValue("IsSQLAuthentication")
            If gblnSQLAuthentication = True Then '''' this is used in gloSurescriptGeneral.GetconnectionString()
                gloSureScript.gloSurescriptGeneral.gblnIsSQLAuthentication = True
            End If

        End If
        If gloRegistrySetting.GetRegistryValue("SQLUserEMR") IsNot Nothing Then
            gstrSQLUserEMR = gloRegistrySetting.GetRegistryValue("SQLUserEMR")
        End If
        If gloRegistrySetting.GetRegistryValue("SQLPasswordEMR") IsNot Nothing Then
            If gloRegistrySetting.GetRegistryValue("SQLPasswordEMR") <> "" Then
                Dim oEncryption As New clsencryption
                gstrSQLPasswordEMR = oEncryption.DecryptFromBase64String(gloRegistrySetting.GetRegistryValue("SQLPasswordEMR"), constEncryptDecryptKey)
                oEncryption = Nothing
            End If
        End If
        '' END SUDHIR ''
        If IsNothing(gloRegistrySetting.GetRegistryValue("ServerPath")) = False Then
            gstrServerPath = gloRegistrySetting.GetRegistryValue("ServerPath")
        End If
        'gloRegistrySetting.CloseRegistryKey()
        Return Nothing
    End Function

    ''This function used for retriving connectionstring :Unit test cases-For MU Project
    Public Function SetConnectionStringtoappsettings() As String
        Dim appSettings As System.Collections.Specialized.NameValueCollection = System.Configuration.ConfigurationManager.AppSettings
        appSettings("DataBaseConnectionString") = GetConnectionString()
        Return Nothing
    End Function

    Public Function getUniqueID() As String
        'Static firstTime As Boolean = True
        'Static myWatch As New Stopwatch()
        'Static myTime As DateTime
        'If firstTime = True Then
        '    firstTime = False
        '    myTime = Now()
        '    myWatch.Start()
        'End If
        'Dim TmSp As New TimeSpan(myTime.Ticks + myWatch.ElapsedTicks)
        'getUniqueID = TmSp.Ticks.ToString()
        'TmSp = Nothing
        Return gloGlobal.clsFileExtensions.GetUniqueDateString()
    End Function

    Public Function Scan_n_Lock_FormLevel(ByVal PatID As Long, ByVal VisitID As Long, ByVal TrasnsID As Long, ByVal FormName As String) As DataTable
        Dim Con As New SqlConnection(GetConnectionString)
        Try
            Dim cmd As New SqlCommand("Scan_n_Lock_FormLevel", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As New SqlParameter

            sqlParam = cmd.Parameters.Add("@FormLockingID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.InputOutput
            sqlParam.Value = 0

            sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatID

            sqlParam = cmd.Parameters.Add("@VisitID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = VisitID

            sqlParam = cmd.Parameters.Add("@TransactionID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = TrasnsID

            sqlParam = cmd.Parameters.Add("@FormName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = FormName

            ''Add ProcessID 
            sqlParam = cmd.Parameters.Add("@ProcessID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = System.Diagnostics.Process.GetCurrentProcess().Id

            ''Add TVP All processes
            sqlParam = cmd.Parameters.Add("@TmpFormLocks", SqlDbType.Structured)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = getAllInstances(FormName)

            sqlParam = cmd.Parameters.Add("@MachineName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = gstrClientMachineName

            sqlParam = cmd.Parameters.Add("@UserName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = gstrLoginName

            Dim dt As New DataTable
            Dim da As SqlDataAdapter

            da = New SqlDataAdapter(cmd)
            da.Fill(dt)
            da.Dispose()
            da = Nothing
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
            Return dt

        Catch ex As SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            If Not IsNothing(Con) Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If
        End Try
    End Function

    Public Function Delete_Lock_FormLevel(ByVal FormLevelID As Long, ByVal PatID As Long) As Integer
        Dim Con As New SqlConnection(GetConnectionString)
        Try
            Dim cmd As New SqlCommand("Delete_Lock_FormLevel", Con)
            cmd.CommandType = CommandType.StoredProcedure
            Dim sqlParam As New SqlParameter

            sqlParam = cmd.Parameters.Add("@FormLockingID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = FormLevelID

            sqlParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = PatID

            sqlParam = cmd.Parameters.Add("@ProcessID", SqlDbType.BigInt)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = System.Diagnostics.Process.GetCurrentProcess().Id


            Con.Open()
            cmd.ExecuteNonQuery()
            Con.Close()
            cmd.Parameters.Clear()
            cmd.Dispose()
            cmd = Nothing
        Catch ex As SqlException
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(Con) Then
                Con.Close()
                Con.Dispose()
                Con = Nothing
            End If
        End Try
        Return Nothing
    End Function
    Public Function ValidateInteger(ByVal Text As String, ByVal e As KeyPressEventArgs)
        Try
            If e.KeyChar = "." Then
                e.Handled = True
                ValidateInteger = False
                Exit Function
            End If

            If Char.IsNumber(e.KeyChar) = False And e.KeyChar <> "." And AscW(e.KeyChar) <> 8 Then
                e.Handled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        ValidateInteger = True
    End Function

    Public Sub OpenCDA(ByVal PatientID As Long)
        Try
            Dim objfrm As New frmCCDAGenerateList(PatientID)
            With objfrm
                .nCDAFileType = gloCCDSchema.CDAFileTypeEnum.CareRecordSummary
                .nOrderId = 0
                .sDetail = "Order"
            End With
            'objfrm()

            With objfrm
                .WindowState = FormWindowState.Normal
                .ChkAll.Checked = True
                .CallFromSecureInbox = True
                ' .TopMost = True
                .ShowInTaskbar = False
                .BringToFront()
                .ShowDialog(objfrm.Parent)
            End With
            If Not IsNothing(objfrm) Then
                objfrm.Dispose()
                objfrm = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub openCDAFromword(ByVal OrderID As Int64, ByVal Details As String, Optional ByVal nContactID As Long = 0, Optional ByVal DisablePreferredProvider As Boolean = False, Optional ByVal PatientID As Int64 = 0)

        Try

            Dim objfrm As New frmCCDAGenerateList(PatientID)

            objfrm.nCDAFileType = CDAFileTypeEnum.CareRecordSummary
            objfrm.nOrderId = OrderID
            objfrm.sDetail = Details
            objfrm.ContactID = nContactID
            objfrm.DisablePreferredProvider = DisablePreferredProvider

            With objfrm
                .WindowState = FormWindowState.Normal
                .ChkAll.Checked = True
                .ShowDialog()
            End With
            If Not IsNothing(objfrm) Then
                objfrm.Dispose()
                objfrm = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    ''Added on 20140613-To add clinical chart button on orders and results screen
    Public Sub OpenClinicalChart(ByVal PatientID As Long)
        Try
            Dim ofrmCC As frmClinicalChart = New frmClinicalChart(PatientID)
            'AddHandler ofrmCC.EvntGenerateCDAFromClinicalChart, AddressOf openCDA
            ofrmCC.Visible = False
            ofrmCC.ShowInTaskbar = False
            ofrmCC.StartPosition = FormStartPosition.CenterScreen
            ofrmCC.WindowState = FormWindowState.Maximized
            ofrmCC.ShowDialog()
            ofrmCC.Dispose()
            ofrmCC = Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Dashboard, gloAuditTrail.ActivityCategory.ClinicalChart, gloAuditTrail.ActivityType.Open, "Error while opening clinical charts.", gloAuditTrail.ActivityOutCome.Failure)
        End Try
    End Sub

    Public Sub openCCD(ByVal PatientID As Int64)
        'Code Start - Added by sanjog on 20100707 for CCD
        Try
            'Developer:Mitesh Patel
            'Date:24 May 2012 
            'Bug # 26421 
            'Dim frmlab As frmViewgloLab
            'frmlab = frmViewgloLab.GetInstance()
            'If IsNothing(frmlab) = False Then
            '    If IsNothing(frmlab.LabPatientID) = False Then
            '        If frmlab.LabPatientID <> 0 Then
            '            SelectPatientOnDashboard(frmlab.LabPatientID)
            '        End If
            '    End If
            'End If
            Dim objfrm As New frmCCDGenerateList(PatientID)
            objfrm.ChkResults.Checked = True

            With objfrm
                .WindowState = FormWindowState.Normal
                .BringToFront()
                .StartPosition = FormStartPosition.CenterParent
                .ShowDialog()
            End With
            'SLR: Dispose
            If Not IsNothing(objfrm) Then
                objfrm.Dispose()
                objfrm = Nothing
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Function GetPatientExamProviderMismatchMessage(ByVal ProviderName As String) As String

        Return "This patient belongs to '" & Trim(ProviderName) & "'." & vbCrLf & vbCrLf & "Do you want to change the Patients default Provider?" & vbCrLf & vbCrLf & "[This can also be done from the Patient Demographics screen]."

    End Function

    'Public Function getFontFromExistingSource(existingFont As Font, neededStyle As FontStyle) As Font

    '    If existingFont.Name = gFont.Name Then
    '        If (existingFont.Height = gFont.Height) Then
    '            If (neededStyle = FontStyle.Bold) Then
    '                Return gFont_BOLD
    '            Else
    '                Return gFont
    '            End If
    '        End If
    '        If (existingFont.Height = gFont_SMALL.Height) Then
    '            If (neededStyle = FontStyle.Bold) Then
    '                Return gFont_SMALL_BOLD
    '            Else
    '                Return gFont_SMALL
    '            End If
    '        End If
    '    End If
    '    If existingFont.Name = gFontArial_Regular.Name Then
    '        If (existingFont.Height = gFontArial_Regular.Height) Then
    '            If (neededStyle = FontStyle.Bold) Then
    '                Return gFontArial_Bold
    '            Else
    '                Return gFontArial_Regular
    '            End If
    '        End If
    '        If (existingFont.Height = gFontArial_Big_Regular.Height) Then
    '            If (neededStyle = FontStyle.Bold) Then
    '                Return gFontArial_Big_Bold
    '            Else
    '                Return gFontArial_Big_Regular
    '            End If
    '        End If
    '    End If
    '    If existingFont.Name = gFontVerdana_Regular.Name Then
    '        If (existingFont.Height = gFontVerdana_Regular.Height) Then
    '            If (neededStyle = FontStyle.Bold) Then
    '                Return gFontVerdana_Bold
    '            Else
    '                Return gFontVerdana_Regular
    '            End If
    '        End If
    '    End If
    '    Return New Font(existingFont, neededStyle)

    'End Function


    Public Function GenratePatientInfoPDF(ByVal PatientID As String, ByVal StrPatientInfoPDFPath As String) As String

        Dim dsImgRp As New gloPatient.PatientImage()
        Dim dr As gloPatient.PatientImage.ImageRow = Nothing
        Dim crtableLogoninfo As TableLogOnInfo = Nothing
        Dim crConnectionInfo As New ConnectionInfo()
        Dim oRpt As ReportDocument = Nothing
        Dim CrTables As Tables = Nothing
        Dim myBytes As Byte() = Nothing

        Try

            oRpt = New ReportDocument()
            If System.IO.Directory.Exists(Application.StartupPath + "\Reports") = True Then
                oRpt.Load(Application.StartupPath + "\Reports\rptPatientRegDeta.rpt")
            Else
                MessageBox.Show("Reports Directory does not exists.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return ""
            End If

            crConnectionInfo.ServerName = System.Convert.ToString(AppSettings("SQLServerName"))
            crConnectionInfo.DatabaseName = System.Convert.ToString(AppSettings("DatabaseName"))

            If System.Convert.ToBoolean(AppSettings("WindowAuthentication")) = False Then
                crConnectionInfo.IntegratedSecurity = False
                crConnectionInfo.UserID = System.Convert.ToString(AppSettings("SQLLoginName"))
                crConnectionInfo.Password = System.Convert.ToString(AppSettings("SQLPassword"))
            Else
                crConnectionInfo.IntegratedSecurity = True
            End If
            CrTables = oRpt.Database.Tables



            Dim patientPhotoHeight As Integer = oRpt.ReportDefinition.Sections("GroupHeaderSection1").ReportObjects("Image1").Height
            Dim patientPhotoWidth As Integer = oRpt.ReportDefinition.Sections("GroupHeaderSection1").ReportObjects("Image1").Width
            dr = dsImgRp.Image.NewImageRow()
            myBytes = SaveImage(PatientID, patientPhotoWidth, patientPhotoHeight)
            dr.Image = myBytes
            dsImgRp.Image.Rows.Add(dr)


            For Each CrTable As Table In CrTables

                If (CrTable.Name.ToUpper() = "Image".ToUpper()) Then
                    If dsImgRp.Tables(0).Rows.Count > 0 Then
                        oRpt.Database.Tables("Image").SetDataSource(dsImgRp.Tables(0))
                    End If
                Else

                    crtableLogoninfo = CrTable.LogOnInfo
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo
                    CrTable.ApplyLogOnInfo(crtableLogoninfo)


                    CrTable.Location = ("" & ".dbo.") + CrTable.Name
                End If
            Next

            oRpt.SetParameterValue("PatientID", PatientID)
            oRpt.ExportToDisk(ExportFormatType.PortableDocFormat, StrPatientInfoPDFPath)

            Return StrPatientInfoPDFPath
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
            Return Nothing
        Finally
            dr = Nothing
            myBytes = Nothing

            If Not IsNothing(CrTables) Then
                CrTables.Dispose()
                CrTables = Nothing
            End If


            If Not IsNothing(dsImgRp) Then
                dsImgRp.Dispose()
                dsImgRp = Nothing
            End If
            If Not IsNothing(crtableLogoninfo) Then
                crtableLogoninfo = Nothing
            End If
            If Not IsNothing(crConnectionInfo) Then
                crConnectionInfo = Nothing
            End If
            If Not IsNothing(oRpt) Then
                oRpt.Dispose()
                oRpt = Nothing
            End If
        End Try

    End Function

    Private Function SaveImage(ByVal PatientID As String, ByVal patientPhotoWidth As Integer, ByVal patientPhotoHeight As Integer) As Byte()

        Dim dt_GetPatientImage As DataTable = Nothing
        Dim arrByteImage As Byte() = Nothing

        Try

            dt_GetPatientImage = Getdata(PatientID)
            If dt_GetPatientImage IsNot Nothing Then
                If dt_GetPatientImage.Rows.Count > 0 Then
                    If dt_GetPatientImage.Rows(0)(0).ToString() <> "" Then
                        Return gloPictureBox.gloImage.GetImageByteArray(DirectCast(dt_GetPatientImage.Rows(0)("iPhoto"), Byte()), patientPhotoWidth, patientPhotoHeight)


                    End If
                End If
            End If
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), False)
            ex = Nothing
        Finally
            If dt_GetPatientImage IsNot Nothing Then
                dt_GetPatientImage.Dispose()
                dt_GetPatientImage = Nothing
            End If
        End Try
        Return arrByteImage

    End Function

    Private Function Getdata(ByVal PatientID As String) As DataTable
        Dim dt As New DataTable()
        Dim da As SqlDataAdapter = Nothing
        Try
            Dim str As String = "select iPhoto  from VWRptPatientInfo where PatientId  = '" & PatientID & "'"
            If GetConnectionString() <> "" Then
                Dim sqlConn As String = GetConnectionString()
                Dim conn As New System.Data.SqlClient.SqlConnection(sqlConn)
                da = New System.Data.SqlClient.SqlDataAdapter(str, conn)
                da.Fill(dt)
                conn.Close()
                conn.Dispose()
                conn = Nothing
            End If
            Return dt
        Catch Ex As Exception

            gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), False)
            Ex = Nothing
            Return dt
        Finally
            If da IsNot Nothing Then
                da.Dispose()
                da = Nothing
            End If
        End Try

    End Function

    Public Function GetQueuedPrinterName(nQueuedID As Int64, Optional ByVal isForPrint As Boolean = False) As String
        Dim oPrintername As String = Nothing
        Dim oDB As New gloDatabaseLayer.DBLayer(GetConnectionString())
        Try
            oDB.Connect(False)
            Dim _strSqlQuery As String
            If isForPrint Then
                _strSqlQuery = " Select ISNULL(dbo.ClinicalChartQueueMST_ForPrint.sClaimPrinter,'Default') FROM ClinicalChartQueueMST_ForPrint WHERE dbo.ClinicalChartQueueMST_ForPrint.nQueueID=" & nQueuedID
            Else
                _strSqlQuery = " Select ISNULL(dbo.ClinicalChartQueueMst.sClaimPrinter,'Default') FROM ClinicalChartQueueMst WHERE dbo.ClinicalChartQueueMst.nQueueID=" & nQueuedID
            End If


            ''oDB.Retrive_Query(_strSqlQuery, dtDocument)
            oPrintername = oDB.ExecuteScalar_Query(_strSqlQuery)

            If (oPrintername IsNot Nothing) Then
                If (oPrintername = "") Then
                    Return "Default"
                Else
                    Return oPrintername
                End If

            Else
                Return "Default"
            End If

        Catch ex As gloDatabaseLayer.DBException
            ex.ERROR_Log(ex.ToString())
            Return "Default"
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), True)
            Return "Default"
        Finally
            If oDB IsNot Nothing Then
                oDB.Disconnect()
                oDB.Dispose()
            End If
        End Try
    End Function
End Module
