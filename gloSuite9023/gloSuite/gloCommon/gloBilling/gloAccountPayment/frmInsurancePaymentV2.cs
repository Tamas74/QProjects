using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloBilling;
using gloBilling.Payment;
using gloDateMaster;
using gloPatient;
using gloStrips;
using gloSettings;
using System.Collections.Generic;
using gloBilling.Collections;
using gloGlobal;

namespace gloAccountsV2
{
    public partial class frmInsurancePaymentV2 :gloGlobal.Common.TriarqFormWithFocusListner
    {

        #region " Variable Declarations "

        private int PaymentDialogResult = 0;
        decimal _allowedAmountBeforeEdit = 0;
     //   decimal _lastPayment = 0;
        const string _paymentPrefix = "GPM#";
        private string _databaseConnectionString = string.Empty;
        private Int64 nPAccountID = 0;
        private Int64 nGuarantorID = 0;
        private Int64 nAccountPatientID = 0;
        private Int64 nLastTransactionMasterID = 0;
        private Int64 PatientID = 0;
        Int64 nTransactionID = 0;
        private bool _IsPatientAccountFeature = false;
        bool _IsValidEntered = true;
        bool _IsFromOnInsuranceSelected = false;
        bool _IsShowChargeUnit = false;
        ArrayList _innerlist = new ArrayList();
        gloStripControl.ucPatientStripControl PatientControl = null;
        private gloAccountsV2.PaymentCollection.Reserves _ReserveDetails = new PaymentCollection.Reserves();
        gloAccountsV2.PaymentCollection.Credit oCreditLine = new gloAccountsV2.PaymentCollection.Credit();
        gloAccountsV2.PaymentCollection.Credit oCreditResDTL = new PaymentCollection.Credit();
        gloAccountsV2.PaymentCollection.Credit oCreditResDTL_CopyForRule = new PaymentCollection.Credit();
        gloBilling.gloAccountPayment.dsPaymentTVP_V2 dsInsurancePayment_TVP = null;
        string CurrentSelectedInsuranceCompany = "";
        bool _bAllowNegativeWHAmount = false;

        //Added By Shweta 
        private bool _isAssociationSave = false;
        
        #endregion

        #region " Grid Constants "

        const int COL_GENERAL = 0;
        const int COL_PATIENTID = 1;
        const int COL_PATIENTNAME = 2;
        const int COL_CLAIMNO = 3;

        const int COL_BILLING_TRANSACTON_ID = 4; //Billing Transaction ID
        const int COL_BILLING_TRANSACTON_DETAILID = 5; //Billing Transaction Serive Line Detail ID
        const int COL_BILLING_TRANSACTON_LINENO = 6; //Billing Transaction Service Line No

        const int COL_PAYMENT_NO = 7;
        const int COL_PAY_DATE = 8;

        const int COL_PAY_EOBPAYMENTID = 9;//Previous Name : COL_PAY_MULTIPLEPAYMENTID
        const int COL_PAY_EOBID = 10;//Previous Name : COL_PAY_PAYMENTID
        const int COL_PAY_EOBDTLID = 11;//Previous Name : COL_PAY_PAYMENTDETAILID
        const int COL_PAY_EOBPAYMENTDTLID = 12;

        const int COL_CLAIMDISPNO = 13;
        const int COL_DOS_FROM = 14;
        const int COL_DOS_TO = 15;

        const int COL_CPT_CODE = 16;
        
        const int COL_CPT_DESCRIPTON = 17;
        const int COL_CROSSWALK_CPT_CODE = 18;
        const int COL_CROSSWALK_CPT_DESC = 19;
        const int COL_MODIFIER = 20;
        const int COL_UNIT = 21;
        const int COL_CHARGE = 22;
        
        const int COL_TOTALCHARGE = 23;
        const int COL_ALLOWED = 24;
        const int COL_PAYMENT = 25;

        const int COL_WRITEOFF = 26;

        const int COL_COPAY = 27;
        const int COL_DEDUCTIBLE = 28;
        const int COL_COINSURANCE = 29;
        const int COL_WITHHOLD = 30;

        const int COL_LAST_ALLOWED = 31;
        const int COL_LAST_PAYMENT = 32;
        const int COL_LAST_WRITEOFF = 33;
        const int COL_LAST_COPAY = 34;
        const int COL_LAST_DEDUCTIBLE = 35;
        const int COL_LAST_COINSURANCE = 36;
        const int COL_LAST_WITHHOLD = 37;

        const int COL_PREVPAID = 38;
        const int COL_BALANCE = 39;

        const int COL_ISCORRECTION = 40;

        const int COL_NEXT = 41;
        const int COL_PARTY = 42;
        const int COL_REASON = 43;

        const int COL_SERVICELINE_TYPE = 44;
        const int COL_ISOPENFORMODIFY = 45;
        const int COL_PAY_CLINICID = 46;
        const int COL_PAY_LINESTATUS = 47;

        const int COL_CELLRANGE_R1 = 48;
        const int COL_CELLRANGE_R2 = 49;

        const int COL_LINE_STATEMENTNOTE = 50;
        const int COL_LINE_STATEMENTNOTEONPRINT = 51;
        const int COL_LINE_INTERNALNOTE = 52;
        const int COL_LINE_INTERNALNOTEONPRINT = 53;

        const int COL_LINE_DB_BALANCE = 54;

        const int COL_SUBCLAIMNO = 55;
        const int COL_TRACK_BILLING_TRANSACTON_ID = 56; //Billing Transaction ID from Tracking Table
        const int COL_TRACK_BILLING_TRANSACTON_DETAILID = 57; //Billing Transaction Serive Line Detail ID from Tracking Table
        const int COL_TRACK_BILLING_TRANSACTON_LINENO = 58; //Billing Transaction Service Line No from Tracking Table

        const int COL_ISSPLITTED = 59;
        const int COL_LASTEOBPAYMENTID = 62;
        const int COL_PAYMENTTYPE = 60;
        const int COL_PAYMENTSUBTYPE = 61;
        const int COL_PatientPaidAmount = 63;
        const int COL_COUNT = 64;
        

        #endregion

        #region Payment Grid Enum

        enum PaymentGrid
        {
            COL_GENERAL,
            COL_PATIENTID,
            COL_PATIENTNAME,
            COL_CLAIMNO,
            COL_BILLING_TRANSACTON_ID,
            COL_BILLING_TRANSACTON_DETAILID,
            COL_BILLING_TRANSACTON_LINENO,
            COL_PAYMENT_NO,
            COL_PAY_DATE,
            COL_PAY_EOBPAYMENTID,
            COL_PAY_EOBID,
            COL_PAY_EOBDTLID,
            COL_PAY_EOBPAYMENTDTLID,
            COL_CLAIMDISPNO,
            COL_DOS_FROM,
            COL_DOS_TO,
            COL_CPT_CODE,
            COL_CPT_DESCRIPTON,
            COL_CROSSWALK_CPT_CODE,
            COL_CROSSWALK_CPT_DESC,
            COL_MODIFIER,
            COL_CHARGE,
            COL_UNIT,
            COL_TOTALCHARGE,
            COL_ALLOWED,
            COL_PAYMENT,
            COL_WRITEOFF,
            COL_COPAY,
            COL_DEDUCTIBLE,
            COL_COINSURANCE,
            COL_WITHHOLD,
            COL_LAST_ALLOWED,
            COL_LAST_PAYMENT,
            COL_LAST_WRITEOFF,
            COL_LAST_COPAY,
            COL_LAST_DEDUCTIBLE,
            COL_LAST_COINSURANCE,
            COL_LAST_WITHHOLD,
            COL_PREVPAID,
            COL_BALANCE,
            COL_ISCORRECTION,
            COL_NEXT,
            COL_PARTY,
            COL_REASON,
            COL_SERVICELINE_TYPE,
            COL_ISOPENFORMODIFY,
            COL_PAY_CLINICID,
            COL_PAY_LINESTATUS,
            COL_CELLRANGE_R1,
            COL_CELLRANGE_R2,
            COL_LINE_STATEMENTNOTE,
            COL_LINE_STATEMENTNOTEONPRINT,
            COL_LINE_INTERNALNOTE,
            COL_LINE_INTERNALNOTEONPRINT,
            COL_LINE_DB_BALANCE,
            COL_SUBCLAIMNO,
            COL_TRACK_BILLING_TRANSACTON_ID,
            COL_TRACK_BILLING_TRANSACTON_DETAILID,
            COL_TRACK_BILLING_TRANSACTON_LINENO,
            COL_ISSPLITTED,
            COL_PAYMENTTYPE,
            COL_PAYMENTSUBTYPE,
            COL_LASTEOBPAYMENTID
            
        }

        #endregion

        #region " Claim Details Constants "

        const int COL_CLM_FILEDDATE = 0;
        const int COL_CLM_REFILEDDATE = 1;
        const int COL_CLM_INSURANCENAME = 2;
        const int COL_CLM_FILEDAMOUNT = 3;

        const int COL_CLM_COUNT = 4;

        #endregion " Claim Details Constants "

        #region " Enums "

        private enum CorrTBActionPerformed
        {
            Edit,
            Delete
        }

        private enum FormMode
        {
            NewPaymentMode,
            CorrectionMode
        }
            
        private enum ColServiceLineType
        {
            None = 0, Claim = 1, ServiceLine = 2, Patient = 3
        }

        #endregion

        #region " Delegates & Events "

        public delegate void CalculationChanged();
        public event CalculationChanged OnRemainingCalculationChanged;
       // public event CalculationChanged OnRemainingCalculationChangedCorrTB;
        public delegate void AdjustmetnalculationChanged(string ReasonCodeType, decimal Payment);
        public event AdjustmetnalculationChanged onAdjustmetnalculationChanged;
     
        #endregion

        #region " Property Procedures "

        private Int64 _EOBPaymentID = 0;
        private Int64 _LastEOBPaymentID = 0;
        private Int64 _insuranceCompanyID = 0;
        private Int64 _contactInsuranceID = 0;
        private Int64 _patientInsuranceID = 0;
        private Int64 _SelectedTrayID = 0;

        private string _insuranceCompanyName = string.Empty;
        private string _insurancePlanName = string.Empty;
        private string _ReserveNote = string.Empty;
        private string _SelectedTrayName = string.Empty;
        private string _SelectedTrayCode = string.Empty;
        private string _PaymentCloseDate = string.Empty;
        private string _NewOpenDate = string.Empty;
        private string _OriginalCloseDate = string.Empty;
        private string _RevCycleClaimNo = string.Empty;
        private string _VoidAndReplaceClaimNo = string.Empty;

        private bool _IsSelectedPlanOnHold = false;
        private bool _IsReserveAdded = false;
    //    private bool _IsFromReserveRemaining = false;
        private bool _IsReserveUsed = false;
      //  private bool _IsTakeback = true;
        private bool _useReserves = false;
        private bool _ResetCheckAmount;
        private bool _isOriginalPayment = false;
        private bool _IsPendingCheckLoaded = false;
        private bool _IsCorrectionGridDesigned = false;
        private bool _IsMultiPaymentGridDesigned = false;
        private bool _IsMultiPaymentTotalGridDesigned = false;
        private bool _IsOpenFromRevenueCycle = false;
        private bool _IsOpenFromVoidAndReplace = false;

        private decimal _AmountTakenFromReserve = 0;
        private decimal _ResAmtFrmSameCheck = 0;
    //    private decimal _ResAmtFrmDiffCheck = 0;
        private decimal _AmountAddedToReserve = 0;
     //   private decimal _AmountAddedTooldReserve = 0;
        private decimal _AmountTakenFromoldReserve = 0;
        private decimal _CheckAmount = 0;
        private decimal _TakeBackAmount = 0;
        private decimal _TotalFundsAvaiable = 0;
        private decimal _AmountApplied = 0;
        private decimal _TotalFundsRemaining = 0;
     //   private decimal _TotalFundsRemainingOld = 0;
        private decimal _ReservesApplied = 0;
        private decimal _OriginalCheckAmount = 0;
        private decimal _TotalAllocation = 0;
   //     private decimal _AmountAddedToReserveCurrent = 0;
    //    private decimal _CurrentTakeBack = 0;
    //    private decimal _TakeBack = 0;

   //     private bool _isPendingCheckLoading = false;

        private gloGeneralItem.gloItems _oSeletedReserveItems = new gloGeneralItem.gloItems();
        private FormMode _formAction = FormMode.NewPaymentMode;
        private PaymentModeV2 _SelectedPaymentModeV2 = PaymentModeV2.None;
        private SplitClaimDetails _ClaimDetails = new SplitClaimDetails();

        public bool IsOpenFromRevenueCycle
        {
            get { return _IsOpenFromRevenueCycle; }
            set { _IsOpenFromRevenueCycle = value; }
        }
        public bool  IsOpenfromModifyCharges { get; set; }
        public string VoidAndReplaceClaimNo
        {
            get { return _VoidAndReplaceClaimNo; }
            set { _VoidAndReplaceClaimNo = value; }
        }
        public bool IsOpenFromVoidAndReplace
        {
            get { return _IsOpenFromVoidAndReplace; }
            set { _IsOpenFromVoidAndReplace = value; }
        }

        public string RevCycleClaimNo
        {
            get { return _RevCycleClaimNo; }
            set { _RevCycleClaimNo = value; }
        }
        public string ModifyChargesClaimNo { get; set; }
        public bool IsPendingCheckLoaded
        {
            get { return _IsPendingCheckLoaded; }
            set { _IsPendingCheckLoaded = value; }
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

        private Int64 EOBPaymentID
        {
            get { return _EOBPaymentID; }
            set { _EOBPaymentID = value; }
        }

        public bool ResetCheckAmount
        {
            get { return _ResetCheckAmount; }
            set
            {
                _ResetCheckAmount = value;
                if (_ResetCheckAmount)
                {
                    txtCheckAmount.Text = string.Empty;
                    _CheckAmount = 0;
                }
            }
        }

        private decimal CheckAmount
        {
            get
            {
                if (txtCheckAmount.Text != "")
                {
                    try
                    { _CheckAmount = Convert.ToDecimal(txtCheckAmount.Text); }
                    catch
                    {
                        _CheckAmount = 0;
                    }

                }

                return _CheckAmount;
            }
            set
            {
                _CheckAmount = value;
                OnRemainingCalculationChanged();
                if (txtCheckAmount.Text != "")
                {
                    txtCheckAmount.Text = _CheckAmount.ToString("#0.00");
                }
                else if (IsPendingCheckLoaded == true)
                {
                    txtCheckAmount.Text = _CheckAmount.ToString("#0.00");
                }
                //
            }
        }

        private decimal TakeBackAmount
        {
            get
            {
                if (lblTakeBackAmt.Text != "")
                { _TakeBackAmount = Convert.ToDecimal(lblTakeBackAmt.Text); }

                return _TakeBackAmount;
            }
            set
            {
                _TakeBackAmount = value;

                if (_TakeBackAmount.Equals(0))
                { lblTakeBackAmt.Text = string.Empty; }
                else
                { lblTakeBackAmt.Text = _TakeBackAmount.ToString("#0.00"); }
            }
        }

        private decimal TotalFundsAvaiable
        {
            get
            { return _TotalFundsAvaiable; }
            set
            {
                _TotalFundsAvaiable = value;

                if (_TotalFundsAvaiable.Equals(0))
                { lblTotalFunds.Text = string.Empty; }
                else
                { lblTotalFunds.Text = _TotalFundsAvaiable.ToString("#0.00"); }
            }
        }

        private decimal AmountApplied
        {
            get
            { return _AmountApplied; }
            set
            { _AmountApplied = value; }
        }

        private decimal ReservesApplied
        {
            get
            { return _ReservesApplied; }
            set
            { _ReservesApplied = value; }
        }

        private decimal TotalFundsRemaining
        {
            get
            { return _TotalFundsRemaining; }
            set
            {
                _TotalFundsRemaining = value;
                txtCheckRemaining.Text = _TotalFundsRemaining.ToString("#0.00");
            }
        }

        private FormMode PaymentAction
        {
            get { return _formAction; }
            set { _formAction = value; }
        }

        private bool IsClaimLoaded
        {
            get
            {
                if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                { return true; }
                else
                { return false; }
            }
        }

        private bool IsPaymentAllocated
        {
            get
            {
                bool _IsPaymentAllocated = false;
                c1SinglePayment.FinishEditing();
                c1SinglePaymentCorrTB.FinishEditing();

                if (PaymentAction == FormMode.NewPaymentMode)
                {
                    if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                    {
                        for (int rIndex = 1; rIndex < c1SinglePayment.Rows.Count; rIndex++)
                        {
                            if (c1SinglePayment.GetData(rIndex, COL_PAYMENT) != null
                               && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_PAYMENT)).Trim() != string.Empty)
                            {
                                _IsPaymentAllocated = true;
                                break;
                            }
                        }
                    }

                }
                else
                {

                    if (c1SinglePaymentCorrTB != null && c1SinglePaymentCorrTB.Rows.Count > 1)
                    {
                        for (int rIndex = 1; rIndex < c1SinglePaymentCorrTB.Rows.Count; rIndex++)
                        {
                            if (c1SinglePaymentCorrTB.GetData(rIndex, COL_PAYMENT) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rIndex, COL_PAYMENT)).Trim() != string.Empty)
                            {
                                _IsPaymentAllocated = true;
                                break;
                            }



                            //if (
                            //   (c1SinglePaymentCorrTB.GetData(rIndex, COL_ALLOWED) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rIndex, COL_ALLOWED)).Trim() != string.Empty)
                            //    || (c1SinglePaymentCorrTB.GetData(rIndex, COL_PAYMENT) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rIndex, COL_PAYMENT)).Trim() != string.Empty)
                            //    || (c1SinglePaymentCorrTB.GetData(rIndex, COL_WRITEOFF) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rIndex, COL_WRITEOFF)).Trim() != string.Empty)
                            //    || (c1SinglePaymentCorrTB.GetData(rIndex, COL_COPAY) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rIndex, COL_COPAY)).Trim() != string.Empty)
                            //    || (c1SinglePaymentCorrTB.GetData(rIndex, COL_DEDUCTIBLE) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rIndex, COL_DEDUCTIBLE)).Trim() != string.Empty)
                            //    || (c1SinglePaymentCorrTB.GetData(rIndex, COL_COINSURANCE) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rIndex, COL_COINSURANCE)).Trim() != string.Empty)
                            //    || (c1SinglePaymentCorrTB.GetData(rIndex, COL_WITHHOLD) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rIndex, COL_WITHHOLD)).Trim() != string.Empty)
                            //   )
                            //{
                            //    _IsPaymentAllocated = true;
                            //    break;
                            //}
                        }  
                    }
                }

                return _IsPaymentAllocated;
            }
        }

        private bool IsCorrectionDone
        {
            get
            {
                bool _IsCorrectionDone = false;
                c1SinglePayment.FinishEditing();
                c1SinglePaymentCorrTB.FinishEditing();

                if (PaymentAction == FormMode.CorrectionMode)
                {
                    if (c1SinglePaymentCorrTB != null && c1SinglePaymentCorrTB.Rows.Count > 1)
                    {
                        for (int rIndex = 1; rIndex < c1SinglePaymentCorrTB.Rows.Count; rIndex++)
                        {
                            if (
                               (c1SinglePaymentCorrTB.GetData(rIndex, COL_ALLOWED) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rIndex, COL_ALLOWED)).Trim() != string.Empty)
                                || (c1SinglePaymentCorrTB.GetData(rIndex, COL_PAYMENT) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rIndex, COL_PAYMENT)).Trim() != string.Empty)
                                || (c1SinglePaymentCorrTB.GetData(rIndex, COL_WRITEOFF) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rIndex, COL_WRITEOFF)).Trim() != string.Empty)
                                || (c1SinglePaymentCorrTB.GetData(rIndex, COL_COPAY) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rIndex, COL_COPAY)).Trim() != string.Empty)
                                || (c1SinglePaymentCorrTB.GetData(rIndex, COL_DEDUCTIBLE) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rIndex, COL_DEDUCTIBLE)).Trim() != string.Empty)
                                || (c1SinglePaymentCorrTB.GetData(rIndex, COL_COINSURANCE) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rIndex, COL_COINSURANCE)).Trim() != string.Empty)
                                || (c1SinglePaymentCorrTB.GetData(rIndex, COL_WITHHOLD) != null && Convert.ToString(c1SinglePaymentCorrTB.GetData(rIndex, COL_WITHHOLD)).Trim() != string.Empty)
                               )
                            {
                                _IsCorrectionDone = true;
                                break;
                            }
                        }
                    }
                }

                return _IsCorrectionDone;
            }
        }

        private bool IsNextActionUpdated
        {
            get
            {
                bool _IsNextActionUpdated = false;

                //.. Check if only next action OR party has to be updated
                if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                {
                    for (int rIndex = 1; rIndex < c1SinglePayment.Rows.Count; rIndex++)
                    {
                        if (c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                        {
                            if ((c1SinglePayment.GetData(rIndex, COL_NEXT) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_NEXT)).Trim() != string.Empty)
                            || (c1SinglePayment.GetData(rIndex, COL_PARTY) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_PARTY)).Trim() != string.Empty))
                            {
                                _IsNextActionUpdated = true;
                                break;
                            }
                        }
                    }
                }

                return _IsNextActionUpdated;
            }
        }

        private PaymentModeV2 SelectedPaymentMode
        {
            set { _SelectedPaymentModeV2 = value; }
            get
            {
                return _SelectedPaymentModeV2;
            }
        }
        private string ClaimRemittanceReferenceNo
        {
            get { return txtClaimRemittanceRef.Text; }
            set { txtClaimRemittanceRef.Text = value; }
        }

        private bool ShowAlertMessage
        {
            get
            {
                return lblAlertMessage.Visible;
            }
            set
            {
                lblAlertMessage.Visible = value;
                if (!lblAlertMessage.Visible)
                { lblAlertMessage.Text = string.Empty; }
            }
        }

        private SplitClaimDetails ClaimDetails
        {
            get { return _ClaimDetails; }
            set
            {
                _ClaimDetails = value;

                txtClaimNo.Text = string.Empty;
                if (_ClaimDetails.IsClaimExist)
                { txtClaimNo.Text = _ClaimDetails.ClaimDisplayNo; }
            }
        }

        public gloAccountsV2.PaymentCollection.Reserves ReserveDetails
        {

            get { return _ReserveDetails; }
            set { _ReserveDetails = value; }

        }

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;
        //        return cp;
        //    }
        //}

        //protected override void OnPaintBackground(PaintEventArgs pevent)
        //{
        //    //do not allow the background to be painted 
        //}

        //protected void InvalidateEx()
        //{
        //    if (Parent == null)
        //        return;

        //    Rectangle rc = new Rectangle(this.Location, this.Size);
        //    Parent.Invalidate(rc, true);
        //}

        #region " Insurance Plan & Insurance Company"

        private Int64 SelectedInsuranceCompanyID
        {
            get { return _insuranceCompanyID; }
            set
            {
                _insuranceCompanyID = value;
                lblInsCompany.Tag = _insuranceCompanyID;
                if (SelectedInsuranceCompanyID == 0)
                { SelectedInsuranceCompany = string.Empty; }
            }
        }

        private string SelectedInsuranceCompany
        {
            get { return _insuranceCompanyName; }
            set
            {
                _insuranceCompanyName = value;
                lblInsCompany.Text = _insuranceCompanyName;
            }
        }

        private string SelectedInsurancePlan
        {
            get { return _insurancePlanName; }
            set { _insurancePlanName = value; }
        }

        /// <summary>
        /// Note : 
        /// 1) This property should be set before ContactInsuranceID
        /// </summary>
        private bool IsSelectedPlanOnHold
        {
            get { return _IsSelectedPlanOnHold; }
            set { _IsSelectedPlanOnHold = value; }
        }

        /// <summary>
        /// Notes : 
        /// 1) This property should be set after IsSelectedPlanOnHold
        /// 2) This property has to be set every time when insurance plan changed as these are required to set the correction mode.
        /// </summary>
        private Int64 ContactInsuranceID
        {
            get { return _contactInsuranceID; }
            set
            {
                _contactInsuranceID = value;
                SetCPTCrossWalk();
                SetAlertMessage();
            }
        }

        /// <summary>
        /// Notes :
        /// 1) This property has to be set every time when insurance plan changed as these are required to set the correction mode.
        /// </summary>
        private Int64 PatientInsuranceID
        {
            get { return _patientInsuranceID; }
            set { _patientInsuranceID = value; }
        }

        #endregion

        #region " Reserve Used / Reserve Remaining "

        private bool IsReserveAdded
        {
            get { return _IsReserveAdded; }
            set
            {
                _IsReserveAdded = value;

                if (!_IsReserveAdded)
                {
                    _AmountAddedToReserve = 0;
                    _ReserveNote = string.Empty;
                }
            }
        }

        private decimal ResAmtFrmSameCheck
        {
            get { return _ResAmtFrmSameCheck; }
            set
            {
                _ResAmtFrmSameCheck = value;
            }
        }

        private bool IsReserveUsed
        {
            get { return _IsReserveUsed; }
            set
            {
                _IsReserveUsed = value;
            }
        }

        private decimal AmountTakenFromReserve
        {
            get { return _AmountTakenFromReserve; }
            set
            {
                _AmountTakenFromReserve = value;

                if (_AmountTakenFromReserve >= 0)
                {
                    //IsReserveUsed = true;
                    if (_AmountTakenFromReserve == 0) { lblReserveAmount.Text = string.Empty; }
                    else { lblReserveAmount.Text = _AmountTakenFromReserve.ToString("#0.00"); }
                }
                else
                {
                    //IsReserveUsed = false;
                    lblReserveAmount.Text = string.Empty;
                }
                //OnRemainingCalculationChanged();
            }
        }

        private decimal AmountAddedToReserve
        {
            get { return _AmountAddedToReserve; }
            set
            {
                _AmountAddedToReserve = value;

                if (_AmountAddedToReserve != 0)
                {
                    IsReserveAdded = true;
                }
                else
                {
                    IsReserveAdded = false;
                    ReserveNote = string.Empty;
                }
                OnRemainingCalculationChanged();

                lblReserveRemaining.Text = _AmountAddedToReserve.ToString("#.00");
                lblReserveRemaining.Tag = _AmountAddedToReserve;
            }
        }

        private gloGeneralItem.gloItems SeletedReserveItems
        {
            get { return _oSeletedReserveItems; }
            set { _oSeletedReserveItems = value; }
        }

        private string ReserveNote
        {
            get { return _ReserveNote; }
            set { _ReserveNote = value; }
        }
        #endregion

        #region " Payment Tray & Close date "

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

        private bool IsCloseDaySet
        {
            get { return mskCloseDate.MaskCompleted; }
        }

        private bool IsCorrectionGridDesigned
        {
            get { return _IsCorrectionGridDesigned; }
            set
            {
                _IsCorrectionGridDesigned = value;
            }
        }

        private bool IsMultiPaymentGridDesigned
        {
            get { return _IsMultiPaymentGridDesigned; }
            set
            {
                _IsMultiPaymentGridDesigned = value;
            }
        }

        private bool IsMultiPaymentTotalGridDesigned
        {
            get { return _IsMultiPaymentTotalGridDesigned; }
            set
            {
                _IsMultiPaymentTotalGridDesigned = value;
            }
        }

        #endregion

        #endregion

        
        #region " Constructors "

        public frmInsurancePaymentV2()
        {
            InitializeComponent();
        }

        public frmInsurancePaymentV2(string _databaseConnectionString)
        {
            InitializeComponent();
            this._databaseConnectionString = _databaseConnectionString;
        }

        #endregion

        #region " Form Events & Methods "

        private void frmInsurancePayment_Load(object sender, EventArgs e)
        {
            AddGotFocusListener(this);
            gloAccount objAccount = null;
            Cls_TabIndexSettings tabSettings = null;
            try
            {
                //InvalidateEx();
                OnRemainingCalculationChanged += new CalculationChanged(frmInsurancePayment_OnRemainingCalculationChanged);
                onAdjustmetnalculationChanged += new AdjustmetnalculationChanged(SelectReasonCodes);
                // Get Patient Account Feature is Enable or Disable
                objAccount = new gloAccount(_databaseConnectionString);
                _IsPatientAccountFeature = objAccount.GetPatientAccountFeatureSetting();

                this.c1SinglePayment.EnterCell -= new System.EventHandler(this.c1SinglePayment_EnterCell);
                this.c1SinglePaymentCorrTB.EnterCell -= new System.EventHandler(this.c1SinglePayment_EnterCell);

                LoadFormData();
                
                tabSettings = new Cls_TabIndexSettings(this);
                tabSettings.SetTabOrder(Cls_TabIndexSettings.TabScheme.AcrossFirst);

                this.c1SinglePayment.EnterCell += new System.EventHandler(this.c1SinglePayment_EnterCell);
                this.c1SinglePaymentCorrTB.EnterCell += new System.EventHandler(this.c1SinglePaymentCorrTB_EnterCell);

                if (_IsOpenFromRevenueCycle)
                {

                    if (PatientControl != null)
                    {
                        if (IsOpenFromRevenueCycle)
                        {
                            if (RevCycleClaimNo.Trim() != string.Empty)
                            {
                                if (RevCycleClaimNo.Trim().Contains("-"))
                                {
                                    PatientControl.ClaimNumber = Convert.ToInt64(RevCycleClaimNo.Trim().Substring(0, RevCycleClaimNo.Trim().IndexOf("-")));
                                    PatientControl.SubClaimNumber = RevCycleClaimNo.Trim().Substring(RevCycleClaimNo.Trim().IndexOf("-") + 1);
                                    LoadClaim();
                                }
                                else
                                {
                                    PatientControl.ClaimNumber = Convert.ToInt64(RevCycleClaimNo.Trim());
                                    PatientControl.SubClaimNumber = "";
                                    LoadClaim();
                                }
                                PatientControl.ClaimFieldText = RevCycleClaimNo.Trim();
                            }
                        }

                    }
                }
                if (IsOpenFromVoidAndReplace)
                {

                    if (PatientControl != null)
                    {
                        if (IsOpenFromVoidAndReplace)
                        {
                            if (VoidAndReplaceClaimNo.Trim() != string.Empty)
                            {
                                if (VoidAndReplaceClaimNo.Trim().Contains("-"))
                                {
                                    PatientControl.ClaimNumber = Convert.ToInt64(VoidAndReplaceClaimNo.Trim().Substring(0, VoidAndReplaceClaimNo.Trim().IndexOf("-")));
                                    PatientControl.SubClaimNumber = VoidAndReplaceClaimNo.Trim().Substring(VoidAndReplaceClaimNo.Trim().IndexOf("-") + 1);
                                    LoadClaim();
                                }
                                else
                                {
                                    PatientControl.ClaimNumber = Convert.ToInt64(VoidAndReplaceClaimNo.Trim());
                                    PatientControl.SubClaimNumber = "";
                                    LoadClaim();
                                }
                                PatientControl.ClaimFieldText = VoidAndReplaceClaimNo.Trim();
                                tls_btnCharge.Enabled = false;
                            }
                        }

                    }
                }

                if (IsOpenfromModifyCharges)
                {

                    if (PatientControl != null)
                    {
                        if (IsOpenfromModifyCharges)
                        {
                            if (ModifyChargesClaimNo.Trim() != string.Empty)
                            {
                                if (ModifyChargesClaimNo.Trim().Contains("-"))
                                {
                                    PatientControl.ClaimNumber = Convert.ToInt64(ModifyChargesClaimNo.Trim().Substring(0, ModifyChargesClaimNo.Trim().IndexOf("-")));
                                    PatientControl.SubClaimNumber = ModifyChargesClaimNo.Trim().Substring(ModifyChargesClaimNo.Trim().IndexOf("-") + 1);
                                    LoadClaim();
                                }
                                else
                                {
                                    PatientControl.ClaimNumber = Convert.ToInt64(ModifyChargesClaimNo.Trim());
                                    PatientControl.SubClaimNumber = "";
                                    LoadClaim();
                                }
                                PatientControl.ClaimFieldText = ModifyChargesClaimNo.Trim();
                                tls_btnCharge.Enabled = false;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (objAccount != null) { objAccount.Dispose(); }
                if (tabSettings != null) { tabSettings = null; }
            }
           
        }
              
        private void frmInsurancePayment_OnRemainingCalculationChanged()
        {
            decimal _payment = 0;
            decimal _oldPayment = 0;
            DataTable _dtCheckBalance = new DataTable();
            //RowColEventArgs _args = null;

            try
            {

                #region " Calculate last payment made "

                decimal _lastPayment = 0;
                decimal _lastTakeBackAmount = 0;
                decimal _CurrentTranctionCheckAllocation = 0;
                decimal _ReservesFromSameCheck = 0;
                Int64 _nBillingTransactionID = 0;
                decimal _CorrectionAmount = 0;
                _dtCheckBalance = GetLastPaymentMade();

                #endregion

                #region "Calculate Amount applied & Take back "
                _TakeBackAmount = 0;
                AmountApplied = 0;
                if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 0)
                {
                    for (int i = 1; i <= c1SinglePayment.Rows.Count - 1; i++)
                    {
                        if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                        {
                            if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && c1SinglePayment.GetData(i, COL_PAYMENT).ToString().Trim() != "")
                            {
                                bool _isCorrection = Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISCORRECTION));
                                _LastEOBPaymentID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_LASTEOBPAYMENTID));
                                _nBillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                if (_isCorrection)
                                {
                                    _payment = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT));
                                    _oldPayment = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT));

                                    if (_oldPayment < _payment)
                                    { _AmountApplied += _payment - _oldPayment; }
                                    else
                                    { _TakeBackAmount = _TakeBackAmount + (_oldPayment - _payment); }
                                }
                                else
                                { _AmountApplied += Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)); }
                            }
                            // Update the payment grid calculations (c1SinglePayment & c1MultiplePayment)
                            CalculateRunningBalance(i);
                        }
                    }
                }

                #endregion

                #region " Calculate Reserve Used (during last payments made)"

                decimal amt = 0;
                if (this.oCreditResDTL != null && this.oCreditResDTL.EOBCreditDTL.Count > 0)
                {
                    for (int index = 0; index < oCreditResDTL.EOBCreditDTL.Count; index++)
                    {
                        amt += oCreditResDTL.EOBCreditDTL[index].Amount; 
                    }
                }
                AmountTakenFromReserve = amt;

                #endregion

                #region "CheckBalances In case of no payment grid loaded"
                if (_dtCheckBalance != null && _dtCheckBalance.Rows.Count > 0)
                {
                    _lastPayment = Convert.ToDecimal(_dtCheckBalance.Rows[0]["LastPayment"].ToString());
                    _lastTakeBackAmount = Convert.ToDecimal(_dtCheckBalance.Rows[0]["TakebackFromOtherCheck"].ToString());
                    _AmountTakenFromoldReserve = Convert.ToDecimal(_dtCheckBalance.Rows[0]["UsedReservesFromSameCheck"].ToString()) + Convert.ToDecimal(_dtCheckBalance.Rows[0]["UsedReservesFromOtherCheck"].ToString());
                    _ReservesFromSameCheck = Convert.ToDecimal(_dtCheckBalance.Rows[0]["UsedReservesFromSameCheck"].ToString());
                }
                #endregion "CheckBalances In case of no payment grid loaded"

                //.. _CurrentTranctionCheckAllocation will be calculated for current grid transactions allocation
                //.. _IsFromOnInsuranceSelected used for triggering GetLastPaymentMade() method for considering database takebacks and last payment

                _CurrentTranctionCheckAllocation = gloInsurancePaymentV2.GetCurrentTransactionAllocation(_nBillingTransactionID, _EOBPaymentID);
                if (_CurrentTranctionCheckAllocation > 0 && !_IsFromOnInsuranceSelected)
                {
                    if (_TakeBackAmount >= _CurrentTranctionCheckAllocation && _LastEOBPaymentID != _EOBPaymentID)
                    {
                        TakeBackAmount = (_TakeBackAmount - _CurrentTranctionCheckAllocation);
                        _CorrectionAmount = _CurrentTranctionCheckAllocation;
                    }
                    else if (_LastEOBPaymentID == _EOBPaymentID && !IsPendingCheckLoaded)
                    {
                        TakeBackAmount = 0;
                        _CorrectionAmount = 0;
                    }
                    else
                    {
                        TakeBackAmount = (_TakeBackAmount);
                        _CorrectionAmount = 0;
                    }
                }
                else
                {
                    _TakeBackAmount = _lastTakeBackAmount + _TakeBackAmount;
                    TakeBackAmount = _TakeBackAmount;
                }

                // calculate available & remaining amount 
                TotalFundsAvaiable = _CheckAmount + (_TakeBackAmount) + _AmountTakenFromReserve;
                TotalFundsRemaining = ((_TotalFundsAvaiable - (_AmountApplied + _lastPayment) - _AmountAddedToReserve) + _CorrectionAmount) - _ReservesFromSameCheck;
                //TotalFundsRemaining = _TotalFundsAvaiable - (_AmountApplied + _lastPayment + _AmountAddedToReserve + _CorrectionAmount + _AmountTakenFromoldReserve);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                
            }
        }

        public DataTable GetLastPaymentMade()
        {
            DataTable _dtCheckBalance = new DataTable();
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();


            try
            {
                if (EOBPaymentID != 0)
                {
                    //_IsFromSave = true;
                    oParameters.Add("@nPaymentID", EOBPaymentID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Connect(false);
                    oDB.Retrive("BL_GetInsurancePaymentCheckBalance_V2", oParameters, out _dtCheckBalance);
                    oDB.Disconnect();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }
                if (oParameters != null)
                {
                    oParameters.Dispose();
                    oParameters = null;
                }

            }
            return _dtCheckBalance;
        }

  
        private void LoadFormData()
        {
            GeneralSettings oSettings=null;
               
            try
            {
                object oValue = null;
                bool SettingsValue = false;
                oSettings = new GeneralSettings(gloPMGlobal.DatabaseConnectionString);
                oSettings.GetSetting("IPShowChargeUnit", 0, gloPMGlobal.ClinicID, out oValue);
                if (Convert.ToString(oValue).ToLower().Trim() == "True".ToLower() || Convert.ToString(oValue).ToLower().Trim() == "False".ToLower())
                {
                    _IsShowChargeUnit = Convert.ToBoolean(oValue);
                }
                oValue = null;
                oSettings.GetSetting("bAllowNegativeWithHoldAmount", 0, gloPMGlobal.ClinicID, out oValue);
                if (Convert.ToString(oValue).ToLower().Trim() == "True".ToLower() || Convert.ToString(oValue).ToLower().Trim() == "False".ToLower())
                {
                    _bAllowNegativeWHAmount = Convert.ToBoolean(oValue);
                }
                SetPaymentPrefixNumber();
                FillPaymentMode();
                FillCreditCards();
                txtCheckAmount.Enabled = true;
                if (PatientControl == null)
                { LoadPatientStrip(0, 0, true); }
                else
                { PatientControl.ClearDetails(); }
                SetupControls();
                if (!IsCloseDaySet)
                { SetCloseDate(); }
                if (SelectedPaymentTrayID == 0)
                { FillPaymentTray(); }
                pnlCredit.Enabled = false;
                pnlClaimFollowUp.Visible = false;
                lblClaimFollowup.Text = "";
                toolTip1.SetToolTip(lblClaimFollowup, "");
                pnlClaimNote.Visible = false;
                lblClaimNote.Text = "";
                toolTip1.SetToolTip(lblClaimNote, "");

                #region "Checking Followup Feature is On or Off "

              
               
              
                oSettings.GetSetting("FOLLOWUP_FEATURE", 0, gloPMGlobal.ClinicID, out oValue);
                if (Convert.ToString(oValue).ToLower().Trim() == "True".ToLower() || Convert.ToString(oValue).ToLower().Trim() == "False".ToLower())
                {
                    SettingsValue = Convert.ToBoolean(oValue);
                }
                else if (Convert.ToString(oValue).Trim() == "1" || Convert.ToString(oValue).Trim() == "0")
                {
                    SettingsValue = Convert.ToBoolean(Convert.ToString(oValue).Trim() == "1" ? "TRUE" : "FALSE");
                }
                if (!SettingsValue)
                {
                    tls_FollowUpActionDate.Visible = false;
                }
               

                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            finally
            {
                if (oSettings != null)
                {
                    oSettings.Dispose();
                    oSettings = null;
                }

            }
            
        }

        private void LoadFormData(Int64 EOBPaymentID, Int64 EOBID, bool _IsFromSave)
        {
            gloBilling.gloBilling ogloBilling = null;
            try
            {
                if (IsPaymentInProcess)
                {
                    bool _RetCrLine = true;
                    ogloBilling = new gloBilling.gloBilling(AppSettings.ConnectionStringPM, string.Empty);
                     
                    if (_RetCrLine == true)
                    {
                        #region " Set Master Data "

                        DataRow _drEOBPaymentMST = gloInsurancePaymentV2.GetEOBPaymentMST(EOBPaymentID);
                        bool _IsDayCloseMsg = false;
                        if (_drEOBPaymentMST != null)
                        {

                            lblPaymetNo.Text = Convert.ToString(_drEOBPaymentMST["sPaymentNoPrefix"]) + Convert.ToString(_drEOBPaymentMST["sPaymentNo"]);

                            SelectedPaymentTrayID = Convert.ToInt64(_drEOBPaymentMST["nPaymentTrayID"]);
                            SelectedPaymentTray = InsurancePayment.GetPaymentTrayDescription(SelectedPaymentTrayID);

                            SelectedInsuranceCompanyID = Convert.ToInt64(_drEOBPaymentMST["nPayerID"]);
                            SelectedInsuranceCompany = Convert.ToString(_drEOBPaymentMST["sPayerName"]);

                            cmbPayMode.Text = ((PaymentMode)Convert.ToInt32(_drEOBPaymentMST["nPaymentMode"])).ToString();
                            txtCheckNumber.Text = Convert.ToString(_drEOBPaymentMST["sCheckNumber"]);
                            mskCheckDate.Text = String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(_drEOBPaymentMST["nCheckDate"]));
                            cmbCardType.Text = Convert.ToString(_drEOBPaymentMST["sCreditCardType"]); 
                            txtCardAuthorizationNo.Text = Convert.ToString(_drEOBPaymentMST["sAuthorizationNo"]);
                             CheckAmount = Convert.ToDecimal(_drEOBPaymentMST["nCheckAmount"]);
                            _OriginalCheckAmount = CheckAmount;
                            _OriginalCloseDate = String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(_drEOBPaymentMST["nCloseDate"]));
                            _NewOpenDate = string.Empty;
                            if (Convert.ToDateTime(_drEOBPaymentMST["nCloseDate"]) != null)
                            {
                                if (!ogloBilling.IsDayClosed(Convert.ToDateTime(_drEOBPaymentMST["nCloseDate"].ToString())))
                                {
                                    mskCloseDate.Text = String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(_drEOBPaymentMST["nCloseDate"]));
                                    _PaymentCloseDate = mskCloseDate.Text;
                                    _NewOpenDate = String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(_drEOBPaymentMST["nCloseDate"]));
                                }
                                else
                                {
                                    _NewOpenDate = GetNewOpenDate(EOBPaymentID);
                                    if (_NewOpenDate != string.Empty)
                                        _NewOpenDate = String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(_NewOpenDate));
                                    if (_NewOpenDate != string.Empty && !ogloBilling.IsDayClosed(Convert.ToDateTime(_NewOpenDate)))
                                    {
                                        mskCloseDate.Text = _NewOpenDate;
                                        _PaymentCloseDate = mskCloseDate.Text;
                                    }
                                    else
                                    {
                                        _IsDayCloseMsg = true;                                        
                                    }
                                }

                            }
                            
                        }

                        #endregion " Set Master Data "

                        #region "Credit Details in Case of Use Reserves"
                        //if (!_IsFromSave)
                        //{
                            DataTable _dtUseReserve = gloInsurancePaymentV2.GetUseReserveCreditEntry(EOBPaymentID);
                            
                            for (int i = 0; i <= _dtUseReserve.Rows.Count - 1; i++)
                            {

                                //Roopali .. There should be only one UR entry for one check.
                                // e.g. Chk-1------ > R1 -- > 10
                                //Chk-2------ > R1 -- > 2
                                //Only one entry for -- >  Chk-1------ > R1 
                                bool _samecheck = false;
                                for (int index = 0; index <= oCreditResDTL.EOBCreditDTL.Count - 1; index++)
                                {
                                    if (oCreditResDTL.EOBCreditDTL[index].CreditsRef_ID == Convert.ToInt64(_dtUseReserve.Rows[i]["nCreditsRef_ID"])
                                        && oCreditResDTL.EOBCreditDTL[index].ReserveID == Convert.ToInt64(_dtUseReserve.Rows[i]["nReserveRef_ID"])
                                        )
                                    {
                                        _samecheck = true;                                        
                                        oCreditResDTL.EOBCreditDTL[index].Amount += Convert.ToDecimal(_dtUseReserve.Rows[i]["dAmount"]);                                        
                                        oCreditResDTL.EOBCreditDTL[index].CreditsDTL_ID = Convert.ToInt64(_dtUseReserve.Rows[i]["nCreditsDTL_ID"]);
                                        oCreditResDTL.EOBCreditDTL[index].Credits_ID = Convert.ToInt64(_dtUseReserve.Rows[i]["nCreditID"]);
                                        oCreditResDTL.EOBCreditDTL[index].DBReserveAmount = Convert.ToDecimal(_dtUseReserve.Rows[i]["dAmount"]);
                                    }
                                }

                                if (_samecheck == false)
                                {
                                    PaymentCollection.CreditsDTL ReservesLines = new PaymentCollection.CreditsDTL();
                                    ReservesLines.Credits_ID = Convert.ToInt64(_dtUseReserve.Rows[i]["nCreditID"]);

                                    if (txtCheckAmount.Enabled == true)
                                    {
                                        ReservesLines.Amount = Convert.ToDecimal(_dtUseReserve.Rows[i]["dAmount"]);
                                    }
                                    else
                                    {
                                        for (int k = 0; k <= oCreditResDTL_CopyForRule.EOBCreditDTL.Count - 1; k++)
                                        {

                                            if (oCreditResDTL_CopyForRule.EOBCreditDTL[k].CreditsRef_ID == Convert.ToInt64(_dtUseReserve.Rows[i]["nCreditsRef_ID"])
                                        && oCreditResDTL_CopyForRule.EOBCreditDTL[k].ReserveID == Convert.ToInt64(_dtUseReserve.Rows[i]["nReserveRef_ID"])
                                        )
                                            {
                                                ReservesLines.Amount = oCreditResDTL_CopyForRule.EOBCreditDTL[k].Amount;
                                            }

                                        }
                                    }

                                    ReservesLines.DBReserveAmount = Convert.ToDecimal(_dtUseReserve.Rows[i]["dAmount"]);
                                    ReservesLines.CloseDate = Convert.ToDateTime(_dtUseReserve.Rows[i]["dtCloseDate"]);
                                    ReservesLines.CreditsRef_ID = Convert.ToInt64(_dtUseReserve.Rows[i]["nCreditsRef_ID"]);
                                    ReservesLines.CreditsDTL_ID = Convert.ToInt64(_dtUseReserve.Rows[i]["nCreditsDTL_ID"]);
                                    ReservesLines.EntryType = PaymentEntryTypeV2.InsuraceReserved;
                                    ReservesLines.ReserveID = Convert.ToInt64(_dtUseReserve.Rows[i]["nReserveRef_ID"]);
                                    ReservesLines.InsuranceID = Convert.ToInt64(_dtUseReserve.Rows[i]["nInsCompanyID"]);
                                    oCreditResDTL.EOBCreditDTL.Add(ReservesLines);
                                }                          


                           }
                            
                            //oCreditResDTL_CopyForRule has some extra use reserve entries which are not yet used (Allocated)
                            //those lines should be added to oCreditResDTL.EOBCreditDTL only if Rule has to applied.
                            if (txtCheckAmount.Enabled == false)
                            {

                                for (int k = 0; k <= oCreditResDTL_CopyForRule.EOBCreditDTL.Count - 1; k++)
                                {
                                    bool _Foundentry = false;
                                    for (int p = 0; p <= oCreditResDTL.EOBCreditDTL.Count - 1; p++)
                                    {
                                        if (oCreditResDTL.EOBCreditDTL[p].CreditsRef_ID == oCreditResDTL_CopyForRule.EOBCreditDTL[k].CreditsRef_ID
                                        && oCreditResDTL.EOBCreditDTL[p].ReserveID == oCreditResDTL_CopyForRule.EOBCreditDTL[k].ReserveID
                                        )
                                        {
                                            _Foundentry = true;
                                            break; 
                                        }

                                    }
                                    if (_Foundentry == false)
                                    {
                                        PaymentCollection.CreditsDTL ReservesLines = new PaymentCollection.CreditsDTL();
                                        ReservesLines.Credits_ID = oCreditResDTL_CopyForRule.EOBCreditDTL[k].Credits_ID;
                                        ReservesLines.Amount = oCreditResDTL_CopyForRule.EOBCreditDTL[k].Amount ;
                                        ReservesLines.DBReserveAmount =oCreditResDTL_CopyForRule.EOBCreditDTL[k].DBReserveAmount;
                                        ReservesLines.CloseDate =oCreditResDTL_CopyForRule.EOBCreditDTL[k].CloseDate;
                                        ReservesLines.CreditsRef_ID =oCreditResDTL_CopyForRule.EOBCreditDTL[k].CreditsRef_ID;
                                        ReservesLines.CreditsDTL_ID = oCreditResDTL_CopyForRule.EOBCreditDTL[k].CreditsDTL_ID;
                                        ReservesLines.EntryType = PaymentEntryTypeV2.InsuraceReserved;
                                        ReservesLines.ReserveID =oCreditResDTL_CopyForRule.EOBCreditDTL[k].ReserveID;
                                        ReservesLines.InsuranceID =oCreditResDTL_CopyForRule.EOBCreditDTL[k].InsuranceID;
                                        oCreditResDTL.EOBCreditDTL.Add(ReservesLines);
                                         
                                    }

                                }


                            }




                        #endregion "Credit Details in Case of Use Reserves"

                        FillEOBPayments(EOBPaymentID, EOBID);
                        OnRemainingCalculationChanged();
                        decimal _CheckRemaining = 0;
                        if (_drEOBPaymentMST != null)
                        {
                            _CheckRemaining = Convert.ToDecimal(_drEOBPaymentMST["Remaining"].ToString());
                        }
                        
                        if (_NewOpenDate != "" && ogloBilling.IsDayClosed(Convert.ToDateTime(_drEOBPaymentMST["nCloseDate"].ToString())) == true && _CheckRemaining == 0 && InsurancePayment.GetDialyCloseValidationSetting(gloGlobal.gloPMGlobal.ClinicID) == true)
                            txtCheckAmount.Enabled = false;
                        else
                            txtCheckAmount.Enabled = true;
                        
                        if (_IsDayCloseMsg == true && _IsFromSave == false)
                            MessageBox.Show("Payment’s Close Date of " + _OriginalCloseDate.ToString() + " is closed.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }

        }

        private string GetNewOpenDate(long EOBPaymentID)
        {
            try
            {
                return gloInsurancePaymentV2.GetNewOpenCloseDate_V2(EOBPaymentID);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return "";
            }
        }

        private void SetupControls()
        {
            try
            {
                ShowAlertMessage = false;

                btnSearchInsuranceCompany.Select();
                btnSearchInsuranceCompany.Focus();

                DesignPaymentGrid(c1SinglePayment);
                DesignPaymentGrid(c1SinglePaymentTotal);

                if (!IsPendingCheckLoaded)
                {
                    // Note : EOBPaymentGrid will reset in 2 conditions 
                    // 1) New Payment
                    // 2) Load / Clear Pending Check

                    if (!IsMultiPaymentGridDesigned) { DesignPaymentGrid(c1MultiplePayment); IsMultiPaymentGridDesigned = true; }
                    //if (!IsMultiPaymentGridDesigned) { DesignMultiplePaymentGrid(c1MultiplePayment); IsMultiPaymentGridDesigned = true; }
                    if (!IsMultiPaymentTotalGridDesigned) { DesignPaymentGrid(c1MultiplePaymentTotal); IsMultiPaymentTotalGridDesigned = true; }
                }
                 
                //Hiding Correction Grid
                SetupCorrectionTBWindow(false);
                //this.oCreditResDTL = new PaymentCollection.Credit();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            
        }

        private void SetupCorrectionTBWindow(bool showvalue)
        {
            try
            {               
                if (showvalue)
                {
                    pnlSinglePaymentCorrTB.Visible = true;
                    pnlSinglePaymentCorrTB.Height = (pnlSinglePayment.Height - c1SinglePaymentTotal.Height) / 2;
                    pnlSinglePaymentAllocationHdr.Visible = true;

                    //Checking Correction Grid is already Designed or not
                    if (!IsCorrectionGridDesigned)
                    { DesignPaymentGrid(c1SinglePaymentCorrTB); IsCorrectionGridDesigned = true; }
                }
                else
                {
                    pnlSinglePaymentCorrTB.Visible = false;
                    pnlSinglePaymentCorrTB.Height = 0;
                    pnlSinglePaymentAllocationHdr.Visible = false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void ResetForm()
        {

            ShowAlertMessage = false;
            EOBPaymentID = 0;
            LoadFormData();
            SelectedInsuranceCompanyID = 0;
            if (_IsFromOnInsuranceSelected == true) { _IsFromOnInsuranceSelected = false; }
            _AmountAddedToReserve = 0;
            //_lastPayment = 0;
           // _AmountAddedTooldReserve = 0;
           // _TakeBack = 0;
            // This will reset the check amount (blank text box & check amount = 0
            ResetCheckAmount = true;
            AmountAddedToReserve = 0;
            TakeBackAmount = 0;
            AmountTakenFromReserve = 0;
            _AmountApplied = 0;
            TotalFundsAvaiable = 0;
            TotalFundsRemaining = 0;
            txtPayMstNotes.Text = "";
            txtCheckNumber.Text = string.Empty;
            txtClaimRemittanceRef.Text = string.Empty;
            mskCheckDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");
            PaymentAction = FormMode.NewPaymentMode;
            _PaymentCloseDate = "";
            _NewOpenDate = "";
            _isOriginalPayment = false;
            ReserveDetails = new PaymentCollection.Reserves();
            txtCheckAmount.Enabled = true;
            _OriginalCloseDate = "";
            pnlCredit.Visible = false;
            if (oCreditResDTL != null)
            {
                oCreditResDTL.EOBCreditDTL.Clear();
            }
            if (oCreditLine !=  null)
            {
                oCreditLine.EOBReserves.Clear();  
            }
            if (oCreditResDTL_CopyForRule != null)
            {
                oCreditResDTL_CopyForRule.EOBCreditDTL.Clear();
            }
            //this.oCreditLine = new PaymentCollection.Credit();
            //this.oCreditResDTL = new PaymentCollection.Credit();


        }

        #endregion

        #region " Patient Control Events "

        private void PatientControl_OnClaimNumberEntered(string ClaimText)
        {
            try
            {

                DialogResult _result = DialogResult.No;

                if (IsPaymentAllocated)
                {
                    // _result = SaveChangesAlert(); 
                    _result = MessageBox.Show("Entries have been made against claim " + txtClaimNo.Text + ". Save changes before continuing?", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3);
                }

                if (_result == DialogResult.No)
                {
                    LoadClaim();
                    OnRemainingCalculationChanged();

                }
                else if (_result == DialogResult.Yes)
                {
                    try
                    {
                        if (txtCheckAmount.Text.Trim() == "" && IsPaymentAllocated)
                        {
                            preSave();
                        }
                        else
                        {
                            PerformSavePayment();
                        }
                    }
                    catch (Exception ex)
                    { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
                }




                //DialogResult _result = DialogResult.No;
                //Boolean _bResetForm = false;

                //if (IsPaymentAllocated)
                //{
                //    _result = MessageBox.Show("Entries have been made against claim " + txtClaimNo.Text
                //        + ". Save changes before continuing?", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3);
                //    _bResetForm = true;
                //}
                //else { _bResetForm = true; }

                //if (_result == DialogResult.No)
                //{
                //    if (_bResetForm == true)
                //    {
                //        PatientControl._bIsClaimChange = true;
                //        ResetForm();
                //        txtCheckRemaining.Text = string.Empty;
                //        PatientControl._bIsClaimChange = false;
                //        _bResetForm = false;
                //    }

                //    LoadClaim();

                //}
                //else if (_result == DialogResult.Yes)
                //{
                //    try
                //    {
                //        if (txtCheckAmount.Text.Trim() == string.Empty && IsPaymentAllocated)
                //        {
                //            preSave();
                //        }
                //        else
                //        {
                //            PerformSavePayment();
                            
                //        }
                //    }
                //    catch (Exception ex)
                //    { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
                //}
         
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            
        }
              
        private void PatientControl_OnClearPatientDetails()
        {
            try
            {
                // Hide Crosswalk Option 
                chkShowCrosswalk.Visible = false;
                // Reset the claim details here 
                ClaimDetails = new SplitClaimDetails();
                // Reset the claim remittance number
                txtClaimRemittanceRef.Text = string.Empty;
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        private void PatientControl_OnInsuranceSelected(gloStripControl.InsuranceSelectedArgs args)
        {
            try
            {
                _IsFromOnInsuranceSelected = true;
                this.c1SinglePayment.EnterCell -= new System.EventHandler(this.c1SinglePayment_EnterCell);
                this.c1SinglePaymentCorrTB.EnterCell -= new System.EventHandler(this.c1SinglePayment_EnterCell);

                if (args.InsuraceSelfMode != PayerMode.Self.GetHashCode() && args.ContactID != 0)
                {
                    if (!IsPaymentInProcess)
                    {
                        
                        {
                            if (args.ContactID > 0)
                            {                               
                                gloAccountsV2.PaymentCollection.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new gloAccountsV2.PaymentCollection.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);

                                this.SelectedInsuranceCompanyID = Convert.ToInt64(ogloEOBPaymentInsurance.GetContactCompanyId(args.ContactID));
                                this.SelectedInsuranceCompany = Convert.ToString(ogloEOBPaymentInsurance.GetContactCompanyName(args.ContactID));
                                                               
                                ogloEOBPaymentInsurance.Dispose();
                            }
                            
                        }
                    }
                }

                if (args.ContactID == 0)
                {
                    // Note : 
                    // 1) If no plan is selected & payment is done (PaymentInProcess=true) then please don't reset the Insurance Company 
                    // 2) Insurance Company will be reset only if payment is not done  
                    // 
                    if (!IsPaymentInProcess)
                    { this.SelectedInsuranceCompanyID = 0; }

                    this.PatientInsuranceID = 0;
                    this.SelectedInsurancePlan = string.Empty;
                    this.IsSelectedPlanOnHold = false;

                    this.ContactInsuranceID = 0;
                                     
                    SetupCorrectionTBWindow(false);
                }
                else if (args.ContactID != 0)
                {
                    this.SelectedInsurancePlan = args.SelectedInsurancePlan;
                    this.IsSelectedPlanOnHold = args.IsSelectedPlanOnHold;
                   
                    // Note : 
                    // PatientInsuranceID & ContactInsuranceID  has to be set every time when insurance plan changed
                    // As these are required to set the correction mode.
                    // 
                    this.PatientInsuranceID = args.InsuranceID;
                    this.ContactInsuranceID = args.ContactID;
                    

                    if (PatientControl != null && PatientControl.PatientID > 0 && PatientControl.ClaimNumber > 0)
                    {

                        SetCorrectionMode();
                      
                        #region " Set Correction Take back Window "

                        //Set Correction Grid and Fill Data if  PaymentAction is in Correction Mode
                        if (PaymentAction == FormMode.CorrectionMode)
                        {
                            SetupCorrectionTBWindow(true);
                            OnRemainingCalculationChanged();
                            FillDataInCorrectionTBWindow();
                        }
                        else
                        { SetupCorrectionTBWindow(false);}

                        //if (gloInsurancePaymentV2.IsRebilled(ClaimDetails.TransactionMasterID, ClaimDetails.TransactionID, ContactInsuranceID, PatientInsuranceID))
                        //{
                        ClaimRemittanceReferenceNo = gloInsurancePaymentV2.GetClaimRemittanceRefNo(ClaimDetails.TransactionMasterID, ContactInsuranceID, PatientInsuranceID);
                        txtClaimRemittanceRef.Focus();
                        //}
                        //else
                        //{ ClaimRemittanceReferenceNo = string.Empty; }
                        #endregion
                    }

                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                this.c1SinglePayment.EnterCell += new System.EventHandler(this.c1SinglePayment_EnterCell);
                this.c1SinglePaymentCorrTB.EnterCell += new System.EventHandler(this.c1SinglePayment_EnterCell);
                //_IsFromOnInsuranceSelected = false;
            }
        }

        #endregion

        #region " Patient Control Methods "

        private void LoadPatientStrip(Int64 PatientId, Int64 PatientProviderId, bool SearchEnable)
        {
            try
            {
                if (PatientControl != null)
                {
                    Control[] controls = pnlPatientStrip.Controls.Find(PatientControl.Name, true);
                    pnlPatientStrip.Controls.Remove(controls[0]);
                    try
                    {
                        PatientControl.OnClaimNumberEntered -= new gloStripControl.ucPatientStripControl.ClaimNumberEntered(PatientControl_OnClaimNumberEntered);
                        PatientControl.OnInsuranceSelected -= new gloStripControl.ucPatientStripControl.InsuranceSelected(PatientControl_OnInsuranceSelected);
                        PatientControl.OnClearPatientDetails -= new gloStripControl.ucPatientStripControl.ClearPatientDetails(PatientControl_OnClearPatientDetails);
                        PatientControl.OnLoadViewBenefit -= new gloStripControl.ucPatientStripControl.ViewBenefit(PatientControl_OnLoadViewBenefit);
                    }
                    catch 
                    { 
                    }
                    PatientControl.Dispose();
                    PatientControl = null;                
                }
                PatientControl = new gloStripControl.ucPatientStripControl();
                PatientControl.OnClaimNumberEntered += new gloStripControl.ucPatientStripControl.ClaimNumberEntered(PatientControl_OnClaimNumberEntered);
                PatientControl.OnInsuranceSelected += new gloStripControl.ucPatientStripControl.InsuranceSelected(PatientControl_OnInsuranceSelected);
                PatientControl.OnClearPatientDetails += new gloStripControl.ucPatientStripControl.ClearPatientDetails(PatientControl_OnClearPatientDetails);
                PatientControl.OnLoadViewBenefit += new gloStripControl.ucPatientStripControl.ViewBenefit(PatientControl_OnLoadViewBenefit);

                pnlPatientStrip.Controls.Add(PatientControl);
                PatientControl.Dock = DockStyle.Top;
                PatientControl.ViewSearchOptionCheckBox = false;
                PatientControl.PatientStripHeaderText = "Claim # :";
                PatientControl.AllowEditingParty = true;
                this.nPAccountID = PatientControl.PAccountID;
                this.nGuarantorID = PatientControl.GuarantorID;
                this.nAccountPatientID = PatientControl.AccountPatientID;
                pnlSinglePayment.BringToFront();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }
        private void LoadClaim()
        
        {
            //Set ClaimDetails Property for further use
            DataSet dsClaimFollowUp = null;
          //  DataTable dtAccountNotes = null;
            DataTable dtClaimNote = null;
            DataTable dtClaimFollowUp = null;
            ClaimRemittanceReferenceNo = string.Empty;
            ClaimDetails = new SplitClaimDetails(PatientControl.ClaimNumber, PatientControl.SubClaimNumber);
            this.nPAccountID = ClaimDetails.PAccountID;
            this.nGuarantorID = ClaimDetails.GuarantorID;
            this.nAccountPatientID = ClaimDetails.AccountPatientID;
            PatientID = ClaimDetails.PatientID;
          
            try
            {
                this.c1SinglePayment.EnterCell -= new System.EventHandler(this.c1SinglePayment_EnterCell);
                this.c1SinglePaymentCorrTB.EnterCell -= new System.EventHandler(this.c1SinglePaymentCorrTB_EnterCell);

                if (PatientControl.ClaimNumber > 0)
                {
                    if (IsValidClaim())
                    {                       
                        ContactInsuranceID = 0;
                        PatientInsuranceID = 0;

                        SetupControls();
                        // Note : 
                        // FillPaymentGrid() method should be called after PatientControl.FillDetails() method
                        // This is required for CPT Crosswalk module
                        FillPaymentGrid();
                        if (nLastTransactionMasterID > 0 && nLastTransactionMasterID != ClaimDetails.TransactionMasterID)
                        {
                            ReleaseLockStatus(nLastTransactionMasterID);
                        }
                        nLastTransactionMasterID = ClaimDetails.TransactionMasterID;
                        PatientControl.SelectedInsuranceParty = ClaimDetails.CurrentResponsibleParty;
                        PatientControl.LoadselectedInsuranceParty = ClaimDetails.CurrentResponsibleParty;
                        PatientControl.IsRevisedPayment = true;
                        PatientControl.FillDetails(ClaimDetails.TransactionMasterID, ClaimDetails.TransactionID);
                        //if (gloInsurancePaymentV2.IsRebilled(ClaimDetails.TransactionMasterID, ClaimDetails.TransactionID, ContactInsuranceID, PatientInsuranceID))
                        //{
                            ClaimRemittanceReferenceNo = gloInsurancePaymentV2.GetClaimRemittanceRefNo(ClaimDetails.TransactionMasterID, ContactInsuranceID, PatientInsuranceID);
                        //}
                        txtClaimRemittanceRef.Focus();
                        if (CL_FollowUpCode.IsFollowUpFeatureON())
                        {
                            dsClaimFollowUp = CL_FollowUpCode.GetClaimFollowUp(ClaimDetails.TransactionMasterID, ClaimDetails.TransactionID);
                            if (dsClaimFollowUp != null && dsClaimFollowUp.Tables.Count > 0)
                            {
                                dtClaimNote = dsClaimFollowUp.Tables[0];
                                dtClaimFollowUp = dsClaimFollowUp.Tables[1];
                            }
                            if (dtClaimNote != null && dtClaimNote.Rows.Count > 0)
                            {
                                if (Convert.ToString(dtClaimNote.Rows[0]["Note"]) != "")
                                {
                                    pnlClaimNote.Visible = true;
                                    lblClaimNote.Text = dtClaimNote.Rows[0]["Note"].ToString();
                                    toolTip1.SetToolTip(lblClaimNote, SplitToolTip(dtClaimNote.Rows[0]["Note"].ToString()));
                                }
                                else
                                {
                                    pnlClaimNote.Visible = false;
                                    lblClaimNote.Text = "";
                                    toolTip1.SetToolTip(lblClaimNote, "");
                                }
                            }
                            else
                            {
                                pnlClaimNote.Visible = false;
                                lblClaimNote.Text = "";
                                toolTip1.SetToolTip(lblClaimNote, "");
                            }
                            if (dtClaimFollowUp != null && dtClaimFollowUp.Rows.Count > 0)
                            {
                                if (Convert.ToString(dtClaimFollowUp.Rows[0]["sFollowupDescription"]) != "")
                                {
                                    pnlClaimFollowUp.Visible = true;
                                    lblClaimFollowup.Text = dtClaimFollowUp.Rows[0]["sFollowupDescription"].ToString();
                                    toolTip1.SetToolTip(lblClaimFollowup, SplitToolTip(dtClaimFollowUp.Rows[0]["sFollowupDescription"].ToString()));
                                    if (Convert.ToDateTime(dtClaimFollowUp.Rows[0]["dtClaimFollowUpDate"].ToString()) <= DateTime.Now)
                                    {
                                        lblClaimFollowup.Font = gloGlobal.clsgloFont.getFontFromExistingSource(lblClaimFollowup.Font, FontStyle.Bold);
                                        lblClaimFollowup.ForeColor = System.Drawing.Color.Maroon;
                                    }
                                    else
                                    {
                                        lblClaimFollowup.ForeColor = System.Drawing.Color.Black;
                                    }
                                }
                                else
                                {
                                    pnlClaimFollowUp.Visible = false;
                                    lblClaimFollowup.Text = "";
                                    toolTip1.SetToolTip(lblClaimFollowup, "");
                                }
                            }
                            else
                            {
                                pnlClaimFollowUp.Visible = false;
                                lblClaimFollowup.Text = "";
                                toolTip1.SetToolTip(lblClaimFollowup, "");
                            }
                        }
                    }
                    else
                    {
                        // reset the remittance ref no
                        ClaimRemittanceReferenceNo = string.Empty;
                        SetupControls();
                        PatientControl.ClearDetails();
                        PatientControl.SelectSearchBox();
                        chkShowCrosswalk.Visible = false;
                    }
                }
                else
                {
                    // reset the remittance ref no
                    ClaimRemittanceReferenceNo = string.Empty;
                    SetupControls();
                    PatientControl.ClearDetails();
                    PatientControl.SelectSearchBox();
                    chkShowCrosswalk.Visible = false;

                    pnlClaimFollowUp.Visible = false;
                    lblClaimFollowup.Text = "";
                    toolTip1.SetToolTip(lblClaimFollowup, "");

                    pnlClaimNote.Visible = false;
                    lblClaimNote.Text = "";
                    toolTip1.SetToolTip(lblClaimNote, "");
                }

                if (ClaimDetails.HasChildClaims || this.SelectedInsurancePlan.Trim() == "")
                {
                    tls_FollowUpActionDate.Enabled = false;
                }
                else
                {
                    tls_FollowUpActionDate.Enabled = true;
                }
              
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                this.c1SinglePayment.EnterCell += new System.EventHandler(this.c1SinglePayment_EnterCell);
                this.c1SinglePaymentCorrTB.EnterCell += new System.EventHandler(this.c1SinglePaymentCorrTB_EnterCell);
            }
        }
        private void SetAlertMessage()
        {
            string _claim = gloInsurancePaymentV2.GetFormattedClaimPaymentNumber(Convert.ToString(PatientControl.ClaimNumber));

            if (!String.IsNullOrEmpty(PatientControl.SubClaimNumber))
            { _claim = string.Concat(_claim, "-", PatientControl.SubClaimNumber); }

            if (((IsSelectedPlanOnHold) || ClaimDetails.IsClaimOnHold) || (ClaimDetails.HasChildClaims))
            {
                ShowAlertMessage = true;

                // Don't show the Hold Alert in case of following status
                // 1) Status is InsurancePaid 
                // 2) Status is SendToClaimManger
                // 3) Status is SendToClearingHouse
                // 4) If claim is not a split claim  
                if (ClaimDetails.ClaimStatus.Equals(TransactionStatus.InsurancePaid.GetHashCode()) || ClaimDetails.ClaimStatus.Equals(TransactionStatus.SendToClaimManager.GetHashCode()) || ClaimDetails.ClaimStatus.Equals(TransactionStatus.SendToClearingHouse.GetHashCode()))
                {
                    // Set the property to false to ignore the plan hold message alert
                    IsSelectedPlanOnHold = false;
                }

                // 1) If Plan and Claim both are on hold
                if ((IsSelectedPlanOnHold) && (ClaimDetails.IsClaimOnHold))
                { lblAlertMessage.Text = "Claim and Insurance Plan [" + SelectedInsurancePlan + "] On Hold"; return; }
                // 2) If only plan on hold
                if (IsSelectedPlanOnHold)
                { lblAlertMessage.Text = "Insurance Plan [" + SelectedInsurancePlan + "] On Hold"; return; }
                // 3) If only claim on hold
                if (ClaimDetails.IsClaimOnHold)
                { lblAlertMessage.Text = "Claim On Hold"; return; }
                // 4) If claim is a split claim
                if (ClaimDetails.HasChildClaims)
                { lblAlertMessage.Text = "Claim " + _claim + " is Split Claim"; return; }

                // Note : 
                // 1) If claim is on billing hold / plan hold then split claim indication will not come.
                // 2) If plan is on hold, claim status is (paid/STCM/STCH) & claim is not splitted then show split indication only.
            }
            else
            { ShowAlertMessage = false; }
        }
        private string SplitToolTip(string strOrig)
        {
            try
            {
                string[] strArray;
                string CR = "\r\n";
                string strBuilder = "";
                string strReturn = "";
                strArray = strOrig.Split(' ');
                foreach (string strOneWord in strArray)
                {
                    strBuilder = (strBuilder + (strOneWord + ' '));
                    if (strBuilder.Length > 70)
                    {
                        strReturn = (strReturn + (strBuilder + CR));
                        strBuilder = "";
                    }
                }
                if (strBuilder.Length < 8)
                {
                    strReturn = strReturn.Substring(0, (strReturn.Length - 2));
                }
                return (strReturn + strBuilder);
            }
            catch //(Exception e)
            {
                return strOrig;
            }
        }
        #endregion

        #region " ToolStrip Button Click Events "
       
        private void tls_btnSave_Click(object sender, EventArgs e)
        {
            try
            {               
                //if the user has entered remittance information onto a claim without having an Insurance Payment
                //Here we check that Insurance Payment Information is entered or not (Header information.)
                if (txtCheckAmount.Text.Trim() == "" && IsPaymentAllocated)
                {
                    preSave();
                }
                else
                {
                    PerformSavePayment();
                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        private void preSave()
        {
            frmSavePaymentDialog objFrmSaveDialog = null;
            try
            {
                //If the claim has never been remitted by the selected plan then
                if (PaymentAction == FormMode.NewPaymentMode)
                {
                    if (lblReserveAmount.Text != string.Empty && Convert.ToDecimal(lblReserveAmount.Text) > 0)
                        objFrmSaveDialog = new frmSavePaymentDialog(3);
                    else
                        objFrmSaveDialog = new frmSavePaymentDialog(1);
                    objFrmSaveDialog.ShowDialog(this);
                    PaymentDialogResult = objFrmSaveDialog.DialogResult;
                }
                //If the claim has been remitted previously by the selected plan then
                else if (PaymentAction == FormMode.CorrectionMode)
                {
                    if (lblReserveAmount.Text != string.Empty && Convert.ToDecimal(lblReserveAmount.Text) > 0)
                        objFrmSaveDialog = new frmSavePaymentDialog(3);
                    else
                        objFrmSaveDialog = new frmSavePaymentDialog(2);
                    objFrmSaveDialog.ShowDialog(this);
                    PaymentDialogResult = objFrmSaveDialog.DialogResult;
                }

                switch (PaymentDialogResult)
                {
                    //Pending Insurance Payment
                    case 1:
                        LoadPendingCheck();
                        
                        break;
                    //Original Payment
                    case 2:
                        LoadOriginalPayment();
                        break;
                    //New Payment
                    case 3:
                        MessageBox.Show("Enter the Payment information and reselect save.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (objFrmSaveDialog != null)
                {
                    objFrmSaveDialog.Dispose();
                    objFrmSaveDialog = null;
                }
            }
        }

        private void LoadOriginalPayment()
        {
            try
            {
                if (oCreditResDTL.EOBCreditDTL != null && oCreditResDTL.EOBCreditDTL.Count > 0)
                {
                    string EOBPaymentID = string.Empty;
                    for (int lnInd = 0; lnInd < oCreditResDTL.EOBCreditDTL.Count; lnInd++)
                    {
                        if (lnInd == 0)
                            EOBPaymentID = oCreditResDTL.EOBCreditDTL[lnInd].CreditsRef_ID.ToString();
                        else
                            EOBPaymentID = EOBPaymentID + "," + oCreditResDTL.EOBCreditDTL[lnInd].CreditsRef_ID;

                    }

                    DataRow DrReserveEOB;
                    DrReserveEOB = gloInsurancePaymentV2.GetEOBOriginalPaymentId(EOBPaymentID);

                    _EOBPaymentID = Convert.ToInt64(DrReserveEOB["nEOBPaymentID"]);
                    IsPendingCheckLoaded = true;
                    LoadFormData(_EOBPaymentID, 0, false);
                    
                }
                else
                {

                    _EOBPaymentID = gloInsurancePaymentV2.GetEOBOriginalPaymentId(ContactInsuranceID, PatientInsuranceID, ClaimDetails.TransactionMasterID, ClaimDetails.TransactionID, ClaimDetails.SubClaimNo);
                    IsPendingCheckLoaded = true;
                    LoadFormData(_EOBPaymentID, 0, false);
                    
                }
                _isOriginalPayment = true;
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }

        }
        
        private void tls_btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult _result = DialogResult.No;

                if (IsPaymentMade())
                { _result = SaveChangesAlert(); }

                //if (_result != DialogResult.Cancel)
                //{ IsPendingCheckLoaded = false; }

                if (_result == DialogResult.No)
                {
                    ReleaseLockStatus(ClaimDetails.TransactionMasterID);

                    ///Roopali. Moved this code to reset form..

                    ////.. Clear Credit Details In case of New Payment
                    //this.oCreditLine = new PaymentCollection.Credit();
                    //this.oCreditResDTL = new PaymentCollection.Credit();
                    ////.. Clear Credit Details In case of New Payment

                    IsPendingCheckLoaded = false;
                    IsMultiPaymentGridDesigned = false;
                    IsMultiPaymentTotalGridDesigned = false;
                    PatientControl.ClaimFieldText = "";
                  //  c1MultiplePayment.Clear();
                    c1MultiplePayment.DataSource = null;
                    ResetForm();
                }

            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        private void tls_btnViewEOB_Click(object sender, EventArgs e)
        {
            GetControlSelection();
            ToggleEOB();
            SetControlSelection();
        }

        private void tls_btnCharge_Click(object sender, EventArgs e)
        {
            try
            {
                GetControlSelection();
                ShowModifyCharges();
                OnRemainingCalculationChanged();

                // solving the Problem# 318: Reset the form when the loaded check is voided.
                //****************************************************
                if (IsPendingCheckLoaded)
                {
                    bool _isPaymentVoid = gloInsurancePaymentV2.IsVoidedInsurancePayment(EOBPaymentID);
                    if (_isPaymentVoid)
                    {
                        IsPendingCheckLoaded = false;
                        ResetForm();
                    }
                }
                //*****************************************************
            }

            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (ClaimDetails.IsClaimVoided==false)
                {
                    SetControlSelection(); 
                }
            }
        }

        private void tls_Top_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
         {
            if (PatientControl != null)
            {
                if (PatientControl.IsSplitClaimSearchActive)
                {
                    PatientControl.IsSplitClaimSearchActive = false;
                }
            }
        }

        private void tls_InsuranceLog_Click(object sender, EventArgs e)
        {
            frmInsurancePaymentLogV2 ofrmInsurancePaymentLog = new frmInsurancePaymentLogV2();
            try
            {
                GetControlSelection();
                ofrmInsurancePaymentLog.ShowDialog(this);

                if (IsPendingCheckLoaded)
                {
                    bool _isPaymentVoid = gloInsurancePaymentV2.IsVoidedInsurancePayment(EOBPaymentID);
                    if (_isPaymentVoid)
                    {
                        IsPendingCheckLoaded = false;
                        ResetForm();
                    }
                }

            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                if (ofrmInsurancePaymentLog != null) { ofrmInsurancePaymentLog.Dispose(); }
                SetControlSelection();
            }
        }

        private void tsb_PaymentPatient_Click(object sender, EventArgs e)
        {
            if (PatientControl != null && PatientControl.PatientID > 0 && PatientControl.ClaimNumber > 0)
            {
                GetControlSelection();
                //frmPaymentPatient oPaymentInsurace = new frmPaymentPatient(PatientControl.PatientID, false, 0, 0, 0, 0, EOBPaymentSubType.Other);
                gloAccountsV2.frmPatientPaymentV2 frmPatientPaymentV2 = new gloAccountsV2.frmPatientPaymentV2(PatientControl.PatientID, false, 0, 0, 0, 0, EOBPaymentSubType.Other);
                try
                {
                    frmPatientPaymentV2.StartPosition = FormStartPosition.CenterScreen;
                    frmPatientPaymentV2.WindowState = FormWindowState.Maximized;
                    frmPatientPaymentV2.ShowInTaskbar = false;
                    frmPatientPaymentV2.ShowDialog(this);
                    PatientControl.IsRevisedPayment = true;
                    PatientControl.FillDetails(ClaimDetails.TransactionMasterID, ClaimDetails.TransactionID);
                    FillPaymentGrid();
                    OnRemainingCalculationChanged();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (frmPatientPaymentV2 != null) { frmPatientPaymentV2.Dispose(); }
                    SetControlSelection();
                }
            }
            else
            {
                MessageBox.Show("Please select the claim", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tsb_Refund_Click(object sender, EventArgs e)
        {
            ArrayList _arrUsedUnsavedReservesOnForm = null;

            try
            {
                GetControlSelection();
                if (this.oCreditResDTL != null && this.oCreditResDTL.EOBCreditDTL.Count > 0)
                {
                    _arrUsedUnsavedReservesOnForm = new ArrayList();
                    for (int objIndex = 0; objIndex < this.oCreditResDTL.EOBCreditDTL.Count; objIndex++)
                    {
                        if (this.oCreditResDTL.EOBCreditDTL[objIndex].CreditsDTL_ID == 0)
                        {
                            _arrUsedUnsavedReservesOnForm.Add(this.oCreditResDTL.EOBCreditDTL[objIndex].ReserveID);
                        }
                    }
                }

                using (frmInsurancePaymentRefundLogV2 ofrmInsurancePaymentRefundLog = new frmInsurancePaymentRefundLogV2(_arrUsedUnsavedReservesOnForm))
                {

                    ofrmInsurancePaymentRefundLog.ShowDialog(this);
                    if (IsPendingCheckLoaded)
                    {
                        bool _isPaymentVoid = gloInsurancePaymentV2.IsVoidedInsurancePayment(EOBPaymentID);
                        if (_isPaymentVoid)
                        {
                            IsPendingCheckLoaded = false;
                            ResetForm();
                        }
                    }

                }




                if (_arrUsedUnsavedReservesOnForm != null && _arrUsedUnsavedReservesOnForm.Count > 0)
                {
                    _arrUsedUnsavedReservesOnForm.Clear();
                    _arrUsedUnsavedReservesOnForm = null;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
                SetControlSelection();
            }
        }

        private void tls_btnPatAcct_Click(object sender, EventArgs e)
        {
            frmPatientFinancialViewV2 frm = null;
            try
            {
                GetControlSelection();
                if (ClaimDetails.IsClaimExist)
                {
                    if (ClaimDetails.TransactionID != 0)
                    {
                        frm = new frmPatientFinancialViewV2(PatientControl.PatientID);
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.WindowState = FormWindowState.Maximized;
                        frm.ShowInTaskbar = false;
                        frm.IsCalledFromInsPmt = true;
                        frm.ShowDialog(this);
                        LoadClaim();
                        OnRemainingCalculationChanged();
                    }
                }
                else
                {
                    MessageBox.Show("Please select the claim", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (IsPendingCheckLoaded)
                {
                    bool _isPaymentVoid = gloInsurancePaymentV2.IsVoidedInsurancePayment(EOBPaymentID);
                    if (_isPaymentVoid)
                    {
                        IsPendingCheckLoaded = false;
                        ResetForm();
                    }
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

        private void tls_btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult _result = DialogResult.No;
                              
                if (IsPaymentMade())
                { _result = SaveChangesAlert(); }

                if (_result == DialogResult.No)
                { this.Close(); }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }

        }
        #endregion

        #region " Form Controls Events "

        private void btnSearchInsuranceCompany_Click(object sender, EventArgs e)
        {
            SearchInsuranceCompany();
        }

        private void btnUseReserve_Click(object sender, EventArgs e)
        {
            UseReserveAmount();            
        }

        private void btnTraySelection_Click(object sender, EventArgs e)
        {
            SelectPaymentTray();
        }

        private void btnReserveRemaining_Click(object sender, EventArgs e)
        {
            ReserveRemaining();
        }

        private void txtCheckAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
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
                    if (txtCheckAmount.Text != "")
                    {
                        try
                        { CheckAmount = Convert.ToDecimal(txtCheckAmount.Text); }
                        catch
                        { CheckAmount = 0; }
                                               
                    }
                    else
                    { CheckAmount = 0; }
                                      
                }
            }
            catch (System.OverflowException ex)
            {
                MessageBox.Show("Amount is invalid, Please enter a valid amount", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                return;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        private void btnLoadCheck_Click(object sender, EventArgs e)
        {
            try
            {
                LoadPendingCheck();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void btnRemoveCheck_Click(object sender, EventArgs e)
        {
            if (IsPendingCheckLoaded)
            {
                DialogResult _result = DialogResult.No;
                             
                if (IsPaymentMade())
                { _result = SaveChangesAlert(); }

                if (_result == DialogResult.No)
                {
                    IsPendingCheckLoaded = false;
                    IsMultiPaymentGridDesigned = false;
                    IsMultiPaymentTotalGridDesigned = false;
                    this.oCreditLine = new PaymentCollection.Credit();
                    this.oCreditResDTL = new PaymentCollection.Credit();
                   // c1MultiplePayment.Clear();
                    c1MultiplePayment.DataSource = null;

                    ResetForm();
                }
            }
        }

        private void cmbPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPaymentModeDetails();
        }

        private void txtCheckAmount_Leave(object sender, EventArgs e)
        {
            if (txtCheckAmount.Text != "")
            {
                try
                { CheckAmount = Convert.ToDecimal(txtCheckAmount.Text); }

                catch
                { CheckAmount = 0; }

            }
            else
            { CheckAmount = 0; }
        }

        private bool CheckAmount_Modify()
        {
            bool _isModifyCheckAmt = false;

            if (txtCheckAmount.Text != "" && CheckAmount != _OriginalCheckAmount)
            {
                gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(AppSettings.ConnectionStringPM, string.Empty);
                try
                {

                    bool _isAppyRules = false;

                    _isAppyRules = gloInsurancePaymentV2.GetDialyCloseValidationSetting(gloGlobal.gloPMGlobal.ClinicID);

                    if (!_isAppyRules)
                    {
                        if (_NewOpenDate != "" && ogloBilling.IsDayClosed(Convert.ToDateTime(_NewOpenDate)) == true)
                        {
                            DialogResult _result = MessageBox.Show("Payment was closed on " + _OriginalCloseDate.ToString()   + ". Are you sure you want to modify the payment amount?", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                            if (_result == DialogResult.Yes)
                                _isModifyCheckAmt = true;
                            else
                            {
                                txtCheckAmount.Text = _OriginalCheckAmount.ToString();
                                CheckAmount = _OriginalCheckAmount;
                                frmInsurancePayment_OnRemainingCalculationChanged();
                                _isModifyCheckAmt = false;
                            }
                        }
                        else
                            _isModifyCheckAmt = true;
                    }
                    else
                    {
                        if (Convert.ToDecimal(txtCheckRemaining.Text) > 0)
                        {
                            if (_NewOpenDate != "" && ogloBilling.IsDayClosed(Convert.ToDateTime(_NewOpenDate)) == true)
                            {
                                mskCloseDate.Text = _NewOpenDate;
                                DialogResult _result = MessageBox.Show("Payment was closed on " + _OriginalCloseDate.ToString() + ". Are you sure you want to modify the payment amount?", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                                if (_result == DialogResult.Yes)
                                    _isModifyCheckAmt = true;
                                else
                                {
                                    txtCheckAmount.Text = _OriginalCheckAmount.ToString();
                                    CheckAmount = _OriginalCheckAmount;
                                    frmInsurancePayment_OnRemainingCalculationChanged();
                                    _isModifyCheckAmt = false;
                                }
                            }
                            else
                                _isModifyCheckAmt = true;
                        }
                        else
                            _isModifyCheckAmt = true;
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    if (ogloBilling != null) { ogloBilling.Dispose(); }
                }

            }
          
            return _isModifyCheckAmt;

        }

        private void ValidateDate(object sender, CancelEventArgs e)
        {
            MaskedTextBox mskDate = (MaskedTextBox)sender;
            mskDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
            string strDate = mskDate.Text;
            mskDate.TextMaskFormat = MaskFormat.IncludeLiterals;

            bool _isValid = false;

            if (mskDate != null)
            {
                if (strDate.Length > 0)
                {
                    _isValid = gloDate.IsValid(mskDate.Text);

                    if (!_isValid)
                    {
                        MessageBox.Show("Please enter a valid date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskDate.Clear();
                        mskDate.Focus();
                        e.Cancel = true;
                    }
                }
            }
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

        private void txtClaimRemittanceRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (c1SinglePayment.Rows.Count > 1)
                {
                    // to set the focus to allowed column for the 1st service line.
                    for (int i = 1; i <= c1SinglePayment.Rows.Count; i++)
                    {
                        if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                        { c1SinglePayment.Select(i, COL_ALLOWED); break; }
                    }
                    c1SinglePayment.Focus();
                }
            }
        }
        
        private void MoveCursorOnEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (sender.GetType() == typeof(System.Windows.Forms.Button) && ((Button)sender).Name.ToUpper() == btnSearchInsuranceCompany.Name.ToUpper())
                { txtCheckAmount.Select(); txtCheckAmount.Focus(); }
                if (sender.GetType() == typeof(System.Windows.Forms.TextBox) && ((TextBox)sender).Name.ToUpper() == txtCheckAmount.Name.ToUpper())
                { cmbPayMode.Select(); cmbPayMode.Focus(); }
                else if (sender.GetType() == typeof(System.Windows.Forms.ComboBox) && ((ComboBox)sender).Name.ToUpper() == cmbPayMode.Name.ToUpper())
                { txtCheckNumber.Select(); txtCheckNumber.Focus(); }
                else if (sender.GetType() == typeof(System.Windows.Forms.TextBox) && ((TextBox)sender).Name.ToUpper() == txtCheckNumber.Name.ToUpper())
                { mskCheckDate.Select(); mskCheckDate.Focus(); }
                else if (sender.GetType() == typeof(System.Windows.Forms.MaskedTextBox) && ((MaskedTextBox)sender).Name.ToUpper() == mskCheckDate.Name.ToUpper())
                { txtPayMstNotes.Select(); txtPayMstNotes.Focus(); }
                else if (sender.GetType() == typeof(System.Windows.Forms.TextBox) && ((TextBox)sender).Name.ToUpper() == txtPayMstNotes.Name.ToUpper())
                { if (PatientControl != null) { PatientControl.Focus(); PatientControl.SelectSearchBox(); } }
                else if (sender.GetType() == typeof(System.Windows.Forms.UserControl) && ((UserControl)sender).Name.ToUpper() == PatientControl.Name.ToUpper())
                { txtClaimRemittanceRef.Focus(); }
            }
        }

        private void chkShowCrosswalk_CheckedChanged(object sender, EventArgs e)
        {
            DisplayCrossWalk();
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            pnlSinglePaymentCorrTB.Height = (pnlSinglePayment.Height - c1SinglePaymentTotal.Height) / 2;
        }
      
        #endregion

        #region " Short Cut Menu Click Events "

        private void mnuPayment_Save_Click(object sender, EventArgs e)
        {
            PerformSavePayment();
            
        }
        
        private void mnuPayment_PaymentGrid_Click(object sender, EventArgs e)
        {
            c1SinglePayment.Select();
            c1SinglePayment.Focus();

            if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
            {
                for (int i = 0; i <= c1SinglePayment.Rows.Count - 1; i++)
                {
                    if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                    { c1SinglePayment.Select(i, COL_ALLOWED, true); break; }
                }
            }
        }

        private void mnuPayment_NextServiceLine_Click(object sender, EventArgs e)
        {
            int _currentRowIndex = 0;
            int _nextRowIndex = 0;

            if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
            {
                _currentRowIndex = c1SinglePayment.RowSel;

                for (int rIndex = _currentRowIndex + 1; rIndex < c1SinglePayment.Rows.Count; rIndex++)
                {
                    if (c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) != null)
                    {
                        if ((ColServiceLineType)c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                        { _nextRowIndex = rIndex; break; }
                    }
                }

                if (_nextRowIndex > 0)
                {
                    c1SinglePayment.Select(_nextRowIndex, COL_ALLOWED, true);
                    c1SinglePayment.RowSel = _nextRowIndex;
                    c1SinglePayment.Row = _nextRowIndex;
                }
            }
        }

        private void mnuPayment_PrvServiceLine_Click(object sender, EventArgs e)
        {
            int _currentRowIndex = 0;
            int _prvRowIndex = 0;

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
                    c1SinglePayment.Select(_prvRowIndex, COL_ALLOWED, true);
                    c1SinglePayment.RowSel = _prvRowIndex;
                    c1SinglePayment.Row = _prvRowIndex;
                }
            }
        }

        private void mnuPayment_ReasonCode_Click(object sender, EventArgs e)
        {
            if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 0)
            {
                if (c1SinglePayment.RowSel > 0)
                {
                    if (c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(c1SinglePayment.RowSel, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                    { SelectReasonCodes(); }
                    else
                    { MessageBox.Show("Please select service line to add reason code", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
            }
        }

        #endregion " Short Cut Menu Click Events "

        #region " Methods and Procedures "

        public void DisplayCrossWalk()
        {
            if ((chkShowCrosswalk.Visible) && (chkShowCrosswalk.Checked))
            {
                c1SinglePayment.Cols[COL_CPT_CODE].Visible = false;
                c1SinglePayment.Cols[COL_CROSSWALK_CPT_CODE].Visible = true;
            }
            else
            {
                c1SinglePayment.Cols[COL_CPT_CODE].Visible = true;
                c1SinglePayment.Cols[COL_CROSSWALK_CPT_CODE].Visible = false;
            }
        }

        private void SetCPTCrossWalk()
        {
            Int64 _crossWalkID = gloInsurancePaymentV2.GetCPTCrossWalKID(ContactInsuranceID);

            if (_crossWalkID == 0)
            { chkShowCrosswalk.Visible = false; chkShowCrosswalk.Checked = false; }
            else
            { chkShowCrosswalk.Visible = true; chkShowCrosswalk.Checked = true; }
        }

        private void SetPaymentModeDetails()
        {
            try
            {
                if (cmbPayMode.SelectedIndex >= 0)
                {
                    pnlCredit.Visible = false;
                    txtCheckNumber.Text = "";
                    mskCheckDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    cmbCardType.SelectedIndex = -1;
                    txtCardAuthorizationNo.Text = "";
                    mskCreditExpiryDate.Text = "";
                    pnlCredit.Enabled = false;
                    lblCheckNo.TextAlign = ContentAlignment.MiddleRight;
                    lblCheckDate.TextAlign = ContentAlignment.MiddleRight;
                    txtCheckNumber.MaxLength = 15;
                    if (cmbPayMode.Text.Trim() == PaymentModeV2.Check.ToString())
                    {
                        SelectedPaymentMode = PaymentModeV2.Check;
                        lblCheckNo.Text = "Check# :";
                        lblCheckDate.Text = "Check Date :";
                        mskCheckDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    }
                    else if (cmbPayMode.Text.Trim() == PaymentModeV2.MoneyOrder.ToString())
                    {
                        SelectedPaymentMode = PaymentModeV2.MoneyOrder;
                        lblCheckNo.Text = "MO # :";
                        lblCheckDate.Text = "    MO Date :";
                    }
                    else if (cmbPayMode.Text.Trim() == PaymentModeV2.EFT.ToString())
                    {
                        SelectedPaymentMode = PaymentModeV2.EFT;
                        lblCheckNo.Text = "EFT# :";
                        lblCheckDate.Text = "   EFT Date :";
                    }
                    else if (cmbPayMode.Text.Trim() == PaymentModeV2.Voucher.ToString())
                    {
                        SelectedPaymentMode = PaymentModeV2.Voucher;
                        lblCheckNo.Text = "Voucher# :";
                        lblCheckDate.Text = " Voucher Date :";
                    }
                    else if (cmbPayMode.Text.Trim() == PaymentModeV2.CreditCard.ToString())
                    {
                        pnlCredit.Visible = true;
                        SelectedPaymentMode = PaymentModeV2.CreditCard;
                        //Bug #82934: 00000913: application showing error message while post payment using credit card
                        if (cmbCardType.Items.Count > 0)
                        {
                            cmbCardType.SelectedIndex = 0;
                        }
                        else
                        {
                            MessageBox.Show("Card Type is not present, Please add card type using following steps" + Environment.NewLine + "gloPM  >> Edit >> Billing Configuration >> Credit Card Type", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        
                        lblCheckNo.Text = "Card# :";
                        lblCheckNo.TextAlign = ContentAlignment.MiddleRight;
                        lblCheckDate.Text = "         Date :";
                        lblCheckDate.TextAlign = ContentAlignment.MiddleRight;
                        pnlCredit.Enabled = true;
                        txtCheckNumber.MaxLength = 4;
                        pnlCredit.TabStop = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void ToggleEOB()
        {
            c1SinglePayment.FinishEditing();
            if (tls_btnViewEOB.Tag.ToString() == "ViewEOB")
            {
                pnlMultiplePayment.Visible = true;
                tls_btnViewEOB.Tag = "HideEOB";
                tls_btnViewEOB.Text = "&Hide EOB";
                tls_btnViewEOB.ToolTipText = "Hide Explanation of Benefit";
                splitter1.Enabled = true;
            }
            else
            {
                pnlMultiplePayment.Visible = false;
                tls_btnViewEOB.Tag = "ViewEOB";
                tls_btnViewEOB.Text = "&View EOB";
                tls_btnViewEOB.ToolTipText = "View Explanation of Benefit";
                splitter1.Enabled = false;
            }
        }

        private void SetCloseDate()
        {
            try
            {
                mskCloseDate.Text = gloBilling.gloBilling.GetUserWiseCloseDay(gloGlobal.gloPMGlobal.UserID, CloseDayType.Payment);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
           
        }

        private void SetPaymentPrefixNumber()
        {
            DataTable _dtUniquePaymentPrfixNumber = null;
            try
            {
                lblPaymetNo.Text = "";
                _dtUniquePaymentPrfixNumber = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);
                if (_dtUniquePaymentPrfixNumber != null && _dtUniquePaymentPrfixNumber.Rows.Count > 0)
                {
                    lblPaymetNo.Text = "GPM#" + Convert.ToString(_dtUniquePaymentPrfixNumber.Rows[0]["ID"].ToString());
                }
                    
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_dtUniquePaymentPrfixNumber != null) { _dtUniquePaymentPrfixNumber.Dispose(); }
            }
        }

        private void FillPaymentMode()
        {
            try
            {
                cmbPayMode.Items.Clear();
                cmbPayMode.Items.Add(PaymentMode.Check.ToString());
                cmbPayMode.Items.Add(PaymentMode.CreditCard.ToString());
                cmbPayMode.Items.Add(PaymentMode.MoneyOrder.ToString());
                cmbPayMode.Items.Add(PaymentMode.EFT.ToString());
                cmbPayMode.Items.Add(PaymentMode.Voucher.ToString());

                SelectPaymentMode(PaymentMode.Check);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void SelectPaymentMode(PaymentMode mode)
        {
            try
            {
                for (int i = 0; i <= cmbPayMode.Items.Count - 1; i++)
                {
                    if (cmbPayMode.Items[i].ToString() == mode.ToString())
                    {
                        cmbPayMode.SelectedIndex = i;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void FillPaymentTray()
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
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void FillCreditCards()
        {
            CreditCards oCreditCards = new CreditCards(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            DataTable _dtCards = null;

            try
            {
               // cmbCardType.Items.Clear();
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
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oCreditCards != null) { oCreditCards.Dispose(); }
                //if (_dtCards != null) { _dtCards.Dispose(); }
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
                if (lblPaymentTray.Text != "")
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

        private void FillPaymentGrid()
        {
            gloAccountsV2.PaymentCollection.PaymentInsuranceClaim oPaymentInsuranceClaim = new gloAccountsV2.PaymentCollection.PaymentInsuranceClaim();

            try
            {
                // This method need to be refactor - TO DO
                oPaymentInsuranceClaim = gloInsurancePaymentV2.GetBillingTransaction(PatientControl.ClaimNumber, PatientControl.SubClaimNumber, ContactInsuranceID, PatientInsuranceID);
                LoadPaymentGrid(oPaymentInsuranceClaim);

                DisplayCrossWalk();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oPaymentInsuranceClaim != null) { oPaymentInsuranceClaim.Dispose(); }
            }
        }

        private void LoadPaymentGrid(gloAccountsV2.PaymentCollection.PaymentInsuranceClaim oPaymentInsuranceClaim)
        {
            int _FocusRowIndex = 0;
            int _ClaimStartRowIndex = 0;
             ClsFeeSchedule objClsFeeSchedule=null;
            c1SinglePayment.Redraw = false;
            c1SinglePayment.ScrollBars = ScrollBars.None;

            #region "Fill Billed Transaction"
                       
            DesignPaymentGrid(c1SinglePayment);
            DesignPaymentGrid(c1SinglePaymentTotal);

            if (oPaymentInsuranceClaim != null)
            {
                if (oPaymentInsuranceClaim.CliamLines.Count > 0)
                {
                    #region "Master Data"

                    // ******************************************************************************************************
                    // This line is no more visible, can be deleted the code after checking if anywhere these values are used 

                    c1SinglePayment.Rows.Add();
                    c1SinglePayment.Rows[c1SinglePayment.Rows.Count - 1].AllowEditing = false;

                    c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMDISPNO, ClaimDetails.ClaimDisplayNo);
                    c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_CLAIMNO, oPaymentInsuranceClaim.ClaimNo);
                    c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_SUBCLAIMNO, oPaymentInsuranceClaim.SubClaimNo);
                    c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_TRACK_BILLING_TRANSACTON_ID, oPaymentInsuranceClaim.TrackBillingTrnID);
                    c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_BILLING_TRANSACTON_ID, oPaymentInsuranceClaim.BillingTransactionID);
                    c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_SERVICELINE_TYPE, ColServiceLineType.Claim);
                    c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_ISOPENFORMODIFY, false);
                    c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_PAY_EOBID, 0);
                    c1SinglePayment.SetData(c1SinglePayment.Rows.Count - 1, COL_PATIENTID, oPaymentInsuranceClaim.PatientID);

                    c1SinglePayment.Rows[c1SinglePayment.Rows.Count - 1].Style = c1SinglePayment.Styles["cs_ClaimRowStyle"];
                    _ClaimStartRowIndex = c1SinglePayment.Rows.Count - 1;
                    c1SinglePayment.SetData(_ClaimStartRowIndex, COL_CELLRANGE_R1, c1SinglePayment.Rows.Count - 1);

                    c1SinglePayment.Rows[c1SinglePayment.Rows.Count - 1].Visible = false;

                    // ******************************************************************************************************

                    #endregion

                    #region "Service Lines"

                    for (int j = 0; j <= oPaymentInsuranceClaim.CliamLines.Count - 1; j++)
                    {
                        c1SinglePayment.Rows.Add();

                        int _RowIndex = c1SinglePayment.Rows.Count - 1;

                        if (_FocusRowIndex == 0)
                        { _FocusRowIndex = _RowIndex; }

                        c1SinglePayment.SetData(_RowIndex, COL_GENERAL, "");
                        c1SinglePayment.SetData(_RowIndex, COL_PATIENTID, oPaymentInsuranceClaim.CliamLines[j].PatientID);
                        c1SinglePayment.SetData(_RowIndex, COL_PATIENTNAME, "");

                        c1SinglePayment.SetData(_RowIndex, COL_CLAIMNO, oPaymentInsuranceClaim.CliamLines[j].ClaimNumber);
                        c1SinglePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_ID, oPaymentInsuranceClaim.CliamLines[j].BLTransactionID);
                        c1SinglePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_DETAILID, oPaymentInsuranceClaim.CliamLines[j].BLTransactionDetailID);
                        c1SinglePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_LINENO, oPaymentInsuranceClaim.CliamLines[j].BLTransactionLineNo);
                        
                        c1SinglePayment.SetData(_RowIndex, COL_SUBCLAIMNO, oPaymentInsuranceClaim.CliamLines[j].SubClaimNumber);
                        c1SinglePayment.SetData(_RowIndex, COL_TRACK_BILLING_TRANSACTON_ID, oPaymentInsuranceClaim.CliamLines[j].TrackBLTransactionID);
                        c1SinglePayment.SetData(_RowIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID, oPaymentInsuranceClaim.CliamLines[j].TrackBLTransactionDetailID);
                        c1SinglePayment.SetData(_RowIndex, COL_TRACK_BILLING_TRANSACTON_LINENO, oPaymentInsuranceClaim.CliamLines[j].TrackBLTransactionLineNo);

                        c1SinglePayment.SetData(_RowIndex, COL_PAYMENT_NO, j + 1);
                        c1SinglePayment.SetData(_RowIndex, COL_PAY_DATE, "");
                        c1SinglePayment.SetData(_RowIndex, COL_PAY_EOBPAYMENTID, ""); //TO DO
                        c1SinglePayment.SetData(_RowIndex, COL_PAY_EOBID, 0);
                        c1SinglePayment.SetData(_RowIndex, COL_PAY_EOBDTLID, 0);
                        c1SinglePayment.SetData(_RowIndex, COL_PAY_EOBPAYMENTDTLID, 0);

                        c1SinglePayment.SetData(_RowIndex, COL_DOS_FROM, gloDateMaster.gloDate.DateAsDateString(oPaymentInsuranceClaim.CliamLines[j].DOSFrom));
                        c1SinglePayment.SetData(_RowIndex, COL_DOS_TO, gloDateMaster.gloDate.DateAsDateString(oPaymentInsuranceClaim.CliamLines[j].DOSTo));

                        c1SinglePayment.SetData(_RowIndex, COL_CPT_CODE, oPaymentInsuranceClaim.CliamLines[j].CPTCode);
                        c1SinglePayment.SetData(_RowIndex, COL_CPT_DESCRIPTON, oPaymentInsuranceClaim.CliamLines[j].CPTDescription);
                        c1SinglePayment.SetData(_RowIndex, COL_MODIFIER, oPaymentInsuranceClaim.CliamLines[j].Modifier);

                        c1SinglePayment.SetData(_RowIndex, COL_CROSSWALK_CPT_CODE, oPaymentInsuranceClaim.CliamLines[j].CrossWalkCPTCode);
                        c1SinglePayment.SetData(_RowIndex, COL_CROSSWALK_CPT_DESC, oPaymentInsuranceClaim.CliamLines[j].CrossWalkCPTDescription);

                        c1SinglePayment.SetData(_RowIndex, COL_CHARGE, oPaymentInsuranceClaim.CliamLines[j].Charges);
                        c1SinglePayment.SetData(_RowIndex, COL_UNIT, gloCharges.FormatNumber(oPaymentInsuranceClaim.CliamLines[j].Unit));
                        c1SinglePayment.SetData(_RowIndex, COL_TOTALCHARGE, oPaymentInsuranceClaim.CliamLines[j].TotalCharges);

                        c1SinglePayment.SetData(_RowIndex, COL_PAYMENT, null);

                        if (oPaymentInsuranceClaim.CliamLines[j].Last_allowed > 0)
                        {
                          //  c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, oPaymentInsuranceClaim.CliamLines[j].Last_allowed);
                            c1SinglePayment.SetData(_RowIndex, COL_WRITEOFF, oPaymentInsuranceClaim.CliamLines[j].Last_writeoff);

                        }
                        else
                        {
                          //  c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, oPaymentInsuranceClaim.CliamLines[j].Allowed);
                            if ((oPaymentInsuranceClaim.CliamLines[j].TotalCharges - oPaymentInsuranceClaim.CliamLines[j].Allowed) == 0)
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_WRITEOFF, null);
                            }
                            else
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_WRITEOFF, oPaymentInsuranceClaim.CliamLines[j].TotalCharges - oPaymentInsuranceClaim.CliamLines[j].Allowed);
                            }
                        }
                        c1SinglePayment.SetData(_RowIndex, COL_WRITEOFF, null);

                        c1SinglePayment.SetData(_RowIndex, COL_COPAY, null);
                        c1SinglePayment.SetData(_RowIndex, COL_DEDUCTIBLE, null);
                        c1SinglePayment.SetData(_RowIndex, COL_COINSURANCE, null);
                        c1SinglePayment.SetData(_RowIndex, COL_WITHHOLD, null);

                        c1SinglePayment.SetData(_RowIndex, COL_PREVPAID, oPaymentInsuranceClaim.CliamLines[j].LinePaidAmount);
                        c1SinglePayment.SetData(_RowIndex, COL_BALANCE, oPaymentInsuranceClaim.CliamLines[j].LineBalance);
                        c1SinglePayment.SetData(_RowIndex, COL_LINE_DB_BALANCE, oPaymentInsuranceClaim.CliamLines[j].LineBalance);

                        c1SinglePayment.SetData(_RowIndex, COL_SERVICELINE_TYPE, ColServiceLineType.ServiceLine);
                        c1SinglePayment.SetData(_RowIndex, COL_ISOPENFORMODIFY, "");
                        c1SinglePayment.SetData(_RowIndex, COL_PAY_CLINICID, 0);
                        c1SinglePayment.SetData(_RowIndex, COL_PAY_LINESTATUS, "");

                        c1SinglePayment.SetData(_RowIndex, COL_LAST_ALLOWED, oPaymentInsuranceClaim.CliamLines[j].Last_allowed);
                        c1SinglePayment.SetData(_RowIndex, COL_LAST_PAYMENT, oPaymentInsuranceClaim.CliamLines[j].Last_payment);
                        c1SinglePayment.SetData(_RowIndex, COL_LAST_WRITEOFF, oPaymentInsuranceClaim.CliamLines[j].Last_writeoff);
                        c1SinglePayment.SetData(_RowIndex, COL_LAST_COPAY, oPaymentInsuranceClaim.CliamLines[j].Last_copay);
                        c1SinglePayment.SetData(_RowIndex, COL_LAST_DEDUCTIBLE, oPaymentInsuranceClaim.CliamLines[j].Last_deductible);
                        c1SinglePayment.SetData(_RowIndex, COL_LAST_COINSURANCE, oPaymentInsuranceClaim.CliamLines[j].Last_coinsurance);
                        c1SinglePayment.SetData(_RowIndex, COL_LAST_WITHHOLD, oPaymentInsuranceClaim.CliamLines[j].Last_withhold);
                        c1SinglePayment.SetData(_RowIndex, COL_ISCORRECTION, oPaymentInsuranceClaim.CliamLines[j].Iscorrection);

                        c1SinglePayment.SetData(_RowIndex, COL_ISSPLITTED, oPaymentInsuranceClaim.CliamLines[j].IsSplitted);
                        c1SinglePayment.SetData(_RowIndex, COL_LASTEOBPAYMENTID, oPaymentInsuranceClaim.CliamLines[j].LastEOBPaymentId);
                        c1SinglePayment.SetData(_RowIndex, COL_PatientPaidAmount, oPaymentInsuranceClaim.CliamLines[j].PatientPaidAmount);

                        Boolean bHasAllowedAmt = false;
                        
                        decimal _dAllowedAmount = 0;
                        if (oPaymentInsuranceClaim.CliamLines[j].IsNullAllowed)
                        {

                            objClsFeeSchedule = new ClsFeeSchedule(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                            _dAllowedAmount = objClsFeeSchedule.GetAllowedAmount(oPaymentInsuranceClaim.BillingTransactionID, oPaymentInsuranceClaim.CliamLines[j].CPTCode, oPaymentInsuranceClaim.CliamLines[j].Modifier, oPaymentInsuranceClaim.FacilityType, ref bHasAllowedAmt, oPaymentInsuranceClaim.CliamLines[j].DOSFrom);
                            if (bHasAllowedAmt)
                            {
                                _dAllowedAmount = _dAllowedAmount * Convert.ToDecimal(oPaymentInsuranceClaim.CliamLines[j].Unit);
                                c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, _dAllowedAmount);                                
                            }
                            else
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, null);
                                bHasAllowedAmt = false;
                            }
                        }
                        else
                        {   
                            _dAllowedAmount = oPaymentInsuranceClaim.CliamLines[j].Allowed;
                            bHasAllowedAmt = true;
                        }
                       
                        #region "Allow Editing"

                        if (PatientControl != null && PatientControl.ClaimNumber > 0 && PatientControl.PatientID > 0 && PatientControl.SelectedInsuranceParty > 1)
                        {
                            //7022 items:making allowed amount column editable for secondary & tertiary insurances.
                            c1SinglePayment.SetCellStyle(_RowIndex, COL_ALLOWED, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                            c1SinglePayment.Cols[COL_ALLOWED].AllowEditing = true ;
                            c1SinglePayment.SetData(_RowIndex, COL_WRITEOFF, null);
                            c1SinglePayment.SetData(_RowIndex, COL_WITHHOLD, null);
                        }
                        else
                        {
                            c1SinglePayment.SetCellStyle(_RowIndex, COL_ALLOWED, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                        }


                        c1SinglePayment.SetCellStyle(_RowIndex, COL_PAYMENT, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                        c1SinglePayment.SetCellStyle(_RowIndex, COL_WRITEOFF, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                        c1SinglePayment.SetCellStyle(_RowIndex, COL_COPAY, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                        c1SinglePayment.SetCellStyle(_RowIndex, COL_DEDUCTIBLE, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                        c1SinglePayment.SetCellStyle(_RowIndex, COL_COINSURANCE, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                        c1SinglePayment.SetCellStyle(_RowIndex, COL_WITHHOLD, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                        c1SinglePayment.SetCellStyle(_RowIndex, COL_NEXT, c1SinglePayment.Styles["cs_EditableActionStatus"]);
                        c1SinglePayment.SetCellStyle(_RowIndex, COL_PARTY, c1SinglePayment.Styles["cs_EditableParty"]);
                        c1SinglePayment.SetCellStyle(_RowIndex, COL_REASON, c1SinglePayment.Styles["cs_EditableReason"]);

                        c1SinglePayment.SetCellStyle(_RowIndex, COL_WRITEOFF, c1SinglePayment.Styles["cs_NonEditableCurrencyStyle"]);
                        c1SinglePayment.SetCellStyle(_RowIndex, COL_COPAY, c1SinglePayment.Styles["cs_NonEditableCurrencyStyle"]);
                        c1SinglePayment.SetCellStyle(_RowIndex, COL_DEDUCTIBLE, c1SinglePayment.Styles["cs_NonEditableCurrencyStyle"]);
                        c1SinglePayment.SetCellStyle(_RowIndex, COL_COINSURANCE, c1SinglePayment.Styles["cs_NonEditableCurrencyStyle"]);
                        c1SinglePayment.SetCellStyle(_RowIndex, COL_WITHHOLD, c1SinglePayment.Styles["cs_NonEditableCurrencyStyle"]);

                        #endregion


                        //added To show Last Payable amounts in Corresponding Cells.
                        if (oPaymentInsuranceClaim.CliamLines[j].Iscorrection)
                        {
                            //if (!oPaymentInsuranceClaim.CliamLines[j].IsLast_allowedNull)
                            //{
                            //    c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, oPaymentInsuranceClaim.CliamLines[j].Last_allowed);
                            //}
                            //else
                            //{
                            //    c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, null);
                            //}
                            if (bHasAllowedAmt)
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, _dAllowedAmount);
                            }
                            else
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, null);
                            }
                            if (!oPaymentInsuranceClaim.CliamLines[j].IsLast_paymentNull)
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_PAYMENT, oPaymentInsuranceClaim.CliamLines[j].Last_payment);
                            }
                            else
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_PAYMENT, null);
                            }

                            if (!oPaymentInsuranceClaim.CliamLines[j].IsLast_writeoffNull)
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_WRITEOFF, oPaymentInsuranceClaim.CliamLines[j].Last_writeoff);
                            }
                            else
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_WRITEOFF, null);
                            }

                            if (!oPaymentInsuranceClaim.CliamLines[j].IsLast_copayNull)
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_COPAY, oPaymentInsuranceClaim.CliamLines[j].Last_copay);
                            }
                            else
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_COPAY, null);
                            }

                            if (!oPaymentInsuranceClaim.CliamLines[j].IsLast_deductibleNull)
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_DEDUCTIBLE, oPaymentInsuranceClaim.CliamLines[j].Last_deductible);
                            }
                            else
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_DEDUCTIBLE, null);
                            }

                            if (!oPaymentInsuranceClaim.CliamLines[j].IsLast_coinsuranceNull)
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_COINSURANCE, oPaymentInsuranceClaim.CliamLines[j].Last_coinsurance);
                            }
                            else
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_COINSURANCE, null);
                            }

                            if (!oPaymentInsuranceClaim.CliamLines[j].IsLast_withholdNull)
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_WITHHOLD, oPaymentInsuranceClaim.CliamLines[j].Last_withhold);
                            }
                            else
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_WITHHOLD, null);
                            }
                        }
                        else
                        {
                            //if (oPaymentInsuranceClaim.CliamLines[j].Last_allowed > 0)
                            //{
                            //    c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, oPaymentInsuranceClaim.CliamLines[j].Last_allowed);
                            //}
                            //else
                            //{
                            //    c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, oPaymentInsuranceClaim.CliamLines[j].Allowed);
                            //}
                            if (bHasAllowedAmt)
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, _dAllowedAmount);
                            }
                            else
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, null);
                            }
                            c1SinglePayment.SetData(_RowIndex, COL_PAYMENT, null);
                            c1SinglePayment.SetData(_RowIndex, COL_WRITEOFF, null);
                            c1SinglePayment.SetData(_RowIndex, COL_COPAY, null);
                            c1SinglePayment.SetData(_RowIndex, COL_DEDUCTIBLE, null);
                            c1SinglePayment.SetData(_RowIndex, COL_COINSURANCE, null);
                            c1SinglePayment.SetData(_RowIndex, COL_WITHHOLD, null);

                        }
                        //*****
                        if (objClsFeeSchedule != null)
                        {
                            objClsFeeSchedule.Dispose();
                        }
                    }
                    c1SinglePayment.SetData(_ClaimStartRowIndex, COL_CELLRANGE_R2, c1SinglePayment.Rows.Count - 1);

                    #endregion

                    #region "Calculate Total of Claims"

                    c1SinglePaymentTotal.SetData(0, COL_CHARGE, CalculateSinglePaymentTotal(COL_CHARGE));
                    c1SinglePaymentTotal.SetData(0, COL_ALLOWED, CalculateSinglePaymentTotal(COL_ALLOWED));
                    c1SinglePaymentTotal.SetData(0, COL_PAYMENT, CalculateSinglePaymentTotal(COL_PAYMENT));

                    c1SinglePaymentTotal.SetData(0, COL_WRITEOFF, CalculateSinglePaymentTotal(COL_WRITEOFF));

                    c1SinglePaymentTotal.SetData(0, COL_COPAY, CalculateSinglePaymentTotal(COL_COPAY));
                    c1SinglePaymentTotal.SetData(0, COL_DEDUCTIBLE, CalculateSinglePaymentTotal(COL_DEDUCTIBLE));
                    c1SinglePaymentTotal.SetData(0, COL_COINSURANCE, CalculateSinglePaymentTotal(COL_COINSURANCE));
                    c1SinglePaymentTotal.SetData(0, COL_WITHHOLD, CalculateSinglePaymentTotal(COL_WITHHOLD));

                    c1SinglePaymentTotal.SetData(0, COL_PREVPAID, CalculateSinglePaymentTotal(COL_PREVPAID));
                    c1SinglePaymentTotal.SetData(0, COL_BALANCE, CalculateSinglePaymentTotal(COL_BALANCE));

                    #endregion
                }
            }

            #endregion

            #region "Set Index"

            if (_FocusRowIndex > 0)
            {
                for (int t = COL_CHARGE; t <= COL_BALANCE; t++)
                {
                    decimal _TotAmount = 0;

                    for (int r = 1; r <= c1SinglePayment.Rows.Count - 1; r++)
                    {
                        if (c1SinglePayment.GetData(r, t) != null && c1SinglePayment.GetData(r, t).ToString() != "")
                        {
                            _TotAmount = _TotAmount + Convert.ToDecimal(c1SinglePayment.GetData(r, t).ToString());
                        }
                    }
                    c1SinglePaymentTotal.SetData(0, t, _TotAmount);
                }

            }

            #endregion
            if (objClsFeeSchedule != null)
            {
                objClsFeeSchedule.Dispose();
            }
            c1SinglePayment.Redraw = true;
            c1SinglePayment.ScrollBars = ScrollBars.Vertical;
            c1SinglePayment.TabStop = true;
        }

        private void FillEOBPayments(Int64 EOBPaymentID, Int64 EOBID)
        {
            DataTable _dtEOBPayments = new DataTable();

            try
            {
                _dtEOBPayments = gloInsurancePaymentV2.GetEOBPayment(EOBPaymentID, EOBID);

                if (_dtEOBPayments != null && _dtEOBPayments.Rows.Count > 0)
                {
                    if (EOBID > 0)
                    {
                        LoadEOBPaymentsV2(_dtEOBPayments, true);
                    }
                    else
                    {
                        LoadEOBPaymentsV2(_dtEOBPayments);
                    }
                }
                else
                {
                    if (!IsMultiPaymentGridDesigned) { DesignPaymentGrid(c1MultiplePayment); IsMultiPaymentGridDesigned = true; }
                    if (!IsMultiPaymentTotalGridDesigned) { DesignPaymentGrid(c1MultiplePaymentTotal); IsMultiPaymentTotalGridDesigned = true; }
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            
        }

        private void LoadEOBPayments(DataTable dtEOBPayments)
        {
            int _RowIndex = 0;
            string _sModifiers = string.Empty;
            try
            {
                if (!IsMultiPaymentGridDesigned){ DesignPaymentGrid(c1MultiplePayment); IsMultiPaymentGridDesigned = true; }
                if (!IsMultiPaymentTotalGridDesigned) { DesignPaymentGrid(c1MultiplePaymentTotal); IsMultiPaymentTotalGridDesigned = true; }

                c1MultiplePayment.Rows.Count = 1;
                c1MultiplePaymentTotal.Rows.Count = 1;

                

                foreach (DataRow row in dtEOBPayments.Rows)
                {
                    c1MultiplePayment.Rows.Add();
                    _RowIndex = c1MultiplePayment.Rows.Count - 1;
                   
                    _sModifiers = "";
                    if (Convert.ToString(row["sMod1Code"]) != "")
                    {
                        _sModifiers += (_sModifiers == string.Empty ? Convert.ToString(row["sMod1Code"]) : "," + Convert.ToString(row["sMod1Code"]));
                    }
                    if (Convert.ToString(row["sMod2Code"]) != "")
                    {
                        _sModifiers += (_sModifiers == string.Empty ? Convert.ToString(row["sMod2Code"]) : "," + Convert.ToString(row["sMod2Code"]));
                    }
                    if (Convert.ToString(row["sMod3Code"]) != "")
                    {
                        _sModifiers += (_sModifiers == string.Empty ? Convert.ToString(row["sMod3Code"]) : "," + Convert.ToString(row["sMod3Code"]));
                    }
                    if (Convert.ToString(row["sMod4Code"]) != "")
                    {
                        _sModifiers += (_sModifiers == string.Empty ? Convert.ToString(row["sMod4Code"]) : "," + Convert.ToString(row["sMod4Code"]));
                    }

                    c1MultiplePayment.SetData(_RowIndex, COL_PAY_EOBPAYMENTID, Convert.ToInt64(row["nCreditID"]));

                    if (Convert.ToString(row["PatientName"]).Trim() != "Reserve")
                    {
                        c1MultiplePayment.SetData(_RowIndex, COL_PAY_EOBID, Convert.ToInt64(row["nEOBID"]));
                        c1MultiplePayment.SetData(_RowIndex, COL_DOS_FROM, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(row["nDOSFrom"])).ToString("MM/dd/yyyy"));
                    }
                    else
                    {
                        c1MultiplePayment.SetData(_RowIndex, COL_GENERAL, Convert.ToInt64(row["nEOBID"]));
                        c1MultiplePayment.SetData(_RowIndex, COL_DOS_FROM, null);
                    }

                    c1MultiplePayment.SetData(_RowIndex, COL_PATIENTID, Convert.ToInt64(row["nPatientID"]));
                    c1MultiplePayment.SetData(_RowIndex, COL_PATIENTNAME, Convert.ToString(row["PatientName"]));
                    c1MultiplePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_ID, Convert.ToInt64(row["nBillingTransactionID"]));
                    c1MultiplePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_DETAILID, Convert.ToInt64(row["nBillingTransactionDetailID"]));
                    c1MultiplePayment.SetData(_RowIndex, COL_MODIFIER, _sModifiers);
                    c1MultiplePayment.SetData(_RowIndex, COL_CLAIMDISPNO, Convert.ToString(row["ClaimNumber"]));                  
                    c1MultiplePayment.SetData(_RowIndex, COL_CPT_CODE, Convert.ToString(row["sCPTCode"]));

                    if (row["dTotalCharges"] != DBNull.Value) { c1MultiplePayment.SetData(_RowIndex, COL_TOTALCHARGE, Convert.ToDecimal(row["dTotalCharges"])); }
                    else { c1MultiplePayment.SetData(_RowIndex, COL_TOTALCHARGE, null); }

                    if (row["dAllowed"] != DBNull.Value && Convert.ToDecimal(row["dAllowed"]) != 0) { c1MultiplePayment.SetData(_RowIndex, COL_ALLOWED, Convert.ToDecimal(row["dAllowed"])); }
                    else { c1MultiplePayment.SetData(_RowIndex, COL_ALLOWED, null); }

                    if (row["dPayment"] != DBNull.Value && Convert.ToDecimal(row["dPayment"]) != 0) { c1MultiplePayment.SetData(_RowIndex, COL_PAYMENT, Convert.ToDecimal(row["dPayment"])); }
                    else { c1MultiplePayment.SetData(_RowIndex, COL_PAYMENT, null); }

                    if (row["dWriteOff"] != DBNull.Value && Convert.ToDecimal(row["dWriteOff"]) != 0) { c1MultiplePayment.SetData(_RowIndex, COL_WRITEOFF, Convert.ToDecimal(row["dWriteOff"])); }
                    else { c1MultiplePayment.SetData(_RowIndex, COL_WRITEOFF, null); }

                    if (row["dCopay"] != DBNull.Value && Convert.ToDecimal(row["dCopay"]) != 0) { c1MultiplePayment.SetData(_RowIndex, COL_COPAY, Convert.ToDecimal(row["dCopay"])); }
                    else { c1MultiplePayment.SetData(_RowIndex, COL_COPAY, null); }

                    if (row["dDeductible"] != DBNull.Value && Convert.ToDecimal(row["dDeductible"]) != 0) { c1MultiplePayment.SetData(_RowIndex, COL_DEDUCTIBLE, Convert.ToDecimal(row["dDeductible"])); }
                    else { c1MultiplePayment.SetData(_RowIndex, COL_DEDUCTIBLE, null); }

                    if (row["dCoInsurance"] != DBNull.Value && Convert.ToDecimal(row["dCoInsurance"]) != 0) { c1MultiplePayment.SetData(_RowIndex, COL_COINSURANCE, Convert.ToDecimal(row["dCoInsurance"])); }
                    else { c1MultiplePayment.SetData(_RowIndex, COL_COINSURANCE, null); }

                    if (row["dWithhold"] != DBNull.Value && Convert.ToDecimal(row["dWithhold"]) != 0) { c1MultiplePayment.SetData(_RowIndex, COL_WITHHOLD, Convert.ToDecimal(row["dWithhold"])); }
                    else { c1MultiplePayment.SetData(_RowIndex, COL_WITHHOLD, null); }

                    if (row["Other"] != DBNull.Value && Convert.ToDecimal(row["Other"]) != 0) { c1MultiplePayment.SetData(_RowIndex, COL_REASON, Convert.ToDecimal(row["Other"])); }
                    else { c1MultiplePayment.SetData(_RowIndex, COL_REASON, null); }

                    c1MultiplePayment.SetData(_RowIndex, COL_SERVICELINE_TYPE, ColServiceLineType.ServiceLine);                  
                }

                c1MultiplePayment.Cols[COL_PATIENTNAME].Visible = true;
                c1MultiplePayment.Cols[COL_PATIENTNAME].Width = 200;

                c1MultiplePayment.Cols[COL_PREVPAID].Visible = false;
                c1MultiplePayment.Cols[COL_BALANCE].Visible = false;
                c1MultiplePayment.Cols[COL_NEXT].Visible = false;
                c1MultiplePayment.Cols[COL_PARTY].Visible = false;
                c1MultiplePayment.Cols[COL_REASON].Visible = true;

                #region "Calculate Total of Claims"

                c1MultiplePaymentTotal.SetData(0, COL_CHARGE, CalculateMultiplePaymentTotal(COL_CHARGE));
                c1MultiplePaymentTotal.SetData(0, COL_TOTALCHARGE, CalculateMultiplePaymentTotal(COL_TOTALCHARGE));
                c1MultiplePaymentTotal.SetData(0, COL_ALLOWED, CalculateMultiplePaymentTotal(COL_ALLOWED));

                c1MultiplePaymentTotal.SetData(0, COL_PAYMENT, CalculateMultiplePaymentTotal(COL_PAYMENT));
                c1MultiplePaymentTotal.SetData(0, COL_WRITEOFF, CalculateMultiplePaymentTotal(COL_WRITEOFF));
                c1MultiplePaymentTotal.SetData(0, COL_COPAY, CalculateMultiplePaymentTotal(COL_COPAY));
                c1MultiplePaymentTotal.SetData(0, COL_DEDUCTIBLE, CalculateMultiplePaymentTotal(COL_DEDUCTIBLE));
                c1MultiplePaymentTotal.SetData(0, COL_COINSURANCE, CalculateMultiplePaymentTotal(COL_COINSURANCE));
                c1MultiplePaymentTotal.SetData(0, COL_WITHHOLD, CalculateMultiplePaymentTotal(COL_WITHHOLD));
                c1MultiplePaymentTotal.SetData(0, COL_REASON, CalculateMultiplePaymentTotal(COL_REASON));

                #endregion

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void LoadEOBPaymentsV2(DataTable dtEOBPayments)
        {

            object _COL_TOTALCHARGE_SUM = 0;
            object _COL_ALLOWED_SUM = 0;
            object _COL_PAYMENT_SUM = 0;
            object _COL_WRITEOFF_SUM = 0;
            object _COL_COPAY_SUM = 0;
            object _COL_DEDUCTIBLE_SUM = 0;
            object _COL_COINSURANCE_SUM = 0;
            object _COL_WITHHOLD_SUM = 0;
            object _COL_REASON_SUM = 0;

            try
            {
                if (!IsMultiPaymentTotalGridDesigned) { DesignPaymentGrid(c1MultiplePaymentTotal); IsMultiPaymentTotalGridDesigned = true; }
               // c1MultiplePayment.Clear();
                c1MultiplePayment.DataSource = null;
                c1MultiplePayment.Rows.Count = 1;
                c1MultiplePaymentTotal.Rows.Count = 1;

                if (dtEOBPayments != null && dtEOBPayments.Rows.Count > 0)
                {
                    c1MultiplePayment.BeginUpdate();
                    c1MultiplePayment.DataSource = dtEOBPayments.Copy();
                    c1MultiplePayment.EndUpdate();

                    _COL_TOTALCHARGE_SUM = dtEOBPayments.Compute("Sum(COL_TOTALCHARGE)", "COL_TOTALCHARGE IS NOT NULL");
                    _COL_ALLOWED_SUM = dtEOBPayments.Compute("Sum(COL_ALLOWED)", "COL_ALLOWED IS NOT NULL");
                    _COL_PAYMENT_SUM = dtEOBPayments.Compute("Sum(COL_PAYMENT)", "COL_PAYMENT IS NOT NULL");
                    _COL_WRITEOFF_SUM = dtEOBPayments.Compute("Sum(COL_WRITEOFF)", "COL_WRITEOFF IS NOT NULL");
                    _COL_COPAY_SUM = dtEOBPayments.Compute("Sum(COL_COPAY)", "COL_COPAY IS NOT NULL");
                    _COL_DEDUCTIBLE_SUM = dtEOBPayments.Compute("Sum(COL_DEDUCTIBLE)", "COL_DEDUCTIBLE IS NOT NULL");
                    _COL_COINSURANCE_SUM = dtEOBPayments.Compute("Sum(COL_COINSURANCE)", "COL_COINSURANCE IS NOT NULL");
                    _COL_WITHHOLD_SUM = dtEOBPayments.Compute("Sum(COL_WITHHOLD)", "COL_WITHHOLD IS NOT NULL");
                    _COL_REASON_SUM = dtEOBPayments.Compute("Sum(COL_REASON)", "COL_REASON IS NOT NULL");

                }

                //if (!IsMultiPaymentGridDesigned) 
                { DesignMultiplePaymentGrid(c1MultiplePayment); IsMultiPaymentGridDesigned = true; }
                

                #region "Calculate Total of Claims"

                //c1MultiplePaymentTotal.SetData(0, COL_CHARGE, CalculateMultiplePaymentTotal(c1MultiplePayment.Cols["COL_CHARGE"].Index));
                if (Convert.ToString(_COL_TOTALCHARGE_SUM).Trim() != "")
                { c1MultiplePaymentTotal.SetData(0, COL_TOTALCHARGE, Convert.ToDecimal(_COL_TOTALCHARGE_SUM)); }
                else
                { c1MultiplePaymentTotal.SetData(0, COL_TOTALCHARGE, Convert.ToDecimal("0.00")); }

                if (Convert.ToString(_COL_ALLOWED_SUM).Trim() != "")
                { c1MultiplePaymentTotal.SetData(0, COL_ALLOWED, Convert.ToDecimal(_COL_ALLOWED_SUM)); }
                else
                { c1MultiplePaymentTotal.SetData(0, COL_ALLOWED, Convert.ToDecimal("0.00")); }

                if (Convert.ToString(_COL_PAYMENT_SUM).Trim() != "")
                { c1MultiplePaymentTotal.SetData(0, COL_PAYMENT, Convert.ToDecimal(_COL_PAYMENT_SUM)); }
                else
                { c1MultiplePaymentTotal.SetData(0, COL_PAYMENT, Convert.ToDecimal("0.00")); }

                if (Convert.ToString(_COL_WRITEOFF_SUM).Trim() != "")
                { c1MultiplePaymentTotal.SetData(0, COL_WRITEOFF, Convert.ToDecimal(_COL_WRITEOFF_SUM)); }
                else
                { c1MultiplePaymentTotal.SetData(0, COL_WRITEOFF, Convert.ToDecimal("0.00")); }

                if (Convert.ToString(_COL_COPAY_SUM).Trim() != "")
                { c1MultiplePaymentTotal.SetData(0, COL_COPAY, Convert.ToDecimal(_COL_COPAY_SUM)); }
                else
                { c1MultiplePaymentTotal.SetData(0, COL_COPAY, Convert.ToDecimal("0.00")); }

                if (Convert.ToString(_COL_DEDUCTIBLE_SUM).Trim() != "")
                { c1MultiplePaymentTotal.SetData(0, COL_DEDUCTIBLE, Convert.ToDecimal(_COL_DEDUCTIBLE_SUM)); }
                else
                { c1MultiplePaymentTotal.SetData(0, COL_DEDUCTIBLE, Convert.ToDecimal("0.00")); }

                if (Convert.ToString(_COL_COINSURANCE_SUM).Trim() != "")
                { c1MultiplePaymentTotal.SetData(0, COL_COINSURANCE, Convert.ToDecimal(_COL_COINSURANCE_SUM)); }
                else
                { c1MultiplePaymentTotal.SetData(0, COL_COINSURANCE, Convert.ToDecimal("0.00")); }

                if (Convert.ToString(_COL_WITHHOLD_SUM).Trim() != "")
                { c1MultiplePaymentTotal.SetData(0, COL_WITHHOLD, Convert.ToDecimal(_COL_WITHHOLD_SUM)); }
                else
                { c1MultiplePaymentTotal.SetData(0, COL_WITHHOLD, Convert.ToDecimal("0.00")); }

                if (Convert.ToString(_COL_REASON_SUM).Trim() != "")
                { c1MultiplePaymentTotal.SetData(0, COL_REASON, Convert.ToDecimal(_COL_REASON_SUM)); }
                else
                { c1MultiplePaymentTotal.SetData(0, COL_REASON, Convert.ToDecimal("0.00")); }
                #endregion

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                _COL_TOTALCHARGE_SUM = null;
                _COL_ALLOWED_SUM = null;
                _COL_PAYMENT_SUM = null;
                _COL_WRITEOFF_SUM = null;
                _COL_COPAY_SUM = null;
                _COL_DEDUCTIBLE_SUM = null;
                _COL_COINSURANCE_SUM = null;
                _COL_WITHHOLD_SUM = null;
                _COL_REASON_SUM = null;

                if (dtEOBPayments != null) { dtEOBPayments.Dispose(); dtEOBPayments = null; }
                
            }
        }

        private void LoadEOBPaymentsV2(DataTable dtEOBPayments, bool _isEOBSave)
        {
            DataTable _dtDataSource = null;
            object _COL_TOTALCHARGE_SUM = 0;
            object _COL_ALLOWED_SUM = 0;
            object _COL_PAYMENT_SUM = 0;
            object _COL_WRITEOFF_SUM = 0;
            object _COL_COPAY_SUM = 0;
            object _COL_DEDUCTIBLE_SUM = 0;
            object _COL_COINSURANCE_SUM = 0;
            object _COL_WITHHOLD_SUM = 0;
            object _COL_REASON_SUM = 0;

            try
            {
                if (!IsMultiPaymentTotalGridDesigned) { DesignPaymentGrid(c1MultiplePaymentTotal); IsMultiPaymentTotalGridDesigned = true; }

                c1MultiplePayment.Redraw = false;
                c1MultiplePaymentTotal.Redraw = false;

                if (dtEOBPayments != null && dtEOBPayments.Rows.Count > 0)
                {
                    try
                    {
                        _dtDataSource = ((DataTable)c1MultiplePayment.DataSource);
                    }
                    catch 
                    {
                        _dtDataSource = null;
                    }

                    if (_dtDataSource != null && _dtDataSource.Rows.Count > 0)
                    {

                        DataRow[] _dataRowReservesEOBPayments = dtEOBPayments.Select("COL_PATIENTNAME = 'Reserve'");
                        DataRow[] _dataRowReservesDataSource = _dtDataSource.Select("COL_PATIENTNAME = 'Reserve'");

                        if (_dataRowReservesDataSource != null && _dataRowReservesDataSource.Length > 0)
                        {
                            foreach (DataRow sourceRow in _dataRowReservesDataSource)
                            {
                                if (Convert.ToString(sourceRow["COL_GENERAL"]).Trim() != "")
                                {
                                    _dtDataSource.Rows.Remove(sourceRow);
                                    
                                }
                            }
                            dtEOBPayments.AcceptChanges();
                        }

                        //if (_dataRowReservesDataSource != null && _dataRowReservesDataSource.Length > 0)
                        //{
                        //    if (_dataRowReservesEOBPayments != null && _dataRowReservesEOBPayments.Length > 0)
                        //    {
                        //        foreach (DataRow sourceRow in _dataRowReservesDataSource)
                        //        {
                        //            foreach (DataRow newRow in _dataRowReservesEOBPayments)
                        //            {
                        //                if (Convert.ToInt64(sourceRow["COL_GENERAL"]) == Convert.ToInt64(newRow["COL_GENERAL"]))
                        //                {
                        //                    dtEOBPayments.BeginInit();
                        //                    dtEOBPayments.Rows.Remove(newRow);
                        //                    dtEOBPayments.AcceptChanges();
                        //                    dtEOBPayments.EndInit();
                        //                    break;
                        //                }
                        //            }
                                    
                        //        }
                        //    }
                        //}

                        

                        _dataRowReservesEOBPayments = null;
                        _dataRowReservesDataSource = null;

                        _dtDataSource.Merge(dtEOBPayments, true);
                        _dtDataSource.AcceptChanges();
                        c1MultiplePayment.BeginUpdate();
                        c1MultiplePayment.DataSource = _dtDataSource.Copy();
                        c1MultiplePayment.EndUpdate();

                        _COL_TOTALCHARGE_SUM = _dtDataSource.Compute("Sum(COL_TOTALCHARGE)", "COL_TOTALCHARGE IS NOT NULL");
                        _COL_ALLOWED_SUM = _dtDataSource.Compute("Sum(COL_ALLOWED)", "COL_ALLOWED IS NOT NULL");
                        _COL_PAYMENT_SUM = _dtDataSource.Compute("Sum(COL_PAYMENT)", "COL_PAYMENT IS NOT NULL");
                        _COL_WRITEOFF_SUM = _dtDataSource.Compute("Sum(COL_WRITEOFF)", "COL_WRITEOFF IS NOT NULL");
                        _COL_COPAY_SUM = _dtDataSource.Compute("Sum(COL_COPAY)", "COL_COPAY IS NOT NULL");
                        _COL_DEDUCTIBLE_SUM = _dtDataSource.Compute("Sum(COL_DEDUCTIBLE)", "COL_DEDUCTIBLE IS NOT NULL");
                        _COL_COINSURANCE_SUM = _dtDataSource.Compute("Sum(COL_COINSURANCE)", "COL_COINSURANCE IS NOT NULL");
                        _COL_WITHHOLD_SUM = _dtDataSource.Compute("Sum(COL_WITHHOLD)", "COL_WITHHOLD IS NOT NULL");
                        _COL_REASON_SUM = _dtDataSource.Compute("Sum(COL_REASON)", "COL_REASON IS NOT NULL");

                    }
                    else
                    {
                        c1MultiplePayment.BeginUpdate();
                        c1MultiplePayment.DataSource = dtEOBPayments.Copy();
                        c1MultiplePayment.EndUpdate();

                        _COL_TOTALCHARGE_SUM = dtEOBPayments.Compute("Sum(COL_TOTALCHARGE)", "COL_TOTALCHARGE IS NOT NULL");
                        _COL_ALLOWED_SUM = dtEOBPayments.Compute("Sum(COL_ALLOWED)", "COL_ALLOWED IS NOT NULL");
                        _COL_PAYMENT_SUM = dtEOBPayments.Compute("Sum(COL_PAYMENT)", "COL_PAYMENT IS NOT NULL");
                        _COL_WRITEOFF_SUM = dtEOBPayments.Compute("Sum(COL_WRITEOFF)", "COL_WRITEOFF IS NOT NULL");
                        _COL_COPAY_SUM = dtEOBPayments.Compute("Sum(COL_COPAY)", "COL_COPAY IS NOT NULL");
                        _COL_DEDUCTIBLE_SUM = dtEOBPayments.Compute("Sum(COL_DEDUCTIBLE)", "COL_DEDUCTIBLE IS NOT NULL");
                        _COL_COINSURANCE_SUM = dtEOBPayments.Compute("Sum(COL_COINSURANCE)", "COL_COINSURANCE IS NOT NULL");
                        _COL_WITHHOLD_SUM = dtEOBPayments.Compute("Sum(COL_WITHHOLD)", "COL_WITHHOLD IS NOT NULL");
                        _COL_REASON_SUM = dtEOBPayments.Compute("Sum(COL_REASON)", "COL_REASON IS NOT NULL");
                    }

                }

                //if (!IsMultiPaymentGridDesigned) 
                { DesignMultiplePaymentGrid(c1MultiplePayment); IsMultiPaymentGridDesigned = true; }

                #region "Calculate Total of Claims"

                if (Convert.ToString(_COL_TOTALCHARGE_SUM).Trim() != "")
                { c1MultiplePaymentTotal.SetData(0, COL_TOTALCHARGE, Convert.ToDecimal(_COL_TOTALCHARGE_SUM)); }
                else
                { c1MultiplePaymentTotal.SetData(0, COL_TOTALCHARGE, Convert.ToDecimal("0.00")); }

                if (Convert.ToString(_COL_ALLOWED_SUM).Trim() != "")
                { c1MultiplePaymentTotal.SetData(0, COL_ALLOWED, Convert.ToDecimal(_COL_ALLOWED_SUM)); }
                else
                { c1MultiplePaymentTotal.SetData(0, COL_ALLOWED, Convert.ToDecimal("0.00")); }

                if (Convert.ToString(_COL_PAYMENT_SUM).Trim() != "")
                { c1MultiplePaymentTotal.SetData(0, COL_PAYMENT, Convert.ToDecimal(_COL_PAYMENT_SUM)); }
                else
                { c1MultiplePaymentTotal.SetData(0, COL_PAYMENT, Convert.ToDecimal("0.00")); }

                if (Convert.ToString(_COL_WRITEOFF_SUM).Trim() != "")
                { c1MultiplePaymentTotal.SetData(0, COL_WRITEOFF, Convert.ToDecimal(_COL_WRITEOFF_SUM)); }
                else
                { c1MultiplePaymentTotal.SetData(0, COL_WRITEOFF, Convert.ToDecimal("0.00")); }

                if (Convert.ToString(_COL_COPAY_SUM).Trim() != "")
                { c1MultiplePaymentTotal.SetData(0, COL_COPAY, Convert.ToDecimal(_COL_COPAY_SUM)); }
                else
                { c1MultiplePaymentTotal.SetData(0, COL_COPAY, Convert.ToDecimal("0.00")); }

                if (Convert.ToString(_COL_DEDUCTIBLE_SUM).Trim() != "")
                { c1MultiplePaymentTotal.SetData(0, COL_DEDUCTIBLE, Convert.ToDecimal(_COL_DEDUCTIBLE_SUM)); }
                else
                { c1MultiplePaymentTotal.SetData(0, COL_DEDUCTIBLE, Convert.ToDecimal("0.00")); }

                if (Convert.ToString(_COL_COINSURANCE_SUM).Trim() != "")
                { c1MultiplePaymentTotal.SetData(0, COL_COINSURANCE, Convert.ToDecimal(_COL_COINSURANCE_SUM)); }
                else
                { c1MultiplePaymentTotal.SetData(0, COL_COINSURANCE, Convert.ToDecimal("0.00")); }

                if (Convert.ToString(_COL_WITHHOLD_SUM).Trim() != "")
                { c1MultiplePaymentTotal.SetData(0, COL_WITHHOLD, Convert.ToDecimal(_COL_WITHHOLD_SUM)); }
                else
                { c1MultiplePaymentTotal.SetData(0, COL_WITHHOLD, Convert.ToDecimal("0.00")); }

                if (Convert.ToString(_COL_REASON_SUM).Trim() != "")
                { c1MultiplePaymentTotal.SetData(0, COL_REASON, Convert.ToDecimal(_COL_REASON_SUM)); }
                else
                { c1MultiplePaymentTotal.SetData(0, COL_REASON, Convert.ToDecimal("0.00")); }

                #endregion

                //c1MultiplePayment.Sort(SortFlags.Ascending, COL_PAY_EOBPAYMENTDTLID);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                c1MultiplePayment.Redraw = true;
                c1MultiplePaymentTotal.Redraw = true;

                _COL_TOTALCHARGE_SUM = null;
                _COL_ALLOWED_SUM = null;
                _COL_PAYMENT_SUM = null;
                _COL_WRITEOFF_SUM = null;
                _COL_COPAY_SUM = null;
                _COL_DEDUCTIBLE_SUM = null;
                _COL_COINSURANCE_SUM = null;
                _COL_WITHHOLD_SUM = null;
                _COL_REASON_SUM = null;

                if (dtEOBPayments != null) { dtEOBPayments.Dispose(); dtEOBPayments = null; }
                if (_dtDataSource != null) { _dtDataSource.Dispose(); _dtDataSource = null; }
            }
            
        }
        
        private void LoadEOBPayments(DataTable dtEOBPayments, bool _isEOBSave)
        {
            int _RowIndex = 1;
            string _sModifiers = "";           
            try
            {
                c1MultiplePayment.Redraw = false;
                c1MultiplePaymentTotal.Redraw = false;
                //for (int i = dtEOBPayments.Rows.Count - 1; i >= 0; i--)
                for (int i = 0; i <= dtEOBPayments.Rows.Count - 1; i++)
                {
                    DataRow row = dtEOBPayments.Rows[i];
                    if ((Convert.ToString(row["PatientName"]).Trim() == "Reserve" && c1MultiplePayment.FindRow(Convert.ToString(row["nEOBID"]), 1, COL_GENERAL, true) == -1) || Convert.ToString(row["PatientName"]) != "Reserve")
                    {

                        if (Convert.ToString(row["PatientName"]).Trim() == "Reserve")
                        {
                            c1MultiplePayment.Rows.Add();
                            _RowIndex = c1MultiplePayment.Rows.Count - 1;
                        }
                        else
                        {
                            c1MultiplePayment.Rows.Insert(1);
                            _RowIndex = 1;
                        }

                        //c1MultiplePayment.Rows.Insert(1);

                        _sModifiers = "";
                        if (Convert.ToString(row["sMod1Code"]) != "")
                        {
                            _sModifiers += (_sModifiers == string.Empty ? Convert.ToString(row["sMod1Code"]) : "," + Convert.ToString(row["sMod1Code"]));
                        }
                        if (Convert.ToString(row["sMod2Code"]) != "")
                        {
                            _sModifiers += (_sModifiers == string.Empty ? Convert.ToString(row["sMod2Code"]) : "," + Convert.ToString(row["sMod2Code"]));
                        }
                        if (Convert.ToString(row["sMod3Code"]) != "")
                        {
                            _sModifiers += (_sModifiers == string.Empty ? Convert.ToString(row["sMod3Code"]) : "," + Convert.ToString(row["sMod3Code"]));
                        }
                        if (Convert.ToString(row["sMod4Code"]) != "")
                        {
                            _sModifiers += (_sModifiers == string.Empty ? Convert.ToString(row["sMod4Code"]) : "," + Convert.ToString(row["sMod4Code"]));
                        }

                        c1MultiplePayment.SetData(_RowIndex, COL_PAY_EOBPAYMENTID, Convert.ToInt64(row["nCreditID"]));
                        if (Convert.ToString(row["PatientName"]).Trim() != "Reserve")
                        {
                            c1MultiplePayment.SetData(_RowIndex, COL_PAY_EOBID, Convert.ToInt64(row["nEOBID"]));
                            c1MultiplePayment.SetData(_RowIndex, COL_DOS_FROM, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(row["nDOSFrom"])).ToString("MM/dd/yyyy"));
                        }
                        else
                        {
                            c1MultiplePayment.SetData(_RowIndex, COL_GENERAL, Convert.ToInt64(row["nEOBID"]));
                            c1MultiplePayment.SetData(_RowIndex, COL_DOS_FROM, null);
                        }
                        c1MultiplePayment.SetData(_RowIndex, COL_PATIENTID, Convert.ToInt64(row["nPatientID"]));
                        c1MultiplePayment.SetData(_RowIndex, COL_PATIENTNAME, Convert.ToString(row["PatientName"]));
                        c1MultiplePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_ID, Convert.ToInt64(row["nBillingTransactionID"]));
                        c1MultiplePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_DETAILID, Convert.ToInt64(row["nBillingTransactionDetailID"]));
                        c1MultiplePayment.SetData(_RowIndex, COL_MODIFIER, _sModifiers);
                        c1MultiplePayment.SetData(_RowIndex, COL_CLAIMDISPNO, Convert.ToString(row["ClaimNumber"]));
                        
                        c1MultiplePayment.SetData(_RowIndex, COL_CPT_CODE, Convert.ToString(row["sCPTCode"]));

                        if (row["dTotalCharges"] != DBNull.Value) { c1MultiplePayment.SetData(_RowIndex, COL_TOTALCHARGE, Convert.ToDecimal(row["dTotalCharges"])); }
                        else { c1MultiplePayment.SetData(_RowIndex, COL_TOTALCHARGE, null); }

                        if (row["dAllowed"] != DBNull.Value && Convert.ToDecimal(row["dAllowed"]) != 0) { c1MultiplePayment.SetData(_RowIndex, COL_ALLOWED, Convert.ToDecimal(row["dAllowed"])); }
                        else { c1MultiplePayment.SetData(_RowIndex, COL_ALLOWED, null); }

                        if (row["dPayment"] != DBNull.Value && Convert.ToDecimal(row["dPayment"]) != 0) { c1MultiplePayment.SetData(_RowIndex, COL_PAYMENT, Convert.ToDecimal(row["dPayment"])); }
                        else { c1MultiplePayment.SetData(_RowIndex, COL_PAYMENT, null); }

                        if (row["dWriteOff"] != DBNull.Value && Convert.ToDecimal(row["dWriteOff"]) != 0) { c1MultiplePayment.SetData(_RowIndex, COL_WRITEOFF, Convert.ToDecimal(row["dWriteOff"])); }
                        else { c1MultiplePayment.SetData(_RowIndex, COL_WRITEOFF, null); }

                        if (row["dCopay"] != DBNull.Value && Convert.ToDecimal(row["dCopay"]) != 0) { c1MultiplePayment.SetData(_RowIndex, COL_COPAY, Convert.ToDecimal(row["dCopay"])); }
                        else { c1MultiplePayment.SetData(_RowIndex, COL_COPAY, null); }

                        if (row["dDeductible"] != DBNull.Value && Convert.ToDecimal(row["dDeductible"]) != 0) { c1MultiplePayment.SetData(_RowIndex, COL_DEDUCTIBLE, Convert.ToDecimal(row["dDeductible"])); }
                        else { c1MultiplePayment.SetData(_RowIndex, COL_DEDUCTIBLE, null); }

                        if (row["dCoInsurance"] != DBNull.Value && Convert.ToDecimal(row["dCoInsurance"]) != 0) { c1MultiplePayment.SetData(_RowIndex, COL_COINSURANCE, Convert.ToDecimal(row["dCoInsurance"])); }
                        else { c1MultiplePayment.SetData(_RowIndex, COL_COINSURANCE, null); }

                        if (row["dWithhold"] != DBNull.Value && Convert.ToDecimal(row["dWithhold"]) != 0) { c1MultiplePayment.SetData(_RowIndex, COL_WITHHOLD, Convert.ToDecimal(row["dWithhold"])); }
                        else { c1MultiplePayment.SetData(_RowIndex, COL_WITHHOLD, null); }

                        if (row["Other"] != DBNull.Value && Convert.ToDecimal(row["Other"]) != 0) { c1MultiplePayment.SetData(_RowIndex, COL_REASON, Convert.ToDecimal(row["Other"])); }
                        else { c1MultiplePayment.SetData(_RowIndex, COL_REASON, null); }

                        c1MultiplePayment.SetData(_RowIndex, COL_SERVICELINE_TYPE, ColServiceLineType.ServiceLine);
                    }
                    
                }
            
                c1MultiplePayment.Cols[COL_PATIENTNAME].Visible = true;
                c1MultiplePayment.Cols[COL_PATIENTNAME].Width = 200;

                c1MultiplePayment.Cols[COL_PREVPAID].Visible = false;
                c1MultiplePayment.Cols[COL_BALANCE].Visible = false;
                c1MultiplePayment.Cols[COL_NEXT].Visible = false;
                c1MultiplePayment.Cols[COL_PARTY].Visible = false;
                c1MultiplePayment.Cols[COL_REASON].Visible = true;

                #region "Calculate Total of Claims"

                c1MultiplePaymentTotal.SetData(0, COL_CHARGE, CalculateMultiplePaymentTotal(COL_CHARGE));
                c1MultiplePaymentTotal.SetData(0, COL_TOTALCHARGE, CalculateMultiplePaymentTotal(COL_TOTALCHARGE));
                c1MultiplePaymentTotal.SetData(0, COL_ALLOWED, CalculateMultiplePaymentTotal(COL_ALLOWED));

                c1MultiplePaymentTotal.SetData(0, COL_PAYMENT, CalculateMultiplePaymentTotal(COL_PAYMENT));
                c1MultiplePaymentTotal.SetData(0, COL_WRITEOFF, CalculateMultiplePaymentTotal(COL_WRITEOFF));
                c1MultiplePaymentTotal.SetData(0, COL_COPAY, CalculateMultiplePaymentTotal(COL_COPAY));
                c1MultiplePaymentTotal.SetData(0, COL_DEDUCTIBLE, CalculateMultiplePaymentTotal(COL_DEDUCTIBLE));
                c1MultiplePaymentTotal.SetData(0, COL_COINSURANCE, CalculateMultiplePaymentTotal(COL_COINSURANCE));
                c1MultiplePaymentTotal.SetData(0, COL_WITHHOLD, CalculateMultiplePaymentTotal(COL_WITHHOLD));
                c1MultiplePaymentTotal.SetData(0, COL_REASON, CalculateMultiplePaymentTotal(COL_REASON));

                c1MultiplePayment.Sort(SortFlags.Ascending, COL_PAY_EOBPAYMENTDTLID);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                c1MultiplePayment.Redraw = true;
                c1MultiplePaymentTotal.Redraw = true;
            }
                #endregion
        }
       
        private void SetDefaultParty(string SelectedAction, string SelectedParty, string CurrentResponsibility, string NextResponsibility, bool IsSelfPartySelected, RowColEventArgs e)
        {
            // Parameter Selected Party is taken for the future development only..
            // Set party column to default 
            c1SinglePayment.SetData(e.Row, COL_PARTY, string.Empty);
             
            if (SelectedAction.StartsWith("B")) // B = Bill
            {
                c1SinglePayment.SetData(e.Row, COL_PARTY, NextResponsibility);
            }
            else if (SelectedAction.StartsWith("R") || SelectedAction.StartsWith("N")) // R = Rebill & N = None
            {
                c1SinglePayment.SetData(e.Row, COL_PARTY, CurrentResponsibility);
            }
            else if (SelectedAction.StartsWith("P")) // P = Pending / No Bill
            {
                // Rule : Pending action is not allowed for self party
                // So if self, set it to empty, else to next (default)
                if (IsSelfPartySelected)
                { c1SinglePayment.SetData(e.Row, COL_PARTY, null); }
                else
                { c1SinglePayment.SetData(e.Row, COL_PARTY, NextResponsibility); }
            }          
            else if (SelectedAction.Equals(string.Empty))
            {
                c1SinglePayment.SetData(e.Row, COL_PARTY, null);
            }
        }

        private C1.Win.C1FlexGrid.C1FlexGrid GetLineReasonCodes(Int64 TransactionID, Int64 TransactionDetailID, Int64 LastEOBId, Int64 nTrackTransactionID, Int64 nTrackTransactionDetailID, string sSubClaimNumber)
        {
            C1.Win.C1FlexGrid.C1FlexGrid oReasonFlex = new C1FlexGrid();
            DataTable dtLineReasonCode = gloInsurancePaymentV2.GetBillingTransactionLine_ReasonCodes(TransactionID, TransactionDetailID, LastEOBId);
            frmEOBPaymentReasonCode ofrmEOBPaymentReasonCode = new frmEOBPaymentReasonCode(AppSettings.ConnectionStringPM, PatientControl.ClaimNumber, 0, 0, null);

            try
            {
                oReasonFlex.Rows.Count = 0;
                oReasonFlex.Cols.Count = 0;
                oReasonFlex.Rows.Fixed = 0;
                oReasonFlex.Cols.Fixed = 0;

                ofrmEOBPaymentReasonCode.DesignGrid(oReasonFlex);

                if (dtLineReasonCode != null)
                {
                    if (dtLineReasonCode.Rows.Count > 0)
                    {
                        for (int k = 0; k < dtLineReasonCode.Rows.Count; k++)
                        {
                            if (oReasonFlex.Cols["Type"] == null)
                            {
                                C1.Win.C1FlexGrid.Column col = oReasonFlex.Cols.Add();
                                col.DataType = typeof(String);
                                col.Caption = "Type";
                                col.Name = "Type";
                            }
                            oReasonFlex.Rows.Add();

                            oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["ClaimNo"].Index, Convert.ToString(dtLineReasonCode.Rows[k]["ClaimNo"]));
                            oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["BLTransactionID"].Index, Convert.ToString(dtLineReasonCode.Rows[k]["BLTransactionID"]));
                            oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["BLTransactionDtlID"].Index, Convert.ToString(dtLineReasonCode.Rows[k]["BLTransactionDtlID"]));
                            oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["SubClaimNo"].Index, sSubClaimNumber);
                            oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["TrackBLTransactionID"].Index, nTrackTransactionID);
                            oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["TrackBLTransactionDtlID"].Index, nTrackTransactionDetailID);
                            oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["Code"].Index, Convert.ToString(dtLineReasonCode.Rows[k]["Code"]));
                            oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["Description"].Index, Convert.ToString(dtLineReasonCode.Rows[k]["Description"]));
                            oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["Amount"].Index, Convert.ToString(dtLineReasonCode.Rows[k]["Amount"]));
                            if (Convert.ToInt32(dtLineReasonCode.Rows[k]["nReasonCodeType"]) == gloBilling.gloERA.enum_CASReasonType.WH.GetHashCode())
                            {
                                oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["ReasonCodeType"].Index, "Withhold");
                            }
                            if (Convert.ToInt32(dtLineReasonCode.Rows[k]["nReasonCodeType"]) == gloBilling.gloERA.enum_CASReasonType.WO.GetHashCode())
                            {
                                oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["ReasonCodeType"].Index, "W/O");
                            }
                            if (Convert.ToInt32(dtLineReasonCode.Rows[k]["nReasonCodeType"]) == gloBilling.gloERA.enum_CASReasonType.Coins.GetHashCode())
                            {
                                oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["ReasonCodeType"].Index, "Co-ins");
                            }
                            if (Convert.ToInt32(dtLineReasonCode.Rows[k]["nReasonCodeType"]) == gloBilling.gloERA.enum_CASReasonType.Copay.GetHashCode())
                            {
                                oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["ReasonCodeType"].Index, "Copay");
                            }
                            if (Convert.ToInt32(dtLineReasonCode.Rows[k]["nReasonCodeType"]) == gloBilling.gloERA.enum_CASReasonType.Deduct.GetHashCode())
                            {
                                oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["ReasonCodeType"].Index, "Deduct");
                            }
                            if (Convert.ToInt32(dtLineReasonCode.Rows[k]["nReasonCodeType"]) == gloBilling.gloERA.enum_CASReasonType.Other.GetHashCode())
                            {
                                oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["ReasonCodeType"].Index, "Other");
                            }
                            oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["Type"].Index, "Reason");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (dtLineReasonCode != null)
                {
                    dtLineReasonCode.Dispose();
                    dtLineReasonCode = null;
                }

                if (ofrmEOBPaymentReasonCode != null)
                {
                    ofrmEOBPaymentReasonCode.Dispose();
                    ofrmEOBPaymentReasonCode = null;
                }

            }
           
            return oReasonFlex;
        }
     
        private void SetCorrectionMode()
        {
            gloAccountsV2.PaymentCollection.PaymentInsuranceClaim oPaymentInsuranceClaim = new gloAccountsV2.PaymentCollection.PaymentInsuranceClaim();
            try
            {
                #region "Get Previous Billed Transaction"

                oPaymentInsuranceClaim = gloInsurancePaymentV2.GetBillingTransaction(PatientControl.ClaimNumber, PatientControl.SubClaimNumber, ContactInsuranceID, PatientInsuranceID);

                #endregion

                #region " Set Correction Flag "

                PaymentAction = FormMode.NewPaymentMode;
                if (oPaymentInsuranceClaim != null && oPaymentInsuranceClaim.CliamLines.Count > 0)
                {
                    for (int j = 0; j <= oPaymentInsuranceClaim.CliamLines.Count - 1; j++)
                    {
                        if (oPaymentInsuranceClaim.CliamLines[j].Iscorrection == true)
                        {
                            PaymentAction = FormMode.CorrectionMode;
                            break;
                        }
                    }
                }

                #endregion
                       
                #region "Setup Previous Billied Transaction details, to the Hidden Grid Columns"

                if (oPaymentInsuranceClaim != null && c1SinglePayment != null)
                {
                    if (oPaymentInsuranceClaim.CliamLines.Count > 0 && c1SinglePayment.Rows.Count > 1)
                    {
                        for (int j = 0; j <= oPaymentInsuranceClaim.CliamLines.Count - 1; j++)
                        {
                            for (int rIndex = 1; rIndex < c1SinglePayment.Rows.Count; rIndex++)
                            {
                                if (c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                                {
                                    if (c1SinglePayment.GetData(rIndex, COL_BILLING_TRANSACTON_DETAILID) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_BILLING_TRANSACTON_DETAILID)).Trim() != "")
                                    {
                                        if (Convert.ToInt64(c1SinglePayment.GetData(rIndex, COL_BILLING_TRANSACTON_DETAILID)) == oPaymentInsuranceClaim.CliamLines[j].BLTransactionDetailID)
                                        {
                                            c1SinglePayment.SetData(rIndex, COL_LAST_ALLOWED, oPaymentInsuranceClaim.CliamLines[j].Last_allowed);
                                            c1SinglePayment.SetData(rIndex, COL_LAST_PAYMENT, oPaymentInsuranceClaim.CliamLines[j].Last_payment);
                                            c1SinglePayment.SetData(rIndex, COL_LAST_WRITEOFF, oPaymentInsuranceClaim.CliamLines[j].Last_writeoff);
                                            c1SinglePayment.SetData(rIndex, COL_LAST_COPAY, oPaymentInsuranceClaim.CliamLines[j].Last_copay);
                                            c1SinglePayment.SetData(rIndex, COL_LAST_DEDUCTIBLE, oPaymentInsuranceClaim.CliamLines[j].Last_deductible);
                                            c1SinglePayment.SetData(rIndex, COL_LAST_COINSURANCE, oPaymentInsuranceClaim.CliamLines[j].Last_coinsurance);
                                            c1SinglePayment.SetData(rIndex, COL_LAST_WITHHOLD, oPaymentInsuranceClaim.CliamLines[j].Last_withhold);
                                            c1SinglePayment.SetData(rIndex, COL_ISCORRECTION, oPaymentInsuranceClaim.CliamLines[j].Iscorrection);
                                            //added To show Last Payable amounts in Corresponding Cells.
                                            if (oPaymentInsuranceClaim.CliamLines[j].Iscorrection)
                                            {

                                                if (!oPaymentInsuranceClaim.CliamLines[j].IsLast_allowedNull)
                                                {
                                                    c1SinglePayment.SetData(rIndex, COL_ALLOWED, oPaymentInsuranceClaim.CliamLines[j].Last_allowed);
                                                }
                                                else
                                                {
                                                    c1SinglePayment.SetData(rIndex, COL_ALLOWED, null);
                                                }
                                                if (!oPaymentInsuranceClaim.CliamLines[j].IsLast_paymentNull)
                                                {
                                                    c1SinglePayment.SetData(rIndex, COL_PAYMENT, oPaymentInsuranceClaim.CliamLines[j].Last_payment);
                                                }
                                                else
                                                {
                                                    c1SinglePayment.SetData(rIndex, COL_PAYMENT, null);
                                                }

                                                if (!oPaymentInsuranceClaim.CliamLines[j].IsLast_writeoffNull)
                                                {
                                                    c1SinglePayment.SetData(rIndex, COL_WRITEOFF, oPaymentInsuranceClaim.CliamLines[j].Last_writeoff);
                                                }
                                                else
                                                {
                                                    c1SinglePayment.SetData(rIndex, COL_WRITEOFF, null);
                                                }

                                                if (!oPaymentInsuranceClaim.CliamLines[j].IsLast_copayNull)
                                                {
                                                    c1SinglePayment.SetData(rIndex, COL_COPAY, oPaymentInsuranceClaim.CliamLines[j].Last_copay);
                                                }
                                                else
                                                {
                                                    c1SinglePayment.SetData(rIndex, COL_COPAY, null);
                                                }

                                                if (!oPaymentInsuranceClaim.CliamLines[j].IsLast_deductibleNull)
                                                {
                                                    c1SinglePayment.SetData(rIndex, COL_DEDUCTIBLE, oPaymentInsuranceClaim.CliamLines[j].Last_deductible);
                                                }
                                                else
                                                {
                                                    c1SinglePayment.SetData(rIndex, COL_DEDUCTIBLE, null);
                                                }

                                                if (!oPaymentInsuranceClaim.CliamLines[j].IsLast_coinsuranceNull)
                                                {
                                                    c1SinglePayment.SetData(rIndex, COL_COINSURANCE, oPaymentInsuranceClaim.CliamLines[j].Last_coinsurance);
                                                }
                                                else
                                                {
                                                    c1SinglePayment.SetData(rIndex, COL_COINSURANCE, null);
                                                }

                                                if (!oPaymentInsuranceClaim.CliamLines[j].IsLast_withholdNull)
                                                {
                                                    c1SinglePayment.SetData(rIndex, COL_WITHHOLD, oPaymentInsuranceClaim.CliamLines[j].Last_withhold);
                                                }
                                                else
                                                {
                                                    c1SinglePayment.SetData(rIndex, COL_WITHHOLD, null);
                                                }

                                                c1SinglePayment.SetData(rIndex, COL_REASON, GetLineReasonCodes(oPaymentInsuranceClaim.CliamLines[j].BLTransactionID, oPaymentInsuranceClaim.CliamLines[j].BLTransactionDetailID, oPaymentInsuranceClaim.CliamLines[j].Last_EOBID, oPaymentInsuranceClaim.CliamLines[j].TrackBLTransactionID, oPaymentInsuranceClaim.CliamLines[j].TrackBLTransactionDetailID, oPaymentInsuranceClaim.CliamLines[j].SubClaimNumber));
                                            }
                                            else
                                            {
                                                if (oPaymentInsuranceClaim.CliamLines[j].Last_allowed > 0)
                                                {
                                                    c1SinglePayment.SetData(rIndex, COL_ALLOWED, oPaymentInsuranceClaim.CliamLines[j].Last_allowed);                                                 
                                                }
                                                else
                                                {
                                                    ClsFeeSchedule objClsFeeSchedule = new ClsFeeSchedule(gloGlobal.gloPMGlobal.DatabaseConnectionString);
                                                    try
                                                    {
                                                        if (oPaymentInsuranceClaim.CliamLines[j].IsNullAllowed)
                                                        {
                                                            Boolean bHasAllowedAmt = false;
                                                            decimal _dAllowedAmount = objClsFeeSchedule.GetAllowedAmount(oPaymentInsuranceClaim.BillingTransactionID, oPaymentInsuranceClaim.CliamLines[j].CPTCode, oPaymentInsuranceClaim.CliamLines[j].Modifier, oPaymentInsuranceClaim.FacilityType, ref bHasAllowedAmt, oPaymentInsuranceClaim.CliamLines[j].DOSFrom);
                                                            if (bHasAllowedAmt)
                                                            {
                                                                c1SinglePayment.SetData(rIndex, COL_ALLOWED, _dAllowedAmount * oPaymentInsuranceClaim.CliamLines[j].Unit);
                                                            }
                                                            else
                                                            {
                                                                c1SinglePayment.SetData(rIndex, COL_ALLOWED, null);
                                                            }
                                                        }
                                                        else
                                                            c1SinglePayment.SetData(rIndex, COL_ALLOWED, oPaymentInsuranceClaim.CliamLines[j].Allowed);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    }
                                                    finally
                                                    {
                                                        objClsFeeSchedule.Dispose();
                                                        objClsFeeSchedule = null;
                                                    }
                                                }
                                                c1SinglePayment.SetData(rIndex, COL_PAYMENT, null);
                                                c1SinglePayment.SetData(rIndex, COL_WRITEOFF, null);
                                                c1SinglePayment.SetData(rIndex, COL_COPAY, null);
                                                c1SinglePayment.SetData(rIndex, COL_DEDUCTIBLE, null);
                                                c1SinglePayment.SetData(rIndex, COL_COINSURANCE, null);
                                                c1SinglePayment.SetData(rIndex, COL_WITHHOLD, null);
                                                c1SinglePayment.SetData(rIndex, COL_REASON, GetLineReasonCodes(oPaymentInsuranceClaim.CliamLines[j].BLTransactionID, oPaymentInsuranceClaim.CliamLines[j].BLTransactionDetailID, oPaymentInsuranceClaim.CliamLines[j].Last_EOBID, oPaymentInsuranceClaim.CliamLines[j].TrackBLTransactionID, oPaymentInsuranceClaim.CliamLines[j].TrackBLTransactionDetailID, oPaymentInsuranceClaim.CliamLines[j].SubClaimNumber));

                                            }
                                            //*****

                                            #region "Calculate Total of Claims"

                                            c1SinglePaymentTotal.SetData(0, COL_CHARGE, CalculateSinglePaymentTotal(COL_CHARGE));
                                            c1SinglePaymentTotal.SetData(0, COL_ALLOWED, CalculateSinglePaymentTotal(COL_ALLOWED));
                                            c1SinglePaymentTotal.SetData(0, COL_PAYMENT, CalculateSinglePaymentTotal(COL_PAYMENT));

                                            c1SinglePaymentTotal.SetData(0, COL_WRITEOFF, CalculateSinglePaymentTotal(COL_WRITEOFF));

                                            c1SinglePaymentTotal.SetData(0, COL_COPAY, CalculateSinglePaymentTotal(COL_COPAY));
                                            c1SinglePaymentTotal.SetData(0, COL_DEDUCTIBLE, CalculateSinglePaymentTotal(COL_DEDUCTIBLE));
                                            c1SinglePaymentTotal.SetData(0, COL_COINSURANCE, CalculateSinglePaymentTotal(COL_COINSURANCE));
                                            c1SinglePaymentTotal.SetData(0, COL_WITHHOLD, CalculateSinglePaymentTotal(COL_WITHHOLD));

                                            c1SinglePaymentTotal.SetData(0, COL_PREVPAID, CalculateSinglePaymentTotal(COL_PREVPAID));
                                            c1SinglePaymentTotal.SetData(0, COL_BALANCE, CalculateSinglePaymentTotal(COL_BALANCE));

                                            #endregion

                                            // Note : while setting correction mode next & party must be reset to blank
                                            c1SinglePayment.SetData(rIndex, COL_NEXT, null);
                                            c1SinglePayment.SetData(rIndex, COL_PARTY, null);

                                            // Crosswalk Billing CPT update, as per selected Insurance 
                                            c1SinglePayment.SetData(rIndex, COL_CROSSWALK_CPT_CODE, oPaymentInsuranceClaim.CliamLines[j].CrossWalkCPTCode);

                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion
                
                #region " Set Allowed & WriteOff for Secondary "

                if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                {
                    for (int rIndex = 1; rIndex < c1SinglePayment.Rows.Count; rIndex++)
                    {
                        if (c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                        {
                            //7022 items:making allowed amount column editable for secondary & tertiary insurances.

                            //if (PatientControl.SelectedInsuranceParty > 1)
                            //{
                            //    c1SinglePayment.SetCellStyle(rIndex, COL_ALLOWED, c1SinglePayment.Styles["cs_NonEditableCurrencyStyle"]);
                            //    c1SinglePayment.Cols[COL_ALLOWED].AllowEditing = false;                              
                            //}
                            //else
                            //{
                                c1SinglePayment.SetCellStyle(rIndex, COL_ALLOWED, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                                c1SinglePayment.Cols[COL_ALLOWED].AllowEditing = true;
                            //}
                        }
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oPaymentInsuranceClaim != null) { oPaymentInsuranceClaim.Dispose(); }
            }
        }

        //private void SelectReasonCodes()
        //{
        //    Int64 _billingTranId = 0;
        //    Int64 _billingTranDtlId = 0;
        //    Int64 _claimNo = 0;
        //    Int64 _TrackbillingTranId = 0;
        //    Int64 _TrackbillingTranDtlId = 0;
        //    string _SubclaimNo = "";
        //    int _rowIndex = 0;

        //    C1.Win.C1FlexGrid.C1FlexGrid oReasonFlex = new C1FlexGrid();
        //    oReasonFlex.Rows.Count = 0;
        //    oReasonFlex.Cols.Count = 0;
        //    oReasonFlex.Rows.Fixed = 0;
        //    oReasonFlex.Cols.Fixed = 0;

        //    try
        //    {

        //        _rowIndex = c1SinglePayment.RowSel;

        //        if (c1SinglePayment.GetData(_rowIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(_rowIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
        //        {
        //            if (c1SinglePayment.GetData(_rowIndex, COL_CLAIMNO) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_CLAIMNO)).Trim() != "")
        //            { _claimNo = Convert.ToInt64(c1SinglePayment.GetData(_rowIndex, COL_CLAIMNO)); }

        //            if (c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_ID) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_ID)).Trim() != "")
        //            { _billingTranId = Convert.ToInt64(c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_ID)); }

        //            if (c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_DETAILID) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_DETAILID)).Trim() != "")
        //            { _billingTranDtlId = Convert.ToInt64(c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_DETAILID)); }

        //            if (c1SinglePayment.GetData(_rowIndex, COL_REASON) != null)
        //            { oReasonFlex = ((C1.Win.C1FlexGrid.C1FlexGrid)c1SinglePayment.GetData(_rowIndex, COL_REASON)); }

        //            if (c1SinglePayment.GetData(_rowIndex, COL_SUBCLAIMNO) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_SUBCLAIMNO)).Trim() != "")
        //            { _SubclaimNo = Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_SUBCLAIMNO)); }

        //            if (c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_ID) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_ID)).Trim() != "")
        //            { _TrackbillingTranId = Convert.ToInt64(c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_ID)); }

        //            if (c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID)).Trim() != "")
        //            { _TrackbillingTranDtlId = Convert.ToInt64(c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID)); }

        //            frmEOBPaymentReasonCode ofrmEOBPaymentReasonCode = new frmEOBPaymentReasonCode(AppSettings.ConnectionStringPM, _claimNo, _billingTranId, _billingTranDtlId, null);
        //            ofrmEOBPaymentReasonCode.SubClaimNo = _SubclaimNo;
        //            ofrmEOBPaymentReasonCode.TrackBillingTransactionID = _TrackbillingTranId;
        //            ofrmEOBPaymentReasonCode.TrackBillingTransactionDetailID = _TrackbillingTranDtlId;

        //            if (oReasonFlex != null && oReasonFlex.Rows.Count > 0)
        //            {
        //                ofrmEOBPaymentReasonCode.DesignGrid(ofrmEOBPaymentReasonCode.ReasonGrid);
        //                ofrmEOBPaymentReasonCode.DesignGrid(ofrmEOBPaymentReasonCode.RemarkGrid);
        //                for (int rInd = 1; rInd < oReasonFlex.Rows.Count; rInd++)
        //                {
        //                    if (Convert.ToString(oReasonFlex.GetData(rInd, oReasonFlex.Cols["Type"].Index)) == "Reason")
        //                    {
        //                        int row = 0;
        //                        row = ofrmEOBPaymentReasonCode.ReasonGrid.Rows.Add().Index;

        //                        for (int cInd = 0; cInd < oReasonFlex.Cols.Count; cInd++)
        //                        {
        //                            if (oReasonFlex.Cols[cInd].Name != "Type")
        //                            {
        //                                ofrmEOBPaymentReasonCode.ReasonGrid.SetData(row, cInd, oReasonFlex.GetData(rInd, cInd));
        //                            }

        //                        }
        //                    }
        //                    if (Convert.ToString(oReasonFlex.GetData(rInd, oReasonFlex.Cols["Type"].Index)) == "Remark")
        //                    {
        //                        int row = 0;
        //                        row = ofrmEOBPaymentReasonCode.RemarkGrid.Rows.Add().Index;

        //                        for (int cInd = 0; cInd < oReasonFlex.Cols.Count; cInd++)
        //                        {
        //                            if (oReasonFlex.Cols[cInd].Name != "Type")
        //                            {
        //                                ofrmEOBPaymentReasonCode.RemarkGrid.SetData(row, cInd, oReasonFlex.GetData(rInd, cInd));
        //                            }
        //                        }
        //                    }
        //                }
        //                if (ofrmEOBPaymentReasonCode.ReasonGrid.Rows.Count > 1)
        //                {
        //                    ofrmEOBPaymentReasonCode.SetReasonCodes();
        //                }
        //            }
        //            else
        //            {
        //                ofrmEOBPaymentReasonCode.DesignGrid(oReasonFlex);
        //                ofrmEOBPaymentReasonCode.DesignGrid(ofrmEOBPaymentReasonCode.ReasonGrid);
        //                ofrmEOBPaymentReasonCode.DesignGrid(ofrmEOBPaymentReasonCode.RemarkGrid);
        //            }

        //            //...Set Statment Notes if any for the line
        //            if (c1SinglePayment.GetData(_rowIndex, COL_LINE_STATEMENTNOTE) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_LINE_STATEMENTNOTE)).Trim() != "")
        //            { ofrmEOBPaymentReasonCode.StatementNote = Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_LINE_STATEMENTNOTE)).Trim(); }

        //            if (c1SinglePayment.GetData(_rowIndex, COL_LINE_STATEMENTNOTEONPRINT) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_LINE_STATEMENTNOTEONPRINT)).Trim() != "")
        //            { ofrmEOBPaymentReasonCode.IncludeStatmentNoteOnPrint = Convert.ToBoolean(c1SinglePayment.GetData(_rowIndex, COL_LINE_STATEMENTNOTEONPRINT)); }
        //            else
        //            { ofrmEOBPaymentReasonCode.IncludeStatmentNoteOnPrint = false; }

        //            //...Set Internal Note if any for the line
        //            if (c1SinglePayment.GetData(_rowIndex, COL_LINE_INTERNALNOTE) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_LINE_INTERNALNOTE)).Trim() != "")
        //            { ofrmEOBPaymentReasonCode.InternalNote = Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_LINE_INTERNALNOTE)).Trim(); }

        //            if (c1SinglePayment.GetData(_rowIndex, COL_LINE_INTERNALNOTEONPRINT) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_LINE_INTERNALNOTEONPRINT)).Trim() != "")
        //            { ofrmEOBPaymentReasonCode.IncludeInternalNoteOnPrint = Convert.ToBoolean(c1SinglePayment.GetData(_rowIndex, COL_LINE_INTERNALNOTEONPRINT)); }
        //            else
        //            { ofrmEOBPaymentReasonCode.IncludeInternalNoteOnPrint = false; }


        //            ofrmEOBPaymentReasonCode.ShowDialog(this);

        //            if (ofrmEOBPaymentReasonCode.FrmDlgRst == DialogResult.OK)
        //            {
        //                oReasonFlex = new C1FlexGrid();
        //                ofrmEOBPaymentReasonCode.DesignGrid(oReasonFlex);


        //                C1.Win.C1FlexGrid.Column col = oReasonFlex.Cols.Add();
        //                col.DataType = typeof(String);
        //                col.Caption = "Type";
        //                col.Name = "Type";
        //                if (ofrmEOBPaymentReasonCode.ReasonGrid.Rows.Count > 0)
        //                {
        //                    for (int rInd = 1; rInd < ofrmEOBPaymentReasonCode.ReasonGrid.Rows.Count; rInd++)
        //                    {
        //                        int row = 0;
        //                        row = oReasonFlex.Rows.Add().Index;

        //                        for (int cInd = 0; cInd < ofrmEOBPaymentReasonCode.ReasonGrid.Cols.Count; cInd++)
        //                        {
        //                            oReasonFlex.SetData(row, cInd, ofrmEOBPaymentReasonCode.ReasonGrid.GetData(rInd, cInd));
        //                        }

        //                        oReasonFlex.SetData(row, oReasonFlex.Cols["Type"].Index, "Reason");

        //                    }
        //                }

        //                if (ofrmEOBPaymentReasonCode.RemarkGrid.Rows.Count>0)
        //                {
        //                    for (int rInd = 1; rInd < ofrmEOBPaymentReasonCode.RemarkGrid.Rows.Count; rInd++)
        //                    {
        //                        int row = 0;
        //                        row = oReasonFlex.Rows.Add().Index;

        //                        for (int cInd = 0; cInd < ofrmEOBPaymentReasonCode.RemarkGrid.Cols.Count; cInd++)
        //                        {
        //                            oReasonFlex.SetData(row, cInd, ofrmEOBPaymentReasonCode.RemarkGrid.GetData(rInd, cInd));
        //                        }

        //                        oReasonFlex.SetData(row, oReasonFlex.Cols["Type"].Index, "Remark");

        //                    }
        //                }

        //                c1SinglePayment.SetData(_rowIndex, COL_REASON, oReasonFlex);
        //                c1SinglePayment.SetData(_rowIndex, COL_LINE_STATEMENTNOTE, ofrmEOBPaymentReasonCode.StatementNote.Trim());
        //                c1SinglePayment.SetData(_rowIndex, COL_LINE_STATEMENTNOTEONPRINT, ofrmEOBPaymentReasonCode.IncludeStatmentNoteOnPrint);
        //                c1SinglePayment.SetData(_rowIndex, COL_LINE_INTERNALNOTE, ofrmEOBPaymentReasonCode.InternalNote.Trim());
        //                c1SinglePayment.SetData(_rowIndex, COL_LINE_INTERNALNOTEONPRINT, ofrmEOBPaymentReasonCode.IncludeInternalNoteOnPrint);

        //            }

        //            ofrmEOBPaymentReasonCode.Dispose();
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}


        private void HighlightReasonCodes(int _rowIndex,string _AmountMissMatch)
        {
            Int64 _billingTranId = 0;
            Int64 _billingTranDtlId = 0;
            Int64 _claimNo = 0;
            Int64 _TrackbillingTranId = 0;
            Int64 _TrackbillingTranDtlId = 0;
            string _SubclaimNo = "";
            //int ReasonCodeSetup = 0;
            C1.Win.C1FlexGrid.C1FlexGrid oReasonFlex = new C1FlexGrid();
            oReasonFlex.Rows.Count = 0;
            oReasonFlex.Cols.Count = 0;
            oReasonFlex.Rows.Fixed = 0;
            oReasonFlex.Cols.Fixed = 0;
            string ReasonCodeType = "Other";
            
            if (c1SinglePayment.GetData(_rowIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(_rowIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
            {
                if (c1SinglePayment.GetData(_rowIndex, COL_CLAIMNO) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_CLAIMNO)).Trim() != "")
                { _claimNo = Convert.ToInt64(c1SinglePayment.GetData(_rowIndex, COL_CLAIMNO)); }

                if (c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_ID) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_ID)).Trim() != "")
                { _billingTranId = Convert.ToInt64(c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_ID)); }

                if (c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_DETAILID) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_DETAILID)).Trim() != "")
                { _billingTranDtlId = Convert.ToInt64(c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_DETAILID)); }

                if (c1SinglePayment.GetData(_rowIndex, COL_REASON) != null)
                { oReasonFlex = ((C1.Win.C1FlexGrid.C1FlexGrid)c1SinglePayment.GetData(_rowIndex, COL_REASON)); }

                if (c1SinglePayment.GetData(_rowIndex, COL_SUBCLAIMNO) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_SUBCLAIMNO)).Trim() != "")
                { _SubclaimNo = Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_SUBCLAIMNO)); }

                if (c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_ID) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_ID)).Trim() != "")
                { _TrackbillingTranId = Convert.ToInt64(c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_ID)); }

                if (c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID)).Trim() != "")
                { _TrackbillingTranDtlId = Convert.ToInt64(c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID)); }

                frmEOBPaymentReasonCode ofrmEOBPaymentReasonCode = new frmEOBPaymentReasonCode(AppSettings.ConnectionStringPM, _claimNo, _billingTranId, _billingTranDtlId, null, ReasonCodeType);
                ofrmEOBPaymentReasonCode.SubClaimNo = _SubclaimNo;
                ofrmEOBPaymentReasonCode.TrackBillingTransactionID = _TrackbillingTranId;
                ofrmEOBPaymentReasonCode.TrackBillingTransactionDetailID = _TrackbillingTranDtlId;
                ofrmEOBPaymentReasonCode.ReasonCodeType = ReasonCodeType;
                ofrmEOBPaymentReasonCode.Payment = 0;
                ofrmEOBPaymentReasonCode.AmountMissmatch = _AmountMissMatch;
                if (oReasonFlex != null && oReasonFlex.Rows.Count > 0)
                {
                    ofrmEOBPaymentReasonCode.DesignGrid(ofrmEOBPaymentReasonCode.ReasonGrid);
                    ofrmEOBPaymentReasonCode.DesignGrid(ofrmEOBPaymentReasonCode.RemarkGrid);
                    for (int rInd = 1; rInd < oReasonFlex.Rows.Count; rInd++)
                    {
                        if (Convert.ToString(oReasonFlex.GetData(rInd, oReasonFlex.Cols["Type"].Index)) == "Reason")
                        {
                            int row = 0;
                            row = ofrmEOBPaymentReasonCode.ReasonGrid.Rows.Add().Index;

                            for (int cInd = 0; cInd < oReasonFlex.Cols.Count; cInd++)
                            {
                                if (oReasonFlex.Cols[cInd].Name != "Type")
                                {
                                    ofrmEOBPaymentReasonCode.ReasonGrid.SetData(row, cInd, oReasonFlex.GetData(rInd, cInd));
                                }
                            }
                        }
                        if (Convert.ToString(oReasonFlex.GetData(rInd, oReasonFlex.Cols["Type"].Index)) == "Remark")
                        {
                            int row = 0;
                            row = ofrmEOBPaymentReasonCode.RemarkGrid.Rows.Add().Index;

                            for (int cInd = 0; cInd < oReasonFlex.Cols.Count; cInd++)
                            {
                                if (oReasonFlex.Cols[cInd].Name != "Type")
                                {
                                    ofrmEOBPaymentReasonCode.RemarkGrid.SetData(row, cInd, oReasonFlex.GetData(rInd, cInd));
                                }
                            }
                        }
                    }
                    if (ofrmEOBPaymentReasonCode.ReasonGrid.Rows.Count > 1)
                    {
                        ofrmEOBPaymentReasonCode.SetReasonCodes();
                    }
                }
                else
                {
                    ofrmEOBPaymentReasonCode.DesignGrid(oReasonFlex);
                    ofrmEOBPaymentReasonCode.DesignGrid(ofrmEOBPaymentReasonCode.ReasonGrid);
                    ofrmEOBPaymentReasonCode.DesignGrid(ofrmEOBPaymentReasonCode.RemarkGrid);
                }

                //...Set Statment Notes if any for the line
                if (c1SinglePayment.GetData(_rowIndex, COL_LINE_STATEMENTNOTE) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_LINE_STATEMENTNOTE)).Trim() != "")
                { ofrmEOBPaymentReasonCode.StatementNote = Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_LINE_STATEMENTNOTE)).Trim(); }

                if (c1SinglePayment.GetData(_rowIndex, COL_LINE_STATEMENTNOTEONPRINT) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_LINE_STATEMENTNOTEONPRINT)).Trim() != "")
                { ofrmEOBPaymentReasonCode.IncludeStatmentNoteOnPrint = Convert.ToBoolean(c1SinglePayment.GetData(_rowIndex, COL_LINE_STATEMENTNOTEONPRINT)); }
                else
                { ofrmEOBPaymentReasonCode.IncludeStatmentNoteOnPrint = false; }

                //...Set Internal Note if any for the line
                if (c1SinglePayment.GetData(_rowIndex, COL_LINE_INTERNALNOTE) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_LINE_INTERNALNOTE)).Trim() != "")
                { ofrmEOBPaymentReasonCode.InternalNote = Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_LINE_INTERNALNOTE)).Trim(); }

                if (c1SinglePayment.GetData(_rowIndex, COL_LINE_INTERNALNOTEONPRINT) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_LINE_INTERNALNOTEONPRINT)).Trim() != "")
                { ofrmEOBPaymentReasonCode.IncludeInternalNoteOnPrint = Convert.ToBoolean(c1SinglePayment.GetData(_rowIndex, COL_LINE_INTERNALNOTEONPRINT)); }
                else
                { ofrmEOBPaymentReasonCode.IncludeInternalNoteOnPrint = false; }


                int rIndex = 0;
                for (int cInd = 1; cInd < oReasonFlex.Rows.Count; cInd++)
                {
                    if (Convert.ToString(oReasonFlex.GetData(cInd, oReasonFlex.Cols["ReasonCodeType"].Index)) == ReasonCodeType)
                    {
                        rIndex++;
                    }
                }

                ofrmEOBPaymentReasonCode.ReasonCodeSetup = "Other";
                ofrmEOBPaymentReasonCode.ReasonCodeType = "Other";

                if (oReasonFlex.Rows.Count > 1)
                {
                    try
                    {
                        DataTable dtReason = new DataTable();
                        DataColumn dColumn1 = new DataColumn("ReasonCode");
                        DataColumn dColumn2 = new DataColumn("sDescription");
                        DataColumn dColumn3 = new DataColumn("Amount");
                        DataColumn dColumn4 = new DataColumn("ReasonCodeType");
                        dtReason.Columns.Add(dColumn1);
                        dtReason.Columns.Add(dColumn2);
                        dtReason.Columns.Add(dColumn3);
                        dtReason.Columns.Add(dColumn4);
                        for (int j = 1; j < oReasonFlex.Rows.Count; j++)
                        {
                            dtReason.Rows.Add();
                            dtReason.Rows[dtReason.Rows.Count - 1]["ReasonCode"] = oReasonFlex.GetData(j, oReasonFlex.Cols["Code"].Index);
                            dtReason.Rows[dtReason.Rows.Count - 1]["sDescription"] = oReasonFlex.GetData(j, oReasonFlex.Cols["Description"].Index);
                            dtReason.Rows[dtReason.Rows.Count - 1]["Amount"] = oReasonFlex.GetData(j, oReasonFlex.Cols["Amount"].Index);
                            dtReason.Rows[dtReason.Rows.Count - 1]["ReasonCodeType"] = oReasonFlex.GetData(j, oReasonFlex.Cols["ReasonCodeType"].Index);
                        }
                        ofrmEOBPaymentReasonCode.dtERAPayerSetup = dtReason;

                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                    }
                }
                ofrmEOBPaymentReasonCode.ShowDialog(this);

                if (ofrmEOBPaymentReasonCode.FrmDlgRst == DialogResult.OK)
                {
                    oReasonFlex = new C1FlexGrid();
                    ofrmEOBPaymentReasonCode.DesignGrid(oReasonFlex);


                    C1.Win.C1FlexGrid.Column col = oReasonFlex.Cols.Add();
                    col.DataType = typeof(String);
                    col.Caption = "Type";
                    col.Name = "Type";
                    if (ofrmEOBPaymentReasonCode.ReasonGrid.Rows.Count > 0)
                    {
                        for (int rInd = 1; rInd < ofrmEOBPaymentReasonCode.ReasonGrid.Rows.Count; rInd++)
                        {
                            int row = 0;
                            if (Convert.ToString(ofrmEOBPaymentReasonCode.ReasonGrid.GetData(rInd, 7)) == "Other")
                            {
                                row = oReasonFlex.Rows.Add().Index;
                                for (int cInd = 0; cInd < ofrmEOBPaymentReasonCode.ReasonGrid.Cols.Count; cInd++)
                                {
                                    oReasonFlex.SetData(row, cInd, ofrmEOBPaymentReasonCode.ReasonGrid.GetData(rInd, cInd));
                                }
                                oReasonFlex.SetData(row, oReasonFlex.Cols["Type"].Index, "Reason");
                            }
                            else
                            {
                                if (Convert.ToString(ofrmEOBPaymentReasonCode.ReasonGrid.GetData(rInd, 10)) != "" &&
                                    Convert.ToDecimal(ofrmEOBPaymentReasonCode.ReasonGrid.GetData(rInd, 10)) != 0)// Add Reason code Only When Reason code Amount is Greater than ZERO 
                                {
                                    row = oReasonFlex.Rows.Add().Index;
                                    for (int cInd = 0; cInd < ofrmEOBPaymentReasonCode.ReasonGrid.Cols.Count; cInd++)
                                    {
                                        oReasonFlex.SetData(row, cInd, ofrmEOBPaymentReasonCode.ReasonGrid.GetData(rInd, cInd));
                                    }
                                    oReasonFlex.SetData(row, oReasonFlex.Cols["Type"].Index, "Reason");
                                }
                            }
                        }
                    }

                    if (ofrmEOBPaymentReasonCode.RemarkGrid.Rows.Count > 0)
                    {
                        for (int rInd = 1; rInd < ofrmEOBPaymentReasonCode.RemarkGrid.Rows.Count; rInd++)
                        {
                            int row = 0;
                            row = oReasonFlex.Rows.Add().Index;

                            for (int cInd = 0; cInd < ofrmEOBPaymentReasonCode.RemarkGrid.Cols.Count; cInd++)
                            {
                                oReasonFlex.SetData(row, cInd, ofrmEOBPaymentReasonCode.RemarkGrid.GetData(rInd, cInd));
                            }

                            oReasonFlex.SetData(row, oReasonFlex.Cols["Type"].Index, "Remark");

                        }
                    }

                    c1SinglePayment.SetData(_rowIndex, COL_REASON, oReasonFlex);
                    c1SinglePayment.SetData(_rowIndex, COL_LINE_STATEMENTNOTE, ofrmEOBPaymentReasonCode.StatementNote.Trim());
                    c1SinglePayment.SetData(_rowIndex, COL_LINE_STATEMENTNOTEONPRINT, ofrmEOBPaymentReasonCode.IncludeStatmentNoteOnPrint);
                    c1SinglePayment.SetData(_rowIndex, COL_LINE_INTERNALNOTE, ofrmEOBPaymentReasonCode.InternalNote.Trim());
                    c1SinglePayment.SetData(_rowIndex, COL_LINE_INTERNALNOTEONPRINT, ofrmEOBPaymentReasonCode.IncludeInternalNoteOnPrint);

                }

                ofrmEOBPaymentReasonCode.Dispose();

            }

        }

        private void SelectReasonCodes(string ReasonCodeType = "Other", decimal Payment = 0)
        {
            Int64 _billingTranId = 0;
            Int64 _billingTranDtlId = 0;
            Int64 _claimNo = 0;
            Int64 _TrackbillingTranId = 0;
            Int64 _TrackbillingTranDtlId = 0;
            string _SubclaimNo = "";
            int _rowIndex = 0;
            int ReasonCodeSetup = 0;
            C1.Win.C1FlexGrid.C1FlexGrid oReasonFlex = new C1FlexGrid();
            oReasonFlex.Rows.Count = 0;
            oReasonFlex.Cols.Count = 0;
            oReasonFlex.Rows.Fixed = 0;
            oReasonFlex.Cols.Fixed = 0;

            try
            {
                if (ReasonCodeType != "Other")
                {
                    ReasonCodeSetup = gloGlobal.gloPMGlobal.ReasonCodeSetup;
                }
                _rowIndex = c1SinglePayment.RowSel;

                if (c1SinglePayment.GetData(_rowIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(_rowIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                {
                    if (c1SinglePayment.GetData(_rowIndex, COL_CLAIMNO) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_CLAIMNO)).Trim() != "")
                    { _claimNo = Convert.ToInt64(c1SinglePayment.GetData(_rowIndex, COL_CLAIMNO)); }

                    if (c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_ID) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_ID)).Trim() != "")
                    { _billingTranId = Convert.ToInt64(c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_ID)); }

                    if (c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_DETAILID) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_DETAILID)).Trim() != "")
                    { _billingTranDtlId = Convert.ToInt64(c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_DETAILID)); }

                    if (c1SinglePayment.GetData(_rowIndex, COL_REASON) != null)
                    { oReasonFlex = ((C1.Win.C1FlexGrid.C1FlexGrid)c1SinglePayment.GetData(_rowIndex, COL_REASON)); }

                    if (c1SinglePayment.GetData(_rowIndex, COL_SUBCLAIMNO) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_SUBCLAIMNO)).Trim() != "")
                    { _SubclaimNo = Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_SUBCLAIMNO)); }

                    if (c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_ID) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_ID)).Trim() != "")
                    { _TrackbillingTranId = Convert.ToInt64(c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_ID)); }

                    if (c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID)).Trim() != "")
                    { _TrackbillingTranDtlId = Convert.ToInt64(c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID)); }

                    frmEOBPaymentReasonCode ofrmEOBPaymentReasonCode = new frmEOBPaymentReasonCode(AppSettings.ConnectionStringPM, _claimNo, _billingTranId, _billingTranDtlId, null, ReasonCodeType);
                    ofrmEOBPaymentReasonCode.SubClaimNo = _SubclaimNo;
                    ofrmEOBPaymentReasonCode.TrackBillingTransactionID = _TrackbillingTranId;
                    ofrmEOBPaymentReasonCode.TrackBillingTransactionDetailID = _TrackbillingTranDtlId;
                    ofrmEOBPaymentReasonCode.ReasonCodeType = ReasonCodeType;
                    ofrmEOBPaymentReasonCode.Payment = Payment;

                    if (oReasonFlex != null && oReasonFlex.Rows.Count > 0)
                    {
                        ofrmEOBPaymentReasonCode.DesignGrid(ofrmEOBPaymentReasonCode.ReasonGrid);
                        ofrmEOBPaymentReasonCode.DesignGrid(ofrmEOBPaymentReasonCode.RemarkGrid);
                        for (int rInd = 1; rInd < oReasonFlex.Rows.Count; rInd++)
                        {
                            if (Convert.ToString(oReasonFlex.GetData(rInd, oReasonFlex.Cols["Type"].Index)) == "Reason")
                            {
                                int row = 0;
                                row = ofrmEOBPaymentReasonCode.ReasonGrid.Rows.Add().Index;

                                for (int cInd = 0; cInd < oReasonFlex.Cols.Count; cInd++)
                                {
                                    if (oReasonFlex.Cols[cInd].Name != "Type")
                                    {
                                        ofrmEOBPaymentReasonCode.ReasonGrid.SetData(row, cInd, oReasonFlex.GetData(rInd, cInd));
                                    }
                                }
                            }
                            if (Convert.ToString(oReasonFlex.GetData(rInd, oReasonFlex.Cols["Type"].Index)) == "Remark")
                            {
                                int row = 0;
                                row = ofrmEOBPaymentReasonCode.RemarkGrid.Rows.Add().Index;

                                for (int cInd = 0; cInd < oReasonFlex.Cols.Count; cInd++)
                                {
                                    if (oReasonFlex.Cols[cInd].Name != "Type")
                                    {
                                        ofrmEOBPaymentReasonCode.RemarkGrid.SetData(row, cInd, oReasonFlex.GetData(rInd, cInd));
                                    }
                                }
                            }
                        }
                        if (ofrmEOBPaymentReasonCode.ReasonGrid.Rows.Count > 1)
                        {
                            ofrmEOBPaymentReasonCode.SetReasonCodes();
                        }
                    }
                    else
                    {
                        ofrmEOBPaymentReasonCode.DesignGrid(oReasonFlex);
                        ofrmEOBPaymentReasonCode.DesignGrid(ofrmEOBPaymentReasonCode.ReasonGrid);
                        ofrmEOBPaymentReasonCode.DesignGrid(ofrmEOBPaymentReasonCode.RemarkGrid);
                    }

                    //...Set Statment Notes if any for the line
                    if (c1SinglePayment.GetData(_rowIndex, COL_LINE_STATEMENTNOTE) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_LINE_STATEMENTNOTE)).Trim() != "")
                    { ofrmEOBPaymentReasonCode.StatementNote = Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_LINE_STATEMENTNOTE)).Trim(); }

                    if (c1SinglePayment.GetData(_rowIndex, COL_LINE_STATEMENTNOTEONPRINT) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_LINE_STATEMENTNOTEONPRINT)).Trim() != "")
                    { ofrmEOBPaymentReasonCode.IncludeStatmentNoteOnPrint = Convert.ToBoolean(c1SinglePayment.GetData(_rowIndex, COL_LINE_STATEMENTNOTEONPRINT)); }
                    else
                    { ofrmEOBPaymentReasonCode.IncludeStatmentNoteOnPrint = false; }

                    //...Set Internal Note if any for the line
                    if (c1SinglePayment.GetData(_rowIndex, COL_LINE_INTERNALNOTE) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_LINE_INTERNALNOTE)).Trim() != "")
                    { ofrmEOBPaymentReasonCode.InternalNote = Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_LINE_INTERNALNOTE)).Trim(); }

                    if (c1SinglePayment.GetData(_rowIndex, COL_LINE_INTERNALNOTEONPRINT) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_LINE_INTERNALNOTEONPRINT)).Trim() != "")
                    { ofrmEOBPaymentReasonCode.IncludeInternalNoteOnPrint = Convert.ToBoolean(c1SinglePayment.GetData(_rowIndex, COL_LINE_INTERNALNOTEONPRINT)); }
                    else
                    { ofrmEOBPaymentReasonCode.IncludeInternalNoteOnPrint = false; }
                   
                    
                    int rIndex = 0;
                    for (int cInd = 1; cInd < oReasonFlex.Rows.Count; cInd++)
                    {
                        if (Convert.ToString(oReasonFlex.GetData(cInd, oReasonFlex.Cols["ReasonCodeType"].Index)) == ReasonCodeType)
                        {
                            rIndex++;
                        }
                    }

                    if (ReasonCodeSetup == 1)//Standered Reason Code setup
                    {
                        if (Convert.ToDecimal(Payment) != 0)
                        {
                            ofrmEOBPaymentReasonCode.ReasonCodeSetup = "Standard";
                            ofrmEOBPaymentReasonCode.ReasonCodeType = ReasonCodeType;

                            DataTable _dtStandardSetup = GetStandardReasonCodeSetUp(ReasonCodeType);
                            bool BlockPopUp = false;

                            if (_dtStandardSetup != null && _dtStandardSetup.Rows.Count > 0)
                            {
                                if (_dtStandardSetup.Rows.Count == 1)
                                {
                                    BlockPopUp = true;
                                    if (PaymentAction == FormMode.CorrectionMode && rIndex > 1)
                                    {
                                        BlockPopUp = false;
                                    }
                                }
                                else
                                {
                                    if (PaymentAction == FormMode.CorrectionMode)
                                    {
                                        if (rIndex == 1)
                                        {
                                            BlockPopUp = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (rIndex == 1)
                                    {
                                        BlockPopUp = true;
                                    }
                            }


                            string Code = string.Empty;
                            string Description = string.Empty;
                            int rIndex_v1 = oReasonFlex.FindRow(ReasonCodeType, 1, oReasonFlex.Cols["ReasonCodeType"].Index, true);

                            if (PaymentAction != FormMode.CorrectionMode)
                            {
                                if (_dtStandardSetup != null && _dtStandardSetup.Rows.Count > 0)
                                {
                                    Code = Convert.ToString(_dtStandardSetup.Rows[0]["ReasonCode"]);
                                    Description = Convert.ToString(_dtStandardSetup.Rows[0]["sDescription"]);
                                }
                            }
                            else
                            {
                                if (rIndex_v1 > 0)
                                {
                                    Code = Convert.ToString(oReasonFlex.GetData(rIndex_v1, oReasonFlex.Cols["Code"].Index));
                                    Description = Convert.ToString(oReasonFlex.GetData(rIndex_v1, oReasonFlex.Cols["Description"].Index));
                                }
                                else
                                {
                                    if (_dtStandardSetup != null && _dtStandardSetup.Rows.Count > 0)
                                    {
                                        if (_dtStandardSetup.Rows.Count == 1)
                                        {
                                            Code = Convert.ToString(_dtStandardSetup.Rows[0]["ReasonCode"]);
                                            Description = Convert.ToString(_dtStandardSetup.Rows[0]["sDescription"]);
                                            BlockPopUp = true;
                                        }
                                        else
                                        {
                                            BlockPopUp = false;
                                        }
                                    }
                                }
                            }

                            if (Code == "" && Description == "")
                            {
                                BlockPopUp = false;
                            }

                            if (BlockPopUp)
                            {
                                if (rIndex_v1 < 0)//Inserting new reason code
                                {
                                    //if (PaymentAction != FormMode.CorrectionMode)
                                    //{
                                        if (oReasonFlex.Cols["Type"] == null)
                                        {
                                            C1.Win.C1FlexGrid.Column col = oReasonFlex.Cols.Add();
                                            col.DataType = typeof(String);
                                            col.Caption = "Type";
                                            col.Name = "Type";
                                        }
                                        oReasonFlex.Rows.Add();

                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["ClaimNo"].Index, _claimNo);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["BLTransactionID"].Index, _billingTranId);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["BLTransactionDtlID"].Index, _billingTranDtlId);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["SubClaimNo"].Index, _SubclaimNo);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["TrackBLTransactionID"].Index, _TrackbillingTranId);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["TrackBLTransactionDtlID"].Index, _TrackbillingTranDtlId);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["Code"].Index, Code);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["Description"].Index, Description);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["Amount"].Index, Payment);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["ReasonCodeType"].Index, ReasonCodeType);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["Type"].Index, "Reason");
                                    //}
                                }
                                else//Updateing allready inserted reason code
                                {
                                    if (Convert.ToDecimal(Payment) != 0)
                                    {
                                        oReasonFlex.SetData(rIndex_v1, oReasonFlex.Cols["ClaimNo"].Index, _claimNo);
                                        oReasonFlex.SetData(rIndex_v1, oReasonFlex.Cols["BLTransactionID"].Index, _billingTranId);
                                        oReasonFlex.SetData(rIndex_v1, oReasonFlex.Cols["BLTransactionDtlID"].Index, _billingTranDtlId);
                                        oReasonFlex.SetData(rIndex_v1, oReasonFlex.Cols["SubClaimNo"].Index, _SubclaimNo);
                                        oReasonFlex.SetData(rIndex_v1, oReasonFlex.Cols["TrackBLTransactionID"].Index, _TrackbillingTranId);
                                        oReasonFlex.SetData(rIndex_v1, oReasonFlex.Cols["TrackBLTransactionDtlID"].Index, _TrackbillingTranDtlId);
                                        oReasonFlex.SetData(rIndex_v1, oReasonFlex.Cols["Code"].Index, Code);
                                        oReasonFlex.SetData(rIndex_v1, oReasonFlex.Cols["Description"].Index, Description);
                                        oReasonFlex.SetData(rIndex_v1, oReasonFlex.Cols["Amount"].Index, Payment);
                                        oReasonFlex.SetData(rIndex_v1, oReasonFlex.Cols["ReasonCodeType"].Index, ReasonCodeType);
                                        oReasonFlex.SetData(rIndex_v1, oReasonFlex.Cols["Type"].Index, "Reason");
                                    }
                                    else
                                    {
                                        oReasonFlex.Rows.Remove(rIndex);
                                    }
                                }
                                c1SinglePayment.SetData(_rowIndex, COL_REASON, oReasonFlex);
                                ofrmEOBPaymentReasonCode.FrmDlgRst = DialogResult.Abort;
                            }
                            else
                            {
                                //if (PaymentAction != FormMode.CorrectionMode)
                                //{
                                if (rIndex_v1 <= 0)//Inserting new reason code
                                {
                                    ofrmEOBPaymentReasonCode.dtERAPayerSetup = _dtStandardSetup;
                                }
                                //}
                                ofrmEOBPaymentReasonCode.ShowDialog(this);
                            }
                            if (_dtStandardSetup != null)
                            {
                                _dtStandardSetup.Dispose();
                                _dtStandardSetup = null;
                            }
                        }
                        else
                        {
                            if (Convert.ToDecimal(Payment) == 0)
                            {
                                DeleteReasonCodes(c1SinglePayment.RowSel, ReasonCodeType);
                            }
                        }
                    }

                    else if (ReasonCodeSetup == 2)//Payer's reason Code setup
                    {
                        if (Convert.ToDecimal(Payment) != 0)
                        {

                            ofrmEOBPaymentReasonCode.ReasonCodeSetup = "PayerSetup";
                            ofrmEOBPaymentReasonCode.ReasonCodeType = ReasonCodeType;

                            DataTable _dtERAPayerSetup = GetPayerSetUp(ReasonCodeType);
                            bool BlockPopUp = false;

                            if (_dtERAPayerSetup != null && _dtERAPayerSetup.Rows.Count > 0)
                            {
                                if (_dtERAPayerSetup.Rows.Count == 1)
                                {
                                    BlockPopUp = true;
                                    if (PaymentAction == FormMode.CorrectionMode && rIndex > 1)
                                    {
                                        BlockPopUp = false;
                                    }
                                }
                                else
                                {
                                    if (PaymentAction == FormMode.CorrectionMode)
                                    {
                                        if (rIndex == 1)
                                        {
                                            BlockPopUp = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (rIndex == 1)
                                {
                                    BlockPopUp = true;
                                }
                            }

                            string Code = string.Empty;
                            string Description = string.Empty;
                            int rIndex_v2 = oReasonFlex.FindRow(ReasonCodeType, 1, oReasonFlex.Cols["ReasonCodeType"].Index, true);

                            if (PaymentAction != FormMode.CorrectionMode)
                            {
                                if (_dtERAPayerSetup != null && _dtERAPayerSetup.Rows.Count > 0)
                                {
                                    Code = Convert.ToString(_dtERAPayerSetup.Rows[0]["ReasonCode"]);
                                    Description = Convert.ToString(_dtERAPayerSetup.Rows[0]["sDescription"]);
                                }
                            }
                            else
                            {
                                if (rIndex_v2 > 0)
                                {
                                    Code = Convert.ToString(oReasonFlex.GetData(rIndex_v2, oReasonFlex.Cols["Code"].Index));
                                    Description = Convert.ToString(oReasonFlex.GetData(rIndex_v2, oReasonFlex.Cols["Description"].Index));
                                }
                                else
                                {
                                    if (_dtERAPayerSetup != null && _dtERAPayerSetup.Rows.Count > 0)
                                    {
                                        if (_dtERAPayerSetup.Rows.Count == 1)
                                        {
                                            Code = Convert.ToString(_dtERAPayerSetup.Rows[0]["ReasonCode"]);
                                            Description = Convert.ToString(_dtERAPayerSetup.Rows[0]["sDescription"]);
                                            BlockPopUp = true;
                                        }
                                        else
                                        {
                                            BlockPopUp = false;
                                        }
                                    }
                                }
                            }

                            if (Code == "" && Description == "")
                            {
                                BlockPopUp = false;
                            }

                            if (BlockPopUp)
                            {
                                if (rIndex_v2 < 0)//Inserting new reason code
                                {
                                    //if (PaymentAction != FormMode.CorrectionMode)
                                    //{
                                        if (oReasonFlex.Cols["Type"] == null)
                                        {
                                            C1.Win.C1FlexGrid.Column col = oReasonFlex.Cols.Add();
                                            col.DataType = typeof(String);
                                            col.Caption = "Type";
                                            col.Name = "Type";
                                        }
                                        oReasonFlex.Rows.Add();

                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["ClaimNo"].Index, _claimNo);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["BLTransactionID"].Index, _billingTranId);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["BLTransactionDtlID"].Index, _billingTranDtlId);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["SubClaimNo"].Index, _SubclaimNo);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["TrackBLTransactionID"].Index, _TrackbillingTranId);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["TrackBLTransactionDtlID"].Index, _TrackbillingTranDtlId);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["Code"].Index, Code);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["Description"].Index, Description);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["Amount"].Index, Payment);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["ReasonCodeType"].Index, ReasonCodeType);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["Type"].Index, "Reason");
                                    //}
                                }
                                else//Updateing allready inserted reason code
                                {
                                    if (Convert.ToDecimal(Payment) != 0)
                                    {
                                        oReasonFlex.SetData(rIndex_v2, oReasonFlex.Cols["ClaimNo"].Index, _claimNo);
                                        oReasonFlex.SetData(rIndex_v2, oReasonFlex.Cols["BLTransactionID"].Index, _billingTranId);
                                        oReasonFlex.SetData(rIndex_v2, oReasonFlex.Cols["BLTransactionDtlID"].Index, _billingTranDtlId);
                                        oReasonFlex.SetData(rIndex_v2, oReasonFlex.Cols["SubClaimNo"].Index, _SubclaimNo);
                                        oReasonFlex.SetData(rIndex_v2, oReasonFlex.Cols["TrackBLTransactionID"].Index, _TrackbillingTranId);
                                        oReasonFlex.SetData(rIndex_v2, oReasonFlex.Cols["TrackBLTransactionDtlID"].Index, _TrackbillingTranDtlId);
                                        oReasonFlex.SetData(rIndex_v2, oReasonFlex.Cols["Code"].Index, Code);
                                        oReasonFlex.SetData(rIndex_v2, oReasonFlex.Cols["Description"].Index, Description);
                                        oReasonFlex.SetData(rIndex_v2, oReasonFlex.Cols["Amount"].Index, Payment);
                                        oReasonFlex.SetData(rIndex_v2, oReasonFlex.Cols["ReasonCodeType"].Index, ReasonCodeType);
                                        oReasonFlex.SetData(rIndex_v2, oReasonFlex.Cols["Type"].Index, "Reason");
                                    }
                                    else
                                    {
                                        oReasonFlex.Rows.Remove(rIndex);
                                    }
                                }
                                c1SinglePayment.SetData(_rowIndex, COL_REASON, oReasonFlex);
                                ofrmEOBPaymentReasonCode.FrmDlgRst = DialogResult.Abort;
                            }
                            else
                            {
                                //if (PaymentAction != FormMode.CorrectionMode)
                                //{
                                if (rIndex_v2 <= 0)//Inserting new reason code
                                {
                                    ofrmEOBPaymentReasonCode.dtERAPayerSetup = _dtERAPayerSetup;
                                }
                                //}
                                ofrmEOBPaymentReasonCode.ShowDialog(this);
                            }
                            if (_dtERAPayerSetup != null)
                            {
                                _dtERAPayerSetup.Dispose();
                                _dtERAPayerSetup = null;
                            }
                        }
                        else
                        {
                            if (Convert.ToDecimal(Payment) == 0)
                            {
                                DeleteReasonCodes(c1SinglePayment.RowSel, ReasonCodeType);
                            }
                        }
                    }
                    else if (ReasonCodeSetup == 3)//Manual reason Code setup
                    {
                        if (Convert.ToDecimal(Payment) != 0)
                        {
                            ofrmEOBPaymentReasonCode.ReasonCodeSetup = "Manual";
                            ofrmEOBPaymentReasonCode.ReasonCodeType = ReasonCodeType;
                            int rIndex_v3= oReasonFlex.FindRow(ReasonCodeType, 1, oReasonFlex.Cols["ReasonCodeType"].Index, true);
                            bool BlockPopUp = false;
                            string Code = string.Empty;
                            string Description = string.Empty;

                            if (PaymentAction == FormMode.CorrectionMode)
                            {
                                if (rIndex == 1)
                                {
                                    BlockPopUp = true;
                                    if (rIndex_v3 > 0)
                                    {
                                        BlockPopUp = true;
                                        Code = Convert.ToString(oReasonFlex.GetData(rIndex_v3, oReasonFlex.Cols["Code"].Index));
                                        Description = Convert.ToString(oReasonFlex.GetData(rIndex_v3, oReasonFlex.Cols["Description"].Index));
                                    }
                                    else
                                    {
                                        BlockPopUp = false;
                                    }
                                }
                                else if (rIndex > 1)
                                {
                                    BlockPopUp = false;
                                }

                              
                                if (Code == "" && Description == "")
                                {
                                    BlockPopUp = false;
                                }
                            }

                            if (BlockPopUp)
                            {
                                if (rIndex_v3 < 0)//Inserting new reason code
                                {
                                    //if (PaymentAction != FormMode.CorrectionMode)
                                    //{
                                        if (oReasonFlex.Cols["Type"] == null)
                                        {
                                            C1.Win.C1FlexGrid.Column col = oReasonFlex.Cols.Add();
                                            col.DataType = typeof(String);
                                            col.Caption = "Type";
                                            col.Name = "Type";
                                        }
                                        oReasonFlex.Rows.Add();

                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["ClaimNo"].Index, _claimNo);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["BLTransactionID"].Index, _billingTranId);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["BLTransactionDtlID"].Index, _billingTranDtlId);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["SubClaimNo"].Index, _SubclaimNo);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["TrackBLTransactionID"].Index, _TrackbillingTranId);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["TrackBLTransactionDtlID"].Index, _TrackbillingTranDtlId);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["Code"].Index, Code);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["Description"].Index, Description);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["Amount"].Index, Payment);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["ReasonCodeType"].Index, ReasonCodeType);
                                        oReasonFlex.SetData(oReasonFlex.Rows.Count - 1, oReasonFlex.Cols["Type"].Index, "Reason");
                                    //}
                                }
                                else//Updateing allready inserted reason code
                                {
                                    if (Convert.ToDecimal(Payment) != 0)
                                    {
                                        oReasonFlex.SetData(rIndex_v3, oReasonFlex.Cols["ClaimNo"].Index, _claimNo);
                                        oReasonFlex.SetData(rIndex_v3, oReasonFlex.Cols["BLTransactionID"].Index, _billingTranId);
                                        oReasonFlex.SetData(rIndex_v3, oReasonFlex.Cols["BLTransactionDtlID"].Index, _billingTranDtlId);
                                        oReasonFlex.SetData(rIndex_v3, oReasonFlex.Cols["SubClaimNo"].Index, _SubclaimNo);
                                        oReasonFlex.SetData(rIndex_v3, oReasonFlex.Cols["TrackBLTransactionID"].Index, _TrackbillingTranId);
                                        oReasonFlex.SetData(rIndex_v3, oReasonFlex.Cols["TrackBLTransactionDtlID"].Index, _TrackbillingTranDtlId);
                                        oReasonFlex.SetData(rIndex_v3, oReasonFlex.Cols["Code"].Index, Code);
                                        oReasonFlex.SetData(rIndex_v3, oReasonFlex.Cols["Description"].Index, Description);
                                        oReasonFlex.SetData(rIndex_v3, oReasonFlex.Cols["Amount"].Index, Payment);
                                        oReasonFlex.SetData(rIndex_v3, oReasonFlex.Cols["ReasonCodeType"].Index, ReasonCodeType);
                                        oReasonFlex.SetData(rIndex_v3, oReasonFlex.Cols["Type"].Index, "Reason");
                                    }
                                    else
                                    {
                                        oReasonFlex.Rows.Remove(rIndex);
                                    }
                                }
                                c1SinglePayment.SetData(_rowIndex, COL_REASON, oReasonFlex);
                                ofrmEOBPaymentReasonCode.FrmDlgRst = DialogResult.Abort;
                            }
                            else
                            {
                                ofrmEOBPaymentReasonCode.ShowDialog(this);
                            }
                        }
                        else
                        {
                            if (Convert.ToDecimal(Payment) == 0)
                            {
                                DeleteReasonCodes(c1SinglePayment.RowSel, ReasonCodeType);
                            }
                        }
                    }
                    else if (ReasonCodeSetup == 0)//Other reason Code setup(Old Flow)
                    {
                        ofrmEOBPaymentReasonCode.ReasonCodeSetup = "Other";
                        ofrmEOBPaymentReasonCode.ReasonCodeType = "Other";

                        if (oReasonFlex.Rows.Count > 1)
                        {
                            try
                            {
                                DataTable dtReason = new DataTable();
                                DataColumn dColumn1 = new DataColumn("ReasonCode");
                                DataColumn dColumn2 = new DataColumn("sDescription");
                                DataColumn dColumn3 = new DataColumn("Amount");
                                DataColumn dColumn4 = new DataColumn("ReasonCodeType");
                                dtReason.Columns.Add(dColumn1);
                                dtReason.Columns.Add(dColumn2);
                                dtReason.Columns.Add(dColumn3);
                                dtReason.Columns.Add(dColumn4);
                                for (int j = 1; j < oReasonFlex.Rows.Count; j++)
                                {
                                    dtReason.Rows.Add();
                                    dtReason.Rows[dtReason.Rows.Count - 1]["ReasonCode"] = oReasonFlex.GetData(j, oReasonFlex.Cols["Code"].Index);
                                    dtReason.Rows[dtReason.Rows.Count - 1]["sDescription"] = oReasonFlex.GetData(j, oReasonFlex.Cols["Description"].Index);
                                    dtReason.Rows[dtReason.Rows.Count - 1]["Amount"] = oReasonFlex.GetData(j, oReasonFlex.Cols["Amount"].Index);
                                    dtReason.Rows[dtReason.Rows.Count - 1]["ReasonCodeType"] = oReasonFlex.GetData(j, oReasonFlex.Cols["ReasonCodeType"].Index);
                                }
                                ofrmEOBPaymentReasonCode.dtERAPayerSetup = dtReason;
                               
                            }
                            catch (Exception ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                            }
                        } 
                        ofrmEOBPaymentReasonCode.ShowDialog(this);
                    }

                    //if (Convert.ToDecimal(Payment) != 0)
                    //{
                    //    ofrmEOBPaymentReasonCode.ShowDialog(this);
                    //}
                    //else
                    //{
                    //    if (ReasonCodeSetup == 0)
                    //    {
                    //        ofrmEOBPaymentReasonCode.ShowDialog(this);
                    //    }
                    //}

                    if (ofrmEOBPaymentReasonCode.FrmDlgRst == DialogResult.OK)
                    {
                        oReasonFlex = new C1FlexGrid();
                        ofrmEOBPaymentReasonCode.DesignGrid(oReasonFlex);


                        C1.Win.C1FlexGrid.Column col = oReasonFlex.Cols.Add();
                        col.DataType = typeof(String);
                        col.Caption = "Type";
                        col.Name = "Type";
                        if (ofrmEOBPaymentReasonCode.ReasonGrid.Rows.Count > 0)
                        {
                            for (int rInd = 1; rInd < ofrmEOBPaymentReasonCode.ReasonGrid.Rows.Count; rInd++)
                            {
                                int row = 0;
                                if (Convert.ToString(ofrmEOBPaymentReasonCode.ReasonGrid.GetData(rInd, 7)) == "Other")
                                {
                                    row = oReasonFlex.Rows.Add().Index;
                                    for (int cInd = 0; cInd < ofrmEOBPaymentReasonCode.ReasonGrid.Cols.Count; cInd++)
                                    {
                                        oReasonFlex.SetData(row, cInd, ofrmEOBPaymentReasonCode.ReasonGrid.GetData(rInd, cInd));
                                    }
                                    oReasonFlex.SetData(row, oReasonFlex.Cols["Type"].Index, "Reason");
                                }
                                else
                                {
                                    if (Convert.ToString(ofrmEOBPaymentReasonCode.ReasonGrid.GetData(rInd, 10)) != "" &&
                                        Convert.ToDecimal(ofrmEOBPaymentReasonCode.ReasonGrid.GetData(rInd, 10)) != 0)// Add Reason code Only When Reason code Amount is Greater than ZERO 
                                    {
                                        row = oReasonFlex.Rows.Add().Index;
                                        for (int cInd = 0; cInd < ofrmEOBPaymentReasonCode.ReasonGrid.Cols.Count; cInd++)
                                        {
                                            oReasonFlex.SetData(row, cInd, ofrmEOBPaymentReasonCode.ReasonGrid.GetData(rInd, cInd));
                                        }
                                        oReasonFlex.SetData(row, oReasonFlex.Cols["Type"].Index, "Reason");
                                    }
                                }
                            }
                        }

                        if (ofrmEOBPaymentReasonCode.RemarkGrid.Rows.Count > 0)
                        {
                            for (int rInd = 1; rInd < ofrmEOBPaymentReasonCode.RemarkGrid.Rows.Count; rInd++)
                            {
                                int row = 0;
                                row = oReasonFlex.Rows.Add().Index;

                                for (int cInd = 0; cInd < ofrmEOBPaymentReasonCode.RemarkGrid.Cols.Count; cInd++)
                                {
                                    oReasonFlex.SetData(row, cInd, ofrmEOBPaymentReasonCode.RemarkGrid.GetData(rInd, cInd));
                                }

                                oReasonFlex.SetData(row, oReasonFlex.Cols["Type"].Index, "Remark");

                            }
                        }

                        c1SinglePayment.SetData(_rowIndex, COL_REASON, oReasonFlex);
                        c1SinglePayment.SetData(_rowIndex, COL_LINE_STATEMENTNOTE, ofrmEOBPaymentReasonCode.StatementNote.Trim());
                        c1SinglePayment.SetData(_rowIndex, COL_LINE_STATEMENTNOTEONPRINT, ofrmEOBPaymentReasonCode.IncludeStatmentNoteOnPrint);
                        c1SinglePayment.SetData(_rowIndex, COL_LINE_INTERNALNOTE, ofrmEOBPaymentReasonCode.InternalNote.Trim());
                        c1SinglePayment.SetData(_rowIndex, COL_LINE_INTERNALNOTEONPRINT, ofrmEOBPaymentReasonCode.IncludeInternalNoteOnPrint);

                    }

                    ofrmEOBPaymentReasonCode.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        private void LoadPendingCheck()
        {
            frmLoadPendingChecksV2 ofrmBillingCheckDiff = new frmLoadPendingChecksV2();
            this.UseWaitCursor = true;
            this.Cursor = Cursors.WaitCursor; 
            try
            {
                GetControlSelection();
                ofrmBillingCheckDiff.InsuranceCompanyID = SelectedInsuranceCompanyID; 
                ofrmBillingCheckDiff.InsuranceCompanyName = SelectedInsuranceCompany; 

                ofrmBillingCheckDiff.ShowDialog(this);

                if (ofrmBillingCheckDiff._frmDlgRst == DialogResult.OK)
                {
                    DialogResult _result = DialogResult.No;
                    if (IsPaymentMade() && txtCheckNumber.Text.Trim() != "")
                    { _result = SaveChangesAlert(); }

                    //if (_result == DialogResult.Cancel)
                    //{
                    //    if (!IsPendingCheckLoaded)
                    //    { IsPendingCheckLoaded = false; }
                    //}
                    else if (_result == DialogResult.No)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        IsPendingCheckLoaded = true;
                        _EOBPaymentID = ofrmBillingCheckDiff.EOBPaymentID;
                        IsMultiPaymentGridDesigned = false;
                        IsMultiPaymentTotalGridDesigned = false;

                        //..This code need to revert if not necessary

                        //AmountAddedToReserve = 0;
                        //AmountTakenFromReserve = 0;
                        //TotalFundsRemaining = 0;
                        //TotalFundsAvaiable = 0;
                        this.oCreditLine = new PaymentCollection.Credit();
                        this.oCreditResDTL = new PaymentCollection.Credit();

                        //..This code need to revert if not necessary

                        LoadFormData(_EOBPaymentID, 0, false);
                        pnlMultiplePayment.Visible = true;
                        tls_btnViewEOB.Tag = "HideEOB";
                        tls_btnViewEOB.Text = "&Hide EOB";
                        tls_btnViewEOB.ToolTipText = "Hide Explanation of Benefit";
                        splitter1.Enabled = true;
                    }
                }
                //_IsTakeback = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ofrmBillingCheckDiff.Dispose();
                //_isPendingCheckLoading = false;
                this.UseWaitCursor = false;
                this.Cursor = Cursors.Default; 
                //frmInsurancePayment_OnRemainingCalculationChanged();
                SetControlSelection();
            }
        }

        private void SearchInsuranceCompany()
        {
            try
            {
                if (!IsPaymentInProcess) 
                {
                    //if (AmountTakenFromReserve > 0)
                    //{
                    //    MessageBox.Show("Insurance company cannot be changed, there is reserve amount used on current payment." + Environment.NewLine + "Clear existing reserve amount to change the insurance company.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //else
                    //{
                        frmSearchInsuranceCompany ofrmSearchInsuranceCompany = new frmSearchInsuranceCompany();
                        GetControlSelection();
                        try
                        {
                            ofrmSearchInsuranceCompany.StartPosition = FormStartPosition.CenterScreen;
                            ofrmSearchInsuranceCompany.ShowDialog(this);

                            if (ofrmSearchInsuranceCompany.FrmDlgRst == DialogResult.OK)
                            {
                                this.SelectedInsuranceCompanyID = ofrmSearchInsuranceCompany.InsuranceCompanyID;
                                this.SelectedInsuranceCompany = ofrmSearchInsuranceCompany.InsuranceCompanyName;

                                if (SelectedInsuranceCompanyID != 0 && SelectedInsuranceCompany != "")
                                { toolTip1.SetToolTip(lblInsCompany, SelectedInsuranceCompany); }
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                        }
                        finally
                        {
                            if (ofrmSearchInsuranceCompany != null) { ofrmSearchInsuranceCompany.Dispose(); }
                            SetControlSelection();
                        }

                    //}
                }
                else
                {
                    MessageBox.Show("Insurance company cannot be changed.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private void ShowModifyCharges()
        {
            gloBilling.gloBilling ogloBilling = null;
            try
            {
                if (ClaimDetails.IsClaimExist)
                {
                    if (ClaimDetails.TransactionID != 0)
                    {
                        ogloBilling = new gloBilling.gloBilling(AppSettings.ConnectionStringPM, string.Empty);

                        // Set the transactionID for the latest claim 
                        //nTransactionID = ogloBilling.GetLastTransactionID(ClaimDetails.TransactionID);
                        //---------------------------------------------------------------
                        Boolean _IsModified = false;
                        if (PatientControl.PatientID == 0)
                        {
                            if ( PatientID > 0)
                            { _IsModified = ogloBilling.ShowModifyCharges(PatientID, ClaimDetails.TransactionID, ClaimDetails.IsClaimVoided, true, this.Name, this); }
                        }
                        else
                        {
                            _IsModified = ogloBilling.ShowModifyCharges(PatientControl.PatientID, ClaimDetails.TransactionID, ClaimDetails.IsClaimVoided, true, this.Name, this);
                        }

                        
                     
                        // If changes has been modified then only refresh the claim else skip
                        if (_IsModified == true)
                        {
                            if (!ClaimDetails.IsClaimVoided)
                            { LoadClaim(); }
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Please select the claim", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); }
            }
        }

        public static Boolean ReleaseLockStatus(Int64 nMasterTransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            Boolean isReleased = false;
            try
            {
                oDBParameters.Add("@nTransactionMasterID", nMasterTransactionID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sMachineName", Environment.MachineName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDB.Connect(false);
                oDB.Execute("BL_UnLockClaims", oDBParameters);
                oDB.Disconnect();
                isReleased = true;
            }

            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                isReleased = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                isReleased = false;
            }
            finally
            {
                if (oDB != null)
                    oDB.Dispose();
                if (oDBParameters != null)
                    oDBParameters.Dispose();

            }
            return isReleased;
        }

        #region "Reserve Methods "

        private void UseReserveAmount()
        {
            frmPaymentUseReserveInsuranceV2 ofrmInsuranceUseReserve = null;
            try
            {
                GetControlSelection();
                bool _IsReservedUsed = false;
               
                if (this.oCreditResDTL.EOBCreditDTL.Count > 0)
                {
                    for (int i = 0; i <= this.oCreditResDTL.EOBCreditDTL.Count - 1; i++)
                    {
                        if (this.oCreditResDTL.EOBCreditDTL[i].EntryType == PaymentEntryTypeV2.InsuraceReserved)
                        {
                            _IsReservedUsed = true;
                            break;
                        }
                    }
                    if (_IsReservedUsed)
                    {
                        ofrmInsuranceUseReserve = new frmPaymentUseReserveInsuranceV2(AppSettings.ConnectionStringPM, 0);
                    }
                    else
                    {
                        ofrmInsuranceUseReserve = new frmPaymentUseReserveInsuranceV2(AppSettings.ConnectionStringPM, SelectedInsuranceCompanyID);
                    }

                }
                else
                {
                    ofrmInsuranceUseReserve = new frmPaymentUseReserveInsuranceV2(AppSettings.ConnectionStringPM, SelectedInsuranceCompanyID);
                }
                if (PatientControl != null)
                {
                    ofrmInsuranceUseReserve.LoadedClaimPatientName = Convert.ToString(PatientControl.PatientName);
                    ofrmInsuranceUseReserve.LoadedClaimPatientID = Convert.ToInt64(PatientControl.PatientID);
                }
                if (txtCheckNumber.Text != "" && txtCheckNumber.Text != null)
                {
                    ofrmInsuranceUseReserve.LoadedCheckEOBPaymentID = _EOBPaymentID;
                }
                ofrmInsuranceUseReserve.ShowInTaskbar = false;
                ofrmInsuranceUseReserve.StartPosition = FormStartPosition.CenterScreen;
                
                ofrmInsuranceUseReserve.Reserves.EOBCreditDTL = this.oCreditResDTL.EOBCreditDTL.CopyCreditDTL();

                ofrmInsuranceUseReserve.ShowDialog(this);

                if (ofrmInsuranceUseReserve.DialogResult == DialogResult.OK)
                {
                    AmountTakenFromReserve = ofrmInsuranceUseReserve.AmountTakenFromReserve;
                    _useReserves = true;
                    IsReserveUsed = true;
                    oCreditResDTL.EOBCreditDTL = ofrmInsuranceUseReserve.Reserves.EOBCreditDTL.CopyCreditDTL();

                    OnRemainingCalculationChanged();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ofrmInsuranceUseReserve != null) { ofrmInsuranceUseReserve.Dispose(); }
                SetControlSelection();
            }
        

        }

        private void ReserveRemaining()
        {
            try
            {
                bool _allowToReserve = true;
                //_IsFromReserveRemaining = true;
                if (oCreditLine != null && oCreditLine.EOBReserves.Count > 0)
                {
                    for (int lnInd = 0; lnInd < oCreditLine.EOBReserves.Count; lnInd++)
                    {
                        if (oCreditLine.EOBReserves[lnInd].ReserveAmount > 0)
                        {
                            _allowToReserve = true;
                            break;
                        }
                    }
                }

                if (_allowToReserve == true)
                {
                    #region " Validate & Open Reserve Remaining Form "

                    if (IsReserveAdded)
                    { SendAmountToReserve(AmountAddedToReserve); }
                    else
                    {
                        if (IsValidForReserveRemaining())
                        { SendAmountToReserve(TotalFundsRemaining); }
                    }

                    if (AmountAddedToReserve == 0)
                    { IsReserveAdded = false; }
                    else
                    { IsReserveAdded = true; }

                    #endregion
                }
                else
                {
                    MessageBox.Show("The amount you are trying to reserve is already from reserve you cannot reserve this amount." + Environment.NewLine + "", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);               
            }
        }

        private void SendAmountToReserve(decimal ReserveAmount)
        {
            frmInsuranceReserveRemainingV2 ofrmInsuranceReserveRemaining = new frmInsuranceReserveRemainingV2(AppSettings.ConnectionStringPM, IsReserveAdded);
            try
            {
                DataTable dtBusinessCenter = null;
                ReserveDetails.InsCompanyID = SelectedInsuranceCompanyID;
                ReserveDetails.InsCompanyName = lblInsCompany.Text;
                ReserveDetails.ReserveAmount = ReserveAmount;
                ReserveDetails.ReserveNote = ReserveNote;
                ReserveDetails.nCreditID = EOBPaymentID;
                // If Claim opened in Correction mode then default the claims account business center
                dtBusinessCenter = gloCharges.GetPatientAccountBusinessCenter(ClaimDetails.PAccountID);
                ReserveDetails.ClaimsAccountID = ClaimDetails.PAccountID;
                if(dtBusinessCenter != null && dtBusinessCenter.Rows.Count > 0)
                {
                    ReserveDetails.BusinessCenterID = Convert.ToInt64(dtBusinessCenter.Rows[0]["nBusinessCenterID"]);
                }
                if (TakeBackAmount != 0 && TotalFundsRemaining != 0 && TakeBackAmount == TotalFundsRemaining && ReserveDetails.PatientID == 0)
                {

                    if (PatientControl != null)
                    {
                        ReserveDetails.PatientID = PatientControl.PatientID;
                        ReserveDetails.PatientName = PatientControl.PatientName;
                        ReserveDetails.MSTTransactionID = ClaimDetails.TransactionMasterID;
                        ReserveDetails.TransactionID = ClaimDetails.TransactionID;
                        ReserveDetails.ClaimNo = InsurancePayment.GetFormattedClaimPaymentNumber(Convert.ToString(PatientControl.ClaimNumber));
                    }
                }
                ofrmInsuranceReserveRemaining.ReserveDetails = this.ReserveDetails;
                ofrmInsuranceReserveRemaining.ShowInTaskbar = false;
                ofrmInsuranceReserveRemaining.StartPosition = FormStartPosition.CenterScreen;
                ofrmInsuranceReserveRemaining.ShowDialog(this);

                if (ofrmInsuranceReserveRemaining.DialogResult == DialogResult.OK)
                {

                    _isAssociationSave = true;
                    this.ReserveDetails = ofrmInsuranceReserveRemaining.ReserveDetails;
                    AmountAddedToReserve = ReserveDetails.ReserveAmount;
                    ReserveNote = ReserveDetails.ReserveNote;
                    //if check is loaded then dont change the current SelectedInsuranceCompanyID,SelectedInsuranceCompany
                    if (EOBPaymentID == 0)
                    {
                        SelectedInsuranceCompanyID = ReserveDetails.InsCompanyID;
                        SelectedInsuranceCompany = ReserveDetails.InsCompanyName;
                        lblInsCompany.Text = SelectedInsuranceCompany;
                    }
                    OnRemainingCalculationChanged();
                }
                else
                {
                    if (TakeBackAmount != 0 && TotalFundsRemaining != 0 && TakeBackAmount == TotalFundsRemaining && ReserveDetails.PatientID != 0)
                    {
                        if (!_isAssociationSave)
                        {
                            if (PatientControl != null)
                            {
                                ReserveDetails.PatientID = 0;
                                ReserveDetails.PatientName = "";
                                ReserveDetails.MSTTransactionID = 0;
                                ReserveDetails.TransactionID = 0;
                                ReserveDetails.ClaimNo = "";
                            }
                        }
                    }
                
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ofrmInsuranceReserveRemaining != null) { ofrmInsuranceReserveRemaining.Dispose(); }
            }
        }

        #endregion

        #region  " Save Payment Methods "

        private bool PerformSavePayment()
        {
            DateTime t = DateTime.Now;
            if (mskCheckDate.MaskCompleted == true)
            {
                mskCheckDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (mskCheckDate.Text.Trim() == "")
                {
                    mskCheckDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                    if (!DateTime.TryParse(mskCheckDate.Text, out t))
                    {
                        MessageBox.Show("Please enter the " + cmbPayMode.Text + " date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCheckDate.Focus();
                        return false;
                    }
                }
                else
                {
                    mskCheckDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                    if (!DateTime.TryParse(mskCheckDate.Text, out t))
                    {
                        MessageBox.Show("Please enter a valid date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCheckDate.Focus();
                        return false;
                    }
                }
            }
            else
            {
                mskCheckDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                
                if (mskCheckDate.Text.Trim() == "")
                { MessageBox.Show("Please enter the " + cmbPayMode.Text + " date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); mskCheckDate.Focus(); }
                else
                { MessageBox.Show("Please enter a valid date.  ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); mskCheckDate.Focus(); }
                mskCheckDate.TextMaskFormat = MaskFormat.IncludeLiterals;
                return false;
            }

            bool _isPaymentSaved = false;
            bool _isPaymentMade = IsPaymentMade();
            bool _isCheckUpdating = IsCheckUpdating();

            if (_isCheckUpdating)
            {
                if (!CheckAmount_Modify())
                {                    
                    _isPaymentSaved = false;
                }
            }

            if ((_isPaymentMade) || (IsNextActionUpdated) || (_isCheckUpdating))
            {
                // Check necessary validation before proceed 
                if (SavePaymentValidation(_isPaymentMade))
                {
                    Int64 nLastPaymentCloseDate = 0;
                    if (!_isPaymentMade && IsNextActionUpdated && !_isCheckUpdating)
                    {
                        if (!IsLessThenPreTransDate(ClaimDetails.TransactionID, mskCloseDate.Text.ToString(), ref nLastPaymentCloseDate))
                        {
                            MessageBox.Show("Payment Close Date [" + mskCloseDate.Text + "] is not allowed. Payment Close Date cannot come before last Payment Close Date [" + gloDateMaster.gloDate.DateAsDateString(nLastPaymentCloseDate) + "]", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            mskCloseDate.Focus();
                            mskCloseDate.Select();
                            return false;
                        }
                    }

                    _isPaymentSaved = SavePayment(_isPaymentMade, _isCheckUpdating);
                    if (_isPaymentSaved && txtCheckAmount.Enabled == true )
                    {
                        if (oCreditResDTL != null)
                        {
                            //oCreditResDTL.EOBCreditDTL.Clear();
                            //oCreditResDTL.EOBCreditDTL.Dispose();
                            //oCreditResDTL.Dispose();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No payment has been made to save. ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isPaymentSaved = false;
            }
            return _isPaymentSaved;
        }

        //private bool SavePayment(bool _isPaymentMade, bool _isCheckUpdating)
        //{
        //    bool _IsDataSaved = true;
        //    gloAccountsV2.gloInsurancePaymentV2 ObjaccPayment = null;
        //    SplitClaimDetails oSplitClaimDetails = new SplitClaimDetails();
        //    gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(AppSettings.ConnectionStringPM, "");
        //    DataTable _dtUniqueIDs = new DataTable();
        //    DataTable _dtUniqueIDsCorrection = new DataTable();
        //    DataTable _dtUniqueCreditID = new DataTable();
        //    Int32 row_num = 0;
        //    Int64 _nEOBID = 0;
        //    Int64 _nCreditID = 0;
        //    Boolean _bIsUsedReserveVal = false;
        //    try
        //    {
        //        dsInsurancePayment_TVP = new gloBilling.gloAccountPayment.dsPaymentTVP_V2();
        //         if ((_isPaymentMade && txtCheckAmount.Text.Trim() != "") || (_isCheckUpdating))
        //        {
        //            #region "Master Data"
        //            _dtUniqueCreditID = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);
        //            if (EOBPaymentID != 0)
        //            {
        //                SetCreditsDetails(dsInsurancePayment_TVP, EOBPaymentID);
        //            }
        //            else
        //            {
        //                SetCreditsDetails(dsInsurancePayment_TVP, _nCreditID);

        //                //Adding Unique KEY for Credit ID

        //                if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
        //                { _nCreditID = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID"].ToString()); }
        //                dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"] = _nCreditID;
        //            }

        //            #region "Payment Master Note"

        //            //Notes if any to main payment to all claim OR main payment to reserve account
        //            if (txtPayMstNotes.Text.Trim().Length > 0)
        //            {
        //                #region "General Note"

        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Add();
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = 0;
        //                if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
        //                {
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
        //                }
        //                if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
        //                {
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID2"].ToString());
        //                }

        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = 0;
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = 0;
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "";
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = txtPayMstNotes.Text.Trim();
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = AmountAddedToReserve;
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = NoteTypeV2.Payment_Insurance.GetHashCode();
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = NoteSubTypeV2.MasterPayment.GetHashCode();
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = chkPayMstIncludeNotes.Checked;
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = 0;
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = BillingNoteTypeV2.None.GetHashCode();
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DBNull.Value;
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = 0;
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = 0;
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = "";
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
        //                dsInsurancePayment_TVP.Tables["EOBNotes"].AcceptChanges();

        //                #endregion
        //            }

        //            #endregion

        //            #endregion "Master Data"

        //            #region "Claim Payment"
        //            bool _announceBillToSelf = true;
        //            _announceBillToSelf = ogloBilling.GetbIsSkipZeroBillingClaimSettingIPP();
        //            DataTable _dt = null;

        //            if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
        //            {
        //                _dtUniqueIDs = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(c1SinglePayment.Rows.Count - 1);

        //                #region "EOB Service Lines"
        //                for (int i = 1; i <= c1SinglePayment.Rows.Count - 1; i++)
        //                {
        //                    if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
        //                    {
        //                        if ((c1SinglePayment.GetData(i, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PAYMENT)).Trim() != ""))
        //                        {
        //                            #region "EOB Save"
        //                            if (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_BALANCE)) <= 0 && Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PatientPaidAmount)) <= 0&& _announceBillToSelf)
        //                            {
        //                                _announceBillToSelf = true;
        //                            }
        //                            else if (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_BALANCE)) < 0 && (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_BALANCE)) + Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PatientPaidAmount)) <= 0) && _announceBillToSelf)
        //                            {
        //                                _announceBillToSelf = true;
        //                            }
        //                            else
        //                            {
        //                                _announceBillToSelf = false;
        //                            }
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows.Add();
        //                            if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
        //                            {
        //                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCreditID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
        //                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
        //                            }
        //                            if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
        //                            {
        //                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["ID"].ToString());
        //                            }
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPatientID"] = PatientControl.PatientID;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPatientInsuranceID"] = PatientInsuranceID;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nContactID"] = ContactInsuranceID;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nInsCompanyID"] = SelectedInsuranceCompanyID;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));

        //                            if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PAYMENT)).Trim() != "")
        //                            {
        //                                if (c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_PAYMENT)).Trim() != "")
        //                                {
        //                                    DataTable _nCrDataTable = null;
        //                                    decimal _fillPayAmt = 0;
        //                                    decimal _crPayAmt = 0;
        //                                    if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null)
        //                                    {
        //                                        _fillPayAmt = (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT)));
        //                                        _crPayAmt = (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT)));
        //                                        _crPayAmt = _crPayAmt - (_crPayAmt * 2);
        //                                        _nCrDataTable = gloInsurancePaymentV2.GetCorrectionRefList(_crPayAmt, PatientControl.PatientID, Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID)), Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID)), this.PatientInsuranceID, this.ContactInsuranceID);
        //                                    }
        //                                    if (_nCrDataTable != null && _nCrDataTable.Rows.Count > 0)
        //                                    {
        //                                        if (Convert.ToInt64(_nCrDataTable.Rows[0]["nCreditID"].ToString()) == EOBPaymentID && _fillPayAmt < 0)
        //                                        {
        //                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.InsuranceCorrection.GetHashCode();
        //                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sEntryDesc"] = "InsCor";

        //                                        }
        //                                        else if (Convert.ToInt64(_nCrDataTable.Rows[0]["nCreditID"].ToString()) != EOBPaymentID && _fillPayAmt < 0)
        //                                        {
        //                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.TakeBack.GetHashCode();
        //                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sEntryDesc"] = "InsTB";

        //                                        }
        //                                        else
        //                                        {
        //                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.InsuracePayment.GetHashCode();
        //                                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sEntryDesc"] = "InsPmt";

        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.InsuracePayment.GetHashCode();
        //                                        dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sEntryDesc"] = "InsPmt";
        //                                    }
        //                                }
        //                            }


        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));

        //                            if (mskCloseDate.MaskCompleted == true)
        //                            {
        //                                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        //                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
        //                            }

        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPaymentTrayID"] = SelectedPaymentTrayID;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sPaymentTrayDesc"] = SelectedPaymentTray;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = DBNull.Value;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["bIsPaymentVoid"] = false;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nVoidType"] = 0;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPAccountID"] = this.nPAccountID;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nGuarantorID"] = this.nGuarantorID;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nAccountPatientID"] = this.nAccountPatientID;
        //                            if (c1SinglePayment.GetData(i, COL_TOTALCHARGE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_TOTALCHARGE)).Trim() != "")
        //                            {
        //                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dTotalChargeAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_TOTALCHARGE));
        //                            }

        //                            if (c1SinglePayment.GetData(i, COL_ALLOWED) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_ALLOWED)).Trim() != "")
        //                            {
        //                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dAllowedAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_ALLOWED));
        //                            }

        //                            if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF)).Trim() != "")
        //                            { dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dWriteoffAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_WRITEOFF)); }

        //                            if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PAYMENT)).Trim() != "")
        //                            { dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dPaymentAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)); }

        //                            if (c1SinglePayment.GetData(i, COL_COPAY) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_COPAY)).Trim() != "")
        //                            { dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dCopayAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_COPAY)); }

        //                            if (c1SinglePayment.GetData(i, COL_DEDUCTIBLE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_DEDUCTIBLE)).Trim() != "")
        //                            { dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dDeductibleAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_DEDUCTIBLE)); }

        //                            if (c1SinglePayment.GetData(i, COL_COINSURANCE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_COINSURANCE)).Trim() != "")
        //                            { dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dCoinsurance"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_COINSURANCE)); }

        //                            if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD)).Trim() != "")
        //                            { dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dWithholdAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_WITHHOLD)); }


        //                            #region "Retrieve Next Action For EOB EXT "

        //                            string _party = "";
        //                            string _partyCode = "";
        //                            string _partyDesc = "";
        //                            Int64 _partyInsId = 0;
        //                            Int64 _partyContactId = 0;
        //                            string _nextAction = "";
        //                            string _nextActionCode = "";

        //                            if (c1SinglePayment.GetData(i, COL_NEXT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT)).Trim() != "")
        //                            {
        //                                _nextAction = Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT)).Trim();
        //                                _nextActionCode = _nextAction.Substring(0, _nextAction.IndexOf('-'));
        //                            }


        //                            if (c1SinglePayment.GetData(i, COL_PARTY) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PARTY)).Trim() != "")
        //                            {

        //                                _party = Convert.ToString(c1SinglePayment.GetData(i, COL_PARTY)).Trim();
        //                                _partyCode = _party.Substring(0, _party.IndexOf('-'));
        //                                _partyDesc = _party.Substring(_party.IndexOf('-') + 1, (_party.Length - _party.IndexOf('-')) - 1);


        //                                _dt = null;
        //                                _dt = gloStripControl.PatientStripControl.GetInsuranceParties(Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID)), Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID))); 

        //                                if (_dt != null && _dt.Rows.Count > 0)
        //                                {
        //                                    for (int pIndex = 0; pIndex < _dt.Rows.Count; pIndex++)
        //                                    {
        //                                        if (Convert.ToString(_dt.Rows[pIndex]["nResponsibilityNo"]) == _partyCode)
        //                                        {
        //                                            _partyInsId = Convert.ToInt64(_dt.Rows[pIndex]["nInsuranceID"]);
        //                                            _partyContactId = Convert.ToInt64(_dt.Rows[pIndex]["nContactID"]);
        //                                            break;
        //                                        }
        //                                    }
        //                                }
        //                                if (_dt != null) { _dt.Dispose(); }
        //                            }

        //                            #endregion


        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEXTID"] = 0;
        //                            if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
        //                            {
        //                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCreditIDEXT"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
        //                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBIDEXT"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
        //                            }
        //                            if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
        //                            {
        //                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBDetailIDEXT"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["ID"].ToString());
        //                            }
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sNextActionEXT"] = _nextActionCode;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sNextPartyEXT"] = _partyDesc.Trim();
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nNextPartyIDEXT"] = _partyInsId;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtCreatedDateTimeEXT"] = DBNull.Value;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtModifiedDateTimeEXT"] = DBNull.Value;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtPaymentVoidDateTimeEXT"] = DBNull.Value;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nUserIDEXT"] = gloGlobal.gloPMGlobal.UserID;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sUserNameEXT"] = AppSettings.UserName;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["bIsERAPostEXT"] = false;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nSVCIDEXT"] = DBNull.Value;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sMachineNameEXT"] = Environment.MachineName;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sSiteIDEXT"] = DBNull.Value;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sVersionEXT"] = gloGlobal.gloPMGlobal.ApplicationVersion;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nClinicIDEXT"] = gloGlobal.gloPMGlobal.ClinicID;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCLPIdEXT"] = 0;
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionEXT"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
        //                            dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionDetailEXT"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
        //                            dsInsurancePayment_TVP.Tables["EOB"].AcceptChanges();
        //                            dsInsurancePayment_TVP.Tables["Debits"].AcceptChanges();

        //                            #region " Set Line Reason Codes "

        //                            //...Code added to auto set the adjustment codes for W/O,Copay,Dedutible,Coinsurance,Withhold
        //                            //...by reading there respective values from admin settings
        //                            string _code = "";

        //                            for (int colIndex = COL_WRITEOFF; colIndex <= COL_WITHHOLD; colIndex++)
        //                            {
        //                                _code = "";
        //                                if (c1SinglePayment.GetData(i, colIndex) != null && Convert.ToString(c1SinglePayment.GetData(i, colIndex)).Trim() != ""
        //                                    && Convert.ToDecimal(c1SinglePayment.GetData(i, colIndex)) != 0)
        //                                {
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Add();
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nID"] = 0;
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nClaimNo"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
        //                                    if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
        //                                    {
        //                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID2"].ToString());
        //                                    }

        //                                    if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
        //                                    {
        //                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
        //                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
        //                                    }
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sReasonCode"] = GetSelectedReasonCode(colIndex);
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sReasonDescription"] = gloAccountsV2.gloInsurancePaymentV2.GetReasonDescription(_code);
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["dReasonAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, colIndex));
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nType"] = EOBCommentTypeV2.SystemReasonCode.GetHashCode();
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["bIsVoid"] = 0;
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnDtlID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnLineNo"] = DBNull.Value;
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sSubClaimNo"] = Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nVoidType"] = 0;
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["bIsPaymentVoid"] = 0;
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nPaymentVoidCloseDate"] = DBNull.Value;
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
        //                                    dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nSubType"] = DBNull.Value;

        //                                }
        //                            }

        //                            if (c1SinglePayment.GetData(i, COL_REASON) != null)
        //                            {
        //                                C1FlexGrid oFlex = ((C1FlexGrid)(c1SinglePayment.GetData(i, COL_REASON)));
        //                                if (oFlex != null && oFlex.Rows.Count > 0)
        //                                {

        //                                    for (int rIndex = 1; rIndex < oFlex.Rows.Count; rIndex++)
        //                                    {
        //                                        if (Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Type"].Index)) == "Reason")
        //                                        {


        //                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Add();


        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["Id"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Id"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["Id"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["ClaimNo"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["ClaimNo"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nClaimNo"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["ClaimNo"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["SubClaimNo"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["SubClaimNo"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sSubClaimNo"] = Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["SubClaimNo"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["EOBPaymentID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["EOBPaymentID"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["EOBPaymentID"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["EOBID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["EOBID"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["EOBID"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["EOBPayDtlID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["EOBPayDtlID"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["EOBPayDtlID"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["BLTransactionID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["BLTransactionID"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["BLTransactionID"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["BLTransactionDtlID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["BLTransactionDtlID"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["BLTransactionDtlID"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionID"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionID"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionDtlID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionDtlID"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnDtlID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionDtlID"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["Code"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Code"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sReasonCode"] = Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Code"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["Description"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Description"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sReasonDescription"] = Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Description"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["Amount"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Amount"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["dReasonAmount"] = Convert.ToDecimal(oFlex.GetData(rIndex, oFlex.Cols["Amount"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["ClinicId"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["ClinicId"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nClinicID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["ClinicId"].Index)); }

        //                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nType"] = EOBCommentTypeV2.Reason.GetHashCode();
        //                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nID"] = 0;
        //                                            if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
        //                                            {
        //                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
        //                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
        //                                            }
        //                                            if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
        //                                            {
        //                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID2"].ToString());
        //                                            }
        //                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["bIsVoid"] = 0;
        //                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnLineNo"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_LINENO)); ;
        //                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nVoidType"] = 0;
        //                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["bIsPaymentVoid"] = 0;
        //                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nPaymentVoidCloseDate"] = DBNull.Value;
        //                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;

        //                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
        //                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nSubType"] = DBNull.Value;
        //                                        }
        //                                    }
        //                                }
        //                            }

        //                            #endregion " Set Line Reason Codes "

        //                            #region " Set Line Remark Codes "

        //                            if (c1SinglePayment.GetData(i, COL_REASON) != null)
        //                            {
        //                                C1FlexGrid oFlex = ((C1FlexGrid)(c1SinglePayment.GetData(i, COL_REASON)));
        //                                if (oFlex != null && oFlex.Rows.Count > 0)
        //                                {

        //                                    for (int rIndex = 1; rIndex < oFlex.Rows.Count; rIndex++)
        //                                    {
        //                                        if (Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Type"].Index)) == "Remark")
        //                                        {


        //                                            dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Add();


        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["Id"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Id"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nEOBRemarkID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["Id"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["ClaimNo"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["ClaimNo"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nClaimNo"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["ClaimNo"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["SubClaimNo"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["SubClaimNo"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["sSubClaimNo"] = Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["SubClaimNo"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["EOBPaymentID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["EOBPaymentID"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["EOBPaymentID"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["EOBID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["EOBID"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["EOBID"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["EOBPayDtlID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["EOBPayDtlID"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["EOBPayDtlID"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["BLTransactionID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["BLTransactionID"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["BLTransactionID"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["BLTransactionDtlID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["BLTransactionDtlID"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["BLTransactionDtlID"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionID"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nTrackTrnID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionID"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionDtlID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionDtlID"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nTrackTrnDtlID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionDtlID"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["Code"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Code"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["sRemarkCode"] = Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Code"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["Description"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Description"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["sRemarkDescription"] = Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Description"].Index)); }

        //                                            //if (oFlex.GetData(rIndex, oFlex.Cols["Amount"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Amount"].Index)).Trim() != "")
        //                                            //{ dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["dReasonAmount"] = Convert.ToDecimal(oFlex.GetData(rIndex, oFlex.Cols["Amount"].Index)); }

        //                                            if (oFlex.GetData(rIndex, oFlex.Cols["ClinicId"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["ClinicId"].Index)).Trim() != "")
        //                                            { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nClinicID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["ClinicId"].Index)); }

        //                                            dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nType"] = EOBCommentTypeV2.Reason.GetHashCode();
        //                                            dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nEOBRemarkID"] = 0;
        //                                            if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
        //                                            {
        //                                                dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
        //                                                dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
        //                                            }
        //                                            if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
        //                                            {
        //                                                dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID2"].ToString());
        //                                            }
        //                                            dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //                                            dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["bIsVoid"] = 0;
        //                                            dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nTrackTrnLineNo"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_LINENO)); ;
        //                                            dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nVoidType"] = 0;
        //                                            dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["bIsPaymentVoid"] = 0;
        //                                            dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nPaymentVoidCloseDate"] = DBNull.Value;
        //                                            dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;

        //                                            dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
        //                                            dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nSubType"] = DBNull.Value;
        //                                            dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["sReasonCode"] = Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["RemarkReasonCodes"].Index));
        //                                        }
        //                                    }
        //                                }
        //                            }

        //                            #endregion " Set Line Remark Codes "

        //                            #region " Statement Notes & Internal Notes for Line "

        //                            if (c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTE)).Trim() != "")
        //                            {

        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Add();
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
        //                                if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
        //                                {
        //                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
        //                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
        //                                }

        //                                if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
        //                                {
        //                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID2"].ToString());
        //                                }
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "";
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTE)).Trim();
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = 0;
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = NoteTypeV2.Payment_Insurance.GetHashCode();
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = NoteSubTypeV2.StatementNote.GetHashCode();
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = Convert.ToBoolean(c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTEONPRINT));
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = 0;
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = BillingNoteTypeV2.PaymentStatementNote.GetHashCode();
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DBNull.Value;
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;

        //                            }

        //                            if (c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTE)).Trim() != "")
        //                            {
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Add();
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
        //                                if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
        //                                {
        //                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
        //                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
        //                                }
        //                                if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
        //                                {
        //                                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID2"].ToString());
        //                                }

        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "";
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTE)).Trim();
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = 0;
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = NoteTypeV2.Payment_Insurance.GetHashCode();
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = NoteSubTypeV2.InternalNote.GetHashCode();
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = Convert.ToBoolean(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTEONPRINT));
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = 0;
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = BillingNoteTypeV2.PaymentInternalNote.GetHashCode();
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DBNull.Value;
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
        //                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;

        //                            }

        //                            dsInsurancePayment_TVP.Tables["ReasonCode"].AcceptChanges();
        //                            dsInsurancePayment_TVP.Tables["EOBNotes"].AcceptChanges();
        //                            dsInsurancePayment_TVP.Tables["RemarkCode"].AcceptChanges();
        //                            #endregion " Statement Notes & Internal Notes for Line "

        //                            #endregion "EOB Save"

        //                            #region "Debit Service Line - Insurance - Allocation from Current Check, Correction/Takeback, Use Reserved"

        //                            if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PAYMENT)).Trim() != "")
        //                            {
        //                                if (c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_PAYMENT)).Trim() != "")
        //                                {
        //                                    decimal _fillPayAmt = 0;
        //                                    decimal _crPayAmt = 0;

        //                                    DataTable _nCrDataTable = null;

        //                                    if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null)
        //                                    {
        //                                        _fillPayAmt = (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT)));
        //                                        _crPayAmt = (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT)));
        //                                        _crPayAmt = _crPayAmt - (_crPayAmt * 2);
        //                                        _nCrDataTable = gloInsurancePaymentV2.GetCorrectionRefList(_crPayAmt, PatientControl.PatientID, Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID)), Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID)), this.PatientInsuranceID, this.ContactInsuranceID);
        //                                    }

        //                                    else if (c1SinglePayment.GetData(i, COL_PAYMENT) != null)
        //                                    {
        //                                        _fillPayAmt = (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)));
        //                                    }
        //                                    else if (c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null)
        //                                    {
        //                                        _fillPayAmt = 0 - (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT)));

        //                                    }

        //                                    #region "Set Debits object"
        //                                    if (_nCrDataTable.Rows.Count > 0)
        //                                    {
        //                                        _dtUniqueIDsCorrection = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(_nCrDataTable.Rows.Count);
        //                                        for (int nCrDTIndex = 0; nCrDTIndex < _nCrDataTable.Rows.Count; nCrDTIndex++)
        //                                        {
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows.Add();
        //                                            if (Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nCreditID"].ToString()) != EOBPaymentID && _fillPayAmt < 0 && (_fillPayAmt <= Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nLineAmt"])))
        //                                            {
        //                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCredit_RefID"] = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nCreditID"].ToString());
        //                                            }
        //                                            else
        //                                            {
        //                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCredit_RefID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
        //                                            }


        //                                            if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
        //                                            {
        //                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCreditID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
        //                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
        //                                            }
        //                                            if (_dtUniqueIDsCorrection != null && _dtUniqueIDsCorrection.Rows.Count > 0)
        //                                            {
        //                                                if (Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID)) == Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nBillingTransactionID"].ToString())
        //                                                    && Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID)) == Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nBillingTransactionDetailID"].ToString()))
        //                                                {
        //                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBDetailID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBDetailID"].ToString());
        //                                                }
        //                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = Convert.ToInt64(_dtUniqueIDsCorrection.Rows[nCrDTIndex]["ID2"].ToString());
        //                                            }

        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));

        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTDesc"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dPaymentAmount"] = Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]) - (Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]) * 2); ;

        //                                            if (nCrDTIndex == 0)
        //                                            {
        //                                                if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null && c1SinglePayment.GetData(i, COL_WRITEOFF).ToString().Trim() != "")
        //                                                {
        //                                                    if (c1SinglePayment.GetData(i, COL_ISCORRECTION) != null && Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISCORRECTION)) == true)
        //                                                    {

        //                                                        if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF)).Trim() != "")
        //                                                        {
        //                                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWriteoffAmount"] = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF))) - Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WRITEOFF)));
        //                                                        }
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF)).Trim() != "")
        //                                                        {
        //                                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWriteoffAmount"] = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF)));
        //                                                        }
        //                                                    }
        //                                                }


        //                                                if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null && c1SinglePayment.GetData(i, COL_WITHHOLD).ToString().Trim() != "")
        //                                                {
        //                                                    if (c1SinglePayment.GetData(i, COL_ISCORRECTION) != null && Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISCORRECTION)) == true)
        //                                                    {
        //                                                        if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD)).Trim() != "")
        //                                                        {
        //                                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWithholdAmount"] = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD))) - Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WITHHOLD)));
        //                                                        }
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD)).Trim() != "")
        //                                                        {
        //                                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWithholdAmount"] = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD)));
        //                                                        }
        //                                                    }
        //                                                }
        //                                            }

        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientInsuranceID"] = PatientInsuranceID;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nInsCompanyID"] = SelectedInsuranceCompanyID;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nContactID"] = ContactInsuranceID;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientID"] = PatientControl.PatientID;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentTrayID"] = SelectedPaymentTrayID;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentTrayDesc"] = SelectedPaymentTray;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sUserName"] = AppSettings.UserName;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;

        //                                            if (c1SinglePayment.GetData(i, COL_ISCORRECTION) != null && Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISCORRECTION)) == true)
        //                                            {
        //                                                if (_nCrDataTable != null && _nCrDataTable.Rows.Count > 0)
        //                                                {
        //                                                    if (Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nCreditID"].ToString()) == EOBPaymentID && _fillPayAmt < 0)
        //                                                    {
        //                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.InsuranceCorrection.GetHashCode();
        //                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sEntryDesc"] = "InsCor";

        //                                                    }
        //                                                    else if (Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nCreditID"].ToString()) != EOBPaymentID && _fillPayAmt < 0)
        //                                                    {
        //                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.TakeBack.GetHashCode();
        //                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sEntryDesc"] = "InsTB";

        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.InsuracePayment.GetHashCode();
        //                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sEntryDesc"] = "InsPmt";

        //                                                    }
        //                                                }
        //                                            }
        //                                            else
        //                                            {
        //                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.InsuracePayment.GetHashCode();
        //                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sEntryDesc"] = "InsPmt";
        //                                            }

        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nGuarantorID"] = this.nGuarantorID;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPAccountID"] = this.nPAccountID;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nAccountPatientID"] = this.nAccountPatientID;
        //                                            if (mskCloseDate.MaskCompleted == true)
        //                                            {
        //                                                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        //                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
        //                                            }

        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dOtherAdjustmentAmount"] = DBNull.Value;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = DBNull.Value;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsPaymentVoid"] = false;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nVoidType"] = 0;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidDateTime"] = DBNull.Value;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidCloseDate"] = DBNull.Value;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidDateTime"] = DBNull.Value;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsERAPost"] = DBNull.Value;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCreatedDateTime"] = DBNull.Value;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sSiteID"] = DBNull.Value;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].AcceptChanges();

        //                                            if (Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nCreditID"].ToString()) != EOBPaymentID && _fillPayAmt < 0)
        //                                            {
        //                                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Add();
        //                                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nCreditsDTL_ID"] = 0;
        //                                                if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
        //                                                {
        //                                                    dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nCreditID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
        //                                                }
        //                                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nCreditsRef_ID"] = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nCreditID"].ToString());
        //                                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nReserveRef_ID"] = 0;
        //                                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dAmount"] = Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"].ToString());
        //                                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.TakeBack.GetHashCode();
        //                                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["sEntryDesc"] = "TB";

        //                                                if (mskCloseDate.MaskCompleted == true)
        //                                                {
        //                                                    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        //                                                    dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
        //                                                }
        //                                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dtCreatedDateTime"] = DBNull.Value;
        //                                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
        //                                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["bIsPaymentVoid"] = false;

        //                                                dsInsurancePayment_TVP.Tables["CreditsDTL"].AcceptChanges();
        //                                            }
        //                                        }

        //                                    }
        //                                    else
        //                                    {
        //                                        //Bug #64688: 00000631 : Error message says that "there is no row at position 5". 
        //                                        //Added to generate new unique id and used if uniqueID not available in _dtUniqueIDs id.
        //                                        _dtUniqueIDsCorrection = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);
                                                
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows.Add();
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCredit_RefID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());

        //                                        if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
        //                                        {
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCreditID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
        //                                            //if (dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCredit_RefID"] != null)
        //                                            //{
        //                                            //    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCredit_RefID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
        //                                            //}
        //                                        }
        //                                        if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
        //                                        {
        //                                            //Bug #64688: 00000631 : Error message says that "there is no row at position 5".
        //                                            //Commented the following line.
        //                                            //1. Make entry for nEOBDetailID as get ID form EOB table of dsInsurancePayment_TVP dataset and assing to nEOBDetailID.
        //                                            //2. Added new logic if uniqueID not available in _dtUniqueIDs then used newly generated id from _dtUniqueIDsCorrection.

        //                                            //dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["ID"].ToString());
        //                                            //dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["ID2"].ToString());

        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBDetailID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBDetailID"].ToString());

        //                                            if (_dtUniqueIDs != null && ((_dtUniqueIDs.Rows.Count - 1) >= (dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1)))
        //                                            {
        //                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["ID2"].ToString());
        //                                            }
        //                                            else
        //                                            { dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = Convert.ToInt64(_dtUniqueIDsCorrection.Rows[0]["ID2"].ToString()); }
        //                                        }

        //                                        //if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
        //                                        //{
        //                                        //    if (_dtUniqueIDs.Rows.Count == Convert.ToInt64(dsInsurancePayment_TVP.Tables["Debits"].Rows.Count))
        //                                        //    {
        //                                        //        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["ID"].ToString());
        //                                        //        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["ID2"].ToString());
        //                                        //    }
        //                                        //    else
        //                                        //    {
        //                                        //        if (dsInsurancePayment_TVP.Tables["Debits"] != null && Convert.ToInt64(dsInsurancePayment_TVP.Tables["Debits"].Rows.Count) > 0)
        //                                        //        {
        //                                        //            DataTable _dtUniqueDebitIDs = new DataTable();
        //                                        //            _dtUniqueDebitIDs = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(dsInsurancePayment_TVP.Tables["Debits"].Rows.Count);

        //                                        //            for (int iVar = 0; iVar <= dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1; iVar++)
        //                                        //            {
        //                                        //                dsInsurancePayment_TVP.Tables["Debits"].Rows[iVar]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueDebitIDs.Rows[iVar]["ID"].ToString());
        //                                        //                dsInsurancePayment_TVP.Tables["Debits"].Rows[iVar]["nDebitID"] = Convert.ToInt64(_dtUniqueDebitIDs.Rows[iVar]["ID2"].ToString());
        //                                        //            }

        //                                        //        }
        //                                        //    }
        //                                        //}

        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));

        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTDesc"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dPaymentAmount"] = _fillPayAmt;


        //                                        if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null && c1SinglePayment.GetData(i, COL_WRITEOFF).ToString().Trim() != "")
        //                                        {
        //                                            if (c1SinglePayment.GetData(i, COL_ISCORRECTION) != null && Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISCORRECTION)) == true)
        //                                            {

        //                                                if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF)).Trim() != "")
        //                                                {
        //                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWriteoffAmount"] = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF))) - Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WRITEOFF)));
        //                                                }
        //                                            }
        //                                            else
        //                                            {
        //                                                if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF)).Trim() != "")
        //                                                {
        //                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWriteoffAmount"] = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF)));
        //                                                }
        //                                            }
        //                                        }


        //                                        if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null && c1SinglePayment.GetData(i, COL_WITHHOLD).ToString().Trim() != "")
        //                                        {
        //                                            if (c1SinglePayment.GetData(i, COL_ISCORRECTION) != null && Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISCORRECTION)) == true)
        //                                            {
        //                                                if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD)).Trim() != "")
        //                                                {
        //                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWithholdAmount"] = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD))) - Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WITHHOLD)));
        //                                                }
        //                                            }
        //                                            else
        //                                            {
        //                                                if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD)).Trim() != "")
        //                                                {
        //                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWithholdAmount"] = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD)));
        //                                                }
        //                                            }
        //                                        }

        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientInsuranceID"] = PatientInsuranceID;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nInsCompanyID"] = SelectedInsuranceCompanyID;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nContactID"] = ContactInsuranceID;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientID"] = PatientControl.PatientID;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentTrayID"] = SelectedPaymentTrayID;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentTrayDesc"] = SelectedPaymentTray;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sUserName"] = AppSettings.UserName;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;

        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.InsuracePayment.GetHashCode();
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sEntryDesc"] = "InsPmt";

        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nGuarantorID"] = this.nGuarantorID;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPAccountID"] = this.nPAccountID;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nAccountPatientID"] = this.nAccountPatientID;
        //                                        if (mskCloseDate.MaskCompleted == true)
        //                                        {
        //                                            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        //                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
        //                                        }

        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dOtherAdjustmentAmount"] = DBNull.Value;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = DBNull.Value;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsPaymentVoid"] = false;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nVoidType"] = 0;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidDateTime"] = DBNull.Value;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidCloseDate"] = DBNull.Value;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidDateTime"] = DBNull.Value;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsERAPost"] = DBNull.Value;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCreatedDateTime"] = DBNull.Value;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sSiteID"] = DBNull.Value;
        //                                        dsInsurancePayment_TVP.Tables["Debits"].AcceptChanges();
                                                
        //                                    }
        //                                    #endregion
        //                                }
        //                            }
        //                            #endregion
        //                        }
        //                    }
        //                }
        //                #endregion
        //                DataRow[] _dr = dsInsurancePayment_TVP.Tables["EOB"].Select("sNextPartyEXT='Self'");

        //                    //.Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sNextPartyEXT"]
        //                if (_announceBillToSelf && _dr.Length<=0 && _dt!=null)
        //                {
        //                    string _transfertoSelfAnnouncment = "Warning - $0.00 Secondary Claim"+
        //                    "" + Environment.NewLine + "" +
        //                    "" + Environment.NewLine + "" +
        //                    "Charges have been fully paid by primary." +
        //                    ""+Environment.NewLine+"" +
        //                    "" + Environment.NewLine + "" +
        //                    "Skip Secondary and bill the patient?"+
        //                    "" + Environment.NewLine + "" +
        //                    "" + Environment.NewLine + "" +
        //                    "Yes - Transfer all the charge(s) on claim to patient" +
        //                    "" + Environment.NewLine + "" +
        //                    "" + Environment.NewLine + "" +
        //                    "No  - Continue with current selection"+
        //                    "" + Environment.NewLine + "" +
        //                    "" + Environment.NewLine + "" +
        //                    "Cancel - Stop and review the payment";


        //                    switch (MessageBox.Show(_transfertoSelfAnnouncment, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
        //                    {
                               
        //                        case DialogResult.Cancel:
        //                            return false;
        //                           // break;
        //                        case DialogResult.No:
        //                            break;
        //                        case DialogResult.None:
        //                            break;
        //                         case DialogResult.Yes:
        //                            for (int i = 1; i <= c1SinglePayment.Rows.Count - 1; i++)
        //                            {
        //                                #region "Set Next Action Bill And ResponsibleParty to Self"

        //                                c1SinglePayment.BeginUpdate();
        //                                c1SinglePayment.SetData(i, COL_NEXT, "B-Bill");
        //                                c1SinglePayment.SetData(i, COL_PARTY, Convert.ToString(_dt.Rows.Count) + "-Self");
        //                                c1SinglePayment.EndUpdate();

        //                                #endregion
        //                            }

        //                            for (int _nrow = 0; _nrow < dsInsurancePayment_TVP.Tables["EOB"].Rows.Count;_nrow++ )
        //                            {
        //                                dsInsurancePayment_TVP.Tables["EOB"].Rows[_nrow]["sNextActionEXT"] = "B";
        //                                dsInsurancePayment_TVP.Tables["EOB"].Rows[_nrow]["sNextPartyEXT"] = "Self";
        //                                dsInsurancePayment_TVP.Tables["EOB"].Rows[_nrow]["nNextPartyIDEXT"] = 0;
        //                                dsInsurancePayment_TVP.Tables["EOB"].AcceptChanges();
        //                            }
        //                            break;
                               
        //                    }
                           
        //                }
        //            }
        //            #endregion "Claim Payment"

        //            #region "Reserve Debit Entry "
        //            if (IsReserveAdded)
        //            {
        //                decimal _reserveAmt = 0;
        //                _reserveAmt = AmountAddedToReserve;

        //                //0 ReserveAmount 
        //                //1 ReserveNote 
        //                //3 ReserveNoteOnPrint 

        //                if (AmountAddedToReserve > 0)
        //                {
        //                    dsInsurancePayment_TVP.Tables["Reserves"].Rows.Add();
        //                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nReserveID"] = 0;
        //                    if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
        //                    {
        //                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nCreditID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
        //                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nCredits_RefID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
        //                    }
        //                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dReserveAmount"] = _reserveAmt;
        //                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nReserveType"] = ReserveEntryTypeV2.InsuraceReserve.GetHashCode();
        //                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nInsCompanyID"] = SelectedInsuranceCompanyID;
        //                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nPatientID"] = PatientControl.PatientID;
        //                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
        //                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["sUserName"] = AppSettings.UserName;
        //                    if (mskCloseDate.MaskCompleted == true)
        //                    {
        //                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        //                        dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
        //                    }
        //                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["bIsPaymentVoid"] = false;
        //                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nVoidType"] = 0;
        //                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
        //                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nPAccountID"] = 0;
        //                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nGuarantorID"] = 0;
        //                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nAccountPatientID"] = 0;
        //                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtPaymentVoidDateTime"] = DBNull.Value;
        //                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtCreatedDateTime"] = DBNull.Value;
        //                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
        //                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["sReserveNote"] = "Reserved";
        //                    dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
        //                    dsInsurancePayment_TVP.Tables["Reserves"].AcceptChanges();

        //                    dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Add();
        //                    dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
        //                    dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nEOBPaymentDetailID"] = 0;
        //                    dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nTransactionID"] = ReserveDetails.MSTTransactionID;
        //                    dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nTrackTrnID"] = ReserveDetails.TransactionID;
        //                    dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nPatientID"] = ReserveDetails.PatientID;
        //                    dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nProviderID"] = ReserveDetails.ProviderID;
        //                    dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nBusinessCenterID"] = ReserveDetails.BusinessCenterID;
                            
        //                    dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].AcceptChanges();

        //                    #region "General Note"

        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Add();
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = 0;
        //                    if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
        //                    {
        //                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
        //                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
        //                    }
        //                    if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
        //                    {
        //                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID2"].ToString());
        //                    }

        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = 0;
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = 0;
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "";
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = ReserveNote.Trim();
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = AmountAddedToReserve;
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = NoteTypeV2.Payment_InsuranceReserved.GetHashCode();
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = NoteSubTypeV2.MasterPayment.GetHashCode();
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = false;
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = 0;
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = BillingNoteTypeV2.None.GetHashCode();
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DBNull.Value;
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = 0;
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = 0;
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = "";
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
        //                    dsInsurancePayment_TVP.Tables["EOBNotes"].AcceptChanges();

        //                    #endregion

        //                }
        //            }



        //            //Insert credit detail entry for Used reserved ..........

        //            if (_useReserves)
        //            {

        //                if (txtCheckAmount.Enabled == false)
        //                {
        //                    _TotalAllocation = 0;
        //                    if (PaymentAction == FormMode.NewPaymentMode)
        //                    {
        //                        if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
        //                        {
        //                            for (int rIndex = 1; rIndex < c1SinglePayment.Rows.Count; rIndex++)
        //                            {
        //                                if (c1SinglePayment.GetData(rIndex, COL_PAYMENT) != null
        //                                   && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_PAYMENT)).Trim() != "")
        //                                {
        //                                    _TotalAllocation = _TotalAllocation + Convert.ToDecimal((c1SinglePayment.GetData(rIndex, COL_PAYMENT)));
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (c1SinglePaymentCorrTB != null && c1SinglePaymentCorrTB.Rows.Count > 1)
        //                        {
        //                            for (int rIndex = 1; rIndex < c1SinglePaymentCorrTB.Rows.Count; rIndex++)
        //                            {
        //                                if (c1SinglePaymentCorrTB.GetData(rIndex, COL_PAYMENT) != null
        //                                   && Convert.ToString(c1SinglePaymentCorrTB.GetData(rIndex, COL_PAYMENT)).Trim() != ""
        //                                   && Convert.ToDecimal(c1SinglePaymentCorrTB.GetData(rIndex, COL_PAYMENT)) > 0)
        //                                {
        //                                    _TotalAllocation = _TotalAllocation + Convert.ToDecimal((c1SinglePaymentCorrTB.GetData(rIndex, COL_PAYMENT)));
        //                                }
        //                            }
        //                        }
        //                    }

        //                    oCreditResDTL_CopyForRule.EOBCreditDTL = oCreditResDTL.EOBCreditDTL.CopyCreditDTL(); 

        //                }

        //                for (int index = 0; index < oCreditResDTL.EOBCreditDTL.Count; index++)
        //                {
        //                    if ((oCreditResDTL.EOBCreditDTL[index].Amount - oCreditResDTL.EOBCreditDTL[index].DBReserveAmount) > 0)
        //                    {
        //                        dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Add();
        //                        if (oCreditResDTL.EOBCreditDTL[index].Amount > 0)
        //                        {
        //                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nCreditsDTL_ID"] = oCreditResDTL.EOBCreditDTL[index].CreditsDTL_ID;
        //                            if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
        //                            {
        //                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nCreditID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
        //                            }
        //                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nCreditsRef_ID"] = oCreditResDTL.EOBCreditDTL[index].CreditsRef_ID;
        //                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nReserveRef_ID"] = oCreditResDTL.EOBCreditDTL[index].ReserveID;

        //                            if (txtCheckAmount.Enabled == false)
        //                            {
        //                                if (_TotalAllocation <= oCreditResDTL.EOBCreditDTL[index].Amount - oCreditResDTL.EOBCreditDTL[index].DBReserveAmount)
        //                                {

        //                                    dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dAmount"] = _TotalAllocation + oCreditResDTL.EOBCreditDTL[index].DBReserveAmount;
        //                                    _TotalAllocation = _TotalAllocation - Convert.ToDecimal(dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dAmount"]);
        //                                    //oCreditResDTL.EOBCreditDTL[index].Amount = Convert.ToDecimal(oCreditResDTL.EOBCreditDTL[index].Amount) - Convert.ToDecimal(dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dAmount"]);


        //                                }
        //                                else
        //                                {
        //                                    dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dAmount"] = oCreditResDTL.EOBCreditDTL[index].Amount;
        //                                    _TotalAllocation = _TotalAllocation - (oCreditResDTL.EOBCreditDTL[index].Amount - oCreditResDTL.EOBCreditDTL[index].DBReserveAmount);
        //                                    //oCreditResDTL.EOBCreditDTL[index].Amount = Convert.ToDecimal(oCreditResDTL.EOBCreditDTL[index].Amount) - Convert.ToDecimal(dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dAmount"]);
        //                                }

        //                            }
        //                            else
        //                            {
        //                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dAmount"] = oCreditResDTL.EOBCreditDTL[index].Amount;
        //                            }

        //                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.UseReserve.GetHashCode();
        //                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["sEntryDesc"] = "UR";

        //                            //dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dtCloseDate"] = oCreditResDTL.EOBCreditDTL[index].CloseDate;
        //                            if (mskCloseDate.MaskCompleted == true)
        //                            {
        //                                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        //                                if (oCreditResDTL.EOBCreditDTL[index].CloseDate > Convert.ToDateTime(mskCloseDate.Text))
        //                                {
        //                                    MessageBox.Show("The used reserved amount close date is in future than the current payment close date. Please select a different payment close date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                                    _bIsUsedReserveVal = true;
        //                                    return false;
        //                                }
        //                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
        //                            }

        //                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dtCreatedDateTime"] = DBNull.Value;
        //                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
        //                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["bIsPaymentVoid"] = false;
        //                            dsInsurancePayment_TVP.Tables["CreditsDTL"].AcceptChanges();
        //                        }

        //                        if (_TotalAllocation <= 0 && txtCheckAmount.Enabled == false)
        //                        {
        //                            break;
        //                        }
        //                    }
        //                }


        //            }
        //            #endregion
        //        }

        //        //if (dsInsurancePayment_TVP.Tables["Debits"] != null && Convert.ToInt64(dsInsurancePayment_TVP.Tables["Debits"].Rows.Count) > 0)
        //        //{

        //        //    DataView dv = dsInsurancePayment_TVP.Tables["Debits"].DefaultView;
        //        //    dv.RowFilter = "nEntryType = 4";
        //        //    DataTable dt = dv.ToTable();
        //        //    _dtUniqueIDs = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(dt.Rows.Count);
        //        //    for (int i = 0; i <= dt.Rows.Count - 1; i++)
        //        //    {
        //        //        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
        //        //        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID2"].ToString());
        //        //    }

        //        //    for (int i = 0; i <= dt.Rows.Count - 1; i++)
        //        //    {
        //        //        dsInsurancePayment_TVP.Tables["EOB"].Rows[i]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
        //        //        dsInsurancePayment_TVP.Tables["EOB"].Rows[i]["nEOBDetailIDEXT"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
        //        //    }

        //        //    //DataView dv = dsInsurancePayment_TVP.Tables["Debits"].DefaultView;
        //        //    //dv.RowFilter = "nEntryType = 4";
        //        //    //DataTable dt = dv.ToTable();
        //        //    //_dtUniqueIDs = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(dt.Rows.Count);
        //        //    //for (int i = 0; i <= dt.Rows.Count - 1; i++)
        //        //    //{
        //        //    //    dsInsurancePayment_TVP.Tables["Debits"].Rows[i]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
        //        //    //    dsInsurancePayment_TVP.Tables["Debits"].Rows[i]["nDebitID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID2"].ToString());
        //        //    //}

        //        //    //for (int i = 0; i <= dt.Rows.Count - 1; i++)
        //        //    //{
        //        //    //    dsInsurancePayment_TVP.Tables["EOB"].Rows[i]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
        //        //    //    dsInsurancePayment_TVP.Tables["EOB"].Rows[i]["nEOBDetailIDEXT"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
        //        //    //}
        //        //}

        //        #region " Save EOB Payment & Next action "
        //        gloAccountsV2.PaymentCollection.PaymentInsuranceLineNextActions oNextActions = new gloAccountsV2.PaymentCollection.PaymentInsuranceLineNextActions();
        //        oSplitClaimDetails = new SplitClaimDetails();
        //        SetNextActionDetails(out oSplitClaimDetails);
        //        DataTable _dtHoldInfo = GetSplittedClaimsHoldInfo(oSplitClaimDetails);
        //        if (_dtHoldInfo != null && _dtHoldInfo.Rows.Count > 0)
        //        {
        //            for (int holdIndex = 0; holdIndex < _dtHoldInfo.Rows.Count; holdIndex++)
        //            {
        //                dsInsurancePayment_TVP.Tables["HoldSelection"].Rows.Add();
        //                dsInsurancePayment_TVP.Tables["HoldSelection"].Rows[dsInsurancePayment_TVP.Tables["HoldSelection"].Rows.Count - 1]["IsHold"] = _dtHoldInfo.Rows[holdIndex]["IsHold"];
        //                dsInsurancePayment_TVP.Tables["HoldSelection"].Rows[dsInsurancePayment_TVP.Tables["HoldSelection"].Rows.Count - 1]["SubClaimNo"] = _dtHoldInfo.Rows[holdIndex]["SubClaimNo"];
        //                dsInsurancePayment_TVP.Tables["HoldSelection"].Rows[dsInsurancePayment_TVP.Tables["HoldSelection"].Rows.Count - 1]["PartyNo"] = _dtHoldInfo.Rows[holdIndex]["PartyNo"];
        //                dsInsurancePayment_TVP.Tables["HoldSelection"].Rows[dsInsurancePayment_TVP.Tables["HoldSelection"].Rows.Count - 1]["NextAction"] = _dtHoldInfo.Rows[holdIndex]["NextAction"];
        //                dsInsurancePayment_TVP.Tables["HoldSelection"].Rows[dsInsurancePayment_TVP.Tables["HoldSelection"].Rows.Count - 1]["Party"] = _dtHoldInfo.Rows[holdIndex]["Party"];
        //                dsInsurancePayment_TVP.Tables["HoldSelection"].AcceptChanges();
        //            }
        //        }

        //        if (dsInsurancePayment_TVP != null)
        //        {
        //            ObjaccPayment = new gloAccountsV2.gloInsurancePaymentV2();
        //            ObjaccPayment.ClaimRemitRefNo = txtClaimRemittanceRef.Text.Trim();
        //            ObjaccPayment.InsuranceParty =  PatientControl.SelectedInsuranceParty;
                    
        //            if (EOBPaymentID > 0)
        //            {
        //                ObjaccPayment.SavePayment(dsInsurancePayment_TVP);
        //            }
        //            else
        //            {
        //                EOBPaymentID = ObjaccPayment.SavePayment(dsInsurancePayment_TVP);
        //            }

        //            #region "Follow-Up Operations for Account"

        //            gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(AppSettings.ConnectionStringPM);
        //            CL_FollowUpCode.SetAutoAccountFollowUp(PatientControl.PAccountID, PatientControl.PatientID, Convert.ToDateTime(mskCloseDate.Text.Trim()));

        //            #endregion "Follow-Up Operations for Account"

                   
        //            oSettings.AddSetting("PAYMENT_LASTCLOSEDATE", Convert.ToDateTime(mskCloseDate.Text).ToString("MM/dd/yyyy"), gloGlobal.gloPMGlobal.ClinicID, gloGlobal.gloPMGlobal.UserID, gloSettings.SettingFlag.User);
        //            oSettings.AddSetting("PAYMENT_LASTCLOSETRAYID", SelectedPaymentTrayID.ToString(), gloGlobal.gloPMGlobal.ClinicID, gloGlobal.gloPMGlobal.UserID, gloSettings.SettingFlag.User);
        //            oSettings.Dispose();

        //            ogloBilling.SaveUserWiseCloseDay(mskCloseDate.Text.Trim(), CloseDayType.Payment, gloGlobal.gloPMGlobal.ClinicID);
        //        }

        //        #endregion

        //        #region " Reset parameters & controls "

        //        _IsDataSaved = true;

        //        IsPendingCheckLoaded = true;

        //        // Reset reserve added 
        //        IsReserveAdded = false;

        //        // Reset controls & load EOB
        //        SetupControls();
        //        if (_dtUniqueCreditID.Rows.Count > 0)
        //            _nEOBID = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
        //        else
        //            _nEOBID = 0;


        //        //..Clearing the reserves details object after save, the object is again set in LoadFormData method
        //        //..the object was intially cleared in the PerformSavePayment() method which was called after LoadFormData
        //        //..causing the object to get cleared when done save.
        //        //....


                
        //        if (oCreditResDTL != null)
        //        {
        //            oCreditResDTL.EOBCreditDTL.Clear();
        //        }
                

        //        LoadFormData(_EOBPaymentID, _nEOBID, true);
               
        //        //added By Shweta to clear Reserve associartion after save payment

        //        if(_IsDataSaved)
        //        {
        //            ReserveDetails.PatientID = 0;
        //            ReserveDetails.MSTTransactionID = 0;
        //            ReserveDetails.TransactionID = 0;
        //            ReserveDetails.ClaimNo = "";
        //            ReserveDetails.PatientName = "";
        //            _isAssociationSave = false;

        //        }

        //        //..Release Claim lock after successfully saving payment against claim
        //        ReleaseLockStatus(ClaimDetails.TransactionMasterID);
        //        //..Release Claim lock after successfully saving payment against claim

        //        // Reset the patient strip control
        //        PatientControl.ClearDetails();
        //        PatientControl.SelectSearchBox();

        //        if (!_bIsUsedReserveVal && txtCheckAmount.Enabled == true)
        //        {
        //            _useReserves = false;
        //        }
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //        _IsDataSaved = false;
        //    }
        //    finally
        //    {
        //        if (ObjaccPayment != null) { ObjaccPayment.Dispose(); }
        //        if (ogloBilling != null) { ogloBilling.Dispose(); }
        //        if (oSplitClaimDetails != null) { oSplitClaimDetails.Dispose(); }
        //        //_IsTakeback = false;
        //        //_TakeBack = 0;
        //        IsReserveUsed = false;
        //        _AmountAddedToReserve = 0;
        //        if (dsInsurancePayment_TVP != null)
        //        { dsInsurancePayment_TVP.Clear(); }
        //        PaymentAction = FormMode.NewPaymentMode;
        //        SetPaymentPrefixNumber();
        //        if (_IsFromOnInsuranceSelected == true) { _IsFromOnInsuranceSelected = false; }
        //        if (_dtUniqueIDs != null) { _dtUniqueIDs = null; }
        //    }

        //    return _IsDataSaved;
        //}


        private bool SavePayment(bool _isPaymentMade, bool _isCheckUpdating)
        {
            bool _IsDataSaved = true;
            gloAccountsV2.gloInsurancePaymentV2 ObjaccPayment = null;
            SplitClaimDetails oSplitClaimDetails = new SplitClaimDetails();
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(AppSettings.ConnectionStringPM, "");
            DataTable _dtUniqueIDs = new DataTable();
            DataTable _dtUniqueIDsCorrection = new DataTable();
            DataTable _dtUniqueCreditID = new DataTable();
            Int32 row_num = 0;
            Int64 _nEOBID = 0;
            Int64 _nCreditID = 0;
            Boolean _bIsUsedReserveVal = false;
            try
            {
                dsInsurancePayment_TVP = new gloBilling.gloAccountPayment.dsPaymentTVP_V2();
                if ((_isPaymentMade && txtCheckAmount.Text.Trim() != "") || (_isCheckUpdating))
                {
                    #region "Master Data"
                    _dtUniqueCreditID = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);
                    if (EOBPaymentID != 0)
                    {
                        SetCreditsDetails(dsInsurancePayment_TVP, EOBPaymentID);
                    }
                    else
                    {
                        SetCreditsDetails(dsInsurancePayment_TVP, _nCreditID);

                        //Adding Unique KEY for Credit ID

                        if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                        { _nCreditID = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID"].ToString()); }
                        dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"] = _nCreditID;
                    }

                    #region "Payment Master Note"

                    //Notes if any to main payment to all claim OR main payment to reserve account
                    if (txtPayMstNotes.Text.Trim().Length > 0)
                    {
                        #region "General Note"

                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Add();
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = 0;
                        if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                        {
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                        }
                        if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                        {
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID2"].ToString());
                        }

                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "";
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = txtPayMstNotes.Text.Trim();
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = AmountAddedToReserve;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = NoteTypeV2.Payment_Insurance.GetHashCode();
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = NoteSubTypeV2.MasterPayment.GetHashCode();
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = chkPayMstIncludeNotes.Checked;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = BillingNoteTypeV2.None.GetHashCode();
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DBNull.Value;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = "";
                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                        dsInsurancePayment_TVP.Tables["EOBNotes"].AcceptChanges();

                        #endregion
                    }

                    #endregion

                    #endregion "Master Data"

                    #region "Claim Payment"
                    bool _announceBillToSelf = true;
                    _announceBillToSelf = ogloBilling.GetbIsSkipZeroBillingClaimSettingIPP();
                    DataTable _dt = null;

                    if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                    {
                        _dtUniqueIDs = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(c1SinglePayment.Rows.Count - 1);

                        #region "EOB Service Lines"
                        for (int i = 1; i <= c1SinglePayment.Rows.Count - 1; i++)
                        {
                            if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                            {
                                if ((c1SinglePayment.GetData(i, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PAYMENT)).Trim() != ""))
                                {
                                    #region "EOB Save"
                                    if (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_BALANCE)) <= 0 && Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PatientPaidAmount)) <= 0 && _announceBillToSelf)
                                    {
                                        _announceBillToSelf = true;
                                    }
                                    else if (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_BALANCE)) < 0 && (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_BALANCE)) + Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PatientPaidAmount)) <= 0) && _announceBillToSelf)
                                    {
                                        _announceBillToSelf = true;
                                    }
                                    else
                                    {
                                        _announceBillToSelf = false;
                                    }
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows.Add();
                                    if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                                    {
                                        dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCreditID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                        dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                                    }
                                    if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                    {
                                        dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["ID"].ToString());
                                    }
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPatientID"] = PatientControl.PatientID;
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPatientInsuranceID"] = PatientInsuranceID;
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nContactID"] = ContactInsuranceID;
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nInsCompanyID"] = SelectedInsuranceCompanyID;
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));

                                    if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PAYMENT)).Trim() != "")
                                    {
                                        if (c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_PAYMENT)).Trim() != "")
                                        {
                                            DataTable _nCrDataTable = null;
                                            decimal _fillPayAmt = 0;
                                            decimal _crPayAmt = 0;
                                            if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null)
                                            {
                                                _fillPayAmt = (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT)));
                                                _crPayAmt = (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT)));
                                                _crPayAmt = _crPayAmt - (_crPayAmt * 2);
                                                _nCrDataTable = gloInsurancePaymentV2.GetCorrectionRefList(_crPayAmt, PatientControl.PatientID, Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID)), Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID)), this.PatientInsuranceID, this.ContactInsuranceID);
                                            }
                                            if (_nCrDataTable != null && _nCrDataTable.Rows.Count > 0)
                                            {
                                                if (Convert.ToInt64(_nCrDataTable.Rows[0]["nCreditID"].ToString()) == EOBPaymentID && _fillPayAmt < 0)
                                                {
                                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.InsuranceCorrection.GetHashCode();
                                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sEntryDesc"] = "InsCor";

                                                }
                                                else if (Convert.ToInt64(_nCrDataTable.Rows[0]["nCreditID"].ToString()) != EOBPaymentID && _fillPayAmt < 0)
                                                {
                                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.TakeBack.GetHashCode();
                                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sEntryDesc"] = "InsTB";

                                                }
                                                else
                                                {
                                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.InsuracePayment.GetHashCode();
                                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sEntryDesc"] = "InsPmt";

                                                }
                                            }
                                            else
                                            {
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.InsuracePayment.GetHashCode();
                                                dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sEntryDesc"] = "InsPmt";
                                            }
                                        }
                                    }


                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));

                                    if (mskCloseDate.MaskCompleted == true)
                                    {
                                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                        dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                                    }

                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nPaymentTrayID"] = SelectedPaymentTrayID;
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sPaymentTrayDesc"] = SelectedPaymentTray;
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

                                    if (c1SinglePayment.GetData(i, COL_ALLOWED) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_ALLOWED)).Trim() != "")
                                    {
                                        dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dAllowedAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_ALLOWED));
                                    }

                                    if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF)).Trim() != "")
                                    { dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dWriteoffAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_WRITEOFF)); }

                                    if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PAYMENT)).Trim() != "")
                                    { dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dPaymentAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)); }

                                    if (c1SinglePayment.GetData(i, COL_COPAY) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_COPAY)).Trim() != "")
                                    { dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dCopayAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_COPAY)); }

                                    if (c1SinglePayment.GetData(i, COL_DEDUCTIBLE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_DEDUCTIBLE)).Trim() != "")
                                    { dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dDeductibleAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_DEDUCTIBLE)); }

                                    if (c1SinglePayment.GetData(i, COL_COINSURANCE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_COINSURANCE)).Trim() != "")
                                    { dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dCoinsurance"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_COINSURANCE)); }

                                    if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD)).Trim() != "")
                                    { dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dWithholdAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_WITHHOLD)); }


                                    #region "Retrieve Next Action For EOB EXT "

                                    string _party = "";
                                    string _partyCode = "";
                                    string _partyDesc = "";
                                    Int64 _partyInsId = 0;
                                    Int64 _partyContactId = 0;
                                    string _nextAction = "";
                                    string _nextActionCode = "";

                                    if (c1SinglePayment.GetData(i, COL_NEXT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT)).Trim() != "")
                                    {
                                        _nextAction = Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT)).Trim();
                                        _nextActionCode = _nextAction.Substring(0, _nextAction.IndexOf('-'));
                                    }


                                    if (c1SinglePayment.GetData(i, COL_PARTY) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PARTY)).Trim() != "")
                                    {

                                        _party = Convert.ToString(c1SinglePayment.GetData(i, COL_PARTY)).Trim();
                                        _partyCode = _party.Substring(0, _party.IndexOf('-'));
                                        _partyDesc = _party.Substring(_party.IndexOf('-') + 1, (_party.Length - _party.IndexOf('-')) - 1);


                                        _dt = null;
                                        _dt = gloStripControl.PatientStripControl.GetInsuranceParties(Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID)), Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID)));

                                        if (_dt != null && _dt.Rows.Count > 0)
                                        {
                                            for (int pIndex = 0; pIndex < _dt.Rows.Count; pIndex++)
                                            {
                                                if (Convert.ToString(_dt.Rows[pIndex]["nResponsibilityNo"]) == _partyCode)
                                                {
                                                    _partyInsId = Convert.ToInt64(_dt.Rows[pIndex]["nInsuranceID"]);
                                                    _partyContactId = Convert.ToInt64(_dt.Rows[pIndex]["nContactID"]);
                                                    break;
                                                }
                                            }
                                        }
                                        if (_dt != null) { _dt.Dispose(); }
                                    }

                                    #endregion


                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEXTID"] = 0;
                                    if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                                    {
                                        dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCreditIDEXT"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                        dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBIDEXT"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                                    }
                                    if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                    {
                                        dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBDetailIDEXT"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["ID"].ToString());
                                    }
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sNextActionEXT"] = _nextActionCode;
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sNextPartyEXT"] = _partyDesc.Trim();
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nNextPartyIDEXT"] = _partyInsId;
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtCreatedDateTimeEXT"] = DBNull.Value;
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtModifiedDateTimeEXT"] = DBNull.Value;
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["dtPaymentVoidDateTimeEXT"] = DBNull.Value;
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nUserIDEXT"] = gloGlobal.gloPMGlobal.UserID;
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sUserNameEXT"] = AppSettings.UserName;
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["bIsERAPostEXT"] = false;
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nSVCIDEXT"] = DBNull.Value;
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sMachineNameEXT"] = Environment.MachineName;
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sSiteIDEXT"] = DBNull.Value;
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sVersionEXT"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nClinicIDEXT"] = gloGlobal.gloPMGlobal.ClinicID;
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nCLPIdEXT"] = 0;
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionEXT"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                    dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nBillingTransactionDetailEXT"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                    dsInsurancePayment_TVP.Tables["EOB"].AcceptChanges();
                                    dsInsurancePayment_TVP.Tables["Debits"].AcceptChanges();

                                    #region " Set Line Reason Codes "

                                    //...Code added to auto set the adjustment codes for W/O,Copay,Dedutible,Coinsurance,Withhold
                                    //...by reading there respective values from admin settings
                                    //string _code = "";

                                    //for (int colIndex = COL_WRITEOFF; colIndex <= COL_WITHHOLD; colIndex++)
                                    //{
                                    //    _code = "";
                                    //    if (c1SinglePayment.GetData(i, colIndex) != null && Convert.ToString(c1SinglePayment.GetData(i, colIndex)).Trim() != ""
                                    //        && Convert.ToDecimal(c1SinglePayment.GetData(i, colIndex)) != 0)
                                    //    {
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Add();
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nID"] = 0;
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nClaimNo"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                                    //        if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                    //        {
                                    //            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID2"].ToString());
                                    //        }

                                    //        if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                                    //        {
                                    //            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                    //            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                                    //        }
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sReasonCode"] = GetSelectedReasonCode(colIndex);
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sReasonDescription"] = gloAccountsV2.gloInsurancePaymentV2.GetReasonDescription(_code);
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["dReasonAmount"] = Convert.ToDecimal(c1SinglePayment.GetData(i, colIndex));
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nType"] = EOBCommentTypeV2.SystemReasonCode.GetHashCode();
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["bIsVoid"] = 0;
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnDtlID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnLineNo"] = DBNull.Value;
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sSubClaimNo"] = Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nVoidType"] = 0;
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["bIsPaymentVoid"] = 0;
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nPaymentVoidCloseDate"] = DBNull.Value;
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                    //        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nSubType"] = DBNull.Value;

                                    //    }
                                    //}

                                    if (c1SinglePayment.GetData(i, COL_REASON) != null)
                                    {
                                        C1FlexGrid oFlex = ((C1FlexGrid)(c1SinglePayment.GetData(i, COL_REASON)));
                                        if (oFlex != null && oFlex.Rows.Count > 0)
                                        {

                                            for (int rIndex = 1; rIndex < oFlex.Rows.Count; rIndex++)
                                            {
                                                if (Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Type"].Index)) == "Reason")
                                                {
                                                    if (oFlex.GetData(rIndex, oFlex.Cols["Amount"].Index) != null)
                                                    {
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Add();

                                                        if (oFlex.GetData(rIndex, oFlex.Cols["Id"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Id"].Index)).Trim() != "")
                                                        { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["Id"].Index)); }

                                                        if (oFlex.GetData(rIndex, oFlex.Cols["ClaimNo"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["ClaimNo"].Index)).Trim() != "")
                                                        { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nClaimNo"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["ClaimNo"].Index)); }

                                                        if (oFlex.GetData(rIndex, oFlex.Cols["SubClaimNo"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["SubClaimNo"].Index)).Trim() != "")
                                                        { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sSubClaimNo"] = Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["SubClaimNo"].Index)); }

                                                        if (oFlex.GetData(rIndex, oFlex.Cols["EOBPaymentID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["EOBPaymentID"].Index)).Trim() != "")
                                                        { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["EOBPaymentID"].Index)); }

                                                        if (oFlex.GetData(rIndex, oFlex.Cols["EOBID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["EOBID"].Index)).Trim() != "")
                                                        { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["EOBID"].Index)); }

                                                        if (oFlex.GetData(rIndex, oFlex.Cols["EOBPayDtlID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["EOBPayDtlID"].Index)).Trim() != "")
                                                        { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["EOBPayDtlID"].Index)); }

                                                        if (oFlex.GetData(rIndex, oFlex.Cols["BLTransactionID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["BLTransactionID"].Index)).Trim() != "")
                                                        { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["BLTransactionID"].Index)); }




                                                        if (oFlex.GetData(rIndex, oFlex.Cols["BLTransactionDtlID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["BLTransactionDtlID"].Index)).Trim() != "")
                                                        { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["BLTransactionDtlID"].Index)); }

                                                        if (oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionID"].Index)).Trim() != "")
                                                        { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionID"].Index)); }

                                                        if (oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionDtlID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionDtlID"].Index)).Trim() != "")
                                                        { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnDtlID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionDtlID"].Index)); }

                                                        if (oFlex.GetData(rIndex, oFlex.Cols["Code"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Code"].Index)).Trim() != "")
                                                        { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sReasonCode"] = Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Code"].Index)); }

                                                        if (oFlex.GetData(rIndex, oFlex.Cols["Description"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Description"].Index)).Trim() != "")
                                                        { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sReasonDescription"] = Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Description"].Index)); }
                                                        else
                                                        {
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["sReasonDescription"] = "";
                                                        }

                                                        if (oFlex.GetData(rIndex, oFlex.Cols["Amount"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Amount"].Index)).Trim() != "")
                                                        { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["dReasonAmount"] = Convert.ToDecimal(oFlex.GetData(rIndex, oFlex.Cols["Amount"].Index)); }

                                                        if (oFlex.GetData(rIndex, oFlex.Cols["ClinicId"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["ClinicId"].Index)).Trim() != "")
                                                        { dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nClinicID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["ClinicId"].Index)); }

                                                        if (Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["ReasonCodeType"].Index)) == "Other" ||
                                                            Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["ReasonCodeType"].Index)) == "")
                                                        {
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nType"] = EOBCommentTypeV2.Reason.GetHashCode();
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nReasonCodeType"] = gloBilling.gloERA.AdjustmentType.OtherAdjustment;
                                                        }
                                                        else
                                                        {
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nType"] = EOBCommentTypeV2.SystemReasonCode.GetHashCode();
                                                            if (Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["ReasonCodeType"].Index)) == "W/O")
                                                            {
                                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nReasonCodeType"] = gloBilling.gloERA.AdjustmentType.WriteOff;
                                                            }
                                                            if (Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["ReasonCodeType"].Index)) == "Copay")
                                                            {
                                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nReasonCodeType"] = gloBilling.gloERA.AdjustmentType.Copay;
                                                            }
                                                            if (Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["ReasonCodeType"].Index)) == "Deduct")
                                                            {
                                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nReasonCodeType"] = gloBilling.gloERA.AdjustmentType.Deductable;
                                                            }
                                                            if (Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["ReasonCodeType"].Index)) == "Co-ins")
                                                            {
                                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nReasonCodeType"] = gloBilling.gloERA.AdjustmentType.CoInsurance;
                                                            }
                                                            if (Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["ReasonCodeType"].Index)) == "Withhold")
                                                            {
                                                                dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nReasonCodeType"] = gloBilling.gloERA.AdjustmentType.WithHold;
                                                            }
                                                        }

                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nID"] = 0;
                                                        if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                                                        {
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                                                        }
                                                        if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                        {
                                                            dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID2"].ToString());
                                                        }
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["bIsVoid"] = 0;
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nTrackTrnLineNo"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_LINENO)); ;
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nVoidType"] = 0;
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["bIsPaymentVoid"] = 0;
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nPaymentVoidCloseDate"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;

                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                                                        dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["nSubType"] = DBNull.Value;








                                                    }










                                                }
                                            }
                                        }
                                    }

                                    #endregion " Set Line Reason Codes "

                                    #region " Set Line Remark Codes "

                                    if (c1SinglePayment.GetData(i, COL_REASON) != null)
                                    {
                                        C1FlexGrid oFlex = ((C1FlexGrid)(c1SinglePayment.GetData(i, COL_REASON)));
                                        if (oFlex != null && oFlex.Rows.Count > 0)
                                        {

                                            for (int rIndex = 1; rIndex < oFlex.Rows.Count; rIndex++)
                                            {
                                                if (Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Type"].Index)) == "Remark")
                                                {


                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Add();


                                                    if (oFlex.GetData(rIndex, oFlex.Cols["Id"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Id"].Index)).Trim() != "")
                                                    { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nEOBRemarkID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["Id"].Index)); }

                                                    if (oFlex.GetData(rIndex, oFlex.Cols["ClaimNo"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["ClaimNo"].Index)).Trim() != "")
                                                    { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nClaimNo"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["ClaimNo"].Index)); }

                                                    if (oFlex.GetData(rIndex, oFlex.Cols["SubClaimNo"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["SubClaimNo"].Index)).Trim() != "")
                                                    { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["sSubClaimNo"] = Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["SubClaimNo"].Index)); }

                                                    if (oFlex.GetData(rIndex, oFlex.Cols["EOBPaymentID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["EOBPaymentID"].Index)).Trim() != "")
                                                    { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["EOBPaymentID"].Index)); }

                                                    if (oFlex.GetData(rIndex, oFlex.Cols["EOBID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["EOBID"].Index)).Trim() != "")
                                                    { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["EOBID"].Index)); }

                                                    if (oFlex.GetData(rIndex, oFlex.Cols["EOBPayDtlID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["EOBPayDtlID"].Index)).Trim() != "")
                                                    { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["EOBPayDtlID"].Index)); }

                                                    if (oFlex.GetData(rIndex, oFlex.Cols["BLTransactionID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["BLTransactionID"].Index)).Trim() != "")
                                                    { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["BLTransactionID"].Index)); }

                                                    if (oFlex.GetData(rIndex, oFlex.Cols["BLTransactionDtlID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["BLTransactionDtlID"].Index)).Trim() != "")
                                                    { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["BLTransactionDtlID"].Index)); }

                                                    if (oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionID"].Index)).Trim() != "")
                                                    { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nTrackTrnID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionID"].Index)); }

                                                    if (oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionDtlID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionDtlID"].Index)).Trim() != "")
                                                    { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nTrackTrnDtlID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionDtlID"].Index)); }

                                                    if (oFlex.GetData(rIndex, oFlex.Cols["Code"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Code"].Index)).Trim() != "")
                                                    { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["sRemarkCode"] = Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Code"].Index)); }

                                                    if (oFlex.GetData(rIndex, oFlex.Cols["Description"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Description"].Index)).Trim() != "")
                                                    { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["sRemarkDescription"] = Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Description"].Index)); }

                                                    //if (oFlex.GetData(rIndex, oFlex.Cols["Amount"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Amount"].Index)).Trim() != "")
                                                    //{ dsInsurancePayment_TVP.Tables["ReasonCode"].Rows[dsInsurancePayment_TVP.Tables["ReasonCode"].Rows.Count - 1]["dReasonAmount"] = Convert.ToDecimal(oFlex.GetData(rIndex, oFlex.Cols["Amount"].Index)); }

                                                    if (oFlex.GetData(rIndex, oFlex.Cols["ClinicId"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["ClinicId"].Index)).Trim() != "")
                                                    { dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nClinicID"] = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["ClinicId"].Index)); }

                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nType"] = EOBCommentTypeV2.Reason.GetHashCode();
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nEOBRemarkID"] = 0;
                                                    if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                                                    {
                                                        dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                                        dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                                                    }
                                                    if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                    {
                                                        dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID2"].ToString());
                                                    }
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["bIsVoid"] = 0;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nTrackTrnLineNo"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_LINENO)); ;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nVoidType"] = 0;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["bIsPaymentVoid"] = 0;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nPaymentVoidCloseDate"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;

                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["nSubType"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["RemarkCode"].Rows[dsInsurancePayment_TVP.Tables["RemarkCode"].Rows.Count - 1]["sReasonCode"] = Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["RemarkReasonCodes"].Index));
                                                }
                                            }
                                        }
                                    }

                                    #endregion " Set Line Remark Codes "

                                    #region " Statement Notes & Internal Notes for Line "

                                    if (c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTE)).Trim() != "")
                                    {

                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Add();
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                                        if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                                        {
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                                        }

                                        if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                        {
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID2"].ToString());
                                        }
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "";
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTE)).Trim();
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = 0;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = NoteTypeV2.Payment_Insurance.GetHashCode();
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = NoteSubTypeV2.StatementNote.GetHashCode();
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = Convert.ToBoolean(c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTEONPRINT));
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = 0;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = BillingNoteTypeV2.PaymentStatementNote.GetHashCode();
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DBNull.Value;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;

                                    }

                                    if (c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTE)).Trim() != "")
                                    {
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Add();
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                                        if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                                        {
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                                        }
                                        if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                        {
                                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID2"].ToString());
                                        }

                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "";
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTE)).Trim();
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = 0;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = NoteTypeV2.Payment_Insurance.GetHashCode();
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = NoteSubTypeV2.InternalNote.GetHashCode();
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = Convert.ToBoolean(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTEONPRINT));
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = 0;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = BillingNoteTypeV2.PaymentInternalNote.GetHashCode();
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DBNull.Value;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
                                        dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;

                                    }

                                    dsInsurancePayment_TVP.Tables["ReasonCode"].AcceptChanges();
                                    dsInsurancePayment_TVP.Tables["EOBNotes"].AcceptChanges();
                                    dsInsurancePayment_TVP.Tables["RemarkCode"].AcceptChanges();
                                    #endregion " Statement Notes & Internal Notes for Line "

                                    #endregion "EOB Save"

                                    #region "Debit Service Line - Insurance - Allocation from Current Check, Correction/Takeback, Use Reserved"

                                    if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PAYMENT)).Trim() != "")
                                    {
                                        if (c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_PAYMENT)).Trim() != "")
                                        {
                                            decimal _fillPayAmt = 0;
                                            decimal _crPayAmt = 0;

                                            DataTable _nCrDataTable = null;

                                            if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null)
                                            {
                                                _fillPayAmt = (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT)));
                                                _crPayAmt = (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT)));
                                                _crPayAmt = _crPayAmt - (_crPayAmt * 2);
                                                _nCrDataTable = gloInsurancePaymentV2.GetCorrectionRefList(_crPayAmt, PatientControl.PatientID, Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID)), Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID)), this.PatientInsuranceID, this.ContactInsuranceID);
                                            }

                                            else if (c1SinglePayment.GetData(i, COL_PAYMENT) != null)
                                            {
                                                _fillPayAmt = (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)));
                                            }
                                            else if (c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null)
                                            {
                                                _fillPayAmt = 0 - (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT)));

                                            }

                                            #region "Set Debits object"
                                            if (_nCrDataTable.Rows.Count > 0)
                                            {
                                                _dtUniqueIDsCorrection = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(_nCrDataTable.Rows.Count);
                                                for (int nCrDTIndex = 0; nCrDTIndex < _nCrDataTable.Rows.Count; nCrDTIndex++)
                                                {
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows.Add();
                                                    if (Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nCreditID"].ToString()) != EOBPaymentID && _fillPayAmt < 0 && (_fillPayAmt <= Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nLineAmt"])))
                                                    {
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCredit_RefID"] = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nCreditID"].ToString());
                                                    }
                                                    else
                                                    {
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCredit_RefID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                                    }


                                                    if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                                                    {
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCreditID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                                                    }
                                                    if (_dtUniqueIDsCorrection != null && _dtUniqueIDsCorrection.Rows.Count > 0)
                                                    {
                                                        if (Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID)) == Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nBillingTransactionID"].ToString())
                                                            && Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID)) == Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nBillingTransactionDetailID"].ToString()))
                                                        {
                                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBDetailID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBDetailID"].ToString());
                                                        }
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = Convert.ToInt64(_dtUniqueIDsCorrection.Rows[nCrDTIndex]["ID2"].ToString());
                                                    }

                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));

                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTDesc"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dPaymentAmount"] = Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]) - (Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]) * 2); ;

                                                    if (nCrDTIndex == 0)
                                                    {
                                                        if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null && c1SinglePayment.GetData(i, COL_WRITEOFF).ToString().Trim() != "")
                                                        {
                                                            if (c1SinglePayment.GetData(i, COL_ISCORRECTION) != null && Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISCORRECTION)) == true)
                                                            {

                                                                if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF)).Trim() != "")
                                                                {
                                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWriteoffAmount"] = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF))) - Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WRITEOFF)));
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF)).Trim() != "")
                                                                {
                                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWriteoffAmount"] = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF)));
                                                                }
                                                            }
                                                        }


                                                        if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null && c1SinglePayment.GetData(i, COL_WITHHOLD).ToString().Trim() != "")
                                                        {
                                                            if (c1SinglePayment.GetData(i, COL_ISCORRECTION) != null && Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISCORRECTION)) == true)
                                                            {
                                                                if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD)).Trim() != "")
                                                                {
                                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWithholdAmount"] = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD))) - Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WITHHOLD)));
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD)).Trim() != "")
                                                                {
                                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWithholdAmount"] = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD)));
                                                                }
                                                            }
                                                        }
                                                    }

                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientInsuranceID"] = PatientInsuranceID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nInsCompanyID"] = SelectedInsuranceCompanyID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nContactID"] = ContactInsuranceID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientID"] = PatientControl.PatientID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentTrayID"] = SelectedPaymentTrayID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentTrayDesc"] = SelectedPaymentTray;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sUserName"] = AppSettings.UserName;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;

                                                    if (c1SinglePayment.GetData(i, COL_ISCORRECTION) != null && Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISCORRECTION)) == true)
                                                    {
                                                        if (_nCrDataTable != null && _nCrDataTable.Rows.Count > 0)
                                                        {
                                                            if (Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nCreditID"].ToString()) == EOBPaymentID && _fillPayAmt < 0)
                                                            {
                                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.InsuranceCorrection.GetHashCode();
                                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sEntryDesc"] = "InsCor";

                                                            }
                                                            else if (Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nCreditID"].ToString()) != EOBPaymentID && _fillPayAmt < 0)
                                                            {
                                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.TakeBack.GetHashCode();
                                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sEntryDesc"] = "InsTB";

                                                            }
                                                            else
                                                            {
                                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.InsuracePayment.GetHashCode();
                                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sEntryDesc"] = "InsPmt";

                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.InsuracePayment.GetHashCode();
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sEntryDesc"] = "InsPmt";
                                                    }

                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nGuarantorID"] = this.nGuarantorID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPAccountID"] = this.nPAccountID;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nAccountPatientID"] = this.nAccountPatientID;
                                                    if (mskCloseDate.MaskCompleted == true)
                                                    {
                                                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                                                    }

                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dOtherAdjustmentAmount"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nVoidType"] = 0;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidDateTime"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidCloseDate"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidDateTime"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsERAPost"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCreatedDateTime"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sSiteID"] = DBNull.Value;
                                                    dsInsurancePayment_TVP.Tables["Debits"].AcceptChanges();

                                                    if (Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nCreditID"].ToString()) != EOBPaymentID && _fillPayAmt < 0)
                                                    {
                                                        dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Add();
                                                        dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nCreditsDTL_ID"] = 0;
                                                        if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                                                        {
                                                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nCreditID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                                        }
                                                        dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nCreditsRef_ID"] = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nCreditID"].ToString());
                                                        dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nReserveRef_ID"] = 0;
                                                        dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dAmount"] = Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"].ToString());
                                                        dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.TakeBack.GetHashCode();
                                                        dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["sEntryDesc"] = "TB";

                                                        if (mskCloseDate.MaskCompleted == true)
                                                        {
                                                            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                                                        }
                                                        dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dtCreatedDateTime"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                                                        dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["bIsPaymentVoid"] = false;

                                                        dsInsurancePayment_TVP.Tables["CreditsDTL"].AcceptChanges();
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                //Bug #64688: 00000631 : Error message says that "there is no row at position 5". 
                                                //Added to generate new unique id and used if uniqueID not available in _dtUniqueIDs id.
                                                _dtUniqueIDsCorrection = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(1);

                                                dsInsurancePayment_TVP.Tables["Debits"].Rows.Add();
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCredit_RefID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());

                                                if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                                                {
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCreditID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                                                    //if (dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCredit_RefID"] != null)
                                                    //{
                                                    //    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nCredit_RefID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                                    //}
                                                }
                                                if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                {
                                                    //Bug #64688: 00000631 : Error message says that "there is no row at position 5".
                                                    //Commented the following line.
                                                    //1. Make entry for nEOBDetailID as get ID form EOB table of dsInsurancePayment_TVP dataset and assing to nEOBDetailID.
                                                    //2. Added new logic if uniqueID not available in _dtUniqueIDs then used newly generated id from _dtUniqueIDsCorrection.

                                                    //dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["ID"].ToString());
                                                    //dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["ID2"].ToString());

                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBDetailID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["EOB"].Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["nEOBDetailID"].ToString());

                                                    if (_dtUniqueIDs != null && ((_dtUniqueIDs.Rows.Count - 1) >= (dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1)))
                                                    {
                                                        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["ID2"].ToString());
                                                    }
                                                    else
                                                    { dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = Convert.ToInt64(_dtUniqueIDsCorrection.Rows[0]["ID2"].ToString()); }
                                                }

                                                //if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                                                //{
                                                //    if (_dtUniqueIDs.Rows.Count == Convert.ToInt64(dsInsurancePayment_TVP.Tables["Debits"].Rows.Count))
                                                //    {
                                                //        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["ID"].ToString());
                                                //        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = Convert.ToInt64(_dtUniqueIDs.Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["ID2"].ToString());
                                                //    }
                                                //    else
                                                //    {
                                                //        if (dsInsurancePayment_TVP.Tables["Debits"] != null && Convert.ToInt64(dsInsurancePayment_TVP.Tables["Debits"].Rows.Count) > 0)
                                                //        {
                                                //            DataTable _dtUniqueDebitIDs = new DataTable();
                                                //            _dtUniqueDebitIDs = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(dsInsurancePayment_TVP.Tables["Debits"].Rows.Count);

                                                //            for (int iVar = 0; iVar <= dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1; iVar++)
                                                //            {
                                                //                dsInsurancePayment_TVP.Tables["Debits"].Rows[iVar]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueDebitIDs.Rows[iVar]["ID"].ToString());
                                                //                dsInsurancePayment_TVP.Tables["Debits"].Rows[iVar]["nDebitID"] = Convert.ToInt64(_dtUniqueDebitIDs.Rows[iVar]["ID2"].ToString());
                                                //            }

                                                //        }
                                                //    }
                                                //}

                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));

                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nTrackTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTCode"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sCPTDesc"] = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dPaymentAmount"] = _fillPayAmt;


                                                if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null && c1SinglePayment.GetData(i, COL_WRITEOFF).ToString().Trim() != "")
                                                {
                                                    if (c1SinglePayment.GetData(i, COL_ISCORRECTION) != null && Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISCORRECTION)) == true)
                                                    {

                                                        if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF)).Trim() != "")
                                                        {
                                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWriteoffAmount"] = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF))) - Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WRITEOFF)));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF)).Trim() != "")
                                                        {
                                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWriteoffAmount"] = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF)));
                                                        }
                                                    }
                                                }


                                                if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null && c1SinglePayment.GetData(i, COL_WITHHOLD).ToString().Trim() != "")
                                                {
                                                    if (c1SinglePayment.GetData(i, COL_ISCORRECTION) != null && Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISCORRECTION)) == true)
                                                    {
                                                        if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD)).Trim() != "")
                                                        {
                                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWithholdAmount"] = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD))) - Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WITHHOLD)));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD)).Trim() != "")
                                                        {
                                                            dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dWithholdAmount"] = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD)));
                                                        }
                                                    }
                                                }

                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientInsuranceID"] = PatientInsuranceID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nInsCompanyID"] = SelectedInsuranceCompanyID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nContactID"] = ContactInsuranceID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPatientID"] = PatientControl.PatientID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentTrayID"] = SelectedPaymentTrayID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentTrayDesc"] = SelectedPaymentTray;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sUserName"] = AppSettings.UserName;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;

                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.InsuracePayment.GetHashCode();
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sEntryDesc"] = "InsPmt";

                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nGuarantorID"] = this.nGuarantorID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPAccountID"] = this.nPAccountID;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nAccountPatientID"] = this.nAccountPatientID;
                                                if (mskCloseDate.MaskCompleted == true)
                                                {
                                                    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                                    dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                                                }

                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dOtherAdjustmentAmount"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nVoidType"] = 0;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtPaymentVoidDateTime"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidCloseDate"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtClaimVoidDateTime"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["bIsERAPost"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtCreatedDateTime"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
                                                dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["sSiteID"] = DBNull.Value;
                                                dsInsurancePayment_TVP.Tables["Debits"].AcceptChanges();

                                            }
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion
                        DataRow[] _dr = dsInsurancePayment_TVP.Tables["EOB"].Select("sNextPartyEXT='Self'");

                        //.Rows[dsInsurancePayment_TVP.Tables["EOB"].Rows.Count - 1]["sNextPartyEXT"]
                        if (_announceBillToSelf && _dr.Length <= 0 && _dt != null)
                        {
                            string _transfertoSelfAnnouncment = "Warning - $0.00 Secondary Claim" +
                            "" + Environment.NewLine + "" +
                            "" + Environment.NewLine + "" +
                            "Charges have been fully paid by primary." +
                            "" + Environment.NewLine + "" +
                            "" + Environment.NewLine + "" +
                            "Skip Secondary and bill the patient?" +
                            "" + Environment.NewLine + "" +
                            "" + Environment.NewLine + "" +
                            "Yes - Transfer all the charge(s) on claim to patient" +
                            "" + Environment.NewLine + "" +
                            "" + Environment.NewLine + "" +
                            "No  - Continue with current selection" +
                            "" + Environment.NewLine + "" +
                            "" + Environment.NewLine + "" +
                            "Cancel - Stop and review the payment";


                            switch (MessageBox.Show(_transfertoSelfAnnouncment, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                            {

                                case DialogResult.Cancel:
                                    return false;
                                // break;
                                case DialogResult.No:
                                    break;
                                case DialogResult.None:
                                    break;
                                case DialogResult.Yes:
                                    for (int i = 1; i <= c1SinglePayment.Rows.Count - 1; i++)
                                    {
                                        #region "Set Next Action Bill And ResponsibleParty to Self"

                                        c1SinglePayment.BeginUpdate();
                                        c1SinglePayment.SetData(i, COL_NEXT, "B-Bill");
                                        c1SinglePayment.SetData(i, COL_PARTY, Convert.ToString(_dt.Rows.Count) + "-Self");
                                        c1SinglePayment.EndUpdate();

                                        #endregion
                                    }

                                    for (int _nrow = 0; _nrow < dsInsurancePayment_TVP.Tables["EOB"].Rows.Count; _nrow++)
                                    {
                                        dsInsurancePayment_TVP.Tables["EOB"].Rows[_nrow]["sNextActionEXT"] = "B";
                                        dsInsurancePayment_TVP.Tables["EOB"].Rows[_nrow]["sNextPartyEXT"] = "Self";
                                        dsInsurancePayment_TVP.Tables["EOB"].Rows[_nrow]["nNextPartyIDEXT"] = 0;
                                        dsInsurancePayment_TVP.Tables["EOB"].AcceptChanges();
                                    }
                                    break;

                            }

                        }
                    }
                    #endregion "Claim Payment"

                    #region "Reserve Debit Entry "
                    if (IsReserveAdded)
                    {
                        decimal _reserveAmt = 0;
                        _reserveAmt = AmountAddedToReserve;

                        //0 ReserveAmount 
                        //1 ReserveNote 
                        //3 ReserveNoteOnPrint 

                        if (AmountAddedToReserve > 0)
                        {
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows.Add();
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nReserveID"] = 0;
                            if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                            {
                                dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nCreditID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nCredits_RefID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                            }
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dReserveAmount"] = _reserveAmt;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nReserveType"] = ReserveEntryTypeV2.InsuraceReserve.GetHashCode();
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nInsCompanyID"] = SelectedInsuranceCompanyID;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nPatientID"] = PatientControl.PatientID;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["sUserName"] = AppSettings.UserName;
                            if (mskCloseDate.MaskCompleted == true)
                            {
                                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                            }
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nVoidType"] = 0;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nPAccountID"] = 0;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nGuarantorID"] = 0;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["nAccountPatientID"] = 0;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtPaymentVoidDateTime"] = DBNull.Value;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtCreatedDateTime"] = DBNull.Value;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["sReserveNote"] = "Reserved";
                            dsInsurancePayment_TVP.Tables["Reserves"].Rows[dsInsurancePayment_TVP.Tables["Reserves"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
                            dsInsurancePayment_TVP.Tables["Reserves"].AcceptChanges();

                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Add();
                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nEOBPaymentDetailID"] = 0;
                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nTransactionID"] = ReserveDetails.MSTTransactionID;
                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nTrackTrnID"] = ReserveDetails.TransactionID;
                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nPatientID"] = ReserveDetails.PatientID;
                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nProviderID"] = ReserveDetails.ProviderID;
                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows[dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].Rows.Count - 1]["nBusinessCenterID"] = ReserveDetails.BusinessCenterID;

                            dsInsurancePayment_TVP.Tables["BL_Reserve_Association"].AcceptChanges();

                            #region "General Note"

                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Add();
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nID"] = 0;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClaimNo"] = 0;
                            if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                            {
                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBID"] = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                            }
                            if (_dtUniqueIDs != null && _dtUniqueIDs.Rows.Count > 0)
                            {
                                dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nEOBPaymentDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[row_num]["ID2"].ToString());
                            }

                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionID"] = 0;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingTransactionDetailID"] = 0;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteCode"] = "";
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sNoteDescription"] = ReserveNote.Trim();
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dNoteAmount"] = AmountAddedToReserve;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteType"] = NoteTypeV2.Payment_InsuranceReserved.GetHashCode();
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nPaymentNoteSubType"] = NoteSubTypeV2.MasterPayment.GetHashCode();
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nIncludeNoteOnPrint"] = false;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["bIsVoid"] = 0;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nBillingNoteType"] = BillingNoteTypeV2.None.GetHashCode();
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nDateTime"] = DBNull.Value;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnID"] = 0;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnDtlID"] = 0;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["nTrackTrnLineNo"] = 0;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["sSubClaimNo"] = "";
                            dsInsurancePayment_TVP.Tables["EOBNotes"].Rows[dsInsurancePayment_TVP.Tables["EOBNotes"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                            dsInsurancePayment_TVP.Tables["EOBNotes"].AcceptChanges();

                            #endregion

                        }
                    }



                    //Insert credit detail entry for Used reserved ..........

                    if (_useReserves)
                    {

                        if (txtCheckAmount.Enabled == false)
                        {
                            _TotalAllocation = 0;
                            if (PaymentAction == FormMode.NewPaymentMode)
                            {
                                if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                                {
                                    for (int rIndex = 1; rIndex < c1SinglePayment.Rows.Count; rIndex++)
                                    {
                                        if (c1SinglePayment.GetData(rIndex, COL_PAYMENT) != null
                                           && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_PAYMENT)).Trim() != "")
                                        {
                                            _TotalAllocation = _TotalAllocation + Convert.ToDecimal((c1SinglePayment.GetData(rIndex, COL_PAYMENT)));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (c1SinglePaymentCorrTB != null && c1SinglePaymentCorrTB.Rows.Count > 1)
                                {
                                    for (int rIndex = 1; rIndex < c1SinglePaymentCorrTB.Rows.Count; rIndex++)
                                    {
                                        if (c1SinglePaymentCorrTB.GetData(rIndex, COL_PAYMENT) != null
                                           && Convert.ToString(c1SinglePaymentCorrTB.GetData(rIndex, COL_PAYMENT)).Trim() != ""
                                           && Convert.ToDecimal(c1SinglePaymentCorrTB.GetData(rIndex, COL_PAYMENT)) > 0)
                                        {
                                            _TotalAllocation = _TotalAllocation + Convert.ToDecimal((c1SinglePaymentCorrTB.GetData(rIndex, COL_PAYMENT)));
                                        }
                                    }
                                }
                            }

                            oCreditResDTL_CopyForRule.EOBCreditDTL = oCreditResDTL.EOBCreditDTL.CopyCreditDTL();

                        }

                        for (int index = 0; index < oCreditResDTL.EOBCreditDTL.Count; index++)
                        {
                            if ((oCreditResDTL.EOBCreditDTL[index].Amount - oCreditResDTL.EOBCreditDTL[index].DBReserveAmount) > 0)
                            {
                                dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Add();
                                if (oCreditResDTL.EOBCreditDTL[index].Amount > 0)
                                {
                                    dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nCreditsDTL_ID"] = oCreditResDTL.EOBCreditDTL[index].CreditsDTL_ID;
                                    if (_dtUniqueCreditID != null && _dtUniqueCreditID.Rows.Count > 0)
                                    {
                                        dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nCreditID"] = Convert.ToInt64(dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"].ToString());
                                    }
                                    dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nCreditsRef_ID"] = oCreditResDTL.EOBCreditDTL[index].CreditsRef_ID;
                                    dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nReserveRef_ID"] = oCreditResDTL.EOBCreditDTL[index].ReserveID;

                                    if (txtCheckAmount.Enabled == false)
                                    {
                                        if (_TotalAllocation <= oCreditResDTL.EOBCreditDTL[index].Amount - oCreditResDTL.EOBCreditDTL[index].DBReserveAmount)
                                        {

                                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dAmount"] = _TotalAllocation + oCreditResDTL.EOBCreditDTL[index].DBReserveAmount;
                                            _TotalAllocation = _TotalAllocation - Convert.ToDecimal(dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dAmount"]);
                                            //oCreditResDTL.EOBCreditDTL[index].Amount = Convert.ToDecimal(oCreditResDTL.EOBCreditDTL[index].Amount) - Convert.ToDecimal(dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dAmount"]);


                                        }
                                        else
                                        {
                                            dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dAmount"] = oCreditResDTL.EOBCreditDTL[index].Amount;
                                            _TotalAllocation = _TotalAllocation - (oCreditResDTL.EOBCreditDTL[index].Amount - oCreditResDTL.EOBCreditDTL[index].DBReserveAmount);
                                            //oCreditResDTL.EOBCreditDTL[index].Amount = Convert.ToDecimal(oCreditResDTL.EOBCreditDTL[index].Amount) - Convert.ToDecimal(dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dAmount"]);
                                        }

                                    }
                                    else
                                    {
                                        dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dAmount"] = oCreditResDTL.EOBCreditDTL[index].Amount;
                                    }

                                    dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.UseReserve.GetHashCode();
                                    dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["sEntryDesc"] = "UR";

                                    //dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dtCloseDate"] = oCreditResDTL.EOBCreditDTL[index].CloseDate;
                                    if (mskCloseDate.MaskCompleted == true)
                                    {
                                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                        if (oCreditResDTL.EOBCreditDTL[index].CloseDate > Convert.ToDateTime(mskCloseDate.Text))
                                        {
                                            MessageBox.Show("The used reserved amount close date is in future than the current payment close date. Please select a different payment close date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            _bIsUsedReserveVal = true;
                                            return false;
                                        }
                                        dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
                                    }

                                    dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dtCreatedDateTime"] = DBNull.Value;
                                    dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["dtModifiedDateTime"] = DBNull.Value;
                                    dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows[dsInsurancePayment_TVP.Tables["CreditsDTL"].Rows.Count - 1]["bIsPaymentVoid"] = false;
                                    dsInsurancePayment_TVP.Tables["CreditsDTL"].AcceptChanges();
                                }

                                if (_TotalAllocation <= 0 && txtCheckAmount.Enabled == false)
                                {
                                    break;
                                }
                            }
                        }


                    }
                    #endregion
                }

                //if (dsInsurancePayment_TVP.Tables["Debits"] != null && Convert.ToInt64(dsInsurancePayment_TVP.Tables["Debits"].Rows.Count) > 0)
                //{

                //    DataView dv = dsInsurancePayment_TVP.Tables["Debits"].DefaultView;
                //    dv.RowFilter = "nEntryType = 4";
                //    DataTable dt = dv.ToTable();
                //    _dtUniqueIDs = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(dt.Rows.Count);
                //    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                //    {
                //        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                //        dsInsurancePayment_TVP.Tables["Debits"].Rows[dsInsurancePayment_TVP.Tables["Debits"].Rows.Count - 1]["nDebitID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID2"].ToString());
                //    }

                //    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                //    {
                //        dsInsurancePayment_TVP.Tables["EOB"].Rows[i]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                //        dsInsurancePayment_TVP.Tables["EOB"].Rows[i]["nEOBDetailIDEXT"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                //    }

                //    //DataView dv = dsInsurancePayment_TVP.Tables["Debits"].DefaultView;
                //    //dv.RowFilter = "nEntryType = 4";
                //    //DataTable dt = dv.ToTable();
                //    //_dtUniqueIDs = gloAccountsV2.gloInsurancePaymentV2.GetUniqueIDs(dt.Rows.Count);
                //    //for (int i = 0; i <= dt.Rows.Count - 1; i++)
                //    //{
                //    //    dsInsurancePayment_TVP.Tables["Debits"].Rows[i]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                //    //    dsInsurancePayment_TVP.Tables["Debits"].Rows[i]["nDebitID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID2"].ToString());
                //    //}

                //    //for (int i = 0; i <= dt.Rows.Count - 1; i++)
                //    //{
                //    //    dsInsurancePayment_TVP.Tables["EOB"].Rows[i]["nEOBDetailID"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                //    //    dsInsurancePayment_TVP.Tables["EOB"].Rows[i]["nEOBDetailIDEXT"] = Convert.ToInt64(_dtUniqueIDs.Rows[i]["ID"].ToString());
                //    //}
                //}

                #region " Save EOB Payment & Next action "
                gloAccountsV2.PaymentCollection.PaymentInsuranceLineNextActions oNextActions = new gloAccountsV2.PaymentCollection.PaymentInsuranceLineNextActions();
                oSplitClaimDetails = new SplitClaimDetails();
                SetNextActionDetails(out oSplitClaimDetails);
                DataTable _dtHoldInfo = GetSplittedClaimsHoldInfo(oSplitClaimDetails);
                if (_dtHoldInfo != null && _dtHoldInfo.Rows.Count > 0)
                {
                    for (int holdIndex = 0; holdIndex < _dtHoldInfo.Rows.Count; holdIndex++)
                    {
                        dsInsurancePayment_TVP.Tables["HoldSelection"].Rows.Add();
                        dsInsurancePayment_TVP.Tables["HoldSelection"].Rows[dsInsurancePayment_TVP.Tables["HoldSelection"].Rows.Count - 1]["IsHold"] = _dtHoldInfo.Rows[holdIndex]["IsHold"];
                        dsInsurancePayment_TVP.Tables["HoldSelection"].Rows[dsInsurancePayment_TVP.Tables["HoldSelection"].Rows.Count - 1]["SubClaimNo"] = _dtHoldInfo.Rows[holdIndex]["SubClaimNo"];
                        dsInsurancePayment_TVP.Tables["HoldSelection"].Rows[dsInsurancePayment_TVP.Tables["HoldSelection"].Rows.Count - 1]["PartyNo"] = _dtHoldInfo.Rows[holdIndex]["PartyNo"];
                        dsInsurancePayment_TVP.Tables["HoldSelection"].Rows[dsInsurancePayment_TVP.Tables["HoldSelection"].Rows.Count - 1]["NextAction"] = _dtHoldInfo.Rows[holdIndex]["NextAction"];
                        dsInsurancePayment_TVP.Tables["HoldSelection"].Rows[dsInsurancePayment_TVP.Tables["HoldSelection"].Rows.Count - 1]["Party"] = _dtHoldInfo.Rows[holdIndex]["Party"];
                        dsInsurancePayment_TVP.Tables["HoldSelection"].AcceptChanges();
                    }
                }

                if (dsInsurancePayment_TVP != null)
                {
                    ObjaccPayment = new gloAccountsV2.gloInsurancePaymentV2();
                    ObjaccPayment.ClaimRemitRefNo = txtClaimRemittanceRef.Text.Trim();
                    ObjaccPayment.InsuranceParty = PatientControl.SelectedInsuranceParty;

                    if (EOBPaymentID > 0)
                    {
                        ObjaccPayment.SavePayment(dsInsurancePayment_TVP);
                    }
                    else
                    {
                        EOBPaymentID = ObjaccPayment.SavePayment(dsInsurancePayment_TVP);
                    }

                    #region "Follow-Up Operations for Account"

                    gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(AppSettings.ConnectionStringPM);
                    CL_FollowUpCode.SetAutoAccountFollowUp(PatientControl.PAccountID, PatientControl.PatientID, Convert.ToDateTime(mskCloseDate.Text.Trim()));

                    #endregion "Follow-Up Operations for Account"


                    oSettings.AddSetting("PAYMENT_LASTCLOSEDATE", Convert.ToDateTime(mskCloseDate.Text).ToString("MM/dd/yyyy"), gloGlobal.gloPMGlobal.ClinicID, gloGlobal.gloPMGlobal.UserID, gloSettings.SettingFlag.User);
                    oSettings.AddSetting("PAYMENT_LASTCLOSETRAYID", SelectedPaymentTrayID.ToString(), gloGlobal.gloPMGlobal.ClinicID, gloGlobal.gloPMGlobal.UserID, gloSettings.SettingFlag.User);
                    oSettings.Dispose();

                    ogloBilling.SaveUserWiseCloseDay(mskCloseDate.Text.Trim(), CloseDayType.Payment, gloGlobal.gloPMGlobal.ClinicID);
                }

                #endregion

                #region " Reset parameters & controls "

                _IsDataSaved = true;

                IsPendingCheckLoaded = true;

                // Reset reserve added 
                IsReserveAdded = false;

                // Reset controls & load EOB
                SetupControls();
                if (_dtUniqueCreditID.Rows.Count > 0)
                    _nEOBID = Convert.ToInt64(_dtUniqueCreditID.Rows[0]["ID2"].ToString());
                else
                    _nEOBID = 0;


                //..Clearing the reserves details object after save, the object is again set in LoadFormData method
                //..the object was intially cleared in the PerformSavePayment() method which was called after LoadFormData
                //..causing the object to get cleared when done save.
                //....



                if (oCreditResDTL != null)
                {
                    oCreditResDTL.EOBCreditDTL.Clear();
                }


                LoadFormData(_EOBPaymentID, _nEOBID, true);

                //added By Shweta to clear Reserve associartion after save payment

                if (_IsDataSaved)
                {
                    ReserveDetails.PatientID = 0;
                    ReserveDetails.MSTTransactionID = 0;
                    ReserveDetails.TransactionID = 0;
                    ReserveDetails.ClaimNo = "";
                    ReserveDetails.PatientName = "";
                    _isAssociationSave = false;

                }

                //..Release Claim lock after successfully saving payment against claim
                ReleaseLockStatus(ClaimDetails.TransactionMasterID);
                //..Release Claim lock after successfully saving payment against claim

                // Reset the patient strip control
                PatientControl.ClearDetails();
                PatientControl.SelectSearchBox();

                if (!_bIsUsedReserveVal && txtCheckAmount.Enabled == true)
                {
                    _useReserves = false;
                }
                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _IsDataSaved = false;
            }
            finally
            {
                if (ObjaccPayment != null) { ObjaccPayment.Dispose(); }
                if (ogloBilling != null) { ogloBilling.Dispose(); }
                if (oSplitClaimDetails != null) { oSplitClaimDetails.Dispose(); }
                //_IsTakeback = false;
                //_TakeBack = 0;
                IsReserveUsed = false;
                _AmountAddedToReserve = 0;
                if (dsInsurancePayment_TVP != null)
                { dsInsurancePayment_TVP.Clear(); }
                PaymentAction = FormMode.NewPaymentMode;
                SetPaymentPrefixNumber();
                if (_IsFromOnInsuranceSelected == true) { _IsFromOnInsuranceSelected = false; }
                if (_dtUniqueIDs != null) { _dtUniqueIDs = null; }
            }

            return _IsDataSaved;
        }
        private void SetNextActionDetails(out SplitClaimDetails _SplitClaimDetails)
        {
            SplitClaimDetails oSplitClaimDetails = new SplitClaimDetails();           
            int row_num = 0;
            string _party = "";
            string _partyCode = "";
            string _partyDesc = "";
            Int64 _partyInsId = 0;
            Int64 _partyContactId = 0;

            try
            {
                #region " Line Next Action & Party "

                for (int i = 0; i <= c1SinglePayment.Rows.Count - 1; i++)
                {
                    if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.Claim)
                    {
                        if (c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID)).Trim() != "")
                        {
                            oSplitClaimDetails.TransactionMasterID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                            oSplitClaimDetails.TransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                            oSplitClaimDetails.ClaimNo = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                            oSplitClaimDetails.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
                            oSplitClaimDetails.ClinicID = gloGlobal.gloPMGlobal.ClinicID;
                        }
                    }

                    if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                    {
                        if (c1SinglePayment.GetData(i, COL_NEXT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT)).Trim() != "")
                        {
                            #region " Entire "
                            bool _addSplitLine = false;
                            dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Add();
                            dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nID"] = 0;
                            dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nClaimNo"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                            if (dsInsurancePayment_TVP.Tables["Credits"].Rows.Count > 0)
                            {
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nEOBPaymentID"] = dsInsurancePayment_TVP.Tables["Credits"].Rows[0]["nCreditID"];
                            }
                            else
                            {
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nEOBPaymentID"] = 0;
                            }
                            if (dsInsurancePayment_TVP.Tables["EOB"].Rows.Count > 0)
                            {
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nEOBID"] = dsInsurancePayment_TVP.Tables["EOB"].Rows[row_num]["nEOBID"];
                            }
                            else
                            {
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nEOBID"] = 0;
                            }
                            if (dsInsurancePayment_TVP.Tables["Debits"].Rows.Count > 0)
                            {
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nEOBPaymentDetailID"] = dsInsurancePayment_TVP.Tables["Debits"].Rows[row_num]["nDebitID"];
                            }
                            else
                            {
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nEOBPaymentDetailID"] = 0;
                            }
                            
                            dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nBillingTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                            dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nBillingTransactionDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));

                            dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
                            dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["dtDate"] = DBNull.Value;
                            dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["sUserName"] = AppSettings.UserName;
                            dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["sSubClaimNo"] = Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
                            dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nTrackMstTrnID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                            dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nTrackMstTrnDetailID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));

                            if (mskCloseDate.MaskCompleted == true)
                            {
                                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nCloseDate"] = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                            }

                            string _nextAction = "";
                            string _nextActionCode = "";
                            string _nextActionDesc = "";

                            _nextAction = Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT)).Trim();
                            _nextActionCode = _nextAction.Substring(0, _nextAction.IndexOf('-'));
                            _nextActionDesc = _nextAction.Substring(_nextAction.IndexOf('-') + 1, (_nextAction.Length - _nextAction.IndexOf('-')) - 1);

                            //.. Split the claim for all the actions (Rebill, Bill, Pending, None)

                            if (_nextActionCode == "R" || _nextActionCode == "B" || _nextActionCode == "P" || _nextActionCode == "N")
                            {
                                _addSplitLine = true;
                            }

                            dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["sNextActionCode"] = _nextActionCode;
                            dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["sNextActionDescription"] = _nextActionDesc;

                            if (c1SinglePayment.GetData(i, COL_PARTY) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PARTY)).Trim() != "")
                            {
                                _party = Convert.ToString(c1SinglePayment.GetData(i, COL_PARTY)).Trim();
                                _partyCode = _party.Substring(0, _party.IndexOf('-'));
                                _partyDesc = _party.Substring(_party.IndexOf('-') + 1, (_party.Length - _party.IndexOf('-')) - 1);

                                //.... Get the next party insuranceid & contactid number for the claim
                                DataTable _dt = null;
                                _dt = gloStripControl.PatientStripControl.GetInsuranceParties(Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID)), Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID))); //_dt = ogloEOBPaymentInsurance.GetClaimInsurances(PatientControl.ClaimNumber, PatientControl.PatientID);

                                if (_dt != null && _dt.Rows.Count > 0)
                                {
                                    for (int pIndex = 0; pIndex < _dt.Rows.Count; pIndex++)
                                    {
                                        if (Convert.ToString(_dt.Rows[pIndex]["nResponsibilityNo"]) == _partyCode)
                                        {
                                            _partyInsId = Convert.ToInt64(_dt.Rows[pIndex]["nInsuranceID"]);
                                            _partyContactId = Convert.ToInt64(_dt.Rows[pIndex]["nContactID"]);
                                            break;
                                        }
                                    }
                                }
                                if (_dt != null) { _dt.Dispose(); }

                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nNextActionPatientInsID"] = _partyInsId;
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nNextActionPatientInsName"] = _partyDesc.Trim();
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nNextActionPartyNumber"] = Convert.ToInt32(_partyCode.Trim());
                                dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nNextActionContactID"] = _partyContactId;
                            }
                            if (_nextActionCode == "R" || _nextActionCode == "B" || _nextActionCode == "P" || _nextActionCode == "N")
                            {
                                gloInsurancePaymentV2.UpdateClaimRemittanceRefNo(Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID)), ContactInsuranceID, PatientInsuranceID, Convert.ToInt32(_partyCode.Trim()), txtClaimRemittanceRef.Text.Trim(), gloGlobal.gloPMGlobal.ClinicID);
                            }

                            decimal _nextActionAmount = 0;
                            dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["dNextActionAmount"] = _nextActionAmount;
                            dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
                            dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["bIsVoid"] = 0;

                            if (_partyInsId == 0) //.self resp.
                            { dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nNextPartyType"] = PayerTypeV2.Self.GetHashCode(); }
                            else
                            { dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows[dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].Rows.Count - 1]["nNextPartyType"] = PayerTypeV2.Insurance.GetHashCode(); }

                            #region " Set Split Claim Details "

                            if (_addSplitLine == true)
                            {
                                SplitClaimLine oSplitLine = new SplitClaimLine();
                                oSplitLine.TransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                oSplitLine.TransactionMasterDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                oSplitLine.NextActionCode = _nextActionCode;
                                oSplitLine.InsuranceId = _partyInsId;
                                oSplitLine.ContactID = _partyContactId;
                                oSplitLine.ResponsibilityNo = Convert.ToInt32(_partyCode.Trim());

                                oSplitLine.Party = _partyDesc.Trim();

                                //dsInsurancePayment_TVP.Tables["ClaimRemitRefNo"].Rows.Add();
                                //dsInsurancePayment_TVP.Tables["ClaimRemitRefNo"].Rows[dsInsurancePayment_TVP.Tables["ClaimRemitRefNo"].Rows.Count - 1]["nTransactionID"] = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                //dsInsurancePayment_TVP.Tables["ClaimRemitRefNo"].Rows[dsInsurancePayment_TVP.Tables["ClaimRemitRefNo"].Rows.Count - 1]["nContactID"] = oSplitLine.ContactID;
                                //dsInsurancePayment_TVP.Tables["ClaimRemitRefNo"].Rows[dsInsurancePayment_TVP.Tables["ClaimRemitRefNo"].Rows.Count - 1]["nInsuranceID"] = oSplitLine.InsuranceId;
                                //dsInsurancePayment_TVP.Tables["ClaimRemitRefNo"].Rows[dsInsurancePayment_TVP.Tables["ClaimRemitRefNo"].Rows.Count - 1]["nResponsibilityNo"] = oSplitLine.ResponsibilityNo;
                                //dsInsurancePayment_TVP.Tables["ClaimRemitRefNo"].Rows[dsInsurancePayment_TVP.Tables["ClaimRemitRefNo"].Rows.Count - 1]["sClaimRemittanceRefNo"] = txtClaimRemittanceRef.Text.Trim();
                                //dsInsurancePayment_TVP.Tables["ClaimRemitRefNo"].AcceptChanges();

                                oSplitClaimDetails.Lines.Add(oSplitLine);
                                oSplitLine.Dispose();
                            }

                            #endregion " Set Split Claim Details "

                            #endregion " Entire "

                            dsInsurancePayment_TVP.Tables["BL_EOB_NextAction"].AcceptChanges();
                        }
                    }
                }

                #endregion " Line Next Action & Party "
                               
                _SplitClaimDetails = oSplitClaimDetails;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _SplitClaimDetails = null;
            }
            finally
            {
                if (oSplitClaimDetails != null) { oSplitClaimDetails.Dispose(); }
            }
          
        }
             
        private DataTable GetSplittedClaimsHoldInfo(SplitClaimDetails oSplitClaimDetails)
        {
            DataTable _dtHoldInfo = null;
            DataRow _drParentClaimHoldNote = null;

            gloSplitClaim ogloSplitClaim = new gloSplitClaim(AppSettings.ConnectionStringPM);
            try
            {
                if (ClaimDetails.IsClaimOnHold)
                {
                    _drParentClaimHoldNote = gloInsurancePaymentV2.GetBillingHoldNote(oSplitClaimDetails.TransactionMasterID, oSplitClaimDetails.TransactionID);
                    _dtHoldInfo = ogloSplitClaim.GetSubClaims(oSplitClaimDetails);
                    if (_dtHoldInfo != null)
                    {
                        if (_dtHoldInfo.Rows.Count > 1)
                        {
                            frmSplitClaimHoldSelection ofrmSplitClaimHoldSelection = new frmSplitClaimHoldSelection(_dtHoldInfo, _drParentClaimHoldNote, PatientControl.ClaimNumber, PatientControl.SubClaimNumber);
                            ofrmSplitClaimHoldSelection.ShowDialog(this);
                            _dtHoldInfo = ofrmSplitClaimHoldSelection.SplittedClaims;
                            ofrmSplitClaimHoldSelection.Dispose();
                            ofrmSplitClaimHoldSelection = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloSplitClaim != null) { ogloSplitClaim.Dispose(); }
            }
            
            return _dtHoldInfo;
        }

        private void SetCreditsDetails(gloBilling.gloAccountPayment.dsPaymentTVP_V2 dsInsurancePayment_TVP, Int64 _nCreditID)
        {

            dsInsurancePayment_TVP.Tables["Credits"].Rows.Add();
            if (_nCreditID != 0)
            {
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nCreditID"] = _nCreditID;
            }
            else
            {
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nCreditID"] = 0;
            }
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sReceiptNo"] = txtCheckNumber.Text.Trim();
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["dReceiptAmount"] = CheckAmount;
            if (mskCheckDate.MaskCompleted)
            {
                mskCheckDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["dtReceiptDate"] = Convert.ToDateTime(mskCheckDate.Text);
            }
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPayerType"] = PayerTypeV2.Insurance.GetHashCode();
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPayerID"] = SelectedInsuranceCompanyID;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sPayerName"] = SelectedInsuranceCompany;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sPaymentNo"] = lblPaymetNo.Text.Trim().Split('#')[1];
            //None = 0,
            //Cash = 1,
            //Check = 2,
            //MoneyOrder = 3,
            //CreditCard = 4,
            //EFT = 5
            if (SelectedPaymentMode == PaymentModeV2.Check)
            { dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 2; }
            else if (SelectedPaymentMode == PaymentModeV2.Cash)
            { dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 1; }
            else if (SelectedPaymentMode == PaymentModeV2.EFT)
            { dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 5; }
            else if (SelectedPaymentMode == PaymentModeV2.Voucher)
            { dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 6; }
            else if (SelectedPaymentMode == PaymentModeV2.MoneyOrder)
            { dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 3; }
            else if (SelectedPaymentMode == PaymentModeV2.CreditCard)
            { dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 4; }
            else
            { dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentMode"] = 0; }

            if (mskCloseDate.MaskCompleted == true)
            {
                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["dtCloseDate"] = Convert.ToDateTime(mskCloseDate.Text);
            }
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentTrayID"] = SelectedPaymentTrayID;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sPaymentTrayDesc"] = SelectedPaymentTray;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nUserID"] = gloGlobal.gloPMGlobal.UserID;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sUserName"] = AppSettings.UserName;

            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["bIsPaymentVoid"] = 0;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nVoidType"] = 0;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["dtPaymentVoidCloseDate"] = DBNull.Value;

                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPAccountID"] = this.nPAccountID;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nAccountPatientID"] = this.nAccountPatientID;
                dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nGuarantorID"] = this.nGuarantorID;


            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sPaymentNote"] = "Payment Note";
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nPaymentVoidTrayID"] = DBNull.Value;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sPaymentVoidTrayDesc"] = "Blank Tray";

            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["nEntryType"] = PaymentEntryTypeV2.InsuracePayment.GetHashCode();

            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["Credits_EXTID"] = 0;
            if (SelectedPaymentMode == PaymentModeV2.CreditCard)
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
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["ClinicID"] = gloGlobal.gloPMGlobal.ClinicID;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["PaymentVoidDateTime"] = DBNull.Value;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["CreatedDateTime"] = DBNull.Value;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["ModifiedDateTime"] = DBNull.Value;

            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sVersion"] = gloGlobal.gloPMGlobal.ApplicationVersion;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["sMachineName"] = Environment.MachineName;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["SiteID"] = "";
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["IsFinished"] = false;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["IsERAPost"] = false;
            dsInsurancePayment_TVP.Tables["Credits"].Rows[dsInsurancePayment_TVP.Tables["Credits"].Rows.Count - 1]["BPRID"] = DBNull.Value;

            dsInsurancePayment_TVP.Tables["Credits"].AcceptChanges();
        }

        string GetSelectedReasonCode(int ColumnIndex)
        {
            string _reasonCode = string.Empty;
            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(AppSettings.ConnectionStringPM);
            object oSetValue = null;

            try
            {
                switch (ColumnIndex)
                {
                    case COL_WRITEOFF:
                        oSetting.GetSetting("RCODEWRITEOFF", out oSetValue);
                        break;
                    case COL_COPAY:
                        oSetting.GetSetting("RCODECOPAY", out oSetValue);
                        break;
                    case COL_DEDUCTIBLE:
                        oSetting.GetSetting("RCODEDEDUCTIBLE", out oSetValue);
                        break;
                    case COL_COINSURANCE:
                        oSetting.GetSetting("RCODECOINSURANCE", out oSetValue);
                        break;
                    case COL_WITHHOLD:
                        oSetting.GetSetting("RCODEWITHHOLD", out oSetValue);
                        break;
                }

                if (oSetValue != null && oSetValue.ToString().Trim() != "")
                {
                    _reasonCode = oSetValue.ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oSetting != null) { oSetting.Dispose(); }
                if (oSetValue != null) { oSetValue = null; }
               
            }
           
            return _reasonCode;
        }

        private DataTable GetClaimEOB()
        {
            DataTable _dtResult = null;

            try
            {

            }
            catch //(Exception ex)
            {
                throw;
            }
            finally
            { 
            
            }

            return _dtResult;
        }

        #endregion

        #region " Other Validation Methods "

        private bool IsValidClaim()
        {
            bool _isValidClaim = true;

            if (ClaimDetails.IsClaimExist)
            {
                if (ClaimDetails.IsClaimVoided)
                {
                    DialogResult _result = MessageBox.Show("Claim is voided, do you want it to open for view ?", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (_result == DialogResult.Yes)
                    {
                        if (ClaimDetails.TransactionID != 0)
                        {
                            ShowModifyCharges();

                        }
                    }
                    _isValidClaim = false;
                }
                else
                {
                    if (ClaimDetails.ValidateBatch)
                    {
                        if (!ClaimDetails.IsClaimBatched)
                        {
                            MessageBox.Show("Claim selected for payment is not yet batched. Please batch the claim for payment.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isValidClaim = false;
                        }
                    }
                    else
                    {
                        _isValidClaim = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("Claim selected is invalid or does not exist", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isValidClaim = false;
            }
            return _isValidClaim;
        }

        private bool IsNextOrPartyAllowed(RowColEventArgs e)
        {
            bool _isValid = true;
            bool _isChargeLineSplitted = Convert.ToBoolean(c1SinglePayment.GetData(e.Row, COL_ISSPLITTED));
            bool _isCorrection = Convert.ToBoolean(c1SinglePayment.GetData(e.Row, COL_ISCORRECTION));

            string _next = Convert.ToString(c1SinglePayment.GetData(e.Row, COL_NEXT)).Trim().Replace('-', ' ').Replace(',', ' ').Replace('(', ' ').Replace(')', ' ').Trim();
            string _party = Convert.ToString(c1SinglePayment.GetData(e.Row, COL_PARTY)).Trim().Replace('-', ' ').Replace(',', ' ').Replace('(', ' ').Replace(')', ' ').Trim();

            if ((_next != null) || (_party != null))
            {
                if (_isChargeLineSplitted) //|| (_isCorrection)
                {
                    MessageBox.Show("You cannot select any next action or party for this line.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isValid = false;
                    c1SinglePayment.SetData(e.Row, COL_NEXT, null);
                    c1SinglePayment.SetData(e.Row, COL_PARTY, null);
                }
            }
            return _isValid;
        }

        private bool IsValidCode(RowColEventArgs e, int Column)
        {
            bool _isValid = true;

            
            string _columnText = Convert.ToString(c1SinglePayment.GetData(e.Row, Column)).Trim().Replace('-', ' ').Replace(',', ' ').Replace('(', ' ').Replace(')', ' ').Trim();
            CellStyle cs = c1SinglePayment.GetCellStyle(e.Row, Column);

            _IsValidEntered = true;

            if (_columnText != null)
            {
                _columnText = Convert.ToString(c1SinglePayment.GetData(e.Row, Column)).Trim();
                bool _isValidCode = cs.ComboList.Contains(_columnText);

                if (!_isValidCode)
                {
                    MessageBox.Show("Please select a valid code.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isValid = false;
                    _IsValidEntered = false;
                    c1SinglePayment.SetData(e.Row, COL_NEXT, null);
                    c1SinglePayment.SetData(e.Row, COL_PARTY, null);

                    if (e.Row <= c1SinglePaymentCorrTB.Rows.Count - 1)
                    {
                        if (PaymentAction == FormMode.CorrectionMode)
                        {
                            c1SinglePaymentCorrTB.SetData(e.Row, COL_NEXT, null);
                            c1SinglePaymentCorrTB.SetData(e.Row, COL_PARTY, null);
                        }
                    }
                }
            }
            return _isValid;
        }

        private bool IsPartyBilled(string _selectedParty, string _selectedAction)
        {
            bool _isBilled = false;
            int _selectedResponsibilityNo = 0;
            Int64 _insuranceID = 0;

            if (!_selectedParty.Equals(string.Empty))
            {
                _selectedResponsibilityNo = Convert.ToInt32(_selectedParty.Substring(0, 1));
                _insuranceID = gloInsurancePaymentV2.GetClaimInsuranceID(PatientControl.ClaimNumber, _selectedResponsibilityNo);
                _isBilled = gloInsurancePaymentV2.IsResponsibilityBilled(PatientControl.ClaimNumber, PatientControl.SubClaimNumber, _insuranceID, PatientControl.GetSelectedPartyResponsibility(), _selectedAction);
            }
            return _isBilled;
        }

        private bool IsPartyBilled(string _selectedParty, string _selectedAction, Int64 TransactionMasterID, Int64 TransactionID)
        {
            bool _isBilled = false;
            int _selectedResponsibilityNo = 0;
            Int64 _insuranceID = 0;

            if (!_selectedParty.Equals(string.Empty))
            {
                _selectedResponsibilityNo = Convert.ToInt32(_selectedParty.Substring(0, 1));
                _insuranceID = gloInsurancePaymentV2.GetClaimInsuranceIDRevised(TransactionMasterID, TransactionID, _selectedResponsibilityNo);
                _isBilled = gloInsurancePaymentV2.IsResponsibilityBilled(PatientControl.ClaimNumber, PatientControl.SubClaimNumber, _insuranceID, PatientControl.GetSelectedPartyResponsibility(), _selectedAction);
            }
            return _isBilled;
        }

        private bool IsValidParty(RowColEventArgs e)
        {
            bool _isValid = true;

            string _party = Convert.ToString(c1SinglePayment.GetData(e.Row, COL_PARTY)).Trim().Replace('-', ' ').Replace(',', ' ').Replace('(', ' ').Replace(')', ' ').Trim();
            CellStyle cs = c1SinglePayment.GetCellStyle(c1SinglePayment.RowSel, COL_PARTY);

            string _selectedAction = Convert.ToString(c1SinglePayment.GetData(e.Row, COL_NEXT));
            string _selectedParty = Convert.ToString(c1SinglePayment.GetData(e.Row, COL_PARTY));

            string _currentResponsibility = PatientControl.GetCurrentParty();
            string _nextResponsibility = PatientControl.GetNextParty();

            CellStyle csAction = c1SinglePayment.GetCellStyle(e.Row, COL_NEXT);
            CellStyle csParty = c1SinglePayment.GetCellStyle(e.Row, COL_PARTY);

            int _selectedResponsibilityNo = 0;
            int _currentResponsibilityNo = 0;
            int _nextResponsibilityNo = 0;
            int _selfResponsibilityNo = PatientControl.GetSelfPartyNo();

            Int64 TransactionMasterID = Convert.ToInt64(c1SinglePayment.GetData(e.Row, COL_BILLING_TRANSACTON_ID));
            Int64 TransactionID = Convert.ToInt64(c1SinglePayment.GetData(e.Row, COL_TRACK_BILLING_TRANSACTON_ID));

            if (!_selectedParty.Equals(string.Empty)) { _selectedResponsibilityNo = Convert.ToInt32(_selectedParty.Substring(0, 1)); }
            if (!_currentResponsibility.Equals(string.Empty)) { _currentResponsibilityNo = Convert.ToInt32(_currentResponsibility.Substring(0, 1)); }
            if (!_nextResponsibility.Equals(string.Empty)) { _nextResponsibilityNo = Convert.ToInt32(_nextResponsibility.Substring(0, 1)); }

            bool _isBilled = IsPartyBilled(_selectedParty, _selectedAction, TransactionMasterID, TransactionID);

            if (!_selectedParty.Equals(string.Empty))
            {
                if (_selectedAction.StartsWith("B")) // B = Bill
                {
                    if (_isBilled)
                    {
                        MessageBox.Show("Party " + _selectedResponsibilityNo + " has already been billed. Select a different next party option.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1SinglePayment.SetData(e.Row, COL_PARTY, null);
                        _isValid = false;

                        _IsValidEntered = false;
                        if (e.Row <= c1SinglePaymentCorrTB.Rows.Count - 1)
                        {
                            if (PaymentAction == FormMode.CorrectionMode)
                            {
                                c1SinglePaymentCorrTB.SetData(e.Row, COL_PARTY, null);
                            }
                        }
                    }
                    else
                    { _isValid = true; }
                       
                }
                else if (_selectedAction.StartsWith("R")) // R = Rebill
                {
                    if (_selectedResponsibilityNo.Equals(_selfResponsibilityNo))
                    {
                        MessageBox.Show("Party " + _selfResponsibilityNo + " cannot be selected for rebill.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        c1SinglePayment.SetData(e.Row, COL_PARTY, null);
                        _isValid = false;

                        _IsValidEntered = false;
                        if (e.Row <= c1SinglePaymentCorrTB.Rows.Count - 1)
                        {
                            c1SinglePaymentCorrTB.SetData(e.Row, COL_PARTY, null);
                        }
                    }
                    else
                    {
                        if (!_isBilled)
                        {
                            MessageBox.Show("Party " + _selectedResponsibilityNo + " has not been billed, so it cannot be rebilled. Select a different next party option.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            c1SinglePayment.SetData(e.Row, COL_PARTY, null);
                            _isValid = false;

                            _IsValidEntered = false;
                            if (e.Row <= c1SinglePaymentCorrTB.Rows.Count - 1)
                            {
                                if (PaymentAction == FormMode.CorrectionMode)
                                {
                                    c1SinglePaymentCorrTB.SetData(e.Row, COL_PARTY, null);
                                }
                            }
                        }
                        else
                        { _isValid = true; }
                    }
                 
                }
                else if (_selectedAction.StartsWith("P")) // P = Pending / No Bill
                {
                    if (_selectedResponsibilityNo >= _currentResponsibilityNo)
                    {
                        if (_selectedResponsibilityNo.Equals(_selfResponsibilityNo))
                        {
                            MessageBox.Show("Party " + _selectedResponsibilityNo + " cannot be selected for pending.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            #region " clear the party column for all the service lines "

                            this.c1SinglePayment.AfterEdit -= new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_AfterEdit);
                            for (int rInd = 1; rInd < c1SinglePayment.Rows.Count; rInd++)
                            {
                                if (c1SinglePayment.GetData(rInd, COL_SERVICELINE_TYPE) != null && Convert.ToInt32(c1SinglePayment.GetData(rInd, COL_SERVICELINE_TYPE)) == ColServiceLineType.ServiceLine.GetHashCode())
                                { c1SinglePayment.SetData(rInd, COL_PARTY, null); }
                            }
                            this.c1SinglePayment.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(this.c1SinglePayment_AfterEdit);

                            #endregion

                            _isValid = false;

                            _IsValidEntered = false;
                            if (e.Row <= c1SinglePaymentCorrTB.Rows.Count - 1)
                            {
                                if (PaymentAction == FormMode.CorrectionMode)
                                {
                                    c1SinglePaymentCorrTB.SetData(e.Row, COL_PARTY, null);
                                }
                            }

                        }
                        else
                        { _isValid = true; }
                    }
                       
                }
                else if (_selectedAction.Equals(string.Empty))
                {
                    MessageBox.Show("Please select next action, before selecting any insurance party.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isValid = false;
                    SetDefaultParty(_selectedAction, string.Empty, _currentResponsibility, _nextResponsibility, false, e);

                    _IsValidEntered = false;
                    if (e.Row <= c1SinglePaymentCorrTB.Rows.Count - 1)
                    {
                        if (PaymentAction == FormMode.CorrectionMode)
                        {
                            c1SinglePaymentCorrTB.SetData(e.Row, COL_PARTY, c1SinglePayment.GetData(e.Row, COL_PARTY));
                        }
                    }
                }
            }
            else
            {
                _isValid = true;

                if (_selectedResponsibilityNo.Equals(0))
                { SetDefaultParty(_selectedAction, _selectedParty, _currentResponsibility, _nextResponsibility, true, e); }
                else
                { SetDefaultParty(_selectedAction, _selectedParty, _currentResponsibility, _nextResponsibility, false, e); }
            }
            return _isValid;
        }

        private bool IsValidAmount(RowColEventArgs e)
        {
            bool _isValidContext = true;
            _IsValidEntered = true;
            decimal _value = 0;
            decimal _allowed = Convert.ToDecimal(c1SinglePayment.GetData(e.Row, COL_ALLOWED));
            decimal _charges = Convert.ToDecimal(c1SinglePayment.GetData(e.Row, COL_TOTALCHARGE));
            string _cptCode = Convert.ToString(c1SinglePayment.GetData(e.Row, COL_CPT_CODE));

            _value = Convert.ToDecimal(c1SinglePayment.GetData(e.Row, e.Col));

            // Validation for negative amount
            if (_value < 0 && e.Col != COL_WRITEOFF && (e.Col != COL_WITHHOLD || !_bAllowNegativeWHAmount))
            {
                
                MessageBox.Show("Payment amount cannot be negative", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isValidContext = false;
                _IsValidEntered = false;
                // If allowed then reset to last allowed amount else set to null
                if (e.Col.Equals(COL_ALLOWED))
                {
                    c1SinglePayment.SetData(e.Row, e.Col, _allowedAmountBeforeEdit);

                    if (PaymentAction == FormMode.CorrectionMode)
                    {
                        double dAmount = 0;
                        dAmount = Convert.ToDouble(c1SinglePayment.GetData(e.Row, e.Col)) - Convert.ToDouble(c1SinglePayment.GetData(e.Row, COL_LAST_ALLOWED));

                        if (dAmount != 0)
                        {
                            c1SinglePaymentCorrTB.SetData(e.Row, COL_ALLOWED, dAmount);

                            //Assigning Write off Delta Value in Delta Grid.
                            dAmount = Convert.ToDouble(c1SinglePayment.GetData(e.Row, COL_WRITEOFF)) - Convert.ToDouble(c1SinglePayment.GetData(e.Row, COL_LAST_WRITEOFF));
                            c1SinglePaymentCorrTB.SetData(e.Row, COL_WRITEOFF, dAmount);
                            //** 
                        }
                    }
                }
                else
                {
                    c1SinglePayment.SetData(e.Row, e.Col, null);
                    if (PaymentAction == FormMode.CorrectionMode)
                    {
                        c1SinglePaymentCorrTB.SetData(e.Row, e.Col, null);
                    }
                }
               
            }
            else
            {
                // Validation for allowed amount
                // Check only when Insurance responsibility is Primary
                if (e.Col.Equals(COL_ALLOWED))
                {
                    _isValidContext = CheckAllowedAmount(e.Row);
                    if (_isValidContext == false)
                    {
                        c1SinglePayment.SetData(e.Row, COL_ALLOWED, _allowedAmountBeforeEdit);

                        _IsValidEntered = false;
                        double dAmount = 0;
                        dAmount = Convert.ToDouble(c1SinglePayment.GetData(e.Row, COL_ALLOWED)) - Convert.ToDouble(c1SinglePayment.GetData(e.Row, COL_LAST_ALLOWED));

                        if (PaymentAction == FormMode.CorrectionMode)
                        {
                            if (dAmount != 0)
                            {
                                c1SinglePaymentCorrTB.SetData(e.Row, COL_ALLOWED, dAmount);

                                //Assigning Write off Delta Value in Delta Grid.

                                dAmount = Convert.ToDouble(c1SinglePayment.GetData(e.Row, COL_WRITEOFF)) - Convert.ToDouble(c1SinglePayment.GetData(e.Row, COL_LAST_WRITEOFF));
                                c1SinglePaymentCorrTB.SetData(e.Row, COL_WRITEOFF, dAmount);

                                //** 
                            }
                            else
                            {
                                if (PaymentAction == FormMode.CorrectionMode)
                                {
                                    c1SinglePaymentCorrTB.SetData(e.Row, COL_ALLOWED, null);
                                }
                            }
                        }
                    }
                
                }
                // Warning : Zero Payment
                else if (e.Col.Equals(COL_PAYMENT))
                {
                    if (_value == 0)
                    {
                        DialogResult _result = MessageBox.Show("Are you sure you want to do zero payment ?", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (_result.Equals(DialogResult.No))
                        { c1SinglePayment.SetData(e.Row, e.Col, null); }
                        else if (_result.Equals(DialogResult.Yes))
                        { _isValidContext = true; }
                    }
                }

            }
            if (!_isValidContext)
            {
                c1SinglePayment.Focus();
                c1SinglePayment.Select(e.Row, e.Col);
            }
            return _isValidContext;
        }

        #endregion

        #region " Warnings - Save "

        private void CheckInsuranceCompany()
        {
            if (SelectedInsuranceCompanyID != 0)
            {
                if (ContactInsuranceID != 0)
                {
                    gloAccountsV2.PaymentCollection.gloEOBPaymentInsurance ogloIns = new gloAccountsV2.PaymentCollection.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);
                    if (ogloIns.IsContactCompanyAssociated(ContactInsuranceID, SelectedInsuranceCompanyID) == false)
                    {
                        DialogResult _result = MessageBox.Show("Warning - Insurance Company does not match Claim." + Environment.NewLine + " Continue?", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    ogloIns.Dispose();
                }
            }
        }

        private bool CheckForSameCheck()
        {
            try
            {
                if (SelectedPaymentMode == PaymentModeV2.Check || SelectedPaymentMode == PaymentModeV2.Voucher)
                {
                    if (!IsPaymentInProcess)
                    {
                        string _checkNo = "";
                        DateTime _checkDate = DateTime.Now;
                        string _showCheckDate = "";

                        if (txtCheckNumber.Text.Trim() != "")
                        { _checkNo = txtCheckNumber.Text.Trim(); }

                        if (mskCheckDate.MaskCompleted == true)
                        {
                            _checkDate = Convert.ToDateTime(mskCheckDate.Text);
                            _showCheckDate = mskCheckDate.Text;
                        }
                        bool isCheckExist = gloInsurancePaymentV2.IsExistCheck(_checkNo, _checkDate, CheckAmount, PayerTypeV2.Insurance, SelectedPaymentMode.GetHashCode());
                        if (isCheckExist)
                        {
                            DialogResult _checkDlg = DialogResult.None;
                            string _message = "";
                            if (SelectedPaymentMode == PaymentModeV2.Check)
                            {
                                _message = " Same Check with Check#: " + _checkNo + ", Check Date: " + _showCheckDate + Environment.NewLine + " and Check Amount: $" + CheckAmount.ToString("#0.00") + " " + Environment.NewLine + " already exists in the system." + Environment.NewLine + " Do you want to continue with save? ";
                            }
                            else if (SelectedPaymentMode == PaymentModeV2.Voucher)
                            {
                                _message = " Same Voucher with Voucher#: " + _checkNo + ", Voucher Date: " + _showCheckDate + Environment.NewLine + " and Voucher Amount: $" + CheckAmount.ToString("#0.00") + " " + Environment.NewLine + " already exists in the system." + Environment.NewLine + " Do you want to continue with save? ";
                            }
                            _checkDlg = MessageBox.Show(_message, AppSettings.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                            if (_checkDlg == DialogResult.Cancel)
                            {
                                txtCheckNumber.SelectAll();
                                txtCheckNumber.Focus();
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                return false;
            }
        }

        private bool CheckChargeLineBalances_Old()
        {
            decimal _currentPaymentAmt = 0;
            decimal _currentWriteOffAmt = 0;
            decimal _cuurentCopayAmt = 0;
            decimal _currentDeductibleAmt = 0;
            decimal _currentCoInsAmt = 0;
            decimal _currentWithholdAmt = 0;
            decimal _currentOtherAdjustmentAmt = 0;
            decimal _totalCharges = 0;
            string _cptCode = "";

            if (PatientControl != null && PatientControl.PatientID > 0 && PatientControl.ClaimNumber > 0)
            {               
                if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                {
                    for (int rIndex = 1; rIndex < c1SinglePayment.Rows.Count; rIndex++)
                    {
                        if (c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine && IsInsuranceParty(rIndex) == true)
                        {
                            _currentPaymentAmt = 0;
                            _currentWriteOffAmt = 0;
                            _cuurentCopayAmt = 0;
                            _currentDeductibleAmt = 0;
                            _currentCoInsAmt = 0;
                            _currentWithholdAmt = 0;
                            _currentOtherAdjustmentAmt = 0;
                            _totalCharges = 0;
                            _cptCode = "";

                            if (c1SinglePayment.GetData(rIndex, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_PAYMENT)).Trim() != "")
                            { _currentPaymentAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_PAYMENT)); }
                            if (c1SinglePayment.GetData(rIndex, COL_WRITEOFF) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_WRITEOFF)).Trim() != "")
                            { _currentWriteOffAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_WRITEOFF)); }
                            if (c1SinglePayment.GetData(rIndex, COL_COPAY) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_COPAY)).Trim() != "")
                            { _cuurentCopayAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_COPAY)); }
                            if (c1SinglePayment.GetData(rIndex, COL_DEDUCTIBLE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_DEDUCTIBLE)).Trim() != "")
                            { _currentDeductibleAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_DEDUCTIBLE)); }
                            if (c1SinglePayment.GetData(rIndex, COL_COINSURANCE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_COINSURANCE)).Trim() != "")
                            { _currentCoInsAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_COINSURANCE)); }
                            if (c1SinglePayment.GetData(rIndex, COL_WITHHOLD) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_WITHHOLD)).Trim() != "")
                            { _currentWithholdAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_WITHHOLD)); }
                            if (c1SinglePayment.GetData(rIndex, COL_TOTALCHARGE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_TOTALCHARGE)).Trim() != "")
                            { _totalCharges = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_TOTALCHARGE)); }
                            if (c1SinglePayment.GetData(rIndex, COL_CPT_CODE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CPT_CODE)).Trim() != "")
                            { _cptCode = Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CPT_CODE)); }

                            if (c1SinglePayment.GetData(rIndex, COL_REASON) != null)
                            {
                                _currentOtherAdjustmentAmt = 0;

                                #region " Get the other reason code amount "

                                C1FlexGrid c1OtherReason = ((C1.Win.C1FlexGrid.C1FlexGrid)c1SinglePayment.GetData(rIndex, COL_REASON));

                                if (c1OtherReason != null && c1OtherReason.Rows.Count > 1)
                                {
                                    for (int rCodeIndex = 1; rCodeIndex < c1OtherReason.Rows.Count; rCodeIndex++)
                                    {
                                        if (c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["Amount"].Index) != null
                                            && Convert.ToString(c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["Amount"].Index)).Trim() != "")
                                        {
                                            try
                                            {
                                                _currentOtherAdjustmentAmt += Convert.ToDecimal(c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["Amount"].Index));
                                            }
                                            catch (Exception)
                                            {
                                                continue;
                                            }

                                        }
                                    }

                                }

                                #endregion " Get the other reason code amount "

                            }

                            if ((_totalCharges - _currentPaymentAmt) != (_currentWriteOffAmt + _cuurentCopayAmt + _currentDeductibleAmt + _currentCoInsAmt + _currentWithholdAmt + _currentOtherAdjustmentAmt))
                            {
                                DialogResult _dlgRst = DialogResult.None;

                                #region " Build Warning Message Text "

                                string _warningMsg = "";

                                _warningMsg =
                                " Warning:  Remittance for Charge " + (rIndex - 1) + " ('" + _cptCode + "') does not Balance." + Environment.NewLine + " " + Environment.NewLine + "" +
                                " This can cause secondary payer billing rejections. " + Environment.NewLine + " " + Environment.NewLine + "" +
                                " Remittances must add up according to this formula: " + Environment.NewLine + "" +
                                " Billed Amount – Insurance Payment must equal sum of " + Environment.NewLine + "" +
                                " Write-off + Copay + Deduct + Co-ins + Withhold + Other Reasons." + Environment.NewLine + " " + Environment.NewLine + "" +
                                " Correct the remittance?";


                                #endregion " Build Warning Message Text "

                                _dlgRst = MessageBox.Show(_warningMsg, AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                                if (_dlgRst == DialogResult.No)
                                {
                                    continue;
                                }
                                else
                                {
                                    c1SinglePayment.Focus();
                                    c1SinglePayment.Select(rIndex, COL_PAYMENT, true);
                                    return false;
                                }

                            }
                        }
                    }
                }

            }
            
            return true;
        }

        private bool CheckChargeLineBalances_ReasonCodeAmount()
        {
            decimal _currentPaymentAmt = 0;
            decimal _currentWriteOffAmt = 0;
            decimal _cuurentCopayAmt = 0;
            decimal _currentDeductibleAmt = 0;
            decimal _currentCoInsAmt = 0;
            decimal _currentWithholdAmt = 0;

            decimal _reasonWriteOffAmt = 0;
            decimal _reasonCopayAmt = 0;
            decimal _reasonDeductibleAmt = 0;
            decimal _reasonCoInsAmt = 0;
            decimal _reasonWithholdAmt = 0;

            decimal _currentOtherAdjustmentAmt = 0;
            decimal _totalCharges = 0;
            string _cptCode = "";

            if (PatientControl != null && PatientControl.PatientID > 0 && PatientControl.ClaimNumber > 0)
            {
                if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                {
                    for (int rIndex = 1; rIndex < c1SinglePayment.Rows.Count; rIndex++)
                    {
                        if (c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                        {
                            _currentPaymentAmt = 0;

                            _currentWriteOffAmt = 0;
                            _cuurentCopayAmt = 0;
                            _currentDeductibleAmt = 0;
                            _currentCoInsAmt = 0;
                            _currentWithholdAmt = 0;

                            _currentOtherAdjustmentAmt = 0;
                            _totalCharges = 0;
                            _cptCode = "";

                            if (c1SinglePayment.GetData(rIndex, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_PAYMENT)).Trim() != "")
                            { _currentPaymentAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_PAYMENT)); }

                            if (c1SinglePayment.GetData(rIndex, COL_WRITEOFF) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_WRITEOFF)).Trim() != "")
                            { _currentWriteOffAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_WRITEOFF)); }
                            if (c1SinglePayment.GetData(rIndex, COL_COPAY) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_COPAY)).Trim() != "")
                            { _cuurentCopayAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_COPAY)); }
                            if (c1SinglePayment.GetData(rIndex, COL_DEDUCTIBLE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_DEDUCTIBLE)).Trim() != "")
                            { _currentDeductibleAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_DEDUCTIBLE)); }
                            if (c1SinglePayment.GetData(rIndex, COL_COINSURANCE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_COINSURANCE)).Trim() != "")
                            { _currentCoInsAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_COINSURANCE)); }
                            if (c1SinglePayment.GetData(rIndex, COL_WITHHOLD) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_WITHHOLD)).Trim() != "")
                            { _currentWithholdAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_WITHHOLD)); }

                            if (c1SinglePayment.GetData(rIndex, COL_TOTALCHARGE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_TOTALCHARGE)).Trim() != "")
                            { _totalCharges = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_TOTALCHARGE)); }
                            if (c1SinglePayment.GetData(rIndex, COL_CPT_CODE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CPT_CODE)).Trim() != "")
                            { _cptCode = Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CPT_CODE)); }
                            string amountMismatch = "";
                            _reasonWriteOffAmt = 0;
                            _reasonCopayAmt = 0;
                            _reasonDeductibleAmt = 0;
                            _reasonCoInsAmt = 0;
                            _reasonWithholdAmt = 0;
                            if (c1SinglePayment.GetData(rIndex, COL_REASON) != null)
                            {
                                _currentOtherAdjustmentAmt = 0;

                                #region " Get the other reason code amount "

                                C1FlexGrid c1OtherReason = ((C1.Win.C1FlexGrid.C1FlexGrid)c1SinglePayment.GetData(rIndex, COL_REASON));

                                _reasonWriteOffAmt = 0;
                                _reasonCopayAmt = 0;
                                _reasonDeductibleAmt = 0;
                                _reasonCoInsAmt = 0;
                                _reasonWithholdAmt = 0;

                                if (c1OtherReason != null && c1OtherReason.Rows.Count > 1)
                                {
                                    for (int rCodeIndex = 1; rCodeIndex < c1OtherReason.Rows.Count; rCodeIndex++)
                                    {
                                        if (c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["Amount"].Index) != null
                                            && Convert.ToString(c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["Amount"].Index)).Trim() != "")
                                        {
                                            try
                                            {
                                                if (Convert.ToString(c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["ReasonCodeType"].Index)) == "W/O")
                                                {
                                                    _reasonWriteOffAmt = _reasonWriteOffAmt + Convert.ToDecimal(c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["Amount"].Index));
                                                }
                                                else if (Convert.ToString(c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["ReasonCodeType"].Index)) == "Copay")
                                                {
                                                    _reasonCopayAmt = _reasonCopayAmt + Convert.ToDecimal(c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["Amount"].Index));
                                                }
                                                else if (Convert.ToString(c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["ReasonCodeType"].Index)) == "Deduct")
                                                {
                                                    _reasonDeductibleAmt = _reasonDeductibleAmt + Convert.ToDecimal(c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["Amount"].Index));
                                                }
                                                else if (Convert.ToString(c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["ReasonCodeType"].Index)) == "Co-ins")
                                                {
                                                    _reasonCoInsAmt = _reasonCoInsAmt + Convert.ToDecimal(c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["Amount"].Index));
                                                }
                                                else if (Convert.ToString(c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["ReasonCodeType"].Index)) == "Withhold")
                                                {
                                                    _reasonWithholdAmt = _reasonWithholdAmt + Convert.ToDecimal(c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["Amount"].Index));
                                                }

                                                _currentOtherAdjustmentAmt += Convert.ToDecimal(c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["Amount"].Index));
                                            }
                                            catch (Exception)
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                }

                                #endregion " Get the other reason code amount "

                            }

                            if (_currentWriteOffAmt != _reasonWriteOffAmt) { amountMismatch = "W/O"; }
                            if (_cuurentCopayAmt != _reasonCopayAmt) { if (amountMismatch != "") { amountMismatch = amountMismatch + ",Copay"; } else { amountMismatch = "Copay"; } }
                            if (_currentDeductibleAmt != _reasonDeductibleAmt) { if (amountMismatch != "") { amountMismatch = amountMismatch + ",Deduct"; } else { amountMismatch = "Deduct"; } }
                            if (_currentCoInsAmt != _reasonCoInsAmt) { if (amountMismatch != "") { amountMismatch = amountMismatch + ",Co-ins"; } else { amountMismatch = "Co-ins"; } }
                            if (_currentWithholdAmt != _reasonWithholdAmt) { if (amountMismatch != "") { amountMismatch = amountMismatch + ",Withhold"; } else { amountMismatch = "Withhold"; } }

                            if (amountMismatch != "")
                            {
                                DialogResult _dlgRst = DialogResult.None;

                                #region " Build Warning Message Text "

                                string _warningMsg = "";

                                _warningMsg =
                                " Adjustment amounts for Charge " + (rIndex - 1) + " ('" + _cptCode + "') does not Balance." + Environment.NewLine + " " + Environment.NewLine + "" +
                                " Amounts mismatched for :" + amountMismatch + "" + Environment.NewLine + " " + Environment.NewLine + "" +
                                " Please correct adjustment amounts.";


                                #endregion " Build Warning Message Text "

                                _dlgRst = MessageBox.Show(_warningMsg, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                HighlightReasonCodes(rIndex, amountMismatch);
                                //c1SinglePayment.Focus();
                                //c1SinglePayment.Select(rIndex, COL_PAYMENT, true);
                                return false;
                            }
                            //else
                            //{
                            //    if ((_totalCharges - _currentPaymentAmt) != (_currentOtherAdjustmentAmt))
                            //    {
                            //        DialogResult _dlgRst = DialogResult.None;

                            //        #region " Build Warning Message Text "

                            //        string _warningMsg = "";
                            //        _warningMsg =
                            //        " Warning:  Remittance for Charge " + (rIndex - 1) + " ('" + _cptCode + "') does not Balance." + Environment.NewLine + " " + Environment.NewLine + "" +
                            //        " This can cause secondary payer billing rejections. " + Environment.NewLine + " " + Environment.NewLine + "" +
                            //        " Remittances must add up according to this formula: " + Environment.NewLine + "" +
                            //        " Billed Amount – Insurance Payment must equal sum of " + Environment.NewLine + "" +
                            //        " Write-off + Copay + Deduct + Co-ins + Withhold + Other Reasons." + Environment.NewLine + " " + Environment.NewLine + "" +
                            //        " Correct the remittance?";
                            //        #endregion " Build Warning Message Text "

                            //        _dlgRst = MessageBox.Show(_warningMsg, AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                            //        if (_dlgRst == DialogResult.No)
                            //        {
                            //            continue;
                            //        }
                            //        else
                            //        {
                            //            c1SinglePayment.Focus();
                            //            c1SinglePayment.Select(rIndex, COL_PAYMENT, true);
                            //            return false;
                            //        }
                            //    }
                            //}
                        }
                    }
                }

            }

            return true;
        }

        private bool CheckChargeLineBalances()
        {
            decimal _currentPaymentAmt = 0;
            decimal _currentWriteOffAmt = 0;
            decimal _cuurentCopayAmt = 0;
            decimal _currentDeductibleAmt = 0;
            decimal _currentCoInsAmt = 0;
            decimal _currentWithholdAmt = 0;
            decimal _currentOtherAdjustmentAmt = 0;
            decimal _totalCharges = 0;
            string _cptCode = "";

            if (PatientControl != null && PatientControl.PatientID > 0 && PatientControl.ClaimNumber > 0)
            {
                if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                {
                    for (int rIndex = 1; rIndex < c1SinglePayment.Rows.Count; rIndex++)
                    {
                        if (c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine && IsInsuranceParty(rIndex) == true)
                        {
                            _currentPaymentAmt = 0;
                            //_currentWriteOffAmt = 0;
                            //_cuurentCopayAmt = 0;
                            //_currentDeductibleAmt = 0;
                            //_currentCoInsAmt = 0;
                            //_currentWithholdAmt = 0;
                            _currentOtherAdjustmentAmt = 0;
                            _totalCharges = 0;
                            _cptCode = "";

                            if (c1SinglePayment.GetData(rIndex, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_PAYMENT)).Trim() != "")
                            { _currentPaymentAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_PAYMENT)); }
                            //if (c1SinglePayment.GetData(rIndex, COL_WRITEOFF) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_WRITEOFF)).Trim() != "")
                            //{ _currentWriteOffAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_WRITEOFF)); }
                            //if (c1SinglePayment.GetData(rIndex, COL_COPAY) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_COPAY)).Trim() != "")
                            //{ _cuurentCopayAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_COPAY)); }
                            //if (c1SinglePayment.GetData(rIndex, COL_DEDUCTIBLE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_DEDUCTIBLE)).Trim() != "")
                            //{ _currentDeductibleAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_DEDUCTIBLE)); }
                            //if (c1SinglePayment.GetData(rIndex, COL_COINSURANCE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_COINSURANCE)).Trim() != "")
                            //{ _currentCoInsAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_COINSURANCE)); }
                            //if (c1SinglePayment.GetData(rIndex, COL_WITHHOLD) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_WITHHOLD)).Trim() != "")
                            //{ _currentWithholdAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_WITHHOLD)); }
                            if (c1SinglePayment.GetData(rIndex, COL_TOTALCHARGE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_TOTALCHARGE)).Trim() != "")
                            { _totalCharges = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_TOTALCHARGE)); }
                            if (c1SinglePayment.GetData(rIndex, COL_CPT_CODE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CPT_CODE)).Trim() != "")
                            { _cptCode = Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CPT_CODE)); }

                            if (c1SinglePayment.GetData(rIndex, COL_REASON) != null)
                            {
                                _currentOtherAdjustmentAmt = 0;

                                #region " Get the other reason code amount "

                                C1FlexGrid c1OtherReason = ((C1.Win.C1FlexGrid.C1FlexGrid)c1SinglePayment.GetData(rIndex, COL_REASON));

                                if (c1OtherReason != null && c1OtherReason.Rows.Count > 1)
                                {
                                    for (int rCodeIndex = 1; rCodeIndex < c1OtherReason.Rows.Count; rCodeIndex++)
                                    {
                                        if (c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["Amount"].Index) != null
                                            && Convert.ToString(c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["Amount"].Index)).Trim() != "")
                                        {
                                            try
                                            {
                                                _currentOtherAdjustmentAmt += Convert.ToDecimal(c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["Amount"].Index));
                                            }
                                            catch (Exception)
                                            {
                                                continue;
                                            }

                                        }
                                    }

                                }

                                #endregion " Get the other reason code amount "

                            }

                            if ((_totalCharges - _currentPaymentAmt) != (_currentWriteOffAmt + _cuurentCopayAmt + _currentDeductibleAmt + _currentCoInsAmt + _currentWithholdAmt + _currentOtherAdjustmentAmt))
                            {
                                DialogResult _dlgRst = DialogResult.None;

                                #region " Build Warning Message Text "

                                string _warningMsg = "";

                                //_warningMsg =
                                //" Warning:  Remittance for Charge " + (rIndex - 1) + " ('" + _cptCode + "') does not Balance." + Environment.NewLine + " " + Environment.NewLine + "" +
                                //" This can cause secondary payer billing rejections. " + Environment.NewLine + " " + Environment.NewLine + "" +
                                //" Remittances must add up according to this formula: " + Environment.NewLine + "" +
                                //" Billed Amount – Insurance Payment must equal sum of " + Environment.NewLine + "" +
                                //" Write-off + Copay + Deduct + Co-ins + Withhold + Other Reasons." + Environment.NewLine + " " + Environment.NewLine + "" +
                                //" Correct the remittance?";

                                dynamic SettingName = "Restrict user to correct the remittance";
                                bool RestrictCorrection =GetSetting(SettingName);

                                //if (RestrictCorrection.Value.Value == "1" ? true : false)
                                if (RestrictCorrection)
                                {
                                    _warningMsg =
                                       " Warning:  Remittance for Charge " + (rIndex - 1) + " ('" + _cptCode + "') does not Balance." + Environment.NewLine + " " + Environment.NewLine + "" +
                                       " This can cause secondary payer billing rejections. " + Environment.NewLine + " " + Environment.NewLine + "" +
                                       " Remittances must add up according to this formula: " + Environment.NewLine + "" +
                                       " Billed Amount – Insurance Payment must equal sum of " + Environment.NewLine + "" +
                                       " Write-off + Copay + Deduct + Co-ins + Withhold + Other Reasons." + Environment.NewLine + " " + Environment.NewLine + "" +
                                       " Please correct the remittance.";
                                    _dlgRst = MessageBox.Show(_warningMsg, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                                }
                                else
                                {
                                    _warningMsg =
                                        " Warning:  Remittance for Charge " + (rIndex - 1) + " ('" + _cptCode + "') does not Balance." + Environment.NewLine + " " + Environment.NewLine + "" +
                                        " This can cause secondary payer billing rejections. " + Environment.NewLine + " " + Environment.NewLine + "" +
                                        " Remittances must add up according to this formula: " + Environment.NewLine + "" +
                                        " Billed Amount – Insurance Payment must equal sum of " + Environment.NewLine + "" +
                                        " Write-off + Copay + Deduct + Co-ins + Withhold + Other Reasons." + Environment.NewLine + " " + Environment.NewLine + "" +
                                        " Correct the remittance?";
                                    _dlgRst = MessageBox.Show(_warningMsg, AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                                }
                                #endregion " Build Warning Message Text "

                               // _dlgRst = MessageBox.Show(_warningMsg, AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                                if (_dlgRst == DialogResult.No)
                                {
                                    continue;
                                }
                                else
                                {
                                    c1SinglePayment.Focus();
                                    c1SinglePayment.Select(rIndex, COL_PAYMENT, true);
                                    return false;
                                }

                            }
                        }
                    }
                }

            }

            return true;
        }


        private bool IsInsuranceParty(int rIndex)
        {
            string _nextParty = Convert.ToString(c1SinglePayment.GetData(rIndex, COL_PARTY));
            string[] party = _nextParty.Split('-');

            if (party.Length > 1)
            {
                if (Convert.ToString(party[1]).ToUpper().Equals("SELF"))
                {
                    return false;

                }

            }

            return true;
        }

        #endregion

        #region " Save Validations "

        private bool SavePaymentValidation(bool isPaymentDone)
        {
            bool _isValidSave = true;
            bool _isValidInsCompany = true;
            string _party = "";
            string _partyCode = "";
            string _partyDesc = "";
            Int64 _partyContactId = 0;

            #region " Close Date "

            if (!IsValidCloseDate())
            {
                _isValidSave = false;
                return _isValidSave;
            }

            #endregion

            #region " Tray selection "

            if (!IsValidPaymentTray())
            {
                _isValidSave = false;
                return _isValidSave;
            }

            #endregion " Tray selection "

            #region "Complete Payments before Daily Close"

            if (!PerformDailyCloseValidation())
            {
                _isValidSave = false;
                return _isValidSave;
            }

            #endregion


            

            #region "Use Reserve Insurance Company Validation"

            for (int i = 0; i <= oCreditResDTL.EOBCreditDTL.Count - 1; i++)
            {
                if (oCreditResDTL.EOBCreditDTL[i].InsuranceID != 0)
                {

                    if (SelectedInsuranceCompanyID > 0 && SelectedInsuranceCompanyID != oCreditResDTL.EOBCreditDTL[i].InsuranceID)
                    {
                        _isValidInsCompany = false;
                    }
                }
                //else
                //{ _isValidInsCompany = true; }

            }

            if (!_isValidInsCompany)
            {
                DialogResult _dialogResult;
                _dialogResult = MessageBox.Show("Selected insurance reserve's company does not match the payment’s company.", "gloPM", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (_dialogResult == DialogResult.Cancel)
                {
                    _isValidSave = false;
                    return _isValidSave;
                }
            }



            #endregion"

            if (isPaymentDone)
            {
                #region " Payment mode selection "

                if (!IsValidPaymentMode())
                {
                    _isValidSave = false;
                    return _isValidSave;
                }

                #endregion

                #region " Check if Check amount is zero"

                if (isPaymentDone)
                {
                    if (CheckAmount == 0) //&& (!IsReserveUsed)
                    {
                        DialogResult _dlgRst = DialogResult.None;

                        string _mode = string.Empty;
                        if (SelectedPaymentMode == PaymentModeV2.Check)
                        { _mode = "Check"; }
                        else if (SelectedPaymentMode == PaymentModeV2.MoneyOrder)
                        { _mode = "MO"; }
                        else if (SelectedPaymentMode == PaymentModeV2.EFT)
                        { _mode = "EFT"; }
                        else if (SelectedPaymentMode == PaymentModeV2.Voucher)
                        { _mode = "Voucher"; }

                        if (txtCheckAmount.Text.Trim() == "")
                        {
                            if (_mode.Equals("Check"))
                            { _mode = _mode.ToLower(); }

                            MessageBox.Show("Enter the " + _mode + " amount.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCheckAmount.Select();
                            txtCheckAmount.Focus();
                            _isValidSave = false;
                            return _isValidSave;
                        }
                        else
                        {
                            _dlgRst = MessageBox.Show(_mode + " amount is zero. Continue with save ?", AppSettings.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                            if (_dlgRst == DialogResult.Cancel)
                            {
                                txtCheckAmount.Select();
                                txtCheckAmount.Focus();

                                _isValidSave = false;
                                return _isValidSave;
                            }
                        }
                    }
                }

                #endregion " Check if there is any data to save "

                #region " Same Check Warning "

                if (CheckForSameCheck() == false)
                {
                    _isValidSave = false;
                    return _isValidSave;
                }

                #endregion

                #region " Insurance Company & Insurance Plan "

                if (!IsInsuranceCompanySelected())
                {
                    btnSearchInsuranceCompany.Focus();
                    btnSearchInsuranceCompany.Select();
                    _isValidSave = false;
                    return _isValidSave;
                }

                #endregion

                if (IsPaymentAllocated || IsCorrectionDone)
                {
                    #region " Check for valid allowed amount "

                    if (CheckAllowedAmount() == false)
                    {
                        _isValidSave = false;
                        return _isValidSave;
                    }

                    #endregion " Check for allowed amount "

                    #region "Check line adjustment amount balance"
                    if (CheckChargeLineBalances_ReasonCodeAmount() == false)
                    {
                        _isValidSave = false;
                        return _isValidSave;
                    }
                    #endregion 
                    #region " Check line amount balance calculations "

                    if (CheckChargeLineBalances() == false)
                    {
                        _isValidSave = false;
                        return _isValidSave;
                    }

                    #endregion

                    #region " Check for Patient Insurance Plan selection "

                    CheckInsuranceCompany();

                    #endregion " Check line amount balance calculations "
                }
            }

            else if (IsNextActionUpdated == false)
            {
                #region " Payment mode selection "

                if (!IsValidPaymentMode())
                {
                    _isValidSave = false;
                    return _isValidSave;
                }

                #endregion
            }

            #region " Check for Action Selected "

            if (IsClaimLoaded)
            {
                if (CheckNextActionPartySelected() == false)
                {
                    _isValidSave = false;
                    return _isValidSave;
                }
            }

            #endregion

            #region Responsibility Transfer Warning Message if Claim is marked as Corrected/Replacement

            Boolean bResult = false;
            if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 0)
            {
                for (int rowCount = 1; rowCount <= c1SinglePayment.Rows.Count - 1; rowCount++)
                {
                    if ((c1SinglePayment.GetData(rowCount, COL_NEXT) != null) && (Convert.ToString(c1SinglePayment.GetData(rowCount, COL_NEXT)).Trim() != string.Empty))
                    {
                        if (c1SinglePayment.GetData(rowCount, COL_NEXT).ToString().ToUpper().StartsWith("B"))
                        {
                            if (ClaimDetails.IsClaimMarkedReplacement)
                            {
                                bResult = true;
                                break;
                            }

                        }
                    }
                }

            }
            if (bResult)
            {
                MessageBox.Show("Transferring to new party while claim is marked as Corrected/Replacement.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            #endregion

            #region "Claim Remittance Ref# warning"

            // if any charge on the claim is selected for rebill, on Save, give a warning if the claim does not have an original reference # entered.

            Boolean _bClaimRefNotEntered = false;
            if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 0)
            {
                for (int rowCount = 1; rowCount <= c1SinglePayment.Rows.Count - 1; rowCount++)
                {
                    if ((c1SinglePayment.GetData(rowCount, COL_NEXT) != null) && (Convert.ToString(c1SinglePayment.GetData(rowCount, COL_NEXT)).Trim() != string.Empty))
                    {
                        if (c1SinglePayment.GetData(rowCount, COL_NEXT).ToString().ToUpper().StartsWith("R"))
                        {
                            if (txtClaimRemittanceRef.Text.Trim() == string.Empty)
                            {
                                _bClaimRefNotEntered = true;
                                break;
                            }
                        }
                    }
                }
            }
            if (_bClaimRefNotEntered)
            {
                DialogResult _dialogResult;
                _dialogResult = MessageBox.Show("Rebilling normally requires a Claim Remittance Ref #, but none has been entered.\nEnter a Claim Remittance Ref # now?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if (_dialogResult == DialogResult.Yes)
                {
                    txtClaimRemittanceRef.Focus();
                    _isValidSave = false;
                    return _isValidSave;
                }
            }
            #endregion "Claim Remittance Ref# warning"

            #region "Medicare Claim Reference # Warning"

            if (isPaymentDone) // In case of only responsibility transfer without payment suppress the validation
            {
                string _nextAction = string.Empty;
                string _nextActionCode = string.Empty;

                Boolean _bMedicarePrimaryandClaimRefNotEntered = false;
                // Get Insurance parties for current loaded claim
                DataTable _dt = null;
                _dt = gloStripControl.PatientStripControl.GetInsuranceParties(ClaimDetails.TransactionMasterID, ClaimDetails.TransactionID);
                for (int i = 0; i <= c1SinglePayment.Rows.Count - 1; i++)
                {
                    if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                    {
                        if (c1SinglePayment.GetData(i, COL_NEXT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT)).Trim() != "")
                        {
                            if (c1SinglePayment.GetData(i, COL_PARTY) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PARTY)).Trim() != "")
                            {
                                //Get next action selected

                                _nextAction = Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT)).Trim();
                                _nextActionCode = _nextAction.Substring(0, _nextAction.IndexOf('-'));

                                // If next selected action is billed then check for selected insurance plan

                                if (_nextActionCode == "B")
                                {
                                    _party = Convert.ToString(c1SinglePayment.GetData(i, COL_PARTY)).Trim();
                                    _partyCode = _party.Substring(0, _party.IndexOf('-'));
                                    _partyDesc = _party.Substring(_party.IndexOf('-') + 1, (_party.Length - _party.IndexOf('-')) - 1);

                                    if (_dt != null && _dt.Rows.Count > 0)
                                    {
                                        for (int pIndex = 0; pIndex < _dt.Rows.Count; pIndex++)
                                        {
                                            if (Convert.ToString(_dt.Rows[pIndex]["nResponsibilityNo"]) == _partyCode)
                                            {
                                                // Get next insurance party contact id

                                                _partyContactId = Convert.ToInt64(_dt.Rows[pIndex]["nContactID"]);
                                                break;
                                            }
                                        }
                                    }
                                    if (_dt != null) { _dt.Dispose(); }

                                    // Check whether the selected insurance is Medicare
                                    if (gloInsurancePaymentV2.IsMedicarePrimary(ClaimDetails.TransactionMasterID, ClaimDetails.TransactionID, ContactInsuranceID))
                                    {
                                        // Check whether the bill next insurance has Include Medicare reference # setting on or off
                                        if (gloInsurancePaymentV2.IsIncludeMedicareClaimRef(_partyContactId))
                                        {
                                            // If the bill next insurance has Include Medicare reference # setting on and no claim reference no is entered then validation message should appear
                                            if (txtClaimRemittanceRef.Text.Trim() == string.Empty)
                                            {
                                                _bMedicarePrimaryandClaimRefNotEntered = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }

                }

                if (_bMedicarePrimaryandClaimRefNotEntered)
                {
                    DialogResult _dialogResult;
                    _dialogResult = MessageBox.Show(_partyDesc + " requires Medicare’s claim reference # But none was entered.\nStop to enter the claim reference #?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                    if (_dialogResult == DialogResult.Yes)
                    {
                        txtClaimRemittanceRef.Focus();
                        _isValidSave = false;
                        return _isValidSave;
                    }
                }
            }

            #endregion "Medicare Claim Reference # Warning"

            return _isValidSave;
        }

        private bool CheckAllowedAmount(int rowIndex)
        {
            bool _isValidContext = true;

            decimal _allowed = 0;
            decimal _charges = 0;
            string _allowed_amt = string.Empty;
            string _cptCode = string.Empty;
            _IsValidEntered = true;

            _allowed_amt = Convert.ToString(c1SinglePayment.GetData(rowIndex, COL_ALLOWED));
            _cptCode = Convert.ToString(c1SinglePayment.GetData(rowIndex, COL_CPT_CODE));

            if (_allowed_amt != "")
            {
                _allowed = Convert.ToDecimal(_allowed_amt);
                _charges = Convert.ToDecimal(c1SinglePayment.GetData(rowIndex, COL_TOTALCHARGE));

                // Bug# 52650 - Removing the If Condition which checks whether the current resposbility is Primary or not and then show the following message
                // Now we show this message whether current resposbility is on Primary,Secondary or tertiary.
                if (_allowed > _charges)
                {
                    _isValidContext = false;
                    MessageBox.Show("Allowed amount for charge " + (rowIndex - 1) + " ('" + _cptCode + "')" + " should not be greater than charge amount. ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                _isValidContext = false;
                MessageBox.Show("Allowed amount for charge " + (rowIndex - 1) + " ('" + _cptCode + "')" + " cannot be blank. ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return _isValidContext;
        }

        private bool CheckAllowedAmount()
        {
            bool _isValidContext = true;

            if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
            {
                for (int rIndex = 1; rIndex < c1SinglePayment.Rows.Count; rIndex++)
                {
                    if (c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                    {
                        _isValidContext = CheckAllowedAmount(rIndex);
                        if (!_isValidContext)
                        {
                            c1SinglePayment.Focus();
                            c1SinglePayment.Select(rIndex, COL_ALLOWED);
                            break;
                        }
                    }
                }
            }
            return _isValidContext;
        }

        private bool CheckNextActionPartySelected()
        {
            bool _IsActionSelectedForAllCPTs = true;

            if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
            {
                bool _IsSplitted = false;
                string _Next = string.Empty;
                string _Party = string.Empty;
                string _CPT = string.Empty;

                int _line = 0;

                for (int i = 1; i < c1SinglePayment.Rows.Count; i++)
                {
                    if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                    {
                        _line += 1;

                        _Next = Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT));
                        _Party = Convert.ToString(c1SinglePayment.GetData(i, COL_PARTY));
                        _IsSplitted = Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISSPLITTED));
                        _CPT = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));

                        if (!_IsSplitted) // Skip the validation check for Splitted Charge Line
                        {
                            if (_Next.Equals(string.Empty)) // Check if Next action is selected or not 
                            {
                                _IsActionSelectedForAllCPTs = false;
                                MessageBox.Show("Next action for charge " + (_line) + " ('" + _CPT + "')" + " is not selected. ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                c1SinglePayment.Select(i, COL_NEXT);
                                break;
                            }

                            // Following validation is added on 02-aug-2010
                            if (_Party.Equals(string.Empty)) // Check if party is selected or not 
                            {
                                _IsActionSelectedForAllCPTs = false;
                                MessageBox.Show("Party for charge " + (_line) + " ('" + _CPT + "')" + " is not selected. ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                c1SinglePayment.Select(i, COL_NEXT);
                                break;
                            }
                        }
                    }
                }
              
            }
            else
            {
                _IsActionSelectedForAllCPTs = false;
            }
            return _IsActionSelectedForAllCPTs;
        }

        private bool IsInsurancePlanSelected()
        {
            if (ContactInsuranceID.Equals(0)) 
            {
                MessageBox.Show("Please select Insurance Plan.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private bool IsInsuranceCompanySelected()
        {
            if (SelectedInsuranceCompanyID.Equals(0)) 
            {
                MessageBox.Show("Please select Insurance Company.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private bool IsValidCloseDate()
        {
            int _addDays = 0;
            _addDays = gloAccountsV2.gloBillingCommonV2.GetFutureCloseDayDateSettings();
            if (mskCloseDate.MaskCompleted == false)
            {
                MessageBox.Show("Please enter the close date", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskCloseDate.Focus();
                mskCloseDate.Select();
                return false;
            }
            
            CancelEventArgs e = new CancelEventArgs();
            ValidateDate(mskCloseDate, e);
            if (e.Cancel)
            { return false; }

            #region " check for day closed "

            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(AppSettings.ConnectionStringPM, string.Empty);
            try
            {
                if (ogloBilling.IsDayClosed(Convert.ToDateTime(mskCloseDate.Text)) == true)
                {
                    if (IsPendingCheckLoaded)
                    {
                        if (IsPaymentAllocated == true)
                        {
                            MessageBox.Show("Selected date is already closed. Please select a different close date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mskCloseDate.Select(); mskCloseDate.Focus();
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selected date is already closed. Please select a different close date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCloseDate.Select(); mskCloseDate.Focus();
                        return false;
                    }
                }
            }
            catch
            {
            }
            finally
            {
                ogloBilling.Dispose();
            }

            #endregion " check for day closed  "

            #region " check for future date "

            if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(_addDays))
            {
                MessageBox.Show("Close date " + mskCloseDate.Text.Trim() + " is too far in the future. ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning); mskCloseDate.Focus();
                mskCloseDate.Select();
                return false;
            }
            else
            {
                if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date)
                {
                    DialogResult _dlgCloseDate = DialogResult.None;
                    _dlgCloseDate = MessageBox.Show("Close date " + mskCloseDate.Text.Trim() + " is in future. Are you sure you want to continue with save?", AppSettings.MessageBoxCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (_dlgCloseDate == DialogResult.Cancel)
                    {
                        mskCloseDate.Focus();
                        mskCloseDate.Select();
                        return false;
                    }
                }
            }

            #endregion

            #region " check if selected close date is greater than or equal to charge close date "

            if (ClaimDetails.BillingTransactionDate != 0)
            {
                if (ClaimDetails.BillingTransactionDate > gloDate.DateAsNumber(mskCloseDate.Text.Trim()))
                {
                    MessageBox.Show("Cannot save payment – payment close date precedes charge’s close date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning); mskCloseDate.Focus();
                    mskCloseDate.Select();
                    return false;
                }
            }
        
            #endregion

            return true;
        }
        private Boolean IsLessThenPreTransDate(Int64 nTransactionID, string strDate, ref Int64 nNextActionCloseDate)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(AppSettings.ConnectionStringPM);
            string strQuery = "";
            DataTable dtCloseDate = null;
            Boolean bReturn = true;
            try
            {
                oDB.Connect(false);
                strQuery = "SELECT ISNULL(MAX(nclosedate),0) as nCloseDate FROM BL_EOB_NextAction_HST WHERE nTrackMstTrnID = " + nTransactionID;
                oDB.Retrive_Query(strQuery, out dtCloseDate);

                if (dtCloseDate != null && dtCloseDate.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtCloseDate.Rows[0][0]) > 0)
                    {
                        if (gloDateMaster.gloDate.DateAsNumber(strDate) < Convert.ToInt32(dtCloseDate.Rows[0]["nCloseDate"]))
                        {
                            nNextActionCloseDate = Convert.ToInt64(dtCloseDate.Rows[0][0]);
                            bReturn = false;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return bReturn;
        }
        private bool IsValidPaymentTray()
        {
            if (SelectedPaymentTrayID == 0)
            {
                MessageBox.Show("Please select the payment tray.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnTraySelection.Select();
                btnTraySelection.Focus();
                return false;
            }
            else if (gloInsurancePaymentV2.IsPaymentTrayActive(SelectedPaymentTrayID) == false)
            {
                MessageBox.Show("The payment tray selected is inactive. Please select another tray", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnTraySelection.Select();
                btnTraySelection.Focus();
                return false;
            }
            return true;
        }

        private bool IsValidPaymentMode()
        {
            if (SelectedPaymentMode == PaymentModeV2.None)
            {
                MessageBox.Show("Please select the payment mode.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbPayMode.Select();
                cmbPayMode.Focus();
                return false;
            }
            else if (SelectedPaymentMode == PaymentModeV2.CreditCard)
            {
                if (mskCheckDate.MaskCompleted == false)
                {
                    MessageBox.Show("Please enter the " + SelectedPaymentMode.ToString() + " date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCheckDate.Select();
                    mskCheckDate.Focus();
                    return false;
                }

                if (cmbCardType == null || cmbCardType.Items.Count <= 0 || cmbCardType.Text.Trim() == "")
                {
                    MessageBox.Show("Please select the card type.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbCardType.Select();
                    cmbCardType.Focus();
                    return false;
                }

                mskCreditExpiryDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                if (mskCreditExpiryDate.Text != "")
                {
                    if (mskCreditExpiryDate.MaskFull == false)
                    {
                        MessageBox.Show("Please enter valid " + SelectedPaymentMode.ToString() + " expiration date (MM/yy).", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCreditExpiryDate.Select();
                        mskCreditExpiryDate.Focus();
                        return false;
                   }
                }
            }
            // If payment mode is check & EFT, number & date is mandatory.
            // Note : for MO both are optional
            else if ((SelectedPaymentMode == PaymentModeV2.Check) || (SelectedPaymentMode == PaymentModeV2.EFT) || (SelectedPaymentMode == PaymentModeV2.Voucher) || (SelectedPaymentMode == PaymentModeV2.MoneyOrder))
            {
                string _mode = string.Empty;

                if (SelectedPaymentMode == PaymentModeV2.Check)
                { _mode = SelectedPaymentMode.ToString().ToLower(); }
                else
                { _mode = SelectedPaymentMode.ToString().ToUpper(); }

                if (txtCheckNumber.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter the " + _mode + " number.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCheckNumber.Select();
                    txtCheckNumber.Focus();
                    return false;
                }
                if (mskCheckDate.MaskCompleted == false)
                {
                    MessageBox.Show("Please enter the " + _mode + " date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCheckDate.Select();
                    mskCheckDate.Focus();
                    return false;
                }
            }
            return true;
        }

        private bool IsValidForReserveRemaining()
        {
            try
            {
                if (TotalFundsRemaining == 0) // Reserve amount should not be zero
                {
                    MessageBox.Show("Nothing to Reserve, Please enter some amount.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else if (TotalFundsRemaining < 0)
                {
                    MessageBox.Show("Cannot reserve negative amount.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                //else if (!IsInsuranceCompanySelected()) // Insurance company must be selected.
                //{
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return true;
        }

        private bool IsPaymentMade()
        {
            try
            {
                // 1) If amount added to Insurance Reserve (reserve remaining)
                if (IsReserveAdded)
                {
                    return true;
                }
                // 2) If amount used from reserve and allocated
                if ((IsReserveUsed && IsPaymentAllocated) || IsReserveUsed )
                {
                    return true;
                }
                // 3) If amount is allocated but not taken from reserve 
                if ((!IsReserveUsed) && (IsPaymentAllocated))
                {
                    return true;
                }

                // 4) 
                if (txtCheckAmount.Text.Trim() != "")
                {
                    if (SelectedPaymentMode == PaymentModeV2.Check && txtCheckNumber.Text.Trim() != "" && _EOBPaymentID == 0)
                    { return true; }
                    else if (SelectedPaymentMode == PaymentModeV2.EFT && txtCheckNumber.Text.Trim() != "" && _EOBPaymentID == 0)
                    { return true; }
                    else if (SelectedPaymentMode == PaymentModeV2.Voucher && txtCheckNumber.Text.Trim() != "" && _EOBPaymentID == 0)
                    { return true; }
                    else if (SelectedPaymentMode == PaymentModeV2.MoneyOrder && txtCheckNumber.Text.Trim() != "" && _EOBPaymentID == 0)
                    { return true; }
                    else if (SelectedPaymentMode == PaymentModeV2.CreditCard && txtCheckNumber.Text.Trim() != "" && _EOBPaymentID == 0)
                    { return true; }
                    else
                    { return false; }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : " + ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return false;
        }

        private bool IsCheckUpdating()
        {
            bool isCheckUpdating = false;
            string _checkNumber = Convert.ToString(txtCheckNumber.Text);
            int _paymentMode = SelectedPaymentMode.GetHashCode();
            DateTime _checkDate = DateTime.Now;

            if (mskCheckDate.MaskCompleted)
            {
                mskCheckDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                _checkDate = Convert.ToDateTime(mskCheckDate.Text);
            }
            if (EOBPaymentID > 0)
                isCheckUpdating = gloInsurancePaymentV2.IsCheckUpdating(EOBPaymentID, _checkNumber, _checkDate, _paymentMode, CheckAmount);

            return isCheckUpdating;
        }

        private bool PerformDailyCloseValidation()
        {
            bool _isAppyRules = false;
            bool _isvalid = false;
            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            try
            {
                _isAppyRules = gloInsurancePaymentV2.GetDialyCloseValidationSetting(gloGlobal.gloPMGlobal.ClinicID);
          
                if (_NewOpenDate != _OriginalCloseDate && _NewOpenDate.ToString() != mskCloseDate.Text.ToString())
                {
                    DialogResult _Res = MessageBox.Show("Allocations have been made to " + _NewOpenDate + ". Do you want to use that Close Date?", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information);  
                    if (_Res == DialogResult.Yes )
                            mskCloseDate.Text = _NewOpenDate.ToString();
                    _isvalid = true;
                }
                else if (_PaymentCloseDate.ToString() != "" && _PaymentCloseDate.ToString() != mskCloseDate.Text.ToString())
                {
                    MessageBox.Show("Payment’s Close Date is open. No other close date is allowed.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskCloseDate.Text = _PaymentCloseDate.ToString();
                    _isvalid = false;
                }
                else if (txtCheckAmount.Enabled == false && _isOriginalPayment == true && _isAppyRules == true)
                {
                    decimal _remaining = 0;
                    try
                    {
                     _remaining = Convert.ToDecimal(txtCheckRemaining.Text);
                    }
                    catch 
                    {
                        _remaining =0;
                    }

                    if (_remaining == 0)
                    {
                        _isvalid = true;
                    }
                    else if (_remaining < 0)
                    {
                        MessageBox.Show("Payment’s Close Date is closed. Remaining must be $0.00.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _isvalid = false;
                    }
                    else if (_remaining > 0)
                    {
                        MessageBox.Show("Payment’s Close Date is closed. Remaining must be $0.00.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _isvalid = false;
                    }
                    else if (lblTakeBackAmt.Text != "" && Convert.ToDecimal(lblTakeBackAmt.Text) > 0)
                    {
                        SendAmountToReserve(Convert.ToDecimal(lblTakeBackAmt.Text));
                        //try
                        //{
                        // _remaining = Convert.ToDecimal(txtCheckRemaining.Text);
                        //}
                        //catch 
                        //{
                        //    _remaining =0;
                        //}

                        if (IsReserveAdded == true && AmountAddedToReserve >= Convert.ToDecimal(lblTakeBackAmt.Text))
                            _isvalid = true;
                        else
                        {
                            MessageBox.Show("Payment’s Close Date is closed. Remaining must be $0.00.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _isvalid = false;
                        }
                    }
                    else
                        _isvalid = true;
                }      
                
                else
                    _isvalid = true;

            }
            catch (Exception ex)
            {
                _isvalid = false; 
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

            return _isvalid;
        }

        private DialogResult SaveChangesAlert()
        {
            DialogResult _result = DialogResult.No;
            try
            {
                _result = MessageBox.Show("Do you want to save changes?", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (_result == DialogResult.Yes)
                {
                    if (txtCheckAmount.Text.Trim() == "" && IsPaymentAllocated)
                    {
                        preSave();
                    }
                    else
                    {
                        PerformSavePayment();
                    }
                }
            }
            catch (Exception ex)           
            {              
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return _result;
        }

        #endregion

        #endregion

        #region " C1 Grid Design Methods "

        private void DesignPaymentGrid(C1FlexGrid c1Payment)
        {
            
            try
            {               
                c1Payment.Redraw = false;
                c1Payment.ScrollBars = ScrollBars.None;
                c1Payment.AllowSorting = AllowSortingEnum.None;
                if (c1Payment.Name != c1SinglePayment.Name) { c1Payment.Clear(); }                
                c1Payment.Cols.Count = COL_COUNT;
                c1Payment.Rows.Count = 1;
                c1Payment.Rows.Fixed = 1;
                c1Payment.Cols.Fixed = 0;

                if (c1Payment.Name == c1SinglePaymentTotal.Name || c1Payment.Name == c1MultiplePaymentTotal.Name)
                {
                    c1Payment.Rows.Fixed = 0;
                    c1Payment.ScrollBars = ScrollBars.None;
                }
                else
                {
                    #region " Set Headers "

                    c1Payment.SetData(0, COL_GENERAL, "General");
                    c1Payment.SetData(0, COL_PATIENTID, "PatientID");
                    c1Payment.SetData(0, COL_PATIENTNAME, "Patient");
                    c1Payment.SetData(0, COL_CLAIMDISPNO, "Claim #");
                    c1Payment.SetData(0, COL_CLAIMNO, "Claim No");
                    c1Payment.SetData(0, COL_BILLING_TRANSACTON_ID, "Transacton ID");
                    c1Payment.SetData(0, COL_BILLING_TRANSACTON_DETAILID, "Transacton Detail ID");
                    c1Payment.SetData(0, COL_BILLING_TRANSACTON_LINENO, "Transacton Line No");
                    c1Payment.SetData(0, COL_PAYMENT_NO, "Payment No");
                    c1Payment.SetData(0, COL_PAY_EOBPAYMENTID, "EOBPaymentID");
                    c1Payment.SetData(0, COL_PAY_EOBID, "EOBID");
                    c1Payment.SetData(0, COL_PAY_EOBDTLID, "EOBDTLID");
                    c1Payment.SetData(0, COL_PAY_EOBPAYMENTDTLID, "EOBPAYDTLID");
                    c1Payment.SetData(0, COL_DOS_FROM, "DOS");
                    c1Payment.SetData(0, COL_DOS_TO, "DOS To");

                    c1Payment.SetData(0, COL_CPT_CODE, "CPT");
                    c1Payment.SetData(0, COL_CPT_DESCRIPTON, "CPT Desc");

                    c1Payment.SetData(0, COL_CROSSWALK_CPT_CODE, "CPT");
                    c1Payment.SetData(0, COL_CROSSWALK_CPT_DESC, "CPT Desc");

                    c1Payment.SetData(0, COL_CHARGE, "Charge");
                    c1Payment.SetData(0, COL_UNIT, "Unit");
                    c1Payment.SetData(0, COL_TOTALCHARGE, "Charge");
                    if (c1Payment.Name == c1SinglePaymentCorrTB.Name)
                    {
                        c1Payment.SetData(0, COL_ALLOWED, "+/- Allowed");
                        c1Payment.SetData(0, COL_PAYMENT, "+/- Payment");
                        c1Payment.SetData(0, COL_WRITEOFF, "+/- W/O");
                        c1Payment.SetData(0, COL_COPAY, "+/- Copay");
                        c1Payment.SetData(0, COL_DEDUCTIBLE, "+/- Deduct");
                        c1Payment.SetData(0, COL_COINSURANCE, "+/- Co-ins");
                        c1Payment.SetData(0, COL_WITHHOLD, "+/- Withhold");
                    }
                    else
                    {
                        c1Payment.SetData(0, COL_ALLOWED, "Allowed");
                        c1Payment.SetData(0, COL_PAYMENT, "Payment");
                        c1Payment.SetData(0, COL_WRITEOFF, "W/O");
                        c1Payment.SetData(0, COL_COPAY, "Copay");
                        c1Payment.SetData(0, COL_DEDUCTIBLE, "Deduct");
                        c1Payment.SetData(0, COL_COINSURANCE, "Co-ins");
                        c1Payment.SetData(0, COL_WITHHOLD, "Withhold");
                    }
                                   
                    c1Payment.SetData(0, COL_PREVPAID, "Prev Paid");

                    c1Payment.SetData(0, COL_LAST_ALLOWED, "Last Allowed");
                    c1Payment.SetData(0, COL_LAST_PAYMENT, "Last Payment");
                    c1Payment.SetData(0, COL_LAST_WRITEOFF, "Last W/O");
                    c1Payment.SetData(0, COL_LAST_COPAY, "Last Copay");
                    c1Payment.SetData(0, COL_LAST_DEDUCTIBLE, "Last Ded");
                    c1Payment.SetData(0, COL_LAST_COINSURANCE, "Last Co-ins");
                    c1Payment.SetData(0, COL_LAST_WITHHOLD, "Last Withhold");
                    c1Payment.SetData(0, COL_ISCORRECTION, "Is Correction");
                                     
                    c1Payment.SetData(0, COL_BALANCE, "New Balance");
                    c1Payment.SetData(0, COL_NEXT, "Next");
                    c1Payment.SetData(0, COL_PARTY, "Party");
                    if (c1Payment.Name == c1MultiplePayment.Name)
                    {
                        c1Payment.SetData(0, COL_REASON, "Other"); //Reason Codes
                    }
                    else
                    {
                        c1Payment.SetData(0, COL_REASON, "R"); //Reason Codes
                    }

                    c1Payment.SetData(0, COL_LINE_STATEMENTNOTE, "StatementNote");
                    c1Payment.SetData(0, COL_LINE_STATEMENTNOTEONPRINT, "StatementNoteOnPrint");
                    c1Payment.SetData(0, COL_LINE_INTERNALNOTE, "InternalNote");
                    c1Payment.SetData(0, COL_LINE_INTERNALNOTEONPRINT, "InternalNoteOnPrint");

                    c1Payment.SetData(0, COL_LINE_DB_BALANCE, "LineDbBalAmt");

                    c1Payment.SetData(0, COL_SUBCLAIMNO, "SubClaimNo");
                    c1Payment.SetData(0, COL_TRACK_BILLING_TRANSACTON_ID, "TrackTrnID");
                    c1Payment.SetData(0, COL_TRACK_BILLING_TRANSACTON_DETAILID, "TrackTrnDtlID");
                    c1Payment.SetData(0, COL_TRACK_BILLING_TRANSACTON_LINENO, "TrackLineID");

                    c1Payment.SetData(0, COL_ISSPLITTED, "IsSplitted");

                    c1Payment.SetData(0, COL_PAYMENTTYPE, "PayType");
                    c1Payment.SetData(0, COL_PAYMENTSUBTYPE, "PaySubType");
                    c1Payment.SetData(0, COL_MODIFIER, "Mod");
                    #endregion
                }

                #region " Show/Hide "

                c1Payment.Cols[COL_GENERAL].Visible = false;
                c1Payment.Cols[COL_PATIENTID].Visible = false;
                c1Payment.Cols[COL_PATIENTNAME].Visible = false;
                
                if (c1Payment.Name == c1MultiplePayment.Name || c1Payment.Name == c1MultiplePaymentTotal.Name)
                {
                    c1Payment.Cols[COL_CLAIMDISPNO].Visible = true;
                    c1Payment.Cols[COL_UNIT].Visible = false;
                }
                else if (c1Payment.Name == c1SinglePayment.Name || c1Payment.Name == c1SinglePaymentTotal.Name || c1Payment.Name == c1SinglePaymentCorrTB.Name)
                { 
                    c1Payment.Cols[COL_CLAIMDISPNO].Visible = false;
                    c1Payment.Cols[COL_LASTEOBPAYMENTID].Visible = false;
                    c1Payment.Cols[COL_PatientPaidAmount].Visible = false;
                    if (_IsShowChargeUnit == false)
                    {
                        c1Payment.Cols[COL_UNIT].Visible = false;
                    }
                    else
                    {
                        c1Payment.Cols[COL_UNIT].Visible = true;
                    }
                    
                }
                

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
                c1Payment.Cols[COL_DOS_FROM].Visible = true;
                c1Payment.Cols[COL_DOS_TO].Visible = false;
                
                c1Payment.Cols[COL_CPT_CODE].Visible = true;
                c1Payment.Cols[COL_CROSSWALK_CPT_CODE].Visible = false;

                c1Payment.Cols[COL_CPT_DESCRIPTON].Visible = false;
                c1Payment.Cols[COL_CROSSWALK_CPT_DESC].Visible = false;

                c1Payment.Cols[COL_CHARGE].Visible = false;
                
                c1Payment.Cols[COL_TOTALCHARGE].Visible = true;
                c1Payment.Cols[COL_ALLOWED].Visible = true;
                c1Payment.Cols[COL_PAYMENT].Visible = true;
                c1Payment.Cols[COL_WRITEOFF].Visible = true;
                c1Payment.Cols[COL_COPAY].Visible = true;
                c1Payment.Cols[COL_DEDUCTIBLE].Visible = true;
                c1Payment.Cols[COL_COINSURANCE].Visible = true;
                c1Payment.Cols[COL_WITHHOLD].Visible = true;
                c1Payment.Cols[COL_PREVPAID].Visible = true;
                c1Payment.Cols[COL_MODIFIER].Visible = true;

                bool _show = false;
                c1Payment.Cols[COL_LAST_ALLOWED].Visible = _show;
                c1Payment.Cols[COL_LAST_PAYMENT].Visible = _show;
                c1Payment.Cols[COL_LAST_WRITEOFF].Visible = _show;
                c1Payment.Cols[COL_LAST_COPAY].Visible = _show;
                c1Payment.Cols[COL_LAST_DEDUCTIBLE].Visible = _show;
                c1Payment.Cols[COL_LAST_COINSURANCE].Visible = _show;
                c1Payment.Cols[COL_LAST_WITHHOLD].Visible = _show;
                c1Payment.Cols[COL_ISCORRECTION].Visible = _show;
                c1Payment.Cols[COL_PAYMENTTYPE].Visible = _show;
                c1Payment.Cols[COL_PAYMENTSUBTYPE].Visible = _show;

                c1Payment.Cols[COL_BALANCE].Visible = true;
                c1Payment.Cols[COL_NEXT].Visible = true;
                c1Payment.Cols[COL_PARTY].Visible = true;
                c1Payment.Cols[COL_REASON].Visible = true;
                c1Payment.Cols[COL_SERVICELINE_TYPE].Visible = false;
                c1Payment.Cols[COL_ISOPENFORMODIFY].Visible = false;
                c1Payment.Cols[COL_PAY_CLINICID].Visible = false;
                c1Payment.Cols[COL_PAY_LINESTATUS].Visible = false;
                c1Payment.Cols[COL_CELLRANGE_R1].Visible = false;
                c1Payment.Cols[COL_CELLRANGE_R2].Visible = false;

                c1Payment.Cols[COL_LINE_STATEMENTNOTE].Visible = false;
                c1Payment.Cols[COL_LINE_STATEMENTNOTEONPRINT].Visible = false;
                c1Payment.Cols[COL_LINE_INTERNALNOTE].Visible = false;
                c1Payment.Cols[COL_LINE_INTERNALNOTEONPRINT].Visible = false;
                c1Payment.Cols[COL_LINE_DB_BALANCE].Visible = false;

                c1Payment.Cols[COL_SUBCLAIMNO].Visible = false;
                c1Payment.Cols[COL_TRACK_BILLING_TRANSACTON_ID].Visible = false;
                c1Payment.Cols[COL_TRACK_BILLING_TRANSACTON_DETAILID].Visible = false;
                c1Payment.Cols[COL_TRACK_BILLING_TRANSACTON_LINENO].Visible = false;
                c1Payment.Cols[COL_LASTEOBPAYMENTID].Visible = false;
                c1Payment.Cols[COL_PatientPaidAmount].Visible = false;
                c1Payment.Cols[COL_ISSPLITTED].Visible = false;
         
                #endregion

                #region " Width "

                bool _designWidth = false;

                if (_designWidth == false)
                {
                    c1Payment.Cols[COL_GENERAL].Width = 0;
                    c1Payment.Cols[COL_PATIENTID].Width = 0;
                    c1Payment.Cols[COL_PATIENTNAME].Width = 0;
                    c1Payment.Cols[COL_CLAIMDISPNO].Width = 70;
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
                    c1Payment.Cols[COL_DOS_FROM].Width = 75;
                    c1Payment.Cols[COL_DOS_TO].Width = 0;

                    c1Payment.Cols[COL_CPT_CODE].Width = 50;
                    c1Payment.Cols[COL_CPT_DESCRIPTON].Width = 0;                    
                    c1Payment.Cols[COL_CROSSWALK_CPT_CODE].Width = 50;
                    c1Payment.Cols[COL_CROSSWALK_CPT_DESC].Width = 0;

                    c1Payment.Cols[COL_CHARGE].Width = 0;                    
                    c1Payment.Cols[COL_TOTALCHARGE].Width = 100;
                    c1Payment.Cols[COL_ALLOWED].Width = 100;
                    c1Payment.Cols[COL_PAYMENT].Width = 90;
                    c1Payment.Cols[COL_WRITEOFF].Width = 80;
                    c1Payment.Cols[COL_COPAY].Width = 80;
                    c1Payment.Cols[COL_DEDUCTIBLE].Width = 80;
                    c1Payment.Cols[COL_COINSURANCE].Width = 80;
                    c1Payment.Cols[COL_WITHHOLD].Width = 80;
                    c1Payment.Cols[COL_PREVPAID].Width = 80;
                    c1Payment.Cols[COL_BALANCE].Width = 90;

                    c1Payment.Cols[COL_LAST_ALLOWED].Width = 80;
                    c1Payment.Cols[COL_LAST_PAYMENT].Width = 80;
                    c1Payment.Cols[COL_LAST_WRITEOFF].Width = 80;
                    c1Payment.Cols[COL_LAST_COPAY].Width = 80;
                    c1Payment.Cols[COL_LAST_DEDUCTIBLE].Width = 80;
                    c1Payment.Cols[COL_LAST_COINSURANCE].Width = 80;
                    c1Payment.Cols[COL_LAST_WITHHOLD].Width = 80;
                    c1Payment.Cols[COL_ISCORRECTION].Width = 80;
                    c1Payment.Cols[COL_PAYMENTTYPE].Width = 0;
                    c1Payment.Cols[COL_PAYMENTSUBTYPE].Width = 0;

                    c1Payment.Cols[COL_SERVICELINE_TYPE].Width = 0;
                    c1Payment.Cols[COL_ISOPENFORMODIFY].Width = 0;
                    c1Payment.Cols[COL_PAY_CLINICID].Width = 0;
                    c1Payment.Cols[COL_PAY_LINESTATUS].Width = 0;
                    c1Payment.Cols[COL_CELLRANGE_R1].Width = 0;
                    c1Payment.Cols[COL_CELLRANGE_R1].Width = 0;

                    c1Payment.Cols[COL_LINE_STATEMENTNOTE].Width = 0;
                    c1Payment.Cols[COL_LINE_STATEMENTNOTEONPRINT].Width = 0;
                    c1Payment.Cols[COL_LINE_INTERNALNOTE].Width = 0;
                    c1Payment.Cols[COL_LINE_INTERNALNOTEONPRINT].Width = 0;

                    c1Payment.Cols[COL_LINE_DB_BALANCE].Width = 0;

                    c1Payment.Cols[COL_SUBCLAIMNO].Width = 0;
                    c1Payment.Cols[COL_TRACK_BILLING_TRANSACTON_ID].Width = 0;
                    c1Payment.Cols[COL_TRACK_BILLING_TRANSACTON_DETAILID].Width = 0;
                    c1Payment.Cols[COL_TRACK_BILLING_TRANSACTON_LINENO].Width = 0;

                    c1Payment.Cols[COL_ISSPLITTED].Width = 0;
                    if (_IsShowChargeUnit == true && (c1Payment.Name == c1SinglePayment.Name || c1Payment.Name == c1SinglePaymentTotal.Name || c1Payment.Name == c1SinglePaymentCorrTB.Name))
                    {
                        c1Payment.Cols[COL_NEXT].Width = 82;
                        c1Payment.Cols[COL_PARTY].Width = 82;
                        c1Payment.Cols[COL_UNIT].Width =37;
                        c1Payment.Cols[COL_MODIFIER].Width = 70;
                        c1Payment.Cols[COL_REASON].Width = 18;//19;

                        
                       
                        c1Payment.Cols[COL_WRITEOFF].Width = 77;
                        c1Payment.Cols[COL_COPAY].Width = 77;
                        c1Payment.Cols[COL_DEDUCTIBLE].Width = 77;
                        c1Payment.Cols[COL_COINSURANCE].Width = 77;
                        c1Payment.Cols[COL_WITHHOLD].Width = 77;
                        c1Payment.Cols[COL_PREVPAID].Width = 77;
                    }
                    else
                    {
                         c1Payment.Cols[COL_NEXT].Width = 92;
                        c1Payment.Cols[COL_PARTY].Width = 98;
                        c1Payment.Cols[COL_UNIT].Width =0;
                        c1Payment.Cols[COL_MODIFIER].Width = 75;
                        c1Payment.Cols[COL_REASON].Width = 22;//19;
                    }

                }

                #endregion

                #region " Data Type "

                c1Payment.Cols[COL_GENERAL].DataType = typeof(System.String);
                c1Payment.Cols[COL_PATIENTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_PATIENTNAME].DataType = typeof(System.String);
                c1Payment.Cols[COL_CLAIMDISPNO].DataType = typeof(System.String);
                c1Payment.Cols[COL_CLAIMNO].DataType = typeof(System.String);
                c1Payment.Cols[COL_BILLING_TRANSACTON_ID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_BILLING_TRANSACTON_DETAILID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_BILLING_TRANSACTON_LINENO].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_PAYMENT_NO].DataType = typeof(System.String);
                c1Payment.Cols[COL_PAY_EOBPAYMENTID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_PAY_EOBID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_PAY_EOBDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_PAY_EOBPAYMENTDTLID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_DOS_FROM].DataType = typeof(System.String);
                c1Payment.Cols[COL_DOS_TO].DataType = typeof(System.String);

                c1Payment.Cols[COL_CPT_CODE].DataType = typeof(System.String);
                c1Payment.Cols[COL_CPT_DESCRIPTON].DataType = typeof(System.String);

                c1Payment.Cols[COL_CROSSWALK_CPT_CODE].DataType = typeof(System.String);
                c1Payment.Cols[COL_CROSSWALK_CPT_DESC].DataType = typeof(System.String);

                c1Payment.Cols[COL_CHARGE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_UNIT].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_TOTALCHARGE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_ALLOWED].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_PAYMENT].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_WRITEOFF].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_COPAY].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_DEDUCTIBLE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_COINSURANCE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_WITHHOLD].DataType = typeof(System.Decimal);

                c1Payment.Cols[COL_LAST_ALLOWED].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_LAST_PAYMENT].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_LAST_WRITEOFF].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_LAST_COPAY].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_LAST_DEDUCTIBLE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_LAST_COINSURANCE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_LAST_WITHHOLD].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_ISCORRECTION].DataType = typeof(System.Boolean);

                c1Payment.Cols[COL_PAYMENTTYPE].DataType = typeof(System.Int16);
                c1Payment.Cols[COL_PAYMENTSUBTYPE].DataType = typeof(System.Int16);

                c1Payment.Cols[COL_PREVPAID].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_BALANCE].DataType = typeof(System.Decimal);
                c1Payment.Cols[COL_UNIT].DataType = Type.GetType("System.Decimal");
                c1Payment.Cols[COL_NEXT].DataType = typeof(System.String);
                c1Payment.Cols[COL_PARTY].DataType = typeof(System.String);
           
                if (c1Payment.Name == c1MultiplePayment.Name || c1Payment.Name == c1MultiplePaymentTotal.Name)
                {
                    c1Payment.Cols[COL_REASON].DataType = typeof(System.Decimal);
                }
                else
                {
                    c1Payment.Cols[COL_REASON].DataType = typeof(C1.Win.C1FlexGrid.C1FlexGrid);
                }

                c1Payment.Cols[COL_SERVICELINE_TYPE].DataType = typeof(ColServiceLineType);

                c1Payment.Cols[COL_LINE_STATEMENTNOTE].DataType = typeof(System.String);
                c1Payment.Cols[COL_LINE_STATEMENTNOTEONPRINT].DataType = typeof(System.Boolean);
                c1Payment.Cols[COL_LINE_INTERNALNOTE].DataType = typeof(System.String);
                c1Payment.Cols[COL_LINE_INTERNALNOTEONPRINT].DataType = typeof(System.Boolean);
                c1Payment.Cols[COL_LINE_DB_BALANCE].DataType = typeof(System.Decimal);

                c1Payment.Cols[COL_SUBCLAIMNO].DataType = typeof(System.String);
                c1Payment.Cols[COL_TRACK_BILLING_TRANSACTON_ID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_TRACK_BILLING_TRANSACTON_DETAILID].DataType = typeof(System.Int64);
                c1Payment.Cols[COL_TRACK_BILLING_TRANSACTON_LINENO].DataType = typeof(System.Int32);

                c1Payment.Cols[COL_ISSPLITTED].DataType = typeof(System.Boolean);
                c1Payment.Cols[COL_MODIFIER].DataType = typeof(System.String);
           
                #endregion

                #region " Alignment "

                c1Payment.Cols[COL_GENERAL].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PATIENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PATIENTNAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_CLAIMDISPNO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_CLAIMNO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_BILLING_TRANSACTON_ID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_BILLING_TRANSACTON_DETAILID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_BILLING_TRANSACTON_LINENO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PAYMENT_NO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PAY_EOBPAYMENTID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PAY_EOBID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PAY_EOBDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PAY_EOBPAYMENTDTLID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_DOS_FROM].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_UNIT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_DOS_TO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                c1Payment.Cols[COL_CPT_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_CPT_DESCRIPTON].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                c1Payment.Cols[COL_CROSSWALK_CPT_CODE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_CROSSWALK_CPT_DESC].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

                c1Payment.Cols[COL_CHARGE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_ALLOWED].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_PAYMENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_WRITEOFF].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_COPAY].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_DEDUCTIBLE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_COINSURANCE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_WITHHOLD].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

                c1Payment.Cols[COL_LAST_ALLOWED].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_LAST_PAYMENT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_LAST_WRITEOFF].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_LAST_COPAY].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_LAST_DEDUCTIBLE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_LAST_COINSURANCE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_LAST_WITHHOLD].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_ISCORRECTION].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

                c1Payment.Cols[COL_PREVPAID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_BALANCE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_NEXT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[COL_PARTY].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
           
                c1Payment.Cols[COL_REASON].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;

                c1Payment.Cols[COL_LINE_DB_BALANCE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

                c1Payment.Cols[COL_SUBCLAIMNO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_TRACK_BILLING_TRANSACTON_ID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_TRACK_BILLING_TRANSACTON_DETAILID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_TRACK_BILLING_TRANSACTON_LINENO].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[COL_MODIFIER].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

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
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    }

                }
                catch
                {
                    csCurrencyStyle = c1Payment.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                }
  

                c1Payment.Cols[COL_CHARGE].Style = csCurrencyStyle;
                c1Payment.Cols[COL_TOTALCHARGE].Style = csCurrencyStyle;
                c1Payment.Cols[COL_ALLOWED].Style = csCurrencyStyle;
                c1Payment.Cols[COL_PAYMENT].Style = csCurrencyStyle;
                c1Payment.Cols[COL_WRITEOFF].Style = csCurrencyStyle;
                c1Payment.Cols[COL_COPAY].Style = csCurrencyStyle;
                c1Payment.Cols[COL_DEDUCTIBLE].Style = csCurrencyStyle;
                c1Payment.Cols[COL_COINSURANCE].Style = csCurrencyStyle;
                c1Payment.Cols[COL_WITHHOLD].Style = csCurrencyStyle;
                c1Payment.Cols[COL_PREVPAID].Style = csCurrencyStyle;
                c1Payment.Cols[COL_BALANCE].Style = csCurrencyStyle;
                c1Payment.Cols[COL_LINE_DB_BALANCE].Style = csCurrencyStyle;

                c1Payment.Cols[COL_LAST_ALLOWED].Style = csCurrencyStyle;
                c1Payment.Cols[COL_LAST_PAYMENT].Style = csCurrencyStyle;
                c1Payment.Cols[COL_LAST_WRITEOFF].Style = csCurrencyStyle;
                c1Payment.Cols[COL_LAST_COPAY].Style = csCurrencyStyle;
                c1Payment.Cols[COL_LAST_DEDUCTIBLE].Style = csCurrencyStyle;
                c1Payment.Cols[COL_LAST_COINSURANCE].Style = csCurrencyStyle;
                c1Payment.Cols[COL_LAST_WITHHOLD].Style = csCurrencyStyle;

                if (c1Payment.Name == c1MultiplePayment.Name || c1Payment.Name == c1MultiplePaymentTotal.Name)
                {
                    c1Payment.Cols[COL_REASON].Style = csCurrencyStyle;
                }

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
                        csEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableCurrencyStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableCurrencyStyle = c1Payment.Styles.Add("cs_EditableCurrencyStyle");
                    csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                    csEditableCurrencyStyle.Format = "c";
                    csEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableCurrencyStyle.BackColor = Color.White;
                }
 

                C1.Win.C1FlexGrid.CellStyle csNonEditableCurrencyStyle;// = c1Payment.Styles.Add("cs_NonEditableCurrencyStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_NonEditableCurrencyStyle"))
                    {
                        csNonEditableCurrencyStyle = c1Payment.Styles["cs_NonEditableCurrencyStyle"];
                    }
                    else
                    {
                        csNonEditableCurrencyStyle = c1Payment.Styles.Add("cs_NonEditableCurrencyStyle");
                        csNonEditableCurrencyStyle.DataType = typeof(System.Decimal);
                        csNonEditableCurrencyStyle.Format = "c";
                        csNonEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csNonEditableCurrencyStyle.BackColor = Color.FromArgb(232, 230, 230);
                    }

                }
                catch
                {
                    csNonEditableCurrencyStyle = c1Payment.Styles.Add("cs_NonEditableCurrencyStyle");
                    csNonEditableCurrencyStyle.DataType = typeof(System.Decimal);
                    csNonEditableCurrencyStyle.Format = "c";
                    csNonEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csNonEditableCurrencyStyle.BackColor = Color.FromArgb(232, 230, 230);
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
                        csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);
                    }

                }
                catch
                {
                    csClaimRowStyle = c1Payment.Styles.Add("cs_ClaimRowStyle");
                    csClaimRowStyle.DataType = typeof(System.String);
                    csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
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
                        csPatientRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csPatientRowStyle.BackColor = Color.FromArgb(215, 228, 188);
                    }

                }
                catch
                {
                    csPatientRowStyle = c1Payment.Styles.Add("cs_PatientRowStyle");
                    csPatientRowStyle.DataType = typeof(System.String);
                    csPatientRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csPatientRowStyle.BackColor = Color.FromArgb(215, 228, 188);
                }
           


                #region " Set Payment Action Status "

                if (c1Payment.Name == c1SinglePayment.Name || c1Payment.Name == c1SinglePaymentCorrTB.Name)
                {
                    string _comboList = "";
                    gloAccountsV2.PaymentCollection.gloEOBPaymentInsurance ogloEOBPayIns = new gloAccountsV2.PaymentCollection.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);

                    C1.Win.C1FlexGrid.CellStyle csEditableActionStatus;// = c1Payment.Styles.Add("cs_EditableActionStatus");
                    try
                    {
                        if (c1Payment.Styles.Contains("cs_EditableActionStatus"))
                        {
                            csEditableActionStatus = c1Payment.Styles["cs_EditableActionStatus"];
                        }
                        else
                        {
                            csEditableActionStatus = c1Payment.Styles.Add("cs_EditableActionStatus");
                            csEditableActionStatus.DataType = typeof(System.String);
                            csEditableActionStatus.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                            csEditableActionStatus.BackColor = Color.White;
                        }

                    }
                    catch
                    {
                        csEditableActionStatus = c1Payment.Styles.Add("cs_EditableActionStatus");
                        csEditableActionStatus.DataType = typeof(System.String);
                        csEditableActionStatus.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableActionStatus.BackColor = Color.White;
                    }
       

                    _comboList = gloInsurancePaymentV2.GetNextActions();

                    csEditableActionStatus.ComboList = _comboList;

                    C1.Win.C1FlexGrid.CellStyle csEditableParty;// = c1Payment.Styles.Add("cs_EditableParty");
                    try
                    {
                        if (c1Payment.Styles.Contains("cs_EditableParty"))
                        {
                            csEditableParty = c1Payment.Styles["cs_EditableParty"];
                        }
                        else
                        {
                            csEditableParty = c1Payment.Styles.Add("cs_EditableParty");
                            csEditableParty.DataType = typeof(System.String);
                            csEditableParty.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                            csEditableParty.BackColor = Color.White;
                        }

                    }
                    catch
                    {
                        csEditableParty = c1Payment.Styles.Add("cs_EditableParty");
                        csEditableParty.DataType = typeof(System.String);
                        csEditableParty.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableParty.BackColor = Color.White;
                    }
            
                    _comboList = "";

                    if (PatientControl != null)
                    {
                        //_comboList = gloInsurancePaymentV2.GetInsuranceParties(PatientControl.ClaimNumber);
                        _comboList = gloInsurancePaymentV2.GetInsuranceParties(ClaimDetails.TransactionMasterID,ClaimDetails.TransactionID);
                        csEditableParty.ComboList = _comboList;
                    }

                    C1.Win.C1FlexGrid.CellStyle csEditableReason;// = c1Payment.Styles.Add("cs_EditableReason");
                    try
                    {
                        if (c1Payment.Styles.Contains("cs_EditableReason"))
                        {
                            csEditableReason = c1Payment.Styles["cs_EditableReason"];
                        }
                        else
                        {
                            csEditableReason = c1Payment.Styles.Add("cs_EditableReason");
                            csEditableReason.DataType = typeof(C1.Win.C1FlexGrid.C1FlexGrid);
                            csEditableReason.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                            csEditableReason.BackColor = Color.White;
                            csEditableReason.ComboList = "...";
                        }

                    }
                    catch
                    {
                        csEditableReason = c1Payment.Styles.Add("cs_EditableReason");
                        csEditableReason.DataType = typeof(C1.Win.C1FlexGrid.C1FlexGrid);
                        csEditableReason.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableReason.BackColor = Color.White;
                        csEditableReason.ComboList = "...";
                    }
           

                    ogloEOBPayIns.Dispose();

                }
                #endregion " Set Payment Action Status "

                #endregion

                c1Payment.AllowDragging = C1.Win.C1FlexGrid.AllowDraggingEnum.None;
                c1Payment.AllowResizing = C1.Win.C1FlexGrid.AllowResizingEnum.Columns;

                #region " Allow Editing "

                c1Payment.AllowEditing = true;

                if (c1Payment.Name == c1SinglePayment.Name || c1Payment.Name == c1SinglePaymentCorrTB.Name)
                {
                    c1Payment.Cols[COL_GENERAL].AllowEditing = false;
                    c1Payment.Cols[COL_PATIENTID].AllowEditing = false;
                    c1Payment.Cols[COL_PATIENTNAME].AllowEditing = false;
                    c1Payment.Cols[COL_CLAIMDISPNO].AllowEditing = false;
                    c1Payment.Cols[COL_CLAIMNO].AllowEditing = false;
                    c1Payment.Cols[COL_BILLING_TRANSACTON_ID].AllowEditing = false;
                    c1Payment.Cols[COL_BILLING_TRANSACTON_DETAILID].AllowEditing = false;
                    c1Payment.Cols[COL_BILLING_TRANSACTON_LINENO].AllowEditing = false;
                    c1Payment.Cols[COL_PAYMENT_NO].AllowEditing = false;
                    c1Payment.Cols[COL_PAY_EOBPAYMENTID].AllowEditing = false;
                    c1Payment.Cols[COL_PAY_EOBID].AllowEditing = false;
                    c1Payment.Cols[COL_PAY_EOBDTLID].AllowEditing = false;
                    c1Payment.Cols[COL_PAY_EOBPAYMENTDTLID].AllowEditing = false;
                    c1Payment.Cols[COL_DOS_FROM].AllowEditing = false;
                    c1Payment.Cols[COL_DOS_TO].AllowEditing = false;

                    c1Payment.Cols[COL_CPT_CODE].AllowEditing = false;
                    c1Payment.Cols[COL_CPT_DESCRIPTON].AllowEditing = false;

                    c1Payment.Cols[COL_CROSSWALK_CPT_CODE].AllowEditing = false;
                    c1Payment.Cols[COL_CROSSWALK_CPT_DESC].AllowEditing = false;

                    c1Payment.Cols[COL_CHARGE].AllowEditing = false;
                    c1Payment.Cols[COL_UNIT].AllowEditing = false;
                    c1Payment.Cols[COL_TOTALCHARGE].AllowEditing = false;
                    c1Payment.Cols[COL_ALLOWED].AllowEditing = true;
                    c1Payment.Cols[COL_PAYMENT].AllowEditing = true;
                    c1Payment.Cols[COL_WRITEOFF].AllowEditing = true;
                    c1Payment.Cols[COL_COPAY].AllowEditing = true;
                    c1Payment.Cols[COL_DEDUCTIBLE].AllowEditing = true;
                    c1Payment.Cols[COL_COINSURANCE].AllowEditing = true;
                    c1Payment.Cols[COL_WITHHOLD].AllowEditing = true;

                    c1Payment.Cols[COL_LAST_ALLOWED].AllowEditing = false;
                    c1Payment.Cols[COL_LAST_PAYMENT].AllowEditing = false;
                    c1Payment.Cols[COL_LAST_WRITEOFF].AllowEditing = false;
                    c1Payment.Cols[COL_LAST_COPAY].AllowEditing = false;
                    c1Payment.Cols[COL_LAST_DEDUCTIBLE].AllowEditing = false;
                    c1Payment.Cols[COL_LAST_COINSURANCE].AllowEditing = false;
                    c1Payment.Cols[COL_LAST_WITHHOLD].AllowEditing = false;

                    c1Payment.Cols[COL_PREVPAID].AllowEditing = false;
                    c1Payment.Cols[COL_BALANCE].AllowEditing = false;
                    c1Payment.Cols[COL_NEXT].AllowEditing = true;
                    c1Payment.Cols[COL_PARTY].AllowEditing = true;
                    c1Payment.Cols[COL_REASON].AllowEditing = true;
                    c1Payment.Cols[COL_LINE_DB_BALANCE].AllowEditing = false;

                    c1Payment.Cols[COL_SUBCLAIMNO].AllowEditing = false;
                    c1Payment.Cols[COL_TRACK_BILLING_TRANSACTON_ID].AllowEditing = false;
                    c1Payment.Cols[COL_TRACK_BILLING_TRANSACTON_DETAILID].AllowEditing = false;
                    c1Payment.Cols[COL_TRACK_BILLING_TRANSACTON_LINENO].AllowEditing = false;
                    c1Payment.Cols[COL_MODIFIER].AllowEditing = false;
                }
                else
                {
                    for (int i = 0; i <= c1Payment.Cols.Count - 1; i++)
                    {
                        c1Payment.Cols[i].AllowEditing = false;
                    }
                }
                #endregion

                if (c1Payment.Name == c1MultiplePayment.Name)
                {
                    c1Payment.Cols[COL_PATIENTNAME].Visible = true;
                    c1Payment.Cols[COL_PATIENTNAME].Width = 200;
                    c1Payment.Cols[COL_PREVPAID].Visible = false;
                    c1Payment.Cols[COL_BALANCE].Visible = false;
                    c1Payment.Cols[COL_NEXT].Visible = false;
                    c1Payment.Cols[COL_PARTY].Visible = false;
                    c1Payment.Cols[COL_UNIT].Visible = false;
                 
                    c1Payment.ScrollBars = ScrollBars.Vertical;
                }
                else if (c1Payment.Name == c1MultiplePaymentTotal.Name)
                {
                    c1Payment.Cols[COL_PATIENTNAME].Visible = true;
                    c1Payment.Cols[COL_PATIENTNAME].Width = 200;
                    c1Payment.Cols[COL_PREVPAID].Visible = false;
                    c1Payment.Cols[COL_BALANCE].Visible = false;
                    c1Payment.Cols[COL_NEXT].Visible = false;
                    c1Payment.Cols[COL_PARTY].Visible = false;
                }

                c1Payment.KeyActionEnter = KeyActionEnum.MoveAcross;
                c1Payment.VisualStyle = VisualStyle.Office2007Blue;
                c1Payment.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                c1Payment.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                c1Payment.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);

                if (c1Payment.Name == c1SinglePaymentTotal.Name || c1Payment.Name == c1MultiplePaymentTotal.Name)
                {
                    #region " Set the style for the total grid "

                    C1.Win.C1FlexGrid.CellStyle csTotalHeader;// = c1Payment.Styles.Add("cs_TotalHeader");
                    try
                    {
                        if (c1Payment.Styles.Contains("cs_TotalHeader"))
                        {
                            csTotalHeader = c1Payment.Styles["cs_TotalHeader"];
                        }
                        else
                        {
                            csTotalHeader = c1Payment.Styles.Add("cs_TotalHeader");
                            csTotalHeader.DataType = typeof(System.String);
                            csTotalHeader.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                            csTotalHeader.ForeColor = Color.Maroon;
                            csTotalHeader.TextAlign = TextAlignEnum.RightCenter;
                        }

                    }
                    catch
                    {
                        csTotalHeader = c1Payment.Styles.Add("cs_TotalHeader");
                        csTotalHeader.DataType = typeof(System.String);
                        csTotalHeader.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csTotalHeader.ForeColor = Color.Maroon;
                        csTotalHeader.TextAlign = TextAlignEnum.RightCenter;
                    }
                
                    c1Payment.Styles[CellStyleEnum.Fixed].ForeColor = Color.Maroon;
                    c1Payment.Styles[CellStyleEnum.Alternate].ForeColor = Color.Maroon;
                    c1Payment.SetData(0, COL_MODIFIER, "Total : ");
                    c1Payment.SetCellStyle(0, COL_CPT_CODE, csTotalHeader);
                    #endregion " Set the style for the total grid "
                }

                c1Payment.ExtendLastCol = true;

            }
            catch(Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {              
                c1Payment.Redraw = true;
                c1SinglePayment.TabStop = false;
            }
        }

        private void DesignMultiplePaymentGrid(C1FlexGrid c1Payment)
        {
            try
            {
                c1Payment.Redraw = false;
                c1Payment.ScrollBars = ScrollBars.None;
                c1Payment.AllowSorting = AllowSortingEnum.None;
                c1Payment.Cols.Fixed = 0;

                #region " Set Headers "

                c1Payment.SetData(0, c1Payment.Cols["COL_GENERAL"].Index, "General");
                c1Payment.SetData(0, c1Payment.Cols["COL_PATIENTID"].Index, "PatientID");
                c1Payment.SetData(0, c1Payment.Cols["COL_PATIENTNAME"].Index, "Patient");
                c1Payment.SetData(0, c1Payment.Cols["COL_CLAIMDISPNO"].Index, "Claim #");
                c1Payment.SetData(0, c1Payment.Cols["COL_BILLING_TRANSACTON_ID"].Index, "Transacton ID");
                c1Payment.SetData(0, c1Payment.Cols["COL_BILLING_TRANSACTON_DETAILID"].Index, "Transacton Detail ID");
                c1Payment.SetData(0, c1Payment.Cols["COL_PAY_EOBPAYMENTID"].Index, "EOBPaymentID");
                c1Payment.SetData(0, c1Payment.Cols["COL_PAY_EOBID"].Index, "EOBID");
                c1Payment.SetData(0, c1Payment.Cols["COL_DOS_FROM"].Index, "DOS");
                c1Payment.SetData(0, c1Payment.Cols["COL_CPT_CODE"].Index, "CPT");
                c1Payment.SetData(0, c1Payment.Cols["COL_MODIFIER"].Index, "Mod");
                c1Payment.SetData(0, c1Payment.Cols["COL_TOTALCHARGE"].Index, "Charge");
                c1Payment.SetData(0, c1Payment.Cols["COL_ALLOWED"].Index, "Allowed");
                c1Payment.SetData(0, c1Payment.Cols["COL_PAYMENT"].Index, "Payment");
                c1Payment.SetData(0, c1Payment.Cols["COL_WRITEOFF"].Index, "W/O");
                c1Payment.SetData(0, c1Payment.Cols["COL_COPAY"].Index, "Copay");
                c1Payment.SetData(0, c1Payment.Cols["COL_DEDUCTIBLE"].Index, "Deduct");
                c1Payment.SetData(0, c1Payment.Cols["COL_COINSURANCE"].Index, "Co-ins");
                c1Payment.SetData(0, c1Payment.Cols["COL_WITHHOLD"].Index, "Withhold");
                c1Payment.SetData(0, c1Payment.Cols["COL_REASON"].Index, "Other");
                c1Payment.SetData(0, c1Payment.Cols["COL_SERVICELINE_TYPE"].Index, "COL_SERVICELINE_TYPE");

                #endregion " Set Headers "

                #region " Show/Hide "
                
                c1MultiplePayment.Cols[c1Payment.Cols["COL_GENERAL"].Index].Visible = false;
                c1MultiplePayment.Cols[c1Payment.Cols["COL_PATIENTID"].Index].Visible = false;
                c1MultiplePayment.Cols[c1Payment.Cols["COL_BILLING_TRANSACTON_ID"].Index].Visible = false;
                c1MultiplePayment.Cols[c1Payment.Cols["COL_BILLING_TRANSACTON_DETAILID"].Index].Visible = false;
                c1MultiplePayment.Cols[c1Payment.Cols["COL_PAY_EOBPAYMENTID"].Index].Visible = false;
                c1MultiplePayment.Cols[c1Payment.Cols["COL_PAY_EOBID"].Index].Visible = false;
                c1MultiplePayment.Cols[c1Payment.Cols["COL_SERVICELINE_TYPE"].Index].Visible = false;

                c1MultiplePayment.Cols[c1Payment.Cols["COL_PATIENTNAME"].Index].Visible = true;
                c1MultiplePayment.Cols[c1Payment.Cols["COL_PATIENTNAME"].Index].Width = 200;

                #endregion " Show/Hide "

                #region " Width "
                
                c1Payment.Cols[c1Payment.Cols["COL_GENERAL"].Index].Width = 0;
                    c1Payment.Cols[c1Payment.Cols["COL_PATIENTID"].Index].Width = 0;
                    c1Payment.Cols[c1Payment.Cols["COL_PATIENTNAME"].Index].Width = 0;
                    c1Payment.Cols[c1Payment.Cols["COL_CLAIMDISPNO"].Index].Width = 70;
                    c1Payment.Cols[c1Payment.Cols["COL_BILLING_TRANSACTON_ID"].Index].Width = 0;
                    c1Payment.Cols[c1Payment.Cols["COL_BILLING_TRANSACTON_DETAILID"].Index].Width = 0;
                    c1Payment.Cols[c1Payment.Cols["COL_PAY_EOBPAYMENTID"].Index].Width = 0;
                    c1Payment.Cols[c1Payment.Cols["COL_PAY_EOBID"].Index].Width = 0;
                    c1Payment.Cols[c1Payment.Cols["COL_DOS_FROM"].Index].Width = 75;
                    c1Payment.Cols[c1Payment.Cols["COL_CPT_CODE"].Index].Width = 50;
                    c1Payment.Cols[c1Payment.Cols["COL_MODIFIER"].Index].Width = 75;
                    c1Payment.Cols[c1Payment.Cols["COL_TOTALCHARGE"].Index].Width = 100;
                    c1Payment.Cols[c1Payment.Cols["COL_ALLOWED"].Index].Width = 100;
                    c1Payment.Cols[c1Payment.Cols["COL_PAYMENT"].Index].Width = 90;
                    c1Payment.Cols[c1Payment.Cols["COL_WRITEOFF"].Index].Width = 80;
                    c1Payment.Cols[c1Payment.Cols["COL_COPAY"].Index].Width = 80;
                    c1Payment.Cols[c1Payment.Cols["COL_DEDUCTIBLE"].Index].Width = 80;
                    c1Payment.Cols[c1Payment.Cols["COL_COINSURANCE"].Index].Width = 80;
                    c1Payment.Cols[c1Payment.Cols["COL_WITHHOLD"].Index].Width = 80;
                    c1Payment.Cols[c1Payment.Cols["COL_REASON"].Index].Width = 22;//19;
                    c1Payment.Cols[c1Payment.Cols["COL_SERVICELINE_TYPE"].Index].Width = 0;
                    

                #endregion

                #region " Data Type "

                c1Payment.Cols[c1Payment.Cols["COL_GENERAL"].Index].DataType = typeof(System.String);
                c1Payment.Cols[c1Payment.Cols["COL_PATIENTID"].Index].DataType = typeof(System.Int64);
                c1Payment.Cols[c1Payment.Cols["COL_PATIENTNAME"].Index].DataType = typeof(System.String);
                c1Payment.Cols[c1Payment.Cols["COL_CLAIMDISPNO"].Index].DataType = typeof(System.String);
                c1Payment.Cols[c1Payment.Cols["COL_BILLING_TRANSACTON_ID"].Index].DataType = typeof(System.Int64);
                c1Payment.Cols[c1Payment.Cols["COL_BILLING_TRANSACTON_DETAILID"].Index].DataType = typeof(System.Int64);
                c1Payment.Cols[c1Payment.Cols["COL_PAY_EOBPAYMENTID"].Index].DataType = typeof(System.Int64);
                c1Payment.Cols[c1Payment.Cols["COL_PAY_EOBID"].Index].DataType = typeof(System.Int64);
                c1Payment.Cols[c1Payment.Cols["COL_DOS_FROM"].Index].DataType = typeof(System.String);
                c1Payment.Cols[c1Payment.Cols["COL_CPT_CODE"].Index].DataType = typeof(System.String);
                c1Payment.Cols[c1Payment.Cols["COL_TOTALCHARGE"].Index].DataType = typeof(System.Decimal);
                c1Payment.Cols[c1Payment.Cols["COL_ALLOWED"].Index].DataType = typeof(System.Decimal);
                c1Payment.Cols[c1Payment.Cols["COL_PAYMENT"].Index].DataType = typeof(System.Decimal);
                c1Payment.Cols[c1Payment.Cols["COL_WRITEOFF"].Index].DataType = typeof(System.Decimal);
                c1Payment.Cols[c1Payment.Cols["COL_COPAY"].Index].DataType = typeof(System.Decimal);
                c1Payment.Cols[c1Payment.Cols["COL_DEDUCTIBLE"].Index].DataType = typeof(System.Decimal);
                c1Payment.Cols[c1Payment.Cols["COL_COINSURANCE"].Index].DataType = typeof(System.Decimal);
                c1Payment.Cols[c1Payment.Cols["COL_WITHHOLD"].Index].DataType = typeof(System.Decimal);
                c1Payment.Cols[c1Payment.Cols["COL_REASON"].Index].DataType = typeof(System.Decimal);
                c1Payment.Cols[c1Payment.Cols["COL_SERVICELINE_TYPE"].Index].DataType = typeof(ColServiceLineType);
                c1Payment.Cols[c1Payment.Cols["COL_MODIFIER"].Index].DataType = typeof(System.String);

                #endregion

                #region " Alignment "

                c1Payment.Cols[c1Payment.Cols["COL_GENERAL"].Index].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[c1Payment.Cols["COL_PATIENTID"].Index].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[c1Payment.Cols["COL_PATIENTNAME"].Index].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[c1Payment.Cols["COL_CLAIMDISPNO"].Index].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[c1Payment.Cols["COL_BILLING_TRANSACTON_ID"].Index].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[c1Payment.Cols["COL_BILLING_TRANSACTON_DETAILID"].Index].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[c1Payment.Cols["COL_PAY_EOBPAYMENTID"].Index].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[c1Payment.Cols["COL_PAY_EOBID"].Index].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[c1Payment.Cols["COL_DOS_FROM"].Index].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[c1Payment.Cols["COL_CPT_CODE"].Index].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1Payment.Cols[c1Payment.Cols["COL_ALLOWED"].Index].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[c1Payment.Cols["COL_PAYMENT"].Index].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[c1Payment.Cols["COL_WRITEOFF"].Index].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[c1Payment.Cols["COL_COPAY"].Index].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[c1Payment.Cols["COL_DEDUCTIBLE"].Index].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[c1Payment.Cols["COL_COINSURANCE"].Index].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[c1Payment.Cols["COL_WITHHOLD"].Index].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                c1Payment.Cols[c1Payment.Cols["COL_REASON"].Index].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                c1Payment.Cols[c1Payment.Cols["COL_MODIFIER"].Index].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;

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
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    }

                }
                catch
                {
                    csCurrencyStyle = c1Payment.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                }


                
                c1Payment.Cols[c1Payment.Cols["COL_TOTALCHARGE"].Index].Style = csCurrencyStyle;
                c1Payment.Cols[c1Payment.Cols["COL_ALLOWED"].Index].Style = csCurrencyStyle;
                c1Payment.Cols[c1Payment.Cols["COL_PAYMENT"].Index].Style = csCurrencyStyle;
                c1Payment.Cols[c1Payment.Cols["COL_WRITEOFF"].Index].Style = csCurrencyStyle;
                c1Payment.Cols[c1Payment.Cols["COL_COPAY"].Index].Style = csCurrencyStyle;
                c1Payment.Cols[c1Payment.Cols["COL_DEDUCTIBLE"].Index].Style = csCurrencyStyle;
                c1Payment.Cols[c1Payment.Cols["COL_COINSURANCE"].Index].Style = csCurrencyStyle;
                c1Payment.Cols[c1Payment.Cols["COL_WITHHOLD"].Index].Style = csCurrencyStyle;
                c1Payment.Cols[c1Payment.Cols["COL_REASON"].Index].Style = csCurrencyStyle;


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
                        csEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableCurrencyStyle.BackColor = Color.White;
                    }

                }
                catch
                {
                    csEditableCurrencyStyle = c1Payment.Styles.Add("cs_EditableCurrencyStyle");
                    csEditableCurrencyStyle.DataType = typeof(System.Decimal);
                    csEditableCurrencyStyle.Format = "c";
                    csEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;// new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csEditableCurrencyStyle.BackColor = Color.White;
                }


                C1.Win.C1FlexGrid.CellStyle csNonEditableCurrencyStyle;// = c1Payment.Styles.Add("cs_NonEditableCurrencyStyle");
                try
                {
                    if (c1Payment.Styles.Contains("cs_NonEditableCurrencyStyle"))
                    {
                        csNonEditableCurrencyStyle = c1Payment.Styles["cs_NonEditableCurrencyStyle"];
                    }
                    else
                    {
                        csNonEditableCurrencyStyle = c1Payment.Styles.Add("cs_NonEditableCurrencyStyle");
                        csNonEditableCurrencyStyle.DataType = typeof(System.Decimal);
                        csNonEditableCurrencyStyle.Format = "c";
                        csNonEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csNonEditableCurrencyStyle.BackColor = Color.FromArgb(232, 230, 230);
                    }

                }
                catch
                {
                    csNonEditableCurrencyStyle = c1Payment.Styles.Add("cs_NonEditableCurrencyStyle");
                    csNonEditableCurrencyStyle.DataType = typeof(System.Decimal);
                    csNonEditableCurrencyStyle.Format = "c";
                    csNonEditableCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csNonEditableCurrencyStyle.BackColor = Color.FromArgb(232, 230, 230);
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
                        csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csClaimRowStyle.BackColor = Color.FromArgb(229, 224, 236);
                    }

                }
                catch
                {
                    csClaimRowStyle = c1Payment.Styles.Add("cs_ClaimRowStyle");
                    csClaimRowStyle.DataType = typeof(System.String);
                    csClaimRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
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
                        csPatientRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csPatientRowStyle.BackColor = Color.FromArgb(215, 228, 188);
                    }

                }
                catch
                {
                    csPatientRowStyle = c1Payment.Styles.Add("cs_PatientRowStyle");
                    csPatientRowStyle.DataType = typeof(System.String);
                    csPatientRowStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csPatientRowStyle.BackColor = Color.FromArgb(215, 228, 188);
                }

                #region " Set Payment Action Status "

                //if (c1Payment.Name == c1SinglePayment.Name || c1Payment.Name == c1MultiplePayment.Name || c1Payment.Name == c1SinglePaymentCorrTB.Name)
                //{
                //    string _comboList = "";
                //    gloAccountsV2.PaymentCollection.gloEOBPaymentInsurance ogloEOBPayIns = new gloAccountsV2.PaymentCollection.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);

                //    C1.Win.C1FlexGrid.CellStyle csEditableActionStatus;// = c1Payment.Styles.Add("cs_EditableActionStatus");
                //    try
                //    {
                //        if (c1Payment.Styles.Contains("cs_EditableActionStatus"))
                //        {
                //            csEditableActionStatus = c1Payment.Styles["cs_EditableActionStatus"];
                //        }
                //        else
                //        {
                //            csEditableActionStatus = c1Payment.Styles.Add("cs_EditableActionStatus");
                //            csEditableActionStatus.DataType = typeof(System.String);
                //            csEditableActionStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                //            csEditableActionStatus.BackColor = Color.White;
                //        }

                //    }
                //    catch
                //    {
                //        csEditableActionStatus = c1Payment.Styles.Add("cs_EditableActionStatus");
                //        csEditableActionStatus.DataType = typeof(System.String);
                //        csEditableActionStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                //        csEditableActionStatus.BackColor = Color.White;
                //    }


                //    _comboList = gloInsurancePaymentV2.GetNextActions();

                //    csEditableActionStatus.ComboList = _comboList;

                //    C1.Win.C1FlexGrid.CellStyle csEditableParty;// = c1Payment.Styles.Add("cs_EditableParty");
                //    try
                //    {
                //        if (c1Payment.Styles.Contains("cs_EditableParty"))
                //        {
                //            csEditableParty = c1Payment.Styles["cs_EditableParty"];
                //        }
                //        else
                //        {
                //            csEditableParty = c1Payment.Styles.Add("cs_EditableParty");
                //            csEditableParty.DataType = typeof(System.String);
                //            csEditableParty.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                //            csEditableParty.BackColor = Color.White;
                //        }

                //    }
                //    catch
                //    {
                //        csEditableParty = c1Payment.Styles.Add("cs_EditableParty");
                //        csEditableParty.DataType = typeof(System.String);
                //        csEditableParty.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                //        csEditableParty.BackColor = Color.White;
                //    }

                //    _comboList = "";

                //    if (PatientControl != null)
                //    {
                //        //_comboList = gloInsurancePaymentV2.GetInsuranceParties(PatientControl.ClaimNumber);
                //        _comboList = gloInsurancePaymentV2.GetInsuranceParties(ClaimDetails.TransactionMasterID, ClaimDetails.TransactionID);
                //        csEditableParty.ComboList = _comboList;
                //    }

                //    C1.Win.C1FlexGrid.CellStyle csEditableReason;// = c1Payment.Styles.Add("cs_EditableReason");
                //    try
                //    {
                //        if (c1Payment.Styles.Contains("cs_EditableReason"))
                //        {
                //            csEditableReason = c1Payment.Styles["cs_EditableReason"];
                //        }
                //        else
                //        {
                //            csEditableReason = c1Payment.Styles.Add("cs_EditableReason");
                //            csEditableReason.DataType = typeof(C1.Win.C1FlexGrid.C1FlexGrid);
                //            csEditableReason.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                //            csEditableReason.BackColor = Color.White;
                //            csEditableReason.ComboList = "...";
                //        }

                //    }
                //    catch
                //    {
                //        csEditableReason = c1Payment.Styles.Add("cs_EditableReason");
                //        csEditableReason.DataType = typeof(C1.Win.C1FlexGrid.C1FlexGrid);
                //        csEditableReason.Font = new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                //        csEditableReason.BackColor = Color.White;
                //        csEditableReason.ComboList = "...";
                //    }


                //    ogloEOBPayIns.Dispose();

                //}

                #endregion " Set Payment Action Status "

                #endregion

                c1Payment.AllowEditing = false;

                if (c1Payment.Name == c1MultiplePayment.Name)
                {
                    c1Payment.Cols[c1Payment.Cols["COL_PATIENTNAME"].Index].Visible = true;
                    c1Payment.Cols[c1Payment.Cols["COL_PATIENTNAME"].Index].Width = 200;
                    c1Payment.ScrollBars = ScrollBars.Vertical;
                }

                c1Payment.KeyActionEnter = KeyActionEnum.MoveAcross;
                c1Payment.VisualStyle = VisualStyle.Office2007Blue;
                c1Payment.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                c1Payment.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                c1Payment.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);

                c1Payment.ExtendLastCol = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                c1Payment.Redraw = true;
                c1SinglePayment.TabStop = false;
            }
        }
          
        #endregion

        private void frmInsurancePaymentV2_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                RemoveGotFocusListener(this);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ReleaseLockStatus(ClaimDetails.TransactionMasterID);
            }
        }

        private void btnViewTakeBack_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmInsuranceTakebackV2 ofrmInsuranceTakebackV2 = new frmInsuranceTakebackV2(EOBPaymentID))
                {
                    if (ofrmInsuranceTakebackV2.EOBTakeback != null && ofrmInsuranceTakebackV2.EOBTakeback.Rows.Count > 0)
                    {
                        ofrmInsuranceTakebackV2.ShowInTaskbar = false;
                        ofrmInsuranceTakebackV2.StartPosition = FormStartPosition.CenterScreen;
                        ofrmInsuranceTakebackV2.ShowDialog(this);
                    }
                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
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
        //Insurance Payment None action restriction changes
        private void c1SinglePayment_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (e.Col == COL_PARTY)
            {
                if (Convert.ToString(c1SinglePayment.GetData(e.Row, COL_NEXT)) == "N-NONE")
                {
                   e.Cancel = true;
                }
            }
        }

        private void c1SinglePaymentCorrTB_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (e.Col == COL_PARTY)
            {
                if (Convert.ToString(c1SinglePaymentCorrTB.GetData(e.Row, COL_NEXT)) == "N-NONE")
                {
                    e.Cancel = true;
                }
            }
        }

        private void tls_FollowUpActionDate_Click(object sender, EventArgs e)
        {
            if (ClaimDetails.IsClaimExist)
            {
                try
                {
                    GetControlSelection();
                    frmSetupFollowupDateAction objFollowup = new frmSetupFollowupDateAction(CollectionEnums.FollowUpType.Claim, PatientID, nPAccountID, ClaimDetails.TransactionID, nLastTransactionMasterID);
                    objFollowup.CallingContainer = this;
                    objFollowup.ShowDialog(this);
                    SetLastFollowUP();
                    objFollowup.Dispose();
                    objFollowup = null;
                }
                catch (Exception exFollowUpActionDate)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(exFollowUpActionDate.ToString(), false);
                }
                finally
                {
                    SetControlSelection();
                }
            }
            else
            {
                MessageBox.Show("Please select the claim", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SetLastFollowUP()
        {

            DataSet dsClaimFollowUp = null;
            //  DataTable dtAccountNotes = null;
            DataTable dtClaimNote = null;
            DataTable dtClaimFollowUp = null;
            try
            {
                if (CL_FollowUpCode.IsFollowUpFeatureON())
                {
                    dsClaimFollowUp = CL_FollowUpCode.GetClaimFollowUp(ClaimDetails.TransactionMasterID, ClaimDetails.TransactionID);
                    if (dsClaimFollowUp != null && dsClaimFollowUp.Tables.Count > 0)
                    {
                        dtClaimNote = dsClaimFollowUp.Tables[0];
                        dtClaimFollowUp = dsClaimFollowUp.Tables[1];
                    }
                    if (dtClaimNote != null && dtClaimNote.Rows.Count > 0)
                    {
                        if (Convert.ToString(dtClaimNote.Rows[0]["Note"]) != "")
                        {
                            pnlClaimNote.Visible = true;
                            lblClaimNote.Text = dtClaimNote.Rows[0]["Note"].ToString();
                            toolTip1.SetToolTip(lblClaimNote, SplitToolTip(dtClaimNote.Rows[0]["Note"].ToString()));
                        }
                        else
                        {
                            pnlClaimNote.Visible = false;
                            lblClaimNote.Text = "";
                            toolTip1.SetToolTip(lblClaimNote, "");
                        }
                    }
                    else
                    {
                        pnlClaimNote.Visible = false;
                        lblClaimNote.Text = "";
                        toolTip1.SetToolTip(lblClaimNote, "");
                    }
                    if (dtClaimFollowUp != null && dtClaimFollowUp.Rows.Count > 0)
                    {
                        if (Convert.ToString(dtClaimFollowUp.Rows[0]["sFollowupDescription"]) != "")
                        {
                            pnlClaimFollowUp.Visible = true;
                            lblClaimFollowup.Text = dtClaimFollowUp.Rows[0]["sFollowupDescription"].ToString();
                            toolTip1.SetToolTip(lblClaimFollowup, SplitToolTip(dtClaimFollowUp.Rows[0]["sFollowupDescription"].ToString()));
                            if (Convert.ToDateTime(dtClaimFollowUp.Rows[0]["dtClaimFollowUpDate"].ToString()) <= DateTime.Now)
                            {
                                lblClaimFollowup.Font = gloGlobal.clsgloFont.getFontFromExistingSource(lblClaimFollowup.Font, FontStyle.Bold);
                                lblClaimFollowup.ForeColor = System.Drawing.Color.Maroon;
                            }
                            else
                            {
                                lblClaimFollowup.ForeColor = System.Drawing.Color.Black;
                            }
                        }
                        else
                        {
                            pnlClaimFollowUp.Visible = false;
                            lblClaimFollowup.Text = "";
                            toolTip1.SetToolTip(lblClaimFollowup, "");
                        }
                    }
                    else
                    {
                        pnlClaimFollowUp.Visible = false;
                        lblClaimFollowup.Text = "";
                        toolTip1.SetToolTip(lblClaimFollowup, "");
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); 
            }

            finally
            {
                if (dsClaimFollowUp != null)
                {
                    dsClaimFollowUp.Dispose();
                    dsClaimFollowUp = null;
                }

                if (dtClaimNote != null)
                {
                    dtClaimNote.Dispose();
                    dtClaimNote = null;
                }

                if (dtClaimFollowUp != null)
                {
                    dtClaimFollowUp.Dispose();
                    dtClaimFollowUp = null;
                }
               
                  
            }



        }

        private void tsbViewHistory_Click(object sender, EventArgs e)
        {
            gloBilling.gloBilling ogloBilling = null;

            try
            {
                GetControlSelection();
                if (ClaimDetails.IsClaimExist)
                {
                    if (ClaimDetails.TransactionID != 0)
                    {
                        // Set the transactionID for the latest claim 
                        ogloBilling = new gloBilling.gloBilling(gloGlobal.gloPMGlobal.DatabaseConnectionString, gloGlobal.gloPMGlobal.DatabaseConnectionString);
                        nTransactionID = ogloBilling.GetLastTransactionID(ClaimDetails.TransactionID);
                        //---------------------------------------------------------------
                        if (nTransactionID > 0)
                        {
                            frmClaimChargeHistoryV2 ofrmClaimChargeHistory = new frmClaimChargeHistoryV2(gloGlobal.gloPMGlobal.DatabaseConnectionString, ClaimDetails.PatientID, 1, nTransactionID,true);
                            ofrmClaimChargeHistory.StartPosition = FormStartPosition.CenterScreen;
                            ofrmClaimChargeHistory.CallingContainer = this.Name;
                            ofrmClaimChargeHistory.ShowDialog(this);
                            ofrmClaimChargeHistory.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("Please select the claim", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select the claim", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;

            }
            finally
            {
                if (ogloBilling != null) { ogloBilling.Dispose(); ogloBilling = null; }
                SetControlSelection();
            }
        }


        private DataTable FillReasonCodeForPayment(long TrackTrnId, long TrackTrnDtlId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            string _sqlQuery = "";
            object _retVal = null;
            //bool _isActiveTray = false;
            DataTable dtResult = new DataTable();
            try
            {
                _sqlQuery = "SELECT ER.sReasonCOde,ER.sReasonDescription,ER.dReasonAmount,Er.nType,ER.nTrackTrnLineNo " +
                            "FROM   BL_EOB_ReasonCodes ER inner join BL_Transaction_Claim_Lines CL " +
                            "ON     ER.nTrackTrnId=CL.nParentTransactionID and " +
                            "ER.nTrackTrnDtlId= CL.nParentTransactionDetailID " +
                            "WHERE CL.nTransactionId=" + TrackTrnId + " and Cl.nTransactionDetailID=" + TrackTrnDtlId + " " +
                            "ORDER BY ER.nTrackTrnLineNo";

                oDB.Connect(false);
                oDB.Retrive_Query(_sqlQuery, out dtResult);
                oDB.Disconnect();
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
            return dtResult;
        }

        private void SetAdjustmentResoncode(string ReasonCodeType)
        {
            Int64 _billingTranId = 0;
            Int64 _billingTranDtlId = 0;
            Int64 _claimNo = 0;
            Int64 _TrackbillingTranId = 0;
            Int64 _TrackbillingTranDtlId = 0;
            string _SubclaimNo = "";
            int _rowIndex = 0;
            int ReasonCodeSetup = 0;
            C1.Win.C1FlexGrid.C1FlexGrid oReasonFlex = new C1FlexGrid();
            oReasonFlex.Rows.Count = 0;
            oReasonFlex.Cols.Count = 0;
            oReasonFlex.Rows.Fixed = 0;
            oReasonFlex.Cols.Fixed = 0;

            try
            {
                ReasonCodeSetup = GetInsurancePaymentResonCodeSetupSetting();
                ReasonCodeSetup = 2;
                if (ReasonCodeSetup == 2 || ReasonCodeSetup == 3)
                {

                    _rowIndex = c1SinglePayment.RowSel;

                    if (c1SinglePayment.GetData(_rowIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(_rowIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                    {
                        if (c1SinglePayment.GetData(_rowIndex, COL_CLAIMNO) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_CLAIMNO)).Trim() != "")
                        { _claimNo = Convert.ToInt64(c1SinglePayment.GetData(_rowIndex, COL_CLAIMNO)); }

                        if (c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_ID) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_ID)).Trim() != "")
                        { _billingTranId = Convert.ToInt64(c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_ID)); }

                        if (c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_DETAILID) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_DETAILID)).Trim() != "")
                        { _billingTranDtlId = Convert.ToInt64(c1SinglePayment.GetData(_rowIndex, COL_BILLING_TRANSACTON_DETAILID)); }

                        if (c1SinglePayment.GetData(_rowIndex, COL_SUBCLAIMNO) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_SUBCLAIMNO)).Trim() != "")
                        { _SubclaimNo = Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_SUBCLAIMNO)); }

                        if (c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_ID) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_ID)).Trim() != "")
                        { _TrackbillingTranId = Convert.ToInt64(c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_ID)); }

                        if (c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID) != null && Convert.ToString(c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID)).Trim() != "")
                        { _TrackbillingTranDtlId = Convert.ToInt64(c1SinglePayment.GetData(_rowIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID)); }


                        frmEOBPaymentReasonCode ofrmEOBPaymentReasonCode = new frmEOBPaymentReasonCode(AppSettings.ConnectionStringPM, _claimNo, _billingTranId, _billingTranDtlId, null);
                        ofrmEOBPaymentReasonCode.SubClaimNo = _SubclaimNo;
                        ofrmEOBPaymentReasonCode.TrackBillingTransactionID = _TrackbillingTranId;
                        ofrmEOBPaymentReasonCode.TrackBillingTransactionDetailID = _TrackbillingTranDtlId;
                        ofrmEOBPaymentReasonCode.DesignGrid(ofrmEOBPaymentReasonCode.ReasonGrid);
                        ofrmEOBPaymentReasonCode.ReasonCodeType = ReasonCodeType;

                        if (ReasonCodeSetup == 2)
                        {
                            ofrmEOBPaymentReasonCode.ReasonCodeSetup = "PayerSetup";
                            DataTable _dtERAPayerSetup = GetPayerSetUp(ReasonCodeType);
                            if (_dtERAPayerSetup != null && _dtERAPayerSetup.Rows.Count > 1)
                            {
                                ofrmEOBPaymentReasonCode.dtERAPayerSetup = _dtERAPayerSetup;
                                ofrmEOBPaymentReasonCode.ShowDialog(this);
                            }
                            else
                            {
                                ofrmEOBPaymentReasonCode.FrmDlgRst = DialogResult.Abort;
                            }
                        }
                        else if (ReasonCodeSetup == 3)
                        {
                            ofrmEOBPaymentReasonCode.ReasonCodeSetup = "Manual";
                            ofrmEOBPaymentReasonCode.ShowDialog(this);
                        }

                        if (ofrmEOBPaymentReasonCode.FrmDlgRst == DialogResult.OK)
                        {
                            oReasonFlex = new C1FlexGrid();
                            ofrmEOBPaymentReasonCode.DesignGrid(oReasonFlex);


                            C1.Win.C1FlexGrid.Column col = oReasonFlex.Cols.Add();
                            col.DataType = typeof(String);
                            col.Caption = "Type";
                            col.Name = "Type";
                            if (ofrmEOBPaymentReasonCode.ReasonGrid.Rows.Count > 0)
                            {
                                for (int rInd = 1; rInd < ofrmEOBPaymentReasonCode.ReasonGrid.Rows.Count; rInd++)
                                {
                                    int row = 0;
                                    row = oReasonFlex.Rows.Add().Index;

                                    for (int cInd = 0; cInd < ofrmEOBPaymentReasonCode.ReasonGrid.Cols.Count; cInd++)
                                    {
                                        oReasonFlex.SetData(row, cInd, ofrmEOBPaymentReasonCode.ReasonGrid.GetData(rInd, cInd));
                                    }

                                    oReasonFlex.SetData(row, oReasonFlex.Cols["Type"].Index, "Reason");
                                    oReasonFlex.SetData(row, oReasonFlex.Cols["ReasonCodeType"].Index, ReasonCodeType);
                                }
                            }
                            c1SinglePayment.SetData(_rowIndex, COL_REASON, oReasonFlex);
                            c1SinglePayment.SetData(_rowIndex, COL_LINE_STATEMENTNOTE, ofrmEOBPaymentReasonCode.StatementNote.Trim());
                            c1SinglePayment.SetData(_rowIndex, COL_LINE_STATEMENTNOTEONPRINT, ofrmEOBPaymentReasonCode.IncludeStatmentNoteOnPrint);
                            c1SinglePayment.SetData(_rowIndex, COL_LINE_INTERNALNOTE, ofrmEOBPaymentReasonCode.InternalNote.Trim());
                            c1SinglePayment.SetData(_rowIndex, COL_LINE_INTERNALNOTEONPRINT, ofrmEOBPaymentReasonCode.IncludeInternalNoteOnPrint);

                        }

                        ofrmEOBPaymentReasonCode.Dispose();

                    }
                }

            }
            catch(Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private DataTable GetStandardReasonCodeSetUp(string ReasonCodeType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            DataTable _dtStandardSetup = null;
            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@Flag", 0, ParameterDirection.Input, SqlDbType.Int);
                switch (ReasonCodeType)
                {
                    case "W/O": oParameters.Add("@nCASType", gloBilling.gloERA.enum_CASReasonType.WO.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); break;
                    case "Copay": oParameters.Add("@nCASType", gloBilling.gloERA.enum_CASReasonType.Copay.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); break;
                    case "Deduct": oParameters.Add("@nCASType", gloBilling.gloERA.enum_CASReasonType.Deduct.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); break;
                    case "Co-ins": oParameters.Add("@nCASType", gloBilling.gloERA.enum_CASReasonType.Coins.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); break;
                    case "Withhold": oParameters.Add("@nCASType", gloBilling.gloERA.enum_CASReasonType.WH.GetHashCode(), ParameterDirection.Input, SqlDbType.Int); break;
                }
                oDB.Retrive("BL_SELECT_Standard_ReasonCode", oParameters, out _dtStandardSetup);
                oDB.Disconnect();
                return _dtStandardSetup;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
                return _dtStandardSetup;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return _dtStandardSetup;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (_dtStandardSetup != null) { _dtStandardSetup.Dispose(); }
            }
        }

        private DataTable GetPayerSetUp(string ReasonCodeType)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloGlobal.gloPMGlobal.DatabaseConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

            DataTable _dtPayerSetup = null;
            try
            {
                oDB.Connect(false);
                oParameters.Clear();

                oParameters.Add("@nContactID", ContactInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);
                if (ReasonCodeType == "W/O")
                {
                    oParameters.Add("@nCASType", gloBilling.gloERA.enum_CASReasonType.WO.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                }
                if (ReasonCodeType == "Copay")
                {
                    oParameters.Add("@nCASType", gloBilling.gloERA.enum_CASReasonType.Copay.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                }
                if (ReasonCodeType == "Deduct")
                {
                    oParameters.Add("@nCASType", gloBilling.gloERA.enum_CASReasonType.Deduct.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                }
                if (ReasonCodeType == "Co-ins")
                {
                    oParameters.Add("@nCASType", gloBilling.gloERA.enum_CASReasonType.Coins.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                }
                if (ReasonCodeType == "Withhold")
                {
                    oParameters.Add("@nCASType", gloBilling.gloERA.enum_CASReasonType.WH.GetHashCode(), ParameterDirection.Input, SqlDbType.Int);
                }

                oDB.Retrive("BL_SELECT_PayerSetUp_ReasonCode", oParameters, out _dtPayerSetup);
                oDB.Disconnect();
                return _dtPayerSetup;
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.Message);
                return _dtPayerSetup;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return _dtPayerSetup;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oParameters != null) { oParameters.Dispose(); }
                if (_dtPayerSetup != null) { _dtPayerSetup.Dispose(); }
            }
        }

        private int GetInsurancePaymentResonCodeSetupSetting()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            DataTable dtSyncPatient = null;
            try
            {
                oDB.Connect(false);
                string query = "Select sSettingsValue as SettingsValue from Settings where sSettingsName ='InsurancePaymentResoneCodeSetup'";// and nUserid=" + gloGlobal.gloPMGlobal.UserID + "";
                oDB.Retrive_Query(query, out dtSyncPatient);
                if (dtSyncPatient.Rows.Count > 0 && dtSyncPatient.Rows[0]["SettingsValue"].ToString() != "")
                {
                    oDB.Disconnect();
                    return Convert.ToInt32(dtSyncPatient.Rows[0]["SettingsValue"]);
                }
                else
                {
                    oDB.Disconnect();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                return 0;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }

        private string GetDefultReasonCodeByType(int CASType)
        {
            object Value = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseConnectionString);
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

        private void lblAlertMessage_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(lblAlertMessage, lblAlertMessage.Text);
        }

        private Boolean GetSetting(dynamic SettingName)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(gloPMGlobal.DatabaseConnectionString);
            DataTable dtSetting = null;
            try
            {
                oDB.Connect(false);
                string query = "Select sSettingsValue as SettingsValue from Settings where sSettingsName ='" + SettingName + "'";// and nUserid=" + gloGlobal.gloPMGlobal.UserID + "";
                oDB.Retrive_Query(query, out dtSetting);
                if (dtSetting.Rows.Count > 0 && dtSetting.Rows[0]["SettingsValue"].ToString() != "")
                {
                    oDB.Disconnect();
                    return Convert.ToBoolean(dtSetting.Rows[0]["SettingsValue"]);
                }
                else
                {
                    oDB.Disconnect();
                    return false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex, false);
                return false;
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
        }
        private void PatientControl_OnLoadViewBenefit(Int64 _nPatientID, Int64 _InsuranceId, string _databaseConnectionString)
        {
            gloPMGeneral.frmViewBenefit ofrm = new gloPMGeneral.frmViewBenefit(_nPatientID, _InsuranceId, _databaseConnectionString);
            ofrm.StartPosition = FormStartPosition.CenterParent;
            ofrm.ShowDialog(this);
            ofrm.Dispose();
            ofrm = null;
            LoadClaim();
        }
        //#region Cursur Jumping
     
        //private string SelectedControl = "";
        //private int latestRowSel = 0;
        //private int latestColSel = 0;

        //private void AddGotFocusListener(Control ctrl)
        //{
        //    try
        //    {
        //        foreach (Control c in ctrl.Controls)
        //        {
        //            c.GotFocus += new EventHandler(Control_GotFocus);
        //            if (c.Controls.Count > 0)
        //            {
        //                AddGotFocusListener(c);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //}

        //private void RemoveGotFocusListener(Control ctrl)
        //{
        //    try
        //    {
        //        foreach (Control c in ctrl.Controls)
        //        {

        //            c.GotFocus -= new EventHandler(Control_GotFocus);
        //            if (c.Controls.Count > 0)
        //            {
        //                RemoveGotFocusListener(c);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //}

        //private void SetControlSelection()
        //{
        //    Control[] oSeleced = null;
        //    try
        //    {
        //        oSeleced = this.Controls.Find(SelectedControl, true);
        //        if (oSeleced.Length > 0)
        //        {
        //            if (oSeleced[0].Name == c1SinglePayment.Name)
        //            {
        //                c1SinglePayment.Focus();
        //                c1SinglePayment.Select(latestRowSel, latestColSel, true);
        //            }
        //            else if (oSeleced[0].Name == c1SinglePaymentCorrTB.Name)
        //            {
        //                c1SinglePaymentCorrTB.Focus();
        //                c1SinglePaymentCorrTB.Select(latestRowSel, latestColSel, true);
        //            }
        //            else if (oSeleced[0].Name == c1MultiplePayment.Name)
        //            {
        //                c1MultiplePayment.Focus();
        //                c1MultiplePayment.Select(latestRowSel, latestColSel, true);
        //            }
        //            else
        //            {
        //                oSeleced[0].Focus();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        oSeleced = null;
        //    }
        //}

        //private void GetControlSelection()
        //{
        //    Control[] oSeleced = null;

        //    try
        //    {
        //        oSeleced = this.Controls.Find(SelectedControl, true);
        //        if (oSeleced.Length > 0)
        //        {
        //            if (oSeleced[0].Name == c1SinglePayment.Name)
        //            {
        //                latestRowSel = c1SinglePayment.RowSel;
        //                latestColSel = c1SinglePayment.ColSel;
        //            }
        //            else if (oSeleced[0].Name == c1SinglePaymentCorrTB.Name)
        //            {
        //                latestRowSel = c1SinglePaymentCorrTB.RowSel;
        //                latestColSel = c1SinglePaymentCorrTB.ColSel;
        //            }
        //            else if (oSeleced[0].Name == c1MultiplePayment.Name)
        //            {
        //                latestRowSel = c1MultiplePayment.RowSel;
        //                latestColSel = c1MultiplePayment.ColSel;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        oSeleced = null;
        //    }
        //}

        //public override void Control_GotFocus(object obj, EventArgs e)
        //{
        //    // Set focused control here
        //    Control oCntrl = null;
        //    try
        //    {

        //        oCntrl = (Control)obj;
        //        if (oCntrl.GetType() == typeof(TextBox))
        //        {
        //            if (oCntrl.Name != txtClaimRemittanceRef.Name)
        //            {
        //                base.SelectedControl = oCntrl;
        //            }
        //        }
        //        else if (oCntrl.GetType() == typeof(C1.Win.C1FlexGrid.C1FlexGrid) ||
        //                oCntrl.GetType() == typeof(ComboBox) ||
        //                oCntrl.GetType() == typeof(DateTimePicker) ||
        //                oCntrl.GetType() == typeof(RichTextBox) ||
        //                oCntrl.GetType() == typeof(MaskedTextBox) ||
        //                oCntrl.GetType() == typeof(CheckBox) ||
        //                oCntrl.GetType() == typeof(DataGrid))
        //        {
        //            base.SelectedControl = oCntrl;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    finally
        //    {
        //        oCntrl = null;
        //    }
        //}
        
        //#endregion
    }
}
