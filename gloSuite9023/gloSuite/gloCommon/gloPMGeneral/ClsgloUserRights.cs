using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace gloPMGeneral
{

    public class ClsgloUserRights : IDisposable
    {


        #region "Constructors"

        public ClsgloUserRights(String DatabaseConnectionstring)
        {
            _databaseconnectionstring = DatabaseConnectionstring;
        }
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~ClsgloUserRights()
        {
            Dispose(false);
        }

        #endregion


        #region "Private Variables"

        private string _databaseconnectionstring = "";

        //Appointment Configuration
        private Boolean _bResource;
        private Boolean _bABType;
        private Boolean _bAType;
        private Boolean _bPType;
        private Boolean _bAStatus;
        private Boolean _bLocation;
        private Boolean _bDepartment;
        private Boolean _bTemplate;
        private Boolean _bProviderATS;
        private Boolean _bFollowup;


        //Billing Configuration
        private Boolean _bICD9;
        private Boolean _bCPT;
        private Boolean _bDrugs;
        private Boolean _bTOS;
        private Boolean _bCategory;
        private Boolean _bPOS;
        private Boolean _bFacility;
        private Boolean _bsTreatment;
        private Boolean _bCType;
        private Boolean _bPatRelationship;
        private Boolean _bFlagType;
        private Boolean _bSpecialty;
        private Boolean _bInsServiceType;
        private Boolean _bInsPlanCodes;
        private Boolean _bInsType;
        private Boolean _bAdjType;
        private Boolean _bFSchedule;
        private Boolean _bFScheduleAllocation;
        private Boolean _bCCardType;
        private Boolean _bScrubber;
        private Boolean _bRefCPT;
        private Boolean _bCScrubber;
        private Boolean _bInValidICD9;

        //Task Mail Configuration
        private Boolean _bTaskFollowup;
        private Boolean _bSType;
        private Boolean _bPriorType;
        private Boolean _bSignature;


        private Boolean _bTemplateGallery;
        private Boolean _bTemplateAssociation;


        //GO
        private Boolean _bNewPatient;
        private Boolean _bModifyPatient;
        private Boolean _bCardScanning;
        private Boolean _bAppointment;
        private Boolean _bSchedule;
        private Boolean _bCharges;
        private Boolean _bBatch;
        private Boolean _bPayment;

        //View
        private Boolean _bPatientForms;
        private Boolean _bTasks;


        //Reports
        private Boolean _bPatPayHistory;
        private Boolean _bPatTransHistory;
        private Boolean _bPatStatement;
        private Boolean _bProdByDoctor;
        private Boolean _bProdByFacility;
        private Boolean _bProdByDate;
        private Boolean _bProdByMonth;
        private Boolean _bProdByInsCarrier;
        private Boolean _bProdByProcGroup;
        private Boolean _bProdByProcCode;
        private Boolean _bProdByFacilityByPatSummary;
        private Boolean _bProdByFacilityByPatDetail;
        private Boolean _bReimbByMonth;
        private Boolean _bReimbByMonthByAcc;
        private Boolean _bReimbByInsCarrier;
        private Boolean _bReimbByInsCarrierByCPT;
        private Boolean _bReimbByCPTByInsCarrier;
        private Boolean _bReimbByDocByInsCarrier;
        private Boolean _bReimbByInsCarrierForCPT;
        private Boolean _bReimbDetailsByAcc;
        private Boolean _bASumary;
        private Boolean _bASumaryByPat;
        private Boolean _bASumaryByInsCarrier;
        private Boolean _bProdByPhysicianGroup;
        private Boolean _bProdAnalysisByFacility;
        private Boolean _bProdAnalysisByPG;
        private Boolean _bProdAnalysisByTrendsByMonth;
        private Boolean _bProdTrendsByProcGroup;
        private Boolean _bAppointments;
        private Boolean _bCancelAppointments;
        private Boolean _bBatchPrintTemp;
        private Boolean _bMissingCharges;
        private Boolean _bChargesSummaryReport;
        private Boolean _bNewPatvsEstPat;
        private Boolean _bRefund;


        private Boolean _bPatBalance;
        private Boolean _bTransHistory;
        private Boolean _bTransNotes;
        private Boolean _bTransHistoryAnalysis;
        private Boolean _bPatReport;
        private Boolean _bProvRefPat ;
        private Boolean _bCashFlowReport;
        private Boolean _bPatRecall;

        private Boolean _bPrintList;
        private Boolean _bGatewayEDI;
        private Boolean _bAuditTrial;


        //Tools
        private Boolean _bImportTemplates;
        private Boolean _bExportTemplates;
        private Boolean _bUpdateTemplates;
        private Boolean _bMergePatient;
        private Boolean _bCardImage;

        //Settings
        private Boolean _bRestoreDashBoard;
        private Boolean _bSystemSetting;
        private Boolean _bCardScanner;
        private Boolean _bOffice2003Theme;
        private Boolean _bOffice2003DarkTheme;
       // private Boolean _bOffice2007Theme;
        private Boolean _bCustomizeToolBar;


        //Help
        private Boolean _bSearch;
        private Boolean _bHowDoI;
        private Boolean _bContents;
        private Boolean _bTSupport;
        private Boolean _bAbout;

        //DashBoard
        private Boolean _bCalender;
        private Boolean _bScanPatient;
        private Boolean _bRemittance;
        private Boolean _bAdvance;
        private Boolean _bLedger;
        private Boolean _bBalance;



       

        #endregion


        #region "Property Procedures"

        #region "Appointment Configuration"

        public Boolean Resource
        {
            get { return _bResource; }
            set { _bResource = value; }
        }

        public Boolean AppointmentBlockType
        {
            get { return _bABType; }
            set { _bABType = value; }
        }

        public Boolean AppointmentType
        {
            get { return _bAType; }
            set { _bAType = value; }
        }

        public Boolean ProblemType
        {
            get { return _bPType; }
            set { _bPType = value; }
        }

        public Boolean AppointmentStatus
        {
            get { return _bAStatus; }
            set { _bAStatus = value; }
        }

        public Boolean Location
        {
            get { return _bLocation; }
            set { _bLocation = value; }
        }

        public Boolean Department
        {
            get { return _bDepartment; }
            set { _bDepartment = value; }
        }

        public Boolean Template
        {
            get { return _bTemplate; }
            set { _bTemplate = value; }
        }

        public Boolean ProviderATS
        {
            get { return _bProviderATS; }
            set { _bProviderATS = value; }
        }

        public Boolean Followup
        {
            get { return _bFollowup; }
            set { _bFollowup = value; }
        }

        #endregion


        #region "Billing Configuration

        public Boolean ICD9
        {
            get { return _bICD9; }
            set { _bICD9 = value; }
        }

        public Boolean CPT
        {
            get { return _bCPT; }
            set { _bCPT = value; }
        }

        public Boolean Drugs
        {
            get { return _bDrugs; }
            set { _bDrugs = value; }
        }

        public Boolean Category
        {
            get { return _bCategory; }
            set { _bCategory = value; }
        }

        public Boolean TOS
        {
            get { return _bTOS; }
            set { _bTOS = value; }
        }

        public Boolean POS
        {
            get { return _bPOS; }
            set { _bPOS = value; }
        }

        public Boolean Facility
        {
            get { return _bFacility; }
            set { _bFacility = value; }
        }

        public Boolean smartTreatment
        {
            get { return _bsTreatment; }
            set { _bsTreatment = value; }
        }

        public Boolean CodeType
        {
            get { return _bCType; }
            set { _bCType = value; }
        }

        public Boolean PatientRelationship
        {
            get { return _bPatRelationship; }
            set { _bPatRelationship = value; }
        }

        public Boolean FlagType
        {
            get { return _bFlagType; }
            set { _bFlagType = value; }
        }

        public Boolean Specialty
        {
            get { return _bSpecialty; }
            set { _bSpecialty = value; }
        }

        public Boolean InsServiceType
        {
            get { return _bInsServiceType; }
            set { _bInsServiceType = value; }
        }

        public Boolean InsPlanCodes
        {
            get { return _bInsPlanCodes; }
            set { _bInsPlanCodes = value; }
        }

        public Boolean InsType
        {
            get { return _bInsType; }
            set { _bInsType = value; }
        }

        public Boolean AdjType
        {
            get { return _bAdjType; }
            set { _bAdjType = value; }
        }

        public Boolean FSchedule
        {
            get { return _bFSchedule; }
            set { _bFSchedule = value; }
        }

        public Boolean FScheduleAllocation
        {
            get { return _bFScheduleAllocation; }
            set { _bFScheduleAllocation = value; }
        }

        public Boolean CreditCardType
        {
            get { return _bCCardType; }
            set { _bCCardType = value; }
        }

        public Boolean Scrubber
        {
            get { return _bScrubber; }
            set { _bScrubber = value; }
        }


        public Boolean RefCPT
        {
            get { return _bRefCPT; }
            set { _bRefCPT = value; }
        }

        public Boolean CScrubber
        {
            get { return _bCScrubber; }
            set { _bCScrubber = value; }
        }

        public Boolean InValidICD9
        {
            get { return _bInValidICD9; }
            set { _bInValidICD9 = value; }
        }

        #endregion


        #region "Task Mail Configuration"

        public Boolean TaskFollowup
        {
            get { return _bTaskFollowup; }
            set { _bTaskFollowup = value; }
        }

        public Boolean StatusType
        {
            get { return _bSType; }
            set { _bSType = value; }
        }

        public Boolean PriorotyType
        {
            get { return _bPriorType; }
            set { _bPriorType = value; }
        }

        public Boolean Signature
        {
            get { return _bSignature; }
            set { _bSignature = value; }
        }


        #endregion


        #region "Others"

        public Boolean TemplateGallery
        {
            get { return _bTemplateGallery; }
            set { _bTemplateGallery = value; }
        }

        public Boolean TemplateAssociation
        {
            get { return _bTemplateAssociation; }
            set { _bTemplateAssociation = value; }
        }

        #endregion


        #region "Go"

        public Boolean NewPatient
        {
            get { return _bNewPatient; }
            set { _bNewPatient = value; }
        }

        public Boolean ModifyPatient
        {
            get { return _bModifyPatient; }
            set { _bModifyPatient = value; }
        }

        public Boolean CardScanning
        {
            get { return _bCardScanning; }
            set { _bCardScanning = value; }
        }

        public Boolean Appointment
        {
            get { return _bAppointment; }
            set { _bAppointment = value; }
        }

        public Boolean Schedule
        {
            get { return _bSchedule; }
            set { _bSchedule = value; }
        }

        public Boolean Charges
        {
            get { return _bCharges; }
            set { _bCharges = value; }
        }

        public Boolean Batch
        {
            get { return _bBatch; }
            set { _bBatch = value; }
        }

        public Boolean Payment
        {
            get { return _bPayment; }
            set { _bPayment = value; }
        }

        #endregion


        #region "View"

        public Boolean PatientForms
        {
            get { return _bPatientForms; }
            set { _bPatientForms = value; }
        }

        public Boolean Tasks
        {
            get { return _bTasks; }
            set { _bTasks = value; }
        }

        #endregion


        #region "Reports"

        public Boolean PatPayHistory
        {
            get { return _bPatPayHistory; }
            set { _bPatPayHistory = value; }
        }

        public Boolean PatTransHistory
        {
            get { return _bPatTransHistory; }
            set { _bPatTransHistory = value; }
        }

        public Boolean PatStatement
        {
            get { return _bPatStatement; }
            set { _bPatStatement = value; }
        }

        public Boolean ProdByDoctor
        {
            get { return _bProdByDoctor; }
            set { _bProdByDoctor = value; }
        }

        public Boolean ProdByFacility
        {
            get { return _bProdByFacility; }
            set { _bProdByFacility = value; }
        }

        public Boolean ProdByDate
        {
            get { return _bProdByDate; }
            set { _bProdByDate = value; }
        }

        public Boolean ProdByMonth
        {
            get { return _bProdByMonth; }
            set { _bProdByMonth = value; }
        }

        public Boolean ProdByInsCarrier
        {
            get { return _bProdByInsCarrier; }
            set { _bProdByInsCarrier = value; }
        }

        public Boolean ProdByProcGroup
        {
            get { return _bProdByProcGroup; }
            set { _bProdByProcGroup = value; }
        }

        public Boolean ProdByProcCode
        {
            get { return _bProdByProcCode; }
            set { _bProdByProcCode = value; }
        }

        public Boolean ProdByFacilityByPatSummary
        {
            get { return _bProdByFacilityByPatSummary; }
            set { _bProdByFacilityByPatSummary = value; }
        }

        public Boolean ProdByFacilityByPatDetail
        {
            get { return _bProdByFacilityByPatDetail; }
            set { _bProdByFacilityByPatDetail = value; }
        }

        public Boolean ReimbByMonth
        {
            get { return _bReimbByMonth; }
            set { _bReimbByMonth = value; }
        }

        public Boolean ReimbByMonthByAcc
        {
            get { return _bReimbByMonthByAcc; }
            set { _bReimbByMonthByAcc = value; }
        }

        public Boolean ReimbByInsCarrier
        {
            get { return _bReimbByInsCarrier; }
            set { _bReimbByInsCarrier = value; }
        }

        public Boolean ReimbByInsCarrierByCPT
        {
            get { return _bReimbByInsCarrierByCPT; }
            set { _bReimbByInsCarrierByCPT = value; }
        }

        public Boolean ReimbByCPTByInsCarrier
        {
            get { return _bReimbByCPTByInsCarrier; }
            set { _bReimbByCPTByInsCarrier = value; }
        }

        public Boolean ReimbByDocByInsCarrier
        {
            get { return _bReimbByDocByInsCarrier; }
            set { _bReimbByDocByInsCarrier = value; }
        }

        public Boolean ReimbByInsCarrierForCPT
        {
            get { return _bReimbByInsCarrierForCPT; }
            set { _bReimbByInsCarrierForCPT = value; }
        }

        public Boolean ReimbDetailsByAcc
        {
            get { return _bReimbDetailsByAcc; }
            set { _bReimbDetailsByAcc = value; }
        }

        public Boolean ASumary
        {
            get { return _bASumary; }
            set { _bASumary = value; }
        }

        public Boolean ASumaryByPat
        {
            get { return _bASumaryByPat; }
            set { _bASumaryByPat = value; }
        }

        public Boolean ASumaryByInsCarrier
        {
            get { return _bASumaryByInsCarrier; }
            set { _bASumaryByInsCarrier = value; }
        }

        public Boolean ProdByPhysicianGroup
        {
            get { return _bProdByPhysicianGroup; }
            set { _bProdByPhysicianGroup = value; }
        }

        public Boolean ProdAnalysisByFacility
        {
            get { return _bProdAnalysisByFacility; }
            set { _bProdAnalysisByFacility = value; }
        }

        public Boolean ProdAnalysisByPG
        {
            get { return _bProdAnalysisByPG; }
            set { _bProdAnalysisByPG = value; }
        }

        public Boolean ProdAnalysisByTrendsByMonth
        {
            get { return _bProdAnalysisByTrendsByMonth; }
            set { _bProdAnalysisByTrendsByMonth = value; }
        }

        public Boolean ProdTrendsByProcGroup
        {
            get { return _bProdTrendsByProcGroup; }
            set { _bProdTrendsByProcGroup = value; }
        }


        public Boolean Appointments
        {
            get { return _bAppointments; }
            set { _bAppointments = value; }
        }

        public Boolean CancelAppointments
        {
            get { return _bCancelAppointments; }
            set { _bCancelAppointments = value; }
        }

        public Boolean BatchPrintTemp
        {
            get { return _bBatchPrintTemp; }
            set { _bBatchPrintTemp = value; }
        }

        public Boolean MissingCharges
        {
            get { return _bMissingCharges; }
            set { _bMissingCharges = value; }
        }

        public Boolean ChargesSummaryReport
        {
            get { return _bChargesSummaryReport; }
            set { _bChargesSummaryReport = value; }
        }


        public Boolean NewPatvsEstPat
        {
            get { return _bNewPatvsEstPat; }
            set { _bNewPatvsEstPat = value; }
        }


        public Boolean Refund
        {
            get { return _bRefund; }
            set { _bRefund = value; }
        }


        public Boolean PatBalance
        {
            get { return _bPatBalance; }
            set { _bPatBalance = value; }
        }

        public Boolean TransHistory
        {
            get { return _bTransHistory; }
            set { _bTransHistory = value; }
        }


        public Boolean TransNotes
        {
            get { return _bTransNotes; }
            set { _bTransNotes = value; }
        }

        public Boolean TransHistoryAnalysis
        {
            get { return _bTransHistoryAnalysis; }
            set { _bTransHistoryAnalysis = value; }
        }


        public Boolean PatReport
        {
            get { return _bPatReport; }
            set { _bPatReport = value; }
        }


        public Boolean ProvRefPat
        {
            get { return _bProvRefPat; }
            set { _bProvRefPat = value; }
        }

        public Boolean CashFlowReport
        {
            get { return _bCashFlowReport; }
            set { _bCashFlowReport = value; }
        }

        public Boolean PatRecall
        {
            get { return _bPatRecall; }
            set { _bPatRecall = value; }
        }

        public Boolean PrintList
        {
            get { return _bPrintList; }
            set { _bPrintList = value; }
        }

        public Boolean GatewayEDI
        {
            get { return _bGatewayEDI; }
            set { _bGatewayEDI = value; }
        }

        public Boolean AuditTrial
        {
            get { return _bAuditTrial; }
            set { _bAuditTrial = value; }
        }

        #endregion


        #region "Tools"

        public Boolean ImportTemplates
        {
            get { return _bImportTemplates; }
            set { _bImportTemplates = value; }
        }


        public Boolean ExportTemplates
        {
            get { return _bExportTemplates; }
            set { _bExportTemplates = value; }
        }

        public Boolean UpdateTemplates
        {
            get { return _bUpdateTemplates; }
            set { _bUpdateTemplates = value; }
        }

        public Boolean MergePatient
        {
            get { return _bMergePatient; }
            set { _bMergePatient = value; }
        }

        public Boolean CardImage
        {
            get { return _bCardImage; }
            set { _bCardImage = value; }
        }

        #endregion


        #region "Settings"

        public Boolean RestoreDashBoard
        {
            get { return _bRestoreDashBoard; }
            set { _bRestoreDashBoard = value; }
        }

        public Boolean SystemSetting
        {
            get { return _bSystemSetting; }
            set { _bSystemSetting = value; }
        }

        public Boolean CardScanner
        {
            get { return _bCardScanner; }
            set { _bCardScanner = value; }
        }

        public Boolean Office2003Theme
        {
            get { return _bOffice2003Theme; }
            set { _bOffice2003Theme = value; }
        }

        public Boolean Office2003DarkTheme
        {
            get { return _bOffice2003DarkTheme; }
            set { _bOffice2003DarkTheme = value; }
        }

        public Boolean Office2007Theme
        {
            get { return _bOffice2003Theme; }
            set { _bOffice2003Theme = value; }
        }

        public Boolean CustomizeToolBar
        {
            get { return _bCustomizeToolBar; }
            set { _bCustomizeToolBar = value; }
        }

        #endregion


        #region "Help"

        public Boolean Search
        {
            get { return _bSearch; }
            set { _bSearch = value; }
        }

        public Boolean HowDoI
        {
            get { return _bHowDoI; }
            set { _bHowDoI = value; }
        }

        public Boolean Contents
        {
            get { return _bContents; }
            set { _bContents = value; }
        }

        public Boolean TSupport
        {
            get { return _bTSupport; }
            set { _bTSupport = value; }
        }

        public Boolean About
        {
            get { return _bAbout; }
            set { _bAbout = value; }
        }

        #endregion


        #region "DashBoard"
        
        public Boolean Calender
        {
            get { return _bCalender; }
            set { _bCalender = value; }
        }

        public Boolean ScanPatient
        {
            get { return _bScanPatient; }
            set { _bScanPatient = value; }
        }

        public Boolean Remittance
        {
            get { return _bRemittance; }
            set { _bRemittance = value; }
        }

        public Boolean Advance
        {
            get { return _bAdvance; }
            set { _bAdvance = value; }
        }

        public Boolean Ledger
        {
            get { return _bLedger; }
            set { _bLedger = value; }
        }

        public Boolean Balance
        {
            get { return _bBalance; }
            set { _bBalance = value; }
        }

        #endregion

        #endregion


        #region "Fill Methods"

        public void CheckForUserRights(String UName)
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            DataTable dt_UserRights = null;
            try
            {

                oDBParameters.Add("@UserName", UName, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("gsp_RetrieveUserRights", oDBParameters, out dt_UserRights);

                ArrayList arrLstUserRights = new ArrayList();

                if (dt_UserRights != null)
                {
                    if (dt_UserRights.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt_UserRights.Rows.Count -1; i++)
                        {
                            arrLstUserRights.Add(dt_UserRights.Rows[i]["RightsName"]);

                        }
                    }

                }


                #region "Billing Configuration"

                if (arrLstUserRights.Contains("ICD9"))
               _bICD9 = true;
               else
               _bICD9 = false;

               if (arrLstUserRights.Contains("CPT"))
                   _bCPT = true;
               else
                   _bCPT = false;

               if (arrLstUserRights.Contains("Drugs"))
                   _bDrugs = true;
               else
                   _bDrugs = false;

               if (arrLstUserRights.Contains("Type of Service"))
                   _bTOS = true;
               else
                   _bTOS = false;

               if (arrLstUserRights.Contains("Place of Service"))
                   _bPOS = true;
               else
                   _bPOS = false;

               if (arrLstUserRights.Contains("Category"))
                   _bCategory = true;
               else
                   _bCategory = false;

               if (arrLstUserRights.Contains("Calendar"))
                   _bCalender = true;
               else
                   _bCalender = false;

               if (arrLstUserRights.Contains("Facility"))
                   _bFacility = true;
               else
                   _bFacility = false;

               if (arrLstUserRights.Contains("Smart Treatment"))
                   _bsTreatment = true;
               else
                   _bsTreatment = false;

               if (arrLstUserRights.Contains("Code Type"))
                   _bCType = true;
               else
                   _bCType = false;

               if (arrLstUserRights.Contains("Patient RelationShip"))
                   _bPatRelationship = true;
               else
                   _bPatRelationship = false;

               if (arrLstUserRights.Contains("Flag Type"))
                   _bFlagType = true;
               else
                   _bFlagType = false;


               if (arrLstUserRights.Contains("Speciality"))
                   _bSpecialty = true;
               else
                   _bSpecialty = false;


               if (arrLstUserRights.Contains("Insurance Service Type"))
                   _bInsServiceType = true;
               else
                   _bInsServiceType = false;

               if (arrLstUserRights.Contains("Insurance Plan Codes"))
                   _bInsPlanCodes = true;
               else
                   _bInsPlanCodes = false;

               if (arrLstUserRights.Contains("Insurance Type"))
                   _bInsType = true;
               else
                   _bInsType = false;

               if (arrLstUserRights.Contains("Adjustment Type"))
                   _bAdjType = true;
               else
                   _bAdjType = false;

               if (arrLstUserRights.Contains("Fee Schedule"))
                   _bFSchedule = true;
               else
                   _bFSchedule = false;

               if (arrLstUserRights.Contains("Fee Schedule Allocation"))
                   _bFScheduleAllocation = true;
               else
                   _bFScheduleAllocation = false;

               if (arrLstUserRights.Contains("Credit Card Type"))
                   _bCCardType = true;
               else
                   _bCCardType = false;

               if (arrLstUserRights.Contains("Scrubber"))
                   _bScrubber = true;
               else
                   _bScrubber = false;

               if (arrLstUserRights.Contains("Referral CPT"))
                   _bRefCPT = true;
               else
                   _bRefCPT = false;

               if (arrLstUserRights.Contains("Claim Scrubber"))
                   _bCScrubber = true;
               else
                   _bCScrubber = false;

               if (arrLstUserRights.Contains("Claim Scrubber"))
                   _bCScrubber = true;
               else
                   _bCScrubber = false;

               if (arrLstUserRights.Contains("InValid ICD9"))
                   _bInValidICD9 = true;
               else
                   _bInValidICD9 = false;

                #endregion

               #region "Appointment Configuration"

               if (arrLstUserRights.Contains("Resource"))
                   _bResource = true;
               else
                   _bResource = false;

               if (arrLstUserRights.Contains("Appointment Block Type"))
                   _bABType = true;
               else
                   _bABType = false;


               if (arrLstUserRights.Contains("Appointment Type"))
                   _bAType = true;
               else
                   _bAType = false;

               if (arrLstUserRights.Contains("Problem Type"))
                   _bPType = true;
               else
                   _bPType = false;

               if (arrLstUserRights.Contains("Appointment Status"))
                   _bAStatus = true;
               else
                   _bAStatus = false;

               if (arrLstUserRights.Contains("Location"))
                   _bLocation = true;
               else
                   _bLocation = false;

               if (arrLstUserRights.Contains("Department"))
                   _bDepartment = true;
               else
                   _bDepartment = false;

               if (arrLstUserRights.Contains("Template"))
                   _bTemplate = true;
               else
                   _bTemplate = false;

               if (arrLstUserRights.Contains("Provider Appointment Type Association"))
                   _bProviderATS = true;
               else
                   _bProviderATS = false;

               if (arrLstUserRights.Contains("Follow Up"))
                   _bFollowup = true;
               else
                   _bFollowup = false;




               #endregion

               #region "Task mail Configuration"

               if (arrLstUserRights.Contains("Follow Up"))
                   _bTaskFollowup = true;
               else
                   _bTaskFollowup = false;


               if (arrLstUserRights.Contains("Status Type"))
                   _bSType = true;
               else
                   _bSType = false;

               if (arrLstUserRights.Contains("Priority Type"))
                   _bPriorType = true;
               else
                   _bPriorType = false;

               if (arrLstUserRights.Contains("Signature"))
                   _bSignature = true;
               else
                   _bSignature = false;


               #endregion

               if (arrLstUserRights.Contains("Template Gallery"))
                   _bTemplateGallery = true;
               else
                   _bTemplateGallery = false;

               if (arrLstUserRights.Contains("Template Association"))
                   _bTemplateGallery = true;
               else
                   _bTemplateGallery = false;


               #region "Go"

               if (arrLstUserRights.Contains("New Patient"))
                   _bNewPatient = true;
               else
                   _bNewPatient = false;

               if (arrLstUserRights.Contains("Modify Patient"))
                   _bModifyPatient = true;
               else
                   _bModifyPatient = false;

               if (arrLstUserRights.Contains("Card Scanning"))
                   _bCardScanning = true;
               else
                   _bCardScanning = false;

               if (arrLstUserRights.Contains("Appointment"))
                   _bAppointment = true;
               else
                   _bAppointment = false;

               if (arrLstUserRights.Contains("Schedule"))
                   _bSchedule = true;
               else
                   _bSchedule = false;

               if (arrLstUserRights.Contains("Charges"))
                   _bCharges = true;
               else
                   _bCharges = false;

               if (arrLstUserRights.Contains("Batch"))
                   _bBatch = true;
               else
                   _bBatch = false;

               if (arrLstUserRights.Contains("Payment"))
                   _bPayment = true;
               else
                   _bPayment = false;


               #endregion

               #region "View"

               if (arrLstUserRights.Contains("Patient Forms"))
                   _bPatientForms = true;
               else
                   _bPatientForms = false;

               if (arrLstUserRights.Contains("Tasks"))
                   _bTasks = true;
               else
                   _bTasks = false;

               #endregion

               #region "Reports"

               if (arrLstUserRights.Contains("Patient Payment History"))
                   _bPatPayHistory = true;
               else
                   _bPatPayHistory = false;


               if (arrLstUserRights.Contains("Patient Transaction History"))
                   _bPatTransHistory = true;
               else
                   _bPatTransHistory = false;


               if (arrLstUserRights.Contains("Patient Statement"))
                   _bPatStatement = true;
               else
                   _bPatStatement = false;

               if (arrLstUserRights.Contains("Production by Doctor"))
                   _bProdByDoctor = true;
               else
                   _bProdByDoctor = false;

               if (arrLstUserRights.Contains("Production by Facility"))
                   _bProdByFacility = true;
               else
                   _bProdByFacility = false;

               if (arrLstUserRights.Contains("Production by Date"))
                   _bProdByDate = true;
               else
                   _bProdByDate = false;

               if (arrLstUserRights.Contains("Production by Month"))
                   _bProdByMonth = true;
               else
                   _bProdByMonth = false;

               if (arrLstUserRights.Contains("Production by Insurance Carrier"))
                   _bProdByInsCarrier = true;
               else
                   _bProdByInsCarrier = false;

               if (arrLstUserRights.Contains("Production by Procedure Group"))
                   _bProdByProcGroup = true;
               else
                   _bProdByProcGroup = false;

               if (arrLstUserRights.Contains("Production by Procedure Code"))
                   _bProdByProcCode = true;
               else
                   _bProdByProcCode = false;

               if (arrLstUserRights.Contains("Production by Facility by Patient – Summary"))
                   _bProdByFacilityByPatSummary = true;
               else
                   _bProdByFacilityByPatSummary = false;

               if (arrLstUserRights.Contains("Production by Facility by Patient – Detail"))
                   _bProdByFacilityByPatDetail = true;
               else
                   _bProdByFacilityByPatDetail = false;

               if (arrLstUserRights.Contains("Reimbursement by Month"))
                   _bReimbByMonth = true;
               else
                   _bReimbByMonth = false;

               if (arrLstUserRights.Contains("Reimbursement by Month by Account"))
                   _bReimbByMonthByAcc = true;
               else
                   _bReimbByMonthByAcc = false;

               if (arrLstUserRights.Contains("Reimbursement by Insurance Carrier"))
                   _bReimbByInsCarrier = true;
               else
                   _bReimbByInsCarrier = false;

               if (arrLstUserRights.Contains("Reimbursement by Insurance Carrier by CPT Code"))
                   _bReimbByInsCarrierByCPT = true;
               else
                   _bReimbByInsCarrierByCPT = false;

               if (arrLstUserRights.Contains("Reimbursement by CPT Code by Insurance Carrier"))
                   _bReimbByCPTByInsCarrier = true;
               else
                   _bReimbByCPTByInsCarrier = false;

               if (arrLstUserRights.Contains("Reimbursement by Doctor by Insurance Carrier"))
                   _bReimbByDocByInsCarrier = true;
               else
                   _bReimbByDocByInsCarrier = false;

               if (arrLstUserRights.Contains("Reimbursement by Insurance Carrier for CPT Code"))
                   _bReimbByInsCarrierForCPT = true;
               else
                   _bReimbByInsCarrierForCPT = false;

               if (arrLstUserRights.Contains("Reimbursement Details by Account"))
                   _bReimbDetailsByAcc = true;
               else
                   _bReimbDetailsByAcc = false;

               if (arrLstUserRights.Contains("Aging Analysis"))
                   _bASumary = true;
               else
                   _bASumary = false;

               if (arrLstUserRights.Contains("Aging Summary by Patient"))
                   _bASumaryByPat = true;
               else
                   _bASumaryByPat = false;

               if (arrLstUserRights.Contains("Aging Summary by Insurance Carrier"))
                   _bASumaryByInsCarrier = true;
               else
                   _bASumaryByInsCarrier = false;

               if (arrLstUserRights.Contains("Production by Physician Group"))
                   _bProdByPhysicianGroup = true;
               else
                   _bProdByPhysicianGroup = false;

               if (arrLstUserRights.Contains("Production Analysis by Facility"))
                   _bProdAnalysisByFacility = true;
               else
                   _bProdAnalysisByFacility = false;

               if (arrLstUserRights.Contains("Production Analysis by Procedure Group"))
                   _bProdAnalysisByPG = true;
               else
                   _bProdAnalysisByPG = false;

               if (arrLstUserRights.Contains("Production Analysis and Trends by Month"))
                   _bProdAnalysisByTrendsByMonth = true;
               else
                   _bProdAnalysisByTrendsByMonth = false;

               if (arrLstUserRights.Contains("Production Trends by Procedure Group"))
                   _bProdTrendsByProcGroup = true;
               else
                   _bProdTrendsByProcGroup = false;

               if (arrLstUserRights.Contains("Appointments"))
                   _bAppointments = true;
               else
                   _bAppointments = false;

            

               if (arrLstUserRights.Contains("Cancel Appointments"))
                   _bCancelAppointments = true;
               else
                   _bCancelAppointments = false;

               if (arrLstUserRights.Contains("Batch Print Templates"))
                   _bBatchPrintTemp = true;
               else
                   _bBatchPrintTemp = false;

               if (arrLstUserRights.Contains("Missing Charges"))
                   _bMissingCharges = true;
               else
                   _bMissingCharges = false;

               if (arrLstUserRights.Contains("Charge Summary Report"))
                   _bChargesSummaryReport = true;
               else
                   _bChargesSummaryReport = false;

               if (arrLstUserRights.Contains("New Patient vs. Established Patient"))
                   _bNewPatvsEstPat = true;
               else
                   _bNewPatvsEstPat = false;


               if (arrLstUserRights.Contains("Refund"))
                   _bRefund = true;
               else
                   _bRefund = false;


                             
               if (arrLstUserRights.Contains("Patient Balance"))
                   _bPatBalance = true;
               else
                   _bPatBalance = false;

               if (arrLstUserRights.Contains("Transaction History"))
                   _bTransHistory = true;
               else
                   _bTransHistory = false;

               if (arrLstUserRights.Contains("Transaction Notes"))
                   _bTransNotes = true;
               else
                   _bTransNotes = false;


               if (arrLstUserRights.Contains("Transaction History Analysis"))
                   _bTransHistoryAnalysis = true;
               else
                   _bTransHistoryAnalysis = false;

               if (arrLstUserRights.Contains("Patient Report"))
                   _bPatReport = true;
               else
                   _bPatReport = false;

               if (arrLstUserRights.Contains("Provider/Referrals – Patients"))
                   _bProvRefPat = true;
               else
                   _bProvRefPat = false;

               if (arrLstUserRights.Contains("Cash Flow Report"))
                   _bCashFlowReport = true;
               else
                   _bCashFlowReport = false;

               if (arrLstUserRights.Contains("Patient Recall"))
                   _bPatRecall = true;
               else
                   _bPatRecall = false;


               if (arrLstUserRights.Contains("Print List"))
                   _bPrintList = true;
               else
                   _bPrintList = false;

               if (arrLstUserRights.Contains("Gateway EDI"))
                   _bGatewayEDI = true;
               else
                   _bGatewayEDI = false;

               if (arrLstUserRights.Contains("Audit Trial"))
                   _bAuditTrial = true;
               else
                   _bAuditTrial = false;


               #endregion

               #region "Tools"

               if (arrLstUserRights.Contains("Import Templates"))
                   _bImportTemplates = true;
               else
                   _bImportTemplates = false;


               if (arrLstUserRights.Contains("Export Templates"))
                   _bExportTemplates = true;
               else
                   _bExportTemplates = false;

               if (arrLstUserRights.Contains("Update Templates"))
                   _bUpdateTemplates = true;
               else
                   _bUpdateTemplates = false;

               if (arrLstUserRights.Contains("Card Image"))
                   _bCardImage = true;
               else
                   _bCardImage = false;

               if (arrLstUserRights.Contains("Merge Patient"))
                   _bMergePatient = true;
               else
                   _bMergePatient = false;


               #endregion

               #region "Settings"

               if (arrLstUserRights.Contains("Restore Dashboard Settings"))
                   _bRestoreDashBoard = true;
               else
                   _bRestoreDashBoard = false;


               if (arrLstUserRights.Contains("System Setting"))
                   _bSystemSetting = true;
               else
                   _bSystemSetting = false;

               if (arrLstUserRights.Contains("Card Scanner"))
                   _bCardScanner = true;
               else
                   _bCardScanner = false;

               if (arrLstUserRights.Contains("Office 2003 Theme"))
                   _bOffice2003Theme = true;
               else
                   _bOffice2003Theme = false;

               if (arrLstUserRights.Contains("Office 2003 Dark Theme"))
                   _bOffice2003DarkTheme = true;
               else
                   _bOffice2003DarkTheme = false;

               //if (arrLstUserRights.Contains("Office 2007 Theme"))
               //   // _bOffice2007Theme = true;
               //else
               //  //  _bOffice2007Theme = false;

               if (arrLstUserRights.Contains("Customize Tool Bar"))
                   _bCustomizeToolBar = true;
               else
                   _bCustomizeToolBar = false;


               #endregion

               #region "Help"

               if (arrLstUserRights.Contains("How Do I"))
                   _bHowDoI = true;
               else
                   _bHowDoI = false;

               if (arrLstUserRights.Contains("Search"))
                   _bSearch = true;
               else
                   _bSearch = false;

               if (arrLstUserRights.Contains("Contents"))
                   _bContents = true;
               else
                   _bContents = false;

               if (arrLstUserRights.Contains("Technical Support"))
                   _bTSupport = true;
               else
                   _bTSupport = false;
                
               if (arrLstUserRights.Contains("About gloPM 2009"))
                   _bAbout = true;
               else
                   _bAbout = false;

               #endregion

               #region "DashBoard"

               if (arrLstUserRights.Contains("Scan Patient"))
                   _bScanPatient = true;
               else
                   _bScanPatient = false;

               if (arrLstUserRights.Contains("Remittance"))
                   _bRemittance = true;
               else
                   _bRemittance = false;

               if (arrLstUserRights.Contains("Advance"))
                   _bAdvance = true;
               else
                   _bAdvance = false;

               if (arrLstUserRights.Contains("Balance"))
                   _bBalance = true;
               else
                   _bBalance = false;

               if (arrLstUserRights.Contains("Ledger"))
                   _bLedger = true;
               else
                   _bLedger = false;


               #endregion









           }
            catch (gloDatabaseLayer.DBException dbEx)
            {

                dbEx.ERROR_Log(dbEx.ToString());
               

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
               
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                    oDBParameters = null;
                }
            }
        }

        #endregion


    }
}
