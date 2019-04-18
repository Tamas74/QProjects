using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloBilling.Common;
using gloBilling;
using gloPatient;
using gloStripControl;
using gloBilling.Collections;
using gloBilling.Statement;
using System.Collections;


namespace gloAccountsV2
{
    public partial class frmPatientPaymentV2 :gloGlobal.Common.TriarqFormWithFocusListner// Form
    {

        #region " Variable Declarations "
        Label label;

        //gloStripControl.gloPatientStrip_FA oPatientControl = null;
        private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        Int64 _PatientID = 0;
       // Int64 _PAccountID = 0;
        Int64 _ClinicID = 1;
        Int64 _UserId = 0;
        string _UserName = "";
        Int64 _EOBPaymentID = 0; // it is used to hold master payement id for multiple claim payment
        private PaymentModeV2 _SelectedEOBPaymentMode = PaymentModeV2.None;
        string _messageboxcaption = string.Empty;
        bool _IsFormLoading = false;
        bool _IsPaymentGridLoading = false;
        bool _IsUseReserveEntry = false;
        bool _useReserves = false;
        private bool _IsPatientAccountFeature = false;
        C1FlexGrid c1ModifyPaymentTempGrid = null;
  //      private string _paymentPrefix = "GPM#";
        private bool _showZeroBalanceClaims = true;
        private bool _showbaddebtBalanceClaims = true;
        private bool _IsPaymentCorrectionMode = false;
        private Int64 _nTransactionPatientID = 0;
        private bool _IsAdjustmentMode = false;
        private bool _IsAllDatesValid = true;
        private bool _IsEditable = false;
        private bool _IsClaimDateMessageShown = true;
   //     private bool _IsC1SinglePaymentClicked = false;
        private Int64 _afterCloseDateChanged = 0;
        decimal _previousamt = 0;
        public int width;
        private Int64 nPAccountID = 0; //279818031508578695; //0;
        private Int64 nAccountPatientID = 0; //479094774563580567; //0;
        private Int64 nGuarantorID = 0; // 252513278405865382; //0;
        private bool isclosecheck = false;
        private Int64 nGlobalPeriodId = 0; // 252513278405865382; //0;
        gloBilling.gloAccountPayment.dsPaymentTVP_V2 dsInsurancePayment_TVP = null;
        gloAccountsV2.gloPatientPaymentV2 ogloPatientPayment = null;

        private Int64 ProviderID = 0;
        private Int64 nAssociateCollectionAgencyContactID = 0;
        long CollectionAgencyContactID = 0;
        Boolean IsPaymentMarkforcollectionAgency = false;
        private string ProviderName = string.Empty;
        private Int64 _SelectedTrayID = 0;
        private string _SelectedTrayName = string.Empty;
        private string _SelectedTrayCode = string.Empty;
        bool IsBusinessCenterEnable = false;
        ArrayList _Arr_ClaimFollowupRemove_TransactionMSTID = null;
        ArrayList _Arr_ClaimFollowupRemove_TrackTransactionID = null;
        public bool IsFromCopayReserveList = false;
        decimal dCheckAmount = 0;
        public gloGeneralItem.gloItems SeletedReserveItems = new gloGeneralItem.gloItems();
        public DateTime MaxCloseDate = DateTime.MinValue;
        public bool IsFromCleargagePosting = false;
        public Int64 ReturnPatientPaymentID = 0;
        public DataTable dtCleargagePaymentDetails = null;
        public Int64 nCG_OneTimePaymentID = 0;
        private ClearGage.SSO.SsoHelper ssoHelper;
        #region "Varaibles of Reserve DOS"

        private DateTime dtReserveforDOS = DateTime.MinValue;        

        #endregion

        #endregion " Variable Declarations "

        #region "Font Declaration"
        //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
        //declaring the font object as global and used at all places. 
        Font Font_CellStyle = gloGlobal.clsgloFont.gFont_SMALL_BOLD; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        Font Font_Template = gloGlobal.clsgloFont.gFont_BOLD; //new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        #endregion "Font Declaration"

        #region " Property Procedures "

        public bool ShowZeroBalanceClaims
        {
            get { return _showZeroBalanceClaims; }
            set { _showZeroBalanceClaims = value; }
        }
        public bool ShowBadDebtBalanceClaims
        {
            get { return _showbaddebtBalanceClaims; }
            set { _showbaddebtBalanceClaims = value; }
        }
        //GLO2011-0013738 : Patient Payment screen does not prompt to pick a tray
        // whenver payment tray load it will show last payment tray selected which is used for transaction.
        private Int64 SelectedPaymentTrayID
        {
            get { return _SelectedTrayID; }
            set
            {
                _SelectedTrayID = value;
                lblPaymentTray.Tag = _SelectedTrayID;
            }
        }

        private string SelectedPaymentTray
        {
            get { return _SelectedTrayName; }
            set
            {
                _SelectedTrayName = value;
                lblPaymentTray.Text = _SelectedTrayName;
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

        #region "Intuit Bill Pay"

        public bool IsIntuitBillPay { get; set; }

        public decimal IBPCheckamount { get; set; }

        public string IBPReferenceNumber { get; set; }

        public string IBPCardType { get; set; }

        public string IBPAuthNumber { get; set; }

        public Int64 IBPToken { get; set; }

        public Int64 IBPTaskID { get; set; }

        public Boolean IsTaskCompleted { get; set; }

        #endregion "Intuit Bill Pay"

        public Int64 PAccountID
        {
            get { return nPAccountID; }
            set
            {
                nPAccountID = value;
            }
        }

        public decimal CheckAmount
        {
            get { return dCheckAmount; }
            set
            {
                dCheckAmount = value;
            }
        }
        #endregion " Property Procedures "

        #region "Grid Constant"
        const int COL_GENERAL = 0;
        const int COL_PATIENTID = 1;
        const int COL_PATIENTNAME = 2;
        const int COL_CLAIMNO = 3;

        const int COL_BILLING_TRANSACTON_ID = 4;
        const int COL_BILLING_TRANSACTON_DETAILID = 5;
        const int COL_BILLING_TRANSACTON_LINENO = 6;

        const int COL_PAYMENT_NO = 7;
        const int COL_PAY_DATE = 8;

        const int COL_PAY_EOBPAYMENTID = 9;
        const int COL_PAY_EOBID = 10;
        const int COL_PAY_EOBDTLID = 11;
        const int COL_PAY_EOBPAYMENTDTLID = 12;

        const int COL_CLAIMDISPNO = 13;
        const int COL_CRESP_PARTY = 14;

        const int COL_DOS_FROM = 15;
        const int COL_DOS_TO = 16;
        const int COL_CPT_CODE = 17;
        const int COL_MODIFIER = 18;
        const int COL_CPT_DESCRIPTON = 19;

        const int COL_CHARGE = 20;
        const int COL_ALLOWED = 23;
        const int COL_UNIT = 21;
        const int COL_TOTALCHARGE = 22;
        const int COL_PREV_PAID = 24;
        const int COL_PREV_ADJ = 25;

        const int COL_CLM_BALANCE = 26;
        const int COL_PAT_DUE = 27;
        const int COL_BadDebt_DUE = 28;

        const int COL_CUR_PAYMENT = 29;

        const int COL_CUR_ADJ_TYPECODE = 30;
        const int COL_CUR_ADJ_TYPEDESCRIPTION = 31;
        const int COL_CUR_ADJ_AMOUNT = 32;

        const int COL_SERVICELINE_TYPE = 33;
        const int COL_ISOPENFORMODIFY = 34;
        const int COL_PAY_CLINICID = 35;
        const int COL_PAY_LINESTATUS = 36;

        const int COL_CELLRANGE_R1 = 37;
        const int COL_CELLRANGE_R2 = 38;

        const int COL_TRACK_TRN_ID = 39;
        const int COL_TRACK_TRN_DTL_ID = 40;
        const int COL_SUB_CLAIM_NO = 41;
        const int COL_PREV_PATIENTPAYMENT_AMT = 42;
        const int COL_CLAIMSUBCLAIM_NO = 43;
        const int COL_HOLD = 44;
        const int COL_TRANSACTION_DATE = 45;

        const int COL_PREV_PATIENTADJUSTMENT_AMT = 46;
        const int COL_NON_SERVICECODE = 47;
        const int COL_COUNT = 48;

        #endregion

        #region " Enumeratioin "

        private enum ColServiceLineType
        {
            None = 0, Claim = 1, ServiceLine = 2, Patient = 3
        }

        #endregion " Enumeratioin "

        #region " Constructor "
       
        public frmPatientPaymentV2(Int64 PatientID, bool IsOpenForModify, Int64 mPaymentClaimNo, Int64 mEOBPaymentID, Int64 mEOBID, Int64 mEOBPaymentDetailID, EOBPaymentSubType nEOBNewPaymentType)
        {
            InitializeComponent();

            c1ModifyPaymentTempGrid = new C1FlexGrid();

            #region " Retrive ClinicID from AppSettings "

            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }

            #endregion " Retrive ClinicID from AppSettings "

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageboxcaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageboxcaption = "gloPM"; ;
                }
            }
            else
            { _messageboxcaption = "gloPM"; ; }

            #endregion

            #region " Retrive UserID from appSettings "

            if (appSettings["UserID"] != null)
            {
                if (appSettings["UserID"] != "")
                {
                    _UserId = Convert.ToInt64(appSettings["UserID"]);
                }
            }
            else
            {
                _UserId = 0;
            }

            #endregion

            #region " Retrive UserName from appSettings "

            if (appSettings["UserName"] != null)
            {
                if (appSettings["UserName"] != "")
                {
                    _UserName = Convert.ToString(appSettings["UserName"]);
                }
            }
            else
            {
                _UserName = "";
            }

            #endregion

            _PatientID = PatientID;
            gloAccount objAccount = new gloAccount(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            _IsPatientAccountFeature = objAccount.GetPatientAccountFeatureSetting();
            //SLR: Free objAccount
            if (objAccount!=null)
            {
                objAccount.Dispose();
            }

            if (gloGlobal.gloPMGlobal.IsCleargageEnable)
            {
                ClearGage.clsCleargage oclsCleargage = new ClearGage.clsCleargage();
                ssoHelper = oclsCleargage.InitiateSOSHelper(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                SetClearGageCallbacks(ssoHelper);
                if (oclsCleargage != null)
                {
                    oclsCleargage = null;
                }
            }
			
        }

        #endregion " Constructor "

        #region " Form Load Event "

        private void frmPatientPaymentV2_Load(object sender, EventArgs e)
        {
           //gloC1FlexStyle.Style(c1SinglePayment, true);
            AddGotFocusListener(this);
            LoadPatientStrip(_PatientID, 0, false);
            FormDefaultControl = mskCloseDate;
            if (oPatientControl != null)
            {
                txtPatientSearch.Text = oPatientControl.PatientCode;
            }
            if (IsFromCleargagePosting && gloGlobal.gloPMGlobal.IsCleargageEnable)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Open, "Manual Cleargage Payment Opened", _PatientID, Convert.ToInt64(dtCleargagePaymentDetails.Rows[0]["nFileID"]), 0, gloAuditTrail.ActivityOutCome.Success,gloAuditTrail.SoftwareComponent.gloPM,true);
                oPatientControl.IsAllAccPatSelected = false;
            }
            DisplayGlobalPeriodAlert(_PatientID);

            //SLR: Free previous memory before assigning
            if (dsInsurancePayment_TVP!=null)
            {
                dsInsurancePayment_TVP = null;
            }
            dsInsurancePayment_TVP = new gloBilling.gloAccountPayment.dsPaymentTVP_V2();
            SetShowZeroBalance();
            SetShowBadDebtBalance();
            
            ClearFormData();
            if (IsIntuitBillPay)
            {
                SetIntuitBillPay();
            }
            FillPaymentTray();
            SetPaymentTrayPopup();
            mskCloseDate.Focus();
            Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            tom.SetTabOrder(scheme);
            IsBusinessCenterEnable = gloGlobal.gloPMGlobal.GetBusinessCenterSettings("BusinessCenter_PatientAccount");
            AllowEditValidation();
            //SLR: Free tom
            if (tom!=null)
            {
                tom = null;
            }
            if (IsFromCopayReserveList)
            {
                AllocateUseReserveforCopay();
            }
            if (gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
            {
                tsb_ShowHideBadDebtBalance.Visible = true;
            }
            else
            {
                tsb_ShowHideBadDebtBalance.Visible = false;
            }
            if (IsFromCleargagePosting == true && gloGlobal.gloPMGlobal.IsCleargageEnable)
            {
                AllocateCleargageAmount();
            }
            if (!gloGlobal.gloPMGlobal.IsCleargageEnable)
            {
                tls_btnOneTimePayment.Visible = false;
            }
        }

        private void DisplayGlobalPeriodAlert(long _PatientID)
        {
            //DataTable dtGlobalPeriod = gloBillingCommonV2.GetGlobalPeriods_ForAlter(_PatientID);
            DataTable dtGlobalPeriod = gloCharges.GetLastPatientGlobalPeriod(_PatientID,true);

            if (dtGlobalPeriod != null)
            {
                if (dtGlobalPeriod.Rows.Count > 0)
                {
                    pnlAlerts.Visible = true;
                    //lblGlobalPeriodAlert.Text = "Global Period in Effect : " + dtGlobalPeriod.Rows[0]["Dates"].ToString();
                    //nGlobalPeriodId = Convert.ToInt64(dtGlobalPeriod.Rows[0]["nId"].ToString());

                    string sMessage = Convert.ToString(dtGlobalPeriod.Rows[0]["sDuration"]) + "   ";
                    lblGlobalPeriodAlert.Text = (sMessage != string.Empty ? "Global Period in Effect : " + sMessage : "");
                    nGlobalPeriodId = Convert.ToInt64(dtGlobalPeriod.Rows[0]["nGlobalPeriodID"]);
                    
                }
                else
                    pnlAlerts.Visible = false;  
            }
            else
                pnlAlerts.Visible = false;  



        }

        #endregion " Form Load Event "

        #region " C1 Grid Design Methods "

        private void RefreshClaimList()
        {
            //Start Code added by Subashish ---------------------
            if (_IsPatientAccountFeature)
            {

                //if (oPatientControl.PatientID > 0)
                if (!oPatientControl.IsAllAccPatSelected)
                {
                    FillBillingTransactionAccountPatient(oPatientControl.PatientID, oPatientControl.PAccountID);
                }
                else
                {
                    FillBillingTransactionAccount(oPatientControl.PAccountID, true);
                }
            }
            else
            {
                if (_PatientID > 0)
                { FillBillingTransaction(_PatientID, true); }
            }


        }

        private void RefreshFormData()
        {

            RefreshClaimList();

            CalculateRemainingAmount();

            //if (oPatientControl != null)
            //{
            //    oPatientControl.FillDetails(_PatientID, gloStripControl.FormName.None, 0, false);
            //}
            Cls_TabIndexSettings.TabScheme scheme = Cls_TabIndexSettings.TabScheme.AcrossFirst;
            Cls_TabIndexSettings tom = new Cls_TabIndexSettings(this);
            tom.SetTabOrder(scheme);
            //SLR: FRee tom
            if (tom!=null)
            {
                tom = null;
            }
        }
        private void DesignPaymentGrid(C1FlexGrid c1Payment)
        {
            try
            {
                _IsFormLoading = true;
                c1Payment.Redraw = false;
                c1Payment.ScrollBars = ScrollBars.None;
                c1Payment.AllowSorting = AllowSortingEnum.None;

                c1Payment.Clear();
                c1Payment.Cols.Count = COL_COUNT;
                c1Payment.Rows.Count = 1;
                c1Payment.Rows.Fixed = 1;
                c1Payment.Cols.Fixed = 0;
                c1Payment.ScrollBars = ScrollBars.None;
                c1Payment.KeyActionEnter = KeyActionEnum.None;
                c1Payment.KeyActionTab = KeyActionEnum.MoveAcross;

                if (c1Payment.Name == c1SinglePaymentTotal.Name)
                {
                    c1Payment.Rows.Fixed = 0;
                    c1Payment.ScrollBars = ScrollBars.Horizontal;
                    c1Payment.ForeColor = Color.Maroon;
                    if (_IsPatientAccountFeature == true)
                    {
                        if (c1Payment.Height < 38)
                        {
                            c1Payment.Height = c1Payment.Height + c1Payment.ScrollableRectangle.Height;
                        }
                    }

                }
                else
                #region " Set Headers "
                {

                    c1Payment.SetData(0, COL_GENERAL, "General");
                    c1Payment.SetData(0, COL_PATIENTID, "Patient ID");
                    c1Payment.SetData(0, COL_PATIENTNAME, "Patient");
                    c1Payment.SetData(0, COL_CLAIMNO, "Claim #");
                    //Added by Subashish_b on 16/March /2011 (integration made on date-10/May/2011) for  displaying Responsible party name and no
                    c1Payment.SetData(0, COL_CRESP_PARTY, "Party");
                    //End

                    c1Payment.SetData(0, COL_BILLING_TRANSACTON_ID, "Transacton ID");
                    c1Payment.SetData(0, COL_BILLING_TRANSACTON_DETAILID, "Transacton Detail ID");
                    c1Payment.SetData(0, COL_BILLING_TRANSACTON_LINENO, "Transacton Line No");

                    c1Payment.SetData(0, COL_PAYMENT_NO, "Payment No");
                    c1Payment.SetData(0, COL_PAY_DATE, "Payment Date");

                    c1Payment.SetData(0, COL_PAY_EOBPAYMENTID, "EOBPaymentID");
                    c1Payment.SetData(0, COL_PAY_EOBID, "EOBID");
                    c1Payment.SetData(0, COL_PAY_EOBDTLID, "EOBDetailID");
                    c1Payment.SetData(0, COL_PAY_EOBPAYMENTDTLID, "EOBPaymentDetailID");

                    c1Payment.SetData(0, COL_CLAIMDISPNO, "Claim #");
                    c1Payment.SetData(0, COL_DOS_FROM, "DOS");
                    c1Payment.SetData(0, COL_DOS_TO, "DOS To");
                    c1Payment.SetData(0, COL_CPT_CODE, "CPT");
                    c1Payment.SetData(0, COL_CPT_DESCRIPTON, "CPT Description");

                    c1Payment.SetData(0, COL_CHARGE, "Charge");
                    c1Payment.SetData(0, COL_ALLOWED, "Allowed");
                    c1Payment.SetData(0, COL_UNIT, "Unit");
                    c1Payment.SetData(0, COL_TOTALCHARGE, "Charge");
                    c1Payment.SetData(0, COL_PREV_PAID, "Prev Paid");
                    c1Payment.SetData(0, COL_PREV_ADJ, "Prev Adj");
                    c1Payment.SetData(0, COL_PAT_DUE, "Pat. Due");
                    c1Payment.SetData(0, COL_BadDebt_DUE, "Bad Debt Due");
                    c1Payment.SetData(0, COL_CLM_BALANCE, "Balance");


                    c1Payment.SetData(0, COL_CUR_PAYMENT, "Payment");

                    c1Payment.SetData(0, COL_CUR_ADJ_TYPECODE, "Adj Code");
                    c1Payment.SetData(0, COL_CUR_ADJ_TYPEDESCRIPTION, "Adj Description");
                    c1Payment.SetData(0, COL_CUR_ADJ_AMOUNT, "Adj Amount");

                    c1Payment.SetData(0, COL_SERVICELINE_TYPE, "Line Type");
                    c1Payment.SetData(0, COL_ISOPENFORMODIFY, "Open For Modify");
                    c1Payment.SetData(0, COL_PAY_CLINICID, "Clinic ID");
                    c1Payment.SetData(0, COL_PAY_LINESTATUS, "Line Status");

                    // Newly added columns by Pankaj
                    c1Payment.SetData(0, COL_TRACK_TRN_ID, "TrackTrnID");
                    c1Payment.SetData(0, COL_TRACK_TRN_DTL_ID, "TrackTrnDtlID");
                    c1Payment.SetData(0, COL_SUB_CLAIM_NO, "sSubClaimNo");

                    c1Payment.SetData(0, COL_PREV_PATIENTPAYMENT_AMT, "Prev. PatPaidAmt");
                    c1Payment.SetData(0, COL_CLAIMSUBCLAIM_NO, "ClaimSubClaim No");
                    c1Payment.SetData(0, COL_HOLD, "Hold");
                    c1Payment.SetData(0, COL_MODIFIER, "Mod");
                    //c1Payment.SetData(0, COL_HOLD, "nTransactionDate");
                }
                #endregion

                #region " Show/Hide "

                c1Payment.Cols[COL_GENERAL].Visible = false;
                c1Payment.Cols[COL_PATIENTID].Visible = false;
                //Start Code Modified By Subashish- 24-01-2011---
                //For displaying Patient Name in PAF
                //Previous Syntax/code
                //c1Payment.Cols[COL_PATIENTNAME].Visible = false;
                if (_IsPatientAccountFeature)
                {
                    c1Payment.Cols[COL_PATIENTNAME].Visible = true;
                }
                else
                {
                    c1Payment.Cols[COL_PATIENTNAME].Visible = false;
                }
                //End
                //Added by Subashish_b on 16/March /2011 (integration made on date-10/May/2011) for  displaying Responsible party name and no
                c1Payment.Cols[COL_CRESP_PARTY].Visible = true;
                //End
                c1Payment.Cols[COL_CLAIMNO].Visible = false;

                c1Payment.Cols[COL_BILLING_TRANSACTON_ID].Visible = false;
                c1Payment.Cols[COL_BILLING_TRANSACTON_DETAILID].Visible = false;
                c1Payment.Cols[COL_BILLING_TRANSACTON_LINENO].Visible = false;

                c1Payment.Cols[COL_PAYMENT_NO].Visible = false;
                c1Payment.Cols[COL_PAY_DATE].Visible = false;

                c1Payment.Cols[COL_PAY_EOBPAYMENTID].Visible = false;
                c1Payment.Cols[COL_PAY_EOBID].Visible = false;
                c1Payment.Cols[COL_PAY_EOBDTLID].Visible = false;
                c1Payment.Cols[COL_PAY_EOBPAYMENTDTLID].Visible = false;

                c1Payment.Cols[COL_CLAIMDISPNO].Visible = true;
                c1Payment.Cols[COL_DOS_FROM].Visible = true;
                c1Payment.Cols[COL_DOS_TO].Visible = false;
                c1Payment.Cols[COL_CPT_CODE].Visible = true;
                c1Payment.Cols[COL_CPT_DESCRIPTON].Visible = false;
                c1Payment.Cols[COL_MODIFIER].Visible = true;
                c1Payment.Cols[COL_CHARGE].Visible = false;
                c1Payment.Cols[COL_ALLOWED].Visible = true;
                c1Payment.Cols[COL_UNIT].Visible = false;
                c1Payment.Cols[COL_TOTALCHARGE].Visible = true;
                c1Payment.Cols[COL_PREV_PAID].Visible = true;
                c1Payment.Cols[COL_PREV_ADJ].Visible = true;
                c1Payment.Cols[COL_PAT_DUE].Visible = true;
                c1Payment.Cols[COL_CLM_BALANCE].Visible = true;

                c1Payment.Cols[COL_CUR_PAYMENT].Visible = true;

                c1Payment.Cols[COL_CUR_ADJ_TYPECODE].Visible = true;
                c1Payment.Cols[COL_CUR_ADJ_TYPEDESCRIPTION].Visible = false;
                c1Payment.Cols[COL_CUR_ADJ_AMOUNT].Visible = true;
                c1Payment.Cols[COL_PREV_PATIENTADJUSTMENT_AMT].Visible = false;

                c1Payment.Cols[COL_SERVICELINE_TYPE].Visible = false;
                c1Payment.Cols[COL_ISOPENFORMODIFY].Visible = false;
                c1Payment.Cols[COL_PAY_CLINICID].Visible = false;
                c1Payment.Cols[COL_PAY_LINESTATUS].Visible = false;

                c1Payment.Cols[COL_CELLRANGE_R1].Visible = false;
                c1Payment.Cols[COL_CELLRANGE_R2].Visible = false;

                // newly added colums by Pankaj
                c1Payment.Cols[COL_TRACK_TRN_ID].Visible = false;
                c1Payment.Cols[COL_TRACK_TRN_DTL_ID].Visible = false;
                c1Payment.Cols[COL_SUB_CLAIM_NO].Visible = false;
                c1Payment.Cols[COL_PREV_PATIENTPAYMENT_AMT].Visible = false;
                c1Payment.Cols[COL_CLAIMSUBCLAIM_NO].Visible = false;
                c1Payment.Cols[COL_HOLD].Visible = true;
                c1Payment.Cols[COL_TRANSACTION_DATE].Visible = false;
                #endregion

                #region " Width "
                bool _designWidth = false;

                if (_designWidth == false)
                {
                    c1Payment.Cols[COL_GENERAL].Width = 0;
                    c1Payment.Cols[COL_PATIENTID].Width = 0;



                    //Start Code Modified By Subashish- 24-01-2011---
                    //For displaying Patient Name in PAF
                    //Previous Syntax/code
                    //c1Payment.Cols[COL_PATIENTNAME].Width = 0;
                    if (_IsPatientAccountFeature)
                    {
                        c1Payment.Cols[COL_PATIENTNAME].Width = 120;
                    }
                    else
                    {
                        c1Payment.Cols[COL_PATIENTNAME].Width = 0;
                    }
                    //End Code Modified By Subashish- 24-01-2011---

                    c1Payment.Cols[COL_CLAIMNO].Width = 0;

                    c1Payment.Cols[COL_BILLING_TRANSACTON_ID].Width = 0;
                    c1Payment.Cols[COL_BILLING_TRANSACTON_DETAILID].Width = 0;
                    c1Payment.Cols[COL_BILLING_TRANSACTON_LINENO].Width = 0;

                    c1Payment.Cols[COL_PAYMENT_NO].Width = 0;
                    c1Payment.Cols[COL_PAY_DATE].Width = 0;

                    c1Payment.Cols[COL_PAY_EOBPAYMENTID].Width = 0;
                    c1Payment.Cols[COL_PAY_EOBID].Width = 0;
                    c1Payment.Cols[COL_PAY_EOBDTLID].Width = 0;
                    c1Payment.Cols[COL_PAY_EOBPAYMENTDTLID].Width = 0;

                    c1Payment.Cols[COL_CLAIMDISPNO].Width = 70;
                    c1Payment.Cols[COL_DOS_FROM].Width = 80;
                    c1Payment.Cols[COL_DOS_TO].Width = 0;
                    c1Payment.Cols[COL_CPT_CODE].Width = 50;
                    c1Payment.Cols[COL_CPT_DESCRIPTON].Width = 0;
                    c1Payment.Cols[COL_MODIFIER].Width = 75;
                    c1Payment.Cols[COL_CHARGE].Width = 0;
                    c1Payment.Cols[COL_ALLOWED].Width = 90;
                    c1Payment.Cols[COL_UNIT].Width = 0;
                    c1Payment.Cols[COL_TOTALCHARGE].Width = 90;
                    c1Payment.Cols[COL_PREV_PAID].Width = 90;
                    c1Payment.Cols[COL_PREV_ADJ].Width = 90;
                    c1Payment.Cols[COL_PAT_DUE].Width = 90;
                    c1Payment.Cols[COL_CLM_BALANCE].Width = 90;

                    c1Payment.Cols[COL_CUR_PAYMENT].Width = 90;

                    c1Payment.Cols[COL_CUR_ADJ_TYPECODE].Width = 90;
                    c1Payment.Cols[COL_CUR_ADJ_TYPEDESCRIPTION].Width = 0;
                    c1Payment.Cols[COL_CUR_ADJ_AMOUNT].Width = 90;

                    c1Payment.Cols[COL_SERVICELINE_TYPE].Width = 0;
                    c1Payment.Cols[COL_ISOPENFORMODIFY].Width = 0;
                    c1Payment.Cols[COL_PAY_CLINICID].Width = 0;
                    c1Payment.Cols[COL_PAY_LINESTATUS].Width = 0;
                    c1Payment.Cols[COL_CELLRANGE_R1].Width = 0;
                    c1Payment.Cols[COL_CELLRANGE_R2].Width = 0;
                    c1Payment.Cols[COL_PREV_PATIENTPAYMENT_AMT].Width = 0;
                    c1Payment.Cols[COL_CLAIMSUBCLAIM_NO].Width = 0;
                    c1Payment.Cols[COL_HOLD].Width = 150;
                }

                #endregion

                #region " Data Type "
                c1Payment.Cols[COL_GENERAL].DataType = typeof(System.String);
                c1Payment.Cols[COL_PATIENTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_PATIENTNAME].DataType = typeof(System.String);
                c1Payment.Cols[COL_CLAIMNO].DataType = typeof(System.String);
                //Added by Subashish_b on 16/March /2011 (integration made on date-10/May/2011) for  setting Responsible party column datatype
                c1Payment.Cols[COL_CRESP_PARTY].DataType = typeof(System.String);
                //End
                c1Payment.Cols[COL_BILLING_TRANSACTON_ID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_BILLING_TRANSACTON_DETAILID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_BILLING_TRANSACTON_LINENO].DataType = typeof(System.Int64);

                c1Payment.Cols[COL_PAYMENT_NO].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_PAY_DATE].DataType = typeof(System.String);

                c1Payment.Cols[COL_PAY_EOBPAYMENTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_PAY_EOBID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_PAY_EOBDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_PAY_EOBPAYMENTDTLID].DataType = typeof(System.Int64);

                c1Payment.Cols[COL_CLAIMDISPNO].DataType = typeof(System.String);



                //Date Format Mahesh Nawal
                c1Payment.Cols[COL_DOS_FROM].DataType = typeof(System.DateTime);
                c1Payment.Cols[COL_DOS_FROM].Format = "MM/dd/yyyy";
                c1Payment.Cols[COL_DOS_TO].DataType = typeof(System.DateTime);
                c1Payment.Cols[COL_DOS_TO].Format = "MM/dd/yyyy";

                //c1Payment.Cols[COL_DOS_FROM].DataType = typeof(System.String);
                //c1Payment.Cols[COL_DOS_TO].DataType = typeof(System.String);




                c1Payment.Cols[COL_CPT_CODE].DataType = typeof(System.String);
                c1Payment.Cols[COL_CPT_DESCRIPTON].DataType = typeof(System.String);
                c1Payment.Cols[COL_MODIFIER].DataType = typeof(System.String);

                c1Payment.Cols[COL_CHARGE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_ALLOWED].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_UNIT].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_TOTALCHARGE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_PREV_PAID].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_PREV_ADJ].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_PAT_DUE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_CLM_BALANCE].DataType = typeof(System.Decimal);

                c1Payment.Cols[COL_CUR_PAYMENT].DataType = typeof(System.Decimal);

                c1Payment.Cols[COL_CUR_ADJ_TYPECODE].DataType = typeof(System.String);
                c1Payment.Cols[COL_CUR_ADJ_TYPEDESCRIPTION].DataType = typeof(System.String);
                c1Payment.Cols[COL_CUR_ADJ_AMOUNT].DataType = typeof(System.Decimal);

                c1Payment.Cols[COL_SERVICELINE_TYPE].DataType = typeof(ColServiceLineType);
                c1Payment.Cols[COL_ISOPENFORMODIFY].DataType = typeof(System.String);
                c1Payment.Cols[COL_PAY_CLINICID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_PAY_LINESTATUS].DataType = typeof(System.String);

                // newly added columns by pankaj
                c1Payment.Cols[COL_TRACK_TRN_ID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_TRACK_TRN_DTL_ID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_SUB_CLAIM_NO].DataType = typeof(System.String);
                c1Payment.Cols[COL_PREV_PATIENTPAYMENT_AMT].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_CLAIMSUBCLAIM_NO].DataType = typeof(System.String);

                c1Payment.Cols[COL_HOLD].DataType = typeof(System.String);
                #endregion

                #region " Alignment "

                c1Payment.Cols[COL_GENERAL].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PATIENTNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_CLAIMNO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                //Added by Subashish_b on 16/March /2011 (integration made on date-10/May/2011) for  setting alignment for Responsible party column
                c1Payment.Cols[COL_CRESP_PARTY].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                //End

                c1Payment.Cols[COL_BILLING_TRANSACTON_ID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_BILLING_TRANSACTON_DETAILID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_BILLING_TRANSACTON_LINENO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                c1Payment.Cols[COL_PAYMENT_NO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PAY_DATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                c1Payment.Cols[COL_PAY_EOBPAYMENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PAY_EOBID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PAY_EOBDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PAY_EOBPAYMENTDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                c1Payment.Cols[COL_CLAIMDISPNO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_DOS_FROM].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_DOS_TO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_CPT_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_CPT_DESCRIPTON].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_MODIFIER].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_CHARGE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_ALLOWED].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_UNIT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_TOTALCHARGE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_PREV_PAID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_PREV_ADJ].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_PAT_DUE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_CLM_BALANCE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;


                c1Payment.Cols[COL_CUR_PAYMENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

                c1Payment.Cols[COL_CUR_ADJ_TYPECODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_CUR_ADJ_TYPEDESCRIPTION].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_CUR_ADJ_AMOUNT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

                c1Payment.Cols[COL_SERVICELINE_TYPE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_ISOPENFORMODIFY].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PAY_CLINICID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PAY_LINESTATUS].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                // Newly added columns by pankaj
                c1Payment.Cols[COL_TRACK_TRN_ID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_TRACK_TRN_DTL_ID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_SUB_CLAIM_NO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PREV_PATIENTPAYMENT_AMT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_CLAIMSUBCLAIM_NO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                #endregion

                #region " Set Styles "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1Payment.Styles.Add("cs_CurrencyStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = c1Payment.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = c1Payment.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                        //replace declaration for create new font by font variable.
                        csCurrencyStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    }

                }
                catch
                {
                    csCurrencyStyle = c1Payment.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                    //replace declaration for create new font by font variable.
                    csCurrencyStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                }
       

                c1Payment.Cols[COL_CHARGE].Style = csCurrencyStyle;
                c1Payment.Cols[COL_ALLOWED].Style = csCurrencyStyle;
                c1Payment.Cols[COL_TOTALCHARGE].Style = csCurrencyStyle;
                c1Payment.Cols[COL_PREV_PAID].Style = csCurrencyStyle;
                c1Payment.Cols[COL_PREV_ADJ].Style = csCurrencyStyle;
                c1Payment.Cols[COL_PAT_DUE].Style = csCurrencyStyle;
                c1Payment.Cols[COL_BadDebt_DUE].Style = csCurrencyStyle;
                c1Payment.Cols[COL_CLM_BALANCE].Style = csCurrencyStyle;
                c1Payment.Cols[COL_CUR_PAYMENT].Style = csCurrencyStyle;
                c1Payment.Cols[COL_CUR_ADJ_AMOUNT].Style = csCurrencyStyle;
                c1Payment.Cols[COL_PREV_PATIENTPAYMENT_AMT].Style = csCurrencyStyle;

                C1.Win.C1FlexGrid.CellStyle csEditableCurrencyStyle;// = c1Payment.Styles.Add("cs_EditableCurrencyStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_EditableCurrencyStyle"))
                    {
                        csEditableCurrencyStyle = c1Payment.Styles["cs_EditableCurrencyStyle"];
                    }
                    else
                    {
                        csEditableCurrencyStyle = c1Payment.Styles.Add("cs_EditableCurrencyStyle");
                        csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                        csEditableCurrencyStyle.Format = "c";
                        //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                        //replace declaration for create new font by font variable.
                        csEditableCurrencyStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableCurrencyStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableCurrencyStyle = c1Payment.Styles.Add("cs_EditableCurrencyStyle");
                    csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                    csEditableCurrencyStyle.Format = "c";
                    //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                    //replace declaration for create new font by font variable.
                    csEditableCurrencyStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableCurrencyStyle.BackColor = Color.White;

                }
        




                C1.Win.C1FlexGrid.CellStyle csEditableStringStyle;// = c1Payment.Styles.Add("cs_EditableStringStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_EditableStringStyle"))
                    {
                        csEditableStringStyle = c1Payment.Styles["cs_EditableStringStyle"];
                    }
                    else
                    {
                        csEditableStringStyle = c1Payment.Styles.Add("cs_EditableStringStyle");
                        csEditableStringStyle.DataType = typeof(System.String);
                        //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                        //replace declaration for create new font by font variable.
                        csEditableStringStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableStringStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableStringStyle = c1Payment.Styles.Add("cs_EditableStringStyle");
                    csEditableStringStyle.DataType = typeof(System.String);
                    //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                    //replace declaration for create new font by font variable.
                    csEditableStringStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableStringStyle.BackColor = Color.White;

                }
 

                C1.Win.C1FlexGrid.CellStyle csEditableDateStyle;// = c1Payment.Styles.Add("cs_EditableDateStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_EditableDateStyle"))
                    {
                        csEditableDateStyle = c1Payment.Styles["cs_EditableDateStyle"];
                    }
                    else
                    {
                        csEditableDateStyle = c1Payment.Styles.Add("cs_EditableDateStyle");
                        csEditableDateStyle.DataType = typeof(System.DateTime);
                        //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                        //replace declaration for create new font by font variable.
                        csEditableDateStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableDateStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableDateStyle = c1Payment.Styles.Add("cs_EditableDateStyle");
                    csEditableDateStyle.DataType = typeof(System.DateTime);
                    //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                    //replace declaration for create new font by font variable.
                    csEditableDateStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableDateStyle.BackColor = Color.White;

                }
   

                C1.Win.C1FlexGrid.CellStyle csClaimRowStyle;// = c1Payment.Styles.Add("cs_ClaimRowStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_ClaimRowStyle"))
                    {
                        csClaimRowStyle = c1Payment.Styles["cs_ClaimRowStyle"];
                    }
                    else
                    {
                        csClaimRowStyle = c1Payment.Styles.Add("cs_ClaimRowStyle");
                        csClaimRowStyle.DataType = typeof(System.String);
                        //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                        //replace declaration for create new font by font variable.
                        csClaimRowStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);
                    }

                }
                catch
                {
                    csClaimRowStyle = c1Payment.Styles.Add("cs_ClaimRowStyle");
                    csClaimRowStyle.DataType = typeof(System.String);
                    //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                    //replace declaration for create new font by font variable.
                    csClaimRowStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);

                }
   



                C1.Win.C1FlexGrid.CellStyle csPatientRowStyle;// = c1Payment.Styles.Add("cs_PatientRowStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_PatientRowStyle"))
                    {
                        csPatientRowStyle = c1Payment.Styles["cs_PatientRowStyle"];
                    }
                    else
                    {
                        csPatientRowStyle = c1Payment.Styles.Add("cs_PatientRowStyle");
                        csPatientRowStyle.DataType = typeof(System.String);
                        //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                        //replace declaration for create new font by font variable.
                        csPatientRowStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csPatientRowStyle.BackColor = Color.FromArgb(215, 228, 188);
                    }

                }
                catch
                {
                    csPatientRowStyle = c1Payment.Styles.Add("cs_PatientRowStyle");
                    csPatientRowStyle.DataType = typeof(System.String);
                    //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                    //replace declaration for create new font by font variable.
                    csPatientRowStyle.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csPatientRowStyle.BackColor = Color.FromArgb(215, 228, 188);

                }
 


                #region " Set Payment Action Status "

                if (c1Payment.Name == c1SinglePayment.Name)
                {
                    string _comboList = "";
                    gloPatientPaymentV2 ogloEOBPayPat = new gloPatientPaymentV2();

                    C1.Win.C1FlexGrid.CellStyle csEditableAdjustment;// = c1Payment.Styles.Add("cs_EditableAdjustment");
                    try
                    {
                        if (c1Payment.Styles.Contains("cs_EditableAdjustment"))
                        {
                            csEditableAdjustment = c1Payment.Styles["cs_EditableAdjustment"];
                        }
                        else
                        {
                            csEditableAdjustment = c1Payment.Styles.Add("cs_EditableAdjustment");
                            csEditableAdjustment.DataType = typeof(System.String);
                            //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                            //replace declaration for create new font by font variable.
                            csEditableAdjustment.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                            csEditableAdjustment.BackColor = Color.White;
                        }

                    }
                    catch
                    {
                        csEditableAdjustment = c1Payment.Styles.Add("cs_EditableAdjustment");
                        csEditableAdjustment.DataType = typeof(System.String);
                        //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                        //replace declaration for create new font by font variable.
                        csEditableAdjustment.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableAdjustment.BackColor = Color.White;

                    }
        
                    _comboList = ogloEOBPayPat.GetAdjustmentCodes();
                    csEditableAdjustment.ComboList = _comboList;
                    //SLR: Free ogloEOBPayPat
                    if (ogloEOBPayPat!=null)
                    {
                        ogloEOBPayPat.Dispose();
                    }


                }


                #endregion " Set Payment Action Status "

                #endregion

                c1Payment.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1Payment.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #region " Allow Editing "

                c1Payment.AllowEditing = true;

                if (c1Payment.Name == c1SinglePayment.Name)
                {
                    c1Payment.Cols[COL_GENERAL].AllowEditing = false;
                    c1Payment.Cols[COL_PATIENTID].AllowEditing = false;
                    c1Payment.Cols[COL_PATIENTNAME].AllowEditing = false;
                    c1Payment.Cols[COL_CLAIMNO].AllowEditing = false;
                    //Added by Subashish_b on 16/March /2011 (integration made on date-10/May/2011) for  allowing editing status
                    c1Payment.Cols[COL_CRESP_PARTY].AllowEditing = false;
                    //End

                    c1Payment.Cols[COL_BILLING_TRANSACTON_ID].AllowEditing = false;
                    c1Payment.Cols[COL_BILLING_TRANSACTON_DETAILID].AllowEditing = false;
                    c1Payment.Cols[COL_BILLING_TRANSACTON_LINENO].AllowEditing = false;

                    c1Payment.Cols[COL_PAYMENT_NO].AllowEditing = false;
                    c1Payment.Cols[COL_PAY_DATE].AllowEditing = false;

                    c1Payment.Cols[COL_PAY_EOBPAYMENTID].AllowEditing = false;
                    c1Payment.Cols[COL_PAY_EOBID].AllowEditing = false;
                    c1Payment.Cols[COL_PAY_EOBDTLID].AllowEditing = false;
                    c1Payment.Cols[COL_PAY_EOBPAYMENTDTLID].AllowEditing = false;

                    c1Payment.Cols[COL_CLAIMDISPNO].AllowEditing = false;
                    c1Payment.Cols[COL_DOS_FROM].AllowEditing = false;
                    c1Payment.Cols[COL_DOS_TO].AllowEditing = false;
                    c1Payment.Cols[COL_CPT_CODE].AllowEditing = false;
                    c1Payment.Cols[COL_CPT_DESCRIPTON].AllowEditing = false;
                    c1Payment.Cols[COL_MODIFIER].AllowEditing = false;

                    c1Payment.Cols[COL_CHARGE].AllowEditing = false;
                    c1Payment.Cols[COL_ALLOWED].AllowEditing = false;
                    c1Payment.Cols[COL_UNIT].AllowEditing = false;
                    c1Payment.Cols[COL_TOTALCHARGE].AllowEditing = false;
                    c1Payment.Cols[COL_PREV_PAID].AllowEditing = false;
                    c1Payment.Cols[COL_PREV_ADJ].AllowEditing = false;
                    c1Payment.Cols[COL_PAT_DUE].AllowEditing = false;
                    c1Payment.Cols[COL_CLM_BALANCE].AllowEditing = false;

                    c1Payment.Cols[COL_CUR_PAYMENT].AllowEditing = true;

                    c1Payment.Cols[COL_CUR_ADJ_TYPECODE].AllowEditing = true;
                    c1Payment.Cols[COL_CUR_ADJ_TYPEDESCRIPTION].AllowEditing = true;
                    c1Payment.Cols[COL_CUR_ADJ_AMOUNT].AllowEditing = true;

                    c1Payment.Cols[COL_SERVICELINE_TYPE].AllowEditing = false;
                    c1Payment.Cols[COL_ISOPENFORMODIFY].AllowEditing = false;
                    c1Payment.Cols[COL_PAY_CLINICID].AllowEditing = false;
                    c1Payment.Cols[COL_PAY_LINESTATUS].AllowEditing = false;

                    // Newly added columns by pankaj
                    c1Payment.Cols[COL_TRACK_TRN_ID].AllowEditing = false;
                    c1Payment.Cols[COL_TRACK_TRN_DTL_ID].AllowEditing = false;
                    c1Payment.Cols[COL_SUB_CLAIM_NO].AllowEditing = false;
                    c1Payment.Cols[COL_PREV_PATIENTPAYMENT_AMT].AllowEditing = false;
                    c1Payment.Cols[COL_CLAIMSUBCLAIM_NO].AllowEditing = false;
                    c1Payment.Cols[COL_HOLD].AllowEditing = false;
                    c1Payment.Cols[COL_NON_SERVICECODE].Visible = false;
                }
                else
                {
                    for (int i = 0; i <= c1Payment.Cols.Count - 1; i++)
                    {
                        c1Payment.Cols[i].AllowEditing = false;
                    }
                }
                #endregion
                c1Payment.VisualStyle = VisualStyle.Office2007Blue;
                c1Payment.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                c1Payment.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                c1Payment.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);
                c1Payment.ExtendLastCol = true;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            { _IsFormLoading = false; c1Payment.Redraw = true; }
        }

        #endregion " C1 Grid Design Methods "

        #region " Patient Strip Control Events "


        void oPatientControl_PatientChanged(object sender, EventArgs e)
        {
            try
            {
                _PatientID = oPatientControl.PatientID;
                _afterCloseDateChanged = 0;
                this.nPAccountID = oPatientControl.PAccountID;
                this.nGuarantorID = oPatientControl.GuarantorID;
                this.nAccountPatientID = oPatientControl.AccountPatientID;
                txtPatientSearch.Text = oPatientControl.PatientCode.ToString();

                if (oPatientControl.IsAllAccPatSelected)
                {
                    if (oPatientControl.PAccountID != 0)
                    {
                        
                        FillBillingTransactionAccount(oPatientControl.PAccountID, true);
                    }
                    else
                    {
                        c1SinglePayment.Rows.Count = 1;
                    }
                }
                else
                {
                    if (oPatientControl.PatientID > 0)
                    {
                        FillBillingTransactionAccountPatient(oPatientControl.PatientID, oPatientControl.PAccountID);
                    }
                }
                LoadFormDataOnPatientChanged();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void LoadPatientStrip(Int64 PatientId, Int64 PatientProviderId, bool SearchEnable)
        {
            try
            {
                oPatientControl.IsAllAccPatSelected = true;
                
                //_PatientID = oPatientControl.PatientID;
                if (this.nPAccountID > 0)
                {
                    oPatientControl.FillDetails(PatientId, this.nPAccountID, gloStripControl.FormName.PatientPayment);
                }
                else
                {
                    oPatientControl.FillDetails(PatientId, gloStripControl.FormName.PatientPayment, PatientProviderId, false);
                    this.nPAccountID = oPatientControl.PAccountID;
                }
                
                this.nGuarantorID = oPatientControl.GuarantorID;
                this.nAccountPatientID = oPatientControl.AccountPatientID;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }
        void oPatientControl_OnAccountChanged(object sender, EventArgs e)
        {
            _PatientID = oPatientControl.PatientID;
            _afterCloseDateChanged = 0;
            nPAccountID = oPatientControl.PAccountID;
            nAccountPatientID = oPatientControl.AccountPatientID;
            nGuarantorID = oPatientControl.GuarantorID;
            txtPatientSearch.Text = oPatientControl.PatientCode.ToString();

            if (_PatientID > 0)
            {
                FillBillingTransactionAccountPatient(oPatientControl.PatientID, oPatientControl.PAccountID);
            }
            else if (_IsPatientAccountFeature == true && _PatientID == 0 && oPatientControl.PAccountID > 0)
            {
                FillBillingTransactionAccount(oPatientControl.PAccountID, true);
            }
            LoadFormDataOnPatientChanged();
        }
        #endregion " Patient Strip Control Events "

        #region "Methods and Procedures"

        private void FillBillingTransactionAccount(Int64 nPAccountID, bool bProcessOnce)
        {
            gloAccountsV2.gloPatientPaymentV2 ogloPatientPaymentV2 = new gloAccountsV2.gloPatientPaymentV2();
            gloAccountsV2.PaymentCollection.PaymentPatientClaim oPaymentPatientClaim = new gloAccountsV2.PaymentCollection.PaymentPatientClaim();
            gloAccountsV2.PaymentCollection.PaymentPatientClaims oPaymentPatientClaims = new gloAccountsV2.PaymentCollection.PaymentPatientClaims();
            ClsFeeSchedule objClsFeeSchedule = null;
            int _FocusRowIndex = 0;
            int _ClaimStartRowIndex = 0;
         //   Int64 TransactionDate = 0;
            Boolean IsBadDebtPatient = false;

            try
            {
                if (bProcessOnce) // Subashish
                {
                    _IsPaymentGridLoading = true;
                    DesignPaymentGrid(c1SinglePayment);

                    c1SinglePayment.Redraw = false;
                    c1SinglePayment.ScrollBars = ScrollBars.None;
                }

                if (oPatientControl.IsAllAccPatSelected)
                {
                    IsBadDebtPatient = gloPatientPaymentV2.IsBadDebtPatient(nPAccountID, 0);
                }
                else
                {
                    IsBadDebtPatient = gloPatientPaymentV2.IsBadDebtPatient(nPAccountID, _PatientID);
                }
                #region "Get Billied Transaction"

                oPaymentPatientClaims = ogloPatientPaymentV2.GetBillingTransactionAccountPatients_PAF(nPAccountID, ShowZeroBalanceClaims,ShowBadDebtBalanceClaims);

                #endregion

                #region "Fill Billed Transaction"

                if (oPaymentPatientClaims != null && oPaymentPatientClaims.Count > 0)
                {
                    for (int x = 0; x < oPaymentPatientClaims.Count; x++)
                    {
                        if (oPaymentPatientClaims[x] != null)
                        {
                            oPaymentPatientClaim = oPaymentPatientClaims[x];
                            if (oPaymentPatientClaim.CliamLines.Count > 0)
                            {
                                #region "Master Data"
                                //Dta = oPaymentPatientClaim.BillingTransactionDate;
                                c1SinglePayment.Rows.Add();
                                c1SinglePayment.Rows[c1SinglePayment.Rows.Count - 1].AllowEditing = false;

                                //Subashish to accomodate PAF
                                if (_IsPatientAccountFeature)
                                {
                                    c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_PATIENTID, oPaymentPatientClaim.PatientID);
                                    c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_PATIENTNAME, oPaymentPatientClaim.PatientName);

                                }
                                //end Subashish

                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMDISPNO, oPaymentPatientClaim.DisplayClaimNo);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMNO, oPaymentPatientClaim.ClaimNo);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMSUBCLAIM_NO, oPaymentPatientClaim.DisplayClaimNo);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_BILLING_TRANSACTON_ID, oPaymentPatientClaim.BillingTransactionID);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_SERVICELINE_TYPE, ColServiceLineType.Claim);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_PAY_EOBID, 0);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_TRANSACTION_DATE, oPaymentPatientClaim.BillingTransactionDate);
                                c1SinglePayment.Rows[c1SinglePayment.Rows.Count - 1].Style = c1SinglePayment.Styles["cs_ClaimRowStyle"];
                                
                                //Code Added by Subashish
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CRESP_PARTY, oPaymentPatientClaim.RespParty);
                                //Code Added by Subashish
                                _ClaimStartRowIndex = c1SinglePayment.Rows.Count - 1;
                                c1SinglePayment.SetData(_ClaimStartRowIndex, COL_CELLRANGE_R1, c1SinglePayment.Rows.Count - 1);

                                #endregion

                                for (int j = 0; j <= oPaymentPatientClaim.CliamLines.Count - 1; j++)
                                {
                                    c1SinglePayment.Rows.Add();
                                    int _RowIndex = c1SinglePayment.Rows.Count - 1;
                                    if (_FocusRowIndex == 0) { _FocusRowIndex = _RowIndex; }

                                    #region "Service Lines"

                                    c1SinglePayment.SetData(_RowIndex, COL_GENERAL, "");
                                    c1SinglePayment.SetData(_RowIndex, COL_PATIENTID, oPaymentPatientClaim.CliamLines[j].PatientID);
                                    c1SinglePayment.SetData(_RowIndex, COL_PATIENTNAME, "");
                                    c1SinglePayment.SetData(_RowIndex, COL_CRESP_PARTY, "");
                                    //c1SinglePayment.SetData(_RowIndex, COL_CRESP_PARTY, oPaymentPatientClaim.CliamLines[j].RespParty);

                                    c1SinglePayment.SetData(_RowIndex, COL_CLAIMNO, oPaymentPatientClaim.CliamLines[j].ClaimNumber);
                                    //c1SinglePayment.SetData(_RowIndex, COL_CLAIMDISPNO, oPaymentPatientClaim.DisplayClaimNo);
                                    c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMSUBCLAIM_NO, oPaymentPatientClaim.DisplayClaimNo);

                                    c1SinglePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_ID, oPaymentPatientClaim.CliamLines[j].BLTransactionID);
                                    c1SinglePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_DETAILID, oPaymentPatientClaim.CliamLines[j].BLTransactionDetailID);
                                    c1SinglePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_LINENO, oPaymentPatientClaim.CliamLines[j].BLTransactionLineNo);

                                    c1SinglePayment.SetData(_RowIndex, COL_PAYMENT_NO, j + 1);
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_DATE, "");
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_EOBPAYMENTID, ""); //TO DO
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_EOBID, 0);
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_EOBDTLID, 0);
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_EOBPAYMENTDTLID, 0);

                                    c1SinglePayment.SetData(_RowIndex, COL_DOS_FROM, gloDateMaster.gloDate.DateAsDate(oPaymentPatientClaim.CliamLines[j].DOSFrom).ToShortDateString());
                                    c1SinglePayment.SetData(_RowIndex, COL_DOS_TO, gloDateMaster.gloDate.DateAsDate(oPaymentPatientClaim.CliamLines[j].DOSTo).ToShortDateString());
                                    c1SinglePayment.SetData(_RowIndex, COL_CPT_CODE, oPaymentPatientClaim.CliamLines[j].CPTCode);
                                    c1SinglePayment.SetData(_RowIndex, COL_CPT_DESCRIPTON, oPaymentPatientClaim.CliamLines[j].CPTDescription);
                                    c1SinglePayment.SetData(_RowIndex, COL_MODIFIER, oPaymentPatientClaim.CliamLines[j].Modifiers);

                                    c1SinglePayment.SetData(_RowIndex, COL_CHARGE, oPaymentPatientClaim.CliamLines[j].Charges);
                                    if (oPaymentPatientClaim.CliamLines[j].IsNullAllowed)
                                    {

                                        Boolean bHasAllowedAmt = false;
                                        //SLR: Free previous objClsFeeshedule before allocation
                                        if (objClsFeeSchedule!=null)
                                        {
                                            objClsFeeSchedule = null;
                                        }
                                        objClsFeeSchedule = new ClsFeeSchedule(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                                        decimal _dAllowedAmount = objClsFeeSchedule.GetAllowedAmount(oPaymentPatientClaim.BillingTransactionID, oPaymentPatientClaim.CliamLines[j].CPTCode, oPaymentPatientClaim.CliamLines[j].Modifiers, oPaymentPatientClaim.FacilityType, ref bHasAllowedAmt, oPaymentPatientClaim.CliamLines[j].DOSFrom);
                                        if (bHasAllowedAmt)
                                        {
                                            c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, _dAllowedAmount * oPaymentPatientClaim.CliamLines[j].Unit);
                                        }
                                        else
                                        {
                                            c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, null);
                                        }
                                    }                                    
                                    else
                                        c1SinglePayment.SetData(_RowIndex, COL_ALLOWED,  oPaymentPatientClaim.CliamLines[j].Allowed);

                                    c1SinglePayment.SetData(_RowIndex, COL_UNIT, oPaymentPatientClaim.CliamLines[j].Unit);
                                    c1SinglePayment.SetData(_RowIndex, COL_TOTALCHARGE, oPaymentPatientClaim.CliamLines[j].TotalCharges);
                                    c1SinglePayment.SetData(_RowIndex, COL_CUR_PAYMENT, "");

                                    c1SinglePayment.SetData(_RowIndex, COL_PREV_PAID, oPaymentPatientClaim.CliamLines[j].LinePreviousPaid);
                                    c1SinglePayment.SetData(_RowIndex, COL_PREV_ADJ, oPaymentPatientClaim.CliamLines[j].LinePreviousAdjuestment);


                                    c1SinglePayment.SetData(_RowIndex, COL_PAT_DUE, oPaymentPatientClaim.CliamLines[j].LinePatientDue);
                                   // if (oPaymentPatientClaim.CliamLines[j].LineBadDebtDue != null && oPaymentPatientClaim.CliamLines[j].LineBadDebtDue > 0)
                                   // {
                                        c1SinglePayment.SetData(_RowIndex, COL_BadDebt_DUE, oPaymentPatientClaim.CliamLines[j].LineBadDebtDue);
                                   // }
                                    c1SinglePayment.SetData(_RowIndex, COL_CLM_BALANCE, oPaymentPatientClaim.CliamLines[j].LineBalance);


                                    c1SinglePayment.SetData(_RowIndex, COL_SERVICELINE_TYPE, ColServiceLineType.ServiceLine);
                                    c1SinglePayment.SetData(_RowIndex, COL_ISOPENFORMODIFY, "");
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_CLINICID, 0);
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_LINESTATUS, "");
                                    c1SinglePayment.SetData(_RowIndex, COL_PREV_PATIENTPAYMENT_AMT, oPaymentPatientClaim.CliamLines[j].LinePreviousPatientPaid);

                                    c1SinglePayment.SetData(_RowIndex, COL_PREV_PATIENTADJUSTMENT_AMT, oPaymentPatientClaim.CliamLines[j].LinePrevPatientAdjustment);

                                    c1SinglePayment.SetData(_RowIndex, COL_TRANSACTION_DATE, oPaymentPatientClaim.BillingTransactionDate);
                                    c1SinglePayment.SetData(_RowIndex, COL_TRACK_TRN_ID, oPaymentPatientClaim.CliamLines[j].TrackBLTransactionID);
                                    c1SinglePayment.SetData(_RowIndex, COL_TRACK_TRN_DTL_ID, oPaymentPatientClaim.CliamLines[j].TrackBLTransactionDetailID);
                                    c1SinglePayment.SetData(_RowIndex, COL_SUB_CLAIM_NO, oPaymentPatientClaim.CliamLines[j].SubClaimNumber);
                                    c1SinglePayment.SetData(_RowIndex,COL_NON_SERVICECODE, oPaymentPatientClaim.CliamLines[j].bNonServiceCode);
                                    //c1SinglePayment.SetData(_RowIndex - 1 , COL_HOLD, oPaymentPatientClaim.CliamLines[j].ClaimOnHold);

                                    if (c1SinglePayment.GetData(_RowIndex - 1, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(_RowIndex - 1, COL_SERVICELINE_TYPE) == ColServiceLineType.Claim)
                                    {
                                        c1SinglePayment.SetData(_RowIndex - 1, COL_HOLD, oPaymentPatientClaim.CliamLines[j].ClaimOnHold);
                                    }
                                    #endregion
                                    #region "Allow Editing"
                                    c1SinglePayment.SetCellStyle(_RowIndex, COL_CUR_PAYMENT, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                                    c1SinglePayment.SetCellStyle(_RowIndex, COL_CUR_ADJ_TYPECODE, c1SinglePayment.Styles["cs_EditableAdjustment"]);
                                    c1SinglePayment.SetCellStyle(_RowIndex, COL_CUR_ADJ_TYPEDESCRIPTION, c1SinglePayment.Styles["cs_EditableStringStyle"]);
                                    c1SinglePayment.SetCellStyle(_RowIndex, COL_CUR_ADJ_AMOUNT, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                                    #endregion

                                    if (objClsFeeSchedule != null)
                                    {
                                        objClsFeeSchedule.Dispose();
                                    }
                                }
                                c1SinglePayment.SetData(_ClaimStartRowIndex, COL_CELLRANGE_R2, c1SinglePayment.Rows.Count - 1);


                                #region "Calculate Total of Clims"
                                c1SinglePaymentTotal.SetData(0, COL_CHARGE, CalculateSinglePaymentTotal(COL_CHARGE));
                                c1SinglePaymentTotal.SetData(0, COL_TOTALCHARGE, CalculateSinglePaymentTotal(COL_TOTALCHARGE));
                                c1SinglePaymentTotal.SetData(0, COL_CUR_PAYMENT, CalculateSinglePaymentTotal(COL_CUR_PAYMENT));
                                c1SinglePaymentTotal.SetData(0, COL_CUR_ADJ_AMOUNT, CalculateSinglePaymentTotal(COL_CUR_ADJ_AMOUNT));
                                c1SinglePaymentTotal.SetData(0, COL_ALLOWED, CalculateSinglePaymentTotal(COL_ALLOWED));
                                c1SinglePaymentTotal.SetData(0, COL_PREV_PAID, CalculateSinglePaymentTotal(COL_PREV_PAID));
                                c1SinglePaymentTotal.SetData(0, COL_PREV_ADJ, CalculateSinglePaymentTotal(COL_PREV_ADJ));
                                c1SinglePaymentTotal.SetData(0, COL_PAT_DUE, CalculateSinglePaymentTotal(COL_PAT_DUE));
                                c1SinglePaymentTotal.SetData(0, COL_BadDebt_DUE, CalculateSinglePaymentTotal(COL_BadDebt_DUE));
                                c1SinglePaymentTotal.SetData(0, COL_CLM_BALANCE, CalculateSinglePaymentTotal(COL_CLM_BALANCE));
                                

                                #endregion
                            }
                        }
                    }
                    if (! IsBadDebtPatient && CalculateSinglePaymentTotal(COL_BadDebt_DUE) <= 0)
                    {
                        c1SinglePaymentTotal.Cols[COL_BadDebt_DUE].Visible = false;
                        c1SinglePayment.Cols[COL_BadDebt_DUE].Visible = false;
                    }
                }
                else
                {
                    #region "Clear Totals "
                    c1SinglePaymentTotal.SetData(0, COL_CHARGE, 0);
                    c1SinglePaymentTotal.SetData(0, COL_TOTALCHARGE, 0);
                    c1SinglePaymentTotal.SetData(0, COL_CUR_PAYMENT, 0);
                    c1SinglePaymentTotal.SetData(0, COL_CUR_ADJ_AMOUNT, 0);

                    c1SinglePaymentTotal.SetData(0, COL_PREV_PAID, 0);
                    c1SinglePaymentTotal.SetData(0, COL_PREV_ADJ, 0);
                    c1SinglePaymentTotal.SetData(0, COL_PAT_DUE, 0);
                    c1SinglePaymentTotal.SetData(0, COL_BadDebt_DUE, 0);
                    c1SinglePaymentTotal.SetData(0, COL_CLM_BALANCE, 0);
                    if (!IsBadDebtPatient && CalculateSinglePaymentTotal(COL_BadDebt_DUE) <= 0)
                    {
                        c1SinglePaymentTotal.Cols[COL_BadDebt_DUE].Visible = false;
                        c1SinglePayment.Cols[COL_BadDebt_DUE].Visible = false;
                    }
                    #endregion
                }
                #endregion

                #region " Set the style for the total grid "

                C1.Win.C1FlexGrid.CellStyle csTotalHeader;// = c1SinglePaymentTotal.Styles.Add("cs_TotalHeader");
                try
                {
                    if (c1SinglePaymentTotal.Styles.Contains("cs_TotalHeader"))
                    {
                        csTotalHeader = c1SinglePaymentTotal.Styles["cs_TotalHeader"];
                    }
                    else
                    {
                        csTotalHeader = c1SinglePaymentTotal.Styles.Add("cs_TotalHeader");
                        csTotalHeader.DataType = typeof(System.String);
                        //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                        //replace declaration for create new font by font variable.
                        csTotalHeader.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csTotalHeader.ForeColor = Color.Maroon;
                        csTotalHeader.TextAlign = TextAlignEnum.RightCenter;
                    }

                }
                catch
                {
                    csTotalHeader = c1SinglePaymentTotal.Styles.Add("cs_TotalHeader");
                    csTotalHeader.DataType = typeof(System.String);
                    //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                    //replace declaration for create new font by font variable.
                    csTotalHeader.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csTotalHeader.ForeColor = Color.Maroon;
                    csTotalHeader.TextAlign = TextAlignEnum.RightCenter;

                }
         
                c1SinglePaymentTotal.Styles[CellStyleEnum.Fixed].ForeColor = Color.Maroon;
                c1SinglePaymentTotal.Styles[CellStyleEnum.Alternate].ForeColor = Color.Maroon;
                c1SinglePaymentTotal.SetData(0, COL_CPT_CODE, "Total : ");
                c1SinglePaymentTotal.SetCellStyle(0, COL_CPT_CODE, csTotalHeader);

                #endregion " Set the style for the total grid "
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _IsPaymentGridLoading = false;

                #region "Set Index"
                if (_FocusRowIndex > 0)
                {
                    c1SinglePayment.Focus();
                    c1SinglePayment.Select(_FocusRowIndex, COL_CUR_PAYMENT, true);
                }
                #endregion

                c1SinglePayment.Redraw = true;
                c1SinglePayment.ScrollBars = ScrollBars.Vertical;
                if (oPaymentPatientClaim != null) { oPaymentPatientClaim.Dispose(); }
                if (oPaymentPatientClaims != null) { oPaymentPatientClaims.Dispose(); }
                if (objClsFeeSchedule != null)
                {
                    objClsFeeSchedule.Dispose();
                }
                //SLR: Free ogloPatientPaymentV2
                if (ogloPatientPaymentV2!=null)
                {
                    ogloPatientPaymentV2.Dispose();
                }
            }
        }

        private void FillBillingTransaction(Int64 nPatientID, bool bProcessOnce)
        {
            gloAccountsV2.gloPatientPaymentV2 ogloPatientPaymentV2 = new gloAccountsV2.gloPatientPaymentV2();
            gloAccountsV2.PaymentCollection.PaymentPatientClaim oPaymentPatientClaim = new gloAccountsV2.PaymentCollection.PaymentPatientClaim();
            gloAccountsV2.PaymentCollection.PaymentPatientClaims oPaymentPatientClaims = new gloAccountsV2.PaymentCollection.PaymentPatientClaims();
            ClsFeeSchedule objClsFeeSchedule = new ClsFeeSchedule(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            int _FocusRowIndex = 0;
            int _ClaimStartRowIndex = 0;
      //      Int64 TransactionDate = 0;
            Boolean IsBadDebtPatient = false;

            try
            {
                if (bProcessOnce) // Subashish
                {
                    _IsPaymentGridLoading = true;
                    DesignPaymentGrid(c1SinglePayment);

                    c1SinglePayment.Redraw = false;
                    c1SinglePayment.ScrollBars = ScrollBars.None;
                }
                if (oPatientControl.IsAllAccPatSelected)
                {
                    IsBadDebtPatient = gloPatientPaymentV2.IsBadDebtPatient(nPAccountID, 0);
                }
                else
                {
                    IsBadDebtPatient = gloPatientPaymentV2.IsBadDebtPatient(nPAccountID, _PatientID);
                }
                #region "Get Billied Transaction"
                if (IsFromCleargagePosting && gloGlobal.gloPMGlobal.IsCleargageEnable)
                {
                    oPaymentPatientClaims = ogloPatientPaymentV2.Cleargage_GetBillingTransaction_PAF(0, _PatientID, Convert.ToInt64(dtCleargagePaymentDetails.Rows[0]["nFileID"]), Convert.ToString(dtCleargagePaymentDetails.Rows[0]["EncounterID"]), ShowZeroBalanceClaims, ShowBadDebtBalanceClaims);
                }
                else
                {
                    oPaymentPatientClaims = ogloPatientPaymentV2.GetBillingTransaction(nPatientID, ShowZeroBalanceClaims, ShowBadDebtBalanceClaims);
                }

                #endregion

                #region "Fill Billed Transaction"

                if (oPaymentPatientClaims != null && oPaymentPatientClaims.Count > 0)
                {
                    for (int x = 0; x < oPaymentPatientClaims.Count; x++)
                    {
                        if (oPaymentPatientClaims[x] != null)
                        {
                            oPaymentPatientClaim = oPaymentPatientClaims[x];
                            if (oPaymentPatientClaim.CliamLines.Count > 0)
                            {
                                #region "Master Data"
                                //Dta = oPaymentPatientClaim.BillingTransactionDate;
                                c1SinglePayment.Rows.Add();
                                c1SinglePayment.Rows[c1SinglePayment.Rows.Count - 1].AllowEditing = false;

                                //Subashish to accomodate PAF
                                if (_IsPatientAccountFeature)
                                {
                                    c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_PATIENTID, oPaymentPatientClaim.PatientID);
                                    c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_PATIENTNAME, oPaymentPatientClaim.PatientName);

                                }
                                //end Subashish

                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMDISPNO, oPaymentPatientClaim.DisplayClaimNo);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMNO, oPaymentPatientClaim.ClaimNo);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMSUBCLAIM_NO, oPaymentPatientClaim.DisplayClaimNo);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_BILLING_TRANSACTON_ID, oPaymentPatientClaim.BillingTransactionID);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_SERVICELINE_TYPE, ColServiceLineType.Claim);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_PAY_EOBID, 0);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_TRANSACTION_DATE, oPaymentPatientClaim.BillingTransactionDate);
                                c1SinglePayment.Rows[c1SinglePayment.Rows.Count - 1].Style = c1SinglePayment.Styles["cs_ClaimRowStyle"];

                                //Code Added by Subashish
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CRESP_PARTY, oPaymentPatientClaim.RespParty);
                                //Code Added by Subashish
                                _ClaimStartRowIndex = c1SinglePayment.Rows.Count - 1;
                                c1SinglePayment.SetData(_ClaimStartRowIndex, COL_CELLRANGE_R1, c1SinglePayment.Rows.Count - 1);

                                #endregion

                                for (int j = 0; j <= oPaymentPatientClaim.CliamLines.Count - 1; j++)
                                {
                                    c1SinglePayment.Rows.Add();
                                    int _RowIndex = c1SinglePayment.Rows.Count - 1;
                                    if (_FocusRowIndex == 0) { _FocusRowIndex = _RowIndex; }

                                    #region "Service Lines"

                                    c1SinglePayment.SetData(_RowIndex, COL_GENERAL, "");
                                    c1SinglePayment.SetData(_RowIndex, COL_PATIENTID, oPaymentPatientClaim.CliamLines[j].PatientID);
                                    c1SinglePayment.SetData(_RowIndex, COL_PATIENTNAME, "");
                                    c1SinglePayment.SetData(_RowIndex, COL_CRESP_PARTY, "");
                                    //c1SinglePayment.SetData(_RowIndex, COL_CRESP_PARTY, oPaymentPatientClaim.CliamLines[j].RespParty);

                                    c1SinglePayment.SetData(_RowIndex, COL_CLAIMNO, oPaymentPatientClaim.CliamLines[j].ClaimNumber);
                                    //c1SinglePayment.SetData(_RowIndex, COL_CLAIMDISPNO, oPaymentPatientClaim.DisplayClaimNo);
                                    c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMSUBCLAIM_NO, oPaymentPatientClaim.DisplayClaimNo);

                                    c1SinglePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_ID, oPaymentPatientClaim.CliamLines[j].BLTransactionID);
                                    c1SinglePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_DETAILID, oPaymentPatientClaim.CliamLines[j].BLTransactionDetailID);
                                    c1SinglePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_LINENO, oPaymentPatientClaim.CliamLines[j].BLTransactionLineNo);

                                    c1SinglePayment.SetData(_RowIndex, COL_PAYMENT_NO, j + 1);
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_DATE, "");
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_EOBPAYMENTID, ""); //TO DO
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_EOBID, 0);
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_EOBDTLID, 0);
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_EOBPAYMENTDTLID, 0);

                                    c1SinglePayment.SetData(_RowIndex, COL_DOS_FROM, gloDateMaster.gloDate.DateAsDate(oPaymentPatientClaim.CliamLines[j].DOSFrom).ToShortDateString());
                                    c1SinglePayment.SetData(_RowIndex, COL_DOS_TO, gloDateMaster.gloDate.DateAsDate(oPaymentPatientClaim.CliamLines[j].DOSTo).ToShortDateString());
                                    c1SinglePayment.SetData(_RowIndex, COL_CPT_CODE, oPaymentPatientClaim.CliamLines[j].CPTCode);
                                    c1SinglePayment.SetData(_RowIndex, COL_CPT_DESCRIPTON, oPaymentPatientClaim.CliamLines[j].CPTDescription);
                                    c1SinglePayment.SetData(_RowIndex, COL_MODIFIER, oPaymentPatientClaim.CliamLines[j].Modifiers);

                                    c1SinglePayment.SetData(_RowIndex, COL_CHARGE, oPaymentPatientClaim.CliamLines[j].Charges);
                                    if (oPaymentPatientClaim.CliamLines[j].IsNullAllowed)
                                    {
                                        Boolean bHasAllowedAmt = false;
                                       
                                        decimal _dAllowedAmount = objClsFeeSchedule.GetAllowedAmount(oPaymentPatientClaim.BillingTransactionID, oPaymentPatientClaim.CliamLines[j].CPTCode, oPaymentPatientClaim.CliamLines[j].Modifiers, oPaymentPatientClaim.FacilityType, ref bHasAllowedAmt, oPaymentPatientClaim.CliamLines[j].DOSFrom);
                                        if (bHasAllowedAmt)
                                        {
                                            c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, _dAllowedAmount * oPaymentPatientClaim.CliamLines[j].Unit);
                                        }
                                        else
                                        {
                                            c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, null);
                                        }
                                    }
                                    else
                                        c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, oPaymentPatientClaim.CliamLines[j].Allowed);
                                    c1SinglePayment.SetData(_RowIndex, COL_UNIT, oPaymentPatientClaim.CliamLines[j].Unit);
                                    c1SinglePayment.SetData(_RowIndex, COL_TOTALCHARGE, oPaymentPatientClaim.CliamLines[j].TotalCharges);
                                    c1SinglePayment.SetData(_RowIndex, COL_CUR_PAYMENT, "");

                                    c1SinglePayment.SetData(_RowIndex, COL_PREV_PAID, oPaymentPatientClaim.CliamLines[j].LinePreviousPaid);
                                    c1SinglePayment.SetData(_RowIndex, COL_PREV_ADJ, oPaymentPatientClaim.CliamLines[j].LinePreviousAdjuestment);


                                    c1SinglePayment.SetData(_RowIndex, COL_PAT_DUE, oPaymentPatientClaim.CliamLines[j].LinePatientDue);
                                    c1SinglePayment.SetData(_RowIndex, COL_BadDebt_DUE, oPaymentPatientClaim.CliamLines[j].LineBadDebtDue);
                                    c1SinglePayment.SetData(_RowIndex, COL_CLM_BALANCE, oPaymentPatientClaim.CliamLines[j].LineBalance);


                                    c1SinglePayment.SetData(_RowIndex, COL_SERVICELINE_TYPE, ColServiceLineType.ServiceLine);
                                    c1SinglePayment.SetData(_RowIndex, COL_ISOPENFORMODIFY, "");
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_CLINICID, 0);
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_LINESTATUS, "");
                                    
                                    c1SinglePayment.SetData(_RowIndex, COL_PREV_PATIENTPAYMENT_AMT, oPaymentPatientClaim.CliamLines[j].LinePreviousPatientPaid);
                                    c1SinglePayment.SetData(_RowIndex, COL_PREV_PATIENTADJUSTMENT_AMT, oPaymentPatientClaim.CliamLines[j].LinePrevPatientAdjustment);

                                    c1SinglePayment.SetData(_RowIndex, COL_TRANSACTION_DATE, oPaymentPatientClaim.BillingTransactionDate);
                                    c1SinglePayment.SetData(_RowIndex, COL_TRACK_TRN_ID, oPaymentPatientClaim.CliamLines[j].TrackBLTransactionID);
                                    c1SinglePayment.SetData(_RowIndex, COL_TRACK_TRN_DTL_ID, oPaymentPatientClaim.CliamLines[j].TrackBLTransactionDetailID);
                                    c1SinglePayment.SetData(_RowIndex, COL_SUB_CLAIM_NO, oPaymentPatientClaim.CliamLines[j].SubClaimNumber);
                                    c1SinglePayment.SetData(_RowIndex, COL_NON_SERVICECODE, oPaymentPatientClaim.CliamLines[j].bNonServiceCode);
                                    if (c1SinglePayment.GetData(_RowIndex - 1, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(_RowIndex - 1, COL_SERVICELINE_TYPE) == ColServiceLineType.Claim)
                                    {
                                        c1SinglePayment.SetData(_RowIndex - 1, COL_HOLD, oPaymentPatientClaim.CliamLines[j].ClaimOnHold);
                                    }


                                    #endregion
                                    #region "Allow Editing"
                                    c1SinglePayment.SetCellStyle(_RowIndex, COL_CUR_PAYMENT, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                                    c1SinglePayment.SetCellStyle(_RowIndex, COL_CUR_ADJ_TYPECODE, c1SinglePayment.Styles["cs_EditableAdjustment"]);
                                    c1SinglePayment.SetCellStyle(_RowIndex, COL_CUR_ADJ_TYPEDESCRIPTION, c1SinglePayment.Styles["cs_EditableStringStyle"]);
                                    c1SinglePayment.SetCellStyle(_RowIndex, COL_CUR_ADJ_AMOUNT, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                                    #endregion

                                      
                                }
                                c1SinglePayment.SetData(_ClaimStartRowIndex, COL_CELLRANGE_R2, c1SinglePayment.Rows.Count - 1);


                                #region "Calculate Total of Clims"
                                c1SinglePaymentTotal.SetData(0, COL_CHARGE, CalculateSinglePaymentTotal(COL_CHARGE));
                                c1SinglePaymentTotal.SetData(0, COL_TOTALCHARGE, CalculateSinglePaymentTotal(COL_TOTALCHARGE));
                                c1SinglePaymentTotal.SetData(0, COL_CUR_PAYMENT, CalculateSinglePaymentTotal(COL_CUR_PAYMENT));
                                c1SinglePaymentTotal.SetData(0, COL_CUR_ADJ_AMOUNT, CalculateSinglePaymentTotal(COL_CUR_ADJ_AMOUNT));
                                c1SinglePaymentTotal.SetData(0, COL_ALLOWED, CalculateSinglePaymentTotal(COL_ALLOWED));
                                c1SinglePaymentTotal.SetData(0, COL_PREV_PAID, CalculateSinglePaymentTotal(COL_PREV_PAID));
                                c1SinglePaymentTotal.SetData(0, COL_PREV_ADJ, CalculateSinglePaymentTotal(COL_PREV_ADJ));
                                c1SinglePaymentTotal.SetData(0, COL_PAT_DUE, CalculateSinglePaymentTotal(COL_PAT_DUE));
                                c1SinglePaymentTotal.SetData(0, COL_BadDebt_DUE, CalculateSinglePaymentTotal(COL_BadDebt_DUE));
                                c1SinglePaymentTotal.SetData(0, COL_CLM_BALANCE, CalculateSinglePaymentTotal(COL_CLM_BALANCE));
                                #endregion
                            }
                        }
                    }
                    if (!IsBadDebtPatient && CalculateSinglePaymentTotal(COL_BadDebt_DUE) <= 0)
                    {
                        c1SinglePaymentTotal.Cols[COL_BadDebt_DUE].Visible = false;
                        c1SinglePayment.Cols[COL_BadDebt_DUE].Visible = false;
                    }
                }
                else
                {
                    #region "Clear Totals "
                    c1SinglePaymentTotal.SetData(0, COL_CHARGE, 0);
                    c1SinglePaymentTotal.SetData(0, COL_TOTALCHARGE, 0);
                    c1SinglePaymentTotal.SetData(0, COL_CUR_PAYMENT, 0);
                    c1SinglePaymentTotal.SetData(0, COL_CUR_ADJ_AMOUNT, 0);

                    c1SinglePaymentTotal.SetData(0, COL_PREV_PAID, 0);
                    c1SinglePaymentTotal.SetData(0, COL_PREV_ADJ, 0);
                    c1SinglePaymentTotal.SetData(0, COL_PAT_DUE, 0);
                    c1SinglePaymentTotal.SetData(0, COL_BadDebt_DUE, 0);
                    c1SinglePaymentTotal.SetData(0, COL_CLM_BALANCE, 0);
                    if (!IsBadDebtPatient && CalculateSinglePaymentTotal(COL_BadDebt_DUE) <= 0)
                    {
                        c1SinglePaymentTotal.Cols[COL_BadDebt_DUE].Visible = false;
                        c1SinglePayment.Cols[COL_BadDebt_DUE].Visible = false;
                    }
                    #endregion
                }
                #endregion

                #region " Set the style for the total grid "

                C1.Win.C1FlexGrid.CellStyle csTotalHeader;// = c1SinglePaymentTotal.Styles.Add("cs_TotalHeader");
                try
                {
                    if (c1SinglePaymentTotal.Styles.Contains("cs_TotalHeader"))
                    {
                        csTotalHeader = c1SinglePaymentTotal.Styles["cs_TotalHeader"];
                    }
                    else
                    {
                        csTotalHeader = c1SinglePaymentTotal.Styles.Add("cs_TotalHeader");
                        csTotalHeader.DataType = typeof(System.String);
                        //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                        //replace declaration for create new font by font variable.
                        csTotalHeader.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csTotalHeader.ForeColor = Color.Maroon;
                        csTotalHeader.TextAlign = TextAlignEnum.RightCenter;
                    }

                }
                catch
                {
                    csTotalHeader = c1SinglePaymentTotal.Styles.Add("cs_TotalHeader");
                    csTotalHeader.DataType = typeof(System.String);
                    //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                    //replace declaration for create new font by font variable.
                    csTotalHeader.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csTotalHeader.ForeColor = Color.Maroon;
                    csTotalHeader.TextAlign = TextAlignEnum.RightCenter;

                }
             
                c1SinglePaymentTotal.Styles[CellStyleEnum.Fixed].ForeColor = Color.Maroon;
                c1SinglePaymentTotal.Styles[CellStyleEnum.Alternate].ForeColor = Color.Maroon;
                c1SinglePaymentTotal.SetData(0, COL_CPT_CODE, "Total : ");
                c1SinglePaymentTotal.SetCellStyle(0, COL_CPT_CODE, csTotalHeader);

                #endregion " Set the style for the total grid "
                if (objClsFeeSchedule != null)
                {
                    objClsFeeSchedule.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _IsPaymentGridLoading = false;

                #region "Set Index"
                if (_FocusRowIndex > 0)
                {
                    c1SinglePayment.Focus();
                    c1SinglePayment.Select(_FocusRowIndex, COL_CUR_PAYMENT, true);
                }
                #endregion

                c1SinglePayment.Redraw = true;
                c1SinglePayment.ScrollBars = ScrollBars.Vertical;
                if (oPaymentPatientClaim != null) { oPaymentPatientClaim.Dispose(); }
                if (oPaymentPatientClaims != null) { oPaymentPatientClaims.Dispose(); }
                if (objClsFeeSchedule != null)
                {
                    objClsFeeSchedule.Dispose();
                }
                //SLR: Free ogloPatientPaymentV2
                if (ogloPatientPaymentV2 != null)
                {
                    ogloPatientPaymentV2.Dispose();
                }
            }
        }

        private void FillBillingTransactionAccountPatient(Int64 PatientId, Int64 nPAccountID)
        {
            gloAccountsV2.gloPatientPaymentV2 ogloPatientPaymentV2 = new gloPatientPaymentV2();
            gloAccountsV2.PaymentCollection.PaymentPatientClaim oPaymentPatientClaim = new gloAccountsV2.PaymentCollection.PaymentPatientClaim();
            gloAccountsV2.PaymentCollection.PaymentPatientClaims oPaymentPatientClaims = new gloAccountsV2.PaymentCollection.PaymentPatientClaims();
            ClsFeeSchedule  objClsFeeSchedule = new ClsFeeSchedule(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            int _FocusRowIndex = 0;
            int _ClaimStartRowIndex = 0;
         //   Int64 TransactionDate = 0;
            Boolean IsBadDebtPatient = false;
            try
            {
                _IsPaymentGridLoading = true;
                DesignPaymentGrid(c1SinglePayment);
                if (oPatientControl.IsAllAccPatSelected)
                {
                    IsBadDebtPatient = gloPatientPaymentV2.IsBadDebtPatient(nPAccountID, 0);
                }
                else
                {
                    IsBadDebtPatient = gloPatientPaymentV2.IsBadDebtPatient(nPAccountID, _PatientID);
                }
                c1SinglePayment.Redraw = false;
                c1SinglePayment.ScrollBars = ScrollBars.None;

                #region "Get Billied Transaction"
                if (IsFromCleargagePosting && gloGlobal.gloPMGlobal.IsCleargageEnable)
                {
                    oPaymentPatientClaims = ogloPatientPaymentV2.Cleargage_GetBillingTransaction_PAF(nPAccountID, PatientId, Convert.ToInt64(dtCleargagePaymentDetails.Rows[0]["nFileID"]), Convert.ToString(dtCleargagePaymentDetails.Rows[0]["EncounterID"]), ShowZeroBalanceClaims, ShowBadDebtBalanceClaims);
                }
                else
                {
                    oPaymentPatientClaims = ogloPatientPaymentV2.GetBillingTransaction_PAF(nPAccountID, PatientId, ShowZeroBalanceClaims, ShowBadDebtBalanceClaims);
                }
               
                #endregion

                #region "Fill Billed Transaction"

                if (oPaymentPatientClaims != null && oPaymentPatientClaims.Count > 0)
                {
                    for (int x = 0; x < oPaymentPatientClaims.Count; x++)
                    {
                        if (oPaymentPatientClaims[x] != null)
                        {
                            oPaymentPatientClaim = oPaymentPatientClaims[x];
                            if (oPaymentPatientClaim.CliamLines.Count > 0)
                            {
                                #region "Master Data"
                                //Dta = oPaymentPatientClaim.BillingTransactionDate;
                                c1SinglePayment.Rows.Add();
                                c1SinglePayment.Rows[c1SinglePayment.Rows.Count - 1].AllowEditing = false;

                                //Subashish Patient   
                                if (_IsPatientAccountFeature)
                                {
                                    c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_PATIENTNAME, oPaymentPatientClaim.PatientName);
                                }
                                //end Subashish

                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMDISPNO, oPaymentPatientClaim.DisplayClaimNo);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMNO, oPaymentPatientClaim.ClaimNo);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMSUBCLAIM_NO, oPaymentPatientClaim.DisplayClaimNo);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_BILLING_TRANSACTON_ID, oPaymentPatientClaim.BillingTransactionID);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_SERVICELINE_TYPE, ColServiceLineType.Claim);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CRESP_PARTY, oPaymentPatientClaim.RespParty);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_PAY_EOBID, 0);
                                c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_TRANSACTION_DATE, oPaymentPatientClaim.BillingTransactionDate);
                                c1SinglePayment.Rows[c1SinglePayment.Rows.Count - 1].Style = c1SinglePayment.Styles["cs_ClaimRowStyle"];

                                _ClaimStartRowIndex = c1SinglePayment.Rows.Count - 1;
                                c1SinglePayment.SetData(_ClaimStartRowIndex, COL_CELLRANGE_R1, c1SinglePayment.Rows.Count - 1);

                                #endregion

                                for (int j = 0; j <= oPaymentPatientClaim.CliamLines.Count - 1; j++)
                                {
                                    c1SinglePayment.Rows.Add();
                                    int _RowIndex = c1SinglePayment.Rows.Count - 1;
                                    if (_FocusRowIndex == 0) { _FocusRowIndex = _RowIndex; }

                                    #region "Service Lines"

                                    c1SinglePayment.SetData(_RowIndex, COL_GENERAL, "");
                                    c1SinglePayment.SetData(_RowIndex, COL_PATIENTID, oPaymentPatientClaim.CliamLines[j].PatientID);
                                    c1SinglePayment.SetData(_RowIndex, COL_PATIENTNAME, "");

                                    c1SinglePayment.SetData(_RowIndex, COL_CLAIMNO, oPaymentPatientClaim.CliamLines[j].ClaimNumber);
                                    //c1SinglePayment.SetData(_RowIndex, COL_CLAIMDISPNO, oPaymentPatientClaim.DisplayClaimNo);
                                    c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMSUBCLAIM_NO, oPaymentPatientClaim.DisplayClaimNo);

                                    c1SinglePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_ID, oPaymentPatientClaim.CliamLines[j].BLTransactionID);
                                    c1SinglePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_DETAILID, oPaymentPatientClaim.CliamLines[j].BLTransactionDetailID);
                                    c1SinglePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_LINENO, oPaymentPatientClaim.CliamLines[j].BLTransactionLineNo);

                                    c1SinglePayment.SetData(_RowIndex, COL_PAYMENT_NO, j + 1);
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_DATE, "");
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_EOBPAYMENTID, ""); //TO DO
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_EOBID, 0);
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_EOBDTLID, 0);
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_EOBPAYMENTDTLID, 0);

                                    c1SinglePayment.SetData(_RowIndex, COL_DOS_FROM, gloDateMaster.gloDate.DateAsDate(oPaymentPatientClaim.CliamLines[j].DOSFrom).ToShortDateString());
                                    c1SinglePayment.SetData(_RowIndex, COL_DOS_TO, gloDateMaster.gloDate.DateAsDate(oPaymentPatientClaim.CliamLines[j].DOSTo).ToShortDateString());
                                    c1SinglePayment.SetData(_RowIndex, COL_CPT_CODE, oPaymentPatientClaim.CliamLines[j].CPTCode);
                                    c1SinglePayment.SetData(_RowIndex, COL_CPT_DESCRIPTON, oPaymentPatientClaim.CliamLines[j].CPTDescription);
                                    c1SinglePayment.SetData(_RowIndex, COL_MODIFIER, oPaymentPatientClaim.CliamLines[j].Modifiers);

                                    c1SinglePayment.SetData(_RowIndex, COL_CHARGE, oPaymentPatientClaim.CliamLines[j].Charges);
                                    Boolean bHasAllowedAmt = false;
                                    if (oPaymentPatientClaim.CliamLines[j].IsNullAllowed)
                                    {

                                        decimal _dAllowedAmount = objClsFeeSchedule.GetAllowedAmount(oPaymentPatientClaim.BillingTransactionID, oPaymentPatientClaim.CliamLines[j].CPTCode, oPaymentPatientClaim.CliamLines[j].Modifiers, oPaymentPatientClaim.FacilityType, ref bHasAllowedAmt, oPaymentPatientClaim.CliamLines[j].DOSFrom);

                                        if (bHasAllowedAmt)
                                        {
                                            c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, _dAllowedAmount * oPaymentPatientClaim.CliamLines[j].Unit);
                                        }
                                        else
                                        {
                                            c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, null);
                                        }
                                    }
                                    else
                                        c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, oPaymentPatientClaim.CliamLines[j].Allowed);

                                    c1SinglePayment.SetData(_RowIndex, COL_UNIT, oPaymentPatientClaim.CliamLines[j].Unit);
                                    c1SinglePayment.SetData(_RowIndex, COL_TOTALCHARGE, oPaymentPatientClaim.CliamLines[j].TotalCharges);
                                    c1SinglePayment.SetData(_RowIndex, COL_CUR_PAYMENT, "");

                                    c1SinglePayment.SetData(_RowIndex, COL_PREV_PAID, oPaymentPatientClaim.CliamLines[j].LinePreviousPaid);
                                    c1SinglePayment.SetData(_RowIndex, COL_PREV_ADJ, oPaymentPatientClaim.CliamLines[j].LinePreviousAdjuestment);


                                    c1SinglePayment.SetData(_RowIndex, COL_PAT_DUE, oPaymentPatientClaim.CliamLines[j].LinePatientDue);
                                    c1SinglePayment.SetData(_RowIndex, COL_BadDebt_DUE, oPaymentPatientClaim.CliamLines[j].LineBadDebtDue);
                                    c1SinglePayment.SetData(_RowIndex, COL_CLM_BALANCE, oPaymentPatientClaim.CliamLines[j].LineBalance);


                                    c1SinglePayment.SetData(_RowIndex, COL_SERVICELINE_TYPE, ColServiceLineType.ServiceLine);
                                    c1SinglePayment.SetData(_RowIndex, COL_ISOPENFORMODIFY, "");
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_CLINICID, 0);
                                    c1SinglePayment.SetData(_RowIndex, COL_PAY_LINESTATUS, "");
                                    
                                    c1SinglePayment.SetData(_RowIndex, COL_PREV_PATIENTPAYMENT_AMT, oPaymentPatientClaim.CliamLines[j].LinePreviousPatientPaid);
                                    c1SinglePayment.SetData(_RowIndex, COL_PREV_PATIENTADJUSTMENT_AMT, oPaymentPatientClaim.CliamLines[j].LinePrevPatientAdjustment);

                                    c1SinglePayment.SetData(_RowIndex, COL_TRANSACTION_DATE, oPaymentPatientClaim.BillingTransactionDate);

                                    c1SinglePayment.SetData(_RowIndex, COL_TRACK_TRN_ID, oPaymentPatientClaim.CliamLines[j].TrackBLTransactionID);
                                    c1SinglePayment.SetData(_RowIndex, COL_TRACK_TRN_DTL_ID, oPaymentPatientClaim.CliamLines[j].TrackBLTransactionDetailID);
                                    c1SinglePayment.SetData(_RowIndex, COL_SUB_CLAIM_NO, oPaymentPatientClaim.CliamLines[j].SubClaimNumber);
                                    //c1SinglePayment.SetData(_RowIndex - 1, COL_HOLD, oPaymentPatientClaim.CliamLines[j].ClaimOnHold);
                                    c1SinglePayment.SetData(_RowIndex, COL_NON_SERVICECODE, oPaymentPatientClaim.CliamLines[j].bNonServiceCode);
                                    if (c1SinglePayment.GetData(_RowIndex - 1, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(_RowIndex - 1, COL_SERVICELINE_TYPE) == ColServiceLineType.Claim)
                                    {
                                        c1SinglePayment.SetData(_RowIndex - 1, COL_HOLD, oPaymentPatientClaim.CliamLines[j].ClaimOnHold);
                                    }
                                    #endregion
                                    #region "Allow Editing"
                                    c1SinglePayment.SetCellStyle(_RowIndex, COL_CUR_PAYMENT, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                                    c1SinglePayment.SetCellStyle(_RowIndex, COL_CUR_ADJ_TYPECODE, c1SinglePayment.Styles["cs_EditableAdjustment"]);
                                    c1SinglePayment.SetCellStyle(_RowIndex, COL_CUR_ADJ_TYPEDESCRIPTION, c1SinglePayment.Styles["cs_EditableStringStyle"]);
                                    c1SinglePayment.SetCellStyle(_RowIndex, COL_CUR_ADJ_AMOUNT, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);

                                    #endregion

                                }
                                c1SinglePayment.SetData(_ClaimStartRowIndex, COL_CELLRANGE_R2, c1SinglePayment.Rows.Count - 1);
                                #region "Calculate Total of Clims"
                                c1SinglePaymentTotal.SetData(0, COL_CHARGE, CalculateSinglePaymentTotal(COL_CHARGE));
                                c1SinglePaymentTotal.SetData(0, COL_TOTALCHARGE, CalculateSinglePaymentTotal(COL_TOTALCHARGE));
                                c1SinglePaymentTotal.SetData(0, COL_CUR_PAYMENT, CalculateSinglePaymentTotal(COL_CUR_PAYMENT));
                                c1SinglePaymentTotal.SetData(0, COL_CUR_ADJ_AMOUNT, CalculateSinglePaymentTotal(COL_CUR_ADJ_AMOUNT));
                                c1SinglePaymentTotal.SetData(0, COL_ALLOWED, CalculateSinglePaymentTotal(COL_ALLOWED));
                                c1SinglePaymentTotal.SetData(0, COL_PREV_PAID, CalculateSinglePaymentTotal(COL_PREV_PAID));
                                c1SinglePaymentTotal.SetData(0, COL_PREV_ADJ, CalculateSinglePaymentTotal(COL_PREV_ADJ));
                                c1SinglePaymentTotal.SetData(0, COL_PAT_DUE, CalculateSinglePaymentTotal(COL_PAT_DUE));
                                c1SinglePaymentTotal.SetData(0, COL_BadDebt_DUE, CalculateSinglePaymentTotal(COL_BadDebt_DUE));
                                c1SinglePaymentTotal.SetData(0, COL_CLM_BALANCE, CalculateSinglePaymentTotal(COL_CLM_BALANCE));
                                #endregion
                            }
                        }
                    }
                    if (!IsBadDebtPatient && CalculateSinglePaymentTotal(COL_BadDebt_DUE) <= 0)
                    {
                        c1SinglePaymentTotal.Cols[COL_BadDebt_DUE].Visible = false;
                        c1SinglePayment.Cols[COL_BadDebt_DUE].Visible = false;
                    }
                }
                else
                {
                    //Added on 20100105 to clear Totals if no claim present against selected patient
                    #region "Clear Totals "
                    c1SinglePaymentTotal.SetData(0, COL_CHARGE, 0);
                    c1SinglePaymentTotal.SetData(0, COL_TOTALCHARGE, 0);
                    c1SinglePaymentTotal.SetData(0, COL_CUR_PAYMENT, 0);
                    c1SinglePaymentTotal.SetData(0, COL_CUR_ADJ_AMOUNT, 0);

                    c1SinglePaymentTotal.SetData(0, COL_PREV_PAID, 0);
                    c1SinglePaymentTotal.SetData(0, COL_PREV_ADJ, 0);
                    c1SinglePaymentTotal.SetData(0, COL_PAT_DUE, 0);
                    c1SinglePaymentTotal.SetData(0, COL_BadDebt_DUE, 0);
                    c1SinglePaymentTotal.SetData(0, COL_CLM_BALANCE, 0);
                    if (!IsBadDebtPatient && CalculateSinglePaymentTotal(COL_BadDebt_DUE) <= 0)
                    {
                        c1SinglePaymentTotal.Cols[COL_BadDebt_DUE].Visible = false;
                        c1SinglePayment.Cols[COL_BadDebt_DUE].Visible = false;
                    }
                    #endregion
                }
                #endregion

                #region " Set the style for the total grid "

                C1.Win.C1FlexGrid.CellStyle csTotalHeader ;//= c1SinglePaymentTotal.Styles.Add("cs_TotalHeader");
                try
                {
                    if (c1SinglePaymentTotal.Styles.Contains("cs_TotalHeader"))
                    {
                        csTotalHeader = c1SinglePaymentTotal.Styles["cs_TotalHeader"];
                    }
                    else
                    {
                        csTotalHeader = c1SinglePaymentTotal.Styles.Add("cs_TotalHeader");
                        csTotalHeader.DataType = typeof(System.String);
                        //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                        //replace declaration for create new font by font variable.
                        csTotalHeader.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csTotalHeader.ForeColor = Color.Maroon;
                        csTotalHeader.TextAlign = TextAlignEnum.RightCenter;
                    }

                }
                catch
                {
                    csTotalHeader = c1SinglePaymentTotal.Styles.Add("cs_TotalHeader");
                    csTotalHeader.DataType = typeof(System.String);
                    //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                    //replace declaration for create new font by font variable.
                    csTotalHeader.Font = Font_CellStyle; //new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csTotalHeader.ForeColor = Color.Maroon;
                    csTotalHeader.TextAlign = TextAlignEnum.RightCenter;

                }
   
                c1SinglePaymentTotal.Styles[CellStyleEnum.Fixed].ForeColor = Color.Maroon;
                c1SinglePaymentTotal.Styles[CellStyleEnum.Alternate].ForeColor = Color.Maroon;
                c1SinglePaymentTotal.SetData(0, COL_CPT_CODE, "Total : ");
                c1SinglePaymentTotal.SetCellStyle(0, COL_CPT_CODE, csTotalHeader);

                #endregion " Set the style for the total grid "

                //AllowEditValidation();
                if (objClsFeeSchedule != null)
                {
                    objClsFeeSchedule.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _IsPaymentGridLoading = false;

                #region "Set Index"
                if (_FocusRowIndex > 0)
                {
                    c1SinglePayment.Focus();
                    c1SinglePayment.Select(_FocusRowIndex, COL_CUR_PAYMENT, true);
                }
                #endregion

                c1SinglePayment.Redraw = true;
                c1SinglePayment.ScrollBars = ScrollBars.Vertical;
                if (oPaymentPatientClaim != null) { oPaymentPatientClaim.Dispose(); }
                if (oPaymentPatientClaims != null) { oPaymentPatientClaims.Dispose(); }
                if (objClsFeeSchedule != null)
                {
                    objClsFeeSchedule.Dispose();
                }
                //SLR: Free ogloPatientPaymentV2
                if (ogloPatientPaymentV2 != null)
                {
                    ogloPatientPaymentV2.Dispose();
                }
            }
        }

        private void AllowClaimsEdit()
        {
            gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
            Object _retValue = null;
            string _clsDate = "";
            Int64 _closeDate = 0;
            StringBuilder _ClaimNumbers = new StringBuilder();
            String FormattedClaimNo = "";
            String _ClaimValidationMesssage = "";
            Int32 _firstEditableRowIndex = 0;
            try
            {
                #region "GetCloseDate"
                MaskedTextBox mskDate = (MaskedTextBox)mskCloseDate;
                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (mskDate.Text == "")
                {
                    oSettings.GetSetting("PAYMENT_LASTCLOSEDATE", _UserId, _ClinicID, out _retValue);
                    //SLR: Move the dispose to finally block
                    //oSettings.Dispose();

                    if (_retValue != null && Convert.ToString(_retValue).Trim() != "")
                    {
                        try
                        { _clsDate = Convert.ToDateTime(Convert.ToString(_retValue).Trim()).ToString("MM/dd/yyyy"); }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            ex = null;
                            _clsDate = DateTime.Now.Date.ToString("MM/dd/yyyy");
                        }
                    }
                    else
                    { _clsDate = DateTime.Now.Date.ToString("MM/dd/yyyy"); }

                    if (_clsDate.Trim() != "")
                    {
                        if (ogloBilling.IsDayClosed(Convert.ToDateTime(_clsDate.Trim())) == true)
                        { _clsDate = ""; }
                        //SLR: Move the dispose to finally block
                        //ogloBilling.Dispose();
                    }
                    _closeDate = 0;
                }
                else
                {
                    mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                    if (mskCloseDate.MaskCompleted == true)
                    {
                        _closeDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                    }
                }
                #endregion "GetCloseDate"

                for (int i = 1; i <= c1SinglePayment.Rows.Count - 1; i++)
                {
                    Int64 _transactionDate = 0;
                    if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.Claim)
                    {
                        FormattedClaimNo = Convert.ToString(c1SinglePayment.GetData(i, COL_CLAIMDISPNO));
                    }

                    if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                    {
                        _transactionDate = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRANSACTION_DATE));
                        if (_transactionDate > _closeDate)
                        {
                            _IsEditable = true;
                            c1SinglePayment.Rows[i].AllowEditing = false;
                            c1SinglePayment.SetCellStyle(i, COL_CUR_PAYMENT, c1SinglePayment.Styles["csEditableStringStyle"]);
                            c1SinglePayment.SetCellStyle(i, COL_CUR_ADJ_TYPECODE, c1SinglePayment.Styles["csEditableStringStyle"]);
                            c1SinglePayment.SetCellStyle(i, COL_CUR_ADJ_TYPEDESCRIPTION, c1SinglePayment.Styles["csEditableStringStyle"]);
                            c1SinglePayment.SetCellStyle(i, COL_CUR_ADJ_AMOUNT, c1SinglePayment.Styles["csEditableStringStyle"]);
                            c1SinglePayment.SetData(i, COL_CUR_PAYMENT, null);
                            c1SinglePayment.SetData(i, COL_CUR_ADJ_AMOUNT, null);
                            c1SinglePayment.SetData(i, COL_CUR_ADJ_TYPECODE, null);
                            _ClaimNumbers.Append(FormattedClaimNo);
                            if (FormattedClaimNo != "")
                                _ClaimNumbers.Append(",");
                            FormattedClaimNo = "";
                        }
                        else
                        {
                            if (_firstEditableRowIndex == 0) { _firstEditableRowIndex = i; }
                            c1SinglePayment.Rows[i].AllowEditing = true;
                            c1SinglePayment.SetCellStyle(i, COL_CUR_ADJ_TYPECODE, c1SinglePayment.Styles["cs_EditableAdjustment"]);
                            c1SinglePayment.SetCellStyle(i, COL_CUR_PAYMENT, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                            c1SinglePayment.SetCellStyle(i, COL_CUR_ADJ_TYPEDESCRIPTION, c1SinglePayment.Styles["cs_EditableStringStyle"]);
                            c1SinglePayment.SetCellStyle(i, COL_CUR_ADJ_AMOUNT, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                        }
                    }
                }
                if (_ClaimNumbers.Length > 0)
                { _ClaimNumbers.Remove(_ClaimNumbers.Length - 1, 1); }
                _ClaimValidationMesssage = "Cannot save payment – Payment Close Date for Claim # " + _ClaimNumbers + " precedes Charge’s Close Date.";

                if (_IsEditable && _IsClaimDateMessageShown)
                {
                    if (_closeDate == 0 || (_closeDate != _afterCloseDateChanged))
                    {
                        _IsClaimDateMessageShown = true;

                        if (_ClaimNumbers.Length > 0)
                        {
                            MessageBox.Show(_ClaimValidationMesssage, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            _ClaimNumbers.Remove(0, _ClaimNumbers.Length);
                            _IsClaimDateMessageShown = false;
                        }
                    }
                }

                if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                {
                    if (_firstEditableRowIndex != 0)
                    {
                        if (c1SinglePayment.RowSel != _firstEditableRowIndex)
                        {
                            c1SinglePayment.RowSel = _firstEditableRowIndex;
                        }
                    }
                    else
                    { c1SinglePayment.RowSel = 2; }
                }

                //mnuPayment_NextServiceLine_Click(null,null);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oSettings!=null)
                {
                    oSettings.Dispose();
                }
                if (ogloBilling!=null)
                {
                    ogloBilling.Dispose();
                }
            }
        }

        private void FillPaymentTray()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloSecurity.gloValidateUser ogloValidateUser = new gloSecurity.gloValidateUser(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            Int64 _defaultTrayId = 0;
            Int64 _lastselectedTrayId = 0;
            Object _retVal = null;

            try
            {
                //if (dtCloseDayTray != null && dtCloseDayTray.Rows.Count > 0)
                //{
                #region " .... Get the last selected Payment tray ... "

                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                Object _retSettingValue = null;
                oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", _UserId, _ClinicID, out _retSettingValue);
                oSettings.Dispose();

                if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                { _lastselectedTrayId = Convert.ToInt64(_retSettingValue); }

                #endregion " .... Get the last selected Payment tray ... "

                #region " ... Get the default Payment Tray .... "

                _sqlQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID FROM BL_CloseDayTray " +
               " WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
               " AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0 AND bIsDefault = 'true' AND nUserID = " + _UserId + " AND nClinicID = " + _ClinicID + "";
                oDB.Connect(false);
                _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                oDB.Disconnect();

                if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                { _defaultTrayId = Convert.ToInt64(_retVal); }

                #endregion " ... Get the default Payment Tray .... "

                //...Load the last selected tray if present or else load the default tray
                //SLR: Free previously allocated memory before making new memory
                if (oDB!=null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                oDB.Connect(false);
                _retVal = new object();
                if (_lastselectedTrayId > 0)
                {
                    _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WHERE nCloseDayTrayID = " + _lastselectedTrayId + " and ISNULL(bIsActive,0) = 1 AND nClinicID = " + _ClinicID + "");
                    if (_retVal != null && _retVal.ToString().Trim().Length > 0)
                    {
                        lblPaymentTray.Text = _retVal.ToString(); ;
                        lblPaymentTray.Tag = _lastselectedTrayId;
                    }
                    else
                    {
                        _lastselectedTrayId = 0;
                        lblPaymentTray.Text = "";
                        lblPaymentTray.Tag = 0;
                    }
                }
                else
                {
                    _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WHERE nCloseDayTrayID = " + _defaultTrayId + " AND nClinicID = " + _ClinicID + "");
                    if (_retVal != null && _retVal.ToString().Trim().Length > 0)
                    {
                        lblPaymentTray.Text = _retVal.ToString(); ;
                        lblPaymentTray.Tag = _defaultTrayId;
                    }
                }
                oDB.Disconnect();
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (_retVal != null) { _retVal = null; }
                //SLR: Free ogloValidateUser
                if (ogloValidateUser!=null)
                {
                    ogloValidateUser.Dispose();
                }
            }
        }

        //commented function below new functions are written
        //GLO2011-0013738 : Patient Payment screen does not prompt to pick a tray
        // whenver payment tray load it will show last payment tray selected which is used for transaction.
        private void SetPaymentTrayPopup_Old()
        {

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloSecurity.gloValidateUser ogloValidateUser = new gloSecurity.gloValidateUser(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            Int64 _defaultTrayId = 0;
            Object _retVal = null;

            try
            {
                if (lblPaymentTray.Tag != null && lblPaymentTray.Tag.ToString().Trim().Length > 0)
                {
                    _sqlQuery = " SELECT ISNULL(nCloseDayTrayID,0) As nCloseDayTrayID FROM BL_CloseDayTray " +
                   " WHERE nCloseDayTrayID IS NOT NULL AND sDescription IS NOT NULL AND nCloseDayTrayID > 0 " +
                   " AND sDescription <> ''  AND ISNULL(bIsClosed,0) = 0 AND bIsDefault = 'true' AND nUserID = " + _UserId + " AND nClinicID = " + _ClinicID + "";

                    oDB.Connect(false);
                    _retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                    oDB.Disconnect();

                    if (_retVal != null && Convert.ToString(_retVal).Trim() != "" && Convert.ToInt64(_retVal) > 0)
                    {
                        _defaultTrayId = Convert.ToInt64(_retVal);

                        #region " Set last selected tray "

                        //...Check if the last selected tray is same as the default tray if yes set the 
                        //...last selected tray or else show pop to select the tray

                        gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                        Object _retSettingValue = null;
                        oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", _UserId, _ClinicID, out _retSettingValue);
                        oSettings.Dispose();

                        if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                        {
                            if (Convert.ToString(_retSettingValue).Trim() == _defaultTrayId.ToString())
                            {
                                //SLR: Before allocating free previous meemory
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                    oDB.Dispose();
                                    oDB = null;
                                }
                                oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                                oDB.Connect(false);
                                _retVal = new object();
                                _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WHERE nCloseDayTrayID = " + _defaultTrayId + " AND nClinicID = " + _ClinicID + "");
                                if (_retVal != null && _retVal.ToString().Trim().Length > 0)
                                {
                                    lblPaymentTray.Text = _retVal.ToString(); ;
                                    lblPaymentTray.Tag = _defaultTrayId;
                                }
                                oDB.Disconnect();
                            }
                            else
                            {
                                //...Show pop-up to select the Tray
                                frmBillingTraySelection ofrmBillingTraySelection = new frmBillingTraySelection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                                ofrmBillingTraySelection.WindowState = FormWindowState.Normal;
                                ofrmBillingTraySelection.StartPosition = FormStartPosition.CenterParent;
                                ofrmBillingTraySelection.IsChargeTray = false;
                                ofrmBillingTraySelection.ShowDialog(this);
                                if (ofrmBillingTraySelection.FormResult == DialogResult.OK)
                                {
                                    if (ofrmBillingTraySelection.SelectedTrayID > 0)
                                    {
                                        lblPaymentTray.Tag = ofrmBillingTraySelection.SelectedTrayID;
                                        lblPaymentTray.Text = ofrmBillingTraySelection.SelectedTrayName;
                                    }
                                }

                                ofrmBillingTraySelection.Dispose();
                            }
                        }
                        else
                        {
                            //SLR: Free previous memory
                            if (oDB != null)
                            {
                                oDB.Disconnect();
                                oDB.Dispose();
                                oDB = null;
                            }
                            oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                            oDB.Connect(false);
                            _retVal = new object();
                            _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WHERE nCloseDayTrayID = " + _defaultTrayId + " AND nClinicID = " + _ClinicID + "");
                            if (_retVal != null && _retVal.ToString().Trim().Length > 0)
                            {
                                lblPaymentTray.Text = _retVal.ToString(); ;
                                lblPaymentTray.Tag = _defaultTrayId;
                            }
                            oDB.Disconnect();
                        }

                        #endregion " Set last selected close date "
                    }
                    else
                    {
                        //...Is default is not present then select the last selected tray

                        gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                        Object _retSettingValue = null;
                        oSettings.GetSetting("PAYMENT_LASTCLOSETRAYID", _UserId, _ClinicID, out _retSettingValue);
                        oSettings.Dispose();

                        if (_retSettingValue != null && Convert.ToString(_retSettingValue).Trim() != "")
                        {
                            //SLR: before allocating free previous memeory
                            if (oDB != null)
                            {
                                oDB.Disconnect();
                                oDB.Dispose();
                                oDB = null;
                            }
                            oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                            oDB.Connect(false);
                            _retVal = new object();
                            _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WHERE nCloseDayTrayID = " + Convert.ToInt64(_retSettingValue) + " AND nClinicID = " + _ClinicID + "");
                            if (_retVal != null && _retVal.ToString().Trim().Length > 0)
                            {
                                lblPaymentTray.Text = _retVal.ToString(); ;
                                lblPaymentTray.Tag = Convert.ToInt64(_retSettingValue);
                            }
                            oDB.Disconnect();
                        }
                    }
                }
                else
                {
                    //...Show pop-up to select the Tray
                    frmBillingTraySelection ofrmBillingTraySelection = new frmBillingTraySelection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    ofrmBillingTraySelection.WindowState = FormWindowState.Normal;
                    ofrmBillingTraySelection.StartPosition = FormStartPosition.CenterParent;
                    ofrmBillingTraySelection.IsChargeTray = false;
                    ofrmBillingTraySelection.ShowDialog(this);
                    if (ofrmBillingTraySelection.FormResult == DialogResult.OK)
                    {
                        if (ofrmBillingTraySelection.SelectedTrayID > 0)
                        {
                            lblPaymentTray.Tag = ofrmBillingTraySelection.SelectedTrayID;
                            lblPaymentTray.Text = ofrmBillingTraySelection.SelectedTrayName;
                        }
                    }

                    ofrmBillingTraySelection.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {//SLR: Free ogloValidateUser
                if (ogloValidateUser!=null)
                {
                    ogloValidateUser.Dispose();
                }
                //SLR: before allocating free previous memeory
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
            }
        }

        //GLO2011-0013738 : Patient Payment screen does not prompt to pick a tray
        // whenver payment tray load it will show last payment tray selected which is used for transaction.
        private void SetPaymentTrayPopup()
        {
            try
            {
                Int64 _lastSelectedTrayID = BillingSettings.LastSelectedPaymentTrayID;
                Int64 _defaultTrayID = gloInsurancePaymentV2.GetDefaultPaymentTrayID();

                // Set default payment tray
                SelectedPaymentTray = gloInsurancePaymentV2.GetPaymentTrayDescription(_defaultTrayID);
                SelectedPaymentTrayID = _defaultTrayID;

                if (_lastSelectedTrayID > 0)
                {
                    if (gloInsurancePaymentV2.IsPaymentTrayActive(_lastSelectedTrayID))
                    {
                        if (!_lastSelectedTrayID.Equals(_defaultTrayID))
                        {
                            SelectedPaymentTray = gloInsurancePaymentV2.GetPaymentTrayDescription(_lastSelectedTrayID);
                            SelectedPaymentTrayID = _lastSelectedTrayID;
                            SelectPaymentTray();
                        }
                    }
                    else
                    {
                        SelectPaymentTray();
                    }
                }
                else
                {
                    SelectPaymentTray();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void SelectPaymentTray()
        {
            frmBillingTraySelection ofrmBillingTraySelection = new frmBillingTraySelection(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            try
            {
                //...Show pop-up to select the Tray
                ofrmBillingTraySelection.WindowState = FormWindowState.Normal;
                ofrmBillingTraySelection.StartPosition = FormStartPosition.CenterParent;
                ofrmBillingTraySelection.IsChargeTray = false;
                if(lblPaymentTray.Text != "")
                {
                    ofrmBillingTraySelection.IsPaymentTrayLoaded = true;
                    ofrmBillingTraySelection.LoadedPaymentTrayID = Convert.ToInt64(lblPaymentTray.Tag);
                    ofrmBillingTraySelection.LoadedPaymentTray = Convert.ToString(lblPaymentTray.Text);
                }
                ofrmBillingTraySelection.ShowDialog(this);
                
                toolTip1.SetToolTip(lblPaymentTray, null);

                // If payment tray Modified or selected from payment tray dialog then reflect the changes
                if (ofrmBillingTraySelection.FormResult == DialogResult.OK) 
                {
                    if (ofrmBillingTraySelection.SelectedTrayID > 0)
                    {
                        this.SelectedPaymentTray = ofrmBillingTraySelection.SelectedTrayName;
                        this.SelectedPaymentTrayID = ofrmBillingTraySelection.SelectedTrayID;

                        toolTip1.SetToolTip(lblPaymentTray, ofrmBillingTraySelection.SelectedTrayName);
                    }
                    else
                    {
                        this.SelectedPaymentTray = string.Empty;
                        this.SelectedPaymentTrayID = 0;
                    }
                }
                // If Payment Tray dialog closed and payment tray made inactivated then reflect the changes
                else if (ofrmBillingTraySelection.IsOperationPerformed) 
                {
                    if (ofrmBillingTraySelection.SelectedTrayID > 0)
                    {
                        this.SelectedPaymentTray = ofrmBillingTraySelection.SelectedTrayName;
                        this.SelectedPaymentTrayID = ofrmBillingTraySelection.SelectedTrayID;

                        toolTip1.SetToolTip(lblPaymentTray, ofrmBillingTraySelection.SelectedTrayName);
                    }
                    else
                    {
                        this.SelectedPaymentTray = string.Empty;
                        this.SelectedPaymentTrayID = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ofrmBillingTraySelection.Dispose();
            }
        }

        //end

        private void FillPaymentMode()
        {
            cmbPayMode.Items.Clear();
            cmbPayMode.Items.Add(PaymentMode.Cash.ToString());
            cmbPayMode.Items.Add(PaymentMode.Check.ToString());
            cmbPayMode.Items.Add(PaymentMode.CreditCard.ToString());
            cmbPayMode.Items.Add(PaymentMode.MoneyOrder.ToString());
            cmbPayMode.Items.Add(PaymentMode.EFT.ToString());
            cmbPayMode.Items.Add(PaymentMode.Voucher.ToString());
            MaskedTextBox mskDate = (MaskedTextBox)mskCloseDate;
            mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            for (int i = 0; i <= cmbPayMode.Items.Count - 1; i++)
            {
                if (cmbPayMode.Items[i].ToString() == PaymentMode.Check.ToString())
                {
                    cmbPayMode.SelectedIndex = i;
                    if (mskDate.Text == "")
                    {
                        mskCheckDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    }
                    else
                    { mskCheckDate.Text = mskCloseDate.Text; }
                    break;
                }
            }
            mskDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }

        private bool IsAdmin(Int64 UserId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable oDataTable = null;
            bool result = false;
            oDB.Connect(false);
            oDB.Retrive_Query("Select nAdministrator from User_MST where nUserID='" + UserId + "' and nAdministrator = 1", out oDataTable);
            if (oDataTable != null)
            {
                if (oDataTable.Rows.Count > 0)
                {
                    result = true;
                }
                oDataTable.Dispose();
            }
            
            oDB.Dispose();
            return result;


        }

        private object GetTagElement(string TagContent, Char Delimeter, Int64 Position)
        {
            //1. InsuranceID
            //2. InsuranceName
            //3 InsuraceSelfMode
            //4. ContactID

            string[] temp;
            try
            {
                temp = TagContent.Split(Delimeter);
                if (Position - 1 < temp.Length)
                {
                    return temp[Position - 1];
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            return (object)"";
        }

        private void MoveCursor(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (sender.GetType() == typeof(System.Windows.Forms.TextBox) && ((TextBox)sender).Name.ToUpper() == txtPatientSearch.Name.ToUpper())
                    {
                        if (_IsPaymentCorrectionMode == false)
                        {
                            if (_IsUseReserveEntry == false)
                            {
                                if (oPatientControl != null && oPatientControl.PatientID > 0)
                                { cmbPayMode.Select(); cmbPayMode.Focus(); }
                                else
                                { txtPatientSearch.Select(); txtPatientSearch.Focus(); }
                            }
                            else
                            {
                                if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                                {
                                    for (int i = 1; i <= c1SinglePayment.Rows.Count - 1; i++)
                                    {
                                        if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                                        {

                                            c1SinglePayment.Select(i, COL_CUR_PAYMENT, true);
                                            c1SinglePayment.Focus();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                            {
                                for (int i = 1; i <= c1SinglePayment.Rows.Count - 1; i++)
                                {
                                    if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                                    {

                                        c1SinglePayment.Select(i, COL_CUR_PAYMENT, true);
                                        c1SinglePayment.Focus();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else if (sender.GetType() == typeof(System.Windows.Forms.ComboBox) && ((ComboBox)sender).Name.ToUpper() == cmbPayMode.Name.ToUpper())
                    {
                        if (cmbPayMode.Text.Trim() == PaymentMode.Cash.ToString())
                        { mskCheckDate.Select(); mskCheckDate.Focus(); }
                        else
                        { txtCheckNumber.Select(); txtCheckNumber.Focus(); }
                    }
                    else if (sender.GetType() == typeof(System.Windows.Forms.TextBox) && ((TextBox)sender).Name.ToUpper() == txtCheckNumber.Name.ToUpper())
                    { mskCheckDate.Select(); mskCheckDate.Focus(); }
                    else if (sender.GetType() == typeof(System.Windows.Forms.MaskedTextBox) && ((MaskedTextBox)sender).Name.ToUpper() == mskCheckDate.Name.ToUpper())
                    {
                        //if (txtCheckAmount.Visible == true) { txtCheckAmount.Select(); txtCheckAmount.Focus(); } 
                        //else { txtEOBRefNumber.Select(); txtEOBRefNumber.Focus(); } 
                        if (cmbPayMode.Text.Trim() == PaymentMode.CreditCard.ToString())
                        { cmbCardType.Select(); cmbCardType.Focus(); cmbCardType_MouseEnter(null, null); }
                        else
                        { txtCheckAmount.Select(); txtCheckAmount.Focus(); }

                    }
                    else if (sender.GetType() == typeof(System.Windows.Forms.TextBox) && ((TextBox)sender).Name.ToUpper() == txtCheckAmount.Name.ToUpper())
                    //{ txtEOBRefNumber.Select(); txtEOBRefNumber.Focus(); }
                    //else if (sender.GetType() == typeof(System.Windows.Forms.TextBox) && ((TextBox)sender).Name.ToUpper() == txtEOBRefNumber.Name.ToUpper())
                    {

                        if (c1SinglePayment.Rows.Count > 1)
                        {
                            for (int i = 1; i <= c1SinglePayment.Rows.Count - 1; i++)
                            {
                                if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                                {

                                    c1SinglePayment.Select(i, COL_CUR_PAYMENT, true);
                                    c1SinglePayment.Focus();
                                    break;
                                }
                            }
                        }
                        else
                        {
                            txtPayMstNotes.Select(); txtPayMstNotes.Focus();
                        }

                        //if (oPatientControl != null) { oPatientControl.Select(); oPatientControl.Focus(); oPatientControl.SelectSearchBox(); } 
                    }
                    else if (sender.GetType() == typeof(System.Windows.Forms.TextBox) && ((TextBox)sender).Name.ToUpper() == txtPayMstNotes.Name.ToUpper())
                    {
                        if (c1SinglePayment.Rows.Count > 1)
                        {
                            for (int i = 1; i <= c1SinglePayment.Rows.Count - 1; i++)
                            {
                                if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                                {

                                    c1SinglePayment.Select(i, COL_CUR_PAYMENT, true);
                                    c1SinglePayment.Focus();
                                    break;
                                }
                            }
                        }
                    }
                    else if (sender.GetType() == typeof(System.Windows.Forms.ComboBox) && ((ComboBox)sender).Name.ToUpper() == cmbCardType.Name.ToUpper())
                    { txtCardAuthorizationNo.Select(); txtCardAuthorizationNo.Focus(); }
                    else if (sender.GetType() == typeof(System.Windows.Forms.TextBox) && ((TextBox)sender).Name.ToUpper() == txtCardAuthorizationNo.Name.ToUpper())
                    { mskCreditExpiryDate.Select(); mskCreditExpiryDate.Focus(); }
                    else if (sender.GetType() == typeof(System.Windows.Forms.MaskedTextBox) && ((MaskedTextBox)sender).Name.ToUpper() == mskCreditExpiryDate.Name.ToUpper())
                    {
                        txtCheckAmount.Select(); txtCheckAmount.Focus();
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void FillPrintReceipt()
        {
            gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            //DataTable dtCategories = new DataTable();//SLR: not used
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable dtTemplates = null;//SLR: new is not needed
           // String CategoryName = "";
            //SLR: Before clear, clear events, then delete each menu items and then finally 
            

            try
            {
                oDB.Connect(false);
                tls_btnDefaultReceipt.Visible = false;
                tls_btnReceipt.Visible = false;
                tls_btnDefaultReceipt.Tag = null;

                dtTemplates = ogloTemplate.GetAssociation(gloOffice.AssociationCategories.PatientReceipt);

                if (dtTemplates != null && dtTemplates.Rows.Count > 0)
                {
                    ToolStripItem[] _toolStripDropDownItemArray = new ToolStripItem[dtTemplates.Rows.Count];
                    ToolStripMenuItem oTemplateItem = null;
                    
                    Console.WriteLine(DateTime.Now.ToLongTimeString());

                    for (int j = 0; j < dtTemplates.Rows.Count; j++)
                    {
                        oTemplateItem = new ToolStripMenuItem();
                        oTemplateItem.Text = dtTemplates.Rows[j]["sTemplateName"].ToString();
                        oTemplateItem.Tag = dtTemplates.Rows[j]["nTemplateID"].ToString();
                        oTemplateItem.ForeColor = Color.FromArgb(31, 73, 125);
                        //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                        //replace declaration for create new font by font variable.
                        oTemplateItem.Font = Font_Template; //new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        oTemplateItem.Image = global ::gloBilling.Properties.Resources.Bullet06;
                        oTemplateItem.ImageAlign = ContentAlignment.MiddleLeft;
                        oTemplateItem.ImageScaling = ToolStripItemImageScaling.None;
                        _toolStripDropDownItemArray[j] = oTemplateItem;
                        //_btnReceipts.DropDownItems.Add(oTemplateItem);
                        oTemplateItem.Click += new EventHandler(oTemplateItem_Click);

                        if (dtTemplates.Rows[j]["bIsDefault"] != null && Convert.ToBoolean(dtTemplates.Rows[j]["bIsDefault"]) == true)
                        {
                            tls_btnDefaultReceipt.Tag = dtTemplates.Rows[j]["nTemplateID"].ToString();
                            tls_btnDefaultReceipt.Visible = true;
                        }
                       oTemplateItem = null;
                    }

                    tls_btnReceipt.DropDownItems.Clear();
                    if (_toolStripDropDownItemArray != null && _toolStripDropDownItemArray.Length > 0)
                    {
                        tls_btnReceipt.DropDownItems.AddRange(_toolStripDropDownItemArray);
                        _toolStripDropDownItemArray = null;
                    }

                    //Console.WriteLine(DateTime.Now.ToLongTimeString());
                }

                if (dtTemplates != null) { dtTemplates.Dispose(); }
                if (tls_btnReceipt.DropDownItems.Count > 0) { tls_btnReceipt.Visible = true; }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                //SLR: Free ogloTemplate
                if (ogloTemplate!=null)
                {
                    ogloTemplate.Dispose();
                }
            }
        }

        void oTemplateItem_Click(object sender, EventArgs e)
        {
            if (_IsAllDatesValid == true && SavePaymentValidation())
            {
                Int64 _payId = 0;
                _payId = SavePayment();
                ClearFormData();
                FillPaymentTray();
                AllowEditValidation();
                oPatientControl.RefreshBalances();
                ToolStripMenuItem oTemplateItem = null;
                oTemplateItem = (ToolStripMenuItem)sender;
                if (oTemplateItem != null && oTemplateItem.Tag != null && oTemplateItem.Tag.ToString().Trim().Length > 0)
                {
                    PrintReceipt(_payId, Convert.ToInt64(Convert.ToString(oTemplateItem.Tag)));
                }
                oTemplateItem = null;
            }
        }

        private void PrintReceipt(Int64 PaymentId, Int64 TemplateID)
        {
            gloOffice.gloTemplate ogloTemplate = new gloOffice.gloTemplate(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            Int64 _TemplateID = 0;
            Int64 _PayId = 0;
            try
            {
                #region " Print Receipt using Payment ID "

                _PayId = PaymentId;


                if (_PayId > 0)
                {
                    if (TemplateID > 0)
                    {
                        _TemplateID = TemplateID;// Convert.ToInt64(cmbPayReceipt.SelectedValue);
                        ogloTemplate.TemplateID = _TemplateID;
                        ogloTemplate.PrimeryID = _PayId;
                        //ogloTemplate.TemplateName = "";
                        ogloTemplate.PatientID = _PatientID;
                        ogloTemplate.ClinicID = _ClinicID;
                        gloOffice.frmWd_PatientTemplate frm = new gloOffice.frmWd_PatientTemplate(gloGlobal.gloPMGlobal.DatabaseConnectionString, ogloTemplate);
                        frm._AccountID = oPatientControl.PAccountID;
                        frm.Show();
                        frm.WindowState = FormWindowState.Maximized;
                    }
                }
               

                #endregion " Print Receipt using Payment ID "
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
            finally
            {
                //SLR: Free ogloTemplate
                if (ogloTemplate!=null)
                {
                    ogloTemplate.Dispose();
                }
            }
        }

        private void FillCreditCards()
        {
            CreditCards oCreditCards = new CreditCards(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtCards = null;

            try
            {
            //    cmbCardType.Items.Clear();
                cmbCardType.DataSource = null;
                cmbCardType.Items.Clear();
                _dtCards = oCreditCards.GetList();

                if (_dtCards != null && _dtCards.Rows.Count > 0)
                {
                    DataRow _dr = _dtCards.NewRow();
                    _dr["nCreditCardID"] = 0;
                    _dr["sCreditCardDesc"] = "";
                    _dtCards.Rows.InsertAt(_dr, 0);
                    _dtCards.AcceptChanges();

                    cmbCardType.DataSource = _dtCards.Copy();
                    cmbCardType.ValueMember = _dtCards.Columns[0].ColumnName;
                    cmbCardType.DisplayMember = _dtCards.Columns[1].ColumnName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (oCreditCards != null) { oCreditCards.Dispose(); }
                //if (_dtCards != null) { _dtCards.Dispose(); }
            }
        }

        private void SetShowZeroBalance()
        {
            ShowZeroBalanceClaims = false;
            tsb_ShowHideZeroBalance.Tag = "Show";
            tsb_ShowHideZeroBalance.Text = "Show Zero &Balance";
            tsb_ShowHideZeroBalance.Image = global ::gloBilling.Properties.Resources.Show_Zero_Balance;
        }

        private void SetShowBadDebtBalance()
        {
            ShowBadDebtBalanceClaims = false;
            tsb_ShowHideBadDebtBalance.Tag = "Show";
            tsb_ShowHideBadDebtBalance.Text = "Show Bad&Debt Balance";
            tsb_ShowHideBadDebtBalance.Image = global ::gloBilling.Properties.Resources.ShowBadDebt;
        }

        private void SetHideZeroBalance()
        {
            tsb_ShowHideZeroBalance.Tag = "Hide";
            tsb_ShowHideZeroBalance.Text = "Hide Zero &Balance";
            ShowZeroBalanceClaims = true;
            tsb_ShowHideZeroBalance.Image = global ::gloBilling.Properties.Resources.Hide_Zero_Balance;
        }

        private void SetHideBadDebtBalance()
        {
            tsb_ShowHideBadDebtBalance.Tag = "Hide";
            tsb_ShowHideBadDebtBalance.Text = "Show &All Balance";
            ShowBadDebtBalanceClaims = true;
            tsb_ShowHideBadDebtBalance.Image = global ::gloBilling.Properties.Resources.HideBadDebt;
        }

        private void SetCloseDate()
        {
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
            try
            {
                MaskedTextBox mskDate = (MaskedTextBox)mskCloseDate;
                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskDate.Text;
                mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                if (mskDate != null)
                {
                    if (strDate.Length <= 0)
                    {
                        #region " Set last selected close date "

                        //....Load last selected close date
                        //...If the last selected close date is being closed then make the close date empty.

                        //gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                        //Object _retValue = null;
                        //string _clsDate = "";
                        //oSettings.GetSetting("PAYMENT_LASTCLOSEDATE", _UserId, _ClinicID, out _retValue);
                        //oSettings.Dispose();

                        //if (_retValue != null && Convert.ToString(_retValue).Trim() != "")
                        //{
                        //    try
                        //    { _clsDate = Convert.ToDateTime(Convert.ToString(_retValue).Trim()).ToString("MM/dd/yyyy"); }
                        //    catch (Exception ex)
                        //    {
                        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        //        ex = null;  
                        //        _clsDate = DateTime.Now.Date.ToString("MM/dd/yyyy"); }
                        //}
                        //else
                        //{ _clsDate = DateTime.Now.Date.ToString("MM/dd/yyyy"); }

                        //if (_clsDate.Trim() != "")
                        //{
                        //    //gloBilling ogloBilling = new gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
                        //    if (ogloBilling.IsDayClosed(Convert.ToDateTime(_clsDate.Trim())) == true)
                        //    { _clsDate = ""; }
                        //    ogloBilling.Dispose();
                        //}

                        //mskCloseDate.Text = _clsDate;
                        string _lastCloseDate = gloBilling.gloBilling.GetUserWiseCloseDay(_UserId, CloseDayType.Payment);
                        mskCloseDate.Text = _lastCloseDate;
                        if (mskDate.Text == "")
                        {
                            mskCheckDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                        }
                        else
                        { mskCheckDate.Text = mskCloseDate.Text; }
                        #endregion " Set last selected close date "
                    }
                }
                mskDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }
        }

        #endregion

        #region "C1 Events"

        #region "Single Payment"

        private void c1SinglePaymentTotal_AfterScroll(object sender, RangeEventArgs e)
        {
            try
            {
                c1SinglePayment.ScrollPosition = new Point(c1SinglePaymentTotal.ScrollPosition.X,c1SinglePayment.ScrollPosition.Y);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1SinglePayment_AfterScroll(object sender, RangeEventArgs e)
        {
            try
            {
                c1SinglePaymentTotal.ScrollPosition = new Point(c1SinglePayment.ScrollPosition.X, c1SinglePaymentTotal.ScrollPosition.Y);                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }


        private void c1SinglePayment_CellChanged(object sender, RowColEventArgs e)
        {
            c1SinglePayment.FinishEditing();
            if (_IsFormLoading == false && _IsPaymentGridLoading == false)
            {
                if (e.Col == COL_CUR_PAYMENT || e.Col == COL_CUR_ADJ_AMOUNT)
                {
                    //c1SinglePaymentTotal.SetData(0, e.Col, CalculateSinglePaymentTotal(e.Col));


                    if (_IsPaymentCorrectionMode == false)
                    {
                        #region " Event code for New Payment "

                        if (e.Col == COL_CUR_ADJ_AMOUNT)
                        {

                            if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                            {
                                //if (Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)) < 0)
                                //{
                                //    MessageBox.Show("Adjustment amount cannot be less than 0 (zero).", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    try
                                //    {
                                //        this.c1SinglePayment.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged);
                                //        c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT, null);
                                //    }
                                //    catch (Exception ex)
                                //    {
                                //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                //        ex = null;
                                //    }
                                //    finally
                                //    { this.c1SinglePayment.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged); }
                                //}
                                //else
                                    if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                    {
                                        //..Phil's Formula - Adjustment Amount cannot be greater than (Total Charge + Prev. Adjustment) as below
                                        //if (Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)) > (Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_TOTALCHARGE)) - Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ))))
                                        //..but this creates negative balance for the service line
                                        //so changing formula as below
                                        // .... Adjustment Amount cannot be greater than (Total Charge  + Prev. Adjustment + Curr. Payment + Prev. Paid)

                                        decimal _CurrentAdjusmentAmt = 0;
                                        decimal _TotalCharge = 0;
                                        decimal _PrevAdjustment = 0;
                                        decimal _CurrentPayment = 0;
                                        decimal _PrevPayment = 0;
                                        decimal _PrevTotalAdjustment = 0;

                                        if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_TOTALCHARGE) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_TOTALCHARGE)).Trim() != "")
                                        { _TotalCharge = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_TOTALCHARGE)); }

                                        //if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ)).Trim() != "")
                                        //{ _PrevAdjustment = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ)); }

                                        if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PATIENTADJUSTMENT_AMT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PATIENTADJUSTMENT_AMT)).Trim() != "")
                                        { _PrevAdjustment = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PATIENTADJUSTMENT_AMT)); }

                                        if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)).Trim() != "")
                                        { _CurrentPayment = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)); }

                                        if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PAID) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PAID)).Trim() != "")
                                        { _PrevPayment = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PAID)); }

                                        if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                        { _CurrentAdjusmentAmt = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)); }

                                        if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ)).Trim() != "")
                                        { _PrevTotalAdjustment = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ)); }

                                        //..
                                        //if (_CurrentAdjusmentAmt > (_TotalCharge - _PrevAdjustment - _CurrentPayment - _PrevPayment))
                                        if (_CurrentAdjusmentAmt > (_TotalCharge - _PrevTotalAdjustment - _CurrentPayment - _PrevPayment))
                                        {
                                            MessageBox.Show("Adjustment amount cannot be greater than net charge.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            try
                                            {
                                                this.c1SinglePayment.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged);
                                                c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT, null);
                                            }
                                            catch (Exception ex)
                                            {
                                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                                ex = null;
                                            }
                                            finally
                                            { this.c1SinglePayment.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged); }
                                        }
                                    }
                            }
                        }

                        if (e.Col == COL_CUR_PAYMENT)
                        {
                            if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)).Trim() != "")
                            {
                                if (Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)) < 0)
                                {
                                    MessageBox.Show("Payment amount cannot be less than 0 (zero).", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    try
                                    {
                                        this.c1SinglePayment.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged);
                                        c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT, null);
                                    }
                                    catch (Exception ex)
                                    {
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                        ex = null;
                                    }
                                    finally
                                    { this.c1SinglePayment.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged); }
                                }
                            }

                            if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)).Trim() != ""
                             && c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PAT_DUE) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PAT_DUE)).Trim() != "")
                            {
                                //if (Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)) > Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PAT_DUE)))
                                if (Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PAT_DUE)) > 0 && Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)) > Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PAT_DUE)))
                                {
                                    //MessageBox.Show("Payment amount cannot be greater than balance amount.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    MessageBox.Show("Warning: Payment amount is greater than patient due amount.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    //try
                                    //{
                                    //    this.c1SinglePayment.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged);
                                    //    c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT, null);
                                    //}
                                    //catch (Exception ex)
                                    //{ }
                                    //finally
                                    //{ this.c1SinglePayment.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged); }
                                }
                            }
                        }

                        #endregion " Event code for New Payment "
                    }
                    else
                    {
                        #region " Event code for Correction Payment "

                        //..... Correction Mode events code
                        if (e.Col == COL_CUR_PAYMENT)
                        {

                            if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)).Trim() != ""
                             && c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PATIENTPAYMENT_AMT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PATIENTPAYMENT_AMT)).Trim() != "")
                            {

                                if ((
                                    Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)) -
                                    (Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)) * 2)
                                    )
                                    > (Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PATIENTPAYMENT_AMT))))
                                {
                                    MessageBox.Show("Correction amount cannot be greater than previous patient paid amount.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    try
                                    {
                                        this.c1SinglePayment.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged);
                                        c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT, null);
                                    }
                                    catch (Exception ex)
                                    {
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                        ex = null;
                                    }
                                    finally
                                    { this.c1SinglePayment.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged); }
                                }
                            }
                        }

                        //Bug # 00000794 payment correction - payment lost. Added condition to avoid check during changes in adjustment column
                        if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)).Trim() != "" && e.Col != COL_CUR_ADJ_AMOUNT)
                        {
                            if (Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)) > 0)
                            {
                                if ((txtCheckRemaining.Text.Trim() != "" && (Convert.ToDecimal(txtCheckRemaining.Text.Trim()) + _previousamt) >= 0)
                                    && (Convert.ToDecimal(txtCheckRemaining.Text.Trim()) + _previousamt) >= Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT))
                                )
                                {

                                }
                                else
                                {
                                    //MessageBox.Show("Payment amount cannot be greater than 0 (zero) or no funds remaining to allocate.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    MessageBox.Show("Funds remaining are less than allocation amount.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    try
                                    {
                                        this.c1SinglePayment.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged);
                                        c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT, null);
                                    }
                                    catch (Exception ex)
                                    {
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                        ex = null;
                                    }
                                    finally
                                    { this.c1SinglePayment.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged); }
                                }
                            }
                        }

                        if (e.Col == COL_CUR_ADJ_AMOUNT)
                        {

                            //..Phil's Formula - Adjustment Amount cannot be greater than (Total Charge + Prev. Adjustment) as below
                            //if (Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)) > (Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_TOTALCHARGE)) - Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ))))
                            //..but this creates negative balance for the service line
                            //so changing formula as below
                            // .... Adjustment Amount cannot be greater than (Total Charge  + Prev. Adjustment + Curr. Payment + Prev. Paid)

                            decimal _CurrentAdjusmentAmt = 0;
                            decimal _TotalCharge = 0;
                            decimal _PrevAdjustment = 0;
                            decimal _CurrentPayment = 0;
                            decimal _PrevPayment = 0;

                            if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_TOTALCHARGE) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_TOTALCHARGE)).Trim() != "")
                            { _TotalCharge = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_TOTALCHARGE)); }

                            //if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ)).Trim() != "")
                            //{ _PrevAdjustment = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ)); }

                            if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PATIENTADJUSTMENT_AMT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PATIENTADJUSTMENT_AMT)).Trim() != "")
                            { _PrevAdjustment = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PATIENTADJUSTMENT_AMT)); }

                            if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)).Trim() != "")
                            { _CurrentPayment = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_PAYMENT)); }

                            if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PAID) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PAID)).Trim() != "")
                            { _PrevPayment = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PAID)); }

                            if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                            { _CurrentAdjusmentAmt = Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)); }

                            //if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                            //{
                            //    if (Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)) > 0)
                            //    {
                            //        MessageBox.Show("Correction adjustment amount cannot be positive.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //        try
                            //        {
                            //            this.c1SinglePayment.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged);
                            //            c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT, null);
                            //        }
                            //        catch (Exception ex)
                            //        {
                            //            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            //            ex = null;
                            //        }
                            //        finally
                            //        { this.c1SinglePayment.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged); }
                            //    }
                            //}

                            //if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)).Trim() != ""
                            // && c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ)).Trim() != "")
                            if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)).Trim() != ""
                             && c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PATIENTADJUSTMENT_AMT) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_PATIENTADJUSTMENT_AMT)).Trim() != "")
                            {
                                //if ((
                                //    Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)) -
                                //    (Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT)) * 2)
                                //    )
                                //    > (Convert.ToDecimal(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PREV_ADJ))))
                                decimal _currAdj = 0;
                                _currAdj = _CurrentAdjusmentAmt - (_CurrentAdjusmentAmt * 2);
                                if (_currAdj > _PrevAdjustment)
                                {
                                    string _strMessage = "";
                                    if (_PrevAdjustment == 0)
                                    {
                                        _strMessage = "Cannot perform patient adjustment correction, previous patient adjusment is zero.";
                                    }
                                    else
                                    {
                                        _strMessage = "Correction adjustment amount cannot be greater than previous patient adjustment amount. (Previous Patient Adj. : $ " + _PrevAdjustment.ToString("#0.00") + ")";
                                    }

                                    //MessageBox.Show("Correction adjustment amount cannot be greater than previous patient adjustment ( $ " + _PrevAdjustment.ToString("#0.00") + " ).", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    MessageBox.Show(_strMessage, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    try
                                    {
                                        this.c1SinglePayment.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged);
                                        c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT, null);
                                    }
                                    catch (Exception ex)
                                    {
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                        ex = null;
                                    }
                                    finally
                                    { this.c1SinglePayment.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged); }
                                }
                            }
                        }

                        #endregion " Event code for Correction Payment "
                    }

                    c1SinglePaymentTotal.SetData(0, e.Col, CalculateSinglePaymentTotal(e.Col));
                    CalculateRemainingAmount();
                }
                else if (e.Col == COL_CUR_ADJ_TYPECODE)
                {
                    if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_TYPECODE) != null && Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_TYPECODE)).Trim() != "")
                    {
                        string[] AdjCodeDesc = null;
                        string _adjstr = "";
                        _adjstr = Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_TYPECODE)).Trim().Replace('-', ' ').Replace(',', ' ').Replace('(', ' ').Replace(')', ' ').Trim();
                        bool _isValidCOde = false;
                        CellStyle cs = c1SinglePayment.GetCellStyle(c1SinglePayment.RowSel, COL_CUR_ADJ_TYPECODE);

                        if (_adjstr != "")
                        {
                            _adjstr = _adjstr = Convert.ToString(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_CUR_ADJ_TYPECODE)).Trim();
                            _isValidCOde = cs.ComboList.Contains(_adjstr);
                        }

                        try
                        {
                            this.c1SinglePayment.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged);

                            if (_isValidCOde == true)
                            {
                                AdjCodeDesc = _adjstr.Split('-');
                                c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_CUR_ADJ_TYPECODE, AdjCodeDesc[0]);
                                c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_CUR_ADJ_TYPEDESCRIPTION, AdjCodeDesc[1]);
                            }
                            else
                            {
                                MessageBox.Show("Please select a valid code.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_CUR_ADJ_TYPECODE, null);
                                c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_CUR_ADJ_TYPEDESCRIPTION, null);
                                c1SinglePayment.Focus();
                                c1SinglePayment.Select(c1SinglePayment.RowSel, COL_CUR_ADJ_TYPECODE, true);
                            }

                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            ex = null;
                        }
                        finally
                        { this.c1SinglePayment.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged); }
                        //= AdjCodeDesc[0];
                        //= AdjCodeDesc[1];
                    }
                }

            }
        }

        private void c1SinglePayment_AfterResizeColumn(object sender, RowColEventArgs e)
        {
            try
            {
                c1SinglePaymentTotal.Cols[e.Col].Width = c1SinglePayment.Cols[e.Col].Width;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1SinglePayment_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (c1SinglePayment.ColSel == COL_CLAIMDISPNO)
                {
                    for (int i = c1SinglePayment.RowSel; i <= c1SinglePayment.Rows.Count - 1; i++)
                    {
                        if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                        {
                            c1SinglePayment.Select(i, COL_CUR_PAYMENT, true);
                            break;
                        }
                    }
                }
                else if (c1SinglePayment.ColSel == COL_CUR_PAYMENT)
                {
                    mnuPayment_NextServiceLine_Click(null, null);
                }
                else if (c1SinglePayment.ColSel == COL_CUR_ADJ_TYPECODE)
                {
                    if (c1SinglePayment.RowSel > 0) { c1SinglePayment.Select(c1SinglePayment.RowSel, COL_CUR_ADJ_AMOUNT, true); }
                }
                else if (c1SinglePayment.ColSel == COL_CUR_ADJ_AMOUNT)
                {
                    mnuPayment_NextServiceLine_Click(null, null);
                }

            }
            else if (e.KeyCode == Keys.Tab)
            {
                if (c1SinglePayment.ColSel == COL_CLAIMDISPNO)
                {
                    for (int i = c1SinglePayment.RowSel; i <= c1SinglePayment.Rows.Count - 1; i++)
                    {
                        if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                        {
                            c1SinglePayment.Select(i, COL_CUR_PAYMENT, true);
                            break;
                        }
                    }
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1 && c1SinglePayment.RowSel > 1)
                {
                    if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                    {
                        if (c1SinglePayment.ColSel == COL_CUR_PAYMENT || c1SinglePayment.ColSel == COL_CUR_ADJ_AMOUNT)
                        { c1SinglePayment.SetData(c1SinglePayment.RowSel, c1SinglePayment.ColSel, null); }
                        else if (c1SinglePayment.ColSel == COL_CUR_ADJ_TYPECODE)
                        {
                            c1SinglePayment.SetData(c1SinglePayment.RowSel, c1SinglePayment.ColSel, null);
                            c1SinglePayment.SetData(c1SinglePayment.RowSel, COL_CUR_ADJ_TYPEDESCRIPTION, null);
                        }
                    }
                }
            }
        }

        private void c1SinglePayment_StartEdit(object sender, RowColEventArgs e)
        {
            try
            {
                if (e.Row <= 0)
                { e.Cancel = true; }
                else
                {
                    _previousamt = 0;
                    if (e.Col == COL_CUR_PAYMENT && c1SinglePayment.GetData(e.Row, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(e.Row, COL_CUR_PAYMENT)).Trim() != "")
                    {
                        if (Convert.ToDecimal(c1SinglePayment.GetData(e.Row, COL_CUR_PAYMENT)) > 0)
                        { _previousamt = Convert.ToDecimal(c1SinglePayment.GetData(e.Row, COL_CUR_PAYMENT)); }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void c1SinglePayment_AfterRowColChange(object sender, RangeEventArgs e)
        {            
            try
            {
                if (_IsFormLoading == false && _IsPaymentGridLoading == false)
                {
                    if (e.OldRange.r1 != e.NewRange.r1)
                    {
                        if (c1SinglePayment.GetData(e.NewRange.r1, COL_SERVICELINE_TYPE) != null)                            
                        {
                            ColServiceLineType convertedValue = ColServiceLineType.None;
                            if (Enum.TryParse(Convert.ToString(c1SinglePayment.GetData(e.NewRange.r1, COL_SERVICELINE_TYPE)), out convertedValue))
                            {
                                if (convertedValue == ColServiceLineType.Claim || convertedValue == ColServiceLineType.ServiceLine)
                                {
                                    if (c1SinglePayment.GetData(e.NewRange.r1, COL_CLAIMNO) != null && Convert.ToString(c1SinglePayment.GetData(e.NewRange.r1, COL_CLAIMNO)).Trim() != "")
                                    {
                                        Int64 _clmNo = 0;
                                        string _subClaimNo = "";
                                        String _claimSubClaimNo = "";

                                        _claimSubClaimNo = Convert.ToString(c1SinglePayment.GetData(e.NewRange.r1, COL_CLAIMSUBCLAIM_NO));
                                        if (_claimSubClaimNo.Split('-').Length == 2)
                                        {
                                            _subClaimNo = _claimSubClaimNo.Split('-')[1];
                                        }

                                        _clmNo = Convert.ToInt64(c1SinglePayment.GetData(e.NewRange.r1, COL_CLAIMNO));

                                        // Subashish start - need to test this code post integration of Patient banner--------------
                                        if (_IsPatientAccountFeature)
                                        {
                                            Int64 nSelectedClaimPatient = 0;
                                            if (c1SinglePayment.Rows[c1SinglePayment.RowSel][COL_PATIENTID] != null)
                                                nSelectedClaimPatient = Convert.ToInt64(c1SinglePayment.Rows[c1SinglePayment.RowSel][COL_PATIENTID].ToString());

                                        } // end subashish

                                    }
                                }                               
                            }                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {  }
        }

        #endregion

        private decimal CalculateSinglePaymentTotal(int ColNumber)
        {
            decimal _result = 0;
            try
            {
                if (c1SinglePayment.Rows.Count > 1)
                {
                    for (int i = 1; i <= c1SinglePayment.Rows.Count - 1; i++)
                    {
                        if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                        {
                            if (c1SinglePayment.GetData(i, ColNumber) != null && c1SinglePayment.GetData(i, ColNumber).ToString() != null && c1SinglePayment.GetData(i, ColNumber).ToString().Trim().Length > 0)
                            {
                                _result = _result + Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, ColNumber)));
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
            }
            return _result;

        }

        private void CalculateRemainingAmount()
        {
            decimal _totalCheckAmt = 0;
            decimal _appliedAmt = 0;
            decimal _AdjAmt = 0;
            decimal _ResAmt = 0;
            decimal _remainingAmt = 0;

            try
            {
                if (txtCheckAmount.Text.Trim() != "")
                { _totalCheckAmt = Convert.ToDecimal(txtCheckAmount.Text.Trim()); }

                if (c1SinglePaymentTotal.GetData(0, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePaymentTotal.GetData(0, COL_CUR_PAYMENT)).Trim() != "")
                { _appliedAmt = Convert.ToDecimal(c1SinglePaymentTotal.GetData(0, COL_CUR_PAYMENT)); }

                //if (c1MultiplePaymentTotal.GetData(0, COL_CUR_PAYMENT) != null && Convert.ToString(c1MultiplePaymentTotal.GetData(0, COL_CUR_PAYMENT)).Trim() != "")
                //{ _appliedAmt += Convert.ToDecimal(c1MultiplePaymentTotal.GetData(0, COL_CUR_PAYMENT)); }

                if (c1SinglePaymentTotal.GetData(0, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePaymentTotal.GetData(0, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                { _AdjAmt = Convert.ToDecimal(c1SinglePaymentTotal.GetData(0, COL_CUR_ADJ_AMOUNT)); }

                #region "Reserve Amount"
                //0 ReserveAmount 
                string[] oList = null;
                if (btnReserveRemaining.Tag != null)
                {
                    oList = btnReserveRemaining.Tag.ToString().Split('~');
                }

                if (oList != null && oList.Length == 8)
                {
                    if (oList[0] != null && Convert.ToString(oList[0]).Trim() != "")
                    {
                        _ResAmt = Convert.ToDecimal(Convert.ToString(oList[0]));
                    }
                }

                #endregion

                _remainingAmt = (_totalCheckAmt - (_appliedAmt + _ResAmt));// +_AdjAmt;

                txtCheckRemaining.Text = _remainingAmt.ToString("#0.00");
                if (_remainingAmt != 0)
                { lblShowRemaining.Text = _remainingAmt.ToString("#0.00"); }
                else
                { lblShowRemaining.Text = ""; }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        #endregion

        #region "ToolStrip Button Click Events"

        private void tls_Top_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            tls_Top.Select();
        }
        private void tls_btnClose_Click(object sender, EventArgs e)
        {

            if (IsPaymentMade() == true)
            {
                DialogResult _dlgRst = DialogResult.None;
                _dlgRst = MessageBox.Show("Do you want to save changes ?", _messageboxcaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (_dlgRst == DialogResult.Yes)
                {
                    tls_btnSaveNClose_Click(null, null);
                    isclosecheck = false;
                }
                else if (_dlgRst == DialogResult.Cancel)
                { isclosecheck = false; }
                else if (_dlgRst == DialogResult.No)
                { this.Close(); }
            }
            else
            {
                this.Close();
            }
        }
        private void tls_btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 _retPayId = 0;
                c1SinglePayment.FinishEditing();

                if (_IsAllDatesValid == true && SavePaymentValidation())
                {
                    _retPayId = SavePayment();
                    if (_retPayId > 0)
                    {
                        //LoadFormDataOnPatientChanged();
                        ClearFormData();
                        _IsFormLoading = true;
                        if (_IsPatientAccountFeature)
                        {
                            //if (oPatientControl.PatientID > 0)
                            if (!oPatientControl.IsAllAccPatSelected)
                            {
                                FillBillingTransactionAccountPatient(oPatientControl.PatientID, oPatientControl.PAccountID);
                            }
                            else
                            {
                                FillBillingTransactionAccount(oPatientControl.PAccountID, true);
                            }
                        }
                        else
                        {
                            FillBillingTransaction(oPatientControl.PatientID, true);
                        }
                        _IsFormLoading = false;

                        AllowEditValidation();
                    }
                }


                oPatientControl.RefreshBalances();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
            }
        }
        private void tls_btnSaveNClose_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 _retPayId = 0;
                c1SinglePayment.FinishEditing();
                if (_IsAllDatesValid == true && SavePaymentValidation())
                { _retPayId=SavePayment();
                if (_retPayId!=0)
                    this.Close(); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void tls_btnNew_Click(object sender, EventArgs e)
        {
            c1SinglePayment.FinishEditing();
            if (_IsAllDatesValid == true)
            {
                SetNewPayment();
                AllowEditValidation();
            }
             
        }
        private void tls_btnNewCorrection_Click(object sender, EventArgs e)
        {

            c1SinglePayment.FinishEditing();

            if (_IsAllDatesValid == false) { return; }
            if (IsPaymentMade() == true)
            {
                DialogResult _dlgRst = DialogResult.None;
                _dlgRst = MessageBox.Show("Do you want to save changes ?", _messageboxcaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (_dlgRst == DialogResult.Yes)
                {
                    if (_IsAllDatesValid == true && SavePaymentValidation())
                    {
                        if (_IsPaymentCorrectionMode == true)
                        {
                            SavePatientCorrectionPayment();
                        }
                        else
                        {
                            if (_IsUseReserveEntry == false)
                            {
                                SavePatientPayment();
                            }
                            else
                            {
                                SavePatientUseReservePayment();
                            }
                        }

                        lblPayType.Enabled = false;
                        cmbPayMode.Enabled = false;
                        lblCheckNo.Enabled = false;
                        txtCheckNumber.Enabled = false;
                        // btnLoadCheck.Enabled = false;
                        // btnRemoveCheck.Enabled = false;
                        lblCheckDate.Enabled = false;
                        mskCheckDate.Enabled = false;
                        lblCardType.Enabled = false;
                        cmbCardType.Enabled = false;
                        lblCardAuthorizationNo.Enabled = false;
                        txtCardAuthorizationNo.Enabled = false;
                        lblExpiryDate.Enabled = false;
                        mskCreditExpiryDate.Enabled = false;

                        ClearFormData();
                        lblCheckAmount.Visible = false;
                        txtCheckAmount.Visible = false;
                        label6.Visible = false;
                        btnDistubuteAmount.Visible = false;
                        btnUseReserve.Visible = false;

                        tls_btnReceipt.Visible = false;
                        tls_btnDefaultReceipt.Visible = false;

                        _IsPaymentCorrectionMode = true;
                        _IsUseReserveEntry = false;
                        _IsAdjustmentMode = false;
                    }
                }
                else if (_dlgRst == DialogResult.No)
                {


                    lblPayType.Enabled = false;
                    cmbPayMode.Enabled = false;
                    lblCheckNo.Enabled = false;
                    txtCheckNumber.Enabled = false;
                    // btnLoadCheck.Enabled = false;
                    //btnRemoveCheck.Enabled = false;
                    lblCheckDate.Enabled = false;
                    mskCheckDate.Enabled = false;
                    lblCardType.Enabled = false;
                    cmbCardType.Enabled = false;
                    lblCardAuthorizationNo.Enabled = false;
                    txtCardAuthorizationNo.Enabled = false;
                    lblExpiryDate.Enabled = false;
                    mskCreditExpiryDate.Enabled = false;


                    ClearFormData();
                    lblCheckAmount.Visible = false;
                    txtCheckAmount.Visible = false;
                    label6.Visible = false;
                    btnDistubuteAmount.Visible = false;
                    btnUseReserve.Visible = false;
                    tls_btnReceipt.Visible = false;
                    tls_btnDefaultReceipt.Visible = false;

                    _IsPaymentCorrectionMode = true;
                    _IsUseReserveEntry = false;
                    _IsAdjustmentMode = false;

                }
            }
            else
            {
                lblPayType.Enabled = false;
                cmbPayMode.Enabled = false;
                lblCheckNo.Enabled = false;
                txtCheckNumber.Enabled = false;
                // btnLoadCheck.Enabled = false;
                //btnRemoveCheck.Enabled = false;
                lblCheckDate.Enabled = false;
                mskCheckDate.Enabled = false;
                lblCardType.Enabled = false;
                cmbCardType.Enabled = false;
                lblCardAuthorizationNo.Enabled = false;
                txtCardAuthorizationNo.Enabled = false;
                lblExpiryDate.Enabled = false;
                mskCreditExpiryDate.Enabled = false;


                ClearFormData();
                lblCheckAmount.Visible = false;
                txtCheckAmount.Visible = false;
                label6.Visible = false;
                btnDistubuteAmount.Visible = false;
                btnUseReserve.Visible = false;
                tls_btnReceipt.Visible = false;
                tls_btnDefaultReceipt.Visible = false;

                _IsPaymentCorrectionMode = true;
                _IsUseReserveEntry = false;
                _IsAdjustmentMode = false;
            }
            AllowEditValidation();
        }
        private void tls_btnDefaultReceipt_Click(object sender, EventArgs e)
        {
            GetControlSelection();
            c1SinglePayment.FinishEditing();

            if (_IsAllDatesValid == true && SavePaymentValidation())
            {
                Int64 _payId = 0;
                _payId = SavePayment();

                if (_payId > 0)
                {
                    if (tls_btnDefaultReceipt.Tag != null)
                    {
                        if (tls_btnDefaultReceipt.Tag.ToString().Trim().Length > 0)
                        {
                            PrintReceipt(_payId, Convert.ToInt64(Convert.ToString(tls_btnDefaultReceipt.Tag)));
                        }
                    }
                }
            } 
            SetControlSelection();
        }
        private void tsb_ShowHideZeroBalance_Click(object sender, EventArgs e)
        {
            if (_IsAllDatesValid == false) { return; }
            //if (oPatientControl != null && oPatientControl.PatientID > 0)
            if (oPatientControl != null)
            {
                if (IsPaymentMade() == true)
                {
                    DialogResult _dlgRst = DialogResult.None;
                    _dlgRst = MessageBox.Show("Do you want to save changes ?", _messageboxcaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (_dlgRst == DialogResult.Yes)
                    {
                        tls_btnSave_Click(null, null);
                    }
                    else if (_dlgRst == DialogResult.No)
                    {
                        if (tsb_ShowHideZeroBalance.Tag.ToString().Trim() == "Hide")
                        { SetShowZeroBalance(); }
                        else if (tsb_ShowHideZeroBalance.Tag.ToString().Trim() == "Show")
                        { SetHideZeroBalance(); }

                        RefreshClaimList();
                        CalculateRemainingAmount();
                    }
                }
                else
                {
                    if (tsb_ShowHideZeroBalance.Tag.ToString().Trim() == "Hide")
                    { SetShowZeroBalance(); }
                    else if (tsb_ShowHideZeroBalance.Tag.ToString().Trim() == "Show")
                    { SetHideZeroBalance(); }

                    RefreshClaimList();
                    CalculateRemainingAmount();
                }
            }
            AllowEditValidation();
        }
        private void tls_btnPatAcct_Click(object sender, EventArgs e)
        {
            GetControlSelection();
            frmPatientFinancialViewV2 frm = null;
            try
            {

                if (oPatientControl.PatientID != 0)
                {
                    frm = new frmPatientFinancialViewV2(oPatientControl.PatientID);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.ShowInTaskbar = false;
                    frm.IsCalledFromInsPmt = false;
                    frm.ShowDialog(this);

                    oPatientControl.RefreshData();
                    RefreshClaimList();
                    CalculateRemainingAmount();
                }
                else
                {
                    MessageBox.Show("Select Patient to view account information.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (frm != null) { frm.Dispose(); }
                SetControlSelection();
            }

        }
        #endregion

        #region "Form Control Events"

        private void txtCheckAmount_Leave(object sender, EventArgs e)
        {
            bool _isValidAmt = false;
            decimal _CheckAmount = 0;

            try
            {
                _isValidAmt = Decimal.TryParse(txtCheckAmount.Text.Trim(), out _CheckAmount);
                if (_isValidAmt != true) { _CheckAmount = 0; }
                txtCheckAmount.Text = _CheckAmount.ToString("#0.00");
                CalculateRemainingAmount();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void txtCheckAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            #region " Allow decimal amount only "

            bool hasDecimal = false;
            if (e.KeyChar == (char)46)
            {
                if (txtCheckAmount.Text.Contains(Convert.ToString(e.KeyChar)))
                { hasDecimal = true; }
                else
                { hasDecimal = false; }
            }

            if (hasDecimal == true)
            {
                if (!Char.IsDigit(e.KeyChar) & e.KeyChar != (char)8)
                { e.Handled = true; }
            }
            else
            {
                if (!Char.IsDigit(e.KeyChar) & e.KeyChar != (char)46 & e.KeyChar != (char)8)
                { e.Handled = true; }
            }

            #endregion

            if (e.KeyChar == Convert.ToChar(13))
            {
                CalculateRemainingAmount();
                //txtCheckAmount.Select();
                //txtCheckAmount.Focus();
            }
        }

        private void txtPatientSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13)
            //{
            //    if (oPatientControl != null)
            //    {
            //        // it will trigger FillBillingTransaction Method for changed patient
            //        oPatientControl.SearchPatient(txtPatientSearch.Text.Trim());
            //        txtPatientSearch.Text = oPatientControl.PatientCode.ToString();
            //    }
            //}
        }

        private void DateMouseClick(object sender, MouseEventArgs e)
        {
            ((MaskedTextBox)sender).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (((MaskedTextBox)sender).Text.Trim() == "")
            {
                ((MaskedTextBox)sender).SelectionStart = 0;
                ((MaskedTextBox)sender).SelectionLength = 0;
            }
        }

        private void ValidateDate(object sender, CancelEventArgs e)
        {

            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
            try
            {
                MaskedTextBox mskDate = (MaskedTextBox)sender;
                mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                string strDate = mskDate.Text;
                mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                _IsAllDatesValid = true;

                if (mskDate != null)
                {
                    if (strDate.Length > 0)
                    {
                        if (IsValidDate(mskDate.Text.Trim()) == false)
                        {
                            MessageBox.Show("Please enter a valid date.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            e.Cancel = true;
                            _IsAllDatesValid = false;
                            isclosecheck = false;
                        }
                        else if (mskCloseDate.MaskCompleted == true && IsValidDate(mskCloseDate.Text.Trim()) == false && ogloBilling.IsDayClosed(Convert.ToDateTime(mskCloseDate.Text)) == true && ((MaskedTextBox)sender).Name == mskCloseDate.Name)
                        {
                            MessageBox.Show("Selected date is already closed. Please select a different close date.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskCloseDate.Select(); mskCloseDate.Focus();
                            e.Cancel = true;
                            _IsAllDatesValid = false;
                            isclosecheck = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                MessageBox.Show("Please enter a valid date.  ", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                _IsAllDatesValid = false;
                isclosecheck = false;
            }
            finally
            { if (ogloBilling != null) { ogloBilling.Dispose(); } }

        }

        private bool IsValidDate(object strDate)
        {
            bool Success;
            try
            {
                DateTime validatedDate;
                Success = DateTime.TryParseExact(strDate.ToString(), "MM/dd/yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out validatedDate);
                if (validatedDate != null && Success == true)
                {
                    if (validatedDate < DateTime.MaxValue && validatedDate >= Convert.ToDateTime("01/01/1900"))
                    {
                        Success = true;
                    }
                    else
                    {
                        Success = false;
                    }

                }
            }
            catch (FormatException e)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(e.ToString(), false);
                e = null;
                Success = false; // If this line is reached, an exception was thrown
            }
            return Success;
        }

        private void cmbPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPayMode.SelectedIndex >= 0)
            {
                //txtCheckNumber.Text = "";
                cmbCardType.SelectedIndex = -1;
                txtCardAuthorizationNo.Text = "";
                mskCreditExpiryDate.Text = "";
                // panel16.TabStop = false;
                pnlCredit.TabStop = false;

              //  EOBPaymentMode _EOBPaymentMode = EOBPaymentMode.None;

                if (cmbPayMode.Text.Trim() == PaymentMode.Cash.ToString())
                {
                //    _EOBPaymentMode = EOBPaymentMode.Cash;
                    lblCheckDate.Text = "         Date :";
                    //txtCheckNumber.Text = "";
                    lblCheckNo.Text = "Ref.# :";
                    lblCheckNo.TextAlign = ContentAlignment.MiddleRight;
                    lblCheckNo.Enabled = true;
                    txtCheckNumber.Enabled = true;
                    pnlCredit.Enabled = false;
                    txtCheckNumber.MaxLength = 15;
                    label60.Visible = false;
                }
                else if (cmbPayMode.Text.Trim() == PaymentMode.Check.ToString())
                {
                  //  _EOBPaymentMode = EOBPaymentMode.Check;

                    lblCheckNo.Text = "Check# :";
                    lblCheckNo.TextAlign = ContentAlignment.MiddleRight;
                    lblCheckDate.Text = "Check Date :";
                    lblCheckDate.TextAlign = ContentAlignment.MiddleRight;
                    pnlCredit.Enabled = false;
                    txtCheckNumber.MaxLength = 15;
                    label60.Visible = true;
                    label5.Visible = false;
                }
                else if (cmbPayMode.Text.Trim() == PaymentMode.MoneyOrder.ToString())
                {
                    // _EOBPaymentMode = EOBPaymentMode.MoneyOrder;

                    lblCheckNo.Text = "MO# :";
                    lblCheckNo.TextAlign = ContentAlignment.MiddleRight;
                    lblCheckDate.Text = "    MO Date :";
                    lblCheckDate.TextAlign = ContentAlignment.MiddleRight;
                    pnlCredit.Enabled = false;
                    txtCheckNumber.MaxLength = 15;
                    label60.Visible = false;
                    label5.Visible = false;
                }
                else if (cmbPayMode.Text.Trim() == PaymentMode.CreditCard.ToString())
                {
                   // _EOBPaymentMode = EOBPaymentMode.CreditCard;
                    //cmbCardType.SelectedIndex = 0;
                    //Bug #82934: 00000913: application showing error message while post payment using credit card
                    if (cmbCardType.Items.Count == 0)
                    {
                        MessageBox.Show("Card Type is not present, Please add card type using following steps" + Environment.NewLine + "gloPM  >> Edit >> Billing Configuration >> Credit Card Type", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (Convert.ToString(txtCheckNumber.Text).Trim().Length > 4)
                    {
                        txtCheckNumber.Text = "";
                    }

                    lblCheckNo.Text = "Card# :";
                    lblCheckNo.TextAlign = ContentAlignment.MiddleRight;
                    lblCheckDate.Text = "         Date :";
                    lblCheckDate.TextAlign = ContentAlignment.MiddleRight;
                    pnlCredit.Enabled = true;
                    txtCheckNumber.MaxLength = 4;
                    pnlCredit.TabStop = true;
                    label60.Visible = false;
                    label5.Visible = true;
                }
                else if (cmbPayMode.Text.Trim() == PaymentMode.EFT.ToString())
                {
                   // _EOBPaymentMode = EOBPaymentMode.EFT;
                    lblCheckNo.Text = "EFT# :";
                    lblCheckNo.TextAlign = ContentAlignment.MiddleRight;
                    lblCheckDate.Text = "   EFT Date :";
                    lblCheckDate.TextAlign = ContentAlignment.MiddleRight;
                    pnlCredit.Enabled = false;
                    txtCheckNumber.MaxLength = 15;
                    label60.Visible = true;
                    label5.Visible = false;
                }

                else if (cmbPayMode.Text.Trim() == PaymentMode.Voucher.ToString())
                {
                    // _EOBPaymentMode = EOBPaymentMode.EFT;
                    lblCheckNo.Text = "Voucher# :";
                    lblCheckNo.TextAlign = ContentAlignment.MiddleRight;
                    lblCheckDate.Text = "Voucher Date :";
                    lblCheckDate.TextAlign = ContentAlignment.MiddleRight;
                    pnlCredit.Enabled = false;
                    txtCheckNumber.MaxLength = 15;
                    label60.Visible = true;
                    label5.Visible = false;
                }

                //if (_EOBPaymentMode == EOBPaymentMode.Check)
                //{
                //    mskCheckDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                //}
            }
        }

        #endregion

        #region " Form Button Click "
        private void btnRemoveCheck_Click(object sender, EventArgs e)
        {
            try
            {
                #region "Desing Grids"

                _IsFormLoading = true;

                txtCheckAmount.Text = "0.00";
                txtCheckNumber.Text = "";
                mskCheckDate.Text = "";
                txtCheckRemaining.Text = "0.00";
                lblShowRemaining.Text = "0.00";

                for (int i = 0; i <= cmbPayMode.Items.Count - 1; i++)
                {
                    if (cmbPayMode.Items[i].ToString() == PaymentMode.Check.ToString())
                    {
                        cmbPayMode.SelectedIndex = i;
                        break;
                    }
                }
                DesignPaymentGrid(c1SinglePayment);
                DesignPaymentGrid(c1SinglePaymentTotal);
                _PatientID = 0;
                _EOBPaymentID = 0; // it is used to hold master payement id for multiple claim payment
                c1ModifyPaymentTempGrid = null;

                _IsFormLoading = false;
                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }
        private void btnDistubuteAmount_Click(object sender, EventArgs e)
        {            
            try
            {
                //this.c1SinglePayment.CellChanged -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged);

                if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 0)
                {
                    decimal _PatTotAmount = 0;
                    decimal _PatBalAmount = 0;
                    decimal _PatLineAmount = 0;
                    decimal _PatLineDistAmount = 0;
                    decimal _PatLineBalAmount = 0;
                    int _SelectFirstIndex = 0;
                    if (txtCheckRemaining.Text.Trim() != null && txtCheckRemaining.Text.Trim().Length > 0 && txtCheckRemaining.Text.Trim() != "0.00" && txtCheckRemaining.Text.Trim() != "0")
                    {
                        _PatTotAmount = Convert.ToDecimal(txtCheckRemaining.Text.Trim());
                        _PatLineAmount = 0;
                        _PatBalAmount = _PatTotAmount - _PatLineAmount;
                    }

                    if (_PatTotAmount > 0 && c1SinglePayment.Rows.Count > 0)
                    {
                        //for (int i = 1; i <= c1SinglePayment.Rows.Count - 1; i++)
                        for (int i = c1SinglePayment.Rows.Count - 1; i > 0; i--)
                        {                            
                            _PatLineAmount = 0;

                            if (_PatBalAmount > 0)
                            {
                                if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine && c1SinglePayment.GetData(i, COL_NON_SERVICECODE).ToString().ToLower() == "false")
                                {
                                    if (c1SinglePayment.Rows[i].AllowEditing == true)
                                    {
                                        if (_SelectFirstIndex <= 0) { _SelectFirstIndex = i; }

                                        if (c1SinglePayment.GetData(i, COL_PAT_DUE) != null && c1SinglePayment.GetData(i, COL_PAT_DUE).ToString().Trim() != "")
                                        {
                                            _PatLineBalAmount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_PAT_DUE)));
                                        }
                                       
                                        //if (c1SinglePayment.Cols[COL_BadDebt_DUE].Visible)
                                        //{
                                        //    if (_PatLineBalAmount <= 0)
                                        //    {
                                        //        if (c1SinglePayment.GetData(i, COL_BadDebt_DUE) != null && c1SinglePayment.GetData(i, COL_BadDebt_DUE).ToString().Trim() != "")
                                        //        {
                                        //            _PatLineBalAmount = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_BadDebt_DUE).ToString());
                                        //        }
                                        //    }
                                        //}
                                        if (c1SinglePayment.GetData(i, COL_CUR_PAYMENT) != null && c1SinglePayment.GetData(i, COL_CUR_PAYMENT).ToString().Trim() != "")
                                        {
                                            _PatLineAmount = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_PAYMENT).ToString());
                                        }

                                        if (_PatLineBalAmount > 0)
                                        {
                                            if (_PatLineAmount <= 0)
                                            {
                                                if (_PatBalAmount < _PatLineBalAmount)
                                                {
                                                    _PatLineDistAmount = _PatBalAmount;
                                                }
                                                else
                                                {
                                                    _PatLineDistAmount = _PatLineBalAmount;
                                                }
                                                c1SinglePayment.SetData(i, COL_CUR_PAYMENT, _PatLineDistAmount);
                                                _PatBalAmount = _PatBalAmount - _PatLineDistAmount;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }

                        c1SinglePayment.Select(_SelectFirstIndex, COL_CUR_PAYMENT, true);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            {
                //this.c1SinglePayment.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_CellChanged);
            }
        }
        private void btnTraySelection_Click(object sender, EventArgs e)
        {
            GetControlSelection();
            SelectPaymentTray();
            SetControlSelection();
        }
        private bool IsValidForReserveRemaining()
        {
            decimal _AmtToRes = 0;

            try
            {

                if (txtCheckRemaining.Text.Trim().Equals(""))
                { txtCheckRemaining.Text = "0.00"; }
                else { _AmtToRes = Convert.ToDecimal(txtCheckRemaining.Text); }

                if (_AmtToRes == 0) // Reserve amount should not be zero
                {
                    MessageBox.Show("Nothing to Reserve, Please enter some amount.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return true;
        }
        private void btnReserveRemaining_Click(object sender, EventArgs e)
        {
            GetControlSelection();
            //0 ReserveAmount 
            //1 ReserveNote 
            //2 ReserveSubType 
            //3 ReserveNoteOnPrint 
            //4 CPT
            //5 ICD9

            string _AdvCPT = "";
            string _AdvICD9 = "";
            gloGeneralItem.gloItems _AdvList = null;

            if (txtCheckRemaining.Text != null && txtCheckRemaining.Text.Trim().Length > 0)
            {
                //if (Convert.ToDecimal(Convert.ToString(txtCheckRemaining.Text)) >= 0)
                {
                    string[] oList = null;
                    if (btnReserveRemaining.Tag != null)
                    {
                        oList = btnReserveRemaining.Tag.ToString().Split('~');
                    }

                    frmPaymentReserveRemaningV2 ofrmPaymentReserveRemaning = new frmPaymentReserveRemaningV2(gloGlobal.gloPMGlobal.DatabaseConnectionString, _PatientID);
                    if (oList != null && oList.Length == 8)
                    {
                        ofrmPaymentReserveRemaning.ReserveNote = oList[1].ToString();
                        if (oList[3] != null && Convert.ToString(oList[3]).Trim() != "")
                        {
                            ofrmPaymentReserveRemaning.ReserveNoteOnPrint = Convert.ToBoolean(oList[3]);
                        }
                    }
                    if (txtCheckRemaining.Text != null && txtCheckRemaining.Text.Trim().Length > 0)
                    {
                        if (Convert.ToDecimal(Convert.ToString(txtCheckRemaining.Text)) <= 0)
                        {
                            if (oList != null && oList[0] != null && Convert.ToString(oList[0]).Trim() != "")
                            {
                                ofrmPaymentReserveRemaning.ReserveAmount = Convert.ToDecimal(Convert.ToString(oList[0]));

                                decimal _remainingAmt = 0;
                                if (txtCheckRemaining.Text.Trim().Equals(""))
                                { _remainingAmt = 0; }
                                else { _remainingAmt = Convert.ToDecimal(txtCheckRemaining.Text); }

                                if (_remainingAmt < 0)
                                {
                                    ofrmPaymentReserveRemaning.ReserveAllowed = ofrmPaymentReserveRemaning.ReserveAmount;
                                }
                                else
                                {
                                    ofrmPaymentReserveRemaning.ReserveAllowed = ofrmPaymentReserveRemaning.ReserveAmount + _remainingAmt;
                                }

                            }
                        }
                        else
                        {
                            if (oList != null && oList[0] != null && Convert.ToString(oList[0]).Trim() != "")
                            {
                                if (Convert.ToDecimal(Convert.ToString(oList[0])) > 0)
                                {
                                    ofrmPaymentReserveRemaning.ReserveAmount = Convert.ToDecimal(Convert.ToString(oList[0]));

                                    decimal _remainingAmt = 0;
                                    if (txtCheckRemaining.Text.Trim().Equals(""))
                                    { _remainingAmt = 0; }
                                    else { _remainingAmt = Convert.ToDecimal(txtCheckRemaining.Text); }

                                    if (_remainingAmt < 0)
                                    {
                                        ofrmPaymentReserveRemaning.ReserveAllowed = ofrmPaymentReserveRemaning.ReserveAmount;
                                    }
                                    else
                                    {
                                        ofrmPaymentReserveRemaning.ReserveAllowed = ofrmPaymentReserveRemaning.ReserveAmount + _remainingAmt;
                                    }

                                }
                                else
                                {
                                    ofrmPaymentReserveRemaning.ReserveAmount = Convert.ToDecimal(txtCheckRemaining.Text);
                                    ofrmPaymentReserveRemaning.ReserveAllowed = ofrmPaymentReserveRemaning.ReserveAmount;
                                }
                            }
                            else
                            {
                                ofrmPaymentReserveRemaning.ReserveAmount = Convert.ToDecimal(txtCheckRemaining.Text);
                                ofrmPaymentReserveRemaning.ReserveAllowed = ofrmPaymentReserveRemaning.ReserveAmount;
                            }
                        }

                    }
                    if (oList != null && oList[2] != null && Convert.ToString(oList[2]).Trim() != "")
                    {
                        ofrmPaymentReserveRemaning.ReserveSubType = (NoteSubTypeV2)Convert.ToInt32(oList[2]);
                    }
                    else
                    {
                        ofrmPaymentReserveRemaning.ReserveSubType = NoteSubTypeV2.Other;                        
                    }

                    if (oList != null && oList.Length == 8)
                    {
                        if (oList != null && oList[4] != null && Convert.ToString(oList[4]).Trim() != "")
                        {
                            //SLR: Before allocating new one, free previous memeory
                            
                            _AdvList = new gloGeneralItem.gloItems();
                            string[] oCPTList = null;
                            _AdvCPT = oList[4].ToString();
                            oCPTList = _AdvCPT.ToString().Split('|');

                            if (oCPTList != null && oCPTList.Length > 0)
                            {
                                for (int i = 0; i <= oCPTList.Length - 1; i++)
                                {
                                    if (oCPTList[i] != null && oCPTList[i].Trim() != "")
                                    {
                                        string[] oTemp = null;
                                        oTemp = oCPTList[i].Split('^');
                                        if (oTemp != null && oTemp.Length > 0 && oTemp[0].Trim() != "")
                                        { _AdvList.Add(0, oTemp[0].Trim(), oTemp[1].Trim()); }
                                    }
                                }
                            }

                            ofrmPaymentReserveRemaning.oCPTList = _AdvList;
                           // _AdvList.Dispose();
                        }
                    }
                    if (oList != null && oList.Length == 8)
                    {
                        if (oList != null && oList[5] != null && Convert.ToString(oList[5]).Trim() != "")
                        {
                            //SLR: Before allocating new one, free previous memeory
                            
                            _AdvList = new gloGeneralItem.gloItems();
                            string[] oICD9List = null;
                            _AdvICD9 = oList[5].ToString();
                            oICD9List = _AdvICD9.ToString().Split('|');

                            for (int i = 0; i <= oICD9List.Length - 1; i++)
                            {
                                if (oICD9List[i] != null && oICD9List[i].Trim() != "")
                                {
                                    string[] oTemp = null;
                                    oTemp = oICD9List[i].Split('^');
                                    if (oTemp != null && oTemp.Length > 0 && oTemp[0].Trim() != "")
                                    { _AdvList.Add(0, oTemp[0].Trim(), oTemp[1].Trim()); }
                                }
                            }

                            ofrmPaymentReserveRemaning.oICD9List = _AdvList;
                            //_AdvList.Dispose();
                        }
                    }

                    if (oList != null && oList.Length == 8)
                    {
                        if (oList[6] != null && Convert.ToString(oList[6]).Trim() != "")
                        {
                            ofrmPaymentReserveRemaning.ProviderName = Convert.ToString(oList[6]);
                        }
                    }

                    if (oList != null && oList.Length == 8)
                    {
                        if (oList[7] != null && Convert.ToString(oList[7]).Trim() != "")
                        {
                            ofrmPaymentReserveRemaning.ProviderID = Convert.ToInt64(oList[7]);
                        }
                    }

                    //if (ofrmPaymentReserveRemaning.ReserveAmount > 0)
                    //{+
                    ofrmPaymentReserveRemaning.dtReserveForDOS = dtReserveforDOS;
                    ofrmPaymentReserveRemaning.ShowInTaskbar = false;
                    ofrmPaymentReserveRemaning.StartPosition = FormStartPosition.CenterScreen;
                    ofrmPaymentReserveRemaning.ShowDialog(this);
                    if (ofrmPaymentReserveRemaning.DialogResult == DialogResult.OK)
                    {
                        _AdvCPT = "";
                        _AdvICD9 = "";
                        if (ofrmPaymentReserveRemaning.oCPTList != null && ofrmPaymentReserveRemaning.oCPTList.Count > 0)
                        {
                            //SLR: Before allocating new one, free previous memeory
                            
                            _AdvList = ofrmPaymentReserveRemaning.oCPTList;
                            if (_AdvList != null && _AdvList.Count > 0)
                            {
                                for (int i = 0; i <= _AdvList.Count - 1; i++)
                                {
                                    if (_AdvCPT.Trim() == "")
                                    {
                                        if (_AdvList[i] != null && _AdvList[i].Code.ToString().TrimEnd() != "")
                                        { _AdvCPT = _AdvList[i].Code + "^" + _AdvList[i].Description; }
                                    }
                                    else
                                    {
                                        if (_AdvList[i] != null && _AdvList[i].Code.ToString().TrimEnd() != "")
                                        { _AdvCPT = _AdvCPT + "|" + _AdvList[i].Code + "^" + _AdvList[i].Description; }
                                    }
                                }
                                _AdvCPT = _AdvCPT.TrimEnd('|');
                            }
                            //_AdvList.Dispose();
                        }

                        if (ofrmPaymentReserveRemaning.oICD9List != null && ofrmPaymentReserveRemaning.oICD9List.Count > 0)
                        {
                            //SLR: Before allocating new one, free previous memeory
                            
                            _AdvList = ofrmPaymentReserveRemaning.oICD9List;
                            if (_AdvList != null && _AdvList.Count > 0)
                            {
                                for (int i = 0; i <= _AdvList.Count - 1; i++)
                                {
                                    if (_AdvICD9.Trim() == "")
                                    {
                                        if (_AdvList[i] != null && _AdvList[i].Code.ToString().TrimEnd() != "")
                                        { _AdvICD9 = _AdvList[i].Code + "^" + _AdvList[i].Description; }
                                    }
                                    else
                                    {
                                        if (_AdvList[i] != null && _AdvList[i].Code.ToString().TrimEnd() != "")
                                        { _AdvICD9 = _AdvICD9 + "|" + _AdvList[i].Code + "^" + _AdvList[i].Description; }
                                    }
                                }
                                _AdvICD9.TrimEnd('|');
                            }
                            //_AdvList.Dispose();
                        }
                        NoteSubTypeV2 reserveSubType = ofrmPaymentReserveRemaning.ReserveSubType;
                        btnReserveRemaining.Tag = ofrmPaymentReserveRemaning.ReserveAmount + "~" + ofrmPaymentReserveRemaning.ReserveNote + "~" + reserveSubType.GetHashCode() + "~" + ofrmPaymentReserveRemaning.ReserveNoteOnPrint + "~" + _AdvCPT + "~" + _AdvICD9 + "~" + ofrmPaymentReserveRemaning.ProviderName + "~" + ofrmPaymentReserveRemaning.ProviderID;
                        ProviderID = ofrmPaymentReserveRemaning.ProviderID;
                        ProviderName = ofrmPaymentReserveRemaning.ProviderName;
                        nAssociateCollectionAgencyContactID = ofrmPaymentReserveRemaning.CollectionAgencyContactID;
                        dtReserveforDOS = ofrmPaymentReserveRemaning.dtReserveForDOS;
                        //CalculateRemainingAmount();
                    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("You can not reserve zero (0) amount.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}

                    if (ofrmPaymentReserveRemaning != null) { ofrmPaymentReserveRemaning.Dispose(); }
                    CalculateRemainingAmount();

                }
            }
            SetControlSelection();
        }
        private void btnUseReserve_Click(object sender, EventArgs e)
        {
            GetControlSelection();
            bool IsProviderEnable = false;
            try
            {
                if (nPAccountID > 0)
                {
                    DialogResult _dlgRst = DialogResult.None;
                    _dlgRst = SetNewPayment();
                    IsProviderEnable = gloAccountsV2.gloBillingCommonV2.IsPatientReserve_ProviderEnable();
                    if (_dlgRst != DialogResult.Cancel)
                    {
                        frmPaymentUseReserveV2 ofrmPaymentUseReserve;
                        if (_IsPatientAccountFeature)
                        {
                            ofrmPaymentUseReserve = new frmPaymentUseReserveV2(gloGlobal.gloPMGlobal.DatabaseConnectionString, _PatientID, this.nPAccountID);
                        }
                        else
                        {
                            ofrmPaymentUseReserve = new frmPaymentUseReserveV2(gloGlobal.gloPMGlobal.DatabaseConnectionString, _PatientID);
                        }

                        if (mskCloseDate.MaskCompleted == true)
                        {
                            ofrmPaymentUseReserve.CloseDate = Convert.ToDateTime(mskCloseDate.Text);
                        }

                        ofrmPaymentUseReserve.CloseDayTray = lblPaymentTray.Text.Trim();
                        if (IsProviderEnable)
                        { ofrmPaymentUseReserve.Width = 1115; ofrmPaymentUseReserve.StartPosition = FormStartPosition.CenterScreen; }
                        else { ofrmPaymentUseReserve.Width = 966; ofrmPaymentUseReserve.StartPosition = FormStartPosition.CenterScreen; }
                        ofrmPaymentUseReserve.ShowDialog(this);
                        oPatientControl.RefreshBalances();
                        if (ofrmPaymentUseReserve.DialogResult == DialogResult.OK)
                        {
                            btnUseReserve.Visible = false;
                            btnClearReserve.Visible = true;
                            btnReserveRemaining.Visible = false;
                            txtCheckAmount.ReadOnly = true;
                            txtCheckAmount.BackColor = Color.White;
                            decimal reserveAmount = ofrmPaymentUseReserve.SelectedUseReserveAmount;
                            txtCheckAmount.Text = reserveAmount.ToString();
                            btnUseReserve.Tag = ofrmPaymentUseReserve.oSeletedReserveItems;

                            tls_btnReceipt.Enabled = false;
                            tls_btnDefaultReceipt.Enabled = false;
                            _useReserves = true;
                            CalculateRemainingAmount();

                            lblPayType.Enabled = false;
                            cmbPayMode.Enabled = false;
                            lblCheckNo.Enabled = false;
                            txtCheckNumber.Enabled = false;
                            // btnLoadCheck.Enabled = false;
                            //btnRemoveCheck.Enabled = false;
                            lblPatientSearch.Enabled = false;
                            lblCheckDate.Enabled = false;
                            mskCheckDate.Enabled = false;
                            lblCardType.Enabled = false;
                            cmbCardType.Enabled = false;
                            lblCardAuthorizationNo.Enabled = false;
                            txtCardAuthorizationNo.Enabled = false;
                            lblExpiryDate.Enabled = false;
                            mskCreditExpiryDate.Enabled = false;

                            _IsUseReserveEntry = true;
                            txtPatientSearch.Enabled = false;
                            _useReserves = true;
                        }
                        else
                        { txtPatientSearch.Enabled = true; lblPatientSearch.Enabled = true; }
                        ofrmPaymentUseReserve.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("Please select the patient.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPatientSearch.Focus(); txtPatientSearch.Select();
                }
                AllowEditValidation();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                SetControlSelection();
            }
        }
        private void btnClearReserve_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear reserve amount?", _messageboxcaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lblPatientSearch.Enabled = true;
                lblPayType.Enabled = true;
                cmbPayMode.Enabled = true;
                lblCheckNo.Enabled = true;
                txtCheckNumber.Enabled = true;
                // btnLoadCheck.Enabled = true;
                //btnRemoveCheck.Enabled = true;
                lblCheckDate.Enabled = true;
                mskCheckDate.Enabled = true;
                lblCardType.Enabled = true;
                cmbCardType.Enabled = true;
                lblCardAuthorizationNo.Enabled = true;
                txtCardAuthorizationNo.Enabled = true;
                lblExpiryDate.Enabled = true;
                mskCreditExpiryDate.Enabled = true;

                ClearFormData();

                btnUseReserve.Visible = true;
                btnClearReserve.Visible = false;
                btnReserveRemaining.Visible = true;
                txtCheckAmount.ReadOnly = false;
                txtCheckAmount.Text = "";
                btnUseReserve.Tag = null;
                tls_btnReceipt.Enabled = true;
                tls_btnDefaultReceipt.Enabled = true;
                _IsUseReserveEntry = false;
                txtPatientSearch.Enabled = true;


            }
        }
        #endregion " Form Button Click "

        #region " Short Cut Menu Click Events "
        private void mnuPayment_Save_Click(object sender, EventArgs e)
        {
            tls_btnSave_Click(null, null);
        }
        private void mnuPayment_SavenClose_Click(object sender, EventArgs e)
        {
            tls_btnSaveNClose_Click(null, null);
        }
        private void mnuPayment_PaymentGrid_Click(object sender, EventArgs e)
        {
            try
            {
                c1SinglePayment.Select();
                c1SinglePayment.Focus();
                if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                {
                    for (int i = 1; i <= c1SinglePayment.Rows.Count - 1; i++)
                    {
                        if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                        { c1SinglePayment.Select(i, COL_CUR_PAYMENT, true); break; }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }
        private void mnuPayment_NextServiceLine_Click(object sender, EventArgs e)
        {
            int _currentRowIndex = 0;
            int _nextRowIndex = 0;
            try
            {
                if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                {
                    _currentRowIndex = c1SinglePayment.RowSel;

                    for (int rIndex = _currentRowIndex + 1; rIndex < c1SinglePayment.Rows.Count; rIndex++)
                    {
                        if (c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) != null)
                        {
                            if ((ColServiceLineType)c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine
                                && c1SinglePayment.Rows[rIndex].AllowEditing == true)
                            { _nextRowIndex = rIndex; break; }
                        }
                    }

                    if (_nextRowIndex > 0)
                    {
                        c1SinglePayment.Select(_nextRowIndex, COL_CUR_PAYMENT, true);
                        c1SinglePayment.RowSel = _nextRowIndex;
                        c1SinglePayment.Row = _nextRowIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }
        private void mnuPayment_PrvServiceLine_Click(object sender, EventArgs e)
        {
            int _currentRowIndex = 0;
            int _prvRowIndex = 0;

            try
            {

                if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                {
                    _currentRowIndex = c1SinglePayment.RowSel;

                    for (int rIndex = _currentRowIndex - 1; rIndex > 0; rIndex--)
                    {
                        if (c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) != null)
                        {
                            if ((ColServiceLineType)c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                            { _prvRowIndex = rIndex; break; }
                        }
                    }

                    if (_prvRowIndex > 0)
                    {
                        c1SinglePayment.Select(_prvRowIndex, COL_CUR_PAYMENT, true);
                        c1SinglePayment.RowSel = _prvRowIndex;
                        c1SinglePayment.Row = _prvRowIndex;
                    }
                }
            }
            catch //(Exception ex)
            {
            }
        }
        private void mnuPayment_DistributePayment_Click(object sender, EventArgs e)
        {
            btnDistubuteAmount_Click(null, null);
        }
        #endregion " Short Cut Menu Click Events "

        #region "Payment Collect and Apply Changed"
        private void rbPayType_Payment_CheckedChanged(object sender, EventArgs e)
        {
            lblCheckAmount.Visible = true;
            txtCheckAmount.Visible = true;
            label6.Visible = true;
            lblCheckRemaining.Visible = true;
            // txtCheckRemaining.Visible = true;
            lblShowRemaining.Visible = true;

            btnDistubuteAmount.Visible = true;
            btnReserveRemaining.Visible = true;
            btnUseReserve.Visible = true;
            btnClearReserve.Visible = false;
            txtCheckAmount.ReadOnly = false;

            DesignPaymentGrid(c1SinglePayment);
            DesignPaymentGrid(c1SinglePaymentTotal);
            pnlSinglePayment.Visible = true;
            MaskedTextBox mskDate = (MaskedTextBox)mskCloseDate;
            mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (cmbPayMode.SelectedIndex >= 0)
            {
                if (cmbPayMode.Text == PaymentMode.Check.ToString())
                {
                    if (mskDate.Text == "")
                    {
                        mskCheckDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    }
                    else
                    { mskCheckDate.Text = mskCloseDate.Text; }
                }
            }
            mskDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            //txtCheckNumber.Text = "";
            txtCheckAmount.Text = "";
            txtCheckRemaining.Text = "";
            lblShowRemaining.Text = "";

            tls_btnReceipt.Enabled = true;
            tls_btnDefaultReceipt.Enabled = true;
            lblPayType.Enabled = true;
            cmbPayMode.Enabled = true;
            lblCheckNo.Enabled = true;
            txtCheckNumber.Enabled = true;
            lblCheckDate.Enabled = true;
            mskCheckDate.Enabled = true;
            lblCardType.Enabled = true;
            cmbCardType.Enabled = true;
            lblCardAuthorizationNo.Enabled = true;
            txtCardAuthorizationNo.Enabled = true;
            lblExpiryDate.Enabled = true;
            mskCreditExpiryDate.Enabled = true;


            //Temp Comments
            // if (oPatientControl != null) { oPatientControl.SetClaimNoSearch(false); }
        }
        private void rbPayType_Refund_CheckedChanged(object sender, EventArgs e)
        {
            lblCheckAmount.Visible = true;
            txtCheckAmount.Visible = true;
            label6.Visible = true;
            lblCheckRemaining.Visible = true;
            //txtCheckRemaining.Visible = true;
            lblShowRemaining.Visible = true;

            btnDistubuteAmount.Visible = true;
            btnReserveRemaining.Visible = true;
            btnUseReserve.Visible = true;
            btnClearReserve.Visible = false;
            txtCheckAmount.ReadOnly = false;

            DesignPaymentGrid(c1SinglePayment);
            DesignPaymentGrid(c1SinglePaymentTotal);
            pnlSinglePayment.Visible = true;

            //txtCheckNumber.Text = "";
            txtCheckAmount.Text = "";
            txtCheckRemaining.Text = "";
            lblShowRemaining.Text = "";
            mskCheckDate.Text = "";
            MaskedTextBox mskDate = (MaskedTextBox)mskCloseDate;
            mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (cmbPayMode.SelectedIndex >= 0)
            {
                if (cmbPayMode.Text == PaymentMode.Check.ToString())
                {
                    if (mskDate.Text == "")
                    {
                        mskCheckDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    }
                    else
                    { mskCheckDate.Text = mskCloseDate.Text; }
                }
            }
            mskDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }
        private void rbPayType_ExitingPayment_CheckedChanged(object sender, EventArgs e)
        {
            lblCheckAmount.Visible = true;
            txtCheckAmount.Visible = true;
            label6.Visible = true;
            lblCheckRemaining.Visible = true;
            lblShowRemaining.Visible = true; //txtCheckRemaining.Visible = true;

            btnDistubuteAmount.Visible = true;
            btnReserveRemaining.Visible = true;
            btnUseReserve.Visible = true;
            btnClearReserve.Visible = false;
            txtCheckAmount.ReadOnly = false;

            DesignPaymentGrid(c1SinglePayment);
            DesignPaymentGrid(c1SinglePaymentTotal);
            pnlSinglePayment.Visible = true;

            //txtCheckNumber.Text = "";
            txtCheckAmount.Text = "";
            lblShowRemaining.Text = ""; //txtCheckRemaining.Text = "";

            MaskedTextBox mskDate = (MaskedTextBox)mskCloseDate;
            mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            if (cmbPayMode.SelectedIndex >= 0)
            {
                if (cmbPayMode.Text == PaymentMode.Check.ToString())
                {
                    if (mskDate.Text == "")
                    {
                        mskCheckDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    }
                    else
                    { mskCheckDate.Text = mskCloseDate.Text; }
                }
            }
            mskDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }
        #endregion

        private void tls_btnCharge_Click(object sender, EventArgs e)
        {
            GetControlSelection();
            if (_IsAllDatesValid == true)
            {
                OpenModifyCharges();
                AllowEditValidation();
                DisplayGlobalPeriodAlert(_PatientID); 
            }
            SetControlSelection();
        }

        private void c1SinglePayment_DoubleClick(object sender, EventArgs e)
        {
            if (c1SinglePayment.ColSel != COL_CUR_PAYMENT && c1SinglePayment.ColSel != COL_CUR_ADJ_TYPECODE && c1SinglePayment.ColSel != COL_CUR_ADJ_AMOUNT)
            {
                GetControlSelection();
                OpenModifyCharges();
                AllowEditValidation();
                DisplayGlobalPeriodAlert(_PatientID);
                SetControlSelection();
            }
            
        }
        private void OpenModifyCharges()
        {
            try
            {
                Int64 _transactionId = 0;

                #region "Find Claim to open"

                if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                {
                    if (Convert.ToInt64(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_TRACK_TRN_ID)) == 0)
                    {
                        if (c1SinglePayment.GetData(c1SinglePayment.RowSel + 1, COL_TRACK_TRN_ID) != null && c1SinglePayment.GetData(c1SinglePayment.RowSel + 1, COL_TRACK_TRN_ID).ToString().Trim() != "")
                        {
                            _transactionId = Convert.ToInt64(c1SinglePayment.GetData(c1SinglePayment.RowSel + 1, COL_TRACK_TRN_ID));
                            _PatientID = Convert.ToInt64(c1SinglePayment.GetData(c1SinglePayment.RowSel + 1, COL_PATIENTID));
                        }
                    }
                    else
                    {
                        if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_TRACK_TRN_ID) != null && c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_TRACK_TRN_ID).ToString().Trim() != "")
                        {
                            _transactionId = Convert.ToInt64(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_TRACK_TRN_ID));
                            _PatientID = Convert.ToInt64(c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_PATIENTID));
                        }
                    }
                }

                #endregion


                if (_transactionId > 0 && _PatientID > 0)
                {
                    #region " Modify Charges Claim Code "

                    Boolean _IsModified = false;

                    gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
                    _IsModified = ogloBilling.ShowModifyChargesFromPatientPayment(_PatientID, _transactionId, true,this);
                    ogloBilling.Dispose();
                    oPatientControl.RefreshBalances();
                    // If changes has been modified then only refresh the claim else skip
                    if (_IsModified == true)
                    {
                        //frmPaymentInsurace_Load(null, null);
                        RefreshFormData();
                    }

                    #endregion
                }
                else
                {
                    MessageBox.Show("Please select the claim", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch //(Exception ex)
            {


            }
        }

        private int getWidthofListItems(string _text, Label label)
        {
            //Code Review Changes: Dispose Graphics object
            int width = 0;
            Graphics g = this.CreateGraphics();
            if (g!=null)
            {
                SizeF s = g.MeasureString(_text, label.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }
            
            return width;
        }
        private void c1SinglePayment_Click(object sender, EventArgs e)
        {
            //_IsC1SinglePaymentClicked = true;
        }
        private void mskCloseDate_Validated(object sender, EventArgs e)
        {
            if (isclosecheck == false)
            {
                mskCloseDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

                if (mskCloseDate.Text.Length > 0)
                {
                    _IsClaimDateMessageShown = true;
                    AllowClaimsEdit();
                    _afterCloseDateChanged = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                }
                else
                {
                    _IsClaimDateMessageShown = false;
                    AllowClaimsEdit();
                    _afterCloseDateChanged = 0;
                }
                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            }

        }
        private void AllowEditValidation()
        {

            if (IsValidDate(mskCloseDate.Text))
            {
                mskCloseDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (mskCloseDate.Text.Length > 0)
                {
                    _IsClaimDateMessageShown = true;
                    AllowClaimsEdit();
                    _afterCloseDateChanged = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                }
                else
                {
                    _IsClaimDateMessageShown = false;
                    AllowClaimsEdit();
                    _afterCloseDateChanged = 0;
                }
            }
            else
            {
                _IsClaimDateMessageShown = false;
                AllowClaimsEdit();
                _afterCloseDateChanged = 0;
            }
            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }
        private void tls_btnClose_MouseDown(object sender, MouseEventArgs e)
        {
            isclosecheck = true;
        }
        private void lblPaymentTray_MouseEnter(object sender, EventArgs e)
        {
            try
            {

                label = (Label)sender;

                if (lblPaymentTray.Text != null && lblPaymentTray.Text != "")
                {
                    if (getWidthofListItems(Convert.ToString(lblPaymentTray.Text), lblPaymentTray) >= lblPaymentTray.Width - 20)
                    {
                        //tooltip_Billing.Show(Convert.ToString(((DataRowView)cmbInsuranceCompany.Items[cmbInsuranceCompany.SelectedIndex])["sDescription"]), cmbInsuranceCompany,0, System.Windows.Forms.Control.MousePosition.Y - 230);
                        tooltip_Billing.SetToolTip(lblPaymentTray, lblPaymentTray.Text);
                    }
                    else
                    {
                        this.tooltip_Billing.Hide(lblPaymentTray);
                    }
                }
                else
                {
                    this.tooltip_Billing.Hide(lblPaymentTray);
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
        }
        private int getWidthofListItems(string _text, ComboBox combo)
        {
            //Code Review Changes: Dispose Graphics object
            int width = 0;
            Graphics g = this.CreateGraphics();
            if (g!=null)
            {
                SizeF s = g.MeasureString(_text, combo.Font);
                width = Convert.ToInt32(s.Width);
                //Dispose graphics object
                g.Dispose();
            }
            
            return width;
        }
        private void cmbCardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCardType.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbCardType.Items[cmbCardType.SelectedIndex])["sCreditCardDesc"]), cmbCardType) >= cmbCardType.DropDownWidth - 18)
                    tooltip_Billing.SetToolTip(cmbCardType, Convert.ToString(((DataRowView)cmbCardType.Items[cmbCardType.SelectedIndex])["sCreditCardDesc"]));//, cmbCardType, 0, cmbCardType.Bottom - 40);
                else
                    tooltip_Billing.SetToolTip(cmbCardType, "");

            }
        }
        private void cmbCardType_MouseEnter(object sender, EventArgs e)
        {
            if (cmbCardType.SelectedItem != null)
            {
                if (getWidthofListItems(Convert.ToString(((DataRowView)cmbCardType.Items[cmbCardType.SelectedIndex])["sCreditCardDesc"]), cmbCardType) >= cmbCardType.DropDownWidth - 18)
                    tooltip_Billing.SetToolTip(cmbCardType, Convert.ToString(((DataRowView)cmbCardType.Items[cmbCardType.SelectedIndex])["sCreditCardDesc"]));//, cmbCardType, 0, cmbCardType.Bottom - 40);
                else
                    tooltip_Billing.SetToolTip(cmbCardType, "");

            }
        }

        #region "Save Methods"      
        private Int64 SavePayment()
        {
            Int64 _retPayId = 0;

            try
            {  
                c1SinglePayment.FinishEditing();
                if (oPatientControl.PatientID == 0)
                {
                    //To Save the Reserves against the  Owner of the Acc When All Acc Pat is Selected 
                    DataTable dtPatients = null;
                    dtPatients = PatientStripControl.GetAccountPatients(oPatientControl.PAccountID);
                    if (dtPatients != null && dtPatients.Rows.Count > 0)
                    {
                        _PatientID = dtPatients.Rows[0]["nPatientID"] == DBNull.Value ? 0 : Convert.ToInt64(dtPatients.Rows[0]["nPatientID"]);
                    }
                }

                string sCheckAmount = txtCheckAmount.Text;
                if (_IsPaymentCorrectionMode == false)
                {
                    if (_IsUseReserveEntry == false)
                    {
                        _retPayId = SavePatientPayment();
                    }
                    else
                    {
                        _retPayId = SavePatientUseReservePayment();
                    }
                }
                else
                {
                    _retPayId = SavePatientCorrectionPayment();
                }
                oPatientControl.RefreshBalances();
                if (_retPayId > 0 && IsFromCleargagePosting== true)
                {
                    UpdateCleargagePaymentPosting(_retPayId);
                }
                if (_retPayId!=0 && nCG_OneTimePaymentID!=0)
                {
                    if (!IsCleargageDataChanged(nCG_OneTimePaymentID, sCheckAmount))
                    {
                        ClsCleargagePaymentPosting oClsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
                        oClsCleargagePaymentPosting.UpdateCleargageTransaction(nCG_OneTimePaymentID, _retPayId, null);
                        if (oClsCleargagePaymentPosting != null)
                        {
                            oClsCleargagePaymentPosting.Dispose();
                            oClsCleargagePaymentPosting = null;
                        } 
                    }
                }
                return _retPayId;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true );
              //  MessageBox.Show("Error in Save Payment Method ",_messageboxcaption,MessageBoxButtons.OK ,MessageBoxIcon.Error) 
                return 0;

            }
            finally
            {

            }
            //return _retPayId;
           
        }

        private void UpdateCleargagePaymentPosting(Int64 _retPayId)
        {
            Int64 returnCGTransactionID = 0;
            string EncounterID = "";
            Int64 CleargageFileID = 0;
            bool result = false;
            ClsCleargagePaymentPosting oclsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
            try
            {
                if (_retPayId > 0)
                {
                    if (dtCleargagePaymentDetails != null && dtCleargagePaymentDetails.Rows.Count > 0)
                    {
                        EncounterID = Convert.ToString(dtCleargagePaymentDetails.Rows[0]["EncounterID"]);
                        CleargageFileID = Convert.ToInt64(dtCleargagePaymentDetails.Rows[0]["nFileID"]);
                        string CG_TransactionID = Convert.ToString(dtCleargagePaymentDetails.Rows[0]["CG_TransactionID"]);
                        string CG_OriginalTransactionID = Convert.ToString(dtCleargagePaymentDetails.Rows[0]["CG_OriginalTransactionID"]);
                        string ReferenceNo = Convert.ToString(dtCleargagePaymentDetails.Rows[0]["ReferenceNumber"]);
                       // string sAction = Convert.ToString(dtCleargagePaymentDetails.Rows[0]["Action"]);
                        string PatientCode = Convert.ToString(dtCleargagePaymentDetails.Rows[0]["PatientCode"]);
                        string PatientName = Convert.ToString(dtCleargagePaymentDetails.Rows[0]["PatientName"]);
                        string PlanID = Convert.ToString(dtCleargagePaymentDetails.Rows[0]["PaymentPlanID"]);
                        decimal Amount = Convert.ToDecimal(dtCleargagePaymentDetails.Rows[0]["Amount"]);
                        string PaymentMethod = Convert.ToString(dtCleargagePaymentDetails.Rows[0]["PaymentMethod"]);
                        DateTime TimeStamp = Convert.ToDateTime(dtCleargagePaymentDetails.Rows[0]["TimeStamp"]);
                        string AccountType = Convert.ToString(dtCleargagePaymentDetails.Rows[0]["AccountType"]);
                        string AccountNo = Convert.ToString(dtCleargagePaymentDetails.Rows[0]["AccountNumber"]);

                        result=oclsCleargagePaymentPosting.UpdateStatusAsPosted(EncounterID, CleargageFileID, CG_TransactionID, CG_OriginalTransactionID, ReferenceNo, Convert.ToString(Actions.PAYMENT));
                        if (result == true)
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Modify, "Manual Cleargage payment file status updated for encounter ID : " + EncounterID + " and System credit ID : " + _retPayId, _PatientID, CleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                            result = false;
                        }
                        result = oclsCleargagePaymentPosting.UpdateMasterDetailsStatus(CleargageFileID);
                        if (result == true)
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Modify, "Manual Cleargage payment file status updated for CleargageFileID : " + CleargageFileID, _PatientID, CleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                        }
                        returnCGTransactionID = oclsCleargagePaymentPosting.SavePaymentPostingDetails(returnCGTransactionID, PatientCode, PatientName, PlanID, CG_TransactionID, CG_OriginalTransactionID, Amount, PaymentMethod, Convert.ToString(Actions.PAYMENT), TimeStamp, "", "", AccountType, AccountNo, ReferenceNo, _retPayId, EncounterID, CleargageFileID);
                        if (returnCGTransactionID > 0)
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Save, "Manual Cleargage payment save credit details for encounter ID : " + EncounterID + " and System credit ID : " + _retPayId, _PatientID, CleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                        }
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Save, "Manual Cleargage Payment for encounter ID : " + EncounterID + " and System credit ID : " + _retPayId, _PatientID, CleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Save, "Manual Cleargage Payment Exception occured :"+ ex.ToString() +" for encounter IDs : " + EncounterID, _PatientID, CleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, false);
            }
            finally
            {
                if (oclsCleargagePaymentPosting != null)
                {
                    oclsCleargagePaymentPosting.Dispose();
                    oclsCleargagePaymentPosting = null;
                }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.CleargagePayment, gloAuditTrail.ActivityType.Close, "Manual Cleargage Payment Closed", _PatientID, CleargageFileID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
            }
          
        }
        private Int64 SavePatientPayment()
        {
            Int64 _retPayId = 0;
            Int32 row_num = 0;
         //   int rowDebitIndex = 0;
            DataTable _dtUniqueIDs = null;//SLR: new is not needed
            DataTable _dtUniqueCreditID = null;//SLR: new is not needed
            DataTable _dtUniqueReserveIDs = null;//SLR: new is not needed
            Int64 _nEOBID = 0;
            Int64 _nCreditID = 0;
            c1SinglePayment.FinishEditing();
            gloBilling.gloBilling ogloBilling = null;
            try
            {
               
                Int64 _CloseDayTrayID = 0;
                string _CloseDayTrayName = "";

                // Remove $0.00 bug from insurance claim followup when patient did full patient payment for claim
                _Arr_ClaimFollowupRemove_TransactionMSTID = new ArrayList();
                _Arr_ClaimFollowupRemove_TrackTransactionID = new ArrayList();
                //


                if (oPatientControl.PatientID == 0)
                {
                    //To Save the Reserves against the  Owner of the Acc When All Acc Pat is Selected 
                    DataTable dtPatients = null;
                    dtPatients = PatientStripControl.GetAccountPatients(oPatientControl.PAccountID);
                    if (dtPatients != null && dtPatients.Rows.Count > 0)
                    {
                        _PatientID = dtPatients.Rows[0]["nPatientID"] == DBNull.Value ? 0 : Convert.ToInt64(dtPatients.Rows[0]["nPatientID"]);
                    }
                }

                if ((txtCheckAmount.Text.Trim().Length > 0 && Convert.ToDecimal(txtCheckAmount.Text) > 0) || _IsAdjustmentMode == true || _IsPaymentCorrectionMode == true)
                {

                    #region "Payment Tray"
                    _CloseDayTrayID = Convert.ToInt64(lblPaymentTray.Tag.ToString().Trim());

                    _CloseDayTrayName = lblPaymentTray.Text;
                    #endregion

                    #region " Master Data "

                    _dtUniqueCreditID = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);
                    if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                    {
                        _nCreditID = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID"].ToString());
                        _nEOBID = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                    }

                    SetCreditsDetails(dsInsurancePayment_TVP, _nCreditID);


                    #region "Payment Master Note"
                    //Notes if any to main payment to all claim OR main payment to reserve account
                    if (txtPayMstNotes.Text.Trim().Length > 0)
                    {
                        #region "General Note"
                   //     int rowNum = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Add();
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = _nCreditID;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                        if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                        {
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["ID2"].ToString());
                        }

                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "";
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = txtPayMstNotes.Text.Trim();
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = chkPayMstIncludeNotes.Checked;
                        if (txtCheckAmount.Text.Trim() != "")
                        {
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = Convert.ToDecimal(txtCheckAmount.Text.Trim());
                        }
                        if (mskCloseDate.MaskCompleted == true)
                        {
                            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);

                        }
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = NoteTypeV2.Payment_Patient.GetHashCode();
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = NoteSubTypeV2.MasterPayment.GetHashCode();
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = _ClinicID;

                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = BillingNoteTypeV2.None.GetHashCode();
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = _UserId;

                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DBNull.Value;
                        if (mskCloseDate.MaskCompleted == true)
                        {
                            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                        }
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = "";
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].AcceptChanges();
                        #endregion
                    }
                    #endregion


                    #endregion

                    if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 0)
                    {
                        for (int rowIndex = 1; rowIndex <= c1SinglePayment.Rows.Count - 1; rowIndex++)
                        {
                            if (c1SinglePayment.GetData(rowIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(rowIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.Claim)
                            {
                                int _claimStartIndex = Convert.ToInt32(c1SinglePayment.GetData(rowIndex, COL_CELLRANGE_R1)) + 1;
                                int _claimEndIndex = Convert.ToInt32(c1SinglePayment.GetData(rowIndex, COL_CELLRANGE_R2));
                                bool _hasDataToSave = false;
                                _dtUniqueIDs = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(c1SinglePayment.Rows.Count - 1);
                                for (int index = _claimStartIndex; index <= _claimEndIndex; index++)
                                {
                                    if (
                                        (c1SinglePayment.GetData(index, COL_CUR_PAYMENT) != null
                                        && Convert.ToString(c1SinglePayment.GetData(index, COL_CUR_PAYMENT)).Trim() != ""
                                        && Convert.ToDecimal(c1SinglePayment.GetData(index, COL_CUR_PAYMENT)) > 0) ||
                                        (c1SinglePayment.GetData(index, COL_CUR_ADJ_AMOUNT) != null
                                        && Convert.ToString(c1SinglePayment.GetData(index, COL_CUR_ADJ_AMOUNT)).Trim() != ""
                                        && Convert.ToDecimal(c1SinglePayment.GetData(index, COL_CUR_ADJ_AMOUNT)) != 0)
                                       )
                                    {
                                        _hasDataToSave = true;
                                        break;
                                    }

                                }

                                if (_hasDataToSave == false) { continue; }

                                for (int i = _claimStartIndex; i <= _claimEndIndex; i++)
                                {
                                    if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                                    {
                                        if (
                                        (c1SinglePayment.GetData(i, COL_CUR_PAYMENT) != null
                                        && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)).Trim() != ""
                                        && Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)) > 0) ||
                                        (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null
                                        && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != ""
                                        && Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)) != 0)
                                       )
                                        {
                                            _nTransactionPatientID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_PATIENTID));
                                            #region "EOB Data Save"
                                            decimal _fillPayAmt = 0; decimal _fillAdjAmt = 0;
                                            if (!_IsPaymentCorrectionMode)
                                            {


                                                dsInsurancePayment_TVP.Tables["EOB"].Rows.Add();
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                                                if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                {
                                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["ID"].ToString());
                                                }
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPatientID"] = _nTransactionPatientID;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPatientInsuranceID"] = 0;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nContactID"] = 0;  
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nInsCompanyID"] = 0;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID)); ;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.PatientPayment.GetHashCode();
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sEntryDesc"] = "PatientPayment";
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));

                                                if (mskCloseDate.MaskCompleted == true)
                                                {
                                                    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                                                }

                                                if (c1SinglePayment.GetData(i, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)).Trim() != "")
                                                {
                                                    _fillPayAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)));
                                                }
                                                if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                {
                                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dWriteoffAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT));
                                                }

                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dPaymentAmount"] = _fillPayAmt;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPaymentTrayID"] = _CloseDayTrayID;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sPaymentTrayDesc"] = _CloseDayTrayName;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nVoidType"] = 0;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPAccountID"] = this.nPAccountID;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nGuarantorID"] = this.nGuarantorID;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nAccountPatientID"] = this.nAccountPatientID;

                                                if (c1SinglePayment.GetData(i, COL_TOTALCHARGE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_TOTALCHARGE)).Trim() != "")
                                                {
                                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dTotalChargeAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_TOTALCHARGE));
                                                }

                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEXTID"] = 0;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCreditIDEXT"] = _nCreditID;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBIDEXT"] = _nEOBID;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBDetailIDEXT"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID"].ToString());
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sNextActionEXT"] = "";
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sNextPartyEXT"] = "";
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nNextPartyIDEXT"] = 0;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtCreatedDateTimeEXT"] = DateTime.Now;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtModifiedDateTimeEXT"] = DateTime.Now;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtPaymentVoidDateTimeEXT"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nUserIDEXT"] = _UserId;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sUserNameEXT"] = _UserName;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["bIsERAPostEXT"] = false;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nSVCIDEXT"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sMachineNameEXT"] = Environment.MachineName;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sSiteIDEXT"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sVersionEXT"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nClinicIDEXT"] = _ClinicID;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCLPIdEXT"] = 0;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionEXT"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionDetailEXT"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                                if (IsPaymentMarkforcollectionAgency)
                                                {
                                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCollectionAgencyContactID"] = CollectionAgencyContactID;//(Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID)), Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID)));
                                                }
                                                else
                                                {
                                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCollectionAgencyContactID"] = 0;
                                                }
                                                dsInsurancePayment_TVP.Tables["EOB"].AcceptChanges();


                                                #region " Set Line Reason Codes for Adjustment "

                                                if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                {
                                                    if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))) != 0)
                                                    {
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Add();
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nID"] = 0;
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nClaimNo"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                                                        if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                        {
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID2"].ToString());
                                                        }

                                                        if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                                                        {
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                                                        }
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));


                                                        if (c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE)).Trim() != "")
                                                        { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sReasonCode"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE)).Trim(); }
                                                        if (c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION)).Trim() != "")
                                                        { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sReasonDescription"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION)).Trim(); }

                                                        if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                        { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["dReasonAmount"] = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); }

                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nClinicID"] = _ClinicID;
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nType"] = EOBCommentTypeV2.SystemReasonCode.GetHashCode();
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["bIsVoid"] = 0;
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnDtlID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnLineNo"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sSubClaimNo"] = Convert.ToString(c1SinglePayment.GetData(i, COL_SUB_CLAIM_NO));
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nVoidType"] = 0;
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["bIsPaymentVoid"] = 0;
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nPaymentVoidCloseDate"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nSubType"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].AcceptChanges();
                                                    }
                                                }

                                                #endregion " Set Line Reason Codes "
                                            }
                                            #endregion "EOB Data Save"

                                            #region "Debits"

                                            dsInsurancePayment_TVP.Tables["Debits"].Rows.Add();
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCredit_RefID"] = _nCreditID;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                                            if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                            {
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["ID"].ToString());
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["ID2"].ToString());
                                            }
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientInsuranceID"] = 0;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nContactID"] = 0;                                           
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nInsCompanyID"] = 0;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientID"] = _nTransactionPatientID;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nGuarantorID"] = this.nGuarantorID;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPAccountID"] = this.nPAccountID;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nAccountPatientID"] = this.nAccountPatientID;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.PatientPayment.GetHashCode();
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sEntryDesc"] = "PatientPayment";
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTDesc"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

                                            if (c1SinglePayment.GetData(i, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)).Trim() != "")
                                            {
                                                _fillPayAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)));
                                            }
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dPaymentAmount"] = _fillPayAmt;

                                            if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                            { _fillAdjAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); }
                                            else
                                            { _fillAdjAmt = 0; }

                                            //if (_fillAdjAmt > 0)
                                            //{
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dOtherAdjustmentAmount"] = _fillAdjAmt;
                                            //}
                                            //else
                                            //{
                                            //    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dOtherAdjustmentAmount"] = 0;
                                            //}

                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWriteoffAmount"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWithholdAmount"] = DBNull.Value;

                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentTrayID"] = _CloseDayTrayID;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentTrayDesc"] = _CloseDayTrayName;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nUserID"] = _UserId;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sUserName"] = _UserName;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nVoidType"] = 0;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidDateTime"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidCloseDate"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidDateTime"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsERAPost"] = false;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nClinicID"] = _ClinicID;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCreatedDateTime"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sSiteID"] = DBNull.Value;
                                            if (IsPaymentMarkforcollectionAgency)
                                            {
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCollectionAgencyContactID"] = CollectionAgencyContactID; //gloPatientPaymentV2.getCollectionAgencyContactID(Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID)), Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID)));
                                            }
                                            else
                                            {
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCollectionAgencyContactID"] = 0;
                                            }
                                            dsInsurancePayment_TVP.Tables["Debits"].AcceptChanges();
                                            row_num = row_num + 1;
                                            #endregion "Debits"

                                            //***********************
                                            // Remove $0.00 bug from insurance claim followup when patient did full patient payment for claim.
                                            _Arr_ClaimFollowupRemove_TransactionMSTID.Add(Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID)));
                                            _Arr_ClaimFollowupRemove_TrackTransactionID.Add(Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID)));
                                            //**********************
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //Patient Payment Reserve Start
                    if (!_IsPaymentCorrectionMode)
                    {
                        if (btnReserveRemaining.Tag != null && btnReserveRemaining.Tag.ToString().Trim().Length > 0)
                        {

                            string[] oList = null;
                            if (btnReserveRemaining.Tag != null)
                            {
                                oList = btnReserveRemaining.Tag.ToString().Split('~');
                            }
                            if (oList != null && oList.Length == 8)
                            {
                                //...Condition added to avoid zero reserve entry in debit table
                                if (oList[0] != null && Convert.ToString(oList[0]).Trim() != "" && Convert.ToDecimal(oList[0]) > 0)
                                {


                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows.Add();
                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nReserveID"] = 0;
                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nCredits_RefID"] = _nCreditID;
                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dReserveAmount"] = Convert.ToDecimal(oList[0]);
                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nReserveType"] = ReserveEntryTypeV2.PatientReserve.GetHashCode();
                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nInsCompanyID"] = 0;
                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nPatientID"] = _PatientID;
                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nUserID"] = _UserId;
                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["sUserName"] = _UserName;
                                    if (mskCloseDate.MaskCompleted == true)
                                    {
                                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                                    }
                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nVoidType"] = 0;
                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nPAccountID"] = this.nPAccountID;
                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nGuarantorID"] = this.nGuarantorID;
                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nAccountPatientID"] = this.nAccountPatientID;
                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtPaymentVoidDateTime"] = DBNull.Value;
                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtCreatedDateTime"] = DBNull.Value;
                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["sReserveNote"] = "Reserved";
                                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;                                    
                                    dsInsurancePayment_TVP.Tables["Reserves"].AcceptChanges();

                                    dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Add();
                                    dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nEOBPaymentID"] = _nCreditID;
                                    dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nEOBPaymentDetailID"] = 0;
                                    dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nTransactionID"] = 0;
                                    dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nTrackTrnID"] = 0;
                                    dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nPatientID"] = 0;
                                    dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nProviderID"] = ProviderID;
                                    dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nCollectionAgencycontactID"] = nAssociateCollectionAgencyContactID;

                                    if (dtReserveforDOS != DateTime.MinValue)
                                    {
                                        dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["dtReserveForDOS"] = dtReserveforDOS;
                                    }
                                    else
                                    {
                                        dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["dtReserveForDOS"] = DBNull.Value;
                                    }

                                    dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].AcceptChanges();

                                    #region "General Note"
                                 //   int rowNum = 0;
                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Add();
                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = 0;
                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = _nCreditID;
                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                                    if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                    {
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["ID2"].ToString());
                                    }

                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = 0;
                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = 0;
                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "";
                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = Convert.ToString(oList[1]).Trim();

                                    if (oList[3] != null && oList[3].ToString().Trim() != "")
                                    {
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = Convert.ToBoolean(oList[3]);
                                    }
                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = Convert.ToDecimal(oList[0]);
                                    if (mskCloseDate.MaskCompleted == true)
                                    {
                                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);

                                    }
                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = NoteTypeV2.Payment_PatientReserved.GetHashCode();
                                    if (oList[2] != null && oList[2].ToString().Trim() != "")
                                    {
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = Convert.ToInt32(oList[2]);
                                    }

                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = _ClinicID;

                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = 0;
                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = BillingNoteTypeV2.None.GetHashCode();
                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = _UserId;

                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DBNull.Value;
                                    if (mskCloseDate.MaskCompleted == true)
                                    {
                                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                    }
                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = 0;
                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = 0;
                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = "";
                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                                    dsInsurancePayment_TVP.Tables["EOBNotes"].AcceptChanges();
                                    #endregion

                                    if ((EOBPaymentSubType)Convert.ToInt32(oList[2]) == EOBPaymentSubType.Advance)
                                    {
                                        #region "CPT Note"

                                        if (oList[4] != null && oList[4].ToString().TrimEnd() != "")
                                        {

                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Add();
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = _nCreditID;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                                            if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                            {
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID2"].ToString());
                                            }
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "CPT";
                                            if (oList[4] != null && oList[4].ToString().TrimEnd() != "")
                                            {
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = Convert.ToString(oList[4]).Replace('^', '~');
                                            }
                                            if (oList[3] != null && oList[3].ToString().Trim() != "")
                                            {
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = Convert.ToBoolean(oList[3]);
                                            }
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = Convert.ToDecimal(oList[0]);
                                            if (mskCloseDate.MaskCompleted == true)
                                            {
                                                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);

                                            }
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = NoteTypeV2.Payment_PatientReserved.GetHashCode();
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = NoteSubTypeV2.Advance.GetHashCode();
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = _ClinicID;

                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = false;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = BillingNoteTypeV2.CPT.GetHashCode();
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = _UserId;

                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DBNull.Value;
                                            if (mskCloseDate.MaskCompleted == true)
                                            {
                                                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                            }
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = "";
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].AcceptChanges();
                                        }
                                        #endregion

                                        #region "ICD9 Note"

                                        if (oList[5] != null && oList[5].Trim() != "")
                                        {

                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Add();
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = _nCreditID;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                                            if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                            {
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID2"].ToString());
                                            }
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "ICD9";
                                            if (oList[5] != null && oList[5].Trim() != "")
                                            { dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = Convert.ToString(oList[5]).Replace('^', '~'); }

                                            if (oList[3] != null && oList[3].ToString().Trim() != "")
                                            {
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = Convert.ToBoolean(oList[3]);
                                            }
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = Convert.ToDecimal(oList[0]);

                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = NoteTypeV2.Payment_PatientReserved.GetHashCode();

                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = NoteSubTypeV2.Advance.GetHashCode();
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = _ClinicID;

                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = false;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = BillingNoteTypeV2.ICD9.GetHashCode();
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = _UserId;

                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DateTime.Now;
                                            if (mskCloseDate.MaskCompleted == true)
                                            {
                                                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                            }
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = "";
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DateTime.Now;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].AcceptChanges();
                                        }
                                        #endregion
                                    }

                                }
                            }
                        }
                    }
                    //Patient Payment Reserve End
                    if (dsInsurancePayment_TVP.Tables["Reserves"] != null && Convert.ToInt64(dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count) > 0)
                    {
                        _dtUniqueReserveIDs = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count);
                        for (int i = 0; i <= dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1; i++)
                        {
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[i]["nReserveID"] = Convert.ToInt64(_dtUniqueReserveIDs.Rows[i]["ID"].ToString());
                        }

                        for (int i = 0; i <= dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1; i++)
                        {
                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[i]["nEOBPaymentDetailID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Reserves"].Rows[i]["nReserveID"].ToString());
                        }

                        DataView dv = dsInsurancePayment_TVP.Tables["EOBNotes"].DefaultView;
                        DataTable dt = dv.ToTable(true, "nEOBPaymentID");
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            Int64 resID = Convert.ToInt64(_dtUniqueReserveIDs.Rows[i]["ID"].ToString());
                            for (int iVar = 0; iVar <= dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1; iVar++)
                            {
                                if (dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[iVar]["nEOBPaymentID"].ToString().Trim() == dt.Rows[i]["nEOBPaymentID"].ToString().Trim())
                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[iVar]["nEOBPaymentDetailID"] = resID;
                            }
                        }
                    }
                    //SLR: Free previous memory of ogloBilling and then assingm
                    if (ogloBilling!=null)
                    {
                        ogloBilling.Dispose();
                        ogloBilling = null;
                    }
                    ogloBilling = new gloBilling.gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
                    ogloBilling.SaveUserWiseCloseDay(mskCloseDate.Text.Trim(), CloseDayType.Payment, _ClinicID);
                    gloAccountsV2.gloPatientPaymentV2 objclsAccPayment = new gloAccountsV2.gloPatientPaymentV2();

                    _EOBPaymentID = objclsAccPayment.SavePatientPayment(dsInsurancePayment_TVP);
                    _retPayId = _EOBPaymentID;

                    #region "Account Follow Up"

                    if (_retPayId > 0)
                    {
                        CL_FollowUpCode.SetAutoAccountFollowUp(oPatientControl.PAccountID, oPatientControl.PatientID, Convert.ToDateTime(mskCloseDate.Text));
                        if (gloAccountsV2.gloBillingCommonV2.IsIntuitBillPayTaskOfSamePatient(IBPTaskID, oPatientControl.PatientID))
                        {
                            if (IsIntuitBillPay)
                            {
                                gloAccountsV2.gloBillingCommonV2.CompleteIntuitBillPayTask(IBPTaskID);
                                IsTaskCompleted = true;
                            }
                        }
                        IsIntuitBillPay = false;
                    }

                    #endregion "Account Follow Up"

                    //********
                    // Remove $0.00 bug from insurance claim followup when patient did full patient payment for claim
                    // Here both the array lists (_Arr_ClaimFollowupRemove_TransactionMSTID & _Arr_ClaimFollowupRemove_TrackTransactionID)
                    // contains same no. of items in it.So take any one of the array list for following function.
                    for (int i = 0; i < _Arr_ClaimFollowupRemove_TransactionMSTID.Count; i++)
                    {
                        CL_FollowUpCode.ClearZeroBalanceClaimfromInsuranceFollowup(Convert.ToInt64(_Arr_ClaimFollowupRemove_TrackTransactionID[i]), Convert.ToInt64(_Arr_ClaimFollowupRemove_TransactionMSTID[i]));
                    }
                    //********

                    _EOBPaymentID = 0;
                    _IsAdjustmentMode = false;
                    btnReserveRemaining.Tag = null;

                    #region "Desing Grids"
                    _IsFormLoading = true;
                    rbPayType_Payment_CheckedChanged(null, null);
                    _IsFormLoading = false;
                    #endregion
                    //SLR: Free obClsaccpayment
                    if (objclsAccPayment != null)
                    {
                        objclsAccPayment.Dispose();
                    }
                }

                //GLO2011-0013738 : Patient Payment screen does not prompt to pick a tray
                // whenver payment tray load it will show last payment tray selected which is used for transaction.
                if (_retPayId > 0)
                {
                    //gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
                    ogloBilling.SaveUserWiseCloseDay(mskCloseDate.Text.Trim(), CloseDayType.Payment, gloGlobal.gloPMGlobal.ClinicID);
                    gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);

                    oSettings.AddSetting("PAYMENT_LASTCLOSEDATE", Convert.ToDateTime(mskCloseDate.Text).ToString("MM/dd/yyyy"), gloGlobal.gloPMGlobal.ClinicID, gloGlobal.gloPMGlobal.UserID, gloSettings.SettingFlag.User);
                    oSettings.AddSetting("PAYMENT_LASTCLOSETRAYID", _CloseDayTrayID.ToString(), gloGlobal.gloPMGlobal.ClinicID, gloGlobal.gloPMGlobal.UserID, gloSettings.SettingFlag.User);
                    oSettings.Dispose();
                }
                //end
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _useReserves = false;
                if (dsInsurancePayment_TVP != null)
                { dsInsurancePayment_TVP.Clear(); }
                if (ogloBilling != null)
                { ogloBilling.Dispose(); }
            }
            return _retPayId;
        }
        private Int64 SavePatientCorrectionPayment()
        {
            Int64 _retPayId = 0;
            Int32 row_num = 0;
       //     int rowDebitIndex = 0;
            DataTable _dtUniqueIDs = null;//SLR: new is not needed
            DataTable _dtUniqueCreditID = null;//SLR: new is not needed
            DataTable _dtUniqueReserveIDs = null;//SLR: new is not needed
            Int64 _nEOBID = 0;
            Int64 _nCreditID = 0;
            c1SinglePayment.FinishEditing();
            gloBilling.gloBilling ogloBilling = null;
            gloGeneralItem.gloItems oCrItems = new gloGeneralItem.gloItems();
            try
            {
                Int64 _CloseDayTrayID = 0;
                string _CloseDayTrayName = "";

                if ((txtCheckAmount.Text.Trim().Length > 0 && Convert.ToDecimal(txtCheckAmount.Text) > 0) || _IsAdjustmentMode == true || _IsPaymentCorrectionMode == true)
                {
                    if (oPatientControl.PatientID == 0)
                    {
                        //To Save the Reserves against the  Owner of the Acc When All Acc Pat is Selected 
                        DataTable dtPatients = null;
                        dtPatients = PatientStripControl.GetAccountPatients(oPatientControl.PAccountID);
                        if (dtPatients != null && dtPatients.Rows.Count > 0)
                        {
                            _PatientID = dtPatients.Rows[0]["nPatientID"] == DBNull.Value ? 0 : Convert.ToInt64(dtPatients.Rows[0]["nPatientID"]);
                        }
                    }

                    #region "Payment Tray"
                    _CloseDayTrayID = Convert.ToInt64(lblPaymentTray.Tag.ToString().Trim());

                    _CloseDayTrayName = lblPaymentTray.Text;
                    #endregion

                    #region " Master Data "

                    _dtUniqueCreditID = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);
                    if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                    {
                        _nCreditID = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID"].ToString());
                        _nEOBID = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                    }

                    SetCreditsDetails(dsInsurancePayment_TVP, _nCreditID);

                    #region "Payment Master Note"
                    //Notes if any to main payment to all claim OR main payment to reserve account
                    if (txtPayMstNotes.Text.Trim().Length > 0)
                    {
                        #region "General Note"
                      //  int rowNum = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Add();
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = _nCreditID;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                        if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                        {
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["ID2"].ToString());
                        }

                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "";
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = txtPayMstNotes.Text.Trim();
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = chkPayMstIncludeNotes.Checked;
                        if (txtCheckAmount.Text.Trim() != "")
                        {
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = Convert.ToDecimal(txtCheckAmount.Text.Trim());
                        }
                        if (mskCloseDate.MaskCompleted == true)
                        {
                            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);

                        }
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = NoteTypeV2.Payment_Patient.GetHashCode();
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = NoteSubTypeV2.MasterPayment.GetHashCode();
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = _ClinicID;

                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = BillingNoteTypeV2.None.GetHashCode();
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = _UserId;

                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DBNull.Value;
                        if (mskCloseDate.MaskCompleted == true)
                        {
                            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                        }
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = "";
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].AcceptChanges();
                        #endregion
                    }
                    #endregion

                    #endregion

                    #region "Negative Amount Entries - Fetch from database and set to object"

                    if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 0)
                    {
                        for (int nCrIndex = 1; nCrIndex <= c1SinglePayment.Rows.Count - 1; nCrIndex++)
                        {
                            _nTransactionPatientID = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_PATIENTID));
                            if (c1SinglePayment.GetData(nCrIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(nCrIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                            {
                                if (c1SinglePayment.GetData(nCrIndex, COL_CUR_PAYMENT) != null && c1SinglePayment.GetData(nCrIndex, COL_CUR_PAYMENT).ToString().Trim() != "")
                                {
                                    if (Convert.ToDecimal(c1SinglePayment.GetData(nCrIndex, COL_CUR_PAYMENT)) < 0)
                                    {
                                        decimal _crPayAmt = Convert.ToDecimal(c1SinglePayment.GetData(nCrIndex, COL_CUR_PAYMENT)) - (Convert.ToDecimal(c1SinglePayment.GetData(nCrIndex, COL_CUR_PAYMENT)) * 2);
                                       // int _crResPayMode = 0;

                                        Int64 _crBillTrnId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_ID));
                                        Int64 _crBillTrnDtlId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_DETAILID));
                                        Int64 _crBillTrnDtlLineId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_LINENO));
                                        Int64 _crPatientId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_PATIENTID));
                                        string _crCptCode = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_CODE)).Trim();
                                        Int64 _crPAccountID = this.nPAccountID;

                                        gloDatabaseLayer.DBLayer _nCrDBLayer = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                                        gloDatabaseLayer.DBParameters _nCrDBParameters = new gloDatabaseLayer.DBParameters();
                                        DataTable _nCrDataTable = null;
                                        decimal _checkSumCorrection = 0;

                                        _nCrDBParameters.Add("@CorrectionAmount", _crPayAmt, ParameterDirection.Input, SqlDbType.Decimal);// numeric(18,2),
                                        _nCrDBParameters.Add("@nPatientID", _crPatientId, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0),
                                        _nCrDBParameters.Add("@nBillingTransactionID", _crBillTrnId, ParameterDirection.Input, SqlDbType.BigInt);//   numeric(18,0),
                                        _nCrDBParameters.Add("@nBillingTransactionDetailID", _crBillTrnDtlId, ParameterDirection.Input, SqlDbType.BigInt);//  numeric(18,0)
                                        _nCrDBLayer.Connect(false);
                                        _nCrDBLayer.Retrive("BL_SELECT_EOBCorrectionAmountList_V2", _nCrDBParameters, out _nCrDataTable);
                                        _nCrDBLayer.Disconnect();
                                        _nCrDBLayer.Dispose();

                                        if (_nCrDataTable != null && _nCrDataTable.Rows.Count > 0)
                                        {
                                            _checkSumCorrection = 0;

                                            for (int nCrDTIndex = 0; nCrDTIndex <= _nCrDataTable.Rows.Count - 1; nCrDTIndex++)
                                            {
                                                //decimal _fillPayAmt = 0;
                                                decimal _fillAdjAmt = 0;
                                                _checkSumCorrection += Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]);

                                                //Store Cr Amt list for apply to debit lines
                                                gloGeneralItem.gloItem ogloItem = new gloGeneralItem.gloItem(Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]), Convert.ToString(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]));
                                                oCrItems.Add(ogloItem);
                                                ogloItem.Dispose();

                                                dsInsurancePayment_TVP.Tables["Debits"].Rows.Add();

                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCredit_RefID"] = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]);
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                                                if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                {
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBDetailID"] = 0;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = 0;
                                                }
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientInsuranceID"] = 0;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nContactID"] = 0;                                                
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nInsCompanyID"] = 0;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientID"] = _nTransactionPatientID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nGuarantorID"] = this.nGuarantorID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPAccountID"] = this.nPAccountID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nAccountPatientID"] = this.nAccountPatientID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_ID));
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_DETAILID));
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_TRN_ID));
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_TRN_DTL_ID));
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.PatientCorrection.GetHashCode();
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sEntryDesc"] = "PatientCorrection";
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_CODE));
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTDesc"] = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_DESCRIPTON));
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dPaymentAmount"] = Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]) - (Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]) * 2);

                                                if (c1SinglePayment.GetData(nCrIndex, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                { _fillAdjAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CUR_ADJ_AMOUNT))); }
                                                if (nCrDTIndex == 0)
                                                {
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dOtherAdjustmentAmount"] = _fillAdjAmt;
                                                }

                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWriteoffAmount"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWithholdAmount"] = DBNull.Value;

                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentTrayID"] = _CloseDayTrayID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentTrayDesc"] = _CloseDayTrayName;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nUserID"] = _UserId;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sUserName"] = _UserName;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nVoidType"] = 0;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidDateTime"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidCloseDate"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidDateTime"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsERAPost"] = false;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nClinicID"] = _ClinicID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCreatedDateTime"] = DateTime.Now;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtModifiedDateTime"] = DateTime.Now;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sSiteID"] = DBNull.Value;
                                                if (IsPaymentMarkforcollectionAgency)
                                                {
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCollectionAgencyContactID"] = CollectionAgencyContactID; //gloPatientPaymentV2.getCollectionAgencyContactID(Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_TRN_ID)), Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_TRN_DTL_ID))); ;
                                                }
                                                else
                                                {
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCollectionAgencyContactID"] = 0;
                                                }
                                            }
                                            //..Code added to check the correction list sum amount is equal to the actual correction amount sent
                                            //..If not equal abort save else continue.
                                            if (_crPayAmt != _checkSumCorrection)
                                            {
                                                string _message = "Invalid Patient Payment correction list retrival for PatientID : " + _crPatientId + ", Amount : " + _crPayAmt + ",BillingTrnID : " + _crBillTrnId + ",BillingTrnDtlID : " + _crBillTrnDtlId + " ";
                                                MessageBox.Show("ERROR : Invalid correction amount.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Payment, gloAuditTrail.ActivityType.Add, _message, _crPatientId, 0, 0, gloAuditTrail.ActivityOutCome.Failure);
                                                return 0;
                                            }

                                        }
                                        if (_nCrDataTable != null)
                                        {
                                            _nCrDataTable.Dispose();
                                            _nCrDataTable = null;
                                        }
                                       // _nCrDBLayer.Dispose();
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    //Correction Patient Payment Entry start
                    if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 0)
                    {
                        for (int rowIndex = 1; rowIndex <= c1SinglePayment.Rows.Count - 1; rowIndex++)
                        {
                            if (c1SinglePayment.GetData(rowIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(rowIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.Claim)
                            {
                                int _claimStartIndex = Convert.ToInt32(c1SinglePayment.GetData(rowIndex, COL_CELLRANGE_R1)) + 1;
                                int _claimEndIndex = Convert.ToInt32(c1SinglePayment.GetData(rowIndex, COL_CELLRANGE_R2));

                                #region  "..Need to check if payment or adjustment is made against any of the claim line then skip claim "

                                bool _hasDataToSave = false;
                                for (int index = _claimStartIndex; index <= _claimEndIndex; index++)
                                {
                                    if (
                                        (c1SinglePayment.GetData(index, COL_CUR_PAYMENT) != null
                                        && Convert.ToString(c1SinglePayment.GetData(index, COL_CUR_PAYMENT)).Trim() != ""
                                        && Convert.ToDecimal(c1SinglePayment.GetData(index, COL_CUR_PAYMENT)) > 0) ||
                                        (c1SinglePayment.GetData(index, COL_CUR_ADJ_AMOUNT) != null
                                        && Convert.ToString(c1SinglePayment.GetData(index, COL_CUR_ADJ_AMOUNT)).Trim() != ""
                                        && Convert.ToDecimal(c1SinglePayment.GetData(index, COL_CUR_ADJ_AMOUNT)) != 0)
                                       )
                                    {
                                        _hasDataToSave = true;
                                        break;
                                    }

                                }

                                if (_hasDataToSave == false) { continue; }

                                #endregion

                                for (int i = _claimStartIndex; i <= _claimEndIndex; i++)
                                {
                                    _nTransactionPatientID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_PATIENTID));
                                    if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                                    {
                                        if (
                                        (c1SinglePayment.GetData(i, COL_CUR_PAYMENT) != null
                                        && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)).Trim() != "")
                                        || (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null
                                        && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                       )
                                        {
                                            #region "EOB Service Lines"

                                            decimal _fillPayAmt = 0; decimal _fillAdjAmt = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows.Add();
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                                            if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                            {
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBDetailID"] = 0;
                                            }

                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPatientID"] = _nTransactionPatientID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPatientInsuranceID"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nContactID"] = 0;                                        
                                            
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nInsCompanyID"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.PatientCorrection.GetHashCode();
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sEntryDesc"] = "PatientCorrection";
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));

                                            if (mskCloseDate.MaskCompleted == true)
                                            {
                                                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                                            }

                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPaymentTrayID"] = _CloseDayTrayID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sPaymentTrayDesc"] = _CloseDayTrayName;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nVoidType"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPAccountID"] = this.nPAccountID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nGuarantorID"] = this.nGuarantorID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nAccountPatientID"] = this.nAccountPatientID;
                                            if (c1SinglePayment.GetData(i, COL_TOTALCHARGE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_TOTALCHARGE)).Trim() != "")
                                            {
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dTotalChargeAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_TOTALCHARGE));
                                            }
                                            if (c1SinglePayment.GetData(i, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)).Trim() != "")
                                            {
                                                _fillPayAmt = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_PAYMENT));
                                            }
                                            if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                            {
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dWriteoffAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT));
                                            }

                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dPaymentAmount"] = _fillPayAmt;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEXTID"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCreditIDEXT"] = _nCreditID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBIDEXT"] = _nEOBID;
                                            if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                            {
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBDetailIDEXT"] = 0;
                                            }
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sNextActionEXT"] = "";
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sNextPartyEXT"] = "";
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nNextPartyIDEXT"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtCreatedDateTimeEXT"] = DateTime.Now;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtModifiedDateTimeEXT"] = DateTime.Now;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtPaymentVoidDateTimeEXT"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nUserIDEXT"] = _UserId;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sUserNameEXT"] = _UserName;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["bIsERAPostEXT"] = false;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nSVCIDEXT"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sMachineNameEXT"] = Environment.MachineName;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sSiteIDEXT"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sVersionEXT"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nClinicIDEXT"] = _ClinicID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCLPIdEXT"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionEXT"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionDetailEXT"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                            if (IsPaymentMarkforcollectionAgency)
                                            {
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCollectionAgencyContactID"] = CollectionAgencyContactID; //gloPatientPaymentV2.getCollectionAgencyContactID(Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID)), Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID))); ;
                                            }
                                            else
                                            {
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCollectionAgencyContactID"] = 0;
                                            }

                                            #region " Set Line Reason Codes for Adjustment "

                                            if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                            {
                                                if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))) != 0)
                                                {
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Add();
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nID"] = 0;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nClaimNo"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                                                    if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                    {
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID2"].ToString());
                                                    }

                                                    if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                                                    {
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                                                    }
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));


                                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE)).Trim() != "")
                                                    { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sReasonCode"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE)).Trim(); }
                                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION)).Trim() != "")
                                                    { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sReasonDescription"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION)).Trim(); }

                                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                    { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["dReasonAmount"] = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); }

                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nClinicID"] = _ClinicID;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nType"] = EOBCommentTypeV2.SystemReasonCode.GetHashCode();
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["bIsVoid"] = 0;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnDtlID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnLineNo"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sSubClaimNo"] = Convert.ToString(c1SinglePayment.GetData(i, COL_SUB_CLAIM_NO));
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nVoidType"] = 0;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["bIsPaymentVoid"] = 0;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nPaymentVoidCloseDate"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nSubType"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].AcceptChanges();
                                                }
                                            }

                                            #endregion " Set Line Reason Codes "

                                            #region "Debit Service Line - Patient"
                                            if (c1SinglePayment.GetData(i, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)).Trim() != ""
                                                    && (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)) >= 0))
                                            {
                                                #region "Debit Service Line - patient "

                                                decimal _fillResAmt = 0;
                                               // Int64 _fillResPayID = 0; Int64 _fillResPayDtlID = 0;
                                                Int64 _fillRefPayID = 0; 
                                                //Int64 _fillRefPayDtlID = 0;
                                                int _fillrPayIndx = -1;
                                                //int _fillRefFinanceLieNo = 0;
                                                //bool _fillUseRefFinanceLieNo = false;
                                                //bool _isNullfillPayAmt = false;
                                                //bool _isNullfillAdjAmt = false;

                                                if (c1SinglePayment.GetData(i, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)).Trim() != "")
                                                { _fillPayAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)));
                                                //    _isNullfillPayAmt = false;
                                                }

                                                if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != ""
                                                     && (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)) > 0
                                                     || Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)) < 0)
                                                   )
                                                {
                                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                    { _fillAdjAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); 
                                                        //_isNullfillAdjAmt = false;
                                                    }
                                                }

                                                _fillAdjAmt = _fillAdjAmt - (_fillAdjAmt * 2);

                                                for (int rPay = 0; rPay <= oCrItems.Count - 1; rPay++)
                                                {
                                                    if (Convert.ToDecimal(oCrItems[rPay].Description) > 0)
                                                    {
                                                        _fillResAmt = Convert.ToDecimal(oCrItems[rPay].Description);
                                                        _fillRefPayID = Convert.ToInt64(oCrItems[rPay].ID);
                                                        _fillrPayIndx = rPay;
                                                        break;
                                                    }
                                                }

                                                if (_fillPayAmt <= _fillResAmt)
                                                {
                                                    #region "Set Less Amount Single object"
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows.Add();

                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCredit_RefID"] = _fillRefPayID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                                                    if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                    {
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBDetailID"] = 0;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = 0;
                                                    }
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientInsuranceID"] = 0;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nContactID"] = 0;                                                   
                                                    
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nInsCompanyID"] = 0;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientID"] = _nTransactionPatientID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nGuarantorID"] = this.nGuarantorID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPAccountID"] = this.nPAccountID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nAccountPatientID"] = this.nAccountPatientID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.PatientPayment.GetHashCode();
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sEntryDesc"] = "PatientPayment";
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTDesc"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dPaymentAmount"] = _fillPayAmt;

                                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                    { _fillAdjAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); }

                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dOtherAdjustmentAmount"] = _fillAdjAmt;

                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWriteoffAmount"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWithholdAmount"] = DBNull.Value;

                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentTrayID"] = _CloseDayTrayID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentTrayDesc"] = _CloseDayTrayName;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nUserID"] = _UserId;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sUserName"] = _UserName;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nVoidType"] = 0;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidDateTime"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidCloseDate"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidDateTime"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsERAPost"] = false;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nClinicID"] = _ClinicID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCreatedDateTime"] = DateTime.Now;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtModifiedDateTime"] = DateTime.Now;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sSiteID"] = DBNull.Value;
                                                    if (IsPaymentMarkforcollectionAgency)
                                                    {
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCollectionAgencyContactID"] = CollectionAgencyContactID ;//gloPatientPaymentV2.getCollectionAgencyContactID(Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID)), Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID))); ;
                                                    }
                                                    else
                                                    {
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCollectionAgencyContactID"] = 0;
                                                    }
                                                    

                                                    if (_fillrPayIndx != -1)
                                                    {

                                                        oCrItems[_fillrPayIndx].Description = Convert.ToString(_fillResAmt - _fillPayAmt);
                                                    }
                                                    #endregion
                                                }
                                                else
                                                {
                                                    #region "Set More Amount Multiple object"

                                                    decimal _fillPayMulAmt = _fillPayAmt;

                                                    do
                                                    {
                                                        if (Convert.ToDecimal(oCrItems[_fillrPayIndx].Description) > 0)
                                                        {
                                                            _fillResAmt = Convert.ToDecimal(oCrItems[_fillrPayIndx].Description);
                                                            _fillRefPayID = Convert.ToInt64(oCrItems[_fillrPayIndx].ID);

                                                            //_fillRefFinanceLieNo = 0;
                                                            //_fillUseRefFinanceLieNo = false;
                                                            //_isNullfillPayAmt = false;


                                                            if (_fillPayMulAmt >= _fillResAmt)
                                                            {
                                                                _fillPayAmt = _fillResAmt;
                                                                _fillPayMulAmt = _fillPayMulAmt - _fillPayAmt;
                                                            }
                                                            else
                                                            {
                                                                _fillPayAmt = _fillPayMulAmt;
                                                                _fillPayMulAmt = 0;
                                                            }
                                                        }

                                                        #region "Set object"

                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows.Add();

                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCredit_RefID"] = _fillRefPayID;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                                                        if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                        {
                                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBDetailID"] = 0;
                                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = 0;
                                                        }
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientInsuranceID"] = 0;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nContactID"] = 0;
                                                        
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nInsCompanyID"] = 0;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientID"] = _nTransactionPatientID;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nGuarantorID"] = this.nGuarantorID;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPAccountID"] = this.nPAccountID;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nAccountPatientID"] = this.nAccountPatientID;
                                                        //nPAccountID,nGuarantorID,nAccountPatientID
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.PatientPayment.GetHashCode();
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sEntryDesc"] = "PatientPayment";
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTDesc"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dPaymentAmount"] = _fillPayAmt;

                                                        if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                        { _fillAdjAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); }
                                                        if (_fillrPayIndx == 0)
                                                        {
                                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dOtherAdjustmentAmount"] = _fillAdjAmt;
                                                        }

                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWriteoffAmount"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWithholdAmount"] = DBNull.Value;

                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentTrayID"] = _CloseDayTrayID;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentTrayDesc"] = _CloseDayTrayName;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nUserID"] = _UserId;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sUserName"] = _UserName;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nVoidType"] = 0;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidDateTime"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidCloseDate"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidDateTime"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsERAPost"] = false;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nClinicID"] = _ClinicID;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCreatedDateTime"] = DateTime.Now;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtModifiedDateTime"] = DateTime.Now;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sSiteID"] = DBNull.Value;
                                                        if (IsPaymentMarkforcollectionAgency)
                                                        {
                                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCollectionAgencyContactID"] = CollectionAgencyContactID; //gloPatientPaymentV2.getCollectionAgencyContactID(Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID)), Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID)));
                                                        }
                                                        else
                                                        {
                                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCollectionAgencyContactID"] = 0;
                                                        }
                                                        #endregion

                                                        if (_fillrPayIndx != -1)
                                                        {
                                                            oCrItems[_fillrPayIndx].Description = Convert.ToString(_fillResAmt - _fillPayAmt);
                                                            _fillrPayIndx = _fillrPayIndx + 1;
                                                            if (_fillrPayIndx >= oCrItems.Count) { break; }
                                                        }

                                                    }
                                                    while (_fillPayMulAmt > 0);

                                                    #endregion
                                                }

                                                #endregion
                                            }
                                            else if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != ""
                                                     && (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)) > 0
                                                     || Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)) < 0)
                                                   && _IsAdjustmentMode == true && c1SinglePayment.GetData(i, COL_CUR_PAYMENT) == null)
                                            {
                                                _fillAdjAmt = 0;
                                                #region "Debit Service Line - patient adjustment if any"

                                                if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                {
                                                    if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))) != 0 )
                                                    {
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows.Add();

                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCredit_RefID"] = _nCreditID;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                                                        if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                        {
                                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBDetailID"] = 0;
                                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = 0;
                                                        }
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientInsuranceID"] = 0;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nContactID"] = 0;
                                                        
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nInsCompanyID"] = 0;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientID"] = _nTransactionPatientID;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nGuarantorID"] = this.nGuarantorID;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPAccountID"] = this.nPAccountID;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nAccountPatientID"] = this.nAccountPatientID;

                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.PatientCorrection.GetHashCode();
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sEntryDesc"] = "PatientCorrection";
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTDesc"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dPaymentAmount"] = 0;

                                                        if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                        { _fillAdjAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); }

                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dOtherAdjustmentAmount"] = _fillAdjAmt;

                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWriteoffAmount"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWithholdAmount"] = DBNull.Value;

                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentTrayID"] = _CloseDayTrayID;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentTrayDesc"] = _CloseDayTrayName;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nUserID"] = _UserId;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sUserName"] = _UserName;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nVoidType"] = 0;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidDateTime"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidCloseDate"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidDateTime"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsERAPost"] = false;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nClinicID"] = _ClinicID;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCreatedDateTime"] = DateTime.Now;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtModifiedDateTime"] = DateTime.Now;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sSiteID"] = DBNull.Value;
                                                        if (IsPaymentMarkforcollectionAgency)
                                                        {
                                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCollectionAgencyContactID"] = CollectionAgencyContactID;//gloPatientPaymentV2.getCollectionAgencyContactID(Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID)), Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID)));
                                                        }
                                                        else
                                                        {
                                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCollectionAgencyContactID"] = 0;
                                                        }
                                                    }
                                                }
                                                #endregion
                                            }
                                            #endregion
                                            #endregion
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #region "Reserve Debit Entry if any and it will goes directly to payment object with credit line"
                    if (btnReserveRemaining.Tag != null && btnReserveRemaining.Tag.ToString().Trim().Length > 0)
                    {

                        //0 ReserveAmount 
                        //1 ReserveNote 
                        //2 ReserveSubType 
                        //3 ReserveNoteOnPrint 
                        string[] oList = null;
                        if (btnReserveRemaining.Tag != null)
                        {
                            oList = btnReserveRemaining.Tag.ToString().Split('~');
                        }
                        if (oList != null && oList.Length == 8)
                        {
                            if (oList[0] != null && Convert.ToString(oList[0]).Trim() != "")
                            {
                                if (Convert.ToDecimal(Convert.ToString(oList[0]).Trim()) > 0)
                                {
                                    #region "Put Amount into reserve, but according to remaining amount which is collected from negative payment"

                                    //New Collection
                                    gloGeneralItem.gloItems oReserveItems = new gloGeneralItem.gloItems();
                                    Int64 _TempCrID = 0;
                                    int _ResItemIndex = 0;
                                    for (int rPay = 0; rPay <= oCrItems.Count - 1; rPay++)
                                    {
                                        if (_TempCrID != Convert.ToInt64(oCrItems[rPay].ID))
                                        {
                                            _TempCrID = Convert.ToInt64(oCrItems[rPay].ID);
                                            oReserveItems.Add(_TempCrID, oCrItems[rPay].Description);
                                            _ResItemIndex = _ResItemIndex + 1;
                                        }
                                        else
                                        {
                                            oReserveItems[oReserveItems.Count - 1].Description = Convert.ToString(Convert.ToDecimal(oReserveItems[oReserveItems.Count - 1].Description) + Convert.ToDecimal(oCrItems[rPay].Description));
                                            _ResItemIndex = _ResItemIndex + 1;
                                        }
                                    }

                                    for (int rPay = 0; rPay <= oReserveItems.Count - 1; rPay++)
                                    {
                                        #region "Set Amount object"

                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows.Add();
                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nReserveID"] = 0;
                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nCredits_RefID"] = Convert.ToInt64(oReserveItems[rPay].ID);
                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dReserveAmount"] = Convert.ToDecimal(oReserveItems[rPay].Description);

                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nReserveType"] = ReserveEntryTypeV2.PatientReserve.GetHashCode();
                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nInsCompanyID"] = 0;
                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nPatientID"] = _PatientID;
                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nUserID"] = _UserId;
                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["sUserName"] = _UserName;
                                        if (mskCloseDate.MaskCompleted == true)
                                        {
                                            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                                        }
                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nVoidType"] = 0;
                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nPAccountID"] = this.nPAccountID;
                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nGuarantorID"] = this.nGuarantorID;
                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nAccountPatientID"] = this.nAccountPatientID;
                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtPaymentVoidDateTime"] = DBNull.Value;
                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtCreatedDateTime"] = DateTime.Now;
                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtModifiedDateTime"] = DateTime.Now;
                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["sReserveNote"] = "Reserved";
                                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
                                        dsInsurancePayment_TVP.Tables["Reserves"].AcceptChanges();

                                        dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Add();
                                        dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nEOBPaymentID"] = _nCreditID;
                                        dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nEOBPaymentDetailID"] = 0;
                                        dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nTransactionID"] = 0;
                                        dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nTrackTrnID"] = 0;
                                        dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nPatientID"] = 0;
                                        dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nProviderID"] = ProviderID;
                                        dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nCollectionAgencycontactID"] = nAssociateCollectionAgencyContactID;
                                        if (dtReserveforDOS != DateTime.MinValue)
                                        {
                                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["dtReserveForDOS"] = dtReserveforDOS;
                                        }
                                        else
                                        {
                                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["dtReserveForDOS"] = DBNull.Value;
                                        }
                                        dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].AcceptChanges();

                                        #endregion


                                        #region "Reserve Notes"

                                        #region "General Note"
                                    //    int rowNum = 0;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Add();
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = 0;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = _nCreditID;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = 0;
                                        if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                        {
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = 0;
                                        }

                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = 0;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = 0;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "";
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = Convert.ToString(oList[1]).Trim();

                                        if (oList[3] != null && oList[3].ToString().Trim() != "")
                                        {
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = Convert.ToBoolean(oList[3]);
                                        }
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = Convert.ToDecimal(oReserveItems[rPay].Description);
                                        if (mskCloseDate.MaskCompleted == true)
                                        {
                                            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);

                                        }
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = NoteTypeV2.Payment_PatientReserved.GetHashCode();
                                        if (oList[2] != null && oList[2].ToString().Trim() != "")
                                        {
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = Convert.ToInt32(oList[2]);
                                        }

                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = _ClinicID;

                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = 0;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = BillingNoteTypeV2.None.GetHashCode();
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = _UserId;

                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DBNull.Value;
                                        if (mskCloseDate.MaskCompleted == true)
                                        {
                                            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                        }
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = 0;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = 0;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = "";
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["SequenceNo"] = dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].AcceptChanges();
                                        #endregion "General Note"

                                        if ((EOBPaymentSubType)Convert.ToInt32(oList[2]) == EOBPaymentSubType.Advance)
                                        {
                                            #region "CPT Note"

                                            if (oList[4] != null && oList[4].ToString().TrimEnd() != "")
                                            {

                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Add();
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = 0;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = _nCreditID;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = 0;
                                                if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                {
                                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = 0;
                                                }
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = 0;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = 0;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "CPT";
                                                if (oList[4] != null && oList[4].ToString().TrimEnd() != "")
                                                {
                                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = Convert.ToString(oList[4]).Replace('^', '~');
                                                }
                                                if (oList[3] != null && oList[3].ToString().Trim() != "")
                                                {
                                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = Convert.ToBoolean(oList[3]);
                                                }
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = Convert.ToDecimal(oReserveItems[rPay].Description);
                                                if (mskCloseDate.MaskCompleted == true)
                                                {
                                                    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);

                                                }
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = NoteTypeV2.Payment_PatientReserved.GetHashCode();
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = Convert.ToInt32(oList[2]);
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = _ClinicID;

                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = false;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = BillingNoteTypeV2.CPT.GetHashCode();
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = _UserId;

                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DBNull.Value;
                                                if (mskCloseDate.MaskCompleted == true)
                                                {
                                                    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                                }
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = 0;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = 0;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = "";
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["SequenceNo"] = dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].AcceptChanges();
                                            }
                                            #endregion "CPT Note"

                                            #region "ICD9 Note"

                                            if (oList[5] != null && oList[5].Trim() != "")
                                            {

                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Add();
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = 0;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = _nCreditID;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = 0;
                                                if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                {
                                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = 0;
                                                }
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = 0;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = 0;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "ICD9";
                                                if (oList[5] != null && oList[5].Trim() != "")
                                                { dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = Convert.ToString(oList[5]).Replace('^', '~'); }

                                                if (oList[3] != null && oList[3].ToString().Trim() != "")
                                                {
                                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = Convert.ToBoolean(oList[3]);
                                                }
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = Convert.ToDecimal(oReserveItems[rPay].Description);

                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = NoteTypeV2.Payment_PatientReserved.GetHashCode();

                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = Convert.ToInt32(oList[2]);
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = _ClinicID;

                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = false;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = BillingNoteTypeV2.ICD9.GetHashCode();
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = _UserId;

                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DBNull.Value;
                                                if (mskCloseDate.MaskCompleted == true)
                                                {
                                                    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                                }
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = 0;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = 0;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = "";
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["SequenceNo"] = dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1;
                                                dsInsurancePayment_TVP.Tables["EOBNotes"].AcceptChanges();
                                            }
                                            #endregion "ICD9 Note"
                                        }
                                        #endregion "Reserve Notes"

                                    }
                                    if (oReserveItems != null)
                                    {
                                        oReserveItems.Clear();
                                        oReserveItems.Dispose();
                                        oReserveItems = null;
                                    }
                                }
                            }
                        }
                                    #endregion "Put Amount into reserve, but according to remaining amount which is collected from negative payment"

                    }
                    #endregion "Reserve Debit Entry if any and it will goes directly to payment object with credit line"

                    if (dsInsurancePayment_TVP.Tables["Debits"] != null && Convert.ToInt64(dsInsurancePayment_TVP.Tables["Debits"].Rows.Count) > 0)
                    {
                        _dtUniqueIDs = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(dsInsurancePayment_TVP.Tables["Debits"].Rows.Count);

                        for (int i = 0; i <= dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1; i++)
                        {
                            dsInsurancePayment_TVP.Tables["Debits"].Rows[i]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                            dsInsurancePayment_TVP.Tables["Debits"].Rows[i]["nDebitID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID2"].ToString());
                        }

                        for (int i = 0; i <= dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1; i++)
                        {
                            dsInsurancePayment_TVP.Tables["EOB"].Rows[i]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                            dsInsurancePayment_TVP.Tables["EOB"].Rows[i]["nEOBDetailIDEXT"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                        }
                    }

                    if (dsInsurancePayment_TVP.Tables["Reserves"] != null && Convert.ToInt64(dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count) > 0)
                    {
                        _dtUniqueReserveIDs = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count);
                        for (int i = 0; i <= dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1; i++)
                        {
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[i]["nReserveID"] = Convert.ToInt64(_dtUniqueReserveIDs.Rows[i]["ID"].ToString());
                        }

                        for (int i = 0; i <= dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1; i++)
                        {
                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[i]["nEOBPaymentDetailID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Reserves"].Rows[i]["nReserveID"].ToString());
                        }

                        DataView dv = dsInsurancePayment_TVP.Tables["EOBNotes"].DefaultView;
                        dv.RowFilter = "nPaymentNoteType = 4";
                        DataTable dt = dv.ToTable(true, "SequenceNo");
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            Int64 resID = Convert.ToInt64(_dtUniqueReserveIDs.Rows[i]["ID"].ToString());
                            for (int iVar = 0; iVar <= dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1; iVar++)
                            {
                                if (dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[iVar]["SequenceNo"].ToString().Trim() == dt.Rows[i]["SequenceNo"].ToString().Trim())
                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[iVar]["nEOBPaymentDetailID"] = resID;
                            }
                        }
                    }


                    //SLR: Free previous memory of ogloBilling and then assingm
                    if (ogloBilling != null)
                    {
                        ogloBilling.Dispose();
                        ogloBilling = null;
                    }
                    ogloBilling = new gloBilling.gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
                    ogloBilling.SaveUserWiseCloseDay(mskCloseDate.Text.Trim(), CloseDayType.Payment, _ClinicID);
                    gloAccountsV2.gloPatientPaymentV2 objclsAccPayment = new gloAccountsV2.gloPatientPaymentV2();

                    _EOBPaymentID = objclsAccPayment.SavePatientPayment(dsInsurancePayment_TVP);
                    _retPayId = _EOBPaymentID;


                    if (_retPayId > 0)
                    {
                        CL_FollowUpCode.SetAutoAccountFollowUp(oPatientControl.PAccountID, oPatientControl.PatientID, Convert.ToDateTime(mskCloseDate.Text));
                        if (gloAccountsV2.gloBillingCommonV2.IsIntuitBillPayTaskOfSamePatient(IBPTaskID, oPatientControl.PatientID))
                        {
                            if (IsIntuitBillPay)
                            {
                                gloAccountsV2.gloBillingCommonV2.CompleteIntuitBillPayTask(IBPTaskID);
                            }
                        }
                        IsIntuitBillPay = false;
                    }

                    //GLO2011-0013738 : Patient Payment screen does not prompt to pick a tray
                    // whenver payment tray load it will show last payment tray selected which is used for transaction.
                    if (_retPayId > 0)

                    {
                        //gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
                        ogloBilling.SaveUserWiseCloseDay(mskCloseDate.Text.Trim(), CloseDayType.Payment, gloGlobal.gloPMGlobal.ClinicID);
                        gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);

                       // gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString );
                        oSettings.AddSetting("PAYMENT_LASTCLOSEDATE", Convert.ToDateTime(mskCloseDate.Text).ToString("MM/dd/yyyy"), gloGlobal.gloPMGlobal.ClinicID, gloGlobal.gloPMGlobal.UserID, gloSettings.SettingFlag.User);
                        if (lblPaymentTray.Tag != null || lblPaymentTray.Tag.ToString().Trim().Length > 0 || lblPaymentTray.Text.Trim() != "")
                        {
                            oSettings.AddSetting("PAYMENT_LASTCLOSETRAYID", lblPaymentTray.Tag.ToString(), gloGlobal.gloPMGlobal.ClinicID, gloGlobal.gloPMGlobal.UserID, gloSettings.SettingFlag.User);
                        }
                        oSettings.Dispose();
                    }
                    //end

                    _EOBPaymentID = 0;
                    _IsAdjustmentMode = false;
                    btnReserveRemaining.Tag = null;

                    #region "Desing Grids"
                    _IsFormLoading = true;
                    rbPayType_Payment_CheckedChanged(null, null);
                    _IsFormLoading = false;
                    #endregion
                    //SLR: Free obClsaccpayment
                    if (objclsAccPayment != null)
                    {
                        objclsAccPayment.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _useReserves = false;
                if (dsInsurancePayment_TVP != null)
                { dsInsurancePayment_TVP.Clear(); }
                if (ogloBilling != null)
                { ogloBilling.Dispose(); }
                if (oCrItems != null)
                {
                    oCrItems.Clear();
                    oCrItems.Dispose();
                    oCrItems = null;
                }
            }
            return _retPayId;
        }
        private Int64 SavePatientUseReservePayment()
        {
            Boolean _bIsUsedReserveVal = false;
            Int64 _retPayId = 0;
            int row_num = 0;
            int row_index = 0;
            DataTable _dtUniqueIDs = null;//SLR: New is not needed
            DataTable _dtUniqueCreditID = null;//SLR: New is not needed
            Int64 _nEOBID = 0;
            Int64 _nCreditID = 0;
            c1SinglePayment.FinishEditing();
            _IsUseReserveEntry = true;
            try
            {
                Int64 _CloseDayTrayID = 0;
                string _CloseDayTrayName = "";

                if ((txtCheckAmount.Text.Trim().Length > 0 && Convert.ToDecimal(txtCheckAmount.Text) > 0) || _IsAdjustmentMode == true || _IsPaymentCorrectionMode == true)
                {

                    #region "Payment Tray"
                    _CloseDayTrayID = Convert.ToInt64(lblPaymentTray.Tag.ToString().Trim());
                    _CloseDayTrayName = lblPaymentTray.Text;
                    #endregion

                    #region " Master Data "

                    _dtUniqueCreditID = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);
                    if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                    {
                        _nCreditID = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID"].ToString());
                        _nEOBID = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                    }
                    SetCreditsDetails(dsInsurancePayment_TVP, _nCreditID);

                    #region "Payment Master Note"
                    //Notes if any to main payment to all claim OR main payment to reserve account
                    if (txtPayMstNotes.Text.Trim().Length > 0)
                    {
                        #region "General Note"
                     //   int rowNum = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Add();
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = _nCreditID;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                        if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                        {
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["ID2"].ToString());
                        }

                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "";
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = txtPayMstNotes.Text.Trim();
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = chkPayMstIncludeNotes.Checked;
                        if (txtCheckAmount.Text.Trim() != "")
                        {
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = Convert.ToDecimal(txtCheckAmount.Text.Trim());
                        }
                        if (mskCloseDate.MaskCompleted == true)
                        {
                            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);

                        }
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = NoteTypeV2.Payment_Patient.GetHashCode();
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = NoteSubTypeV2.MasterPayment.GetHashCode();
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = _ClinicID;

                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = BillingNoteTypeV2.None.GetHashCode();
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = _UserId;

                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DBNull.Value;
                        if (mskCloseDate.MaskCompleted == true)
                        {
                            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                        }
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = "";
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].AcceptChanges();
                        #endregion
                    }
                    #endregion

                    #endregion

                    #region "Credit Details in case of TB/UR"
                    if (_useReserves)
                    {
                        
                        if (btnUseReserve.Tag != null)
                        {
                            gloGeneralItem.gloItems ocrItems =  (gloGeneralItem.gloItems)btnUseReserve.Tag;

                            if (ocrItems == null)
                            {
                                ocrItems = new gloGeneralItem.gloItems();
                            }
                            for (int crPay = 0; crPay <= ocrItems.Count - 1; crPay++)
                            {
                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Add();
                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nCreditsDTL_ID"] = 0;
                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nCreditsRef_ID"] = Convert.ToInt64(ocrItems[crPay].ID);
                                for (int crSub = 0; crSub <= ocrItems[crPay].SubItems.Count - 1; crSub++)
                                {
                                    dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nReserveRef_ID"] = Convert.ToInt64(ocrItems[crPay].SubItems[crSub].ID);
                                }
                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dAmount"] = Convert.ToDecimal(ocrItems[crPay].Description);
                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.UseReserve.GetHashCode();
                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["sEntryDesc"] = "UR";

                                if (mskCloseDate.MaskCompleted == true)
                                {

                                    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                    for (int crSub = 0; crSub <= ocrItems[crPay].SubItems.Count - 1; crSub++)
                                    {
                                        if (Convert.ToDateTime(mskCloseDate.Text) < Convert.ToDateTime(ocrItems[crPay].SubItems[crSub].CloseDate))
                                        {
                                            MessageBox.Show("The used reserved amount close date is in future than the current payment close date. Please select a different payment close date.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            _bIsUsedReserveVal = true;
                                            return 0;
                                        }
                                    }
                                    dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                                }
                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dtCreatedDateTime"] = DateTime.Now;
                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dtModifiedDateTime"] = DateTime.Now;
                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["bIsPaymentVoid"] = false;

                                dsInsurancePayment_TVP.Tables["CreditsDTL"].AcceptChanges();
                            }
                        }
                    }

                    #endregion "Credit Details in case of TB/UR"

                    #region "EOB and Debit Entry"
                    if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 0)
                    {
                        for (int rowIndex = 1; rowIndex <= c1SinglePayment.Rows.Count - 1; rowIndex++)
                        {
                            if (c1SinglePayment.GetData(rowIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(rowIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.Claim)
                            {
                                int _claimStartIndex = Convert.ToInt32(c1SinglePayment.GetData(rowIndex, COL_CELLRANGE_R1)) + 1;
                                int _claimEndIndex = Convert.ToInt32(c1SinglePayment.GetData(rowIndex, COL_CELLRANGE_R2));
                                _dtUniqueIDs = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(c1SinglePayment.Rows.Count - 1);
                                #region  "..Need to check if payment or adjustment is made against any of the claim line then skip claim "

                                bool _hasDataToSave = false;
                                for (int index = _claimStartIndex; index <= _claimEndIndex; index++)
                                {
                                    if (
                                        (c1SinglePayment.GetData(index, COL_CUR_PAYMENT) != null
                                        && Convert.ToString(c1SinglePayment.GetData(index, COL_CUR_PAYMENT)).Trim() != ""
                                        && Convert.ToDecimal(c1SinglePayment.GetData(index, COL_CUR_PAYMENT)) > 0) ||
                                        (c1SinglePayment.GetData(index, COL_CUR_ADJ_AMOUNT) != null
                                        && Convert.ToString(c1SinglePayment.GetData(index, COL_CUR_ADJ_AMOUNT)).Trim() != ""
                                        && Convert.ToDecimal(c1SinglePayment.GetData(index, COL_CUR_ADJ_AMOUNT)) != 0)
                                       )
                                    {
                                        _hasDataToSave = true;
                                        break;
                                    }

                                }

                                if (_hasDataToSave == false) { continue; }

                                #endregion

                                #region "Claim wise EOB and Finance Line"
                                for (int i = _claimStartIndex; i <= _claimEndIndex; i++)
                                {
                                    _nTransactionPatientID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_PATIENTID));
                                    if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                                    {
                                        if (
                                        (c1SinglePayment.GetData(i, COL_CUR_PAYMENT) != null
                                        && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)).Trim() != ""
                                        && Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)) > 0) ||
                                        (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null
                                        && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != ""
                                        && Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)) != 0)
                                       )
                                        {
                                            #region "EOB Service Lines"
                                            decimal _fillPayAmt = 0; decimal _fillAdjAmt = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows.Add();
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                                            if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                            {
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBDetailID"] = 0;
                                            }
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPatientID"] = _nTransactionPatientID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPatientInsuranceID"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nContactID"] = 0;
                                            
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nInsCompanyID"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.PatientPayment.GetHashCode();
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sEntryDesc"] = "PatientPayment";
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));

                                            if (mskCloseDate.MaskCompleted == true)
                                            {
                                                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                                            }

                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPaymentTrayID"] = _CloseDayTrayID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sPaymentTrayDesc"] = _CloseDayTrayName;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nVoidType"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPAccountID"] = this.nPAccountID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nGuarantorID"] = this.nGuarantorID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nAccountPatientID"] = this.nAccountPatientID;

                                            if (c1SinglePayment.GetData(i, COL_TOTALCHARGE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_TOTALCHARGE)).Trim() != "")
                                            {
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dTotalChargeAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_TOTALCHARGE));
                                            }

                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEXTID"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCreditIDEXT"] = _nCreditID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBIDEXT"] = _nEOBID;
                                            if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                            {
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBDetailIDEXT"] = 0;
                                            }

                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sNextActionEXT"] = "";
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sNextPartyEXT"] = "";
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nNextPartyIDEXT"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtCreatedDateTimeEXT"] = DateTime.Now;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtModifiedDateTimeEXT"] = DateTime.Now;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtPaymentVoidDateTimeEXT"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nUserIDEXT"] = _UserId;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sUserNameEXT"] = _UserName;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["bIsERAPostEXT"] = false;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nSVCIDEXT"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sMachineNameEXT"] = Environment.MachineName;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sSiteIDEXT"] = DBNull.Value;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sVersionEXT"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nClinicIDEXT"] = _ClinicID;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCLPIdEXT"] = 0;
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionEXT"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionDetailEXT"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                            if (IsPaymentMarkforcollectionAgency)
                                            {
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCollectionAgencyContactID"] = CollectionAgencyContactID; //gloPatientPaymentV2.getCollectionAgencyContactID(Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID)), Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID))); ;
                                            }
                                            else
                                            {
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCollectionAgencyContactID"] = 0;
                                            }
                                            if (c1SinglePayment.GetData(i, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)).Trim() != "")
                                            {
                                                _fillPayAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)));
                                            }
                                            if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                            {
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dWriteoffAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT));
                                            }

                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dPaymentAmount"] = _fillPayAmt;

                                            #endregion "EOB Service Lines"
                                            #region " Set Line Reason Codes for Adjustment "

                                            if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                            {
                                                if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))) != 0)
                                                {
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Add();
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nID"] = 0;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nClaimNo"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                                                    if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                    {
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID2"].ToString());
                                                    }

                                                    if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                                                    {
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                                                    }
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));


                                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE)).Trim() != "")
                                                    { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sReasonCode"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPECODE)).Trim(); }
                                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION)).Trim() != "")
                                                    { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sReasonDescription"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_TYPEDESCRIPTION)).Trim(); }

                                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                    { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["dReasonAmount"] = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); }

                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nClinicID"] = _ClinicID;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nType"] = EOBCommentTypeV2.SystemReasonCode.GetHashCode();
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["bIsVoid"] = 0;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnDtlID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnLineNo"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sSubClaimNo"] = Convert.ToString(c1SinglePayment.GetData(i, COL_SUB_CLAIM_NO));
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nVoidType"] = 0;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["bIsPaymentVoid"] = 0;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nPaymentVoidCloseDate"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nSubType"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["ReasonCode"].AcceptChanges();
                                                }
                                            }

                                            #endregion " Set Line Reason Codes "
                                            #region "Debit Service Line - patient "

                                            gloGeneralItem.gloItems oItems = null;
                                            if (btnUseReserve.Tag != null)
                                            {
                                                oItems = (gloGeneralItem.gloItems)btnUseReserve.Tag;
                                            }
                                            if( oItems == null )
                                            {
                                                oItems = new gloGeneralItem.gloItems();
                                            }

                                            decimal _fillResAmt = 0;
                                            Int64 _fillResPayID = 0; Int64 _fillResPayDtlID = 0;
                                            Int64 _fillRefPayID = 0; Int64 _fillRefPayDtlID = 0;
                                            int _fillrPayIndx = -1;
                                            int _fillRefFinanceLieNo = 0;
                                            //bool _fillUseRefFinanceLieNo = false;
                                            //bool _isNullfillPayAmt = true;
                                            //bool _isNullfillAdjAmt = true;

                                            if (c1SinglePayment.GetData(i, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT)).Trim() != "")
                                            { _fillPayAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_PAYMENT))); 
                                            //    _isNullfillPayAmt = false; 
                                            }

                                            if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                            { _fillAdjAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); 
                                               // _isNullfillAdjAmt = false;
                                            }

                                            for (int rPay = 0; rPay <= oItems.Count - 1; rPay++)
                                            {
                                                if (Convert.ToDecimal(oItems[rPay].Description) > 0)
                                                {
                                                    _fillResAmt = Convert.ToDecimal(oItems[rPay].Description);
                                                    _fillResPayID = Convert.ToInt64(oItems[rPay].ID);
                                                    _fillResPayDtlID = Convert.ToInt64(oItems[rPay].Code);

                                                    if (oItems[rPay].SubItems != null && oItems[rPay].SubItems.Count > 0)
                                                    {
                                                        _fillRefPayID = Convert.ToInt64(oItems[rPay].SubItems[0].ID);
                                                        _fillRefPayDtlID = Convert.ToInt64(oItems[rPay].SubItems[0].Description);
                                                    }

                                                    //This logic is temporary depend upon the gloItems
                                                    //when we implement partial payment it should be depend upon the "EOBInsurancePaymentMasterAllocationLines" this object
                                                    if (_fillRefPayID == 0 && _fillRefPayDtlID == 0)
                                                    {
                                                        //_fillUseRefFinanceLieNo = true;
                                                        _fillRefFinanceLieNo = rPay + 2;
                                                    }

                                                    _fillrPayIndx = rPay;
                                                    break;
                                                }
                                            }

                                            if (_fillPayAmt <= _fillResAmt)
                                            {
                                                #region "Set Less Amount Single object"
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows.Add();
                                                //if (row_index < 0)
                                                //    row_index = row_index + 1;
                                                //Bug : 00000840: Patient Accounts. Patient Used reserve and made only adjustment for some lines then CreditRefId is saving zero.
                                                if (_fillResPayID==0)
                                                {
                                                    _fillResPayID = _nCreditID;
                                                }
                                                if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                {
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBDetailID"] = 0;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = 0;
                                                }
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCredit_RefID"] = _fillResPayID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBID"] = _nEOBID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientInsuranceID"] = 0;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nContactID"] = 0;
                                                
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nInsCompanyID"] = 0;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientID"] = _nTransactionPatientID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nGuarantorID"] = this.nGuarantorID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPAccountID"] = this.nPAccountID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nAccountPatientID"] = this.nAccountPatientID;
                                                //nPAccountID,nGuarantorID,nAccountPatientID
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.PatientPayment.GetHashCode();
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sEntryDesc"] = "PatientPayment";
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTDesc"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dPaymentAmount"] = _fillPayAmt;

                                                if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                { _fillAdjAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); }

                                                if (_fillAdjAmt > 0)
                                                {
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dOtherAdjustmentAmount"] = _fillAdjAmt;
                                                }
                                                else
                                                {
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dOtherAdjustmentAmount"] = 0;
                                                }


                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWriteoffAmount"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWithholdAmount"] = DBNull.Value;

                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentTrayID"] = _CloseDayTrayID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentTrayDesc"] = _CloseDayTrayName;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nUserID"] = _UserId;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sUserName"] = _UserName;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nVoidType"] = 0;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidDateTime"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidCloseDate"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidDateTime"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsERAPost"] = false;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nClinicID"] = _ClinicID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCreatedDateTime"] = DateTime.Now;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtModifiedDateTime"] = DateTime.Now;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sSiteID"] = DBNull.Value;
                                                if (IsPaymentMarkforcollectionAgency)
                                                {
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCollectionAgencyContactID"] = CollectionAgencyContactID; //gloPatientPaymentV2.getCollectionAgencyContactID(Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID)), Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID)));
                                                }
                                                else
                                                {
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCollectionAgencyContactID"] = 0;
                                                }
                                                if (_fillrPayIndx != -1)
                                                {
                                                    oItems[_fillrPayIndx].Description = Convert.ToString(_fillResAmt - _fillPayAmt);
                                                    btnUseReserve.Tag = oItems;
                                                }
                                                #endregion
                                                row_index = row_index + 1;
                                            }
                                            else
                                            {
                                                #region "Set More Amount Multiple object"

                                                decimal _fillPayMulAmt = _fillPayAmt;
                                                int loopIndex = 0; //Bug : 00000840: Patient Accounts. 
                                                do
                                                {
                                                    //Bug : 00000840: Patient Accounts. 
                                                    loopIndex++;
                                                    if (Convert.ToDecimal(oItems[_fillrPayIndx].Description) > 0)
                                                    {
                                                        _fillResAmt = Convert.ToDecimal(oItems[_fillrPayIndx].Description);
                                                        _fillResPayID = Convert.ToInt64(oItems[_fillrPayIndx].ID);
                                                        _fillResPayDtlID = Convert.ToInt64(oItems[_fillrPayIndx].Code);
                                                        _fillRefFinanceLieNo = 0;
                                                        //_fillUseRefFinanceLieNo = false;

                                                        if (oItems[_fillrPayIndx].SubItems != null && oItems[_fillrPayIndx].SubItems.Count > 0)
                                                        {
                                                            _fillRefPayID = Convert.ToInt64(oItems[_fillrPayIndx].SubItems[0].ID);
                                                            _fillRefPayDtlID = Convert.ToInt64(oItems[_fillrPayIndx].SubItems[0].Description);
                                                        }

                                                        if (_fillPayMulAmt >= _fillResAmt)
                                                        { _fillPayAmt = _fillResAmt; _fillPayMulAmt = _fillPayMulAmt - _fillPayAmt; }
                                                        else
                                                        { _fillPayAmt = _fillPayMulAmt; _fillPayMulAmt = _fillPayMulAmt - _fillPayMulAmt; }

                                                        //This logic is temporary depend upon the gloItems
                                                        //when we implement partial payment it should be depend upon the "EOBInsurancePaymentMasterAllocationLines" this object
                                                        if (_fillRefPayID == 0 && _fillRefPayDtlID == 0)
                                                        {
                                                            //_fillUseRefFinanceLieNo = true;
                                                            _fillRefFinanceLieNo = _fillrPayIndx + 2;
                                                        }
                                                    }
                                                    // Bug : 00000840: Patient Accounts. Patient Used reserve and made only adjustment for some lines then CreditRefId is saving zero.
                                                    if (_fillResPayID == 0)
                                                    {
                                                        _fillResPayID = _nCreditID;
                                                    }
                                                    #region "Set object"
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows.Add();
                                                    if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                    {
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBDetailID"] = 0;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = 0;
                                                    }
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCreditID"] = _nCreditID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCredit_RefID"] = _fillResPayID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBID"] = _nEOBID;

                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientInsuranceID"] = 0;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nContactID"] = 0;
                                                    
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nInsCompanyID"] = 0;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientID"] = _nTransactionPatientID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nGuarantorID"] = this.nGuarantorID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPAccountID"] = this.nPAccountID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nAccountPatientID"] = this.nAccountPatientID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID));
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID));
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.PatientPayment.GetHashCode();
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sEntryDesc"] = "PatientPayment";
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTDesc"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dPaymentAmount"] = _fillPayAmt;

                                                    if (c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT)).Trim() != "")
                                                    { _fillAdjAmt = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_CUR_ADJ_AMOUNT))); }

                                                    //Bug : 00000840: Patient Accounts. Adjustment was saving multiple time for same line.
                                                    if (_fillAdjAmt > 0 && loopIndex == 1)
                                                    {
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dOtherAdjustmentAmount"] = _fillAdjAmt;
                                                    }
                                                    else
                                                    {
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dOtherAdjustmentAmount"] = 0;
                                                    }


                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWriteoffAmount"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWithholdAmount"] = DBNull.Value;

                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentTrayID"] = _CloseDayTrayID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentTrayDesc"] = _CloseDayTrayName;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nUserID"] = _UserId;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sUserName"] = _UserName;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nVoidType"] = 0;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidDateTime"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidCloseDate"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidDateTime"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsERAPost"] = false;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nClinicID"] = _ClinicID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCreatedDateTime"] = DateTime.Now;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtModifiedDateTime"] = DateTime.Now;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sSiteID"] = DBNull.Value;
                                                    if (IsPaymentMarkforcollectionAgency)
                                                    {
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCollectionAgencyContactID"] = CollectionAgencyContactID; //gloPatientPaymentV2.getCollectionAgencyContactID(Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_ID)), Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_TRN_DTL_ID)));
                                                    }
                                                    else
                                                    {
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCollectionAgencyContactID"] = 0;
                                                    }
                                                    #endregion

                                                    if (_fillrPayIndx != -1)
                                                    {
                                                        oItems[_fillrPayIndx].Description = Convert.ToString(_fillResAmt - _fillPayAmt);
                                                        btnUseReserve.Tag = oItems;
                                                        _fillrPayIndx = _fillrPayIndx + 1;
                                                        if (_fillrPayIndx >= oItems.Count) { break; }
                                                    }

                                                }
                                                while (_fillPayMulAmt > 0);

                                                #endregion
                                            }

                                            #endregion
                                        }
                                        dsInsurancePayment_TVP.Tables["EOB"].AcceptChanges();
                                        dsInsurancePayment_TVP.Tables["Debits"].AcceptChanges();

                                        row_num = row_num + 1;
                                    }
                                }

                                #endregion "Claim wise EOB and Finance Line"
                            }
                        }
                    }

                    #endregion "EOB and Debit Entry"

                    if (dsInsurancePayment_TVP.Tables["Debits"] != null && Convert.ToInt64(dsInsurancePayment_TVP.Tables["Debits"].Rows.Count) > 0)
                    {
                        _dtUniqueIDs = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(dsInsurancePayment_TVP.Tables["Debits"].Rows.Count);

                        for (int i = 0; i <= dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1; i++)
                        {
                            dsInsurancePayment_TVP.Tables["Debits"].Rows[i]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                            dsInsurancePayment_TVP.Tables["Debits"].Rows[i]["nDebitID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID2"].ToString());
                        }

                        for (int i = 0; i <= dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1; i++)
                        {
                            dsInsurancePayment_TVP.Tables["EOB"].Rows[i]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                            dsInsurancePayment_TVP.Tables["EOB"].Rows[i]["nEOBDetailIDEXT"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                        }
                    }

                    gloAccountsV2.gloPatientPaymentV2 objclsAccPayment = new gloAccountsV2.gloPatientPaymentV2();
                    _EOBPaymentID = objclsAccPayment.SavePatientPayment(dsInsurancePayment_TVP);
                    _retPayId = _EOBPaymentID;

                    if (_retPayId > 0)
                    {
                        CL_FollowUpCode.SetAutoAccountFollowUp(oPatientControl.PAccountID, oPatientControl.PatientID, Convert.ToDateTime(mskCloseDate.Text));
                        if (gloAccountsV2.gloBillingCommonV2.IsIntuitBillPayTaskOfSamePatient(IBPTaskID, oPatientControl.PatientID))
                        {
                            if (IsIntuitBillPay)
                            {
                                gloAccountsV2.gloBillingCommonV2.CompleteIntuitBillPayTask(IBPTaskID);
                            }
                        }
                        IsIntuitBillPay = false;
                    }
                    _EOBPaymentID = 0;
                    _IsAdjustmentMode = false;
                    btnReserveRemaining.Tag = null;

                    #region "Desing Grids"
                    _IsFormLoading = true;
                    rbPayType_Payment_CheckedChanged(null, null);
                    _IsFormLoading = false;
                    #endregion
                    //SLR: Free obClsaccpayment
                    if (objclsAccPayment != null)
                    {
                        objclsAccPayment.Dispose();
                    }
                }

                //GLO2011-0013738 : Patient Payment screen does not prompt to pick a tray
                // whenver payment tray load it will show last payment tray selected which is used for transaction.
                if (_retPayId > 0)
                {
                    gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
                    ogloBilling.SaveUserWiseCloseDay(mskCloseDate.Text.Trim(), CloseDayType.Payment, gloGlobal.gloPMGlobal.ClinicID);
                    gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloGlobal.gloPMGlobal.DatabaseConnectionString);

                    oSettings.AddSetting("PAYMENT_LASTCLOSEDATE", Convert.ToDateTime(mskCloseDate.Text).ToString("MM/dd/yyyy"), gloGlobal.gloPMGlobal.ClinicID, gloGlobal.gloPMGlobal.UserID, gloSettings.SettingFlag.User);
                    oSettings.AddSetting("PAYMENT_LASTCLOSETRAYID", _CloseDayTrayID.ToString(), gloGlobal.gloPMGlobal.ClinicID, gloGlobal.gloPMGlobal.UserID, gloSettings.SettingFlag.User);
                    oSettings.Dispose();
                    ogloBilling.Dispose();
                }
                //end

                if (!_bIsUsedReserveVal)
                {
                    _useReserves = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (dsInsurancePayment_TVP != null)
                { dsInsurancePayment_TVP.Clear(); }
               // _useReserves = false;
            }
            return _retPayId;
        }
        #endregion "Save Methods"

        private PaymentModeV2 SelectedPaymentMode
        {
            set { _SelectedEOBPaymentMode = value; }
            get
            {
                return _SelectedEOBPaymentMode;
            }
        }
        private bool SavePaymentValidation()
        {
            int _addDays = 0;
            _addDays = gloAccountsV2.gloBillingCommonV2.GetFutureCloseDayDateSettings();
            try
            {
                if (IsBusinessCenterEnable)
                {
                    if (btnReserveRemaining.Tag != null)
                    {
                        if (!gloBillingCommonV2.IsBusinessCenterAssociated(nPAccountID))
                        {
                            MessageBox.Show("Cannot save payment – Account is not assigned to a Business Center.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                    }
                }

                //If only special character is present the set to zero
                isclosecheck = false;
                
                    try
                    {
                        if (_IsPaymentCorrectionMode == false)
                        {
                            decimal dValue = 0;

                            if (Decimal.TryParse(txtCheckAmount.Text.Trim(), out dValue) == false)
                            {
                                dValue = 0;
                                txtCheckAmount.Text = dValue.ToString("#0.00"); 
                            }                            
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        ex = null;
                        decimal _amt = 0;
                        txtCheckAmount.Text = _amt.ToString("#0.00");
                    }
                
                try
                {
                    
                    decimal _ValidateAmt = 0;

                    if (Decimal.TryParse(Convert.ToString(txtCheckRemaining.Text.Trim()), out _ValidateAmt) == false)
                    {
                        _ValidateAmt = 0;
                        txtCheckRemaining.Text = _ValidateAmt.ToString("#0.00");
                    }
                    

                    //_ValidateAmt = Convert.ToDecimal(Convert.ToString(txtCheckRemaining.Text.Trim()));
                    if (_IsPaymentCorrectionMode == false)
                    {
                        if (_IsUseReserveEntry == false)
                        {
                            if (_ValidateAmt > 0)
                            {
                                MessageBox.Show("Please use the remaining amount or put it into reserve to continue with next payment.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnReserveRemaining.Focus(); btnReserveRemaining.Select();
                                return false;
                            }
                        }
                        else
                        {
                            if (_ValidateAmt > 0)
                            {
                                MessageBox.Show("Please apply all reserve amount to continue with next payment.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnReserveRemaining.Focus(); btnReserveRemaining.Select();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (_ValidateAmt > 0)
                        {
                            MessageBox.Show("Please use the remaining amount or put it into reserve to continue with next payment.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnReserveRemaining.Focus(); btnReserveRemaining.Select();
                            return false;
                        }
                    }

                    if (_ValidateAmt < 0)
                    {
                        MessageBox.Show("Remaining amount goes to negative, please correct the allocation to continue with next payment.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    ex = null;
                    decimal _amt = 0;
                    txtCheckRemaining.Text = _amt.ToString("#0.00");
                }
                if (mskCloseDate.MaskCompleted == false)
                {
                    MessageBox.Show("Please enter the close date", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Focus();
                    mskCloseDate.Select();
                    return false;
                }

                if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                {
                    for (int rInd = 1; rInd < c1SinglePayment.Rows.Count; rInd++)
                    {
                        if (c1SinglePayment.GetData(rInd, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(rInd, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                        {
                            if (c1SinglePayment.GetData(rInd, COL_CUR_ADJ_AMOUNT) != null
                                && Convert.ToString(c1SinglePayment.GetData(rInd, COL_CUR_ADJ_AMOUNT)).Trim() != ""
                                && (Convert.ToDecimal(c1SinglePayment.GetData(rInd, COL_CUR_ADJ_AMOUNT)) > 0
                                || Convert.ToDecimal(c1SinglePayment.GetData(rInd, COL_CUR_ADJ_AMOUNT)) < 0)
                               )
                            {
                                if (c1SinglePayment.GetData(rInd, COL_CUR_ADJ_TYPECODE) == null ||
                                    Convert.ToString(c1SinglePayment.GetData(rInd, COL_CUR_ADJ_TYPECODE)).Trim() == "")
                                {
                                    //_IsAdjustmentMode = true;
                                    string _Msg = "";
                                    string _cptCode = "";
                                    if (c1SinglePayment.GetData(rInd, COL_CPT_CODE) != null && Convert.ToString(c1SinglePayment.GetData(rInd, COL_CPT_CODE)).Trim() != "")
                                    { _cptCode = Convert.ToString(c1SinglePayment.GetData(rInd, COL_CPT_CODE)).Trim(); }
                                    _Msg = " Select adjustment code for Charge ('" + _cptCode + "') ";
                                    MessageBox.Show(_Msg, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return false;
                                }
                            }
                        }
                    }
                }

                gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, "");
                if (ogloBilling.IsDayClosed(Convert.ToDateTime(mskCloseDate.Text)) == true)
                {
                    MessageBox.Show("Selected date is already closed. Please select a different close date.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Select(); mskCloseDate.Focus();
                    ogloBilling.Dispose();
                    ogloBilling = null;
                    return false;
                }
                ogloBilling.Dispose();
                ogloBilling = null;
                if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(_addDays))
                {
                    MessageBox.Show("Close Date " + mskCloseDate.Text.Trim() + " is too far in the future.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    mskCloseDate.Focus();
                    mskCloseDate.Select();
                    return false;
                }
                else
                {
                    if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date)
                    {
                        DialogResult _dlgCloseDate = DialogResult.None;
                        _dlgCloseDate = MessageBox.Show("Close Date " + mskCloseDate.Text.Trim() + " is in future. Are you sure you want to continue with save?", _messageboxcaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                        if (_dlgCloseDate == DialogResult.Cancel)
                        {
                            mskCloseDate.Focus();
                            mskCloseDate.Select();
                            return false;
                        }
                    }
                }


                if (lblPaymentTray.Tag == null || lblPaymentTray.Tag.ToString().Trim().Length <= 0 || lblPaymentTray.Text.Trim() == "")
                {
                    MessageBox.Show("Please select payment tray.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else
                {

                    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                    oDB.Connect(false);
                    object _retVal = null;
                    _retVal = oDB.ExecuteScalar_Query("SELECT sDescription FROM BL_CloseDayTray WHERE nCloseDayTrayID = " + Convert.ToInt64(lblPaymentTray.Tag) + " and ISNULL(bIsActive,0) = 1 AND nClinicID = " + _ClinicID + "");
                    if (_retVal == null || _retVal.ToString().Trim().Length == 0)
                    {
                        MessageBox.Show("Selected payment tray is inactive. Please select the another payment tray.", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    oDB.Disconnect();
                    oDB.Dispose();
                }

                if (_IsUseReserveEntry == false && _IsPaymentCorrectionMode == false)
                {
                    if (IsPaymentMade() == true && _IsAdjustmentMode == true)
                    {
                        return true;
                    }
                }
                else if (_IsPaymentCorrectionMode == true && _IsUseReserveEntry == false)
                {
                    if (IsPaymentMade() == true && _IsAdjustmentMode == true)
                    { return true; }
                }

                #region " Check Payment Mode "
                PaymentMode _EOBPaymentMode = PaymentMode.None;

                if (_IsUseReserveEntry == false && _IsPaymentCorrectionMode == false)
                {
                    if (cmbPayMode.Text.Trim() == PaymentMode.Cash.ToString())
                    { _EOBPaymentMode = PaymentMode.Cash; }
                    if (cmbPayMode.Text.Trim() == PaymentMode.Check.ToString())
                    { _EOBPaymentMode = PaymentMode.Check; }
                    else if (cmbPayMode.Text.Trim() == PaymentMode.MoneyOrder.ToString())
                    { _EOBPaymentMode = PaymentMode.MoneyOrder; }
                    else if (cmbPayMode.Text.Trim() == PaymentMode.CreditCard.ToString())
                    { _EOBPaymentMode = PaymentMode.CreditCard; }
                    else if (cmbPayMode.Text.Trim() == PaymentMode.EFT.ToString())
                    { _EOBPaymentMode = PaymentMode.EFT; }

                    else if (cmbPayMode.Text.Trim() == PaymentMode.Voucher.ToString())
                    { _EOBPaymentMode = PaymentMode.Voucher; }

                    if (_EOBPaymentMode == PaymentMode.None)
                    {
                        MessageBox.Show("Select the payment mode", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cmbPayMode.Select();
                        cmbPayMode.Focus();
                        return false;
                    }
                    else if (_EOBPaymentMode == PaymentMode.CreditCard)
                    {

                        if (mskCheckDate.MaskCompleted == false)
                        {
                            MessageBox.Show("Please enter the " + _EOBPaymentMode.ToString().ToLower() + " date", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskCheckDate.Select();
                            mskCheckDate.Focus();
                            return false;
                        }

                        if (cmbCardType == null || cmbCardType.Items.Count <= 0 || cmbCardType.Text.Trim() == "")
                        {
                            MessageBox.Show("Please select the card type", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cmbCardType.Select();
                            cmbCardType.Focus();
                            return false;
                        }

                        mskCreditExpiryDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                        if (mskCreditExpiryDate.Text != "")
                        {
                            if (mskCreditExpiryDate.MaskFull == false)
                            {
                                MessageBox.Show("Please enter valid " + _EOBPaymentMode.ToString() + " expiration date (MM/yy).", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                mskCreditExpiryDate.Select();
                                mskCreditExpiryDate.Focus();
                                return false;
                            }
                        }

                    }
                    else if (_EOBPaymentMode == PaymentMode.Check)
                    {

                        if (txtCheckNumber.Text.Trim() == "")
                        {
                            MessageBox.Show("Please enter the " + _EOBPaymentMode.ToString() + " number", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCheckNumber.Text = "";
                            txtCheckNumber.Select();
                            txtCheckNumber.Focus();
                            return false;
                        }


                        if (mskCheckDate.MaskCompleted == false)
                        {
                            MessageBox.Show("Please enter the " + _EOBPaymentMode.ToString() + " date", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskCheckDate.Select();
                            mskCheckDate.Focus();
                            return false;
                        }
                    }
                    else if (_EOBPaymentMode == PaymentMode.EFT)
                    {
                        if (txtCheckNumber.Text.Trim() == "")
                        {
                            MessageBox.Show("Please enter the " + _EOBPaymentMode.ToString() + " number", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCheckNumber.Select();
                            txtCheckNumber.Focus();
                            return false;
                        }
                    }

                    else if (_EOBPaymentMode == PaymentMode.Voucher)
                    {
                        if (txtCheckNumber.Text.Trim() == "")
                        {
                            MessageBox.Show("Please enter the " + _EOBPaymentMode.ToString() + " number", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCheckNumber.Select();
                            txtCheckNumber.Focus();
                            return false;
                        }

                        if (mskCheckDate.MaskCompleted == false)
                        {
                            MessageBox.Show("Please enter the " + _EOBPaymentMode.ToString() + " date", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskCheckDate.Select();
                            mskCheckDate.Focus();
                            return false;
                        }
                    }
                }

                #endregion " Check Payment Mode "

                if (_IsPaymentCorrectionMode == false)
                {
                    if (txtCheckAmount.Text.Trim() == "" || Convert.ToDecimal(txtCheckAmount.Text.Trim()) <= 0)
                    {
                        MessageBox.Show("Please enter the amount", _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCheckAmount.Select();
                        txtCheckAmount.Focus();
                        return false;
                    }
                }

                #region " Warning Message if the entered check number already exists "

                if (_EOBPaymentMode == PaymentMode.Check || _EOBPaymentMode == PaymentMode.Voucher)
                {
                    string _checkNo = "";
                    DateTime _checkDate = DateTime.Now;
                    decimal _checkAmount = 0;
                    string _showCheckDate = "";

                    if (txtCheckNumber.Text.Trim() != "")
                    { _checkNo = txtCheckNumber.Text.Trim(); }

                    if (mskCheckDate.MaskCompleted == true)
                    {
                        _checkDate = Convert.ToDateTime(mskCheckDate.Text);
                        _showCheckDate = mskCheckDate.Text;
                    }

                    if (txtCheckAmount.Text.Trim() != "")
                    { _checkAmount = Convert.ToDecimal(txtCheckAmount.Text.Trim()); }

                    if (_EOBPaymentID <= 0)
                    {

                        if (gloAccountsV2.gloInsurancePaymentV2.IsExistCheck(_checkNo, _checkDate, _checkAmount, PayerTypeV2.Self, _EOBPaymentMode.GetHashCode()) == true)
                        {
                            DialogResult _checkDlg = DialogResult.None;
                            string _message = "";
                            if (_EOBPaymentMode == PaymentMode.Check)
                            {
                                _message = " Same Check with Check#: " + _checkNo + ", Check Date: " + _showCheckDate + Environment.NewLine + " and Check Amount: $" + _checkAmount.ToString("#0.00") + " " + Environment.NewLine + " already exists in the system." + Environment.NewLine + " Do you want to continue with save? ";
                            }
                            else if (_EOBPaymentMode == PaymentMode.Voucher)
                            {
                                _message = " Same Voucher with Voucher#: " + _checkNo + ", Voucher Date: " + _showCheckDate + Environment.NewLine + " and Voucher Amount: $" + _checkAmount.ToString("#0.00") + " " + Environment.NewLine + " already exists in the system." + Environment.NewLine + " Do you want to continue with save? ";
                            }

                            _checkDlg = MessageBox.Show(_message, _messageboxcaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                            if (_checkDlg == DialogResult.Cancel)
                            {
                                txtCheckNumber.SelectAll(); txtCheckNumber.Focus();
                                return false;
                            }
                        }
                    }

                }

                #endregion

                if (c1SinglePayment == null || c1SinglePayment.Rows.Count <= 1)
                {
                    bool _rValue = false;
                    if (btnReserveRemaining.Tag != null && btnReserveRemaining.Tag.ToString().Trim().Length > 0)
                    {
                        //0 ReserveAmount //1 ReserveNote //2 ReserveSubType //3 ReserveNoteOnPrint 

                        string[] oList = null;
                        if (btnReserveRemaining.Tag != null)
                        {
                            oList = btnReserveRemaining.Tag.ToString().Split('~');
                        }

                        if (oList != null && oList.Length == 8)
                        {
                            if (oList[0] != null && oList[0].ToString().Trim() != "")
                            {
                                if (Convert.ToDecimal(oList[0].ToString().Trim()) == Convert.ToDecimal(txtCheckAmount.Text.Trim()))
                                {
                                    _rValue = true;
                                }
                            }
                        }
                    }
                    return _rValue;
                }
                else
                { c1SinglePayment.FinishEditing(); }


                //Validate used reserves availability for void and refund
                if (Convert.ToString(btnUseReserve.Tag).Trim() != "")
                {
                    gloGeneralItem.gloItems _usedReservesToValidate = null;
                    try
                    {
                        //try convert to gloItems
                        _usedReservesToValidate = ((gloGeneralItem.gloItems)btnUseReserve.Tag);

                        string _reserveStatus = "";

                        if (_usedReservesToValidate != null && _usedReservesToValidate.Count > 0)
                        {
                            Int64 _reserveId = 0;
                            Int64 _creditId = 0;
                            decimal _amountUsed = 0;
                            

                            foreach (gloGeneralItem.gloItem item in _usedReservesToValidate)
                            {
                                _reserveId = 0;
                                _creditId = 0;
                                _amountUsed = 0;
                                _reserveStatus = "";

                                _reserveId = Convert.ToInt64(item.Code);
                                _creditId = Convert.ToInt64(item.ID);
                                _amountUsed = Convert.ToDecimal(item.Description);
                                _reserveStatus = gloPatientPaymentV2.GetReserveStatus(_creditId, _reserveId, _amountUsed);

                                if (_reserveStatus.Trim() != "")
                                {
                                    MessageBox.Show(_reserveStatus.Replace("\\n", Environment.NewLine), _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    break;
                                }

                            }
                        }

                        if (_reserveStatus.Trim() != "")
                        { return false; }
                    }
                    catch
                    { 
                        //blank catch
                    }
                    
                }

                DataTable dtCollectionAgency = null;
                gloContacts.clsCollectionAgency oCollection = null;

                DialogResult oresult = DialogResult.Cancel;
                oCollection = new gloContacts.clsCollectionAgency(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                dtCollectionAgency = oCollection.GetCollectionAgencyforPayment();
                if (gloPatientPaymentV2.IsBadDebtPatient(oPatientControl.PAccountID, oPatientControl.PatientID) && dtCollectionAgency != null && dtCollectionAgency.Rows.Count > 0)
                {
                    frmSelectPaymentcollectionAgency oCollectionAgency = new frmSelectPaymentcollectionAgency(gloGlobal.gloPMGlobal.DatabaseConnectionString, dtCollectionAgency);
                    oCollectionAgency.ShowDialog(this);
                    oresult = oCollectionAgency.DialogResult;
                    if (oresult == DialogResult.OK)
                    {
                        CollectionAgencyContactID = oCollectionAgency.ContactId_Collection;
                        if (CollectionAgencyContactID > 0)
                        {
                            IsPaymentMarkforcollectionAgency = true;
                        }
                    }
                    else if (oresult == DialogResult.Cancel)
                    {
                        CollectionAgencyContactID = 0;
                        IsPaymentMarkforcollectionAgency = false;
                    }
                    else
                    {
                        return false;
                    }
                    oCollection.Dispose();
                    oCollection = null;

                }  

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
            finally
            { }

            return true;
        }
        private DialogResult SetNewPayment()
        {
            DialogResult _dlgRst = DialogResult.None;
            if (IsPaymentMade() == true)
            {

                _dlgRst = MessageBox.Show("Do you want to save changes ?", _messageboxcaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (_dlgRst == DialogResult.Yes)
                {
                    if (_IsAllDatesValid == true && SavePaymentValidation())
                    {
                        SavePatientPayment();


                        lblPayType.Enabled = true;
                        cmbPayMode.Enabled = true;
                        lblCheckNo.Enabled = true;
                        txtCheckNumber.Enabled = true;
                        lblCheckDate.Enabled = true;
                        mskCheckDate.Enabled = true;
                        lblCardType.Enabled = true;
                        cmbCardType.Enabled = true;
                        lblCardAuthorizationNo.Enabled = true;
                        txtCardAuthorizationNo.Enabled = true;
                        lblExpiryDate.Enabled = true;
                        mskCreditExpiryDate.Enabled = true;
                        tls_btnReceipt.Visible = true;
                        if (tls_btnDefaultReceipt.Tag != null && Convert.ToString(tls_btnDefaultReceipt.Tag).Trim().Length > 0)
                        { tls_btnDefaultReceipt.Visible = true; }
                        ClearFormData();
                        lblCheckAmount.Visible = true;
                        txtCheckAmount.Visible = true;
                        label6.Visible = true;
                        btnDistubuteAmount.Visible = true;
                        btnUseReserve.Visible = true;
                        _IsPaymentCorrectionMode = false;
                        _IsUseReserveEntry = false;
                        txtPatientSearch.Enabled = true;
                        IsIntuitBillPay = false;
                    }
                    else
                    {
                        _dlgRst = DialogResult.Cancel;
                    }
                }
                else if (_dlgRst == DialogResult.No)
                {
                    lblPayType.Enabled = true;
                    cmbPayMode.Enabled = true;
                    lblCheckNo.Enabled = true;
                    txtCheckNumber.Enabled = true;
                    lblCheckDate.Enabled = true;
                    mskCheckDate.Enabled = true;
                    lblCardType.Enabled = true;
                    cmbCardType.Enabled = true;
                    lblCardAuthorizationNo.Enabled = true;
                    txtCardAuthorizationNo.Enabled = true;
                    lblExpiryDate.Enabled = true;
                    mskCreditExpiryDate.Enabled = true;
                    tls_btnReceipt.Visible = true;
                    if (tls_btnDefaultReceipt.Tag != null && Convert.ToString(tls_btnDefaultReceipt.Tag).Trim().Length > 0)
                    { tls_btnDefaultReceipt.Visible = true; }
                    ClearFormData();
                    lblCheckAmount.Visible = true;
                    txtCheckAmount.Visible = true;
                    label6.Visible = true;
                    btnDistubuteAmount.Visible = true;
                    btnUseReserve.Visible = true;
                    _IsPaymentCorrectionMode = false;
                    _IsUseReserveEntry = false;
                    txtPatientSearch.Enabled = true;
                    IsIntuitBillPay = false;

                }
                else if (_dlgRst == DialogResult.Cancel)
                { }
            }
            else
            {
                lblPayType.Enabled = true;
                cmbPayMode.Enabled = true;
                lblCheckNo.Enabled = true;
                txtCheckNumber.Enabled = true;
                lblCheckDate.Enabled = true;
                mskCheckDate.Enabled = true;
                lblCardType.Enabled = true;
                cmbCardType.Enabled = true;
                lblCardAuthorizationNo.Enabled = true;
                txtCardAuthorizationNo.Enabled = true;
                lblExpiryDate.Enabled = true;
                mskCreditExpiryDate.Enabled = true;
                tls_btnReceipt.Visible = true;
                if (tls_btnDefaultReceipt.Tag != null && Convert.ToString(tls_btnDefaultReceipt.Tag).Trim().Length > 0)
                { tls_btnDefaultReceipt.Visible = true; }

                ClearFormData();
                lblCheckAmount.Visible = true;
                txtCheckAmount.Visible = true;
                label6.Visible = true;
                btnDistubuteAmount.Visible = true;
                btnUseReserve.Visible = true;
                _IsPaymentCorrectionMode = false;
                _IsUseReserveEntry = false;
                txtPatientSearch.Enabled = true;
                IsIntuitBillPay = false;

            }
            return _dlgRst;
        }
        private void SetIntuitBillPay()
        {
            CreditCards oCreditCards = null;
                DataTable _dtCards = null;
            try
            {
                //foreach (ToolStripItem StripButton in this.tls_Top.Items)
                //{
                //    if (StripButton.Equals(StripButton))
                //    {
                //        StripButton.Visible = false;
                //    }
                //}

                //tls_btnSaveNClose.Visible = true;
                //tls_btnClose.Visible = true;

                cmbPayMode.Text = PaymentMode.CreditCard.ToString();
                txtCardAuthorizationNo.Text = IBPAuthNumber;
                txtCheckAmount.Text = Convert.ToString(IBPCheckamount.ToString("#0.00"));
                txtCheckNumber.Text = IBPReferenceNumber;

                if (IBPCheckamount > 0)
                { CalculateRemainingAmount(); }
                //oPatientControl.DisableInputControl = true;
                oCreditCards = new CreditCards(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                //DataTable _dtCards = null;
                _dtCards = oCreditCards.GetList();
                if (_dtCards != null && _dtCards.Rows.Count > 0)
                {
                    for (int i = 0; i < _dtCards.Rows.Count; i++)
                    {
                        if (IBPCardType == Convert.ToString(_dtCards.Rows[i]["sCreditCardDesc"]))
                        {
                            cmbCardType.SelectedIndex = i + 1;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
            finally
            {
                //SLR: Free oCreditCards, _dtCards
                if (oCreditCards!=null)
                {
                    oCreditCards.Dispose();
                }
                if (_dtCards!=null)
                {
                    _dtCards.Dispose();
                }
            }
        }

        private void SetCreditsDetails(gloBilling.gloAccountPayment.dsPaymentTVP_V2 dsInsurancePayment_TVP, Int64 _nCreditID)
        {
            Int64 _CloseDayTrayID = 0;
            string _CloseDayTrayName = "";
            PaymentModeV2 _EOBPaymentMode = PaymentModeV2.None;
            #region "Payment Tray"
            _CloseDayTrayID = Convert.ToInt64(lblPaymentTray.Tag.ToString().Trim());
            _CloseDayTrayName = lblPaymentTray.Text;
            #endregion

            #region "Payment Mode"
            if (cmbPayMode.Text != "")
            {
                if (cmbPayMode.Text.Trim() == PaymentModeV2.None.ToString())
                { _EOBPaymentMode = PaymentModeV2.None; }
                else if (cmbPayMode.Text.Trim() == PaymentModeV2.Cash.ToString())
                { _EOBPaymentMode = PaymentModeV2.Cash; }
                else if (cmbPayMode.Text.Trim() == PaymentModeV2.Check.ToString())
                { _EOBPaymentMode = PaymentModeV2.Check; }
                else if (cmbPayMode.Text.Trim() == PaymentModeV2.MoneyOrder.ToString())
                { _EOBPaymentMode = PaymentModeV2.MoneyOrder; }
                else if (cmbPayMode.Text.Trim() == PaymentModeV2.CreditCard.ToString())
                { _EOBPaymentMode = PaymentModeV2.CreditCard; }
                else if (cmbPayMode.Text.Trim() == PaymentModeV2.EFT.ToString())
                { _EOBPaymentMode = PaymentModeV2.EFT; }
                else if (cmbPayMode.Text.Trim() == PaymentModeV2.Voucher.ToString())
                { _EOBPaymentMode = PaymentModeV2.Voucher; }
            }
            #endregion

            dsInsurancePayment_TVP.Tables["Credits"].Rows.Add();

            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nCreditID"] = _nCreditID;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sReceiptNo"] = txtCheckNumber.Text.Trim();
            if (txtCheckAmount.Text.Trim() != "") { dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["dReceiptAmount"] = Convert.ToDecimal(txtCheckAmount.Text); }
            if (mskCheckDate.MaskCompleted)
            {
                mskCheckDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["dtReceiptDate"] = Convert.ToDateTime(mskCheckDate.Text);
            }
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPayerType"] = PayerTypeV2.Self.GetHashCode();
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPayerID"] = _PatientID;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sPayerName"] = "";
            if (lblPaymetNo.Text != "")
            {
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sPaymentNo"] = lblPaymetNo.Text.Trim().Split('#')[1];
            }
            //None = 0,
            //Cash = 1,
            //Check = 2,
            //MoneyOrder = 3,
            //CreditCard = 4,
            //EFT = 5
            if (_EOBPaymentMode == PaymentModeV2.Check)
            { dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 2; }
            else if (_EOBPaymentMode == PaymentModeV2.Cash)
            { dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 1; }
            else if (_EOBPaymentMode == PaymentModeV2.EFT)
            { dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 5; }
            else if (_EOBPaymentMode == PaymentModeV2.Voucher)
            { dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 6; }
            else if (_EOBPaymentMode == PaymentModeV2.MoneyOrder)
            { dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 3; }
            else if (_EOBPaymentMode == PaymentModeV2.CreditCard)
            { dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 4; }
            else
            { dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 0; }

            if (mskCloseDate.MaskCompleted == true)
            {
                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
            }
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentTrayID"] = _CloseDayTrayID;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sPaymentTrayDesc"] = _CloseDayTrayName;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nUserID"] = _UserId;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sUserName"] = _UserName;

            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["bIsPaymentVoid"] = 0;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nVoidType"] = 0;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPAccountID"] = this.nPAccountID;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nAccountPatientID"] = this.nAccountPatientID;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nGuarantorID"] = this.nGuarantorID;

            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sPaymentNote"] = "Payment Note";
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = "Blank Tray";
            if (_IsPaymentCorrectionMode)
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.PatientCorrection.GetHashCode();
            else if (_IsUseReserveEntry)
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.UseReserve.GetHashCode();
            else
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.PatientPayment.GetHashCode();
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["Credits_EXTID"] = 0;
            if (_EOBPaymentMode == PaymentModeV2.CreditCard)
            {
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["CreditCardType"] = cmbCardType.Text.Trim();
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["AuthorizationNo"] = txtCardAuthorizationNo.Text.Trim();
                if (mskCreditExpiryDate.MaskCompleted)
                {
                    mskCreditExpiryDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sCreditCardExpiryDate"] = mskCreditExpiryDate.Text;
                }
                if (mskCreditExpiryDate.MaskCompleted)
                {
                    mskCreditExpiryDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                }
            }
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["ClinicID"] = _ClinicID;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["PaymentVoidDateTime"] = DBNull.Value;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["CreatedDateTime"] = DateTime.Now;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["ModifiedDateTime"] = DateTime.Now;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["SiteID"] = "";
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["IsFinished"] = false;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["IsERAPost"] = false;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["BPRID"] = DBNull.Value;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["CollectionAgencyContactID"] = CollectionAgencyContactID ;
            if (IsPaymentMarkforcollectionAgency)
            {
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["bIsCollectionAgencyPayment"] = true;
            }
            else
            {
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["bIsCollectionAgencyPayment"] = false ;
            }


            dsInsurancePayment_TVP.Tables["Credits"].AcceptChanges();
        }
        #region " Clear Form for new Payment "
        private bool IsPaymentMade()
        {
            try
            {
                _IsAdjustmentMode = false;
                c1SinglePayment.FinishEditing();
                if (_IsPaymentCorrectionMode == false)
                {
                    if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                    {
                        for (int rIndex = 1; rIndex < c1SinglePayment.Rows.Count; rIndex++)
                        {
                            if (c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                            {
                                if (c1SinglePayment.GetData(rIndex, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CUR_PAYMENT)).Trim() != ""
                                   && (Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_CUR_PAYMENT)) > 0
                                   || Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_CUR_PAYMENT)) < 0))
                                {
                                    _IsAdjustmentMode = false;
                                    return true;
                                }
                            }
                        }

                        if ((txtCheckAmount.Text.Trim().Length > 0 && Convert.ToDecimal(txtCheckAmount.Text.Trim()) > 0)
                          || txtCheckRemaining.Text.Trim().Length > 0 && Convert.ToDecimal(txtCheckRemaining.Text.Trim()) > 0)
                        {
                            return true;
                        }
                        //Bug #86189: 00000941 : Blank check number getting saved in patient payment
                        for (int rIndex = 1; rIndex < c1SinglePayment.Rows.Count; rIndex++)
                        {
                            if (c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                            {
                                if (c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_AMOUNT)).Trim() != ""
                                         && (Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_AMOUNT)) > 0
                                         || Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_AMOUNT)) < 0)
                                       )
                                {
                                    //if (((txtCheckAmount.Text.Trim().Length <= 0 && Convert.ToDecimal(txtCheckAmount.Text.Trim()) <= 0)|| txtCheckRemaining.Text.Trim().Length <= 0 && Convert.ToDecimal(txtCheckRemaining.Text.Trim()) <= 0) && c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_TYPECODE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_TYPECODE)).Trim() != "")
                                    //{
                                    //    _IsAdjustmentMode = true;
                                    //    return true;
                                    //}
                                    //else if (((txtCheckAmount.Text.Trim().Length > 0 && Convert.ToDecimal(txtCheckAmount.Text.Trim()) > 0)|| txtCheckRemaining.Text.Trim().Length > 0 && Convert.ToDecimal(txtCheckRemaining.Text.Trim()) > 0) && c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_TYPECODE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_TYPECODE)).Trim() != "")
                                    //{
                                    //     return true;
                                    //}
                                    if (c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_TYPECODE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_TYPECODE)).Trim() != "")
                                    {
                                        _IsAdjustmentMode = true;
                                        return true;
                                    }
                                   
                                }
                            }
                        }
                    }

                    //if ((txtCheckAmount.Text.Trim().Length > 0 && Convert.ToDecimal(txtCheckAmount.Text.Trim()) > 0)
                    //    || txtCheckRemaining.Text.Trim().Length > 0 && Convert.ToDecimal(txtCheckRemaining.Text.Trim()) > 0)
                    //{
                    //    return true;
                    //}
                }
                else
                {
                    if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                    {
                        for (int rIndex = 1; rIndex < c1SinglePayment.Rows.Count; rIndex++)
                        {
                            if (c1SinglePayment.GetData(rIndex, COL_CUR_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CUR_PAYMENT)).Trim() != ""
                               && (Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_CUR_PAYMENT)) > 0
                               || Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_CUR_PAYMENT)) < 0))
                            {
                                //If adjustment correction is also done with correction payment
                                if (c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_TYPECODE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_TYPECODE)).Trim() != "")
                                {
                                    //return true;
                                    //}
                                    if (c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_AMOUNT)).Trim() != ""
                                         && (Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_AMOUNT)) > 0
                                         || Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_AMOUNT)) < 0)
                                       )
                                    {
                                        _IsAdjustmentMode = true;
                                        return true;
                                    }
                                }
                                //return true;
                            }

                            if (c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_TYPECODE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_TYPECODE)).Trim() != "")
                            {
                                //return true;
                                //}
                                if (c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_AMOUNT) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_AMOUNT)).Trim() != ""
                                     && (Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_AMOUNT)) > 0
                                     || Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_CUR_ADJ_AMOUNT)) < 0)
                                   )
                                {
                                    _IsAdjustmentMode = true;
                                    return true;
                                }
                            }

                        }
                    }

                    if (txtCheckRemaining.Text.Trim().Length > 0 && Convert.ToDecimal(txtCheckRemaining.Text.Trim()) > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, _messageboxcaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return false;
        }
        private void ClearFormData()
        {
            DataTable _dtUniquePaymentPrfixNumber = null;
            try
            {
                //DesignPaymentGrid(c1SinglePayment);
                //DesignPaymentGrid(c1SinglePaymentTotal);


                tls_btnSave.Visible = true;
                tls_btnSaveNClose.Visible = true;
                tls_btnClose.Visible = true;
                tls_btnNew.Visible = true;
                tls_btnReceipt.Visible = true;
                btnUseReserve.Tag = null;
                _IsUseReserveEntry = false;
                tls_btnReceipt.Enabled = true;

                _IsPaymentCorrectionMode = false;
                _IsAdjustmentMode = false;
                //gloPMMasters.GetPaymentTrays();
                FillPaymentMode();
                FillPrintReceipt();
                FillCreditCards();

                if(MaxCloseDate != DateTime.MinValue)
                    mskCloseDate.Text = MaxCloseDate.ToString("MM/dd/yyyy");
                else
                    SetCloseDate();

                btnReserveRemaining.Tag = null;

                rbPayType_Payment_CheckedChanged(null, null);
                txtPatientSearch.Select(); txtPatientSearch.Focus();
                txtPayMstNotes.Text = "";
                txtCheckNumber.Text = "";
                dtReserveforDOS = DateTime.MinValue;
                chkPayMstIncludeNotes.Checked = false;

                txtPatientSearch.Enabled = true;

                lblPaymetNo.Text = "";
                _dtUniquePaymentPrfixNumber = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);
                if (_dtUniquePaymentPrfixNumber != null && _dtUniquePaymentPrfixNumber.Rows.Count > 0)
                {
                    lblPaymetNo.Text = "GPM#" + Convert.ToString(_dtUniquePaymentPrfixNumber.Rows[0]["ID"].ToString());
                }
                

                if (_PatientID > 0)
                {
                    if (_IsPatientAccountFeature)
                    {
                        if (!oPatientControl.IsAllAccPatSelected)
                        {
                            FillBillingTransactionAccountPatient(oPatientControl.PatientID, oPatientControl.PAccountID);
                        }
                        else
                        {
                            FillBillingTransactionAccount(oPatientControl.PAccountID, true);
                        }
                    }
                    else
                    {
                        FillBillingTransaction(oPatientControl.PatientID, true);
                    }
                }
                else if (_IsPatientAccountFeature == true && _PatientID == 0 && oPatientControl.PAccountID > 0)
                {
                    FillBillingTransactionAccount(oPatientControl.PAccountID, true);
                }

                if (ogloPatientPayment != null)
                { ogloPatientPayment.Dispose(); }
                if (! gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
                {
                    IsPaymentMarkforcollectionAgency= false;                   
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        private void LoadFormDataOnPatientChanged()
        {
            DataTable _dtUniquePaymentPrfixNumber = null;
            try
            {
                //DesignPaymentGrid(c1SinglePayment);
                //DesignPaymentGrid(c1SinglePaymentTotal);


                tls_btnSave.Visible = true;
                tls_btnSaveNClose.Visible = true;
                tls_btnClose.Visible = true;
                tls_btnNew.Visible = true;
                tls_btnReceipt.Visible = true;
                btnUseReserve.Tag = null;
                _IsUseReserveEntry = false;
                tls_btnReceipt.Enabled = true;

                _IsPaymentCorrectionMode = false;
                _IsAdjustmentMode = false;
                //gloPMMasters.GetPaymentTrays();

                FillPaymentMode();
                FillPrintReceipt();
                FillCreditCards();

                //mskCloseDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                SetCloseDate();

                btnReserveRemaining.Tag = null;

                rbPayType_Payment_CheckedChanged(null, null);
                txtPatientSearch.Select(); txtPatientSearch.Focus();
                txtPayMstNotes.Text = "";
                txtCheckNumber.Text = "";
                chkPayMstIncludeNotes.Checked = false;

                txtPatientSearch.Enabled = true;

                lblPaymetNo.Text = "";
                _dtUniquePaymentPrfixNumber = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);
                if (_dtUniquePaymentPrfixNumber != null && _dtUniquePaymentPrfixNumber.Rows.Count > 0)
                {
                    lblPaymetNo.Text = "GPM#" + Convert.ToString(_dtUniquePaymentPrfixNumber.Rows[0]["ID"].ToString());
                }


                if (_PatientID > 0)
                {
                    if (_IsPatientAccountFeature)
                    {
                        if (!oPatientControl.IsAllAccPatSelected)
                        {
                            FillBillingTransactionAccountPatient(oPatientControl.PatientID, oPatientControl.PAccountID);
                        }
                        else
                        {
                            FillBillingTransactionAccount(oPatientControl.PAccountID, true);
                        }
                    }
                    else
                    {
                        FillBillingTransaction(oPatientControl.PatientID, true);
                    }
                }
                else if (_IsPatientAccountFeature == true && _PatientID == 0 && oPatientControl.PAccountID > 0)
                {
                    FillBillingTransactionAccount(oPatientControl.PAccountID, true);
                }
                AllowClaimsEdit();
                if (ogloPatientPayment != null)
                { ogloPatientPayment.Dispose(); }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
            }
        }

        #endregion " Clear Form for new Payment "
        private void c1SinglePayment_MouseMove(object sender, MouseEventArgs e)
        {

            if (c1SinglePayment.HitTest(e.X, e.Y).Column == COL_CRESP_PARTY && c1SinglePayment.HitTest(e.X, e.Y).Column == COL_CRESP_PARTY)
            {
                gloBilling.gloC1FlexStyle.ShowToolTipForLineBreak(C1SuperTooltip1, ((C1.Win.C1FlexGrid.C1FlexGrid)sender), e.Location);

            }
            else
            {
                gloBilling.gloC1FlexStyle.ShowToolTip(C1SuperTooltip1, (C1FlexGrid)sender, e.Location);
            }
        }
        private void btnModifyGlobalPeriod_Click(object sender, EventArgs e)
        {
            frmSetupModifyGlobalPeriod objfrmSetupModifyGlobalPeriod = new frmSetupModifyGlobalPeriod(nGlobalPeriodId);
            try
            {
                objfrmSetupModifyGlobalPeriod.ShowDialog(this);
                DisplayGlobalPeriodAlert(_PatientID);
            }
            finally
            {
                if (objfrmSetupModifyGlobalPeriod != null)
                    objfrmSetupModifyGlobalPeriod.Dispose(); 
            }
        }
        private void tsb_ViewBenefit_Click(object sender, EventArgs e)
        {
            GetControlSelection();
            gloPMGeneral.frmViewBenefit ofrm = new gloPMGeneral.frmViewBenefit(_PatientID, gloGlobal.gloPMGlobal.DatabaseConnectionString);
            ofrm.StartPosition = FormStartPosition.CenterParent;
            ofrm.ShowDialog(this);
            //SLR: Free ofrm
            if (ofrm!=null)
            {
                ofrm.Dispose();
            }
            SetControlSelection();
        }

        private void frmPatientPaymentV2_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                RemoveGotFocusListener(this);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            if (dsInsurancePayment_TVP != null)
            {
                dsInsurancePayment_TVP.Dispose();
                dsInsurancePayment_TVP = null;
            }
            if (ogloPatientPayment != null)
            {
                ogloPatientPayment.Dispose();
            }
            if (c1SinglePayment != null)
            {
                c1SinglePayment.Dispose();
                c1SinglePayment = null;
            }

            //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
            //dispose oPatientControl, Font_CellStyle, Font_Template.
            if (oPatientControl != null)
            {
                oPatientControl.Dispose();
                oPatientControl = null;
            }
            //Font_CellStyle.Dispose();
            //Font_CellStyle = null;

            //Font_Template.Dispose();
            //Font_Template = null;

            //GC.Collect();
            //GC.WaitForPendingFinalizers();

        }

        // Problem# : 00000353 : USER: "bpemberton" posted payments, but upon running their daily payment report the user is displaying as "jfoster". 
        // when  login with 'User 1' >> patient Payment screen then after Lock screen 'user 2' entered, but patient payment screen still hold the 'user 1'.
        // To resolve this issue we have added this form event to get the new login user after Lock screen.
        private void frmPatientPaymentV2_Activated(object sender, EventArgs e)
        {
            _UserId = gloGlobal.gloPMGlobal.UserID;
            _UserName = gloGlobal.gloPMGlobal.UserName;
        }

        private void btnMouseHover(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = gloBilling.Properties.Resources.Img_LongYellow;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                //Blank catch
            }
        }

        private void btnMouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    ((Button)sender).BackgroundImage = gloBilling.Properties.Resources.Img_LongButton;
                    ((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception)
            {
                //Blank catch
            }
        }
        // End.

        private void AllocateUseReserveforCopay()
        {
            if (dCheckAmount > 0)
            {
                txtCheckAmount.Text = dCheckAmount.ToString("#0.00");
            }
            else
                return;
            btnUseReserve.Visible = false;
            btnClearReserve.Visible = true;
            btnReserveRemaining.Visible = false;
            txtCheckAmount.ReadOnly = true;
            txtCheckAmount.BackColor = Color.White;

            btnUseReserve.Tag = SeletedReserveItems;

            tls_btnReceipt.Enabled = false;
            tls_btnDefaultReceipt.Enabled = false;
            _useReserves = true;
            CalculateRemainingAmount();

            lblPayType.Enabled = false;
            cmbPayMode.Enabled = false;
            lblCheckNo.Enabled = false;
            txtCheckNumber.Enabled = false;
         
            lblPatientSearch.Enabled = false;
            lblCheckDate.Enabled = false;
            mskCheckDate.Enabled = false;
            lblCardType.Enabled = false;
            cmbCardType.Enabled = false;
            lblCardAuthorizationNo.Enabled = false;
            txtCardAuthorizationNo.Enabled = false;
            lblExpiryDate.Enabled = false;
            mskCreditExpiryDate.Enabled = false;

            _IsUseReserveEntry = true;
            txtPatientSearch.Enabled = false;
            _useReserves = true;
        }

        private void tsb_ShowHideBadDebtBalance_Click(object sender, EventArgs e)
        {
            if (_IsAllDatesValid == false) { return; }
            //if (oPatientControl != null && oPatientControl.PatientID > 0)
            if (oPatientControl != null)
            {
                if (IsPaymentMade() == true)
                {
                    DialogResult _dlgRst = DialogResult.None;
                    _dlgRst = MessageBox.Show("Do you want to save changes ?", _messageboxcaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (_dlgRst == DialogResult.Yes)
                    {
                        tls_btnSave_Click(null, null);
                    }
                    else if (_dlgRst == DialogResult.No)
                    {
                        if (tsb_ShowHideBadDebtBalance.Tag.ToString().Trim() == "Hide")
                        { SetShowBadDebtBalance(); }
                        else if (tsb_ShowHideBadDebtBalance.Tag.ToString().Trim() == "Show")
                        { SetHideBadDebtBalance(); }

                        RefreshClaimList();
                        CalculateRemainingAmount();
                    }
                }
                else
                {
                    if (tsb_ShowHideBadDebtBalance.Tag.ToString().Trim() == "Hide")
                    { SetShowBadDebtBalance(); }
                    else if (tsb_ShowHideBadDebtBalance.Tag.ToString().Trim() == "Show")
                    { SetHideBadDebtBalance(); }

                    RefreshClaimList();
                    CalculateRemainingAmount();
                }
            }
            AllowEditValidation();
        }
        private void AllocateCleargageAmount()
        {
            string CleargagePaymentMode = string.Empty;
            string AccountType = string.Empty;
            try
            {
                oPatientControl.DisableInputControl = true;
                oPatientControl.DisableChkAllAcctPat = true;
                oPatientControl.IsAllAccPatSelected = false;
             //   oPatientControl_PatientChanged(null, null);
                if (dtCleargagePaymentDetails != null && dtCleargagePaymentDetails.Rows.Count > 0)
                {
                    dCheckAmount = Convert.ToDecimal(dtCleargagePaymentDetails.Rows[0]["Amount"]);
                    if (dCheckAmount > 0)
                    {
                        txtCheckAmount.Text = dCheckAmount.ToString("#0.00");
                        txtCheckRemaining.Text = dCheckAmount.ToString("#0.00");
                    }
                    else
                        return;

                    CleargagePaymentMode = Convert.ToString(dtCleargagePaymentDetails.Rows[0]["PaymentMethod"]);
                    AccountType = Convert.ToString(dtCleargagePaymentDetails.Rows[0]["AccountType"]);

                    switch (CleargagePaymentMode.ToUpper())
                    {
                        case "CREDIT": cmbPayMode.Text = PaymentModeV2.CreditCard.ToString();

                            break;
                        case "CASH": cmbPayMode.Text = PaymentModeV2.Cash.ToString();
                            break;
                        case "ACH": cmbPayMode.Text = PaymentModeV2.Check.ToString();
                            break;
                    }
                    if (CleargagePaymentMode.ToUpper() == PaymentMethod.CREDIT.ToString().ToUpper())
                    {
                        switch (AccountType.ToUpper())
                        {
                            case "VI": cmbCardType.Text = "Visa";
                                break;
                            case "MC": cmbCardType.Text = "Master Card";
                                break;
                            case "AX": cmbCardType.Text = "American Express";
                                break;
                            case "DI": cmbCardType.Text = "Discover";
                                break;
                        }


                        txtCheckNumber.Text = Convert.ToString(dtCleargagePaymentDetails.Rows[0]["AccountNumber"]);
                        txtCardAuthorizationNo.Text = Convert.ToString(dtCleargagePaymentDetails.Rows[0]["ReferenceNumber"]);
                    }
                    else if (Convert.ToString(dtCleargagePaymentDetails.Rows[0]["PaymentMethod"]).ToUpper() == PaymentMethod.CASH.ToString().ToUpper())
                    {
                        txtCheckNumber.Text = "CG_CASH_" + Convert.ToString(dtCleargagePaymentDetails.Rows[0]["ReferenceNumber"]);
                    }
                    else if (Convert.ToString(dtCleargagePaymentDetails.Rows[0]["PaymentMethod"]).ToUpper() == PaymentMethod.ACH.ToString().ToUpper())
                    {
                        txtCheckNumber.Text = "CG_ACH_" + Convert.ToString(dtCleargagePaymentDetails.Rows[0]["ReferenceNumber"]);
                    }
                    if (Convert.ToString(dtCleargagePaymentDetails.Rows[0]["TimeStamp"]) != "")
                    {
                        mskCheckDate.Text=Convert.ToDateTime(dtCleargagePaymentDetails.Rows[0]["TimeStamp"]).ToString("MM/dd/yyyy");
                    }
                    
                    btnUseReserve.Visible = false;
                    txtCheckAmount.ReadOnly = true;
                    txtCheckAmount.BackColor = Color.White;

                    CalculateRemainingAmount();
                    lblPatientSearch.Enabled = false;
                    _IsUseReserveEntry = false;
                    txtPatientSearch.Enabled = false;
                    _useReserves = false;
                    tls_btnNew.Enabled = false;
                    tls_btnNewCorrection.Enabled = false;
                    txtCheckNumber.ReadOnly = true;
                    txtCardAuthorizationNo.ReadOnly = true;
                    cmbCardType.Enabled = false;
                    cmbPayMode.Enabled = false;
                    tls_btnOneTimePayment.Enabled = false;
                    mskCheckDate.ReadOnly = true;
                    tls_btnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            
        }

        #region "Cleargage One Time Payment"
        private void tls_btnOneTimePayment_Click(object sender, EventArgs e)
        {
            try
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.OneTimePaymentBegin, "One Time Payment start", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                ClearGage.SSO.Patient oPatient = GetPatientInfo(_PatientID);
                double amount = Convert.ToDouble(GetAccountDue());
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.View, "One Time Payment open for amount: " + Convert.ToString(amount), _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                string content = ssoHelper.GetOneTimePaymentDialogHtml(oPatient, amount);
                DisplayWebBrowser(content, "One Time Payment");
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.OneTimePaymentEnd, "One Time Payment end", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.OneTimePaymentEnd, "Exception: " + ex.ToString(), _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void DisplayWebBrowser(string content, string sFormName)
        {
            try
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.View, "One Time Payment webform open", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);

                ClearGage.frmCGWebBrowser oWeb = new ClearGage.frmCGWebBrowser(ssoHelper);
                oWeb.Text = sFormName;
                oWeb.Icon = gloBilling.Properties.Resources.OneTimePayment1;
                oWeb.LoadContent(content);
                oWeb.ShowDialog();
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.View, "One Time Payment webform open", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.View, "Exception: " + ex.ToString(), _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        private ClearGage.SSO.Patient GetPatientInfo(long nPatientID)
        {
            ClearGage.SSO.Patient oPat = null;
            DataTable dt = null;
            gloPatient.gloPatient oPatient = null;
            try
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.Generate, "Get Patient information start", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                oPat = new ClearGage.SSO.Patient();
                oPatient = new gloPatient.gloPatient(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                dt = oPatient.GetPatientDemographics(nPatientID);

                if (dt != null && dt.Rows.Count > 0)
                {
                    oPat.FirstName = Convert.ToString(dt.Rows[0]["sFirstName"]);
                    oPat.LastName = Convert.ToString(dt.Rows[0]["sLastName"]);
                    oPat.BirthDate = Convert.ToString(dt.Rows[0]["dtDOB"]);
                    oPat.Gender = Convert.ToString(dt.Rows[0]["sGender"]).Substring(0, 1);
                    oPat.Address1 = Convert.ToString(dt.Rows[0]["sAddressLine1"]);
                    oPat.Address2 = Convert.ToString(dt.Rows[0]["sAddressLine2"]);
                    oPat.City = Convert.ToString(dt.Rows[0]["sCity"]);
                    oPat.State = Convert.ToString(dt.Rows[0]["sState"]);
                    oPat.Zip = Convert.ToString(dt.Rows[0]["sZip"]);
                    oPat.Ssn = Convert.ToString(dt.Rows[0]["nSSN"]);
                    oPat.EmailAddress = Convert.ToString(dt.Rows[0]["sEmail"]);
                    oPat.MobilePhone = Convert.ToString(dt.Rows[0]["sMobile"]);
                    oPat.Phone = Convert.ToString(dt.Rows[0]["sPhone"]);
                    oPat.DriversLicenseNumber = "";
                    oPat.DriversLicenseState = "";
                    oPat.PatientId = Convert.ToString(dt.Rows[0]["sPatientCode"]);
                }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.Generate, "Get Patient information end", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.Generate, "Eception in GetPatientInfo(): " + ex.ToString(), _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return oPat;
        }

        private decimal GetAccountDue()
        {

            decimal dLastPatPayment = 0;
            decimal dTotalBalAmt = 0;
            decimal dTotalInsPending = 0;
            decimal dTotalPatientDue = 0;
            decimal dTotalcopayReserve = 0;
            decimal dTotalAdvancedReserve = 0;
            decimal dTotalOtherReserve = 0;
            decimal dTotalBadDebtDue = 0;
            string sLastPatPaymentDate = "";

            DataSet dtSet = null;
            DataTable dtInsuranceDetails = null;
            DataTable dtReserveDetails = null;
            try
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.Generate, "Get due started", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                dtSet = gloStripControl.PatientStripControl.GetPatientBalances(_PatientID, oPatientControl.PAccountID);

                dtInsuranceDetails = dtSet.Tables[0];
                dtReserveDetails = dtSet.Tables[1];

                if (dtInsuranceDetails != null && dtInsuranceDetails.Rows.Count > 0)
                {
                    //dTotalBalAmt = dtInsuranceDetails.Rows[0]["TotalBalance"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["TotalBalance"]);
                    dTotalInsPending = dtInsuranceDetails.Rows[0]["InsuranceDue"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["InsuranceDue"]);
                    dTotalPatientDue = dtInsuranceDetails.Rows[0]["PatientDue"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["PatientDue"]);
                    dTotalBadDebtDue = dtInsuranceDetails.Rows[0]["BadDebtDue"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["BadDebtDue"]);
                    dLastPatPayment = dtInsuranceDetails.Rows[0]["PatientLastPay"] == DBNull.Value ? 0 : Convert.ToDecimal(dtInsuranceDetails.Rows[0]["PatientLastPay"]);
                    sLastPatPaymentDate = dtInsuranceDetails.Rows[0]["LastPayDate"] == DBNull.Value ? "" : Convert.ToDateTime(dtInsuranceDetails.Rows[0]["LastPayDate"]).ToString("MM/dd/yyyy");
                    if (gloGlobal.gloPMGlobal.IsExternalCollectionfeatureEnabled)
                    {
                        dTotalBalAmt = dTotalInsPending + dTotalPatientDue + dTotalBadDebtDue;
                    }
                    else
                    {
                        dTotalBalAmt = dTotalInsPending + dTotalPatientDue;
                    }
                }
                //Assign Copay Reserve,AdvancedResere,OtherReserve to Varialbles
                if (dtReserveDetails != null && dtReserveDetails.Rows.Count > 0)
                {
                    foreach (DataRow drReserveDetails in dtReserveDetails.Rows)
                    {
                        if (Convert.ToInt16(drReserveDetails["nPaymentNoteSubType"]) == 2)   //For Copay Reserve
                        {
                            dTotalcopayReserve = drReserveDetails["AvailableReserve"] == DBNull.Value ? 0 : Convert.ToDecimal(drReserveDetails["AvailableReserve"]);
                        }

                        if (Convert.ToInt16(drReserveDetails["nPaymentNoteSubType"]) == 3)  //ForAdvanced Reserve
                        {
                            dTotalAdvancedReserve = drReserveDetails["AvailableReserve"] == DBNull.Value ? 0 : Convert.ToDecimal(drReserveDetails["AvailableReserve"]);
                        }
                        if (Convert.ToInt16(drReserveDetails["nPaymentNoteSubType"]) == 4) //For OtherReserve
                        {
                            dTotalOtherReserve = drReserveDetails["AvailableReserve"] == DBNull.Value ? 0 : Convert.ToDecimal(drReserveDetails["AvailableReserve"]);
                        }
                    }
                }
                dTotalBalAmt = dTotalBalAmt - (dTotalcopayReserve + dTotalAdvancedReserve + dTotalOtherReserve);
                dTotalPatientDue = dTotalPatientDue - (dTotalcopayReserve + dTotalAdvancedReserve + dTotalOtherReserve);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.Generate, "Get due end", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.Generate, "Exception in GetAccoutDue(): " + ex.ToString(), _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (dtInsuranceDetails != null)
                    dtInsuranceDetails.Dispose();
                if (dtReserveDetails != null)
                    dtReserveDetails.Dispose();
                if (dtSet != null)
                {
                    dtSet.Dispose();
                    dtSet = null;
                }
            }
            return dTotalPatientDue;
        }

        private void SetClearGageCallbacks(ClearGage.SSO.SsoHelper ssoHelper)
        {
            ssoHelper.OneTimePaymentDialogCallback = new ClearGage.SSO.OneTimePaymentDialogResponseHandler(OneTimePaymentDialogCallback);
        }

        private void OneTimePaymentDialogCallback(ClearGage.SSO.Response.Transaction[] transactions)
        {
            ClsCleargagePaymentPosting oClsCleargagePaymentPosting = null;
            Int64 nCleargageID = 0;
            try
            {
                //ClearGage.SSO.Response.Transaction[] oOTPTransactions = (ClearGage.SSO.Response.Transaction[])obj;
                //Int64 nOTPTransactionID = 0;
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.Generate, "One time payment callback start", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                oClsCleargagePaymentPosting = new ClsCleargagePaymentPosting();

                nCleargageID = oClsCleargagePaymentPosting.SaveCleargageTransaction(transactions[0]);

                if (nCleargageID != -1)
                {
                    nCG_OneTimePaymentID = nCleargageID;
                    //nCleargageID= -1: payment info save on first submit before sign & after sign no update is made & returns -1 as info. already present.
                    //nCleargageID!= -1: payment info save on first submit before sign and loaded in respective field on patient payment.
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.Generate, "One time payment Json save with ID: " + Convert.ToString(nCG_OneTimePaymentID), _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                    SetOTPDataForPatientPayment(nCG_OneTimePaymentID);
                    //c1OneTimePayment.DataSource = null;
                    //c1OneTimePayment.DataSource = oTransactions;
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.Generate, "One time payment callback end", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                    
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.Generate, "Exception in One time payment callback: " + ex.ToString(), _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oClsCleargagePaymentPosting!=null)
                {
                    oClsCleargagePaymentPosting.Dispose();
                    oClsCleargagePaymentPosting = null;
                }
            }
        }

        private void SetOTPDataForPatientPayment(Int64 nOTPTransactionID)
        {
            ClsCleargagePaymentPosting oClsCleargagePaymentPosting = null;
            try
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.Generate, "One time payment set transaction data to form start", _PatientID, nOTPTransactionID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                oClsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
                DataTable dtOTPTransaction = oClsCleargagePaymentPosting.GetCleargageTransaction(nOTPTransactionID);
                if (Convert.ToString(dtOTPTransaction.Rows[0]["PaymentMethod"]).ToUpper() == "CASH")
                {
                    if (Convert.ToString(dtOTPTransaction.Rows[0]["Amount"]) == "" || Convert.ToString(dtOTPTransaction.Rows[0]["PaymentMethod"]) == ""
                    || Convert.ToString(dtOTPTransaction.Rows[0]["AccountType"]) == "" || Convert.ToString(dtOTPTransaction.Rows[0]["Reference"]) == "")
                    {
                        MessageBox.Show("Unable to post payment in QPM due to one of the information(Amount, Pay Method, Card Type, Auth#) is missing from Cleargage.");
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.None, "Unable to post payment in QPM due to one of the information(Amount, Pay Method, Card Type, Card#, Auth#) is missing from Cleargage.", _PatientID, nOTPTransactionID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                        return;
                    }
                }
                else
                {
                    if (Convert.ToString(dtOTPTransaction.Rows[0]["Amount"]) == "" || Convert.ToString(dtOTPTransaction.Rows[0]["PaymentMethod"]) == ""
                        || Convert.ToString(dtOTPTransaction.Rows[0]["AccountType"]) == "" || Convert.ToString(dtOTPTransaction.Rows[0]["AccountNumber"]) == ""
                        || Convert.ToString(dtOTPTransaction.Rows[0]["Reference"]) == "")
                    {
                        MessageBox.Show("Unable to post payment in QPM due to one of the information(Amount, Pay Method, Card Type, Card#, Auth#) is missing from Cleargage.");
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.None, "Unable to post payment in QPM due to one of the information(Amount, Pay Method, Card Type, Card#, Auth#) is missing from Cleargage.", _PatientID, nOTPTransactionID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                        return;
                    }
                }
                if (dtOTPTransaction != null && dtOTPTransaction.Rows.Count > 0)
                {
                    txtCheckAmount.Text = Convert.ToString(dtOTPTransaction.Rows[0]["Amount"]).Replace("-", "");
                    switch (Convert.ToString(dtOTPTransaction.Rows[0]["PaymentMethod"]).ToUpper())
                    {
                        case "CREDIT": cmbPayMode.Text = PaymentModeV2.CreditCard.ToString();
                            break;
                        case "CASH": cmbPayMode.Text = PaymentModeV2.Cash.ToString();
                            break;
                        case "ACH": cmbPayMode.Text = PaymentModeV2.Check.ToString();
                            break;
                    }
                    if (Convert.ToString(dtOTPTransaction.Rows[0]["PaymentMethod"]).ToUpper() == PaymentMethod.CREDIT.ToString().ToUpper())
                    {
                        switch (Convert.ToString(dtOTPTransaction.Rows[0]["AccountType"]).ToUpper())
                        {
                            case "VI": cmbCardType.Text = "Visa";
                                break;
                            case "MC": cmbCardType.Text = "Master Card";
                                break;
                            case "AX": cmbCardType.Text = "American Express";
                                break;
                            case "DI": cmbCardType.Text = "Discover";
                                break;
                        }
                        txtCheckNumber.Text = Convert.ToString(dtOTPTransaction.Rows[0]["AccountNumber"]);
                        txtCardAuthorizationNo.Text = Convert.ToString(dtOTPTransaction.Rows[0]["Reference"]);
                    }
                    else if (Convert.ToString(dtOTPTransaction.Rows[0]["PaymentMethod"]).ToUpper() == PaymentMethod.CASH.ToString().ToUpper())
                    {
                        txtCheckNumber.Text = "CG_CASH_" + Convert.ToString(dtOTPTransaction.Rows[0]["Reference"]);
                    }
                    else if (Convert.ToString(dtOTPTransaction.Rows[0]["PaymentMethod"]).ToUpper() == PaymentMethod.ACH.ToString().ToUpper())
                    {
                        txtCheckNumber.Text = "CG_ACH_" + Convert.ToString(dtOTPTransaction.Rows[0]["Reference"]);
                    }

                    txtCheckAmount_Leave(null, null);
                }
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.Generate, "One time payment set transaction data to form end", _PatientID, nOTPTransactionID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.Generate, "Exception in One time payment set transaction data to form: " + ex.ToString(), _PatientID, nOTPTransactionID, 0, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloPM, true);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oClsCleargagePaymentPosting != null)
                {
                    oClsCleargagePaymentPosting.Dispose();
                    oClsCleargagePaymentPosting = null;
                }
            }
        }

        public bool IsCleargageDataChanged(Int64 nOTPTransactionID, string sCheckAmount)
        {
            bool bIsCleargageDataChanged = false;
            ClsCleargagePaymentPosting oClsCleargagePaymentPosting = null;
            DataTable dtOTPTransaction = null;
            try
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.Generate, "One time payment is data changed check start", _PatientID, nOTPTransactionID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                oClsCleargagePaymentPosting = new ClsCleargagePaymentPosting();
                dtOTPTransaction = oClsCleargagePaymentPosting.GetCleargageTransaction(nOTPTransactionID);
                if (dtOTPTransaction != null && dtOTPTransaction.Rows.Count > 0)
                {
                    string sCardType = string.Empty;
                    string sCheckNumber = string.Empty;
                    string sCardAuthNo=string.Empty;
                    string sPayMode = string.Empty;
                    switch (Convert.ToString(dtOTPTransaction.Rows[0]["PaymentMethod"]).ToUpper())
                    {
                        case "CREDIT": sPayMode = PaymentModeV2.CreditCard.ToString();
                            break;
                        case "CASH": sPayMode = PaymentModeV2.Cash.ToString();
                            break;
                        case "ACH": sPayMode = PaymentModeV2.Check.ToString();
                            break;
                    }
                    if (Convert.ToString(dtOTPTransaction.Rows[0]["PaymentMethod"]).ToUpper() == PaymentMethod.CREDIT.ToString().ToUpper())
                    {
                        switch (Convert.ToString(dtOTPTransaction.Rows[0]["AccountType"]).ToUpper())
                        {
                            case "VI": sCardType = "Visa";
                                break;
                            case "MC": sCardType = "Master Card";
                                break;
                            case "AX": sCardType = "American Express";
                                break;
                            case "DI": sCardType = "Discover";
                                break;
                        }
                        sCheckNumber = Convert.ToString(dtOTPTransaction.Rows[0]["AccountNumber"]);
                        sCardAuthNo = Convert.ToString(dtOTPTransaction.Rows[0]["Reference"]);
                    }
                    else if (Convert.ToString(dtOTPTransaction.Rows[0]["PaymentMethod"]).ToUpper() == PaymentMethod.CASH.ToString().ToUpper())
                    {
                        sCheckNumber = "CG_CASH_" + Convert.ToString(dtOTPTransaction.Rows[0]["Reference"]);
                    }
                    else if (Convert.ToString(dtOTPTransaction.Rows[0]["PaymentMethod"]).ToUpper() == PaymentMethod.ACH.ToString().ToUpper())
                    {
                        sCheckNumber = "CG_ACH_" + Convert.ToString(dtOTPTransaction.Rows[0]["Reference"]);
                    }

                    if (sCheckAmount != Convert.ToString(dtOTPTransaction.Rows[0]["Amount"]))
                    {
                        bIsCleargageDataChanged = true;
                    }
                    else if (cmbPayMode.Text != sPayMode)
                    {
                        bIsCleargageDataChanged = true;
                    }
                    else if (txtCheckNumber.Text!=sCheckNumber)
                    {
                        bIsCleargageDataChanged = true;
                    }
                    else if (sCardType!="")
                    {
                        if (cmbCardType.Text!=sCardType)
                        {
                            bIsCleargageDataChanged = true;
                        }
                        if (txtCardAuthorizationNo.Text!=sCardAuthNo)
                        {
                            bIsCleargageDataChanged = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                bIsCleargageDataChanged = false;
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.Generate, "Exception in IsCleargageDataChanged: " + ex.ToString(), _PatientID, nOTPTransactionID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
            }
            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Cleargage, gloAuditTrail.ActivityCategory.CleargageOneTimePayment, gloAuditTrail.ActivityType.None, "IsCleargageDataChanged: "+Convert.ToString(bIsCleargageDataChanged), _PatientID, nOTPTransactionID, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloPM, true);
            return bIsCleargageDataChanged;
        }
        #endregion
    }
}
