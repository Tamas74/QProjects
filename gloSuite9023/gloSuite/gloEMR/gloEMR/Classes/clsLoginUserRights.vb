Imports System.Data.SqlClient
Public Class clsLoginUserRights
    ' Private dv As DataView


#Region " Edit Menu"
    'Masters
    Dim _blnICD9 As Boolean
    Dim _blnCPT As Boolean
    Dim _blnDrugs As Boolean
    Dim _blnSIG As Boolean
    Dim _blnTemplateGallery As Boolean
    Dim _blnHistory As Boolean
    Dim _blnContacts As Boolean
    Dim _blnSpeciality As Boolean
    Dim _blnCategory As Boolean
    Dim _blnMedicalCategory As Boolean
    Dim _blnOrdersAndResultsSetup As Boolean
    Dim _blnROS As Boolean
    Dim _blnRadiology As Boolean
    Dim _blnModifier As Boolean
    Dim _blnAppointmentSchedularType As Boolean
    Dim _blnEditFlowsheet As Boolean
    Dim _blnEditFormGallery As Boolean
    Dim _blnDMSCategory As Boolean
    Dim _blnStatusUsers As Boolean
    Dim _blnDrugsConfig As Boolean
    Dim _blnDMSetup As Boolean
    Dim _blnShowDMAlert As Boolean
    Dim _blnViewRecommendation As Boolean
    Dim _blnIMSetup As Boolean
    Dim _blnSmartOrder As Boolean
    Dim _blnSmartDiagnosis As Boolean
    Dim _blnSmartTreatment As Boolean
    Dim _blnICD9CPTGallery As Boolean
    Dim _blnDisclosureSet As Boolean
    Dim _blnPatientSummary As Boolean
    Dim _blnAppointmentBook As Boolean
    'user right added by dipak 20100105 for ProviderSig from userlogin which is not provider
    Dim _blnAssociatedProviderSignature As Boolean
    'user right added by dipak 20100109 for DeceasedPatient to user for accessing /modifying documents of Deceased Patient
    Dim _blnDeceasedPatient As Boolean
    'sarika 20090302
    Dim _blnCVSetup As Boolean
    Dim _blnTaxIDSetup As Boolean
    Dim _blnBillingConfiguration As Boolean
    Dim _blnLiquidData As Boolean
    'Dim _blnAppointmentBook As Boolean

    ''Added Rahul for new user right Durg Interaction on 20101012
    Dim _blnDrugInteraction As Boolean
    ''

    '---

    'Masters

    ''gloCommunity - Added User rights settings on 20120727
    Dim _blnMnugloCommunity As Boolean
    Dim _blnCommunityConnect As Boolean
    Dim _blnMyCommunity As Boolean
    Dim _blnShare As Boolean
    ''End

    '02-May-13 Aniket: Resolving Bug 50030
    Private _blnFamilyMember As Boolean
    Private _blnZip As Boolean
    Private _blnVitalSettings As Boolean

    Private _blnClinicalInstructions As Boolean
    Private _blnCarePlan As Boolean
    Private _blnRCMCategory As Boolean
    Private _blnRCMDocumentManagement As Boolean

    Private _blnCCDASchedule As Boolean
    Private _blnExportSummary As Boolean

    Public Property ExportSummary() As Boolean
        Get
            Return _blnExportSummary
        End Get
        Set(ByVal Value As Boolean)
            _blnExportSummary = Value
        End Set
    End Property

    Public Property CCDASchedule() As Boolean
        Get
            Return _blnCCDASchedule
        End Get
        Set(ByVal Value As Boolean)
            _blnCCDASchedule = Value
        End Set
    End Property

    Public Property VitalSettings() As Boolean
        Get
            Return _blnVitalSettings
        End Get
        Set(ByVal Value As Boolean)
            _blnVitalSettings = Value
        End Set
    End Property


    Public Property Zip() As Boolean
        Get
            Return _blnZip
        End Get
        Set(ByVal Value As Boolean)
            _blnZip = Value
        End Set
    End Property

    Public Property FamilyMember() As Boolean
        Get
            Return _blnFamilyMember
        End Get
        Set(ByVal Value As Boolean)
            _blnFamilyMember = Value
        End Set
    End Property

    Public Property ClinicalInstructions() As Boolean
        Get
            Return _blnClinicalInstructions
        End Get
        Set(ByVal Value As Boolean)
            _blnClinicalInstructions = Value
        End Set
    End Property

    Public Property CarePlan() As Boolean
        Get
            Return _blnCarePlan
        End Get
        Set(ByVal Value As Boolean)
            _blnCarePlan = Value
        End Set
    End Property

    Public Property PatientSummary() As Boolean
        Get
            Return _blnPatientSummary
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientSummary = Value
        End Set
    End Property

    Public Property DisclosureSet() As Boolean
        Get
            Return _blnDisclosureSet
        End Get
        Set(ByVal Value As Boolean)
            _blnDisclosureSet = Value
        End Set
    End Property
    ''Added Rahul for new user right Durg Interaction on 20101012
    Public Property DrugInteraction() As Boolean
        Get
            Return _blnDrugInteraction
        End Get
        Set(ByVal Value As Boolean)
            _blnDrugInteraction = Value
        End Set
    End Property
    Public Property ICD9CPTGallery() As Boolean
        Get
            Return _blnICD9CPTGallery
        End Get
        Set(ByVal Value As Boolean)
            _blnICD9CPTGallery = Value
        End Set
    End Property

    Public Property DrugsConfig() As Boolean
        Get
            Return _blnDrugsConfig
        End Get
        Set(ByVal Value As Boolean)
            _blnDrugsConfig = Value
        End Set
    End Property
    Public Property DMSetup() As Boolean
        Get
            Return _blnDMSetup
        End Get
        Set(ByVal Value As Boolean)
            _blnDMSetup = Value
        End Set
    End Property
    Public Property IMSetup() As Boolean
        Get
            Return _blnIMSetup
        End Get
        Set(ByVal Value As Boolean)
            _blnIMSetup = Value
        End Set
    End Property
    Public Property ShowDMAlert() As Boolean
        Get
            Return _blnShowDMAlert
        End Get
        Set(ByVal Value As Boolean)
            _blnShowDMAlert = Value
        End Set
    End Property

    Public Property ViewRecommendation() As Boolean
        Get
            Return _blnViewRecommendation
        End Get
        Set(ByVal Value As Boolean)
            _blnViewRecommendation = Value
        End Set
    End Property

    Public Property SmartDiagnosis() As Boolean
        Get
            Return _blnSmartDiagnosis
        End Get
        Set(ByVal Value As Boolean)
            _blnSmartDiagnosis = Value
        End Set
    End Property

    Public Property SmartTreatment() As Boolean
        Get
            Return _blnSmartTreatment
        End Get
        Set(ByVal Value As Boolean)
            _blnSmartTreatment = Value
        End Set
    End Property
    Public Property SmartOrder() As Boolean
        Get
            Return _blnSmartOrder
        End Get
        Set(ByVal Value As Boolean)
            _blnSmartOrder = Value
        End Set
    End Property
    Public Property Modifier() As Boolean
        Get
            Return _blnModifier
        End Get
        Set(ByVal Value As Boolean)
            _blnModifier = Value
        End Set
    End Property
    Public Property Radiology() As Boolean
        Get
            Return _blnRadiology
        End Get
        Set(ByVal Value As Boolean)
            _blnRadiology = Value
        End Set
    End Property
    Public Property ROS() As Boolean
        Get
            Return _blnROS
        End Get
        Set(ByVal Value As Boolean)
            _blnROS = Value
        End Set
    End Property
    Public Property OrdersAndResultsSetup() As Boolean
        Get
            Return _blnOrdersAndResultsSetup
        End Get
        Set(ByVal Value As Boolean)
            _blnOrdersAndResultsSetup = Value
        End Set
    End Property
    Public Property Category() As Boolean
        Get
            Return _blnCategory
        End Get
        Set(ByVal Value As Boolean)
            _blnCategory = Value
        End Set
    End Property

    Public Property MedicalCategory() As Boolean
        Get
            Return _blnMedicalCategory
        End Get
        Set(ByVal Value As Boolean)
            _blnMedicalCategory = Value
        End Set
    End Property

    Public Property Speciality() As Boolean
        Get
            Return _blnSpeciality
        End Get
        Set(ByVal Value As Boolean)
            _blnSpeciality = Value
        End Set
    End Property
    Public Property Contacts() As Boolean
        Get
            Return _blnContacts
        End Get
        Set(ByVal Value As Boolean)
            _blnContacts = Value
        End Set
    End Property
    Public Property History() As Boolean
        Get
            Return _blnHistory
        End Get
        Set(ByVal Value As Boolean)
            _blnHistory = Value
        End Set
    End Property
    Public Property TemplateGallery() As Boolean
        Get
            Return _blnTemplateGallery
        End Get
        Set(ByVal Value As Boolean)
            _blnTemplateGallery = Value
        End Set
    End Property
    Public Property SIG() As Boolean
        Get
            Return _blnSIG
        End Get
        Set(ByVal Value As Boolean)
            _blnSIG = Value
        End Set
    End Property
    Public Property Drugs() As Boolean
        Get
            Return _blnDrugs
        End Get
        Set(ByVal Value As Boolean)
            _blnDrugs = Value
        End Set
    End Property
    Public Property CPT() As Boolean
        Get
            Return _blnCPT
        End Get
        Set(ByVal Value As Boolean)
            _blnCPT = Value
        End Set
    End Property
    Public Property ICD9() As Boolean
        Get
            Return _blnICD9
        End Get
        Set(ByVal Value As Boolean)
            _blnICD9 = Value
        End Set
    End Property
    Public Property AppointmentSchedularType() As Boolean
        Get
            Return _blnAppointmentSchedularType
        End Get
        Set(ByVal Value As Boolean)
            _blnAppointmentSchedularType = Value
        End Set
    End Property
    Public Property Flowsheet() As Boolean
        Get
            Return _blnEditFlowsheet
        End Get
        Set(ByVal Value As Boolean)
            _blnEditFlowsheet = Value
        End Set
    End Property
    'sarika 1/2
    Public Property EditFormGallery() As Boolean
        Get
            Return _blnEditFormGallery
        End Get
        Set(ByVal Value As Boolean)
            _blnEditFormGallery = Value
        End Set
    End Property
    Public Property DMSCategory() As Boolean
        Get
            Return _blnDMSCategory
        End Get
        Set(ByVal Value As Boolean)
            _blnDMSCategory = Value
        End Set
    End Property

    Public Property StatusUsers() As Boolean
        Get
            Return _blnStatusUsers
        End Get
        Set(ByVal Value As Boolean)
            _blnStatusUsers = Value
        End Set
    End Property

    Public Property AppointmentBook() As Boolean
        Get
            Return _blnAppointmentBook
        End Get
        Set(ByVal Value As Boolean)
            _blnAppointmentBook = Value
        End Set
    End Property

    Public Property AssociatedProviderSignature() As Boolean
        Get
            Return _blnAssociatedProviderSignature
        End Get
        Set(ByVal Value As Boolean)
            _blnAssociatedProviderSignature = Value
        End Set
    End Property
    'user right added by dipak 20100109 for DeceasedPatient to user for accessing /modifying documents of Deceased Patient

    Public Property DeceasedPatient() As Boolean
        Get
            Return _blnDeceasedPatient
        End Get
        Set(ByVal Value As Boolean)
            _blnDeceasedPatient = Value
        End Set
    End Property
    'sarika 20090302
    Public Property CVSetup() As Boolean
        Get
            Return _blnCVSetup
        End Get
        Set(ByVal value As Boolean)
            _blnCVSetup = value
        End Set
    End Property

    Public Property TaxIDSetup() As Boolean
        Get
            Return _blnTaxIDSetup
        End Get
        Set(ByVal value As Boolean)
            _blnTaxIDSetup = value
        End Set
    End Property


    Public Property BillingConfiguration() As Boolean
        Get
            Return _blnBillingConfiguration
        End Get
        Set(ByVal value As Boolean)
            _blnBillingConfiguration = value
        End Set
    End Property

    Public Property LiquidData() As Boolean
        Get
            Return _blnLiquidData
        End Get
        Set(ByVal value As Boolean)
            _blnLiquidData = value
        End Set
    End Property


    '---

    ''gloCommunity - Added User rights settings on 20120727
    Public Property gloCommunityMnu() As Boolean
        Get
            Return _blnMnugloCommunity
        End Get
        Set(ByVal Value As Boolean)
            _blnMnugloCommunity = Value
        End Set
    End Property
    Public Property CommunityConnect() As Boolean
        Get
            Return _blnCommunityConnect
        End Get
        Set(ByVal Value As Boolean)
            _blnCommunityConnect = Value
        End Set
    End Property
    Public Property MyCommunity() As Boolean
        Get
            Return _blnMyCommunity
        End Get
        Set(ByVal Value As Boolean)
            _blnMyCommunity = Value
        End Set
    End Property
    Public Property Share() As Boolean
        Get
            Return _blnShare
        End Get
        Set(ByVal Value As Boolean)
            _blnShare = Value
        End Set
    End Property

    Public Property RCMCategory() As Boolean
        Get
            Return _blnRCMCategory
        End Get
        Set(ByVal Value As Boolean)
            _blnRCMCategory = Value
        End Set
    End Property

    Public Property RCMDocumentManagement() As Boolean
        Get
            Return _blnRCMDocumentManagement
        End Get
        Set(ByVal Value As Boolean)
            _blnRCMDocumentManagement = Value
        End Set
    End Property
    ''End

#End Region

#Region " Go Menu "
    'Patient
    Dim _blnPatientRegistration As Boolean
    Dim _blnModifyPatient As Boolean
    Dim _blnPatientCards As Boolean
    Dim _blnPatientROS As Boolean
    Dim _blnPatientHistory As Boolean
    Dim _blnPatientPrescription As Boolean
    Dim _blnPatientMedication As Boolean
    Dim _blnPatientNewExam As Boolean
    Dim _blnPatientPastExam As Boolean
    Dim _blnUnfinished As Boolean
    Dim _blnPatientVitals As Boolean
    Dim _blnPatientFlowsheet As Boolean
    Dim _blnPatientLetters As Boolean
    Dim _blnRadiologyOrders As Boolean
    Dim _blnOrdersAndResults As Boolean
    Dim _blnPullCharts As Boolean
    Dim _blnPTProtocols As Boolean
    Dim _blnMessages As Boolean
    Dim _blnUploadVideo As Boolean
    Dim _blnPatientConcent As Boolean
    Dim _blnDislocureMgmt As Boolean
    Dim _blnFormGallery As Boolean
    Dim _blnAddTasks As Boolean
    Dim _blnImmunization As Boolean
    Dim _blnPatientHealthPlan As Boolean
    Dim _blnCalendar As Boolean
    Dim _blnDocMgmt As Boolean
    Dim _blnNurseNotes As Boolean
    Dim _blnSetAlert As Boolean
    Dim _blnSetSurgicalAlert As Boolean

    Dim _blnTriage As Boolean


    'sarika 20090302
    Dim _blnCharges As Boolean
    Dim _blnBatch As Boolean
    Dim _blnPayment As Boolean
    Dim _blnRemittance As Boolean
    Dim _blnBalance As Boolean
    Dim _blnLedger As Boolean


    Dim _blnViewCCDFiles As Boolean

    '--
    ' <summary>
    ''Sandip Darade 20090821
    Dim _blnAppointment As Boolean
    Dim _blnPatientForms As Boolean
    'Sandip Darade 20090828
    Dim _blnClosedJournals As Boolean
    '' chetan integrated for change password access right on 26 oct 2010
    Dim _blnchangepass As Boolean
    Dim _blnPatientSaving As Boolean

    ''added for satisfying 2015 certification SocialPsychologicalBehavioralObservations new criteria
    Dim _blnSocialPsychologicalBehavioralObservations As Boolean
    Dim _blnImplantableDevices As Boolean
    Dim _blnPatientImplantableDevices As Boolean
    Dim _blnPlanOfTreatment As Boolean

    Private bPrescriptionProviderAssociation As Boolean
    Public Property PrescriptionProviderAssociation() As Boolean
        Get
            Return bPrescriptionProviderAssociation
        End Get
        Set(ByVal value As Boolean)
            bPrescriptionProviderAssociation = value
        End Set
    End Property

    Private bMergeOrder As Boolean
    Public Property MergeOrder() As Boolean
        Get
            Return bMergeOrder
        End Get
        Set(ByVal value As Boolean)
            bMergeOrder = value
        End Set
    End Property


    Public Property PatientSaving() As Boolean
        Get
            Return _blnPatientSaving
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientSaving = Value
        End Set
    End Property
    Public Property ChangePassword() As Boolean
        Get
            Return _blnchangepass
        End Get
        Set(ByVal Value As Boolean)
            _blnchangepass = Value
        End Set
    End Property

    Public Property ClosedJournals() As Boolean
        Get
            Return _blnClosedJournals
        End Get
        Set(ByVal Value As Boolean)
            _blnClosedJournals = Value
        End Set
    End Property
    Public Property Appointment() As Boolean
        Get
            Return _blnAppointment
        End Get
        Set(ByVal Value As Boolean)
            _blnAppointment = Value
        End Set
    End Property

    Public Property PatientForms() As Boolean
        Get
            Return _blnPatientForms
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientForms = Value
        End Set
    End Property
    Public Property SetSurgicalAlert() As Boolean
        Get
            Return _blnSetSurgicalAlert
        End Get
        Set(ByVal Value As Boolean)
            _blnSetSurgicalAlert = Value
        End Set
    End Property

    Public Property SetAlert() As Boolean
        Get
            Return _blnSetAlert
        End Get
        Set(ByVal Value As Boolean)
            _blnSetAlert = Value
        End Set
    End Property

    'Patient
    Public Property Messages() As Boolean
        Get
            Return _blnMessages
        End Get
        Set(ByVal Value As Boolean)
            _blnMessages = Value
        End Set
    End Property

    Public Property PullCharts() As Boolean
        Get
            Return _blnPullCharts
        End Get
        Set(ByVal Value As Boolean)
            _blnPullCharts = Value
        End Set
    End Property

    Public Property RadiologyOrders() As Boolean
        Get
            Return _blnRadiologyOrders
        End Get
        Set(ByVal Value As Boolean)
            _blnRadiologyOrders = Value
        End Set
    End Property
    Public Property OrdersAndResults() As Boolean
        Get
            Return _blnOrdersAndResults
        End Get
        Set(ByVal Value As Boolean)
            _blnOrdersAndResults = Value
        End Set
    End Property
    Public Property DislocureMgmt() As Boolean
        Get
            Return _blnDislocureMgmt
        End Get
        Set(ByVal Value As Boolean)
            _blnDislocureMgmt = Value
        End Set
    End Property
    Public Property FormGallery() As Boolean
        Get
            Return _blnFormGallery
        End Get
        Set(ByVal Value As Boolean)
            _blnFormGallery = Value
        End Set
    End Property
    Public Property Tasks() As Boolean
        Get
            Return _blnAddTasks
        End Get
        Set(ByVal Value As Boolean)
            _blnAddTasks = Value
        End Set
    End Property
    Public Property Immunization() As Boolean
        Get
            Return _blnImmunization
        End Get
        Set(ByVal Value As Boolean)
            _blnImmunization = Value
        End Set
    End Property
    Public Property PatientHealthPlan() As Boolean
        Get
            Return _blnPatientHealthPlan
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientHealthPlan = Value
        End Set
    End Property
    Public Property Unfinished() As Boolean
        Get
            Return _blnUnfinished
        End Get
        Set(ByVal Value As Boolean)
            _blnUnfinished = Value
        End Set
    End Property
    Public Property PatientRegistration() As Boolean
        Get
            Return _blnPatientRegistration
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientRegistration = Value
        End Set
    End Property
    Public Property ModifyPatient() As Boolean
        Get
            Return _blnModifyPatient
        End Get
        Set(ByVal Value As Boolean)
            _blnModifyPatient = Value
        End Set
    End Property

    Public Property PatientCards() As Boolean
        Get
            Return _blnPatientCards
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientCards = Value
        End Set
    End Property

    Public Property PatientHistory() As Boolean
        Get
            Return _blnPatientHistory
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientHistory = Value
        End Set
    End Property
    Public Property PatientPrescription() As Boolean
        Get
            Return _blnPatientPrescription
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientPrescription = Value
        End Set
    End Property
    Public Property PatientMedication() As Boolean
        Get
            Return _blnPatientMedication
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientMedication = Value
        End Set
    End Property
    Public Property PatientNewExam() As Boolean
        Get
            Return _blnPatientNewExam
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientNewExam = Value
        End Set
    End Property
    Public Property PatientPastExam() As Boolean
        Get
            Return _blnPatientPastExam
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientPastExam = Value
        End Set
    End Property
    Public Property PatientVitals() As Boolean
        Get
            Return _blnPatientVitals
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientVitals = Value
        End Set
    End Property

    Public Property PatientROS() As Boolean
        Get
            Return _blnPatientROS
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientROS = Value
        End Set
    End Property
    Public Property PatientFlowsheet() As Boolean
        Get
            Return _blnPatientFlowsheet
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientFlowsheet = Value
        End Set
    End Property
    Public Property PatientLetters() As Boolean
        Get
            Return _blnPatientLetters
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientLetters = Value
        End Set
    End Property
    ''Mayuri
    Public Property Triage() As Boolean
        Get
            Return _blnTriage
        End Get
        Set(ByVal Value As Boolean)
            _blnTriage = Value
        End Set
    End Property
    ''added for user rights changes
    Public Property GoNewSchedule() As Boolean
        Get
            Return _blnGoNewSchedule
        End Get
        Set(ByVal Value As Boolean)
            _blnGoNewSchedule = Value
        End Set
    End Property
    ''added for user rights changes
    Public Property GoProblem() As Boolean
        Get
            Return _blnGoProblem
        End Get
        Set(ByVal Value As Boolean)
            _blnGoProblem = Value
        End Set
    End Property
    ''added for user rights changes
    Public Property GoClinicalChartPrintQueue() As Boolean
        Get
            Return _blnGoClinicalChartPrintQueue
        End Get
        Set(ByVal Value As Boolean)
            _blnGoClinicalChartPrintQueue = Value
        End Set
    End Property

    ''added for user rights changes
    Public Property GoSocialPsychologicalBehavioralObservations() As Boolean
        Get
            Return _blnSocialPsychologicalBehavioralObservations
        End Get
        Set(ByVal Value As Boolean)
            _blnSocialPsychologicalBehavioralObservations = Value
        End Set
    End Property

    Public Property EditImplantableDevices() As Boolean
        Get
            Return _blnImplantableDevices
        End Get
        Set(ByVal Value As Boolean)
            _blnImplantableDevices = Value
        End Set
    End Property
    Public Property GoPatientImplantableDevices() As Boolean
        Get
            Return _blnPatientImplantableDevices
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientImplantableDevices = Value
        End Set
    End Property

    ''added for user rights changes
    Public Property GoAppointment() As Boolean
        Get
            Return _blnGoAppointment
        End Get
        Set(ByVal Value As Boolean)
            _blnGoAppointment = Value
        End Set
    End Property
    ''
    Public Property PatientConcent() As Boolean
        Get
            Return _blnPatientConcent
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientConcent = Value
        End Set
    End Property

    Public Property PatientPTProtocols() As Boolean
        Get
            Return _blnPTProtocols
        End Get
        Set(ByVal Value As Boolean)
            _blnPTProtocols = Value
        End Set
    End Property

    Public Property UploadVideo() As Boolean
        Get
            Return _blnUploadVideo
        End Get
        Set(ByVal Value As Boolean)
            _blnUploadVideo = Value
        End Set
    End Property

    Public Property Calendar() As Boolean
        Get
            Return _blnCalendar
        End Get
        Set(ByVal Value As Boolean)
            _blnCalendar = Value
        End Set
    End Property

    Public Property DocMgmt() As Boolean
        Get
            Return _blnDocMgmt
        End Get
        Set(ByVal Value As Boolean)
            _blnDocMgmt = Value
        End Set
    End Property
    Public Property NurseNotes() As Boolean
        Get
            Return _blnNurseNotes
        End Get
        Set(ByVal Value As Boolean)
            _blnNurseNotes = Value
        End Set
    End Property


    'sarika 20090302
    Public Property Charges() As Boolean
        Get
            Return _blnCharges
        End Get
        Set(ByVal value As Boolean)
            _blnCharges = value
        End Set
    End Property

    Public Property Batch() As Boolean
        Get
            Return _blnBatch
        End Get
        Set(ByVal value As Boolean)
            _blnBatch = value
        End Set
    End Property

    Public Property Payment() As Boolean
        Get
            Return _blnPayment
        End Get
        Set(ByVal value As Boolean)
            _blnPayment = value
        End Set
    End Property

    Public Property Remittance() As Boolean
        Get
            Return _blnRemittance
        End Get
        Set(ByVal value As Boolean)
            _blnRemittance = value
        End Set
    End Property

    Public Property Balance() As Boolean
        Get
            Return _blnBalance
        End Get
        Set(ByVal value As Boolean)
            _blnBalance = value
        End Set
    End Property


    Public Property Ledger() As Boolean
        Get
            Return _blnLedger
        End Get
        Set(ByVal value As Boolean)
            _blnLedger = value
        End Set
    End Property


    Public Property ViewCCDFiles() As Boolean
        Get
            Return _blnViewCCDFiles
        End Get
        Set(ByVal value As Boolean)
            _blnViewCCDFiles = value
        End Set
    End Property

    Public Property PlanOfTreatment() As Boolean
        Get
            Return _blnPlanOfTreatment
        End Get
        Set(ByVal value As Boolean)
            _blnPlanOfTreatment = value
        End Set
    End Property
    '----
#End Region

#Region " View Menu"
    'View
    'sarika
    Dim _blnViewDiscloures As Boolean

    Dim _blnViewReferrals As Boolean
    Dim _blnViewTasks As Boolean
    Dim _blnviewCompleteOtherUsersTasks As Boolean
    Dim _blnViewFormGallery As Boolean
    'Dim _blnViewPatientEducation As Boolean
    Dim _blnViewPatientVideo As Boolean
    Dim _blnViewPatientLetters As Boolean
    Dim _blnViewPTProtocols As Boolean
    Dim _blnViewPatientConcent As Boolean
    Dim _blnViewPatientConcentTracking As Boolean
    Dim _blnViewReceivedFAXes As Boolean
    Dim _blnViewMessages As Boolean
    Dim _blnViewVitals As Boolean

    Dim _blnviewSummaryCareRecord As Boolean
    ''Added Code Changes for View OBVitals
    Dim _blnViewOBVitals As Boolean
    Dim _blnViewOutstandingOrders As Boolean
    Dim _blnViewPendingRefillRequest As Boolean
    Dim _blnViewErrorMsg As Boolean
    Dim _blnVWNurseNotes As Boolean
    Dim _blnViewPatientSummary As Boolean

    Dim _blnViewPendingLabOrders As Boolean
    Dim _blnViewCompleteTaskforallUsers As Boolean

    'sarika 20090302
    Dim _blnMails As Boolean
    Dim _blnDICOM As Boolean
    Dim _blnPatientSynopsis As Boolean
    Dim _blnPatientConfidential As Boolean
    Dim _blnPatientChiefComplaints As Boolean

    Dim _blnCardioVascularRisk As Boolean
    Dim _blnSchedule As Boolean

    'For Recover Exam Module 
    Dim _blnRecoverExam As Boolean
    Dim _bViewCDAErrors As Boolean

    'For Recover Exam Module 
    Public Property ViewRecoverExam() As Boolean
        Get
            Return _blnRecoverExam
        End Get
        Set(ByVal Value As Boolean)
            _blnRecoverExam = Value
        End Set
    End Property

    '-----
    Public Property ViewPatientSummary() As Boolean
        Get
            Return _blnViewPatientSummary
        End Get
        Set(ByVal Value As Boolean)
            _blnViewPatientSummary = Value
        End Set
    End Property
    Public Property ViewNurseNotes() As Boolean
        Get
            Return _blnVWNurseNotes
        End Get
        Set(ByVal Value As Boolean)
            _blnVWNurseNotes = Value
        End Set
    End Property
    Public Property ViewPendingRefillRequest() As Boolean
        Get
            Return _blnViewPendingRefillRequest
        End Get
        Set(ByVal Value As Boolean)
            _blnViewPendingRefillRequest = Value
        End Set
    End Property

    Public Property ViewErrorMessages() As Boolean
        Get
            Return _blnViewErrorMsg
        End Get
        Set(ByVal Value As Boolean)
            _blnViewErrorMsg = Value
        End Set
    End Property

    Public Property ViewOutstandingOrders() As Boolean
        Get
            Return _blnViewOutstandingOrders
        End Get
        Set(ByVal Value As Boolean)
            _blnViewOutstandingOrders = Value
        End Set
    End Property

    Public Property ViewDiscloures() As Boolean
        Get
            Return _blnViewDiscloures
        End Get
        Set(ByVal Value As Boolean)
            _blnViewDiscloures = Value
        End Set
    End Property

    Public Property ViewReferrals() As Boolean
        Get
            Return _blnViewReferrals
        End Get
        Set(ByVal Value As Boolean)
            _blnViewReferrals = Value
        End Set
    End Property
    Public Property ViewTasks() As Boolean
        Get
            Return _blnViewTasks
        End Get
        Set(ByVal Value As Boolean)
            _blnViewTasks = Value
        End Set
    End Property
    Public Property viewCompleteOtherUsersTasks() As Boolean  ''setting added to enable/disable other user dropdown to view other user task
        Get
            Return _blnviewCompleteOtherUsersTasks
        End Get
        Set(ByVal Value As Boolean)
            _blnviewCompleteOtherUsersTasks = Value


        End Set
    End Property
    Public Property ViewFormGallery() As Boolean
        Get
            Return _blnViewFormGallery
        End Get
        Set(ByVal Value As Boolean)
            _blnViewFormGallery = Value
        End Set
    End Property
    'Public Property ViewPatientEducation() As Boolean
    '    Get
    '        'Return _blnViewPatientEducation
    '        'Changes made by Ashish on 2nd June 2014
    '        'Shifted this functionality to  Go -> Patient Education
    '        Return True
    '    End Get
    '    Set(ByVal Value As Boolean)
    '        _blnViewPatientEducation = Value
    '    End Set
    'End Property

    Public Property ViewPatientVideo() As Boolean
        Get
            Return _blnViewPatientVideo
        End Get
        Set(ByVal Value As Boolean)
            _blnViewPatientVideo = Value
        End Set
    End Property

    Public Property ViewReceivedFaxes() As Boolean
        Get
            Return _blnViewReceivedFAXes
        End Get
        Set(ByVal Value As Boolean)
            _blnViewReceivedFAXes = Value
        End Set
    End Property

    Public Property ViewPatientConcent() As Boolean
        Get
            Return _blnViewPatientConcent
        End Get
        Set(ByVal Value As Boolean)
            _blnViewPatientConcent = Value
        End Set
    End Property

    Public Property ViewPatientConcentTracking() As Boolean
        Get
            Return _blnViewPatientConcentTracking
        End Get
        Set(ByVal Value As Boolean)
            _blnViewPatientConcentTracking = Value
        End Set
    End Property
    Public Property PatientViewVitals() As Boolean
        Get
            Return _blnViewVitals
        End Get
        Set(ByVal Value As Boolean)
            _blnViewVitals = Value
        End Set
    End Property
    'Added Code Changes for View OBVitals
    Public Property ViewOBVitals() As Boolean
        Get
            Return _blnViewOBVitals
        End Get
        Set(ByVal Value As Boolean)
            _blnViewOBVitals = Value
        End Set
    End Property


    Public Property ViewPTProtocols() As Boolean
        Get
            Return _blnViewPTProtocols
        End Get
        Set(ByVal Value As Boolean)
            _blnViewPTProtocols = Value
        End Set
    End Property

    Public Property ViewMessages() As Boolean
        Get
            Return _blnViewMessages
        End Get
        Set(ByVal Value As Boolean)
            _blnViewMessages = Value
        End Set
    End Property

    Public Property ViewPatientLetters() As Boolean
        Get
            Return _blnViewPatientLetters
        End Get
        Set(ByVal Value As Boolean)
            _blnViewPatientLetters = Value
        End Set
    End Property

    Public Property ViewPendingLabOrders() As Boolean
        Get
            Return _blnViewPendingLabOrders
        End Get
        Set(ByVal Value As Boolean)
            _blnViewPendingLabOrders = Value
        End Set
    End Property

    'sarika 20090302

    Public Property Mails() As Boolean
        Get
            Return _blnMails
        End Get
        Set(ByVal value As Boolean)
            _blnMails = value
        End Set
    End Property

    Public Property DICOM() As Boolean
        Get
            Return _blnDICOM
        End Get
        Set(ByVal value As Boolean)
            _blnDICOM = value
        End Set
    End Property

    Public Property PatientSynopsis() As Boolean
        Get
            Return _blnPatientSynopsis
        End Get
        Set(ByVal value As Boolean)
            _blnPatientSynopsis = value
        End Set
    End Property

    Public Property PatientConfidential() As Boolean
        Get
            Return _blnPatientConfidential
        End Get
        Set(ByVal value As Boolean)
            _blnPatientConfidential = value
        End Set
    End Property

    Public Property PatientChiefComplaints() As Boolean
        Get
            Return _blnPatientChiefComplaints
        End Get
        Set(ByVal value As Boolean)
            _blnPatientChiefComplaints = value
        End Set
    End Property



    Public Property CardioVascularRisk() As Boolean
        Get
            Return _blnCardioVascularRisk
        End Get
        Set(ByVal value As Boolean)
            _blnCardioVascularRisk = value
        End Set
    End Property
    Public Property ViewSchedule() As Boolean
        Get
            Return _blnSchedule
        End Get
        Set(ByVal Value As Boolean)
            _blnSchedule = Value
        End Set
    End Property
    Public Property ViewCompleteTaskforallUsers() As Boolean
        Get
            Return _blnViewCompleteTaskforallUsers
        End Get
        Set(ByVal Value As Boolean)
            _blnViewCompleteTaskforallUsers = Value
        End Set
    End Property

    Public Property ViewCDAErrros() As String
        Get
            Return _bViewCDAErrors
        End Get
        Set(value As String)
            _bViewCDAErrors = value
        End Set
    End Property
    Public Property ViewSummaryCareRecord() As Boolean
        Get
            Return _blnviewSummaryCareRecord
        End Get
        Set(ByVal Value As Boolean)
            _blnviewSummaryCareRecord = Value

        End Set
    End Property
    '----

#End Region

#Region " Tools Menu "
    Dim _blnMergePatient As Boolean
    Dim _blnMigrateDiagnosis As Boolean
    Dim _blnPatientControl As Boolean
    Dim _blnExportTemplates As Boolean
    Dim _blnImportTemplates As Boolean
    Dim _blnUpdateExisting As Boolean
    Dim _blnUpdateOther As Boolean
    Dim _blnUpgradeTemplates As Boolean
    Dim _blnUnlockRecords As Boolean
    Dim _blnImportVitals As Boolean

    'sarika 20090302
    Dim _blnSettings As Boolean
    Dim _blnLoadDefaultDisplaySettings As Boolean
    Dim _blnImportVitalGraphData As Boolean
    Dim _blnTimeSynchronizationwithServer As Boolean
    Dim _blnToolbar As Boolean
    Dim _blnStatusbar As Boolean
    Dim _blnImportCCD As Boolean
    Dim _blnImportRestrictedCCD As Boolean
    Dim _blnGenerateCCD As Boolean
    Dim _blnCDS As Boolean

    '---
    '' Added on 20090623 
    ''Ref:  GLO2009-0002313	- Ability to disable the clear patients documents
    Dim _blnClearPatientDocs As Boolean
    Dim _blnSecureMessages As Boolean
    Public Property MigrateDiagnosis() As Boolean
        Get
            Return _blnMigrateDiagnosis
        End Get
        Set(ByVal Value As Boolean)
            _blnMigrateDiagnosis = Value
        End Set
    End Property

    Public Property PatientControl() As Boolean
        Get
            Return _blnPatientControl
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientControl = Value
        End Set
    End Property

    Public Property ExportTemplates() As Boolean
        Get
            Return _blnExportTemplates
        End Get
        Set(ByVal Value As Boolean)
            _blnExportTemplates = Value
        End Set
    End Property

    Public Property ImportTemplates() As Boolean
        Get
            Return _blnImportTemplates
        End Get
        Set(ByVal Value As Boolean)
            _blnImportTemplates = Value
        End Set
    End Property

    Public Property UpdateExisting() As Boolean
        Get
            Return _blnUpdateExisting
        End Get
        Set(ByVal Value As Boolean)
            _blnUpdateExisting = Value
        End Set
    End Property

    Public Property UpdateOther() As Boolean
        Get
            Return _blnUpdateOther
        End Get
        Set(ByVal Value As Boolean)
            _blnUpdateOther = Value
        End Set
    End Property

    Public Property UpgradeTemplates() As Boolean
        Get
            Return _blnUpgradeTemplates
        End Get
        Set(ByVal Value As Boolean)
            _blnUpgradeTemplates = Value
        End Set
    End Property

    Public Property UnlockRecords() As Boolean
        Get
            Return _blnUnlockRecords
        End Get
        Set(ByVal Value As Boolean)
            _blnUnlockRecords = Value
        End Set
    End Property

    Public Property ImportVitals() As Boolean
        Get
            Return _blnImportVitals
        End Get
        Set(ByVal Value As Boolean)
            _blnImportVitals = Value
        End Set
    End Property

    Public Property MergePatient() As Boolean
        Get
            Return _blnMergePatient
        End Get
        Set(ByVal Value As Boolean)
            _blnMergePatient = Value
        End Set
    End Property

    'sarika 20090302
    Public Property Settings() As Boolean
        Get
            Return _blnSettings
        End Get
        Set(ByVal Value As Boolean)
            _blnSettings = Value
        End Set
    End Property

    Public Property LoadDefaultDisplaySettings() As Boolean
        Get
            Return _blnLoadDefaultDisplaySettings
        End Get
        Set(ByVal Value As Boolean)
            _blnLoadDefaultDisplaySettings = Value
        End Set
    End Property

    Public Property ImportVitalGraphData() As Boolean
        Get
            Return _blnImportVitalGraphData
        End Get
        Set(ByVal Value As Boolean)
            _blnImportVitalGraphData = Value
        End Set
    End Property

    Public Property TimeSynchronizationwithServer() As Boolean
        Get
            Return _blnTimeSynchronizationwithServer
        End Get
        Set(ByVal Value As Boolean)
            _blnTimeSynchronizationwithServer = Value
        End Set
    End Property

    Public Property Toolbar() As Boolean
        Get
            Return _blnToolbar
        End Get
        Set(ByVal Value As Boolean)
            _blnToolbar = Value
        End Set
    End Property

    Public Property Statusbar() As Boolean
        Get
            Return _blnStatusbar
        End Get
        Set(ByVal Value As Boolean)
            _blnStatusbar = Value
        End Set
    End Property

    Public Property CDS() As Boolean
        Get
            Return _blnCDS
        End Get
        Set(ByVal Value As Boolean)
            _blnCDS = Value
        End Set
    End Property

    Public Property ImportCCD() As Boolean
        Get
            Return _blnImportCCD
        End Get
        Set(ByVal Value As Boolean)
            _blnImportCCD = Value
        End Set
    End Property

    Public Property ImportRestrictedCCDA() As Boolean
        Get
            Return _blnImportRestrictedCCD
        End Get
        Set(ByVal Value As Boolean)
            _blnImportRestrictedCCD = Value
        End Set
    End Property

    Public Property GenerateCCD() As Boolean
        Get
            Return _blnGenerateCCD
        End Get
        Set(ByVal Value As Boolean)
            _blnGenerateCCD = Value
        End Set
    End Property
    '----

    '' Added on 20090623 
    ''Ref:  GLO2009-0002313	- Ability to disable the clear patients documents
    Public Property ClearPatientDocs() As Boolean
        Get
            Return _blnClearPatientDocs
        End Get
        Set(ByVal Value As Boolean)
            _blnClearPatientDocs = Value
        End Set
    End Property
    ''--
    Public Property SecureMessages() As Boolean
        Get
            Return _blnSecureMessages
        End Get
        Set(ByVal value As Boolean)
            _blnSecureMessages = value
        End Set
    End Property

#End Region

#Region "Patient Details Menu"
    'Patient Details
    Dim _blnHistoryDetails As Boolean
    Dim _blnPastExams As Boolean
    Dim _blnNewExams As Boolean
    Dim _blnMedications As Boolean
    Dim _blnPrescriptions As Boolean
    Dim _blnAuditTrail As Boolean
    Dim _blnPatientOrders As Boolean
    Dim _blnPendingFaxes As Boolean
    Dim _blnSentFaxes As Boolean
    Dim _blnPatientProblemList As Boolean
    Dim _blnPatientMessages As Boolean
    Dim _blnViewDocs As Boolean

    Dim _blnInsurance As Boolean
    Dim _blnBilling As Boolean
    ''Added by Mayuri:20100113
    Dim _blnEligibility As Boolean
    Dim _blnPatientTasks As Boolean
    Dim _blnGoNewSchedule As Boolean
    Dim _blnGoAppointment As Boolean
    Dim _blnGoProblem As Boolean
    Dim _blnGoClinicalChartPrintQueue As Boolean
    '06122012-Added Intuit Communication
    Dim _blnIntuit As Boolean

 
    'Patient Details
    '06122012-Added Intuit Communication
    Public Property Intuit() As Boolean
        Get
            Return _blnIntuit
        End Get
        Set(ByVal Value As Boolean)
            _blnIntuit = Value
        End Set
    End Property
    'Patient Details

    Dim _blnAPIAccess As Boolean
    Public Property APIAccess() As Boolean
        Get
            Return _blnAPIAccess
        End Get
        Set(ByVal Value As Boolean)
            _blnAPIAccess = Value
        End Set
    End Property
    Public Property ViewDocs() As Boolean
        Get
            Return _blnViewDocs
        End Get
        Set(ByVal Value As Boolean)
            _blnViewDocs = Value
        End Set
    End Property
    Public Property HistoryDetails() As Boolean
        Get
            Return _blnHistoryDetails
        End Get
        Set(ByVal Value As Boolean)
            _blnHistoryDetails = Value
        End Set
    End Property
    ''Added by Mayuri:20100113
    Public Property EligibilityInfo() As Boolean
        Get
            Return _blnEligibility
        End Get
        Set(ByVal Value As Boolean)
            _blnEligibility = Value
        End Set
    End Property

    Public Property NewExams() As Boolean
        Get
            Return _blnNewExams
        End Get
        Set(ByVal Value As Boolean)
            _blnNewExams = Value
        End Set
    End Property

    Public Property PastExams() As Boolean
        Get
            Return _blnPastExams
        End Get
        Set(ByVal Value As Boolean)
            _blnPastExams = Value
        End Set
    End Property
    Public Property Medications() As Boolean
        Get
            Return _blnMedications
        End Get
        Set(ByVal Value As Boolean)
            _blnMedications = Value
        End Set
    End Property
    Public Property Prescriptions() As Boolean
        Get
            Return _blnPrescriptions
        End Get
        Set(ByVal Value As Boolean)
            _blnPrescriptions = Value
        End Set
    End Property
    Public Property AuditTrail() As Boolean
        Get
            Return _blnAuditTrail
        End Get
        Set(ByVal Value As Boolean)
            _blnAuditTrail = Value
        End Set
    End Property
    Public Property PatientOrders() As Boolean
        Get
            Return _blnPatientOrders
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientOrders = Value
        End Set
    End Property
    Public Property PatientMessages() As Boolean
        Get
            Return _blnPatientMessages
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientMessages = Value
        End Set
    End Property
    Public Property PendingFaxes()
        Get
            Return _blnPendingFaxes
        End Get
        Set(ByVal Value)
            _blnPendingFaxes = Value
        End Set
    End Property

    Public Property PatientProblemList() As Boolean
        Get
            Return _blnPatientProblemList
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientProblemList = Value
        End Set
    End Property

    Public Property SentFaxes() As Boolean
        Get
            Return _blnSentFaxes
        End Get
        Set(ByVal Value As Boolean)
            _blnSentFaxes = Value
        End Set
    End Property


    Public Property Insurance() As Boolean
        Get
            Return _blnInsurance
        End Get
        Set(ByVal Value As Boolean)
            _blnInsurance = Value
        End Set
    End Property

    Public Property Billing() As Boolean
        Get
            Return _blnBilling
        End Get
        Set(ByVal Value As Boolean)
            _blnBilling = Value
        End Set
    End Property

    Public Property PatientTasks() As String
        Get
            Return _blnPatientTasks
        End Get
        Set(ByVal value As String)
            _blnPatientTasks = value
        End Set
    End Property

#End Region

#Region " Reports Menu "
    'Reports
    Dim _blnUnfishedExams As Boolean
    Dim _blnFAXReport As Boolean
    'sarika 4th feb 2007
    Dim _blnExamsPrintFAX As Boolean
    Dim _blnExamStatus As Boolean
    Dim _blnPatientDemographics As Boolean
    Dim _blnHCFAReport As Boolean
    Dim _blnDiagnosisLabResult As Boolean
    Dim _blnReviewExams As Boolean
    Dim _blnBatchReferrals As Boolean
    Dim _blnGuidelineDueRpt As Boolean
    Dim _blnHealthPlansForCriteria As Boolean
    Dim _blnLabGraph As Boolean
    Dim _blnImmunizationDueRpt As Boolean
    'Reports

    'sarika 20090302
    '    Dim _blnDiagnosisLabResult As Boolean
    Dim _blnGuidelineReports As Boolean
    '   Dim _blnLabGraph As Boolean
    Dim _blnPatientReminderLetters As Boolean
    '----
    Public Property ImmunizationDueRpt() As Boolean
        Get
            Return _blnImmunizationDueRpt
        End Get
        Set(ByVal Value As Boolean)
            _blnImmunizationDueRpt = Value
        End Set
    End Property
    Public Property LabGraph() As Boolean
        Get
            Return _blnLabGraph
        End Get
        Set(ByVal Value As Boolean)
            _blnLabGraph = Value
        End Set
    End Property

    Public Property BatchReferrals() As Boolean
        Get
            Return _blnBatchReferrals
        End Get
        Set(ByVal Value As Boolean)
            _blnBatchReferrals = Value
        End Set
    End Property
    Public Property ReviewExams() As Boolean
        Get
            Return _blnReviewExams
        End Get
        Set(ByVal Value As Boolean)
            _blnReviewExams = Value
        End Set
    End Property
    Public Property UnfinishedExams() As Boolean
        Get
            Return _blnUnfishedExams
        End Get
        Set(ByVal Value As Boolean)
            _blnUnfishedExams = Value
        End Set
    End Property
    Public Property FAXReports() As Boolean
        Get
            Return _blnFAXReport
        End Get
        Set(ByVal Value As Boolean)
            _blnFAXReport = Value
        End Set
    End Property
    Public Property RptExamsPrintFax() As Boolean
        Get
            Return _blnExamsPrintFAX
        End Get
        Set(ByVal Value As Boolean)
            _blnExamsPrintFAX = Value
        End Set
    End Property

    Public Property RptPatientDemographics() As Boolean
        Get
            Return _blnPatientDemographics
        End Get
        Set(ByVal Value As Boolean)
            _blnPatientDemographics = Value
        End Set
    End Property

    Public Property RptExamsStatus() As Boolean
        Get
            Return _blnExamStatus
        End Get
        Set(ByVal Value As Boolean)
            _blnExamStatus = Value
        End Set
    End Property

    Public Property RptHCFAReport() As Boolean
        Get
            Return _blnHCFAReport
        End Get
        Set(ByVal Value As Boolean)
            _blnHCFAReport = Value
        End Set
    End Property

    Public Property RptDiagnosisLabResult() As Boolean
        Get
            Return _blnDiagnosisLabResult
        End Get
        Set(ByVal Value As Boolean)
            _blnDiagnosisLabResult = Value
        End Set
    End Property

    Public Property GuidelineDueReport() As Boolean
        Get
            Return _blnGuidelineDueRpt
        End Get
        Set(ByVal Value As Boolean)
            _blnGuidelineDueRpt = Value
        End Set
    End Property

    Public Property HealthPlansForCriteria() As Boolean
        Get
            Return _blnHealthPlansForCriteria
        End Get
        Set(ByVal Value As Boolean)
            _blnHealthPlansForCriteria = Value
        End Set
    End Property

    Public Property GuidelineReports() As Boolean
        Get
            Return _blnGuidelineDueRpt
        End Get
        Set(ByVal value As Boolean)
            _blnGuidelineDueRpt = value
        End Set
    End Property

    Public Property PatientReminderLetters() As Boolean
        Get
            Return _blnPatientReminderLetters
        End Get
        Set(ByVal value As Boolean)
            _blnPatientReminderLetters = value
        End Set
    End Property

#End Region

    '#Region " Private Orders"
    '    'Orders
    '    Dim _blnViewOrders As Boolean
    '    'sarika
    '    Dim _blnOrdersLabResults As Boolean
    '    Dim _blnOrdersAssignOrders As Boolean

    '    Dim _blnImportLabResult As Boolean
    '#End Region
    '#Region " Private Others"
    '    'Others
    '    'Dim _blnICD9Association As Boolean
    '    Dim _blnCalendar As Boolean
    '    'Dim _blnTasks As Boolean
    '    Dim _blnDocMGMT As Boolean
    '    Dim _blnPullCharts As Boolean
    '    ' Dim _blnMessage As Boolean
    '    'Dim _blnCPTAssociation As Boolean
    '    'Dim _blnFormGallery As Boolean

    '    Dim _blnVoiceCenter As Boolean
    '    Dim _blnDoctorSpeakerConfiguration As Boolean
    '#End Region


    '#Region "Private Smart Scan"
    '    'sarika
    '    Dim _blnSmartRequest As Boolean
    '    Dim _blnSmartMapping As Boolean
    '    Dim _blnSmartImport As Boolean
    '    Dim _blnSmartProcess As Boolean
    '    Dim _blnSmartSetting As Boolean
    '    Dim _blnSmartPrintTemplate As Boolean
    '#End Region

    '    'sarika 5th feb 2007
    '#Region "Private HPI"
    '    'HPI
    '    Dim _blnSetup As Boolean
    '    Dim _blnTemplates As Boolean
    '#End Region

    '#Region " Properties Orders"
    '    'Orders
    '    Public Property ViewOrders() As Boolean
    '        Get
    '            Return _blnViewOrders
    '        End Get
    '        Set(ByVal Value As Boolean)
    '            _blnViewOrders = Value
    '        End Set
    '    End Property
    '    Public Property OrdersLabResults() As Boolean
    '        Get
    '            Return _blnOrdersLabResults
    '        End Get
    '        Set(ByVal Value As Boolean)
    '            _blnOrdersLabResults = Value
    '        End Set
    '    End Property
    '    Public Property OrdersAssignOrders() As Boolean
    '        Get
    '            Return _blnOrdersAssignOrders
    '        End Get
    '        Set(ByVal Value As Boolean)
    '            _blnOrdersAssignOrders = Value
    '        End Set
    '    End Property

    '    Public Property ImportLabResult() As Boolean
    '        Get
    '            Return _blnImportLabResult
    '        End Get
    '        Set(ByVal Value As Boolean)
    '            _blnImportLabResult = Value
    '        End Set
    '    End Property
    '#End Region

    '#Region " Properties Others"
    '    'Others
    '    'Public Property ICD9Association() As Boolean
    '    '    Get
    '    '        Return _blnICD9Association
    '    '    End Get
    '    '    Set(ByVal Value As Boolean)
    '    '        _blnICD9Association = Value
    '    '    End Set
    '    'End Property
    '    Public Property Calendar() As Boolean
    '        Get
    '            Return _blnCalendar
    '        End Get
    '        Set(ByVal Value As Boolean)
    '            _blnCalendar = Value
    '        End Set
    '    End Property
    '    'Public Property Tasks() As Boolean
    '    '    Get
    '    '        Return _blnTasks
    '    '    End Get
    '    '    Set(ByVal Value As Boolean)
    '    '        _blnTasks = Value
    '    '    End Set
    '    'End Property
    '    Public Property DocMGMT() As Boolean
    '        Get
    '            Return _blnDocMGMT
    '        End Get
    '        Set(ByVal Value As Boolean)
    '            _blnDocMGMT = Value
    '        End Set
    '    End Property
    '    Public Property PullCharts() As Boolean
    '        Get
    '            Return _blnPullCharts
    '        End Get
    '        Set(ByVal Value As Boolean)
    '            _blnPullCharts = Value
    '        End Set
    '    End Property

    '    'Public Property CPTAssociation() As Boolean
    '    '    Get
    '    '        Return _blnCPTAssociation
    '    '    End Get
    '    '    Set(ByVal Value As Boolean)
    '    '        _blnCPTAssociation = Value
    '    '    End Set
    '    'End Property
    '    'Public Property FormGallery() As Boolean
    '    '    Get
    '    '        Return _blnFormGallery
    '    '    End Get
    '    '    Set(ByVal Value As Boolean)
    '    '        _blnFormGallery = Value
    '    '    End Set
    '    'End Property
    '    Public Property Settings() As Boolean
    '        Get
    '            Return _blnToolsSettings
    '        End Get
    '        Set(ByVal Value As Boolean)
    '            _blnToolsSettings = Value
    '        End Set
    '    End Property
    '    Public Property VoiceCenter() As Boolean
    '        Get
    '            Return _blnVoiceCenter
    '        End Get
    '        Set(ByVal Value As Boolean)
    '            _blnVoiceCenter = False
    '        End Set
    '    End Property
    '    Public Property DoctorSpeakerConfiguration() As Boolean
    '        Get
    '            Return _blnDoctorSpeakerConfiguration
    '        End Get
    '        Set(ByVal Value As Boolean)
    '            _blnDoctorSpeakerConfiguration = Value
    '        End Set
    '    End Property
    '#End Region

    '#Region "Properties Smart Scan"
    '    Public Property SmartRequest() As Boolean
    '        Get
    '            Return _blnSmartRequest
    '        End Get
    '        Set(ByVal Value As Boolean)
    '            _blnSmartRequest = Value
    '        End Set
    '    End Property

    '    Public Property SmartMapping() As Boolean
    '        Get
    '            Return _blnSmartMapping
    '        End Get
    '        Set(ByVal Value As Boolean)
    '            _blnSmartMapping = Value
    '        End Set
    '    End Property
    '    Public Property SmartImport() As Boolean
    '        Get
    '            Return _blnSmartImport
    '        End Get
    '        Set(ByVal Value As Boolean)
    '            _blnSmartImport = Value
    '        End Set
    '    End Property
    '    Public Property SmartProcess() As Boolean
    '        Get
    '            Return _blnSmartProcess
    '        End Get
    '        Set(ByVal Value As Boolean)
    '            _blnSmartProcess = Value
    '        End Set
    '    End Property
    '    Public Property SmartSetting() As Boolean
    '        Get
    '            Return _blnSmartSetting
    '        End Get
    '        Set(ByVal Value As Boolean)
    '            _blnSmartSetting = Value
    '        End Set
    '    End Property
    '    Public Property SmartPrintTemplate() As Boolean
    '        Get
    '            Return _blnSmartPrintTemplate
    '        End Get
    '        Set(ByVal Value As Boolean)
    '            _blnSmartPrintTemplate = Value
    '        End Set
    '    End Property

    '#End Region

    '#Region "Properties HPI"
    '    Public Property HPISetup() As Boolean
    '        Get
    '            Return _blnSetup
    '        End Get
    '        Set(ByVal Value As Boolean)
    '            _blnSetup = Value
    '        End Set
    '    End Property
    '    Public Property HPITemplates() As Boolean
    '        Get
    '            Return _blnTemplates
    '        End Get
    '        Set(ByVal Value As Boolean)
    '            _blnTemplates = Value
    '        End Set
    '    End Property
    '#End Region

    ''06222012
    'Reflection Is implemented to Access User Rights On Patient Strip
    'This Function Called Only From  gloUC_PatientStrip User Control
    Public Function RetriveUserRightsForPatientStrip() As ArrayList

        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_RetrieveUserRights"

        Dim objParaUserName As New SqlParameter
        'Sql parameter added by dipak 20091029 for indicate store procedure executed from EMR or PM
        Dim objParaApplicationType As New SqlParameter
        With objParaUserName
            .ParameterName = "@UserName"
            .Value = gstrLoginName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaUserName)
        'code added by dipak 20091029 to pass Sql parameter attributes value
        With objParaApplicationType
            .ParameterName = "@ApplicationType"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaApplicationType)


        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader()

        Dim arrLst As New ArrayList
        While objSQLDataReader.Read
            If IsNothing(objSQLDataReader.Item(0)) = False Then
                arrLst.Add(Trim(objSQLDataReader.Item(0)))
            End If
        End While
        objSQLDataReader.Close()
        objCon.Close()
        objSQLDataReader = Nothing
        objCon.Dispose()
        objCon = Nothing

        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        objParaApplicationType = Nothing
        objParaUserName = Nothing
        Return arrLst
    End Function
    ''' ''''''''''''''''
    Public Shared Function GetUserRightsArrayForPatientStrip() As ArrayList

        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_RetrieveUserRights"

        Dim objParaUserName As New SqlParameter
        'Sql parameter added by dipak 20091029 for indicate store procedure executed from EMR or PM
        Dim objParaApplicationType As New SqlParameter
        With objParaUserName
            .ParameterName = "@UserName"
            .Value = gstrLoginName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaUserName)
        'code added by dipak 20091029 to pass Sql parameter attributes value
        With objParaApplicationType
            .ParameterName = "@ApplicationType"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaApplicationType)


        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader()

        Dim arrLst As New ArrayList
        While objSQLDataReader.Read
            If IsNothing(objSQLDataReader.Item(0)) = False Then
                arrLst.Add(Trim(objSQLDataReader.Item(0)))
            End If
        End While
        objSQLDataReader.Close()
        objCon.Close()
        objSQLDataReader = Nothing
        objCon.Dispose()
        objCon = Nothing

        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If
        objParaApplicationType = Nothing
        objParaUserName = Nothing
        Return arrLst
    End Function

    Public Sub RetrieveUserRights(ByVal strUserName As String)
        Dim objCon As New SqlConnection
        objCon.ConnectionString = GetConnectionString()
        Dim objCmd As New SqlCommand
        Dim objSQLDataReader As SqlDataReader
        objCmd.CommandType = CommandType.StoredProcedure
        objCmd.CommandText = "gsp_RetrieveUserRights"

        Dim objParaUserName As New SqlParameter
        'Sql parameter added by dipak 20091029 for indicate store procedure executed from EMR or PM
        Dim objParaApplicationType As New SqlParameter
        With objParaUserName
            .ParameterName = "@UserName"
            .Value = strUserName
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.VarChar
        End With
        objCmd.Parameters.Add(objParaUserName)
        'code added by dipak 20091029 to pass Sql parameter attributes value
        With objParaApplicationType
            .ParameterName = "@ApplicationType"
            .Value = 0
            .Direction = ParameterDirection.Input
            .SqlDbType = SqlDbType.Int
        End With
        objCmd.Parameters.Add(objParaApplicationType)


        objCmd.Connection = objCon
        objCon.Open()
        objSQLDataReader = objCmd.ExecuteReader()

        Dim arrLst As New ArrayList
        While objSQLDataReader.Read
            If IsNothing(objSQLDataReader.Item(0)) = False Then
                arrLst.Add(Trim(objSQLDataReader.Item(0)))
            End If
        End While
        objSQLDataReader.Close()
        objCon.Close()
        objSQLDataReader = Nothing
        objCon.Dispose()
        objCon = Nothing
        'If arrLst.Count = 1 Then
        '    ''Security User Login
        '    If arrLst.Contains("Audit Trail") = True Then
        '        _blnAuditTrail = True
        '        ''Security User Login
        '        gblnSecurityUser = True
        '    Else
        '        gblnSecurityUser = False
        '        _blnAuditTrail = False
        '    End If
        'Else
        '    gblnSecurityUser = False
        'End If
        'If gblnSecurityUser Then
        '    _blnAuditTrail = True
        'Else
        '    _blnAuditTrail = False
        'End If
        ''Added by Mayuri:20100113-if user is security user then only display AuditTrail in Patient Details
        If gblnSecurityUser Then
            _blnAuditTrail = True
            Exit Sub
        End If
        ''

        If arrLst.Contains("Prescription Provider Association") = True Then
            bPrescriptionProviderAssociation = True
        Else
            bPrescriptionProviderAssociation = False
        End If

        If arrLst.Contains("Merge Order") = True Then
            bMergeOrder = True
        Else
            bMergeOrder = False
        End If

        If arrLst.Contains("Audit Trail") = True Then
            _blnAuditTrail = True
        Else
            _blnAuditTrail = False
        End If

        ''Start Edit menu
        If arrLst.Contains("ICD9") = True Then
            _blnICD9 = True
        Else
            _blnICD9 = False
        End If

        If arrLst.Contains("CPT") = True Then
            _blnCPT = True
        Else
            _blnCPT = False
        End If

        If arrLst.Contains("Modifier") = True Then
            _blnModifier = True
        Else
            _blnModifier = False
        End If

        If arrLst.Contains("Drugs") = True Then
            _blnDrugs = True
        Else
            _blnDrugs = False
        End If

        If arrLst.Contains("SIG") = True Then
            _blnSIG = True
        Else
            _blnSIG = False
        End If

        If arrLst.Contains("Drugs Configuration") = True Then
            _blnDrugsConfig = True
        Else
            _blnDrugsConfig = False
        End If

        If arrLst.Contains("Templates") = True Then
            _blnTemplateGallery = True
        Else
            _blnTemplateGallery = False
        End If

        If arrLst.Contains("Category") = True Then
            _blnCategory = True
        Else
            _blnCategory = False
        End If

        If arrLst.Contains("Medical Category") = True Then
            _blnMedicalCategory = True
        Else
            _blnMedicalCategory = False
        End If

        If arrLst.Contains("DMS Category") = True Then
            _blnDMSCategory = True
        Else
            _blnDMSCategory = False
        End If

        If arrLst.Contains("Review of System") = True Then
            _blnROS = True
        Else
            _blnROS = False
        End If

        If arrLst.Contains("History") = True Then
            _blnHistory = True
        Else
            _blnHistory = False
        End If

        If arrLst.Contains("Contacts") = True Then
            _blnContacts = True
        Else
            _blnContacts = False
        End If

        If arrLst.Contains("Orders and Results Setup") = True Then
            _blnOrdersAndResultsSetup = True
        Else
            _blnOrdersAndResultsSetup = False
        End If

        If arrLst.Contains("Radiology") = True Then
            _blnRadiology = True
        Else
            _blnRadiology = False
        End If

        If arrLst.Contains("Flowsheet") = True Then
            _blnEditFlowsheet = True
        Else
            _blnEditFlowsheet = False
        End If

        If arrLst.Contains("Speciality") = True Then
            _blnSpeciality = True
        Else
            _blnSpeciality = False
        End If

        If arrLst.Contains("Appointment Scheduler Type") = True Then
            _blnAppointmentSchedularType = True
        Else
            _blnAppointmentSchedularType = False
        End If

        If arrLst.Contains("Appointment Book") = True Then
            _blnAppointmentBook = True
        Else
            _blnAppointmentBook = False
        End If
        If arrLst.Contains("Associated Provider Signature") = True Then
            _blnAssociatedProviderSignature = True
        Else
            _blnAssociatedProviderSignature = False
        End If
        If arrLst.Contains("Deceased Patient") = True Then
            _blnDeceasedPatient = True
        Else
            _blnDeceasedPatient = False
        End If

        ''Added Rahul on 20101012
        If arrLst.Contains("Drug Interaction") = True Then
            _blnDrugInteraction = True
        Else
            _blnDrugInteraction = False
        End If
        ''

        If arrLst.Contains("DM Setup") = True Then
            _blnDMSetup = True
        Else
            _blnDMSetup = False
        End If

        If arrLst.Contains("View Recommendation") = True Then
            _blnViewRecommendation = True
            gbShowviewRecommendation = True
        Else
            _blnViewRecommendation = False
            gbShowviewRecommendation = False
        End If

        If arrLst.Contains("Show DM Alert") = True Then
            _blnShowDMAlert = True
            gbShowDMAlert = True
        Else
            _blnShowDMAlert = False
            gbShowDMAlert = False
        End If

        If arrLst.Contains("IM Setup") = True Then
            _blnIMSetup = True
        Else
            _blnIMSetup = False
        End If

        'Smart Diagnosis,Smart Treatment,Smart Order
        If arrLst.Contains("Edit Form Gallery") = True Then
            _blnEditFormGallery = True
        Else
            _blnEditFormGallery = False
        End If

        If arrLst.Contains("Smart Order") = True Then
            _blnSmartOrder = True
        Else
            _blnSmartOrder = False
        End If

        If arrLst.Contains("Status Users") = True Then
            _blnStatusUsers = True
        Else
            _blnStatusUsers = False
        End If

        If arrLst.Contains("Smart Diagnosis") = True Then
            _blnSmartDiagnosis = True
        Else
            _blnSmartDiagnosis = False
        End If
        If arrLst.Contains("Smart Treatment") = True Then
            _blnSmartTreatment = True
        Else
            _blnSmartTreatment = False
        End If

        If arrLst.Contains("ICD9CPT Gallery") = True Then
            _blnICD9CPTGallery = True
        Else
            _blnICD9CPTGallery = False
        End If

        If arrLst.Contains("Disclosure Set") = True Then
            _blnDisclosureSet = True
        Else
            _blnDisclosureSet = False
        End If

        If arrLst.Contains("Patient Summary") = True Then
            _blnPatientSummary = True
        Else
            _blnPatientSummary = False
        End If


        'sarika 20090302
        'CV Setup , Billing Configuration , Liquid Data  
        If arrLst.Contains("CV Setup") = True Then
            _blnCVSetup = True
        Else
            _blnCVSetup = False
        End If

        If arrLst.Contains("TaxID Setup") = True Then
            _blnTaxIDSetup = True
        Else
            _blnTaxIDSetup = False
        End If

        If arrLst.Contains("Billing Configuration") = True Then
            _blnBillingConfiguration = True
        Else
            _blnBillingConfiguration = False
        End If

        If arrLst.Contains("Liquid Data") = True Then
            _blnLiquidData = True
        Else
            _blnLiquidData = False
        End If

        'Shubhangi
        'For Checking whether access for Delete Unfinished Exam is True or not

        If arrLst.Contains("Delete Unfinished Exam") = True Then
            gIsUnfinishedExamDelete = True
        Else
            gIsUnfinishedExamDelete = False
        End If
        'End Shubhangi'

        '-----


        ''End of Edit menu

        ''Start of Go menu 
        If arrLst.Contains("Patient Registration") = True Then
            _blnPatientRegistration = True
        Else
            _blnPatientRegistration = False
        End If

        If arrLst.Contains("Modify Patient") = True Then
            _blnModifyPatient = True
        Else
            _blnModifyPatient = False
        End If

        If arrLst.Contains("Patient Card") = True Then
            _blnPatientCards = True
        Else
            _blnPatientCards = False
        End If

        If arrLst.Contains("Patient ROS") = True Then
            _blnPatientROS = True
        Else
            _blnPatientROS = False
        End If

        If arrLst.Contains("Patient History") = True Then
            _blnPatientHistory = True
        Else
            _blnPatientHistory = False
        End If

        If arrLst.Contains("Prescription") = True Then
            _blnPatientPrescription = True
        Else
            _blnPatientPrescription = False
        End If

        If arrLst.Contains("Medication") = True Then
            _blnPatientMedication = True
        Else
            _blnPatientMedication = False
        End If

        If arrLst.Contains("Messages") = True Then
            _blnMessages = True
        Else
            _blnMessages = False
        End If

        If arrLst.Contains("Add Vitals") = True Then
            _blnPatientVitals = True
        Else
            _blnPatientVitals = False
        End If

        If arrLst.Contains("New Exam") = True Then
            _blnPatientNewExam = True
        Else
            _blnPatientNewExam = False
        End If

        If arrLst.Contains("Past Exam") = True Then
            _blnPatientPastExam = True
        Else
            _blnPatientPastExam = False
        End If

        If arrLst.Contains("Unfinished") = True Then
            _blnUnfinished = True
        Else
            _blnUnfinished = False
        End If

        If arrLst.Contains("Pull Charts") = True Then
            _blnPullCharts = True
        Else
            _blnPullCharts = False
        End If

        If arrLst.Contains("Radiology Orders") = True Then
            _blnRadiologyOrders = True
        Else
            _blnRadiologyOrders = False
        End If

        If arrLst.Contains("Orders and Results") = True Then
            _blnOrdersAndResults = True
        Else
            _blnOrdersAndResults = False
        End If

        If arrLst.Contains("Patient Letters") = True Then
            _blnPatientLetters = True
        Else
            _blnPatientLetters = False
        End If

        If arrLst.Contains("PT Protocols") = True Then
            _blnPTProtocols = True
        Else
            _blnPTProtocols = False
        End If

        If arrLst.Contains("Patient Concent") = True Or arrLst.Contains("Patient Consent") = True Then
            _blnPatientConcent = True
        Else
            _blnPatientConcent = False
        End If

        If arrLst.Contains("Disclosure Management") = True Then
            _blnDislocureMgmt = True
        Else
            _blnDislocureMgmt = False
        End If

        If arrLst.Contains("NurseNotes") = True Then
            _blnNurseNotes = True
        Else
            _blnNurseNotes = False
        End If

        If arrLst.Contains("Patient Flowsheet") = True Then
            _blnPatientFlowsheet = True
        Else
            _blnPatientFlowsheet = False
        End If

        If arrLst.Contains("Form Gallery") = True Then
            _blnFormGallery = True
        Else
            _blnFormGallery = False
        End If

        If arrLst.Contains("Tasks") = True Then
            _blnAddTasks = True
        Else
            _blnAddTasks = False
        End If

        If arrLst.Contains("Immunization Transaction") = True Then
            _blnImmunization = True
        Else
            _blnImmunization = False
        End If

        If arrLst.Contains("Health Plan For Patient") = True Then
            _blnPatientHealthPlan = True
        Else
            _blnPatientHealthPlan = False
        End If

        If arrLst.Contains("Upload Video") = True Then
            _blnUploadVideo = True
        Else
            _blnUploadVideo = False
        End If

        If arrLst.Contains("Calendar") = True Then
            _blnCalendar = True
        Else
            _blnCalendar = False
        End If

        If arrLst.Contains("Document Management") = True Then
            _blnDocMgmt = True
        Else
            _blnDocMgmt = False
        End If

        If arrLst.Contains("Set Alert") = True Then
            _blnSetAlert = True
        Else
            _blnSetAlert = False
        End If

        If arrLst.Contains("Set Surgical Alert") = True Then
            _blnSetSurgicalAlert = True
        Else
            _blnSetSurgicalAlert = False
        End If

        'sarika 20090302
        'Charges, Batch, Payment, Remittance, Balance, Ledger
        If arrLst.Contains("Charges") = True Then
            _blnCharges = True
        Else
            _blnCharges = False
        End If

        If arrLst.Contains("Batch") = True Then
            _blnBatch = True
        Else
            _blnBatch = False
        End If

        If arrLst.Contains("Payment") = True Then
            _blnPayment = True
        Else
            _blnPayment = False
        End If

        If arrLst.Contains("Remittance") = True Then
            _blnRemittance = True
        Else
            _blnRemittance = False
        End If

        If arrLst.Contains("Balance") = True Then
            _blnBalance = True
        Else
            _blnBalance = False
        End If

        If arrLst.Contains("Ledger") = True Then
            _blnLedger = True
        Else
            _blnLedger = False
        End If
        ''Added by Mayuri:20100113
        If arrLst.Contains("Triage") = True Then
            _blnTriage = True
        Else
            _blnTriage = False
        End If
        If arrLst.Contains("Eligibility") = True Then
            _blnEligibility = True
        Else
            _blnEligibility = False
        End If
        '---
        ''added for user rights changes
        If arrLst.Contains("Appointment") = True Then
            _blnGoAppointment = True
        Else
            _blnGoAppointment = False
        End If
        ''added for user rights changes
        If arrLst.Contains("New Schedule") = True Then
            _blnGoNewSchedule = True
        Else
            _blnGoNewSchedule = False
        End If
        ''added for user rights changes
        If arrLst.Contains("Problem") = True Then
            _blnGoProblem = True
        Else
            _blnGoProblem = False
        End If
        ''added for user rights changes
        If arrLst.Contains("ClinicalChartPrintQueue") = True Then
            _blnGoClinicalChartPrintQueue = True
        Else
            _blnGoClinicalChartPrintQueue = False
        End If

        If arrLst.Contains("Social Psychological Behavioral Observations") = True Or arrLst.Contains("Social Psychological Behavioral observations") = True Then
            _blnSocialPsychologicalBehavioralObservations = True
        Else
            _blnSocialPsychologicalBehavioralObservations = False
        End If


        _blnImplantableDevices = arrLst.Contains("Implantable Devices")
        _blnPatientImplantableDevices = arrLst.Contains("Patient Implantable Devices")
        ''End of Go menu --

        ''Start of View menu
        If arrLst.Contains("View Patient Vitals") = True Then
            _blnViewVitals = True
        Else
            _blnViewVitals = False
        End If
        'Added Code Changes for View OBVitals
        If arrLst.Contains("View OB Vitals") = True Then
            _blnViewOBVitals = True
        Else
            _blnViewOBVitals = False
        End If

        If arrLst.Contains("View Tasks") = True Then
            _blnViewTasks = True
        Else
            _blnViewTasks = False
        End If

        If arrLst.Contains("View/Complete Other Users Tasks") = True Then
            _blnviewCompleteOtherUsersTasks = True
        Else
            _blnviewCompleteOtherUsersTasks = False
        End If

        If arrLst.Contains("View Messages") = True Then
            _blnViewMessages = True
        Else
            _blnViewMessages = False
        End If

        If arrLst.Contains("Summary Care Record") = True Then
            _blnviewSummaryCareRecord = True
        Else
            _blnviewSummaryCareRecord = False
        End If

        'Changed by Ashish on 2nd June 2014
        'to disable Patient Education from View Menu
        'as the same functionality is shifted to Go -> Patient Education

        'If arrLst.Contains("Patient Education") = True Then
        '    _blnViewPatientEducation = True
        'Else
        '_blnViewPatientEducation = False
        'End If

        If arrLst.Contains("View Form Gallery") = True Then
            _blnViewFormGallery = True
        Else
            _blnViewFormGallery = False
        End If

        If arrLst.Contains("View Referrals") = True Then
            _blnViewReferrals = True
        Else
            _blnViewReferrals = False
        End If

        If arrLst.Contains("View Patient Letters") = True Then
            _blnViewPatientLetters = True
        Else
            _blnViewPatientLetters = False
        End If

        If arrLst.Contains("View PT Protocols") = True Then
            _blnViewPTProtocols = True
        Else
            _blnViewPTProtocols = False
        End If

        If arrLst.Contains("View Patient Concent") = True Or arrLst.Contains("View Patient Consent") = True Then
            _blnViewPatientConcent = True
        Else
            _blnViewPatientConcent = False
        End If

        If arrLst.Contains("Patient Consent Tracking") = True Then
            _blnViewPatientConcentTracking = True
        Else
            _blnViewPatientConcentTracking = False
        End If

        If arrLst.Contains("View NurseNotes") = True Then
            _blnVWNurseNotes = True
        Else
            _blnVWNurseNotes = False
        End If

        If arrLst.Contains("View Disclosure Management") = True Then
            _blnViewDiscloures = True
        Else
            _blnViewDiscloures = False
        End If

        If arrLst.Contains("Patient Video") = True Then
            _blnViewPatientVideo = True
        Else
            _blnViewPatientVideo = False
        End If

        If arrLst.Contains("Received  Faxes") = True Then
            _blnViewReceivedFAXes = True
        Else
            _blnViewReceivedFAXes = False
        End If

        If arrLst.Contains("OutStanding Orders") = True Then
            _blnViewOutstandingOrders = True
        Else
            _blnViewOutstandingOrders = False
        End If

        'View Refill Request,View Error Messages

        If arrLst.Contains("View Refill Request") = True Then
            _blnViewPendingRefillRequest = True
        Else
            _blnViewPendingRefillRequest = False
        End If

        If arrLst.Contains("View Error Messages") = True Then
            _blnViewErrorMsg = True
        Else
            _blnViewErrorMsg = False
        End If

        If arrLst.Contains("View Patient Summary") = True Then
            _blnViewPatientSummary = True
        Else
            _blnViewPatientSummary = False
        End If

        'For Recover Exam Change
        If arrLst.Contains("Recover Exam") = True Then
            _blnRecoverExam = True
        Else
            _blnRecoverExam = False
        End If

        'sarika 20090302
        'Mails,DICOM,Patient Synopsis,Patient Confidential,Patient Chief Complaints
        If arrLst.Contains("Mails") = True Then
            _blnMails = True
        Else
            _blnMails = False
        End If

        If arrLst.Contains("DICOM") = True Then
            _blnDICOM = True
        Else
            _blnDICOM = False
        End If

        If arrLst.Contains("Patient Synopsis") = True Then
            _blnPatientSynopsis = True
        Else
            _blnPatientSynopsis = False
        End If

        If arrLst.Contains("Patient Confidential") = True Then
            _blnPatientConfidential = True
        Else
            _blnPatientConfidential = False
        End If

        If arrLst.Contains("Patient Chief Complaints") = True Then
            _blnPatientChiefComplaints = True
        Else
            _blnPatientChiefComplaints = False
        End If

        '12-Jun-14 Aniket: Replace Schedule user rights with Calendar as both as same modules
        If arrLst.Contains("Calendar") = True Then
            _blnSchedule = True
        Else
            _blnSchedule = False
        End If


        'Settings , Load Default Display Settings , Import Vital Graph Data , Time Synchronization with Server , 
        'Toolbar , Statusbar , Import CCD , Generate CCD



        '----

        ''End of View menu ----

        '' Start of Patient Details menu
        If arrLst.Contains("History Details") = True Then
            _blnHistoryDetails = True
        Else
            _blnHistoryDetails = False
        End If

        If arrLst.Contains("Past Exams") = True Then
            _blnPastExams = True
        Else
            _blnPastExams = False
        End If

        If arrLst.Contains("New Exams") = True Then
            _blnNewExams = True
        Else
            _blnNewExams = False
        End If

        If arrLst.Contains("Medications") = True Then
            _blnMedications = True
        Else
            _blnMedications = False
        End If

        If arrLst.Contains("Prescriptions") = True Then
            _blnPrescriptions = True
        Else
            _blnPrescriptions = False
        End If

        If arrLst.Contains("Audit Trail") = True Then
            _blnAuditTrail = True
        Else
            _blnAuditTrail = False
        End If

        If arrLst.Contains("Order Templates") = True Then
            _blnPatientOrders = True
        Else
            _blnPatientOrders = False
        End If

        If arrLst.Contains("Patient Messages") = True Then
            _blnPatientMessages = True
        Else
            _blnPatientMessages = False
        End If

        If arrLst.Contains("Problem List") = True Then
            _blnPatientProblemList = True
        Else
            _blnPatientProblemList = False
        End If

        If arrLst.Contains("View Documents") = True Then
            _blnViewDocs = True
        Else
            _blnViewDocs = False
        End If

        If arrLst.Contains("Pending Faxes") = True Then
            _blnPendingFaxes = True
        Else
            _blnPendingFaxes = False
        End If

        If arrLst.Contains("Sent Faxes") = True Then
            _blnSentFaxes = True
        Else
            _blnSentFaxes = False
        End If

        If arrLst.Contains("View Pending Lab Orders") = True Then
            _blnViewPendingLabOrders = True
        Else
            _blnViewPendingLabOrders = False
        End If




        ''End view menu ---

        ''Start Tools Menu

        'Dim _blnMergePatient As Boolean
        'Dim _blnMigrateDiagnosis As Boolean
        'Dim _blnPatientControl As Boolean
        'Dim _blnExportTemplates As Boolean
        'Dim _blnImportTemplates As Boolean
        'Dim _blnUpdateExisting As Boolean
        'Dim _blnUpdateOther As Boolean
        'Dim _blnUpgradeTemplates As Boolean
        'Dim _blnUnlockRecords As Boolean
        'Dim _blnImportVitals As Boolean

        If arrLst.Contains("Patient Control") = True Then
            _blnPatientControl = True
        Else
            _blnPatientControl = False
        End If

        If arrLst.Contains("Export Templates") = True Then
            _blnExportTemplates = True
        Else
            _blnExportTemplates = False
        End If

        If arrLst.Contains("Import Templates") = True Then
            _blnImportTemplates = True
        Else
            _blnImportTemplates = False
        End If

        If arrLst.Contains("Update Existing Templates") = True Then
            _blnUpdateExisting = True
        Else
            _blnUpdateExisting = False
        End If

        If arrLst.Contains("Update Other Templates") = True Then
            _blnUpdateOther = True
        Else
            _blnUpdateOther = False
        End If

        If arrLst.Contains("Upgrade Templates") = True Then
            _blnUpgradeTemplates = True
        Else
            _blnUpgradeTemplates = False
        End If

        If arrLst.Contains("Merge Patient") = True Then
            _blnMergePatient = True
        Else
            _blnMergePatient = False
        End If

        '' Added on 20090623 
        ''Ref:  GLO2009-0002313	- Ability to disable the clear patients documents
        If arrLst.Contains("Clear Patient Documents") = True Then
            _blnClearPatientDocs = True
        Else
            _blnClearPatientDocs = False
        End If
        ''

        If arrLst.Contains("Migrate Diagnosis & Treatments") = True Then
            _blnMigrateDiagnosis = True
        Else
            _blnMigrateDiagnosis = False
        End If

        If arrLst.Contains("Import Vital Graph") = True Then
            _blnImportVitals = True
        Else
            _blnImportVitals = False
        End If

        If arrLst.Contains("Unlock Records") = True Then
            _blnUnlockRecords = True
        Else
            _blnUnlockRecords = False
        End If

        If arrLst.Contains("Settings") = True Then
            _blnSettings = True
        Else
            _blnSettings = False
        End If

        If arrLst.Contains("Load Default Display Settings") = True Then
            _blnLoadDefaultDisplaySettings = True
        Else
            _blnLoadDefaultDisplaySettings = False
        End If

        If arrLst.Contains("Import Vital Graph Data") = True Then
            _blnImportVitalGraphData = True
        Else
            _blnImportVitalGraphData = False
        End If

        If arrLst.Contains("Time Synchronization with Server") = True Then
            _blnTimeSynchronizationwithServer = True
        Else
            _blnTimeSynchronizationwithServer = False
        End If

        If arrLst.Contains("Toolbar") = True Then
            _blnToolbar = True
        Else
            _blnToolbar = False
        End If


        If arrLst.Contains("Statusbar") = True Then
            _blnStatusbar = True
        Else
            _blnStatusbar = False
        End If

        If arrLst.Contains("Import CCD") = True Then
            _blnImportCCD = True
        Else
            _blnImportCCD = False
        End If

        If arrLst.Contains("Import Restricted CCDA") = True Then
            _blnImportRestrictedCCD = True
        Else
            _blnImportRestrictedCCD = False
        End If

        If arrLst.Contains("Generate CCD") = True Then
            _blnGenerateCCD = True
        Else
            _blnGenerateCCD = False
        End If

        If arrLst.Contains("CDS") = True Then
            _blnCDS = True
        Else
            _blnCDS = False
        End If


        ''End of Tools menu

        '' Start of Reports menu
        If arrLst.Contains("Unfinished Exams") = True Then
            _blnUnfishedExams = True
        Else
            _blnUnfishedExams = False
        End If

        If arrLst.Contains("FAX Report") = True Then
            _blnFAXReport = True
        Else
            _blnFAXReport = False
        End If

        If arrLst.Contains("Exams Print / FAX") = True Then
            _blnExamsPrintFAX = True
        Else
            _blnExamsPrintFAX = False
        End If


        If arrLst.Contains("Review Exams") = True Then
            _blnReviewExams = True
        Else
            _blnReviewExams = False
        End If

        If arrLst.Contains("Exam Status") = True Then
            _blnExamStatus = True
        Else
            _blnExamStatus = False
        End If

        If arrLst.Contains("Batch Referrals") = True Then
            _blnBatchReferrals = True
        Else
            _blnBatchReferrals = False
        End If

        If arrLst.Contains("Patient Demographics") = True Then
            _blnPatientDemographics = True
        Else
            _blnPatientDemographics = False
        End If
        If arrLst.Contains("HCFA Report") = True Then
            _blnHCFAReport = True
        Else
            _blnHCFAReport = False
        End If

        If arrLst.Contains("Diagnosis Lab Result") = True Then
            _blnDiagnosisLabResult = True
        Else
            _blnDiagnosisLabResult = False
        End If

        If arrLst.Contains("Patients For Criteria") = True Then
            _blnHealthPlansForCriteria = True
        Else
            _blnHealthPlansForCriteria = False
        End If

        If arrLst.Contains("Lab Graph") = True Then
            _blnLabGraph = True
        Else
            _blnLabGraph = False
        End If

        If arrLst.Contains("Guideline Reports") = True Then
            _blnGuidelineDueRpt = True
        Else
            _blnGuidelineDueRpt = False
        End If

        If arrLst.Contains("Immunization Due Report") = True Then
            _blnImmunizationDueRpt = True
        Else
            _blnImmunizationDueRpt = False
        End If

        'Diagnosis Lab Result , Guideline Reports , Lab Graph , Patient Reminder Letters

        If arrLst.Contains("Diagnosis Lab Result") = True Then
            _blnDiagnosisLabResult = True
        Else
            _blnDiagnosisLabResult = False
        End If

        If arrLst.Contains("Guideline Reports") = True Then
            _blnGuidelineDueRpt = True
        Else
            _blnGuidelineDueRpt = False
        End If

        If arrLst.Contains("Lab Graph") = True Then
            _blnLabGraph = True
        Else
            _blnLabGraph = False
        End If

        If arrLst.Contains("Patient Reminder Letters") = True Then
            _blnPatientReminderLetters = True
        Else
            _blnPatientReminderLetters = False
        End If

        ''End  of Reports menu

        If arrLst.Contains("Cardio Vascular Risk") = True Then
            _blnCardioVascularRisk = True
        Else
            _blnCardioVascularRisk = False
        End If

        If arrLst.Contains("Insurance") = True Then
            _blnInsurance = True
        Else
            _blnInsurance = False
        End If

        If arrLst.Contains("Billing") = True Then
            _blnBilling = True
        Else
            _blnBilling = False
        End If

        If arrLst.Contains("View CCD Files") = True Then
            _blnViewCCDFiles = True
        Else
            _blnViewCCDFiles = False
        End If

        If arrLst.Contains("Provider DIRECT Message") = True Then
            _blnSecureMessages = True
        Else
            _blnSecureMessages = False
        End If

        ''Sandip Darade 20090821
        ''To view appointment in patient detail information box 
        If arrLst.Contains("View Appointments") = True Then
            _blnAppointment = True
        Else
            _blnAppointment = False
        End If

        If arrLst.Contains("Patient Forms") = True Then
            _blnPatientForms = True
        Else
            _blnPatientForms = False
        End If
        ''Sandip Darade 20090828
        ''Closed Journals now renamed  as Payment Tray
        If arrLst.Contains("Payment Tray") = True Then
            _blnClosedJournals = True
        Else
            _blnClosedJournals = False
        End If

        If arrLst.Contains("Change Password") = True Then
            _blnchangepass = True
        Else
            _blnchangepass = False
        End If

        If arrLst.Contains("Patient Tasks") = True Then
            _blnPatientTasks = True
        Else
            _blnPatientTasks = False
        End If

        '06122012-Added For Checking user rights For Intuit Communication
        ''Task #68533: gloEMR Admin - User Management - User Rights - Change "Intuit" to "Patient Portal".
        ''change name of right from "Intuit" to "Patient Portal"
        If arrLst.Contains("Patient Portal") = True Then
            _blnIntuit = True
        Else
            _blnIntuit = False
        End If

        If arrLst.Contains("API") = True Then
            _blnAPIAccess = True
        Else
            _blnAPIAccess = False
        End If

        If arrLst.Contains("Complete Task for all Users") = True Then
            _blnViewCompleteTaskforallUsers = True
        Else
            _blnViewCompleteTaskforallUsers = False
        End If

        ''gloCommunity - Added for User right setting on 20120727
        If arrLst.Contains("gloCommunity") = True Then
            _blnMnugloCommunity = True
        Else
            _blnMnugloCommunity = False
        End If
        If arrLst.Contains("Community Connect") = True Then
            _blnCommunityConnect = True
        Else
            _blnCommunityConnect = False
        End If
        If arrLst.Contains("My Community") = True Then
            _blnMyCommunity = True
        Else
            _blnMyCommunity = False
        End If

        If arrLst.Contains("Share") = True Then
            _blnShare = True
        Else
            _blnShare = False
        End If

        '02-May-13 Aniket: Resolving Bug 50030
        If arrLst.Contains("Family Member Relation Master") = True Then
            _blnFamilyMember = True
        Else
            _blnFamilyMember = False
        End If

        If arrLst.Contains("Zip") = True Then
            _blnZip = True
        Else
            _blnZip = False
        End If

        If arrLst.Contains("Clinical Instructions") = True Then
            _blnClinicalInstructions = True
        Else
            _blnClinicalInstructions = False
        End If

        If arrLst.Contains("Care Plan") = True Then
            _blnCarePlan = True
        Else
            _blnCarePlan = False
        End If

        If arrLst.Contains("CCDA Schedule") = True Then
            _blnCCDASchedule = True
        Else
            _blnCCDASchedule = False
        End If

        If arrLst.Contains("Export Summary") = True Then
            _blnExportSummary = True
        Else
            _blnExportSummary = False
        End If

        If arrLst.Contains("Vitals") = True Then
            _blnVitalSettings = True
        Else
            _blnVitalSettings = False
        End If

        If arrLst.Contains("Education Material Mapping") = True Then
            gblnEducationMaterialMappingEnabled = True
        Else
            gblnEducationMaterialMappingEnabled = False
        End If

        If arrLst.Contains("Education Material") = True Then
            gblnEducationMaterialEnabled = True
        Else
            gblnEducationMaterialEnabled = False
        End If

        If arrLst.Contains("Education Material - Advanced Provider Reference") = True Then
            gblnAdvancedReferenceEnabled = True
        Else
            gblnAdvancedReferenceEnabled = False
        End If

        If arrLst.Contains("Rx Savings Message") = True Then ''resolved Bug #60948 version 8000, Changed setting name from [Patient Saving Message] to [Rx Savings Message]
            _blnPatientSaving = True
        Else
            _blnPatientSaving = False
        End If

        If arrLst.Contains("RCM Category") = True Then
            _blnRCMCategory = True
        Else
            _blnRCMCategory = False
        End If

        If arrLst.Contains("RCM Document Management") = True Then
            _blnRCMDocumentManagement = True
        Else
            _blnRCMDocumentManagement = False
        End If

        If arrLst.Contains("View CDA Errors") = True Then
            _bViewCDAErrors = True
        Else
            _bViewCDAErrors = False
        End If

        If arrLst.Contains("Plan of Treatment") = True Then
            _blnPlanOfTreatment = True
        Else
            _blnPlanOfTreatment = False
        End If

        If objCmd IsNot Nothing Then
            objCmd.Parameters.Clear()
            objCmd.Dispose()
            objCmd = Nothing
        End If

        objParaUserName = Nothing
        objParaApplicationType = Nothing
    End Sub

    Public Function GetRights() As DataTable
        'Code Added by Mayuri:20090829 To fetch data from database into flexgrid
        'Added Rights Form in Edit Menu

        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing
        Dim sQLQuery As String = ""
        Try
            'query string commented and modified by dipak 20091029 for EMR-PM data migration changes. of new field "ApplicationType"
            'sQLQuery = "SELECT IsNULL(nRightsId,0) AS nRightsId,ISNULL(sRightsName,'') AS sRightsName,ISNULL(sRightsValue,'') AS sRightsValue,ISNULL(sParentRightsName,'') AS sParentRightsName from Rights_MST"
            sQLQuery = "SELECT IsNULL(nRightsId,0) AS nRightsId,ISNULL(sRightsName,'') AS sRightsName,ISNULL(sRightsValue,'') AS sRightsValue,ISNULL(sParentRightsName,'') AS sParentRightsName from Rights_MST where ISNULL(ApplicationType,0)=0 "

            objCon.ConnectionString = GetConnectionString()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = sQLQuery
            objCmd.Connection = objCon

            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = objCmd
            dt = New DataTable
            da.Fill(dt)
            objCon.Close()

            'Return dv
        Catch ex As SqlException

        Catch ex As Exception

        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
        End Try
        Return dt
    End Function

    Public Function GetParentRights() As DataTable
        'Code Added by Mayuri:20090829 To fill Field Parent Rights Name into combobox on Load
        'Added Rights Form in Edit Menu

        Dim objCon As New SqlConnection
        Dim da As New SqlDataAdapter
        Dim dt As DataTable
        Dim sParentsRightName As String = ""
        Try

            objCon.ConnectionString = GetConnectionString()

            Dim objCmd As New SqlCommand("gsp_FillParentsRightName", objCon)
            objCmd.CommandType = CommandType.StoredProcedure
            objCon.Open()

            da.SelectCommand = objCmd
            dt = New DataTable
            da.Fill(dt)
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            objCon.Close()
            Return dt

        Catch ex As SqlException
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Rights", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally

            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
        End Try

    End Function

    Public Function AddNewRights(ByVal ID As Long, ByVal RightsName As String, ByVal RightsValue As String, ByVal ParentRightsName As String) As Int64
        'Code Added by Mayuri:20090829 To add/Modify Rights 
        'Added Rights Form in Edit Menu

        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim sqlParam As SqlParameter
        Dim sqlParamId As SqlParameter

        Try
            objCon.ConnectionString = GetConnectionString()

            objCmd = New SqlCommand("gsp_InUpRights", objCon)
            objCmd.CommandType = CommandType.StoredProcedure

            sqlParamId = objCmd.Parameters.Add("@RightID", SqlDbType.BigInt)
            sqlParamId.Direction = ParameterDirection.InputOutput
            sqlParamId.Value = ID

            sqlParam = objCmd.Parameters.Add("@RightsName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = RightsName

            sqlParam = objCmd.Parameters.Add("@ParentsRightName", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = ParentRightsName

            sqlParam = objCmd.Parameters.Add("@RightsValue", SqlDbType.VarChar)
            sqlParam.Direction = ParameterDirection.Input
            sqlParam.Value = RightsValue

            objCon.Open()
            objCmd.ExecuteNonQuery()



            If ID <> 0 Then

                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Modify, "Rights Modified", gloAuditTrail.ActivityOutCome.Success)

            Else
                'To set Focus on newly added Right
                ID = sqlParamId.Value

                gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, "Rights Added", gloAuditTrail.ActivityOutCome.Success)
            End If
            Return ID

        Catch ex As SqlException

            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(gstrSQLError, gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Catch ex As Exception
            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Admin, gloAuditTrail.ActivityCategory.ICD9, gloAuditTrail.ActivityType.Add, ex.ToString, gloAuditTrail.ActivityOutCome.Failure)
            MessageBox.Show(ex.ToString, "Rights", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        Finally
            objCon.Close()
            objCon.Dispose()
            objCon = Nothing
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If

            sqlParam = Nothing
            sqlParamId = Nothing
        End Try

    End Function

    'Function To check whether right already exists means check for duplicate Rights
    Public Function CheckDupRights(ByVal sRightsName As String, ByVal sParentRightsName As String, ByVal Id As Int64) As Integer
        Dim objCon As New SqlConnection
        'Dim da As New SqlDataAdapter
        'Dim dt As DataTable
        Dim cnt As Integer = 0
        Dim _strSQL As String = ""
        Dim objCMD As SqlCommand = Nothing

        Try
            objCon.ConnectionString = GetConnectionString()
            If Id = 0 Then
                'query string commented and modified by dipak 20091029 for EMR-PM data migration changes. of new field "ApplicationType"
                '_strSQL = " select  count(*) from Rights_MST  where  sRightsName = '" & sRightsName.Replace("'", " ") & "' and  sParentRightsName= '" & sParentRightsName.Replace("'", " ") & "'"
                _strSQL = " select  count(*) from Rights_MST  where  sRightsName = '" & sRightsName.Replace("'", " ") & "' and  sParentRightsName= '" & sParentRightsName.Replace("'", " ") & "'" & "and ISNULL(ApplicationType,0) = 0"
            Else
                'query string commented and modified by dipak 20091029 for EMR-PM data migration changes. of new field "ApplicationType"
                '_strSQL = " select  count(*) from Rights_MST  where  sRightsName = '" & sRightsName.Replace("'", " ") & "' and  sParentRightsName= '" & sParentRightsName.Replace("'", " ") & "'and nRightsId <> " & Id & " "
                _strSQL = " select  count(*) from Rights_MST  where  sRightsName = '" & sRightsName.Replace("'", " ") & "' and  sParentRightsName= '" & sParentRightsName.Replace("'", " ") & "'and nRightsId <> " & Id & "and ISNULL(ApplicationType,0) = 0 "

            End If

            objCMD = New SqlCommand
            objCon.Open()
            objCMD.Connection = objCon
            objCMD.CommandType = CommandType.Text
            objCMD.CommandText = _strSQL
            'Contains count of duplicate records
            cnt = objCMD.ExecuteScalar()

        Catch ex As Exception
            Throw ex
        Finally
            If objCon.State = ConnectionState.Open Then
                objCon.Close()
            End If
            objCon.Dispose()
            objCon = Nothing
            If objCMD IsNot Nothing Then
                objCMD.Parameters.Clear()
                objCMD.Dispose()
                objCMD = Nothing
            End If
        End Try
        'Returns count of duplicate Records
        Return cnt
    End Function
    Public Function selectRight(ByVal Right_ID As Long) As DataTable
        'Code Added by Mayuri
        'Added Rights Form in Edit Menu
        '20090829
        Dim objCon As New SqlConnection
        Dim objCmd As New SqlCommand
        Dim da As SqlDataAdapter = Nothing
        Dim dt As DataTable = Nothing

        Dim sQLQuery As String = ""
        Try
            'query string commented and modified by dipak 20091029 for EMR-PM data migration changes. of new field "ApplicationType"
            'sQLQuery = "SELECT IsNULL(nRightsId,0) AS nRightsId,ISNULL(sRightsName,'') AS sRightsName,ISNULL(sRightsValue,'') AS sRightsValue,ISNULL(sParentRightsName,'') AS sParentRightsName from Rights_MST WHERE nRightsId =" & Right_ID & ""
            sQLQuery = "SELECT IsNULL(nRightsId,0) AS nRightsId,ISNULL(sRightsName,'') AS sRightsName,ISNULL(sRightsValue,'') AS sRightsValue,ISNULL(sParentRightsName,'') AS sParentRightsName from Rights_MST WHERE nRightsId =" & Right_ID & "and ISNULL(ApplicationType,0) =0"
            objCon.ConnectionString = GetConnectionString()
            objCmd.CommandType = CommandType.Text
            objCmd.CommandText = sQLQuery
            objCmd.Connection = objCon

            objCon.Open()
            da = New SqlDataAdapter
            da.SelectCommand = objCmd
            dt = New DataTable
            da.Fill(dt)
            objCon.Close()

        Catch ex As SqlException

        Catch ex As Exception

        Finally
            If Not IsNothing(objCon) Then
                objCon.Dispose()
                objCon = Nothing
            End If
            If objCmd IsNot Nothing Then
                objCmd.Parameters.Clear()
                objCmd.Dispose()
                objCmd = Nothing
            End If
            If Not IsNothing(da) Then
                da.Dispose()
                da = Nothing
            End If
        End Try
        Return dt
    End Function
    'Public ReadOnly Property GetDataview() As DataView
    '    Get

    '        'Dv = Ds.Tables("Category_Mst").DefaultView
    '        Return dv
    '        'Return Ds
    '    End Get
    'End Property
End Class
