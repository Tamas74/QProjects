using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using gloSettings;
using gloPatientStripControl;
using gloDateMaster;
using gloBilling.EOBPayment.Common;
using System.Data.SqlClient;
using gloStrips;

namespace gloBilling.Payment
{
    /// <summary>
    /// TO DO
    /// 1) IsClaimLoaded : Use the property instead of checking (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
    /// 2) 
    /// </summary>
    public partial class frmInsurancePayment : Form
    {
        #region " Variable Declarations "

        // Used to reset if allowed amount invalid
        decimal _allowedAmountBeforeEdit = 0;
        bool _IsValidEntered = true;

        // Patient strip control 
        //gloPatientStripControl.ucPatientStripControl PatientControl = null;//previous code
        //Added by Subashish_b on 16/March /2011 (integration made on date-10/May/2011) for declaring object for new patient banner and comenting old
        gloStripControl.ucPatientStripControl PatientControl = null;
        //End

        // Used to show Formatted Payment reference number
        const string _paymentPrefix = "GPM#";

        // Needs to be refactor - TO DO
        EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines EOBInsurancePaymentMasterLines = new EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines();
        EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines EOBInsurancePaymentMasterAllocationLines = new EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines();
        EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines EOBInsuranceReserveMasterLines = new EOBPayment.Common.EOBInsurancePaymentMasterAllocationLines();
        EOBPayment.Common.EOBInsurancePaymentSelectedCreditLines SelectedReserveMasterLines = new EOBPayment.Common.EOBInsurancePaymentSelectedCreditLines();
        EOBPayment.Common.InsuranceReserveRemainingDetails _ReserveDetails = new EOBPayment.Common.InsuranceReserveRemainingDetails();

        //Added by Subashish_b on 16/March /2011 (integration made on date-10/May/2011) for  storing the PAF information from the patient banner control
        private Int64 nPAccountID = 0;
        private Int64 nGuarantorID = 0;
        private Int64 nAccountPatientID = 0;
        // End

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
        const int COL_CHARGE = 21;
        const int COL_UNIT = 22;
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

        const int COL_PAYMENTTYPE = 60;
        const int COL_PAYMENTSUBTYPE = 61;
        //const int COL_CREATEDDATETIME = 62;
        const int COL_COUNT = 62;

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

        bool _IsPendingCheckLoaded = false;
        public bool IsPendingCheckLoaded
        {
            get { return _IsPendingCheckLoaded; }
            set { _IsPendingCheckLoaded = value; }
        }

        private enum ColServiceLineType
        {
            None = 0, Claim = 1, ServiceLine = 2, Patient = 3
        }

        #endregion

        #region " Delegates & Events "

        public delegate void CalculationChanged();
        public event CalculationChanged OnRemainingCalculationChanged;
      //  public event CalculationChanged OnRemainingCalculationChangedCorrTB;

        #endregion

        #region " Property Procedures "

        private Int64 _EOBPaymentID = 0;
        private Int64 _insuranceCompanyID = 0;
        private string _insuranceCompanyName = string.Empty;
        private string _insurancePlanName = string.Empty;
        private bool _IsSelectedPlanOnHold = false;
        private Int64 _contactInsuranceID = 0;
        private Int64 _patientInsuranceID = 0;
        private bool _IsReserveAdded = false;
        private bool _IsReserveUsed = false;
        private decimal _AmountTakenFromReserve = 0;
        private decimal _AmountAddedToReserve = 0;
        private gloGeneralItem.gloItems _oSeletedReserveItems = new gloGeneralItem.gloItems();
        private string _ReserveNote = string.Empty;
        private Int64 _SelectedTrayID = 0;
        private string _SelectedTrayName = "";
        private string _SelectedTrayCode = "";
        private decimal _CheckAmount = 0;
        private decimal _TakeBackAmount = 0;
        private decimal _TotalFundsAvaiable = 0;
        private decimal _AmountApplied = 0;
        private decimal _TotalFundsRemaining = 0;
        private decimal _ReservesApplied = 0;
        private FormMode _formAction = FormMode.NewPaymentMode;
        private EOBPaymentMode _SelectedEOBPaymentMode = EOBPaymentMode.None;
        private SplitClaimDetails _ClaimDetails = new SplitClaimDetails();
        bool _ResetCheckAmount;
        private string _PaymentCloseDate = "";
        string _NewOpenDate = "";
        private string _OriginalCloseDate = "";
        private bool _isOriginalPayment = false;
        private decimal _OriginalCheckAmount = 0;
        decimal _TotalAllocation = 0;
        

        //bool _IsFormLoading = false;

        //public bool IsFormLoading
        //{
        //    get { return _IsFormLoading; }
        //    set { _IsFormLoading = value; }
        //}
       
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

        //private decimal CheckAmount
        //{
        //    get
        //    {
        //        if (txtCheckAmount.Text != "")
        //        { _CheckAmount = Convert.ToDecimal(txtCheckAmount.Text); }

        //        return _CheckAmount;
        //    }
        //    set
        //    {
        //        _CheckAmount = value;
        //        OnRemainingCalculationChanged();
        //        txtCheckAmount.Text = _CheckAmount.ToString("#0.00");
        //    }
        //}

        private decimal CheckAmount
        {
            get
            {
                if (txtCheckAmount.Text != "" )
                {
                    try
                    { _CheckAmount = Convert.ToDecimal(txtCheckAmount.Text); }
                    catch
                    {  _CheckAmount = 0;                       
                    }
                    
                }

                return _CheckAmount;
            }
            set
            {
                _CheckAmount = value;
                OnRemainingCalculationChanged();
                if (txtCheckAmount.Text != "" )
                {
                    txtCheckAmount.Text = _CheckAmount.ToString("#0.00");
                }
                else if(IsPendingCheckLoaded== true)
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


                if (PaymentAction == FormMode.NewPaymentMode)
                {
                    if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                    {
                        for (int rIndex = 1; rIndex < c1SinglePayment.Rows.Count; rIndex++)
                        {
                            if (c1SinglePayment.GetData(rIndex, COL_PAYMENT) != null
                               && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_PAYMENT)).Trim() != "")
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
                            if (c1SinglePaymentCorrTB.GetData(rIndex, COL_PAYMENT) != null
                               && Convert.ToString(c1SinglePaymentCorrTB.GetData(rIndex, COL_PAYMENT)).Trim() != "")
                            {
                                _IsPaymentAllocated = true;
                                break;
                            }
                        }
                    }
                }

                return _IsPaymentAllocated;
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
                            if ((c1SinglePayment.GetData(rIndex, COL_NEXT) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_NEXT)).Trim() != "")
                            || (c1SinglePayment.GetData(rIndex, COL_PARTY) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_PARTY)).Trim() != ""))
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

        private EOBPaymentMode SelectedPaymentMode
        {
            set { _SelectedEOBPaymentMode = value; }
            get
            {
                //if (cmbPayMode.Text != "")
                //{
                //    if (cmbPayMode.Text.Trim() == EOBPaymentMode.None.ToString())
                //    { _SelectedEOBPaymentMode = EOBPaymentMode.None; }
                //    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.Cash.ToString())
                //    { _SelectedEOBPaymentMode = EOBPaymentMode.Cash; }
                //    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.Check.ToString())
                //    { _SelectedEOBPaymentMode = EOBPaymentMode.Check; }
                //    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.MoneyOrder.ToString())
                //    { _SelectedEOBPaymentMode = EOBPaymentMode.MoneyOrder; }
                //    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.CreditCard.ToString())
                //    { _SelectedEOBPaymentMode = EOBPaymentMode.CreditCard; }
                //    else if (cmbPayMode.Text.Trim() == EOBPaymentMode.EFT.ToString())
                //    { _SelectedEOBPaymentMode = EOBPaymentMode.EFT; }
                //}
                return _SelectedEOBPaymentMode;
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
                //else
                //{ lblAlertMessage.BringToFront(); }
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

        public EOBPayment.Common.InsuranceReserveRemainingDetails ReserveDetails
        {

            get { return _ReserveDetails; }
            set { _ReserveDetails = value; }

        }

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
                    IsReserveUsed = true;
                    if (_AmountTakenFromReserve == 0) { lblReserveAmount.Text = string.Empty; }
                    else { lblReserveAmount.Text = _AmountTakenFromReserve.ToString("#0.00"); }
                }
                else
                {
                    IsReserveUsed = false;
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

        //Int64 _ReservePatientID = 0;
        //public Int64 ReservePatientID
        //{
        //    get { return _ReservePatientID; }
        //    set { _ReservePatientID = value; }
        //}

        //string _ReservePatientName = "";
        //public string ReservePatientName
        //{
        //    get { return _ReservePatientName; }
        //    set { _ReservePatientName = value; }
        //}

        //Int64 _nReserveMSTTransactionID = 0;
        //public Int64 ReserveMSTTransactionID
        //{
        //    get { return _nReserveMSTTransactionID; }
        //    set { _nReserveMSTTransactionID = value; }
        //}

        //Int64 _nReserveTransactionID = 0;
        //public Int64 ReserveTransactionID
        //{
        //    get { return _nReserveTransactionID; }
        //    set { _nReserveTransactionID = value; }
        //}

        //string _ReserveClaimNo = "";
        //public string ReserveClaimNo
        //{
        //    get { return _ReserveClaimNo; }
        //    set { _ReserveClaimNo = value; }
        //}
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

        #endregion

        #endregion

        #region " Constructors "

        public frmInsurancePayment()
        {
            InitializeComponent();
            OnRemainingCalculationChanged += new CalculationChanged(frmInsurancePayment_OnRemainingCalculationChanged);
           
        }

        #endregion

        #region " Form Events & Methods "

        private void frmInsurancePayment_Load(object sender, EventArgs e)
        {
            this.c1SinglePayment.EnterCell -= new System.EventHandler(this.c1SinglePayment_EnterCell);
            this.c1SinglePaymentCorrTB.EnterCell -= new System.EventHandler(this.c1SinglePayment_EnterCell);

            LoadFormData();

            Cls_TabIndexSettings tabSettings = new Cls_TabIndexSettings(this);
            tabSettings.SetTabOrder(Cls_TabIndexSettings.TabScheme.AcrossFirst);

            this.c1SinglePayment.EnterCell += new System.EventHandler(this.c1SinglePayment_EnterCell);
            this.c1SinglePaymentCorrTB.EnterCell += new System.EventHandler(this.c1SinglePaymentCorrTB_EnterCell);
        }

        private void frmInsurancePayment_OnRemainingCalculationChanged()
        {
            decimal _payment = 0;
            decimal _oldPayment = 0;

            //RowColEventArgs _args = null;

            try
            {
                #region " Calculate last payment made "

                decimal _lastPayment = 0;
                _lastPayment = GetLastPaymentMade();

                #endregion

                #region "Calculate Amount applied & Take back "

                AmountApplied = 0;
                TakeBackAmount = 0;

                if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 0)
                {
                    for (int i = 1; i <= c1SinglePayment.Rows.Count - 1; i++)
                    {
                        if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                        {
                            //if Condition Commented on 6020
                            //Reason: After Pressing Delete Button on c1SinglePayment COL_PAYMENT column Take bake Label was not updating.

                            if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && c1SinglePayment.GetData(i, COL_PAYMENT).ToString().Trim() != "")
                            {
                                bool _isCorrection = Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISCORRECTION));
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
                            //_args = new RowColEventArgs(i, c1SinglePayment.ColSel);
                            CalculateRunningBalance(i);
                        }
                    }
                }
                TakeBackAmount = _TakeBackAmount;

                #endregion

                #region " Calculate Reserve Used (during last payments made)"

              

                        decimal amt = 0;
                        if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
                        {
                            for (int index = 0; index < EOBInsurancePaymentMasterLines.Count; index++)
                            {
                                if (EOBInsurancePaymentMasterLines[index].IsReserveCreditLine == true)
                                { amt += EOBInsurancePaymentMasterLines[index].Amount; }
                            }
                        }
                        AmountTakenFromReserve = amt;
               
              

                #endregion

                // calculate available & remaining amount 
                TotalFundsAvaiable = _CheckAmount + _TakeBackAmount + _AmountTakenFromReserve;
                TotalFundsRemaining = _TotalFundsAvaiable - (_AmountApplied + _lastPayment) - _AmountAddedToReserve;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            //finally
            //{
            //    _args = null;  
            //}
        }



        private decimal GetLastPaymentMade()
        {
            decimal _lastPayment = 0;
            System.Collections.Hashtable oList = new System.Collections.Hashtable();
            if (c1MultiplePayment != null && c1MultiplePayment.Rows.Count > 1)
            {
                Int64 _billingTransactionID = 0;
                Int64 _billingTransactionDetailID = 0;

                int _payType = 0;
                int _paySubType = 0;

                bool _addLastPay = false;

                for (int i = 1; i <= c1MultiplePayment.Rows.Count - 1; i++)
                {
                    if (c1MultiplePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1MultiplePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                    {
                        if (c1MultiplePayment.GetData(i, COL_PAYMENT) != null && c1MultiplePayment.GetData(i, COL_PAYMENT).ToString().Trim() != "")
                        {
                            _billingTransactionID = Convert.ToInt64(c1MultiplePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                            _billingTransactionDetailID = Convert.ToInt64(c1MultiplePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));

                            _payType = Convert.ToInt16(c1MultiplePayment.GetData(i, COL_PAYMENTTYPE));
                            _paySubType = Convert.ToInt16(c1MultiplePayment.GetData(i, COL_PAYMENTSUBTYPE));

                            if (oList.Count == 0)
                            {
                                _addLastPay = true;
                            }
                            else if (_payType == EOBPaymentType.InsuraceReserverd.GetHashCode() && _paySubType == EOBPaymentSubType.Reserved.GetHashCode())
                            {
                                _addLastPay = true;
                            }
                            else if (oList.ContainsKey(_billingTransactionDetailID) && oList.ContainsValue(_billingTransactionID))
                            {
                                _addLastPay = false;
                            }
                            else
                            {
                                _addLastPay = true;
                            }

                            if (_addLastPay)
                            {
                                _lastPayment += Convert.ToDecimal(c1MultiplePayment.GetData(i, COL_LAST_PAYMENT));

                                //if (!oList.ContainsKey(_billingTransactionDetailID) && oList.ContainsValue(_billingTransactionID))
                                if (!oList.ContainsKey(_billingTransactionDetailID))
                                {
                                    oList.Add(_billingTransactionDetailID, _billingTransactionID);
                                }

                                _addLastPay = false;
                            }
                        }
                    }
                }
            }
            return _lastPayment;
        }

        //private void MoveCursor(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (sender.GetType() == typeof(System.Windows.Forms.Button) && ((Button)sender).Name.ToUpper() == btnSearchInsuranceCompany.Name.ToUpper())
        //        { txtCheckAmount.Select();txtCheckAmount.Focus(); }
        //        else if (sender.GetType() == typeof(System.Windows.Forms.TextBox) && ((TextBox)sender).Name.ToUpper() == txtCheckAmount.Name.ToUpper())
        //        { cmbPayMode.Select(); cmbPayMode.Focus(); }
        //        else if (sender.GetType() == typeof(System.Windows.Forms.ComboBox) && ((ComboBox)sender).Name.ToUpper() == cmbPayMode.Name.ToUpper())
        //        { txtCheckNumber.Select(); txtCheckNumber.Focus(); }
        //        else if (sender.GetType() == typeof(System.Windows.Forms.TextBox) && ((TextBox)sender).Name.ToUpper() == txtCheckNumber.Name.ToUpper())
        //        { mskCheckDate.Select(); mskCheckDate.Focus(); }
        //        else if (sender.GetType() == typeof(System.Windows.Forms.MaskedTextBox) && ((MaskedTextBox)sender).Name.ToUpper() == mskCheckDate.Name.ToUpper())
        //        { txtPayMstNotes.Select(); txtPayMstNotes.Focus(); }
        //        else if (sender.GetType() == typeof(System.Windows.Forms.TextBox) && ((TextBox)sender).Name.ToUpper() == txtPayMstNotes.Name.ToUpper())
        //        { if (PatientControl != null) { PatientControl.SelectSearchBox(); } }

        //        //else if (sender.GetType() == typeof(System.Windows.Forms.TextBox) && ((TextBox)sender).Name.ToUpper() == txtPayMstNotes.Name.ToUpper())
        //        //{ if (PatientControl != null) { PatientControl.Select(); PatientControl.Focus(); PatientControl.SelectSearchBox(); } }

        //    }
        //}

        private void LoadFormData()
        {
            SetPaymentPrefixNumber();
            FillPaymentMode();

            EOBInsurancePaymentMasterLines.Clear();
            EOBInsurancePaymentMasterAllocationLines.Clear();
            EOBInsuranceReserveMasterLines.Clear();
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


        }

        private void LoadFormData(Int64 EOBPaymentID, Int64 EOBID, bool _IsFromSave)
        {
            if (IsPaymentInProcess)//(EOBPaymentID > 0)
            {
                bool _RetCrLine = false;
                gloBilling ogloBilling = new gloBilling(AppSettings.ConnectionStringPM, string.Empty);

                #region "Credit Lines"

                EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new global::gloBilling.EOBPayment.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);
                _RetCrLine = ogloEOBPaymentInsurance.GetEOBMasterCreditLineAndAllocationLines(EOBPaymentID, AppSettings.ClinicID, out EOBInsurancePaymentMasterLines, out EOBInsurancePaymentMasterAllocationLines);

                //Roopali ..  If use reserve is done before load pending checks 23 March 2011
                //So reserve master lines should get added to main(original) to EOBInsurancePaymentMasterLines

                if (!_IsFromSave)
                {
                    bool _isSelectedItemFoundInMasterLines = false;
                    if (EOBInsuranceReserveMasterLines != null && EOBInsuranceReserveMasterLines.Count > 0)
                    {
                        EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine oTempReserveLine = null;
                        _isSelectedItemFoundInMasterLines = false;


                        for (int lnInd = 0; lnInd < EOBInsuranceReserveMasterLines.Count; lnInd++)
                        {
                            oTempReserveLine = EOBInsuranceReserveMasterLines[lnInd];
                            _isSelectedItemFoundInMasterLines = false;
                            for (int mstLnIndex = 0; mstLnIndex < EOBInsurancePaymentMasterLines.Count; mstLnIndex++)
                            {
                                if (EOBInsurancePaymentMasterLines[mstLnIndex].PaymentType == EOBPaymentType.InsuraceReserverd
                                    && EOBInsurancePaymentMasterLines[mstLnIndex].PaymentSubType == EOBPaymentSubType.Reserved
                                    && EOBInsurancePaymentMasterLines[mstLnIndex].PaySign == EOBPaymentSign.Payment_Credit
                                    && EOBInsurancePaymentMasterLines[mstLnIndex].ReserveEOBPaymentID == oTempReserveLine.ReserveEOBPaymentID
                                    && EOBInsurancePaymentMasterLines[mstLnIndex].ReserveEOBPaymentDetailID == oTempReserveLine.ReserveEOBPaymentDetailID
                                    )
                                {
                                    _isSelectedItemFoundInMasterLines = true;
                                    EOBInsurancePaymentMasterLines[mstLnIndex].Amount += oTempReserveLine.Amount;
                                }

                            }

                            if (_isSelectedItemFoundInMasterLines == false)
                            {
                                EOBInsurancePaymentMasterLines.Add(oTempReserveLine);
                            }
                            oTempReserveLine = null;
                        }
                    }

                    if (SelectedReserveMasterLines != null && SelectedReserveMasterLines.Count > 0)
                    {
                //        bool _isSelectedItemFoundInMasterCollection = false;
                        
                        for (int mstLnIndex = 0; mstLnIndex < EOBInsurancePaymentMasterLines.Count; mstLnIndex++)
                        {
                            //_isSelectedItemFoundInMasterCollection = false;
                            for (int selResLnIndex = 0; selResLnIndex < SelectedReserveMasterLines.Count; selResLnIndex++)
                            {
                                if (EOBInsurancePaymentMasterLines[mstLnIndex].PaymentType == EOBPaymentType.InsuraceReserverd
                                    && EOBInsurancePaymentMasterLines[mstLnIndex].PaymentSubType == EOBPaymentSubType.Reserved
                                    && EOBInsurancePaymentMasterLines[mstLnIndex].PaySign == EOBPaymentSign.Payment_Credit
                                    && EOBInsurancePaymentMasterLines[mstLnIndex].ReserveEOBPaymentID == SelectedReserveMasterLines[selResLnIndex].ReserveEOBPaymentID
                                    && EOBInsurancePaymentMasterLines[mstLnIndex].ReserveEOBPaymentDetailID == SelectedReserveMasterLines[selResLnIndex].ReserveEOBPaymentDetailID
                                    )
                                {

                                    SelectedReserveMasterLines[selResLnIndex].Amount = EOBInsurancePaymentMasterLines[mstLnIndex].Amount; 

                                }
                            }
                        }
                    }

                }
                else
                {


                    #region " Check if day is closed and making use reserve payment with temp hold reserves "
                    if (txtCheckAmount.Enabled == false)
                    {
                        if (SelectedReserveMasterLines != null && SelectedReserveMasterLines.Count > 0)
                        {
                            if (EOBInsurancePaymentMasterLines != null & EOBInsurancePaymentMasterLines.Count > 0)
                            {
                                bool _isSelectedItemFoundInMasterCollection = false;

                                for (int selResLnIndex = 0; selResLnIndex < SelectedReserveMasterLines.Count; selResLnIndex++)
                                {
                                    _isSelectedItemFoundInMasterCollection = false;

                                    //if (SelectedReserveMasterLines[selResLnIndex].RemainingAmount > 0 )
                                    //{
                                        for (int mstLnIndex = 0; mstLnIndex < EOBInsurancePaymentMasterLines.Count; mstLnIndex++)
                                        {
                                            if (EOBInsurancePaymentMasterLines[mstLnIndex].PaymentType == EOBPaymentType.InsuraceReserverd
                                                && EOBInsurancePaymentMasterLines[mstLnIndex].PaymentSubType == EOBPaymentSubType.Reserved
                                                && EOBInsurancePaymentMasterLines[mstLnIndex].PaySign == EOBPaymentSign.Payment_Credit
                                                && EOBInsurancePaymentMasterLines[mstLnIndex].ReserveEOBPaymentID == SelectedReserveMasterLines[selResLnIndex].ReserveEOBPaymentID
                                                && EOBInsurancePaymentMasterLines[mstLnIndex].ReserveEOBPaymentDetailID == SelectedReserveMasterLines[selResLnIndex].ReserveEOBPaymentDetailID
                                                )
                                            {
                                                _isSelectedItemFoundInMasterCollection = true;
                                                EOBInsurancePaymentMasterLines[mstLnIndex].Amount += SelectedReserveMasterLines[selResLnIndex].RemainingAmount ;
                                            }
                                        }

                                        //..if selected item not found in master collection add it directly to the master collection
                                        if (_isSelectedItemFoundInMasterCollection == false)
                                        {
                                            EOBInsurancePaymentMasterLines.Add(SelectedReserveMasterLines[selResLnIndex]);
                                        }
                                   // }
                                }
                            }
                        }
                    }
                    #endregion " Check if day is closed and making use reserve payment with temp hold reserves "













                }
                ogloEOBPaymentInsurance.Dispose();

                #endregion

                if (_RetCrLine == true)
                {

                   

                    #region " Set Master Data "

                    DataRow _drEOBPaymentMST = InsurancePayment.GetEOBPaymentMST(EOBPaymentID);
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
                        mskCheckDate.Text = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(_drEOBPaymentMST["nCheckDate"])).ToString("MM/dd/yyyy");

                        CheckAmount = Convert.ToDecimal(_drEOBPaymentMST["nCheckAmount"]);
                        _OriginalCheckAmount = CheckAmount;
                        _OriginalCloseDate = _drEOBPaymentMST["nCloseDate"].ToString();
                        _NewOpenDate = "";
                        if (_drEOBPaymentMST["nCloseDate"].ToString() != null)
                        {

                            if (ogloBilling.IsDayClosed(Convert.ToDateTime(_drEOBPaymentMST["nCloseDate"].ToString())) == false)
                            {
                                mskCloseDate.Text = _drEOBPaymentMST["nCloseDate"].ToString();
                                _PaymentCloseDate = mskCloseDate.Text;
                                _NewOpenDate = _drEOBPaymentMST["nCloseDate"].ToString();
                            }
                            else
                            {
                                _NewOpenDate = GetNewOpenDate(EOBPaymentID);
                                if (_NewOpenDate != "" && ogloBilling.IsDayClosed(Convert.ToDateTime(_NewOpenDate)) == false)
                                {
                                    mskCloseDate.Text = _NewOpenDate;
                                    _PaymentCloseDate = mskCloseDate.Text;
                                }
                                else
                                {
                                    _IsDayCloseMsg = true; 
                                    //if (mskCloseDate.Text.Trim() != "/  /")
                                    //MessageBox.Show("Payment’s Close Date of " + _NewOpenDate + " is closed.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    //mskCloseDate.Text = _NewOpenDate;
                                }
                            }

                        }


                    }

                    #endregion " Set Master Data "

                    FillEOBPayments(EOBPaymentID, EOBID);

                    OnRemainingCalculationChanged();

                    
                    decimal _CheckRemaining = 0;
                    _CheckRemaining = Convert.ToDecimal(_drEOBPaymentMST["Remaining"].ToString());                                      
                  

                    if (_NewOpenDate != "" && ogloBilling.IsDayClosed(Convert.ToDateTime(_drEOBPaymentMST["nCloseDate"].ToString())) == true && _CheckRemaining == 0 && InsurancePayment.GetDialyCloseValidationSetting(AppSettings.ClinicID) == true)
                        txtCheckAmount.Enabled = false;
                    else
                        txtCheckAmount.Enabled = true;


                    if(_IsDayCloseMsg==true  && _IsFromSave == false )
                        MessageBox.Show("Payment’s Close Date of " + _OriginalCloseDate.ToString() + " is closed.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);


                }
                ogloBilling.Dispose();
            }
            //

        }

        private string GetNewOpenDate(long EOBPaymentID)
        {
            return InsurancePayment.GetNewOpenCloseDate(EOBPaymentID);
        }

        private void SetupControls()
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
                DesignPaymentGrid(c1MultiplePayment);
                DesignPaymentGrid(c1MultiplePaymentTotal);
            }

            DesignClaimDetailsGrid();

            //6020 - PaymentRepaymentCorrection
            SetupCorrectionTBWindow(false);
        }

        private void SetupCorrectionTBWindow(bool showvalue)
        {
            DesignPaymentGrid(c1SinglePaymentCorrTB);
            if (showvalue == true)
            {
                pnlSinglePaymentCorrTB.Visible = true;
                pnlSinglePaymentCorrTB.Height = (pnlSinglePayment.Height - c1SinglePaymentTotal.Height) / 2;
                pnlSinglePaymentAllocationHdr.Visible = true;
            }
            else
            {
                pnlSinglePaymentCorrTB.Visible = false;
                pnlSinglePaymentCorrTB.Height = 0;
                pnlSinglePaymentAllocationHdr.Visible = false;
            }
        }

        private void ResetForm()
        {

            ShowAlertMessage = false;
            EOBPaymentID = 0;

            LoadFormData();

            SelectedInsuranceCompanyID = 0;

            //CheckAmount = 0;

            // This will reset the check amount (blank text box & check amount = 0
            ResetCheckAmount = true;

            TakeBackAmount = 0;
            AmountTakenFromReserve = 0;
            AmountAddedToReserve = 0;
            AmountApplied = 0;

            txtPayMstNotes.Text = "";
            txtCheckNumber.Text = string.Empty;
            txtClaimRemittanceRef.Text = string.Empty;
            mskCheckDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");

            PaymentAction = FormMode.NewPaymentMode;
            _PaymentCloseDate = "";
            _NewOpenDate = "";
            _isOriginalPayment = false;
            ReserveDetails = new InsuranceReserveRemainingDetails();
            EOBInsuranceReserveMasterLines.Clear();
            SelectedReserveMasterLines.Clear();
            txtCheckAmount.Enabled = true;
            _OriginalCloseDate = "";
            //PatientControl.SelectSearchBox();

            //ReservePatientID = 0;
            //ReservePatientName = "";
            //ReserveClaimNo = "";
            //ReserveMSTTransactionID = 0;
            //ReserveTransactionID = 0;

        }

        #endregion

        #region " Patient Control Events "

        private void PatientControl_OnClaimNumberEntered(string ClaimText)
        {
            try
            {
                //this.c1SinglePayment.EnterCell -= new System.EventHandler(this.c1SinglePayment_EnterCell);
                //this.c1SinglePaymentCorrTB.EnterCell -= new System.EventHandler(this.c1SinglePayment_EnterCell);

                DialogResult _result = DialogResult.No;
              
                if (IsPaymentAllocated)
                { 
                   // _result = SaveChangesAlert(); 
                    _result = MessageBox.Show("Entries have been made against claim " + txtClaimNo.Text + ". Save changes before continuing?",AppSettings.MessageBoxCaption,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Information,MessageBoxDefaultButton.Button3);              
                }

                if (_result == DialogResult.No)
                {                    
                    LoadClaim();
                   
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
         
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                //this.c1SinglePayment.EnterCell += new System.EventHandler(this.c1SinglePayment_EnterCell);
                //this.c1SinglePaymentCorrTB.EnterCell += new System.EventHandler(this.c1SinglePayment_EnterCell);
            }
        }

        /// <summary>
        /// This event will triggered if PatientControl.ClearDetails() called.
        /// </summary>
        private void PatientControl_OnClearPatientDetails()
        {
            // Hide Crosswalk Option 
            chkShowCrosswalk.Visible = false;
            // Reset the claim details here 
            ClaimDetails = new SplitClaimDetails();
            // Reset the claim remittance number
            txtClaimRemittanceRef.Text = string.Empty;
            //Recalculate the amounts when claim loaded
            OnRemainingCalculationChanged();
        }

        //Parameter Changed By Debasish on 29/06/2011 while Integrating GloStrip Control from FA 
        //private void PatientControl_OnInsuranceSelected(InsuranceSelectedArgs args)
        private void PatientControl_OnInsuranceSelected(gloStripControl.InsuranceSelectedArgs args)
        {
            try
            {
                this.c1SinglePayment.EnterCell -= new System.EventHandler(this.c1SinglePayment_EnterCell);
                this.c1SinglePaymentCorrTB.EnterCell -= new System.EventHandler(this.c1SinglePayment_EnterCell);

                if (args.InsuraceSelfMode != PayerMode.Self.GetHashCode() && args.ContactID != 0)
                {
                    if (!IsPaymentInProcess)
                    {
                        // Commented following condition to resolve an issue 
                        // 0001796: Application does not load Insurance Company 
                        //if (!IsPaymentMade())
                        {
                            if (args.ContactID > 0)
                            {
                                //lblPayer.Tag = InsuranceID + "~" + InsuranceName + "~" + InsuraceSelfMode + "~" + ContactID;
                                EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new EOBPayment.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);

                                this.SelectedInsuranceCompanyID = Convert.ToInt64(ogloEOBPaymentInsurance.GetContactCompanyId(args.ContactID));
                                this.SelectedInsuranceCompany = Convert.ToString(ogloEOBPaymentInsurance.GetContactCompanyName(args.ContactID));

                                ogloEOBPaymentInsurance.Dispose();
                            }
                            else
                            {
                                //this.SelectedInsuranceCompanyID = 0;
                                //this.SelectedInsuranceCompany = string.Empty;
                                //this.ContactInsuranceID = 0;
                                //this.PatientInsuranceID = 0;
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

                    //6020 - PaymentRepaymentCorrection
                    SetupCorrectionTBWindow(false);
                }
                else if (args.ContactID != 0)
                {
                    this.SelectedInsurancePlan = args.SelectedInsurancePlan;
                   
                    // commented as showing wrong alert messages for plan hold - pankaj (14052010)
                    //if (args.IsSelectedPlanOnHold)
                    {
                        this.IsSelectedPlanOnHold = args.IsSelectedPlanOnHold;
                    }

                    // Note : 
                    // PatientInsuranceID & ContactInsuranceID  has to be set every time when insurance plan changed
                    // As these are required to set the correction mode.
                    // 
                    this.PatientInsuranceID = args.InsuranceID;
                    this.ContactInsuranceID = args.ContactID;

                    if (PatientControl != null && PatientControl.PatientID > 0 && PatientControl.ClaimNumber > 0)
                    { SetCorrectionMode(); OnRemainingCalculationChanged(); FillDataInCorrectionTBWindow(); }

                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
            finally
            {
                this.c1SinglePayment.EnterCell += new System.EventHandler(this.c1SinglePayment_EnterCell);
                this.c1SinglePaymentCorrTB.EnterCell += new System.EventHandler(this.c1SinglePayment_EnterCell);
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
                    for (int i = 0; i < pnlPatientStrip.Controls.Count; i++)
                    {
                        if (PatientControl.Name == pnlPatientStrip.Controls[i].Name)
                        {
                            pnlPatientStrip.Controls.RemoveAt(i);
                            break;
                        }
                    }
                    try
                    {
                        PatientControl.OnClaimNumberEntered -= new gloStripControl.ucPatientStripControl.ClaimNumberEntered(PatientControl_OnClaimNumberEntered);
                        PatientControl.OnInsuranceSelected -= new gloStripControl.ucPatientStripControl.InsuranceSelected(PatientControl_OnInsuranceSelected);
                        //PatientControl.OnInsuranceSelected -= new gloStripControl.ucPatientStripControl.InsuranceSelected(PatientControl_OnInsuranceSelected);
                        //PatientControl.OnPatientModified -=new ucPatientStripControl.PatientModified(PatientControl_OnPatientModified);
                        PatientControl.OnClearPatientDetails -= new gloStripControl.ucPatientStripControl.ClearPatientDetails(PatientControl_OnClearPatientDetails);
           
                    }
                    catch { }
                    PatientControl.Dispose();
                    PatientControl = null;
                }

                //Added-code modified  by Subashish_b on 16/March /2011 (integration made on date-10/May/2011) for  working new new patient banner(PAF)
                PatientControl = new gloStripControl.ucPatientStripControl();

                PatientControl.OnClaimNumberEntered += new gloStripControl.ucPatientStripControl.ClaimNumberEntered(PatientControl_OnClaimNumberEntered);
                PatientControl.OnInsuranceSelected += new gloStripControl.ucPatientStripControl.InsuranceSelected(PatientControl_OnInsuranceSelected);
                //PatientControl.OnInsuranceSelected += new gloStripControl.ucPatientStripControl.InsuranceSelected(PatientControl_OnInsuranceSelected);
                //PatientControl.OnPatientModified +=new ucPatientStripControl.PatientModified(PatientControl_OnPatientModified);
                PatientControl.OnClearPatientDetails += new gloStripControl.ucPatientStripControl.ClearPatientDetails(PatientControl_OnClearPatientDetails);
                //End
                ////////////////PatientControl = new gloPatientStripControl.ucPatientStripControl();

                ////////////////PatientControl.OnClaimNumberEntered += new gloPatientStripControl.ucPatientStripControl.ClaimNumberEntered(PatientControl_OnClaimNumberEntered);
                ////////////////PatientControl.OnInsuranceSelected += new gloPatientStripControl.ucPatientStripControl.InsuranceSelected(PatientControl_OnInsuranceSelected);
                //////////////////PatientControl.OnPatientModified +=new ucPatientStripControl.PatientModified(PatientControl_OnPatientModified);
                ////////////////PatientControl.OnClearPatientDetails += new ucPatientStripControl.ClearPatientDetails(PatientControl_OnClearPatientDetails);


                pnlPatientStrip.Controls.Add(PatientControl);

                PatientControl.Dock = DockStyle.Top;
                PatientControl.ViewSearchOptionCheckBox = false;
                PatientControl.PatientStripHeaderText = "Claim # :";
                PatientControl.AllowEditingParty = true;

                pnlSinglePayment.BringToFront();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        private void LoadClaim()
        {
            //Set ClaimDetails Property for further use
            ClaimDetails = new SplitClaimDetails(PatientControl.ClaimNumber, PatientControl.SubClaimNumber);
            //Added by Subashish_b on 16/March /2011 (integration made on date-10/May/2011) for  storing the selected PAF values in local variables
            this.nPAccountID = ClaimDetails.PAccountID;
            this.nGuarantorID = ClaimDetails.GuarantorID;
            this.nAccountPatientID = ClaimDetails.AccountPatientID;
            //End
            try
            {
                this.c1SinglePayment.EnterCell -= new System.EventHandler(this.c1SinglePayment_EnterCell);
                this.c1SinglePaymentCorrTB.EnterCell -= new System.EventHandler(this.c1SinglePaymentCorrTB_EnterCell);

                if (PatientControl.ClaimNumber > 0)
                {
                    if (IsValidClaim())
                    {
                        //PatientControl.SelectedInsuranceParty = ClaimDetails.CurrentResponsibleParty;
                        //PatientControl.FillDetails();
                        ContactInsuranceID = 0;
                        PatientInsuranceID = 0;


                        // Note : 
                        // FillPaymentGrid() method should be called after PatientControl.FillDetails() method
                        // This is requir;d for CPT Crosswalk module
                        FillPaymentGrid();

                        PatientControl.SelectedInsuranceParty = ClaimDetails.CurrentResponsibleParty;
                        PatientControl.IsRevisedPayment = false;
                        PatientControl.FillDetails(ClaimDetails.TransactionMasterID, ClaimDetails.TransactionID);

                        // set the remittance ref no
                        ClaimRemittanceReferenceNo = InsurancePayment.GetClaimRemittanceRefNo(ClaimDetails.TransactionMasterID, ContactInsuranceID, PatientInsuranceID);
                        txtClaimRemittanceRef.Focus();

                        //Added By Pramod Nair To Set the Loaded claim and Patient in the Reserve Window
                        //ReservePatientID = PatientControl.PatientID;
                        //ReservePatientName = PatientControl.PatientName;
                        //ReserveClaimNo = PatientControl.ClaimDisplayNo;
                        //ReserveMSTTransactionID = ClaimDetails.TransactionMasterID;
                        //ReserveTransactionID = ClaimDetails.TransactionID;
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

        /// <summary>
        /// This function will be automatically triggered after ContactInsuranceID is changed
        /// </summary>
        private void SetAlertMessage()
        {
            string _claim = InsurancePayment.GetFormattedClaimPaymentNumber(Convert.ToString(PatientControl.ClaimNumber));

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

        #endregion



        #region " ToolStrip Button Click Events "
        private int PaymentDialogResult = 0;
        private void tls_btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Roopali 03/12/2011
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
            try
            {
                //If the claim has never been remitted by the selected plan then
                if (PaymentAction == FormMode.NewPaymentMode)
                {
                    frmSavePaymentDialog objFrmSaveDialog;

                    if (lblReserveAmount.Text != "" && Convert.ToDecimal(lblReserveAmount.Text) > 0)
                        objFrmSaveDialog = new frmSavePaymentDialog(3);
                    else
                        objFrmSaveDialog = new frmSavePaymentDialog(1);

                    objFrmSaveDialog.ShowDialog(this);
                    PaymentDialogResult = objFrmSaveDialog.DialogResult;
                    //Pending Insurance Payment
                    if (PaymentDialogResult == 1)
                    {
                        LoadPendingCheck();
                    }
                    //New Payment
                    else if (PaymentDialogResult == 3)
                    {
                        MessageBox.Show("Enter the Payment information and reselect save.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //Original Payment
                    else if (PaymentDialogResult == 2)
                    {
                        LoadOriginalPayment();
                    }
                    if (objFrmSaveDialog != null)
                    {
                        objFrmSaveDialog.Dispose();
                        objFrmSaveDialog = null;
                    }

                }
                //If the claim has been remitted previously by the selected plan then
                else if (PaymentAction == FormMode.CorrectionMode)
                {
                    frmSavePaymentDialog objFrmSaveDialog;

                    if (lblReserveAmount.Text != "" && Convert.ToDecimal(lblReserveAmount.Text) > 0)
                        objFrmSaveDialog = new frmSavePaymentDialog(3);
                    else
                        objFrmSaveDialog = new frmSavePaymentDialog(2);
                    objFrmSaveDialog.ShowDialog(this);

                    PaymentDialogResult = objFrmSaveDialog.DialogResult;
                    //Pending Insurance Payment
                    if (PaymentDialogResult == 1)
                    {
                        LoadPendingCheck();
                    }
                    //New Payment
                    else if (PaymentDialogResult == 3)
                    {
                        MessageBox.Show("Enter the Payment information and reselect save.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //Original Payment
                    else if (PaymentDialogResult == 2)
                    {
                        LoadOriginalPayment();
                        //MessageBox.Show("Enter the Payment information and reselect a Save option.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (objFrmSaveDialog != null)
                    {
                        objFrmSaveDialog.Dispose();
                        objFrmSaveDialog = null;
                    }
                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }





        //Roopali 03/15/2011
        //To get the last insurance payment made against the claim for the selected plan.
        private void LoadOriginalPayment()
        {

            try
            {
                if (EOBInsuranceReserveMasterLines != null && EOBInsuranceReserveMasterLines.Count > 0)
                {
                    string EOBPaymentID = string.Empty;
                    for (int lnInd = 0; lnInd < EOBInsuranceReserveMasterLines.Count; lnInd++)
                    {
                        if (lnInd == 0)
                            EOBPaymentID = EOBInsuranceReserveMasterLines[lnInd].ReserveEOBPaymentID.ToString();
                        else
                            EOBPaymentID = EOBPaymentID + "," + EOBInsuranceReserveMasterLines[lnInd].ReserveEOBPaymentID;

                    }

                    DataRow DrReserveEOB;
                    DrReserveEOB = InsurancePayment.GetEOBOriginalPaymentId(EOBPaymentID);
                    //gloBilling ogloBilling = new gloBilling(AppSettings.ConnectionStringPM, string.Empty);
                    //if (ogloBilling.IsDayClosed(Convert.ToDateTime(DrReserveEOB["closedate"].ToString())) == false)
                    //{
                        _EOBPaymentID = Convert.ToInt64(DrReserveEOB["nEOBPaymentID"]);
                        IsPendingCheckLoaded = true; 
                        LoadFormData(_EOBPaymentID, 0,false );
                       
                        //_isOriginalPayment = true;                            
                    //}
                    //else
                    //{
                    //    MessageBox.Show("The Original Payment could not be selected for use with reserve because the payment’s close date has been closed. A new $0.00 payment has been created for using the reserves.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtCheckAmount.Text = "0.00";
                    //    txtCheckNumber.Text = DrReserveEOB["sCheckNumber"].ToString();
                    //    mskCheckDate.Text = DrReserveEOB["CheckDate"].ToString();
                    //}


                }
                else
                {
                    if (ClaimDetails.SubClaimNo != "")
                    _EOBPaymentID = InsurancePayment.GetEOBOriginalPaymentId(_insuranceCompanyID, ClaimDetails.TransactionMasterID, ClaimDetails.TransactionID);
                    else
                        _EOBPaymentID = InsurancePayment.GetEOBOriginalPaymentId(_insuranceCompanyID, ClaimDetails.TransactionMasterID, 0);
                    IsPendingCheckLoaded = true;
                    LoadFormData(_EOBPaymentID, 0,false );
                  
                    
                }
                _isOriginalPayment = true;
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }


        }

        private void tls_btnSaveNClose_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    bool _isPaymentSaved = PerformSavePayment();

            //    if (_isPaymentSaved)
            //    { this.Close(); }
            //}
            //catch (Exception ex)
            //{ gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        private void tls_btnNew_Click(object sender, EventArgs e)
        {

            try
            {
                DialogResult _result = DialogResult.No;

                //if (IsPaymentAllocated || IsReserveAdded)
                if (IsPaymentMade())
                { _result = SaveChangesAlert(); }

                if (_result != DialogResult.Cancel)
                { IsPendingCheckLoaded = false; }

                if (_result == DialogResult.No)
                { ResetForm();
                    //subashish comment
                    // PatientControl.ClaimFieldText = "";
                }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        private void tls_btnViewEOB_Click(object sender, EventArgs e)
        {
            ToggleEOB();
        }

        private void tls_btnCharge_Click(object sender, EventArgs e)
        {
            try
            {
                ShowModifyCharges();
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }
        }

        private void tls_btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult _result = DialogResult.No;

                //if (IsPaymentAllocated || IsReserveAdded)
                if (IsPaymentMade())
                { _result = SaveChangesAlert(); }

                if (_result == DialogResult.No)
                { this.Close(); }
            }
            catch (Exception ex)
            { gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true); }

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
            try
            {
                using (frmInsurancePaymentLog ofrmInsurancePaymentLog = new frmInsurancePaymentLog())
                {

                    ofrmInsurancePaymentLog.ShowDialog(this);

                    if (IsPendingCheckLoaded)
                    {
                        bool _isPaymentVoid = InsurancePayment.IsVoidedInsurancePayment(EOBPaymentID);
                        if (_isPaymentVoid)
                        {
                            IsPendingCheckLoaded = false;
                            ResetForm();
                        }
                    }
                }
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
            //if (IsInsuranceCompanySelected())
            //{
            UseReserveAmount();
            //}
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

                //if (e.KeyChar != Convert.ToChar(8) && e.KeyChar != Convert.ToChar(46))
                //{
                //    if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"^[0-9]*$") == false)
                //    {
                //        e.Handled = true;
                //    }
                //}
                //else if (txtCheckAmount.Text.Contains(".") && e.KeyChar == Convert.ToChar(46))
                //{
                //    e.Handled = true;
                //}

                if (e.KeyChar == Convert.ToChar(13))
                {
                    if (txtCheckAmount.Text != "")
                    {
                        try
                        { CheckAmount = Convert.ToDecimal(txtCheckAmount.Text); }
                        catch
                        { CheckAmount = 0; }

                        //OnRemainingCalculationChanged();
                    }
                    else
                    { CheckAmount = 0; }

                    //if (txtCheckAmount.Text != "")
                    //{
                    //    try
                    //    {
                    //        CheckAmount = Convert.ToDecimal(txtCheckAmount.Text);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        //MessageBox.Show("Amount is invalid, Please enter a valid amount", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        //return;
                    //    }
                    //    OnRemainingCalculationChanged();
                    //}
                    //else
                    //{ CheckAmount = 0; }
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
            LoadPendingCheck();
            //SelectPaymentMode(PaymentMode.Check);
        }

        private void btnRemoveCheck_Click(object sender, EventArgs e)
        {
            if (IsPendingCheckLoaded)
            {
                DialogResult _result = DialogResult.No;

                //if (IsPaymentAllocated || IsReserveAdded)
                if (IsPaymentMade())
                { _result = SaveChangesAlert(); }

                if (_result == DialogResult.No)
                {
                    IsPendingCheckLoaded = false;
                    ResetForm();
                }
            }
        }

        private void cmbPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPaymentModeDetails();
        }

        //private void txtCheckAmount_Leave(object sender, EventArgs e)
        //{
        //    if (txtCheckAmount.Text != "")
        //    {
        //        try
        //        {
        //            CheckAmount = Convert.ToDecimal(txtCheckAmount.Text);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Amount is invalid, Please enter a valid amount", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return;
        //        }
        //        OnRemainingCalculationChanged();
        //    }
        //    else
        //    { CheckAmount = 0; }
        //}

        private void txtCheckAmount_Leave(object sender, EventArgs e)
        {
            if (txtCheckAmount.Text != "")
            {
                try
                { CheckAmount = Convert.ToDecimal(txtCheckAmount.Text); }

                catch
                { CheckAmount = 0; }

                //OnRemainingCalculationChanged();
            }
            else
            { CheckAmount = 0; }
        }



        private bool CheckAmount_Modify()
        {
            bool _isModifyCheckAmt = false;

            if (txtCheckAmount.Text != "" && CheckAmount != _OriginalCheckAmount)
            {
                gloBilling ogloBilling = new gloBilling(AppSettings.ConnectionStringPM, string.Empty);
                try
                {

                    bool _isAppyRules = false;

                    _isAppyRules = InsurancePayment.GetDialyCloseValidationSetting(AppSettings.ClinicID);

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
            //else
            //{ CheckAmount = 0; }

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

        #endregion

        #region " Short Cut Menu Click Events "

        private void mnuPayment_Save_Click(object sender, EventArgs e)
        {
            PerformSavePayment();
        }

        private void mnuPayment_SavenClose_Click(object sender, EventArgs e)
        {
            //bool _isPaymentSaved = PerformSavePayment();

            //if (_isPaymentSaved)
            //{
            //    this.Close();
            //}
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
            Int64 _crossWalkID = InsurancePayment.GetCPTCrossWalKID(ContactInsuranceID);

            if (_crossWalkID == 0)
            { chkShowCrosswalk.Visible = false; chkShowCrosswalk.Checked = false; }
            else
            { chkShowCrosswalk.Visible = true; chkShowCrosswalk.Checked = true; }
        }

        private void SetPaymentModeDetails()
        {
            if (cmbPayMode.SelectedIndex >= 0)
            {
                txtCheckNumber.Text = "";
                mskCheckDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                cmbCardType.SelectedIndex = -1;
                txtCardAuthorizationNo.Text = "";
                mskCreditExpiryDate.Text = "";
                panel16.TabStop = false;

                if (cmbPayMode.Text.Trim() == EOBPaymentMode.Check.ToString())
                {
                    SelectedPaymentMode = EOBPaymentMode.Check;
                    lblCheckNo.Text = " Check# :";
                    lblCheckDate.Text = "Check Date :";
                    mskCheckDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                }
                else if (cmbPayMode.Text.Trim() == EOBPaymentMode.MoneyOrder.ToString())
                {
                    SelectedPaymentMode = EOBPaymentMode.MoneyOrder;
                    lblCheckNo.Text = "    MO # :";
                    lblCheckDate.Text = "    MO Date :";
                }
                if (cmbPayMode.Text.Trim() == EOBPaymentMode.EFT.ToString())
                {
                    SelectedPaymentMode = EOBPaymentMode.EFT;
                    lblCheckNo.Text = "    EFT# :";
                    lblCheckDate.Text = "   EFT Date :";
                }
                panel16.Enabled = false;

                lblCheckNo.TextAlign = ContentAlignment.MiddleRight;
                lblCheckDate.TextAlign = ContentAlignment.MiddleRight;
                txtCheckNumber.MaxLength = 15;
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

            pnlSinglePaymentCorrTB.Height = (pnlSinglePayment.Height - c1SinglePaymentTotal.Height) / 2;
        }

        private void SetCloseDate()
        {
            gloBilling ogloBilling = new gloBilling(AppSettings.ConnectionStringPM, string.Empty);

            try
            {
                //...Load last selected close date
                //string _lastCloseDate = BillingSettings.LastSelectedCloseDate;

                //...If the last selected close date is being closed then make the close date empty.
                //if (!_lastCloseDate.Equals(string.Empty))
                //{
                //    if (ogloBilling.IsDayClosed(Convert.ToDateTime(_lastCloseDate)) == true)
                //    {
                //        _lastCloseDate = string.Empty;
                //    }
                //}
                string _lastCloseDate = gloBilling.GetUserWiseCloseDay(AppSettings.UserID, CloseDayType.Payment);
                mskCloseDate.Text = _lastCloseDate;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ogloBilling.Dispose();
            }
        }

        private void SetPaymentPrefixNumber()
        {
            EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new global::gloBilling.EOBPayment.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);
            lblPaymetNo.Text = ogloEOBPaymentInsurance.GetPaymentPrefixNumber(_paymentPrefix).Trim();
            ogloEOBPaymentInsurance.Dispose();
        }

        private void FillPaymentMode()
        {
            cmbPayMode.Items.Clear();
            cmbPayMode.Items.Add(PaymentMode.Check.ToString());
            cmbPayMode.Items.Add(PaymentMode.MoneyOrder.ToString());
            cmbPayMode.Items.Add(PaymentMode.EFT.ToString());

            SelectPaymentMode(PaymentMode.Check);
        }

        private void SelectPaymentMode(PaymentMode mode)
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

        private void FillPaymentTray()
        {
            Int64 _lastSelectedTrayID = BillingSettings.LastSelectedPaymentTrayID;
            Int64 _defaultTrayID = InsurancePayment.GetDefaultPaymentTrayID();

            // Set default payment tray
            SelectedPaymentTray = InsurancePayment.GetPaymentTrayDescription(_defaultTrayID);
            SelectedPaymentTrayID = _defaultTrayID;

            if (_lastSelectedTrayID > 0)
            {
                if (InsurancePayment.IsPaymentTrayActive(_lastSelectedTrayID))
                {
                    if (!_lastSelectedTrayID.Equals(_defaultTrayID))
                    {
                        SelectedPaymentTray = InsurancePayment.GetPaymentTrayDescription(_lastSelectedTrayID);
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

        private void SelectPaymentTray()
        {
            frmBillingTraySelection ofrmBillingTraySelection = new frmBillingTraySelection(AppSettings.ConnectionStringPM);

            try
            {
                //...Show pop-up to select the Tray
                ofrmBillingTraySelection.WindowState = FormWindowState.Normal;
                ofrmBillingTraySelection.StartPosition = FormStartPosition.CenterParent;
                ofrmBillingTraySelection.IsChargeTray = false;
                ofrmBillingTraySelection.ShowDialog(this);

                toolTip1.SetToolTip(lblPaymentTray, null);

                if (ofrmBillingTraySelection.FormResult == DialogResult.OK)
                {
                    if (ofrmBillingTraySelection.SelectedTrayID > 0)
                    {
                        this.SelectedPaymentTray = ofrmBillingTraySelection.SelectedTrayName;
                        this.SelectedPaymentTrayID = ofrmBillingTraySelection.SelectedTrayID;

                        toolTip1.SetToolTip(lblPaymentTray, ofrmBillingTraySelection.SelectedTrayName);
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
            //string _claim = string.Empty;

            //if (!PatientControl.SubClaimNumber.Equals(0))
            //{ _claim = String.Concat(PatientControl.ClaimNumber, "-", PatientControl.SubClaimNumber); }
            //else
            //{ _claim = PatientControl.ClaimNumber.ToString(); }

            EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new EOBPayment.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);
            EOBPayment.Common.PaymentInsuranceClaim oPaymentInsuranceClaim = new global::gloBilling.EOBPayment.Common.PaymentInsuranceClaim();

            try
            {
                // This method need to be refactor - TO DO
                //oPaymentInsuranceClaim = InsurancePayment.GetBillingTransaction(PatientControl.ClaimNumber, PatientControl.SubClaimNumber, ContactInsuranceID, 0);//ogloEOBPaymentInsurance.GetBillingTransaction(_claim, 0, 0, 0);
                oPaymentInsuranceClaim = InsurancePayment.GetBillingTransaction(PatientControl.ClaimNumber, PatientControl.SubClaimNumber, ContactInsuranceID, PatientInsuranceID);
                LoadPaymentGrid(oPaymentInsuranceClaim);

                DisplayCrossWalk();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (ogloEOBPaymentInsurance != null) { ogloEOBPaymentInsurance.Dispose(); }
                if (oPaymentInsuranceClaim != null) { oPaymentInsuranceClaim.Dispose(); }
            }
        }

        private void LoadPaymentGrid(EOBPayment.Common.PaymentInsuranceClaim oPaymentInsuranceClaim)
        {
            int _FocusRowIndex = 0;
            int _ClaimStartRowIndex = 0;

            c1SinglePayment.Redraw = false;
            c1SinglePayment.ScrollBars = ScrollBars.None;

            #region "Fill Billed Transaction"

       //     bool _IsPaymentGridLoading = true;

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

                        c1SinglePayment.SetData(_RowIndex, COL_DOS_FROM, gloDateMaster.gloDate.DateAsDate(oPaymentInsuranceClaim.CliamLines[j].DOSFrom).ToString("MM/dd/yyyy"));
                        c1SinglePayment.SetData(_RowIndex, COL_DOS_TO, gloDateMaster.gloDate.DateAsDate(oPaymentInsuranceClaim.CliamLines[j].DOSTo).ToString("MM/dd/yyyy"));


                        c1SinglePayment.SetData(_RowIndex, COL_CPT_CODE, oPaymentInsuranceClaim.CliamLines[j].CPTCode);
                        c1SinglePayment.SetData(_RowIndex, COL_CPT_DESCRIPTON, oPaymentInsuranceClaim.CliamLines[j].CPTDescription);
                        c1SinglePayment.SetData(_RowIndex, COL_MODIFIER, oPaymentInsuranceClaim.CliamLines[j].Modifier);

                        c1SinglePayment.SetData(_RowIndex, COL_CROSSWALK_CPT_CODE, oPaymentInsuranceClaim.CliamLines[j].CrossWalkCPTCode);
                        c1SinglePayment.SetData(_RowIndex, COL_CROSSWALK_CPT_DESC, oPaymentInsuranceClaim.CliamLines[j].CrossWalkCPTDescription);

                        c1SinglePayment.SetData(_RowIndex, COL_CHARGE, oPaymentInsuranceClaim.CliamLines[j].Charges);
                        c1SinglePayment.SetData(_RowIndex, COL_UNIT, oPaymentInsuranceClaim.CliamLines[j].Unit);
                        c1SinglePayment.SetData(_RowIndex, COL_TOTALCHARGE, oPaymentInsuranceClaim.CliamLines[j].TotalCharges);

                        c1SinglePayment.SetData(_RowIndex, COL_PAYMENT, null);

                        if (oPaymentInsuranceClaim.CliamLines[j].Last_allowed > 0)
                        {
                            c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, oPaymentInsuranceClaim.CliamLines[j].Last_allowed);
                            c1SinglePayment.SetData(_RowIndex, COL_WRITEOFF, oPaymentInsuranceClaim.CliamLines[j].Last_writeoff);

                        }
                        else
                        {
                            c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, oPaymentInsuranceClaim.CliamLines[j].Allowed);
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

                        #region "Allow Editing"

                        if (PatientControl != null && PatientControl.ClaimNumber > 0 && PatientControl.PatientID > 0 && PatientControl.SelectedInsuranceParty > 1)
                        {
                            c1SinglePayment.SetCellStyle(_RowIndex, COL_ALLOWED, c1SinglePayment.Styles["cs_NonEditableCurrencyStyle"]);
                            c1SinglePayment.Cols[COL_ALLOWED].AllowEditing = false;
                            c1SinglePayment.SetData(_RowIndex, COL_WRITEOFF, null);
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
                            if (!oPaymentInsuranceClaim.CliamLines[j].IsLast_allowedNull)
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, oPaymentInsuranceClaim.CliamLines[j].Last_allowed);
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
                            if (oPaymentInsuranceClaim.CliamLines[j].Last_allowed > 0)
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, oPaymentInsuranceClaim.CliamLines[j].Last_allowed);
                                //c1SinglePayment.SetData(_RowIndex, COL_WRITEOFF, null);
                            }
                            else
                            {
                                c1SinglePayment.SetData(_RowIndex, COL_ALLOWED, oPaymentInsuranceClaim.CliamLines[j].Allowed);
                                //if ((oPaymentInsuranceClaim.CliamLines[j].TotalCharges - oPaymentInsuranceClaim.CliamLines[j].Allowed) == 0)
                                //{
                                //    c1SinglePayment.SetData(_RowIndex, COL_WRITEOFF, null);
                                //}
                                //else
                                //{
                                //    c1SinglePayment.SetData(_RowIndex, COL_WRITEOFF, oPaymentInsuranceClaim.CliamLines[j].TotalCharges - oPaymentInsuranceClaim.CliamLines[j].Allowed);
                                //}
                            }
                            c1SinglePayment.SetData(_RowIndex, COL_PAYMENT, null);
                            c1SinglePayment.SetData(_RowIndex, COL_WRITEOFF, null);
                            c1SinglePayment.SetData(_RowIndex, COL_COPAY, null);
                            c1SinglePayment.SetData(_RowIndex, COL_DEDUCTIBLE, null);
                            c1SinglePayment.SetData(_RowIndex, COL_COINSURANCE, null);
                            c1SinglePayment.SetData(_RowIndex, COL_WITHHOLD, null);

                        }
                        //*****


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
                //c1SinglePayment.Select(_FocusRowIndex, COL_ALLOWED);
                //c1SinglePayment.Focus();
            }

            #endregion

            c1SinglePayment.Redraw = true;
            c1SinglePayment.ScrollBars = ScrollBars.Both;
            c1SinglePayment.TabStop = true;
        }

        private void FillEOBPayments(Int64 EOBPaymentID, Int64 EOBID)
        {
            DataTable _dtEOBPayments = new DataTable();

            try
            {
                _dtEOBPayments = InsurancePayment.GetEOBPayment(EOBPaymentID, EOBID);

                if (_dtEOBPayments != null && _dtEOBPayments.Rows.Count > 0)
                {
                    if (EOBID > 0)
                    {
                        LoadEOBPayments(_dtEOBPayments, true);
                    }
                    else
                    {
                        LoadEOBPayments(_dtEOBPayments);
                    }
                }
                else
                {
                    DesignPaymentGrid(c1MultiplePayment);
                    DesignPaymentGrid(c1MultiplePaymentTotal);
                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            { dbEx.ERROR_Log(dbEx.ToString()); }
            catch
            { }
            finally
            { }
        }

        private void LoadEOBPayments(DataTable dtEOBPayments)
        {
            try
            {
                c1MultiplePayment.Redraw = false;
                c1MultiplePaymentTotal.Redraw = false;
                DesignPaymentGrid(c1MultiplePayment);
                DesignPaymentGrid(c1MultiplePaymentTotal);
                int _RowIndex = 0;
                string _sModifiers = "";
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
                    
                    c1MultiplePayment.SetData(_RowIndex, COL_PAY_EOBPAYMENTID, Convert.ToInt64(row["nEOBPaymentID"]));
                    c1MultiplePayment.SetData(_RowIndex, COL_PAY_EOBID, Convert.ToInt64(row["nEOBID"]));
                    c1MultiplePayment.SetData(_RowIndex, COL_PAY_EOBDTLID, 0);
                    c1MultiplePayment.SetData(_RowIndex, COL_PAY_EOBPAYMENTDTLID, Convert.ToInt64(row["nEOBPaymentDetailID"])); //Newly Added
                    c1MultiplePayment.SetData(_RowIndex, COL_PATIENTID, Convert.ToInt64(row["nPatientID"]));
                    c1MultiplePayment.SetData(_RowIndex, COL_PATIENTNAME, Convert.ToString(row["PatientName"]));
                    c1MultiplePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_ID, Convert.ToInt64(row["nBillingTransactionID"]));
                    c1MultiplePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_DETAILID, Convert.ToInt64(row["nBillingTransactionDetailID"]));
                    c1MultiplePayment.SetData(_RowIndex, COL_MODIFIER, _sModifiers);
                    c1MultiplePayment.SetData(_RowIndex, COL_CLAIMNO, Convert.ToString(row["nClaimNo"]));
                    c1MultiplePayment.SetData(_RowIndex, COL_CLAIMDISPNO, Convert.ToString(row["ClaimNumber"]));
                    if (Convert.ToString(row["PatientName"]) != "Reserve")
                    {
                        c1MultiplePayment.SetData(_RowIndex, COL_DOS_FROM, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(row["nDOSFrom"])).ToString("MM/dd/yyyy"));
                        c1MultiplePayment.SetData(_RowIndex, COL_DOS_TO, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(row["nDOSTo"])).ToString("MM/dd/yyyy"));

                        if (row["dTotalCharges"] != DBNull.Value) { c1MultiplePayment.SetData(_RowIndex, COL_TOTALCHARGE, Convert.ToDecimal(row["dTotalCharges"])); }
                        else { c1MultiplePayment.SetData(_RowIndex, COL_TOTALCHARGE, null); }
                        
                    }
                    else
                    {
                        c1MultiplePayment.SetData(_RowIndex, COL_DOS_FROM, null);
                        c1MultiplePayment.SetData(_RowIndex, COL_DOS_TO, null);
                        c1MultiplePayment.SetData(_RowIndex, COL_TOTALCHARGE, null);
                        
                    }

                    if (row["dCharges"] != DBNull.Value) { c1MultiplePayment.SetData(_RowIndex, COL_CHARGE, Convert.ToDecimal(row["dCharges"])); }
                    else { c1MultiplePayment.SetData(_RowIndex, COL_CHARGE, null); }
                   
                    c1MultiplePayment.SetData(_RowIndex, COL_CPT_CODE, Convert.ToString(row["sCPTCode"]));
                    //c1MultiplePayment.SetData(_RowIndex, COL_CREATEDDATETIME, row["CreatedDateTime"]);
                      
                    if (row["dUnit"] != DBNull.Value) { c1MultiplePayment.SetData(_RowIndex, COL_UNIT, Convert.ToDecimal(row["dUnit"])); }
                    else { c1MultiplePayment.SetData(_RowIndex, COL_UNIT, null); }

                   

                    if (row["dAllowed"] != DBNull.Value && Convert.ToDecimal(row["dAllowed"]) != 0) { c1MultiplePayment.SetData(_RowIndex, COL_ALLOWED, Convert.ToDecimal(row["dAllowed"])); }
                    else { c1MultiplePayment.SetData(_RowIndex, COL_ALLOWED, null); }

                    if (row["dPayment"] != DBNull.Value && Convert.ToDecimal(row["dPayment"]) != 0) { c1MultiplePayment.SetData(_RowIndex, COL_PAYMENT, Convert.ToDecimal(row["dPayment"])); }
                    else { c1MultiplePayment.SetData(_RowIndex, COL_PAYMENT, null); }

                    if (row["PrevPaid"] != DBNull.Value)
                    { c1MultiplePayment.SetData(_RowIndex, COL_LAST_PAYMENT, Convert.ToDecimal(row["PrevPaid"])); }
                    else { c1MultiplePayment.SetData(_RowIndex, COL_LAST_PAYMENT, null); }

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
                    c1MultiplePayment.SetData(_RowIndex, COL_GENERAL, "");

                    if (row["nPaymentType"] != DBNull.Value)
                    { c1MultiplePayment.SetData(_RowIndex, COL_PAYMENTTYPE, Convert.ToInt16(row["nPaymentType"])); }

                    if (row["nPaymentSubType"] != DBNull.Value)
                    { c1MultiplePayment.SetData(_RowIndex, COL_PAYMENTSUBTYPE, Convert.ToInt16(row["nPaymentSubType"])); }
                }

                //c1MultiplePayment.Cols[COL_CLAIMDISPNO].Visible = true;
                c1MultiplePayment.Cols[COL_PATIENTNAME].Visible = true;
                c1MultiplePayment.Cols[COL_PATIENTNAME].Width = 200;

                c1MultiplePayment.Cols[COL_PREVPAID].Visible = false;
                c1MultiplePayment.Cols[COL_BALANCE].Visible = false;
                c1MultiplePayment.Cols[COL_NEXT].Visible = false;
                c1MultiplePayment.Cols[COL_PARTY].Visible = false;
                //c1MultiplePayment.Cols[COL_REASON].Visible = false;

                #region "Calculate Total of Clims"

                c1MultiplePaymentTotal.SetData(0, COL_CHARGE, CalculateMultiplePaymentTotal(COL_CHARGE));
                c1MultiplePaymentTotal.SetData(0, COL_TOTALCHARGE, CalculateMultiplePaymentTotal(COL_TOTALCHARGE));
                c1MultiplePaymentTotal.SetData(0, COL_ALLOWED, CalculateMultiplePaymentTotal(COL_ALLOWED));

                c1MultiplePaymentTotal.SetData(0, COL_PAYMENT, CalculateMultiplePaymentTotal(COL_PAYMENT));
                c1MultiplePaymentTotal.SetData(0, COL_WRITEOFF, CalculateMultiplePaymentTotal(COL_WRITEOFF));
                c1MultiplePaymentTotal.SetData(0, COL_COPAY, CalculateMultiplePaymentTotal(COL_COPAY));
                c1MultiplePaymentTotal.SetData(0, COL_DEDUCTIBLE, CalculateMultiplePaymentTotal(COL_DEDUCTIBLE));
                c1MultiplePaymentTotal.SetData(0, COL_COINSURANCE, CalculateMultiplePaymentTotal(COL_COINSURANCE));
                c1MultiplePaymentTotal.SetData(0, COL_WITHHOLD, CalculateMultiplePaymentTotal(COL_WITHHOLD));

                c1MultiplePaymentTotal.SetData(0, COL_PREVPAID, CalculateMultiplePaymentTotal(COL_PREVPAID));
                c1MultiplePaymentTotal.SetData(0, COL_BALANCE, CalculateMultiplePaymentTotal(COL_BALANCE));
                c1MultiplePaymentTotal.SetData(0, COL_REASON, CalculateMultiplePaymentTotal(COL_REASON));

                c1MultiplePayment.Sort(SortFlags.Ascending, COL_PAY_EOBPAYMENTDTLID);
            }
            catch (Exception)
            {

            }
            finally
            {
                c1MultiplePayment.Redraw = true;
                c1MultiplePaymentTotal.Redraw = true;
            }
                #endregion
        }

        private void LoadEOBPayments(DataTable dtEOBPayments, bool _isEOBSave)
        {
            int _RowIndex = 1;
            string _sModifiers = "";
            try
            {
                c1MultiplePayment.Redraw = false;
                c1MultiplePaymentTotal.Redraw = false;
                for (int i = dtEOBPayments.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow row = dtEOBPayments.Rows[i];
                    if ((Convert.ToString(row["PatientName"]) == "Reserve" && c1MultiplePayment.FindRow(Convert.ToInt64(row["nEOBPaymentDetailID"]), 1, COL_PAY_EOBPAYMENTDTLID, true) == -1) || Convert.ToString(row["PatientName"]) != "Reserve")
                    {
                        
                        c1MultiplePayment.Rows.Insert(1);

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

                        c1MultiplePayment.SetData(_RowIndex, COL_PAY_EOBPAYMENTID, Convert.ToInt64(row["nEOBPaymentID"]));
                        c1MultiplePayment.SetData(_RowIndex, COL_PAY_EOBID, Convert.ToInt64(row["nEOBID"]));
                        c1MultiplePayment.SetData(_RowIndex, COL_PAY_EOBDTLID, 0);
                        c1MultiplePayment.SetData(_RowIndex, COL_PAY_EOBPAYMENTDTLID, Convert.ToInt64(row["nEOBPaymentDetailID"]));//Newly added
                        c1MultiplePayment.SetData(_RowIndex, COL_PATIENTID, Convert.ToInt64(row["nPatientID"]));
                        c1MultiplePayment.SetData(_RowIndex, COL_PATIENTNAME, Convert.ToString(row["PatientName"]));
                        c1MultiplePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_ID, Convert.ToInt64(row["nBillingTransactionID"]));
                        c1MultiplePayment.SetData(_RowIndex, COL_BILLING_TRANSACTON_DETAILID, Convert.ToInt64(row["nBillingTransactionDetailID"]));
                        c1MultiplePayment.SetData(_RowIndex, COL_MODIFIER, _sModifiers);
                        c1MultiplePayment.SetData(_RowIndex, COL_CLAIMNO, Convert.ToString(row["nClaimNo"]));
                        c1MultiplePayment.SetData(_RowIndex, COL_CLAIMDISPNO, Convert.ToString(row["ClaimNumber"]));
                        if (Convert.ToString(row["PatientName"]) != "Reserve")
                        {
                            c1MultiplePayment.SetData(_RowIndex, COL_DOS_FROM, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(row["nDOSFrom"])).ToString("MM/dd/yyyy"));
                            c1MultiplePayment.SetData(_RowIndex, COL_DOS_TO, gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(row["nDOSTo"])).ToString("MM/dd/yyyy"));

                            if (row["dTotalCharges"] != DBNull.Value) { c1MultiplePayment.SetData(_RowIndex, COL_TOTALCHARGE, Convert.ToDecimal(row["dTotalCharges"])); }
                            else { c1MultiplePayment.SetData(_RowIndex, COL_TOTALCHARGE, null); }
                        }
                        else
                        {
                            c1MultiplePayment.SetData(_RowIndex, COL_DOS_FROM, null);
                            c1MultiplePayment.SetData(_RowIndex, COL_DOS_TO, null);
                            c1MultiplePayment.SetData(_RowIndex, COL_TOTALCHARGE, null);

                        }
                        c1MultiplePayment.SetData(_RowIndex, COL_CPT_CODE, Convert.ToString(row["sCPTCode"]));
                        //c1MultiplePayment.SetData(_RowIndex, COL_CREATEDDATETIME, Convert.ToDateTime(row["CreatedDateTime"]).ToString("MM/dd/yyyy hh:mm:ss.fff "));

                        if (row["dCharges"] != DBNull.Value) { c1MultiplePayment.SetData(_RowIndex, COL_CHARGE, Convert.ToDecimal(row["dCharges"])); }
                        else { c1MultiplePayment.SetData(_RowIndex, COL_CHARGE, null); }


                        if (row["dCharges"] != DBNull.Value) { c1MultiplePayment.SetData(_RowIndex, COL_CHARGE, Convert.ToDecimal(row["dCharges"])); }
                        else { c1MultiplePayment.SetData(_RowIndex, COL_CHARGE, null); }

                        if (row["dUnit"] != DBNull.Value) { c1MultiplePayment.SetData(_RowIndex, COL_UNIT, Convert.ToDecimal(row["dUnit"])); }
                        else { c1MultiplePayment.SetData(_RowIndex, COL_UNIT, null); }

                     

                        if (row["dAllowed"] != DBNull.Value && Convert.ToDecimal(row["dAllowed"]) != 0) { c1MultiplePayment.SetData(_RowIndex, COL_ALLOWED, Convert.ToDecimal(row["dAllowed"])); }
                        else { c1MultiplePayment.SetData(_RowIndex, COL_ALLOWED, null); }

                        if (row["dPayment"] != DBNull.Value && Convert.ToDecimal(row["dPayment"]) != 0) { c1MultiplePayment.SetData(_RowIndex, COL_PAYMENT, Convert.ToDecimal(row["dPayment"])); }
                        else { c1MultiplePayment.SetData(_RowIndex, COL_PAYMENT, null); }

                        if (row["PrevPaid"] != DBNull.Value)
                        { c1MultiplePayment.SetData(_RowIndex, COL_LAST_PAYMENT, Convert.ToDecimal(row["PrevPaid"])); }
                        else { c1MultiplePayment.SetData(_RowIndex, COL_LAST_PAYMENT, null); }

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
                        c1MultiplePayment.SetData(_RowIndex, COL_GENERAL, "");

                        if (row["nPaymentType"] != DBNull.Value)
                        { c1MultiplePayment.SetData(_RowIndex, COL_PAYMENTTYPE, Convert.ToInt16(row["nPaymentType"])); }

                        if (row["nPaymentSubType"] != DBNull.Value)
                        { c1MultiplePayment.SetData(_RowIndex, COL_PAYMENTSUBTYPE, Convert.ToInt16(row["nPaymentSubType"])); }
                    }
                }
                //c1MultiplePayment.Cols[COL_CLAIMDISPNO].Visible = true;
                c1MultiplePayment.Cols[COL_PATIENTNAME].Visible = true;
                c1MultiplePayment.Cols[COL_PATIENTNAME].Width = 200;

                c1MultiplePayment.Cols[COL_PREVPAID].Visible = false;
                c1MultiplePayment.Cols[COL_BALANCE].Visible = false;
                c1MultiplePayment.Cols[COL_NEXT].Visible = false;
                c1MultiplePayment.Cols[COL_PARTY].Visible = false;
                //c1MultiplePayment.Cols[COL_REASON].Visible = false;

                #region "Calculate Total of Clims"

                c1MultiplePaymentTotal.SetData(0, COL_CHARGE, CalculateMultiplePaymentTotal(COL_CHARGE));
                c1MultiplePaymentTotal.SetData(0, COL_TOTALCHARGE, CalculateMultiplePaymentTotal(COL_TOTALCHARGE));
                c1MultiplePaymentTotal.SetData(0, COL_ALLOWED, CalculateMultiplePaymentTotal(COL_ALLOWED));

                c1MultiplePaymentTotal.SetData(0, COL_PAYMENT, CalculateMultiplePaymentTotal(COL_PAYMENT));
                c1MultiplePaymentTotal.SetData(0, COL_WRITEOFF, CalculateMultiplePaymentTotal(COL_WRITEOFF));
                c1MultiplePaymentTotal.SetData(0, COL_COPAY, CalculateMultiplePaymentTotal(COL_COPAY));
                c1MultiplePaymentTotal.SetData(0, COL_DEDUCTIBLE, CalculateMultiplePaymentTotal(COL_DEDUCTIBLE));
                c1MultiplePaymentTotal.SetData(0, COL_COINSURANCE, CalculateMultiplePaymentTotal(COL_COINSURANCE));
                c1MultiplePaymentTotal.SetData(0, COL_WITHHOLD, CalculateMultiplePaymentTotal(COL_WITHHOLD));

                c1MultiplePaymentTotal.SetData(0, COL_PREVPAID, CalculateMultiplePaymentTotal(COL_PREVPAID));
                c1MultiplePaymentTotal.SetData(0, COL_BALANCE, CalculateMultiplePaymentTotal(COL_BALANCE));
                c1MultiplePaymentTotal.SetData(0, COL_REASON, CalculateMultiplePaymentTotal(COL_REASON));

                c1MultiplePayment.Sort(SortFlags.Ascending, COL_PAY_EOBPAYMENTDTLID);
            }
            catch (Exception)
            {

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
                {
                    c1SinglePayment.SetData(e.Row, COL_PARTY, null);

                }
                else
                { c1SinglePayment.SetData(e.Row, COL_PARTY, NextResponsibility); }
            }
            //else if (SelectedAction.StartsWith("N")) // N = None
            //{
            //    c1SinglePayment.SetData(e.Row, COL_PARTY, null);
            //}
            else if (SelectedAction.Equals(string.Empty))
            {
                c1SinglePayment.SetData(e.Row, COL_PARTY, null);

            }
        }

        /// <summary>
        /// This method should be called after setting following properties 
        /// 1) PatientInsuranceID
        /// 2) ContactInsuranceID
        /// 3) Claim Number & SubClaim Number 
        /// </summary>
        private void SetCorrectionMode()
        {
            EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new EOBPayment.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);
            EOBPayment.Common.PaymentInsuranceClaim oPaymentInsuranceClaim = new EOBPayment.Common.PaymentInsuranceClaim();

            try
            {
                #region "Get Previous Billied Transaction"

                oPaymentInsuranceClaim = InsurancePayment.GetBillingTransaction(PatientControl.ClaimNumber, PatientControl.SubClaimNumber, ContactInsuranceID, PatientInsuranceID);

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

                #region " Set Correction Takeback Window "
                if (PaymentAction == FormMode.CorrectionMode)
                {
                    //6020 - PaymentRepaymentCorrection
                    SetupCorrectionTBWindow(true);
                }
                else
                {
                    //6020 - PaymentRepaymentCorrection
                    SetupCorrectionTBWindow(false);
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
                                            }
                                            else
                                            {
                                                if (oPaymentInsuranceClaim.CliamLines[j].Last_allowed > 0)
                                                {
                                                    c1SinglePayment.SetData(rIndex, COL_ALLOWED, oPaymentInsuranceClaim.CliamLines[j].Last_allowed);
                                                    //c1SinglePayment.SetData(rIndex, COL_WRITEOFF, null);
                                                }
                                                else
                                                {
                                                    c1SinglePayment.SetData(rIndex, COL_ALLOWED, oPaymentInsuranceClaim.CliamLines[j].Allowed);
                                                    //if ((oPaymentInsuranceClaim.CliamLines[j].TotalCharges - oPaymentInsuranceClaim.CliamLines[j].Allowed) == 0)
                                                    //{
                                                    //    c1SinglePayment.SetData(rIndex, COL_WRITEOFF, null);
                                                    //}
                                                    //else
                                                    //{
                                                    //    c1SinglePayment.SetData(rIndex, COL_WRITEOFF, oPaymentInsuranceClaim.CliamLines[j].TotalCharges - oPaymentInsuranceClaim.CliamLines[j].Allowed);
                                                    //}
                                                }
                                                c1SinglePayment.SetData(rIndex, COL_PAYMENT, null);
                                                c1SinglePayment.SetData(rIndex, COL_WRITEOFF, null);
                                                c1SinglePayment.SetData(rIndex, COL_COPAY, null);
                                                c1SinglePayment.SetData(rIndex, COL_DEDUCTIBLE, null);
                                                c1SinglePayment.SetData(rIndex, COL_COINSURANCE, null);
                                                c1SinglePayment.SetData(rIndex, COL_WITHHOLD, null);
                                               
                                            }
                                            //*****

                                            // Note : while setting correction mode next & party must be reset to blank
                                            c1SinglePayment.SetData(rIndex, COL_NEXT, null);
                                            c1SinglePayment.SetData(rIndex, COL_PARTY, null);

                                            // Crosswalk Billing CPT update, as per selected Insurance 
                                            c1SinglePayment.SetData(rIndex, COL_CROSSWALK_CPT_CODE, oPaymentInsuranceClaim.CliamLines[j].CrossWalkCPTCode);

                                            //Filling correction grid

                                            //c1SinglePaymentCorrTB.Rows.Add();
                                            //c1SinglePaymentCorrTB.SetData(c1SinglePaymentCorrTB.Rows.Count - 1, COL_CPT_CODE, oPaymentInsuranceClaim.CliamLines[j].CPTCode);
                                            //c1SinglePaymentCorrTB.SetData(c1SinglePaymentCorrTB.Rows.Count - 1, COL_CPT_DESCRIPTON, oPaymentInsuranceClaim.CliamLines[j].CPTDescription);
                                            //c1SinglePaymentCorrTB.SetData(c1SinglePaymentCorrTB.Rows.Count - 1, COL_DOS_FROM, oPaymentInsuranceClaim.CliamLines[j].DOSFrom);
                                            //c1SinglePaymentCorrTB.SetData(c1SinglePaymentCorrTB.Rows.Count - 1, COL_MODIFIER, oPaymentInsuranceClaim.CliamLines[j].Modifier);
                                            //c1SinglePaymentCorrTB.SetData(c1SinglePaymentCorrTB.Rows.Count - 1, COL_CHARGE, oPaymentInsuranceClaim.CliamLines[j].Charges);
                                            //c1SinglePaymentCorrTB.SetData(c1SinglePaymentCorrTB.Rows.Count - 1, COL_TOTALCHARGE, oPaymentInsuranceClaim.CliamLines[j].TotalCharges);
                                            //c1SinglePaymentCorrTB.SetData(c1SinglePaymentCorrTB.Rows.Count - 1, COL_ALLOWED, oPaymentInsuranceClaim.CliamLines[j].Last_allowed);
                                            //c1SinglePaymentCorrTB.SetData(c1SinglePaymentCorrTB.Rows.Count - 1, COL_PREVPAID, oPaymentInsuranceClaim.CliamLines[j].LinePaidAmount);


                                            //c1SinglePaymentCorrTB.SetData(c1SinglePaymentCorrTB.Rows.Count - 1, COL_LAST_ALLOWED, oPaymentInsuranceClaim.CliamLines[j].Last_allowed);
                                            //c1SinglePaymentCorrTB.SetData(c1SinglePaymentCorrTB.Rows.Count - 1, COL_LAST_PAYMENT, oPaymentInsuranceClaim.CliamLines[j].Last_payment);
                                            //c1SinglePaymentCorrTB.SetData(c1SinglePaymentCorrTB.Rows.Count - 1, COL_LAST_WRITEOFF, oPaymentInsuranceClaim.CliamLines[j].Last_writeoff);
                                            //c1SinglePaymentCorrTB.SetData(c1SinglePaymentCorrTB.Rows.Count - 1, COL_LAST_COPAY, oPaymentInsuranceClaim.CliamLines[j].Last_copay);
                                            //c1SinglePaymentCorrTB.SetData(c1SinglePaymentCorrTB.Rows.Count - 1, COL_LAST_DEDUCTIBLE, oPaymentInsuranceClaim.CliamLines[j].Last_deductible);
                                            //c1SinglePaymentCorrTB.SetData(c1SinglePaymentCorrTB.Rows.Count - 1, COL_LAST_COINSURANCE, oPaymentInsuranceClaim.CliamLines[j].Last_coinsurance);
                                            //c1SinglePaymentCorrTB.SetData(c1SinglePaymentCorrTB.Rows.Count - 1, COL_LAST_WITHHOLD, oPaymentInsuranceClaim.CliamLines[j].Last_withhold);
                                            //c1SinglePaymentCorrTB.SetData(c1SinglePaymentCorrTB.Rows.Count - 1, COL_ISCORRECTION, oPaymentInsuranceClaim.CliamLines[j].Iscorrection);

                                            //**

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
                            if (PatientControl.SelectedInsuranceParty > 1)
                            {
                                c1SinglePayment.SetCellStyle(rIndex, COL_ALLOWED, c1SinglePayment.Styles["cs_NonEditableCurrencyStyle"]);
                                c1SinglePayment.Cols[COL_ALLOWED].AllowEditing = false;
                                //c1SinglePayment.SetData(rIndex, COL_WRITEOFF, null);

                            }
                            else
                            {
                                c1SinglePayment.SetCellStyle(rIndex, COL_ALLOWED, c1SinglePayment.Styles["cs_EditableCurrencyStyle"]);
                                c1SinglePayment.Cols[COL_ALLOWED].AllowEditing = true;

                            }
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
                if (ogloEOBPaymentInsurance != null) { ogloEOBPaymentInsurance.Dispose(); }
                if (oPaymentInsuranceClaim != null) { oPaymentInsuranceClaim.Dispose(); }
            }
        }

        private void SelectReasonCodes()
        {
            Int64 _billingTranId = 0;
            Int64 _billingTranDtlId = 0;
            Int64 _claimNo = 0;
            Int64 _TrackbillingTranId = 0;
            Int64 _TrackbillingTranDtlId = 0;
            string _SubclaimNo = "";
            int _rowIndex = 0;

            C1.Win.C1FlexGrid.C1FlexGrid oReasonFlex = new C1FlexGrid();
            oReasonFlex.Rows.Count = 0;
            oReasonFlex.Cols.Count = 0;
            oReasonFlex.Rows.Fixed = 0;
            oReasonFlex.Cols.Fixed = 0;

            try
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

                    if (c1SinglePayment.GetData(_rowIndex, COL_REASON) != null)
                    { oReasonFlex = ((C1.Win.C1FlexGrid.C1FlexGrid)c1SinglePayment.GetData(_rowIndex, COL_REASON)); }

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

                    if (oReasonFlex != null && oReasonFlex.Rows.Count > 0)
                    {
                        ofrmEOBPaymentReasonCode.DesignGrid(ofrmEOBPaymentReasonCode.ReasonGrid);
                        for (int rInd = 1; rInd < oReasonFlex.Rows.Count; rInd++)
                        {
                            int row = 0;
                            row = ofrmEOBPaymentReasonCode.ReasonGrid.Rows.Add().Index;

                            for (int cInd = 0; cInd < oReasonFlex.Cols.Count; cInd++)
                            {
                                ofrmEOBPaymentReasonCode.ReasonGrid.SetData(row, cInd, oReasonFlex.GetData(rInd, cInd));
                            }
                        }
                    }
                    else
                    {
                        ofrmEOBPaymentReasonCode.DesignGrid(oReasonFlex);
                        ofrmEOBPaymentReasonCode.DesignGrid(ofrmEOBPaymentReasonCode.ReasonGrid);
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


                    ofrmEOBPaymentReasonCode.ShowDialog(this);

                    if (ofrmEOBPaymentReasonCode.FrmDlgRst == DialogResult.OK)
                    {
                        oReasonFlex = new C1FlexGrid();
                        ofrmEOBPaymentReasonCode.DesignGrid(oReasonFlex);

                        for (int rInd = 1; rInd < ofrmEOBPaymentReasonCode.ReasonGrid.Rows.Count; rInd++)
                        {
                            int row = 0;
                            row = oReasonFlex.Rows.Add().Index;

                            for (int cInd = 0; cInd < ofrmEOBPaymentReasonCode.ReasonGrid.Cols.Count; cInd++)
                            {
                                oReasonFlex.SetData(row, cInd, ofrmEOBPaymentReasonCode.ReasonGrid.GetData(rInd, cInd));
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
            catch
            {
            }
        }

        private void LoadPendingCheck()
        {
            frmBillingCheckDiff ofrmBillingCheckDiff = new frmBillingCheckDiff();

            ofrmBillingCheckDiff.InsuranceCompanyID = SelectedInsuranceCompanyID; //Convert.ToInt64(lblInsCompany.Tag);
            ofrmBillingCheckDiff.InsuranceCompanyName = SelectedInsuranceCompany; //lblInsCompany.Text;

            // Commented code, as close date should be blank by default
            //if (mskCloseDate.MaskCompleted == true)
            //{
            //    ofrmBillingCheckDiff.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
            //}
            ofrmBillingCheckDiff.ShowDialog(this);

            if (ofrmBillingCheckDiff._frmDlgRst == DialogResult.OK)
            {
                DialogResult _result = DialogResult.No;

                //if (IsPaymentAllocated || IsReserveAdded)
                if (IsPaymentMade() && txtCheckNumber.Text.Trim() != "")
                { _result = SaveChangesAlert(); }

                if (_result == DialogResult.Cancel)
                {
                    // If check is already loaded then don't reset the property
                    // If check is not loaded & user clicked cancel then reset the property
                    if (!IsPendingCheckLoaded)
                    { IsPendingCheckLoaded = false; }
                }
                else if (_result == DialogResult.No)
                {
                    IsPendingCheckLoaded = true;


                    // Reset method call
                    //Roopali -- 03/12/2011
                    //When the user selects an existing payment, the changes entered for the claim have to stay 
                    //ResetForm();


                    // Set the EOBPayment ID
                    _EOBPaymentID = ofrmBillingCheckDiff.EOBPaymentID;
                    // Load the EOB
                    LoadFormData(_EOBPaymentID, 0,false );
                    // Setup the controls
                    pnlMultiplePayment.Visible = true;
                    tls_btnViewEOB.Tag = "HideEOB";
                    tls_btnViewEOB.Text = "&Hide EOB";
                    tls_btnViewEOB.ToolTipText = "Hide Explanation of Benefit";
                    splitter1.Enabled = true;
                }
            }

            ofrmBillingCheckDiff.Dispose();
        }

        //private void SearchInsuranceCompany()
        //{
        //    try
        //    {
        //        if (!IsPaymentInProcess) //(_EOBPaymentID <= 0)
        //        {
        //            frmSearchInsuranceCompany ofrmSearchInsuranceCompany = new frmSearchInsuranceCompany();
        //            ofrmSearchInsuranceCompany.StartPosition = FormStartPosition.CenterScreen;
        //            ofrmSearchInsuranceCompany.ShowDialog();

        //            if (ofrmSearchInsuranceCompany.FrmDlgRst == DialogResult.OK)
        //            {
        //                this.SelectedInsuranceCompanyID = ofrmSearchInsuranceCompany.InsuranceCompanyID;
        //                this.SelectedInsuranceCompany = ofrmSearchInsuranceCompany.InsuranceCompanyName;

        //                if (SelectedInsuranceCompanyID != 0 && SelectedInsuranceCompany != "")
        //                { toolTip1.SetToolTip(lblInsCompany, SelectedInsuranceCompany); }
        //            }
        //            ofrmSearchInsuranceCompany.Dispose();

        //            //txtCheckAmount.Select(); 
        //            //txtCheckAmount.Focus();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Insurance company cannot be changed.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //}

        private void SearchInsuranceCompany()
        {
            try
            {
                if (!IsPaymentInProcess) //(_EOBPaymentID <= 0)
                {
                    //if (AmountTakenFromReserve > 0)
                    //{
                    //    MessageBox.Show("Insurance company cannot be changed, there is reserve amount used on current payment." + Environment.NewLine + "Clear existing reserve amount to change the insurance company.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //else
                    //{
                        frmSearchInsuranceCompany ofrmSearchInsuranceCompany = new frmSearchInsuranceCompany();
                        ofrmSearchInsuranceCompany.StartPosition = FormStartPosition.CenterScreen;
                        ofrmSearchInsuranceCompany.ShowDialog(this);

                        if (ofrmSearchInsuranceCompany.FrmDlgRst == DialogResult.OK)
                        {
                            this.SelectedInsuranceCompanyID = ofrmSearchInsuranceCompany.InsuranceCompanyID;
                            this.SelectedInsuranceCompany = ofrmSearchInsuranceCompany.InsuranceCompanyName;

                            if (SelectedInsuranceCompanyID != 0 && SelectedInsuranceCompany != "")
                            { toolTip1.SetToolTip(lblInsCompany, SelectedInsuranceCompany); }
                        }
                        ofrmSearchInsuranceCompany.Dispose();

                        //txtCheckAmount.Select(); 
                        //txtCheckAmount.Focus();
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
            //SplitClaimDetails _claimDetails = new SplitClaimDetails(PatientControl.ClaimNumber, PatientControl.SubClaimNumber);
            if (ClaimDetails.IsClaimExist)
            {
                if (ClaimDetails.TransactionID != 0)
                {
                    // Old code commented on 19052010
                    //gloBilling ogloBilling = new gloBilling(AppSettings.ConnectionStringPM, string.Empty);
                    //ogloBilling.ShowModifyCharges(PatientControl.PatientID, ClaimDetails.TransactionID, ClaimDetails.IsClaimVoided);
                    //if (!ClaimDetails.IsClaimVoided)
                    //{ LoadClaim(); }

                    gloBilling ogloBilling = new gloBilling(AppSettings.ConnectionStringPM, string.Empty);

                    // Set the transactionID for the latest claim 
                    Int64 nTransactionID = ogloBilling.GetLastTransactionID(ClaimDetails.TransactionID);

                    //frmBillingModifyCharges ofrmBillingModifyCharges = frmBillingModifyCharges.GetInstance(PatientControl.PatientID, nTransactionID, false, AppSettings.ConnectionStringPM, string.Empty);
                    //ofrmBillingModifyCharges.WindowState = FormWindowState.Maximized;
                    //ofrmBillingModifyCharges.IsClaimVoided = ClaimDetails.IsClaimVoided;
                    //ofrmBillingModifyCharges.ShowDialog();

                    //ogloBilling.Dispose();

                    //// If changes has been modified then only refresh the claim else skip
                    //if (ofrmBillingModifyCharges.IsModified)
                    //{
                    //    if (!ClaimDetails.IsClaimVoided)
                    //    { LoadClaim(); }
                    //}

                    //---------------------------------------------------------------
                    Boolean _IsModified = false;

                    _IsModified = ogloBilling.ShowModifyCharges(PatientControl.PatientID, nTransactionID, ClaimDetails.IsClaimVoided);
                    ogloBilling.Dispose();

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

        #region " Reserve Methods "

        private void UseReserveAmount()
        {
            bool _IsReservedUsed = false;
            frmPaymentUseReserveInsurance ofrmInsuranceUseReserve = null;
            if (this.EOBInsurancePaymentMasterLines.Count > 0)
            {
                for (int i = 0; i <= this.EOBInsurancePaymentMasterLines.Count -1 ; i++)
                {
                    if (this.EOBInsurancePaymentMasterLines[i].PaymentType == EOBPaymentType.InsuraceReserverd)
                    {
                        _IsReservedUsed = true;
                        break;
                    }
                }
                if (_IsReservedUsed)
                {
                    ofrmInsuranceUseReserve = new frmPaymentUseReserveInsurance(AppSettings.ConnectionStringPM, 0);
                }
                else
                {
                    ofrmInsuranceUseReserve = new frmPaymentUseReserveInsurance(AppSettings.ConnectionStringPM, SelectedInsuranceCompanyID);
                }

            }
            else
            {
                ofrmInsuranceUseReserve = new frmPaymentUseReserveInsurance(AppSettings.ConnectionStringPM, SelectedInsuranceCompanyID);
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

            ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines = this.EOBInsurancePaymentMasterLines.Copy();
            ofrmInsuranceUseReserve.ShowDialog(this);

            if (ofrmInsuranceUseReserve.DialogResult == DialogResult.OK)
            {
                AmountTakenFromReserve = ofrmInsuranceUseReserve.AmountTakenFromReserve;
                SeletedReserveItems = ofrmInsuranceUseReserve.SeletedReserveItems;

                EOBInsurancePaymentMasterLines = ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines.Copy();

                EOBInsuranceReserveMasterLines = new EOBInsurancePaymentMasterAllocationLines();
                EOBInsuranceReserveMasterLines = ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines;

                //SelectedReserveMasterLines.Clear();  
                //SelectedReserveMasterLines.Clear();
                    if (ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines != null && ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines.Count > 0)
                    {
                        if (SelectedReserveMasterLines != null && SelectedReserveMasterLines.Count > 0)
                        {
                            bool _isSelectedItemFoundInMasterCollection = false;

                            for (int objIndex = 0; objIndex < ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines.Count; objIndex++)
                            {
                                _isSelectedItemFoundInMasterCollection = false;
                                for (int selResLnIndex = 0; selResLnIndex < SelectedReserveMasterLines.Count; selResLnIndex++)
                                {

                                    if (ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines[objIndex].PaymentType == EOBPaymentType.InsuraceReserverd
                               && ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines[objIndex].PaymentSubType == EOBPaymentSubType.Reserved
                               && ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines[objIndex].PaySign == EOBPaymentSign.Payment_Credit
                               && ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines[objIndex].ReserveEOBPaymentID == SelectedReserveMasterLines[selResLnIndex].ReserveEOBPaymentID
                               && ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines[objIndex].ReserveEOBPaymentDetailID == SelectedReserveMasterLines[selResLnIndex].ReserveEOBPaymentDetailID
                               )
                                    {
                                        _isSelectedItemFoundInMasterCollection = true;
                                        SelectedReserveMasterLines[selResLnIndex].Amount = ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines[objIndex].Amount; 



                                    }


                                }
                                if (_isSelectedItemFoundInMasterCollection == false && ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines[objIndex].PaymentType == EOBPaymentType.InsuraceReserverd
                               && ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines[objIndex].PaymentSubType == EOBPaymentSubType.Reserved
                               && ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines[objIndex].PaySign == EOBPaymentSign.Payment_Credit)
                                {
                                    SelectedReserveMasterLines.Add(ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines[objIndex]);
                                }

                            }
                        }
                        else
                        {
                            SelectedReserveMasterLines.Clear();
                            for (int objIndex = 0; objIndex < ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines.Count; objIndex++)
                            {

                                if (ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines[objIndex].PaymentType == EOBPaymentType.InsuraceReserverd
                                    && ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines[objIndex].PaymentSubType == EOBPaymentSubType.Reserved
                                    && ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines[objIndex].PaySign == EOBPaymentSign.Payment_Credit)
                                {
                                    SelectedReserveMasterLines.Add(ofrmInsuranceUseReserve.EOBInsurancePaymentMasterLines[objIndex]);
                                }

                            }
                        }
                    }
               

                OnRemainingCalculationChanged();
            }
            ofrmInsuranceUseReserve.Dispose();
        }

        private void ReserveRemaining()
        {
            bool _allowToReserve = true;

            if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
            {
                for (int lnInd = 0; lnInd < EOBInsurancePaymentMasterLines.Count; lnInd++)
                {
                    if (EOBInsurancePaymentMasterLines[lnInd].PaymentType == EOBPaymentType.InsuraceReserverd && EOBInsurancePaymentMasterLines[lnInd].Amount > 0)
                    {
                        _allowToReserve = false;
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

        private void SendAmountToReserve(decimal ReserveAmount)
        {
            try
            {
                frmInsuranceReserveRemaining ofrmInsuranceReserveRemaining = new frmInsuranceReserveRemaining(AppSettings.ConnectionStringPM, IsReserveAdded);
                ReserveDetails.InsuranceCompanyID = SelectedInsuranceCompanyID;
                ReserveDetails.InsuranceCompany = lblInsCompany.Text;
                ReserveDetails.AmountToReserve = ReserveAmount;
                ReserveDetails.ReserveNote = ReserveNote;
                //Changed (TakeBackAmount !0 Only)
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
                    this.ReserveDetails = ofrmInsuranceReserveRemaining.ReserveDetails;
                    AmountAddedToReserve = ReserveDetails.AmountToReserve;
                    ReserveNote = ReserveDetails.ReserveNote;
                    SelectedInsuranceCompanyID = ReserveDetails.InsuranceCompanyID;
                    SelectedInsuranceCompany = ReserveDetails.InsuranceCompany;
                    lblInsCompany.Text = SelectedInsuranceCompany;
                }
                ofrmInsuranceReserveRemaining.Dispose();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                ex = null;
            }
        }


        #endregion

        #region  " Save Payment Methods "

        private bool PerformSavePayment()
        {
            bool _isPaymentSaved = false;





            bool _isPaymentMade = IsPaymentMade();
            bool _isCheckUpdating = IsCheckUpdating();


            if (_isCheckUpdating)
            {
                if (!CheckAmount_Modify())
                {
                    //MessageBox.Show("No payment has been made to save. ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isPaymentSaved = false;
                }
            }

            if ((_isPaymentMade) || (IsNextActionUpdated) || (_isCheckUpdating))
            {
                // Check necessary validation before proceed 
                if (SavePaymentValidation(_isPaymentMade))
                {
                    _isPaymentSaved = SavePayment(_isPaymentMade, _isCheckUpdating);
                }
            }
            else
            {
                MessageBox.Show("No payment has been made to save. ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isPaymentSaved = false;
            }
            return _isPaymentSaved;
        }

        private bool SavePayment(bool _isPaymentMade, bool _isCheckUpdating)
        {
            bool _IsDataSaved = true;

            // Main Payment Master Object
            EOBPayment.Common.PaymentInsurance oPaymentInsurace = null;
            // Main Credit Line Entry Object
            EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsPaymentCreditDetail = null;
            // Class with Save EOB Method 

            EOBPayment.Common.PaymentInsuranceClaim oPaymentInsuranceClaim = null;
            EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsurancePaymentReserveDetail = null;
            SplitClaimDetails oSplitClaimDetails = new SplitClaimDetails();

            int _crResPayMode = 0;
            //bool _IsClaimTobeSplit = false;
            int _financeLineNo = 0;
            Int64 _nEOBID = 0;

            System.Data.SqlClient.SqlTransaction _sqlTransaction = null;

            try
            {
                if ((_isPaymentMade && txtCheckAmount.Text.Trim() != "") || (_isCheckUpdating))
                {
                    oPaymentInsurace = new PaymentInsurance();

                    #region " Master Data "

                    // Get Payment Master Object
                    oPaymentInsurace = GetPaymentMaster();

                    #endregion

                    #region " Credit Service Line Entry applicable to all claims, so it goes to master level not line level "

                    #region " Check/Cash/etc txtCheckAmount - Main Credit Line Entry "

                    // Get financial line no
                    _financeLineNo = oPaymentInsurace.EOBInsurancePaymentLineDetails.Count + 1;
                    // Pass financial line no & EOBPaymentID to get the main credit line object
                    oEOBInsPaymentCreditDetail = GetMainCreditLineEntry(_financeLineNo);
                    // Add the Main credit line object to main payment object
                    oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentCreditDetail);

                    #endregion



                    //..*** For correction (if amount -ve) we make credit entry against the cpt to balance cpt amount
                    //..*** & according to new logic we have to make credit line entry against current check with making the 
                    //..** -ve correction amount +ve

                    #region "Correction Line Credit Line Entry - Credit -ve against CPT & Positive Credit line against current check."

                    decimal _crPayAmt = 0;
                    decimal _CheckSumCorrectionAmount = 0;
                    if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 0)
                    {
                        for (int nCrIndex = 1; nCrIndex <= c1SinglePayment.Rows.Count - 1; nCrIndex++)
                        {
                            if (c1SinglePayment.GetData(nCrIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(nCrIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                            {
                                if (c1SinglePayment.GetData(nCrIndex, COL_ISCORRECTION) != null && Convert.ToBoolean(c1SinglePayment.GetData(nCrIndex, COL_ISCORRECTION)) == true)
                                {
                                    if (c1SinglePayment.GetData(nCrIndex, COL_PAYMENT) != null && c1SinglePayment.GetData(nCrIndex, COL_PAYMENT).ToString().Trim() != "")
                                    {
                                        _crPayAmt = Convert.ToDecimal(c1SinglePayment.GetData(nCrIndex, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(nCrIndex, COL_LAST_PAYMENT));

                                        if (_crPayAmt < 0)
                                        {
                                            _crPayAmt = _crPayAmt - (_crPayAmt * 2);
                                            _crResPayMode = 0;

                                            Int64 _crPatientId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_PATIENTID));
                                            Int64 _crBillTrnId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_ID));
                                            Int64 _crBillTrnDtlId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_DETAILID));

                                            // TO DO : delete as no reference found
                                            //Int64 _crBillTrnDtlLineId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_LINENO));
                                            //Int64 _crTrackBillTrnId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_ID));
                                            //Int64 _crTrackBillTrnDtlId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                            //Int64 _crTrackBillTrnDtlLineId = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_LINENO));
                                            //string _crCptCode = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_CODE)).Trim();

                                            //DataTable _nCrDataTable = InsurancePayment.GetCorrectionRefList(_crPayAmt, _crPatientId, _crBillTrnId, _crBillTrnDtlId, this.PatientInsuranceID, this.ContactInsuranceID, this.SelectedInsuranceCompanyID);
                                            DataTable _nCrDataTable = InsurancePayment.GetCorrectionRefList(_crPayAmt, _crPatientId, _crBillTrnId, _crBillTrnDtlId, this.PatientInsuranceID, this.ContactInsuranceID);

                                            if (_nCrDataTable != null && _nCrDataTable.Rows.Count > 0)
                                            {
                                                _CheckSumCorrectionAmount = 0;

                                                for (int nCrDTIndex = 0; nCrDTIndex <= _nCrDataTable.Rows.Count - 1; nCrDTIndex++)
                                                {
                                                    _CheckSumCorrectionAmount += Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]);

                                                    #region "Set Object to make -ve credit line entry for cpt balance calculation"

                                                    EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsPaymentCorrAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
                                                    oEOBInsPaymentCorrAsCreditDetail.EOBPaymentID = _EOBPaymentID;
                                                    oEOBInsPaymentCorrAsCreditDetail.EOBID = 0;
                                                    oEOBInsPaymentCorrAsCreditDetail.EOBDtlID = 0;
                                                    oEOBInsPaymentCorrAsCreditDetail.EOBPaymentDetailID = 0;

                                                    //...Will be assigning current check payment & payment details id's to Ref. Id.
                                                    oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentID = 0;
                                                    oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentDetailID = 0;
                                                    oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentID = 0;// Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentID"]);
                                                    oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentDetailID = 0;// Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentDetailID"]);

                                                    oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]);
                                                    oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]);
                                                    oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentID"]);
                                                    oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentDetailID"]);

                                                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_ID));
                                                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_DETAILID));
                                                    oEOBInsPaymentCorrAsCreditDetail.BillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(nCrIndex, COL_BILLING_TRANSACTON_LINENO));

                                                    oEOBInsPaymentCorrAsCreditDetail.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_SUBCLAIMNO));
                                                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_ID));
                                                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                                    oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(nCrIndex, COL_TRACK_BILLING_TRANSACTON_LINENO));

                                                    oEOBInsPaymentCorrAsCreditDetail.DOSFrom = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_FROM).ToString()));
                                                    oEOBInsPaymentCorrAsCreditDetail.DOSTo = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_TO).ToString()));

                                                    oEOBInsPaymentCorrAsCreditDetail.CPTCode = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_CODE));
                                                    oEOBInsPaymentCorrAsCreditDetail.CPTDescription = Convert.ToString(c1SinglePayment.GetData(nCrIndex, COL_CPT_DESCRIPTON));
                                                    oEOBInsPaymentCorrAsCreditDetail.Amount = Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]) - (Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]) * 2);
                                                    oEOBInsPaymentCorrAsCreditDetail.IsNullAmount = false;
                                                    oEOBInsPaymentCorrAsCreditDetail.PaymentType = EOBPaymentType.InsuracePayment;
                                                    oEOBInsPaymentCorrAsCreditDetail.PaymentSubType = EOBPaymentSubType.Correction;
                                                    oEOBInsPaymentCorrAsCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
                                                    oEOBInsPaymentCorrAsCreditDetail.PayMode = (EOBPaymentMode)_crResPayMode;

                                                    //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                                    oEOBInsPaymentCorrAsCreditDetail.AccountID = SelectedInsuranceCompanyID; //Convert.ToInt64(lblInsCompany.Tag);
                                                    oEOBInsPaymentCorrAsCreditDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
                                                    oEOBInsPaymentCorrAsCreditDetail.MSTAccountID = PatientInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                                    oEOBInsPaymentCorrAsCreditDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
                                                    oEOBInsPaymentCorrAsCreditDetail.ContactInsID = ContactInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));

                                                    oEOBInsPaymentCorrAsCreditDetail.PatientID = PatientControl.PatientID;
                                                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayID = SelectedPaymentTrayID;
                                                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayCode = SelectedPaymentTrayCode;
                                                    oEOBInsPaymentCorrAsCreditDetail.PaymentTrayDescription = SelectedPaymentTray;
                                                    oEOBInsPaymentCorrAsCreditDetail.UserID = AppSettings.UserID;
                                                    oEOBInsPaymentCorrAsCreditDetail.UserName = AppSettings.UserName;
                                                    oEOBInsPaymentCorrAsCreditDetail.ClinicID = AppSettings.ClinicID;

                                                    oEOBInsPaymentCorrAsCreditDetail.FinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails.Count + 1;
                                                    oEOBInsPaymentCorrAsCreditDetail.MainCreditLineID = 0;
                                                    oEOBInsPaymentCorrAsCreditDetail.IsMainCreditLine = false;
                                                    oEOBInsPaymentCorrAsCreditDetail.IsReserveCreditLine = false;
                                                    oEOBInsPaymentCorrAsCreditDetail.IsCorrectionCreditLine = true;
                                                    oEOBInsPaymentCorrAsCreditDetail.RefFinanceLieNo = 0;
                                                    oEOBInsPaymentCorrAsCreditDetail.UseRefFinanceLieNo = false;

                                                    if (mskCloseDate.MaskCompleted == true)
                                                    {
                                                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                                        oEOBInsPaymentCorrAsCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                                    }

                                                    oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentCorrAsCreditDetail);
                                                    oEOBInsPaymentCorrAsCreditDetail.Dispose();

                                                    #endregion

                                                    #region "Set Object to make +ve credit entry against current check"

                                                    //---->> 1 = Add Object , 2 = Modify Object , 0 = Do Nothing
                                                    int _Object_Add_Modify_None = -1;
                                                    int _Object_Modify_Index = -1;
                                                    bool _isAnyCorrectionLinePresent = false;

                                                    if (EOBInsurancePaymentMasterLines != null && EOBInsurancePaymentMasterLines.Count > 0)
                                                    {
                                                        for (int index = 0; index < EOBInsurancePaymentMasterLines.Count; index++)
                                                        {
                                                            if (EOBInsurancePaymentMasterLines[index].IsCorrectionCreditLine == true)
                                                            {
                                                                //Check if the MasterLines has any correction line in it
                                                                //if no entry is present then for current correction entry we have to 
                                                                //add the entry based on this flag
                                                                _isAnyCorrectionLinePresent = true;

                                                                //1. Check if the correction amount is from the current check if yes do not add object

                                                                //2. If correction amount is from different check & the credit line does not exists then 
                                                                //   add the +ve credit line entry

                                                                //3. If the correction amount is from different check & the credit line exists then
                                                                //   modify the credit line entry

                                                                if (EOBInsurancePaymentMasterLines[index].RefEOBPaymentID == _EOBPaymentID)
                                                                {
                                                                    _Object_Add_Modify_None = 0;
                                                                    break;
                                                                }
                                                                else if (EOBInsurancePaymentMasterLines[index].RefEOBPaymentID == Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"])
                                                                    && EOBInsurancePaymentMasterLines[index].RefEOBPaymentDetailID == Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]))
                                                                {
                                                                    EOBInsurancePaymentMasterLines[index].Amount += Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]);
                                                                    _Object_Add_Modify_None = 2;
                                                                    _Object_Modify_Index = index;
                                                                    break;
                                                                }
                                                                else
                                                                {
                                                                    _Object_Add_Modify_None = 1;
                                                                }
                                                            }
                                                        }

                                                        if (_isAnyCorrectionLinePresent == false)
                                                        { _Object_Add_Modify_None = 1; _isAnyCorrectionLinePresent = false; }
                                                    }
                                                    else
                                                    { _Object_Add_Modify_None = 1; }

                                                    if (_Object_Add_Modify_None == 1)
                                                    {
                                                        #region " Set new Credit line object "

                                                        oEOBInsPaymentCorrAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
                                                        oEOBInsPaymentCorrAsCreditDetail.EOBPaymentID = _EOBPaymentID;
                                                        oEOBInsPaymentCorrAsCreditDetail.EOBID = 0;
                                                        oEOBInsPaymentCorrAsCreditDetail.EOBDtlID = 0;
                                                        oEOBInsPaymentCorrAsCreditDetail.EOBPaymentDetailID = 0;

                                                        //...Will be assigning current check payment & payment details id's to Ref. Id.
                                                        //oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]);
                                                        //oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]);
                                                        oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]);
                                                        oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]);
                                                        oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentID"]);
                                                        oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentDetailID"]);

                                                        oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentID"]);
                                                        oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nRefEOBPaymentDetailID"]);
                                                        oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentID"]);
                                                        oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentDetailID = Convert.ToInt64(_nCrDataTable.Rows[nCrDTIndex]["nResEOBPaymentDetailID"]);

                                                        oEOBInsPaymentCorrAsCreditDetail.BillingTransactionID = 0;
                                                        oEOBInsPaymentCorrAsCreditDetail.BillingTransactionDetailID = 0;
                                                        oEOBInsPaymentCorrAsCreditDetail.BillingTransactionLineNo = 0;

                                                        oEOBInsPaymentCorrAsCreditDetail.SubClaimNo = "";
                                                        oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionID = 0;
                                                        oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionDetailID = 0;
                                                        oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionLineNo = 0;

                                                        oEOBInsPaymentCorrAsCreditDetail.DOSFrom = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_FROM).ToString()));
                                                        oEOBInsPaymentCorrAsCreditDetail.DOSTo = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_TO).ToString()));

                                                        oEOBInsPaymentCorrAsCreditDetail.CPTCode = "";
                                                        oEOBInsPaymentCorrAsCreditDetail.CPTDescription = "";
                                                        oEOBInsPaymentCorrAsCreditDetail.Amount = Convert.ToDecimal(_nCrDataTable.Rows[nCrDTIndex]["nSelAmt"]);
                                                        oEOBInsPaymentCorrAsCreditDetail.IsNullAmount = false;
                                                        oEOBInsPaymentCorrAsCreditDetail.PaymentType = EOBPaymentType.InsuranceCorrection;
                                                        oEOBInsPaymentCorrAsCreditDetail.PaymentSubType = EOBPaymentSubType.Correction;
                                                        oEOBInsPaymentCorrAsCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
                                                        oEOBInsPaymentCorrAsCreditDetail.PayMode = (EOBPaymentMode)_crResPayMode;

                                                        //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                                        oEOBInsPaymentCorrAsCreditDetail.AccountID = SelectedInsuranceCompanyID; //Convert.ToInt64(lblInsCompany.Tag);
                                                        oEOBInsPaymentCorrAsCreditDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;

                                                        if (PatientInsuranceID != 0)// lblPayer.Tag != null && Convert.ToString(lblPayer.Tag).Trim() != "")
                                                        {
                                                            oEOBInsPaymentCorrAsCreditDetail.MSTAccountID = PatientInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                                            oEOBInsPaymentCorrAsCreditDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
                                                            oEOBInsPaymentCorrAsCreditDetail.ContactInsID = ContactInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));
                                                        }

                                                        oEOBInsPaymentCorrAsCreditDetail.PatientID = 0;
                                                        oEOBInsPaymentCorrAsCreditDetail.PaymentTrayID = SelectedPaymentTrayID;
                                                        oEOBInsPaymentCorrAsCreditDetail.PaymentTrayCode = SelectedPaymentTrayCode;
                                                        oEOBInsPaymentCorrAsCreditDetail.PaymentTrayDescription = SelectedPaymentTray;
                                                        oEOBInsPaymentCorrAsCreditDetail.UserID = AppSettings.UserID;
                                                        oEOBInsPaymentCorrAsCreditDetail.UserName = AppSettings.UserName;
                                                        oEOBInsPaymentCorrAsCreditDetail.ClinicID = AppSettings.ClinicID;

                                                        oEOBInsPaymentCorrAsCreditDetail.FinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails.Count + 1;
                                                        oEOBInsPaymentCorrAsCreditDetail.MainCreditLineID = 0;
                                                        oEOBInsPaymentCorrAsCreditDetail.IsMainCreditLine = false;
                                                        oEOBInsPaymentCorrAsCreditDetail.IsReserveCreditLine = false;
                                                        oEOBInsPaymentCorrAsCreditDetail.IsCorrectionCreditLine = true;
                                                        oEOBInsPaymentCorrAsCreditDetail.RefFinanceLieNo = 0;
                                                        oEOBInsPaymentCorrAsCreditDetail.UseRefFinanceLieNo = false;

                                                        if (mskCloseDate.MaskCompleted == true)
                                                        {
                                                            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                                            oEOBInsPaymentCorrAsCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                                        }

                                                        oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentCorrAsCreditDetail);
                                                        oEOBInsPaymentCorrAsCreditDetail.Dispose();

                                                        //break;

                                                        #endregion " Set new Credit line object "
                                                    }
                                                    else if (_Object_Add_Modify_None == 2)
                                                    {
                                                        #region " Set new Credit line object "

                                                        oEOBInsPaymentCorrAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
                                                        oEOBInsPaymentCorrAsCreditDetail.EOBPaymentID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].EOBPaymentID;
                                                        oEOBInsPaymentCorrAsCreditDetail.EOBID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].EOBID;
                                                        oEOBInsPaymentCorrAsCreditDetail.EOBDtlID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].EOBDtlID;
                                                        oEOBInsPaymentCorrAsCreditDetail.EOBPaymentDetailID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].EOBPaymentDetailID;


                                                        oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].RefEOBPaymentID;
                                                        oEOBInsPaymentCorrAsCreditDetail.RefEOBPaymentDetailID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].RefEOBPaymentDetailID;
                                                        oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].ReserveEOBPaymentID;
                                                        oEOBInsPaymentCorrAsCreditDetail.ReserveEOBPaymentDetailID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].ReserveEOBPaymentDetailID;

                                                        oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].OldRefEOBPaymentID;
                                                        oEOBInsPaymentCorrAsCreditDetail.OldRefEOBPaymentDetailID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].OldRefEOBPaymentDetailID;
                                                        oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].OldReserveEOBPaymentID;
                                                        oEOBInsPaymentCorrAsCreditDetail.OldReserveEOBPaymentDetailID = EOBInsurancePaymentMasterLines[_Object_Modify_Index].OldReserveEOBPaymentDetailID;

                                                        oEOBInsPaymentCorrAsCreditDetail.BillingTransactionID = 0;
                                                        oEOBInsPaymentCorrAsCreditDetail.BillingTransactionDetailID = 0;
                                                        oEOBInsPaymentCorrAsCreditDetail.BillingTransactionLineNo = 0;

                                                        oEOBInsPaymentCorrAsCreditDetail.SubClaimNo = "";
                                                        oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionID = 0;
                                                        oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionDetailID = 0;
                                                        oEOBInsPaymentCorrAsCreditDetail.TrackBillingTransactionLineNo = 0;

                                                        oEOBInsPaymentCorrAsCreditDetail.DOSFrom = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_FROM).ToString()));
                                                        oEOBInsPaymentCorrAsCreditDetail.DOSTo = Convert.ToInt64(gloDateMaster.gloDate.DateAsNumber(c1SinglePayment.GetData(nCrIndex, COL_DOS_TO).ToString()));

                                                        oEOBInsPaymentCorrAsCreditDetail.CPTCode = "";
                                                        oEOBInsPaymentCorrAsCreditDetail.CPTDescription = "";
                                                        oEOBInsPaymentCorrAsCreditDetail.Amount = EOBInsurancePaymentMasterLines[_Object_Modify_Index].Amount;
                                                        oEOBInsPaymentCorrAsCreditDetail.IsNullAmount = false;
                                                        oEOBInsPaymentCorrAsCreditDetail.PaymentType = EOBPaymentType.InsuranceCorrection;
                                                        oEOBInsPaymentCorrAsCreditDetail.PaymentSubType = EOBPaymentSubType.Correction;
                                                        oEOBInsPaymentCorrAsCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
                                                        oEOBInsPaymentCorrAsCreditDetail.PayMode = (EOBPaymentMode)_crResPayMode;

                                                        //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
                                                        oEOBInsPaymentCorrAsCreditDetail.AccountID = SelectedInsuranceCompanyID; //Convert.ToInt64(lblInsCompany.Tag);
                                                        oEOBInsPaymentCorrAsCreditDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;

                                                        if (PatientInsuranceID != 0)// lblPayer.Tag != null && Convert.ToString(lblPayer.Tag).Trim() != "")
                                                        {
                                                            oEOBInsPaymentCorrAsCreditDetail.MSTAccountID = PatientInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                                            oEOBInsPaymentCorrAsCreditDetail.MSTAccountType = EOBPaymentAccountType.PatientInsurace;
                                                            oEOBInsPaymentCorrAsCreditDetail.ContactInsID = ContactInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));
                                                        }

                                                        oEOBInsPaymentCorrAsCreditDetail.PatientID = 0;
                                                        oEOBInsPaymentCorrAsCreditDetail.PaymentTrayID = SelectedPaymentTrayID;
                                                        oEOBInsPaymentCorrAsCreditDetail.PaymentTrayCode = SelectedPaymentTrayCode;
                                                        oEOBInsPaymentCorrAsCreditDetail.PaymentTrayDescription = SelectedPaymentTray;
                                                        oEOBInsPaymentCorrAsCreditDetail.UserID = AppSettings.UserID;
                                                        oEOBInsPaymentCorrAsCreditDetail.UserName = AppSettings.UserName;
                                                        oEOBInsPaymentCorrAsCreditDetail.ClinicID = AppSettings.ClinicID;

                                                        oEOBInsPaymentCorrAsCreditDetail.FinanceLieNo = oPaymentInsurace.EOBInsurancePaymentLineDetails.Count + 1;
                                                        oEOBInsPaymentCorrAsCreditDetail.MainCreditLineID = 0;
                                                        oEOBInsPaymentCorrAsCreditDetail.IsMainCreditLine = false;
                                                        oEOBInsPaymentCorrAsCreditDetail.IsReserveCreditLine = false;
                                                        oEOBInsPaymentCorrAsCreditDetail.IsCorrectionCreditLine = true;
                                                        oEOBInsPaymentCorrAsCreditDetail.RefFinanceLieNo = 0;
                                                        oEOBInsPaymentCorrAsCreditDetail.UseRefFinanceLieNo = false;

                                                        if (mskCloseDate.MaskCompleted == true)
                                                        {
                                                            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                                            oEOBInsPaymentCorrAsCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                                        }

                                                        oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentCorrAsCreditDetail);
                                                        oEOBInsPaymentCorrAsCreditDetail.Dispose();

                                                        //break;

                                                        #endregion " Set new Credit line object "
                                                    }

                                                    #endregion
                                                }

                                                if (_CheckSumCorrectionAmount != _crPayAmt)
                                                {
                                                    string _message = "Invalid Insurance Payment correction list retrival for PatientID : " + _crPatientId + ", Amount : " + _crPayAmt + ",InsuranceID : " + this.PatientInsuranceID + ",ContactID : " + this.ContactInsuranceID + " ,BillingTrnID : " + _crBillTrnId + ",BillingTrnDtlID : " + _crBillTrnDtlId + " ";
                                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.Payment, gloAuditTrail.ActivityType.Add, _message, gloAuditTrail.ActivityOutCome.Failure);
                                                    return false;
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }

                    #endregion

                    #region "Use Reserved Credit Line Entry"

                    if (IsReserveUsed)
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
                            
                        }

                        for (int i = 0; i <= EOBInsurancePaymentMasterLines.Count - 1; i++)
                        {
                            EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine EOBInsurancePaymentMasterLine = EOBInsurancePaymentMasterLines[i];

                            //..Code changes done by Sagar Ghodke on 20100105(critical change Confirmation needed)
                            //...Below commented condition is previous one
                            //if (EOBInsurancePaymentMasterLines[i].PaymentType == EOBPaymentType.InsuracePayment)
                            if (EOBInsurancePaymentMasterLine.PaymentType == EOBPaymentType.InsuraceReserverd && EOBInsurancePaymentMasterLine.PaymentSubType == EOBPaymentSubType.Reserved)
                            {
                                EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsPaymentResAsCreditDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
                                // Get financial line no
                                _financeLineNo = oPaymentInsurace.EOBInsurancePaymentLineDetails.Count + 1;
                                // Pass finalcial line & used reserve details to get the credit line object for used reserve

                                if (txtCheckAmount.Enabled == true)
                                {
                                    oEOBInsPaymentResAsCreditDetail = GetCreditLineForReserveUsed(EOBInsurancePaymentMasterLine, _financeLineNo);
                                    // Add the credit line object to Main payment object
                                    oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentResAsCreditDetail);

                                }
                                else
                                {
                                    if ((EOBInsurancePaymentMasterLine.Amount - EOBInsurancePaymentMasterLine.DBReserveAmount) > 0)
                                    {
                                        oEOBInsPaymentResAsCreditDetail = GetCreditLineForReserveUsed(EOBInsurancePaymentMasterLine, _financeLineNo);
                                        // Add the credit line object to Main payment object
                                        oPaymentInsurace.EOBInsurancePaymentLineDetails.Add(oEOBInsPaymentResAsCreditDetail);
                                    }
                                }

                                if (_TotalAllocation <= 0 && txtCheckAmount.Enabled == false)
                                {
                                    break;
                                }
                            }
                        }
                    }

                    #endregion

                    #endregion

                    //Allocation Amount - Start
                    //Assign collected credit lines to masterallocationlines,
                    //Purpose is we are going to allocate money to debit line from this object
                    //and the same time we have to reduce to amount from object to allocate remaining
                    //in this case while saving we have to send actual amount
                    //that's why we take one object separate to allocation

                    if (oPaymentInsurace != null && oPaymentInsurace.EOBInsurancePaymentLineDetails != null)
                    {
                        for (int i = 0; i <= oPaymentInsurace.EOBInsurancePaymentLineDetails.Count - 1; i++)
                        {
                            bool _AddLine = true;

                            EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine oEOBInsPaymentAllDtl = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentMasterAllocationLine();

                            #region "If Credit line first time added and its second time then dont add just update the amount"

                            if (oPaymentInsurace.EOBInsurancePaymentLineDetails[i].IsMainCreditLine == true)
                            {
                                for (int ccl = 0; ccl <= EOBInsurancePaymentMasterAllocationLines.Count - 1; ccl++)
                                {
                                    if (EOBInsurancePaymentMasterAllocationLines[ccl].IsMainCreditLine == true)
                                    {
                                        decimal _OldCheckBalAmt = EOBInsurancePaymentMasterAllocationLines[ccl].Amount;
                                        decimal _OldCheckAmt = 0;
                                        for (int cml = 0; cml <= EOBInsurancePaymentMasterLines.Count - 1; cml++)
                                        {
                                            if (EOBInsurancePaymentMasterLines[cml].IsMainCreditLine == true)
                                            {
                                                _OldCheckAmt = EOBInsurancePaymentMasterLines[cml].Amount;
                                                break;
                                            }
                                        }
                                        decimal _NewCheckAmt = oPaymentInsurace.EOBInsurancePaymentLineDetails[i].Amount;
                                        decimal _NewCheckBalAmt = 0;
                                        if (_NewCheckAmt < _OldCheckAmt)
                                        {
                                            _NewCheckBalAmt = _OldCheckBalAmt - (_OldCheckAmt - _NewCheckAmt);
                                        }
                                        else
                                        {
                                            _NewCheckBalAmt = _OldCheckBalAmt + (_NewCheckAmt - _OldCheckAmt);
                                        }

                                        EOBInsurancePaymentMasterAllocationLines[ccl].Amount = _NewCheckBalAmt;// oPaymentInsurace.EOBInsurancePaymentLineDetails[ccl].Amount;
                                        _AddLine = false;
                                        break;
                                    }
                                }
                            }
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

                                //Added by Subashish_b on 16/Feb /2011 (integration made on date-10/May/2011) for  assigning the PAF values to object while saving.
                                oEOBInsPaymentAllDtl.PAccountID = oPaymentInsurace.PAccountID;
                                oEOBInsPaymentAllDtl.GuarantorID = oPaymentInsurace.GuarantorID;
                                oEOBInsPaymentAllDtl.AccountPatientID = oPaymentInsurace.AccountPatientID;
                                //End

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

                    oPaymentInsuranceClaim = new EOBPayment.Common.PaymentInsuranceClaim();

                    if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                    {
                        if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
                        {
                            for (int clmIndex = 1; clmIndex < c1SinglePayment.Rows.Count; clmIndex++)
                            {
                                if (c1SinglePayment.GetData(clmIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(clmIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.Claim)
                                {
                                    if (c1SinglePayment.GetData(clmIndex, COL_BILLING_TRANSACTON_ID) != null && Convert.ToString(c1SinglePayment.GetData(clmIndex, COL_BILLING_TRANSACTON_ID)).Trim() != "")
                                    {
                                        oPaymentInsuranceClaim.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(clmIndex, COL_BILLING_TRANSACTON_ID));
                                        oPaymentInsuranceClaim.TrackBillingTrnID = Convert.ToInt64(c1SinglePayment.GetData(clmIndex, COL_TRACK_BILLING_TRANSACTON_ID));
                                        oPaymentInsuranceClaim.ClaimNo = Convert.ToInt64(c1SinglePayment.GetData(clmIndex, COL_CLAIMNO));
                                        oPaymentInsuranceClaim.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(clmIndex, COL_SUBCLAIMNO));

                                        oSplitClaimDetails.TransactionMasterID = oPaymentInsuranceClaim.BillingTransactionID;
                                        oSplitClaimDetails.TransactionID = oPaymentInsuranceClaim.TrackBillingTrnID;
                                        oSplitClaimDetails.ClaimNo = oPaymentInsuranceClaim.ClaimNo;
                                        oSplitClaimDetails.SubClaimNo = oPaymentInsuranceClaim.SubClaimNo;
                                        oSplitClaimDetails.ClinicID = AppSettings.ClinicID;
                                    }
                                }
                            }
                        }

                        #region "EOB Service Lines - New Logic - Direct allocation from credit line insted of allocation from correction, reserve and check"

                        EOBPayment.Common.EOBInsurancePaymentDetail oEOBInsurancePaymentDetail = null;
                        for (int i = 0; i <= c1SinglePayment.Rows.Count - 1; i++)
                        {
                            if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                            {
                                if (
                                        (c1SinglePayment.GetData(i, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PAYMENT)).Trim() != "")
                                    //|| (c1SinglePayment.GetData(i, COL_NEXT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT)).Trim() != "")
                                    )
                                {
                                    EOBPayment.Common.PaymentInsuranceLine oPaymentInsuranceLine = new EOBPayment.Common.PaymentInsuranceLine();
                                    bool _Add_WO_WH = false;

                                    #region "EOB Line"

                                    oPaymentInsuranceLine.PatientID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_PATIENTID));
                                    oPaymentInsuranceLine.PatInsuranceID = PatientInsuranceID;  //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 1));
                                    oPaymentInsuranceLine.InsContactID = ContactInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));

                                    oPaymentInsuranceLine.BLTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                    oPaymentInsuranceLine.BLTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                    oPaymentInsuranceLine.BLTransactionLineNo = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));

                                    oPaymentInsuranceLine.TrackBLTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                    oPaymentInsuranceLine.TrackBLTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                    oPaymentInsuranceLine.TrackBLTransactionLineNo = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_LINENO));

                                    oPaymentInsuranceLine.ClaimNumber = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                                    oPaymentInsuranceLine.SubClaimNumber = Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));

                                    oPaymentInsuranceLine.DOSFrom = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                    oPaymentInsuranceLine.DOSTo = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
                                    oPaymentInsuranceLine.CPTCode = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                    oPaymentInsuranceLine.CPTDescription = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

                                    oPaymentInsuranceLine.BLInsuranceID = 0;
                                    oPaymentInsuranceLine.BLInsuranceName = "";
                                    oPaymentInsuranceLine.BLInsuranceFlag = InsuranceTypeFlag.None;

                                    if (c1SinglePayment.GetData(i, COL_CHARGE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_CHARGE)).Trim() != "")
                                    { oPaymentInsuranceLine.Charges = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_CHARGE)); oPaymentInsuranceLine.IsNullCharges = false; }

                                    if (c1SinglePayment.GetData(i, COL_UNIT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_UNIT)).Trim() != "")
                                    { oPaymentInsuranceLine.Unit = Convert.ToInt64(c1SinglePayment.GetData(i, COL_UNIT)); oPaymentInsuranceLine.IsNullUnit = false; }


                                    if (c1SinglePayment.GetData(i, COL_TOTALCHARGE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_TOTALCHARGE)).Trim() != "")
                                    { oPaymentInsuranceLine.TotalCharges = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_TOTALCHARGE)); oPaymentInsuranceLine.IsNullTotalCharges = false; }

                                    if (c1SinglePayment.GetData(i, COL_ALLOWED) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_ALLOWED)).Trim() != "")
                                    { oPaymentInsuranceLine.Allowed = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_ALLOWED)); oPaymentInsuranceLine.IsNullAllowed = false; }

                                    if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF)).Trim() != "")
                                    { oPaymentInsuranceLine.WriteOff = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_WRITEOFF)); oPaymentInsuranceLine.IsNullWriteOff = false; }

                                    oPaymentInsuranceLine.NonCovered = 0;

                                    if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PAYMENT)).Trim() != "")
                                    { oPaymentInsuranceLine.InsuranceAmount = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)); oPaymentInsuranceLine.IsNullInsurance = false; }

                                    if (c1SinglePayment.GetData(i, COL_COPAY) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_COPAY)).Trim() != "")
                                    { oPaymentInsuranceLine.Copay = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_COPAY)); oPaymentInsuranceLine.IsNullCopay = false; }

                                    if (c1SinglePayment.GetData(i, COL_DEDUCTIBLE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_DEDUCTIBLE)).Trim() != "")
                                    { oPaymentInsuranceLine.Deductible = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_DEDUCTIBLE)); oPaymentInsuranceLine.IsNullDeductible = false; }

                                    if (c1SinglePayment.GetData(i, COL_COINSURANCE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_COINSURANCE)).Trim() != "")
                                    { oPaymentInsuranceLine.CoInsurance = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_COINSURANCE)); oPaymentInsuranceLine.IsNullCoInsurance = false; }

                                    if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD)).Trim() != "")
                                    { oPaymentInsuranceLine.Withhold = Convert.ToDecimal(c1SinglePayment.GetData(i, COL_WITHHOLD)); oPaymentInsuranceLine.IsNullWithhold = false; }

                                    oPaymentInsuranceLine.PaymentTrayID = SelectedPaymentTrayID;
                                    oPaymentInsuranceLine.PaymentTrayCode = SelectedPaymentTrayCode;
                                    oPaymentInsuranceLine.PaymentTrayDesc = SelectedPaymentTray;

                                    oPaymentInsuranceLine.UserID = AppSettings.UserID;
                                    oPaymentInsuranceLine.UserName = AppSettings.UserName;
                                    oPaymentInsuranceLine.ClinicID = AppSettings.ClinicID;

                                    if (mskCloseDate.MaskCompleted == true)
                                    {
                                        mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                        oPaymentInsuranceLine.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                    }
                                    //Added by Subashish_b on 16/Feb /2011 (integration made on date-10/May/2011) for  assigning the PAF values from local variable to the object while saving 
                                    oPaymentInsuranceLine.PAccountID = this.nPAccountID;
                                    oPaymentInsuranceLine.GuarantorID = this.nGuarantorID;
                                    oPaymentInsuranceLine.AccountPatientID = this.nAccountPatientID;
                                    //End

                                    #region " Set Line Reason Codes "

                                    //...Code added on 20100318 by Sagar Ghodke
                                    //...Code added to auto set the adjustment codes for W/O,Copay,Dedutible,Coinsurance,Withhold
                                    //...by reading there respective values from admin settings

                                    EOBPayment.Common.PaymentInsuranceLineResonCode oPaymentInsuranceLineResonCode = null;
                                    string _code = "";

                                    for (int colIndex = COL_WRITEOFF; colIndex <= COL_WITHHOLD; colIndex++)
                                    {
                                        _code = "";
                                        if (c1SinglePayment.GetData(i, colIndex) != null && Convert.ToString(c1SinglePayment.GetData(i, colIndex)).Trim() != ""
                                            && Convert.ToDecimal(c1SinglePayment.GetData(i, colIndex)) != 0)
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
                                            oPaymentInsuranceLineResonCode.ReasonAmount = Convert.ToDecimal(c1SinglePayment.GetData(i, colIndex));
                                            oPaymentInsuranceLineResonCode.CommentType = EOBCommentType.SystemReasonCode;
                                            oPaymentInsuranceLine.LineResonCodes.Add(oPaymentInsuranceLineResonCode);
                                            oPaymentInsuranceLineResonCode = null;
                                        }
                                    }
                                    //...End code add 20100318

                                    if (c1SinglePayment.GetData(i, COL_REASON) != null)
                                    {
                                        C1FlexGrid oFlex = ((C1FlexGrid)(c1SinglePayment.GetData(i, COL_REASON)));
                                        //EOBPayment.Common.PaymentInsuranceLineResonCode oPaymentInsuranceLineResonCode = null;

                                        if (oFlex != null && oFlex.Rows.Count > 0)
                                        {
                                            for (int rIndex = 1; rIndex < oFlex.Rows.Count; rIndex++)
                                            {
                                                oPaymentInsuranceLineResonCode = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineResonCode();

                                                if (oFlex.GetData(rIndex, oFlex.Cols["Id"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Id"].Index)).Trim() != "")
                                                { oPaymentInsuranceLineResonCode.ID = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["Id"].Index)); }

                                                if (oFlex.GetData(rIndex, oFlex.Cols["ClaimNo"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["ClaimNo"].Index)).Trim() != "")
                                                { oPaymentInsuranceLineResonCode.ClaimNo = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["ClaimNo"].Index)); }

                                                if (oFlex.GetData(rIndex, oFlex.Cols["SubClaimNo"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["SubClaimNo"].Index)).Trim() != "")
                                                { oPaymentInsuranceLineResonCode.SubClaimNo = Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["SubClaimNo"].Index)); }

                                                if (oFlex.GetData(rIndex, oFlex.Cols["EOBPaymentID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["EOBPaymentID"].Index)).Trim() != "")
                                                { oPaymentInsuranceLineResonCode.EOBPaymentID = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["EOBPaymentID"].Index)); }

                                                if (oFlex.GetData(rIndex, oFlex.Cols["EOBID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["EOBID"].Index)).Trim() != "")
                                                { oPaymentInsuranceLineResonCode.EOBID = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["EOBID"].Index)); }

                                                if (oFlex.GetData(rIndex, oFlex.Cols["EOBPayDtlID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["EOBPayDtlID"].Index)).Trim() != "")
                                                { oPaymentInsuranceLineResonCode.EOBPaymentDetailID = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["EOBPayDtlID"].Index)); }

                                                if (oFlex.GetData(rIndex, oFlex.Cols["BLTransactionID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["BLTransactionID"].Index)).Trim() != "")
                                                { oPaymentInsuranceLineResonCode.BillingTransactionID = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["BLTransactionID"].Index)); }

                                                if (oFlex.GetData(rIndex, oFlex.Cols["BLTransactionDtlID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["BLTransactionDtlID"].Index)).Trim() != "")
                                                { oPaymentInsuranceLineResonCode.BillingTransactionDetailID = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["BLTransactionDtlID"].Index)); }

                                                if (oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionID"].Index)).Trim() != "")
                                                { oPaymentInsuranceLineResonCode.TrackBillingTransactionID = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionID"].Index)); }

                                                if (oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionDtlID"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionDtlID"].Index)).Trim() != "")
                                                { oPaymentInsuranceLineResonCode.TrackBillingTransactionDetailID = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["TrackBLTransactionDtlID"].Index)); }

                                                if (oFlex.GetData(rIndex, oFlex.Cols["Code"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Code"].Index)).Trim() != "")
                                                { oPaymentInsuranceLineResonCode.ReasonCode = Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Code"].Index)); }

                                                if (oFlex.GetData(rIndex, oFlex.Cols["Description"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Description"].Index)).Trim() != "")
                                                { oPaymentInsuranceLineResonCode.ReasonDescription = Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Description"].Index)); }

                                                if (oFlex.GetData(rIndex, oFlex.Cols["Amount"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["Amount"].Index)).Trim() != "")
                                                { oPaymentInsuranceLineResonCode.ReasonAmount = Convert.ToDecimal(oFlex.GetData(rIndex, oFlex.Cols["Amount"].Index)); oPaymentInsuranceLineResonCode.IsNullReasonAmount = false; }

                                                if (oFlex.GetData(rIndex, oFlex.Cols["ClinicId"].Index) != null && Convert.ToString(oFlex.GetData(rIndex, oFlex.Cols["ClinicId"].Index)).Trim() != "")
                                                { oPaymentInsuranceLineResonCode.ClinicID = Convert.ToInt64(oFlex.GetData(rIndex, oFlex.Cols["ClinicId"].Index)); }

                                                oPaymentInsuranceLineResonCode.CloseDate = oPaymentInsurace.CloseDate;
                                                oPaymentInsuranceLineResonCode.HasData = true;
                                                oPaymentInsuranceLineResonCode.CommentType = EOBCommentType.Reason;

                                                oPaymentInsuranceLine.LineResonCodes.Add(oPaymentInsuranceLineResonCode);
                                            }
                                        }
                                    }

                                    #endregion " Set Line Reason Codes "


                                    #region " Statement Notes & Internal Notes for Line "

                                    if (c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTE)).Trim() != "")
                                    {
                                        EOBPayment.Common.PaymentInsuranceLineNote oPaymentInsuranceLineNote = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineNote();

                                        oPaymentInsuranceLineNote.ClaimNo = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                                        oPaymentInsuranceLineNote.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
                                        oPaymentInsuranceLineNote.EOBPaymentID = _EOBPaymentID;
                                        oPaymentInsuranceLineNote.EOBID = 0;
                                        oPaymentInsuranceLineNote.EOBPaymentDetailID = 0;

                                        oPaymentInsuranceLineNote.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                        oPaymentInsuranceLineNote.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));

                                        oPaymentInsuranceLineNote.TrackBillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                        oPaymentInsuranceLineNote.TrackBillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));

                                        oPaymentInsuranceLineNote.Code = "";
                                        oPaymentInsuranceLineNote.Description = Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTE)).Trim();
                                        oPaymentInsuranceLineNote.Amount = 0;
                                        if (c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTEONPRINT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTEONPRINT)).Trim() != "")
                                        { oPaymentInsuranceLineNote.IncludeOnPrint = Convert.ToBoolean(c1SinglePayment.GetData(i, COL_LINE_STATEMENTNOTEONPRINT)); }
                                        oPaymentInsuranceLineNote.ClinicID = AppSettings.ClinicID;
                                        oPaymentInsuranceLineNote.PaymentNoteType = EOBPaymentType.InsuracePayment;
                                        oPaymentInsuranceLineNote.PaymentNoteSubType = EOBPaymentSubType.StatementNote;
                                        oPaymentInsuranceLineNote.HasData = true;
                                        oPaymentInsuranceLineNote.CloseDate = oPaymentInsurace.CloseDate;
                                        oPaymentInsuranceLineNote.UserId = AppSettings.UserID;

                                        oPaymentInsuranceLine.LineNotes.Add(oPaymentInsuranceLineNote);
                                        //oPaymentInsurace.Notes.Add(oPaymentInsuranceLineNote);
                                        oPaymentInsuranceLineNote.Dispose();
                                    }

                                    if (c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTE) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTE)).Trim() != "")
                                    {
                                        EOBPayment.Common.PaymentInsuranceLineNote oPaymentInsuranceLineNote = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineNote();

                                        oPaymentInsuranceLineNote.ClaimNo = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                                        oPaymentInsuranceLineNote.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));

                                        oPaymentInsuranceLineNote.EOBPaymentID = _EOBPaymentID;
                                        oPaymentInsuranceLineNote.EOBID = 0;
                                        oPaymentInsuranceLineNote.EOBPaymentDetailID = 0;

                                        oPaymentInsuranceLineNote.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                        oPaymentInsuranceLineNote.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));

                                        oPaymentInsuranceLineNote.TrackBillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                        oPaymentInsuranceLineNote.TrackBillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));

                                        oPaymentInsuranceLineNote.Code = "";
                                        oPaymentInsuranceLineNote.Description = Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTE)).Trim();
                                        oPaymentInsuranceLineNote.Amount = 0;
                                        if (c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTEONPRINT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTEONPRINT)).Trim() != "")
                                        { oPaymentInsuranceLineNote.IncludeOnPrint = Convert.ToBoolean(c1SinglePayment.GetData(i, COL_LINE_INTERNALNOTEONPRINT)); }
                                        oPaymentInsuranceLineNote.ClinicID = AppSettings.ClinicID;
                                        oPaymentInsuranceLineNote.PaymentNoteType = EOBPaymentType.InsuracePayment;
                                        oPaymentInsuranceLineNote.PaymentNoteSubType = EOBPaymentSubType.InternalNote;
                                        oPaymentInsuranceLineNote.HasData = true;
                                        oPaymentInsuranceLineNote.CloseDate = oPaymentInsurace.CloseDate;
                                        oPaymentInsuranceLineNote.UserId = AppSettings.UserID;

                                        //oPaymentInsurace.Notes.Add(oPaymentInsuranceLineNote);
                                        oPaymentInsuranceLine.LineNotes.Add(oPaymentInsuranceLineNote);
                                        oPaymentInsuranceLineNote.Dispose();
                                    }

                                    #endregion " Statement Notes & Internal Notes for Line "

                                    oPaymentInsuranceLine.InsCompanyID = SelectedInsuranceCompanyID;//Convert.ToInt64(lblInsCompany.Tag);

                                    #endregion

                                    #region "Debit Service Line - Insurance - Allocation from Current Check, Correction/Takeback, Use Reserved"

                                    if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PAYMENT)).Trim() != "")
                                    {
                                        if (c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_PAYMENT)).Trim() != "")
                                        {

                                            //..Code changes done by sagar ghodke .. on 20100322 to resolve save of zero payment debit line
                                            //below commented condition is previous
                                            //if ((Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT))) > 0)
                                            if ((Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT))) >= 0)
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

                                                if (c1SinglePayment.GetData(i, COL_PAYMENT) != null && c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null)
                                                { _fillPayAmt = (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT)) - Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT))); }
                                                else if (c1SinglePayment.GetData(i, COL_PAYMENT) != null)
                                                { _fillPayAmt = (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_PAYMENT))); }
                                                else if (c1SinglePayment.GetData(i, COL_LAST_PAYMENT) != null)
                                                { _fillPayAmt = 0 - (Convert.ToDecimal(c1SinglePayment.GetData(i, COL_LAST_PAYMENT))); }
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

                                                oEOBInsurancePaymentDetail.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                                oEOBInsurancePaymentDetail.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                                oEOBInsurancePaymentDetail.BillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));

                                                oEOBInsurancePaymentDetail.TrackBillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                                oEOBInsurancePaymentDetail.TrackBillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                                oEOBInsurancePaymentDetail.TrackBillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_LINENO));
                                                oEOBInsurancePaymentDetail.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));

                                                oEOBInsurancePaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                                oEOBInsurancePaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
                                                oEOBInsurancePaymentDetail.CPTCode = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                                oEOBInsurancePaymentDetail.CPTDescription = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

                                                oEOBInsurancePaymentDetail.Amount = _fillPayAmt;
                                                oEOBInsurancePaymentDetail.IsNullAmount = _isNullfillPayAmt;

                                                oEOBInsurancePaymentDetail.PaymentType = EOBPaymentType.InsuracePayment;
                                                oEOBInsurancePaymentDetail.PaymentSubType = EOBPaymentSubType.Insurace;
                                                oEOBInsurancePaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                                oEOBInsurancePaymentDetail.PayMode = SelectedPaymentMode;

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
                                                oEOBInsurancePaymentDetail.ContactInsID = ContactInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));

                                                oEOBInsurancePaymentDetail.PatientID = PatientControl.PatientID;
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

                                                if (mskCloseDate.MaskCompleted == true)
                                                {
                                                    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                                    oEOBInsurancePaymentDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                                }

                                                //Added by Subashish_b on 16/Feb /2011 (integration made on date-10/May/2011) for  assigning the PAF values from local variable to the object while saving 
                                                oEOBInsurancePaymentDetail.PAccountID = this.nPAccountID;
                                                oEOBInsurancePaymentDetail.GuarantorID = this.nGuarantorID;
                                                oEOBInsurancePaymentDetail.AccountPatientID = this.nAccountPatientID;
                                                //End

                                                oPaymentInsuranceLine.EOBInsurancePaymentLineDetails.Add(oEOBInsurancePaymentDetail);
                                                oEOBInsurancePaymentDetail.Dispose();


                                                #endregion
                                            }
                                        }
                                    }
                                    #endregion

                                    _Add_WO_WH = false;

                                    #region "Debit Service Line - WriteOff"

                                    if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null)
                                    {
                                        oEOBInsurancePaymentDetail = new EOBPayment.Common.EOBInsurancePaymentDetail();
                                        oEOBInsurancePaymentDetail.EOBPaymentID = 0;
                                        oEOBInsurancePaymentDetail.EOBID = 0;
                                        oEOBInsurancePaymentDetail.EOBDtlID = 0;
                                        oEOBInsurancePaymentDetail.EOBPaymentDetailID = 0;

                                        oEOBInsurancePaymentDetail.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                        oEOBInsurancePaymentDetail.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                        oEOBInsurancePaymentDetail.BillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));

                                        oEOBInsurancePaymentDetail.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
                                        oEOBInsurancePaymentDetail.TrackBillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                        oEOBInsurancePaymentDetail.TrackBillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                        oEOBInsurancePaymentDetail.TrackBillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_LINENO));

                                        oEOBInsurancePaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                        oEOBInsurancePaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
                                        oEOBInsurancePaymentDetail.CPTCode = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                        oEOBInsurancePaymentDetail.CPTDescription = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

                                        if (c1SinglePayment.GetData(i, COL_WRITEOFF) != null && c1SinglePayment.GetData(i, COL_WRITEOFF).ToString().Trim() != "")
                                        {
                                            if (c1SinglePayment.GetData(i, COL_ISCORRECTION) != null && Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISCORRECTION)) == true)
                                            {
                                                oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF))) - Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WRITEOFF)));
                                                if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF))) != Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WRITEOFF))))
                                                {
                                                    _Add_WO_WH = true;
                                                }
                                            }
                                            else
                                            {
                                                oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WRITEOFF)));
                                                _Add_WO_WH = true;
                                            }
                                        }

                                        oEOBInsurancePaymentDetail.PaymentType = EOBPaymentType.InsuracePayment;
                                        oEOBInsurancePaymentDetail.PaymentSubType = EOBPaymentSubType.WriteOff;
                                        oEOBInsurancePaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                        oEOBInsurancePaymentDetail.PayMode = SelectedPaymentMode;

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
                                        oEOBInsurancePaymentDetail.ContactInsID = ContactInsuranceID;//Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));

                                        oEOBInsurancePaymentDetail.PatientID = PatientControl.PatientID;
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
                                        //Added by Subashish_b on 23/Feb /2011 (integration made on date-10/May/2011) for  assigning the PAF Values to the save object
                                        oEOBInsurancePaymentDetail.PAccountID = this.nPAccountID;
                                        oEOBInsurancePaymentDetail.GuarantorID = this.nGuarantorID;
                                        oEOBInsurancePaymentDetail.AccountPatientID = this.nAccountPatientID;
                                        //End
                                        if (mskCloseDate.MaskCompleted == true)
                                        {
                                            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                            oEOBInsurancePaymentDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                        }

                                        if (_Add_WO_WH == true)
                                        {
                                            oPaymentInsuranceLine.EOBInsurancePaymentLineDetails.Add(oEOBInsurancePaymentDetail);
                                        }
                                        oEOBInsurancePaymentDetail.Dispose();
                                    }
                                    #endregion

                                    _Add_WO_WH = false;

                                    #region "Debit Service Line - WithHold"

                                    if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null)
                                    {
                                        oEOBInsurancePaymentDetail = new EOBPayment.Common.EOBInsurancePaymentDetail();
                                        oEOBInsurancePaymentDetail.EOBPaymentID = 0;
                                        oEOBInsurancePaymentDetail.EOBID = 0;
                                        oEOBInsurancePaymentDetail.EOBDtlID = 0;
                                        oEOBInsurancePaymentDetail.EOBPaymentDetailID = 0;

                                        oEOBInsurancePaymentDetail.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                                        oEOBInsurancePaymentDetail.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                                        oEOBInsurancePaymentDetail.BillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_LINENO));

                                        oEOBInsurancePaymentDetail.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
                                        oEOBInsurancePaymentDetail.TrackBillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                                        oEOBInsurancePaymentDetail.TrackBillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                                        oEOBInsurancePaymentDetail.TrackBillingTransactionLineNo = Convert.ToInt32(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_LINENO));

                                        oEOBInsurancePaymentDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_FROM)));
                                        oEOBInsurancePaymentDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(c1SinglePayment.GetData(i, COL_DOS_TO)));
                                        oEOBInsurancePaymentDetail.CPTCode = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_CODE));
                                        oEOBInsurancePaymentDetail.CPTDescription = Convert.ToString(c1SinglePayment.GetData(i, COL_CPT_DESCRIPTON));

                                        if (c1SinglePayment.GetData(i, COL_WITHHOLD) != null && c1SinglePayment.GetData(i, COL_WITHHOLD).ToString().Trim() != "")
                                        {
                                            if (c1SinglePayment.GetData(i, COL_ISCORRECTION) != null && Convert.ToBoolean(c1SinglePayment.GetData(i, COL_ISCORRECTION)) == true)
                                            {
                                                oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD))) - Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WITHHOLD)));
                                                if (Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD))) != Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_LAST_WITHHOLD))))
                                                {
                                                    _Add_WO_WH = true;
                                                }
                                            }
                                            else
                                            {
                                                oEOBInsurancePaymentDetail.Amount = Convert.ToDecimal(Convert.ToString(c1SinglePayment.GetData(i, COL_WITHHOLD)));
                                                _Add_WO_WH = true;
                                            }
                                        }

                                        oEOBInsurancePaymentDetail.PaymentType = EOBPaymentType.InsuracePayment;
                                        oEOBInsurancePaymentDetail.PaymentSubType = EOBPaymentSubType.WithHold;
                                        oEOBInsurancePaymentDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                                        oEOBInsurancePaymentDetail.PayMode = SelectedPaymentMode;

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
                                        oEOBInsurancePaymentDetail.ContactInsID = ContactInsuranceID; //Convert.ToInt64(GetTagElement(lblPayer.Tag.ToString(), '~', 4));

                                        oEOBInsurancePaymentDetail.PatientID = PatientControl.PatientID;
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
                                        //Added by Subashish_b on 23/Feb /2011 (integration made on date-10/May/2011) for  assigning the PAF Values to the save object
                                        oEOBInsurancePaymentDetail.PAccountID = this.nPAccountID;
                                        oEOBInsurancePaymentDetail.GuarantorID = this.nGuarantorID;
                                        oEOBInsurancePaymentDetail.AccountPatientID = this.nAccountPatientID;
                                        //End
                                        if (mskCloseDate.MaskCompleted == true)
                                        {
                                            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                            oEOBInsurancePaymentDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                        }

                                        if (_Add_WO_WH == true)
                                        {
                                            oPaymentInsuranceLine.EOBInsurancePaymentLineDetails.Add(oEOBInsurancePaymentDetail);
                                        }
                                        oEOBInsurancePaymentDetail.Dispose();
                                    }
                                    #endregion

                                    oPaymentInsuranceClaim.CliamLines.Add(oPaymentInsuranceLine);
                                    oPaymentInsuranceLine.Dispose();
                                }
                            }
                        }

                        if (oEOBInsurancePaymentDetail != null) { oEOBInsurancePaymentDetail.Dispose(); }

                        #endregion

                        oPaymentInsurace.InsuranceClaims.Add(oPaymentInsuranceClaim);
                        oPaymentInsuranceClaim.Dispose();
                    }

                    #endregion " ......................... Claim Payment Details End ................................. "

                    #region "Reserve Debit Entry if any and it will goes directlly to payment object with credit line"

                    if (IsReserveAdded) //Check if there is any reserve entry to be made
                    {
                        decimal _reserveAmt = 0;
                        //string _reserveNote = "";

                        _reserveAmt = AmountAddedToReserve;

                        //0 ReserveAmount 
                        //1 ReserveNote 
                        //3 ReserveNoteOnPrint 

                        if (AmountAddedToReserve > 0) //Check for the reserve amount is greater than zero
                        {
                            oEOBInsurancePaymentReserveDetail = new global::gloBilling.EOBPayment.Common.EOBInsurancePaymentDetail();
                            oEOBInsurancePaymentReserveDetail.EOBPaymentID = 0;
                            oEOBInsurancePaymentReserveDetail.EOBID = 0;
                            oEOBInsurancePaymentReserveDetail.EOBDtlID = 0;
                            oEOBInsurancePaymentReserveDetail.EOBPaymentDetailID = 0;

                            oEOBInsurancePaymentReserveDetail.BillingTransactionID = 0;
                            oEOBInsurancePaymentReserveDetail.BillingTransactionDetailID = 0;
                            oEOBInsurancePaymentReserveDetail.BillingTransactionLineNo = 0;

                            oEOBInsurancePaymentReserveDetail.TrackBillingTransactionID = 0;
                            oEOBInsurancePaymentReserveDetail.TrackBillingTransactionDetailID = 0;
                            oEOBInsurancePaymentReserveDetail.TrackBillingTransactionLineNo = 0;
                            oEOBInsurancePaymentReserveDetail.SubClaimNo = "";

                            if (mskCloseDate.MaskCompleted == true)
                            {
                                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                oEOBInsurancePaymentReserveDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                                oEOBInsurancePaymentReserveDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                            }
                            oEOBInsurancePaymentReserveDetail.CPTCode = "";
                            oEOBInsurancePaymentReserveDetail.CPTDescription = "";

                            oEOBInsurancePaymentReserveDetail.Amount = _reserveAmt;
                            oEOBInsurancePaymentReserveDetail.IsNullAmount = false;

                            oEOBInsurancePaymentReserveDetail.PaymentType = EOBPaymentType.InsuraceReserverd;
                            oEOBInsurancePaymentReserveDetail.PaymentSubType = EOBPaymentSubType.Reserved;
                            oEOBInsurancePaymentReserveDetail.PaySign = EOBPaymentSign.Receipt_Debit;
                            oEOBInsurancePaymentReserveDetail.PayMode = SelectedPaymentMode;

                            oEOBInsurancePaymentReserveDetail.RefEOBPaymentID = oEOBInsPaymentCreditDetail.RefEOBPaymentID;
                            oEOBInsurancePaymentReserveDetail.RefEOBPaymentDetailID = oEOBInsPaymentCreditDetail.RefEOBPaymentDetailID;
                            oEOBInsurancePaymentReserveDetail.ReserveEOBPaymentID = oEOBInsPaymentCreditDetail.ReserveEOBPaymentID;
                            oEOBInsurancePaymentReserveDetail.ReserveEOBPaymentDetailID = oEOBInsPaymentCreditDetail.ReserveEOBPaymentDetailID;

                            oEOBInsurancePaymentReserveDetail.OldRefEOBPaymentID = oEOBInsPaymentCreditDetail.RefEOBPaymentID;
                            oEOBInsurancePaymentReserveDetail.OldRefEOBPaymentDetailID = oEOBInsPaymentCreditDetail.RefEOBPaymentDetailID;
                            oEOBInsurancePaymentReserveDetail.OldReserveEOBPaymentID = oEOBInsPaymentCreditDetail.ReserveEOBPaymentID;
                            oEOBInsurancePaymentReserveDetail.OldReserveEOBPaymentDetailID = oEOBInsPaymentCreditDetail.ReserveEOBPaymentDetailID;


                            //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID

                            oEOBInsurancePaymentReserveDetail.AccountID = oPaymentInsurace.PayerID;
                            oEOBInsurancePaymentReserveDetail.AccountType = EOBPaymentAccountType.InsuranceCompany;
                            oEOBInsurancePaymentReserveDetail.MSTAccountID = EOBPaymentTypeAccountNo.InsuraceReserverd.GetHashCode();
                            oEOBInsurancePaymentReserveDetail.MSTAccountType = EOBPaymentAccountType.Reserved;
                            oEOBInsurancePaymentReserveDetail.PatientID = PatientControl.PatientID;
                            oEOBInsurancePaymentReserveDetail.PaymentTrayID = SelectedPaymentTrayID;
                            oEOBInsurancePaymentReserveDetail.PaymentTrayCode = SelectedPaymentTrayCode;
                            oEOBInsurancePaymentReserveDetail.PaymentTrayDescription = SelectedPaymentTray;
                            oEOBInsurancePaymentReserveDetail.UserID = AppSettings.UserID;
                            oEOBInsurancePaymentReserveDetail.UserName = AppSettings.UserName;
                            oEOBInsurancePaymentReserveDetail.ClinicID = AppSettings.ClinicID;
                            oEOBInsurancePaymentReserveDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                            oEOBInsurancePaymentReserveDetail.FinanceLieNo = 0;
                            oEOBInsurancePaymentReserveDetail.MainCreditLineID = 0;
                            oEOBInsurancePaymentReserveDetail.IsMainCreditLine = false;
                            oEOBInsurancePaymentReserveDetail.IsReserveCreditLine = false;
                            oEOBInsurancePaymentReserveDetail.IsCorrectionCreditLine = false;
                            oEOBInsurancePaymentReserveDetail.RefFinanceLieNo = 1;
                            oEOBInsurancePaymentReserveDetail.UseRefFinanceLieNo = true;


                            oEOBInsurancePaymentReserveDetail.ReserveAssociationMSTTransactionID = ReserveDetails.MSTTransactionID;
                            oEOBInsurancePaymentReserveDetail.ReserveAssociationTransactionID = ReserveDetails.TransactionID;
                            oEOBInsurancePaymentReserveDetail.ReserveAssociationPatientID = ReserveDetails.PatientID;
                            //Added by Subashish_b on 22/Feb /2011 (integration made on date-10/May/2011) for  assigning the PAF Values to the save object
                            oEOBInsurancePaymentReserveDetail.PAccountID = this.nPAccountID;
                            oEOBInsurancePaymentReserveDetail.GuarantorID = this.nGuarantorID;
                            oEOBInsurancePaymentReserveDetail.AccountPatientID = this.nAccountPatientID;
                            //End
                            //0 ReserveAmount 
                            //1 ReserveNote 
                            //2 ReserveSubType 
                            //3 ReserveNoteOnPrint 

                            #region "General Note"

                            EOBPayment.Common.PaymentInsuranceLineNote oPaymentInsuranceLineNote = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineNote();

                            oPaymentInsuranceLineNote.ClaimNo = 0;
                            oPaymentInsuranceLineNote.EOBPaymentID = 0;
                            oPaymentInsuranceLineNote.EOBID = 0;
                            oPaymentInsuranceLineNote.EOBPaymentDetailID = 0;
                            oPaymentInsuranceLineNote.BillingTransactionID = 0;
                            oPaymentInsuranceLineNote.BillingTransactionDetailID = 0;

                            oPaymentInsuranceLineNote.SubClaimNo = "";
                            oPaymentInsuranceLineNote.TrackBillingTransactionID = 0;
                            oPaymentInsuranceLineNote.TrackBillingTransactionDetailID = 0;
                            oPaymentInsuranceLineNote.TrackBillingTransactionLineNo = 0;

                            oPaymentInsuranceLineNote.Code = "";
                            oPaymentInsuranceLineNote.Description = ReserveNote.Trim();
                            oPaymentInsuranceLineNote.Amount = AmountAddedToReserve;
                            oPaymentInsuranceLineNote.IncludeOnPrint = false;
                            oPaymentInsuranceLineNote.ClinicID = AppSettings.ClinicID;
                            oPaymentInsuranceLineNote.PaymentNoteType = EOBPaymentType.InsuraceReserverd;
                            oPaymentInsuranceLineNote.PaymentNoteSubType = EOBPaymentSubType.Reserved;
                            oPaymentInsuranceLineNote.HasData = true;
                            oPaymentInsuranceLineNote.CloseDate = oPaymentInsurace.CloseDate;
                            oPaymentInsuranceLineNote.UserId = AppSettings.UserID;

                            oEOBInsurancePaymentReserveDetail.LineNotes.Add(oPaymentInsuranceLineNote);
                            oPaymentInsuranceLineNote.Dispose();

                            #endregion

                            oPaymentInsurace.EOBInsurancePaymentReserveLineDetail.Add(oEOBInsurancePaymentReserveDetail);
                            oEOBInsurancePaymentReserveDetail.Dispose();

                            //EOBInsurancePaymentMasterAllocationLines.Add();
                        }
                    }
                    #endregion

                    #region "On hold selection for splitted claims "

                    //gloSplitClaim ogloSplitClaim = null;
                    //DataTable _dtHoldInfo = GetSplittedClaimsHoldInfo(oSplitClaimDetails);

                    #endregion

                    //EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new EOBPayment.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);
                    //this.EOBPaymentID = ogloEOBPaymentInsurance.SaveSplitEOB(oPaymentInsurace, false, out _outEOBid);
                    //ogloEOBPaymentInsurance.Dispose();
                }

                //TEmpSaveNextAction(EOBPaymentID, 0, _outEOBid);

                #region " Save EOB Payment & Next action "

                // Created a new instance for storing the nextaction details which needs to be saved
                PaymentInsuranceLineNextActions oNextActions = new PaymentInsuranceLineNextActions();

                // Created a new instance for split claim logic
                oSplitClaimDetails = new SplitClaimDetails();

                // Called this method to set the NextAction & SplitClaim details
                SetNextActionDetails(out oNextActions, out oSplitClaimDetails);

                bool isEOBSaved = false;
                using (System.Data.SqlClient.SqlConnection _sqlConnection = new System.Data.SqlClient.SqlConnection(AppSettings.ConnectionStringPM))
                {
                    _sqlConnection.Open();
                    _sqlTransaction = _sqlConnection.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);

                    if (oPaymentInsurace != null)
                    { EOBPaymentID = InsurancePayment.SaveEOBPayment(_sqlConnection, _sqlTransaction, oPaymentInsurace, false, out _nEOBID, out isEOBSaved, true, 0); }

                    if (IsNextActionUpdated)
                    { isEOBSaved = false; InsurancePayment.SaveEOBNextAction(_sqlConnection, _sqlTransaction, ref oNextActions, EOBPaymentID, _nEOBID, out isEOBSaved); }

                    if (isEOBSaved)
                    { _sqlTransaction.Commit(); }
                    else
                    { _sqlTransaction.Rollback(); throw new Exception("Unable to save payment"); }

                    _sqlConnection.Close();
                }

                #endregion

                if (isEOBSaved)
                {
                    #region " Split claim logic "

                    DataTable _dtHoldInfo = GetSplittedClaimsHoldInfo(oSplitClaimDetails);
                    
                    if (oSplitClaimDetails != null && oSplitClaimDetails.Lines.Count > 0)
                    {
                        bool _splitFlag = false;
                        gloSplitClaim ogloSplitClaim = new gloSplitClaim(AppSettings.ConnectionStringPM);
                        // Set the claim remittance ref no to the split object
                        oSplitClaimDetails.ClaimRemittanceReferenceNo = ClaimRemittanceReferenceNo;

                        // Setting EOB reference ID's
                        oSplitClaimDetails.EOBID = _nEOBID;
                        oSplitClaimDetails.EOBPaymentID = EOBPaymentID;

                        // Payment allocation check added to resolved the following issue
                        // Issue : if pending is selected, split should not happened.
                        if (IsPaymentAllocated)
                        { oSplitClaimDetails.IsPaymentDone = true; }
                        else
                        { oSplitClaimDetails.IsPaymentDone = false; }

                        //_splitFlag = ogloSplitClaim.SplitTransactionClaim(oSplitClaimDetails);
                        _splitFlag = ogloSplitClaim.SplitTransactionClaim(oSplitClaimDetails, _dtHoldInfo);

                        ogloSplitClaim.Dispose();
                    }

                    #endregion " Split claim logic "

                    #region " Save NextAction History "

                    isEOBSaved = false;
                    InsurancePayment.SaveEOBNextActionHistory(ref oNextActions, EOBPaymentID, _nEOBID, out isEOBSaved);

                    #endregion
                }

                #region " Reset parameters & controls "

                _IsDataSaved = true;

                IsPendingCheckLoaded = true;

                // Reset reserve added 
                IsReserveAdded = false;

                // Reset controls & load EOB
                SetupControls();
                

                EOBInsuranceReserveMasterLines.Clear();  

                LoadFormData(_EOBPaymentID, _nEOBID,true);

                // Reset the patient strip control
                PatientControl.ClearDetails();

                //***********************************************
                //By Debasish On 11th Apr 2011.
                //Re Initialize  Form Variables.
                this.PatientInsuranceID = 0;
                this.SelectedInsurancePlan = string.Empty;
                this.IsSelectedPlanOnHold = false;

                this.ContactInsuranceID = 0;
                //************************************************

                PatientControl.SelectSearchBox();
                //For Shweta
                //ReservePatientID = 0;
                //ReservePatientName = "";
                //ReserveMSTTransactionID = 0;
                //ReserveTransactionID = 0;
                //ReserveClaimNo = "";

                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _IsDataSaved = false;
            }
            finally
            {
                if (oPaymentInsurace != null) { oPaymentInsurace.Dispose(); }
                if (oEOBInsPaymentCreditDetail != null) { oEOBInsPaymentCreditDetail.Dispose(); }
                if (oPaymentInsuranceClaim != null) { oPaymentInsuranceClaim.Dispose(); }
                if (oEOBInsurancePaymentReserveDetail != null) { oEOBInsurancePaymentReserveDetail.Dispose(); }
                if (oSplitClaimDetails != null) { oSplitClaimDetails.Dispose(); }
            }

            return _IsDataSaved;
        }

        private void SetNextActionDetails(out EOBPayment.Common.PaymentInsuranceLineNextActions _PaymentInsuranceLineNextActions, out SplitClaimDetails _SplitClaimDetails)
        {
            EOBPayment.Common.PaymentInsuranceLineNextActions NextActions = new EOBPayment.Common.PaymentInsuranceLineNextActions();
            EOBPayment.Common.PaymentInsuranceLineNextAction LineNextAction = null;
            SplitClaimDetails oSplitClaimDetails = new SplitClaimDetails();
            EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new EOBPayment.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);

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
                            oSplitClaimDetails.ClinicID = AppSettings.ClinicID;
                            //oSplitClaimDetails.EOBPaymentID = EOBPaymentID;
                            //oSplitClaimDetails.EOBID = EOBID;
                        }
                    }

                    if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                    {
                        if (c1SinglePayment.GetData(i, COL_NEXT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT)).Trim() != "")
                        {
                            #region " Entire "

                            bool _addSplitLine = false;
                            //bool _isPendingLine = false;
                            LineNextAction = new EOBPayment.Common.PaymentInsuranceLineNextAction();
                            LineNextAction.ID = 0;
                            LineNextAction.ClaimNo = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                            //LineNextAction.EOBPaymentID = EOBPaymentID;
                            //LineNextAction.EOBID = EOBID;
                            LineNextAction.EOBPaymentDetailID = 0;

                            LineNextAction.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                            LineNextAction.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));

                            LineNextAction.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
                            LineNextAction.TrackBillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                            LineNextAction.TrackBillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));

                            //...Code added on 20100129 by Sagar Ghodke
                            //...Code added to set the close date,user for Responsibility
                            if (mskCloseDate.MaskCompleted == true)
                            {
                                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                LineNextAction.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                            }
                            LineNextAction.UserID = AppSettings.UserID;
                            LineNextAction.UserName = AppSettings.UserName;

                            //...ENd code add 20100129

                            string _nextAction = "";
                            string _nextActionCode = "";
                            string _nextActionDesc = "";

                            _nextAction = Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT)).Trim();
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
                            if (c1SinglePayment.GetData(i, COL_PARTY) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PARTY)).Trim() != "")
                            {
                                string _party = "";
                                string _partyCode = "";
                                string _partyDesc = "";
                                Int64 _partyInsId = 0;
                                Int64 _partyContactId = 0;

                                _party = Convert.ToString(c1SinglePayment.GetData(i, COL_PARTY)).Trim();
                                _partyCode = _party.Substring(0, _party.IndexOf('-'));
                                _partyDesc = _party.Substring(_party.IndexOf('-') + 1, (_party.Length - _party.IndexOf('-')) - 1);

                                //.... Get the next party insuranceid & contactid number for the claim
                                DataTable _dt = null;
                                _dt = PatientStripControl.GetInsuranceParties(PatientControl.ClaimNumber, PatientControl.PatientID); //_dt = ogloEOBPaymentInsurance.GetClaimInsurances(PatientControl.ClaimNumber, PatientControl.PatientID);

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

                                LineNextAction.NextActionPatientInsID = _partyInsId;
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
                    }
                }

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

        private DataTable GetSplittedClaimsHoldInfo(SplitClaimDetails oSplitClaimDetails)
        {
            DataTable _dtHoldInfo = null;
            DataRow _drParentClaimHoldNote = null;

            gloSplitClaim ogloSplitClaim = new gloSplitClaim(AppSettings.ConnectionStringPM);

            if (ClaimDetails.IsClaimOnHold)
            {
                _drParentClaimHoldNote = InsurancePayment.GetBillingHoldNote(oSplitClaimDetails.TransactionMasterID, oSplitClaimDetails.TransactionID);
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
            ogloSplitClaim.Dispose();
            return _dtHoldInfo;
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

            if (mskCloseDate.MaskCompleted == true)
            {
                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                oEOBInsPaymentResAsCreditDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                oEOBInsPaymentResAsCreditDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
            }

            oEOBInsPaymentResAsCreditDetail.CPTCode = "";
            oEOBInsPaymentResAsCreditDetail.CPTDescription = "";

            if (txtCheckAmount.Enabled == true)
            { oEOBInsPaymentResAsCreditDetail.Amount = EOBInsurancePaymentMasterLine.Amount; }
            else
            {
                //if (_TotalAllocation > (EOBInsurancePaymentMasterLine.Amount - EOBInsurancePaymentMasterLine.DBReserveAmount))
                //{
                //    //if (((EOBInsurancePaymentMasterLine.Amount - EOBInsurancePaymentMasterLine.DBReserveAmount) - _TotalAllocation) <= 0)
                //    //{
                //    //    oEOBInsPaymentResAsCreditDetail.Amount = EOBInsurancePaymentMasterLine.Amount;
                //    //}
                //    //else
                //    //{
                //        oEOBInsPaymentResAsCreditDetail.Amount = (EOBInsurancePaymentMasterLine.Amount - EOBInsurancePaymentMasterLine.DBReserveAmount);
                //    //}
                //}
                //else
                //{
                //    oEOBInsPaymentResAsCreditDetail.Amount = _TotalAllocation;
                //}

                //if (_TotalAllocation > (EOBInsurancePaymentMasterLine.Amount - EOBInsurancePaymentMasterLine.DBReserveAmount))
                //{
                //    if ((_TotalAllocation + EOBInsurancePaymentMasterLine.DBReserveAmount) >= EOBInsurancePaymentMasterLine.Amount)
                //    {
                //        oEOBInsPaymentResAsCreditDetail.Amount = EOBInsurancePaymentMasterLine.Amount;
                //    }
                //    else
                //    {
                //        oEOBInsPaymentResAsCreditDetail.Amount = (_TotalAllocation + EOBInsurancePaymentMasterLine.DBReserveAmount);
                //    }
                //}
                //else
                //{


              
                    if ((_TotalAllocation + EOBInsurancePaymentMasterLine.DBReserveAmount) >= EOBInsurancePaymentMasterLine.Amount)
                    {
                        oEOBInsPaymentResAsCreditDetail.Amount = EOBInsurancePaymentMasterLine.Amount;
                        _TotalAllocation = _TotalAllocation - (EOBInsurancePaymentMasterLine.Amount - EOBInsurancePaymentMasterLine.DBReserveAmount);
                        for (int selResLnIndex = 0; selResLnIndex < SelectedReserveMasterLines.Count; selResLnIndex++)
                        {
                            if (EOBInsurancePaymentMasterLine.ReserveEOBPaymentID == SelectedReserveMasterLines[selResLnIndex].ReserveEOBPaymentID)
                            {
                                SelectedReserveMasterLines[selResLnIndex].RemainingAmount = 0;
                                break;
                            }
                        } 
                    }
                    else
                    {
                        oEOBInsPaymentResAsCreditDetail.Amount = (_TotalAllocation + EOBInsurancePaymentMasterLine.DBReserveAmount);
                        _TotalAllocation = _TotalAllocation - oEOBInsPaymentResAsCreditDetail.Amount;
                        for (int selResLnIndex = 0; selResLnIndex < SelectedReserveMasterLines.Count; selResLnIndex++)
                        {
                            if (EOBInsurancePaymentMasterLine.ReserveEOBPaymentID == SelectedReserveMasterLines[selResLnIndex].ReserveEOBPaymentID)
                            {
                                SelectedReserveMasterLines[selResLnIndex].RemainingAmount = SelectedReserveMasterLines[selResLnIndex].Amount - oEOBInsPaymentResAsCreditDetail.Amount;
                                break;
                            }
                        }
                    }

                  
                        


                   // oEOBInsPaymentResAsCreditDetail.Amount = _TotalAllocation;
                //}


                    //_TotalAllocation = _TotalAllocation - (EOBInsurancePaymentMasterLine.Amount - EOBInsurancePaymentMasterLine.DBReserveAmount);
            }


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

            oEOBInsPaymentResAsCreditDetail.PatientID = PatientControl.PatientID; // _PatientID;
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
                if (mskCloseDate.MaskCompleted == true)
                {
                    mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                    oEOBInsPaymentResAsCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                }
            }
            //Added by Subashish_b on 23/Feb /2011 (integration made on date-10/May/2011) for  assigning the PAF  values to the properties
            oEOBInsPaymentResAsCreditDetail.PAccountID = this.nPAccountID;
            oEOBInsPaymentResAsCreditDetail.GuarantorID = this.nGuarantorID;
            oEOBInsPaymentResAsCreditDetail.AccountPatientID = this.nAccountPatientID;
            //End
            #endregion
           
            return oEOBInsPaymentResAsCreditDetail;
        }

        private EOBInsurancePaymentDetail GetMainCreditLineEntry(int FinanceLineNo)
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

            if (mskCloseDate.MaskCompleted == true)
            {
                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                oEOBInsPaymentCreditDetail.DOSFrom = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                oEOBInsPaymentCreditDetail.DOSTo = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
            }

            if (CheckAmount != 0) //(txtCheckAmount.Text.Trim().Length > 0)
            {
                oEOBInsPaymentCreditDetail.Amount = CheckAmount; //Convert.ToDecimal(txtCheckAmount.Text);
                oEOBInsPaymentCreditDetail.IsNullAmount = false;
            }

            oEOBInsPaymentCreditDetail.PaymentType = EOBPaymentType.InsuracePayment;
            oEOBInsPaymentCreditDetail.PaymentSubType = EOBPaymentSubType.Insurace;
            oEOBInsPaymentCreditDetail.PaySign = EOBPaymentSign.Payment_Credit;
            oEOBInsPaymentCreditDetail.PayMode = SelectedPaymentMode;

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

            oEOBInsPaymentCreditDetail.PatientID = PatientControl.PatientID;
            oEOBInsPaymentCreditDetail.PaymentTrayID = SelectedPaymentTrayID;
            oEOBInsPaymentCreditDetail.PaymentTrayCode = SelectedPaymentTrayCode;
            oEOBInsPaymentCreditDetail.PaymentTrayDescription = SelectedPaymentTray;
            oEOBInsPaymentCreditDetail.UserID = AppSettings.UserID;
            oEOBInsPaymentCreditDetail.UserName = AppSettings.UserName;
            oEOBInsPaymentCreditDetail.ClinicID = AppSettings.ClinicID;

            //Added by Subashish_b on 22/Feb /2011 (integration made on date-10/May/2011) for  assigning the PAF values to the save object
            oEOBInsPaymentCreditDetail.PAccountID = this.nPAccountID;
            oEOBInsPaymentCreditDetail.GuarantorID = this.nGuarantorID;
            oEOBInsPaymentCreditDetail.AccountPatientID = this.nAccountPatientID;
            //End

            oEOBInsPaymentCreditDetail.FinanceLieNo = FinanceLineNo;
            oEOBInsPaymentCreditDetail.IsMainCreditLine = true;

            if (mskCloseDate.MaskCompleted == true)
            {
                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                oEOBInsPaymentCreditDetail.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
            }

            return oEOBInsPaymentCreditDetail;
        }

        private PaymentInsurance GetPaymentMaster()
        {
            EOBPayment.Common.PaymentInsurance oPaymentInsurace = new EOBPayment.Common.PaymentInsurance();

            oPaymentInsurace.PaymentNumber = lblPaymetNo.Text.Trim().Split('#')[1];
            oPaymentInsurace.PaymentNumberPefix = _paymentPrefix;
            oPaymentInsurace.EOBPaymentID = _EOBPaymentID;
            oPaymentInsurace.EOBRefNO = txtEOBRefNumber.Text.Trim();

            //1. InsuranceID //2. InsuranceName //3 InsuraceSelfMode //4. ContactID
            oPaymentInsurace.PayerID = SelectedInsuranceCompanyID;
            oPaymentInsurace.PayerName = SelectedInsuranceCompany;
            oPaymentInsurace.PayerType = EOBPaymentAccountType.InsuranceCompany;

            oPaymentInsurace.PaymentMode = SelectedPaymentMode;
            oPaymentInsurace.CheckNumber = txtCheckNumber.Text.Trim(); ;
            oPaymentInsurace.CheckAmount = CheckAmount;

            if (mskCheckDate.MaskCompleted)
            {
                mskCheckDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                oPaymentInsurace.CheckDate = gloDateMaster.gloDate.DateAsNumber(mskCheckDate.Text);
            }

            if (SelectedPaymentMode == EOBPaymentMode.CreditCard)
            {
                oPaymentInsurace.CardType = cmbCardType.Text.Trim();
                oPaymentInsurace.AuthorizationNo = txtCardAuthorizationNo.Text.Trim();
                if (mskCreditExpiryDate.MaskCompleted)
                {
                    mskCreditExpiryDate.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
                    oPaymentInsurace.CardExpiryDate = Convert.ToInt64(mskCreditExpiryDate.Text);
                }
                oPaymentInsurace.CardID = Convert.ToInt64(cmbCardType.SelectedValue);
            }

            oPaymentInsurace.MSTAccountID = SelectedInsuranceCompanyID;
            oPaymentInsurace.MSTAccountType = EOBPaymentAccountType.InsuranceCompany;

            oPaymentInsurace.ClinicID = AppSettings.ClinicID;
            oPaymentInsurace.CreatedDateTime = DateTime.Now;
            oPaymentInsurace.ModifiedDateTime = DateTime.Now;

            oPaymentInsurace.PaymentTrayID = SelectedPaymentTrayID;
            oPaymentInsurace.PaymentTrayCode = SelectedPaymentTrayCode;
            oPaymentInsurace.PaymentTrayDesc = SelectedPaymentTray;

            if (mskCloseDate.MaskCompleted == true)
            {
                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                oPaymentInsurace.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
            }

            oPaymentInsurace.UserID = AppSettings.UserID;
            oPaymentInsurace.UserName = AppSettings.UserName;

            //Added by Subashish_b on 22/Feb /2011 (integration made on date-10/May/2011) for  assigning the PAF Values to the save object
            oPaymentInsurace.PAccountID = this.nPAccountID;
            oPaymentInsurace.AccountPatientID = this.nAccountPatientID;
            oPaymentInsurace.GuarantorID = this.nGuarantorID;
            //End

            #region "Payment Master Note"

            //Notes if any to main payment to all claim OR main payment to reserve account
            if (txtPayMstNotes.Text.Trim().Length > 0)
            {
                EOBPayment.Common.PaymentInsuranceLineNote oPaymentInsuranceLineNote = new global::gloBilling.EOBPayment.Common.PaymentInsuranceLineNote();

                oPaymentInsuranceLineNote.EOBPaymentID = _EOBPaymentID;
                oPaymentInsuranceLineNote.Description = txtPayMstNotes.Text.Trim();
                oPaymentInsuranceLineNote.Amount = CheckAmount;
                oPaymentInsuranceLineNote.IncludeOnPrint = chkPayMstIncludeNotes.Checked;
                oPaymentInsuranceLineNote.ClinicID = AppSettings.ClinicID;
                oPaymentInsuranceLineNote.PaymentNoteType = EOBPaymentType.InsuracePayment;
                oPaymentInsuranceLineNote.PaymentNoteSubType = EOBPaymentSubType.Other;
                oPaymentInsuranceLineNote.HasData = true;
                oPaymentInsuranceLineNote.CloseDate = oPaymentInsurace.CloseDate;
                oPaymentInsuranceLineNote.UserId = AppSettings.UserID;

                oPaymentInsurace.Notes.Add(oPaymentInsuranceLineNote);
                oPaymentInsuranceLineNote.Dispose();
            }

            #endregion

            return oPaymentInsurace;
        }

        string GetSelectedReasonCode(int ColumnIndex)
        {
            string _reasonCode = string.Empty;

            gloSettings.GeneralSettings oSetting = new gloSettings.GeneralSettings(AppSettings.ConnectionStringPM);
            object oSetValue = null;

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

            return _reasonCode;
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
                            //gloBilling ogloBilling = new gloBilling(AppSettings.ConnectionStringPM, string.Empty);
                            //ogloBilling.ShowModifyCharges(PatientControl.PatientID, _claimDetails.TransactionID, true);
                            //ogloBilling.Dispose();
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
                // If service line is belongs to splitted claim...
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
                        c1SinglePaymentCorrTB.SetData(e.Row, COL_NEXT, null);
                        c1SinglePaymentCorrTB.SetData(e.Row, COL_PARTY, null);
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
                _insuranceID = InsurancePayment.GetClaimInsuranceID(PatientControl.ClaimNumber, _selectedResponsibilityNo);
                _isBilled = InsurancePayment.IsResponsibilityBilled(PatientControl.ClaimNumber, PatientControl.SubClaimNumber, _insuranceID, PatientControl.GetSelectedPartyResponsibility(), _selectedAction);
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

            if (!_selectedParty.Equals(string.Empty)) { _selectedResponsibilityNo = Convert.ToInt32(_selectedParty.Substring(0, 1)); }
            if (!_currentResponsibility.Equals(string.Empty)) { _currentResponsibilityNo = Convert.ToInt32(_currentResponsibility.Substring(0, 1)); }
            if (!_nextResponsibility.Equals(string.Empty)) { _nextResponsibilityNo = Convert.ToInt32(_nextResponsibility.Substring(0, 1)); }

            bool _isBilled = IsPartyBilled(_selectedParty, _selectedAction);

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
                            c1SinglePaymentCorrTB.SetData(e.Row, COL_PARTY, null);
                        }
                    }
                    else
                    { _isValid = true; }

                    #region " Old validation rules uptill 5050"

                    //// let user bill any party that has not been billed earlier.
                    //// Check if Selected Party is greater than Current Party
                    //if (_selectedResponsibilityNo > _currentResponsibilityNo)
                    //{ _isValid = true; }
                    //else
                    //{
                    //    MessageBox.Show("Party " + _selectedResponsibilityNo + " has already been billed. Select a different next party option.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    _isValid = false;
                    //    SetDefaultParty(_selectedAction, string.Empty, _currentResponsibility, _nextResponsibility, false, e);
                    //}

                    #endregion

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
                                c1SinglePaymentCorrTB.SetData(e.Row, COL_PARTY, null);
                            }
                        }
                        else
                        { _isValid = true; }
                    }
                    #region " Old validation rules uptill 5050"

                    //// let user Rebill any party that has already been billed, even if not the current party.
                    //// Check if Selected Party is less than Current Party if Yes 
                    //if (_selectedResponsibilityNo <= _currentResponsibilityNo)
                    //{ _isValid = true; }
                    //else
                    //{
                    //    MessageBox.Show("Party " + _selectedResponsibilityNo + " has not been billed, so it can not be rebilled. Select a different next party option.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    _isValid = false;
                    //    SetDefaultParty(_selectedAction, string.Empty, _currentResponsibility, _nextResponsibility, false, e);
                    //}

                    #endregion
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
                                c1SinglePaymentCorrTB.SetData(e.Row, COL_PARTY, null);
                            }

                        }
                        else
                        { _isValid = true; }
                    }

                    #region " Old validation rules uptill 5050"

                    //TODO : Commented for imapact analysis

                    //// let user Select any party that has not been billed yet or Current Party. but not the Self Party
                    //// Check if Selected Party is greater than or equal party Current Party 
                    //if (_selectedResponsibilityNo >= _currentResponsibilityNo)
                    //{
                    //    if (_selectedResponsibilityNo.Equals(_selfResponsibilityNo))
                    //    {
                    //        MessageBox.Show("Party " + _selectedResponsibilityNo + " can not be selected for pending.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        _isValid = false;

                    //        // Note that IsSelfPartySelected flag is passed true
                    //        // So that the function will take care of the rest selection
                    //        SetDefaultParty(_selectedAction, string.Empty, _currentResponsibility, _nextResponsibility, false, e);
                    //    }
                    //    else
                    //    { _isValid = true; }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Party " + _selectedResponsibilityNo + " has already been billed. Select a different next party option.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    _isValid = false;
                    //    SetDefaultParty(_selectedAction, string.Empty, _currentResponsibility, _nextResponsibility, false, e);
                    //}

                    #endregion

                }
                else if (_selectedAction.StartsWith("N")) // N = None
                {
                    #region " Old validation rules uptill 5050"
                    //TODO : Commented for imapact analysis

                    //// Check if Selected Party is Current Party 
                    //if (_selectedResponsibilityNo == _currentResponsibilityNo)
                    //{ _isValid = true; }
                    //else
                    //{
                    //    MessageBox.Show("Party " + _selectedResponsibilityNo + " can not be selected. Select a current party option.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    _isValid = false;
                    //    SetDefaultParty(_selectedAction, string.Empty, _currentResponsibility, _nextResponsibility, false, e);
                    //}


                    ////// If Party is selected for None action, then don't show any message box.
                    ////_isValid = false;
                    ////SetDefaultParty(_selectedAction, string.Empty, _currentResponsibility, _nextResponsibility, false, e);

                    #endregion
                }
                else if (_selectedAction.Equals(string.Empty))
                {
                    MessageBox.Show("Please select next action, before selecting any insurance party.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _isValid = false;
                    SetDefaultParty(_selectedAction, string.Empty, _currentResponsibility, _nextResponsibility, false, e);

                    _IsValidEntered = false;
                    if (e.Row <= c1SinglePaymentCorrTB.Rows.Count - 1)
                    {
                        c1SinglePaymentCorrTB.SetData(e.Row, COL_PARTY, c1SinglePayment.GetData(e.Row, COL_PARTY));
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
            if (_value < 0)
            {
                MessageBox.Show("Payment amount cannot be negative", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isValidContext = false;
                _IsValidEntered = false;
                // If allowed then reset to last allowed amount else set to null
                if (e.Col.Equals(COL_ALLOWED))
                {
                    c1SinglePayment.SetData(e.Row, e.Col, _allowedAmountBeforeEdit);

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
                else
                {
                    c1SinglePayment.SetData(e.Row, e.Col, null);
                    c1SinglePaymentCorrTB.SetData(e.Row, e.Col, null);
                }

                if (e.Row <= c1SinglePaymentCorrTB.Rows.Count - 1)
                {
                    //c1SinglePaymentCorrTB.SetData(e.Row, e.Col, null);
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
                            c1SinglePaymentCorrTB.SetData(e.Row, COL_ALLOWED, null);
                        }
                    }
                    #region " old code for allowed amount validation"
                    //Shifted to a common function CheckAllowedAmount() 
                    //
                    //if (PatientControl.SelectedInsuranceParty.Equals(1) && (e.Col.Equals(COL_ALLOWED) || e.Col.Equals(COL_PAYMENT)))
                    //{
                    //    if (_allowed > _charges)
                    //    {
                    //        MessageBox.Show("Allowed amount for charge " + (e.Row-1) + " ('" + _cptCode + "')" + " should not be greater than charge amount. ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        //MessageBox.Show("Allowed amount cannot be greater than Charges ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        _isValidContext = false;
                    //        c1SinglePayment.SetData(e.Row, e.Col, _allowedAmountBeforeEdit);
                    //    }
                    //}
                    #endregion " old code for allowed amount validation"
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
                    EOBPayment.gloEOBPaymentInsurance ogloIns = new global::gloBilling.EOBPayment.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);
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
                if (SelectedPaymentMode == EOBPaymentMode.Check)
                {
                    if (!IsPaymentInProcess)
                    {
                        string _checkNo = "";
                        Int64 _checkDate = 0;
                        string _showCheckDate = "";

                        if (txtCheckNumber.Text.Trim() != "")
                        { _checkNo = txtCheckNumber.Text.Trim(); }

                        if (mskCheckDate.MaskCompleted == true)
                        {
                            _checkDate = gloDateMaster.gloDate.DateAsNumber(mskCheckDate.Text);
                            _showCheckDate = mskCheckDate.Text;
                        }
                        bool isCheckExist = InsurancePayment.IsExistCheck(_checkNo, _checkDate, CheckAmount, EOBPaymentAccountType.InsuranceCompany);
                        if (isCheckExist)
                        {
                            DialogResult _checkDlg = DialogResult.None;
                            string _message = "";
                            _message = " Same Check with Check#: " + _checkNo + ", Check Date: " + _showCheckDate + Environment.NewLine + " and Check Amount: $" + CheckAmount.ToString("#0.00") + " " + Environment.NewLine + " already exists in the system." + Environment.NewLine + " Do you want to continue with save? ";
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

        //private bool CheckChargeLineBalances()
        //{
        //    C1.Win.C1FlexGrid.C1FlexGrid c1OtherReason = null;

        //    if (PatientControl != null && PatientControl.PatientID > 0 && PatientControl.ClaimNumber > 0 && PatientControl.HasSecondaryInsOnClaim == true)
        //    {
        //        // 1 : First Party 
        //        if (PatientControl.SelectedInsuranceParty.Equals(1)) // Check for the balance only for first party, else skip the checking.
        //        {
        //            if (c1SinglePayment != null && c1SinglePayment.Rows.Count > 1)
        //            {
        //                for (int rIndex = 1; rIndex < c1SinglePayment.Rows.Count; rIndex++)
        //                {
        //                    if (c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(rIndex, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
        //                    {
        //                        decimal _currentPaymentAmt = 0;
        //                        decimal _currentWriteOffAmt = 0;
        //                        decimal _cuurentCopayAmt = 0;
        //                        decimal _currentDeductibleAmt = 0;
        //                        decimal _currentCoInsAmt = 0;
        //                        decimal _currentWithholdAmt = 0;
        //                        decimal _currentOtherAdjustmentAmt = 0;
        //                        decimal _totalCharges = 0;
        //                        string _cptCode = "";

        //                        if (c1SinglePayment.GetData(rIndex, COL_PAYMENT) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_PAYMENT)).Trim() != "")
        //                        { _currentPaymentAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_PAYMENT)); }
        //                        if (c1SinglePayment.GetData(rIndex, COL_WRITEOFF) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_WRITEOFF)).Trim() != "")
        //                        { _currentWriteOffAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_WRITEOFF)); }
        //                        if (c1SinglePayment.GetData(rIndex, COL_COPAY) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_COPAY)).Trim() != "")
        //                        { _cuurentCopayAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_COPAY)); }
        //                        if (c1SinglePayment.GetData(rIndex, COL_DEDUCTIBLE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_DEDUCTIBLE)).Trim() != "")
        //                        { _currentDeductibleAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_DEDUCTIBLE)); }
        //                        if (c1SinglePayment.GetData(rIndex, COL_COINSURANCE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_COINSURANCE)).Trim() != "")
        //                        { _currentCoInsAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_COINSURANCE)); }
        //                        if (c1SinglePayment.GetData(rIndex, COL_WITHHOLD) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_WITHHOLD)).Trim() != "")
        //                        { _currentWithholdAmt = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_WITHHOLD)); }
        //                        if (c1SinglePayment.GetData(rIndex, COL_TOTALCHARGE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_TOTALCHARGE)).Trim() != "")
        //                        { _totalCharges = Convert.ToDecimal(c1SinglePayment.GetData(rIndex, COL_TOTALCHARGE)); }
        //                        if (c1SinglePayment.GetData(rIndex, COL_CPT_CODE) != null && Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CPT_CODE)).Trim() != "")
        //                        { _cptCode = Convert.ToString(c1SinglePayment.GetData(rIndex, COL_CPT_CODE)); }

        //                        //Start - Code added by Sagar Ghodke on 2011 May 04
        //                        //Code added for including the other reason code amount in the claim balance verification formula

        //                        c1OtherReason = null;

        //                        if (c1SinglePayment.GetData(rIndex, COL_REASON) != null)
        //                        {
        //                            _currentOtherAdjustmentAmt = 0;
        //                            c1OtherReason = ((C1.Win.C1FlexGrid.C1FlexGrid)c1SinglePayment.GetData(rIndex, COL_REASON));
        //                            if (c1OtherReason != null && c1OtherReason.Rows.Count > 1)
        //                            {
        //                                for (int rCodeIndex = 1; rCodeIndex < c1OtherReason.Rows.Count; rCodeIndex++)
        //                                {
        //                                    if (c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["Amount"].Index) != null
        //                                        && Convert.ToString(c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["Amount"].Index)).Trim() != "")
        //                                    {
        //                                        try
        //                                        {
        //                                            _currentOtherAdjustmentAmt += Convert.ToDecimal(c1OtherReason.GetData(rCodeIndex, c1OtherReason.Cols["Amount"].Index));
        //                                        }
        //                                        catch (Exception)
        //                                        {
        //                                            continue;
        //                                        }

        //                                    }
        //                                }

        //                            }
        //                        }

        //                        //_currentOtherAdjustmentAmt

        //                        //End - Code added by Sagar Ghodke on 2011 May 04



        //                        if (_totalCharges != (_currentPaymentAmt + _currentWriteOffAmt + _cuurentCopayAmt + _currentDeductibleAmt + _currentCoInsAmt + _currentWithholdAmt + _currentOtherAdjustmentAmt))
        //                        {
        //                            DialogResult _dlgRst = DialogResult.None;

        //                            string _warningMsg = "";
        //                            //Start - Code added by Sagar Ghodke on 2011 May 04
        //                            //Message change request for 6022 about claim balancing 
        //                            //Below commented code is existing message text.


        //                            //_warningMsg =
        //                            //" Warning:  Remittance for Charge " + (rIndex - 1) + " ('" + _cptCode + "') does not Balance." + Environment.NewLine + "" +
        //                            //" Billed Amount – Insurance Paid must equal sum of Writedown +" + Environment.NewLine + "" +
        //                            //" Deductible + Copay + Coins + Other Reasons." + Environment.NewLine + "" +
        //                            //" Review entry before continuing?";

        //                            _warningMsg =
        //                        " Warning:  Remittance for Charge " + (rIndex - 1) + " ('" + _cptCode + "') does not Balance." + Environment.NewLine + " " + Environment.NewLine + "" +
        //                        " This can cause secondary payer billing rejections. " + Environment.NewLine + " " + Environment.NewLine + "" +
        //                        " Remittances must add up according to this formula: " + Environment.NewLine + "" +
        //                        " Billed Amount – Insurance Payment must equal sum of " + Environment.NewLine + "" +
        //                        " Write-off + Copay + Deduct + Co-ins + Withhold + Other Reasons." + Environment.NewLine + " " + Environment.NewLine + "" +
        //                        " Correct the remittance?";

        //                            //End - Code added by Sagar Ghodke on 2011 May 04

        //                            _dlgRst = MessageBox.Show(_warningMsg, AppSettings.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
        //                            if (_dlgRst == DialogResult.No)
        //                            {
        //                                continue;
        //                            }
        //                            else
        //                            {
        //                                c1SinglePayment.Focus();
        //                                c1SinglePayment.Select(rIndex, COL_PAYMENT, true);
        //                                return false;
        //                            }

        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    //Start - Code added by Sagar Ghodke on 2011 May 04
        //    //Dispose temp. c1 grid varible of other reason code amount
        //    c1OtherReason.Dispose();
        //    //End - Code added by Sagar Ghodke on 2011 May 04

        //    return true;
        //}

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

         
          
            //if (PatientControl != null && PatientControl.PatientID > 0 && PatientControl.ClaimNumber > 0 && PatientControl.HasSecondaryInsOnClaim == true)
            if (PatientControl != null && PatientControl.PatientID > 0 && PatientControl.ClaimNumber > 0 && IsInsuranceParty() == true )
            {
                // 1 : First Party 
                //if (PatientControl.SelectedInsuranceParty.Equals(1)) // Check for the balance only for first party, else skip the checking.
                //if(PatientControl.Resposebility.Equals(2))
               // {
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

                                //Start - Code added by Sagar Ghodke on 2011 May 04
                                //Code added for including the other reason code amount in the claim balance verification formula



                                if (c1SinglePayment.GetData(rIndex, COL_REASON) != null)
                                {
                                    _currentOtherAdjustmentAmt = 0;

                                    #region " Get the other reason code amount "

                                    //**Note : Do not dispose this c1 object since it is refrenced directly 
                                    //         this will cause the original object dispose.

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



                                //End - Code added by Sagar Ghodke on 2011 May 04

                                //if (_totalCharges != (_currentPaymentAmt + _currentWriteOffAmt + _cuurentCopayAmt + _currentDeductibleAmt + _currentCoInsAmt + _currentWithholdAmt + _currentOtherAdjustmentAmt))
                                if ((_totalCharges - _currentPaymentAmt) != (_currentWriteOffAmt + _cuurentCopayAmt + _currentDeductibleAmt + _currentCoInsAmt + _currentWithholdAmt + _currentOtherAdjustmentAmt))
                                {
                                    DialogResult _dlgRst = DialogResult.None;

                                    #region " Build Warning Message Text "

                                    string _warningMsg = "";

                                    //Start - Code added by Sagar Ghodke on 2011 May 04
                                    //Message change request for 6022 about claim balancing 
                                    //Below commented code is existing message text.

                                    //_warningMsg =
                                    //" Warning:  Remittance for Charge " + (rIndex - 1) + " ('" + _cptCode + "') does not Balance." + Environment.NewLine + "" +
                                    //" Billed Amount – Insurance Paid must equal sum of Writedown +" + Environment.NewLine + "" +
                                    //" Deductible + Copay + Coins + Other Reasons." + Environment.NewLine + "" +
                                    //" Review entry before continuing?";

                                    _warningMsg =
                                    " Warning:  Remittance for Charge " + (rIndex - 1) + " ('" + _cptCode + "') does not Balance." + Environment.NewLine + " " + Environment.NewLine + "" +
                                    " This can cause secondary payer billing rejections. " + Environment.NewLine + " " + Environment.NewLine + "" +
                                    " Remittances must add up according to this formula: " + Environment.NewLine + "" +
                                    " Billed Amount – Insurance Payment must equal sum of " + Environment.NewLine + "" +
                                    " Write-off + Copay + Deduct + Co-ins + Withhold + Other Reasons." + Environment.NewLine + " " + Environment.NewLine + "" +
                                    " Correct the remittance?";

                                    //End - Code added by Sagar Ghodke on 2011 May 04

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
               // }
            }


            return true;
        }
        //claim remittance balance issue solved 1.Included the other reason code amount in balancing formula 2.
        private bool IsInsuranceParty()
        {
            string _nextParty = PatientControl.GetNextParty();
            string[] party = _nextParty.Split('-');

            if (party.Length > 1)
            {
                if (Convert.ToString(party[1]).Equals("SELF"))
                {
                    return false;
                    
                }
              
            }

            return true;
        }
        //end.

        #endregion

        #region " Save Validations "

        private bool SavePaymentValidation(bool isPaymentDone)
        {
            bool _isValidSave = true;
            bool _isValidInsCompany = true;

            //If only special character is present the set to zero
            try
            { Convert.ToDecimal(txtCheckAmount.Text.Trim()); }
            catch
            { 
               // decimal _amt = 0; 
            } //txtCheckAmount.Text = _amt.ToString("#0.00"); 


            #region "Complete Payments before Daily Close"

            if (!PerformDailyCloseValidation())
            {
                _isValidSave = false;
                return _isValidSave;
            }

            #endregion


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

            #region "Use Reserve Insurance Company Validation"

            for (int i = 0; i <= EOBInsurancePaymentMasterLines.Count - 1; i++)
            {
                if (EOBInsurancePaymentMasterLines[i].InsuranceCompanyID != 0)
                {
                    if (SelectedInsuranceCompanyID != EOBInsurancePaymentMasterLines[i].InsuranceCompanyID)
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
                        if (SelectedPaymentMode == EOBPaymentMode.Check)
                        { _mode = "Check"; }
                        else if (SelectedPaymentMode == EOBPaymentMode.MoneyOrder)
                        { _mode = "MO"; }
                        else if (SelectedPaymentMode == EOBPaymentMode.EFT)
                        { _mode = "EFT"; }

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

                if (IsPaymentAllocated)
                {
                    #region " Check for valid allowed amount "

                    if (CheckAllowedAmount() == false)
                    {
                        _isValidSave = false;
                        return _isValidSave;
                    }

                    #endregion " Check for allowed amount "

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
            // solving sales force case - GLO2011-0011711
            else if (IsNextActionUpdated == false )
            {
                #region " Payment mode selection "

                if (!IsValidPaymentMode())
                {
                    _isValidSave = false;
                    return _isValidSave;
                }

                #endregion
            }
         // end sa;es force case.

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
                //Boolean bReplacementClaim = false;

                for (int rowCount = 1; rowCount <= c1SinglePayment.Rows.Count - 1; rowCount++)
                {
                    if ((c1SinglePayment.GetData(rowCount, COL_NEXT) != null) && (Convert.ToString(c1SinglePayment.GetData(rowCount, COL_NEXT)).Trim() != string.Empty))
                    {
                        if (c1SinglePayment.GetData(rowCount, COL_NEXT).ToString().ToUpper().StartsWith("B"))
                        {
                            //if ((c1SinglePayment.GetData(rowCount, COL_TRACK_BILLING_TRANSACTON_ID) != null) && (Convert.ToString(c1SinglePayment.GetData(rowCount, COL_TRACK_BILLING_TRANSACTON_ID)).Trim() != string.Empty))
                            //{
                            //    // bReplacementClaim = InsurancePayment.IsClaimReplacement(Convert.ToInt64(c1SinglePayment.GetData(rowCount, COL_TRACK_BILLING_TRANSACTON_ID)));

                            //}
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

                if (PatientControl.SelectedInsuranceParty.Equals(1))
                {
                    if (_allowed > _charges)
                    {
                        _isValidContext = false;
                        MessageBox.Show("Allowed amount for charge " + (rowIndex - 1) + " ('" + _cptCode + "')" + " should not be greater than charge amount. ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
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
                //if (_IsActionSelectedForAllCPTs.Equals(false)) // If action not selected for any cpt show an error message..
                //{
                //    //MessageBox.Show("Next action is not selected for the line " + _line + ".", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return false;
                //}
            }
            else
            {
                _IsActionSelectedForAllCPTs = false;
            }
            return _IsActionSelectedForAllCPTs;
        }

        private bool IsInsurancePlanSelected()
        {
            if (ContactInsuranceID.Equals(0)) //(lblInsCompany.Tag == null)
            {
                MessageBox.Show("Please select Insurance Plan.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private bool IsInsuranceCompanySelected()
        {
            if (SelectedInsuranceCompanyID.Equals(0)) //(lblInsCompany.Tag == null)
            {
                MessageBox.Show("Please select Insurance Company.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private bool IsValidCloseDate()
        {
            if (mskCloseDate.MaskCompleted == false)
            {
                MessageBox.Show("Please enter the close date", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                mskCloseDate.Focus();
                mskCloseDate.Select();
                return false;
            }

            //if (gloDate.IsValid(mskCloseDate.Text) == false)
            {
                CancelEventArgs e = new CancelEventArgs();
                ValidateDate(mskCloseDate, e);
                if (e.Cancel)
                { return false; }
            }
            //else
            {
                #region " check for day closed "

                gloBilling ogloBilling = new gloBilling(AppSettings.ConnectionStringPM, string.Empty);
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
                    if (ogloBilling != null)
                    {
                        ogloBilling.Dispose();
                        ogloBilling = null;
                    }
                }

                #endregion " check for day closed  "

                #region " check for future date "

                if (Convert.ToDateTime(mskCloseDate.Text.Trim()).Date > DateTime.Now.Date.AddDays(7))
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
            }
            return true;
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
            else if (InsurancePayment.IsPaymentTrayActive(SelectedPaymentTrayID) == false)
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
            if (SelectedPaymentMode == EOBPaymentMode.None)
            {
                MessageBox.Show("Please select the payment mode.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbPayMode.Select();
                cmbPayMode.Focus();
                return false;
            }
            else if (SelectedPaymentMode == EOBPaymentMode.CreditCard)
            {
                if (mskCheckDate.MaskCompleted == false)
                {
                    MessageBox.Show("Please enter the " + SelectedPaymentMode.ToString().ToLower() + " date.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("Please enter valid " + SelectedPaymentMode.ToString().ToLower() + " expiration date (MM/yy).", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mskCreditExpiryDate.Select();
                        mskCreditExpiryDate.Focus();
                        return false;
                    }
                }

            }
            // If payment mode is check & EFT, number & date is mandatory.
            // Note : for MO both are optional
            else if ((SelectedPaymentMode == EOBPaymentMode.Check) || (SelectedPaymentMode == EOBPaymentMode.EFT) || (SelectedPaymentMode == EOBPaymentMode.MoneyOrder))
            {
                string _mode = string.Empty;

                if (SelectedPaymentMode == EOBPaymentMode.Check)
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
            //else if (SelectedPaymentMode == EOBPaymentMode.EFT)
            //{
            //    if (txtCheckNumber.Text.Trim() == "")
            //    {
            //        MessageBox.Show("Please enter the " + SelectedPaymentMode.ToString().ToUpper() + " number.", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        txtCheckNumber.Select();
            //        txtCheckNumber.Focus();
            //        return false;
            //    }
            //}
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
                if ((IsReserveUsed) && (IsPaymentAllocated))
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
                    if (SelectedPaymentMode == EOBPaymentMode.Check && txtCheckNumber.Text.Trim() != "" && _EOBPaymentID == 0)
                    { return true; }
                    else if (SelectedPaymentMode == EOBPaymentMode.EFT && txtCheckNumber.Text.Trim() != "" && _EOBPaymentID == 0)
                    { return true; }
                    else if (SelectedPaymentMode == EOBPaymentMode.MoneyOrder && txtCheckNumber.Text.Trim() != "" && _EOBPaymentID == 0)
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

        //private bool IsCheckUpdating()
        //{
        //    bool isCheckUpdating = false;
        //    string _checkNumber = Convert.ToString(txtCheckNumber.Text);
        //    int _paymentMode = SelectedPaymentMode.GetHashCode();
        //    Int64 _checkDate = 0;

        //    if (mskCheckDate.MaskCompleted)
        //    {
        //        mskCheckDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        //        _checkDate = gloDateMaster.gloDate.DateAsNumber(mskCheckDate.Text);
        //    }
        //    isCheckUpdating = InsurancePayment.IsCheckUpdating(EOBPaymentID, _checkNumber, _checkDate, _paymentMode, CheckAmount);

        //    return isCheckUpdating;
        //}


        private bool IsCheckUpdating()
        {
            bool isCheckUpdating = false;
            //bool _isAppyRules = false;
            string _checkNumber = Convert.ToString(txtCheckNumber.Text);
            int _paymentMode = SelectedPaymentMode.GetHashCode();
            Int64 _checkDate = 0;

            if (mskCheckDate.MaskCompleted)
            {
                mskCheckDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                _checkDate = gloDateMaster.gloDate.DateAsNumber(mskCheckDate.Text);
            }
            if (EOBPaymentID > 0)
                isCheckUpdating = InsurancePayment.IsCheckUpdating(EOBPaymentID, _checkNumber, _checkDate, _paymentMode, CheckAmount);




            return isCheckUpdating;
        }


        private bool PerformDailyCloseValidation()
        {
            bool _isAppyRules = false;
            bool _isvalid = false;
            mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
            try
            {
                _isAppyRules = InsurancePayment.GetDialyCloseValidationSetting(AppSettings.ClinicID);
                //Payment with closed close date and completed.

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
                    else if (lblTakeBackAmt.Text != "" && Convert.ToDecimal(lblTakeBackAmt.Text) > 0  )
                    {
                        SendAmountToReserve(Convert.ToDecimal(lblTakeBackAmt.Text));
                        try
                        {
                         _remaining = Convert.ToDecimal(txtCheckRemaining.Text);
                        }
                        catch 
                        {
                            _remaining =0;
                        }

                        if (IsReserveAdded == true && _remaining == 0)
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
            DialogResult _result = MessageBox.Show("Do you want to save changes?", AppSettings.MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

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
            return _result;
        }

        #endregion

        #endregion

        #region " C1 Grid Design Methods "

        private void DesignPaymentGrid(C1FlexGrid c1Payment)
        {
            try
            {


                //_isFormLoading = true;
                c1Payment.Redraw = false;
                c1Payment.ScrollBars = ScrollBars.None;
                c1Payment.AllowSorting = AllowSortingEnum.None;

                c1Payment.Clear();
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
                        //c1Payment.SetData(0, COL_DEDUCTIBLE, "Ded");
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
                        //c1Payment.SetData(0, COL_DEDUCTIBLE, "Ded");
                        c1Payment.SetData(0, COL_DEDUCTIBLE, "Deduct");
                        c1Payment.SetData(0, COL_COINSURANCE, "Co-ins");
                        c1Payment.SetData(0, COL_WITHHOLD, "Withhold");
                    }

                    //c1Payment.SetData(0, COL_PREVPAID, "Prev Pd");
                    c1Payment.SetData(0, COL_PREVPAID, "Prev Paid");

                    c1Payment.SetData(0, COL_LAST_ALLOWED, "Last Allowed");
                    c1Payment.SetData(0, COL_LAST_PAYMENT, "Last Payment");
                    c1Payment.SetData(0, COL_LAST_WRITEOFF, "Last W/O");
                    c1Payment.SetData(0, COL_LAST_COPAY, "Last Copay");
                    c1Payment.SetData(0, COL_LAST_DEDUCTIBLE, "Last Ded");
                    c1Payment.SetData(0, COL_LAST_COINSURANCE, "Last Co-ins");
                    c1Payment.SetData(0, COL_LAST_WITHHOLD, "Last Withhold");
                    c1Payment.SetData(0, COL_ISCORRECTION, "Is Correction");

                    //c1Payment.SetData(0, COL_BALANCE, "Balance");
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
                { c1Payment.Cols[COL_CLAIMDISPNO].Visible = true; }
                else if (c1Payment.Name == c1SinglePayment.Name || c1Payment.Name == c1SinglePaymentTotal.Name || c1Payment.Name == c1SinglePaymentCorrTB.Name)
                { c1Payment.Cols[COL_CLAIMDISPNO].Visible = false; }

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
                c1Payment.Cols[COL_UNIT].Visible = false;
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

                c1Payment.Cols[COL_ISSPLITTED].Visible = false;
                //c1Payment.Cols[COL_CREATEDDATETIME].Visible = false;
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
                    c1Payment.Cols[COL_MODIFIER].Width = 75;
                    c1Payment.Cols[COL_CROSSWALK_CPT_CODE].Width = 50;
                    c1Payment.Cols[COL_CROSSWALK_CPT_DESC].Width = 0;

                    c1Payment.Cols[COL_CHARGE].Width = 0;
                    c1Payment.Cols[COL_UNIT].Width = 0;
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

                    c1Payment.Cols[COL_NEXT].Width = 92;
                    c1Payment.Cols[COL_PARTY].Width = 98;
                    c1Payment.Cols[COL_REASON].Width = 22;//19;

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

                c1Payment.Cols[COL_NEXT].DataType = typeof(System.String);
                c1Payment.Cols[COL_PARTY].DataType = typeof(System.String);
                //c1Payment.Cols[COL_REASON].DataType = typeof(System.String);

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
                //c1Payment.Cols[COL_CREATEDDATETIME].DataType = typeof(System.DateTime);

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
                //c1Payment.Cols[COL_REASON].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
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

                if (c1Payment.Name == c1SinglePayment.Name || c1Payment.Name == c1MultiplePayment.Name || c1Payment.Name == c1SinglePaymentCorrTB.Name)
                {
                    string _comboList = "";
                    EOBPayment.gloEOBPaymentInsurance ogloEOBPayIns = new global::gloBilling.EOBPayment.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);

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
               

                    _comboList = InsurancePayment.GetNextActions();

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
                        _comboList = InsurancePayment.GetInsuranceParties(PatientControl.ClaimNumber);
                        csEditableParty.ComboList = _comboList;
                    }

                    C1.Win.C1FlexGrid.CellStyle csEditableReason; // = c1Payment.Styles.Add("cs_EditableReason");
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
                        }

                    }
                    catch
                    {
                        csEditableReason = c1Payment.Styles.Add("cs_EditableReason");
                        csEditableReason.DataType = typeof(C1.Win.C1FlexGrid.C1FlexGrid);
                        csEditableReason.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csEditableReason.BackColor = Color.White;

                    }
    
                    csEditableReason.ComboList = "...";

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
                    //c1Payment.Cols[COL_REASON].Visible = false;

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
                    //c1Payment.Cols[COL_REASON].Visible = false;
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
                    c1Payment.SetData(0, COL_CPT_CODE, "Total : ");
                    c1Payment.SetCellStyle(0, COL_CPT_CODE, csTotalHeader);

                    #endregion " Set the style for the total grid "
                }

                c1Payment.ExtendLastCol = true;

            }
            catch
            {
            }
            finally
            {
                //_isFormLoading = false; 
                c1Payment.Redraw = true;
                c1SinglePayment.TabStop = false;
            }
        }

        private void DesignClaimDetailsGrid()
        {
            try
            {

                c1ClaimDetails.Clear();
                c1ClaimDetails.Cols.Count = COL_CLM_COUNT;
                c1ClaimDetails.Rows.Count = 1;
                c1ClaimDetails.Rows.Fixed = 1;
                c1ClaimDetails.Cols.Fixed = 0;

                c1ClaimDetails.ScrollBars = ScrollBars.None;

                #region " Set Headers "

                c1ClaimDetails.SetData(0, COL_CLM_FILEDDATE, "Date Filed");
                c1ClaimDetails.SetData(0, COL_CLM_REFILEDDATE, "Date Refiled");
                c1ClaimDetails.SetData(0, COL_CLM_INSURANCENAME, "Insurance");
                c1ClaimDetails.SetData(0, COL_CLM_FILEDAMOUNT, "Filed Amount");

                #endregion " Set Headers "

                #region " Set Width "

                c1ClaimDetails.Cols[COL_CLM_FILEDDATE].Width = 110;
                c1ClaimDetails.Cols[COL_CLM_REFILEDDATE].Width = 110;
                c1ClaimDetails.Cols[COL_CLM_INSURANCENAME].Width = 300;
                c1ClaimDetails.Cols[COL_CLM_FILEDAMOUNT].Width = 110;

                #endregion " Set Width "

                #region " Set Visibitly "

                c1ClaimDetails.Cols[COL_CLM_FILEDDATE].Visible = true;
                c1ClaimDetails.Cols[COL_CLM_REFILEDDATE].Visible = true;
                c1ClaimDetails.Cols[COL_CLM_INSURANCENAME].Visible = true;
                c1ClaimDetails.Cols[COL_CLM_FILEDAMOUNT].Visible = true;

                #endregion " Set Visibitly "

                #region " Set Data Type "

                c1ClaimDetails.Cols[COL_CLM_FILEDDATE].DataType = typeof(System.String);
                c1ClaimDetails.Cols[COL_CLM_REFILEDDATE].DataType = typeof(System.String);
                c1ClaimDetails.Cols[COL_CLM_INSURANCENAME].DataType = typeof(System.String);
                c1ClaimDetails.Cols[COL_CLM_FILEDAMOUNT].DataType = typeof(System.Decimal);

                #endregion " Set Data Type "

                #region " Set Alignment "

                c1ClaimDetails.Cols[COL_CLM_FILEDDATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ClaimDetails.Cols[COL_CLM_REFILEDDATE].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ClaimDetails.Cols[COL_CLM_INSURANCENAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
                c1ClaimDetails.Cols[COL_CLM_FILEDAMOUNT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;

                #endregion " Set Alignment "

                #region " Set Style "

                C1.Win.C1FlexGrid.CellStyle csCurrencyStyle;// = c1ClaimDetails.Styles.Add("cs_CurrencyStyle");
                try
                {
                    if (c1ClaimDetails.Styles.Contains("cs_CurrencyStyle"))
                    {
                        csCurrencyStyle = c1ClaimDetails.Styles["cs_CurrencyStyle"];
                    }
                    else
                    {
                        csCurrencyStyle = c1ClaimDetails.Styles.Add("cs_CurrencyStyle");
                        csCurrencyStyle.DataType = typeof(System.Decimal);
                        csCurrencyStyle.Format = "c";
                        csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                        csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;
                    }

                }
                catch
                {
                    csCurrencyStyle = c1ClaimDetails.Styles.Add("cs_CurrencyStyle");
                    csCurrencyStyle.DataType = typeof(System.Decimal);
                    csCurrencyStyle.Format = "c";
                    csCurrencyStyle.Font = gloGlobal.clsgloFont.gFont_SMALL_BOLD;//new System.Drawing.Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    csCurrencyStyle.TextEffect = C1.Win.C1FlexGrid.TextEffectEnum.Flat;

                }
          
                c1ClaimDetails.Cols[COL_CLM_FILEDAMOUNT].Style = csCurrencyStyle;

                #endregion " Set Style "

                c1ClaimDetails.AllowEditing = false;
                c1ClaimDetails.VisualStyle = VisualStyle.Office2007Blue;

                c1ClaimDetails.Styles[CellStyleEnum.SelectedColumnHeader].ForeColor = Color.FromArgb(31, 73, 125);
                c1ClaimDetails.Styles[CellStyleEnum.Fixed].ForeColor = Color.FromArgb(31, 73, 125);
                c1ClaimDetails.Styles[CellStyleEnum.Alternate].ForeColor = Color.FromArgb(31, 73, 125);
            }
            catch
            {
            }
        }

        #endregion

        #region " Methods that not in use "

        private void TEmpSaveNextAction(Int64 EOBPaymentID, Int64 EOBPaymentDetailID, Int64 EOBID)
        {
            EOBPayment.Common.PaymentInsuranceLineNextActions NextActions = new EOBPayment.Common.PaymentInsuranceLineNextActions();
            EOBPayment.Common.PaymentInsuranceLineNextAction LineNextAction = null;
            SplitClaimDetails oSplitClaimDetails = new SplitClaimDetails();
            EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new EOBPayment.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);

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
                            oSplitClaimDetails.ClinicID = AppSettings.ClinicID;
                            oSplitClaimDetails.EOBPaymentID = EOBPaymentID;
                            oSplitClaimDetails.EOBID = EOBID;
                        }
                    }

                    if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                    {
                        if (c1SinglePayment.GetData(i, COL_NEXT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT)).Trim() != "")
                        {
                            #region " Entire "

                            bool _addSplitLine = false;
                            //bool _isPendingLine = false;
                            LineNextAction = new EOBPayment.Common.PaymentInsuranceLineNextAction();
                            LineNextAction.ID = 0;
                            LineNextAction.ClaimNo = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                            LineNextAction.EOBPaymentID = EOBPaymentID;
                            LineNextAction.EOBID = EOBID;
                            LineNextAction.EOBPaymentDetailID = 0;

                            LineNextAction.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                            LineNextAction.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));

                            LineNextAction.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));
                            LineNextAction.TrackBillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                            LineNextAction.TrackBillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));

                            //...Code added on 20100129 by Sagar Ghodke
                            //...Code added to set the close date,user for Responsibility
                            if (mskCloseDate.MaskCompleted == true)
                            {
                                mskCloseDate.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
                                LineNextAction.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text);
                            }
                            LineNextAction.UserID = AppSettings.UserID;
                            LineNextAction.UserName = AppSettings.UserName;

                            //...ENd code add 20100129

                            string _nextAction = "";
                            string _nextActionCode = "";
                            string _nextActionDesc = "";

                            _nextAction = Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT)).Trim();
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
                            if (c1SinglePayment.GetData(i, COL_PARTY) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PARTY)).Trim() != "")
                            {
                                string _party = "";
                                string _partyCode = "";
                                string _partyDesc = "";
                                Int64 _partyInsId = 0;
                                Int64 _partyContactId = 0;

                                _party = Convert.ToString(c1SinglePayment.GetData(i, COL_PARTY)).Trim();
                                _partyCode = _party.Substring(0, _party.IndexOf('-'));
                                _partyDesc = _party.Substring(_party.IndexOf('-') + 1, (_party.Length - _party.IndexOf('-')) - 1);

                                //.... Get the next party insuranceid & contactid number for the claim
                                DataTable _dt = null;
                                _dt = PatientStripControl.GetInsuranceParties(PatientControl.ClaimNumber, PatientControl.PatientID); //_dt = ogloEOBPaymentInsurance.GetClaimInsurances(PatientControl.ClaimNumber, PatientControl.PatientID);

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

                                LineNextAction.NextActionPatientInsID = _partyInsId;
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
                    }
                }

                ogloEOBPaymentInsurance.UpdateParty(NextActions);
                ogloEOBPaymentInsurance.UpdateNextAction(NextActions);


                DataTable _dtHoldInfo = GetSplittedClaimsHoldInfo(oSplitClaimDetails);

                if (oSplitClaimDetails != null && oSplitClaimDetails.Lines.Count > 0)
                {
                    bool _splitFlag = false;
                    gloSplitClaim ogloSplitClaim = new gloSplitClaim(AppSettings.ConnectionStringPM);
                    // Set the claim remittance ref no to the split object
                    oSplitClaimDetails.ClaimRemittanceReferenceNo = ClaimRemittanceReferenceNo;

                    // Payment allocation check added to resolved the following issue
                    // Issue : if pending is selected, split should not happened.
                    if (IsPaymentAllocated)
                    { oSplitClaimDetails.IsPaymentDone = true; }
                    else
                    { oSplitClaimDetails.IsPaymentDone = false; }

                    //_splitFlag = ogloSplitClaim.SplitTransactionClaim(oSplitClaimDetails);
                    _splitFlag = ogloSplitClaim.SplitTransactionClaim(oSplitClaimDetails, _dtHoldInfo);
                    ogloSplitClaim.Dispose();
                }

                #endregion " Line Next Action & Party "
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }

        }

        /// <summary>
        /// Not in use , to be deleted..
        /// Need to refactor this method - TO DO.
        /// </summary>
        /// <param name="TransactionMasterID"></param>
        private void NotifyIfClaimSplitted(Int64 TransactionMasterID, Int64 TransactionID)
        {
            gloBilling ogloBilling = new gloBilling(AppSettings.ConnectionStringPM, "");
            global::gloBilling.Common.Transaction oTransaction = new global::gloBilling.Common.Transaction();
            oTransaction.TransactionMasterID = TransactionMasterID;
            oTransaction.TransactionID = TransactionID;

            bool _isSplitted = ogloBilling.IsClaimSplitted(oTransaction);

            if (_isSplitted)
            {
                lblAlertMessage.BringToFront();
                lblAlertMessage.Visible = true;
            }
            else
            {
                lblAlertMessage.Visible = false;
            }

            ogloBilling.Dispose();
            oTransaction.Dispose();
        }

        /// <summary>
        /// Backup of the updated method PerformSavePayment().
        /// </summary>
        /// <returns></returns>
        private bool PerformSavePayment_old()
        {
            bool _proceedToSave = false;
            bool _isPaymentSaved = false;

            bool _isPaymentDone = IsPaymentMade();

            // There are 2 cases for which we allow to proceed for payment
            // 1) Check if any payment has been made to save 
            if (_isPaymentDone)
            { _proceedToSave = true; }
            // 2) This condition will allow user to update check details eg. check number & check amount
            else if (txtCheckNumber.Text.Trim() != "" && txtCheckAmount.Text.Trim() != "")
            { _proceedToSave = true; }

            if (_proceedToSave)
            {
                // Check necessary validation before proceed 
                //if (SavePaymentValidation(_isPaymentDone))
                //{ _isPaymentSaved = SavePayment(); }
            }
            // If responsibility needs to be transfer without any allocation
            // Check if any next action selected 
            else if (IsNextActionUpdated)
            {
                // Check necessary validation before proceed
                if (SaveNextActionValidation())
                { _isPaymentSaved = SaveNextActionParty(); }
            }
            // Note : 
            // 1) If no payment has been made 
            // 2) If no responsibility transferred then show following message
            else
            {
                MessageBox.Show("No payment has been made to save. ", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _isPaymentSaved = false;
            }
            return _isPaymentSaved;
        }

        /// <summary>
        /// NextAction Save validations are handled in the SavePaymentValidation() method.
        /// </summary>
        /// <returns></returns>
        private bool SaveNextActionValidation()
        {
            if (CheckNextActionPartySelected() == false)
            {
                return false;
            }
            else if (!IsValidCloseDate())
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// NextAction save is handled in SavePayment() method.
        /// </summary>
        /// <returns></returns>
        private bool SaveNextActionParty()
        {
            bool _IsDataSaved = false;

            EOBPayment.gloEOBPaymentInsurance ogloEOBPaymentInsurance = new EOBPayment.gloEOBPaymentInsurance(AppSettings.ConnectionStringPM);
            EOBPayment.Common.PaymentInsuranceLineNextActions oPaymentInsuranceLineNextActions = new EOBPayment.Common.PaymentInsuranceLineNextActions();
            EOBPayment.Common.PaymentInsuranceLineNextAction LineNextAction = null;
            SplitClaimDetails oSplitClaimDetails = new SplitClaimDetails();

            bool _addToSplitLine = false;
            string _nextAction = "";
            string _nextActionCode = "";
            string _nextActionDesc = "";
            string _party = "";
            string _partyCode = "";
            string _partyDesc = "";
            Int64 _partyInsId = 0;
            Int64 _partyContactId = 0;
            decimal _nextActionAmount = 0;
            SplitClaimLine oSplitLine = null;
            Int64 _trnId = 0;
            Int64 _trnMstId = 0;
            string _subClaimNo = "";
            Int64 _claimNo = 0;

            try
            {
                for (int i = 0; i <= c1SinglePayment.Rows.Count - 1; i++)
                {
                    _addToSplitLine = false;
                    _nextActionAmount = 0;

                    if (c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) != null && (ColServiceLineType)c1SinglePayment.GetData(i, COL_SERVICELINE_TYPE) == ColServiceLineType.ServiceLine)
                    {
                        if (c1SinglePayment.GetData(i, COL_NEXT) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT)).Trim() != "")
                        {
                            LineNextAction = new EOBPayment.Common.PaymentInsuranceLineNextAction();

                            LineNextAction.ID = 0;
                            LineNextAction.ClaimNo = Convert.ToInt64(c1SinglePayment.GetData(i, COL_CLAIMNO));
                            LineNextAction.EOBPaymentID = 0;
                            LineNextAction.EOBID = 0;
                            LineNextAction.EOBPaymentDetailID = 0;
                            LineNextAction.BillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_ID));
                            LineNextAction.BillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_BILLING_TRANSACTON_DETAILID));
                            LineNextAction.TrackBillingTransactionID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_ID));
                            LineNextAction.TrackBillingTransactionDetailID = Convert.ToInt64(c1SinglePayment.GetData(i, COL_TRACK_BILLING_TRANSACTON_DETAILID));
                            LineNextAction.SubClaimNo = Convert.ToString(c1SinglePayment.GetData(i, COL_SUBCLAIMNO));

                            _trnMstId = LineNextAction.BillingTransactionID;
                            _trnId = LineNextAction.TrackBillingTransactionID;
                            _subClaimNo = LineNextAction.SubClaimNo;
                            _claimNo = LineNextAction.ClaimNo;

                            LineNextAction.CloseDate = gloDateMaster.gloDate.DateAsNumber(mskCloseDate.Text.ToString());
                            LineNextAction.UserID = AppSettings.UserID;
                            LineNextAction.UserName = AppSettings.UserName;

                            _nextAction = Convert.ToString(c1SinglePayment.GetData(i, COL_NEXT)).Trim();
                            _nextActionCode = _nextAction.Substring(0, _nextAction.IndexOf('-'));
                            _nextActionDesc = _nextAction.Substring(_nextAction.IndexOf('-') + 1, (_nextAction.Length - _nextAction.IndexOf('-')) - 1);

                            //.. Split the claim for all the actions (Rebill, Bill, Pending, None)
                            if (_nextActionCode == "R" || _nextActionCode == "B" || _nextActionCode == "P" || _nextActionCode == "N")
                            {
                                LineNextAction.HasActionData = true;
                                LineNextAction.HasNextData = true;

                                _addToSplitLine = true;
                            }

                            LineNextAction.NextActionCode = _nextActionCode;
                            LineNextAction.NextActionDescription = _nextActionDesc;

                            if (c1SinglePayment.GetData(i, COL_PARTY) != null && Convert.ToString(c1SinglePayment.GetData(i, COL_PARTY)).Trim() != "")
                            {
                                _party = "";
                                _partyCode = "";
                                _partyDesc = "";
                                _partyInsId = 0;
                                _partyContactId = 0;

                                _party = Convert.ToString(c1SinglePayment.GetData(i, COL_PARTY)).Trim();
                                _partyCode = _party.Substring(0, _party.IndexOf('-'));
                                _partyDesc = _party.Substring(_party.IndexOf('-') + 1, (_party.Length - _party.IndexOf('-')) - 1);

                                //.... Get the next party insuranceid & contactid number for the claim
                                DataTable _dt = null;
                                _dt = ogloEOBPaymentInsurance.GetClaimInsurances(PatientControl.ClaimNumber, PatientControl.PatientID);
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

                                LineNextAction.NextActionPatientInsID = _partyInsId;
                                LineNextAction.NextActionPatientInsName = _partyDesc.Trim();
                                LineNextAction.NextActionPartyNumber = Convert.ToInt32(_partyCode.Trim());
                                LineNextAction.NextActionContactID = _partyContactId;
                                LineNextAction.HasNextData = true;
                            }

                            if (LineNextAction.NextActionPatientInsID == 0) //.self resp.
                            { LineNextAction.NextPartyType = PayerMode.Self; }
                            else
                            { LineNextAction.NextPartyType = PayerMode.Insurance; }

                            LineNextAction.HasData = true;
                            LineNextAction.NextActionAmount = _nextActionAmount;
                            LineNextAction.IsNullNextActionAmount = false;
                            LineNextAction.ClinicID = AppSettings.ClinicID;

                            oPaymentInsuranceLineNextActions.Add(LineNextAction);

                            #region " Set Split Claim Details "

                            if (_addToSplitLine == true)
                            {
                                oSplitLine = new SplitClaimLine();
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
                        }
                    }
                }

                oSplitClaimDetails.TransactionMasterID = _trnMstId;
                oSplitClaimDetails.TransactionID = _trnId;
                oSplitClaimDetails.ClaimNo = _claimNo;
                oSplitClaimDetails.SubClaimNo = _subClaimNo;
                oSplitClaimDetails.ClinicID = AppSettings.ClinicID;

                if (oPaymentInsuranceLineNextActions != null && oPaymentInsuranceLineNextActions.Count > 0)
                {
                    gloSplitClaim ogloSplitClaim = null;
                    DataTable _dtHoldInfo = GetSplittedClaimsHoldInfo(oSplitClaimDetails);

                    // Next action & party update implementation is pending - TO DO (07052010)
                    //InsurancePayment.UpdateNextActionNParty(oPaymentInsuranceLineNextActions);

                    ogloEOBPaymentInsurance.UpdateParty(oPaymentInsuranceLineNextActions);
                    ogloEOBPaymentInsurance.UpdateNextAction(oPaymentInsuranceLineNextActions);

                    if (oSplitClaimDetails != null && oSplitClaimDetails.Lines.Count > 0)
                    {
                        bool _splitFlag = false;
                        ogloSplitClaim = new gloSplitClaim(AppSettings.ConnectionStringPM);
                        oSplitClaimDetails.IsPaymentDone = false;

                        // Set the claim remittance ref no to the split object
                        oSplitClaimDetails.ClaimRemittanceReferenceNo = ClaimRemittanceReferenceNo;
                        //oSplitClaimDetails.IsPaymentDone = true;

                        _splitFlag = ogloSplitClaim.SplitTransactionClaim(oSplitClaimDetails, _dtHoldInfo);
                        ogloSplitClaim.Dispose();
                    }
                }
                _IsDataSaved = true;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                _IsDataSaved = false;
            }
            finally
            {
                if (ogloEOBPaymentInsurance != null) { ogloEOBPaymentInsurance.Dispose(); }
                if (oPaymentInsuranceLineNextActions != null) { oPaymentInsuranceLineNextActions.Dispose(); }
                if (LineNextAction != null) { LineNextAction.Dispose(); }
                if (oSplitClaimDetails != null) { oSplitClaimDetails.Dispose(); }
                if (oSplitLine != null) { oSplitLine.Dispose(); }

                ResetForm();
            }
            return _IsDataSaved;
        }


        #endregion

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

        private void tsb_PaymentPatient_Click(object sender, EventArgs e)
        {
            if (PatientControl != null && PatientControl.PatientID > 0 && PatientControl.ClaimNumber > 0)
            {
                try
                {
                    using (frmPaymentPatient oPaymentInsurace = new frmPaymentPatient(PatientControl.PatientID, false, 0, 0, 0, 0, EOBPaymentSubType.Other))
                    {
                        oPaymentInsurace.StartPosition = FormStartPosition.CenterScreen;
                        oPaymentInsurace.WindowState = FormWindowState.Maximized;
                        oPaymentInsurace.ShowInTaskbar = false;
                        oPaymentInsurace.ShowDialog(this);
                        PatientControl.IsRevisedPayment = false;
                        PatientControl.FillDetails(ClaimDetails.TransactionMasterID, ClaimDetails.TransactionID);
                        FillPaymentGrid();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select the claim", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tsb_Refund_Click(object sender, EventArgs e)
        {
            using (frmInsurancePaymentRefundLog ofrmInsurancePaymentRefundLog = new frmInsurancePaymentRefundLog())
            {
                try
                {
                    ofrmInsurancePaymentRefundLog.EOBInsurancePaymentMasterLines = this.EOBInsurancePaymentMasterLines.Copy();
                    ofrmInsurancePaymentRefundLog.ShowDialog(this);
                    if (IsPendingCheckLoaded)
                    {
                        bool _isPaymentVoid = InsurancePayment.IsVoidedInsurancePayment(EOBPaymentID);
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
                    ofrmInsurancePaymentRefundLog.Dispose();
                }
            }
        }

        private void tls_btnPatAcct_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClaimDetails.IsClaimExist)
                {
                    if (ClaimDetails.TransactionID != 0)
                    {
                        frmPatientFinancialView frm = new frmPatientFinancialView(PatientControl.PatientID);
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.WindowState = FormWindowState.Maximized;
                        frm.ShowInTaskbar = false;
                        frm.ShowDialog(this);
                        frm.Dispose();
                        frm = null;
                        LoadClaim();
                    }
                }
                else
                {
                    MessageBox.Show("Please select the claim", AppSettings.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch //(Exception ex)
            {

            }
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            pnlSinglePaymentCorrTB.Height = (pnlSinglePayment.Height - c1SinglePaymentTotal.Height) / 2;
        }

        private void txtCheckAmount_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
