using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using gloAccountsV2;
using gloAuditTrail;
using gloBilling.EOBPayment.Common;
using gloBilling.Payment;
using gloSettings;
using System.Data.SqlClient;
using gloBilling.Collections; 

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

    public class clsERAPostingV2
    {

        #region " Constructor & Destructor "

        private bool disposed = false;

        public clsERAPostingV2()
        {
            _ClinicID = gloGlobal.gloPMGlobal.ClinicID;
            _DataBaseConnectionString = gloGlobal.gloPMGlobal.DatabaseConnectionString;
            _UserID = gloGlobal.gloPMGlobal.UserID;
            _MessageBoxCaption = gloGlobal.gloPMGlobal.MessageBoxCaption; 

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

        ~clsERAPostingV2()
        {
            Dispose(false);
        }

        #endregion

        #region " Variable Declarations "

        
        public string _DataBaseConnectionString;
        private Int64 _ClinicID = 1;
        private Int64 _UserID;
        private string _MessageBoxCaption;

        private gloDatabaseLayer.DBLayer oDB;
        private gloDatabaseLayer.DBParameters oDBPara;
        string _TempStr;
        private bool _IsPatientAccountFeature;
        private bool _IsStandardReasonCodeExists;
        

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
        Int64 _nOperationID = 0;
        #endregion

        #region " Properties for Payment Tray "
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
        private PaymentModeV2  _PaymentMode = PaymentModeV2.Check;//solving sales force case -GLO2011-0010771
        private string _CheckDate = string.Empty;
        private decimal _CheckAmount = 0;
        private decimal _PLBAmount = 0;

        private Int64 _MSTAccountID = 0;
        private string _NextAction = string.Empty;
        private string _NextActionContactID = string.Empty;
        private string _NextActionPatientID = string.Empty;
        private string _NextParty = string.Empty;

        private Int64 _BillingTransactionID = 0;
        private Int64 _BillingTransactionDetailID = 0;
        
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

        string sPayerID = "";
        private bool _bIsSkipZeroBillingClaimForERA = false;
        private bool _PayerSettingIsTransferClaim = false;

        private bool bIsSkipZeroBillingClaimForERA
        {
            get { return _bIsSkipZeroBillingClaimForERA; }
            set
            {
                _bIsSkipZeroBillingClaimForERA = value;
            }
        }

        private bool PayerSettingIsTransferClaim
        {
            get { return _PayerSettingIsTransferClaim; }
            set
            {
                _PayerSettingIsTransferClaim = value;
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
        private gloAccountsV2.PaymentModeV2 PaymentMode   //solving sales force case -GLO2011-0010771
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

        private Boolean isGCodeCPT = false;

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

        #region " Method"

        #region "  refresh  progress Method "

        private void RefreshProgress(ref ProgressBar oProgress, ref Label oLabel, String sProgressText)
        {
            oProgress.Increment(1);
            oLabel.Text = sProgressText;
            Application.DoEvents();
           
        }

        #endregion 

        #region " Posting Method"
        public bool PostERAFile_Temp(Int64 nBPRID, Int64 nOperationID, out string sMessage, out StopFlag oStopFlag, ref ProgressBar oProgress, ref Label oLabel)
        {
            int systemCode_index = 0;
            int ReasonCode_index = 0;
            Int32  ErrorLevel = 0;
            bool IsStopPost = false;
            bool bReturn = false;
            
            DataTable dtBPRClaims = null;
            DataTable dtCheckDetails = null;
            DataTable dtPayerDetails = null;
            DataTable dtClaimDetails = null;

            gloAccountPayment.dsPaymentTVP_V2 dsInsurancePayment_TVP = null; 
            dsInsurancePayment_TVP = new gloAccountPayment.dsPaymentTVP_V2();
            ArrayList nClaimsPosted = new ArrayList();
            Hashtable nContactIDList = new Hashtable(); 
           // System.Data.SqlClient.SqlTransaction _sqlTransaction = null;

            // Custom Error code & Custom Error Desc 
            sMessage = string.Empty;
            oStopFlag = StopFlag.NotProcessed; //"0"
            gloBilling ogloBilling = new gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, AppSettings.ConnectionStringEMR);
            Int64 nEOBID = 0;
            
            Int64 nEOBDetailID = 0;
            Int64 nDebitID = 0;
            try
            {

                #region " CheckValidation "


                // Validate check status In-Process or Not.
                if (ClsERAValidation.CheckValidation(nBPRID, out ErrorLevel, out sMessage))
                {
                    // Display message to the user.
                    if (ErrorLevel==1)
                     oStopFlag = StopFlag.CheckOpened;
                    else
                        oStopFlag = StopFlag.NoClaimProcessed;
                    return false;
                }
            
                #endregion

                #region " PreSaveValidation "

             
               // nRetValue = PreSaveValidation(nBPRID, out sMessage);

                //if (nRetValue > 0)
                //{
                //    // Display Message to the user.
                //    // Stop Payment for the Check.
                    
                //    oStopFlag = StopFlag.NoClaimProcessed;
                //    return false;
                //}

             
                #endregion

                #region " GetAllBPRClaims "

                // Fetch all claim against a BPR Value.
                dtBPRClaims = GetAllBPRClaims(nBPRID);

                // Get ClaimStatus from Datatable (dtBPRClaims)
                if (dtBPRClaims == null)
                {
                    sMessage = String.Format("Claims not found for the nBPRID: {0}", nBPRID);
                    return false;
                }


                #endregion

                #region " FillPaymentTray "
                FillPaymentTray();

                #endregion

                GetPayerSetupDetails(nBPRID);
                SetReasonCodes();

                bool bCommentFlag = false;
                Int64 nContactID = 0;
                Int64 nClaimStatus = 0;
                Int64 nCLPId = 0;
                bool bClaimLevelCASExists;
                string sClaim_CLP03 = "";
                string sClaim_CLP04 = "";
                Int64 nInsuranceId = 0;
                bool bCASOtherReasonCodeExists = false;
                bool bPayerCASWindowSetup = false;

                oProgress.Maximum = dtBPRClaims.Rows.Count + 2;
                oProgress.Minimum = 0;
                oProgress.Value = 0;
                oLabel.Text = "";
                oProgress.Visible = true;
                oLabel.Visible = true;
                
                #region " Master Data "

                this.CloseDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Today.ToString());
               
                #endregion

                int row_num = 0;
                
                #region " Fill Master Details "

                // Assign Check details into the local Properties/Variable 
                dtCheckDetails = GetCheckDetails(nBPRID);
                FillMasterDetails(dtCheckDetails);
                SetCreditsDetails(dsInsurancePayment_TVP);

                #endregion

                #region " Check Patient Account Feature Setting"
                gloPatient.gloAccount objAccount = new gloPatient.gloAccount(_DataBaseConnectionString);
                _IsPatientAccountFeature = objAccount.GetPatientAccountFeatureSetting();
                #endregion

                #region "Generate Unique Operation ID for ERA_ClaimMatchingDetails"
                this._nOperationID = nOperationID;                
                #endregion
                #region "Delete Claim Matching Data if exists"
                ClsERAValidation.DeleteClaimMatchDetailsonClose(0, nBPRID);
                #endregion
                foreach (DataRow dtClaimRow in dtBPRClaims.Rows)
                {
                    RefreshProgress(ref oProgress, ref oLabel, "Processing Claim " + dtClaimRow["sCLP01_ClaimSubmitterID"].ToString());

                    #region " For Each Claim "
                    IsStopPost = false;
                    bCommentFlag = false;
                    nContactID = 0;  
                    nResponsibilityNo = 0;
                    nClaimStatus = Convert.ToInt64(dtClaimRow["sCLP02_ClaimStatusCode"]);
                    nCLPId = Convert.ToInt64(dtClaimRow["nCLPID"]);
                    bClaimLevelCASExists = Convert.ToBoolean(dtClaimRow["CASExists"]);

                    sClaim_CLP03 = dtClaimRow["sCLP03_Amount"].ToString();
                    sClaim_CLP04 = dtClaimRow["sCLP04_Amount"].ToString();
                    nInsuranceId = 0;
                    nEOBID = Convert.ToInt64(dtClaimRow["nEOBID"]);
                    
                    this.ERAClaimNo = dtClaimRow["sCLP01_ClaimSubmitterID"].ToString();

                    #region " ClaimsValidations "

                    // Verify ERA Claim No is available in System and also check for the claim is voided.
                    if (!ClsERAValidation.ClaimsValidations(nBPRID, this.ERAClaimNo, nCLPId, this.sPayerID, _nOperationID, out nContactID, out nInsuranceId, out nResponsibilityNo, out IsStopPost))
                    {
                        // Logs are written using Stored Procedure.
                        // Stop Posting
                        IsStopPost = true;
                        continue;
                    }

               
                    #endregion

                    #region "Commented Validation"
                    //#region " Determine Payer "

               
                    //if (!ClsERAValidation.DeterminePayer(nBPRID, this.ERAClaimNo, nCLPId, out nContactID, out nInsuranceId,out nResponsibilityNo))  // Returns nContactID/PayerID //1044623:HardCode
                    //{
                    //    // Logs are written using Stored Procedure.
                    //    // Stop Posting if nContactID is 0.
 
                    //    IsStopPost = true;
                    //    continue;
                    //}

                    //#endregion

                    //#region " Reasons To Stop Payment "

                    //if (ClsERAValidation.ReasonsToStopPaymentClaim(nBPRID, nCLPId, this.ERAClaimNo, nContactID))
                    //{
                    //    // Logs are written using Stored Procedure.
                    //    // Stop Posting if nContactID is 0.        
                    //    IsStopPost = true;
                    //    continue;
                    //}

           
                    //#endregion

                    //#region " Charged Matched Against Claim "


              

                    //if (!ClsERAValidation.IsChargeMatchedAgaintClaim(nBPRID, this.ERAClaimNo, nContactID, nCLPId, nResponsibilityNo))
                    //{
                    //    // Logs are written using Stored Procedure.
                    //    // Stop Posting.
                    //    IsStopPost = true;
                    //    //////continue;
                    //}


                    //#endregion
                   #endregion


                    #region " Determine Next Action & Party "


                    Hashtable ohtTab = null;
                    if (ClsERAValidation.DetermineNextActionParty(nBPRID, nCLPId, this.ERAClaimNo, nClaimStatus, nContactID, nInsuranceId, out ohtTab,this.sPayerID))
                    {
                        // Logs are written using Stored Procedure.
                        // Stop Posting

                        
                        IsStopPost = true;
                        //////continue;

                    }

                    if (ohtTab != null) // condition added if multiple next action and party exists then hashtable will be rendered null.
                    {
                        this.NextAction = ohtTab["NextAction"].ToString();
                        this.NextParty = ohtTab["NextParty"].ToString();
                        this.NextActionContactID = ohtTab["NextContactId"].ToString();
                        this.NextActionPatientID = ohtTab["nNextActionPatientInsID"].ToString();
                        this.PayerSettingIsTransferClaim = Convert.ToBoolean(ohtTab["bIsTranfer"].ToString());   
                    }
                    else
                    {                      
                        IsStopPost = true;
                    }
                    ohtTab = null;

            
                    #endregion

                    #region " Get Check Details & Fill Payment Tray Details "

                    #region " Get Check Details "
                    if (!nContactIDList.Contains(nContactID))
                    {
                        dtPayerDetails = GetPayerDetails(nContactID);

                        if (dtPayerDetails == null)
                        {
                            // Check details not found.
                            sMessage = String.Format("Check details not found for the nBPRID: {0}", nBPRID);
                            nContactIDList.Add(nContactID, false);
                            continue;
                        }
                        // Code to check/handle Insurance Plans without Company.
                        else if (dtPayerDetails != null)
                        {
                            if (dtPayerDetails.Rows.Count > 0)
                            {



                                DataRow dr = dtPayerDetails.Rows[0];

                                if (dr["sPayerID"] != null)
                                    if (dr["sPayerID"].ToString() == "0")
                                    {
                                        ClsERALogs.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "Payer is not associated with Insurance Company", LogStage.Claim);
                                        ClsERALogs.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);
                                        //dr = null;
                                        nContactIDList.Add(nContactID, false);
                                        IsStopPost = true;
                                        continue;
                                    }
                                    else
                                    {
                                        dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nPayerID"] = Convert.ToInt64(dr["sPayerID"]); ;
                                        dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["sPayerName"] = dr["sPayerName"].ToString();
                                        SelectedInsuranceCompanyID = Convert.ToInt64(dr["sPayerID"]);
                                        this.MSTAccountID = Convert.ToInt64(dr["sPayerID"]);
                                        nContactIDList.Add(nContactID, true);
                                    }
                                dr = null;
                            }

                        }



                    }
                    else
                    {
                        bool _result = Convert.ToBoolean(nContactIDList[nContactID]);
                        if (!_result)
                        {
                            ClsERALogs.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "Payer is not associated with Insurance Company", LogStage.Claim);
                            ClsERALogs.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);
                            //dr = null;
                            IsStopPost = true;
                           continue;
                        }

                    }
                    
                    #endregion






                    #endregion

                    if (IsPaymentMade()) //(IsPaymentMade() && txtCheckAmount.Text.Trim() != "")
                    {
                        //if (this.ERAClaimNo!=null && this.ERAClaimNo.Contains("-") && this.ERAClaimNo.Trim().Length>0)
                        //    ogloBilling.UpdateRecordStatus(GetLastTransactionId(this.ERAClaimNo), Convert.ToInt64(this.ERAClaimNo.Substring(0,this.ERAClaimNo.IndexOf("-"))), true);
                        //else
                        //   ogloBilling.UpdateRecordStatus( GetLastTransactionId(this.ERAClaimNo), Convert.ToInt64(this.ERAClaimNo), true);

                        #region " GetCheckClaimDetails "
                        

                        dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["BPRID"] = nBPRID;

                        dsInsurancePayment_TVP.Tables["Credits"].AcceptChanges();

                        dtClaimDetails = GetCheckClaimDetails(nBPRID, nContactID, this.ERAClaimNo, nCLPId,nResponsibilityNo,this.sPayerID,_nOperationID);

                        if (dtClaimDetails == null)
                        {
                            // Claim Details not found.
                            sMessage = String.Format("Claim charges/details not found for the nBPRID: {0}  and Claim: {1}", nBPRID, this.ERAClaimNo);

                        }
                        else
                        {
                            for (int i = 0; i < dtClaimDetails.Rows.Count; i++)
                            {                                
                                if (String.Format("{0:0.00}", (dtClaimDetails.Rows[i]["nPayment"]).ToString()) == "0.00" || String.Format("{0:0.00}", (dtClaimDetails.Rows[i]["nPayment"]).ToString()) == "0")
                               {
                                   ClsERALogs.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, Convert.ToInt64(dtClaimDetails.Rows[i]["nSVCID"].ToString()), "z", sMessage, LogStage.ServiceLine);
                                   //ClsERASave.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, Convert.ToInt64((dtClaimDetails.Rows[i]["nSVCID"]).ToString()), "z", "", LogStage.ServiceLine);
                               }
                            }
                        }
                        
                        #endregion
                        // Check Claim Count for duplicate CPT and modifier   
                        if (dtClaimDetails.AsDataView().ToTable(true, "nBillingTransactionDetailID").Rows.Count != dtClaimDetails.Rows.Count)
                        {
                            ClsERALogs.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "Charge Not Matched", LogStage.Claim);
                            ClsERALogs.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);
                            IsStopPost = true;
                            continue;
                        }

                      
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
                                            ClsERALogs.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", sMessage, LogStage.Claim);
                                            ClsERALogs.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);
                                            IsStopPost = true;
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    // If more than one charge line exists along with claim level CAS then 
                                    // Update status log with "NP: Claim Level Adjustments Exist"
                                    sMessage = "Claim Level Adjustments Exist";
                                    ClsERALogs.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", sMessage, LogStage.Claim);
                                    ClsERALogs.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);
                                    IsStopPost = true;
                                    continue;
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
                                            ClsERALogs.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", sMessage, LogStage.Claim);
                                            ClsERALogs.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);
                                            IsStopPost = true;
                                            continue;
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
                                ClsERALogs.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "Charge Payment Not Balanced", LogStage.Claim);
                                ClsERALogs.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, nTempSVCID, "NP", "Charge Payment Not Balanced", LogStage.ServiceLine);
                                ClsERALogs.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, nTempSVCID, "NP", "", LogStage.ServiceLine);
                                IsStopPost = true;
                                continue;

                            }

                            //nZeroPaidBilled -- (Zero Paid) Other Reasons equal to Billed 
                            if (this.ZeroPaidBilled)
                            {
                                if (Convert.ToDouble(sChargePaid) == 0 && Convert.ToDouble(sChargeOtherReasonAmount) == Convert.ToDouble(sChargeBilled))
                                {
                                    ClsERALogs.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "ERA Payer Setup: (Zero Paid) Other Reasons equal to Billed Action is \"Stop Post\"", LogStage.Claim);
                                    ClsERALogs.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);
                                    IsStopPost = true;
                                    continue;
                                }
                            }

                            //nZeroPaidNotBilled -- (Zero Paid) Other Reasons not equal to Billed 
                            if (this.ZeroPaidNotBilled)
                            {
                                if (Convert.ToDouble(sChargePaid) == 0 && Convert.ToDouble(sChargeOtherReasonAmount) != Convert.ToDouble(sChargeBilled))
                                {
                                    ClsERALogs.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "ERA Payer Setup: (Zero Paid) Other Reasons not equal to Billed Action is \"Stop Post\"", LogStage.Claim);
                                    ClsERALogs.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);
                                    IsStopPost = true;
                                    continue;
                                }
                            }

                            //nPaidNotZero -- Paid) Other Reasons not equal to 0.00 
                            if (this.PaidNotZero)
                            {
                                if (Convert.ToDouble(sChargePaid) > 0 && Convert.ToDouble(sChargeOtherReasonAmount) != 0)
                                {
                                    ClsERALogs.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "ERA Payer Setup: (Paid) Other Reasons not equal to 0.00 Action is \"Stop Post\"", LogStage.Claim);
                                    ClsERALogs.SVCExceptionLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "NP", "", LogStage.ServiceLine);
                                    IsStopPost = true;
                                    continue;
                                }
                            }

                         }

                        #endregion 
                        bool bSecondaryAdjudication = false;
                        bool _announceBillToSelf = true;
                        DataTable _dt = null;

                        if (bIsSkipZeroBillingClaimForERA && PayerSettingIsTransferClaim)
                        {

                            foreach (DataRow dRow in dtClaimDetails.Rows)   // Iterate Claim Charges
                            {
                                bSecondaryAdjudication = false;
                                bSecondaryAdjudication = Convert.ToBoolean(dRow["PostSecondaryAdjustments"]);
                                this.BillingTransactionID = Convert.ToInt64(dRow["nBillingTransactionID"]);
                                this.TrackTrnID = Convert.ToInt64(dRow["nTrackTrnId"]);
                                this.PreTotalInsPaid = dRow["TotalInsPaid"].ToString();
                                this.PreTotalPatAdjustment = dRow["TotalPatAdjustment"].ToString();
                                this.PreTotalPatPaid = dRow["TotalPatPaid"].ToString();
                                this.PreTotalWithhold = dRow["TotalWithhold"].ToString();
                                this.PreTotalWriteOff = dRow["TotalWriteOff"].ToString();
                                ResetClaimDetails();

                                // Fill claim details into the local Properties/variable for processing a claim.
                                FillClaimDetails(dRow);
                                if (_dt == null)
                                {
                                    _dt = gloStripControl.PatientStripControl.GetInsuranceParties(this.BillingTransactionID, this.TrackTrnID);
                                }
                                if (CalculateNewBalancePatientPaidExclude(bSecondaryAdjudication) && _announceBillToSelf)
                                {
                                    _announceBillToSelf = true;
                                }
                                else
                                {
                                    _announceBillToSelf = false;
                                }

                            }
                            if (_announceBillToSelf && _dt != null)
                            {

                                this.NextAction = "B-Bill";
                                this.NextParty = Convert.ToString(_dt.Rows.Count) + "-Self";
                                this.NextActionContactID = "0";
                                this.NextActionPatientID = "0";
                                ClsERAValidation.UpdateClaimStatus(nBPRID, nCLPId);
                            }
                        }
                        foreach (DataRow dRow in dtClaimDetails.Rows)   // Iterate Claim Charges
                        {
                            #region " For Each Claim Charge "
                            bSecondaryAdjudication = false;
                            bSecondaryAdjudication = Convert.ToBoolean(dRow["PostSecondaryAdjustments"]);
                            this.PreTotalInsPaid = dRow["TotalInsPaid"].ToString();
                            this.PreTotalPatAdjustment = dRow["TotalPatAdjustment"].ToString();
                            this.PreTotalPatPaid = dRow["TotalPatPaid"].ToString();
                            this.PreTotalWithhold = dRow["TotalWithhold"].ToString();
                            this.PreTotalWriteOff = dRow["TotalWriteOff"].ToString();
                            this.isGCodeCPT = Convert.ToBoolean(dRow["isGCode"]);
                            
                            nEOBDetailID = Convert.ToInt64(dRow["nEOBDetailID"]);
                            nDebitID = Convert.ToInt64(dRow["nDebitID"]);
                            // Reset Local Properties/Variable before processing a claims.
                            ResetClaimDetails();

                            // Fill claim details into the local Properties/variable for processing a claim.
                            FillClaimDetails(dRow);

                            PatientInsuranceID = nInsuranceId; //Convert.ToInt64(dRow["nInsuranceID"]);

                            #region " New code for OverPaid & Over Adjustment (Negative Payment) "

                            // Calculated new balance is -ve then stop post on the charge. (Point#11; Doc: ReasonToStopPost.doc)
                            if (CalculateNewBalance(bSecondaryAdjudication))
                            {
                                if (!this.isGCodeCPT)
                                {
                                    sMessage = String.Format("Charge Balance is Negative");

                                    if (!bCommentFlag)
                                    {
                                        ClsERALogs.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, 0, "n", sMessage, LogStage.Claim);
                                        bCommentFlag = true;
                                    }

                                    ClsERALogs.ClaimStatusLog(nBPRID, nCLPId, this.ERAClaimNo, this.SVCId, "n", sMessage, LogStage.ServiceLine);
                                }
                                else
                                {
                                    this.Allowed = "0.00";
                                    this.WriteOff = "0.00";
                                    this.Payment = "0.00";
                                    this.Copay = "0.00";
                                    this.Deductible = "0.00";
                                    this.CoInsurance = "0.00";
                                    this.WithHold = "0.00";
                                }
                              //  bIsStopPayment = false;
                            }
                            #endregion
                            // Similar to get the Service Line Type as Claim in Manual posting.
                            if (this.BillingTransactionID > 0) //(c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                            {
                                #region "Set EOB Debits and Next Action "

                                #region "Set EOB  "
                                dsInsurancePayment_TVP.Tables["EOB"].Rows.Add();
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nCreditID"] =EOBPaymentID;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nEOBID"] = nEOBID;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nEOBDetailID"] = nEOBDetailID;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nPatientID"] = this.PatientID; 

                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nPatientInsuranceID"] = PatientInsuranceID;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nContactID"] = nContactID;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nInsCompanyID"] = SelectedInsuranceCompanyID;// 
                                ;                                
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nBillingTransactionID"] = this.BillingTransactionID; ;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nBillingTransactionDetailID"] = this.BillingTransactionDetailID;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nTrackTransactionDetailID"] = this.TrackTrnDtlID ;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nTrackTransactionID"] = this.TrackTrnID;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nEntryType"] = PaymentEntryTypeV2.InsuracePayment.GetHashCode();
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["sEntryDesc"] = "InsPmt";
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["sCPTCode"] = this.CPT;


                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["dtCloseDate"] = DateTime.Now;


                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nPaymentTrayID"] = SelectedPaymentTrayID;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["sPaymentTrayDesc"] = SelectedPaymentTray;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nPaymentVoidTrayID"] = DBNull.Value;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["bIsPaymentVoid"] = false;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nVoidType"] = 0;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["dtPaymentVoidCloseDate"] = DBNull.Value;
                               

                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nEXTID"] = 0;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nCreditIDEXT"] = EOBPaymentID;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nEOBIDEXT"] = nEOBID;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nEOBDetailIDEXT"] = nEOBDetailID;
                                
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["dtCreatedDateTimeEXT"] = DateTime.Now;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["dtModifiedDateTimeEXT"] = DateTime.Now;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["dtPaymentVoidDateTimeEXT"] = DBNull.Value;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nUserIDEXT"] = AppSettings.UserID;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["sUserNameEXT"] = AppSettings.UserName;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["bIsERAPostEXT"] = true;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nSVCIDEXT"] = this.SVCId;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["sMachineNameEXT"] = Environment.MachineName;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["sSiteIDEXT"] = DBNull.Value;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["sVersionEXT"] = Environment.Version;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nClinicIDEXT"] = AppSettings.ClinicID;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nCLPIdEXT"] = nCLPId;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nBillingTransactionEXT"] = this.BillingTransactionID;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nBillingTransactionDetailEXT"] = this.BillingTransactionDetailID;
                                dsInsurancePayment_TVP.Tables["EOB"].AcceptChanges();

                              
                                    if (this.Charges.Trim() != "")
                                    { dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["dTotalChargeAmount"] = Convert.ToDecimal(this.TotalCharges); }

                                    if (this.Allowed.Trim() != "")
                                    { dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["dAllowedAmount"] = Convert.ToDecimal(this.Allowed); }

                                    if (this.WriteOff.Trim() != "")
                                    { dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["dWriteoffAmount"] = Convert.ToDecimal(this.WriteOff); }


                                    if (this.Payment.Trim() != "")
                                    { dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["dPaymentAmount"] = Convert.ToDecimal(this.Payment); }

                                    if (this.Copay.Trim() != "")
                                    { dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["dCopayAmount"] = Convert.ToDecimal(this.Copay); }

                                    if (this.Deductible.Trim() != "")
                                    { dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["dDeductibleAmount"] = Convert.ToDecimal(this.Deductible); }

                                    if (this.CoInsurance.Trim() != "")
                                    { dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["dCoinsurance"] = Convert.ToDecimal(this.CoInsurance); }

                                    if (this.WithHold.Trim() != "")
                                    { dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["dWithholdAmount"] = Convert.ToDecimal(this.WithHold); }
                              
                                


                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nPAccountID"] = dRow["nPAccountID"];
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nAccountPatientID"] = dRow["nAccountPatientID"];
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nGuarantorID"] = dRow["nGuarantorID"];

                                //if (bIsSkipZeroBillingClaimForERA && PayerSettingIsTransferClaim)
                                //{
                                //    if (_announceBillToSelf)
                                //    {
                                //        dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["sNextActionEXT"] = "B";
                                //        dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["sNextPartyEXT"] = "Self";
                                //        dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nNextPartyIDEXT"] = 0;
                                //    }
                                //} 

                                #endregion
                             
                                #region " Set Line Reason Codes "

                                //string _code = "";
                                int nReasoncodetype = AdjustmentType.None.GetHashCode();
                                Hashtable htTab = new Hashtable();
                                htTab.Add(23, "nWriteOff");
                                htTab.Add(24, "nCopay");
                                htTab.Add(25, "nDeductible");
                                htTab.Add(26, "nCoInsurance");
                                htTab.Add(27, "nWithhold");


                                for (int colIndex = COL_WRITEOFF; colIndex <= COL_WITHHOLD; colIndex++)
                                {
                                   // _code = "";
                                    if (dRow[htTab[(object)colIndex].ToString()].ToString().Trim() != "" && Convert.ToDecimal(dRow[htTab[(object)colIndex].ToString()]) != 0)
                                    {
                                        DataTable dtStandardReasonCode = null;
                                        switch (colIndex)
                                        {
                                            case 23:
                                                dtStandardReasonCode = GetStandardReasonCodes(nCLPId, this.SVCId, nResponsibilityNo, 6);
                                                nReasoncodetype = AdjustmentType.WriteOff.GetHashCode();
                                                break;
                                            case 24:
                                                dtStandardReasonCode = GetStandardReasonCodes(nCLPId, this.SVCId, nResponsibilityNo, 2);
                                                nReasoncodetype = AdjustmentType.Copay.GetHashCode();
                                                break;
                                            case 25:
                                                dtStandardReasonCode = GetStandardReasonCodes(nCLPId, this.SVCId, nResponsibilityNo, 3);
                                                nReasoncodetype = AdjustmentType.Deductable.GetHashCode();
                                                break;
                                            case 26:
                                                dtStandardReasonCode = GetStandardReasonCodes(nCLPId, this.SVCId, nResponsibilityNo, 1);
                                                nReasoncodetype = AdjustmentType.CoInsurance.GetHashCode();
                                                break;
                                            case 27:
                                                dtStandardReasonCode = GetStandardReasonCodes(nCLPId, this.SVCId, nResponsibilityNo, 5);
                                                nReasoncodetype = AdjustmentType.WithHold.GetHashCode();
                                                break;
                                            default:
                                                break;
                                        }


                                        if (dtStandardReasonCode != null)
                                        {
                                            if (dtStandardReasonCode.Rows.Count > 0)
                                            {
                                                foreach (DataRow dtRow in dtStandardReasonCode.Rows)
                                                {
                                                    if (dtRow["ReasonAmount"] != null)
                                                    {
                                                        if (dtRow["ReasonAmount"].ToString().Trim().Length > 0 && Convert.ToDecimal(dtRow["ReasonAmount"].ToString()) != 0)
                                                        {

                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Add();
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nID"] = 0;
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nClaimNo"] = this.ClaimNo;
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nEOBPaymentID"] = EOBPaymentID;
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nEOBID"] = nEOBID;
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nEOBPaymentDetailID"] = nEOBDetailID;
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nBillingTransactionID"] = this.BillingTransactionID;
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nBillingTransactionDetailID"] = this.BillingTransactionDetailID;
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["bIsVoid"] = 0;
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nTrackTrnLineNo"] = DBNull.Value;
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["sSubClaimNo"] = this.SubclaimNo;
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nTrackTrnID"] = this.TrackTrnID;
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nTrackTrnDtlID"] = this.TrackTrnDtlID;
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nCloseDate"] = this.CloseDate;
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nClinicID"] = AppSettings.ClinicID;
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nVoidType"] = 0;
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["bIsPaymentVoid"] = 0;
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nPaymentVoidCloseDate"] = DBNull.Value;
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nPaymentVoidTrayID"] = DBNull.Value;

                                                            //dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["sReasonCode"] = GetSelectedReasonCode(colIndex);
                                                            //dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["sReasonDescription"] = InsurancePayment.GetReasonDescription(_code);
                                                            //dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["dReasonAmount"] = Convert.ToDecimal(dRow[htTab[(object)colIndex].ToString()]);//CalculateReasonAmount(oPaymentInsuranceLineResonCode.ReasonCode);

                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["dReasonAmount"] = Convert.ToDecimal(dtRow["ReasonAmount"].ToString());
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["sReasonCode"] = dtRow["ReasonCode"].ToString();
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["sReasonDescription"] = (InsurancePayment.GetReasonDescription(dtRow["ReasonCode"].ToString()) == "") ? "No Description Available" : InsurancePayment.GetReasonDescription(dtRow["ReasonCode"].ToString());

                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nType"] = EOBCommentTypeV2.SystemReasonCode.GetHashCode();
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nSubType"] = colIndex;
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nReasonCodeType"] = nReasoncodetype;
                                                            systemCode_index = systemCode_index + 1;

                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].AcceptChanges();
                                                            _IsStandardReasonCodeExists = true;

                                                        }
                                                    }
                                                }
                                            }
                                            else if (isGCodeCPT && this.SVCId==0)
                                            {
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Add();
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nID"] = 0;
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nClaimNo"] = this.ClaimNo;
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nEOBPaymentID"] = EOBPaymentID;
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nEOBID"] = nEOBID;
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nEOBPaymentDetailID"] = nEOBDetailID;
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nBillingTransactionID"] = this.BillingTransactionID;
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nBillingTransactionDetailID"] = this.BillingTransactionDetailID;
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["bIsVoid"] = 0;
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nTrackTrnLineNo"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["sSubClaimNo"] = this.SubclaimNo;
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nTrackTrnID"] = this.TrackTrnID;
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nTrackTrnDtlID"] = this.TrackTrnDtlID;
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nCloseDate"] = this.CloseDate;
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nClinicID"] = AppSettings.ClinicID;
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nVoidType"] = 0;
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["bIsPaymentVoid"] = 0;
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nPaymentVoidCloseDate"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nPaymentVoidTrayID"] = DBNull.Value;

                                                //dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["sReasonCode"] = GetSelectedReasonCode(colIndex);
                                                //dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["sReasonDescription"] = InsurancePayment.GetReasonDescription(_code);
                                                //dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["dReasonAmount"] = Convert.ToDecimal(dRow[htTab[(object)colIndex].ToString()]);//CalculateReasonAmount(oPaymentInsuranceLineResonCode.ReasonCode);

                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["dReasonAmount"] = Convert.ToDecimal(dRow==null?"0.00":dRow["nWriteOff"].ToString());
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["sReasonCode"] = GetDefultReasonCodeByType(AdjustmentType.WriteOff.GetHashCode());//dtRow["ReasonCode"].ToString();;
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["sReasonDescription"] = (InsurancePayment.GetReasonDescription(GetDefultReasonCodeByType(AdjustmentType.WriteOff.GetHashCode())) == "") ? "No Description Available" : InsurancePayment.GetReasonDescription(GetDefultReasonCodeByType(AdjustmentType.WriteOff.GetHashCode()));

                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nType"] = EOBCommentTypeV2.SystemReasonCode.GetHashCode();
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nSubType"] = colIndex;
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nReasonCodeType"] = nReasoncodetype;
                                                systemCode_index = systemCode_index + 1;

                                                dsInsurancePayment_TVP.Tables["ReasonCode"].AcceptChanges();
                                                _IsStandardReasonCodeExists = true;

                                            }
                                        }
                                        
                                    }
                                    //else 
                                    //{
                                    //    _IsStandardReasonCodeExists = false;
                                    //}
                                }

                                htTab = null;

                                DataTable dtOther = GetOtherReasonCodes(this.SVCId,false,nResponsibilityNo);
                                
                                if (dtOther != null)
                                {
                                    if (dtOther.Rows.Count > 0 && bCASOtherReasonCodeExists == false)
                                    {
                                        foreach (DataRow dtRow in dtOther.Rows)
                                        {
                                            if (dtRow["ReasonAmount"] != null)
                                            {
                                                if (dtRow["ReasonAmount"].ToString().Trim().Length > 0 && Convert.ToDecimal(dtRow["ReasonAmount"].ToString()) != 0)
                                                {
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Add();
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nID"] = 0;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nClaimNo"] = this.ClaimNo;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nEOBPaymentID"] = EOBPaymentID;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nEOBID"] = nEOBID;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nEOBPaymentDetailID"] = nEOBDetailID;

                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nBillingTransactionID"] = this.BillingTransactionID;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nBillingTransactionDetailID"] = this.BillingTransactionDetailID;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["bIsVoid"] = 0;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nTrackTrnLineNo"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["sSubClaimNo"] = this.SubclaimNo;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nTrackTrnID"] = this.TrackTrnID;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nTrackTrnDtlID"] = this.TrackTrnDtlID;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nCloseDate"] = this.CloseDate;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nClinicID"] = AppSettings.ClinicID;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nVoidType"] = 0;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["bIsPaymentVoid"] = 0;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nPaymentVoidCloseDate"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nPaymentVoidTrayID"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["dReasonAmount"] = Convert.ToDecimal(dtRow["ReasonAmount"].ToString());
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["sReasonCode"] = dtRow["ReasonCode"].ToString();

                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["sReasonDescription"] = (InsurancePayment.GetReasonDescription(dtRow["ReasonCode"].ToString()) == "") ? "No Description Available" : InsurancePayment.GetReasonDescription(dtRow["ReasonCode"].ToString());
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nType"] = EOBCommentTypeV2.Reason.GetHashCode();
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nSubType"] = 0;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nReasonCodeType"] = AdjustmentType.OtherAdjustment.GetHashCode();
                                                    systemCode_index = systemCode_index + 1;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].AcceptChanges();
                                                    SaveResaonCode(dtRow["AdjustGroupCode"].ToString(), dtRow["AdjustReasonCode"].ToString());
                                                }
                                            }
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
                                                oRow["AdjustGroupCode"] = dtOther.Rows[iRow]["AdjustGroupCode"].ToString(); ;
                                                oRow["AdjustReasonCode"] = dtOther.Rows[iRow]["AdjustReasonCode"].ToString();
                                                
                                                dtOtherCodes.Rows.Add(oRow);
                                            }
                                        }
                                    }
                                    foreach (DataRow dtRow in dtOtherCodes.Rows)                                    
                                    {
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Add();
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nID"] = 0;
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nClaimNo"] = this.ClaimNo;
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nEOBPaymentID"] = EOBPaymentID;
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nEOBID"] = nEOBID;
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nEOBPaymentDetailID"] = nEOBDetailID;
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nBillingTransactionID"] = this.BillingTransactionID;
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nBillingTransactionDetailID"] = this.BillingTransactionDetailID;
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["bIsVoid"] = 0;
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nTrackTrnLineNo"] = DBNull.Value;
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["sSubClaimNo"] = this.SubclaimNo;
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nTrackTrnID"] = this.TrackTrnID;
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nTrackTrnDtlID"] = this.TrackTrnDtlID;
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nCloseDate"] = this.CloseDate;
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nClinicID"] = AppSettings.ClinicID;
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nVoidType"] = 0;
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["bIsPaymentVoid"] = 0;
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nPaymentVoidCloseDate"] = DBNull.Value;
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nPaymentVoidTrayID"] = DBNull.Value;
                                        if (dtRow["ReasonAmount"] != null)
                                        {
                                            if (dtRow["ReasonAmount"].ToString().Trim().Length > 0)
                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["dReasonAmount"] = Convert.ToDecimal(dtRow["ReasonAmount"].ToString());

                                        }
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["sReasonCode"] = dtRow["ReasonCode"].ToString();
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["sReasonDescription"] = (InsurancePayment.GetReasonDescription(dtRow["ReasonCode"].ToString()) == "") ? "No Description Available" : InsurancePayment.GetReasonDescription(dtRow["ReasonCode"].ToString());
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nType"] = EOBCommentTypeV2.Reason.GetHashCode();
                                       dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nSubType"] = 0;
                                       dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[systemCode_index]["nReasonCodeType"] = AdjustmentType.OtherAdjustment.GetHashCode();
                                        systemCode_index = systemCode_index + 1;
                                        SaveResaonCode(dtRow["AdjustGroupCode"].ToString(), dtRow["AdjustReasonCode"].ToString());
                                        dsInsurancePayment_TVP.Tables["ReasonCode"].AcceptChanges();
                                    }

                                }
                                #endregion



                                #region " Set Line Remark Codes "

                                DataTable dtRemarkCodes = GetRemarkCodes(this.SVCId);

                                if (dtRemarkCodes != null)
                                {
                                    if (dtRemarkCodes.Rows.Count > 0)
                                    {
                                        foreach (DataRow dtRow in dtRemarkCodes.Rows)
                                        {
                                            if (dtRow["RemarkCodes"] != null)
                                            {
                                                if (dtRow["RemarkCodes"].ToString().Trim().Length > 0)
                                                { 
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Add();
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["nEOBRemarkID"] = 0;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["nClaimNo"] = this.ClaimNo;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["nEOBPaymentID"] = EOBPaymentID;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["nEOBID"] = nEOBID;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["nEOBPaymentDetailID"] = nEOBDetailID;

                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["nBillingTransactionID"] = this.BillingTransactionID;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["nBillingTransactionDetailID"] = this.BillingTransactionDetailID;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["bIsVoid"] = 0;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["nTrackTrnLineNo"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["sSubClaimNo"] = this.SubclaimNo;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["nTrackTrnID"] = this.TrackTrnID;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["nTrackTrnDtlID"] = this.TrackTrnDtlID;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["nCloseDate"] = this.CloseDate;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["nClinicID"] = AppSettings.ClinicID;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["nVoidType"] = 0;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["bIsPaymentVoid"] = 0;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["nPaymentVoidCloseDate"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["nPaymentVoidTrayID"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["sRemarkCode"] = dtRow["RemarkCodes"].ToString();
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["sRemarkDescription"] = (InsurancePayment.GetRemarkDescription(dtRow["RemarkCodes"].ToString()) == "") ? "No Description Available" : InsurancePayment.GetRemarkDescription(dtRow["RemarkCodes"].ToString());
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["nType"] = EOBCommentTypeV2.Reason.GetHashCode();
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["nSubType"] = 0;
                                                    if (dtOther.Rows.Count == 1 && _IsStandardReasonCodeExists == false)
                                                    {
                                                        if (Convert.ToString( dtOther.Rows[0]["ReasonCode"])!="")
                                                        {
                                                            dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[ReasonCode_index]["sReasonCode"] = Convert.ToString(dtOther.Rows[0]["ReasonCode"]);
                                                        }
                                                      
                                                    }
                                                    ReasonCode_index = ReasonCode_index + 1;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].AcceptChanges();
                                                    SaveRemarkCode(dtRow["RemarkCodes"].ToString());
                                                }
                                            }
                                        }
                                    }
                                }

                               
                                #endregion


                                #region "Debit Service Line "



                               
                                dsInsurancePayment_TVP.Tables["Debits"].Rows.Add();

                                         
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nCreditID"] = EOBPaymentID;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nCredit_RefID"] = EOBPaymentID;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nEOBID"] = nEOBID;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nEOBDetailID"] = nEOBDetailID;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nDebitID"] = nDebitID;

                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nBillingTransactionID"] = this.BillingTransactionID;// (Int64)dRow["nBillingTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nBillingTransactionDetailID"] = this.BillingTransactionDetailID; // (Int64)dRow["nBillingTransactionDetailID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nTrackTransactionID"] = this.TrackTrnID;// (Int64)dRow["nTrackBLTransactionID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nTrackTransactionDetailID"] = this.TrackTrnDtlID;// (Int64)dRow["nTrackBLTransactionDetailID"]; //Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["sCPTCode"] = this.CPT; // Convert.ToString(dRow["sCPTCode"]);
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["sCPTDesc"] = this.CPTDescription; // Convert.ToString(dRow["sCPTDescription"]);
                            
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nEntryType"] = PaymentEntryTypeV2.InsuracePayment.GetHashCode();
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["sEntryDesc"] = "InsPmt";

                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nPatientInsuranceID"] = PatientInsuranceID;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nInsCompanyID"] = SelectedInsuranceCompanyID;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nContactID"] = nContactID;

                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nPatientID"] = this.PatientID;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nPaymentTrayID"] = SelectedPaymentTrayID;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["sPaymentTrayDesc"] = SelectedPaymentTray;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nUserID"] = AppSettings.UserID;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["sUserName"] = AppSettings.UserName;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nClinicID"] = AppSettings.ClinicID;

                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nPaymentVoidTrayID"] = DBNull.Value;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["bIsPaymentVoid"] = false;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nVoidType"] = 0;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["dtPaymentVoidDateTime"] = DBNull.Value;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["dtClaimVoidCloseDate"] = DBNull.Value;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["dtClaimVoidDateTime"] = DBNull.Value;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["bIsERAPost"] = 1;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["dtCreatedDateTime"] = DateTime.Now;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["dtModifiedDateTime"] = DateTime.Now;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["sMachineName"] = Environment.MachineName;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["sVersion"] = DBNull.Value;
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["sSiteID"] = DBNull.Value;

                                if (this.Payment != null && this.Payment.Trim() != "")
                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["dPaymentAmount"] = Convert.ToDecimal(this.Payment);




                                if (this.WriteOff.Trim() != "" && dRow["nWriteOff"] != null && (bSecondaryAdjudication || nResponsibilityNo == 1))
                                {

                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["dWriteoffAmount"] = Convert.ToDecimal(this.WriteOff);
                                    

                                }

                                if (this.WithHold.Trim() != "" && dRow["nWithhold"] != null && (bSecondaryAdjudication || nResponsibilityNo == 1))  //bSecondaryAdjudication))
                                {
                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["dWithholdAmount"] = Convert.ToDecimal(this.WithHold);
                                    
                                }

                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nPAccountID"] = dRow["nPAccountID"];
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nAccountPatientID"] = dRow["nAccountPatientID"];
                                dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nGuarantorID"] = dRow["nGuarantorID"];


                                #endregion

                                #region "Set Next Action "
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Add();

                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["nID"] = 0;
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["nClaimNo"] = this.ClaimNo;
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["nEOBPaymentID"] = EOBPaymentID;
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["nEOBID"] = nEOBID;
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["nEOBPaymentDetailID"] = nEOBDetailID;
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["nBillingTransactionID"] = this.BillingTransactionID;
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["nBillingTransactionDetailID"] = this.BillingTransactionDetailID;

                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["nUserID"] = AppSettings.UserID;
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["dtDate"] = DateTime.Now;
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["sUserName"] = AppSettings.UserName;
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["sSubClaimNo"] = this.SubclaimNo;
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["nTrackMstTrnID"] = this.TrackTrnID;
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["nTrackMstTrnDetailID"] = this.TrackTrnDtlID;
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["nCloseDate"] = CloseDate;
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["nNextActionPatientInsID"] =  Convert.ToInt64(this.NextActionPatientID);

                                String _party = this.NextParty; //Convert.ToString(c1SinglePayment.GetData(i, COL_PARTY)).Trim();
                                String _partyCode = _party.Substring(0, _party.IndexOf('-'));
                                String _partyDesc = _party.Substring(_party.IndexOf('-') + 1, (_party.Length - _party.IndexOf('-')) - 1).Trim();

                                String _nextaction = this.NextAction;
                                String _nextactionCode = _nextaction.Substring(0, _nextaction.IndexOf('-'));
                                String _nextactionDesc = _nextaction.Substring(_nextaction.IndexOf('-') + 1, (_nextaction.Length - _nextaction.IndexOf('-')) - 1).Trim();

                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["nNextActionPatientInsID"] = Convert.ToInt64(this.NextActionPatientID);
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["nNextActionPatientInsName"] = _partyDesc;
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["sNextActionCode"] = _nextactionCode;
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["sNextActionDescription"] = _nextactionDesc;
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["nNextActionPartyNumber"] = Convert.ToInt32(_partyCode.Trim());
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["nNextActionContactID"] = Convert.ToInt64(this.NextActionContactID);
                                
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["dNextActionAmount"] = 0;
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["nClinicID"] = AppSettings.ClinicID;
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["bIsVoid"] = 0;

                                if (this.NextActionPatientID == "0") //.self resp.
                                { dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["nNextPartyType"] = PayerTypeV2.Self.GetHashCode(); }
                                else
                                { dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[row_num]["nNextPartyType"] = PayerTypeV2.Insurance.GetHashCode(); }

                               
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["sNextActionEXT"] = _nextactionCode;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["sNextPartyEXT"] = _partyDesc;
                                dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nNextPartyIDEXT"] = Convert.ToInt64(this.NextActionContactID);
                                  
                                dsInsurancePayment_TVP.Tables["Debits"].AcceptChanges();
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].AcceptChanges();
                                dsInsurancePayment_TVP.Tables["EOB"].AcceptChanges();
                                row_num++;
                                           
                                      

                                #endregion

                                #endregion "Set EOB Debits and Next Action "
                            }
                            _IsStandardReasonCodeExists = false;
                            #endregion
                        }

                        if (IsStopPost == false && nClaimsPosted.Count==0)
                            nClaimsPosted.Add(dsInsurancePayment_TVP);

                    }
                    #endregion

                }

                #region " Save EOB Payment & Next action "

              //  bool bFlag = false;
             //   bool isEOBSaved = false;

                if (nClaimsPosted.Count>0 )
                {
                    //using (System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString))
                    //{

                    //    _sqlConnection.Open();
                    //    _sqlTransaction = _sqlConnection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

                        gloAccountsV2.gloInsurancePaymentV2 ogloInsurancePaymentV2 = new gloInsurancePaymentV2();
                        if (ogloInsurancePaymentV2.ERA_SaveCredit(dsInsurancePayment_TVP))
                        {
                           // isEOBSaved = true;
                            oStopFlag = StopFlag.Passed;
                            bReturn = true;
                        }
                        else
                        {   
                            oStopFlag = StopFlag.NotProcessed;
                            sMessage = "Unable to save payment. Transaction Rollback.";
                        }

                    //    _sqlConnection.Close();
                    //}
                }
                else
                {
                    oStopFlag = StopFlag.NoClaimProcessed;
                    sMessage = "ERA was unable to post payment.";
                }
                #endregion

             
                
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
                if (dtBPRClaims != null) { dtBPRClaims.Dispose(); }               
                if (dtCheckDetails != null) { dtCheckDetails.Dispose(); }
                if (dtPayerDetails != null) { dtPayerDetails.Dispose(); }
                if (dtClaimDetails != null) { dtClaimDetails.Dispose(); }
                //if (_sqlTransaction != null) { _sqlTransaction.Dispose(); }
                if (ogloBilling != null) { ogloBilling.Dispose(); ogloBilling = null; }
                if(dsInsurancePayment_TVP!=null ){dsInsurancePayment_TVP.Dispose();dsInsurancePayment_TVP=null ; }
            }
            return bReturn;
        }

        public bool PostERAFile_New(Int64 nBPRID, Int64 nTrayID, string sCloseDate, Int64 nSelectedPaymentTrayID, out string sMessage, out StopFlag oStopFlag, ref ProgressBar oProgress, ref Label oLabel,ref SSRSApplication.frmSSRSViewer frmSSRS)
        {

            bool bReturn = false;

            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(AppSettings.ConnectionStringPM);
            SqlConnection _sqlConnection = null;
            SqlCommand _sqlCommand  = null;
            try
            {
                _EOBPaymentID = 0;
                oStopFlag = StopFlag.NotProcessed;
                sMessage = "";
                oProgress.Value = 10;
                oProgress.Maximum = 80;
                oProgress.Minimum = 0;
                oProgress.Width = 410;
                oProgress.Visible = true;
                oProgress.Increment(0);
                oProgress.Refresh();
                frmSSRS.Refresh();
                oLabel.Text = " ";
                //oLabel.Visible = true;
                //oLabel.BringToFront();
                Int32 _retVal = 0;
                String _Error = "";
                
                oProgress.Refresh();
              // oDB.Connect(false);

                oParameters.Clear();
                oParameters.Add("@nBPRID", nBPRID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@dtCloseDate", Convert.ToDateTime(sCloseDate), ParameterDirection.Input, SqlDbType.DateTime);
                oParameters.Add("@nPaymentTrayID", nSelectedPaymentTrayID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@Error", _Error, ParameterDirection.Output, SqlDbType.VarChar,50000);
                oParameters.Add("@nERACreditID", 0, ParameterDirection.Output, SqlDbType.BigInt);
                
                //frmSSRS.Refresh();
                
                oProgress.PerformStep();
                oProgress.Refresh();

                _sqlConnection = new SqlConnection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                _sqlConnection.Open();
                _sqlCommand = oDB.GetCmdParameters(oParameters);
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.Connection = _sqlConnection;
                _sqlCommand.CommandText = "ERA_PostPayment";
                _sqlCommand.CommandTimeout = 0;
                
                _retVal = _sqlCommand.ExecuteNonQuery();

                //..If ERA_PostPayment has exception/errror "@Error" flag will be set with error description
                //..and "@nERACreditID" will be set to zero
                //On ERROR we will discontinue further operation and Exit the method
                //On SUCCESS we will move to Split Claim "BL_SplitTransactionClaim_NewERA"
                
                if (Convert.ToString(_sqlCommand.Parameters["@Error"].Value).Trim() != "")  // Display comments to the user.
                { _Error = Convert.ToString(_sqlCommand.Parameters["@Error"].Value).Trim(); }

                if (Convert.ToString(_sqlCommand.Parameters["@nERACreditID"].Value).Trim() != "")  // Display comments to the user.
                { _EOBPaymentID = Convert.ToInt64(_sqlCommand.Parameters["@nERACreditID"].Value); }

                //On successful ERA Payment Post
                if (_EOBPaymentID > 0)
                {
                    oProgress.Increment(20);
                    oProgress.Refresh();
                    _Error = "";

                    oParameters.Clear();
                    oParameters.Add("@nBPRID", nBPRID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nERACreditID", _EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@dtCloseDate", Convert.ToDateTime(sCloseDate), ParameterDirection.Input, SqlDbType.DateTime);
                    oParameters.Add("@Message", _Error, ParameterDirection.Output, SqlDbType.VarChar, 50000);

                    oDB.Connect(false);
                    Object _splitOutPut = null;
                    oDB.Execute("BL_SplitTransactionClaim_NewERA", oParameters, out _splitOutPut);

                    //..On success BL_SplitTransactionClaim_NewERA will return blank _splitOutPut else will have error message
                    if (_splitOutPut != null && Convert.ToString(_splitOutPut).Trim() != "")  // Display comments to the user.
                    { _Error = Convert.ToString(_splitOutPut).Trim(); }

                    oDB.Disconnect();

                    oProgress.Increment(20);
                    oProgress.Refresh();

                    if (_Error == "")
                    {
                        bReturn = true;

                        try
                        {
                            oSettings.AddSetting("PAYMENT_LASTCLOSEDATE", Convert.ToDateTime(sCloseDate).ToString("MM/dd/yyyy"), gloGlobal.gloPMGlobal.ClinicID, gloGlobal.gloPMGlobal.UserID, gloSettings.SettingFlag.User);
                            oSettings.AddSetting("PAYMENT_LASTCLOSETRAYID", nSelectedPaymentTrayID.ToString(), gloGlobal.gloPMGlobal.ClinicID, gloGlobal.gloPMGlobal.UserID, gloSettings.SettingFlag.User);

                            CL_FollowUpCode.SetAutoAccountFollowUp(_EOBPaymentID, gloGlobal.gloPMGlobal.UserID, gloGlobal.gloPMGlobal.UserName, Convert.ToDateTime(sCloseDate));
                            oProgress.Increment(20);
                            oProgress.Refresh();

                            CL_FollowUpCode.SetClaimFollowUp(_EOBPaymentID);
                            oProgress.Increment(20);
                            oProgress.Refresh();
                        }
                        catch (Exception ex)
                        {
                            //No action on error
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        }
                    }
                    else
                    { 
                        //If split has error reported, we will rollback payment posted and message the user
                        RollBackTransaction(_EOBPaymentID);
                        oStopFlag = StopFlag.Error;
                        sMessage = "Error while payment, Payment not done";
                        bReturn = false;
                        gloAuditTrail.gloAuditTrail.ExceptionLog("Error while ERA Post, Claim Split failed." + Environment.NewLine + " BPR : " + nBPRID + " " + Environment.NewLine + " " + _Error, false);

                    }
                }
                else //...else for if (_EOBPaymentID > 0)
                {
                    //On error in payment post do following
                    oStopFlag = StopFlag.Error;
                    sMessage = "Error while payment, Payment not done";
                    bReturn = false;
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Error while ERA Post, Payment posting failed." + Environment.NewLine + " BPR : " + nBPRID + " " + Environment.NewLine + " " + _Error, false);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                oStopFlag = StopFlag.Error;
                sMessage = "Error while payment, Payment not done";
                bReturn = false;           
                //RollBackTransaction(_EOBPaymentID);

                //if (_EOBPaymentID == 0)
                //    _sqlCommand.Transaction.Rollback();                
            }
            finally
            {

                if (oParameters != null) { oParameters.Dispose(); }
                if (oDB != null) {  oDB.Dispose(); }
                if (_sqlConnection != null) { _sqlConnection.Close(); _sqlConnection.Dispose(); }
                if (_sqlCommand != null)
                {
                    _sqlCommand.Parameters.Clear();
                    _sqlCommand.Dispose();
                    _sqlCommand = null;
                }

                if (oSettings != null) { oSettings.Dispose(); oSettings = null; }
                InsurancePayment.UnlockCheckClaims(nBPRID);

            }
            return bReturn;
        }


        public  void RollBackTransaction(Int64 nCreditID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            gloDatabaseLayer.DBParameters oDBPara = new gloDatabaseLayer.DBParameters();

            try
            {
                if (nCreditID == 0)
                    return;
                oDB.Connect(false);
                //string _Query = "UPDATE BL_Transaction_MST with(readpast) SET bIsOpened = " + 0 + ", sMachineID = '' " +
                //        " WHERE nClaimNo IN (SELECT CONVERT(NUMERIC(18,0),sCLP01_ClaimSubmitterID) FROM ERA_CLP WHERE nBPRID = " + nBPRID.ToString() + ")" +
                //        " AND ISNULL(bIsOpened,0) = 1 AND nClinicID = " + AppSettings.ClinicID;
                oDBPara.Add("@nCreditID", nCreditID, ParameterDirection.Input, SqlDbType.BigInt);                
                oDB.Execute("ERA_ROLLBACKTRANSACTION", oDBPara);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
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

        }
        #region " Supporting/Common Methods & Function for ERA Temporary & Original Posting "

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
        public bool CalculateNewBalancePatientPaidExclude(bool bSecondaryAdjudication)
        {
            bool bFlag = false;
            Double nTempTotal = 0;
            if (this._TotalCharges.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) + Convert.ToDouble(this._TotalCharges); }
            if (this._Payment.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._Payment); }

            if (bSecondaryAdjudication || nResponsibilityNo == 1)
            {
                if (this._WriteOff.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._WriteOff); }
                if (this._WithHold.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._WithHold); }
            }


            if (this._PreTotalInsPaid.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._PreTotalInsPaid); }           
            if (this._PreTotalWithhold.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._PreTotalWithhold); }
            if (this._PreTotalWriteOff.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._PreTotalWriteOff); }            
            if (Math.Round(nTempTotal, 2) <= 0)
                bFlag = true;

            return bFlag;
        }

       public bool CalculateNewBalance(bool bSecondaryAdjudication)
        {
            bool bFlag = false;
            Double nTempTotal = 0;
            if (this._TotalCharges.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) + Convert.ToDouble(this._TotalCharges); }
            if (this._Payment.Trim() != "") { nTempTotal = Math.Round(nTempTotal, 2) - Convert.ToDouble(this._Payment); }

            if (bSecondaryAdjudication || nResponsibilityNo == 1)
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
                            ClsERALogs.ClaimStatusLog(nBPRID, nCLPID, this.ERAClaimNo, nSVCID, "d", "Deductible Applied", LogStage.ServiceLine);
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
                    && (!bClaimLevelCASExists && nResponsibilityNo > 1))
                {
                    // Write Posting Logs
                    //ERA_IN_PostingLogs @nBPRID, @nCLPId, @sERAClaimNo, @nSVCID, 'w', '', 3 
                    ClsERALogs.ClaimStatusLog(nBPRID, nCLPID, this.ERAClaimNo, nSVCID, "w", "Adjustments Not Posted", LogStage.ServiceLine);
                }

                if (sDeductable.Trim().Length > 0 && sDeductable != "0.00")
                {
                    ClsERALogs.ClaimStatusLog(nBPRID, nCLPID, this.ERAClaimNo, nSVCID, "d", "Deductible Applied", LogStage.ServiceLine);
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

       public bool IsPaymentMade()
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
            SelectedPaymentTray = InsurancePayment.GetPaymentTrayDescription(_defaultTrayID);
            SelectedPaymentTrayID = _defaultTrayID;
            SelectedPaymentTrayCode = string.Empty;

        }

        void FillPaymentTray(Int64 _defaultTrayID)
        {   
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

        private Int64 GetLastTransactionId(String nClaimNo)
        {
            Int64 _TransactionID = 0;
            string _sqlQuery = string.Empty;
            object _retVal = null;

            try
            {
                if (OpenConnection(false))
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

        #endregion

        #region " Set Credit Object"

        private void SetCreditsDetails(gloAccountPayment.dsPaymentTVP_V2 dsInsurancePayment_TVP)
        {
         
            gloAccountsV2.PaymentCollection.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new gloAccountsV2.PaymentCollection.gloEOBPaymentInsurance(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _PaymentNo = ogloEOBPaymentInsurance.GetPaymentPrefixNumber(_paymentPrefix).Trim();
            dsInsurancePayment_TVP.Tables["Credits"].Rows.Add();
            this.PaymentNumber = _PaymentNo;

            
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"] = _EOBPaymentID;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["sReceiptNo"] = CheckNumber;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["dReceiptAmount"] = CheckAmount;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["dtReceiptDate"] = Convert.ToDateTime(this.CheckDate); ;
            
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nPayerType"] = PayerTypeV2.Insurance.GetHashCode();         
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["sPaymentNo"] = _PaymentNo;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nPaymentMode"] = this.PaymentMode.GetHashCode();
            
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["dtCloseDate"] = Convert.ToDateTime(DateTime.Now);
            
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nPaymentTrayID"] = SelectedPaymentTrayID;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["sPaymentTrayDesc"] = SelectedPaymentTray;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nUserID"] = AppSettings.UserID;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["sUserName"] = AppSettings.UserName;

            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["bIsPaymentVoid"] = 0;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nVoidType"] = 0;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["dtPaymentVoidCloseDate"] = DBNull.Value;

            //if (_IsPatientAccountFeature)
            //{
            //    dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nPAccountID"] = this.nPAccountID;
            //    dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nAccountPatientID"] = this.nAccountPatientID;
            //    dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nGuarantorID"] = this.nGuarantorID;
            //}
            //else
            {
                dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nPAccountID"] = 0;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nAccountPatientID"] = 0;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nGuarantorID"] = 0;
            }

            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["sPaymentNote"] = "Payment Note";
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nPaymentVoidTrayID"] = DBNull.Value;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["sPaymentVoidTrayDesc"] = "Blank Tray";

            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nEntryType"] = PaymentEntryTypeV2.InsuracePayment.GetHashCode();

            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["Credits_EXTID"] = 0;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["ClinicID"] = AppSettings.ClinicID;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["PaymentVoidDateTime"] = DBNull.Value;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["CreatedDateTime"] = DateTime.Now;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["ModifiedDateTime"] = DateTime.Now;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["sVersion"] = Environment.Version.ToString();
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["sMachineName"] = Environment.MachineName;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["SiteID"] = "";
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["IsFinished"] = false;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["IsERAPost"] = true;
            
        }
        #endregion

        #region " Methods performs database operations & returns DataTable object. Assignment using DataRow "

        void SetReasonCodes()
        {
            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
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

    public    bool FillMasterDetails(DataTable dtERA)
        {
            bool blnFlag = false;

            if (dtERA != null)
            {
                if (dtERA.Rows.Count > 0)
                {
                    DataRow dRow = dtERA.Rows[0];

                    this.CheckNumber = dRow["sCheckNo"].ToString();
                    this.CheckAmount = Convert.ToDecimal(dRow["CheckAmount"]);
                    this.CheckDate = dRow["dCheckDate"].ToString();                    
                    this.PLBAmount = Convert.ToInt64(dRow["PLBAmount"]);
                    this.PaymentMode = (gloAccountsV2.PaymentModeV2)Convert.ToInt64(dRow["nPaymentMode"]);
                    this.EOBPaymentID = Convert.ToInt64(dRow["nCreditID"]);
                    this.sPayerID = dRow["sERAPayerID"].ToString();
                    if (dRow["bIsSkipZeroBillingClaimForERA"] != DBNull.Value)
                    {
                        this.bIsSkipZeroBillingClaimForERA = Convert.ToBoolean(dRow["bIsSkipZeroBillingClaimForERA"]);
                    }
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
                this.TrackTrnID = Convert.ToInt64(dRow["nTrackTrnId"]);
                this.TrackTrnDtlID = Convert.ToInt64(dRow["nTrackTrnDtlId"]);
                this.CPT = dRow["sCPTCode"].ToString();
                this.CPTDescription = dRow["sCPTDescription"].ToString();
                this.DOSFrom = dRow["dDOSFrom"].ToString();
                this.DOSTo = dRow["dDOSTo"].ToString();
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
      private string GetDefultReasonCodeByType(int CASType)
        {
            object Value = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                oDB.Connect(false);
                Value = oDB.ExecuteScalar_Query("SELECT sGroupCode +''+ sReasonCode FROM Insurance_DefaultReasonCodes WHERE nCASType=+" + CASType + " ");
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                Value = null;
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                Value = null;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
            }
            return Convert.ToString(Value);
        }

     public   DataTable GetCheckDetails(Int64 nBPRID)
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

       public  DataTable GetPayerDetails(Int64 nContactID)
        {
            DataTable dtFile = null;
            try
            {
                if (OpenConnection(false))
                {
                    _TempStr = "ERA_GetPayerDetails";  // Stored procedure to retrive ERA File details.
                    oDBPara = new gloDatabaseLayer.DBParameters();
                    oDBPara.Clear();                    
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

       public DataTable GetCheckClaimDetails(Int64 nBPRID, Int64 nContactID, string ERAClaimNo, Int64 nCLPId, Int64 nResponsibilityNo,string sPayerID , Int64 nOperaionId =0 )
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
                    oDBPara.Add("@sPayerId", sPayerID, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBPara.Add("@nOperationID", nOperaionId, ParameterDirection.Input, SqlDbType.BigInt);
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
       DataTable GetStandardReasonCodes(Int64 nCLPID, Int64 ID, Int64 nResponsibilityNo, int nCaseType)
       {
           DataTable dtFile = null;
           try
           {
               if (OpenConnection(false))
               {
                   _TempStr = "ERA_GetStandardReasonCodes";  // Stored procedure to retrive claim service line  reason codes which is set in Payer Setup.
                   oDBPara = new gloDatabaseLayer.DBParameters();
                   oDBPara.Clear();
                   oDBPara.Add("@nCLPID", nCLPID, ParameterDirection.Input, SqlDbType.BigInt);
                   oDBPara.Add("@ID", ID, ParameterDirection.Input, SqlDbType.BigInt);
                   oDBPara.Add("@nResponsibilityNo", nResponsibilityNo, ParameterDirection.Input, SqlDbType.BigInt);
                   oDBPara.Add("@nCasType", nCaseType, ParameterDirection.Input, SqlDbType.Int);
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
       public DataTable GetAllBPRClaims(Int64 nBPRID)
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

        DataTable GetOtherReasonCodes(Int64 ID, bool IsClaimLevel, Int64 nResponsibilityNo)
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


        DataTable GetRemarkCodes(Int64 ID)
        {
            DataTable dtFile = null;
            try
            {
                if (OpenConnection(false))
                {
                    _TempStr = "ERA_GetRemarkCodes";  
                    oDBPara = new gloDatabaseLayer.DBParameters();
                    oDBPara.Clear();
                    oDBPara.Add("@ID", ID, ParameterDirection.Input, SqlDbType.BigInt);
                    //oDBPara.Add("@IsClaimLevel", IsClaimLevel, ParameterDirection.Input, SqlDbType.Bit);
                    //oDBPara.Add("@nResponsibilityNo", nResponsibilityNo, ParameterDirection.Input, SqlDbType.BigInt);
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

       
        void SaveResaonCode(String sGroupCode, String sReasonCode)
        {
            try
            {
                if (OpenConnection(false))
                {
                    _TempStr = "ERA_INSERT_ReasonCode";  // Stored procedure to retrive claim service line other reason codes.
                    oDBPara = new gloDatabaseLayer.DBParameters();
                    oDBPara.Clear();
                    oDBPara.Add("@GroupCode", sGroupCode, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBPara.Add("@ReasonCode", sReasonCode, ParameterDirection.Input, SqlDbType.VarChar);
                    oDB.ExecuteScalar(_TempStr, oDBPara);
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
        }

        void SaveRemarkCode(String sRemarkCode)
        {
            try
            {
                if (OpenConnection(false))
                {
                    _TempStr = "ERA_INSERT_RemarkCode";  // Stored procedure to retrive claim service line other reason codes.
                    oDBPara = new gloDatabaseLayer.DBParameters();
                    oDBPara.Clear();
                    oDBPara.Add("@RemarkCode", sRemarkCode, ParameterDirection.Input, SqlDbType.VarChar);
                    oDB.ExecuteScalar(_TempStr, oDBPara);
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

        #endregion

    }

    
}
