using System;
using System.Collections.Generic;
using System.Text;


namespace gloAuditTrail
{




    public enum ActivityModule
    { 
        None = 0,
        Security = 1,
        Appointment = 2,
        Scheduling = 3,
        Billing = 4,
        Patient = 5,
        Contact = 6,
        AppointmentBook = 7,
        Setting = 8,
        Admin = 9,
        Fax = 10,
        ERAPosting = 11, // Added by Dev66
       
        #region "EMR ActivityModules"
       // General=10,
        Exam =12 ,
        Prescription = 13 ,
        Medication = 14 ,
        History = 15 ,
        Orders = 16 ,
        Voice = 17 ,
         CCD = 18 ,
        HL7 = 19 ,
        Genius =20 ,
        Labs = 21 ,
        DiseaseManagement = 22 ,
        CardioVascular = 23,
        Synopsis = 24 ,
        ROS = 25 ,
        PTProtocol = 26,
        ReminderLetter = 27 ,
        TemplateGallery = 28 ,
        Drugs = 29,
        Immunization = 30,
        ProblemList = 31 ,
        Letters = 32 ,
        Messages = 33 ,
        Triage = 34 ,
        DMS = 35 ,
        Flowsheet = 36,
        Formgallery = 37,
        EnM = 38,
        Vitals = 39,
        PatientConsent = 40,
        DisclosureManagement = 41,
        DICOM = 42,
        Task = 43,
        VMS = 44,
        NurseNotes = 45,
        MUReports = 46,
        PatientSummary = 47,
        Dashboard = 48,
        Category = 49,
        Visit = 50,
        //sanjog 20100727
        Reports = 51,
        //sanjog 20100727
        CDS = 52,
        gloCommunity = 53,//added on 20120204
        CQMReports = 54, // By Pranit on 17 march 2012
        
        SecureMessage = 55,      
        #endregion
      
        
        #region "CDS"
        DM_RuleSetup=56,
        ViewRecommendationRule = 57,
        Amedments = 58,
        CarePlan = 59,
        #endregion
        RxSavings = 60,
        RCM = 61,  //7022 Items: Remove claim from queue manually
        ICD = 62,
        DailyClose = 63,
        OBPlan = 64,
        PastPregnancies = 65,
        smartdiagnosis = 66,
        ChargeRule = 67,
        NYWCForms = 68,
        RCMDMS = 69,
        EPA = 70,
        Batch=71,
        ImplantableDevice=72,
        SocialPsychologicalBehavioralobservations = 73,
        ProviderMultipleTaxID = 74,
        PHI=75,
        PlanOfTreatment=76,
        API=77,
        PatientRepresentative=78,
        SummaryCareRecord = 79,
        InsurancePayment=80,
        ERAPayer=81,
        ACI=82,
        ScreeningTools = 83,
        Cleargage = 84,
        PDMP = 85
    }

    public enum ActivityCategory
    {
        None = 0,
        BillingCharges = 1,
        Payment = 2,
        LoginScreen = 3,
        LockScreen = 4,
        SetupPatient = 5,
        ViewAppointment  = 6,
        SetupAppointment = 7,
        SearchAppointment = 8,
        Hospital = 9,
        Insurance = 10,
        Pharmacy = 11,
        Physician = 12,
        Department = 13,
        Provider_AppointmentType_Association = 14,
        AppointmentBlockType = 15,
        AppointmentStatus = 16,
        AppointmentType = 17,
        ClinicDays = 18,
        FlagType = 19,
        FollowUp = 20,
        Location = 21,
        POS = 22,
        Resource = 23,
        ResourceType = 24,
        Template = 25,
        TemplateAllocation = 26,
        TOS = 27,
        ProblemType = 28,
        Relationship = 29,
        ICD9 = 30,
        CPT = 31,
        Drugs = 32,
        Modifier = 33,
        Category = 34,
        InsuranceCPTAssociation = 35,
        Facility = 36,
        CodeType = 37,
        SmartTreatment = 38,
        Speciality = 39,
        InsuranceServiceType = 40,
        InsurancePlancode = 41,
        InsuranceType = 42,
        ClearingHouse = 43,
        AdjustmentType = 44,
        StandardFeeSchedule = 45,
        ViewSchedule  = 46,
        SetupSchedule = 47,
        SetupCharges = 48,
        ViewTransaction = 49,
        SetupHCFA1500 = 50,
        SetupStandardFeeSchedule = 51,
        Provider = 52,
        Clinic = 53,
        User = 54,
        Settings = 55,
        SetupCoPay = 56,
        PrintReceipt = 57,
        ClaimStatus = 58,
        DailyCollection = 59,
        MissingCharges = 60,
        MonthEndReport = 62,
        NewPatientVsEst_PatientRpt = 63,
        OverdueReport = 64,
        PatientLedger = 65,
        PatientReport = 66,
        ProviderType = 67,
        PendingCoPay = 68,
        TransactionHistory = 69,
        TransactionHistoryAnalysis = 70,
        ZeroBalanceReport = 71,
        ClaimDetailsReport = 72,
        Eligibility= 73,
        SetupUB04=74,
        MedicationRefill = 75,
        MedicationHistory = 76,

        ERATemporaryPosting = 77, // Added by Dev66
        ERAOriginalPosting = 78, // Added by Dev66
        ERAInsurancePaymentSave = 79,

        UC_TemplateTreeControl = 80,
        UC_PastWordNotes = 81,
        UC_AddRefreshDic = 82,

        #region "EMRActivityCategory"
        //General=83
        SmartDiagnosis = 84,
        SmartOrders = 85,
        PatientEducation = 86,
        ProviderReference = 87,
        Guidelines = 88,
        //Added by Mayuri:20100423-Case No:#0003868
        PatientMessages = 89,
        PatientLetters = 90,
        PatientConsent = 91,
        Triage = 92,
        PTProtocol = 93,
        History = 94,
        NurseNotes = 95,
        MUReport = 96,
        //Prescription Module
        eRx = 97,
        RefillRequest = 98,
        RefillResponce = 99,
        CreatePrescription = 100,
        DrugInteraction = 101,
        ModifyPrescription = 102,
        DeletePrescription = 103,
        PrintPrescription = 104,
        FaxPrescription = 105,
       /// <summary>
       /// Medication Module
       /// </summary>
        CreateMedication = 106,
        ModifyMedication = 107,
        DeleteMedication = 108,
        PrintMedication = 109,
        FaxMedication = 110,
        /// <summary>
        /// History,DMS, DICOM etc. Module
        /// </summary>
        Review = 111,

        /// <summary>
        /// History Module
        /// </summary>
        HistoryofHistory = 112,
        /// <summary>
        /// History Module
        /// </summary>
        Narrative = 113,
      
        /// <summary>
        /// ROS Module
        /// </summary>
        CreateROS = 114,

        /// <summary>
        /// Orders Module
        /// </summary>
        CreateOrders = 115,
    

        /// <summary>
        /// VOICE Module
        /// </summary>
        DoctorSpeakerConfiguration = 116,

        /// <summary>
        /// VOICE Module
        /// </summary>
        CreateSpeaker = 117,

        /// <summary>
        /// VOICE Module
        /// </summary>
        Dictation = 118,


        /// <summary>
        /// VOICE Module
        /// </summary>
        VoiceCommands = 119,

        /// <summary>
        ///  Exam, Patient Module
        /// </summary>
        Referrals = 120,

        /// <summary>
        /// CCD, Templates, DICOM , DMS ,  Module        
        /// </summary>
       Import =121,

       /// <summary>
       /// CCD, Templates, DICOM , DMS ,  Module        
       /// </summary>
       Export = 122,

       /// <summary>
       /// CCD Module         
       /// </summary>
       ViewCCD = 123,
        
        /// <summary>
        /// DMS
        /// </summary>
        Scan=124,

        /// <summary>
        /// HL7 ActivityModules
        /// </summary>
        CreateA04 = 125,
                /// <summary>
        /// HL7 ActivityModules
        /// </summary>
        CreateA08 = 126,
        /// <summary>
        /// HL7 ActivityModules
        /// </summary>
        CreateP03 = 127,
        /// <summary>
        /// HL7 ActivityModules
        /// </summary>
        CreateO01 = 128,

        /// <summary>
        /// Genius ActivityModules
        /// </summary>
        SendCharges = 129,

        /// <summary>
        /// All Masters like History, ROS, Labs, IM, Flosheet , Template etc ActivityModules
        /// </summary>
        Setup = 130,

        /// <summary>
        /// Labs, DICOM, DMS etc ActivityModules
        /// </summary>
        Acknowledgement = 131,

        /// <summary>
        /// Labs ActivityModules
        /// </summary>
        CreateLabs = 132,
        // Added by Abhijeet on 2010192010
        ModifyLabs = 133,

        /// <summary>
        /// Labs ActivityModules
        /// </summary>
        PreviousLabs = 134,

        /// <summary>
        /// Labs,Orders, History,Rx, Medication,ROS etc ActivityModules
        /// </summary>
        ShowPrevious = 135,
        
        /// <summary>
        /// History, ROS etc ActivityModules
        /// </summary>
        MakeasCurrent = 136,

        /// <summary>
        ///  DM ActivityModules
        /// </summary>
        PatientSpecificCriteria = 137,

        /// <summary>
        ///  DM ActivityModules
        /// </summary>
FindHealthPlan = 138,

/// <summary>
///  CardioVascular ActivityModules
/// </summary>
FindCriteria = 139,

        /// <summary>
///  Sysnopsis ActivityModules
/// </summary>
CVStressTest = 140,

/// <summary>
///  Sysnopsis ActivityModules
/// </summary>
Electrophysiology = 141,

/// <summary>
///  Sysnopsis ActivityModules
/// </summary>
ImplantDevices = 142,

/// <summary>
///  Sysnopsis ActivityModules
/// </summary>
Ejectionfraction = 143,

/// <summary>
///  Sysnopsis ActivityModules
/// </summary>
Intervention = 144,

/// <summary>
///  Sysnopsis ActivityModules
/// </summary>
ShowCheckinPatient = 145,

/// <summary>
///  Sysnopsis ActivityModules
/// </summary>
SynopsisScreen = 146,

        /// <summary>
        ///  Templates ActivityModules
        /// </summary>
        InsertSignature = 147,

        /// <summary>
        ///  Templates ActivityModules
        /// </summary>
        InsertFile = 148,

        /// <summary>
        ///  Templates ActivityModules
        /// </summary>
        Finish = 149,

        /// <summary>
        ///  Templates ActivityModules
        /// </summary>
        Redo = 150,

        /// <summary>
        ///  Templates ActivityModules
        /// </summary>
        Undo = 151,

        /// <summary>
        ///  ReminderLetter, Patient ActivityModules
        /// </summary>
        Reports=152,

        /// <summary>
        ///  TemplateGallery ActivityModules
        /// </summary>
        FillDatadictionary = 153,
        /// <summary>
        ///  TemplateGallery ActivityModules
        /// </summary>
        InsertField = 154,
                /// <summary>
        ///  TemplateGallery ActivityModules
        /// </summary>
        AddBookMark = 155,
                /// <summary>
        ///  TemplateGallery ActivityModules
        /// </summary>
        DeleteBookMark = 156,
                /// <summary>
        ///  TemplateGallery ActivityModules
        /// </summary>
        ShowHideBookMark = 157,
        /// <summary>
        ///  TemplateGallery ActivityModules
        /// </summary>
        InsertCheckBox = 158,
        /// <summary>
        ///  TemplateGallery ActivityModules
        /// </summary>
        InsertDropDown = 159,
        /// <summary>
        ///  TemplateGallery ActivityModules
        /// </summary>
        InsertHeader = 160,

        /// <summary>
        ///  EnM ActivityModules
        /// </summary>
        Assciation = 161,

        /// <summary>
        ///  EnM ActivityModules
        /// </summary>
        GenerateCode = 162,

        /// <summary>
        ///  Vitals ActivityModules.
        ///  Vital Norms
        /// </summary>
        Validate = 163,

        /// <summary>
        ///  Vital ActivityModules
        /// </summary>
        AdvancedChart = 164,

        /// <summary>
        ///  Vital ActivityModules
        /// </summary>
        ShowGraph = 165,

        /// <summary>
        ///  DMS ActivityModules
        /// </summary>
        ReceivedFax = 166,
        
        /// <summary>
        ///  DMS, DICOM ActivityModules
        /// </summary>
        Notes = 167,

        /// <summary>
        ///  DMS, DICOM ActivityModules
        /// </summary>
        Amendments = 168,

        /// <summary>
        /// DICOM ActivityModules
        /// </summary>
        AdjustSpeed = 169,

        /// <summary>
        /// Mesages ActivityModules
        /// </summary>
        Reply = 170,

        /// <summary>
        /// VMS ActivityModules
        /// </summary>
        UploadVideo = 171,

              /// <summary>
        /// Dashboard ActivityModules
        /// </summary>
        CheckIn = 172,

        /// <summary>
        /// Dashboard ActivityModules
        /// </summary>
        CheckOut = 173,

        /// <summary>
        /// Dashboard ActivityModules
        /// </summary>
        ViewPatientDemographics = 174,
        /// <summary>
        /// Dashboard ActivityModules
        /// </summary>
        ViewPatientCard = 175,

        /// <summary>
        /// Dashboard ActivityModules
        /// </summary>
        PatientAlert = 176,
        /// <summary>
        /// Dashboard ActivityModules
        /// </summary>
        SurgicalAlert = 177,
        /// <summary>
        /// Dashboard ActivityModules
        /// </summary>
        ChangeDoctor = 178,
        /// <summary>
        /// Dashboard ActivityModules
        /// </summary>
        MyDay = 179,
        /// <summary>
        /// Dashboard ActivityModules
        /// </summary>
        PatientDetail = 180,

        CloseTransaction = 181,
        /// <summary>
        /// TeamplateGallery ActivityModules
        /// </summary>
        DataDictionary = 182,
        /// <summary>
        /// Patient ActivityModules
        /// </summary>
        ConfidentialInformation = 183,
        /// <summary>
        /// Exam ActivityModules
        /// </summary>
        Diagnosis = 184,

        /// <summary>
        ///  Sysnopsis ActivityModules
        /// </summary>
        CVRisk = 185,
        /// <summary>
        ///  Appointment ActivityModules
        /// </summary>
        PullCharts = 186,

        /// <summary>
        /// Exam ActivityModules
        /// </summary>
        DrawingPad = 187,

        /// <summary>
        /// Patient ActivityModules
        /// </summary>
        ChiefComplaints = 188,

        /// <summary>
        /// Imunization ActivityModules
        /// </summary>
        Validation = 189,

        /// <summary>
        /// Imunization ActivityModules
        /// </summary>
        IMReport = 190,

        /// <summary>
        /// Templates & word relates forms ActivityModules
        /// </summary>
        WINWORD = 191,
        /// <summary>
        /// Admin module ActivityModules
        /// </summary>
        ChangePassword = 192,
        Fax = 193,
        // added by chetan on 20100712
        
        UnLockChart = 194,
        BreakTheGlass = 195,
        ElectroCardioGram = 196,
        Encryption = 197,
        GenerateCCD = 198,
        CVCatheterization = 199,
        Query = 200,
        PatientRecordModified = 201,
        ClinicalChart = 202,
        Spirometry = 203,
#endregion
        //enum added for InterfaceReport on 20110113 by Rohit
        ViewInterfaceReport = 204,
        //enum added for InterfaceReport on 20110113 by Rohit
        ViewCDS = 205,
        CQMReport = 206, // Added by Pranit on 16 march 2012
        IntuitMessage = 268,
        Reconciliation = 207,

        #region "CDS"
       // CDS
        DM_RuleSetup = 208,
        ViewRecommendationRule = 209,
        AmedmentsSetupScreen = 210,    
        AddAmedments = 211,
        ModifyAmedments = 212,
        DeleteAmedments = 213,
        ViewAmedments = 214,
        AcceptAmedments = 215,
        DeniedAmedments = 216,
        PendingAmedments = 217,
        RefreshViewAmedments = 218,
        ProblemList = 219,
        //Activate_Deactivate_RuleSetup=220,
        //CopyRule=221,
        //NewRule=222,
        //ModifyRule=223,
        //deleteRule=224,
        //RefereshRules=225,
        //CloseRuleSetup=226,
        //ViewRecommendationScreen=227,
        //RecommendationMarkSatisfied=228,
        //RecommendationReopened=229,
        //RecommendationInProcess=230,
        //RecommendationCancelAsNotAppilcable=231,
        //RecommendationSnooze=232,
        //RecommendationViewRuleReference=233,
        //RecommendationViewHistory=234,
        //RecommendationNoteUpdate=235,
        DxSnomed = 236,
        NewMessage = 237,
        ViewRxSaving = 238,
        PrintRxSaving = 239,
        DispositionRxSaving = 240,
        Prescription = 241,        
        #endregion
        Time = 242,
        DailyCloseAlert = 243,
        Formulary30 = 244,
        EpcsPrescriberRegistration = 245,
        smartdiagnosissetup = 246,
        ChargeRuleSetup = 247,
        ChargeRuleSetting = 248,
        ChargeRuleEvaluation = 249,
        NYWCForms = 250,
        ICD10 = 251,
        CollectionAgency = 252,
        ModifyDocument = 253,
        ViewDocument = 254,
        PrintDocument = 255,
        Batch = 256,
        BadDebtStatus = 257,
        InsuranceFollowUpWQ = 258,
        AccountFollowUpWQ=259,
        TransferClaim = 260,
        ResendBatchClaims = 261,
        RebillBatchClaims=262,
        PriorAuthorization = 263,
        Reserves = 264,
        FollowUpScheduledActions = 265,

        #region "EMRADMIN"
        AdditionOfUserPrivileges = 266,
        ChangesToUserPrivileges = 267,
        Merge=268,
        #endregion
        PatientCommunication = 269,
        ReasonCodeSetup=270,
        Task = 271,
        ViewedSocialPsychologicalBehavioralobservations = 272,
        RxChangeRequest = 273,
        ModifiedSocialPsychologicalBehavioralobservations = 274,
        SavedSocialPsychologicalBehavioralobservations = 275,
        RxRequest = 276,
        ViewDocumentErrors = 277,
        RecordCDAErrors = 278,
        ProviderMultipleTaxIDView=279,
        SecureMessage = 280,
        SubmitPHI=281,
        ReviewPHI=282,
        ViewPHI=283,
        CancelRx=284,
        RxFill=285,
        APIUser=286,
        APIRole=287,
        CCDAPatientConsent=288,
        PatientRepresentative=289,
        PatientHealthConcern=290,
        PatientGoal = 291,
        PatientIntervention = 292,
        PatientOutcome = 293,
        ClinicalReconciliation=294,
        RxChangeResponse = 295,
        SummaryCareRecord = 296,
        DMAlert=297,
        HoldBilling = 298,
        AppointmentDefault=299,
        InsuranceClaimFollowupMapping=300,
        QPPSubmission=301,
		PrintLoginCredentials = 302,
        GenerateUserName = 303,
        CleargagePayment = 304,
        CleargageDiscount = 305,
        CleargageFee = 306,
        MUScreeningTools = 307,
        CleargageURL=308,
        CleargageOneTimePayment=309,
        CleargageAddPaymentPlan=310,
        CleargageEditPaymentPlan=311,
        CleargageRecentTransaction=312,
        UnlockRecord = 313,
        CleargageVoidRefund=314,
        PDMPPost = 315,
        PDMPGet = 316,
        PDMPView = 317,
        PatientControlCustomization = 318
    }

    public enum ActivityType
    {

        None = 0,
        Add = 1,
        Modify = 2,
        Delete = 3,
        View = 4,
        Print = 5,
        PrintAll = 6,
        Fax = 7,
        FaxAll = 8,
        Login = 9,
        Logoff = 10,
        AddTransactionLine =11,
        RemoveTransactionLine = 12,
        LoadSmartTreatment = 13,
        ValidateClaim = 14,
        Select = 15,
        Remove = 16,
        AssociateEMRPatient = 17,
        RegisterEMRPatient = 18,
        CancelOperation = 19,
        ExportToExcel = 20,
        GenerateEDI=21,
        Finish=22,
       
        
        //code added by supriya 07/04/2009
        #region "EMRActivityType"
        Refresh = 23,
        General = 24,
        PatientRecordAdded = 25,
        PatientRecordModified = 26,
        PatientRecordViewed = 27,
        Query = 28,
        Close = 29,
        Open = 30,
        Initialize = 31,
        Dispose = 32,
        Search = 33,
        NewRx = 34,
        RefillRequest = 35,
        Refillresponse = 36,
        ApproveRefillRequest = 37,
        DenyRefillRequest = 38,
        Load = 39,
        //Sanjog 20100727
        ExportReport = 40,
        Refill = 41,
        Send = 42,
        Reply = 43,
        //Sanjog 20100727
                #endregion

        // code added by Abhijeet on 20100512
        #region "gloLabActivityType = 44"
        RegisterLabPatient = 45,
        LabOrderRequest = 46,
        RetrivedAllLabOrders = 47, 
        ModifyLabPatient = 48,
        #endregion "gloLabActivityType"
        

#region " ERA Posting Activity Types -- Added on 2-July-2010 by Dev66 "

        IsCheckInProcess = 49,
        PreSaveValidation = 50,
        GetAllBPRClaims = 51,
        ISClaimMatch = 52,
        DeterminePayer = 53,
        ReasonsToStopPayment = 54,
        GetCheckDetails = 55,
        FillMasterDetails = 56,
        FillPaymentTray = 57,
        GetPaymentMaster = 58,
        GetMainCreditLineEntry = 59,
        GetCheckClaimDetails = 60,
        ResetClaimDetails = 61,
        FillClaimDetails = 62,
        ChargeOverPaid = 63,
        CalculateNewBalance = 64,
        ZeroApproved = 65,
        VerifyPaymentCorrection = 66,
        UseReserved = 67,
        EOBLine = 68,
        SetLineReasonCodes = 69,
        StatementNotes = 70,
        InternalNotes = 71,
        SetNextActionDetails = 72,
        SaveOperationStarts = 73,
        SaveOperationEnds = 74,
        SaveOperationAborted = 75,
        ChargeMatchedAgainstClaim = 76,
        DetermineNextActionParty = 77,
        PaymentChargesOverPaid = 78,
        PaymentBalanced = 79,
        DebitServiceLine = 80,
        DebitServiceLineWriteOff = 81 ,
        DebitServiceLineWithHold = 82,
        CreateERAPaymentObject = 83,
        AssignERAPaymentObject = 84,
        AddERAPaymentToListObject = 85,
        NoClaimProcessed = 86,

        GetTemporaryPostedData =87 ,
        ValidateDataSet = 88,
        ClaimIterationStart = 89,
        ClaimIterationEnd = 90,
        ChargeIterationStart = 91 ,
        ChargeIterationEnd = 92,

        SaveMasterDetails = 93,
        SaveNotes = 94,
        SaveEOBDetails = 95,
        SaveEOBLine = 96,
        SaveFinancialLine = 97,
        SaveReasonCodes= 98,
        SaveNextAction = 99,
        SaveCreditLine = 100,
        AssignCreditLineReferences = 101,
        SaveReserveCreditLine = 102,


#endregion
         // added by chetan on 20100712
        ModifyLabs = 103, //Added by kanchan on 20100821 for integration
        Unlock = 104,
        EmergencyAccess = 105,
        Export = 106,
        Save = 107,
        Email = 108,
        ClinicalExchange = 109,
        Extract = 110,
        Reconcile = 111,
        AcceptReconcileList=112,
        CalculateFollowUp=113,//7022 Items: Claim queue reset utility

        #region "CDS"
        //Activate,
        //Deactivate,
        //CopyRule,
        //NewRule,
        //ModifyRule,
        //deleteRule,
        //RefereshRules,
        //CloseRuleSetup,
        //ViewRecommendationScreen,
        //RecommendationMarkSatisfied,
        //RecommendationReopened,
        //RecommendationInProcess,
        //RecommendationCancelAsNotAppilcable,
        //RecommendationSnooze,
        //RecommendationViewRuleReference,
        //RecommendationViewHistory,
        //RecommendationNoteUpdate,
        
        #endregion
        Generate=114,
        Preview=115,
        //AmedmentsSetupScreen=116,       
        //AddAmedments=117,
        //ModifyAmedments=118,
        //DeleteAmedments=119,
        //ViewAmedments=120,
        //AcceptAmedments=121,
        //DeniedAmedments=122,
        //PendingAmedments=123,
        //RefreshViewAmedments=124, 
        Copy = 125,
        SendDisposition = 126,
        Synchronization = 127,
        No = 128,
        Yes = 129,
        OK = 130,
        Cancle=131,
        EligibilityCheck = 132,
        NoCharge = 133,
        Status = 134,
        Invite = 135,
        Activate = 136,
        Register = 137,
        EpcseRx = 138,
        merge = 139,
        BillPendingClaim = 140,
        DeActivate = 141,
        EvaluateRule = 142,
        Ruleconditionadded = 143,
        Ruleconditiondeleted = 144,
        Groupclauseadded = 145,
        Groupclausedeleted = 146,
        SaveWithErrors = 147,
        MarkedInvalid = 148,
        Batch = 149,
        SendtoClaimManager = 150,
        ResendToClaimManager = 151,
        TransferAccountBalance = 152,
        Checked = 152,
        Unchecked = 153,
        Action = 154,
        TransferClaimResponsibilityToSelf = 155,
        ResendBatchClaims = 156,
        RebillBatchClaims = 157,
        UserRoles = 158,
        TransferClaimsInsuranceResponsibility = 159,
        ReservesDistribution = 160,
        DITrigger = 161,
        QuickOrdersLab=162,
        QuickOrdersOrder=163,
        QuickOrdersGuidline=164,
        QuickOrdersDrug=165,
        QuickOrdersIM=166,
        findhealthplan=167,
        Accept=168,
        Reject=169,
        AssociatedWithPatient=170,
        FSTrigger=171,
        DMAlertTriggered=172,
        Download=173,
        Import=174,
        OneTimePaymentBegin=175,
        OneTimePaymentEnd = 176,
        AddPaymentPlanBegin = 177,
        AddPaymentPlanEnd = 178,
        EditPaymentPlanBegin = 179,
        EditPaymentPlanEnd = 180,
        RecentTransctionBegin = 181,
        RecentTransctionEnd = 182
    }

    public enum ActivityOutCome
    {
        Success = 0,
        Failure = 1
    }

    public enum SoftwareComponent
    {
        None = 0,
        gloPM = 1,
        gloPMAdmin = 2,
        gloExchangeService = 3,
        gloClaimService = 4,

         #region "EMRActivityType"
        gloEMR = 5,
        gloEMRAdmin = 6,
        gloGenius = 7,
        gloHL7 = 8,
        gloRxSniffer = 9,
        gloeFax = 10
        #endregion
       
    }
}
