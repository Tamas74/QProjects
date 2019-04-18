namespace gloPM
{
    partial class frmDashBoardMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                try
                {
                    gloGlobal.cEventHelper.RemoveAllEventHandlers(this);
                }
                catch
                {
                }
                try
                {
                    if (oClsgloUserRights != null)
                    {
                        oClsgloUserRights.Dispose();
                        oClsgloUserRights = null;
                    }
                }
                catch
                {
                }
                try
                {
                    if (HelpComponent1 != null)
                    {
                        gloGlobal.cEventHelper.RemoveAllEventHandlers(HelpComponent1);
                        HelpComponent1.Dispose();
                        HelpComponent1 = null;
                    }
                }
                catch
                {
                }
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()  
        
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDashBoardMain));
            this.mnuMainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFile_New = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFile_Modify = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFile_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFile_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFile_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFile_Lock = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFile_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_Contacts = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEdit_AppointmentBook = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_BillingBook = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_TaskMailBook = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView_TemplateGallary = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_TemplateAssociation = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_RCMCagetory = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit_ICD9CPTGallery = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMaster_ICD9Gallery = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMaster_ICD10Gallery = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMaster_CPTGallery = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_NewPatient = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_ModifyPatient = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_CardScanning = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_ScanDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuGo_Appointment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_Schedule = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuGo_Billing = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_BatchProcessing = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_ClaimProcessing = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_PaymentPatient = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_PaymentInsurace = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_ERAPayment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_Payment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_DailyClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuGo_ClosedJournals = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_ChargesTray = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_PatientStatementNotes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_PatientLedger = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_Remittance = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_EOBLedger = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_PatientStatment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_RevenueCycle = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_CopayDistributionList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCopayDist_ByAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCopayDist_ByCharge = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_ReservesDistributionList = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGo_CollectionAgencyRefund = new System.Windows.Forms.ToolStripMenuItem();
            this.munView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView_Appointment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView_Schedule = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView_Reminders = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuView_Billing = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView_BatchProcessing = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView_ClaimProcessing = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView_Payment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView_PatientTemplates = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView_PatientBatchStatement = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView_Tasks = new System.Windows.Forms.ToolStripMenuItem();
            this.messageQueueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchEligibilityActivityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBatch_Eligibility = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView_Documents = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView_CleargageFileHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_Reports = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuReports_ReportingTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_PracticeAnalysis = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu_MISReports = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_PatientPaymentHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_PatientTransactionHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu_MISReports_Productivity = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ProductivityRVU = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ProductivityProviderPaymentMthd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ProductivityDOS = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ExpectedCollection = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ProductionByDoctor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ProductionByFacility = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ProductionByDate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ProductionByMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ProductionByProcedureGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ProductionByProcedureCode = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ProductionByInsuranceCarrier = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ProductionByFacilityByPatient = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ProductionByFacilityByPatientDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu_MISReports_ReimbursementByMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ReimbursementByMonthDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ReimbursementByInsuranceCarrier = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ReimbursementByInsuranceByCPT = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ReimbursementByCPTByInsurance = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ReimbursementByDoctorByInsurance = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ReimbursementByInsuranceForCPT = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ReimbursementDetailsByAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator26 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu_MISReports_Aging = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_FinancialSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_FinancialPayments = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_AvailableReserve = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_DenialManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu_MISReports_DailyCharges = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_DailyPayments = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_DailySummary = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_MonthlyCharges = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_MonthlyPayments = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_MonthlyClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_FinancialProSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_Fin_ProdSummaryDX = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_CachedFinancialProductivitySum = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_AgedPayment = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu_MISReports_ProductionByPhysicianGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ProductionAnalysisByFacility = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ProductionAnalysisByprocedureGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ProductionAnalysisandTrendsByMonth = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ProductionTrendsByProcedureGrop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu_MISReports_CPTAnalysis = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_QualityMeasures = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_ChargesVSAllowedReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_PayerLagReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_PaymentPlanReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_BadDebtCollectionReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MISReports_MTDYTDReport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu_rpt_PaymentTrayReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_rpt_Appointments = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_rpt_ConfirmAppointments = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_rpt_NoShowAppointments = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_rpt_MissedOpportunitiesReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_rpt_AppointmentCensusReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_rpt_PatientBenefitsInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_rpt_AppointmentWaiting = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRpt_BatchPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRpt_VoidClaims = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MissingChargesReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReportsPendingCopayReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_MonthEndReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_OvreduePatientPayment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_OverdueInsurancePayment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_PatientVsEstablishedReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_DailyCollectionReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReportsRefund = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_ZeroBalancePatient = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReportsTransactionHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_InsuranceCompanySetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_InsuranceCompanySetup_Company_Category = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPriorAuthReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTransactionNotes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTransactionHistoryAnalysis = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_patientReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_ProviderReferral_Patients = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Reports_DplicateCliam = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_ChargesPayments = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_PatientRecall = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_PrintLabels = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_PrintLabels_Patient = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_PrintLabels_Insurance = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_PrintLabels_Contacts = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_PrintLabels_Employies = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_PrintList = new System.Windows.Forms.ToolStripMenuItem();
            this.patientToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.guarantorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insurancesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.billingCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.diagnosisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_Graphs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_ClaimStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Reports_PatientByDOBReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Reports_PatientExcludeSt = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Reports_BatchReport = new System.Windows.Forms.ToolStripMenuItem();
            this.payerListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_gatewayEDI = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMyReportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listOfClaimsRecivedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.staffProductivityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myFilesRecivedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myFileLocationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateMyWebAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeMyPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewProviderInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.referringProviderNPITableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enrollmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.providerEnrollmentFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.claimsStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.providerSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insurerSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.secondarySearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.realtimeClaimStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cCISuspensionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.claimHistoryToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rejectionAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transactionSummaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.safetyNetReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.claimsFileReconcliToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bestPracticesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eligibilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchInquiriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newRealtimeInquiriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takePaymentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeCreditCardPaymentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeACHPaymentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.managePaymentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cCAuthorizationFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aCHAuthorizationFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientStatementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sampleStatementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.climsReviewedToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_AuditTrail = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInterfaceReports = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPatientActivationtReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReimbursementWarning = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBusCenterMismatch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChargeLagReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBatchLagReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_InactiveCPTSReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_ChargeEditReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports_CollectionExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools_UpdateTemplates = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTools_Synchronize = new System.Windows.Forms.ToolStripMenuItem();
            this.blockUnblockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools_MergePatient = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools_MergePatientAccount = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools_CardImage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools_UnLockRecords = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuICDAnalysis = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools_CodeGuide = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools_MergeScheduledActions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools_ChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSecurity = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSecurity_UserManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSecurity_SystemManagemnet = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSecurity_PasswordPolicy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSecurity_PatientLog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSecurity_Forms = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetting_DefaultDashboard = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetting_DefaultPatientSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetting_SystemSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSetting_Appointment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetting_Schedule = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetting_Billing = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSetting_CardScanner = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetting_RefreshDevicesPrinters = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetting_Printer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSetting_Theme2003 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetting_Theme2003Dark = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetting_Theme2007 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetting_Customization = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBilling = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBilling_PatPayment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBilling_InsPayment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBilling_PaymentTray = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBilling_Ledger = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBilling_DailyClose = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBilling_Remittance = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBilling_Charges = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp_HowDoI = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp_Search = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp_Contents = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHelp_TechnicalSupport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp_Version = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp_License = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSupport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp_AboutgloPMS = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.sslbl_Login = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslbl_SingleSignOn = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslbl_Database = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslbl_Version = new System.Windows.Forms.ToolStripStatusLabel();
            this.sslbl_LastModifiedDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.ts_Main = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_PatientRegistration = new System.Windows.Forms.ToolStripButton();
            this.tsb_PatientModification = new System.Windows.Forms.ToolStripButton();
            this.tsb_PatientScan = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_Calendar = new System.Windows.Forms.ToolStripButton();
            this.tsb_Appointment = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_Billing = new System.Windows.Forms.ToolStripButton();
            this.tsb_PaymentPatient = new System.Windows.Forms.ToolStripButton();
            this.tsb_BillingBatch = new System.Windows.Forms.ToolStripButton();
            this.tsb_PaymentInsurance = new System.Windows.Forms.ToolStripButton();
            this.tsb_Advance = new System.Windows.Forms.ToolStripButton();
            this.tsb_ERAPayment = new System.Windows.Forms.ToolStripButton();
            this.tsb_Payment = new System.Windows.Forms.ToolStripButton();
            this.tsb_PatBalance = new System.Windows.Forms.ToolStripButton();
            this.tsb_Remittance = new System.Windows.Forms.ToolStripButton();
            this.tsb_Exit = new System.Windows.Forms.ToolStripButton();
            this.tsb_PatLedger = new System.Windows.Forms.ToolStripButton();
            this.tsb_PatientStatment = new System.Windows.Forms.ToolStripButton();
            this.tsb_Calculator = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_ShowDashboard = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_LockScreen = new System.Windows.Forms.ToolStripButton();
            this.tsb_RevenueCycle = new System.Windows.Forms.ToolStripButton();
            this.tsbDailyClose = new System.Windows.Forms.ToolStripButton();
            this.tsb_ScanDocs = new System.Windows.Forms.ToolStripButton();
            this.tsb_RCMDocs = new System.Windows.Forms.ToolStripButton();
            this.tsb_ClearGagePayment = new System.Windows.Forms.ToolStripButton();
            this.uiPanelManager1 = new Janus.Windows.UI.Dock.UIPanelManager(this.components);
            this.uipnlPatient_Alert = new Janus.Windows.UI.Dock.UIPanel();
            this.uipnlPatient_AlertContainer = new Janus.Windows.UI.Dock.UIPanelInnerContainer();
            this.pnlSideButton = new System.Windows.Forms.Panel();
            this.pnl_Appointment = new System.Windows.Forms.Panel();
            this.label47 = new System.Windows.Forms.Label();
            this.btn_Appointment = new System.Windows.Forms.Button();
            this.label48 = new System.Windows.Forms.Label();
            this.pnl_Calendar = new System.Windows.Forms.Panel();
            this.label49 = new System.Windows.Forms.Label();
            this.btn_Calendar = new System.Windows.Forms.Button();
            this.pnl_Tasks = new System.Windows.Forms.Panel();
            this.label50 = new System.Windows.Forms.Label();
            this.btn_Tasks = new System.Windows.Forms.Button();
            this.label51 = new System.Windows.Forms.Label();
            this.pnlPatientAlertMain = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.pnlc1PatientAlerts = new System.Windows.Forms.Panel();
            this.c1PatientAlerts = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnl_LeftButtons = new System.Windows.Forms.Panel();
            this.btnAppointment = new System.Windows.Forms.Button();
            this.btnCalender = new System.Windows.Forms.Button();
            this.btnMail = new System.Windows.Forms.Button();
            this.btnTask = new System.Windows.Forms.Button();
            this.pnlLeftPatientAlert = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.pnlEligibilityCheck = new System.Windows.Forms.Panel();
            this.c1EligibilityCheck = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlCopayAlert = new System.Windows.Forms.Panel();
            this.c1CopayAlert = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlPatient_UpComingAppointments = new Janus.Windows.UI.Dock.UIPanel();
            this.pnlPatient_UpComingAppointmentsContainer = new Janus.Windows.UI.Dock.UIPanelInnerContainer();
            this._picBkg = new System.Windows.Forms.PictureBox();
            this.c1PatientStatus = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlPatient_Details = new Janus.Windows.UI.Dock.UIPanel();
            this.pnlPatient_DetailsContainer = new Janus.Windows.UI.Dock.UIPanelInnerContainer();
            this.c1PatientDetails = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.pnlSearchFilter = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.label62 = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panel16 = new System.Windows.Forms.Panel();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.lblto = new System.Windows.Forms.Label();
            this.panel17 = new System.Windows.Forms.Panel();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.chkApptDate = new System.Windows.Forms.CheckBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.panel18 = new System.Windows.Forms.Panel();
            this.label63 = new System.Windows.Forms.Label();
            this.ts_PatientDetail = new gloGlobal.gloToolStripIgnoreFocus();
            this.tsb_PD_Insurance = new System.Windows.Forms.ToolStripButton();
            this.tsb_PD_Appointments = new System.Windows.Forms.ToolStripButton();
            this.tsb_PD_Referral = new System.Windows.Forms.ToolStripButton();
            this.tsb_PD_Procedure = new System.Windows.Forms.ToolStripButton();
            this.tsb_PD_PriorAuthorization = new System.Windows.Forms.ToolStripButton();
            this.tsb_PD_Billing = new System.Windows.Forms.ToolStripButton();
            this.tsb_PD_Cases = new System.Windows.Forms.ToolStripButton();
            this.tsb_PatientTasks = new System.Windows.Forms.ToolStripButton();
            this.tsb_PDViewDocument = new System.Windows.Forms.ToolStripButton();
            this.tsb_NYWCForms = new System.Windows.Forms.ToolStripButton();
            this.pnlPatient_Demographics = new Janus.Windows.UI.Dock.UIPanelGroup();
            this.pnlPatient_Demo = new Janus.Windows.UI.Dock.UIPanel();
            this.pnlPatient_DemographicsContainer = new Janus.Windows.UI.Dock.UIPanelInnerContainer();
            this.gb_Demographics = new Janus.Windows.EditControls.UIGroupBox();
            this.panel12 = new System.Windows.Forms.Panel();
            this.pnl_PD_ContactInfo = new System.Windows.Forms.Panel();
            this.pnlBusinessCenter = new System.Windows.Forms.Panel();
            this.lblBusinessCenter = new System.Windows.Forms.Label();
            this.lblBusinessCenterCaption = new System.Windows.Forms.Label();
            this.pnl_WorkPhone = new System.Windows.Forms.Panel();
            this.lblWorkPhone = new System.Windows.Forms.Label();
            this.lblWorkPhoneCaption = new System.Windows.Forms.Label();
            this.pnl_Occupation = new System.Windows.Forms.Panel();
            this.lblOccupation = new System.Windows.Forms.Label();
            this.lblOccupationCaption = new System.Windows.Forms.Label();
            this.pnl_MedCat = new System.Windows.Forms.Panel();
            this.lblPatMedCat = new System.Windows.Forms.Label();
            this.lblMedCatCaption = new System.Windows.Forms.Label();
            this.pnl_PatStatus = new System.Windows.Forms.Panel();
            this.lblPatStatus = new System.Windows.Forms.Label();
            this.lblstCaption = new System.Windows.Forms.Label();
            this.pnl_Language = new System.Windows.Forms.Panel();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.lblLanguageCaption = new System.Windows.Forms.Label();
            this.pnl_Ethinicity = new System.Windows.Forms.Panel();
            this.lblEthinicity = new System.Windows.Forms.Label();
            this.lblEthinicityCaption = new System.Windows.Forms.Label();
            this.pnl_Race = new System.Windows.Forms.Panel();
            this.lblRace = new System.Windows.Forms.Label();
            this.lblRaceCaption = new System.Windows.Forms.Label();
            this.pnl_TertiaryInsurance = new System.Windows.Forms.Panel();
            this.lbl_TertiaryInsurance = new System.Windows.Forms.Label();
            this.Label90 = new System.Windows.Forms.Label();
            this.pnl_SecondaryInsurance = new System.Windows.Forms.Panel();
            this.lblSecondaryInsurance = new System.Windows.Forms.Label();
            this.Label91 = new System.Windows.Forms.Label();
            this.pnl_PrimaryInsurance = new System.Windows.Forms.Panel();
            this.lblPrimaryInsurance = new System.Windows.Forms.Label();
            this.Label92 = new System.Windows.Forms.Label();
            this.pnl_PD_PCPPhone = new System.Windows.Forms.Panel();
            this.lblPD_PCP_Phone = new System.Windows.Forms.Label();
            this.lblPD_PCP_PhoneCaption = new System.Windows.Forms.Label();
            this.pnl_PD_PCPMobile = new System.Windows.Forms.Panel();
            this.lblPD_PCP_Mobile = new System.Windows.Forms.Label();
            this.lblPD_PCP_MobileCaption = new System.Windows.Forms.Label();
            this.pnl_PD_Referral = new System.Windows.Forms.Panel();
            this.lblPD_Referral = new System.Windows.Forms.Label();
            this.lblPD_ReferralCaption = new System.Windows.Forms.Label();
            this.pnl_PD_Physician = new System.Windows.Forms.Panel();
            this.lblPD_Physician = new System.Windows.Forms.Label();
            this.lblPD_PhysicianCaption = new System.Windows.Forms.Label();
            this.pnl_PD_Status = new System.Windows.Forms.Panel();
            this.lbl_PD_Status = new System.Windows.Forms.Label();
            this.lbl_PD_Status1 = new System.Windows.Forms.Label();
            this.pnl_PD_Pharmacy = new System.Windows.Forms.Panel();
            this.lblPD_Pharmacy = new System.Windows.Forms.Label();
            this.lblPD_PharmacyCaption = new System.Windows.Forms.Label();
            this.pnl_PD_Provider = new System.Windows.Forms.Panel();
            this.lblProvider = new System.Windows.Forms.Label();
            this.lblProviderCaption = new System.Windows.Forms.Label();
            this.pnl_EMmobile = new System.Windows.Forms.Panel();
            this.lblEMMobile = new System.Windows.Forms.Label();
            this.lblEMMobileCaption = new System.Windows.Forms.Label();
            this.pnl_EmPhone = new System.Windows.Forms.Panel();
            this.lblEMPhone = new System.Windows.Forms.Label();
            this.lblEMPhoneCaption = new System.Windows.Forms.Label();
            this.pnl_EmContacts = new System.Windows.Forms.Panel();
            this.lblEMContact = new System.Windows.Forms.Label();
            this.lblEMContactCaption = new System.Windows.Forms.Label();
            this.pnl_Email = new System.Windows.Forms.Panel();
            this.lblPD_Email = new System.Windows.Forms.Label();
            this.lblPD_EmailCaption = new System.Windows.Forms.Label();
            this.pnl_Fax = new System.Windows.Forms.Panel();
            this.lblPD_FaxPhone = new System.Windows.Forms.Label();
            this.lblPD_FaxPhoneCaption = new System.Windows.Forms.Label();
            this.pnl_Demo_Mobile = new System.Windows.Forms.Panel();
            this.lblPD_MobilePhone = new System.Windows.Forms.Label();
            this.lblPD_MobilePhoneCaption = new System.Windows.Forms.Label();
            this.pnl_HomePhone = new System.Windows.Forms.Panel();
            this.lblPD_HomePhone = new System.Windows.Forms.Label();
            this.lblPD_HomePhoneCaption = new System.Windows.Forms.Label();
            this.pnl_Demo_Gender = new System.Windows.Forms.Panel();
            this.lblPD_Gender = new System.Windows.Forms.Label();
            this.lblGenderCaption = new System.Windows.Forms.Label();
            this.pnl_Demo_DOB = new System.Windows.Forms.Panel();
            this.lblPD_DateofBirth = new System.Windows.Forms.Label();
            this.lblPD_DOBCaption = new System.Windows.Forms.Label();
            this.pnl_Demo_address = new System.Windows.Forms.Panel();
            this.lblPD_Address = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.pnlBadDebt = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.lblPD_Age = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.lblPD_Name = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.lblPD_Code = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.picPD_Photo = new System.Windows.Forms.PictureBox();
            this.label59 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlCards = new Janus.Windows.UI.Dock.UIPanel();
            this.pnlCardsContainer = new Janus.Windows.UI.Dock.UIPanelInnerContainer();
            this.gb_Cards = new Janus.Windows.EditControls.UIGroupBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.picPC_Cards = new System.Windows.Forms.PictureBox();
            this.label61 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPC_MoveFirst = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnPC_MovePrevious = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.btnPC_MoveNext = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.btnPC_MoveLast = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.btnPC_PrintCards = new System.Windows.Forms.Button();
            this.label60 = new System.Windows.Forms.Label();
            this.btnPC_DeleteCard = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.btnPC_ScanCard = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.panel22 = new System.Windows.Forms.Panel();
            this.lblScannedDate = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.HelpComponent1 = new gloEMR.Help.HelpComponent(this.components);
            this.uiPanel0Container = new Janus.Windows.UI.Dock.UIPanelInnerContainer();
            this.pnlPatient_List = new System.Windows.Forms.Panel();
            this.label38 = new System.Windows.Forms.Label();
            this.gloCntrlPatient = new gloPatient.PatientListControl();
            this.pnlPatient = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.tmr_Dashboard = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.pnl_ts_Main = new System.Windows.Forms.Panel();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.timerLockScreen = new System.Windows.Forms.Timer(this.components);
            this.cmnu_Tasks = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuItem_Priority = new System.Windows.Forms.ToolStripMenuItem();
            this.cmunItem_Completed = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuItem_0Percent = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuItem_25Percent = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuItem_50Percent = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuItem_75Percent = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuItem_100Percent = new System.Windows.Forms.ToolStripMenuItem();
            this.imgList_Priority = new System.Windows.Forms.ImageList(this.components);
            this.cmnu_PatientDetails = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuItem_Authorization = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_RemoveRef = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_Modify = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_eligibilityCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_eligibilityCheckTest = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_copay = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_coverage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_Ledger = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_Payment = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_ViewBenefits = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_ModifyCharges = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_NewWCForm = new System.Windows.Forms.ToolStripMenuItem();
            this.c4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c42ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.c4AUTHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mG2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mG21ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuItem_ModifyWCForm = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnu_PatientList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuPatientItem_CheckIn = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuPatientItem_CheckOut = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuPatientItem_Template = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuPatientItem_PatientAlert = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuPatientItem_SaveAsCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuPatientItem_Cases = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuToolStripCustomize = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CustomizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDoc = new System.Drawing.Printing.PrintDocument();
            this.tmrCopayAlertBlink = new System.Windows.Forms.Timer(this.components);
            this.imgList_ApptPrint = new System.Windows.Forms.ImageList(this.components);
            this.cachedrptSummaryOfVisit1 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit2 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit3 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cm_Task = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmu_NewTask = new System.Windows.Forms.ToolStripMenuItem();
            this.cmu_OpenTask = new System.Windows.Forms.ToolStripMenuItem();
            this.cmu_MarkCompleted = new System.Windows.Forms.ToolStripMenuItem();
            this.cmu_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.cmu_FollowUp = new System.Windows.Forms.ToolStripMenuItem();
            this.cmu_Priority = new System.Windows.Forms.ToolStripMenuItem();
            this.cmu_Complete = new System.Windows.Forms.ToolStripMenuItem();
            this.smn_Zero = new System.Windows.Forms.ToolStripMenuItem();
            this.smn_Quater = new System.Windows.Forms.ToolStripMenuItem();
            this.smn_Half = new System.Windows.Forms.ToolStripMenuItem();
            this.smn_ThreeQuater = new System.Windows.Forms.ToolStripMenuItem();
            this.smn_Full = new System.Windows.Forms.ToolStripMenuItem();
            this.cmu_AcceptTask = new System.Windows.Forms.ToolStripMenuItem();
            this.cmu_DeclineTask = new System.Windows.Forms.ToolStripMenuItem();
            this.C1SuperTooltip1 = new C1.Win.C1SuperTooltip.C1SuperTooltip(this.components);
            this.cachedrptSummaryOfVisit4 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit5 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit6 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit7 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit8 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit9 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit10 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit11 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit12 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit13 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit14 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit15 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.tmrSingleSignOn = new System.Windows.Forms.Timer(this.components);
            this.cachedrptSummaryOfVisit16 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit17 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit18 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.cachedrptSummaryOfVisit19 = new gloPM.Reports.CachedrptSummaryOfVisit();
            this.mnuMainMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.ts_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiPanelManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uipnlPatient_Alert)).BeginInit();
            this.uipnlPatient_Alert.SuspendLayout();
            this.uipnlPatient_AlertContainer.SuspendLayout();
            this.pnlSideButton.SuspendLayout();
            this.pnl_Appointment.SuspendLayout();
            this.pnl_Calendar.SuspendLayout();
            this.pnl_Tasks.SuspendLayout();
            this.pnlPatientAlertMain.SuspendLayout();
            this.panel7.SuspendLayout();
            this.pnlc1PatientAlerts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientAlerts)).BeginInit();
            this.pnl_LeftButtons.SuspendLayout();
            this.pnlLeftPatientAlert.SuspendLayout();
            this.panel5.SuspendLayout();
            this.pnlEligibilityCheck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1EligibilityCheck)).BeginInit();
            this.pnlCopayAlert.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1CopayAlert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPatient_UpComingAppointments)).BeginInit();
            this.pnlPatient_UpComingAppointments.SuspendLayout();
            this.pnlPatient_UpComingAppointmentsContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._picBkg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPatient_Details)).BeginInit();
            this.pnlPatient_Details.SuspendLayout();
            this.pnlPatient_DetailsContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientDetails)).BeginInit();
            this.pnlSearchFilter.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel18.SuspendLayout();
            this.ts_PatientDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPatient_Demographics)).BeginInit();
            this.pnlPatient_Demographics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPatient_Demo)).BeginInit();
            this.pnlPatient_Demo.SuspendLayout();
            this.pnlPatient_DemographicsContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gb_Demographics)).BeginInit();
            this.gb_Demographics.SuspendLayout();
            this.panel12.SuspendLayout();
            this.pnl_PD_ContactInfo.SuspendLayout();
            this.pnlBusinessCenter.SuspendLayout();
            this.pnl_WorkPhone.SuspendLayout();
            this.pnl_Occupation.SuspendLayout();
            this.pnl_MedCat.SuspendLayout();
            this.pnl_PatStatus.SuspendLayout();
            this.pnl_Language.SuspendLayout();
            this.pnl_Ethinicity.SuspendLayout();
            this.pnl_Race.SuspendLayout();
            this.pnl_TertiaryInsurance.SuspendLayout();
            this.pnl_SecondaryInsurance.SuspendLayout();
            this.pnl_PrimaryInsurance.SuspendLayout();
            this.pnl_PD_PCPPhone.SuspendLayout();
            this.pnl_PD_PCPMobile.SuspendLayout();
            this.pnl_PD_Referral.SuspendLayout();
            this.pnl_PD_Physician.SuspendLayout();
            this.pnl_PD_Status.SuspendLayout();
            this.pnl_PD_Pharmacy.SuspendLayout();
            this.pnl_PD_Provider.SuspendLayout();
            this.pnl_EMmobile.SuspendLayout();
            this.pnl_EmPhone.SuspendLayout();
            this.pnl_EmContacts.SuspendLayout();
            this.pnl_Email.SuspendLayout();
            this.pnl_Fax.SuspendLayout();
            this.pnl_Demo_Mobile.SuspendLayout();
            this.pnl_HomePhone.SuspendLayout();
            this.pnl_Demo_Gender.SuspendLayout();
            this.pnl_Demo_DOB.SuspendLayout();
            this.pnl_Demo_address.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel8.SuspendLayout();
            this.pnlBadDebt.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPD_Photo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCards)).BeginInit();
            this.pnlCards.SuspendLayout();
            this.pnlCardsContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gb_Cards)).BeginInit();
            this.gb_Cards.SuspendLayout();
            this.panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPC_Cards)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel22.SuspendLayout();
            this.pnlPatient_List.SuspendLayout();
            this.pnlPatient.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnl_ts_Main.SuspendLayout();
            this.panel4.SuspendLayout();
            this.cmnu_Tasks.SuspendLayout();
            this.cmnu_PatientDetails.SuspendLayout();
            this.cmnu_PatientList.SuspendLayout();
            this.cmnuToolStripCustomize.SuspendLayout();
            this.cm_Task.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMainMenu
            // 
            this.mnuMainMenu.BackColor = System.Drawing.Color.Transparent;
            this.mnuMainMenu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mnuMainMenu.BackgroundImage")));
            this.mnuMainMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mnuMainMenu.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuGo,
            this.munView,
            this.mnuReports,
            this.mnuTools,
            this.mnuSecurity,
            this.mnuSetting,
            this.mnuWindow,
            this.mnuBilling,
            this.mnuHelp});
            this.mnuMainMenu.Location = new System.Drawing.Point(1, 1);
            this.mnuMainMenu.MdiWindowListItem = this.mnuWindow;
            this.mnuMainMenu.Name = "mnuMainMenu";
            this.mnuMainMenu.Padding = new System.Windows.Forms.Padding(1);
            this.mnuMainMenu.Size = new System.Drawing.Size(1241, 24);
            this.mnuMainMenu.TabIndex = 9;
            this.mnuMainMenu.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile_New,
            this.mnuFile_Modify,
            this.mnuFile_Delete,
            this.toolStripSeparator18,
            this.mnuFile_Refresh,
            this.mnuFile_Close,
            this.toolStripSeparator19,
            this.mnuFile_Lock,
            this.toolStripSeparator20,
            this.mnuFile_Exit});
            this.mnuFile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(36, 22);
            this.mnuFile.Text = "&File";
            // 
            // mnuFile_New
            // 
            this.mnuFile_New.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuFile_New.Image = ((System.Drawing.Image)(resources.GetObject("mnuFile_New.Image")));
            this.mnuFile_New.Name = "mnuFile_New";
            this.mnuFile_New.Size = new System.Drawing.Size(145, 22);
            this.mnuFile_New.Text = "New";
            this.mnuFile_New.Visible = false;
            this.mnuFile_New.Click += new System.EventHandler(this.mnuFile_New_Click);
            // 
            // mnuFile_Modify
            // 
            this.mnuFile_Modify.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuFile_Modify.Image = ((System.Drawing.Image)(resources.GetObject("mnuFile_Modify.Image")));
            this.mnuFile_Modify.Name = "mnuFile_Modify";
            this.mnuFile_Modify.Size = new System.Drawing.Size(145, 22);
            this.mnuFile_Modify.Text = "Modify";
            this.mnuFile_Modify.Visible = false;
            this.mnuFile_Modify.Click += new System.EventHandler(this.mnuFile_Modify_Click);
            // 
            // mnuFile_Delete
            // 
            this.mnuFile_Delete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuFile_Delete.Image = ((System.Drawing.Image)(resources.GetObject("mnuFile_Delete.Image")));
            this.mnuFile_Delete.Name = "mnuFile_Delete";
            this.mnuFile_Delete.Size = new System.Drawing.Size(145, 22);
            this.mnuFile_Delete.Text = "Delete";
            this.mnuFile_Delete.Visible = false;
            this.mnuFile_Delete.Click += new System.EventHandler(this.mnuFile_Delete_Click);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(142, 6);
            this.toolStripSeparator18.Visible = false;
            // 
            // mnuFile_Refresh
            // 
            this.mnuFile_Refresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuFile_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("mnuFile_Refresh.Image")));
            this.mnuFile_Refresh.Name = "mnuFile_Refresh";
            this.mnuFile_Refresh.Size = new System.Drawing.Size(145, 22);
            this.mnuFile_Refresh.Text = "Refresh";
            this.mnuFile_Refresh.Visible = false;
            this.mnuFile_Refresh.Click += new System.EventHandler(this.mnuFile_Refresh_Click);
            // 
            // mnuFile_Close
            // 
            this.mnuFile_Close.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuFile_Close.Image = ((System.Drawing.Image)(resources.GetObject("mnuFile_Close.Image")));
            this.mnuFile_Close.Name = "mnuFile_Close";
            this.mnuFile_Close.Size = new System.Drawing.Size(145, 22);
            this.mnuFile_Close.Text = "Close";
            this.mnuFile_Close.Visible = false;
            this.mnuFile_Close.Click += new System.EventHandler(this.mnuFile_Close_Click);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(142, 6);
            this.toolStripSeparator19.Visible = false;
            // 
            // mnuFile_Lock
            // 
            this.mnuFile_Lock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuFile_Lock.Image = ((System.Drawing.Image)(resources.GetObject("mnuFile_Lock.Image")));
            this.mnuFile_Lock.Name = "mnuFile_Lock";
            this.mnuFile_Lock.Size = new System.Drawing.Size(145, 22);
            this.mnuFile_Lock.Text = "Lock Screen ";
            this.mnuFile_Lock.Click += new System.EventHandler(this.mnuFile_Lock_Click);
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size(142, 6);
            // 
            // mnuFile_Exit
            // 
            this.mnuFile_Exit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuFile_Exit.Image = ((System.Drawing.Image)(resources.GetObject("mnuFile_Exit.Image")));
            this.mnuFile_Exit.Name = "mnuFile_Exit";
            this.mnuFile_Exit.Size = new System.Drawing.Size(145, 22);
            this.mnuFile_Exit.Text = "Exit";
            this.mnuFile_Exit.Click += new System.EventHandler(this.mnuFile_Exit_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEdit_Contacts,
            this.toolStripSeparator4,
            this.mnuEdit_AppointmentBook,
            this.mnuEdit_BillingBook,
            this.mnuEdit_TaskMailBook,
            this.mnuView_TemplateGallary,
            this.mnuEdit_TemplateAssociation,
            this.mnuEdit_RCMCagetory,
            this.mnuEdit_ICD9CPTGallery});
            this.mnuEdit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(40, 22);
            this.mnuEdit.Text = "&Edit";
            // 
            // mnuEdit_Contacts
            // 
            this.mnuEdit_Contacts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuEdit_Contacts.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_Contacts.Image")));
            this.mnuEdit_Contacts.Name = "mnuEdit_Contacts";
            this.mnuEdit_Contacts.Size = new System.Drawing.Size(222, 22);
            this.mnuEdit_Contacts.Text = "Contacts";
            this.mnuEdit_Contacts.Click += new System.EventHandler(this.mnuEdit_Contacts_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(219, 6);
            // 
            // mnuEdit_AppointmentBook
            // 
            this.mnuEdit_AppointmentBook.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuEdit_AppointmentBook.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_AppointmentBook.Image")));
            this.mnuEdit_AppointmentBook.Name = "mnuEdit_AppointmentBook";
            this.mnuEdit_AppointmentBook.Size = new System.Drawing.Size(222, 22);
            this.mnuEdit_AppointmentBook.Text = "Appointment Configuration";
            this.mnuEdit_AppointmentBook.Click += new System.EventHandler(this.mnuEdit_AppointmentBook_Click);
            // 
            // mnuEdit_BillingBook
            // 
            this.mnuEdit_BillingBook.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuEdit_BillingBook.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_BillingBook.Image")));
            this.mnuEdit_BillingBook.Name = "mnuEdit_BillingBook";
            this.mnuEdit_BillingBook.Size = new System.Drawing.Size(222, 22);
            this.mnuEdit_BillingBook.Text = "Billing Configuration";
            this.mnuEdit_BillingBook.Click += new System.EventHandler(this.mnuEdit_BillingBook_Click);
            // 
            // mnuEdit_TaskMailBook
            // 
            this.mnuEdit_TaskMailBook.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuEdit_TaskMailBook.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_TaskMailBook.Image")));
            this.mnuEdit_TaskMailBook.Name = "mnuEdit_TaskMailBook";
            this.mnuEdit_TaskMailBook.Size = new System.Drawing.Size(222, 22);
            this.mnuEdit_TaskMailBook.Text = "Task/Mail Configuration";
            this.mnuEdit_TaskMailBook.Click += new System.EventHandler(this.mnuEdit_TaskMailBook_Click);
            // 
            // mnuView_TemplateGallary
            // 
            this.mnuView_TemplateGallary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuView_TemplateGallary.Image = ((System.Drawing.Image)(resources.GetObject("mnuView_TemplateGallary.Image")));
            this.mnuView_TemplateGallary.Name = "mnuView_TemplateGallary";
            this.mnuView_TemplateGallary.Size = new System.Drawing.Size(222, 22);
            this.mnuView_TemplateGallary.Text = "Template Gallery";
            this.mnuView_TemplateGallary.Click += new System.EventHandler(this.mnuView_TemplateGallary_Click);
            // 
            // mnuEdit_TemplateAssociation
            // 
            this.mnuEdit_TemplateAssociation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuEdit_TemplateAssociation.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_TemplateAssociation.Image")));
            this.mnuEdit_TemplateAssociation.Name = "mnuEdit_TemplateAssociation";
            this.mnuEdit_TemplateAssociation.Size = new System.Drawing.Size(222, 22);
            this.mnuEdit_TemplateAssociation.Text = "Template Association";
            this.mnuEdit_TemplateAssociation.Click += new System.EventHandler(this.mnuEdit_TemplateAssociation_Click);
            // 
            // mnuEdit_RCMCagetory
            // 
            this.mnuEdit_RCMCagetory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuEdit_RCMCagetory.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_RCMCagetory.Image")));
            this.mnuEdit_RCMCagetory.Name = "mnuEdit_RCMCagetory";
            this.mnuEdit_RCMCagetory.Size = new System.Drawing.Size(222, 22);
            this.mnuEdit_RCMCagetory.Text = "RCM Category";
            this.mnuEdit_RCMCagetory.Click += new System.EventHandler(this.mnuEdit_RCMCagetory_Click);
            // 
            // mnuEdit_ICD9CPTGallery
            // 
            this.mnuEdit_ICD9CPTGallery.BackColor = System.Drawing.SystemColors.Control;
            this.mnuEdit_ICD9CPTGallery.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMaster_ICD9Gallery,
            this.mnuMaster_ICD10Gallery,
            this.mnuMaster_CPTGallery});
            this.mnuEdit_ICD9CPTGallery.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuEdit_ICD9CPTGallery.Image = ((System.Drawing.Image)(resources.GetObject("mnuEdit_ICD9CPTGallery.Image")));
            this.mnuEdit_ICD9CPTGallery.Name = "mnuEdit_ICD9CPTGallery";
            this.mnuEdit_ICD9CPTGallery.Size = new System.Drawing.Size(222, 22);
            this.mnuEdit_ICD9CPTGallery.Text = "Code Gallery";
            this.mnuEdit_ICD9CPTGallery.Click += new System.EventHandler(this.mnuEdit_ICD9CPTGallery_Click);
            // 
            // mnuMaster_ICD9Gallery
            // 
            this.mnuMaster_ICD9Gallery.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuMaster_ICD9Gallery.Image = ((System.Drawing.Image)(resources.GetObject("mnuMaster_ICD9Gallery.Image")));
            this.mnuMaster_ICD9Gallery.Name = "mnuMaster_ICD9Gallery";
            this.mnuMaster_ICD9Gallery.Size = new System.Drawing.Size(146, 22);
            this.mnuMaster_ICD9Gallery.Text = "ICD9 Gallery";
            this.mnuMaster_ICD9Gallery.Click += new System.EventHandler(this.mnuMaster_ICD9Gallery_Click);
            // 
            // mnuMaster_ICD10Gallery
            // 
            this.mnuMaster_ICD10Gallery.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuMaster_ICD10Gallery.Image = ((System.Drawing.Image)(resources.GetObject("mnuMaster_ICD10Gallery.Image")));
            this.mnuMaster_ICD10Gallery.Name = "mnuMaster_ICD10Gallery";
            this.mnuMaster_ICD10Gallery.Size = new System.Drawing.Size(146, 22);
            this.mnuMaster_ICD10Gallery.Text = "ICD10 Gallery";
            this.mnuMaster_ICD10Gallery.Click += new System.EventHandler(this.mnuMaster_ICD10Gallery_Click);
            // 
            // mnuMaster_CPTGallery
            // 
            this.mnuMaster_CPTGallery.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuMaster_CPTGallery.Image = ((System.Drawing.Image)(resources.GetObject("mnuMaster_CPTGallery.Image")));
            this.mnuMaster_CPTGallery.Name = "mnuMaster_CPTGallery";
            this.mnuMaster_CPTGallery.Size = new System.Drawing.Size(146, 22);
            this.mnuMaster_CPTGallery.Text = "CPT Gallery";
            this.mnuMaster_CPTGallery.Click += new System.EventHandler(this.mnuMaster_CPTGallery_Click);
            // 
            // mnuGo
            // 
            this.mnuGo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGo_NewPatient,
            this.mnuGo_ModifyPatient,
            this.mnuGo_CardScanning,
            this.mnuGo_ScanDocument,
            this.toolStripSeparator11,
            this.mnuGo_Appointment,
            this.mnuGo_Schedule,
            this.toolStripSeparator12,
            this.mnuGo_Billing,
            this.mnuGo_BatchProcessing,
            this.mnuGo_ClaimProcessing,
            this.mnuGo_PaymentPatient,
            this.mnuGo_PaymentInsurace,
            this.mnuGo_ERAPayment,
            this.mnuGo_Payment,
            this.mnuGo_DailyClose,
            this.toolStripSeparator15,
            this.mnuGo_ClosedJournals,
            this.mnuGo_ChargesTray,
            this.mnuGo_PatientStatementNotes,
            this.mnuGo_PatientLedger,
            this.mnuGo_Remittance,
            this.mnuGo_EOBLedger,
            this.mnuGo_PatientStatment,
            this.mnuGo_RevenueCycle,
            this.mnuGo_CopayDistributionList,
            this.mnuGo_ReservesDistributionList,
            this.mnuGo_CollectionAgencyRefund});
            this.mnuGo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuGo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo.Name = "mnuGo";
            this.mnuGo.Size = new System.Drawing.Size(34, 22);
            this.mnuGo.Text = "&Go";
            // 
            // mnuGo_NewPatient
            // 
            this.mnuGo_NewPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_NewPatient.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_NewPatient.Image")));
            this.mnuGo_NewPatient.Name = "mnuGo_NewPatient";
            this.mnuGo_NewPatient.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_NewPatient.Text = "New Patient";
            this.mnuGo_NewPatient.Click += new System.EventHandler(this.mnuGo_NewPatient_Click);
            // 
            // mnuGo_ModifyPatient
            // 
            this.mnuGo_ModifyPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_ModifyPatient.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_ModifyPatient.Image")));
            this.mnuGo_ModifyPatient.Name = "mnuGo_ModifyPatient";
            this.mnuGo_ModifyPatient.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_ModifyPatient.Text = "Modify Patient";
            this.mnuGo_ModifyPatient.Click += new System.EventHandler(this.mnuGo_ModifyPatient_Click);
            // 
            // mnuGo_CardScanning
            // 
            this.mnuGo_CardScanning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_CardScanning.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_CardScanning.Image")));
            this.mnuGo_CardScanning.Name = "mnuGo_CardScanning";
            this.mnuGo_CardScanning.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_CardScanning.Text = "Card Scanning";
            this.mnuGo_CardScanning.Visible = false;
            this.mnuGo_CardScanning.Click += new System.EventHandler(this.mnuGo_CardScanning_Click);
            // 
            // mnuGo_ScanDocument
            // 
            this.mnuGo_ScanDocument.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_ScanDocument.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_ScanDocument.Image")));
            this.mnuGo_ScanDocument.Name = "mnuGo_ScanDocument";
            this.mnuGo_ScanDocument.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_ScanDocument.Text = "Scan Documents";
            this.mnuGo_ScanDocument.Click += new System.EventHandler(this.mnuGo_ScanDocument_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(211, 6);
            // 
            // mnuGo_Appointment
            // 
            this.mnuGo_Appointment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_Appointment.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_Appointment.Image")));
            this.mnuGo_Appointment.Name = "mnuGo_Appointment";
            this.mnuGo_Appointment.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_Appointment.Text = "Appointment";
            this.mnuGo_Appointment.Click += new System.EventHandler(this.mnuGo_Appointment_Click);
            // 
            // mnuGo_Schedule
            // 
            this.mnuGo_Schedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_Schedule.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_Schedule.Image")));
            this.mnuGo_Schedule.Name = "mnuGo_Schedule";
            this.mnuGo_Schedule.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_Schedule.Text = "Schedule";
            this.mnuGo_Schedule.Click += new System.EventHandler(this.mnuGo_Schedule_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(211, 6);
            // 
            // mnuGo_Billing
            // 
            this.mnuGo_Billing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_Billing.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_Billing.Image")));
            this.mnuGo_Billing.Name = "mnuGo_Billing";
            this.mnuGo_Billing.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_Billing.Text = "Charges";
            this.mnuGo_Billing.Click += new System.EventHandler(this.mnuGo_Billing_Click);
            // 
            // mnuGo_BatchProcessing
            // 
            this.mnuGo_BatchProcessing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_BatchProcessing.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_BatchProcessing.Image")));
            this.mnuGo_BatchProcessing.Name = "mnuGo_BatchProcessing";
            this.mnuGo_BatchProcessing.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_BatchProcessing.Text = "Batch";
            this.mnuGo_BatchProcessing.Click += new System.EventHandler(this.mnuGo_BatchProcessing_Click);
            // 
            // mnuGo_ClaimProcessing
            // 
            this.mnuGo_ClaimProcessing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_ClaimProcessing.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_ClaimProcessing.Image")));
            this.mnuGo_ClaimProcessing.Name = "mnuGo_ClaimProcessing";
            this.mnuGo_ClaimProcessing.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_ClaimProcessing.Text = "Claim";
            this.mnuGo_ClaimProcessing.Visible = false;
            this.mnuGo_ClaimProcessing.Click += new System.EventHandler(this.mnuGo_ClaimProcessing_Click);
            // 
            // mnuGo_PaymentPatient
            // 
            this.mnuGo_PaymentPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_PaymentPatient.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_PaymentPatient.Image")));
            this.mnuGo_PaymentPatient.Name = "mnuGo_PaymentPatient";
            this.mnuGo_PaymentPatient.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_PaymentPatient.Text = "Patient Payment";
            this.mnuGo_PaymentPatient.Click += new System.EventHandler(this.mnuGo_PaymentPatient_Click);
            // 
            // mnuGo_PaymentInsurace
            // 
            this.mnuGo_PaymentInsurace.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_PaymentInsurace.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_PaymentInsurace.Image")));
            this.mnuGo_PaymentInsurace.Name = "mnuGo_PaymentInsurace";
            this.mnuGo_PaymentInsurace.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_PaymentInsurace.Text = "Insurance Payment";
            this.mnuGo_PaymentInsurace.Click += new System.EventHandler(this.mnuGo_PaymentInsurace_Click);
            // 
            // mnuGo_ERAPayment
            // 
            this.mnuGo_ERAPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_ERAPayment.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_ERAPayment.Image")));
            this.mnuGo_ERAPayment.Name = "mnuGo_ERAPayment";
            this.mnuGo_ERAPayment.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_ERAPayment.Text = "ERA Payment";
            this.mnuGo_ERAPayment.Click += new System.EventHandler(this.mnuGo_ERAPayment_Click);
            // 
            // mnuGo_Payment
            // 
            this.mnuGo_Payment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_Payment.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_Payment.Image")));
            this.mnuGo_Payment.Name = "mnuGo_Payment";
            this.mnuGo_Payment.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_Payment.Text = "Payment";
            this.mnuGo_Payment.Visible = false;
            this.mnuGo_Payment.Click += new System.EventHandler(this.mnuGo_Payment_Click);
            // 
            // mnuGo_DailyClose
            // 
            this.mnuGo_DailyClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_DailyClose.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_DailyClose.Image")));
            this.mnuGo_DailyClose.Name = "mnuGo_DailyClose";
            this.mnuGo_DailyClose.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_DailyClose.Text = "Daily Close";
            this.mnuGo_DailyClose.Click += new System.EventHandler(this.mnu_rpt_ChargePaymentSummaryReport_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(211, 6);
            // 
            // mnuGo_ClosedJournals
            // 
            this.mnuGo_ClosedJournals.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_ClosedJournals.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_ClosedJournals.Image")));
            this.mnuGo_ClosedJournals.Name = "mnuGo_ClosedJournals";
            this.mnuGo_ClosedJournals.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_ClosedJournals.Text = "Payment Tray";
            this.mnuGo_ClosedJournals.Visible = false;
            this.mnuGo_ClosedJournals.Click += new System.EventHandler(this.mnuGo_ClosedJournals_Click);
            // 
            // mnuGo_ChargesTray
            // 
            this.mnuGo_ChargesTray.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_ChargesTray.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_ChargesTray.Image")));
            this.mnuGo_ChargesTray.Name = "mnuGo_ChargesTray";
            this.mnuGo_ChargesTray.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_ChargesTray.Text = "Charge Tray";
            this.mnuGo_ChargesTray.Visible = false;
            this.mnuGo_ChargesTray.Click += new System.EventHandler(this.mnuGo_ChargesTray_Click);
            // 
            // mnuGo_PatientStatementNotes
            // 
            this.mnuGo_PatientStatementNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_PatientStatementNotes.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_PatientStatementNotes.Image")));
            this.mnuGo_PatientStatementNotes.Name = "mnuGo_PatientStatementNotes";
            this.mnuGo_PatientStatementNotes.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_PatientStatementNotes.Text = "Patient Statement Notes";
            this.mnuGo_PatientStatementNotes.Visible = false;
            this.mnuGo_PatientStatementNotes.Click += new System.EventHandler(this.mnuGo_PatientStatementNotes_Click);
            // 
            // mnuGo_PatientLedger
            // 
            this.mnuGo_PatientLedger.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_PatientLedger.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_PatientLedger.Image")));
            this.mnuGo_PatientLedger.Name = "mnuGo_PatientLedger";
            this.mnuGo_PatientLedger.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_PatientLedger.Text = "Patient Account";
            this.mnuGo_PatientLedger.Click += new System.EventHandler(this.mnuGo_PatientLedger_Click);
            // 
            // mnuGo_Remittance
            // 
            this.mnuGo_Remittance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_Remittance.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_Remittance.Image")));
            this.mnuGo_Remittance.Name = "mnuGo_Remittance";
            this.mnuGo_Remittance.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_Remittance.Text = "Remittance";
            this.mnuGo_Remittance.Visible = false;
            this.mnuGo_Remittance.Click += new System.EventHandler(this.mnuGo_Remittance_Click);
            // 
            // mnuGo_EOBLedger
            // 
            this.mnuGo_EOBLedger.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_EOBLedger.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_EOBLedger.Image")));
            this.mnuGo_EOBLedger.Name = "mnuGo_EOBLedger";
            this.mnuGo_EOBLedger.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_EOBLedger.Text = "Ledger";
            this.mnuGo_EOBLedger.Visible = false;
            this.mnuGo_EOBLedger.Click += new System.EventHandler(this.mnuGo_EOBLedger_Click);
            // 
            // mnuGo_PatientStatment
            // 
            this.mnuGo_PatientStatment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_PatientStatment.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_PatientStatment.Image")));
            this.mnuGo_PatientStatment.Name = "mnuGo_PatientStatment";
            this.mnuGo_PatientStatment.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_PatientStatment.Text = "Patient Statement";
            this.mnuGo_PatientStatment.Click += new System.EventHandler(this.mnuGo_PatientStatment_Click);
            // 
            // mnuGo_RevenueCycle
            // 
            this.mnuGo_RevenueCycle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_RevenueCycle.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_RevenueCycle.Image")));
            this.mnuGo_RevenueCycle.Name = "mnuGo_RevenueCycle";
            this.mnuGo_RevenueCycle.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_RevenueCycle.Text = "Revenue Cycle";
            this.mnuGo_RevenueCycle.Click += new System.EventHandler(this.mnuGo_RevenueCycle_Click);
            // 
            // mnuGo_CopayDistributionList
            // 
            this.mnuGo_CopayDistributionList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCopayDist_ByAccount,
            this.mnuCopayDist_ByCharge});
            this.mnuGo_CopayDistributionList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_CopayDistributionList.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_CopayDistributionList.Image")));
            this.mnuGo_CopayDistributionList.Name = "mnuGo_CopayDistributionList";
            this.mnuGo_CopayDistributionList.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_CopayDistributionList.Text = "Copay Distribution List";
            this.mnuGo_CopayDistributionList.Click += new System.EventHandler(this.mnuGo_CopayDistributionList_Click);
            // 
            // mnuCopayDist_ByAccount
            // 
            this.mnuCopayDist_ByAccount.Image = ((System.Drawing.Image)(resources.GetObject("mnuCopayDist_ByAccount.Image")));
            this.mnuCopayDist_ByAccount.Name = "mnuCopayDist_ByAccount";
            this.mnuCopayDist_ByAccount.Size = new System.Drawing.Size(137, 22);
            this.mnuCopayDist_ByAccount.Text = "By Account";
            this.mnuCopayDist_ByAccount.Click += new System.EventHandler(this.mnuCopayDist_ByAccount_Click);
            // 
            // mnuCopayDist_ByCharge
            // 
            this.mnuCopayDist_ByCharge.Image = ((System.Drawing.Image)(resources.GetObject("mnuCopayDist_ByCharge.Image")));
            this.mnuCopayDist_ByCharge.Name = "mnuCopayDist_ByCharge";
            this.mnuCopayDist_ByCharge.Size = new System.Drawing.Size(137, 22);
            this.mnuCopayDist_ByCharge.Tag = "ByCharge";
            this.mnuCopayDist_ByCharge.Text = "By Charge";
            this.mnuCopayDist_ByCharge.Click += new System.EventHandler(this.mnuCopayDist_ByCharge_Click);
            // 
            // mnuGo_ReservesDistributionList
            // 
            this.mnuGo_ReservesDistributionList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_ReservesDistributionList.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_ReservesDistributionList.Image")));
            this.mnuGo_ReservesDistributionList.Name = "mnuGo_ReservesDistributionList";
            this.mnuGo_ReservesDistributionList.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_ReservesDistributionList.Tag = "ReservesDistributionList";
            this.mnuGo_ReservesDistributionList.Text = "Reserves Distribution List";
            this.mnuGo_ReservesDistributionList.Click += new System.EventHandler(this.mnuGo_ReservesDistributionList_Click);
            // 
            // mnuGo_CollectionAgencyRefund
            // 
            this.mnuGo_CollectionAgencyRefund.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuGo_CollectionAgencyRefund.Image = ((System.Drawing.Image)(resources.GetObject("mnuGo_CollectionAgencyRefund.Image")));
            this.mnuGo_CollectionAgencyRefund.Name = "mnuGo_CollectionAgencyRefund";
            this.mnuGo_CollectionAgencyRefund.Size = new System.Drawing.Size(214, 22);
            this.mnuGo_CollectionAgencyRefund.Tag = "CollectionAgencyRefund";
            this.mnuGo_CollectionAgencyRefund.Text = "Collection Agency Refund";
            this.mnuGo_CollectionAgencyRefund.Click += new System.EventHandler(this.mnuGo_CollectionAgencyRefund_Click);
            // 
            // munView
            // 
            this.munView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuView_Appointment,
            this.mnuView_Schedule,
            this.mnuView_Reminders,
            this.toolStripSeparator6,
            this.mnuView_Billing,
            this.mnuView_BatchProcessing,
            this.mnuView_ClaimProcessing,
            this.mnuView_Payment,
            this.mnuView_PatientTemplates,
            this.mnuView_PatientBatchStatement,
            this.mnuView_Tasks,
            this.messageQueueToolStripMenuItem,
            this.batchEligibilityActivityToolStripMenuItem,
            this.mnuBatch_Eligibility,
            this.mnuView_Documents,
            this.mnuView_CleargageFileHistory});
            this.munView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.munView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.munView.Name = "munView";
            this.munView.Size = new System.Drawing.Size(46, 22);
            this.munView.Text = "&View";
            // 
            // mnuView_Appointment
            // 
            this.mnuView_Appointment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuView_Appointment.Image = ((System.Drawing.Image)(resources.GetObject("mnuView_Appointment.Image")));
            this.mnuView_Appointment.Name = "mnuView_Appointment";
            this.mnuView_Appointment.Size = new System.Drawing.Size(209, 22);
            this.mnuView_Appointment.Text = "Calendar";
            this.mnuView_Appointment.Visible = false;
            this.mnuView_Appointment.Click += new System.EventHandler(this.mnuView_Appointment_Click);
            // 
            // mnuView_Schedule
            // 
            this.mnuView_Schedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuView_Schedule.Image = ((System.Drawing.Image)(resources.GetObject("mnuView_Schedule.Image")));
            this.mnuView_Schedule.Name = "mnuView_Schedule";
            this.mnuView_Schedule.Size = new System.Drawing.Size(209, 22);
            this.mnuView_Schedule.Text = "Schedule";
            this.mnuView_Schedule.Click += new System.EventHandler(this.mnuView_Schedule_Click);
            // 
            // mnuView_Reminders
            // 
            this.mnuView_Reminders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuView_Reminders.Image = ((System.Drawing.Image)(resources.GetObject("mnuView_Reminders.Image")));
            this.mnuView_Reminders.Name = "mnuView_Reminders";
            this.mnuView_Reminders.Size = new System.Drawing.Size(209, 22);
            this.mnuView_Reminders.Text = "Reminders";
            this.mnuView_Reminders.Visible = false;
            this.mnuView_Reminders.Click += new System.EventHandler(this.mnuView_Reminders_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(206, 6);
            // 
            // mnuView_Billing
            // 
            this.mnuView_Billing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuView_Billing.Image = ((System.Drawing.Image)(resources.GetObject("mnuView_Billing.Image")));
            this.mnuView_Billing.Name = "mnuView_Billing";
            this.mnuView_Billing.Size = new System.Drawing.Size(209, 22);
            this.mnuView_Billing.Text = "Batch";
            this.mnuView_Billing.Visible = false;
            this.mnuView_Billing.Click += new System.EventHandler(this.mnuView_Billing_Click);
            // 
            // mnuView_BatchProcessing
            // 
            this.mnuView_BatchProcessing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuView_BatchProcessing.Image = ((System.Drawing.Image)(resources.GetObject("mnuView_BatchProcessing.Image")));
            this.mnuView_BatchProcessing.Name = "mnuView_BatchProcessing";
            this.mnuView_BatchProcessing.Size = new System.Drawing.Size(209, 22);
            this.mnuView_BatchProcessing.Text = "Batch Processing";
            this.mnuView_BatchProcessing.Visible = false;
            this.mnuView_BatchProcessing.Click += new System.EventHandler(this.mnuView_BatchProcessing_Click);
            // 
            // mnuView_ClaimProcessing
            // 
            this.mnuView_ClaimProcessing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuView_ClaimProcessing.Image = ((System.Drawing.Image)(resources.GetObject("mnuView_ClaimProcessing.Image")));
            this.mnuView_ClaimProcessing.Name = "mnuView_ClaimProcessing";
            this.mnuView_ClaimProcessing.Size = new System.Drawing.Size(209, 22);
            this.mnuView_ClaimProcessing.Text = "Claim Processing";
            this.mnuView_ClaimProcessing.Visible = false;
            this.mnuView_ClaimProcessing.Click += new System.EventHandler(this.mnuView_ClaimProcessing_Click);
            // 
            // mnuView_Payment
            // 
            this.mnuView_Payment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuView_Payment.Image = ((System.Drawing.Image)(resources.GetObject("mnuView_Payment.Image")));
            this.mnuView_Payment.Name = "mnuView_Payment";
            this.mnuView_Payment.Size = new System.Drawing.Size(209, 22);
            this.mnuView_Payment.Text = "Payment";
            this.mnuView_Payment.Visible = false;
            this.mnuView_Payment.Click += new System.EventHandler(this.mnuView_Payment_Click);
            // 
            // mnuView_PatientTemplates
            // 
            this.mnuView_PatientTemplates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuView_PatientTemplates.Image = ((System.Drawing.Image)(resources.GetObject("mnuView_PatientTemplates.Image")));
            this.mnuView_PatientTemplates.Name = "mnuView_PatientTemplates";
            this.mnuView_PatientTemplates.Size = new System.Drawing.Size(209, 22);
            this.mnuView_PatientTemplates.Text = "Patient Forms";
            this.mnuView_PatientTemplates.Click += new System.EventHandler(this.mnuView_PatientTemplates_Click);
            // 
            // mnuView_PatientBatchStatement
            // 
            this.mnuView_PatientBatchStatement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuView_PatientBatchStatement.Image = ((System.Drawing.Image)(resources.GetObject("mnuView_PatientBatchStatement.Image")));
            this.mnuView_PatientBatchStatement.Name = "mnuView_PatientBatchStatement";
            this.mnuView_PatientBatchStatement.Size = new System.Drawing.Size(209, 22);
            this.mnuView_PatientBatchStatement.Text = "Statement Batch History";
            this.mnuView_PatientBatchStatement.Click += new System.EventHandler(this.mnuView_PatientBatchStatement_Click);
            // 
            // mnuView_Tasks
            // 
            this.mnuView_Tasks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuView_Tasks.Image = ((System.Drawing.Image)(resources.GetObject("mnuView_Tasks.Image")));
            this.mnuView_Tasks.Name = "mnuView_Tasks";
            this.mnuView_Tasks.Size = new System.Drawing.Size(209, 22);
            this.mnuView_Tasks.Text = "Tasks";
            this.mnuView_Tasks.Click += new System.EventHandler(this.mnuView_Tasks_Click);
            // 
            // messageQueueToolStripMenuItem
            // 
            this.messageQueueToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.messageQueueToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("messageQueueToolStripMenuItem.Image")));
            this.messageQueueToolStripMenuItem.Name = "messageQueueToolStripMenuItem";
            this.messageQueueToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.messageQueueToolStripMenuItem.Text = "Message Queue";
            this.messageQueueToolStripMenuItem.Click += new System.EventHandler(this.messageQueueToolStripMenuItem_Click);
            // 
            // batchEligibilityActivityToolStripMenuItem
            // 
            this.batchEligibilityActivityToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.batchEligibilityActivityToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("batchEligibilityActivityToolStripMenuItem.Image")));
            this.batchEligibilityActivityToolStripMenuItem.Name = "batchEligibilityActivityToolStripMenuItem";
            this.batchEligibilityActivityToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.batchEligibilityActivityToolStripMenuItem.Text = "Batch Eligibility Activity";
            this.batchEligibilityActivityToolStripMenuItem.Click += new System.EventHandler(this.batchEligibilityActivityToolStripMenuItem_Click);
            // 
            // mnuBatch_Eligibility
            // 
            this.mnuBatch_Eligibility.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuBatch_Eligibility.Image = ((System.Drawing.Image)(resources.GetObject("mnuBatch_Eligibility.Image")));
            this.mnuBatch_Eligibility.Name = "mnuBatch_Eligibility";
            this.mnuBatch_Eligibility.Size = new System.Drawing.Size(209, 22);
            this.mnuBatch_Eligibility.Text = "Batch Eligibility";
            this.mnuBatch_Eligibility.Click += new System.EventHandler(this.mnuBatch_Eligibility_Click);
            // 
            // mnuView_Documents
            // 
            this.mnuView_Documents.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuView_Documents.Image = ((System.Drawing.Image)(resources.GetObject("mnuView_Documents.Image")));
            this.mnuView_Documents.Name = "mnuView_Documents";
            this.mnuView_Documents.Size = new System.Drawing.Size(209, 22);
            this.mnuView_Documents.Text = "Documents";
            this.mnuView_Documents.Click += new System.EventHandler(this.mnuView_Documents_Click);
            // 
            // mnuView_CleargageFileHistory
            // 
            this.mnuView_CleargageFileHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuView_CleargageFileHistory.Image = ((System.Drawing.Image)(resources.GetObject("mnuView_CleargageFileHistory.Image")));
            this.mnuView_CleargageFileHistory.Name = "mnuView_CleargageFileHistory";
            this.mnuView_CleargageFileHistory.Size = new System.Drawing.Size(209, 22);
            this.mnuView_CleargageFileHistory.Text = "Cleargage File History";
            this.mnuView_CleargageFileHistory.Click += new System.EventHandler(this.mnuView_CleargageFileHistory_Click);
            // 
            // mnuReports
            // 
            this.mnuReports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuReports_Reports,
            this.toolStripSeparator25,
            this.mnuReports_ReportingTools,
            this.mnuReports_PracticeAnalysis,
            this.toolStripSeparator3,
            this.mnu_MISReports,
            this.toolStripSeparator13,
            this.mnu_rpt_PaymentTrayReport,
            this.mnu_rpt_Appointments,
            this.mnu_rpt_ConfirmAppointments,
            this.mnu_rpt_NoShowAppointments,
            this.mnu_rpt_MissedOpportunitiesReport,
            this.mnu_rpt_AppointmentCensusReport,
            this.mnu_rpt_PatientBenefitsInfo,
            this.mnu_rpt_AppointmentWaiting,
            this.mnuRpt_BatchPrint,
            this.mnuRpt_VoidClaims,
            this.mnu_MissingChargesReport,
            this.mnuReportsPendingCopayReport,
            this.mnu_MonthEndReport,
            this.mnuReports_OvreduePatientPayment,
            this.mnuReports_OverdueInsurancePayment,
            this.mnuReports_PatientVsEstablishedReport,
            this.mnuReports_DailyCollectionReport,
            this.mnuReportsRefund,
            this.mnuReports_ZeroBalancePatient,
            this.mnuReportsTransactionHistory,
            this.mnu_InsuranceCompanySetup,
            this.mnuPriorAuthReport,
            this.mnuTransactionNotes,
            this.mnuTransactionHistoryAnalysis,
            this.mnu_patientReport,
            this.mnuReports_ProviderReferral_Patients,
            this.mnu_Reports_DplicateCliam,
            this.mnu_ChargesPayments,
            this.mnu_PatientRecall,
            this.mnuReports_PrintLabels,
            this.mnuReports_PrintList,
            this.mnuReports_Graphs,
            this.mnuReports_ClaimStatus,
            this.mnu_Reports_PatientByDOBReport,
            this.mnu_Reports_PatientExcludeSt,
            this.mnu_Reports_BatchReport,
            this.payerListToolStripMenuItem,
            this.mnuReports_gatewayEDI,
            this.mnuReports_AuditTrail,
            this.mnuInterfaceReports,
            this.mnuPatientActivationtReport,
            this.mnuReimbursementWarning,
            this.mnuBusCenterMismatch,
            this.mnuChargeLagReport,
            this.mnuBatchLagReport,
            this.mnuReports_InactiveCPTSReport,
            this.mnuReports_ChargeEditReport,
            this.mnuReports_CollectionExport});
            this.mnuReports.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuReports.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports.Name = "mnuReports";
            this.mnuReports.Size = new System.Drawing.Size(61, 22);
            this.mnuReports.Text = "&Reports";
            // 
            // mnuReports_Reports
            // 
            this.mnuReports_Reports.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_Reports.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_Reports.Image")));
            this.mnuReports_Reports.Name = "mnuReports_Reports";
            this.mnuReports_Reports.Size = new System.Drawing.Size(266, 22);
            this.mnuReports_Reports.Text = "Reports";
            this.mnuReports_Reports.Visible = false;
            this.mnuReports_Reports.Click += new System.EventHandler(this.mnuReports_Reports_Click);
            // 
            // toolStripSeparator25
            // 
            this.toolStripSeparator25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.toolStripSeparator25.Name = "toolStripSeparator25";
            this.toolStripSeparator25.Size = new System.Drawing.Size(263, 6);
            this.toolStripSeparator25.Visible = false;
            // 
            // mnuReports_ReportingTools
            // 
            this.mnuReports_ReportingTools.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_ReportingTools.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_ReportingTools.Image")));
            this.mnuReports_ReportingTools.Name = "mnuReports_ReportingTools";
            this.mnuReports_ReportingTools.Size = new System.Drawing.Size(266, 22);
            this.mnuReports_ReportingTools.Text = "Reporting Tool";
            this.mnuReports_ReportingTools.Visible = false;
            this.mnuReports_ReportingTools.Click += new System.EventHandler(this.mnuReports_ReportingTools_Click);
            // 
            // mnuReports_PracticeAnalysis
            // 
            this.mnuReports_PracticeAnalysis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_PracticeAnalysis.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_PracticeAnalysis.Image")));
            this.mnuReports_PracticeAnalysis.Name = "mnuReports_PracticeAnalysis";
            this.mnuReports_PracticeAnalysis.Size = new System.Drawing.Size(266, 22);
            this.mnuReports_PracticeAnalysis.Text = "Practice Analysis";
            this.mnuReports_PracticeAnalysis.Visible = false;
            this.mnuReports_PracticeAnalysis.Click += new System.EventHandler(this.mnuReports_PracticeAnalysis_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(263, 6);
            // 
            // mnu_MISReports
            // 
            this.mnu_MISReports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_MISReports_PatientPaymentHistory,
            this.mnu_MISReports_PatientTransactionHistory,
            this.toolStripSeparator5,
            this.mnu_MISReports_Productivity,
            this.mnu_MISReports_ProductivityRVU,
            this.mnu_MISReports_ProductivityProviderPaymentMthd,
            this.mnu_MISReports_ProductivityDOS,
            this.mnu_MISReports_ExpectedCollection,
            this.mnu_MISReports_ProductionByDoctor,
            this.mnu_MISReports_ProductionByFacility,
            this.mnu_MISReports_ProductionByDate,
            this.mnu_MISReports_ProductionByMonth,
            this.mnu_MISReports_ProductionByProcedureGroup,
            this.mnu_MISReports_ProductionByProcedureCode,
            this.mnu_MISReports_ProductionByInsuranceCarrier,
            this.mnu_MISReports_ProductionByFacilityByPatient,
            this.mnu_MISReports_ProductionByFacilityByPatientDetail,
            this.toolStripSeparator16,
            this.mnu_MISReports_ReimbursementByMonth,
            this.mnu_MISReports_ReimbursementByMonthDetail,
            this.mnu_MISReports_ReimbursementByInsuranceCarrier,
            this.mnu_MISReports_ReimbursementByInsuranceByCPT,
            this.mnu_MISReports_ReimbursementByCPTByInsurance,
            this.mnu_MISReports_ReimbursementByDoctorByInsurance,
            this.mnu_MISReports_ReimbursementByInsuranceForCPT,
            this.mnu_MISReports_ReimbursementDetailsByAccount,
            this.toolStripSeparator26,
            this.mnu_MISReports_Aging,
            this.mnu_MISReports_FinancialSummary,
            this.mnu_MISReports_FinancialPayments,
            this.mnu_MISReports_AvailableReserve,
            this.mnu_MISReports_DenialManagement,
            this.toolStripSeparator28,
            this.mnu_MISReports_DailyCharges,
            this.mnu_MISReports_DailyPayments,
            this.mnu_MISReports_DailySummary,
            this.mnu_MISReports_MonthlyCharges,
            this.mnu_MISReports_MonthlyPayments,
            this.mnu_MISReports_MonthlyClose,
            this.mnu_MISReports_FinancialProSummary,
            this.mnu_MISReports_Fin_ProdSummaryDX,
            this.mnu_MISReports_CachedFinancialProductivitySum,
            this.mnu_MISReports_AgedPayment,
            this.toolStripSeparator29,
            this.mnu_MISReports_ProductionByPhysicianGroup,
            this.mnu_MISReports_ProductionAnalysisByFacility,
            this.mnu_MISReports_ProductionAnalysisByprocedureGroup,
            this.mnu_MISReports_ProductionAnalysisandTrendsByMonth,
            this.mnu_MISReports_ProductionTrendsByProcedureGrop,
            this.toolStripSeparator27,
            this.mnu_MISReports_CPTAnalysis,
            this.mnu_MISReports_QualityMeasures,
            this.mnu_MISReports_ChargesVSAllowedReport,
            this.mnu_MISReports_PayerLagReport,
            this.mnu_MISReports_PaymentPlanReport,
            this.mnu_MISReports_BadDebtCollectionReport,
            this.mnu_MISReports_MTDYTDReport});
            this.mnu_MISReports.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports.Image")));
            this.mnu_MISReports.Name = "mnu_MISReports";
            this.mnu_MISReports.Size = new System.Drawing.Size(266, 22);
            this.mnu_MISReports.Text = "MIS Reports";
            // 
            // mnu_MISReports_PatientPaymentHistory
            // 
            this.mnu_MISReports_PatientPaymentHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_PatientPaymentHistory.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_PatientPaymentHistory.Image")));
            this.mnu_MISReports_PatientPaymentHistory.Name = "mnu_MISReports_PatientPaymentHistory";
            this.mnu_MISReports_PatientPaymentHistory.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_PatientPaymentHistory.Text = "Patient Payment History";
            this.mnu_MISReports_PatientPaymentHistory.Visible = false;
            this.mnu_MISReports_PatientPaymentHistory.Click += new System.EventHandler(this.mnu_MISReports_PatientPaymentHistory_Click);
            // 
            // mnu_MISReports_PatientTransactionHistory
            // 
            this.mnu_MISReports_PatientTransactionHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_PatientTransactionHistory.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_PatientTransactionHistory.Image")));
            this.mnu_MISReports_PatientTransactionHistory.Name = "mnu_MISReports_PatientTransactionHistory";
            this.mnu_MISReports_PatientTransactionHistory.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_PatientTransactionHistory.Text = "Patient Transaction History";
            this.mnu_MISReports_PatientTransactionHistory.Visible = false;
            this.mnu_MISReports_PatientTransactionHistory.Click += new System.EventHandler(this.mnu_MISReports_PatientTransactionHistory_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(347, 6);
            this.toolStripSeparator5.Visible = false;
            // 
            // mnu_MISReports_Productivity
            // 
            this.mnu_MISReports_Productivity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_Productivity.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_Productivity.Image")));
            this.mnu_MISReports_Productivity.Name = "mnu_MISReports_Productivity";
            this.mnu_MISReports_Productivity.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_Productivity.Text = "Productivity";
            this.mnu_MISReports_Productivity.Click += new System.EventHandler(this.mnu_MISReports_Productivity_Click);
            // 
            // mnu_MISReports_ProductivityRVU
            // 
            this.mnu_MISReports_ProductivityRVU.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ProductivityRVU.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ProductivityRVU.Image")));
            this.mnu_MISReports_ProductivityRVU.Name = "mnu_MISReports_ProductivityRVU";
            this.mnu_MISReports_ProductivityRVU.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ProductivityRVU.Text = "Productivity By RVU";
            this.mnu_MISReports_ProductivityRVU.Click += new System.EventHandler(this.mnu_MISReports_ProductivityRVU_Click);
            // 
            // mnu_MISReports_ProductivityProviderPaymentMthd
            // 
            this.mnu_MISReports_ProductivityProviderPaymentMthd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ProductivityProviderPaymentMthd.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ProductivityProviderPaymentMthd.Image")));
            this.mnu_MISReports_ProductivityProviderPaymentMthd.Name = "mnu_MISReports_ProductivityProviderPaymentMthd";
            this.mnu_MISReports_ProductivityProviderPaymentMthd.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ProductivityProviderPaymentMthd.Text = "Productivity By Provider by Payment Method";
            this.mnu_MISReports_ProductivityProviderPaymentMthd.Click += new System.EventHandler(this.mnu_MISReports_ProductivityProviderPaymentMthd_Click);
            // 
            // mnu_MISReports_ProductivityDOS
            // 
            this.mnu_MISReports_ProductivityDOS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ProductivityDOS.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ProductivityDOS.Image")));
            this.mnu_MISReports_ProductivityDOS.Name = "mnu_MISReports_ProductivityDOS";
            this.mnu_MISReports_ProductivityDOS.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ProductivityDOS.Text = "Productivity By DOS";
            this.mnu_MISReports_ProductivityDOS.Click += new System.EventHandler(this.mnu_MISReports_ProductivityDOS_Click);
            // 
            // mnu_MISReports_ExpectedCollection
            // 
            this.mnu_MISReports_ExpectedCollection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ExpectedCollection.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ExpectedCollection.Image")));
            this.mnu_MISReports_ExpectedCollection.Name = "mnu_MISReports_ExpectedCollection";
            this.mnu_MISReports_ExpectedCollection.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ExpectedCollection.Text = "% Expected Collection";
            this.mnu_MISReports_ExpectedCollection.Click += new System.EventHandler(this.mnu_MISReports_ExpectedCollection_Click);
            // 
            // mnu_MISReports_ProductionByDoctor
            // 
            this.mnu_MISReports_ProductionByDoctor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ProductionByDoctor.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ProductionByDoctor.Image")));
            this.mnu_MISReports_ProductionByDoctor.Name = "mnu_MISReports_ProductionByDoctor";
            this.mnu_MISReports_ProductionByDoctor.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ProductionByDoctor.Text = "Production By Doctor";
            this.mnu_MISReports_ProductionByDoctor.Visible = false;
            this.mnu_MISReports_ProductionByDoctor.Click += new System.EventHandler(this.mnu_MISReports_ProductionByDoctor_Click);
            // 
            // mnu_MISReports_ProductionByFacility
            // 
            this.mnu_MISReports_ProductionByFacility.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ProductionByFacility.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ProductionByFacility.Image")));
            this.mnu_MISReports_ProductionByFacility.Name = "mnu_MISReports_ProductionByFacility";
            this.mnu_MISReports_ProductionByFacility.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ProductionByFacility.Text = "Production By Facility";
            this.mnu_MISReports_ProductionByFacility.Visible = false;
            this.mnu_MISReports_ProductionByFacility.Click += new System.EventHandler(this.mnu_MISReports_ProductionByFacility_Click);
            // 
            // mnu_MISReports_ProductionByDate
            // 
            this.mnu_MISReports_ProductionByDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ProductionByDate.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ProductionByDate.Image")));
            this.mnu_MISReports_ProductionByDate.Name = "mnu_MISReports_ProductionByDate";
            this.mnu_MISReports_ProductionByDate.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ProductionByDate.Text = "Production By Date";
            this.mnu_MISReports_ProductionByDate.Visible = false;
            this.mnu_MISReports_ProductionByDate.Click += new System.EventHandler(this.mnu_MISReports_ProductionByDate_Click);
            // 
            // mnu_MISReports_ProductionByMonth
            // 
            this.mnu_MISReports_ProductionByMonth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ProductionByMonth.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ProductionByMonth.Image")));
            this.mnu_MISReports_ProductionByMonth.Name = "mnu_MISReports_ProductionByMonth";
            this.mnu_MISReports_ProductionByMonth.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ProductionByMonth.Text = "Production By Month";
            this.mnu_MISReports_ProductionByMonth.Visible = false;
            this.mnu_MISReports_ProductionByMonth.Click += new System.EventHandler(this.mnu_MISReports_ProductionByMonth_Click);
            // 
            // mnu_MISReports_ProductionByProcedureGroup
            // 
            this.mnu_MISReports_ProductionByProcedureGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ProductionByProcedureGroup.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ProductionByProcedureGroup.Image")));
            this.mnu_MISReports_ProductionByProcedureGroup.Name = "mnu_MISReports_ProductionByProcedureGroup";
            this.mnu_MISReports_ProductionByProcedureGroup.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ProductionByProcedureGroup.Text = "Production By Procedure Group";
            this.mnu_MISReports_ProductionByProcedureGroup.Visible = false;
            this.mnu_MISReports_ProductionByProcedureGroup.Click += new System.EventHandler(this.mnu_MISReports_ProductionByProcedureGroup_Click);
            // 
            // mnu_MISReports_ProductionByProcedureCode
            // 
            this.mnu_MISReports_ProductionByProcedureCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ProductionByProcedureCode.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ProductionByProcedureCode.Image")));
            this.mnu_MISReports_ProductionByProcedureCode.Name = "mnu_MISReports_ProductionByProcedureCode";
            this.mnu_MISReports_ProductionByProcedureCode.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ProductionByProcedureCode.Text = "Production By Procedure Code";
            this.mnu_MISReports_ProductionByProcedureCode.Visible = false;
            this.mnu_MISReports_ProductionByProcedureCode.Click += new System.EventHandler(this.mnu_MISReports_ProductionByProcedureCode_Click);
            // 
            // mnu_MISReports_ProductionByInsuranceCarrier
            // 
            this.mnu_MISReports_ProductionByInsuranceCarrier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ProductionByInsuranceCarrier.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ProductionByInsuranceCarrier.Image")));
            this.mnu_MISReports_ProductionByInsuranceCarrier.Name = "mnu_MISReports_ProductionByInsuranceCarrier";
            this.mnu_MISReports_ProductionByInsuranceCarrier.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ProductionByInsuranceCarrier.Text = "Production By Insurance Carrier";
            this.mnu_MISReports_ProductionByInsuranceCarrier.Visible = false;
            this.mnu_MISReports_ProductionByInsuranceCarrier.Click += new System.EventHandler(this.mnu_MISReports_ProductionByInsuranceCarrier_Click);
            // 
            // mnu_MISReports_ProductionByFacilityByPatient
            // 
            this.mnu_MISReports_ProductionByFacilityByPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ProductionByFacilityByPatient.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ProductionByFacilityByPatient.Image")));
            this.mnu_MISReports_ProductionByFacilityByPatient.Name = "mnu_MISReports_ProductionByFacilityByPatient";
            this.mnu_MISReports_ProductionByFacilityByPatient.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ProductionByFacilityByPatient.Text = "Production By Facility By Patient - Summary";
            this.mnu_MISReports_ProductionByFacilityByPatient.Visible = false;
            this.mnu_MISReports_ProductionByFacilityByPatient.Click += new System.EventHandler(this.mnu_MISReports_ProductionByFacilityByPatient_Click);
            // 
            // mnu_MISReports_ProductionByFacilityByPatientDetail
            // 
            this.mnu_MISReports_ProductionByFacilityByPatientDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ProductionByFacilityByPatientDetail.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ProductionByFacilityByPatientDetail.Image")));
            this.mnu_MISReports_ProductionByFacilityByPatientDetail.Name = "mnu_MISReports_ProductionByFacilityByPatientDetail";
            this.mnu_MISReports_ProductionByFacilityByPatientDetail.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ProductionByFacilityByPatientDetail.Text = "Production By Facility By Patient - Details";
            this.mnu_MISReports_ProductionByFacilityByPatientDetail.Visible = false;
            this.mnu_MISReports_ProductionByFacilityByPatientDetail.Click += new System.EventHandler(this.mnu_MISReports_ProductionByFacilityByPatientDetail_Click);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(347, 6);
            this.toolStripSeparator16.Visible = false;
            // 
            // mnu_MISReports_ReimbursementByMonth
            // 
            this.mnu_MISReports_ReimbursementByMonth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ReimbursementByMonth.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ReimbursementByMonth.Image")));
            this.mnu_MISReports_ReimbursementByMonth.Name = "mnu_MISReports_ReimbursementByMonth";
            this.mnu_MISReports_ReimbursementByMonth.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ReimbursementByMonth.Text = "Reimbursement By Month";
            this.mnu_MISReports_ReimbursementByMonth.Visible = false;
            this.mnu_MISReports_ReimbursementByMonth.Click += new System.EventHandler(this.mnu_MISReports_ReimbursementByMonth_Click);
            // 
            // mnu_MISReports_ReimbursementByMonthDetail
            // 
            this.mnu_MISReports_ReimbursementByMonthDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ReimbursementByMonthDetail.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ReimbursementByMonthDetail.Image")));
            this.mnu_MISReports_ReimbursementByMonthDetail.Name = "mnu_MISReports_ReimbursementByMonthDetail";
            this.mnu_MISReports_ReimbursementByMonthDetail.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ReimbursementByMonthDetail.Text = "Reimbursement By Month By Account - Detail";
            this.mnu_MISReports_ReimbursementByMonthDetail.Visible = false;
            this.mnu_MISReports_ReimbursementByMonthDetail.Click += new System.EventHandler(this.mnu_MISReports_ReimbursementByMonthDetail_Click);
            // 
            // mnu_MISReports_ReimbursementByInsuranceCarrier
            // 
            this.mnu_MISReports_ReimbursementByInsuranceCarrier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ReimbursementByInsuranceCarrier.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ReimbursementByInsuranceCarrier.Image")));
            this.mnu_MISReports_ReimbursementByInsuranceCarrier.Name = "mnu_MISReports_ReimbursementByInsuranceCarrier";
            this.mnu_MISReports_ReimbursementByInsuranceCarrier.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ReimbursementByInsuranceCarrier.Text = "Reimbursement By Insurance Carrier";
            this.mnu_MISReports_ReimbursementByInsuranceCarrier.Visible = false;
            this.mnu_MISReports_ReimbursementByInsuranceCarrier.Click += new System.EventHandler(this.mnu_MISReports_ReimbursementByInsuranceCarrier_Click);
            // 
            // mnu_MISReports_ReimbursementByInsuranceByCPT
            // 
            this.mnu_MISReports_ReimbursementByInsuranceByCPT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ReimbursementByInsuranceByCPT.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ReimbursementByInsuranceByCPT.Image")));
            this.mnu_MISReports_ReimbursementByInsuranceByCPT.Name = "mnu_MISReports_ReimbursementByInsuranceByCPT";
            this.mnu_MISReports_ReimbursementByInsuranceByCPT.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ReimbursementByInsuranceByCPT.Text = "Reimbursement By Insurance Carrier By CPT Code";
            this.mnu_MISReports_ReimbursementByInsuranceByCPT.Visible = false;
            this.mnu_MISReports_ReimbursementByInsuranceByCPT.Click += new System.EventHandler(this.mnu_MISReports_ReimbursementByInsuranceByCPT_Click);
            // 
            // mnu_MISReports_ReimbursementByCPTByInsurance
            // 
            this.mnu_MISReports_ReimbursementByCPTByInsurance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ReimbursementByCPTByInsurance.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ReimbursementByCPTByInsurance.Image")));
            this.mnu_MISReports_ReimbursementByCPTByInsurance.Name = "mnu_MISReports_ReimbursementByCPTByInsurance";
            this.mnu_MISReports_ReimbursementByCPTByInsurance.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ReimbursementByCPTByInsurance.Text = "Reimbursement By CPT Code By Insurance Carrier";
            this.mnu_MISReports_ReimbursementByCPTByInsurance.Visible = false;
            this.mnu_MISReports_ReimbursementByCPTByInsurance.Click += new System.EventHandler(this.mnu_MISReports_ReimbursementByCPTByInsurance_Click);
            // 
            // mnu_MISReports_ReimbursementByDoctorByInsurance
            // 
            this.mnu_MISReports_ReimbursementByDoctorByInsurance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ReimbursementByDoctorByInsurance.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ReimbursementByDoctorByInsurance.Image")));
            this.mnu_MISReports_ReimbursementByDoctorByInsurance.Name = "mnu_MISReports_ReimbursementByDoctorByInsurance";
            this.mnu_MISReports_ReimbursementByDoctorByInsurance.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ReimbursementByDoctorByInsurance.Text = "Reimbursement By Doctor By Insurance Carrier";
            this.mnu_MISReports_ReimbursementByDoctorByInsurance.Visible = false;
            this.mnu_MISReports_ReimbursementByDoctorByInsurance.Click += new System.EventHandler(this.mnu_MISReports_ReimbursementByDoctorByInsurance_Click);
            // 
            // mnu_MISReports_ReimbursementByInsuranceForCPT
            // 
            this.mnu_MISReports_ReimbursementByInsuranceForCPT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ReimbursementByInsuranceForCPT.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ReimbursementByInsuranceForCPT.Image")));
            this.mnu_MISReports_ReimbursementByInsuranceForCPT.Name = "mnu_MISReports_ReimbursementByInsuranceForCPT";
            this.mnu_MISReports_ReimbursementByInsuranceForCPT.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ReimbursementByInsuranceForCPT.Text = "Reimbursement By Insurance Carrier For CPT Code";
            this.mnu_MISReports_ReimbursementByInsuranceForCPT.Visible = false;
            this.mnu_MISReports_ReimbursementByInsuranceForCPT.Click += new System.EventHandler(this.mnu_MISReports_ReimbursementByInsuranceForCPT_Click);
            // 
            // mnu_MISReports_ReimbursementDetailsByAccount
            // 
            this.mnu_MISReports_ReimbursementDetailsByAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ReimbursementDetailsByAccount.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ReimbursementDetailsByAccount.Image")));
            this.mnu_MISReports_ReimbursementDetailsByAccount.Name = "mnu_MISReports_ReimbursementDetailsByAccount";
            this.mnu_MISReports_ReimbursementDetailsByAccount.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ReimbursementDetailsByAccount.Text = "Reimbursement Details By Account";
            this.mnu_MISReports_ReimbursementDetailsByAccount.Visible = false;
            this.mnu_MISReports_ReimbursementDetailsByAccount.Click += new System.EventHandler(this.mnu_MISReports_ReimbursementDetailsByAccount_Click);
            // 
            // toolStripSeparator26
            // 
            this.toolStripSeparator26.Name = "toolStripSeparator26";
            this.toolStripSeparator26.Size = new System.Drawing.Size(347, 6);
            // 
            // mnu_MISReports_Aging
            // 
            this.mnu_MISReports_Aging.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_Aging.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_Aging.Image")));
            this.mnu_MISReports_Aging.Name = "mnu_MISReports_Aging";
            this.mnu_MISReports_Aging.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_Aging.Text = "Aging ";
            this.mnu_MISReports_Aging.Click += new System.EventHandler(this.mnu_MISReports_Aging_Click);
            // 
            // mnu_MISReports_FinancialSummary
            // 
            this.mnu_MISReports_FinancialSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_FinancialSummary.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_FinancialSummary.Image")));
            this.mnu_MISReports_FinancialSummary.Name = "mnu_MISReports_FinancialSummary";
            this.mnu_MISReports_FinancialSummary.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_FinancialSummary.Text = "Financial Summary-old";
            this.mnu_MISReports_FinancialSummary.Visible = false;
            this.mnu_MISReports_FinancialSummary.Click += new System.EventHandler(this.mnu_MISReports_FinancialSummary_Click);
            // 
            // mnu_MISReports_FinancialPayments
            // 
            this.mnu_MISReports_FinancialPayments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_FinancialPayments.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_FinancialPayments.Image")));
            this.mnu_MISReports_FinancialPayments.Name = "mnu_MISReports_FinancialPayments";
            this.mnu_MISReports_FinancialPayments.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_FinancialPayments.Text = "Financial Summary ";
            this.mnu_MISReports_FinancialPayments.Visible = false;
            this.mnu_MISReports_FinancialPayments.Click += new System.EventHandler(this.mnu_MISReports_FinancialPayments_Click);
            // 
            // mnu_MISReports_AvailableReserve
            // 
            this.mnu_MISReports_AvailableReserve.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_AvailableReserve.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_AvailableReserve.Image")));
            this.mnu_MISReports_AvailableReserve.Name = "mnu_MISReports_AvailableReserve";
            this.mnu_MISReports_AvailableReserve.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_AvailableReserve.Text = "Available Reserves";
            this.mnu_MISReports_AvailableReserve.Click += new System.EventHandler(this.mnu_MISReports_AvailableReserve_Click);
            // 
            // mnu_MISReports_DenialManagement
            // 
            this.mnu_MISReports_DenialManagement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_DenialManagement.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_DenialManagement.Image")));
            this.mnu_MISReports_DenialManagement.Name = "mnu_MISReports_DenialManagement";
            this.mnu_MISReports_DenialManagement.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_DenialManagement.Text = "Denial Management";
            this.mnu_MISReports_DenialManagement.Click += new System.EventHandler(this.mnu_MISReports_DenialManagement_Click);
            // 
            // toolStripSeparator28
            // 
            this.toolStripSeparator28.Name = "toolStripSeparator28";
            this.toolStripSeparator28.Size = new System.Drawing.Size(347, 6);
            // 
            // mnu_MISReports_DailyCharges
            // 
            this.mnu_MISReports_DailyCharges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_DailyCharges.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_DailyCharges.Image")));
            this.mnu_MISReports_DailyCharges.Name = "mnu_MISReports_DailyCharges";
            this.mnu_MISReports_DailyCharges.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_DailyCharges.Text = "Daily Charges";
            this.mnu_MISReports_DailyCharges.Click += new System.EventHandler(this.mnu_MISReports_DailyCharges_Click);
            // 
            // mnu_MISReports_DailyPayments
            // 
            this.mnu_MISReports_DailyPayments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_DailyPayments.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_DailyPayments.Image")));
            this.mnu_MISReports_DailyPayments.Name = "mnu_MISReports_DailyPayments";
            this.mnu_MISReports_DailyPayments.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_DailyPayments.Text = "Daily Payments";
            this.mnu_MISReports_DailyPayments.Click += new System.EventHandler(this.mnu_MISReports_DailyPayments_Click);
            // 
            // mnu_MISReports_DailySummary
            // 
            this.mnu_MISReports_DailySummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_DailySummary.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_DailySummary.Image")));
            this.mnu_MISReports_DailySummary.Name = "mnu_MISReports_DailySummary";
            this.mnu_MISReports_DailySummary.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_DailySummary.Text = "Daily Close";
            this.mnu_MISReports_DailySummary.Click += new System.EventHandler(this.mnu_MISReports_DailySummary_Click);
            // 
            // mnu_MISReports_MonthlyCharges
            // 
            this.mnu_MISReports_MonthlyCharges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_MonthlyCharges.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_MonthlyCharges.Image")));
            this.mnu_MISReports_MonthlyCharges.Name = "mnu_MISReports_MonthlyCharges";
            this.mnu_MISReports_MonthlyCharges.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_MonthlyCharges.Text = "Monthly Charges";
            this.mnu_MISReports_MonthlyCharges.Click += new System.EventHandler(this.mnu_MISReports_MonthlyCharges_Click);
            // 
            // mnu_MISReports_MonthlyPayments
            // 
            this.mnu_MISReports_MonthlyPayments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_MonthlyPayments.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_MonthlyPayments.Image")));
            this.mnu_MISReports_MonthlyPayments.Name = "mnu_MISReports_MonthlyPayments";
            this.mnu_MISReports_MonthlyPayments.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_MonthlyPayments.Text = "Monthly Payments";
            this.mnu_MISReports_MonthlyPayments.Click += new System.EventHandler(this.mnu_MISReports_MonthlyPayments_Click);
            // 
            // mnu_MISReports_MonthlyClose
            // 
            this.mnu_MISReports_MonthlyClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_MonthlyClose.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_MonthlyClose.Image")));
            this.mnu_MISReports_MonthlyClose.Name = "mnu_MISReports_MonthlyClose";
            this.mnu_MISReports_MonthlyClose.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_MonthlyClose.Text = "Monthly Close";
            this.mnu_MISReports_MonthlyClose.Click += new System.EventHandler(this.mnu_MISReports_MonthlyClose_Click);
            // 
            // mnu_MISReports_FinancialProSummary
            // 
            this.mnu_MISReports_FinancialProSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_FinancialProSummary.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_FinancialProSummary.Image")));
            this.mnu_MISReports_FinancialProSummary.Name = "mnu_MISReports_FinancialProSummary";
            this.mnu_MISReports_FinancialProSummary.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_FinancialProSummary.Text = "Financial Productivity Summary";
            this.mnu_MISReports_FinancialProSummary.Click += new System.EventHandler(this.mnu_MISReports_FinancialProSummary_Click);
            // 
            // mnu_MISReports_Fin_ProdSummaryDX
            // 
            this.mnu_MISReports_Fin_ProdSummaryDX.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_Fin_ProdSummaryDX.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_Fin_ProdSummaryDX.Image")));
            this.mnu_MISReports_Fin_ProdSummaryDX.Name = "mnu_MISReports_Fin_ProdSummaryDX";
            this.mnu_MISReports_Fin_ProdSummaryDX.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_Fin_ProdSummaryDX.Text = "Fin-Prod Summary with DX";
            this.mnu_MISReports_Fin_ProdSummaryDX.Click += new System.EventHandler(this.mnu_MISReports_Fin_ProdSummaryDX_Click);
            // 
            // mnu_MISReports_CachedFinancialProductivitySum
            // 
            this.mnu_MISReports_CachedFinancialProductivitySum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_CachedFinancialProductivitySum.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_CachedFinancialProductivitySum.Image")));
            this.mnu_MISReports_CachedFinancialProductivitySum.Name = "mnu_MISReports_CachedFinancialProductivitySum";
            this.mnu_MISReports_CachedFinancialProductivitySum.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_CachedFinancialProductivitySum.Text = "Cached-Financial Productivity Summary";
            this.mnu_MISReports_CachedFinancialProductivitySum.Click += new System.EventHandler(this.mnu_MISReports_CachedFinancialProductivitySum_Click);
            // 
            // mnu_MISReports_AgedPayment
            // 
            this.mnu_MISReports_AgedPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_AgedPayment.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_AgedPayment.Image")));
            this.mnu_MISReports_AgedPayment.Name = "mnu_MISReports_AgedPayment";
            this.mnu_MISReports_AgedPayment.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_AgedPayment.Text = "Aged Payment";
            this.mnu_MISReports_AgedPayment.Click += new System.EventHandler(this.mnu_MISReports_AgedPayment_Click);
            // 
            // toolStripSeparator29
            // 
            this.toolStripSeparator29.Name = "toolStripSeparator29";
            this.toolStripSeparator29.Size = new System.Drawing.Size(347, 6);
            this.toolStripSeparator29.Visible = false;
            // 
            // mnu_MISReports_ProductionByPhysicianGroup
            // 
            this.mnu_MISReports_ProductionByPhysicianGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ProductionByPhysicianGroup.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ProductionByPhysicianGroup.Image")));
            this.mnu_MISReports_ProductionByPhysicianGroup.Name = "mnu_MISReports_ProductionByPhysicianGroup";
            this.mnu_MISReports_ProductionByPhysicianGroup.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ProductionByPhysicianGroup.Text = "Production By Physician Group";
            this.mnu_MISReports_ProductionByPhysicianGroup.Visible = false;
            this.mnu_MISReports_ProductionByPhysicianGroup.Click += new System.EventHandler(this.mnu_MISReports_ProductionByPhysicianGroup_Click);
            // 
            // mnu_MISReports_ProductionAnalysisByFacility
            // 
            this.mnu_MISReports_ProductionAnalysisByFacility.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ProductionAnalysisByFacility.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ProductionAnalysisByFacility.Image")));
            this.mnu_MISReports_ProductionAnalysisByFacility.Name = "mnu_MISReports_ProductionAnalysisByFacility";
            this.mnu_MISReports_ProductionAnalysisByFacility.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ProductionAnalysisByFacility.Text = "Production Analysis By Facility";
            this.mnu_MISReports_ProductionAnalysisByFacility.Visible = false;
            this.mnu_MISReports_ProductionAnalysisByFacility.Click += new System.EventHandler(this.mnu_MISReports_ProductionAnalysisByFacility_Click);
            // 
            // mnu_MISReports_ProductionAnalysisByprocedureGroup
            // 
            this.mnu_MISReports_ProductionAnalysisByprocedureGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ProductionAnalysisByprocedureGroup.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ProductionAnalysisByprocedureGroup.Image")));
            this.mnu_MISReports_ProductionAnalysisByprocedureGroup.Name = "mnu_MISReports_ProductionAnalysisByprocedureGroup";
            this.mnu_MISReports_ProductionAnalysisByprocedureGroup.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ProductionAnalysisByprocedureGroup.Text = "Production Analysis By Procedure Group";
            this.mnu_MISReports_ProductionAnalysisByprocedureGroup.Visible = false;
            this.mnu_MISReports_ProductionAnalysisByprocedureGroup.Click += new System.EventHandler(this.mnu_MISReports_ProductionAnalysisByprocedureGroup_Click);
            // 
            // mnu_MISReports_ProductionAnalysisandTrendsByMonth
            // 
            this.mnu_MISReports_ProductionAnalysisandTrendsByMonth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ProductionAnalysisandTrendsByMonth.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ProductionAnalysisandTrendsByMonth.Image")));
            this.mnu_MISReports_ProductionAnalysisandTrendsByMonth.Name = "mnu_MISReports_ProductionAnalysisandTrendsByMonth";
            this.mnu_MISReports_ProductionAnalysisandTrendsByMonth.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ProductionAnalysisandTrendsByMonth.Text = "Production Analysis And Trends By Month";
            this.mnu_MISReports_ProductionAnalysisandTrendsByMonth.Visible = false;
            this.mnu_MISReports_ProductionAnalysisandTrendsByMonth.Click += new System.EventHandler(this.mnu_MISReports_ProductionAnalysisandTrendsByMonth_Click);
            // 
            // mnu_MISReports_ProductionTrendsByProcedureGrop
            // 
            this.mnu_MISReports_ProductionTrendsByProcedureGrop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ProductionTrendsByProcedureGrop.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ProductionTrendsByProcedureGrop.Image")));
            this.mnu_MISReports_ProductionTrendsByProcedureGrop.Name = "mnu_MISReports_ProductionTrendsByProcedureGrop";
            this.mnu_MISReports_ProductionTrendsByProcedureGrop.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ProductionTrendsByProcedureGrop.Text = "Production Trends By Procedure Group";
            this.mnu_MISReports_ProductionTrendsByProcedureGrop.Visible = false;
            this.mnu_MISReports_ProductionTrendsByProcedureGrop.Click += new System.EventHandler(this.mnu_MISReports_ProductionTrendsByProcedureGrop_Click);
            // 
            // toolStripSeparator27
            // 
            this.toolStripSeparator27.Name = "toolStripSeparator27";
            this.toolStripSeparator27.Size = new System.Drawing.Size(347, 6);
            this.toolStripSeparator27.Visible = false;
            // 
            // mnu_MISReports_CPTAnalysis
            // 
            this.mnu_MISReports_CPTAnalysis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_CPTAnalysis.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_CPTAnalysis.Image")));
            this.mnu_MISReports_CPTAnalysis.Name = "mnu_MISReports_CPTAnalysis";
            this.mnu_MISReports_CPTAnalysis.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_CPTAnalysis.Text = "CPT Analysis";
            this.mnu_MISReports_CPTAnalysis.Click += new System.EventHandler(this.mnu_MISReports_CPTAnalysis_Click);
            // 
            // mnu_MISReports_QualityMeasures
            // 
            this.mnu_MISReports_QualityMeasures.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_QualityMeasures.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_QualityMeasures.Image")));
            this.mnu_MISReports_QualityMeasures.Name = "mnu_MISReports_QualityMeasures";
            this.mnu_MISReports_QualityMeasures.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_QualityMeasures.Text = "Quality Measures";
            this.mnu_MISReports_QualityMeasures.Click += new System.EventHandler(this.mnu_MISReports_QualityMeasures_Click);
            // 
            // mnu_MISReports_ChargesVSAllowedReport
            // 
            this.mnu_MISReports_ChargesVSAllowedReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_ChargesVSAllowedReport.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_ChargesVSAllowedReport.Image")));
            this.mnu_MISReports_ChargesVSAllowedReport.Name = "mnu_MISReports_ChargesVSAllowedReport";
            this.mnu_MISReports_ChargesVSAllowedReport.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_ChargesVSAllowedReport.Text = "Charges v/s Allowed Report";
            this.mnu_MISReports_ChargesVSAllowedReport.Click += new System.EventHandler(this.mnu_MISReports_ChargesVSAllowedReport_Click);
            // 
            // mnu_MISReports_PayerLagReport
            // 
            this.mnu_MISReports_PayerLagReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_PayerLagReport.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_PayerLagReport.Image")));
            this.mnu_MISReports_PayerLagReport.Name = "mnu_MISReports_PayerLagReport";
            this.mnu_MISReports_PayerLagReport.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_PayerLagReport.Text = "Payer Lag Time Report";
            this.mnu_MISReports_PayerLagReport.Click += new System.EventHandler(this.mnu_MISReports_PayerLagReport_Click);
            // 
            // mnu_MISReports_PaymentPlanReport
            // 
            this.mnu_MISReports_PaymentPlanReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_PaymentPlanReport.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_PaymentPlanReport.Image")));
            this.mnu_MISReports_PaymentPlanReport.Name = "mnu_MISReports_PaymentPlanReport";
            this.mnu_MISReports_PaymentPlanReport.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_PaymentPlanReport.Text = "Payment Plan Report";
            this.mnu_MISReports_PaymentPlanReport.Click += new System.EventHandler(this.mnu_MISReports_PaymentPlanReport_Click);
            // 
            // mnu_MISReports_BadDebtCollectionReport
            // 
            this.mnu_MISReports_BadDebtCollectionReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_BadDebtCollectionReport.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_BadDebtCollectionReport.Image")));
            this.mnu_MISReports_BadDebtCollectionReport.Name = "mnu_MISReports_BadDebtCollectionReport";
            this.mnu_MISReports_BadDebtCollectionReport.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_BadDebtCollectionReport.Text = "Bad Debt Collection Report";
            this.mnu_MISReports_BadDebtCollectionReport.Click += new System.EventHandler(this.mnu_MISReports_BadDebtCollectionReport_Click);
            // 
            // mnu_MISReports_MTDYTDReport
            // 
            this.mnu_MISReports_MTDYTDReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MISReports_MTDYTDReport.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MISReports_MTDYTDReport.Image")));
            this.mnu_MISReports_MTDYTDReport.Name = "mnu_MISReports_MTDYTDReport";
            this.mnu_MISReports_MTDYTDReport.Size = new System.Drawing.Size(350, 22);
            this.mnu_MISReports_MTDYTDReport.Text = "MTD/YTD Report";
            this.mnu_MISReports_MTDYTDReport.Click += new System.EventHandler(this.mnu_MISReports_MTDYTDReport_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(263, 6);
            // 
            // mnu_rpt_PaymentTrayReport
            // 
            this.mnu_rpt_PaymentTrayReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_rpt_PaymentTrayReport.Image = ((System.Drawing.Image)(resources.GetObject("mnu_rpt_PaymentTrayReport.Image")));
            this.mnu_rpt_PaymentTrayReport.Name = "mnu_rpt_PaymentTrayReport";
            this.mnu_rpt_PaymentTrayReport.Size = new System.Drawing.Size(266, 22);
            this.mnu_rpt_PaymentTrayReport.Text = "Payment Tray Report";
            this.mnu_rpt_PaymentTrayReport.Visible = false;
            this.mnu_rpt_PaymentTrayReport.Click += new System.EventHandler(this.mnu_rpt_PaymentTrayReport_Click);
            // 
            // mnu_rpt_Appointments
            // 
            this.mnu_rpt_Appointments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_rpt_Appointments.Image = ((System.Drawing.Image)(resources.GetObject("mnu_rpt_Appointments.Image")));
            this.mnu_rpt_Appointments.Name = "mnu_rpt_Appointments";
            this.mnu_rpt_Appointments.Size = new System.Drawing.Size(266, 22);
            this.mnu_rpt_Appointments.Text = "Appointments";
            this.mnu_rpt_Appointments.Click += new System.EventHandler(this.mnu_rpt_Appointments_Click);
            // 
            // mnu_rpt_ConfirmAppointments
            // 
            this.mnu_rpt_ConfirmAppointments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_rpt_ConfirmAppointments.Image = ((System.Drawing.Image)(resources.GetObject("mnu_rpt_ConfirmAppointments.Image")));
            this.mnu_rpt_ConfirmAppointments.Name = "mnu_rpt_ConfirmAppointments";
            this.mnu_rpt_ConfirmAppointments.Size = new System.Drawing.Size(266, 22);
            this.mnu_rpt_ConfirmAppointments.Text = "Confirm Appointments";
            this.mnu_rpt_ConfirmAppointments.Click += new System.EventHandler(this.mnu_rpt_ConfirmAppointments_Click);
            // 
            // mnu_rpt_NoShowAppointments
            // 
            this.mnu_rpt_NoShowAppointments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_rpt_NoShowAppointments.Image = ((System.Drawing.Image)(resources.GetObject("mnu_rpt_NoShowAppointments.Image")));
            this.mnu_rpt_NoShowAppointments.Name = "mnu_rpt_NoShowAppointments";
            this.mnu_rpt_NoShowAppointments.Size = new System.Drawing.Size(266, 22);
            this.mnu_rpt_NoShowAppointments.Text = "Cancel Appointments";
            this.mnu_rpt_NoShowAppointments.Click += new System.EventHandler(this.mnu_rpt_NoShowAppointments_Click);
            // 
            // mnu_rpt_MissedOpportunitiesReport
            // 
            this.mnu_rpt_MissedOpportunitiesReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_rpt_MissedOpportunitiesReport.Image = ((System.Drawing.Image)(resources.GetObject("mnu_rpt_MissedOpportunitiesReport.Image")));
            this.mnu_rpt_MissedOpportunitiesReport.Name = "mnu_rpt_MissedOpportunitiesReport";
            this.mnu_rpt_MissedOpportunitiesReport.Size = new System.Drawing.Size(266, 22);
            this.mnu_rpt_MissedOpportunitiesReport.Text = "Missed Opportunities Report";
            this.mnu_rpt_MissedOpportunitiesReport.Click += new System.EventHandler(this.mnu_rpt_MissedOpportunitiesReport_Click);
            // 
            // mnu_rpt_AppointmentCensusReport
            // 
            this.mnu_rpt_AppointmentCensusReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_rpt_AppointmentCensusReport.Image = ((System.Drawing.Image)(resources.GetObject("mnu_rpt_AppointmentCensusReport.Image")));
            this.mnu_rpt_AppointmentCensusReport.Name = "mnu_rpt_AppointmentCensusReport";
            this.mnu_rpt_AppointmentCensusReport.Size = new System.Drawing.Size(266, 22);
            this.mnu_rpt_AppointmentCensusReport.Text = "Appointment Census Report";
            this.mnu_rpt_AppointmentCensusReport.Click += new System.EventHandler(this.mnu_rpt_AppointmentCensusReport_Click);
            // 
            // mnu_rpt_PatientBenefitsInfo
            // 
            this.mnu_rpt_PatientBenefitsInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_rpt_PatientBenefitsInfo.Image = ((System.Drawing.Image)(resources.GetObject("mnu_rpt_PatientBenefitsInfo.Image")));
            this.mnu_rpt_PatientBenefitsInfo.Name = "mnu_rpt_PatientBenefitsInfo";
            this.mnu_rpt_PatientBenefitsInfo.Size = new System.Drawing.Size(266, 22);
            this.mnu_rpt_PatientBenefitsInfo.Text = "Patient Benefits Information";
            this.mnu_rpt_PatientBenefitsInfo.Click += new System.EventHandler(this.mnu_rpt_PatientBenefitsInfo_Click);
            // 
            // mnu_rpt_AppointmentWaiting
            // 
            this.mnu_rpt_AppointmentWaiting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_rpt_AppointmentWaiting.Image = ((System.Drawing.Image)(resources.GetObject("mnu_rpt_AppointmentWaiting.Image")));
            this.mnu_rpt_AppointmentWaiting.Name = "mnu_rpt_AppointmentWaiting";
            this.mnu_rpt_AppointmentWaiting.Size = new System.Drawing.Size(266, 22);
            this.mnu_rpt_AppointmentWaiting.Text = "Waiting Appointments";
            this.mnu_rpt_AppointmentWaiting.Visible = false;
            this.mnu_rpt_AppointmentWaiting.Click += new System.EventHandler(this.mnu_rpt_AppointmentWaiting_Click);
            // 
            // mnuRpt_BatchPrint
            // 
            this.mnuRpt_BatchPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuRpt_BatchPrint.Image = ((System.Drawing.Image)(resources.GetObject("mnuRpt_BatchPrint.Image")));
            this.mnuRpt_BatchPrint.Name = "mnuRpt_BatchPrint";
            this.mnuRpt_BatchPrint.Size = new System.Drawing.Size(266, 22);
            this.mnuRpt_BatchPrint.Text = "Batch Print Templates";
            this.mnuRpt_BatchPrint.Click += new System.EventHandler(this.mnuRpt_BatchPrint_Click);
            // 
            // mnuRpt_VoidClaims
            // 
            this.mnuRpt_VoidClaims.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuRpt_VoidClaims.Image = ((System.Drawing.Image)(resources.GetObject("mnuRpt_VoidClaims.Image")));
            this.mnuRpt_VoidClaims.Name = "mnuRpt_VoidClaims";
            this.mnuRpt_VoidClaims.Size = new System.Drawing.Size(266, 22);
            this.mnuRpt_VoidClaims.Text = "Void Claims";
            this.mnuRpt_VoidClaims.Click += new System.EventHandler(this.mnuRpt_VoidClaims_Click);
            // 
            // mnu_MissingChargesReport
            // 
            this.mnu_MissingChargesReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MissingChargesReport.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MissingChargesReport.Image")));
            this.mnu_MissingChargesReport.Name = "mnu_MissingChargesReport";
            this.mnu_MissingChargesReport.Size = new System.Drawing.Size(266, 22);
            this.mnu_MissingChargesReport.Text = "Missing Charges";
            this.mnu_MissingChargesReport.Click += new System.EventHandler(this.mnu_MissingChargesReport_Click);
            // 
            // mnuReportsPendingCopayReport
            // 
            this.mnuReportsPendingCopayReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReportsPendingCopayReport.Image = ((System.Drawing.Image)(resources.GetObject("mnuReportsPendingCopayReport.Image")));
            this.mnuReportsPendingCopayReport.Name = "mnuReportsPendingCopayReport";
            this.mnuReportsPendingCopayReport.Size = new System.Drawing.Size(266, 22);
            this.mnuReportsPendingCopayReport.Text = "Pending Copay Report";
            this.mnuReportsPendingCopayReport.Visible = false;
            this.mnuReportsPendingCopayReport.Click += new System.EventHandler(this.mnuReportsPendingCopayReport_Click);
            // 
            // mnu_MonthEndReport
            // 
            this.mnu_MonthEndReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_MonthEndReport.Image = ((System.Drawing.Image)(resources.GetObject("mnu_MonthEndReport.Image")));
            this.mnu_MonthEndReport.Name = "mnu_MonthEndReport";
            this.mnu_MonthEndReport.Size = new System.Drawing.Size(266, 22);
            this.mnu_MonthEndReport.Text = "Charge Summary Report";
            this.mnu_MonthEndReport.Click += new System.EventHandler(this.mnu_MonthEndReport_Click);
            // 
            // mnuReports_OvreduePatientPayment
            // 
            this.mnuReports_OvreduePatientPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_OvreduePatientPayment.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_OvreduePatientPayment.Image")));
            this.mnuReports_OvreduePatientPayment.Name = "mnuReports_OvreduePatientPayment";
            this.mnuReports_OvreduePatientPayment.Size = new System.Drawing.Size(266, 22);
            this.mnuReports_OvreduePatientPayment.Text = "Overdue Patient Payment";
            this.mnuReports_OvreduePatientPayment.Visible = false;
            this.mnuReports_OvreduePatientPayment.Click += new System.EventHandler(this.mnuReports_OvreduePatientPayment_Click);
            // 
            // mnuReports_OverdueInsurancePayment
            // 
            this.mnuReports_OverdueInsurancePayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_OverdueInsurancePayment.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_OverdueInsurancePayment.Image")));
            this.mnuReports_OverdueInsurancePayment.Name = "mnuReports_OverdueInsurancePayment";
            this.mnuReports_OverdueInsurancePayment.Size = new System.Drawing.Size(266, 22);
            this.mnuReports_OverdueInsurancePayment.Text = "Overdue Insurance Payment";
            this.mnuReports_OverdueInsurancePayment.Visible = false;
            this.mnuReports_OverdueInsurancePayment.Click += new System.EventHandler(this.mnuReports_OverdueInsurancePayment_Click);
            // 
            // mnuReports_PatientVsEstablishedReport
            // 
            this.mnuReports_PatientVsEstablishedReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_PatientVsEstablishedReport.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_PatientVsEstablishedReport.Image")));
            this.mnuReports_PatientVsEstablishedReport.Name = "mnuReports_PatientVsEstablishedReport";
            this.mnuReports_PatientVsEstablishedReport.Size = new System.Drawing.Size(266, 22);
            this.mnuReports_PatientVsEstablishedReport.Text = "New Patient Vs Established Patient";
            this.mnuReports_PatientVsEstablishedReport.Click += new System.EventHandler(this.mnuReports_PatientVsEstablishedReport_Click);
            // 
            // mnuReports_DailyCollectionReport
            // 
            this.mnuReports_DailyCollectionReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_DailyCollectionReport.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_DailyCollectionReport.Image")));
            this.mnuReports_DailyCollectionReport.Name = "mnuReports_DailyCollectionReport";
            this.mnuReports_DailyCollectionReport.Size = new System.Drawing.Size(266, 22);
            this.mnuReports_DailyCollectionReport.Text = "Daily Collection Report";
            this.mnuReports_DailyCollectionReport.Visible = false;
            this.mnuReports_DailyCollectionReport.Click += new System.EventHandler(this.mnuReports_DailyCollectionReport_Click);
            // 
            // mnuReportsRefund
            // 
            this.mnuReportsRefund.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReportsRefund.Image = ((System.Drawing.Image)(resources.GetObject("mnuReportsRefund.Image")));
            this.mnuReportsRefund.Name = "mnuReportsRefund";
            this.mnuReportsRefund.Size = new System.Drawing.Size(266, 22);
            this.mnuReportsRefund.Text = "Refund";
            this.mnuReportsRefund.ToolTipText = "Refund";
            this.mnuReportsRefund.Visible = false;
            this.mnuReportsRefund.Click += new System.EventHandler(this.mnuReportsRefund_Click);
            // 
            // mnuReports_ZeroBalancePatient
            // 
            this.mnuReports_ZeroBalancePatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_ZeroBalancePatient.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_ZeroBalancePatient.Image")));
            this.mnuReports_ZeroBalancePatient.Name = "mnuReports_ZeroBalancePatient";
            this.mnuReports_ZeroBalancePatient.Size = new System.Drawing.Size(266, 22);
            this.mnuReports_ZeroBalancePatient.Text = "Patient Balance ";
            this.mnuReports_ZeroBalancePatient.Visible = false;
            this.mnuReports_ZeroBalancePatient.Click += new System.EventHandler(this.mnuReports_ZeroBalancePatient_Click);
            // 
            // mnuReportsTransactionHistory
            // 
            this.mnuReportsTransactionHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReportsTransactionHistory.Image = ((System.Drawing.Image)(resources.GetObject("mnuReportsTransactionHistory.Image")));
            this.mnuReportsTransactionHistory.Name = "mnuReportsTransactionHistory";
            this.mnuReportsTransactionHistory.Size = new System.Drawing.Size(266, 22);
            this.mnuReportsTransactionHistory.Text = "Transaction History";
            this.mnuReportsTransactionHistory.Visible = false;
            this.mnuReportsTransactionHistory.Click += new System.EventHandler(this.mnuReportsTransactionHistory_Click);
            // 
            // mnu_InsuranceCompanySetup
            // 
            this.mnu_InsuranceCompanySetup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_InsuranceCompanySetup_Company_Category});
            this.mnu_InsuranceCompanySetup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_InsuranceCompanySetup.Image = ((System.Drawing.Image)(resources.GetObject("mnu_InsuranceCompanySetup.Image")));
            this.mnu_InsuranceCompanySetup.Name = "mnu_InsuranceCompanySetup";
            this.mnu_InsuranceCompanySetup.Size = new System.Drawing.Size(266, 22);
            this.mnu_InsuranceCompanySetup.Text = "Insurance Company Setup";
            // 
            // mnu_InsuranceCompanySetup_Company_Category
            // 
            this.mnu_InsuranceCompanySetup_Company_Category.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_InsuranceCompanySetup_Company_Category.Image = ((System.Drawing.Image)(resources.GetObject("mnu_InsuranceCompanySetup_Company_Category.Image")));
            this.mnu_InsuranceCompanySetup_Company_Category.Name = "mnu_InsuranceCompanySetup_Company_Category";
            this.mnu_InsuranceCompanySetup_Company_Category.Size = new System.Drawing.Size(178, 22);
            this.mnu_InsuranceCompanySetup_Company_Category.Text = "Company/Category";
            this.mnu_InsuranceCompanySetup_Company_Category.Click += new System.EventHandler(this.mnu_InsuranceCompanySetup_Company_Category_Click);
            // 
            // mnuPriorAuthReport
            // 
            this.mnuPriorAuthReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuPriorAuthReport.Image = ((System.Drawing.Image)(resources.GetObject("mnuPriorAuthReport.Image")));
            this.mnuPriorAuthReport.Name = "mnuPriorAuthReport";
            this.mnuPriorAuthReport.Size = new System.Drawing.Size(266, 22);
            this.mnuPriorAuthReport.Text = "Prior Authorization Review";
            this.mnuPriorAuthReport.Click += new System.EventHandler(this.mnuPriorAuthReport_Click);
            // 
            // mnuTransactionNotes
            // 
            this.mnuTransactionNotes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuTransactionNotes.Image = ((System.Drawing.Image)(resources.GetObject("mnuTransactionNotes.Image")));
            this.mnuTransactionNotes.Name = "mnuTransactionNotes";
            this.mnuTransactionNotes.Size = new System.Drawing.Size(266, 22);
            this.mnuTransactionNotes.Text = "Transaction Notes";
            this.mnuTransactionNotes.Visible = false;
            this.mnuTransactionNotes.Click += new System.EventHandler(this.mnuTransactionNotes_Click);
            // 
            // mnuTransactionHistoryAnalysis
            // 
            this.mnuTransactionHistoryAnalysis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuTransactionHistoryAnalysis.Image = ((System.Drawing.Image)(resources.GetObject("mnuTransactionHistoryAnalysis.Image")));
            this.mnuTransactionHistoryAnalysis.Name = "mnuTransactionHistoryAnalysis";
            this.mnuTransactionHistoryAnalysis.Size = new System.Drawing.Size(266, 22);
            this.mnuTransactionHistoryAnalysis.Text = "Transaction History Analysis";
            this.mnuTransactionHistoryAnalysis.Visible = false;
            this.mnuTransactionHistoryAnalysis.Click += new System.EventHandler(this.mnuTransactionHistoryAnalysis_Click);
            // 
            // mnu_patientReport
            // 
            this.mnu_patientReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_patientReport.Image = ((System.Drawing.Image)(resources.GetObject("mnu_patientReport.Image")));
            this.mnu_patientReport.Name = "mnu_patientReport";
            this.mnu_patientReport.Size = new System.Drawing.Size(266, 22);
            this.mnu_patientReport.Text = "Patient Report";
            this.mnu_patientReport.Click += new System.EventHandler(this.mnu_patientReport_Click);
            // 
            // mnuReports_ProviderReferral_Patients
            // 
            this.mnuReports_ProviderReferral_Patients.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_ProviderReferral_Patients.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_ProviderReferral_Patients.Image")));
            this.mnuReports_ProviderReferral_Patients.Name = "mnuReports_ProviderReferral_Patients";
            this.mnuReports_ProviderReferral_Patients.Size = new System.Drawing.Size(266, 22);
            this.mnuReports_ProviderReferral_Patients.Text = "Provider/Referrals - Patients";
            this.mnuReports_ProviderReferral_Patients.Click += new System.EventHandler(this.mnuReports_ProviderReferral_Patients_Click);
            // 
            // mnu_Reports_DplicateCliam
            // 
            this.mnu_Reports_DplicateCliam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_Reports_DplicateCliam.Image = ((System.Drawing.Image)(resources.GetObject("mnu_Reports_DplicateCliam.Image")));
            this.mnu_Reports_DplicateCliam.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.mnu_Reports_DplicateCliam.Name = "mnu_Reports_DplicateCliam";
            this.mnu_Reports_DplicateCliam.Size = new System.Drawing.Size(266, 22);
            this.mnu_Reports_DplicateCliam.Text = "Duplicate Claim Report";
            this.mnu_Reports_DplicateCliam.Click += new System.EventHandler(this.mnu_Reports_DuplicateCliam_Click);
            // 
            // mnu_ChargesPayments
            // 
            this.mnu_ChargesPayments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_ChargesPayments.Image = ((System.Drawing.Image)(resources.GetObject("mnu_ChargesPayments.Image")));
            this.mnu_ChargesPayments.Name = "mnu_ChargesPayments";
            this.mnu_ChargesPayments.Size = new System.Drawing.Size(266, 22);
            this.mnu_ChargesPayments.Text = "Cash Flow Report";
            this.mnu_ChargesPayments.Visible = false;
            this.mnu_ChargesPayments.Click += new System.EventHandler(this.mnu_ChargesPayments_Click);
            // 
            // mnu_PatientRecall
            // 
            this.mnu_PatientRecall.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_PatientRecall.Image = ((System.Drawing.Image)(resources.GetObject("mnu_PatientRecall.Image")));
            this.mnu_PatientRecall.Name = "mnu_PatientRecall";
            this.mnu_PatientRecall.Size = new System.Drawing.Size(266, 22);
            this.mnu_PatientRecall.Text = "Patient Recall";
            this.mnu_PatientRecall.Click += new System.EventHandler(this.mnu_PatientRecall_Click);
            // 
            // mnuReports_PrintLabels
            // 
            this.mnuReports_PrintLabels.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuReports_PrintLabels_Patient,
            this.mnuReports_PrintLabels_Insurance,
            this.mnuReports_PrintLabels_Contacts,
            this.mnuReports_PrintLabels_Employies});
            this.mnuReports_PrintLabels.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_PrintLabels.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_PrintLabels.Image")));
            this.mnuReports_PrintLabels.Name = "mnuReports_PrintLabels";
            this.mnuReports_PrintLabels.Size = new System.Drawing.Size(266, 22);
            this.mnuReports_PrintLabels.Text = "Print Labels";
            this.mnuReports_PrintLabels.Visible = false;
            this.mnuReports_PrintLabels.Click += new System.EventHandler(this.mnuReports_PrintLabels_Click);
            // 
            // mnuReports_PrintLabels_Patient
            // 
            this.mnuReports_PrintLabels_Patient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_PrintLabels_Patient.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_PrintLabels_Patient.Image")));
            this.mnuReports_PrintLabels_Patient.Name = "mnuReports_PrintLabels_Patient";
            this.mnuReports_PrintLabels_Patient.Size = new System.Drawing.Size(132, 22);
            this.mnuReports_PrintLabels_Patient.Text = "Patient";
            this.mnuReports_PrintLabels_Patient.Click += new System.EventHandler(this.mnuReports_PrintLabels_Patient_Click);
            // 
            // mnuReports_PrintLabels_Insurance
            // 
            this.mnuReports_PrintLabels_Insurance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_PrintLabels_Insurance.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_PrintLabels_Insurance.Image")));
            this.mnuReports_PrintLabels_Insurance.Name = "mnuReports_PrintLabels_Insurance";
            this.mnuReports_PrintLabels_Insurance.Size = new System.Drawing.Size(132, 22);
            this.mnuReports_PrintLabels_Insurance.Text = "Insurance ";
            this.mnuReports_PrintLabels_Insurance.Click += new System.EventHandler(this.mnuReports_PrintLabels_Insurance_Click);
            // 
            // mnuReports_PrintLabels_Contacts
            // 
            this.mnuReports_PrintLabels_Contacts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_PrintLabels_Contacts.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_PrintLabels_Contacts.Image")));
            this.mnuReports_PrintLabels_Contacts.Name = "mnuReports_PrintLabels_Contacts";
            this.mnuReports_PrintLabels_Contacts.Size = new System.Drawing.Size(132, 22);
            this.mnuReports_PrintLabels_Contacts.Text = "Contacts";
            this.mnuReports_PrintLabels_Contacts.Click += new System.EventHandler(this.mnuReports_PrintLabels_Contacts_Click);
            // 
            // mnuReports_PrintLabels_Employies
            // 
            this.mnuReports_PrintLabels_Employies.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_PrintLabels_Employies.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_PrintLabels_Employies.Image")));
            this.mnuReports_PrintLabels_Employies.Name = "mnuReports_PrintLabels_Employies";
            this.mnuReports_PrintLabels_Employies.Size = new System.Drawing.Size(132, 22);
            this.mnuReports_PrintLabels_Employies.Text = "Employees";
            this.mnuReports_PrintLabels_Employies.Click += new System.EventHandler(this.mnuReports_PrintLabels_Employies_Click);
            // 
            // mnuReports_PrintList
            // 
            this.mnuReports_PrintList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.patientToolStripMenuItem1,
            this.guarantorToolStripMenuItem,
            this.insurancesToolStripMenuItem,
            this.billingCodeToolStripMenuItem,
            this.diagnosisToolStripMenuItem});
            this.mnuReports_PrintList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_PrintList.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_PrintList.Image")));
            this.mnuReports_PrintList.Name = "mnuReports_PrintList";
            this.mnuReports_PrintList.Size = new System.Drawing.Size(266, 22);
            this.mnuReports_PrintList.Text = "Print List";
            this.mnuReports_PrintList.Click += new System.EventHandler(this.mnuReports_PrintList_Click);
            // 
            // patientToolStripMenuItem1
            // 
            this.patientToolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.patientToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("patientToolStripMenuItem1.Image")));
            this.patientToolStripMenuItem1.Name = "patientToolStripMenuItem1";
            this.patientToolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
            this.patientToolStripMenuItem1.Text = "Patient";
            this.patientToolStripMenuItem1.Click += new System.EventHandler(this.patientToolStripMenuItem1_Click);
            // 
            // guarantorToolStripMenuItem
            // 
            this.guarantorToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.guarantorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("guarantorToolStripMenuItem.Image")));
            this.guarantorToolStripMenuItem.Name = "guarantorToolStripMenuItem";
            this.guarantorToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.guarantorToolStripMenuItem.Text = "Guarantor";
            this.guarantorToolStripMenuItem.Click += new System.EventHandler(this.guarantorToolStripMenuItem_Click);
            // 
            // insurancesToolStripMenuItem
            // 
            this.insurancesToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.insurancesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("insurancesToolStripMenuItem.Image")));
            this.insurancesToolStripMenuItem.Name = "insurancesToolStripMenuItem";
            this.insurancesToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.insurancesToolStripMenuItem.Text = "Insurances";
            this.insurancesToolStripMenuItem.Click += new System.EventHandler(this.insurancesToolStripMenuItem_Click);
            // 
            // billingCodeToolStripMenuItem
            // 
            this.billingCodeToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.billingCodeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("billingCodeToolStripMenuItem.Image")));
            this.billingCodeToolStripMenuItem.Name = "billingCodeToolStripMenuItem";
            this.billingCodeToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.billingCodeToolStripMenuItem.Text = "Billing Code";
            this.billingCodeToolStripMenuItem.Click += new System.EventHandler(this.billingCodeToolStripMenuItem_Click);
            // 
            // diagnosisToolStripMenuItem
            // 
            this.diagnosisToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.diagnosisToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("diagnosisToolStripMenuItem.Image")));
            this.diagnosisToolStripMenuItem.Name = "diagnosisToolStripMenuItem";
            this.diagnosisToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.diagnosisToolStripMenuItem.Text = "Diagnosis";
            this.diagnosisToolStripMenuItem.Click += new System.EventHandler(this.diagnosisToolStripMenuItem_Click);
            // 
            // mnuReports_Graphs
            // 
            this.mnuReports_Graphs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_Graphs.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_Graphs.Image")));
            this.mnuReports_Graphs.Name = "mnuReports_Graphs";
            this.mnuReports_Graphs.Size = new System.Drawing.Size(266, 22);
            this.mnuReports_Graphs.Text = "Graphs";
            this.mnuReports_Graphs.Visible = false;
            this.mnuReports_Graphs.Click += new System.EventHandler(this.mnuReports_Graphs_Click);
            // 
            // mnuReports_ClaimStatus
            // 
            this.mnuReports_ClaimStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_ClaimStatus.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_ClaimStatus.Image")));
            this.mnuReports_ClaimStatus.Name = "mnuReports_ClaimStatus";
            this.mnuReports_ClaimStatus.Size = new System.Drawing.Size(266, 22);
            this.mnuReports_ClaimStatus.Text = "Claim Status";
            this.mnuReports_ClaimStatus.Visible = false;
            this.mnuReports_ClaimStatus.Click += new System.EventHandler(this.mnuReports_ClaimStatus_Click);
            // 
            // mnu_Reports_PatientByDOBReport
            // 
            this.mnu_Reports_PatientByDOBReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_Reports_PatientByDOBReport.Image = ((System.Drawing.Image)(resources.GetObject("mnu_Reports_PatientByDOBReport.Image")));
            this.mnu_Reports_PatientByDOBReport.Name = "mnu_Reports_PatientByDOBReport";
            this.mnu_Reports_PatientByDOBReport.Size = new System.Drawing.Size(266, 22);
            this.mnu_Reports_PatientByDOBReport.Text = "Patient By DOB Report";
            this.mnu_Reports_PatientByDOBReport.Click += new System.EventHandler(this.mnu_Reports_PatientByDOBReport_Click);
            // 
            // mnu_Reports_PatientExcludeSt
            // 
            this.mnu_Reports_PatientExcludeSt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_Reports_PatientExcludeSt.Image = ((System.Drawing.Image)(resources.GetObject("mnu_Reports_PatientExcludeSt.Image")));
            this.mnu_Reports_PatientExcludeSt.Name = "mnu_Reports_PatientExcludeSt";
            this.mnu_Reports_PatientExcludeSt.Size = new System.Drawing.Size(266, 22);
            this.mnu_Reports_PatientExcludeSt.Text = "Patients Excluded from Statement";
            this.mnu_Reports_PatientExcludeSt.Click += new System.EventHandler(this.mnu_Reports_PatientExcludeSt_Click);
            // 
            // mnu_Reports_BatchReport
            // 
            this.mnu_Reports_BatchReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnu_Reports_BatchReport.Image = ((System.Drawing.Image)(resources.GetObject("mnu_Reports_BatchReport.Image")));
            this.mnu_Reports_BatchReport.Name = "mnu_Reports_BatchReport";
            this.mnu_Reports_BatchReport.Size = new System.Drawing.Size(266, 22);
            this.mnu_Reports_BatchReport.Text = "Batch Report";
            this.mnu_Reports_BatchReport.Click += new System.EventHandler(this.mnu_Reports_BatchReport_Click);
            // 
            // payerListToolStripMenuItem
            // 
            this.payerListToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.payerListToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("payerListToolStripMenuItem.Image")));
            this.payerListToolStripMenuItem.Name = "payerListToolStripMenuItem";
            this.payerListToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.payerListToolStripMenuItem.Text = "Payer List";
            this.payerListToolStripMenuItem.Visible = false;
            // 
            // mnuReports_gatewayEDI
            // 
            this.mnuReports_gatewayEDI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reportsToolStripMenuItem,
            this.myAccountToolStripMenuItem,
            this.claimsStatusToolStripMenuItem,
            this.claimHistoryToolsToolStripMenuItem,
            this.remitsToolStripMenuItem,
            this.eligibilityToolStripMenuItem,
            this.takePaymentToolStripMenuItem,
            this.patientStatementsToolStripMenuItem,
            this.climsReviewedToolStripMenuItem2});
            this.mnuReports_gatewayEDI.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_gatewayEDI.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_gatewayEDI.Image")));
            this.mnuReports_gatewayEDI.Name = "mnuReports_gatewayEDI";
            this.mnuReports_gatewayEDI.Size = new System.Drawing.Size(266, 22);
            this.mnuReports_gatewayEDI.Tag = "gatewayEDI";
            this.mnuReports_gatewayEDI.Text = "Gateway EDI";
            this.mnuReports_gatewayEDI.Visible = false;
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewMyReportsToolStripMenuItem,
            this.listOfClaimsRecivedToolStripMenuItem,
            this.staffProductivityToolStripMenuItem,
            this.myFilesRecivedToolStripMenuItem});
            this.reportsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.reportsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("reportsToolStripMenuItem.Image")));
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // viewMyReportsToolStripMenuItem
            // 
            this.viewMyReportsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.viewMyReportsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("viewMyReportsToolStripMenuItem.Image")));
            this.viewMyReportsToolStripMenuItem.Name = "viewMyReportsToolStripMenuItem";
            this.viewMyReportsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.viewMyReportsToolStripMenuItem.Text = "View My Reports";
            this.viewMyReportsToolStripMenuItem.Click += new System.EventHandler(this.viewMyReportsToolStripMenuItem_Click);
            // 
            // listOfClaimsRecivedToolStripMenuItem
            // 
            this.listOfClaimsRecivedToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.listOfClaimsRecivedToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("listOfClaimsRecivedToolStripMenuItem.Image")));
            this.listOfClaimsRecivedToolStripMenuItem.Name = "listOfClaimsRecivedToolStripMenuItem";
            this.listOfClaimsRecivedToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.listOfClaimsRecivedToolStripMenuItem.Text = "List of Claims Recived";
            this.listOfClaimsRecivedToolStripMenuItem.Click += new System.EventHandler(this.listOfClaimsRecivedToolStripMenuItem_Click);
            // 
            // staffProductivityToolStripMenuItem
            // 
            this.staffProductivityToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.staffProductivityToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("staffProductivityToolStripMenuItem.Image")));
            this.staffProductivityToolStripMenuItem.Name = "staffProductivityToolStripMenuItem";
            this.staffProductivityToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.staffProductivityToolStripMenuItem.Text = "Staff Productivity";
            this.staffProductivityToolStripMenuItem.Click += new System.EventHandler(this.staffProductivityToolStripMenuItem_Click);
            // 
            // myFilesRecivedToolStripMenuItem
            // 
            this.myFilesRecivedToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.myFilesRecivedToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("myFilesRecivedToolStripMenuItem.Image")));
            this.myFilesRecivedToolStripMenuItem.Name = "myFilesRecivedToolStripMenuItem";
            this.myFilesRecivedToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.myFilesRecivedToolStripMenuItem.Text = "My Files Recived";
            this.myFilesRecivedToolStripMenuItem.Click += new System.EventHandler(this.myFilesRecivedToolStripMenuItem_Click);
            // 
            // myAccountToolStripMenuItem
            // 
            this.myAccountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myFileLocationsToolStripMenuItem,
            this.updateMyWebAccountToolStripMenuItem,
            this.changeMyPasswordToolStripMenuItem,
            this.viewProviderInfoToolStripMenuItem,
            this.referringProviderNPITableToolStripMenuItem,
            this.enrollmentToolStripMenuItem,
            this.providerEnrollmentFormToolStripMenuItem});
            this.myAccountToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.myAccountToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("myAccountToolStripMenuItem.Image")));
            this.myAccountToolStripMenuItem.Name = "myAccountToolStripMenuItem";
            this.myAccountToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.myAccountToolStripMenuItem.Text = "My Account";
            this.myAccountToolStripMenuItem.Visible = false;
            // 
            // myFileLocationsToolStripMenuItem
            // 
            this.myFileLocationsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.myFileLocationsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("myFileLocationsToolStripMenuItem.Image")));
            this.myFileLocationsToolStripMenuItem.Name = "myFileLocationsToolStripMenuItem";
            this.myFileLocationsToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.myFileLocationsToolStripMenuItem.Text = "My File Locations";
            // 
            // updateMyWebAccountToolStripMenuItem
            // 
            this.updateMyWebAccountToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.updateMyWebAccountToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("updateMyWebAccountToolStripMenuItem.Image")));
            this.updateMyWebAccountToolStripMenuItem.Name = "updateMyWebAccountToolStripMenuItem";
            this.updateMyWebAccountToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.updateMyWebAccountToolStripMenuItem.Text = "Update My Web Account";
            // 
            // changeMyPasswordToolStripMenuItem
            // 
            this.changeMyPasswordToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.changeMyPasswordToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("changeMyPasswordToolStripMenuItem.Image")));
            this.changeMyPasswordToolStripMenuItem.Name = "changeMyPasswordToolStripMenuItem";
            this.changeMyPasswordToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.changeMyPasswordToolStripMenuItem.Text = "Change My Password";
            // 
            // viewProviderInfoToolStripMenuItem
            // 
            this.viewProviderInfoToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.viewProviderInfoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("viewProviderInfoToolStripMenuItem.Image")));
            this.viewProviderInfoToolStripMenuItem.Name = "viewProviderInfoToolStripMenuItem";
            this.viewProviderInfoToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.viewProviderInfoToolStripMenuItem.Text = "View Provider Info";
            // 
            // referringProviderNPITableToolStripMenuItem
            // 
            this.referringProviderNPITableToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.referringProviderNPITableToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("referringProviderNPITableToolStripMenuItem.Image")));
            this.referringProviderNPITableToolStripMenuItem.Name = "referringProviderNPITableToolStripMenuItem";
            this.referringProviderNPITableToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.referringProviderNPITableToolStripMenuItem.Text = "Referring Provider NPI Table";
            // 
            // enrollmentToolStripMenuItem
            // 
            this.enrollmentToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.enrollmentToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("enrollmentToolStripMenuItem.Image")));
            this.enrollmentToolStripMenuItem.Name = "enrollmentToolStripMenuItem";
            this.enrollmentToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.enrollmentToolStripMenuItem.Text = "Enrollment Paperwork";
            // 
            // providerEnrollmentFormToolStripMenuItem
            // 
            this.providerEnrollmentFormToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.providerEnrollmentFormToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("providerEnrollmentFormToolStripMenuItem.Image")));
            this.providerEnrollmentFormToolStripMenuItem.Name = "providerEnrollmentFormToolStripMenuItem";
            this.providerEnrollmentFormToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.providerEnrollmentFormToolStripMenuItem.Text = "Provider Enrollment Form";
            // 
            // claimsStatusToolStripMenuItem
            // 
            this.claimsStatusToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.patientSearchToolStripMenuItem,
            this.providerSearchToolStripMenuItem,
            this.insurerSearchToolStripMenuItem,
            this.secondarySearchToolStripMenuItem,
            this.advancedSearchToolStripMenuItem,
            this.realtimeClaimStatusToolStripMenuItem,
            this.cCISuspensionsToolStripMenuItem});
            this.claimsStatusToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.claimsStatusToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("claimsStatusToolStripMenuItem.Image")));
            this.claimsStatusToolStripMenuItem.Name = "claimsStatusToolStripMenuItem";
            this.claimsStatusToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.claimsStatusToolStripMenuItem.Text = "Claims Status";
            // 
            // patientSearchToolStripMenuItem
            // 
            this.patientSearchToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.patientSearchToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("patientSearchToolStripMenuItem.Image")));
            this.patientSearchToolStripMenuItem.Name = "patientSearchToolStripMenuItem";
            this.patientSearchToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.patientSearchToolStripMenuItem.Text = "Patient Search";
            this.patientSearchToolStripMenuItem.Click += new System.EventHandler(this.patientSearchToolStripMenuItem_Click);
            // 
            // providerSearchToolStripMenuItem
            // 
            this.providerSearchToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.providerSearchToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("providerSearchToolStripMenuItem.Image")));
            this.providerSearchToolStripMenuItem.Name = "providerSearchToolStripMenuItem";
            this.providerSearchToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.providerSearchToolStripMenuItem.Text = "Provider Search";
            this.providerSearchToolStripMenuItem.Click += new System.EventHandler(this.providerSearchToolStripMenuItem_Click);
            // 
            // insurerSearchToolStripMenuItem
            // 
            this.insurerSearchToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.insurerSearchToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("insurerSearchToolStripMenuItem.Image")));
            this.insurerSearchToolStripMenuItem.Name = "insurerSearchToolStripMenuItem";
            this.insurerSearchToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.insurerSearchToolStripMenuItem.Text = "Insurer Search";
            this.insurerSearchToolStripMenuItem.Click += new System.EventHandler(this.insurerSearchToolStripMenuItem_Click);
            // 
            // secondarySearchToolStripMenuItem
            // 
            this.secondarySearchToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.secondarySearchToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("secondarySearchToolStripMenuItem.Image")));
            this.secondarySearchToolStripMenuItem.Name = "secondarySearchToolStripMenuItem";
            this.secondarySearchToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.secondarySearchToolStripMenuItem.Text = "Secondary Search";
            this.secondarySearchToolStripMenuItem.Click += new System.EventHandler(this.secondarySearchToolStripMenuItem_Click);
            // 
            // advancedSearchToolStripMenuItem
            // 
            this.advancedSearchToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.advancedSearchToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("advancedSearchToolStripMenuItem.Image")));
            this.advancedSearchToolStripMenuItem.Name = "advancedSearchToolStripMenuItem";
            this.advancedSearchToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.advancedSearchToolStripMenuItem.Text = "Advanced Search";
            this.advancedSearchToolStripMenuItem.Click += new System.EventHandler(this.advancedSearchToolStripMenuItem_Click);
            // 
            // realtimeClaimStatusToolStripMenuItem
            // 
            this.realtimeClaimStatusToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.realtimeClaimStatusToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("realtimeClaimStatusToolStripMenuItem.Image")));
            this.realtimeClaimStatusToolStripMenuItem.Name = "realtimeClaimStatusToolStripMenuItem";
            this.realtimeClaimStatusToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.realtimeClaimStatusToolStripMenuItem.Text = "Realtime Claim Status";
            this.realtimeClaimStatusToolStripMenuItem.Click += new System.EventHandler(this.realtimeClaimStatusToolStripMenuItem_Click);
            // 
            // cCISuspensionsToolStripMenuItem
            // 
            this.cCISuspensionsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cCISuspensionsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cCISuspensionsToolStripMenuItem.Image")));
            this.cCISuspensionsToolStripMenuItem.Name = "cCISuspensionsToolStripMenuItem";
            this.cCISuspensionsToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.cCISuspensionsToolStripMenuItem.Text = "CCI Suspensions";
            this.cCISuspensionsToolStripMenuItem.Click += new System.EventHandler(this.cCISuspensionsToolStripMenuItem_Click);
            // 
            // claimHistoryToolsToolStripMenuItem
            // 
            this.claimHistoryToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rejectionAnalysisToolStripMenuItem,
            this.transactionSummaryToolStripMenuItem,
            this.safetyNetReportToolStripMenuItem,
            this.claimsFileReconcliToolStripMenuItem,
            this.bestPracticesToolStripMenuItem});
            this.claimHistoryToolsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.claimHistoryToolsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("claimHistoryToolsToolStripMenuItem.Image")));
            this.claimHistoryToolsToolStripMenuItem.Name = "claimHistoryToolsToolStripMenuItem";
            this.claimHistoryToolsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.claimHistoryToolsToolStripMenuItem.Text = "Claim History Tools";
            // 
            // rejectionAnalysisToolStripMenuItem
            // 
            this.rejectionAnalysisToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.rejectionAnalysisToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("rejectionAnalysisToolStripMenuItem.Image")));
            this.rejectionAnalysisToolStripMenuItem.Name = "rejectionAnalysisToolStripMenuItem";
            this.rejectionAnalysisToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.rejectionAnalysisToolStripMenuItem.Text = "Rejection Analysis";
            this.rejectionAnalysisToolStripMenuItem.Click += new System.EventHandler(this.rejectionAnalysisToolStripMenuItem_Click);
            // 
            // transactionSummaryToolStripMenuItem
            // 
            this.transactionSummaryToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.transactionSummaryToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("transactionSummaryToolStripMenuItem.Image")));
            this.transactionSummaryToolStripMenuItem.Name = "transactionSummaryToolStripMenuItem";
            this.transactionSummaryToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.transactionSummaryToolStripMenuItem.Text = "Transaction Summary";
            this.transactionSummaryToolStripMenuItem.Click += new System.EventHandler(this.transactionSummaryToolStripMenuItem_Click);
            // 
            // safetyNetReportToolStripMenuItem
            // 
            this.safetyNetReportToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.safetyNetReportToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("safetyNetReportToolStripMenuItem.Image")));
            this.safetyNetReportToolStripMenuItem.Name = "safetyNetReportToolStripMenuItem";
            this.safetyNetReportToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.safetyNetReportToolStripMenuItem.Text = "Safety Net Report";
            this.safetyNetReportToolStripMenuItem.Click += new System.EventHandler(this.safetyNetReportToolStripMenuItem_Click);
            // 
            // claimsFileReconcliToolStripMenuItem
            // 
            this.claimsFileReconcliToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.claimsFileReconcliToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("claimsFileReconcliToolStripMenuItem.Image")));
            this.claimsFileReconcliToolStripMenuItem.Name = "claimsFileReconcliToolStripMenuItem";
            this.claimsFileReconcliToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.claimsFileReconcliToolStripMenuItem.Text = "Claims File Reconciliation";
            this.claimsFileReconcliToolStripMenuItem.Click += new System.EventHandler(this.claimsFileReconcliToolStripMenuItem_Click);
            // 
            // bestPracticesToolStripMenuItem
            // 
            this.bestPracticesToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.bestPracticesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("bestPracticesToolStripMenuItem.Image")));
            this.bestPracticesToolStripMenuItem.Name = "bestPracticesToolStripMenuItem";
            this.bestPracticesToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.bestPracticesToolStripMenuItem.Text = "Best Practices";
            this.bestPracticesToolStripMenuItem.Click += new System.EventHandler(this.bestPracticesToolStripMenuItem_Click);
            // 
            // remitsToolStripMenuItem
            // 
            this.remitsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.remitsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("remitsToolStripMenuItem.Image")));
            this.remitsToolStripMenuItem.Name = "remitsToolStripMenuItem";
            this.remitsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.remitsToolStripMenuItem.Text = "Remits";
            this.remitsToolStripMenuItem.Visible = false;
            // 
            // eligibilityToolStripMenuItem
            // 
            this.eligibilityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.batchInquiriesToolStripMenuItem,
            this.newRealtimeInquiriesToolStripMenuItem});
            this.eligibilityToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.eligibilityToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("eligibilityToolStripMenuItem.Image")));
            this.eligibilityToolStripMenuItem.Name = "eligibilityToolStripMenuItem";
            this.eligibilityToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.eligibilityToolStripMenuItem.Text = "Eligibility";
            this.eligibilityToolStripMenuItem.Visible = false;
            // 
            // batchInquiriesToolStripMenuItem
            // 
            this.batchInquiriesToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.batchInquiriesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("batchInquiriesToolStripMenuItem.Image")));
            this.batchInquiriesToolStripMenuItem.Name = "batchInquiriesToolStripMenuItem";
            this.batchInquiriesToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.batchInquiriesToolStripMenuItem.Text = "Batch Inquiries";
            // 
            // newRealtimeInquiriesToolStripMenuItem
            // 
            this.newRealtimeInquiriesToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.newRealtimeInquiriesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newRealtimeInquiriesToolStripMenuItem.Image")));
            this.newRealtimeInquiriesToolStripMenuItem.Name = "newRealtimeInquiriesToolStripMenuItem";
            this.newRealtimeInquiriesToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.newRealtimeInquiriesToolStripMenuItem.Text = "New Realtime Inquiries";
            // 
            // takePaymentToolStripMenuItem
            // 
            this.takePaymentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.takeCreditCardPaymentToolStripMenuItem,
            this.takeACHPaymentToolStripMenuItem,
            this.managePaymentsToolStripMenuItem,
            this.cCAuthorizationFormToolStripMenuItem,
            this.aCHAuthorizationFormToolStripMenuItem});
            this.takePaymentToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.takePaymentToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("takePaymentToolStripMenuItem.Image")));
            this.takePaymentToolStripMenuItem.Name = "takePaymentToolStripMenuItem";
            this.takePaymentToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.takePaymentToolStripMenuItem.Text = "Take Payment";
            this.takePaymentToolStripMenuItem.Visible = false;
            // 
            // takeCreditCardPaymentToolStripMenuItem
            // 
            this.takeCreditCardPaymentToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.takeCreditCardPaymentToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("takeCreditCardPaymentToolStripMenuItem.Image")));
            this.takeCreditCardPaymentToolStripMenuItem.Name = "takeCreditCardPaymentToolStripMenuItem";
            this.takeCreditCardPaymentToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.takeCreditCardPaymentToolStripMenuItem.Text = "Take Credit Card Payment";
            // 
            // takeACHPaymentToolStripMenuItem
            // 
            this.takeACHPaymentToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.takeACHPaymentToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("takeACHPaymentToolStripMenuItem.Image")));
            this.takeACHPaymentToolStripMenuItem.Name = "takeACHPaymentToolStripMenuItem";
            this.takeACHPaymentToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.takeACHPaymentToolStripMenuItem.Text = "Take ACH Payment";
            // 
            // managePaymentsToolStripMenuItem
            // 
            this.managePaymentsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.managePaymentsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("managePaymentsToolStripMenuItem.Image")));
            this.managePaymentsToolStripMenuItem.Name = "managePaymentsToolStripMenuItem";
            this.managePaymentsToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.managePaymentsToolStripMenuItem.Text = "Manage Payments";
            // 
            // cCAuthorizationFormToolStripMenuItem
            // 
            this.cCAuthorizationFormToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cCAuthorizationFormToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cCAuthorizationFormToolStripMenuItem.Image")));
            this.cCAuthorizationFormToolStripMenuItem.Name = "cCAuthorizationFormToolStripMenuItem";
            this.cCAuthorizationFormToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.cCAuthorizationFormToolStripMenuItem.Text = "CC Authorization Form";
            // 
            // aCHAuthorizationFormToolStripMenuItem
            // 
            this.aCHAuthorizationFormToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.aCHAuthorizationFormToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aCHAuthorizationFormToolStripMenuItem.Image")));
            this.aCHAuthorizationFormToolStripMenuItem.Name = "aCHAuthorizationFormToolStripMenuItem";
            this.aCHAuthorizationFormToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.aCHAuthorizationFormToolStripMenuItem.Text = "ACH Authorization Form";
            // 
            // patientStatementsToolStripMenuItem
            // 
            this.patientStatementsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sampleStatementsToolStripMenuItem});
            this.patientStatementsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.patientStatementsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("patientStatementsToolStripMenuItem.Image")));
            this.patientStatementsToolStripMenuItem.Name = "patientStatementsToolStripMenuItem";
            this.patientStatementsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.patientStatementsToolStripMenuItem.Text = "Patient Statements";
            this.patientStatementsToolStripMenuItem.Visible = false;
            // 
            // sampleStatementsToolStripMenuItem
            // 
            this.sampleStatementsToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.sampleStatementsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sampleStatementsToolStripMenuItem.Image")));
            this.sampleStatementsToolStripMenuItem.Name = "sampleStatementsToolStripMenuItem";
            this.sampleStatementsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.sampleStatementsToolStripMenuItem.Text = "Sample Statements";
            // 
            // climsReviewedToolStripMenuItem2
            // 
            this.climsReviewedToolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.climsReviewedToolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("climsReviewedToolStripMenuItem2.Image")));
            this.climsReviewedToolStripMenuItem2.Name = "climsReviewedToolStripMenuItem2";
            this.climsReviewedToolStripMenuItem2.Size = new System.Drawing.Size(181, 22);
            this.climsReviewedToolStripMenuItem2.Text = "Claim Reviewed";
            this.climsReviewedToolStripMenuItem2.Click += new System.EventHandler(this.claimsReviewedToolStripMenuItem_Click);
            // 
            // mnuReports_AuditTrail
            // 
            this.mnuReports_AuditTrail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_AuditTrail.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_AuditTrail.Image")));
            this.mnuReports_AuditTrail.Name = "mnuReports_AuditTrail";
            this.mnuReports_AuditTrail.Size = new System.Drawing.Size(266, 22);
            this.mnuReports_AuditTrail.Text = "Audit Trail";
            this.mnuReports_AuditTrail.Click += new System.EventHandler(this.mnuReports_AuditTrail_Click);
            // 
            // mnuInterfaceReports
            // 
            this.mnuInterfaceReports.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuInterfaceReports.Image = ((System.Drawing.Image)(resources.GetObject("mnuInterfaceReports.Image")));
            this.mnuInterfaceReports.Name = "mnuInterfaceReports";
            this.mnuInterfaceReports.Size = new System.Drawing.Size(266, 22);
            this.mnuInterfaceReports.Text = "Interface Reports";
            this.mnuInterfaceReports.Click += new System.EventHandler(this.mnuInterfaceReports_Click);
            // 
            // mnuPatientActivationtReport
            // 
            this.mnuPatientActivationtReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuPatientActivationtReport.Image = ((System.Drawing.Image)(resources.GetObject("mnuPatientActivationtReport.Image")));
            this.mnuPatientActivationtReport.Name = "mnuPatientActivationtReport";
            this.mnuPatientActivationtReport.Size = new System.Drawing.Size(266, 22);
            this.mnuPatientActivationtReport.Text = "Patient Activation Report";
            this.mnuPatientActivationtReport.Click += new System.EventHandler(this.mnuPatientActivationtReport_Click);
            // 
            // mnuReimbursementWarning
            // 
            this.mnuReimbursementWarning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReimbursementWarning.Image = ((System.Drawing.Image)(resources.GetObject("mnuReimbursementWarning.Image")));
            this.mnuReimbursementWarning.Name = "mnuReimbursementWarning";
            this.mnuReimbursementWarning.Size = new System.Drawing.Size(266, 22);
            this.mnuReimbursementWarning.Text = "Reimbursement Warning";
            this.mnuReimbursementWarning.Click += new System.EventHandler(this.mnuReimbursementWarning_Click);
            // 
            // mnuBusCenterMismatch
            // 
            this.mnuBusCenterMismatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuBusCenterMismatch.Image = ((System.Drawing.Image)(resources.GetObject("mnuBusCenterMismatch.Image")));
            this.mnuBusCenterMismatch.Name = "mnuBusCenterMismatch";
            this.mnuBusCenterMismatch.Size = new System.Drawing.Size(266, 22);
            this.mnuBusCenterMismatch.Text = "Business Center Mismatch";
            this.mnuBusCenterMismatch.Click += new System.EventHandler(this.mnuBusCenterMismatch_Click);
            // 
            // mnuChargeLagReport
            // 
            this.mnuChargeLagReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuChargeLagReport.Image = ((System.Drawing.Image)(resources.GetObject("mnuChargeLagReport.Image")));
            this.mnuChargeLagReport.Name = "mnuChargeLagReport";
            this.mnuChargeLagReport.Size = new System.Drawing.Size(266, 22);
            this.mnuChargeLagReport.Text = "Charge Lag Report";
            this.mnuChargeLagReport.Click += new System.EventHandler(this.mnuChargeLagReport_Click);
            // 
            // mnuBatchLagReport
            // 
            this.mnuBatchLagReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuBatchLagReport.Image = ((System.Drawing.Image)(resources.GetObject("mnuBatchLagReport.Image")));
            this.mnuBatchLagReport.Name = "mnuBatchLagReport";
            this.mnuBatchLagReport.Size = new System.Drawing.Size(266, 22);
            this.mnuBatchLagReport.Text = "Batch Lag Report";
            this.mnuBatchLagReport.Click += new System.EventHandler(this.mnuBatchLagReport_Click);
            // 
            // mnuReports_InactiveCPTSReport
            // 
            this.mnuReports_InactiveCPTSReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_InactiveCPTSReport.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_InactiveCPTSReport.Image")));
            this.mnuReports_InactiveCPTSReport.Name = "mnuReports_InactiveCPTSReport";
            this.mnuReports_InactiveCPTSReport.Size = new System.Drawing.Size(266, 22);
            this.mnuReports_InactiveCPTSReport.Text = "Inactive CPTs Report ";
            this.mnuReports_InactiveCPTSReport.Click += new System.EventHandler(this.mnuReports_InactiveCPTSReport_Click);
            // 
            // mnuReports_ChargeEditReport
            // 
            this.mnuReports_ChargeEditReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_ChargeEditReport.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_ChargeEditReport.Image")));
            this.mnuReports_ChargeEditReport.Name = "mnuReports_ChargeEditReport";
            this.mnuReports_ChargeEditReport.Size = new System.Drawing.Size(266, 22);
            this.mnuReports_ChargeEditReport.Text = "Charge Edit Report";
            this.mnuReports_ChargeEditReport.Visible = false;
            this.mnuReports_ChargeEditReport.Click += new System.EventHandler(this.mnuReports_ChargeEditReport_Click);
            // 
            // mnuReports_CollectionExport
            // 
            this.mnuReports_CollectionExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuReports_CollectionExport.Image = ((System.Drawing.Image)(resources.GetObject("mnuReports_CollectionExport.Image")));
            this.mnuReports_CollectionExport.Name = "mnuReports_CollectionExport";
            this.mnuReports_CollectionExport.Size = new System.Drawing.Size(266, 22);
            this.mnuReports_CollectionExport.Text = "Collection Export";
            this.mnuReports_CollectionExport.Click += new System.EventHandler(this.mnuReports_CollectionExport_Click);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTools_Import,
            this.mnuTools_Export,
            this.mnuTools_UpdateTemplates,
            this.toolStripSeparator1,
            this.mnuTools_Synchronize,
            this.blockUnblockToolStripMenuItem,
            this.mnuTools_MergePatient,
            this.mnuTools_MergePatientAccount,
            this.mnuTools_CardImage,
            this.mnuTools_UnLockRecords,
            this.mnuICDAnalysis,
            this.mnuTools_CodeGuide,
            this.mnuTools_MergeScheduledActions,
            this.mnuTools_ChangePassword});
            this.mnuTools.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuTools.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(48, 22);
            this.mnuTools.Text = "&Tools";
            // 
            // mnuTools_Import
            // 
            this.mnuTools_Import.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuTools_Import.Image = ((System.Drawing.Image)(resources.GetObject("mnuTools_Import.Image")));
            this.mnuTools_Import.Name = "mnuTools_Import";
            this.mnuTools_Import.Size = new System.Drawing.Size(288, 22);
            this.mnuTools_Import.Text = "Import Templates";
            this.mnuTools_Import.Click += new System.EventHandler(this.mnuTools_Import_Click);
            // 
            // mnuTools_Export
            // 
            this.mnuTools_Export.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuTools_Export.Image = ((System.Drawing.Image)(resources.GetObject("mnuTools_Export.Image")));
            this.mnuTools_Export.Name = "mnuTools_Export";
            this.mnuTools_Export.Size = new System.Drawing.Size(288, 22);
            this.mnuTools_Export.Text = "Export Templates";
            this.mnuTools_Export.Click += new System.EventHandler(this.mnuTools_Export_Click);
            // 
            // mnuTools_UpdateTemplates
            // 
            this.mnuTools_UpdateTemplates.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuTools_UpdateTemplates.Image = ((System.Drawing.Image)(resources.GetObject("mnuTools_UpdateTemplates.Image")));
            this.mnuTools_UpdateTemplates.Name = "mnuTools_UpdateTemplates";
            this.mnuTools_UpdateTemplates.Size = new System.Drawing.Size(288, 22);
            this.mnuTools_UpdateTemplates.Text = "Update Templates";
            this.mnuTools_UpdateTemplates.Click += new System.EventHandler(this.mnuTools_UpdateTemplates_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(285, 6);
            // 
            // mnuTools_Synchronize
            // 
            this.mnuTools_Synchronize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuTools_Synchronize.Image = ((System.Drawing.Image)(resources.GetObject("mnuTools_Synchronize.Image")));
            this.mnuTools_Synchronize.Name = "mnuTools_Synchronize";
            this.mnuTools_Synchronize.Size = new System.Drawing.Size(288, 22);
            this.mnuTools_Synchronize.Text = "Synchronize";
            this.mnuTools_Synchronize.Visible = false;
            this.mnuTools_Synchronize.Click += new System.EventHandler(this.mnuTools_Synchronize_Click);
            // 
            // blockUnblockToolStripMenuItem
            // 
            this.blockUnblockToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.blockUnblockToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("blockUnblockToolStripMenuItem.Image")));
            this.blockUnblockToolStripMenuItem.Name = "blockUnblockToolStripMenuItem";
            this.blockUnblockToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.blockUnblockToolStripMenuItem.Text = "Block Unblock";
            this.blockUnblockToolStripMenuItem.Visible = false;
            this.blockUnblockToolStripMenuItem.Click += new System.EventHandler(this.blockUnblockToolStripMenuItem_Click);
            // 
            // mnuTools_MergePatient
            // 
            this.mnuTools_MergePatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuTools_MergePatient.Image = ((System.Drawing.Image)(resources.GetObject("mnuTools_MergePatient.Image")));
            this.mnuTools_MergePatient.Name = "mnuTools_MergePatient";
            this.mnuTools_MergePatient.Size = new System.Drawing.Size(288, 22);
            this.mnuTools_MergePatient.Text = "Merge Patient";
            this.mnuTools_MergePatient.Click += new System.EventHandler(this.mnuTools_MergePatient_Click);
            // 
            // mnuTools_MergePatientAccount
            // 
            this.mnuTools_MergePatientAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuTools_MergePatientAccount.Image = ((System.Drawing.Image)(resources.GetObject("mnuTools_MergePatientAccount.Image")));
            this.mnuTools_MergePatientAccount.Name = "mnuTools_MergePatientAccount";
            this.mnuTools_MergePatientAccount.Size = new System.Drawing.Size(288, 22);
            this.mnuTools_MergePatientAccount.Text = "Merge Patient Account";
            this.mnuTools_MergePatientAccount.Visible = false;
            this.mnuTools_MergePatientAccount.Click += new System.EventHandler(this.mnuTools_MergePatientAccount_Click);
            // 
            // mnuTools_CardImage
            // 
            this.mnuTools_CardImage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuTools_CardImage.Image = ((System.Drawing.Image)(resources.GetObject("mnuTools_CardImage.Image")));
            this.mnuTools_CardImage.Name = "mnuTools_CardImage";
            this.mnuTools_CardImage.Size = new System.Drawing.Size(288, 22);
            this.mnuTools_CardImage.Text = "Card Image";
            this.mnuTools_CardImage.Click += new System.EventHandler(this.mnuTools_CardImage_Click);
            // 
            // mnuTools_UnLockRecords
            // 
            this.mnuTools_UnLockRecords.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuTools_UnLockRecords.Image = ((System.Drawing.Image)(resources.GetObject("mnuTools_UnLockRecords.Image")));
            this.mnuTools_UnLockRecords.Name = "mnuTools_UnLockRecords";
            this.mnuTools_UnLockRecords.Size = new System.Drawing.Size(288, 22);
            this.mnuTools_UnLockRecords.Text = "Unlock Records";
            this.mnuTools_UnLockRecords.Click += new System.EventHandler(this.mnuTools_UnLockRecords_Click);
            // 
            // mnuICDAnalysis
            // 
            this.mnuICDAnalysis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuICDAnalysis.Image = ((System.Drawing.Image)(resources.GetObject("mnuICDAnalysis.Image")));
            this.mnuICDAnalysis.Name = "mnuICDAnalysis";
            this.mnuICDAnalysis.Size = new System.Drawing.Size(288, 22);
            this.mnuICDAnalysis.Text = "ICD9 Usage and ICD10 Mapping Report";
            this.mnuICDAnalysis.Click += new System.EventHandler(this.mnuICDAnalysis_Click);
            // 
            // mnuTools_CodeGuide
            // 
            this.mnuTools_CodeGuide.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuTools_CodeGuide.Image = ((System.Drawing.Image)(resources.GetObject("mnuTools_CodeGuide.Image")));
            this.mnuTools_CodeGuide.Name = "mnuTools_CodeGuide";
            this.mnuTools_CodeGuide.Size = new System.Drawing.Size(288, 22);
            this.mnuTools_CodeGuide.Text = "Code Guide";
            this.mnuTools_CodeGuide.Click += new System.EventHandler(this.mnuTools_CodeGuide_Click);
            // 
            // mnuTools_MergeScheduledActions
            // 
            this.mnuTools_MergeScheduledActions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuTools_MergeScheduledActions.Image = ((System.Drawing.Image)(resources.GetObject("mnuTools_MergeScheduledActions.Image")));
            this.mnuTools_MergeScheduledActions.Name = "mnuTools_MergeScheduledActions";
            this.mnuTools_MergeScheduledActions.Size = new System.Drawing.Size(288, 22);
            this.mnuTools_MergeScheduledActions.Text = "Merge Scheduled Actions";
            this.mnuTools_MergeScheduledActions.Click += new System.EventHandler(this.mnuTools_MergeScheduledActions_Click);
            // 
            // mnuTools_ChangePassword
            // 
            this.mnuTools_ChangePassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuTools_ChangePassword.Image = ((System.Drawing.Image)(resources.GetObject("mnuTools_ChangePassword.Image")));
            this.mnuTools_ChangePassword.Name = "mnuTools_ChangePassword";
            this.mnuTools_ChangePassword.Size = new System.Drawing.Size(288, 22);
            this.mnuTools_ChangePassword.Text = "Change Password";
            this.mnuTools_ChangePassword.Click += new System.EventHandler(this.mnuTools_ChangePassword_Click);
            // 
            // mnuSecurity
            // 
            this.mnuSecurity.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSecurity_UserManagement,
            this.mnuSecurity_SystemManagemnet,
            this.toolStripSeparator21,
            this.mnuSecurity_PasswordPolicy,
            this.mnuSecurity_PatientLog,
            this.toolStripSeparator22,
            this.mnuSecurity_Forms});
            this.mnuSecurity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuSecurity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuSecurity.Name = "mnuSecurity";
            this.mnuSecurity.Size = new System.Drawing.Size(63, 22);
            this.mnuSecurity.Text = "Sec&urity";
            this.mnuSecurity.Visible = false;
            // 
            // mnuSecurity_UserManagement
            // 
            this.mnuSecurity_UserManagement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuSecurity_UserManagement.Image = ((System.Drawing.Image)(resources.GetObject("mnuSecurity_UserManagement.Image")));
            this.mnuSecurity_UserManagement.Name = "mnuSecurity_UserManagement";
            this.mnuSecurity_UserManagement.Size = new System.Drawing.Size(189, 22);
            this.mnuSecurity_UserManagement.Text = "User Management";
            this.mnuSecurity_UserManagement.Click += new System.EventHandler(this.mnuSecurity_UserManagement_Click);
            // 
            // mnuSecurity_SystemManagemnet
            // 
            this.mnuSecurity_SystemManagemnet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuSecurity_SystemManagemnet.Image = ((System.Drawing.Image)(resources.GetObject("mnuSecurity_SystemManagemnet.Image")));
            this.mnuSecurity_SystemManagemnet.Name = "mnuSecurity_SystemManagemnet";
            this.mnuSecurity_SystemManagemnet.Size = new System.Drawing.Size(189, 22);
            this.mnuSecurity_SystemManagemnet.Text = "System Management";
            this.mnuSecurity_SystemManagemnet.Click += new System.EventHandler(this.mnuSecurity_SystemManagemnet_Click);
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(186, 6);
            // 
            // mnuSecurity_PasswordPolicy
            // 
            this.mnuSecurity_PasswordPolicy.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.mnuSecurity_PasswordPolicy.Image = ((System.Drawing.Image)(resources.GetObject("mnuSecurity_PasswordPolicy.Image")));
            this.mnuSecurity_PasswordPolicy.Name = "mnuSecurity_PasswordPolicy";
            this.mnuSecurity_PasswordPolicy.Size = new System.Drawing.Size(189, 22);
            this.mnuSecurity_PasswordPolicy.Text = "Password Policy";
            this.mnuSecurity_PasswordPolicy.Click += new System.EventHandler(this.mnuSecurity_PasswordPolicy_Click);
            // 
            // mnuSecurity_PatientLog
            // 
            this.mnuSecurity_PatientLog.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.mnuSecurity_PatientLog.Image = ((System.Drawing.Image)(resources.GetObject("mnuSecurity_PatientLog.Image")));
            this.mnuSecurity_PatientLog.Name = "mnuSecurity_PatientLog";
            this.mnuSecurity_PatientLog.Size = new System.Drawing.Size(189, 22);
            this.mnuSecurity_PatientLog.Text = "Patient Log";
            this.mnuSecurity_PatientLog.Click += new System.EventHandler(this.mnuSecurity_PatientLog_Click);
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new System.Drawing.Size(186, 6);
            // 
            // mnuSecurity_Forms
            // 
            this.mnuSecurity_Forms.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuSecurity_Forms.Image = ((System.Drawing.Image)(resources.GetObject("mnuSecurity_Forms.Image")));
            this.mnuSecurity_Forms.Name = "mnuSecurity_Forms";
            this.mnuSecurity_Forms.Size = new System.Drawing.Size(189, 22);
            this.mnuSecurity_Forms.Text = "Forms";
            this.mnuSecurity_Forms.Click += new System.EventHandler(this.mnuSecurity_Forms_Click);
            // 
            // mnuSetting
            // 
            this.mnuSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSetting_DefaultDashboard,
            this.mnuSetting_DefaultPatientSetting,
            this.mnuSetting_SystemSetting,
            this.toolStripSeparator7,
            this.mnuSetting_Appointment,
            this.mnuSetting_Schedule,
            this.mnuSetting_Billing,
            this.toolStripSeparator8,
            this.mnuSetting_CardScanner,
            this.mnuSetting_RefreshDevicesPrinters,
            this.mnuSetting_Printer,
            this.toolStripSeparator24,
            this.mnuSetting_Theme2003,
            this.mnuSetting_Theme2003Dark,
            this.mnuSetting_Theme2007,
            this.mnuSetting_Customization});
            this.mnuSetting.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuSetting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuSetting.Name = "mnuSetting";
            this.mnuSetting.Size = new System.Drawing.Size(64, 22);
            this.mnuSetting.Text = "&Settings";
            // 
            // mnuSetting_DefaultDashboard
            // 
            this.mnuSetting_DefaultDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuSetting_DefaultDashboard.Image = ((System.Drawing.Image)(resources.GetObject("mnuSetting_DefaultDashboard.Image")));
            this.mnuSetting_DefaultDashboard.Name = "mnuSetting_DefaultDashboard";
            this.mnuSetting_DefaultDashboard.Size = new System.Drawing.Size(260, 22);
            this.mnuSetting_DefaultDashboard.Text = "Restore Dashboard Settings";
            this.mnuSetting_DefaultDashboard.Click += new System.EventHandler(this.mnuSetting_DefaultDashboard_Click);
            // 
            // mnuSetting_DefaultPatientSetting
            // 
            this.mnuSetting_DefaultPatientSetting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuSetting_DefaultPatientSetting.Image = ((System.Drawing.Image)(resources.GetObject("mnuSetting_DefaultPatientSetting.Image")));
            this.mnuSetting_DefaultPatientSetting.Name = "mnuSetting_DefaultPatientSetting";
            this.mnuSetting_DefaultPatientSetting.Size = new System.Drawing.Size(260, 22);
            this.mnuSetting_DefaultPatientSetting.Text = "Default Patient Setting";
            this.mnuSetting_DefaultPatientSetting.Visible = false;
            this.mnuSetting_DefaultPatientSetting.Click += new System.EventHandler(this.mnuSetting_DefaultPatientSetting_Click);
            // 
            // mnuSetting_SystemSetting
            // 
            this.mnuSetting_SystemSetting.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuSetting_SystemSetting.Image = ((System.Drawing.Image)(resources.GetObject("mnuSetting_SystemSetting.Image")));
            this.mnuSetting_SystemSetting.Name = "mnuSetting_SystemSetting";
            this.mnuSetting_SystemSetting.Size = new System.Drawing.Size(260, 22);
            this.mnuSetting_SystemSetting.Text = "System Setting";
            this.mnuSetting_SystemSetting.Click += new System.EventHandler(this.mnuSetting_SystemSetting_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(257, 6);
            this.toolStripSeparator7.Visible = false;
            // 
            // mnuSetting_Appointment
            // 
            this.mnuSetting_Appointment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuSetting_Appointment.Image = ((System.Drawing.Image)(resources.GetObject("mnuSetting_Appointment.Image")));
            this.mnuSetting_Appointment.Name = "mnuSetting_Appointment";
            this.mnuSetting_Appointment.Size = new System.Drawing.Size(260, 22);
            this.mnuSetting_Appointment.Text = "Appointment";
            this.mnuSetting_Appointment.Visible = false;
            this.mnuSetting_Appointment.Click += new System.EventHandler(this.mnuSetting_Appointment_Click);
            // 
            // mnuSetting_Schedule
            // 
            this.mnuSetting_Schedule.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuSetting_Schedule.Image = ((System.Drawing.Image)(resources.GetObject("mnuSetting_Schedule.Image")));
            this.mnuSetting_Schedule.Name = "mnuSetting_Schedule";
            this.mnuSetting_Schedule.Size = new System.Drawing.Size(260, 22);
            this.mnuSetting_Schedule.Text = "Schedule";
            this.mnuSetting_Schedule.Visible = false;
            this.mnuSetting_Schedule.Click += new System.EventHandler(this.mnuSetting_Schedule_Click);
            // 
            // mnuSetting_Billing
            // 
            this.mnuSetting_Billing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuSetting_Billing.Image = ((System.Drawing.Image)(resources.GetObject("mnuSetting_Billing.Image")));
            this.mnuSetting_Billing.Name = "mnuSetting_Billing";
            this.mnuSetting_Billing.Size = new System.Drawing.Size(260, 22);
            this.mnuSetting_Billing.Text = "Billing";
            this.mnuSetting_Billing.Visible = false;
            this.mnuSetting_Billing.Click += new System.EventHandler(this.mnuSetting_Billing_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(257, 6);
            // 
            // mnuSetting_CardScanner
            // 
            this.mnuSetting_CardScanner.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuSetting_CardScanner.Image = ((System.Drawing.Image)(resources.GetObject("mnuSetting_CardScanner.Image")));
            this.mnuSetting_CardScanner.Name = "mnuSetting_CardScanner";
            this.mnuSetting_CardScanner.Size = new System.Drawing.Size(260, 22);
            this.mnuSetting_CardScanner.Text = "Card Scanner";
            this.mnuSetting_CardScanner.Click += new System.EventHandler(this.mnuSetting_CardScanner_Click);
            // 
            // mnuSetting_RefreshDevicesPrinters
            // 
            this.mnuSetting_RefreshDevicesPrinters.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuSetting_RefreshDevicesPrinters.Image = ((System.Drawing.Image)(resources.GetObject("mnuSetting_RefreshDevicesPrinters.Image")));
            this.mnuSetting_RefreshDevicesPrinters.Name = "mnuSetting_RefreshDevicesPrinters";
            this.mnuSetting_RefreshDevicesPrinters.Size = new System.Drawing.Size(260, 22);
            this.mnuSetting_RefreshDevicesPrinters.Text = "Refresh Local Devices and Printers";
            this.mnuSetting_RefreshDevicesPrinters.Click += new System.EventHandler(this.mnuSetting_RefreshDevicesPrinters_Click);
            // 
            // mnuSetting_Printer
            // 
            this.mnuSetting_Printer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuSetting_Printer.Image = ((System.Drawing.Image)(resources.GetObject("mnuSetting_Printer.Image")));
            this.mnuSetting_Printer.Name = "mnuSetting_Printer";
            this.mnuSetting_Printer.Size = new System.Drawing.Size(260, 22);
            this.mnuSetting_Printer.Text = "Printer";
            this.mnuSetting_Printer.Visible = false;
            this.mnuSetting_Printer.Click += new System.EventHandler(this.mnuSetting_Printer_Click);
            // 
            // toolStripSeparator24
            // 
            this.toolStripSeparator24.Name = "toolStripSeparator24";
            this.toolStripSeparator24.Size = new System.Drawing.Size(257, 6);
            // 
            // mnuSetting_Theme2003
            // 
            this.mnuSetting_Theme2003.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuSetting_Theme2003.Image = ((System.Drawing.Image)(resources.GetObject("mnuSetting_Theme2003.Image")));
            this.mnuSetting_Theme2003.Name = "mnuSetting_Theme2003";
            this.mnuSetting_Theme2003.Size = new System.Drawing.Size(260, 22);
            this.mnuSetting_Theme2003.Text = "Office 2003 Theme";
            this.mnuSetting_Theme2003.Click += new System.EventHandler(this.mnuSetting_Theme2003_Click);
            // 
            // mnuSetting_Theme2003Dark
            // 
            this.mnuSetting_Theme2003Dark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuSetting_Theme2003Dark.Image = ((System.Drawing.Image)(resources.GetObject("mnuSetting_Theme2003Dark.Image")));
            this.mnuSetting_Theme2003Dark.Name = "mnuSetting_Theme2003Dark";
            this.mnuSetting_Theme2003Dark.Size = new System.Drawing.Size(260, 22);
            this.mnuSetting_Theme2003Dark.Text = "Office 2003 Dark Theme";
            this.mnuSetting_Theme2003Dark.Click += new System.EventHandler(this.mnuSetting_Theme2003Dark_Click);
            // 
            // mnuSetting_Theme2007
            // 
            this.mnuSetting_Theme2007.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuSetting_Theme2007.Image = ((System.Drawing.Image)(resources.GetObject("mnuSetting_Theme2007.Image")));
            this.mnuSetting_Theme2007.Name = "mnuSetting_Theme2007";
            this.mnuSetting_Theme2007.Size = new System.Drawing.Size(260, 22);
            this.mnuSetting_Theme2007.Text = "Office 2007 Theme";
            this.mnuSetting_Theme2007.Click += new System.EventHandler(this.mnuSetting_Theme2007_Click);
            // 
            // mnuSetting_Customization
            // 
            this.mnuSetting_Customization.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.mnuSetting_Customization.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuSetting_Customization.Image = ((System.Drawing.Image)(resources.GetObject("mnuSetting_Customization.Image")));
            this.mnuSetting_Customization.Name = "mnuSetting_Customization";
            this.mnuSetting_Customization.Size = new System.Drawing.Size(260, 22);
            this.mnuSetting_Customization.Text = "Customize Toolbar";
            this.mnuSetting_Customization.Click += new System.EventHandler(this.mnuSetting_Customization_Click);
            // 
            // mnuWindow
            // 
            this.mnuWindow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuWindow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuWindow.Name = "mnuWindow";
            this.mnuWindow.Size = new System.Drawing.Size(64, 22);
            this.mnuWindow.Text = "&Window";
            this.mnuWindow.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mnuWindow_DropDownItemClicked);
            // 
            // mnuBilling
            // 
            this.mnuBilling.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBilling_PatPayment,
            this.mnuBilling_InsPayment,
            this.mnuBilling_PaymentTray,
            this.mnuBilling_Ledger,
            this.mnuBilling_DailyClose,
            this.mnuBilling_Remittance,
            this.mnuBilling_Charges});
            this.mnuBilling.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuBilling.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuBilling.Name = "mnuBilling";
            this.mnuBilling.Size = new System.Drawing.Size(111, 22);
            this.mnuBilling.Text = "&Billing in progress";
            this.mnuBilling.Visible = false;
            // 
            // mnuBilling_PatPayment
            // 
            this.mnuBilling_PatPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuBilling_PatPayment.Image = ((System.Drawing.Image)(resources.GetObject("mnuBilling_PatPayment.Image")));
            this.mnuBilling_PatPayment.Name = "mnuBilling_PatPayment";
            this.mnuBilling_PatPayment.Size = new System.Drawing.Size(179, 22);
            this.mnuBilling_PatPayment.Text = "Patient Payment";
            this.mnuBilling_PatPayment.Visible = false;
            this.mnuBilling_PatPayment.Click += new System.EventHandler(this.mnuBilling_PatPayment_Click);
            // 
            // mnuBilling_InsPayment
            // 
            this.mnuBilling_InsPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuBilling_InsPayment.Image = ((System.Drawing.Image)(resources.GetObject("mnuBilling_InsPayment.Image")));
            this.mnuBilling_InsPayment.Name = "mnuBilling_InsPayment";
            this.mnuBilling_InsPayment.Size = new System.Drawing.Size(179, 22);
            this.mnuBilling_InsPayment.Text = "Insurance Payment";
            this.mnuBilling_InsPayment.Click += new System.EventHandler(this.mnuBilling_InsPayment_Click);
            // 
            // mnuBilling_PaymentTray
            // 
            this.mnuBilling_PaymentTray.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuBilling_PaymentTray.Image = ((System.Drawing.Image)(resources.GetObject("mnuBilling_PaymentTray.Image")));
            this.mnuBilling_PaymentTray.Name = "mnuBilling_PaymentTray";
            this.mnuBilling_PaymentTray.Size = new System.Drawing.Size(179, 22);
            this.mnuBilling_PaymentTray.Text = "Payment Tray";
            this.mnuBilling_PaymentTray.Click += new System.EventHandler(this.mnuBilling_PaymentTray_Click);
            // 
            // mnuBilling_Ledger
            // 
            this.mnuBilling_Ledger.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuBilling_Ledger.Image = ((System.Drawing.Image)(resources.GetObject("mnuBilling_Ledger.Image")));
            this.mnuBilling_Ledger.Name = "mnuBilling_Ledger";
            this.mnuBilling_Ledger.Size = new System.Drawing.Size(179, 22);
            this.mnuBilling_Ledger.Text = "Ledger";
            this.mnuBilling_Ledger.Click += new System.EventHandler(this.mnuBilling_Ledger_Click);
            // 
            // mnuBilling_DailyClose
            // 
            this.mnuBilling_DailyClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuBilling_DailyClose.Image = ((System.Drawing.Image)(resources.GetObject("mnuBilling_DailyClose.Image")));
            this.mnuBilling_DailyClose.Name = "mnuBilling_DailyClose";
            this.mnuBilling_DailyClose.Size = new System.Drawing.Size(179, 22);
            this.mnuBilling_DailyClose.Text = "Daily Close";
            this.mnuBilling_DailyClose.Click += new System.EventHandler(this.mnuBilling_DailyClose_Click);
            // 
            // mnuBilling_Remittance
            // 
            this.mnuBilling_Remittance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuBilling_Remittance.Image = ((System.Drawing.Image)(resources.GetObject("mnuBilling_Remittance.Image")));
            this.mnuBilling_Remittance.Name = "mnuBilling_Remittance";
            this.mnuBilling_Remittance.Size = new System.Drawing.Size(179, 22);
            this.mnuBilling_Remittance.Text = "Remittance";
            this.mnuBilling_Remittance.Click += new System.EventHandler(this.mnuBilling_Remittance_Click);
            // 
            // mnuBilling_Charges
            // 
            this.mnuBilling_Charges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuBilling_Charges.Image = ((System.Drawing.Image)(resources.GetObject("mnuBilling_Charges.Image")));
            this.mnuBilling_Charges.Name = "mnuBilling_Charges";
            this.mnuBilling_Charges.Size = new System.Drawing.Size(179, 22);
            this.mnuBilling_Charges.Text = "Charges";
            this.mnuBilling_Charges.Click += new System.EventHandler(this.mnuBilling_Charges_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelp_HowDoI,
            this.mnuHelp_Search,
            this.mnuHelp_Contents,
            this.toolStripSeparator10,
            this.mnuHelp_TechnicalSupport,
            this.mnuHelp_Version,
            this.mnuHelp_License,
            this.toolStripSeparator9,
            this.mnuSupport,
            this.mnuHelp_AboutgloPMS});
            this.mnuHelp.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuHelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(43, 22);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelp_HowDoI
            // 
            this.mnuHelp_HowDoI.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuHelp_HowDoI.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelp_HowDoI.Image")));
            this.mnuHelp_HowDoI.Name = "mnuHelp_HowDoI";
            this.mnuHelp_HowDoI.Size = new System.Drawing.Size(173, 22);
            this.mnuHelp_HowDoI.Text = "User Guide";
            this.mnuHelp_HowDoI.Click += new System.EventHandler(this.mnuHelp_HowDoI_Click);
            // 
            // mnuHelp_Search
            // 
            this.mnuHelp_Search.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuHelp_Search.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelp_Search.Image")));
            this.mnuHelp_Search.Name = "mnuHelp_Search";
            this.mnuHelp_Search.Size = new System.Drawing.Size(173, 22);
            this.mnuHelp_Search.Text = "Search";
            this.mnuHelp_Search.Click += new System.EventHandler(this.mnuHelp_Search_Click);
            // 
            // mnuHelp_Contents
            // 
            this.mnuHelp_Contents.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuHelp_Contents.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelp_Contents.Image")));
            this.mnuHelp_Contents.Name = "mnuHelp_Contents";
            this.mnuHelp_Contents.Size = new System.Drawing.Size(173, 22);
            this.mnuHelp_Contents.Text = "Contents";
            this.mnuHelp_Contents.Click += new System.EventHandler(this.mnuHelp_Contents_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(170, 6);
            this.toolStripSeparator10.Visible = false;
            // 
            // mnuHelp_TechnicalSupport
            // 
            this.mnuHelp_TechnicalSupport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuHelp_TechnicalSupport.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelp_TechnicalSupport.Image")));
            this.mnuHelp_TechnicalSupport.Name = "mnuHelp_TechnicalSupport";
            this.mnuHelp_TechnicalSupport.Size = new System.Drawing.Size(173, 22);
            this.mnuHelp_TechnicalSupport.Text = "Technical Support";
            this.mnuHelp_TechnicalSupport.Visible = false;
            this.mnuHelp_TechnicalSupport.Click += new System.EventHandler(this.mnuHelp_TechnicalSupport_Click);
            // 
            // mnuHelp_Version
            // 
            this.mnuHelp_Version.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuHelp_Version.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelp_Version.Image")));
            this.mnuHelp_Version.Name = "mnuHelp_Version";
            this.mnuHelp_Version.Size = new System.Drawing.Size(173, 22);
            this.mnuHelp_Version.Text = "Version";
            this.mnuHelp_Version.Visible = false;
            this.mnuHelp_Version.Click += new System.EventHandler(this.mnuHelp_Version_Click);
            // 
            // mnuHelp_License
            // 
            this.mnuHelp_License.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuHelp_License.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelp_License.Image")));
            this.mnuHelp_License.Name = "mnuHelp_License";
            this.mnuHelp_License.Size = new System.Drawing.Size(173, 22);
            this.mnuHelp_License.Text = "License";
            this.mnuHelp_License.Visible = false;
            this.mnuHelp_License.Click += new System.EventHandler(this.mnuHelp_License_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(170, 6);
            // 
            // mnuSupport
            // 
            this.mnuSupport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuSupport.Image = ((System.Drawing.Image)(resources.GetObject("mnuSupport.Image")));
            this.mnuSupport.Name = "mnuSupport";
            this.mnuSupport.Size = new System.Drawing.Size(173, 22);
            this.mnuSupport.Text = "Support";
            this.mnuSupport.Click += new System.EventHandler(this.mnuSupport_Click);
            // 
            // mnuHelp_AboutgloPMS
            // 
            this.mnuHelp_AboutgloPMS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuHelp_AboutgloPMS.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelp_AboutgloPMS.Image")));
            this.mnuHelp_AboutgloPMS.Name = "mnuHelp_AboutgloPMS";
            this.mnuHelp_AboutgloPMS.Size = new System.Drawing.Size(173, 22);
            this.mnuHelp_AboutgloPMS.Text = "About us";
            this.mnuHelp_AboutgloPMS.Click += new System.EventHandler(this.mnuHelp_AboutgloPMS_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("statusStrip1.BackgroundImage")));
            this.statusStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.statusStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sslbl_Login,
            this.sslbl_SingleSignOn,
            this.sslbl_Database,
            this.sslbl_Version,
            this.sslbl_LastModifiedDate});
            this.statusStrip1.Location = new System.Drawing.Point(1, 3);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1241, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // sslbl_Login
            // 
            this.sslbl_Login.Image = ((System.Drawing.Image)(resources.GetObject("sslbl_Login.Image")));
            this.sslbl_Login.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sslbl_Login.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.sslbl_Login.Name = "sslbl_Login";
            this.sslbl_Login.Size = new System.Drawing.Size(271, 17);
            this.sslbl_Login.Spring = true;
            // 
            // sslbl_SingleSignOn
            // 
            this.sslbl_SingleSignOn.AutoToolTip = true;
            this.sslbl_SingleSignOn.ForeColor = System.Drawing.Color.Red;
            this.sslbl_SingleSignOn.Image = ((System.Drawing.Image)(resources.GetObject("sslbl_SingleSignOn.Image")));
            this.sslbl_SingleSignOn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sslbl_SingleSignOn.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.sslbl_SingleSignOn.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.sslbl_SingleSignOn.LinkColor = System.Drawing.Color.Red;
            this.sslbl_SingleSignOn.Name = "sslbl_SingleSignOn";
            this.sslbl_SingleSignOn.Size = new System.Drawing.Size(321, 17);
            this.sslbl_SingleSignOn.Text = "The user has been auto logged in with Single sign-on.";
            this.sslbl_SingleSignOn.VisitedLinkColor = System.Drawing.Color.Red;
            // 
            // sslbl_Database
            // 
            this.sslbl_Database.Image = ((System.Drawing.Image)(resources.GetObject("sslbl_Database.Image")));
            this.sslbl_Database.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sslbl_Database.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.sslbl_Database.Name = "sslbl_Database";
            this.sslbl_Database.Size = new System.Drawing.Size(271, 17);
            this.sslbl_Database.Spring = true;
            // 
            // sslbl_Version
            // 
            this.sslbl_Version.Image = ((System.Drawing.Image)(resources.GetObject("sslbl_Version.Image")));
            this.sslbl_Version.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sslbl_Version.Name = "sslbl_Version";
            this.sslbl_Version.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.sslbl_Version.Size = new System.Drawing.Size(85, 17);
            this.sslbl_Version.Text = "gloPM2010";
            // 
            // sslbl_LastModifiedDate
            // 
            this.sslbl_LastModifiedDate.AutoSize = false;
            this.sslbl_LastModifiedDate.Image = ((System.Drawing.Image)(resources.GetObject("sslbl_LastModifiedDate.Image")));
            this.sslbl_LastModifiedDate.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sslbl_LastModifiedDate.Name = "sslbl_LastModifiedDate";
            this.sslbl_LastModifiedDate.Size = new System.Drawing.Size(271, 17);
            this.sslbl_LastModifiedDate.Spring = true;
            this.sslbl_LastModifiedDate.Text = "Last Modified Date";
            this.sslbl_LastModifiedDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ts_Main
            // 
            this.ts_Main.BackColor = System.Drawing.Color.Transparent;
            this.ts_Main.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_Main.BackgroundImage")));
            this.ts_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_Main.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_Main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_Main.ImageScalingSize = new System.Drawing.Size(42, 42);
            this.ts_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_PatientRegistration,
            this.tsb_PatientModification,
            this.tsb_PatientScan,
            this.toolStripSeparator2,
            this.tsb_Calendar,
            this.tsb_Appointment,
            this.toolStripSeparator23,
            this.tsb_Billing,
            this.tsb_PaymentPatient,
            this.tsb_BillingBatch,
            this.tsb_PaymentInsurance,
            this.tsb_Advance,
            this.tsb_ERAPayment,
            this.tsb_Payment,
            this.tsb_PatBalance,
            this.tsb_Remittance,
            this.tsb_Exit,
            this.tsb_PatLedger,
            this.tsb_PatientStatment,
            this.tsb_Calculator,
            this.toolStripSeparator14,
            this.tsb_ShowDashboard,
            this.toolStripSeparator17,
            this.tsb_LockScreen,
            this.tsb_RevenueCycle,
            this.tsbDailyClose,
            this.tsb_ScanDocs,
            this.tsb_RCMDocs,
            this.tsb_ClearGagePayment});
            this.ts_Main.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ts_Main.Location = new System.Drawing.Point(1, 0);
            this.ts_Main.Name = "ts_Main";
            this.ts_Main.Size = new System.Drawing.Size(1242, 63);
            this.ts_Main.TabIndex = 12;
            this.ts_Main.Text = "toolStrip1";
            this.ts_Main.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ts_Main_MouseDown);
            // 
            // tsb_PatientRegistration
            // 
            this.tsb_PatientRegistration.BackColor = System.Drawing.Color.Transparent;
            this.tsb_PatientRegistration.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_PatientRegistration.BackgroundImage")));
            this.tsb_PatientRegistration.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_PatientRegistration.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PatientRegistration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PatientRegistration.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PatientRegistration.Image")));
            this.tsb_PatientRegistration.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PatientRegistration.Name = "tsb_PatientRegistration";
            this.tsb_PatientRegistration.Size = new System.Drawing.Size(86, 60);
            this.tsb_PatientRegistration.Tag = "Add New Patient";
            this.tsb_PatientRegistration.Text = "New Patient";
            this.tsb_PatientRegistration.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PatientRegistration.ToolTipText = "Add New Patient";
            this.tsb_PatientRegistration.Click += new System.EventHandler(this.tsb_PatientRegistration_Click);
            // 
            // tsb_PatientModification
            // 
            this.tsb_PatientModification.BackColor = System.Drawing.Color.Transparent;
            this.tsb_PatientModification.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_PatientModification.BackgroundImage")));
            this.tsb_PatientModification.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_PatientModification.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PatientModification.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PatientModification.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PatientModification.Image")));
            this.tsb_PatientModification.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PatientModification.Name = "tsb_PatientModification";
            this.tsb_PatientModification.Size = new System.Drawing.Size(102, 60);
            this.tsb_PatientModification.Tag = "Modify Patient";
            this.tsb_PatientModification.Text = "Modify Patient";
            this.tsb_PatientModification.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PatientModification.ToolTipText = "Modify Patient";
            this.tsb_PatientModification.Click += new System.EventHandler(this.tsb_PatientModification_Click);
            // 
            // tsb_PatientScan
            // 
            this.tsb_PatientScan.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PatientScan.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PatientScan.Image")));
            this.tsb_PatientScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PatientScan.Name = "tsb_PatientScan";
            this.tsb_PatientScan.Size = new System.Drawing.Size(89, 60);
            this.tsb_PatientScan.Tag = "Scan Patient";
            this.tsb_PatientScan.Text = "Scan Patient";
            this.tsb_PatientScan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PatientScan.Visible = false;
            this.tsb_PatientScan.Click += new System.EventHandler(this.tsb_PatientScan_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 63);
            this.toolStripSeparator2.Tag = "Separator";
            // 
            // tsb_Calendar
            // 
            this.tsb_Calendar.BackColor = System.Drawing.Color.Transparent;
            this.tsb_Calendar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_Calendar.BackgroundImage")));
            this.tsb_Calendar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_Calendar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Calendar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Calendar.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Calendar.Image")));
            this.tsb_Calendar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Calendar.Name = "tsb_Calendar";
            this.tsb_Calendar.Size = new System.Drawing.Size(64, 60);
            this.tsb_Calendar.Tag = "Calendar";
            this.tsb_Calendar.Text = "Calendar";
            this.tsb_Calendar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Calendar.Click += new System.EventHandler(this.tsb_Calendar_Click);
            // 
            // tsb_Appointment
            // 
            this.tsb_Appointment.BackColor = System.Drawing.Color.Transparent;
            this.tsb_Appointment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_Appointment.BackgroundImage")));
            this.tsb_Appointment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_Appointment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Appointment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Appointment.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Appointment.Image")));
            this.tsb_Appointment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Appointment.Name = "tsb_Appointment";
            this.tsb_Appointment.Size = new System.Drawing.Size(93, 60);
            this.tsb_Appointment.Tag = "Appointment";
            this.tsb_Appointment.Text = "Appointment";
            this.tsb_Appointment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Appointment.Click += new System.EventHandler(this.tsb_Appointment_Click);
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new System.Drawing.Size(6, 63);
            this.toolStripSeparator23.Tag = "Separator";
            // 
            // tsb_Billing
            // 
            this.tsb_Billing.BackColor = System.Drawing.Color.Transparent;
            this.tsb_Billing.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_Billing.BackgroundImage")));
            this.tsb_Billing.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_Billing.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Billing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Billing.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Billing.Image")));
            this.tsb_Billing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Billing.Name = "tsb_Billing";
            this.tsb_Billing.Size = new System.Drawing.Size(60, 60);
            this.tsb_Billing.Tag = "Charges";
            this.tsb_Billing.Text = "Charges";
            this.tsb_Billing.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Billing.Click += new System.EventHandler(this.tsb_Billing_Click);
            // 
            // tsb_PaymentPatient
            // 
            this.tsb_PaymentPatient.BackColor = System.Drawing.Color.Transparent;
            this.tsb_PaymentPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_PaymentPatient.BackgroundImage")));
            this.tsb_PaymentPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_PaymentPatient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PaymentPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PaymentPatient.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PaymentPatient.Image")));
            this.tsb_PaymentPatient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PaymentPatient.Name = "tsb_PaymentPatient";
            this.tsb_PaymentPatient.Size = new System.Drawing.Size(114, 60);
            this.tsb_PaymentPatient.Tag = "Patient Payment";
            this.tsb_PaymentPatient.Text = "Patient Payment";
            this.tsb_PaymentPatient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PaymentPatient.ToolTipText = "Patient Payment";
            this.tsb_PaymentPatient.Click += new System.EventHandler(this.tsb_PaymentPatient_Click);
            // 
            // tsb_BillingBatch
            // 
            this.tsb_BillingBatch.BackColor = System.Drawing.Color.Transparent;
            this.tsb_BillingBatch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_BillingBatch.BackgroundImage")));
            this.tsb_BillingBatch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_BillingBatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_BillingBatch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_BillingBatch.Image = ((System.Drawing.Image)(resources.GetObject("tsb_BillingBatch.Image")));
            this.tsb_BillingBatch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_BillingBatch.Name = "tsb_BillingBatch";
            this.tsb_BillingBatch.Size = new System.Drawing.Size(46, 60);
            this.tsb_BillingBatch.Tag = "Batch";
            this.tsb_BillingBatch.Text = "Batch";
            this.tsb_BillingBatch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_BillingBatch.ToolTipText = "Batch";
            this.tsb_BillingBatch.Click += new System.EventHandler(this.tsb_BillingBatch_Click);
            // 
            // tsb_PaymentInsurance
            // 
            this.tsb_PaymentInsurance.BackColor = System.Drawing.Color.Transparent;
            this.tsb_PaymentInsurance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_PaymentInsurance.BackgroundImage")));
            this.tsb_PaymentInsurance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_PaymentInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PaymentInsurance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PaymentInsurance.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PaymentInsurance.Image")));
            this.tsb_PaymentInsurance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PaymentInsurance.Name = "tsb_PaymentInsurance";
            this.tsb_PaymentInsurance.Size = new System.Drawing.Size(129, 60);
            this.tsb_PaymentInsurance.Tag = "Insurance Payment";
            this.tsb_PaymentInsurance.Text = "Insurance Payment";
            this.tsb_PaymentInsurance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PaymentInsurance.ToolTipText = "Insurance Payment";
            this.tsb_PaymentInsurance.Click += new System.EventHandler(this.tsb_PaymentInsurance_Click);
            // 
            // tsb_Advance
            // 
            this.tsb_Advance.BackColor = System.Drawing.Color.Transparent;
            this.tsb_Advance.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_Advance.BackgroundImage")));
            this.tsb_Advance.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_Advance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Advance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Advance.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Advance.Image")));
            this.tsb_Advance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Advance.Name = "tsb_Advance";
            this.tsb_Advance.Size = new System.Drawing.Size(67, 60);
            this.tsb_Advance.Tag = "Advance ";
            this.tsb_Advance.Text = "Advance ";
            this.tsb_Advance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Advance.Click += new System.EventHandler(this.tsb_Advance_Click);
            // 
            // tsb_ERAPayment
            // 
            this.tsb_ERAPayment.BackColor = System.Drawing.Color.Transparent;
            this.tsb_ERAPayment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_ERAPayment.BackgroundImage")));
            this.tsb_ERAPayment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_ERAPayment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ERAPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_ERAPayment.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ERAPayment.Image")));
            this.tsb_ERAPayment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ERAPayment.Name = "tsb_ERAPayment";
            this.tsb_ERAPayment.Size = new System.Drawing.Size(94, 60);
            this.tsb_ERAPayment.Tag = "ERA Payment";
            this.tsb_ERAPayment.Text = "ERA Payment";
            this.tsb_ERAPayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ERAPayment.ToolTipText = "ERA Payment";
            this.tsb_ERAPayment.Click += new System.EventHandler(this.tsb_ERAPayment_Click);
            // 
            // tsb_Payment
            // 
            this.tsb_Payment.BackColor = System.Drawing.Color.Transparent;
            this.tsb_Payment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_Payment.BackgroundImage")));
            this.tsb_Payment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_Payment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Payment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Payment.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Payment.Image")));
            this.tsb_Payment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Payment.Name = "tsb_Payment";
            this.tsb_Payment.Size = new System.Drawing.Size(65, 60);
            this.tsb_Payment.Tag = "Payment";
            this.tsb_Payment.Text = "Payment";
            this.tsb_Payment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Payment.Visible = false;
            this.tsb_Payment.Click += new System.EventHandler(this.tsb_Payment_Click);
            // 
            // tsb_PatBalance
            // 
            this.tsb_PatBalance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PatBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PatBalance.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PatBalance.Image")));
            this.tsb_PatBalance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PatBalance.Name = "tsb_PatBalance";
            this.tsb_PatBalance.Size = new System.Drawing.Size(57, 60);
            this.tsb_PatBalance.Tag = "Balance";
            this.tsb_PatBalance.Text = "Balance";
            this.tsb_PatBalance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PatBalance.ToolTipText = "Balance";
            this.tsb_PatBalance.Visible = false;
            this.tsb_PatBalance.Click += new System.EventHandler(this.tsb_PatBalance_Click);
            // 
            // tsb_Remittance
            // 
            this.tsb_Remittance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Remittance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Remittance.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Remittance.Image")));
            this.tsb_Remittance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Remittance.Name = "tsb_Remittance";
            this.tsb_Remittance.Size = new System.Drawing.Size(81, 60);
            this.tsb_Remittance.Tag = "Remittance";
            this.tsb_Remittance.Text = "Remittance";
            this.tsb_Remittance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Remittance.Visible = false;
            this.tsb_Remittance.Click += new System.EventHandler(this.tsb_Remittance_Click);
            // 
            // tsb_Exit
            // 
            this.tsb_Exit.BackColor = System.Drawing.Color.Transparent;
            this.tsb_Exit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_Exit.BackgroundImage")));
            this.tsb_Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_Exit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Exit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Exit.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Exit.Image")));
            this.tsb_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Exit.Name = "tsb_Exit";
            this.tsb_Exit.Size = new System.Drawing.Size(46, 60);
            this.tsb_Exit.Tag = "Exit";
            this.tsb_Exit.Text = "Exit";
            this.tsb_Exit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Exit.Click += new System.EventHandler(this.tsb_Exit_Click);
            // 
            // tsb_PatLedger
            // 
            this.tsb_PatLedger.BackColor = System.Drawing.Color.Transparent;
            this.tsb_PatLedger.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_PatLedger.BackgroundImage")));
            this.tsb_PatLedger.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_PatLedger.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PatLedger.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PatLedger.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PatLedger.Image")));
            this.tsb_PatLedger.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PatLedger.Name = "tsb_PatLedger";
            this.tsb_PatLedger.Size = new System.Drawing.Size(71, 60);
            this.tsb_PatLedger.Tag = "Ledger";
            this.tsb_PatLedger.Text = "Pat. Acct.";
            this.tsb_PatLedger.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PatLedger.ToolTipText = "Patient Account";
            this.tsb_PatLedger.Click += new System.EventHandler(this.tsb_PatLedger_Click);
            // 
            // tsb_PatientStatment
            // 
            this.tsb_PatientStatment.BackColor = System.Drawing.Color.Transparent;
            this.tsb_PatientStatment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_PatientStatment.BackgroundImage")));
            this.tsb_PatientStatment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_PatientStatment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PatientStatment.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PatientStatment.Image")));
            this.tsb_PatientStatment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PatientStatment.Name = "tsb_PatientStatment";
            this.tsb_PatientStatment.Size = new System.Drawing.Size(126, 60);
            this.tsb_PatientStatment.Tag = "Patient Statement";
            this.tsb_PatientStatment.Text = "Patient Statement";
            this.tsb_PatientStatment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_PatientStatment.Click += new System.EventHandler(this.tsb_PatientStatment_Click);
            // 
            // tsb_Calculator
            // 
            this.tsb_Calculator.BackColor = System.Drawing.Color.Transparent;
            this.tsb_Calculator.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_Calculator.BackgroundImage")));
            this.tsb_Calculator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_Calculator.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_Calculator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_Calculator.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Calculator.Image")));
            this.tsb_Calculator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Calculator.Name = "tsb_Calculator";
            this.tsb_Calculator.Size = new System.Drawing.Size(72, 60);
            this.tsb_Calculator.Tag = "Calculator";
            this.tsb_Calculator.Text = "Calculator";
            this.tsb_Calculator.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Calculator.Click += new System.EventHandler(this.tsb_Calculator_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 63);
            this.toolStripSeparator14.Tag = "Separator";
            // 
            // tsb_ShowDashboard
            // 
            this.tsb_ShowDashboard.BackColor = System.Drawing.Color.Transparent;
            this.tsb_ShowDashboard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_ShowDashboard.BackgroundImage")));
            this.tsb_ShowDashboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_ShowDashboard.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ShowDashboard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_ShowDashboard.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ShowDashboard.Image")));
            this.tsb_ShowDashboard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ShowDashboard.Name = "tsb_ShowDashboard";
            this.tsb_ShowDashboard.Size = new System.Drawing.Size(116, 60);
            this.tsb_ShowDashboard.Tag = "Show Dashboard";
            this.tsb_ShowDashboard.Text = "Show Dashboard";
            this.tsb_ShowDashboard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ShowDashboard.Click += new System.EventHandler(this.tsb_ShowDashboard_Click);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(6, 63);
            // 
            // tsb_LockScreen
            // 
            this.tsb_LockScreen.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsb_LockScreen.BackColor = System.Drawing.Color.Transparent;
            this.tsb_LockScreen.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_LockScreen.BackgroundImage")));
            this.tsb_LockScreen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_LockScreen.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_LockScreen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_LockScreen.Image = ((System.Drawing.Image)(resources.GetObject("tsb_LockScreen.Image")));
            this.tsb_LockScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_LockScreen.Name = "tsb_LockScreen";
            this.tsb_LockScreen.Size = new System.Drawing.Size(84, 60);
            this.tsb_LockScreen.Tag = "Lock Screen";
            this.tsb_LockScreen.Text = "Lock Screen";
            this.tsb_LockScreen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_LockScreen.ToolTipText = "Lock Screen";
            this.tsb_LockScreen.Click += new System.EventHandler(this.tsb_LockScreen_Click);
            // 
            // tsb_RevenueCycle
            // 
            this.tsb_RevenueCycle.BackColor = System.Drawing.Color.Transparent;
            this.tsb_RevenueCycle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_RevenueCycle.BackgroundImage")));
            this.tsb_RevenueCycle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_RevenueCycle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_RevenueCycle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_RevenueCycle.Image = ((System.Drawing.Image)(resources.GetObject("tsb_RevenueCycle.Image")));
            this.tsb_RevenueCycle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_RevenueCycle.Name = "tsb_RevenueCycle";
            this.tsb_RevenueCycle.Size = new System.Drawing.Size(99, 60);
            this.tsb_RevenueCycle.Tag = "Revenue Cycle";
            this.tsb_RevenueCycle.Text = "Revenue Cycle";
            this.tsb_RevenueCycle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_RevenueCycle.ToolTipText = "Revenue Cycle";
            this.tsb_RevenueCycle.Click += new System.EventHandler(this.tsb_RevenueCycle_Click);
            // 
            // tsbDailyClose
            // 
            this.tsbDailyClose.BackColor = System.Drawing.Color.Transparent;
            this.tsbDailyClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsbDailyClose.BackgroundImage")));
            this.tsbDailyClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsbDailyClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbDailyClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsbDailyClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbDailyClose.Image")));
            this.tsbDailyClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDailyClose.Name = "tsbDailyClose";
            this.tsbDailyClose.Size = new System.Drawing.Size(76, 60);
            this.tsbDailyClose.Tag = "Daily Close";
            this.tsbDailyClose.Text = "Daily Close";
            this.tsbDailyClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbDailyClose.ToolTipText = "Daily Close";
            this.tsbDailyClose.Click += new System.EventHandler(this.tsbDailyClose_Click);
            // 
            // tsb_ScanDocs
            // 
            this.tsb_ScanDocs.BackColor = System.Drawing.Color.Transparent;
            this.tsb_ScanDocs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_ScanDocs.BackgroundImage")));
            this.tsb_ScanDocs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_ScanDocs.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ScanDocs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_ScanDocs.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ScanDocs.Image")));
            this.tsb_ScanDocs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ScanDocs.Name = "tsb_ScanDocs";
            this.tsb_ScanDocs.Size = new System.Drawing.Size(73, 60);
            this.tsb_ScanDocs.Tag = "Scan Docs";
            this.tsb_ScanDocs.Text = "Scan Docs";
            this.tsb_ScanDocs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ScanDocs.ToolTipText = "Scan Docs";
            this.tsb_ScanDocs.Visible = false;
            this.tsb_ScanDocs.Click += new System.EventHandler(this.tsb_ScanDocs_Click);
            // 
            // tsb_RCMDocs
            // 
            this.tsb_RCMDocs.BackColor = System.Drawing.Color.Transparent;
            this.tsb_RCMDocs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_RCMDocs.BackgroundImage")));
            this.tsb_RCMDocs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_RCMDocs.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_RCMDocs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_RCMDocs.Image = ((System.Drawing.Image)(resources.GetObject("tsb_RCMDocs.Image")));
            this.tsb_RCMDocs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_RCMDocs.Name = "tsb_RCMDocs";
            this.tsb_RCMDocs.Size = new System.Drawing.Size(72, 60);
            this.tsb_RCMDocs.Tag = "RCMDocs";
            this.tsb_RCMDocs.Text = "RCM Docs";
            this.tsb_RCMDocs.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_RCMDocs.ToolTipText = "RCM Docs";
            this.tsb_RCMDocs.Click += new System.EventHandler(this.tsb_RCMDocs_Click);
            // 
            // tsb_ClearGagePayment
            // 
            this.tsb_ClearGagePayment.BackColor = System.Drawing.Color.Transparent;
            this.tsb_ClearGagePayment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsb_ClearGagePayment.BackgroundImage")));
            this.tsb_ClearGagePayment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_ClearGagePayment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_ClearGagePayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_ClearGagePayment.Image = ((System.Drawing.Image)(resources.GetObject("tsb_ClearGagePayment.Image")));
            this.tsb_ClearGagePayment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_ClearGagePayment.Name = "tsb_ClearGagePayment";
            this.tsb_ClearGagePayment.Size = new System.Drawing.Size(129, 60);
            this.tsb_ClearGagePayment.Tag = "Cleargage Payment";
            this.tsb_ClearGagePayment.Text = "Cleargage Payment";
            this.tsb_ClearGagePayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_ClearGagePayment.ToolTipText = "Cleargage Payment";
            this.tsb_ClearGagePayment.Click += new System.EventHandler(this.tsb_ClearGagePayment_Click);
            // 
            // uiPanelManager1
            // 
            this.uiPanelManager1.ContainerControl = this;
            this.uiPanelManager1.DefaultPanelSettings.CaptionFormatStyle.ForeColor = System.Drawing.Color.DarkBlue;
            this.uiPanelManager1.DefaultPanelSettings.DarkCaptionFormatStyle.ForeColor = System.Drawing.Color.White;
            this.uiPanelManager1.LayoutStream = ((System.IO.Stream)(resources.GetObject("uiPanelManager1.LayoutStream")));
            this.uiPanelManager1.PanelPadding.Bottom = 2;
            this.uiPanelManager1.PanelPadding.Left = 2;
            this.uiPanelManager1.PanelPadding.Right = 2;
            this.uiPanelManager1.PanelPadding.Top = 2;
            this.uiPanelManager1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007;
            this.uipnlPatient_Alert.Id = new System.Guid("838e6142-a7e9-4031-b071-a94fddecd051");
            this.uiPanelManager1.Panels.Add(this.uipnlPatient_Alert);
            this.pnlPatient_UpComingAppointments.Id = new System.Guid("19897340-3abd-4067-a36e-727daf463bbf");
            this.uiPanelManager1.Panels.Add(this.pnlPatient_UpComingAppointments);
            this.pnlPatient_Details.Id = new System.Guid("a233a277-f394-47b5-8c85-f6ad73f39024");
            this.uiPanelManager1.Panels.Add(this.pnlPatient_Details);
            this.pnlPatient_Demographics.Id = new System.Guid("66cea3a3-2874-4889-ae0c-84009f9e732c");
            this.pnlPatient_Demo.Id = new System.Guid("015238d2-f16c-40c2-9f72-e4ff1e28c473");
            this.pnlPatient_Demographics.Panels.Add(this.pnlPatient_Demo);
            this.pnlCards.Id = new System.Guid("b88cdc6f-3499-4aa5-82ed-f21e686c25bf");
            this.pnlPatient_Demographics.Panels.Add(this.pnlCards);
            this.uiPanelManager1.Panels.Add(this.pnlPatient_Demographics);
            // 
            // Design Time Panel Info:
            // 
            this.uiPanelManager1.BeginPanelInfo();
            this.uiPanelManager1.AddDockPanelInfo(new System.Guid("838e6142-a7e9-4031-b071-a94fddecd051"), Janus.Windows.UI.Dock.PanelDockStyle.Left, new System.Drawing.Size(197, 609), true);
            this.uiPanelManager1.AddDockPanelInfo(new System.Guid("19897340-3abd-4067-a36e-727daf463bbf"), Janus.Windows.UI.Dock.PanelDockStyle.Right, new System.Drawing.Size(128, 609), true);
            this.uiPanelManager1.AddDockPanelInfo(new System.Guid("a233a277-f394-47b5-8c85-f6ad73f39024"), Janus.Windows.UI.Dock.PanelDockStyle.Bottom, new System.Drawing.Size(914, 137), true);
            this.uiPanelManager1.AddDockPanelInfo(new System.Guid("66cea3a3-2874-4889-ae0c-84009f9e732c"), Janus.Windows.UI.Dock.PanelGroupStyle.VerticalTiles, Janus.Windows.UI.Dock.PanelDockStyle.Bottom, false, new System.Drawing.Size(914, 329), true);
            this.uiPanelManager1.AddDockPanelInfo(new System.Guid("015238d2-f16c-40c2-9f72-e4ff1e28c473"), new System.Guid("66cea3a3-2874-4889-ae0c-84009f9e732c"), 512, true);
            this.uiPanelManager1.AddDockPanelInfo(new System.Guid("b88cdc6f-3499-4aa5-82ed-f21e686c25bf"), new System.Guid("66cea3a3-2874-4889-ae0c-84009f9e732c"), 399, true);
            this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("19897340-3abd-4067-a36e-727daf463bbf"), new System.Drawing.Point(813, 534), new System.Drawing.Size(200, 200), false);
            this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("fe5e4ffa-f9f7-4520-8a97-2a56f1864f97"), Janus.Windows.UI.Dock.PanelGroupStyle.OutlookNavigator, true, new System.Drawing.Point(-1, -1), new System.Drawing.Size(-1, -1), false);
            this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("ba4de5d6-1f4d-49b9-b4d1-15122e12bc99"), new System.Drawing.Point(-1, -1), new System.Drawing.Size(-1, -1), false);
            this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("c09228be-8421-4c86-b266-7c20c970f920"), new System.Drawing.Point(-1, -1), new System.Drawing.Size(-1, -1), false);
            this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("0883257c-49bf-4369-9a03-f594d6e6eeca"), new System.Drawing.Point(-1, -1), new System.Drawing.Size(-1, -1), false);
            this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("cfde5164-8a7c-4108-9f68-4f9dc6042abd"), new System.Drawing.Point(-1, -1), new System.Drawing.Size(-1, -1), false);
            this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("33da2f62-185b-4ef0-af20-e94b0f75c350"), new System.Drawing.Point(-1, -1), new System.Drawing.Size(-1, -1), false);
            this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("ad54dce2-360b-4f1a-bd46-7b2c233e3bae"), new System.Drawing.Point(-1, -1), new System.Drawing.Size(-1, -1), false);
            this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("a233a277-f394-47b5-8c85-f6ad73f39024"), new System.Drawing.Point(-1, -1), new System.Drawing.Size(-1, -1), false);
            this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("015238d2-f16c-40c2-9f72-e4ff1e28c473"), new System.Drawing.Point(791, 547), new System.Drawing.Size(200, 200), false);
            this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("e0c162a7-22b7-455c-ab74-970f2663d515"), new System.Drawing.Point(772, 499), new System.Drawing.Size(200, 200), false);
            this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("838e6142-a7e9-4031-b071-a94fddecd051"), new System.Drawing.Point(-55, 450), new System.Drawing.Size(200, 200), false);
            this.uiPanelManager1.AddFloatingPanelInfo(new System.Guid("b88cdc6f-3499-4aa5-82ed-f21e686c25bf"), new System.Drawing.Point(628, 553), new System.Drawing.Size(200, 200), false);
            this.uiPanelManager1.EndPanelInfo();
            // 
            // uipnlPatient_Alert
            // 
            this.uipnlPatient_Alert.CaptionFormatStyle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.uipnlPatient_Alert.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark;
            this.uipnlPatient_Alert.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
            this.uipnlPatient_Alert.FloatingLocation = new System.Drawing.Point(-55, 450);
            this.uipnlPatient_Alert.Icon = ((System.Drawing.Icon)(resources.GetObject("uipnlPatient_Alert.Icon")));
            this.uipnlPatient_Alert.InnerContainer = this.uipnlPatient_AlertContainer;
            this.uipnlPatient_Alert.Location = new System.Drawing.Point(2, 93);
            this.uipnlPatient_Alert.Name = "uipnlPatient_Alert";
            this.uipnlPatient_Alert.Size = new System.Drawing.Size(197, 609);
            this.uipnlPatient_Alert.TabIndex = 4;
            this.uipnlPatient_Alert.Text = "Patient Alerts";
            // 
            // uipnlPatient_AlertContainer
            // 
            this.uipnlPatient_AlertContainer.Controls.Add(this.pnlSideButton);
            this.uipnlPatient_AlertContainer.Controls.Add(this.pnlPatientAlertMain);
            this.uipnlPatient_AlertContainer.Location = new System.Drawing.Point(1, 23);
            this.uipnlPatient_AlertContainer.Name = "uipnlPatient_AlertContainer";
            this.uipnlPatient_AlertContainer.Size = new System.Drawing.Size(191, 585);
            this.uipnlPatient_AlertContainer.TabIndex = 0;
            // 
            // pnlSideButton
            // 
            this.pnlSideButton.BackColor = System.Drawing.Color.White;
            this.pnlSideButton.Controls.Add(this.pnl_Appointment);
            this.pnlSideButton.Controls.Add(this.label48);
            this.pnlSideButton.Controls.Add(this.pnl_Calendar);
            this.pnlSideButton.Controls.Add(this.pnl_Tasks);
            this.pnlSideButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSideButton.Location = new System.Drawing.Point(0, 495);
            this.pnlSideButton.Name = "pnlSideButton";
            this.pnlSideButton.Size = new System.Drawing.Size(191, 90);
            this.pnlSideButton.TabIndex = 13;
            // 
            // pnl_Appointment
            // 
            this.pnl_Appointment.Controls.Add(this.label47);
            this.pnl_Appointment.Controls.Add(this.btn_Appointment);
            this.pnl_Appointment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_Appointment.Location = new System.Drawing.Point(0, 0);
            this.pnl_Appointment.Name = "pnl_Appointment";
            this.pnl_Appointment.Size = new System.Drawing.Size(191, 30);
            this.pnl_Appointment.TabIndex = 13;
            // 
            // label47
            // 
            this.label47.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.label47.Dock = System.Windows.Forms.DockStyle.Top;
            this.label47.Location = new System.Drawing.Point(0, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(191, 1);
            this.label47.TabIndex = 0;
            // 
            // btn_Appointment
            // 
            this.btn_Appointment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Appointment.BackgroundImage")));
            this.btn_Appointment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Appointment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Appointment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Appointment.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.btn_Appointment.FlatAppearance.BorderSize = 0;
            this.btn_Appointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Appointment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Appointment.Image = ((System.Drawing.Image)(resources.GetObject("btn_Appointment.Image")));
            this.btn_Appointment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Appointment.Location = new System.Drawing.Point(0, 0);
            this.btn_Appointment.Name = "btn_Appointment";
            this.btn_Appointment.Size = new System.Drawing.Size(191, 30);
            this.btn_Appointment.TabIndex = 6;
            this.btn_Appointment.Tag = "UnSelected";
            this.btn_Appointment.Text = "         Appointment";
            this.btn_Appointment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Appointment.UseVisualStyleBackColor = true;
            this.btn_Appointment.Click += new System.EventHandler(this.btn_Appointment_Click);
            this.btn_Appointment.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_Appointment.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // label48
            // 
            this.label48.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.label48.Dock = System.Windows.Forms.DockStyle.Top;
            this.label48.Location = new System.Drawing.Point(0, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(191, 1);
            this.label48.TabIndex = 2;
            // 
            // pnl_Calendar
            // 
            this.pnl_Calendar.Controls.Add(this.label49);
            this.pnl_Calendar.Controls.Add(this.btn_Calendar);
            this.pnl_Calendar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_Calendar.Location = new System.Drawing.Point(0, 30);
            this.pnl_Calendar.Name = "pnl_Calendar";
            this.pnl_Calendar.Size = new System.Drawing.Size(191, 30);
            this.pnl_Calendar.TabIndex = 11;
            // 
            // label49
            // 
            this.label49.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.label49.Dock = System.Windows.Forms.DockStyle.Top;
            this.label49.Location = new System.Drawing.Point(0, 0);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(191, 1);
            this.label49.TabIndex = 0;
            // 
            // btn_Calendar
            // 
            this.btn_Calendar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Calendar.BackgroundImage")));
            this.btn_Calendar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Calendar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Calendar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Calendar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.btn_Calendar.FlatAppearance.BorderSize = 0;
            this.btn_Calendar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Calendar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Calendar.Image = ((System.Drawing.Image)(resources.GetObject("btn_Calendar.Image")));
            this.btn_Calendar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Calendar.Location = new System.Drawing.Point(0, 0);
            this.btn_Calendar.Name = "btn_Calendar";
            this.btn_Calendar.Size = new System.Drawing.Size(191, 30);
            this.btn_Calendar.TabIndex = 6;
            this.btn_Calendar.Tag = "UnSelected";
            this.btn_Calendar.Text = "         Calendar";
            this.btn_Calendar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Calendar.UseVisualStyleBackColor = true;
            this.btn_Calendar.Click += new System.EventHandler(this.btn_Calendar_Click);
            this.btn_Calendar.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_Calendar.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // pnl_Tasks
            // 
            this.pnl_Tasks.Controls.Add(this.label50);
            this.pnl_Tasks.Controls.Add(this.btn_Tasks);
            this.pnl_Tasks.Controls.Add(this.label51);
            this.pnl_Tasks.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_Tasks.Location = new System.Drawing.Point(0, 60);
            this.pnl_Tasks.Name = "pnl_Tasks";
            this.pnl_Tasks.Size = new System.Drawing.Size(191, 30);
            this.pnl_Tasks.TabIndex = 12;
            // 
            // label50
            // 
            this.label50.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.label50.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label50.Location = new System.Drawing.Point(0, 29);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(191, 1);
            this.label50.TabIndex = 1;
            // 
            // btn_Tasks
            // 
            this.btn_Tasks.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Tasks.BackgroundImage")));
            this.btn_Tasks.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Tasks.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Tasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Tasks.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.btn_Tasks.FlatAppearance.BorderSize = 0;
            this.btn_Tasks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Tasks.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Tasks.Image = ((System.Drawing.Image)(resources.GetObject("btn_Tasks.Image")));
            this.btn_Tasks.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Tasks.Location = new System.Drawing.Point(0, 1);
            this.btn_Tasks.Name = "btn_Tasks";
            this.btn_Tasks.Size = new System.Drawing.Size(191, 29);
            this.btn_Tasks.TabIndex = 0;
            this.btn_Tasks.Tag = "UnSelected";
            this.btn_Tasks.Text = "         Tasks";
            this.btn_Tasks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Tasks.UseVisualStyleBackColor = true;
            this.btn_Tasks.Click += new System.EventHandler(this.btn_Tasks_Click);
            this.btn_Tasks.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            this.btn_Tasks.MouseHover += new System.EventHandler(this.btn_MouseHover);
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.label51.Dock = System.Windows.Forms.DockStyle.Top;
            this.label51.Location = new System.Drawing.Point(0, 0);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(191, 1);
            this.label51.TabIndex = 6;
            // 
            // pnlPatientAlertMain
            // 
            this.pnlPatientAlertMain.BackColor = System.Drawing.Color.White;
            this.pnlPatientAlertMain.Controls.Add(this.panel7);
            this.pnlPatientAlertMain.Controls.Add(this.pnlEligibilityCheck);
            this.pnlPatientAlertMain.Controls.Add(this.pnlCopayAlert);
            this.pnlPatientAlertMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPatientAlertMain.Location = new System.Drawing.Point(0, 0);
            this.pnlPatientAlertMain.Name = "pnlPatientAlertMain";
            this.pnlPatientAlertMain.Size = new System.Drawing.Size(191, 585);
            this.pnlPatientAlertMain.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.pnlc1PatientAlerts);
            this.panel7.Controls.Add(this.pnl_LeftButtons);
            this.panel7.Controls.Add(this.pnlLeftPatientAlert);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 50);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(191, 535);
            this.panel7.TabIndex = 6;
            // 
            // pnlc1PatientAlerts
            // 
            this.pnlc1PatientAlerts.Controls.Add(this.c1PatientAlerts);
            this.pnlc1PatientAlerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlc1PatientAlerts.Location = new System.Drawing.Point(0, 0);
            this.pnlc1PatientAlerts.Name = "pnlc1PatientAlerts";
            this.pnlc1PatientAlerts.Size = new System.Drawing.Size(191, 535);
            this.pnlc1PatientAlerts.TabIndex = 6;
            // 
            // c1PatientAlerts
            // 
            this.c1PatientAlerts.AllowEditing = false;
            this.c1PatientAlerts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PatientAlerts.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PatientAlerts.ColumnInfo = "10,0,0,0,0,95,Columns:";
            this.c1PatientAlerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PatientAlerts.EditOptions = C1.Win.C1FlexGrid.EditFlags.None;
            this.c1PatientAlerts.ExtendLastCol = true;
            this.c1PatientAlerts.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1PatientAlerts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PatientAlerts.Location = new System.Drawing.Point(0, 0);
            this.c1PatientAlerts.Name = "c1PatientAlerts";
            this.c1PatientAlerts.Rows.Count = 5;
            this.c1PatientAlerts.Rows.DefaultSize = 19;
            this.c1PatientAlerts.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1PatientAlerts.ShowCellLabels = true;
            this.c1PatientAlerts.Size = new System.Drawing.Size(191, 535);
            this.c1PatientAlerts.StyleInfo = resources.GetString("c1PatientAlerts.StyleInfo");
            this.c1PatientAlerts.TabIndex = 5;
            this.c1PatientAlerts.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1PatientAlerts.Tree.NodeImageCollapsed")));
            this.c1PatientAlerts.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1PatientAlerts.Tree.NodeImageExpanded")));
            this.c1PatientAlerts.DoubleClick += new System.EventHandler(this.c1PatientAlerts_DoubleClick);
            // 
            // pnl_LeftButtons
            // 
            this.pnl_LeftButtons.Controls.Add(this.btnAppointment);
            this.pnl_LeftButtons.Controls.Add(this.btnCalender);
            this.pnl_LeftButtons.Controls.Add(this.btnMail);
            this.pnl_LeftButtons.Controls.Add(this.btnTask);
            this.pnl_LeftButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_LeftButtons.Location = new System.Drawing.Point(0, 0);
            this.pnl_LeftButtons.Name = "pnl_LeftButtons";
            this.pnl_LeftButtons.Size = new System.Drawing.Size(191, 535);
            this.pnl_LeftButtons.TabIndex = 3;
            this.pnl_LeftButtons.Visible = false;
            // 
            // btnAppointment
            // 
            this.btnAppointment.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAppointment.BackgroundImage")));
            this.btnAppointment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAppointment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAppointment.FlatAppearance.BorderSize = 0;
            this.btnAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAppointment.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAppointment.Location = new System.Drawing.Point(0, 415);
            this.btnAppointment.Name = "btnAppointment";
            this.btnAppointment.Size = new System.Drawing.Size(191, 30);
            this.btnAppointment.TabIndex = 3;
            this.btnAppointment.Text = "      Appointment";
            this.btnAppointment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAppointment.UseVisualStyleBackColor = true;
            this.btnAppointment.Click += new System.EventHandler(this.btnAppointment_Click);
            // 
            // btnCalender
            // 
            this.btnCalender.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCalender.BackgroundImage")));
            this.btnCalender.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCalender.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnCalender.FlatAppearance.BorderSize = 0;
            this.btnCalender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalender.Location = new System.Drawing.Point(0, 445);
            this.btnCalender.Name = "btnCalender";
            this.btnCalender.Size = new System.Drawing.Size(191, 30);
            this.btnCalender.TabIndex = 2;
            this.btnCalender.Text = "      Calendar";
            this.btnCalender.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCalender.UseVisualStyleBackColor = true;
            this.btnCalender.Click += new System.EventHandler(this.btnCalender_Click);
            // 
            // btnMail
            // 
            this.btnMail.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMail.BackgroundImage")));
            this.btnMail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMail.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnMail.FlatAppearance.BorderSize = 0;
            this.btnMail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMail.Location = new System.Drawing.Point(0, 475);
            this.btnMail.Name = "btnMail";
            this.btnMail.Size = new System.Drawing.Size(191, 30);
            this.btnMail.TabIndex = 1;
            this.btnMail.Text = "      Mail";
            this.btnMail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMail.UseVisualStyleBackColor = true;
            this.btnMail.Click += new System.EventHandler(this.btnMail_Click);
            // 
            // btnTask
            // 
            this.btnTask.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnTask.BackgroundImage")));
            this.btnTask.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTask.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnTask.FlatAppearance.BorderSize = 0;
            this.btnTask.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTask.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTask.Location = new System.Drawing.Point(0, 505);
            this.btnTask.Name = "btnTask";
            this.btnTask.Size = new System.Drawing.Size(191, 30);
            this.btnTask.TabIndex = 0;
            this.btnTask.Text = "      Tasks";
            this.btnTask.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTask.UseVisualStyleBackColor = true;
            this.btnTask.Click += new System.EventHandler(this.btnTask_Click);
            // 
            // pnlLeftPatientAlert
            // 
            this.pnlLeftPatientAlert.Controls.Add(this.panel5);
            this.pnlLeftPatientAlert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeftPatientAlert.Location = new System.Drawing.Point(0, 0);
            this.pnlLeftPatientAlert.Name = "pnlLeftPatientAlert";
            this.pnlLeftPatientAlert.Size = new System.Drawing.Size(191, 535);
            this.pnlLeftPatientAlert.TabIndex = 2;
            this.pnlLeftPatientAlert.Visible = false;
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel5.BackgroundImage")));
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.label45);
            this.panel5.Controls.Add(this.label44);
            this.panel5.Controls.Add(this.label43);
            this.panel5.Controls.Add(this.label42);
            this.panel5.Controls.Add(this.label41);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(191, 23);
            this.panel5.TabIndex = 6;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.Transparent;
            this.label45.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.White;
            this.label45.Image = ((System.Drawing.Image)(resources.GetObject("label45.Image")));
            this.label45.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label45.Location = new System.Drawing.Point(1, 1);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(189, 21);
            this.label45.TabIndex = 24;
            this.label45.Text = "     Provider";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.label44.Dock = System.Windows.Forms.DockStyle.Right;
            this.label44.Location = new System.Drawing.Point(190, 1);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(1, 21);
            this.label44.TabIndex = 23;
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.label43.Dock = System.Windows.Forms.DockStyle.Left;
            this.label43.Location = new System.Drawing.Point(0, 1);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(1, 21);
            this.label43.TabIndex = 22;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.label42.Dock = System.Windows.Forms.DockStyle.Top;
            this.label42.Location = new System.Drawing.Point(0, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(191, 1);
            this.label42.TabIndex = 21;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.label41.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label41.Location = new System.Drawing.Point(0, 22);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(191, 1);
            this.label41.TabIndex = 20;
            // 
            // pnlEligibilityCheck
            // 
            this.pnlEligibilityCheck.Controls.Add(this.c1EligibilityCheck);
            this.pnlEligibilityCheck.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEligibilityCheck.Location = new System.Drawing.Point(0, 25);
            this.pnlEligibilityCheck.Name = "pnlEligibilityCheck";
            this.pnlEligibilityCheck.Padding = new System.Windows.Forms.Padding(3);
            this.pnlEligibilityCheck.Size = new System.Drawing.Size(191, 25);
            this.pnlEligibilityCheck.TabIndex = 9;
            // 
            // c1EligibilityCheck
            // 
            this.c1EligibilityCheck.AllowEditing = false;
            this.c1EligibilityCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1EligibilityCheck.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1EligibilityCheck.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.c1EligibilityCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1EligibilityCheck.EditOptions = C1.Win.C1FlexGrid.EditFlags.None;
            this.c1EligibilityCheck.ExtendLastCol = true;
            this.c1EligibilityCheck.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
            this.c1EligibilityCheck.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1EligibilityCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1EligibilityCheck.HighLight = C1.Win.C1FlexGrid.HighLightEnum.Never;
            this.c1EligibilityCheck.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.None;
            this.c1EligibilityCheck.Location = new System.Drawing.Point(3, 3);
            this.c1EligibilityCheck.Name = "c1EligibilityCheck";
            this.c1EligibilityCheck.Rows.Count = 0;
            this.c1EligibilityCheck.Rows.DefaultSize = 19;
            this.c1EligibilityCheck.Rows.Fixed = 0;
            this.c1EligibilityCheck.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.c1EligibilityCheck.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            this.c1EligibilityCheck.ShowCellLabels = true;
            this.c1EligibilityCheck.Size = new System.Drawing.Size(185, 19);
            this.c1EligibilityCheck.StyleInfo = resources.GetString("c1EligibilityCheck.StyleInfo");
            this.c1EligibilityCheck.TabIndex = 5;
            this.c1EligibilityCheck.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1EligibilityCheck.Tree.NodeImageCollapsed")));
            this.c1EligibilityCheck.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1EligibilityCheck.Tree.NodeImageExpanded")));
            // 
            // pnlCopayAlert
            // 
            this.pnlCopayAlert.Controls.Add(this.c1CopayAlert);
            this.pnlCopayAlert.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCopayAlert.Location = new System.Drawing.Point(0, 0);
            this.pnlCopayAlert.Name = "pnlCopayAlert";
            this.pnlCopayAlert.Padding = new System.Windows.Forms.Padding(3);
            this.pnlCopayAlert.Size = new System.Drawing.Size(191, 25);
            this.pnlCopayAlert.TabIndex = 7;
            // 
            // c1CopayAlert
            // 
            this.c1CopayAlert.AllowEditing = false;
            this.c1CopayAlert.BackColor = System.Drawing.Color.White;
            this.c1CopayAlert.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1CopayAlert.ColumnInfo = "0,0,0,0,0,95,Columns:";
            this.c1CopayAlert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1CopayAlert.EditOptions = C1.Win.C1FlexGrid.EditFlags.None;
            this.c1CopayAlert.ExtendLastCol = true;
            this.c1CopayAlert.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1CopayAlert.Location = new System.Drawing.Point(3, 3);
            this.c1CopayAlert.Name = "c1CopayAlert";
            this.c1CopayAlert.Rows.Count = 5;
            this.c1CopayAlert.Rows.DefaultSize = 19;
            this.c1CopayAlert.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.c1CopayAlert.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1CopayAlert.ShowButtons = C1.Win.C1FlexGrid.ShowButtonsEnum.Always;
            this.c1CopayAlert.ShowCellLabels = true;
            this.c1CopayAlert.Size = new System.Drawing.Size(185, 19);
            this.c1CopayAlert.StyleInfo = resources.GetString("c1CopayAlert.StyleInfo");
            this.c1CopayAlert.TabIndex = 8;
            this.c1CopayAlert.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1CopayAlert.Tree.NodeImageCollapsed")));
            this.c1CopayAlert.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1CopayAlert.Tree.NodeImageExpanded")));
            // 
            // pnlPatient_UpComingAppointments
            // 
            this.pnlPatient_UpComingAppointments.CaptionFormatStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pnlPatient_UpComingAppointments.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark;
            this.pnlPatient_UpComingAppointments.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
            this.pnlPatient_UpComingAppointments.FloatingLocation = new System.Drawing.Point(813, 534);
            this.pnlPatient_UpComingAppointments.Icon = ((System.Drawing.Icon)(resources.GetObject("pnlPatient_UpComingAppointments.Icon")));
            this.pnlPatient_UpComingAppointments.InnerContainer = this.pnlPatient_UpComingAppointmentsContainer;
            this.pnlPatient_UpComingAppointments.InnerContainerFormatStyle.BackColor = System.Drawing.Color.GhostWhite;
            this.pnlPatient_UpComingAppointments.Location = new System.Drawing.Point(1113, 93);
            this.pnlPatient_UpComingAppointments.Name = "pnlPatient_UpComingAppointments";
            this.pnlPatient_UpComingAppointments.Size = new System.Drawing.Size(128, 609);
            this.pnlPatient_UpComingAppointments.TabIndex = 4;
            this.pnlPatient_UpComingAppointments.Text = "Patient Status";
            // 
            // pnlPatient_UpComingAppointmentsContainer
            // 
            this.pnlPatient_UpComingAppointmentsContainer.Controls.Add(this._picBkg);
            this.pnlPatient_UpComingAppointmentsContainer.Controls.Add(this.c1PatientStatus);
            this.pnlPatient_UpComingAppointmentsContainer.Location = new System.Drawing.Point(5, 23);
            this.pnlPatient_UpComingAppointmentsContainer.Name = "pnlPatient_UpComingAppointmentsContainer";
            this.pnlPatient_UpComingAppointmentsContainer.Size = new System.Drawing.Size(122, 585);
            this.pnlPatient_UpComingAppointmentsContainer.TabIndex = 0;
            this.pnlPatient_UpComingAppointmentsContainer.Resize += new System.EventHandler(this.pnlPatient_UpComingAppointmentsContainer_Resize);
            // 
            // _picBkg
            // 
            this._picBkg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this._picBkg.Image = ((System.Drawing.Image)(resources.GetObject("_picBkg.Image")));
            this._picBkg.Location = new System.Drawing.Point(-61, 3);
            this._picBkg.Name = "_picBkg";
            this._picBkg.Size = new System.Drawing.Size(184, 50);
            this._picBkg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._picBkg.TabIndex = 6;
            this._picBkg.TabStop = false;
            this._picBkg.Visible = false;
            // 
            // c1PatientStatus
            // 
            this.c1PatientStatus.BackColor = System.Drawing.Color.White;
            this.c1PatientStatus.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PatientStatus.ColumnInfo = "10,0,0,0,0,95,Columns:";
            this.c1PatientStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PatientStatus.EditOptions = C1.Win.C1FlexGrid.EditFlags.None;
            this.c1PatientStatus.ExtendLastCol = true;
            this.c1PatientStatus.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.Heavy;
            this.c1PatientStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1PatientStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PatientStatus.KeyActionEnter = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1PatientStatus.KeyActionTab = C1.Win.C1FlexGrid.KeyActionEnum.MoveAcrossOut;
            this.c1PatientStatus.Location = new System.Drawing.Point(0, 0);
            this.c1PatientStatus.Name = "c1PatientStatus";
            this.c1PatientStatus.Rows.Count = 5;
            this.c1PatientStatus.Rows.DefaultSize = 19;
            this.c1PatientStatus.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PatientStatus.ShowCellLabels = true;
            this.c1PatientStatus.Size = new System.Drawing.Size(122, 585);
            this.c1PatientStatus.StyleInfo = resources.GetString("c1PatientStatus.StyleInfo");
            this.c1PatientStatus.TabIndex = 7;
            this.c1PatientStatus.Tag = "Daily Close";
            this.c1PatientStatus.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1PatientStatus.Tree.NodeImageCollapsed")));
            this.c1PatientStatus.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1PatientStatus.Tree.NodeImageExpanded")));
            this.c1PatientStatus.OwnerDrawCell += new C1.Win.C1FlexGrid.OwnerDrawCellEventHandler(this.c1PatientStatus_OwnerDrawCell);
            this.c1PatientStatus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1PatientStatus_MouseDown);
            // 
            // pnlPatient_Details
            // 
            this.pnlPatient_Details.CaptionFormatStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pnlPatient_Details.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark;
            this.pnlPatient_Details.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
            this.pnlPatient_Details.Icon = ((System.Drawing.Icon)(resources.GetObject("pnlPatient_Details.Icon")));
            this.pnlPatient_Details.InnerContainer = this.pnlPatient_DetailsContainer;
            this.pnlPatient_Details.InnerContainerFormatStyle.BackColor = System.Drawing.Color.GhostWhite;
            this.pnlPatient_Details.Location = new System.Drawing.Point(199, 565);
            this.pnlPatient_Details.Name = "pnlPatient_Details";
            this.pnlPatient_Details.Size = new System.Drawing.Size(914, 137);
            this.pnlPatient_Details.TabIndex = 4;
            this.pnlPatient_Details.Text = "Patient Details";
            // 
            // pnlPatient_DetailsContainer
            // 
            this.pnlPatient_DetailsContainer.Controls.Add(this.c1PatientDetails);
            this.pnlPatient_DetailsContainer.Controls.Add(this.pnlSearchFilter);
            this.pnlPatient_DetailsContainer.Controls.Add(this.ts_PatientDetail);
            this.pnlPatient_DetailsContainer.Location = new System.Drawing.Point(1, 27);
            this.pnlPatient_DetailsContainer.Name = "pnlPatient_DetailsContainer";
            this.pnlPatient_DetailsContainer.Size = new System.Drawing.Size(912, 109);
            this.pnlPatient_DetailsContainer.TabIndex = 0;
            // 
            // c1PatientDetails
            // 
            this.c1PatientDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.c1PatientDetails.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.None;
            this.c1PatientDetails.ColumnInfo = "10,0,0,0,0,95,Columns:";
            this.c1PatientDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c1PatientDetails.EditOptions = C1.Win.C1FlexGrid.EditFlags.None;
            this.c1PatientDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c1PatientDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.c1PatientDetails.Location = new System.Drawing.Point(0, 53);
            this.c1PatientDetails.Name = "c1PatientDetails";
            this.c1PatientDetails.Rows.Count = 5;
            this.c1PatientDetails.Rows.DefaultSize = 19;
            this.c1PatientDetails.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            this.c1PatientDetails.Size = new System.Drawing.Size(912, 56);
            this.c1PatientDetails.StyleInfo = resources.GetString("c1PatientDetails.StyleInfo");
            this.c1PatientDetails.TabIndex = 0;
            this.c1PatientDetails.Tree.NodeImageCollapsed = ((System.Drawing.Image)(resources.GetObject("c1PatientDetails.Tree.NodeImageCollapsed")));
            this.c1PatientDetails.Tree.NodeImageExpanded = ((System.Drawing.Image)(resources.GetObject("c1PatientDetails.Tree.NodeImageExpanded")));
            this.c1PatientDetails.AfterSort += new C1.Win.C1FlexGrid.SortColEventHandler(this.c1PatientDetails_AfterSort);
            this.c1PatientDetails.AfterResizeColumn += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1PatientDetails_AfterResizeColumn);
            this.c1PatientDetails.RowColChange += new System.EventHandler(this.c1PatientDetails_RowColChange);
            this.c1PatientDetails.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.c1PatientDetails_MouseDoubleClick);
            this.c1PatientDetails.MouseDown += new System.Windows.Forms.MouseEventHandler(this.c1PatientDetails_MouseDown);
            this.c1PatientDetails.MouseMove += new System.Windows.Forms.MouseEventHandler(this.c1PatientDetails_MouseMove);
            // 
            // pnlSearchFilter
            // 
            this.pnlSearchFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlSearchFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlSearchFilter.Controls.Add(this.panel14);
            this.pnlSearchFilter.Controls.Add(this.label62);
            this.pnlSearchFilter.Controls.Add(this.panel15);
            this.pnlSearchFilter.Controls.Add(this.lblStatus);
            this.pnlSearchFilter.Controls.Add(this.panel16);
            this.pnlSearchFilter.Controls.Add(this.lblto);
            this.pnlSearchFilter.Controls.Add(this.panel17);
            this.pnlSearchFilter.Controls.Add(this.chkApptDate);
            this.pnlSearchFilter.Controls.Add(this.lblFrom);
            this.pnlSearchFilter.Controls.Add(this.panel18);
            this.pnlSearchFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchFilter.Location = new System.Drawing.Point(0, 25);
            this.pnlSearchFilter.Name = "pnlSearchFilter";
            this.pnlSearchFilter.Size = new System.Drawing.Size(912, 28);
            this.pnlSearchFilter.TabIndex = 22;
            this.pnlSearchFilter.Visible = false;
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.cmbProvider);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel14.Location = new System.Drawing.Point(620, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(155, 28);
            this.panel14.TabIndex = 13;
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Location = new System.Drawing.Point(4, 3);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(150, 22);
            this.cmbProvider.TabIndex = 8;
            this.cmbProvider.SelectedIndexChanged += new System.EventHandler(this.cmbProvider_SelectedIndexChanged);
            // 
            // label62
            // 
            this.label62.BackColor = System.Drawing.Color.Transparent;
            this.label62.Dock = System.Windows.Forms.DockStyle.Left;
            this.label62.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label62.Location = new System.Drawing.Point(550, 0);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(70, 28);
            this.label62.TabIndex = 7;
            this.label62.Text = "Provider :";
            this.label62.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.cmbStatus);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel15.Location = new System.Drawing.Point(440, 0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(110, 28);
            this.panel15.TabIndex = 12;
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(3, 3);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(100, 22);
            this.cmbStatus.TabIndex = 1;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblStatus.Location = new System.Drawing.Point(380, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(60, 28);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status :";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.dtpToDate);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel16.Location = new System.Drawing.Point(269, 0);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(111, 28);
            this.panel16.TabIndex = 10;
            // 
            // dtpToDate
            // 
            this.dtpToDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpToDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpToDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpToDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpToDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpToDate.CustomFormat = "MM/dd/yyyy";
            this.dtpToDate.Enabled = false;
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(3, 3);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(100, 22);
            this.dtpToDate.TabIndex = 5;
            this.dtpToDate.ValueChanged += new System.EventHandler(this.dtpToDate_ValueChanged);
            // 
            // lblto
            // 
            this.lblto.BackColor = System.Drawing.Color.Transparent;
            this.lblto.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblto.Location = new System.Drawing.Point(234, 0);
            this.lblto.Name = "lblto";
            this.lblto.Size = new System.Drawing.Size(35, 28);
            this.lblto.TabIndex = 2;
            this.lblto.Text = "To :";
            this.lblto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.dtpFromDate);
            this.panel17.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel17.Location = new System.Drawing.Point(124, 0);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(110, 28);
            this.panel17.TabIndex = 11;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CalendarForeColor = System.Drawing.Color.Maroon;
            this.dtpFromDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtpFromDate.CalendarTitleBackColor = System.Drawing.Color.Orange;
            this.dtpFromDate.CalendarTitleForeColor = System.Drawing.Color.Brown;
            this.dtpFromDate.CalendarTrailingForeColor = System.Drawing.Color.Tomato;
            this.dtpFromDate.CustomFormat = "MM/dd/yyyy";
            this.dtpFromDate.Enabled = false;
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(3, 3);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(100, 22);
            this.dtpFromDate.TabIndex = 3;
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.dtpFromDate_ValueChanged);
            // 
            // chkApptDate
            // 
            this.chkApptDate.AutoSize = true;
            this.chkApptDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkApptDate.Location = new System.Drawing.Point(109, 0);
            this.chkApptDate.Name = "chkApptDate";
            this.chkApptDate.Size = new System.Drawing.Size(15, 28);
            this.chkApptDate.TabIndex = 1;
            this.chkApptDate.UseVisualStyleBackColor = true;
            this.chkApptDate.CheckedChanged += new System.EventHandler(this.chkApptDate_CheckedChanged);
            // 
            // lblFrom
            // 
            this.lblFrom.BackColor = System.Drawing.Color.Transparent;
            this.lblFrom.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblFrom.Location = new System.Drawing.Point(66, 0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(43, 28);
            this.lblFrom.TabIndex = 4;
            this.lblFrom.Text = "From :";
            this.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel18
            // 
            this.panel18.Controls.Add(this.label63);
            this.panel18.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel18.Location = new System.Drawing.Point(0, 0);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(66, 28);
            this.panel18.TabIndex = 9;
            // 
            // label63
            // 
            this.label63.Dock = System.Windows.Forms.DockStyle.Left;
            this.label63.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label63.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label63.Location = new System.Drawing.Point(0, 0);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(63, 28);
            this.label63.TabIndex = 0;
            this.label63.Text = "Search :";
            this.label63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ts_PatientDetail
            // 
            this.ts_PatientDetail.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ts_PatientDetail.BackgroundImage")));
            this.ts_PatientDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ts_PatientDetail.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts_PatientDetail.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_PD_Insurance,
            this.tsb_PD_Appointments,
            this.tsb_PD_Referral,
            this.tsb_PD_Procedure,
            this.tsb_PD_PriorAuthorization,
            this.tsb_PD_Billing,
            this.tsb_PD_Cases,
            this.tsb_PatientTasks,
            this.tsb_PDViewDocument,
            this.tsb_NYWCForms});
            this.ts_PatientDetail.Location = new System.Drawing.Point(0, 0);
            this.ts_PatientDetail.Name = "ts_PatientDetail";
            this.ts_PatientDetail.Size = new System.Drawing.Size(912, 25);
            this.ts_PatientDetail.TabIndex = 1;
            this.ts_PatientDetail.Text = "toolStrip2";
            this.ts_PatientDetail.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_PatientDetail_ItemClicked);
            this.ts_PatientDetail.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ts_PatientDetail_MouseDown);
            // 
            // tsb_PD_Insurance
            // 
            this.tsb_PD_Insurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PD_Insurance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PD_Insurance.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PD_Insurance.Image")));
            this.tsb_PD_Insurance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PD_Insurance.Name = "tsb_PD_Insurance";
            this.tsb_PD_Insurance.Size = new System.Drawing.Size(91, 22);
            this.tsb_PD_Insurance.Tag = " Insurance";
            this.tsb_PD_Insurance.Text = " Insurance";
            this.tsb_PD_Insurance.Click += new System.EventHandler(this.tsb_PD_Insurance_Click);
            // 
            // tsb_PD_Appointments
            // 
            this.tsb_PD_Appointments.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PD_Appointments.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PD_Appointments.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PD_Appointments.Image")));
            this.tsb_PD_Appointments.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PD_Appointments.Name = "tsb_PD_Appointments";
            this.tsb_PD_Appointments.Size = new System.Drawing.Size(119, 22);
            this.tsb_PD_Appointments.Tag = " Appointments";
            this.tsb_PD_Appointments.Text = " Appointments";
            this.tsb_PD_Appointments.Click += new System.EventHandler(this.tsb_PD_Appointments_Click);
            // 
            // tsb_PD_Referral
            // 
            this.tsb_PD_Referral.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PD_Referral.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PD_Referral.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PD_Referral.Image")));
            this.tsb_PD_Referral.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PD_Referral.Name = "tsb_PD_Referral";
            this.tsb_PD_Referral.Size = new System.Drawing.Size(143, 22);
            this.tsb_PD_Referral.Tag = " Referring Provider";
            this.tsb_PD_Referral.Text = " Referring Provider";
            this.tsb_PD_Referral.Click += new System.EventHandler(this.tsb_PD_Referral_Click);
            // 
            // tsb_PD_Procedure
            // 
            this.tsb_PD_Procedure.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PD_Procedure.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PD_Procedure.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PD_Procedure.Image")));
            this.tsb_PD_Procedure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PD_Procedure.Name = "tsb_PD_Procedure";
            this.tsb_PD_Procedure.Size = new System.Drawing.Size(99, 22);
            this.tsb_PD_Procedure.Tag = " Procedures";
            this.tsb_PD_Procedure.Text = " Procedures";
            this.tsb_PD_Procedure.Visible = false;
            this.tsb_PD_Procedure.Click += new System.EventHandler(this.tsb_PD_Procedure_Click);
            // 
            // tsb_PD_PriorAuthorization
            // 
            this.tsb_PD_PriorAuthorization.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_PD_PriorAuthorization.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PD_PriorAuthorization.Image")));
            this.tsb_PD_PriorAuthorization.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PD_PriorAuthorization.Name = "tsb_PD_PriorAuthorization";
            this.tsb_PD_PriorAuthorization.Size = new System.Drawing.Size(145, 22);
            this.tsb_PD_PriorAuthorization.Tag = "Prior Authorization";
            this.tsb_PD_PriorAuthorization.Text = "Prior Authorization";
            this.tsb_PD_PriorAuthorization.Click += new System.EventHandler(this.tsb_PD_PriorAuthorization_Click);
            // 
            // tsb_PD_Billing
            // 
            this.tsb_PD_Billing.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PD_Billing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PD_Billing.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PD_Billing.Image")));
            this.tsb_PD_Billing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PD_Billing.Name = "tsb_PD_Billing";
            this.tsb_PD_Billing.Size = new System.Drawing.Size(67, 22);
            this.tsb_PD_Billing.Tag = " Billing";
            this.tsb_PD_Billing.Text = " Billing";
            this.tsb_PD_Billing.Click += new System.EventHandler(this.tsb_PD_Billing_Click);
            // 
            // tsb_PD_Cases
            // 
            this.tsb_PD_Cases.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PD_Cases.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PD_Cases.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PD_Cases.Image")));
            this.tsb_PD_Cases.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PD_Cases.Name = "tsb_PD_Cases";
            this.tsb_PD_Cases.Size = new System.Drawing.Size(61, 22);
            this.tsb_PD_Cases.Tag = "Cases";
            this.tsb_PD_Cases.Text = "Cases";
            this.tsb_PD_Cases.Click += new System.EventHandler(this.tsb_PD_Cases_Click);
            // 
            // tsb_PatientTasks
            // 
            this.tsb_PatientTasks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tsb_PatientTasks.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_PatientTasks.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_PatientTasks.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PatientTasks.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PatientTasks.Image")));
            this.tsb_PatientTasks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PatientTasks.Name = "tsb_PatientTasks";
            this.tsb_PatientTasks.Size = new System.Drawing.Size(60, 22);
            this.tsb_PatientTasks.Tag = "Tasks";
            this.tsb_PatientTasks.Text = "Tasks";
            this.tsb_PatientTasks.Click += new System.EventHandler(this.tsb_PatientTasks_Click);
            // 
            // tsb_PDViewDocument
            // 
            this.tsb_PDViewDocument.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tsb_PDViewDocument.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_PDViewDocument.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsb_PDViewDocument.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_PDViewDocument.Image = ((System.Drawing.Image)(resources.GetObject("tsb_PDViewDocument.Image")));
            this.tsb_PDViewDocument.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_PDViewDocument.Name = "tsb_PDViewDocument";
            this.tsb_PDViewDocument.Size = new System.Drawing.Size(129, 22);
            this.tsb_PDViewDocument.Tag = "View Documents";
            this.tsb_PDViewDocument.Text = "View Documents";
            this.tsb_PDViewDocument.Click += new System.EventHandler(this.tsb_PDViewDocument_Click);
            // 
            // tsb_NYWCForms
            // 
            this.tsb_NYWCForms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.tsb_NYWCForms.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsb_NYWCForms.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.tsb_NYWCForms.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.tsb_NYWCForms.Image = ((System.Drawing.Image)(resources.GetObject("tsb_NYWCForms.Image")));
            this.tsb_NYWCForms.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_NYWCForms.Name = "tsb_NYWCForms";
            this.tsb_NYWCForms.Size = new System.Drawing.Size(106, 20);
            this.tsb_NYWCForms.Tag = "NY WC Forms";
            this.tsb_NYWCForms.Text = "NY WC Forms";
            this.tsb_NYWCForms.Click += new System.EventHandler(this.tsb_NYWCForms_Click);
            // 
            // pnlPatient_Demographics
            // 
            this.pnlPatient_Demographics.GroupStyle = Janus.Windows.UI.Dock.PanelGroupStyle.VerticalTiles;
            this.pnlPatient_Demographics.Location = new System.Drawing.Point(199, 236);
            this.pnlPatient_Demographics.Name = "pnlPatient_Demographics";
            this.pnlPatient_Demographics.Size = new System.Drawing.Size(914, 329);
            this.pnlPatient_Demographics.TabIndex = 31;
            // 
            // pnlPatient_Demo
            // 
            this.pnlPatient_Demo.CaptionFormatStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pnlPatient_Demo.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark;
            this.pnlPatient_Demo.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
            this.pnlPatient_Demo.FloatingLocation = new System.Drawing.Point(791, 547);
            this.pnlPatient_Demo.Icon = ((System.Drawing.Icon)(resources.GetObject("pnlPatient_Demo.Icon")));
            this.pnlPatient_Demo.InnerContainer = this.pnlPatient_DemographicsContainer;
            this.pnlPatient_Demo.InnerContainerFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(254)))));
            this.pnlPatient_Demo.InnerContainerFormatStyle.BackColorGradient = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(241)))), ((int)(((byte)(255)))));
            this.pnlPatient_Demo.InnerContainerFormatStyle.BackgroundGradientMode = Janus.Windows.UI.BackgroundGradientMode.Vertical;
            this.pnlPatient_Demo.Location = new System.Drawing.Point(0, 4);
            this.pnlPatient_Demo.Name = "pnlPatient_Demo";
            this.pnlPatient_Demo.Size = new System.Drawing.Size(512, 325);
            this.pnlPatient_Demo.TabIndex = 4;
            this.pnlPatient_Demo.Text = "Patient Demographics";
            this.pnlPatient_Demo.Click += new System.EventHandler(this.pnlPatient_Demographics_Click);
            // 
            // pnlPatient_DemographicsContainer
            // 
            this.pnlPatient_DemographicsContainer.BackColor = System.Drawing.Color.White;
            this.pnlPatient_DemographicsContainer.Controls.Add(this.gb_Demographics);
            this.pnlPatient_DemographicsContainer.Controls.Add(this.label10);
            this.pnlPatient_DemographicsContainer.Controls.Add(this.label9);
            this.pnlPatient_DemographicsContainer.Controls.Add(this.label11);
            this.pnlPatient_DemographicsContainer.Controls.Add(this.label8);
            this.pnlPatient_DemographicsContainer.Location = new System.Drawing.Point(1, 23);
            this.pnlPatient_DemographicsContainer.Name = "pnlPatient_DemographicsContainer";
            this.pnlPatient_DemographicsContainer.Size = new System.Drawing.Size(510, 301);
            this.pnlPatient_DemographicsContainer.TabIndex = 0;
            // 
            // gb_Demographics
            // 
            this.gb_Demographics.BackColor = System.Drawing.Color.Transparent;
            this.gb_Demographics.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gb_Demographics.Controls.Add(this.panel12);
            this.gb_Demographics.Controls.Add(this.panel3);
            this.gb_Demographics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_Demographics.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_Demographics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gb_Demographics.FormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gb_Demographics.Location = new System.Drawing.Point(11, 11);
            this.gb_Demographics.Name = "gb_Demographics";
            this.gb_Demographics.Size = new System.Drawing.Size(489, 279);
            this.gb_Demographics.TabIndex = 103;
            this.gb_Demographics.Text = "Demographics";
            this.gb_Demographics.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007;
            // 
            // panel12
            // 
            this.panel12.AutoScroll = true;
            this.panel12.Controls.Add(this.pnl_PD_ContactInfo);
            this.panel12.Controls.Add(this.pnl_Demo_address);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(213, 18);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(273, 258);
            this.panel12.TabIndex = 102;
            // 
            // pnl_PD_ContactInfo
            // 
            this.pnl_PD_ContactInfo.AutoSize = true;
            this.pnl_PD_ContactInfo.Controls.Add(this.pnlBusinessCenter);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_WorkPhone);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_Occupation);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_MedCat);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_PatStatus);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_Language);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_Ethinicity);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_Race);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_TertiaryInsurance);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_SecondaryInsurance);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_PrimaryInsurance);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_PD_PCPPhone);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_PD_PCPMobile);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_PD_Referral);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_PD_Physician);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_PD_Status);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_PD_Pharmacy);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_PD_Provider);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_EMmobile);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_EmPhone);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_EmContacts);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_Email);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_Fax);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_Demo_Mobile);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_HomePhone);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_Demo_Gender);
            this.pnl_PD_ContactInfo.Controls.Add(this.pnl_Demo_DOB);
            this.pnl_PD_ContactInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_PD_ContactInfo.Location = new System.Drawing.Point(0, 57);
            this.pnl_PD_ContactInfo.Name = "pnl_PD_ContactInfo";
            this.pnl_PD_ContactInfo.Size = new System.Drawing.Size(256, 432);
            this.pnl_PD_ContactInfo.TabIndex = 126;
            // 
            // pnlBusinessCenter
            // 
            this.pnlBusinessCenter.Controls.Add(this.lblBusinessCenter);
            this.pnlBusinessCenter.Controls.Add(this.lblBusinessCenterCaption);
            this.pnlBusinessCenter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBusinessCenter.Location = new System.Drawing.Point(0, 415);
            this.pnlBusinessCenter.Name = "pnlBusinessCenter";
            this.pnlBusinessCenter.Size = new System.Drawing.Size(256, 17);
            this.pnlBusinessCenter.TabIndex = 139;
            this.pnlBusinessCenter.Visible = false;
            // 
            // lblBusinessCenter
            // 
            this.lblBusinessCenter.AutoEllipsis = true;
            this.lblBusinessCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBusinessCenter.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusinessCenter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBusinessCenter.Location = new System.Drawing.Point(100, 0);
            this.lblBusinessCenter.Name = "lblBusinessCenter";
            this.lblBusinessCenter.Size = new System.Drawing.Size(156, 17);
            this.lblBusinessCenter.TabIndex = 93;
            this.lblBusinessCenter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBusinessCenterCaption
            // 
            this.lblBusinessCenterCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblBusinessCenterCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblBusinessCenterCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusinessCenterCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblBusinessCenterCaption.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBusinessCenterCaption.Location = new System.Drawing.Point(0, 0);
            this.lblBusinessCenterCaption.Name = "lblBusinessCenterCaption";
            this.lblBusinessCenterCaption.Size = new System.Drawing.Size(100, 17);
            this.lblBusinessCenterCaption.TabIndex = 76;
            this.lblBusinessCenterCaption.Text = "Bus. Center :";
            this.lblBusinessCenterCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_WorkPhone
            // 
            this.pnl_WorkPhone.Controls.Add(this.lblWorkPhone);
            this.pnl_WorkPhone.Controls.Add(this.lblWorkPhoneCaption);
            this.pnl_WorkPhone.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_WorkPhone.Location = new System.Drawing.Point(0, 398);
            this.pnl_WorkPhone.Name = "pnl_WorkPhone";
            this.pnl_WorkPhone.Size = new System.Drawing.Size(256, 17);
            this.pnl_WorkPhone.TabIndex = 138;
            this.pnl_WorkPhone.Visible = false;
            // 
            // lblWorkPhone
            // 
            this.lblWorkPhone.AutoEllipsis = true;
            this.lblWorkPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWorkPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblWorkPhone.Location = new System.Drawing.Point(100, 0);
            this.lblWorkPhone.Name = "lblWorkPhone";
            this.lblWorkPhone.Size = new System.Drawing.Size(156, 17);
            this.lblWorkPhone.TabIndex = 93;
            this.lblWorkPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblWorkPhoneCaption
            // 
            this.lblWorkPhoneCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblWorkPhoneCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblWorkPhoneCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkPhoneCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblWorkPhoneCaption.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblWorkPhoneCaption.Location = new System.Drawing.Point(0, 0);
            this.lblWorkPhoneCaption.Name = "lblWorkPhoneCaption";
            this.lblWorkPhoneCaption.Size = new System.Drawing.Size(100, 17);
            this.lblWorkPhoneCaption.TabIndex = 76;
            this.lblWorkPhoneCaption.Text = "Work Phone :";
            this.lblWorkPhoneCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_Occupation
            // 
            this.pnl_Occupation.Controls.Add(this.lblOccupation);
            this.pnl_Occupation.Controls.Add(this.lblOccupationCaption);
            this.pnl_Occupation.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Occupation.Location = new System.Drawing.Point(0, 381);
            this.pnl_Occupation.Name = "pnl_Occupation";
            this.pnl_Occupation.Size = new System.Drawing.Size(256, 17);
            this.pnl_Occupation.TabIndex = 137;
            this.pnl_Occupation.Visible = false;
            // 
            // lblOccupation
            // 
            this.lblOccupation.AutoEllipsis = true;
            this.lblOccupation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOccupation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOccupation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOccupation.Location = new System.Drawing.Point(100, 0);
            this.lblOccupation.Name = "lblOccupation";
            this.lblOccupation.Size = new System.Drawing.Size(156, 17);
            this.lblOccupation.TabIndex = 93;
            this.lblOccupation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOccupationCaption
            // 
            this.lblOccupationCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblOccupationCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblOccupationCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOccupationCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblOccupationCaption.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblOccupationCaption.Location = new System.Drawing.Point(0, 0);
            this.lblOccupationCaption.Name = "lblOccupationCaption";
            this.lblOccupationCaption.Size = new System.Drawing.Size(100, 17);
            this.lblOccupationCaption.TabIndex = 76;
            this.lblOccupationCaption.Text = "Occupation :";
            this.lblOccupationCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_MedCat
            // 
            this.pnl_MedCat.Controls.Add(this.lblPatMedCat);
            this.pnl_MedCat.Controls.Add(this.lblMedCatCaption);
            this.pnl_MedCat.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_MedCat.Location = new System.Drawing.Point(0, 364);
            this.pnl_MedCat.Name = "pnl_MedCat";
            this.pnl_MedCat.Size = new System.Drawing.Size(256, 17);
            this.pnl_MedCat.TabIndex = 136;
            this.pnl_MedCat.Visible = false;
            // 
            // lblPatMedCat
            // 
            this.lblPatMedCat.AutoEllipsis = true;
            this.lblPatMedCat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPatMedCat.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatMedCat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPatMedCat.Location = new System.Drawing.Point(100, 0);
            this.lblPatMedCat.Name = "lblPatMedCat";
            this.lblPatMedCat.Size = new System.Drawing.Size(156, 17);
            this.lblPatMedCat.TabIndex = 93;
            this.lblPatMedCat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMedCatCaption
            // 
            this.lblMedCatCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblMedCatCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblMedCatCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMedCatCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblMedCatCaption.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblMedCatCaption.Location = new System.Drawing.Point(0, 0);
            this.lblMedCatCaption.Name = "lblMedCatCaption";
            this.lblMedCatCaption.Size = new System.Drawing.Size(100, 17);
            this.lblMedCatCaption.TabIndex = 76;
            this.lblMedCatCaption.Text = "Med. Cat :";
            this.lblMedCatCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_PatStatus
            // 
            this.pnl_PatStatus.Controls.Add(this.lblPatStatus);
            this.pnl_PatStatus.Controls.Add(this.lblstCaption);
            this.pnl_PatStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_PatStatus.Location = new System.Drawing.Point(0, 350);
            this.pnl_PatStatus.Name = "pnl_PatStatus";
            this.pnl_PatStatus.Size = new System.Drawing.Size(256, 14);
            this.pnl_PatStatus.TabIndex = 135;
            this.pnl_PatStatus.Visible = false;
            // 
            // lblPatStatus
            // 
            this.lblPatStatus.AutoEllipsis = true;
            this.lblPatStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPatStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPatStatus.Location = new System.Drawing.Point(100, 0);
            this.lblPatStatus.Name = "lblPatStatus";
            this.lblPatStatus.Size = new System.Drawing.Size(156, 14);
            this.lblPatStatus.TabIndex = 93;
            this.lblPatStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblstCaption
            // 
            this.lblstCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblstCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblstCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblstCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblstCaption.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblstCaption.Location = new System.Drawing.Point(0, 0);
            this.lblstCaption.Name = "lblstCaption";
            this.lblstCaption.Size = new System.Drawing.Size(100, 14);
            this.lblstCaption.TabIndex = 76;
            this.lblstCaption.Text = "Status :";
            this.lblstCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_Language
            // 
            this.pnl_Language.Controls.Add(this.lblLanguage);
            this.pnl_Language.Controls.Add(this.lblLanguageCaption);
            this.pnl_Language.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Language.Location = new System.Drawing.Point(0, 333);
            this.pnl_Language.Name = "pnl_Language";
            this.pnl_Language.Size = new System.Drawing.Size(256, 17);
            this.pnl_Language.TabIndex = 134;
            this.pnl_Language.Visible = false;
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoEllipsis = true;
            this.lblLanguage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLanguage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLanguage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblLanguage.Location = new System.Drawing.Point(100, 0);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(156, 17);
            this.lblLanguage.TabIndex = 93;
            this.lblLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLanguageCaption
            // 
            this.lblLanguageCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblLanguageCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLanguageCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLanguageCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblLanguageCaption.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblLanguageCaption.Location = new System.Drawing.Point(0, 0);
            this.lblLanguageCaption.Name = "lblLanguageCaption";
            this.lblLanguageCaption.Size = new System.Drawing.Size(100, 17);
            this.lblLanguageCaption.TabIndex = 76;
            this.lblLanguageCaption.Text = "Language :";
            this.lblLanguageCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_Ethinicity
            // 
            this.pnl_Ethinicity.Controls.Add(this.lblEthinicity);
            this.pnl_Ethinicity.Controls.Add(this.lblEthinicityCaption);
            this.pnl_Ethinicity.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Ethinicity.Location = new System.Drawing.Point(0, 316);
            this.pnl_Ethinicity.Name = "pnl_Ethinicity";
            this.pnl_Ethinicity.Size = new System.Drawing.Size(256, 17);
            this.pnl_Ethinicity.TabIndex = 133;
            this.pnl_Ethinicity.Visible = false;
            // 
            // lblEthinicity
            // 
            this.lblEthinicity.AutoEllipsis = true;
            this.lblEthinicity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEthinicity.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEthinicity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblEthinicity.Location = new System.Drawing.Point(100, 0);
            this.lblEthinicity.Name = "lblEthinicity";
            this.lblEthinicity.Size = new System.Drawing.Size(156, 17);
            this.lblEthinicity.TabIndex = 93;
            this.lblEthinicity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEthinicityCaption
            // 
            this.lblEthinicityCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblEthinicityCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblEthinicityCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEthinicityCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblEthinicityCaption.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblEthinicityCaption.Location = new System.Drawing.Point(0, 0);
            this.lblEthinicityCaption.Name = "lblEthinicityCaption";
            this.lblEthinicityCaption.Size = new System.Drawing.Size(100, 17);
            this.lblEthinicityCaption.TabIndex = 76;
            this.lblEthinicityCaption.Text = "Ethinicity :";
            this.lblEthinicityCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_Race
            // 
            this.pnl_Race.Controls.Add(this.lblRace);
            this.pnl_Race.Controls.Add(this.lblRaceCaption);
            this.pnl_Race.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Race.Location = new System.Drawing.Point(0, 299);
            this.pnl_Race.Name = "pnl_Race";
            this.pnl_Race.Size = new System.Drawing.Size(256, 17);
            this.pnl_Race.TabIndex = 132;
            this.pnl_Race.Visible = false;
            // 
            // lblRace
            // 
            this.lblRace.AutoEllipsis = true;
            this.lblRace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRace.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRace.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRace.Location = new System.Drawing.Point(100, 0);
            this.lblRace.Name = "lblRace";
            this.lblRace.Size = new System.Drawing.Size(156, 17);
            this.lblRace.TabIndex = 93;
            this.lblRace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRaceCaption
            // 
            this.lblRaceCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblRaceCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblRaceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRaceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblRaceCaption.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblRaceCaption.Location = new System.Drawing.Point(0, 0);
            this.lblRaceCaption.Name = "lblRaceCaption";
            this.lblRaceCaption.Size = new System.Drawing.Size(100, 17);
            this.lblRaceCaption.TabIndex = 76;
            this.lblRaceCaption.Text = "Race :";
            this.lblRaceCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_TertiaryInsurance
            // 
            this.pnl_TertiaryInsurance.BackColor = System.Drawing.Color.Transparent;
            this.pnl_TertiaryInsurance.Controls.Add(this.lbl_TertiaryInsurance);
            this.pnl_TertiaryInsurance.Controls.Add(this.Label90);
            this.pnl_TertiaryInsurance.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_TertiaryInsurance.Location = new System.Drawing.Point(0, 284);
            this.pnl_TertiaryInsurance.Name = "pnl_TertiaryInsurance";
            this.pnl_TertiaryInsurance.Size = new System.Drawing.Size(256, 15);
            this.pnl_TertiaryInsurance.TabIndex = 176;
            // 
            // lbl_TertiaryInsurance
            // 
            this.lbl_TertiaryInsurance.AutoEllipsis = true;
            this.lbl_TertiaryInsurance.BackColor = System.Drawing.Color.Transparent;
            this.lbl_TertiaryInsurance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_TertiaryInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TertiaryInsurance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.lbl_TertiaryInsurance.Location = new System.Drawing.Point(100, 0);
            this.lbl_TertiaryInsurance.Name = "lbl_TertiaryInsurance";
            this.lbl_TertiaryInsurance.Size = new System.Drawing.Size(156, 15);
            this.lbl_TertiaryInsurance.TabIndex = 151;
            this.lbl_TertiaryInsurance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label90
            // 
            this.Label90.BackColor = System.Drawing.Color.Transparent;
            this.Label90.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label90.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label90.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.Label90.Location = new System.Drawing.Point(0, 0);
            this.Label90.Name = "Label90";
            this.Label90.Size = new System.Drawing.Size(100, 15);
            this.Label90.TabIndex = 150;
            this.Label90.Text = "Tertiary Ins. :";
            this.Label90.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_SecondaryInsurance
            // 
            this.pnl_SecondaryInsurance.BackColor = System.Drawing.Color.Transparent;
            this.pnl_SecondaryInsurance.Controls.Add(this.lblSecondaryInsurance);
            this.pnl_SecondaryInsurance.Controls.Add(this.Label91);
            this.pnl_SecondaryInsurance.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_SecondaryInsurance.Location = new System.Drawing.Point(0, 269);
            this.pnl_SecondaryInsurance.Name = "pnl_SecondaryInsurance";
            this.pnl_SecondaryInsurance.Size = new System.Drawing.Size(256, 15);
            this.pnl_SecondaryInsurance.TabIndex = 175;
            // 
            // lblSecondaryInsurance
            // 
            this.lblSecondaryInsurance.AutoEllipsis = true;
            this.lblSecondaryInsurance.BackColor = System.Drawing.Color.Transparent;
            this.lblSecondaryInsurance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSecondaryInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSecondaryInsurance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.lblSecondaryInsurance.Location = new System.Drawing.Point(100, 0);
            this.lblSecondaryInsurance.Name = "lblSecondaryInsurance";
            this.lblSecondaryInsurance.Size = new System.Drawing.Size(156, 15);
            this.lblSecondaryInsurance.TabIndex = 151;
            this.lblSecondaryInsurance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label91
            // 
            this.Label91.BackColor = System.Drawing.Color.Transparent;
            this.Label91.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label91.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label91.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.Label91.Location = new System.Drawing.Point(0, 0);
            this.Label91.Name = "Label91";
            this.Label91.Size = new System.Drawing.Size(100, 15);
            this.Label91.TabIndex = 150;
            this.Label91.Text = "Secondary Ins. :";
            this.Label91.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_PrimaryInsurance
            // 
            this.pnl_PrimaryInsurance.BackColor = System.Drawing.Color.Transparent;
            this.pnl_PrimaryInsurance.Controls.Add(this.lblPrimaryInsurance);
            this.pnl_PrimaryInsurance.Controls.Add(this.Label92);
            this.pnl_PrimaryInsurance.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_PrimaryInsurance.Location = new System.Drawing.Point(0, 254);
            this.pnl_PrimaryInsurance.Name = "pnl_PrimaryInsurance";
            this.pnl_PrimaryInsurance.Size = new System.Drawing.Size(256, 15);
            this.pnl_PrimaryInsurance.TabIndex = 174;
            // 
            // lblPrimaryInsurance
            // 
            this.lblPrimaryInsurance.AutoEllipsis = true;
            this.lblPrimaryInsurance.BackColor = System.Drawing.Color.Transparent;
            this.lblPrimaryInsurance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrimaryInsurance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrimaryInsurance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.lblPrimaryInsurance.Location = new System.Drawing.Point(100, 0);
            this.lblPrimaryInsurance.Name = "lblPrimaryInsurance";
            this.lblPrimaryInsurance.Size = new System.Drawing.Size(156, 15);
            this.lblPrimaryInsurance.TabIndex = 151;
            this.lblPrimaryInsurance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label92
            // 
            this.Label92.BackColor = System.Drawing.Color.Transparent;
            this.Label92.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label92.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label92.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.Label92.Location = new System.Drawing.Point(0, 0);
            this.Label92.Name = "Label92";
            this.Label92.Size = new System.Drawing.Size(100, 15);
            this.Label92.TabIndex = 150;
            this.Label92.Text = "Primary Ins. :";
            this.Label92.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_PD_PCPPhone
            // 
            this.pnl_PD_PCPPhone.Controls.Add(this.lblPD_PCP_Phone);
            this.pnl_PD_PCPPhone.Controls.Add(this.lblPD_PCP_PhoneCaption);
            this.pnl_PD_PCPPhone.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_PD_PCPPhone.Location = new System.Drawing.Point(0, 237);
            this.pnl_PD_PCPPhone.Name = "pnl_PD_PCPPhone";
            this.pnl_PD_PCPPhone.Size = new System.Drawing.Size(256, 17);
            this.pnl_PD_PCPPhone.TabIndex = 131;
            this.pnl_PD_PCPPhone.Visible = false;
            // 
            // lblPD_PCP_Phone
            // 
            this.lblPD_PCP_Phone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPD_PCP_Phone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_PCP_Phone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_PCP_Phone.Location = new System.Drawing.Point(100, 0);
            this.lblPD_PCP_Phone.Name = "lblPD_PCP_Phone";
            this.lblPD_PCP_Phone.Size = new System.Drawing.Size(156, 17);
            this.lblPD_PCP_Phone.TabIndex = 93;
            this.lblPD_PCP_Phone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPD_PCP_PhoneCaption
            // 
            this.lblPD_PCP_PhoneCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblPD_PCP_PhoneCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPD_PCP_PhoneCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_PCP_PhoneCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_PCP_PhoneCaption.Location = new System.Drawing.Point(0, 0);
            this.lblPD_PCP_PhoneCaption.Name = "lblPD_PCP_PhoneCaption";
            this.lblPD_PCP_PhoneCaption.Size = new System.Drawing.Size(100, 17);
            this.lblPD_PCP_PhoneCaption.TabIndex = 76;
            this.lblPD_PCP_PhoneCaption.Text = "PCP Phone :";
            this.lblPD_PCP_PhoneCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_PD_PCPMobile
            // 
            this.pnl_PD_PCPMobile.Controls.Add(this.lblPD_PCP_Mobile);
            this.pnl_PD_PCPMobile.Controls.Add(this.lblPD_PCP_MobileCaption);
            this.pnl_PD_PCPMobile.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_PD_PCPMobile.Location = new System.Drawing.Point(0, 220);
            this.pnl_PD_PCPMobile.Name = "pnl_PD_PCPMobile";
            this.pnl_PD_PCPMobile.Size = new System.Drawing.Size(256, 17);
            this.pnl_PD_PCPMobile.TabIndex = 130;
            this.pnl_PD_PCPMobile.Visible = false;
            // 
            // lblPD_PCP_Mobile
            // 
            this.lblPD_PCP_Mobile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPD_PCP_Mobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_PCP_Mobile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_PCP_Mobile.Location = new System.Drawing.Point(100, 0);
            this.lblPD_PCP_Mobile.Name = "lblPD_PCP_Mobile";
            this.lblPD_PCP_Mobile.Size = new System.Drawing.Size(156, 17);
            this.lblPD_PCP_Mobile.TabIndex = 93;
            this.lblPD_PCP_Mobile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPD_PCP_MobileCaption
            // 
            this.lblPD_PCP_MobileCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblPD_PCP_MobileCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPD_PCP_MobileCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_PCP_MobileCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_PCP_MobileCaption.Location = new System.Drawing.Point(0, 0);
            this.lblPD_PCP_MobileCaption.Name = "lblPD_PCP_MobileCaption";
            this.lblPD_PCP_MobileCaption.Size = new System.Drawing.Size(100, 17);
            this.lblPD_PCP_MobileCaption.TabIndex = 76;
            this.lblPD_PCP_MobileCaption.Text = "PCP Mobile :";
            this.lblPD_PCP_MobileCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_PD_Referral
            // 
            this.pnl_PD_Referral.Controls.Add(this.lblPD_Referral);
            this.pnl_PD_Referral.Controls.Add(this.lblPD_ReferralCaption);
            this.pnl_PD_Referral.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_PD_Referral.Location = new System.Drawing.Point(0, 203);
            this.pnl_PD_Referral.Name = "pnl_PD_Referral";
            this.pnl_PD_Referral.Size = new System.Drawing.Size(256, 17);
            this.pnl_PD_Referral.TabIndex = 127;
            this.pnl_PD_Referral.Visible = false;
            // 
            // lblPD_Referral
            // 
            this.lblPD_Referral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPD_Referral.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_Referral.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_Referral.Location = new System.Drawing.Point(100, 0);
            this.lblPD_Referral.Name = "lblPD_Referral";
            this.lblPD_Referral.Size = new System.Drawing.Size(156, 17);
            this.lblPD_Referral.TabIndex = 92;
            this.lblPD_Referral.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPD_ReferralCaption
            // 
            this.lblPD_ReferralCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblPD_ReferralCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPD_ReferralCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_ReferralCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_ReferralCaption.Location = new System.Drawing.Point(0, 0);
            this.lblPD_ReferralCaption.Name = "lblPD_ReferralCaption";
            this.lblPD_ReferralCaption.Size = new System.Drawing.Size(100, 17);
            this.lblPD_ReferralCaption.TabIndex = 76;
            this.lblPD_ReferralCaption.Text = "Referral :";
            this.lblPD_ReferralCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_PD_Physician
            // 
            this.pnl_PD_Physician.Controls.Add(this.lblPD_Physician);
            this.pnl_PD_Physician.Controls.Add(this.lblPD_PhysicianCaption);
            this.pnl_PD_Physician.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_PD_Physician.Location = new System.Drawing.Point(0, 186);
            this.pnl_PD_Physician.Name = "pnl_PD_Physician";
            this.pnl_PD_Physician.Size = new System.Drawing.Size(256, 17);
            this.pnl_PD_Physician.TabIndex = 128;
            // 
            // lblPD_Physician
            // 
            this.lblPD_Physician.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPD_Physician.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_Physician.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_Physician.Location = new System.Drawing.Point(100, 0);
            this.lblPD_Physician.Name = "lblPD_Physician";
            this.lblPD_Physician.Size = new System.Drawing.Size(156, 17);
            this.lblPD_Physician.TabIndex = 92;
            this.lblPD_Physician.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPD_PhysicianCaption
            // 
            this.lblPD_PhysicianCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblPD_PhysicianCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPD_PhysicianCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_PhysicianCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_PhysicianCaption.Location = new System.Drawing.Point(0, 0);
            this.lblPD_PhysicianCaption.Name = "lblPD_PhysicianCaption";
            this.lblPD_PhysicianCaption.Size = new System.Drawing.Size(100, 17);
            this.lblPD_PhysicianCaption.TabIndex = 76;
            this.lblPD_PhysicianCaption.Text = "PCP :";
            this.lblPD_PhysicianCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_PD_Status
            // 
            this.pnl_PD_Status.Controls.Add(this.lbl_PD_Status);
            this.pnl_PD_Status.Controls.Add(this.lbl_PD_Status1);
            this.pnl_PD_Status.Location = new System.Drawing.Point(29, 265);
            this.pnl_PD_Status.Name = "pnl_PD_Status";
            this.pnl_PD_Status.Size = new System.Drawing.Size(199, 20);
            this.pnl_PD_Status.TabIndex = 116;
            this.pnl_PD_Status.Visible = false;
            // 
            // lbl_PD_Status
            // 
            this.lbl_PD_Status.BackColor = System.Drawing.Color.Lime;
            this.lbl_PD_Status.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PD_Status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.lbl_PD_Status.Location = new System.Drawing.Point(77, 2);
            this.lbl_PD_Status.Name = "lbl_PD_Status";
            this.lbl_PD_Status.Size = new System.Drawing.Size(122, 14);
            this.lbl_PD_Status.TabIndex = 92;
            this.lbl_PD_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_PD_Status1
            // 
            this.lbl_PD_Status1.AutoSize = true;
            this.lbl_PD_Status1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_PD_Status1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lbl_PD_Status1.Location = new System.Drawing.Point(7, 2);
            this.lbl_PD_Status1.Name = "lbl_PD_Status1";
            this.lbl_PD_Status1.Size = new System.Drawing.Size(50, 14);
            this.lbl_PD_Status1.TabIndex = 91;
            this.lbl_PD_Status1.Text = "Status :";
            this.lbl_PD_Status1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnl_PD_Pharmacy
            // 
            this.pnl_PD_Pharmacy.Controls.Add(this.lblPD_Pharmacy);
            this.pnl_PD_Pharmacy.Controls.Add(this.lblPD_PharmacyCaption);
            this.pnl_PD_Pharmacy.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_PD_Pharmacy.Location = new System.Drawing.Point(0, 169);
            this.pnl_PD_Pharmacy.Name = "pnl_PD_Pharmacy";
            this.pnl_PD_Pharmacy.Size = new System.Drawing.Size(256, 17);
            this.pnl_PD_Pharmacy.TabIndex = 126;
            this.pnl_PD_Pharmacy.Visible = false;
            // 
            // lblPD_Pharmacy
            // 
            this.lblPD_Pharmacy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPD_Pharmacy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_Pharmacy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_Pharmacy.Location = new System.Drawing.Point(100, 0);
            this.lblPD_Pharmacy.Name = "lblPD_Pharmacy";
            this.lblPD_Pharmacy.Size = new System.Drawing.Size(156, 17);
            this.lblPD_Pharmacy.TabIndex = 92;
            this.lblPD_Pharmacy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPD_PharmacyCaption
            // 
            this.lblPD_PharmacyCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblPD_PharmacyCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPD_PharmacyCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_PharmacyCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_PharmacyCaption.Location = new System.Drawing.Point(0, 0);
            this.lblPD_PharmacyCaption.Name = "lblPD_PharmacyCaption";
            this.lblPD_PharmacyCaption.Size = new System.Drawing.Size(100, 17);
            this.lblPD_PharmacyCaption.TabIndex = 76;
            this.lblPD_PharmacyCaption.Text = "Pharmacy :";
            this.lblPD_PharmacyCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_PD_Provider
            // 
            this.pnl_PD_Provider.Controls.Add(this.lblProvider);
            this.pnl_PD_Provider.Controls.Add(this.lblProviderCaption);
            this.pnl_PD_Provider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_PD_Provider.Location = new System.Drawing.Point(0, 152);
            this.pnl_PD_Provider.Name = "pnl_PD_Provider";
            this.pnl_PD_Provider.Size = new System.Drawing.Size(256, 17);
            this.pnl_PD_Provider.TabIndex = 129;
            this.pnl_PD_Provider.Visible = false;
            // 
            // lblProvider
            // 
            this.lblProvider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProvider.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProvider.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblProvider.Location = new System.Drawing.Point(100, 0);
            this.lblProvider.Name = "lblProvider";
            this.lblProvider.Size = new System.Drawing.Size(156, 17);
            this.lblProvider.TabIndex = 92;
            this.lblProvider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProviderCaption
            // 
            this.lblProviderCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblProviderCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblProviderCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProviderCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblProviderCaption.Location = new System.Drawing.Point(0, 0);
            this.lblProviderCaption.Name = "lblProviderCaption";
            this.lblProviderCaption.Size = new System.Drawing.Size(100, 17);
            this.lblProviderCaption.TabIndex = 76;
            this.lblProviderCaption.Text = "Provider :";
            this.lblProviderCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_EMmobile
            // 
            this.pnl_EMmobile.Controls.Add(this.lblEMMobile);
            this.pnl_EMmobile.Controls.Add(this.lblEMMobileCaption);
            this.pnl_EMmobile.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_EMmobile.Location = new System.Drawing.Point(0, 135);
            this.pnl_EMmobile.Name = "pnl_EMmobile";
            this.pnl_EMmobile.Size = new System.Drawing.Size(256, 17);
            this.pnl_EMmobile.TabIndex = 123;
            this.pnl_EMmobile.Visible = false;
            // 
            // lblEMMobile
            // 
            this.lblEMMobile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEMMobile.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEMMobile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblEMMobile.Location = new System.Drawing.Point(100, 0);
            this.lblEMMobile.Name = "lblEMMobile";
            this.lblEMMobile.Size = new System.Drawing.Size(156, 17);
            this.lblEMMobile.TabIndex = 92;
            this.lblEMMobile.Text = "553-277-3586";
            this.lblEMMobile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEMMobileCaption
            // 
            this.lblEMMobileCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblEMMobileCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblEMMobileCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEMMobileCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblEMMobileCaption.Location = new System.Drawing.Point(0, 0);
            this.lblEMMobileCaption.Name = "lblEMMobileCaption";
            this.lblEMMobileCaption.Size = new System.Drawing.Size(100, 17);
            this.lblEMMobileCaption.TabIndex = 76;
            this.lblEMMobileCaption.Text = "EM.Mobile :";
            this.lblEMMobileCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_EmPhone
            // 
            this.pnl_EmPhone.Controls.Add(this.lblEMPhone);
            this.pnl_EmPhone.Controls.Add(this.lblEMPhoneCaption);
            this.pnl_EmPhone.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_EmPhone.Location = new System.Drawing.Point(0, 118);
            this.pnl_EmPhone.Name = "pnl_EmPhone";
            this.pnl_EmPhone.Size = new System.Drawing.Size(256, 17);
            this.pnl_EmPhone.TabIndex = 122;
            this.pnl_EmPhone.Visible = false;
            // 
            // lblEMPhone
            // 
            this.lblEMPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEMPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEMPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblEMPhone.Location = new System.Drawing.Point(100, 0);
            this.lblEMPhone.Name = "lblEMPhone";
            this.lblEMPhone.Size = new System.Drawing.Size(156, 17);
            this.lblEMPhone.TabIndex = 92;
            this.lblEMPhone.Text = "456-937-0336";
            this.lblEMPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEMPhoneCaption
            // 
            this.lblEMPhoneCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblEMPhoneCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblEMPhoneCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEMPhoneCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblEMPhoneCaption.Location = new System.Drawing.Point(0, 0);
            this.lblEMPhoneCaption.Name = "lblEMPhoneCaption";
            this.lblEMPhoneCaption.Size = new System.Drawing.Size(100, 17);
            this.lblEMPhoneCaption.TabIndex = 77;
            this.lblEMPhoneCaption.Text = "EM.Phone :";
            this.lblEMPhoneCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_EmContacts
            // 
            this.pnl_EmContacts.Controls.Add(this.lblEMContact);
            this.pnl_EmContacts.Controls.Add(this.lblEMContactCaption);
            this.pnl_EmContacts.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_EmContacts.Location = new System.Drawing.Point(0, 102);
            this.pnl_EmContacts.Name = "pnl_EmContacts";
            this.pnl_EmContacts.Size = new System.Drawing.Size(256, 16);
            this.pnl_EmContacts.TabIndex = 124;
            this.pnl_EmContacts.Visible = false;
            // 
            // lblEMContact
            // 
            this.lblEMContact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEMContact.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEMContact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblEMContact.Location = new System.Drawing.Point(100, 0);
            this.lblEMContact.Name = "lblEMContact";
            this.lblEMContact.Size = new System.Drawing.Size(156, 16);
            this.lblEMContact.TabIndex = 92;
            this.lblEMContact.Text = "Mr. Paul";
            this.lblEMContact.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEMContactCaption
            // 
            this.lblEMContactCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblEMContactCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblEMContactCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEMContactCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblEMContactCaption.Location = new System.Drawing.Point(0, 0);
            this.lblEMContactCaption.Name = "lblEMContactCaption";
            this.lblEMContactCaption.Size = new System.Drawing.Size(100, 16);
            this.lblEMContactCaption.TabIndex = 76;
            this.lblEMContactCaption.Text = "EM.Contact :";
            this.lblEMContactCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_Email
            // 
            this.pnl_Email.Controls.Add(this.lblPD_Email);
            this.pnl_Email.Controls.Add(this.lblPD_EmailCaption);
            this.pnl_Email.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Email.Location = new System.Drawing.Point(0, 85);
            this.pnl_Email.Name = "pnl_Email";
            this.pnl_Email.Size = new System.Drawing.Size(256, 17);
            this.pnl_Email.TabIndex = 109;
            this.pnl_Email.Visible = false;
            // 
            // lblPD_Email
            // 
            this.lblPD_Email.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPD_Email.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_Email.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_Email.Location = new System.Drawing.Point(100, 0);
            this.lblPD_Email.Name = "lblPD_Email";
            this.lblPD_Email.Size = new System.Drawing.Size(156, 17);
            this.lblPD_Email.TabIndex = 92;
            this.lblPD_Email.Text = "marie.ronald@clinic.com";
            this.lblPD_Email.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPD_EmailCaption
            // 
            this.lblPD_EmailCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblPD_EmailCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPD_EmailCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_EmailCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_EmailCaption.Location = new System.Drawing.Point(0, 0);
            this.lblPD_EmailCaption.Name = "lblPD_EmailCaption";
            this.lblPD_EmailCaption.Size = new System.Drawing.Size(100, 17);
            this.lblPD_EmailCaption.TabIndex = 77;
            this.lblPD_EmailCaption.Text = "Email :";
            this.lblPD_EmailCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_Fax
            // 
            this.pnl_Fax.Controls.Add(this.lblPD_FaxPhone);
            this.pnl_Fax.Controls.Add(this.lblPD_FaxPhoneCaption);
            this.pnl_Fax.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Fax.Location = new System.Drawing.Point(0, 68);
            this.pnl_Fax.Name = "pnl_Fax";
            this.pnl_Fax.Size = new System.Drawing.Size(256, 17);
            this.pnl_Fax.TabIndex = 109;
            this.pnl_Fax.Visible = false;
            // 
            // lblPD_FaxPhone
            // 
            this.lblPD_FaxPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPD_FaxPhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_FaxPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_FaxPhone.Location = new System.Drawing.Point(100, 0);
            this.lblPD_FaxPhone.Name = "lblPD_FaxPhone";
            this.lblPD_FaxPhone.Size = new System.Drawing.Size(156, 17);
            this.lblPD_FaxPhone.TabIndex = 92;
            this.lblPD_FaxPhone.Text = "253-237-0369";
            this.lblPD_FaxPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPD_FaxPhoneCaption
            // 
            this.lblPD_FaxPhoneCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblPD_FaxPhoneCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPD_FaxPhoneCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_FaxPhoneCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_FaxPhoneCaption.Location = new System.Drawing.Point(0, 0);
            this.lblPD_FaxPhoneCaption.Name = "lblPD_FaxPhoneCaption";
            this.lblPD_FaxPhoneCaption.Size = new System.Drawing.Size(100, 17);
            this.lblPD_FaxPhoneCaption.TabIndex = 80;
            this.lblPD_FaxPhoneCaption.Text = "Fax :";
            this.lblPD_FaxPhoneCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_Demo_Mobile
            // 
            this.pnl_Demo_Mobile.Controls.Add(this.lblPD_MobilePhone);
            this.pnl_Demo_Mobile.Controls.Add(this.lblPD_MobilePhoneCaption);
            this.pnl_Demo_Mobile.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Demo_Mobile.Location = new System.Drawing.Point(0, 51);
            this.pnl_Demo_Mobile.Name = "pnl_Demo_Mobile";
            this.pnl_Demo_Mobile.Size = new System.Drawing.Size(256, 17);
            this.pnl_Demo_Mobile.TabIndex = 120;
            this.pnl_Demo_Mobile.Visible = false;
            // 
            // lblPD_MobilePhone
            // 
            this.lblPD_MobilePhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPD_MobilePhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_MobilePhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_MobilePhone.Location = new System.Drawing.Point(100, 0);
            this.lblPD_MobilePhone.Name = "lblPD_MobilePhone";
            this.lblPD_MobilePhone.Size = new System.Drawing.Size(156, 17);
            this.lblPD_MobilePhone.TabIndex = 92;
            this.lblPD_MobilePhone.Text = "553-277-3586";
            this.lblPD_MobilePhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPD_MobilePhoneCaption
            // 
            this.lblPD_MobilePhoneCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblPD_MobilePhoneCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPD_MobilePhoneCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_MobilePhoneCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_MobilePhoneCaption.Location = new System.Drawing.Point(0, 0);
            this.lblPD_MobilePhoneCaption.Name = "lblPD_MobilePhoneCaption";
            this.lblPD_MobilePhoneCaption.Size = new System.Drawing.Size(100, 17);
            this.lblPD_MobilePhoneCaption.TabIndex = 76;
            this.lblPD_MobilePhoneCaption.Text = "Mobile :";
            this.lblPD_MobilePhoneCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_HomePhone
            // 
            this.pnl_HomePhone.Controls.Add(this.lblPD_HomePhone);
            this.pnl_HomePhone.Controls.Add(this.lblPD_HomePhoneCaption);
            this.pnl_HomePhone.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_HomePhone.Location = new System.Drawing.Point(0, 34);
            this.pnl_HomePhone.Name = "pnl_HomePhone";
            this.pnl_HomePhone.Size = new System.Drawing.Size(256, 17);
            this.pnl_HomePhone.TabIndex = 121;
            this.pnl_HomePhone.Visible = false;
            // 
            // lblPD_HomePhone
            // 
            this.lblPD_HomePhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPD_HomePhone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_HomePhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_HomePhone.Location = new System.Drawing.Point(100, 0);
            this.lblPD_HomePhone.Name = "lblPD_HomePhone";
            this.lblPD_HomePhone.Size = new System.Drawing.Size(156, 17);
            this.lblPD_HomePhone.TabIndex = 91;
            this.lblPD_HomePhone.Text = "456-937-0336";
            this.lblPD_HomePhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPD_HomePhoneCaption
            // 
            this.lblPD_HomePhoneCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblPD_HomePhoneCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPD_HomePhoneCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_HomePhoneCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_HomePhoneCaption.Location = new System.Drawing.Point(0, 0);
            this.lblPD_HomePhoneCaption.Name = "lblPD_HomePhoneCaption";
            this.lblPD_HomePhoneCaption.Size = new System.Drawing.Size(100, 17);
            this.lblPD_HomePhoneCaption.TabIndex = 75;
            this.lblPD_HomePhoneCaption.Text = "Phone :";
            this.lblPD_HomePhoneCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_Demo_Gender
            // 
            this.pnl_Demo_Gender.Controls.Add(this.lblPD_Gender);
            this.pnl_Demo_Gender.Controls.Add(this.lblGenderCaption);
            this.pnl_Demo_Gender.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Demo_Gender.Location = new System.Drawing.Point(0, 17);
            this.pnl_Demo_Gender.Name = "pnl_Demo_Gender";
            this.pnl_Demo_Gender.Size = new System.Drawing.Size(256, 17);
            this.pnl_Demo_Gender.TabIndex = 125;
            this.pnl_Demo_Gender.Visible = false;
            // 
            // lblPD_Gender
            // 
            this.lblPD_Gender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPD_Gender.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_Gender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_Gender.Location = new System.Drawing.Point(100, 0);
            this.lblPD_Gender.Name = "lblPD_Gender";
            this.lblPD_Gender.Size = new System.Drawing.Size(156, 17);
            this.lblPD_Gender.TabIndex = 90;
            this.lblPD_Gender.Text = "Male";
            this.lblPD_Gender.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGenderCaption
            // 
            this.lblGenderCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblGenderCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGenderCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblGenderCaption.Location = new System.Drawing.Point(0, 0);
            this.lblGenderCaption.Name = "lblGenderCaption";
            this.lblGenderCaption.Size = new System.Drawing.Size(100, 17);
            this.lblGenderCaption.TabIndex = 89;
            this.lblGenderCaption.Text = "Gender :";
            this.lblGenderCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_Demo_DOB
            // 
            this.pnl_Demo_DOB.Controls.Add(this.lblPD_DateofBirth);
            this.pnl_Demo_DOB.Controls.Add(this.lblPD_DOBCaption);
            this.pnl_Demo_DOB.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Demo_DOB.Location = new System.Drawing.Point(0, 0);
            this.pnl_Demo_DOB.Name = "pnl_Demo_DOB";
            this.pnl_Demo_DOB.Size = new System.Drawing.Size(256, 17);
            this.pnl_Demo_DOB.TabIndex = 119;
            this.pnl_Demo_DOB.Visible = false;
            // 
            // lblPD_DateofBirth
            // 
            this.lblPD_DateofBirth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPD_DateofBirth.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_DateofBirth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_DateofBirth.Location = new System.Drawing.Point(100, 0);
            this.lblPD_DateofBirth.Name = "lblPD_DateofBirth";
            this.lblPD_DateofBirth.Size = new System.Drawing.Size(156, 17);
            this.lblPD_DateofBirth.TabIndex = 90;
            this.lblPD_DateofBirth.Text = "01-01-1985";
            this.lblPD_DateofBirth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPD_DOBCaption
            // 
            this.lblPD_DOBCaption.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblPD_DOBCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_DOBCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_DOBCaption.Location = new System.Drawing.Point(0, 0);
            this.lblPD_DOBCaption.Name = "lblPD_DOBCaption";
            this.lblPD_DOBCaption.Size = new System.Drawing.Size(100, 17);
            this.lblPD_DOBCaption.TabIndex = 89;
            this.lblPD_DOBCaption.Text = "DOB :";
            this.lblPD_DOBCaption.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnl_Demo_address
            // 
            this.pnl_Demo_address.Controls.Add(this.lblPD_Address);
            this.pnl_Demo_address.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Demo_address.Location = new System.Drawing.Point(0, 0);
            this.pnl_Demo_address.Name = "pnl_Demo_address";
            this.pnl_Demo_address.Size = new System.Drawing.Size(256, 57);
            this.pnl_Demo_address.TabIndex = 118;
            // 
            // lblPD_Address
            // 
            this.lblPD_Address.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_Address.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_Address.Location = new System.Drawing.Point(5, 5);
            this.lblPD_Address.Name = "lblPD_Address";
            this.lblPD_Address.Size = new System.Drawing.Size(225, 47);
            this.lblPD_Address.TabIndex = 90;
            this.lblPD_Address.Text = "AddressLine1 AddressLine2 City, State ZIP#";
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.panel8);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(3, 18);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(210, 258);
            this.panel3.TabIndex = 117;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.pnlBadDebt);
            this.panel8.Controls.Add(this.panel11);
            this.panel8.Controls.Add(this.panel10);
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 153);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(210, 103);
            this.panel8.TabIndex = 112;
            // 
            // pnlBadDebt
            // 
            this.pnlBadDebt.Controls.Add(this.label2);
            this.pnlBadDebt.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBadDebt.Location = new System.Drawing.Point(0, 57);
            this.pnlBadDebt.Name = "pnlBadDebt";
            this.pnlBadDebt.Size = new System.Drawing.Size(210, 19);
            this.pnlBadDebt.TabIndex = 114;
            this.pnlBadDebt.Visible = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 19);
            this.label2.TabIndex = 109;
            this.label2.Text = "BAD DEBT";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.lblPD_Age);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(0, 38);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(210, 19);
            this.panel11.TabIndex = 113;
            // 
            // lblPD_Age
            // 
            this.lblPD_Age.BackColor = System.Drawing.Color.Transparent;
            this.lblPD_Age.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPD_Age.Location = new System.Drawing.Point(0, 0);
            this.lblPD_Age.Name = "lblPD_Age";
            this.lblPD_Age.Size = new System.Drawing.Size(210, 19);
            this.lblPD_Age.TabIndex = 27;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.lblPD_Name);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel10.Location = new System.Drawing.Point(0, 19);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(210, 19);
            this.panel10.TabIndex = 112;
            // 
            // lblPD_Name
            // 
            this.lblPD_Name.BackColor = System.Drawing.Color.Transparent;
            this.lblPD_Name.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPD_Name.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_Name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_Name.Location = new System.Drawing.Point(0, 0);
            this.lblPD_Name.Name = "lblPD_Name";
            this.lblPD_Name.Size = new System.Drawing.Size(210, 19);
            this.lblPD_Name.TabIndex = 110;
            this.lblPD_Name.Text = "Marie Roland";
            this.lblPD_Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.lblPD_Code);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(210, 19);
            this.panel9.TabIndex = 111;
            // 
            // lblPD_Code
            // 
            this.lblPD_Code.BackColor = System.Drawing.Color.Transparent;
            this.lblPD_Code.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPD_Code.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPD_Code.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblPD_Code.Location = new System.Drawing.Point(0, 0);
            this.lblPD_Code.Name = "lblPD_Code";
            this.lblPD_Code.Size = new System.Drawing.Size(210, 19);
            this.lblPD_Code.TabIndex = 109;
            this.lblPD_Code.Text = "PN003";
            this.lblPD_Code.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.picPD_Photo);
            this.panel6.Controls.Add(this.label59);
            this.panel6.Controls.Add(this.label58);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(210, 153);
            this.panel6.TabIndex = 111;
            // 
            // picPD_Photo
            // 
            this.picPD_Photo.BackColor = System.Drawing.Color.Gainsboro;
            this.picPD_Photo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picPD_Photo.BackgroundImage")));
            this.picPD_Photo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picPD_Photo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPD_Photo.Dock = System.Windows.Forms.DockStyle.Left;
            this.picPD_Photo.Location = new System.Drawing.Point(11, 11);
            this.picPD_Photo.Name = "picPD_Photo";
            this.picPD_Photo.Size = new System.Drawing.Size(121, 132);
            this.picPD_Photo.TabIndex = 74;
            this.picPD_Photo.TabStop = false;
            this.picPD_Photo.DoubleClick += new System.EventHandler(this.picPD_Photo_DoubleClick);
            // 
            // label59
            // 
            this.label59.BackColor = System.Drawing.Color.Transparent;
            this.label59.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label59.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.ForeColor = System.Drawing.Color.DarkBlue;
            this.label59.Location = new System.Drawing.Point(11, 11);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(199, 132);
            this.label59.TabIndex = 101;
            // 
            // label58
            // 
            this.label58.BackColor = System.Drawing.Color.Transparent;
            this.label58.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label58.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.ForeColor = System.Drawing.Color.DarkBlue;
            this.label58.Location = new System.Drawing.Point(11, 143);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(199, 10);
            this.label58.TabIndex = 100;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Location = new System.Drawing.Point(11, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(199, 11);
            this.label6.TabIndex = 99;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DarkBlue;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 153);
            this.label5.TabIndex = 98;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.DarkBlue;
            this.label10.Location = new System.Drawing.Point(0, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 279);
            this.label10.TabIndex = 97;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.DarkBlue;
            this.label9.Location = new System.Drawing.Point(0, 290);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(500, 11);
            this.label9.TabIndex = 96;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Dock = System.Windows.Forms.DockStyle.Right;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DarkBlue;
            this.label11.Location = new System.Drawing.Point(500, 11);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(10, 290);
            this.label11.TabIndex = 95;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkBlue;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(510, 11);
            this.label8.TabIndex = 91;
            // 
            // pnlCards
            // 
            this.pnlCards.CaptionFormatStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pnlCards.CaptionStyle = Janus.Windows.UI.Dock.PanelCaptionStyle.Dark;
            this.pnlCards.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.False;
            this.pnlCards.FloatingLocation = new System.Drawing.Point(628, 553);
            this.pnlCards.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlCards.Icon = ((System.Drawing.Icon)(resources.GetObject("pnlCards.Icon")));
            this.pnlCards.InnerContainer = this.pnlCardsContainer;
            this.pnlCards.InnerContainerFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.pnlCards.InnerContainerFormatStyle.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.pnlCards.InnerContainerFormatStyle.FontBold = Janus.Windows.UI.TriState.False;
            this.pnlCards.Location = new System.Drawing.Point(516, 4);
            this.pnlCards.Name = "pnlCards";
            this.pnlCards.Size = new System.Drawing.Size(398, 325);
            this.pnlCards.TabIndex = 4;
            this.pnlCards.TabStateStyles.DisabledFormatStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pnlCards.TabStateStyles.FormatStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pnlCards.TabStateStyles.HotFormatStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pnlCards.TabStateStyles.PressedFormatStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pnlCards.TabStateStyles.SelectedFormatStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.pnlCards.Text = "Patient Cards";
            // 
            // pnlCardsContainer
            // 
            this.pnlCardsContainer.Controls.Add(this.gb_Cards);
            this.pnlCardsContainer.Controls.Add(this.label13);
            this.pnlCardsContainer.Controls.Add(this.label64);
            this.pnlCardsContainer.Controls.Add(this.label65);
            this.pnlCardsContainer.Controls.Add(this.label66);
            this.pnlCardsContainer.Location = new System.Drawing.Point(1, 23);
            this.pnlCardsContainer.Name = "pnlCardsContainer";
            this.pnlCardsContainer.Size = new System.Drawing.Size(396, 301);
            this.pnlCardsContainer.TabIndex = 0;
            // 
            // gb_Cards
            // 
            this.gb_Cards.BackColor = System.Drawing.Color.Transparent;
            this.gb_Cards.Controls.Add(this.panel13);
            this.gb_Cards.Controls.Add(this.panel22);
            this.gb_Cards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_Cards.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_Cards.FormatStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.gb_Cards.Location = new System.Drawing.Point(11, 11);
            this.gb_Cards.Name = "gb_Cards";
            this.gb_Cards.Size = new System.Drawing.Size(375, 279);
            this.gb_Cards.TabIndex = 100;
            this.gb_Cards.Text = "Cards";
            this.gb_Cards.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007;
            // 
            // panel13
            // 
            this.panel13.AutoScroll = true;
            this.panel13.Controls.Add(this.picPC_Cards);
            this.panel13.Controls.Add(this.label61);
            this.panel13.Controls.Add(this.panel2);
            this.panel13.Controls.Add(this.label15);
            this.panel13.Controls.Add(this.label12);
            this.panel13.Controls.Add(this.label17);
            this.panel13.Controls.Add(this.label16);
            this.panel13.Controls.Add(this.label14);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(3, 18);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(369, 236);
            this.panel13.TabIndex = 110;
            // 
            // picPC_Cards
            // 
            this.picPC_Cards.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picPC_Cards.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picPC_Cards.BackgroundImage")));
            this.picPC_Cards.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picPC_Cards.Location = new System.Drawing.Point(17, 3);
            this.picPC_Cards.Name = "picPC_Cards";
            this.picPC_Cards.Size = new System.Drawing.Size(296, 210);
            this.picPC_Cards.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPC_Cards.TabIndex = 106;
            this.picPC_Cards.TabStop = false;
            // 
            // label61
            // 
            this.label61.BackColor = System.Drawing.Color.Transparent;
            this.label61.Dock = System.Windows.Forms.DockStyle.Right;
            this.label61.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.ForeColor = System.Drawing.Color.Black;
            this.label61.Location = new System.Drawing.Point(313, 3);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(9, 229);
            this.label61.TabIndex = 109;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnPC_MoveFirst);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.btnPC_MovePrevious);
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.btnPC_MoveNext);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Controls.Add(this.btnPC_MoveLast);
            this.panel2.Controls.Add(this.label24);
            this.panel2.Controls.Add(this.btnPC_PrintCards);
            this.panel2.Controls.Add(this.label60);
            this.panel2.Controls.Add(this.btnPC_DeleteCard);
            this.panel2.Controls.Add(this.label22);
            this.panel2.Controls.Add(this.btnPC_ScanCard);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(322, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(38, 229);
            this.panel2.TabIndex = 108;
            // 
            // btnPC_MoveFirst
            // 
            this.btnPC_MoveFirst.BackColor = System.Drawing.Color.Transparent;
            this.btnPC_MoveFirst.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPC_MoveFirst.BackgroundImage")));
            this.btnPC_MoveFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPC_MoveFirst.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPC_MoveFirst.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPC_MoveFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPC_MoveFirst.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPC_MoveFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnPC_MoveFirst.Image")));
            this.btnPC_MoveFirst.Location = new System.Drawing.Point(0, 183);
            this.btnPC_MoveFirst.Name = "btnPC_MoveFirst";
            this.btnPC_MoveFirst.Size = new System.Drawing.Size(38, 25);
            this.btnPC_MoveFirst.TabIndex = 5;
            this.btnPC_MoveFirst.UseVisualStyleBackColor = false;
            this.btnPC_MoveFirst.Click += new System.EventHandler(this.btnPC_MoveFirst_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(0, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 5);
            this.label7.TabIndex = 108;
            // 
            // btnPC_MovePrevious
            // 
            this.btnPC_MovePrevious.BackColor = System.Drawing.Color.Transparent;
            this.btnPC_MovePrevious.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPC_MovePrevious.BackgroundImage")));
            this.btnPC_MovePrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPC_MovePrevious.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPC_MovePrevious.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPC_MovePrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPC_MovePrevious.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPC_MovePrevious.Image = ((System.Drawing.Image)(resources.GetObject("btnPC_MovePrevious.Image")));
            this.btnPC_MovePrevious.Location = new System.Drawing.Point(0, 153);
            this.btnPC_MovePrevious.Name = "btnPC_MovePrevious";
            this.btnPC_MovePrevious.Size = new System.Drawing.Size(38, 25);
            this.btnPC_MovePrevious.TabIndex = 4;
            this.btnPC_MovePrevious.UseVisualStyleBackColor = false;
            this.btnPC_MovePrevious.Click += new System.EventHandler(this.btnPC_MovePrevious_Click);
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(0, 148);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(38, 5);
            this.label20.TabIndex = 110;
            // 
            // btnPC_MoveNext
            // 
            this.btnPC_MoveNext.BackColor = System.Drawing.Color.Transparent;
            this.btnPC_MoveNext.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPC_MoveNext.BackgroundImage")));
            this.btnPC_MoveNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPC_MoveNext.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPC_MoveNext.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPC_MoveNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPC_MoveNext.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPC_MoveNext.Image = ((System.Drawing.Image)(resources.GetObject("btnPC_MoveNext.Image")));
            this.btnPC_MoveNext.Location = new System.Drawing.Point(0, 123);
            this.btnPC_MoveNext.Name = "btnPC_MoveNext";
            this.btnPC_MoveNext.Size = new System.Drawing.Size(38, 25);
            this.btnPC_MoveNext.TabIndex = 3;
            this.btnPC_MoveNext.UseVisualStyleBackColor = false;
            this.btnPC_MoveNext.Click += new System.EventHandler(this.btnPC_MoveNext_Click);
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Dock = System.Windows.Forms.DockStyle.Top;
            this.label21.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(0, 118);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(38, 5);
            this.label21.TabIndex = 112;
            // 
            // btnPC_MoveLast
            // 
            this.btnPC_MoveLast.BackColor = System.Drawing.Color.Transparent;
            this.btnPC_MoveLast.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPC_MoveLast.BackgroundImage")));
            this.btnPC_MoveLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPC_MoveLast.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPC_MoveLast.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPC_MoveLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPC_MoveLast.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPC_MoveLast.Image = ((System.Drawing.Image)(resources.GetObject("btnPC_MoveLast.Image")));
            this.btnPC_MoveLast.Location = new System.Drawing.Point(0, 93);
            this.btnPC_MoveLast.Name = "btnPC_MoveLast";
            this.btnPC_MoveLast.Size = new System.Drawing.Size(38, 25);
            this.btnPC_MoveLast.TabIndex = 2;
            this.btnPC_MoveLast.UseVisualStyleBackColor = false;
            this.btnPC_MoveLast.Click += new System.EventHandler(this.btnPC_MoveLast_Click);
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Dock = System.Windows.Forms.DockStyle.Top;
            this.label24.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(0, 88);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(38, 5);
            this.label24.TabIndex = 118;
            // 
            // btnPC_PrintCards
            // 
            this.btnPC_PrintCards.BackColor = System.Drawing.Color.Transparent;
            this.btnPC_PrintCards.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPC_PrintCards.BackgroundImage")));
            this.btnPC_PrintCards.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPC_PrintCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPC_PrintCards.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPC_PrintCards.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPC_PrintCards.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPC_PrintCards.Image = ((System.Drawing.Image)(resources.GetObject("btnPC_PrintCards.Image")));
            this.btnPC_PrintCards.Location = new System.Drawing.Point(0, 63);
            this.btnPC_PrintCards.Name = "btnPC_PrintCards";
            this.btnPC_PrintCards.Size = new System.Drawing.Size(38, 25);
            this.btnPC_PrintCards.TabIndex = 0;
            this.btnPC_PrintCards.UseVisualStyleBackColor = false;
            this.btnPC_PrintCards.Click += new System.EventHandler(this.btnPC_PrintCards_Click);
            // 
            // label60
            // 
            this.label60.BackColor = System.Drawing.Color.Transparent;
            this.label60.Dock = System.Windows.Forms.DockStyle.Top;
            this.label60.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.ForeColor = System.Drawing.Color.Black;
            this.label60.Location = new System.Drawing.Point(0, 57);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(38, 6);
            this.label60.TabIndex = 120;
            // 
            // btnPC_DeleteCard
            // 
            this.btnPC_DeleteCard.BackColor = System.Drawing.Color.Transparent;
            this.btnPC_DeleteCard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPC_DeleteCard.BackgroundImage")));
            this.btnPC_DeleteCard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPC_DeleteCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPC_DeleteCard.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPC_DeleteCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPC_DeleteCard.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPC_DeleteCard.Image = ((System.Drawing.Image)(resources.GetObject("btnPC_DeleteCard.Image")));
            this.btnPC_DeleteCard.Location = new System.Drawing.Point(0, 32);
            this.btnPC_DeleteCard.Name = "btnPC_DeleteCard";
            this.btnPC_DeleteCard.Size = new System.Drawing.Size(38, 25);
            this.btnPC_DeleteCard.TabIndex = 1;
            this.btnPC_DeleteCard.UseVisualStyleBackColor = false;
            this.btnPC_DeleteCard.Click += new System.EventHandler(this.btnPC_DeleteCard_Click);
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Dock = System.Windows.Forms.DockStyle.Top;
            this.label22.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(0, 27);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(38, 5);
            this.label22.TabIndex = 115;
            // 
            // btnPC_ScanCard
            // 
            this.btnPC_ScanCard.BackColor = System.Drawing.Color.Transparent;
            this.btnPC_ScanCard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnPC_ScanCard.BackgroundImage")));
            this.btnPC_ScanCard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPC_ScanCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPC_ScanCard.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.btnPC_ScanCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPC_ScanCard.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPC_ScanCard.Image = ((System.Drawing.Image)(resources.GetObject("btnPC_ScanCard.Image")));
            this.btnPC_ScanCard.Location = new System.Drawing.Point(0, 2);
            this.btnPC_ScanCard.Name = "btnPC_ScanCard";
            this.btnPC_ScanCard.Size = new System.Drawing.Size(38, 25);
            this.btnPC_ScanCard.TabIndex = 0;
            this.btnPC_ScanCard.UseVisualStyleBackColor = false;
            this.btnPC_ScanCard.Click += new System.EventHandler(this.btnPC_ScanCard_Click);
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label19.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(0, 227);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(38, 2);
            this.label19.TabIndex = 100;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Black;
            this.label18.Location = new System.Drawing.Point(0, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(38, 2);
            this.label18.TabIndex = 99;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Dock = System.Windows.Forms.DockStyle.Right;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(360, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(9, 229);
            this.label15.TabIndex = 99;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(8, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(9, 229);
            this.label12.TabIndex = 107;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Dock = System.Windows.Forms.DockStyle.Top;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(8, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(361, 3);
            this.label17.TabIndex = 101;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(8, 232);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(361, 4);
            this.label16.TabIndex = 100;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(8, 236);
            this.label14.TabIndex = 98;
            // 
            // panel22
            // 
            this.panel22.Controls.Add(this.lblScannedDate);
            this.panel22.Controls.Add(this.label67);
            this.panel22.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel22.Location = new System.Drawing.Point(3, 254);
            this.panel22.Name = "panel22";
            this.panel22.Size = new System.Drawing.Size(369, 22);
            this.panel22.TabIndex = 111;
            // 
            // lblScannedDate
            // 
            this.lblScannedDate.BackColor = System.Drawing.Color.Transparent;
            this.lblScannedDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblScannedDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScannedDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.lblScannedDate.Location = new System.Drawing.Point(0, 0);
            this.lblScannedDate.Name = "lblScannedDate";
            this.lblScannedDate.Size = new System.Drawing.Size(316, 22);
            this.lblScannedDate.TabIndex = 110;
            this.lblScannedDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label67
            // 
            this.label67.BackColor = System.Drawing.Color.Transparent;
            this.label67.Dock = System.Windows.Forms.DockStyle.Right;
            this.label67.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label67.Location = new System.Drawing.Point(316, 0);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(53, 22);
            this.label67.TabIndex = 111;
            this.label67.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.DarkBlue;
            this.label13.Location = new System.Drawing.Point(0, 11);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(11, 279);
            this.label13.TabIndex = 104;
            // 
            // label64
            // 
            this.label64.BackColor = System.Drawing.Color.Transparent;
            this.label64.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label64.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.ForeColor = System.Drawing.Color.DarkBlue;
            this.label64.Location = new System.Drawing.Point(0, 290);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(386, 11);
            this.label64.TabIndex = 103;
            // 
            // label65
            // 
            this.label65.BackColor = System.Drawing.Color.Transparent;
            this.label65.Dock = System.Windows.Forms.DockStyle.Right;
            this.label65.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.ForeColor = System.Drawing.Color.DarkBlue;
            this.label65.Location = new System.Drawing.Point(386, 11);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(10, 290);
            this.label65.TabIndex = 102;
            // 
            // label66
            // 
            this.label66.BackColor = System.Drawing.Color.Transparent;
            this.label66.Dock = System.Windows.Forms.DockStyle.Top;
            this.label66.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label66.ForeColor = System.Drawing.Color.DarkBlue;
            this.label66.Location = new System.Drawing.Point(0, 0);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(396, 11);
            this.label66.TabIndex = 101;
            // 
            // HelpComponent1
            // 
            this.HelpComponent1.Mode = gloEMR.Help.HelpComponent.ProviderMode.Client;
            // 
            // uiPanel0Container
            // 
            this.uiPanel0Container.Location = new System.Drawing.Point(0, 0);
            this.uiPanel0Container.Name = "uiPanel0Container";
            this.uiPanel0Container.Size = new System.Drawing.Size(210, 262);
            this.uiPanel0Container.TabIndex = 0;
            // 
            // pnlPatient_List
            // 
            this.pnlPatient_List.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.pnlPatient_List.Controls.Add(this.label38);
            this.pnlPatient_List.Controls.Add(this.gloCntrlPatient);
            this.pnlPatient_List.Controls.Add(this.pnlPatient);
            this.pnlPatient_List.Controls.Add(this.label35);
            this.pnlPatient_List.Controls.Add(this.label36);
            this.pnlPatient_List.Controls.Add(this.label37);
            this.pnlPatient_List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPatient_List.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.pnlPatient_List.Location = new System.Drawing.Point(199, 93);
            this.pnlPatient_List.Name = "pnlPatient_List";
            this.pnlPatient_List.Size = new System.Drawing.Size(914, 143);
            this.pnlPatient_List.TabIndex = 18;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.label38.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label38.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.label38.Location = new System.Drawing.Point(1, 142);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(912, 1);
            this.label38.TabIndex = 26;
            // 
            // gloCntrlPatient
            // 
            this.gloCntrlPatient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.gloCntrlPatient.ChkIsAccess = false;
            this.gloCntrlPatient.ClinicID = ((long)(1));
            this.gloCntrlPatient.ControlHeader = "";
            this.gloCntrlPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gloCntrlPatient.FirstName = "";
            this.gloCntrlPatient.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.gloCntrlPatient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.gloCntrlPatient.IsSecurityUser = false;
            this.gloCntrlPatient.LastName = "";
            this.gloCntrlPatient.Location = new System.Drawing.Point(1, 26);
            this.gloCntrlPatient.Margin = new System.Windows.Forms.Padding(4);
            this.gloCntrlPatient.MiddleName = "";
            this.gloCntrlPatient.Name = "gloCntrlPatient";
            this.gloCntrlPatient.PatientCode = "";
            this.gloCntrlPatient.PatientID = ((long)(0));
            this.gloCntrlPatient.ProviderID = ((long)(0));
            this.gloCntrlPatient.ProviderName = "";
            this.gloCntrlPatient.SelectedPatientID = ((long)(0));
            this.gloCntrlPatient.Size = new System.Drawing.Size(912, 117);
            this.gloCntrlPatient.TabIndex = 1;
            // 
            // pnlPatient
            // 
            this.pnlPatient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.pnlPatient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlPatient.BackgroundImage")));
            this.pnlPatient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPatient.Controls.Add(this.label1);
            this.pnlPatient.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPatient.Location = new System.Drawing.Point(1, 1);
            this.pnlPatient.Name = "pnlPatient";
            this.pnlPatient.Size = new System.Drawing.Size(912, 25);
            this.pnlPatient.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(912, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "      Patients";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.label35.Dock = System.Windows.Forms.DockStyle.Left;
            this.label35.ForeColor = System.Drawing.Color.DarkBlue;
            this.label35.Location = new System.Drawing.Point(0, 1);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(1, 142);
            this.label35.TabIndex = 23;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.label36.Dock = System.Windows.Forms.DockStyle.Right;
            this.label36.ForeColor = System.Drawing.Color.DarkBlue;
            this.label36.Location = new System.Drawing.Point(913, 1);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(1, 142);
            this.label36.TabIndex = 24;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(145)))), ((int)(((byte)(205)))));
            this.label37.Dock = System.Windows.Forms.DockStyle.Top;
            this.label37.ForeColor = System.Drawing.Color.DarkBlue;
            this.label37.Location = new System.Drawing.Point(0, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(914, 1);
            this.label37.TabIndex = 25;
            // 
            // tmr_Dashboard
            // 
            this.tmr_Dashboard.Enabled = true;
            this.tmr_Dashboard.Tick += new System.EventHandler(this.tmr_Dashboard_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.panel1.Controls.Add(this.mnuMainMenu);
            this.panel1.Controls.Add(this.label28);
            this.panel1.Controls.Add(this.label27);
            this.panel1.Controls.Add(this.label26);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1243, 26);
            this.panel1.TabIndex = 20;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label28.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label28.Location = new System.Drawing.Point(1, 25);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(1241, 1);
            this.label28.TabIndex = 13;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label27.Dock = System.Windows.Forms.DockStyle.Top;
            this.label27.Location = new System.Drawing.Point(1, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(1241, 1);
            this.label27.TabIndex = 12;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label26.Dock = System.Windows.Forms.DockStyle.Right;
            this.label26.Location = new System.Drawing.Point(1242, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(1, 26);
            this.label26.TabIndex = 11;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label25.Dock = System.Windows.Forms.DockStyle.Left;
            this.label25.Location = new System.Drawing.Point(0, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(1, 26);
            this.label25.TabIndex = 10;
            // 
            // pnl_ts_Main
            // 
            this.pnl_ts_Main.Controls.Add(this.ts_Main);
            this.pnl_ts_Main.Controls.Add(this.label31);
            this.pnl_ts_Main.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_ts_Main.Location = new System.Drawing.Point(0, 26);
            this.pnl_ts_Main.Name = "pnl_ts_Main";
            this.pnl_ts_Main.Size = new System.Drawing.Size(1243, 65);
            this.pnl_ts_Main.TabIndex = 22;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label31.Dock = System.Windows.Forms.DockStyle.Left;
            this.label31.Location = new System.Drawing.Point(0, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(1, 65);
            this.label31.TabIndex = 15;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label30.Dock = System.Windows.Forms.DockStyle.Left;
            this.label30.Location = new System.Drawing.Point(0, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(1, 26);
            this.label30.TabIndex = 16;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label32.Dock = System.Windows.Forms.DockStyle.Right;
            this.label32.Location = new System.Drawing.Point(1242, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(1, 26);
            this.label32.TabIndex = 17;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label33.Dock = System.Windows.Forms.DockStyle.Top;
            this.label33.Location = new System.Drawing.Point(1, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(1241, 1);
            this.label33.TabIndex = 18;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(150)))));
            this.label34.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label34.Location = new System.Drawing.Point(1, 25);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(1241, 1);
            this.label34.TabIndex = 19;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.statusStrip1);
            this.panel4.Controls.Add(this.label34);
            this.panel4.Controls.Add(this.label33);
            this.panel4.Controls.Add(this.label32);
            this.panel4.Controls.Add(this.label30);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 704);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1243, 26);
            this.panel4.TabIndex = 23;
            // 
            // timerLockScreen
            // 
            this.timerLockScreen.Tick += new System.EventHandler(this.timerLockScreen_Tick);
            // 
            // cmnu_Tasks
            // 
            this.cmnu_Tasks.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmnu_Tasks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuItem_Priority,
            this.cmunItem_Completed});
            this.cmnu_Tasks.Name = "cmnu_Appointment";
            this.cmnu_Tasks.Size = new System.Drawing.Size(140, 48);
            // 
            // cmnuItem_Priority
            // 
            this.cmnuItem_Priority.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmnuItem_Priority.Image = ((System.Drawing.Image)(resources.GetObject("cmnuItem_Priority.Image")));
            this.cmnuItem_Priority.Name = "cmnuItem_Priority";
            this.cmnuItem_Priority.Size = new System.Drawing.Size(139, 22);
            this.cmnuItem_Priority.Text = "Priority";
            // 
            // cmunItem_Completed
            // 
            this.cmunItem_Completed.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuItem_0Percent,
            this.cmnuItem_25Percent,
            this.cmnuItem_50Percent,
            this.cmnuItem_75Percent,
            this.cmnuItem_100Percent});
            this.cmunItem_Completed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmunItem_Completed.Image = ((System.Drawing.Image)(resources.GetObject("cmunItem_Completed.Image")));
            this.cmunItem_Completed.Name = "cmunItem_Completed";
            this.cmunItem_Completed.Size = new System.Drawing.Size(139, 22);
            this.cmunItem_Completed.Text = "% Completed";
            // 
            // cmnuItem_0Percent
            // 
            this.cmnuItem_0Percent.ForeColor = System.Drawing.Color.DarkBlue;
            this.cmnuItem_0Percent.Image = ((System.Drawing.Image)(resources.GetObject("cmnuItem_0Percent.Image")));
            this.cmnuItem_0Percent.Name = "cmnuItem_0Percent";
            this.cmnuItem_0Percent.Size = new System.Drawing.Size(106, 22);
            this.cmnuItem_0Percent.Tag = "0";
            this.cmnuItem_0Percent.Text = "0 %";
            // 
            // cmnuItem_25Percent
            // 
            this.cmnuItem_25Percent.ForeColor = System.Drawing.Color.DarkBlue;
            this.cmnuItem_25Percent.Image = ((System.Drawing.Image)(resources.GetObject("cmnuItem_25Percent.Image")));
            this.cmnuItem_25Percent.Name = "cmnuItem_25Percent";
            this.cmnuItem_25Percent.Size = new System.Drawing.Size(106, 22);
            this.cmnuItem_25Percent.Tag = "25";
            this.cmnuItem_25Percent.Text = "25 %";
            // 
            // cmnuItem_50Percent
            // 
            this.cmnuItem_50Percent.ForeColor = System.Drawing.Color.DarkBlue;
            this.cmnuItem_50Percent.Image = ((System.Drawing.Image)(resources.GetObject("cmnuItem_50Percent.Image")));
            this.cmnuItem_50Percent.Name = "cmnuItem_50Percent";
            this.cmnuItem_50Percent.Size = new System.Drawing.Size(106, 22);
            this.cmnuItem_50Percent.Tag = "50";
            this.cmnuItem_50Percent.Text = "50";
            // 
            // cmnuItem_75Percent
            // 
            this.cmnuItem_75Percent.ForeColor = System.Drawing.Color.DarkBlue;
            this.cmnuItem_75Percent.Image = ((System.Drawing.Image)(resources.GetObject("cmnuItem_75Percent.Image")));
            this.cmnuItem_75Percent.Name = "cmnuItem_75Percent";
            this.cmnuItem_75Percent.Size = new System.Drawing.Size(106, 22);
            this.cmnuItem_75Percent.Tag = "75";
            this.cmnuItem_75Percent.Text = "75 %";
            // 
            // cmnuItem_100Percent
            // 
            this.cmnuItem_100Percent.ForeColor = System.Drawing.Color.DarkBlue;
            this.cmnuItem_100Percent.Image = ((System.Drawing.Image)(resources.GetObject("cmnuItem_100Percent.Image")));
            this.cmnuItem_100Percent.Name = "cmnuItem_100Percent";
            this.cmnuItem_100Percent.Size = new System.Drawing.Size(106, 22);
            this.cmnuItem_100Percent.Tag = "100";
            this.cmnuItem_100Percent.Text = "100 %";
            // 
            // imgList_Priority
            // 
            this.imgList_Priority.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList_Priority.ImageStream")));
            this.imgList_Priority.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList_Priority.Images.SetKeyName(0, "High Priority01.ico");
            this.imgList_Priority.Images.SetKeyName(1, "High Priority02.ico");
            this.imgList_Priority.Images.SetKeyName(2, "High Priority03.ico");
            this.imgList_Priority.Images.SetKeyName(3, "High Priority0004.ico");
            this.imgList_Priority.Images.SetKeyName(4, "High Priority005.ico");
            this.imgList_Priority.Images.SetKeyName(5, "");
            this.imgList_Priority.Images.SetKeyName(6, "");
            this.imgList_Priority.Images.SetKeyName(7, "");
            this.imgList_Priority.Images.SetKeyName(8, "");
            this.imgList_Priority.Images.SetKeyName(9, "");
            this.imgList_Priority.Images.SetKeyName(10, "");
            this.imgList_Priority.Images.SetKeyName(11, "");
            this.imgList_Priority.Images.SetKeyName(12, "");
            this.imgList_Priority.Images.SetKeyName(13, "");
            this.imgList_Priority.Images.SetKeyName(14, "");
            this.imgList_Priority.Images.SetKeyName(15, "");
            this.imgList_Priority.Images.SetKeyName(16, "");
            this.imgList_Priority.Images.SetKeyName(17, "");
            this.imgList_Priority.Images.SetKeyName(18, "");
            this.imgList_Priority.Images.SetKeyName(19, "");
            this.imgList_Priority.Images.SetKeyName(20, "");
            this.imgList_Priority.Images.SetKeyName(21, "");
            this.imgList_Priority.Images.SetKeyName(22, "");
            this.imgList_Priority.Images.SetKeyName(23, "");
            this.imgList_Priority.Images.SetKeyName(24, "");
            this.imgList_Priority.Images.SetKeyName(25, "");
            this.imgList_Priority.Images.SetKeyName(26, "");
            this.imgList_Priority.Images.SetKeyName(27, "");
            this.imgList_Priority.Images.SetKeyName(28, "");
            this.imgList_Priority.Images.SetKeyName(29, "");
            this.imgList_Priority.Images.SetKeyName(30, "");
            this.imgList_Priority.Images.SetKeyName(31, "");
            this.imgList_Priority.Images.SetKeyName(32, "");
            this.imgList_Priority.Images.SetKeyName(33, "");
            this.imgList_Priority.Images.SetKeyName(34, "");
            this.imgList_Priority.Images.SetKeyName(35, "");
            this.imgList_Priority.Images.SetKeyName(36, "");
            this.imgList_Priority.Images.SetKeyName(37, "");
            this.imgList_Priority.Images.SetKeyName(38, "");
            this.imgList_Priority.Images.SetKeyName(39, "");
            this.imgList_Priority.Images.SetKeyName(40, "");
            this.imgList_Priority.Images.SetKeyName(41, "");
            this.imgList_Priority.Images.SetKeyName(42, "");
            this.imgList_Priority.Images.SetKeyName(43, "");
            this.imgList_Priority.Images.SetKeyName(44, "");
            this.imgList_Priority.Images.SetKeyName(45, "");
            this.imgList_Priority.Images.SetKeyName(46, "");
            this.imgList_Priority.Images.SetKeyName(47, "");
            this.imgList_Priority.Images.SetKeyName(48, "");
            this.imgList_Priority.Images.SetKeyName(49, "");
            this.imgList_Priority.Images.SetKeyName(50, "");
            this.imgList_Priority.Images.SetKeyName(51, "");
            this.imgList_Priority.Images.SetKeyName(52, "");
            this.imgList_Priority.Images.SetKeyName(53, "");
            this.imgList_Priority.Images.SetKeyName(54, "");
            this.imgList_Priority.Images.SetKeyName(55, "");
            this.imgList_Priority.Images.SetKeyName(56, "");
            this.imgList_Priority.Images.SetKeyName(57, "");
            this.imgList_Priority.Images.SetKeyName(58, "");
            this.imgList_Priority.Images.SetKeyName(59, "");
            this.imgList_Priority.Images.SetKeyName(60, "");
            this.imgList_Priority.Images.SetKeyName(61, "");
            this.imgList_Priority.Images.SetKeyName(62, "");
            this.imgList_Priority.Images.SetKeyName(63, "");
            this.imgList_Priority.Images.SetKeyName(64, "");
            this.imgList_Priority.Images.SetKeyName(65, "");
            this.imgList_Priority.Images.SetKeyName(66, "");
            this.imgList_Priority.Images.SetKeyName(67, "");
            this.imgList_Priority.Images.SetKeyName(68, "");
            this.imgList_Priority.Images.SetKeyName(69, "");
            this.imgList_Priority.Images.SetKeyName(70, "");
            this.imgList_Priority.Images.SetKeyName(71, "");
            this.imgList_Priority.Images.SetKeyName(72, "");
            this.imgList_Priority.Images.SetKeyName(73, "");
            this.imgList_Priority.Images.SetKeyName(74, "");
            this.imgList_Priority.Images.SetKeyName(75, "");
            this.imgList_Priority.Images.SetKeyName(76, "");
            this.imgList_Priority.Images.SetKeyName(77, "");
            this.imgList_Priority.Images.SetKeyName(78, "gloExchangeServer Disable.ico");
            this.imgList_Priority.Images.SetKeyName(79, "Low Priority.ico");
            this.imgList_Priority.Images.SetKeyName(80, "Low Priority.png");
            // 
            // cmnu_PatientDetails
            // 
            this.cmnu_PatientDetails.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmnu_PatientDetails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuItem_Authorization,
            this.mnuItem_Add,
            this.mnuItem_RemoveRef,
            this.mnuItem_Modify,
            this.mnuItem_eligibilityCheck,
            this.mnuItem_eligibilityCheckTest,
            this.mnuItem_copay,
            this.mnuItem_coverage,
            this.mnuItem_Ledger,
            this.mnuItem_Payment,
            this.mnuItem_ViewBenefits,
            this.mnuItem_ModifyCharges,
            this.mnuItem_NewWCForm,
            this.mnuItem_ModifyWCForm});
            this.cmnu_PatientDetails.Name = "cmnu_Appointment";
            this.cmnu_PatientDetails.Size = new System.Drawing.Size(179, 312);
            this.cmnu_PatientDetails.Opening += new System.ComponentModel.CancelEventHandler(this.cmnu_PatientDetails_Opening);
            // 
            // mnuItem_Authorization
            // 
            this.mnuItem_Authorization.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuItem_Authorization.Image = ((System.Drawing.Image)(resources.GetObject("mnuItem_Authorization.Image")));
            this.mnuItem_Authorization.Name = "mnuItem_Authorization";
            this.mnuItem_Authorization.Size = new System.Drawing.Size(178, 22);
            this.mnuItem_Authorization.Text = "Prior Authorization";
            this.mnuItem_Authorization.Click += new System.EventHandler(this.mnuItem_Authorization_Click);
            // 
            // mnuItem_Add
            // 
            this.mnuItem_Add.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuItem_Add.Image = ((System.Drawing.Image)(resources.GetObject("mnuItem_Add.Image")));
            this.mnuItem_Add.Name = "mnuItem_Add";
            this.mnuItem_Add.Size = new System.Drawing.Size(178, 22);
            this.mnuItem_Add.Text = "Add";
            this.mnuItem_Add.Click += new System.EventHandler(this.mnuItem_AddRef_Click);
            // 
            // mnuItem_RemoveRef
            // 
            this.mnuItem_RemoveRef.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuItem_RemoveRef.Image = ((System.Drawing.Image)(resources.GetObject("mnuItem_RemoveRef.Image")));
            this.mnuItem_RemoveRef.Name = "mnuItem_RemoveRef";
            this.mnuItem_RemoveRef.Size = new System.Drawing.Size(178, 22);
            this.mnuItem_RemoveRef.Text = "Remove";
            this.mnuItem_RemoveRef.Click += new System.EventHandler(this.mnuItem_RemoveRef_Click);
            // 
            // mnuItem_Modify
            // 
            this.mnuItem_Modify.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuItem_Modify.Image = ((System.Drawing.Image)(resources.GetObject("mnuItem_Modify.Image")));
            this.mnuItem_Modify.Name = "mnuItem_Modify";
            this.mnuItem_Modify.Size = new System.Drawing.Size(178, 22);
            this.mnuItem_Modify.Text = "Modify";
            this.mnuItem_Modify.Click += new System.EventHandler(this.mnuItem_Modify_Click);
            // 
            // mnuItem_eligibilityCheck
            // 
            this.mnuItem_eligibilityCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuItem_eligibilityCheck.Image = ((System.Drawing.Image)(resources.GetObject("mnuItem_eligibilityCheck.Image")));
            this.mnuItem_eligibilityCheck.Name = "mnuItem_eligibilityCheck";
            this.mnuItem_eligibilityCheck.Size = new System.Drawing.Size(178, 22);
            this.mnuItem_eligibilityCheck.Text = "Eligibility Check";
            this.mnuItem_eligibilityCheck.Click += new System.EventHandler(this.eligibilityCheckToolStripMenuItem_Click);
            // 
            // mnuItem_eligibilityCheckTest
            // 
            this.mnuItem_eligibilityCheckTest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuItem_eligibilityCheckTest.Image = ((System.Drawing.Image)(resources.GetObject("mnuItem_eligibilityCheckTest.Image")));
            this.mnuItem_eligibilityCheckTest.Name = "mnuItem_eligibilityCheckTest";
            this.mnuItem_eligibilityCheckTest.Size = new System.Drawing.Size(178, 22);
            this.mnuItem_eligibilityCheckTest.Text = "Eligibility Check (Test)";
            this.mnuItem_eligibilityCheckTest.Click += new System.EventHandler(this.mnuItem_eligibilityCheckTest_Click);
            // 
            // mnuItem_copay
            // 
            this.mnuItem_copay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuItem_copay.Image = ((System.Drawing.Image)(resources.GetObject("mnuItem_copay.Image")));
            this.mnuItem_copay.Name = "mnuItem_copay";
            this.mnuItem_copay.Size = new System.Drawing.Size(178, 22);
            this.mnuItem_copay.Text = "Copay";
            this.mnuItem_copay.Click += new System.EventHandler(this.mnuItem_copay_Click);
            // 
            // mnuItem_coverage
            // 
            this.mnuItem_coverage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuItem_coverage.Image = ((System.Drawing.Image)(resources.GetObject("mnuItem_coverage.Image")));
            this.mnuItem_coverage.Name = "mnuItem_coverage";
            this.mnuItem_coverage.Size = new System.Drawing.Size(178, 22);
            this.mnuItem_coverage.Text = "Coverage";
            this.mnuItem_coverage.Visible = false;
            this.mnuItem_coverage.Click += new System.EventHandler(this.mnuItem_coverage_Click);
            // 
            // mnuItem_Ledger
            // 
            this.mnuItem_Ledger.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuItem_Ledger.Image = ((System.Drawing.Image)(resources.GetObject("mnuItem_Ledger.Image")));
            this.mnuItem_Ledger.Name = "mnuItem_Ledger";
            this.mnuItem_Ledger.Size = new System.Drawing.Size(178, 22);
            this.mnuItem_Ledger.Text = "Patient Account";
            this.mnuItem_Ledger.Click += new System.EventHandler(this.mnuItem_Ledger_Click);
            // 
            // mnuItem_Payment
            // 
            this.mnuItem_Payment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuItem_Payment.Image = ((System.Drawing.Image)(resources.GetObject("mnuItem_Payment.Image")));
            this.mnuItem_Payment.Name = "mnuItem_Payment";
            this.mnuItem_Payment.Size = new System.Drawing.Size(178, 22);
            this.mnuItem_Payment.Text = "Payment";
            this.mnuItem_Payment.Visible = false;
            this.mnuItem_Payment.Click += new System.EventHandler(this.mnuItem_Payment_Click);
            // 
            // mnuItem_ViewBenefits
            // 
            this.mnuItem_ViewBenefits.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuItem_ViewBenefits.Image = ((System.Drawing.Image)(resources.GetObject("mnuItem_ViewBenefits.Image")));
            this.mnuItem_ViewBenefits.Name = "mnuItem_ViewBenefits";
            this.mnuItem_ViewBenefits.Size = new System.Drawing.Size(178, 22);
            this.mnuItem_ViewBenefits.Text = "Patient Benefits";
            this.mnuItem_ViewBenefits.Click += new System.EventHandler(this.mnuItem_ViewBenefits_Click);
            // 
            // mnuItem_ModifyCharges
            // 
            this.mnuItem_ModifyCharges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuItem_ModifyCharges.Image = ((System.Drawing.Image)(resources.GetObject("mnuItem_ModifyCharges.Image")));
            this.mnuItem_ModifyCharges.Name = "mnuItem_ModifyCharges";
            this.mnuItem_ModifyCharges.Size = new System.Drawing.Size(178, 22);
            this.mnuItem_ModifyCharges.Text = "Modify Charges";
            this.mnuItem_ModifyCharges.Click += new System.EventHandler(this.mnuItem_ModifyCharges_Click);
            // 
            // mnuItem_NewWCForm
            // 
            this.mnuItem_NewWCForm.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c4ToolStripMenuItem,
            this.c42ToolStripMenuItem,
            this.c4AUTHToolStripMenuItem,
            this.mG2ToolStripMenuItem,
            this.mG21ToolStripMenuItem});
            this.mnuItem_NewWCForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuItem_NewWCForm.Image = ((System.Drawing.Image)(resources.GetObject("mnuItem_NewWCForm.Image")));
            this.mnuItem_NewWCForm.Name = "mnuItem_NewWCForm";
            this.mnuItem_NewWCForm.Size = new System.Drawing.Size(178, 22);
            this.mnuItem_NewWCForm.Text = "New Form";
            this.mnuItem_NewWCForm.Click += new System.EventHandler(this.mnuItem_NewWCForm_Click);
            // 
            // c4ToolStripMenuItem
            // 
            this.c4ToolStripMenuItem.Name = "c4ToolStripMenuItem";
            this.c4ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.c4ToolStripMenuItem.Text = "C-4";
            this.c4ToolStripMenuItem.Click += new System.EventHandler(this.c4ToolStripMenuItem_Click);
            // 
            // c42ToolStripMenuItem
            // 
            this.c42ToolStripMenuItem.Name = "c42ToolStripMenuItem";
            this.c42ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.c42ToolStripMenuItem.Text = "C-4.2";
            this.c42ToolStripMenuItem.Click += new System.EventHandler(this.c42ToolStripMenuItem_Click);
            // 
            // c4AUTHToolStripMenuItem
            // 
            this.c4AUTHToolStripMenuItem.Name = "c4AUTHToolStripMenuItem";
            this.c4AUTHToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.c4AUTHToolStripMenuItem.Text = "C-4 AUTH";
            this.c4AUTHToolStripMenuItem.Click += new System.EventHandler(this.c4AUTHToolStripMenuItem_Click);
            // 
            // mG2ToolStripMenuItem
            // 
            this.mG2ToolStripMenuItem.Name = "mG2ToolStripMenuItem";
            this.mG2ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.mG2ToolStripMenuItem.Text = "MG-2";
            this.mG2ToolStripMenuItem.Click += new System.EventHandler(this.mG2ToolStripMenuItem_Click);
            // 
            // mG21ToolStripMenuItem
            // 
            this.mG21ToolStripMenuItem.Name = "mG21ToolStripMenuItem";
            this.mG21ToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.mG21ToolStripMenuItem.Text = "MG-2.1";
            this.mG21ToolStripMenuItem.Click += new System.EventHandler(this.mG21ToolStripMenuItem_Click);
            // 
            // mnuItem_ModifyWCForm
            // 
            this.mnuItem_ModifyWCForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.mnuItem_ModifyWCForm.Image = ((System.Drawing.Image)(resources.GetObject("mnuItem_ModifyWCForm.Image")));
            this.mnuItem_ModifyWCForm.Name = "mnuItem_ModifyWCForm";
            this.mnuItem_ModifyWCForm.Size = new System.Drawing.Size(178, 22);
            this.mnuItem_ModifyWCForm.Text = "Modify Form";
            this.mnuItem_ModifyWCForm.Click += new System.EventHandler(this.mnuItem_ModifyWCForm_Click);
            // 
            // cmnu_PatientList
            // 
            this.cmnu_PatientList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmnu_PatientList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuPatientItem_CheckIn,
            this.cmnuPatientItem_CheckOut,
            this.cmnuPatientItem_Template,
            this.cmnuPatientItem_PatientAlert,
            this.cmnuPatientItem_SaveAsCopy,
            this.cmnuPatientItem_Cases});
            this.cmnu_PatientList.Name = "cmnu_Appointment";
            this.cmnu_PatientList.Size = new System.Drawing.Size(140, 136);
            // 
            // cmnuPatientItem_CheckIn
            // 
            this.cmnuPatientItem_CheckIn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmnuPatientItem_CheckIn.Image = ((System.Drawing.Image)(resources.GetObject("cmnuPatientItem_CheckIn.Image")));
            this.cmnuPatientItem_CheckIn.Name = "cmnuPatientItem_CheckIn";
            this.cmnuPatientItem_CheckIn.Size = new System.Drawing.Size(139, 22);
            this.cmnuPatientItem_CheckIn.Text = "Checkin";
            this.cmnuPatientItem_CheckIn.Click += new System.EventHandler(this.cmnuPatientItem_CheckIn_Click);
            // 
            // cmnuPatientItem_CheckOut
            // 
            this.cmnuPatientItem_CheckOut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmnuPatientItem_CheckOut.Image = ((System.Drawing.Image)(resources.GetObject("cmnuPatientItem_CheckOut.Image")));
            this.cmnuPatientItem_CheckOut.Name = "cmnuPatientItem_CheckOut";
            this.cmnuPatientItem_CheckOut.Size = new System.Drawing.Size(139, 22);
            this.cmnuPatientItem_CheckOut.Text = "Checkout";
            this.cmnuPatientItem_CheckOut.Click += new System.EventHandler(this.cmnuPatientItem_CheckOut_Click);
            // 
            // cmnuPatientItem_Template
            // 
            this.cmnuPatientItem_Template.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmnuPatientItem_Template.Image = ((System.Drawing.Image)(resources.GetObject("cmnuPatientItem_Template.Image")));
            this.cmnuPatientItem_Template.Name = "cmnuPatientItem_Template";
            this.cmnuPatientItem_Template.Size = new System.Drawing.Size(139, 22);
            this.cmnuPatientItem_Template.Text = "Template";
            // 
            // cmnuPatientItem_PatientAlert
            // 
            this.cmnuPatientItem_PatientAlert.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmnuPatientItem_PatientAlert.Image = ((System.Drawing.Image)(resources.GetObject("cmnuPatientItem_PatientAlert.Image")));
            this.cmnuPatientItem_PatientAlert.Name = "cmnuPatientItem_PatientAlert";
            this.cmnuPatientItem_PatientAlert.Size = new System.Drawing.Size(139, 22);
            this.cmnuPatientItem_PatientAlert.Text = "Patient Alerts";
            this.cmnuPatientItem_PatientAlert.Click += new System.EventHandler(this.patientAlertToolStripMenuItem_Click);
            // 
            // cmnuPatientItem_SaveAsCopy
            // 
            this.cmnuPatientItem_SaveAsCopy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmnuPatientItem_SaveAsCopy.Image = ((System.Drawing.Image)(resources.GetObject("cmnuPatientItem_SaveAsCopy.Image")));
            this.cmnuPatientItem_SaveAsCopy.Name = "cmnuPatientItem_SaveAsCopy";
            this.cmnuPatientItem_SaveAsCopy.Size = new System.Drawing.Size(139, 22);
            this.cmnuPatientItem_SaveAsCopy.Text = " Copy Patient";
            this.cmnuPatientItem_SaveAsCopy.Visible = false;
            this.cmnuPatientItem_SaveAsCopy.Click += new System.EventHandler(this.cmnuPatientItem_SaveAsCopy_Click);
            // 
            // cmnuPatientItem_Cases
            // 
            this.cmnuPatientItem_Cases.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.cmnuPatientItem_Cases.Image = ((System.Drawing.Image)(resources.GetObject("cmnuPatientItem_Cases.Image")));
            this.cmnuPatientItem_Cases.Name = "cmnuPatientItem_Cases";
            this.cmnuPatientItem_Cases.Size = new System.Drawing.Size(139, 22);
            this.cmnuPatientItem_Cases.Text = " Cases";
            this.cmnuPatientItem_Cases.Visible = false;
            this.cmnuPatientItem_Cases.Click += new System.EventHandler(this.cmnuPatientItem_Cases_Click);
            // 
            // cmnuToolStripCustomize
            // 
            this.cmnuToolStripCustomize.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CustomizeToolStripMenuItem});
            this.cmnuToolStripCustomize.Name = "cmnuToolStripCustomize";
            this.cmnuToolStripCustomize.Size = new System.Drawing.Size(131, 26);
            this.cmnuToolStripCustomize.Click += new System.EventHandler(this.cmnuToolStripCustomize_Click);
            // 
            // CustomizeToolStripMenuItem
            // 
            this.CustomizeToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.CustomizeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("CustomizeToolStripMenuItem.Image")));
            this.CustomizeToolStripMenuItem.Name = "CustomizeToolStripMenuItem";
            this.CustomizeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.CustomizeToolStripMenuItem.Text = "Customize";
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printDoc
            // 
            this.printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDoc_PrintPage);
            // 
            // tmrCopayAlertBlink
            // 
            this.tmrCopayAlertBlink.Interval = 500;
            this.tmrCopayAlertBlink.Tick += new System.EventHandler(this.tmrCopayAlertBlink_Tick);
            // 
            // imgList_ApptPrint
            // 
            this.imgList_ApptPrint.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList_ApptPrint.ImageStream")));
            this.imgList_ApptPrint.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList_ApptPrint.Images.SetKeyName(0, "CheckTemplate.ico");
            this.imgList_ApptPrint.Images.SetKeyName(1, "Appointment Letter Templatenew.ico");
            this.imgList_ApptPrint.Images.SetKeyName(2, "Appointment  Schedule.ico");
            this.imgList_ApptPrint.Images.SetKeyName(3, "Appointment Letter Template.ico");
            this.imgList_ApptPrint.Images.SetKeyName(4, "Check in template.ico");
            this.imgList_ApptPrint.Images.SetKeyName(5, "View History.ico");
            // 
            // cm_Task
            // 
            this.cm_Task.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmu_NewTask,
            this.cmu_OpenTask,
            this.cmu_MarkCompleted,
            this.cmu_Delete,
            this.cmu_FollowUp,
            this.cmu_Priority,
            this.cmu_Complete,
            this.cmu_AcceptTask,
            this.cmu_DeclineTask});
            this.cm_Task.Name = "cm_MarkComplete";
            this.cm_Task.Size = new System.Drawing.Size(164, 202);
            this.cm_Task.Text = "Mark Complete";
            // 
            // cmu_NewTask
            // 
            this.cmu_NewTask.Image = ((System.Drawing.Image)(resources.GetObject("cmu_NewTask.Image")));
            this.cmu_NewTask.Name = "cmu_NewTask";
            this.cmu_NewTask.Size = new System.Drawing.Size(163, 22);
            this.cmu_NewTask.Tag = "New";
            this.cmu_NewTask.Text = "New";
            this.cmu_NewTask.ToolTipText = "New Task";
            this.cmu_NewTask.Visible = false;
            // 
            // cmu_OpenTask
            // 
            this.cmu_OpenTask.Image = ((System.Drawing.Image)(resources.GetObject("cmu_OpenTask.Image")));
            this.cmu_OpenTask.Name = "cmu_OpenTask";
            this.cmu_OpenTask.Size = new System.Drawing.Size(163, 22);
            this.cmu_OpenTask.Tag = "Open";
            this.cmu_OpenTask.Text = "Edit";
            this.cmu_OpenTask.ToolTipText = "Edit Task";
            this.cmu_OpenTask.Click += new System.EventHandler(this.cmu_OpenTask_Click);
            // 
            // cmu_MarkCompleted
            // 
            this.cmu_MarkCompleted.Image = ((System.Drawing.Image)(resources.GetObject("cmu_MarkCompleted.Image")));
            this.cmu_MarkCompleted.Name = "cmu_MarkCompleted";
            this.cmu_MarkCompleted.Size = new System.Drawing.Size(163, 22);
            this.cmu_MarkCompleted.Text = "Mark Completed";
            this.cmu_MarkCompleted.Click += new System.EventHandler(this.cmu_MarkCompleted_Click);
            // 
            // cmu_Delete
            // 
            this.cmu_Delete.Image = ((System.Drawing.Image)(resources.GetObject("cmu_Delete.Image")));
            this.cmu_Delete.Name = "cmu_Delete";
            this.cmu_Delete.Size = new System.Drawing.Size(163, 22);
            this.cmu_Delete.Tag = "Delete";
            this.cmu_Delete.Text = "Delete";
            this.cmu_Delete.ToolTipText = "Delete";
            this.cmu_Delete.Click += new System.EventHandler(this.cmu_Delete_Click);
            // 
            // cmu_FollowUp
            // 
            this.cmu_FollowUp.Image = ((System.Drawing.Image)(resources.GetObject("cmu_FollowUp.Image")));
            this.cmu_FollowUp.Name = "cmu_FollowUp";
            this.cmu_FollowUp.Size = new System.Drawing.Size(163, 22);
            this.cmu_FollowUp.Tag = "Followup";
            this.cmu_FollowUp.Text = "Follow Up";
            // 
            // cmu_Priority
            // 
            this.cmu_Priority.Image = ((System.Drawing.Image)(resources.GetObject("cmu_Priority.Image")));
            this.cmu_Priority.Name = "cmu_Priority";
            this.cmu_Priority.Size = new System.Drawing.Size(163, 22);
            this.cmu_Priority.Tag = "Priority";
            this.cmu_Priority.Text = "Priority";
            this.cmu_Priority.ToolTipText = "Set Priority";
            // 
            // cmu_Complete
            // 
            this.cmu_Complete.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smn_Zero,
            this.smn_Quater,
            this.smn_Half,
            this.smn_ThreeQuater,
            this.smn_Full});
            this.cmu_Complete.Image = ((System.Drawing.Image)(resources.GetObject("cmu_Complete.Image")));
            this.cmu_Complete.Name = "cmu_Complete";
            this.cmu_Complete.Size = new System.Drawing.Size(163, 22);
            this.cmu_Complete.Text = "Complete %";
            this.cmu_Complete.ToolTipText = "Task Progress";
            // 
            // smn_Zero
            // 
            this.smn_Zero.Image = ((System.Drawing.Image)(resources.GetObject("smn_Zero.Image")));
            this.smn_Zero.Name = "smn_Zero";
            this.smn_Zero.Size = new System.Drawing.Size(105, 22);
            this.smn_Zero.Tag = "0";
            this.smn_Zero.Text = "0 %";
            this.smn_Zero.Click += new System.EventHandler(this.smn_Zero_Click);
            // 
            // smn_Quater
            // 
            this.smn_Quater.Image = ((System.Drawing.Image)(resources.GetObject("smn_Quater.Image")));
            this.smn_Quater.Name = "smn_Quater";
            this.smn_Quater.Size = new System.Drawing.Size(105, 22);
            this.smn_Quater.Tag = "25";
            this.smn_Quater.Text = "25 %";
            this.smn_Quater.Click += new System.EventHandler(this.smn_Zero_Click);
            // 
            // smn_Half
            // 
            this.smn_Half.Image = ((System.Drawing.Image)(resources.GetObject("smn_Half.Image")));
            this.smn_Half.Name = "smn_Half";
            this.smn_Half.Size = new System.Drawing.Size(105, 22);
            this.smn_Half.Tag = "50";
            this.smn_Half.Text = "50 %";
            this.smn_Half.Click += new System.EventHandler(this.smn_Zero_Click);
            // 
            // smn_ThreeQuater
            // 
            this.smn_ThreeQuater.Image = ((System.Drawing.Image)(resources.GetObject("smn_ThreeQuater.Image")));
            this.smn_ThreeQuater.Name = "smn_ThreeQuater";
            this.smn_ThreeQuater.Size = new System.Drawing.Size(105, 22);
            this.smn_ThreeQuater.Tag = "75";
            this.smn_ThreeQuater.Text = "75 %";
            this.smn_ThreeQuater.Click += new System.EventHandler(this.smn_Zero_Click);
            // 
            // smn_Full
            // 
            this.smn_Full.Image = ((System.Drawing.Image)(resources.GetObject("smn_Full.Image")));
            this.smn_Full.Name = "smn_Full";
            this.smn_Full.Size = new System.Drawing.Size(105, 22);
            this.smn_Full.Tag = "100";
            this.smn_Full.Text = "100 %";
            this.smn_Full.Click += new System.EventHandler(this.smn_Zero_Click);
            // 
            // cmu_AcceptTask
            // 
            this.cmu_AcceptTask.Image = ((System.Drawing.Image)(resources.GetObject("cmu_AcceptTask.Image")));
            this.cmu_AcceptTask.Name = "cmu_AcceptTask";
            this.cmu_AcceptTask.Size = new System.Drawing.Size(163, 22);
            this.cmu_AcceptTask.Tag = "Accept";
            this.cmu_AcceptTask.Text = "Accept";
            this.cmu_AcceptTask.ToolTipText = "Accept";
            this.cmu_AcceptTask.Visible = false;
            this.cmu_AcceptTask.Click += new System.EventHandler(this.cmu_AcceptTask_Click);
            // 
            // cmu_DeclineTask
            // 
            this.cmu_DeclineTask.Image = ((System.Drawing.Image)(resources.GetObject("cmu_DeclineTask.Image")));
            this.cmu_DeclineTask.Name = "cmu_DeclineTask";
            this.cmu_DeclineTask.Size = new System.Drawing.Size(163, 22);
            this.cmu_DeclineTask.Tag = "Decline";
            this.cmu_DeclineTask.Text = "Decline";
            this.cmu_DeclineTask.ToolTipText = "Decline Task";
            this.cmu_DeclineTask.Visible = false;
            this.cmu_DeclineTask.Click += new System.EventHandler(this.cmu_DeclineTask_Click);
            // 
            // C1SuperTooltip1
            // 
            this.C1SuperTooltip1.BackgroundGradient = C1.Win.C1SuperTooltip.BackgroundGradient.None;
            this.C1SuperTooltip1.Font = new System.Drawing.Font("Tahoma", 8F);
            // 
            // tmrSingleSignOn
            // 
            this.tmrSingleSignOn.Interval = 60000;
            this.tmrSingleSignOn.Tick += new System.EventHandler(this.tmrSingleSignOn_Tick);
            // 
            // frmDashBoardMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(224)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1243, 730);
            this.Controls.Add(this.pnlPatient_List);
            this.Controls.Add(this.pnlPatient_Demographics);
            this.Controls.Add(this.pnlPatient_Details);
            this.Controls.Add(this.pnlPatient_UpComingAppointments);
            this.Controls.Add(this.uipnlPatient_Alert);
            this.Controls.Add(this.pnl_ts_Main);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(73)))), ((int)(((byte)(125)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mnuMainMenu;
            this.Name = "frmDashBoardMain";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QPM";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDashBoardMain_FormClosing);
            this.Load += new System.EventHandler(this.frmDashBoardMain_Load);
            this.Shown += new System.EventHandler(this.frmDashBoardMain_Shown);
            this.mnuMainMenu.ResumeLayout(false);
            this.mnuMainMenu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ts_Main.ResumeLayout(false);
            this.ts_Main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiPanelManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uipnlPatient_Alert)).EndInit();
            this.uipnlPatient_Alert.ResumeLayout(false);
            this.uipnlPatient_AlertContainer.ResumeLayout(false);
            this.pnlSideButton.ResumeLayout(false);
            this.pnl_Appointment.ResumeLayout(false);
            this.pnl_Calendar.ResumeLayout(false);
            this.pnl_Tasks.ResumeLayout(false);
            this.pnlPatientAlertMain.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.pnlc1PatientAlerts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientAlerts)).EndInit();
            this.pnl_LeftButtons.ResumeLayout(false);
            this.pnlLeftPatientAlert.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.pnlEligibilityCheck.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1EligibilityCheck)).EndInit();
            this.pnlCopayAlert.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1CopayAlert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPatient_UpComingAppointments)).EndInit();
            this.pnlPatient_UpComingAppointments.ResumeLayout(false);
            this.pnlPatient_UpComingAppointmentsContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._picBkg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPatient_Details)).EndInit();
            this.pnlPatient_Details.ResumeLayout(false);
            this.pnlPatient_DetailsContainer.ResumeLayout(false);
            this.pnlPatient_DetailsContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1PatientDetails)).EndInit();
            this.pnlSearchFilter.ResumeLayout(false);
            this.pnlSearchFilter.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel17.ResumeLayout(false);
            this.panel18.ResumeLayout(false);
            this.ts_PatientDetail.ResumeLayout(false);
            this.ts_PatientDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPatient_Demographics)).EndInit();
            this.pnlPatient_Demographics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlPatient_Demo)).EndInit();
            this.pnlPatient_Demo.ResumeLayout(false);
            this.pnlPatient_DemographicsContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gb_Demographics)).EndInit();
            this.gb_Demographics.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.pnl_PD_ContactInfo.ResumeLayout(false);
            this.pnlBusinessCenter.ResumeLayout(false);
            this.pnl_WorkPhone.ResumeLayout(false);
            this.pnl_Occupation.ResumeLayout(false);
            this.pnl_MedCat.ResumeLayout(false);
            this.pnl_PatStatus.ResumeLayout(false);
            this.pnl_Language.ResumeLayout(false);
            this.pnl_Ethinicity.ResumeLayout(false);
            this.pnl_Race.ResumeLayout(false);
            this.pnl_TertiaryInsurance.ResumeLayout(false);
            this.pnl_SecondaryInsurance.ResumeLayout(false);
            this.pnl_PrimaryInsurance.ResumeLayout(false);
            this.pnl_PD_PCPPhone.ResumeLayout(false);
            this.pnl_PD_PCPMobile.ResumeLayout(false);
            this.pnl_PD_Referral.ResumeLayout(false);
            this.pnl_PD_Physician.ResumeLayout(false);
            this.pnl_PD_Status.ResumeLayout(false);
            this.pnl_PD_Status.PerformLayout();
            this.pnl_PD_Pharmacy.ResumeLayout(false);
            this.pnl_PD_Provider.ResumeLayout(false);
            this.pnl_EMmobile.ResumeLayout(false);
            this.pnl_EmPhone.ResumeLayout(false);
            this.pnl_EmContacts.ResumeLayout(false);
            this.pnl_Email.ResumeLayout(false);
            this.pnl_Fax.ResumeLayout(false);
            this.pnl_Demo_Mobile.ResumeLayout(false);
            this.pnl_HomePhone.ResumeLayout(false);
            this.pnl_Demo_Gender.ResumeLayout(false);
            this.pnl_Demo_DOB.ResumeLayout(false);
            this.pnl_Demo_address.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.pnlBadDebt.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPD_Photo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCards)).EndInit();
            this.pnlCards.ResumeLayout(false);
            this.pnlCardsContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gb_Cards)).EndInit();
            this.gb_Cards.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPC_Cards)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel22.ResumeLayout(false);
            this.pnlPatient_List.ResumeLayout(false);
            this.pnlPatient.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnl_ts_Main.ResumeLayout(false);
            this.pnl_ts_Main.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.cmnu_Tasks.ResumeLayout(false);
            this.cmnu_PatientDetails.ResumeLayout(false);
            this.cmnu_PatientList.ResumeLayout(false);
            this.cmnuToolStripCustomize.ResumeLayout(false);
            this.cm_Task.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuMainMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFile_New;
        private System.Windows.Forms.ToolStripMenuItem mnuFile_Modify;
        private System.Windows.Forms.ToolStripMenuItem mnuFile_Delete;
        private System.Windows.Forms.ToolStripMenuItem mnuFile_Refresh;
        private System.Windows.Forms.ToolStripMenuItem mnuFile_Close;
        private System.Windows.Forms.ToolStripMenuItem mnuFile_Lock;
        private System.Windows.Forms.ToolStripMenuItem mnuFile_Exit;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_AppointmentBook;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_BillingBook;
        private System.Windows.Forms.ToolStripMenuItem mnuSecurity;
        private System.Windows.Forms.ToolStripMenuItem mnuSecurity_UserManagement;
        private System.Windows.Forms.ToolStripMenuItem mnuSecurity_SystemManagemnet;
        private System.Windows.Forms.ToolStripMenuItem mnuSecurity_PasswordPolicy;
        private System.Windows.Forms.ToolStripMenuItem mnuSecurity_PatientLog;
        private System.Windows.Forms.ToolStripMenuItem mnuSecurity_Forms;
        private System.Windows.Forms.ToolStripMenuItem mnuGo;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_NewPatient;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_CardScanning;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_Appointment;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_Schedule;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_Billing;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_BatchProcessing;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_ClaimProcessing;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_Payment;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuTools_Import;
        private System.Windows.Forms.ToolStripMenuItem mnuTools_Export;
        private System.Windows.Forms.ToolStripMenuItem mnuTools_Synchronize;
        private System.Windows.Forms.ToolStripMenuItem mnuReports;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_PracticeAnalysis;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_OvreduePatientPayment;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_OverdueInsurancePayment;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_PrintLabels;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_PrintLabels_Patient;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_PrintLabels_Insurance;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_PrintLabels_Contacts;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_PrintLabels_Employies;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_PrintList;
        private System.Windows.Forms.ToolStripMenuItem patientToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem guarantorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insurancesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem billingCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem diagnosisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_Graphs;
        private System.Windows.Forms.ToolStripMenuItem munView;
        private System.Windows.Forms.ToolStripMenuItem mnuView_Appointment;
        private System.Windows.Forms.ToolStripMenuItem mnuView_Schedule;
        private System.Windows.Forms.ToolStripMenuItem mnuView_Reminders;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem mnuView_Billing;
        private System.Windows.Forms.ToolStripMenuItem mnuView_BatchProcessing;
        private System.Windows.Forms.ToolStripMenuItem mnuView_ClaimProcessing;
        private System.Windows.Forms.ToolStripMenuItem mnuView_Payment;
        private System.Windows.Forms.ToolStripMenuItem mnuSetting;
        private System.Windows.Forms.ToolStripMenuItem mnuSetting_DefaultPatientSetting;
        private System.Windows.Forms.ToolStripMenuItem mnuSetting_SystemSetting;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem mnuSetting_Appointment;
        private System.Windows.Forms.ToolStripMenuItem mnuSetting_Schedule;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem mnuSetting_CardScanner;
        private System.Windows.Forms.ToolStripMenuItem mnuSetting_Printer;
        private System.Windows.Forms.ToolStripMenuItem mnuWindow;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp_HowDoI;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp_Search;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp_Contents;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp_TechnicalSupport;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp_Version;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp_License;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp_AboutgloPMS;
        private System.Windows.Forms.ToolStripMenuItem mnuSupport;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private gloGlobal.gloToolStripIgnoreFocus ts_Main;
        private System.Windows.Forms.ToolStripButton tsb_PatientRegistration;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsb_Calendar;
        private System.Windows.Forms.ToolStripButton tsb_Appointment;
        private System.Windows.Forms.ToolStripButton tsb_Billing;
        private System.Windows.Forms.ToolStripButton tsb_Payment;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripButton tsb_Exit;
        private Janus.Windows.UI.Dock.UIPanelManager uiPanelManager1;
        private Janus.Windows.UI.Dock.UIPanel pnlPatient_UpComingAppointments;
        private Janus.Windows.UI.Dock.UIPanelInnerContainer pnlPatient_UpComingAppointmentsContainer;
        private Janus.Windows.UI.Dock.UIPanel pnlPatient_Demo;
        private Janus.Windows.UI.Dock.UIPanelInnerContainer pnlPatient_DemographicsContainer;
        private Janus.Windows.UI.Dock.UIPanel pnlPatient_Details;
        private Janus.Windows.UI.Dock.UIPanelInnerContainer pnlPatient_DetailsContainer;
        private System.Windows.Forms.Panel pnlPatient_List;
        private gloPatient.PatientListControl gloCntrlPatient;
        private System.Windows.Forms.Panel pnlPatient;
        private System.Windows.Forms.Label label1;
        private gloGlobal.gloToolStripIgnoreFocus ts_PatientDetail;
        private System.Windows.Forms.ToolStripButton tsb_PD_Insurance;
        private System.Windows.Forms.ToolStripButton tsb_PD_Appointments;
        private System.Windows.Forms.ToolStripButton tsb_PD_Procedure;
        private System.Windows.Forms.ToolStripButton tsb_PD_Billing;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PatientDetails;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private Janus.Windows.EditControls.UIGroupBox gb_Cards;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox picPC_Cards;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private Janus.Windows.EditControls.UIGroupBox gb_Demographics;
        private System.Windows.Forms.Label lblPD_DOBCaption;
        private System.Windows.Forms.Label lblPD_EmailCaption;
        private System.Windows.Forms.Label lblPD_MobilePhoneCaption;
        private System.Windows.Forms.Label lblPD_HomePhoneCaption;
        private System.Windows.Forms.Label lblPD_FaxPhoneCaption;
        private System.Windows.Forms.PictureBox picPD_Photo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator22;
        private System.Windows.Forms.ToolStripButton tsb_BillingBatch;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;
        private System.Windows.Forms.Timer tmr_Dashboard;
        private System.Windows.Forms.ToolStripButton tsb_PD_Referral;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;
        private System.Windows.Forms.ToolStripMenuItem mnuSetting_Theme2003;
        private System.Windows.Forms.ToolStripMenuItem mnuSetting_Theme2003Dark;
        private System.Windows.Forms.ToolStripMenuItem mnuSetting_Theme2007;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnPC_DeleteCard;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnPC_ScanCard;
        private System.Windows.Forms.Button btnPC_MoveLast;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnPC_MoveNext;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnPC_MovePrevious;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnPC_MoveFirst;
        private System.Windows.Forms.ToolStripButton tsb_PatientModification;
        private System.Windows.Forms.ToolStripButton tsb_ShowDashboard;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_ModifyPatient;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_ReportingTools;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_Reports;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator25;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_TaskMailBook;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel pnl_ts_Main;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Timer timerLockScreen;
        private System.Windows.Forms.ToolStripMenuItem blockUnblockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_Contacts;
        private System.Windows.Forms.ToolStripButton tsb_PatientScan;
        private System.Windows.Forms.Panel pnl_PD_Status;
        private System.Windows.Forms.Label lbl_PD_Status1;
        private System.Windows.Forms.Label lbl_PD_Status;
        private System.Windows.Forms.ToolStripMenuItem mnuSetting_Billing;
        private System.Windows.Forms.ContextMenuStrip cmnu_Tasks;
        private System.Windows.Forms.ToolStripMenuItem cmnuItem_Priority;
        private System.Windows.Forms.ToolStripMenuItem cmunItem_Completed;
        private System.Windows.Forms.ToolStripMenuItem cmnuItem_0Percent;
        private System.Windows.Forms.ToolStripMenuItem cmnuItem_25Percent;
        private System.Windows.Forms.ToolStripMenuItem cmnuItem_50Percent;
        private System.Windows.Forms.ToolStripMenuItem cmnuItem_75Percent;
        private System.Windows.Forms.ToolStripMenuItem cmnuItem_100Percent;
        private System.Windows.Forms.ImageList imgList_Priority;
        private System.Windows.Forms.ToolStripButton tsb_PatBalance;
        private System.Windows.Forms.ToolStripButton tsb_PatLedger;
        private System.Windows.Forms.ToolStripButton tsb_Remittance;
        private System.Windows.Forms.ContextMenuStrip cmnu_PatientDetails;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_Authorization;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_ClaimStatus;
        private System.Windows.Forms.ToolStripMenuItem payerListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_eligibilityCheck;
        private System.Windows.Forms.ToolStripButton tsb_PD_Cases;
        private System.Windows.Forms.ContextMenuStrip cmnu_PatientList;
        private System.Windows.Forms.ToolStripMenuItem cmnuPatientItem_CheckIn;
        private System.Windows.Forms.ToolStripMenuItem cmnuPatientItem_CheckOut;
        private System.Windows.Forms.ToolStripMenuItem mnuReportsTransactionHistory;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_copay;
        private System.Windows.Forms.ToolStripMenuItem mnuTransactionHistoryAnalysis;
        private System.Windows.Forms.ToolStripMenuItem mnu_patientReport;
        private Janus.Windows.UI.Dock.UIPanelInnerContainer uiPanel0Container;
        private System.Windows.Forms.ToolStripMenuItem mnu_rpt_Appointments;
        private System.Windows.Forms.ToolStripMenuItem cmnuPatientItem_Template;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PatientStatus;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_ZeroBalancePatient;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_DailyCollectionReport;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_gatewayEDI;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMyReportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listOfClaimsRecivedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem myFilesRecivedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem staffProductivityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem myAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem myFileLocationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateMyWebAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeMyPasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewProviderInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem referringProviderNPITableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enrollmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem providerEnrollmentFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem claimsStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patientSearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem providerSearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insurerSearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem secondarySearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem advancedSearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem realtimeClaimStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cCISuspensionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem claimHistoryToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rejectionAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transactionSummaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem safetyNetReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem claimsFileReconcliToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bestPracticesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem remitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eligibilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem batchInquiriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newRealtimeInquiriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takePaymentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeCreditCardPaymentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeACHPaymentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem managePaymentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cCAuthorizationFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aCHAuthorizationFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem patientStatementsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sampleStatementsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuView_PatientTemplates;
        private System.Windows.Forms.ToolStripMenuItem mnuRpt_BatchPrint;
        private System.Windows.Forms.ToolStripMenuItem mnu_MissingChargesReport;
        private System.Windows.Forms.ToolStripMenuItem mnu_MonthEndReport;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_PatientVsEstablishedReport;
        private System.Windows.Forms.Panel pnl_Demo_DOB;
        private System.Windows.Forms.Panel pnl_Demo_address;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnl_Demo_Mobile;
        private System.Windows.Forms.Panel pnl_HomePhone;
        private System.Windows.Forms.Panel pnl_Fax;
        private System.Windows.Forms.Panel pnl_Email;
        private System.Windows.Forms.Panel pnl_EMmobile;
        private System.Windows.Forms.Label lblEMMobileCaption;
        private System.Windows.Forms.Panel pnl_EmPhone;
        private System.Windows.Forms.Label lblEMPhoneCaption;
        private System.Windows.Forms.Panel pnl_EmContacts;
        private System.Windows.Forms.Label lblEMContactCaption;
        private System.Windows.Forms.Panel pnl_Demo_Gender;
        private System.Windows.Forms.Label lblGenderCaption;
        private System.Windows.Forms.Panel pnl_PD_ContactInfo;
        private System.Windows.Forms.ToolStripMenuItem cmnuPatientItem_PatientAlert;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_Ledger;      
        private Janus.Windows.UI.Dock.UIPanel uipnlPatient_Alert;
        private Janus.Windows.UI.Dock.UIPanelInnerContainer uipnlPatient_AlertContainer;
        private System.Windows.Forms.Panel pnlPatientAlertMain;
        private System.Windows.Forms.Panel pnl_LeftButtons;
        private System.Windows.Forms.Panel pnlLeftPatientAlert;
        private C1.Win.C1FlexGrid.C1FlexGrid c1PatientAlerts;
        private System.Windows.Forms.Button btnTask;
        private System.Windows.Forms.Button btnAppointment;
        private System.Windows.Forms.Button btnCalender;
        private System.Windows.Forms.Button btnMail;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Panel pnlc1PatientAlerts;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.ToolStripMenuItem climsReviewedToolStripMenuItem2;
        private System.Windows.Forms.Panel pnlSideButton;
        private System.Windows.Forms.Panel pnl_Appointment;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Button btn_Appointment;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Panel pnl_Calendar;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Button btn_Calendar;
        private System.Windows.Forms.Panel pnl_Tasks;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Button btn_Tasks;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_AuditTrail;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_ProviderReferral_Patients;
        private System.Windows.Forms.ToolStripMenuItem mnuReportsRefund;
        private System.Windows.Forms.ToolStripButton tsb_LockScreen;
        internal System.Windows.Forms.ToolStripStatusLabel sslbl_Version;
        internal System.Windows.Forms.ToolStripStatusLabel sslbl_LastModifiedDate;
        //private System.Windows.Forms.ToolStripButton tsb_RestoreDashboard;
        private System.Windows.Forms.ToolStripMenuItem mnuSetting_DefaultDashboard;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_Payment;
        private System.Windows.Forms.ToolStripMenuItem mnuView_TemplateGallary;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_coverage;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_Modify;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_Add;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_RemoveRef;
        private System.Windows.Forms.ToolStripMenuItem mnuTools_MergePatient;
        private System.Windows.Forms.ToolStripMenuItem mnuTools_MergePatientAccount;
        private System.Windows.Forms.ToolStripMenuItem mnu_rpt_NoShowAppointments;
        private System.Windows.Forms.ToolStripMenuItem mnuView_Tasks;
        private System.Windows.Forms.ToolStripMenuItem mnuTransactionNotes;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_TemplateAssociation;
        private System.Windows.Forms.ToolStripMenuItem mnu_ChargesPayments;
        private System.Windows.Forms.ToolStripButton tsb_Advance;
        private System.Windows.Forms.ToolStripMenuItem mnuTools_CardImage;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel pnlCopayAlert;
        private System.Windows.Forms.ToolStripMenuItem mnuSetting_Customization;
        private System.Windows.Forms.ToolStripMenuItem mnuTools_UpdateTemplates;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel pnlEligibilityCheck;
        private C1.Win.C1FlexGrid.C1FlexGrid c1EligibilityCheck;
        private System.Windows.Forms.ToolStripMenuItem mnu_PatientRecall;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ProductionByDoctor;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ProductionByFacility;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ProductionByDate;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ProductionByMonth;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ProductionByInsuranceCarrier;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ReimbursementByMonth;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ReimbursementByInsuranceCarrier;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ReimbursementByInsuranceByCPT;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ProductionByProcedureCode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator26;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_PatientPaymentHistory;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_PatientTransactionHistory;
        private System.Windows.Forms.Panel pnl_PD_Physician;
        private System.Windows.Forms.Label lblPD_PhysicianCaption;
        private System.Windows.Forms.Panel pnl_PD_Referral;
        private System.Windows.Forms.Label lblPD_ReferralCaption;
        private System.Windows.Forms.Panel pnl_PD_Pharmacy;
        private System.Windows.Forms.Label lblPD_PharmacyCaption;
        private System.Windows.Forms.Panel pnl_PD_Provider;
        private System.Windows.Forms.Label lblProviderCaption;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ProductionByFacilityByPatient;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ProductionByFacilityByPatientDetail;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ReimbursementByCPTByInsurance;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ReimbursementByDoctorByInsurance;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ProductionByPhysicianGroup;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ReimbursementByMonthDetail;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ProductionAnalysisByFacility;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ReimbursementByInsuranceForCPT;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ProductionByProcedureGroup;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ProductionAnalysisByprocedureGroup;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ReimbursementDetailsByAccount;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ProductionAnalysisandTrendsByMonth;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ProductionTrendsByProcedureGrop;
        private System.Windows.Forms.Panel pnl_PD_PCPMobile;
        private System.Windows.Forms.Label lblPD_PCP_MobileCaption;
        private System.Windows.Forms.Panel pnl_PD_PCPPhone;
        private System.Windows.Forms.Label lblPD_PCP_PhoneCaption;
        private System.Windows.Forms.Label lblPD_Age;
        internal System.Windows.Forms.ContextMenuStrip cmnuToolStripCustomize;
        internal System.Windows.Forms.ToolStripMenuItem CustomizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_ClosedJournals;
        private System.Windows.Forms.ToolStripMenuItem mnuTools_UnLockRecords;
        private System.Windows.Forms.Label lblPD_Address;
        private System.Windows.Forms.Label lblPD_DateofBirth;
        private System.Windows.Forms.Label lblPD_Gender;
        private System.Windows.Forms.Label lblPD_HomePhone;
        private System.Windows.Forms.Label lblPD_Referral;
        private System.Windows.Forms.Label lblPD_Physician;
        private System.Windows.Forms.Label lblPD_Pharmacy;
        private System.Windows.Forms.Label lblProvider;
        private System.Windows.Forms.Label lblEMMobile;
        private System.Windows.Forms.Label lblEMPhone;
        private System.Windows.Forms.Label lblEMContact;
        private System.Windows.Forms.Label lblPD_Email;
        private System.Windows.Forms.Label lblPD_FaxPhone;
        private System.Windows.Forms.Label lblPD_MobilePhone;
        private System.Windows.Forms.Label lblPD_PCP_Phone;
        private System.Windows.Forms.Label lblPD_PCP_Mobile;
        private System.Windows.Forms.Label lblPD_Code;
        private System.Windows.Forms.Label lblPD_Name;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_ChargesTray;
        private System.Windows.Forms.ToolStripMenuItem mnuReportsPendingCopayReport;
        private System.Windows.Forms.ToolStripButton tsb_Calculator;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_PatientStatementNotes;
        private System.Windows.Forms.ToolStripMenuItem mnu_Reports_PatientByDOBReport;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.ToolStripMenuItem mnu_Reports_DplicateCliam;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_Remittance;
        private System.Windows.Forms.ToolStripMenuItem mnuView_PatientBatchStatement;
        private System.Windows.Forms.ToolStripMenuItem mnu_rpt_PaymentTrayReport;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Button btnPC_PrintCards;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDoc;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling_PaymentTray;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling_PatPayment;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling_InsPayment;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling_Ledger;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling_DailyClose;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling_Remittance;
        private System.Windows.Forms.ToolStripMenuItem mnuBilling_Charges;
        private System.Windows.Forms.ToolStripMenuItem mnu_rpt_AppointmentWaiting;
        internal System.Windows.Forms.Panel pnlSearchFilter;
        internal System.Windows.Forms.Panel panel14;
        internal System.Windows.Forms.ComboBox cmbProvider;
        internal System.Windows.Forms.Label label62;
        internal System.Windows.Forms.Panel panel15;
        internal System.Windows.Forms.ComboBox cmbStatus;
        internal System.Windows.Forms.Label lblStatus;
        internal System.Windows.Forms.Panel panel16;
        internal System.Windows.Forms.DateTimePicker dtpToDate;
        internal System.Windows.Forms.Label lblto;
        internal System.Windows.Forms.Panel panel17;
        internal System.Windows.Forms.DateTimePicker dtpFromDate;
        internal System.Windows.Forms.Label lblFrom;
        internal System.Windows.Forms.Panel panel18;
        internal System.Windows.Forms.Label label63;
        private System.Windows.Forms.ToolStripButton tsb_PaymentPatient;
        private System.Windows.Forms.ToolStripButton tsb_PaymentInsurance;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_PaymentPatient;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_PaymentInsurace;
        private System.Windows.Forms.ToolStripMenuItem mnuRpt_VoidClaims;
        private System.Windows.Forms.ToolStripButton tsb_PatientStatment;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_PatientStatment;
        private System.Windows.Forms.ToolStripMenuItem mnu_InsuranceCompanySetup;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_Aging;
        private System.Windows.Forms.ToolStripMenuItem mnu_InsuranceCompanySetup_Company_Category;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_PatientLedger;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_AvailableReserve;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator28;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_DailyClose;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_FinancialSummary;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_DailyCharges;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_DailyPayments;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_DailySummary;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_MonthlyCharges;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_MonthlyPayments;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator29;
        private Janus.Windows.UI.Dock.UIPanel pnlCards;
        private Janus.Windows.UI.Dock.UIPanelInnerContainer pnlCardsContainer;
        private Janus.Windows.UI.Dock.UIPanelGroup pnlPatient_Demographics;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.ToolStripButton tsbDailyClose;
        private C1.Win.C1FlexGrid.C1FlexGrid c1CopayAlert;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_EOBLedger;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_ERAPayment;
        private System.Windows.Forms.ToolStripButton tsb_ERAPayment;
        private System.Windows.Forms.ToolStripButton tsb_PD_PriorAuthorization;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_MonthlyClose;
        private System.Windows.Forms.ToolStripMenuItem mnuPriorAuthReport;
        private System.Windows.Forms.ToolStripMenuItem mnu_Reports_PatientExcludeSt;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_Productivity;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_FinancialPayments;
        private System.Windows.Forms.ToolStripMenuItem cmnuPatientItem_SaveAsCopy;
        private System.Windows.Forms.ToolStripMenuItem cmnuPatientItem_Cases;
        private System.Windows.Forms.Timer tmrCopayAlertBlink;
        private System.Windows.Forms.ImageList imgList_ApptPrint;
        private System.Windows.Forms.ToolStripMenuItem mnu_Reports_BatchReport;
        private System.Windows.Forms.Label lblScannedDate;
        private System.Windows.Forms.Panel panel22;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_eligibilityCheckTest;
        private System.Windows.Forms.PictureBox _picBkg;
        private gloEMR.Help.HelpComponent HelpComponent1;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit1;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit2;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ProductivityRVU;
        private System.Windows.Forms.ToolStripMenuItem messageQueueToolStripMenuItem;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit3;
        private System.Windows.Forms.ToolStripMenuItem mnuInterfaceReports;
        private System.Windows.Forms.ToolStripMenuItem mnuPatientActivationtReport;
        internal System.Windows.Forms.ToolStripButton tsb_PatientTasks;
        private System.Windows.Forms.ContextMenuStrip cm_Task;
        private System.Windows.Forms.ToolStripMenuItem cmu_NewTask;
        private System.Windows.Forms.ToolStripMenuItem cmu_OpenTask;
        private System.Windows.Forms.ToolStripMenuItem cmu_MarkCompleted;
        private System.Windows.Forms.ToolStripMenuItem cmu_Delete;
        private System.Windows.Forms.ToolStripMenuItem cmu_FollowUp;
        private System.Windows.Forms.ToolStripMenuItem cmu_Priority;
        private System.Windows.Forms.ToolStripMenuItem cmu_Complete;
        private System.Windows.Forms.ToolStripMenuItem smn_Zero;
        private System.Windows.Forms.ToolStripMenuItem smn_Quater;
        private System.Windows.Forms.ToolStripMenuItem smn_Half;
        private System.Windows.Forms.ToolStripMenuItem smn_ThreeQuater;
        private System.Windows.Forms.ToolStripMenuItem smn_Full;
        private System.Windows.Forms.ToolStripMenuItem cmu_AcceptTask;
        private System.Windows.Forms.ToolStripMenuItem cmu_DeclineTask;
        internal System.Windows.Forms.ToolStripStatusLabel sslbl_Database;
        internal System.Windows.Forms.ToolStripStatusLabel sslbl_Login;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_ICD9CPTGallery;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ProductivityProviderPaymentMthd;
        private System.Windows.Forms.ToolStripMenuItem mnuBatch_Eligibility;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit4;
        private System.Windows.Forms.ToolStripMenuItem batchEligibilityActivityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_RevenueCycle;
        private System.Windows.Forms.ToolStripButton tsb_RevenueCycle;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_ViewBenefits;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit5;
        private System.Windows.Forms.ToolStripMenuItem mnuReimbursementWarning;
        private System.Windows.Forms.ToolStripMenuItem mnuBusCenterMismatch;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_ModifyCharges;
        private System.Windows.Forms.ToolStripMenuItem mnuChargeLagReport;
        private System.Windows.Forms.ToolStripMenuItem mnuBatchLagReport;
        private System.Windows.Forms.Panel pnl_Race;
        private System.Windows.Forms.Label lblRace;
        private System.Windows.Forms.Label lblRaceCaption;
        private System.Windows.Forms.Panel pnl_Ethinicity;
        private System.Windows.Forms.Label lblEthinicity;
        private System.Windows.Forms.Label lblEthinicityCaption;
        private System.Windows.Forms.Panel pnl_Language;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.Label lblLanguageCaption;
        private C1.Win.C1SuperTooltip.C1SuperTooltip C1SuperTooltip1;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ProductivityDOS;
        private System.Windows.Forms.ToolStripMenuItem mnuMaster_ICD9Gallery;
        private System.Windows.Forms.ToolStripMenuItem mnuMaster_ICD10Gallery;
        private System.Windows.Forms.ToolStripMenuItem mnuMaster_CPTGallery;
        private System.Windows.Forms.ToolStripMenuItem mnuICDAnalysis;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit6;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit7;
        private System.Windows.Forms.CheckBox chkApptDate;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit8;
        private System.Windows.Forms.ToolStripButton tsb_ScanDocs;
        private System.Windows.Forms.ToolStripButton tsb_RCMDocs;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_ScanDocument;
        private System.Windows.Forms.ToolStripMenuItem mnuView_Documents;
        internal System.Windows.Forms.ToolStripButton tsb_PDViewDocument;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_CopayDistributionList;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ExpectedCollection;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit9;
        internal System.Windows.Forms.ToolStripButton tsb_NYWCForms;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_NewWCForm;
        private System.Windows.Forms.ToolStripMenuItem mnuItem_ModifyWCForm;
        private System.Windows.Forms.ToolStripMenuItem c4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c42ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem c4AUTHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mG2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mG21ToolStripMenuItem;
        private System.Windows.Forms.Panel pnl_PatStatus;
        private System.Windows.Forms.Label lblPatStatus;
        private System.Windows.Forms.Label lblstCaption;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_FinancialProSummary;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit10;
        private System.Windows.Forms.Panel pnl_MedCat;
        private System.Windows.Forms.Label lblPatMedCat;
        private System.Windows.Forms.Label lblMedCatCaption;
        private System.Windows.Forms.ToolStripMenuItem mnuTools_CodeGuide;
        private System.Windows.Forms.ToolStripMenuItem mnu_rpt_MissedOpportunitiesReport;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit11;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_DenialManagement;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_ChargeEditReport;
        private System.Windows.Forms.ToolStripMenuItem mnu_rpt_AppointmentCensusReport;
        private System.Windows.Forms.Panel pnlBusinessCenter;
        private System.Windows.Forms.Label lblBusinessCenter;
        private System.Windows.Forms.Label lblBusinessCenterCaption;
        private System.Windows.Forms.Panel pnl_WorkPhone;
        private System.Windows.Forms.Label lblWorkPhone;
        private System.Windows.Forms.Label lblWorkPhoneCaption;
        private System.Windows.Forms.Panel pnl_Occupation;
        private System.Windows.Forms.Label lblOccupation;
        private System.Windows.Forms.Label lblOccupationCaption;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_InactiveCPTSReport;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit12;
        private System.Windows.Forms.Panel pnlBadDebt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_ReservesDistributionList;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_Fin_ProdSummaryDX;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit13;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_AgedPayment;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit_RCMCagetory;
        private System.Windows.Forms.ToolStripMenuItem mnu_rpt_PatientBenefitsInfo;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit14;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator27;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_CPTAnalysis;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_QualityMeasures;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_ChargesVSAllowedReport;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_PayerLagReport;
        private System.Windows.Forms.ToolStripMenuItem mnuReports_CollectionExport;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_PaymentPlanReport;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit15;
        private System.Windows.Forms.Timer tmrSingleSignOn;
        internal System.Windows.Forms.ToolStripStatusLabel sslbl_SingleSignOn;
        internal System.Windows.Forms.Panel pnl_TertiaryInsurance;
        internal System.Windows.Forms.Label lbl_TertiaryInsurance;
        private System.Windows.Forms.Label Label90;
        internal System.Windows.Forms.Panel pnl_SecondaryInsurance;
        internal System.Windows.Forms.Label lblSecondaryInsurance;
        private System.Windows.Forms.Label Label91;
        internal System.Windows.Forms.Panel pnl_PrimaryInsurance;
        internal System.Windows.Forms.Label lblPrimaryInsurance;
        private System.Windows.Forms.Label Label92;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit16;
        internal System.Windows.Forms.ToolStripMenuItem mnu_rpt_ConfirmAppointments;
        private System.Windows.Forms.ToolStripMenuItem mnuTools_MergeScheduledActions;
        private System.Windows.Forms.ToolStripMenuItem mnuGo_CollectionAgencyRefund;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_BadDebtCollectionReport;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_CachedFinancialProductivitySum;
        private System.Windows.Forms.ToolStripMenuItem mnuTools_ChangePassword;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit17;
        private System.Windows.Forms.ToolStripMenuItem mnuSetting_RefreshDevicesPrinters;
        private System.Windows.Forms.ToolStripMenuItem mnu_MISReports_MTDYTDReport;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit18;
        private System.Windows.Forms.ToolStripMenuItem mnuCopayDist_ByAccount;
        private System.Windows.Forms.ToolStripMenuItem mnuCopayDist_ByCharge;
        private System.Windows.Forms.ToolStripButton tsb_ClearGagePayment;
        private Reports.CachedrptSummaryOfVisit cachedrptSummaryOfVisit19;
        private System.Windows.Forms.ToolStripMenuItem mnuView_CleargageFileHistory;
    }
}
