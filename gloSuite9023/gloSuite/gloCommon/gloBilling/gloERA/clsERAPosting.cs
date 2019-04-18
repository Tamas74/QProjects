using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Edidev.FrameworkEDI;
using System.Windows.Forms;
using System.Collections;
using System.Data;

using gloSettings;
using gloBilling.Payment;
using gloBilling.EOBPayment.Common;
using gloAuditTrail;
//using gloPatientStripControl;

namespace gloBilling.gloERA
{

    #region " Enumerations "

    public enum SaveType
    {
        TemporarySave = 1,
        OriginalSave = 0
    }

    public enum StopFlag
    {
        NotProcessed = 0,
        Passed = 1,
        NoClaimProcessed = 2,
        Error = 3,
        CheckOpened = 4
    }

    public enum LogStage
    {
        Check = 1,
        Claim = 2,
        ServiceLine = 3
    }

    public enum AdjustmentType
    {
        CoInsurance = 1,
        Copay = 2,
        Deductable = 3,
        PrevPaid = 4,
        WithHold = 5,
        WriteOff = 6,
        OtherAdjustment = 7,
        None = 99
    }

    #endregion


    public class clsERAPosting
    {

        #region " Constructor & Destructor "

        private bool disposed = false;

        public clsERAPosting()
        {
            #region " Get ClinicID from AppSettings "
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }
            #endregion

            #region " Get DatabaseConnectionString from AppSettings "
            if (appSettings["DataBaseConnectionString"] != null)
            {
                if (appSettings["DataBaseConnectionString"] != "")
                { _DataBaseConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]); }
            }
            #endregion

            #region " Get UserID from AppSettings "
            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                { _UserID = Convert.ToInt64(appSettings["UserID"]); }
                else { _UserID = 0; }
            }
            else
            { _UserID = 0; }
            #endregion

            #region " Get MessageBoxCaption from AppSettings "
            if (appSettings["MessageBoxCaption"] != null)
            {
                if (appSettings["MessageBoxCaption"] != "")
                { _MessageBoxCaption = Convert.ToString(appSettings["MessageBoxCaption"]); }
                else
                    _MessageBoxCaption = "gloPM";
            }
            else
                _MessageBoxCaption = "gloPM";
            #endregion

        }

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
                    if (oDB != null) { oDB.Dispose(); oDB = null; }
                    if (oDBPara != null) { oDBPara.Dispose(); oDBPara = null; }



                }
            }
            disposed = true;
        }

        ~clsERAPosting()
        {
            Dispose(false);
        }

        #endregion

        #region " Variable Declarations "

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        public string _DataBaseConnectionString;
        private Int64 _ClinicID = 1;
        private Int64 _UserID;
        private string _MessageBoxCaption;

        private gloDatabaseLayer.DBLayer oDB;
        private gloDatabaseLayer.DBParameters oDBPara;
        string _TempStr;

        EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines EOBInsurancePaymentMasterLines = null;
        EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines EOBInsurancePaymentMasterAllocationLines = null;

        // Grid constant defined to use in for loop 
        const int COL_WRITEOFF = 23;
        const int COL_COPAY = 24;
        const int COL_DEDUCTIBLE = 25;
        const int COL_COINSURANCE = 26;
        const int COL_WITHHOLD = 27;
        const string _paymentPrefix = "GPM#";

        string _REASONCODE_WRITEOFF = "";
        string _REASONCODE_COPAY = "";
        string _REASONCODE_DEDUCTIBLE = "";
        string _REASONCODE_COINSURANCE = "";
        string _REASONCODE_WITHHOLD = "";
        int nResponsibilityNo = 0;
        #endregion

        #region " Properties for Payment Tray "
        private bool _IsReserveUsed = false;
        private Int64 _SelectedTrayID = 0;
        private string _SelectedTrayName = "";
        private string _SelectedTrayCode = "";
        private Int64 _EOBPaymentID = 0;
        private Int64 _PatientID = 0;
        private Int64 _CloseDate = 0;
        private Int64 _InsuranceCompanyID = 0;
        private string _PaymentNumber = string.Empty;
        private string _SelectedInsuranceCompany = string.Empty;
        private string _CheckNumber = string.Empty;
        private EOBPaymentMode _PaymentMode = EOBPaymentMode.Check;//solving sales force case -GLO2011-0010771
        private string _CheckDate = string.Empty;
        private decimal _CheckAmount = 0;
        private decimal _PLBAmount = 0;

        private decimal _RunningCheckAmount = 0;
        private Int64 _MSTAccountID = 0;
        private string _NextAction = string.Empty;
        private string _NextActionContactID = string.Empty;
        private string _NextActionPatientID = string.Empty;
        private string _NextParty = string.Empty;

        private Int64 _BillingTransactionID = 0;
        private Int64 _BillingTransactionDetailID = 0;
        private Int64 _BillingTransactionLineNo = 0;
        private Int64 _TrackTrnID = 0;
        private Int64 _TrackTrnDtlID = 0;
        private Int64 _ClaimNo = 0;
        private string _ERAClaimNo = string.Empty;
        private Int64 _SVCId = 0;
        private string _SubclaimNo = string.Empty;
        private string _DOSFrom = string.Empty;
        private string _DOSTo = string.Empty;
        private string _CPT = string.Empty;
        private string _CPTDescription = string.Empty;
        private Int64 _ClaimStatus = 0;
        private string _Charges = string.Empty;
        private string _Unit = string.Empty;
        private string _TotalCharges = string.Empty;
        private string _Allowed = string.Empty;
        private string _WriteOff = string.Empty;
        private string _Payment = string.Empty;
        private string _Copay = string.Empty;
        private string _Deductible = string.Empty;
        private string _CoInsurance = string.Empty;
        private string _WithHold = string.Empty;
        private string _ClaimRemittanceReferenceNo = string.Empty;
        private bool _IsPaymentAllocated = false;
        private Int64 _ContactInsuranceID = 0;
        private Int64 _PatientInsuranceID = 0;


        private string _PreTotalInsPaid = string.Empty;
        private string _PreTotalWriteOff = string.Empty;
        private string _PreTotalWithhold = string.Empty;
        private string _PreTotalPatPaid = string.Empty;
        private string _PreTotalPatAdjustment = string.Empty;

        string sGeneratedOtherReasonCode = string.Empty;


        private bool IsReserveUsed
        {
            get { return _IsReserveUsed; }
            set
            {
                _IsReserveUsed = value;
            }
        }

        private Int64 SelectedPaymentTrayID
        {
            get { return _SelectedTrayID; }
            set
            {
                _SelectedTrayID = value;
            }
        }

        private string SelectedPaymentTray
        {
            get { return _SelectedTrayName; }
            set
            {
                _SelectedTrayName = value;
            }
        }

        private string SelectedPaymentTrayCode
        {
            get { return _SelectedTrayCode; }
            set
            {
                _SelectedTrayCode = value;
            }
        }

        private Int64 EOBPaymentID
        {
            get { return _EOBPaymentID; }
            set { _EOBPaymentID = value; }
        }

        private Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }

        private Int64 CloseDate
        {
            get { return _CloseDate; }
            set { _CloseDate = value; }
        }

        private Int64 SelectedInsuranceCompanyID
        {
            get { return _InsuranceCompanyID; }
            set
            {
                _InsuranceCompanyID = value;
            }
        }

        private string PaymentNumber
        {
            get { return _PaymentNumber; }
            set
            {
                _PaymentNumber = value;
            }
        }


        private string SelectedInsuranceCompany
        {
            get { return _SelectedInsuranceCompany; }
            set
            {
                _SelectedInsuranceCompany = value;
            }
        }

        private string CheckNumber
        {
            get { return _CheckNumber; }
            set
            {
                _CheckNumber = value;
            }
        }
        private EOBPaymentMode PaymentMode   //solving sales force case -GLO2011-0010771
        {
            get { return _PaymentMode; }
            set
            {
                _PaymentMode = value;
            }
        }
        private string CheckDate
        {
            get { return _CheckDate; }
            set
            {
                _CheckDate = value;
            }
        }

        private decimal CheckAmount
        {
            get { return _CheckAmount; }
            set
            {
                _CheckAmount = value;
            }
        }

        private decimal PLBAmount
        {
            get { return _PLBAmount; }
            set
            {
                _PLBAmount = value;
            }
        }
        private decimal RunningCheckAmount
        {
            get { return _RunningCheckAmount; }
            set
            {
                _RunningCheckAmount = value;
            }
        }

        private Int64 MSTAccountID
        {
            get { return _MSTAccountID; }
            set
            {
                _MSTAccountID = value;
            }
        }

        public string NextActionContactID
        {
            get { return _NextActionContactID; }
            set
            {
                _NextActionContactID = value;
            }
        }
        public string NextActionPatientID
        {
            get { return _NextActionPatientID; }
            set
            {
                _NextActionPatientID = value;
            }
        }

        public string NextAction
        {
            get { return _NextAction; }
            set
            {
                _NextAction = value;
            }
        }

        public string NextParty
        {
            get { return _NextParty; }
            set
            {
                _NextParty = value;
            }
        }

        private bool IsPaymentInProcess
        {
            get
            {
                if (_EOBPaymentID > 0)
                { return true; }
                else { return false; }
            }
        }



        private Int64 BillingTransactionID
        {
            get { return _BillingTransactionID; }
            set
            {
                _BillingTransactionID = value;
            }
        }

        private Int64 BillingTransactionDetailID
        {
            get { return _BillingTransactionDetailID; }
            set
            {
                _BillingTransactionDetailID = value;
            }
        }

        private Int64 BillingTransactionLineNo
        {
            get { return _BillingTransactionLineNo; }
            set
            {
                _BillingTransactionLineNo = value;
            }
        }

        private Int64 TrackTrnID
        {
            get { return _TrackTrnID; }
            set
            {
                _TrackTrnID = value;
            }
        }

        private Int64 TrackTrnDtlID
        {
            get { return _TrackTrnDtlID; }
            set
            {
                _TrackTrnDtlID = value;
            }
        }

        private Int64 ClaimNo
        {
            get { return _ClaimNo; }
            set
            {
                _ClaimNo = value;
            }
        }

        private string ERAClaimNo
        {
            get { return _ERAClaimNo; }
            set
            {
                _ERAClaimNo = value;
            }
        }

        private Int64 SVCId
        {
            get { return _SVCId; }
            set
            {
                _SVCId = value;
            }
        }
        private string SubclaimNo
        {
            get { return _SubclaimNo; }
            set
            {
                _SubclaimNo = value;
            }
        }

        private string DOSFrom
        {
            get { return _DOSFrom; }
            set
            {
                _DOSFrom = value;
            }
        }
        private string DOSTo
        {
            get { return _DOSTo; }
            set
            {
                _DOSTo = value;
            }
        }
        private string CPT
        {
            get { return _CPT; }
            set
            {
                _CPT = value;
            }
        }
        private string CPTDescription
        {
            get { return _CPTDescription; }
            set
            {
                _CPTDescription = value;
            }
        }
        private Int64 ClaimStatus
        {
            get { return _ClaimStatus; }
            set
            {
                _ClaimStatus = value;
            }
        }
        private string Charges
        {
            get { return _Charges; }
            set
            {
                _Charges = value;
            }
        }
        private string Unit
        {
            get { return _Unit; }
            set
            {
                _Unit = value;
            }
        }
        private string TotalCharges
        {
            get { return _TotalCharges; }
            set
            {
                _TotalCharges = value;
            }
        }
        private string Allowed
        {
            get { return _Allowed; }
            set
            {
                _Allowed = value;
            }
        }
        private string WriteOff
        {
            get { return _WriteOff; }
            set
            {
                _WriteOff = value;
            }
        }
        private string Payment
        {
            get { return _Payment; }
            set
            {
                _Payment = value;
            }
        }
        private string Copay
        {
            get { return _Copay; }
            set
            {
                _Copay = value;
            }
        }
        private string Deductible
        {
            get { return _Deductible; }
            set
            {
                _Deductible = value;
            }
        }
        private string CoInsurance
        {
            get { return _CoInsurance; }
            set
            {
                _CoInsurance = value;
            }
        }
        private string WithHold
        {
            get { return _WithHold; }
            set
            {
                _WithHold = value;
            }
        }
        private string ClaimRemittanceReferenceNo
        {
            get { return _ClaimRemittanceReferenceNo; }
            set
            {
                _ClaimRemittanceReferenceNo = value;
            }
        }
        private bool IsPaymentAllocated
        {
            get { return _IsPaymentAllocated; }
            set
            {

                _IsPaymentAllocated = value;
            }
        }
        private Int64 ContactInsuranceID
        {
            get { return _ContactInsuranceID; }
            set
            {

                _ContactInsuranceID = value;
            }
        }
        private Int64 PatientInsuranceID
        {
            get { return _PatientInsuranceID; }
            set
            {

                _PatientInsuranceID = value;
            }
        }
        private string _OtherReasonAmount = string.Empty;
        private string OtherReasonAmount
        {
            get { return _OtherReasonAmount; }
            set
            {

                _OtherReasonAmount = value;
            }
        }


        private string PreTotalInsPaid
        {
            get { return _PreTotalInsPaid; }
            set
            {
                _PreTotalInsPaid = value;
            }
        }

        private string PreTotalWriteOff
        {
            get { return _PreTotalWriteOff; }
            set
            {
                _PreTotalWriteOff = value;
            }
        }


        private string PreTotalWithhold
        {
            get { return _PreTotalWithhold; }
            set
            {
                _PreTotalWithhold = value;
            }
        }

        private string PreTotalPatPaid
        {
            get { return _PreTotalPatPaid; }
            set
            {
                _PreTotalPatPaid = value;
            }
        }


        private string PreTotalPatAdjustment
        {
            get { return _PreTotalPatAdjustment; }
            set
            {
                _PreTotalPatAdjustment = value;
            }
        }

        private bool _ZeroPaidBilled = false;
        private bool _ZeroPaidNotBilled = false;
        private bool _PaidNotZero = false;

        private bool ZeroPaidBilled
        {
            get { return _ZeroPaidBilled; }
            set
            {
                _ZeroPaidBilled = value;
            }
        }
        private bool ZeroPaidNotBilled
        {
            get { return _ZeroPaidNotBilled; }
            set
            {
                _ZeroPaidNotBilled = value;
            }
        }
        private bool PaidNotZero
        {
            get { return _PaidNotZero; }
            set
            {
                _PaidNotZero = value;
            }
        }

        #endregion

        #region " Private & Public Methods "

        private void RefreshProgress(ref ProgressBar oProgress, ref Label oLabel, String sProgressText)
        {
            oProgress.Increment(1);
            oLabel.Text = sProgressText;
            Application.DoEvents();
            //oProgress.Refresh();
            //oLabel.Refresh();
            //oProgress.Parent.Refresh();
            //oProgress.Parent.Invalidate();
            //oProgress.Parent.Update();

            //foreach (Form oForm in Application.OpenForms)
            //{
            //    if (oForm.Name == "frmDashBoardMain" || oForm.Name == "frmERAPayment" || oForm.Name == "frmERAFiles")
            //    {
            //        oForm.Invalidate();
            //        oForm.Refresh();
            //        oForm.Update();
            //    }
            //}
        }

        #region " Temporary ERA Posting. "

        public bool PostERAFile_Temp(Int64 nBPRID, out string sMessage, out StopFlag oStopFlag, ref ProgressBar oProgress, ref Label oLabel)
        
        {
            EOBInsurancePaymentMasterLines = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines();
            EOBInsurancePaymentMasterAllocationLines = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines();

            int _financeLineNo = 0;
            Int64 nRetValue = 0;

            bool IsStopPost = false;
            bool bReturn = false;
            //bool bCheckRunningFlag = false;
            //bool IsReserveUsed = false;

            DataTable dtBPRClaims = null;
            DataTable dtCheckDetails = null;
            DataTable dtClaimDetails = null;

            List<ERAPayment> oPaymentList = new List<ERAPayment>();

            EOBPayment.Common.PaymentInsurance oPaymentInsurace = null;  // Main Payment Master Object
            EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsPaymentCreditDetail = null;  // Main Credit Line Entry Object
            EOBPayment.Common.PaymentInsuranceClaim oPaymentInsuranceClaim = null;
            EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsurancePaymentReserveDetail = null;
            
            SplitClaimDetails oSplitClaimDetails = new SplitClaimDetails();
            System.Data.SqlClient.SqlTransaction _sqlTransaction = null;

            // Custom Error code & Custom Error Desc 
            sMessage = string.Empty;
            oStopFlag = StopFlag.NotProcessed; //"0"
            gloBilling ogloBilling = new gloBilling(AppSettings.ConnectionStringPM, AppSettings.ConnectionStringEMR);
              
            try
            {

                #region " IsCheckInProcess "

                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                    ActivityType.IsCheckInProcess, String.Format("IsCheckInProcess - Start (BPRID: {0})", nBPRID), ActivityOutCome.Success);

                // Validate check status In-Process or Not.
                if (ClsERAValidation.IsCheckInProcess(nBPRID, out sMessage))
                {
                    // Display message to the user.
                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                    ActivityType.IsCheckInProcess, sMessage, ActivityOutCome.Failure);
                    oStopFlag = StopFlag.CheckOpened;
                    return false;
                }

                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                    ActivityType.IsCheckInProcess, "IsCheckInProcess - End ", ActivityOutCome.Success);

                #endregion

                #region " PreSaveValidation "

                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                    ActivityType.PreSaveValidation, "PreSaveValidation - Start", ActivityOutCome.Success);

                nRetValue = PreSaveValidation(nBPRID, out sMessage);

                if (nRetValue > 0)
                {
                    // Display Message to the user.
                    // Stop Payment for the Check.

                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                    ActivityType.PreSaveValidation, sMessage, ActivityOutCome.Failure);
                    oStopFlag = StopFlag.NoClaimProcessed;
                    return false;
                }

                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                    ActivityType.PreSaveValidation, "PreSaveValidation - End ", ActivityOutCome.Success);

                #endregion

                #region " GetAllBPRClaims "

                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                    ActivityType.GetAllBPRClaims, "GetAllBPRClaims - Start", ActivityOutCome.Success);


                // Fetch all claim against a BPR Value.
                dtBPRClaims = GetAllBPRClaims(nBPRID);

                // Get ClaimStatus from Datatable (dtBPRClaims)
                if (dtBPRClaims == null)
                {
                    // BPR Claims not found.
                    sMessage = String.Format("Claims not found for the nBPRID: {0}", nBPRID);
                    //ClsERASave.ClaimStatusLog(nBPRID, 0, "", 0, "NP", sMessage, LogStage.Check);
                    //ClsERASave.SVCExceptionLog(nBPRID, 0, "", 0, "NP", "", LogStage.ServiceLine);

                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                    ActivityType.GetAllBPRClaims, sMessage, ActivityOutCome.Failure);

                    return false;
                }

                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                    ActivityType.GetAllBPRClaims, "GetAllBPRClaims - End", ActivityOutCome.Success);

                #endregion

                #region " FillPaymentTray "
                // Set payment tray information.
                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                            ActivityType.FillMasterDetails, "FillPaymentTray - Start", ActivityOutCome.Success);
                FillPaymentTray();

                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                            ActivityType.FillMasterDetails, "FillPaymentTray - End", ActivityOutCome.Success);

                #endregion

                GetPayerSetupDetails(nBPRID);
                SetReasonCodes();

                //bool bChkDupConfirm = false;

                //bool bRunningCalculation = false;
                bool bCommentFlag = false;
                Int64 nContactID = 0;
                Int64 nClaimStatus = 0;
                Int64 nCLPId = 0;
                bool bClaimLevelCASExists;
                string sClaim_CLP03 = "";
                string sClaim_CLP04 = "";
                Int64 nInsuranceId = 0;
                EOBPayment.Common.PaymentInsuranceLineNextActions oListNextActions = null;
                ERAPayment oPayment = null;
                bool bCASOtherReasonCodeExists = false;
                bool bPayerCASWindowSetup = false;
                oProgress.Maximum = dtBPRClaims.Rows.Count + 2;
                oProgress.Minimum = 0;
                oProgress.Value = 0;
                oLabel.Text = "";
                oProgress.Visible = true;
                oLabel.Visible = true;
                foreach (DataRow dtClaimRow in dtBPRClaims.Rows)
                {
                    RefreshProgress(ref oProgress, ref oLabel, "Processing Claim " + dtClaimRow["sCLP01_ClaimSubmitterID"].ToString());

                    #region " For Each Claim "
                    IsStopPost = false;
                    bCommentFlag = false;
                    nContactID = 0;  // InsurancePlanID (return from DeterminePayer)
                    //Int64 nClaimNo = Convert.ToInt64(dtClaimRow["sCLP01_ClaimSubmitterID"]);
                    nResponsibilityNo = 0;
                    nClaimStatus = Convert.ToInt64(dtClaimRow["sCLP02_ClaimStatusCode"]);
                    nCLPId = Convert.ToInt64(dtClaimRow["nCLPID"]);
                    bClaimLevelCASExists = Convert.ToBoolean(dtClaimRow["CASExists"]);

                    sClaim_CLP03 = dtClaimRow["sCLP03_Amount"].ToString();
                    sClaim_CLP04 = dtClaimRow["sCLP04_Amount"].ToString();
                    nInsuranceId = 0;

                    oListNextActions = new EOBPayment.Common.PaymentInsuranceLineNextActions();

                    this.ERAClaimNo = dtClaimRow["sCLP01_ClaimSubmitterID"].ToString();

                    #region " IsClaimMatch "

                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.ISClaimMatch, String.Format("IsClaimMatch - Start for Claim # {0}", this.ERAClaimNo), ActivityOutCome.Success);

                    // Verify ERA Claim No is available in System and also check for the claim is voided.
                    if (!ClsERAValidation.IsClaimMatch(nBPRID, this.ERAClaimNo, nCLPId))
                    {
                        // Logs are written using Stored Procedure.
                        // Stop Posting
                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.ISClaimMatch, String.Format("Claim not matched for Claim # {0} & BPRID {1} ", this.ERAClaimNo, nBPRID), ActivityOutCome.Failure);
                        
                        IsStopPost = true;
                        continue;
                    }

                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.ISClaimMatch, "IsClaimMatch - End", ActivityOutCome.Success);

                    #endregion

                    #region " Determine Payer "

                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.DeterminePayer, "DeterminePayer - Start", ActivityOutCome.Success);

                    if (!ClsERAValidation.DeterminePayer(nBPRID, this.ERAClaimNo, nCLPId, out nContactID, out nInsuranceId,out nResponsibilityNo))  // Returns nContactID/PayerID //1044623:HardCode
                    {
                        // Logs are written using Stored Procedure.
                        // Stop Posting if nContactID is 0.

                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.DeterminePayer, string.Format("Stop Posting - Payer not found for the claim # {0}", this.ERAClaimNo), ActivityOutCome.Failure);
                        
                        IsStopPost = true;
                        continue;
                    }

                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.DeterminePayer, "DeterminePayer - End", ActivityOutCome.Success);

                    #endregion

                    #region " Reasons To Stop Payment "

                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.ReasonsToStopPayment, "ReasonsToStopPayment - Start", ActivityOutCome.Success);

                    if (ClsERAValidation.ReasonsToStopPaymentClaim(nBPRID, nCLPId, this.ERAClaimNo, nContactID))
                    {
                        // Logs are written using Stored Procedure.
                        // Stop Posting if nContactID is 0.
                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.ReasonsToStopPayment, string.Format("Stop Post for the Claim # {0}", this.ERAClaimNo), ActivityOutCome.Failure);

                        IsStopPost = true;
                        continue;
                    }

                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.ReasonsToStopPayment, "ReasonsToStopPayment - End", ActivityOutCome.Success);

                    #endregion

                    #region " Charged Matched Against Claim "


                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.ChargeMatchedAgainstClaim, "ChargeMatchedAgainstClaim - Start", ActivityOutCome.Success);


                    if (!ClsERAValidation.IsChargeMatchedAgaintClaim(nBPRID, this.ERAClaimNo, nContactID, nCLPId, nResponsibilityNo))
                    {
                        // Logs are written using Stored Procedure.
                        // Stop Posting.
                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.ChargeMatchedAgainstClaim, String.Format("Charges not matched against claim # {0}", this.ERAClaimNo), ActivityOutCome.Failure);

                        IsStopPost = true;
                        //////continue;
                    }

                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.ChargeMatchedAgainstClaim, "ChargeMatchedAgainstClaim - End", ActivityOutCome.Success);


                    #endregion

                    #region " Determine Next Action & Party "

                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.DetermineNextActionParty, "DetermineNextActionParty - Start", ActivityOutCome.Success);

                    Hashtable ohtTab = null;
                    //if (ClsERAValidation.DetermineNextActionParty(nBPRID, nCLPId, this.ERAClaimNo, nClaimStatus, nContactID, nInsuranceId, out ohtTab))
                    //{
                    //    // Logs are written using Stored Procedure.
                        // Stop Posting

                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.DetermineNextActionParty, string.Format("Next Action & Next Party not found for the Claim # {0}", this.ERAClaimNo), ActivityOutCome.Failure);
                        
                        IsStopPost = true;
                        //////continue;

                    //}

                    if (ohtTab != null) // condition added if multiple next action and party exists then hashtable will be rendered null.
                    {
                        this.NextAction = ohtTab["NextAction"].ToString();
                        this.NextParty = ohtTab["NextParty"].ToString();
                        this.NextActionContactID = ohtTab["NextContactId"].ToString();
                        this.NextActionPatientID = ohtTab["nNextActionPatientInsID"].ToString();
                        //htTab.Add("NextContactId", dr["NextContactId"].ToString());
                        //htTab.Add("IsStopPosting", dr["IsStopPosting"].ToString());
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.DetermineNextActionParty, string.Format("Multiple Next Action & Next Party found for the Claim # {0}", this.ERAClaimNo), ActivityOutCome.Failure);

                        IsStopPost = true;
                    }
                    ohtTab = null;

                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.DetermineNextActionParty, "DetermineNextActionParty - End", ActivityOutCome.Success);

                    #endregion

                    #region " Get Check Details & Fill Payment Tray Details "

                    #region " Get Check Details "
                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.GetCheckDetails, "GetCheckDetails - Start", ActivityOutCome.Success);

                    dtCheckDetails = GetCheckDetails(nBPRID, nContactID);

                    if (dtCheckDetails == null)
                    {
                        // Check details not found.
                        sMessage = String.Format("Check details not found for the nBPRID: {0}", nBPRID);
                        //ClsERASave.ClaimStatusLog(nBPRID,nCLPId, this.ERAClaimNo, 0, "", sMessage, LogStage.Claim);

                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.GetCheckDetails, sMessage, ActivityOutCome.Failure);
                        continue;
                    }
                    // Code to check/handle Insurance Plans without Company.
                    if (dtCheckDetails != null )
                    {
                        if (dtCheckDetails.Rows.Count > 0)
                        {
                            DataRow dr = dtCheckDetails.Rows[0];

                            if (dr["sPayerID"] != null)
                                if (dr["sPayerID"].ToString() == "0")
                                {
                                    ClsERASave.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "Payer is not associated with Insurance Company", LogStage.Claim);
                                    ClsERASave.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);
                                    //dr = null;
                                    IsStopPost = true;
                                    //////continue;
                                }

                            dr = null;
                        }
                        
                    }


                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.GetCheckDetails, "GetCheckDetails - End", ActivityOutCome.Success);

                    #endregion

                    #region " Fill Master Details "

                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.FillMasterDetails, "FillMasterDetails - Start", ActivityOutCome.Success);


                    // Assign Check details into the local Properties/Variable 
                    if (!FillMasterDetails(dtCheckDetails))
                    {
                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.FillMasterDetails, "Error while assigning Master details", ActivityOutCome.Failure);

                    }

                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.FillMasterDetails, "FillMasterDetails - End", ActivityOutCome.Success);

                    #endregion

                    //////// Commented on 25-Aug-2010, Since this scenario is handled in Database.
                    #region " Payment is Over-Adjusted -- Calculate Balance Amount (CheckAmount - Sum of PLB Amount) "
                    

                    // This method should execute only once. Calculate Balance (CheckAmount - Sum of PLB Amount)
                    // Calculate running balance to validate "Payment is Over-Adjusted".

                    //////if (!bCheckRunningFlag)
                    //////{
                    //////    //this.RunningCheckAmount = Convert.ToInt64(dtClaimRow["sCLP04_Amount"]);
                    //////    this.RunningCheckAmount = Math.Round(this.CheckAmount, 2); //- Math.Round(this.PLBAmount, 2);
                    //////    bCheckRunningFlag = true;
                    //////}

                    //////if ((Math.Round(this.RunningCheckAmount, 2) - Convert.ToDecimal(dtClaimRow["sCLP04_Amount"])) < 0) //(this.RunningCheckAmount < 0)
                    //////{
                    //////    if (this.PLBAmount != 0)
                    //////        sMessage = "PLB – Insufficient Funds";
                    //////    else
                    //////        sMessage = "Insufficient Funds";

                    //////    //ClsERASave.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", sMessage, LogStage.Claim);
                    //////    //ClsERASave.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);

                    //////    //IsStopPost = true;
                    //////    //////continue;
                    //////    sMessage = "";
                    //////    if (!bRunningCalculation)
                    //////    {
                    //////        sMessage = "Payment is Over-Allocated";
                    //////        ClsERASave.ClaimStatusLog(nBPRID, nCLPId, "0", 0, "", sMessage, LogStage.Check);
                    //////        bRunningCalculation = true;
                    //////    }
                    //////}

                    //////this.RunningCheckAmount = Math.Round(this.RunningCheckAmount, 2) - Convert.ToDecimal(dtClaimRow["sCLP04_Amount"]);

                    #endregion

                    #region " FillPaymentTray - COMMENTED "
                    //// Set payment tray information.
                    //gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                    //            ActivityType.FillMasterDetails, "FillPaymentTray - Start", ActivityOutCome.Success);
                    //FillPaymentTray();

                    //gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                    //            ActivityType.FillMasterDetails, "FillPaymentTray - End", ActivityOutCome.Success);

                    #endregion

                    #endregion

                    #region "Duplicate Check"

                    //if (bChkDupConfirm == false)
                    //{
                    //    if (ClsERAValidation.IsCheckDuplicate(nBPRID, this.CheckNumber, this.CheckAmount, SelectedInsuranceCompanyID))  // Returns nContactID/PayerID //1044623:HardCode
                    //    {
                    //        // Logs are written using Stored Procedure.
                    //        // Stop Posting if nContactID is 0.

                    //        DialogResult _dlgRst = DialogResult.None;
                    //        string sDisplayMsg=string.Empty;
                    //        sDisplayMsg = string.Format("ERA check already posted.\n\t\t Payer :{0} \n\t\t Check Number : {1} \n\t\t  Check Amount : {2} Continue posting ERA check? ", SelectedInsuranceCompany,this.CheckNumber,this.CheckAmount);
                    //        _dlgRst = MessageBox.Show(sDisplayMsg, _MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //        if (_dlgRst == DialogResult.No)
                    //        {
                    //            sMessage = "ERA check already posted";
                    //            return false;
                    //        }
                    //        else
                    //        {
                    //            bChkDupConfirm = true;
                    //        }

                    //        //continue;
                    //    }

                    //}

                    #endregion

                    oPayment = null;

                    if (IsPaymentMade()) //(IsPaymentMade() && txtCheckAmount.Text.Trim() != "")
                    {
                        //LockUnlockClaim(Convert.ToInt64(this.ERAClaimNo), true);

                        if (this.ERAClaimNo!=null && this.ERAClaimNo.Contains("-") && this.ERAClaimNo.Trim().Length>0)
                            ogloBilling.UpdateRecordStatus(GetLastTransactionId(this.ERAClaimNo), Convert.ToInt64(this.ERAClaimNo.Substring(0,this.ERAClaimNo.IndexOf("-"))), true);
                        else
                        ogloBilling.UpdateRecordStatus( GetLastTransactionId(this.ERAClaimNo), Convert.ToInt64(this.ERAClaimNo), true);

                        #region " Master Data "

                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.GetPaymentMaster, "GetPaymentMaster - Start", ActivityOutCome.Success);


                        // Get Payment Master Object

                        oPaymentInsurace = new EOBPayment.Common.PaymentInsurance();
                        oPaymentInsurace = GetPaymentMaster();
                        oPaymentInsurace.PaymentMode = this.PaymentMode;//solving sales force case -GLO2011-0010771

                       
                        
                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.GetPaymentMaster, "GetPaymentMaster - End", ActivityOutCome.Success);


                        #endregion

                        #region " Check/Cash/etc txtCheckAmount - Main Credit Line Entry "


                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.GetMainCreditLineEntry, "GetMainCreditLineEntry - Start", ActivityOutCome.Success);


                        // Get financial line no
                        _financeLineNo = oPaymentInsurace.EOBInsurancePaymentLineDetails.Count + 1;

                        oEOBInsPaymentCreditDetail = GetMainCreditLineEntry(_financeLineNo);  //, EOBInsurancePaymentMasterLines

                        // Add the Main credit line object to main payment object
                        oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentCreditDetail);


                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.GetMainCreditLineEntry, "GetMainCreditLineEntry - End", ActivityOutCome.Success);


                        #endregion

                        #region " GetCheckClaimDetails "
                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.GetCheckClaimDetails, "GetCheckClaimDetails - Start", ActivityOutCome.Success);

                        oPaymentInsurace.BPRID = nBPRID;

                        dtClaimDetails = GetCheckClaimDetails(nBPRID, nContactID, this.ERAClaimNo, nCLPId,nResponsibilityNo);

                        if (dtClaimDetails == null)
                        {
                            // Claim Details not found.
                            sMessage = String.Format("Claim charges/details not found for the nBPRID: {0}  and Claim: {1}", nBPRID, this.ERAClaimNo);

                            //ClsERASave.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", sMessage, LogStage.Claim);
                            //ClsERASave.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);

                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.GetCheckClaimDetails, sMessage, ActivityOutCome.Failure);
                        }
                        else
                        {
                            for (int i = 0; i < dtClaimDetails.Rows.Count; i++)
                            {                                
                                if (String.Format("{0:0.00}", (dtClaimDetails.Rows[i]["nPayment"]).ToString()) == "0.00" || String.Format("{0:0.00}", (dtClaimDetails.Rows[i]["nPayment"]).ToString()) == "0")
                               {
                                   ClsERASave.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, Convert.ToInt64(dtClaimDetails.Rows[i]["nSVCID"].ToString()), "z", sMessage, LogStage.ServiceLine);
                                   //ClsERASave.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, Convert.ToInt64((dtClaimDetails.Rows[i]["nSVCID"]).ToString()), "z", "", LogStage.ServiceLine);
                               }
                            }
                        }
                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                     ActivityType.GetCheckClaimDetails, "GetCheckClaimDetails - End", ActivityOutCome.Success);
                        #endregion

                        // Check for claim level CAS Exists.
                        // Create virtual charges and configure "dtClaimDetails" DataTable then rest the process will take care.
                        // CAS Segments at Claim level exists

                        bCASOtherReasonCodeExists = false;

                        bPayerCASWindowSetup = false;
                        // CAS window is setup for Payer
                        bPayerCASWindowSetup = Convert.ToBoolean(dtClaimRow["PostSecondaryAdjustments"]);


                        #region " Post to claims that contain Claim Level CAS. "
                        Int64 nTempSVCID = 0;
                        if (bClaimLevelCASExists)
                        {
                            if (dtClaimDetails != null)
                            {
                                // Get Claim Level Adjustments and calculate based on groupcode.
                                if (dtClaimDetails.Rows.Count == 1) // If only one charge available
                                {
                                    nTempSVCID = Convert.ToInt64(dtClaimDetails.Rows[0]["nSVCId"]);
                                    dtClaimDetails.Rows[0]["nTotalCharges"] = (sClaim_CLP03.Trim().Length > 0 ? sClaim_CLP03 : "");
                                    dtClaimDetails.Rows[0]["nPayment"] = (sClaim_CLP04.Trim().Length > 0 ? sClaim_CLP04 : "");

                                    // update row writeoff,withhold, coinsurance etc.,
                                    Hashtable htTemp = AdjustClaimLevelCAS(dtClaimDetails.Rows[0], nBPRID, nCLPId, nTempSVCID, nClaimStatus, bPayerCASWindowSetup);

                                    UpdateClaimDetails(htTemp, ref dtClaimDetails);
                                    
                                    bCASOtherReasonCodeExists = true;
                                    
                                    htTemp = null;
                                }
                                else if (dtClaimDetails.Rows.Count == 0)
                                {
                                    // Create a virtual charge line row and update writeoff,withhold, coinsurance etc.,
                                    dtClaimDetails = GetVirtualCharges(this.ERAClaimNo, nCLPId);
                                    // Update Payment(SVC03) & Total Charge (SVC02)

                                    if (dtClaimDetails != null)
                                    {
                                        if (dtClaimDetails.Rows.Count == 0)
                                        {
                                            // Needed clarification
                                        }
                                        else if (dtClaimDetails.Rows.Count == 1)
                                        {
                                            nTempSVCID = Convert.ToInt64(dtClaimDetails.Rows[0]["nSVCId"]);
                                            dtClaimDetails.Rows[0]["nTotalCharges"] = (sClaim_CLP03.Trim().Length > 0 ? sClaim_CLP03 : "");
                                            dtClaimDetails.Rows[0]["nPayment"] = (sClaim_CLP04.Trim().Length > 0?sClaim_CLP04:"");

                                            Hashtable htTemp = AdjustClaimLevelCAS(dtClaimDetails.Rows[0], nBPRID, nCLPId, nTempSVCID, nClaimStatus, bPayerCASWindowSetup);

                                            UpdateClaimDetails(htTemp, ref dtClaimDetails);

                                            bCASOtherReasonCodeExists = true;

                                            htTemp = null;
                                        }
                                        else
                                        {
                                            sMessage = "Claim Level Adjustments Exist";
                                            ClsERASave.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", sMessage, LogStage.Claim);
                                            ClsERASave.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);

                                            IsStopPost = true;
                                        }
                                    }
                                }
                                else
                                {
                                    // If more than one charge line exists along with claim level CAS then 
                                    // Update status log with "NP: Claim Level Adjustments Exist"
                                    sMessage = "Claim Level Adjustments Exist";
                                    ClsERASave.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", sMessage, LogStage.Claim);
                                    ClsERASave.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);

                                    IsStopPost = true;
                                }
                            }
                            //else // No SVC line available.
                            //{
                            //    // Create a virtual charge line row and update writeoff,withhold, coinsurance etc.,
                            //    dtClaimDetails = GetVirtualCharges(nBPRID);

                            //    Hashtable htTemp = AdjustClaimLevelCAS(dtClaimDetails.Rows[0]);

                            //    UpdateClaimDetails(htTemp, ref dtCheckDetails);
                            //}
                        }
                        else // No CAS Segment at Claim Level exists.
                        {

                            if (dtClaimDetails != null)
                            {
                                if (dtClaimDetails.Rows.Count == 0)  // No SVC Segments for a claim
                                {
                                    // Create a virtual charge line row and update writeoff,withhold, coinsurance etc.,
                                    dtClaimDetails = GetVirtualCharges(this.ERAClaimNo, nCLPId);

                                    if (dtClaimDetails != null)
                                    {

                                        if (dtClaimDetails.Rows.Count == 1) // If only once charge exists in the system for the claim then process further
                                        {
                                            nTempSVCID = Convert.ToInt64(dtClaimDetails.Rows[0]["nSVCId"]);
                                            dtClaimDetails.Rows[0]["nTotalCharges"] = (sClaim_CLP03.Trim().Length > 0 ? sClaim_CLP03 : "");
                                            dtClaimDetails.Rows[0]["nPayment"] = (sClaim_CLP04.Trim().Length > 0 ? sClaim_CLP04 : "");

                                            Hashtable htTemp = AdjustClaimLevelCAS(dtClaimDetails.Rows[0], nBPRID, nCLPId, nTempSVCID, nClaimStatus, bPayerCASWindowSetup);

                                            UpdateClaimDetails(htTemp, ref dtClaimDetails);

                                            bCASOtherReasonCodeExists = true;

                                            htTemp = null;
                                        }
                                        else
                                        {
                                            // If more than one charge line exists in the system
                                            // Update status log with "NP: Claim Level Adjustments Exist"
                                            sMessage = "Claim Level Adjustments Exist";
                                            ClsERASave.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", sMessage, LogStage.Claim);
                                            ClsERASave.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);

                                            IsStopPost = true;
                                        }
                                    }
                                }
                            }

                        }

                        // Validate charge level balance 
                        if (bCASOtherReasonCodeExists && bClaimLevelCASExists)
                        {

                            string sChargePaid = (dtClaimDetails.Rows[0]["nPayment"].ToString().Trim().Length ==0 )? "0" : dtClaimDetails.Rows[0]["nPayment"].ToString();
                            string sChargeBilled = (dtClaimDetails.Rows[0]["nTotalCharges"].ToString().Trim().Length ==0 )? "0" : dtClaimDetails.Rows[0]["nTotalCharges"].ToString();
                            string sChargeOtherReasonAmount = (dtClaimDetails.Rows[0]["nOtherReasonAmount"].ToString().Trim().Length ==0 )? "0" : dtClaimDetails.Rows[0]["nOtherReasonAmount"].ToString();

                            double nSumofChargeLevelCAS = 0;

                            if(dtClaimDetails.Rows[0]["nDeductible"].ToString().Trim().Length > 0) {nSumofChargeLevelCAS = nSumofChargeLevelCAS +  Convert.ToDouble(dtClaimDetails.Rows[0]["nDeductible"].ToString());}
                            if (dtClaimDetails.Rows[0]["nCoInsurance"].ToString().Trim().Length > 0) { nSumofChargeLevelCAS = nSumofChargeLevelCAS + Convert.ToDouble(dtClaimDetails.Rows[0]["nCoInsurance"].ToString()); }
                            if (dtClaimDetails.Rows[0]["nCopay"].ToString().Trim().Length > 0) { nSumofChargeLevelCAS = nSumofChargeLevelCAS + Convert.ToDouble(dtClaimDetails.Rows[0]["nCopay"].ToString()); }
                            if (dtClaimDetails.Rows[0]["nWriteOff"].ToString().Trim().Length > 0) { nSumofChargeLevelCAS = nSumofChargeLevelCAS + Convert.ToDouble(dtClaimDetails.Rows[0]["nWriteOff"].ToString()); }
                            if (dtClaimDetails.Rows[0]["nWithHold"].ToString().Trim().Length > 0) { nSumofChargeLevelCAS = nSumofChargeLevelCAS + Convert.ToDouble(dtClaimDetails.Rows[0]["nWithHold"].ToString()); }
                            if (dtClaimDetails.Rows[0]["nOtherReasonAmount"].ToString().Trim().Length > 0) { nSumofChargeLevelCAS = nSumofChargeLevelCAS + Convert.ToDouble(dtClaimDetails.Rows[0]["nOtherReasonAmount"].ToString()); }
                            
                            //  
                            if (Math.Round(Convert.ToDouble(sChargePaid),2) != Math.Round((Convert.ToDouble(sChargeBilled) - nSumofChargeLevelCAS), 2))
                            {
                                //Exec ERA_IN_PostingLogs @nBPRID, @nCLPId, @sERAClaimNo, 0, 'NP', 'Charge Payment Not Balanced', 2    
                                //Exec ERA_IN_PostingLogs @nBPRID, @nCLPId, @sERAClaimNo, @nSVCID, 'NP', 'Charge Payment Not Balanced', 3
                                //Exec ERA_SVCPostingLogs @nBPRID, @nCLPId, @sERAClaimNo, @nSVCID, 'NP', '', 3    
                                ClsERASave.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "Charge Payment Not Balanced", LogStage.Claim);
                                ClsERASave.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, nTempSVCID, "NP", "Charge Payment Not Balanced", LogStage.ServiceLine);
                                ClsERASave.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, nTempSVCID, "NP", "", LogStage.ServiceLine);
                                IsStopPost = true;

                            }


                            // Charged Billed is nTotalCharges
                            // Charged Paid is nPayment
                            // ERA_GetPayerSetupDetails
                            //GetPayerSetupDetails(nBPRID);

                            //nZeroPaidBilled -- (Zero Paid) Other Reasons equal to Billed 
                            if (this.ZeroPaidBilled)
                            {
                                if (Convert.ToDouble(sChargePaid) == 0 && Convert.ToDouble(sChargeOtherReasonAmount) == Convert.ToDouble(sChargeBilled))
                                {
                                    ClsERASave.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "ERA Payer Setup: (Zero Paid) Other Reasons equal to Billed Action is \"Stop Post\"", LogStage.Claim);
                                    ClsERASave.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);
                                    IsStopPost = true;
                                }
                            }

                            //nZeroPaidNotBilled -- (Zero Paid) Other Reasons not equal to Billed 
                            if (this.ZeroPaidNotBilled)
                            {
                                if (Convert.ToDouble(sChargePaid) == 0 && Convert.ToDouble(sChargeOtherReasonAmount) != Convert.ToDouble(sChargeBilled))
                                {
                                    ClsERASave.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "ERA Payer Setup: (Zero Paid) Other Reasons not equal to Billed Action is \"Stop Post\"", LogStage.Claim);
                                    ClsERASave.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);
                                    IsStopPost = true;
                                }
                            }

                            //nPaidNotZero -- Paid) Other Reasons not equal to 0.00 
                            if (this.PaidNotZero)
                            {
                                if (Convert.ToDouble(sChargePaid) > 0 && Convert.ToDouble(sChargeOtherReasonAmount) != 0)
                                {
                                    ClsERASave.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "ERA Payer Setup: (Paid) Other Reasons not equal to 0.00 Action is \"Stop Post\"", LogStage.Claim);
                                    ClsERASave.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);
                                    IsStopPost = true;
                                }
                            }

                         }

                        #endregion 

                        



                        bool bSecondaryAdjudication = false;
                        EOBPayment.Common.PaymentInsuranceLineNextActions oNextActions = null;
                        foreach (DataRow dRow in dtClaimDetails.Rows)   // Iterate Claim Charges
                        {
                            #region " For Each Claim Charge "
                            bSecondaryAdjudication = false;

                            bSecondaryAdjudication = Convert.ToBoolean(dRow["PostSecondaryAdjustments"]);

                            //bool bExitFlag = false;

                            this.PreTotalInsPaid = dRow["TotalInsPaid"].ToString();
                            this.PreTotalPatAdjustment = dRow["TotalPatAdjustment"].ToString();
                            this.PreTotalPatPaid = dRow["TotalPatPaid"].ToString();
                            this.PreTotalWithhold = dRow["TotalWithhold"].ToString();
                            this.PreTotalWriteOff = dRow["TotalWriteOff"].ToString();



                            // Created a new instance for storing the nextaction details which needs to be saved
                            oNextActions = new EOBPayment.Common.PaymentInsuranceLineNextActions();

                            // Created a new instance for split claim logic
                            SplitClaimDetails oSplitClaimDtls = null;
                            oSplitClaimDtls = new SplitClaimDetails();

                            bool bIsStopPayment = false;
                            // Reset Local Properties/Variable before processing a claims.
                            ResetClaimDetails();

                            // Fill claim details into the local Properties/variable for processing a claim.
                            FillClaimDetails(dRow);

                            PatientInsuranceID = nInsuranceId; //Convert.ToInt64(dRow["nInsuranceID"]);

                            #region " Earlier Code commented for Charges Over Paid & Over Adjusted.  "


                            //#region " Payment for the Charges Over Paid "

                            //gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                            //    ActivityType.PaymentChargesOverPaid, "PaymentChargesOverPaid - Start", ActivityOutCome.Success);

                            //// Payment for the charges is overpaid then stop post for the charge. (Point#10; Doc: ReasonToStopPost.doc)
                            //if (Convert.ToDouble(this.TotalCharges) < Convert.ToDouble(this.Payment))
                            //{
                            //    sMessage = String.Format("Charge Overpaid");

                            //    ClsERASave.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", sMessage, LogStage.Claim);
                            //    ClsERASave.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, this.SVCId, "op", sMessage, LogStage.ServiceLine);
                            //    ClsERASave.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, this.SVCId, "NP", "", LogStage.ServiceLine);

                            //    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                            //    ActivityType.PaymentChargesOverPaid, sMessage, ActivityOutCome.Failure);

                            //    //continue;
                            //    bExitFlag = true;
                            //    //break;
                            //}

                            //if (bExitFlag)
                            //{
                            //    oPayment = null;  // If payment charged over paid then stop claim. (uncomment this line to insert into database for correct/Passed charge lines).
                            //    break;
                            //}
                            //gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                            //    ActivityType.PaymentChargesOverPaid, "PaymentChargesOverPaid - End", ActivityOutCome.Success);



                            //#endregion

                            //#region " Calculate New Balance - Over Adjusted. "

                            //gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                            //    ActivityType.CalculateNewBalance, "CalculateNewBalance - Start", ActivityOutCome.Success);


                            //// Calculated new balance is -ve then stop post on the charge. (Point#11; Doc: ReasonToStopPost.doc)
                            //if (CalculateNewBalance())
                            //{
                            //    sMessage = String.Format("Charge Over-Adjusted");

                            //    ClsERASave.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", sMessage, LogStage.Claim);

                            //    ClsERASave.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, this.SVCId, "oa", sMessage, LogStage.ServiceLine);
                            //    ClsERASave.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, this.SVCId, "NP", "", LogStage.ServiceLine);

                            //    bIsStopPayment = true;

                            //    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                            //    ActivityType.CalculateNewBalance, sMessage, ActivityOutCome.Failure);
                            //    bExitFlag = true;
                            //    //break;
                            //    //continue;
                            //}

                            //if (bExitFlag)
                            //{
                            //    oPayment = null;
                            //    break;
                            //}

                            //gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                            //    ActivityType.CalculateNewBalance, "CalculateNewBalance - End", ActivityOutCome.Success);

                            #endregion

                            #region " New code for OverPaid & Over Adjustment (Negative Payment) "

                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.CalculateNewBalance, "CalculateNewBalance - Start", ActivityOutCome.Success);


                            // Calculated new balance is -ve then stop post on the charge. (Point#11; Doc: ReasonToStopPost.doc)
                            if (CalculateNewBalance(bSecondaryAdjudication))
                            {
                                sMessage = String.Format("Charge Balance is Negative");

                                if (!bCommentFlag)
                                {
                                    ClsERASave.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "n", sMessage, LogStage.Claim);
                                    bCommentFlag = true;
                                }

                                ClsERASave.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, this.SVCId, "n", sMessage, LogStage.ServiceLine);
                                //ClsERASave.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, this.SVCId, "", "", LogStage.ServiceLine);

                                bIsStopPayment = false;

                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.CalculateNewBalance, sMessage, ActivityOutCome.Failure);
                                //bExitFlag = true;
                                //break;
                                //continue;
                            }

                            //if (bExitFlag)
                            //{
                            //    oPayment = null;
                            //    break;
                            //}

                            #endregion

                            //#endregion
                            // Code Commented on 1ST Sept as this condition is incorporated in Stored Procedure "ERA_ReasonsToStopPaymentClaim"
                            #region " Zero Approved/Paid "

                            //////gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                            //////    ActivityType.ZeroApproved, "ZeroApproved - Start", ActivityOutCome.Success);

                            ////////If any charge is paid zero amount then stop post for entire claim. (Point#7; Doc: ReasonToStopPost.doc)
                            //////if (this._Payment.Trim() == "")
                            //////{

                            //////    sMessage = string.Format(" Zero Paid Charge");

                            //////    ClsERASave.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", sMessage, LogStage.Claim);
                            //////    ClsERASave.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);

                            //////    bIsStopPayment = true;

                            //////    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                            //////    ActivityType.ZeroApproved, sMessage, ActivityOutCome.Failure);
                            //////    IsStopPost = true;
                            //////    //////bExitFlag = true;
                            //////    //continue;
                            //////}

                            ////////////if (bExitFlag)
                            ////////////{
                            ////////////    oPayment = null;
                            ////////////    break;
                            ////////////}
                            //////// If any charge is paid zero amount then stop post for entire claim
                            //////if (Convert.ToDouble(this._Payment) == 0)
                            //////{
                            //////    sMessage = string.Format(" Zero Paid Charge");

                            //////    ClsERASave.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", sMessage, LogStage.Claim);
                            //////    ClsERASave.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);

                            //////    bIsStopPayment = true;

                            //////    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                            //////    ActivityType.ZeroApproved, sMessage, ActivityOutCome.Failure);
                            //////    IsStopPost = true;
                            //////    //////bExitFlag = true;
                            //////    //continue;
                            //////}

                            ////////////if (bExitFlag)
                            ////////////{
                            ////////////    oPayment = null;
                            ////////////    break;
                            ////////////}
                            //////gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                            //////    ActivityType.ZeroApproved, "ZeroApproved - End", ActivityOutCome.Success);


                            #endregion

                            #region " Code Commented for Payment Balanced "

                            //gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                            //            ActivityType.PaymentBalanced, "PaymentBalanced - Start", ActivityOutCome.Success);

                            //// Verify (nPayment == nCharge - nWriteOff - nWithHold - nCoInsurance - nDeduct - nCoPay) (Point#13; Doc: ReasonToStopPost.doc)

                            //if (!ISPaymentBalanced())
                            //{
                            //    sMessage = " Charge Payment Not Balanced";

                            //    ClsERASave.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", sMessage, LogStage.Claim);
                            //    ClsERASave.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);

                            //    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                            //            ActivityType.PaymentBalanced, sMessage, ActivityOutCome.Failure);

                            //    bExitFlag = true;
                            //    //continue;
                            //}
                            //gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                            //            ActivityType.PaymentBalanced, "PaymentBalanced - End", ActivityOutCome.Success);

                            //if (bExitFlag)
                            //{
                            //    oPayment = null; 
                            //    break;
                            //}

                            #endregion

                            // Since, Patient ID is not available at GetMainCreditLineEntry()
                            // Need to revisit on this logic - (Dev66).

                            oPaymentInsurace.EOBInsurancePaymentLineDetails[(_financeLineNo - 1)].PatientID = this.PatientID;
                            
                            if (PatientInsuranceID != 0)
                            {
                                oPaymentInsurace.EOBInsurancePaymentLineDetails[(_financeLineNo - 1)].MSTAccountID = PatientInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                oPaymentInsurace.EOBInsurancePaymentLineDetails[(_financeLineNo - 1)].MSTAccountType = EOBPaymentAccountType.PatientInsurace;
                                oPaymentInsurace.EOBInsurancePaymentLineDetails[(_financeLineNo - 1)].ContactInsID = nContactID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));
                            }

                            // Region added on 22_June_2010(Dev66) to process a ERA claims 
                            #region " Loop through the claim avalable in a single ERA file (Check)".

                            //..*** For correction (if amount -ve) we make credit entry against the cpt to balance cpt amount
                            //..*** & according to new logic we have to make credit line entry against current check with making the 
                            //..** -ve correction amount +ve


                            #region "Correction Line Credit Line Entry - Credit -ve against CPT & Positive Credit line against current check."
                            decimal _crPayAmt = 0;

                            ////// Verify for the line is of type service line
                            ////// Verify for the line column is in correction mode.
                            ////// Verify amount in payment column.

                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.VerifyPaymentCorrection, "VerifyPaymentCorrection - Start", ActivityOutCome.Success);
                            
                            int _crResPayMode = 0;
                            
                            if (VerifyPaymentCorrection())  // If previous payments made towards claim line charges then retrieve previous payment. 
                            {
                                #region "Commented code for Correction"
                                //////if (this.Payment.Trim() != "") ///(c1SinglePayment.GetData(nCrIndex, COL_PAYMENT) != null && c1SinglePayment.GetData(nCrIndex, COL_PAYMENT).ToString().Trim() != "")
                                //////{
                                //////    _crPayAmt = Convert.ToDecimal(GetLastPayment()); //Convert.ToDecimal(c1SinglePayment.GetData(nCrIndex, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(nCrIndex, COL_LAST_PAYMENT));

                                //////    if (_crPayAmt < 0)
                                //////    {
                                //////        _crPayAmt = _crPayAmt - (_crPayAmt * 2);
                                //////        _crResPayMode = 0;

                                //////        Int64 _crPatientId = this.PatientID; // Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_PATIENTID));
                                //////        Int64 _crBillTrnId = this.BillingTransactionID; //Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_ID));
                                //////        Int64 _crBillTrnDtlId = this.BillingTransactionDetailID; //Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_DETAILID));

                                //////        // TO DO : delete as no reference found
                                //////        //Int64 _crBillTrnDtlLineId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_LINENO));
                                //////        //Int64 _crTrackBillTrnId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_ID));
                                //////        //Int64 _crTrackBillTrnDtlId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                //////        //Int64 _crTrackBillTrnDtlLineId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_LINENO));
                                //////        //string _crCptCode = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_CODE)).Trim();

                                //////        //DataTable _nCrDataTable = InsurancePayment.GetCorrectionRefList(_crPayAmt, _crPatientId, _crBillTrnId, _crBillTrnDtlId, this.PatientInsuranceID, this.ContactInsuranceID, this.SelectedInsuranceCompanyID);
                                //////        DataTable _nCrDataTable = InsurancePayment.GetCorrectionRefList(_crPayAmt, _crPatientId, _crBillTrnId, _crBillTrnDtlId, this.PatientInsuranceID, this.ContactInsuranceID);

                                //////        if (_nCrDataTable != null && _nCrDataTable.Rows.Count > 0)
                                //////        {
                                //////            for (int nCrDTIndex = 0; nCrDTIndex <= _nCrDataTable.Rows.Count - 1; nCrDTIndex++)
                                //////            {
                                //////                #region "Set Object to make -ve credit line entry for cpt balance calculation"

                                //////                EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsPaymentCorrAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
                                //////                oEOBInsPaymentCorrAsCreditDetail.EOBPaymentID = _EOBPaymentID;
                                //////                oEOBInsPaymentCorrAsCreditDetail.EOBID = 0;
                                //////                oEOBInsPaymentCorrAsCreditDetail.EOBDtlID = 0;
                                //////                oEOBInsPaymentCorrAsCreditDetail.EOBPaymentDetailID = 0;

                                //////                //...Will be assigning current check payment & payment details id's to Ref. Id.
                                //////                oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentID = 0;
                                //////                oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentDetailID = 0;
                                //////                oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentID = 0;// Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentID"]);
                                //////                oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentDetailID = 0;// Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentDetailID"]);

                                //////                oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]);
                                //////                oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]);
                                //////                oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentID"]);
                                //////                oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentDetailID"]);

                                //////                oEOBInsPaymentCorrAsCreditDetail.BillingTransactionID = this.BillingTransactionID; // Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_ID));
                                //////                oEOBInsPaymentCorrAsCreditDetail.BillingTransactionDetailID = this.BillingTransactionDetailID; // Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_DETAILID));
                                //////                oEOBInsPaymentCorrAsCreditDetail.BillingTransactionLineNo = Convert.ToInt32(this.BillingTransactionLineNo); // Convert.ToInt32(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_LINENO));

                                //////                oEOBInsPaymentCorrAsCreditDetail.SubClaimNo = this.SubclaimNo; // Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_SUBCLAIMNO));
                                //////                oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionID = this.TrackTrnID; // Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_ID));
                                //////                oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionDetailID = this.TrackTrnDtlID ;//Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                //////                oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionLineNo = Convert.ToInt32(this.BillingTransactionLineNo);//Convert.ToInt32(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_LINENO));

                                //////                oEOBInsPaymentCorrAsCreditDetail.DOSFrom = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(this.DOSFrom)); // Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_FROM).ToString()));
                                //////                oEOBInsPaymentCorrAsCreditDetail.DOSTo = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(this.DOSTo)); //Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_TO).ToString()));

                                //////                oEOBInsPaymentCorrAsCreditDetail.CPTCode = this.CPT; //Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_CODE));
                                //////                oEOBInsPaymentCorrAsCreditDetail.CPTDescription = this.CPTDescription; //Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_DESCRIPTON));
                                //////                oEOBInsPaymentCorrAsCreditDetail.Amount = Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]) - (Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]) * 2);
                                //////                oEOBInsPaymentCorrAsCreditDetail.IsNullAmount = false;
                                //////                oEOBInsPaymentCorrAsCreditDetail.PaymentType = EOBPaymentType.InsuracePayment;
                                //////                oEOBInsPaymentCorrAsCreditDetail.PaymentSubType = EOBPaymentSubType.Correction;
                                //////                oEOBInsPaymentCorrAsCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
                                //////                oEOBInsPaymentCorrAsCreditDetail.PayMode = (EOBPaymentMode)_crResPayMode;

                                //////                //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                //////                oEOBInsPaymentCorrAsCreditDetail.AccountID = SelectedInsuranceCompanyID; //Convert.ToInt64(lblInsCompany.Tag);
                                //////                oEOBInsPaymentCorrAsCreditDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
                                //////                oEOBInsPaymentCorrAsCreditDetail.MSTAccountID = PatientInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                //////                oEOBInsPaymentCorrAsCreditDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
                                //////                oEOBInsPaymentCorrAsCreditDetail.ContactInsID = ContactInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));

                                //////                oEOBInsPaymentCorrAsCreditDetail.PatientID = this.PatientID; //PatientControl.PatientID;
                                //////                oEOBInsPaymentCorrAsCreditDetail.PaymentTrayID = SelectedPaymentTrayID;
                                //////                oEOBInsPaymentCorrAsCreditDetail.PaymentTrayCode = SelectedPaymentTrayCode;
                                //////                oEOBInsPaymentCorrAsCreditDetail.PaymentTrayDescription = SelectedPaymentTray;
                                //////                oEOBInsPaymentCorrAsCreditDetail.UserID = AppSettings.UserID;
                                //////                oEOBInsPaymentCorrAsCreditDetail.UserName = AppSettings.UserName;
                                //////                oEOBInsPaymentCorrAsCreditDetail.ClinicID = AppSettings.ClinicID;

                                //////                oEOBInsPaymentCorrAsCreditDetail.FinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails.Count + 1;
                                //////                oEOBInsPaymentCorrAsCreditDetail.MainCreditLineID = 0;
                                //////                oEOBInsPaymentCorrAsCreditDetail.IsMainCreditLine = false;
                                //////                oEOBInsPaymentCorrAsCreditDetail.IsReserveCreditLine = false;
                                //////                oEOBInsPaymentCorrAsCreditDetail.IsCorrectionCreditLine = true;
                                //////                oEOBInsPaymentCorrAsCreditDetail.RefFinanceLieNo = 0;
                                //////                oEOBInsPaymentCorrAsCreditDetail.UseRefFinanceLieNo = false;

                                //////                //if (mskCloseDate.MaskCompleted == true)
                                //////                //{
                                //////                //    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                //////                //    oEOBInsPaymentCorrAsCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                //////                //}
                                //////                oEOBInsPaymentCorrAsCreditDetail.CloseDate = this.CloseDate;


                                //////                oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentCorrAsCreditDetail);
                                //////                oEOBInsPaymentCorrAsCreditDetail.Dispose();

                                //////                #endregion

                                //////                #region "Set Object to make +ve credit entry against current check"

                                //////                //---->> 1 = Add Object , 2 = Modify Object , 0 = Do Nothing
                                //////                int _Object_Add_Modify_None = -1;
                                //////                int _Object_Modify_Index = -1;

                                //////                if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
                                //////                {
                                //////                    for (int index = 0; index < EOBInsurancePaymentMasterLines.Count; index++)
                                //////                    {
                                //////                        if (EOBInsurancePaymentMasterLines[index].IsCorrectionCreditLine == true)
                                //////                        {
                                //////                            //1. Check if the correction amount is from the current check if yes do not add object

                                //////                            //2. If correction amount is from different check & the credit line does not exists then 
                                //////                            //   add the +ve credit line entry

                                //////                            //3. If the correction amount is from different check & the credit line exists then
                                //////                            //   modify the credit line entry

                                //////                            if (EOBInsurancePaymentMasterLines[index].RefEOBPaymentID == _EOBPaymentID)
                                //////                            {
                                //////                                _Object_Add_Modify_None = 0;
                                //////                                break;
                                //////                            }
                                //////                            else if (EOBInsurancePaymentMasterLines[index].RefEOBPaymentID == Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"])
                                //////                                && EOBInsurancePaymentMasterLines[index].RefEOBPaymentDetailID == Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]))
                                //////                            {
                                //////                                EOBInsurancePaymentMasterLines[index].Amount += Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]);
                                //////                                _Object_Add_Modify_None = 2;
                                //////                                _Object_Modify_Index = index;
                                //////                                break;
                                //////                            }
                                //////                            else
                                //////                            {
                                //////                                _Object_Add_Modify_None = 1;
                                //////                            }
                                //////                        }
                                //////                    }
                                //////                }
                                //////                else
                                //////                { _Object_Add_Modify_None = 1; }

                                //////                if (_Object_Add_Modify_None == 1)
                                //////                {
                                //////                    #region " Set new Credit line object "

                                //////                    oEOBInsPaymentCorrAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
                                //////                    oEOBInsPaymentCorrAsCreditDetail.EOBPaymentID = _EOBPaymentID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.EOBID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.EOBDtlID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.EOBPaymentDetailID = 0;

                                //////                    //...Will be assigning current check payment & payment details id's to Ref. Id.
                                //////                    //oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]);
                                //////                    //oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentID"]);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentDetailID"]);

                                //////                    oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentID"]);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentDetailID"]);

                                //////                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionDetailID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionLineNo = 0;

                                //////                    oEOBInsPaymentCorrAsCreditDetail.SubClaimNo = "";
                                //////                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionDetailID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionLineNo = 0;

                                //////                    oEOBInsPaymentCorrAsCreditDetail.DOSFrom = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(this.DOSFrom)); //Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_FROM).ToString()));
                                //////                    oEOBInsPaymentCorrAsCreditDetail.DOSTo = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(this.DOSTo)); //Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_TO).ToString()));

                                //////                    oEOBInsPaymentCorrAsCreditDetail.CPTCode = "";
                                //////                    oEOBInsPaymentCorrAsCreditDetail.CPTDescription = "";
                                //////                    oEOBInsPaymentCorrAsCreditDetail.Amount = Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.IsNullAmount = false;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentType = EOBPaymentType.InsuranceCorrection;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentSubType = EOBPaymentSubType.Correction;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PayMode = (EOBPaymentMode)_crResPayMode;

                                //////                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                //////                    oEOBInsPaymentCorrAsCreditDetail.AccountID = SelectedInsuranceCompanyID; //Convert.ToInt64(lblInsCompany.Tag);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;

                                //////                    if (PatientInsuranceID != 0)// lblPayer.Tag != null && Convert.ToString(lblPayer.Tag).Trim() != "")
                                //////                    {
                                //////                        oEOBInsPaymentCorrAsCreditDetail.MSTAccountID = PatientInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                //////                        oEOBInsPaymentCorrAsCreditDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
                                //////                        oEOBInsPaymentCorrAsCreditDetail.ContactInsID = ContactInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));
                                //////                    }

                                //////                    oEOBInsPaymentCorrAsCreditDetail.PatientID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayID = SelectedPaymentTrayID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayCode = SelectedPaymentTrayCode;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayDescription = SelectedPaymentTray;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.UserID = AppSettings.UserID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.UserName = AppSettings.UserName;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.ClinicID = AppSettings.ClinicID;

                                //////                    oEOBInsPaymentCorrAsCreditDetail.FinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails.Count + 1;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.MainCreditLineID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.IsMainCreditLine = false;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.IsReserveCreditLine = false;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.IsCorrectionCreditLine = true;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.RefFinanceLieNo = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.UseRefFinanceLieNo = false;

                                //////                    //if (mskCloseDate.MaskCompleted == true)
                                //////                    //{
                                //////                    //    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                //////                    //    oEOBInsPaymentCorrAsCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                //////                    //}

                                //////                    oEOBInsPaymentCorrAsCreditDetail.CloseDate = this.CloseDate;

                                //////                    oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentCorrAsCreditDetail);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.Dispose();
                                //////                    break;

                                //////                    #endregion " Set new Credit line object "
                                //////                }
                                //////                else if (_Object_Add_Modify_None == 2)
                                //////                {
                                //////                    #region " Set new Credit line object "

                                //////                    oEOBInsPaymentCorrAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
                                //////                    oEOBInsPaymentCorrAsCreditDetail.EOBPaymentID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].EOBPaymentID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.EOBID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].EOBID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.EOBDtlID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].EOBDtlID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.EOBPaymentDetailID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].EOBPaymentDetailID;


                                //////                    oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].RefEOBPaymentID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentDetailID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].RefEOBPaymentDetailID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].ReserveEOBPaymentID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentDetailID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].ReserveEOBPaymentDetailID;

                                //////                    oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].OldRefEOBPaymentID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentDetailID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].OldRefEOBPaymentDetailID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].OldReserveEOBPaymentID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentDetailID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].OldReserveEOBPaymentDetailID;

                                //////                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionDetailID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionLineNo = 0;

                                //////                    oEOBInsPaymentCorrAsCreditDetail.SubClaimNo = "";
                                //////                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionDetailID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionLineNo = 0;

                                //////                    oEOBInsPaymentCorrAsCreditDetail.DOSFrom = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(this.DOSFrom));//Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_FROM).ToString()));
                                //////                    oEOBInsPaymentCorrAsCreditDetail.DOSTo = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(this.DOSTo)); //Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_TO).ToString()));

                                //////                    oEOBInsPaymentCorrAsCreditDetail.CPTCode = "";
                                //////                    oEOBInsPaymentCorrAsCreditDetail.CPTDescription = "";
                                //////                    oEOBInsPaymentCorrAsCreditDetail.Amount = EOBInsurancePaymentMasterLines[_Object_Modify_Index].Amount;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.IsNullAmount = false;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentType = EOBPaymentType.InsuranceCorrection;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentSubType = EOBPaymentSubType.Correction;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PayMode = (EOBPaymentMode)_crResPayMode;

                                //////                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                //////                    oEOBInsPaymentCorrAsCreditDetail.AccountID = SelectedInsuranceCompanyID; //Convert.ToInt64(lblInsCompany.Tag);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;

                                //////                    if (PatientInsuranceID != 0)// lblPayer.Tag != null && Convert.ToString(lblPayer.Tag).Trim() != "")
                                //////                    {
                                //////                        oEOBInsPaymentCorrAsCreditDetail.MSTAccountID = PatientInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                //////                        oEOBInsPaymentCorrAsCreditDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
                                //////                        oEOBInsPaymentCorrAsCreditDetail.ContactInsID = ContactInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));
                                //////                    }

                                //////                    oEOBInsPaymentCorrAsCreditDetail.PatientID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayID = SelectedPaymentTrayID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayCode = SelectedPaymentTrayCode;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayDescription = SelectedPaymentTray;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.UserID = AppSettings.UserID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.UserName = AppSettings.UserName;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.ClinicID = AppSettings.ClinicID;

                                //////                    oEOBInsPaymentCorrAsCreditDetail.FinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails.Count + 1;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.MainCreditLineID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.IsMainCreditLine = false;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.IsReserveCreditLine = false;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.IsCorrectionCreditLine = true;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.RefFinanceLieNo = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.UseRefFinanceLieNo = false;

                                //////                    //if (mskCloseDate.MaskCompleted == true)
                                //////                    //{
                                //////                    //    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                //////                    //    oEOBInsPaymentCorrAsCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                //////                    //}

                                //////                    oEOBInsPaymentCorrAsCreditDetail.CloseDate = this.CloseDate;

                                //////                    oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentCorrAsCreditDetail);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.Dispose();
                                //////                    break;

                                //////                    #endregion " Set new Credit line object "
                                //////                }

                                //////                #endregion
                                //////            }
                                //////        }
                                //////    }
                                //////}
                                #endregion
                            }
                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.VerifyPaymentCorrection, "VerifyPaymentCorrection - End", ActivityOutCome.Success);

                            #endregion

                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.UseReserved, "UseReserved - Start", ActivityOutCome.Success);

                            #region "Use Reserved Credit Line Entry"

                            if (IsReserveUsed)
                            {
                                //////for (int i = 0; i <= EOBInsurancePaymentMasterLines.Count - 1; i++)
                                //////{
                                //////    EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine EOBInsurancePaymentMasterLine = EOBInsurancePaymentMasterLines[i];

                                //////    //..Code changes done by Sagar Ghodke on 20100105(critical change Confirmation needed)
                                //////    //...Below commented condition is previous one
                                //////    //if (EOBInsurancePaymentMasterLines[i].PaymentType == EOBPaymentType.InsuracePayment)
                                //////    if (EOBInsurancePaymentMasterLine.PaymentType == EOBPaymentType.InsuraceReserverd && EOBInsurancePaymentMasterLine.PaymentSubType == EOBPaymentSubType.Reserved)
                                //////    {
                                //////        EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsPaymentResAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
                                //////        // Get financial line no
                                //////        _financeLineNo = oPaymentInsurace.EOBInsurancePaymentLineDetails.Count + 1;
                                //////        // Pass finalcial line & used reserve details to get the credit line object for used reserve
                                //////        oEOBInsPaymentResAsCreditDetail = GetCreditLineForReserveUsed(EOBInsurancePaymentMasterLine, _financeLineNo);
                                //////        // Add the credit line object to Main payment object
                                //////        oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentResAsCreditDetail);
                                //////        oEOBInsPaymentResAsCreditDetail.Dispose();
                                //////    }
                                //////}
                            }

                            #endregion

                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                ActivityType.UseReserved, "UseReserved - End", ActivityOutCome.Success);


                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                               ActivityType.EOBLine, "EOBLine - Start", ActivityOutCome.Success);

                            if (oPaymentInsurace != null && oPaymentInsurace.EOBInsurancePaymentLineDetails != null)
                            {
                                EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine oEOBInsPaymentAllDtl = null;
                                bool _AddLine = true;
                                for (int i = 0; i <= oPaymentInsurace.EOBInsurancePaymentLineDetails.Count - 1; i++)
                                {
                                     _AddLine = true;

                                     oEOBInsPaymentAllDtl = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine();

                                    #region "If Credit line first time added and its second time then dont add just update the amount"

                                    //////if (oPaymentInsurace.EOBInsurancePaymentLineDetails[i].IsMainCreditLine == true)
                                    //////{
                                    //////    for (int ccl = 0; ccl <= EOBInsurancePaymentMasterAllocationLines.Count - 1; ccl++)
                                    //////    {
                                    //////        if (EOBInsurancePaymentMasterAllocationLines[ccl].IsMainCreditLine == true)
                                    //////        {
                                    //////            decimal _OldCheckBalAmt = EOBInsurancePaymentMasterAllocationLines[ccl].Amount;
                                    //////            decimal _OldCheckAmt = 0;
                                    //////            for (int cml = 0; cml <= EOBInsurancePaymentMasterLines.Count - 1; cml++)
                                    //////            {
                                    //////                if (EOBInsurancePaymentMasterLines[cml].IsMainCreditLine == true)
                                    //////                {
                                    //////                    _OldCheckAmt = EOBInsurancePaymentMasterLines[cml].Amount;
                                    //////                    break;
                                    //////                }
                                    //////            }
                                    //////            decimal _NewCheckAmt = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].Amount;
                                    //////            decimal _NewCheckBalAmt = 0;
                                    //////            if (_NewCheckAmt < _OldCheckAmt)
                                    //////            {
                                    //////                _NewCheckBalAmt = _OldCheckBalAmt - (_OldCheckAmt - _NewCheckAmt);
                                    //////            }
                                    //////            else
                                    //////            {
                                    //////                _NewCheckBalAmt = _OldCheckBalAmt + (_NewCheckAmt - _OldCheckAmt);
                                    //////            }

                                    //////            EOBInsurancePaymentMasterAllocationLines[ccl].Amount = _NewCheckBalAmt;// oPaymentInsurace.EOBInsurancePaymentLineDetails[ccl].Amount;
                                    //////            _AddLine = false;
                                    //////            break;
                                    //////        }
                                    //////    }
                                    //////}
                                    #endregion

                                    if (_AddLine == true)
                                    {
                                        oEOBInsPaymentAllDtl = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine();

                                        #region " Set Object "

                                        oEOBInsPaymentAllDtl.EOBPaymentID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].EOBPaymentID;
                                        oEOBInsPaymentAllDtl.EOBID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].EOBID;
                                        oEOBInsPaymentAllDtl.EOBDtlID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].EOBDtlID;
                                        oEOBInsPaymentAllDtl.EOBPaymentDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].EOBPaymentDetailID;

                                        oEOBInsPaymentAllDtl.BillingTransactionID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].BillingTransactionID;
                                        oEOBInsPaymentAllDtl.BillingTransactionDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].BillingTransactionDetailID;
                                        oEOBInsPaymentAllDtl.BillingTransactionLineNo = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].BillingTransactionLineNo;

                                        oEOBInsPaymentAllDtl.TrackBillingTransactionID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].TrackBillingTransactionID;
                                        oEOBInsPaymentAllDtl.TrackBillingTransactionDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].TrackBillingTransactionDetailID;
                                        oEOBInsPaymentAllDtl.TrackBillingTransactionLineNo = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].TrackBillingTransactionLineNo;

                                        oEOBInsPaymentAllDtl.PatientID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PatientID;
                                        oEOBInsPaymentAllDtl.DOSFrom = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].DOSFrom;
                                        oEOBInsPaymentAllDtl.DOSTo = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].DOSTo;
                                        oEOBInsPaymentAllDtl.CPTCode = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].CPTCode;
                                        oEOBInsPaymentAllDtl.CPTDescription = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].CPTDescription;
                                        oEOBInsPaymentAllDtl.Amount = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].Amount;
                                        oEOBInsPaymentAllDtl.IsNullAmount = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].IsNullAmount;
                                        oEOBInsPaymentAllDtl.PaymentType = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PaymentType;
                                        oEOBInsPaymentAllDtl.PaymentSubType = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PaymentSubType;
                                        oEOBInsPaymentAllDtl.PaySign = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PaySign;
                                        oEOBInsPaymentAllDtl.PayMode = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PayMode;

                                        oEOBInsPaymentAllDtl.RefEOBPaymentID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].RefEOBPaymentID;
                                        oEOBInsPaymentAllDtl.RefEOBPaymentDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].RefEOBPaymentDetailID;
                                        oEOBInsPaymentAllDtl.ReserveEOBPaymentID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ReserveEOBPaymentID;
                                        oEOBInsPaymentAllDtl.ReserveEOBPaymentDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ReserveEOBPaymentDetailID;

                                        oEOBInsPaymentAllDtl.OldRefEOBPaymentID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].RefEOBPaymentID;
                                        oEOBInsPaymentAllDtl.OldRefEOBPaymentDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].RefEOBPaymentDetailID;
                                        oEOBInsPaymentAllDtl.OldReserveEOBPaymentID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ReserveEOBPaymentID;
                                        oEOBInsPaymentAllDtl.OldReserveEOBPaymentDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ReserveEOBPaymentDetailID;


                                        oEOBInsPaymentAllDtl.AccountID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].AccountID;
                                        oEOBInsPaymentAllDtl.AccountType = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].AccountType;
                                        oEOBInsPaymentAllDtl.MSTAccountID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].MSTAccountID;
                                        oEOBInsPaymentAllDtl.MSTAccountType = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].MSTAccountType;
                                        oEOBInsPaymentAllDtl.PaymentTrayID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PaymentTrayID;
                                        oEOBInsPaymentAllDtl.PaymentTrayCode = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PaymentTrayCode;
                                        oEOBInsPaymentAllDtl.PaymentTrayDescription = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PaymentTrayDescription;
                                        oEOBInsPaymentAllDtl.UserID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].UserID;
                                        oEOBInsPaymentAllDtl.UserName = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].UserName;
                                        oEOBInsPaymentAllDtl.CreatedDateTime = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].CreatedDateTime;
                                        oEOBInsPaymentAllDtl.ModifiedDateTime = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ModifiedDateTime;
                                        oEOBInsPaymentAllDtl.ClinicID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ClinicID;

                                        oEOBInsPaymentAllDtl.FinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].FinanceLieNo;
                                        oEOBInsPaymentAllDtl.MainCreditLineID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].MainCreditLineID;
                                        oEOBInsPaymentAllDtl.IsMainCreditLine = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].IsMainCreditLine;
                                        oEOBInsPaymentAllDtl.IsReserveCreditLine = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].IsReserveCreditLine;
                                        oEOBInsPaymentAllDtl.IsCorrectionCreditLine = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].IsCorrectionCreditLine;
                                        oEOBInsPaymentAllDtl.RefFinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].RefFinanceLieNo;
                                        oEOBInsPaymentAllDtl.UseRefFinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].UseRefFinanceLieNo;
                                        oEOBInsPaymentAllDtl.ContactInsID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ContactInsID;


                                        #endregion " Set Object "

                                        EOBInsurancePaymentMasterAllocationLines.Add(oEOBInsPaymentAllDtl);
                                        oEOBInsPaymentAllDtl.Dispose();
                                    }
                                }
                            }

                            //while assigning this object for collection amount object, there are -ve amount of correction
                            //we have to make it positive for debit line allocation
                            //so using for loop we will make it positive
                            for (int nAlctn = 0; nAlctn <= EOBInsurancePaymentMasterAllocationLines.Count - 1; nAlctn++)
                            {
                                if (EOBInsurancePaymentMasterAllocationLines[nAlctn].Amount < 0)
                                {
                                    EOBInsurancePaymentMasterAllocationLines[nAlctn].Amount = EOBInsurancePaymentMasterAllocationLines[nAlctn].Amount - (EOBInsurancePaymentMasterAllocationLines[nAlctn].Amount * 2);
                                }
                            }

                            //Allocation Amount - Finish


                            #region " ......................... Claim Payment Details Start ................................. "

                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                               ActivityType.EOBLine, "Claim Payment Details Start", ActivityOutCome.Success);

                            oPaymentInsuranceClaim = new EOBPayment.Common.PaymentInsuranceClaim();

                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                               ActivityType.EOBLine, "BillingTransactionID is Greater than 0; Present BillingTransactionID is " + this.BillingTransactionID, ActivityOutCome.Success);

                            // Similar to get the Service Line Type as Claim in Manual posting.
                            if (this.BillingTransactionID > 0) //(c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                            {


                                // The following line are commented on 21_June_2010 (Dev66), 
                                // Once conditions are define then the commented code can be removed.
                                // Hidden Grid row used to store meta data.
                                //////if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                                //////{
                                //////    for (int clmIndex = 1; clmIndex < c1SinglePayment.Rows.Count; clmIndex++)
                                //////    {
                                //////        if (c1SinglePayment.GetData(clmIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(clmIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.Claim)
                                //////        {
                                //////            if (c1SinglePayment.GetData(clmIndex, COL_BILLING_TRANSACTON_ID) != null && Convert.ToString(c1SinglePayment.GetData(clmIndex, COL_BILLING_TRANSACTON_ID)).Trim() != "")
                                //////            {
                                oPaymentInsuranceClaim.BillingTransactionID = this.BillingTransactionID; //(Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(clmIndex, COL_BILLING_TRANSACTON_ID));
                                oPaymentInsuranceClaim.TrackBillingTrnID = this.TrackTrnID; // this.BillingTransactionDetailID; // (Int64)dRow["nTrackTrnId"];  //Convert.ToInt64(c1SinglePayment.GetData(clmIndex, COL_TRACK_BILLING_TRANSACTON_ID));
                                oPaymentInsuranceClaim.ClaimNo = this.ClaimNo; // (Int64)dRow["nClaimNo"]; //Convert.ToInt64(c1SinglePayment.GetData(clmIndex, COL_CLAIMNO));
                                oPaymentInsuranceClaim.SubClaimNo = this.SubclaimNo; // dRow["nSubClaimNo"].ToString(); //Convert.ToString(c1SinglePayment.GetData(clmIndex, COL_SUBCLAIMNO));

                                oSplitClaimDetails.TransactionMasterID = oPaymentInsuranceClaim.BillingTransactionID;
                                oSplitClaimDetails.TransactionID = oPaymentInsuranceClaim.TrackBillingTrnID;
                                oSplitClaimDetails.ClaimNo = oPaymentInsuranceClaim.ClaimNo;
                                oSplitClaimDetails.SubClaimNo = oPaymentInsuranceClaim.SubClaimNo;
                                oSplitClaimDetails.ClinicID = AppSettings.ClinicID;
                                //////            }
                                //////        }
                                //////    }
                                //////}

                                #region "EOB Service Lines - New Logic - Direct allocation from credit line insted of allocation from correction, reserve and check"

                                EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsurancePaymentDetail = null;

                                // Condition to be implemented.
                                // 1. Identify line is of type Service line.
                                // 2. Verify value available in payment payment column.

                                // The following (for loop & if conditions are commented on 21_June_2010(Dev66).

                                //////for (int i = 0; i <= c1SinglePayment.Rows.Count - 1; i++)
                                //////{
                                //////    if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                                //////    {
                                //////        if (
                                //////                (c1SinglePayment.GetData(i, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PAYMENT)).Trim() != "")
                                //////            //|| (c1SinglePayment.GetData(i, COL_NEXT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT)).Trim() != "")
                                //////            )
                                //////        {


                                EOBPayment.Common.PaymentInsuranceLine oPaymentInsuranceLine = new EOBPayment.Common.PaymentInsuranceLine();
                                bool _Add_WO_WH = false;
                                //*****  EOB Lines 
                                #region "EOB Line"
                                //QCheck for PatInsuranceID,InsContactID
                                oPaymentInsuranceLine.PatientID = this.PatientID; // (Int64)dRow["nPatientID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_PATIENTID));
                                oPaymentInsuranceLine.PatInsuranceID = PatientInsuranceID;  //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                oPaymentInsuranceLine.InsContactID = nContactID;//ContactInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));

                                oPaymentInsuranceLine.BLTransactionID = this.BillingTransactionID; // (Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                oPaymentInsuranceLine.BLTransactionDetailID = this.BillingTransactionDetailID; // (Int64)dRow["nBillingTransactionDetailID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                oPaymentInsuranceLine.BLTransactionLineNo = this.BillingTransactionLineNo; // (Int64)dRow["nBillingTransactionLineNo"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));

                                oPaymentInsuranceLine.TrackBLTransactionID = this.TrackTrnID; // (Int64)dRow["nTrackTrnId"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                oPaymentInsuranceLine.TrackBLTransactionDetailID = this.TrackTrnDtlID; // (Int64)dRow["nTrackTrnDtlId"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                oPaymentInsuranceLine.TrackBLTransactionLineNo = this.BillingTransactionLineNo; // (Int64)dRow["nTrackBLTransactionLineNo"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_LINENO));


                                oPaymentInsuranceLine.ClaimNumber = this.ClaimNo; // (Int64)dRow["nClaimNo"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                                oPaymentInsuranceLine.SubClaimNumber = this.SubclaimNo; // dRow["nSubClaimNo"].ToString(); // Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
                                                              
                                oPaymentInsuranceLine.DOSFrom = gloDateMaster.gloDate.DateAsNumber(this.DOSFrom); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dRow["dDOSFrom"]));
                                if(this.DOSTo !="") 
                                 oPaymentInsuranceLine.DOSTo = gloDateMaster.gloDate.DateAsNumber(this.DOSTo); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dRow["dDOSTo"]));
                                else
                                    oPaymentInsuranceLine.DOSTo = gloDateMaster.gloDate.DateAsNumber(this.DOSFrom);
                                oPaymentInsuranceLine.CPTCode = this.CPT; // Convert.ToString(dRow["sCPTCode"]);
                                oPaymentInsuranceLine.CPTDescription = this.CPTDescription; // Convert.ToString(dRow["sCPTDescription"]);

                                oPaymentInsuranceLine.BLInsuranceID = 0;
                                oPaymentInsuranceLine.BLInsuranceName = "";
                                oPaymentInsuranceLine.BLInsuranceFlag = InsuranceTypeFlag.None;

                                if (this.Charges.Trim() != "")
                                { oPaymentInsuranceLine.Charges = Convert.ToDecimal(this.Charges); oPaymentInsuranceLine.IsNullCharges = false; }

                                if (this.Unit.Trim() != "")
                                { oPaymentInsuranceLine.Unit = Convert.ToDecimal(this.Unit); oPaymentInsuranceLine.IsNullUnit = false; }

                                if (this.TotalCharges.Trim() != "")
                                { oPaymentInsuranceLine.TotalCharges = Convert.ToDecimal(this.TotalCharges); oPaymentInsuranceLine.IsNullTotalCharges = false; }

                                if (this.Allowed.Trim() != "")
                                { oPaymentInsuranceLine.Allowed = Convert.ToDecimal(this.Allowed); oPaymentInsuranceLine.IsNullAllowed = false; }

                                if (this.WriteOff.Trim() != "")
                                { oPaymentInsuranceLine.WriteOff = Convert.ToDecimal(this.WriteOff); oPaymentInsuranceLine.IsNullWriteOff = false; }

                                oPaymentInsuranceLine.NonCovered = 0;

                                if (this.Payment.Trim() != "")
                                { oPaymentInsuranceLine.InsuranceAmount = Convert.ToDecimal(this.Payment); oPaymentInsuranceLine.IsNullInsurance = false; }

                                if (this.Copay.Trim() != "")
                                { oPaymentInsuranceLine.Copay = Convert.ToDecimal(this.Copay); oPaymentInsuranceLine.IsNullCopay = false; }

                                if (this.Deductible.Trim() != "")
                                { oPaymentInsuranceLine.Deductible = Convert.ToDecimal(this.Deductible); oPaymentInsuranceLine.IsNullDeductible = false; }

                                if (this.CoInsurance.Trim() != "")
                                { oPaymentInsuranceLine.CoInsurance = Convert.ToDecimal(this.CoInsurance); oPaymentInsuranceLine.IsNullCoInsurance = false; }

                                if (this.WithHold.Trim() != "")
                                { oPaymentInsuranceLine.Withhold = Convert.ToDecimal(this.WithHold); oPaymentInsuranceLine.IsNullWithhold = false; }

                                oPaymentInsuranceLine.PaymentTrayID = SelectedPaymentTrayID;
                                oPaymentInsuranceLine.PaymentTrayCode = SelectedPaymentTrayCode;
                                oPaymentInsuranceLine.PaymentTrayDesc = SelectedPaymentTray;

                                oPaymentInsuranceLine.InsContactID = nContactID; //SelectedInsuranceCompanyID;
                                oPaymentInsuranceLine.UserID = AppSettings.UserID;
                                oPaymentInsuranceLine.UserName = AppSettings.UserName;
                                oPaymentInsuranceLine.ClinicID = AppSettings.ClinicID;

                                oPaymentInsuranceLine.SVCID = this.SVCId;
                                oPaymentInsuranceLine.CLPID = nCLPId;

                                oPaymentInsuranceLine.IsStopCharge = bIsStopPayment;
                                //if (mskCloseDate.MaskCompleted == true)
                                //{
                                //mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                oPaymentInsuranceLine.CloseDate = this.CloseDate; //gloDateMaster.gloDate.DateAsNumber(oPaymentInsurace.CloseDate.ToString()); //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                //}

                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                            ActivityType.SetLineReasonCodes, "Set Line Reason Codes - Start", ActivityOutCome.Success);

                                #region " Set Line Reason Codes "

                                //...Code added on 20100318 by Sagar Ghodke
                                //...Code added to auto set the adjustment codes for W/O,Copay,Dedutible,Coinsurance,Withhold
                                //...by reading there respective values from admin settings

                                EOBPayment.Common.PaymentInsuranceLineResonCode oPaymentInsuranceLineResonCode = null;
                                string _code = "";

                                // This Hastable object is assigned with database record column name.
                                // Which is used in retrieving column value from a datarow.
                                Hashtable htTab = new Hashtable();
                                htTab.Add(23, "nWriteOff");
                                htTab.Add(24, "nCopay");
                                htTab.Add(25, "nDeductible");
                                htTab.Add(26, "nCoInsurance");
                                htTab.Add(27, "nWithhold");


                                for (int colIndex = COL_WRITEOFF; colIndex <= COL_WITHHOLD; colIndex++)
                                {
                                    _code = "";
                                    //if (c1SinglePayment.GetData(i, colIndex) != null && Convert.ToString(c1SinglePayment.GetData(i, colIndex)).Trim() != ""
                                    //    && Convert.ToDecimal(c1SinglePayment.GetData(i, colIndex)) != 0)
                                    if (dRow[htTab[(object)colIndex].ToString()].ToString().Trim() != "" && Convert.ToDecimal(dRow[htTab[(object)colIndex].ToString()]) !=0)
                                    {
                                        oPaymentInsuranceLineResonCode = new EOBPayment.Common.PaymentInsuranceLineResonCode();
                                        oPaymentInsuranceLineResonCode.ID = 0;
                                        oPaymentInsuranceLineResonCode.ClaimNo = oPaymentInsuranceLine.ClaimNumber;
                                        oPaymentInsuranceLineResonCode.BillingTransactionID = oPaymentInsuranceLine.BLTransactionID;
                                        oPaymentInsuranceLineResonCode.BillingTransactionDetailID = oPaymentInsuranceLine.BLTransactionDetailID;
                                        oPaymentInsuranceLineResonCode.SubClaimNo = oPaymentInsuranceLine.SubClaimNumber;
                                        oPaymentInsuranceLineResonCode.TrackBillingTransactionID = oPaymentInsuranceLine.TrackBLTransactionID;
                                        oPaymentInsuranceLineResonCode.TrackBillingTransactionDetailID = oPaymentInsuranceLine.TrackBLTransactionDetailID;
                                        oPaymentInsuranceLineResonCode.CloseDate = oPaymentInsuranceLine.CloseDate;
                                        oPaymentInsuranceLineResonCode.ClinicID = oPaymentInsuranceLine.ClinicID;
                                        oPaymentInsuranceLineResonCode.EOBPaymentID = 0;
                                        oPaymentInsuranceLineResonCode.EOBID = 0;
                                        oPaymentInsuranceLineResonCode.EOBPaymentDetailID = 0;
                                        oPaymentInsuranceLineResonCode.HasData = true;
                                        oPaymentInsuranceLineResonCode.IsNullReasonAmount = false;

                                        oPaymentInsuranceLineResonCode.ReasonCode = GetSelectedReasonCode(colIndex);

                                        oPaymentInsuranceLineResonCode.ReasonDescription = InsurancePayment.GetReasonDescription(_code);
                                        // Calculate Reason Amount method is required.

                                        oPaymentInsuranceLineResonCode.ReasonAmount = Convert.ToDecimal(dRow[htTab[(object)colIndex].ToString()]);//CalculateReasonAmount(oPaymentInsuranceLineResonCode.ReasonCode);
                                        oPaymentInsuranceLineResonCode.CommentType = EOBCommentType.SystemReasonCode;
                                        oPaymentInsuranceLineResonCode.ReasonCodeSubType = colIndex;
                                        oPaymentInsuranceLine.LineResonCodes.Add(oPaymentInsuranceLineResonCode);
                                        oPaymentInsuranceLineResonCode = null;
                                    }
                                }

                                htTab = null;


                                //...End code add 20100318


                                // Need Clarification on the below code for line reason code - Dev66.
                                // Get Other reason codes.
                                // ERA_GetOtherReasonCodes
                                DataTable dtOther = GetOtherReasonCodes(this.SVCId,false,nResponsibilityNo);
                                
                                if (dtOther != null)
                                {
                                    //bCASOtherReasonCodeExists == false means normal flow
                                    if (dtOther.Rows.Count > 0 && bCASOtherReasonCodeExists == false)
                                    {
                                        foreach (DataRow dtRow in dtOther.Rows)
                                        {
                                            oPaymentInsuranceLineResonCode = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineResonCode();

                                            oPaymentInsuranceLineResonCode.ID = 0;

                                            oPaymentInsuranceLineResonCode.ClaimNo = this.ClaimNo;

                                            oPaymentInsuranceLineResonCode.SubClaimNo = this.SubclaimNo;

                                            oPaymentInsuranceLineResonCode.EOBPaymentID = 0;

                                            oPaymentInsuranceLineResonCode.EOBID = 0;

                                            oPaymentInsuranceLineResonCode.EOBPaymentDetailID = 0;

                                            oPaymentInsuranceLineResonCode.BillingTransactionID = this.BillingTransactionID;

                                            oPaymentInsuranceLineResonCode.BillingTransactionDetailID = this.BillingTransactionDetailID;

                                            oPaymentInsuranceLineResonCode.TrackBillingTransactionID = this.TrackTrnID;

                                            oPaymentInsuranceLineResonCode.TrackBillingTransactionDetailID = this.TrackTrnDtlID;

                                            oPaymentInsuranceLineResonCode.ReasonCode = dtRow["ReasonCode"].ToString();

                                           // oPaymentInsuranceLineResonCode.ReasonDescription = "";
                                            oPaymentInsuranceLineResonCode.ReasonDescription = (InsurancePayment.GetReasonDescription(dtRow["ReasonCode"].ToString()) == "") ? "No Description Available" : InsurancePayment.GetReasonDescription(dtRow["ReasonCode"].ToString());

                                            if (dtRow["ReasonAmount"] != null)
                                            {
                                                if (dtRow["ReasonAmount"].ToString().Trim().Length > 0 && Convert.ToDecimal(dtRow["ReasonAmount"].ToString()) !=0 )
                                                    oPaymentInsuranceLineResonCode.ReasonAmount = Convert.ToDecimal(dtRow["ReasonAmount"].ToString());
                                                oPaymentInsuranceLineResonCode.IsNullReasonAmount = false;
                                            }

                                            oPaymentInsuranceLineResonCode.ClinicID = AppSettings.ClinicID;

                                            oPaymentInsuranceLineResonCode.CloseDate = oPaymentInsurace.CloseDate;
                                            oPaymentInsuranceLineResonCode.HasData = true;
                                            oPaymentInsuranceLineResonCode.CommentType = EOBCommentType.Reason;

                                            oPaymentInsuranceLine.LineResonCodes.Add(oPaymentInsuranceLineResonCode);
                                        }
                                    }
                                }


                                if (bCASOtherReasonCodeExists == true)
                                {
                                    DataTable dtClaimCAS = GetOtherReasonCodes(nCLPId,true,nResponsibilityNo);
                                    DataTable dtOtherCodes = dtOther.Clone();
                                    DataRow oRow = null;
                                    if (dtClaimCAS != null)
                                    {                                        
                                        dtOther.Merge(dtClaimCAS);

                                        DataView _dv = dtOther.DefaultView;
                                        _dv.Sort = "ReasonCode";
                                        dtOther = _dv.ToTable();

                                        string _ReasonCode = "";
                                        Int32 _CurRow = -1;
                                        for (int iRow = 0; iRow < dtOther.Rows.Count; iRow++)
                                        {
                                            _CurRow = -1;
                                            if (_ReasonCode == dtOther.Rows[iRow]["ReasonCode"].ToString())
                                            {
                                                _CurRow = dtOtherCodes.Rows.Count - 1;
                                                dtOtherCodes.Rows[_CurRow]["ReasonAmount"] =
                                                    Convert.ToDouble(dtOtherCodes.Rows[_CurRow]["ReasonAmount"].ToString()) +
                                                    Convert.ToDouble(dtOther.Rows[iRow]["ReasonAmount"].ToString());
                                            }
                                            else
                                            {
                                                _ReasonCode = dtOther.Rows[iRow]["ReasonCode"].ToString();
                                                oRow = dtOtherCodes.NewRow();
                                                oRow["ReasonCode"] = _ReasonCode;
                                                oRow["ReasonAmount"] = dtOther.Rows[iRow]["ReasonAmount"].ToString();
                                                dtOtherCodes.Rows.Add(oRow);
                                            }
                                        }
                                    }
                                    //if(this.OtherReasonAmount.Trim().Length > 0)
                                    foreach (DataRow dtRow in dtOtherCodes.Rows)                                    
                                    {
                                        oPaymentInsuranceLineResonCode = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineResonCode();

                                        oPaymentInsuranceLineResonCode.ID = 0;

                                        oPaymentInsuranceLineResonCode.ClaimNo = this.ClaimNo;

                                        oPaymentInsuranceLineResonCode.SubClaimNo = this.SubclaimNo;

                                        oPaymentInsuranceLineResonCode.EOBPaymentID = 0;

                                        oPaymentInsuranceLineResonCode.EOBID = 0;

                                        oPaymentInsuranceLineResonCode.EOBPaymentDetailID = 0;

                                        oPaymentInsuranceLineResonCode.BillingTransactionID = this.BillingTransactionID;

                                        oPaymentInsuranceLineResonCode.BillingTransactionDetailID = this.BillingTransactionDetailID;

                                        oPaymentInsuranceLineResonCode.TrackBillingTransactionID = this.TrackTrnID;

                                        oPaymentInsuranceLineResonCode.TrackBillingTransactionDetailID = this.TrackTrnDtlID;

                                       // oPaymentInsuranceLineResonCode.ReasonCode = this.sGeneratedOtherReasonCode; //"OA222";
                                        oPaymentInsuranceLineResonCode.ReasonCode = dtRow["ReasonCode"].ToString(); //"OA222";

                                      //  oPaymentInsuranceLineResonCode.ReasonDescription = "";
                                        oPaymentInsuranceLineResonCode.ReasonDescription = (InsurancePayment.GetReasonDescription(dtRow["ReasonCode"].ToString())=="")?"No Description Available":InsurancePayment.GetReasonDescription(dtRow["ReasonCode"].ToString());

                                        //if (this.OtherReasonAmount != null)
                                        //{
                                        //    if (this.OtherReasonAmount.ToString().Trim().Length > 0)
                                        //        oPaymentInsuranceLineResonCode.ReasonAmount = Convert.ToDecimal(this.OtherReasonAmount.ToString());
                                        //    oPaymentInsuranceLineResonCode.IsNullReasonAmount = false;
                                        //}
                                        if (dtRow["ReasonAmount"] != null)
                                        {
                                            if (dtRow["ReasonAmount"].ToString().Trim().Length > 0 && Convert.ToDecimal(dtRow["ReasonAmount"].ToString()) != 0)
                                                oPaymentInsuranceLineResonCode.ReasonAmount = Convert.ToDecimal(dtRow["ReasonAmount"].ToString());
                                            oPaymentInsuranceLineResonCode.IsNullReasonAmount = false;
                                        }
                                        oPaymentInsuranceLineResonCode.ClinicID = AppSettings.ClinicID;

                                        oPaymentInsuranceLineResonCode.CloseDate = oPaymentInsurace.CloseDate;
                                        oPaymentInsuranceLineResonCode.HasData = true;
                                        oPaymentInsuranceLineResonCode.CommentType = EOBCommentType.Reason;

                                        oPaymentInsuranceLine.LineResonCodes.Add(oPaymentInsuranceLineResonCode);
                                    }
                                    
                                }
                                #endregion " Set Line Reason Codes "

                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                             ActivityType.SetLineReasonCodes, "Set Line Reason Codes - End", ActivityOutCome.Success);


                                #region " Statement Notes & Internal Notes for Line "

                                //if (c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTE)).Trim() != "")
                                if (false) // Method required to verify and get line statement notes.
                                {
                                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                             ActivityType.StatementNotes, "Statement Notes - Start", ActivityOutCome.Success);

                                    EOBPayment.Common.PaymentInsuranceLineNote oPaymentInsuranceLineNote = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineNote();

                                    oPaymentInsuranceLineNote.ClaimNo = this.ClaimNo; // (Int64)dRow["nClaimNo"]; // Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                                    oPaymentInsuranceLineNote.SubClaimNo = this.SubclaimNo; // Convert.ToString(dRow["nSubClaimNo"]);
                                    oPaymentInsuranceLineNote.EOBPaymentID = this.EOBPaymentID; //oPaymentInsurace.EOBPaymentID; //_EOBPaymentID; //CheckDev66
                                    oPaymentInsuranceLineNote.EOBID = 0;
                                    oPaymentInsuranceLineNote.EOBPaymentDetailID = 0;

                                    oPaymentInsuranceLineNote.BillingTransactionID = this.BillingTransactionID; // (Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                    oPaymentInsuranceLineNote.BillingTransactionDetailID = this.BillingTransactionDetailID; // (Int64)dRow["nBillingTransactionDetailID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));

                                    oPaymentInsuranceLineNote.TrackBillingTransactionID = this.TrackTrnID; // (Int64)dRow["nTrackBLTransactionID"];  //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                    oPaymentInsuranceLineNote.TrackBillingTransactionDetailID = this.TrackTrnDtlID; // (Int64)dRow["nTrackBLTransactionDetailID"];  //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));

                                    oPaymentInsuranceLineNote.Code = "";
                                    // Dev66 - Required clarification.
                                    oPaymentInsuranceLineNote.Description = ""; // Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTE)).Trim();
                                    oPaymentInsuranceLineNote.Amount = 0;
                                    //////if (c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTEONPRINT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTEONPRINT)).Trim() != "")
                                    //////{ oPaymentInsuranceLineNote.IncludeOnPrint = Convert.ToBoolean(c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTEONPRINT)); }

                                    oPaymentInsuranceLineNote.IncludeOnPrint = false;

                                    oPaymentInsuranceLineNote.ClinicID = AppSettings.ClinicID;
                                    oPaymentInsuranceLineNote.PaymentNoteType = EOBPaymentType.InsuracePayment;
                                    oPaymentInsuranceLineNote.PaymentNoteSubType = EOBPaymentSubType.StatementNote;
                                    oPaymentInsuranceLineNote.HasData = true;
                                    oPaymentInsuranceLineNote.CloseDate = this.CloseDate;//oPaymentInsurace.CloseDate;
                                    oPaymentInsuranceLineNote.UserId = AppSettings.UserID;

                                    oPaymentInsuranceLine.LineNotes.Add(oPaymentInsuranceLineNote);
                                    //oPaymentInsurace.Notes.Add(oPaymentInsuranceLineNote);
                                    oPaymentInsuranceLineNote.Dispose();
                                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                             ActivityType.StatementNotes, "Statement Notes - End", ActivityOutCome.Success);
                                }

                                ////if (c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTE)).Trim() != "")
                                if (false) // Method required to verify and get line Internal notes.
                                {
                                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                             ActivityType.InternalNotes, "Internal Notes - Start", ActivityOutCome.Success);

                                    EOBPayment.Common.PaymentInsuranceLineNote oPaymentInsuranceLineNote = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineNote();

                                    oPaymentInsuranceLineNote.ClaimNo = this.ClaimNo;// (Int64)dRow["nClaimNo"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                                    oPaymentInsuranceLineNote.SubClaimNo = this.SubclaimNo;// Convert.ToString(dRow["nSubClaimNo"]); //Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));

                                    oPaymentInsuranceLineNote.EOBPaymentID = this.EOBPaymentID; // oPaymentInsurace.EOBPaymentID; //_EOBPaymentID; //CheckDev66
                                    oPaymentInsuranceLineNote.EOBID = 0;
                                    oPaymentInsuranceLineNote.EOBPaymentDetailID = 0;

                                    oPaymentInsuranceLineNote.BillingTransactionID = this.BillingTransactionID;// (Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                    oPaymentInsuranceLineNote.BillingTransactionDetailID = this.BillingTransactionDetailID; // (Int64)dRow["nBillingTransactionDetailID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));

                                    oPaymentInsuranceLineNote.TrackBillingTransactionID = this.TrackTrnID; // (Int64)dRow["nTrackBLTransactionID"];  //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                    oPaymentInsuranceLineNote.TrackBillingTransactionDetailID = this.TrackTrnID;// (Int64)dRow["nTrackBLTransactionDetailID"];  //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));

                                    oPaymentInsuranceLineNote.Code = "";
                                    // Dev66 - Required clarification.
                                    oPaymentInsuranceLineNote.Description = "";// Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTE)).Trim();
                                    oPaymentInsuranceLineNote.Amount = 0;
                                    //if (c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTEONPRINT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTEONPRINT)).Trim() != "")
                                    //{ oPaymentInsuranceLineNote.IncludeOnPrint = Convert.ToBoolean(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTEONPRINT)); }

                                    oPaymentInsuranceLineNote.IncludeOnPrint = false; // Convert.ToBoolean(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTEONPRINT)); 

                                    oPaymentInsuranceLineNote.ClinicID = AppSettings.ClinicID;
                                    oPaymentInsuranceLineNote.PaymentNoteType = EOBPaymentType.InsuracePayment;
                                    oPaymentInsuranceLineNote.PaymentNoteSubType = EOBPaymentSubType.InternalNote;
                                    oPaymentInsuranceLineNote.HasData = true;
                                    oPaymentInsuranceLineNote.CloseDate = this.CloseDate;// oPaymentInsurace.CloseDate;
                                    oPaymentInsuranceLineNote.UserId = AppSettings.UserID;

                                    //oPaymentInsurace.Notes.Add(oPaymentInsuranceLineNote);
                                    oPaymentInsuranceLine.LineNotes.Add(oPaymentInsuranceLineNote);
                                    oPaymentInsuranceLineNote.Dispose();

                                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                             ActivityType.InternalNotes, "Internal Notes - End", ActivityOutCome.Success);
                                }

                                #endregion " Statement Notes & Internal Notes for Line "

                                oPaymentInsuranceLine.InsCompanyID = SelectedInsuranceCompanyID; //SelectedInsuranceCompanyID;//Convert.ToInt64(lblInsCompany.Tag);

                                #endregion

                                #region "Debit Service Line - Insurance - Allocation from Current Check, Correction/Takeback, Use Reserved"

                                string Amt_Payment = this.Payment;
                                string Amt_Last_Payment = GetLastPayment(); // "0.00";

                                // Verify Payment amount is available or not
                                // Verify Last Payment amount is available or not
                                // Do subtraction(Check for the payment amount is not zero or less than zero).

                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                             ActivityType.DebitServiceLine, "Debit Service Line - Start", ActivityOutCome.Success);

                                //if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PAYMENT)).Trim() != "")
                                if (Amt_Payment.Trim() != "") // check for payment is available
                                {
                                    //if (c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_PAYMENT)).Trim() != "")
                                    if (Amt_Last_Payment.Trim() != "") // check for last payment is available
                                    {

                                        //..Code changes done by sagar ghodke .. on 20100322 to resolve save of zero payment debit line
                                        //below commented condition is previous
                                        //if ((Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT))) > 0)
                                        //if ((Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT))) >= 0)

                                        // Verify the difference is Greater than zero.
                                        // Calculate difference between (payment) - (last payment)
                                        if ((Convert.ToDecimal(Amt_Payment) - Convert.ToDecimal(Amt_Last_Payment)) >= 0)
                                        {
                                            decimal _fillPayAmt = 0; decimal _fillResAmt = 0;
                                            Int64 _fillResPayID = 0; Int64 _fillResPayDtlID = 0;
                                            Int64 _fillRefPayID = 0; Int64 _fillRefPayDtlID = 0;
                                            //int _fillrPayIndx = -1;
                                            int _fillRefFinanceLieNo = 0;
                                            bool _fillUseRefFinanceLieNo = false;
                                            bool _isNullfillPayAmt = false;


                                            //if no correction then direct current new amount
                                            //if negative correction then it will not come in this loop
                                            //if positive correction then only correction amount, but in grid user will enter total amount not correction amount
                                            //thats why below we have to calculate amount = last amount - current payment


                                            //Code commented by Dev66
                                            /*
                                            if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null)
                                            { _fillPayAmt = (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT))); }
                                            else if (c1SinglePayment.GetData(i, COL_PAYMENT) != null)
                                            { _fillPayAmt = (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT))); }
                                            else if (c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null)
                                            { _fillPayAmt = 0 - (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT))); }
                                            else
                                            { _isNullfillPayAmt = true; }
                                            */


                                            if (Convert.ToDecimal(Amt_Payment) > 0 && Convert.ToDecimal(Amt_Last_Payment) > 0)
                                            { _fillPayAmt = Convert.ToDecimal(Amt_Payment) - Convert.ToDecimal(Amt_Last_Payment); }
                                            else if (Convert.ToDecimal(Amt_Payment) > 0)
                                            { _fillPayAmt = (Convert.ToDecimal(Amt_Payment)); }
                                            else if (Convert.ToDecimal(Amt_Last_Payment) > 0)
                                            { _fillPayAmt = 0 - (Convert.ToDecimal(Amt_Last_Payment)); }
                                            else
                                            { _isNullfillPayAmt = true; } 





                                            int rPay = 0; //we have to always allocate against check, so rPay value set 0 as its first line in collection
                                            _fillResAmt = EOBInsurancePaymentMasterAllocationLines[rPay].Amount;

                                            //..Code changes done by Sagar Ghodke on 20100511
                                            //..Code changes done to make correct debit entries here unnecessary resid 
                                            //..where passed even if the amount is not used from the reserve
                                            //..below commented code lines are existing logic

                                            //_fillResPayID = EOBInsurancePaymentMasterAllocationLines[rPay].EOBPaymentID;
                                            //_fillResPayDtlID = EOBInsurancePaymentMasterAllocationLines[rPay].EOBPaymentDetailID;

                                            _fillResPayID = EOBInsurancePaymentMasterAllocationLines[rPay].ReserveEOBPaymentID;
                                            _fillResPayDtlID = EOBInsurancePaymentMasterAllocationLines[rPay].ReserveEOBPaymentDetailID;

                                            //..End code changes done by Sagar Ghodke on 20100511

                                            _fillRefPayID = EOBInsurancePaymentMasterAllocationLines[rPay].RefEOBPaymentID;
                                            _fillRefPayDtlID = EOBInsurancePaymentMasterAllocationLines[rPay].RefEOBPaymentDetailID;

                                            _fillRefFinanceLieNo = EOBInsurancePaymentMasterAllocationLines[rPay].FinanceLieNo;
                                            if (_fillRefPayID == 0 && _fillRefPayDtlID == 0)
                                            {
                                                _fillUseRefFinanceLieNo = true;
                                            }

                                            #region "Set object"

                                            oEOBInsurancePaymentDetail = new EOBPayment.Common.EOBInsurancePaymentDetail();
                                            oEOBInsurancePaymentDetail.EOBPaymentID = 0;
                                            oEOBInsurancePaymentDetail.EOBID = 0;
                                            oEOBInsurancePaymentDetail.EOBDtlID = 0;
                                            oEOBInsurancePaymentDetail.EOBPaymentDetailID = 0;

                                            oEOBInsurancePaymentDetail.BillingTransactionID = this.BillingTransactionID;// (Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                            oEOBInsurancePaymentDetail.BillingTransactionDetailID = this.BillingTransactionDetailID; // (Int64)dRow["nBillingTransactionDetailID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                            oEOBInsurancePaymentDetail.BillingTransactionLineNo = (Int32)this.BillingTransactionLineNo; // (Int32)dRow["nBillingTransactionLineNo"]; //Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));

                                            oEOBInsurancePaymentDetail.TrackBillingTransactionID = this.TrackTrnID;// (Int64)dRow["nTrackBLTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                            oEOBInsurancePaymentDetail.TrackBillingTransactionDetailID = this.TrackTrnDtlID;// (Int64)dRow["nTrackBLTransactionDetailID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                            oEOBInsurancePaymentDetail.TrackBillingTransactionLineNo = (Int32)this.BillingTransactionLineNo; // (Int32)dRow["nTrackBLTransactionLineNo"]; //Convert.ToInt32(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_LINENO));
                                            oEOBInsurancePaymentDetail.SubClaimNo = this.SubclaimNo;// (string)dRow["nSubClaimNo"]; //Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));

                                            oEOBInsurancePaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(this.DOSFrom); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dRow["dDOSFrom"])); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                            oEOBInsurancePaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(this.DOSTo); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dRow["dDOSTo"]));//gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
                                            oEOBInsurancePaymentDetail.CPTCode = this.CPT; // Convert.ToString(dRow["sCPTCode"]);
                                            oEOBInsurancePaymentDetail.CPTDescription = this.CPTDescription; // Convert.ToString(dRow["sCPTDescription"]);

                                            oEOBInsurancePaymentDetail.Amount = _fillPayAmt;
                                            oEOBInsurancePaymentDetail.IsNullAmount = _isNullfillPayAmt;

                                            oEOBInsurancePaymentDetail.PaymentType = EOBPaymentType.InsuracePayment;
                                            oEOBInsurancePaymentDetail.PaymentSubType = EOBPaymentSubType.Insurace;
                                            oEOBInsurancePaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                            oEOBInsurancePaymentDetail.PayMode = this.PaymentMode;//solving sales force case -GLO2011-0010771

                                        
                                            oEOBInsurancePaymentDetail.RefEOBPaymentID = _fillRefPayID;
                                            oEOBInsurancePaymentDetail.RefEOBPaymentDetailID = _fillRefPayDtlID;
                                            oEOBInsurancePaymentDetail.ReserveEOBPaymentID = _fillResPayID;
                                            oEOBInsurancePaymentDetail.ReserveEOBPaymentDetailID = _fillResPayDtlID;

                                            oEOBInsurancePaymentDetail.OldRefEOBPaymentID = _fillRefPayID;
                                            oEOBInsurancePaymentDetail.OldRefEOBPaymentDetailID = _fillRefPayDtlID;
                                            oEOBInsurancePaymentDetail.OldReserveEOBPaymentID = _fillResPayID;
                                            oEOBInsurancePaymentDetail.OldReserveEOBPaymentDetailID = _fillResPayDtlID;

                                            //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                            oEOBInsurancePaymentDetail.AccountID = SelectedInsuranceCompanyID; //Convert.ToInt64(lblInsCompany.Tag);
                                            oEOBInsurancePaymentDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
                                            oEOBInsurancePaymentDetail.MSTAccountID = PatientInsuranceID; // Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                            oEOBInsurancePaymentDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
                                            oEOBInsurancePaymentDetail.ContactInsID = nContactID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));

                                            oEOBInsurancePaymentDetail.PatientID = this.PatientID;// (Int64)dRow["nPatientID"]; //PatientControl.PatientID;
                                            oEOBInsurancePaymentDetail.PaymentTrayID = SelectedPaymentTrayID;
                                            oEOBInsurancePaymentDetail.PaymentTrayCode = SelectedPaymentTrayCode;
                                            oEOBInsurancePaymentDetail.PaymentTrayDescription = SelectedPaymentTray;
                                            oEOBInsurancePaymentDetail.UserID = AppSettings.UserID;
                                            oEOBInsurancePaymentDetail.UserName = AppSettings.UserName;
                                            oEOBInsurancePaymentDetail.ClinicID = AppSettings.ClinicID;

                                            oEOBInsurancePaymentDetail.FinanceLieNo = 0;
                                            oEOBInsurancePaymentDetail.MainCreditLineID = 0;
                                            oEOBInsurancePaymentDetail.IsMainCreditLine = false;
                                            oEOBInsurancePaymentDetail.IsReserveCreditLine = false;
                                            oEOBInsurancePaymentDetail.IsCorrectionCreditLine = false;
                                            oEOBInsurancePaymentDetail.RefFinanceLieNo = _fillRefFinanceLieNo;
                                            oEOBInsurancePaymentDetail.UseRefFinanceLieNo = _fillUseRefFinanceLieNo;

                                            //if (mskCloseDate.MaskCompleted == true)
                                            //{
                                            //mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                            oEOBInsurancePaymentDetail.CloseDate = this.CloseDate; //gloDateMaster.gloDate.DateAsNumber(oPaymentInsurace.CloseDate.ToString()); //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                            //}

                                            oPaymentInsuranceLine.EOBInsurancePaymentLineDetails.Add(oEOBInsurancePaymentDetail);
                                            oEOBInsurancePaymentDetail.Dispose();


                                            #endregion
                                        }
                                    }
                                }

                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                             ActivityType.DebitServiceLine, "Debit Service Line - End", ActivityOutCome.Success);
                                #endregion

                                _Add_WO_WH = false;

                                #region "Debit Service Line - WriteOff"

                                //if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null)
                              //  if (dRow["nWriteOff"] != null && (nClaimStatus == 1 || nClaimStatus == 19 || bSecondaryAdjudication || nResponsibilityNo==1))  //bSecondaryAdjudication
                                if (dRow["nWriteOff"] != null && ( bSecondaryAdjudication || nResponsibilityNo == 1))  //bSecondaryAdjudication
                                {
                                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                             ActivityType.DebitServiceLineWriteOff, "Debit Service Line WriteOff - Start ", ActivityOutCome.Success);

                                    oEOBInsurancePaymentDetail = new EOBPayment.Common.EOBInsurancePaymentDetail();
                                    oEOBInsurancePaymentDetail.EOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.EOBID = 0;
                                    oEOBInsurancePaymentDetail.EOBDtlID = 0;
                                    oEOBInsurancePaymentDetail.EOBPaymentDetailID = 0;

                                    oEOBInsurancePaymentDetail.BillingTransactionID = this.BillingTransactionID; // (Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                    oEOBInsurancePaymentDetail.BillingTransactionDetailID = this.BillingTransactionDetailID; // (Int64)dRow["nBillingTransactionDetailID"];//Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                    oEOBInsurancePaymentDetail.BillingTransactionLineNo = (Int32)this.BillingTransactionLineNo; // (Int32)dRow["nBillingTransactionLineNo"];//Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));

                                    oEOBInsurancePaymentDetail.SubClaimNo = this.SubclaimNo; // (string)dRow["nSubClaimNo"];//Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
                                    oEOBInsurancePaymentDetail.TrackBillingTransactionID = this.TrackTrnID;// (Int64)dRow["nTrackBLTransactionID"];//Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                    oEOBInsurancePaymentDetail.TrackBillingTransactionDetailID = this.TrackTrnDtlID; // (Int64)dRow["nTrackBLTransactionDetailID"];//Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                    oEOBInsurancePaymentDetail.TrackBillingTransactionLineNo = (Int32)this.BillingTransactionLineNo;// (Int32)dRow["nTrackBLTransactionLineNo"];//Convert.ToInt32(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_LINENO));

                                    oEOBInsurancePaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(this.DOSFrom);  //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dRow["dDOSFrom"])); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                    if(this.DOSTo !="")
                                    oEOBInsurancePaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(this.DOSTo); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dRow["dDOSTo"])); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
                                    else
                                    oEOBInsurancePaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(this.DOSFrom);
                                    oEOBInsurancePaymentDetail.CPTCode = this.CPT; // Convert.ToString(dRow["sCPTCode"]);
                                    oEOBInsurancePaymentDetail.CPTDescription = this.CPTDescription; // Convert.ToString(dRow["sCPTDescription"]);

                                    //if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null && c1SinglePayment.GetData(i, COL_WRITEOFF).ToString().Trim() != "")
                                    //{
                                    //    if (c1SinglePayment.GetData(i, COL_ISCORRECTION) != null && Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISCORRECTION)) == true)
                                    //    {
                                    //        oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF))) - Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WRITEOFF)));
                                    //        if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF))) != Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WRITEOFF))))
                                    //        {
                                    //            _Add_WO_WH = true;
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF)));
                                    //        _Add_WO_WH = true;
                                    //    }
                                    //}

                                    if (this.WriteOff.Trim() != "")
                                    {
                                        if (VerifyPaymentCorrection() == true)
                                        {
                                            decimal _last_WriteOff = GetLastWriteOff();
                                            oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(this.WriteOff) - _last_WriteOff;
                                            if (Convert.ToDecimal(this.WriteOff) != _last_WriteOff)
                                            {
                                                _Add_WO_WH = true;
                                            }
                                        }
                                        else
                                        {
                                            oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(this.WriteOff);
                                            _Add_WO_WH = true;
                                        }
                                    }

                                    oEOBInsurancePaymentDetail.PaymentType = EOBPaymentType.InsuracePayment;
                                    oEOBInsurancePaymentDetail.PaymentSubType = EOBPaymentSubType.WriteOff;
                                    oEOBInsurancePaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                    oEOBInsurancePaymentDetail.PayMode = this.PaymentMode; //SelectedPaymentMode;//solving sales force case -GLO2011-0010771
                                

                                    oEOBInsurancePaymentDetail.RefEOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.RefEOBPaymentDetailID = 0;
                                    oEOBInsurancePaymentDetail.OldRefEOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.OldRefEOBPaymentDetailID = 0;

                                    oEOBInsurancePaymentDetail.ReserveEOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.ReserveEOBPaymentDetailID = 0;
                                    oEOBInsurancePaymentDetail.OldReserveEOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.OldReserveEOBPaymentDetailID = 0;

                                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                    oEOBInsurancePaymentDetail.AccountID = SelectedInsuranceCompanyID; //Convert.ToInt64(lblInsCompany.Tag);
                                    oEOBInsurancePaymentDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
                                    oEOBInsurancePaymentDetail.MSTAccountID = PatientInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                    oEOBInsurancePaymentDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
                                    oEOBInsurancePaymentDetail.ContactInsID = nContactID;//Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));

                                    oEOBInsurancePaymentDetail.PatientID = this.PatientID; // (Int64)dRow["nPatientID"]; //PatientControl.PatientID;
                                    oEOBInsurancePaymentDetail.PaymentTrayID = SelectedPaymentTrayID;
                                    oEOBInsurancePaymentDetail.PaymentTrayCode = SelectedPaymentTrayCode;
                                    oEOBInsurancePaymentDetail.PaymentTrayDescription = SelectedPaymentTray;
                                    oEOBInsurancePaymentDetail.UserID = AppSettings.UserID;
                                    oEOBInsurancePaymentDetail.UserName = AppSettings.UserName;
                                    oEOBInsurancePaymentDetail.ClinicID = AppSettings.ClinicID;

                                    oEOBInsurancePaymentDetail.FinanceLieNo = 0;
                                    oEOBInsurancePaymentDetail.MainCreditLineID = 0;
                                    oEOBInsurancePaymentDetail.IsMainCreditLine = false;
                                    oEOBInsurancePaymentDetail.IsReserveCreditLine = false;
                                    oEOBInsurancePaymentDetail.IsCorrectionCreditLine = false;
                                    oEOBInsurancePaymentDetail.RefFinanceLieNo = 0;
                                    oEOBInsurancePaymentDetail.UseRefFinanceLieNo = false;

                                    //if (mskCloseDate.MaskCompleted == true)
                                    //{
                                    //mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                    oEOBInsurancePaymentDetail.CloseDate = this.CloseDate; //gloDateMaster.gloDate.DateAsNumber(this.CloseDate.ToString()); //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                    //}

                                    if (_Add_WO_WH == true)
                                    {
                                        oPaymentInsuranceLine.EOBInsurancePaymentLineDetails.Add(oEOBInsurancePaymentDetail);
                                    }
                                    oEOBInsurancePaymentDetail.Dispose();

                                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                             ActivityType.DebitServiceLineWriteOff, "Debit Service Line WriteOff - End ", ActivityOutCome.Success);
                                }
                                #endregion

                                _Add_WO_WH = false;

                                #region "Debit Service Line - WithHold"

                                //if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null)
                              //  if (dRow["nWithhold"] != null && (nClaimStatus == 1 || nClaimStatus == 19 || bSecondaryAdjudication))
                                if (dRow["nWithhold"] != null && (bSecondaryAdjudication || nResponsibilityNo == 1))
                                {
                                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                             ActivityType.DebitServiceLineWithHold, "Debit Service Line WithHold - Start ", ActivityOutCome.Success);

                                    oEOBInsurancePaymentDetail = new EOBPayment.Common.EOBInsurancePaymentDetail();
                                    oEOBInsurancePaymentDetail.EOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.EOBID = 0;
                                    oEOBInsurancePaymentDetail.EOBDtlID = 0;
                                    oEOBInsurancePaymentDetail.EOBPaymentDetailID = 0;

                                    oEOBInsurancePaymentDetail.BillingTransactionID = this.BillingTransactionID;// (Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                    oEOBInsurancePaymentDetail.BillingTransactionDetailID = this.BillingTransactionDetailID; // (Int64)dRow["nBillingTransactionDetailID"];//Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                    oEOBInsurancePaymentDetail.BillingTransactionLineNo = (Int32)this.BillingTransactionLineNo; // (Int32)dRow["nBillingTransactionLineNo"];//Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));

                                    oEOBInsurancePaymentDetail.SubClaimNo = this.SubclaimNo; // (string)dRow["nSubClaimNo"];//Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
                                    oEOBInsurancePaymentDetail.TrackBillingTransactionID = this.TrackTrnID;// (Int64)dRow["nTrackBLTransactionID"];//Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                    oEOBInsurancePaymentDetail.TrackBillingTransactionDetailID = this.TrackTrnDtlID;// (Int64)dRow["nTrackBLTransactionDetailID"];//Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                    oEOBInsurancePaymentDetail.TrackBillingTransactionLineNo = (Int32)this.BillingTransactionLineNo; // (Int32)dRow["nTrackBLTransactionLineNo"];//Convert.ToInt32(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_LINENO));

                                    oEOBInsurancePaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(this.DOSFrom); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                    if(this.DOSTo !="")
                                    oEOBInsurancePaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(this.DOSTo); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
                                    else
                                    oEOBInsurancePaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(this.DOSFrom);
                                    oEOBInsurancePaymentDetail.CPTCode = this.CPT; // Convert.ToString(dRow["sCPTCode"]);  //Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                    oEOBInsurancePaymentDetail.CPTDescription = this.CPTDescription; // Convert.ToString(dRow["sCPTDescription"]); //Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

                                    //if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null && c1SinglePayment.GetData(i, COL_WITHHOLD).ToString().Trim() != "")
                                    //{
                                    //    if (c1SinglePayment.GetData(i, COL_ISCORRECTION) != null && Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISCORRECTION)) == true)
                                    //    {
                                    //        oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD))) - Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WITHHOLD)));
                                    //        if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD))) != Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WITHHOLD))))
                                    //        {
                                    //            _Add_WO_WH = true;
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD)));
                                    //        _Add_WO_WH = true;
                                    //    }
                                    //}


                                    if (this.WithHold.Trim() != "")
                                    {
                                        if (VerifyPaymentCorrection() == true)
                                        {
                                            decimal _last_WithHold = GetLastWithHold();
                                            oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(this.WithHold) - _last_WithHold;
                                            if (Convert.ToDecimal(this.WithHold) != _last_WithHold)
                                            {
                                                _Add_WO_WH = true;
                                            }
                                        }
                                        else
                                        {
                                            oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(this.WithHold);
                                            _Add_WO_WH = true;
                                        }
                                    }


                                    oEOBInsurancePaymentDetail.PaymentType = EOBPaymentType.InsuracePayment;
                                    oEOBInsurancePaymentDetail.PaymentSubType = EOBPaymentSubType.WithHold;
                                    oEOBInsurancePaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                    oEOBInsurancePaymentDetail.PayMode = this.PaymentMode; //SelectedPaymentMode;//solving sales force case -GLO2011-0010771
                               


                                    oEOBInsurancePaymentDetail.RefEOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.RefEOBPaymentDetailID = 0;
                                    oEOBInsurancePaymentDetail.OldRefEOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.OldRefEOBPaymentDetailID = 0;
                                    oEOBInsurancePaymentDetail.ReserveEOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.ReserveEOBPaymentDetailID = 0;
                                    oEOBInsurancePaymentDetail.OldReserveEOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.OldReserveEOBPaymentDetailID = 0;

                                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                    oEOBInsurancePaymentDetail.AccountID = SelectedInsuranceCompanyID; // Convert.ToInt64(lblInsCompany.Tag);
                                    oEOBInsurancePaymentDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
                                    oEOBInsurancePaymentDetail.MSTAccountID = PatientInsuranceID; // Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                    oEOBInsurancePaymentDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
                                    oEOBInsurancePaymentDetail.ContactInsID = nContactID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));

                                    oEOBInsurancePaymentDetail.PatientID = this.PatientID; // (Int64)dRow["nPatientID"]; //PatientControl.PatientID;
                                    oEOBInsurancePaymentDetail.PaymentTrayID = SelectedPaymentTrayID;
                                    oEOBInsurancePaymentDetail.PaymentTrayCode = SelectedPaymentTrayCode;
                                    oEOBInsurancePaymentDetail.PaymentTrayDescription = SelectedPaymentTray;
                                    oEOBInsurancePaymentDetail.UserID = AppSettings.UserID;
                                    oEOBInsurancePaymentDetail.UserName = AppSettings.UserName;
                                    oEOBInsurancePaymentDetail.ClinicID = AppSettings.ClinicID;

                                    oEOBInsurancePaymentDetail.FinanceLieNo = 0;
                                    oEOBInsurancePaymentDetail.MainCreditLineID = 0;
                                    oEOBInsurancePaymentDetail.IsMainCreditLine = false;
                                    oEOBInsurancePaymentDetail.IsReserveCreditLine = false;
                                    oEOBInsurancePaymentDetail.IsCorrectionCreditLine = false;
                                    oEOBInsurancePaymentDetail.RefFinanceLieNo = 0;
                                    oEOBInsurancePaymentDetail.UseRefFinanceLieNo = false;

                                    //if (mskCloseDate.MaskCompleted == true)
                                    //{
                                    //mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                    oEOBInsurancePaymentDetail.CloseDate = this.CloseDate; //gloDateMaster.gloDate.DateAsNumber(this.CloseDate.ToString());  //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                    //}

                                    if (_Add_WO_WH == true)
                                    {
                                        oPaymentInsuranceLine.EOBInsurancePaymentLineDetails.Add(oEOBInsurancePaymentDetail);
                                    }
                                    oEOBInsurancePaymentDetail.Dispose();

                                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                        ActivityType.DebitServiceLineWithHold, "Debit Service Line WithHold - End ", ActivityOutCome.Success);

                                }
                                #endregion

                                // Added by Dev66 on 28-June-2010
                                // Called this method to set the NextAction & SplitClaim details
                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                     ActivityType.SetNextActionDetails, "SetNextActionDetails - Start ", ActivityOutCome.Success);

                                SetNextActionDetails(out oNextActions, out oSplitClaimDtls);

                                if (oNextActions.Count > 0)
                                {
                                    oPaymentInsuranceLine.LineNextAction = oNextActions[0];
                                    oListNextActions.Add(oNextActions[0]);
                                }
                                    
                                // End

                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                     ActivityType.SetNextActionDetails, "SetNextActionDetails - End ", ActivityOutCome.Success);

                                oPaymentInsuranceClaim.CliamLines.Add(oPaymentInsuranceLine);
                                oPaymentInsuranceLine.Dispose();
                                //////        }
                                //////    }
                                //////}

                                if (oEOBInsurancePaymentDetail != null) { oEOBInsurancePaymentDetail.Dispose(); }

                                #endregion

                                oPaymentInsurace.InsuranceClaims.Add(oPaymentInsuranceClaim);
                                oPaymentInsuranceClaim.Dispose();
                            }

                            #endregion " ......................... Claim Payment Details End ................................. "


                            #region "Reserve Debit Entry if any and it will goes directlly to payment object with credit line"

                            ////////if (IsReserveAdded()) //Check if there is any reserve entry to be made
                            ////////{
                            ////////    decimal _reserveAmt = 0;
                            ////////    //string _reserveNote = "";

                            ////////    _reserveAmt = AmountAddedToReserve();

                            ////////    //0 ReserveAmount 
                            ////////    //1 ReserveNote 
                            ////////    //3 ReserveNoteOnPrint 

                            ////////    //if (AmountAddedToReserve > 0) //Check for the reserve amount is greater than zero
                            ////////    if (_reserveAmt > 0)
                            ////////    {
                            ////////        oEOBInsurancePaymentReserveDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
                            ////////        oEOBInsurancePaymentReserveDetail.EOBPaymentID = 0;
                            ////////        oEOBInsurancePaymentReserveDetail.EOBID = 0;
                            ////////        oEOBInsurancePaymentReserveDetail.EOBDtlID = 0;
                            ////////        oEOBInsurancePaymentReserveDetail.EOBPaymentDetailID = 0;

                            ////////        oEOBInsurancePaymentReserveDetail.BillingTransactionID = 0;
                            ////////        oEOBInsurancePaymentReserveDetail.BillingTransactionDetailID = 0;
                            ////////        oEOBInsurancePaymentReserveDetail.BillingTransactionLineNo = 0;

                            ////////        oEOBInsurancePaymentReserveDetail.TrackBillingTransactionID = 0;
                            ////////        oEOBInsurancePaymentReserveDetail.TrackBillingTransactionDetailID = 0;
                            ////////        oEOBInsurancePaymentReserveDetail.TrackBillingTransactionLineNo = 0;
                            ////////        oEOBInsurancePaymentReserveDetail.SubClaimNo = "";

                            ////////        //if (mskCloseDate.MaskCompleted == true)
                            ////////        //{
                            ////////            //mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                            ////////        oEOBInsurancePaymentReserveDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(oPaymentInsurace.CloseDate.ToString());  //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                            ////////        oEOBInsurancePaymentReserveDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(oPaymentInsurace.CloseDate.ToString()); //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                            ////////        //}
                            ////////        oEOBInsurancePaymentReserveDetail.CPTCode = "";
                            ////////        oEOBInsurancePaymentReserveDetail.CPTDescription = "";

                            ////////        oEOBInsurancePaymentReserveDetail.Amount = _reserveAmt;
                            ////////        oEOBInsurancePaymentReserveDetail.IsNullAmount = false;

                            ////////        oEOBInsurancePaymentReserveDetail.PaymentType = EOBPaymentType.InsuraceReserverd;
                            ////////        oEOBInsurancePaymentReserveDetail.PaymentSubType = EOBPaymentSubType.Reserved;
                            ////////        oEOBInsurancePaymentReserveDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                            ////////        oEOBInsurancePaymentReserveDetail.PayMode = EOBPaymentMode.Check; //SelectedPaymentMode;

                            ////////        oEOBInsurancePaymentReserveDetail.RefEOBPaymentID = oEOBInsPaymentCreditDetail.RefEOBPaymentID;
                            ////////        oEOBInsurancePaymentReserveDetail.RefEOBPaymentDetailID = oEOBInsPaymentCreditDetail.RefEOBPaymentDetailID;
                            ////////        oEOBInsurancePaymentReserveDetail.ReserveEOBPaymentID = oEOBInsPaymentCreditDetail.ReserveEOBPaymentID;
                            ////////        oEOBInsurancePaymentReserveDetail.ReserveEOBPaymentDetailID = oEOBInsPaymentCreditDetail.ReserveEOBPaymentDetailID;

                            ////////        oEOBInsurancePaymentReserveDetail.OldRefEOBPaymentID = oEOBInsPaymentCreditDetail.RefEOBPaymentID;
                            ////////        oEOBInsurancePaymentReserveDetail.OldRefEOBPaymentDetailID = oEOBInsPaymentCreditDetail.RefEOBPaymentDetailID;
                            ////////        oEOBInsurancePaymentReserveDetail.OldReserveEOBPaymentID = oEOBInsPaymentCreditDetail.ReserveEOBPaymentID;
                            ////////        oEOBInsurancePaymentReserveDetail.OldReserveEOBPaymentDetailID = oEOBInsPaymentCreditDetail.ReserveEOBPaymentDetailID;


                            ////////        //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID

                            ////////        oEOBInsurancePaymentReserveDetail.AccountID = oPaymentInsurace.PayerID;
                            ////////        oEOBInsurancePaymentReserveDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
                            ////////        oEOBInsurancePaymentReserveDetail.MSTAccountID = EOBPaymentTypeAccountNo.InsuraceReserverd.GetHashCode();
                            ////////        oEOBInsurancePaymentReserveDetail.MSTAccountType = EOBPaymentAccountType.Reserved;
                            ////////        oEOBInsurancePaymentReserveDetail.PatientID = (Int64) dRow["nPatientID"]; //PatientControl.PatientID;
                            ////////        oEOBInsurancePaymentReserveDetail.PaymentTrayID = oPaymentInsurace.PaymentTrayID;//SelectedPaymentTrayID;
                            ////////        oEOBInsurancePaymentReserveDetail.PaymentTrayCode = oPaymentInsurace.PaymentTrayCode;//SelectedPaymentTrayCode;
                            ////////        oEOBInsurancePaymentReserveDetail.PaymentTrayDescription = oPaymentInsurace.PaymentTrayDesc;//SelectedPaymentTray;
                            ////////        oEOBInsurancePaymentReserveDetail.UserID = AppSettings.UserID;
                            ////////        oEOBInsurancePaymentReserveDetail.UserName = AppSettings.UserName;
                            ////////        oEOBInsurancePaymentReserveDetail.ClinicID = AppSettings.ClinicID;
                            ////////        oEOBInsurancePaymentReserveDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(oPaymentInsurace.CloseDate.ToString());  //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                            ////////        oEOBInsurancePaymentReserveDetail.FinanceLieNo = 0;
                            ////////        oEOBInsurancePaymentReserveDetail.MainCreditLineID = 0;
                            ////////        oEOBInsurancePaymentReserveDetail.IsMainCreditLine = false;
                            ////////        oEOBInsurancePaymentReserveDetail.IsReserveCreditLine = false;
                            ////////        oEOBInsurancePaymentReserveDetail.IsCorrectionCreditLine = false;
                            ////////        oEOBInsurancePaymentReserveDetail.RefFinanceLieNo = 1;
                            ////////        oEOBInsurancePaymentReserveDetail.UseRefFinanceLieNo = true;

                            ////////        //0 ReserveAmount 
                            ////////        //1 ReserveNote 
                            ////////        //2 ReserveSubType 
                            ////////        //3 ReserveNoteOnPrint 

                            ////////        #region "General Note"

                            ////////        EOBPayment.Common.PaymentInsuranceLineNote oPaymentInsuranceLineNote = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineNote();

                            ////////        oPaymentInsuranceLineNote.ClaimNo = 0;
                            ////////        oPaymentInsuranceLineNote.EOBPaymentID = 0;
                            ////////        oPaymentInsuranceLineNote.EOBID = 0;
                            ////////        oPaymentInsuranceLineNote.EOBPaymentDetailID = 0;
                            ////////        oPaymentInsuranceLineNote.BillingTransactionID = 0;
                            ////////        oPaymentInsuranceLineNote.BillingTransactionDetailID = 0;

                            ////////        oPaymentInsuranceLineNote.SubClaimNo = "";
                            ////////        oPaymentInsuranceLineNote.TrackBillingTransactionID = 0;
                            ////////        oPaymentInsuranceLineNote.TrackBillingTransactionDetailID = 0;
                            ////////        oPaymentInsuranceLineNote.TrackBillingTransactionLineNo = 0;

                            ////////        oPaymentInsuranceLineNote.Code = "";
                            ////////        oPaymentInsuranceLineNote.Description = ReserveNote();
                            ////////        oPaymentInsuranceLineNote.Amount = AmountAddedToReserve();
                            ////////        oPaymentInsuranceLineNote.IncludeOnPrint = false;
                            ////////        oPaymentInsuranceLineNote.ClinicID = AppSettings.ClinicID;
                            ////////        oPaymentInsuranceLineNote.PaymentNoteType = EOBPaymentType.InsuraceReserverd;
                            ////////        oPaymentInsuranceLineNote.PaymentNoteSubType = EOBPaymentSubType.Reserved;
                            ////////        oPaymentInsuranceLineNote.HasData = true;
                            ////////        oPaymentInsuranceLineNote.CloseDate = oPaymentInsurace.CloseDate;
                            ////////        oPaymentInsuranceLineNote.UserId = AppSettings.UserID;

                            ////////        oEOBInsurancePaymentReserveDetail.LineNotes.Add(oPaymentInsuranceLineNote);
                            ////////        oPaymentInsuranceLineNote.Dispose();

                            ////////        #endregion

                            ////////        oPaymentInsurace.EOBInsurancePaymentReserveLineDetail.Add(oEOBInsurancePaymentReserveDetail);
                            ////////        oEOBInsurancePaymentReserveDetail.Dispose();

                            ////////        //EOBInsurancePaymentMasterAllocationLines.Add();
                            ////////    }
                            ////////}
                            #endregion

                            #region "On hold selection for splitted claims "

                            //gloSplitClaim ogloSplitClaim = null;
                            //DataTable _dtHoldInfo = GetSplittedClaimsHoldInfo(oSplitClaimDetails);

                            #endregion

                            //EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new EOBPayment.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);

                            //this.EOBPaymentID = ogloEOBPaymentInsurance.SaveSplitEOB(oPaymentInsurace, false, out _outEOBid);
                            //ogloEOBPaymentInsurance.Dispose();
                            #endregion


                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                     ActivityType.CreateERAPaymentObject, "CreateERAPaymentObject - Start ", ActivityOutCome.Success);

                            // oPaymentInsurace.  oNextActions. 
                            // Assign Object
                            oPayment = new ERAPayment();
                            oPayment.PaymentInsurance = oPaymentInsurace;
                            oPayment.PaymentInsuranceLineNextActions = oListNextActions;
                            oPayment.SplitClaimDetails = oSplitClaimDtls;

                            oNextActions.Dispose();
                            oSplitClaimDtls.Dispose();

                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                     ActivityType.CreateERAPaymentObject, "CreateERAPaymentObject - End ", ActivityOutCome.Success);
                            #endregion
                        }

                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                     ActivityType.AddERAPaymentToListObject, "AddERAPaymentToListObject - Start ", ActivityOutCome.Success);

                        if (oPayment != null && IsStopPost == false)
                            oPaymentList.Add(oPayment);


                        //oPaymentInsurances.Add(oPaymentInsurace);
                        oPaymentInsurace.Dispose();

                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                     ActivityType.CreateERAPaymentObject, "AddERAPaymentToListObject - End ", ActivityOutCome.Success);
                    }
                    #endregion 
                }


                Int64 _nEOBID = 0;

                #region " Save EOB Payment & Next action "

                bool bFlag = false;
                bool isEOBSaved = false;

                if (oPaymentList.Count > 0)
                {
                    using (System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(AppSettings.ConnectionStringPM))
                    {

                        _sqlConnection.Open();
                        _sqlTransaction = _sqlConnection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                     ActivityType.SaveOperationStarts, "SaveOperationStarts - Start ", ActivityOutCome.Success);

                        foreach (ERAPayment oERAPay in oPaymentList)
                        {
                            //bool IsNextActionUpdated = false;

                            if (oERAPay.PaymentInsurance != null)
                            {
                                ClsERASave.IsCheckDetailSaved = false;
                                if (bFlag == false)
                                {
                                    ClsERASave.IsCheckDetailSaved = true;
                                    bFlag = true;
                                }
                                EOBPaymentID = ClsERASave.TemperorySaveEOBPayment(_sqlConnection, _sqlTransaction, oERAPay.PaymentInsurance, false, out _nEOBID, out isEOBSaved);
                            }
                            ClsERASave.CheckEOBID = EOBPaymentID;
                            //if (IsNextActionUpdated)
                            //{
                            EOBPayment.Common.PaymentInsuranceLineNextActions Notes = oERAPay.PaymentInsuranceLineNextActions;
                            if (Notes.Count > 0) // Check any object available for Next Action & Next Party.
                            {
                                isEOBSaved = false;
                                ClsERASave.TemperorySaveEOBNextAction(_sqlConnection, _sqlTransaction, ref Notes, EOBPaymentID, _nEOBID, out isEOBSaved);
                            }
                            //}


                        }

                        if (isEOBSaved)
                        {
                            _sqlTransaction.Commit(); bReturn = true; oStopFlag = StopFlag.Passed;
                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                     ActivityType.SaveOperationEnds, "SaveOperationEnds", ActivityOutCome.Success);
                        }
                        else
                        {

                            _sqlTransaction.Rollback();
                            oStopFlag = StopFlag.NotProcessed;
                            sMessage = "Unable to save payment. Transaction Rollback.";
                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                     ActivityType.SaveOperationAborted, "SaveOperationAborted ", ActivityOutCome.Failure);
                            //throw new Exception("Unable to save payment"); 
                        }

                        _sqlConnection.Close();
                    }
                }
                else
                {
                    oStopFlag = StopFlag.NoClaimProcessed;
                    sMessage = "ERA was unable to post payment.";
                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERATemporaryPosting,
                                     ActivityType.NoClaimProcessed, "ERA was unable to post payment.", ActivityOutCome.Failure);
                }
                #endregion

                if (isEOBSaved)
                {
                    #region " Split claim logic "


                    #endregion " Split claim logic "
                }
                ClsERASave.CheckEOBID = 0;
            }
            catch (Exception ex)
            {
                oStopFlag = StopFlag.Error;
                sMessage = "Error Occured While Processing.";
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                InsurancePayment.UnlockCheckClaims(nBPRID);
            }
            finally
            {
                if (oPaymentInsurace != null) { oPaymentInsurace.Dispose(); }
                if (oEOBInsPaymentCreditDetail != null) { oEOBInsPaymentCreditDetail.Dispose(); }
                if (oPaymentInsuranceClaim != null) { oPaymentInsuranceClaim.Dispose(); }
                if (oEOBInsurancePaymentReserveDetail != null) { oEOBInsurancePaymentReserveDetail.Dispose(); }
                if (oSplitClaimDetails != null) { oSplitClaimDetails.Dispose(); }
                if (dtBPRClaims != null) { dtBPRClaims.Dispose(); }
                if (oPaymentList != null) { oPaymentList = null; }
                if (dtCheckDetails != null) { dtCheckDetails.Dispose(); }
                if (dtClaimDetails != null) { dtClaimDetails.Dispose(); }
                if (_sqlTransaction != null) { _sqlTransaction.Dispose(); }
                if (ogloBilling != null) { ogloBilling.Dispose(); ogloBilling = null; }
                //oPaymentInsurace = null;
                //oEOBInsPaymentCreditDetail = null;
                //oPaymentInsuranceClaim = null;
                //oEOBInsurancePaymentReserveDetail = null;
                //oSplitClaimDetails = null;
                //dtBPRClaims = null;
                //oPaymentList = null;
                //dtCheckDetails = null;
                //dtClaimDetails = null;
                //_sqlTransaction = null;

                //GC.Collect();
            }
            return bReturn;
        }

        #endregion

        //#region " ERA Old Method for Original Save.(Once the code gets stable please delete the below commented code.)"

        //public bool PostERAFile(Int64 nBPRID, Int64 nTrayID, string sCloseDate, out string sMessage, out StopFlag oStopFlag)
        //{
        //    EOBInsurancePaymentMasterLines = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines();
        //    EOBInsurancePaymentMasterAllocationLines = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines();

        //    bool bReturn = false;
        //    int _financeLineNo = 0;
        //    //bool IsReserveUsed = false;

        //    //Int64 SelectedInsuranceCompanyID = 0; 


        //    DataSet dsTempPost = null;
        //    DataTable dtBPRClaims = null;
        //    DataTable dtClaims = null;
        //    DataTable dtCheckDetails = null;
        //    DataTable dtClaimDetails = null;
        //    DataTable dtDistinctClaims = null;
        //    DataTable dtClaimReasonCodes = null;
        //    DataTable dtClaimNotes = null;

        //    List<ERAPayment> oPaymentList = new List<ERAPayment>();

        //    //EOBPayment.Common.PaymentInsurance oPaymentInsurace = new EOBPayment.Common.PaymentInsurance();  // Main Payment Master Object
        //    EOBPayment.Common.PaymentInsurance oPaymentInsurace = null;  // Main Payment Master Object
        //    EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsPaymentCreditDetail = null;  // Main Credit Line Entry Object
        //    EOBPayment.Common.PaymentInsuranceClaim oPaymentInsuranceClaim = null;
        //    EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsurancePaymentReserveDetail = null;
        //    SplitClaimDetails oSplitClaimDetails = new SplitClaimDetails();
        //    System.Data.SqlClient.SqlTransaction _sqlTransaction = null;

        //    // Custom Error code
        //    // Custom Error Desc 

        //    sMessage = string.Empty;
        //    string sQueryFilter = string.Empty;
        //    StringBuilder sb = new StringBuilder();
        //    try
        //    {

        //        oStopFlag = StopFlag.NotProcessed; //0

        //        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //            ActivityType.GetTemporaryPostedData, "GetTemporaryPostedData - Start", ActivityOutCome.Success);

        //        // Fetch all Temporary Posted claims against a BPR into DataSet.
        //        dsTempPost = GetTemporaryPostedData(nBPRID);

        //        if (!ValidateDataSet(dsTempPost))
        //        {
        //            sMessage = String.Format("Check details not found for the nBPRID: {0} in Temporary Tables. ", nBPRID);

        //            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //            ActivityType.GetTemporaryPostedData, sMessage, ActivityOutCome.Failure);

        //            sMessage = String.Format("No Claims Processed. ", nBPRID);
        //            oStopFlag = StopFlag.Passed;

        //            return true;
        //        }

        //        dtCheckDetails = dsTempPost.Tables[0];  // Contains records available in "BL_EOBPayment_MST_ERA"
        //        dtDistinctClaims = dsTempPost.Tables[1];  //// Fetch distinct posted claims against check(Contains records avialable in "BL_EOBPayment_DTL_ERA")
        //        dtClaims = dsTempPost.Tables[2];  // Fetch claims against check(Contains records avialable in "BL_EOBPayment_DTL_ERA")
        //        dtClaimReasonCodes = dsTempPost.Tables[5];

        //        if (!GetClaimCount(dtDistinctClaims))
        //        {
        //            sMessage = String.Format("No Claims Processed. ", nBPRID);
        //            oStopFlag = StopFlag.Passed;

        //            return true;
        //        }


        //        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //            ActivityType.GetTemporaryPostedData, "GetTemporaryPostedData - End", ActivityOutCome.Success);


        //        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //            ActivityType.FillMasterDetails, "FillMasterDetails - Start", ActivityOutCome.Success);


        //        // Assign Check details into the local Properties/Varible 
        //        FillMasterDetails(dtCheckDetails);

        //        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //            ActivityType.FillMasterDetails, "FillMasterDetails - End", ActivityOutCome.Success);

        //        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //            ActivityType.FillPaymentTray, "FillPaymentTray - Start", ActivityOutCome.Success);

        //        // Set payment tray information. 
        //        FillPaymentTray(nTrayID);

        //        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //            ActivityType.FillPaymentTray, "FillPaymentTray - End", ActivityOutCome.Success);

        //        if (!GetClaimCount(dtDistinctClaims))
        //        {
        //            //sMessage = String.Format("Claim details not found for the nBPRID: {0} in Temporary Tables. ", nBPRID);

        //            sMessage = String.Format("No Claims Processed.");
        //            oStopFlag = StopFlag.Passed;
        //            return false;
        //        }


        //        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //            ActivityType.ClaimIterationStart, "ClaimIterationStart - Start", ActivityOutCome.Success);

        //        Int64 iClaimCount = 0;
        //        // Iterate claims to assign object (oPaymentInsurace)
        //        foreach (DataRow dtClaimRow in dtDistinctClaims.Rows)
        //        {
        //            iClaimCount++;

        //            Int64 nContactID = Convert.ToInt64(dtClaimRow["nContactID"]);   // InsurancePlanID (return from DeterminePayer)
        //            Int64 nClaimNo = Convert.ToInt64(dtClaimRow["nClaimNo"]);
        //            string nSubClaimNo = dtClaimRow["sSubClaimNo"].ToString();
        //            Int64 nClaimStatus = 0;
        //            Int64 nCLPId = 0;

        //            this.PatientID = Convert.ToInt64(dtClaimRow["nPatientID"]);

        //            PatientInsuranceID = Convert.ToInt64(dtClaimRow["nInsuraceID"]);
        //            SelectedInsuranceCompanyID = Convert.ToInt64(dtClaimRow["nInsuranceCompanyID"]);
        //            ContactInsuranceID = nContactID;

        //            ////Hashtable ohtTab = new Hashtable();
        //            ////ohtTab.Add("NextAction", "");
        //            ////ohtTab.Add("NextContactId", "");
        //            ////ohtTab.Add("NextParty", "");
        //            ////ohtTab.Add("IsStopPosting", "");

        //            ////this.NextAction = ohtTab["NextAction"].ToString();
        //            ////this.NextParty = ohtTab["NextParty"].ToString();
        //            //////htTab.Add("NextContactId", dr["NextContactId"].ToString());
        //            //////htTab.Add("IsStopPosting", dr["IsStopPosting"].ToString());

        //            ////ohtTab = null;

        //            ERAPayment oPayment = null;
        //            if (IsPaymentMade()) // revisit this condition
        //            {

        //                #region " Master Data "

        //                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                        ActivityType.GetPaymentMaster, "GetPaymentMaster - Start", ActivityOutCome.Success);

        //                // Get Payment Master Object
        //                oPaymentInsurace = new EOBPayment.Common.PaymentInsurance();
        //                oPaymentInsurace = GetPaymentMaster();

        //                this.CloseDate = gloDateMaster.gloDate.DateAsNumber(sCloseDate);
        //                oPaymentInsurace.CloseDate = this.CloseDate;

        //                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                        ActivityType.GetPaymentMaster, "GetPaymentMaster - End", ActivityOutCome.Success);

        //                oPaymentInsurace.BPRID = nBPRID;

        //                #endregion

        //                #region " Check/Cash/etc txtCheckAmount - Main Credit Line Entry "

        //                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                        ActivityType.GetMainCreditLineEntry, "GetMainCreditLineEntry - Start", ActivityOutCome.Success);

        //                // Get financial line no
        //                _financeLineNo = oPaymentInsurace.EOBInsurancePaymentLineDetails.Count + 1;

        //                oEOBInsPaymentCreditDetail = GetMainCreditLineEntry(_financeLineNo);  //, EOBInsurancePaymentMasterLines

        //                // Add the Main credit line object to main payment object
        //                oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentCreditDetail);

        //                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                        ActivityType.GetMainCreditLineEntry, "GetMainCreditLineEntry - End", ActivityOutCome.Success);

        //                #endregion

        //                // Get Claims against against check.
        //                // Iterate claims 
        //                sQueryFilter = String.Format(" nClaimNo = {0} and sSubClaimNo = '{1}' ", nClaimNo, nSubClaimNo);
        //                DataRow[] drChargeLines = dtClaims.Select(sQueryFilter, " nBillingTransactionLineNo Asc");


        //                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                        ActivityType.ChargeIterationStart, "ChargeIterationStart - Start", ActivityOutCome.Success);
        //                // Created a new instance for split claim logic
        //                SplitClaimDetails oSplitClaimDtls = null;
        //                SplitClaimDetails oSplitClaimDtls2 = null;
        //                oSplitClaimDtls = new SplitClaimDetails();
        //                oSplitClaimDtls2 = new SplitClaimDetails();

        //                EOBPayment.Common.PaymentInsuranceLineNextActions oTempNextActions = new EOBPayment.Common.PaymentInsuranceLineNextActions();

        //                bool bProcessOnce = false;

        //                foreach (DataRow dRow in drChargeLines)   // Claim Charges
        //                {

        //                    // Created a new instance for storing the nextaction details which needs to be saved
        //                    EOBPayment.Common.PaymentInsuranceLineNextActions oNextActions = new EOBPayment.Common.PaymentInsuranceLineNextActions();



        //                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                        ActivityType.FillClaimDetails, "FillClaimDetails - Start", ActivityOutCome.Success);

        //                    // Reset Local Properties/Variable before processing a claims.
        //                    ResetClaimDetails();

        //                    // Fill claim details into the local Properties/variable for processing a claim.
        //                    FillClaimDetails(dRow);

        //                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                        ActivityType.FillClaimDetails, "FillClaimDetails - End", ActivityOutCome.Success);


        //                    // Region added on 22_June_2010(Dev66) to process a ERA claims 
        //                    #region " Loop through the claim avalable in a single ERA file (Check)".

        //                    //..*** For correction (if amount -ve) we make credit entry against the cpt to balance cpt amount
        //                    //..*** & according to new logic we have to make credit line entry against current check with making the 
        //                    //..** -ve correction amount +ve


        //                    #region "Correction Line Credit Line Entry - Credit -ve against CPT & Positive Credit line against current check."
        //                    decimal _crPayAmt = 0;

        //                    ////// Verify for the line is of type service line
        //                    ////// Verify for the line column is in correction mode.
        //                    ////// Verify amount in payment column.

        //                    if (VerifyPaymentCorrection())
        //                    {
        //                        #region "Commented code for Correction"
        //                        //////if (c1SinglePayment.GetData(nCrIndex, COL_PAYMENT) != null && c1SinglePayment.GetData(nCrIndex, COL_PAYMENT).ToString().Trim() != "")
        //                        //////{
        //                        //////    _crPayAmt = Convert.ToDecimal(c1SinglePayment.GetData(nCrIndex, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(nCrIndex, COL_LAST_PAYMENT));

        //                        //////    if (_crPayAmt < 0)
        //                        //////    {
        //                        //////        _crPayAmt = _crPayAmt - (_crPayAmt * 2);
        //                        //////        _crResPayMode = 0;

        //                        //////        Int64 _crPatientId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_PATIENTID));
        //                        //////        Int64 _crBillTrnId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_ID));
        //                        //////        Int64 _crBillTrnDtlId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_DETAILID));

        //                        //////        // TO DO : delete as no reference found
        //                        //////        //Int64 _crBillTrnDtlLineId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_LINENO));
        //                        //////        //Int64 _crTrackBillTrnId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_ID));
        //                        //////        //Int64 _crTrackBillTrnDtlId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID));
        //                        //////        //Int64 _crTrackBillTrnDtlLineId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_LINENO));
        //                        //////        //string _crCptCode = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_CODE)).Trim();

        //                        //////        //DataTable _nCrDataTable = InsurancePayment.GetCorrectionRefList(_crPayAmt, _crPatientId, _crBillTrnId, _crBillTrnDtlId, this.PatientInsuranceID, this.ContactInsuranceID, this.SelectedInsuranceCompanyID);
        //                        //////        DataTable _nCrDataTable = InsurancePayment.GetCorrectionRefList(_crPayAmt, _crPatientId, _crBillTrnId, _crBillTrnDtlId, this.PatientInsuranceID, this.ContactInsuranceID);

        //                        //////        if (_nCrDataTable != null && _nCrDataTable.Rows.Count > 0)
        //                        //////        {
        //                        //////            for (int nCrDTIndex = 0; nCrDTIndex <= _nCrDataTable.Rows.Count - 1; nCrDTIndex++)
        //                        //////            {
        //                        //////                #region "Set Object to make -ve credit line entry for cpt balance calculation"

        //                        //////                EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsPaymentCorrAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.EOBPaymentID = _EOBPaymentID;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.EOBID = 0;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.EOBDtlID = 0;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.EOBPaymentDetailID = 0;

        //                        //////                //...Will be assigning current check payment & payment details id's to Ref. Id.
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentID = 0;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentDetailID = 0;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentID = 0;// Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentID"]);
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentDetailID = 0;// Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentDetailID"]);

        //                        //////                oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]);
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]);
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentID"]);
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentDetailID"]);

        //                        //////                oEOBInsPaymentCorrAsCreditDetail.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_ID));
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_DETAILID));
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.BillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_LINENO));

        //                        //////                oEOBInsPaymentCorrAsCreditDetail.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_SUBCLAIMNO));
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_ID));
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID));
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_LINENO));

        //                        //////                oEOBInsPaymentCorrAsCreditDetail.DOSFrom = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_FROM).ToString()));
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.DOSTo = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_TO).ToString()));

        //                        //////                oEOBInsPaymentCorrAsCreditDetail.CPTCode = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_CODE));
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.CPTDescription = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_DESCRIPTON));
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.Amount = Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]) - (Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]) * 2);
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.IsNullAmount = false;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.PaymentType = EOBPaymentType.InsuracePayment;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.PaymentSubType = EOBPaymentSubType.Correction;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.PayMode = (EOBPaymentMode)_crResPayMode;

        //                        //////                //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.AccountID = SelectedInsuranceCompanyID; //Convert.ToInt64(lblInsCompany.Tag);
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.MSTAccountID = PatientInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.ContactInsID = ContactInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));

        //                        //////                oEOBInsPaymentCorrAsCreditDetail.PatientID = PatientControl.PatientID;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.PaymentTrayID = SelectedPaymentTrayID;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.PaymentTrayCode = SelectedPaymentTrayCode;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.PaymentTrayDescription = SelectedPaymentTray;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.UserID = AppSettings.UserID;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.UserName = AppSettings.UserName;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.ClinicID = AppSettings.ClinicID;

        //                        //////                oEOBInsPaymentCorrAsCreditDetail.FinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails.Count + 1;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.MainCreditLineID = 0;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.IsMainCreditLine = false;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.IsReserveCreditLine = false;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.IsCorrectionCreditLine = true;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.RefFinanceLieNo = 0;
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.UseRefFinanceLieNo = false;

        //                        //////                if (mskCloseDate.MaskCompleted == true)
        //                        //////                {
        //                        //////                    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //                        //////                }

        //                        //////                oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentCorrAsCreditDetail);
        //                        //////                oEOBInsPaymentCorrAsCreditDetail.Dispose();

        //                        //////                #endregion

        //                        //////                #region "Set Object to make +ve credit entry against current check"

        //                        //////                //---->> 1 = Add Object , 2 = Modify Object , 0 = Do Nothing
        //                        //////                int _Object_Add_Modify_None = -1;
        //                        //////                int _Object_Modify_Index = -1;

        //                        //////                if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
        //                        //////                {
        //                        //////                    for (int index = 0; index < EOBInsurancePaymentMasterLines.Count; index++)
        //                        //////                    {
        //                        //////                        if (EOBInsurancePaymentMasterLines[index].IsCorrectionCreditLine == true)
        //                        //////                        {
        //                        //////                            //1. Check if the correction amount is from the current check if yes do not add object

        //                        //////                            //2. If correction amount is from different check & the credit line does not exists then 
        //                        //////                            //   add the +ve credit line entry

        //                        //////                            //3. If the correction amount is from different check & the credit line exists then
        //                        //////                            //   modify the credit line entry

        //                        //////                            if (EOBInsurancePaymentMasterLines[index].RefEOBPaymentID == _EOBPaymentID)
        //                        //////                            {
        //                        //////                                _Object_Add_Modify_None = 0;
        //                        //////                                break;
        //                        //////                            }
        //                        //////                            else if (EOBInsurancePaymentMasterLines[index].RefEOBPaymentID == Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"])
        //                        //////                                && EOBInsurancePaymentMasterLines[index].RefEOBPaymentDetailID == Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]))
        //                        //////                            {
        //                        //////                                EOBInsurancePaymentMasterLines[index].Amount += Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]);
        //                        //////                                _Object_Add_Modify_None = 2;
        //                        //////                                _Object_Modify_Index = index;
        //                        //////                                break;
        //                        //////                            }
        //                        //////                            else
        //                        //////                            {
        //                        //////                                _Object_Add_Modify_None = 1;
        //                        //////                            }
        //                        //////                        }
        //                        //////                    }
        //                        //////                }
        //                        //////                else
        //                        //////                { _Object_Add_Modify_None = 1; }

        //                        //////                if (_Object_Add_Modify_None == 1)
        //                        //////                {
        //                        //////                    #region " Set new Credit line object "

        //                        //////                    oEOBInsPaymentCorrAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.EOBPaymentID = _EOBPaymentID;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.EOBID = 0;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.EOBDtlID = 0;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.EOBPaymentDetailID = 0;

        //                        //////                    //...Will be assigning current check payment & payment details id's to Ref. Id.
        //                        //////                    //oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]);
        //                        //////                    //oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]);
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]);
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]);
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentID"]);
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentDetailID"]);

        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]);
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]);
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentID"]);
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentDetailID"]);

        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionID = 0;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionDetailID = 0;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionLineNo = 0;

        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.SubClaimNo = "";
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionID = 0;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionDetailID = 0;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionLineNo = 0;

        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.DOSFrom = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_FROM).ToString()));
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.DOSTo = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_TO).ToString()));

        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.CPTCode = "";
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.CPTDescription = "";
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.Amount = Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]);
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.IsNullAmount = false;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentType = EOBPaymentType.InsuranceCorrection;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentSubType = EOBPaymentSubType.Correction;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.PayMode = (EOBPaymentMode)_crResPayMode;

        //                        //////                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.AccountID = SelectedInsuranceCompanyID; //Convert.ToInt64(lblInsCompany.Tag);
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;

        //                        //////                    if (PatientInsuranceID != 0)// lblPayer.Tag != null && Convert.ToString(lblPayer.Tag).Trim() != "")
        //                        //////                    {
        //                        //////                        oEOBInsPaymentCorrAsCreditDetail.MSTAccountID = PatientInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
        //                        //////                        oEOBInsPaymentCorrAsCreditDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
        //                        //////                        oEOBInsPaymentCorrAsCreditDetail.ContactInsID = ContactInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));
        //                        //////                    }

        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.PatientID = 0;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayID = SelectedPaymentTrayID;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayCode = SelectedPaymentTrayCode;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayDescription = SelectedPaymentTray;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.UserID = AppSettings.UserID;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.UserName = AppSettings.UserName;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.ClinicID = AppSettings.ClinicID;

        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.FinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails.Count + 1;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.MainCreditLineID = 0;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.IsMainCreditLine = false;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.IsReserveCreditLine = false;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.IsCorrectionCreditLine = true;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.RefFinanceLieNo = 0;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.UseRefFinanceLieNo = false;

        //                        //////                    if (mskCloseDate.MaskCompleted == true)
        //                        //////                    {
        //                        //////                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        //                        //////                        oEOBInsPaymentCorrAsCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //                        //////                    }

        //                        //////                    oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentCorrAsCreditDetail);
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.Dispose();
        //                        //////                    break;

        //                        //////                    #endregion " Set new Credit line object "
        //                        //////                }
        //                        //////                else if (_Object_Add_Modify_None == 2)
        //                        //////                {
        //                        //////                    #region " Set new Credit line object "

        //                        //////                    oEOBInsPaymentCorrAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.EOBPaymentID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].EOBPaymentID;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.EOBID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].EOBID;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.EOBDtlID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].EOBDtlID;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.EOBPaymentDetailID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].EOBPaymentDetailID;


        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].RefEOBPaymentID;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentDetailID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].RefEOBPaymentDetailID;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].ReserveEOBPaymentID;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentDetailID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].ReserveEOBPaymentDetailID;

        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].OldRefEOBPaymentID;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentDetailID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].OldRefEOBPaymentDetailID;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].OldReserveEOBPaymentID;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentDetailID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].OldReserveEOBPaymentDetailID;

        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionID = 0;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionDetailID = 0;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionLineNo = 0;

        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.SubClaimNo = "";
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionID = 0;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionDetailID = 0;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionLineNo = 0;

        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.DOSFrom = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_FROM).ToString()));
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.DOSTo = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_TO).ToString()));

        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.CPTCode = "";
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.CPTDescription = "";
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.Amount = EOBInsurancePaymentMasterLines[_Object_Modify_Index].Amount;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.IsNullAmount = false;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentType = EOBPaymentType.InsuranceCorrection;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentSubType = EOBPaymentSubType.Correction;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.PayMode = (EOBPaymentMode)_crResPayMode;

        //                        //////                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.AccountID = SelectedInsuranceCompanyID; //Convert.ToInt64(lblInsCompany.Tag);
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;

        //                        //////                    if (PatientInsuranceID != 0)// lblPayer.Tag != null && Convert.ToString(lblPayer.Tag).Trim() != "")
        //                        //////                    {
        //                        //////                        oEOBInsPaymentCorrAsCreditDetail.MSTAccountID = PatientInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
        //                        //////                        oEOBInsPaymentCorrAsCreditDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
        //                        //////                        oEOBInsPaymentCorrAsCreditDetail.ContactInsID = ContactInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));
        //                        //////                    }

        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.PatientID = 0;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayID = SelectedPaymentTrayID;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayCode = SelectedPaymentTrayCode;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayDescription = SelectedPaymentTray;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.UserID = AppSettings.UserID;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.UserName = AppSettings.UserName;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.ClinicID = AppSettings.ClinicID;

        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.FinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails.Count + 1;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.MainCreditLineID = 0;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.IsMainCreditLine = false;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.IsReserveCreditLine = false;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.IsCorrectionCreditLine = true;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.RefFinanceLieNo = 0;
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.UseRefFinanceLieNo = false;

        //                        //////                    if (mskCloseDate.MaskCompleted == true)
        //                        //////                    {
        //                        //////                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        //                        //////                        oEOBInsPaymentCorrAsCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //                        //////                    }

        //                        //////                    oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentCorrAsCreditDetail);
        //                        //////                    oEOBInsPaymentCorrAsCreditDetail.Dispose();
        //                        //////                    break;

        //                        //////                    #endregion " Set new Credit line object "
        //                        //////                }

        //                        //////                #endregion
        //                        //////            }
        //                        //////        }
        //                        //////    }
        //                        //////}
        //                        #endregion
        //                    }
        //                    #endregion


        //                    #region "Use Reserved Credit Line Entry"

        //                    //////////if (IsReserveUsed)
        //                    //////////{
        //                    //////////    //for (int i = 0; i <= EOBInsurancePaymentMasterLines.Count - 1; i++)
        //                    //////////    //{
        //                    //////////    //    EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine EOBInsurancePaymentMasterLine = EOBInsurancePaymentMasterLines[i];

        //                    //////////    //    //..Code changes done by Sagar Ghodke on 20100105(critical change Confirmation needed)
        //                    //////////    //    //...Below commented condition is previous one
        //                    //////////    //    //if (EOBInsurancePaymentMasterLines[i].PaymentType == EOBPaymentType.InsuracePayment)
        //                    //////////    //    if (EOBInsurancePaymentMasterLine.PaymentType == EOBPaymentType.InsuraceReserverd && EOBInsurancePaymentMasterLine.PaymentSubType == EOBPaymentSubType.Reserved)
        //                    //////////    //    {
        //                    //////////    //        EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsPaymentResAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
        //                    //////////    //        // Get financial line no
        //                    //////////    //        _financeLineNo = oPaymentInsurace.EOBInsurancePaymentLineDetails.Count + 1;
        //                    //////////    //        // Pass finalcial line & used reserve details to get the credit line object for used reserve
        //                    //////////    //        oEOBInsPaymentResAsCreditDetail = GetCreditLineForReserveUsed(EOBInsurancePaymentMasterLine, _financeLineNo);
        //                    //////////    //        // Add the credit line object to Main payment object
        //                    //////////    //        oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentResAsCreditDetail);
        //                    //////////    //        oEOBInsPaymentResAsCreditDetail.Dispose();
        //                    //////////    //    }
        //                    //////////    //}
        //                    //////////}

        //                    #endregion

        //                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                        ActivityType.EOBLine, "EOBLine - Start", ActivityOutCome.Success);

        //                    if (oPaymentInsurace != null && oPaymentInsurace.EOBInsurancePaymentLineDetails != null)
        //                    {
        //                        for (int i = 0; i <= oPaymentInsurace.EOBInsurancePaymentLineDetails.Count - 1; i++)
        //                        {
        //                            bool _AddLine = true;

        //                            EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine oEOBInsPaymentAllDtl = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine();

        //                            #region "If Credit line first time added and its second time then dont add just update the amount"

        //                            //////////if (oPaymentInsurace.EOBInsurancePaymentLineDetails[i].IsMainCreditLine == true)
        //                            //////////{
        //                            //////////    for (int ccl = 0; ccl <= EOBInsurancePaymentMasterAllocationLines.Count - 1; ccl++)
        //                            //////////    {
        //                            //////////        if (EOBInsurancePaymentMasterAllocationLines[ccl].IsMainCreditLine == true)
        //                            //////////        {
        //                            //////////            decimal _OldCheckBalAmt = EOBInsurancePaymentMasterAllocationLines[ccl].Amount;
        //                            //////////            decimal _OldCheckAmt = 0;
        //                            //////////            for (int cml = 0; cml <= EOBInsurancePaymentMasterLines.Count - 1; cml++)
        //                            //////////            {
        //                            //////////                if (EOBInsurancePaymentMasterLines[cml].IsMainCreditLine == true)
        //                            //////////                {
        //                            //////////                    _OldCheckAmt = EOBInsurancePaymentMasterLines[cml].Amount;
        //                            //////////                    break;
        //                            //////////                }
        //                            //////////            }
        //                            //////////            decimal _NewCheckAmt = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].Amount;
        //                            //////////            decimal _NewCheckBalAmt = 0;
        //                            //////////            if (_NewCheckAmt < _OldCheckAmt)
        //                            //////////            {
        //                            //////////                _NewCheckBalAmt = _OldCheckBalAmt - (_OldCheckAmt - _NewCheckAmt);
        //                            //////////            }
        //                            //////////            else
        //                            //////////            {
        //                            //////////                _NewCheckBalAmt = _OldCheckBalAmt + (_NewCheckAmt - _OldCheckAmt);
        //                            //////////            }

        //                            //////////            EOBInsurancePaymentMasterAllocationLines[ccl].Amount = _NewCheckBalAmt;// oPaymentInsurace.EOBInsurancePaymentLineDetails[ccl].Amount;
        //                            //////////            _AddLine = false;
        //                            //////////            break;
        //                            //////////        }
        //                            //////////    }
        //                            //////////}
        //                            #endregion

        //                            if (_AddLine == true)
        //                            {
        //                                oEOBInsPaymentAllDtl = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine();

        //                                #region " Set Object "

        //                                oEOBInsPaymentAllDtl.EOBPaymentID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].EOBPaymentID;
        //                                oEOBInsPaymentAllDtl.EOBID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].EOBID;
        //                                oEOBInsPaymentAllDtl.EOBDtlID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].EOBDtlID;
        //                                oEOBInsPaymentAllDtl.EOBPaymentDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].EOBPaymentDetailID;

        //                                oEOBInsPaymentAllDtl.BillingTransactionID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].BillingTransactionID;
        //                                oEOBInsPaymentAllDtl.BillingTransactionDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].BillingTransactionDetailID;
        //                                oEOBInsPaymentAllDtl.BillingTransactionLineNo = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].BillingTransactionLineNo;

        //                                oEOBInsPaymentAllDtl.TrackBillingTransactionID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].TrackBillingTransactionID;
        //                                oEOBInsPaymentAllDtl.TrackBillingTransactionDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].TrackBillingTransactionDetailID;
        //                                oEOBInsPaymentAllDtl.TrackBillingTransactionLineNo = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].TrackBillingTransactionLineNo;

        //                                oEOBInsPaymentAllDtl.PatientID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PatientID;
        //                                oEOBInsPaymentAllDtl.DOSFrom = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].DOSFrom;
        //                                oEOBInsPaymentAllDtl.DOSTo = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].DOSTo;
        //                                oEOBInsPaymentAllDtl.CPTCode = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].CPTCode;
        //                                oEOBInsPaymentAllDtl.CPTDescription = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].CPTDescription;
        //                                oEOBInsPaymentAllDtl.Amount = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].Amount;
        //                                oEOBInsPaymentAllDtl.IsNullAmount = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].IsNullAmount;
        //                                oEOBInsPaymentAllDtl.PaymentType = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PaymentType;
        //                                oEOBInsPaymentAllDtl.PaymentSubType = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PaymentSubType;
        //                                oEOBInsPaymentAllDtl.PaySign = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PaySign;
        //                                oEOBInsPaymentAllDtl.PayMode = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PayMode;

        //                                oEOBInsPaymentAllDtl.RefEOBPaymentID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].RefEOBPaymentID;
        //                                oEOBInsPaymentAllDtl.RefEOBPaymentDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].RefEOBPaymentDetailID;
        //                                oEOBInsPaymentAllDtl.ReserveEOBPaymentID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ReserveEOBPaymentID;
        //                                oEOBInsPaymentAllDtl.ReserveEOBPaymentDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ReserveEOBPaymentDetailID;

        //                                oEOBInsPaymentAllDtl.OldRefEOBPaymentID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].RefEOBPaymentID;
        //                                oEOBInsPaymentAllDtl.OldRefEOBPaymentDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].RefEOBPaymentDetailID;
        //                                oEOBInsPaymentAllDtl.OldReserveEOBPaymentID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ReserveEOBPaymentID;
        //                                oEOBInsPaymentAllDtl.OldReserveEOBPaymentDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ReserveEOBPaymentDetailID;


        //                                oEOBInsPaymentAllDtl.AccountID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].AccountID;
        //                                oEOBInsPaymentAllDtl.AccountType = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].AccountType;
        //                                oEOBInsPaymentAllDtl.MSTAccountID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].MSTAccountID;
        //                                oEOBInsPaymentAllDtl.MSTAccountType = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].MSTAccountType;
        //                                oEOBInsPaymentAllDtl.PaymentTrayID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PaymentTrayID;
        //                                oEOBInsPaymentAllDtl.PaymentTrayCode = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PaymentTrayCode;
        //                                oEOBInsPaymentAllDtl.PaymentTrayDescription = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PaymentTrayDescription;
        //                                oEOBInsPaymentAllDtl.UserID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].UserID;
        //                                oEOBInsPaymentAllDtl.UserName = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].UserName;
        //                                oEOBInsPaymentAllDtl.CreatedDateTime = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].CreatedDateTime;
        //                                oEOBInsPaymentAllDtl.ModifiedDateTime = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ModifiedDateTime;
        //                                oEOBInsPaymentAllDtl.ClinicID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ClinicID;

        //                                oEOBInsPaymentAllDtl.FinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].FinanceLieNo;
        //                                oEOBInsPaymentAllDtl.MainCreditLineID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].MainCreditLineID;
        //                                oEOBInsPaymentAllDtl.IsMainCreditLine = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].IsMainCreditLine;
        //                                oEOBInsPaymentAllDtl.IsReserveCreditLine = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].IsReserveCreditLine;
        //                                oEOBInsPaymentAllDtl.IsCorrectionCreditLine = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].IsCorrectionCreditLine;
        //                                oEOBInsPaymentAllDtl.RefFinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].RefFinanceLieNo;
        //                                oEOBInsPaymentAllDtl.UseRefFinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].UseRefFinanceLieNo;
        //                                oEOBInsPaymentAllDtl.ContactInsID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ContactInsID;


        //                                #endregion " Set Object "

        //                                EOBInsurancePaymentMasterAllocationLines.Add(oEOBInsPaymentAllDtl);
        //                                oEOBInsPaymentAllDtl.Dispose();
        //                            }
        //                        }
        //                    }

        //                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                        ActivityType.EOBLine, "EOBLine - End", ActivityOutCome.Success);


        //                    //while assigning this object for collection amount object, there are -ve amount of correction
        //                    //we have to make it positive for debit line allocation
        //                    //so using for loop we will make it positive
        //                    for (int nAlctn = 0; nAlctn <= EOBInsurancePaymentMasterAllocationLines.Count - 1; nAlctn++)
        //                    {
        //                        if (EOBInsurancePaymentMasterAllocationLines[nAlctn].Amount < 0)
        //                        {
        //                            EOBInsurancePaymentMasterAllocationLines[nAlctn].Amount = EOBInsurancePaymentMasterAllocationLines[nAlctn].Amount - (EOBInsurancePaymentMasterAllocationLines[nAlctn].Amount * 2);
        //                        }
        //                    }

        //                    //Allocation Amount - Finish


        //                    #region " ......................... Claim Payment Details Start ................................. "

        //                    oPaymentInsuranceClaim = new EOBPayment.Common.PaymentInsuranceClaim();
        //                    //DataTable dtFile = dsERAFile.Tables[0];
        //                    //DataRow dRow = dtFile.Rows[1];
        //                    //DataRow dRowMST =  dtFile.Rows[0];

        //                    // Similar to get the Service Line Type as Claim in Manual posting.
        //                    if (this.BillingTransactionID > 0) //(c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
        //                    {


        //                        // The following line are commented on 21_June_2010 (Dev66), 
        //                        // Once conditions are define then the commented code can be removed.
        //                        // Hidden Grid row used to store meta data.
        //                        //////if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
        //                        //////{
        //                        //////    for (int clmIndex = 1; clmIndex < c1SinglePayment.Rows.Count; clmIndex++)
        //                        //////    {
        //                        //////        if (c1SinglePayment.GetData(clmIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(clmIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.Claim)
        //                        //////        {
        //                        //////            if (c1SinglePayment.GetData(clmIndex, COL_BILLING_TRANSACTON_ID) != null && Convert.ToString(c1SinglePayment.GetData(clmIndex, COL_BILLING_TRANSACTON_ID)).Trim() != "")
        //                        //////            {
        //                        oPaymentInsuranceClaim.BillingTransactionID = this.BillingTransactionID; //(Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(clmIndex, COL_BILLING_TRANSACTON_ID));
        //                        oPaymentInsuranceClaim.TrackBillingTrnID = this.TrackTrnID;//this.BillingTransactionDetailID; // (Int64)dRow["nTrackTrnId"];  //Convert.ToInt64(c1SinglePayment.GetData(clmIndex, COL_TRACK_BILLING_TRANSACTON_ID));
        //                        oPaymentInsuranceClaim.ClaimNo = this.ClaimNo; // (Int64)dRow["nClaimNo"]; //Convert.ToInt64(c1SinglePayment.GetData(clmIndex, COL_CLAIMNO));
        //                        oPaymentInsuranceClaim.SubClaimNo = this.SubclaimNo; // dRow["nSubClaimNo"].ToString(); //Convert.ToString(c1SinglePayment.GetData(clmIndex, COL_SUBCLAIMNO));

        //                        oSplitClaimDetails.TransactionMasterID = oPaymentInsuranceClaim.BillingTransactionID;
        //                        oSplitClaimDetails.TransactionID = oPaymentInsuranceClaim.TrackBillingTrnID;
        //                        oSplitClaimDetails.ClaimNo = oPaymentInsuranceClaim.ClaimNo;
        //                        oSplitClaimDetails.SubClaimNo = oPaymentInsuranceClaim.SubClaimNo;
        //                        oSplitClaimDetails.ClinicID = AppSettings.ClinicID;


        //                        oSplitClaimDtls.TransactionMasterID = oPaymentInsuranceClaim.BillingTransactionID;
        //                        oSplitClaimDtls.TransactionID = this.TrackTrnID;//oPaymentInsuranceClaim.TrackBillingTrnID;
        //                        oSplitClaimDtls.ClaimNo = oPaymentInsuranceClaim.ClaimNo;
        //                        oSplitClaimDtls.SubClaimNo = oPaymentInsuranceClaim.SubClaimNo;
        //                        oSplitClaimDtls.ClinicID = AppSettings.ClinicID;
        //                        if (bProcessOnce == false)
        //                        {
        //                            oSplitClaimDtls2.TransactionMasterID = oPaymentInsuranceClaim.BillingTransactionID;
        //                            oSplitClaimDtls2.TransactionID = this.TrackTrnID; //oPaymentInsuranceClaim.TrackBillingTrnID;
        //                            oSplitClaimDtls2.ClaimNo = oPaymentInsuranceClaim.ClaimNo;
        //                            oSplitClaimDtls2.SubClaimNo = oPaymentInsuranceClaim.SubClaimNo;
        //                            oSplitClaimDtls2.ClinicID = AppSettings.ClinicID;
        //                            bProcessOnce = true;
        //                        }
        //                        //////            }
        //                        //////        }
        //                        //////    }
        //                        //////}

        //                        #region "EOB Service Lines - New Logic - Direct allocation from credit line insted of allocation from correction, reserve and check"

        //                        EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsurancePaymentDetail = null;

        //                        // Condition to be implemented.
        //                        // 1. Identify line is of type Service line.
        //                        // 2. Verify value available in payment payment column.

        //                        // The following (for loop & if conditions are commented on 21_June_2010(Dev66).

        //                        //////for (int i = 0; i <= c1SinglePayment.Rows.Count - 1; i++)
        //                        //////{
        //                        //////    if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
        //                        //////    {
        //                        //////        if (
        //                        //////                (c1SinglePayment.GetData(i, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PAYMENT)).Trim() != "")
        //                        //////            //|| (c1SinglePayment.GetData(i, COL_NEXT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT)).Trim() != "")
        //                        //////            )
        //                        //////        {


        //                        EOBPayment.Common.PaymentInsuranceLine oPaymentInsuranceLine = new EOBPayment.Common.PaymentInsuranceLine();
        //                        bool _Add_WO_WH = false;

        //                        #region "EOB Line"
        //                        //QCheck for PatInsuranceID,InsContactID
        //                        oPaymentInsuranceLine.PatientID = this.PatientID; // (Int64)dRow["nPatientID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_PATIENTID));
        //                        oPaymentInsuranceLine.PatInsuranceID = PatientInsuranceID;  //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
        //                        oPaymentInsuranceLine.InsContactID = nContactID;//ContactInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));

        //                        oPaymentInsuranceLine.BLTransactionID = this.BillingTransactionID; // (Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
        //                        oPaymentInsuranceLine.BLTransactionDetailID = this.BillingTransactionDetailID; // (Int64)dRow["nBillingTransactionDetailID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
        //                        oPaymentInsuranceLine.BLTransactionLineNo = this.BillingTransactionLineNo; // (Int64)dRow["nBillingTransactionLineNo"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));

        //                        oPaymentInsuranceLine.TrackBLTransactionID = this.TrackTrnID; // (Int64)dRow["nTrackTrnId"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
        //                        oPaymentInsuranceLine.TrackBLTransactionDetailID = this.TrackTrnDtlID; // (Int64)dRow["nTrackTrnDtlId"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
        //                        oPaymentInsuranceLine.TrackBLTransactionLineNo = this.BillingTransactionLineNo; // (Int64)dRow["nTrackBLTransactionLineNo"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_LINENO));


        //                        oPaymentInsuranceLine.ClaimNumber = this.ClaimNo; // (Int64)dRow["nClaimNo"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
        //                        oPaymentInsuranceLine.SubClaimNumber = this.SubclaimNo; // dRow["nSubClaimNo"].ToString(); // Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));

        //                        oPaymentInsuranceLine.DOSFrom = gloDateMaster.gloDate.DateAsNumber(this.DOSFrom); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dRow["dDOSFrom"]));
        //                        oPaymentInsuranceLine.DOSTo = gloDateMaster.gloDate.DateAsNumber(this.DOSTo); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dRow["dDOSTo"]));
        //                        oPaymentInsuranceLine.CPTCode = this.CPT; // Convert.ToString(dRow["sCPTCode"]);
        //                        oPaymentInsuranceLine.CPTDescription = this.CPTDescription; // Convert.ToString(dRow["sCPTDescription"]);

        //                        oPaymentInsuranceLine.BLInsuranceID = 0;
        //                        oPaymentInsuranceLine.BLInsuranceName = "";
        //                        oPaymentInsuranceLine.BLInsuranceFlag = InsuranceTypeFlag.None;

        //                        if (this.Charges.Trim() != "")
        //                        { oPaymentInsuranceLine.Charges = Convert.ToDecimal(this.Charges); oPaymentInsuranceLine.IsNullCharges = false; }

        //                        if (this.Unit.Trim() != "")
        //                        { oPaymentInsuranceLine.Unit = Convert.ToDecimal(this.Unit); oPaymentInsuranceLine.IsNullUnit = false; }

        //                        if (this.TotalCharges.Trim() != "")
        //                        { oPaymentInsuranceLine.TotalCharges = Convert.ToDecimal(this.TotalCharges); oPaymentInsuranceLine.IsNullTotalCharges = false; }

        //                        if (this.Allowed.Trim() != "")
        //                        { oPaymentInsuranceLine.Allowed = Convert.ToDecimal(this.Allowed); oPaymentInsuranceLine.IsNullAllowed = false; }

        //                        if (this.WriteOff.Trim() != "")
        //                        { oPaymentInsuranceLine.WriteOff = Convert.ToDecimal(this.WriteOff); oPaymentInsuranceLine.IsNullWriteOff = false; }

        //                        oPaymentInsuranceLine.NonCovered = 0;

        //                        if (this.Payment.Trim() != "")
        //                        { oPaymentInsuranceLine.InsuranceAmount = Convert.ToDecimal(this.Payment); oPaymentInsuranceLine.IsNullInsurance = false; }

        //                        if (this.Copay.Trim() != "")
        //                        { oPaymentInsuranceLine.Copay = Convert.ToDecimal(this.Copay); oPaymentInsuranceLine.IsNullCopay = false; }

        //                        if (this.Deductible.Trim() != "")
        //                        { oPaymentInsuranceLine.Deductible = Convert.ToDecimal(this.Deductible); oPaymentInsuranceLine.IsNullDeductible = false; }

        //                        if (this.CoInsurance.Trim() != "")
        //                        { oPaymentInsuranceLine.CoInsurance = Convert.ToDecimal(this.CoInsurance); oPaymentInsuranceLine.IsNullCoInsurance = false; }

        //                        if (this.WithHold.Trim() != "")
        //                        { oPaymentInsuranceLine.Withhold = Convert.ToDecimal(this.WithHold); oPaymentInsuranceLine.IsNullWithhold = false; }


        //                        // Below are the original line commented on 21_June_2010(Dev66). Once code is stable delete the below commented code

        //                        oPaymentInsuranceLine.PaymentTrayID = SelectedPaymentTrayID;
        //                        oPaymentInsuranceLine.PaymentTrayCode = SelectedPaymentTrayCode;
        //                        oPaymentInsuranceLine.PaymentTrayDesc = SelectedPaymentTray;


        //                        oPaymentInsuranceLine.UserID = AppSettings.UserID;
        //                        oPaymentInsuranceLine.UserName = AppSettings.UserName;
        //                        oPaymentInsuranceLine.ClinicID = AppSettings.ClinicID;

        //                        //if (mskCloseDate.MaskCompleted == true)
        //                        //{
        //                        //mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        //                        oPaymentInsuranceLine.CloseDate = this.CloseDate; //gloDateMaster.gloDate.DateAsNumber(oPaymentInsurace.CloseDate.ToString()); //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //                        //}

        //                        #region " Set Line Reason Codes "


        //                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                                ActivityType.SetLineReasonCodes, "SetLineReasonCodes - Start", ActivityOutCome.Success);


        //                        //...Code added on 20100318 by Sagar Ghodke
        //                        //...Code added to auto set the adjustment codes for W/O,Copay,Dedutible,Coinsurance,Withhold
        //                        //...by reading there respective values from admin settings

        //                        EOBPayment.Common.PaymentInsuranceLineResonCode oPaymentInsuranceLineResonCode = null;
        //                        string _code = "";

        //                        // This Hastable object is assigned with database record column name.
        //                        // Which is used in retrieving column value from a datarow.
        //                        Hashtable htTab = new Hashtable();
        //                        htTab.Add(23, "nWriteOff");
        //                        htTab.Add(24, "nCopay");
        //                        htTab.Add(25, "nDeductible");
        //                        htTab.Add(26, "nCoInsurance");
        //                        htTab.Add(27, "nWithhold");


        //                        for (int colIndex = COL_WRITEOFF; colIndex <= COL_WITHHOLD; colIndex++)
        //                        {
        //                            _code = "";
        //                            //if (c1SinglePayment.GetData(i, colIndex) != null && Convert.ToString(c1SinglePayment.GetData(i, colIndex)).Trim() != ""
        //                            //    && Convert.ToDecimal(c1SinglePayment.GetData(i, colIndex)) != 0)
        //                            if (dRow[htTab[(object)colIndex].ToString()].ToString().Trim() != "")
        //                            {
        //                                oPaymentInsuranceLineResonCode = new EOBPayment.Common.PaymentInsuranceLineResonCode();
        //                                oPaymentInsuranceLineResonCode.ID = 0;
        //                                oPaymentInsuranceLineResonCode.ClaimNo = oPaymentInsuranceLine.ClaimNumber;
        //                                oPaymentInsuranceLineResonCode.BillingTransactionID = oPaymentInsuranceLine.BLTransactionID;
        //                                oPaymentInsuranceLineResonCode.BillingTransactionDetailID = oPaymentInsuranceLine.BLTransactionDetailID;
        //                                oPaymentInsuranceLineResonCode.SubClaimNo = oPaymentInsuranceLine.SubClaimNumber;
        //                                oPaymentInsuranceLineResonCode.TrackBillingTransactionID = oPaymentInsuranceLine.TrackBLTransactionID;
        //                                oPaymentInsuranceLineResonCode.TrackBillingTransactionDetailID = oPaymentInsuranceLine.TrackBLTransactionDetailID;
        //                                oPaymentInsuranceLineResonCode.CloseDate = oPaymentInsuranceLine.CloseDate;
        //                                oPaymentInsuranceLineResonCode.ClinicID = oPaymentInsuranceLine.ClinicID;
        //                                oPaymentInsuranceLineResonCode.EOBPaymentID = 0;
        //                                oPaymentInsuranceLineResonCode.EOBID = 0;
        //                                oPaymentInsuranceLineResonCode.EOBPaymentDetailID = 0;
        //                                oPaymentInsuranceLineResonCode.HasData = true;
        //                                oPaymentInsuranceLineResonCode.IsNullReasonAmount = false;

        //                                oPaymentInsuranceLineResonCode.ReasonCode = GetSelectedReasonCode(colIndex);

        //                                oPaymentInsuranceLineResonCode.ReasonDescription = InsurancePayment.GetReasonDescription(_code);
        //                                // Calculate Reason Amount method is required.
        //                                oPaymentInsuranceLineResonCode.ReasonAmount = (decimal)dRow[htTab[(object)colIndex].ToString()];//CalculateReasonAmount(oPaymentInsuranceLineResonCode.ReasonCode);
        //                                oPaymentInsuranceLineResonCode.CommentType = EOBCommentType.SystemReasonCode;
        //                                oPaymentInsuranceLineResonCode.ReasonCodeSubType = colIndex;
        //                                oPaymentInsuranceLine.LineResonCodes.Add(oPaymentInsuranceLineResonCode);
        //                                oPaymentInsuranceLineResonCode = null;
        //                            }
        //                        }

        //                        htTab = null;


        //                        //...End code add 20100318


        //                        // Need Clarification on the below code for line reason code - Dev66.
        //                        // Get Other reason codes.
        //                        // ERA_GetOtherReasonCodes
        //                        // Type = 1
        //                        DataTable dtOtherReason = null;
        //                        //DataTable dtOtherReason = GetOtherReasonCodes(this.SVCId);

        //                        dtOtherReason = dsTempPost.Tables[5];

        //                        object[] oParamLineOtherReasons = { EOBCommentType.Reason.GetHashCode(), this.ClaimNo, this.SubclaimNo, 0, this.TrackTrnDtlID, this.BillingTransactionDetailID };

        //                        sQueryFilter = String.Format("nType = {0} and nClaimNo =  {1} and sSubClaimNo = '{2}' and nSubType = {3} and nTrackTrnDtlID = {4} and nBillingTransactionDetailID = {5} ", oParamLineOtherReasons);

        //                        DataRow[] drOtherReasonLines = dtOtherReason.Select(sQueryFilter);

        //                        foreach (DataRow dtRow in drOtherReasonLines)
        //                        {
        //                            oPaymentInsuranceLineResonCode = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineResonCode();

        //                            oPaymentInsuranceLineResonCode.ID = 0;

        //                            oPaymentInsuranceLineResonCode.ClaimNo = this.ClaimNo;

        //                            oPaymentInsuranceLineResonCode.SubClaimNo = this.SubclaimNo;

        //                            oPaymentInsuranceLineResonCode.EOBPaymentID = 0;

        //                            oPaymentInsuranceLineResonCode.EOBID = 0;

        //                            oPaymentInsuranceLineResonCode.EOBPaymentDetailID = 0;

        //                            oPaymentInsuranceLineResonCode.BillingTransactionID = this.BillingTransactionID;

        //                            oPaymentInsuranceLineResonCode.BillingTransactionDetailID = this.BillingTransactionDetailID;

        //                            oPaymentInsuranceLineResonCode.TrackBillingTransactionID = this.TrackTrnID;

        //                            oPaymentInsuranceLineResonCode.TrackBillingTransactionDetailID = this.TrackTrnDtlID;

        //                            oPaymentInsuranceLineResonCode.ReasonCode = dtRow["sReasonCode"].ToString();

        //                            oPaymentInsuranceLineResonCode.ReasonDescription = "";

        //                            if (dtRow["dReasonAmount"] != null)
        //                            {
        //                                if (dtRow["dReasonAmount"].ToString().Trim().Length > 0)
        //                                    oPaymentInsuranceLineResonCode.ReasonAmount = Convert.ToDecimal(dtRow["dReasonAmount"].ToString());
        //                                oPaymentInsuranceLineResonCode.IsNullReasonAmount = false;
        //                            }

        //                            oPaymentInsuranceLineResonCode.ClinicID = AppSettings.ClinicID;

        //                            oPaymentInsuranceLineResonCode.CloseDate = oPaymentInsurace.CloseDate;
        //                            oPaymentInsuranceLineResonCode.HasData = true;
        //                            oPaymentInsuranceLineResonCode.CommentType = EOBCommentType.Reason;

        //                            oPaymentInsuranceLine.LineResonCodes.Add(oPaymentInsuranceLineResonCode);
        //                        }


        //                        #region " Code commented by Dev66 - Need to revisit the code.
        //                        ////EOBPayment.Common.PaymentInsuranceLineResonCode oPaymentInsuranceLineResonCode = null;
        //                        ////string _code = "";

        //                        ////DataRow[] drLineReasonRows = dtClaimReasonCodes.Select("nType = " + EOBCommentType.SystemReasonCode.GetHashCode() + " and nClaimNo = " + this.ClaimNo + " and nBillingTransactionDetailID=" + dRow["nEOBDtlID"].ToString());
        //                        ////foreach (DataRow drLineReasonRow in drLineReasonRows)
        //                        ////{
        //                        ////    _code = "";
        //                        ////    if (drLineReasonRow != null)
        //                        ////    {
        //                        ////        oPaymentInsuranceLineResonCode = new EOBPayment.Common.PaymentInsuranceLineResonCode();
        //                        ////        oPaymentInsuranceLineResonCode.ID = 0;
        //                        ////        oPaymentInsuranceLineResonCode.ClaimNo = oPaymentInsuranceLine.ClaimNumber;
        //                        ////        oPaymentInsuranceLineResonCode.BillingTransactionID = oPaymentInsuranceLine.BLTransactionID;
        //                        ////        oPaymentInsuranceLineResonCode.BillingTransactionDetailID = oPaymentInsuranceLine.BLTransactionDetailID;
        //                        ////        oPaymentInsuranceLineResonCode.SubClaimNo = oPaymentInsuranceLine.SubClaimNumber;
        //                        ////        oPaymentInsuranceLineResonCode.TrackBillingTransactionID = oPaymentInsuranceLine.TrackBLTransactionID;
        //                        ////        oPaymentInsuranceLineResonCode.TrackBillingTransactionDetailID = oPaymentInsuranceLine.TrackBLTransactionDetailID;
        //                        ////        oPaymentInsuranceLineResonCode.CloseDate = oPaymentInsuranceLine.CloseDate;
        //                        ////        oPaymentInsuranceLineResonCode.ClinicID = oPaymentInsuranceLine.ClinicID;
        //                        ////        oPaymentInsuranceLineResonCode.EOBPaymentID = 0;
        //                        ////        oPaymentInsuranceLineResonCode.EOBID = 0;
        //                        ////        oPaymentInsuranceLineResonCode.EOBPaymentDetailID = 0;
        //                        ////        oPaymentInsuranceLineResonCode.HasData = true;
        //                        ////        oPaymentInsuranceLineResonCode.IsNullReasonAmount = false;

        //                        ////        oPaymentInsuranceLineResonCode.ReasonCode = drLineReasonRow["sReasonCode"].ToString(); ////GetSelectedReasonCode(colIndex);

        //                        ////        oPaymentInsuranceLineResonCode.ReasonDescription = drLineReasonRow["sReasonDescription"].ToString(); //InsurancePayment.GetReasonDescription(_code);
        //                        ////        // Calculate Reason Amount method is required.
        //                        ////        oPaymentInsuranceLineResonCode.ReasonAmount = Convert.ToDecimal(drLineReasonRow["dReasonAmount"].ToString());//CalculateReasonAmount(oPaymentInsuranceLineResonCode.ReasonCode);
        //                        ////        oPaymentInsuranceLineResonCode.CommentType = EOBCommentType.SystemReasonCode;
        //                        ////        oPaymentInsuranceLineResonCode.ReasonCodeSubType = Convert.ToInt32(drLineReasonRow["nSubType"]);

        //                        ////        oPaymentInsuranceLine.LineResonCodes.Add(oPaymentInsuranceLineResonCode);
        //                        ////        oPaymentInsuranceLineResonCode = null;
        //                        ////    }
        //                        ////}


        //                        //////...End code add 20100318


        //                        ////// Get Other Reason codes stored in Temporary table.
        //                        ////drLineReasonRows = dtClaimReasonCodes.Select("nType = " + EOBCommentType.Reason.GetHashCode() + " and nClaimNo = " + this.ClaimNo + " and nBillingTransactionDetailID=" + dRow["nEOBDtlID"].ToString());


        //                        ////if (drLineReasonRows != null)
        //                        ////{

        //                        ////    foreach (DataRow drLineReasonRow in drLineReasonRows)
        //                        ////    {
        //                        ////        oPaymentInsuranceLineResonCode = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineResonCode();

        //                        ////        oPaymentInsuranceLineResonCode.ID = Convert.ToInt64("0");

        //                        ////        oPaymentInsuranceLineResonCode.ClaimNo = this.ClaimNo;
        //                        ////        oPaymentInsuranceLineResonCode.SubClaimNo = this.SubclaimNo; 

        //                        ////        oPaymentInsuranceLineResonCode.EOBPaymentID = 0; 

        //                        ////        oPaymentInsuranceLineResonCode.EOBID = 0; 

        //                        ////        oPaymentInsuranceLineResonCode.EOBPaymentDetailID = 0; 

        //                        ////        oPaymentInsuranceLineResonCode.BillingTransactionID = this.BillingTransactionID; 

        //                        ////        oPaymentInsuranceLineResonCode.BillingTransactionDetailID = this.BillingTransactionDetailID; 

        //                        ////        oPaymentInsuranceLineResonCode.TrackBillingTransactionID = this.TrackTrnID; 

        //                        ////        oPaymentInsuranceLineResonCode.TrackBillingTransactionDetailID = this.TrackTrnDtlID;

        //                        ////        oPaymentInsuranceLineResonCode.ReasonCode = drLineReasonRow["sReasonCode"].ToString();

        //                        ////        oPaymentInsuranceLineResonCode.ReasonDescription = drLineReasonRow["sReasonDescription"].ToString();

        //                        ////        oPaymentInsuranceLineResonCode.ReasonAmount = Convert.ToDecimal(drLineReasonRow["dReasonAmount"].ToString());  oPaymentInsuranceLineResonCode.IsNullReasonAmount = false; 

        //                        ////        oPaymentInsuranceLineResonCode.ClinicID = AppSettings.ClinicID; 

        //                        ////        oPaymentInsuranceLineResonCode.CloseDate = oPaymentInsurace.CloseDate;
        //                        ////        oPaymentInsuranceLineResonCode.HasData = true;
        //                        ////        oPaymentInsuranceLineResonCode.CommentType = EOBCommentType.Reason;

        //                        ////        oPaymentInsuranceLine.LineResonCodes.Add(oPaymentInsuranceLineResonCode);
        //                        ////    }
        //                        ////}
        //                        #endregion

        //                        #endregion " Set Line Reason Codes "

        //                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                                ActivityType.SetLineReasonCodes, "SetLineReasonCodes - End", ActivityOutCome.Success);

        //                        DataTable dtChargeLineNotes = null;

        //                        dtChargeLineNotes = dsTempPost.Tables[4];

        //                        object[] oParamLineStatementNotes = { EOBPaymentType.InsuracePayment.GetHashCode(), this.ClaimNo, 
        //                                                                this.SubclaimNo, EOBPaymentSubType.StatementNote.GetHashCode(), 
        //                                                                this.TrackTrnDtlID, this.BillingTransactionDetailID };

        //                        sQueryFilter = String.Format("nPaymentNoteType = {0} and nClaimNo =  {1} and sSubClaimNo = '{2}' and nPaymentNoteSubType = {3} and nTrackTrnDtlID = {4} and nBillingTransactionDetailID = {5} ", oParamLineStatementNotes);

        //                        DataRow[] dRowLines = dtChargeLineNotes.Select(sQueryFilter);

        //                        #region " Statement Notes & Internal Notes for Line "

        //                        //if (c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTE)).Trim() != "")
        //                        if (dRowLines != null) // Method required to verify and get line statement notes.
        //                        {
        //                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                                ActivityType.StatementNotes, "StatementNotes - Start", ActivityOutCome.Success);

        //                            foreach (DataRow dRowLine in dRowLines)
        //                            {

        //                                EOBPayment.Common.PaymentInsuranceLineNote oPaymentInsuranceLineNote = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineNote();

        //                                oPaymentInsuranceLineNote.ClaimNo = this.ClaimNo; // (Int64)dRow["nClaimNo"]; // Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
        //                                oPaymentInsuranceLineNote.SubClaimNo = this.SubclaimNo; // Convert.ToString(dRow["nSubClaimNo"]);
        //                                oPaymentInsuranceLineNote.EOBPaymentID = this.EOBPaymentID; //oPaymentInsurace.EOBPaymentID; //_EOBPaymentID; //CheckDev66
        //                                oPaymentInsuranceLineNote.EOBID = 0;
        //                                oPaymentInsuranceLineNote.EOBPaymentDetailID = 0;

        //                                oPaymentInsuranceLineNote.BillingTransactionID = this.BillingTransactionID; // (Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
        //                                oPaymentInsuranceLineNote.BillingTransactionDetailID = this.BillingTransactionDetailID; // (Int64)dRow["nBillingTransactionDetailID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));

        //                                oPaymentInsuranceLineNote.TrackBillingTransactionID = this.TrackTrnID; // (Int64)dRow["nTrackBLTransactionID"];  //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
        //                                oPaymentInsuranceLineNote.TrackBillingTransactionDetailID = this.TrackTrnDtlID; // (Int64)dRow["nTrackBLTransactionDetailID"];  //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));

        //                                oPaymentInsuranceLineNote.Code = dRowLine["nNoteCode"].ToString();
        //                                // Dev66 - Required clarification.
        //                                oPaymentInsuranceLineNote.Description = dRowLine["nNoteDescription"].ToString();
        //                                oPaymentInsuranceLineNote.Amount = Convert.ToInt64(dRowLine["dNoteAmount"].ToString());
        //                                //////if (c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTEONPRINT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTEONPRINT)).Trim() != "")
        //                                //////{ oPaymentInsuranceLineNote.IncludeOnPrint = Convert.ToBoolean(c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTEONPRINT)); }

        //                                oPaymentInsuranceLineNote.IncludeOnPrint = false;

        //                                oPaymentInsuranceLineNote.ClinicID = AppSettings.ClinicID;
        //                                oPaymentInsuranceLineNote.PaymentNoteType = EOBPaymentType.InsuracePayment;
        //                                oPaymentInsuranceLineNote.PaymentNoteSubType = EOBPaymentSubType.StatementNote;
        //                                oPaymentInsuranceLineNote.HasData = true;
        //                                oPaymentInsuranceLineNote.CloseDate = this.CloseDate;//oPaymentInsurace.CloseDate;
        //                                oPaymentInsuranceLineNote.UserId = AppSettings.UserID;

        //                                oPaymentInsuranceLine.LineNotes.Add(oPaymentInsuranceLineNote);
        //                                //oPaymentInsurace.Notes.Add(oPaymentInsuranceLineNote);
        //                                oPaymentInsuranceLineNote.Dispose();
        //                            }

        //                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                                ActivityType.StatementNotes, "StatementNotes - End", ActivityOutCome.Success);
        //                        }

        //                        oParamLineStatementNotes = null;

        //                        object[] oParamLineInternalNotes = { EOBPaymentType.InsuracePayment.GetHashCode(), this.ClaimNo, 
        //                                                               this.SubclaimNo, EOBPaymentSubType.InternalNote.GetHashCode(), 
        //                                                               this.TrackTrnDtlID, this.BillingTransactionDetailID };

        //                        sQueryFilter = String.Format("nPaymentNoteType = {0} and nClaimNo =  {1} and sSubClaimNo = '{2}' and nPaymentNoteSubType = {3} and nTrackTrnDtlID = {4} and nBillingTransactionDetailID = {5} ", oParamLineInternalNotes);

        //                        dRowLines = dtChargeLineNotes.Select(sQueryFilter);

        //                        ////if (c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTE)).Trim() != "")
        //                        if (dRowLines != null) // Method required to verify and get line Internal notes.
        //                        {
        //                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                                ActivityType.InternalNotes, "InternalNotes - Start", ActivityOutCome.Success);

        //                            foreach (DataRow dRowLine in dRowLines)
        //                            {
        //                                EOBPayment.Common.PaymentInsuranceLineNote oPaymentInsuranceLineNote = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineNote();

        //                                oPaymentInsuranceLineNote.ClaimNo = this.ClaimNo;// (Int64)dRow["nClaimNo"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
        //                                oPaymentInsuranceLineNote.SubClaimNo = this.SubclaimNo;// Convert.ToString(dRow["nSubClaimNo"]); //Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));

        //                                oPaymentInsuranceLineNote.EOBPaymentID = this.EOBPaymentID; // oPaymentInsurace.EOBPaymentID; //_EOBPaymentID; //CheckDev66
        //                                oPaymentInsuranceLineNote.EOBID = 0;
        //                                oPaymentInsuranceLineNote.EOBPaymentDetailID = 0;

        //                                oPaymentInsuranceLineNote.BillingTransactionID = this.BillingTransactionID;// (Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
        //                                oPaymentInsuranceLineNote.BillingTransactionDetailID = this.BillingTransactionDetailID; // (Int64)dRow["nBillingTransactionDetailID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));

        //                                oPaymentInsuranceLineNote.TrackBillingTransactionID = this.TrackTrnID; // (Int64)dRow["nTrackBLTransactionID"];  //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
        //                                oPaymentInsuranceLineNote.TrackBillingTransactionDetailID = this.TrackTrnID;// (Int64)dRow["nTrackBLTransactionDetailID"];  //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));

        //                                oPaymentInsuranceLineNote.Code = dRowLine["nNoteCode"].ToString();
        //                                // Dev66 - Required clarification.
        //                                oPaymentInsuranceLineNote.Description = dRowLine["nNoteDescription"].ToString();// Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTE)).Trim();
        //                                oPaymentInsuranceLineNote.Amount = Convert.ToInt64(dRowLine["dNoteAmount"].ToString()); ;
        //                                //if (c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTEONPRINT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTEONPRINT)).Trim() != "")
        //                                //{ oPaymentInsuranceLineNote.IncludeOnPrint = Convert.ToBoolean(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTEONPRINT)); }

        //                                oPaymentInsuranceLineNote.IncludeOnPrint = false; // Convert.ToBoolean(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTEONPRINT)); 

        //                                oPaymentInsuranceLineNote.ClinicID = AppSettings.ClinicID;
        //                                oPaymentInsuranceLineNote.PaymentNoteType = EOBPaymentType.InsuracePayment;
        //                                oPaymentInsuranceLineNote.PaymentNoteSubType = EOBPaymentSubType.InternalNote;
        //                                oPaymentInsuranceLineNote.HasData = true;
        //                                oPaymentInsuranceLineNote.CloseDate = this.CloseDate;// oPaymentInsurace.CloseDate;
        //                                oPaymentInsuranceLineNote.UserId = AppSettings.UserID;

        //                                //oPaymentInsurace.Notes.Add(oPaymentInsuranceLineNote);
        //                                oPaymentInsuranceLine.LineNotes.Add(oPaymentInsuranceLineNote);
        //                                oPaymentInsuranceLineNote.Dispose();
        //                            }

        //                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                                ActivityType.InternalNotes, "InternalNotes - End", ActivityOutCome.Success);
        //                        }

        //                        oParamLineInternalNotes = null;
        //                        dRowLines = null;
        //                        dtChargeLineNotes = null;

        //                        #endregion " Statement Notes & Internal Notes for Line "

        //                        oPaymentInsuranceLine.InsCompanyID = SelectedInsuranceCompanyID; //SelectedInsuranceCompanyID;//Convert.ToInt64(lblInsCompany.Tag);

        //                        #endregion

        //                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                                ActivityType.DebitServiceLine, "DebitServiceLine - Start", ActivityOutCome.Success);

        //                        #region "Debit Service Line - Insurance - Allocation from Current Check, Correction/Takeback, Use Reserved"

        //                        string Amt_Payment = this.Payment;
        //                        string Amt_Last_Payment = GetLastPayment();

        //                        // Verify Payment amount is available or not
        //                        // Verify Last Payment amount is available or not
        //                        // Do subtraction(Check for the payment amount is not zero or less than zero).

        //                        //if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PAYMENT)).Trim() != "")
        //                        if (Amt_Payment.Trim() != "") // check for payment is available
        //                        {
        //                            //if (c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_PAYMENT)).Trim() != "")
        //                            if (Amt_Last_Payment.Trim() != "") // check for last payment is available
        //                            {

        //                                //..Code changes done by sagar ghodke .. on 20100322 to resolve save of zero payment debit line
        //                                //below commented condition is previous
        //                                //if ((Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT))) > 0)
        //                                //if ((Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT))) >= 0)

        //                                // Verify the difference is Greater than zero.
        //                                // Calculate difference between (payment) - (last payment)
        //                                if ((Convert.ToDecimal(Amt_Payment) - Convert.ToDecimal(Amt_Last_Payment)) >= 0)
        //                                {
        //                                    decimal _fillPayAmt = 0; decimal _fillResAmt = 0;
        //                                    Int64 _fillResPayID = 0; Int64 _fillResPayDtlID = 0;
        //                                    Int64 _fillRefPayID = 0; Int64 _fillRefPayDtlID = 0;
        //                                    //int _fillrPayIndx = -1;
        //                                    int _fillRefFinanceLieNo = 0;
        //                                    bool _fillUseRefFinanceLieNo = false;
        //                                    bool _isNullfillPayAmt = false;


        //                                    //if no correction then direct current new amount
        //                                    //if negative correction then it will not come in this loop
        //                                    //if positive correction then only correction amount, but in grid user will enter total amount not correction amount
        //                                    //thats why below we have to calculate amount = last amount - current payment


        //                                    //Code commented by Dev66
        //                                    /*
        //                                    if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null)
        //                                    { _fillPayAmt = (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT))); }
        //                                    else if (c1SinglePayment.GetData(i, COL_PAYMENT) != null)
        //                                    { _fillPayAmt = (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT))); }
        //                                    else if (c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null)
        //                                    { _fillPayAmt = 0 - (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT))); }
        //                                    else
        //                                    { _isNullfillPayAmt = true; }
        //                                    */


        //                                    if (Convert.ToDecimal(Amt_Payment) > 0 && Convert.ToDecimal(Amt_Last_Payment) > 0)
        //                                    { _fillPayAmt = Convert.ToDecimal(Amt_Payment) - Convert.ToDecimal(Amt_Last_Payment); }
        //                                    else if (Convert.ToDecimal(Amt_Payment) > 0)
        //                                    { _fillPayAmt = (Convert.ToDecimal(Amt_Payment)); }
        //                                    else if (Convert.ToDecimal(Amt_Last_Payment) > 0)
        //                                    { _fillPayAmt = 0 - (Convert.ToDecimal(Amt_Last_Payment)); }
        //                                    else
        //                                    { _isNullfillPayAmt = true; }





        //                                    int rPay = 0; //we have to always allocate against check, so rPay value set 0 as its first line in collection
        //                                    _fillResAmt = EOBInsurancePaymentMasterAllocationLines[rPay].Amount;

        //                                    //..Code changes done by Sagar Ghodke on 20100511
        //                                    //..Code changes done to make correct debit entries here unnecessary resid 
        //                                    //..where passed even if the amount is not used from the reserve
        //                                    //..below commented code lines are existing logic

        //                                    //_fillResPayID = EOBInsurancePaymentMasterAllocationLines[rPay].EOBPaymentID;
        //                                    //_fillResPayDtlID = EOBInsurancePaymentMasterAllocationLines[rPay].EOBPaymentDetailID;

        //                                    _fillResPayID = EOBInsurancePaymentMasterAllocationLines[rPay].ReserveEOBPaymentID;
        //                                    _fillResPayDtlID = EOBInsurancePaymentMasterAllocationLines[rPay].ReserveEOBPaymentDetailID;

        //                                    //..End code changes done by Sagar Ghodke on 20100511

        //                                    _fillRefPayID = EOBInsurancePaymentMasterAllocationLines[rPay].RefEOBPaymentID;
        //                                    _fillRefPayDtlID = EOBInsurancePaymentMasterAllocationLines[rPay].RefEOBPaymentDetailID;

        //                                    _fillRefFinanceLieNo = EOBInsurancePaymentMasterAllocationLines[rPay].FinanceLieNo;
        //                                    if (_fillRefPayID == 0 && _fillRefPayDtlID == 0)
        //                                    {
        //                                        _fillUseRefFinanceLieNo = true;
        //                                    }

        //                                    #region "Set object"

        //                                    oEOBInsurancePaymentDetail = new EOBPayment.Common.EOBInsurancePaymentDetail();
        //                                    oEOBInsurancePaymentDetail.EOBPaymentID = 0;
        //                                    oEOBInsurancePaymentDetail.EOBID = 0;
        //                                    oEOBInsurancePaymentDetail.EOBDtlID = 0;
        //                                    oEOBInsurancePaymentDetail.EOBPaymentDetailID = 0;

        //                                    oEOBInsurancePaymentDetail.BillingTransactionID = this.BillingTransactionID;// (Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
        //                                    oEOBInsurancePaymentDetail.BillingTransactionDetailID = this.BillingTransactionDetailID; // (Int64)dRow["nBillingTransactionDetailID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
        //                                    oEOBInsurancePaymentDetail.BillingTransactionLineNo = (Int32)this.BillingTransactionLineNo; // (Int32)dRow["nBillingTransactionLineNo"]; //Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));

        //                                    oEOBInsurancePaymentDetail.TrackBillingTransactionID = this.TrackTrnID;// (Int64)dRow["nTrackBLTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
        //                                    oEOBInsurancePaymentDetail.TrackBillingTransactionDetailID = this.TrackTrnDtlID;// (Int64)dRow["nTrackBLTransactionDetailID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
        //                                    oEOBInsurancePaymentDetail.TrackBillingTransactionLineNo = (Int32)this.BillingTransactionLineNo; // (Int32)dRow["nTrackBLTransactionLineNo"]; //Convert.ToInt32(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_LINENO));
        //                                    oEOBInsurancePaymentDetail.SubClaimNo = this.SubclaimNo;// (string)dRow["nSubClaimNo"]; //Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));

        //                                    oEOBInsurancePaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(this.DOSFrom); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dRow["dDOSFrom"])); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
        //                                    oEOBInsurancePaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(this.DOSTo); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dRow["dDOSTo"]));//gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
        //                                    oEOBInsurancePaymentDetail.CPTCode = this.CPT; // Convert.ToString(dRow["sCPTCode"]);
        //                                    oEOBInsurancePaymentDetail.CPTDescription = this.CPTDescription; // Convert.ToString(dRow["sCPTDescription"]);

        //                                    oEOBInsurancePaymentDetail.Amount = _fillPayAmt;
        //                                    oEOBInsurancePaymentDetail.IsNullAmount = _isNullfillPayAmt;

        //                                    oEOBInsurancePaymentDetail.PaymentType = EOBPaymentType.InsuracePayment;
        //                                    oEOBInsurancePaymentDetail.PaymentSubType = EOBPaymentSubType.Insurace;
        //                                    oEOBInsurancePaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
        //                                    oEOBInsurancePaymentDetail.PayMode = EOBPaymentMode.Check; //SelectedPaymentMode;

        //                                    oEOBInsurancePaymentDetail.RefEOBPaymentID = _fillRefPayID;
        //                                    oEOBInsurancePaymentDetail.RefEOBPaymentDetailID = _fillRefPayDtlID;
        //                                    oEOBInsurancePaymentDetail.ReserveEOBPaymentID = _fillResPayID;
        //                                    oEOBInsurancePaymentDetail.ReserveEOBPaymentDetailID = _fillResPayDtlID;

        //                                    oEOBInsurancePaymentDetail.OldRefEOBPaymentID = _fillRefPayID;
        //                                    oEOBInsurancePaymentDetail.OldRefEOBPaymentDetailID = _fillRefPayDtlID;
        //                                    oEOBInsurancePaymentDetail.OldReserveEOBPaymentID = _fillResPayID;
        //                                    oEOBInsurancePaymentDetail.OldReserveEOBPaymentDetailID = _fillResPayDtlID;

        //                                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
        //                                    oEOBInsurancePaymentDetail.AccountID = SelectedInsuranceCompanyID; //Convert.ToInt64(lblInsCompany.Tag);
        //                                    oEOBInsurancePaymentDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
        //                                    oEOBInsurancePaymentDetail.MSTAccountID = PatientInsuranceID; // Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
        //                                    oEOBInsurancePaymentDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
        //                                    oEOBInsurancePaymentDetail.ContactInsID = nContactID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));

        //                                    oEOBInsurancePaymentDetail.PatientID = this.PatientID;// (Int64)dRow["nPatientID"]; //PatientControl.PatientID;
        //                                    oEOBInsurancePaymentDetail.PaymentTrayID = SelectedPaymentTrayID;
        //                                    oEOBInsurancePaymentDetail.PaymentTrayCode = SelectedPaymentTrayCode;
        //                                    oEOBInsurancePaymentDetail.PaymentTrayDescription = SelectedPaymentTray;
        //                                    oEOBInsurancePaymentDetail.UserID = AppSettings.UserID;
        //                                    oEOBInsurancePaymentDetail.UserName = AppSettings.UserName;
        //                                    oEOBInsurancePaymentDetail.ClinicID = AppSettings.ClinicID;

        //                                    oEOBInsurancePaymentDetail.FinanceLieNo = 0;
        //                                    oEOBInsurancePaymentDetail.MainCreditLineID = 0;
        //                                    oEOBInsurancePaymentDetail.IsMainCreditLine = false;
        //                                    oEOBInsurancePaymentDetail.IsReserveCreditLine = false;
        //                                    oEOBInsurancePaymentDetail.IsCorrectionCreditLine = false;
        //                                    oEOBInsurancePaymentDetail.RefFinanceLieNo = _fillRefFinanceLieNo;
        //                                    oEOBInsurancePaymentDetail.UseRefFinanceLieNo = _fillUseRefFinanceLieNo;

        //                                    //if (mskCloseDate.MaskCompleted == true)
        //                                    //{
        //                                    //mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        //                                    oEOBInsurancePaymentDetail.CloseDate = this.CloseDate; //gloDateMaster.gloDate.DateAsNumber(oPaymentInsurace.CloseDate.ToString()); //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //                                    //}

        //                                    oPaymentInsuranceLine.EOBInsurancePaymentLineDetails.Add(oEOBInsurancePaymentDetail);
        //                                    oEOBInsurancePaymentDetail.Dispose();


        //                                    #endregion
        //                                }
        //                            }
        //                        }
        //                        #endregion

        //                        _Add_WO_WH = false;

        //                        #region "Debit Service Line - WriteOff"

        //                        //if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null)
        //                        if (dRow["nWriteOff"] != null)
        //                        {
        //                            oEOBInsurancePaymentDetail = new EOBPayment.Common.EOBInsurancePaymentDetail();
        //                            oEOBInsurancePaymentDetail.EOBPaymentID = 0;
        //                            oEOBInsurancePaymentDetail.EOBID = 0;
        //                            oEOBInsurancePaymentDetail.EOBDtlID = 0;
        //                            oEOBInsurancePaymentDetail.EOBPaymentDetailID = 0;

        //                            oEOBInsurancePaymentDetail.BillingTransactionID = this.BillingTransactionID; // (Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
        //                            oEOBInsurancePaymentDetail.BillingTransactionDetailID = this.BillingTransactionDetailID; // (Int64)dRow["nBillingTransactionDetailID"];//Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
        //                            oEOBInsurancePaymentDetail.BillingTransactionLineNo = (Int32)this.BillingTransactionLineNo; // (Int32)dRow["nBillingTransactionLineNo"];//Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));

        //                            oEOBInsurancePaymentDetail.SubClaimNo = this.SubclaimNo; // (string)dRow["nSubClaimNo"];//Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
        //                            oEOBInsurancePaymentDetail.TrackBillingTransactionID = this.TrackTrnID;// (Int64)dRow["nTrackBLTransactionID"];//Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
        //                            oEOBInsurancePaymentDetail.TrackBillingTransactionDetailID = this.TrackTrnDtlID; // (Int64)dRow["nTrackBLTransactionDetailID"];//Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
        //                            oEOBInsurancePaymentDetail.TrackBillingTransactionLineNo = (Int32)this.BillingTransactionLineNo;// (Int32)dRow["nTrackBLTransactionLineNo"];//Convert.ToInt32(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_LINENO));

        //                            oEOBInsurancePaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(this.DOSFrom);  //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dRow["dDOSFrom"])); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
        //                            oEOBInsurancePaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(this.DOSTo); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dRow["dDOSTo"])); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
        //                            oEOBInsurancePaymentDetail.CPTCode = this.CPT; // Convert.ToString(dRow["sCPTCode"]);
        //                            oEOBInsurancePaymentDetail.CPTDescription = this.CPTDescription; // Convert.ToString(dRow["sCPTDescription"]);

        //                            //if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null && c1SinglePayment.GetData(i, COL_WRITEOFF).ToString().Trim() != "")
        //                            //{
        //                            //    if (c1SinglePayment.GetData(i, COL_ISCORRECTION) != null && Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISCORRECTION)) == true)
        //                            //    {
        //                            //        oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF))) - Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WRITEOFF)));
        //                            //        if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF))) != Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WRITEOFF))))
        //                            //        {
        //                            //            _Add_WO_WH = true;
        //                            //        }
        //                            //    }
        //                            //    else
        //                            //    {
        //                            //        oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF)));
        //                            //        _Add_WO_WH = true;
        //                            //    }
        //                            //}

        //                            if (this.WriteOff.Trim() != "")
        //                            {
        //                                if (VerifyPaymentCorrection() == true)
        //                                {
        //                                    decimal _last_WriteOff = GetLastWriteOff();
        //                                    oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(this.WriteOff) - _last_WriteOff;
        //                                    if (Convert.ToDecimal(this.WriteOff) != _last_WriteOff)
        //                                    {
        //                                        _Add_WO_WH = true;
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(this.WriteOff);
        //                                    _Add_WO_WH = true;
        //                                }
        //                            }

        //                            oEOBInsurancePaymentDetail.PaymentType = EOBPaymentType.InsuracePayment;
        //                            oEOBInsurancePaymentDetail.PaymentSubType = EOBPaymentSubType.WriteOff;
        //                            oEOBInsurancePaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
        //                            oEOBInsurancePaymentDetail.PayMode = EOBPaymentMode.Check; //SelectedPaymentMode;

        //                            oEOBInsurancePaymentDetail.RefEOBPaymentID = 0;
        //                            oEOBInsurancePaymentDetail.RefEOBPaymentDetailID = 0;
        //                            oEOBInsurancePaymentDetail.OldRefEOBPaymentID = 0;
        //                            oEOBInsurancePaymentDetail.OldRefEOBPaymentDetailID = 0;

        //                            oEOBInsurancePaymentDetail.ReserveEOBPaymentID = 0;
        //                            oEOBInsurancePaymentDetail.ReserveEOBPaymentDetailID = 0;
        //                            oEOBInsurancePaymentDetail.OldReserveEOBPaymentID = 0;
        //                            oEOBInsurancePaymentDetail.OldReserveEOBPaymentDetailID = 0;

        //                            //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
        //                            oEOBInsurancePaymentDetail.AccountID = SelectedInsuranceCompanyID; //Convert.ToInt64(lblInsCompany.Tag);
        //                            oEOBInsurancePaymentDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
        //                            oEOBInsurancePaymentDetail.MSTAccountID = PatientInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
        //                            oEOBInsurancePaymentDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
        //                            oEOBInsurancePaymentDetail.ContactInsID = nContactID;//Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));

        //                            oEOBInsurancePaymentDetail.PatientID = this.PatientID; // (Int64)dRow["nPatientID"]; //PatientControl.PatientID;
        //                            oEOBInsurancePaymentDetail.PaymentTrayID = SelectedPaymentTrayID;
        //                            oEOBInsurancePaymentDetail.PaymentTrayCode = SelectedPaymentTrayCode;
        //                            oEOBInsurancePaymentDetail.PaymentTrayDescription = SelectedPaymentTray;
        //                            oEOBInsurancePaymentDetail.UserID = AppSettings.UserID;
        //                            oEOBInsurancePaymentDetail.UserName = AppSettings.UserName;
        //                            oEOBInsurancePaymentDetail.ClinicID = AppSettings.ClinicID;

        //                            oEOBInsurancePaymentDetail.FinanceLieNo = 0;
        //                            oEOBInsurancePaymentDetail.MainCreditLineID = 0;
        //                            oEOBInsurancePaymentDetail.IsMainCreditLine = false;
        //                            oEOBInsurancePaymentDetail.IsReserveCreditLine = false;
        //                            oEOBInsurancePaymentDetail.IsCorrectionCreditLine = false;
        //                            oEOBInsurancePaymentDetail.RefFinanceLieNo = 0;
        //                            oEOBInsurancePaymentDetail.UseRefFinanceLieNo = false;

        //                            //if (mskCloseDate.MaskCompleted == true)
        //                            //{
        //                            //mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        //                            oEOBInsurancePaymentDetail.CloseDate = this.CloseDate; //gloDateMaster.gloDate.DateAsNumber(this.CloseDate.ToString()); //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //                            //}

        //                            if (_Add_WO_WH == true)
        //                            {
        //                                oPaymentInsuranceLine.EOBInsurancePaymentLineDetails.Add(oEOBInsurancePaymentDetail);
        //                            }
        //                            oEOBInsurancePaymentDetail.Dispose();
        //                        }
        //                        #endregion

        //                        _Add_WO_WH = false;

        //                        #region "Debit Service Line - WithHold"

        //                        //if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null)
        //                        if (dRow["nWithhold"] != null)
        //                        {
        //                            oEOBInsurancePaymentDetail = new EOBPayment.Common.EOBInsurancePaymentDetail();
        //                            oEOBInsurancePaymentDetail.EOBPaymentID = 0;
        //                            oEOBInsurancePaymentDetail.EOBID = 0;
        //                            oEOBInsurancePaymentDetail.EOBDtlID = 0;
        //                            oEOBInsurancePaymentDetail.EOBPaymentDetailID = 0;

        //                            oEOBInsurancePaymentDetail.BillingTransactionID = this.BillingTransactionID;// (Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
        //                            oEOBInsurancePaymentDetail.BillingTransactionDetailID = this.BillingTransactionDetailID; // (Int64)dRow["nBillingTransactionDetailID"];//Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
        //                            oEOBInsurancePaymentDetail.BillingTransactionLineNo = (Int32)this.BillingTransactionLineNo; // (Int32)dRow["nBillingTransactionLineNo"];//Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));

        //                            oEOBInsurancePaymentDetail.SubClaimNo = this.SubclaimNo; // (string)dRow["nSubClaimNo"];//Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
        //                            oEOBInsurancePaymentDetail.TrackBillingTransactionID = this.TrackTrnID;// (Int64)dRow["nTrackBLTransactionID"];//Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
        //                            oEOBInsurancePaymentDetail.TrackBillingTransactionDetailID = this.TrackTrnDtlID;// (Int64)dRow["nTrackBLTransactionDetailID"];//Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
        //                            oEOBInsurancePaymentDetail.TrackBillingTransactionLineNo = (Int32)this.BillingTransactionLineNo; // (Int32)dRow["nTrackBLTransactionLineNo"];//Convert.ToInt32(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_LINENO));

        //                            oEOBInsurancePaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(this.DOSFrom); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
        //                            oEOBInsurancePaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(this.DOSTo); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
        //                            oEOBInsurancePaymentDetail.CPTCode = this.CPT; // Convert.ToString(dRow["sCPTCode"]);  //Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
        //                            oEOBInsurancePaymentDetail.CPTDescription = this.CPTDescription; // Convert.ToString(dRow["sCPTDescription"]); //Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

        //                            //if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null && c1SinglePayment.GetData(i, COL_WITHHOLD).ToString().Trim() != "")
        //                            //{
        //                            //    if (c1SinglePayment.GetData(i, COL_ISCORRECTION) != null && Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISCORRECTION)) == true)
        //                            //    {
        //                            //        oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD))) - Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WITHHOLD)));
        //                            //        if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD))) != Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WITHHOLD))))
        //                            //        {
        //                            //            _Add_WO_WH = true;
        //                            //        }
        //                            //    }
        //                            //    else
        //                            //    {
        //                            //        oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD)));
        //                            //        _Add_WO_WH = true;
        //                            //    }
        //                            //}


        //                            if (this.WithHold.Trim() != "")
        //                            {
        //                                if (VerifyPaymentCorrection() == true)
        //                                {
        //                                    decimal _last_WithHold = GetLastWithHold();
        //                                    oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(this.WithHold) - _last_WithHold;
        //                                    if (Convert.ToDecimal(this.WithHold) != _last_WithHold)
        //                                    {
        //                                        _Add_WO_WH = true;
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(this.WithHold);
        //                                    _Add_WO_WH = true;
        //                                }
        //                            }


        //                            oEOBInsurancePaymentDetail.PaymentType = EOBPaymentType.InsuracePayment;
        //                            oEOBInsurancePaymentDetail.PaymentSubType = EOBPaymentSubType.WithHold;
        //                            oEOBInsurancePaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
        //                            oEOBInsurancePaymentDetail.PayMode = EOBPaymentMode.Check; //SelectedPaymentMode;

        //                            oEOBInsurancePaymentDetail.RefEOBPaymentID = 0;
        //                            oEOBInsurancePaymentDetail.RefEOBPaymentDetailID = 0;
        //                            oEOBInsurancePaymentDetail.OldRefEOBPaymentID = 0;
        //                            oEOBInsurancePaymentDetail.OldRefEOBPaymentDetailID = 0;
        //                            oEOBInsurancePaymentDetail.ReserveEOBPaymentID = 0;
        //                            oEOBInsurancePaymentDetail.ReserveEOBPaymentDetailID = 0;
        //                            oEOBInsurancePaymentDetail.OldReserveEOBPaymentID = 0;
        //                            oEOBInsurancePaymentDetail.OldReserveEOBPaymentDetailID = 0;

        //                            //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
        //                            oEOBInsurancePaymentDetail.AccountID = SelectedInsuranceCompanyID; // Convert.ToInt64(lblInsCompany.Tag);
        //                            oEOBInsurancePaymentDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
        //                            oEOBInsurancePaymentDetail.MSTAccountID = PatientInsuranceID; // Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
        //                            oEOBInsurancePaymentDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
        //                            oEOBInsurancePaymentDetail.ContactInsID = nContactID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));

        //                            oEOBInsurancePaymentDetail.PatientID = this.PatientID; // (Int64)dRow["nPatientID"]; //PatientControl.PatientID;
        //                            oEOBInsurancePaymentDetail.PaymentTrayID = SelectedPaymentTrayID;
        //                            oEOBInsurancePaymentDetail.PaymentTrayCode = SelectedPaymentTrayCode;
        //                            oEOBInsurancePaymentDetail.PaymentTrayDescription = SelectedPaymentTray;
        //                            oEOBInsurancePaymentDetail.UserID = AppSettings.UserID;
        //                            oEOBInsurancePaymentDetail.UserName = AppSettings.UserName;
        //                            oEOBInsurancePaymentDetail.ClinicID = AppSettings.ClinicID;

        //                            oEOBInsurancePaymentDetail.FinanceLieNo = 0;
        //                            oEOBInsurancePaymentDetail.MainCreditLineID = 0;
        //                            oEOBInsurancePaymentDetail.IsMainCreditLine = false;
        //                            oEOBInsurancePaymentDetail.IsReserveCreditLine = false;
        //                            oEOBInsurancePaymentDetail.IsCorrectionCreditLine = false;
        //                            oEOBInsurancePaymentDetail.RefFinanceLieNo = 0;
        //                            oEOBInsurancePaymentDetail.UseRefFinanceLieNo = false;

        //                            //if (mskCloseDate.MaskCompleted == true)
        //                            //{
        //                            //mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        //                            oEOBInsurancePaymentDetail.CloseDate = this.CloseDate; //gloDateMaster.gloDate.DateAsNumber(this.CloseDate.ToString());  //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //                            //}

        //                            if (_Add_WO_WH == true)
        //                            {
        //                                oPaymentInsuranceLine.EOBInsurancePaymentLineDetails.Add(oEOBInsurancePaymentDetail);
        //                            }
        //                            oEOBInsurancePaymentDetail.Dispose();
        //                        }
        //                        #endregion

        //                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                                ActivityType.DebitServiceLine, "DebitServiceLine - End", ActivityOutCome.Success);

        //                        // Get Next Action & Next Party from Temporary Table 
        //                        // Assign it to LineNextAction object.
        //                        DataTable dtNextPartyAction = null;

        //                        dtNextPartyAction = dsTempPost.Tables[6];

        //                        object[] oParams = { this.ClaimNo, this.SubclaimNo, 
        //                                               this.TrackTrnDtlID, this.BillingTransactionDetailID };

        //                        sQueryFilter = String.Format("nClaimNo = {0} and sSubClaimNo = '{1}' and nTrackMstTrnDetailID = {2} and nBillingTransactionDetailID = {3}", oParams);

        //                        DataRow[] dRowNextPartyActions = dtNextPartyAction.Select(sQueryFilter);

        //                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                                ActivityType.SetNextActionDetails, "SetNextActionDetails - Start", ActivityOutCome.Success);

        //                        //nTrackMstTrnDetailID = nTrackTrnDtlId
        //                        //nBillingTransactionDetailID = nBillingTransactionDetailID
        //                        foreach (DataRow dRowNextPartyAction in dRowNextPartyActions)
        //                        {
        //                            this.NextAction = dRowNextPartyAction["NextAction"].ToString();
        //                            this.NextParty = dRowNextPartyAction["NextParty"].ToString();
        //                            this.NextActionContactID = dRowNextPartyAction["nNextActionContactID"].ToString();
        //                            this.NextActionPatientID = dRowNextPartyAction["nNextActionPatientInsID"].ToString();
        //                        }
        //                        dRowNextPartyActions = null;
        //                        dtNextPartyAction = null;

        //                        // Added by Dev66 on 28-June-2010
        //                        // Called this method to set the NextAction & SplitClaim details
        //                        SetNextActionDetails(out oNextActions, out oSplitClaimDtls);


        //                        if (oNextActions.Count > 0)
        //                        {
        //                            oPaymentInsuranceLine.LineNextAction = oNextActions[0];
        //                            oTempNextActions.Add(oNextActions[0]);
        //                        }

        //                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                                ActivityType.SetNextActionDetails, "SetNextActionDetails - End", ActivityOutCome.Success);

        //                        // End



        //                        oPaymentInsuranceClaim.CliamLines.Add(oPaymentInsuranceLine);
        //                        oPaymentInsuranceLine.Dispose();
        //                        //////        }
        //                        //////    }
        //                        //////}

        //                        if (oEOBInsurancePaymentDetail != null) { oEOBInsurancePaymentDetail.Dispose(); }

        //                        #endregion

        //                        oPaymentInsurace.InsuranceClaims.Add(oPaymentInsuranceClaim);
        //                        oPaymentInsuranceClaim.Dispose();
        //                    }

        //                    #endregion " ......................... Claim Payment Details End ................................. "



        //                    #region "Reserve Debit Entry if any and it will goes directlly to payment object with credit line"

        //                    ////////if (IsReserveAdded()) //Check if there is any reserve entry to be made
        //                    ////////{
        //                    ////////    decimal _reserveAmt = 0;
        //                    ////////    //string _reserveNote = "";

        //                    ////////    _reserveAmt = AmountAddedToReserve();

        //                    ////////    //0 ReserveAmount 
        //                    ////////    //1 ReserveNote 
        //                    ////////    //3 ReserveNoteOnPrint 

        //                    ////////    //if (AmountAddedToReserve > 0) //Check for the reserve amount is greater than zero
        //                    ////////    if (_reserveAmt > 0)
        //                    ////////    {
        //                    ////////        oEOBInsurancePaymentReserveDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
        //                    ////////        oEOBInsurancePaymentReserveDetail.EOBPaymentID = 0;
        //                    ////////        oEOBInsurancePaymentReserveDetail.EOBID = 0;
        //                    ////////        oEOBInsurancePaymentReserveDetail.EOBDtlID = 0;
        //                    ////////        oEOBInsurancePaymentReserveDetail.EOBPaymentDetailID = 0;

        //                    ////////        oEOBInsurancePaymentReserveDetail.BillingTransactionID = 0;
        //                    ////////        oEOBInsurancePaymentReserveDetail.BillingTransactionDetailID = 0;
        //                    ////////        oEOBInsurancePaymentReserveDetail.BillingTransactionLineNo = 0;

        //                    ////////        oEOBInsurancePaymentReserveDetail.TrackBillingTransactionID = 0;
        //                    ////////        oEOBInsurancePaymentReserveDetail.TrackBillingTransactionDetailID = 0;
        //                    ////////        oEOBInsurancePaymentReserveDetail.TrackBillingTransactionLineNo = 0;
        //                    ////////        oEOBInsurancePaymentReserveDetail.SubClaimNo = "";

        //                    ////////        //if (mskCloseDate.MaskCompleted == true)
        //                    ////////        //{
        //                    ////////            //mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        //                    ////////        oEOBInsurancePaymentReserveDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(oPaymentInsurace.CloseDate.ToString());  //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //                    ////////        oEOBInsurancePaymentReserveDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(oPaymentInsurace.CloseDate.ToString()); //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //                    ////////        //}
        //                    ////////        oEOBInsurancePaymentReserveDetail.CPTCode = "";
        //                    ////////        oEOBInsurancePaymentReserveDetail.CPTDescription = "";

        //                    ////////        oEOBInsurancePaymentReserveDetail.Amount = _reserveAmt;
        //                    ////////        oEOBInsurancePaymentReserveDetail.IsNullAmount = false;

        //                    ////////        oEOBInsurancePaymentReserveDetail.PaymentType = EOBPaymentType.InsuraceReserverd;
        //                    ////////        oEOBInsurancePaymentReserveDetail.PaymentSubType = EOBPaymentSubType.Reserved;
        //                    ////////        oEOBInsurancePaymentReserveDetail.PaySign = EOBPaymentSign.Receipt_Debit;
        //                    ////////        oEOBInsurancePaymentReserveDetail.PayMode = EOBPaymentMode.Check; //SelectedPaymentMode;

        //                    ////////        oEOBInsurancePaymentReserveDetail.RefEOBPaymentID = oEOBInsPaymentCreditDetail.RefEOBPaymentID;
        //                    ////////        oEOBInsurancePaymentReserveDetail.RefEOBPaymentDetailID = oEOBInsPaymentCreditDetail.RefEOBPaymentDetailID;
        //                    ////////        oEOBInsurancePaymentReserveDetail.ReserveEOBPaymentID = oEOBInsPaymentCreditDetail.ReserveEOBPaymentID;
        //                    ////////        oEOBInsurancePaymentReserveDetail.ReserveEOBPaymentDetailID = oEOBInsPaymentCreditDetail.ReserveEOBPaymentDetailID;

        //                    ////////        oEOBInsurancePaymentReserveDetail.OldRefEOBPaymentID = oEOBInsPaymentCreditDetail.RefEOBPaymentID;
        //                    ////////        oEOBInsurancePaymentReserveDetail.OldRefEOBPaymentDetailID = oEOBInsPaymentCreditDetail.RefEOBPaymentDetailID;
        //                    ////////        oEOBInsurancePaymentReserveDetail.OldReserveEOBPaymentID = oEOBInsPaymentCreditDetail.ReserveEOBPaymentID;
        //                    ////////        oEOBInsurancePaymentReserveDetail.OldReserveEOBPaymentDetailID = oEOBInsPaymentCreditDetail.ReserveEOBPaymentDetailID;


        //                    ////////        //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID

        //                    ////////        oEOBInsurancePaymentReserveDetail.AccountID = oPaymentInsurace.PayerID;
        //                    ////////        oEOBInsurancePaymentReserveDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
        //                    ////////        oEOBInsurancePaymentReserveDetail.MSTAccountID = EOBPaymentTypeAccountNo.InsuraceReserverd.GetHashCode();
        //                    ////////        oEOBInsurancePaymentReserveDetail.MSTAccountType = EOBPaymentAccountType.Reserved;
        //                    ////////        oEOBInsurancePaymentReserveDetail.PatientID = (Int64) dRow["nPatientID"]; //PatientControl.PatientID;
        //                    ////////        oEOBInsurancePaymentReserveDetail.PaymentTrayID = oPaymentInsurace.PaymentTrayID;//SelectedPaymentTrayID;
        //                    ////////        oEOBInsurancePaymentReserveDetail.PaymentTrayCode = oPaymentInsurace.PaymentTrayCode;//SelectedPaymentTrayCode;
        //                    ////////        oEOBInsurancePaymentReserveDetail.PaymentTrayDescription = oPaymentInsurace.PaymentTrayDesc;//SelectedPaymentTray;
        //                    ////////        oEOBInsurancePaymentReserveDetail.UserID = AppSettings.UserID;
        //                    ////////        oEOBInsurancePaymentReserveDetail.UserName = AppSettings.UserName;
        //                    ////////        oEOBInsurancePaymentReserveDetail.ClinicID = AppSettings.ClinicID;
        //                    ////////        oEOBInsurancePaymentReserveDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(oPaymentInsurace.CloseDate.ToString());  //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //                    ////////        oEOBInsurancePaymentReserveDetail.FinanceLieNo = 0;
        //                    ////////        oEOBInsurancePaymentReserveDetail.MainCreditLineID = 0;
        //                    ////////        oEOBInsurancePaymentReserveDetail.IsMainCreditLine = false;
        //                    ////////        oEOBInsurancePaymentReserveDetail.IsReserveCreditLine = false;
        //                    ////////        oEOBInsurancePaymentReserveDetail.IsCorrectionCreditLine = false;
        //                    ////////        oEOBInsurancePaymentReserveDetail.RefFinanceLieNo = 1;
        //                    ////////        oEOBInsurancePaymentReserveDetail.UseRefFinanceLieNo = true;

        //                    ////////        //0 ReserveAmount 
        //                    ////////        //1 ReserveNote 
        //                    ////////        //2 ReserveSubType 
        //                    ////////        //3 ReserveNoteOnPrint 

        //                    ////////        #region "General Note"

        //                    ////////        EOBPayment.Common.PaymentInsuranceLineNote oPaymentInsuranceLineNote = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineNote();

        //                    ////////        oPaymentInsuranceLineNote.ClaimNo = 0;
        //                    ////////        oPaymentInsuranceLineNote.EOBPaymentID = 0;
        //                    ////////        oPaymentInsuranceLineNote.EOBID = 0;
        //                    ////////        oPaymentInsuranceLineNote.EOBPaymentDetailID = 0;
        //                    ////////        oPaymentInsuranceLineNote.BillingTransactionID = 0;
        //                    ////////        oPaymentInsuranceLineNote.BillingTransactionDetailID = 0;

        //                    ////////        oPaymentInsuranceLineNote.SubClaimNo = "";
        //                    ////////        oPaymentInsuranceLineNote.TrackBillingTransactionID = 0;
        //                    ////////        oPaymentInsuranceLineNote.TrackBillingTransactionDetailID = 0;
        //                    ////////        oPaymentInsuranceLineNote.TrackBillingTransactionLineNo = 0;

        //                    ////////        oPaymentInsuranceLineNote.Code = "";
        //                    ////////        oPaymentInsuranceLineNote.Description = ReserveNote();
        //                    ////////        oPaymentInsuranceLineNote.Amount = AmountAddedToReserve();
        //                    ////////        oPaymentInsuranceLineNote.IncludeOnPrint = false;
        //                    ////////        oPaymentInsuranceLineNote.ClinicID = AppSettings.ClinicID;
        //                    ////////        oPaymentInsuranceLineNote.PaymentNoteType = EOBPaymentType.InsuraceReserverd;
        //                    ////////        oPaymentInsuranceLineNote.PaymentNoteSubType = EOBPaymentSubType.Reserved;
        //                    ////////        oPaymentInsuranceLineNote.HasData = true;
        //                    ////////        oPaymentInsuranceLineNote.CloseDate = oPaymentInsurace.CloseDate;
        //                    ////////        oPaymentInsuranceLineNote.UserId = AppSettings.UserID;

        //                    ////////        oEOBInsurancePaymentReserveDetail.LineNotes.Add(oPaymentInsuranceLineNote);
        //                    ////////        oPaymentInsuranceLineNote.Dispose();

        //                    ////////        #endregion

        //                    ////////        oPaymentInsurace.EOBInsurancePaymentReserveLineDetail.Add(oEOBInsurancePaymentReserveDetail);
        //                    ////////        oEOBInsurancePaymentReserveDetail.Dispose();

        //                    ////////        //EOBInsurancePaymentMasterAllocationLines.Add();
        //                    ////////    }
        //                    ////////}
        //                    #endregion

        //                    #region "On hold selection for splitted claims "

        //                    //gloSplitClaim ogloSplitClaim = null;
        //                    //DataTable _dtHoldInfo = GetSplittedClaimsHoldInfo(oSplitClaimDetails);

        //                    #endregion

        //                    EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new EOBPayment.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);

        //                    //this.EOBPaymentID = ogloEOBPaymentInsurance.SaveSplitEOB(oPaymentInsurace, false, out _outEOBid);
        //                    ogloEOBPaymentInsurance.Dispose();
        //                    #endregion



        //                    ////// Created a new instance for storing the nextaction details which needs to be saved
        //                    ////EOBPayment.Common.PaymentInsuranceLineNextActions oNextActions = new EOBPayment.Common.PaymentInsuranceLineNextActions();

        //                    ////// Created a new instance for split claim logic
        //                    ////SplitClaimDetails oSplitClaimDtls = null;
        //                    ////oSplitClaimDtls = new SplitClaimDetails();

        //                    // Called this method to set the NextAction & SplitClaim details
        //                    //SetNextActionDetails(out oNextActions, out oSplitClaimDtls);
        //                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                             ActivityType.CreateERAPaymentObject, "CreateERAPaymentObject - Start ", ActivityOutCome.Success);

        //                    oSplitClaimDtls2.Lines.Add(oSplitClaimDtls.Lines[0]);

        //                    oPayment = new ERAPayment();
        //                    oPayment.PaymentInsurance = oPaymentInsurace;
        //                    oPayment.PaymentInsuranceLineNextActions = oTempNextActions;
        //                    oPayment.SplitClaimDetails = oSplitClaimDtls2;


        //                    oNextActions.Dispose();
        //                    oSplitClaimDtls.Dispose();

        //                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                             ActivityType.CreateERAPaymentObject, "CreateERAPaymentObject - End ", ActivityOutCome.Success);

        //                } // Charges loop ends

        //                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                        ActivityType.ChargeIterationEnd, "ChargeIterationEnd - End", ActivityOutCome.Success);


        //                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                             ActivityType.AddERAPaymentToListObject, "AddERAPaymentToListObject - Start ", ActivityOutCome.Success);

        //                oPaymentList.Add(oPayment);
        //                //oPaymentInsurances.Add(oPaymentInsurace);
        //                oPaymentInsurace.Dispose();
        //                drChargeLines = null;


        //                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                             ActivityType.AddERAPaymentToListObject, "AddERAPaymentToListObject - End ", ActivityOutCome.Success);
        //            }
        //        }

        //        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                        ActivityType.ClaimIterationEnd, "ClaimIterationEnd - End", ActivityOutCome.Success);

        //        Int64 _nEOBID = 0;

        //        #region " Save EOB Payment & Next action "

        //        bool bFlag = false;
        //        bool bIsSaved = false;
        //        bool isEOBSaved = false;
        //        Int64 _TempEOBID = 0;
        //        // Pass nBPRID to save the data in Temporary table for reference.

        //        if (oPaymentList.Count > 0)
        //        {

        //            using (System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(AppSettings.ConnectionStringPM))
        //            {



        //                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                             ActivityType.SaveOperationStarts, "SaveOperationStarts - Start ", ActivityOutCome.Success);

        //                foreach (ERAPayment oERAPay in oPaymentList)
        //                {
        //                    //bool IsNextActionUpdated = false;

        //                    if (oERAPay.PaymentInsurance != null)
        //                    {
        //                        bIsSaved = false;
        //                        if (bFlag == false)
        //                        {
        //                            bIsSaved = true;
        //                            bFlag = true;
        //                        }

        //                        if (_sqlConnection.State != ConnectionState.Open)
        //                        {
        //                            _sqlConnection.Open();
        //                            _sqlTransaction = _sqlConnection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

        //                        }

        //                        EOBPaymentID = InsurancePayment.SaveEOBPayment(_sqlConnection, _sqlTransaction, oERAPay.PaymentInsurance, false, out _nEOBID, out isEOBSaved, bIsSaved, _TempEOBID);
        //                    }
        //                    _TempEOBID = EOBPaymentID;
        //                    //if (IsNextActionUpdated)
        //                    //{
        //                    EOBPayment.Common.PaymentInsuranceLineNextActions Notes = oERAPay.PaymentInsuranceLineNextActions;
        //                    if (Notes.Count > 0) // Check any object available for Next Action & Next Party.
        //                    {
        //                        isEOBSaved = true;
        //                        InsurancePayment.SaveEOBNextAction(_sqlConnection, _sqlTransaction, ref Notes, EOBPaymentID, _nEOBID, out isEOBSaved);
        //                    }
        //                    //}
        //                    if (isEOBSaved)
        //                    {
        //                        // Update Check Status 
        //                        // ClsERAValidation.UpdateCheckStatus(nBPRID, enumCheckStatus.Posted.GetHashCode());  // CheckStatus = (2 - Posted)
        //                        _sqlTransaction.Commit();
        //                        oStopFlag = StopFlag.Passed;
        //                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                                 ActivityType.SaveOperationEnds, "SaveOperationEnds", ActivityOutCome.Success);
        //                        bReturn = true;
        //                    }
        //                    else
        //                    {
        //                        oStopFlag = StopFlag.Error;
        //                        _sqlTransaction.Rollback(); throw new Exception("Unable to save payment");
        //                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
        //                                 ActivityType.SaveOperationAborted, "SaveOperationAborted ", ActivityOutCome.Failure);
        //                        sMessage = "Unable to save payment";
        //                        bReturn = false;
        //                    }

        //                    _sqlConnection.Close();

        //                    if (isEOBSaved)
        //                    {
        //                        #region " Split claim logic "

        //                        DataTable _dtHoldInfo = GetSplittedClaimsHoldInfo(oERAPay.SplitClaimDetails);
        //                        if (oERAPay.SplitClaimDetails != null && oERAPay.SplitClaimDetails.Lines.Count > 0)
        //                        {
        //                            bool _splitFlag = false;
        //                            gloSplitClaim ogloSplitClaim = new gloSplitClaim(AppSettings.ConnectionStringPM);

        //                            //// Set the claim remittance ref no to the split object
        //                            //oSplitClaimDetails.ClaimRemittanceReferenceNo = ClaimRemittanceReferenceNo;

        //                            //// Payment allocation check added to resolved the following issue
        //                            //// Issue : if pending is selected, split should not happened.
        //                            //if (IsPaymentAllocated)
        //                            //{ oSplitClaimDetails.IsPaymentDone = true; }
        //                            //else
        //                            //{ oSplitClaimDetails.IsPaymentDone = false; }


        //                            oERAPay.SplitClaimDetails.EOBPaymentID = EOBPaymentID;
        //                            oERAPay.SplitClaimDetails.EOBID = _nEOBID;

        //                            ////_splitFlag = ogloSplitClaim.SplitTransactionClaim(oSplitClaimDetails);
        //                            _splitFlag = ogloSplitClaim.SplitTransactionClaim(oERAPay.SplitClaimDetails, _dtHoldInfo);
        //                            ogloSplitClaim.Dispose();
        //                        }

        //                        #endregion " Split claim logic "

        //                        #region " Save NextAction History "

        //                        isEOBSaved = false;
        //                        ClsERASave.SaveEOBNextActionHistory(ref Notes, EOBPaymentID, _nEOBID, out isEOBSaved);

        //                        #endregion

        //                        sMessage = String.Format(" {0} Claims Processed", iClaimCount);
        //                    }

        //                }


        //            }
        //        }

        //        #endregion


        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //        oStopFlag = StopFlag.Error;
        //        sMessage = "Error while payment, Payment not done";
        //        bReturn = false;
        //    }
        //    finally
        //    {
        //        if (oPaymentInsurace != null) { oPaymentInsurace.Dispose(); }
        //        if (oEOBInsPaymentCreditDetail != null) { oEOBInsPaymentCreditDetail.Dispose(); }
        //        if (oPaymentInsuranceClaim != null) { oPaymentInsuranceClaim.Dispose(); }
        //        if (oEOBInsurancePaymentReserveDetail != null) { oEOBInsurancePaymentReserveDetail.Dispose(); }
        //        if (oSplitClaimDetails != null) { oSplitClaimDetails.Dispose(); }
        //        if (dtBPRClaims != null) { dtBPRClaims.Dispose(); }
        //        if (oPaymentList != null) { oPaymentList = null; }
        //        if (dtCheckDetails != null) { dtCheckDetails.Dispose(); }
        //        if (dtClaimDetails != null) { dtClaimDetails.Dispose(); }
        //        if (_sqlTransaction != null) { _sqlTransaction.Dispose(); }
        //        if (sb != null) { sb = null; }
        //        if (dsTempPost != null) { dsTempPost = null; }
        //        if (dtClaims != null) { dtClaims = null; }
        //        if (dtCheckDetails != null) { dtCheckDetails.Dispose(); }
        //        if (dtClaimDetails != null) { dtClaimDetails.Dispose(); }
        //        if (dtDistinctClaims != null) { dtDistinctClaims.Dispose(); }
        //        if (dtClaimReasonCodes != null) { dtClaimReasonCodes.Dispose(); }

        //    }
        //    return bReturn;
        //}

        //#endregion

        private void LockUnlockClaim(Int64 nClaimNo, bool IsLock)
        {
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Add("@nClaimNo", nClaimNo, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@bIsLock", IsLock, ParameterDirection.Input, SqlDbType.Bit);
                    oDBPara.Add("@sMachineName", Environment.MachineName, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBPara.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Execute("BL_LockUnlockClaims", oDBPara);
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private Int64 GetLastTransactionId(String nClaimNo)
        {
            Int64 _TransactionID = 0;
            string _sqlQuery = string.Empty;
            object _retVal = null;

            try
            {
                if(OpenConnection(false))
                 {
                    _sqlQuery = "SELECT DBO.ERA_GetLastTransactionMSTID('" + nClaimNo + "')";

                    oDB.Connect(false);
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "")
                    { _TransactionID = Convert.ToInt64(_retVal); }
                    oDB.Disconnect();
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
            }
            return _TransactionID;
        }

        #region " ERA New Method for Original Save. "

        public bool PostERAFile_New(Int64 nBPRID, Int64 nTrayID, string sCloseDate, out string sMessage, out StopFlag oStopFlag, ref ProgressBar oProgress, ref Label oLabel)
        {
            EOBInsurancePaymentMasterLines = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines();
            EOBInsurancePaymentMasterAllocationLines = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines();

            bool bReturn = false;
            int _financeLineNo = 0;
            //bool IsReserveUsed = false;
            //Int64 SelectedInsuranceCompanyID = 0; 

            DataSet dsTempPost = null;
            DataTable dtBPRClaims = null;
            DataTable dtClaims = null;
            DataTable dtCheckDetails = null;
            DataTable dtClaimDetails = null;
            DataTable dtDistinctClaims = null;
            DataTable dtClaimReasonCodes = null;

            EOBPayment.Common.PaymentInsurance oPaymentInsurace = null;  // Main Payment Master Object
            EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsPaymentCreditDetail = null;  // Main Credit Line Entry Object
            EOBPayment.Common.PaymentInsuranceClaim oPaymentInsuranceClaim = null;
            EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsurancePaymentReserveDetail = null;
            
            SplitClaimDetails oSplitClaimDetails = new SplitClaimDetails();
            System.Data.SqlClient.SqlTransaction _sqlTransaction = null;

            // Custom Error code 
            // Custom Error Desc 

            sMessage = string.Empty;
            string sQueryFilter = string.Empty;
            StringBuilder sb = new StringBuilder();
            try
            {

                oStopFlag = StopFlag.NotProcessed; 

                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                    ActivityType.GetTemporaryPostedData, "GetTemporaryPostedData - Start", ActivityOutCome.Success);

                // Fetch all Temporary Posted claims against a BPR into DataSet.
                dsTempPost = GetTemporaryPostedData(nBPRID);

                if (!ValidateDataSet(dsTempPost))
                {
                    sMessage = String.Format("Check details not found for the nBPRID: {0} in Temporary Tables. ", nBPRID);

                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                    ActivityType.GetTemporaryPostedData, sMessage, ActivityOutCome.Failure);

                    sMessage = String.Format("No Claims Processed. ", nBPRID);
                    oStopFlag = StopFlag.Passed;

                    return true;
                }
                SetReasonCodes();
                dtCheckDetails = dsTempPost.Tables[0];  // Contains records available in "BL_EOBPayment_MST_ERA"
                dtDistinctClaims = dsTempPost.Tables[1];  //// Fetch distinct posted claims against check(Contains records avialable in "BL_EOBPayment_DTL_ERA")
                dtClaims = dsTempPost.Tables[2];  // Fetch claims against check(Contains records avialable in "BL_EOBPayment_DTL_ERA")
                dtClaimReasonCodes = dsTempPost.Tables[5];

                if (!GetClaimCount(dtDistinctClaims))
                {
                    sMessage = String.Format("No Claims Processed. ", nBPRID);
                    oStopFlag = StopFlag.Passed;

                    return true;
                }


                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                    ActivityType.GetTemporaryPostedData, "GetTemporaryPostedData - End", ActivityOutCome.Success);


                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                    ActivityType.FillMasterDetails, "FillMasterDetails - Start", ActivityOutCome.Success);


                // Assign Check details into the local Properties/Varible 
                FillMasterDetails(dtCheckDetails);

                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                    ActivityType.FillMasterDetails, "FillMasterDetails - End", ActivityOutCome.Success);

                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                    ActivityType.FillPaymentTray, "FillPaymentTray - Start", ActivityOutCome.Success);

                // Set payment tray information. 
                FillPaymentTray(nTrayID);

                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                    ActivityType.FillPaymentTray, "FillPaymentTray - End", ActivityOutCome.Success);

                if (!GetClaimCount(dtDistinctClaims))
                {
                    //sMessage = String.Format("Claim details not found for the nBPRID: {0} in Temporary Tables. ", nBPRID);

                    sMessage = String.Format("No Claims Processed.");
                    oStopFlag = StopFlag.Passed;
                    return false;
                }


                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                    ActivityType.ClaimIterationStart, "ClaimIterationStart - Start", ActivityOutCome.Success);
                bool IsProcessed = false;
                Int64 iClaimCount = 0;
                // Iterate claims to assign object (oPaymentInsurace)
                oProgress.Value = 0;
                oProgress.Maximum =100;
                oProgress.Minimum = 0;
                oProgress.Visible = true;
                oLabel.Visible = true;
                oLabel.BringToFront();
                foreach (DataRow dtClaimRow in dtDistinctClaims.Rows)
                {
                    RefreshProgress(ref oProgress, ref oLabel, "Preparing Claim " + dtClaimRow["nClaimNo"].ToString().PadLeft(5, '0') + " ");
                    iClaimCount++;

                    Int64 nContactID = Convert.ToInt64(dtClaimRow["nContactID"]);   // InsurancePlanID (return from DeterminePayer)
                    Int64 nClaimNo = Convert.ToInt64(dtClaimRow["nClaimNo"]);
                    string nSubClaimNo = dtClaimRow["sSubClaimNo"].ToString();
                    //Int64 nClaimStatus = 0;
                    //Int64 nCLPId = 0;
                    nResponsibilityNo = int.Parse(dtClaimRow["nResponsibilityNo"].ToString());   
                    this.PatientID = Convert.ToInt64(dtClaimRow["nPatientID"]);

                    PatientInsuranceID = Convert.ToInt64(dtClaimRow["nInsuraceID"]);
                    SelectedInsuranceCompanyID = Convert.ToInt64(dtClaimRow["nInsuranceCompanyID"]);
                    ContactInsuranceID = nContactID;

                    if (IsPaymentMade()) // revisit this condition
                    {
                        if (!IsProcessed)
                        {
                            #region " Master Data "

                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                    ActivityType.GetPaymentMaster, "GetPaymentMaster - Start", ActivityOutCome.Success);

                            // Get Payment Master Object
                            oPaymentInsurace = new EOBPayment.Common.PaymentInsurance();
                            oPaymentInsurace = GetPaymentMaster();
                            oPaymentInsurace.PaymentMode = this.PaymentMode;//solving sales force case -GLO2011-0010771
                     

                            this.CloseDate = gloDateMaster.gloDate.DateAsNumber(sCloseDate);
                            oPaymentInsurace.CloseDate = this.CloseDate;

                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                    ActivityType.GetPaymentMaster, "GetPaymentMaster - End", ActivityOutCome.Success);

                            oPaymentInsurace.BPRID = nBPRID;

                            #endregion

                            #region " Check/Cash/etc txtCheckAmount - Main Credit Line Entry "

                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                    ActivityType.GetMainCreditLineEntry, "GetMainCreditLineEntry - Start", ActivityOutCome.Success);

                            // Get financial line no
                            _financeLineNo = oPaymentInsurace.EOBInsurancePaymentLineDetails.Count + 1;

                            oEOBInsPaymentCreditDetail = GetMainCreditLineEntry(_financeLineNo);  //, EOBInsurancePaymentMasterLines

                            // Add the Main credit line object to main payment object
                            oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentCreditDetail);

                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                    ActivityType.GetMainCreditLineEntry, "GetMainCreditLineEntry - End", ActivityOutCome.Success);

                            #endregion

                            oPaymentInsurace.bIsERAPayment = true;

                            IsProcessed = true;
                        }



                        // Get Claims against against check.
                        // Iterate claims 
                        sQueryFilter = String.Format(" nClaimNo = {0} and sSubClaimNo = '{1}' ", nClaimNo, nSubClaimNo);
                        DataRow[] drChargeLines = dtClaims.Select(sQueryFilter, " nBillingTransactionLineNo Asc");


                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                ActivityType.ChargeIterationStart, "ChargeIterationStart - Start", ActivityOutCome.Success);
                        // Created a new instance for split claim logic
                        SplitClaimDetails oSplitClaimDtls = null;
                        SplitClaimDetails oSplitClaimDtls2 = null;
                        oSplitClaimDtls = new SplitClaimDetails();
                        oSplitClaimDtls2 = new SplitClaimDetails();

                        EOBPayment.Common.PaymentInsuranceLineNextActions oTempNextActions = new EOBPayment.Common.PaymentInsuranceLineNextActions();

                        bool bProcessOnce = false;
                        oPaymentInsuranceClaim = new EOBPayment.Common.PaymentInsuranceClaim();
                        foreach (DataRow dRow in drChargeLines)   // Claim Charges
                        {
                            bool bSecondaryAdjudication = false;

                            bSecondaryAdjudication = Convert.ToBoolean(dRow["PostSecondaryAdjustments"]); 

                            // Created a new instance for storing the nextaction details which needs to be saved
                            EOBPayment.Common.PaymentInsuranceLineNextActions oNextActions = new EOBPayment.Common.PaymentInsuranceLineNextActions();



                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                ActivityType.FillClaimDetails, "FillClaimDetails - Start", ActivityOutCome.Success);

                            // Reset Local Properties/Variable before processing a claims.
                            ResetClaimDetails();

                            // Fill claim details into the local Properties/variable for processing a claim.
                            FillClaimDetails(dRow);

                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                ActivityType.FillClaimDetails, "FillClaimDetails - End", ActivityOutCome.Success);


                            // Region added on 22_June_2010(Dev66) to process a ERA claims 
                            #region " Loop through the claim avalable in a single ERA file (Check)".

                            //..*** For correction (if amount -ve) we make credit entry against the cpt to balance cpt amount
                            //..*** & according to new logic we have to make credit line entry against current check with making the 
                            //..** -ve correction amount +ve


                            #region "Correction Line Credit Line Entry - Credit -ve against CPT & Positive Credit line against current check."
                            decimal _crPayAmt = 0;

                            ////// Verify for the line is of type service line
                            ////// Verify for the line column is in correction mode.
                            ////// Verify amount in payment column.

                            if (VerifyPaymentCorrection())
                            {
                                #region "Commented code for Correction"
                                //////if (c1SinglePayment.GetData(nCrIndex, COL_PAYMENT) != null && c1SinglePayment.GetData(nCrIndex, COL_PAYMENT).ToString().Trim() != "")
                                //////{
                                //////    _crPayAmt = Convert.ToDecimal(c1SinglePayment.GetData(nCrIndex, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(nCrIndex, COL_LAST_PAYMENT));

                                //////    if (_crPayAmt < 0)
                                //////    {
                                //////        _crPayAmt = _crPayAmt - (_crPayAmt * 2);
                                //////        _crResPayMode = 0;

                                //////        Int64 _crPatientId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_PATIENTID));
                                //////        Int64 _crBillTrnId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_ID));
                                //////        Int64 _crBillTrnDtlId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_DETAILID));

                                //////        // TO DO : delete as no reference found
                                //////        //Int64 _crBillTrnDtlLineId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_LINENO));
                                //////        //Int64 _crTrackBillTrnId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_ID));
                                //////        //Int64 _crTrackBillTrnDtlId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                //////        //Int64 _crTrackBillTrnDtlLineId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_LINENO));
                                //////        //string _crCptCode = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_CODE)).Trim();

                                //////        //DataTable _nCrDataTable = InsurancePayment.GetCorrectionRefList(_crPayAmt, _crPatientId, _crBillTrnId, _crBillTrnDtlId, this.PatientInsuranceID, this.ContactInsuranceID, this.SelectedInsuranceCompanyID);
                                //////        DataTable _nCrDataTable = InsurancePayment.GetCorrectionRefList(_crPayAmt, _crPatientId, _crBillTrnId, _crBillTrnDtlId, this.PatientInsuranceID, this.ContactInsuranceID);

                                //////        if (_nCrDataTable != null && _nCrDataTable.Rows.Count > 0)
                                //////        {
                                //////            for (int nCrDTIndex = 0; nCrDTIndex <= _nCrDataTable.Rows.Count - 1; nCrDTIndex++)
                                //////            {
                                //////                #region "Set Object to make -ve credit line entry for cpt balance calculation"

                                //////                EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsPaymentCorrAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
                                //////                oEOBInsPaymentCorrAsCreditDetail.EOBPaymentID = _EOBPaymentID;
                                //////                oEOBInsPaymentCorrAsCreditDetail.EOBID = 0;
                                //////                oEOBInsPaymentCorrAsCreditDetail.EOBDtlID = 0;
                                //////                oEOBInsPaymentCorrAsCreditDetail.EOBPaymentDetailID = 0;

                                //////                //...Will be assigning current check payment & payment details id's to Ref. Id.
                                //////                oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentID = 0;
                                //////                oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentDetailID = 0;
                                //////                oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentID = 0;// Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentID"]);
                                //////                oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentDetailID = 0;// Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentDetailID"]);

                                //////                oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]);
                                //////                oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]);
                                //////                oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentID"]);
                                //////                oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentDetailID"]);

                                //////                oEOBInsPaymentCorrAsCreditDetail.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_ID));
                                //////                oEOBInsPaymentCorrAsCreditDetail.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_DETAILID));
                                //////                oEOBInsPaymentCorrAsCreditDetail.BillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_LINENO));

                                //////                oEOBInsPaymentCorrAsCreditDetail.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_SUBCLAIMNO));
                                //////                oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_ID));
                                //////                oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                //////                oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_LINENO));

                                //////                oEOBInsPaymentCorrAsCreditDetail.DOSFrom = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_FROM).ToString()));
                                //////                oEOBInsPaymentCorrAsCreditDetail.DOSTo = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_TO).ToString()));

                                //////                oEOBInsPaymentCorrAsCreditDetail.CPTCode = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_CODE));
                                //////                oEOBInsPaymentCorrAsCreditDetail.CPTDescription = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_DESCRIPTON));
                                //////                oEOBInsPaymentCorrAsCreditDetail.Amount = Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]) - (Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]) * 2);
                                //////                oEOBInsPaymentCorrAsCreditDetail.IsNullAmount = false;
                                //////                oEOBInsPaymentCorrAsCreditDetail.PaymentType = EOBPaymentType.InsuracePayment;
                                //////                oEOBInsPaymentCorrAsCreditDetail.PaymentSubType = EOBPaymentSubType.Correction;
                                //////                oEOBInsPaymentCorrAsCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
                                //////                oEOBInsPaymentCorrAsCreditDetail.PayMode = (EOBPaymentMode)_crResPayMode;

                                //////                //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                //////                oEOBInsPaymentCorrAsCreditDetail.AccountID = SelectedInsuranceCompanyID; //Convert.ToInt64(lblInsCompany.Tag);
                                //////                oEOBInsPaymentCorrAsCreditDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
                                //////                oEOBInsPaymentCorrAsCreditDetail.MSTAccountID = PatientInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                //////                oEOBInsPaymentCorrAsCreditDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
                                //////                oEOBInsPaymentCorrAsCreditDetail.ContactInsID = ContactInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));

                                //////                oEOBInsPaymentCorrAsCreditDetail.PatientID = PatientControl.PatientID;
                                //////                oEOBInsPaymentCorrAsCreditDetail.PaymentTrayID = SelectedPaymentTrayID;
                                //////                oEOBInsPaymentCorrAsCreditDetail.PaymentTrayCode = SelectedPaymentTrayCode;
                                //////                oEOBInsPaymentCorrAsCreditDetail.PaymentTrayDescription = SelectedPaymentTray;
                                //////                oEOBInsPaymentCorrAsCreditDetail.UserID = AppSettings.UserID;
                                //////                oEOBInsPaymentCorrAsCreditDetail.UserName = AppSettings.UserName;
                                //////                oEOBInsPaymentCorrAsCreditDetail.ClinicID = AppSettings.ClinicID;

                                //////                oEOBInsPaymentCorrAsCreditDetail.FinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails.Count + 1;
                                //////                oEOBInsPaymentCorrAsCreditDetail.MainCreditLineID = 0;
                                //////                oEOBInsPaymentCorrAsCreditDetail.IsMainCreditLine = false;
                                //////                oEOBInsPaymentCorrAsCreditDetail.IsReserveCreditLine = false;
                                //////                oEOBInsPaymentCorrAsCreditDetail.IsCorrectionCreditLine = true;
                                //////                oEOBInsPaymentCorrAsCreditDetail.RefFinanceLieNo = 0;
                                //////                oEOBInsPaymentCorrAsCreditDetail.UseRefFinanceLieNo = false;

                                //////                if (mskCloseDate.MaskCompleted == true)
                                //////                {
                                //////                    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                //////                }

                                //////                oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentCorrAsCreditDetail);
                                //////                oEOBInsPaymentCorrAsCreditDetail.Dispose();

                                //////                #endregion

                                //////                #region "Set Object to make +ve credit entry against current check"

                                //////                //---->> 1 = Add Object , 2 = Modify Object , 0 = Do Nothing
                                //////                int _Object_Add_Modify_None = -1;
                                //////                int _Object_Modify_Index = -1;

                                //////                if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
                                //////                {
                                //////                    for (int index = 0; index < EOBInsurancePaymentMasterLines.Count; index++)
                                //////                    {
                                //////                        if (EOBInsurancePaymentMasterLines[index].IsCorrectionCreditLine == true)
                                //////                        {
                                //////                            //1. Check if the correction amount is from the current check if yes do not add object

                                //////                            //2. If correction amount is from different check & the credit line does not exists then 
                                //////                            //   add the +ve credit line entry

                                //////                            //3. If the correction amount is from different check & the credit line exists then
                                //////                            //   modify the credit line entry

                                //////                            if (EOBInsurancePaymentMasterLines[index].RefEOBPaymentID == _EOBPaymentID)
                                //////                            {
                                //////                                _Object_Add_Modify_None = 0;
                                //////                                break;
                                //////                            }
                                //////                            else if (EOBInsurancePaymentMasterLines[index].RefEOBPaymentID == Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"])
                                //////                                && EOBInsurancePaymentMasterLines[index].RefEOBPaymentDetailID == Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]))
                                //////                            {
                                //////                                EOBInsurancePaymentMasterLines[index].Amount += Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]);
                                //////                                _Object_Add_Modify_None = 2;
                                //////                                _Object_Modify_Index = index;
                                //////                                break;
                                //////                            }
                                //////                            else
                                //////                            {
                                //////                                _Object_Add_Modify_None = 1;
                                //////                            }
                                //////                        }
                                //////                    }
                                //////                }
                                //////                else
                                //////                { _Object_Add_Modify_None = 1; }

                                //////                if (_Object_Add_Modify_None == 1)
                                //////                {
                                //////                    #region " Set new Credit line object "

                                //////                    oEOBInsPaymentCorrAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
                                //////                    oEOBInsPaymentCorrAsCreditDetail.EOBPaymentID = _EOBPaymentID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.EOBID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.EOBDtlID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.EOBPaymentDetailID = 0;

                                //////                    //...Will be assigning current check payment & payment details id's to Ref. Id.
                                //////                    //oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]);
                                //////                    //oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentID"]);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentDetailID"]);

                                //////                    oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentID"]);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentDetailID"]);

                                //////                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionDetailID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionLineNo = 0;

                                //////                    oEOBInsPaymentCorrAsCreditDetail.SubClaimNo = "";
                                //////                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionDetailID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionLineNo = 0;

                                //////                    oEOBInsPaymentCorrAsCreditDetail.DOSFrom = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_FROM).ToString()));
                                //////                    oEOBInsPaymentCorrAsCreditDetail.DOSTo = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_TO).ToString()));

                                //////                    oEOBInsPaymentCorrAsCreditDetail.CPTCode = "";
                                //////                    oEOBInsPaymentCorrAsCreditDetail.CPTDescription = "";
                                //////                    oEOBInsPaymentCorrAsCreditDetail.Amount = Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.IsNullAmount = false;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentType = EOBPaymentType.InsuranceCorrection;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentSubType = EOBPaymentSubType.Correction;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PayMode = (EOBPaymentMode)_crResPayMode;

                                //////                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                //////                    oEOBInsPaymentCorrAsCreditDetail.AccountID = SelectedInsuranceCompanyID; //Convert.ToInt64(lblInsCompany.Tag);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;

                                //////                    if (PatientInsuranceID != 0)// lblPayer.Tag != null && Convert.ToString(lblPayer.Tag).Trim() != "")
                                //////                    {
                                //////                        oEOBInsPaymentCorrAsCreditDetail.MSTAccountID = PatientInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                //////                        oEOBInsPaymentCorrAsCreditDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
                                //////                        oEOBInsPaymentCorrAsCreditDetail.ContactInsID = ContactInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));
                                //////                    }

                                //////                    oEOBInsPaymentCorrAsCreditDetail.PatientID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayID = SelectedPaymentTrayID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayCode = SelectedPaymentTrayCode;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayDescription = SelectedPaymentTray;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.UserID = AppSettings.UserID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.UserName = AppSettings.UserName;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.ClinicID = AppSettings.ClinicID;

                                //////                    oEOBInsPaymentCorrAsCreditDetail.FinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails.Count + 1;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.MainCreditLineID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.IsMainCreditLine = false;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.IsReserveCreditLine = false;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.IsCorrectionCreditLine = true;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.RefFinanceLieNo = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.UseRefFinanceLieNo = false;

                                //////                    if (mskCloseDate.MaskCompleted == true)
                                //////                    {
                                //////                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                //////                        oEOBInsPaymentCorrAsCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                //////                    }

                                //////                    oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentCorrAsCreditDetail);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.Dispose();
                                //////                    break;

                                //////                    #endregion " Set new Credit line object "
                                //////                }
                                //////                else if (_Object_Add_Modify_None == 2)
                                //////                {
                                //////                    #region " Set new Credit line object "

                                //////                    oEOBInsPaymentCorrAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
                                //////                    oEOBInsPaymentCorrAsCreditDetail.EOBPaymentID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].EOBPaymentID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.EOBID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].EOBID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.EOBDtlID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].EOBDtlID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.EOBPaymentDetailID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].EOBPaymentDetailID;


                                //////                    oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].RefEOBPaymentID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentDetailID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].RefEOBPaymentDetailID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].ReserveEOBPaymentID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentDetailID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].ReserveEOBPaymentDetailID;

                                //////                    oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].OldRefEOBPaymentID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentDetailID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].OldRefEOBPaymentDetailID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].OldReserveEOBPaymentID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentDetailID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].OldReserveEOBPaymentDetailID;

                                //////                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionDetailID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionLineNo = 0;

                                //////                    oEOBInsPaymentCorrAsCreditDetail.SubClaimNo = "";
                                //////                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionDetailID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionLineNo = 0;

                                //////                    oEOBInsPaymentCorrAsCreditDetail.DOSFrom = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_FROM).ToString()));
                                //////                    oEOBInsPaymentCorrAsCreditDetail.DOSTo = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_TO).ToString()));

                                //////                    oEOBInsPaymentCorrAsCreditDetail.CPTCode = "";
                                //////                    oEOBInsPaymentCorrAsCreditDetail.CPTDescription = "";
                                //////                    oEOBInsPaymentCorrAsCreditDetail.Amount = EOBInsurancePaymentMasterLines[_Object_Modify_Index].Amount;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.IsNullAmount = false;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentType = EOBPaymentType.InsuranceCorrection;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentSubType = EOBPaymentSubType.Correction;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PayMode = (EOBPaymentMode)_crResPayMode;

                                //////                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                //////                    oEOBInsPaymentCorrAsCreditDetail.AccountID = SelectedInsuranceCompanyID; //Convert.ToInt64(lblInsCompany.Tag);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;

                                //////                    if (PatientInsuranceID != 0)// lblPayer.Tag != null && Convert.ToString(lblPayer.Tag).Trim() != "")
                                //////                    {
                                //////                        oEOBInsPaymentCorrAsCreditDetail.MSTAccountID = PatientInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                //////                        oEOBInsPaymentCorrAsCreditDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
                                //////                        oEOBInsPaymentCorrAsCreditDetail.ContactInsID = ContactInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));
                                //////                    }

                                //////                    oEOBInsPaymentCorrAsCreditDetail.PatientID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayID = SelectedPaymentTrayID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayCode = SelectedPaymentTrayCode;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayDescription = SelectedPaymentTray;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.UserID = AppSettings.UserID;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.UserName = AppSettings.UserName;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.ClinicID = AppSettings.ClinicID;

                                //////                    oEOBInsPaymentCorrAsCreditDetail.FinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails.Count + 1;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.MainCreditLineID = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.IsMainCreditLine = false;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.IsReserveCreditLine = false;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.IsCorrectionCreditLine = true;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.RefFinanceLieNo = 0;
                                //////                    oEOBInsPaymentCorrAsCreditDetail.UseRefFinanceLieNo = false;

                                //////                    if (mskCloseDate.MaskCompleted == true)
                                //////                    {
                                //////                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                //////                        oEOBInsPaymentCorrAsCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                //////                    }

                                //////                    oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentCorrAsCreditDetail);
                                //////                    oEOBInsPaymentCorrAsCreditDetail.Dispose();
                                //////                    break;

                                //////                    #endregion " Set new Credit line object "
                                //////                }

                                //////                #endregion
                                //////            }
                                //////        }
                                //////    }
                                //////}
                                #endregion
                            }
                            #endregion


                            #region "Use Reserved Credit Line Entry"

                            //////////if (IsReserveUsed)
                            //////////{
                            //////////    //for (int i = 0; i <= EOBInsurancePaymentMasterLines.Count - 1; i++)
                            //////////    //{
                            //////////    //    EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine EOBInsurancePaymentMasterLine = EOBInsurancePaymentMasterLines[i];

                            //////////    //    //..Code changes done by Sagar Ghodke on 20100105(critical change Confirmation needed)
                            //////////    //    //...Below commented condition is previous one
                            //////////    //    //if (EOBInsurancePaymentMasterLines[i].PaymentType == EOBPaymentType.InsuracePayment)
                            //////////    //    if (EOBInsurancePaymentMasterLine.PaymentType == EOBPaymentType.InsuraceReserverd && EOBInsurancePaymentMasterLine.PaymentSubType == EOBPaymentSubType.Reserved)
                            //////////    //    {
                            //////////    //        EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsPaymentResAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
                            //////////    //        // Get financial line no
                            //////////    //        _financeLineNo = oPaymentInsurace.EOBInsurancePaymentLineDetails.Count + 1;
                            //////////    //        // Pass finalcial line & used reserve details to get the credit line object for used reserve
                            //////////    //        oEOBInsPaymentResAsCreditDetail = GetCreditLineForReserveUsed(EOBInsurancePaymentMasterLine, _financeLineNo);
                            //////////    //        // Add the credit line object to Main payment object
                            //////////    //        oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentResAsCreditDetail);
                            //////////    //        oEOBInsPaymentResAsCreditDetail.Dispose();
                            //////////    //    }
                            //////////    //}
                            //////////}

                            #endregion

                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                ActivityType.EOBLine, "EOBLine - Start", ActivityOutCome.Success);

                            if (oPaymentInsurace != null && oPaymentInsurace.EOBInsurancePaymentLineDetails != null)
                            {
                                for (int i = 0; i <= oPaymentInsurace.EOBInsurancePaymentLineDetails.Count - 1; i++)
                                {
                                    bool _AddLine = true;

                                    EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine oEOBInsPaymentAllDtl = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine();

                                    #region "If Credit line first time added and its second time then dont add just update the amount"

                                    //////////if (oPaymentInsurace.EOBInsurancePaymentLineDetails[i].IsMainCreditLine == true)
                                    //////////{
                                    //////////    for (int ccl = 0; ccl <= EOBInsurancePaymentMasterAllocationLines.Count - 1; ccl++)
                                    //////////    {
                                    //////////        if (EOBInsurancePaymentMasterAllocationLines[ccl].IsMainCreditLine == true)
                                    //////////        {
                                    //////////            decimal _OldCheckBalAmt = EOBInsurancePaymentMasterAllocationLines[ccl].Amount;
                                    //////////            decimal _OldCheckAmt = 0;
                                    //////////            for (int cml = 0; cml <= EOBInsurancePaymentMasterLines.Count - 1; cml++)
                                    //////////            {
                                    //////////                if (EOBInsurancePaymentMasterLines[cml].IsMainCreditLine == true)
                                    //////////                {
                                    //////////                    _OldCheckAmt = EOBInsurancePaymentMasterLines[cml].Amount;
                                    //////////                    break;
                                    //////////                }
                                    //////////            }
                                    //////////            decimal _NewCheckAmt = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].Amount;
                                    //////////            decimal _NewCheckBalAmt = 0;
                                    //////////            if (_NewCheckAmt < _OldCheckAmt)
                                    //////////            {
                                    //////////                _NewCheckBalAmt = _OldCheckBalAmt - (_OldCheckAmt - _NewCheckAmt);
                                    //////////            }
                                    //////////            else
                                    //////////            {
                                    //////////                _NewCheckBalAmt = _OldCheckBalAmt + (_NewCheckAmt - _OldCheckAmt);
                                    //////////            }

                                    //////////            EOBInsurancePaymentMasterAllocationLines[ccl].Amount = _NewCheckBalAmt;// oPaymentInsurace.EOBInsurancePaymentLineDetails[ccl].Amount;
                                    //////////            _AddLine = false;
                                    //////////            break;
                                    //////////        }
                                    //////////    }
                                    //////////}
                                    #endregion

                                    if (_AddLine == true)
                                    {
                                        oEOBInsPaymentAllDtl = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine();

                                        #region " Set Object "

                                        oEOBInsPaymentAllDtl.EOBPaymentID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].EOBPaymentID;
                                        oEOBInsPaymentAllDtl.EOBID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].EOBID;
                                        oEOBInsPaymentAllDtl.EOBDtlID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].EOBDtlID;
                                        oEOBInsPaymentAllDtl.EOBPaymentDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].EOBPaymentDetailID;

                                        oEOBInsPaymentAllDtl.BillingTransactionID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].BillingTransactionID;
                                        oEOBInsPaymentAllDtl.BillingTransactionDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].BillingTransactionDetailID;
                                        oEOBInsPaymentAllDtl.BillingTransactionLineNo = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].BillingTransactionLineNo;

                                        oEOBInsPaymentAllDtl.TrackBillingTransactionID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].TrackBillingTransactionID;
                                        oEOBInsPaymentAllDtl.TrackBillingTransactionDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].TrackBillingTransactionDetailID;
                                        oEOBInsPaymentAllDtl.TrackBillingTransactionLineNo = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].TrackBillingTransactionLineNo;

                                        oEOBInsPaymentAllDtl.PatientID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PatientID;
                                        oEOBInsPaymentAllDtl.DOSFrom = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].DOSFrom;
                                        oEOBInsPaymentAllDtl.DOSTo = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].DOSTo;
                                        oEOBInsPaymentAllDtl.CPTCode = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].CPTCode;
                                        oEOBInsPaymentAllDtl.CPTDescription = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].CPTDescription;
                                        oEOBInsPaymentAllDtl.Amount = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].Amount;
                                        oEOBInsPaymentAllDtl.IsNullAmount = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].IsNullAmount;
                                        oEOBInsPaymentAllDtl.PaymentType = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PaymentType;
                                        oEOBInsPaymentAllDtl.PaymentSubType = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PaymentSubType;
                                        oEOBInsPaymentAllDtl.PaySign = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PaySign;
                                        oEOBInsPaymentAllDtl.PayMode = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PayMode;

                                        oEOBInsPaymentAllDtl.RefEOBPaymentID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].RefEOBPaymentID;
                                        oEOBInsPaymentAllDtl.RefEOBPaymentDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].RefEOBPaymentDetailID;
                                        oEOBInsPaymentAllDtl.ReserveEOBPaymentID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ReserveEOBPaymentID;
                                        oEOBInsPaymentAllDtl.ReserveEOBPaymentDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ReserveEOBPaymentDetailID;

                                        oEOBInsPaymentAllDtl.OldRefEOBPaymentID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].RefEOBPaymentID;
                                        oEOBInsPaymentAllDtl.OldRefEOBPaymentDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].RefEOBPaymentDetailID;
                                        oEOBInsPaymentAllDtl.OldReserveEOBPaymentID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ReserveEOBPaymentID;
                                        oEOBInsPaymentAllDtl.OldReserveEOBPaymentDetailID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ReserveEOBPaymentDetailID;


                                        oEOBInsPaymentAllDtl.AccountID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].AccountID;
                                        oEOBInsPaymentAllDtl.AccountType = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].AccountType;
                                        oEOBInsPaymentAllDtl.MSTAccountID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].MSTAccountID;
                                        oEOBInsPaymentAllDtl.MSTAccountType = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].MSTAccountType;
                                        oEOBInsPaymentAllDtl.PaymentTrayID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PaymentTrayID;
                                        oEOBInsPaymentAllDtl.PaymentTrayCode = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PaymentTrayCode;
                                        oEOBInsPaymentAllDtl.PaymentTrayDescription = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].PaymentTrayDescription;
                                        oEOBInsPaymentAllDtl.UserID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].UserID;
                                        oEOBInsPaymentAllDtl.UserName = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].UserName;
                                        oEOBInsPaymentAllDtl.CreatedDateTime = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].CreatedDateTime;
                                        oEOBInsPaymentAllDtl.ModifiedDateTime = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ModifiedDateTime;
                                        oEOBInsPaymentAllDtl.ClinicID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ClinicID;

                                        oEOBInsPaymentAllDtl.FinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].FinanceLieNo;
                                        oEOBInsPaymentAllDtl.MainCreditLineID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].MainCreditLineID;
                                        oEOBInsPaymentAllDtl.IsMainCreditLine = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].IsMainCreditLine;
                                        oEOBInsPaymentAllDtl.IsReserveCreditLine = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].IsReserveCreditLine;
                                        oEOBInsPaymentAllDtl.IsCorrectionCreditLine = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].IsCorrectionCreditLine;
                                        oEOBInsPaymentAllDtl.RefFinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].RefFinanceLieNo;
                                        oEOBInsPaymentAllDtl.UseRefFinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].UseRefFinanceLieNo;
                                        oEOBInsPaymentAllDtl.ContactInsID = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].ContactInsID;


                                        #endregion " Set Object "

                                        EOBInsurancePaymentMasterAllocationLines.Add(oEOBInsPaymentAllDtl);
                                        oEOBInsPaymentAllDtl.Dispose();
                                    }
                                }
                            }

                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                ActivityType.EOBLine, "EOBLine - End", ActivityOutCome.Success);


                            //while assigning this object for collection amount object, there are -ve amount of correction
                            //we have to make it positive for debit line allocation
                            //so using for loop we will make it positive
                            for (int nAlctn = 0; nAlctn <= EOBInsurancePaymentMasterAllocationLines.Count - 1; nAlctn++)
                            {
                                if (EOBInsurancePaymentMasterAllocationLines[nAlctn].Amount < 0)
                                {
                                    EOBInsurancePaymentMasterAllocationLines[nAlctn].Amount = EOBInsurancePaymentMasterAllocationLines[nAlctn].Amount - (EOBInsurancePaymentMasterAllocationLines[nAlctn].Amount * 2);
                                }
                            }

                            //Allocation Amount - Finish


                            #region " ......................... Claim Payment Details Start ................................. "
                            //Modified Required
                            ////oPaymentInsuranceClaim = new EOBPayment.Common.PaymentInsuranceClaim();
                            
                            
                            //DataTable dtFile = dsERAFile.Tables[0];
                            //DataRow dRow = dtFile.Rows[1];
                            //DataRow dRowMST =  dtFile.Rows[0];

                            // Similar to get the Service Line Type as Claim in Manual posting.
                            if (this.BillingTransactionID > 0) //(c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                            {


                                // The following line are commented on 21_June_2010 (Dev66), 
                                // Once conditions are define then the commented code can be removed.
                                // Hidden Grid row used to store meta data.
                                //////if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                                //////{
                                //////    for (int clmIndex = 1; clmIndex < c1SinglePayment.Rows.Count; clmIndex++)
                                //////    {
                                //////        if (c1SinglePayment.GetData(clmIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(clmIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.Claim)
                                //////        {
                                //////            if (c1SinglePayment.GetData(clmIndex, COL_BILLING_TRANSACTON_ID) != null && Convert.ToString(c1SinglePayment.GetData(clmIndex, COL_BILLING_TRANSACTON_ID)).Trim() != "")
                                //////            {
                                
                                if (bProcessOnce == false)
                                {
                                    oPaymentInsuranceClaim.BillingTransactionID =  this.BillingTransactionID; //(Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(clmIndex, COL_BILLING_TRANSACTON_ID));
                                    oPaymentInsuranceClaim.TrackBillingTrnID = this.TrackTrnID; //this.BillingTransactionID;// this.BillingTransactionDetailID; ; // this.TrackTrnID;//this.BillingTransactionDetailID; // (Int64)dRow["nTrackTrnId"];  //Convert.ToInt64(c1SinglePayment.GetData(clmIndex, COL_TRACK_BILLING_TRANSACTON_ID));
                                    oPaymentInsuranceClaim.ClaimNo = this.ClaimNo; // (Int64)dRow["nClaimNo"]; //Convert.ToInt64(c1SinglePayment.GetData(clmIndex, COL_CLAIMNO));
                                    oPaymentInsuranceClaim.SubClaimNo = this.SubclaimNo; // dRow["nSubClaimNo"].ToString(); //Convert.ToString(c1SinglePayment.GetData(clmIndex, COL_SUBCLAIMNO));

                                    oSplitClaimDetails.TransactionMasterID = oPaymentInsuranceClaim.BillingTransactionID;
                                    oSplitClaimDetails.TransactionID = oPaymentInsuranceClaim.TrackBillingTrnID;
                                    oSplitClaimDetails.ClaimNo = oPaymentInsuranceClaim.ClaimNo;
                                    oSplitClaimDetails.SubClaimNo = oPaymentInsuranceClaim.SubClaimNo;
                                    oSplitClaimDetails.ClinicID = AppSettings.ClinicID;


                                    oSplitClaimDtls.TransactionMasterID = oPaymentInsuranceClaim.BillingTransactionID;
                                    oSplitClaimDtls.TransactionID = oPaymentInsuranceClaim.TrackBillingTrnID; // this.TrackTrnID;//oPaymentInsuranceClaim.TrackBillingTrnID;
                                    oSplitClaimDtls.ClaimNo = oPaymentInsuranceClaim.ClaimNo;
                                    oSplitClaimDtls.SubClaimNo = oPaymentInsuranceClaim.SubClaimNo;
                                    oSplitClaimDtls.ClinicID = AppSettings.ClinicID;

                                    
                                    oSplitClaimDtls2.TransactionMasterID = oPaymentInsuranceClaim.BillingTransactionID;
                                    oSplitClaimDtls2.TransactionID = oPaymentInsuranceClaim.TrackBillingTrnID; // this.TrackTrnID; //oPaymentInsuranceClaim.TrackBillingTrnID;
                                    oSplitClaimDtls2.ClaimNo = oPaymentInsuranceClaim.ClaimNo;
                                    oSplitClaimDtls2.SubClaimNo = oPaymentInsuranceClaim.SubClaimNo;
                                    oSplitClaimDtls2.ClinicID = AppSettings.ClinicID;
                                    
                                    bProcessOnce = true;

                                }

                                //////            }
                                //////        }
                                //////    }
                                //////}

                                #region "EOB Service Lines - New Logic - Direct allocation from credit line insted of allocation from correction, reserve and check"

                                EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsurancePaymentDetail = null;

                                // Condition to be implemented.
                                // 1. Identify line is of type Service line.
                                // 2. Verify value available in payment payment column.

                                // The following (for loop & if conditions are commented on 21_June_2010(Dev66).

                                //////for (int i = 0; i <= c1SinglePayment.Rows.Count - 1; i++)
                                //////{
                                //////    if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                                //////    {
                                //////        if (
                                //////                (c1SinglePayment.GetData(i, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PAYMENT)).Trim() != "")
                                //////            //|| (c1SinglePayment.GetData(i, COL_NEXT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT)).Trim() != "")
                                //////            )
                                //////        {


                                EOBPayment.Common.PaymentInsuranceLine oPaymentInsuranceLine = new EOBPayment.Common.PaymentInsuranceLine();
                                bool _Add_WO_WH = false;

                                #region "EOB Line"
                                //QCheck for PatInsuranceID,InsContactID
                                oPaymentInsuranceLine.PatientID = this.PatientID; // (Int64)dRow["nPatientID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_PATIENTID));
                                oPaymentInsuranceLine.PatInsuranceID = PatientInsuranceID;  //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                oPaymentInsuranceLine.InsContactID = nContactID;//ContactInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));

                                oPaymentInsuranceLine.BLTransactionID = this.BillingTransactionID; // (Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                oPaymentInsuranceLine.BLTransactionDetailID = this.BillingTransactionDetailID; // (Int64)dRow["nBillingTransactionDetailID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                oPaymentInsuranceLine.BLTransactionLineNo = this.BillingTransactionLineNo; // (Int64)dRow["nBillingTransactionLineNo"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));

                                oPaymentInsuranceLine.TrackBLTransactionID = this.TrackTrnID; // (Int64)dRow["nTrackTrnId"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                oPaymentInsuranceLine.TrackBLTransactionDetailID = this.TrackTrnDtlID; // (Int64)dRow["nTrackTrnDtlId"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                oPaymentInsuranceLine.TrackBLTransactionLineNo = this.BillingTransactionLineNo; // (Int64)dRow["nTrackBLTransactionLineNo"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_LINENO));


                                oPaymentInsuranceLine.ClaimNumber = this.ClaimNo; // (Int64)dRow["nClaimNo"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                                oPaymentInsuranceLine.SubClaimNumber = this.SubclaimNo; // dRow["nSubClaimNo"].ToString(); // Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));

                                oPaymentInsuranceLine.DOSFrom = gloDateMaster.gloDate.DateAsNumber(this.DOSFrom); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dRow["dDOSFrom"]));
                                oPaymentInsuranceLine.DOSTo = gloDateMaster.gloDate.DateAsNumber(this.DOSTo); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dRow["dDOSTo"]));
                                oPaymentInsuranceLine.CPTCode = this.CPT; // Convert.ToString(dRow["sCPTCode"]);
                                oPaymentInsuranceLine.CPTDescription = this.CPTDescription; // Convert.ToString(dRow["sCPTDescription"]);

                                oPaymentInsuranceLine.BLInsuranceID = 0;
                                oPaymentInsuranceLine.BLInsuranceName = "";
                                oPaymentInsuranceLine.BLInsuranceFlag = InsuranceTypeFlag.None;

                                if (this.Charges.Trim() != "")
                                { oPaymentInsuranceLine.Charges = Convert.ToDecimal(this.Charges); oPaymentInsuranceLine.IsNullCharges = false; }

                                if (this.Unit.Trim() != "")
                                { oPaymentInsuranceLine.Unit = Convert.ToDecimal(this.Unit); oPaymentInsuranceLine.IsNullUnit = false; }

                                if (this.TotalCharges.Trim() != "")
                                { oPaymentInsuranceLine.TotalCharges = Convert.ToDecimal(this.TotalCharges); oPaymentInsuranceLine.IsNullTotalCharges = false; }

                                if (this.Allowed.Trim() != "")
                                { oPaymentInsuranceLine.Allowed = Convert.ToDecimal(this.Allowed); oPaymentInsuranceLine.IsNullAllowed = false; }

                                if (this.WriteOff.Trim() != "")
                                { oPaymentInsuranceLine.WriteOff = Convert.ToDecimal(this.WriteOff); oPaymentInsuranceLine.IsNullWriteOff = false; }

                                oPaymentInsuranceLine.NonCovered = 0;

                                if (this.Payment.Trim() != "")
                                { oPaymentInsuranceLine.InsuranceAmount = Convert.ToDecimal(this.Payment); oPaymentInsuranceLine.IsNullInsurance = false; }

                                if (this.Copay.Trim() != "")
                                { oPaymentInsuranceLine.Copay = Convert.ToDecimal(this.Copay); oPaymentInsuranceLine.IsNullCopay = false; }

                                if (this.Deductible.Trim() != "")
                                { oPaymentInsuranceLine.Deductible = Convert.ToDecimal(this.Deductible); oPaymentInsuranceLine.IsNullDeductible = false; }

                                if (this.CoInsurance.Trim() != "")
                                { oPaymentInsuranceLine.CoInsurance = Convert.ToDecimal(this.CoInsurance); oPaymentInsuranceLine.IsNullCoInsurance = false; }

                                if (this.WithHold.Trim() != "")
                                { oPaymentInsuranceLine.Withhold = Convert.ToDecimal(this.WithHold); oPaymentInsuranceLine.IsNullWithhold = false; }


                                // Below are the original line commented on 21_June_2010(Dev66). Once code is stable delete the below commented code

                                oPaymentInsuranceLine.PaymentTrayID = SelectedPaymentTrayID;
                                oPaymentInsuranceLine.PaymentTrayCode = SelectedPaymentTrayCode;
                                oPaymentInsuranceLine.PaymentTrayDesc = SelectedPaymentTray;


                                oPaymentInsuranceLine.UserID = AppSettings.UserID;
                                oPaymentInsuranceLine.UserName = AppSettings.UserName;
                                oPaymentInsuranceLine.ClinicID = AppSettings.ClinicID;

                                //if (mskCloseDate.MaskCompleted == true)
                                //{
                                //mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                oPaymentInsuranceLine.CloseDate = this.CloseDate; //gloDateMaster.gloDate.DateAsNumber(oPaymentInsurace.CloseDate.ToString()); //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                //}

                                #region " Set Line Reason Codes "


                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                        ActivityType.SetLineReasonCodes, "SetLineReasonCodes - Start", ActivityOutCome.Success);


                                //...Code added on 20100318 by Sagar Ghodke
                                //...Code added to auto set the adjustment codes for W/O,Copay,Dedutible,Coinsurance,Withhold
                                //...by reading there respective values from admin settings

                                EOBPayment.Common.PaymentInsuranceLineResonCode oPaymentInsuranceLineResonCode = null;
                                string _code = "";

                                // This Hastable object is assigned with database record column name.
                                // Which is used in retrieving column value from a datarow.
                                Hashtable htTab = new Hashtable();
                                htTab.Add(23, "nWriteOff");
                                htTab.Add(24, "nCopay");
                                htTab.Add(25, "nDeductible");
                                htTab.Add(26, "nCoInsurance");
                                htTab.Add(27, "nWithhold");


                                for (int colIndex = COL_WRITEOFF; colIndex <= COL_WITHHOLD; colIndex++)
                                {
                                    _code = "";
                                    //if (c1SinglePayment.GetData(i, colIndex) != null && Convert.ToString(c1SinglePayment.GetData(i, colIndex)).Trim() != ""
                                    //    && Convert.ToDecimal(c1SinglePayment.GetData(i, colIndex)) != 0)
                                    if (dRow[htTab[(object)colIndex].ToString()].ToString().Trim() != "" && (decimal)dRow[htTab[(object)colIndex].ToString()]!=0)
                                    {
                                        oPaymentInsuranceLineResonCode = new EOBPayment.Common.PaymentInsuranceLineResonCode();
                                        oPaymentInsuranceLineResonCode.ID = 0;
                                        oPaymentInsuranceLineResonCode.ClaimNo = oPaymentInsuranceLine.ClaimNumber;
                                        oPaymentInsuranceLineResonCode.BillingTransactionID = oPaymentInsuranceLine.BLTransactionID;
                                        oPaymentInsuranceLineResonCode.BillingTransactionDetailID = oPaymentInsuranceLine.BLTransactionDetailID;
                                        oPaymentInsuranceLineResonCode.SubClaimNo = oPaymentInsuranceLine.SubClaimNumber;
                                        oPaymentInsuranceLineResonCode.TrackBillingTransactionID = oPaymentInsuranceLine.TrackBLTransactionID;
                                        oPaymentInsuranceLineResonCode.TrackBillingTransactionDetailID = oPaymentInsuranceLine.TrackBLTransactionDetailID;
                                        oPaymentInsuranceLineResonCode.CloseDate = oPaymentInsuranceLine.CloseDate;
                                        oPaymentInsuranceLineResonCode.ClinicID = oPaymentInsuranceLine.ClinicID;
                                        oPaymentInsuranceLineResonCode.EOBPaymentID = 0;
                                        oPaymentInsuranceLineResonCode.EOBID = 0;
                                        oPaymentInsuranceLineResonCode.EOBPaymentDetailID = 0;
                                        oPaymentInsuranceLineResonCode.HasData = true;
                                        oPaymentInsuranceLineResonCode.IsNullReasonAmount = false;

                                        oPaymentInsuranceLineResonCode.ReasonCode = GetSelectedReasonCode(colIndex);

                                        oPaymentInsuranceLineResonCode.ReasonDescription = InsurancePayment.GetReasonDescription(_code);
                                        // Calculate Reason Amount method is required.
                                        oPaymentInsuranceLineResonCode.ReasonAmount = (decimal)dRow[htTab[(object)colIndex].ToString()];//CalculateReasonAmount(oPaymentInsuranceLineResonCode.ReasonCode);
                                        oPaymentInsuranceLineResonCode.CommentType = EOBCommentType.SystemReasonCode;
                                        oPaymentInsuranceLineResonCode.ReasonCodeSubType = colIndex;
                                        oPaymentInsuranceLine.LineResonCodes.Add(oPaymentInsuranceLineResonCode);
                                        oPaymentInsuranceLineResonCode = null;
                                    }
                                }

                                htTab = null;


                                //...End code add 20100318


                                // Need Clarification on the below code for line reason code - Dev66.
                                // Get Other reason codes.
                                // ERA_GetOtherReasonCodes
                                // Type = 1
                                DataTable dtOtherReason = null;
                                //DataTable dtOtherReason = GetOtherReasonCodes(this.SVCId);

                                dtOtherReason = dsTempPost.Tables[5];

                                object[] oParamLineOtherReasons = { EOBCommentType.Reason.GetHashCode(), this.ClaimNo, this.SubclaimNo, 0, this.TrackTrnDtlID, this.BillingTransactionDetailID };

                                sQueryFilter = String.Format("nType = {0} and nClaimNo =  {1} and sSubClaimNo = '{2}' and nSubType = {3} and nTrackTrnDtlID = {4} and nBillingTransactionDetailID = {5} ", oParamLineOtherReasons);

                                DataRow[] drOtherReasonLines = dtOtherReason.Select(sQueryFilter);

                                foreach (DataRow dtRow in drOtherReasonLines)
                                {
                                    oPaymentInsuranceLineResonCode = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineResonCode();

                                    oPaymentInsuranceLineResonCode.ID = 0;

                                    oPaymentInsuranceLineResonCode.ClaimNo = this.ClaimNo;

                                    oPaymentInsuranceLineResonCode.SubClaimNo = this.SubclaimNo;

                                    oPaymentInsuranceLineResonCode.EOBPaymentID = 0;

                                    oPaymentInsuranceLineResonCode.EOBID = 0;

                                    oPaymentInsuranceLineResonCode.EOBPaymentDetailID = 0;

                                    oPaymentInsuranceLineResonCode.BillingTransactionID = this.BillingTransactionID;

                                    oPaymentInsuranceLineResonCode.BillingTransactionDetailID = this.BillingTransactionDetailID;

                                    oPaymentInsuranceLineResonCode.TrackBillingTransactionID = this.TrackTrnID;

                                    oPaymentInsuranceLineResonCode.TrackBillingTransactionDetailID = this.TrackTrnDtlID;

                                    oPaymentInsuranceLineResonCode.ReasonCode = dtRow["sReasonCode"].ToString();

                                    //oPaymentInsuranceLineResonCode.ReasonDescription = "";
                                    oPaymentInsuranceLineResonCode.ReasonDescription = dtRow["sReasonDescription"].ToString();

                                    if (dtRow["dReasonAmount"] != null)
                                    {
                                        if (dtRow["dReasonAmount"].ToString().Trim().Length > 0 && Convert.ToDecimal(dtRow["dReasonAmount"].ToString()) != 0)
                                            oPaymentInsuranceLineResonCode.ReasonAmount = Convert.ToDecimal(dtRow["dReasonAmount"].ToString());
                                        oPaymentInsuranceLineResonCode.IsNullReasonAmount = false;
                                    }

                                    oPaymentInsuranceLineResonCode.ClinicID = AppSettings.ClinicID;

                                    oPaymentInsuranceLineResonCode.CloseDate = oPaymentInsurace.CloseDate;
                                    oPaymentInsuranceLineResonCode.HasData = true;
                                    oPaymentInsuranceLineResonCode.CommentType = EOBCommentType.Reason;

                                    oPaymentInsuranceLine.LineResonCodes.Add(oPaymentInsuranceLineResonCode);
                                }


                                #region " Code commented by Dev66 - Need to revisit the code.
                                ////EOBPayment.Common.PaymentInsuranceLineResonCode oPaymentInsuranceLineResonCode = null;
                                ////string _code = "";

                                ////DataRow[] drLineReasonRows = dtClaimReasonCodes.Select("nType = " + EOBCommentType.SystemReasonCode.GetHashCode() + " and nClaimNo = " + this.ClaimNo + " and nBillingTransactionDetailID=" + dRow["nEOBDtlID"].ToString());
                                ////foreach (DataRow drLineReasonRow in drLineReasonRows)
                                ////{
                                ////    _code = "";
                                ////    if (drLineReasonRow != null)
                                ////    {
                                ////        oPaymentInsuranceLineResonCode = new EOBPayment.Common.PaymentInsuranceLineResonCode();
                                ////        oPaymentInsuranceLineResonCode.ID = 0;
                                ////        oPaymentInsuranceLineResonCode.ClaimNo = oPaymentInsuranceLine.ClaimNumber;
                                ////        oPaymentInsuranceLineResonCode.BillingTransactionID = oPaymentInsuranceLine.BLTransactionID;
                                ////        oPaymentInsuranceLineResonCode.BillingTransactionDetailID = oPaymentInsuranceLine.BLTransactionDetailID;
                                ////        oPaymentInsuranceLineResonCode.SubClaimNo = oPaymentInsuranceLine.SubClaimNumber;
                                ////        oPaymentInsuranceLineResonCode.TrackBillingTransactionID = oPaymentInsuranceLine.TrackBLTransactionID;
                                ////        oPaymentInsuranceLineResonCode.TrackBillingTransactionDetailID = oPaymentInsuranceLine.TrackBLTransactionDetailID;
                                ////        oPaymentInsuranceLineResonCode.CloseDate = oPaymentInsuranceLine.CloseDate;
                                ////        oPaymentInsuranceLineResonCode.ClinicID = oPaymentInsuranceLine.ClinicID;
                                ////        oPaymentInsuranceLineResonCode.EOBPaymentID = 0;
                                ////        oPaymentInsuranceLineResonCode.EOBID = 0;
                                ////        oPaymentInsuranceLineResonCode.EOBPaymentDetailID = 0;
                                ////        oPaymentInsuranceLineResonCode.HasData = true;
                                ////        oPaymentInsuranceLineResonCode.IsNullReasonAmount = false;

                                ////        oPaymentInsuranceLineResonCode.ReasonCode = drLineReasonRow["sReasonCode"].ToString(); ////GetSelectedReasonCode(colIndex);

                                ////        oPaymentInsuranceLineResonCode.ReasonDescription = drLineReasonRow["sReasonDescription"].ToString(); //InsurancePayment.GetReasonDescription(_code);
                                ////        // Calculate Reason Amount method is required.
                                ////        oPaymentInsuranceLineResonCode.ReasonAmount = Convert.ToDecimal(drLineReasonRow["dReasonAmount"].ToString());//CalculateReasonAmount(oPaymentInsuranceLineResonCode.ReasonCode);
                                ////        oPaymentInsuranceLineResonCode.CommentType = EOBCommentType.SystemReasonCode;
                                ////        oPaymentInsuranceLineResonCode.ReasonCodeSubType = Convert.ToInt32(drLineReasonRow["nSubType"]);

                                ////        oPaymentInsuranceLine.LineResonCodes.Add(oPaymentInsuranceLineResonCode);
                                ////        oPaymentInsuranceLineResonCode = null;
                                ////    }
                                ////}


                                //////...End code add 20100318


                                ////// Get Other Reason codes stored in Temporary table.
                                ////drLineReasonRows = dtClaimReasonCodes.Select("nType = " + EOBCommentType.Reason.GetHashCode() + " and nClaimNo = " + this.ClaimNo + " and nBillingTransactionDetailID=" + dRow["nEOBDtlID"].ToString());


                                ////if (drLineReasonRows != null)
                                ////{

                                ////    foreach (DataRow drLineReasonRow in drLineReasonRows)
                                ////    {
                                ////        oPaymentInsuranceLineResonCode = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineResonCode();

                                ////        oPaymentInsuranceLineResonCode.ID = Convert.ToInt64("0");

                                ////        oPaymentInsuranceLineResonCode.ClaimNo = this.ClaimNo;
                                ////        oPaymentInsuranceLineResonCode.SubClaimNo = this.SubclaimNo; 

                                ////        oPaymentInsuranceLineResonCode.EOBPaymentID = 0; 

                                ////        oPaymentInsuranceLineResonCode.EOBID = 0; 

                                ////        oPaymentInsuranceLineResonCode.EOBPaymentDetailID = 0; 

                                ////        oPaymentInsuranceLineResonCode.BillingTransactionID = this.BillingTransactionID; 

                                ////        oPaymentInsuranceLineResonCode.BillingTransactionDetailID = this.BillingTransactionDetailID; 

                                ////        oPaymentInsuranceLineResonCode.TrackBillingTransactionID = this.TrackTrnID; 

                                ////        oPaymentInsuranceLineResonCode.TrackBillingTransactionDetailID = this.TrackTrnDtlID;

                                ////        oPaymentInsuranceLineResonCode.ReasonCode = drLineReasonRow["sReasonCode"].ToString();

                                ////        oPaymentInsuranceLineResonCode.ReasonDescription = drLineReasonRow["sReasonDescription"].ToString();

                                ////        oPaymentInsuranceLineResonCode.ReasonAmount = Convert.ToDecimal(drLineReasonRow["dReasonAmount"].ToString());  oPaymentInsuranceLineResonCode.IsNullReasonAmount = false; 

                                ////        oPaymentInsuranceLineResonCode.ClinicID = AppSettings.ClinicID; 

                                ////        oPaymentInsuranceLineResonCode.CloseDate = oPaymentInsurace.CloseDate;
                                ////        oPaymentInsuranceLineResonCode.HasData = true;
                                ////        oPaymentInsuranceLineResonCode.CommentType = EOBCommentType.Reason;

                                ////        oPaymentInsuranceLine.LineResonCodes.Add(oPaymentInsuranceLineResonCode);
                                ////    }
                                ////}
                                #endregion

                                #endregion " Set Line Reason Codes "

                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                        ActivityType.SetLineReasonCodes, "SetLineReasonCodes - End", ActivityOutCome.Success);

                                DataTable dtChargeLineNotes = null;

                                dtChargeLineNotes = dsTempPost.Tables[4];

                                object[] oParamLineStatementNotes = { EOBPaymentType.InsuracePayment.GetHashCode(), this.ClaimNo, 
                                                                        this.SubclaimNo, EOBPaymentSubType.StatementNote.GetHashCode(), 
                                                                        this.TrackTrnDtlID, this.BillingTransactionDetailID };

                                sQueryFilter = String.Format("nPaymentNoteType = {0} and nClaimNo =  {1} and sSubClaimNo = '{2}' and nPaymentNoteSubType = {3} and nTrackTrnDtlID = {4} and nBillingTransactionDetailID = {5} ", oParamLineStatementNotes);

                                DataRow[] dRowLines = dtChargeLineNotes.Select(sQueryFilter);

                                #region " Statement Notes & Internal Notes for Line "

                                //if (c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTE)).Trim() != "")
                                if (dRowLines != null) // Method required to verify and get line statement notes.
                                {
                                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                        ActivityType.StatementNotes, "StatementNotes - Start", ActivityOutCome.Success);

                                    foreach (DataRow dRowLine in dRowLines)
                                    {

                                        EOBPayment.Common.PaymentInsuranceLineNote oPaymentInsuranceLineNote = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineNote();

                                        oPaymentInsuranceLineNote.ClaimNo = this.ClaimNo; // (Int64)dRow["nClaimNo"]; // Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                                        oPaymentInsuranceLineNote.SubClaimNo = this.SubclaimNo; // Convert.ToString(dRow["nSubClaimNo"]);
                                        oPaymentInsuranceLineNote.EOBPaymentID = this.EOBPaymentID; //oPaymentInsurace.EOBPaymentID; //_EOBPaymentID; //CheckDev66
                                        oPaymentInsuranceLineNote.EOBID = 0;
                                        oPaymentInsuranceLineNote.EOBPaymentDetailID = 0;

                                        oPaymentInsuranceLineNote.BillingTransactionID = this.BillingTransactionID; // (Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                        oPaymentInsuranceLineNote.BillingTransactionDetailID = this.BillingTransactionDetailID; // (Int64)dRow["nBillingTransactionDetailID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));

                                        oPaymentInsuranceLineNote.TrackBillingTransactionID = this.TrackTrnID; // (Int64)dRow["nTrackBLTransactionID"];  //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                        oPaymentInsuranceLineNote.TrackBillingTransactionDetailID = this.TrackTrnDtlID; // (Int64)dRow["nTrackBLTransactionDetailID"];  //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));

                                        oPaymentInsuranceLineNote.Code = dRowLine["nNoteCode"].ToString();
                                        // Dev66 - Required clarification.
                                        oPaymentInsuranceLineNote.Description = dRowLine["nNoteDescription"].ToString();
                                        oPaymentInsuranceLineNote.Amount = Convert.ToInt64(dRowLine["dNoteAmount"].ToString());
                                        //////if (c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTEONPRINT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTEONPRINT)).Trim() != "")
                                        //////{ oPaymentInsuranceLineNote.IncludeOnPrint = Convert.ToBoolean(c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTEONPRINT)); }

                                        oPaymentInsuranceLineNote.IncludeOnPrint = false;

                                        oPaymentInsuranceLineNote.ClinicID = AppSettings.ClinicID;
                                        oPaymentInsuranceLineNote.PaymentNoteType = EOBPaymentType.InsuracePayment;
                                        oPaymentInsuranceLineNote.PaymentNoteSubType = EOBPaymentSubType.StatementNote;
                                        oPaymentInsuranceLineNote.HasData = true;
                                        oPaymentInsuranceLineNote.CloseDate = this.CloseDate;//oPaymentInsurace.CloseDate;
                                        oPaymentInsuranceLineNote.UserId = AppSettings.UserID;

                                        oPaymentInsuranceLine.LineNotes.Add(oPaymentInsuranceLineNote);
                                        //oPaymentInsurace.Notes.Add(oPaymentInsuranceLineNote);
                                        oPaymentInsuranceLineNote.Dispose();
                                    }

                                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                        ActivityType.StatementNotes, "StatementNotes - End", ActivityOutCome.Success);
                                }

                                oParamLineStatementNotes = null;

                                object[] oParamLineInternalNotes = { EOBPaymentType.InsuracePayment.GetHashCode(), this.ClaimNo, 
                                                                       this.SubclaimNo, EOBPaymentSubType.InternalNote.GetHashCode(), 
                                                                       this.TrackTrnDtlID, this.BillingTransactionDetailID };

                                sQueryFilter = String.Format("nPaymentNoteType = {0} and nClaimNo =  {1} and sSubClaimNo = '{2}' and nPaymentNoteSubType = {3} and nTrackTrnDtlID = {4} and nBillingTransactionDetailID = {5} ", oParamLineInternalNotes);

                                dRowLines = dtChargeLineNotes.Select(sQueryFilter);

                                ////if (c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTE)).Trim() != "")
                                if (dRowLines != null) // Method required to verify and get line Internal notes.
                                {
                                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                        ActivityType.InternalNotes, "InternalNotes - Start", ActivityOutCome.Success);

                                    foreach (DataRow dRowLine in dRowLines)
                                    {
                                        EOBPayment.Common.PaymentInsuranceLineNote oPaymentInsuranceLineNote = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineNote();

                                        oPaymentInsuranceLineNote.ClaimNo = this.ClaimNo;// (Int64)dRow["nClaimNo"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                                        oPaymentInsuranceLineNote.SubClaimNo = this.SubclaimNo;// Convert.ToString(dRow["nSubClaimNo"]); //Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));

                                        oPaymentInsuranceLineNote.EOBPaymentID = this.EOBPaymentID; // oPaymentInsurace.EOBPaymentID; //_EOBPaymentID; //CheckDev66
                                        oPaymentInsuranceLineNote.EOBID = 0;
                                        oPaymentInsuranceLineNote.EOBPaymentDetailID = 0;

                                        oPaymentInsuranceLineNote.BillingTransactionID = this.BillingTransactionID;// (Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                        oPaymentInsuranceLineNote.BillingTransactionDetailID = this.BillingTransactionDetailID; // (Int64)dRow["nBillingTransactionDetailID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));

                                        oPaymentInsuranceLineNote.TrackBillingTransactionID = this.TrackTrnID; // (Int64)dRow["nTrackBLTransactionID"];  //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                        oPaymentInsuranceLineNote.TrackBillingTransactionDetailID = this.TrackTrnID;// (Int64)dRow["nTrackBLTransactionDetailID"];  //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));

                                        oPaymentInsuranceLineNote.Code = dRowLine["nNoteCode"].ToString();
                                        // Dev66 - Required clarification.
                                        oPaymentInsuranceLineNote.Description = dRowLine["nNoteDescription"].ToString();// Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTE)).Trim();
                                        oPaymentInsuranceLineNote.Amount = Convert.ToInt64(dRowLine["dNoteAmount"].ToString()); ;
                                        //if (c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTEONPRINT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTEONPRINT)).Trim() != "")
                                        //{ oPaymentInsuranceLineNote.IncludeOnPrint = Convert.ToBoolean(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTEONPRINT)); }

                                        oPaymentInsuranceLineNote.IncludeOnPrint = false; // Convert.ToBoolean(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTEONPRINT)); 

                                        oPaymentInsuranceLineNote.ClinicID = AppSettings.ClinicID;
                                        oPaymentInsuranceLineNote.PaymentNoteType = EOBPaymentType.InsuracePayment;
                                        oPaymentInsuranceLineNote.PaymentNoteSubType = EOBPaymentSubType.InternalNote;
                                        oPaymentInsuranceLineNote.HasData = true;
                                        oPaymentInsuranceLineNote.CloseDate = this.CloseDate;// oPaymentInsurace.CloseDate;
                                        oPaymentInsuranceLineNote.UserId = AppSettings.UserID;

                                        //oPaymentInsurace.Notes.Add(oPaymentInsuranceLineNote);
                                        oPaymentInsuranceLine.LineNotes.Add(oPaymentInsuranceLineNote);
                                        oPaymentInsuranceLineNote.Dispose();
                                    }

                                    gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                        ActivityType.InternalNotes, "InternalNotes - End", ActivityOutCome.Success);
                                }

                                oParamLineInternalNotes = null;
                                dRowLines = null;
                                dtChargeLineNotes = null;

                                #endregion " Statement Notes & Internal Notes for Line "

                                oPaymentInsuranceLine.InsCompanyID = SelectedInsuranceCompanyID; //SelectedInsuranceCompanyID;//Convert.ToInt64(lblInsCompany.Tag);

                                #endregion

                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                        ActivityType.DebitServiceLine, "DebitServiceLine - Start", ActivityOutCome.Success);

                                #region "Debit Service Line - Insurance - Allocation from Current Check, Correction/Takeback, Use Reserved"

                                string Amt_Payment = this.Payment;
                                string Amt_Last_Payment = GetLastPayment();

                                // Verify Payment amount is available or not
                                // Verify Last Payment amount is available or not
                                // Do subtraction(Check for the payment amount is not zero or less than zero).

                                //if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PAYMENT)).Trim() != "")
                                if (Amt_Payment.Trim() != "") // check for payment is available
                                {
                                    //if (c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_PAYMENT)).Trim() != "")
                                    if (Amt_Last_Payment.Trim() != "") // check for last payment is available
                                    {

                                        //..Code changes done by sagar ghodke .. on 20100322 to resolve save of zero payment debit line
                                        //below commented condition is previous
                                        //if ((Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT))) > 0)
                                        //if ((Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT))) >= 0)

                                        // Verify the difference is Greater than zero.
                                        // Calculate difference between (payment) - (last payment)
                                        if ((Convert.ToDecimal(Amt_Payment) - Convert.ToDecimal(Amt_Last_Payment)) >= 0)
                                        {
                                            decimal _fillPayAmt = 0; decimal _fillResAmt = 0;
                                            Int64 _fillResPayID = 0; Int64 _fillResPayDtlID = 0;
                                            Int64 _fillRefPayID = 0; Int64 _fillRefPayDtlID = 0;
                                            //int _fillrPayIndx = -1;
                                            int _fillRefFinanceLieNo = 0;
                                            bool _fillUseRefFinanceLieNo = false;
                                            bool _isNullfillPayAmt = false;


                                            //if no correction then direct current new amount
                                            //if negative correction then it will not come in this loop
                                            //if positive correction then only correction amount, but in grid user will enter total amount not correction amount
                                            //thats why below we have to calculate amount = last amount - current payment


                                            //Code commented by Dev66
                                            /*
                                            if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null)
                                            { _fillPayAmt = (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT))); }
                                            else if (c1SinglePayment.GetData(i, COL_PAYMENT) != null)
                                            { _fillPayAmt = (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT))); }
                                            else if (c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null)
                                            { _fillPayAmt = 0 - (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT))); }
                                            else
                                            { _isNullfillPayAmt = true; }
                                            */


                                            if (Convert.ToDecimal(Amt_Payment) > 0 && Convert.ToDecimal(Amt_Last_Payment) > 0)
                                            { _fillPayAmt = Convert.ToDecimal(Amt_Payment) - Convert.ToDecimal(Amt_Last_Payment); }
                                            else if (Convert.ToDecimal(Amt_Payment) > 0)
                                            { _fillPayAmt = (Convert.ToDecimal(Amt_Payment)); }
                                            else if (Convert.ToDecimal(Amt_Last_Payment) > 0)
                                            { _fillPayAmt = 0 - (Convert.ToDecimal(Amt_Last_Payment)); }
                                            else
                                            { _isNullfillPayAmt = true; }





                                            int rPay = 0; //we have to always allocate against check, so rPay value set 0 as its first line in collection
                                            _fillResAmt = EOBInsurancePaymentMasterAllocationLines[rPay].Amount;

                                            //..Code changes done by Sagar Ghodke on 20100511
                                            //..Code changes done to make correct debit entries here unnecessary resid 
                                            //..where passed even if the amount is not used from the reserve
                                            //..below commented code lines are existing logic

                                            //_fillResPayID = EOBInsurancePaymentMasterAllocationLines[rPay].EOBPaymentID;
                                            //_fillResPayDtlID = EOBInsurancePaymentMasterAllocationLines[rPay].EOBPaymentDetailID;

                                            _fillResPayID = EOBInsurancePaymentMasterAllocationLines[rPay].ReserveEOBPaymentID;
                                            _fillResPayDtlID = EOBInsurancePaymentMasterAllocationLines[rPay].ReserveEOBPaymentDetailID;

                                            //..End code changes done by Sagar Ghodke on 20100511

                                            _fillRefPayID = EOBInsurancePaymentMasterAllocationLines[rPay].RefEOBPaymentID;
                                            _fillRefPayDtlID = EOBInsurancePaymentMasterAllocationLines[rPay].RefEOBPaymentDetailID;

                                            _fillRefFinanceLieNo = EOBInsurancePaymentMasterAllocationLines[rPay].FinanceLieNo;
                                            if (_fillRefPayID == 0 && _fillRefPayDtlID == 0)
                                            {
                                                _fillUseRefFinanceLieNo = true;
                                            }

                                            #region "Set object"

                                            oEOBInsurancePaymentDetail = new EOBPayment.Common.EOBInsurancePaymentDetail();
                                            oEOBInsurancePaymentDetail.EOBPaymentID = 0;
                                            oEOBInsurancePaymentDetail.EOBID = 0;
                                            oEOBInsurancePaymentDetail.EOBDtlID = 0;
                                            oEOBInsurancePaymentDetail.EOBPaymentDetailID = 0;

                                            oEOBInsurancePaymentDetail.BillingTransactionID = this.BillingTransactionID;// (Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                            oEOBInsurancePaymentDetail.BillingTransactionDetailID = this.BillingTransactionDetailID; // (Int64)dRow["nBillingTransactionDetailID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                            oEOBInsurancePaymentDetail.BillingTransactionLineNo = (Int32)this.BillingTransactionLineNo; // (Int32)dRow["nBillingTransactionLineNo"]; //Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));

                                            oEOBInsurancePaymentDetail.TrackBillingTransactionID = this.TrackTrnID;// (Int64)dRow["nTrackBLTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                            oEOBInsurancePaymentDetail.TrackBillingTransactionDetailID = this.TrackTrnDtlID;// (Int64)dRow["nTrackBLTransactionDetailID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                            oEOBInsurancePaymentDetail.TrackBillingTransactionLineNo = (Int32)this.BillingTransactionLineNo; // (Int32)dRow["nTrackBLTransactionLineNo"]; //Convert.ToInt32(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_LINENO));
                                            oEOBInsurancePaymentDetail.SubClaimNo = this.SubclaimNo;// (string)dRow["nSubClaimNo"]; //Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));

                                            oEOBInsurancePaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(this.DOSFrom); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dRow["dDOSFrom"])); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                            oEOBInsurancePaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(this.DOSTo); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dRow["dDOSTo"]));//gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
                                            oEOBInsurancePaymentDetail.CPTCode = this.CPT; // Convert.ToString(dRow["sCPTCode"]);
                                            oEOBInsurancePaymentDetail.CPTDescription = this.CPTDescription; // Convert.ToString(dRow["sCPTDescription"]);

                                            oEOBInsurancePaymentDetail.Amount = _fillPayAmt;
                                            oEOBInsurancePaymentDetail.IsNullAmount = _isNullfillPayAmt;

                                            oEOBInsurancePaymentDetail.PaymentType = EOBPaymentType.InsuracePayment;
                                            oEOBInsurancePaymentDetail.PaymentSubType = EOBPaymentSubType.Insurace;
                                            oEOBInsurancePaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                            oEOBInsurancePaymentDetail.PayMode = this.PaymentMode; //solving sales force case -GLO2011-0010771


                                       

                                            oEOBInsurancePaymentDetail.RefEOBPaymentID = _fillRefPayID;
                                            oEOBInsurancePaymentDetail.RefEOBPaymentDetailID = _fillRefPayDtlID;
                                            oEOBInsurancePaymentDetail.ReserveEOBPaymentID = _fillResPayID;
                                            oEOBInsurancePaymentDetail.ReserveEOBPaymentDetailID = _fillResPayDtlID;

                                            oEOBInsurancePaymentDetail.OldRefEOBPaymentID = _fillRefPayID;
                                            oEOBInsurancePaymentDetail.OldRefEOBPaymentDetailID = _fillRefPayDtlID;
                                            oEOBInsurancePaymentDetail.OldReserveEOBPaymentID = _fillResPayID;
                                            oEOBInsurancePaymentDetail.OldReserveEOBPaymentDetailID = _fillResPayDtlID;

                                            //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                            oEOBInsurancePaymentDetail.AccountID = SelectedInsuranceCompanyID; //Convert.ToInt64(lblInsCompany.Tag);
                                            oEOBInsurancePaymentDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
                                            oEOBInsurancePaymentDetail.MSTAccountID = PatientInsuranceID; // Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                            oEOBInsurancePaymentDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
                                            oEOBInsurancePaymentDetail.ContactInsID = nContactID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));

                                            oEOBInsurancePaymentDetail.PatientID = this.PatientID;// (Int64)dRow["nPatientID"]; //PatientControl.PatientID;
                                            oEOBInsurancePaymentDetail.PaymentTrayID = SelectedPaymentTrayID;
                                            oEOBInsurancePaymentDetail.PaymentTrayCode = SelectedPaymentTrayCode;
                                            oEOBInsurancePaymentDetail.PaymentTrayDescription = SelectedPaymentTray;
                                            oEOBInsurancePaymentDetail.UserID = AppSettings.UserID;
                                            oEOBInsurancePaymentDetail.UserName = AppSettings.UserName;
                                            oEOBInsurancePaymentDetail.ClinicID = AppSettings.ClinicID;

                                            oEOBInsurancePaymentDetail.FinanceLieNo = 0;
                                            oEOBInsurancePaymentDetail.MainCreditLineID = 0;
                                            oEOBInsurancePaymentDetail.IsMainCreditLine = false;
                                            oEOBInsurancePaymentDetail.IsReserveCreditLine = false;
                                            oEOBInsurancePaymentDetail.IsCorrectionCreditLine = false;
                                            oEOBInsurancePaymentDetail.RefFinanceLieNo = _fillRefFinanceLieNo;
                                            oEOBInsurancePaymentDetail.UseRefFinanceLieNo = _fillUseRefFinanceLieNo;

                                            //if (mskCloseDate.MaskCompleted == true)
                                            //{
                                            //mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                            oEOBInsurancePaymentDetail.CloseDate = this.CloseDate; //gloDateMaster.gloDate.DateAsNumber(oPaymentInsurace.CloseDate.ToString()); //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                            //}

                                            oPaymentInsuranceLine.EOBInsurancePaymentLineDetails.Add(oEOBInsurancePaymentDetail);
                                            oEOBInsurancePaymentDetail.Dispose();


                                            #endregion
                                        }
                                    }
                                }
                                #endregion

                                _Add_WO_WH = false;

                                #region "Debit Service Line - WriteOff"

                                //if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null)
                               // if (dRow["nWriteOff"] != null && (this.ClaimStatus == 1 || this.ClaimStatus == 19 || bSecondaryAdjudication))
                                if (dRow["nWriteOff"] != null && (bSecondaryAdjudication || nResponsibilityNo == 1))
                                {
                                    oEOBInsurancePaymentDetail = new EOBPayment.Common.EOBInsurancePaymentDetail();
                                    oEOBInsurancePaymentDetail.EOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.EOBID = 0;
                                    oEOBInsurancePaymentDetail.EOBDtlID = 0;
                                    oEOBInsurancePaymentDetail.EOBPaymentDetailID = 0;

                                    oEOBInsurancePaymentDetail.BillingTransactionID = this.BillingTransactionID; // (Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                    oEOBInsurancePaymentDetail.BillingTransactionDetailID = this.BillingTransactionDetailID; // (Int64)dRow["nBillingTransactionDetailID"];//Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                    oEOBInsurancePaymentDetail.BillingTransactionLineNo = (Int32)this.BillingTransactionLineNo; // (Int32)dRow["nBillingTransactionLineNo"];//Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));

                                    oEOBInsurancePaymentDetail.SubClaimNo = this.SubclaimNo; // (string)dRow["nSubClaimNo"];//Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
                                    oEOBInsurancePaymentDetail.TrackBillingTransactionID = this.TrackTrnID;// (Int64)dRow["nTrackBLTransactionID"];//Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                    oEOBInsurancePaymentDetail.TrackBillingTransactionDetailID = this.TrackTrnDtlID; // (Int64)dRow["nTrackBLTransactionDetailID"];//Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                    oEOBInsurancePaymentDetail.TrackBillingTransactionLineNo = (Int32)this.BillingTransactionLineNo;// (Int32)dRow["nTrackBLTransactionLineNo"];//Convert.ToInt32(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_LINENO));

                                    oEOBInsurancePaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(this.DOSFrom);  //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dRow["dDOSFrom"])); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                    oEOBInsurancePaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(this.DOSTo); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dRow["dDOSTo"])); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
                                    oEOBInsurancePaymentDetail.CPTCode = this.CPT; // Convert.ToString(dRow["sCPTCode"]);
                                    oEOBInsurancePaymentDetail.CPTDescription = this.CPTDescription; // Convert.ToString(dRow["sCPTDescription"]);

                                    //if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null && c1SinglePayment.GetData(i, COL_WRITEOFF).ToString().Trim() != "")
                                    //{
                                    //    if (c1SinglePayment.GetData(i, COL_ISCORRECTION) != null && Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISCORRECTION)) == true)
                                    //    {
                                    //        oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF))) - Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WRITEOFF)));
                                    //        if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF))) != Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WRITEOFF))))
                                    //        {
                                    //            _Add_WO_WH = true;
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF)));
                                    //        _Add_WO_WH = true;
                                    //    }
                                    //}

                                    if (this.WriteOff.Trim() != "")
                                    {
                                        if (VerifyPaymentCorrection() == true)
                                        {
                                            decimal _last_WriteOff = GetLastWriteOff();
                                            oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(this.WriteOff) - _last_WriteOff;
                                            if (Convert.ToDecimal(this.WriteOff) != _last_WriteOff)
                                            {
                                                _Add_WO_WH = true;
                                            }
                                        }
                                        else
                                        {
                                            oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(this.WriteOff);
                                            _Add_WO_WH = true;
                                        }
                                    }

                                    oEOBInsurancePaymentDetail.PaymentType = EOBPaymentType.InsuracePayment;
                                    oEOBInsurancePaymentDetail.PaymentSubType = EOBPaymentSubType.WriteOff;
                                    oEOBInsurancePaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                    oEOBInsurancePaymentDetail.PayMode = this.PaymentMode ; //SelectedPaymentMode;//solving sales force case -GLO2011-0010771
                               


                                    oEOBInsurancePaymentDetail.RefEOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.RefEOBPaymentDetailID = 0;
                                    oEOBInsurancePaymentDetail.OldRefEOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.OldRefEOBPaymentDetailID = 0;

                                    oEOBInsurancePaymentDetail.ReserveEOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.ReserveEOBPaymentDetailID = 0;
                                    oEOBInsurancePaymentDetail.OldReserveEOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.OldReserveEOBPaymentDetailID = 0;

                                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                    oEOBInsurancePaymentDetail.AccountID = SelectedInsuranceCompanyID; //Convert.ToInt64(lblInsCompany.Tag);
                                    oEOBInsurancePaymentDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
                                    oEOBInsurancePaymentDetail.MSTAccountID = PatientInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                    oEOBInsurancePaymentDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
                                    oEOBInsurancePaymentDetail.ContactInsID = nContactID;//Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));

                                    oEOBInsurancePaymentDetail.PatientID = this.PatientID; // (Int64)dRow["nPatientID"]; //PatientControl.PatientID;
                                    oEOBInsurancePaymentDetail.PaymentTrayID = SelectedPaymentTrayID;
                                    oEOBInsurancePaymentDetail.PaymentTrayCode = SelectedPaymentTrayCode;
                                    oEOBInsurancePaymentDetail.PaymentTrayDescription = SelectedPaymentTray;
                                    oEOBInsurancePaymentDetail.UserID = AppSettings.UserID;
                                    oEOBInsurancePaymentDetail.UserName = AppSettings.UserName;
                                    oEOBInsurancePaymentDetail.ClinicID = AppSettings.ClinicID;

                                    oEOBInsurancePaymentDetail.FinanceLieNo = 0;
                                    oEOBInsurancePaymentDetail.MainCreditLineID = 0;
                                    oEOBInsurancePaymentDetail.IsMainCreditLine = false;
                                    oEOBInsurancePaymentDetail.IsReserveCreditLine = false;
                                    oEOBInsurancePaymentDetail.IsCorrectionCreditLine = false;
                                    oEOBInsurancePaymentDetail.RefFinanceLieNo = 0;
                                    oEOBInsurancePaymentDetail.UseRefFinanceLieNo = false;

                                    //if (mskCloseDate.MaskCompleted == true)
                                    //{
                                    //mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                    oEOBInsurancePaymentDetail.CloseDate = this.CloseDate; //gloDateMaster.gloDate.DateAsNumber(this.CloseDate.ToString()); //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                    //}

                                    if (_Add_WO_WH == true)
                                    {
                                        oPaymentInsuranceLine.EOBInsurancePaymentLineDetails.Add(oEOBInsurancePaymentDetail);
                                    }
                                    oEOBInsurancePaymentDetail.Dispose();
                                }
                                #endregion

                                _Add_WO_WH = false;

                                #region "Debit Service Line - WithHold"

                                //if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null)
                               // if (dRow["nWithhold"] != null && (this.ClaimStatus == 1 || this.ClaimStatus == 19 || bSecondaryAdjudication))
                               if (dRow["nWithhold"] != null && (bSecondaryAdjudication||nResponsibilityNo ==1))
                                {
                                    oEOBInsurancePaymentDetail = new EOBPayment.Common.EOBInsurancePaymentDetail();
                                    oEOBInsurancePaymentDetail.EOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.EOBID = 0;
                                    oEOBInsurancePaymentDetail.EOBDtlID = 0;
                                    oEOBInsurancePaymentDetail.EOBPaymentDetailID = 0;

                                    oEOBInsurancePaymentDetail.BillingTransactionID = this.BillingTransactionID;// (Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                    oEOBInsurancePaymentDetail.BillingTransactionDetailID = this.BillingTransactionDetailID; // (Int64)dRow["nBillingTransactionDetailID"];//Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                    oEOBInsurancePaymentDetail.BillingTransactionLineNo = (Int32)this.BillingTransactionLineNo; // (Int32)dRow["nBillingTransactionLineNo"];//Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));

                                    oEOBInsurancePaymentDetail.SubClaimNo = this.SubclaimNo; // (string)dRow["nSubClaimNo"];//Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
                                    oEOBInsurancePaymentDetail.TrackBillingTransactionID = this.TrackTrnID;// (Int64)dRow["nTrackBLTransactionID"];//Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                    oEOBInsurancePaymentDetail.TrackBillingTransactionDetailID = this.TrackTrnDtlID;// (Int64)dRow["nTrackBLTransactionDetailID"];//Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                    oEOBInsurancePaymentDetail.TrackBillingTransactionLineNo = (Int32)this.BillingTransactionLineNo; // (Int32)dRow["nTrackBLTransactionLineNo"];//Convert.ToInt32(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_LINENO));

                                    oEOBInsurancePaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(this.DOSFrom); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                    oEOBInsurancePaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(this.DOSTo); //gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
                                    oEOBInsurancePaymentDetail.CPTCode = this.CPT; // Convert.ToString(dRow["sCPTCode"]);  //Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                    oEOBInsurancePaymentDetail.CPTDescription = this.CPTDescription; // Convert.ToString(dRow["sCPTDescription"]); //Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

                                    //if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null && c1SinglePayment.GetData(i, COL_WITHHOLD).ToString().Trim() != "")
                                    //{
                                    //    if (c1SinglePayment.GetData(i, COL_ISCORRECTION) != null && Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISCORRECTION)) == true)
                                    //    {
                                    //        oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD))) - Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WITHHOLD)));
                                    //        if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD))) != Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WITHHOLD))))
                                    //        {
                                    //            _Add_WO_WH = true;
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD)));
                                    //        _Add_WO_WH = true;
                                    //    }
                                    //}


                                    if (this.WithHold.Trim() != "")
                                    {
                                        if (VerifyPaymentCorrection() == true)
                                        {
                                            decimal _last_WithHold = GetLastWithHold();
                                            oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(this.WithHold) - _last_WithHold;
                                            if (Convert.ToDecimal(this.WithHold) != _last_WithHold)
                                            {
                                                _Add_WO_WH = true;
                                            }
                                        }
                                        else
                                        {
                                            oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(this.WithHold);
                                            _Add_WO_WH = true;
                                        }
                                    }


                                    oEOBInsurancePaymentDetail.PaymentType = EOBPaymentType.InsuracePayment;
                                    oEOBInsurancePaymentDetail.PaymentSubType = EOBPaymentSubType.WithHold;
                                    oEOBInsurancePaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                    oEOBInsurancePaymentDetail.PayMode = this.PaymentMode; //SelectedPaymentMode;//solving sales force case -GLO2011-0010771
                                 

                                    oEOBInsurancePaymentDetail.RefEOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.RefEOBPaymentDetailID = 0;
                                    oEOBInsurancePaymentDetail.OldRefEOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.OldRefEOBPaymentDetailID = 0;
                                    oEOBInsurancePaymentDetail.ReserveEOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.ReserveEOBPaymentDetailID = 0;
                                    oEOBInsurancePaymentDetail.OldReserveEOBPaymentID = 0;
                                    oEOBInsurancePaymentDetail.OldReserveEOBPaymentDetailID = 0;

                                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                    oEOBInsurancePaymentDetail.AccountID = SelectedInsuranceCompanyID; // Convert.ToInt64(lblInsCompany.Tag);
                                    oEOBInsurancePaymentDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
                                    oEOBInsurancePaymentDetail.MSTAccountID = PatientInsuranceID; // Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                    oEOBInsurancePaymentDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
                                    oEOBInsurancePaymentDetail.ContactInsID = nContactID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));

                                    oEOBInsurancePaymentDetail.PatientID = this.PatientID; // (Int64)dRow["nPatientID"]; //PatientControl.PatientID;
                                    oEOBInsurancePaymentDetail.PaymentTrayID = SelectedPaymentTrayID;
                                    oEOBInsurancePaymentDetail.PaymentTrayCode = SelectedPaymentTrayCode;
                                    oEOBInsurancePaymentDetail.PaymentTrayDescription = SelectedPaymentTray;
                                    oEOBInsurancePaymentDetail.UserID = AppSettings.UserID;
                                    oEOBInsurancePaymentDetail.UserName = AppSettings.UserName;
                                    oEOBInsurancePaymentDetail.ClinicID = AppSettings.ClinicID;

                                    oEOBInsurancePaymentDetail.FinanceLieNo = 0;
                                    oEOBInsurancePaymentDetail.MainCreditLineID = 0;
                                    oEOBInsurancePaymentDetail.IsMainCreditLine = false;
                                    oEOBInsurancePaymentDetail.IsReserveCreditLine = false;
                                    oEOBInsurancePaymentDetail.IsCorrectionCreditLine = false;
                                    oEOBInsurancePaymentDetail.RefFinanceLieNo = 0;
                                    oEOBInsurancePaymentDetail.UseRefFinanceLieNo = false;

                                    //if (mskCloseDate.MaskCompleted == true)
                                    //{
                                    //mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                    oEOBInsurancePaymentDetail.CloseDate = this.CloseDate; //gloDateMaster.gloDate.DateAsNumber(this.CloseDate.ToString());  //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                    //}

                                    if (_Add_WO_WH == true)
                                    {
                                        oPaymentInsuranceLine.EOBInsurancePaymentLineDetails.Add(oEOBInsurancePaymentDetail);
                                    }
                                    oEOBInsurancePaymentDetail.Dispose();
                                }
                                #endregion

                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                        ActivityType.DebitServiceLine, "DebitServiceLine - End", ActivityOutCome.Success);

                                // Get Next Action & Next Party from Temporary Table 
                                // Assign it to LineNextAction object.
                                DataTable dtNextPartyAction = null;

                                dtNextPartyAction = dsTempPost.Tables[6];

                                object[] oParams = { this.ClaimNo, this.SubclaimNo, 
                                                       this.TrackTrnDtlID, this.BillingTransactionDetailID };

                                sQueryFilter = String.Format("nClaimNo = {0} and sSubClaimNo = '{1}' and nTrackMstTrnDetailID = {2} and nBillingTransactionDetailID = {3}", oParams);

                                DataRow[] dRowNextPartyActions = dtNextPartyAction.Select(sQueryFilter);

                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                        ActivityType.SetNextActionDetails, "SetNextActionDetails - Start", ActivityOutCome.Success);

                                //nTrackMstTrnDetailID = nTrackTrnDtlId
                                //nBillingTransactionDetailID = nBillingTransactionDetailID
                                foreach (DataRow dRowNextPartyAction in dRowNextPartyActions)
                                {
                                    this.NextAction = dRowNextPartyAction["NextAction"].ToString();
                                    this.NextParty = dRowNextPartyAction["NextParty"].ToString();
                                    this.NextActionContactID = dRowNextPartyAction["nNextActionContactID"].ToString();
                                    this.NextActionPatientID = dRowNextPartyAction["nNextActionPatientInsID"].ToString();
                                }
                                dRowNextPartyActions = null;
                                dtNextPartyAction = null;

                                // Added by Dev66 on 28-June-2010
                                // Called this method to set the NextAction & SplitClaim details
                                SetNextActionDetails(out oNextActions, out oSplitClaimDtls);


                                if (oNextActions.Count > 0)
                                {
                                    oPaymentInsuranceLine.LineNextAction = oNextActions[0];
                                    oTempNextActions.Add(oNextActions[0]);
                                }

                                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                        ActivityType.SetNextActionDetails, "SetNextActionDetails - End", ActivityOutCome.Success);

                                // End

                                oPaymentInsuranceLine.SVCID = this.SVCId;
                                oPaymentInsuranceLine.bIsERAPayment = true;


                                oPaymentInsuranceClaim.CliamLines.Add(oPaymentInsuranceLine);
                                oPaymentInsuranceLine.Dispose();
                                //////        }
                                //////    }
                                //////}

                                if (oEOBInsurancePaymentDetail != null) { oEOBInsurancePaymentDetail.Dispose(); }

                                #endregion

                                // Modification Required.

                                ////oPaymentInsurace.InsuranceClaims.Add(oPaymentInsuranceClaim);
                                ////oPaymentInsuranceClaim.Dispose();
                            }

                            #endregion " ......................... Claim Payment Details End ................................. "



                            #region "Reserve Debit Entry if any and it will goes directlly to payment object with credit line"

                            ////////if (IsReserveAdded()) //Check if there is any reserve entry to be made
                            ////////{
                            ////////    decimal _reserveAmt = 0;
                            ////////    //string _reserveNote = "";

                            ////////    _reserveAmt = AmountAddedToReserve();

                            ////////    //0 ReserveAmount 
                            ////////    //1 ReserveNote 
                            ////////    //3 ReserveNoteOnPrint 

                            ////////    //if (AmountAddedToReserve > 0) //Check for the reserve amount is greater than zero
                            ////////    if (_reserveAmt > 0)
                            ////////    {
                            ////////        oEOBInsurancePaymentReserveDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
                            ////////        oEOBInsurancePaymentReserveDetail.EOBPaymentID = 0;
                            ////////        oEOBInsurancePaymentReserveDetail.EOBID = 0;
                            ////////        oEOBInsurancePaymentReserveDetail.EOBDtlID = 0;
                            ////////        oEOBInsurancePaymentReserveDetail.EOBPaymentDetailID = 0;

                            ////////        oEOBInsurancePaymentReserveDetail.BillingTransactionID = 0;
                            ////////        oEOBInsurancePaymentReserveDetail.BillingTransactionDetailID = 0;
                            ////////        oEOBInsurancePaymentReserveDetail.BillingTransactionLineNo = 0;

                            ////////        oEOBInsurancePaymentReserveDetail.TrackBillingTransactionID = 0;
                            ////////        oEOBInsurancePaymentReserveDetail.TrackBillingTransactionDetailID = 0;
                            ////////        oEOBInsurancePaymentReserveDetail.TrackBillingTransactionLineNo = 0;
                            ////////        oEOBInsurancePaymentReserveDetail.SubClaimNo = "";

                            ////////        //if (mskCloseDate.MaskCompleted == true)
                            ////////        //{
                            ////////            //mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                            ////////        oEOBInsurancePaymentReserveDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(oPaymentInsurace.CloseDate.ToString());  //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                            ////////        oEOBInsurancePaymentReserveDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(oPaymentInsurace.CloseDate.ToString()); //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                            ////////        //}
                            ////////        oEOBInsurancePaymentReserveDetail.CPTCode = "";
                            ////////        oEOBInsurancePaymentReserveDetail.CPTDescription = "";

                            ////////        oEOBInsurancePaymentReserveDetail.Amount = _reserveAmt;
                            ////////        oEOBInsurancePaymentReserveDetail.IsNullAmount = false;

                            ////////        oEOBInsurancePaymentReserveDetail.PaymentType = EOBPaymentType.InsuraceReserverd;
                            ////////        oEOBInsurancePaymentReserveDetail.PaymentSubType = EOBPaymentSubType.Reserved;
                            ////////        oEOBInsurancePaymentReserveDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                            ////////        oEOBInsurancePaymentReserveDetail.PayMode = EOBPaymentMode.Check; //SelectedPaymentMode;

                            ////////        oEOBInsurancePaymentReserveDetail.RefEOBPaymentID = oEOBInsPaymentCreditDetail.RefEOBPaymentID;
                            ////////        oEOBInsurancePaymentReserveDetail.RefEOBPaymentDetailID = oEOBInsPaymentCreditDetail.RefEOBPaymentDetailID;
                            ////////        oEOBInsurancePaymentReserveDetail.ReserveEOBPaymentID = oEOBInsPaymentCreditDetail.ReserveEOBPaymentID;
                            ////////        oEOBInsurancePaymentReserveDetail.ReserveEOBPaymentDetailID = oEOBInsPaymentCreditDetail.ReserveEOBPaymentDetailID;

                            ////////        oEOBInsurancePaymentReserveDetail.OldRefEOBPaymentID = oEOBInsPaymentCreditDetail.RefEOBPaymentID;
                            ////////        oEOBInsurancePaymentReserveDetail.OldRefEOBPaymentDetailID = oEOBInsPaymentCreditDetail.RefEOBPaymentDetailID;
                            ////////        oEOBInsurancePaymentReserveDetail.OldReserveEOBPaymentID = oEOBInsPaymentCreditDetail.ReserveEOBPaymentID;
                            ////////        oEOBInsurancePaymentReserveDetail.OldReserveEOBPaymentDetailID = oEOBInsPaymentCreditDetail.ReserveEOBPaymentDetailID;


                            ////////        //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID

                            ////////        oEOBInsurancePaymentReserveDetail.AccountID = oPaymentInsurace.PayerID;
                            ////////        oEOBInsurancePaymentReserveDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
                            ////////        oEOBInsurancePaymentReserveDetail.MSTAccountID = EOBPaymentTypeAccountNo.InsuraceReserverd.GetHashCode();
                            ////////        oEOBInsurancePaymentReserveDetail.MSTAccountType = EOBPaymentAccountType.Reserved;
                            ////////        oEOBInsurancePaymentReserveDetail.PatientID = (Int64) dRow["nPatientID"]; //PatientControl.PatientID;
                            ////////        oEOBInsurancePaymentReserveDetail.PaymentTrayID = oPaymentInsurace.PaymentTrayID;//SelectedPaymentTrayID;
                            ////////        oEOBInsurancePaymentReserveDetail.PaymentTrayCode = oPaymentInsurace.PaymentTrayCode;//SelectedPaymentTrayCode;
                            ////////        oEOBInsurancePaymentReserveDetail.PaymentTrayDescription = oPaymentInsurace.PaymentTrayDesc;//SelectedPaymentTray;
                            ////////        oEOBInsurancePaymentReserveDetail.UserID = AppSettings.UserID;
                            ////////        oEOBInsurancePaymentReserveDetail.UserName = AppSettings.UserName;
                            ////////        oEOBInsurancePaymentReserveDetail.ClinicID = AppSettings.ClinicID;
                            ////////        oEOBInsurancePaymentReserveDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(oPaymentInsurace.CloseDate.ToString());  //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                            ////////        oEOBInsurancePaymentReserveDetail.FinanceLieNo = 0;
                            ////////        oEOBInsurancePaymentReserveDetail.MainCreditLineID = 0;
                            ////////        oEOBInsurancePaymentReserveDetail.IsMainCreditLine = false;
                            ////////        oEOBInsurancePaymentReserveDetail.IsReserveCreditLine = false;
                            ////////        oEOBInsurancePaymentReserveDetail.IsCorrectionCreditLine = false;
                            ////////        oEOBInsurancePaymentReserveDetail.RefFinanceLieNo = 1;
                            ////////        oEOBInsurancePaymentReserveDetail.UseRefFinanceLieNo = true;

                            ////////        //0 ReserveAmount 
                            ////////        //1 ReserveNote 
                            ////////        //2 ReserveSubType 
                            ////////        //3 ReserveNoteOnPrint 

                            ////////        #region "General Note"

                            ////////        EOBPayment.Common.PaymentInsuranceLineNote oPaymentInsuranceLineNote = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineNote();

                            ////////        oPaymentInsuranceLineNote.ClaimNo = 0;
                            ////////        oPaymentInsuranceLineNote.EOBPaymentID = 0;
                            ////////        oPaymentInsuranceLineNote.EOBID = 0;
                            ////////        oPaymentInsuranceLineNote.EOBPaymentDetailID = 0;
                            ////////        oPaymentInsuranceLineNote.BillingTransactionID = 0;
                            ////////        oPaymentInsuranceLineNote.BillingTransactionDetailID = 0;

                            ////////        oPaymentInsuranceLineNote.SubClaimNo = "";
                            ////////        oPaymentInsuranceLineNote.TrackBillingTransactionID = 0;
                            ////////        oPaymentInsuranceLineNote.TrackBillingTransactionDetailID = 0;
                            ////////        oPaymentInsuranceLineNote.TrackBillingTransactionLineNo = 0;

                            ////////        oPaymentInsuranceLineNote.Code = "";
                            ////////        oPaymentInsuranceLineNote.Description = ReserveNote();
                            ////////        oPaymentInsuranceLineNote.Amount = AmountAddedToReserve();
                            ////////        oPaymentInsuranceLineNote.IncludeOnPrint = false;
                            ////////        oPaymentInsuranceLineNote.ClinicID = AppSettings.ClinicID;
                            ////////        oPaymentInsuranceLineNote.PaymentNoteType = EOBPaymentType.InsuraceReserverd;
                            ////////        oPaymentInsuranceLineNote.PaymentNoteSubType = EOBPaymentSubType.Reserved;
                            ////////        oPaymentInsuranceLineNote.HasData = true;
                            ////////        oPaymentInsuranceLineNote.CloseDate = oPaymentInsurace.CloseDate;
                            ////////        oPaymentInsuranceLineNote.UserId = AppSettings.UserID;

                            ////////        oEOBInsurancePaymentReserveDetail.LineNotes.Add(oPaymentInsuranceLineNote);
                            ////////        oPaymentInsuranceLineNote.Dispose();

                            ////////        #endregion

                            ////////        oPaymentInsurace.EOBInsurancePaymentReserveLineDetail.Add(oEOBInsurancePaymentReserveDetail);
                            ////////        oEOBInsurancePaymentReserveDetail.Dispose();

                            ////////        //EOBInsurancePaymentMasterAllocationLines.Add();
                            ////////    }
                            ////////}
                            #endregion

                            #region "On hold selection for splitted claims "

                            //gloSplitClaim ogloSplitClaim = null;
                            //DataTable _dtHoldInfo = GetSplittedClaimsHoldInfo(oSplitClaimDetails);

                            #endregion

                            EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new EOBPayment.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);

                            //this.EOBPaymentID = ogloEOBPaymentInsurance.SaveSplitEOB(oPaymentInsurace, false, out _outEOBid);
                            ogloEOBPaymentInsurance.Dispose();
                            #endregion



                            ////// Created a new instance for storing the nextaction details which needs to be saved
                            ////EOBPayment.Common.PaymentInsuranceLineNextActions oNextActions = new EOBPayment.Common.PaymentInsuranceLineNextActions();

                            ////// Created a new instance for split claim logic
                            ////SplitClaimDetails oSplitClaimDtls = null;
                            ////oSplitClaimDtls = new SplitClaimDetails();

                            // Called this method to set the NextAction & SplitClaim details
                            //SetNextActionDetails(out oNextActions, out oSplitClaimDtls);
                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                     ActivityType.CreateERAPaymentObject, "CreateERAPaymentObject - Start ", ActivityOutCome.Success);

                            if (oSplitClaimDtls.Lines.Count > 0)
                                oSplitClaimDtls2.Lines.Add(oSplitClaimDtls.Lines[0]);

                            gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                     ActivityType.CreateERAPaymentObject, "CreateERAPaymentObject - End ", ActivityOutCome.Success);

                        } // Charges loop ends

                        if (bProcessOnce)
                        {
                            oPaymentInsurace.InsuranceClaims.Add(oPaymentInsuranceClaim);
                            oPaymentInsuranceClaim.Dispose();
                        }
                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                ActivityType.ChargeIterationEnd, "ChargeIterationEnd - End", ActivityOutCome.Success);


                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                     ActivityType.AddERAPaymentToListObject, "AddERAPaymentToListObject - Start ", ActivityOutCome.Success);


                        drChargeLines = null;


                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                     ActivityType.AddERAPaymentToListObject, "AddERAPaymentToListObject - End ", ActivityOutCome.Success);
                    }
                }

                gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                ActivityType.ClaimIterationEnd, "ClaimIterationEnd - End", ActivityOutCome.Success);

                bReturn = false; 
                if (oPaymentInsurace != null)
                {

                    sMessage = String.Format(" {0} Claims Processed", iClaimCount);

                    if (InsurancePayment.SaveERAEOBPayment(oPaymentInsurace, false, ref oProgress, ref oLabel, nBPRID) > 0)
                    {
                        oStopFlag = StopFlag.Passed;
                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                 ActivityType.SaveOperationEnds, "SaveOperationEnds", ActivityOutCome.Success);
                        bReturn = true;
                    }
                    else
                    {
                        oStopFlag = StopFlag.Error;
                        gloAuditTrail.gloAuditTrail.UpdateLog(ActivityModule.ERAPosting, ActivityCategory.ERAOriginalPosting,
                                 ActivityType.SaveOperationAborted, "SaveOperationAborted ", ActivityOutCome.Failure);
                        sMessage = "Unable to save payment";
                        //bReturn = false;
                    }

                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                oStopFlag = StopFlag.Error;
                sMessage = "Error while payment, Payment not done";
                bReturn = false;
            }
            finally
            {
                if (oPaymentInsurace != null) { oPaymentInsurace.Dispose(); }
                if (oEOBInsPaymentCreditDetail != null) { oEOBInsPaymentCreditDetail.Dispose(); }
                if (oPaymentInsuranceClaim != null) { oPaymentInsuranceClaim.Dispose(); }
                if (oEOBInsurancePaymentReserveDetail != null) { oEOBInsurancePaymentReserveDetail.Dispose(); }
                if (oSplitClaimDetails != null) { oSplitClaimDetails.Dispose(); }
                if (dtBPRClaims != null) { dtBPRClaims.Dispose(); }
                //if (oPaymentList != null) { oPaymentList = null; }
                if (dtCheckDetails != null) { dtCheckDetails.Dispose(); }
                if (dtClaimDetails != null) { dtClaimDetails.Dispose(); }
                if (_sqlTransaction != null) { _sqlTransaction.Dispose(); }
                if (sb != null) { sb = null; }
                if (dsTempPost != null) { dsTempPost = null; }
                if (dtClaims != null) { dtClaims = null; }
                if (dtCheckDetails != null) { dtCheckDetails.Dispose(); }
                if (dtClaimDetails != null) { dtClaimDetails.Dispose(); }
                if (dtDistinctClaims != null) { dtDistinctClaims.Dispose(); }
                if (dtClaimReasonCodes != null) { dtClaimReasonCodes.Dispose(); }
                InsurancePayment.UnlockCheckClaims(nBPRID);
                
            }
            return bReturn;
        }

        #endregion

        #region " Supporting/Common Methods & Function for ERA Temporary & Original Posting "

        void SetNextActionDetails(out EOBPayment.Common.PaymentInsuranceLineNextActions _PaymentInsuranceLineNextActions, out SplitClaimDetails _SplitClaimDetails)
        {
            EOBPayment.Common.PaymentInsuranceLineNextActions NextActions = new EOBPayment.Common.PaymentInsuranceLineNextActions();
            EOBPayment.Common.PaymentInsuranceLineNextAction LineNextAction = null;
            SplitClaimDetails oSplitClaimDetails = new SplitClaimDetails();
            EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new EOBPayment.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);

            try
            {
                #region " Line Next Action & Party "

                //for (int i = 0; i <= c1SinglePayment.Rows.Count - 1; i++)
                //{
                //if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.Claim)
                //{
                if (this.BillingTransactionID > 0) //(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID)).Trim() != "")
                {
                    oSplitClaimDetails.TransactionMasterID = this.BillingTransactionID; // Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                    oSplitClaimDetails.TransactionID = this.TrackTrnID; // Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                    oSplitClaimDetails.ClaimNo = this.ClaimNo; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                    oSplitClaimDetails.SubClaimNo = this.SubclaimNo; // Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
                    oSplitClaimDetails.ClinicID = AppSettings.ClinicID;

                    if (this.Payment !="" &&  Convert.ToDecimal(this.Payment.ToString().Trim()) > 0)
                        oSplitClaimDetails.IsPaymentDone = true;
                    //oSplitClaimDetails.EOBPaymentID = EOBPaymentID;
                    //oSplitClaimDetails.EOBID = EOBID;
                }
                //}

                //if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                //{
                if (this.NextAction.Trim() != "")//(c1SinglePayment.GetData(i, COL_NEXT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT)).Trim() != "")
                {
                    #region " Entire "

                    bool _addSplitLine = false;
                    //bool _isPendingLine = false;
                    LineNextAction = new EOBPayment.Common.PaymentInsuranceLineNextAction();
                    LineNextAction.ID = 0;
                    LineNextAction.ClaimNo = this.ClaimNo; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                    //LineNextAction.EOBPaymentID = EOBPaymentID;
                    //LineNextAction.EOBID = EOBID;
                    LineNextAction.EOBPaymentDetailID = 0;

                    LineNextAction.BillingTransactionID = this.BillingTransactionID; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                    LineNextAction.BillingTransactionDetailID = this.BillingTransactionDetailID;// Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));

                    LineNextAction.SubClaimNo = this.SubclaimNo; // Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
                    LineNextAction.TrackBillingTransactionID = this.TrackTrnID;// Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                    LineNextAction.TrackBillingTransactionDetailID = this.TrackTrnDtlID; // Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));

                    //...Code added on 20100129 by Sagar Ghodke
                    //...Code added to set the close date,user for Responsibility
                    //if (mskCloseDate.MaskCompleted == true)
                    //{
                    //mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    LineNextAction.CloseDate = this.CloseDate;//gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                    //}
                    LineNextAction.UserID = AppSettings.UserID;
                    LineNextAction.UserName = AppSettings.UserName;

                    //...ENd code add 20100129

                    string _nextAction = "";
                    string _nextActionCode = "";
                    string _nextActionDesc = "";

                    _nextAction = this.NextAction; // Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT)).Trim();
                    _nextActionCode = _nextAction.Substring(0, _nextAction.IndexOf('-'));
                    _nextActionDesc = _nextAction.Substring(_nextAction.IndexOf('-') + 1, (_nextAction.Length - _nextAction.IndexOf('-')) - 1);

                    //.. Split the claim for all the actions (Rebill, Bill, Pending, None)

                    if (_nextActionCode == "R" || _nextActionCode == "B" || _nextActionCode == "P" || _nextActionCode == "N")
                    {
                        LineNextAction.HasActionData = true;
                        LineNextAction.HasNextData = true;

                        _addSplitLine = true;
                    }

                    LineNextAction.NextActionCode = _nextActionCode;
                    LineNextAction.NextActionDescription = _nextActionDesc;

                    //..Logic to be implemented for retrieve next insid,insname &  party number
                    if (this.NextParty.Trim() != "") //(c1SinglePayment.GetData(i, COL_PARTY) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PARTY)).Trim() != "")
                    {
                        string _party = "";
                        string _partyCode = "";
                        string _partyDesc = "";
                        Int64 _partyInsId = 0;
                        Int64 _partyContactId = 0;

                        _party = this.NextParty; //Convert.ToString(c1SinglePayment.GetData(i, COL_PARTY)).Trim();
                        _partyCode = _party.Substring(0, _party.IndexOf('-'));
                        _partyDesc = _party.Substring(_party.IndexOf('-') + 1, (_party.Length - _party.IndexOf('-')) - 1);

                        //////.... Get the next party insuranceid & contactid number for the claim
                        ////DataTable _dt = null;
                        //////_dt = PatientStripControl.GetInsuranceParties(PatientControl.ClaimNumber, PatientControl.PatientID); //_dt = ogloEOBPaymentInsurance.GetClaimInsurances(PatientControl.ClaimNumber, PatientControl.PatientID);
                        ////_dt = PatientStripControl.GetInsuranceParties(this.ClaimNo, this.PatientID); //_dt = ogloEOBPaymentInsurance.GetClaimInsurances(PatientControl.ClaimNumber, PatientControl.PatientID);

                        ////if (_dt != null && _dt.Rows.Count > 0)
                        ////{
                        ////    for (int pIndex = 0; pIndex < _dt.Rows.Count; pIndex++)
                        ////    {
                        ////        if (Convert.ToString(_dt.Rows[pIndex]["nResponsibilityNo"]) == _partyCode)
                        ////        {
                        ////            _partyInsId = Convert.ToInt64(_dt.Rows[pIndex]["nInsuranceID"]);
                        ////            _partyContactId = Convert.ToInt64(_dt.Rows[pIndex]["nContactID"]);
                        ////            break;
                        ////        }
                        ////    }
                        ////}
                        ////if (_dt != null) { _dt.Dispose(); }
                        _partyInsId = Convert.ToInt64(this.NextActionPatientID);
                        _partyContactId = Convert.ToInt64(this.NextActionContactID);
                        LineNextAction.NextActionPatientInsID = _partyInsId; // Presently not considering.
                        LineNextAction.NextActionPatientInsName = _partyDesc.Trim();
                        LineNextAction.NextActionPartyNumber = Convert.ToInt32(_partyCode.Trim());
                        LineNextAction.NextActionContactID = _partyContactId;
                    }

                    //...Remaining balance to be transferred to selected next party
                    //...NextActionAmount = Charges - (W/O + Ins. Payment + Withhold)

                    decimal _nextActionAmount = 0;
                    //_nextActionAmount = TotalCharges - (InsuranceAmount + WriteOff + Withhold);
                    LineNextAction.NextActionAmount = _nextActionAmount;
                    //...Need to implement logic for null value
                    LineNextAction.IsNullNextActionAmount = false;

                    LineNextAction.ClinicID = AppSettings.ClinicID;
                    LineNextAction.HasData = true;

                    if (LineNextAction.NextActionPatientInsID == 0) //.self resp.
                    { LineNextAction.NextPartyType = PayerMode.Self; }
                    else
                    { LineNextAction.NextPartyType = PayerMode.Insurance; }

                    #region " Set Split Claim Details "

                    if (_addSplitLine == true)
                    {
                        SplitClaimLine oSplitLine = new SplitClaimLine();
                        oSplitLine.TransactionDetailID = LineNextAction.TrackBillingTransactionDetailID;
                        oSplitLine.TransactionMasterDetailID = LineNextAction.BillingTransactionDetailID;
                        oSplitLine.NextActionCode = LineNextAction.NextActionCode;
                        oSplitLine.InsuranceId = LineNextAction.NextActionPatientInsID;
                        oSplitLine.ContactID = LineNextAction.NextActionContactID;
                        oSplitLine.ResponsibilityNo = LineNextAction.NextActionPartyNumber;

                        oSplitLine.Party = LineNextAction.NextActionPatientInsName;

                        oSplitClaimDetails.Lines.Add(oSplitLine);
                        oSplitLine.Dispose();
                    }

                    #endregion " Set Split Claim Details "

                    NextActions.Add(LineNextAction);
                    LineNextAction = null;

                    #endregion " Entrire "
                }
                //}
                //}

                // Needs to be check before activating - pending change
                //InsurancePayment.UpdateNextActionNParty(NextActions);

                //ogloEOBPaymentInsurance.UpdateParty(NextActions);
                //ogloEOBPaymentInsurance.UpdateNextAction(NextActions);

                #endregion " Line Next Action & Party "

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            _PaymentInsuranceLineNextActions = NextActions;
            _SplitClaimDetails = oSplitClaimDetails;
        }

        Int64 PreSaveValidation(Int64 nBPRID, out string sMessage)
        {
            Int64 _retValue = 0;
            sMessage = string.Empty;

            _retValue = ClsERAValidation.ValidateCheck(nBPRID, out sMessage);

            return _retValue;
        }

        bool ISPaymentBalanced()
        {
            bool bFlag = false;

            Double nTempTotal = 0;
            // Formula : nPayment == nCharge - nWriteOff - nWithHold - nCoInsurance - nDeduct - nCoPay - OtherReasonAmount

            if (this._TotalCharges.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) + Convert.ToDouble(this._TotalCharges); }
            if (this._WriteOff.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._WriteOff); }
            if (this._WithHold.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._WithHold); }
            if (this._CoInsurance.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._CoInsurance); }
            if (this._Deductible.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._Deductible); }
            if (this._Copay.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._Copay); }
            if (this._OtherReasonAmount.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._OtherReasonAmount); }   // add on 10-July-2010 by Dev66

            //if (Convert.ToDouble(this._Payment) == nTempTotal)
            if (Math.Round(Convert.ToDouble(this._Payment), 2) == Math.Round(nTempTotal, 2))
                bFlag = true;

            return bFlag;
        }

        bool CalculateNewBalance(bool bSecondaryAdjudication)
        {
            //Method return true is new balance is -ve else return's false


            bool bFlag = false;

            Double nTempTotal = 0;

            //Formula : NewPayment == nCharge - (nPayment + nWithHold + WriteOff)
            // Correction Mode Formula : 
            //if (_IsPaymentCorrectionMode)
            //{
            //    _newBalance = (nCharge + _lastPaidAmt + _lastWOAmt + _lastWHAmt) - (nPayment + nWithHold + WriteOff);
            //}



            if (this._TotalCharges.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) + Convert.ToDouble(this._TotalCharges); }
            if (this._Payment.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._Payment); }

          // if (this.ClaimStatus == 1 || this.ClaimStatus == 19 || bSecondaryAdjudication)
            if (bSecondaryAdjudication||nResponsibilityNo ==1)
            {
                if (this._WriteOff.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._WriteOff); }
                if (this._WithHold.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._WithHold); }
            }


            if (this._PreTotalInsPaid.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._PreTotalInsPaid); }
            if (this._PreTotalPatAdjustment.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._PreTotalPatAdjustment); }
            if (this._PreTotalWithhold.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._PreTotalWithhold); }
            if (this._PreTotalWriteOff.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._PreTotalWriteOff); }
            if (this._PreTotalPatPaid.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._PreTotalPatPaid); }


            if (Math.Round(nTempTotal, 2) < 0)
                bFlag = true;

            return bFlag;
        }

        void UpdateAdjustmentAmounts(string AdjustmentAmount, ref string TotalAmount)
        {

            if (TotalAmount.Trim().Length > 0)
            {
                TotalAmount = (Convert.ToDouble(TotalAmount) + (AdjustmentAmount.Trim().Length > 0 ?Convert.ToDouble(AdjustmentAmount):0)).ToString();
            }
            else
            {
                TotalAmount = (AdjustmentAmount.Trim().Length > 0 ? AdjustmentAmount : "").ToString();
            }

            //switch (oAdjustmentType)
            //{
            //    case AdjustmentType.CoInsurance:
            //        if (TotalAmount.Trim().Length > 0)
            //        {
            //            TotalAmount = (Convert.ToDouble(TotalAmount) + Convert.ToDouble(AdjustmentAmount)).ToString();
            //        }
            //        break;
            //    case AdjustmentType.Copay:

            //        break;
            //    case AdjustmentType.Deductable:

            //        break;
            //    case AdjustmentType.OtherAdjustment:

            //        break;
            //    case AdjustmentType.WithHold:

            //        break;
            //    case AdjustmentType.WriteOff:

            //        break;
            //}
        }

        AdjustmentType GetAdjustmentTypeCode(string sGroupCode, string sReasonCode)
        {
            AdjustmentType oAdjustmentType = AdjustmentType.OtherAdjustment;

            if (((sGroupCode.Trim() + sReasonCode.Trim()) == "PR1") || ((sGroupCode.Trim() + sReasonCode.Trim()) == "PR66")) { oAdjustmentType = AdjustmentType.Deductable; }
            if ((sGroupCode.Trim() + sReasonCode.Trim()) == "PR2") { oAdjustmentType = AdjustmentType.CoInsurance; }
            if ((sGroupCode.Trim() + sReasonCode.Trim()) == "PR3") { oAdjustmentType = AdjustmentType.Copay; }
            if (((sGroupCode.Trim() + sReasonCode.Trim()) == "CO42") || ((sGroupCode.Trim() + sReasonCode.Trim()) == "CO45") || ((sGroupCode.Trim() + sReasonCode.Trim()) == "COA2")) { oAdjustmentType = AdjustmentType.WriteOff; }
            if (((sGroupCode.Trim() + sReasonCode.Trim()) == "CO104") || ((sGroupCode.Trim() + sReasonCode.Trim()) == "CO105")) { oAdjustmentType = AdjustmentType.WithHold; }
            if ((sGroupCode.Trim() + sReasonCode.Trim()) == "OA23") { oAdjustmentType = AdjustmentType.OtherAdjustment; }

            return oAdjustmentType;
        }

        DataTable GetClaimLevelAjustments(Int64 nCLPID,Int64 nSVCID,Int64 nBPRID)
        {
            DataTable dtFile = null;
            try
            {
                if (OpenConnection(false))
                {
                    _TempStr = "ERA_GetClaimLevelAjustments";  // Stored procedure to retrive ERA File details.
                    oDBPara = new gloDatabaseLayer.DBParameters();
                    oDBPara.Clear();
                    oDBPara.Add("@nCLPID", nCLPID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@nSVCID", nSVCID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@nBPRID", nBPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive(_TempStr, oDBPara, out dtFile);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                CloseConnection();
            }
            return dtFile;

        }

        DataTable GetVirtualCharges(string sERAClaimNo, Int64 nCLPId)
        {
            DataTable dtFile = null;
            try
            {
                if (OpenConnection(false))
                {
                    _TempStr = "ERA_GetSystemClaimDetails";  // Stored procedure to retrive ERA File details.
                    oDBPara = new gloDatabaseLayer.DBParameters();
                    oDBPara.Clear();
                    oDBPara.Add("@sERAClaimNo", sERAClaimNo, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBPara.Add("@nCLPID", nCLPId, ParameterDirection.Input, SqlDbType.VarChar);

                    oDB.Retrive(_TempStr, oDBPara, out dtFile);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                CloseConnection();
            }
            return dtFile;

        }


        void GetPayerSetupDetails(Int64 nBPRId)
        {
            System.Data.SqlClient.SqlDataReader dr = null;
            try
            {
                if (OpenConnection(false))
                {
                    _TempStr = "ERA_GetPayerSetupDetails";  // Stored procedure to retrive ERA File details.
                    oDBPara = new gloDatabaseLayer.DBParameters();
                    oDBPara.Clear();
                    oDBPara.Add("@nBPRID", nBPRId, ParameterDirection.Input, SqlDbType.VarChar);
                    //oDBPara.Add("@sPayerId", sPayerId, ParameterDirection.Input, SqlDbType.VarChar);

                    oDB.Retrive(_TempStr, oDBPara, out dr);

                    if (dr.HasRows)
                    {
                        dr.Read();

                        this.ZeroPaidBilled = Convert.ToBoolean(dr["bZeroPaidBilled"]);
                        this.ZeroPaidNotBilled = Convert.ToBoolean(dr["bZeroPaidNotBilled"]);
                        this.PaidNotZero = Convert.ToBoolean(dr["bPaidNotZero"]);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                dr.Close();
                CloseConnection();
            }
        }

        void UpdateClaimDetails(Hashtable htTab, ref DataTable dtClaimDetails)
        {

            // This data table contains only one row.
            Double iPayment = 0;

            foreach (DataRow oRow in dtClaimDetails.Rows)
            {

                // Apply calculated amount

                //oRow["nPayment"] = ""; SVC-03
                //oRow["nTotalCharges"] = "";  SVC-02

                if(htTab["sDeductable"].ToString().Trim().Length > 0) {iPayment = iPayment + Convert.ToDouble(htTab["sDeductable"].ToString()); }
                if(htTab["sCoInsurance"].ToString().Trim().Length > 0) {iPayment = iPayment + Convert.ToDouble(htTab["sCoInsurance"].ToString()); }
                if (htTab["sCopay"].ToString().Trim().Length > 0) { iPayment = iPayment + Convert.ToDouble(htTab["sCopay"].ToString()); }
                if (htTab["sWriteOff"].ToString().Trim().Length > 0) { iPayment = iPayment + Convert.ToDouble(htTab["sWriteOff"].ToString()); }
                if(htTab["sWithHold"].ToString().Trim().Length > 0) {iPayment = iPayment + Convert.ToDouble(htTab["sWithHold"].ToString()); }
                if(htTab["sOtherAdjustment"].ToString().Trim().Length > 0) {iPayment = iPayment + Convert.ToDouble(htTab["sOtherAdjustment"].ToString()); }
                // Charged Billed is nTotalCharges
                // Charged Paid is nPayment
                // 
                oRow["nPayment"] = Convert.ToDouble(oRow["nTotalCharges"].ToString()) - iPayment;

                oRow["nAllowed"] = Convert.ToDouble(oRow["nTotalCharges"].ToString()) - Convert.ToDouble(htTab["sWriteOff"].ToString() == "" ? "0" : htTab["sWriteOff"].ToString());
                

                oRow["nDeductible"] = htTab["sDeductable"].ToString();   //
                oRow["nCoInsurance"] = htTab["sCoInsurance"].ToString(); //
                oRow["nCopay"] = htTab["sCopay"].ToString(); //
                oRow["nWriteOff"] = htTab["sWriteOff"].ToString();
                oRow["nWithhold"] = htTab["sWithHold"].ToString();
                oRow["nOtherReasonAmount"] = htTab["sOtherAdjustment"].ToString(); 
            }

        }
        Hashtable AdjustClaimLevelCAS(DataRow oDrow, Int64 nBPRID, Int64 nCLPID, Int64 nSVCID, Int64 nClaimStatus, bool bClaimLevelCASExists)
        {
            Hashtable htTable = null;
            string sWriteOff = string.Empty;
            string sWithHold = string.Empty;
            string sDeductable = string.Empty;
            string sOtherAdjustment = string.Empty;
            string sCoInsurance = string.Empty;
            string sCopay = string.Empty;


            //// 'PR1', 'PR66' = Deduct  
            //// 'PR2' = CoIns  
            //// 'PR3 = CoPay  
            //// CO42, CO45, COA2 = Writeoff  
            //// CO104, CO105 = WithHold  


            // Existing CAS on SVC
	        sDeductable = oDrow["nDeductible"] != null ? oDrow["nDeductible"].ToString() : string.Empty;
	        sCoInsurance = oDrow["nCoInsurance"] != null ? oDrow["nCoInsurance"].ToString() : string.Empty;             
            sCopay = oDrow["nCopay"] != null ? oDrow["nCopay"].ToString() : string.Empty; 
            sWriteOff = oDrow["nWriteOff"] != null ? oDrow["nWriteOff"].ToString() : string.Empty; 
	        sWithHold = oDrow["nWithhold"] != null ? oDrow["nWithhold"].ToString() : string.Empty;
            sOtherAdjustment = oDrow["nOtherReasonAmount"] != null ? oDrow["nOtherReasonAmount"].ToString() : string.Empty;

            try
            {
                // Get Existing Claim Level Adjustments and Charge Level Adjustments

                DataTable oClaimCAS = GetClaimLevelAjustments(nCLPID, nSVCID, nBPRID);
                string sTemptotal = string.Empty;
                foreach (DataRow dRow in oClaimCAS.Rows)
                {

                    switch (ClsERAValidation.GetAdjustmentTypeCode(nBPRID, dRow["sCAS01_ClaimAdjustGroupCode"].ToString(), dRow["sCAS02_ClaimAdjustReasonCode"].ToString()))
                    {
                        case AdjustmentType.CoInsurance:
                            sTemptotal = sCoInsurance;
                            UpdateAdjustmentAmounts(dRow["sCAS03_Amount"].ToString(), ref sTemptotal);
                            sCoInsurance = sTemptotal;
                            //if (sTemptotal.Trim().Length > 0)
                            //{
                            //    sCoInsurance = ((sCoInsurance.Trim().Length > 0 ? Convert.ToDouble(sCoInsurance) : 0) + Convert.ToDouble(sTemptotal)).ToString();
                            //}
                            //else
                            //{
                            //    sCoInsurance = sTemptotal;
                            //}
                            break;
                        case AdjustmentType.Copay:
                            sTemptotal = sCopay;
                            UpdateAdjustmentAmounts(dRow["sCAS03_Amount"].ToString(), ref sTemptotal);
                            sCopay = sTemptotal;
                            break;
                        case AdjustmentType.Deductable:
                            sTemptotal = sDeductable;
                            UpdateAdjustmentAmounts(dRow["sCAS03_Amount"].ToString(), ref sTemptotal);
                            sDeductable = sTemptotal;
                            // Write log file.
                            ClsERASave.ClaimStatusLog(nBPRID, nCLPID, this.ERAClaimNo, nSVCID, "d", "Deductible Applied", LogStage.ServiceLine);
                            break;
                        case AdjustmentType.OtherAdjustment:
                            sGeneratedOtherReasonCode = dRow["sCAS01_ClaimAdjustGroupCode"].ToString() + dRow["sCAS02_ClaimAdjustReasonCode"].ToString();
                            sTemptotal = sOtherAdjustment;
                            UpdateAdjustmentAmounts(dRow["sCAS03_Amount"].ToString(), ref sTemptotal);
                            sOtherAdjustment = sTemptotal;
                            break;
                        case AdjustmentType.WithHold:
                            if (!bClaimLevelCASExists && nResponsibilityNo > 1)
                            {
                                sGeneratedOtherReasonCode = dRow["sCAS01_ClaimAdjustGroupCode"].ToString() + dRow["sCAS02_ClaimAdjustReasonCode"].ToString();
                                sTemptotal = sOtherAdjustment;
                                UpdateAdjustmentAmounts(dRow["sCAS03_Amount"].ToString(), ref sTemptotal);
                                sOtherAdjustment = sTemptotal;
                            }
                            else
                            {
                                sTemptotal = sWithHold;
                                UpdateAdjustmentAmounts(dRow["sCAS03_Amount"].ToString(), ref sTemptotal);
                                sWithHold = sTemptotal;
                            }
                            break;
                        case AdjustmentType.WriteOff:
                            if (!bClaimLevelCASExists && nResponsibilityNo > 1)
                            {
                                sGeneratedOtherReasonCode = dRow["sCAS01_ClaimAdjustGroupCode"].ToString() + dRow["sCAS02_ClaimAdjustReasonCode"].ToString();
                                sTemptotal = sOtherAdjustment;
                                UpdateAdjustmentAmounts(dRow["sCAS03_Amount"].ToString(), ref sTemptotal);
                                sOtherAdjustment = sTemptotal;
                            }
                            else
                            {
                                sTemptotal = sWriteOff;
                                UpdateAdjustmentAmounts(dRow["sCAS03_Amount"].ToString(), ref sTemptotal);
                                sWriteOff = sTemptotal;
                            }
                            break;
                        case AdjustmentType.None:
                            break;
                    }

                    // 2
                    switch (ClsERAValidation.GetAdjustmentTypeCode(nBPRID, dRow["sCAS01_ClaimAdjustGroupCode"].ToString(), dRow["sCAS05_ClaimAdjustReasonCode"].ToString()))
                    {
                        case AdjustmentType.CoInsurance:
                            sTemptotal = sCoInsurance;
                            UpdateAdjustmentAmounts(dRow["sCAS06_Amount"].ToString(), ref sTemptotal);
                            sCoInsurance = sTemptotal;
                            break;
                        case AdjustmentType.Copay:
                            sTemptotal = sCopay;
                            UpdateAdjustmentAmounts(dRow["sCAS06_Amount"].ToString(), ref sTemptotal);
                            sCopay = sTemptotal;
                            break;
                        case AdjustmentType.Deductable:
                            sTemptotal = sDeductable;
                            UpdateAdjustmentAmounts(dRow["sCAS06_Amount"].ToString(), ref sTemptotal);
                            sDeductable = sTemptotal;
                            break;
                        case AdjustmentType.OtherAdjustment:
                            sGeneratedOtherReasonCode = dRow["sCAS01_ClaimAdjustGroupCode"].ToString() + dRow["sCAS05_ClaimAdjustReasonCode"].ToString();
                            sTemptotal = sOtherAdjustment;
                            UpdateAdjustmentAmounts(dRow["sCAS06_Amount"].ToString(), ref sTemptotal);
                            sOtherAdjustment = sTemptotal;
                            break;
                        case AdjustmentType.WithHold:
                            if (!bClaimLevelCASExists && nResponsibilityNo > 1)
                            {
                                sGeneratedOtherReasonCode = dRow["sCAS01_ClaimAdjustGroupCode"].ToString() + dRow["sCAS05_ClaimAdjustReasonCode"].ToString();
                                sTemptotal = sOtherAdjustment;
                                UpdateAdjustmentAmounts(dRow["sCAS06_Amount"].ToString(), ref sTemptotal);
                                sOtherAdjustment = sTemptotal;
                            }
                            else
                            {
                                sTemptotal = sWithHold;
                                UpdateAdjustmentAmounts(dRow["sCAS06_Amount"].ToString(), ref sTemptotal);
                                sWithHold = sTemptotal;
                            }
                            break;
                        case AdjustmentType.WriteOff:

                            if (!bClaimLevelCASExists && nResponsibilityNo > 1)
                            {
                                sGeneratedOtherReasonCode = dRow["sCAS01_ClaimAdjustGroupCode"].ToString() + dRow["sCAS05_ClaimAdjustReasonCode"].ToString();
                                sTemptotal = sOtherAdjustment;
                                UpdateAdjustmentAmounts(dRow["sCAS06_Amount"].ToString(), ref sTemptotal);
                                sOtherAdjustment = sTemptotal;
                            }
                            else
                            {
                                sTemptotal = sWriteOff;
                                UpdateAdjustmentAmounts(dRow["sCAS06_Amount"].ToString(), ref sTemptotal);
                                sWriteOff = sTemptotal;
                            }
                            break;
                        case AdjustmentType.None:
                            break;
                    }

                    //3
                    switch (ClsERAValidation.GetAdjustmentTypeCode(nBPRID, dRow["sCAS01_ClaimAdjustGroupCode"].ToString(), dRow["sCAS08_ClaimAdjustReasonCode"].ToString()))
                    {
                        case AdjustmentType.CoInsurance:
                            sTemptotal = sCoInsurance;
                            UpdateAdjustmentAmounts(dRow["sCAS09_Amount"].ToString(), ref sTemptotal);
                            sCoInsurance = sTemptotal;
                            break;
                        case AdjustmentType.Copay:
                            sTemptotal = sCopay;
                            UpdateAdjustmentAmounts(dRow["sCAS09_Amount"].ToString(), ref sTemptotal);
                            sCopay = sTemptotal;
                            break;
                        case AdjustmentType.Deductable:
                            sTemptotal = sDeductable;
                            UpdateAdjustmentAmounts(dRow["sCAS09_Amount"].ToString(), ref sTemptotal);
                            sDeductable = sTemptotal;
                            break;
                        case AdjustmentType.OtherAdjustment:
                            sGeneratedOtherReasonCode = dRow["sCAS01_ClaimAdjustGroupCode"].ToString() + dRow["sCAS08_ClaimAdjustReasonCode"].ToString();
                            sTemptotal = sOtherAdjustment;
                            UpdateAdjustmentAmounts(dRow["sCAS09_Amount"].ToString(), ref sTemptotal);
                            sOtherAdjustment = sTemptotal;
                            break;
                        case AdjustmentType.WithHold:
                            if (!bClaimLevelCASExists && nResponsibilityNo > 1)
                            {
                                sGeneratedOtherReasonCode = dRow["sCAS01_ClaimAdjustGroupCode"].ToString() + dRow["sCAS08_ClaimAdjustReasonCode"].ToString();
                                sTemptotal = sOtherAdjustment;
                                UpdateAdjustmentAmounts(dRow["sCAS09_Amount"].ToString(), ref sTemptotal);
                                sOtherAdjustment = sTemptotal;
                            }
                            else
                            {
                                sTemptotal = sWithHold;
                                UpdateAdjustmentAmounts(dRow["sCAS09_Amount"].ToString(), ref sTemptotal);
                                sWithHold = sTemptotal;
                            }                            
                            break;
                        case AdjustmentType.WriteOff:
                            if (!bClaimLevelCASExists && nResponsibilityNo > 1)
                            {
                                sGeneratedOtherReasonCode = dRow["sCAS01_ClaimAdjustGroupCode"].ToString() + dRow["sCAS08_ClaimAdjustReasonCode"].ToString();
                                sTemptotal = sOtherAdjustment;
                                UpdateAdjustmentAmounts(dRow["sCAS09_Amount"].ToString(), ref sTemptotal);
                                sOtherAdjustment = sTemptotal;
                            }
                            else
                            {
                                sTemptotal = sWriteOff;
                                UpdateAdjustmentAmounts(dRow["sCAS09_Amount"].ToString(), ref sTemptotal);
                                sWriteOff = sTemptotal;
                            }  
                            break;
                        case AdjustmentType.None:
                            break;
                    }

                    //4
                    switch (ClsERAValidation.GetAdjustmentTypeCode(nBPRID, dRow["sCAS01_ClaimAdjustGroupCode"].ToString(), dRow["sCAS11_ClaimAdjustReasonCode"].ToString()))
                    {
                        case AdjustmentType.CoInsurance:
                            sTemptotal = sCoInsurance;
                            UpdateAdjustmentAmounts(dRow["sCAS12_Amount"].ToString(), ref sTemptotal);
                            sCoInsurance = sTemptotal;
                            break;
                        case AdjustmentType.Copay:
                            sTemptotal = sCopay;
                            UpdateAdjustmentAmounts(dRow["sCAS12_Amount"].ToString(), ref sTemptotal);
                            sCopay = sTemptotal;
                            break;
                        case AdjustmentType.Deductable:
                            sTemptotal = sDeductable;
                            UpdateAdjustmentAmounts(dRow["sCAS12_Amount"].ToString(), ref sTemptotal);
                            sDeductable = sTemptotal;
                            break;
                        case AdjustmentType.OtherAdjustment:
                            sGeneratedOtherReasonCode = dRow["sCAS01_ClaimAdjustGroupCode"].ToString() + dRow["sCAS11_ClaimAdjustReasonCode"].ToString();
                            sTemptotal = sOtherAdjustment;
                            UpdateAdjustmentAmounts(dRow["sCAS12_Amount"].ToString(), ref sTemptotal);
                            sOtherAdjustment = sTemptotal;
                            break;
                        case AdjustmentType.WithHold:
                            if (!bClaimLevelCASExists && nResponsibilityNo > 1)
                            {
                                sGeneratedOtherReasonCode = dRow["sCAS01_ClaimAdjustGroupCode"].ToString() + dRow["sCAS11_ClaimAdjustReasonCode"].ToString();
                                sTemptotal = sOtherAdjustment;
                                UpdateAdjustmentAmounts(dRow["sCAS12_Amount"].ToString(), ref sTemptotal);
                                sOtherAdjustment = sTemptotal;
                            }
                            else
                            {
                                sTemptotal = sWithHold;
                                UpdateAdjustmentAmounts(dRow["sCAS12_Amount"].ToString(), ref sTemptotal);
                                sWithHold = sTemptotal;
                            }  
                            break;
                        case AdjustmentType.WriteOff:
                            if (!bClaimLevelCASExists && nResponsibilityNo > 1)
                            {
                                sGeneratedOtherReasonCode = dRow["sCAS01_ClaimAdjustGroupCode"].ToString() + dRow["sCAS11_ClaimAdjustReasonCode"].ToString();
                                sTemptotal = sOtherAdjustment;
                                UpdateAdjustmentAmounts(dRow["sCAS12_Amount"].ToString(), ref sTemptotal);
                                sOtherAdjustment = sTemptotal;
                            }
                            else
                            {
                                sTemptotal = sWriteOff;
                                UpdateAdjustmentAmounts(dRow["sCAS12_Amount"].ToString(), ref sTemptotal);
                                sWriteOff = sTemptotal;
                            } 
                            break;
                        case AdjustmentType.None:
                            break;
                    }

                    //5
                    switch (ClsERAValidation.GetAdjustmentTypeCode(nBPRID, dRow["sCAS01_ClaimAdjustGroupCode"].ToString(), dRow["sCAS14_ClaimAdjustReasonCode"].ToString()))
                    {
                        case AdjustmentType.CoInsurance:
                            sTemptotal = sCoInsurance;
                            UpdateAdjustmentAmounts(dRow["sCAS15_Amount"].ToString(), ref sTemptotal);
                            sCoInsurance = sTemptotal;
                            break;
                        case AdjustmentType.Copay:
                            sTemptotal = sCopay;
                            UpdateAdjustmentAmounts(dRow["sCAS15_Amount"].ToString(), ref sTemptotal);
                            sCopay = sTemptotal;
                            break;
                        case AdjustmentType.Deductable:
                            sTemptotal = sDeductable;
                            UpdateAdjustmentAmounts(dRow["sCAS15_Amount"].ToString(), ref sTemptotal);
                            sDeductable = sTemptotal;
                            break;
                        case AdjustmentType.OtherAdjustment:
                            sGeneratedOtherReasonCode = dRow["sCAS01_ClaimAdjustGroupCode"].ToString() + dRow["sCAS14_ClaimAdjustReasonCode"].ToString();
                            sTemptotal = sOtherAdjustment;
                            UpdateAdjustmentAmounts(dRow["sCAS15_Amount"].ToString(), ref sTemptotal);
                            sOtherAdjustment = sTemptotal;
                            break;
                        case AdjustmentType.WithHold:
                            if (!bClaimLevelCASExists && nResponsibilityNo > 1)
                            {
                                sGeneratedOtherReasonCode = dRow["sCAS01_ClaimAdjustGroupCode"].ToString() + dRow["sCAS14_ClaimAdjustReasonCode"].ToString();
                                sTemptotal = sOtherAdjustment;
                                UpdateAdjustmentAmounts(dRow["sCAS15_Amount"].ToString(), ref sTemptotal);
                                sOtherAdjustment = sTemptotal;
                            }
                            else
                            {
                                sTemptotal = sWithHold;
                                UpdateAdjustmentAmounts(dRow["sCAS15_Amount"].ToString(), ref sTemptotal);
                                sWithHold = sTemptotal;
                            }
                            break;
                        case AdjustmentType.WriteOff:
                            if (!bClaimLevelCASExists && nResponsibilityNo > 1)
                            {
                                sGeneratedOtherReasonCode = dRow["sCAS01_ClaimAdjustGroupCode"].ToString() + dRow["sCAS14_ClaimAdjustReasonCode"].ToString();
                                sTemptotal = sOtherAdjustment;
                                UpdateAdjustmentAmounts(dRow["sCAS15_Amount"].ToString(), ref sTemptotal);
                                sOtherAdjustment = sTemptotal;
                            }
                            else
                            {
                                sTemptotal = sWriteOff;
                                UpdateAdjustmentAmounts(dRow["sCAS15_Amount"].ToString(), ref sTemptotal);
                                sWriteOff = sTemptotal;
                            }
                            break;
                        case AdjustmentType.None:
                            break;
                    }

                    //6
                    switch (ClsERAValidation.GetAdjustmentTypeCode(nBPRID, dRow["sCAS01_ClaimAdjustGroupCode"].ToString(), dRow["sCAS17_ClaimAdjustReasonCode"].ToString()))
                    {
                        case AdjustmentType.CoInsurance:
                            sTemptotal = sCoInsurance;
                            UpdateAdjustmentAmounts(dRow["sCAS18_Amount"].ToString(), ref sTemptotal);
                            sCoInsurance = sTemptotal;
                            break;
                        case AdjustmentType.Copay:
                            sTemptotal = sCopay;
                            UpdateAdjustmentAmounts(dRow["sCAS18_Amount"].ToString(), ref sTemptotal);
                            sCopay = sTemptotal;
                            break;
                        case AdjustmentType.Deductable:
                            sTemptotal = sDeductable;
                            UpdateAdjustmentAmounts(dRow["sCAS18_Amount"].ToString(), ref sTemptotal);
                            sDeductable = sTemptotal;
                            break;
                        case AdjustmentType.OtherAdjustment:
                            sGeneratedOtherReasonCode = dRow["sCAS01_ClaimAdjustGroupCode"].ToString() + dRow["sCAS17_ClaimAdjustReasonCode"].ToString();
                            sTemptotal = sOtherAdjustment;
                            UpdateAdjustmentAmounts(dRow["sCAS18_Amount"].ToString(), ref sTemptotal);
                            sOtherAdjustment = sTemptotal;
                            break;
                        case AdjustmentType.WithHold:
                            if (!bClaimLevelCASExists && nResponsibilityNo > 1)
                            {
                                sGeneratedOtherReasonCode = dRow["sCAS01_ClaimAdjustGroupCode"].ToString() + dRow["sCAS17_ClaimAdjustReasonCode"].ToString();
                                sTemptotal = sOtherAdjustment;
                                UpdateAdjustmentAmounts(dRow["sCAS18_Amount"].ToString(), ref sTemptotal);
                                sOtherAdjustment = sTemptotal;
                            }
                            else
                            {
                                sTemptotal = sWithHold;
                                UpdateAdjustmentAmounts(dRow["sCAS18_Amount"].ToString(), ref sTemptotal);
                                sWithHold = sTemptotal;
                            }
                            break;
                        case AdjustmentType.WriteOff:
                            if (!bClaimLevelCASExists && nResponsibilityNo > 1)
                            {
                                sGeneratedOtherReasonCode = dRow["sCAS01_ClaimAdjustGroupCode"].ToString() + dRow["sCAS17_ClaimAdjustReasonCode"].ToString();
                                sTemptotal = sOtherAdjustment;
                                UpdateAdjustmentAmounts(dRow["sCAS18_Amount"].ToString(), ref sTemptotal);
                                sOtherAdjustment = sTemptotal;
                            }
                            else
                            {
                                sTemptotal = sWriteOff;
                                UpdateAdjustmentAmounts(dRow["sCAS18_Amount"].ToString(), ref sTemptotal);
                                sWriteOff = sTemptotal;
                            }
                            break;
                        case AdjustmentType.None:
                            break;
                    }


                }

                htTable = new Hashtable();
                htTable.Add("sWriteOff", sWriteOff);
                htTable.Add("sWithHold", sWithHold);
                htTable.Add("sDeductable", sDeductable);
                htTable.Add("sOtherAdjustment", sOtherAdjustment);
                htTable.Add("sCoInsurance", sCoInsurance);
                htTable.Add("sCopay", sCopay);

                // nClaimStatus should be in (2, 3, 20 and 21)
                if ((sWriteOff.Trim().Length > 0 || sWithHold.Trim().Length > 0) 
                    && (!bClaimLevelCASExists)
                    && (nClaimStatus == 2 || nClaimStatus == 3 || nClaimStatus == 20 || nClaimStatus == 21 || nResponsibilityNo>1 ))
                {
                    // Write Posting Logs
                    //ERA_IN_PostingLogs @nBPRID, @nCLPId, @sERAClaimNo, @nSVCID, 'w', '', 3 
                    ClsERASave.ClaimStatusLog(nBPRID, nCLPID, this.ERAClaimNo, nSVCID, "w", "Adjustments Not Posted", LogStage.ServiceLine);
                }

                if (sDeductable.Trim().Length > 0 && sDeductable!="0.00" )
                {
                   ClsERASave.ClaimStatusLog(nBPRID, nCLPID, this.ERAClaimNo, nSVCID, "d", "Deductible Applied", LogStage.ServiceLine);
                }
               


                return htTable;
            }
            catch (Exception ex)
            {
                return null;
                throw (ex);
            }
            finally
            {
                htTable = null;
            }
        }

        bool IsPaymentMade()
        {
            if (this.CheckAmount >= 0)
            {
                return true;
            }
            return false;
        }

        void FillPaymentTray()
        {
            Int64 _defaultTrayID = InsurancePayment.GetDefaultPaymentTrayID();

            // Set default payment tray
            SelectedPaymentTray = InsurancePayment.GetPaymentTrayDescription(_defaultTrayID);
            SelectedPaymentTrayID = _defaultTrayID;
            SelectedPaymentTrayCode = string.Empty;

        }

        void FillPaymentTray(Int64 _defaultTrayID)
        {
            //Int64 _defaultTrayID = InsurancePayment.GetDefaultPaymentTrayID();

            // Set default payment tray
            SelectedPaymentTray = InsurancePayment.GetPaymentTrayDescription(_defaultTrayID);
            SelectedPaymentTrayID = _defaultTrayID;
            SelectedPaymentTrayCode = string.Empty;

        }

        string ReserveNote()
        {
            return string.Empty;
        }

        decimal AmountAddedToReserve()
        {
            return Convert.ToDecimal(0);
        }

        bool IsReserveAdded()
        {
            return false;
        }

        decimal GetLastWithHold()
        {
            return Convert.ToDecimal(0);
        }

        decimal GetLastWriteOff()
        {
            return Convert.ToDecimal(0);
        }

        string GetLastPayment()
        {
            return "0.00";
        }

        bool VerifyPaymentCorrection()
        {
            return false;
        }

        decimal CalculateReasonAmount(string ReasonCode)
        {
            return Convert.ToDecimal(0);
        }

        EOBPayment.Common.PaymentInsurance GetPaymentMaster()
        {
            EOBPayment.Common.PaymentInsurance oPaymentInsurace = new EOBPayment.Common.PaymentInsurance();

            EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new global::gloBilling.EOBPayment.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);
            string _PaymentNo = ogloEOBPaymentInsurance.GetPaymentPrefixNumber(_paymentPrefix).Trim();

            this.PaymentNumber = _PaymentNo;
            oPaymentInsurace.PaymentNumber = _PaymentNo;
            oPaymentInsurace.PaymentNumberPefix = _paymentPrefix;
            oPaymentInsurace.EOBPaymentID = 0;
            this.EOBPaymentID = oPaymentInsurace.EOBPaymentID;
            oPaymentInsurace.EOBRefNO = "";

            //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
            oPaymentInsurace.PayerID = SelectedInsuranceCompanyID; // (Int64)dRow["sPayerID"];
            oPaymentInsurace.PayerName = SelectedInsuranceCompany; //dRow["sPayerName"].ToString();
            oPaymentInsurace.PayerType = EOBPaymentAccountType.InsuranceCompany;

            oPaymentInsurace.PaymentMode = EOBPaymentMode.Check;
            oPaymentInsurace.CheckNumber = this.CheckNumber; //dRow["sCheckNo"].ToString()
            oPaymentInsurace.CheckAmount = this.CheckAmount; //(decimal) dRow["nCheckAmount"];

            //if (mskCheckDate.MaskCompleted)
            //{
            //mskCheckDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            oPaymentInsurace.CheckDate = gloDateMaster.gloDate.DateAsNumber(this.CheckDate); //dRow["dCheckDate"].ToString()
            //this.CloseDate = oPaymentInsurace.CheckDate;
            //}


            oPaymentInsurace.MSTAccountID = SelectedInsuranceCompanyID;//(Int64) dRow["nMSTAccountID"];
            oPaymentInsurace.MSTAccountType = EOBPaymentAccountType.InsuranceCompany;



            oPaymentInsurace.ClinicID = AppSettings.ClinicID;
            oPaymentInsurace.CreatedDateTime = DateTime.Now;
            oPaymentInsurace.ModifiedDateTime = DateTime.Now;

            oPaymentInsurace.PaymentTrayID = SelectedPaymentTrayID;
            oPaymentInsurace.PaymentTrayCode = SelectedPaymentTrayCode;
            oPaymentInsurace.PaymentTrayDesc = SelectedPaymentTray;

            //if (mskCloseDate.MaskCompleted == true)
            //{
            //mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            oPaymentInsurace.CloseDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToString());
            this.CloseDate = oPaymentInsurace.CloseDate;
            //}

            oPaymentInsurace.UserID = AppSettings.UserID;
            oPaymentInsurace.UserName = AppSettings.UserName;

            #region "Payment Master Note"

            //Notes if any to main payment to all claim OR main payment to reserve account
            ////if ("TestNotest".Trim().Length > 0)
            ////{
            ////    EOBPayment.Common.PaymentInsuranceLineNote oPaymentInsuranceLineNote = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineNote();

            ////    oPaymentInsuranceLineNote.EOBPaymentID = (Int64)dRow["nEOBPaymentID"];
            ////    oPaymentInsuranceLineNote.Description = "Test notes"; // txtPayMstNotes.Text.Trim();
            ////    oPaymentInsuranceLineNote.Amount = (decimal)dRow["nCheckAmount"];
            ////    oPaymentInsuranceLineNote.IncludeOnPrint = true; //chkPayMstIncludeNotes.Checked;
            ////    oPaymentInsuranceLineNote.ClinicID = AppSettings.ClinicID;
            ////    oPaymentInsuranceLineNote.PaymentNoteType = EOBPaymentType.InsuracePayment;
            ////    oPaymentInsuranceLineNote.PaymentNoteSubType = EOBPaymentSubType.Other;
            ////    oPaymentInsuranceLineNote.HasData = true;
            ////    oPaymentInsuranceLineNote.CloseDate = oPaymentInsurace.CloseDate;
            ////    oPaymentInsuranceLineNote.UserId = AppSettings.UserID;

            ////    oPaymentInsurace.Notes.Add(oPaymentInsuranceLineNote);
            ////    oPaymentInsuranceLineNote.Dispose();
            ////}

            #endregion

            return oPaymentInsurace;
        }

        EOBPayment.Common.EOBInsurancePaymentDetail GetMainCreditLineEntry(int FinanceLineNo)
        {
            EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsPaymentCreditDetail = new EOBPayment.Common.EOBInsurancePaymentDetail();

            if (IsPaymentInProcess)
            {
                for (int i = 0; i <= EOBInsurancePaymentMasterLines.Count - 1; i++)
                {
                    //..Code changes done by Sagar Ghodke on 20100105(critical change Confirmation needed)
                    //...Below commented condition is previous one
                    //if (EOBInsurancePaymentMasterLines[i].PaymentType == EOBPaymentType.InsuracePayment)
                    if (EOBInsurancePaymentMasterLines[i].PaymentType == EOBPaymentType.InsuracePayment && EOBInsurancePaymentMasterLines[i].PaymentSubType == EOBPaymentSubType.Insurace)
                    {
                        oEOBInsPaymentCreditDetail.EOBPaymentID = EOBInsurancePaymentMasterLines[i].EOBPaymentID;
                        oEOBInsPaymentCreditDetail.EOBID = EOBInsurancePaymentMasterLines[i].EOBID;
                        oEOBInsPaymentCreditDetail.EOBDtlID = EOBInsurancePaymentMasterLines[i].EOBDtlID;
                        oEOBInsPaymentCreditDetail.EOBPaymentDetailID = EOBInsurancePaymentMasterLines[i].EOBPaymentDetailID;

                        oEOBInsPaymentCreditDetail.RefEOBPaymentID = EOBInsurancePaymentMasterLines[i].RefEOBPaymentID;
                        oEOBInsPaymentCreditDetail.RefEOBPaymentDetailID = EOBInsurancePaymentMasterLines[i].RefEOBPaymentDetailID;
                        //oEOBInsPaymentCreditDetail.ReserveEOBPaymentID = 0;
                        //oEOBInsPaymentCreditDetail.ReserveEOBPaymentDetailID = 0;

                        oEOBInsPaymentCreditDetail.OldRefEOBPaymentID = EOBInsurancePaymentMasterLines[i].RefEOBPaymentID;
                        oEOBInsPaymentCreditDetail.OldRefEOBPaymentDetailID = EOBInsurancePaymentMasterLines[i].RefEOBPaymentDetailID;
                        oEOBInsPaymentCreditDetail.OldReserveEOBPaymentID = EOBInsurancePaymentMasterLines[i].ReserveEOBPaymentID;
                        oEOBInsPaymentCreditDetail.OldReserveEOBPaymentDetailID = EOBInsurancePaymentMasterLines[i].ReserveEOBPaymentDetailID;

                        //..added code line of break by sagar ghodke on 20100105
                        break;
                    }
                    //..end code changes sagar ghodke 20100105
                }
            }

            //if (mskCloseDate.MaskCompleted == true)
            //{
            //mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            oEOBInsPaymentCreditDetail.DOSFrom = this.CloseDate; //gloDateMaster.gloDate.DateAsNumber(oPaymentInsurance.CloseDate);
            oEOBInsPaymentCreditDetail.DOSTo = this.CloseDate;   //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
            //}

            if (this.CheckAmount != 0) //(txtCheckAmount.Text.Trim().Length > 0)
            {
                oEOBInsPaymentCreditDetail.Amount = this.CheckAmount; //CheckAmount; //Convert.ToDecimal(txtCheckAmount.Text);
                oEOBInsPaymentCreditDetail.IsNullAmount = false;
            }


            oEOBInsPaymentCreditDetail.PaymentType = EOBPaymentType.InsuracePayment;
            oEOBInsPaymentCreditDetail.PaymentSubType = EOBPaymentSubType.Insurace;
            oEOBInsPaymentCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
            //oEOBInsPaymentCreditDetail.PayMode = EOBPaymentMode.Check; //SelectedPaymentMode;
            oEOBInsPaymentCreditDetail.PayMode = this.PaymentMode;//SelectedPaymentMode;//solving sales force case -GLO2011-0010771

            //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
            oEOBInsPaymentCreditDetail.AccountID = SelectedInsuranceCompanyID; //Convert.ToInt64(lblInsCompany.Tag);
            oEOBInsPaymentCreditDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;

            //if (lblPayer.Tag != null && Convert.ToString(lblPayer.Tag).Trim() != "")
            if (PatientInsuranceID != 0)
            {
                oEOBInsPaymentCreditDetail.MSTAccountID = PatientInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                oEOBInsPaymentCreditDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
                oEOBInsPaymentCreditDetail.ContactInsID = ContactInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));
            }

            oEOBInsPaymentCreditDetail.PatientID = this.PatientID;//(Int64) dRow["nPatientID"]; //PatientControl.PatientID;
            oEOBInsPaymentCreditDetail.PaymentTrayID = SelectedPaymentTrayID;
            oEOBInsPaymentCreditDetail.PaymentTrayCode = SelectedPaymentTrayCode;
            oEOBInsPaymentCreditDetail.PaymentTrayDescription = SelectedPaymentTray;
            oEOBInsPaymentCreditDetail.UserID = AppSettings.UserID;
            oEOBInsPaymentCreditDetail.UserName = AppSettings.UserName;
            oEOBInsPaymentCreditDetail.ClinicID = AppSettings.ClinicID;

            oEOBInsPaymentCreditDetail.FinanceLieNo = FinanceLineNo;
            oEOBInsPaymentCreditDetail.IsMainCreditLine = true;

            //if (mskCloseDate.MaskCompleted == true)
            //{
            //mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            oEOBInsPaymentCreditDetail.CloseDate = this.CloseDate; //gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
            //}

            return oEOBInsPaymentCreditDetail;
        }

        #region " Methods performs database operations & returns DataTable object. Assignment using DataRow "

        void SetReasonCodes()
        {
            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(AppSettings.ConnectionStringPM);
            object oSetValue = null;

            oSetting.GetSetting("RCODEWRITEOFF", out oSetValue);
            if (oSetValue != null && oSetValue.ToString().Trim() != "")
            { _REASONCODE_WRITEOFF = oSetValue.ToString().Trim(); } oSetValue = null;

            oSetting.GetSetting("RCODECOPAY", out oSetValue);
            if (oSetValue != null && oSetValue.ToString().Trim() != "")
            { _REASONCODE_COPAY = oSetValue.ToString().Trim(); } oSetValue = null;

            oSetting.GetSetting("RCODEDEDUCTIBLE", out oSetValue);
            if (oSetValue != null && oSetValue.ToString().Trim() != "")
            { _REASONCODE_DEDUCTIBLE = oSetValue.ToString().Trim(); } oSetValue = null;

            oSetting.GetSetting("RCODECOINSURANCE", out oSetValue);
            if (oSetValue != null && oSetValue.ToString().Trim() != "")
            { _REASONCODE_COINSURANCE = oSetValue.ToString().Trim(); } oSetValue = null;

            oSetting.GetSetting("RCODEWITHHOLD", out oSetValue);
            if (oSetValue != null && oSetValue.ToString().Trim() != "")
            { _REASONCODE_WITHHOLD = oSetValue.ToString().Trim(); } oSetValue = null;

            if (oSetting != null) { oSetting.Dispose(); oSetting = null; }
        }

        string GetSelectedReasonCode(int ColumnIndex)
        {
            string _Return = "";
            switch (ColumnIndex)
            {
                case COL_WRITEOFF:
                    _Return = _REASONCODE_WRITEOFF;
                    break;
                case COL_COPAY:
                    _Return = _REASONCODE_COPAY;
                    break;
                case COL_DEDUCTIBLE:
                    _Return = _REASONCODE_DEDUCTIBLE;
                    break;
                case COL_COINSURANCE:
                    _Return = _REASONCODE_COINSURANCE;
                    break;
                case COL_WITHHOLD:
                    _Return = _REASONCODE_WITHHOLD;
                    break;
            }
            return _Return;

        }

        bool ValidateDataSet(DataSet ds)
        {
            bool blnFlag = false;

            try
            {
                if (ds != null)
                    blnFlag = true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

            return blnFlag;
        }

        bool GetClaimCount(DataTable dt)
        {
            bool blnFlag = false;

            try
            {
                if (dt != null)
                    if (dt.Rows.Count > 0)
                        blnFlag = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

            return blnFlag;
        }

        bool FillMasterDetails(DataTable dtERA)
        {
            bool blnFlag = false;

            if (dtERA != null)
            {
                if (dtERA.Rows.Count > 0)
                {
                    DataRow dRow = dtERA.Rows[0];

                    SelectedInsuranceCompanyID = Convert.ToInt64(dRow["sPayerID"]);
                    SelectedInsuranceCompany = dRow["sPayerName"].ToString();
                    this.CheckNumber = dRow["sCheckNo"].ToString();
                    this.CheckAmount = Convert.ToDecimal(dRow["CheckAmount"]);
                    this.CheckDate = dRow["dCheckDate"].ToString();
                    this.MSTAccountID = Convert.ToInt64(dRow["sPayerID"]);
                    this.PLBAmount = Convert.ToInt64(dRow["PLBAmount"]);
                    this._PaymentMode = (EOBPaymentMode)Convert.ToInt32(dRow["nPaymentMode"]);//solving sales force case -GLO2011-0010771
                    dRow = null;

                    blnFlag = true;
                }
            }

            return blnFlag;
        }


        void FillClaimDetails(DataRow dRow)
        {
            try
            {
                this.PatientID = Convert.ToInt64(dRow["nPatientID"]);
                this.ClaimNo = Convert.ToInt64(dRow["nClaimNo"]);   //Int64 nClaimNo = Convert.ToInt64(dtClaimRow["sCLP01_ClaimSubmitterID"]);
                this.SVCId = Convert.ToInt64(dRow["nSVCId"]);
                this.SubclaimNo = dRow["sSubClaimNo"].ToString();
                this.ClaimStatus = Convert.ToInt64(dRow["ClaimStatus"]);
                this.BillingTransactionID = Convert.ToInt64(dRow["nBillingTransactionID"]);
                this.BillingTransactionDetailID = Convert.ToInt64(dRow["nBillingTransactionDetailID"]);
                this.BillingTransactionLineNo = Convert.ToInt64(dRow["nBillingTransactionLineNo"]);
                this.TrackTrnID = Convert.ToInt64(dRow["nTrackTrnId"]);
                this.TrackTrnDtlID = Convert.ToInt64(dRow["nTrackTrnDtlId"]);
                this.CPT = dRow["sCPTCode"].ToString();
                this.CPTDescription = dRow["sCPTDescription"].ToString();
                this.DOSFrom = dRow["dDOSFrom"].ToString();
                if (dRow["dDOSTo"].ToString() != "")
                    this.DOSTo = dRow["dDOSTo"].ToString();
                else
                    this.DOSTo = this.DOSFrom;
                this.Charges = dRow["nCharges"].ToString();
                this.Unit = dRow["sUnit"].ToString();
                this.TotalCharges = dRow["nTotalCharges"].ToString();
                this.Allowed = dRow["nAllowed"].ToString();
                this.Payment = dRow["nPayment"].ToString();
                this.Deductible = dRow["nDeductible"].ToString();
                this.CoInsurance = dRow["nCoInsurance"].ToString();
                this.Copay = dRow["nCopay"].ToString();
                this.WriteOff = dRow["nWriteOff"].ToString();
                this.WithHold = dRow["nWithhold"].ToString();

                this.OtherReasonAmount = dRow["nOtherReasonAmount"].ToString();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        void ResetClaimDetails()
        {
            this.PatientID = 0;
            this.ClaimNo = 0;
            this.SVCId = 0;
            this.SubclaimNo = string.Empty;
            this.ClaimStatus = 0;
            this.BillingTransactionID = 0;
            this.BillingTransactionDetailID = 0;
            this.BillingTransactionLineNo = 0;
            this.TrackTrnID = 0;
            this.TrackTrnDtlID = 0;
            this.CPT = string.Empty;
            this.CPTDescription = string.Empty;
            this.DOSFrom = string.Empty;
            this.DOSTo = string.Empty;
            this.Charges = string.Empty;
            this.Unit = string.Empty;
            this.TotalCharges = string.Empty;
            this.Allowed = string.Empty;
            this.Payment = string.Empty;
            this.Deductible = string.Empty;
            this.CoInsurance = string.Empty;
            this.Copay = string.Empty;
            this.WriteOff = string.Empty;
            this.WithHold = string.Empty;
            this.OtherReasonAmount = string.Empty;
        }

        DataSet GetERAFileDetails(Int64 nBPRID)
        {
            DataSet dsFile = null;
            try
            {
                if (OpenConnection(false))
                {
                    _TempStr = "BL_CheckDetails_ERA";  // Stored procedure to retrive ERA File details.
                    oDBPara = new gloDatabaseLayer.DBParameters();
                    oDBPara.Clear();
                    oDBPara.Add("@nBPRID", nBPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive(_TempStr, oDBPara, out dsFile);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                CloseConnection();
            }
            return dsFile;
        }

        DataTable GetCheckDetails(Int64 nBPRID, Int64 nContactID)
        {
            DataTable dtFile = null;
            try
            {
                if (OpenConnection(false))
                {
                    _TempStr = "ERA_GetCheckDetails";  // Stored procedure to retrive ERA File details.
                    oDBPara = new gloDatabaseLayer.DBParameters();
                    oDBPara.Clear();
                    oDBPara.Add("@nBPRID", nBPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@nContactId", nContactID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive(_TempStr, oDBPara, out dtFile);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                CloseConnection();
            }
            return dtFile;
        }

        DataTable GetCheckClaimDetails(Int64 nBPRID, Int64 nContactID, string ERAClaimNo, Int64 nCLPId,Int64 nResponsibilityNo)
        {
            DataTable dtFile = null;
            try
            {
                if (OpenConnection(false))
                {
                    _TempStr = "ERA_GetCheckClaimDetails";  // Stored procedure to retrive ERA File details.
                    oDBPara = new gloDatabaseLayer.DBParameters();
                    oDBPara.Clear();
                    oDBPara.Add("@nBPRID", nBPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@nContactId", nContactID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@sERAClaimNo", ERAClaimNo, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBPara.Add("@nCLPId", nCLPId, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@nResponsibilityNo", nResponsibilityNo, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive(_TempStr, oDBPara, out dtFile);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                CloseConnection();
            }
            return dtFile;
        }

        DataTable GetAllBPRClaims(Int64 nBPRID)
        {
            DataTable dtFile = null;
            try
            {
                if (OpenConnection(false))
                {
                    _TempStr = "ERA_GetClaimsForBPR";  // Stored procedure to retrive Claim against BPR.
                    oDBPara = new gloDatabaseLayer.DBParameters();
                    oDBPara.Clear();
                    oDBPara.Add("@nBPRID", nBPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive(_TempStr, oDBPara, out dtFile);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                CloseConnection();
            }
            return dtFile;
        }

        DataTable GetOtherReasonCodes(Int64 ID, bool IsClaimLevel,int nResponsibilityNo)
        {
            DataTable dtFile = null;
            try
            {
                if (OpenConnection(false))
                {
                    _TempStr = "ERA_GetOtherReasonCodes";  // Stored procedure to retrive claim service line other reason codes.
                    oDBPara = new gloDatabaseLayer.DBParameters();
                    oDBPara.Clear();
                    oDBPara.Add("@ID", ID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@IsClaimLevel", IsClaimLevel, ParameterDirection.Input, SqlDbType.Bit);
                    oDBPara.Add("@nResponsibilityNo", nResponsibilityNo, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive(_TempStr, oDBPara, out dtFile);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                CloseConnection();
            }
            return dtFile;
        }

        DataSet GetTemporaryPostedData(Int64 nBPRID)
        {
            DataSet dsFile = null;
            try
            {
                if (OpenConnection(false))
                {
                    _TempStr = "BL_GetEOBTemporaryPostedData";  // Stored procedure to retrive Claim against BPR.
                    oDBPara = new gloDatabaseLayer.DBParameters();
                    oDBPara.Clear();
                    oDBPara.Add("@nBPRID", nBPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive(_TempStr, oDBPara, out dsFile);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                CloseConnection();
            }
            return dsFile;
        }

        public DataTable GetTemporaryPostedCheckDetails(Int64 nBPRID)
        {
            DataTable dtFile = null;
            try
            {
                if (OpenConnection(false))
                {
                    _TempStr = "BL_GetTemporaryPostedCheckDetails";  // Stored procedure to retrive Claim against BPR.
                    oDBPara = new gloDatabaseLayer.DBParameters();
                    oDBPara.Clear();
                    oDBPara.Add("@nBPRID", nBPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive(_TempStr, oDBPara, out dtFile);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                CloseConnection();
            }
            return dtFile;
        }

        private EOBInsurancePaymentDetail GetCreditLineForReserveUsed(EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine EOBInsurancePaymentMasterLine, int FinanceLineNo)
        {
            EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsPaymentResAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();

            oEOBInsPaymentResAsCreditDetail.EOBPaymentID = EOBInsurancePaymentMasterLine.EOBPaymentID;
            oEOBInsPaymentResAsCreditDetail.EOBID = EOBInsurancePaymentMasterLine.EOBID;
            oEOBInsPaymentResAsCreditDetail.EOBDtlID = EOBInsurancePaymentMasterLine.EOBDtlID;
            oEOBInsPaymentResAsCreditDetail.EOBPaymentDetailID = EOBInsurancePaymentMasterLine.EOBPaymentDetailID;

            oEOBInsPaymentResAsCreditDetail.RefEOBPaymentID = EOBInsurancePaymentMasterLine.RefEOBPaymentID;
            oEOBInsPaymentResAsCreditDetail.RefEOBPaymentDetailID = EOBInsurancePaymentMasterLine.RefEOBPaymentDetailID;
            oEOBInsPaymentResAsCreditDetail.ReserveEOBPaymentID = EOBInsurancePaymentMasterLine.ReserveEOBPaymentID;
            oEOBInsPaymentResAsCreditDetail.ReserveEOBPaymentDetailID = EOBInsurancePaymentMasterLine.ReserveEOBPaymentDetailID;

            oEOBInsPaymentResAsCreditDetail.OldRefEOBPaymentID = EOBInsurancePaymentMasterLine.RefEOBPaymentDetailID;
            oEOBInsPaymentResAsCreditDetail.OldRefEOBPaymentDetailID = EOBInsurancePaymentMasterLine.RefEOBPaymentDetailID;
            oEOBInsPaymentResAsCreditDetail.OldReserveEOBPaymentID = EOBInsurancePaymentMasterLine.ReserveEOBPaymentID;
            oEOBInsPaymentResAsCreditDetail.OldReserveEOBPaymentDetailID = EOBInsurancePaymentMasterLine.ReserveEOBPaymentDetailID;

            #region "Set Object"

            oEOBInsPaymentResAsCreditDetail.BillingTransactionID = 0;
            oEOBInsPaymentResAsCreditDetail.BillingTransactionDetailID = 0;
            oEOBInsPaymentResAsCreditDetail.BillingTransactionLineNo = 0;

            oEOBInsPaymentResAsCreditDetail.SubClaimNo = "";
            oEOBInsPaymentResAsCreditDetail.TrackBillingTransactionID = 0;
            oEOBInsPaymentResAsCreditDetail.TrackBillingTransactionDetailID = 0;
            oEOBInsPaymentResAsCreditDetail.TrackBillingTransactionLineNo = 0;

            //if (mskCloseDate.MaskCompleted == true)
            //{
            //    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            //    oEOBInsPaymentResAsCreditDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
            //    oEOBInsPaymentResAsCreditDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
            //}

            oEOBInsPaymentResAsCreditDetail.DOSFrom = this.CloseDate; // gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
            oEOBInsPaymentResAsCreditDetail.DOSTo = this.CloseDate; // gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);


            oEOBInsPaymentResAsCreditDetail.CPTCode = "";
            oEOBInsPaymentResAsCreditDetail.CPTDescription = "";
            oEOBInsPaymentResAsCreditDetail.Amount = EOBInsurancePaymentMasterLine.Amount;
            oEOBInsPaymentResAsCreditDetail.IsNullAmount = false;

            //Pending - idenify is patient reserve or insurace reserve
            EOBPaymentType _ResIsPatOrIns = EOBPaymentType.InsuraceReserverd;
            oEOBInsPaymentResAsCreditDetail.PaymentType = _ResIsPatOrIns;
            oEOBInsPaymentResAsCreditDetail.PaymentSubType = EOBPaymentSubType.Reserved;
            oEOBInsPaymentResAsCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
            oEOBInsPaymentResAsCreditDetail.PayMode = EOBPaymentMode.None; // (EOBPaymentMode)_crResPayMode;

            //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID

            //Pending - idenify is patient reserve or insurace reserve
            Int64 _ResIDIsPatOrIns = EOBPaymentTypeAccountNo.InsuraceReserverd.GetHashCode();
            Int64 _MstResIDIsPatOrIns = EOBPaymentTypeAccountNo.InsuraceReserverd.GetHashCode();
            oEOBInsPaymentResAsCreditDetail.AccountID = _ResIDIsPatOrIns;
            oEOBInsPaymentResAsCreditDetail.AccountType = EOBPaymentAccountType.Reserved;
            oEOBInsPaymentResAsCreditDetail.MSTAccountID = _MstResIDIsPatOrIns;
            oEOBInsPaymentResAsCreditDetail.MSTAccountType = EOBPaymentAccountType.Reserved;

            oEOBInsPaymentResAsCreditDetail.PatientID = this.PatientID; // PatientControl.PatientID; // _PatientID;
            oEOBInsPaymentResAsCreditDetail.PaymentTrayID = SelectedPaymentTrayID; //_CloseDayTrayID;
            oEOBInsPaymentResAsCreditDetail.PaymentTrayCode = SelectedPaymentTrayCode; //_CloseDayTrayCode;
            oEOBInsPaymentResAsCreditDetail.PaymentTrayDescription = SelectedPaymentTray; //_CloseDayTrayName;
            oEOBInsPaymentResAsCreditDetail.UserID = AppSettings.UserID;
            oEOBInsPaymentResAsCreditDetail.UserName = AppSettings.UserName; //_UserName;
            oEOBInsPaymentResAsCreditDetail.ClinicID = AppSettings.ClinicID;

            oEOBInsPaymentResAsCreditDetail.FinanceLieNo = FinanceLineNo;
            oEOBInsPaymentResAsCreditDetail.MainCreditLineID = EOBInsurancePaymentMasterLine.MainCreditLineID;
            oEOBInsPaymentResAsCreditDetail.IsMainCreditLine = false;
            oEOBInsPaymentResAsCreditDetail.IsReserveCreditLine = true;
            oEOBInsPaymentResAsCreditDetail.IsCorrectionCreditLine = false;
            oEOBInsPaymentResAsCreditDetail.RefFinanceLieNo = 0; //to do
            oEOBInsPaymentResAsCreditDetail.UseRefFinanceLieNo = false; //to do

            if (oEOBInsPaymentResAsCreditDetail.CloseDate <= 0)
            {
                //if (mskCloseDate.MaskCompleted == true)
                //{
                //    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                //    oEOBInsPaymentResAsCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                //}
                oEOBInsPaymentResAsCreditDetail.CloseDate = this.CloseDate;
            }

            #endregion

            return oEOBInsPaymentResAsCreditDetail;
        }

        #endregion

        #endregion

        #endregion

        #region " Open/Close Database Connection "

        private bool OpenConnection(bool withParameters)
        {
            bool _Result = false;
            try
            {
                if (_DataBaseConnectionString != "")
                {
                    oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
                    oDB.Connect(false);
                    if (withParameters)
                        oDBPara = new gloDatabaseLayer.DBParameters();
                    _Result = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _Result;
        }

        private void CloseConnection()
        {
            if (oDB != null)
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
            }
            if (oDBPara != null)
            {
                oDBPara.Dispose();
                oDBPara = null;
            }
        }

        #endregion

        #region " Code added for saving temporary data and assigning it to "oPaymentInsurance" object. "

        private DataTable GetSplittedClaimsHoldInfo(SplitClaimDetails oSplitClaimDetails)
        {
            DataTable _dtHoldInfo = null;
            DataRow _drParentClaimHoldNote = null;

            gloSplitClaim ogloSplitClaim = new gloSplitClaim(AppSettings.ConnectionStringPM);

            if (false) //(ClaimDetails.IsClaimOnHold) // Code commented by Dev66(Revisit condition).
            {
                _drParentClaimHoldNote = InsurancePayment.GetBillingHoldNote(oSplitClaimDetails.TransactionMasterID, oSplitClaimDetails.TransactionID);
                _dtHoldInfo = ogloSplitClaim.GetSubClaims(oSplitClaimDetails);
                if (_dtHoldInfo != null)
                {
                    if (_dtHoldInfo.Rows.Count > 1)
                    {
                        frmSplitClaimHoldSelection ofrmSplitClaimHoldSelection = new frmSplitClaimHoldSelection(_dtHoldInfo, _drParentClaimHoldNote, this.ClaimNo, this.SubclaimNo);
                        ofrmSplitClaimHoldSelection.ShowDialog();
                        _dtHoldInfo = ofrmSplitClaimHoldSelection.SplittedClaims;
                    }
                }
            }
            ogloSplitClaim.Dispose();
            return _dtHoldInfo;
        }




        #endregion
    }

    #region " Supporting Class to Assign PaymentInsurance, NextAction & Split which is used to Iterate Claim. "

    public class ERAPayment
    {
        private EOBPayment.Common.PaymentInsurance _PaymentInsurance = null;
        private EOBPayment.Common.PaymentInsuranceLineNextActions _PaymentInsuranceLineNextActions = null;
        private SplitClaimDetails _SplitClaimDetails = null;

        public EOBPayment.Common.PaymentInsurance PaymentInsurance
        {
            get { return _PaymentInsurance; }
            set
            {
                _PaymentInsurance = value;
            }
        }

        public EOBPayment.Common.PaymentInsuranceLineNextActions PaymentInsuranceLineNextActions
        {
            get { return _PaymentInsuranceLineNextActions; }
            set
            {
                _PaymentInsuranceLineNextActions = value;
            }
        }

        public SplitClaimDetails SplitClaimDetails
        {
            get { return _SplitClaimDetails; }
            set
            {
                _SplitClaimDetails = value;
            }
        }

    }

    #endregion

}
